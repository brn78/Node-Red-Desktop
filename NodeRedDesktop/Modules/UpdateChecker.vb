Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports System.Threading
Imports System.Web.Script.Serialization
Imports System.Threading.Tasks
Imports System.Text.RegularExpressions
Imports NodeRedDesktop

Namespace Modules

''' <summary>
''' Modulo per il controllo degli aggiornamenti di Node-RED e Node.js.
''' Interroga i registry npm e nodejs.org per confrontare le versioni.
''' </summary>
Public Module UpdateChecker

#Region "Tipi"

    ''' <summary>
    ''' Contiene le informazioni su un aggiornamento disponibile per un componente.
    ''' </summary>
    Public Structure UpdateInfo
        ''' <summary>Nome del componente (es. "Node-RED", "Node.js").</summary>
        Public ComponentName As String
        ''' <summary>Versione attualmente installata (o messaggio di errore).</summary>
        Public CurrentVersion As String
        ''' <summary>Ultima versione disponibile nel repository.</summary>
        Public LatestVersion As String
        ''' <summary>True se la versione disponibile e piu recente di quella installata.</summary>
        Public IsUpdateAvailable As Boolean
        ''' <summary>URL della release sul repository ufficiale.</summary>
        Public ReleaseUrl As String
        ''' <summary>URL del changelog della versione disponibile.</summary>
        Public ChangelogUrl As String
        ''' <summary>URL diretto di download dell'asset installer (.exe).</summary>
        Public DownloadUrl As String
        ''' <summary>Note di rilascio della versione.</summary>
        Public ReleaseNotes As String
        ''' <summary>Hash SHA-256 atteso dell'installer (esadecimale), se pubblicato nelle note di rilascio.</summary>
        Public ExpectedSha256 As String
        ''' <summary>URL dell'asset contenente l'hash SHA-256 (*.sha256 / SHA256SUMS), se pubblicato.</summary>
        Public Sha256Url As String
    End Structure

#End Region

#Region "Verifica Aggiornamenti"

    ''' <summary>
    ''' Verifica in modo asincrono se e disponibile un aggiornamento per Node-RED.
    ''' Interroga https://registry.npmjs.org/node-red/latest
    ''' </summary>
    ''' <returns>UpdateInfo con informazioni sull aggiornamento disponibile.</returns>
    Public Async Function CheckNodeRedUpdateAsync() As Task(Of UpdateInfo)
        Dim info As New UpdateInfo()
        info.ComponentName = "Node-RED"
        info.IsUpdateAvailable = False
        info.ReleaseUrl = "https://github.com/node-red/node-red/releases"
        info.ChangelogUrl = "https://github.com/node-red/node-red/blob/master/CHANGELOG.md"

        Try
            info.CurrentVersion = Await GetInstalledNodeRedVersionAsync()

            Dim jsonResponse As String = Await FetchWithTimeoutAsync("https://registry.npmjs.org/node-red/latest", 10000)

            Dim versionMatch As Match = Regex.Match(jsonResponse, """version""\s*:\s*""([^""]+)""")
            If Not versionMatch.Success Then
                LogManager.AddWarn("Impossibile estrarre la versione da registry.npmjs.org/node-red/latest.", "UpdateChecker")
                Return info
            End If

            info.LatestVersion = versionMatch.Groups(1).Value

            If Not String.IsNullOrWhiteSpace(info.CurrentVersion) AndAlso
               Not info.CurrentVersion.StartsWith("Errore") Then
                info.IsUpdateAvailable = (CompareVersions(info.LatestVersion, info.CurrentVersion) > 0)
            End If

            If info.IsUpdateAvailable Then
                LogManager.AddInfo(String.Format("Aggiornamento Node-RED disponibile: {0} -> {1}", info.CurrentVersion, info.LatestVersion), "UpdateChecker")
            Else
                LogManager.AddInfo(String.Format("Node-RED aggiornato: {0}", info.CurrentVersion), "UpdateChecker")
            End If

        Catch ex As WebException
            Dim errMsg As String = String.Format("Errore rete: {0}", ex.Message)
            LogManager.AddWarn(String.Format("CheckNodeRedUpdate - {0}", errMsg), "UpdateChecker")
            info.CurrentVersion = errMsg
            info.IsUpdateAvailable = False
        Catch ex As Exception
            Dim errMsg As String = String.Format("Errore: {0}", ex.Message)
            LogManager.AddError(String.Format("CheckNodeRedUpdate - {0}", errMsg), "UpdateChecker")
            info.CurrentVersion = errMsg
            info.IsUpdateAvailable = False
        End Try

        Return info
    End Function

    ''' <summary>
    ''' Verifica in modo asincrono se e disponibile un aggiornamento per Node.js (versione LTS).
    ''' Interroga https://nodejs.org/dist/index.json
    ''' </summary>
    ''' <returns>UpdateInfo con informazioni sull aggiornamento disponibile.</returns>
    Public Async Function CheckNodeJsUpdateAsync() As Task(Of UpdateInfo)
        Dim info As New UpdateInfo()
        info.ComponentName = "Node.js"
        info.IsUpdateAvailable = False
        info.ReleaseUrl = "https://nodejs.org/en/download/"
        info.ChangelogUrl = "https://github.com/nodejs/node/blob/main/CHANGELOG.md"

        Try
            info.CurrentVersion = Await GetInstalledNodeJsVersionAsync()

            Dim jsonResponse As String = Await FetchWithTimeoutAsync("https://nodejs.org/dist/index.json", 10000)

            Dim ltsVersion As String = ExtractFirstLtsVersion(jsonResponse)

            If String.IsNullOrWhiteSpace(ltsVersion) Then
                LogManager.AddWarn("Impossibile trovare la versione LTS di Node.js.", "UpdateChecker")
                Return info
            End If

            info.LatestVersion = ltsVersion.TrimStart("v"c)

            Dim currentClean As String = info.CurrentVersion.TrimStart("v"c)
            If Not String.IsNullOrWhiteSpace(currentClean) AndAlso
               Not currentClean.StartsWith("Errore") Then
                info.IsUpdateAvailable = (CompareVersions(info.LatestVersion, currentClean) > 0)
            End If

            If info.IsUpdateAvailable Then
                LogManager.AddInfo(String.Format("Aggiornamento Node.js disponibile: {0} -> {1}", info.CurrentVersion, info.LatestVersion), "UpdateChecker")
            Else
                LogManager.AddInfo(String.Format("Node.js aggiornato: {0}", info.CurrentVersion), "UpdateChecker")
            End If

        Catch ex As WebException
            Dim errMsg As String = String.Format("Errore rete: {0}", ex.Message)
            LogManager.AddWarn(String.Format("CheckNodeJsUpdate - {0}", errMsg), "UpdateChecker")
            info.CurrentVersion = errMsg
            info.IsUpdateAvailable = False
        Catch ex As Exception
            Dim errMsg As String = String.Format("Errore: {0}", ex.Message)
            LogManager.AddError(String.Format("CheckNodeJsUpdate - {0}", errMsg), "UpdateChecker")
            info.CurrentVersion = errMsg
            info.IsUpdateAvailable = False
        End Try

        Return info
    End Function

    ''' <summary>
    ''' Verifica in modo asincrono se è disponibile un aggiornamento per Ollama.
    ''' Interroga le release GitHub ufficiali (api.github.com/repos/ollama/ollama/releases/latest).
    ''' </summary>
    Public Async Function CheckOllamaUpdateAsync() As Task(Of UpdateInfo)
        Dim info As New UpdateInfo()
        info.ComponentName = "Ollama"
        info.IsUpdateAvailable = False
        info.ReleaseUrl = "https://github.com/ollama/ollama/releases"
        info.ChangelogUrl = "https://github.com/ollama/ollama/releases"

        Try
            info.CurrentVersion = Await Task.Run(Function() DependencyChecker.GetOllamaVersion())

            Dim jsonResponse As String = Await FetchWithTimeoutAsync("https://api.github.com/repos/ollama/ollama/releases/latest", 10000)
            Dim tagMatch As Match = Regex.Match(jsonResponse, """tag_name""\s*:\s*""([^""]+)""")
            If tagMatch.Success Then
                info.LatestVersion = tagMatch.Groups(1).Value.TrimStart("v"c)
                Dim currentClean = info.CurrentVersion.TrimStart("v"c)
                If Not String.IsNullOrWhiteSpace(currentClean) AndAlso Not currentClean.StartsWith("Errore") Then
                    info.IsUpdateAvailable = (CompareVersions(info.LatestVersion, currentClean) > 0)
                End If
            End If
        Catch ex As Exception
            info.IsUpdateAvailable = False
        End Try

        Return info
    End Function

    ''' <summary>
    ''' Verifica in modo asincrono se è disponibile una nuova release di Node-RED Desktop su GitHub.
    ''' Interroga api.github.com/repos/brn78/Node-Red-Desktop/releases/latest e ne interpreta
    ''' la risposta JSON con un parser reale (JavaScriptSerializer) anziché con espressioni regolari.
    ''' Oltre all'installer (.exe) cerca anche l'hash SHA-256 pubblicato con la release
    ''' (asset "*.sha256" / "SHA256SUMS*" oppure un hash a 64 cifre esadecimali nelle note di rilascio),
    ''' che verrà usato per validare il download prima dell'esecuzione.
    ''' </summary>
    Public Async Function CheckDesktopAppUpdateAsync() As Task(Of UpdateInfo)
        Dim info As New UpdateInfo()
        info.ComponentName = "Node-RED Desktop"
        info.IsUpdateAvailable = False
        info.CurrentVersion = GetCurrentAppVersion()
        info.ReleaseUrl = DesktopReleasesUrl
        info.ChangelogUrl = DesktopReleasesUrl

        Try
            Dim jsonResponse As String = Await FetchWithTimeoutAsync(DesktopLatestReleaseApi, 10000)

            Dim serializer As New JavaScriptSerializer()
            serializer.MaxJsonLength = Integer.MaxValue
            Dim release As Dictionary(Of String, Object) = TryCast(serializer.DeserializeObject(jsonResponse), Dictionary(Of String, Object))
            If release Is Nothing Then
                Throw New FormatException("Risposta GitHub non riconosciuta (atteso oggetto JSON).")
            End If

            Dim tag As String = JsonString(release, "tag_name")
            If Not String.IsNullOrWhiteSpace(tag) Then
                info.LatestVersion = tag.TrimStart("v"c, "V"c)
                info.IsUpdateAvailable = (CompareVersions(info.LatestVersion, info.CurrentVersion) > 0)
            End If

            Dim htmlUrl As String = JsonString(release, "html_url")
            If IsHttpsUrl(htmlUrl) Then
                info.ReleaseUrl = htmlUrl
                info.ChangelogUrl = htmlUrl
            End If

            info.ReleaseNotes = JsonString(release, "body")

            ' Asset: installer .exe (preferito quello con il nome ufficiale) e file hash
            Dim assets As Object() = TryCast(JsonValue(release, "assets"), Object())
            If assets IsNot Nothing Then
                Dim firstExe As String = Nothing
                For Each a As Object In assets
                    Dim asset As Dictionary(Of String, Object) = TryCast(a, Dictionary(Of String, Object))
                    If asset Is Nothing Then Continue For
                    Dim name As String = If(JsonString(asset, "name"), "")
                    Dim url As String = JsonString(asset, "browser_download_url")
                    If Not IsHttpsUrl(url) Then Continue For

                    Dim lower As String = name.ToLowerInvariant()
                    If lower = InstallerAssetName.ToLowerInvariant() Then
                        info.DownloadUrl = url
                    ElseIf lower.EndsWith(".exe") AndAlso firstExe Is Nothing Then
                        firstExe = url
                    ElseIf lower.EndsWith(".sha256") OrElse lower.StartsWith("sha256sums") Then
                        info.Sha256Url = url
                    End If
                Next
                If String.IsNullOrEmpty(info.DownloadUrl) Then info.DownloadUrl = firstExe
            End If

            ' Hash SHA-256 eventualmente incluso nelle note di rilascio
            If Not String.IsNullOrEmpty(info.ReleaseNotes) Then
                Dim hashMatch As Match = Regex.Match(info.ReleaseNotes, "\b([A-Fa-f0-9]{64})\b")
                If hashMatch.Success Then info.ExpectedSha256 = hashMatch.Groups(1).Value.ToLowerInvariant()
            End If

            If info.IsUpdateAvailable Then
                LogManager.AddInfo(String.Format("Nuova versione Node-RED Desktop disponibile: v{0} -> v{1}", info.CurrentVersion, info.LatestVersion), "UpdateChecker")
                If String.IsNullOrEmpty(info.ExpectedSha256) AndAlso String.IsNullOrEmpty(info.Sha256Url) Then
                    LogManager.AddWarn("La release non pubblica un hash SHA-256: l'installazione automatica richiederà un installer firmato.", "UpdateChecker")
                End If
            Else
                LogManager.AddInfo(String.Format("Node-RED Desktop è aggiornato (v{0})", info.CurrentVersion), "UpdateChecker")
            End If

        Catch ex As Exception
            LogManager.AddWarn(String.Format("CheckDesktopAppUpdateAsync: {0}", ex.Message), "UpdateChecker")
            info.IsUpdateAvailable = False
        End Try

        Return info
    End Function

    ''' <summary>
    ''' Scarica l'installer di Node-RED Desktop da GitHub releases, ne verifica l'integrità e lo esegue.
    ''' Politica di sicurezza: il file viene eseguito solo se
    '''  1) l'hash SHA-256 pubblicato con la release coincide con quello calcolato sul download, oppure
    '''  2) in assenza di hash pubblicato, l'eseguibile porta una firma Authenticode con catena valida.
    ''' In ogni altro caso il file viene eliminato e la funzione restituisce False
    ''' (dettaglio in <see cref="LastUpdateError"/>).
    ''' </summary>
    Public Async Function DownloadAndInstallAppUpdateAsync(info As UpdateInfo, progressCallback As Action(Of Integer, String)) As Task(Of Boolean)
        LastUpdateError = String.Empty
        Dim tempFile As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), InstallerAssetName)
        Try
            If Not IsHttpsUrl(info.DownloadUrl) Then
                LastUpdateError = "URL di download mancante o non HTTPS."
                LogManager.AddError(LastUpdateError, "UpdateChecker")
                Return False
            End If

            If System.IO.File.Exists(tempFile) Then
                Try : System.IO.File.Delete(tempFile) : Catch : End Try
            End If

            ' Hash atteso: dalle note di rilascio oppure dal file .sha256 pubblicato come asset
            Dim expectedHash As String = If(info.ExpectedSha256, "").Trim().ToLowerInvariant()
            If String.IsNullOrEmpty(expectedHash) AndAlso IsHttpsUrl(info.Sha256Url) Then
                progressCallback?.Invoke(0, "Recupero hash SHA-256 della release...")
                Try
                    Dim hashText As String = Await FetchWithTimeoutAsync(info.Sha256Url, 10000)
                    expectedHash = ExtractSha256ForFile(hashText, InstallerAssetName)
                Catch ex As Exception
                    LogManager.AddWarn(String.Format("Impossibile scaricare il file hash: {0}", ex.Message), "UpdateChecker")
                End Try
            End If

            progressCallback?.Invoke(0, "Avvio download nuovo installer...")
            Await DownloadFileWithProgressAsync(info.DownloadUrl, tempFile, progressCallback)

            If Not System.IO.File.Exists(tempFile) OrElse New System.IO.FileInfo(tempFile).Length < 50000 Then
                LastUpdateError = "File scaricato assente o troppo piccolo."
                LogManager.AddError(LastUpdateError, "UpdateChecker")
                Return False
            End If

            ' --- Verifica integrità ---
            progressCallback?.Invoke(100, "Verifica integrità dell'installer...")
            Dim actualHash As String = ComputeSha256(tempFile)

            If Not String.IsNullOrEmpty(expectedHash) Then
                If Not String.Equals(actualHash, expectedHash, StringComparison.OrdinalIgnoreCase) Then
                    LastUpdateError = String.Format("Hash SHA-256 non corrispondente (atteso {0}..., ottenuto {1}...). Il file è stato eliminato.", expectedHash.Substring(0, 12), actualHash.Substring(0, 12))
                    LogManager.AddError(LastUpdateError, "UpdateChecker")
                    SafeDelete(tempFile)
                    Return False
                End If
                LogManager.AddSuccess("Hash SHA-256 dell'installer verificato.", "UpdateChecker")
            Else
                Dim signer As String = Nothing
                If Not HasValidAuthenticodeSignature(tempFile, signer) Then
                    LastUpdateError = "La release non pubblica un hash SHA-256 e l'installer non è firmato digitalmente: installazione automatica annullata per sicurezza. Scarica manualmente dalla pagina GitHub."
                    LogManager.AddError(LastUpdateError, "UpdateChecker")
                    SafeDelete(tempFile)
                    Return False
                End If
                LogManager.AddSuccess(String.Format("Firma Authenticode valida ({0}). SHA-256: {1}", signer, actualHash), "UpdateChecker")
            End If

            progressCallback?.Invoke(100, "Download verificato! Avvio installer...")
            Dim psi As New ProcessStartInfo(tempFile)
            psi.UseShellExecute = True
            Process.Start(psi)
            Return True

        Catch ex As Exception
            LastUpdateError = String.Format("Download aggiornamento fallito: {0}", ex.Message)
            LogManager.AddError(LastUpdateError, "UpdateChecker")
            SafeDelete(tempFile)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Compatibilità: scarica e installa da un URL senza hash noto (si applica la politica
    ''' di verifica della firma Authenticode). Preferire l'overload con <see cref="UpdateInfo"/>.
    ''' </summary>
    Public Function DownloadAndInstallAppUpdateAsync(downloadUrl As String, progressCallback As Action(Of Integer, String)) As Task(Of Boolean)
        Dim info As New UpdateInfo()
        info.DownloadUrl = downloadUrl
        Return DownloadAndInstallAppUpdateAsync(info, progressCallback)
    End Function

    ''' <summary>
    ''' Applica un aggiornamento di Node-RED delegando a DependencyChecker.UpdateNodeRedAsync.
    ''' </summary>
    ''' <param name="progressCallback">Callback per ricevere messaggi di progresso.</param>
    ''' <returns>True se l aggiornamento e riuscito, False altrimenti.</returns>
    Public Async Function ApplyNodeRedUpdateAsync(progressCallback As Action(Of String)) As Task(Of Boolean)
        Try
            LogManager.AddInfo("Avvio aggiornamento Node-RED...", "UpdateChecker")
            If progressCallback IsNot Nothing Then progressCallback("Avvio aggiornamento Node-RED...")
            Dim result As Boolean = Await DependencyChecker.UpdateNodeRedAsync(progressCallback)
            If result Then
                LogManager.AddSuccess("Aggiornamento Node-RED completato con successo.", "UpdateChecker")
            Else
                LogManager.AddError("Aggiornamento Node-RED fallito.", "UpdateChecker")
            End If
            Return result
        Catch ex As Exception
            Dim msg As String = String.Format("Errore durante l aggiornamento Node-RED: {0}", ex.Message)
            LogManager.AddError(msg, "UpdateChecker")
            If progressCallback IsNot Nothing Then progressCallback(String.Format("ERRORE: {0}", msg))
            Return False
        End Try
    End Function

