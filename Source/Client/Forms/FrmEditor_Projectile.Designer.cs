using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_Projectile : Form
    {

        // Shared instance of the form
        private static frmEditor_Projectile _instance;

        // Public property to get the shared instance
        public static frmEditor_Projectile Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Projectile();
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
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            nudDamage = new DarkUI.Controls.DarkNumericUpDown();
            nudDamage.Click += new EventHandler(NudDamage_ValueChanged);
            nudSpeed = new DarkUI.Controls.DarkNumericUpDown();
            nudSpeed.Click += new EventHandler(NudSpeed_ValueChanged);
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            nudRange = new DarkUI.Controls.DarkNumericUpDown();
            nudRange.Click += new EventHandler(NudRange_ValueChanged);
            nudPic = new DarkUI.Controls.DarkNumericUpDown();
            nudPic.Click += new EventHandler(NudPic_ValueChanged);
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            picProjectile = new PictureBox();
            txtName = new DarkUI.Controls.DarkTextBox();
            txtName.TextChanged += new EventHandler(TxtName_TextChanged);
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            btnSave = new DarkUI.Controls.DarkButton();
            btnSave.Click += new EventHandler(BtnSave_Click);
            btnCancel = new DarkUI.Controls.DarkButton();
            btnCancel.Click += new EventHandler(BtnCancel_Click);
            btnDelete = new DarkUI.Controls.DarkButton();
            btnDelete.Click += new EventHandler(btnDelete_Click);
            DarkGroupBox1.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDamage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picProjectile).BeginInit();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(8, 6);
            DarkGroupBox1.Margin = new Padding(8, 6, 8, 6);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(8, 6, 8, 6);
            DarkGroupBox1.Size = new Size(407, 460);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Projectile List";
            // 
            // lstIndex
            // 
            lstIndex.BackColor = Color.FromArgb(45, 45, 48);
            lstIndex.BorderStyle = BorderStyle.FixedSingle;
            lstIndex.ForeColor = Color.Gainsboro;
            lstIndex.FormattingEnabled = true;
            lstIndex.Location = new Point(13, 43);
            lstIndex.Margin = new Padding(8, 6, 8, 6);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(379, 386);
            lstIndex.TabIndex = 1;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(DarkLabel5);
            DarkGroupBox2.Controls.Add(DarkLabel4);
            DarkGroupBox2.Controls.Add(nudDamage);
            DarkGroupBox2.Controls.Add(nudSpeed);
            DarkGroupBox2.Controls.Add(DarkLabel3);
            DarkGroupBox2.Controls.Add(nudRange);
            DarkGroupBox2.Controls.Add(nudPic);
            DarkGroupBox2.Controls.Add(DarkLabel2);
            DarkGroupBox2.Controls.Add(picProjectile);
            DarkGroupBox2.Controls.Add(txtName);
            DarkGroupBox2.Controls.Add(DarkLabel1);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(427, 6);
            DarkGroupBox2.Margin = new Padding(8, 6, 8, 6);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(8, 6, 8, 6);
            DarkGroupBox2.Size = new Size(538, 672);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(13, 480);
            DarkLabel5.Margin = new Padding(8, 0, 8, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(224, 32);
            DarkLabel5.TabIndex = 11;
            DarkLabel5.Text = "Additional Damage:";
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(13, 416);
            DarkLabel4.Margin = new Padding(8, 0, 8, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(86, 32);
            DarkLabel4.TabIndex = 10;
            DarkLabel4.Text = "Speed:";
            // 
            // nudDamage
            // 
            nudDamage.Location = new Point(258, 476);
            nudDamage.Margin = new Padding(8, 6, 8, 6);
            nudDamage.Name = "nudDamage";
            nudDamage.Size = new Size(260, 39);
            nudDamage.TabIndex = 9;
            // 
            // nudSpeed
            // 
            nudSpeed.Location = new Point(258, 412);
            nudSpeed.Margin = new Padding(8, 6, 8, 6);
            nudSpeed.Name = "nudSpeed";
            nudSpeed.Size = new Size(260, 39);
            nudSpeed.TabIndex = 8;
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(13, 352);
            DarkLabel3.Margin = new Padding(8, 0, 8, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(86, 32);
            DarkLabel3.TabIndex = 7;
            DarkLabel3.Text = "Range:";
            // 
            // nudRange
            // 
            nudRange.Location = new Point(258, 348);
            nudRange.Margin = new Padding(8, 6, 8, 6);
            nudRange.Name = "nudRange";
            nudRange.Size = new Size(260, 39);
            nudRange.TabIndex = 6;
            // 
            // nudPic
            // 
            nudPic.Location = new Point(258, 284);
            nudPic.Margin = new Padding(8, 6, 8, 6);
            nudPic.Name = "nudPic";
            nudPic.Size = new Size(260, 39);
            nudPic.TabIndex = 5;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(13, 288);
            DarkLabel2.Margin = new Padding(8, 0, 8, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(92, 32);
            DarkLabel2.TabIndex = 4;
            DarkLabel2.Text = "Picture:";
            // 
            // picProjectile
            // 
            picProjectile.BackColor = Color.Black;
            picProjectile.Location = new Point(18, 111);
            picProjectile.Margin = new Padding(8, 6, 8, 6);
            picProjectile.Name = "picProjectile";
            picProjectile.Size = new Size(498, 158);
            picProjectile.TabIndex = 3;
            picProjectile.TabStop = false;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(208, 47);
            txtName.Margin = new Padding(8, 6, 8, 6);
            txtName.Name = "txtName";
            txtName.Size = new Size(306, 39);
            txtName.TabIndex = 1;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(13, 52);
            DarkLabel1.Margin = new Padding(8, 0, 8, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(83, 32);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(21, 480);
            btnSave.Margin = new Padding(8, 6, 8, 6);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(11, 12, 11, 12);
            btnSave.Size = new Size(381, 58);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(21, 620);
            btnCancel.Margin = new Padding(8, 6, 8, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(11, 12, 11, 12);
            btnCancel.Size = new Size(381, 58);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(21, 550);
            btnDelete.Margin = new Padding(8, 6, 8, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(11, 12, 11, 12);
            btnDelete.Size = new Size(381, 58);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            // 
            // frmEditor_Projectile
            // 
            AutoScaleDimensions = new SizeF(13.0f, 32.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(979, 689);
            Controls.Add(btnDelete);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(8, 6, 8, 6);
            Name = "frmEditor_Projectile";
            Text = "Projectile Editor";
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDamage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRange).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPic).EndInit();
            ((System.ComponentModel.ISupportInitialize)picProjectile).EndInit();
            Load += new EventHandler(frmEditor_Projectile_Load);
            FormClosing += new FormClosingEventHandler(frmEditor_Projectile_FormClosing);
            ResumeLayout(false);

        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal PictureBox picProjectile;
        internal DarkUI.Controls.DarkNumericUpDown nudRange;
        internal DarkUI.Controls.DarkNumericUpDown nudPic;
        internal DarkUI.Controls.DarkLabel DarkLabel2;
        internal DarkUI.Controls.DarkLabel DarkLabel3;
        internal DarkUI.Controls.DarkNumericUpDown nudDamage;
        internal DarkUI.Controls.DarkNumericUpDown nudSpeed;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal DarkUI.Controls.DarkLabel DarkLabel5;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkButton btnCancel;
        internal DarkUI.Controls.DarkButton btnDelete;
    }
}