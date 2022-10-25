<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmGame
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGame))
        Me.picscreen = New System.Windows.Forms.PictureBox()
        Me.pnlCurrency = New System.Windows.Forms.Panel()
        Me.lblCurrencyCancel = New System.Windows.Forms.Label()
        Me.lblCurrencyOk = New System.Windows.Forms.Label()
        Me.txtCurrency = New System.Windows.Forms.TextBox()
        Me.lblCurrency = New System.Windows.Forms.Label()
        CType(Me.picscreen,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlCurrency.SuspendLayout
        Me.SuspendLayout
        '
        'picscreen
        '
        Me.picscreen.Location = New System.Drawing.Point(0, 0)
        Me.picscreen.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picscreen.Name = "picscreen"
        Me.picscreen.Size = New System.Drawing.Size(735, 508)
        Me.picscreen.TabIndex = 4
        Me.picscreen.TabStop = false
        '
        'pnlCurrency
        '
        Me.pnlCurrency.BackColor = System.Drawing.Color.DimGray
        Me.pnlCurrency.Controls.Add(Me.lblCurrencyCancel)
        Me.pnlCurrency.Controls.Add(Me.lblCurrencyOk)
        Me.pnlCurrency.Controls.Add(Me.txtCurrency)
        Me.pnlCurrency.Controls.Add(Me.lblCurrency)
        Me.pnlCurrency.Location = New System.Drawing.Point(152, 271)
        Me.pnlCurrency.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlCurrency.Name = "pnlCurrency"
        Me.pnlCurrency.Size = New System.Drawing.Size(410, 113)
        Me.pnlCurrency.TabIndex = 16
        Me.pnlCurrency.Visible = false
        '
        'lblCurrencyCancel
        '
        Me.lblCurrencyCancel.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrencyCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblCurrencyCancel.ForeColor = System.Drawing.Color.White
        Me.lblCurrencyCancel.Location = New System.Drawing.Point(250, 82)
        Me.lblCurrencyCancel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCurrencyCancel.Name = "lblCurrencyCancel"
        Me.lblCurrencyCancel.Size = New System.Drawing.Size(126, 18)
        Me.lblCurrencyCancel.TabIndex = 4
        Me.lblCurrencyCancel.Text = "Cancel"
        Me.lblCurrencyCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCurrencyOk
        '
        Me.lblCurrencyOk.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrencyOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblCurrencyOk.ForeColor = System.Drawing.Color.White
        Me.lblCurrencyOk.Location = New System.Drawing.Point(15, 82)
        Me.lblCurrencyOk.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCurrencyOk.Name = "lblCurrencyOk"
        Me.lblCurrencyOk.Size = New System.Drawing.Size(119, 18)
        Me.lblCurrencyOk.TabIndex = 3
        Me.lblCurrencyOk.Text = "Okay"
        Me.lblCurrencyOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCurrency
        '
        Me.txtCurrency.Location = New System.Drawing.Point(98, 40)
        Me.txtCurrency.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtCurrency.Name = "txtCurrency"
        Me.txtCurrency.Size = New System.Drawing.Size(209, 23)
        Me.txtCurrency.TabIndex = 2
        '
        'lblCurrency
        '
        Me.lblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrency.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblCurrency.ForeColor = System.Drawing.Color.White
        Me.lblCurrency.Location = New System.Drawing.Point(4, 0)
        Me.lblCurrency.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(402, 28)
        Me.lblCurrency.TabIndex = 1
        Me.lblCurrency.Text = "How many do you want to drop?"
        Me.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmGame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 15!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(741, 516)
        Me.Controls.Add(Me.pnlCurrency)
        Me.Controls.Add(Me.picscreen)
        Me.DoubleBuffered = true
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = false
        Me.Name = "FrmGame"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMainGame"
        CType(Me.picscreen,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlCurrency.ResumeLayout(false)
        Me.pnlCurrency.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents picscreen As System.Windows.Forms.PictureBox
    Friend WithEvents pnlCurrency As System.Windows.Forms.Panel
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents lblCurrencyCancel As System.Windows.Forms.Label
    Friend WithEvents lblCurrencyOk As System.Windows.Forms.Label
    Friend WithEvents txtCurrency As System.Windows.Forms.TextBox
End Class
