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
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            DarkLabel35 = new DarkUI.Controls.DarkLabel();
            fraEquipment = new DarkUI.Controls.DarkGroupBox();
            fraProjectile = new DarkUI.Controls.DarkGroupBox();
            cmbAmmo = new DarkUI.Controls.DarkComboBox();
            DarkLabel25 = new DarkUI.Controls.DarkLabel();
            cmbProjectile = new DarkUI.Controls.DarkComboBox();
            DarkLabel24 = new DarkUI.Controls.DarkLabel();
            nudPaperdoll = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel23 = new DarkUI.Controls.DarkLabel();
            picPaperdoll = new PictureBox();
            cmbKnockBackTiles = new DarkUI.Controls.DarkComboBox();
            DarkLabel16 = new DarkUI.Controls.DarkLabel();
            chkKnockBack = new DarkUI.Controls.DarkCheckBox();
            nudSpeed = new DarkUI.Controls.DarkNumericUpDown();
            lblSpeed = new DarkUI.Controls.DarkLabel();
            nudDamage = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel15 = new DarkUI.Controls.DarkLabel();
            cmbTool = new DarkUI.Controls.DarkComboBox();
            DarkLabel14 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            nudSpirit = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel22 = new DarkUI.Controls.DarkLabel();
            nudIntelligence = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel21 = new DarkUI.Controls.DarkLabel();
            nudVitality = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel20 = new DarkUI.Controls.DarkLabel();
            nudLuck = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel19 = new DarkUI.Controls.DarkLabel();
            nudStrength = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel17 = new DarkUI.Controls.DarkLabel();
            btnBasics = new DarkUI.Controls.DarkButton();
            btnRequirements = new DarkUI.Controls.DarkButton();
            fraRequirements = new DarkUI.Controls.DarkGroupBox();
            DarkLabel28 = new DarkUI.Controls.DarkLabel();
            nudLevelReq = new DarkUI.Controls.DarkNumericUpDown();
            cmbAccessReq = new DarkUI.Controls.DarkComboBox();
            DarkLabel27 = new DarkUI.Controls.DarkLabel();
            cmbJobReq = new DarkUI.Controls.DarkComboBox();
            DarkLabel26 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            nudSprReq = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel32 = new DarkUI.Controls.DarkLabel();
            nudIntReq = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel33 = new DarkUI.Controls.DarkLabel();
            nudVitReq = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel34 = new DarkUI.Controls.DarkLabel();
            nudLuckReq = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel29 = new DarkUI.Controls.DarkLabel();
            nudStrReq = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel31 = new DarkUI.Controls.DarkLabel();
            btnSave = new DarkUI.Controls.DarkButton();
            btnDelete = new DarkUI.Controls.DarkButton();
            btnCancel = new DarkUI.Controls.DarkButton();
            btnSpawn = new DarkUI.Controls.DarkButton();
            nudSpanwAmount = new DarkUI.Controls.DarkNumericUpDown();
            fraSkill = new DarkUI.Controls.DarkGroupBox();
            cmbSkills = new DarkUI.Controls.DarkComboBox();
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            fraVitals = new DarkUI.Controls.DarkGroupBox();
            nudVitalMod = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            txtName = new DarkUI.Controls.DarkTextBox();
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            nudIcon = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            nudRarity = new DarkUI.Controls.DarkNumericUpDown();
            picItem = new PictureBox();
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            cmbType = new DarkUI.Controls.DarkComboBox();
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            cmbSubType = new DarkUI.Controls.DarkComboBox();
            chkStackable = new DarkUI.Controls.DarkCheckBox();
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            cmbBind = new DarkUI.Controls.DarkComboBox();
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            nudPrice = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel8 = new DarkUI.Controls.DarkLabel();
            nudItemLvl = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel9 = new DarkUI.Controls.DarkLabel();
            cmbAnimation = new DarkUI.Controls.DarkComboBox();
            DarkLabel10 = new DarkUI.Controls.DarkLabel();
            txtDescription = new DarkUI.Controls.DarkTextBox();
            fraPet = new DarkUI.Controls.DarkGroupBox();
            cmbPet = new DarkUI.Controls.DarkComboBox();
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            fraEvents = new DarkUI.Controls.DarkGroupBox();
            nudEventValue = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel39 = new DarkUI.Controls.DarkLabel();
            nudEvent = new DarkUI.Controls.DarkNumericUpDown();
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
            DarkGroupBox1.Location = new Point(2, 2);
            DarkGroupBox1.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Size = new Size(244, 467);
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
            lstIndex.Location = new Point(7, 16);
            lstIndex.Margin = new Padding(4, 3, 4, 3);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(228, 452);
            lstIndex.TabIndex = 1;
            lstIndex.Click += lstIndex_Click;
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
            fraEquipment.Location = new Point(253, 283);
            fraEquipment.Margin = new Padding(4, 3, 4, 3);
            fraEquipment.Name = "fraEquipment";
            fraEquipment.Padding = new Padding(4, 3, 4, 3);
            fraEquipment.Size = new Size(525, 283);
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
            fraProjectile.Location = new Point(140, 192);
            fraProjectile.Margin = new Padding(4, 3, 4, 3);
            fraProjectile.Name = "fraProjectile";
            fraProjectile.Padding = new Padding(4, 3, 4, 3);
            fraProjectile.Size = new Size(379, 80);
            fraProjectile.TabIndex = 63;
            fraProjectile.TabStop = false;
            fraProjectile.Text = "Projectile Settings";
            // 
            // cmbAmmo
            // 
            cmbAmmo.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAmmo.FormattingEnabled = true;
            cmbAmmo.Location = new Point(75, 46);
            cmbAmmo.Margin = new Padding(4, 3, 4, 3);
            cmbAmmo.Name = "cmbAmmo";
            cmbAmmo.Size = new Size(296, 24);
            cmbAmmo.TabIndex = 3;
            cmbAmmo.SelectedIndexChanged += CmbAmmo_SelectedIndexChanged;
            // 
            // DarkLabel25
            // 
            DarkLabel25.AutoSize = true;
            DarkLabel25.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel25.Location = new Point(22, 50);
            DarkLabel25.Margin = new Padding(4, 0, 4, 0);
            DarkLabel25.Name = "DarkLabel25";
            DarkLabel25.Size = new Size(47, 15);
            DarkLabel25.TabIndex = 2;
            DarkLabel25.Text = "Ammo:";
            // 
            // cmbProjectile
            // 
            cmbProjectile.DrawMode = DrawMode.OwnerDrawFixed;
            cmbProjectile.FormattingEnabled = true;
            cmbProjectile.Location = new Point(75, 15);
            cmbProjectile.Margin = new Padding(4, 3, 4, 3);
            cmbProjectile.Name = "cmbProjectile";
            cmbProjectile.Size = new Size(296, 24);
            cmbProjectile.TabIndex = 1;
            cmbProjectile.SelectedIndexChanged += CmbProjectile_SelectedIndexChanged;
            // 
            // DarkLabel24
            // 
            DarkLabel24.AutoSize = true;
            DarkLabel24.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel24.Location = new Point(9, 18);
            DarkLabel24.Margin = new Padding(4, 0, 4, 0);
            DarkLabel24.Name = "DarkLabel24";
            DarkLabel24.Size = new Size(59, 15);
            DarkLabel24.TabIndex = 0;
            DarkLabel24.Text = "Projectile:";
            // 
            // nudPaperdoll
            // 
            nudPaperdoll.Location = new Point(77, 249);
            nudPaperdoll.Margin = new Padding(4, 3, 4, 3);
            nudPaperdoll.Name = "nudPaperdoll";
            nudPaperdoll.Size = new Size(55, 23);
            nudPaperdoll.TabIndex = 59;
            nudPaperdoll.Click += NudPaperdoll_ValueChanged;
            // 
            // DarkLabel23
            // 
            DarkLabel23.AutoSize = true;
            DarkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel23.Location = new Point(7, 252);
            DarkLabel23.Margin = new Padding(4, 0, 4, 0);
            DarkLabel23.Name = "DarkLabel23";
            DarkLabel23.Size = new Size(60, 15);
            DarkLabel23.TabIndex = 58;
            DarkLabel23.Text = "Paperdoll:";
            // 
            // picPaperdoll
            // 
            picPaperdoll.BackColor = Color.Black;
            picPaperdoll.Location = new Point(8, 192);
            picPaperdoll.Margin = new Padding(4, 3, 4, 3);
            picPaperdoll.Name = "picPaperdoll";
            picPaperdoll.Size = new Size(125, 55);
            picPaperdoll.TabIndex = 57;
            picPaperdoll.TabStop = false;
            // 
            // cmbKnockBackTiles
            // 
            cmbKnockBackTiles.DrawMode = DrawMode.OwnerDrawFixed;
            cmbKnockBackTiles.FormattingEnabled = true;
            cmbKnockBackTiles.Items.AddRange(new object[] { "No KnockBack", "1 Tile", "2 Tiles", "3 Tiles", "4 Tiles", "5 Tiles" });
            cmbKnockBackTiles.Location = new Point(379, 67);
            cmbKnockBackTiles.Margin = new Padding(4, 3, 4, 3);
            cmbKnockBackTiles.Name = "cmbKnockBackTiles";
            cmbKnockBackTiles.Size = new Size(138, 24);
            cmbKnockBackTiles.TabIndex = 8;
            cmbKnockBackTiles.SelectedIndexChanged += CmbKnockBackTiles_SelectedIndexChanged;
            // 
            // DarkLabel16
            // 
            DarkLabel16.AutoSize = true;
            DarkLabel16.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel16.Location = new Point(351, 70);
            DarkLabel16.Margin = new Padding(4, 0, 4, 0);
            DarkLabel16.Name = "DarkLabel16";
            DarkLabel16.Size = new Size(20, 15);
            DarkLabel16.TabIndex = 7;
            DarkLabel16.Text = "Of";
            // 
            // chkKnockBack
            // 
            chkKnockBack.AutoSize = true;
            chkKnockBack.Location = new Point(230, 69);
            chkKnockBack.Margin = new Padding(4, 3, 4, 3);
            chkKnockBack.Name = "chkKnockBack";
            chkKnockBack.Size = new Size(107, 19);
            chkKnockBack.TabIndex = 6;
            chkKnockBack.Text = "Has KnockBack";
            chkKnockBack.CheckedChanged += ChkKnockBack_CheckedChanged;
            // 
            // nudSpeed
            // 
            nudSpeed.Location = new Point(116, 68);
            nudSpeed.Margin = new Padding(4, 3, 4, 3);
            nudSpeed.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            nudSpeed.Name = "nudSpeed";
            nudSpeed.Size = new Size(107, 23);
            nudSpeed.TabIndex = 5;
            nudSpeed.Click += NudSpeed_ValueChanged;
            // 
            // lblSpeed
            // 
            lblSpeed.AutoSize = true;
            lblSpeed.ForeColor = Color.FromArgb(220, 220, 220);
            lblSpeed.Location = new Point(7, 70);
            lblSpeed.Margin = new Padding(4, 0, 4, 0);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new Size(60, 15);
            lblSpeed.TabIndex = 4;
            lblSpeed.Text = "Speed: 0.1";
            // 
            // nudDamage
            // 
            nudDamage.Location = new Point(295, 23);
            nudDamage.Margin = new Padding(4, 3, 4, 3);
            nudDamage.Name = "nudDamage";
            nudDamage.Size = new Size(140, 23);
            nudDamage.TabIndex = 3;
            nudDamage.Click += NudDamage_ValueChanged;
            // 
            // DarkLabel15
            // 
            DarkLabel15.AutoSize = true;
            DarkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel15.Location = new Point(230, 25);
            DarkLabel15.Margin = new Padding(4, 0, 4, 0);
            DarkLabel15.Name = "DarkLabel15";
            DarkLabel15.Size = new Size(54, 15);
            DarkLabel15.TabIndex = 2;
            DarkLabel15.Text = "Damage:";
            // 
            // cmbTool
            // 
            cmbTool.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTool.FormattingEnabled = true;
            cmbTool.Items.AddRange(new object[] { "None", "Hatchet", "Rod", "Pickaxe", "Hoe" });
            cmbTool.Location = new Point(82, 22);
            cmbTool.Margin = new Padding(4, 3, 4, 3);
            cmbTool.Name = "cmbTool";
            cmbTool.Size = new Size(140, 24);
            cmbTool.TabIndex = 1;
            cmbTool.SelectedIndexChanged += CmbTool_SelectedIndexChanged;
            // 
            // DarkLabel14
            // 
            DarkLabel14.AutoSize = true;
            DarkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel14.Location = new Point(7, 25);
            DarkLabel14.Margin = new Padding(4, 0, 4, 0);
            DarkLabel14.Name = "DarkLabel14";
            DarkLabel14.Size = new Size(61, 15);
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
            DarkGroupBox2.Location = new Point(7, 98);
            DarkGroupBox2.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Size = new Size(511, 88);
            DarkGroupBox2.TabIndex = 9;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Stats";
            // 
            // nudSpirit
            // 
            nudSpirit.Location = new Point(195, 50);
            nudSpirit.Margin = new Padding(4, 3, 4, 3);
            nudSpirit.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudSpirit.Name = "nudSpirit";
            nudSpirit.Size = new Size(58, 23);
            nudSpirit.TabIndex = 12;
            nudSpirit.Click += NudSpirit_ValueChanged;
            // 
            // DarkLabel22
            // 
            DarkLabel22.AutoSize = true;
            DarkLabel22.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel22.Location = new Point(146, 52);
            DarkLabel22.Margin = new Padding(4, 0, 4, 0);
            DarkLabel22.Name = "DarkLabel22";
            DarkLabel22.Size = new Size(37, 15);
            DarkLabel22.TabIndex = 11;
            DarkLabel22.Text = "Spirit:";
            // 
            // nudIntelligence
            // 
            nudIntelligence.Location = new Point(81, 50);
            nudIntelligence.Margin = new Padding(4, 3, 4, 3);
            nudIntelligence.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudIntelligence.Name = "nudIntelligence";
            nudIntelligence.Size = new Size(58, 23);
            nudIntelligence.TabIndex = 10;
            nudIntelligence.Click += NudIntelligence_ValueChanged;
            // 
            // DarkLabel21
            // 
            DarkLabel21.AutoSize = true;
            DarkLabel21.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel21.Location = new Point(7, 52);
            DarkLabel21.Margin = new Padding(4, 0, 4, 0);
            DarkLabel21.Name = "DarkLabel21";
            DarkLabel21.Size = new Size(71, 15);
            DarkLabel21.TabIndex = 9;
            DarkLabel21.Text = "Intelligence:";
            // 
            // nudVitality
            // 
            nudVitality.Location = new Point(56, 21);
            nudVitality.Margin = new Padding(4, 3, 4, 3);
            nudVitality.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudVitality.Name = "nudVitality";
            nudVitality.Size = new Size(58, 23);
            nudVitality.TabIndex = 8;
            nudVitality.Click += NudVitality_ValueChanged;
            // 
            // DarkLabel20
            // 
            DarkLabel20.AutoSize = true;
            DarkLabel20.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel20.Location = new Point(7, 23);
            DarkLabel20.Margin = new Padding(4, 0, 4, 0);
            DarkLabel20.Name = "DarkLabel20";
            DarkLabel20.Size = new Size(46, 15);
            DarkLabel20.TabIndex = 7;
            DarkLabel20.Text = "Vitality:";
            // 
            // nudLuck
            // 
            nudLuck.Location = new Point(331, 23);
            nudLuck.Margin = new Padding(4, 3, 4, 3);
            nudLuck.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudLuck.Name = "nudLuck";
            nudLuck.Size = new Size(58, 23);
            nudLuck.TabIndex = 6;
            nudLuck.Click += NudLuck_ValueChanged;
            // 
            // DarkLabel19
            // 
            DarkLabel19.AutoSize = true;
            DarkLabel19.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel19.Location = new Point(282, 25);
            DarkLabel19.Margin = new Padding(4, 0, 4, 0);
            DarkLabel19.Name = "DarkLabel19";
            DarkLabel19.Size = new Size(35, 15);
            DarkLabel19.TabIndex = 5;
            DarkLabel19.Text = "Luck:";
            // 
            // nudStrength
            // 
            nudStrength.Location = new Point(195, 21);
            nudStrength.Margin = new Padding(4, 3, 4, 3);
            nudStrength.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudStrength.Name = "nudStrength";
            nudStrength.Size = new Size(58, 23);
            nudStrength.TabIndex = 2;
            nudStrength.Click += NudStrength_ValueChanged;
            // 
            // DarkLabel17
            // 
            DarkLabel17.AutoSize = true;
            DarkLabel17.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel17.Location = new Point(138, 23);
            DarkLabel17.Margin = new Padding(4, 0, 4, 0);
            DarkLabel17.Name = "DarkLabel17";
            DarkLabel17.Size = new Size(55, 15);
            DarkLabel17.TabIndex = 1;
            DarkLabel17.Text = "Strength:";
            // 
            // btnBasics
            // 
            btnBasics.Location = new Point(253, 9);
            btnBasics.Margin = new Padding(4, 3, 4, 3);
            btnBasics.Name = "btnBasics";
            btnBasics.Padding = new Padding(6, 6, 6, 6);
            btnBasics.Size = new Size(88, 27);
            btnBasics.TabIndex = 3;
            btnBasics.Text = "Properties";
            btnBasics.Click += BtnBasics_Click;
            // 
            // btnRequirements
            // 
            btnRequirements.Location = new Point(348, 9);
            btnRequirements.Margin = new Padding(4, 3, 4, 3);
            btnRequirements.Name = "btnRequirements";
            btnRequirements.Padding = new Padding(6, 6, 6, 6);
            btnRequirements.Size = new Size(107, 27);
            btnRequirements.TabIndex = 4;
            btnRequirements.Text = "Requirements";
            btnRequirements.Click += BtnRequirements_Click;
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
            fraRequirements.Location = new Point(253, 43);
            fraRequirements.Margin = new Padding(4, 3, 4, 3);
            fraRequirements.Name = "fraRequirements";
            fraRequirements.Padding = new Padding(4, 3, 4, 3);
            fraRequirements.Size = new Size(525, 233);
            fraRequirements.TabIndex = 5;
            fraRequirements.TabStop = false;
            fraRequirements.Text = "Requirements";
            fraRequirements.Visible = false;
            // 
            // DarkLabel28
            // 
            DarkLabel28.AutoSize = true;
            DarkLabel28.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel28.Location = new Point(7, 78);
            DarkLabel28.Margin = new Padding(4, 0, 4, 0);
            DarkLabel28.Name = "DarkLabel28";
            DarkLabel28.Size = new Size(108, 15);
            DarkLabel28.TabIndex = 5;
            DarkLabel28.Text = "Level Requirement:";
            // 
            // nudLevelReq
            // 
            nudLevelReq.Location = new Point(140, 76);
            nudLevelReq.Margin = new Padding(4, 3, 4, 3);
            nudLevelReq.Name = "nudLevelReq";
            nudLevelReq.Size = new Size(140, 23);
            nudLevelReq.TabIndex = 4;
            nudLevelReq.Click += NudLevelReq_ValueChanged;
            // 
            // cmbAccessReq
            // 
            cmbAccessReq.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAccessReq.FormattingEnabled = true;
            cmbAccessReq.Items.AddRange(new object[] { "Player", "Moderator", "Mapper", "Developer", "Owneer" });
            cmbAccessReq.Location = new Point(140, 45);
            cmbAccessReq.Margin = new Padding(4, 3, 4, 3);
            cmbAccessReq.Name = "cmbAccessReq";
            cmbAccessReq.Size = new Size(206, 24);
            cmbAccessReq.TabIndex = 3;
            cmbAccessReq.SelectedIndexChanged += CmbAccessReq_SelectedIndexChanged;
            // 
            // DarkLabel27
            // 
            DarkLabel27.AutoSize = true;
            DarkLabel27.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel27.Location = new Point(7, 50);
            DarkLabel27.Margin = new Padding(4, 0, 4, 0);
            DarkLabel27.Name = "DarkLabel27";
            DarkLabel27.Size = new Size(117, 15);
            DarkLabel27.TabIndex = 2;
            DarkLabel27.Text = "Access Requirement:";
            // 
            // cmbJobReq
            // 
            cmbJobReq.DrawMode = DrawMode.OwnerDrawFixed;
            cmbJobReq.FormattingEnabled = true;
            cmbJobReq.Location = new Point(140, 15);
            cmbJobReq.Margin = new Padding(4, 3, 4, 3);
            cmbJobReq.Name = "cmbJobReq";
            cmbJobReq.Size = new Size(206, 24);
            cmbJobReq.TabIndex = 1;
            cmbJobReq.SelectedIndexChanged += CmbJobReq_SelectedIndexChanged;
            // 
            // DarkLabel26
            // 
            DarkLabel26.AutoSize = true;
            DarkLabel26.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel26.Location = new Point(7, 18);
            DarkLabel26.Margin = new Padding(4, 0, 4, 0);
            DarkLabel26.Name = "DarkLabel26";
            DarkLabel26.Size = new Size(99, 15);
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
            DarkGroupBox4.Location = new Point(7, 111);
            DarkGroupBox4.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Size = new Size(511, 115);
            DarkGroupBox4.TabIndex = 6;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Stat Requirements";
            // 
            // nudSprReq
            // 
            nudSprReq.Location = new Point(220, 74);
            nudSprReq.Margin = new Padding(4, 3, 4, 3);
            nudSprReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudSprReq.Name = "nudSprReq";
            nudSprReq.Size = new Size(58, 23);
            nudSprReq.TabIndex = 18;
            nudSprReq.Click += NudSprReq_ValueChanged;
            // 
            // DarkLabel32
            // 
            DarkLabel32.AutoSize = true;
            DarkLabel32.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel32.Location = new Point(139, 75);
            DarkLabel32.Margin = new Padding(4, 0, 4, 0);
            DarkLabel32.Name = "DarkLabel32";
            DarkLabel32.Size = new Size(37, 15);
            DarkLabel32.TabIndex = 17;
            DarkLabel32.Text = "Spirit:";
            // 
            // nudIntReq
            // 
            nudIntReq.Location = new Point(220, 25);
            nudIntReq.Margin = new Padding(4, 3, 4, 3);
            nudIntReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudIntReq.Name = "nudIntReq";
            nudIntReq.Size = new Size(58, 23);
            nudIntReq.TabIndex = 16;
            nudIntReq.Click += NudIntReq_ValueChanged;
            // 
            // DarkLabel33
            // 
            DarkLabel33.AutoSize = true;
            DarkLabel33.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel33.Location = new Point(139, 28);
            DarkLabel33.Margin = new Padding(4, 0, 4, 0);
            DarkLabel33.Name = "DarkLabel33";
            DarkLabel33.Size = new Size(71, 15);
            DarkLabel33.TabIndex = 15;
            DarkLabel33.Text = "Intelligence:";
            // 
            // nudVitReq
            // 
            nudVitReq.Location = new Point(72, 75);
            nudVitReq.Margin = new Padding(4, 3, 4, 3);
            nudVitReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudVitReq.Name = "nudVitReq";
            nudVitReq.Size = new Size(58, 23);
            nudVitReq.TabIndex = 14;
            nudVitReq.Click += NudVitReq_ValueChanged;
            // 
            // DarkLabel34
            // 
            DarkLabel34.AutoSize = true;
            DarkLabel34.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel34.Location = new Point(7, 76);
            DarkLabel34.Margin = new Padding(4, 0, 4, 0);
            DarkLabel34.Name = "DarkLabel34";
            DarkLabel34.Size = new Size(46, 15);
            DarkLabel34.TabIndex = 13;
            DarkLabel34.Text = "Vitality:";
            // 
            // nudLuckReq
            // 
            nudLuckReq.Location = new Point(337, 26);
            nudLuckReq.Margin = new Padding(4, 3, 4, 3);
            nudLuckReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudLuckReq.Name = "nudLuckReq";
            nudLuckReq.Size = new Size(58, 23);
            nudLuckReq.TabIndex = 12;
            nudLuckReq.Click += NudLuckReq_ValueChanged;
            // 
            // DarkLabel29
            // 
            DarkLabel29.AutoSize = true;
            DarkLabel29.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel29.Location = new Point(294, 28);
            DarkLabel29.Margin = new Padding(4, 0, 4, 0);
            DarkLabel29.Name = "DarkLabel29";
            DarkLabel29.Size = new Size(35, 15);
            DarkLabel29.TabIndex = 11;
            DarkLabel29.Text = "Luck:";
            // 
            // nudStrReq
            // 
            nudStrReq.Location = new Point(75, 27);
            nudStrReq.Margin = new Padding(4, 3, 4, 3);
            nudStrReq.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudStrReq.Name = "nudStrReq";
            nudStrReq.Size = new Size(58, 23);
            nudStrReq.TabIndex = 8;
            nudStrReq.Click += NudStrReq_ValueChanged;
            // 
            // DarkLabel31
            // 
            DarkLabel31.AutoSize = true;
            DarkLabel31.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel31.Location = new Point(7, 28);
            DarkLabel31.Margin = new Padding(4, 0, 4, 0);
            DarkLabel31.Name = "DarkLabel31";
            DarkLabel31.Size = new Size(55, 15);
            DarkLabel31.TabIndex = 7;
            DarkLabel31.Text = "Strength:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(9, 473);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(6, 6, 6, 6);
            btnSave.Size = new Size(229, 27);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.Click += BtnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(9, 506);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(6, 6, 6, 6);
            btnDelete.Size = new Size(229, 27);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(9, 539);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(6, 6, 6, 6);
            btnCancel.Size = new Size(229, 27);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSpawn
            // 
            btnSpawn.Location = new Point(463, 9);
            btnSpawn.Margin = new Padding(4, 3, 4, 3);
            btnSpawn.Name = "btnSpawn";
            btnSpawn.Padding = new Padding(6, 6, 6, 6);
            btnSpawn.Size = new Size(88, 27);
            btnSpawn.TabIndex = 10;
            btnSpawn.Text = "Spawn";
            btnSpawn.Click += btnSpawn_Click;
            // 
            // nudSpanwAmount
            // 
            nudSpanwAmount.Location = new Point(558, 14);
            nudSpanwAmount.Margin = new Padding(4, 3, 4, 3);
            nudSpanwAmount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSpanwAmount.Name = "nudSpanwAmount";
            nudSpanwAmount.Size = new Size(148, 23);
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
            fraSkill.Location = new Point(284, 138);
            fraSkill.Margin = new Padding(4, 3, 4, 3);
            fraSkill.Name = "fraSkill";
            fraSkill.Padding = new Padding(4, 3, 4, 3);
            fraSkill.Size = new Size(233, 46);
            fraSkill.TabIndex = 24;
            fraSkill.TabStop = false;
            fraSkill.Text = "Skills";
            // 
            // cmbSkills
            // 
            cmbSkills.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSkills.FormattingEnabled = true;
            cmbSkills.Location = new Point(48, 16);
            cmbSkills.Margin = new Padding(4, 3, 4, 3);
            cmbSkills.Name = "cmbSkills";
            cmbSkills.Size = new Size(178, 24);
            cmbSkills.TabIndex = 1;
            cmbSkills.SelectedIndexChanged += CmbSkills_SelectedIndexChanged;
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(7, 20);
            DarkLabel12.Margin = new Padding(4, 0, 4, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(31, 15);
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
            fraVitals.Location = new Point(284, 138);
            fraVitals.Margin = new Padding(4, 3, 4, 3);
            fraVitals.Name = "fraVitals";
            fraVitals.Padding = new Padding(4, 3, 4, 3);
            fraVitals.Size = new Size(233, 46);
            fraVitals.TabIndex = 23;
            fraVitals.TabStop = false;
            fraVitals.Text = "Vitals";
            // 
            // nudVitalMod
            // 
            nudVitalMod.Location = new Point(78, 19);
            nudVitalMod.Margin = new Padding(4, 3, 4, 3);
            nudVitalMod.Name = "nudVitalMod";
            nudVitalMod.Size = new Size(148, 23);
            nudVitalMod.TabIndex = 1;
            nudVitalMod.Click += NudVitalMod_Click;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(7, 20);
            DarkLabel11.Margin = new Padding(4, 0, 4, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(35, 15);
            DarkLabel11.TabIndex = 0;
            DarkLabel11.Text = "Mod:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(85, 16);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(266, 23);
            txtName.TabIndex = 1;
            txtName.TextChanged += TxtName_TextChanged;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(358, 18);
            DarkLabel2.Margin = new Padding(4, 0, 4, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(33, 15);
            DarkLabel2.TabIndex = 2;
            DarkLabel2.Text = "Icon:";
            // 
            // nudIcon
            // 
            nudIcon.Location = new Point(415, 16);
            nudIcon.Margin = new Padding(4, 3, 4, 3);
            nudIcon.Name = "nudIcon";
            nudIcon.Size = new Size(57, 23);
            nudIcon.TabIndex = 3;
            nudIcon.Click += NudPic_Click;
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(358, 50);
            DarkLabel3.Margin = new Padding(4, 0, 4, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(40, 15);
            DarkLabel3.TabIndex = 4;
            DarkLabel3.Text = "Rarity:";
            // 
            // nudRarity
            // 
            nudRarity.Location = new Point(415, 46);
            nudRarity.Margin = new Padding(4, 3, 4, 3);
            nudRarity.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            nudRarity.Name = "nudRarity";
            nudRarity.Size = new Size(57, 23);
            nudRarity.TabIndex = 5;
            nudRarity.Click += NudRarity_ValueChanged;
            // 
            // picItem
            // 
            picItem.BackColor = Color.Black;
            picItem.Location = new Point(480, 16);
            picItem.Margin = new Padding(4, 3, 4, 3);
            picItem.Name = "picItem";
            picItem.Size = new Size(32, 32);
            picItem.TabIndex = 7;
            picItem.TabStop = false;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(7, 50);
            DarkLabel4.Margin = new Padding(4, 0, 4, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(35, 15);
            DarkLabel4.TabIndex = 8;
            DarkLabel4.Text = "Type:";
            // 
            // cmbType
            // 
            cmbType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "Equipment", "Consumable", "Common Event", "Currency", "Skill", "Projectile", "Pet" });
            cmbType.Location = new Point(85, 46);
            cmbType.Margin = new Padding(4, 3, 4, 3);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(140, 24);
            cmbType.TabIndex = 9;
            cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(7, 81);
            DarkLabel5.Margin = new Padding(4, 0, 4, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(60, 15);
            DarkLabel5.TabIndex = 10;
            DarkLabel5.Text = "Sub-Type:";
            // 
            // cmbSubType
            // 
            cmbSubType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSubType.FormattingEnabled = true;
            cmbSubType.Items.AddRange(new object[] { "Weapon", "Armor", "Helmet", "Shield", "Add HP", "Add MP", "Add SP", "Sub HP", "Sub MP", "Sub SP", "Experience", "Common Event", "Currency", "Skill" });
            cmbSubType.Location = new Point(85, 77);
            cmbSubType.Margin = new Padding(4, 3, 4, 3);
            cmbSubType.Name = "cmbSubType";
            cmbSubType.Size = new Size(140, 24);
            cmbSubType.TabIndex = 11;
            cmbSubType.SelectedIndexChanged += CmbSubType_SelectedIndexChanged;
            // 
            // chkStackable
            // 
            chkStackable.AutoSize = true;
            chkStackable.Location = new Point(265, 48);
            chkStackable.Margin = new Padding(4, 3, 4, 3);
            chkStackable.Name = "chkStackable";
            chkStackable.Size = new Size(76, 19);
            chkStackable.TabIndex = 12;
            chkStackable.Text = "Stackable";
            chkStackable.CheckedChanged += ChkStackable_CheckedChanged;
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(261, 81);
            DarkLabel6.Margin = new Padding(4, 0, 4, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(62, 15);
            DarkLabel6.TabIndex = 13;
            DarkLabel6.Text = "Bind Type:";
            // 
            // cmbBind
            // 
            cmbBind.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBind.FormattingEnabled = true;
            cmbBind.Items.AddRange(new object[] { "None", "Bind on Pickup", "Bind on Equip" });
            cmbBind.Location = new Point(332, 77);
            cmbBind.Margin = new Padding(4, 3, 4, 3);
            cmbBind.Name = "cmbBind";
            cmbBind.Size = new Size(184, 24);
            cmbBind.TabIndex = 14;
            cmbBind.SelectedIndexChanged += CmbBind_SelectedIndexChanged;
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(7, 111);
            DarkLabel7.Margin = new Padding(4, 0, 4, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(36, 15);
            DarkLabel7.TabIndex = 15;
            DarkLabel7.Text = "Price:";
            // 
            // nudPrice
            // 
            nudPrice.Location = new Point(85, 108);
            nudPrice.Margin = new Padding(4, 3, 4, 3);
            nudPrice.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudPrice.Name = "nudPrice";
            nudPrice.Size = new Size(80, 23);
            nudPrice.TabIndex = 16;
            nudPrice.Click += NudPrice_ValueChanged;
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(173, 111);
            DarkLabel8.Margin = new Padding(4, 0, 4, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(37, 15);
            DarkLabel8.TabIndex = 17;
            DarkLabel8.Text = "Level:";
            // 
            // nudItemLvl
            // 
            nudItemLvl.Location = new Point(244, 108);
            nudItemLvl.Margin = new Padding(4, 3, 4, 3);
            nudItemLvl.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudItemLvl.Name = "nudItemLvl";
            nudItemLvl.Size = new Size(56, 23);
            nudItemLvl.TabIndex = 18;
            nudItemLvl.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudItemLvl.Click += NuditemLvl_ValueChanged;
            // 
            // DarkLabel9
            // 
            DarkLabel9.AutoSize = true;
            DarkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel9.Location = new Point(307, 111);
            DarkLabel9.Margin = new Padding(4, 0, 4, 0);
            DarkLabel9.Name = "DarkLabel9";
            DarkLabel9.Size = new Size(66, 15);
            DarkLabel9.TabIndex = 19;
            DarkLabel9.Text = "Animation:";
            // 
            // cmbAnimation
            // 
            cmbAnimation.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnimation.FormattingEnabled = true;
            cmbAnimation.Location = new Point(379, 107);
            cmbAnimation.Margin = new Padding(4, 3, 4, 3);
            cmbAnimation.Name = "cmbAnimation";
            cmbAnimation.Size = new Size(137, 24);
            cmbAnimation.TabIndex = 20;
            cmbAnimation.SelectedIndexChanged += CmbAnimation_SelectedIndexChanged;
            // 
            // DarkLabel10
            // 
            DarkLabel10.AutoSize = true;
            DarkLabel10.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel10.Location = new Point(7, 137);
            DarkLabel10.Margin = new Padding(4, 0, 4, 0);
            DarkLabel10.Name = "DarkLabel10";
            DarkLabel10.Size = new Size(70, 15);
            DarkLabel10.TabIndex = 21;
            DarkLabel10.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.BackColor = Color.FromArgb(69, 73, 74);
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.ForeColor = Color.FromArgb(220, 220, 220);
            txtDescription.Location = new Point(10, 156);
            txtDescription.Margin = new Padding(4, 3, 4, 3);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(266, 69);
            txtDescription.TabIndex = 22;
            txtDescription.TextChanged += TxtDescription_TextChanged;
            // 
            // fraPet
            // 
            fraPet.BackColor = Color.FromArgb(45, 45, 48);
            fraPet.BorderColor = Color.FromArgb(90, 90, 90);
            fraPet.Controls.Add(cmbPet);
            fraPet.Controls.Add(DarkLabel13);
            fraPet.ForeColor = Color.Gainsboro;
            fraPet.Location = new Point(284, 183);
            fraPet.Margin = new Padding(4, 3, 4, 3);
            fraPet.Name = "fraPet";
            fraPet.Padding = new Padding(4, 3, 4, 3);
            fraPet.Size = new Size(233, 46);
            fraPet.TabIndex = 25;
            fraPet.TabStop = false;
            fraPet.Text = "Pets";
            // 
            // cmbPet
            // 
            cmbPet.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPet.FormattingEnabled = true;
            cmbPet.Location = new Point(48, 16);
            cmbPet.Margin = new Padding(4, 3, 4, 3);
            cmbPet.Name = "cmbPet";
            cmbPet.Size = new Size(178, 24);
            cmbPet.TabIndex = 1;
            cmbPet.SelectedIndexChanged += CmbPet_SelectedIndexChanged;
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(7, 20);
            DarkLabel13.Margin = new Padding(4, 0, 4, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(37, 15);
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
            fraEvents.Location = new Point(292, 137);
            fraEvents.Margin = new Padding(4, 3, 4, 3);
            fraEvents.Name = "fraEvents";
            fraEvents.Padding = new Padding(4, 3, 4, 3);
            fraEvents.Size = new Size(233, 96);
            fraEvents.TabIndex = 27;
            fraEvents.TabStop = false;
            fraEvents.Text = "Events";
            // 
            // nudEventValue
            // 
            nudEventValue.Location = new Point(78, 57);
            nudEventValue.Margin = new Padding(4, 3, 4, 3);
            nudEventValue.Name = "nudEventValue";
            nudEventValue.Size = new Size(148, 23);
            nudEventValue.TabIndex = 5;
            // 
            // DarkLabel39
            // 
            DarkLabel39.AutoSize = true;
            DarkLabel39.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel39.Location = new Point(9, 61);
            DarkLabel39.Margin = new Padding(4, 0, 4, 0);
            DarkLabel39.Name = "DarkLabel39";
            DarkLabel39.Size = new Size(38, 15);
            DarkLabel39.TabIndex = 4;
            DarkLabel39.Text = "Value:";
            // 
            // nudEvent
            // 
            nudEvent.Location = new Point(78, 16);
            nudEvent.Margin = new Padding(4, 3, 4, 3);
            nudEvent.Name = "nudEvent";
            nudEvent.Size = new Size(148, 23);
            nudEvent.TabIndex = 1;
            nudEvent.ValueChanged += nudEvents_ValueChanged;
            // 
            // DarkLabel38
            // 
            DarkLabel38.AutoSize = true;
            DarkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel38.Location = new Point(9, 20);
            DarkLabel38.Margin = new Padding(4, 0, 4, 0);
            DarkLabel38.Name = "DarkLabel38";
            DarkLabel38.Size = new Size(21, 15);
            DarkLabel38.TabIndex = 0;
            DarkLabel38.Text = "ID:";
            // 
            // DarkLabel36
            // 
            DarkLabel36.AutoSize = true;
            DarkLabel36.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel36.Location = new Point(7, 18);
            DarkLabel36.Margin = new Padding(4, 0, 4, 0);
            DarkLabel36.Name = "DarkLabel36";
            DarkLabel36.Size = new Size(42, 15);
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
            fraBasics.Location = new Point(253, 43);
            fraBasics.Margin = new Padding(4, 3, 4, 3);
            fraBasics.Name = "fraBasics";
            fraBasics.Padding = new Padding(4, 3, 4, 3);
            fraBasics.Size = new Size(525, 233);
            fraBasics.TabIndex = 1;
            fraBasics.TabStop = false;
            fraBasics.Text = "Properties";
            // 
            // frmEditor_Item
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(785, 572);
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
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmEditor_Item";
            Text = "Item Editor";
            FormClosing += frmEditor_Item_FormClosing;
            Load += frmEditor_Item_Load;
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