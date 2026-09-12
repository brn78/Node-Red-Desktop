Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.IO.Compression
Imports System.Threading
Imports NodeRedDesktop

Namespace Modules

''' <summary>
''' Modulo per la gestione dei backup di Node-RED.
''' Supporta backup automatici schedulati, ripristino e politiche di retention.
''' </summary>
Public Module BackupManager

#Region "Tipi"

    ''' <summary>
    ''' Rappresenta le informazioni su un singolo file di backup.
    ''' </summary>
    Public Structure BackupInfo
        ''' <summary>Percorso completo del file ZIP di backup.</summary>
        Public FilePath As String
        ''' <summary>Nome del file ZIP di backup.</summary>
        Public FileName As String
        ''' <summary>Data e ora di creazione del backup.</summary>
        Public CreatedAt As DateTime
        ''' <summary>Dimensione del file in byte.</summary>
        Public SizeBytes As Long
        ''' <summary>Versione di Node-RED al momento del backup (da metadata nel ZIP).</summary>
        Public NodeRedVersion As String
    End Structure

#End Region

#Region "Stato"

    ' Delegate per gli eventi di modulo (simulati con liste di handler)
    Public Delegate Sub BackupCompletedHandler(info As BackupInfo)
    Public Delegate Sub BackupFailedHandler(errorMessage As String)

    ' Liste di handler per gli eventi
    Private _completedHandlers As New List(Of BackupCompletedHandler)()
    Private _failedHandlers As New List(Of BackupFailedHandler)()

    ' Timer per il backup automatico
    Private _backupTimer As Threading.Timer
    ' Flag per indicare se un backup e in corso
    Private _isRunning As Boolean = False
    ' Lock per thread-safety
    Private ReadOnly _lock As New Object()

    ''' <summary>
    ''' Aggiunge un handler all evento BackupCompleted.
    ''' </summary>
    Public Sub AddBackupCompletedHandler(handler As BackupCompletedHandler)
        SyncLock _lock
            _completedHandlers.Add(handler)
        End SyncLock
    End Sub

    ''' <summary>
    ''' Rimuove un handler dall evento BackupCompleted.
    ''' </summary>
    Public Sub RemoveBackupCompletedHandler(handler As BackupCompletedHandler)
        SyncLock _lock
            _completedHandlers.Remove(handler)
        End SyncLock
    End Sub

    ''' <summary>
    ''' Aggiunge un handler all evento BackupFailed.
    ''' </summary>
    Public Sub AddBackupFailedHandler(handler As BackupFailedHandler)
        SyncLock _lock
            _failedHandlers.Add(handler)
        End SyncLock
    End Sub

    ''' <summary>
    ''' Rimuove un handler dall evento BackupFailed.
    ''' </summary>
    Public Sub RemoveBackupFailedHandler(handler As BackupFailedHandler)
        SyncLock _lock
            _failedHandlers.Remove(handler)
        End SyncLock
    End Sub

    ''' <summary>Notifica tutti gli handler di BackupCompleted.</summary>
    Private Sub RaiseBackupCompleted(info As BackupInfo)
        Dim handlers As List(Of BackupCompletedHandler)
        SyncLock _lock
            handlers = New List(Of BackupCompletedHandler)(_completedHandlers)
        End SyncLock
        For Each h In handlers
            Try
                h(info)
            Catch ex As Exception
                ' Ignora errori negli handler esterni
            End Try
        Next
    End Sub

    ''' <summary>Notifica tutti gli handler di BackupFailed.</summary>
    Private Sub RaiseBackupFailed(errorMessage As String)
        Dim handlers As List(Of BackupFailedHandler)
        SyncLock _lock
            handlers = New List(Of BackupFailedHandler)(_failedHandlers)
        End SyncLock
        For Each h In handlers
            Try
                h(errorMessage)
            Catch ex As Exception
                ' Ignora errori negli handler esterni
            End Try
        Next
    End Sub

#End Region

