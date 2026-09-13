' =============================================================================
' Node-RED Desktop - OllamaManager.vb
' Gestione del ciclo di vita del server locale Ollama, API REST, modelli veloci
' e ottimizzazione hardware per Windows (CPU AVX2).
' Autore: Node-RED Desktop Suite
' =============================================================================
Option Strict Off
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks
Imports NodeRedDesktop

Namespace Modules

    ''' <summary>
    ''' Gestore singleton per il servizio Ollama locale, i modelli AI e le ottimizzazioni di sistema.
    ''' </summary>
    Public Class OllamaManager

#Region "Campi & Singleton"

        Private Shared _instance As OllamaManager = Nothing
        Private Shared ReadOnly _padlock As New Object()

        Private _process As Process = Nothing
        Private ReadOnly _stateLock As New Object()
        Private _syncContext As SynchronizationContext = Nothing
        Private _isRunning As Boolean = False
        Private _lastCheckTime As DateTime = DateTime.MinValue
        Private _cachedModels As New List(Of String)()

        ''' <summary>Istanza singleton del gestore Ollama.</summary>
        Public Shared ReadOnly Property Instance As OllamaManager
            Get
                If _instance Is Nothing Then
                    SyncLock _padlock
                        If _instance Is Nothing Then
                            _instance = New OllamaManager()
                        End If
                    End SyncLock
                End If
                Return _instance
            End Get
        End Property

        Private Sub New()
            _syncContext = SynchronizationContext.Current
        End Sub

#End Region

#Region "Proprietà di Stato"

        ''' <summary>True se il server Ollama è in esecuzione e risponde su HTTP.</summary>
        Public ReadOnly Property IsRunning As Boolean
            Get
                SyncLock _stateLock
                    Return _isRunning
                End SyncLock
            End Get
        End Property

        ''' <summary>PID del processo Ollama attivo (se avviato da questo gestore o rilevato).</summary>
        Public ReadOnly Property ProcessId As Integer
            Get
                SyncLock _stateLock
                    Try
                        If _process IsNot Nothing AndAlso Not _process.HasExited Then
                            Return _process.Id
                        End If
                    Catch
                    End Try
                    Return 0
                End SyncLock
            End Get
        End Property

        ''' <summary>Elenco cache dei modelli rilevati su Ollama.</summary>
        Public ReadOnly Property CachedModels As List(Of String)
            Get
                SyncLock _stateLock
                    Return New List(Of String)(_cachedModels)
                End SyncLock
            End Get
        End Property

#End Region

#Region "Eventi"

        ''' <summary>Sollevato quando Ollama si avvia con successo.</summary>
        Public Event OllamaStarted(sender As Object, e As EventArgs)

        ''' <summary>Sollevato quando Ollama viene arrestato.</summary>
        Public Event OllamaStopped(sender As Object, e As EventArgs)

        ''' <summary>Sollevato ad ogni cambio di stato operativo.</summary>
        Public Event StatusChanged(sender As Object, isRunning As Boolean, message As String)

        Private Sub RaiseOnUiThread(action As Action)
            Try
                If _syncContext IsNot Nothing Then
                    _syncContext.Post(Sub(state) action(), Nothing)
                Else
                    action()
                End If
            Catch
            End Try
        End Sub

#End Region

