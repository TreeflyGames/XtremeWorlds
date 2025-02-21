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
            DarkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            btnLearn = new DarkUI.Controls.DarkButton();
            DarkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            DarkGroupBox8 = new DarkUI.Controls.DarkGroupBox();
            cmbAnim = new DarkUI.Controls.DarkComboBox();
            DarkLabel23 = new DarkUI.Controls.DarkLabel();
            cmbAnimCast = new DarkUI.Controls.DarkComboBox();
            DarkLabel22 = new DarkUI.Controls.DarkLabel();
            nudStun = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel21 = new DarkUI.Controls.DarkLabel();
            DarkLabel20 = new DarkUI.Controls.DarkLabel();
            nudAoE = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel19 = new DarkUI.Controls.DarkLabel();
            chkAoE = new DarkUI.Controls.DarkCheckBox();
            DarkLabel18 = new DarkUI.Controls.DarkLabel();
            nudRange = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel17 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox7 = new DarkUI.Controls.DarkGroupBox();
            nudInterval = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel16 = new DarkUI.Controls.DarkLabel();
            nudDuration = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel15 = new DarkUI.Controls.DarkLabel();
            nudVital = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel14 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox6 = new DarkUI.Controls.DarkGroupBox();
            nudY = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            nudX = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            cmbDir = new DarkUI.Controls.DarkComboBox();
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            nudMap = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel10 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            chkKnockBack = new DarkUI.Controls.DarkCheckBox();
            cmbKnockBackTiles = new DarkUI.Controls.DarkComboBox();
            cmbProjectile = new DarkUI.Controls.DarkComboBox();
            chkProjectile = new DarkUI.Controls.DarkCheckBox();
            nudIcon = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel9 = new DarkUI.Controls.DarkLabel();
            picSprite = new PictureBox();
            nudCool = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel8 = new DarkUI.Controls.DarkLabel();
            nudCast = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            cmbJob = new DarkUI.Controls.DarkComboBox();
            cmbAccessReq = new DarkUI.Controls.DarkComboBox();
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            nudLevel = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            nudMp = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            cmbType = new DarkUI.Controls.DarkComboBox();
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            txtName = new DarkUI.Controls.DarkTextBox();
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            btnDelete = new DarkUI.Controls.DarkButton();
            btnCancel = new DarkUI.Controls.DarkButton();
            btnSave = new DarkUI.Controls.DarkButton();
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
            DarkGroupBox1.Location = new Point(4, 3);
            DarkGroupBox1.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Size = new Size(214, 357);
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
            lstIndex.Location = new Point(7, 22);
            lstIndex.Margin = new Padding(4, 3, 4, 3);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(199, 332);
            lstIndex.TabIndex = 1;
            lstIndex.Click += lstIndex_Click;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(btnLearn);
            DarkGroupBox2.Controls.Add(DarkGroupBox5);
            DarkGroupBox2.Controls.Add(DarkGroupBox3);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(224, 3);
            DarkGroupBox2.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Size = new Size(722, 455);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // btnLearn
            // 
            btnLearn.Location = new Point(11, 413);
            btnLearn.Margin = new Padding(4, 3, 4, 3);
            btnLearn.Name = "btnLearn";
            btnLearn.Padding = new Padding(6, 6, 6, 6);
            btnLearn.Size = new Size(88, 27);
            btnLearn.TabIndex = 11;
            btnLearn.Text = "Learn";
            btnLearn.Click += btnLearn_Click;
            // 
            // DarkGroupBox5
            // 
            DarkGroupBox5.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox5.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox5.Controls.Add(DarkGroupBox8);
            DarkGroupBox5.Controls.Add(DarkGroupBox7);
            DarkGroupBox5.Controls.Add(DarkGroupBox6);
            DarkGroupBox5.ForeColor = Color.Gainsboro;
            DarkGroupBox5.Location = new Point(399, 22);
            DarkGroupBox5.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox5.Name = "DarkGroupBox5";
            DarkGroupBox5.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox5.Size = new Size(312, 423);
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
            DarkGroupBox8.Location = new Point(7, 209);
            DarkGroupBox8.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox8.Name = "DarkGroupBox8";
            DarkGroupBox8.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox8.Size = new Size(296, 209);
            DarkGroupBox8.TabIndex = 2;
            DarkGroupBox8.TabStop = false;
            DarkGroupBox8.Text = "Cast Settings";
            // 
            // cmbAnim
            // 
            cmbAnim.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnim.FormattingEnabled = true;
            cmbAnim.Location = new Point(121, 177);
            cmbAnim.Margin = new Padding(4, 3, 4, 3);
            cmbAnim.Name = "cmbAnim";
            cmbAnim.Size = new Size(167, 24);
            cmbAnim.TabIndex = 12;
            cmbAnim.SelectedIndexChanged += CmbAnim_Scroll;
            // 
            // DarkLabel23
            // 
            DarkLabel23.AutoSize = true;
            DarkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel23.Location = new Point(7, 180);
            DarkLabel23.Margin = new Padding(4, 0, 4, 0);
            DarkLabel23.Name = "DarkLabel23";
            DarkLabel23.Size = new Size(66, 15);
            DarkLabel23.TabIndex = 11;
            DarkLabel23.Text = "Animation:";
            // 
            // cmbAnimCast
            // 
            cmbAnimCast.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnimCast.FormattingEnabled = true;
            cmbAnimCast.Location = new Point(121, 145);
            cmbAnimCast.Margin = new Padding(4, 3, 4, 3);
            cmbAnimCast.Name = "cmbAnimCast";
            cmbAnimCast.Size = new Size(167, 24);
            cmbAnimCast.TabIndex = 10;
            cmbAnimCast.SelectedIndexChanged += CmbAnimCast_Scroll;
            // 
            // DarkLabel22
            // 
            DarkLabel22.AutoSize = true;
            DarkLabel22.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel22.Location = new Point(7, 149);
            DarkLabel22.Margin = new Padding(4, 0, 4, 0);
            DarkLabel22.Name = "DarkLabel22";
            DarkLabel22.Size = new Size(92, 15);
            DarkLabel22.TabIndex = 9;
            DarkLabel22.Text = "Cast Animation:";
            // 
            // nudStun
            // 
            nudStun.Location = new Point(175, 110);
            nudStun.Margin = new Padding(4, 3, 4, 3);
            nudStun.Name = "nudStun";
            nudStun.Size = new Size(88, 23);
            nudStun.TabIndex = 8;
            nudStun.ValueChanged += NudStun_Scroll;
            // 
            // DarkLabel21
            // 
            DarkLabel21.AutoSize = true;
            DarkLabel21.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel21.Location = new Point(7, 112);
            DarkLabel21.Margin = new Padding(4, 0, 4, 0);
            DarkLabel21.Name = "DarkLabel21";
            DarkLabel21.Size = new Size(113, 15);
            DarkLabel21.TabIndex = 7;
            DarkLabel21.Text = "Stun Duration(secs):";
            // 
            // DarkLabel20
            // 
            DarkLabel20.AutoSize = true;
            DarkLabel20.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel20.Location = new Point(125, 82);
            DarkLabel20.Margin = new Padding(4, 0, 4, 0);
            DarkLabel20.Name = "DarkLabel20";
            DarkLabel20.Size = new Size(130, 15);
            DarkLabel20.TabIndex = 6;
            DarkLabel20.Text = "Tiles. Hint: 0 is self-cast";
            // 
            // nudAoE
            // 
            nudAoE.Location = new Point(63, 80);
            nudAoE.Margin = new Padding(4, 3, 4, 3);
            nudAoE.Name = "nudAoE";
            nudAoE.Size = new Size(55, 23);
            nudAoE.TabIndex = 5;
            nudAoE.ValueChanged += NudAoE_Scroll;
            // 
            // DarkLabel19
            // 
            DarkLabel19.AutoSize = true;
            DarkLabel19.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel19.Location = new Point(7, 82);
            DarkLabel19.Margin = new Padding(4, 0, 4, 0);
            DarkLabel19.Name = "DarkLabel19";
            DarkLabel19.Size = new Size(31, 15);
            DarkLabel19.TabIndex = 4;
            DarkLabel19.Text = "AoE:";
            // 
            // chkAoE
            // 
            chkAoE.AutoSize = true;
            chkAoE.Location = new Point(10, 53);
            chkAoE.Margin = new Padding(4, 3, 4, 3);
            chkAoE.Name = "chkAoE";
            chkAoE.Size = new Size(82, 19);
            chkAoE.TabIndex = 3;
            chkAoE.Text = "Is AoE Skill";
            chkAoE.CheckedChanged += ChkAOE_CheckedChanged;
            // 
            // DarkLabel18
            // 
            DarkLabel18.AutoSize = true;
            DarkLabel18.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel18.Location = new Point(125, 27);
            DarkLabel18.Margin = new Padding(4, 0, 4, 0);
            DarkLabel18.Name = "DarkLabel18";
            DarkLabel18.Size = new Size(130, 15);
            DarkLabel18.TabIndex = 2;
            DarkLabel18.Text = "Tiles. Hint: 0 is self-cast";
            // 
            // nudRange
            // 
            nudRange.Location = new Point(63, 23);
            nudRange.Margin = new Padding(4, 3, 4, 3);
            nudRange.Name = "nudRange";
            nudRange.Size = new Size(55, 23);
            nudRange.TabIndex = 1;
            nudRange.ValueChanged += NudRange_Scroll;
            // 
            // DarkLabel17
            // 
            DarkLabel17.AutoSize = true;
            DarkLabel17.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel17.Location = new Point(7, 27);
            DarkLabel17.Margin = new Padding(4, 0, 4, 0);
            DarkLabel17.Name = "DarkLabel17";
            DarkLabel17.Size = new Size(43, 15);
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
            DarkGroupBox7.Location = new Point(7, 113);
            DarkGroupBox7.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox7.Name = "DarkGroupBox7";
            DarkGroupBox7.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox7.Size = new Size(296, 89);
            DarkGroupBox7.TabIndex = 1;
            DarkGroupBox7.TabStop = false;
            DarkGroupBox7.Text = "HoT & DoT Settings ";
            // 
            // nudInterval
            // 
            nudInterval.Location = new Point(237, 52);
            nudInterval.Margin = new Padding(4, 3, 4, 3);
            nudInterval.Name = "nudInterval";
            nudInterval.Size = new Size(52, 23);
            nudInterval.TabIndex = 5;
            nudInterval.ValueChanged += NudInterval_Scroll;
            // 
            // DarkLabel16
            // 
            DarkLabel16.AutoSize = true;
            DarkLabel16.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel16.Location = new Point(177, 55);
            DarkLabel16.Margin = new Padding(4, 0, 4, 0);
            DarkLabel16.Name = "DarkLabel16";
            DarkLabel16.Size = new Size(49, 15);
            DarkLabel16.TabIndex = 4;
            DarkLabel16.Text = "Interval:";
            // 
            // nudDuration
            // 
            nudDuration.Location = new Point(105, 52);
            nudDuration.Margin = new Padding(4, 3, 4, 3);
            nudDuration.Name = "nudDuration";
            nudDuration.Size = new Size(52, 23);
            nudDuration.TabIndex = 3;
            nudDuration.ValueChanged += NudDuration_Scroll;
            // 
            // DarkLabel15
            // 
            DarkLabel15.AutoSize = true;
            DarkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel15.Location = new Point(7, 55);
            DarkLabel15.Margin = new Padding(4, 0, 4, 0);
            DarkLabel15.Name = "DarkLabel15";
            DarkLabel15.Size = new Size(86, 15);
            DarkLabel15.TabIndex = 2;
            DarkLabel15.Text = "Duration(secs):";
            // 
            // nudVital
            // 
            nudVital.Location = new Point(170, 22);
            nudVital.Margin = new Padding(4, 3, 4, 3);
            nudVital.Name = "nudVital";
            nudVital.Size = new Size(119, 23);
            nudVital.TabIndex = 1;
            nudVital.ValueChanged += NudVital_Scroll;
            // 
            // DarkLabel14
            // 
            DarkLabel14.AutoSize = true;
            DarkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel14.Location = new Point(7, 25);
            DarkLabel14.Margin = new Padding(4, 0, 4, 0);
            DarkLabel14.Name = "DarkLabel14";
            DarkLabel14.Size = new Size(153, 15);
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
            DarkGroupBox6.Location = new Point(7, 16);
            DarkGroupBox6.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox6.Name = "DarkGroupBox6";
            DarkGroupBox6.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox6.Size = new Size(296, 90);
            DarkGroupBox6.TabIndex = 0;
            DarkGroupBox6.TabStop = false;
            DarkGroupBox6.Text = "Warp Settings";
            // 
            // nudY
            // 
            nudY.Location = new Point(205, 52);
            nudY.Margin = new Padding(4, 3, 4, 3);
            nudY.Name = "nudY";
            nudY.Size = new Size(80, 23);
            nudY.TabIndex = 7;
            nudY.ValueChanged += NudY_Scroll;
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(138, 55);
            DarkLabel13.Margin = new Padding(4, 0, 4, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(17, 15);
            DarkLabel13.TabIndex = 6;
            DarkLabel13.Text = "Y:";
            // 
            // nudX
            // 
            nudX.Location = new Point(50, 52);
            nudX.Margin = new Padding(4, 3, 4, 3);
            nudX.Name = "nudX";
            nudX.Size = new Size(80, 23);
            nudX.TabIndex = 5;
            nudX.ValueChanged += NudX_Scroll;
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(7, 55);
            DarkLabel12.Margin = new Padding(4, 0, 4, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(17, 15);
            DarkLabel12.TabIndex = 4;
            DarkLabel12.Text = "X:";
            // 
            // cmbDir
            // 
            cmbDir.DrawMode = DrawMode.OwnerDrawFixed;
            cmbDir.FormattingEnabled = true;
            cmbDir.Items.AddRange(new object[] { "Up", "Down", "Left", "Right" });
            cmbDir.Location = new Point(205, 20);
            cmbDir.Margin = new Padding(4, 3, 4, 3);
            cmbDir.Name = "cmbDir";
            cmbDir.Size = new Size(80, 24);
            cmbDir.TabIndex = 3;
            cmbDir.SelectedIndexChanged += CmbDir_SelectedIndexChanged;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(138, 25);
            DarkLabel11.Margin = new Padding(4, 0, 4, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(58, 15);
            DarkLabel11.TabIndex = 2;
            DarkLabel11.Text = "Direction:";
            // 
            // nudMap
            // 
            nudMap.Location = new Point(50, 22);
            nudMap.Margin = new Padding(4, 3, 4, 3);
            nudMap.Name = "nudMap";
            nudMap.Size = new Size(80, 23);
            nudMap.TabIndex = 1;
            nudMap.ValueChanged += NudMap_Scroll;
            // 
            // DarkLabel10
            // 
            DarkLabel10.AutoSize = true;
            DarkLabel10.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel10.Location = new Point(7, 25);
            DarkLabel10.Margin = new Padding(4, 0, 4, 0);
            DarkLabel10.Name = "DarkLabel10";
            DarkLabel10.Size = new Size(34, 15);
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
            DarkGroupBox3.Location = new Point(7, 22);
            DarkGroupBox3.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Size = new Size(385, 373);
            DarkGroupBox3.TabIndex = 0;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Basic Settings";
            // 
            // chkKnockBack
            // 
            chkKnockBack.AutoSize = true;
            chkKnockBack.Location = new Point(10, 182);
            chkKnockBack.Margin = new Padding(4, 3, 4, 3);
            chkKnockBack.Name = "chkKnockBack";
            chkKnockBack.Size = new Size(120, 19);
            chkKnockBack.TabIndex = 61;
            chkKnockBack.Text = "Has knockback of";
            chkKnockBack.CheckedChanged += ChkKnockBack_CheckedChanged;
            // 
            // cmbKnockBackTiles
            // 
            cmbKnockBackTiles.DrawMode = DrawMode.OwnerDrawFixed;
            cmbKnockBackTiles.FormattingEnabled = true;
            cmbKnockBackTiles.Items.AddRange(new object[] { "No KnockBack", "1 Tile", "2 Tiles", "3 Tiles", "4 Tiles", "5 Tiles" });
            cmbKnockBackTiles.Location = new Point(178, 180);
            cmbKnockBackTiles.Margin = new Padding(4, 3, 4, 3);
            cmbKnockBackTiles.Name = "cmbKnockBackTiles";
            cmbKnockBackTiles.Size = new Size(199, 24);
            cmbKnockBackTiles.TabIndex = 60;
            cmbKnockBackTiles.SelectedIndexChanged += CmbKnockBackTiles_SelectedIndexChanged;
            // 
            // cmbProjectile
            // 
            cmbProjectile.DrawMode = DrawMode.OwnerDrawFixed;
            cmbProjectile.FormattingEnabled = true;
            cmbProjectile.Location = new Point(178, 149);
            cmbProjectile.Margin = new Padding(4, 3, 4, 3);
            cmbProjectile.Name = "cmbProjectile";
            cmbProjectile.Size = new Size(199, 24);
            cmbProjectile.TabIndex = 59;
            cmbProjectile.SelectedIndexChanged += ScrlProjectile_Scroll;
            // 
            // chkProjectile
            // 
            chkProjectile.AutoSize = true;
            chkProjectile.Location = new Point(10, 151);
            chkProjectile.Margin = new Padding(4, 3, 4, 3);
            chkProjectile.Name = "chkProjectile";
            chkProjectile.Size = new Size(103, 19);
            chkProjectile.TabIndex = 58;
            chkProjectile.Text = "Has Projectile?";
            chkProjectile.CheckedChanged += ChkProjectile_CheckedChanged;
            // 
            // nudIcon
            // 
            nudIcon.Location = new Point(84, 113);
            nudIcon.Margin = new Padding(4, 3, 4, 3);
            nudIcon.Name = "nudIcon";
            nudIcon.Size = new Size(93, 23);
            nudIcon.TabIndex = 57;
            nudIcon.Click += nudIcon_Click;
            // 
            // DarkLabel9
            // 
            DarkLabel9.AutoSize = true;
            DarkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel9.Location = new Point(7, 110);
            DarkLabel9.Margin = new Padding(4, 0, 4, 0);
            DarkLabel9.Name = "DarkLabel9";
            DarkLabel9.Size = new Size(33, 15);
            DarkLabel9.TabIndex = 56;
            DarkLabel9.Text = "Icon:";
            // 
            // picSprite
            // 
            picSprite.BackColor = Color.Black;
            picSprite.Location = new Point(185, 113);
            picSprite.Margin = new Padding(4, 3, 4, 3);
            picSprite.Name = "picSprite";
            picSprite.Size = new Size(34, 29);
            picSprite.TabIndex = 55;
            picSprite.TabStop = false;
            // 
            // nudCool
            // 
            nudCool.Location = new Point(287, 83);
            nudCool.Margin = new Padding(4, 3, 4, 3);
            nudCool.Name = "nudCool";
            nudCool.Size = new Size(90, 23);
            nudCool.TabIndex = 12;
            nudCool.ValueChanged += NudCool_Scroll;
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(185, 87);
            DarkLabel8.Margin = new Padding(4, 0, 4, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(95, 15);
            DarkLabel8.TabIndex = 11;
            DarkLabel8.Text = "Cooldown Time:";
            // 
            // nudCast
            // 
            nudCast.Location = new Point(83, 83);
            nudCast.Margin = new Padding(4, 3, 4, 3);
            nudCast.Name = "nudCast";
            nudCast.Size = new Size(93, 23);
            nudCast.TabIndex = 10;
            nudCast.ValueChanged += NudCast_Scroll;
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(7, 80);
            DarkLabel7.Margin = new Padding(4, 0, 4, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(63, 15);
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
            DarkGroupBox4.Location = new Point(7, 262);
            DarkGroupBox4.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Size = new Size(371, 100);
            DarkGroupBox4.TabIndex = 8;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Requirements";
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(7, 55);
            DarkLabel6.Margin = new Padding(4, 0, 4, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(78, 15);
            DarkLabel6.TabIndex = 11;
            DarkLabel6.Text = "Job Required:";
            // 
            // cmbJob
            // 
            cmbJob.DrawMode = DrawMode.OwnerDrawFixed;
            cmbJob.FormattingEnabled = true;
            cmbJob.Location = new Point(110, 52);
            cmbJob.Margin = new Padding(4, 3, 4, 3);
            cmbJob.Name = "cmbJob";
            cmbJob.Size = new Size(254, 24);
            cmbJob.TabIndex = 10;
            cmbJob.SelectedIndexChanged += CmbClass_SelectedIndexChanged;
            // 
            // cmbAccessReq
            // 
            cmbAccessReq.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAccessReq.FormattingEnabled = true;
            cmbAccessReq.Items.AddRange(new object[] { "Player", "Moderator", "Mapper", "Developer", "Owner" });
            cmbAccessReq.Location = new Point(281, 20);
            cmbAccessReq.Margin = new Padding(4, 3, 4, 3);
            cmbAccessReq.Name = "cmbAccessReq";
            cmbAccessReq.Size = new Size(82, 24);
            cmbAccessReq.TabIndex = 9;
            cmbAccessReq.SelectedIndexChanged += CmbAccessReq_SelectedIndexChanged;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(168, 25);
            DarkLabel5.Margin = new Padding(4, 0, 4, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(96, 15);
            DarkLabel5.TabIndex = 8;
            DarkLabel5.Text = "Access Required:";
            // 
            // nudLevel
            // 
            nudLevel.Location = new Point(110, 22);
            nudLevel.Margin = new Padding(4, 3, 4, 3);
            nudLevel.Name = "nudLevel";
            nudLevel.Size = new Size(51, 23);
            nudLevel.TabIndex = 7;
            nudLevel.ValueChanged += NudLevel_ValueChanged;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(7, 25);
            DarkLabel4.Margin = new Padding(4, 0, 4, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(87, 15);
            DarkLabel4.TabIndex = 6;
            DarkLabel4.Text = "Level Required:";
            // 
            // nudMp
            // 
            nudMp.Location = new Point(286, 55);
            nudMp.Margin = new Padding(4, 3, 4, 3);
            nudMp.Name = "nudMp";
            nudMp.Size = new Size(90, 23);
            nudMp.TabIndex = 5;
            nudMp.ValueChanged += NudMp_ValueChanged;
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(223, 56);
            DarkLabel3.Margin = new Padding(4, 0, 4, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(55, 15);
            DarkLabel3.TabIndex = 4;
            DarkLabel3.Text = "MP Cost:";
            // 
            // cmbType
            // 
            cmbType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "Damage HP", "Damage MP", "Heal HP", "Heal MP", "Warp" });
            cmbType.Location = new Point(84, 53);
            cmbType.Margin = new Padding(4, 3, 4, 3);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(104, 24);
            cmbType.TabIndex = 3;
            cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(7, 50);
            DarkLabel2.Margin = new Padding(4, 0, 4, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(35, 15);
            DarkLabel2.TabIndex = 2;
            DarkLabel2.Text = "Type:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(84, 22);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(294, 23);
            txtName.TabIndex = 1;
            txtName.TextChanged += TxtName_TextChanged;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(8, 25);
            DarkLabel1.Margin = new Padding(4, 0, 4, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(42, 15);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(11, 398);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(6, 5, 6, 5);
            btnDelete.Size = new Size(199, 27);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(11, 431);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(199, 27);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(11, 365);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(6, 5, 6, 5);
            btnSave.Size = new Size(199, 27);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.Click += BtnSave_Click;
            // 
            // frmEditor_Skill
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(952, 463);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmEditor_Skill";
            Text = "Skill Editor";
            FormClosing += frmEditor_Skill_FormClosing;
            Load += frmEditor_Skill_Load;
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