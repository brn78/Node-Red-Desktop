Imports System.IO
Imports System.Xml.Serialization

' ============================================================
' AppSettings.vb - Configurazione applicazione Node-RED Desktop
' Servizio singleton per la gestione delle impostazioni
' ============================================================


#Region "Classe di Configurazione AppConfig"

    ''' <summary>
    ''' Classe di dati di configurazione serializzabile in XML.
    ''' Contiene tutte le impostazioni dell''applicazione Node-RED Desktop.
    ''' </summary>
    <XmlRoot("NodeRedDesktopConfig")>
    Public Class AppConfig

#Region "Impostazioni Node-RED"

        ''' <summary>Porta HTTP su cui Node-RED ascolta.</summary>
        <XmlElement("NodeRedPort")>
        Public Property NodeRedPort As Integer = 1880

        ''' <summary>Directory utente di Node-RED (userDir). Vuoto = default %USERPROFILE%\.node-red</summary>
        <XmlElement("UserDir")>
        Public Property UserDir As String = ""

        ''' <summary>Nome del file dei flussi Node-RED.</summary>
        <XmlElement("FlowFile")>
        Public Property FlowFile As String = "flows.json"

#End Region

#Region "Impostazioni Avvio Automatico"

        ''' <summary>Avvia automaticamente l''applicazione con Windows.</summary>
        <XmlElement("AutoStartWindows")>
        Public Property AutoStartWindows As Boolean = False

        ''' <summary>Metodo di avvio automatico: "Registry" o "TaskScheduler".</summary>
        <XmlElement("StartupMethod")>
        Public Property StartupMethod As String = "Registry"

        ''' <summary>Avvia automaticamente Node-RED all''apertura dell''applicazione.</summary>
        <XmlElement("AutoStartNodeRed")>
        Public Property AutoStartNodeRed As Boolean = True

        ''' <summary>Ritardo in secondi prima di avviare Node-RED all''apertura dell''app.</summary>
        <XmlElement("StartDelaySeconds")>
        Public Property StartDelaySeconds As Integer = 3

#End Region

#Region "Impostazioni Watchdog"

        ''' <summary>Abilita il watchdog per il riavvio automatico di Node-RED se si ferma.</summary>
        <XmlElement("WatchdogEnabled")>
        Public Property WatchdogEnabled As Boolean = True

        ''' <summary>Intervallo in secondi tra i controlli del watchdog.</summary>
        <XmlElement("WatchdogIntervalSeconds")>
        Public Property WatchdogIntervalSeconds As Integer = 30

        ''' <summary>Numero massimo di riavvii automatici prima di arrendersi.</summary>
        <XmlElement("MaxRestarts")>
        Public Property MaxRestarts As Integer = 5

#End Region

