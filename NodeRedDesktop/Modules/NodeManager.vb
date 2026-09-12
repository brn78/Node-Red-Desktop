Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Net.NetworkInformation

Namespace Modules

    ''' <summary>
    ''' Gestisce il ciclo di vita del processo Node-RED: avvio, stop, watchdog e monitoraggio risorse.
    ''' Classe Singleton — usare NodeManager.Instance.
    ''' </summary>
    Public Class NodeManager

#Region "Singleton"

        Private Shared _instance As NodeManager
        Private Shared ReadOnly _lock As New Object()

        ''' <summary>Restituisce l'unica istanza del NodeManager (thread-safe lazy init).</summary>
        Public Shared ReadOnly Property Instance As NodeManager
            Get
                If _instance Is Nothing Then
                    SyncLock _lock
                        If _instance Is Nothing Then
                            _instance = New NodeManager()
                        End If
                    End SyncLock
                End If
                Return _instance
            End Get
        End Property

        ''' <summary>Costruttore privato — impedisce istanziazione esterna.</summary>
        Private Sub New()
            _syncContext = SynchronizationContext.Current
        End Sub

#End Region

#Region "Stato Privato"

        Private _process As Process
        Private _startTime As DateTime
        Private _expectedStop As Boolean
        Private _watchdogTimer As Threading.Timer
        Private _restartCount As Integer
        Private _lastTotalProcessorTime As TimeSpan = TimeSpan.Zero
        Private _lastCpuCheckTime As DateTime = DateTime.MinValue
        Private _syncContext As SynchronizationContext
        Private ReadOnly _stateLock As New Object()
        Private _lastCpuValue As Double
        Private _isWatchdogActive As Boolean

#End Region