#Region "Backup Automatico"

    ''' <summary>
    ''' Avvia il timer per i backup automatici basandosi su AppSettings.Current.BackupSchedule.
    ''' Schedule supportati: "Hourly", "Daily", "Weekly", "Disabled".
    ''' </summary>
    Public Sub StartAutoBackup()
        Try
            StopAutoBackup()

            Dim schedule As String = AppSettings.Current.BackupSchedule
            If String.IsNullOrWhiteSpace(schedule) OrElse schedule.Equals("Disabled", StringComparison.OrdinalIgnoreCase) Then
                LogManager.AddInfo("Backup automatico disabilitato dalla configurazione.", "BackupManager")
                Return
            End If

            Dim intervalMs As Long
            Dim dueTimeMs As Long = 0

            Select Case schedule.ToLower()
                Case "hourly"
                    intervalMs = 3600000L
                    dueTimeMs = 3600000L
                    LogManager.AddInfo("Backup automatico schedulato: ogni ora.", "BackupManager")

                Case "daily"
                    intervalMs = 86400000L
                    Dim now As DateTime = DateTime.Now
                    Dim nextMidnight As DateTime = now.Date.AddDays(1)
                    dueTimeMs = CLng((nextMidnight - now).TotalMilliseconds)
                    LogManager.AddInfo(String.Format("Backup automatico schedulato: giornaliero. Prossima esecuzione: {0:HH:mm}", nextMidnight), "BackupManager")

                Case "weekly"
                    intervalMs = 7L * 86400000L
                    dueTimeMs = intervalMs
                    LogManager.AddInfo("Backup automatico schedulato: settimanale.", "BackupManager")

                Case Else
                    LogManager.AddWarn(String.Format("Schedule backup non riconosciuto: '{0}'. Auto-backup non avviato.", schedule), "BackupManager")
                    Return
            End Select

            _backupTimer = New Threading.Timer(AddressOf AutoBackupCallback, Nothing, dueTimeMs, intervalMs)

        Catch ex As Exception
            LogManager.AddError(String.Format("Errore avvio backup automatico: {0}", ex.Message), "BackupManager")
        End Try
    End Sub

    ''' <summary>
    ''' Ferma il timer per i backup automatici.
    ''' </summary>
    Public Sub StopAutoBackup()
        Try
            If _backupTimer IsNot Nothing Then
                _backupTimer.Dispose()
                _backupTimer = Nothing
                LogManager.AddInfo("Backup automatico fermato.", "BackupManager")
            End If
        Catch ex As Exception
            LogManager.AddError(String.Format("Errore interruzione backup automatico: {0}", ex.Message), "BackupManager")
        End Try
    End Sub

    ''' <summary>
    ''' Callback invocato dal timer per eseguire il backup automatico.
    ''' </summary>
    Private Sub AutoBackupCallback(state As Object)
        SyncLock _lock
            If _isRunning Then Return
            _isRunning = True
        End SyncLock

        Try
            LogManager.AddInfo("Avvio backup automatico schedulato...", "BackupManager")
            Dim info As BackupInfo = CreateBackup()
            EnforceRetention()
            RaiseBackupCompleted(info)
            LogManager.AddSuccess(String.Format("Backup automatico completato: {0}", info.FileName), "BackupManager")
        Catch ex As Exception
            Dim msg As String = String.Format("Backup automatico fallito: {0}", ex.Message)
            LogManager.AddError(msg, "BackupManager")
            RaiseBackupFailed(msg)
        Finally
            SyncLock _lock
                _isRunning = False
            End SyncLock
        End Try
    End Sub

#End Region

