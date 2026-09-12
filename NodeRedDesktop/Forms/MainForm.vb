' =============================================================================
' Node-RED Desktop - MainForm.vb  
' Form principale dell'applicazione con tutta la logica di business
' Autore: Node-RED Desktop
' Versione: 1.0
' =============================================================================
Option Strict Off
Option Explicit On

Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Threading
Imports System.IO
Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports NodeRedDesktop.Modules
Imports NodeRedDesktop.Helpers


    ''' <summary>
    ''' Form principale dell'applicazione Node-RED Desktop.
    ''' Gestisce l'interfaccia utente a schede, lo stato dei servizi e il controllo del processo.
    ''' </summary>
    Partial Public Class MainForm

#Region "Variabili di Stato"
        Private _isClosing As Boolean = False
        Private _forceClose As Boolean = False
        Private _notificationCount As Integer = 0
        Private _logPaused As Boolean = False
        Private _logFilter As LogLevel? = Nothing
        Private _securityUsers As New List(Of SecurityManager.NodeRedUser)()
        Private _allowedIps As New List(Of String)()
#End Region

#Region "Inizializzazione"
        Public Sub New()
            ' La chiamata è richiesta da Progettazione Windows Form.
            InitializeComponent()
        End Sub

        Private Async Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' Non eseguire logica applicativa a tempo di progettazione in Visual Studio
            If DesignMode OrElse System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
                Return
            End If
            Await InitializeAppAsync()
        End Sub

        Private Async Function InitializeAppAsync() As Task
            ' Carica logo Node-RED
            Try
                Dim asmLoc = System.Reflection.Assembly.GetExecutingAssembly().Location
                Dim appDir = If(String.IsNullOrEmpty(asmLoc), Application.StartupPath, Path.GetDirectoryName(asmLoc))
                Dim pngPath = Path.Combine(appDir, "Resources", "nodered.png")
                Dim icoPath = Path.Combine(appDir, "Resources", "nodered.ico")
                
                If Not File.Exists(pngPath) Then
                    ' Prova anche cartella progetto se in debug
                    pngPath = Path.Combine(appDir, "..", "..", "Resources", "nodered.png")
                    icoPath = Path.Combine(appDir, "..", "..", "Resources", "nodered.ico")
                End If

                If File.Exists(pngPath) Then
                    picLogo.Image = Image.FromFile(pngPath)
                ElseIf File.Exists(icoPath) Then
                    Using ico As New Icon(icoPath)
                        picLogo.Image = ico.ToBitmap()
                    End Using
                End If
            Catch
            End Try

            UIHelper.ApplyDarkTheme(Me)

            ' Stile pulsanti principali
            UIHelper.StylePrimaryButton(btnStart)
            UIHelper.StyleSecondaryButton(btnStop)
            UIHelper.StyleSecondaryButton(btnRestart)
            UIHelper.StyleSecondaryButton(btnOpenBrowser)
            UIHelper.StylePrimaryButton(btnSaveSettings)
            UIHelper.StyleSecondaryButton(btnResetSettings)
            UIHelper.StylePrimaryButton(btnSaveSecurity)
            UIHelper.StyleSecondaryButton(btnOpenSettingsFile)
            UIHelper.StylePrimaryButton(btnBackupNow)
            UIHelper.StyleSecondaryButton(btnRestoreBackup)
            UIHelper.StyleDangerButton(btnDeleteBackup)
            UIHelper.StyleSecondaryButton(btnOpenBackupFolder)
            UIHelper.StylePrimaryButton(btnSaveBackupConfig)
            UIHelper.StyleSecondaryButton(btnCheckEnvironment)
            UIHelper.StylePrimaryButton(btnInstallNodeRed)
            UIHelper.StyleSecondaryButton(btnUpdateNodeRed)
            UIHelper.StyleSecondaryButton(btnDetectPaths)
            UIHelper.StyleSecondaryButton(btnBrowseUserDir)
            UIHelper.StyleSecondaryButton(btnBrowseBackup)

            btnAddUser.BackColor = UIHelper.ColSuccess
            btnAddUser.ForeColor = Color.White
            btnEditUser.BackColor = UIHelper.ColSurface
            btnEditUser.ForeColor = UIHelper.ColText
            btnDeleteUser.BackColor = UIHelper.ColError
            btnDeleteUser.ForeColor = Color.White

            ConfigureUsersGrid()
            ConfigureBackupListView()
            LogManager.InitSyncContext()
            SetupNotifyIcon()

            ' Event handlers NodeManager
            AddHandler NodeManager.Instance.NodeStarted, AddressOf OnNodeStarted
            AddHandler NodeManager.Instance.NodeStopped, AddressOf OnNodeStopped
            AddHandler NodeManager.Instance.NodeCrashed, AddressOf OnNodeCrashed
            AddHandler NodeManager.Instance.OutputReceived, AddressOf OnNodeOutputReceived
            AddHandler NodeManager.Instance.StatusChanged, AddressOf OnNodeStatusChanged
            AddHandler LogManager.LogAdded, AddressOf OnLogAdded

            ' Menu contestuale TrayIcon
            AddHandler cmsOpen.Click, AddressOf cmsApri_Click
            AddHandler cmsStart.Click, AddressOf cmsAvvia_Click
            AddHandler cmsStop.Click, AddressOf cmsFerma_Click
            AddHandler cmsRestart.Click, AddressOf cmsRiavvia_Click
            AddHandler cmsBrowser.Click, AddressOf cmsOpenBrowser_Click
            AddHandler cmsExit.Click, AddressOf cmsEsci_Click

            tmrRefresh.Start()
            tmrLog.Start()
            tmrBackup.Start()

            ' Caricamento impostazioni ed auto-rilevamento in background asincrono (interfaccia 100% reattiva)
            tssAppStatus.Text = "Inizializzazione configurazione..."
            Await Task.Run(Sub()
                               AppSettings.Load()
                               AppSettings.AutoDetectPaths()
                           End Sub)

            LoadSettingsToUI()
            BackupManager.StartAutoBackup()

            Dim cfg = AppSettings.Current

            ' Controllo porta non bloccante (0ms tramite IPGlobalProperties)
            If NodeManager.Instance.IsPortListening(cfg.NodeRedPort) Then
                Dim existing = Await Task.Run(Function() NodeManager.Instance.FindProcessListeningOnPort(cfg.NodeRedPort))
                If existing IsNot Nothing AndAlso existing.ProcessName.ToLower().Contains("node") Then
                    LogManager.AddInfo($"Rilevato Node-RED già attivo sulla porta {cfg.NodeRedPort} (PID={existing.Id}). Aggancio in corso...", "App")
                    NodeManager.Instance.AttachToExistingProcess(existing)
                End If
            ElseIf cfg.AutoStartNodeRed Then
                If cfg.StartDelaySeconds > 0 Then
                    LogManager.AddInfo($"Avvio automatico Node-RED programmato tra {cfg.StartDelaySeconds}s...", "App")
                    Await Task.Delay(cfg.StartDelaySeconds * 1000)
                End If
                If Not Me.IsDisposed AndAlso Not NodeManager.Instance.IsRunning Then
                    Await StartNodeRedAsync()
                End If
            End If

            tssAppStatus.Text = "Pronto"
            LogManager.AddSuccess("Node-RED Desktop avviato con successo.", "App")
        End Function

        Private Sub ConfigureUsersGrid()
            dgvUsers.Columns.Clear()
            dgvUsers.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "colUsername", .HeaderText = "Username", .Width = 140})
            Dim colPerms As New DataGridViewComboBoxColumn()
            colPerms.Name = "colPermissions"
            colPerms.HeaderText = "Permesso"
            colPerms.Width = 100
            colPerms.Items.AddRange("full", "read")
            dgvUsers.Columns.Add(colPerms)
            dgvUsers.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "colPassword", .HeaderText = "Password (Hash)", .Width = 180, .ReadOnly = True})
            dgvUsers.BackgroundColor = UIHelper.ColBackground
            dgvUsers.GridColor = UIHelper.ColBorder
            dgvUsers.DefaultCellStyle.BackColor = UIHelper.ColSurface
            dgvUsers.DefaultCellStyle.ForeColor = UIHelper.ColText
            dgvUsers.DefaultCellStyle.SelectionBackColor = UIHelper.ColAccent
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = UIHelper.ColHeaderBg
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = UIHelper.ColText
            dgvUsers.EnableHeadersVisualStyles = False
        End Sub

        Private Sub ConfigureBackupListView()
            lvBackups.View = View.Details
            lvBackups.FullRowSelect = True
            lvBackups.BackColor = UIHelper.ColSurface
            lvBackups.ForeColor = UIHelper.ColText
            lvBackups.BorderStyle = BorderStyle.None
        End Sub

        Private Sub SetupNotifyIcon()
            Try
                Dim iconPath As String = Path.Combine(Application.StartupPath, "Resources", "nodered.ico")
                If File.Exists(iconPath) Then
                    notifyIcon1.Icon = New Icon(iconPath)
                    Me.Icon = New Icon(iconPath)
                End If
            Catch ex As Exception
                ' Fallback
            End Try
            notifyIcon1.Text = "Node-RED Desktop"
            notifyIcon1.ContextMenuStrip = cmsNotifyIcon
        End Sub