#Region "Proprieta Pubbliche"

        ''' <summary>True se il processo Node-RED e in esecuzione.</summary>
        Public ReadOnly Property IsRunning As Boolean
            Get
                SyncLock _stateLock
                    Try
                        Return _process IsNot Nothing AndAlso
                               Not _process.HasExited
                    Catch
                        Return False
                    End Try
                End SyncLock
            End Get
        End Property

        ''' <summary>PID del processo Node-RED attivo (0 se non in esecuzione).</summary>
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

        ''' <summary>Tempo di attivita del processo dall avvio.</summary>
        Public ReadOnly Property Uptime As TimeSpan
            Get
                If IsRunning AndAlso _startTime <> DateTime.MinValue Then
                    Return DateTime.Now - _startTime
                End If
                Return TimeSpan.Zero
            End Get
        End Property

        ''' <summary>Percentuale CPU utilizzata dal processo Node-RED.</summary>
        Public ReadOnly Property CpuPercent As Double
            Get
                Return _lastCpuValue
            End Get
        End Property

        ''' <summary>Memoria RAM utilizzata in MB dal processo Node-RED.</summary>
        Public ReadOnly Property MemoryMB As Double
            Get
                SyncLock _stateLock
                    Try
                        If _process IsNot Nothing AndAlso Not _process.HasExited Then
                            _process.Refresh()
                            Return Math.Round(_process.WorkingSet64 / (1024.0 * 1024.0), 2)
                        End If
                    Catch
                    End Try
                    Return 0.0
                End SyncLock
            End Get
        End Property

        ''' <summary>Numero di riavvii automatici effettuati dal watchdog.</summary>
        Public ReadOnly Property RestartCount As Integer
            Get
                Return _restartCount
            End Get
        End Property

        ''' <summary>True se il watchdog e attualmente attivo.</summary>
        Public ReadOnly Property IsWatchdogActive As Boolean
            Get
                Return _isWatchdogActive
            End Get
        End Property

#End Region

#Region "Events"

        ''' <summary>Sollevato quando Node-RED si avvia correttamente.</summary>
        Public Event NodeStarted(sender As Object, e As EventArgs)

        ''' <summary>Sollevato quando Node-RED si ferma (wasExpected=True se fermato intenzionalmente).</summary>
        Public Event NodeStopped(sender As Object, e As EventArgs, wasExpected As Boolean)

        ''' <summary>Sollevato quando Node-RED crasha inaspettatamente.</summary>
        Public Event NodeCrashed(sender As Object, e As EventArgs)

        ''' <summary>Sollevato ad ogni riga di output (stdout o stderr).</summary>
        Public Event OutputReceived(sender As Object, text As String, isError As Boolean)

        ''' <summary>Sollevato ad ogni cambio di stato (avvio/stop).</summary>
        Public Event StatusChanged(sender As Object, isRunning As Boolean, message As String)

        ''' <summary>Solleva un evento sul thread UI tramite SynchronizationContext.</summary>
        Private Sub RaiseOnUiThread(action As Action)
            Try
                If _syncContext IsNot Nothing Then
                    _syncContext.Post(Sub(state) action(), Nothing)
                Else
                    action()
                End If
            Catch ex As Exception
                LogManager.AddError($"[NodeManager] Errore RaiseOnUiThread: {ex.Message}", "NodeManager")
            End Try
        End Sub

#End Region

#Region "Avvio / Stop"

        ''' <summary>
        ''' Verifica rapida e non bloccante (0ms) se la porta TCP specificata è in ascolto.
        ''' </summary>
        Public Function IsPortListening(port As Integer) As Boolean
            Try
                Dim ipProps = IPGlobalProperties.GetIPGlobalProperties()
                Dim listeners = ipProps.GetActiveTcpListeners()
                For Each ep In listeners
                    If ep.Port = port Then Return True
                Next
            Catch
            End Try
            Return False
        End Function

        ''' <summary>
        ''' Rileva se un processo sta ascoltando sulla porta specificata (es. 1880) e ne restituisce l'istanza Process.
        ''' </summary>
        Public Function FindProcessListeningOnPort(port As Integer) As Process
            Try
                ' Verifica ultra-rapida (0ms): se la porta non è in ascolto, ritorna subito Nothing
                If Not IsPortListening(port) Then Return Nothing

                Dim psi As New ProcessStartInfo()
                psi.FileName = "cmd.exe"
                psi.Arguments = $"/c netstat -ano -p tcp | findstr :{port} | findstr LISTENING"
                psi.RedirectStandardOutput = True
                psi.UseShellExecute = False
                psi.CreateNoWindow = True

                Using p = Process.Start(psi)
                    Dim output = p.StandardOutput.ReadToEnd()
                    p.WaitForExit(3000)

                    If Not String.IsNullOrWhiteSpace(output) Then
                        Dim lines = output.Split(New String() {Environment.NewLine, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                        For Each line In lines
                            Dim trimmed = line.Trim()
                            If trimmed.Contains($":{port}") AndAlso trimmed.Contains("LISTENING") Then
                                Dim parts = System.Text.RegularExpressions.Regex.Split(trimmed, "\s+")
                                If parts.Length >= 5 Then
                                    Dim pidStr = parts(parts.Length - 1)
                                    Dim pid As Integer
                                    If Integer.TryParse(pidStr, pid) AndAlso pid > 0 Then
                                        Try
                                            Dim proc = Process.GetProcessById(pid)
                                            Return proc
                                        Catch
                                        End Try
                                    End If
                                End If
                            End If
                        Next
                    End If
                End Using
            Catch ex As Exception
                LogManager.AddWarn($"[NodeManager] Errore verifica porta {port}: {ex.Message}", "NodeManager")
            End Try
            Return Nothing
        End Function

        ''' <summary>
        ''' Si aggancia a un processo Node-RED già attivo sul sistema.
        ''' </summary>
        Public Function AttachToExistingProcess(proc As Process) As Boolean
            If proc Is Nothing OrElse proc.HasExited Then Return False

            SyncLock _stateLock
                Try
                    _process = proc
                    _process.EnableRaisingEvents = True
                    AddHandler _process.Exited, AddressOf OnProcessExited

                    Try
                        _startTime = proc.StartTime
                    Catch
                        _startTime = DateTime.Now
                    End Try

                    _expectedStop = False
                Catch ex As Exception
                    LogManager.AddWarn($"[NodeManager] Impossibile agganciare eventi processo: {ex.Message}", "NodeManager")
                End Try
            End SyncLock

            UpdateCpuCounter()
            LogManager.AddSuccess($"Processo Node-RED già attivo rilevato e agganciato con successo (PID={proc.Id}, Nome={proc.ProcessName}).", "NodeManager")

            RaiseOnUiThread(Sub()
                                RaiseEvent NodeStarted(Me, EventArgs.Empty)
                                RaiseEvent StatusChanged(Me, True, $"Node-RED in esecuzione (PID {proc.Id})")
                            End Sub)

            ' Avvia il watchdog se configurato
            If AppSettings.Current.WatchdogEnabled Then
                StartWatchdog()
            End If

            Return True
        End Function

        ''' <summary>
        ''' Termina un processo e tutti i relativi processi figli (albero dei processi).
        ''' </summary>
        Public Sub KillProcessTree(pid As Integer)
            Try
                Dim psi As New ProcessStartInfo("taskkill", $"/PID {pid} /T /F")
                psi.CreateNoWindow = True
                psi.UseShellExecute = False
                Using p = Process.Start(psi)
                    p.WaitForExit(4000)
                End Using
            Catch
                Try
                    Dim p = Process.GetProcessById(pid)
                    p.Kill()
                Catch
                End Try
            End Try
        End Sub

        ''' <summary>
        ''' Avvia il processo Node-RED in modo asincrono su un thread di background.
        ''' Non blocca mai il thread UI.
        ''' </summary>
        Public Async Function StartAsync() As Task(Of Boolean)
            Return Await Task.Run(Function() As Boolean
                If IsRunning Then
                    LogManager.AddWarn("Node-RED e gia in esecuzione.", "NodeManager")
                    Return True
                End If

                Dim cfg = AppSettings.Current

                ' 1. Verifica rapida porta (0ms con IPGlobalProperties)
                If IsPortListening(cfg.NodeRedPort) Then
                    Dim existingProc = FindProcessListeningOnPort(cfg.NodeRedPort)
                    If existingProc IsNot Nothing Then
                        If existingProc.ProcessName.ToLower().Contains("node") Then
                            Return AttachToExistingProcess(existingProc)
                        Else
                            LogManager.AddWarn($"La porta {cfg.NodeRedPort} e occupata da un altro processo ({existingProc.ProcessName}, PID={existingProc.Id}).", "NodeManager")
                            Return False
                        End If
                    End If
                End If

                Try
                    Dim cmdPath As String = String.Empty

                    ' Usa percorso configurato o auto-rilevamento
                    If Not String.IsNullOrWhiteSpace(cfg.NodeRedCmdPath) AndAlso File.Exists(cfg.NodeRedCmdPath) Then
                        cmdPath = cfg.NodeRedCmdPath
                    Else
                        cmdPath = FindNodeRedCmd()
                    End If

                    If String.IsNullOrWhiteSpace(cmdPath) Then
                        LogManager.AddError("Impossibile trovare node-red.cmd. Configura il percorso nelle impostazioni.", "NodeManager")
                        RaiseOnUiThread(Sub()
                                            RaiseEvent StatusChanged(Me, False, "node-red.cmd non trovato")
                                        End Sub)
                        Return False
                    End If

                    ' Costruzione argomenti CLI puliti
                    Dim args As New System.Text.StringBuilder()
                    args.Append($"--port {cfg.NodeRedPort}")

                    If Not String.IsNullOrWhiteSpace(cfg.UserDir) Then
                        args.Append($" --userDir ""{cfg.UserDir}""")
                    End If

                    ' Esegui direttamente node-red.cmd senza cmd.exe /c
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = cmdPath
                    psi.Arguments = args.ToString()
                    psi.RedirectStandardOutput = True
                    psi.RedirectStandardError = True
                    psi.UseShellExecute = False
                    psi.CreateNoWindow = True
                    psi.WorkingDirectory = If(Not String.IsNullOrWhiteSpace(cfg.UserDir) AndAlso
                                              Directory.Exists(cfg.UserDir), cfg.UserDir,
                                              Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))

                    ' Imposta cartella Node.js nel PATH se configurata
                    If Not String.IsNullOrWhiteSpace(cfg.NodeExePath) Then
                        Dim nodeDir = Path.GetDirectoryName(cfg.NodeExePath)
                        If Not String.IsNullOrWhiteSpace(nodeDir) Then
                            psi.EnvironmentVariables("PATH") = nodeDir & ";" & psi.EnvironmentVariables("PATH")
                        End If
                    End If

                    Dim currentPid As Integer = 0
                    SyncLock _stateLock
                        _process = New Process()
                        _process.StartInfo = psi
                        _process.EnableRaisingEvents = True

                        AddHandler _process.OutputDataReceived, AddressOf OnOutputDataReceived
                        AddHandler _process.ErrorDataReceived, AddressOf OnErrorDataReceived
                        AddHandler _process.Exited, AddressOf OnProcessExited

                        _expectedStop = False
                        _process.Start()
                        Try
                            currentPid = _process.Id
                        Catch
                        End Try
                        _process.BeginOutputReadLine()
                        _process.BeginErrorReadLine()
                        _startTime = DateTime.Now
                    End SyncLock

                    UpdateCpuCounter()
                    LogManager.AddSuccess($"Node-RED avviato. PID={currentPid} Porta={cfg.NodeRedPort}", "NodeManager")

                    RaiseOnUiThread(Sub()
                                        SyncLock _stateLock
                                            If _process Is Nothing OrElse _process.HasExited Then Return
                                        End SyncLock
                                        RaiseEvent NodeStarted(Me, EventArgs.Empty)
                                        RaiseEvent StatusChanged(Me, True, $"Node-RED in esecuzione (PID {currentPid})")
                                    End Sub)

                    ' Avvia Watchdog se configurato
                    If AppSettings.Current.WatchdogEnabled Then
                        StartWatchdog()
                    End If

                    Return True

                Catch ex As Exception
                    LogManager.AddError($"Errore durante l avvio di Node-RED: {ex.Message}", "NodeManager")
                    RaiseOnUiThread(Sub()
                                        RaiseEvent StatusChanged(Me, False, $"Errore avvio: {ex.Message}")
                                    End Sub)
                    Return False
                End Try
            End Function)
        End Function

        ''' <summary>
        ''' Logica interna di arresto del processo Node-RED (senza deadlock, rilascio immediato del lock).
        ''' </summary>
        Public Sub StopProcessInternal()
            Dim pidToKill As Integer = 0
            Dim procToDispose As Process = Nothing

            SyncLock _stateLock
                _expectedStop = True
                If _process IsNot Nothing AndAlso Not _process.HasExited Then
                    Try
                        pidToKill = _process.Id
                    Catch
                    End Try
                    procToDispose = _process
                    _process = Nothing
                Else
                    _process = Nothing
                End If
                _lastCpuValue = 0
                _lastTotalProcessorTime = TimeSpan.Zero
                _lastCpuCheckTime = DateTime.MinValue
            End SyncLock

            ' Se c'era un processo attivo, rimuovi gli handler e terminalo FUORI dal lock
            If procToDispose IsNot Nothing Then
                Try
                    RemoveHandler procToDispose.OutputDataReceived, AddressOf OnOutputDataReceived
                    RemoveHandler procToDispose.ErrorDataReceived, AddressOf OnErrorDataReceived
                    RemoveHandler procToDispose.Exited, AddressOf OnProcessExited
                Catch
                End Try

                If pidToKill > 0 Then
                    KillProcessTree(pidToKill)
                    LogManager.AddInfo($"Node-RED (PID={pidToKill}) terminato correttamente.", "NodeManager")
                End If

                Try
                    procToDispose.Dispose()
                Catch
                End Try
            Else
                ' Nessun processo gestito attivo: controlla se c'è un processo orfano in ascolto sulla porta
                Dim cfg = AppSettings.Current
                If IsPortListening(cfg.NodeRedPort) Then
                    Dim lingering = FindProcessListeningOnPort(cfg.NodeRedPort)
                    If lingering IsNot Nothing AndAlso lingering.ProcessName.ToLower().Contains("node") Then
                        Try
                            Dim lingPid = lingering.Id
                            KillProcessTree(lingPid)
                            LogManager.AddInfo($"Processo orfano Node-RED terminato (PID={lingPid}).", "NodeManager")
                        Catch ex As Exception
                            LogManager.AddError($"Errore terminazione processo orfano: {ex.Message}", "NodeManager")
                        End Try
                    End If
                Else
                    LogManager.AddWarn("Nessun processo Node-RED attivo da fermare.", "NodeManager")
                End If
            End If

            RaiseOnUiThread(Sub()
                                RaiseEvent NodeStopped(Me, EventArgs.Empty, True)
                                RaiseEvent StatusChanged(Me, False, "Node-RED fermato")
                            End Sub)
        End Sub

        ''' <summary>
        ''' Ferma il processo Node-RED in modo asincrono, rilasciando la porta.
        ''' </summary>
        Public Async Function StopAsync() As Task(Of Boolean)
            Return Await Task.Run(Function() As Boolean
                StopProcessInternal()
                Return True
            End Function)
        End Function

        ''' <summary>
        ''' Riavvia Node-RED in modo asincrono: stop, attesa liberazione porta e nuovo avvio.
        ''' </summary>
        Public Async Function RestartAsync() As Task(Of Boolean)
            LogManager.AddInfo("Riavvio di Node-RED in corso...", "NodeManager")
            Await StopAsync()

            Dim cfg = AppSettings.Current
            ' Attendi in modo asincrono (non bloccante) che la porta si liberi
            For i As Integer = 0 To 15
                If Not IsPortListening(cfg.NodeRedPort) Then Exit For
                Await Task.Delay(300)
            Next

            Return Await StartAsync()
        End Function

        ''' <summary>Wrapper sincrono diretto per Stop (senza Task.Wait bloccante).</summary>
        Public Sub [Stop]()
            StopProcessInternal()
        End Sub

        ''' <summary>Wrapper sincrono per Restart (eseguito su task in background).</summary>
        Public Sub Restart()
            Task.Run(Async Function() Await RestartAsync())
        End Sub

        ''' <summary>Rilascia le risorse del processo terminato in sicurezza.</summary>
        Private Sub CleanupProcess()
            Dim proc As Process = Nothing
            SyncLock _stateLock
                proc = _process
                _process = Nothing
                _lastCpuValue = 0
                _lastTotalProcessorTime = TimeSpan.Zero
                _lastCpuCheckTime = DateTime.MinValue
            End SyncLock

            If proc IsNot Nothing Then
                Try
                    RemoveHandler proc.OutputDataReceived, AddressOf OnOutputDataReceived
                    RemoveHandler proc.ErrorDataReceived, AddressOf OnErrorDataReceived
                    RemoveHandler proc.Exited, AddressOf OnProcessExited
                    proc.Dispose()
                Catch ex As Exception
                End Try
            End If
        End Sub

#End Region

#Region "Watchdog"

        ''' <summary>
        ''' Avvia il timer watchdog che controlla periodicamente se Node-RED e vivo.
        ''' Intervallo definito in AppSettings.WatchdogIntervalSeconds.
        ''' </summary>
        Public Sub StartWatchdog()
            If _isWatchdogActive Then Return

            Try
                Dim intervalMs As Integer = AppSettings.Current.WatchdogIntervalSeconds * 1000
                If intervalMs < 5000 Then intervalMs = 5000 ' Minimo 5 secondi

                _watchdogTimer = New Threading.Timer(AddressOf WatchdogTick, Nothing,
                                                     intervalMs, intervalMs)
                _isWatchdogActive = True
                LogManager.AddInfo($"Watchdog avviato (intervallo: {AppSettings.Current.WatchdogIntervalSeconds}s).", "NodeManager")
            Catch ex As Exception
                LogManager.AddError($"Errore avvio watchdog: {ex.Message}", "NodeManager")
            End Try
        End Sub

        ''' <summary>Ferma il timer watchdog.</summary>
        Public Sub StopWatchdog()
            Try
                If _watchdogTimer IsNot Nothing Then
                    _watchdogTimer.Dispose()
                    _watchdogTimer = Nothing
                End If
                _isWatchdogActive = False
                LogManager.AddInfo("Watchdog fermato.", "NodeManager")
            Catch ex As Exception
                LogManager.AddError($"Errore stop watchdog: {ex.Message}", "NodeManager")
            End Try
        End Sub

        ''' <summary>
        ''' Callback del timer watchdog. Controlla lo stato del processo e riavvia se necessario,
        ''' rispettando il limite MaxRestarts configurato.
        ''' </summary>
        Private Sub WatchdogTick(state As Object)
            Task.Run(Async Function()
                Try
                    If Not AppSettings.Current.WatchdogEnabled OrElse _expectedStop Then Return

                    UpdateCpuCounter()

                    If Not IsRunning Then
                        Dim maxRestarts = AppSettings.Current.MaxRestarts

                        If maxRestarts > 0 AndAlso _restartCount >= maxRestarts Then
                            LogManager.AddError($"Watchdog: raggiunto limite massimo di riavvii ({maxRestarts}). Watchdog disattivato.", "NodeManager")
                            StopWatchdog()
                            Return
                        End If

                        _restartCount += 1
                        LogManager.AddWarn($"Watchdog: Node-RED non risponde. Riavvio #{_restartCount} in corso...", "NodeManager")

                        RaiseOnUiThread(Sub()
                                            RaiseEvent NodeCrashed(Me, EventArgs.Empty)
                                        End Sub)

                        Await Task.Delay(2000)
                        Await StartAsync()
                    End If

                Catch ex As Exception
                    LogManager.AddError($"Errore nel WatchdogTick: {ex.Message}", "NodeManager")
                End Try
            End Function)
        End Sub