#Region "Backup e Ripristino"

    ''' <summary>
    ''' Crea un backup ZIP dei file principali di Node-RED.
    ''' File inclusi: flows.json, flows_cred.json, settings.js, package.json, package-lock.json.
    ''' Il file viene nominato backup_YYYY-MM-DD_HH-mm-ss.zip.
    ''' </summary>
    ''' <returns>BackupInfo con le informazioni del backup creato.</returns>
    Public Function CreateBackup() As BackupInfo
        Dim info As New BackupInfo()
        Try
            Dim backupFolder As String = GetBackupFolder()
            If Not Directory.Exists(backupFolder) Then
                Directory.CreateDirectory(backupFolder)
            End If

            Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
            Dim zipFileName As String = String.Format("backup_{0}.zip", timestamp)
            Dim zipFilePath As String = Path.Combine(backupFolder, zipFileName)

            Dim nodeRedDir As String = GetNodeRedUserDir()

            Dim filesToBackup As String() = {"flows.json", "flows_cred.json", "settings.js", "package.json", "package-lock.json"}

            Using archive As ZipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create)
                For Each fileName In filesToBackup
                    Dim filePath As String = Path.Combine(nodeRedDir, fileName)
                    If File.Exists(filePath) Then
                        archive.CreateEntryFromFile(filePath, fileName, CompressionLevel.Optimal)
                        LogManager.AddInfo(String.Format("File aggiunto al backup: {0}", fileName), "BackupManager")
                    End If
                Next

                Dim nodeRedVersion As String = GetInstalledNodeRedVersion(nodeRedDir)
                Dim metadataContent As String = String.Format(
                    "{{""nodeRedVersion"":""{0}"",""backupDate"":""{1:O}"",""host"":""{2}""}}",
                    nodeRedVersion, DateTime.Now, Environment.MachineName)
                Dim metaEntry As ZipArchiveEntry = archive.CreateEntry("_backup_metadata.json")
                Using sw As New IO.StreamWriter(metaEntry.Open())
                    sw.Write(metadataContent)
                End Using

                info.NodeRedVersion = nodeRedVersion
            End Using

            Dim fi As New FileInfo(zipFilePath)
            info.FilePath = zipFilePath
            info.FileName = zipFileName
            info.CreatedAt = fi.CreationTime
            info.SizeBytes = fi.Length

            LogManager.AddSuccess(String.Format("Backup creato: {0} ({1})", zipFileName, FormatBytes(info.SizeBytes)), "BackupManager")
            Return info

        Catch ex As Exception
            LogManager.AddError(String.Format("Errore creazione backup: {0}", ex.Message), "BackupManager")
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Elenca tutti i backup disponibili nella cartella backup, ordinati per data decrescente.
    ''' </summary>
    ''' <returns>Lista di BackupInfo ordinata dalla piu recente alla piu vecchia.</returns>
    Public Function ListBackups() As List(Of BackupInfo)
        Dim result As New List(Of BackupInfo)()
        Try
            Dim backupFolder As String = GetBackupFolder()
            If Not Directory.Exists(backupFolder) Then
                Return result
            End If

            Dim zipFiles As String() = Directory.GetFiles(backupFolder, "*.zip")
            For Each zipPath In zipFiles
                Try
                    Dim fi As New FileInfo(zipPath)
                    Dim info As New BackupInfo()
                    info.FilePath = zipPath
                    info.FileName = fi.Name
                    info.CreatedAt = fi.CreationTime
                    info.SizeBytes = fi.Length
                    info.NodeRedVersion = ReadVersionFromZip(zipPath)
                    result.Add(info)
                Catch ex As Exception
                    LogManager.AddWarn(String.Format("Impossibile leggere backup '{0}': {1}", Path.GetFileName(zipPath), ex.Message), "BackupManager")
                End Try
            Next

            result.Sort(Function(a, b) b.CreatedAt.CompareTo(a.CreatedAt))

        Catch ex As Exception
            LogManager.AddError(String.Format("Errore elenco backup: {0}", ex.Message), "BackupManager")
        End Try
        Return result
    End Function

    ''' <summary>
    ''' Ripristina un backup ZIP nella directory Node-RED.
    ''' Prima di ripristinare, crea automaticamente un backup dello stato attuale.
    ''' </summary>
    ''' <param name="backupPath">Percorso del file ZIP da ripristinare.</param>
    ''' <returns>True se il ripristino e riuscito, False altrimenti.</returns>
    Public Function RestoreBackup(backupPath As String) As Boolean
        Try
            If Not File.Exists(backupPath) Then
                LogManager.AddError(String.Format("File di backup non trovato: {0}", backupPath), "BackupManager")
                Return False
            End If

            Dim nodeRedDir As String = GetNodeRedUserDir()

            LogManager.AddInfo("Creazione backup preventivo prima del ripristino...", "BackupManager")
            Try
                Dim preRestoreBackup As BackupInfo = CreateBackup()
                LogManager.AddInfo(String.Format("Backup preventivo creato: {0}", preRestoreBackup.FileName), "BackupManager")
            Catch backupEx As Exception
                LogManager.AddWarn(String.Format("Impossibile creare backup preventivo: {0}. Procedo ugualmente.", backupEx.Message), "BackupManager")
            End Try

            LogManager.AddInfo(String.Format("Ripristino backup: {0}...", Path.GetFileName(backupPath)), "BackupManager")
            Using archive As ZipArchive = ZipFile.OpenRead(backupPath)
                For Each entry In archive.Entries
                    If entry.Name.StartsWith("_backup_") Then Continue For
                    If String.IsNullOrEmpty(entry.Name) Then Continue For

                    Dim targetPath As String = Path.Combine(nodeRedDir, entry.FullName)
                    Dim targetDir As String = Path.GetDirectoryName(targetPath)

                    If Not Directory.Exists(targetDir) Then
                        Directory.CreateDirectory(targetDir)
                    End If

                    entry.ExtractToFile(targetPath, overwrite:=True)
                    LogManager.AddInfo(String.Format("Ripristinato: {0}", entry.Name), "BackupManager")
                Next
            End Using

            LogManager.AddSuccess(String.Format("Backup ripristinato con successo: {0}", Path.GetFileName(backupPath)), "BackupManager")
            Return True

        Catch ex As Exception
            LogManager.AddError(String.Format("Errore ripristino backup '{0}': {1}", Path.GetFileName(backupPath), ex.Message), "BackupManager")
            Return False
        End Try
    End Function

