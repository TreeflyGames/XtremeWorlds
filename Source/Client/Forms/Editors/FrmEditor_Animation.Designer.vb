<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEditor_Animation
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
        Me.cmbSound = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel2 = New DarkUI.Controls.DarkLabel()
        Me.DarkGroupBox4 = New DarkUI.Controls.DarkGroupBox()
        Me.nudLoopTime1 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudFrameCount1 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudLoopCount1 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudSprite1 = New DarkUI.Controls.DarkNumericUpDown()
        Me.lblLoopTime1 = New DarkUI.Controls.DarkLabel()
        Me.lblFrameCount1 = New DarkUI.Controls.DarkLabel()
        Me.lblLoopCount1 = New DarkUI.Controls.DarkLabel()
        Me.lblSprite1 = New DarkUI.Controls.DarkLabel()
        Me.picSprite1 = New System.Windows.Forms.PictureBox()
        Me.DarkGroupBox3 = New DarkUI.Controls.DarkGroupBox()
        Me.nudLoopTime0 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudFrameCount0 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudLoopCount0 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudSprite0 = New DarkUI.Controls.DarkNumericUpDown()
        Me.lblLoopTime0 = New DarkUI.Controls.DarkLabel()
        Me.lblFrameCount0 = New DarkUI.Controls.DarkLabel()
        Me.lblLoopCount0 = New DarkUI.Controls.DarkLabel()
        Me.lblSprite0 = New DarkUI.Controls.DarkLabel()
        Me.picSprite0 = New System.Windows.Forms.PictureBox()
        Me.txtName = New DarkUI.Controls.DarkTextBox()
        Me.DarkLabel1 = New DarkUI.Controls.DarkLabel()
        Me.btnSave = New DarkUI.Controls.DarkButton()
        Me.btnDelete = New DarkUI.Controls.DarkButton()
        Me.btnCancel = New DarkUI.Controls.DarkButton()
        Me.DarkGroupBox1.SuspendLayout
        Me.DarkGroupBox2.SuspendLayout
        Me.DarkGroupBox4.SuspendLayout
        CType(Me.nudLoopTime1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudFrameCount1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudLoopCount1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudSprite1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picSprite1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.DarkGroupBox3.SuspendLayout
        CType(Me.nudLoopTime0,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudFrameCount0,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudLoopCount0,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudSprite0,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picSprite0,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'DarkGroupBox1
        '
        Me.DarkGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.DarkGroupBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer))
        Me.DarkGroupBox1.Controls.Add(Me.lstIndex)
        Me.DarkGroupBox1.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox1.Location = New System.Drawing.Point(2, 3)
        Me.DarkGroupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox1.Name = "DarkGroupBox1"
        Me.DarkGroupBox1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox1.Size = New System.Drawing.Size(233, 429)
        Me.DarkGroupBox1.TabIndex = 0
        Me.DarkGroupBox1.TabStop = false
        Me.DarkGroupBox1.Text = "Animations List"
        '
        'lstIndex
        '
        Me.lstIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.lstIndex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstIndex.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstIndex.FormattingEnabled = true
        Me.lstIndex.ItemHeight = 15
        Me.lstIndex.Location = New System.Drawing.Point(7, 22)
        Me.lstIndex.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstIndex.Name = "lstIndex"
        Me.lstIndex.Size = New System.Drawing.Size(219, 390)
        Me.lstIndex.TabIndex = 0
        '
        'DarkGroupBox2
        '
        Me.DarkGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.DarkGroupBox2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer))
        Me.DarkGroupBox2.Controls.Add(Me.cmbSound)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel2)
        Me.DarkGroupBox2.Controls.Add(Me.DarkGroupBox4)
        Me.DarkGroupBox2.Controls.Add(Me.DarkGroupBox3)
        Me.DarkGroupBox2.Controls.Add(Me.txtName)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel1)
        Me.DarkGroupBox2.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox2.Location = New System.Drawing.Point(243, 3)
        Me.DarkGroupBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox2.Name = "DarkGroupBox2"
        Me.DarkGroupBox2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox2.Size = New System.Drawing.Size(511, 535)
        Me.DarkGroupBox2.TabIndex = 1
        Me.DarkGroupBox2.TabStop = false
        Me.DarkGroupBox2.Text = "Animation Properties"
        '
        'cmbSound
        '
        Me.cmbSound.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cmbSound.FormattingEnabled = true
        Me.cmbSound.Location = New System.Drawing.Point(136, 62)
        Me.cmbSound.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbSound.Name = "cmbSound"
        Me.cmbSound.Size = New System.Drawing.Size(181, 24)
        Me.cmbSound.TabIndex = 25
        '
        'DarkLabel2
        '
        Me.DarkLabel2.AutoSize = true
        Me.DarkLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.DarkLabel2.Location = New System.Drawing.Point(19, 66)
        Me.DarkLabel2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel2.Name = "DarkLabel2"
        Me.DarkLabel2.Size = New System.Drawing.Size(44, 15)
        Me.DarkLabel2.TabIndex = 24
        Me.DarkLabel2.Text = "Sound:"
        '
        'DarkGroupBox4
        '
        Me.DarkGroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.DarkGroupBox4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer))
        Me.DarkGroupBox4.Controls.Add(Me.nudLoopTime1)
        Me.DarkGroupBox4.Controls.Add(Me.nudFrameCount1)
        Me.DarkGroupBox4.Controls.Add(Me.nudLoopCount1)
        Me.DarkGroupBox4.Controls.Add(Me.nudSprite1)
        Me.DarkGroupBox4.Controls.Add(Me.lblLoopTime1)
        Me.DarkGroupBox4.Controls.Add(Me.lblFrameCount1)
        Me.DarkGroupBox4.Controls.Add(Me.lblLoopCount1)
        Me.DarkGroupBox4.Controls.Add(Me.lblSprite1)
        Me.DarkGroupBox4.Controls.Add(Me.picSprite1)
        Me.DarkGroupBox4.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox4.Location = New System.Drawing.Point(7, 321)
        Me.DarkGroupBox4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox4.Name = "DarkGroupBox4"
        Me.DarkGroupBox4.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox4.Size = New System.Drawing.Size(497, 208)
        Me.DarkGroupBox4.TabIndex = 23
        Me.DarkGroupBox4.TabStop = false
        Me.DarkGroupBox4.Text = "Layer 1 - Above Player"
        '
        'nudLoopTime1
        '
        Me.nudLoopTime1.Location = New System.Drawing.Point(102, 159)
        Me.nudLoopTime1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudLoopTime1.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudLoopTime1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudLoopTime1.Name = "nudLoopTime1"
        Me.nudLoopTime1.Size = New System.Drawing.Size(140, 23)
        Me.nudLoopTime1.TabIndex = 33
        Me.nudLoopTime1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudFrameCount1
        '
        Me.nudFrameCount1.Location = New System.Drawing.Point(102, 114)
        Me.nudFrameCount1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudFrameCount1.Name = "nudFrameCount1"
        Me.nudFrameCount1.Size = New System.Drawing.Size(140, 23)
        Me.nudFrameCount1.TabIndex = 32
        '
        'nudLoopCount1
        '
        Me.nudLoopCount1.Location = New System.Drawing.Point(102, 70)
        Me.nudLoopCount1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudLoopCount1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudLoopCount1.Name = "nudLoopCount1"
        Me.nudLoopCount1.Size = New System.Drawing.Size(140, 23)
        Me.nudLoopCount1.TabIndex = 31
        Me.nudLoopCount1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudSprite1
        '
        Me.nudSprite1.Location = New System.Drawing.Point(102, 28)
        Me.nudSprite1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudSprite1.Name = "nudSprite1"
        Me.nudSprite1.Size = New System.Drawing.Size(140, 23)
        Me.nudSprite1.TabIndex = 30
        '
        'lblLoopTime1
        '
        Me.lblLoopTime1.AutoSize = true
        Me.lblLoopTime1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.lblLoopTime1.Location = New System.Drawing.Point(12, 162)
        Me.lblLoopTime1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLoopTime1.Name = "lblLoopTime1"
        Me.lblLoopTime1.Size = New System.Drawing.Size(66, 15)
        Me.lblLoopTime1.TabIndex = 28
        Me.lblLoopTime1.Text = "Loop Time:"
        '
        'lblFrameCount1
        '
        Me.lblFrameCount1.AutoSize = true
        Me.lblFrameCount1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.lblFrameCount1.Location = New System.Drawing.Point(13, 117)
        Me.lblFrameCount1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFrameCount1.Name = "lblFrameCount1"
        Me.lblFrameCount1.Size = New System.Drawing.Size(79, 15)
        Me.lblFrameCount1.TabIndex = 26
        Me.lblFrameCount1.Text = "Frame Count:"
        '
        'lblLoopCount1
        '
        Me.lblLoopCount1.AutoSize = true
        Me.lblLoopCount1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.lblLoopCount1.Location = New System.Drawing.Point(13, 73)
        Me.lblLoopCount1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLoopCount1.Name = "lblLoopCount1"
        Me.lblLoopCount1.Size = New System.Drawing.Size(73, 15)
        Me.lblLoopCount1.TabIndex = 24
        Me.lblLoopCount1.Text = "Loop Count:"
        '
        'lblSprite1
        '
        Me.lblSprite1.AutoSize = true
        Me.lblSprite1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.lblSprite1.Location = New System.Drawing.Point(12, 30)
        Me.lblSprite1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSprite1.Name = "lblSprite1"
        Me.lblSprite1.Size = New System.Drawing.Size(40, 15)
        Me.lblSprite1.TabIndex = 22
        Me.lblSprite1.Text = "Sprite:"
        '
        'picSprite1
        '
        Me.picSprite1.BackColor = System.Drawing.Color.Black
        Me.picSprite1.Location = New System.Drawing.Point(256, 12)
        Me.picSprite1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picSprite1.Name = "picSprite1"
        Me.picSprite1.Size = New System.Drawing.Size(239, 190)
        Me.picSprite1.TabIndex = 21
        Me.picSprite1.TabStop = false
        '
        'DarkGroupBox3
        '
        Me.DarkGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.DarkGroupBox3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer), CType(CType(90,Byte),Integer))
        Me.DarkGroupBox3.Controls.Add(Me.nudLoopTime0)
        Me.DarkGroupBox3.Controls.Add(Me.nudFrameCount0)
        Me.DarkGroupBox3.Controls.Add(Me.nudLoopCount0)
        Me.DarkGroupBox3.Controls.Add(Me.nudSprite0)
        Me.DarkGroupBox3.Controls.Add(Me.lblLoopTime0)
        Me.DarkGroupBox3.Controls.Add(Me.lblFrameCount0)
        Me.DarkGroupBox3.Controls.Add(Me.lblLoopCount0)
        Me.DarkGroupBox3.Controls.Add(Me.lblSprite0)
        Me.DarkGroupBox3.Controls.Add(Me.picSprite0)
        Me.DarkGroupBox3.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox3.Location = New System.Drawing.Point(7, 106)
        Me.DarkGroupBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox3.Name = "DarkGroupBox3"
        Me.DarkGroupBox3.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox3.Size = New System.Drawing.Size(497, 208)
        Me.DarkGroupBox3.TabIndex = 22
        Me.DarkGroupBox3.TabStop = false
        Me.DarkGroupBox3.Text = "Layer 0 - Beneath Player"
        '
        'nudLoopTime0
        '
        Me.nudLoopTime0.Location = New System.Drawing.Point(102, 159)
        Me.nudLoopTime0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudLoopTime0.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudLoopTime0.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudLoopTime0.Name = "nudLoopTime0"
        Me.nudLoopTime0.Size = New System.Drawing.Size(140, 23)
        Me.nudLoopTime0.TabIndex = 33
        Me.nudLoopTime0.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'nudFrameCount0
        '
        Me.nudFrameCount0.Location = New System.Drawing.Point(102, 114)
        Me.nudFrameCount0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudFrameCount0.Name = "nudFrameCount0"
        Me.nudFrameCount0.Size = New System.Drawing.Size(140, 23)
        Me.nudFrameCount0.TabIndex = 32
        '
        'nudLoopCount0
        '
        Me.nudLoopCount0.Location = New System.Drawing.Point(102, 70)
        Me.nudLoopCount0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudLoopCount0.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudLoopCount0.Name = "nudLoopCount0"
        Me.nudLoopCount0.Size = New System.Drawing.Size(140, 23)
        Me.nudLoopCount0.TabIndex = 31
        Me.nudLoopCount0.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudSprite0
        '
        Me.nudSprite0.Location = New System.Drawing.Point(102, 28)
        Me.nudSprite0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudSprite0.Name = "nudSprite0"
        Me.nudSprite0.Size = New System.Drawing.Size(140, 23)
        Me.nudSprite0.TabIndex = 30
        '
        'lblLoopTime0
        '
        Me.lblLoopTime0.AutoSize = true
        Me.lblLoopTime0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.lblLoopTime0.Location = New System.Drawing.Point(12, 162)
        Me.lblLoopTime0.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLoopTime0.Name = "lblLoopTime0"
        Me.lblLoopTime0.Size = New System.Drawing.Size(66, 15)
        Me.lblLoopTime0.TabIndex = 28
        Me.lblLoopTime0.Text = "Loop Time:"
        '
        'lblFrameCount0
        '
        Me.lblFrameCount0.AutoSize = true
        Me.lblFrameCount0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.lblFrameCount0.Location = New System.Drawing.Point(13, 117)
        Me.lblFrameCount0.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFrameCount0.Name = "lblFrameCount0"
        Me.lblFrameCount0.Size = New System.Drawing.Size(79, 15)
        Me.lblFrameCount0.TabIndex = 26
        Me.lblFrameCount0.Text = "Frame Count:"
        '
        'lblLoopCount0
        '
        Me.lblLoopCount0.AutoSize = true
        Me.lblLoopCount0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.lblLoopCount0.Location = New System.Drawing.Point(13, 73)
        Me.lblLoopCount0.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLoopCount0.Name = "lblLoopCount0"
        Me.lblLoopCount0.Size = New System.Drawing.Size(73, 15)
        Me.lblLoopCount0.TabIndex = 24
        Me.lblLoopCount0.Text = "Loop Count:"
        '
        'lblSprite0
        '
        Me.lblSprite0.AutoSize = true
        Me.lblSprite0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.lblSprite0.Location = New System.Drawing.Point(12, 30)
        Me.lblSprite0.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSprite0.Name = "lblSprite0"
        Me.lblSprite0.Size = New System.Drawing.Size(40, 15)
        Me.lblSprite0.TabIndex = 22
        Me.lblSprite0.Text = "Sprite:"
        '
        'picSprite0
        '
        Me.picSprite0.BackColor = System.Drawing.Color.Black
        Me.picSprite0.Location = New System.Drawing.Point(256, 12)
        Me.picSprite0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picSprite0.Name = "picSprite0"
        Me.picSprite0.Size = New System.Drawing.Size(239, 190)
        Me.picSprite0.TabIndex = 21
        Me.picSprite0.TabStop = false
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(69,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(74,Byte),Integer))
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.txtName.Location = New System.Drawing.Point(136, 32)
        Me.txtName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(367, 23)
        Me.txtName.TabIndex = 1
        '
        'DarkLabel1
        '
        Me.DarkLabel1.AutoSize = true
        Me.DarkLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer), CType(CType(220,Byte),Integer))
        Me.DarkLabel1.Location = New System.Drawing.Point(21, 32)
        Me.DarkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel1.Name = "DarkLabel1"
        Me.DarkLabel1.Size = New System.Drawing.Size(42, 15)
        Me.DarkLabel1.TabIndex = 0
        Me.DarkLabel1.Text = "Name:"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(9, 438)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSave.Size = New System.Drawing.Size(219, 27)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(9, 471)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Padding = New System.Windows.Forms.Padding(6)
        Me.btnDelete.Size = New System.Drawing.Size(219, 27)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Delete"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(9, 504)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCancel.Size = New System.Drawing.Size(219, 27)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        '
        'FrmEditor_Animation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 15!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = true
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(758, 543)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.DarkGroupBox2)
        Me.Controls.Add(Me.DarkGroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "FrmEditor_Animation"
        Me.Text = "Animation Editor"
        Me.DarkGroupBox1.ResumeLayout(false)
        Me.DarkGroupBox2.ResumeLayout(false)
        Me.DarkGroupBox2.PerformLayout
        Me.DarkGroupBox4.ResumeLayout(false)
        Me.DarkGroupBox4.PerformLayout
        CType(Me.nudLoopTime1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudFrameCount1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudLoopCount1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudSprite1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picSprite1,System.ComponentModel.ISupportInitialize).EndInit
        Me.DarkGroupBox3.ResumeLayout(false)
        Me.DarkGroupBox3.PerformLayout
        CType(Me.nudLoopTime0,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudFrameCount0,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudLoopCount0,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudSprite0,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picSprite0,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents DarkGroupBox1 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkGroupBox2 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents lstIndex As System.Windows.Forms.ListBox
    Friend WithEvents txtName As DarkUI.Controls.DarkTextBox
    Friend WithEvents DarkLabel1 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkGroupBox3 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents picSprite0 As System.Windows.Forms.PictureBox
    Friend WithEvents lblLoopCount0 As DarkUI.Controls.DarkLabel
    Friend WithEvents lblSprite0 As DarkUI.Controls.DarkLabel
    Friend WithEvents lblLoopTime0 As DarkUI.Controls.DarkLabel
    Friend WithEvents lblFrameCount0 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkGroupBox4 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents lblLoopTime1 As DarkUI.Controls.DarkLabel
    Friend WithEvents lblFrameCount1 As DarkUI.Controls.DarkLabel
    Friend WithEvents lblLoopCount1 As DarkUI.Controls.DarkLabel
    Friend WithEvents lblSprite1 As DarkUI.Controls.DarkLabel
    Friend WithEvents picSprite1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnSave As DarkUI.Controls.DarkButton
    Friend WithEvents btnDelete As DarkUI.Controls.DarkButton
    Friend WithEvents btnCancel As DarkUI.Controls.DarkButton
    Friend WithEvents nudLoopTime0 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudFrameCount0 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudLoopCount0 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudSprite0 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudLoopTime1 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudFrameCount1 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudLoopCount1 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudSprite1 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents cmbSound As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel2 As DarkUI.Controls.DarkLabel
End Class
