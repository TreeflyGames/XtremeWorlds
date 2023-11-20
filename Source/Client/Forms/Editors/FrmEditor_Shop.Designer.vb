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
        Me.DarkGroupBox1 = New DarkUI.Controls.DarkGroupBox()
        Me.lstIndex = New System.Windows.Forms.ListBox()
        Me.DarkGroupBox2 = New DarkUI.Controls.DarkGroupBox()
        Me.DarkGroupBox3 = New DarkUI.Controls.DarkGroupBox()
        Me.btnDeleteTrade = New DarkUI.Controls.DarkButton()
        Me.btnUpdate = New DarkUI.Controls.DarkButton()
        Me.nudCostValue = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel8 = New DarkUI.Controls.DarkLabel()
        Me.cmbCostItem = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel7 = New DarkUI.Controls.DarkLabel()
        Me.nudItemValue = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel6 = New DarkUI.Controls.DarkLabel()
        Me.cmbItem = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel5 = New DarkUI.Controls.DarkLabel()
        Me.lstTradeItem = New System.Windows.Forms.ListBox()
        Me.DarkLabel4 = New DarkUI.Controls.DarkLabel()
        Me.nudBuy = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel3 = New DarkUI.Controls.DarkLabel()
        Me.nudFace = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel2 = New DarkUI.Controls.DarkLabel()
        Me.txtName = New DarkUI.Controls.DarkTextBox()
        Me.DarkLabel1 = New DarkUI.Controls.DarkLabel()
        Me.picFace = New System.Windows.Forms.PictureBox()
        Me.btnCancel = New DarkUI.Controls.DarkButton()
        Me.btnDelete = New DarkUI.Controls.DarkButton()
        Me.btnSave = New DarkUI.Controls.DarkButton()
        Me.DarkGroupBox1.SuspendLayout
        Me.DarkGroupBox2.SuspendLayout
        Me.DarkGroupBox3.SuspendLayout
        CType(Me.nudCostValue,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudItemValue,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudBuy,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.nudFace,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picFace,System.ComponentModel.ISupportInitialize).BeginInit
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
        Me.DarkGroupBox1.Size = New System.Drawing.Size(244, 327)
        Me.DarkGroupBox1.TabIndex = 0
        Me.DarkGroupBox1.TabStop = False
        Me.DarkGroupBox1.Text = "Shop List"
        '
        'lstIndex
        '
        Me.lstIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstIndex.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstIndex.FormattingEnabled = True
        Me.lstIndex.ItemHeight = 15
        Me.lstIndex.Location = New System.Drawing.Point(7, 22)
        Me.lstIndex.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstIndex.Name = "lstIndex"
        Me.lstIndex.Size = New System.Drawing.Size(228, 302)
        Me.lstIndex.TabIndex = 1
        '
        'DarkGroupBox2
        '
        Me.DarkGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox2.Controls.Add(Me.DarkGroupBox3)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel4)
        Me.DarkGroupBox2.Controls.Add(Me.nudBuy)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel3)
        Me.DarkGroupBox2.Controls.Add(Me.nudFace)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel2)
        Me.DarkGroupBox2.Controls.Add(Me.txtName)
        Me.DarkGroupBox2.Controls.Add(Me.DarkLabel1)
        Me.DarkGroupBox2.Controls.Add(Me.picFace)
        Me.DarkGroupBox2.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox2.Location = New System.Drawing.Point(254, 3)
        Me.DarkGroupBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox2.Name = "DarkGroupBox2"
        Me.DarkGroupBox2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox2.Size = New System.Drawing.Size(483, 434)
        Me.DarkGroupBox2.TabIndex = 1
        Me.DarkGroupBox2.TabStop = False
        Me.DarkGroupBox2.Text = "Shop Properties"
        '
        'DarkGroupBox3
        '
        Me.DarkGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox3.Controls.Add(Me.btnDeleteTrade)
        Me.DarkGroupBox3.Controls.Add(Me.btnUpdate)
        Me.DarkGroupBox3.Controls.Add(Me.nudCostValue)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel8)
        Me.DarkGroupBox3.Controls.Add(Me.cmbCostItem)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel7)
        Me.DarkGroupBox3.Controls.Add(Me.nudItemValue)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel6)
        Me.DarkGroupBox3.Controls.Add(Me.cmbItem)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel5)
        Me.DarkGroupBox3.Controls.Add(Me.lstTradeItem)
        Me.DarkGroupBox3.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox3.Location = New System.Drawing.Point(7, 140)
        Me.DarkGroupBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox3.Name = "DarkGroupBox3"
        Me.DarkGroupBox3.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox3.Size = New System.Drawing.Size(468, 281)
        Me.DarkGroupBox3.TabIndex = 52
        Me.DarkGroupBox3.TabStop = False
        Me.DarkGroupBox3.Text = "Items the Shop Sells"
        '
        'btnDeleteTrade
        '
        Me.btnDeleteTrade.Location = New System.Drawing.Point(237, 243)
        Me.btnDeleteTrade.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDeleteTrade.Name = "btnDeleteTrade"
        Me.btnDeleteTrade.Padding = New System.Windows.Forms.Padding(6)
        Me.btnDeleteTrade.Size = New System.Drawing.Size(88, 27)
        Me.btnDeleteTrade.TabIndex = 53
        Me.btnDeleteTrade.Text = "Delete"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(142, 243)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Padding = New System.Windows.Forms.Padding(6)
        Me.btnUpdate.Size = New System.Drawing.Size(88, 27)
        Me.btnUpdate.TabIndex = 52
        Me.btnUpdate.Text = "Update"
        '
        'nudCostValue
        '
        Me.nudCostValue.Location = New System.Drawing.Point(346, 212)
        Me.nudCostValue.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudCostValue.Name = "nudCostValue"
        Me.nudCostValue.Size = New System.Drawing.Size(114, 23)
        Me.nudCostValue.TabIndex = 51
        '
        'DarkLabel8
        '
        Me.DarkLabel8.AutoSize = True
        Me.DarkLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel8.Location = New System.Drawing.Point(286, 215)
        Me.DarkLabel8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel8.Name = "DarkLabel8"
        Me.DarkLabel8.Size = New System.Drawing.Size(54, 15)
        Me.DarkLabel8.TabIndex = 50
        Me.DarkLabel8.Text = "Amount:"
        '
        'cmbCostItem
        '
        Me.cmbCostItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCostItem.FormattingEnabled = True
        Me.cmbCostItem.Location = New System.Drawing.Point(86, 212)
        Me.cmbCostItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCostItem.Name = "cmbCostItem"
        Me.cmbCostItem.Size = New System.Drawing.Size(192, 24)
        Me.cmbCostItem.TabIndex = 49
        '
        'DarkLabel7
        '
        Me.DarkLabel7.AutoSize = True
        Me.DarkLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel7.Location = New System.Drawing.Point(7, 216)
        Me.DarkLabel7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel7.Name = "DarkLabel7"
        Me.DarkLabel7.Size = New System.Drawing.Size(61, 15)
        Me.DarkLabel7.TabIndex = 48
        Me.DarkLabel7.Text = "Item Cost:"
        '
        'nudItemValue
        '
        Me.nudItemValue.Location = New System.Drawing.Point(346, 182)
        Me.nudItemValue.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudItemValue.Name = "nudItemValue"
        Me.nudItemValue.Size = New System.Drawing.Size(114, 23)
        Me.nudItemValue.TabIndex = 47
        '
        'DarkLabel6
        '
        Me.DarkLabel6.AutoSize = True
        Me.DarkLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel6.Location = New System.Drawing.Point(286, 185)
        Me.DarkLabel6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel6.Name = "DarkLabel6"
        Me.DarkLabel6.Size = New System.Drawing.Size(54, 15)
        Me.DarkLabel6.TabIndex = 46
        Me.DarkLabel6.Text = "Amount:"
        '
        'cmbItem
        '
        Me.cmbItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbItem.FormattingEnabled = True
        Me.cmbItem.Location = New System.Drawing.Point(86, 181)
        Me.cmbItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(192, 24)
        Me.cmbItem.TabIndex = 45
        '
        'DarkLabel5
        '
        Me.DarkLabel5.AutoSize = True
        Me.DarkLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel5.Location = New System.Drawing.Point(7, 185)
        Me.DarkLabel5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel5.Name = "DarkLabel5"
        Me.DarkLabel5.Size = New System.Drawing.Size(69, 15)
        Me.DarkLabel5.TabIndex = 44
        Me.DarkLabel5.Text = "Item to Sell:"
        '
        'lstTradeItem
        '
        Me.lstTradeItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstTradeItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstTradeItem.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstTradeItem.FormattingEnabled = True
        Me.lstTradeItem.ItemHeight = 15
        Me.lstTradeItem.Items.AddRange(New Object() {"1.", "2.", "3.", "4.", "5.", "6.", "7.", "8."})
        Me.lstTradeItem.Location = New System.Drawing.Point(7, 22)
        Me.lstTradeItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstTradeItem.Name = "lstTradeItem"
        Me.lstTradeItem.Size = New System.Drawing.Size(453, 152)
        Me.lstTradeItem.TabIndex = 43
        '
        'DarkLabel4
        '
        Me.DarkLabel4.AutoSize = True
        Me.DarkLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel4.Location = New System.Drawing.Point(326, 100)
        Me.DarkLabel4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel4.Name = "DarkLabel4"
        Me.DarkLabel4.Size = New System.Drawing.Size(109, 15)
        Me.DarkLabel4.TabIndex = 51
        Me.DarkLabel4.Text = "% of the Item Value"
        '
        'nudBuy
        '
        Me.nudBuy.Location = New System.Drawing.Point(225, 98)
        Me.nudBuy.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudBuy.Name = "nudBuy"
        Me.nudBuy.Size = New System.Drawing.Size(93, 23)
        Me.nudBuy.TabIndex = 50
        '
        'DarkLabel3
        '
        Me.DarkLabel3.AutoSize = True
        Me.DarkLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel3.Location = New System.Drawing.Point(126, 100)
        Me.DarkLabel3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel3.Name = "DarkLabel3"
        Me.DarkLabel3.Size = New System.Drawing.Size(81, 15)
        Me.DarkLabel3.TabIndex = 49
        Me.DarkLabel3.Text = "Buyback Rate:"
        '
        'nudFace
        '
        Me.nudFace.Location = New System.Drawing.Point(210, 59)
        Me.nudFace.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudFace.Name = "nudFace"
        Me.nudFace.Size = New System.Drawing.Size(108, 23)
        Me.nudFace.TabIndex = 48
        '
        'DarkLabel2
        '
        Me.DarkLabel2.AutoSize = True
        Me.DarkLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel2.Location = New System.Drawing.Point(126, 61)
        Me.DarkLabel2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel2.Name = "DarkLabel2"
        Me.DarkLabel2.Size = New System.Drawing.Size(34, 15)
        Me.DarkLabel2.TabIndex = 47
        Me.DarkLabel2.Text = "Face:"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(210, 22)
        Me.txtName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(264, 23)
        Me.txtName.TabIndex = 46
        '
        'DarkLabel1
        '
        Me.DarkLabel1.AutoSize = True
        Me.DarkLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel1.Location = New System.Drawing.Point(126, 24)
        Me.DarkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel1.Name = "DarkLabel1"
        Me.DarkLabel1.Size = New System.Drawing.Size(42, 15)
        Me.DarkLabel1.TabIndex = 45
        Me.DarkLabel1.Text = "Name:"
        '
        'picFace
        '
        Me.picFace.BackColor = System.Drawing.Color.Black
        Me.picFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picFace.Location = New System.Drawing.Point(7, 22)
        Me.picFace.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picFace.Name = "picFace"
        Me.picFace.Size = New System.Drawing.Size(112, 111)
        Me.picFace.TabIndex = 44
        Me.picFace.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(13, 402)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCancel.Size = New System.Drawing.Size(228, 27)
        Me.btnCancel.TabIndex = 55
        Me.btnCancel.Text = "Cancel"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(13, 369)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Padding = New System.Windows.Forms.Padding(6)
        Me.btnDelete.Size = New System.Drawing.Size(228, 27)
        Me.btnDelete.TabIndex = 54
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(11, 336)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSave.Size = New System.Drawing.Size(228, 27)
        Me.btnSave.TabIndex = 53
        Me.btnSave.Text = "Save"
        '
        'frmEditor_Shop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 15!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45,Byte),Integer), CType(CType(45,Byte),Integer), CType(CType(48,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(740, 441)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.DarkGroupBox2)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.DarkGroupBox1)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = false
        Me.Name = "frmEditor_Shop"
        Me.Text = "Shop Editor"
        Me.DarkGroupBox1.ResumeLayout(false)
        Me.DarkGroupBox2.ResumeLayout(false)
        Me.DarkGroupBox2.PerformLayout
        Me.DarkGroupBox3.ResumeLayout(false)
        Me.DarkGroupBox3.PerformLayout
        CType(Me.nudCostValue,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudItemValue,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudBuy,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudFace,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picFace,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

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
