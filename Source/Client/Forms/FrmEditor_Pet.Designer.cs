using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_Pet : Form
    {

        // Shared instance of the form
        private static frmEditor_Pet _instance;

        // Public property to get the shared instance
        public static frmEditor_Pet Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Pet();
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
            DarkGroupBox6 = new DarkUI.Controls.DarkGroupBox();
            cmbSkill4 = new DarkUI.Controls.DarkComboBox();
            cmbSkill4.SelectedIndexChanged += new EventHandler(CmbSkill4_SelectedIndexChanged);
            DarkLabel19 = new DarkUI.Controls.DarkLabel();
            cmbSkill3 = new DarkUI.Controls.DarkComboBox();
            cmbSkill3.SelectedIndexChanged += new EventHandler(CmbSkill3_SelectedIndexChanged);
            DarkLabel18 = new DarkUI.Controls.DarkLabel();
            cmbSkill2 = new DarkUI.Controls.DarkComboBox();
            cmbSkill2.SelectedIndexChanged += new EventHandler(CmbSkill2_SelectedIndexChanged);
            DarkLabel17 = new DarkUI.Controls.DarkLabel();
            cmbSkill1 = new DarkUI.Controls.DarkComboBox();
            cmbSkill1.SelectedIndexChanged += new EventHandler(CmbSkill1_SelectedIndexChanged);
            DarkLabel16 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            pnlPetlevel = new Panel();
            DarkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            cmbEvolve = new DarkUI.Controls.DarkComboBox();
            cmbEvolve.SelectedIndexChanged += new EventHandler(CmbEvolve_SelectedIndexChanged);
            DarkLabel15 = new DarkUI.Controls.DarkLabel();
            nudEvolveLvl = new DarkUI.Controls.DarkNumericUpDown();
            nudEvolveLvl.ValueChanged += new EventHandler(NudEvolveLvl_ValueChanged);
            DarkLabel14 = new DarkUI.Controls.DarkLabel();
            chkEvolve = new DarkUI.Controls.DarkCheckBox();
            chkEvolve.CheckedChanged += new EventHandler(ChkEvolve_CheckedChanged);
            nudMaxLevel = new DarkUI.Controls.DarkNumericUpDown();
            nudMaxLevel.Click += new EventHandler(NudMaxLevel_ValueChanged);
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            nudPetPnts = new DarkUI.Controls.DarkNumericUpDown();
            nudPetPnts.Click += new EventHandler(NudPetPnts_ValueChanged);
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            nudPetExp = new DarkUI.Controls.DarkNumericUpDown();
            nudPetExp.Click += new EventHandler(NudPetExp_ValueChanged);
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            optDoNotLevel = new DarkUI.Controls.DarkRadioButton();
            optDoNotLevel.Click += new EventHandler(OptDoNotLevel_CheckedChanged);
            optLevel = new DarkUI.Controls.DarkRadioButton();
            optLevel.Click += new EventHandler(OptLevel_CheckedChanged);
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            pnlCustomStats = new Panel();
            nudLevel = new DarkUI.Controls.DarkNumericUpDown();
            nudLevel.ValueChanged += new EventHandler(NudLevel_ValueChanged);
            DarkLabel10 = new DarkUI.Controls.DarkLabel();
            nudSpirit = new DarkUI.Controls.DarkNumericUpDown();
            nudSpirit.ValueChanged += new EventHandler(NudSpirit_ValueChanged);
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            nudIntelligence = new DarkUI.Controls.DarkNumericUpDown();
            nudIntelligence.ValueChanged += new EventHandler(NudIntelligence_ValueChanged);
            DarkLabel8 = new DarkUI.Controls.DarkLabel();
            nudLuck = new DarkUI.Controls.DarkNumericUpDown();
            nudLuck.ValueChanged += new EventHandler(NudLuck_ValueChanged);
            DarkLabel9 = new DarkUI.Controls.DarkLabel();
            nudVitality = new DarkUI.Controls.DarkNumericUpDown();
            nudVitality.ValueChanged += new EventHandler(NudVitality_ValueChanged);
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            nudStrength = new DarkUI.Controls.DarkNumericUpDown();
            nudStrength.ValueChanged += new EventHandler(NudStrength_ValueChanged);
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            optAdoptStats = new DarkUI.Controls.DarkRadioButton();
            optAdoptStats.CheckedChanged += new EventHandler(OptAdoptStats_CheckedChanged);
            optCustomStats = new DarkUI.Controls.DarkRadioButton();
            optCustomStats.CheckedChanged += new EventHandler(OptCustomStats_CheckedChanged);
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            nudRange = new DarkUI.Controls.DarkNumericUpDown();
            nudRange.Click += new EventHandler(NudRange_ValueChanged);
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            nudSprite = new DarkUI.Controls.DarkNumericUpDown();
            nudSprite.Click += new EventHandler(NudSprite_Click);
            picSprite = new PictureBox();
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
            DarkGroupBox6.SuspendLayout();
            DarkGroupBox4.SuspendLayout();
            pnlPetlevel.SuspendLayout();
            DarkGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudEvolveLvl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPetPnts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPetExp).BeginInit();
            DarkGroupBox3.SuspendLayout();
            pnlCustomStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSpirit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIntelligence).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLuck).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVitality).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSprite).BeginInit();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(2, 5);
            DarkGroupBox1.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox1.Size = new Size(348, 741);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Pet List";
            // 
            // lstIndex
            // 
            lstIndex.BackColor = Color.FromArgb(45, 45, 48);
            lstIndex.BorderStyle = BorderStyle.FixedSingle;
            lstIndex.ForeColor = Color.Gainsboro;
            lstIndex.FormattingEnabled = true;
            lstIndex.ItemHeight = 25;
            lstIndex.Location = new Point(10, 27);
            lstIndex.Margin = new Padding(6, 5, 6, 5);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(329, 702);
            lstIndex.TabIndex = 1;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(DarkGroupBox6);
            DarkGroupBox2.Controls.Add(DarkGroupBox4);
            DarkGroupBox2.Controls.Add(DarkGroupBox3);
            DarkGroupBox2.Controls.Add(DarkLabel3);
            DarkGroupBox2.Controls.Add(nudRange);
            DarkGroupBox2.Controls.Add(DarkLabel2);
            DarkGroupBox2.Controls.Add(nudSprite);
            DarkGroupBox2.Controls.Add(picSprite);
            DarkGroupBox2.Controls.Add(txtName);
            DarkGroupBox2.Controls.Add(DarkLabel1);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(360, 5);
            DarkGroupBox2.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox2.Size = new Size(683, 909);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // DarkGroupBox6
            // 
            DarkGroupBox6.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox6.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox6.Controls.Add(cmbSkill4);
            DarkGroupBox6.Controls.Add(DarkLabel19);
            DarkGroupBox6.Controls.Add(cmbSkill3);
            DarkGroupBox6.Controls.Add(DarkLabel18);
            DarkGroupBox6.Controls.Add(cmbSkill2);
            DarkGroupBox6.Controls.Add(DarkLabel17);
            DarkGroupBox6.Controls.Add(cmbSkill1);
            DarkGroupBox6.Controls.Add(DarkLabel16);
            DarkGroupBox6.ForeColor = Color.Gainsboro;
            DarkGroupBox6.Location = new Point(10, 750);
            DarkGroupBox6.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox6.Name = "DarkGroupBox6";
            DarkGroupBox6.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox6.Size = new Size(663, 147);
            DarkGroupBox6.TabIndex = 10;
            DarkGroupBox6.TabStop = false;
            DarkGroupBox6.Text = "Start Skills";
            // 
            // cmbSkill4
            // 
            cmbSkill4.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill4.FormattingEnabled = true;
            cmbSkill4.Location = new Point(414, 88);
            cmbSkill4.Margin = new Padding(6, 5, 6, 5);
            cmbSkill4.Name = "cmbSkill4";
            cmbSkill4.Size = new Size(227, 32);
            cmbSkill4.TabIndex = 7;
            // 
            // DarkLabel19
            // 
            DarkLabel19.AutoSize = true;
            DarkLabel19.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel19.Location = new Point(342, 95);
            DarkLabel19.Margin = new Padding(6, 0, 6, 0);
            DarkLabel19.Name = "DarkLabel19";
            DarkLabel19.Size = new Size(62, 25);
            DarkLabel19.TabIndex = 6;
            DarkLabel19.Text = "Skill 4:";
            // 
            // cmbSkill3
            // 
            cmbSkill3.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill3.FormattingEnabled = true;
            cmbSkill3.Location = new Point(80, 88);
            cmbSkill3.Margin = new Padding(6, 5, 6, 5);
            cmbSkill3.Name = "cmbSkill3";
            cmbSkill3.Size = new Size(227, 32);
            cmbSkill3.TabIndex = 5;
            // 
            // DarkLabel18
            // 
            DarkLabel18.AutoSize = true;
            DarkLabel18.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel18.Location = new Point(7, 95);
            DarkLabel18.Margin = new Padding(6, 0, 6, 0);
            DarkLabel18.Name = "DarkLabel18";
            DarkLabel18.Size = new Size(62, 25);
            DarkLabel18.TabIndex = 4;
            DarkLabel18.Text = "Skill 3:";
            // 
            // cmbSkill2
            // 
            cmbSkill2.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill2.FormattingEnabled = true;
            cmbSkill2.Location = new Point(414, 37);
            cmbSkill2.Margin = new Padding(6, 5, 6, 5);
            cmbSkill2.Name = "cmbSkill2";
            cmbSkill2.Size = new Size(227, 32);
            cmbSkill2.TabIndex = 3;
            // 
            // DarkLabel17
            // 
            DarkLabel17.AutoSize = true;
            DarkLabel17.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel17.Location = new Point(342, 41);
            DarkLabel17.Margin = new Padding(6, 0, 6, 0);
            DarkLabel17.Name = "DarkLabel17";
            DarkLabel17.Size = new Size(62, 25);
            DarkLabel17.TabIndex = 2;
            DarkLabel17.Text = "Skill 2:";
            // 
            // cmbSkill1
            // 
            cmbSkill1.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill1.FormattingEnabled = true;
            cmbSkill1.Location = new Point(80, 37);
            cmbSkill1.Margin = new Padding(6, 5, 6, 5);
            cmbSkill1.Name = "cmbSkill1";
            cmbSkill1.Size = new Size(227, 32);
            cmbSkill1.TabIndex = 1;
            // 
            // DarkLabel16
            // 
            DarkLabel16.AutoSize = true;
            DarkLabel16.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel16.Location = new Point(7, 41);
            DarkLabel16.Margin = new Padding(6, 0, 6, 0);
            DarkLabel16.Name = "DarkLabel16";
            DarkLabel16.Size = new Size(62, 25);
            DarkLabel16.TabIndex = 0;
            DarkLabel16.Text = "Skill 1:";
            // 
            // DarkGroupBox4
            // 
            DarkGroupBox4.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox4.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox4.Controls.Add(pnlPetlevel);
            DarkGroupBox4.Controls.Add(optDoNotLevel);
            DarkGroupBox4.Controls.Add(optLevel);
            DarkGroupBox4.ForeColor = Color.Gainsboro;
            DarkGroupBox4.Location = new Point(10, 420);
            DarkGroupBox4.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox4.Size = new Size(663, 320);
            DarkGroupBox4.TabIndex = 9;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Leveling";
            // 
            // pnlPetlevel
            // 
            pnlPetlevel.Controls.Add(DarkGroupBox5);
            pnlPetlevel.Controls.Add(nudMaxLevel);
            pnlPetlevel.Controls.Add(DarkLabel12);
            pnlPetlevel.Controls.Add(nudPetPnts);
            pnlPetlevel.Controls.Add(DarkLabel13);
            pnlPetlevel.Controls.Add(nudPetExp);
            pnlPetlevel.Controls.Add(DarkLabel11);
            pnlPetlevel.Location = new Point(10, 80);
            pnlPetlevel.Margin = new Padding(6, 5, 6, 5);
            pnlPetlevel.Name = "pnlPetlevel";
            pnlPetlevel.Size = new Size(643, 227);
            pnlPetlevel.TabIndex = 2;
            // 
            // DarkGroupBox5
            // 
            DarkGroupBox5.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox5.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox5.Controls.Add(cmbEvolve);
            DarkGroupBox5.Controls.Add(DarkLabel15);
            DarkGroupBox5.Controls.Add(nudEvolveLvl);
            DarkGroupBox5.Controls.Add(DarkLabel14);
            DarkGroupBox5.Controls.Add(chkEvolve);
            DarkGroupBox5.ForeColor = Color.Gainsboro;
            DarkGroupBox5.Location = new Point(10, 73);
            DarkGroupBox5.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox5.Name = "DarkGroupBox5";
            DarkGroupBox5.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox5.Size = new Size(622, 145);
            DarkGroupBox5.TabIndex = 7;
            DarkGroupBox5.TabStop = false;
            DarkGroupBox5.Text = "Evolution";
            // 
            // cmbEvolve
            // 
            cmbEvolve.DrawMode = DrawMode.OwnerDrawFixed;
            cmbEvolve.FormattingEnabled = true;
            cmbEvolve.Location = new Point(167, 87);
            cmbEvolve.Margin = new Padding(6, 5, 6, 5);
            cmbEvolve.Name = "cmbEvolve";
            cmbEvolve.Size = new Size(442, 32);
            cmbEvolve.TabIndex = 4;
            // 
            // DarkLabel15
            // 
            DarkLabel15.AutoSize = true;
            DarkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel15.Location = new Point(10, 91);
            DarkLabel15.Margin = new Padding(6, 0, 6, 0);
            DarkLabel15.Name = "DarkLabel15";
            DarkLabel15.Size = new Size(111, 25);
            DarkLabel15.TabIndex = 3;
            DarkLabel15.Text = "Evolves into:";
            // 
            // nudEvolveLvl
            // 
            nudEvolveLvl.Location = new Point(508, 41);
            nudEvolveLvl.Margin = new Padding(6, 5, 6, 5);
            nudEvolveLvl.Name = "nudEvolveLvl";
            nudEvolveLvl.Size = new Size(103, 31);
            nudEvolveLvl.TabIndex = 2;
            // 
            // DarkLabel14
            // 
            DarkLabel14.AutoSize = true;
            DarkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel14.Location = new Point(344, 45);
            DarkLabel14.Margin = new Padding(6, 0, 6, 0);
            DarkLabel14.Name = "DarkLabel14";
            DarkLabel14.Size = new Size(145, 25);
            DarkLabel14.TabIndex = 1;
            DarkLabel14.Text = "Evolves on Level:";
            // 
            // chkEvolve
            // 
            chkEvolve.AutoSize = true;
            chkEvolve.Location = new Point(10, 37);
            chkEvolve.Margin = new Padding(6, 5, 6, 5);
            chkEvolve.Name = "chkEvolve";
            chkEvolve.Size = new Size(153, 29);
            chkEvolve.TabIndex = 0;
            chkEvolve.Text = "Pet Can Evolve";
            // 
            // nudMaxLevel
            // 
            nudMaxLevel.Location = new Point(542, 23);
            nudMaxLevel.Margin = new Padding(6, 5, 6, 5);
            nudMaxLevel.Name = "nudMaxLevel";
            nudMaxLevel.Size = new Size(78, 31);
            nudMaxLevel.TabIndex = 6;
            nudMaxLevel.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(433, 27);
            DarkLabel12.Margin = new Padding(6, 0, 6, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(93, 25);
            DarkLabel12.TabIndex = 5;
            DarkLabel12.Text = "Max Level:";
            // 
            // nudPetPnts
            // 
            nudPetPnts.Location = new Point(362, 23);
            nudPetPnts.Margin = new Padding(6, 5, 6, 5);
            nudPetPnts.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudPetPnts.Name = "nudPetPnts";
            nudPetPnts.Size = new Size(60, 31);
            nudPetPnts.TabIndex = 4;
            nudPetPnts.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(212, 27);
            DarkLabel13.Margin = new Padding(6, 0, 6, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(137, 25);
            DarkLabel13.TabIndex = 3;
            DarkLabel13.Text = "Points Per Level:";
            // 
            // nudPetExp
            // 
            nudPetExp.Location = new Point(123, 23);
            nudPetExp.Margin = new Padding(6, 5, 6, 5);
            nudPetExp.Name = "nudPetExp";
            nudPetExp.Size = new Size(78, 31);
            nudPetExp.TabIndex = 1;
            nudPetExp.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(7, 27);
            DarkLabel11.Margin = new Padding(6, 0, 6, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(104, 25);
            DarkLabel11.TabIndex = 0;
            DarkLabel11.Text = "Exp Gain %:";
            // 
            // optDoNotLevel
            // 
            optDoNotLevel.AutoSize = true;
            optDoNotLevel.Location = new Point(440, 37);
            optDoNotLevel.Margin = new Padding(6, 5, 6, 5);
            optDoNotLevel.Name = "optDoNotLevel";
            optDoNotLevel.Size = new Size(180, 29);
            optDoNotLevel.TabIndex = 1;
            optDoNotLevel.Text = "Does Not LevelUp";
            // 
            // optLevel
            // 
            optLevel.AutoSize = true;
            optLevel.Location = new Point(10, 37);
            optLevel.Margin = new Padding(6, 5, 6, 5);
            optLevel.Name = "optLevel";
            optLevel.Size = new Size(189, 29);
            optLevel.TabIndex = 0;
            optLevel.Text = "Level by Experience";
            // 
            // DarkGroupBox3
            // 
            DarkGroupBox3.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox3.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox3.Controls.Add(pnlCustomStats);
            DarkGroupBox3.Controls.Add(optAdoptStats);
            DarkGroupBox3.Controls.Add(optCustomStats);
            DarkGroupBox3.ForeColor = Color.Gainsboro;
            DarkGroupBox3.Location = new Point(10, 148);
            DarkGroupBox3.Margin = new Padding(6, 5, 6, 5);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(6, 5, 6, 5);
            DarkGroupBox3.Size = new Size(660, 259);
            DarkGroupBox3.TabIndex = 8;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Starting Stats";
            // 
            // pnlCustomStats
            // 
            pnlCustomStats.Controls.Add(nudLevel);
            pnlCustomStats.Controls.Add(DarkLabel10);
            pnlCustomStats.Controls.Add(nudSpirit);
            pnlCustomStats.Controls.Add(DarkLabel7);
            pnlCustomStats.Controls.Add(nudIntelligence);
            pnlCustomStats.Controls.Add(DarkLabel8);
            pnlCustomStats.Controls.Add(nudLuck);
            pnlCustomStats.Controls.Add(DarkLabel9);
            pnlCustomStats.Controls.Add(nudVitality);
            pnlCustomStats.Controls.Add(DarkLabel6);
            pnlCustomStats.Controls.Add(nudStrength);
            pnlCustomStats.Controls.Add(DarkLabel4);
            pnlCustomStats.Location = new Point(10, 80);
            pnlCustomStats.Margin = new Padding(6, 5, 6, 5);
            pnlCustomStats.Name = "pnlCustomStats";
            pnlCustomStats.Size = new Size(640, 170);
            pnlCustomStats.TabIndex = 2;
            // 
            // nudLevel
            // 
            nudLevel.Location = new Point(99, 20);
            nudLevel.Margin = new Padding(6, 5, 6, 5);
            nudLevel.Name = "nudLevel";
            nudLevel.Size = new Size(90, 31);
            nudLevel.TabIndex = 13;
            // 
            // DarkLabel10
            // 
            DarkLabel10.AutoSize = true;
            DarkLabel10.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel10.Location = new Point(7, 26);
            DarkLabel10.Margin = new Padding(6, 0, 6, 0);
            DarkLabel10.Name = "DarkLabel10";
            DarkLabel10.Size = new Size(55, 25);
            DarkLabel10.TabIndex = 12;
            DarkLabel10.Text = "Level:";
            // 
            // nudSpirit
            // 
            nudSpirit.Location = new Point(542, 66);
            nudSpirit.Margin = new Padding(6, 5, 6, 5);
            nudSpirit.Name = "nudSpirit";
            nudSpirit.Size = new Size(90, 31);
            nudSpirit.TabIndex = 11;
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(450, 70);
            DarkLabel7.Margin = new Padding(6, 0, 6, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(57, 25);
            DarkLabel7.TabIndex = 10;
            DarkLabel7.Text = "Spirit:";
            // 
            // nudIntelligence
            // 
            nudIntelligence.Location = new Point(332, 66);
            nudIntelligence.Margin = new Padding(6, 5, 6, 5);
            nudIntelligence.Name = "nudIntelligence";
            nudIntelligence.Size = new Size(90, 31);
            nudIntelligence.TabIndex = 9;
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(220, 70);
            DarkLabel8.Margin = new Padding(6, 0, 6, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(105, 25);
            DarkLabel8.TabIndex = 8;
            DarkLabel8.Text = "Intelligence:";
            // 
            // nudLuck
            // 
            nudLuck.Location = new Point(98, 66);
            nudLuck.Margin = new Padding(6, 5, 6, 5);
            nudLuck.Name = "nudLuck";
            nudLuck.Size = new Size(90, 31);
            nudLuck.TabIndex = 7;
            // 
            // DarkLabel9
            // 
            DarkLabel9.AutoSize = true;
            DarkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel9.Location = new Point(7, 70);
            DarkLabel9.Margin = new Padding(6, 0, 6, 0);
            DarkLabel9.Name = "DarkLabel9";
            DarkLabel9.Size = new Size(51, 25);
            DarkLabel9.TabIndex = 6;
            DarkLabel9.Text = "Luck:";
            // 
            // nudVitality
            // 
            nudVitality.Location = new Point(542, 16);
            nudVitality.Margin = new Padding(6, 5, 6, 5);
            nudVitality.Name = "nudVitality";
            nudVitality.Size = new Size(90, 31);
            nudVitality.TabIndex = 5;
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(448, 20);
            DarkLabel6.Margin = new Padding(6, 0, 6, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(69, 25);
            DarkLabel6.TabIndex = 4;
            DarkLabel6.Text = "Vitality:";
            // 
            // nudStrength
            // 
            nudStrength.Location = new Point(334, 16);
            nudStrength.Margin = new Padding(6, 5, 6, 5);
            nudStrength.Name = "nudStrength";
            nudStrength.Size = new Size(90, 31);
            nudStrength.TabIndex = 1;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(242, 20);
            DarkLabel4.Margin = new Padding(6, 0, 6, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(83, 25);
            DarkLabel4.TabIndex = 0;
            DarkLabel4.Text = "Strength:";
            // 
            // optAdoptStats
            // 
            optAdoptStats.AutoSize = true;
            optAdoptStats.Location = new Point(448, 37);
            optAdoptStats.Margin = new Padding(6, 5, 6, 5);
            optAdoptStats.Name = "optAdoptStats";
            optAdoptStats.Size = new Size(200, 29);
            optAdoptStats.TabIndex = 1;
            optAdoptStats.TabStop = true;
            optAdoptStats.Text = "Adopt Owner's Stats";
            // 
            // optCustomStats
            // 
            optCustomStats.AutoSize = true;
            optCustomStats.Location = new Point(10, 37);
            optCustomStats.Margin = new Padding(6, 5, 6, 5);
            optCustomStats.Name = "optCustomStats";
            optCustomStats.Size = new Size(142, 29);
            optCustomStats.TabIndex = 0;
            optCustomStats.TabStop = true;
            optCustomStats.Text = "Custom Stats";
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(10, 91);
            DarkLabel3.Margin = new Padding(6, 0, 6, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(66, 25);
            DarkLabel3.TabIndex = 7;
            DarkLabel3.Text = "Range:";
            // 
            // nudRange
            // 
            nudRange.Location = new Point(114, 88);
            nudRange.Margin = new Padding(6, 5, 6, 5);
            nudRange.Name = "nudRange";
            nudRange.Size = new Size(127, 31);
            nudRange.TabIndex = 6;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(374, 91);
            DarkLabel2.Margin = new Padding(6, 0, 6, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(62, 25);
            DarkLabel2.TabIndex = 5;
            DarkLabel2.Text = "Sprite:";
            // 
            // nudSprite
            // 
            nudSprite.Location = new Point(458, 88);
            nudSprite.Margin = new Padding(6, 5, 6, 5);
            nudSprite.Name = "nudSprite";
            nudSprite.Size = new Size(127, 31);
            nudSprite.TabIndex = 4;
            // 
            // picSprite
            // 
            picSprite.BackColor = Color.Black;
            picSprite.Location = new Point(590, 45);
            picSprite.Margin = new Padding(6, 5, 6, 5);
            picSprite.Name = "picSprite";
            picSprite.Size = new Size(46, 53);
            picSprite.TabIndex = 3;
            picSprite.TabStop = false;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(114, 34);
            txtName.Margin = new Padding(6, 5, 6, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(464, 31);
            txtName.TabIndex = 1;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(10, 38);
            DarkLabel1.Margin = new Padding(6, 0, 6, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(63, 25);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(13, 759);
            btnSave.Margin = new Padding(6, 5, 6, 5);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(8, 9, 8, 9);
            btnSave.Size = new Size(330, 45);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(13, 868);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(8, 9, 8, 9);
            btnCancel.Size = new Size(330, 45);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(13, 813);
            btnDelete.Margin = new Padding(6, 5, 6, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(8, 9, 8, 9);
            btnDelete.Size = new Size(330, 45);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            // 
            // frmEditor_Pet
            // 
            AutoScaleDimensions = new SizeF(10.0f, 25.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1052, 923);
            Controls.Add(btnDelete);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(6, 5, 6, 5);
            Name = "frmEditor_Pet";
            Text = "Pet Editor";
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            DarkGroupBox6.ResumeLayout(false);
            DarkGroupBox6.PerformLayout();
            DarkGroupBox4.ResumeLayout(false);
            DarkGroupBox4.PerformLayout();
            pnlPetlevel.ResumeLayout(false);
            pnlPetlevel.PerformLayout();
            DarkGroupBox5.ResumeLayout(false);
            DarkGroupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudEvolveLvl).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPetPnts).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPetExp).EndInit();
            DarkGroupBox3.ResumeLayout(false);
            DarkGroupBox3.PerformLayout();
            pnlCustomStats.ResumeLayout(false);
            pnlCustomStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSpirit).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIntelligence).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLuck).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVitality).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRange).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSprite).EndInit();
            Load += new EventHandler(frmEditor_Pet_Load);
            FormClosing += new FormClosingEventHandler(frmEditor_Pet_FormClosing);
            ResumeLayout(false);

        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal PictureBox picSprite;
        internal DarkUI.Controls.DarkLabel DarkLabel2;
        internal DarkUI.Controls.DarkNumericUpDown nudSprite;
        internal DarkUI.Controls.DarkLabel DarkLabel3;
        internal DarkUI.Controls.DarkNumericUpDown nudRange;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox3;
        internal DarkUI.Controls.DarkRadioButton optCustomStats;
        internal DarkUI.Controls.DarkRadioButton optAdoptStats;
        internal Panel pnlCustomStats;
        internal DarkUI.Controls.DarkNumericUpDown nudVitality;
        internal DarkUI.Controls.DarkLabel DarkLabel6;
        internal DarkUI.Controls.DarkNumericUpDown nudStrength;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal DarkUI.Controls.DarkNumericUpDown nudSpirit;
        internal DarkUI.Controls.DarkLabel DarkLabel7;
        internal DarkUI.Controls.DarkNumericUpDown nudIntelligence;
        internal DarkUI.Controls.DarkLabel DarkLabel8;
        internal DarkUI.Controls.DarkNumericUpDown nudLuck;
        internal DarkUI.Controls.DarkLabel DarkLabel9;
        internal DarkUI.Controls.DarkNumericUpDown nudLevel;
        internal DarkUI.Controls.DarkLabel DarkLabel10;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox4;
        internal DarkUI.Controls.DarkRadioButton optLevel;
        internal DarkUI.Controls.DarkRadioButton optDoNotLevel;
        internal Panel pnlPetlevel;
        internal DarkUI.Controls.DarkNumericUpDown nudPetExp;
        internal DarkUI.Controls.DarkLabel DarkLabel11;
        internal DarkUI.Controls.DarkLabel DarkLabel13;
        internal DarkUI.Controls.DarkNumericUpDown nudMaxLevel;
        internal DarkUI.Controls.DarkLabel DarkLabel12;
        internal DarkUI.Controls.DarkNumericUpDown nudPetPnts;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox5;
        internal DarkUI.Controls.DarkCheckBox chkEvolve;
        internal DarkUI.Controls.DarkLabel DarkLabel14;
        internal DarkUI.Controls.DarkNumericUpDown nudEvolveLvl;
        internal DarkUI.Controls.DarkLabel DarkLabel15;
        internal DarkUI.Controls.DarkComboBox cmbEvolve;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox6;
        internal DarkUI.Controls.DarkComboBox cmbSkill4;
        internal DarkUI.Controls.DarkLabel DarkLabel19;
        internal DarkUI.Controls.DarkComboBox cmbSkill3;
        internal DarkUI.Controls.DarkLabel DarkLabel18;
        internal DarkUI.Controls.DarkComboBox cmbSkill2;
        internal DarkUI.Controls.DarkLabel DarkLabel17;
        internal DarkUI.Controls.DarkComboBox cmbSkill1;
        internal DarkUI.Controls.DarkLabel DarkLabel16;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkButton btnCancel;
        internal DarkUI.Controls.DarkButton btnDelete;
    }
}