#Region "Impostazioni Backup"

        ''' <summary>Cartella di destinazione per i backup. Vuoto = disabilitato.</summary>
        <XmlElement("BackupFolder")>
        Public Property BackupFolder As String = ""

        ''' <summary>Frequenza di backup: "Disabled", "Hourly", "Daily", "Weekly".</summary>
        <XmlElement("BackupSchedule")>
        Public Property BackupSchedule As String = "Daily"

        ''' <summary>Numero massimo di backup conservati prima di eliminare i vecchi.</summary>
        <XmlElement("MaxBackups")>
        Public Property MaxBackups As Integer = 10

#End Region

#Region "Percorsi Eseguibili"

        ''' <summary>Percorso completo dell''eseguibile node.exe.</summary>
        <XmlElement("NodeExePath")>
        Public Property NodeExePath As String = ""

        ''' <summary>Percorso completo dell''eseguibile npm (npm.cmd).</summary>
        <XmlElement("NpmExePath")>
        Public Property NpmExePath As String = ""

        ''' <summary>Percorso completo dello script node-red.cmd.</summary>
        <XmlElement("NodeRedCmdPath")>
        Public Property NodeRedCmdPath As String = ""

        ''' <summary>Ultima versione nota di Node.js rilevata.</summary>
        <XmlElement("LastKnownNodeVersion")>
        Public Property LastKnownNodeVersion As String = ""

        ''' <summary>Ultima versione nota di npm rilevata.</summary>
        <XmlElement("LastKnownNpmVersion")>
        Public Property LastKnownNpmVersion As String = ""

        ''' <summary>Ultima versione nota di Node-RED rilevata.</summary>
        <XmlElement("LastKnownNodeRedVersion")>
        Public Property LastKnownNodeRedVersion As String = ""

#End Region

#Region "Impostazioni Sicurezza"

        ''' <summary>Abilita l''autenticazione in Node-RED (settings.js adminAuth).</summary>
        <XmlElement("AuthEnabled")>
        Public Property AuthEnabled As Boolean = False

        ''' <summary>Abilita la restrizione degli IP ammessi.</summary>
        <XmlElement("IpRestrictionEnabled")>
        Public Property IpRestrictionEnabled As Boolean = False

        ''' <summary>IP ammessi separati da punto e virgola (es: "127.0.0.1;192.168.1.0/24").</summary>
        <XmlElement("AllowedIps")>
        Public Property AllowedIps As String = ""

        ''' <summary>Abilita autenticazione LDAP.</summary>
        <XmlElement("LdapEnabled")>
        Public Property LdapEnabled As Boolean = False

        ''' <summary>URL del server LDAP (es: "ldap://192.168.1.10").</summary>
        <XmlElement("LdapUrl")>
        Public Property LdapUrl As String = ""

        ''' <summary>Base DN per la ricerca LDAP (es: "dc=azienda,dc=local").</summary>
        <XmlElement("LdapBaseDn")>
        Public Property LdapBaseDn As String = ""

#End Region

#Region "Impostazioni Interfaccia"

        ''' <summary>Minimizza nella system tray invece di chiudere.</summary>
        <XmlElement("MinimizeToTray")>
        Public Property MinimizeToTray As Boolean = True

        ''' <summary>Avvia l''applicazione minimizzata.</summary>
        <XmlElement("StartMinimized")>
        Public Property StartMinimized As Boolean = False

        ''' <summary>Tema grafico: "Dark" o "Light".</summary>
        <XmlElement("Theme")>
        Public Property Theme As String = "Dark"

#End Region

    End Class

#End Region

#Region "Servizio Singleton AppSettings"

    ''' <summary>
    ''' Servizio singleton per la gestione delle impostazioni dell''applicazione.
    ''' Gestisce il caricamento e il salvataggio della configurazione su file XML.
    ''' </summary>
    Public Module AppSettings

#Region "Proprieta'"

        Private _configPath As String = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NodeRedDesktop",
            "config.xml")
        Private _current As AppConfig = Nothing

        ''' <summary>Percorso del file di configurazione XML.</summary>
        Public ReadOnly Property ConfigPath As String
            Get
                Return _configPath
            End Get
        End Property

        ''' <summary>
        ''' Configurazione corrente. Se non ancora caricata, viene caricata automaticamente.
        ''' </summary>
        Public Property Current As AppConfig
            Get
                If _current Is Nothing Then
                    Load()
                End If
                Return _current
            End Get
            Set(value As AppConfig)
                _current = value
            End Set
        End Property

#End Region

