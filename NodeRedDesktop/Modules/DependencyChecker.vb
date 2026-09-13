Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports System.Threading.Tasks

Namespace Modules

    ''' <summary>
    ''' Verifica la presenza e la versione delle dipendenze richieste da Node-RED Desktop:
    ''' Node.js, npm, node-red. Supporta installazione/aggiornamento tramite npm.
    ''' </summary>
    Public Module DependencyChecker

#Region "Tipi"

        ''' <summary>
        ''' Rappresenta lo stato di installazione di una dipendenza software.
        ''' </summary>
        Public Structure DependencyStatus
            ''' <summary>Nome della dipendenza (es. "nodejs", "npm", "nodered").</summary>
            Public Name As String

            ''' <summary>True se la dipendenza e installata e raggiungibile.</summary>
            Public IsInstalled As Boolean

            ''' <summary>Versione rilevata (es. "20.11.0").</summary>
            Public Version As String

            ''' <summary>Percorso assoluto dell eseguibile trovato.</summary>
            Public Path As String

            ''' <summary>Messaggio di errore in caso di mancata rilevazione.</summary>
            Public ErrorMessage As String

            Public Overrides Function ToString() As String
                If IsInstalled Then
                    Return $"{Name} v{Version} ({Path})"
                Else
                    Return $"{Name} -- NON installato. {ErrorMessage}"
                End If
            End Function
        End Structure

#End Region

