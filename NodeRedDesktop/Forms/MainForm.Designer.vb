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
    Inherits System.Windows.Forms.Form

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
        Me.rtbDashLog = New System.Windows.Forms.RichTextBox()
        Me.lblRecentLogTitle = New System.Windows.Forms.Label()
        Me.lnkNodeRedUrl = New System.Windows.Forms.LinkLabel()
        Me.lblUrlLabel = New System.Windows.Forms.Label()
        Me.pnlDashButtons = New System.Windows.Forms.Panel()
        Me.btnOpenBrowser = New System.Windows.Forms.Button()
        Me.btnRestart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.pnlResourcesCard = New System.Windows.Forms.Panel()
        Me.lblRestartValue = New System.Windows.Forms.Label()
        Me.lblRestartLabel = New System.Windows.Forms.Label()
        Me.pgbRam = New System.Windows.Forms.ProgressBar()
        Me.lblRamValue = New System.Windows.Forms.Label()
        Me.lblRamLabel = New System.Windows.Forms.Label()
        Me.pgbCpu = New System.Windows.Forms.ProgressBar()
        Me.lblCpuValue = New System.Windows.Forms.Label()
        Me.lblCpuLabel = New System.Windows.Forms.Label()
        Me.lblResourcesHeader = New System.Windows.Forms.Label()
        Me.pnlStatusCard = New System.Windows.Forms.Panel()
        Me.lnkOpenBrowser = New System.Windows.Forms.LinkLabel()
        Me.lblPortValue = New System.Windows.Forms.Label()
        Me.lblPortLabel = New System.Windows.Forms.Label()
        Me.lblUptimeValue = New System.Windows.Forms.Label()
        Me.lblUptimeLabel = New System.Windows.Forms.Label()
        Me.lblPidValue = New System.Windows.Forms.Label()
        Me.lblPidLabel = New System.Windows.Forms.Label()
        Me.lblStatusValue = New System.Windows.Forms.Label()
        Me.lblStatusHeader = New System.Windows.Forms.Label()
        Me.tabEnvironment = New System.Windows.Forms.TabPage()
        Me.rtbEnvOutput = New System.Windows.Forms.RichTextBox()
        Me.pgbEnvProgress = New System.Windows.Forms.ProgressBar()
        Me.pnlEnvButtons = New System.Windows.Forms.Panel()
        Me.btnUpdateNodeRed = New System.Windows.Forms.Button()
        Me.btnInstallNodeRed = New System.Windows.Forms.Button()
        Me.btnCheckEnvironment = New System.Windows.Forms.Button()
        Me.grpNodeRed = New System.Windows.Forms.GroupBox()
        Me.lblNodeRedPath = New System.Windows.Forms.Label()
        Me.lblNodeRedVersion = New System.Windows.Forms.Label()
        Me.lblNodeRedStatusIcon = New System.Windows.Forms.Label()
        Me.grpNpm = New System.Windows.Forms.GroupBox()
        Me.lblNpmPath = New System.Windows.Forms.Label()
        Me.lblNpmVersion = New System.Windows.Forms.Label()
        Me.lblNpmStatusIcon = New System.Windows.Forms.Label()
        Me.grpNodeJs = New System.Windows.Forms.GroupBox()
        Me.lblNodejsPath = New System.Windows.Forms.Label()
        Me.lblNodejsVersion = New System.Windows.Forms.Label()
        Me.lblNodejsStatusIcon = New System.Windows.Forms.Label()
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
        Me.lvBackups = New System.Windows.Forms.ListView()
        Me.colBackupName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBackupDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBackupSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBackupStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblBackupListTitle = New System.Windows.Forms.Label()
        Me.pnlBackupActions = New System.Windows.Forms.Panel()
        Me.btnOpenBackupFolder = New System.Windows.Forms.Button()
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
        Me.pnlSecurityButtons = New System.Windows.Forms.Panel()
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
        Me.pnlSettingsButtons = New System.Windows.Forms.Panel()
        Me.btnResetSettings = New System.Windows.Forms.Button()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.grpMinimize = New System.Windows.Forms.GroupBox()
        Me.chkStartMinimized = New System.Windows.Forms.CheckBox()
        Me.chkMinimizeToTray = New System.Windows.Forms.CheckBox()
        Me.grpWatchdog = New System.Windows.Forms.GroupBox()
        Me.lblMaxRestartsHint = New System.Windows.Forms.Label()
        Me.nudMaxRestarts = New System.Windows.Forms.NumericUpDown()
        Me.lblMaxRestarts = New System.Windows.Forms.Label()
        Me.nudWatchdogInterval = New System.Windows.Forms.NumericUpDown()
        Me.lblWatchInterval = New System.Windows.Forms.Label()
        Me.chkWatchdog = New System.Windows.Forms.CheckBox()
        Me.grpStartup = New System.Windows.Forms.GroupBox()
        Me.nudStartDelay = New System.Windows.Forms.NumericUpDown()
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
        Me.pnlDashButtons.SuspendLayout()
        Me.pnlResourcesCard.SuspendLayout()
        Me.pnlStatusCard.SuspendLayout()
        Me.tabEnvironment.SuspendLayout()
        Me.pnlEnvButtons.SuspendLayout()
        Me.grpNodeRed.SuspendLayout()
        Me.grpNpm.SuspendLayout()
        Me.grpNodeJs.SuspendLayout()
        Me.tabLog.SuspendLayout()
        Me.tsLog.SuspendLayout()
        Me.tabBackup.SuspendLayout()
        Me.pnlBackupActions.SuspendLayout()
        Me.grpBackupConfig.SuspendLayout()
        CType(Me.nudMaxBackups, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSecurity.SuspendLayout()
        Me.pnlSecurityButtons.SuspendLayout()
        Me.grpLdap.SuspendLayout()
        Me.grpIpRestriction.SuspendLayout()
        Me.grpUsers.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAuthToggle.SuspendLayout()
        Me.tabSettings.SuspendLayout()
        Me.pnlSettingsButtons.SuspendLayout()
        Me.grpMinimize.SuspendLayout()
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
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(27, Byte), Integer))
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
        Me.pnlHeader.Size = New System.Drawing.Size(1024, 60)
        Me.pnlHeader.TabIndex = 0
        '
        'lblNotificationBadge
        '
        Me.lblNotificationBadge.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNotificationBadge.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.lblNotificationBadge.Font = New System.Drawing.Font("Segoe UI", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblNotificationBadge.ForeColor = System.Drawing.Color.White
        Me.lblNotificationBadge.Location = New System.Drawing.Point(946, 7)
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
        Me.btnNotifications.BackColor = System.Drawing.Color.Transparent
        Me.btnNotifications.FlatAppearance.BorderSize = 0
        Me.btnNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNotifications.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.btnNotifications.ForeColor = System.Drawing.Color.White
        Me.btnNotifications.Location = New System.Drawing.Point(920, 12)
        Me.btnNotifications.Name = "btnNotifications"
        Me.btnNotifications.Size = New System.Drawing.Size(36, 36)
        Me.btnNotifications.TabIndex = 5
        Me.btnNotifications.Text = "!"
        Me.btnNotifications.UseVisualStyleBackColor = False
        '
        'lblOnlineStatus
        '
        Me.lblOnlineStatus.AutoSize = True
        Me.lblOnlineStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblOnlineStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblOnlineStatus.Location = New System.Drawing.Point(263, 22)
        Me.lblOnlineStatus.Name = "lblOnlineStatus"
        Me.lblOnlineStatus.Size = New System.Drawing.Size(43, 15)
        Me.lblOnlineStatus.TabIndex = 4
        Me.lblOnlineStatus.Text = "Offline"
        '
        'pnlOnlineIndicator
        '
        Me.pnlOnlineIndicator.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.pnlOnlineIndicator.Location = New System.Drawing.Point(245, 24)
        Me.pnlOnlineIndicator.Name = "pnlOnlineIndicator"
        Me.pnlOnlineIndicator.Size = New System.Drawing.Size(12, 12)
        Me.pnlOnlineIndicator.TabIndex = 3
        '
        'lblAppVersion
        '
        Me.lblAppVersion.AutoSize = True
        Me.lblAppVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAppVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblAppVersion.Location = New System.Drawing.Point(65, 37)
        Me.lblAppVersion.Name = "lblAppVersion"
        Me.lblAppVersion.Size = New System.Drawing.Size(28, 15)
        Me.lblAppVersion.TabIndex = 2
        Me.lblAppVersion.Text = "v1.0"
        '
        'lblAppTitle
        '
        Me.lblAppTitle.AutoSize = True
        Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblAppTitle.ForeColor = System.Drawing.Color.White
        Me.lblAppTitle.Location = New System.Drawing.Point(65, 10)
        Me.lblAppTitle.Name = "lblAppTitle"
        Me.lblAppTitle.Size = New System.Drawing.Size(184, 25)
        Me.lblAppTitle.TabIndex = 1
        Me.lblAppTitle.Text = "Node-RED Desktop"
        '
        'picLogo
        '
        Me.picLogo.Location = New System.Drawing.Point(15, 10)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(40, 40)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLogo.TabIndex = 0
        Me.picLogo.TabStop = False
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabDashboard)
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
        Me.tabMain.Size = New System.Drawing.Size(1024, 617)
        Me.tabMain.TabIndex = 1
        '
        'tabDashboard
        '
        Me.tabDashboard.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.tabDashboard.Controls.Add(Me.rtbDashLog)
        Me.tabDashboard.Controls.Add(Me.lblRecentLogTitle)
        Me.tabDashboard.Controls.Add(Me.lnkNodeRedUrl)
        Me.tabDashboard.Controls.Add(Me.lblUrlLabel)
        Me.tabDashboard.Controls.Add(Me.pnlDashButtons)
        Me.tabDashboard.Controls.Add(Me.pnlResourcesCard)
        Me.tabDashboard.Controls.Add(Me.pnlStatusCard)
        Me.tabDashboard.Location = New System.Drawing.Point(4, 24)
        Me.tabDashboard.Name = "tabDashboard"
        Me.tabDashboard.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDashboard.Size = New System.Drawing.Size(1016, 589)
        Me.tabDashboard.TabIndex = 0
        Me.tabDashboard.Text = "Dashboard"
        '
        'rtbDashLog
        '
        Me.rtbDashLog.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.rtbDashLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbDashLog.Font = New System.Drawing.Font("Consolas", 8.0!)
        Me.rtbDashLog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.rtbDashLog.Location = New System.Drawing.Point(20, 330)
        Me.rtbDashLog.Name = "rtbDashLog"
        Me.rtbDashLog.ReadOnly = True
        Me.rtbDashLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbDashLog.Size = New System.Drawing.Size(580, 160)
        Me.rtbDashLog.TabIndex = 6
        Me.rtbDashLog.Text = ""
        '
        'lblRecentLogTitle
        '
        Me.lblRecentLogTitle.AutoSize = True
        Me.lblRecentLogTitle.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblRecentLogTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblRecentLogTitle.Location = New System.Drawing.Point(20, 310)
        Me.lblRecentLogTitle.Name = "lblRecentLogTitle"
        Me.lblRecentLogTitle.Size = New System.Drawing.Size(79, 13)
        Me.lblRecentLogTitle.TabIndex = 5
        Me.lblRecentLogTitle.Text = "LOG RECENTE"
        '
        'lnkNodeRedUrl
        '
        Me.lnkNodeRedUrl.AutoSize = True
        Me.lnkNodeRedUrl.LinkColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.lnkNodeRedUrl.Location = New System.Drawing.Point(100, 275)
        Me.lnkNodeRedUrl.Name = "lnkNodeRedUrl"
        Me.lnkNodeRedUrl.Size = New System.Drawing.Size(114, 15)
        Me.lnkNodeRedUrl.TabIndex = 4
        Me.lnkNodeRedUrl.TabStop = True
        Me.lnkNodeRedUrl.Text = "http://127.0.0.1:1880"
        '
        'lblUrlLabel
        '
        Me.lblUrlLabel.AutoSize = True
        Me.lblUrlLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblUrlLabel.Location = New System.Drawing.Point(20, 275)
        Me.lblUrlLabel.Name = "lblUrlLabel"
        Me.lblUrlLabel.Size = New System.Drawing.Size(65, 15)
        Me.lblUrlLabel.TabIndex = 3
        Me.lblUrlLabel.Text = "URL Editor:"
        '
        'pnlDashButtons
        '
        Me.pnlDashButtons.BackColor = System.Drawing.Color.Transparent
        Me.pnlDashButtons.Controls.Add(Me.btnOpenBrowser)
        Me.pnlDashButtons.Controls.Add(Me.btnRestart)
        Me.pnlDashButtons.Controls.Add(Me.btnStop)
        Me.pnlDashButtons.Controls.Add(Me.btnStart)
        Me.pnlDashButtons.Location = New System.Drawing.Point(20, 200)
        Me.pnlDashButtons.Name = "pnlDashButtons"
        Me.pnlDashButtons.Size = New System.Drawing.Size(580, 60)
        Me.pnlDashButtons.TabIndex = 2
        '
        'btnOpenBrowser
        '
        Me.btnOpenBrowser.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnOpenBrowser.FlatAppearance.BorderSize = 0
        Me.btnOpenBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenBrowser.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnOpenBrowser.ForeColor = System.Drawing.Color.White
        Me.btnOpenBrowser.Location = New System.Drawing.Point(435, 0)
        Me.btnOpenBrowser.Name = "btnOpenBrowser"
        Me.btnOpenBrowser.Size = New System.Drawing.Size(130, 45)
        Me.btnOpenBrowser.TabIndex = 3
        Me.btnOpenBrowser.Text = "Apri nel Browser"
        Me.btnOpenBrowser.UseVisualStyleBackColor = False
        '
        'btnRestart
        '
        Me.btnRestart.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnRestart.FlatAppearance.BorderSize = 0
        Me.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestart.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnRestart.ForeColor = System.Drawing.Color.Black
        Me.btnRestart.Location = New System.Drawing.Point(290, 0)
        Me.btnRestart.Name = "btnRestart"
        Me.btnRestart.Size = New System.Drawing.Size(130, 45)
        Me.btnRestart.TabIndex = 2
        Me.btnRestart.Text = "↺  Riavvia"
        Me.btnRestart.UseVisualStyleBackColor = False
        '
        'btnStop
        '
        Me.btnStop.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnStop.FlatAppearance.BorderSize = 0
        Me.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStop.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnStop.ForeColor = System.Drawing.Color.White
        Me.btnStop.Location = New System.Drawing.Point(145, 0)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(130, 45)
        Me.btnStop.TabIndex = 1
        Me.btnStop.Text = "■  Ferma"
        Me.btnStop.UseVisualStyleBackColor = False
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnStart.FlatAppearance.BorderSize = 0
        Me.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStart.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnStart.ForeColor = System.Drawing.Color.White
        Me.btnStart.Location = New System.Drawing.Point(0, 0)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(130, 45)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "▶  Avvia"
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'pnlResourcesCard
        '
        Me.pnlResourcesCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.pnlResourcesCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlResourcesCard.Controls.Add(Me.lblRestartValue)
        Me.pnlResourcesCard.Controls.Add(Me.lblRestartLabel)
        Me.pnlResourcesCard.Controls.Add(Me.pgbRam)
        Me.pnlResourcesCard.Controls.Add(Me.lblRamValue)
        Me.pnlResourcesCard.Controls.Add(Me.lblRamLabel)
        Me.pnlResourcesCard.Controls.Add(Me.pgbCpu)
        Me.pnlResourcesCard.Controls.Add(Me.lblCpuValue)
        Me.pnlResourcesCard.Controls.Add(Me.lblCpuLabel)
        Me.pnlResourcesCard.Controls.Add(Me.lblResourcesHeader)
        Me.pnlResourcesCard.Location = New System.Drawing.Point(300, 20)
        Me.pnlResourcesCard.Name = "pnlResourcesCard"
        Me.pnlResourcesCard.Size = New System.Drawing.Size(260, 160)
        Me.pnlResourcesCard.TabIndex = 1
        '
        'lblRestartValue
        '
        Me.lblRestartValue.AutoSize = True
        Me.lblRestartValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblRestartValue.Location = New System.Drawing.Point(100, 116)
        Me.lblRestartValue.Name = "lblRestartValue"
        Me.lblRestartValue.Size = New System.Drawing.Size(13, 15)
        Me.lblRestartValue.TabIndex = 8
        Me.lblRestartValue.Text = "0"
        '
        'lblRestartLabel
        '
        Me.lblRestartLabel.AutoSize = True
        Me.lblRestartLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblRestartLabel.Location = New System.Drawing.Point(15, 116)
        Me.lblRestartLabel.Name = "lblRestartLabel"
        Me.lblRestartLabel.Size = New System.Drawing.Size(66, 15)
        Me.lblRestartLabel.TabIndex = 7
        Me.lblRestartLabel.Text = "Riavvii WD:"
        '
        'pgbRam
        '
        Me.pgbRam.Location = New System.Drawing.Point(15, 96)
        Me.pgbRam.Maximum = 1000
        Me.pgbRam.Name = "pgbRam"
        Me.pgbRam.Size = New System.Drawing.Size(200, 8)
        Me.pgbRam.TabIndex = 6
        '
        'lblRamValue
        '
        Me.lblRamValue.AutoSize = True
        Me.lblRamValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblRamValue.Location = New System.Drawing.Point(60, 78)
        Me.lblRamValue.Name = "lblRamValue"
        Me.lblRamValue.Size = New System.Drawing.Size(19, 15)
        Me.lblRamValue.TabIndex = 5
        Me.lblRamValue.Text = "—"
        '
        'lblRamLabel
        '
        Me.lblRamLabel.AutoSize = True
        Me.lblRamLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblRamLabel.Location = New System.Drawing.Point(15, 78)
        Me.lblRamLabel.Name = "lblRamLabel"
        Me.lblRamLabel.Size = New System.Drawing.Size(36, 15)
        Me.lblRamLabel.TabIndex = 4
        Me.lblRamLabel.Text = "RAM:"
        '
        'pgbCpu
        '
        Me.pgbCpu.Location = New System.Drawing.Point(15, 58)
        Me.pgbCpu.Name = "pgbCpu"
        Me.pgbCpu.Size = New System.Drawing.Size(200, 8)
        Me.pgbCpu.TabIndex = 3
        '
        'lblCpuValue
        '
        Me.lblCpuValue.AutoSize = True
        Me.lblCpuValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblCpuValue.Location = New System.Drawing.Point(60, 40)
        Me.lblCpuValue.Name = "lblCpuValue"
        Me.lblCpuValue.Size = New System.Drawing.Size(19, 15)
        Me.lblCpuValue.TabIndex = 2
        Me.lblCpuValue.Text = "—"
        '
        'lblCpuLabel
        '
        Me.lblCpuLabel.AutoSize = True
        Me.lblCpuLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblCpuLabel.Location = New System.Drawing.Point(15, 40)
        Me.lblCpuLabel.Name = "lblCpuLabel"
        Me.lblCpuLabel.Size = New System.Drawing.Size(33, 15)
        Me.lblCpuLabel.TabIndex = 1
        Me.lblCpuLabel.Text = "CPU:"
        '
        'lblResourcesHeader
        '
        Me.lblResourcesHeader.AutoSize = True
        Me.lblResourcesHeader.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblResourcesHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblResourcesHeader.Location = New System.Drawing.Point(15, 12)
        Me.lblResourcesHeader.Name = "lblResourcesHeader"
        Me.lblResourcesHeader.Size = New System.Drawing.Size(50, 13)
        Me.lblResourcesHeader.TabIndex = 0
        Me.lblResourcesHeader.Text = "RISORSE"
        '
        'pnlStatusCard
        '
        Me.pnlStatusCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.pnlStatusCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStatusCard.Controls.Add(Me.lnkOpenBrowser)
        Me.pnlStatusCard.Controls.Add(Me.lblPortValue)
        Me.pnlStatusCard.Controls.Add(Me.lblPortLabel)
        Me.pnlStatusCard.Controls.Add(Me.lblUptimeValue)
        Me.pnlStatusCard.Controls.Add(Me.lblUptimeLabel)
        Me.pnlStatusCard.Controls.Add(Me.lblPidValue)
        Me.pnlStatusCard.Controls.Add(Me.lblPidLabel)
        Me.pnlStatusCard.Controls.Add(Me.lblStatusValue)
        Me.pnlStatusCard.Controls.Add(Me.lblStatusHeader)
        Me.pnlStatusCard.Location = New System.Drawing.Point(20, 20)
        Me.pnlStatusCard.Name = "pnlStatusCard"
        Me.pnlStatusCard.Size = New System.Drawing.Size(260, 160)
        Me.pnlStatusCard.TabIndex = 0
        '
        'lnkOpenBrowser
        '
        Me.lnkOpenBrowser.AutoSize = True
        Me.lnkOpenBrowser.LinkColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
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
        Me.lblPortValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblPortValue.Location = New System.Drawing.Point(60, 115)
        Me.lblPortValue.Name = "lblPortValue"
        Me.lblPortValue.Size = New System.Drawing.Size(31, 15)
        Me.lblPortValue.TabIndex = 7
        Me.lblPortValue.Text = "1880"
        '
        'lblPortLabel
        '
        Me.lblPortLabel.AutoSize = True
        Me.lblPortLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblPortLabel.Location = New System.Drawing.Point(15, 115)
        Me.lblPortLabel.Name = "lblPortLabel"
        Me.lblPortLabel.Size = New System.Drawing.Size(38, 15)
        Me.lblPortLabel.TabIndex = 6
        Me.lblPortLabel.Text = "Porta:"
        '
        'lblUptimeValue
        '
        Me.lblUptimeValue.AutoSize = True
        Me.lblUptimeValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblUptimeValue.Location = New System.Drawing.Point(70, 95)
        Me.lblUptimeValue.Name = "lblUptimeValue"
        Me.lblUptimeValue.Size = New System.Drawing.Size(19, 15)
        Me.lblUptimeValue.TabIndex = 5
        Me.lblUptimeValue.Text = "—"
        '
        'lblUptimeLabel
        '
        Me.lblUptimeLabel.AutoSize = True
        Me.lblUptimeLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblUptimeLabel.Location = New System.Drawing.Point(15, 95)
        Me.lblUptimeLabel.Name = "lblUptimeLabel"
        Me.lblUptimeLabel.Size = New System.Drawing.Size(49, 15)
        Me.lblUptimeLabel.TabIndex = 4
        Me.lblUptimeLabel.Text = "Uptime:"
        '
        'lblPidValue
        '
        Me.lblPidValue.AutoSize = True
        Me.lblPidValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblPidValue.Location = New System.Drawing.Point(60, 75)
        Me.lblPidValue.Name = "lblPidValue"
        Me.lblPidValue.Size = New System.Drawing.Size(19, 15)
        Me.lblPidValue.TabIndex = 3
        Me.lblPidValue.Text = "—"
        '
        'lblPidLabel
        '
        Me.lblPidLabel.AutoSize = True
        Me.lblPidLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblPidLabel.Location = New System.Drawing.Point(15, 75)
        Me.lblPidLabel.Name = "lblPidLabel"
        Me.lblPidLabel.Size = New System.Drawing.Size(28, 15)
        Me.lblPidLabel.TabIndex = 2
        Me.lblPidLabel.Text = "PID:"
        '
        'lblStatusValue
        '
        Me.lblStatusValue.AutoSize = True
        Me.lblStatusValue.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblStatusValue.Location = New System.Drawing.Point(15, 35)
        Me.lblStatusValue.Name = "lblStatusValue"
        Me.lblStatusValue.Size = New System.Drawing.Size(92, 25)
        Me.lblStatusValue.TabIndex = 1
        Me.lblStatusValue.Text = "● FERMO"
        '
        'lblStatusHeader
        '
        Me.lblStatusHeader.AutoSize = True
        Me.lblStatusHeader.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblStatusHeader.Location = New System.Drawing.Point(15, 12)
        Me.lblStatusHeader.Name = "lblStatusHeader"
        Me.lblStatusHeader.Size = New System.Drawing.Size(97, 13)
        Me.lblStatusHeader.TabIndex = 0
        Me.lblStatusHeader.Text = "STATO PROCESSO"
        '
        'tabEnvironment
        '
        Me.tabEnvironment.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.tabEnvironment.Controls.Add(Me.rtbEnvOutput)
        Me.tabEnvironment.Controls.Add(Me.pgbEnvProgress)
        Me.tabEnvironment.Controls.Add(Me.pnlEnvButtons)
        Me.tabEnvironment.Controls.Add(Me.grpNodeRed)
        Me.tabEnvironment.Controls.Add(Me.grpNpm)
        Me.tabEnvironment.Controls.Add(Me.grpNodeJs)
        Me.tabEnvironment.Controls.Add(Me.lblEnvSubtitle)
        Me.tabEnvironment.Controls.Add(Me.lblEnvTitle)
        Me.tabEnvironment.Location = New System.Drawing.Point(4, 24)
        Me.tabEnvironment.Name = "tabEnvironment"
        Me.tabEnvironment.Size = New System.Drawing.Size(1016, 589)
        Me.tabEnvironment.TabIndex = 1
        Me.tabEnvironment.Text = "Ambiente"
        '
        'rtbEnvOutput
        '
        Me.rtbEnvOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.rtbEnvOutput.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbEnvOutput.Font = New System.Drawing.Font("Consolas", 8.0!)
        Me.rtbEnvOutput.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.rtbEnvOutput.Location = New System.Drawing.Point(20, 420)
        Me.rtbEnvOutput.Name = "rtbEnvOutput"
        Me.rtbEnvOutput.ReadOnly = True
        Me.rtbEnvOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbEnvOutput.Size = New System.Drawing.Size(560, 150)
        Me.rtbEnvOutput.TabIndex = 7
        Me.rtbEnvOutput.Text = ""
        '
        'pgbEnvProgress
        '
        Me.pgbEnvProgress.Location = New System.Drawing.Point(20, 405)
        Me.pgbEnvProgress.Name = "pgbEnvProgress"
        Me.pgbEnvProgress.Size = New System.Drawing.Size(560, 8)
        Me.pgbEnvProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pgbEnvProgress.TabIndex = 6
        Me.pgbEnvProgress.Visible = False
        '
        'pnlEnvButtons
        '
        Me.pnlEnvButtons.BackColor = System.Drawing.Color.Transparent
        Me.pnlEnvButtons.Controls.Add(Me.btnUpdateNodeRed)
        Me.pnlEnvButtons.Controls.Add(Me.btnInstallNodeRed)
        Me.pnlEnvButtons.Controls.Add(Me.btnCheckEnvironment)
        Me.pnlEnvButtons.Location = New System.Drawing.Point(20, 345)
        Me.pnlEnvButtons.Name = "pnlEnvButtons"
        Me.pnlEnvButtons.Size = New System.Drawing.Size(560, 50)
        Me.pnlEnvButtons.TabIndex = 5
        '
        'btnUpdateNodeRed
        '
        Me.btnUpdateNodeRed.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnUpdateNodeRed.FlatAppearance.BorderSize = 0
        Me.btnUpdateNodeRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateNodeRed.ForeColor = System.Drawing.Color.White
        Me.btnUpdateNodeRed.Location = New System.Drawing.Point(360, 0)
        Me.btnUpdateNodeRed.Name = "btnUpdateNodeRed"
        Me.btnUpdateNodeRed.Size = New System.Drawing.Size(150, 38)
        Me.btnUpdateNodeRed.TabIndex = 2
        Me.btnUpdateNodeRed.Text = "Aggiorna"
        Me.btnUpdateNodeRed.UseVisualStyleBackColor = False
        '
        'btnInstallNodeRed
        '
        Me.btnInstallNodeRed.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnInstallNodeRed.FlatAppearance.BorderSize = 0
        Me.btnInstallNodeRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstallNodeRed.ForeColor = System.Drawing.Color.White
        Me.btnInstallNodeRed.Location = New System.Drawing.Point(165, 0)
        Me.btnInstallNodeRed.Name = "btnInstallNodeRed"
        Me.btnInstallNodeRed.Size = New System.Drawing.Size(180, 38)
        Me.btnInstallNodeRed.TabIndex = 1
        Me.btnInstallNodeRed.Text = "Installa Node-RED"
        Me.btnInstallNodeRed.UseVisualStyleBackColor = False
        '
        'btnCheckEnvironment
        '
        Me.btnCheckEnvironment.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnCheckEnvironment.FlatAppearance.BorderSize = 0
        Me.btnCheckEnvironment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckEnvironment.ForeColor = System.Drawing.Color.White
        Me.btnCheckEnvironment.Location = New System.Drawing.Point(0, 0)
        Me.btnCheckEnvironment.Name = "btnCheckEnvironment"
        Me.btnCheckEnvironment.Size = New System.Drawing.Size(150, 38)
        Me.btnCheckEnvironment.TabIndex = 0
        Me.btnCheckEnvironment.Text = "Verifica Dipendenze"
        Me.btnCheckEnvironment.UseVisualStyleBackColor = False
        '
        'grpNodeRed
        '
        Me.grpNodeRed.BackColor = System.Drawing.Color.Transparent
        Me.grpNodeRed.Controls.Add(Me.lblNodeRedPath)
        Me.grpNodeRed.Controls.Add(Me.lblNodeRedVersion)
        Me.grpNodeRed.Controls.Add(Me.lblNodeRedStatusIcon)
        Me.grpNodeRed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpNodeRed.Location = New System.Drawing.Point(20, 250)
        Me.grpNodeRed.Name = "grpNodeRed"
        Me.grpNodeRed.Size = New System.Drawing.Size(560, 75)
        Me.grpNodeRed.TabIndex = 4
        Me.grpNodeRed.TabStop = False
        Me.grpNodeRed.Text = "Node-RED"
        '
        'lblNodeRedPath
        '
        Me.lblNodeRedPath.AutoSize = True
        Me.lblNodeRedPath.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblNodeRedPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblNodeRedPath.Location = New System.Drawing.Point(45, 48)
        Me.lblNodeRedPath.Name = "lblNodeRedPath"
        Me.lblNodeRedPath.Size = New System.Drawing.Size(0, 13)
        Me.lblNodeRedPath.TabIndex = 2
        '
        'lblNodeRedVersion
        '
        Me.lblNodeRedVersion.AutoSize = True
        Me.lblNodeRedVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNodeRedVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblNodeRedVersion.Location = New System.Drawing.Point(45, 28)
        Me.lblNodeRedVersion.Name = "lblNodeRedVersion"
        Me.lblNodeRedVersion.Size = New System.Drawing.Size(76, 15)
        Me.lblNodeRedVersion.TabIndex = 1
        Me.lblNodeRedVersion.Text = "Non rilevato"
        '
        'lblNodeRedStatusIcon
        '
        Me.lblNodeRedStatusIcon.AutoSize = True
        Me.lblNodeRedStatusIcon.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblNodeRedStatusIcon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblNodeRedStatusIcon.Location = New System.Drawing.Point(15, 25)
        Me.lblNodeRedStatusIcon.Name = "lblNodeRedStatusIcon"
        Me.lblNodeRedStatusIcon.Size = New System.Drawing.Size(32, 30)
        Me.lblNodeRedStatusIcon.TabIndex = 0
        Me.lblNodeRedStatusIcon.Text = "○"
        '
        'grpNpm
        '
        Me.grpNpm.BackColor = System.Drawing.Color.Transparent
        Me.grpNpm.Controls.Add(Me.lblNpmPath)
        Me.grpNpm.Controls.Add(Me.lblNpmVersion)
        Me.grpNpm.Controls.Add(Me.lblNpmStatusIcon)
        Me.grpNpm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpNpm.Location = New System.Drawing.Point(20, 165)
        Me.grpNpm.Name = "grpNpm"
        Me.grpNpm.Size = New System.Drawing.Size(560, 75)
        Me.grpNpm.TabIndex = 3
        Me.grpNpm.TabStop = False
        Me.grpNpm.Text = "npm"
        '
        'lblNpmPath
        '
        Me.lblNpmPath.AutoSize = True
        Me.lblNpmPath.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblNpmPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblNpmPath.Location = New System.Drawing.Point(45, 48)
        Me.lblNpmPath.Name = "lblNpmPath"
        Me.lblNpmPath.Size = New System.Drawing.Size(0, 13)
        Me.lblNpmPath.TabIndex = 2
        '
        'lblNpmVersion
        '
        Me.lblNpmVersion.AutoSize = True
        Me.lblNpmVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNpmVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblNpmVersion.Location = New System.Drawing.Point(45, 28)
        Me.lblNpmVersion.Name = "lblNpmVersion"
        Me.lblNpmVersion.Size = New System.Drawing.Size(76, 15)
        Me.lblNpmVersion.TabIndex = 1
        Me.lblNpmVersion.Text = "Non rilevato"
        '
        'lblNpmStatusIcon
        '
        Me.lblNpmStatusIcon.AutoSize = True
        Me.lblNpmStatusIcon.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblNpmStatusIcon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblNpmStatusIcon.Location = New System.Drawing.Point(15, 25)
        Me.lblNpmStatusIcon.Name = "lblNpmStatusIcon"
        Me.lblNpmStatusIcon.Size = New System.Drawing.Size(32, 30)
        Me.lblNpmStatusIcon.TabIndex = 0
        Me.lblNpmStatusIcon.Text = "○"
        '
        'grpNodeJs
        '
        Me.grpNodeJs.BackColor = System.Drawing.Color.Transparent
        Me.grpNodeJs.Controls.Add(Me.lblNodejsPath)
        Me.grpNodeJs.Controls.Add(Me.lblNodejsVersion)
        Me.grpNodeJs.Controls.Add(Me.lblNodejsStatusIcon)
        Me.grpNodeJs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpNodeJs.Location = New System.Drawing.Point(20, 80)
        Me.grpNodeJs.Name = "grpNodeJs"
        Me.grpNodeJs.Size = New System.Drawing.Size(560, 75)
        Me.grpNodeJs.TabIndex = 2
        Me.grpNodeJs.TabStop = False
        Me.grpNodeJs.Text = "Node.js"
        '
        'lblNodejsPath
        '
        Me.lblNodejsPath.AutoSize = True
        Me.lblNodejsPath.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblNodejsPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblNodejsPath.Location = New System.Drawing.Point(45, 48)
        Me.lblNodejsPath.Name = "lblNodejsPath"
        Me.lblNodejsPath.Size = New System.Drawing.Size(0, 13)
        Me.lblNodejsPath.TabIndex = 2
        '
        'lblNodejsVersion
        '
        Me.lblNodejsVersion.AutoSize = True
        Me.lblNodejsVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNodejsVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblNodejsVersion.Location = New System.Drawing.Point(45, 28)
        Me.lblNodejsVersion.Name = "lblNodejsVersion"
        Me.lblNodejsVersion.Size = New System.Drawing.Size(76, 15)
        Me.lblNodejsVersion.TabIndex = 1
        Me.lblNodejsVersion.Text = "Non rilevato"
        '
        'lblNodejsStatusIcon
        '
        Me.lblNodejsStatusIcon.AutoSize = True
        Me.lblNodejsStatusIcon.Font = New System.Drawing.Font("Segoe UI", 16.0!)
        Me.lblNodejsStatusIcon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblNodejsStatusIcon.Location = New System.Drawing.Point(15, 25)
        Me.lblNodejsStatusIcon.Name = "lblNodejsStatusIcon"
        Me.lblNodejsStatusIcon.Size = New System.Drawing.Size(32, 30)
        Me.lblNodejsStatusIcon.TabIndex = 0
        Me.lblNodejsStatusIcon.Text = "○"
        '
        'lblEnvSubtitle
        '
        Me.lblEnvSubtitle.AutoSize = True
        Me.lblEnvSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblEnvSubtitle.Location = New System.Drawing.Point(20, 45)
        Me.lblEnvSubtitle.Name = "lblEnvSubtitle"
        Me.lblEnvSubtitle.Size = New System.Drawing.Size(297, 15)
        Me.lblEnvSubtitle.TabIndex = 1
        Me.lblEnvSubtitle.Text = "Stato di Node.js, npm e Node-RED installati nel sistema"
        '
        'lblEnvTitle
        '
        Me.lblEnvTitle.AutoSize = True
        Me.lblEnvTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblEnvTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblEnvTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblEnvTitle.Name = "lblEnvTitle"
        Me.lblEnvTitle.Size = New System.Drawing.Size(147, 20)
        Me.lblEnvTitle.TabIndex = 0
        Me.lblEnvTitle.Text = "Verifica Dipendenze"
        '
        'tabLog
        '
        Me.tabLog.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.tabLog.Controls.Add(Me.rtbLog)
        Me.tabLog.Controls.Add(Me.tsLog)
        Me.tabLog.Location = New System.Drawing.Point(4, 24)
        Me.tabLog.Name = "tabLog"
        Me.tabLog.Size = New System.Drawing.Size(1016, 589)
        Me.tabLog.TabIndex = 2
        Me.tabLog.Text = "Log"
        '
        'rtbLog
        '
        Me.rtbLog.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbLog.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.rtbLog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.rtbLog.Location = New System.Drawing.Point(0, 25)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.ReadOnly = True
        Me.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbLog.Size = New System.Drawing.Size(1016, 564)
        Me.rtbLog.TabIndex = 1
        Me.rtbLog.Text = ""
        '
        'tsLog
        '
        Me.tsLog.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.tsLog.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsLog.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbPauseLog, Me.tsbClearLog, Me.sep1, Me.tsbExportTxt, Me.tsbExportCsv, Me.sep2, Me.lblFilter, Me.cmbLogFilter, Me.sep3, Me.tsbAutoScroll})
        Me.tsLog.Location = New System.Drawing.Point(0, 0)
        Me.tsLog.Name = "tsLog"
        Me.tsLog.Size = New System.Drawing.Size(1016, 25)
        Me.tsLog.TabIndex = 0
        '
        'tsbPauseLog
        '
        Me.tsbPauseLog.CheckOnClick = True
        Me.tsbPauseLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbPauseLog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.tsbPauseLog.Name = "tsbPauseLog"
        Me.tsbPauseLog.Size = New System.Drawing.Size(57, 22)
        Me.tsbPauseLog.Text = "⏸ Pausa"
        '
        'tsbClearLog
        '
        Me.tsbClearLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbClearLog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
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
        Me.tsbExportTxt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.tsbExportTxt.Name = "tsbExportTxt"
        Me.tsbExportTxt.Size = New System.Drawing.Size(72, 22)
        Me.tsbExportTxt.Text = "Esporta TXT"
        '
        'tsbExportCsv
        '
        Me.tsbExportCsv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbExportCsv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
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
        Me.lblFilter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
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
        Me.tsbAutoScroll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.tsbAutoScroll.Name = "tsbAutoScroll"
        Me.tsbAutoScroll.Size = New System.Drawing.Size(66, 22)
        Me.tsbAutoScroll.Text = "AutoScroll"
        '
        'tabBackup
        '
        Me.tabBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.tabBackup.Controls.Add(Me.lvBackups)
        Me.tabBackup.Controls.Add(Me.lblBackupListTitle)
        Me.tabBackup.Controls.Add(Me.pnlBackupActions)
        Me.tabBackup.Controls.Add(Me.grpBackupConfig)
        Me.tabBackup.Controls.Add(Me.lblBackupTitle)
        Me.tabBackup.Location = New System.Drawing.Point(4, 24)
        Me.tabBackup.Name = "tabBackup"
        Me.tabBackup.Size = New System.Drawing.Size(1016, 589)
        Me.tabBackup.TabIndex = 3
        Me.tabBackup.Text = "Backup"
        '
        'lvBackups
        '
        Me.lvBackups.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.lvBackups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvBackups.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colBackupName, Me.colBackupDate, Me.colBackupSize, Me.colBackupStatus})
        Me.lvBackups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lvBackups.FullRowSelect = True
        Me.lvBackups.HideSelection = False
        Me.lvBackups.Location = New System.Drawing.Point(20, 278)
        Me.lvBackups.Name = "lvBackups"
        Me.lvBackups.Size = New System.Drawing.Size(560, 200)
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
        'lblBackupListTitle
        '
        Me.lblBackupListTitle.AutoSize = True
        Me.lblBackupListTitle.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblBackupListTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblBackupListTitle.Location = New System.Drawing.Point(20, 258)
        Me.lblBackupListTitle.Name = "lblBackupListTitle"
        Me.lblBackupListTitle.Size = New System.Drawing.Size(117, 13)
        Me.lblBackupListTitle.TabIndex = 3
        Me.lblBackupListTitle.Text = "BACKUP DISPONIBILI"
        '
        'pnlBackupActions
        '
        Me.pnlBackupActions.BackColor = System.Drawing.Color.Transparent
        Me.pnlBackupActions.Controls.Add(Me.btnOpenBackupFolder)
        Me.pnlBackupActions.Controls.Add(Me.btnDeleteBackup)
        Me.pnlBackupActions.Controls.Add(Me.btnRestoreBackup)
        Me.pnlBackupActions.Controls.Add(Me.btnBackupNow)
        Me.pnlBackupActions.Location = New System.Drawing.Point(20, 200)
        Me.pnlBackupActions.Name = "pnlBackupActions"
        Me.pnlBackupActions.Size = New System.Drawing.Size(620, 45)
        Me.pnlBackupActions.TabIndex = 2
        '
        'btnOpenBackupFolder
        '
        Me.btnOpenBackupFolder.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnOpenBackupFolder.FlatAppearance.BorderSize = 0
        Me.btnOpenBackupFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenBackupFolder.ForeColor = System.Drawing.Color.White
        Me.btnOpenBackupFolder.Location = New System.Drawing.Point(445, 0)
        Me.btnOpenBackupFolder.Name = "btnOpenBackupFolder"
        Me.btnOpenBackupFolder.Size = New System.Drawing.Size(150, 38)
        Me.btnOpenBackupFolder.TabIndex = 3
        Me.btnOpenBackupFolder.Text = "Apri Cartella"
        Me.btnOpenBackupFolder.UseVisualStyleBackColor = False
        '
        'btnDeleteBackup
        '
        Me.btnDeleteBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnDeleteBackup.FlatAppearance.BorderSize = 0
        Me.btnDeleteBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteBackup.ForeColor = System.Drawing.Color.White
        Me.btnDeleteBackup.Location = New System.Drawing.Point(310, 0)
        Me.btnDeleteBackup.Name = "btnDeleteBackup"
        Me.btnDeleteBackup.Size = New System.Drawing.Size(120, 38)
        Me.btnDeleteBackup.TabIndex = 2
        Me.btnDeleteBackup.Text = "Elimina"
        Me.btnDeleteBackup.UseVisualStyleBackColor = False
        '
        'btnRestoreBackup
        '
        Me.btnRestoreBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnRestoreBackup.FlatAppearance.BorderSize = 0
        Me.btnRestoreBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestoreBackup.ForeColor = System.Drawing.Color.White
        Me.btnRestoreBackup.Location = New System.Drawing.Point(165, 0)
        Me.btnRestoreBackup.Name = "btnRestoreBackup"
        Me.btnRestoreBackup.Size = New System.Drawing.Size(130, 38)
        Me.btnRestoreBackup.TabIndex = 1
        Me.btnRestoreBackup.Text = "Ripristina"
        Me.btnRestoreBackup.UseVisualStyleBackColor = False
        '
        'btnBackupNow
        '
        Me.btnBackupNow.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnBackupNow.FlatAppearance.BorderSize = 0
        Me.btnBackupNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackupNow.ForeColor = System.Drawing.Color.White
        Me.btnBackupNow.Location = New System.Drawing.Point(0, 0)
        Me.btnBackupNow.Name = "btnBackupNow"
        Me.btnBackupNow.Size = New System.Drawing.Size(150, 38)
        Me.btnBackupNow.TabIndex = 0
        Me.btnBackupNow.Text = "Esegui Backup"
        Me.btnBackupNow.UseVisualStyleBackColor = False
        '
        'grpBackupConfig
        '
        Me.grpBackupConfig.BackColor = System.Drawing.Color.Transparent
        Me.grpBackupConfig.Controls.Add(Me.btnSaveBackupConfig)
        Me.grpBackupConfig.Controls.Add(Me.nudMaxBackups)
        Me.grpBackupConfig.Controls.Add(Me.lblMaxBackups)
        Me.grpBackupConfig.Controls.Add(Me.cmbBackupSchedule)
        Me.grpBackupConfig.Controls.Add(Me.lblSchedule)
        Me.grpBackupConfig.Controls.Add(Me.btnBrowseBackup)
        Me.grpBackupConfig.Controls.Add(Me.txtBackupFolder)
        Me.grpBackupConfig.Controls.Add(Me.lblBackupFolder)
        Me.grpBackupConfig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpBackupConfig.Location = New System.Drawing.Point(20, 55)
        Me.grpBackupConfig.Name = "grpBackupConfig"
        Me.grpBackupConfig.Size = New System.Drawing.Size(560, 130)
        Me.grpBackupConfig.TabIndex = 1
        Me.grpBackupConfig.TabStop = False
        Me.grpBackupConfig.Text = "Configurazione"
        '
        'btnSaveBackupConfig
        '
        Me.btnSaveBackupConfig.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.btnSaveBackupConfig.FlatAppearance.BorderSize = 0
        Me.btnSaveBackupConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveBackupConfig.ForeColor = System.Drawing.Color.White
        Me.btnSaveBackupConfig.Location = New System.Drawing.Point(460, 90)
        Me.btnSaveBackupConfig.Name = "btnSaveBackupConfig"
        Me.btnSaveBackupConfig.Size = New System.Drawing.Size(80, 28)
        Me.btnSaveBackupConfig.TabIndex = 7
        Me.btnSaveBackupConfig.Text = "Salva"
        Me.btnSaveBackupConfig.UseVisualStyleBackColor = False
        '
        'nudMaxBackups
        '
        Me.nudMaxBackups.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.nudMaxBackups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.nudMaxBackups.Location = New System.Drawing.Point(390, 55)
        Me.nudMaxBackups.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudMaxBackups.Name = "nudMaxBackups"
        Me.nudMaxBackups.Size = New System.Drawing.Size(70, 23)
        Me.nudMaxBackups.TabIndex = 6
        Me.nudMaxBackups.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'lblMaxBackups
        '
        Me.lblMaxBackups.AutoSize = True
        Me.lblMaxBackups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblMaxBackups.Location = New System.Drawing.Point(300, 58)
        Me.lblMaxBackups.Name = "lblMaxBackups"
        Me.lblMaxBackups.Size = New System.Drawing.Size(75, 15)
        Me.lblMaxBackups.TabIndex = 5
        Me.lblMaxBackups.Text = "Backup max:"
        '
        'cmbBackupSchedule
        '
        Me.cmbBackupSchedule.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.cmbBackupSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBackupSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbBackupSchedule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.cmbBackupSchedule.Items.AddRange(New Object() {"Disabilitato", "Ogni ora", "Giornaliero", "Settimanale"})
        Me.cmbBackupSchedule.Location = New System.Drawing.Point(130, 55)
        Me.cmbBackupSchedule.Name = "cmbBackupSchedule"
        Me.cmbBackupSchedule.Size = New System.Drawing.Size(150, 23)
        Me.cmbBackupSchedule.TabIndex = 4
        '
        'lblSchedule
        '
        Me.lblSchedule.AutoSize = True
        Me.lblSchedule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblSchedule.Location = New System.Drawing.Point(15, 58)
        Me.lblSchedule.Name = "lblSchedule"
        Me.lblSchedule.Size = New System.Drawing.Size(83, 15)
        Me.lblSchedule.TabIndex = 3
        Me.lblSchedule.Text = "Pianificazione:"
        '
        'btnBrowseBackup
        '
        Me.btnBrowseBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnBrowseBackup.FlatAppearance.BorderSize = 0
        Me.btnBrowseBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseBackup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnBrowseBackup.Location = New System.Drawing.Point(490, 22)
        Me.btnBrowseBackup.Name = "btnBrowseBackup"
        Me.btnBrowseBackup.Size = New System.Drawing.Size(50, 24)
        Me.btnBrowseBackup.TabIndex = 2
        Me.btnBrowseBackup.Text = "..."
        Me.btnBrowseBackup.UseVisualStyleBackColor = False
        '
        'txtBackupFolder
        '
        Me.txtBackupFolder.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.txtBackupFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBackupFolder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.txtBackupFolder.Location = New System.Drawing.Point(130, 22)
        Me.txtBackupFolder.Name = "txtBackupFolder"
        Me.txtBackupFolder.Size = New System.Drawing.Size(350, 23)
        Me.txtBackupFolder.TabIndex = 1
        '
        'lblBackupFolder
        '
        Me.lblBackupFolder.AutoSize = True
        Me.lblBackupFolder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblBackupFolder.Location = New System.Drawing.Point(15, 25)
        Me.lblBackupFolder.Name = "lblBackupFolder"
        Me.lblBackupFolder.Size = New System.Drawing.Size(92, 15)
        Me.lblBackupFolder.TabIndex = 0
        Me.lblBackupFolder.Text = "Cartella Backup:"
        '
        'lblBackupTitle
        '
        Me.lblBackupTitle.AutoSize = True
        Me.lblBackupTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblBackupTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblBackupTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblBackupTitle.Name = "lblBackupTitle"
        Me.lblBackupTitle.Size = New System.Drawing.Size(126, 20)
        Me.lblBackupTitle.TabIndex = 0
        Me.lblBackupTitle.Text = "Gestione Backup"
        '
        'tabSecurity
        '
        Me.tabSecurity.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.tabSecurity.Controls.Add(Me.pnlSecurityButtons)
        Me.tabSecurity.Controls.Add(Me.grpLdap)
        Me.tabSecurity.Controls.Add(Me.grpIpRestriction)
        Me.tabSecurity.Controls.Add(Me.grpUsers)
        Me.tabSecurity.Controls.Add(Me.grpAuthToggle)
        Me.tabSecurity.Controls.Add(Me.lblSecSubtitle)
        Me.tabSecurity.Controls.Add(Me.lblSecTitle)
        Me.tabSecurity.Location = New System.Drawing.Point(4, 24)
        Me.tabSecurity.Name = "tabSecurity"
        Me.tabSecurity.Size = New System.Drawing.Size(1016, 589)
        Me.tabSecurity.TabIndex = 4
        Me.tabSecurity.Text = "Sicurezza"
        '
        'pnlSecurityButtons
        '
        Me.pnlSecurityButtons.BackColor = System.Drawing.Color.Transparent
        Me.pnlSecurityButtons.Controls.Add(Me.btnOpenSettingsFile)
        Me.pnlSecurityButtons.Controls.Add(Me.btnSaveSecurity)
        Me.pnlSecurityButtons.Location = New System.Drawing.Point(20, 630)
        Me.pnlSecurityButtons.Name = "pnlSecurityButtons"
        Me.pnlSecurityButtons.Size = New System.Drawing.Size(560, 45)
        Me.pnlSecurityButtons.TabIndex = 6
        '
        'btnOpenSettingsFile
        '
        Me.btnOpenSettingsFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnOpenSettingsFile.FlatAppearance.BorderSize = 0
        Me.btnOpenSettingsFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenSettingsFile.ForeColor = System.Drawing.Color.White
        Me.btnOpenSettingsFile.Location = New System.Drawing.Point(195, 0)
        Me.btnOpenSettingsFile.Name = "btnOpenSettingsFile"
        Me.btnOpenSettingsFile.Size = New System.Drawing.Size(180, 38)
        Me.btnOpenSettingsFile.TabIndex = 1
        Me.btnOpenSettingsFile.Text = "Apri settings.js"
        Me.btnOpenSettingsFile.UseVisualStyleBackColor = False
        '
        'btnSaveSecurity
        '
        Me.btnSaveSecurity.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.btnSaveSecurity.FlatAppearance.BorderSize = 0
        Me.btnSaveSecurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveSecurity.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaveSecurity.ForeColor = System.Drawing.Color.White
        Me.btnSaveSecurity.Location = New System.Drawing.Point(0, 0)
        Me.btnSaveSecurity.Name = "btnSaveSecurity"
        Me.btnSaveSecurity.Size = New System.Drawing.Size(180, 38)
        Me.btnSaveSecurity.TabIndex = 0
        Me.btnSaveSecurity.Text = "Salva & Applica"
        Me.btnSaveSecurity.UseVisualStyleBackColor = False
        '
        'grpLdap
        '
        Me.grpLdap.BackColor = System.Drawing.Color.Transparent
        Me.grpLdap.Controls.Add(Me.txtLdapBaseDn)
        Me.grpLdap.Controls.Add(Me.lblLdapBaseDn)
        Me.grpLdap.Controls.Add(Me.txtLdapUrl)
        Me.grpLdap.Controls.Add(Me.lblLdapUrl)
        Me.grpLdap.Controls.Add(Me.chkEnableLdap)
        Me.grpLdap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpLdap.Location = New System.Drawing.Point(20, 502)
        Me.grpLdap.Name = "grpLdap"
        Me.grpLdap.Size = New System.Drawing.Size(560, 110)
        Me.grpLdap.TabIndex = 5
        Me.grpLdap.TabStop = False
        Me.grpLdap.Text = "LDAP / Active Directory (Opzionale)"
        '
        'txtLdapBaseDn
        '
        Me.txtLdapBaseDn.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.txtLdapBaseDn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLdapBaseDn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.txtLdapBaseDn.Location = New System.Drawing.Point(110, 79)
        Me.txtLdapBaseDn.Name = "txtLdapBaseDn"
        Me.txtLdapBaseDn.Size = New System.Drawing.Size(320, 23)
        Me.txtLdapBaseDn.TabIndex = 4
        '
        'lblLdapBaseDn
        '
        Me.lblLdapBaseDn.AutoSize = True
        Me.lblLdapBaseDn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblLdapBaseDn.Location = New System.Drawing.Point(15, 82)
        Me.lblLdapBaseDn.Name = "lblLdapBaseDn"
        Me.lblLdapBaseDn.Size = New System.Drawing.Size(54, 15)
        Me.lblLdapBaseDn.TabIndex = 3
        Me.lblLdapBaseDn.Text = "Base DN:"
        '
        'txtLdapUrl
        '
        Me.txtLdapUrl.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.txtLdapUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLdapUrl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.txtLdapUrl.Location = New System.Drawing.Point(110, 52)
        Me.txtLdapUrl.Name = "txtLdapUrl"
        Me.txtLdapUrl.Size = New System.Drawing.Size(320, 23)
        Me.txtLdapUrl.TabIndex = 2
        '
        'lblLdapUrl
        '
        Me.lblLdapUrl.AutoSize = True
        Me.lblLdapUrl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblLdapUrl.Location = New System.Drawing.Point(15, 55)
        Me.lblLdapUrl.Name = "lblLdapUrl"
        Me.lblLdapUrl.Size = New System.Drawing.Size(66, 15)
        Me.lblLdapUrl.TabIndex = 1
        Me.lblLdapUrl.Text = "URL Server:"
        '
        'chkEnableLdap
        '
        Me.chkEnableLdap.AutoSize = True
        Me.chkEnableLdap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.chkEnableLdap.Location = New System.Drawing.Point(15, 25)
        Me.chkEnableLdap.Name = "chkEnableLdap"
        Me.chkEnableLdap.Size = New System.Drawing.Size(172, 19)
        Me.chkEnableLdap.TabIndex = 0
        Me.chkEnableLdap.Text = "Abilita autenticazione LDAP"
        '
        'grpIpRestriction
        '
        Me.grpIpRestriction.BackColor = System.Drawing.Color.Transparent
        Me.grpIpRestriction.Controls.Add(Me.btnRemoveIp)
        Me.grpIpRestriction.Controls.Add(Me.btnAddIp)
        Me.grpIpRestriction.Controls.Add(Me.txtNewIp)
        Me.grpIpRestriction.Controls.Add(Me.lstAllowedIps)
        Me.grpIpRestriction.Controls.Add(Me.chkEnableIpRestriction)
        Me.grpIpRestriction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpIpRestriction.Location = New System.Drawing.Point(20, 360)
        Me.grpIpRestriction.Name = "grpIpRestriction"
        Me.grpIpRestriction.Size = New System.Drawing.Size(560, 130)
        Me.grpIpRestriction.TabIndex = 4
        Me.grpIpRestriction.TabStop = False
        Me.grpIpRestriction.Text = "Restrizione IP (Whitelist)"
        '
        'btnRemoveIp
        '
        Me.btnRemoveIp.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnRemoveIp.FlatAppearance.BorderSize = 0
        Me.btnRemoveIp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveIp.ForeColor = System.Drawing.Color.White
        Me.btnRemoveIp.Location = New System.Drawing.Point(465, 80)
        Me.btnRemoveIp.Name = "btnRemoveIp"
        Me.btnRemoveIp.Size = New System.Drawing.Size(78, 24)
        Me.btnRemoveIp.TabIndex = 4
        Me.btnRemoveIp.Text = "- Rimuovi"
        Me.btnRemoveIp.UseVisualStyleBackColor = False
        '
        'btnAddIp
        '
        Me.btnAddIp.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnAddIp.FlatAppearance.BorderSize = 0
        Me.btnAddIp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddIp.ForeColor = System.Drawing.Color.White
        Me.btnAddIp.Location = New System.Drawing.Point(380, 80)
        Me.btnAddIp.Name = "btnAddIp"
        Me.btnAddIp.Size = New System.Drawing.Size(78, 24)
        Me.btnAddIp.TabIndex = 3
        Me.btnAddIp.Text = "+ Aggiungi"
        Me.btnAddIp.UseVisualStyleBackColor = False
        '
        'txtNewIp
        '
        Me.txtNewIp.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.txtNewIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewIp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.txtNewIp.Location = New System.Drawing.Point(380, 50)
        Me.txtNewIp.Name = "txtNewIp"
        Me.txtNewIp.Size = New System.Drawing.Size(165, 23)
        Me.txtNewIp.TabIndex = 2
        '
        'lstAllowedIps
        '
        Me.lstAllowedIps.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.lstAllowedIps.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstAllowedIps.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lstAllowedIps.ItemHeight = 15
        Me.lstAllowedIps.Location = New System.Drawing.Point(15, 50)
        Me.lstAllowedIps.Name = "lstAllowedIps"
        Me.lstAllowedIps.Size = New System.Drawing.Size(350, 60)
        Me.lstAllowedIps.TabIndex = 1
        '
        'chkEnableIpRestriction
        '
        Me.chkEnableIpRestriction.AutoSize = True
        Me.chkEnableIpRestriction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.chkEnableIpRestriction.Location = New System.Drawing.Point(15, 25)
        Me.chkEnableIpRestriction.Name = "chkEnableIpRestriction"
        Me.chkEnableIpRestriction.Size = New System.Drawing.Size(241, 19)
        Me.chkEnableIpRestriction.TabIndex = 0
        Me.chkEnableIpRestriction.Text = "Abilita restrizione accesso per indirizzo IP"
        '
        'grpUsers
        '
        Me.grpUsers.BackColor = System.Drawing.Color.Transparent
        Me.grpUsers.Controls.Add(Me.btnDeleteUser)
        Me.grpUsers.Controls.Add(Me.btnEditUser)
        Me.grpUsers.Controls.Add(Me.btnAddUser)
        Me.grpUsers.Controls.Add(Me.dgvUsers)
        Me.grpUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpUsers.Location = New System.Drawing.Point(20, 168)
        Me.grpUsers.Name = "grpUsers"
        Me.grpUsers.Size = New System.Drawing.Size(560, 180)
        Me.grpUsers.TabIndex = 3
        Me.grpUsers.TabStop = False
        Me.grpUsers.Text = "Utenti Autorizzati"
        '
        'btnDeleteUser
        '
        Me.btnDeleteUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnDeleteUser.FlatAppearance.BorderSize = 0
        Me.btnDeleteUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteUser.ForeColor = System.Drawing.Color.White
        Me.btnDeleteUser.Location = New System.Drawing.Point(455, 112)
        Me.btnDeleteUser.Name = "btnDeleteUser"
        Me.btnDeleteUser.Size = New System.Drawing.Size(35, 35)
        Me.btnDeleteUser.TabIndex = 3
        Me.btnDeleteUser.Text = "✕"
        Me.btnDeleteUser.UseVisualStyleBackColor = False
        '
        'btnEditUser
        '
        Me.btnEditUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnEditUser.FlatAppearance.BorderSize = 0
        Me.btnEditUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditUser.ForeColor = System.Drawing.Color.White
        Me.btnEditUser.Location = New System.Drawing.Point(455, 72)
        Me.btnEditUser.Name = "btnEditUser"
        Me.btnEditUser.Size = New System.Drawing.Size(35, 35)
        Me.btnEditUser.TabIndex = 2
        Me.btnEditUser.Text = "✎"
        Me.btnEditUser.UseVisualStyleBackColor = False
        '
        'btnAddUser
        '
        Me.btnAddUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnAddUser.FlatAppearance.BorderSize = 0
        Me.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddUser.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddUser.ForeColor = System.Drawing.Color.White
        Me.btnAddUser.Location = New System.Drawing.Point(455, 30)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(35, 35)
        Me.btnAddUser.TabIndex = 1
        Me.btnAddUser.Text = "+"
        Me.btnAddUser.UseVisualStyleBackColor = False
        '
        'dgvUsers
        '
        Me.dgvUsers.AllowUserToAddRows = False
        Me.dgvUsers.AllowUserToDeleteRows = False
        Me.dgvUsers.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsers.GridColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(91, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.dgvUsers.Location = New System.Drawing.Point(15, 30)
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.RowHeadersVisible = False
        Me.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUsers.Size = New System.Drawing.Size(430, 130)
        Me.dgvUsers.TabIndex = 0
        '
        'grpAuthToggle
        '
        Me.grpAuthToggle.BackColor = System.Drawing.Color.Transparent
        Me.grpAuthToggle.Controls.Add(Me.lblAuthInfo)
        Me.grpAuthToggle.Controls.Add(Me.chkEnableAuth)
        Me.grpAuthToggle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpAuthToggle.Location = New System.Drawing.Point(20, 75)
        Me.grpAuthToggle.Name = "grpAuthToggle"
        Me.grpAuthToggle.Size = New System.Drawing.Size(560, 80)
        Me.grpAuthToggle.TabIndex = 2
        Me.grpAuthToggle.TabStop = False
        Me.grpAuthToggle.Text = "Autenticazione Editor"
        '
        'lblAuthInfo
        '
        Me.lblAuthInfo.AutoSize = True
        Me.lblAuthInfo.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblAuthInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblAuthInfo.Location = New System.Drawing.Point(15, 55)
        Me.lblAuthInfo.Name = "lblAuthInfo"
        Me.lblAuthInfo.Size = New System.Drawing.Size(264, 13)
        Me.lblAuthInfo.TabIndex = 1
        Me.lblAuthInfo.Text = "Richiede riavvio di Node-RED per essere applicata."
        '
        'chkEnableAuth
        '
        Me.chkEnableAuth.AutoSize = True
        Me.chkEnableAuth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.chkEnableAuth.Location = New System.Drawing.Point(15, 30)
        Me.chkEnableAuth.Name = "chkEnableAuth"
        Me.chkEnableAuth.Size = New System.Drawing.Size(348, 19)
        Me.chkEnableAuth.TabIndex = 0
        Me.chkEnableAuth.Text = "Abilita login con username e password per l'editor Node-RED"
        '
        'lblSecSubtitle
        '
        Me.lblSecSubtitle.AutoSize = True
        Me.lblSecSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblSecSubtitle.Location = New System.Drawing.Point(20, 45)
        Me.lblSecSubtitle.Name = "lblSecSubtitle"
        Me.lblSecSubtitle.Size = New System.Drawing.Size(324, 15)
        Me.lblSecSubtitle.TabIndex = 1
        Me.lblSecSubtitle.Text = "Gestione autenticazione editor Node-RED e restrizioni di rete"
        '
        'lblSecTitle
        '
        Me.lblSecTitle.AutoSize = True
        Me.lblSecTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblSecTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblSecTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblSecTitle.Name = "lblSecTitle"
        Me.lblSecTitle.Size = New System.Drawing.Size(201, 20)
        Me.lblSecTitle.TabIndex = 0
        Me.lblSecTitle.Text = "Sicurezza & Controllo Accessi"
        '
        'tabSettings
        '
        Me.tabSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.tabSettings.Controls.Add(Me.pnlSettingsButtons)
        Me.tabSettings.Controls.Add(Me.grpMinimize)
        Me.tabSettings.Controls.Add(Me.grpWatchdog)
        Me.tabSettings.Controls.Add(Me.grpStartup)
        Me.tabSettings.Controls.Add(Me.grpNodeRedConfig)
        Me.tabSettings.Controls.Add(Me.lblSettingsTitle)
        Me.tabSettings.Location = New System.Drawing.Point(4, 24)
        Me.tabSettings.Name = "tabSettings"
        Me.tabSettings.Size = New System.Drawing.Size(1016, 589)
        Me.tabSettings.TabIndex = 5
        Me.tabSettings.Text = "Impostazioni"
        '
        'pnlSettingsButtons
        '
        Me.pnlSettingsButtons.BackColor = System.Drawing.Color.Transparent
        Me.pnlSettingsButtons.Controls.Add(Me.btnResetSettings)
        Me.pnlSettingsButtons.Controls.Add(Me.btnSaveSettings)
        Me.pnlSettingsButtons.Location = New System.Drawing.Point(20, 545)
        Me.pnlSettingsButtons.Name = "pnlSettingsButtons"
        Me.pnlSettingsButtons.Size = New System.Drawing.Size(560, 40)
        Me.pnlSettingsButtons.TabIndex = 5
        '
        'btnResetSettings
        '
        Me.btnResetSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnResetSettings.FlatAppearance.BorderSize = 0
        Me.btnResetSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetSettings.ForeColor = System.Drawing.Color.White
        Me.btnResetSettings.Location = New System.Drawing.Point(195, 0)
        Me.btnResetSettings.Name = "btnResetSettings"
        Me.btnResetSettings.Size = New System.Drawing.Size(160, 36)
        Me.btnResetSettings.TabIndex = 1
        Me.btnResetSettings.Text = "Ripristina Default"
        Me.btnResetSettings.UseVisualStyleBackColor = False
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.btnSaveSettings.FlatAppearance.BorderSize = 0
        Me.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveSettings.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaveSettings.ForeColor = System.Drawing.Color.White
        Me.btnSaveSettings.Location = New System.Drawing.Point(0, 0)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(180, 36)
        Me.btnSaveSettings.TabIndex = 0
        Me.btnSaveSettings.Text = "Salva Impostazioni"
        Me.btnSaveSettings.UseVisualStyleBackColor = False
        '
        'grpMinimize
        '
        Me.grpMinimize.BackColor = System.Drawing.Color.Transparent
        Me.grpMinimize.Controls.Add(Me.chkStartMinimized)
        Me.grpMinimize.Controls.Add(Me.chkMinimizeToTray)
        Me.grpMinimize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpMinimize.Location = New System.Drawing.Point(20, 460)
        Me.grpMinimize.Name = "grpMinimize"
        Me.grpMinimize.Size = New System.Drawing.Size(560, 75)
        Me.grpMinimize.TabIndex = 4
        Me.grpMinimize.TabStop = False
        Me.grpMinimize.Text = "Comportamento"
        '
        'chkStartMinimized
        '
        Me.chkStartMinimized.AutoSize = True
        Me.chkStartMinimized.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.chkStartMinimized.Location = New System.Drawing.Point(15, 47)
        Me.chkStartMinimized.Name = "chkStartMinimized"
        Me.chkStartMinimized.Size = New System.Drawing.Size(174, 19)
        Me.chkStartMinimized.TabIndex = 1
        Me.chkStartMinimized.Text = "Avvia minimizzato nella tray"
        '
        'chkMinimizeToTray
        '
        Me.chkMinimizeToTray.AutoSize = True
        Me.chkMinimizeToTray.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.chkMinimizeToTray.Location = New System.Drawing.Point(15, 23)
        Me.chkMinimizeToTray.Name = "chkMinimizeToTray"
        Me.chkMinimizeToTray.Size = New System.Drawing.Size(270, 19)
        Me.chkMinimizeToTray.TabIndex = 0
        Me.chkMinimizeToTray.Text = "Minimizza nella system tray invece di chiudere"
        '
        'grpWatchdog
        '
        Me.grpWatchdog.BackColor = System.Drawing.Color.Transparent
        Me.grpWatchdog.Controls.Add(Me.lblMaxRestartsHint)
        Me.grpWatchdog.Controls.Add(Me.nudMaxRestarts)
        Me.grpWatchdog.Controls.Add(Me.lblMaxRestarts)
        Me.grpWatchdog.Controls.Add(Me.nudWatchdogInterval)
        Me.grpWatchdog.Controls.Add(Me.lblWatchInterval)
        Me.grpWatchdog.Controls.Add(Me.chkWatchdog)
        Me.grpWatchdog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpWatchdog.Location = New System.Drawing.Point(20, 355)
        Me.grpWatchdog.Name = "grpWatchdog"
        Me.grpWatchdog.Size = New System.Drawing.Size(560, 95)
        Me.grpWatchdog.TabIndex = 3
        Me.grpWatchdog.TabStop = False
        Me.grpWatchdog.Text = "Watchdog (Riavvio Automatico)"
        '
        'lblMaxRestartsHint
        '
        Me.lblMaxRestartsHint.AutoSize = True
        Me.lblMaxRestartsHint.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblMaxRestartsHint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblMaxRestartsHint.Location = New System.Drawing.Point(410, 55)
        Me.lblMaxRestartsHint.Name = "lblMaxRestartsHint"
        Me.lblMaxRestartsHint.Size = New System.Drawing.Size(78, 13)
        Me.lblMaxRestartsHint.TabIndex = 5
        Me.lblMaxRestartsHint.Text = "(0 = illimitato)"
        '
        'nudMaxRestarts
        '
        Me.nudMaxRestarts.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.nudMaxRestarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudMaxRestarts.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.nudMaxRestarts.Location = New System.Drawing.Point(340, 52)
        Me.nudMaxRestarts.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nudMaxRestarts.Name = "nudMaxRestarts"
        Me.nudMaxRestarts.Size = New System.Drawing.Size(60, 23)
        Me.nudMaxRestarts.TabIndex = 4
        Me.nudMaxRestarts.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblMaxRestarts
        '
        Me.lblMaxRestarts.AutoSize = True
        Me.lblMaxRestarts.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblMaxRestarts.Location = New System.Drawing.Point(260, 55)
        Me.lblMaxRestarts.Name = "lblMaxRestarts"
        Me.lblMaxRestarts.Size = New System.Drawing.Size(67, 15)
        Me.lblMaxRestarts.TabIndex = 3
        Me.lblMaxRestarts.Text = "Max riavvii:"
        '
        'nudWatchdogInterval
        '
        Me.nudWatchdogInterval.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.nudWatchdogInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudWatchdogInterval.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.nudWatchdogInterval.Location = New System.Drawing.Point(165, 52)
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
        Me.lblWatchInterval.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblWatchInterval.Location = New System.Drawing.Point(30, 55)
        Me.lblWatchInterval.Name = "lblWatchInterval"
        Me.lblWatchInterval.Size = New System.Drawing.Size(115, 15)
        Me.lblWatchInterval.TabIndex = 1
        Me.lblWatchInterval.Text = "Controllo ogni (sec):"
        '
        'chkWatchdog
        '
        Me.chkWatchdog.AutoSize = True
        Me.chkWatchdog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.chkWatchdog.Location = New System.Drawing.Point(15, 25)
        Me.chkWatchdog.Name = "chkWatchdog"
        Me.chkWatchdog.Size = New System.Drawing.Size(373, 19)
        Me.chkWatchdog.TabIndex = 0
        Me.chkWatchdog.Text = "Abilita watchdog (riavvia Node-RED se si ferma inaspettatamente)"
        '
        'grpStartup
        '
        Me.grpStartup.BackColor = System.Drawing.Color.Transparent
        Me.grpStartup.Controls.Add(Me.nudStartDelay)
        Me.grpStartup.Controls.Add(Me.lblStartDelay)
        Me.grpStartup.Controls.Add(Me.chkAutoStartNodeRed)
        Me.grpStartup.Controls.Add(Me.lblMethodHint)
        Me.grpStartup.Controls.Add(Me.cmbStartupMethod)
        Me.grpStartup.Controls.Add(Me.lblStartupMethod)
        Me.grpStartup.Controls.Add(Me.chkAutoStartWindows)
        Me.grpStartup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpStartup.Location = New System.Drawing.Point(20, 215)
        Me.grpStartup.Name = "grpStartup"
        Me.grpStartup.Size = New System.Drawing.Size(560, 130)
        Me.grpStartup.TabIndex = 2
        Me.grpStartup.TabStop = False
        Me.grpStartup.Text = "Avvio Automatico"
        '
        'nudStartDelay
        '
        Me.nudStartDelay.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.nudStartDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudStartDelay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.nudStartDelay.Location = New System.Drawing.Point(165, 100)
        Me.nudStartDelay.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.nudStartDelay.Name = "nudStartDelay"
        Me.nudStartDelay.Size = New System.Drawing.Size(60, 23)
        Me.nudStartDelay.TabIndex = 6
        Me.nudStartDelay.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'lblStartDelay
        '
        Me.lblStartDelay.AutoSize = True
        Me.lblStartDelay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblStartDelay.Location = New System.Drawing.Point(30, 102)
        Me.lblStartDelay.Name = "lblStartDelay"
        Me.lblStartDelay.Size = New System.Drawing.Size(107, 15)
        Me.lblStartDelay.TabIndex = 5
        Me.lblStartDelay.Text = "Ritardo avvio (sec):"
        '
        'chkAutoStartNodeRed
        '
        Me.chkAutoStartNodeRed.AutoSize = True
        Me.chkAutoStartNodeRed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.chkAutoStartNodeRed.Location = New System.Drawing.Point(15, 78)
        Me.chkAutoStartNodeRed.Name = "chkAutoStartNodeRed"
        Me.chkAutoStartNodeRed.Size = New System.Drawing.Size(317, 19)
        Me.chkAutoStartNodeRed.TabIndex = 4
        Me.chkAutoStartNodeRed.Text = "Avvia Node-RED automaticamente all'apertura dell'app"
        '
        'lblMethodHint
        '
        Me.lblMethodHint.AutoSize = True
        Me.lblMethodHint.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblMethodHint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblMethodHint.Location = New System.Drawing.Point(280, 52)
        Me.lblMethodHint.Name = "lblMethodHint"
        Me.lblMethodHint.Size = New System.Drawing.Size(202, 13)
        Me.lblMethodHint.TabIndex = 3
        Me.lblMethodHint.Text = "(Task Scheduler consigliato per server)"
        '
        'cmbStartupMethod
        '
        Me.cmbStartupMethod.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.cmbStartupMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStartupMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbStartupMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.cmbStartupMethod.Items.AddRange(New Object() {"Registro di Sistema", "Task Scheduler"})
        Me.cmbStartupMethod.Location = New System.Drawing.Point(90, 49)
        Me.cmbStartupMethod.Name = "cmbStartupMethod"
        Me.cmbStartupMethod.Size = New System.Drawing.Size(180, 23)
        Me.cmbStartupMethod.TabIndex = 2
        '
        'lblStartupMethod
        '
        Me.lblStartupMethod.AutoSize = True
        Me.lblStartupMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblStartupMethod.Location = New System.Drawing.Point(30, 52)
        Me.lblStartupMethod.Name = "lblStartupMethod"
        Me.lblStartupMethod.Size = New System.Drawing.Size(52, 15)
        Me.lblStartupMethod.TabIndex = 1
        Me.lblStartupMethod.Text = "Metodo:"
        '
        'chkAutoStartWindows
        '
        Me.chkAutoStartWindows.AutoSize = True
        Me.chkAutoStartWindows.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.chkAutoStartWindows.Location = New System.Drawing.Point(15, 25)
        Me.chkAutoStartWindows.Name = "chkAutoStartWindows"
        Me.chkAutoStartWindows.Size = New System.Drawing.Size(234, 19)
        Me.chkAutoStartWindows.TabIndex = 0
        Me.chkAutoStartWindows.Text = "Avvia Node-RED Desktop con Windows"
        '
        'grpNodeRedConfig
        '
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
        Me.grpNodeRedConfig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grpNodeRedConfig.Location = New System.Drawing.Point(20, 55)
        Me.grpNodeRedConfig.Name = "grpNodeRedConfig"
        Me.grpNodeRedConfig.Size = New System.Drawing.Size(560, 150)
        Me.grpNodeRedConfig.TabIndex = 1
        Me.grpNodeRedConfig.TabStop = False
        Me.grpNodeRedConfig.Text = "Configurazione Node-RED"
        '
        'btnDetectPaths
        '
        Me.btnDetectPaths.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnDetectPaths.FlatAppearance.BorderSize = 0
        Me.btnDetectPaths.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDetectPaths.ForeColor = System.Drawing.Color.White
        Me.btnDetectPaths.Location = New System.Drawing.Point(480, 114)
        Me.btnDetectPaths.Name = "btnDetectPaths"
        Me.btnDetectPaths.Size = New System.Drawing.Size(70, 24)
        Me.btnDetectPaths.TabIndex = 11
        Me.btnDetectPaths.Text = "Auto-rileva"
        Me.btnDetectPaths.UseVisualStyleBackColor = False
        '
        'txtNodeRedCmd
        '
        Me.txtNodeRedCmd.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.txtNodeRedCmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNodeRedCmd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.txtNodeRedCmd.Location = New System.Drawing.Point(120, 114)
        Me.txtNodeRedCmd.Name = "txtNodeRedCmd"
        Me.txtNodeRedCmd.Size = New System.Drawing.Size(350, 23)
        Me.txtNodeRedCmd.TabIndex = 10
        '
        'lblNodeRedCmd
        '
        Me.lblNodeRedCmd.AutoSize = True
        Me.lblNodeRedCmd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblNodeRedCmd.Location = New System.Drawing.Point(15, 117)
        Me.lblNodeRedCmd.Name = "lblNodeRedCmd"
        Me.lblNodeRedCmd.Size = New System.Drawing.Size(95, 15)
        Me.lblNodeRedCmd.TabIndex = 9
        Me.lblNodeRedCmd.Text = "Node-RED CMD:"
        '
        'lblFlowFileHint
        '
        Me.lblFlowFileHint.AutoSize = True
        Me.lblFlowFileHint.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblFlowFileHint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblFlowFileHint.Location = New System.Drawing.Point(330, 87)
        Me.lblFlowFileHint.Name = "lblFlowFileHint"
        Me.lblFlowFileHint.Size = New System.Drawing.Size(133, 13)
        Me.lblFlowFileHint.TabIndex = 8
        Me.lblFlowFileHint.Text = "(relativo a Directory Dati)"
        '
        'txtFlowFile
        '
        Me.txtFlowFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.txtFlowFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFlowFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.txtFlowFile.Location = New System.Drawing.Point(120, 84)
        Me.txtFlowFile.Name = "txtFlowFile"
        Me.txtFlowFile.Size = New System.Drawing.Size(200, 23)
        Me.txtFlowFile.TabIndex = 7
        Me.txtFlowFile.Text = "flows.json"
        '
        'lblFlowFile
        '
        Me.lblFlowFile.AutoSize = True
        Me.lblFlowFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblFlowFile.Location = New System.Drawing.Point(15, 87)
        Me.lblFlowFile.Name = "lblFlowFile"
        Me.lblFlowFile.Size = New System.Drawing.Size(61, 15)
        Me.lblFlowFile.TabIndex = 6
        Me.lblFlowFile.Text = "File Flows:"
        '
        'btnBrowseUserDir
        '
        Me.btnBrowseUserDir.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnBrowseUserDir.FlatAppearance.BorderSize = 0
        Me.btnBrowseUserDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseUserDir.ForeColor = System.Drawing.Color.White
        Me.btnBrowseUserDir.Location = New System.Drawing.Point(480, 54)
        Me.btnBrowseUserDir.Name = "btnBrowseUserDir"
        Me.btnBrowseUserDir.Size = New System.Drawing.Size(50, 24)
        Me.btnBrowseUserDir.TabIndex = 5
        Me.btnBrowseUserDir.Text = "..."
        Me.btnBrowseUserDir.UseVisualStyleBackColor = False
        '
        'txtUserDir
        '
        Me.txtUserDir.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.txtUserDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserDir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.txtUserDir.Location = New System.Drawing.Point(120, 54)
        Me.txtUserDir.Name = "txtUserDir"
        Me.txtUserDir.Size = New System.Drawing.Size(350, 23)
        Me.txtUserDir.TabIndex = 4
        '
        'lblUserDir
        '
        Me.lblUserDir.AutoSize = True
        Me.lblUserDir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblUserDir.Location = New System.Drawing.Point(15, 57)
        Me.lblUserDir.Name = "lblUserDir"
        Me.lblUserDir.Size = New System.Drawing.Size(82, 15)
        Me.lblUserDir.TabIndex = 3
        Me.lblUserDir.Text = "Directory Dati:"
        '
        'lblPortHint
        '
        Me.lblPortHint.AutoSize = True
        Me.lblPortHint.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblPortHint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblPortHint.Location = New System.Drawing.Point(220, 27)
        Me.lblPortHint.Name = "lblPortHint"
        Me.lblPortHint.Size = New System.Drawing.Size(80, 13)
        Me.lblPortHint.TabIndex = 2
        Me.lblPortHint.Text = "(default: 1880)"
        '
        'nudPort
        '
        Me.nudPort.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.nudPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudPort.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.nudPort.Location = New System.Drawing.Point(120, 24)
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
        Me.lblPort.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblPort.Location = New System.Drawing.Point(15, 27)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(69, 15)
        Me.lblPort.TabIndex = 0
        Me.lblPort.Text = "Porta HTTP:"
        '
        'lblSettingsTitle
        '
        Me.lblSettingsTitle.AutoSize = True
        Me.lblSettingsTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblSettingsTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblSettingsTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblSettingsTitle.Name = "lblSettingsTitle"
        Me.lblSettingsTitle.Size = New System.Drawing.Size(162, 20)
        Me.lblSettingsTitle.TabIndex = 0
        Me.lblSettingsTitle.Text = "Impostazioni Generali"
        '
        'ssMain
        '
        Me.ssMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssAppStatus, Me.tssSpring, Me.tssNodeRedStatus, Me.tssSep, Me.tssUptime})
        Me.ssMain.Location = New System.Drawing.Point(0, 677)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(1024, 23)
        Me.ssMain.TabIndex = 2
        Me.ssMain.Text = "ssMain"
        '
        'tssAppStatus
        '
        Me.tssAppStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.tssAppStatus.Name = "tssAppStatus"
        Me.tssAppStatus.Size = New System.Drawing.Size(43, 18)
        Me.tssAppStatus.Text = "Pronto"
        '
        'tssSpring
        '
        Me.tssSpring.Name = "tssSpring"
        Me.tssSpring.Size = New System.Drawing.Size(794, 18)
        Me.tssSpring.Spring = True
        '
        'tssNodeRedStatus
        '
        Me.tssNodeRedStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
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
        Me.tssUptime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(200, Byte), Integer))
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
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1024, 700)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.MinimumSize = New System.Drawing.Size(900, 640)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Node-RED Desktop"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.tabDashboard.ResumeLayout(False)
        Me.tabDashboard.PerformLayout()
        Me.pnlDashButtons.ResumeLayout(False)
        Me.pnlResourcesCard.ResumeLayout(False)
        Me.pnlResourcesCard.PerformLayout()
        Me.pnlStatusCard.ResumeLayout(False)
        Me.pnlStatusCard.PerformLayout()
        Me.tabEnvironment.ResumeLayout(False)
        Me.tabEnvironment.PerformLayout()
        Me.pnlEnvButtons.ResumeLayout(False)
        Me.grpNodeRed.ResumeLayout(False)
        Me.grpNodeRed.PerformLayout()
        Me.grpNpm.ResumeLayout(False)
        Me.grpNpm.PerformLayout()
        Me.grpNodeJs.ResumeLayout(False)
        Me.grpNodeJs.PerformLayout()
        Me.tabLog.ResumeLayout(False)
        Me.tabLog.PerformLayout()
        Me.tsLog.ResumeLayout(False)
        Me.tsLog.PerformLayout()
        Me.tabBackup.ResumeLayout(False)
        Me.tabBackup.PerformLayout()
        Me.pnlBackupActions.ResumeLayout(False)
        Me.grpBackupConfig.ResumeLayout(False)
        Me.grpBackupConfig.PerformLayout()
        CType(Me.nudMaxBackups, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSecurity.ResumeLayout(False)
        Me.tabSecurity.PerformLayout()
        Me.pnlSecurityButtons.ResumeLayout(False)
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
        Me.pnlSettingsButtons.ResumeLayout(False)
        Me.grpMinimize.ResumeLayout(False)
        Me.grpMinimize.PerformLayout()
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
    Friend WithEvents lblStatusValue As System.Windows.Forms.Label
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
    Friend WithEvents pnlDashButtons As System.Windows.Forms.Panel
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnRestart As System.Windows.Forms.Button
    Friend WithEvents btnOpenBrowser As System.Windows.Forms.Button
    Friend WithEvents lblUrlLabel As System.Windows.Forms.Label
    Friend WithEvents lnkNodeRedUrl As System.Windows.Forms.LinkLabel
    Friend WithEvents lblRecentLogTitle As System.Windows.Forms.Label
    Friend WithEvents rtbDashLog As System.Windows.Forms.RichTextBox

    '-- Environment --
    Friend WithEvents lblEnvTitle As System.Windows.Forms.Label
    Friend WithEvents lblEnvSubtitle As System.Windows.Forms.Label
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
    Friend WithEvents pnlEnvButtons As System.Windows.Forms.Panel
    Friend WithEvents btnCheckEnvironment As System.Windows.Forms.Button
    Friend WithEvents btnInstallNodeRed As System.Windows.Forms.Button
    Friend WithEvents btnUpdateNodeRed As System.Windows.Forms.Button
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
    Friend WithEvents pnlBackupActions As System.Windows.Forms.Panel
    Friend WithEvents btnBackupNow As System.Windows.Forms.Button
    Friend WithEvents btnRestoreBackup As System.Windows.Forms.Button
    Friend WithEvents btnDeleteBackup As System.Windows.Forms.Button
    Friend WithEvents btnOpenBackupFolder As System.Windows.Forms.Button
    Friend WithEvents lblBackupListTitle As System.Windows.Forms.Label
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
    Friend WithEvents pnlSecurityButtons As System.Windows.Forms.Panel
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
    Friend WithEvents grpMinimize As System.Windows.Forms.GroupBox
    Friend WithEvents chkMinimizeToTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkStartMinimized As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettingsButtons As System.Windows.Forms.Panel
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

#End Region

End Class