#End Region

#Region "Utilita Versioni"

    ''' <summary>
    ''' Confronta due stringhe di versione semantica nel formato "MAJOR.MINOR.PATCH".
    ''' </summary>
    ''' <param name="v1">Prima versione (es. "3.1.0").</param>
    ''' <param name="v2">Seconda versione (es. "3.0.2").</param>
    ''' <returns>
    ''' -1 se v1 minore di v2 (v1 e piu vecchia),
    '''  0 se v1 uguale a v2,
    '''  1 se v1 maggiore di v2 (v1 e piu recente).
    ''' </returns>
    Public Function CompareVersions(v1 As String, v2 As String) As Integer
        Try
            Dim clean1 As String = Regex.Replace(v1.Trim(), "[^0-9\.]", "")
            Dim clean2 As String = Regex.Replace(v2.Trim(), "[^0-9\.]", "")

            Dim parts1 As String() = clean1.Split("."c)
            Dim parts2 As String() = clean2.Split("."c)

            Dim maxLen As Integer = Math.Max(parts1.Length, parts2.Length)
            For i As Integer = 0 To maxLen - 1
                Dim num1 As Integer = 0
                Dim num2 As Integer = 0
                If i < parts1.Length Then Integer.TryParse(parts1(i), num1)
                If i < parts2.Length Then Integer.TryParse(parts2(i), num2)

                If num1 < num2 Then Return -1
                If num1 > num2 Then Return 1
            Next

            Return 0

        Catch ex As Exception
            LogManager.AddWarn(String.Format("CompareVersions: impossibile confrontare '{0}' e '{1}': {2}", v1, v2, ex.Message), "UpdateChecker")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Effettua una richiesta HTTP GET con timeout specificato in modo asincrono (HttpClient condiviso).
    ''' </summary>
    ''' <param name="url">URL da interrogare.</param>
    ''' <param name="timeoutMs">Timeout in millisecondi.</param>
    ''' <returns>Contenuto testuale della risposta.</returns>
    Private Async Function FetchWithTimeoutAsync(url As String, timeoutMs As Integer) As Task(Of String)
        Using cts As New CancellationTokenSource(timeoutMs)
            Try
                Using response As HttpResponseMessage = Await SharedHttp.GetAsync(url, cts.Token).ConfigureAwait(False)
                    response.EnsureSuccessStatusCode()
                    Return Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                End Using
            Catch ex As OperationCanceledException
                Throw New TimeoutException(String.Format("Timeout ({0}ms) raggiunto per {1}", timeoutMs, url))
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Estrae la versione della prima release LTS dall array JSON di nodejs.org/dist/index.json.
    ''' </summary>
    Private Function ExtractFirstLtsVersion(json As String) As String
        Try
            ' Pattern principale: cerca version seguito da lts non-false nello stesso oggetto
            Dim pattern As String = """version""\s*:\s*""(v[\d\.]+)""[^}]*?""lts""\s*:\s*""([^""]+)"""
            Dim m As Match = Regex.Match(json, pattern, RegexOptions.Singleline)
            If m.Success Then
                Return m.Groups(1).Value
            End If

            ' Pattern alternativo: lts prima di version
            Dim altPattern As String = """lts""\s*:\s*""([^""]+)""[^}]*?""version""\s*:\s*""(v[\d\.]+)"""
            Dim m2 As Match = Regex.Match(json, altPattern, RegexOptions.Singleline)
            If m2.Success Then
                Return m2.Groups(2).Value
            End If

        Catch ex As Exception
            LogManager.AddWarn(String.Format("ExtractFirstLtsVersion: {0}", ex.Message), "UpdateChecker")
        End Try
        Return String.Empty
    End Function

    ''' <summary>
    ''' Ottiene la versione di Node-RED installata tramite DependencyChecker.
    ''' </summary>
    Private Async Function GetInstalledNodeRedVersionAsync() As Task(Of String)
        Try
            Return Await Task.Run(Function()
                Return DependencyChecker.GetNodeRedVersion()
            End Function)
        Catch ex As Exception
            Return String.Format("Errore: {0}", ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Ottiene la versione di Node.js installata tramite DependencyChecker.
    ''' </summary>
    Private Async Function GetInstalledNodeJsVersionAsync() As Task(Of String)
        Try
            Return Await Task.Run(Function()
                Return DependencyChecker.GetNodeVersion()
            End Function)
        Catch ex As Exception
            Return String.Format("Errore: {0}", ex.Message)
        End Try
    End Function

#End Region

#Region "Utilita HTTP e Integrita"

    Private Const DesktopRepo As String = "brn78/Node-Red-Desktop"
    Private Const DesktopReleasesUrl As String = "https://github.com/" & DesktopRepo & "/releases"
    Private Const DesktopLatestReleaseApi As String = "https://api.github.com/repos/" & DesktopRepo & "/releases/latest"
    ''' <summary>Nome dell'asset installer come prodotto da installer\NodeRedDesktop.iss.</summary>
    Public Const InstallerAssetName As String = "Node-RED-Desktop-Setup.exe"

    ''' <summary>Dettaglio dell'ultimo errore di DownloadAndInstallAppUpdateAsync (per l'interfaccia).</summary>
    Public Property LastUpdateError As String = String.Empty

    Private ReadOnly _httpLock As New Object()
    Private _http As HttpClient

    ''' <summary>HttpClient condiviso (TLS 1.2+, User-Agent richiesto dall'API GitHub).</summary>
    Private ReadOnly Property SharedHttp As HttpClient
        Get
            SyncLock _httpLock
                If _http Is Nothing Then
                    ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol Or SecurityProtocolType.Tls12
                    _http = New HttpClient()
                    _http.Timeout = TimeSpan.FromMinutes(10)
                    _http.DefaultRequestHeaders.UserAgent.ParseAdd("NodeRedDesktop/" & GetCurrentAppVersion())
                    _http.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json")
                End If
                Return _http
            End SyncLock
        End Get
    End Property

    ''' <summary>Versione dell'applicazione corrente (da AssemblyInfo), formato MAJOR.MINOR.PATCH.</summary>
    Public Function GetCurrentAppVersion() As String
        Try
            Dim v As Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
            Return String.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build)
        Catch
            Return "1.0.0"
        End Try
    End Function

    Private Function IsHttpsUrl(url As String) As Boolean
        Dim uri As Uri = Nothing
        Return Not String.IsNullOrWhiteSpace(url) AndAlso Uri.TryCreate(url, UriKind.Absolute, uri) AndAlso uri.Scheme = Uri.UriSchemeHttps
    End Function

    Private Function JsonValue(dict As Dictionary(Of String, Object), key As String) As Object
        Dim value As Object = Nothing
        If dict IsNot Nothing AndAlso dict.TryGetValue(key, value) Then Return value
        Return Nothing
    End Function

    Private Function JsonString(dict As Dictionary(Of String, Object), key As String) As String
        Dim value As Object = JsonValue(dict, key)
        If value Is Nothing Then Return Nothing
        Return value.ToString()
    End Function

    ''' <summary>
    ''' Scarica un file su disco riportando l'avanzamento tramite callback (percentuale, testo).
    ''' </summary>
    Private Async Function DownloadFileWithProgressAsync(url As String, destination As String, progressCallback As Action(Of Integer, String)) As Task
        Using response As HttpResponseMessage = Await SharedHttp.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(False)
            response.EnsureSuccessStatusCode()
            Dim total As Long = If(response.Content.Headers.ContentLength.HasValue, response.Content.Headers.ContentLength.Value, -1L)

            Using source As IO.Stream = Await response.Content.ReadAsStreamAsync().ConfigureAwait(False)
                Using target As New IO.FileStream(destination, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None, 81920, True)
                    Dim buffer(81919) As Byte
                    Dim received As Long = 0
                    Dim lastPct As Integer = -1
                    Dim lastReport As DateTime = DateTime.MinValue
                    While True
                        Dim read As Integer = Await source.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(False)
                        If read <= 0 Then Exit While
                        Await target.WriteAsync(buffer, 0, read).ConfigureAwait(False)
                        received += read

                        Dim pct As Integer = If(total > 0, CInt(received * 100L \ total), 0)
                        If pct <> lastPct OrElse (DateTime.UtcNow - lastReport).TotalMilliseconds > 500 Then
                            lastPct = pct
                            lastReport = DateTime.UtcNow
                            Dim mbReceived As Double = received / 1048576.0
                            Dim text As String = If(total > 0,
                                String.Format("Download: {0}% ({1:F1} / {2:F1} MB)", pct, mbReceived, total / 1048576.0),
                                String.Format("Download: {0:F1} MB scaricati", mbReceived))
                            progressCallback?.Invoke(pct, text)
                        End If
                    End While
                End Using
            End Using
        End Using
    End Function

    ''' <summary>Calcola l'hash SHA-256 di un file (esadecimale minuscolo).</summary>
    Public Function ComputeSha256(filePath As String) As String
        Using sha As SHA256 = SHA256.Create()
            Using fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                Dim hash As Byte() = sha.ComputeHash(fs)
                Dim sb As New StringBuilder(hash.Length * 2)
                For Each b As Byte In hash
                    sb.Append(b.ToString("x2"))
                Next
                Return sb.ToString()
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Estrae da un file di hash (formato "HASH  nomefile" stile sha256sum, oppure solo l'hash)
    ''' l'hash relativo al file indicato; se il file contiene un solo hash lo restituisce comunque.
    ''' </summary>
    Public Function ExtractSha256ForFile(hashFileContent As String, fileName As String) As String
        If String.IsNullOrWhiteSpace(hashFileContent) Then Return String.Empty
        Dim matches As MatchCollection = Regex.Matches(hashFileContent, "(?im)^\s*\*?([A-Fa-f0-9]{64})\b\s*\*?(.*)$")
        Dim onlyHash As String = String.Empty
        For Each m As Match In matches
            Dim h As String = m.Groups(1).Value.ToLowerInvariant()
            Dim n As String = m.Groups(2).Value.Trim()
            If n.EndsWith(fileName, StringComparison.OrdinalIgnoreCase) Then Return h
            If matches.Count = 1 Then onlyHash = h
        Next
        Return onlyHash
    End Function

    ''' <summary>
    ''' Verifica che l'eseguibile abbia una firma Authenticode la cui catena di certificati
    ''' sia valida e attendibile per il sistema. Non verifica il contenuto del file rispetto
    ''' alla firma a livello WinVerifyTrust, ma rifiuta file non firmati o firmati da catene non attendibili.
    ''' </summary>
    Public Function HasValidAuthenticodeSignature(filePath As String, ByRef signerName As String) As Boolean
        signerName = Nothing
        Try
            Dim cert As New X509Certificate2(X509Certificate.CreateFromSignedFile(filePath))
            Using chain As New X509Chain()
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag
                If Not chain.Build(cert) Then Return False
            End Using
            signerName = cert.GetNameInfo(X509NameType.SimpleName, False)
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Sub SafeDelete(filePath As String)
        Try
            If IO.File.Exists(filePath) Then IO.File.Delete(filePath)
        Catch
        End Try
    End Sub

#End Region

End Module

End Namespace
