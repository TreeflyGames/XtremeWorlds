using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_Animation : Form
    {

        // Shared instance of the form
        private static frmEditor_Animation _instance;

        // Public property to get the shared instance
        public static frmEditor_Animation Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Animation();
                }
                return _instance;
            }
        }

        // Private constructor to prevent instantiation from outside
        private frmEditor_Animation()
        {
            InitializeComponent(); // Call to initialize the form's controls
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
            cmbSound = new DarkUI.Controls.DarkComboBox();
            cmbSound.SelectedIndexChanged += new EventHandler(CmbSound_SelectedIndexChanged);
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            nudLoopTime1 = new DarkUI.Controls.DarkNumericUpDown();
            nudLoopTime1.Click += new EventHandler(NudLoopTime1_ValueChanged);
            nudFrameCount1 = new DarkUI.Controls.DarkNumericUpDown();
            nudFrameCount1.Click += new EventHandler(NudFrameCount1_ValueChanged);
            nudLoopCount1 = new DarkUI.Controls.DarkNumericUpDown();
            nudLoopCount1.Click += new EventHandler(NudLoopCount1_ValueChanged);
            nudSprite1 = new DarkUI.Controls.DarkNumericUpDown();
            nudSprite1.Click += new EventHandler(NudSprite1_ValueChanged);
            lblLoopTime1 = new DarkUI.Controls.DarkLabel();
            lblFrameCount1 = new DarkUI.Controls.DarkLabel();
            lblLoopCount1 = new DarkUI.Controls.DarkLabel();
            lblSprite1 = new DarkUI.Controls.DarkLabel();
            picSprite1 = new PictureBox();
            picSprite1.Paint += new PaintEventHandler(picSprite1_Paint);
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            nudLoopTime0 = new DarkUI.Controls.DarkNumericUpDown();
            nudLoopTime0.Click += new EventHandler(NudLoopTime0_ValueChanged);
            nudFrameCount0 = new DarkUI.Controls.DarkNumericUpDown();
            nudFrameCount0.Click += new EventHandler(NudFrameCount0_ValueChanged);
            nudLoopCount0 = new DarkUI.Controls.DarkNumericUpDown();
            nudLoopCount0.Click += new EventHandler(NudLoopCount0_ValueChanged);
            nudSprite0 = new DarkUI.Controls.DarkNumericUpDown();
            nudSprite0.Click += new EventHandler(NudSprite0_ValueChanged);
            lblLoopTime0 = new DarkUI.Controls.DarkLabel();
            lblFrameCount0 = new DarkUI.Controls.DarkLabel();
            lblLoopCount0 = new DarkUI.Controls.DarkLabel();
            lblSprite0 = new DarkUI.Controls.DarkLabel();
            picSprite0 = new PictureBox();
            picSprite0.Paint += new PaintEventHandler(picSprite0_Paint);
            txtName = new DarkUI.Controls.DarkTextBox();
            txtName.TextChanged += new EventHandler(TxtName_TextChanged);
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            btnSave = new DarkUI.Controls.DarkButton();
            btnSave.Click += new EventHandler(BtnSave_Click);
            btnDelete = new DarkUI.Controls.DarkButton();
            btnDelete.Click += new EventHandler(BtnDelete_Click);
            btnCancel = new DarkUI.Controls.DarkButton();
            btnCancel.Click += new EventHandler(BtnCancel_Click);
            DarkGroupBox1.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            DarkGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLoopTime1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudFrameCount1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLoopCount1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSprite1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSprite1).BeginInit();
            DarkGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLoopTime0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudFrameCount0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLoopCount0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSprite0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSprite0).BeginInit();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(3, 5);
            DarkGroupBox1.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox1.Size = new Size(333, 716);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Animation List";
            // 
            // lstIndex
            // 
            lstIndex.BackColor = Color.FromArgb(45, 45, 48);
            lstIndex.BorderStyle = BorderStyle.None;
            lstIndex.ForeColor = Color.Gainsboro;
            lstIndex.FormattingEnabled = true;
            lstIndex.ItemHeight = 25;
            lstIndex.Location = new Point(10, 37);
            lstIndex.Margin = new Padding(6, 5, 6, 5);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(313, 650);
            lstIndex.TabIndex = 0;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(cmbSound);
            DarkGroupBox2.Controls.Add(DarkLabel2);
            DarkGroupBox2.Controls.Add(DarkGroupBox4);
            DarkGroupBox2.Controls.Add(DarkGroupBox3);
            DarkGroupBox2.Controls.Add(txtName);
            DarkGroupBox2.Controls.Add(DarkLabel1);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(347, 5);
            DarkGroupBox2.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Size = new Size(730, 891);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // cmbSound
            // 
            cmbSound.DrawMode = DrawMode.OwnerDrawVariable;
            cmbSound.FormattingEnabled = true;
            cmbSound.Location = new Point(194, 103);
            cmbSound.Margin = new Padding(6, 5, 6, 5);
            cmbSound.Name = "cmbSound";
            cmbSound.Size = new Size(257, 32);
            cmbSound.TabIndex = 25;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(17, 106);
            DarkLabel2.Margin = new Padding(6, 0, 6, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(68, 25);
            DarkLabel2.TabIndex = 24;
            DarkLabel2.Text = "Sound:";
            // 
            // DarkGroupBox4
            // 
            DarkGroupBox4.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox4.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox4.Controls.Add(nudLoopTime1);
            DarkGroupBox4.Controls.Add(nudFrameCount1);
            DarkGroupBox4.Controls.Add(nudLoopCount1);
            DarkGroupBox4.Controls.Add(nudSprite1);
            DarkGroupBox4.Controls.Add(lblLoopTime1);
            DarkGroupBox4.Controls.Add(lblFrameCount1);
            DarkGroupBox4.Controls.Add(lblLoopCount1);
            DarkGroupBox4.Controls.Add(lblSprite1);
            DarkGroupBox4.Controls.Add(picSprite1);
            DarkGroupBox4.ForeColor = Color.Gainsboro;
            DarkGroupBox4.Location = new Point(10, 534);
            DarkGroupBox4.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox4.Size = new Size(710, 347);
            DarkGroupBox4.TabIndex = 23;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Layer 1 - Above Player";
            // 
            // nudLoopTime1
            // 
            nudLoopTime1.Location = new Point(146, 266);
            nudLoopTime1.Margin = new Padding(6, 5, 6, 5);
            nudLoopTime1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudLoopTime1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopTime1.Name = "nudLoopTime1";
            nudLoopTime1.Size = new Size(200, 31);
            nudLoopTime1.TabIndex = 33;
            nudLoopTime1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nudFrameCount1
            // 
            nudFrameCount1.Location = new Point(146, 191);
            nudFrameCount1.Margin = new Padding(6, 5, 6, 5);
            nudFrameCount1.Name = "nudFrameCount1";
            nudFrameCount1.Size = new Size(200, 31);
            nudFrameCount1.TabIndex = 32;
            // 
            // nudLoopCount1
            // 
            nudLoopCount1.Location = new Point(146, 116);
            nudLoopCount1.Margin = new Padding(6, 5, 6, 5);
            nudLoopCount1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopCount1.Name = "nudLoopCount1";
            nudLoopCount1.Size = new Size(200, 31);
            nudLoopCount1.TabIndex = 31;
            nudLoopCount1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nudSprite1
            // 
            nudSprite1.Location = new Point(146, 47);
            nudSprite1.Margin = new Padding(6, 5, 6, 5);
            nudSprite1.Name = "nudSprite1";
            nudSprite1.Size = new Size(200, 31);
            nudSprite1.TabIndex = 30;
            // 
            // lblLoopTime1
            // 
            lblLoopTime1.AutoSize = true;
            lblLoopTime1.ForeColor = Color.FromArgb(220, 220, 220);
            lblLoopTime1.Location = new Point(17, 270);
            lblLoopTime1.Margin = new Padding(6, 0, 6, 0);
            lblLoopTime1.Name = "lblLoopTime1";
            lblLoopTime1.Size = new Size(100, 25);
            lblLoopTime1.TabIndex = 28;
            lblLoopTime1.Text = "Loop Time:";
            // 
            // lblFrameCount1
            // 
            lblFrameCount1.AutoSize = true;
            lblFrameCount1.ForeColor = Color.FromArgb(220, 220, 220);
            lblFrameCount1.Location = new Point(18, 195);
            lblFrameCount1.Margin = new Padding(6, 0, 6, 0);
            lblFrameCount1.Name = "lblFrameCount1";
            lblFrameCount1.Size = new Size(118, 25);
            lblFrameCount1.TabIndex = 26;
            lblFrameCount1.Text = "Frame Count:";
            // 
            // lblLoopCount1
            // 
            lblLoopCount1.AutoSize = true;
            lblLoopCount1.ForeColor = Color.FromArgb(220, 220, 220);
            lblLoopCount1.Location = new Point(18, 122);
            lblLoopCount1.Margin = new Padding(6, 0, 6, 0);
            lblLoopCount1.Name = "lblLoopCount1";
            lblLoopCount1.Size = new Size(110, 25);
            lblLoopCount1.TabIndex = 24;
            lblLoopCount1.Text = "Loop Count:";
            // 
            // lblSprite1
            // 
            lblSprite1.AutoSize = true;
            lblSprite1.ForeColor = Color.FromArgb(220, 220, 220);
            lblSprite1.Location = new Point(17, 50);
            lblSprite1.Margin = new Padding(6, 0, 6, 0);
            lblSprite1.Name = "lblSprite1";
            lblSprite1.Size = new Size(62, 25);
            lblSprite1.TabIndex = 22;
            lblSprite1.Text = "Sprite:";
            // 
            // picSprite1
            // 
            picSprite1.BackColor = Color.Black;
            picSprite1.Location = new Point(366, 20);
            picSprite1.Margin = new Padding(6, 5, 6, 5);
            picSprite1.Name = "picSprite1";
            picSprite1.Size = new Size(342, 316);
            picSprite1.TabIndex = 21;
            picSprite1.TabStop = false;
            // 
            // DarkGroupBox3
            // 
            DarkGroupBox3.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox3.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox3.Controls.Add(nudLoopTime0);
            DarkGroupBox3.Controls.Add(nudFrameCount0);
            DarkGroupBox3.Controls.Add(nudLoopCount0);
            DarkGroupBox3.Controls.Add(nudSprite0);
            DarkGroupBox3.Controls.Add(lblLoopTime0);
            DarkGroupBox3.Controls.Add(lblFrameCount0);
            DarkGroupBox3.Controls.Add(lblLoopCount0);
            DarkGroupBox3.Controls.Add(lblSprite0);
            DarkGroupBox3.Controls.Add(picSprite0);
            DarkGroupBox3.ForeColor = Color.Gainsboro;
            DarkGroupBox3.Location = new Point(10, 177);
            DarkGroupBox3.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox3.Size = new Size(710, 347);
            DarkGroupBox3.TabIndex = 22;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Layer 0 - Beneath Player";
            // 
            // nudLoopTime0
            // 
            nudLoopTime0.Location = new Point(146, 266);
            nudLoopTime0.Margin = new Padding(6, 5, 6, 5);
            nudLoopTime0.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudLoopTime0.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopTime0.Name = "nudLoopTime0";
            nudLoopTime0.Size = new Size(200, 31);
            nudLoopTime0.TabIndex = 33;
            nudLoopTime0.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // nudFrameCount0
            // 
            nudFrameCount0.Location = new Point(146, 191);
            nudFrameCount0.Margin = new Padding(6, 5, 6, 5);
            nudFrameCount0.Name = "nudFrameCount0";
            nudFrameCount0.Size = new Size(200, 31);
            nudFrameCount0.TabIndex = 32;
            // 
            // nudLoopCount0
            // 
            nudLoopCount0.Location = new Point(146, 116);
            nudLoopCount0.Margin = new Padding(6, 5, 6, 5);
            nudLoopCount0.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopCount0.Name = "nudLoopCount0";
            nudLoopCount0.Size = new Size(200, 31);
            nudLoopCount0.TabIndex = 31;
            nudLoopCount0.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nudSprite0
            // 
            nudSprite0.Location = new Point(146, 47);
            nudSprite0.Margin = new Padding(6, 5, 6, 5);
            nudSprite0.Name = "nudSprite0";
            nudSprite0.Size = new Size(200, 31);
            nudSprite0.TabIndex = 30;
            // 
            // lblLoopTime0
            // 
            lblLoopTime0.AutoSize = true;
            lblLoopTime0.ForeColor = Color.FromArgb(220, 220, 220);
            lblLoopTime0.Location = new Point(17, 270);
            lblLoopTime0.Margin = new Padding(6, 0, 6, 0);
            lblLoopTime0.Name = "lblLoopTime0";
            lblLoopTime0.Size = new Size(100, 25);
            lblLoopTime0.TabIndex = 28;
            lblLoopTime0.Text = "Loop Time:";
            // 
            // lblFrameCount0
            // 
            lblFrameCount0.AutoSize = true;
            lblFrameCount0.ForeColor = Color.FromArgb(220, 220, 220);
            lblFrameCount0.Location = new Point(18, 195);
            lblFrameCount0.Margin = new Padding(6, 0, 6, 0);
            lblFrameCount0.Name = "lblFrameCount0";
            lblFrameCount0.Size = new Size(118, 25);
            lblFrameCount0.TabIndex = 26;
            lblFrameCount0.Text = "Frame Count:";
            // 
            // lblLoopCount0
            // 
            lblLoopCount0.AutoSize = true;
            lblLoopCount0.ForeColor = Color.FromArgb(220, 220, 220);
            lblLoopCount0.Location = new Point(18, 122);
            lblLoopCount0.Margin = new Padding(6, 0, 6, 0);
            lblLoopCount0.Name = "lblLoopCount0";
            lblLoopCount0.Size = new Size(110, 25);
            lblLoopCount0.TabIndex = 24;
            lblLoopCount0.Text = "Loop Count:";
            // 
            // lblSprite0
            // 
            lblSprite0.AutoSize = true;
            lblSprite0.ForeColor = Color.FromArgb(220, 220, 220);
            lblSprite0.Location = new Point(17, 50);
            lblSprite0.Margin = new Padding(6, 0, 6, 0);
            lblSprite0.Name = "lblSprite0";
            lblSprite0.Size = new Size(62, 25);
            lblSprite0.TabIndex = 22;
            lblSprite0.Text = "Sprite:";
            // 
            // picSprite0
            // 
            picSprite0.BackColor = Color.Black;
            picSprite0.Location = new Point(366, 20);
            picSprite0.Margin = new Padding(6, 5, 6, 5);
            picSprite0.Name = "picSprite0";
            picSprite0.Size = new Size(342, 316);
            picSprite0.TabIndex = 21;
            picSprite0.TabStop = false;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(194, 53);
            txtName.Margin = new Padding(6, 5, 6, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(524, 31);
            txtName.TabIndex = 1;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(17, 55);
            DarkLabel1.Margin = new Padding(6, 0, 6, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(63, 25);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(13, 741);
            btnSave.Margin = new Padding(6, 5, 6, 5);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(8, 9, 8, 9);
            btnSave.Size = new Size(313, 45);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(13, 796);
            btnDelete.Margin = new Padding(6, 5, 6, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(8, 9, 8, 9);
            btnDelete.Size = new Size(313, 45);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(13, 851);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(8, 9, 8, 9);
            btnCancel.Size = new Size(313, 45);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            // 
            // frmEditor_Animation
            // 
            AutoScaleDimensions = new SizeF(10.0f, 25.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1083, 903);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(6, 5, 6, 5);
            Name = "frmEditor_Animation";
            Text = "Animation Editor";
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            DarkGroupBox4.ResumeLayout(false);
            DarkGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLoopTime1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudFrameCount1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLoopCount1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSprite1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSprite1).EndInit();
            DarkGroupBox3.ResumeLayout(false);
            DarkGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLoopTime0).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudFrameCount0).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLoopCount0).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSprite0).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSprite0).EndInit();
            Load += new EventHandler(frmEditor_Animation_Load);
            FormClosing += new FormClosingEventHandler(frmEditor_Animation_FormClosing);
            ResumeLayout(false);

        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox3;
        internal PictureBox picSprite0;
        internal DarkUI.Controls.DarkLabel lblLoopCount0;
        internal DarkUI.Controls.DarkLabel lblSprite0;
        internal DarkUI.Controls.DarkLabel lblLoopTime0;
        internal DarkUI.Controls.DarkLabel lblFrameCount0;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox4;
        internal DarkUI.Controls.DarkLabel lblLoopTime1;
        internal DarkUI.Controls.DarkLabel lblFrameCount1;
        internal DarkUI.Controls.DarkLabel lblLoopCount1;
        internal DarkUI.Controls.DarkLabel lblSprite1;
        internal PictureBox picSprite1;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkButton btnDelete;
        internal DarkUI.Controls.DarkButton btnCancel;
        internal DarkUI.Controls.DarkNumericUpDown nudLoopTime0;
        internal DarkUI.Controls.DarkNumericUpDown nudFrameCount0;
        internal DarkUI.Controls.DarkNumericUpDown nudLoopCount0;
        internal DarkUI.Controls.DarkNumericUpDown nudSprite0;
        internal DarkUI.Controls.DarkNumericUpDown nudLoopTime1;
        internal DarkUI.Controls.DarkNumericUpDown nudFrameCount1;
        internal DarkUI.Controls.DarkNumericUpDown nudLoopCount1;
        internal DarkUI.Controls.DarkNumericUpDown nudSprite1;
        internal DarkUI.Controls.DarkComboBox cmbSound;
        internal DarkUI.Controls.DarkLabel DarkLabel2;
    }
}