#End Region

#Region "Caricamento e Salvataggio Impostazioni"
        Private Sub LoadSettingsToUI()
            Dim cfg = AppSettings.Current
            nudPort.Value = cfg.NodeRedPort
            txtUserDir.Text = If(String.IsNullOrEmpty(cfg.UserDir), AppSettings.GetUserDir(), cfg.UserDir)
            txtFlowFile.Text = cfg.FlowFile
            txtNodeRedCmd.Text = cfg.NodeRedCmdPath
            chkAutoStartWindows.Checked = cfg.AutoStartWindows
            cmbStartupMethod.SelectedIndex = If(cfg.StartupMethod = "TaskScheduler", 1, 0)
            chkAutoStartNodeRed.Checked = cfg.AutoStartNodeRed
            nudStartDelay.Value = cfg.StartDelaySeconds
            chkWatchdog.Checked = cfg.WatchdogEnabled
            nudWatchdogInterval.Value = cfg.WatchdogIntervalSeconds
            nudMaxRestarts.Value = cfg.MaxRestarts
            chkMinimizeToTray.Checked = cfg.MinimizeToTray
            chkStartMinimized.Checked = cfg.StartMinimized
            txtBackupFolder.Text = cfg.BackupFolder
            Select Case cfg.BackupSchedule
                Case "Hourly" : cmbBackupSchedule.SelectedIndex = 1
                Case "Daily" : cmbBackupSchedule.SelectedIndex = 2
                Case "Weekly" : cmbBackupSchedule.SelectedIndex = 3
                Case Else : cmbBackupSchedule.SelectedIndex = 0
            End Select
            nudMaxBackups.Value = cfg.MaxBackups
            lnkNodeRedUrl.Text = "http://127.0.0.1:" & cfg.NodeRedPort.ToString()
            lblPortValue.Text = cfg.NodeRedPort.ToString()
            LoadSecuritySettings()
        End Sub

        Private Sub SaveSettingsFromUI()
            Dim cfg = AppSettings.Current
            cfg.NodeRedPort = CInt(nudPort.Value)
            cfg.UserDir = txtUserDir.Text.Trim()
            cfg.FlowFile = txtFlowFile.Text.Trim()
            cfg.NodeRedCmdPath = txtNodeRedCmd.Text.Trim()
            cfg.AutoStartWindows = chkAutoStartWindows.Checked
            cfg.StartupMethod = If(cmbStartupMethod.SelectedIndex = 1, "TaskScheduler", "Registry")
            cfg.AutoStartNodeRed = chkAutoStartNodeRed.Checked
            cfg.StartDelaySeconds = CInt(nudStartDelay.Value)
            cfg.WatchdogEnabled = chkWatchdog.Checked
            cfg.WatchdogIntervalSeconds = CInt(nudWatchdogInterval.Value)
            cfg.MaxRestarts = CInt(nudMaxRestarts.Value)
            cfg.BackupFolder = txtBackupFolder.Text.Trim()
            cfg.BackupSchedule = GetBackupScheduleString()
            cfg.MaxBackups = CInt(nudMaxBackups.Value)
            cfg.MinimizeToTray = chkMinimizeToTray.Checked
            cfg.StartMinimized = chkStartMinimized.Checked
            AppSettings.Save()
            Try
                StartupManager.SetAutoStart(cfg.AutoStartWindows)
            Catch ex As Exception
                LogManager.AddWarn("Impossibile aggiornare autoavvio: " & ex.Message, "Startup")
            End Try
            If cfg.WatchdogEnabled Then NodeManager.Instance.StartWatchdog() Else NodeManager.Instance.StopWatchdog()
        End Sub

        Private Function GetBackupScheduleString() As String
            Select Case cmbBackupSchedule.SelectedIndex
                Case 1 : Return "Hourly"
                Case 2 : Return "Daily"
                Case 3 : Return "Weekly"
                Case Else : Return "Disabled"
            End Select
        End Function

        Private Sub LoadSecuritySettings()
            Try
                chkEnableAuth.Checked = SecurityManager.GetAuthEnabled()
                chkEnableIpRestriction.Checked = SecurityManager.GetIpRestrictionEnabled()
                chkEnableLdap.Checked = AppSettings.Current.LdapEnabled
                txtLdapUrl.Text = AppSettings.Current.LdapUrl
                txtLdapBaseDn.Text = AppSettings.Current.LdapBaseDn
                _securityUsers = SecurityManager.ReadUsers()
                RefreshUsersGrid()
                _allowedIps = SecurityManager.GetAllowedIps()
                RefreshIpList()
            Catch ex As Exception
                LogManager.AddWarn("Impossibile caricare impostazioni sicurezza: " & ex.Message, "Security")
            End Try
        End Sub
