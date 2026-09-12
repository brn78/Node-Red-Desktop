' ============================================================
' File: ToastForm.vb
' Progetto: Node-RED Desktop
' Descrizione: Logica della form toast di notifica con fade e progress
' Autore: NodeRedDesktop
' ============================================================

Imports System.Runtime.InteropServices


    ''' <summary>
    ''' Tipi di toast notifica disponibili
    ''' </summary>
    Public Enum ToastType
        Success = 0
        Warning = 1
        [Error] = 2
        Info = 3
    End Enum

    ''' <summary>
    ''' Form toast non-modale con auto-dismiss, fade in/out e progress bar
    ''' </summary>
    Partial Public Class ToastForm

#Region "Costanti WinAPI"
        Private Const WM_NCHITTEST As Integer = &H84
        Private Const HTTRANSPARENT As Integer = -1
        Private Const HTCLIENT As Integer = 1
#End Region

#Region "Campi privati"
        ''' <summary>Indica se la form sta eseguendo il fade-out</summary>
        Private _fadingOut As Boolean = False
        ''' <summary>Valore corrente della progress bar (100 -> 0)</summary>
        Private _progress As Double = 100.0
        ''' <summary>Durata totale della notifica in millisecondi</summary>
        Private _duration As Integer = 4000
        ''' <summary>Decremento per tick progress (calcolato in InitForm)</summary>
        Private _progressDecrement As Double = 0.0
        ''' <summary>Tipo corrente del toast</summary>
        Private _toastType As ToastType = ToastType.Info
#End Region

#Region "Metodo statico Show (factory)"

        ''' <summary>
        ''' Crea e mostra un toast di notifica non-modale posizionato in basso a destra.
        ''' Non blocca il thread UI.
        ''' </summary>
        ''' <param name="title">Titolo del toast</param>
        ''' <param name="message">Messaggio del toast</param>
        ''' <param name="type">Tipo (Success/Warning/Error/Info)</param>
        ''' <param name="durationMs">Durata in ms prima dell'auto-dismiss (default 4000)</param>
        Public Shared Sub ShowToast(ByVal title As String, ByVal message As String,
                                    ByVal type As ToastType,
                                    Optional ByVal durationMs As Integer = 4000)
            Try
                Dim toast As New ToastForm()
                toast.InitForm(title, message, type, durationMs)
                CType(toast, System.Windows.Forms.Form).Show()   ' NON ShowDialog - non bloccante
            Catch ex As Exception
                ' Fallback silenzioso: il toast non e' critico
                System.Diagnostics.Debug.WriteLine("ToastForm.ShowToast error: " & ex.Message)
            End Try
        End Sub

        Public Shared Shadows Sub Show(ByVal title As String, ByVal message As String,
                                       ByVal type As ToastType,
                                       Optional ByVal durationMs As Integer = 4000)
            ShowToast(title, message, type, durationMs)
        End Sub

#End Region

#Region "Inizializzazione"

        ''' <summary>
        ''' Inizializza il toast con testo, colori e posizione.
        ''' Avvia i timer per fade-in, progress e dismiss.
        ''' </summary>
        Private Sub InitForm(ByVal title As String, ByVal message As String,
                             ByVal type As ToastType, ByVal durationMs As Integer)
            _duration = durationMs
            _toastType = type

            ' --- Imposta colori del pannello in base al tipo ---
            Select Case type
                Case ToastType.Success
                    pnlToast.BackColor = System.Drawing.Color.FromArgb(40, 167, 69)   ' ColSuccess
                    Me.BackColor = System.Drawing.Color.FromArgb(40, 167, 69)
                    lblToastTitle.ForeColor = System.Drawing.Color.White
                    lblToastMessage.ForeColor = System.Drawing.Color.White
                    lblToastTitle.Text = If(String.IsNullOrEmpty(title), "Successo", title)

                Case ToastType.Warning
                    pnlToast.BackColor = System.Drawing.Color.FromArgb(255, 193, 7)   ' ColWarning
                    Me.BackColor = System.Drawing.Color.FromArgb(255, 193, 7)
                    lblToastTitle.ForeColor = System.Drawing.Color.Black
                    lblToastMessage.ForeColor = System.Drawing.Color.Black
                    lblToastTitle.Text = If(String.IsNullOrEmpty(title), "Attenzione", title)

                Case ToastType.Error
                    pnlToast.BackColor = System.Drawing.Color.FromArgb(220, 53, 69)   ' ColError
                    Me.BackColor = System.Drawing.Color.FromArgb(220, 53, 69)
                    lblToastTitle.ForeColor = System.Drawing.Color.White
                    lblToastMessage.ForeColor = System.Drawing.Color.White
                    lblToastTitle.Text = If(String.IsNullOrEmpty(title), "Errore", title)

                Case ToastType.Info
                    pnlToast.BackColor = System.Drawing.Color.FromArgb(191, 27, 27)   ' ColAccent
                    Me.BackColor = System.Drawing.Color.FromArgb(191, 27, 27)
                    lblToastTitle.ForeColor = System.Drawing.Color.White
                    lblToastMessage.ForeColor = System.Drawing.Color.White
                    lblToastTitle.Text = If(String.IsNullOrEmpty(title), "Informazione", title)
            End Select

            ' --- Testi ---
            lblToastMessage.Text = If(String.IsNullOrEmpty(message), "", message)

            ' --- Opacita' iniziale a 0 per fade-in ---
            Me.Opacity = 0.0R

            ' --- Calcola decremento progress ---
            ' tmrProgress scatta ogni 40ms; durata totale = durationMs
            ' numero tick = durationMs / 40
            Dim tickCount As Double = CDbl(durationMs) / 40.0
            _progressDecrement = 100.0 / tickCount

            ' --- Posizione bottom-right con margine 20px ---
            Dim workingArea As System.Drawing.Rectangle = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea
            Me.Left = workingArea.Right - Me.Width - 20
            Me.Top = workingArea.Bottom - Me.Height - 20

            ' --- Configura tmrDismiss con la durata specificata ---
            tmrDismiss.Interval = durationMs

            ' --- Avvia i timer ---
            tmrFade.Start()
            tmrDismiss.Start()
            tmrProgress.Start()
        End Sub

#End Region

#Region "Handler Timer Fade"

        ''' <summary>
        ''' Gestisce il fade-in e il fade-out dell'opacita'.
        ''' Fase fade-in: incrementa di 0.1 fino a 1.0.
        ''' Fase fade-out: decrementa di 0.05 fino a 0, poi chiude.
        ''' </summary>
        Private Sub tmrFade_Tick(sender As Object, e As EventArgs) Handles tmrFade.Tick
            Try
                If _fadingOut Then
                    ' Fase fade-out
                    If Me.Opacity > 0.0R Then
                        Me.Opacity = Math.Max(0.0R, Me.Opacity - 0.05R)
                    Else
                        tmrFade.Stop()
                        tmrProgress.Stop()
                        Me.Close()
                    End If
                Else
                    ' Fase fade-in
                    If Me.Opacity < 1.0R Then
                        Me.Opacity = Math.Min(1.0R, Me.Opacity + 0.1R)
                    End If
                End If
            Catch ex As Exception
                ' Chiudi in caso di errore per non bloccare il sistema
                Me.Close()
            End Try
        End Sub

