using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_Skill : Form
    {

        // Shared instance of the form
        private static frmEditor_Skill _instance;

        // Public property to get the shared instance
        public static frmEditor_Skill Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Skill();
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
            btnLearn = new DarkUI.Controls.DarkButton();
            btnLearn.Click += new EventHandler(btnLearn_Click);
            DarkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            DarkGroupBox8 = new DarkUI.Controls.DarkGroupBox();
            cmbAnim = new DarkUI.Controls.DarkComboBox();
            cmbAnim.SelectedIndexChanged += new EventHandler(CmbAnim_Scroll);
            DarkLabel23 = new DarkUI.Controls.DarkLabel();
            cmbAnimCast = new DarkUI.Controls.DarkComboBox();
            cmbAnimCast.SelectedIndexChanged += new EventHandler(CmbAnimCast_Scroll);
            DarkLabel22 = new DarkUI.Controls.DarkLabel();
            nudStun = new DarkUI.Controls.DarkNumericUpDown();
            nudStun.ValueChanged += new EventHandler(NudStun_Scroll);
            DarkLabel21 = new DarkUI.Controls.DarkLabel();
            DarkLabel20 = new DarkUI.Controls.DarkLabel();
            nudAoE = new DarkUI.Controls.DarkNumericUpDown();
            nudAoE.ValueChanged += new EventHandler(NudAoE_Scroll);
            DarkLabel19 = new DarkUI.Controls.DarkLabel();
            chkAoE = new DarkUI.Controls.DarkCheckBox();
            chkAoE.CheckedChanged += new EventHandler(ChkAOE_CheckedChanged);
            DarkLabel18 = new DarkUI.Controls.DarkLabel();
            nudRange = new DarkUI.Controls.DarkNumericUpDown();
            nudRange.ValueChanged += new EventHandler(NudRange_Scroll);
            DarkLabel17 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox7 = new DarkUI.Controls.DarkGroupBox();
            nudInterval = new DarkUI.Controls.DarkNumericUpDown();
            nudInterval.ValueChanged += new EventHandler(NudInterval_Scroll);
            DarkLabel16 = new DarkUI.Controls.DarkLabel();
            nudDuration = new DarkUI.Controls.DarkNumericUpDown();
            nudDuration.ValueChanged += new EventHandler(NudDuration_Scroll);
            DarkLabel15 = new DarkUI.Controls.DarkLabel();
            nudVital = new DarkUI.Controls.DarkNumericUpDown();
            nudVital.ValueChanged += new EventHandler(NudVital_Scroll);
            DarkLabel14 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox6 = new DarkUI.Controls.DarkGroupBox();
            nudY = new DarkUI.Controls.DarkNumericUpDown();
            nudY.ValueChanged += new EventHandler(NudY_Scroll);
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            nudX = new DarkUI.Controls.DarkNumericUpDown();
            nudX.ValueChanged += new EventHandler(NudX_Scroll);
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            cmbDir = new DarkUI.Controls.DarkComboBox();
            cmbDir.SelectedIndexChanged += new EventHandler(CmbDir_SelectedIndexChanged);
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            nudMap = new DarkUI.Controls.DarkNumericUpDown();
            nudMap.ValueChanged += new EventHandler(NudMap_Scroll);
            DarkLabel10 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            chkKnockBack = new DarkUI.Controls.DarkCheckBox();
            chkKnockBack.CheckedChanged += new EventHandler(ChkKnockBack_CheckedChanged);
            cmbKnockBackTiles = new DarkUI.Controls.DarkComboBox();
            cmbKnockBackTiles.SelectedIndexChanged += new EventHandler(CmbKnockBackTiles_SelectedIndexChanged);
            cmbProjectile = new DarkUI.Controls.DarkComboBox();
            cmbProjectile.SelectedIndexChanged += new EventHandler(ScrlProjectile_Scroll);
            chkProjectile = new DarkUI.Controls.DarkCheckBox();
            chkProjectile.CheckedChanged += new EventHandler(ChkProjectile_CheckedChanged);
            nudIcon = new DarkUI.Controls.DarkNumericUpDown();
            nudIcon.Click += new EventHandler(nudIcon_Click);
            DarkLabel9 = new DarkUI.Controls.DarkLabel();
            picSprite = new PictureBox();
            nudCool = new DarkUI.Controls.DarkNumericUpDown();
            nudCool.ValueChanged += new EventHandler(NudCool_Scroll);
            DarkLabel8 = new DarkUI.Controls.DarkLabel();
            nudCast = new DarkUI.Controls.DarkNumericUpDown();
            nudCast.ValueChanged += new EventHandler(NudCast_Scroll);
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            cmbJob = new DarkUI.Controls.DarkComboBox();
            cmbJob.SelectedIndexChanged += new EventHandler(CmbClass_SelectedIndexChanged);
            cmbAccessReq = new DarkUI.Controls.DarkComboBox();
            cmbAccessReq.SelectedIndexChanged += new EventHandler(CmbAccessReq_SelectedIndexChanged);
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            nudLevel = new DarkUI.Controls.DarkNumericUpDown();
            nudLevel.ValueChanged += new EventHandler(NudLevel_ValueChanged);
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            nudMp = new DarkUI.Controls.DarkNumericUpDown();
            nudMp.ValueChanged += new EventHandler(NudMp_ValueChanged);
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            cmbType = new DarkUI.Controls.DarkComboBox();
            cmbType.SelectedIndexChanged += new EventHandler(CmbType_SelectedIndexChanged);
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            txtName = new DarkUI.Controls.DarkTextBox();
            txtName.TextChanged += new EventHandler(TxtName_TextChanged);
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            btnDelete = new DarkUI.Controls.DarkButton();
            btnDelete.Click += new EventHandler(BtnDelete_Click);
            btnCancel = new DarkUI.Controls.DarkButton();
            btnCancel.Click += new EventHandler(BtnCancel_Click);
            btnSave = new DarkUI.Controls.DarkButton();
            btnSave.Click += new EventHandler(BtnSave_Click);
            DarkGroupBox1.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            DarkGroupBox5.SuspendLayout();
            DarkGroupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudStun).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAoE).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRange).BeginInit();
            DarkGroupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudInterval).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVital).BeginInit();
            DarkGroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMap).BeginInit();
            DarkGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCool).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCast).BeginInit();
            DarkGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMp).BeginInit();
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
            DarkGroupBox1.Size = new Size(306, 595);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Skill List";
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
            lstIndex.Size = new Size(284, 552);
            lstIndex.TabIndex = 1;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(btnLearn);
            DarkGroupBox2.Controls.Add(DarkGroupBox5);
            DarkGroupBox2.Controls.Add(DarkGroupBox3);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(320, 5);
            DarkGroupBox2.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Size = new Size(1032, 759);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // btnLearn
            // 
            btnLearn.Location = new Point(434, 680);
            btnLearn.Margin = new Padding(5);
            btnLearn.Name = "btnLearn";
            btnLearn.Padding = new Padding(8, 10, 8, 10);
            btnLearn.Size = new Size(125, 45);
            btnLearn.TabIndex = 11;
            btnLearn.Text = "Learn";
            // 
            // DarkGroupBox5
            // 
            DarkGroupBox5.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox5.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox5.Controls.Add(DarkGroupBox8);
            DarkGroupBox5.Controls.Add(DarkGroupBox7);
            DarkGroupBox5.Controls.Add(DarkGroupBox6);
            DarkGroupBox5.ForeColor = Color.Gainsboro;
            DarkGroupBox5.Location = new Point(570, 37);
            DarkGroupBox5.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox5.Name = "DarkGroupBox5";
            DarkGroupBox5.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox5.Size = new Size(446, 705);
            DarkGroupBox5.TabIndex = 1;
            DarkGroupBox5.TabStop = false;
            DarkGroupBox5.Text = "Data";
            // 
            // DarkGroupBox8
            // 
            DarkGroupBox8.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox8.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox8.Controls.Add(cmbAnim);
            DarkGroupBox8.Controls.Add(DarkLabel23);
            DarkGroupBox8.Controls.Add(cmbAnimCast);
            DarkGroupBox8.Controls.Add(DarkLabel22);
            DarkGroupBox8.Controls.Add(nudStun);
            DarkGroupBox8.Controls.Add(DarkLabel21);
            DarkGroupBox8.Controls.Add(DarkLabel20);
            DarkGroupBox8.Controls.Add(nudAoE);
            DarkGroupBox8.Controls.Add(DarkLabel19);
            DarkGroupBox8.Controls.Add(chkAoE);
            DarkGroupBox8.Controls.Add(DarkLabel18);
            DarkGroupBox8.Controls.Add(nudRange);
            DarkGroupBox8.Controls.Add(DarkLabel17);
            DarkGroupBox8.ForeColor = Color.Gainsboro;
            DarkGroupBox8.Location = new Point(10, 348);
            DarkGroupBox8.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox8.Name = "DarkGroupBox8";
            DarkGroupBox8.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox8.Size = new Size(423, 348);
            DarkGroupBox8.TabIndex = 2;
            DarkGroupBox8.TabStop = false;
            DarkGroupBox8.Text = "Cast Settings";
            // 
            // cmbAnim
            // 
            cmbAnim.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnim.FormattingEnabled = true;
            cmbAnim.Location = new Point(173, 295);
            cmbAnim.Margin = new Padding(6, 5, 6, 5);
            cmbAnim.Name = "cmbAnim";
            cmbAnim.Size = new Size(237, 32);
            cmbAnim.TabIndex = 12;
            // 
            // DarkLabel23
            // 
            DarkLabel23.AutoSize = true;
            DarkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel23.Location = new Point(10, 300);
            DarkLabel23.Margin = new Padding(6, 0, 6, 0);
            DarkLabel23.Name = "DarkLabel23";
            DarkLabel23.Size = new Size(98, 25);
            DarkLabel23.TabIndex = 11;
            DarkLabel23.Text = "Animation:";
            // 
            // cmbAnimCast
            // 
            cmbAnimCast.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnimCast.FormattingEnabled = true;
            cmbAnimCast.Location = new Point(173, 241);
            cmbAnimCast.Margin = new Padding(6, 5, 6, 5);
            cmbAnimCast.Name = "cmbAnimCast";
            cmbAnimCast.Size = new Size(237, 32);
            cmbAnimCast.TabIndex = 10;
            // 
            // DarkLabel22
            // 
            DarkLabel22.AutoSize = true;
            DarkLabel22.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel22.Location = new Point(10, 248);
            DarkLabel22.Margin = new Padding(6, 0, 6, 0);
            DarkLabel22.Name = "DarkLabel22";
            DarkLabel22.Size = new Size(137, 25);
            DarkLabel22.TabIndex = 9;
            DarkLabel22.Text = "Cast Animation:";
            // 
            // nudStun
            // 
            nudStun.Location = new Point(250, 184);
            nudStun.Margin = new Padding(6, 5, 6, 5);
            nudStun.Name = "nudStun";
            nudStun.Size = new Size(126, 31);
            nudStun.TabIndex = 8;
            // 
            // DarkLabel21
            // 
            DarkLabel21.AutoSize = true;
            DarkLabel21.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel21.Location = new Point(10, 187);
            DarkLabel21.Margin = new Padding(6, 0, 6, 0);
            DarkLabel21.Name = "DarkLabel21";
            DarkLabel21.Size = new Size(168, 25);
            DarkLabel21.TabIndex = 7;
            DarkLabel21.Text = "Stun Duration(secs):";
            // 
            // DarkLabel20
            // 
            DarkLabel20.AutoSize = true;
            DarkLabel20.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel20.Location = new Point(178, 137);
            DarkLabel20.Margin = new Padding(6, 0, 6, 0);
            DarkLabel20.Name = "DarkLabel20";
            DarkLabel20.Size = new Size(194, 25);
            DarkLabel20.TabIndex = 6;
            DarkLabel20.Text = "Tiles. Hint: 0 is self-cast";
            // 
            // nudAoE
            // 
            nudAoE.Location = new Point(90, 134);
            nudAoE.Margin = new Padding(6, 5, 6, 5);
            nudAoE.Name = "nudAoE";
            nudAoE.Size = new Size(78, 31);
            nudAoE.TabIndex = 5;
            // 
            // DarkLabel19
            // 
            DarkLabel19.AutoSize = true;
            DarkLabel19.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel19.Location = new Point(10, 137);
            DarkLabel19.Margin = new Padding(6, 0, 6, 0);
            DarkLabel19.Name = "DarkLabel19";
            DarkLabel19.Size = new Size(48, 25);
            DarkLabel19.TabIndex = 4;
            DarkLabel19.Text = "AoE:";
            // 
            // chkAoE
            // 
            chkAoE.AutoSize = true;
            chkAoE.Location = new Point(14, 88);
            chkAoE.Margin = new Padding(6, 5, 6, 5);
            chkAoE.Name = "chkAoE";
            chkAoE.Size = new Size(124, 29);
            chkAoE.TabIndex = 3;
            chkAoE.Text = "Is AoE Skill";
            // 
            // DarkLabel18
            // 
            DarkLabel18.AutoSize = true;
            DarkLabel18.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel18.Location = new Point(178, 45);
            DarkLabel18.Margin = new Padding(6, 0, 6, 0);
            DarkLabel18.Name = "DarkLabel18";
            DarkLabel18.Size = new Size(194, 25);
            DarkLabel18.TabIndex = 2;
            DarkLabel18.Text = "Tiles. Hint: 0 is self-cast";
            // 
            // nudRange
            // 
            nudRange.Location = new Point(90, 38);
            nudRange.Margin = new Padding(6, 5, 6, 5);
            nudRange.Name = "nudRange";
            nudRange.Size = new Size(78, 31);
            nudRange.TabIndex = 1;
            // 
            // DarkLabel17
            // 
            DarkLabel17.AutoSize = true;
            DarkLabel17.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel17.Location = new Point(10, 45);
            DarkLabel17.Margin = new Padding(6, 0, 6, 0);
            DarkLabel17.Name = "DarkLabel17";
            DarkLabel17.Size = new Size(66, 25);
            DarkLabel17.TabIndex = 0;
            DarkLabel17.Text = "Range:";
            // 
            // DarkGroupBox7
            // 
            DarkGroupBox7.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox7.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox7.Controls.Add(nudInterval);
            DarkGroupBox7.Controls.Add(DarkLabel16);
            DarkGroupBox7.Controls.Add(nudDuration);
            DarkGroupBox7.Controls.Add(DarkLabel15);
            DarkGroupBox7.Controls.Add(nudVital);
            DarkGroupBox7.Controls.Add(DarkLabel14);
            DarkGroupBox7.ForeColor = Color.Gainsboro;
            DarkGroupBox7.Location = new Point(10, 188);
            DarkGroupBox7.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox7.Name = "DarkGroupBox7";
            DarkGroupBox7.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox7.Size = new Size(423, 148);
            DarkGroupBox7.TabIndex = 1;
            DarkGroupBox7.TabStop = false;
            DarkGroupBox7.Text = "HoT & DoT Settings ";
            // 
            // nudInterval
            // 
            nudInterval.Location = new Point(338, 87);
            nudInterval.Margin = new Padding(6, 5, 6, 5);
            nudInterval.Name = "nudInterval";
            nudInterval.Size = new Size(74, 31);
            nudInterval.TabIndex = 5;
            // 
            // DarkLabel16
            // 
            DarkLabel16.AutoSize = true;
            DarkLabel16.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel16.Location = new Point(253, 91);
            DarkLabel16.Margin = new Padding(6, 0, 6, 0);
            DarkLabel16.Name = "DarkLabel16";
            DarkLabel16.Size = new Size(74, 25);
            DarkLabel16.TabIndex = 4;
            DarkLabel16.Text = "Interval:";
            // 
            // nudDuration
            // 
            nudDuration.Location = new Point(150, 87);
            nudDuration.Margin = new Padding(6, 5, 6, 5);
            nudDuration.Name = "nudDuration";
            nudDuration.Size = new Size(74, 31);
            nudDuration.TabIndex = 3;
            // 
            // DarkLabel15
            // 
            DarkLabel15.AutoSize = true;
            DarkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel15.Location = new Point(10, 91);
            DarkLabel15.Margin = new Padding(6, 0, 6, 0);
            DarkLabel15.Name = "DarkLabel15";
            DarkLabel15.Size = new Size(128, 25);
            DarkLabel15.TabIndex = 2;
            DarkLabel15.Text = "Duration(secs):";
            // 
            // nudVital
            // 
            nudVital.Location = new Point(243, 37);
            nudVital.Margin = new Padding(6, 5, 6, 5);
            nudVital.Name = "nudVital";
            nudVital.Size = new Size(170, 31);
            nudVital.TabIndex = 1;
            // 
            // DarkLabel14
            // 
            DarkLabel14.AutoSize = true;
            DarkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel14.Location = new Point(10, 41);
            DarkLabel14.Margin = new Padding(6, 0, 6, 0);
            DarkLabel14.Name = "DarkLabel14";
            DarkLabel14.Size = new Size(232, 25);
            DarkLabel14.TabIndex = 0;
            DarkLabel14.Text = "Amount to heal or damage:";
            // 
            // DarkGroupBox6
            // 
            DarkGroupBox6.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox6.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox6.Controls.Add(nudY);
            DarkGroupBox6.Controls.Add(DarkLabel13);
            DarkGroupBox6.Controls.Add(nudX);
            DarkGroupBox6.Controls.Add(DarkLabel12);
            DarkGroupBox6.Controls.Add(cmbDir);
            DarkGroupBox6.Controls.Add(DarkLabel11);
            DarkGroupBox6.Controls.Add(nudMap);
            DarkGroupBox6.Controls.Add(DarkLabel10);
            DarkGroupBox6.ForeColor = Color.Gainsboro;
            DarkGroupBox6.Location = new Point(10, 27);
            DarkGroupBox6.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox6.Name = "DarkGroupBox6";
            DarkGroupBox6.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox6.Size = new Size(423, 150);
            DarkGroupBox6.TabIndex = 0;
            DarkGroupBox6.TabStop = false;
            DarkGroupBox6.Text = "Warp Settings";
            // 
            // nudY
            // 
            nudY.Location = new Point(293, 87);
            nudY.Margin = new Padding(6, 5, 6, 5);
            nudY.Name = "nudY";
            nudY.Size = new Size(114, 31);
            nudY.TabIndex = 7;
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(197, 91);
            DarkLabel13.Margin = new Padding(6, 0, 6, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(26, 25);
            DarkLabel13.TabIndex = 6;
            DarkLabel13.Text = "Y:";
            // 
            // nudX
            // 
            nudX.Location = new Point(72, 87);
            nudX.Margin = new Padding(6, 5, 6, 5);
            nudX.Name = "nudX";
            nudX.Size = new Size(114, 31);
            nudX.TabIndex = 5;
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(10, 91);
            DarkLabel12.Margin = new Padding(6, 0, 6, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(27, 25);
            DarkLabel12.TabIndex = 4;
            DarkLabel12.Text = "X:";
            // 
            // cmbDir
            // 
            cmbDir.DrawMode = DrawMode.OwnerDrawFixed;
            cmbDir.FormattingEnabled = true;
            cmbDir.Items.AddRange(new object[] { "Up", "Down", "Left", "Right" });
            cmbDir.Location = new Point(293, 34);
            cmbDir.Margin = new Padding(6, 5, 6, 5);
            cmbDir.Name = "cmbDir";
            cmbDir.Size = new Size(112, 32);
            cmbDir.TabIndex = 3;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(197, 41);
            DarkLabel11.Margin = new Padding(6, 0, 6, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(87, 25);
            DarkLabel11.TabIndex = 2;
            DarkLabel11.Text = "Direction:";
            // 
            // nudMap
            // 
            nudMap.Location = new Point(72, 37);
            nudMap.Margin = new Padding(6, 5, 6, 5);
            nudMap.Name = "nudMap";
            nudMap.Size = new Size(114, 31);
            nudMap.TabIndex = 1;
            // 
            // DarkLabel10
            // 
            DarkLabel10.AutoSize = true;
            DarkLabel10.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel10.Location = new Point(10, 41);
            DarkLabel10.Margin = new Padding(6, 0, 6, 0);
            DarkLabel10.Name = "DarkLabel10";
            DarkLabel10.Size = new Size(52, 25);
            DarkLabel10.TabIndex = 0;
            DarkLabel10.Text = "Map:";
            // 
            // DarkGroupBox3
            // 
            DarkGroupBox3.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox3.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox3.Controls.Add(chkKnockBack);
            DarkGroupBox3.Controls.Add(cmbKnockBackTiles);
            DarkGroupBox3.Controls.Add(cmbProjectile);
            DarkGroupBox3.Controls.Add(chkProjectile);
            DarkGroupBox3.Controls.Add(nudIcon);
            DarkGroupBox3.Controls.Add(DarkLabel9);
            DarkGroupBox3.Controls.Add(picSprite);
            DarkGroupBox3.Controls.Add(nudCool);
            DarkGroupBox3.Controls.Add(DarkLabel8);
            DarkGroupBox3.Controls.Add(nudCast);
            DarkGroupBox3.Controls.Add(DarkLabel7);
            DarkGroupBox3.Controls.Add(DarkGroupBox4);
            DarkGroupBox3.Controls.Add(nudMp);
            DarkGroupBox3.Controls.Add(DarkLabel3);
            DarkGroupBox3.Controls.Add(cmbType);
            DarkGroupBox3.Controls.Add(DarkLabel2);
            DarkGroupBox3.Controls.Add(txtName);
            DarkGroupBox3.Controls.Add(DarkLabel1);
            DarkGroupBox3.ForeColor = Color.Gainsboro;
            DarkGroupBox3.Location = new Point(10, 37);
            DarkGroupBox3.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox3.Size = new Size(550, 622);
            DarkGroupBox3.TabIndex = 0;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Basic Settings";
            // 
            // chkKnockBack
            // 
            chkKnockBack.AutoSize = true;
            chkKnockBack.Location = new Point(14, 303);
            chkKnockBack.Margin = new Padding(6, 5, 6, 5);
            chkKnockBack.Name = "chkKnockBack";
            chkKnockBack.Size = new Size(179, 29);
            chkKnockBack.TabIndex = 61;
            chkKnockBack.Text = "Has knockback of";
            // 
            // cmbKnockBackTiles
            // 
            cmbKnockBackTiles.DrawMode = DrawMode.OwnerDrawFixed;
            cmbKnockBackTiles.FormattingEnabled = true;
            cmbKnockBackTiles.Items.AddRange(new object[] { "No KnockBack", "1 Tile", "2 Tiles", "3 Tiles", "4 Tiles", "5 Tiles" });
            cmbKnockBackTiles.Location = new Point(254, 300);
            cmbKnockBackTiles.Margin = new Padding(6, 5, 6, 5);
            cmbKnockBackTiles.Name = "cmbKnockBackTiles";
            cmbKnockBackTiles.Size = new Size(282, 32);
            cmbKnockBackTiles.TabIndex = 60;
            // 
            // cmbProjectile
            // 
            cmbProjectile.DrawMode = DrawMode.OwnerDrawFixed;
            cmbProjectile.FormattingEnabled = true;
            cmbProjectile.Location = new Point(254, 248);
            cmbProjectile.Margin = new Padding(6, 5, 6, 5);
            cmbProjectile.Name = "cmbProjectile";
            cmbProjectile.Size = new Size(282, 32);
            cmbProjectile.TabIndex = 59;
            // 
            // chkProjectile
            // 
            chkProjectile.AutoSize = true;
            chkProjectile.Location = new Point(14, 252);
            chkProjectile.Margin = new Padding(6, 5, 6, 5);
            chkProjectile.Name = "chkProjectile";
            chkProjectile.Size = new Size(152, 29);
            chkProjectile.TabIndex = 58;
            chkProjectile.Text = "Has Projectile?";
            // 
            // nudIcon
            // 
            nudIcon.Location = new Point(120, 188);
            nudIcon.Margin = new Padding(6, 5, 6, 5);
            nudIcon.Name = "nudIcon";
            nudIcon.Size = new Size(133, 31);
            nudIcon.TabIndex = 57;
            // 
            // DarkLabel9
            // 
            DarkLabel9.AutoSize = true;
            DarkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel9.Location = new Point(10, 184);
            DarkLabel9.Margin = new Padding(6, 0, 6, 0);
            DarkLabel9.Name = "DarkLabel9";
            DarkLabel9.Size = new Size(50, 25);
            DarkLabel9.TabIndex = 56;
            DarkLabel9.Text = "Icon:";
            // 
            // picSprite
            // 
            picSprite.BackColor = Color.Black;
            picSprite.Location = new Point(264, 188);
            picSprite.Margin = new Padding(6, 5, 6, 5);
            picSprite.Name = "picSprite";
            picSprite.Size = new Size(48, 48);
            picSprite.TabIndex = 55;
            picSprite.TabStop = false;
            // 
            // nudCool
            // 
            nudCool.Location = new Point(410, 138);
            nudCool.Margin = new Padding(6, 5, 6, 5);
            nudCool.Name = "nudCool";
            nudCool.Size = new Size(128, 31);
            nudCool.TabIndex = 12;
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(264, 145);
            DarkLabel8.Margin = new Padding(6, 0, 6, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(141, 25);
            DarkLabel8.TabIndex = 11;
            DarkLabel8.Text = "Cooldown Time:";
            // 
            // nudCast
            // 
            nudCast.Location = new Point(118, 138);
            nudCast.Margin = new Padding(6, 5, 6, 5);
            nudCast.Name = "nudCast";
            nudCast.Size = new Size(133, 31);
            nudCast.TabIndex = 10;
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(10, 134);
            DarkLabel7.Margin = new Padding(6, 0, 6, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(93, 25);
            DarkLabel7.TabIndex = 9;
            DarkLabel7.Text = "Cast Time:";
            // 
            // DarkGroupBox4
            // 
            DarkGroupBox4.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox4.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox4.Controls.Add(DarkLabel6);
            DarkGroupBox4.Controls.Add(cmbJob);
            DarkGroupBox4.Controls.Add(cmbAccessReq);
            DarkGroupBox4.Controls.Add(DarkLabel5);
            DarkGroupBox4.Controls.Add(nudLevel);
            DarkGroupBox4.Controls.Add(DarkLabel4);
            DarkGroupBox4.ForeColor = Color.Gainsboro;
            DarkGroupBox4.Location = new Point(10, 437);
            DarkGroupBox4.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox4.Size = new Size(530, 166);
            DarkGroupBox4.TabIndex = 8;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Requirements";
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(10, 91);
            DarkLabel6.Margin = new Padding(6, 0, 6, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(119, 25);
            DarkLabel6.TabIndex = 11;
            DarkLabel6.Text = "Job Required:";
            // 
            // cmbJob
            // 
            cmbJob.DrawMode = DrawMode.OwnerDrawFixed;
            cmbJob.FormattingEnabled = true;
            cmbJob.Location = new Point(157, 87);
            cmbJob.Margin = new Padding(6, 5, 6, 5);
            cmbJob.Name = "cmbJob";
            cmbJob.Size = new Size(361, 32);
            cmbJob.TabIndex = 10;
            // 
            // cmbAccessReq
            // 
            cmbAccessReq.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAccessReq.FormattingEnabled = true;
            cmbAccessReq.Items.AddRange(new object[] { "Player", "Moderator", "Mapper", "Developer", "Owner" });
            cmbAccessReq.Location = new Point(402, 34);
            cmbAccessReq.Margin = new Padding(6, 5, 6, 5);
            cmbAccessReq.Name = "cmbAccessReq";
            cmbAccessReq.Size = new Size(116, 32);
            cmbAccessReq.TabIndex = 9;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(240, 41);
            DarkLabel5.Margin = new Padding(6, 0, 6, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(144, 25);
            DarkLabel5.TabIndex = 8;
            DarkLabel5.Text = "Access Required:";
            // 
            // nudLevel
            // 
            nudLevel.Location = new Point(157, 37);
            nudLevel.Margin = new Padding(6, 5, 6, 5);
            nudLevel.Name = "nudLevel";
            nudLevel.Size = new Size(73, 31);
            nudLevel.TabIndex = 7;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(10, 41);
            DarkLabel4.Margin = new Padding(6, 0, 6, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(130, 25);
            DarkLabel4.TabIndex = 6;
            DarkLabel4.Text = "Level Required:";
            // 
            // nudMp
            // 
            nudMp.Location = new Point(408, 91);
            nudMp.Margin = new Padding(6, 5, 6, 5);
            nudMp.Name = "nudMp";
            nudMp.Size = new Size(128, 31);
            nudMp.TabIndex = 5;
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(318, 93);
            DarkLabel3.Margin = new Padding(6, 0, 6, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(83, 25);
            DarkLabel3.TabIndex = 4;
            DarkLabel3.Text = "MP Cost:";
            // 
            // cmbType
            // 
            cmbType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "Damage HP", "Damage MP", "Heal HP", "Heal MP", "Warp" });
            cmbType.Location = new Point(120, 88);
            cmbType.Margin = new Padding(6, 5, 6, 5);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(147, 32);
            cmbType.TabIndex = 3;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(10, 84);
            DarkLabel2.Margin = new Padding(6, 0, 6, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(53, 25);
            DarkLabel2.TabIndex = 2;
            DarkLabel2.Text = "Type:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(120, 37);
            txtName.Margin = new Padding(6, 5, 6, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(419, 31);
            txtName.TabIndex = 1;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(12, 41);
            DarkLabel1.Margin = new Padding(6, 0, 6, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(63, 25);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(16, 663);
            btnDelete.Margin = new Padding(6, 5, 6, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(8, 9, 8, 9);
            btnDelete.Size = new Size(284, 45);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(16, 718);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(8, 9, 8, 9);
            btnCancel.Size = new Size(284, 45);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(16, 609);
            btnSave.Margin = new Padding(6, 5, 6, 5);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(8, 9, 8, 9);
            btnSave.Size = new Size(284, 45);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            // 
            // frmEditor_Skill
            // 
            AutoScaleDimensions = new SizeF(10f, 25f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1360, 772);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(6, 5, 6, 5);
            Name = "frmEditor_Skill";
            Text = "Skill Editor";
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox5.ResumeLayout(false);
            DarkGroupBox8.ResumeLayout(false);
            DarkGroupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudStun).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAoE).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRange).EndInit();
            DarkGroupBox7.ResumeLayout(false);
            DarkGroupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudInterval).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVital).EndInit();
            DarkGroupBox6.ResumeLayout(false);
            DarkGroupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMap).EndInit();
            DarkGroupBox3.ResumeLayout(false);
            DarkGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCool).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCast).EndInit();
            DarkGroupBox4.ResumeLayout(false);
            DarkGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMp).EndInit();
            Load += new EventHandler(frmEditor_Skill_Load);
            FormClosing += new FormClosingEventHandler(frmEditor_Skill_FormClosing);
            ResumeLayout(false);

        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox3;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal DarkUI.Controls.DarkComboBox cmbType;
        internal DarkUI.Controls.DarkLabel DarkLabel2;
        internal DarkUI.Controls.DarkNumericUpDown nudMp;
        internal DarkUI.Controls.DarkLabel DarkLabel3;
        internal DarkUI.Controls.DarkNumericUpDown nudLevel;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox4;
        internal DarkUI.Controls.DarkLabel DarkLabel5;
        internal DarkUI.Controls.DarkComboBox cmbAccessReq;
        internal DarkUI.Controls.DarkLabel DarkLabel6;
        internal DarkUI.Controls.DarkComboBox cmbJob;
        internal DarkUI.Controls.DarkNumericUpDown nudCast;
        internal DarkUI.Controls.DarkLabel DarkLabel7;
        internal DarkUI.Controls.DarkNumericUpDown nudCool;
        internal DarkUI.Controls.DarkLabel DarkLabel8;
        internal DarkUI.Controls.DarkLabel DarkLabel9;
        internal PictureBox picSprite;
        internal DarkUI.Controls.DarkNumericUpDown nudIcon;
        internal DarkUI.Controls.DarkCheckBox chkProjectile;
        internal DarkUI.Controls.DarkComboBox cmbProjectile;
        internal DarkUI.Controls.DarkCheckBox chkKnockBack;
        internal DarkUI.Controls.DarkComboBox cmbKnockBackTiles;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox5;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox6;
        internal DarkUI.Controls.DarkNumericUpDown nudMap;
        internal DarkUI.Controls.DarkLabel DarkLabel10;
        internal DarkUI.Controls.DarkLabel DarkLabel11;
        internal DarkUI.Controls.DarkComboBox cmbDir;
        internal DarkUI.Controls.DarkNumericUpDown nudX;
        internal DarkUI.Controls.DarkLabel DarkLabel12;
        internal DarkUI.Controls.DarkNumericUpDown nudY;
        internal DarkUI.Controls.DarkLabel DarkLabel13;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox7;
        internal DarkUI.Controls.DarkNumericUpDown nudVital;
        internal DarkUI.Controls.DarkLabel DarkLabel14;
        internal DarkUI.Controls.DarkNumericUpDown nudDuration;
        internal DarkUI.Controls.DarkLabel DarkLabel15;
        internal DarkUI.Controls.DarkNumericUpDown nudInterval;
        internal DarkUI.Controls.DarkLabel DarkLabel16;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox8;
        internal DarkUI.Controls.DarkLabel DarkLabel18;
        internal DarkUI.Controls.DarkNumericUpDown nudRange;
        internal DarkUI.Controls.DarkLabel DarkLabel17;
        internal DarkUI.Controls.DarkCheckBox chkAoE;
        internal DarkUI.Controls.DarkLabel DarkLabel20;
        internal DarkUI.Controls.DarkNumericUpDown nudAoE;
        internal DarkUI.Controls.DarkLabel DarkLabel19;
        internal DarkUI.Controls.DarkNumericUpDown nudStun;
        internal DarkUI.Controls.DarkLabel DarkLabel21;
        internal DarkUI.Controls.DarkLabel DarkLabel22;
        internal DarkUI.Controls.DarkComboBox cmbAnimCast;
        internal DarkUI.Controls.DarkComboBox cmbAnim;
        internal DarkUI.Controls.DarkLabel DarkLabel23;
        internal DarkUI.Controls.DarkButton btnDelete;
        internal DarkUI.Controls.DarkButton btnCancel;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkButton btnLearn;
    }
}