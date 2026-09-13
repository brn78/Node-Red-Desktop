Imports System.Drawing
Imports System.Windows.Forms

' ============================================================
' UIHelper.vb - Helper grafico standard Windows Forms per Node-RED Desktop
' Colori standard di sistema (SystemColors.Control) e altezza pulsanti 23
' ============================================================

Namespace Helpers

    ''' <summary>
    ''' Modulo di supporto per il tema grafico dell'applicazione basato sui colori standard di Windows (SystemColors.Control).
    ''' </summary>
    Public Module UIHelper

#Region "Costanti Colore Standard Windows (Control)"

        ' --- Sfondi di sistema ---
        ''' <summary>Sfondo principale standard di Windows (Control).</summary>
        Public ReadOnly ColBackground As Color = SystemColors.Control

        ''' <summary>Sfondo superficie standard (Control).</summary>
        Public ReadOnly ColSurface As Color = SystemColors.Control

        ''' <summary>Sfondo superficie al passaggio del mouse.</summary>
        Public ReadOnly ColSurfaceHover As Color = SystemColors.ControlLight

        ' --- Accento / Selezione ---
        ''' <summary>Colore accento principale (Highlight di sistema).</summary>
        Public ReadOnly ColAccent As Color = SystemColors.Highlight

        ''' <summary>Colore accento al passaggio del mouse.</summary>
        Public ReadOnly ColAccentHover As Color = SystemColors.Highlight

        ''' <summary>Colore accento chiaro.</summary>
        Public ReadOnly ColAccentLight As Color = SystemColors.Highlight

        ' --- Testi di sistema ---
        ''' <summary>Testo principale standard (ControlText - Nero).</summary>
        Public ReadOnly ColText As Color = SystemColors.ControlText

        ''' <summary>Testo secondario/disattivato (GrayText).</summary>
        Public ReadOnly ColTextMuted As Color = SystemColors.GrayText

        ' --- Stato (indicatori semantici leggibili su sfondo chiaro) ---
        ''' <summary>Colore di successo (verde scuro leggibile).</summary>
        Public ReadOnly ColSuccess As Color = Color.FromArgb(0, 128, 0)

        ''' <summary>Colore avviso (arancio scuro leggibile).</summary>
        Public ReadOnly ColWarning As Color = Color.FromArgb(180, 110, 0)

        ''' <summary>Colore errore (rosso scuro leggibile).</summary>
        Public ReadOnly ColError As Color = Color.FromArgb(192, 0, 0)

        ' --- Struttura ---
        ''' <summary>Colore bordi e separatori.</summary>
        Public ReadOnly ColBorder As Color = SystemColors.ControlDark

        ''' <summary>Sfondo intestazioni e barre (Control).</summary>
        Public ReadOnly ColHeaderBg As Color = SystemColors.Control

        ' --- Alias semantici ---
        Public ReadOnly ColGreen As Color = Color.FromArgb(0, 128, 0)
        Public ReadOnly ColYellow As Color = Color.FromArgb(180, 110, 0)
        Public ReadOnly ColRed As Color = Color.FromArgb(192, 0, 0)
        Public ReadOnly ColGrey As Color = SystemColors.GrayText
        Public ReadOnly ColBlue As Color = Color.FromArgb(0, 102, 204)

        ' --- Colori Suite & AI ---
        Public ReadOnly ColTelegram As Color = Color.FromArgb(0, 136, 204)
        Public ReadOnly ColAI As Color = Color.FromArgb(75, 80, 180)
        Public ReadOnly ColAIHover As Color = Color.FromArgb(90, 95, 200)
        Public ReadOnly ColSuite As Color = Color.FromArgb(0, 136, 204)

#End Region

#Region "Font"

        Public ReadOnly FontDefault As New Font("Segoe UI", 9.0F)
        Public ReadOnly FontBold As New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Public ReadOnly FontLarge As New Font("Segoe UI", 12.0F, FontStyle.Bold)
        Public ReadOnly FontTitle As New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Public ReadOnly FontSmall As New Font("Segoe UI", 8.25F)
        Public ReadOnly FontMono As New Font("Consolas", 8.5F)

#End Region

#Region "Tema e Stili (Disabilitati per gestione esclusiva tramite Visual Studio Designer)"

        ''' <summary>
        ''' Metodo safe no-op: il layout e l'aspetto grafico statico dei controlli sono gestiti interamente dal Designer.
        ''' </summary>
        Public Sub ApplyStandardTheme(ctrl As Control)
            If ctrl Is Nothing Then Return
        End Sub

        ''' <summary>
        ''' Metodo safe no-op: il layout e l'aspetto grafico statico dei controlli sono gestiti interamente dal Designer.
        ''' </summary>
        Public Sub ApplyDarkTheme(ctrl As Control)
            If ctrl Is Nothing Then Return
        End Sub

        Public Sub StyleButton(btn As Button, Optional accent As Boolean = True)
            If btn Is Nothing Then Return
        End Sub

        Public Sub StylePrimaryButton(btn As Button)
            If btn Is Nothing Then Return
        End Sub

        Public Sub StyleSecondaryButton(btn As Button)
            If btn Is Nothing Then Return
        End Sub

        Public Sub StyleDangerButton(btn As Button)
            If btn Is Nothing Then Return
        End Sub

        Public Sub StyleSuccessButton(btn As Button)
            If btn Is Nothing Then Return
        End Sub

        Public Sub StyleSuiteButton(btn As Button)
            If btn Is Nothing Then Return
        End Sub

        Public Sub StyleAIButton(btn As Button)
            If btn Is Nothing Then Return
        End Sub

#End Region

#Region "Funzioni di Stato Dinamiche e Formattazione"

        Public Function GetStatusColor(isRunning As Boolean) As Color
            Return If(isRunning, ColSuccess, ColError)
        End Function

        Public Function GetStatusText(isRunning As Boolean) As String
            Return If(isRunning, ChrW(&H25CF) & " IN ESECUZIONE", ChrW(&H25CF) & " FERMO")
        End Function

        Public Function FormatUptime(ts As TimeSpan) As String
            If ts.TotalSeconds < 1 Then Return "0s"
            Dim parts As New List(Of String)()
            If ts.Days > 0 Then parts.Add($"{ts.Days}g")
            If ts.Hours > 0 Then parts.Add($"{ts.Hours}h")
            If ts.Minutes > 0 Then parts.Add($"{ts.Minutes}m")
            If ts.Seconds > 0 OrElse parts.Count = 0 Then parts.Add($"{ts.Seconds}s")
            Return String.Join(" ", parts)
        End Function

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