#Region "Rilevamento Eseguibile & Rete"

        ''' <summary>
        ''' Cerca l'eseguibile ollama.exe nei percorsi standard di Windows:
        ''' %LOCALAPPDATA%\Programs\Ollama, PATH, Program Files.
        ''' </summary>
        Public Function FindOllamaExe() As String
            Dim candidates As New List(Of String)()

            ' 1. Configurazione salvata in AppSettings
            If Not String.IsNullOrWhiteSpace(AppSettings.Current.OllamaExePath) AndAlso File.Exists(AppSettings.Current.OllamaExePath) Then
                Return AppSettings.Current.OllamaExePath
            End If

            ' 2. LocalAppData Programs (installazione Windows standard)
            Dim localApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            candidates.Add(Path.Combine(localApp, "Programs", "Ollama", "ollama.exe"))

            ' 3. PATH di sistema tramite WHERE
            Try
                Dim psi As New ProcessStartInfo("where", "ollama") With {
                    .UseShellExecute = False,
                    .CreateNoWindow = True,
                    .RedirectStandardOutput = True
                }
                Using p = Process.Start(psi)
                    Dim output = p.StandardOutput.ReadToEnd()
                    p.WaitForExit(2000)
                    If Not String.IsNullOrWhiteSpace(output) Then
                        For Each line In output.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                            Dim trimmed = line.Trim()
                            If Not String.IsNullOrEmpty(trimmed) Then candidates.Add(trimmed)
                        Next
                    End If
                End Using
            Catch
            End Try

            ' 4. Program Files
            Dim pf = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            candidates.Add(Path.Combine(pf, "Ollama", "ollama.exe"))

            For Each c In candidates
                Try
                    If Not String.IsNullOrWhiteSpace(c) AndAlso File.Exists(c) Then
                        Return c
                    End If
                Catch
                End Try
            Next

            Return String.Empty
        End Function

        ''' <summary>
        ''' Verifica non bloccante se una porta TCP locale è in ascolto (0ms).
        ''' </summary>
        Public Function IsPortListening(port As Integer) As Boolean
            Try
                Dim ipGlobal = IPGlobalProperties.GetIPGlobalProperties()
                For Each ep In ipGlobal.GetActiveTcpListeners()
                    If ep.Port = port Then Return True
                Next
            Catch
            End Try
            Return False
        End Function

        ''' <summary>
        ''' Controlla in modo asincrono se l'API REST di Ollama risponde su /api/tags.
        ''' </summary>
        Public Async Function CheckStatusAsync() As Task(Of Boolean)
            Dim port = AppSettings.Current.OllamaPort
            If port <= 0 Then port = 11434

            If Not IsPortListening(port) Then
                SyncLock _stateLock
                    _isRunning = False
                End SyncLock
                Return False
            End If

            Try
                Dim url = "http://localhost:" & port.ToString() & "/api/tags"
                Dim req = DirectCast(WebRequest.Create(url), HttpWebRequest)
                req.Method = "GET"
                req.Timeout = 1500
                req.ReadWriteTimeout = 1500

                Using resp = DirectCast(Await req.GetResponseAsync(), HttpWebResponse)
                    Dim ok = (resp.StatusCode = HttpStatusCode.OK)
                    SyncLock _stateLock
                        _isRunning = ok
                    End SyncLock
                    Return ok
                End Using
            Catch
                SyncLock _stateLock
                    _isRunning = False
                End SyncLock
                Return False
            End Try
        End Function

#End Region