#Region "Verifica Dipendenze"

        ''' <summary>
        ''' Esegue la verifica di tutte le dipendenze necessarie e restituisce un dizionario
        ''' indicizzato per nome (nodejs, npm, nodered).
        ''' </summary>
        ''' <returns>Dizionario con lo stato di ogni dipendenza.</returns>
        Public Function CheckAll() As Dictionary(Of String, DependencyStatus)
            Dim results As New Dictionary(Of String, DependencyStatus)(StringComparer.OrdinalIgnoreCase)

            ' ── Node.js ──────────────────────────────────────────────────────────
            Dim nodeStatus As New DependencyStatus With {.Name = "nodejs"}
            Try
                Dim nodePath = FindExecutable("node")
                If String.IsNullOrWhiteSpace(nodePath) Then nodePath = "node"

                Dim result = RunCommand(nodePath, "--version")
                If result.ExitCode = 0 AndAlso Not String.IsNullOrWhiteSpace(result.Output) Then
                    nodeStatus.IsInstalled = True
                    nodeStatus.Version = result.Output.Trim().TrimStart("v"c)
                    nodeStatus.Path = nodePath
                Else
                    nodeStatus.IsInstalled = False
                    nodeStatus.ErrorMessage = If(Not String.IsNullOrWhiteSpace(result.Error),
                                                 result.Error.Trim(),
                                                 "Impossibile ottenere la versione di node.")
                End If
            Catch ex As Exception
                nodeStatus.IsInstalled = False
                nodeStatus.ErrorMessage = ex.Message
            End Try
            results("nodejs") = nodeStatus

            ' ── npm ──────────────────────────────────────────────────────────────
            Dim npmStatus As New DependencyStatus With {.Name = "npm"}
            Try
                Dim npmPath = FindExecutable("npm")
                If String.IsNullOrWhiteSpace(npmPath) Then npmPath = "npm"

                Dim result = RunCommand(npmPath, "--version")
                If result.ExitCode = 0 AndAlso Not String.IsNullOrWhiteSpace(result.Output) Then
                    npmStatus.IsInstalled = True
                    npmStatus.Version = result.Output.Trim()
                    npmStatus.Path = npmPath
                Else
                    npmStatus.IsInstalled = False
                    npmStatus.ErrorMessage = If(Not String.IsNullOrWhiteSpace(result.Error),
                                                result.Error.Trim(),
                                                "Impossibile ottenere la versione di npm.")
                End If
            Catch ex As Exception
                npmStatus.IsInstalled = False
                npmStatus.ErrorMessage = ex.Message
            End Try
            results("npm") = npmStatus

            ' ── Node-RED ─────────────────────────────────────────────────────────
            Dim nrStatus As New DependencyStatus With {.Name = "nodered"}
            Try
                Dim cfg = AppSettings.Current
                Dim nrPath As String = String.Empty

                If Not String.IsNullOrWhiteSpace(cfg.NodeRedCmdPath) AndAlso
                   File.Exists(cfg.NodeRedCmdPath) Then
                    nrPath = cfg.NodeRedCmdPath
                Else
                    nrPath = FindExecutable("node-red")
                End If

                If String.IsNullOrWhiteSpace(nrPath) Then nrPath = "node-red"

                Dim result = RunCommand(nrPath, "--version")
                If result.ExitCode = 0 AndAlso Not String.IsNullOrWhiteSpace(result.Output) Then
                    nrStatus.IsInstalled = True
                    nrStatus.Path = nrPath

                    ' Parsing: cerca "Node-RED vX.Y.Z" nella prima riga non vuota
                    Dim lines = result.Output.Split({vbCrLf, vbLf, vbCr},
                                                    StringSplitOptions.RemoveEmptyEntries)
                    Dim firstLine = If(lines.Length > 0, lines(0).Trim(), result.Output.Trim())
                    Dim vIdx = firstLine.IndexOf("v", StringComparison.OrdinalIgnoreCase)
                    If vIdx >= 0 Then
                        nrStatus.Version = firstLine.Substring(vIdx + 1).Trim()
                    Else
                        nrStatus.Version = firstLine
                    End If
                Else
                    nrStatus.IsInstalled = False
                    nrStatus.ErrorMessage = If(Not String.IsNullOrWhiteSpace(result.Error),
                                               result.Error.Trim(),
                                               "node-red non trovato o non installato.")
                End If
            Catch ex As Exception
                nrStatus.IsInstalled = False
                nrStatus.ErrorMessage = ex.Message
            End Try
            results("nodered") = nrStatus

            ' ── Ollama ───────────────────────────────────────────────────────────
            Dim olStatus As New DependencyStatus With {.Name = "ollama"}
            Try
                Dim olPath = OllamaManager.Instance.FindOllamaExe()
                If String.IsNullOrWhiteSpace(olPath) Then olPath = "ollama"

                Dim result = RunCommand(olPath, "--version")
                If result.ExitCode = 0 AndAlso Not String.IsNullOrWhiteSpace(result.Output) Then
                    olStatus.IsInstalled = True
                    olStatus.Path = olPath
                    Dim m = System.Text.RegularExpressions.Regex.Match(result.Output, "(\d+\.\d+\.\d+)")
                    olStatus.Version = If(m.Success, m.Groups(1).Value, result.Output.Trim())
                Else
                    olStatus.IsInstalled = False
                    olStatus.ErrorMessage = If(Not String.IsNullOrWhiteSpace(result.Error), result.Error.Trim(), "ollama non trovato o non installato.")
                End If
            Catch ex As Exception
                olStatus.IsInstalled = False
                olStatus.ErrorMessage = ex.Message
            End Try
            results("ollama") = olStatus

            ' Log riepilogativo
            For Each kv In results
                If kv.Value.IsInstalled Then
                    LogManager.AddSuccess($"[DependencyChecker] {kv.Value}", "DependencyChecker")
                Else
                    LogManager.AddWarn($"[DependencyChecker] {kv.Value}", "DependencyChecker")
                End If
            Next

            Return results
        End Function

#End Region

#Region "Esecuzione Comandi"

        ''' <summary>
        ''' Esegue un processo esterno e cattura stdout/stderr.
        ''' Timeout massimo: 15 secondi.
        ''' </summary>
        ''' <param name="exe">Percorso o nome dell eseguibile.</param>
        ''' <param name="arguments">Argomenti da passare al processo.</param>
        ''' <returns>Tupla con ExitCode, Output (stdout) ed Error (stderr).</returns>
        Public Function RunCommand(exe As String, arguments As String) _
            As (ExitCode As Integer, Output As String, [Error] As String)

            Try
                Dim psi As New ProcessStartInfo()

                ' Usa cmd.exe /c per file .cmd/.bat
                If exe.EndsWith(".cmd", StringComparison.OrdinalIgnoreCase) OrElse
                   exe.EndsWith(".bat", StringComparison.OrdinalIgnoreCase) Then
                    psi.FileName = "cmd.exe"
                    psi.Arguments = $"/c ""{exe}"" {arguments}"
                Else
                    psi.FileName = exe
                    psi.Arguments = arguments
                End If

                psi.UseShellExecute = False
                psi.CreateNoWindow = True
                psi.RedirectStandardOutput = True
                psi.RedirectStandardError = True

                Using proc = Process.Start(psi)
                    If proc Is Nothing Then
                        Return (-1, String.Empty, "Impossibile avviare il processo.")
                    End If

                    ' Lettura asincrona per evitare deadlock su buffer pieni
                    Dim outTask = proc.StandardOutput.ReadToEndAsync()
                    Dim errTask = proc.StandardError.ReadToEndAsync()

                    Dim finished = proc.WaitForExit(15000)

                    Dim stdout = outTask.Result
                    Dim stderr = errTask.Result

                    If Not finished Then
                        Try
                            proc.Kill()
                        Catch
                        End Try
                        Return (-1, stdout, $"Timeout (15s). {stderr}")
                    End If

                    Return (proc.ExitCode, stdout, stderr)
                End Using

            Catch ex As Exception
                LogManager.AddError($"[DependencyChecker] RunCommand '{exe}': {ex.Message}", "DependencyChecker")
                Return (-1, String.Empty, ex.Message)
            End Try
        End Function

        ''' <summary>
        ''' Cerca un eseguibile nei percorsi comuni di installazione Node.js su Windows.
        ''' Ordine: AppSettings -> PATH (WHERE) -> WinGet -> npm global -> Chocolatey -> nvm -> Scoop.
        ''' </summary>
        ''' <param name="name">Nome dell eseguibile senza estensione (es. "node", "npm", "node-red").</param>
        ''' <returns>Percorso assoluto trovato o stringa vuota.</returns>
        Public Function FindExecutable(name As String) As String
            Dim candidates As New List(Of String)
            Dim cfg = AppSettings.Current

            ' ── 1. Percorsi configurati in AppSettings ────────────────────────
            Select Case name.ToLower()
                Case "node"
                    If Not String.IsNullOrWhiteSpace(cfg.NodeExePath) Then
                        candidates.Add(cfg.NodeExePath)
                    End If
                Case "npm"
                    If Not String.IsNullOrWhiteSpace(cfg.NpmExePath) Then
                        candidates.Add(cfg.NpmExePath)
                    End If
                Case "node-red", "node-red.cmd"
                    If Not String.IsNullOrWhiteSpace(cfg.NodeRedCmdPath) Then
                        candidates.Add(cfg.NodeRedCmdPath)
                    End If
                Case "ollama"
                    If Not String.IsNullOrWhiteSpace(cfg.OllamaExePath) Then
                        candidates.Add(cfg.OllamaExePath)
                    End If
                    candidates.Add(OllamaManager.Instance.FindOllamaExe())
            End Select

            ' ── 2. PATH di sistema tramite WHERE ─────────────────────────────
            Try
                Dim whereResult = RunCommand("where", name)
                If whereResult.ExitCode = 0 AndAlso Not String.IsNullOrWhiteSpace(whereResult.Output) Then
                    For Each line In whereResult.Output.Split({vbCrLf, vbLf, vbCr},
                                                              StringSplitOptions.RemoveEmptyEntries)
                        Dim p = line.Trim()
                        If Not String.IsNullOrWhiteSpace(p) Then candidates.Add(p)
                    Next
                End If
            Catch
            End Try

            Dim appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            Dim localApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            Dim programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            Dim programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)

            ' ── 3. WinGet packages ────────────────────────────────────────────
            Dim wingetBase = Path.Combine(localApp, "Microsoft", "WinGet", "Packages")
            If Directory.Exists(wingetBase) Then
                Try
                    For Each pkgDir In Directory.GetDirectories(wingetBase, "OpenJS.NodeJS*")
                        For Each subDir In Directory.GetDirectories(pkgDir)
                            candidates.Add(Path.Combine(subDir, $"{name}.exe"))
                            candidates.Add(Path.Combine(subDir, $"{name}.cmd"))
                        Next
                    Next
                Catch
                End Try
            End If

            ' ── 4. npm globale (%APPDATA%\npm) ───────────────────────────────
            Dim npmDir = Path.Combine(appData, "npm")
            candidates.Add(Path.Combine(npmDir, $"{name}.cmd"))
            candidates.Add(Path.Combine(npmDir, $"{name}.exe"))
            candidates.Add(Path.Combine(npmDir, name))

            ' ── 5. Chocolatey ────────────────────────────────────────────────
            candidates.Add(Path.Combine("C:\ProgramData\chocolatey\bin", $"{name}.exe"))
            candidates.Add(Path.Combine("C:\ProgramData\chocolatey\bin", $"{name}.cmd"))

            ' ── 6. nvm (%APPDATA%\nvm) ───────────────────────────────────────
            Dim nvmBase = Path.Combine(appData, "nvm")
            If Directory.Exists(nvmBase) Then
                Try
                    For Each vDir In Directory.GetDirectories(nvmBase)
                        candidates.Add(Path.Combine(vDir, $"{name}.exe"))
                        candidates.Add(Path.Combine(vDir, $"{name}.cmd"))
                    Next
                Catch
                End Try
            End If

            ' ── 7. Scoop ─────────────────────────────────────────────────────
            Dim scoopShims = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                          "scoop", "shims")
            candidates.Add(Path.Combine(scoopShims, $"{name}.exe"))
            candidates.Add(Path.Combine(scoopShims, $"{name}.cmd"))

            ' ── 8. Program Files classici ────────────────────────────────────
            For Each pfBase In New String() {programFiles, programFilesX86}
                candidates.Add(Path.Combine(pfBase, "nodejs", $"{name}.exe"))
                candidates.Add(Path.Combine(pfBase, "nodejs", $"{name}.cmd"))
            Next

            ' Ritorna il primo percorso valido trovato
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

