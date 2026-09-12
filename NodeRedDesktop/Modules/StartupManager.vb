Imports System
Imports System.Diagnostics
Imports System.IO
Imports Microsoft.Win32

Namespace Modules

    ''' <summary>
    ''' Gestisce l autoavvio di Node-RED Desktop all avvio di Windows
    ''' tramite Registro di Sistema o Windows Task Scheduler.
    ''' </summary>
    Public Module StartupManager

#Region "Costanti"

        ''' <summary>Nome dell applicazione usato come chiave nel registro e come nome del task.</summary>
        Public Const AppName As String = "Node-RED Desktop"

        ''' <summary>Percorso della chiave di registro per l autoavvio utente corrente.</summary>
        Public Const RegistryKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"

        ''' <summary>Nome del task nel Task Scheduler di Windows.</summary>
        Public Const TaskName As String = "NodeRedDesktop"

#End Region

#Region "Registro di Sistema"

        ''' <summary>
        ''' Verifica se l autoavvio tramite Registro di Sistema (HKCU) e abilitato.
        ''' </summary>
        ''' <returns>True se la chiave esiste e punta all exe corrente.</returns>
        Public Function IsRegistryStartupEnabled() As Boolean
            Try
                Using key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryKey, False)
                    If key Is Nothing Then Return False
                    Dim value = key.GetValue(AppName)
                    Return value IsNot Nothing AndAlso
                           Not String.IsNullOrWhiteSpace(value.ToString())
                End Using
            Catch ex As Exception
                LogManager.AddError($"[StartupManager] Errore lettura registro: {ex.Message}", "StartupManager")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Aggiunge o rimuove la chiave di autoavvio nel Registro di Sistema (HKCU).
        ''' </summary>
        ''' <param name="enabled">True per abilitare, False per disabilitare.</param>
        Public Sub SetRegistryStartup(enabled As Boolean)
            Try
                Using key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryKey, True)
                    If key Is Nothing Then
                        LogManager.AddError("[StartupManager] Impossibile aprire la chiave di registro in scrittura.", "StartupManager")
                        Return
                    End If

                    If enabled Then
                        Dim exePath = GetCurrentExePath()
                        Dim value = $"""{exePath}"" /minimized"
                        key.SetValue(AppName, value, Microsoft.Win32.RegistryValueKind.String)
                        LogManager.AddSuccess($"Autoavvio registro abilitato: {value}", "StartupManager")
                    Else
                        If key.GetValue(AppName) IsNot Nothing Then
                            key.DeleteValue(AppName, False)
                            LogManager.AddInfo("Autoavvio registro rimosso.", "StartupManager")
                        End If
                    End If
                End Using
            Catch ex As Exception
                LogManager.AddError($"[StartupManager] Errore scrittura registro: {ex.Message}", "StartupManager")
                Throw
            End Try
        End Sub

#End Region

#Region "Task Scheduler"

        ''' <summary>
        ''' Verifica se il task di autoavvio e presente nel Task Scheduler di Windows.
        ''' </summary>
        ''' <returns>True se il task esiste.</returns>
        Public Function IsTaskSchedulerStartupEnabled() As Boolean
            Try
                Dim result = RunSchtasks($"/query /tn ""{TaskName}"" /fo LIST")
                Return result
            Catch ex As Exception
                LogManager.AddError($"[StartupManager] Errore query Task Scheduler: {ex.Message}", "StartupManager")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Crea o elimina il task di autoavvio nel Task Scheduler di Windows.
        ''' </summary>
        ''' <param name="enabled">True per creare il task, False per eliminarlo.</param>
        ''' <param name="runAsSystem">Se True, il task viene eseguito come SYSTEM (utile su server).</param>
        Public Sub SetTaskSchedulerStartup(enabled As Boolean, runAsSystem As Boolean)
            Try
                If enabled Then
                    Dim exePath = GetCurrentExePath()
                    Dim taskRun = $"""{exePath}"" /minimized"

                    Dim args As New System.Text.StringBuilder()
                    args.Append($"/create /tn ""{TaskName}""")
                    args.Append($" /tr ""{taskRun}""")
                    args.Append(" /sc ONLOGON")
                    args.Append(" /rl HIGHEST")
                    args.Append(" /f")

                    If runAsSystem Then
                        args.Append(" /ru SYSTEM")
                    End If

                    Dim ok = RunSchtasks(args.ToString())
                    If ok Then
                        LogManager.AddSuccess($"Task Scheduler: task '{TaskName}' creato.", "StartupManager")
                    Else
                        LogManager.AddError($"Task Scheduler: creazione task '{TaskName}' fallita.", "StartupManager")
                    End If
                Else
                    Dim ok = RunSchtasks($"/delete /tn ""{TaskName}"" /f")
                    If ok Then
                        LogManager.AddInfo($"Task Scheduler: task '{TaskName}' eliminato.", "StartupManager")
                    Else
                        LogManager.AddWarn($"Task Scheduler: eliminazione task '{TaskName}' fallita (forse non esisteva).", "StartupManager")
                    End If
                End If
            Catch ex As Exception
                LogManager.AddError($"[StartupManager] Errore Task Scheduler: {ex.Message}", "StartupManager")
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Esegue schtasks.exe con gli argomenti specificati e attende il completamento.
        ''' </summary>
        ''' <param name="arguments">Argomenti da passare a schtasks.exe.</param>
        ''' <returns>True se ExitCode = 0.</returns>
        Public Function RunSchtasks(arguments As String) As Boolean
            Try
                Dim psi As New ProcessStartInfo()
                psi.FileName = "schtasks.exe"
                psi.Arguments = arguments
                psi.UseShellExecute = False
                psi.CreateNoWindow = True
                psi.RedirectStandardOutput = True
                psi.RedirectStandardError = True

                Using proc = Process.Start(psi)
                    If proc Is Nothing Then Return False

                    Dim completed = proc.WaitForExit(15000)
                    If Not completed Then
                        proc.Kill()
                        LogManager.AddWarn("[StartupManager] schtasks.exe timeout (15s).", "StartupManager")
                        Return False
                    End If

                    Dim exitOk = proc.ExitCode = 0
                    If Not exitOk Then
                        Dim errOut = proc.StandardError.ReadToEnd()
                        If Not String.IsNullOrWhiteSpace(errOut) Then
                            LogManager.AddWarn($"[StartupManager] schtasks: {errOut.Trim()}", "StartupManager")
                        End If
                    End If

                    Return exitOk
                End Using
            Catch ex As Exception
                LogManager.AddError($"[StartupManager] Errore esecuzione schtasks: {ex.Message}", "StartupManager")
                Return False
            End Try
        End Function

#End Region

#Region "Interfaccia Pubblica"

        ''' <summary>
        ''' Verifica se l autoavvio e abilitato usando il metodo configurato in AppSettings
        ''' (Registry o TaskScheduler).
        ''' </summary>
        ''' <returns>True se l autoavvio e attivo.</returns>
        Public Function IsAutoStartEnabled() As Boolean
            Try
                Dim method = If(AppSettings.Current.StartupMethod, "").ToLower()
                Select Case method
                    Case "taskscheduler", "task"
                        Return IsTaskSchedulerStartupEnabled()
                    Case Else
                        Return IsRegistryStartupEnabled()
                End Select
            Catch ex As Exception
                LogManager.AddError($"[StartupManager] Errore IsAutoStartEnabled: {ex.Message}", "StartupManager")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Abilita o disabilita l autoavvio usando il metodo configurato in AppSettings.
        ''' Aggiorna AppSettings.Current.AutoStartWindows e salva la configurazione.
        ''' </summary>
        ''' <param name="enabled">True per abilitare, False per disabilitare.</param>
        Public Sub SetAutoStart(enabled As Boolean)
            Try
                Dim cfg = AppSettings.Current
                Dim method = If(cfg.StartupMethod, "").ToLower()

                Select Case method
                    Case "taskscheduler", "task"
                        SetTaskSchedulerStartup(enabled, False)
                    Case Else
                        SetRegistryStartup(enabled)
                End Select

                cfg.AutoStartWindows = enabled
                AppSettings.Save()

                If enabled Then
                    LogManager.AddSuccess($"Autoavvio abilitato ({If(method = "task" OrElse method = "taskscheduler", "Task Scheduler", "Registro")}).", "StartupManager")
                Else
                    LogManager.AddInfo("Autoavvio disabilitato.", "StartupManager")
                End If

            Catch ex As Exception
                LogManager.AddError($"[StartupManager] Errore SetAutoStart: {ex.Message}", "StartupManager")
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Restituisce il percorso completo dell eseguibile dell applicazione corrente.
        ''' </summary>
        ''' <returns>Percorso assoluto dell exe.</returns>
        Public Function GetCurrentExePath() As String
            Return Windows.Forms.Application.ExecutablePath
        End Function

#End Region

    End Module

End Namespace