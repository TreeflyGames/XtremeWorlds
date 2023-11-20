<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditor_Projectile
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DarkGroupBox1 = New DarkUI.Controls.DarkGroupBox()
        Me.lstIndex = New System.Windows.Forms.ListBox()
        Me.DarkGroupBox2 = New DarkUI.Controls.DarkGroupBox()
        Me.DarkLabel5 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel4 = New DarkUI.Controls.DarkLabel()
        Me.nudDamage = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudSpeed = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel3 = New DarkUI.Controls.DarkLabel()
        Me.nudRange = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudPic = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel2 = New DarkUI.Controls.DarkLabel()
        Me.picProjectile = New System.Windows.Forms.PictureBox()
        Me.txtName = New DarkUI.Controls.DarkTextBox()
        Me.DarkLabel1 = New DarkUI.Controls.DarkLabel()
        Me.btnSave = New DarkUI.Controls.DarkButton()
        Me.btnCancel = New DarkUI.Controls.DarkButton()
        Me.btnDelete = New DarkUI.Controls.DarkButton()
        Me.DarkGroupBox1.SuspendLayout
        Me.DarkGroupBox2.SuspendLayout
        CType(Me.nudDamage,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudSpeed,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudRange,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudPic,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picProjectile,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'DarkGroupBox1
        '
        Me.DarkGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.DarkGroupBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer))
        Me.DarkGroupBox1.Controls.Add(Me.lstIndex)
        Me.DarkGroupBox1.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox1.Location = New System.Drawing.Point(4, 3)
        Me.DarkGroupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox1.Name = "DarkGroupBox1"
        Me.DarkGroupBox1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox1.Size = New System.Drawing.Size(219, 216)
        Me.DarkGroupBox1.TabIndex = 0
        Me.DarkGroupBox1.TabStop = false
        Me.DarkGroupBox1.Text = "Projectile List"
        '
        'lstIndex
        '
        Me.lstIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.lstIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstIndex.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstIndex.FormattingEnabled = true
        Me.lstIndex.ItemHeight = 15
        Me.lstIndex.Location = New System.Drawing.Point(7, 20)
        Me.lstIndex.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstIndex.Name = "lstIndex"
        Me.lstIndex.Size = New System.Drawing.Size(205, 182)
        Me.lstIndex.TabIndex = 1
        '
        'DarkGroupBox2
        '
        Me.DarkGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.DarkGroupBox2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer))
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel5)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel4)
        Me.DarkGroupBox2.Controls.Add(Me.nudDamage)
        Me.DarkGroupBox2.Controls.Add(Me.nudSpeed)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel3)
        Me.DarkGroupBox2.Controls.Add(Me.nudRange)
        Me.DarkGroupBox2.Controls.Add(Me.nudPic)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel2)
        Me.DarkGroupBox2.Controls.Add(Me.picProjectile)
        Me.DarkGroupBox2.Controls.Add(Me.txtName)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel1)
        Me.DarkGroupBox2.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox2.Location = New System.Drawing.Point(230, 3)
        Me.DarkGroupBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox2.Name = "DarkGroupBox2"
        Me.DarkGroupBox2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox2.Size = New System.Drawing.Size(290, 315)
        Me.DarkGroupBox2.TabIndex = 1
        Me.DarkGroupBox2.TabStop = false
        Me.DarkGroupBox2.Text = "Projectile Properties"
        '
        'DarkLabel5
        '
        Me.DarkLabel5.AutoSize = true
        Me.DarkLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.DarkLabel5.Location = New System.Drawing.Point(7, 225)
        Me.DarkLabel5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel5.Name = "DarkLabel5"
        Me.DarkLabel5.Size = New System.Drawing.Size(112, 15)
        Me.DarkLabel5.TabIndex = 11
        Me.DarkLabel5.Text = "Additional Damage:"
        '
        'DarkLabel4
        '
        Me.DarkLabel4.AutoSize = true
        Me.DarkLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.DarkLabel4.Location = New System.Drawing.Point(7, 195)
        Me.DarkLabel4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel4.Name = "DarkLabel4"
        Me.DarkLabel4.Size = New System.Drawing.Size(42, 15)
        Me.DarkLabel4.TabIndex = 10
        Me.DarkLabel4.Text = "Speed:"
        '
        'nudDamage
        '
        Me.nudDamage.Location = New System.Drawing.Point(139, 223)
        Me.nudDamage.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudDamage.Name = "nudDamage"
        Me.nudDamage.Size = New System.Drawing.Size(140, 23)
        Me.nudDamage.TabIndex = 9
        '
        'nudSpeed
        '
        Me.nudSpeed.Location = New System.Drawing.Point(139, 193)
        Me.nudSpeed.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudSpeed.Name = "nudSpeed"
        Me.nudSpeed.Size = New System.Drawing.Size(140, 23)
        Me.nudSpeed.TabIndex = 8
        '
        'DarkLabel3
        '
        Me.DarkLabel3.AutoSize = true
        Me.DarkLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.DarkLabel3.Location = New System.Drawing.Point(7, 165)
        Me.DarkLabel3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel3.Name = "DarkLabel3"
        Me.DarkLabel3.Size = New System.Drawing.Size(43, 15)
        Me.DarkLabel3.TabIndex = 7
        Me.DarkLabel3.Text = "Range:"
        '
        'nudRange
        '
        Me.nudRange.Location = New System.Drawing.Point(139, 163)
        Me.nudRange.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudRange.Name = "nudRange"
        Me.nudRange.Size = New System.Drawing.Size(140, 23)
        Me.nudRange.TabIndex = 6
        '
        'nudPic
        '
        Me.nudPic.Location = New System.Drawing.Point(139, 133)
        Me.nudPic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudPic.Name = "nudPic"
        Me.nudPic.Size = New System.Drawing.Size(140, 23)
        Me.nudPic.TabIndex = 5
        '
        'DarkLabel2
        '
        Me.DarkLabel2.AutoSize = true
        Me.DarkLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.DarkLabel2.Location = New System.Drawing.Point(7, 135)
        Me.DarkLabel2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel2.Name = "DarkLabel2"
        Me.DarkLabel2.Size = New System.Drawing.Size(47, 15)
        Me.DarkLabel2.TabIndex = 4
        Me.DarkLabel2.Text = "Picture:"
        '
        'picProjectile
        '
        Me.picProjectile.BackColor = System.Drawing.Color.Black
        Me.picProjectile.Location = New System.Drawing.Point(10, 52)
        Me.picProjectile.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picProjectile.Name = "picProjectile"
        Me.picProjectile.Size = New System.Drawing.Size(268, 74)
        Me.picProjectile.TabIndex = 3
        Me.picProjectile.TabStop = false
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(69,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(74,Byte),Integer))
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.txtName.Location = New System.Drawing.Point(112, 22)
        Me.txtName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(166, 23)
        Me.txtName.TabIndex = 1
        '
        'DarkLabel1
        '
        Me.DarkLabel1.AutoSize = true
        Me.DarkLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.DarkLabel1.Location = New System.Drawing.Point(7, 24)
        Me.DarkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel1.Name = "DarkLabel1"
        Me.DarkLabel1.Size = New System.Drawing.Size(42, 15)
        Me.DarkLabel1.TabIndex = 0
        Me.DarkLabel1.Text = "Name:"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(11, 225)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSave.Size = New System.Drawing.Size(205, 27)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(11, 291)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCancel.Size = New System.Drawing.Size(205, 27)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(11, 258)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Padding = New System.Windows.Forms.Padding(6)
        Me.btnDelete.Size = New System.Drawing.Size(205, 27)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'frmEditor_Projectile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 15!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = true
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(527, 323)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.DarkGroupBox2)
        Me.Controls.Add(Me.DarkGroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmEditor_Projectile"
        Me.Text = "Projectile Editor"
        Me.DarkGroupBox1.ResumeLayout(false)
        Me.DarkGroupBox2.ResumeLayout(false)
        Me.DarkGroupBox2.PerformLayout
        CType(Me.nudDamage,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudSpeed,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudRange,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudPic,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picProjectile,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents DarkGroupBox1 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents lstIndex As System.Windows.Forms.ListBox
    Friend WithEvents DarkGroupBox2 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents txtName As DarkUI.Controls.DarkTextBox
    Friend WithEvents DarkLabel1 As DarkUI.Controls.DarkLabel
    Friend WithEvents picProjectile As System.Windows.Forms.PictureBox
    Friend WithEvents nudRange As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudPic As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel2 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel3 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudDamage As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudSpeed As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel4 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel5 As DarkUI.Controls.DarkLabel
    Friend WithEvents btnSave As DarkUI.Controls.DarkButton
    Friend WithEvents btnCancel As DarkUI.Controls.DarkButton
    Friend WithEvents btnDelete As DarkUI.Controls.DarkButton
End Class
