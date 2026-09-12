' ============================================================
' File: ToastForm.Designer.vb
' Progetto: Node-RED Desktop
' Descrizione: Designer per form toast di notifica (auto-dismiss)
' Autore: NodeRedDesktop
' ============================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ToastForm
    Inherits System.Windows.Forms.Form

    ' ----------------------------------------------------------------
    ' Dispose: rilascia le risorse gestite e non gestite
    ' ----------------------------------------------------------------
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

    ' Variabile richiesta dal Windows Form Designer
    Private components As System.ComponentModel.IContainer

    ' ----------------------------------------------------------------
    ' InitializeComponent: generato automaticamente dal Designer
    ' NON MODIFICARE MANUALMENTE
    ' ----------------------------------------------------------------
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pnlToast = New System.Windows.Forms.Panel()
        Me.picToastIcon = New System.Windows.Forms.PictureBox()
        Me.lblToastTitle = New System.Windows.Forms.Label()
        Me.lblToastMessage = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlProgress = New System.Windows.Forms.Panel()
        Me.tmrDismiss = New System.Windows.Forms.Timer(Me.components)
        Me.tmrFade = New System.Windows.Forms.Timer(Me.components)
        Me.tmrProgress = New System.Windows.Forms.Timer(Me.components)
        Me.pnlToast.SuspendLayout()
        CType(Me.picToastIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        ' pnlToast
        '
        Me.pnlToast.Controls.Add(Me.picToastIcon)
        Me.pnlToast.Controls.Add(Me.lblToastTitle)
        Me.pnlToast.Controls.Add(Me.lblToastMessage)
        Me.pnlToast.Controls.Add(Me.btnClose)
        Me.pnlToast.Controls.Add(Me.pnlProgress)
        Me.pnlToast.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlToast.Location = New System.Drawing.Point(0, 0)
        Me.pnlToast.Name = "pnlToast"
        Me.pnlToast.Size = New System.Drawing.Size(380, 100)
        Me.pnlToast.TabIndex = 0
        '
        ' picToastIcon
        '
        Me.picToastIcon.Location = New System.Drawing.Point(15, 12)
        Me.picToastIcon.Name = "picToastIcon"
        Me.picToastIcon.Size = New System.Drawing.Size(16, 16)
        Me.picToastIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picToastIcon.TabIndex = 0
        Me.picToastIcon.TabStop = False
        '
        ' lblToastTitle
        '
        Me.lblToastTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblToastTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblToastTitle.ForeColor = System.Drawing.Color.White
        Me.lblToastTitle.Location = New System.Drawing.Point(40, 10)
        Me.lblToastTitle.Name = "lblToastTitle"
        Me.lblToastTitle.Size = New System.Drawing.Size(280, 20)
        Me.lblToastTitle.TabIndex = 1
        Me.lblToastTitle.Text = "Titolo"
        '
        ' lblToastMessage
        '
        Me.lblToastMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblToastMessage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblToastMessage.ForeColor = System.Drawing.Color.White
        Me.lblToastMessage.Location = New System.Drawing.Point(40, 30)
        Me.lblToastMessage.Name = "lblToastMessage"
        Me.lblToastMessage.Size = New System.Drawing.Size(280, 45)
        Me.lblToastMessage.TabIndex = 2
        Me.lblToastMessage.Text = "Messaggio"
        '
        ' btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(350, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(22, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = ChrW(10005)
        Me.btnClose.UseVisualStyleBackColor = False
        '
        ' pnlProgress
        '
        Me.pnlProgress.BackColor = System.Drawing.Color.FromArgb(120, 255, 255, 255)
        Me.pnlProgress.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlProgress.Location = New System.Drawing.Point(0, 97)
        Me.pnlProgress.Name = "pnlProgress"
        Me.pnlProgress.Size = New System.Drawing.Size(380, 3)
        Me.pnlProgress.TabIndex = 4
        '
        ' tmrDismiss - 4 secondi per auto-dismiss
        '
        Me.tmrDismiss.Interval = 4000
        '
        ' tmrFade - 30ms per animazione fade
        '
        Me.tmrFade.Interval = 30
        '
        ' tmrProgress - 40ms per aggiornamento progress bar
        '
        Me.tmrProgress.Interval = 40
        '
        ' ToastForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(40, 167, 69)
        Me.ClientSize = New System.Drawing.Size(380, 100)
        Me.Controls.Add(Me.pnlToast)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ToastForm"
        Me.Opacity = 0.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ToastForm"
        Me.TopMost = True
        Me.pnlToast.ResumeLayout(False)
        CType(Me.picToastIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    ' ----------------------------------------------------------------
    ' Dichiarazioni Friend WithEvents di tutti i controlli
    ' ----------------------------------------------------------------
    Friend WithEvents pnlToast As System.Windows.Forms.Panel
    Friend WithEvents picToastIcon As System.Windows.Forms.PictureBox
    Friend WithEvents lblToastTitle As System.Windows.Forms.Label
    Friend WithEvents lblToastMessage As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents pnlProgress As System.Windows.Forms.Panel
    Friend WithEvents tmrDismiss As System.Windows.Forms.Timer
    Friend WithEvents tmrFade As System.Windows.Forms.Timer
    Friend WithEvents tmrProgress As System.Windows.Forms.Timer

End Class