#Region "Ciclo di Vita (Avvio / Arresto / Riavvio)"

        ''' <summary>
        ''' Avvia il server Ollama in background se non è già attivo.
        ''' </summary>
        Public Async Function StartAsync() As Task(Of Boolean)
            Dim isUp = Await CheckStatusAsync()
            If isUp Then
                LogManager.AddInfo("[Ollama] Il server Ollama è già attivo e pronto.", "Ollama")
                Return True
            End If

            Dim exePath = FindOllamaExe()
            If String.IsNullOrEmpty(exePath) Then
                LogManager.AddError("[Ollama] Eseguibile ollama.exe non trovato nel sistema.", "Ollama")
                RaiseOnUiThread(Sub() RaiseEvent StatusChanged(Me, False, "ollama.exe non trovato"))
                Return False
            End If

            Try
                Dim psi As New ProcessStartInfo()
                psi.FileName = exePath
                psi.Arguments = "serve"
                psi.UseShellExecute = False
                psi.CreateNoWindow = True
                psi.WindowStyle = ProcessWindowStyle.Hidden

                LogManager.AddInfo("[Ollama] Avvio server Ollama: " & exePath & " serve", "Ollama")

                SyncLock _stateLock
                    _process = Process.Start(psi)
                End SyncLock

                ' Attendi fino a 5 secondi che il server risponda su HTTP
                For i As Integer = 1 To 10
                    Await Task.Delay(500)
                    If Await CheckStatusAsync() Then
                        LogManager.AddSuccess("[Ollama] Server Ollama avviato e operativo su porta " & AppSettings.Current.OllamaPort.ToString(), "Ollama")
                        RaiseOnUiThread(Sub()
                                            RaiseEvent OllamaStarted(Me, EventArgs.Empty)
                                            RaiseEvent StatusChanged(Me, True, "Ollama Online")
                                        End Sub)
                        ' Aggiorna cache modelli
                        Await GetInstalledModelsAsync()
                        Return True
                    End If
                Next

                LogManager.AddWarn("[Ollama] Timeout avvio server Ollama (non ancora pronto).", "Ollama")
                Return False

            Catch ex As Exception
                LogManager.AddError("[Ollama] Errore durante l'avvio: " & ex.Message, "Ollama")
                RaiseOnUiThread(Sub() RaiseEvent StatusChanged(Me, False, "Errore avvio Ollama"))
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Arresta il processo Ollama in modo sicuro.
        ''' </summary>
        Public Function [Stop]() As Boolean
            Try
                SyncLock _stateLock
                    If _process IsNot Nothing AndAlso Not _process.HasExited Then
                        Try
                            _process.Kill()
                            _process.Dispose()
                        Catch
                        End Try
                        _process = Nothing
                    End If
                    _isRunning = False
                End SyncLock

                ' Termina eventuali processi 'ollama' orfani
                Dim procs = Process.GetProcessesByName("ollama")
                For Each p In procs
                    Try
                        p.Kill()
                    Catch
                    End Try
                Next

                LogManager.AddInfo("[Ollama] Server Ollama arrestato.", "Ollama")
                RaiseOnUiThread(Sub()
                                    RaiseEvent OllamaStopped(Me, EventArgs.Empty)
                                    RaiseEvent StatusChanged(Me, False, "Ollama Fermato")
                                End Sub)
                Return True

            Catch ex As Exception
                LogManager.AddError("[Ollama] Errore durante l'arresto: " & ex.Message, "Ollama")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Arresta il server Ollama in modo asincrono senza bloccare l'interfaccia.
        ''' </summary>
        Public Async Function StopAsync() As Task(Of Boolean)
            Return Await Task.Run(Function() Me.Stop())
        End Function

        ''' <summary>
        ''' Riavvia il server Ollama.
        ''' </summary>
        Public Async Function RestartAsync() As Task(Of Boolean)
            [Stop]()
            Await Task.Delay(1000)
            Return Await StartAsync()
        End Function

#End Region

