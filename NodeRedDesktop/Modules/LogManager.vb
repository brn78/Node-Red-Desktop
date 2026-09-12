Imports System.Collections.Concurrent
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Threading

' ============================================================
' LogManager.vb - Sistema di logging thread-safe
' Node-RED Desktop
' ============================================================


#Region "Tipi"

    ''' <summary>
    ''' Livello di severita'' di un messaggio di log.
    ''' </summary>
    Public Enum LogLevel As Integer
        ''' <summary>Messaggio informativo standard.</summary>
        INFO = 0
        ''' <summary>Avviso: situazione anomala ma non critica.</summary>
        WARN = 1
        ''' <summary>Errore: operazione fallita.</summary>
        [ERROR] = 2
        ''' <summary>Messaggio di debug per sviluppatori.</summary>
        DEBUG = 3
        ''' <summary>Operazione completata con successo.</summary>
        SUCCESS = 4
    End Enum

    ''' <summary>
    ''' Struttura che rappresenta una singola voce di log.
    ''' </summary>
    Public Structure LogEntry

        ''' <summary>Data e ora in cui il messaggio e'' stato generato.</summary>
        Public Timestamp As DateTime

        ''' <summary>Livello di severita'' del messaggio.</summary>
        Public Level As LogLevel

        ''' <summary>Testo del messaggio di log.</summary>
        Public Message As String

        ''' <summary>Sorgente/componente che ha generato il messaggio.</summary>
        Public Source As String

        ''' <summary>
        ''' Restituisce la riga formattata completa del log.
        ''' Formato: [HH:mm:ss] [LEVEL  ] [Source] Message
        ''' </summary>
        Public Function GetFormattedLine() As String
            Return $"[{Timestamp:HH:mm:ss}] [{GetLevelLabel(),7}] [{Source,-12}] {Message}"
        End Function

        ''' <summary>
        ''' Restituisce l''etichetta testuale del livello di log in italiano.
        ''' </summary>
        Public Function GetLevelLabel() As String
            Select Case Level
                Case LogLevel.INFO    : Return "INFO"
                Case LogLevel.WARN    : Return "AVVISO"
                Case LogLevel.ERROR   : Return "ERRORE"
                Case LogLevel.DEBUG   : Return "DEBUG"
                Case LogLevel.SUCCESS : Return "OK"
                Case Else             : Return "???"
            End Select
        End Function

        ''' <summary>
        ''' Restituisce il colore associato al livello di log per la visualizzazione UI.
        ''' </summary>
        Public Function GetColor() As Color
            Select Case Level
                Case LogLevel.INFO    : Return Color.FromArgb(166, 173, 200) ' Grigio-viola chiaro
                Case LogLevel.WARN    : Return Color.FromArgb(255, 193, 7)   ' Giallo
                Case LogLevel.ERROR   : Return Color.FromArgb(220, 53, 69)   ' Rosso
                Case LogLevel.DEBUG   : Return Color.FromArgb(108, 117, 125) ' Grigio
                Case LogLevel.SUCCESS : Return Color.FromArgb(40, 167, 69)   ' Verde
                Case Else             : Return Color.White
            End Select
        End Function

    End Structure

#End Region

#Region "Modulo LogManager"

    ''' <summary>
    ''' Modulo singleton per la gestione centralizzata del log dell''applicazione.
    ''' Thread-safe: puo'' essere chiamato da qualsiasi thread.
    ''' Gli eventi vengono notificati sul thread UI tramite SynchronizationContext.
    ''' </summary>
    Public Module LogManager

#Region "Stato"

        ''' <summary>Numero massimo di voci mantenute in memoria.</summary>
        Public MaxEntries As Integer = 5000

        ''' <summary>Coda thread-safe delle voci di log.</summary>
        Private _entries As New ConcurrentQueue(Of LogEntry)()

        ''' <summary>Contatore delle voci totali inserite (per gestione overflow).</summary>
        Private _totalCount As Integer = 0

        ''' <summary>
        ''' Contesto di sincronizzazione del thread UI.
        ''' Necessario per fare il marshalling degli eventi sul thread UI.
        ''' </summary>
        Private _syncContext As SynchronizationContext = Nothing

        ''' <summary>Oggetto di lock per operazioni sul contesto di sincronizzazione.</summary>
        Private ReadOnly _ctxLock As New Object()

        ''' <summary>
        ''' Evento sollevato ogni volta che viene aggiunta una nuova voce di log.
        ''' Viene sempre eseguito sul thread UI se InitSyncContext() e'' stato chiamato.
        ''' </summary>
        Public Event LogAdded(entry As LogEntry)

#End Region