#Region "Metodi Load/Save"

        ''' <summary>
        ''' Carica la configurazione dal file XML.
        ''' Se il file non esiste o e'' corrotto, crea una configurazione di default.
        ''' </summary>
        Public Sub Load()
            Try
                If File.Exists(_configPath) Then
                    Dim serializer As New XmlSerializer(GetType(AppConfig))
                    Using stream As New FileStream(_configPath, FileMode.Open, FileAccess.Read, FileShare.Read)
                        _current = TryCast(serializer.Deserialize(stream), AppConfig)
                    End Using

                    ' Verifica integrita'' della configurazione caricata
                    If _current Is Nothing Then
                        LogManager.AddWarn("File di configurazione corrotto o vuoto: creazione di default.", "AppSettings")
                        _current = New AppConfig()
                    End If
                Else
                    ' Prima esecuzione: crea configurazione di default
                    _current = New AppConfig()
                    LogManager.AddInfo("Configurazione di default creata (prima esecuzione).", "AppSettings")
                    Save()
                End If

            Catch ex As Exception
                LogManager.AddError($"Errore durante il caricamento della configurazione: {ex.Message}", "AppSettings")
                _current = New AppConfig()
            End Try
        End Sub

        ''' <summary>
        ''' Salva la configurazione corrente sul file XML.
        ''' Crea la directory se non esiste.
        ''' </summary>
        Public Sub Save()
            Try
                ' Assicurati che la directory esista
                Dim dir As String = Path.GetDirectoryName(_configPath)
                If Not Directory.Exists(dir) Then
                    Directory.CreateDirectory(dir)
                End If

                Dim serializer As New XmlSerializer(GetType(AppConfig))
                Using stream As New FileStream(_configPath, FileMode.Create, FileAccess.Write, FileShare.None)
                    serializer.Serialize(stream, _current)
                End Using

                LogManager.AddDebug("Configurazione salvata con successo.", "AppSettings")

            Catch ex As Exception
                LogManager.AddError($"Errore durante il salvataggio della configurazione: {ex.Message}", "AppSettings")
            End Try
        End Sub

#End Region