#Region "Gestione Modelli"

        ''' <summary>
        ''' Interroga l'endpoint REST /api/tags per recuperare la lista dei modelli scaricati.
        ''' </summary>
        Public Async Function GetInstalledModelsAsync() As Task(Of List(Of String))
            Dim models As New List(Of String)()
            Dim port = AppSettings.Current.OllamaPort
            If port <= 0 Then port = 11434

            Try
                Dim url = "http://localhost:" & port.ToString() & "/api/tags"
                Using client As New WebClient()
                    client.Encoding = Encoding.UTF8
                    Dim json = Await client.DownloadStringTaskAsync(url)

                    ' Regex per estrarre "name":"modello:tag"
                    Dim matches = Regex.Matches(json, """name""\s*:\s*""([^""]+)""")
                    For Each m As Match In matches
                        Dim name = m.Groups(1).Value
                        If Not models.Contains(name) Then
                            models.Add(name)
                        End If
                    Next
                End Using

                SyncLock _stateLock
                    _cachedModels = New List(Of String)(models)
                End SyncLock

            Catch ex As Exception
                LogManager.AddWarn("[Ollama] Impossibile recuperare i modelli: " & ex.Message, "Ollama")
            End Try

            Return models
        End Function

        ''' <summary>
        ''' Esegue il pull di un modello Ollama tramite CLI o REST in background con progresso riga per riga.
        ''' </summary>
        Public Async Function PullModelAsync(modelName As String, progressCallback As Action(Of String)) As Task(Of Boolean)
            Dim exePath = FindOllamaExe()
            If String.IsNullOrEmpty(exePath) Then
                progressCallback?.Invoke("[Ollama] ERRORE: ollama.exe non trovato.")
                Return False
            End If

            Return Await Task.Run(
                Async Function() As Task(Of Boolean)
                    Try
                        Dim psi As New ProcessStartInfo()
                        psi.FileName = exePath
                        psi.Arguments = "pull " & modelName
                        psi.UseShellExecute = False
                        psi.CreateNoWindow = True
                        psi.RedirectStandardOutput = True
                        psi.RedirectStandardError = True

                        progressCallback?.Invoke("[Ollama] Download modello " & modelName & " in corso...")
                        LogManager.AddInfo("[Ollama] Inizio download modello " & modelName, "Ollama")

                        Using proc = Process.Start(psi)
                            If proc Is Nothing Then Return False

                            Dim outTask = Task.Run(
                                Async Function()
                                    Dim line As String
                                    Do
                                        line = Await proc.StandardOutput.ReadLineAsync()
                                        If line Is Nothing Then Exit Do
                                        progressCallback?.Invoke(line)
                                    Loop
                                End Function)

                            Dim errTask = Task.Run(
                                Async Function()
                                    Dim line As String
                                    Do
                                        line = Await proc.StandardError.ReadLineAsync()
                                        If line Is Nothing Then Exit Do
                                        progressCallback?.Invoke(line)
                                    Loop
                                End Function)

                            Await Task.WhenAll(outTask, errTask)
                            proc.WaitForExit()

                            Dim ok = (proc.ExitCode = 0)
                            If ok Then
                                progressCallback?.Invoke("[Ollama] Modello " & modelName & " scaricato e pronto con successo!")
                                LogManager.AddSuccess("[Ollama] Modello " & modelName & " completato.", "Ollama")
                                Await GetInstalledModelsAsync()
                            Else
                                progressCallback?.Invoke("[Ollama] Download fallito con codice " & proc.ExitCode.ToString())
                                LogManager.AddError("[Ollama] Download modello fallito. ExitCode=" & proc.ExitCode.ToString(), "Ollama")
                            End If
                            Return ok
                        End Using
                    Catch ex As Exception
                        progressCallback?.Invoke("[Ollama] Eccezione: " & ex.Message)
                        LogManager.AddError("[Ollama] PullModelAsync: " & ex.Message, "Ollama")
                        Return False
                    End Try
                End Function)
        End Function

#End Region