#End Region

#Region "Controllo Processo Node-RED"
        ''' <summary>
        ''' Avvia Node-RED in modo asincrono, aggiornando lo stato dei pulsanti.
        ''' </summary>
        Private Async Function StartNodeRedAsync() As Task(Of Boolean)
            If NodeManager.Instance.IsRunning Then
                ToastForm.Show("Node-RED", "E' già in esecuzione.", ToastType.Info)
                Return True
            End If
            btnStart.Enabled = False
            cmsStart.Enabled = False
            tssAppStatus.Text = "Avvio Node-RED in corso..."
            LogManager.AddInfo("Avvio Node-RED...", "NodeManager")
            Dim success = Await NodeManager.Instance.StartAsync()
            If Not success Then
                UpdateButtonStates(False)
                tssAppStatus.Text = "Errore durante l'avvio"
            End If
            Return success
        End Function

        ''' <summary>
        ''' Ferma Node-RED in modo asincrono senza bloccare l'interfaccia utente.
        ''' </summary>
        Private Async Function StopNodeRedAsync() As Task(Of Boolean)
            If Not NodeManager.Instance.IsRunning AndAlso Not NodeManager.Instance.IsPortListening(AppSettings.Current.NodeRedPort) Then
                ToastForm.Show("Node-RED", "Non è in esecuzione.", ToastType.Info)
                Return True
            End If
            btnStop.Enabled = False
            btnRestart.Enabled = False
            cmsStop.Enabled = False
            cmsRestart.Enabled = False
            tssAppStatus.Text = "Arresto Node-RED in corso..."
            LogManager.AddWarn("Arresto Node-RED...", "NodeManager")
            Dim ok = Await NodeManager.Instance.StopAsync()
            UpdateButtonStates(False)
            tssAppStatus.Text = "Node-RED fermato"
            Return ok
        End Function

        ''' <summary>
        ''' Riavvia Node-RED in modo asincrono: arresto, attesa rilascio porta e riavvio.
        ''' </summary>
        Private Async Function RestartNodeRedAsync() As Task(Of Boolean)
            btnStart.Enabled = False
            btnStop.Enabled = False
            btnRestart.Enabled = False
            cmsStart.Enabled = False
            cmsStop.Enabled = False
            cmsRestart.Enabled = False
            tssAppStatus.Text = "Riavvio Node-RED in corso..."
            LogManager.AddWarn("Riavvio Node-RED in corso...", "NodeManager")
            Dim ok = Await NodeManager.Instance.RestartAsync()
            UpdateButtonStates(NodeManager.Instance.IsRunning)
            Return ok
        End Function

        Private Sub StartNodeRed()
            Task.Run(Async Function() Await StartNodeRedAsync())
        End Sub

        Private Sub StopNodeRed()
            Task.Run(Async Function() Await StopNodeRedAsync())
        End Sub

        Private Sub RestartNodeRed()
            Task.Run(Async Function() Await RestartNodeRedAsync())
        End Sub

        Private Sub UpdateButtonStates(isRunning As Boolean)
            btnStart.Enabled = Not isRunning
            btnStop.Enabled = isRunning
            btnRestart.Enabled = isRunning
            btnOpenBrowser.Enabled = isRunning
            cmsStart.Enabled = Not isRunning
            cmsStop.Enabled = isRunning
            cmsRestart.Enabled = isRunning
            cmsBrowser.Enabled = isRunning
        End Sub
#End Region

#Region "Handler Eventi NodeManager"
        Private Sub OnNodeStarted(sender As Object, e As EventArgs)
            If InvokeRequired Then
                BeginInvoke(Sub() OnNodeStarted(sender, e))
                Return
            End If
            UpdateButtonStates(True)
            lblStatusValue.Text = ChrW(&H25CF) & " IN ESECUZIONE"
            lblStatusValue.ForeColor = UIHelper.ColSuccess
            lblPidValue.Text = NodeManager.Instance.ProcessId.ToString()
            pnlOnlineIndicator.BackColor = UIHelper.ColSuccess
            lblOnlineStatus.Text = "In esecuzione"
            lblOnlineStatus.ForeColor = UIHelper.ColSuccess
            tssNodeRedStatus.Text = "Node-RED: In esecuzione (PID " & NodeManager.Instance.ProcessId.ToString() & ")"
            tssNodeRedStatus.ForeColor = UIHelper.ColSuccess
            notifyIcon1.Text = "Node-RED Desktop - In esecuzione"
            ToastForm.Show("Node-RED Avviato", "In esecuzione sulla porta " & AppSettings.Current.NodeRedPort.ToString(), ToastType.Success)
            AddNotification("Node-RED avviato (PID " & NodeManager.Instance.ProcessId.ToString() & ")")
        End Sub

        Private Sub OnNodeStopped(sender As Object, e As EventArgs, wasExpected As Boolean)
            If InvokeRequired Then
                BeginInvoke(Sub() OnNodeStopped(sender, e, wasExpected))
                Return
            End If
            UpdateButtonStates(False)
            lblStatusValue.Text = ChrW(&H25CF) & " FERMO"
            lblStatusValue.ForeColor = UIHelper.ColError
            lblPidValue.Text = "—"
            lblUptimeValue.Text = "—"
            lblCpuValue.Text = "—"
            lblRamValue.Text = "—"
            pgbCpu.Value = 0
            pgbRam.Value = 0
            pnlOnlineIndicator.BackColor = UIHelper.ColError
            lblOnlineStatus.Text = "Fermo"
            lblOnlineStatus.ForeColor = UIHelper.ColTextMuted
            tssNodeRedStatus.Text = "Node-RED: Fermo"
            tssNodeRedStatus.ForeColor = UIHelper.ColTextMuted
            notifyIcon1.Text = "Node-RED Desktop - Fermo"
            If Not wasExpected Then
                ToastForm.Show("Node-RED Fermato", "Il processo si e' interrotto.", ToastType.Warning)
            End If
        End Sub

        Private Sub OnNodeCrashed(sender As Object, e As EventArgs)
            If InvokeRequired Then
                BeginInvoke(Sub() OnNodeCrashed(sender, e))
                Return
            End If
            ToastForm.Show("Crash Node-RED", "Processo arrestato. Il watchdog riavviera' Node-RED.", ToastType.Error, 6000)
            AddNotification("CRASH: Node-RED arrestato inaspettatamente")
            LogManager.AddError("Node-RED crash rilevato.", "NodeManager")
        End Sub

        Private Sub OnNodeOutputReceived(sender As Object, text As String, isError As Boolean)
            If String.IsNullOrWhiteSpace(text) Then Return
            LogManager.Add(text, If(isError, LogLevel.WARN, LogLevel.INFO), "Node-RED")
        End Sub

        Private Sub OnNodeStatusChanged(sender As Object, isRunning As Boolean, message As String)
            If InvokeRequired Then
                BeginInvoke(Sub() OnNodeStatusChanged(sender, isRunning, message))
                Return
            End If
            tssAppStatus.Text = message
        End Sub
#End Region

#Region "Timer Refresh Dashboard"
        Private Sub tmrRefresh_Tick(sender As Object, e As EventArgs) Handles tmrRefresh.Tick
            If NodeManager.Instance.IsRunning Then
                Dim uptime = NodeManager.Instance.Uptime
                lblUptimeValue.Text = UIHelper.FormatUptime(uptime)
                tssUptime.Text = "Uptime: " & UIHelper.FormatUptime(uptime)
                Dim cpu = NodeManager.Instance.CpuPercent
                lblCpuValue.Text = cpu.ToString("F1") & "%"
                pgbCpu.Value = Math.Min(100, CInt(cpu))
                Dim ram = NodeManager.Instance.MemoryMB
                lblRamValue.Text = ram.ToString("F0") & " MB"
                pgbRam.Value = Math.Min(pgbRam.Maximum, CInt(ram))
                lblRestartValue.Text = NodeManager.Instance.RestartCount.ToString()
            End If
        End Sub

        Private Sub tmrLog_Tick(sender As Object, e As EventArgs) Handles tmrLog.Tick
        End Sub

        Private Sub tmrBackup_Tick(sender As Object, e As EventArgs) Handles tmrBackup.Tick
        End Sub
#End Region

#Region "Log UI"
        Private Sub OnLogAdded(entry As LogEntry)
            If _logPaused Then Return
            AppendLogEntry(entry)
        End Sub

        Private Sub AppendLogEntry(entry As LogEntry)
            If _logFilter.HasValue AndAlso entry.Level <> _logFilter.Value Then Return
            Dim rtb = rtbLog
            Dim line = entry.GetFormattedLine()
            rtb.SuspendLayout()
            Dim start = rtb.TextLength
            rtb.AppendText(line & Environment.NewLine)
            rtb.Select(start, line.Length)
            rtb.SelectionColor = entry.GetColor()
            rtb.SelectionLength = 0
            If tsbAutoScroll.Checked Then
                rtb.SelectionStart = rtb.Text.Length
                rtb.ScrollToCaret()
            End If
            rtb.ResumeLayout()
            UpdateDashboardLog(entry)
        End Sub

        Private Sub UpdateDashboardLog(entry As LogEntry)
            Dim line = "[" & entry.Timestamp.ToString("HH:mm:ss") & "] " & entry.Message
            If rtbDashLog.Lines.Length > 80 Then rtbDashLog.Clear()
            Dim start = rtbDashLog.TextLength
            rtbDashLog.AppendText(line & Environment.NewLine)
            rtbDashLog.Select(start, line.Length)
            rtbDashLog.SelectionColor = entry.GetColor()
            rtbDashLog.SelectionStart = rtbDashLog.Text.Length
            rtbDashLog.ScrollToCaret()
        End Sub
#End Region

#Region "Eventi Pulsanti Dashboard"
        Private Async Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
            Await StartNodeRedAsync()
        End Sub

        Private Async Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
            Dim res = MessageBox.Show("Fermare Node-RED?", "Conferma Arresto", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If res = DialogResult.Yes Then
                Await StopNodeRedAsync()
            End If
        End Sub

        Private Async Sub btnRestart_Click(sender As Object, e As EventArgs) Handles btnRestart.Click
            Await RestartNodeRedAsync()
        End Sub

        Private Sub btnOpenBrowser_Click(sender As Object, e As EventArgs) Handles btnOpenBrowser.Click
            Process.Start(New ProcessStartInfo("http://127.0.0.1:" & AppSettings.Current.NodeRedPort.ToString()) With {.UseShellExecute = True})
        End Sub

        Private Sub lnkNodeRedUrl_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkNodeRedUrl.LinkClicked
            Process.Start(New ProcessStartInfo(lnkNodeRedUrl.Text) With {.UseShellExecute = True})
        End Sub

        Private Sub lnkOpenBrowser_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkOpenBrowser.LinkClicked
            btnOpenBrowser_Click(sender, e)
        End Sub
#End Region

#Region "Eventi Pulsanti Log"
        Private Sub tsbPauseLog_Click(sender As Object, e As EventArgs) Handles tsbPauseLog.Click
            _logPaused = tsbPauseLog.Checked
            tsbPauseLog.Text = If(_logPaused, "▶ Riprendi", "⏸ Pausa")
            tssAppStatus.Text = If(_logPaused, "Log in pausa", "Log attivo")
        End Sub

        Private Sub tsbClearLog_Click(sender As Object, e As EventArgs) Handles tsbClearLog.Click
            LogManager.Clear()
            rtbLog.Clear()
            rtbDashLog.Clear()
        End Sub

        Private Sub tsbExportTxt_Click(sender As Object, e As EventArgs) Handles tsbExportTxt.Click
            Using dlg As New SaveFileDialog()
                dlg.Filter = "File di testo (*.txt)|*.txt"
                dlg.FileName = "nodered-log-" & DateTime.Now.ToString("yyyyMMdd-HHmmss") & ".txt"
                If dlg.ShowDialog() = DialogResult.OK Then
                    If LogManager.ExportToText(dlg.FileName) Then
                        ToastForm.Show("Log Esportato", "File salvato in: " & dlg.FileName, ToastType.Success)
                    End If
                End If
            End Using
        End Sub

        Private Sub tsbExportCsv_Click(sender As Object, e As EventArgs) Handles tsbExportCsv.Click
            Using dlg As New SaveFileDialog()
                dlg.Filter = "File CSV (*.csv)|*.csv"
                dlg.FileName = "nodered-log-" & DateTime.Now.ToString("yyyyMMdd-HHmmss") & ".csv"
                If dlg.ShowDialog() = DialogResult.OK Then
                    If LogManager.ExportToCsv(dlg.FileName) Then
                        ToastForm.Show("Log Esportato", "CSV salvato in: " & dlg.FileName, ToastType.Success)
                    End If
                End If
            End Using
        End Sub

        Private Sub cmbLogFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLogFilter.SelectedIndexChanged
            Select Case cmbLogFilter.SelectedIndex
                Case 1 : _logFilter = LogLevel.INFO
                Case 2 : _logFilter = LogLevel.WARN
                Case 3 : _logFilter = LogLevel.ERROR
                Case 4 : _logFilter = LogLevel.DEBUG
                Case 5 : _logFilter = LogLevel.SUCCESS
                Case Else : _logFilter = Nothing
            End Select
            rtbLog.Clear()
            For Each entry In LogManager.GetAll(_logFilter)
                AppendLogEntry(entry)
            Next
        End Sub
#End Region

#Region "Eventi Scheda Ambiente"
        Private Sub btnCheckEnvironment_Click(sender As Object, e As EventArgs) Handles btnCheckEnvironment.Click
            CheckEnvironmentAsync()
        End Sub

        Private Async Sub CheckEnvironmentAsync()
            pgbEnvProgress.Visible = True
            pgbEnvProgress.Style = ProgressBarStyle.Marquee
            btnCheckEnvironment.Enabled = False
            rtbEnvOutput.Clear()
            rtbEnvOutput.AppendText("Verifica dipendenze in corso..." & Environment.NewLine)
            Dim results = Await Task.Run(Function() DependencyChecker.CheckAll())
            pgbEnvProgress.Visible = False
            btnCheckEnvironment.Enabled = True
            rtbEnvOutput.Clear()

            If results.ContainsKey("nodejs") Then
                Dim nodeStatus = results("nodejs")
                If nodeStatus.IsInstalled Then
                    lblNodejsStatusIcon.Text = ChrW(&H2714) : lblNodejsStatusIcon.ForeColor = UIHelper.ColSuccess
                    lblNodejsVersion.Text = "Node.js " & nodeStatus.Version
                    lblNodejsPath.Text = nodeStatus.Path
                    AppSettings.Current.LastKnownNodeVersion = nodeStatus.Version
                Else
                    lblNodejsStatusIcon.Text = ChrW(&H2716) : lblNodejsStatusIcon.ForeColor = UIHelper.ColError
                    lblNodejsVersion.Text = "Non installato"
                    lblNodejsPath.Text = nodeStatus.ErrorMessage
                End If
            End If

            If results.ContainsKey("npm") Then
                Dim npmStatus = results("npm")
                If npmStatus.IsInstalled Then
                    lblNpmStatusIcon.Text = ChrW(&H2714) : lblNpmStatusIcon.ForeColor = UIHelper.ColSuccess
                    lblNpmVersion.Text = "npm " & npmStatus.Version
                    lblNpmPath.Text = npmStatus.Path
                    AppSettings.Current.LastKnownNpmVersion = npmStatus.Version
                Else
                    lblNpmStatusIcon.Text = ChrW(&H2716) : lblNpmStatusIcon.ForeColor = UIHelper.ColError
                    lblNpmVersion.Text = "Non installato"
                    lblNpmPath.Text = npmStatus.ErrorMessage
                End If
            End If

            If results.ContainsKey("nodered") Then
                Dim nrStatus = results("nodered")
                If nrStatus.IsInstalled Then
                    lblNodeRedStatusIcon.Text = ChrW(&H2714) : lblNodeRedStatusIcon.ForeColor = UIHelper.ColSuccess
                    lblNodeRedVersion.Text = "Node-RED " & nrStatus.Version
                    lblNodeRedPath.Text = nrStatus.Path
                    AppSettings.Current.LastKnownNodeRedVersion = nrStatus.Version
                Else
                    lblNodeRedStatusIcon.Text = ChrW(&H2716) : lblNodeRedStatusIcon.ForeColor = UIHelper.ColError
                    lblNodeRedVersion.Text = "Non installato"
                    lblNodeRedPath.Text = nrStatus.ErrorMessage
                End If
            End If

            AppSettings.Save()
            rtbEnvOutput.AppendText("Verifica completata con successo." & Environment.NewLine)
            ToastForm.Show("Ambiente Verificato", "Verifica dipendenze completata.", ToastType.Info)
        End Sub

        Private Async Sub btnInstallNodeRed_Click(sender As Object, e As EventArgs) Handles btnInstallNodeRed.Click
            Dim res = MessageBox.Show("Installare Node-RED globalmente via npm?", "Conferma Installazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If res <> DialogResult.Yes Then Return
            pgbEnvProgress.Visible = True : pgbEnvProgress.Style = ProgressBarStyle.Marquee
            btnInstallNodeRed.Enabled = False
            rtbEnvOutput.Clear()
            Dim ok = Await DependencyChecker.InstallNodeRedAsync(Sub(line) BeginInvoke(Sub() rtbEnvOutput.AppendText(line & Environment.NewLine)))
            pgbEnvProgress.Visible = False : btnInstallNodeRed.Enabled = True
            If ok Then
                ToastForm.Show("Installazione Completata", "Node-RED installato correttamente!", ToastType.Success)
                CheckEnvironmentAsync()
            Else
                ToastForm.Show("Installazione Fallita", "Errore durante l'installazione.", ToastType.Error)
            End If
        End Sub

        Private Async Sub btnUpdateNodeRed_Click(sender As Object, e As EventArgs) Handles btnUpdateNodeRed.Click
            pgbEnvProgress.Visible = True : pgbEnvProgress.Style = ProgressBarStyle.Marquee
            btnUpdateNodeRed.Enabled = False
            rtbEnvOutput.Clear()
            Dim ok = Await DependencyChecker.UpdateNodeRedAsync(Sub(line) BeginInvoke(Sub() rtbEnvOutput.AppendText(line & Environment.NewLine)))
            pgbEnvProgress.Visible = False : btnUpdateNodeRed.Enabled = True
            If ok Then
                ToastForm.Show("Aggiornamento Completato", "Node-RED aggiornato!", ToastType.Success)
                CheckEnvironmentAsync()
            Else
                ToastForm.Show("Aggiornamento Fallito", "Errore durante l'aggiornamento.", ToastType.Error)
            End If
        End Sub
#End Region

#Region "Eventi Scheda Backup"
        Private Sub btnBrowseBackup_Click(sender As Object, e As EventArgs) Handles btnBrowseBackup.Click
            Using dlg As New FolderBrowserDialog()
                dlg.Description = "Seleziona la cartella per i backup"
                If dlg.ShowDialog() = DialogResult.OK Then txtBackupFolder.Text = dlg.SelectedPath
            End Using
        End Sub

        Private Sub btnSaveBackupConfig_Click(sender As Object, e As EventArgs) Handles btnSaveBackupConfig.Click
            AppSettings.Current.BackupFolder = txtBackupFolder.Text.Trim()
            AppSettings.Current.BackupSchedule = GetBackupScheduleString()
            AppSettings.Current.MaxBackups = CInt(nudMaxBackups.Value)
            AppSettings.Save()
            BackupManager.StopAutoBackup()
            BackupManager.StartAutoBackup()
            ToastForm.Show("Backup", "Configurazione salvata.", ToastType.Success)
        End Sub

        Private Async Sub btnBackupNow_Click(sender As Object, e As EventArgs) Handles btnBackupNow.Click
            btnBackupNow.Enabled = False
            tssAppStatus.Text = "Backup in corso..."
            Dim info = Await Task.Run(Function() BackupManager.CreateBackup())
            btnBackupNow.Enabled = True
            If Not String.IsNullOrEmpty(info.FilePath) Then
                ToastForm.Show("Backup Creato", info.FileName & " (" & UIHelper.FormatBytes(info.SizeBytes) & ")", ToastType.Success)
                RefreshBackupList()
            Else
                ToastForm.Show("Backup Fallito", "Impossibile creare il backup.", ToastType.Error)
            End If
            tssAppStatus.Text = "Pronto"
        End Sub

        Private Async Sub btnRestoreBackup_Click(sender As Object, e As EventArgs) Handles btnRestoreBackup.Click
            If lvBackups.SelectedItems.Count = 0 Then
                ToastForm.Show("Ripristino", "Seleziona un backup dalla lista.", ToastType.Info)
                Return
            End If
            Dim backupPath = CStr(lvBackups.SelectedItems(0).Tag)
            Dim res = MessageBox.Show("Ripristinare il backup selezionato? La configurazione corrente verra' sovrascritta.", "Conferma Ripristino", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If res = DialogResult.Yes Then
                Dim wasRunning = NodeManager.Instance.IsRunning
                If wasRunning Then Await NodeManager.Instance.StopAsync()
                Dim restored = Await Task.Run(Function() BackupManager.RestoreBackup(backupPath))
                If restored Then
                    ToastForm.Show("Ripristino Completato", "Backup ripristinato con successo.", ToastType.Success)
                    If wasRunning Then
                        Await Task.Delay(1000)
                        Await StartNodeRedAsync()
                    End If
                Else
                    ToastForm.Show("Ripristino Fallito", "Errore durante il ripristino.", ToastType.Error)
                End If
            End If
        End Sub

        Private Sub btnDeleteBackup_Click(sender As Object, e As EventArgs) Handles btnDeleteBackup.Click
            If lvBackups.SelectedItems.Count = 0 Then Return
            Dim backupPath = CStr(lvBackups.SelectedItems(0).Tag)
            Dim res = MessageBox.Show("Eliminare il backup selezionato?", "Conferma Eliminazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If res = DialogResult.Yes Then
                Try
                    File.Delete(backupPath)
                    RefreshBackupList()
                    ToastForm.Show("Backup", "Backup eliminato.", ToastType.Info)
                Catch ex As Exception
                    ToastForm.Show("Errore", "Impossibile eliminare: " & ex.Message, ToastType.Error)
                End Try
            End If
        End Sub

        Private Sub btnOpenBackupFolder_Click(sender As Object, e As EventArgs) Handles btnOpenBackupFolder.Click
            Dim folder = BackupManager.GetBackupFolder()
            If Directory.Exists(folder) Then
                Process.Start(New ProcessStartInfo("explorer.exe", folder) With {.UseShellExecute = True})
            Else
                ToastForm.Show("Cartella Backup", "La cartella backup non esiste ancora.", ToastType.Info)
            End If
        End Sub

        Private Sub RefreshBackupList()
            lvBackups.Items.Clear()
            For Each backup In BackupManager.ListBackups()
                Dim item As New ListViewItem(backup.FileName)
                item.SubItems.Add(backup.CreatedAt.ToString("dd/MM/yyyy HH:mm"))
                item.SubItems.Add(UIHelper.FormatBytes(backup.SizeBytes))
                item.SubItems.Add("OK")
                item.Tag = backup.FilePath
                item.ForeColor = UIHelper.ColText
                lvBackups.Items.Add(item)
            Next
        End Sub
#End Region

#Region "Eventi Scheda Sicurezza"
        Private Sub RefreshUsersGrid()
            dgvUsers.Rows.Clear()
            For Each user In _securityUsers
                Dim hashDisplay = If(String.IsNullOrEmpty(user.PasswordHash), "(non impostata)", "••••••••••")
                dgvUsers.Rows.Add(user.Username, user.Permissions, hashDisplay)
            Next
        End Sub

        Private Sub RefreshIpList()
            lstAllowedIps.Items.Clear()
            For Each ip In _allowedIps
                lstAllowedIps.Items.Add(ip)
            Next
        End Sub

        Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
            Dim username = Microsoft.VisualBasic.Interaction.InputBox("Username del nuovo utente:", "Aggiungi Utente", "")
            If String.IsNullOrWhiteSpace(username) Then Return
            Dim password = Microsoft.VisualBasic.Interaction.InputBox("Password (hashata automaticamente con bcrypt):", "Password Utente", "")
            If String.IsNullOrEmpty(password) Then Return
            AddUserAsync(username, password, "full")
        End Sub

        Private Async Sub AddUserAsync(username As String, password As String, permissions As String)
            tssAppStatus.Text = "Calcolo hash password..."
            Dim hash = Await SecurityManager.HashPassword(password)
            tssAppStatus.Text = "Pronto"
            If String.IsNullOrEmpty(hash) Then
                ToastForm.Show("Errore", "Impossibile calcolare hash della password. Node.js disponibile?", ToastType.Error)
                Return
            End If
            _securityUsers.Add(New SecurityManager.NodeRedUser() With {.Username = username, .PasswordHash = hash, .Permissions = permissions})
            RefreshUsersGrid()
            ToastForm.Show("Utente Aggiunto", "Utente '" & username & "' configurato.", ToastType.Success)
        End Sub

        Private Sub btnEditUser_Click(sender As Object, e As EventArgs) Handles btnEditUser.Click
            If dgvUsers.SelectedRows.Count = 0 Then Return
            Dim idx = dgvUsers.SelectedRows(0).Index
            Dim user = _securityUsers(idx)
            Dim newPwd = Microsoft.VisualBasic.Interaction.InputBox("Nuova password per '" & user.Username & "' (lascia vuoto per non modificare):", "Modifica Password", "")
            If String.IsNullOrEmpty(newPwd) Then Return
            EditUserAsync(idx, newPwd)
        End Sub

        Private Async Sub EditUserAsync(idx As Integer, password As String)
            tssAppStatus.Text = "Calcolo hash password..."
            Dim hash = Await SecurityManager.HashPassword(password)
            tssAppStatus.Text = "Pronto"
            If Not String.IsNullOrEmpty(hash) Then
                Dim u = _securityUsers(idx)
                u.PasswordHash = hash
                _securityUsers(idx) = u
                RefreshUsersGrid()
                ToastForm.Show("Utente Aggiornato", "Password modificata con successo.", ToastType.Success)
            End If
        End Sub

        Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs) Handles btnDeleteUser.Click
            If dgvUsers.SelectedRows.Count = 0 Then Return
            Dim idx = dgvUsers.SelectedRows(0).Index
            Dim user = _securityUsers(idx)
            Dim res = MessageBox.Show("Eliminare l'utente '" & user.Username & "'?", "Conferma Eliminazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If res = DialogResult.Yes Then
                _securityUsers.RemoveAt(idx)
                RefreshUsersGrid()
            End If
        End Sub

        Private Sub btnAddIp_Click(sender As Object, e As EventArgs) Handles btnAddIp.Click
            Dim ip = txtNewIp.Text.Trim()
            If String.IsNullOrEmpty(ip) Then Return
            If Not _allowedIps.Contains(ip) Then
                _allowedIps.Add(ip)
                RefreshIpList()
                txtNewIp.Clear()
            End If
        End Sub

        Private Sub btnRemoveIp_Click(sender As Object, e As EventArgs) Handles btnRemoveIp.Click
            If lstAllowedIps.SelectedIndex < 0 Then Return
            _allowedIps.RemoveAt(lstAllowedIps.SelectedIndex)
            RefreshIpList()
        End Sub

        Private Async Sub btnSaveSecurity_Click(sender As Object, e As EventArgs) Handles btnSaveSecurity.Click
            btnSaveSecurity.Enabled = False
            tssAppStatus.Text = "Salvataggio impostazioni sicurezza..."
            Dim ok = Await SecurityManager.SaveSettings(_securityUsers, chkEnableAuth.Checked, chkEnableIpRestriction.Checked, _allowedIps, chkEnableLdap.Checked, txtLdapUrl.Text.Trim(), txtLdapBaseDn.Text.Trim())
            AppSettings.Current.AuthEnabled = chkEnableAuth.Checked
            AppSettings.Current.IpRestrictionEnabled = chkEnableIpRestriction.Checked
            AppSettings.Current.AllowedIps = String.Join(";", _allowedIps)
            AppSettings.Current.LdapEnabled = chkEnableLdap.Checked
            AppSettings.Current.LdapUrl = txtLdapUrl.Text.Trim()
            AppSettings.Current.LdapBaseDn = txtLdapBaseDn.Text.Trim()
            AppSettings.Save()
            btnSaveSecurity.Enabled = True
            tssAppStatus.Text = "Pronto"
            If ok Then
                ToastForm.Show("Sicurezza Salvata", "settings.js aggiornato. Riavviare Node-RED per applicare.", ToastType.Success, 6000)
                Dim res = MessageBox.Show("settings.js salvato con successo." & Environment.NewLine & "Riavviare Node-RED per applicare le nuove restrizioni?", "Riavvio Necessario", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If res = DialogResult.Yes AndAlso NodeManager.Instance.IsRunning Then
                    Await RestartNodeRedAsync()
                End If
            Else
                ToastForm.Show("Errore Sicurezza", "Impossibile aggiornare settings.js.", ToastType.Error)
            End If
        End Sub

        Private Sub btnOpenSettingsFile_Click(sender As Object, e As EventArgs) Handles btnOpenSettingsFile.Click
            Dim path = SecurityManager.GetSettingsFilePath()
            If File.Exists(path) Then
                Process.Start(New ProcessStartInfo("notepad.exe", Chr(34) & path & Chr(34)) With {.UseShellExecute = True})
            Else
                ToastForm.Show("settings.js", "File non trovato.", ToastType.Warning)
            End If
        End Sub
#End Region

#Region "Eventi Scheda Impostazioni"
        Private Sub btnBrowseUserDir_Click(sender As Object, e As EventArgs) Handles btnBrowseUserDir.Click
            Using dlg As New FolderBrowserDialog()
                dlg.Description = "Seleziona la directory dati di Node-RED"
                dlg.SelectedPath = AppSettings.GetUserDir()
                If dlg.ShowDialog() = DialogResult.OK Then txtUserDir.Text = dlg.SelectedPath
            End Using
        End Sub

        Private Async Sub btnDetectPaths_Click(sender As Object, e As EventArgs) Handles btnDetectPaths.Click
            btnDetectPaths.Enabled = False
            tssAppStatus.Text = "Rilevamento percorsi in corso..."
            Await Task.Run(Sub() AppSettings.AutoDetectPaths())
            txtNodeRedCmd.Text = AppSettings.Current.NodeRedCmdPath
            btnDetectPaths.Enabled = True
            tssAppStatus.Text = "Pronto"
            ToastForm.Show("Auto-rilevamento", "Percorsi rilevati con successo.", ToastType.Info)
        End Sub

        Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
            SaveSettingsFromUI()
            LoadSettingsToUI()
            ToastForm.Show("Impostazioni Salvate", "Configurazione aggiornata.", ToastType.Success)
            LogManager.AddSuccess("Impostazioni salvate dall'utente.", "Settings")
        End Sub

        Private Sub btnResetSettings_Click(sender As Object, e As EventArgs) Handles btnResetSettings.Click
            Dim res = MessageBox.Show("Ripristinare tutte le impostazioni ai valori predefiniti?", "Conferma Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If res = DialogResult.Yes Then
                AppSettings.Current = New AppConfig()
                AppSettings.Save()
                LoadSettingsToUI()
                ToastForm.Show("Reset", "Impostazioni ripristinate ai valori predefiniti.", ToastType.Info)
            End If
        End Sub
#End Region

#Region "Centro Notifiche"
        Private Sub AddNotification(message As String)
            _notificationCount += 1
            lblNotificationBadge.Text = If(_notificationCount > 99, "99+", _notificationCount.ToString())
            lblNotificationBadge.Visible = _notificationCount > 0
        End Sub

        Private Sub btnNotifications_Click(sender As Object, e As EventArgs) Handles btnNotifications.Click
            _notificationCount = 0
            lblNotificationBadge.Visible = False
            tabMain.SelectedTab = tabLog
        End Sub
#End Region

#Region "System Tray"
        Private Sub notifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles notifyIcon1.DoubleClick
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            Me.BringToFront()
        End Sub

        Private Sub cmsApri_Click(sender As Object, e As EventArgs)
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            Me.BringToFront()
        End Sub

        Private Async Sub cmsAvvia_Click(sender As Object, e As EventArgs)
            Await StartNodeRedAsync()
        End Sub

        Private Async Sub cmsFerma_Click(sender As Object, e As EventArgs)
            Await StopNodeRedAsync()
        End Sub

        Private Async Sub cmsRiavvia_Click(sender As Object, e As EventArgs)
            Await RestartNodeRedAsync()
        End Sub

        Private Sub cmsOpenBrowser_Click(sender As Object, e As EventArgs)
            btnOpenBrowser_Click(sender, e)
        End Sub

        Private Sub cmsEsci_Click(sender As Object, e As EventArgs)
            _forceClose = True
            Me.Close()
        End Sub

        Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged
            If tabMain.SelectedTab Is tabBackup Then RefreshBackupList()
            If tabMain.SelectedTab Is tabSecurity Then LoadSecuritySettings()
        End Sub
#End Region

#Region "Chiusura Form"
        Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
            If Not _forceClose AndAlso AppSettings.Current.MinimizeToTray Then
                e.Cancel = True
                Me.Hide()
                notifyIcon1.ShowBalloonTip(2000, "Node-RED Desktop", "Applicazione attiva nella barra delle applicazioni.", ToolTipIcon.Info)
                Return
            End If

            _isClosing = True
            tmrRefresh.Stop()
            tmrLog.Stop()
            tmrBackup.Stop()
            BackupManager.StopAutoBackup()

            If NodeManager.Instance.IsRunning Then
                Dim res = MessageBox.Show("Node-RED e' attualmente in esecuzione. Arrestarlo prima di uscire?", "Chiusura", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If res = DialogResult.Cancel Then
                    e.Cancel = True
                    _isClosing = False
                    Return
                ElseIf res = DialogResult.Yes Then
                    NodeManager.Instance.Stop()
                End If
            End If

            notifyIcon1.Visible = False
            AppSettings.Save()
            MyBase.OnFormClosing(e)
        End Sub
#End Region

    End Class
