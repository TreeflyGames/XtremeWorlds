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
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            nudLoopTime1 = new DarkUI.Controls.DarkNumericUpDown();
            nudFrameCount1 = new DarkUI.Controls.DarkNumericUpDown();
            nudLoopCount1 = new DarkUI.Controls.DarkNumericUpDown();
            nudSprite1 = new DarkUI.Controls.DarkNumericUpDown();
            lblLoopTime1 = new DarkUI.Controls.DarkLabel();
            lblFrameCount1 = new DarkUI.Controls.DarkLabel();
            lblLoopCount1 = new DarkUI.Controls.DarkLabel();
            lblSprite1 = new DarkUI.Controls.DarkLabel();
            picSprite1 = new PictureBox();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            nudLoopTime0 = new DarkUI.Controls.DarkNumericUpDown();
            nudFrameCount0 = new DarkUI.Controls.DarkNumericUpDown();
            nudLoopCount0 = new DarkUI.Controls.DarkNumericUpDown();
            nudSprite0 = new DarkUI.Controls.DarkNumericUpDown();
            lblLoopTime0 = new DarkUI.Controls.DarkLabel();
            lblFrameCount0 = new DarkUI.Controls.DarkLabel();
            lblLoopCount0 = new DarkUI.Controls.DarkLabel();
            lblSprite0 = new DarkUI.Controls.DarkLabel();
            picSprite0 = new PictureBox();
            txtName = new DarkUI.Controls.DarkTextBox();
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            btnSave = new DarkUI.Controls.DarkButton();
            btnDelete = new DarkUI.Controls.DarkButton();
            btnCancel = new DarkUI.Controls.DarkButton();
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
            DarkGroupBox1.Location = new Point(2, 3);
            DarkGroupBox1.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Size = new Size(233, 430);
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
            lstIndex.Location = new Point(7, 22);
            lstIndex.Margin = new Padding(4, 3, 4, 3);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(219, 390);
            lstIndex.TabIndex = 0;
            lstIndex.Click += lstIndex_Click;
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
            DarkGroupBox2.Location = new Point(243, 3);
            DarkGroupBox2.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Size = new Size(511, 535);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // cmbSound
            // 
            cmbSound.DrawMode = DrawMode.OwnerDrawVariable;
            cmbSound.FormattingEnabled = true;
            cmbSound.Location = new Point(136, 62);
            cmbSound.Margin = new Padding(4, 3, 4, 3);
            cmbSound.Name = "cmbSound";
            cmbSound.Size = new Size(181, 24);
            cmbSound.TabIndex = 25;
            cmbSound.SelectedIndexChanged += CmbSound_SelectedIndexChanged;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(12, 64);
            DarkLabel2.Margin = new Padding(4, 0, 4, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(44, 15);
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
            DarkGroupBox4.Location = new Point(7, 320);
            DarkGroupBox4.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Size = new Size(497, 208);
            DarkGroupBox4.TabIndex = 23;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Layer 1 - Above Player";
            // 
            // nudLoopTime1
            // 
            nudLoopTime1.Location = new Point(102, 160);
            nudLoopTime1.Margin = new Padding(4, 3, 4, 3);
            nudLoopTime1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudLoopTime1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopTime1.Name = "nudLoopTime1";
            nudLoopTime1.Size = new Size(140, 23);
            nudLoopTime1.TabIndex = 33;
            nudLoopTime1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopTime1.Click += NudLoopTime1_ValueChanged;
            // 
            // nudFrameCount1
            // 
            nudFrameCount1.Location = new Point(102, 115);
            nudFrameCount1.Margin = new Padding(4, 3, 4, 3);
            nudFrameCount1.Name = "nudFrameCount1";
            nudFrameCount1.Size = new Size(140, 23);
            nudFrameCount1.TabIndex = 32;
            nudFrameCount1.Click += NudFrameCount1_ValueChanged;
            // 
            // nudLoopCount1
            // 
            nudLoopCount1.Location = new Point(102, 70);
            nudLoopCount1.Margin = new Padding(4, 3, 4, 3);
            nudLoopCount1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopCount1.Name = "nudLoopCount1";
            nudLoopCount1.Size = new Size(140, 23);
            nudLoopCount1.TabIndex = 31;
            nudLoopCount1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopCount1.Click += NudLoopCount1_ValueChanged;
            // 
            // nudSprite1
            // 
            nudSprite1.Location = new Point(102, 28);
            nudSprite1.Margin = new Padding(4, 3, 4, 3);
            nudSprite1.Name = "nudSprite1";
            nudSprite1.Size = new Size(140, 23);
            nudSprite1.TabIndex = 30;
            nudSprite1.Click += NudSprite1_ValueChanged;
            // 
            // lblLoopTime1
            // 
            lblLoopTime1.AutoSize = true;
            lblLoopTime1.ForeColor = Color.FromArgb(220, 220, 220);
            lblLoopTime1.Location = new Point(12, 162);
            lblLoopTime1.Margin = new Padding(4, 0, 4, 0);
            lblLoopTime1.Name = "lblLoopTime1";
            lblLoopTime1.Size = new Size(66, 15);
            lblLoopTime1.TabIndex = 28;
            lblLoopTime1.Text = "Loop Time:";
            // 
            // lblFrameCount1
            // 
            lblFrameCount1.AutoSize = true;
            lblFrameCount1.ForeColor = Color.FromArgb(220, 220, 220);
            lblFrameCount1.Location = new Point(13, 117);
            lblFrameCount1.Margin = new Padding(4, 0, 4, 0);
            lblFrameCount1.Name = "lblFrameCount1";
            lblFrameCount1.Size = new Size(79, 15);
            lblFrameCount1.TabIndex = 26;
            lblFrameCount1.Text = "Frame Count:";
            // 
            // lblLoopCount1
            // 
            lblLoopCount1.AutoSize = true;
            lblLoopCount1.ForeColor = Color.FromArgb(220, 220, 220);
            lblLoopCount1.Location = new Point(13, 73);
            lblLoopCount1.Margin = new Padding(4, 0, 4, 0);
            lblLoopCount1.Name = "lblLoopCount1";
            lblLoopCount1.Size = new Size(73, 15);
            lblLoopCount1.TabIndex = 24;
            lblLoopCount1.Text = "Loop Count:";
            // 
            // lblSprite1
            // 
            lblSprite1.AutoSize = true;
            lblSprite1.ForeColor = Color.FromArgb(220, 220, 220);
            lblSprite1.Location = new Point(12, 30);
            lblSprite1.Margin = new Padding(4, 0, 4, 0);
            lblSprite1.Name = "lblSprite1";
            lblSprite1.Size = new Size(40, 15);
            lblSprite1.TabIndex = 22;
            lblSprite1.Text = "Sprite:";
            // 
            // picSprite1
            // 
            picSprite1.BackColor = Color.Black;
            picSprite1.Location = new Point(256, 12);
            picSprite1.Margin = new Padding(4, 3, 4, 3);
            picSprite1.Name = "picSprite1";
            picSprite1.Size = new Size(239, 190);
            picSprite1.TabIndex = 21;
            picSprite1.TabStop = false;
            picSprite1.Paint += picSprite1_Paint;
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
            DarkGroupBox3.Location = new Point(7, 106);
            DarkGroupBox3.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Size = new Size(497, 208);
            DarkGroupBox3.TabIndex = 22;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Layer 0 - Beneath Player";
            // 
            // nudLoopTime0
            // 
            nudLoopTime0.Location = new Point(102, 160);
            nudLoopTime0.Margin = new Padding(4, 3, 4, 3);
            nudLoopTime0.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudLoopTime0.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopTime0.Name = "nudLoopTime0";
            nudLoopTime0.Size = new Size(140, 23);
            nudLoopTime0.TabIndex = 33;
            nudLoopTime0.Value = new decimal(new int[] { 100, 0, 0, 0 });
            nudLoopTime0.Click += NudLoopTime0_ValueChanged;
            // 
            // nudFrameCount0
            // 
            nudFrameCount0.Location = new Point(102, 115);
            nudFrameCount0.Margin = new Padding(4, 3, 4, 3);
            nudFrameCount0.Name = "nudFrameCount0";
            nudFrameCount0.Size = new Size(140, 23);
            nudFrameCount0.TabIndex = 32;
            nudFrameCount0.Click += NudFrameCount0_ValueChanged;
            // 
            // nudLoopCount0
            // 
            nudLoopCount0.Location = new Point(102, 70);
            nudLoopCount0.Margin = new Padding(4, 3, 4, 3);
            nudLoopCount0.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopCount0.Name = "nudLoopCount0";
            nudLoopCount0.Size = new Size(140, 23);
            nudLoopCount0.TabIndex = 31;
            nudLoopCount0.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopCount0.Click += NudLoopCount0_ValueChanged;
            // 
            // nudSprite0
            // 
            nudSprite0.Location = new Point(102, 28);
            nudSprite0.Margin = new Padding(4, 3, 4, 3);
            nudSprite0.Name = "nudSprite0";
            nudSprite0.Size = new Size(140, 23);
            nudSprite0.TabIndex = 30;
            nudSprite0.Click += NudSprite0_ValueChanged;
            // 
            // lblLoopTime0
            // 
            lblLoopTime0.AutoSize = true;
            lblLoopTime0.ForeColor = Color.FromArgb(220, 220, 220);
            lblLoopTime0.Location = new Point(12, 162);
            lblLoopTime0.Margin = new Padding(4, 0, 4, 0);
            lblLoopTime0.Name = "lblLoopTime0";
            lblLoopTime0.Size = new Size(66, 15);
            lblLoopTime0.TabIndex = 28;
            lblLoopTime0.Text = "Loop Time:";
            // 
            // lblFrameCount0
            // 
            lblFrameCount0.AutoSize = true;
            lblFrameCount0.ForeColor = Color.FromArgb(220, 220, 220);
            lblFrameCount0.Location = new Point(13, 117);
            lblFrameCount0.Margin = new Padding(4, 0, 4, 0);
            lblFrameCount0.Name = "lblFrameCount0";
            lblFrameCount0.Size = new Size(79, 15);
            lblFrameCount0.TabIndex = 26;
            lblFrameCount0.Text = "Frame Count:";
            // 
            // lblLoopCount0
            // 
            lblLoopCount0.AutoSize = true;
            lblLoopCount0.ForeColor = Color.FromArgb(220, 220, 220);
            lblLoopCount0.Location = new Point(13, 73);
            lblLoopCount0.Margin = new Padding(4, 0, 4, 0);
            lblLoopCount0.Name = "lblLoopCount0";
            lblLoopCount0.Size = new Size(73, 15);
            lblLoopCount0.TabIndex = 24;
            lblLoopCount0.Text = "Loop Count:";
            // 
            // lblSprite0
            // 
            lblSprite0.AutoSize = true;
            lblSprite0.ForeColor = Color.FromArgb(220, 220, 220);
            lblSprite0.Location = new Point(12, 30);
            lblSprite0.Margin = new Padding(4, 0, 4, 0);
            lblSprite0.Name = "lblSprite0";
            lblSprite0.Size = new Size(40, 15);
            lblSprite0.TabIndex = 22;
            lblSprite0.Text = "Sprite:";
            // 
            // picSprite0
            // 
            picSprite0.BackColor = Color.Black;
            picSprite0.Location = new Point(256, 12);
            picSprite0.Margin = new Padding(4, 3, 4, 3);
            picSprite0.Name = "picSprite0";
            picSprite0.Size = new Size(239, 190);
            picSprite0.TabIndex = 21;
            picSprite0.TabStop = false;
            picSprite0.Paint += picSprite0_Paint;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(136, 32);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(367, 23);
            txtName.TabIndex = 1;
            txtName.TextChanged += TxtName_TextChanged;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(12, 33);
            DarkLabel1.Margin = new Padding(4, 0, 4, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(42, 15);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(9, 445);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(6, 5, 6, 5);
            btnSave.Size = new Size(219, 27);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.Click += BtnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(9, 478);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(6, 5, 6, 5);
            btnDelete.Size = new Size(219, 27);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(9, 511);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(219, 27);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.Click += BtnCancel_Click;
            // 
            // frmEditor_Animation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(758, 542);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmEditor_Animation";
            Text = "Animation Editor";
            FormClosing += frmEditor_Animation_FormClosing;
            Load += frmEditor_Animation_Load;
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