#Region "Ottimizzazioni Hardware Windows (CPU AVX2)"

        ''' <summary>
        ''' Nomi e valori consigliati delle variabili d'ambiente per massima efficienza CPU e zero overhead.
        ''' </summary>
        Public Shared ReadOnly Property RecommendedOptimizations As Dictionary(Of String, String)
            Get
                Dim dict As New Dictionary(Of String, String)()
                dict("OLLAMA_NUM_PARALLEL") = "1"
                dict("OLLAMA_KEEP_ALIVE") = "24h"
                dict("OLLAMA_FLASH_ATTENTION") = "1"
                dict("OLLAMA_MAX_LOADED_MODELS") = "1"
                Return dict
            End Get
        End Property

        ''' <summary>
        ''' Controlla se le variabili di ambiente utente Windows sono configurate con i valori ottimali.
        ''' </summary>
        Public Function CheckOptimizations() As Dictionary(Of String, Tuple(Of String, String, Boolean))
            Dim result As New Dictionary(Of String, Tuple(Of String, String, Boolean))()

            For Each kv In RecommendedOptimizations
                Dim currentVal = Environment.GetEnvironmentVariable(kv.Key, EnvironmentVariableTarget.User)
                If String.IsNullOrEmpty(currentVal) Then
                    currentVal = Environment.GetEnvironmentVariable(kv.Key, EnvironmentVariableTarget.Process)
                End If

                Dim isOk = (String.Equals(currentVal, kv.Value, StringComparison.OrdinalIgnoreCase))
                result(kv.Key) = Tuple.Create(If(currentVal, "(Non impostata)"), kv.Value, isOk)
            Next

            Return result
        End Function

        ''' <summary>
        ''' Applica in modo permanente le variabili d'ambiente per l'utente Windows corrente (1-Click).
        ''' </summary>
        Public Function ApplyOptimizations() As Boolean
            Try
                For Each kv In RecommendedOptimizations
                    Environment.SetEnvironmentVariable(kv.Key, kv.Value, EnvironmentVariableTarget.User)
                    Environment.SetEnvironmentVariable(kv.Key, kv.Value, EnvironmentVariableTarget.Process)
                    LogManager.AddSuccess("[Ollama] Variabile impostata: " & kv.Key & "=" & kv.Value, "Ollama")
                Next
                Return True
            Catch ex As Exception
                LogManager.AddError("[Ollama] Errore applicazione setting: " & ex.Message, "Ollama")
                Return False
            End Try
        End Function

#End Region

#Region "Console Benchmark Chat di Test"

        ''' <summary>
        ''' Invia un prompt di test a Ollama e restituisce la risposta con statistiche di velocità (tok/s).
        ''' </summary>
        Public Async Function TestChatAsync(model As String, prompt As String) As Task(Of Tuple(Of Boolean, String, Long, Integer, Double, String))
            If String.IsNullOrWhiteSpace(model) Then model = "chat-light"
            If String.IsNullOrWhiteSpace(prompt) Then prompt = "Ciao! Rispondi in italiano con una frase breve."

            Dim port = AppSettings.Current.OllamaPort
            If port <= 0 Then port = 11434

            Dim sw = Stopwatch.StartNew()

            Try
                Dim url = "http://localhost:" & port.ToString() & "/api/chat"
                Dim req = DirectCast(WebRequest.Create(url), HttpWebRequest)
                req.Method = "POST"
                req.ContentType = "application/json; charset=utf-8"
                req.Timeout = 45000

                ' Costruzione JSON payload
                Dim cleanPrompt = prompt.Replace("""", "\""").Replace(vbCrLf, "\n").Replace(vbLf, "\n")
                Dim jsonBody = "{""model"":""" & model & """,""messages"":[{""role"":""user"",""content"":""" & cleanPrompt & """}],""stream"":false}"
                Dim bodyBytes = Encoding.UTF8.GetBytes(jsonBody)
                req.ContentLength = bodyBytes.Length

                Using reqStream = Await req.GetRequestStreamAsync()
                    Await reqStream.WriteAsync(bodyBytes, 0, bodyBytes.Length)
                End Using

                Using resp = DirectCast(Await req.GetResponseAsync(), HttpWebResponse)
                    Using reader As New StreamReader(resp.GetResponseStream(), Encoding.UTF8)
                        Dim jsonResp = Await reader.ReadToEndAsync()
                        sw.Stop()

                        ' Estrai content da message.content
                        Dim contentMatch = Regex.Match(jsonResp, """content""\s*:\s*""((?:\\.|[^""\\])*)""")
                        Dim reply = If(contentMatch.Success, Regex.Unescape(contentMatch.Groups(1).Value), "")

                        ' Estrai token count e durata eval
                        Dim evalCountMatch = Regex.Match(jsonResp, """eval_count""\s*:\s*(\d+)")
                        Dim evalDurationMatch = Regex.Match(jsonResp, """eval_duration""\s*:\s*(\d+)")

                        Dim evalCount = 0
                        Dim evalDurationNs As Long = 0
                        Dim tokPerSec As Double = 0.0

                        If evalCountMatch.Success Then Integer.TryParse(evalCountMatch.Groups(1).Value, evalCount)
                        If evalDurationMatch.Success Then Long.TryParse(evalDurationMatch.Groups(1).Value, evalDurationNs)

                        If evalCount > 0 AndAlso evalDurationNs > 0 Then
                            tokPerSec = Math.Round(evalCount / (evalDurationNs / 1000000000.0), 1)
                        ElseIf evalCount > 0 AndAlso sw.ElapsedMilliseconds > 0 Then
                            tokPerSec = Math.Round(evalCount / (sw.ElapsedMilliseconds / 1000.0), 1)
                        End If

                        Return Tuple.Create(True, reply, sw.ElapsedMilliseconds, evalCount, tokPerSec, String.Empty)
                    End Using
                End Using

            Catch ex As Exception
                sw.Stop()
                Return Tuple.Create(False, String.Empty, sw.ElapsedMilliseconds, 0, 0.0, ex.Message)
            End Try
        End Function

#End Region

    End Class

End Namespace
