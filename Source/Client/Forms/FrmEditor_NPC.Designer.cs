using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_NPC : Form
    {

        // Shared instance of the form
        private static frmEditor_NPC _instance;

        // Public property to get the shared instance
        public static frmEditor_NPC Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_NPC();
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
            cmbSpawnPeriod = new DarkUI.Controls.DarkComboBox();
            DarkLabel30 = new DarkUI.Controls.DarkLabel();
            nudSpawnSecs = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            nudDamage = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            nudLevel = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            nudExp = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel10 = new DarkUI.Controls.DarkLabel();
            nudHp = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel9 = new DarkUI.Controls.DarkLabel();
            cmbFaction = new DarkUI.Controls.DarkComboBox();
            DarkLabel8 = new DarkUI.Controls.DarkLabel();
            cmbBehaviour = new DarkUI.Controls.DarkComboBox();
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            cmbAnimation = new DarkUI.Controls.DarkComboBox();
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            nudRange = new DarkUI.Controls.DarkNumericUpDown();
            nudSprite = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            txtAttackSay = new DarkUI.Controls.DarkTextBox();
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            picSprite = new PictureBox();
            txtName = new DarkUI.Controls.DarkTextBox();
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            cmbSkill6 = new DarkUI.Controls.DarkComboBox();
            DarkLabel17 = new DarkUI.Controls.DarkLabel();
            cmbSkill5 = new DarkUI.Controls.DarkComboBox();
            DarkLabel18 = new DarkUI.Controls.DarkLabel();
            cmbSkill4 = new DarkUI.Controls.DarkComboBox();
            DarkLabel19 = new DarkUI.Controls.DarkLabel();
            cmbSkill3 = new DarkUI.Controls.DarkComboBox();
            DarkLabel16 = new DarkUI.Controls.DarkLabel();
            cmbSkill2 = new DarkUI.Controls.DarkComboBox();
            DarkLabel15 = new DarkUI.Controls.DarkLabel();
            cmbSkill1 = new DarkUI.Controls.DarkComboBox();
            DarkLabel14 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            nudAmount = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel29 = new DarkUI.Controls.DarkLabel();
            cmbItem = new DarkUI.Controls.DarkComboBox();
            DarkLabel28 = new DarkUI.Controls.DarkLabel();
            cmbDropSlot = new DarkUI.Controls.DarkComboBox();
            nudChance = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel27 = new DarkUI.Controls.DarkLabel();
            DarkLabel26 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            nudSpirit = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel23 = new DarkUI.Controls.DarkLabel();
            nudIntelligence = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel24 = new DarkUI.Controls.DarkLabel();
            nudLuck = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel25 = new DarkUI.Controls.DarkLabel();
            nudVitality = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel22 = new DarkUI.Controls.DarkLabel();
            nudStrength = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel20 = new DarkUI.Controls.DarkLabel();
            btnCancel = new DarkUI.Controls.DarkButton();
            btnDelete = new DarkUI.Controls.DarkButton();
            btnSave = new DarkUI.Controls.DarkButton();
            DarkGroupBox1.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSpawnSecs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDamage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudExp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSprite).BeginInit();
            DarkGroupBox3.SuspendLayout();
            DarkGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAmount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudChance).BeginInit();
            DarkGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSpirit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIntelligence).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLuck).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVitality).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStrength).BeginInit();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(4, 2);
            DarkGroupBox1.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Size = new Size(265, 451);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "NPC List";
            // 
            // lstIndex
            // 
            lstIndex.BackColor = Color.FromArgb(45, 45, 48);
            lstIndex.BorderStyle = BorderStyle.FixedSingle;
            lstIndex.ForeColor = Color.Gainsboro;
            lstIndex.FormattingEnabled = true;
            lstIndex.Location = new Point(5, 18);
            lstIndex.Margin = new Padding(4, 3, 4, 3);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(255, 422);
            lstIndex.TabIndex = 2;
            lstIndex.Click += lstIndex_Click;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(cmbSpawnPeriod);
            DarkGroupBox2.Controls.Add(DarkLabel30);
            DarkGroupBox2.Controls.Add(nudSpawnSecs);
            DarkGroupBox2.Controls.Add(DarkLabel13);
            DarkGroupBox2.Controls.Add(nudDamage);
            DarkGroupBox2.Controls.Add(DarkLabel12);
            DarkGroupBox2.Controls.Add(nudLevel);
            DarkGroupBox2.Controls.Add(DarkLabel11);
            DarkGroupBox2.Controls.Add(nudExp);
            DarkGroupBox2.Controls.Add(DarkLabel10);
            DarkGroupBox2.Controls.Add(nudHp);
            DarkGroupBox2.Controls.Add(DarkLabel9);
            DarkGroupBox2.Controls.Add(cmbFaction);
            DarkGroupBox2.Controls.Add(DarkLabel8);
            DarkGroupBox2.Controls.Add(cmbBehaviour);
            DarkGroupBox2.Controls.Add(DarkLabel5);
            DarkGroupBox2.Controls.Add(cmbAnimation);
            DarkGroupBox2.Controls.Add(DarkLabel7);
            DarkGroupBox2.Controls.Add(DarkLabel4);
            DarkGroupBox2.Controls.Add(nudRange);
            DarkGroupBox2.Controls.Add(nudSprite);
            DarkGroupBox2.Controls.Add(DarkLabel3);
            DarkGroupBox2.Controls.Add(txtAttackSay);
            DarkGroupBox2.Controls.Add(DarkLabel2);
            DarkGroupBox2.Controls.Add(picSprite);
            DarkGroupBox2.Controls.Add(txtName);
            DarkGroupBox2.Controls.Add(DarkLabel1);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(275, 2);
            DarkGroupBox2.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Size = new Size(460, 267);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // cmbSpawnPeriod
            // 
            cmbSpawnPeriod.DrawMode = DrawMode.OwnerDrawVariable;
            cmbSpawnPeriod.FormattingEnabled = true;
            cmbSpawnPeriod.Items.AddRange(new object[] { "Always", "Day", "Night", "Dawn", "Dusk" });
            cmbSpawnPeriod.Location = new Point(332, 233);
            cmbSpawnPeriod.Margin = new Padding(4, 3, 4, 3);
            cmbSpawnPeriod.Name = "cmbSpawnPeriod";
            cmbSpawnPeriod.Size = new Size(117, 24);
            cmbSpawnPeriod.TabIndex = 38;
            cmbSpawnPeriod.SelectedIndexChanged += CmbSpawnPeriod_SelectedIndexChanged;
            // 
            // DarkLabel30
            // 
            DarkLabel30.AutoSize = true;
            DarkLabel30.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel30.Location = new Point(274, 237);
            DarkLabel30.Margin = new Padding(4, 0, 4, 0);
            DarkLabel30.Name = "DarkLabel30";
            DarkLabel30.Size = new Size(50, 15);
            DarkLabel30.TabIndex = 37;
            DarkLabel30.Text = "Spawns:";
            // 
            // nudSpawnSecs
            // 
            nudSpawnSecs.Location = new Point(174, 235);
            nudSpawnSecs.Margin = new Padding(4, 3, 4, 3);
            nudSpawnSecs.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudSpawnSecs.Name = "nudSpawnSecs";
            nudSpawnSecs.Size = new Size(97, 23);
            nudSpawnSecs.TabIndex = 36;
            nudSpawnSecs.ValueChanged += NudSpawnSecs_ValueChanged;
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(7, 237);
            DarkLabel13.Margin = new Padding(4, 0, 4, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(147, 15);
            DarkLabel13.TabIndex = 35;
            DarkLabel13.Text = "Respawn Time in Seconds:";
            // 
            // nudDamage
            // 
            nudDamage.Location = new Point(310, 205);
            nudDamage.Margin = new Padding(4, 3, 4, 3);
            nudDamage.Name = "nudDamage";
            nudDamage.Size = new Size(140, 23);
            nudDamage.TabIndex = 34;
            nudDamage.ValueChanged += NudDamage_ValueChanged;
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(217, 207);
            DarkLabel12.Margin = new Padding(4, 0, 4, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(81, 15);
            DarkLabel12.TabIndex = 33;
            DarkLabel12.Text = "Base Damage:";
            // 
            // nudLevel
            // 
            nudLevel.Location = new Point(70, 205);
            nudLevel.Margin = new Padding(4, 3, 4, 3);
            nudLevel.Name = "nudLevel";
            nudLevel.Size = new Size(140, 23);
            nudLevel.TabIndex = 32;
            nudLevel.ValueChanged += NudLevel_ValueChanged;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(7, 207);
            DarkLabel11.Margin = new Padding(4, 0, 4, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(37, 15);
            DarkLabel11.TabIndex = 31;
            DarkLabel11.Text = "Level:";
            // 
            // nudExp
            // 
            nudExp.Location = new Point(278, 175);
            nudExp.Margin = new Padding(4, 3, 4, 3);
            nudExp.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudExp.Name = "nudExp";
            nudExp.Size = new Size(173, 23);
            nudExp.TabIndex = 30;
            nudExp.ValueChanged += NudExp_ValueChanged;
            // 
            // DarkLabel10
            // 
            DarkLabel10.AutoSize = true;
            DarkLabel10.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel10.Location = new Point(202, 177);
            DarkLabel10.Margin = new Padding(4, 0, 4, 0);
            DarkLabel10.Name = "DarkLabel10";
            DarkLabel10.Size = new Size(61, 15);
            DarkLabel10.TabIndex = 29;
            DarkLabel10.Text = "Exp Given:";
            // 
            // nudHp
            // 
            nudHp.Location = new Point(70, 175);
            nudHp.Margin = new Padding(4, 3, 4, 3);
            nudHp.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudHp.Name = "nudHp";
            nudHp.Size = new Size(125, 23);
            nudHp.TabIndex = 28;
            nudHp.ValueChanged += NudHp_ValueChanged;
            // 
            // DarkLabel9
            // 
            DarkLabel9.AutoSize = true;
            DarkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel9.Location = new Point(7, 177);
            DarkLabel9.Margin = new Padding(4, 0, 4, 0);
            DarkLabel9.Name = "DarkLabel9";
            DarkLabel9.Size = new Size(45, 15);
            DarkLabel9.TabIndex = 27;
            DarkLabel9.Text = "Health:";
            // 
            // cmbFaction
            // 
            cmbFaction.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFaction.FormattingEnabled = true;
            cmbFaction.Items.AddRange(new object[] { "None", "Good", "Bad" });
            cmbFaction.Location = new Point(302, 143);
            cmbFaction.Margin = new Padding(4, 3, 4, 3);
            cmbFaction.Name = "cmbFaction";
            cmbFaction.Size = new Size(148, 24);
            cmbFaction.TabIndex = 26;
            cmbFaction.SelectedIndexChanged += CmbFaction_SelectedIndexChanged;
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(239, 147);
            DarkLabel8.Margin = new Padding(4, 0, 4, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(49, 15);
            DarkLabel8.TabIndex = 25;
            DarkLabel8.Text = "Faction:";
            // 
            // cmbBehaviour
            // 
            cmbBehaviour.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBehaviour.FormattingEnabled = true;
            cmbBehaviour.Items.AddRange(new object[] { "Attack on sight", "Attack when attacked", "Friendly", "Shop keeper", "Guard", "Quest" });
            cmbBehaviour.Location = new Point(70, 143);
            cmbBehaviour.Margin = new Padding(4, 3, 4, 3);
            cmbBehaviour.Name = "cmbBehaviour";
            cmbBehaviour.Size = new Size(162, 24);
            cmbBehaviour.TabIndex = 24;
            cmbBehaviour.SelectedIndexChanged += CmbBehavior_SelectedIndexChanged;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(7, 147);
            DarkLabel5.Margin = new Padding(4, 0, 4, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(56, 15);
            DarkLabel5.TabIndex = 23;
            DarkLabel5.Text = "Behavior:";
            // 
            // cmbAnimation
            // 
            cmbAnimation.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnimation.FormattingEnabled = true;
            cmbAnimation.Location = new Point(84, 113);
            cmbAnimation.Margin = new Padding(4, 3, 4, 3);
            cmbAnimation.Name = "cmbAnimation";
            cmbAnimation.Size = new Size(125, 24);
            cmbAnimation.TabIndex = 21;
            cmbAnimation.SelectedIndexChanged += CmbAnimation_SelectedIndexChanged;
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(7, 115);
            DarkLabel7.Margin = new Padding(4, 0, 4, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(66, 15);
            DarkLabel7.TabIndex = 20;
            DarkLabel7.Text = "Animation:";
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(7, 85);
            DarkLabel4.Margin = new Padding(4, 0, 4, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(43, 15);
            DarkLabel4.TabIndex = 15;
            DarkLabel4.Text = "Range:";
            // 
            // nudRange
            // 
            nudRange.Location = new Point(70, 82);
            nudRange.Margin = new Padding(4, 3, 4, 3);
            nudRange.Name = "nudRange";
            nudRange.Size = new Size(126, 23);
            nudRange.TabIndex = 14;
            nudRange.ValueChanged += NudRange_ValueChanged;
            // 
            // nudSprite
            // 
            nudSprite.Location = new Point(253, 82);
            nudSprite.Margin = new Padding(4, 3, 4, 3);
            nudSprite.Name = "nudSprite";
            nudSprite.Size = new Size(112, 23);
            nudSprite.TabIndex = 13;
            nudSprite.Click += NudSprite_Click;
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(203, 85);
            DarkLabel3.Margin = new Padding(4, 0, 4, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(40, 15);
            DarkLabel3.TabIndex = 12;
            DarkLabel3.Text = "Sprite:";
            // 
            // txtAttackSay
            // 
            txtAttackSay.BackColor = Color.FromArgb(69, 73, 74);
            txtAttackSay.BorderStyle = BorderStyle.FixedSingle;
            txtAttackSay.ForeColor = Color.FromArgb(220, 220, 220);
            txtAttackSay.Location = new Point(70, 52);
            txtAttackSay.Margin = new Padding(4, 3, 4, 3);
            txtAttackSay.Name = "txtAttackSay";
            txtAttackSay.Size = new Size(295, 23);
            txtAttackSay.TabIndex = 11;
            txtAttackSay.TextChanged += TxtAttackSay_TextChanged;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(7, 55);
            DarkLabel2.Margin = new Padding(4, 0, 4, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(28, 15);
            DarkLabel2.TabIndex = 10;
            DarkLabel2.Text = "Say:";
            // 
            // picSprite
            // 
            picSprite.BackColor = Color.Black;
            picSprite.BackgroundImageLayout = ImageLayout.None;
            picSprite.Location = new Point(372, 22);
            picSprite.Margin = new Padding(4, 3, 4, 3);
            picSprite.Name = "picSprite";
            picSprite.Size = new Size(32, 32);
            picSprite.TabIndex = 9;
            picSprite.TabStop = false;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(70, 22);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(295, 23);
            txtName.TabIndex = 1;
            txtName.TextChanged += TxtName_TextChanged;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(7, 25);
            DarkLabel1.Margin = new Padding(4, 0, 4, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(42, 15);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // DarkGroupBox3
            // 
            DarkGroupBox3.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox3.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox3.Controls.Add(cmbSkill6);
            DarkGroupBox3.Controls.Add(DarkLabel17);
            DarkGroupBox3.Controls.Add(cmbSkill5);
            DarkGroupBox3.Controls.Add(DarkLabel18);
            DarkGroupBox3.Controls.Add(cmbSkill4);
            DarkGroupBox3.Controls.Add(DarkLabel19);
            DarkGroupBox3.Controls.Add(cmbSkill3);
            DarkGroupBox3.Controls.Add(DarkLabel16);
            DarkGroupBox3.Controls.Add(cmbSkill2);
            DarkGroupBox3.Controls.Add(DarkLabel15);
            DarkGroupBox3.Controls.Add(cmbSkill1);
            DarkGroupBox3.Controls.Add(DarkLabel14);
            DarkGroupBox3.ForeColor = Color.Gainsboro;
            DarkGroupBox3.Location = new Point(275, 275);
            DarkGroupBox3.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Size = new Size(460, 82);
            DarkGroupBox3.TabIndex = 2;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Skills";
            // 
            // cmbSkill6
            // 
            cmbSkill6.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill6.FormattingEnabled = true;
            cmbSkill6.Location = new Point(332, 46);
            cmbSkill6.Margin = new Padding(4, 3, 4, 3);
            cmbSkill6.Name = "cmbSkill6";
            cmbSkill6.Size = new Size(122, 24);
            cmbSkill6.TabIndex = 11;
            cmbSkill6.SelectedIndexChanged += CmbSkill6_SelectedIndexChanged;
            // 
            // DarkLabel17
            // 
            DarkLabel17.AutoSize = true;
            DarkLabel17.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel17.Location = new Point(310, 50);
            DarkLabel17.Margin = new Padding(4, 0, 4, 0);
            DarkLabel17.Name = "DarkLabel17";
            DarkLabel17.Size = new Size(13, 15);
            DarkLabel17.TabIndex = 10;
            DarkLabel17.Text = "6";
            // 
            // cmbSkill5
            // 
            cmbSkill5.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill5.FormattingEnabled = true;
            cmbSkill5.Location = new Point(181, 46);
            cmbSkill5.Margin = new Padding(4, 3, 4, 3);
            cmbSkill5.Name = "cmbSkill5";
            cmbSkill5.Size = new Size(122, 24);
            cmbSkill5.TabIndex = 9;
            cmbSkill5.SelectedIndexChanged += CmbSkill5_SelectedIndexChanged;
            // 
            // DarkLabel18
            // 
            DarkLabel18.AutoSize = true;
            DarkLabel18.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel18.Location = new Point(159, 50);
            DarkLabel18.Margin = new Padding(4, 0, 4, 0);
            DarkLabel18.Name = "DarkLabel18";
            DarkLabel18.Size = new Size(13, 15);
            DarkLabel18.TabIndex = 8;
            DarkLabel18.Text = "5";
            // 
            // cmbSkill4
            // 
            cmbSkill4.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill4.FormattingEnabled = true;
            cmbSkill4.Location = new Point(29, 46);
            cmbSkill4.Margin = new Padding(4, 3, 4, 3);
            cmbSkill4.Name = "cmbSkill4";
            cmbSkill4.Size = new Size(122, 24);
            cmbSkill4.TabIndex = 7;
            cmbSkill4.SelectedIndexChanged += CmbSkill4_SelectedIndexChanged;
            // 
            // DarkLabel19
            // 
            DarkLabel19.AutoSize = true;
            DarkLabel19.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel19.Location = new Point(7, 50);
            DarkLabel19.Margin = new Padding(4, 0, 4, 0);
            DarkLabel19.Name = "DarkLabel19";
            DarkLabel19.Size = new Size(13, 15);
            DarkLabel19.TabIndex = 6;
            DarkLabel19.Text = "4";
            // 
            // cmbSkill3
            // 
            cmbSkill3.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill3.FormattingEnabled = true;
            cmbSkill3.Location = new Point(332, 15);
            cmbSkill3.Margin = new Padding(4, 3, 4, 3);
            cmbSkill3.Name = "cmbSkill3";
            cmbSkill3.Size = new Size(122, 24);
            cmbSkill3.TabIndex = 5;
            cmbSkill3.SelectedIndexChanged += CmbSkill3_SelectedIndexChanged;
            // 
            // DarkLabel16
            // 
            DarkLabel16.AutoSize = true;
            DarkLabel16.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel16.Location = new Point(310, 18);
            DarkLabel16.Margin = new Padding(4, 0, 4, 0);
            DarkLabel16.Name = "DarkLabel16";
            DarkLabel16.Size = new Size(13, 15);
            DarkLabel16.TabIndex = 4;
            DarkLabel16.Text = "3";
            // 
            // cmbSkill2
            // 
            cmbSkill2.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill2.FormattingEnabled = true;
            cmbSkill2.Location = new Point(181, 15);
            cmbSkill2.Margin = new Padding(4, 3, 4, 3);
            cmbSkill2.Name = "cmbSkill2";
            cmbSkill2.Size = new Size(122, 24);
            cmbSkill2.TabIndex = 3;
            cmbSkill2.SelectedIndexChanged += CmbSkill2_SelectedIndexChanged;
            // 
            // DarkLabel15
            // 
            DarkLabel15.AutoSize = true;
            DarkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel15.Location = new Point(159, 18);
            DarkLabel15.Margin = new Padding(4, 0, 4, 0);
            DarkLabel15.Name = "DarkLabel15";
            DarkLabel15.Size = new Size(13, 15);
            DarkLabel15.TabIndex = 2;
            DarkLabel15.Text = "2";
            // 
            // cmbSkill1
            // 
            cmbSkill1.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkill1.FormattingEnabled = true;
            cmbSkill1.Location = new Point(29, 15);
            cmbSkill1.Margin = new Padding(4, 3, 4, 3);
            cmbSkill1.Name = "cmbSkill1";
            cmbSkill1.Size = new Size(122, 24);
            cmbSkill1.TabIndex = 1;
            cmbSkill1.SelectedIndexChanged += CmbSkill1_SelectedIndexChanged;
            // 
            // DarkLabel14
            // 
            DarkLabel14.AutoSize = true;
            DarkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel14.Location = new Point(7, 18);
            DarkLabel14.Margin = new Padding(4, 0, 4, 0);
            DarkLabel14.Name = "DarkLabel14";
            DarkLabel14.Size = new Size(13, 15);
            DarkLabel14.TabIndex = 0;
            DarkLabel14.Text = "1";
            // 
            // DarkGroupBox4
            // 
            DarkGroupBox4.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox4.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox4.Controls.Add(nudAmount);
            DarkGroupBox4.Controls.Add(DarkLabel29);
            DarkGroupBox4.Controls.Add(cmbItem);
            DarkGroupBox4.Controls.Add(DarkLabel28);
            DarkGroupBox4.Controls.Add(cmbDropSlot);
            DarkGroupBox4.Controls.Add(nudChance);
            DarkGroupBox4.Controls.Add(DarkLabel27);
            DarkGroupBox4.Controls.Add(DarkLabel26);
            DarkGroupBox4.ForeColor = Color.Gainsboro;
            DarkGroupBox4.Location = new Point(275, 462);
            DarkGroupBox4.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Size = new Size(460, 89);
            DarkGroupBox4.TabIndex = 3;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Drop Items";
            // 
            // nudAmount
            // 
            nudAmount.Location = new Point(313, 50);
            nudAmount.Margin = new Padding(4, 3, 4, 3);
            nudAmount.Name = "nudAmount";
            nudAmount.Size = new Size(140, 23);
            nudAmount.TabIndex = 7;
            nudAmount.ValueChanged += ScrlValue_Scroll;
            // 
            // DarkLabel29
            // 
            DarkLabel29.AutoSize = true;
            DarkLabel29.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel29.Location = new Point(239, 53);
            DarkLabel29.Margin = new Padding(4, 0, 4, 0);
            DarkLabel29.Name = "DarkLabel29";
            DarkLabel29.Size = new Size(54, 15);
            DarkLabel29.TabIndex = 6;
            DarkLabel29.Text = "Amount:";
            // 
            // cmbItem
            // 
            cmbItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbItem.FormattingEnabled = true;
            cmbItem.Location = new Point(77, 50);
            cmbItem.Margin = new Padding(4, 3, 4, 3);
            cmbItem.Name = "cmbItem";
            cmbItem.Size = new Size(140, 24);
            cmbItem.TabIndex = 5;
            cmbItem.SelectedIndexChanged += CmbItem_SelectedIndexChanged;
            // 
            // DarkLabel28
            // 
            DarkLabel28.AutoSize = true;
            DarkLabel28.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel28.Location = new Point(7, 53);
            DarkLabel28.Margin = new Padding(4, 0, 4, 0);
            DarkLabel28.Name = "DarkLabel28";
            DarkLabel28.Size = new Size(34, 15);
            DarkLabel28.TabIndex = 4;
            DarkLabel28.Text = "Item:";
            // 
            // cmbDropSlot
            // 
            cmbDropSlot.DrawMode = DrawMode.OwnerDrawFixed;
            cmbDropSlot.FormattingEnabled = true;
            cmbDropSlot.Items.AddRange(new object[] { "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5" });
            cmbDropSlot.Location = new Point(77, 15);
            cmbDropSlot.Margin = new Padding(4, 3, 4, 3);
            cmbDropSlot.Name = "cmbDropSlot";
            cmbDropSlot.Size = new Size(140, 24);
            cmbDropSlot.TabIndex = 3;
            cmbDropSlot.SelectedIndexChanged += CmbDropSlot_SelectedIndexChanged;
            // 
            // nudChance
            // 
            nudChance.Location = new Point(343, 16);
            nudChance.Margin = new Padding(4, 3, 4, 3);
            nudChance.Name = "nudChance";
            nudChance.Size = new Size(110, 23);
            nudChance.TabIndex = 2;
            nudChance.ValueChanged += NudChance_ValueChanged;
            // 
            // DarkLabel27
            // 
            DarkLabel27.AutoSize = true;
            DarkLabel27.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel27.Location = new Point(239, 18);
            DarkLabel27.Margin = new Padding(4, 0, 4, 0);
            DarkLabel27.Name = "DarkLabel27";
            DarkLabel27.Size = new Size(91, 15);
            DarkLabel27.TabIndex = 1;
            DarkLabel27.Text = "Chance 1 out of";
            // 
            // DarkLabel26
            // 
            DarkLabel26.AutoSize = true;
            DarkLabel26.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel26.Location = new Point(7, 18);
            DarkLabel26.Margin = new Padding(4, 0, 4, 0);
            DarkLabel26.Name = "DarkLabel26";
            DarkLabel26.Size = new Size(59, 15);
            DarkLabel26.TabIndex = 0;
            DarkLabel26.Text = "Drop Slot:";
            // 
            // DarkGroupBox5
            // 
            DarkGroupBox5.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox5.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox5.Controls.Add(nudSpirit);
            DarkGroupBox5.Controls.Add(DarkLabel23);
            DarkGroupBox5.Controls.Add(nudIntelligence);
            DarkGroupBox5.Controls.Add(DarkLabel24);
            DarkGroupBox5.Controls.Add(nudLuck);
            DarkGroupBox5.Controls.Add(DarkLabel25);
            DarkGroupBox5.Controls.Add(nudVitality);
            DarkGroupBox5.Controls.Add(DarkLabel22);
            DarkGroupBox5.Controls.Add(nudStrength);
            DarkGroupBox5.Controls.Add(DarkLabel20);
            DarkGroupBox5.ForeColor = Color.Gainsboro;
            DarkGroupBox5.Location = new Point(275, 365);
            DarkGroupBox5.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox5.Name = "DarkGroupBox5";
            DarkGroupBox5.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox5.Size = new Size(460, 90);
            DarkGroupBox5.TabIndex = 4;
            DarkGroupBox5.TabStop = false;
            DarkGroupBox5.Text = "Stats";
            // 
            // nudSpirit
            // 
            nudSpirit.Location = new Point(376, 25);
            nudSpirit.Margin = new Padding(4, 3, 4, 3);
            nudSpirit.Name = "nudSpirit";
            nudSpirit.Size = new Size(74, 23);
            nudSpirit.TabIndex = 11;
            nudSpirit.ValueChanged += NudSpirit_ValueChanged;
            // 
            // DarkLabel23
            // 
            DarkLabel23.AutoSize = true;
            DarkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel23.Location = new Point(313, 27);
            DarkLabel23.Margin = new Padding(4, 0, 4, 0);
            DarkLabel23.Name = "DarkLabel23";
            DarkLabel23.Size = new Size(37, 15);
            DarkLabel23.TabIndex = 10;
            DarkLabel23.Text = "Spirit:";
            // 
            // nudIntelligence
            // 
            nudIntelligence.Location = new Point(230, 52);
            nudIntelligence.Margin = new Padding(4, 3, 4, 3);
            nudIntelligence.Name = "nudIntelligence";
            nudIntelligence.Size = new Size(74, 23);
            nudIntelligence.TabIndex = 9;
            nudIntelligence.ValueChanged += NudIntelligence_ValueChanged;
            // 
            // DarkLabel24
            // 
            DarkLabel24.AutoSize = true;
            DarkLabel24.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel24.Location = new Point(150, 55);
            DarkLabel24.Margin = new Padding(4, 0, 4, 0);
            DarkLabel24.Name = "DarkLabel24";
            DarkLabel24.Size = new Size(71, 15);
            DarkLabel24.TabIndex = 8;
            DarkLabel24.Text = "Intelligence:";
            // 
            // nudLuck
            // 
            nudLuck.Location = new Point(70, 52);
            nudLuck.Margin = new Padding(4, 3, 4, 3);
            nudLuck.Name = "nudLuck";
            nudLuck.Size = new Size(74, 23);
            nudLuck.TabIndex = 7;
            nudLuck.ValueChanged += NudLuck_ValueChanged;
            // 
            // DarkLabel25
            // 
            DarkLabel25.AutoSize = true;
            DarkLabel25.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel25.Location = new Point(7, 55);
            DarkLabel25.Margin = new Padding(4, 0, 4, 0);
            DarkLabel25.Name = "DarkLabel25";
            DarkLabel25.Size = new Size(35, 15);
            DarkLabel25.TabIndex = 6;
            DarkLabel25.Text = "Luck:";
            // 
            // nudVitality
            // 
            nudVitality.Location = new Point(230, 23);
            nudVitality.Margin = new Padding(4, 3, 4, 3);
            nudVitality.Name = "nudVitality";
            nudVitality.Size = new Size(74, 23);
            nudVitality.TabIndex = 5;
            nudVitality.ValueChanged += NudVitality_ValueChanged;
            // 
            // DarkLabel22
            // 
            DarkLabel22.AutoSize = true;
            DarkLabel22.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel22.Location = new Point(167, 26);
            DarkLabel22.Margin = new Padding(4, 0, 4, 0);
            DarkLabel22.Name = "DarkLabel22";
            DarkLabel22.Size = new Size(46, 15);
            DarkLabel22.TabIndex = 4;
            DarkLabel22.Text = "Vitality:";
            // 
            // nudStrength
            // 
            nudStrength.Location = new Point(70, 22);
            nudStrength.Margin = new Padding(4, 3, 4, 3);
            nudStrength.Name = "nudStrength";
            nudStrength.Size = new Size(74, 23);
            nudStrength.TabIndex = 1;
            nudStrength.ValueChanged += NudStrength_ValueChanged;
            // 
            // DarkLabel20
            // 
            DarkLabel20.AutoSize = true;
            DarkLabel20.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel20.Location = new Point(7, 25);
            DarkLabel20.Margin = new Padding(4, 0, 4, 0);
            DarkLabel20.Name = "DarkLabel20";
            DarkLabel20.Size = new Size(55, 15);
            DarkLabel20.TabIndex = 0;
            DarkLabel20.Text = "Strength:";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(12, 524);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(255, 27);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(12, 491);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(6, 5, 6, 5);
            btnDelete.Size = new Size(255, 27);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 458);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(6, 5, 6, 5);
            btnSave.Size = new Size(255, 27);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save";
            btnSave.Click += BtnSave_Click;
            // 
            // frmEditor_NPC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(741, 557);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnCancel);
            Controls.Add(DarkGroupBox5);
            Controls.Add(DarkGroupBox4);
            Controls.Add(DarkGroupBox3);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmEditor_NPC";
            Text = "NPC Editor";
            FormClosing += frmEditor_NPC_FormClosing;
            Load += frmEditor_NPC_Load;
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSpawnSecs).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDamage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudExp).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHp).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRange).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSprite).EndInit();
            DarkGroupBox3.ResumeLayout(false);
            DarkGroupBox3.PerformLayout();
            DarkGroupBox4.ResumeLayout(false);
            DarkGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAmount).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudChance).EndInit();
            DarkGroupBox5.ResumeLayout(false);
            DarkGroupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSpirit).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIntelligence).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLuck).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVitality).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStrength).EndInit();
            ResumeLayout(false);

        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal PictureBox picSprite;
        internal DarkUI.Controls.DarkTextBox txtAttackSay;
        internal DarkUI.Controls.DarkLabel DarkLabel2;
        internal DarkUI.Controls.DarkNumericUpDown nudSprite;
        internal DarkUI.Controls.DarkLabel DarkLabel3;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal DarkUI.Controls.DarkNumericUpDown nudRange;
        internal DarkUI.Controls.DarkComboBox cmbAnimation;
        internal DarkUI.Controls.DarkLabel DarkLabel7;
        internal DarkUI.Controls.DarkComboBox cmbFaction;
        internal DarkUI.Controls.DarkLabel DarkLabel8;
        internal DarkUI.Controls.DarkComboBox cmbBehaviour;
        internal DarkUI.Controls.DarkLabel DarkLabel5;
        internal DarkUI.Controls.DarkNumericUpDown nudHp;
        internal DarkUI.Controls.DarkLabel DarkLabel9;
        internal DarkUI.Controls.DarkNumericUpDown nudExp;
        internal DarkUI.Controls.DarkLabel DarkLabel10;
        internal DarkUI.Controls.DarkNumericUpDown nudDamage;
        internal DarkUI.Controls.DarkLabel DarkLabel12;
        internal DarkUI.Controls.DarkNumericUpDown nudLevel;
        internal DarkUI.Controls.DarkLabel DarkLabel11;
        internal DarkUI.Controls.DarkNumericUpDown nudSpawnSecs;
        internal DarkUI.Controls.DarkLabel DarkLabel13;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox3;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox4;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox5;
        internal DarkUI.Controls.DarkComboBox cmbSkill1;
        internal DarkUI.Controls.DarkLabel DarkLabel14;
        internal DarkUI.Controls.DarkComboBox cmbSkill6;
        internal DarkUI.Controls.DarkLabel DarkLabel17;
        internal DarkUI.Controls.DarkComboBox cmbSkill5;
        internal DarkUI.Controls.DarkLabel DarkLabel18;
        internal DarkUI.Controls.DarkComboBox cmbSkill4;
        internal DarkUI.Controls.DarkLabel DarkLabel19;
        internal DarkUI.Controls.DarkComboBox cmbSkill3;
        internal DarkUI.Controls.DarkLabel DarkLabel16;
        internal DarkUI.Controls.DarkComboBox cmbSkill2;
        internal DarkUI.Controls.DarkLabel DarkLabel15;
        internal DarkUI.Controls.DarkNumericUpDown nudStrength;
        internal DarkUI.Controls.DarkLabel DarkLabel20;
        internal DarkUI.Controls.DarkNumericUpDown nudSpirit;
        internal DarkUI.Controls.DarkLabel DarkLabel23;
        internal DarkUI.Controls.DarkNumericUpDown nudIntelligence;
        internal DarkUI.Controls.DarkLabel DarkLabel24;
        internal DarkUI.Controls.DarkNumericUpDown nudLuck;
        internal DarkUI.Controls.DarkLabel DarkLabel25;
        internal DarkUI.Controls.DarkNumericUpDown nudVitality;
        internal DarkUI.Controls.DarkLabel DarkLabel22;
        internal DarkUI.Controls.DarkLabel DarkLabel26;
        internal DarkUI.Controls.DarkComboBox cmbDropSlot;
        internal DarkUI.Controls.DarkNumericUpDown nudChance;
        internal DarkUI.Controls.DarkLabel DarkLabel27;
        internal DarkUI.Controls.DarkNumericUpDown nudAmount;
        internal DarkUI.Controls.DarkLabel DarkLabel29;
        internal DarkUI.Controls.DarkComboBox cmbItem;
        internal DarkUI.Controls.DarkLabel DarkLabel28;
        internal DarkUI.Controls.DarkButton btnCancel;
        internal DarkUI.Controls.DarkButton btnDelete;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkComboBox cmbSpawnPeriod;
        internal DarkUI.Controls.DarkLabel DarkLabel30;
    }
}