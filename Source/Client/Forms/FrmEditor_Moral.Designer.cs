using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmEditor_Moral : Form
    {

        // Shared instance of the form
        private static frmEditor_Moral _instance;

        // Public property to get the shared instance
        public static frmEditor_Moral Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Moral();
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
            btnDelete = new DarkUI.Controls.DarkButton();
            btnDelete.Click += new EventHandler(BtnDelete_Click);
            btnSave = new DarkUI.Controls.DarkButton();
            btnSave.Click += new EventHandler(BtnSave_Click);
            btnCancel = new DarkUI.Controls.DarkButton();
            btnCancel.Click += new EventHandler(BtnCancel_Click);
            lstIndex = new ListBox();
            lstIndex.Click += new EventHandler(lstIndex_Click);
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            txtName = new DarkUI.Controls.DarkTextBox();
            txtName.TextChanged += new EventHandler(txtName_TextChanged);
            DarkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            chkNpcBlock = new DarkUI.Controls.DarkCheckBox();
            chkNpcBlock.CheckedChanged += new EventHandler(chkNpcBlock_CheckedChanged);
            chkPlayerBlock = new DarkUI.Controls.DarkCheckBox();
            chkPlayerBlock.CheckedChanged += new EventHandler(chkPlayerBlock_CheckedChanged);
            chkLoseExp = new DarkUI.Controls.DarkCheckBox();
            chkLoseExp.CheckedChanged += new EventHandler(chkLoseExp_CheckedChanged);
            chkDropItems = new DarkUI.Controls.DarkCheckBox();
            chkDropItems.CheckedChanged += new EventHandler(chkDropItems_CheckedChanged);
            chkCanUseItem = new DarkUI.Controls.DarkCheckBox();
            chkCanUseItem.CheckedChanged += new EventHandler(chkCanUseItem_CheckedChanged);
            chkCanDropItem = new DarkUI.Controls.DarkCheckBox();
            chkCanDropItem.CheckedChanged += new EventHandler(chkCanDropItem_CheckedChanged);
            chkCanPickupItem = new DarkUI.Controls.DarkCheckBox();
            chkCanPickupItem.CheckedChanged += new EventHandler(chkCanPickupItem_CheckedChanged);
            chkCanPK = new DarkUI.Controls.DarkCheckBox();
            chkCanPK.CheckedChanged += new EventHandler(chkCanPK_CheckedChanged);
            chkCanCast = new DarkUI.Controls.DarkCheckBox();
            chkCanCast.CheckedChanged += new EventHandler(chkCanCast_CheckedChanged);
            cmbColor = new DarkUI.Controls.DarkComboBox();
            cmbColor.SelectedIndexChanged += new EventHandler(cmbColor_SelectedIndexChanged);
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox1.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(btnDelete);
            DarkGroupBox1.Controls.Add(btnSave);
            DarkGroupBox1.Controls.Add(btnCancel);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(3, 0);
            DarkGroupBox1.Margin = new Padding(5);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(5);
            DarkGroupBox1.Size = new Size(288, 486);
            DarkGroupBox1.TabIndex = 1;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Moral List";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(10, 376);
            btnDelete.Margin = new Padding(5);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(8, 10, 8, 10);
            btnDelete.Size = new Size(265, 45);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(10, 320);
            btnSave.Margin = new Padding(5);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(8, 10, 8, 10);
            btnSave.Size = new Size(265, 45);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(10, 431);
            btnCancel.Margin = new Padding(5);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(8, 10, 8, 10);
            btnCancel.Size = new Size(265, 45);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            // 
            // lstIndex
            // 
            lstIndex.BackColor = Color.FromArgb(45, 45, 48);
            lstIndex.BorderStyle = BorderStyle.FixedSingle;
            lstIndex.ForeColor = Color.Gainsboro;
            lstIndex.FormattingEnabled = true;
            lstIndex.ItemHeight = 25;
            lstIndex.Location = new Point(10, 33);
            lstIndex.Margin = new Padding(5);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(265, 277);
            lstIndex.TabIndex = 1;
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(13, 43);
            DarkLabel6.Margin = new Padding(6, 0, 6, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(63, 25);
            DarkLabel6.TabIndex = 0;
            DarkLabel6.Text = "Name:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(100, 37);
            txtName.Margin = new Padding(6, 5, 6, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(229, 31);
            txtName.TabIndex = 1;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(chkNpcBlock);
            DarkGroupBox2.Controls.Add(chkPlayerBlock);
            DarkGroupBox2.Controls.Add(chkLoseExp);
            DarkGroupBox2.Controls.Add(chkDropItems);
            DarkGroupBox2.Controls.Add(chkCanUseItem);
            DarkGroupBox2.Controls.Add(chkCanDropItem);
            DarkGroupBox2.Controls.Add(chkCanPickupItem);
            DarkGroupBox2.Controls.Add(chkCanPK);
            DarkGroupBox2.Controls.Add(chkCanCast);
            DarkGroupBox2.Controls.Add(cmbColor);
            DarkGroupBox2.Controls.Add(DarkLabel11);
            DarkGroupBox2.Controls.Add(txtName);
            DarkGroupBox2.Controls.Add(DarkLabel6);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(302, 0);
            DarkGroupBox2.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Size = new Size(350, 486);
            DarkGroupBox2.TabIndex = 31;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // chkNpcBlock
            // 
            chkNpcBlock.AutoSize = true;
            chkNpcBlock.Location = new Point(201, 237);
            chkNpcBlock.Margin = new Padding(6, 5, 6, 5);
            chkNpcBlock.Name = "chkNpcBlock";
            chkNpcBlock.Size = new Size(119, 29);
            chkNpcBlock.TabIndex = 20;
            chkNpcBlock.Text = "Npc Block";
            // 
            // chkPlayerBlock
            // 
            chkPlayerBlock.AutoSize = true;
            chkPlayerBlock.Location = new Point(201, 198);
            chkPlayerBlock.Margin = new Padding(6, 5, 6, 5);
            chkPlayerBlock.Name = "chkPlayerBlock";
            chkPlayerBlock.Size = new Size(132, 29);
            chkPlayerBlock.TabIndex = 19;
            chkPlayerBlock.Text = "Player Block";
            // 
            // chkLoseExp
            // 
            chkLoseExp.AutoSize = true;
            chkLoseExp.Location = new Point(201, 159);
            chkLoseExp.Margin = new Padding(6, 5, 6, 5);
            chkLoseExp.Name = "chkLoseExp";
            chkLoseExp.Size = new Size(107, 29);
            chkLoseExp.TabIndex = 18;
            chkLoseExp.Text = "Lose Exp";
            // 
            // chkDropItems
            // 
            chkDropItems.AutoSize = true;
            chkDropItems.Location = new Point(201, 120);
            chkDropItems.Margin = new Padding(6, 5, 6, 5);
            chkDropItems.Name = "chkDropItems";
            chkDropItems.Size = new Size(128, 29);
            chkDropItems.TabIndex = 17;
            chkDropItems.Text = "Drop Items";
            // 
            // chkCanUseItem
            // 
            chkCanUseItem.AutoSize = true;
            chkCanUseItem.Location = new Point(13, 276);
            chkCanUseItem.Margin = new Padding(6, 5, 6, 5);
            chkCanUseItem.Name = "chkCanUseItem";
            chkCanUseItem.Size = new Size(143, 29);
            chkCanUseItem.TabIndex = 16;
            chkCanUseItem.Text = "Can Use Item";
            // 
            // chkCanDropItem
            // 
            chkCanDropItem.AutoSize = true;
            chkCanDropItem.Location = new Point(13, 237);
            chkCanDropItem.Margin = new Padding(6, 5, 6, 5);
            chkCanDropItem.Name = "chkCanDropItem";
            chkCanDropItem.Size = new Size(155, 29);
            chkCanDropItem.TabIndex = 15;
            chkCanDropItem.Text = "Can Drop Item";
            // 
            // chkCanPickupItem
            // 
            chkCanPickupItem.AutoSize = true;
            chkCanPickupItem.Location = new Point(13, 198);
            chkCanPickupItem.Margin = new Padding(6, 5, 6, 5);
            chkCanPickupItem.Name = "chkCanPickupItem";
            chkCanPickupItem.Size = new Size(166, 29);
            chkCanPickupItem.TabIndex = 14;
            chkCanPickupItem.Text = "Can Pickup Item";
            // 
            // chkCanPK
            // 
            chkCanPK.AutoSize = true;
            chkCanPK.Location = new Point(13, 159);
            chkCanPK.Margin = new Padding(6, 5, 6, 5);
            chkCanPK.Name = "chkCanPK";
            chkCanPK.Size = new Size(93, 29);
            chkCanPK.TabIndex = 13;
            chkCanPK.Text = "Can PK";
            // 
            // chkCanCast
            // 
            chkCanCast.AutoSize = true;
            chkCanCast.Location = new Point(13, 120);
            chkCanCast.Margin = new Padding(6, 5, 6, 5);
            chkCanCast.Name = "chkCanCast";
            chkCanCast.Size = new Size(107, 29);
            chkCanCast.TabIndex = 12;
            chkCanCast.Text = "Can Cast";
            // 
            // cmbColor
            // 
            cmbColor.DrawMode = DrawMode.OwnerDrawFixed;
            cmbColor.FormattingEnabled = true;
            cmbColor.Items.AddRange(new object[] { "Black", "Blue", "Green", "Cyan", "Red", "Magenta", "Brown", "Gray", "DarkGray", "Bright Blue", "Bright Green", "Bright Cyan", "Bright Red", "Pink", "Yellow", "White" });
            cmbColor.Location = new Point(100, 78);
            cmbColor.Margin = new Padding(5);
            cmbColor.Name = "cmbColor";
            cmbColor.Size = new Size(229, 32);
            cmbColor.TabIndex = 11;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(13, 81);
            DarkLabel11.Margin = new Padding(5, 0, 5, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(59, 25);
            DarkLabel11.TabIndex = 10;
            DarkLabel11.Text = "Color:";
            // 
            // frmEditor_Moral
            // 
            AutoScaleDimensions = new SizeF(10.0f, 25.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(660, 491);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmEditor_Moral";
            Text = "Moral Editor";
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            FormClosing += new FormClosingEventHandler(frmEditor_Moral_FormClosing);
            Load += new EventHandler(frmEditor_Moral_Load);
            ResumeLayout(false);
        }
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkLabel DarkLabel6;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkComboBox cmbColor;
        internal DarkUI.Controls.DarkLabel DarkLabel11;
        internal DarkUI.Controls.DarkCheckBox chkCanPickupItem;
        internal DarkUI.Controls.DarkCheckBox chkCanPK;
        internal DarkUI.Controls.DarkCheckBox chkCanCast;
        internal DarkUI.Controls.DarkCheckBox chkNpcBlock;
        internal DarkUI.Controls.DarkCheckBox chkPlayerBlock;
        internal DarkUI.Controls.DarkCheckBox chkLoseExp;
        internal DarkUI.Controls.DarkCheckBox chkDropItems;
        internal DarkUI.Controls.DarkCheckBox chkCanUseItem;
        internal DarkUI.Controls.DarkCheckBox chkCanDropItem;
        internal DarkUI.Controls.DarkButton btnDelete;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkButton btnCancel;
    }
}