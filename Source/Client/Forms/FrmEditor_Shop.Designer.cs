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
            lstIndex.Click += new EventHandler(lstIndex_Click);
            DarkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            btnDeleteTrade = new DarkUI.Controls.DarkButton();
            btnDeleteTrade.Click += new EventHandler(BtnDeleteTrade_Click);
            btnUpdate = new DarkUI.Controls.DarkButton();
            btnUpdate.Click += new EventHandler(BtnUpdate_Click);
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
            nudBuy = new DarkUI.Controls.DarkNumericUpDown();
            nudBuy.ValueChanged += new EventHandler(ScrlBuy_Scroll);
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            txtName = new DarkUI.Controls.DarkTextBox();
            txtName.TextChanged += new EventHandler(TxtName_TextChanged);
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            btnCancel = new DarkUI.Controls.DarkButton();
            btnCancel.Click += new EventHandler(BtnCancel_Click);
            btnDelete = new DarkUI.Controls.DarkButton();
            btnDelete.Click += new EventHandler(BtnDelete_Click);
            btnSave = new DarkUI.Controls.DarkButton();
            btnSave.Click += new EventHandler(BtnSave_Click);
            DarkGroupBox1.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            DarkGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCostValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudItemValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBuy).BeginInit();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(6, 5);
            DarkGroupBox1.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox1.Size = new Size(349, 429);
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
            lstIndex.ItemHeight = 25;
            lstIndex.Location = new Point(10, 37);
            lstIndex.Margin = new Padding(6, 5, 6, 5);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(325, 377);
            lstIndex.TabIndex = 1;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(DarkGroupBox3);
            DarkGroupBox2.Controls.Add(DarkLabel4);
            DarkGroupBox2.Controls.Add(nudBuy);
            DarkGroupBox2.Controls.Add(DarkLabel3);
            DarkGroupBox2.Controls.Add(txtName);
            DarkGroupBox2.Controls.Add(DarkLabel1);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(363, 5);
            DarkGroupBox2.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Size = new Size(690, 600);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
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
            DarkGroupBox3.Location = new Point(21, 113);
            DarkGroupBox3.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox3.Size = new Size(669, 468);
            DarkGroupBox3.TabIndex = 52;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Items the Shop Sells";
            // 
            // btnDeleteTrade
            // 
            btnDeleteTrade.Location = new Point(339, 405);
            btnDeleteTrade.Margin = new Padding(6, 5, 6, 5);
            btnDeleteTrade.Name = "btnDeleteTrade";
            btnDeleteTrade.Padding = new Padding(9, 10, 9, 10);
            btnDeleteTrade.Size = new Size(126, 45);
            btnDeleteTrade.TabIndex = 53;
            btnDeleteTrade.Text = "Delete";
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(203, 405);
            btnUpdate.Margin = new Padding(6, 5, 6, 5);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Padding = new Padding(9, 10, 9, 10);
            btnUpdate.Size = new Size(126, 45);
            btnUpdate.TabIndex = 52;
            btnUpdate.Text = "Update";
            // 
            // nudCostValue
            // 
            nudCostValue.Location = new Point(494, 353);
            nudCostValue.Margin = new Padding(6, 5, 6, 5);
            nudCostValue.Name = "nudCostValue";
            nudCostValue.Size = new Size(163, 31);
            nudCostValue.TabIndex = 51;
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(409, 359);
            DarkLabel8.Margin = new Padding(6, 0, 6, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(81, 25);
            DarkLabel8.TabIndex = 50;
            DarkLabel8.Text = "Amount:";
            // 
            // cmbCostItem
            // 
            cmbCostItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCostItem.FormattingEnabled = true;
            cmbCostItem.Location = new Point(123, 353);
            cmbCostItem.Margin = new Padding(6, 5, 6, 5);
            cmbCostItem.Name = "cmbCostItem";
            cmbCostItem.Size = new Size(273, 32);
            cmbCostItem.TabIndex = 49;
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(10, 360);
            DarkLabel7.Margin = new Padding(6, 0, 6, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(93, 25);
            DarkLabel7.TabIndex = 48;
            DarkLabel7.Text = "Item Cost:";
            // 
            // nudItemValue
            // 
            nudItemValue.Location = new Point(494, 303);
            nudItemValue.Margin = new Padding(6, 5, 6, 5);
            nudItemValue.Name = "nudItemValue";
            nudItemValue.Size = new Size(163, 31);
            nudItemValue.TabIndex = 47;
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(409, 309);
            DarkLabel6.Margin = new Padding(6, 0, 6, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(81, 25);
            DarkLabel6.TabIndex = 46;
            DarkLabel6.Text = "Amount:";
            // 
            // cmbItem
            // 
            cmbItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbItem.FormattingEnabled = true;
            cmbItem.Location = new Point(123, 302);
            cmbItem.Margin = new Padding(6, 5, 6, 5);
            cmbItem.Name = "cmbItem";
            cmbItem.Size = new Size(273, 32);
            cmbItem.TabIndex = 45;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(10, 309);
            DarkLabel5.Margin = new Padding(6, 0, 6, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(106, 25);
            DarkLabel5.TabIndex = 44;
            DarkLabel5.Text = "Item to Sell:";
            // 
            // lstTradeItem
            // 
            lstTradeItem.BackColor = Color.FromArgb(45, 45, 48);
            lstTradeItem.BorderStyle = BorderStyle.FixedSingle;
            lstTradeItem.ForeColor = Color.Gainsboro;
            lstTradeItem.FormattingEnabled = true;
            lstTradeItem.ItemHeight = 25;
            lstTradeItem.Items.AddRange(new object[] { "1.", "2.", "3.", "4.", "5.", "6.", "7.", "8." });
            lstTradeItem.Location = new Point(10, 37);
            lstTradeItem.Margin = new Padding(6, 5, 6, 5);
            lstTradeItem.Name = "lstTradeItem";
            lstTradeItem.Size = new Size(646, 252);
            lstTradeItem.TabIndex = 43;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(306, 75);
            DarkLabel4.Margin = new Padding(6, 0, 6, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(167, 25);
            DarkLabel4.TabIndex = 51;
            DarkLabel4.Text = "% of the Item Value";
            // 
            // nudBuy
            // 
            nudBuy.Location = new Point(161, 72);
            nudBuy.Margin = new Padding(6, 5, 6, 5);
            nudBuy.Name = "nudBuy";
            nudBuy.Size = new Size(133, 31);
            nudBuy.TabIndex = 50;
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(20, 75);
            DarkLabel3.Margin = new Padding(6, 0, 6, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(122, 25);
            DarkLabel3.TabIndex = 49;
            DarkLabel3.Text = "Buyback Rate:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(99, 39);
            txtName.Margin = new Padding(6, 5, 6, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(376, 31);
            txtName.TabIndex = 46;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(20, 39);
            DarkLabel1.Margin = new Padding(6, 0, 6, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(63, 25);
            DarkLabel1.TabIndex = 45;
            DarkLabel1.Text = "Name:";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(18, 563);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(9, 10, 9, 10);
            btnCancel.Size = new Size(326, 45);
            btnCancel.TabIndex = 55;
            btnCancel.Text = "Cancel";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(18, 509);
            btnDelete.Margin = new Padding(6, 5, 6, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(9, 10, 9, 10);
            btnDelete.Size = new Size(326, 45);
            btnDelete.TabIndex = 54;
            btnDelete.Text = "Delete";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(18, 454);
            btnSave.Margin = new Padding(6, 5, 6, 5);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(9, 10, 9, 10);
            btnSave.Size = new Size(326, 45);
            btnSave.TabIndex = 53;
            btnSave.Text = "Save";
            // 
            // frmEditor_Shop
            // 
            AutoScaleDimensions = new SizeF(10.0f, 25.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1063, 619);
            Controls.Add(btnCancel);
            Controls.Add(DarkGroupBox2);
            Controls.Add(btnDelete);
            Controls.Add(DarkGroupBox1);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(6, 5, 6, 5);
            MaximizeBox = false;
            Name = "frmEditor_Shop";
            Text = "Shop Editor";
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            DarkGroupBox3.ResumeLayout(false);
            DarkGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCostValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudItemValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBuy).EndInit();
            Load += new EventHandler(frmEditor_Shop_Load);
            FormClosing += new FormClosingEventHandler(frmEditor_Shop_FormClosing);
            ResumeLayout(false);

        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal DarkUI.Controls.DarkNumericUpDown nudBuy;
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
    }
}