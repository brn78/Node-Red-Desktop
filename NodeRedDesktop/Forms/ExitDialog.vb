Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' Dialogo di conferma chiusura dell'applicazione che consente all'utente di:
''' - Arrestare tutti i servizi (Node-RED e Ollama)
''' - Lasciare tutti i servizi attivi in background
''' - Selezionare a scelta quale servizio arrestare e quale mantenere attivo
''' - Annullare l'uscita
''' </summary>
Public Class ExitDialog
    Inherits Global.System.Windows.Forms.Form

        Private _chkStopNode As CheckBox
        Private _chkStopOllama As CheckBox
        Private _btnStop As Button
        Private _btnLeave As Button
        Private _btnCancel As Button
        Private _lblTitle As Label
        Private _lblHint As Label

        ''' <summary>True se l'utente ha scelto di arrestare Node-RED.</summary>
        Public Property StopNodeRed As Boolean = False

        ''' <summary>True se l'utente ha scelto di arrestare Ollama.</summary>
        Public Property StopOllama As Boolean = False

        Public Sub New(isNodeRunning As Boolean, isOllamaRunning As Boolean)
            MyBase.New()
            InitializeUI(isNodeRunning, isOllamaRunning)
        End Sub

        Private Sub InitializeUI(isNodeRunning As Boolean, isOllamaRunning As Boolean)
            Me.Text = "Chiusura Node-RED Desktop"
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.BackColor = SystemColors.Control
            Me.ForeColor = SystemColors.ControlText
            Me.Font = New Font("Segoe UI", 9.0F)

            _lblTitle = New Label() With {
                .Text = "I seguenti servizi della Suite sono attualmente in esecuzione:",
                .Location = New Point(20, 16),
                .Size = New Size(400, 20),
                .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            }
            Me.Controls.Add(_lblTitle)

            Dim nextY As Integer = 44

            If isNodeRunning Then
                _chkStopNode = New CheckBox() With {
                    .Text = "Arresta Node-RED (Porta 1880)",
                    .Location = New Point(32, nextY),
                    .Size = New Size(370, 24),
                    .Checked = True,
                    .UseVisualStyleBackColor = True
                }
                AddHandler _chkStopNode.CheckedChanged, AddressOf OnCheckChanged
                Me.Controls.Add(_chkStopNode)
                nextY += 28
            End If

            If isOllamaRunning Then
                _chkStopOllama = New CheckBox() With {
                    .Text = "Arresta Ollama Local AI (Porta 11434)",
                    .Location = New Point(32, nextY),
                    .Size = New Size(370, 24),
                    .Checked = True,
                    .UseVisualStyleBackColor = True
                }
                AddHandler _chkStopOllama.CheckedChanged, AddressOf OnCheckChanged
                Me.Controls.Add(_chkStopOllama)
                nextY += 28
            End If

            _lblHint = New Label() With {
                .Text = "Scegli se arrestare i servizi selezionati o lasciarli attivi in background:",
                .Location = New Point(20, nextY + 6),
                .Size = New Size(400, 20),
                .ForeColor = SystemColors.GrayText,
                .Font = New Font("Segoe UI", 8.25F)
            }
            Me.Controls.Add(_lblHint)

            Dim btnY As Integer = nextY + 36
            Me.ClientSize = New Size(440, btnY + 44)

            _btnStop = New Button() With {
                .Text = "Arresta Tutto",
                .Location = New Point(20, btnY),
                .Size = New Size(125, 23),
                .UseVisualStyleBackColor = True
            }
            AddHandler _btnStop.Click, AddressOf BtnStop_Click
            Me.Controls.Add(_btnStop)

            _btnLeave = New Button() With {
                .Text = "Lascia in Background",
                .Location = New Point(152, btnY),
                .Size = New Size(155, 23),
                .UseVisualStyleBackColor = True
            }
            AddHandler _btnLeave.Click, AddressOf BtnLeave_Click
            Me.Controls.Add(_btnLeave)

            _btnCancel = New Button() With {
                .Text = "Annulla",
                .Location = New Point(315, btnY),
                .Size = New Size(100, 23),
                .UseVisualStyleBackColor = True,
                .DialogResult = DialogResult.Cancel
            }
            Me.Controls.Add(_btnCancel)

            Me.AcceptButton = _btnStop
            Me.CancelButton = _btnCancel

            UpdateButtonText(isNodeRunning, isOllamaRunning)
        End Sub

        Private Sub OnCheckChanged(sender As Object, e As EventArgs)
            Dim nodeChecked = (_chkStopNode IsNot Nothing AndAlso _chkStopNode.Checked)
            Dim ollamaChecked = (_chkStopOllama IsNot Nothing AndAlso _chkStopOllama.Checked)
            Dim anyChecked = nodeChecked OrElse ollamaChecked
            Dim allChecked = True
            If _chkStopNode IsNot Nothing AndAlso Not _chkStopNode.Checked Then allChecked = False
            If _chkStopOllama IsNot Nothing AndAlso Not _chkStopOllama.Checked Then allChecked = False

            If allChecked Then
                _btnStop.Text = "Arresta Tutto"
                _btnStop.Enabled = True
            ElseIf anyChecked Then
                _btnStop.Text = "Arresta Selezionati"
                _btnStop.Enabled = True
            Else
                _btnStop.Text = "Nessun Arresto"
                _btnStop.Enabled = False
            End If
        End Sub

        Private Sub UpdateButtonText(isNodeRunning As Boolean, isOllamaRunning As Boolean)
            If isNodeRunning AndAlso isOllamaRunning Then
                _btnStop.Text = "Arresta Tutto"
            ElseIf isNodeRunning Then
                _btnStop.Text = "Arresta Node-RED"
            ElseIf isOllamaRunning Then
                _btnStop.Text = "Arresta Ollama"
            End If
        End Sub

        Private Sub BtnStop_Click(sender As Object, e As EventArgs)
            StopNodeRed = (_chkStopNode IsNot Nothing AndAlso _chkStopNode.Checked)
            StopOllama = (_chkStopOllama IsNot Nothing AndAlso _chkStopOllama.Checked)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub BtnLeave_Click(sender As Object, e As EventArgs)
            StopNodeRed = False
            StopOllama = False
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

    End Class
