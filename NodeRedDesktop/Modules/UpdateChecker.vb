Imports System
Imports System.Net
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
    ''' Effettua una richiesta HTTP GET con timeout specificato in modo asincrono.
    ''' </summary>
    ''' <param name="url">URL da interrogare.</param>
    ''' <param name="timeoutMs">Timeout in millisecondi.</param>
    ''' <returns>Contenuto testuale della risposta.</returns>
    Private Async Function FetchWithTimeoutAsync(url As String, timeoutMs As Integer) As Task(Of String)
        Return Await Task.Run(Function()
            Using client As New WebClient()
                client.Headers.Add("User-Agent", "NodeRedDesktop/1.0")
                Dim downloadTask As Task(Of String) = Task.Run(Function() client.DownloadString(url))
                If downloadTask.Wait(timeoutMs) Then
                    Return downloadTask.Result
                Else
                    Throw New TimeoutException(String.Format("Timeout ({0}ms) raggiunto per {1}", timeoutMs, url))
                End If
            End Using
        End Function)
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

End Module

End Namespace
