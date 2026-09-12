Imports System.Drawing
Imports System.Windows.Forms

' ============================================================
' UIHelper.vb - Helper per il tema grafico dark Node-RED
' Costanti colori, font, utilita'' di stile
' ============================================================

Namespace Helpers

    ''' <summary>
    ''' Modulo di supporto per il tema grafico dell''applicazione.
    ''' Fornisce costanti colore, font predefiniti e metodi di utilita''
    ''' per applicare il dark theme stile Node-RED a tutti i controlli UI.
    ''' </summary>
    Public Module UIHelper

#Region "Costanti Colore"

        ' --- Sfondi ---
        ''' <summary>Sfondo principale dell''applicazione (scuro profondi).</summary>
        Public ReadOnly ColBackground As Color = Color.FromArgb(30, 30, 46)

        ''' <summary>Sfondo superficie (pannelli, card, controlli).</summary>
        Public ReadOnly ColSurface As Color = Color.FromArgb(49, 50, 68)

        ''' <summary>Sfondo superficie al passaggio del mouse.</summary>
        Public ReadOnly ColSurfaceHover As Color = Color.FromArgb(65, 67, 90)

        ' --- Accento Node-RED (rosso) ---
        ''' <summary>Colore accento principale Node-RED (rosso).</summary>
        Public ReadOnly ColAccent As Color = Color.FromArgb(191, 27, 27)

        ''' <summary>Colore accento al passaggio del mouse (rosso piu'' chiaro).</summary>
        Public ReadOnly ColAccentHover As Color = Color.FromArgb(215, 40, 40)

        ''' <summary>Colore accento chiaro per elementi secondari.</summary>
        Public ReadOnly ColAccentLight As Color = Color.FromArgb(220, 80, 80)

        ' --- Testi ---
        ''' <summary>Testo principale (quasi bianco).</summary>
        Public ReadOnly ColText As Color = Color.FromArgb(248, 248, 242)

        ''' <summary>Testo secondario/disattivato (grigio-viola chiaro).</summary>
        Public ReadOnly ColTextMuted As Color = Color.FromArgb(166, 173, 200)

        ' --- Stato ---
        ''' <summary>Colore di successo (verde).</summary>
        Public ReadOnly ColSuccess As Color = Color.FromArgb(40, 167, 69)

        ''' <summary>Colore avviso (giallo).</summary>
        Public ReadOnly ColWarning As Color = Color.FromArgb(255, 193, 7)

        ''' <summary>Colore errore (rosso acceso).</summary>
        Public ReadOnly ColError As Color = Color.FromArgb(220, 53, 69)

        ' --- Struttura ---
        ''' <summary>Colore bordi e separatori.</summary>
        Public ReadOnly ColBorder As Color = Color.FromArgb(88, 91, 112)

        ''' <summary>Sfondo intestazioni e barre (piu'' scuro della superficie).</summary>
        Public ReadOnly ColHeaderBg As Color = Color.FromArgb(17, 17, 27)

        ' --- Alias semantici ---
        ''' <summary>Verde (alias di ColSuccess).</summary>
        Public ReadOnly ColGreen As Color = Color.FromArgb(40, 167, 69)

        ''' <summary>Giallo (alias di ColWarning).</summary>
        Public ReadOnly ColYellow As Color = Color.FromArgb(255, 193, 7)

        ''' <summary>Rosso (alias di ColError).</summary>
        Public ReadOnly ColRed As Color = Color.FromArgb(220, 53, 69)

        ''' <summary>Grigio neutro per elementi disabilitati o secondari.</summary>
        Public ReadOnly ColGrey As Color = Color.FromArgb(108, 117, 125)

        ''' <summary>Blu per link e informazioni.</summary>
        Public ReadOnly ColBlue As Color = Color.FromArgb(13, 110, 253)

#End Region

#Region "Font"

        ''' <summary>Font predefinito per l''interfaccia utente.</summary>
        Public ReadOnly FontDefault As New Font("Segoe UI", 9.0F)

        ''' <summary>Font grassetto per etichette e titoli di sezione.</summary>
        Public ReadOnly FontBold As New Font("Segoe UI", 9.0F, FontStyle.Bold)

        ''' <summary>Font grande per intestazioni principali.</summary>
        Public ReadOnly FontLarge As New Font("Segoe UI", 14.0F, FontStyle.Bold)

        ''' <summary>Font per titoli di finestra e gruppi.</summary>
        Public ReadOnly FontTitle As New Font("Segoe UI", 11.0F, FontStyle.Bold)

        ''' <summary>Font piccolo per note, hint, etichette secondarie.</summary>
        Public ReadOnly FontSmall As New Font("Segoe UI", 8.0F)

        ''' <summary>Font monospazio per log, percorsi, codice.</summary>
        Public ReadOnly FontMono As New Font("Consolas", 9.0F)

#End Region

#Region "Tema Dark - Applicazione Ricorsiva"

        ''' <summary>
        ''' Applica ricorsivamente il dark theme Node-RED a un controllo e a tutti i suoi figli.
        ''' Gestisce tutti i principali tipi di controllo WinForms.
        ''' </summary>
        ''' <param name="ctrl">Controllo radice da cui partire.</param>
        Public Sub ApplyDarkTheme(ctrl As Control)
            If ctrl Is Nothing Then Return

            Try
                Select Case True

                    Case TypeOf ctrl Is Form
                        ctrl.BackColor = ColBackground
                        ctrl.ForeColor = ColText
                        ctrl.Font = FontDefault

                    Case TypeOf ctrl Is Button
                        Dim btn As Button = DirectCast(ctrl, Button)
                        btn.BackColor = ColAccent
                        btn.ForeColor = Color.White
                        btn.FlatStyle = FlatStyle.Flat
                        btn.FlatAppearance.BorderSize = 0
                        btn.FlatAppearance.MouseOverBackColor = ColAccentHover
                        btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(160, 20, 20)
                        btn.Font = FontBold
                        btn.Cursor = Cursors.Hand

                    Case TypeOf ctrl Is Label
                        ctrl.BackColor = Color.Transparent
                        ctrl.ForeColor = ColText
                        ctrl.Font = FontDefault

                    Case TypeOf ctrl Is RichTextBox
                        Dim rtb As RichTextBox = DirectCast(ctrl, RichTextBox)
                        rtb.BackColor = ColSurface
                        rtb.ForeColor = ColText
                        rtb.BorderStyle = BorderStyle.FixedSingle
                        rtb.Font = FontMono

                    Case TypeOf ctrl Is TextBox
                        Dim tb As TextBox = DirectCast(ctrl, TextBox)
                        tb.BackColor = ColSurface
                        tb.ForeColor = ColText
                        tb.BorderStyle = BorderStyle.FixedSingle
                        tb.Font = FontDefault

                    Case TypeOf ctrl Is ComboBox
                        Dim cmb As ComboBox = DirectCast(ctrl, ComboBox)
                        cmb.BackColor = ColSurface
                        cmb.ForeColor = ColText
                        cmb.FlatStyle = FlatStyle.Flat
                        cmb.Font = FontDefault

                    Case TypeOf ctrl Is CheckBox
                        ctrl.BackColor = Color.Transparent
                        ctrl.ForeColor = ColText
                        ctrl.Font = FontDefault

                    Case TypeOf ctrl Is RadioButton
                        ctrl.BackColor = Color.Transparent
                        ctrl.ForeColor = ColText
                        ctrl.Font = FontDefault

                    Case TypeOf ctrl Is NumericUpDown
                        Dim nud As NumericUpDown = DirectCast(ctrl, NumericUpDown)
                        nud.BackColor = ColSurface
                        nud.ForeColor = ColText
                        nud.Font = FontDefault

                    Case TypeOf ctrl Is GroupBox
                        ctrl.BackColor = Color.Transparent
                        ctrl.ForeColor = ColTextMuted
                        ctrl.Font = FontBold

                    Case TypeOf ctrl Is TabControl
                        ctrl.BackColor = ColBackground
                        ctrl.ForeColor = ColText

                    Case TypeOf ctrl Is TabPage
                        ctrl.BackColor = ColBackground
                        ctrl.ForeColor = ColText

                    Case TypeOf ctrl Is ListBox
                        Dim lb As ListBox = DirectCast(ctrl, ListBox)
                        lb.BackColor = ColSurface
                        lb.ForeColor = ColText
                        lb.Font = FontDefault

                    Case TypeOf ctrl Is ListView
                        Dim lv As ListView = DirectCast(ctrl, ListView)
                        lv.BackColor = ColSurface
                        lv.ForeColor = ColText
                        lv.Font = FontDefault

                    Case TypeOf ctrl Is DataGridView
                        Dim dgv As DataGridView = DirectCast(ctrl, DataGridView)
                        dgv.BackgroundColor = ColBackground
                        dgv.GridColor = ColBorder
                        dgv.BorderStyle = BorderStyle.None
                        dgv.DefaultCellStyle.BackColor = ColSurface
                        dgv.DefaultCellStyle.ForeColor = ColText
                        dgv.DefaultCellStyle.SelectionBackColor = ColAccent
                        dgv.DefaultCellStyle.SelectionForeColor = Color.White
                        dgv.ColumnHeadersDefaultCellStyle.BackColor = ColHeaderBg
                        dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColText
                        dgv.ColumnHeadersDefaultCellStyle.Font = FontBold
                        dgv.RowHeadersDefaultCellStyle.BackColor = ColHeaderBg
                        dgv.RowHeadersDefaultCellStyle.ForeColor = ColTextMuted
                        dgv.EnableHeadersVisualStyles = False
                        dgv.Font = FontDefault

                    Case TypeOf ctrl Is StatusStrip
                        Dim ss As StatusStrip = DirectCast(ctrl, StatusStrip)
                        ss.BackColor = ColHeaderBg
                        ss.ForeColor = ColText
                        ss.Font = FontDefault

                    Case TypeOf ctrl Is ToolStrip
                        Dim ts As ToolStrip = DirectCast(ctrl, ToolStrip)
                        ts.BackColor = ColHeaderBg
                        ts.ForeColor = ColText
                        ts.Font = FontDefault

                    Case TypeOf ctrl Is ProgressBar
                        Dim pb As ProgressBar = DirectCast(ctrl, ProgressBar)
                        pb.BackColor = ColSurface

                    Case TypeOf ctrl Is Panel
                        ' I pannelli usano la superficie, ma quelli con sfondo trasparente rimangono tali
                        If ctrl.BackColor <> Color.Transparent Then
                            ctrl.BackColor = ColSurface
                        End If
                        ctrl.ForeColor = ColText

                    Case TypeOf ctrl Is SplitContainer
                        ctrl.BackColor = ColBackground

                    Case TypeOf ctrl Is SplitterPanel
                        ctrl.BackColor = ColBackground

                    Case TypeOf ctrl Is TabControl
                        ' Lascia il TabControl nativo per evitare bug di disegno delle schede

                    Case TypeOf ctrl Is TabPage
                        ctrl.BackColor = ColBackground
                        ctrl.ForeColor = ColText

                    Case Else
                        ' Per tutti gli altri controlli: applica colori di base
                        Try
                            ctrl.BackColor = ColBackground
                            ctrl.ForeColor = ColText
                        Catch
                            ' Alcuni controlli non supportano BackColor/ForeColor
                        End Try

                End Select

                ' Applica ricorsivamente ai figli
                For Each child As Control In ctrl.Controls
                    ApplyDarkTheme(child)
                Next

            Catch ex As Exception
                ' Non bloccare l''applicazione per errori di tema
            End Try
        End Sub

