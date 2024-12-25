using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_Item : Form
    {

        // Shared instance of the form
        private static frmEditor_Item _instance;

        // Public property to get the shared instance
        public static frmEditor_Item Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Item();
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
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            DarkLabel35 = new DarkUI.Controls.DarkLabel();
            fraEquipment = new DarkUI.Controls.DarkGroupBox();
            fraProjectile = new DarkUI.Controls.DarkGroupBox();
            cmbAmmo = new DarkUI.Controls.DarkComboBox();
            cmbAmmo.SelectedIndexChanged += new EventHandler(CmbAmmo_SelectedIndexChanged);
            DarkLabel25 = new DarkUI.Controls.DarkLabel();
            cmbProjectile = new DarkUI.Controls.DarkComboBox();
            cmbProjectile.SelectedIndexChanged += new EventHandler(CmbProjectile_SelectedIndexChanged);
            DarkLabel24 = new DarkUI.Controls.DarkLabel();
            nudPaperdoll = new DarkUI.Controls.DarkNumericUpDown();
            nudPaperdoll.Click += new EventHandler(NudPaperdoll_ValueChanged);
            DarkLabel23 = new DarkUI.Controls.DarkLabel();
            picPaperdoll = new PictureBox();
            cmbKnockBackTiles = new DarkUI.Controls.DarkComboBox();
            cmbKnockBackTiles.SelectedIndexChanged += new EventHandler(CmbKnockBackTiles_SelectedIndexChanged);
            DarkLabel16 = new DarkUI.Controls.DarkLabel();
            chkKnockBack = new DarkUI.Controls.DarkCheckBox();
            chkKnockBack.CheckedChanged += new EventHandler(ChkKnockBack_CheckedChanged);
            nudSpeed = new DarkUI.Controls.DarkNumericUpDown();
            nudSpeed.Click += new EventHandler(NudSpeed_ValueChanged);
            lblSpeed = new DarkUI.Controls.DarkLabel();
            nudDamage = new DarkUI.Controls.DarkNumericUpDown();
            nudDamage.Click += new EventHandler(NudDamage_ValueChanged);
            DarkLabel15 = new DarkUI.Controls.DarkLabel();
            cmbTool = new DarkUI.Controls.DarkComboBox();
            cmbTool.SelectedIndexChanged += new EventHandler(CmbTool_SelectedIndexChanged);
            DarkLabel14 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            nudSpirit = new DarkUI.Controls.DarkNumericUpDown();
            nudSpirit.Click += new EventHandler(NudSpirit_ValueChanged);
            DarkLabel22 = new DarkUI.Controls.DarkLabel();
            nudIntelligence = new DarkUI.Controls.DarkNumericUpDown();
            nudIntelligence.Click += new EventHandler(NudIntelligence_ValueChanged);
            DarkLabel21 = new DarkUI.Controls.DarkLabel();
            nudVitality = new DarkUI.Controls.DarkNumericUpDown();
            nudVitality.Click += new EventHandler(NudVitality_ValueChanged);
            DarkLabel20 = new DarkUI.Controls.DarkLabel();
            nudLuck = new DarkUI.Controls.DarkNumericUpDown();
            nudLuck.Click += new EventHandler(NudLuck_ValueChanged);
            DarkLabel19 = new DarkUI.Controls.DarkLabel();
            nudStrength = new DarkUI.Controls.DarkNumericUpDown();
            nudStrength.Click += new EventHandler(NudStrength_ValueChanged);
            DarkLabel17 = new DarkUI.Controls.DarkLabel();
            btnBasics = new DarkUI.Controls.DarkButton();
            btnBasics.Click += new EventHandler(BtnBasics_Click);
            btnRequirements = new DarkUI.Controls.DarkButton();
            btnRequirements.Click += new EventHandler(BtnRequirements_Click);
            fraRequirements = new DarkUI.Controls.DarkGroupBox();
            DarkLabel28 = new DarkUI.Controls.DarkLabel();
            nudLevelReq = new DarkUI.Controls.DarkNumericUpDown();
            nudLevelReq.Click += new EventHandler(NudLevelReq_ValueChanged);
            cmbAccessReq = new DarkUI.Controls.DarkComboBox();
            cmbAccessReq.SelectedIndexChanged += new EventHandler(CmbAccessReq_SelectedIndexChanged);
            DarkLabel27 = new DarkUI.Controls.DarkLabel();
            cmbJobReq = new DarkUI.Controls.DarkComboBox();
            cmbJobReq.SelectedIndexChanged += new EventHandler(CmbJobReq_SelectedIndexChanged);
            DarkLabel26 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            nudSprReq = new DarkUI.Controls.DarkNumericUpDown();
            nudSprReq.Click += new EventHandler(NudSprReq_ValueChanged);
            DarkLabel32 = new DarkUI.Controls.DarkLabel();
            nudIntReq = new DarkUI.Controls.DarkNumericUpDown();
            nudIntReq.Click += new EventHandler(NudIntReq_ValueChanged);
            DarkLabel33 = new DarkUI.Controls.DarkLabel();
            nudVitReq = new DarkUI.Controls.DarkNumericUpDown();
            nudVitReq.Click += new EventHandler(NudVitReq_ValueChanged);
            DarkLabel34 = new DarkUI.Controls.DarkLabel();
            nudLuckReq = new DarkUI.Controls.DarkNumericUpDown();
            nudLuckReq.Click += new EventHandler(NudLuckReq_ValueChanged);
            DarkLabel29 = new DarkUI.Controls.DarkLabel();
            nudStrReq = new DarkUI.Controls.DarkNumericUpDown();
            nudStrReq.Click += new EventHandler(NudStrReq_ValueChanged);
            DarkLabel31 = new DarkUI.Controls.DarkLabel();
            btnSave = new DarkUI.Controls.DarkButton();
            btnSave.Click += new EventHandler(BtnSave_Click);
            btnDelete = new DarkUI.Controls.DarkButton();
            btnDelete.Click += new EventHandler(BtnDelete_Click);
            btnCancel = new DarkUI.Controls.DarkButton();
            btnCancel.Click += new EventHandler(BtnCancel_Click);
            btnSpawn = new DarkUI.Controls.DarkButton();
            btnSpawn.Click += new EventHandler(btnSpawn_Click);
            nudSpanwAmount = new DarkUI.Controls.DarkNumericUpDown();
            fraSkill = new DarkUI.Controls.DarkGroupBox();
            cmbSkills = new DarkUI.Controls.DarkComboBox();
            cmbSkills.SelectedIndexChanged += new EventHandler(CmbSkills_SelectedIndexChanged);
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            fraVitals = new DarkUI.Controls.DarkGroupBox();
            nudVitalMod = new DarkUI.Controls.DarkNumericUpDown();
            nudVitalMod.Click += new EventHandler(NudVitalMod_Click);
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            txtName = new DarkUI.Controls.DarkTextBox();
            txtName.TextChanged += new EventHandler(TxtName_TextChanged);
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            nudIcon = new DarkUI.Controls.DarkNumericUpDown();
            nudIcon.Click += new EventHandler(NudPic_Click);
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            nudRarity = new DarkUI.Controls.DarkNumericUpDown();
            nudRarity.Click += new EventHandler(NudRarity_ValueChanged);
            picItem = new PictureBox();
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            cmbType = new DarkUI.Controls.DarkComboBox();
            cmbType.SelectedIndexChanged += new EventHandler(CmbType_SelectedIndexChanged);
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            cmbSubType = new DarkUI.Controls.DarkComboBox();
            cmbSubType.SelectedIndexChanged += new EventHandler(CmbSubType_SelectedIndexChanged);
            chkStackable = new DarkUI.Controls.DarkCheckBox();
            chkStackable.CheckedChanged += new EventHandler(ChkStackable_CheckedChanged);
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            cmbBind = new DarkUI.Controls.DarkComboBox();
            cmbBind.SelectedIndexChanged += new EventHandler(CmbBind_SelectedIndexChanged);
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            nudPrice = new DarkUI.Controls.DarkNumericUpDown();
            nudPrice.Click += new EventHandler(NudPrice_ValueChanged);
            DarkLabel8 = new DarkUI.Controls.DarkLabel();
            nudItemLvl = new DarkUI.Controls.DarkNumericUpDown();
            nudItemLvl.Click += new EventHandler(NuditemLvl_ValueChanged);
            DarkLabel9 = new DarkUI.Controls.DarkLabel();
            cmbAnimation = new DarkUI.Controls.DarkComboBox();
            cmbAnimation.SelectedIndexChanged += new EventHandler(CmbAnimation_SelectedIndexChanged);
            DarkLabel10 = new DarkUI.Controls.DarkLabel();
            txtDescription = new DarkUI.Controls.DarkTextBox();
            txtDescription.TextChanged += new EventHandler(TxtDescription_TextChanged);
            fraPet = new DarkUI.Controls.DarkGroupBox();
            cmbPet = new DarkUI.Controls.DarkComboBox();
            cmbPet.SelectedIndexChanged += new EventHandler(CmbPet_SelectedIndexChanged);
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            fraEvents = new DarkUI.Controls.DarkGroupBox();
            nudEventValue = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel39 = new DarkUI.Controls.DarkLabel();
            nudEvent = new DarkUI.Controls.DarkNumericUpDown();
            nudEvent.ValueChanged += new EventHandler(nudEvents_ValueChanged);
            DarkLabel38 = new DarkUI.Controls.DarkLabel();
            DarkLabel36 = new DarkUI.Controls.DarkLabel();
            fraBasics = new DarkUI.Controls.DarkGroupBox();
            DarkGroupBox1.SuspendLayout();
            fraEquipment.SuspendLayout();
            fraProjectile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPaperdoll).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPaperdoll).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDamage).BeginInit();
            DarkGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSpirit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIntelligence).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVitality).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLuck).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStrength).BeginInit();
            fraRequirements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLevelReq).BeginInit();
            DarkGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSprReq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIntReq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVitReq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLuckReq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStrReq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSpanwAmount).BeginInit();
            fraSkill.SuspendLayout();
            fraVitals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudVitalMod).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRarity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picItem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudItemLvl).BeginInit();
            fraPet.SuspendLayout();
            fraEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudEventValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudEvent).BeginInit();
            fraBasics.SuspendLayout();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(3, 3);
            DarkGroupBox1.Margin = new Padding(5);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(5);
            DarkGroupBox1.Size = new Size(348, 779);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Item List";
            // 
            // lstIndex
            // 
            lstIndex.BackColor = Color.FromArgb(45, 45, 48);
            lstIndex.BorderStyle = BorderStyle.FixedSingle;
            lstIndex.ForeColor = Color.Gainsboro;
            lstIndex.FormattingEnabled = true;
            lstIndex.ItemHeight = 25;
            lstIndex.Location = new Point(10, 27);
            lstIndex.Margin = new Padding(5);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(325, 752);
            lstIndex.TabIndex = 1;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(7, 18);
            DarkLabel1.Margin = new Padding(4, 0, 4, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(42, 15);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // DarkLabel35
            // 
            DarkLabel35.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel35.Location = new Point(0, 0);
            DarkLabel35.Name = "DarkLabel35";
            DarkLabel35.Size = new Size(100, 23);
            DarkLabel35.TabIndex = 0;
            // 
            // fraEquipment
            // 
            fraEquipment.BackColor = Color.FromArgb(45, 45, 48);
            fraEquipment.BorderColor = Color.FromArgb(90, 90, 90);
            fraEquipment.Controls.Add(fraProjectile);
            fraEquipment.Controls.Add(nudPaperdoll);
            fraEquipment.Controls.Add(DarkLabel23);
            fraEquipment.Controls.Add(picPaperdoll);
            fraEquipment.Controls.Add(cmbKnockBackTiles);
            fraEquipment.Controls.Add(DarkLabel16);
            fraEquipment.Controls.Add(chkKnockBack);
            fraEquipment.Controls.Add(nudSpeed);
            fraEquipment.Controls.Add(lblSpeed);
            fraEquipment.Controls.Add(nudDamage);
            fraEquipment.Controls.Add(DarkLabel15);
            fraEquipment.Controls.Add(cmbTool);
            fraEquipment.Controls.Add(DarkLabel14);
            fraEquipment.Controls.Add(DarkGroupBox2);
            fraEquipment.ForeColor = Color.Gainsboro;
            fraEquipment.Location = new Point(362, 472);
            fraEquipment.Margin = new Padding(5);
            fraEquipment.Name = "fraEquipment";
            fraEquipment.Padding = new Padding(5);
            fraEquipment.Size = new Size(750, 472);
            fraEquipment.TabIndex = 2;
            fraEquipment.TabStop = false;
            fraEquipment.Text = "Equipment Settings";
            // 
            // fraProjectile
            // 
            fraProjectile.BackColor = Color.FromArgb(45, 45, 48);
            fraProjectile.BorderColor = Color.FromArgb(90, 90, 90);
            fraProjectile.Controls.Add(cmbAmmo);
            fraProjectile.Controls.Add(DarkLabel25);
            fraProjectile.Controls.Add(cmbProjectile);
            fraProjectile.Controls.Add(DarkLabel24);
            fraProjectile.ForeColor = Color.Gainsboro;
            fraProjectile.Location = new Point(200, 320);
            fraProjectile.Margin = new Padding(5);
            fraProjectile.Name = "fraProjectile";
            fraProjectile.Padding = new Padding(5);
            fraProjectile.Size = new Size(542, 134);
            fraProjectile.TabIndex = 63;
            fraProjectile.TabStop = false;
            fraProjectile.Text = "Projectile Settings";
            // 
            // cmbAmmo
            // 
            cmbAmmo.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAmmo.FormattingEnabled = true;
            cmbAmmo.Location = new Point(107, 77);
            cmbAmmo.Margin = new Padding(5);
            cmbAmmo.Name = "cmbAmmo";
            cmbAmmo.Size = new Size(421, 32);
            cmbAmmo.TabIndex = 3;
            // 
            // DarkLabel25
            // 
            DarkLabel25.AutoSize = true;
            DarkLabel25.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel25.Location = new Point(32, 84);
            DarkLabel25.Margin = new Padding(5, 0, 5, 0);
            DarkLabel25.Name = "DarkLabel25";
            DarkLabel25.Size = new Size(71, 25);
            DarkLabel25.TabIndex = 2;
            DarkLabel25.Text = "Ammo:";
            // 
            // cmbProjectile
            // 
            cmbProjectile.DrawMode = DrawMode.OwnerDrawFixed;
            cmbProjectile.FormattingEnabled = true;
            cmbProjectile.Location = new Point(107, 25);
            cmbProjectile.Margin = new Padding(5);
            cmbProjectile.Name = "cmbProjectile";
            cmbProjectile.Size = new Size(421, 32);
            cmbProjectile.TabIndex = 1;
            // 
            // DarkLabel24
            // 
            DarkLabel24.AutoSize = true;
            DarkLabel24.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel24.Location = new Point(13, 30);
            DarkLabel24.Margin = new Padding(5, 0, 5, 0);
            DarkLabel24.Name = "DarkLabel24";
            DarkLabel24.Size = new Size(87, 25);
            DarkLabel24.TabIndex = 0;
            DarkLabel24.Text = "Projectile:";
            // 
            // nudPaperdoll
            // 
            nudPaperdoll.Location = new Point(110, 415);
            nudPaperdoll.Margin = new Padding(5);
            nudPaperdoll.Name = "nudPaperdoll";
            nudPaperdoll.Size = new Size(78, 31);
            nudPaperdoll.TabIndex = 59;
            // 
            // DarkLabel23
            // 
            DarkLabel23.AutoSize = true;
            DarkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel23.Location = new Point(10, 420);
            DarkLabel23.Margin = new Padding(5, 0, 5, 0);
            DarkLabel23.Name = "DarkLabel23";
            DarkLabel23.Size = new Size(90, 25);
            DarkLabel23.TabIndex = 58;
            DarkLabel23.Text = "Paperdoll:";
            // 
            // picPaperdoll
            // 
            picPaperdoll.BackColor = Color.Black;
            picPaperdoll.Location = new Point(12, 320);
            picPaperdoll.Margin = new Padding(5);
            picPaperdoll.Name = "picPaperdoll";
            picPaperdoll.Size = new Size(178, 91);
            picPaperdoll.TabIndex = 57;
            picPaperdoll.TabStop = false;
            // 
            // cmbKnockBackTiles
            // 
            cmbKnockBackTiles.DrawMode = DrawMode.OwnerDrawFixed;
            cmbKnockBackTiles.FormattingEnabled = true;
            cmbKnockBackTiles.Items.AddRange(new object[] { "No KnockBack", "1 Tile", "2 Tiles", "3 Tiles", "4 Tiles", "5 Tiles" });
            cmbKnockBackTiles.Location = new Point(542, 112);
            cmbKnockBackTiles.Margin = new Padding(5);
            cmbKnockBackTiles.Name = "cmbKnockBackTiles";
            cmbKnockBackTiles.Size = new Size(196, 32);
            cmbKnockBackTiles.TabIndex = 8;
            // 
            // DarkLabel16
            // 
            DarkLabel16.AutoSize = true;
            DarkLabel16.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel16.Location = new Point(502, 116);
            DarkLabel16.Margin = new Padding(5, 0, 5, 0);
            DarkLabel16.Name = "DarkLabel16";
            DarkLabel16.Size = new Size(32, 25);
            DarkLabel16.TabIndex = 7;
            DarkLabel16.Text = "Of";
            // 
            // chkKnockBack
            // 
            chkKnockBack.AutoSize = true;
            chkKnockBack.Location = new Point(328, 115);
            chkKnockBack.Margin = new Padding(5);
            chkKnockBack.Name = "chkKnockBack";
            chkKnockBack.Size = new Size(157, 29);
            chkKnockBack.TabIndex = 6;
            chkKnockBack.Text = "Has KnockBack";
            // 
            // nudSpeed
            // 
            nudSpeed.Location = new Point(165, 113);
            nudSpeed.Margin = new Padding(5);
            nudSpeed.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            nudSpeed.Name = "nudSpeed";
            nudSpeed.Size = new Size(153, 31);
            nudSpeed.TabIndex = 5;
            // 
            // lblSpeed
            // 
            lblSpeed.AutoSize = true;
            lblSpeed.ForeColor = Color.FromArgb(220, 220, 220);
            lblSpeed.Location = new Point(10, 116);
            lblSpeed.Margin = new Padding(5, 0, 5, 0);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new Size(95, 25);
            lblSpeed.TabIndex = 4;
            lblSpeed.Text = "Speed: 0.1";
            // 
            // nudDamage
            // 
            nudDamage.Location = new Point(422, 38);
            nudDamage.Margin = new Padding(5);
            nudDamage.Name = "nudDamage";
            nudDamage.Size = new Size(200, 31);
            nudDamage.TabIndex = 3;
            // 
            // DarkLabel15
            // 
            DarkLabel15.AutoSize = true;
            DarkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel15.Location = new Point(328, 41);
            DarkLabel15.Margin = new Padding(5, 0, 5, 0);
            DarkLabel15.Name = "DarkLabel15";
            DarkLabel15.Size = new Size(83, 25);
            DarkLabel15.TabIndex = 2;
            DarkLabel15.Text = "Damage:";
            // 
            // cmbTool
            // 
            cmbTool.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTool.FormattingEnabled = true;
            cmbTool.Items.AddRange(new object[] { "None", "Hatchet", "Rod", "Pickaxe", "Hoe" });
            cmbTool.Location = new Point(117, 37);
            cmbTool.Margin = new Padding(5);
            cmbTool.Name = "cmbTool";
            cmbTool.Size = new Size(199, 32);
            cmbTool.TabIndex = 1;
            // 
            // DarkLabel14
            // 
            DarkLabel14.AutoSize = true;
            DarkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel14.Location = new Point(10, 41);
            DarkLabel14.Margin = new Padding(5, 0, 5, 0);
            DarkLabel14.Name = "DarkLabel14";
            DarkLabel14.Size = new Size(91, 25);
            DarkLabel14.TabIndex = 0;
            DarkLabel14.Text = "Tool Type:";
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(nudSpirit);
            DarkGroupBox2.Controls.Add(DarkLabel22);
            DarkGroupBox2.Controls.Add(nudIntelligence);
            DarkGroupBox2.Controls.Add(DarkLabel21);
            DarkGroupBox2.Controls.Add(nudVitality);
            DarkGroupBox2.Controls.Add(DarkLabel20);
            DarkGroupBox2.Controls.Add(nudLuck);
            DarkGroupBox2.Controls.Add(DarkLabel19);
            DarkGroupBox2.Controls.Add(nudStrength);
            DarkGroupBox2.Controls.Add(DarkLabel17);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(10, 163);
            DarkGroupBox2.Margin = new Padding(5);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(5);
            DarkGroupBox2.Size = new Size(730, 147);
            DarkGroupBox2.TabIndex = 9;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Stats";
            // 
            // nudSpirit
            // 
            nudSpirit.Location = new Point(279, 83);
            nudSpirit.Margin = new Padding(5);
            nudSpirit.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudSpirit.Name = "nudSpirit";
            nudSpirit.Size = new Size(83, 31);
            nudSpirit.TabIndex = 12;
            // 
            // DarkLabel22
            // 
            DarkLabel22.AutoSize = true;
            DarkLabel22.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel22.Location = new Point(209, 86);
            DarkLabel22.Margin = new Padding(5, 0, 5, 0);
            DarkLabel22.Name = "DarkLabel22";
            DarkLabel22.Size = new Size(57, 25);
            DarkLabel22.TabIndex = 11;
            DarkLabel22.Text = "Spirit:";
            // 
            // nudIntelligence
            // 
            nudIntelligence.Location = new Point(116, 84);
            nudIntelligence.Margin = new Padding(5);
            nudIntelligence.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudIntelligence.Name = "nudIntelligence";
            nudIntelligence.Size = new Size(83, 31);
            nudIntelligence.TabIndex = 10;
            // 
            // DarkLabel21
            // 
            DarkLabel21.AutoSize = true;
            DarkLabel21.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel21.Location = new Point(10, 87);
            DarkLabel21.Margin = new Padding(5, 0, 5, 0);
            DarkLabel21.Name = "DarkLabel21";
            DarkLabel21.Size = new Size(105, 25);
            DarkLabel21.TabIndex = 9;
            DarkLabel21.Text = "Intelligence:";
            // 
            // nudVitality
            // 
            nudVitality.Location = new Point(80, 35);
            nudVitality.Margin = new Padding(5);
            nudVitality.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudVitality.Name = "nudVitality";
            nudVitality.Size = new Size(83, 31);
            nudVitality.TabIndex = 8;
            // 
            // DarkLabel20
            // 
            DarkLabel20.AutoSize = true;
            DarkLabel20.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel20.Location = new Point(10, 38);
            DarkLabel20.Margin = new Padding(5, 0, 5, 0);
            DarkLabel20.Name = "DarkLabel20";
            DarkLabel20.Size = new Size(69, 25);
            DarkLabel20.TabIndex = 7;
            DarkLabel20.Text = "Vitality:";
            // 
            // nudLuck
            // 
            nudLuck.Location = new Point(473, 39);
            nudLuck.Margin = new Padding(5);
            nudLuck.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudLuck.Name = "nudLuck";
            nudLuck.Size = new Size(83, 31);
            nudLuck.TabIndex = 6;
            // 
            // DarkLabel19
            // 
            DarkLabel19.AutoSize = true;
            DarkLabel19.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel19.Location = new Point(403, 41);
            DarkLabel19.Margin = new Padding(5, 0, 5, 0);
            DarkLabel19.Name = "DarkLabel19";
            DarkLabel19.Size = new Size(51, 25);
            DarkLabel19.TabIndex = 5;
            DarkLabel19.Text = "Luck:";
            // 
            // nudStrength
            // 
            nudStrength.Location = new Point(278, 35);
            nudStrength.Margin = new Padding(5);
            nudStrength.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudStrength.Name = "nudStrength";
            nudStrength.Size = new Size(83, 31);
            nudStrength.TabIndex = 2;
            // 
            // DarkLabel17
            // 
            DarkLabel17.AutoSize = true;
            DarkLabel17.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel17.Location = new Point(197, 38);
            DarkLabel17.Margin = new Padding(5, 0, 5, 0);
            DarkLabel17.Name = "DarkLabel17";
            DarkLabel17.Size = new Size(83, 25);
            DarkLabel17.TabIndex = 1;
            DarkLabel17.Text = "Strength:";
            // 
            // btnBasics
            // 
            btnBasics.Location = new Point(362, 15);
            btnBasics.Margin = new Padding(5);
            btnBasics.Name = "btnBasics";
            btnBasics.Padding = new Padding(8, 10, 8, 10);
            btnBasics.Size = new Size(125, 45);
            btnBasics.TabIndex = 3;
            btnBasics.Text = "Properties";
            // 
            // btnRequirements
            // 
            btnRequirements.Location = new Point(497, 15);
            btnRequirements.Margin = new Padding(5);
            btnRequirements.Name = "btnRequirements";
            btnRequirements.Padding = new Padding(8, 10, 8, 10);
            btnRequirements.Size = new Size(153, 45);
            btnRequirements.TabIndex = 4;
            btnRequirements.Text = "Requirements";
            // 
            // fraRequirements
            // 
            fraRequirements.BackColor = Color.FromArgb(45, 45, 48);
            fraRequirements.BorderColor = Color.FromArgb(90, 90, 90);
            fraRequirements.Controls.Add(DarkLabel28);
            fraRequirements.Controls.Add(nudLevelReq);
            fraRequirements.Controls.Add(cmbAccessReq);
            fraRequirements.Controls.Add(DarkLabel27);
            fraRequirements.Controls.Add(cmbJobReq);
            fraRequirements.Controls.Add(DarkLabel26);
            fraRequirements.Controls.Add(DarkGroupBox4);
            fraRequirements.ForeColor = Color.Gainsboro;
            fraRequirements.Location = new Point(362, 72);
            fraRequirements.Margin = new Padding(5);
            fraRequirements.Name = "fraRequirements";
            fraRequirements.Padding = new Padding(5);
            fraRequirements.Size = new Size(750, 388);
            fraRequirements.TabIndex = 5;
            fraRequirements.TabStop = false;
            fraRequirements.Text = "Requirements";
            fraRequirements.Visible = false;
            // 
            // DarkLabel28
            // 
            DarkLabel28.AutoSize = true;
            DarkLabel28.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel28.Location = new Point(10, 130);
            DarkLabel28.Margin = new Padding(5, 0, 5, 0);
            DarkLabel28.Name = "DarkLabel28";
            DarkLabel28.Size = new Size(160, 25);
            DarkLabel28.TabIndex = 5;
            DarkLabel28.Text = "Level Requirement:";
            // 
            // nudLevelReq
            // 
            nudLevelReq.Location = new Point(200, 127);
            nudLevelReq.Margin = new Padding(5);
            nudLevelReq.Name = "nudLevelReq";
            nudLevelReq.Size = new Size(200, 31);
            nudLevelReq.TabIndex = 4;
            // 
            // cmbAccessReq
            // 
            cmbAccessReq.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAccessReq.FormattingEnabled = true;
            cmbAccessReq.Items.AddRange(new object[] { "Player", "Moderator", "Mapper", "Developer", "Owneer" });
            cmbAccessReq.Location = new Point(200, 75);
            cmbAccessReq.Margin = new Padding(5);
            cmbAccessReq.Name = "cmbAccessReq";
            cmbAccessReq.Size = new Size(292, 32);
            cmbAccessReq.TabIndex = 3;
            // 
            // DarkLabel27
            // 
            DarkLabel27.AutoSize = true;
            DarkLabel27.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel27.Location = new Point(10, 84);
            DarkLabel27.Margin = new Padding(5, 0, 5, 0);
            DarkLabel27.Name = "DarkLabel27";
            DarkLabel27.Size = new Size(174, 25);
            DarkLabel27.TabIndex = 2;
            DarkLabel27.Text = "Access Requirement:";
            // 
            // cmbJobReq
            // 
            cmbJobReq.DrawMode = DrawMode.OwnerDrawFixed;
            cmbJobReq.FormattingEnabled = true;
            cmbJobReq.Location = new Point(200, 25);
            cmbJobReq.Margin = new Padding(5);
            cmbJobReq.Name = "cmbJobReq";
            cmbJobReq.Size = new Size(292, 32);
            cmbJobReq.TabIndex = 1;
            // 
            // DarkLabel26
            // 
            DarkLabel26.AutoSize = true;
            DarkLabel26.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel26.Location = new Point(10, 30);
            DarkLabel26.Margin = new Padding(5, 0, 5, 0);
            DarkLabel26.Name = "DarkLabel26";
            DarkLabel26.Size = new Size(149, 25);
            DarkLabel26.TabIndex = 0;
            DarkLabel26.Text = "Job Requirement:";
            // 
            // DarkGroupBox4
            // 
            DarkGroupBox4.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox4.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox4.Controls.Add(nudSprReq);
            DarkGroupBox4.Controls.Add(DarkLabel32);
            DarkGroupBox4.Controls.Add(nudIntReq);
            DarkGroupBox4.Controls.Add(DarkLabel33);
            DarkGroupBox4.Controls.Add(nudVitReq);
            DarkGroupBox4.Controls.Add(DarkLabel34);
            DarkGroupBox4.Controls.Add(nudLuckReq);
            DarkGroupBox4.Controls.Add(DarkLabel29);
            DarkGroupBox4.Controls.Add(nudStrReq);
            DarkGroupBox4.Controls.Add(DarkLabel31);
            DarkGroupBox4.ForeColor = Color.Gainsboro;
            DarkGroupBox4.Location = new Point(10, 185);
            DarkGroupBox4.Margin = new Padding(5);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(5);
            DarkGroupBox4.Size = new Size(730, 191);
            DarkGroupBox4.TabIndex = 6;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Stat Requirements";
            // 
            // nudSprReq
            // 
            nudSprReq.Location = new Point(314, 123);
            nudSprReq.Margin = new Padding(5);
            nudSprReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudSprReq.Name = "nudSprReq";
            nudSprReq.Size = new Size(83, 31);
            nudSprReq.TabIndex = 18;
            // 
            // DarkLabel32
            // 
            DarkLabel32.AutoSize = true;
            DarkLabel32.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel32.Location = new Point(199, 125);
            DarkLabel32.Margin = new Padding(5, 0, 5, 0);
            DarkLabel32.Name = "DarkLabel32";
            DarkLabel32.Size = new Size(57, 25);
            DarkLabel32.TabIndex = 17;
            DarkLabel32.Text = "Spirit:";
            // 
            // nudIntReq
            // 
            nudIntReq.Location = new Point(314, 42);
            nudIntReq.Margin = new Padding(5);
            nudIntReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudIntReq.Name = "nudIntReq";
            nudIntReq.Size = new Size(83, 31);
            nudIntReq.TabIndex = 16;
            // 
            // DarkLabel33
            // 
            DarkLabel33.AutoSize = true;
            DarkLabel33.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel33.Location = new Point(199, 47);
            DarkLabel33.Margin = new Padding(5, 0, 5, 0);
            DarkLabel33.Name = "DarkLabel33";
            DarkLabel33.Size = new Size(105, 25);
            DarkLabel33.TabIndex = 15;
            DarkLabel33.Text = "Intelligence:";
            // 
            // nudVitReq
            // 
            nudVitReq.Location = new Point(103, 125);
            nudVitReq.Margin = new Padding(5);
            nudVitReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudVitReq.Name = "nudVitReq";
            nudVitReq.Size = new Size(83, 31);
            nudVitReq.TabIndex = 14;
            // 
            // DarkLabel34
            // 
            DarkLabel34.AutoSize = true;
            DarkLabel34.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel34.Location = new Point(10, 127);
            DarkLabel34.Margin = new Padding(5, 0, 5, 0);
            DarkLabel34.Name = "DarkLabel34";
            DarkLabel34.Size = new Size(69, 25);
            DarkLabel34.TabIndex = 13;
            DarkLabel34.Text = "Vitality:";
            // 
            // nudLuckReq
            // 
            nudLuckReq.Location = new Point(481, 44);
            nudLuckReq.Margin = new Padding(5);
            nudLuckReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudLuckReq.Name = "nudLuckReq";
            nudLuckReq.Size = new Size(83, 31);
            nudLuckReq.TabIndex = 12;
            // 
            // DarkLabel29
            // 
            DarkLabel29.AutoSize = true;
            DarkLabel29.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel29.Location = new Point(420, 46);
            DarkLabel29.Margin = new Padding(5, 0, 5, 0);
            DarkLabel29.Name = "DarkLabel29";
            DarkLabel29.Size = new Size(51, 25);
            DarkLabel29.TabIndex = 11;
            DarkLabel29.Text = "Luck:";
            // 
            // nudStrReq
            // 
            nudStrReq.Location = new Point(107, 45);
            nudStrReq.Margin = new Padding(5);
            nudStrReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudStrReq.Name = "nudStrReq";
            nudStrReq.Size = new Size(83, 31);
            nudStrReq.TabIndex = 8;
            // 
            // DarkLabel31
            // 
            DarkLabel31.AutoSize = true;
            DarkLabel31.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel31.Location = new Point(10, 47);
            DarkLabel31.Margin = new Padding(5, 0, 5, 0);
            DarkLabel31.Name = "DarkLabel31";
            DarkLabel31.Size = new Size(83, 25);
            DarkLabel31.TabIndex = 7;
            DarkLabel31.Text = "Strength:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(13, 789);
            btnSave.Margin = new Padding(5);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(8, 10, 8, 10);
            btnSave.Size = new Size(327, 45);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(13, 844);
            btnDelete.Margin = new Padding(5);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(8, 10, 8, 10);
            btnDelete.Size = new Size(327, 45);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(13, 898);
            btnCancel.Margin = new Padding(5);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(8, 10, 8, 10);
            btnCancel.Size = new Size(327, 45);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            // 
            // btnSpawn
            // 
            btnSpawn.Location = new Point(662, 15);
            btnSpawn.Margin = new Padding(5);
            btnSpawn.Name = "btnSpawn";
            btnSpawn.Padding = new Padding(8, 10, 8, 10);
            btnSpawn.Size = new Size(125, 45);
            btnSpawn.TabIndex = 10;
            btnSpawn.Text = "Spawn";
            // 
            // nudSpanwAmount
            // 
            nudSpanwAmount.Location = new Point(797, 23);
            nudSpanwAmount.Margin = new Padding(5);
            nudSpanwAmount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSpanwAmount.Name = "nudSpanwAmount";
            nudSpanwAmount.Size = new Size(212, 31);
            nudSpanwAmount.TabIndex = 11;
            nudSpanwAmount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // fraSkill
            // 
            fraSkill.BackColor = Color.FromArgb(45, 45, 48);
            fraSkill.BorderColor = Color.FromArgb(90, 90, 90);
            fraSkill.Controls.Add(cmbSkills);
            fraSkill.Controls.Add(DarkLabel12);
            fraSkill.ForeColor = Color.Gainsboro;
            fraSkill.Location = new Point(405, 230);
            fraSkill.Margin = new Padding(5);
            fraSkill.Name = "fraSkill";
            fraSkill.Padding = new Padding(5);
            fraSkill.Size = new Size(333, 77);
            fraSkill.TabIndex = 24;
            fraSkill.TabStop = false;
            fraSkill.Text = "Skills";
            // 
            // cmbSkills
            // 
            cmbSkills.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkills.FormattingEnabled = true;
            cmbSkills.Location = new Point(68, 27);
            cmbSkills.Margin = new Padding(5);
            cmbSkills.Name = "cmbSkills";
            cmbSkills.Size = new Size(252, 32);
            cmbSkills.TabIndex = 1;
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(10, 34);
            DarkLabel12.Margin = new Padding(5, 0, 5, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(47, 25);
            DarkLabel12.TabIndex = 0;
            DarkLabel12.Text = "Skill:";
            // 
            // fraVitals
            // 
            fraVitals.BackColor = Color.FromArgb(45, 45, 48);
            fraVitals.BorderColor = Color.FromArgb(90, 90, 90);
            fraVitals.Controls.Add(nudVitalMod);
            fraVitals.Controls.Add(DarkLabel11);
            fraVitals.ForeColor = Color.Gainsboro;
            fraVitals.Location = new Point(405, 230);
            fraVitals.Margin = new Padding(5);
            fraVitals.Name = "fraVitals";
            fraVitals.Padding = new Padding(5);
            fraVitals.Size = new Size(333, 77);
            fraVitals.TabIndex = 23;
            fraVitals.TabStop = false;
            fraVitals.Text = "Vitals";
            // 
            // nudVitalMod
            // 
            nudVitalMod.Location = new Point(111, 32);
            nudVitalMod.Margin = new Padding(5);
            nudVitalMod.Name = "nudVitalMod";
            nudVitalMod.Size = new Size(212, 31);
            nudVitalMod.TabIndex = 1;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(10, 34);
            DarkLabel11.Margin = new Padding(5, 0, 5, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(54, 25);
            DarkLabel11.TabIndex = 0;
            DarkLabel11.Text = "Mod:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(122, 27);
            txtName.Margin = new Padding(5);
            txtName.Name = "txtName";
            txtName.Size = new Size(379, 31);
            txtName.TabIndex = 1;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(512, 30);
            DarkLabel2.Margin = new Padding(5, 0, 5, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(50, 25);
            DarkLabel2.TabIndex = 2;
            DarkLabel2.Text = "Icon:";
            // 
            // nudIcon
            // 
            nudIcon.Location = new Point(593, 27);
            nudIcon.Margin = new Padding(5);
            nudIcon.Name = "nudIcon";
            nudIcon.Size = new Size(82, 31);
            nudIcon.TabIndex = 3;
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(512, 84);
            DarkLabel3.Margin = new Padding(5, 0, 5, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(61, 25);
            DarkLabel3.TabIndex = 4;
            DarkLabel3.Text = "Rarity:";
            // 
            // nudRarity
            // 
            nudRarity.Location = new Point(593, 77);
            nudRarity.Margin = new Padding(5);
            nudRarity.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            nudRarity.Name = "nudRarity";
            nudRarity.Size = new Size(82, 31);
            nudRarity.TabIndex = 5;
            // 
            // picItem
            // 
            picItem.BackColor = Color.Black;
            picItem.Location = new Point(685, 27);
            picItem.Margin = new Padding(5);
            picItem.Name = "picItem";
            picItem.Size = new Size(45, 53);
            picItem.TabIndex = 7;
            picItem.TabStop = false;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(10, 84);
            DarkLabel4.Margin = new Padding(5, 0, 5, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(53, 25);
            DarkLabel4.TabIndex = 8;
            DarkLabel4.Text = "Type:";
            // 
            // cmbType
            // 
            cmbType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "None", "Equipment", "Consumable", "Common Event", "Currency", "Skill", "Projectile", "Pet" });
            cmbType.Location = new Point(122, 77);
            cmbType.Margin = new Padding(5);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(199, 32);
            cmbType.TabIndex = 9;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(10, 135);
            DarkLabel5.Margin = new Padding(5, 0, 5, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(91, 25);
            DarkLabel5.TabIndex = 10;
            DarkLabel5.Text = "Sub-Type:";
            // 
            // cmbSubType
            // 
            cmbSubType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSubType.FormattingEnabled = true;
            cmbSubType.Items.AddRange(new object[] { "None", "Weapon", "Armor", "Helmet", "Shield", "Add HP", "Add MP", "Add SP", "Sub HP", "Sub MP", "Sub SP", "Experience", "Common Event", "Currency", "Skill" });
            cmbSubType.Location = new Point(122, 128);
            cmbSubType.Margin = new Padding(5);
            cmbSubType.Name = "cmbSubType";
            cmbSubType.Size = new Size(199, 32);
            cmbSubType.TabIndex = 11;
            // 
            // chkStackable
            // 
            chkStackable.AutoSize = true;
            chkStackable.Location = new Point(378, 80);
            chkStackable.Margin = new Padding(5);
            chkStackable.Name = "chkStackable";
            chkStackable.Size = new Size(112, 29);
            chkStackable.TabIndex = 12;
            chkStackable.Text = "Stackable";
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(373, 135);
            DarkLabel6.Margin = new Padding(5, 0, 5, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(93, 25);
            DarkLabel6.TabIndex = 13;
            DarkLabel6.Text = "Bind Type:";
            // 
            // cmbBind
            // 
            cmbBind.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBind.FormattingEnabled = true;
            cmbBind.Items.AddRange(new object[] { "None", "Bind on Pickup", "Bind on Equip" });
            cmbBind.Location = new Point(475, 128);
            cmbBind.Margin = new Padding(5);
            cmbBind.Name = "cmbBind";
            cmbBind.Size = new Size(261, 32);
            cmbBind.TabIndex = 14;
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(10, 185);
            DarkLabel7.Margin = new Padding(5, 0, 5, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(53, 25);
            DarkLabel7.TabIndex = 15;
            DarkLabel7.Text = "Price:";
            // 
            // nudPrice
            // 
            nudPrice.Location = new Point(122, 180);
            nudPrice.Margin = new Padding(5);
            nudPrice.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudPrice.Name = "nudPrice";
            nudPrice.Size = new Size(115, 31);
            nudPrice.TabIndex = 16;
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(247, 185);
            DarkLabel8.Margin = new Padding(5, 0, 5, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(55, 25);
            DarkLabel8.TabIndex = 17;
            DarkLabel8.Text = "Level:";
            // 
            // nudItemLvl
            // 
            nudItemLvl.Location = new Point(348, 180);
            nudItemLvl.Margin = new Padding(5);
            nudItemLvl.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudItemLvl.Name = "nudItemLvl";
            nudItemLvl.Size = new Size(80, 31);
            nudItemLvl.TabIndex = 18;
            nudItemLvl.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DarkLabel9
            // 
            DarkLabel9.AutoSize = true;
            DarkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel9.Location = new Point(438, 185);
            DarkLabel9.Margin = new Padding(5, 0, 5, 0);
            DarkLabel9.Name = "DarkLabel9";
            DarkLabel9.Size = new Size(98, 25);
            DarkLabel9.TabIndex = 19;
            DarkLabel9.Text = "Animation:";
            // 
            // cmbAnimation
            // 
            cmbAnimation.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnimation.FormattingEnabled = true;
            cmbAnimation.Location = new Point(542, 178);
            cmbAnimation.Margin = new Padding(5);
            cmbAnimation.Name = "cmbAnimation";
            cmbAnimation.Size = new Size(194, 32);
            cmbAnimation.TabIndex = 20;
            // 
            // DarkLabel10
            // 
            DarkLabel10.AutoSize = true;
            DarkLabel10.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel10.Location = new Point(10, 228);
            DarkLabel10.Margin = new Padding(5, 0, 5, 0);
            DarkLabel10.Name = "DarkLabel10";
            DarkLabel10.Size = new Size(106, 25);
            DarkLabel10.TabIndex = 21;
            DarkLabel10.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.BackColor = Color.FromArgb(69, 73, 74);
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.ForeColor = Color.FromArgb(220, 220, 220);
            txtDescription.Location = new Point(15, 260);
            txtDescription.Margin = new Padding(5);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(379, 114);
            txtDescription.TabIndex = 22;
            // 
            // fraPet
            // 
            fraPet.BackColor = Color.FromArgb(45, 45, 48);
            fraPet.BorderColor = Color.FromArgb(90, 90, 90);
            fraPet.Controls.Add(cmbPet);
            fraPet.Controls.Add(DarkLabel13);
            fraPet.ForeColor = Color.Gainsboro;
            fraPet.Location = new Point(405, 305);
            fraPet.Margin = new Padding(5);
            fraPet.Name = "fraPet";
            fraPet.Padding = new Padding(5);
            fraPet.Size = new Size(333, 77);
            fraPet.TabIndex = 25;
            fraPet.TabStop = false;
            fraPet.Text = "Pets";
            // 
            // cmbPet
            // 
            cmbPet.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPet.FormattingEnabled = true;
            cmbPet.Location = new Point(68, 27);
            cmbPet.Margin = new Padding(5);
            cmbPet.Name = "cmbPet";
            cmbPet.Size = new Size(252, 32);
            cmbPet.TabIndex = 1;
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(10, 34);
            DarkLabel13.Margin = new Padding(5, 0, 5, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(55, 25);
            DarkLabel13.TabIndex = 0;
            DarkLabel13.Text = "Num:";
            // 
            // fraEvents
            // 
            fraEvents.BackColor = Color.FromArgb(45, 45, 48);
            fraEvents.BorderColor = Color.FromArgb(90, 90, 90);
            fraEvents.Controls.Add(nudEventValue);
            fraEvents.Controls.Add(DarkLabel39);
            fraEvents.Controls.Add(nudEvent);
            fraEvents.Controls.Add(DarkLabel38);
            fraEvents.ForeColor = Color.Gainsboro;
            fraEvents.Location = new Point(417, 228);
            fraEvents.Margin = new Padding(5);
            fraEvents.Name = "fraEvents";
            fraEvents.Padding = new Padding(5);
            fraEvents.Size = new Size(333, 160);
            fraEvents.TabIndex = 27;
            fraEvents.TabStop = false;
            fraEvents.Text = "Events";
            // 
            // nudEventValue
            // 
            nudEventValue.Location = new Point(112, 95);
            nudEventValue.Margin = new Padding(5);
            nudEventValue.Name = "nudEventValue";
            nudEventValue.Size = new Size(212, 31);
            nudEventValue.TabIndex = 5;
            // 
            // DarkLabel39
            // 
            DarkLabel39.AutoSize = true;
            DarkLabel39.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel39.Location = new Point(13, 102);
            DarkLabel39.Margin = new Padding(5, 0, 5, 0);
            DarkLabel39.Name = "DarkLabel39";
            DarkLabel39.Size = new Size(58, 25);
            DarkLabel39.TabIndex = 4;
            DarkLabel39.Text = "Value:";
            // 
            // nudEvent
            // 
            nudEvent.Location = new Point(112, 27);
            nudEvent.Margin = new Padding(5);
            nudEvent.Name = "nudEvent";
            nudEvent.Size = new Size(212, 31);
            nudEvent.TabIndex = 1;
            // 
            // DarkLabel38
            // 
            DarkLabel38.AutoSize = true;
            DarkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel38.Location = new Point(13, 34);
            DarkLabel38.Margin = new Padding(5, 0, 5, 0);
            DarkLabel38.Name = "DarkLabel38";
            DarkLabel38.Size = new Size(34, 25);
            DarkLabel38.TabIndex = 0;
            DarkLabel38.Text = "ID:";
            // 
            // DarkLabel36
            // 
            DarkLabel36.AutoSize = true;
            DarkLabel36.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel36.Location = new Point(10, 30);
            DarkLabel36.Margin = new Padding(5, 0, 5, 0);
            DarkLabel36.Name = "DarkLabel36";
            DarkLabel36.Size = new Size(63, 25);
            DarkLabel36.TabIndex = 28;
            DarkLabel36.Text = "Name:";
            // 
            // fraBasics
            // 
            fraBasics.BackColor = Color.FromArgb(45, 45, 48);
            fraBasics.BorderColor = Color.FromArgb(90, 90, 90);
            fraBasics.Controls.Add(DarkLabel36);
            fraBasics.Controls.Add(fraPet);
            fraBasics.Controls.Add(txtDescription);
            fraBasics.Controls.Add(DarkLabel10);
            fraBasics.Controls.Add(cmbAnimation);
            fraBasics.Controls.Add(DarkLabel9);
            fraBasics.Controls.Add(nudItemLvl);
            fraBasics.Controls.Add(DarkLabel8);
            fraBasics.Controls.Add(nudPrice);
            fraBasics.Controls.Add(DarkLabel7);
            fraBasics.Controls.Add(cmbBind);
            fraBasics.Controls.Add(DarkLabel6);
            fraBasics.Controls.Add(chkStackable);
            fraBasics.Controls.Add(cmbSubType);
            fraBasics.Controls.Add(DarkLabel5);
            fraBasics.Controls.Add(cmbType);
            fraBasics.Controls.Add(DarkLabel4);
            fraBasics.Controls.Add(picItem);
            fraBasics.Controls.Add(nudRarity);
            fraBasics.Controls.Add(DarkLabel3);
            fraBasics.Controls.Add(nudIcon);
            fraBasics.Controls.Add(DarkLabel2);
            fraBasics.Controls.Add(txtName);
            fraBasics.Controls.Add(fraVitals);
            fraBasics.Controls.Add(fraSkill);
            fraBasics.Controls.Add(fraEvents);
            fraBasics.ForeColor = Color.Gainsboro;
            fraBasics.Location = new Point(362, 72);
            fraBasics.Margin = new Padding(5);
            fraBasics.Name = "fraBasics";
            fraBasics.Padding = new Padding(5);
            fraBasics.Size = new Size(750, 388);
            fraBasics.TabIndex = 1;
            fraBasics.TabStop = false;
            fraBasics.Text = "Properties";
            // 
            // frmEditor_Item
            // 
            AutoScaleDimensions = new SizeF(10.0f, 25.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1121, 953);
            Controls.Add(nudSpanwAmount);
            Controls.Add(btnSpawn);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(btnRequirements);
            Controls.Add(btnBasics);
            Controls.Add(DarkGroupBox1);
            Controls.Add(fraBasics);
            Controls.Add(fraRequirements);
            Controls.Add(fraEquipment);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(5);
            Name = "frmEditor_Item";
            Text = "Item Editor";
            DarkGroupBox1.ResumeLayout(false);
            fraEquipment.ResumeLayout(false);
            fraEquipment.PerformLayout();
            fraProjectile.ResumeLayout(false);
            fraProjectile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPaperdoll).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPaperdoll).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDamage).EndInit();
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSpirit).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIntelligence).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVitality).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLuck).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStrength).EndInit();
            fraRequirements.ResumeLayout(false);
            fraRequirements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLevelReq).EndInit();
            DarkGroupBox4.ResumeLayout(false);
            DarkGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSprReq).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIntReq).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVitReq).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLuckReq).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStrReq).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSpanwAmount).EndInit();
            fraSkill.ResumeLayout(false);
            fraSkill.PerformLayout();
            fraVitals.ResumeLayout(false);
            fraVitals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudVitalMod).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRarity).EndInit();
            ((System.ComponentModel.ISupportInitialize)picItem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudItemLvl).EndInit();
            fraPet.ResumeLayout(false);
            fraPet.PerformLayout();
            fraEvents.ResumeLayout(false);
            fraEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudEventValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudEvent).EndInit();
            fraBasics.ResumeLayout(false);
            fraBasics.PerformLayout();
            Load += new EventHandler(frmEditor_Item_Load);
            FormClosing += new FormClosingEventHandler(frmEditor_item_FormClosing);
            ResumeLayout(false);

        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal DarkUI.Controls.DarkGroupBox fraEquipment;
        internal DarkUI.Controls.DarkComboBox cmbTool;
        internal DarkUI.Controls.DarkLabel DarkLabel14;
        internal DarkUI.Controls.DarkLabel DarkLabel15;
        internal DarkUI.Controls.DarkNumericUpDown nudDamage;
        internal DarkUI.Controls.DarkLabel lblSpeed;
        internal DarkUI.Controls.DarkNumericUpDown nudSpeed;
        internal DarkUI.Controls.DarkCheckBox chkKnockBack;
        internal DarkUI.Controls.DarkComboBox cmbKnockBackTiles;
        internal DarkUI.Controls.DarkLabel DarkLabel16;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkLabel DarkLabel17;
        internal DarkUI.Controls.DarkNumericUpDown nudStrength;
        internal DarkUI.Controls.DarkNumericUpDown nudLuck;
        internal DarkUI.Controls.DarkLabel DarkLabel19;
        internal DarkUI.Controls.DarkNumericUpDown nudVitality;
        internal DarkUI.Controls.DarkLabel DarkLabel20;
        internal DarkUI.Controls.DarkNumericUpDown nudIntelligence;
        internal DarkUI.Controls.DarkLabel DarkLabel21;
        internal DarkUI.Controls.DarkNumericUpDown nudSpirit;
        internal DarkUI.Controls.DarkLabel DarkLabel22;
        internal DarkUI.Controls.DarkNumericUpDown nudPaperdoll;
        internal DarkUI.Controls.DarkLabel DarkLabel23;
        internal PictureBox picPaperdoll;
        internal DarkUI.Controls.DarkButton btnBasics;
        internal DarkUI.Controls.DarkButton btnRequirements;
        internal DarkUI.Controls.DarkGroupBox fraRequirements;
        internal DarkUI.Controls.DarkComboBox cmbJobReq;
        internal DarkUI.Controls.DarkLabel DarkLabel26;
        internal DarkUI.Controls.DarkComboBox cmbAccessReq;
        internal DarkUI.Controls.DarkLabel DarkLabel27;
        internal DarkUI.Controls.DarkLabel DarkLabel28;
        internal DarkUI.Controls.DarkNumericUpDown nudLevelReq;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox4;
        internal DarkUI.Controls.DarkNumericUpDown nudSprReq;
        internal DarkUI.Controls.DarkLabel DarkLabel32;
        internal DarkUI.Controls.DarkNumericUpDown nudIntReq;
        internal DarkUI.Controls.DarkLabel DarkLabel33;
        internal DarkUI.Controls.DarkNumericUpDown nudVitReq;
        internal DarkUI.Controls.DarkLabel DarkLabel34;
        internal DarkUI.Controls.DarkNumericUpDown nudLuckReq;
        internal DarkUI.Controls.DarkLabel DarkLabel29;
        internal DarkUI.Controls.DarkNumericUpDown nudStrReq;
        internal DarkUI.Controls.DarkLabel DarkLabel31;
        internal DarkUI.Controls.DarkLabel DarkLabel35;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkButton btnDelete;
        internal DarkUI.Controls.DarkButton btnCancel;
        internal DarkUI.Controls.DarkButton btnSpawn;
        internal DarkUI.Controls.DarkNumericUpDown nudSpanwAmount;
        internal DarkUI.Controls.DarkGroupBox fraProjectile;
        internal DarkUI.Controls.DarkComboBox cmbAmmo;
        internal DarkUI.Controls.DarkLabel DarkLabel25;
        internal DarkUI.Controls.DarkComboBox cmbProjectile;
        internal DarkUI.Controls.DarkLabel DarkLabel24;
        internal DarkUI.Controls.DarkGroupBox fraSkill;
        internal DarkUI.Controls.DarkComboBox cmbSkills;
        internal DarkUI.Controls.DarkLabel DarkLabel12;
        internal DarkUI.Controls.DarkGroupBox fraVitals;
        internal DarkUI.Controls.DarkNumericUpDown nudVitalMod;
        internal DarkUI.Controls.DarkLabel DarkLabel11;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel2;
        internal DarkUI.Controls.DarkNumericUpDown nudIcon;
        internal DarkUI.Controls.DarkLabel DarkLabel3;
        internal DarkUI.Controls.DarkNumericUpDown nudRarity;
        internal PictureBox picItem;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal DarkUI.Controls.DarkComboBox cmbType;
        internal DarkUI.Controls.DarkLabel DarkLabel5;
        internal DarkUI.Controls.DarkComboBox cmbSubType;
        internal DarkUI.Controls.DarkCheckBox chkStackable;
        internal DarkUI.Controls.DarkLabel DarkLabel6;
        internal DarkUI.Controls.DarkComboBox cmbBind;
        internal DarkUI.Controls.DarkLabel DarkLabel7;
        internal DarkUI.Controls.DarkNumericUpDown nudPrice;
        internal DarkUI.Controls.DarkLabel DarkLabel8;
        internal DarkUI.Controls.DarkNumericUpDown nudItemLvl;
        internal DarkUI.Controls.DarkLabel DarkLabel9;
        internal DarkUI.Controls.DarkComboBox cmbAnimation;
        internal DarkUI.Controls.DarkLabel DarkLabel10;
        internal DarkUI.Controls.DarkTextBox txtDescription;
        internal DarkUI.Controls.DarkGroupBox fraPet;
        internal DarkUI.Controls.DarkComboBox cmbPet;
        internal DarkUI.Controls.DarkLabel DarkLabel13;
        internal DarkUI.Controls.DarkGroupBox fraEvents;
        internal DarkUI.Controls.DarkNumericUpDown nudEventValue;
        internal DarkUI.Controls.DarkLabel DarkLabel39;
        internal DarkUI.Controls.DarkNumericUpDown nudEvent;
        internal DarkUI.Controls.DarkLabel DarkLabel38;
        internal DarkUI.Controls.DarkLabel DarkLabel36;
        internal DarkUI.Controls.DarkGroupBox fraBasics;
    }
}