#Region "Metodi Utilita'"

        ''' <summary>
        ''' Restituisce la directory utente di Node-RED.
        ''' Se UserDir e'' vuoto, usa il percorso di default: %USERPROFILE%\.node-red
        ''' </summary>
        ''' <returns>Percorso assoluto della directory utente Node-RED.</returns>
        Public Function GetUserDir() As String
            Dim cfg As AppConfig = Current
            If Not String.IsNullOrWhiteSpace(cfg.UserDir) AndAlso Directory.Exists(cfg.UserDir) Then
                Return cfg.UserDir
            End If
            Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".node-red")
        End Function

        ''' <summary>
        ''' Restituisce il percorso di node-red.cmd.
        ''' Prima controlla il percorso configurato, poi tenta l''auto-rilevamento.
        ''' </summary>
        ''' <returns>Percorso di node-red.cmd o stringa vuota se non trovato.</returns>
        Public Function GetNodeRedCmd() As String
            Dim cfg As AppConfig = Current

            ' 1. Usa il percorso configurato se valido
            If Not String.IsNullOrWhiteSpace(cfg.NodeRedCmdPath) AndAlso File.Exists(cfg.NodeRedCmdPath) Then
                Return cfg.NodeRedCmdPath
            End If

            ' 2. Tenta auto-rilevamento
            AutoDetectPaths()
            If Not String.IsNullOrWhiteSpace(cfg.NodeRedCmdPath) AndAlso File.Exists(cfg.NodeRedCmdPath) Then
                Return cfg.NodeRedCmdPath
            End If

            Return String.Empty
        End Function

        ''' <summary>
        ''' Auto-rileva i percorsi di Node.js, npm e node-red.cmd cercando nelle posizioni comuni:
        ''' - PATH di sistema/utente
        ''' - Percorsi WinGet standard
        ''' - Percorsi Chocolatey
        ''' - Percorsi NVM for Windows
        ''' - Percorsi di installazione globale npm
        ''' </summary>
        Public Sub AutoDetectPaths()
            Try
                Dim cfg As AppConfig = Current
                LogManager.AddInfo("Avvio auto-rilevamento percorsi Node.js / npm / Node-RED...", "AppSettings")

                ' --- Raccolta percorsi di ricerca ---
                Dim searchPaths As New List(Of String)()

                ' Percorsi da PATH di sistema
                Dim pathEnv As String = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine) & ";" &
                                        Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User)
                For Each p As String In pathEnv.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)
                    Dim trimmed As String = p.Trim()
                    If Not String.IsNullOrEmpty(trimmed) Then searchPaths.Add(trimmed)
                Next

                ' Percorsi WinGet (installazione utente e macchina)
                Dim localAppData As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                Dim programFiles As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                Dim programFilesX86 As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)

                searchPaths.Add(Path.Combine(localAppData, "Programs", "nodejs"))
                searchPaths.Add(Path.Combine(programFiles, "nodejs"))
                searchPaths.Add(Path.Combine(programFilesX86, "nodejs"))

                ' Percorsi Chocolatey
                searchPaths.Add("C:\ProgramData\chocolatey\bin")
                searchPaths.Add("C:\tools\nodejs")

                ' Percorsi NVM for Windows
                Dim nvmRoot As String = If(Environment.GetEnvironmentVariable("NVM_HOME"), "")
                If Not String.IsNullOrEmpty(nvmRoot) Then
                    searchPaths.Add(nvmRoot)
                End If
                searchPaths.Add(Path.Combine(localAppData, "nvm"))
                searchPaths.Add(Path.Combine(programFiles, "nvm"))

                ' Variabile NVM_SYMLINK (percorso Node corrente di NVM)
                Dim nvmSymlink As String = If(Environment.GetEnvironmentVariable("NVM_SYMLINK"), "")
                If Not String.IsNullOrEmpty(nvmSymlink) Then
                    searchPaths.Add(nvmSymlink)
                End If

                ' --- Ricerca node.exe ---
                If String.IsNullOrWhiteSpace(cfg.NodeExePath) OrElse Not File.Exists(cfg.NodeExePath) Then
                    For Each dir As String In searchPaths
                        Dim candidate As String = Path.Combine(dir, "node.exe")
                        If File.Exists(candidate) Then
                            cfg.NodeExePath = candidate
                            LogManager.AddSuccess($"Node.js trovato: {candidate}", "AppSettings")
                            Exit For
                        End If
                    Next
                End If

                ' --- Ricerca npm.cmd ---
                If String.IsNullOrWhiteSpace(cfg.NpmExePath) OrElse Not File.Exists(cfg.NpmExePath) Then
                    For Each dir As String In searchPaths
                        Dim candidate As String = Path.Combine(dir, "npm.cmd")
                        If File.Exists(candidate) Then
                            cfg.NpmExePath = candidate
                            LogManager.AddSuccess($"npm trovato: {candidate}", "AppSettings")
                            Exit For
                        End If
                    Next
                End If

                ' --- Ricerca node-red.cmd ---
                If String.IsNullOrWhiteSpace(cfg.NodeRedCmdPath) OrElse Not File.Exists(cfg.NodeRedCmdPath) Then
                    ' Percorsi aggiuntivi per node-red.cmd (installazione globale npm)
                    Dim nodeRedPaths As New List(Of String)(searchPaths)

                    ' npm global prefix predefiniti
                    Dim appData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    nodeRedPaths.Add(Path.Combine(appData, "npm"))
                    nodeRedPaths.Add(Path.Combine(appData, "Roaming", "npm"))
                    nodeRedPaths.Add(Path.Combine(localAppData, "npm"))

                    ' Se node.exe trovato, aggiungi la sua directory e npm globale relativo
                    If Not String.IsNullOrEmpty(cfg.NodeExePath) Then
                        Dim nodeDir As String = Path.GetDirectoryName(cfg.NodeExePath)
                        nodeRedPaths.Add(nodeDir)
                        nodeRedPaths.Add(Path.Combine(nodeDir, "node_modules", ".bin"))
                    End If

                    For Each dir As String In nodeRedPaths
                        Dim candidate As String = Path.Combine(dir, "node-red.cmd")
                        If File.Exists(candidate) Then
                            cfg.NodeRedCmdPath = candidate
                            LogManager.AddSuccess($"node-red.cmd trovato: {candidate}", "AppSettings")
                            Exit For
                        End If
                    Next
                End If

                ' --- Riepilogo ---
                If String.IsNullOrEmpty(cfg.NodeExePath) Then
                    LogManager.AddWarn("Node.js non trovato automaticamente. Configurare manualmente.", "AppSettings")
                End If
                If String.IsNullOrEmpty(cfg.NodeRedCmdPath) Then
                    LogManager.AddWarn("node-red.cmd non trovato automaticamente. Assicurarsi che Node-RED sia installato globalmente (npm install -g node-red).", "AppSettings")
                End If

                ' Salva i percorsi trovati
                Save()

            Catch ex As Exception
                LogManager.AddError($"Errore durante l''auto-rilevamento dei percorsi: {ex.Message}", "AppSettings")
            End Try
        End Sub

#End Region

    End Module

#End Region