#End Region

#Region "Stile Pulsanti"

        ''' <summary>
        ''' Applica uno stile professionale a un pulsante.
        ''' </summary>
        ''' <param name="btn">Pulsante da stilizzare.</param>
        ''' <param name="accent">True per stile accento rosso (default), False per stile grigio secondario.</param>
        Public Sub StyleButton(btn As Button, Optional accent As Boolean = True)
            If accent Then
                StylePrimaryButton(btn)
            Else
                StyleSecondaryButton(btn)
            End If
        End Sub

        ''' <summary>
        ''' Stile pulsante primario: rosso accento Node-RED.
        ''' Usare per azioni principali (Avvia, Salva, Conferma).
        ''' </summary>
        Public Sub StylePrimaryButton(btn As Button)
            btn.BackColor = ColAccent
            btn.ForeColor = Color.White
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = ColAccentHover
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 18, 18)
            btn.Font = FontBold
            btn.Cursor = Cursors.Hand
            btn.Padding = New Padding(8, 4, 8, 4)
        End Sub

        ''' <summary>
        ''' Stile pulsante secondario: grigio scuro.
        ''' Usare per azioni secondarie (Annulla, Indietro, Chiudi).
        ''' </summary>
        Public Sub StyleSecondaryButton(btn As Button)
            btn.BackColor = ColSurface
            btn.ForeColor = ColText
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 1
            btn.FlatAppearance.BorderColor = ColBorder
            btn.FlatAppearance.MouseOverBackColor = ColSurfaceHover
            btn.FlatAppearance.MouseDownBackColor = ColBackground
            btn.Font = FontDefault
            btn.Cursor = Cursors.Hand
            btn.Padding = New Padding(8, 4, 8, 4)
        End Sub

        ''' <summary>
        ''' Stile pulsante pericolo: rosso scuro.
        ''' Usare per azioni distruttive (Elimina, Forza Arresto, Reset).
        ''' </summary>
        Public Sub StyleDangerButton(btn As Button)
            btn.BackColor = Color.FromArgb(180, 30, 30)
            btn.ForeColor = Color.White
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = ColError
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(140, 20, 20)
            btn.Font = FontBold
            btn.Cursor = Cursors.Hand
            btn.Padding = New Padding(8, 4, 8, 4)
        End Sub

        ''' <summary>
        ''' Stile pulsante successo: verde.
        ''' Usare per azioni positive (Installa, Backup, Verifica).
        ''' </summary>
        Public Sub StyleSuccessButton(btn As Button)
            btn.BackColor = ColSuccess
            btn.ForeColor = Color.White
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 190, 85)
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 130, 50)
            btn.Font = FontBold
            btn.Cursor = Cursors.Hand
            btn.Padding = New Padding(8, 4, 8, 4)
        End Sub

#End Region

#Region "Utilita'"

        ''' <summary>
        ''' Restituisce il colore di stato di Node-RED.
        ''' </summary>
        ''' <param name="isRunning">True se Node-RED e'' in esecuzione.</param>
        ''' <returns>Verde se in esecuzione, rosso se fermo.</returns>
        Public Function GetStatusColor(isRunning As Boolean) As Color
            Return If(isRunning, ColSuccess, ColError)
        End Function

        ''' <summary>
        ''' Restituisce il testo di stato di Node-RED con indicatore visivo.
        ''' </summary>
        ''' <param name="isRunning">True se Node-RED e'' in esecuzione.</param>
        ''' <returns>Stringa di stato leggibile.</returns>
        Public Function GetStatusText(isRunning As Boolean) As String
            Return If(isRunning, "● IN ESECUZIONE", "● FERMO")
        End Function

        ''' <summary>
        ''' Formatta un TimeSpan in formato leggibile compatto.
        ''' Esempi: "2g 3h 45m 12s", "45m 12s", "5s"
        ''' </summary>
        ''' <param name="ts">TimeSpan da formattare.</param>
        ''' <returns>Stringa formattata dell''uptime.</returns>
        Public Function FormatUptime(ts As TimeSpan) As String
            If ts.TotalSeconds < 1 Then Return "0s"

            Dim parts As New List(Of String)()

            If ts.Days > 0 Then parts.Add($"{ts.Days}g")
            If ts.Hours > 0 Then parts.Add($"{ts.Hours}h")
            If ts.Minutes > 0 Then parts.Add($"{ts.Minutes}m")
            If ts.Seconds > 0 OrElse parts.Count = 0 Then parts.Add($"{ts.Seconds}s")

            Return String.Join(" ", parts)
        End Function

        ''' <summary>
        ''' Formatta un valore in bytes in una stringa leggibile (B, KB, MB, GB).
        ''' </summary>
        ''' <param name="bytes">Numero di bytes da formattare.</param>
        ''' <returns>Stringa formattata con unita'' appropriata.</returns>
        Public Function FormatBytes(bytes As Long) As String
            If bytes < 0 Then Return "N/D"
            If bytes < 1024L Then Return $"{bytes} B"
            If bytes < 1024L * 1024L Then Return $"{bytes / 1024.0:F1} KB"
            If bytes < 1024L * 1024L * 1024L Then Return $"{bytes / (1024.0 * 1024.0):F1} MB"
            Return $"{bytes / (1024.0 * 1024.0 * 1024.0):F2} GB"
        End Function

#End Region

    End Module

End Namespace