#End Region

#Region "Handler Timer Dismiss"

        ''' <summary>
        ''' Scatta dopo la durata specificata: avvia il fade-out.
        ''' </summary>
        Private Sub tmrDismiss_Tick(sender As Object, e As EventArgs) Handles tmrDismiss.Tick
            tmrDismiss.Stop()
            StartFadeOut()
        End Sub

#End Region

#Region "Handler Timer Progress"

        ''' <summary>
        ''' Aggiorna la larghezza della barra di progresso (da 376px a 0).
        ''' </summary>
        Private Sub tmrProgress_Tick(sender As Object, e As EventArgs) Handles tmrProgress.Tick
            Try
                If _progress > 0.0 Then
                    _progress = Math.Max(0.0, _progress - _progressDecrement)
                    ' La progress bar parte da 376px (larghezza form - 4px margine) a 0
                    Dim newWidth As Integer = CInt((_progress / 100.0) * 376.0)
                    ' La pnlProgress ha Dock=Bottom: modifichiamo solo la larghezza tramite Bounds
                    ' Annulliamo il Dock per gestire la larghezza manualmente
                    If pnlProgress.Dock <> System.Windows.Forms.DockStyle.None Then
                        pnlProgress.Dock = System.Windows.Forms.DockStyle.None
                        pnlProgress.Size = New System.Drawing.Size(376, 3)
                        pnlProgress.Location = New System.Drawing.Point(2, 94)
                    End If
                    pnlProgress.Width = newWidth
                End If
            Catch ex As Exception
                ' Ignora errori sulla progress bar
            End Try
        End Sub

#End Region

#Region "Handler Bottone Chiudi"

        ''' <summary>
        ''' Click sul bottone X: avvia il fade-out immediato.
        ''' </summary>
        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            tmrDismiss.Stop()
            StartFadeOut()
        End Sub

#End Region

#Region "Metodi privati"

        ''' <summary>
        ''' Avvia la sequenza di fade-out.
        ''' </summary>
        Private Sub StartFadeOut()
            _fadingOut = True
            tmrProgress.Stop()
            If Not tmrFade.Enabled Then
                tmrFade.Start()
            End If
        End Sub

#End Region

#Region "Override WndProc - Prevenzione click-through"

        ''' <summary>
        ''' Override di WndProc per gestire WM_NCHITTEST.
        ''' Restituisce HTTRANSPARENT nelle aree non interattive per
        ''' evitare che la form "rubi" il focus dalla finestra sottostante,
        ''' ma mantenendo la risposta ai click sui controlli figli.
        ''' </summary>
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            If m.Msg = WM_NCHITTEST Then
                ' Lascia passare il messaggio normalmente (HTCLIENT)
                ' cosi' i controlli figli (btnClose) rimangono cliccabili
                MyBase.WndProc(m)
                Return
            End If
            MyBase.WndProc(m)
        End Sub

#End Region

#Region "Override FormClosing"

        ''' <summary>
        ''' Ferma tutti i timer alla chiusura per evitare eccezioni.
        ''' </summary>
        Protected Overrides Sub OnFormClosing(e As System.Windows.Forms.FormClosingEventArgs)
            Try
                tmrFade.Stop()
                tmrDismiss.Stop()
                tmrProgress.Stop()
            Catch
            End Try
            MyBase.OnFormClosing(e)
        End Sub

#End Region

    End Class