#End Region

#Region "Installazione / Aggiornamento"

        ''' <summary>
        ''' Esegue un processo generico con streaming riga per riga di stdout e stderr su progressCallback.
        ''' </summary>
        Public Async Function RunProcessStreamingAsync(exe As String, arguments As String, progressCallback As Action(Of String)) As Task(Of Boolean)
            Return Await Task.Run(
                Async Function() As Task(Of Boolean)
                    Try
                        Dim psi As New ProcessStartInfo()
                        psi.FileName = exe
                        psi.Arguments = arguments
                        psi.UseShellExecute = False
                        psi.CreateNoWindow = True
                        psi.RedirectStandardOutput = True
                        psi.RedirectStandardError = True

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
                            Return (proc.ExitCode = 0)
                        End Using
                    Catch ex As Exception
                        progressCallback?.Invoke("[Process] Eccezione: " & ex.Message)
                        Return False
                    End Try
                End Function)
        End Function

        ''' <summary>
        ''' Installa Node.js LTS tramite WinGet o download diretto MSI in modo asincrono.
        ''' </summary>
        Public Async Function InstallNodeJsAsync(progressCallback As Action(Of String)) As Task(Of Boolean)
            progressCallback?.Invoke("[Node.js] Avvio installazione Node.js LTS...")
            LogManager.AddInfo("[DependencyChecker] Avvio installazione Node.js LTS...", "DependencyChecker")

            ' Prova 1: WinGet
            Dim wingetPath = FindExecutable("winget")
            If Not String.IsNullOrEmpty(wingetPath) Then
                progressCallback?.Invoke("[Node.js] Installazione tramite WinGet (OpenJS.NodeJS.LTS)...")
                Dim okWinget = Await RunProcessStreamingAsync("winget", "install -e --id OpenJS.NodeJS.LTS --accept-source-agreements --accept-package-agreements --silent", progressCallback)
                If okWinget Then
                    progressCallback?.Invoke("[Node.js] Installazione WinGet completata con successo!")
                    AppSettings.AutoDetectPaths()
                    Return True
                End If
                progressCallback?.Invoke("[Node.js] WinGet non riuscito, provo con download diretto...")
            End If

            ' Prova 2: Download MSI ufficiale
            Try
                Dim tempMsi = Path.Combine(Path.GetTempPath(), "node-v20-x64.msi")
                progressCallback?.Invoke("[Node.js] Download installer MSI ufficiale in corso...")
                Using client As New System.Net.WebClient()
                    Await client.DownloadFileTaskAsync(New Uri("https://nodejs.org/dist/v20.18.0/node-v20.18.0-x64.msi"), tempMsi)
                End Using

                progressCallback?.Invoke("[Node.js] Esecuzione installer MSI silenzioso...")
                Dim okMsi = Await RunProcessStreamingAsync("msiexec.exe", "/i """ & tempMsi & """ /quiet /qn /norestart", progressCallback)
                AppSettings.AutoDetectPaths()
                Return okMsi
            Catch ex As Exception
                progressCallback?.Invoke("[Node.js] Errore installazione: " & ex.Message)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Installa Ollama tramite WinGet o download installer ufficiale OllamaSetup.exe.
        ''' </summary>
        Public Async Function InstallOllamaAsync(progressCallback As Action(Of String)) As Task(Of Boolean)
            progressCallback?.Invoke("[Ollama] Avvio installazione server Ollama...")
            LogManager.AddInfo("[DependencyChecker] Avvio installazione Ollama...", "DependencyChecker")

            ' Prova 1: WinGet
            Dim wingetPath = FindExecutable("winget")
            If Not String.IsNullOrEmpty(wingetPath) Then
                progressCallback?.Invoke("[Ollama] Installazione tramite WinGet (Ollama.Ollama)...")
                Dim okWinget = Await RunProcessStreamingAsync("winget", "install -e --id Ollama.Ollama --accept-source-agreements --accept-package-agreements --silent", progressCallback)
                If okWinget Then
                    progressCallback?.Invoke("[Ollama] Installazione WinGet completata!")
                    AppSettings.AutoDetectPaths()
                    Return True
                End If
                progressCallback?.Invoke("[Ollama] WinGet non riuscito, avvio download OllamaSetup.exe...")
            End If

            ' Prova 2: Download ufficiale OllamaSetup.exe
            Try
                Dim tempExe = Path.Combine(Path.GetTempPath(), "OllamaSetup.exe")
                progressCallback?.Invoke("[Ollama] Download OllamaSetup.exe da https://ollama.com/download/OllamaSetup.exe ...")
                Using client As New System.Net.WebClient()
                    Await client.DownloadFileTaskAsync(New Uri("https://ollama.com/download/OllamaSetup.exe"), tempExe)
                End Using

                progressCallback?.Invoke("[Ollama] Esecuzione installer Ollama...")
                Dim okExe = Await RunProcessStreamingAsync(tempExe, "/silent", progressCallback)
                AppSettings.AutoDetectPaths()
                Return okExe
            Catch ex As Exception
                progressCallback?.Invoke("[Ollama] Errore installazione Ollama: " & ex.Message)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Installa node-red globalmente tramite npm in modo asincrono (node-red@latest).
        ''' </summary>
        Public Async Function InstallNodeRedAsync(progressCallback As Action(Of String)) As Task(Of Boolean)
            progressCallback?.Invoke("[Node-RED] Installazione release più recente (node-red@latest)...")
            Dim ok = Await RunNpmGlobalCommandAsync("install", "node-red@latest", "--unsafe-perm", progressCallback)
            If ok Then
                AppSettings.AutoDetectPaths()
                Dim ver = GetNodeRedVersion()
                AppSettings.Current.LastKnownNodeRedVersion = ver
                AppSettings.Save()
            End If
            Return ok
        End Function

        ''' <summary>
        ''' Aggiorna node-red globalmente all'ultima versione stabile, arrestando prima Node-RED
        ''' per liberare i lock sui file nativi (evitando l'errore EPERM) e riavviandolo al termine.
        ''' </summary>
        Public Async Function UpdateNodeRedAsync(progressCallback As Action(Of String)) As Task(Of Boolean)
            Dim wasRunning As Boolean = False
            Try
                ' 1. Verifica se Node-RED è in esecuzione e fermalo
                If NodeManager.Instance.IsRunning Then
                    wasRunning = True
                    progressCallback?.Invoke("[Aggiornamento] Rilevato Node-RED in esecuzione. Arresto temporaneo per sbloccare i file di sistema...")
                    LogManager.AddInfo("[Update] Arresto Node-RED prima dell'aggiornamento...", "DependencyChecker")
                    NodeManager.Instance.StopProcessInternal()
                    Await Task.Delay(2000)
                End If

                ' 2. Esegui npm install -g --unsafe-perm node-red@latest
                progressCallback?.Invoke("[Aggiornamento] Download e installazione dell'ultima versione di Node-RED...")
                Dim ok = Await RunNpmGlobalCommandAsync("install", "node-red@latest", "--unsafe-perm", progressCallback)

                If ok Then
                    AppSettings.AutoDetectPaths()
                    Dim newVer = GetNodeRedVersion()
                    AppSettings.Current.LastKnownNodeRedVersion = newVer
                    AppSettings.Save()
                    progressCallback?.Invoke("[Aggiornamento] Node-RED aggiornato con successo alla versione: v" & newVer)
                    LogManager.AddSuccess("[Update] Node-RED aggiornato con successo a v" & newVer, "DependencyChecker")

                    ' 3. Se era attivo, riavvialo
                    If wasRunning Then
                        progressCallback?.Invoke("[Aggiornamento] Riavvio automatico di Node-RED in corso...")
                        Await NodeManager.Instance.StartAsync()
                    End If
                    Return True
                Else
                    progressCallback?.Invoke("[Aggiornamento] ERRORE durante l'aggiornamento.")
                    If wasRunning Then
                        progressCallback?.Invoke("[Aggiornamento] Riavvio del processo precedente...")
                        Await NodeManager.Instance.StartAsync()
                    End If
                    Return False
                End If

            Catch ex As Exception
                progressCallback?.Invoke("[Aggiornamento] Eccezione: " & ex.Message)
                LogManager.AddError("[Update] UpdateNodeRedAsync: " & ex.Message, "DependencyChecker")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Procedura guidata automatica per configurare da zero un NUOVO PC:
        ''' 1) Node.js e npm -> 2) Node-RED -> 3) Ollama -> 4) Ottimizzazioni Windows -> 5) Modello chat-light.
        ''' </summary>
        Public Async Function SetupCompleteSuiteAsync(progressCallback As Action(Of String)) As Task(Of Boolean)
            Try
                progressCallback?.Invoke("=================================================================")
                progressCallback?.Invoke("   SETUP GUIDATO COMPLETO SUITE (NODE-RED + OLLAMA LOCAL AI)    ")
                progressCallback?.Invoke("=================================================================")
                Await Task.Delay(500)

                Dim deps = CheckAll()

                ' PASSO 1: Node.js & npm
                progressCallback?.Invoke(vbCrLf & ">> PASSO 1/5: Verifica Node.js & npm...")
                If Not deps("nodejs").IsInstalled OrElse Not deps("npm").IsInstalled Then
                    progressCallback?.Invoke(">> Node.js mancante. Avvio installazione automatica...")
                    Dim okNode = Await InstallNodeJsAsync(progressCallback)
                    If Not okNode Then
                        progressCallback?.Invoke(">> ERRORE: Impossibile installare Node.js automaticamente.")
                        Return False
                    End If
                Else
                    progressCallback?.Invoke(">> Node.js v" & deps("nodejs").Version & " e npm v" & deps("npm").Version & " già presenti!")
                End If

                ' PASSO 2: Node-RED
                progressCallback?.Invoke(vbCrLf & ">> PASSO 2/5: Verifica / Installazione Node-RED...")
                deps = CheckAll()
                If Not deps("nodered").IsInstalled Then
                    progressCallback?.Invoke(">> Node-RED mancante. Installazione globale via npm...")
                    Dim okNr = Await InstallNodeRedAsync(progressCallback)
                    If Not okNr Then
                        progressCallback?.Invoke(">> ERRORE: Installazione Node-RED non riuscita.")
                        Return False
                    End If
                Else
                    progressCallback?.Invoke(">> Node-RED v" & deps("nodered").Version & " già installato!")
                End If

                ' PASSO 3: Ollama
                progressCallback?.Invoke(vbCrLf & ">> PASSO 3/5: Verifica / Installazione Server Ollama...")
                deps = CheckAll()
                If Not deps("ollama").IsInstalled Then
                    progressCallback?.Invoke(">> Ollama mancante. Avvio installazione...")
                    Dim okOl = Await InstallOllamaAsync(progressCallback)
                    If Not okOl Then
                        progressCallback?.Invoke(">> ERRORE: Installazione Ollama non riuscita.")
                        Return False
                    End If
                Else
                    progressCallback?.Invoke(">> Ollama v" & deps("ollama").Version & " già installato!")
                End If

                ' PASSO 4: Ottimizzazioni Windows permanenti
                progressCallback?.Invoke(vbCrLf & ">> PASSO 4/5: Applicazione Ottimizzazioni Windows Hardware (AVX2 CPU)...")
                Dim okOpt = OllamaManager.Instance.ApplyOptimizations()
                If okOpt Then
                    progressCallback?.Invoke(">> Variabili d'ambiente Windows (OLLAMA_NUM_PARALLEL, KEEP_ALIVE, FLASH_ATTENTION) impostate!")
                End If

                ' PASSO 5: Avvio Ollama e Download modello consigliato
                progressCallback?.Invoke(vbCrLf & ">> PASSO 5/5: Avvio Ollama & Preparazione Modello Veloce 'chat-light'...")
                Dim okStart = Await OllamaManager.Instance.StartAsync()
                If okStart Then
                    Dim installed = Await OllamaManager.Instance.GetInstalledModelsAsync()
                    Dim hasLight = False
                    For Each m In installed
                        If m.ToLower().Contains("chat-light") OrElse m.ToLower().Contains("qwen2.5:1.5b") Then
                            hasLight = True
                            Exit For
                        End If
                    Next

                    If Not hasLight Then
                        progressCallback?.Invoke(">> Download del modello leggero e veloce 'qwen2.5:1.5b' (consigliato)...")
                        Await OllamaManager.Instance.PullModelAsync("qwen2.5:1.5b", progressCallback)
                    Else
                        progressCallback?.Invoke(">> Modello conversazionale 'chat-light' già pronto!")
                    End If
                End If

                progressCallback?.Invoke(vbCrLf & "=================================================================")
                progressCallback?.Invoke("   COMPLETATO: LA SUITE E PRONTA ALL'USO AL 100%!               ")
                progressCallback?.Invoke("=================================================================")
                LogManager.AddSuccess("[Setup] Installazione completa suite completata con successo!", "Setup")
                Return True

            Catch ex As Exception
                progressCallback?.Invoke(">> Eccezione durante il setup: " & ex.Message)
                LogManager.AddError("[Setup] Errore: " & ex.Message, "Setup")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Esegue un comando npm globale (-g) in modo asincrono con lettura riga-per-riga dell output.
        ''' </summary>
        Private Async Function RunNpmGlobalCommandAsync(npmVerb As String,
                                                        packageName As String,
                                                        extraArgs As String,
                                                        progressCallback As Action(Of String)) As Task(Of Boolean)
            Return Await Task.Run(
                Async Function() As Task(Of Boolean)
                    Try
                        Dim npmPath = FindExecutable("npm")
                        If String.IsNullOrWhiteSpace(npmPath) Then npmPath = "npm"

                        ' Costruzione argomenti
                        Dim args As New StringBuilder()
                        args.Append(npmVerb & " -g " & packageName)
                        If Not String.IsNullOrWhiteSpace(extraArgs) Then
                            args.Append(" " & extraArgs)
                        End If

                        Dim psi As New ProcessStartInfo()

                        If npmPath.EndsWith(".cmd", StringComparison.OrdinalIgnoreCase) OrElse
                           npmPath.EndsWith(".bat", StringComparison.OrdinalIgnoreCase) Then
                            psi.FileName = "cmd.exe"
                            psi.Arguments = "/c """ & npmPath & """ " & args.ToString()
                        Else
                            psi.FileName = npmPath
                            psi.Arguments = args.ToString()
                        End If

                        psi.UseShellExecute = False
                        psi.CreateNoWindow = True
                        psi.RedirectStandardOutput = True
                        psi.RedirectStandardError = True

                        Dim logPrefix = If(npmVerb = "install", "Installazione", "Aggiornamento")
                        progressCallback?.Invoke("[npm] " & logPrefix & " " & packageName & " in corso...")
                        LogManager.AddInfo("[DependencyChecker] npm " & args.ToString(), "DependencyChecker")

                        Using proc = Process.Start(psi)
                            If proc Is Nothing Then
                                progressCallback?.Invoke("[npm] ERRORE: impossibile avviare npm.")
                                Return False
                            End If

                            ' Lettura stdout riga per riga in modo asincrono
                            Dim readOutTask = Task.Run(
                                Async Function()
                                    Dim line As String
                                    Do
                                        line = Await proc.StandardOutput.ReadLineAsync()
                                        If line Is Nothing Then Exit Do
                                        Dim captured = line
                                        progressCallback?.Invoke(captured)
                                        LogManager.Add(captured, LogLevel.INFO, "npm")
                                    Loop
                                End Function)

                            ' Lettura stderr riga per riga
                            Dim readErrTask = Task.Run(
                                Async Function()
                                    Dim line As String
                                    Do
                                        line = Await proc.StandardError.ReadLineAsync()
                                        If line Is Nothing Then Exit Do
                                        Dim captured = line
                                        progressCallback?.Invoke("[stderr] " & captured)
                                        LogManager.Add(captured, LogLevel.WARN, "npm")
                                    Loop
                                End Function)

                            Await Task.WhenAll(readOutTask, readErrTask)
                            proc.WaitForExit()

                            Dim ok = (proc.ExitCode = 0)
                            If ok Then
                                progressCallback?.Invoke("[npm] " & logPrefix & " completata con successo.")
                                LogManager.AddSuccess("npm " & npmVerb & " " & packageName & " completato.", "DependencyChecker")
                            Else
                                progressCallback?.Invoke("[npm] " & logPrefix & " fallita (ExitCode=" & proc.ExitCode.ToString() & ").")
                                LogManager.AddError("npm " & npmVerb & " " & packageName & " fallito (ExitCode=" & proc.ExitCode.ToString() & ").", "DependencyChecker")
                            End If

                            Return ok
                        End Using

                    Catch ex As Exception
                        progressCallback?.Invoke("[npm] Eccezione: " & ex.Message)
                        LogManager.AddError("[DependencyChecker] RunNpmGlobalCommandAsync: " & ex.Message, "DependencyChecker")
                        Return False
                    End Try
                End Function)
        End Function

#End Region

        ''' <summary>Restituisce la versione di Node-RED installata o stringa vuota.</summary>
        Public Function GetNodeRedVersion() As String
            Dim all = CheckAll()
            If all.ContainsKey("nodered") AndAlso all("nodered").IsInstalled Then
                Return all("nodered").Version
            End If
            Return String.Empty
        End Function

        ''' <summary>Restituisce la versione di Node.js installata o stringa vuota.</summary>
        Public Function GetNodeVersion() As String
            Dim all = CheckAll()
            If all.ContainsKey("nodejs") AndAlso all("nodejs").IsInstalled Then
                Return all("nodejs").Version
            End If
            Return String.Empty
        End Function

        ''' <summary>Restituisce la versione di Ollama installata o stringa vuota.</summary>
        Public Function GetOllamaVersion() As String
            Dim all = CheckAll()
            If all.ContainsKey("ollama") AndAlso all("ollama").IsInstalled Then
                Return all("ollama").Version
            End If
            Return String.Empty
        End Function

End Module

End Namespace