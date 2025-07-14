using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_Shop : Form
    {

        // Shared instance of the form
        private static frmEditor_Shop _instance;

        // Public property to get the shared instance
        public static frmEditor_Shop Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Shop();
                }
                return _instance;
            }
        }

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing & components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            DarkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            lstIndex = new ListBox();
            DarkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            nudBuy = new DarkUI.Controls.DarkNumericUpDown();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            btnDeleteTrade = new DarkUI.Controls.DarkButton();
            btnUpdate = new DarkUI.Controls.DarkButton();
            nudCostValue = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel8 = new DarkUI.Controls.DarkLabel();
            cmbCostItem = new DarkUI.Controls.DarkComboBox();
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            nudItemValue = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            cmbItem = new DarkUI.Controls.DarkComboBox();
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            lstTradeItem = new ListBox();
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            txtName = new DarkUI.Controls.DarkTextBox();
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            btnCancel = new DarkUI.Controls.DarkButton();
            btnDelete = new DarkUI.Controls.DarkButton();
            btnSave = new DarkUI.Controls.DarkButton();
            DarkGroupBox1.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudBuy).BeginInit();
            DarkGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCostValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudItemValue).BeginInit();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(4, 3);
            DarkGroupBox1.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Size = new Size(244, 257);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Shop List";
            // 
            // lstIndex
            // 
            lstIndex.BackColor = Color.FromArgb(45, 45, 48);
            lstIndex.BorderStyle = BorderStyle.FixedSingle;
            lstIndex.ForeColor = Color.Gainsboro;
            lstIndex.FormattingEnabled = true;
            lstIndex.Location = new Point(7, 22);
            lstIndex.Margin = new Padding(4, 3, 4, 3);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(228, 227);
            lstIndex.TabIndex = 1;
            lstIndex.Click += lstIndex_Click;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(nudBuy);
            DarkGroupBox2.Controls.Add(DarkGroupBox3);
            DarkGroupBox2.Controls.Add(DarkLabel4);
            DarkGroupBox2.Controls.Add(DarkLabel3);
            DarkGroupBox2.Controls.Add(txtName);
            DarkGroupBox2.Controls.Add(DarkLabel1);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(254, 3);
            DarkGroupBox2.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Size = new Size(483, 362);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // nudBuy
            // 
            nudBuy.Location = new Point(103, 52);
            nudBuy.Margin = new Padding(4, 3, 4, 3);
            nudBuy.Name = "nudBuy";
            nudBuy.Size = new Size(93, 23);
            nudBuy.TabIndex = 53;
            // 
            // DarkGroupBox3
            // 
            DarkGroupBox3.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox3.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox3.Controls.Add(btnDeleteTrade);
            DarkGroupBox3.Controls.Add(btnUpdate);
            DarkGroupBox3.Controls.Add(nudCostValue);
            DarkGroupBox3.Controls.Add(DarkLabel8);
            DarkGroupBox3.Controls.Add(cmbCostItem);
            DarkGroupBox3.Controls.Add(DarkLabel7);
            DarkGroupBox3.Controls.Add(nudItemValue);
            DarkGroupBox3.Controls.Add(DarkLabel6);
            DarkGroupBox3.Controls.Add(cmbItem);
            DarkGroupBox3.Controls.Add(DarkLabel5);
            DarkGroupBox3.Controls.Add(lstTradeItem);
            DarkGroupBox3.ForeColor = Color.Gainsboro;
            DarkGroupBox3.Location = new Point(14, 81);
            DarkGroupBox3.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Size = new Size(468, 275);
            DarkGroupBox3.TabIndex = 52;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Items the Shop Sells";
            // 
            // btnDeleteTrade
            // 
            btnDeleteTrade.Location = new Point(237, 243);
            btnDeleteTrade.Margin = new Padding(4, 3, 4, 3);
            btnDeleteTrade.Name = "btnDeleteTrade";
            btnDeleteTrade.Padding = new Padding(6);
            btnDeleteTrade.Size = new Size(88, 27);
            btnDeleteTrade.TabIndex = 53;
            btnDeleteTrade.Text = "Delete";
            btnDeleteTrade.Click += BtnDeleteTrade_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(142, 243);
            btnUpdate.Margin = new Padding(4, 3, 4, 3);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Padding = new Padding(6);
            btnUpdate.Size = new Size(88, 27);
            btnUpdate.TabIndex = 52;
            btnUpdate.Text = "Update";
            btnUpdate.Click += BtnUpdate_Click;
            // 
            // nudCostValue
            // 
            nudCostValue.Location = new Point(346, 212);
            nudCostValue.Margin = new Padding(4, 3, 4, 3);
            nudCostValue.Name = "nudCostValue";
            nudCostValue.Size = new Size(114, 23);
            nudCostValue.TabIndex = 51;
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(286, 215);
            DarkLabel8.Margin = new Padding(4, 0, 4, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(54, 15);
            DarkLabel8.TabIndex = 50;
            DarkLabel8.Text = "Amount:";
            // 
            // cmbCostItem
            // 
            cmbCostItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCostItem.FormattingEnabled = true;
            cmbCostItem.Location = new Point(86, 212);
            cmbCostItem.Margin = new Padding(4, 3, 4, 3);
            cmbCostItem.Name = "cmbCostItem";
            cmbCostItem.Size = new Size(192, 24);
            cmbCostItem.TabIndex = 49;
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(7, 216);
            DarkLabel7.Margin = new Padding(4, 0, 4, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(61, 15);
            DarkLabel7.TabIndex = 48;
            DarkLabel7.Text = "Item Cost:";
            // 
            // nudItemValue
            // 
            nudItemValue.Location = new Point(346, 182);
            nudItemValue.Margin = new Padding(4, 3, 4, 3);
            nudItemValue.Name = "nudItemValue";
            nudItemValue.Size = new Size(114, 23);
            nudItemValue.TabIndex = 47;
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(286, 185);
            DarkLabel6.Margin = new Padding(4, 0, 4, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(54, 15);
            DarkLabel6.TabIndex = 46;
            DarkLabel6.Text = "Amount:";
            // 
            // cmbItem
            // 
            cmbItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbItem.FormattingEnabled = true;
            cmbItem.Location = new Point(86, 181);
            cmbItem.Margin = new Padding(4, 3, 4, 3);
            cmbItem.Name = "cmbItem";
            cmbItem.Size = new Size(192, 24);
            cmbItem.TabIndex = 45;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(7, 185);
            DarkLabel5.Margin = new Padding(4, 0, 4, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(69, 15);
            DarkLabel5.TabIndex = 44;
            DarkLabel5.Text = "Item to Sell:";
            // 
            // lstTradeItem
            // 
            lstTradeItem.BackColor = Color.FromArgb(45, 45, 48);
            lstTradeItem.BorderStyle = BorderStyle.FixedSingle;
            lstTradeItem.ForeColor = Color.Gainsboro;
            lstTradeItem.FormattingEnabled = true;
            lstTradeItem.Items.AddRange(new object[] { "1.", "2.", "3.", "4.", "5.", "6.", "7.", "8." });
            lstTradeItem.Location = new Point(9, 24);
            lstTradeItem.Margin = new Padding(4, 3, 4, 3);
            lstTradeItem.Name = "lstTradeItem";
            lstTradeItem.Size = new Size(453, 152);
            lstTradeItem.TabIndex = 43;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(204, 54);
            DarkLabel4.Margin = new Padding(4, 0, 4, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(109, 15);
            DarkLabel4.TabIndex = 51;
            DarkLabel4.Text = "% of the Item Value";
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(14, 60);
            DarkLabel3.Margin = new Padding(4, 0, 4, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(81, 15);
            DarkLabel3.TabIndex = 49;
            DarkLabel3.Text = "Buyback Rate:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(69, 23);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(264, 23);
            txtName.TabIndex = 46;
            txtName.TextChanged += TxtName_TextChanged;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(14, 23);
            DarkLabel1.Margin = new Padding(4, 0, 4, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(42, 15);
            DarkLabel1.TabIndex = 45;
            DarkLabel1.Text = "Name:";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(13, 338);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(6);
            btnCancel.Size = new Size(228, 27);
            btnCancel.TabIndex = 55;
            btnCancel.Text = "Cancel";
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(13, 305);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(6);
            btnDelete.Size = new Size(228, 27);
            btnDelete.TabIndex = 54;
            btnDelete.Text = "Delete";
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(13, 272);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(6);
            btnSave.Size = new Size(228, 27);
            btnSave.TabIndex = 53;
            btnSave.Text = "Save";
            btnSave.Click += BtnSave_Click;
            // 
            // frmEditor_Shop
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(744, 374);
            Controls.Add(btnCancel);
            Controls.Add(DarkGroupBox2);
            Controls.Add(btnDelete);
            Controls.Add(DarkGroupBox1);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "frmEditor_Shop";
            Text = "Shop Editor";
            FormClosing += frmEditor_Shop_FormClosing;
            Load += frmEditor_Shop_Load;
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudBuy).EndInit();
            DarkGroupBox3.ResumeLayout(false);
            DarkGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCostValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudItemValue).EndInit();
            ResumeLayout(false);

        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal DarkUI.Controls.DarkLabel DarkLabel3;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox3;
        internal DarkUI.Controls.DarkComboBox cmbItem;
        internal DarkUI.Controls.DarkLabel DarkLabel5;
        internal ListBox lstTradeItem;
        internal DarkUI.Controls.DarkNumericUpDown nudItemValue;
        internal DarkUI.Controls.DarkLabel DarkLabel6;
        internal DarkUI.Controls.DarkLabel DarkLabel7;
        internal DarkUI.Controls.DarkComboBox cmbCostItem;
        internal DarkUI.Controls.DarkNumericUpDown nudCostValue;
        internal DarkUI.Controls.DarkLabel DarkLabel8;
        internal DarkUI.Controls.DarkButton btnUpdate;
        internal DarkUI.Controls.DarkButton btnDeleteTrade;
        internal DarkUI.Controls.DarkButton btnCancel;
        internal DarkUI.Controls.DarkButton btnDelete;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkNumericUpDown nudBuy;
    }
}