Imports System.Windows.Forms
Imports System.Threading

' ============================================================
' Program.vb - Punto di ingresso dell''applicazione
' Node-RED Desktop
' ============================================================


    ''' <summary>
    ''' Classe principale di avvio dell''applicazione.
    ''' Configura il tema visivo, la gestione degli errori globali
    ''' e avvia il form principale.
    ''' </summary>
    Module Program

        ''' <summary>
        ''' Punto di ingresso principale dell''applicazione.
        ''' </summary>
        <STAThread>
        Sub Main()
            ' --- Configurazione rendering visivo ---
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            ' --- Gestione eccezioni globali UI thread ---
            AddHandler Application.ThreadException, AddressOf OnThreadException

            ' --- Gestione eccezioni globali su tutti i thread ---
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnUnhandledException

            ' --- Imposta modalita'' gestione eccezioni thread ---
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)

            Try
                ' --- Avvio del form principale ---
                Application.Run(New MainForm())
            Catch ex As Exception
                MostraErroreCritico(ex, "Errore critico durante l''avvio dell''applicazione")
            End Try
        End Sub

#Region "Gestione Eccezioni Globali"

        ''' <summary>
        ''' Gestisce le eccezioni non catturate nel thread UI.
        ''' </summary>
        Private Sub OnThreadException(sender As Object, e As Threading.ThreadExceptionEventArgs)
            MostraErroreCritico(e.Exception, "Eccezione non gestita nel thread UI")
        End Sub

        ''' <summary>
        ''' Gestisce le eccezioni non catturate in qualsiasi thread dell''applicazione.
        ''' </summary>
        Private Sub OnUnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
            Dim ex As Exception = TryCast(e.ExceptionObject, Exception)
            If ex IsNot Nothing Then
                MostraErroreCritico(ex, "Eccezione non gestita (dominio applicazione)")
            Else
                MessageBox.Show(
                    "Si e'' verificato un errore imprevisto non identificabile." & Environment.NewLine &
                    "L''applicazione potrebbe essere in uno stato instabile.",
                    "Errore Critico - Node-RED Desktop",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
            End If
        End Sub

        ''' <summary>
        ''' Mostra una finestra di errore critico e tenta di registrare l''eccezione nel log.
        ''' </summary>
        ''' <param name="ex">L''eccezione verificatasi.</param>
        ''' <param name="contesto">Descrizione del contesto dell''errore.</param>
        Private Sub MostraErroreCritico(ex As Exception, contesto As String)
            ' Tentativo di log tramite LogManager (se gia'' inizializzato)
            Try
                LogManager.AddError($"{contesto}: {ex.Message}", "Program")
                LogManager.AddError($"Stack Trace: {ex.StackTrace}", "Program")
            Catch
                ' Il LogManager potrebbe non essere ancora inizializzato, ignoriamo l''errore di log
            End Try

            ' Costruzione del messaggio di errore leggibile
            Dim messaggio As String = String.Format(
                "Si e'' verificato un errore non previsto.{0}{0}" &
                "Contesto: {1}{0}" &
                "Errore: {2}{0}{0}" &
                "Dettagli tecnici:{0}{3}{0}{0}" &
                "L''applicazione tentera'' di continuare, ma potrebbe essere in uno stato instabile.",
                Environment.NewLine,
                contesto,
                ex.Message,
                If(ex.StackTrace?.Length > 500, ex.StackTrace.Substring(0, 500) & "...", ex.StackTrace))

            MessageBox.Show(
                messaggio,
                "Errore Critico - Node-RED Desktop",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Sub

#End Region

    End Module