#End Region

#Region "Gestione File"

    ''' <summary>
    ''' Applica la politica di retention eliminando i backup piu vecchi
    ''' se il numero totale supera AppSettings.Current.MaxBackups.
    ''' </summary>
    Public Sub EnforceRetention()
        Try
            Dim maxBackups As Integer = AppSettings.Current.MaxBackups
            If maxBackups <= 0 Then Return

            Dim backups As List(Of BackupInfo) = ListBackups()
            If backups.Count <= maxBackups Then Return

            Dim toDelete As List(Of BackupInfo) = backups.GetRange(maxBackups, backups.Count - maxBackups)
            For Each old In toDelete
                Try
                    File.Delete(old.FilePath)
                    LogManager.AddInfo(String.Format("Backup eliminato per retention policy: {0}", old.FileName), "BackupManager")
                Catch delEx As Exception
                    LogManager.AddWarn(String.Format("Impossibile eliminare backup '{0}': {1}", old.FileName, delEx.Message), "BackupManager")
                End Try
            Next

        Catch ex As Exception
            LogManager.AddError(String.Format("Errore applicazione retention policy: {0}", ex.Message), "BackupManager")
        End Try
    End Sub

    ''' <summary>
    ''' Restituisce la cartella dei backup: quella configurata o il percorso default.
    ''' Default: %APPDATA%\NodeRedDesktop\Backups\
    ''' </summary>
    Public Function GetBackupFolder() As String
        Dim configured As String = AppSettings.Current.BackupFolder
        If Not String.IsNullOrWhiteSpace(configured) Then
            Return configured
        End If
        Return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NodeRedDesktop",
            "Backups"
        )
    End Function

    ''' <summary>
    ''' Restituisce la directory utente di Node-RED.
    ''' Usa UserDir da AppSettings oppure il default ~/.node-red.
    ''' </summary>
    Private Function GetNodeRedUserDir() As String
        Dim userDir As String = AppSettings.Current.UserDir
        If Not String.IsNullOrWhiteSpace(userDir) Then
            Return userDir
        End If
        Return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".node-red"
        )
    End Function

    ''' <summary>
    ''' Legge la versione di Node-RED installata dal file package.json nella userDir.
    ''' </summary>
    Private Function GetInstalledNodeRedVersion(nodeRedDir As String) As String
        Try
            Dim pkgPath As String = Path.Combine(nodeRedDir, "package.json")
            If File.Exists(pkgPath) Then
                Dim content As String = File.ReadAllText(pkgPath)
                Dim match As System.Text.RegularExpressions.Match =
                    System.Text.RegularExpressions.Regex.Match(content, """node-red""\s*:\s*""([^""]+)""")
                If match.Success Then Return match.Groups(1).Value
            End If
        Catch ex As Exception
            ' Ignora errori nella lettura versione
        End Try
        Return "unknown"
    End Function

    ''' <summary>
    ''' Legge la versione di Node-RED dal file _backup_metadata.json dentro il ZIP.
    ''' </summary>
    Private Function ReadVersionFromZip(zipPath As String) As String
        Try
            Using archive As ZipArchive = ZipFile.OpenRead(zipPath)
                Dim metaEntry As ZipArchiveEntry = archive.GetEntry("_backup_metadata.json")
                If metaEntry IsNot Nothing Then
                    Using sr As New IO.StreamReader(metaEntry.Open())
                        Dim content As String = sr.ReadToEnd()
                        Dim match As System.Text.RegularExpressions.Match =
                            System.Text.RegularExpressions.Regex.Match(content, """nodeRedVersion""\s*:\s*""([^""]+)""")
                        If match.Success Then Return match.Groups(1).Value
                    End Using
                End If
            End Using
        Catch ex As Exception
            ' Ignora errori di lettura metadata
        End Try
        Return "unknown"
    End Function

    ''' <summary>
    ''' Formatta una dimensione in byte in formato leggibile (KB, MB, GB).
    ''' </summary>
    Private Function FormatBytes(bytes As Long) As String
        If bytes < 1024 Then Return String.Format("{0} B", bytes)
        If bytes < 1048576 Then Return String.Format("{0:F1} KB", bytes / 1024.0)
        If bytes < 1073741824 Then Return String.Format("{0:F1} MB", bytes / 1048576.0)
        Return String.Format("{0:F1} GB", bytes / 1073741824.0)
    End Function

#End Region

End Module

End Namespace