#End Region

#Region "Handler Processo"

        ''' <summary>Riceve righe di stdout dal processo Node-RED.</summary>
        Private Sub OnOutputDataReceived(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Return
            LogManager.Add(e.Data, LogLevel.INFO, "Node-RED")
            RaiseOnUiThread(Sub()
                                RaiseEvent OutputReceived(Me, e.Data, False)
                            End Sub)
        End Sub

        ''' <summary>Riceve righe di stderr dal processo Node-RED.</summary>
        Private Sub OnErrorDataReceived(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Return
            Dim isError = e.Data.ToLower().Contains("error") OrElse
                          e.Data.ToLower().Contains("exception") OrElse
                          e.Data.ToLower().Contains("uncaught")

            If isError Then
                LogManager.AddError(e.Data, "Node-RED")
            Else
                LogManager.Add(e.Data, LogLevel.INFO, "Node-RED")
            End If

            RaiseOnUiThread(Sub()
                                RaiseEvent OutputReceived(Me, e.Data, isError)
                            End Sub)
        End Sub

        ''' <summary>
        ''' Gestisce l uscita del processo. Se non attesa, segnala crash.
        ''' </summary>
        Private Sub OnProcessExited(sender As Object, e As EventArgs)
            Dim wasExpected As Boolean
            Dim exitCode As Integer = 0
            Dim proc As Process = Nothing

            SyncLock _stateLock
                wasExpected = _expectedStop
                proc = _process
                _process = Nothing
                _lastCpuValue = 0
                _lastTotalProcessorTime = TimeSpan.Zero
                _lastCpuCheckTime = DateTime.MinValue
            End SyncLock

            If proc IsNot Nothing Then
                Try
                    exitCode = proc.ExitCode
                Catch
                End Try
                Try
                    RemoveHandler proc.OutputDataReceived, AddressOf OnOutputDataReceived
                    RemoveHandler proc.ErrorDataReceived, AddressOf OnErrorDataReceived
                    RemoveHandler proc.Exited, AddressOf OnProcessExited
                    proc.Dispose()
                Catch
                End Try
            End If

            If wasExpected Then
                LogManager.AddInfo($"Node-RED terminato (ExitCode={exitCode}).", "NodeManager")
            Else
                ' Se inaspettato, verifica se c'era un'altra istanza sulla porta
                Dim cfg = AppSettings.Current
                If IsPortListening(cfg.NodeRedPort) Then
                    Dim listeningProc = FindProcessListeningOnPort(cfg.NodeRedPort)
                    If listeningProc IsNot Nothing AndAlso listeningProc.ProcessName.ToLower().Contains("node") Then
                        LogManager.AddInfo($"La porta {cfg.NodeRedPort} è occupata da un'istanza attiva di Node-RED (PID={listeningProc.Id}). Aggancio...", "NodeManager")
                        AttachToExistingProcess(listeningProc)
                        Return
                    End If
                End If

                LogManager.AddError($"Node-RED terminato inaspettatamente (ExitCode={exitCode}).", "NodeManager")
            End If

            RaiseOnUiThread(Sub()
                                If wasExpected Then
                                    RaiseEvent NodeStopped(Me, EventArgs.Empty, True)
                                    RaiseEvent StatusChanged(Me, False, "Node-RED fermato")
                                Else
                                    RaiseEvent NodeCrashed(Me, EventArgs.Empty)
                                    RaiseEvent NodeStopped(Me, EventArgs.Empty, False)
                                    RaiseEvent StatusChanged(Me, False, "Node-RED crash!")
                                End If
                            End Sub)
        End Sub

#End Region

#Region "Utilita"

        ''' <summary>
        ''' Cerca node-red.cmd nei percorsi tipici di installazione:
        ''' PATH, WinGet, npm globale, nvm, Chocolatey, Scoop.
        ''' </summary>
        Public Function FindNodeRedCmd() As String
            Dim candidates As New List(Of String)

            ' Recupera PATH di sistema
            Dim pathEnv = If(Environment.GetEnvironmentVariable("PATH"), "")
            For Each pathDir As String In pathEnv.Split(";"c)
                If Not String.IsNullOrWhiteSpace(pathDir) Then
                    candidates.Add(Path.Combine(pathDir.Trim(), "node-red.cmd"))
                End If
            Next

            ' npm globale (%APPDATA%\npm)
            Dim appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            candidates.Add(Path.Combine(appData, "npm", "node-red.cmd"))

            ' WinGet packages
            Dim localApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            Dim wingetBase = Path.Combine(localApp, "Microsoft", "WinGet", "Packages")
            If Directory.Exists(wingetBase) Then
                Try
                    For Each pkgDir In Directory.GetDirectories(wingetBase, "OpenJS.NodeJS*")
                        For Each subDir In Directory.GetDirectories(pkgDir)
                            candidates.Add(Path.Combine(subDir, "node-red.cmd"))
                        Next
                    Next
                Catch
                End Try
            End If

            ' nvm (%APPDATA%\nvm)
            Dim nvmBase = Path.Combine(appData, "nvm")
            If Directory.Exists(nvmBase) Then
                Try
                    For Each vDir In Directory.GetDirectories(nvmBase)
                        candidates.Add(Path.Combine(vDir, "node-red.cmd"))
                    Next
                Catch
                End Try
            End If

            ' Chocolatey
            candidates.Add(Path.Combine("C:\ProgramData\chocolatey\bin", "node-red.cmd"))

            ' Scoop
            Dim scoopBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "scoop", "shims")
            candidates.Add(Path.Combine(scoopBase, "node-red.cmd"))

            For Each c In candidates
                Try
                    If File.Exists(c) Then
                        LogManager.AddInfo($"node-red.cmd trovato in: {c}", "NodeManager")
                        Return c
                    End If
                Catch
                End Try
            Next

            LogManager.AddWarn("node-red.cmd non trovato in nessun percorso comune.", "NodeManager")
            Return String.Empty
        End Function

        ''' <summary>
        ''' Aggiorna il valore della percentuale CPU tramite PerformanceCounter.
        ''' </summary>
        Private Sub UpdateCpuCounter()
            Try
                SyncLock _stateLock
                    If _process Is Nothing OrElse _process.HasExited Then
                        _lastCpuValue = 0
                        Return
                    End If

                    Dim now = DateTime.Now
                    Dim currentTotalTime = _process.TotalProcessorTime

                    If _lastCpuCheckTime <> DateTime.MinValue AndAlso now > _lastCpuCheckTime Then
                        Dim timePassed = (now - _lastCpuCheckTime).TotalMilliseconds
                        Dim cpuUsed = (currentTotalTime - _lastTotalProcessorTime).TotalMilliseconds
                        If timePassed > 0 Then
                            Dim usage = (cpuUsed / (timePassed * Environment.ProcessorCount)) * 100.0
                            _lastCpuValue = Math.Round(Math.Max(0.0, Math.Min(100.0, usage)), 1)
                        End If
                    End If

                    _lastTotalProcessorTime = currentTotalTime
                    _lastCpuCheckTime = now
                End SyncLock
            Catch ex As Exception
                _lastCpuValue = 0
            End Try
        End Sub

        ''' <summary>Azzera il contatore di riavvii (utile dopo un avvio manuale corretto).</summary>
        Public Sub ResetRestartCount()
            _restartCount = 0
        End Sub

#End Region

    End Class

End Namespace