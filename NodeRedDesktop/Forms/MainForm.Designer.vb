' ============================================================
' File: MainForm.Designer.vb
' Progetto: Node-RED Desktop
' Descrizione: Designer completo per la MainForm - finestra principale
'              con header, tabcontrol (Dashboard/Ambiente/Log/Backup/
'              Sicurezza/Impostazioni), statusstrip, tray icon e timer.
' Autore: NodeRedDesktop
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits Global.System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblNotificationBadge = New System.Windows.Forms.Label()
        Me.btnNotifications = New System.Windows.Forms.Button()
        Me.lblOnlineStatus = New System.Windows.Forms.Label()
        Me.pnlOnlineIndicator = New System.Windows.Forms.Panel()
        Me.lblAppVersion = New System.Windows.Forms.Label()
        Me.lblAppTitle = New System.Windows.Forms.Label()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tabDashboard = New System.Windows.Forms.TabPage()
        Me.btnStartSuite = New System.Windows.Forms.Button()
        Me.btnStopSuite = New System.Windows.Forms.Button()
        Me.btnRestartSuite = New System.Windows.Forms.Button()
        Me.btnOpenBrowser = New System.Windows.Forms.Button()
        Me.btnCheckUpdates = New System.Windows.Forms.Button()
        Me.pnlUpdateBanner = New System.Windows.Forms.Panel()
        Me.lnkChangelogQuick = New System.Windows.Forms.LinkLabel()
        Me.btnUpdateNowQuick = New System.Windows.Forms.Button()
        Me.lblUpdateBannerText = New System.Windows.Forms.Label()
        Me.rtbDashLog = New System.Windows.Forms.RichTextBox()
        Me.pnlResourcesCard = New System.Windows.Forms.Panel()
        Me.lblRestartValue = New System.Windows.Forms.Label()
        Me.lblRestartLabel = New System.Windows.Forms.Label()
        Me.lblRamValue = New System.Windows.Forms.Label()
        Me.lblRamLabel = New System.Windows.Forms.Label()
        Me.pgbCpu = New System.Windows.Forms.ProgressBar()
        Me.lblCpuValue = New System.Windows.Forms.Label()
        Me.lblCpuLabel = New System.Windows.Forms.Label()
        Me.lblResourcesHeader = New System.Windows.Forms.Label()
        Me.pgbRam = New System.Windows.Forms.ProgressBar()
        Me.pnlOllamaCard = New System.Windows.Forms.Panel()
        Me.btnRestartOllamaQuick = New System.Windows.Forms.Button()
        Me.btnStopOllamaQuick = New System.Windows.Forms.Button()
        Me.btnStartOllamaQuick = New System.Windows.Forms.Button()
        Me.lblOllamaOptValue = New System.Windows.Forms.Label()
        Me.lblOllamaOptLabel = New System.Windows.Forms.Label()
        Me.lblOllamaPortValue = New System.Windows.Forms.Label()
        Me.lblOllamaPortLabel = New System.Windows.Forms.Label()
        Me.lblOllamaModelValue = New System.Windows.Forms.Label()
        Me.lblOllamaModelLabel = New System.Windows.Forms.Label()
        Me.lblOllamaStatusValue = New System.Windows.Forms.Label()
        Me.lblOllamaHeader = New System.Windows.Forms.Label()
        Me.pnlStatusCard = New System.Windows.Forms.Panel()
        Me.lnkOpenBrowser = New System.Windows.Forms.LinkLabel()
        Me.lblPortValue = New System.Windows.Forms.Label()
        Me.lblPortLabel = New System.Windows.Forms.Label()
        Me.lblUptimeValue = New System.Windows.Forms.Label()
        Me.lblUptimeLabel = New System.Windows.Forms.Label()
        Me.lblPidValue = New System.Windows.Forms.Label()
        Me.lblPidLabel = New System.Windows.Forms.Label()
        Me.btnStartNodeRed = New System.Windows.Forms.Button()
        Me.btnRestartNodeRed = New System.Windows.Forms.Button()
        Me.lblNodeRedStatusValue = New System.Windows.Forms.Label()
        Me.btnStopNodeRed = New System.Windows.Forms.Button()
        Me.lblStatusHeader = New System.Windows.Forms.Label()
        Me.lblUrlLabel = New System.Windows.Forms.Label()
        Me.lnkNodeRedUrl = New System.Windows.Forms.LinkLabel()
        Me.tabOllama = New System.Windows.Forms.TabPage()
        Me.grpTestChat = New System.Windows.Forms.GroupBox()
        Me.lblTestStats = New System.Windows.Forms.Label()
        Me.txtTestReply = New System.Windows.Forms.RichTextBox()
        Me.btnTestChat = New System.Windows.Forms.Button()
        Me.txtTestPrompt = New System.Windows.Forms.TextBox()
        Me.lblTestPrompt = New System.Windows.Forms.Label()
        Me.grpOllamaOpt = New System.Windows.Forms.GroupBox()
        Me.btnApplyOptimizations = New System.Windows.Forms.Button()
        Me.lblOptDesc = New System.Windows.Forms.Label()
        Me.grpOllamaModels = New System.Windows.Forms.GroupBox()
        Me.btnPullModelMini = New System.Windows.Forms.Button()
        Me.btnPullModelLight = New System.Windows.Forms.Button()
        Me.btnRefreshModels = New System.Windows.Forms.Button()
        Me.cmbOllamaModels = New System.Windows.Forms.ComboBox()
        Me.lblSelectModel = New System.Windows.Forms.Label()
        Me.grpOllamaService = New System.Windows.Forms.GroupBox()
        Me.btnRestartOllama = New System.Windows.Forms.Button()
        Me.btnStopOllama = New System.Windows.Forms.Button()
        Me.btnStartOllama = New System.Windows.Forms.Button()
        Me.lblOllamaSvcPort = New System.Windows.Forms.Label()
        Me.lblOllamaSvcStatus = New System.Windows.Forms.Label()
        Me.lblOllamaSubtitle = New System.Windows.Forms.Label()
        Me.lblOllamaTitle = New System.Windows.Forms.Label()
        Me.tabEnvironment = New System.Windows.Forms.TabPage()
        Me.rtbEnvOutput = New System.Windows.Forms.RichTextBox()
        Me.pgbEnvProgress = New System.Windows.Forms.ProgressBar()
        Me.btnSetupSuiteWizard = New System.Windows.Forms.Button()
        Me.btnUpdateNodeRed = New System.Windows.Forms.Button()
        Me.btnCheckEnvironment = New System.Windows.Forms.Button()
        Me.btnInstallOllama = New System.Windows.Forms.Button()
        Me.btnInstallNodeRed = New System.Windows.Forms.Button()
        Me.btnInstallNodeJs = New System.Windows.Forms.Button()
        Me.tlpEnvironment = New System.Windows.Forms.TableLayoutPanel()
        Me.grpNodeJs = New System.Windows.Forms.GroupBox()
        Me.lblNodejsPath = New System.Windows.Forms.Label()
        Me.lblNodejsVersion = New System.Windows.Forms.Label()
        Me.lblNodejsStatusIcon = New System.Windows.Forms.Label()
        Me.grpNpm = New System.Windows.Forms.GroupBox()
        Me.lblNpmPath = New System.Windows.Forms.Label()
        Me.lblNpmVersion = New System.Windows.Forms.Label()
        Me.lblNpmStatusIcon = New System.Windows.Forms.Label()
        Me.grpNodeRed = New System.Windows.Forms.GroupBox()
        Me.lblNodeRedPath = New System.Windows.Forms.Label()
        Me.lblNodeRedVersion = New System.Windows.Forms.Label()
        Me.lblNodeRedStatusIcon = New System.Windows.Forms.Label()
        Me.grpOllama = New System.Windows.Forms.GroupBox()
        Me.lblOllamaEnvPath = New System.Windows.Forms.Label()
        Me.lblOllamaEnvVersion = New System.Windows.Forms.Label()
        Me.lblOllamaEnvIcon = New System.Windows.Forms.Label()
        Me.lblEnvSubtitle = New System.Windows.Forms.Label()
        Me.lblEnvTitle = New System.Windows.Forms.Label()
        Me.tabLog = New System.Windows.Forms.TabPage()
        Me.rtbLog = New System.Windows.Forms.RichTextBox()
        Me.tsLog = New System.Windows.Forms.ToolStrip()
        Me.tsbPauseLog = New System.Windows.Forms.ToolStripButton()
        Me.tsbClearLog = New System.Windows.Forms.ToolStripButton()
        Me.sep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbExportTxt = New System.Windows.Forms.ToolStripButton()
        Me.tsbExportCsv = New System.Windows.Forms.ToolStripButton()
        Me.sep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblFilter = New System.Windows.Forms.ToolStripLabel()
        Me.cmbLogFilter = New System.Windows.Forms.ToolStripComboBox()
        Me.sep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbAutoScroll = New System.Windows.Forms.ToolStripButton()
        Me.tabBackup = New System.Windows.Forms.TabPage()
        Me.btnOpenBackupFolder = New System.Windows.Forms.Button()
        Me.lvBackups = New System.Windows.Forms.ListView()
        Me.colBackupName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBackupDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBackupSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBackupStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnDeleteBackup = New System.Windows.Forms.Button()
        Me.btnRestoreBackup = New System.Windows.Forms.Button()
        Me.btnBackupNow = New System.Windows.Forms.Button()
        Me.grpBackupConfig = New System.Windows.Forms.GroupBox()
        Me.btnSaveBackupConfig = New System.Windows.Forms.Button()
        Me.nudMaxBackups = New System.Windows.Forms.NumericUpDown()
        Me.lblMaxBackups = New System.Windows.Forms.Label()
        Me.cmbBackupSchedule = New System.Windows.Forms.ComboBox()
        Me.lblSchedule = New System.Windows.Forms.Label()
        Me.btnBrowseBackup = New System.Windows.Forms.Button()
        Me.txtBackupFolder = New System.Windows.Forms.TextBox()
        Me.lblBackupFolder = New System.Windows.Forms.Label()
        Me.lblBackupTitle = New System.Windows.Forms.Label()
        Me.tabSecurity = New System.Windows.Forms.TabPage()
        Me.btnOpenSettingsFile = New System.Windows.Forms.Button()
        Me.btnSaveSecurity = New System.Windows.Forms.Button()
        Me.grpLdap = New System.Windows.Forms.GroupBox()
        Me.txtLdapBaseDn = New System.Windows.Forms.TextBox()
        Me.lblLdapBaseDn = New System.Windows.Forms.Label()
        Me.txtLdapUrl = New System.Windows.Forms.TextBox()
        Me.lblLdapUrl = New System.Windows.Forms.Label()
        Me.chkEnableLdap = New System.Windows.Forms.CheckBox()
        Me.grpIpRestriction = New System.Windows.Forms.GroupBox()
        Me.btnRemoveIp = New System.Windows.Forms.Button()
        Me.btnAddIp = New System.Windows.Forms.Button()
        Me.txtNewIp = New System.Windows.Forms.TextBox()
        Me.lstAllowedIps = New System.Windows.Forms.ListBox()
        Me.chkEnableIpRestriction = New System.Windows.Forms.CheckBox()
        Me.grpUsers = New System.Windows.Forms.GroupBox()
        Me.btnDeleteUser = New System.Windows.Forms.Button()
        Me.btnEditUser = New System.Windows.Forms.Button()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.grpAuthToggle = New System.Windows.Forms.GroupBox()
        Me.lblAuthInfo = New System.Windows.Forms.Label()
        Me.chkEnableAuth = New System.Windows.Forms.CheckBox()
        Me.lblSecSubtitle = New System.Windows.Forms.Label()
        Me.lblSecTitle = New System.Windows.Forms.Label()
        Me.tabSettings = New System.Windows.Forms.TabPage()
        Me.btnResetSettings = New System.Windows.Forms.Button()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.grpWatchdog = New System.Windows.Forms.GroupBox()
        Me.lblMaxRestartsHint = New System.Windows.Forms.Label()
        Me.nudMaxRestarts = New System.Windows.Forms.NumericUpDown()
        Me.lblMaxRestarts = New System.Windows.Forms.Label()
        Me.nudWatchdogInterval = New System.Windows.Forms.NumericUpDown()
        Me.lblWatchInterval = New System.Windows.Forms.Label()
        Me.chkWatchdog = New System.Windows.Forms.CheckBox()
        Me.grpStartup = New System.Windows.Forms.GroupBox()
        Me.chkStartMinimized = New System.Windows.Forms.CheckBox()
        Me.nudStartDelay = New System.Windows.Forms.NumericUpDown()
        Me.chkMinimizeToTray = New System.Windows.Forms.CheckBox()
        Me.lblStartDelay = New System.Windows.Forms.Label()
        Me.chkAutoStartNodeRed = New System.Windows.Forms.CheckBox()
        Me.lblMethodHint = New System.Windows.Forms.Label()
        Me.cmbStartupMethod = New System.Windows.Forms.ComboBox()
        Me.lblStartupMethod = New System.Windows.Forms.Label()
        Me.chkAutoStartWindows = New System.Windows.Forms.CheckBox()
        Me.grpNodeRedConfig = New System.Windows.Forms.GroupBox()
        Me.btnDetectPaths = New System.Windows.Forms.Button()
        Me.txtNodeRedCmd = New System.Windows.Forms.TextBox()
        Me.lblNodeRedCmd = New System.Windows.Forms.Label()
        Me.lblFlowFileHint = New System.Windows.Forms.Label()
        Me.txtFlowFile = New System.Windows.Forms.TextBox()
        Me.lblFlowFile = New System.Windows.Forms.Label()
        Me.btnBrowseUserDir = New System.Windows.Forms.Button()
        Me.txtUserDir = New System.Windows.Forms.TextBox()
        Me.lblUserDir = New System.Windows.Forms.Label()
        Me.lblPortHint = New System.Windows.Forms.Label()
        Me.nudPort = New System.Windows.Forms.NumericUpDown()
        Me.lblPort = New System.Windows.Forms.Label()
        Me.lblSettingsTitle = New System.Windows.Forms.Label()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tssAppStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssNodeRedStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssSep = New System.Windows.Forms.ToolStripSeparator()
        Me.tssUptime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.notifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmsNotifyIcon = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsStart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsStop = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsRestart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsBrowser = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLog = New System.Windows.Forms.Timer(Me.components)
        Me.tmrBackup = New System.Windows.Forms.Timer(Me.components)
        Me.pnlHeader.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.tabDashboard.SuspendLayout()
        Me.pnlUpdateBanner.SuspendLayout()
        Me.pnlResourcesCard.SuspendLayout()
        Me.pnlOllamaCard.SuspendLayout()
        Me.pnlStatusCard.SuspendLayout()
        Me.tabOllama.SuspendLayout()
        Me.grpTestChat.SuspendLayout()
        Me.grpOllamaOpt.SuspendLayout()
        Me.grpOllamaModels.SuspendLayout()
        Me.grpOllamaService.SuspendLayout()
        Me.tabEnvironment.SuspendLayout()
        Me.tlpEnvironment.SuspendLayout()
        Me.grpNodeJs.SuspendLayout()
        Me.grpNpm.SuspendLayout()
        Me.grpNodeRed.SuspendLayout()
        Me.grpOllama.SuspendLayout()
        Me.tabLog.SuspendLayout()
        Me.tsLog.SuspendLayout()
        Me.tabBackup.SuspendLayout()
        Me.grpBackupConfig.SuspendLayout()
        CType(Me.nudMaxBackups, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSecurity.SuspendLayout()
        Me.grpLdap.SuspendLayout()
        Me.grpIpRestriction.SuspendLayout()
        Me.grpUsers.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAuthToggle.SuspendLayout()
        Me.tabSettings.SuspendLayout()
        Me.grpWatchdog.SuspendLayout()
        CType(Me.nudMaxRestarts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWatchdogInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpStartup.SuspendLayout()
        CType(Me.nudStartDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpNodeRedConfig.SuspendLayout()
        CType(Me.nudPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ssMain.SuspendLayout()
        Me.cmsNotifyIcon.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.SystemColors.Control
        Me.pnlHeader.Controls.Add(Me.lblNotificationBadge)
        Me.pnlHeader.Controls.Add(Me.btnNotifications)
        Me.pnlHeader.Controls.Add(Me.lblOnlineStatus)
        Me.pnlHeader.Controls.Add(Me.pnlOnlineIndicator)
        Me.pnlHeader.Controls.Add(Me.lblAppVersion)
        Me.pnlHeader.Controls.Add(Me.lblAppTitle)
        Me.pnlHeader.Controls.Add(Me.picLogo)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(884, 60)
        Me.pnlHeader.TabIndex = 0
        '
        'lblNotificationBadge
        '
        Me.lblNotificationBadge.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNotificationBadge.BackColor = System.Drawing.Color.DarkRed
        Me.lblNotificationBadge.Font = New System.Drawing.Font("Segoe UI", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblNotificationBadge.ForeColor = System.Drawing.Color.White
        Me.lblNotificationBadge.Location = New System.Drawing.Point(854, 11)
        Me.lblNotificationBadge.Name = "lblNotificationBadge"
        Me.lblNotificationBadge.Size = New System.Drawing.Size(18, 18)
        Me.lblNotificationBadge.TabIndex = 6
        Me.lblNotificationBadge.Text = "0"
        Me.lblNotificationBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblNotificationBadge.Visible = False
        '
        'btnNotifications
        '
        Me.btnNotifications.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNotifications.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.btnNotifications.Location = New System.Drawing.Point(840, 18)
        Me.btnNotifications.Name = "btnNotifications"
        Me.btnNotifications.Size = New System.Drawing.Size(32, 23)
        Me.btnNotifications.TabIndex = 5
        Me.btnNotifications.Text = "!"
        Me.btnNotifications.UseVisualStyleBackColor = True
        '
        'lblOnlineStatus
        '
        Me.lblOnlineStatus.AutoSize = True
        Me.lblOnlineStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblOnlineStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOnlineStatus.Location = New System.Drawing.Point(266, 18)
        Me.lblOnlineStatus.Name = "lblOnlineStatus"
        Me.lblOnlineStatus.Size = New System.Drawing.Size(43, 15)
        Me.lblOnlineStatus.TabIndex = 4
        Me.lblOnlineStatus.Text = "Offline"
        '
        'pnlOnlineIndicator
        '
        Me.pnlOnlineIndicator.BackColor = System.Drawing.Color.DarkRed
        Me.pnlOnlineIndicator.Location = New System.Drawing.Point(248, 20)
        Me.pnlOnlineIndicator.Name = "pnlOnlineIndicator"
        Me.pnlOnlineIndicator.Size = New System.Drawing.Size(12, 12)
        Me.pnlOnlineIndicator.TabIndex = 3
        '
        'lblAppVersion
        '
        Me.lblAppVersion.AutoSize = True
        Me.lblAppVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAppVersion.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblAppVersion.Location = New System.Drawing.Point(58, 35)
        Me.lblAppVersion.Name = "lblAppVersion"
        Me.lblAppVersion.Size = New System.Drawing.Size(28, 15)
        Me.lblAppVersion.TabIndex = 2
        Me.lblAppVersion.Text = "v1.0"
        Me.lblAppVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAppTitle
        '
        Me.lblAppTitle.AutoSize = True
        Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblAppTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAppTitle.Location = New System.Drawing.Point(58, 10)
        Me.lblAppTitle.Name = "lblAppTitle"
        Me.lblAppTitle.Size = New System.Drawing.Size(184, 25)
        Me.lblAppTitle.TabIndex = 1
        Me.lblAppTitle.Text = "Node-RED Desktop"
        Me.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picLogo
        '
        Me.picLogo.Location = New System.Drawing.Point(12, 10)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(40, 40)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLogo.TabIndex = 0
        Me.picLogo.TabStop = False
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabDashboard)
        Me.tabMain.Controls.Add(Me.tabOllama)
        Me.tabMain.Controls.Add(Me.tabEnvironment)
        Me.tabMain.Controls.Add(Me.tabLog)
        Me.tabMain.Controls.Add(Me.tabBackup)
        Me.tabMain.Controls.Add(Me.tabSecurity)
        Me.tabMain.Controls.Add(Me.tabSettings)
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tabMain.Location = New System.Drawing.Point(0, 60)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(884, 518)
        Me.tabMain.TabIndex = 1
        '
        'tabDashboard
        '
        Me.tabDashboard.BackColor = System.Drawing.SystemColors.Control
        Me.tabDashboard.Controls.Add(Me.btnStartSuite)
        Me.tabDashboard.Controls.Add(Me.btnStopSuite)
        Me.tabDashboard.Controls.Add(Me.btnRestartSuite)
        Me.tabDashboard.Controls.Add(Me.btnOpenBrowser)
        Me.tabDashboard.Controls.Add(Me.btnCheckUpdates)
        Me.tabDashboard.Controls.Add(Me.pnlUpdateBanner)
        Me.tabDashboard.Controls.Add(Me.rtbDashLog)
        Me.tabDashboard.Controls.Add(Me.pnlResourcesCard)
        Me.tabDashboard.Controls.Add(Me.pnlOllamaCard)
        Me.tabDashboard.Controls.Add(Me.pnlStatusCard)
        Me.tabDashboard.Location = New System.Drawing.Point(4, 24)
        Me.tabDashboard.Name = "tabDashboard"
        Me.tabDashboard.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDashboard.Size = New System.Drawing.Size(876, 490)
        Me.tabDashboard.TabIndex = 0
        Me.tabDashboard.Text = "Dashboard"
        '
        'btnStartSuite
        '
        Me.btnStartSuite.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnStartSuite.Location = New System.Drawing.Point(8, 201)
        Me.btnStartSuite.Name = "btnStartSuite"
        Me.btnStartSuite.Size = New System.Drawing.Size(130, 23)
        Me.btnStartSuite.TabIndex = 10
        Me.btnStartSuite.Text = "▶  Avvia"
        Me.btnStartSuite.UseVisualStyleBackColor = True
        '
        'btnStopSuite
        '
        Me.btnStopSuite.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnStopSuite.Location = New System.Drawing.Point(145, 201)
        Me.btnStopSuite.Name = "btnStopSuite"
        Me.btnStopSuite.Size = New System.Drawing.Size(130, 23)
        Me.btnStopSuite.TabIndex = 11
        Me.btnStopSuite.Text = "■  Arresta"
        Me.btnStopSuite.UseVisualStyleBackColor = True
        '
        'btnRestartSuite
        '
        Me.btnRestartSuite.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnRestartSuite.Location = New System.Drawing.Point(281, 201)
        Me.btnRestartSuite.Name = "btnRestartSuite"
        Me.btnRestartSuite.Size = New System.Drawing.Size(130, 23)
        Me.btnRestartSuite.TabIndex = 12
        Me.btnRestartSuite.Text = "↺  Riavvia"
        Me.btnRestartSuite.UseVisualStyleBackColor = True
        '
        'btnOpenBrowser
        '
        Me.btnOpenBrowser.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnOpenBrowser.Location = New System.Drawing.Point(417, 201)
        Me.btnOpenBrowser.Name = "btnOpenBrowser"
        Me.btnOpenBrowser.Size = New System.Drawing.Size(130, 23)
        Me.btnOpenBrowser.TabIndex = 3
        Me.btnOpenBrowser.Text = "Apri nel browser"
        Me.btnOpenBrowser.UseVisualStyleBackColor = True
        '
        'btnCheckUpdates
        '
        Me.btnCheckUpdates.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnCheckUpdates.Location = New System.Drawing.Point(553, 201)
        Me.btnCheckUpdates.Name = "btnCheckUpdates"
        Me.btnCheckUpdates.Size = New System.Drawing.Size(212, 23)
        Me.btnCheckUpdates.TabIndex = 13
        Me.btnCheckUpdates.Text = "🚀  Verifica aggiornamenti"
        Me.btnCheckUpdates.UseVisualStyleBackColor = True
        '
        'pnlUpdateBanner
        '
        Me.pnlUpdateBanner.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlUpdateBanner.BackColor = System.Drawing.Color.LightGreen
        Me.pnlUpdateBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlUpdateBanner.Controls.Add(Me.lnkChangelogQuick)
        Me.pnlUpdateBanner.Controls.Add(Me.btnUpdateNowQuick)
        Me.pnlUpdateBanner.Controls.Add(Me.lblUpdateBannerText)
        Me.pnlUpdateBanner.Location = New System.Drawing.Point(8, 164)
        Me.pnlUpdateBanner.Name = "pnlUpdateBanner"
        Me.pnlUpdateBanner.Size = New System.Drawing.Size(860, 31)
        Me.pnlUpdateBanner.TabIndex = 15
        '
        'lnkChangelogQuick
        '
        Me.lnkChangelogQuick.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkChangelogQuick.AutoSize = True
        Me.lnkChangelogQuick.LinkColor = System.Drawing.SystemColors.ControlText
        Me.lnkChangelogQuick.Location = New System.Drawing.Point(762, 6)
        Me.lnkChangelogQuick.Name = "lnkChangelogQuick"
        Me.lnkChangelogQuick.Size = New System.Drawing.Size(93, 15)
        Me.lnkChangelogQuick.TabIndex = 2
        Me.lnkChangelogQuick.TabStop = True
        Me.lnkChangelogQuick.Text = "📖 Changelog →"
        '
        'btnUpdateNowQuick
        '
        Me.btnUpdateNowQuick.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateNowQuick.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateNowQuick.Location = New System.Drawing.Point(646, 3)
        Me.btnUpdateNowQuick.Name = "btnUpdateNowQuick"
        Me.btnUpdateNowQuick.Size = New System.Drawing.Size(110, 23)
        Me.btnUpdateNowQuick.TabIndex = 1
        Me.btnUpdateNowQuick.Text = "🚀  Aggiorna"
        Me.btnUpdateNowQuick.UseVisualStyleBackColor = True
        Me.btnUpdateNowQuick.Visible = False
        '
        'lblUpdateBannerText
        '
        Me.lblUpdateBannerText.AutoSize = True
        Me.lblUpdateBannerText.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUpdateBannerText.Location = New System.Drawing.Point(8, 6)
        Me.lblUpdateBannerText.Name = "lblUpdateBannerText"
        Me.lblUpdateBannerText.Size = New System.Drawing.Size(209, 15)
        Me.lblUpdateBannerText.TabIndex = 0
        Me.lblUpdateBannerText.Text = "Verifica stato aggiornamenti in corso..."
        '
        'rtbDashLog
        '
        Me.rtbDashLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbDashLog.BackColor = System.Drawing.Color.Black
        Me.rtbDashLog.Font = New System.Drawing.Font("Consolas", 8.0!)
        Me.rtbDashLog.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.rtbDashLog.Location = New System.Drawing.Point(8, 230)
        Me.rtbDashLog.Name = "rtbDashLog"
        Me.rtbDashLog.ReadOnly = True
        Me.rtbDashLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbDashLog.Size = New System.Drawing.Size(860, 254)
        Me.rtbDashLog.TabIndex = 6
        Me.rtbDashLog.Text = ""
        '
        'pnlResourcesCard
        '
        Me.pnlResourcesCard.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlResourcesCard.BackColor = System.Drawing.SystemColors.Control
        Me.pnlResourcesCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlResourcesCard.Controls.Add(Me.lblRestartValue)
        Me.pnlResourcesCard.Controls.Add(Me.lblRestartLabel)
        Me.pnlResourcesCard.Controls.Add(Me.lblRamValue)
        Me.pnlResourcesCard.Controls.Add(Me.lblRamLabel)
        Me.pnlResourcesCard.Controls.Add(Me.pgbCpu)
        Me.pnlResourcesCard.Controls.Add(Me.lblCpuValue)
        Me.pnlResourcesCard.Controls.Add(Me.lblCpuLabel)
        Me.pnlResourcesCard.Controls.Add(Me.lblResourcesHeader)
        Me.pnlResourcesCard.Controls.Add(Me.pgbRam)
        Me.pnlResourcesCard.Location = New System.Drawing.Point(574, 8)
        Me.pnlResourcesCard.Name = "pnlResourcesCard"
        Me.pnlResourcesCard.Size = New System.Drawing.Size(294, 145)
        Me.pnlResourcesCard.TabIndex = 1
        '
        'lblRestartValue
        '
        Me.lblRestartValue.AutoSize = True
        Me.lblRestartValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRestartValue.Location = New System.Drawing.Point(75, 96)
        Me.lblRestartValue.Name = "lblRestartValue"
        Me.lblRestartValue.Size = New System.Drawing.Size(13, 15)
        Me.lblRestartValue.TabIndex = 8
        Me.lblRestartValue.Text = "0"
        '
        'lblRestartLabel
        '
        Me.lblRestartLabel.AutoSize = True
        Me.lblRestartLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblRestartLabel.Location = New System.Drawing.Point(3, 96)
        Me.lblRestartLabel.Name = "lblRestartLabel"
        Me.lblRestartLabel.Size = New System.Drawing.Size(66, 15)
        Me.lblRestartLabel.TabIndex = 7
        Me.lblRestartLabel.Text = "Riavvii WD:"
        '
        'lblRamValue
        '
        Me.lblRamValue.AutoSize = True
        Me.lblRamValue.BackColor = System.Drawing.Color.Transparent
        Me.lblRamValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRamValue.Location = New System.Drawing.Point(75, 75)
        Me.lblRamValue.Name = "lblRamValue"
        Me.lblRamValue.Size = New System.Drawing.Size(19, 15)
        Me.lblRamValue.TabIndex = 5
        Me.lblRamValue.Text = "—"
        '
        'lblRamLabel
        '
        Me.lblRamLabel.AutoSize = True
        Me.lblRamLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblRamLabel.Location = New System.Drawing.Point(3, 75)
        Me.lblRamLabel.Name = "lblRamLabel"
        Me.lblRamLabel.Size = New System.Drawing.Size(36, 15)
        Me.lblRamLabel.TabIndex = 4
        Me.lblRamLabel.Text = "RAM:"
        '
        'pgbCpu
        '
        Me.pgbCpu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbCpu.Location = New System.Drawing.Point(132, 55)
        Me.pgbCpu.Name = "pgbCpu"
        Me.pgbCpu.Size = New System.Drawing.Size(157, 15)
        Me.pgbCpu.TabIndex = 3
        '
        'lblCpuValue
        '
        Me.lblCpuValue.AutoSize = True
        Me.lblCpuValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCpuValue.Location = New System.Drawing.Point(75, 55)
        Me.lblCpuValue.Name = "lblCpuValue"
        Me.lblCpuValue.Size = New System.Drawing.Size(19, 15)
        Me.lblCpuValue.TabIndex = 2
        Me.lblCpuValue.Text = "—"
        '
        'lblCpuLabel
        '
        Me.lblCpuLabel.AutoSize = True
        Me.lblCpuLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblCpuLabel.Location = New System.Drawing.Point(3, 55)
        Me.lblCpuLabel.Name = "lblCpuLabel"
        Me.lblCpuLabel.Size = New System.Drawing.Size(33, 15)
        Me.lblCpuLabel.TabIndex = 1
        Me.lblCpuLabel.Text = "CPU:"
        '
        'lblResourcesHeader
        '
        Me.lblResourcesHeader.AutoSize = True
        Me.lblResourcesHeader.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblResourcesHeader.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblResourcesHeader.Location = New System.Drawing.Point(3, 4)
        Me.lblResourcesHeader.Name = "lblResourcesHeader"
        Me.lblResourcesHeader.Size = New System.Drawing.Size(173, 19)
        Me.lblResourcesHeader.TabIndex = 0
        Me.lblResourcesHeader.Text = "RISORSE DEL COMPUTER"
        '
        'pgbRam
        '
        Me.pgbRam.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbRam.Location = New System.Drawing.Point(132, 75)
        Me.pgbRam.Maximum = 1000
        Me.pgbRam.Name = "pgbRam"
        Me.pgbRam.Size = New System.Drawing.Size(157, 15)
        Me.pgbRam.TabIndex = 6
        '
        'pnlOllamaCard
        '
        Me.pnlOllamaCard.BackColor = System.Drawing.SystemColors.Control
        Me.pnlOllamaCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlOllamaCard.Controls.Add(Me.btnRestartOllamaQuick)
        Me.pnlOllamaCard.Controls.Add(Me.btnStopOllamaQuick)
        Me.pnlOllamaCard.Controls.Add(Me.btnStartOllamaQuick)
        Me.pnlOllamaCard.Controls.Add(Me.lblOllamaOptValue)
        Me.pnlOllamaCard.Controls.Add(Me.lblOllamaOptLabel)
        Me.pnlOllamaCard.Controls.Add(Me.lblOllamaPortValue)
        Me.pnlOllamaCard.Controls.Add(Me.lblOllamaPortLabel)
        Me.pnlOllamaCard.Controls.Add(Me.lblOllamaModelValue)
        Me.pnlOllamaCard.Controls.Add(Me.lblOllamaModelLabel)
        Me.pnlOllamaCard.Controls.Add(Me.lblOllamaStatusValue)
        Me.pnlOllamaCard.Controls.Add(Me.lblOllamaHeader)
        Me.pnlOllamaCard.Location = New System.Drawing.Point(291, 8)
        Me.pnlOllamaCard.Name = "pnlOllamaCard"
        Me.pnlOllamaCard.Size = New System.Drawing.Size(275, 145)
        Me.pnlOllamaCard.TabIndex = 14
        '
        'btnRestartOllamaQuick
        '
        Me.btnRestartOllamaQuick.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnRestartOllamaQuick.Location = New System.Drawing.Point(202, 4)
        Me.btnRestartOllamaQuick.Name = "btnRestartOllamaQuick"
        Me.btnRestartOllamaQuick.Size = New System.Drawing.Size(31, 23)
        Me.btnRestartOllamaQuick.TabIndex = 10
        Me.btnRestartOllamaQuick.Text = "↺"
        Me.btnRestartOllamaQuick.UseVisualStyleBackColor = True
        '
        'btnStopOllamaQuick
        '
        Me.btnStopOllamaQuick.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnStopOllamaQuick.Location = New System.Drawing.Point(239, 4)
        Me.btnStopOllamaQuick.Name = "btnStopOllamaQuick"
        Me.btnStopOllamaQuick.Size = New System.Drawing.Size(31, 23)
        Me.btnStopOllamaQuick.TabIndex = 9
        Me.btnStopOllamaQuick.Text = "■"
        Me.btnStopOllamaQuick.UseVisualStyleBackColor = True
        '
        'btnStartOllamaQuick
        '
        Me.btnStartOllamaQuick.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartOllamaQuick.Location = New System.Drawing.Point(165, 4)
        Me.btnStartOllamaQuick.Name = "btnStartOllamaQuick"
        Me.btnStartOllamaQuick.Size = New System.Drawing.Size(31, 23)
        Me.btnStartOllamaQuick.TabIndex = 8
        Me.btnStartOllamaQuick.Text = "▶"
        Me.btnStartOllamaQuick.UseVisualStyleBackColor = True
        '
        'lblOllamaOptValue
        '
        Me.lblOllamaOptValue.AutoSize = True
        Me.lblOllamaOptValue.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblOllamaOptValue.Location = New System.Drawing.Point(70, 96)
        Me.lblOllamaOptValue.Name = "lblOllamaOptValue"
        Me.lblOllamaOptValue.Size = New System.Drawing.Size(113, 15)
        Me.lblOllamaOptValue.TabIndex = 7
        Me.lblOllamaOptValue.Text = "⚡ AVX2 Ottimizzato"
        '
        'lblOllamaOptLabel
        '
        Me.lblOllamaOptLabel.AutoSize = True
        Me.lblOllamaOptLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblOllamaOptLabel.Location = New System.Drawing.Point(3, 96)
        Me.lblOllamaOptLabel.Name = "lblOllamaOptLabel"
        Me.lblOllamaOptLabel.Size = New System.Drawing.Size(61, 15)
        Me.lblOllamaOptLabel.TabIndex = 6
        Me.lblOllamaOptLabel.Text = "Hardware:"
        '
        'lblOllamaPortValue
        '
        Me.lblOllamaPortValue.AutoSize = True
        Me.lblOllamaPortValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOllamaPortValue.Location = New System.Drawing.Point(70, 75)
        Me.lblOllamaPortValue.Name = "lblOllamaPortValue"
        Me.lblOllamaPortValue.Size = New System.Drawing.Size(37, 15)
        Me.lblOllamaPortValue.TabIndex = 5
        Me.lblOllamaPortValue.Text = "11434"
        '
        'lblOllamaPortLabel
        '
        Me.lblOllamaPortLabel.AutoSize = True
        Me.lblOllamaPortLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblOllamaPortLabel.Location = New System.Drawing.Point(3, 75)
        Me.lblOllamaPortLabel.Name = "lblOllamaPortLabel"
        Me.lblOllamaPortLabel.Size = New System.Drawing.Size(38, 15)
        Me.lblOllamaPortLabel.TabIndex = 4
        Me.lblOllamaPortLabel.Text = "Porta:"
        '
        'lblOllamaModelValue
        '
        Me.lblOllamaModelValue.AutoSize = True
        Me.lblOllamaModelValue.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblOllamaModelValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOllamaModelValue.Location = New System.Drawing.Point(70, 55)
        Me.lblOllamaModelValue.Name = "lblOllamaModelValue"
        Me.lblOllamaModelValue.Size = New System.Drawing.Size(60, 15)
        Me.lblOllamaModelValue.TabIndex = 3
        Me.lblOllamaModelValue.Text = "chat-light"
        '
        'lblOllamaModelLabel
        '
        Me.lblOllamaModelLabel.AutoSize = True
        Me.lblOllamaModelLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblOllamaModelLabel.Location = New System.Drawing.Point(3, 55)
        Me.lblOllamaModelLabel.Name = "lblOllamaModelLabel"
        Me.lblOllamaModelLabel.Size = New System.Drawing.Size(54, 15)
        Me.lblOllamaModelLabel.TabIndex = 2
        Me.lblOllamaModelLabel.Text = "Modello:"
        '
        'lblOllamaStatusValue
        '
        Me.lblOllamaStatusValue.AutoSize = True
        Me.lblOllamaStatusValue.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblOllamaStatusValue.ForeColor = System.Drawing.Color.Orange
        Me.lblOllamaStatusValue.Location = New System.Drawing.Point(2, 30)
        Me.lblOllamaStatusValue.Name = "lblOllamaStatusValue"
        Me.lblOllamaStatusValue.Size = New System.Drawing.Size(149, 21)
        Me.lblOllamaStatusValue.TabIndex = 1
        Me.lblOllamaStatusValue.Text = "AVVIO IN CORSO..."
        '
        'lblOllamaHeader
        '
        Me.lblOllamaHeader.AutoSize = True
        Me.lblOllamaHeader.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblOllamaHeader.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOllamaHeader.Location = New System.Drawing.Point(3, 4)
        Me.lblOllamaHeader.Name = "lblOllamaHeader"
        Me.lblOllamaHeader.Size = New System.Drawing.Size(67, 19)
        Me.lblOllamaHeader.TabIndex = 0
        Me.lblOllamaHeader.Text = "OLLAMA"
        '
        'pnlStatusCard
        '
        Me.pnlStatusCard.BackColor = System.Drawing.SystemColors.Control
        Me.pnlStatusCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStatusCard.Controls.Add(Me.lnkOpenBrowser)
        Me.pnlStatusCard.Controls.Add(Me.lblPortValue)
        Me.pnlStatusCard.Controls.Add(Me.lblPortLabel)
        Me.pnlStatusCard.Controls.Add(Me.lblUptimeValue)
        Me.pnlStatusCard.Controls.Add(Me.lblUptimeLabel)
        Me.pnlStatusCard.Controls.Add(Me.lblPidValue)
        Me.pnlStatusCard.Controls.Add(Me.lblPidLabel)
        Me.pnlStatusCard.Controls.Add(Me.btnStartNodeRed)
        Me.pnlStatusCard.Controls.Add(Me.btnRestartNodeRed)
        Me.pnlStatusCard.Controls.Add(Me.lblNodeRedStatusValue)
        Me.pnlStatusCard.Controls.Add(Me.btnStopNodeRed)
        Me.pnlStatusCard.Controls.Add(Me.lblStatusHeader)
        Me.pnlStatusCard.Controls.Add(Me.lblUrlLabel)
        Me.pnlStatusCard.Controls.Add(Me.lnkNodeRedUrl)
        Me.pnlStatusCard.Location = New System.Drawing.Point(8, 8)
        Me.pnlStatusCard.Name = "pnlStatusCard"
        Me.pnlStatusCard.Size = New System.Drawing.Size(275, 145)
        Me.pnlStatusCard.TabIndex = 0
        '
        'lnkOpenBrowser
        '
        Me.lnkOpenBrowser.AutoSize = True
        Me.lnkOpenBrowser.LinkColor = System.Drawing.SystemColors.Control
        Me.lnkOpenBrowser.Location = New System.Drawing.Point(15, 137)
        Me.lnkOpenBrowser.Name = "lnkOpenBrowser"
        Me.lnkOpenBrowser.Size = New System.Drawing.Size(76, 15)
        Me.lnkOpenBrowser.TabIndex = 8
        Me.lnkOpenBrowser.TabStop = True
        Me.lnkOpenBrowser.Text = "Apri Editor →"
        '
        'lblPortValue
        '
        Me.lblPortValue.AutoSize = True
        Me.lblPortValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPortValue.Location = New System.Drawing.Point(70, 96)
        Me.lblPortValue.Name = "lblPortValue"
        Me.lblPortValue.Size = New System.Drawing.Size(31, 15)
        Me.lblPortValue.TabIndex = 7
        Me.lblPortValue.Text = "1880"
        '
        'lblPortLabel
        '
        Me.lblPortLabel.AutoSize = True
        Me.lblPortLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblPortLabel.Location = New System.Drawing.Point(3, 96)
        Me.lblPortLabel.Name = "lblPortLabel"
        Me.lblPortLabel.Size = New System.Drawing.Size(38, 15)
        Me.lblPortLabel.TabIndex = 6
        Me.lblPortLabel.Text = "Porta:"
        '
        'lblUptimeValue
        '
        Me.lblUptimeValue.AutoSize = True
        Me.lblUptimeValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUptimeValue.Location = New System.Drawing.Point(70, 75)
        Me.lblUptimeValue.Name = "lblUptimeValue"
        Me.lblUptimeValue.Size = New System.Drawing.Size(19, 15)
        Me.lblUptimeValue.TabIndex = 5
        Me.lblUptimeValue.Text = "—"
        '
        'lblUptimeLabel
        '
        Me.lblUptimeLabel.AutoSize = True
        Me.lblUptimeLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblUptimeLabel.Location = New System.Drawing.Point(3, 75)
        Me.lblUptimeLabel.Name = "lblUptimeLabel"
        Me.lblUptimeLabel.Size = New System.Drawing.Size(49, 15)
        Me.lblUptimeLabel.TabIndex = 4
        Me.lblUptimeLabel.Text = "Uptime:"
        '
        'lblPidValue
        '
        Me.lblPidValue.AutoSize = True
        Me.lblPidValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPidValue.Location = New System.Drawing.Point(70, 55)
        Me.lblPidValue.Name = "lblPidValue"
        Me.lblPidValue.Size = New System.Drawing.Size(19, 15)
        Me.lblPidValue.TabIndex = 3
        Me.lblPidValue.Text = "—"
        '
        'lblPidLabel
        '
        Me.lblPidLabel.AutoSize = True
        Me.lblPidLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblPidLabel.Location = New System.Drawing.Point(3, 55)
        Me.lblPidLabel.Name = "lblPidLabel"
        Me.lblPidLabel.Size = New System.Drawing.Size(28, 15)
        Me.lblPidLabel.TabIndex = 2
        Me.lblPidLabel.Text = "PID:"
        '
        'btnStartNodeRed
        '
        Me.btnStartNodeRed.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnStartNodeRed.Location = New System.Drawing.Point(161, 3)
        Me.btnStartNodeRed.Name = "btnStartNodeRed"
        Me.btnStartNodeRed.Size = New System.Drawing.Size(31, 23)
        Me.btnStartNodeRed.TabIndex = 0
        Me.btnStartNodeRed.Text = "▶"
        Me.btnStartNodeRed.UseVisualStyleBackColor = True
        '
        'btnRestartNodeRed
        '
        Me.btnRestartNodeRed.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnRestartNodeRed.Location = New System.Drawing.Point(198, 3)
        Me.btnRestartNodeRed.Name = "btnRestartNodeRed"
        Me.btnRestartNodeRed.Size = New System.Drawing.Size(31, 23)
        Me.btnRestartNodeRed.TabIndex = 2
        Me.btnRestartNodeRed.Text = "↺"
        Me.btnRestartNodeRed.UseVisualStyleBackColor = True
        '
        'lblNodeRedStatusValue
        '
        Me.lblNodeRedStatusValue.AutoSize = True
        Me.lblNodeRedStatusValue.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNodeRedStatusValue.ForeColor = System.Drawing.Color.Orange
        Me.lblNodeRedStatusValue.Location = New System.Drawing.Point(3, 29)
        Me.lblNodeRedStatusValue.Name = "lblNodeRedStatusValue"
        Me.lblNodeRedStatusValue.Size = New System.Drawing.Size(149, 21)
        Me.lblNodeRedStatusValue.TabIndex = 1
        Me.lblNodeRedStatusValue.Text = "AVVIO IN CORSO..."
        '
        'btnStopNodeRed
        '
        Me.btnStopNodeRed.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnStopNodeRed.Location = New System.Drawing.Point(235, 3)
        Me.btnStopNodeRed.Name = "btnStopNodeRed"
        Me.btnStopNodeRed.Size = New System.Drawing.Size(31, 23)
        Me.btnStopNodeRed.TabIndex = 1
        Me.btnStopNodeRed.Text = "■"
        Me.btnStopNodeRed.UseVisualStyleBackColor = True
        '
        'lblStatusHeader
        '
        Me.lblStatusHeader.AutoSize = True
        Me.lblStatusHeader.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusHeader.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatusHeader.Location = New System.Drawing.Point(3, 4)
        Me.lblStatusHeader.Name = "lblStatusHeader"
        Me.lblStatusHeader.Size = New System.Drawing.Size(80, 19)
        Me.lblStatusHeader.TabIndex = 0
        Me.lblStatusHeader.Text = "NODE-RED"
        '
        'lblUrlLabel
        '
        Me.lblUrlLabel.AutoSize = True
        Me.lblUrlLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblUrlLabel.Location = New System.Drawing.Point(3, 118)
        Me.lblUrlLabel.Name = "lblUrlLabel"
        Me.lblUrlLabel.Size = New System.Drawing.Size(61, 15)
        Me.lblUrlLabel.TabIndex = 3
        Me.lblUrlLabel.Text = "Node-red:"
        '
        'lnkNodeRedUrl
        '
        Me.lnkNodeRedUrl.AutoSize = True
        Me.lnkNodeRedUrl.LinkColor = System.Drawing.Color.DodgerBlue
        Me.lnkNodeRedUrl.Location = New System.Drawing.Point(70, 118)
        Me.lnkNodeRedUrl.Name = "lnkNodeRedUrl"
        Me.lnkNodeRedUrl.Size = New System.Drawing.Size(114, 15)
        Me.lnkNodeRedUrl.TabIndex = 4
        Me.lnkNodeRedUrl.TabStop = True
        Me.lnkNodeRedUrl.Text = "http://127.0.0.1:1880"
        '
        'tabOllama
        '
        Me.tabOllama.BackColor = System.Drawing.SystemColors.Control
        Me.tabOllama.Controls.Add(Me.grpTestChat)
        Me.tabOllama.Controls.Add(Me.grpOllamaOpt)
        Me.tabOllama.Controls.Add(Me.grpOllamaModels)
        Me.tabOllama.Controls.Add(Me.grpOllamaService)
        Me.tabOllama.Controls.Add(Me.lblOllamaSubtitle)
        Me.tabOllama.Controls.Add(Me.lblOllamaTitle)
        Me.tabOllama.Location = New System.Drawing.Point(4, 24)
        Me.tabOllama.Name = "tabOllama"
        Me.tabOllama.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOllama.Size = New System.Drawing.Size(876, 490)
        Me.tabOllama.TabIndex = 6
        Me.tabOllama.Text = "Ollama & AI"
        '
        'grpTestChat
        '
        Me.grpTestChat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTestChat.Controls.Add(Me.lblTestStats)
        Me.grpTestChat.Controls.Add(Me.txtTestReply)
        Me.grpTestChat.Controls.Add(Me.btnTestChat)
        Me.grpTestChat.Controls.Add(Me.txtTestPrompt)
        Me.grpTestChat.Controls.Add(Me.lblTestPrompt)
        Me.grpTestChat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpTestChat.Location = New System.Drawing.Point(6, 255)
        Me.grpTestChat.Name = "grpTestChat"
        Me.grpTestChat.Size = New System.Drawing.Size(862, 232)
        Me.grpTestChat.TabIndex = 5
        Me.grpTestChat.TabStop = False
        Me.grpTestChat.Text = "Console benchmark test chat (verifica risposta e token/s)"
        '
        'lblTestStats
        '
        Me.lblTestStats.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTestStats.AutoSize = True
        Me.lblTestStats.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTestStats.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTestStats.Location = New System.Drawing.Point(6, 219)
        Me.lblTestStats.Name = "lblTestStats"
        Me.lblTestStats.Size = New System.Drawing.Size(287, 13)
        Me.lblTestStats.TabIndex = 4
        Me.lblTestStats.Text = "Latenza: — | Generazione: — token/s | Token totali: —"
        '
        'txtTestReply
        '
        Me.txtTestReply.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTestReply.BackColor = System.Drawing.Color.Black
        Me.txtTestReply.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtTestReply.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.txtTestReply.Location = New System.Drawing.Point(6, 51)
        Me.txtTestReply.Name = "txtTestReply"
        Me.txtTestReply.ReadOnly = True
        Me.txtTestReply.Size = New System.Drawing.Size(850, 165)
        Me.txtTestReply.TabIndex = 3
        Me.txtTestReply.Text = ""
        '
        'btnTestChat
        '
        Me.btnTestChat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTestChat.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnTestChat.Location = New System.Drawing.Point(738, 22)
        Me.btnTestChat.Name = "btnTestChat"
        Me.btnTestChat.Size = New System.Drawing.Size(118, 23)
        Me.btnTestChat.TabIndex = 2
        Me.btnTestChat.Text = "💬 Invia AI"
        Me.btnTestChat.UseVisualStyleBackColor = True
        '
        'txtTestPrompt
        '
        Me.txtTestPrompt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTestPrompt.BackColor = System.Drawing.SystemColors.Window
        Me.txtTestPrompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestPrompt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTestPrompt.Location = New System.Drawing.Point(97, 22)
        Me.txtTestPrompt.Name = "txtTestPrompt"
        Me.txtTestPrompt.Size = New System.Drawing.Size(635, 23)
        Me.txtTestPrompt.TabIndex = 1
        Me.txtTestPrompt.Text = "Chi sei e in cosa puoi aiutarmi?"
        '
        'lblTestPrompt
        '
        Me.lblTestPrompt.AutoSize = True
        Me.lblTestPrompt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTestPrompt.Location = New System.Drawing.Point(6, 24)
        Me.lblTestPrompt.Name = "lblTestPrompt"
        Me.lblTestPrompt.Size = New System.Drawing.Size(85, 15)
        Me.lblTestPrompt.TabIndex = 0
        Me.lblTestPrompt.Text = "Prompt di test:"
        '
        'grpOllamaOpt
        '
        Me.grpOllamaOpt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpOllamaOpt.Controls.Add(Me.btnApplyOptimizations)
        Me.grpOllamaOpt.Controls.Add(Me.lblOptDesc)
        Me.grpOllamaOpt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpOllamaOpt.Location = New System.Drawing.Point(6, 153)
        Me.grpOllamaOpt.Name = "grpOllamaOpt"
        Me.grpOllamaOpt.Size = New System.Drawing.Size(862, 96)
        Me.grpOllamaOpt.TabIndex = 4
        Me.grpOllamaOpt.TabStop = False
        Me.grpOllamaOpt.Text = "Ottimizzazione Hardware Windows CPU (AVX2)"
        '
        'btnApplyOptimizations
        '
        Me.btnApplyOptimizations.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApplyOptimizations.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnApplyOptimizations.Location = New System.Drawing.Point(6, 67)
        Me.btnApplyOptimizations.Name = "btnApplyOptimizations"
        Me.btnApplyOptimizations.Size = New System.Drawing.Size(850, 23)
        Me.btnApplyOptimizations.TabIndex = 1
        Me.btnApplyOptimizations.Text = "⚡ Applica ottimizzazioni Windows (1-Click)"
        Me.btnApplyOptimizations.UseVisualStyleBackColor = True
        '
        'lblOptDesc
        '
        Me.lblOptDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOptDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOptDesc.Location = New System.Drawing.Point(15, 22)
        Me.lblOptDesc.Name = "lblOptDesc"
        Me.lblOptDesc.Size = New System.Drawing.Size(841, 42)
        Me.lblOptDesc.TabIndex = 0
        Me.lblOptDesc.Text = "Variabili d'ambiente Windows applicate: OLLAMA_NUM_PARALLEL = 1, KEEP_ALIVE = 24h" &
    ", FLASH_ATTENTION = 1, MAX_LOADED_MODELS = 1."
        '
        'grpOllamaModels
        '
        Me.grpOllamaModels.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpOllamaModels.Controls.Add(Me.btnPullModelMini)
        Me.grpOllamaModels.Controls.Add(Me.btnPullModelLight)
        Me.grpOllamaModels.Controls.Add(Me.btnRefreshModels)
        Me.grpOllamaModels.Controls.Add(Me.cmbOllamaModels)
        Me.grpOllamaModels.Controls.Add(Me.lblSelectModel)
        Me.grpOllamaModels.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpOllamaModels.Location = New System.Drawing.Point(6, 91)
        Me.grpOllamaModels.Name = "grpOllamaModels"
        Me.grpOllamaModels.Size = New System.Drawing.Size(864, 56)
        Me.grpOllamaModels.TabIndex = 3
        Me.grpOllamaModels.TabStop = False
        Me.grpOllamaModels.Text = "Gestione modelli AI locali"
        '
        'btnPullModelMini
        '
        Me.btnPullModelMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPullModelMini.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnPullModelMini.Location = New System.Drawing.Point(678, 22)
        Me.btnPullModelMini.Name = "btnPullModelMini"
        Me.btnPullModelMini.Size = New System.Drawing.Size(180, 23)
        Me.btnPullModelMini.TabIndex = 4
        Me.btnPullModelMini.Text = "🚀 Scarica chat-mini (0.5B)"
        Me.btnPullModelMini.UseVisualStyleBackColor = True
        '
        'btnPullModelLight
        '
        Me.btnPullModelLight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPullModelLight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnPullModelLight.Location = New System.Drawing.Point(486, 22)
        Me.btnPullModelLight.Name = "btnPullModelLight"
        Me.btnPullModelLight.Size = New System.Drawing.Size(186, 23)
        Me.btnPullModelLight.TabIndex = 3
        Me.btnPullModelLight.Text = "⚡ Scarica chat-light (1.5B)"
        Me.btnPullModelLight.UseVisualStyleBackColor = True
        '
        'btnRefreshModels
        '
        Me.btnRefreshModels.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefreshModels.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnRefreshModels.Location = New System.Drawing.Point(360, 22)
        Me.btnRefreshModels.Name = "btnRefreshModels"
        Me.btnRefreshModels.Size = New System.Drawing.Size(120, 23)
        Me.btnRefreshModels.TabIndex = 2
        Me.btnRefreshModels.Text = "↺ Ricarica"
        Me.btnRefreshModels.UseVisualStyleBackColor = True
        '
        'cmbOllamaModels
        '
        Me.cmbOllamaModels.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbOllamaModels.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOllamaModels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOllamaModels.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbOllamaModels.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOllamaModels.FormattingEnabled = True
        Me.cmbOllamaModels.Location = New System.Drawing.Point(101, 22)
        Me.cmbOllamaModels.Name = "cmbOllamaModels"
        Me.cmbOllamaModels.Size = New System.Drawing.Size(253, 23)
        Me.cmbOllamaModels.TabIndex = 1
        '
        'lblSelectModel
        '
        Me.lblSelectModel.AutoSize = True
        Me.lblSelectModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSelectModel.Location = New System.Drawing.Point(6, 25)
        Me.lblSelectModel.Name = "lblSelectModel"
        Me.lblSelectModel.Size = New System.Drawing.Size(89, 15)
        Me.lblSelectModel.TabIndex = 0
        Me.lblSelectModel.Text = "Modello Attivo:"
        '
        'grpOllamaService
        '
        Me.grpOllamaService.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpOllamaService.Controls.Add(Me.btnRestartOllama)
        Me.grpOllamaService.Controls.Add(Me.btnStopOllama)
        Me.grpOllamaService.Controls.Add(Me.btnStartOllama)
        Me.grpOllamaService.Controls.Add(Me.lblOllamaSvcPort)
        Me.grpOllamaService.Controls.Add(Me.lblOllamaSvcStatus)
        Me.grpOllamaService.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpOllamaService.Location = New System.Drawing.Point(6, 29)
        Me.grpOllamaService.Name = "grpOllamaService"
        Me.grpOllamaService.Size = New System.Drawing.Size(864, 56)
        Me.grpOllamaService.TabIndex = 2
        Me.grpOllamaService.TabStop = False
        Me.grpOllamaService.Text = "Controllo servizio Ollama"
        '
        'btnRestartOllama
        '
        Me.btnRestartOllama.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRestartOllama.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestartOllama.Location = New System.Drawing.Point(738, 22)
        Me.btnRestartOllama.Name = "btnRestartOllama"
        Me.btnRestartOllama.Size = New System.Drawing.Size(120, 23)
        Me.btnRestartOllama.TabIndex = 4
        Me.btnRestartOllama.Text = "↺  Riavvia"
        Me.btnRestartOllama.UseVisualStyleBackColor = True
        '
        'btnStopOllama
        '
        Me.btnStopOllama.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStopOllama.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStopOllama.Location = New System.Drawing.Point(612, 22)
        Me.btnStopOllama.Name = "btnStopOllama"
        Me.btnStopOllama.Size = New System.Drawing.Size(120, 23)
        Me.btnStopOllama.TabIndex = 3
        Me.btnStopOllama.Text = "■  Ferma"
        Me.btnStopOllama.UseVisualStyleBackColor = True
        '
        'btnStartOllama
        '
        Me.btnStartOllama.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStartOllama.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartOllama.Location = New System.Drawing.Point(486, 22)
        Me.btnStartOllama.Name = "btnStartOllama"
        Me.btnStartOllama.Size = New System.Drawing.Size(120, 23)
        Me.btnStartOllama.TabIndex = 2
        Me.btnStartOllama.Text = "▶  Avvia"
        Me.btnStartOllama.UseVisualStyleBackColor = True
        '
        'lblOllamaSvcPort
        '
        Me.lblOllamaSvcPort.AutoSize = True
        Me.lblOllamaSvcPort.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOllamaSvcPort.Location = New System.Drawing.Point(213, 26)
        Me.lblOllamaSvcPort.Name = "lblOllamaSvcPort"
        Me.lblOllamaSvcPort.Size = New System.Drawing.Size(71, 15)
        Me.lblOllamaSvcPort.TabIndex = 1
        Me.lblOllamaSvcPort.Text = "Porta: 11434"
        '
        'lblOllamaSvcStatus
        '
        Me.lblOllamaSvcStatus.AutoSize = True
        Me.lblOllamaSvcStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblOllamaSvcStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOllamaSvcStatus.Location = New System.Drawing.Point(6, 26)
        Me.lblOllamaSvcStatus.Name = "lblOllamaSvcStatus"
        Me.lblOllamaSvcStatus.Size = New System.Drawing.Size(93, 15)
        Me.lblOllamaSvcStatus.TabIndex = 0
        Me.lblOllamaSvcStatus.Text = "Stato: ● FERMO"
        '
        'lblOllamaSubtitle
        '
        Me.lblOllamaSubtitle.AutoSize = True
        Me.lblOllamaSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblOllamaSubtitle.ForeColor = System.Drawing.Color.DimGray
        Me.lblOllamaSubtitle.Location = New System.Drawing.Point(66, 12)
        Me.lblOllamaSubtitle.Name = "lblOllamaSubtitle"
        Me.lblOllamaSubtitle.Size = New System.Drawing.Size(587, 13)
        Me.lblOllamaSubtitle.TabIndex = 1
        Me.lblOllamaSubtitle.Text = "Server LLM locale, accelerazione hardware CPU AVX2 e modelli ultra-leggeri ad alt" &
    "a velocità (chat-light / chat-mini)"
        '
        'lblOllamaTitle
        '
        Me.lblOllamaTitle.AutoSize = True
        Me.lblOllamaTitle.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOllamaTitle.ForeColor = System.Drawing.Color.Black
        Me.lblOllamaTitle.Location = New System.Drawing.Point(8, 9)
        Me.lblOllamaTitle.Name = "lblOllamaTitle"
        Me.lblOllamaTitle.Size = New System.Drawing.Size(52, 17)
        Me.lblOllamaTitle.TabIndex = 0
        Me.lblOllamaTitle.Text = "Ollama"
        '
        'tabEnvironment
        '
        Me.tabEnvironment.BackColor = System.Drawing.SystemColors.Control
        Me.tabEnvironment.Controls.Add(Me.rtbEnvOutput)
        Me.tabEnvironment.Controls.Add(Me.pgbEnvProgress)
        Me.tabEnvironment.Controls.Add(Me.btnSetupSuiteWizard)
        Me.tabEnvironment.Controls.Add(Me.btnUpdateNodeRed)
        Me.tabEnvironment.Controls.Add(Me.btnCheckEnvironment)
        Me.tabEnvironment.Controls.Add(Me.btnInstallOllama)
        Me.tabEnvironment.Controls.Add(Me.btnInstallNodeRed)
        Me.tabEnvironment.Controls.Add(Me.btnInstallNodeJs)
        Me.tabEnvironment.Controls.Add(Me.tlpEnvironment)
        Me.tabEnvironment.Controls.Add(Me.lblEnvSubtitle)
        Me.tabEnvironment.Controls.Add(Me.lblEnvTitle)
        Me.tabEnvironment.Location = New System.Drawing.Point(4, 24)
        Me.tabEnvironment.Name = "tabEnvironment"
        Me.tabEnvironment.Size = New System.Drawing.Size(876, 490)
        Me.tabEnvironment.TabIndex = 1
        Me.tabEnvironment.Text = "Ambiente"
        '
        'rtbEnvOutput
        '
        Me.rtbEnvOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbEnvOutput.BackColor = System.Drawing.Color.Black
        Me.rtbEnvOutput.Font = New System.Drawing.Font("Consolas", 8.0!)
        Me.rtbEnvOutput.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.rtbEnvOutput.Location = New System.Drawing.Point(6, 183)
        Me.rtbEnvOutput.Name = "rtbEnvOutput"
        Me.rtbEnvOutput.ReadOnly = True
        Me.rtbEnvOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbEnvOutput.Size = New System.Drawing.Size(864, 301)
        Me.rtbEnvOutput.TabIndex = 10
        Me.rtbEnvOutput.Text = ""
        '
        'pgbEnvProgress
        '
        Me.pgbEnvProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbEnvProgress.Location = New System.Drawing.Point(702, 154)
        Me.pgbEnvProgress.Name = "pgbEnvProgress"
        Me.pgbEnvProgress.Size = New System.Drawing.Size(168, 23)
        Me.pgbEnvProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pgbEnvProgress.TabIndex = 9
        Me.pgbEnvProgress.Visible = False
        '
        'btnSetupSuiteWizard
        '
        Me.btnSetupSuiteWizard.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnSetupSuiteWizard.Location = New System.Drawing.Point(591, 154)
        Me.btnSetupSuiteWizard.Name = "btnSetupSuiteWizard"
        Me.btnSetupSuiteWizard.Size = New System.Drawing.Size(105, 23)
        Me.btnSetupSuiteWizard.TabIndex = 8
        Me.btnSetupSuiteWizard.Text = "Setup Suite"
        Me.btnSetupSuiteWizard.UseVisualStyleBackColor = True
        '
        'btnUpdateNodeRed
        '
        Me.btnUpdateNodeRed.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnUpdateNodeRed.Location = New System.Drawing.Point(460, 154)
        Me.btnUpdateNodeRed.Name = "btnUpdateNodeRed"
        Me.btnUpdateNodeRed.Size = New System.Drawing.Size(125, 23)
        Me.btnUpdateNodeRed.TabIndex = 7
        Me.btnUpdateNodeRed.Text = "Aggiorna Node-RED"
        Me.btnUpdateNodeRed.UseVisualStyleBackColor = True
        '
        'btnCheckEnvironment
        '
        Me.btnCheckEnvironment.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnCheckEnvironment.Location = New System.Drawing.Point(359, 154)
        Me.btnCheckEnvironment.Name = "btnCheckEnvironment"
        Me.btnCheckEnvironment.Size = New System.Drawing.Size(95, 23)
        Me.btnCheckEnvironment.TabIndex = 6
        Me.btnCheckEnvironment.Text = "Verifica Stato"
        Me.btnCheckEnvironment.UseVisualStyleBackColor = True
        '
        'btnInstallOllama
        '
        Me.btnInstallOllama.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnInstallOllama.Location = New System.Drawing.Point(248, 154)
        Me.btnInstallOllama.Name = "btnInstallOllama"
        Me.btnInstallOllama.Size = New System.Drawing.Size(105, 23)
        Me.btnInstallOllama.TabIndex = 5
        Me.btnInstallOllama.Text = "Installa Ollama"
        Me.btnInstallOllama.UseVisualStyleBackColor = True
        '
        'btnInstallNodeRed
        '
        Me.btnInstallNodeRed.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnInstallNodeRed.Location = New System.Drawing.Point(122, 154)
        Me.btnInstallNodeRed.Name = "btnInstallNodeRed"
        Me.btnInstallNodeRed.Size = New System.Drawing.Size(120, 23)
        Me.btnInstallNodeRed.TabIndex = 4
        Me.btnInstallNodeRed.Text = "Installa Node-RED"
        Me.btnInstallNodeRed.UseVisualStyleBackColor = True
        '
        'btnInstallNodeJs
        '
        Me.btnInstallNodeJs.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnInstallNodeJs.Location = New System.Drawing.Point(6, 154)
        Me.btnInstallNodeJs.Name = "btnInstallNodeJs"
        Me.btnInstallNodeJs.Size = New System.Drawing.Size(110, 23)
        Me.btnInstallNodeJs.TabIndex = 3
        Me.btnInstallNodeJs.Text = "Installa Node.js"
        Me.btnInstallNodeJs.UseVisualStyleBackColor = True
        '
        'tlpEnvironment
        '
        Me.tlpEnvironment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpEnvironment.ColumnCount = 2
        Me.tlpEnvironment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpEnvironment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpEnvironment.Controls.Add(Me.grpNodeJs, 0, 0)
        Me.tlpEnvironment.Controls.Add(Me.grpNpm, 1, 0)
        Me.tlpEnvironment.Controls.Add(Me.grpNodeRed, 0, 1)
        Me.tlpEnvironment.Controls.Add(Me.grpOllama, 1, 1)
        Me.tlpEnvironment.Location = New System.Drawing.Point(6, 30)
        Me.tlpEnvironment.Name = "tlpEnvironment"
        Me.tlpEnvironment.RowCount = 2
        Me.tlpEnvironment.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpEnvironment.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpEnvironment.Size = New System.Drawing.Size(864, 118)
        Me.tlpEnvironment.TabIndex = 2
        '
        'grpNodeJs
        '
        Me.grpNodeJs.BackColor = System.Drawing.Color.Transparent
        Me.grpNodeJs.Controls.Add(Me.lblNodejsPath)
        Me.grpNodeJs.Controls.Add(Me.lblNodejsVersion)
        Me.grpNodeJs.Controls.Add(Me.lblNodejsStatusIcon)
        Me.grpNodeJs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpNodeJs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpNodeJs.Location = New System.Drawing.Point(0, 0)
        Me.grpNodeJs.Margin = New System.Windows.Forms.Padding(0, 0, 3, 2)
        Me.grpNodeJs.Name = "grpNodeJs"
        Me.grpNodeJs.Size = New System.Drawing.Size(429, 57)
        Me.grpNodeJs.TabIndex = 0
        Me.grpNodeJs.TabStop = False
        Me.grpNodeJs.Text = "Node.js (Runtime)"
        '
        'lblNodejsPath
        '
        Me.lblNodejsPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNodejsPath.AutoEllipsis = True
        Me.lblNodejsPath.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblNodejsPath.ForeColor = System.Drawing.Color.DimGray
        Me.lblNodejsPath.Location = New System.Drawing.Point(44, 36)
        Me.lblNodejsPath.Name = "lblNodejsPath"
        Me.lblNodejsPath.Size = New System.Drawing.Size(385, 15)
        Me.lblNodejsPath.TabIndex = 2
        Me.lblNodejsPath.Text = "Info..."
        '
        'lblNodejsVersion
        '
        Me.lblNodejsVersion.AutoSize = True
        Me.lblNodejsVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNodejsVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNodejsVersion.Location = New System.Drawing.Point(44, 19)
        Me.lblNodejsVersion.Name = "lblNodejsVersion"
        Me.lblNodejsVersion.Size = New System.Drawing.Size(76, 15)
        Me.lblNodejsVersion.TabIndex = 1
        Me.lblNodejsVersion.Text = "Non rilevato"
        '
        'lblNodejsStatusIcon
        '
        Me.lblNodejsStatusIcon.AutoSize = True
        Me.lblNodejsStatusIcon.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblNodejsStatusIcon.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNodejsStatusIcon.Location = New System.Drawing.Point(6, 17)
        Me.lblNodejsStatusIcon.Name = "lblNodejsStatusIcon"
        Me.lblNodejsStatusIcon.Size = New System.Drawing.Size(32, 30)
        Me.lblNodejsStatusIcon.TabIndex = 0
        Me.lblNodejsStatusIcon.Text = "○"
        '
        'grpNpm
        '
        Me.grpNpm.BackColor = System.Drawing.Color.Transparent
        Me.grpNpm.Controls.Add(Me.lblNpmPath)
        Me.grpNpm.Controls.Add(Me.lblNpmVersion)
        Me.grpNpm.Controls.Add(Me.lblNpmStatusIcon)
        Me.grpNpm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpNpm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpNpm.Location = New System.Drawing.Point(435, 0)
        Me.grpNpm.Margin = New System.Windows.Forms.Padding(3, 0, 0, 2)
        Me.grpNpm.Name = "grpNpm"
        Me.grpNpm.Size = New System.Drawing.Size(429, 57)
        Me.grpNpm.TabIndex = 1
        Me.grpNpm.TabStop = False
        Me.grpNpm.Text = "npm (Package Manager)"
        '
        'lblNpmPath
        '
        Me.lblNpmPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNpmPath.AutoEllipsis = True
        Me.lblNpmPath.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblNpmPath.ForeColor = System.Drawing.Color.DimGray
        Me.lblNpmPath.Location = New System.Drawing.Point(44, 36)
        Me.lblNpmPath.Name = "lblNpmPath"
        Me.lblNpmPath.Size = New System.Drawing.Size(385, 15)
        Me.lblNpmPath.TabIndex = 2
        Me.lblNpmPath.Text = "Info..."
        '
        'lblNpmVersion
        '
        Me.lblNpmVersion.AutoSize = True
        Me.lblNpmVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNpmVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNpmVersion.Location = New System.Drawing.Point(44, 19)
        Me.lblNpmVersion.Name = "lblNpmVersion"
        Me.lblNpmVersion.Size = New System.Drawing.Size(76, 15)
        Me.lblNpmVersion.TabIndex = 1
        Me.lblNpmVersion.Text = "Non rilevato"
        '
        'lblNpmStatusIcon
        '
        Me.lblNpmStatusIcon.AutoSize = True
        Me.lblNpmStatusIcon.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblNpmStatusIcon.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNpmStatusIcon.Location = New System.Drawing.Point(6, 17)
        Me.lblNpmStatusIcon.Name = "lblNpmStatusIcon"
        Me.lblNpmStatusIcon.Size = New System.Drawing.Size(32, 30)
        Me.lblNpmStatusIcon.TabIndex = 0
        Me.lblNpmStatusIcon.Text = "○"
        '
        'grpNodeRed
        '
        Me.grpNodeRed.BackColor = System.Drawing.Color.Transparent
        Me.grpNodeRed.Controls.Add(Me.lblNodeRedPath)
        Me.grpNodeRed.Controls.Add(Me.lblNodeRedVersion)
        Me.grpNodeRed.Controls.Add(Me.lblNodeRedStatusIcon)
        Me.grpNodeRed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpNodeRed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpNodeRed.Location = New System.Drawing.Point(0, 61)
        Me.grpNodeRed.Margin = New System.Windows.Forms.Padding(0, 2, 3, 0)
        Me.grpNodeRed.Name = "grpNodeRed"
        Me.grpNodeRed.Size = New System.Drawing.Size(429, 57)
        Me.grpNodeRed.TabIndex = 2
        Me.grpNodeRed.TabStop = False
        Me.grpNodeRed.Text = "Node-RED (Server)"
        '
        'lblNodeRedPath
        '
        Me.lblNodeRedPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNodeRedPath.AutoEllipsis = True
        Me.lblNodeRedPath.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblNodeRedPath.ForeColor = System.Drawing.Color.DimGray
        Me.lblNodeRedPath.Location = New System.Drawing.Point(44, 36)
        Me.lblNodeRedPath.Name = "lblNodeRedPath"
        Me.lblNodeRedPath.Size = New System.Drawing.Size(385, 15)
        Me.lblNodeRedPath.TabIndex = 2
        Me.lblNodeRedPath.Text = "Info..."
        '
        'lblNodeRedVersion
        '
        Me.lblNodeRedVersion.AutoSize = True
        Me.lblNodeRedVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNodeRedVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNodeRedVersion.Location = New System.Drawing.Point(44, 19)
        Me.lblNodeRedVersion.Name = "lblNodeRedVersion"
        Me.lblNodeRedVersion.Size = New System.Drawing.Size(76, 15)
        Me.lblNodeRedVersion.TabIndex = 1
        Me.lblNodeRedVersion.Text = "Non rilevato"
        '
        'lblNodeRedStatusIcon
        '
        Me.lblNodeRedStatusIcon.AutoSize = True
        Me.lblNodeRedStatusIcon.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblNodeRedStatusIcon.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNodeRedStatusIcon.Location = New System.Drawing.Point(6, 17)
        Me.lblNodeRedStatusIcon.Name = "lblNodeRedStatusIcon"
        Me.lblNodeRedStatusIcon.Size = New System.Drawing.Size(32, 30)
        Me.lblNodeRedStatusIcon.TabIndex = 0
        Me.lblNodeRedStatusIcon.Text = "○"
        '
        'grpOllama
        '
        Me.grpOllama.BackColor = System.Drawing.Color.Transparent
        Me.grpOllama.Controls.Add(Me.lblOllamaEnvPath)
        Me.grpOllama.Controls.Add(Me.lblOllamaEnvVersion)
        Me.grpOllama.Controls.Add(Me.lblOllamaEnvIcon)
        Me.grpOllama.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpOllama.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpOllama.Location = New System.Drawing.Point(435, 61)
        Me.grpOllama.Margin = New System.Windows.Forms.Padding(3, 2, 0, 0)
        Me.grpOllama.Name = "grpOllama"
        Me.grpOllama.Size = New System.Drawing.Size(429, 57)
        Me.grpOllama.TabIndex = 3
        Me.grpOllama.TabStop = False
        Me.grpOllama.Text = "Ollama (Local AI)"
        '
        'lblOllamaEnvPath
        '
        Me.lblOllamaEnvPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOllamaEnvPath.AutoEllipsis = True
        Me.lblOllamaEnvPath.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblOllamaEnvPath.ForeColor = System.Drawing.Color.DimGray
        Me.lblOllamaEnvPath.Location = New System.Drawing.Point(44, 36)
        Me.lblOllamaEnvPath.Name = "lblOllamaEnvPath"
        Me.lblOllamaEnvPath.Size = New System.Drawing.Size(385, 15)
        Me.lblOllamaEnvPath.TabIndex = 2
        Me.lblOllamaEnvPath.Text = "Info..."
        '
        'lblOllamaEnvVersion
        '
        Me.lblOllamaEnvVersion.AutoSize = True
        Me.lblOllamaEnvVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblOllamaEnvVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOllamaEnvVersion.Location = New System.Drawing.Point(44, 19)
        Me.lblOllamaEnvVersion.Name = "lblOllamaEnvVersion"
        Me.lblOllamaEnvVersion.Size = New System.Drawing.Size(76, 15)
        Me.lblOllamaEnvVersion.TabIndex = 1
        Me.lblOllamaEnvVersion.Text = "Non rilevato"
        '
        'lblOllamaEnvIcon
        '
        Me.lblOllamaEnvIcon.AutoSize = True
        Me.lblOllamaEnvIcon.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblOllamaEnvIcon.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOllamaEnvIcon.Location = New System.Drawing.Point(6, 17)
        Me.lblOllamaEnvIcon.Name = "lblOllamaEnvIcon"
        Me.lblOllamaEnvIcon.Size = New System.Drawing.Size(32, 30)
        Me.lblOllamaEnvIcon.TabIndex = 0
        Me.lblOllamaEnvIcon.Text = "○"
        '
        'lblEnvSubtitle
        '
        Me.lblEnvSubtitle.AutoSize = True
        Me.lblEnvSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnvSubtitle.ForeColor = System.Drawing.Color.DimGray
        Me.lblEnvSubtitle.Location = New System.Drawing.Point(145, 12)
        Me.lblEnvSubtitle.Name = "lblEnvSubtitle"
        Me.lblEnvSubtitle.Size = New System.Drawing.Size(374, 13)
        Me.lblEnvSubtitle.TabIndex = 1
        Me.lblEnvSubtitle.Text = "Stato di Node.js, npm, Node-RED e Ollama Local AI installati nel sistema"
        Me.lblEnvSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEnvTitle
        '
        Me.lblEnvTitle.AutoSize = True
        Me.lblEnvTitle.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnvTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEnvTitle.Location = New System.Drawing.Point(8, 9)
        Me.lblEnvTitle.Name = "lblEnvTitle"
        Me.lblEnvTitle.Size = New System.Drawing.Size(129, 17)
        Me.lblEnvTitle.TabIndex = 0
        Me.lblEnvTitle.Text = "Verifica dipendenze"
        Me.lblEnvTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabLog
        '
        Me.tabLog.BackColor = System.Drawing.SystemColors.Control
        Me.tabLog.Controls.Add(Me.rtbLog)
        Me.tabLog.Controls.Add(Me.tsLog)
        Me.tabLog.Location = New System.Drawing.Point(4, 24)
        Me.tabLog.Name = "tabLog"
        Me.tabLog.Size = New System.Drawing.Size(876, 490)
        Me.tabLog.TabIndex = 2
        Me.tabLog.Text = "Log"
        '
        'rtbLog
        '
        Me.rtbLog.BackColor = System.Drawing.Color.Black
        Me.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbLog.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.rtbLog.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.rtbLog.Location = New System.Drawing.Point(0, 25)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.ReadOnly = True
        Me.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbLog.Size = New System.Drawing.Size(876, 465)
        Me.rtbLog.TabIndex = 1
        Me.rtbLog.Text = ""
        '
        'tsLog
        '
        Me.tsLog.BackColor = System.Drawing.SystemColors.Control
        Me.tsLog.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsLog.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbPauseLog, Me.tsbClearLog, Me.sep1, Me.tsbExportTxt, Me.tsbExportCsv, Me.sep2, Me.lblFilter, Me.cmbLogFilter, Me.sep3, Me.tsbAutoScroll})
        Me.tsLog.Location = New System.Drawing.Point(0, 0)
        Me.tsLog.Name = "tsLog"
        Me.tsLog.Size = New System.Drawing.Size(876, 25)
        Me.tsLog.TabIndex = 0
        '
        'tsbPauseLog
        '
        Me.tsbPauseLog.CheckOnClick = True
        Me.tsbPauseLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbPauseLog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tsbPauseLog.Name = "tsbPauseLog"
        Me.tsbPauseLog.Size = New System.Drawing.Size(57, 22)
        Me.tsbPauseLog.Text = "⏸ Pausa"
        '
        'tsbClearLog
        '
        Me.tsbClearLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbClearLog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tsbClearLog.Name = "tsbClearLog"
        Me.tsbClearLog.Size = New System.Drawing.Size(45, 22)
        Me.tsbClearLog.Text = "Pulisci"
        '
        'sep1
        '
        Me.sep1.Name = "sep1"
        Me.sep1.Size = New System.Drawing.Size(6, 25)
        '
        'tsbExportTxt
        '
        Me.tsbExportTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbExportTxt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tsbExportTxt.Name = "tsbExportTxt"
        Me.tsbExportTxt.Size = New System.Drawing.Size(72, 22)
        Me.tsbExportTxt.Text = "Esporta TXT"
        '
        'tsbExportCsv
        '
        Me.tsbExportCsv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbExportCsv.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tsbExportCsv.Name = "tsbExportCsv"
        Me.tsbExportCsv.Size = New System.Drawing.Size(74, 22)
        Me.tsbExportCsv.Text = "Esporta CSV"
        '
        'sep2
        '
        Me.sep2.Name = "sep2"
        Me.sep2.Size = New System.Drawing.Size(6, 25)
        '
        'lblFilter
        '
        Me.lblFilter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(37, 22)
        Me.lblFilter.Text = "Filtro:"
        '
        'cmbLogFilter
        '
        Me.cmbLogFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLogFilter.Items.AddRange(New Object() {"Tutti", "INFO", "AVVISI", "ERRORI", "DEBUG", "OK"})
        Me.cmbLogFilter.Name = "cmbLogFilter"
        Me.cmbLogFilter.Size = New System.Drawing.Size(90, 25)
        '
        'sep3
        '
        Me.sep3.Name = "sep3"
        Me.sep3.Size = New System.Drawing.Size(6, 25)
        '
        'tsbAutoScroll
        '
        Me.tsbAutoScroll.Checked = True
        Me.tsbAutoScroll.CheckOnClick = True
        Me.tsbAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tsbAutoScroll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbAutoScroll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tsbAutoScroll.Name = "tsbAutoScroll"
        Me.tsbAutoScroll.Size = New System.Drawing.Size(66, 22)
        Me.tsbAutoScroll.Text = "AutoScroll"
        '
        'tabBackup
        '
        Me.tabBackup.BackColor = System.Drawing.SystemColors.Control
        Me.tabBackup.Controls.Add(Me.btnOpenBackupFolder)
        Me.tabBackup.Controls.Add(Me.lvBackups)
        Me.tabBackup.Controls.Add(Me.btnDeleteBackup)
        Me.tabBackup.Controls.Add(Me.btnRestoreBackup)
        Me.tabBackup.Controls.Add(Me.btnBackupNow)
        Me.tabBackup.Controls.Add(Me.grpBackupConfig)
        Me.tabBackup.Controls.Add(Me.lblBackupTitle)
        Me.tabBackup.Location = New System.Drawing.Point(4, 24)
        Me.tabBackup.Name = "tabBackup"
        Me.tabBackup.Size = New System.Drawing.Size(876, 490)
        Me.tabBackup.TabIndex = 3
        Me.tabBackup.Text = "Backup"
        '
        'btnOpenBackupFolder
        '
        Me.btnOpenBackupFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenBackupFolder.Location = New System.Drawing.Point(424, 464)
        Me.btnOpenBackupFolder.Name = "btnOpenBackupFolder"
        Me.btnOpenBackupFolder.Size = New System.Drawing.Size(150, 23)
        Me.btnOpenBackupFolder.TabIndex = 3
        Me.btnOpenBackupFolder.Text = "Apri Cartella"
        Me.btnOpenBackupFolder.UseVisualStyleBackColor = True
        '
        'lvBackups
        '
        Me.lvBackups.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvBackups.BackColor = System.Drawing.SystemColors.Window
        Me.lvBackups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvBackups.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colBackupName, Me.colBackupDate, Me.colBackupSize, Me.colBackupStatus})
        Me.lvBackups.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lvBackups.FullRowSelect = True
        Me.lvBackups.HideSelection = False
        Me.lvBackups.Location = New System.Drawing.Point(6, 118)
        Me.lvBackups.Name = "lvBackups"
        Me.lvBackups.Size = New System.Drawing.Size(862, 340)
        Me.lvBackups.TabIndex = 4
        Me.lvBackups.UseCompatibleStateImageBehavior = False
        Me.lvBackups.View = System.Windows.Forms.View.Details
        '
        'colBackupName
        '
        Me.colBackupName.Text = "Nome file"
        Me.colBackupName.Width = 250
        '
        'colBackupDate
        '
        Me.colBackupDate.Text = "Data"
        Me.colBackupDate.Width = 140
        '
        'colBackupSize
        '
        Me.colBackupSize.Text = "Dimensione"
        Me.colBackupSize.Width = 100
        '
        'colBackupStatus
        '
        Me.colBackupStatus.Text = "Stato"
        '
        'btnDeleteBackup
        '
        Me.btnDeleteBackup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteBackup.Location = New System.Drawing.Point(298, 464)
        Me.btnDeleteBackup.Name = "btnDeleteBackup"
        Me.btnDeleteBackup.Size = New System.Drawing.Size(120, 23)
        Me.btnDeleteBackup.TabIndex = 2
        Me.btnDeleteBackup.Text = "Elimina"
        Me.btnDeleteBackup.UseVisualStyleBackColor = True
        '
        'btnRestoreBackup
        '
        Me.btnRestoreBackup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRestoreBackup.Location = New System.Drawing.Point(162, 464)
        Me.btnRestoreBackup.Name = "btnRestoreBackup"
        Me.btnRestoreBackup.Size = New System.Drawing.Size(130, 23)
        Me.btnRestoreBackup.TabIndex = 1
        Me.btnRestoreBackup.Text = "Ripristina"
        Me.btnRestoreBackup.UseVisualStyleBackColor = True
        '
        'btnBackupNow
        '
        Me.btnBackupNow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBackupNow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackupNow.Location = New System.Drawing.Point(6, 464)
        Me.btnBackupNow.Name = "btnBackupNow"
        Me.btnBackupNow.Size = New System.Drawing.Size(150, 23)
        Me.btnBackupNow.TabIndex = 0
        Me.btnBackupNow.Text = "Esegui Backup"
        Me.btnBackupNow.UseVisualStyleBackColor = True
        '
        'grpBackupConfig
        '
        Me.grpBackupConfig.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpBackupConfig.BackColor = System.Drawing.Color.Transparent
        Me.grpBackupConfig.Controls.Add(Me.btnSaveBackupConfig)
        Me.grpBackupConfig.Controls.Add(Me.nudMaxBackups)
        Me.grpBackupConfig.Controls.Add(Me.lblMaxBackups)
        Me.grpBackupConfig.Controls.Add(Me.cmbBackupSchedule)
        Me.grpBackupConfig.Controls.Add(Me.lblSchedule)
        Me.grpBackupConfig.Controls.Add(Me.btnBrowseBackup)
        Me.grpBackupConfig.Controls.Add(Me.txtBackupFolder)
        Me.grpBackupConfig.Controls.Add(Me.lblBackupFolder)
        Me.grpBackupConfig.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpBackupConfig.Location = New System.Drawing.Point(6, 29)
        Me.grpBackupConfig.Name = "grpBackupConfig"
        Me.grpBackupConfig.Size = New System.Drawing.Size(862, 83)
        Me.grpBackupConfig.TabIndex = 1
        Me.grpBackupConfig.TabStop = False
        Me.grpBackupConfig.Text = "Configurazione"
        '
        'btnSaveBackupConfig
        '
        Me.btnSaveBackupConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveBackupConfig.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveBackupConfig.Location = New System.Drawing.Point(776, 22)
        Me.btnSaveBackupConfig.Name = "btnSaveBackupConfig"
        Me.btnSaveBackupConfig.Size = New System.Drawing.Size(80, 23)
        Me.btnSaveBackupConfig.TabIndex = 7
        Me.btnSaveBackupConfig.Text = "Salva"
        Me.btnSaveBackupConfig.UseVisualStyleBackColor = True
        '
        'nudMaxBackups
        '
        Me.nudMaxBackups.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudMaxBackups.BackColor = System.Drawing.SystemColors.Window
        Me.nudMaxBackups.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nudMaxBackups.Location = New System.Drawing.Point(700, 22)
        Me.nudMaxBackups.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudMaxBackups.Name = "nudMaxBackups"
        Me.nudMaxBackups.Size = New System.Drawing.Size(70, 23)
        Me.nudMaxBackups.TabIndex = 6
        Me.nudMaxBackups.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'lblMaxBackups
        '
        Me.lblMaxBackups.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMaxBackups.AutoSize = True
        Me.lblMaxBackups.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxBackups.Location = New System.Drawing.Point(619, 26)
        Me.lblMaxBackups.Name = "lblMaxBackups"
        Me.lblMaxBackups.Size = New System.Drawing.Size(75, 15)
        Me.lblMaxBackups.TabIndex = 5
        Me.lblMaxBackups.Text = "Backup max:"
        '
        'cmbBackupSchedule
        '
        Me.cmbBackupSchedule.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbBackupSchedule.BackColor = System.Drawing.SystemColors.Window
        Me.cmbBackupSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBackupSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbBackupSchedule.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbBackupSchedule.Items.AddRange(New Object() {"Disabilitato", "Ogni ora", "Giornaliero", "Settimanale"})
        Me.cmbBackupSchedule.Location = New System.Drawing.Point(100, 22)
        Me.cmbBackupSchedule.Name = "cmbBackupSchedule"
        Me.cmbBackupSchedule.Size = New System.Drawing.Size(513, 23)
        Me.cmbBackupSchedule.TabIndex = 4
        '
        'lblSchedule
        '
        Me.lblSchedule.AutoSize = True
        Me.lblSchedule.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSchedule.Location = New System.Drawing.Point(6, 25)
        Me.lblSchedule.Name = "lblSchedule"
        Me.lblSchedule.Size = New System.Drawing.Size(83, 15)
        Me.lblSchedule.TabIndex = 3
        Me.lblSchedule.Text = "Pianificazione:"
        '
        'btnBrowseBackup
        '
        Me.btnBrowseBackup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseBackup.Location = New System.Drawing.Point(776, 52)
        Me.btnBrowseBackup.Name = "btnBrowseBackup"
        Me.btnBrowseBackup.Size = New System.Drawing.Size(80, 23)
        Me.btnBrowseBackup.TabIndex = 2
        Me.btnBrowseBackup.Text = "Sfoglia"
        Me.btnBrowseBackup.UseVisualStyleBackColor = True
        '
        'txtBackupFolder
        '
        Me.txtBackupFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBackupFolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtBackupFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBackupFolder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBackupFolder.Location = New System.Drawing.Point(100, 52)
        Me.txtBackupFolder.Name = "txtBackupFolder"
        Me.txtBackupFolder.Size = New System.Drawing.Size(670, 23)
        Me.txtBackupFolder.TabIndex = 1
        '
        'lblBackupFolder
        '
        Me.lblBackupFolder.AutoSize = True
        Me.lblBackupFolder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBackupFolder.Location = New System.Drawing.Point(2, 54)
        Me.lblBackupFolder.Name = "lblBackupFolder"
        Me.lblBackupFolder.Size = New System.Drawing.Size(92, 15)
        Me.lblBackupFolder.TabIndex = 0
        Me.lblBackupFolder.Text = "Cartella Backup:"
        '
        'lblBackupTitle
        '
        Me.lblBackupTitle.AutoSize = True
        Me.lblBackupTitle.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBackupTitle.Location = New System.Drawing.Point(8, 9)
        Me.lblBackupTitle.Name = "lblBackupTitle"
        Me.lblBackupTitle.Size = New System.Drawing.Size(110, 17)
        Me.lblBackupTitle.TabIndex = 0
        Me.lblBackupTitle.Text = "Gestione backup"
        '
        'tabSecurity
        '
        Me.tabSecurity.BackColor = System.Drawing.SystemColors.Control
        Me.tabSecurity.Controls.Add(Me.btnOpenSettingsFile)
        Me.tabSecurity.Controls.Add(Me.btnSaveSecurity)
        Me.tabSecurity.Controls.Add(Me.grpLdap)
        Me.tabSecurity.Controls.Add(Me.grpIpRestriction)
        Me.tabSecurity.Controls.Add(Me.grpUsers)
        Me.tabSecurity.Controls.Add(Me.grpAuthToggle)
        Me.tabSecurity.Controls.Add(Me.lblSecSubtitle)
        Me.tabSecurity.Controls.Add(Me.lblSecTitle)
        Me.tabSecurity.Location = New System.Drawing.Point(4, 24)
        Me.tabSecurity.Name = "tabSecurity"
        Me.tabSecurity.Size = New System.Drawing.Size(876, 490)
        Me.tabSecurity.TabIndex = 4
        Me.tabSecurity.Text = "Sicurezza"
        '
        'btnOpenSettingsFile
        '
        Me.btnOpenSettingsFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenSettingsFile.Location = New System.Drawing.Point(194, 464)
        Me.btnOpenSettingsFile.Name = "btnOpenSettingsFile"
        Me.btnOpenSettingsFile.Size = New System.Drawing.Size(157, 23)
        Me.btnOpenSettingsFile.TabIndex = 1
        Me.btnOpenSettingsFile.Text = "Apri settings.js"
        Me.btnOpenSettingsFile.UseVisualStyleBackColor = True
        '
        'btnSaveSecurity
        '
        Me.btnSaveSecurity.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveSecurity.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaveSecurity.Location = New System.Drawing.Point(8, 464)
        Me.btnSaveSecurity.Name = "btnSaveSecurity"
        Me.btnSaveSecurity.Size = New System.Drawing.Size(180, 23)
        Me.btnSaveSecurity.TabIndex = 0
        Me.btnSaveSecurity.Text = "Salva e applica"
        Me.btnSaveSecurity.UseVisualStyleBackColor = True
        '
        'grpLdap
        '
        Me.grpLdap.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpLdap.BackColor = System.Drawing.Color.Transparent
        Me.grpLdap.Controls.Add(Me.txtLdapBaseDn)
        Me.grpLdap.Controls.Add(Me.lblLdapBaseDn)
        Me.grpLdap.Controls.Add(Me.txtLdapUrl)
        Me.grpLdap.Controls.Add(Me.lblLdapUrl)
        Me.grpLdap.Controls.Add(Me.chkEnableLdap)
        Me.grpLdap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpLdap.Location = New System.Drawing.Point(500, 292)
        Me.grpLdap.Name = "grpLdap"
        Me.grpLdap.Size = New System.Drawing.Size(368, 166)
        Me.grpLdap.TabIndex = 5
        Me.grpLdap.TabStop = False
        Me.grpLdap.Text = "LDAP / Active Directory (opzionale)"
        '
        'txtLdapBaseDn
        '
        Me.txtLdapBaseDn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLdapBaseDn.BackColor = System.Drawing.SystemColors.Window
        Me.txtLdapBaseDn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLdapBaseDn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLdapBaseDn.Location = New System.Drawing.Point(78, 76)
        Me.txtLdapBaseDn.Name = "txtLdapBaseDn"
        Me.txtLdapBaseDn.Size = New System.Drawing.Size(284, 23)
        Me.txtLdapBaseDn.TabIndex = 4
        '
        'lblLdapBaseDn
        '
        Me.lblLdapBaseDn.AutoSize = True
        Me.lblLdapBaseDn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLdapBaseDn.Location = New System.Drawing.Point(6, 78)
        Me.lblLdapBaseDn.Name = "lblLdapBaseDn"
        Me.lblLdapBaseDn.Size = New System.Drawing.Size(54, 15)
        Me.lblLdapBaseDn.TabIndex = 3
        Me.lblLdapBaseDn.Text = "Base DN:"
        '
        'txtLdapUrl
        '
        Me.txtLdapUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLdapUrl.BackColor = System.Drawing.SystemColors.Window
        Me.txtLdapUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLdapUrl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLdapUrl.Location = New System.Drawing.Point(78, 47)
        Me.txtLdapUrl.Name = "txtLdapUrl"
        Me.txtLdapUrl.Size = New System.Drawing.Size(284, 23)
        Me.txtLdapUrl.TabIndex = 2
        '
        'lblLdapUrl
        '
        Me.lblLdapUrl.AutoSize = True
        Me.lblLdapUrl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLdapUrl.Location = New System.Drawing.Point(6, 49)
        Me.lblLdapUrl.Name = "lblLdapUrl"
        Me.lblLdapUrl.Size = New System.Drawing.Size(66, 15)
        Me.lblLdapUrl.TabIndex = 1
        Me.lblLdapUrl.Text = "URL Server:"
        '
        'chkEnableLdap
        '
        Me.chkEnableLdap.AutoSize = True
        Me.chkEnableLdap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEnableLdap.Location = New System.Drawing.Point(6, 22)
        Me.chkEnableLdap.Name = "chkEnableLdap"
        Me.chkEnableLdap.Size = New System.Drawing.Size(172, 19)
        Me.chkEnableLdap.TabIndex = 0
        Me.chkEnableLdap.Text = "Abilita autenticazione LDAP"
        '
        'grpIpRestriction
        '
        Me.grpIpRestriction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpIpRestriction.BackColor = System.Drawing.Color.Transparent
        Me.grpIpRestriction.Controls.Add(Me.btnRemoveIp)
        Me.grpIpRestriction.Controls.Add(Me.btnAddIp)
        Me.grpIpRestriction.Controls.Add(Me.txtNewIp)
        Me.grpIpRestriction.Controls.Add(Me.lstAllowedIps)
        Me.grpIpRestriction.Controls.Add(Me.chkEnableIpRestriction)
        Me.grpIpRestriction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpIpRestriction.Location = New System.Drawing.Point(8, 292)
        Me.grpIpRestriction.Name = "grpIpRestriction"
        Me.grpIpRestriction.Size = New System.Drawing.Size(486, 166)
        Me.grpIpRestriction.TabIndex = 4
        Me.grpIpRestriction.TabStop = False
        Me.grpIpRestriction.Text = "Restrizione IP (whitelist)"
        '
        'btnRemoveIp
        '
        Me.btnRemoveIp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveIp.Location = New System.Drawing.Point(402, 76)
        Me.btnRemoveIp.Name = "btnRemoveIp"
        Me.btnRemoveIp.Size = New System.Drawing.Size(78, 23)
        Me.btnRemoveIp.TabIndex = 4
        Me.btnRemoveIp.Text = "- Rimuovi"
        Me.btnRemoveIp.UseVisualStyleBackColor = True
        '
        'btnAddIp
        '
        Me.btnAddIp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddIp.Location = New System.Drawing.Point(318, 76)
        Me.btnAddIp.Name = "btnAddIp"
        Me.btnAddIp.Size = New System.Drawing.Size(78, 23)
        Me.btnAddIp.TabIndex = 3
        Me.btnAddIp.Text = "+ Aggiungi"
        Me.btnAddIp.UseVisualStyleBackColor = True
        '
        'txtNewIp
        '
        Me.txtNewIp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewIp.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewIp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNewIp.Location = New System.Drawing.Point(318, 47)
        Me.txtNewIp.Name = "txtNewIp"
        Me.txtNewIp.Size = New System.Drawing.Size(162, 23)
        Me.txtNewIp.TabIndex = 2
        '
        'lstAllowedIps
        '
        Me.lstAllowedIps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstAllowedIps.BackColor = System.Drawing.SystemColors.Window
        Me.lstAllowedIps.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstAllowedIps.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstAllowedIps.ItemHeight = 15
        Me.lstAllowedIps.Location = New System.Drawing.Point(6, 47)
        Me.lstAllowedIps.Name = "lstAllowedIps"
        Me.lstAllowedIps.Size = New System.Drawing.Size(306, 105)
        Me.lstAllowedIps.TabIndex = 1
        '
        'chkEnableIpRestriction
        '
        Me.chkEnableIpRestriction.AutoSize = True
        Me.chkEnableIpRestriction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEnableIpRestriction.Location = New System.Drawing.Point(6, 22)
        Me.chkEnableIpRestriction.Name = "chkEnableIpRestriction"
        Me.chkEnableIpRestriction.Size = New System.Drawing.Size(241, 19)
        Me.chkEnableIpRestriction.TabIndex = 0
        Me.chkEnableIpRestriction.Text = "Abilita restrizione accesso per indirizzo IP"
        '
        'grpUsers
        '
        Me.grpUsers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpUsers.BackColor = System.Drawing.Color.Transparent
        Me.grpUsers.Controls.Add(Me.btnDeleteUser)
        Me.grpUsers.Controls.Add(Me.btnEditUser)
        Me.grpUsers.Controls.Add(Me.btnAddUser)
        Me.grpUsers.Controls.Add(Me.dgvUsers)
        Me.grpUsers.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpUsers.Location = New System.Drawing.Point(8, 102)
        Me.grpUsers.Name = "grpUsers"
        Me.grpUsers.Size = New System.Drawing.Size(860, 184)
        Me.grpUsers.TabIndex = 3
        Me.grpUsers.TabStop = False
        Me.grpUsers.Text = "Utenti autorizzati"
        '
        'btnDeleteUser
        '
        Me.btnDeleteUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteUser.Location = New System.Drawing.Point(819, 104)
        Me.btnDeleteUser.Name = "btnDeleteUser"
        Me.btnDeleteUser.Size = New System.Drawing.Size(35, 34)
        Me.btnDeleteUser.TabIndex = 3
        Me.btnDeleteUser.Text = "✕"
        Me.btnDeleteUser.UseVisualStyleBackColor = True
        '
        'btnEditUser
        '
        Me.btnEditUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditUser.Location = New System.Drawing.Point(819, 64)
        Me.btnEditUser.Name = "btnEditUser"
        Me.btnEditUser.Size = New System.Drawing.Size(35, 34)
        Me.btnEditUser.TabIndex = 2
        Me.btnEditUser.Text = "✎"
        Me.btnEditUser.UseVisualStyleBackColor = True
        '
        'btnAddUser
        '
        Me.btnAddUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddUser.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddUser.Location = New System.Drawing.Point(819, 22)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(35, 36)
        Me.btnAddUser.TabIndex = 1
        Me.btnAddUser.Text = "+"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'dgvUsers
        '
        Me.dgvUsers.AllowUserToAddRows = False
        Me.dgvUsers.AllowUserToDeleteRows = False
        Me.dgvUsers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUsers.BackgroundColor = System.Drawing.Color.White
        Me.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsers.Location = New System.Drawing.Point(6, 22)
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.RowHeadersVisible = False
        Me.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUsers.Size = New System.Drawing.Size(807, 153)
        Me.dgvUsers.TabIndex = 0
        '
        'grpAuthToggle
        '
        Me.grpAuthToggle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpAuthToggle.BackColor = System.Drawing.Color.Transparent
        Me.grpAuthToggle.Controls.Add(Me.lblAuthInfo)
        Me.grpAuthToggle.Controls.Add(Me.chkEnableAuth)
        Me.grpAuthToggle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpAuthToggle.Location = New System.Drawing.Point(8, 29)
        Me.grpAuthToggle.Name = "grpAuthToggle"
        Me.grpAuthToggle.Size = New System.Drawing.Size(860, 67)
        Me.grpAuthToggle.TabIndex = 2
        Me.grpAuthToggle.TabStop = False
        Me.grpAuthToggle.Text = "Autenticazione Node-Red"
        '
        'lblAuthInfo
        '
        Me.lblAuthInfo.AutoSize = True
        Me.lblAuthInfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuthInfo.ForeColor = System.Drawing.Color.DimGray
        Me.lblAuthInfo.Location = New System.Drawing.Point(24, 44)
        Me.lblAuthInfo.Name = "lblAuthInfo"
        Me.lblAuthInfo.Size = New System.Drawing.Size(267, 13)
        Me.lblAuthInfo.TabIndex = 1
        Me.lblAuthInfo.Text = "Richiede riavvio di Node-RED per essere applicata."
        '
        'chkEnableAuth
        '
        Me.chkEnableAuth.AutoSize = True
        Me.chkEnableAuth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEnableAuth.Location = New System.Drawing.Point(6, 22)
        Me.chkEnableAuth.Name = "chkEnableAuth"
        Me.chkEnableAuth.Size = New System.Drawing.Size(348, 19)
        Me.chkEnableAuth.TabIndex = 0
        Me.chkEnableAuth.Text = "Abilita login con username e password per l'editor Node-RED"
        '
        'lblSecSubtitle
        '
        Me.lblSecSubtitle.AutoSize = True
        Me.lblSecSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSecSubtitle.ForeColor = System.Drawing.Color.DimGray
        Me.lblSecSubtitle.Location = New System.Drawing.Point(199, 12)
        Me.lblSecSubtitle.Name = "lblSecSubtitle"
        Me.lblSecSubtitle.Size = New System.Drawing.Size(321, 13)
        Me.lblSecSubtitle.TabIndex = 1
        Me.lblSecSubtitle.Text = "Gestione autenticazione editor Node-RED e restrizioni di rete"
        '
        'lblSecTitle
        '
        Me.lblSecTitle.AutoSize = True
        Me.lblSecTitle.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSecTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSecTitle.Location = New System.Drawing.Point(8, 9)
        Me.lblSecTitle.Name = "lblSecTitle"
        Me.lblSecTitle.Size = New System.Drawing.Size(181, 17)
        Me.lblSecTitle.TabIndex = 0
        Me.lblSecTitle.Text = "Sicurezza e controllo accessi"
        '
        'tabSettings
        '
        Me.tabSettings.BackColor = System.Drawing.SystemColors.Control
        Me.tabSettings.Controls.Add(Me.btnResetSettings)
        Me.tabSettings.Controls.Add(Me.btnSaveSettings)
        Me.tabSettings.Controls.Add(Me.grpWatchdog)
        Me.tabSettings.Controls.Add(Me.grpStartup)
        Me.tabSettings.Controls.Add(Me.grpNodeRedConfig)
        Me.tabSettings.Controls.Add(Me.lblSettingsTitle)
        Me.tabSettings.Location = New System.Drawing.Point(4, 24)
        Me.tabSettings.Name = "tabSettings"
        Me.tabSettings.Size = New System.Drawing.Size(876, 490)
        Me.tabSettings.TabIndex = 5
        Me.tabSettings.Text = "Impostazioni"
        '
        'btnResetSettings
        '
        Me.btnResetSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnResetSettings.Location = New System.Drawing.Point(194, 464)
        Me.btnResetSettings.Name = "btnResetSettings"
        Me.btnResetSettings.Size = New System.Drawing.Size(160, 23)
        Me.btnResetSettings.TabIndex = 1
        Me.btnResetSettings.Text = "Ripristina default"
        Me.btnResetSettings.UseVisualStyleBackColor = True
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveSettings.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaveSettings.Location = New System.Drawing.Point(8, 464)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(180, 23)
        Me.btnSaveSettings.TabIndex = 0
        Me.btnSaveSettings.Text = "Salva impostazioni"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'grpWatchdog
        '
        Me.grpWatchdog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpWatchdog.BackColor = System.Drawing.Color.Transparent
        Me.grpWatchdog.Controls.Add(Me.lblMaxRestartsHint)
        Me.grpWatchdog.Controls.Add(Me.nudMaxRestarts)
        Me.grpWatchdog.Controls.Add(Me.lblMaxRestarts)
        Me.grpWatchdog.Controls.Add(Me.nudWatchdogInterval)
        Me.grpWatchdog.Controls.Add(Me.lblWatchInterval)
        Me.grpWatchdog.Controls.Add(Me.chkWatchdog)
        Me.grpWatchdog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpWatchdog.Location = New System.Drawing.Point(8, 378)
        Me.grpWatchdog.Name = "grpWatchdog"
        Me.grpWatchdog.Size = New System.Drawing.Size(860, 80)
        Me.grpWatchdog.TabIndex = 3
        Me.grpWatchdog.TabStop = False
        Me.grpWatchdog.Text = "Watchdog (riavvio automatico)"
        '
        'lblMaxRestartsHint
        '
        Me.lblMaxRestartsHint.AutoSize = True
        Me.lblMaxRestartsHint.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblMaxRestartsHint.ForeColor = System.Drawing.Color.DimGray
        Me.lblMaxRestartsHint.Location = New System.Drawing.Point(360, 51)
        Me.lblMaxRestartsHint.Name = "lblMaxRestartsHint"
        Me.lblMaxRestartsHint.Size = New System.Drawing.Size(78, 13)
        Me.lblMaxRestartsHint.TabIndex = 5
        Me.lblMaxRestartsHint.Text = "(0 = illimitato)"
        '
        'nudMaxRestarts
        '
        Me.nudMaxRestarts.BackColor = System.Drawing.SystemColors.Window
        Me.nudMaxRestarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudMaxRestarts.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nudMaxRestarts.Location = New System.Drawing.Point(294, 47)
        Me.nudMaxRestarts.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nudMaxRestarts.Name = "nudMaxRestarts"
        Me.nudMaxRestarts.Size = New System.Drawing.Size(60, 23)
        Me.nudMaxRestarts.TabIndex = 4
        Me.nudMaxRestarts.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblMaxRestarts
        '
        Me.lblMaxRestarts.AutoSize = True
        Me.lblMaxRestarts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxRestarts.Location = New System.Drawing.Point(221, 49)
        Me.lblMaxRestarts.Name = "lblMaxRestarts"
        Me.lblMaxRestarts.Size = New System.Drawing.Size(67, 15)
        Me.lblMaxRestarts.TabIndex = 3
        Me.lblMaxRestarts.Text = "Max riavvii:"
        '
        'nudWatchdogInterval
        '
        Me.nudWatchdogInterval.BackColor = System.Drawing.SystemColors.Window
        Me.nudWatchdogInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudWatchdogInterval.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nudWatchdogInterval.Location = New System.Drawing.Point(145, 47)
        Me.nudWatchdogInterval.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.nudWatchdogInterval.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudWatchdogInterval.Name = "nudWatchdogInterval"
        Me.nudWatchdogInterval.Size = New System.Drawing.Size(70, 23)
        Me.nudWatchdogInterval.TabIndex = 2
        Me.nudWatchdogInterval.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'lblWatchInterval
        '
        Me.lblWatchInterval.AutoSize = True
        Me.lblWatchInterval.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWatchInterval.Location = New System.Drawing.Point(24, 49)
        Me.lblWatchInterval.Name = "lblWatchInterval"
        Me.lblWatchInterval.Size = New System.Drawing.Size(115, 15)
        Me.lblWatchInterval.TabIndex = 1
        Me.lblWatchInterval.Text = "Controllo ogni (sec):"
        '
        'chkWatchdog
        '
        Me.chkWatchdog.AutoSize = True
        Me.chkWatchdog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWatchdog.Location = New System.Drawing.Point(6, 22)
        Me.chkWatchdog.Name = "chkWatchdog"
        Me.chkWatchdog.Size = New System.Drawing.Size(373, 19)
        Me.chkWatchdog.TabIndex = 0
        Me.chkWatchdog.Text = "Abilita watchdog (riavvia Node-RED se si ferma inaspettatamente)"
        '
        'grpStartup
        '
        Me.grpStartup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpStartup.BackColor = System.Drawing.Color.Transparent
        Me.grpStartup.Controls.Add(Me.chkStartMinimized)
        Me.grpStartup.Controls.Add(Me.nudStartDelay)
        Me.grpStartup.Controls.Add(Me.chkMinimizeToTray)
        Me.grpStartup.Controls.Add(Me.lblStartDelay)
        Me.grpStartup.Controls.Add(Me.chkAutoStartNodeRed)
        Me.grpStartup.Controls.Add(Me.lblMethodHint)
        Me.grpStartup.Controls.Add(Me.cmbStartupMethod)
        Me.grpStartup.Controls.Add(Me.lblStartupMethod)
        Me.grpStartup.Controls.Add(Me.chkAutoStartWindows)
        Me.grpStartup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpStartup.Location = New System.Drawing.Point(8, 178)
        Me.grpStartup.Name = "grpStartup"
        Me.grpStartup.Size = New System.Drawing.Size(860, 194)
        Me.grpStartup.TabIndex = 2
        Me.grpStartup.TabStop = False
        Me.grpStartup.Text = "Avvio automatico e comportamento"
        '
        'chkStartMinimized
        '
        Me.chkStartMinimized.AutoSize = True
        Me.chkStartMinimized.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStartMinimized.Location = New System.Drawing.Point(6, 155)
        Me.chkStartMinimized.Name = "chkStartMinimized"
        Me.chkStartMinimized.Size = New System.Drawing.Size(174, 19)
        Me.chkStartMinimized.TabIndex = 1
        Me.chkStartMinimized.Text = "Avvia minimizzato nella tray"
        '
        'nudStartDelay
        '
        Me.nudStartDelay.BackColor = System.Drawing.SystemColors.Window
        Me.nudStartDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudStartDelay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nudStartDelay.Location = New System.Drawing.Point(137, 101)
        Me.nudStartDelay.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.nudStartDelay.Name = "nudStartDelay"
        Me.nudStartDelay.Size = New System.Drawing.Size(60, 23)
        Me.nudStartDelay.TabIndex = 6
        Me.nudStartDelay.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'chkMinimizeToTray
        '
        Me.chkMinimizeToTray.AutoSize = True
        Me.chkMinimizeToTray.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMinimizeToTray.Location = New System.Drawing.Point(6, 130)
        Me.chkMinimizeToTray.Name = "chkMinimizeToTray"
        Me.chkMinimizeToTray.Size = New System.Drawing.Size(270, 19)
        Me.chkMinimizeToTray.TabIndex = 0
        Me.chkMinimizeToTray.Text = "Minimizza nella system tray invece di chiudere"
        '
        'lblStartDelay
        '
        Me.lblStartDelay.AutoSize = True
        Me.lblStartDelay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStartDelay.Location = New System.Drawing.Point(24, 103)
        Me.lblStartDelay.Name = "lblStartDelay"
        Me.lblStartDelay.Size = New System.Drawing.Size(107, 15)
        Me.lblStartDelay.TabIndex = 5
        Me.lblStartDelay.Text = "Ritardo avvio (sec):"
        '
        'chkAutoStartNodeRed
        '
        Me.chkAutoStartNodeRed.AutoSize = True
        Me.chkAutoStartNodeRed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoStartNodeRed.Location = New System.Drawing.Point(6, 76)
        Me.chkAutoStartNodeRed.Name = "chkAutoStartNodeRed"
        Me.chkAutoStartNodeRed.Size = New System.Drawing.Size(317, 19)
        Me.chkAutoStartNodeRed.TabIndex = 4
        Me.chkAutoStartNodeRed.Text = "Avvia Node-RED automaticamente all'apertura dell'app"
        '
        'lblMethodHint
        '
        Me.lblMethodHint.AutoSize = True
        Me.lblMethodHint.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblMethodHint.ForeColor = System.Drawing.Color.DimGray
        Me.lblMethodHint.Location = New System.Drawing.Point(268, 52)
        Me.lblMethodHint.Name = "lblMethodHint"
        Me.lblMethodHint.Size = New System.Drawing.Size(202, 13)
        Me.lblMethodHint.TabIndex = 3
        Me.lblMethodHint.Text = "(Task Scheduler consigliato per server)"
        '
        'cmbStartupMethod
        '
        Me.cmbStartupMethod.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStartupMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStartupMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbStartupMethod.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStartupMethod.Items.AddRange(New Object() {"Registro di Sistema", "Task Scheduler"})
        Me.cmbStartupMethod.Location = New System.Drawing.Point(82, 47)
        Me.cmbStartupMethod.Name = "cmbStartupMethod"
        Me.cmbStartupMethod.Size = New System.Drawing.Size(180, 23)
        Me.cmbStartupMethod.TabIndex = 2
        '
        'lblStartupMethod
        '
        Me.lblStartupMethod.AutoSize = True
        Me.lblStartupMethod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStartupMethod.Location = New System.Drawing.Point(24, 50)
        Me.lblStartupMethod.Name = "lblStartupMethod"
        Me.lblStartupMethod.Size = New System.Drawing.Size(52, 15)
        Me.lblStartupMethod.TabIndex = 1
        Me.lblStartupMethod.Text = "Metodo:"
        '
        'chkAutoStartWindows
        '
        Me.chkAutoStartWindows.AutoSize = True
        Me.chkAutoStartWindows.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoStartWindows.Location = New System.Drawing.Point(6, 22)
        Me.chkAutoStartWindows.Name = "chkAutoStartWindows"
        Me.chkAutoStartWindows.Size = New System.Drawing.Size(234, 19)
        Me.chkAutoStartWindows.TabIndex = 0
        Me.chkAutoStartWindows.Text = "Avvia Node-RED Desktop con Windows"
        '
        'grpNodeRedConfig
        '
        Me.grpNodeRedConfig.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpNodeRedConfig.BackColor = System.Drawing.Color.Transparent
        Me.grpNodeRedConfig.Controls.Add(Me.btnDetectPaths)
        Me.grpNodeRedConfig.Controls.Add(Me.txtNodeRedCmd)
        Me.grpNodeRedConfig.Controls.Add(Me.lblNodeRedCmd)
        Me.grpNodeRedConfig.Controls.Add(Me.lblFlowFileHint)
        Me.grpNodeRedConfig.Controls.Add(Me.txtFlowFile)
        Me.grpNodeRedConfig.Controls.Add(Me.lblFlowFile)
        Me.grpNodeRedConfig.Controls.Add(Me.btnBrowseUserDir)
        Me.grpNodeRedConfig.Controls.Add(Me.txtUserDir)
        Me.grpNodeRedConfig.Controls.Add(Me.lblUserDir)
        Me.grpNodeRedConfig.Controls.Add(Me.lblPortHint)
        Me.grpNodeRedConfig.Controls.Add(Me.nudPort)
        Me.grpNodeRedConfig.Controls.Add(Me.lblPort)
        Me.grpNodeRedConfig.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpNodeRedConfig.Location = New System.Drawing.Point(8, 29)
        Me.grpNodeRedConfig.Name = "grpNodeRedConfig"
        Me.grpNodeRedConfig.Size = New System.Drawing.Size(860, 143)
        Me.grpNodeRedConfig.TabIndex = 1
        Me.grpNodeRedConfig.TabStop = False
        Me.grpNodeRedConfig.Text = "Configurazione Node-RED"
        '
        'btnDetectPaths
        '
        Me.btnDetectPaths.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDetectPaths.Location = New System.Drawing.Point(753, 109)
        Me.btnDetectPaths.Name = "btnDetectPaths"
        Me.btnDetectPaths.Size = New System.Drawing.Size(101, 23)
        Me.btnDetectPaths.TabIndex = 11
        Me.btnDetectPaths.Text = "Auto-rileva"
        Me.btnDetectPaths.UseVisualStyleBackColor = True
        '
        'txtNodeRedCmd
        '
        Me.txtNodeRedCmd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNodeRedCmd.BackColor = System.Drawing.SystemColors.Window
        Me.txtNodeRedCmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNodeRedCmd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNodeRedCmd.Location = New System.Drawing.Point(111, 109)
        Me.txtNodeRedCmd.Name = "txtNodeRedCmd"
        Me.txtNodeRedCmd.Size = New System.Drawing.Size(636, 23)
        Me.txtNodeRedCmd.TabIndex = 10
        '
        'lblNodeRedCmd
        '
        Me.lblNodeRedCmd.AutoSize = True
        Me.lblNodeRedCmd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNodeRedCmd.Location = New System.Drawing.Point(6, 111)
        Me.lblNodeRedCmd.Name = "lblNodeRedCmd"
        Me.lblNodeRedCmd.Size = New System.Drawing.Size(95, 15)
        Me.lblNodeRedCmd.TabIndex = 9
        Me.lblNodeRedCmd.Text = "Node-RED CMD:"
        '
        'lblFlowFileHint
        '
        Me.lblFlowFileHint.AutoSize = True
        Me.lblFlowFileHint.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblFlowFileHint.ForeColor = System.Drawing.Color.DimGray
        Me.lblFlowFileHint.Location = New System.Drawing.Point(317, 83)
        Me.lblFlowFileHint.Name = "lblFlowFileHint"
        Me.lblFlowFileHint.Size = New System.Drawing.Size(133, 13)
        Me.lblFlowFileHint.TabIndex = 8
        Me.lblFlowFileHint.Text = "(relativo a Directory Dati)"
        '
        'txtFlowFile
        '
        Me.txtFlowFile.BackColor = System.Drawing.SystemColors.Window
        Me.txtFlowFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFlowFile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFlowFile.Location = New System.Drawing.Point(111, 80)
        Me.txtFlowFile.Name = "txtFlowFile"
        Me.txtFlowFile.Size = New System.Drawing.Size(200, 23)
        Me.txtFlowFile.TabIndex = 7
        Me.txtFlowFile.Text = "flows.json"
        '
        'lblFlowFile
        '
        Me.lblFlowFile.AutoSize = True
        Me.lblFlowFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFlowFile.Location = New System.Drawing.Point(6, 81)
        Me.lblFlowFile.Name = "lblFlowFile"
        Me.lblFlowFile.Size = New System.Drawing.Size(61, 15)
        Me.lblFlowFile.TabIndex = 6
        Me.lblFlowFile.Text = "File Flows:"
        '
        'btnBrowseUserDir
        '
        Me.btnBrowseUserDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseUserDir.Location = New System.Drawing.Point(753, 51)
        Me.btnBrowseUserDir.Name = "btnBrowseUserDir"
        Me.btnBrowseUserDir.Size = New System.Drawing.Size(101, 23)
        Me.btnBrowseUserDir.TabIndex = 5
        Me.btnBrowseUserDir.Text = "Sfoglia"
        Me.btnBrowseUserDir.UseVisualStyleBackColor = True
        '
        'txtUserDir
        '
        Me.txtUserDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserDir.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserDir.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserDir.Location = New System.Drawing.Point(111, 51)
        Me.txtUserDir.Name = "txtUserDir"
        Me.txtUserDir.Size = New System.Drawing.Size(636, 23)
        Me.txtUserDir.TabIndex = 4
        '
        'lblUserDir
        '
        Me.lblUserDir.AutoSize = True
        Me.lblUserDir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUserDir.Location = New System.Drawing.Point(6, 55)
        Me.lblUserDir.Name = "lblUserDir"
        Me.lblUserDir.Size = New System.Drawing.Size(82, 15)
        Me.lblUserDir.TabIndex = 3
        Me.lblUserDir.Text = "Directory Dati:"
        '
        'lblPortHint
        '
        Me.lblPortHint.AutoSize = True
        Me.lblPortHint.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblPortHint.ForeColor = System.Drawing.Color.DimGray
        Me.lblPortHint.Location = New System.Drawing.Point(207, 26)
        Me.lblPortHint.Name = "lblPortHint"
        Me.lblPortHint.Size = New System.Drawing.Size(80, 13)
        Me.lblPortHint.TabIndex = 2
        Me.lblPortHint.Text = "(default: 1880)"
        '
        'nudPort
        '
        Me.nudPort.BackColor = System.Drawing.SystemColors.Window
        Me.nudPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.nudPort.Location = New System.Drawing.Point(111, 22)
        Me.nudPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudPort.Name = "nudPort"
        Me.nudPort.Size = New System.Drawing.Size(90, 23)
        Me.nudPort.TabIndex = 1
        Me.nudPort.Value = New Decimal(New Integer() {1880, 0, 0, 0})
        '
        'lblPort
        '
        Me.lblPort.AutoSize = True
        Me.lblPort.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPort.Location = New System.Drawing.Point(6, 26)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(69, 15)
        Me.lblPort.TabIndex = 0
        Me.lblPort.Text = "Porta HTTP:"
        '
        'lblSettingsTitle
        '
        Me.lblSettingsTitle.AutoSize = True
        Me.lblSettingsTitle.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSettingsTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSettingsTitle.Location = New System.Drawing.Point(8, 9)
        Me.lblSettingsTitle.Name = "lblSettingsTitle"
        Me.lblSettingsTitle.Size = New System.Drawing.Size(142, 17)
        Me.lblSettingsTitle.TabIndex = 0
        Me.lblSettingsTitle.Text = "Impostazioni generali"
        '
        'ssMain
        '
        Me.ssMain.BackColor = System.Drawing.SystemColors.Control
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssAppStatus, Me.tssSpring, Me.tssNodeRedStatus, Me.tssSep, Me.tssUptime})
        Me.ssMain.Location = New System.Drawing.Point(0, 578)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(884, 23)
        Me.ssMain.TabIndex = 2
        Me.ssMain.Text = "ssMain"
        '
        'tssAppStatus
        '
        Me.tssAppStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tssAppStatus.Name = "tssAppStatus"
        Me.tssAppStatus.Size = New System.Drawing.Size(43, 18)
        Me.tssAppStatus.Text = "Pronto"
        '
        'tssSpring
        '
        Me.tssSpring.Name = "tssSpring"
        Me.tssSpring.Size = New System.Drawing.Size(654, 18)
        Me.tssSpring.Spring = True
        '
        'tssNodeRedStatus
        '
        Me.tssNodeRedStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tssNodeRedStatus.Name = "tssNodeRedStatus"
        Me.tssNodeRedStatus.Size = New System.Drawing.Size(102, 18)
        Me.tssNodeRedStatus.Text = "Node-RED: Fermo"
        '
        'tssSep
        '
        Me.tssSep.Name = "tssSep"
        Me.tssSep.Size = New System.Drawing.Size(6, 23)
        '
        'tssUptime
        '
        Me.tssUptime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tssUptime.Name = "tssUptime"
        Me.tssUptime.Size = New System.Drawing.Size(64, 18)
        Me.tssUptime.Text = "Uptime: —"
        '
        'notifyIcon1
        '
        Me.notifyIcon1.Text = "Node-RED Desktop"
        Me.notifyIcon1.Visible = True
        '
        'cmsNotifyIcon
        '
        Me.cmsNotifyIcon.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsOpen, Me.cmsSep1, Me.cmsStart, Me.cmsStop, Me.cmsRestart, Me.cmsSep2, Me.cmsBrowser, Me.cmsSep3, Me.cmsExit})
        Me.cmsNotifyIcon.Name = "cmsNotifyIcon"
        Me.cmsNotifyIcon.Size = New System.Drawing.Size(201, 154)
        '
        'cmsOpen
        '
        Me.cmsOpen.Name = "cmsOpen"
        Me.cmsOpen.Size = New System.Drawing.Size(200, 22)
        Me.cmsOpen.Text = "Apri Node-RED Desktop"
        '
        'cmsSep1
        '
        Me.cmsSep1.Name = "cmsSep1"
        Me.cmsSep1.Size = New System.Drawing.Size(197, 6)
        '
        'cmsStart
        '
        Me.cmsStart.Name = "cmsStart"
        Me.cmsStart.Size = New System.Drawing.Size(200, 22)
        Me.cmsStart.Text = "▶ Avvia Node-RED"
        '
        'cmsStop
        '
        Me.cmsStop.Name = "cmsStop"
        Me.cmsStop.Size = New System.Drawing.Size(200, 22)
        Me.cmsStop.Text = "■ Ferma Node-RED"
        '
        'cmsRestart
        '
        Me.cmsRestart.Name = "cmsRestart"
        Me.cmsRestart.Size = New System.Drawing.Size(200, 22)
        Me.cmsRestart.Text = "↺ Riavvia Node-RED"
        '
        'cmsSep2
        '
        Me.cmsSep2.Name = "cmsSep2"
        Me.cmsSep2.Size = New System.Drawing.Size(197, 6)
        '
        'cmsBrowser
        '
        Me.cmsBrowser.Name = "cmsBrowser"
        Me.cmsBrowser.Size = New System.Drawing.Size(200, 22)
        Me.cmsBrowser.Text = "🌐 Apri Editor Browser"
        '
        'cmsSep3
        '
        Me.cmsSep3.Name = "cmsSep3"
        Me.cmsSep3.Size = New System.Drawing.Size(197, 6)
        '
        'cmsExit
        '
        Me.cmsExit.Name = "cmsExit"
        Me.cmsExit.Size = New System.Drawing.Size(200, 22)
        Me.cmsExit.Text = "Esci"
        '
        'tmrRefresh
        '
        '
        'tmrLog
        '
        '
        'tmrBackup
        '
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(884, 601)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MinimumSize = New System.Drawing.Size(900, 640)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Node-RED Desktop"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.tabDashboard.ResumeLayout(False)
        Me.pnlUpdateBanner.ResumeLayout(False)
        Me.pnlUpdateBanner.PerformLayout()
        Me.pnlResourcesCard.ResumeLayout(False)
        Me.pnlResourcesCard.PerformLayout()
        Me.pnlOllamaCard.ResumeLayout(False)
        Me.pnlOllamaCard.PerformLayout()
        Me.pnlStatusCard.ResumeLayout(False)
        Me.pnlStatusCard.PerformLayout()
        Me.tabOllama.ResumeLayout(False)
        Me.tabOllama.PerformLayout()
        Me.grpTestChat.ResumeLayout(False)
        Me.grpTestChat.PerformLayout()
        Me.grpOllamaOpt.ResumeLayout(False)
        Me.grpOllamaModels.ResumeLayout(False)
        Me.grpOllamaModels.PerformLayout()
        Me.grpOllamaService.ResumeLayout(False)
        Me.grpOllamaService.PerformLayout()
        Me.tabEnvironment.ResumeLayout(False)
        Me.tabEnvironment.PerformLayout()
        Me.tlpEnvironment.ResumeLayout(False)
        Me.grpNodeJs.ResumeLayout(False)
        Me.grpNodeJs.PerformLayout()
        Me.grpNpm.ResumeLayout(False)
        Me.grpNpm.PerformLayout()
        Me.grpNodeRed.ResumeLayout(False)
        Me.grpNodeRed.PerformLayout()
        Me.grpOllama.ResumeLayout(False)
        Me.grpOllama.PerformLayout()
        Me.tabLog.ResumeLayout(False)
        Me.tabLog.PerformLayout()
        Me.tsLog.ResumeLayout(False)
        Me.tsLog.PerformLayout()
        Me.tabBackup.ResumeLayout(False)
        Me.tabBackup.PerformLayout()
        Me.grpBackupConfig.ResumeLayout(False)
        Me.grpBackupConfig.PerformLayout()
        CType(Me.nudMaxBackups, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSecurity.ResumeLayout(False)
        Me.tabSecurity.PerformLayout()
        Me.grpLdap.ResumeLayout(False)
        Me.grpLdap.PerformLayout()
        Me.grpIpRestriction.ResumeLayout(False)
        Me.grpIpRestriction.PerformLayout()
        Me.grpUsers.ResumeLayout(False)
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAuthToggle.ResumeLayout(False)
        Me.grpAuthToggle.PerformLayout()
        Me.tabSettings.ResumeLayout(False)
        Me.tabSettings.PerformLayout()
        Me.grpWatchdog.ResumeLayout(False)
        Me.grpWatchdog.PerformLayout()
        CType(Me.nudMaxRestarts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWatchdogInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpStartup.ResumeLayout(False)
        Me.grpStartup.PerformLayout()
        CType(Me.nudStartDelay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpNodeRedConfig.ResumeLayout(False)
        Me.grpNodeRedConfig.PerformLayout()
        CType(Me.nudPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.cmsNotifyIcon.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#Region "Dichiarazioni Controlli (Friend WithEvents)"

    '-- Header --
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblAppTitle As System.Windows.Forms.Label
    Friend WithEvents lblAppVersion As System.Windows.Forms.Label
    Friend WithEvents pnlOnlineIndicator As System.Windows.Forms.Panel
    Friend WithEvents lblOnlineStatus As System.Windows.Forms.Label
    Friend WithEvents btnNotifications As System.Windows.Forms.Button
    Friend WithEvents lblNotificationBadge As System.Windows.Forms.Label

    '-- TabControl --
    Friend WithEvents tabMain As System.Windows.Forms.TabControl
    Friend WithEvents tabDashboard As System.Windows.Forms.TabPage
    Friend WithEvents tabEnvironment As System.Windows.Forms.TabPage
    Friend WithEvents tabLog As System.Windows.Forms.TabPage
    Friend WithEvents tabBackup As System.Windows.Forms.TabPage
    Friend WithEvents tabSecurity As System.Windows.Forms.TabPage
    Friend WithEvents tabSettings As System.Windows.Forms.TabPage

    '-- Dashboard --
    Friend WithEvents pnlStatusCard As System.Windows.Forms.Panel
    Friend WithEvents lblStatusHeader As System.Windows.Forms.Label
    Friend WithEvents lblNodeRedStatusValue As System.Windows.Forms.Label
    Friend WithEvents lblPidLabel As System.Windows.Forms.Label
    Friend WithEvents lblPidValue As System.Windows.Forms.Label
    Friend WithEvents lblUptimeLabel As System.Windows.Forms.Label
    Friend WithEvents lblUptimeValue As System.Windows.Forms.Label
    Friend WithEvents lblPortLabel As System.Windows.Forms.Label
    Friend WithEvents lblPortValue As System.Windows.Forms.Label
    Friend WithEvents lnkOpenBrowser As System.Windows.Forms.LinkLabel
    Friend WithEvents pnlResourcesCard As System.Windows.Forms.Panel
    Friend WithEvents lblResourcesHeader As System.Windows.Forms.Label
    Friend WithEvents lblCpuLabel As System.Windows.Forms.Label
    Friend WithEvents lblCpuValue As System.Windows.Forms.Label
    Friend WithEvents pgbCpu As System.Windows.Forms.ProgressBar
    Friend WithEvents lblRamLabel As System.Windows.Forms.Label
    Friend WithEvents lblRamValue As System.Windows.Forms.Label
    Friend WithEvents pgbRam As System.Windows.Forms.ProgressBar
    Friend WithEvents lblRestartLabel As System.Windows.Forms.Label
    Friend WithEvents lblRestartValue As System.Windows.Forms.Label
    Friend WithEvents btnStartNodeRed As System.Windows.Forms.Button
    Friend WithEvents btnStopNodeRed As System.Windows.Forms.Button
    Friend WithEvents btnRestartNodeRed As System.Windows.Forms.Button
    Friend WithEvents btnOpenBrowser As System.Windows.Forms.Button
    Friend WithEvents lblUrlLabel As System.Windows.Forms.Label
    Friend WithEvents lnkNodeRedUrl As System.Windows.Forms.LinkLabel
    Friend WithEvents rtbDashLog As System.Windows.Forms.RichTextBox

    '-- Environment --
    Friend WithEvents lblEnvTitle As System.Windows.Forms.Label
    Friend WithEvents lblEnvSubtitle As System.Windows.Forms.Label
    Friend WithEvents tlpEnvironment As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grpNodeJs As System.Windows.Forms.GroupBox
    Friend WithEvents lblNodejsStatusIcon As System.Windows.Forms.Label
    Friend WithEvents lblNodejsVersion As System.Windows.Forms.Label
    Friend WithEvents lblNodejsPath As System.Windows.Forms.Label
    Friend WithEvents grpNpm As System.Windows.Forms.GroupBox
    Friend WithEvents lblNpmStatusIcon As System.Windows.Forms.Label
    Friend WithEvents lblNpmVersion As System.Windows.Forms.Label
    Friend WithEvents lblNpmPath As System.Windows.Forms.Label
    Friend WithEvents grpNodeRed As System.Windows.Forms.GroupBox
    Friend WithEvents lblNodeRedStatusIcon As System.Windows.Forms.Label
    Friend WithEvents lblNodeRedVersion As System.Windows.Forms.Label
    Friend WithEvents lblNodeRedPath As System.Windows.Forms.Label
    Friend WithEvents grpOllama As System.Windows.Forms.GroupBox
    Friend WithEvents lblOllamaEnvIcon As System.Windows.Forms.Label
    Friend WithEvents lblOllamaEnvVersion As System.Windows.Forms.Label
    Friend WithEvents lblOllamaEnvPath As System.Windows.Forms.Label
    Friend WithEvents btnInstallNodeJs As System.Windows.Forms.Button
    Friend WithEvents btnInstallNodeRed As System.Windows.Forms.Button
    Friend WithEvents btnInstallOllama As System.Windows.Forms.Button
    Friend WithEvents btnCheckEnvironment As System.Windows.Forms.Button
    Friend WithEvents btnUpdateNodeRed As System.Windows.Forms.Button
    Friend WithEvents btnSetupSuiteWizard As System.Windows.Forms.Button
    Friend WithEvents pgbEnvProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents rtbEnvOutput As System.Windows.Forms.RichTextBox

    '-- Log --
    Friend WithEvents tsLog As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbPauseLog As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbClearLog As System.Windows.Forms.ToolStripButton
    Friend WithEvents sep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbExportTxt As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExportCsv As System.Windows.Forms.ToolStripButton
    Friend WithEvents sep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblFilter As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbLogFilter As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents sep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbAutoScroll As System.Windows.Forms.ToolStripButton
    Friend WithEvents rtbLog As System.Windows.Forms.RichTextBox

    '-- Backup --
    Friend WithEvents lblBackupTitle As System.Windows.Forms.Label
    Friend WithEvents grpBackupConfig As System.Windows.Forms.GroupBox
    Friend WithEvents lblBackupFolder As System.Windows.Forms.Label
    Friend WithEvents txtBackupFolder As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseBackup As System.Windows.Forms.Button
    Friend WithEvents lblSchedule As System.Windows.Forms.Label
    Friend WithEvents cmbBackupSchedule As System.Windows.Forms.ComboBox
    Friend WithEvents lblMaxBackups As System.Windows.Forms.Label
    Friend WithEvents nudMaxBackups As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnSaveBackupConfig As System.Windows.Forms.Button
    Friend WithEvents btnBackupNow As System.Windows.Forms.Button
    Friend WithEvents btnRestoreBackup As System.Windows.Forms.Button
    Friend WithEvents btnDeleteBackup As System.Windows.Forms.Button
    Friend WithEvents btnOpenBackupFolder As System.Windows.Forms.Button
    Friend WithEvents lvBackups As System.Windows.Forms.ListView
    Friend WithEvents colBackupName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colBackupDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents colBackupSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents colBackupStatus As System.Windows.Forms.ColumnHeader

    '-- Security --
    Friend WithEvents lblSecTitle As System.Windows.Forms.Label
    Friend WithEvents lblSecSubtitle As System.Windows.Forms.Label
    Friend WithEvents grpAuthToggle As System.Windows.Forms.GroupBox
    Friend WithEvents chkEnableAuth As System.Windows.Forms.CheckBox
    Friend WithEvents lblAuthInfo As System.Windows.Forms.Label
    Friend WithEvents grpUsers As System.Windows.Forms.GroupBox
    Friend WithEvents dgvUsers As System.Windows.Forms.DataGridView
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
    Friend WithEvents btnEditUser As System.Windows.Forms.Button
    Friend WithEvents btnDeleteUser As System.Windows.Forms.Button
    Friend WithEvents grpIpRestriction As System.Windows.Forms.GroupBox
    Friend WithEvents chkEnableIpRestriction As System.Windows.Forms.CheckBox
    Friend WithEvents lstAllowedIps As System.Windows.Forms.ListBox
    Friend WithEvents txtNewIp As System.Windows.Forms.TextBox
    Friend WithEvents btnAddIp As System.Windows.Forms.Button
    Friend WithEvents btnRemoveIp As System.Windows.Forms.Button
    Friend WithEvents grpLdap As System.Windows.Forms.GroupBox
    Friend WithEvents chkEnableLdap As System.Windows.Forms.CheckBox
    Friend WithEvents lblLdapUrl As System.Windows.Forms.Label
    Friend WithEvents txtLdapUrl As System.Windows.Forms.TextBox
    Friend WithEvents lblLdapBaseDn As System.Windows.Forms.Label
    Friend WithEvents txtLdapBaseDn As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveSecurity As System.Windows.Forms.Button
    Friend WithEvents btnOpenSettingsFile As System.Windows.Forms.Button

    '-- Settings --
    Friend WithEvents lblSettingsTitle As System.Windows.Forms.Label
    Friend WithEvents grpNodeRedConfig As System.Windows.Forms.GroupBox
    Friend WithEvents lblPort As System.Windows.Forms.Label
    Friend WithEvents nudPort As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPortHint As System.Windows.Forms.Label
    Friend WithEvents lblUserDir As System.Windows.Forms.Label
    Friend WithEvents txtUserDir As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseUserDir As System.Windows.Forms.Button
    Friend WithEvents lblFlowFile As System.Windows.Forms.Label
    Friend WithEvents txtFlowFile As System.Windows.Forms.TextBox
    Friend WithEvents lblFlowFileHint As System.Windows.Forms.Label
    Friend WithEvents lblNodeRedCmd As System.Windows.Forms.Label
    Friend WithEvents txtNodeRedCmd As System.Windows.Forms.TextBox
    Friend WithEvents btnDetectPaths As System.Windows.Forms.Button
    Friend WithEvents grpStartup As System.Windows.Forms.GroupBox
    Friend WithEvents chkAutoStartWindows As System.Windows.Forms.CheckBox
    Friend WithEvents lblStartupMethod As System.Windows.Forms.Label
    Friend WithEvents cmbStartupMethod As System.Windows.Forms.ComboBox
    Friend WithEvents lblMethodHint As System.Windows.Forms.Label
    Friend WithEvents chkAutoStartNodeRed As System.Windows.Forms.CheckBox
    Friend WithEvents lblStartDelay As System.Windows.Forms.Label
    Friend WithEvents nudStartDelay As System.Windows.Forms.NumericUpDown
    Friend WithEvents grpWatchdog As System.Windows.Forms.GroupBox
    Friend WithEvents chkWatchdog As System.Windows.Forms.CheckBox
    Friend WithEvents lblWatchInterval As System.Windows.Forms.Label
    Friend WithEvents nudWatchdogInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMaxRestarts As System.Windows.Forms.Label
    Friend WithEvents nudMaxRestarts As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMaxRestartsHint As System.Windows.Forms.Label
    Friend WithEvents chkMinimizeToTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkStartMinimized As System.Windows.Forms.CheckBox
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents btnResetSettings As System.Windows.Forms.Button

    '-- StatusStrip --
    Friend WithEvents ssMain As System.Windows.Forms.StatusStrip
    Friend WithEvents tssAppStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssSpring As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssNodeRedStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tssUptime As System.Windows.Forms.ToolStripStatusLabel

    '-- NotifyIcon & Menu --
    Friend WithEvents notifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents cmsNotifyIcon As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsStart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsRestart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsBrowser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsExit As System.Windows.Forms.ToolStripMenuItem

    '-- Timers --
    Friend WithEvents tmrRefresh As System.Windows.Forms.Timer
    Friend WithEvents tmrLog As System.Windows.Forms.Timer
    Friend WithEvents tmrBackup As System.Windows.Forms.Timer

    '-- Suite Controls --
    Friend WithEvents btnStartSuite As System.Windows.Forms.Button
    Friend WithEvents btnStopSuite As System.Windows.Forms.Button
    Friend WithEvents btnRestartSuite As System.Windows.Forms.Button
    Friend WithEvents btnCheckUpdates As System.Windows.Forms.Button

    '-- Ollama Dashboard Card --
    Friend WithEvents pnlOllamaCard As System.Windows.Forms.Panel
    Friend WithEvents lblOllamaHeader As System.Windows.Forms.Label
    Friend WithEvents lblOllamaStatusValue As System.Windows.Forms.Label
    Friend WithEvents lblOllamaModelLabel As System.Windows.Forms.Label
    Friend WithEvents lblOllamaModelValue As System.Windows.Forms.Label
    Friend WithEvents lblOllamaPortLabel As System.Windows.Forms.Label
    Friend WithEvents lblOllamaPortValue As System.Windows.Forms.Label
    Friend WithEvents lblOllamaOptLabel As System.Windows.Forms.Label
    Friend WithEvents lblOllamaOptValue As System.Windows.Forms.Label
    Friend WithEvents btnStartOllamaQuick As System.Windows.Forms.Button
    Friend WithEvents btnStopOllamaQuick As System.Windows.Forms.Button

    '-- Update Banner Dashboard --
    Friend WithEvents pnlUpdateBanner As System.Windows.Forms.Panel
    Friend WithEvents lblUpdateBannerText As System.Windows.Forms.Label
    Friend WithEvents btnUpdateNowQuick As System.Windows.Forms.Button
    Friend WithEvents lnkChangelogQuick As System.Windows.Forms.LinkLabel

    '-- Tab Ollama & AI --
    Friend WithEvents tabOllama As System.Windows.Forms.TabPage
    Friend WithEvents lblOllamaTitle As System.Windows.Forms.Label
    Friend WithEvents lblOllamaSubtitle As System.Windows.Forms.Label
    Friend WithEvents grpOllamaService As System.Windows.Forms.GroupBox
    Friend WithEvents lblOllamaSvcStatus As System.Windows.Forms.Label
    Friend WithEvents lblOllamaSvcPort As System.Windows.Forms.Label
    Friend WithEvents btnStartOllama As System.Windows.Forms.Button
    Friend WithEvents btnStopOllama As System.Windows.Forms.Button
    Friend WithEvents btnRestartOllama As System.Windows.Forms.Button
    Friend WithEvents grpOllamaModels As System.Windows.Forms.GroupBox
    Friend WithEvents lblSelectModel As System.Windows.Forms.Label
    Friend WithEvents cmbOllamaModels As System.Windows.Forms.ComboBox
    Friend WithEvents btnRefreshModels As System.Windows.Forms.Button
    Friend WithEvents btnPullModelLight As System.Windows.Forms.Button
    Friend WithEvents btnPullModelMini As System.Windows.Forms.Button
    Friend WithEvents grpOllamaOpt As System.Windows.Forms.GroupBox
    Friend WithEvents lblOptDesc As System.Windows.Forms.Label
    Friend WithEvents btnApplyOptimizations As System.Windows.Forms.Button
    Friend WithEvents grpTestChat As System.Windows.Forms.GroupBox
    Friend WithEvents lblTestPrompt As System.Windows.Forms.Label
    Friend WithEvents txtTestPrompt As System.Windows.Forms.TextBox
    Friend WithEvents btnTestChat As System.Windows.Forms.Button
    Friend WithEvents txtTestReply As System.Windows.Forms.RichTextBox
    Friend WithEvents lblTestStats As System.Windows.Forms.Label
    Friend WithEvents btnRestartOllamaQuick As Button

#End Region

End Class
