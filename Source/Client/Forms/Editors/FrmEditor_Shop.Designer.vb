<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditor_Shop
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
        DarkGroupBox1 = New DarkUI.Controls.DarkGroupBox()
        lstIndex = New ListBox()
        DarkGroupBox2 = New DarkUI.Controls.DarkGroupBox()
        DarkGroupBox3 = New DarkUI.Controls.DarkGroupBox()
        btnDeleteTrade = New DarkUI.Controls.DarkButton()
        btnUpdate = New DarkUI.Controls.DarkButton()
        nudCostValue = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel8 = New DarkUI.Controls.DarkLabel()
        cmbCostItem = New DarkUI.Controls.DarkComboBox()
        DarkLabel7 = New DarkUI.Controls.DarkLabel()
        nudItemValue = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel6 = New DarkUI.Controls.DarkLabel()
        cmbItem = New DarkUI.Controls.DarkComboBox()
        DarkLabel5 = New DarkUI.Controls.DarkLabel()
        lstTradeItem = New ListBox()
        DarkLabel4 = New DarkUI.Controls.DarkLabel()
        nudBuy = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel3 = New DarkUI.Controls.DarkLabel()
        nudFace = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel2 = New DarkUI.Controls.DarkLabel()
        txtName = New DarkUI.Controls.DarkTextBox()
        DarkLabel1 = New DarkUI.Controls.DarkLabel()
        picFace = New PictureBox()
        btnCancel = New DarkUI.Controls.DarkButton()
        btnDelete = New DarkUI.Controls.DarkButton()
        btnSave = New DarkUI.Controls.DarkButton()
        DarkGroupBox1.SuspendLayout()
        DarkGroupBox2.SuspendLayout()
        DarkGroupBox3.SuspendLayout()
        CType(nudCostValue, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudItemValue, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudBuy, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudFace, ComponentModel.ISupportInitialize).BeginInit()
        CType(picFace, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DarkGroupBox1
        ' 
        DarkGroupBox1.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox1.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox1.Controls.Add(lstIndex)
        DarkGroupBox1.ForeColor = Color.Gainsboro
        DarkGroupBox1.Location = New Point(6, 5)
        DarkGroupBox1.Margin = New Padding(6, 5, 6, 5)
        DarkGroupBox1.Name = "DarkGroupBox1"
        DarkGroupBox1.Padding = New Padding(6, 5, 6, 5)
        DarkGroupBox1.Size = New Size(349, 545)
        DarkGroupBox1.TabIndex = 0
        DarkGroupBox1.TabStop = False
        DarkGroupBox1.Text = "Shop List"
        ' 
        ' lstIndex
        ' 
        lstIndex.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        lstIndex.BorderStyle = BorderStyle.FixedSingle
        lstIndex.ForeColor = Color.Gainsboro
        lstIndex.FormattingEnabled = True
        lstIndex.ItemHeight = 25
        lstIndex.Location = New Point(10, 37)
        lstIndex.Margin = New Padding(6, 5, 6, 5)
        lstIndex.Name = "lstIndex"
        lstIndex.Size = New Size(325, 502)
        lstIndex.TabIndex = 1
        ' 
        ' DarkGroupBox2
        ' 
        DarkGroupBox2.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox2.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox2.Controls.Add(DarkGroupBox3)
        DarkGroupBox2.Controls.Add(DarkLabel4)
        DarkGroupBox2.Controls.Add(nudBuy)
        DarkGroupBox2.Controls.Add(DarkLabel3)
        DarkGroupBox2.Controls.Add(nudFace)
        DarkGroupBox2.Controls.Add(DarkLabel2)
        DarkGroupBox2.Controls.Add(txtName)
        DarkGroupBox2.Controls.Add(DarkLabel1)
        DarkGroupBox2.Controls.Add(picFace)
        DarkGroupBox2.ForeColor = Color.Gainsboro
        DarkGroupBox2.Location = New Point(363, 5)
        DarkGroupBox2.Margin = New Padding(6, 5, 6, 5)
        DarkGroupBox2.Name = "DarkGroupBox2"
        DarkGroupBox2.Padding = New Padding(6, 5, 6, 5)
        DarkGroupBox2.Size = New Size(690, 723)
        DarkGroupBox2.TabIndex = 1
        DarkGroupBox2.TabStop = False
        DarkGroupBox2.Text = "Properties"
        ' 
        ' DarkGroupBox3
        ' 
        DarkGroupBox3.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox3.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox3.Controls.Add(btnDeleteTrade)
        DarkGroupBox3.Controls.Add(btnUpdate)
        DarkGroupBox3.Controls.Add(nudCostValue)
        DarkGroupBox3.Controls.Add(DarkLabel8)
        DarkGroupBox3.Controls.Add(cmbCostItem)
        DarkGroupBox3.Controls.Add(DarkLabel7)
        DarkGroupBox3.Controls.Add(nudItemValue)
        DarkGroupBox3.Controls.Add(DarkLabel6)
        DarkGroupBox3.Controls.Add(cmbItem)
        DarkGroupBox3.Controls.Add(DarkLabel5)
        DarkGroupBox3.Controls.Add(lstTradeItem)
        DarkGroupBox3.ForeColor = Color.Gainsboro
        DarkGroupBox3.Location = New Point(10, 234)
        DarkGroupBox3.Margin = New Padding(6, 5, 6, 5)
        DarkGroupBox3.Name = "DarkGroupBox3"
        DarkGroupBox3.Padding = New Padding(6, 5, 6, 5)
        DarkGroupBox3.Size = New Size(669, 468)
        DarkGroupBox3.TabIndex = 52
        DarkGroupBox3.TabStop = False
        DarkGroupBox3.Text = "Items the Shop Sells"
        ' 
        ' btnDeleteTrade
        ' 
        btnDeleteTrade.Location = New Point(339, 405)
        btnDeleteTrade.Margin = New Padding(6, 5, 6, 5)
        btnDeleteTrade.Name = "btnDeleteTrade"
        btnDeleteTrade.Padding = New Padding(9, 10, 9, 10)
        btnDeleteTrade.Size = New Size(126, 45)
        btnDeleteTrade.TabIndex = 53
        btnDeleteTrade.Text = "Delete"
        ' 
        ' btnUpdate
        ' 
        btnUpdate.Location = New Point(203, 405)
        btnUpdate.Margin = New Padding(6, 5, 6, 5)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Padding = New Padding(9, 10, 9, 10)
        btnUpdate.Size = New Size(126, 45)
        btnUpdate.TabIndex = 52
        btnUpdate.Text = "Update"
        ' 
        ' nudCostValue
        ' 
        nudCostValue.Location = New Point(494, 353)
        nudCostValue.Margin = New Padding(6, 5, 6, 5)
        nudCostValue.Name = "nudCostValue"
        nudCostValue.Size = New Size(163, 31)
        nudCostValue.TabIndex = 51
        ' 
        ' DarkLabel8
        ' 
        DarkLabel8.AutoSize = True
        DarkLabel8.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel8.Location = New Point(409, 359)
        DarkLabel8.Margin = New Padding(6, 0, 6, 0)
        DarkLabel8.Name = "DarkLabel8"
        DarkLabel8.Size = New Size(81, 25)
        DarkLabel8.TabIndex = 50
        DarkLabel8.Text = "Amount:"
        ' 
        ' cmbCostItem
        ' 
        cmbCostItem.DrawMode = DrawMode.OwnerDrawFixed
        cmbCostItem.FormattingEnabled = True
        cmbCostItem.Location = New Point(123, 353)
        cmbCostItem.Margin = New Padding(6, 5, 6, 5)
        cmbCostItem.Name = "cmbCostItem"
        cmbCostItem.Size = New Size(273, 32)
        cmbCostItem.TabIndex = 49
        ' 
        ' DarkLabel7
        ' 
        DarkLabel7.AutoSize = True
        DarkLabel7.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel7.Location = New Point(10, 360)
        DarkLabel7.Margin = New Padding(6, 0, 6, 0)
        DarkLabel7.Name = "DarkLabel7"
        DarkLabel7.Size = New Size(93, 25)
        DarkLabel7.TabIndex = 48
        DarkLabel7.Text = "Item Cost:"
        ' 
        ' nudItemValue
        ' 
        nudItemValue.Location = New Point(494, 303)
        nudItemValue.Margin = New Padding(6, 5, 6, 5)
        nudItemValue.Name = "nudItemValue"
        nudItemValue.Size = New Size(163, 31)
        nudItemValue.TabIndex = 47
        ' 
        ' DarkLabel6
        ' 
        DarkLabel6.AutoSize = True
        DarkLabel6.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel6.Location = New Point(409, 309)
        DarkLabel6.Margin = New Padding(6, 0, 6, 0)
        DarkLabel6.Name = "DarkLabel6"
        DarkLabel6.Size = New Size(81, 25)
        DarkLabel6.TabIndex = 46
        DarkLabel6.Text = "Amount:"
        ' 
        ' cmbItem
        ' 
        cmbItem.DrawMode = DrawMode.OwnerDrawFixed
        cmbItem.FormattingEnabled = True
        cmbItem.Location = New Point(123, 302)
        cmbItem.Margin = New Padding(6, 5, 6, 5)
        cmbItem.Name = "cmbItem"
        cmbItem.Size = New Size(273, 32)
        cmbItem.TabIndex = 45
        ' 
        ' DarkLabel5
        ' 
        DarkLabel5.AutoSize = True
        DarkLabel5.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel5.Location = New Point(10, 309)
        DarkLabel5.Margin = New Padding(6, 0, 6, 0)
        DarkLabel5.Name = "DarkLabel5"
        DarkLabel5.Size = New Size(106, 25)
        DarkLabel5.TabIndex = 44
        DarkLabel5.Text = "Item to Sell:"
        ' 
        ' lstTradeItem
        ' 
        lstTradeItem.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        lstTradeItem.BorderStyle = BorderStyle.FixedSingle
        lstTradeItem.ForeColor = Color.Gainsboro
        lstTradeItem.FormattingEnabled = True
        lstTradeItem.ItemHeight = 25
        lstTradeItem.Items.AddRange(New Object() {"1.", "2.", "3.", "4.", "5.", "6.", "7.", "8."})
        lstTradeItem.Location = New Point(10, 37)
        lstTradeItem.Margin = New Padding(6, 5, 6, 5)
        lstTradeItem.Name = "lstTradeItem"
        lstTradeItem.Size = New Size(646, 252)
        lstTradeItem.TabIndex = 43
        ' 
        ' DarkLabel4
        ' 
        DarkLabel4.AutoSize = True
        DarkLabel4.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel4.Location = New Point(466, 166)
        DarkLabel4.Margin = New Padding(6, 0, 6, 0)
        DarkLabel4.Name = "DarkLabel4"
        DarkLabel4.Size = New Size(167, 25)
        DarkLabel4.TabIndex = 51
        DarkLabel4.Text = "% of the Item Value"
        ' 
        ' nudBuy
        ' 
        nudBuy.Location = New Point(321, 163)
        nudBuy.Margin = New Padding(6, 5, 6, 5)
        nudBuy.Name = "nudBuy"
        nudBuy.Size = New Size(133, 31)
        nudBuy.TabIndex = 50
        ' 
        ' DarkLabel3
        ' 
        DarkLabel3.AutoSize = True
        DarkLabel3.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel3.Location = New Point(180, 166)
        DarkLabel3.Margin = New Padding(6, 0, 6, 0)
        DarkLabel3.Name = "DarkLabel3"
        DarkLabel3.Size = New Size(122, 25)
        DarkLabel3.TabIndex = 49
        DarkLabel3.Text = "Buyback Rate:"
        ' 
        ' nudFace
        ' 
        nudFace.Location = New Point(300, 98)
        nudFace.Margin = New Padding(6, 5, 6, 5)
        nudFace.Name = "nudFace"
        nudFace.Size = New Size(154, 31)
        nudFace.TabIndex = 48
        ' 
        ' DarkLabel2
        ' 
        DarkLabel2.AutoSize = True
        DarkLabel2.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel2.Location = New Point(180, 102)
        DarkLabel2.Margin = New Padding(6, 0, 6, 0)
        DarkLabel2.Name = "DarkLabel2"
        DarkLabel2.Size = New Size(50, 25)
        DarkLabel2.TabIndex = 47
        DarkLabel2.Text = "Face:"
        ' 
        ' txtName
        ' 
        txtName.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtName.BorderStyle = BorderStyle.FixedSingle
        txtName.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtName.Location = New Point(300, 37)
        txtName.Margin = New Padding(6, 5, 6, 5)
        txtName.Name = "txtName"
        txtName.Size = New Size(376, 31)
        txtName.TabIndex = 46
        ' 
        ' DarkLabel1
        ' 
        DarkLabel1.AutoSize = True
        DarkLabel1.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel1.Location = New Point(180, 40)
        DarkLabel1.Margin = New Padding(6, 0, 6, 0)
        DarkLabel1.Name = "DarkLabel1"
        DarkLabel1.Size = New Size(63, 25)
        DarkLabel1.TabIndex = 45
        DarkLabel1.Text = "Name:"
        ' 
        ' picFace
        ' 
        picFace.BackColor = Color.Black
        picFace.BackgroundImageLayout = ImageLayout.Stretch
        picFace.Location = New Point(10, 37)
        picFace.Margin = New Padding(6, 5, 6, 5)
        picFace.Name = "picFace"
        picFace.Size = New Size(160, 185)
        picFace.TabIndex = 44
        picFace.TabStop = False
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(19, 670)
        btnCancel.Margin = New Padding(6, 5, 6, 5)
        btnCancel.Name = "btnCancel"
        btnCancel.Padding = New Padding(9, 10, 9, 10)
        btnCancel.Size = New Size(326, 45)
        btnCancel.TabIndex = 55
        btnCancel.Text = "Cancel"
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(19, 615)
        btnDelete.Margin = New Padding(6, 5, 6, 5)
        btnDelete.Name = "btnDelete"
        btnDelete.Padding = New Padding(9, 10, 9, 10)
        btnDelete.Size = New Size(326, 45)
        btnDelete.TabIndex = 54
        btnDelete.Text = "Delete"
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(16, 560)
        btnSave.Margin = New Padding(6, 5, 6, 5)
        btnSave.Name = "btnSave"
        btnSave.Padding = New Padding(9, 10, 9, 10)
        btnSave.Size = New Size(326, 45)
        btnSave.TabIndex = 53
        btnSave.Text = "Save"
        ' 
        ' frmEditor_Shop
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        ClientSize = New Size(1057, 737)
        Controls.Add(btnCancel)
        Controls.Add(DarkGroupBox2)
        Controls.Add(btnDelete)
        Controls.Add(DarkGroupBox1)
        Controls.Add(btnSave)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(6, 5, 6, 5)
        MaximizeBox = False
        Name = "frmEditor_Shop"
        Text = "Shop Editor"
        DarkGroupBox1.ResumeLayout(False)
        DarkGroupBox2.ResumeLayout(False)
        DarkGroupBox2.PerformLayout()
        DarkGroupBox3.ResumeLayout(False)
        DarkGroupBox3.PerformLayout()
        CType(nudCostValue, ComponentModel.ISupportInitialize).EndInit()
        CType(nudItemValue, ComponentModel.ISupportInitialize).EndInit()
        CType(nudBuy, ComponentModel.ISupportInitialize).EndInit()
        CType(nudFace, ComponentModel.ISupportInitialize).EndInit()
        CType(picFace, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents DarkGroupBox1 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents lstIndex As System.Windows.Forms.ListBox
    Friend WithEvents DarkGroupBox2 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents txtName As DarkUI.Controls.DarkTextBox
    Friend WithEvents DarkLabel1 As DarkUI.Controls.DarkLabel
    Friend WithEvents picFace As System.Windows.Forms.PictureBox
    Friend WithEvents nudFace As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel2 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel4 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudBuy As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel3 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkGroupBox3 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbItem As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel5 As DarkUI.Controls.DarkLabel
    Friend WithEvents lstTradeItem As ListBox
    Friend WithEvents nudItemValue As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel6 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel7 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbCostItem As DarkUI.Controls.DarkComboBox
    Friend WithEvents nudCostValue As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel8 As DarkUI.Controls.DarkLabel
    Friend WithEvents btnUpdate As DarkUI.Controls.DarkButton
    Friend WithEvents btnDeleteTrade As DarkUI.Controls.DarkButton
    Friend WithEvents btnCancel As DarkUI.Controls.DarkButton
    Friend WithEvents btnDelete As DarkUI.Controls.DarkButton
    Friend WithEvents btnSave As DarkUI.Controls.DarkButton
End Class