#Region "Inizializzazione"

        ''' <summary>
        ''' Inizializza il contesto di sincronizzazione del thread UI.
        ''' DEVE essere chiamato dal thread UI (es: nel costruttore di MainForm o in Program.Main)
        ''' prima di qualsiasi aggiunta di log che deve notificare l''interfaccia.
        ''' </summary>
        Public Sub InitSyncContext()
            SyncLock _ctxLock
                _syncContext = SynchronizationContext.Current
            End SyncLock
        End Sub

#End Region

#Region "Aggiunta Log"

        ''' <summary>
        ''' Aggiunge una nuova voce al log. Thread-safe.
        ''' </summary>
        ''' <param name="message">Testo del messaggio.</param>
        ''' <param name="level">Livello di severita'' (default: INFO).</param>
        ''' <param name="source">Componente sorgente (default: "App").</param>
        Public Sub Add(message As String, Optional level As LogLevel = LogLevel.INFO, Optional source As String = "App")
            Try
                Dim entry As New LogEntry() With {
                    .Timestamp = DateTime.Now,
                    .Level = level,
                    .Message = If(message, String.Empty),
                    .Source = If(source, "App")
                }

                _entries.Enqueue(entry)
                Threading.Interlocked.Increment(_totalCount)

                ' Gestione overflow: rimuove le voci piu'' vecchie se si supera il massimo
                Dim overflow As Integer = _entries.Count - MaxEntries
                Dim dummy As New LogEntry()
                Dim i As Integer = 0
                While i < overflow AndAlso _entries.TryDequeue(dummy)
                    i += 1
                End While

                ' Notifica l''evento sul thread UI
                NotificaLogAdded(entry)

            Catch
                ' Non sollevare mai eccezioni dal logger
            End Try
        End Sub

        ''' <summary>Aggiunge una voce di livello INFO.</summary>
        Public Sub AddInfo(msg As String, Optional src As String = "App")
            Add(msg, LogLevel.INFO, src)
        End Sub

        ''' <summary>Aggiunge una voce di livello WARN (avviso).</summary>
        Public Sub AddWarn(msg As String, Optional src As String = "App")
            Add(msg, LogLevel.WARN, src)
        End Sub

        ''' <summary>Aggiunge una voce di livello ERROR (errore).</summary>
        Public Sub AddError(msg As String, Optional src As String = "App")
            Add(msg, LogLevel.ERROR, src)
        End Sub

        ''' <summary>Aggiunge una voce di livello SUCCESS (successo).</summary>
        Public Sub AddSuccess(msg As String, Optional src As String = "App")
            Add(msg, LogLevel.SUCCESS, src)
        End Sub

        ''' <summary>Aggiunge una voce di livello DEBUG.</summary>
        Public Sub AddDebug(msg As String, Optional src As String = "App")
            Add(msg, LogLevel.DEBUG, src)
        End Sub

        ''' <summary>
        ''' Notifica l''evento LogAdded sul thread UI tramite SynchronizationContext.
        ''' Se il contesto non e'' disponibile, notifica direttamente (potrebbe essere sul thread corrente).
        ''' </summary>
        Private Sub NotificaLogAdded(entry As LogEntry)
            Try
                Dim ctx As SynchronizationContext
                SyncLock _ctxLock
                    ctx = _syncContext
                End SyncLock

                If ctx IsNot Nothing Then
                    ctx.Post(Sub(state)
                                 Try
                                     RaiseEvent LogAdded(DirectCast(state, LogEntry))
                                 Catch
                                 End Try
                             End Sub, entry)
                Else
                    RaiseEvent LogAdded(entry)
                End If
            Catch
            End Try
        End Sub

#End Region

#Region "Lettura ed Esportazione"

        ''' <summary>
        ''' Restituisce tutte le voci di log, con filtro opzionale per livello.
        ''' </summary>
        ''' <param name="filter">Se specificato, restituisce solo le voci di quel livello.</param>
        ''' <returns>Sequenza enumerabile di LogEntry.</returns>
        Public Function GetAll(Optional filter As LogLevel? = Nothing) As IEnumerable(Of LogEntry)
            Dim all As LogEntry() = _entries.ToArray()
            If filter.HasValue Then
                Return Array.FindAll(all, Function(e) e.Level = filter.Value)
            End If
            Return all
        End Function

        ''' <summary>
        ''' Svuota tutte le voci di log dalla memoria.
        ''' </summary>
        Public Sub Clear()
            Dim dummy As New LogEntry()
            While _entries.TryDequeue(dummy)
            End While
            Threading.Interlocked.Exchange(_totalCount, 0)
        End Sub

        ''' <summary>
        ''' Esporta tutte le voci di log in un file di testo.
        ''' </summary>
        ''' <param name="filePath">Percorso del file di destinazione.</param>
        ''' <returns>True se l''esportazione e'' riuscita, False altrimenti.</returns>
        Public Function ExportToText(filePath As String) As Boolean
            Try
                Dim sb As New StringBuilder()
                sb.AppendLine($"=== Log Node-RED Desktop - Esportato il {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===")
                sb.AppendLine()

                For Each entry As LogEntry In _entries.ToArray()
                    sb.AppendLine(entry.GetFormattedLine())
                Next

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)
                Return True

            Catch ex As Exception
                ' Non usare LogManager.AddError qui per evitare ricorsione
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Esporta tutte le voci di log in un file CSV.
        ''' </summary>
        ''' <param name="filePath">Percorso del file CSV di destinazione.</param>
        ''' <returns>True se l''esportazione e'' riuscita, False altrimenti.</returns>
        Public Function ExportToCsv(filePath As String) As Boolean
            Try
                Dim sb As New StringBuilder()
                ' Intestazione CSV
                sb.AppendLine("Timestamp;Livello;Sorgente;Messaggio")

                For Each entry As LogEntry In _entries.ToArray()
                    ' Escape delle virgolette nel messaggio
                    Dim msgEscaped As String = entry.Message.Replace("""", """""")
                    sb.AppendLine($"{entry.Timestamp:yyyy-MM-dd HH:mm:ss};{entry.GetLevelLabel()};{entry.Source};""{msgEscaped}""")
                Next

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)
                Return True

            Catch ex As Exception
                Return False
            End Try
        End Function

#End Region

    End Module

#End Region
