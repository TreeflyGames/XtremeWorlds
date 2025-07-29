using DarkUI.Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmEditor_Map : Form
    {

        // Shared instance of the form
        private static frmEditor_Map _instance;

        // Public property to get the shared instance
        public static frmEditor_Map Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Map();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditor_Map));
            btnClearAttribute = new DarkButton();
            optTrap = new DarkRadioButton();
            optHeal = new DarkRadioButton();
            optBank = new DarkRadioButton();
            optShop = new DarkRadioButton();
            optNpcSpawn = new DarkRadioButton();
            optResource = new DarkRadioButton();
            optNpcAvoid = new DarkRadioButton();
            optItem = new DarkRadioButton();
            optWarp = new DarkRadioButton();
            optBlocked = new DarkRadioButton();
            pnlBack = new Panel();
            picBackSelect = new PictureBox();
            pnlAttributes = new Panel();
            fraAnimation = new DarkGroupBox();
            cmbAnimation = new DarkComboBox();
            brnAnimation = new DarkButton();
            fraMapWarp = new DarkGroupBox();
            btnMapWarp = new DarkButton();
            scrlMapWarpY = new HScrollBar();
            scrlMapWarpX = new HScrollBar();
            scrlMapWarpMap = new HScrollBar();
            lblMapWarpY = new DarkLabel();
            lblMapWarpX = new DarkLabel();
            lblMapWarpMap = new DarkLabel();
            fraNpcSpawn = new DarkGroupBox();
            lstNpc = new DarkComboBox();
            btnNpcSpawn = new DarkButton();
            scrlNpcDir = new HScrollBar();
            lblNpcDir = new DarkLabel();
            fraHeal = new DarkGroupBox();
            scrlHeal = new HScrollBar();
            lblHeal = new DarkLabel();
            cmbHeal = new DarkComboBox();
            btnHeal = new DarkButton();
            fraShop = new DarkGroupBox();
            cmbShop = new DarkComboBox();
            btnShop = new DarkButton();
            fraResource = new DarkGroupBox();
            btnResourceOk = new DarkButton();
            scrlResource = new HScrollBar();
            lblResource = new DarkLabel();
            fraMapItem = new DarkGroupBox();
            picMapItem = new PictureBox();
            btnMapItem = new DarkButton();
            scrlMapItemValue = new HScrollBar();
            scrlMapItem = new HScrollBar();
            lblMapItem = new DarkLabel();
            fraTrap = new DarkGroupBox();
            btnTrap = new DarkButton();
            scrlTrap = new HScrollBar();
            lblTrap = new DarkLabel();
            ToolStrip = new ToolStrip();
            tsbSave = new ToolStripButton();
            tsbDiscard = new ToolStripButton();
            ToolStripSeparator1 = new ToolStripSeparator();
            tsbMapGrid = new ToolStripButton();
            tsbOpacity = new ToolStripButton();
            ToolStripSeparator2 = new ToolStripSeparator();
            tsbFill = new ToolStripButton();
            tsbClear = new ToolStripButton();
            tsbEyeDropper = new ToolStripButton();
            tsbCopyMap = new ToolStripButton();
            tsbDeleteMap = new ToolStripButton();
            tsbUndo = new ToolStripButton();
            tsbRedo = new ToolStripButton();
            tsbScreenshot = new ToolStripButton();
            tsbTileset = new ToolStripButton();
            tabpages = new TabControl();
            tpTiles = new TabPage();
            cmbAutoTile = new DarkComboBox();
            Label11 = new DarkLabel();
            Label10 = new DarkLabel();
            cmbLayers = new DarkComboBox();
            Label9 = new DarkLabel();
            cmbTileSets = new DarkComboBox();
            tpAttributes = new TabPage();
            optNoCrossing = new DarkRadioButton();
            btnFillAttributes = new DarkButton();
            optInfo = new DarkRadioButton();
            Label23 = new DarkLabel();
            cmbAttribute = new DarkComboBox();
            optAnimation = new DarkRadioButton();
            tpNpcs = new TabPage();
            fraNpcs = new DarkGroupBox();
            Label18 = new DarkLabel();
            Label17 = new DarkLabel();
            cmbNpcList = new DarkComboBox();
            lstMapNpc = new ListBox();
            ComboBox23 = new DarkComboBox();
            tpSettings = new TabPage();
            fraMapSettings = new DarkGroupBox();
            Label22 = new DarkLabel();
            lstShop = new DarkComboBox();
            Label8 = new DarkLabel();
            lstMoral = new DarkComboBox();
            fraMapLinks = new DarkGroupBox();
            txtDown = new DarkTextBox();
            txtLeft = new DarkTextBox();
            lblMap = new DarkLabel();
            txtRight = new DarkTextBox();
            txtUp = new DarkTextBox();
            fraBootSettings = new DarkGroupBox();
            chkIndoors = new DarkCheckBox();
            chkNoMapRespawn = new DarkCheckBox();
            txtBootMap = new DarkTextBox();
            Label5 = new DarkLabel();
            txtBootY = new DarkTextBox();
            Label3 = new DarkLabel();
            txtBootX = new DarkTextBox();
            Label4 = new DarkLabel();
            fraMaxSizes = new DarkGroupBox();
            txtMaxY = new DarkTextBox();
            Label2 = new DarkLabel();
            txtMaxX = new DarkTextBox();
            Label7 = new DarkLabel();
            GroupBox2 = new DarkGroupBox();
            btnPreview = new DarkButton();
            lstMusic = new ListBox();
            txtName = new DarkTextBox();
            Label6 = new DarkLabel();
            tpDirBlock = new TabPage();
            Label12 = new DarkLabel();
            tpEvents = new TabPage();
            lblPasteMode = new DarkLabel();
            lblCopyMode = new DarkLabel();
            btnPasteEvent = new DarkButton();
            Label16 = new DarkLabel();
            btnCopyEvent = new DarkButton();
            Label15 = new DarkLabel();
            Label13 = new DarkLabel();
            tpEffects = new TabPage();
            GroupBox6 = new DarkGroupBox();
            scrlMapBrightness = new HScrollBar();
            GroupBox5 = new DarkGroupBox();
            cmbParallax = new DarkComboBox();
            GroupBox4 = new DarkGroupBox();
            cmbPanorama = new DarkComboBox();
            GroupBox3 = new DarkGroupBox();
            chkTint = new DarkCheckBox();
            lblMapAlpha = new DarkLabel();
            lblMapBlue = new DarkLabel();
            lblMapGreen = new DarkLabel();
            lblMapRed = new DarkLabel();
            scrlMapAlpha = new HScrollBar();
            scrlMapBlue = new HScrollBar();
            scrlMapGreen = new HScrollBar();
            scrlMapRed = new HScrollBar();
            GroupBox1 = new DarkGroupBox();
            scrlFogOpacity = new HScrollBar();
            lblFogOpacity = new DarkLabel();
            scrlFogSpeed = new HScrollBar();
            lblFogSpeed = new DarkLabel();
            scrlIntensity = new HScrollBar();
            lblIntensity = new DarkLabel();
            scrlFog = new HScrollBar();
            lblFogIndex = new DarkLabel();
            Label14 = new DarkLabel();
            cmbWeather = new DarkComboBox();
            pnlBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBackSelect).BeginInit();
            pnlAttributes.SuspendLayout();
            fraAnimation.SuspendLayout();
            fraMapWarp.SuspendLayout();
            fraNpcSpawn.SuspendLayout();
            fraHeal.SuspendLayout();
            fraShop.SuspendLayout();
            fraResource.SuspendLayout();
            fraMapItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMapItem).BeginInit();
            fraTrap.SuspendLayout();
            ToolStrip.SuspendLayout();
            tabpages.SuspendLayout();
            tpTiles.SuspendLayout();
            tpAttributes.SuspendLayout();
            tpNpcs.SuspendLayout();
            fraNpcs.SuspendLayout();
            tpSettings.SuspendLayout();
            fraMapSettings.SuspendLayout();
            fraMapLinks.SuspendLayout();
            fraBootSettings.SuspendLayout();
            fraMaxSizes.SuspendLayout();
            GroupBox2.SuspendLayout();
            tpDirBlock.SuspendLayout();
            tpEvents.SuspendLayout();
            tpEffects.SuspendLayout();
            GroupBox6.SuspendLayout();
            GroupBox5.SuspendLayout();
            GroupBox4.SuspendLayout();
            GroupBox3.SuspendLayout();
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnClearAttribute
            // 
            btnClearAttribute.Location = new Point(743, 1214);
            btnClearAttribute.Margin = new Padding(7, 6, 7, 6);
            btnClearAttribute.Name = "btnClearAttribute";
            btnClearAttribute.Padding = new Padding(9, 11, 9, 11);
            btnClearAttribute.Size = new Size(253, 62);
            btnClearAttribute.TabIndex = 14;
            btnClearAttribute.Text = "Clear Attributes";
            btnClearAttribute.Click += BtnClearAttribute_Click;
            // 
            // optTrap
            // 
            optTrap.AutoSize = true;
            optTrap.Location = new Point(513, 124);
            optTrap.Margin = new Padding(7, 6, 7, 6);
            optTrap.Name = "optTrap";
            optTrap.Size = new Size(90, 36);
            optTrap.TabIndex = 12;
            optTrap.Text = "Trap";
            optTrap.CheckedChanged += OptTrap_CheckedChanged;
            // 
            // optHeal
            // 
            optHeal.AutoSize = true;
            optHeal.Location = new Point(375, 124);
            optHeal.Margin = new Padding(7, 6, 7, 6);
            optHeal.Name = "optHeal";
            optHeal.Size = new Size(93, 36);
            optHeal.TabIndex = 11;
            optHeal.Text = "Heal";
            optHeal.CheckedChanged += OptHeal_CheckedChanged;
            // 
            // optBank
            // 
            optBank.AutoSize = true;
            optBank.Location = new Point(219, 124);
            optBank.Margin = new Padding(7, 6, 7, 6);
            optBank.Name = "optBank";
            optBank.Size = new Size(97, 36);
            optBank.TabIndex = 10;
            optBank.Text = "Bank";
            // 
            // optShop
            // 
            optShop.AutoSize = true;
            optShop.Location = new Point(886, 34);
            optShop.Margin = new Padding(7, 6, 7, 6);
            optShop.Name = "optShop";
            optShop.Size = new Size(100, 36);
            optShop.TabIndex = 9;
            optShop.Text = "Shop";
            optShop.CheckedChanged += OptShop_CheckedChanged;
            // 
            // optNpcSpawn
            // 
            optNpcSpawn.AutoSize = true;
            optNpcSpawn.Location = new Point(693, 34);
            optNpcSpawn.Margin = new Padding(7, 6, 7, 6);
            optNpcSpawn.Name = "optNpcSpawn";
            optNpcSpawn.Size = new Size(165, 36);
            optNpcSpawn.TabIndex = 8;
            optNpcSpawn.Text = "Npc Spawn";
            optNpcSpawn.CheckedChanged += OptNpcSpawn_CheckedChanged;
            // 
            // optResource
            // 
            optResource.AutoSize = true;
            optResource.Location = new Point(22, 124);
            optResource.Margin = new Padding(7, 6, 7, 6);
            optResource.Name = "optResource";
            optResource.Size = new Size(141, 36);
            optResource.TabIndex = 6;
            optResource.Text = "Resource";
            optResource.CheckedChanged += OptResource_CheckedChanged;
            // 
            // optNpcAvoid
            // 
            optNpcAvoid.AutoSize = true;
            optNpcAvoid.Location = new Point(513, 34);
            optNpcAvoid.Margin = new Padding(7, 6, 7, 6);
            optNpcAvoid.Name = "optNpcAvoid";
            optNpcAvoid.Size = new Size(156, 36);
            optNpcAvoid.TabIndex = 3;
            optNpcAvoid.Text = "Npc Avoid";
            // 
            // optItem
            // 
            optItem.AutoSize = true;
            optItem.Location = new Point(375, 34);
            optItem.Margin = new Padding(7, 6, 7, 6);
            optItem.Name = "optItem";
            optItem.Size = new Size(93, 36);
            optItem.TabIndex = 2;
            optItem.Text = "Item";
            optItem.CheckedChanged += OptItem_CheckedChanged;
            // 
            // optWarp
            // 
            optWarp.AutoSize = true;
            optWarp.Location = new Point(219, 34);
            optWarp.Margin = new Padding(7, 6, 7, 6);
            optWarp.Name = "optWarp";
            optWarp.Size = new Size(100, 36);
            optWarp.TabIndex = 1;
            optWarp.Text = "Warp";
            optWarp.CheckedChanged += OptWarp_CheckedChanged;
            // 
            // optBlocked
            // 
            optBlocked.AutoSize = true;
            optBlocked.Checked = true;
            optBlocked.Location = new Point(22, 34);
            optBlocked.Margin = new Padding(7, 6, 7, 6);
            optBlocked.Name = "optBlocked";
            optBlocked.Size = new Size(129, 36);
            optBlocked.TabIndex = 0;
            optBlocked.TabStop = true;
            optBlocked.Text = "Blocked";
            optBlocked.CheckedChanged += OptBlocked_CheckedChanged;
            // 
            // pnlBack
            // 
            pnlBack.Controls.Add(picBackSelect);
            pnlBack.Location = new Point(13, 19);
            pnlBack.Margin = new Padding(7, 6, 7, 6);
            pnlBack.Name = "pnlBack";
            pnlBack.Size = new Size(977, 1135);
            pnlBack.TabIndex = 9;
            // 
            // picBackSelect
            // 
            picBackSelect.BackColor = Color.Black;
            picBackSelect.Location = new Point(43, -7);
            picBackSelect.Margin = new Padding(7, 6, 7, 6);
            picBackSelect.Name = "picBackSelect";
            picBackSelect.Size = new Size(951, 1092);
            picBackSelect.TabIndex = 22;
            picBackSelect.TabStop = false;
            picBackSelect.Paint += picBackSelect_Paint;
            picBackSelect.MouseDown += PicBackSelect_MouseDown;
            picBackSelect.MouseMove += PicBackSelect_MouseMove;
            // 
            // pnlAttributes
            // 
            pnlAttributes.Controls.Add(fraAnimation);
            pnlAttributes.Controls.Add(fraMapWarp);
            pnlAttributes.Controls.Add(fraNpcSpawn);
            pnlAttributes.Controls.Add(fraHeal);
            pnlAttributes.Controls.Add(fraShop);
            pnlAttributes.Controls.Add(fraResource);
            pnlAttributes.Controls.Add(fraMapItem);
            pnlAttributes.Controls.Add(fraTrap);
            pnlAttributes.Location = new Point(1031, 119);
            pnlAttributes.Margin = new Padding(7, 6, 7, 6);
            pnlAttributes.Name = "pnlAttributes";
            pnlAttributes.Size = new Size(1088, 1210);
            pnlAttributes.TabIndex = 12;
            pnlAttributes.Visible = false;
            // 
            // fraAnimation
            // 
            fraAnimation.BorderColor = Color.FromArgb(51, 51, 51);
            fraAnimation.Controls.Add(cmbAnimation);
            fraAnimation.Controls.Add(brnAnimation);
            fraAnimation.Location = new Point(397, 621);
            fraAnimation.Margin = new Padding(7, 6, 7, 6);
            fraAnimation.Name = "fraAnimation";
            fraAnimation.Padding = new Padding(7, 6, 7, 6);
            fraAnimation.Size = new Size(377, 277);
            fraAnimation.TabIndex = 17;
            fraAnimation.TabStop = false;
            fraAnimation.Text = "Animation";
            fraAnimation.Visible = false;
            // 
            // cmbAnimation
            // 
            cmbAnimation.DrawMode = DrawMode.OwnerDrawVariable;
            cmbAnimation.FormattingEnabled = true;
            cmbAnimation.Items.AddRange(new object[] { "Heal HP", "Heal MP" });
            cmbAnimation.Location = new Point(13, 47);
            cmbAnimation.Margin = new Padding(7, 6, 7, 6);
            cmbAnimation.Name = "cmbAnimation";
            cmbAnimation.Size = new Size(331, 40);
            cmbAnimation.TabIndex = 37;
            // 
            // brnAnimation
            // 
            brnAnimation.Location = new Point(80, 188);
            brnAnimation.Margin = new Padding(7, 6, 7, 6);
            brnAnimation.Name = "brnAnimation";
            brnAnimation.Padding = new Padding(9, 11, 9, 11);
            brnAnimation.Size = new Size(195, 68);
            brnAnimation.TabIndex = 6;
            brnAnimation.Text = "Accept";
            brnAnimation.Click += brnAnimation_Click;
            // 
            // fraMapWarp
            // 
            fraMapWarp.BorderColor = Color.FromArgb(51, 51, 51);
            fraMapWarp.Controls.Add(btnMapWarp);
            fraMapWarp.Controls.Add(scrlMapWarpY);
            fraMapWarp.Controls.Add(scrlMapWarpX);
            fraMapWarp.Controls.Add(scrlMapWarpMap);
            fraMapWarp.Controls.Add(lblMapWarpY);
            fraMapWarp.Controls.Add(lblMapWarpX);
            fraMapWarp.Controls.Add(lblMapWarpMap);
            fraMapWarp.Location = new Point(19, 911);
            fraMapWarp.Margin = new Padding(7, 6, 7, 6);
            fraMapWarp.Name = "fraMapWarp";
            fraMapWarp.Padding = new Padding(7, 6, 7, 6);
            fraMapWarp.Size = new Size(546, 292);
            fraMapWarp.TabIndex = 0;
            fraMapWarp.TabStop = false;
            fraMapWarp.Text = "Map Warp";
            // 
            // btnMapWarp
            // 
            btnMapWarp.Location = new Point(173, 218);
            btnMapWarp.Margin = new Padding(7, 6, 7, 6);
            btnMapWarp.Name = "btnMapWarp";
            btnMapWarp.Padding = new Padding(9, 11, 9, 11);
            btnMapWarp.Size = new Size(195, 68);
            btnMapWarp.TabIndex = 6;
            btnMapWarp.Text = "Accept";
            btnMapWarp.Click += BtnMapWarp_Click;
            // 
            // scrlMapWarpY
            // 
            scrlMapWarpY.Location = new Point(134, 156);
            scrlMapWarpY.Name = "scrlMapWarpY";
            scrlMapWarpY.Size = new Size(349, 18);
            scrlMapWarpY.TabIndex = 5;
            scrlMapWarpY.ValueChanged += ScrlMapWarpY_Scroll;
            // 
            // scrlMapWarpX
            // 
            scrlMapWarpX.Location = new Point(134, 100);
            scrlMapWarpX.Name = "scrlMapWarpX";
            scrlMapWarpX.Size = new Size(349, 18);
            scrlMapWarpX.TabIndex = 4;
            scrlMapWarpX.ValueChanged += ScrlMapWarpX_Scroll;
            // 
            // scrlMapWarpMap
            // 
            scrlMapWarpMap.Location = new Point(134, 49);
            scrlMapWarpMap.Name = "scrlMapWarpMap";
            scrlMapWarpMap.Size = new Size(349, 18);
            scrlMapWarpMap.TabIndex = 3;
            scrlMapWarpMap.ValueChanged += ScrlMapWarpMap_Scroll;
            // 
            // lblMapWarpY
            // 
            lblMapWarpY.AutoSize = true;
            lblMapWarpY.ForeColor = Color.FromArgb(220, 220, 220);
            lblMapWarpY.Location = new Point(15, 164);
            lblMapWarpY.Margin = new Padding(7, 0, 7, 0);
            lblMapWarpY.Name = "lblMapWarpY";
            lblMapWarpY.Size = new Size(52, 32);
            lblMapWarpY.TabIndex = 2;
            lblMapWarpY.Text = "Y: 1";
            // 
            // lblMapWarpX
            // 
            lblMapWarpX.AutoSize = true;
            lblMapWarpX.ForeColor = Color.FromArgb(220, 220, 220);
            lblMapWarpX.Location = new Point(15, 113);
            lblMapWarpX.Margin = new Padding(7, 0, 7, 0);
            lblMapWarpX.Name = "lblMapWarpX";
            lblMapWarpX.Size = new Size(53, 32);
            lblMapWarpX.TabIndex = 1;
            lblMapWarpX.Text = "X: 1";
            // 
            // lblMapWarpMap
            // 
            lblMapWarpMap.AutoSize = true;
            lblMapWarpMap.ForeColor = Color.FromArgb(220, 220, 220);
            lblMapWarpMap.Location = new Point(13, 62);
            lblMapWarpMap.Margin = new Padding(7, 0, 7, 0);
            lblMapWarpMap.Name = "lblMapWarpMap";
            lblMapWarpMap.Size = new Size(87, 32);
            lblMapWarpMap.TabIndex = 0;
            lblMapWarpMap.Text = "Map: 1";
            // 
            // fraNpcSpawn
            // 
            fraNpcSpawn.BorderColor = Color.FromArgb(51, 51, 51);
            fraNpcSpawn.Controls.Add(lstNpc);
            fraNpcSpawn.Controls.Add(btnNpcSpawn);
            fraNpcSpawn.Controls.Add(scrlNpcDir);
            fraNpcSpawn.Controls.Add(lblNpcDir);
            fraNpcSpawn.Location = new Point(7, 15);
            fraNpcSpawn.Margin = new Padding(7, 6, 7, 6);
            fraNpcSpawn.Name = "fraNpcSpawn";
            fraNpcSpawn.Padding = new Padding(7, 6, 7, 6);
            fraNpcSpawn.Size = new Size(377, 277);
            fraNpcSpawn.TabIndex = 11;
            fraNpcSpawn.TabStop = false;
            fraNpcSpawn.Text = "Npc Spawn";
            // 
            // lstNpc
            // 
            lstNpc.DrawMode = DrawMode.OwnerDrawVariable;
            lstNpc.FormattingEnabled = true;
            lstNpc.Location = new Point(13, 38);
            lstNpc.Margin = new Padding(7, 6, 7, 6);
            lstNpc.Name = "lstNpc";
            lstNpc.Size = new Size(331, 40);
            lstNpc.TabIndex = 37;
            // 
            // btnNpcSpawn
            // 
            btnNpcSpawn.Location = new Point(85, 188);
            btnNpcSpawn.Margin = new Padding(7, 6, 7, 6);
            btnNpcSpawn.Name = "btnNpcSpawn";
            btnNpcSpawn.Padding = new Padding(9, 11, 9, 11);
            btnNpcSpawn.Size = new Size(195, 68);
            btnNpcSpawn.TabIndex = 6;
            btnNpcSpawn.Text = "Accept";
            btnNpcSpawn.Click += BtnNpcSpawn_Click;
            // 
            // scrlNpcDir
            // 
            scrlNpcDir.LargeChange = 1;
            scrlNpcDir.Location = new Point(17, 134);
            scrlNpcDir.Maximum = 3;
            scrlNpcDir.Name = "scrlNpcDir";
            scrlNpcDir.Size = new Size(331, 18);
            scrlNpcDir.TabIndex = 3;
            scrlNpcDir.ValueChanged += ScrlNpcDir_Scroll;
            // 
            // lblNpcDir
            // 
            lblNpcDir.AutoSize = true;
            lblNpcDir.ForeColor = Color.FromArgb(220, 220, 220);
            lblNpcDir.Location = new Point(11, 98);
            lblNpcDir.Margin = new Padding(7, 0, 7, 0);
            lblNpcDir.Name = "lblNpcDir";
            lblNpcDir.Size = new Size(153, 32);
            lblNpcDir.TabIndex = 0;
            lblNpcDir.Text = "Direction: Up";
            // 
            // fraHeal
            // 
            fraHeal.BorderColor = Color.FromArgb(51, 51, 51);
            fraHeal.Controls.Add(scrlHeal);
            fraHeal.Controls.Add(lblHeal);
            fraHeal.Controls.Add(cmbHeal);
            fraHeal.Controls.Add(btnHeal);
            fraHeal.Location = new Point(7, 619);
            fraHeal.Margin = new Padding(7, 6, 7, 6);
            fraHeal.Name = "fraHeal";
            fraHeal.Padding = new Padding(7, 6, 7, 6);
            fraHeal.Size = new Size(377, 277);
            fraHeal.TabIndex = 15;
            fraHeal.TabStop = false;
            fraHeal.Text = "Heal";
            // 
            // scrlHeal
            // 
            scrlHeal.Location = new Point(9, 139);
            scrlHeal.Name = "scrlHeal";
            scrlHeal.Size = new Size(336, 17);
            scrlHeal.TabIndex = 39;
            scrlHeal.ValueChanged += ScrlHeal_Scroll;
            // 
            // lblHeal
            // 
            lblHeal.AutoSize = true;
            lblHeal.ForeColor = Color.FromArgb(220, 220, 220);
            lblHeal.Location = new Point(7, 107);
            lblHeal.Margin = new Padding(7, 0, 7, 0);
            lblHeal.Name = "lblHeal";
            lblHeal.Size = new Size(125, 32);
            lblHeal.TabIndex = 38;
            lblHeal.Text = "Amount: 0";
            // 
            // cmbHeal
            // 
            cmbHeal.DrawMode = DrawMode.OwnerDrawVariable;
            cmbHeal.FormattingEnabled = true;
            cmbHeal.Items.AddRange(new object[] { "Heal HP", "Heal MP" });
            cmbHeal.Location = new Point(13, 47);
            cmbHeal.Margin = new Padding(7, 6, 7, 6);
            cmbHeal.Name = "cmbHeal";
            cmbHeal.Size = new Size(331, 40);
            cmbHeal.TabIndex = 37;
            // 
            // btnHeal
            // 
            btnHeal.Location = new Point(80, 188);
            btnHeal.Margin = new Padding(7, 6, 7, 6);
            btnHeal.Name = "btnHeal";
            btnHeal.Padding = new Padding(9, 11, 9, 11);
            btnHeal.Size = new Size(195, 68);
            btnHeal.TabIndex = 6;
            btnHeal.Text = "Accept";
            btnHeal.Click += BtnHeal_Click;
            // 
            // fraShop
            // 
            fraShop.BorderColor = Color.FromArgb(51, 51, 51);
            fraShop.Controls.Add(cmbShop);
            fraShop.Controls.Add(btnShop);
            fraShop.Location = new Point(732, 19);
            fraShop.Margin = new Padding(7, 6, 7, 6);
            fraShop.Name = "fraShop";
            fraShop.Padding = new Padding(7, 6, 7, 6);
            fraShop.Size = new Size(319, 294);
            fraShop.TabIndex = 12;
            fraShop.TabStop = false;
            fraShop.Text = "Shop";
            // 
            // cmbShop
            // 
            cmbShop.DrawMode = DrawMode.OwnerDrawVariable;
            cmbShop.FormattingEnabled = true;
            cmbShop.Location = new Point(13, 47);
            cmbShop.Margin = new Padding(7, 6, 7, 6);
            cmbShop.Name = "cmbShop";
            cmbShop.Size = new Size(283, 40);
            cmbShop.TabIndex = 37;
            // 
            // btnShop
            // 
            btnShop.Location = new Point(63, 209);
            btnShop.Margin = new Padding(7, 6, 7, 6);
            btnShop.Name = "btnShop";
            btnShop.Padding = new Padding(9, 11, 9, 11);
            btnShop.Size = new Size(195, 68);
            btnShop.TabIndex = 6;
            btnShop.Text = "Accept";
            btnShop.Click += BtnShop_Click;
            // 
            // fraResource
            // 
            fraResource.BorderColor = Color.FromArgb(51, 51, 51);
            fraResource.Controls.Add(btnResourceOk);
            fraResource.Controls.Add(scrlResource);
            fraResource.Controls.Add(lblResource);
            fraResource.Location = new Point(397, 15);
            fraResource.Margin = new Padding(7, 6, 7, 6);
            fraResource.Name = "fraResource";
            fraResource.Padding = new Padding(7, 6, 7, 6);
            fraResource.Size = new Size(319, 277);
            fraResource.TabIndex = 10;
            fraResource.TabStop = false;
            fraResource.Text = "Resource";
            // 
            // btnResourceOk
            // 
            btnResourceOk.Location = new Point(61, 188);
            btnResourceOk.Margin = new Padding(7, 6, 7, 6);
            btnResourceOk.Name = "btnResourceOk";
            btnResourceOk.Padding = new Padding(9, 11, 9, 11);
            btnResourceOk.Size = new Size(195, 68);
            btnResourceOk.TabIndex = 6;
            btnResourceOk.Text = "Accept";
            btnResourceOk.Click += BtnResourceOk_Click;
            // 
            // scrlResource
            // 
            scrlResource.Location = new Point(7, 90);
            scrlResource.Name = "scrlResource";
            scrlResource.Size = new Size(295, 18);
            scrlResource.TabIndex = 3;
            scrlResource.ValueChanged += ScrlResource_ValueChanged;
            // 
            // lblResource
            // 
            lblResource.AutoSize = true;
            lblResource.ForeColor = Color.FromArgb(220, 220, 220);
            lblResource.Location = new Point(0, 38);
            lblResource.Margin = new Padding(7, 0, 7, 0);
            lblResource.Name = "lblResource";
            lblResource.Size = new Size(89, 32);
            lblResource.TabIndex = 0;
            lblResource.Text = "Object:";
            // 
            // fraMapItem
            // 
            fraMapItem.BorderColor = Color.FromArgb(51, 51, 51);
            fraMapItem.Controls.Add(picMapItem);
            fraMapItem.Controls.Add(btnMapItem);
            fraMapItem.Controls.Add(scrlMapItemValue);
            fraMapItem.Controls.Add(scrlMapItem);
            fraMapItem.Controls.Add(lblMapItem);
            fraMapItem.Location = new Point(7, 292);
            fraMapItem.Margin = new Padding(7, 6, 7, 6);
            fraMapItem.Name = "fraMapItem";
            fraMapItem.Padding = new Padding(7, 6, 7, 6);
            fraMapItem.Size = new Size(377, 292);
            fraMapItem.TabIndex = 7;
            fraMapItem.TabStop = false;
            fraMapItem.Text = "Map Item";
            // 
            // picMapItem
            // 
            picMapItem.BackColor = Color.Black;
            picMapItem.Location = new Point(288, 90);
            picMapItem.Margin = new Padding(7, 6, 7, 6);
            picMapItem.Name = "picMapItem";
            picMapItem.Size = new Size(69, 79);
            picMapItem.TabIndex = 7;
            picMapItem.TabStop = false;
            // 
            // btnMapItem
            // 
            btnMapItem.Location = new Point(85, 207);
            btnMapItem.Margin = new Padding(7, 6, 7, 6);
            btnMapItem.Name = "btnMapItem";
            btnMapItem.Padding = new Padding(9, 11, 9, 11);
            btnMapItem.Size = new Size(195, 68);
            btnMapItem.TabIndex = 6;
            btnMapItem.Text = "Accept";
            btnMapItem.Click += BtnMapItem_Click;
            // 
            // scrlMapItemValue
            // 
            scrlMapItemValue.Location = new Point(19, 145);
            scrlMapItemValue.Name = "scrlMapItemValue";
            scrlMapItemValue.Size = new Size(260, 18);
            scrlMapItemValue.TabIndex = 4;
            scrlMapItemValue.ValueChanged += ScrlMapItemValue_ValueChanged;
            // 
            // scrlMapItem
            // 
            scrlMapItem.Location = new Point(19, 92);
            scrlMapItem.Name = "scrlMapItem";
            scrlMapItem.Size = new Size(260, 18);
            scrlMapItem.TabIndex = 3;
            scrlMapItem.ValueChanged += ScrlMapItem_ValueChanged;
            // 
            // lblMapItem
            // 
            lblMapItem.AutoSize = true;
            lblMapItem.ForeColor = Color.FromArgb(220, 220, 220);
            lblMapItem.Location = new Point(13, 53);
            lblMapItem.Margin = new Padding(7, 0, 7, 0);
            lblMapItem.Name = "lblMapItem";
            lblMapItem.Size = new Size(104, 32);
            lblMapItem.TabIndex = 0;
            lblMapItem.Text = "None x0";
            // 
            // fraTrap
            // 
            fraTrap.BorderColor = Color.FromArgb(51, 51, 51);
            fraTrap.Controls.Add(btnTrap);
            fraTrap.Controls.Add(scrlTrap);
            fraTrap.Controls.Add(lblTrap);
            fraTrap.Location = new Point(397, 307);
            fraTrap.Margin = new Padding(7, 6, 7, 6);
            fraTrap.Name = "fraTrap";
            fraTrap.Padding = new Padding(7, 6, 7, 6);
            fraTrap.Size = new Size(319, 294);
            fraTrap.TabIndex = 16;
            fraTrap.TabStop = false;
            fraTrap.Text = "Trap";
            // 
            // btnTrap
            // 
            btnTrap.Location = new Point(61, 209);
            btnTrap.Margin = new Padding(7, 6, 7, 6);
            btnTrap.Name = "btnTrap";
            btnTrap.Padding = new Padding(9, 11, 9, 11);
            btnTrap.Size = new Size(195, 68);
            btnTrap.TabIndex = 42;
            btnTrap.Text = "Accept";
            btnTrap.Click += BtnTrap_Click;
            // 
            // scrlTrap
            // 
            scrlTrap.Location = new Point(24, 81);
            scrlTrap.Name = "scrlTrap";
            scrlTrap.Size = new Size(277, 17);
            scrlTrap.TabIndex = 41;
            scrlTrap.ValueChanged += ScrlTrap_ValueChanged;
            // 
            // lblTrap
            // 
            lblTrap.AutoSize = true;
            lblTrap.ForeColor = Color.FromArgb(220, 220, 220);
            lblTrap.Location = new Point(13, 38);
            lblTrap.Margin = new Padding(7, 0, 7, 0);
            lblTrap.Name = "lblTrap";
            lblTrap.Size = new Size(125, 32);
            lblTrap.TabIndex = 40;
            lblTrap.Text = "Amount: 0";
            // 
            // ToolStrip
            // 
            ToolStrip.AutoSize = false;
            ToolStrip.BackColor = Color.FromArgb(45, 45, 48);
            ToolStrip.ImageScalingSize = new Size(24, 24);
            ToolStrip.Items.AddRange(new ToolStripItem[] { tsbSave, tsbDiscard, ToolStripSeparator1, tsbMapGrid, tsbOpacity, ToolStripSeparator2, tsbFill, tsbClear, tsbEyeDropper, tsbCopyMap, tsbDeleteMap, tsbUndo, tsbRedo, tsbScreenshot, tsbTileset });
            ToolStrip.Location = new Point(0, 0);
            ToolStrip.Name = "ToolStrip";
            ToolStrip.Padding = new Padding(0, 0, 4, 0);
            ToolStrip.Size = new Size(2119, 60);
            ToolStrip.TabIndex = 13;
            ToolStrip.Text = "ToolStrip1";
            ToolStrip.MouseHover += ToolStrip_MouseHover;
            // 
            // tsbSave
            // 
            tsbSave.Image = (Image)resources.GetObject("tsbSave.Image");
            tsbSave.ImageTransparentColor = Color.Magenta;
            tsbSave.Name = "tsbSave";
            tsbSave.Size = new Size(46, 54);
            tsbSave.ToolTipText = "Save";
            tsbSave.Click += TsbSave_Click;
            // 
            // tsbDiscard
            // 
            tsbDiscard.Image = (Image)resources.GetObject("tsbDiscard.Image");
            tsbDiscard.ImageTransparentColor = Color.Magenta;
            tsbDiscard.Name = "tsbDiscard";
            tsbDiscard.Size = new Size(46, 54);
            tsbDiscard.ToolTipText = "Discard";
            tsbDiscard.Click += TsbDiscard_Click;
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(6, 60);
            // 
            // tsbMapGrid
            // 
            tsbMapGrid.Image = (Image)resources.GetObject("tsbMapGrid.Image");
            tsbMapGrid.ImageTransparentColor = Color.Magenta;
            tsbMapGrid.Name = "tsbMapGrid";
            tsbMapGrid.Size = new Size(46, 54);
            tsbMapGrid.Tag = "Map Grid";
            tsbMapGrid.Click += TsbMapGrid_Click;
            // 
            // tsbOpacity
            // 
            tsbOpacity.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbOpacity.Image = (Image)resources.GetObject("tsbOpacity.Image");
            tsbOpacity.ImageTransparentColor = Color.Magenta;
            tsbOpacity.Name = "tsbOpacity";
            tsbOpacity.Size = new Size(46, 54);
            tsbOpacity.Text = "ToolStripButton1";
            tsbOpacity.ToolTipText = "Opacity";
            tsbOpacity.Click += tsbOpacity_Click;
            // 
            // ToolStripSeparator2
            // 
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            ToolStripSeparator2.Size = new Size(6, 60);
            // 
            // tsbFill
            // 
            tsbFill.Image = (Image)resources.GetObject("tsbFill.Image");
            tsbFill.ImageTransparentColor = Color.Magenta;
            tsbFill.Name = "tsbFill";
            tsbFill.Size = new Size(46, 54);
            tsbFill.Tag = "Fill";
            tsbFill.ToolTipText = "Fill Layer";
            tsbFill.Click += TsbFill_Click;
            // 
            // tsbClear
            // 
            tsbClear.Image = (Image)resources.GetObject("tsbClear.Image");
            tsbClear.ImageTransparentColor = Color.Magenta;
            tsbClear.Name = "tsbClear";
            tsbClear.Size = new Size(46, 54);
            tsbClear.ToolTipText = "Erase";
            tsbClear.Click += TsbClear_Click;
            // 
            // tsbEyeDropper
            // 
            tsbEyeDropper.Image = (Image)resources.GetObject("tsbEyeDropper.Image");
            tsbEyeDropper.ImageTransparentColor = Color.Magenta;
            tsbEyeDropper.Name = "tsbEyeDropper";
            tsbEyeDropper.Size = new Size(46, 54);
            tsbEyeDropper.ToolTipText = "Eye Dropper";
            tsbEyeDropper.Click += TsbEyeDropper_Click;
            // 
            // tsbCopyMap
            // 
            tsbCopyMap.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbCopyMap.Image = (Image)resources.GetObject("tsbCopyMap.Image");
            tsbCopyMap.ImageTransparentColor = Color.Magenta;
            tsbCopyMap.Name = "tsbCopyMap";
            tsbCopyMap.Size = new Size(46, 54);
            tsbCopyMap.ToolTipText = "Copy";
            tsbCopyMap.Click += tsbCopyMap_Click;
            // 
            // tsbDeleteMap
            // 
            tsbDeleteMap.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbDeleteMap.Image = (Image)resources.GetObject("tsbDeleteMap.Image");
            tsbDeleteMap.ImageTransparentColor = Color.Magenta;
            tsbDeleteMap.Name = "tsbDeleteMap";
            tsbDeleteMap.Size = new Size(46, 54);
            tsbDeleteMap.ToolTipText = "Clear Map";
            tsbDeleteMap.Click += tsbDeleteMap_Click;
            // 
            // tsbUndo
            // 
            tsbUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbUndo.Image = (Image)resources.GetObject("tsbUndo.Image");
            tsbUndo.ImageTransparentColor = Color.Magenta;
            tsbUndo.Name = "tsbUndo";
            tsbUndo.Size = new Size(46, 54);
            tsbUndo.ToolTipText = "Undo";
            tsbUndo.Click += tsbUndo_Click;
            // 
            // tsbRedo
            // 
            tsbRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRedo.Image = (Image)resources.GetObject("tsbRedo.Image");
            tsbRedo.ImageTransparentColor = Color.Magenta;
            tsbRedo.Name = "tsbRedo";
            tsbRedo.Size = new Size(46, 54);
            tsbRedo.ToolTipText = "Redo";
            tsbRedo.Click += tsbRedo_Click;
            // 
            // tsbScreenshot
            // 
            tsbScreenshot.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbScreenshot.Image = (Image)resources.GetObject("tsbScreenshot.Image");
            tsbScreenshot.ImageTransparentColor = Color.Magenta;
            tsbScreenshot.Name = "tsbScreenshot";
            tsbScreenshot.Size = new Size(46, 54);
            tsbScreenshot.ToolTipText = "Screenshot";
            tsbScreenshot.Click += tsbScreenshot_Click;
            // 
            // tsbTileset
            // 
            tsbTileset.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbTileset.Image = (Image)resources.GetObject("tsbTileset.Image");
            tsbTileset.ImageTransparentColor = Color.Magenta;
            tsbTileset.Name = "tsbTileset";
            tsbTileset.Size = new Size(46, 54);
            tsbTileset.Text = "Tileset";
            tsbTileset.Click += tsbTileset_Click;
            // 
            // tabpages
            // 
            tabpages.Controls.Add(tpTiles);
            tabpages.Controls.Add(tpAttributes);
            tabpages.Controls.Add(tpNpcs);
            tabpages.Controls.Add(tpSettings);
            tabpages.Controls.Add(tpDirBlock);
            tabpages.Controls.Add(tpEvents);
            tabpages.Controls.Add(tpEffects);
            tabpages.Location = new Point(9, 68);
            tabpages.Margin = new Padding(7, 6, 7, 6);
            tabpages.Name = "tabpages";
            tabpages.SelectedIndex = 0;
            tabpages.Size = new Size(1021, 1342);
            tabpages.TabIndex = 14;
            tabpages.SelectedIndexChanged += tabpages_SelectedIndexChanged;
            // 
            // tpTiles
            // 
            tpTiles.BackColor = Color.FromArgb(45, 45, 48);
            tpTiles.Controls.Add(cmbAutoTile);
            tpTiles.Controls.Add(Label11);
            tpTiles.Controls.Add(Label10);
            tpTiles.Controls.Add(cmbLayers);
            tpTiles.Controls.Add(Label9);
            tpTiles.Controls.Add(cmbTileSets);
            tpTiles.Controls.Add(pnlBack);
            tpTiles.Location = new Point(8, 46);
            tpTiles.Margin = new Padding(7, 6, 7, 6);
            tpTiles.Name = "tpTiles";
            tpTiles.Padding = new Padding(7, 6, 7, 6);
            tpTiles.Size = new Size(1005, 1288);
            tpTiles.TabIndex = 0;
            tpTiles.Text = "Tiles";
            // 
            // cmbAutoTile
            // 
            cmbAutoTile.DrawMode = DrawMode.OwnerDrawVariable;
            cmbAutoTile.FormattingEnabled = true;
            cmbAutoTile.Items.AddRange(new object[] { "Normal", "AutoTile (VX)", "Fake (VX)", "Animated (VX)", "Cliff (VX)", "Waterfall (VX)" });
            cmbAutoTile.Location = new Point(739, 1167);
            cmbAutoTile.Margin = new Padding(7, 6, 7, 6);
            cmbAutoTile.Name = "cmbAutoTile";
            cmbAutoTile.Size = new Size(201, 40);
            cmbAutoTile.TabIndex = 17;
            cmbAutoTile.SelectedIndexChanged += CmbAutoTile_SelectedIndexChanged;
            // 
            // Label11
            // 
            Label11.AutoSize = true;
            Label11.ForeColor = Color.FromArgb(220, 220, 220);
            Label11.Location = new Point(620, 1175);
            Label11.Margin = new Padding(7, 0, 7, 0);
            Label11.Name = "Label11";
            Label11.Size = new Size(103, 32);
            Label11.TabIndex = 16;
            Label11.Text = "Autotile:";
            // 
            // Label10
            // 
            Label10.AutoSize = true;
            Label10.ForeColor = Color.FromArgb(220, 220, 220);
            Label10.Location = new Point(277, 1175);
            Label10.Margin = new Padding(7, 0, 7, 0);
            Label10.Name = "Label10";
            Label10.Size = new Size(75, 32);
            Label10.TabIndex = 15;
            Label10.Text = "Layer:";
            // 
            // cmbLayers
            // 
            cmbLayers.DrawMode = DrawMode.OwnerDrawVariable;
            cmbLayers.FormattingEnabled = true;
            cmbLayers.Items.AddRange(new object[] { "Ground", "Mask", "Mask Anim", "Cover", "Cover Anim", "Fringe", "Fringe Anim", "Roof", "Roof Anim" });
            cmbLayers.Location = new Point(368, 1167);
            cmbLayers.Margin = new Padding(7, 6, 7, 6);
            cmbLayers.Name = "cmbLayers";
            cmbLayers.Size = new Size(205, 40);
            cmbLayers.TabIndex = 14;
            cmbLayers.SelectedIndexChanged += cmbLayers_SelectedIndexChanged;
            // 
            // Label9
            // 
            Label9.AutoSize = true;
            Label9.ForeColor = Color.FromArgb(220, 220, 220);
            Label9.Location = new Point(17, 1175);
            Label9.Margin = new Padding(7, 0, 7, 0);
            Label9.Name = "Label9";
            Label9.Size = new Size(88, 32);
            Label9.TabIndex = 13;
            Label9.Text = "Tileset:";
            // 
            // cmbTileSets
            // 
            cmbTileSets.DrawMode = DrawMode.OwnerDrawVariable;
            cmbTileSets.FormattingEnabled = true;
            cmbTileSets.Location = new Point(119, 1167);
            cmbTileSets.Margin = new Padding(7, 6, 7, 6);
            cmbTileSets.Name = "cmbTileSets";
            cmbTileSets.Size = new Size(123, 40);
            cmbTileSets.TabIndex = 12;
            cmbTileSets.SelectedIndexChanged += cmbTileSets_SelectedIndexChanged;
            cmbTileSets.Click += CmbTileSets_Click;
            // 
            // tpAttributes
            // 
            tpAttributes.BackColor = Color.FromArgb(45, 45, 48);
            tpAttributes.Controls.Add(optNoCrossing);
            tpAttributes.Controls.Add(btnFillAttributes);
            tpAttributes.Controls.Add(optInfo);
            tpAttributes.Controls.Add(Label23);
            tpAttributes.Controls.Add(cmbAttribute);
            tpAttributes.Controls.Add(optAnimation);
            tpAttributes.Controls.Add(btnClearAttribute);
            tpAttributes.Controls.Add(optTrap);
            tpAttributes.Controls.Add(optBlocked);
            tpAttributes.Controls.Add(optHeal);
            tpAttributes.Controls.Add(optWarp);
            tpAttributes.Controls.Add(optBank);
            tpAttributes.Controls.Add(optItem);
            tpAttributes.Controls.Add(optShop);
            tpAttributes.Controls.Add(optNpcAvoid);
            tpAttributes.Controls.Add(optNpcSpawn);
            tpAttributes.Controls.Add(optResource);
            tpAttributes.Location = new Point(8, 46);
            tpAttributes.Margin = new Padding(7, 6, 7, 6);
            tpAttributes.Name = "tpAttributes";
            tpAttributes.Padding = new Padding(7, 6, 7, 6);
            tpAttributes.Size = new Size(1005, 1288);
            tpAttributes.TabIndex = 3;
            tpAttributes.Text = "Attributes";
            // 
            // optNoCrossing
            // 
            optNoCrossing.AutoSize = true;
            optNoCrossing.Location = new Point(22, 203);
            optNoCrossing.Margin = new Padding(7, 6, 7, 6);
            optNoCrossing.Name = "optNoCrossing";
            optNoCrossing.Size = new Size(132, 36);
            optNoCrossing.TabIndex = 25;
            optNoCrossing.Text = "No Xing";
            // 
            // btnFillAttributes
            // 
            btnFillAttributes.Location = new Point(475, 1212);
            btnFillAttributes.Margin = new Padding(7, 6, 7, 6);
            btnFillAttributes.Name = "btnFillAttributes";
            btnFillAttributes.Padding = new Padding(9, 11, 9, 11);
            btnFillAttributes.Size = new Size(253, 62);
            btnFillAttributes.TabIndex = 24;
            btnFillAttributes.Text = "Fill Attributes";
            btnFillAttributes.Click += btnFillAttributes_Click;
            // 
            // optInfo
            // 
            optInfo.AutoSize = true;
            optInfo.Location = new Point(254, 1227);
            optInfo.Margin = new Padding(7, 6, 7, 6);
            optInfo.Name = "optInfo";
            optInfo.Size = new Size(87, 36);
            optInfo.TabIndex = 22;
            optInfo.Text = "Info";
            // 
            // Label23
            // 
            Label23.AutoSize = true;
            Label23.ForeColor = Color.FromArgb(220, 220, 220);
            Label23.Location = new Point(15, 1229);
            Label23.Margin = new Padding(7, 0, 7, 0);
            Label23.Name = "Label23";
            Label23.Size = new Size(70, 32);
            Label23.TabIndex = 21;
            Label23.Text = "Type:";
            // 
            // cmbAttribute
            // 
            cmbAttribute.DrawMode = DrawMode.OwnerDrawVariable;
            cmbAttribute.FormattingEnabled = true;
            cmbAttribute.Items.AddRange(new object[] { "Layer 1", "Layer 2" });
            cmbAttribute.Location = new Point(115, 1225);
            cmbAttribute.Margin = new Padding(7, 6, 7, 6);
            cmbAttribute.Name = "cmbAttribute";
            cmbAttribute.Size = new Size(123, 40);
            cmbAttribute.TabIndex = 20;
            cmbAttribute.SelectedIndexChanged += cmbAttribute_SelectedIndexChanged;
            // 
            // optAnimation
            // 
            optAnimation.AutoSize = true;
            optAnimation.Location = new Point(693, 124);
            optAnimation.Margin = new Padding(7, 6, 7, 6);
            optAnimation.Name = "optAnimation";
            optAnimation.Size = new Size(155, 36);
            optAnimation.TabIndex = 19;
            optAnimation.Text = "Animation";
            optAnimation.CheckedChanged += optAnimation_CheckedChanged;
            // 
            // tpNpcs
            // 
            tpNpcs.BackColor = Color.FromArgb(45, 45, 48);
            tpNpcs.Controls.Add(fraNpcs);
            tpNpcs.Location = new Point(8, 46);
            tpNpcs.Margin = new Padding(7, 6, 7, 6);
            tpNpcs.Name = "tpNpcs";
            tpNpcs.Padding = new Padding(7, 6, 7, 6);
            tpNpcs.Size = new Size(1005, 1288);
            tpNpcs.TabIndex = 1;
            tpNpcs.Text = "Npcs";
            // 
            // fraNpcs
            // 
            fraNpcs.BackColor = Color.FromArgb(45, 45, 48);
            fraNpcs.BorderColor = Color.FromArgb(51, 51, 51);
            fraNpcs.Controls.Add(Label18);
            fraNpcs.Controls.Add(Label17);
            fraNpcs.Controls.Add(cmbNpcList);
            fraNpcs.Controls.Add(lstMapNpc);
            fraNpcs.Controls.Add(ComboBox23);
            fraNpcs.Location = new Point(13, 19);
            fraNpcs.Margin = new Padding(7, 6, 7, 6);
            fraNpcs.Name = "fraNpcs";
            fraNpcs.Padding = new Padding(7, 6, 7, 6);
            fraNpcs.Size = new Size(1038, 1050);
            fraNpcs.TabIndex = 11;
            fraNpcs.TabStop = false;
            fraNpcs.Text = "Npcs";
            // 
            // Label18
            // 
            Label18.AutoSize = true;
            Label18.ForeColor = Color.FromArgb(220, 220, 220);
            Label18.Location = new Point(423, 70);
            Label18.Margin = new Padding(7, 0, 7, 0);
            Label18.Name = "Label18";
            Label18.Size = new Size(153, 32);
            Label18.TabIndex = 72;
            Label18.Text = "2. Select Npc";
            // 
            // Label17
            // 
            Label17.AutoSize = true;
            Label17.ForeColor = Color.FromArgb(220, 220, 220);
            Label17.Location = new Point(13, 70);
            Label17.Margin = new Padding(7, 0, 7, 0);
            Label17.Name = "Label17";
            Label17.Size = new Size(124, 32);
            Label17.TabIndex = 71;
            Label17.Text = "1. Npc LIst";
            // 
            // cmbNpcList
            // 
            cmbNpcList.DrawMode = DrawMode.OwnerDrawVariable;
            cmbNpcList.FormattingEnabled = true;
            cmbNpcList.Location = new Point(423, 111);
            cmbNpcList.Margin = new Padding(7, 6, 7, 6);
            cmbNpcList.Name = "cmbNpcList";
            cmbNpcList.Size = new Size(552, 40);
            cmbNpcList.TabIndex = 70;
            cmbNpcList.SelectedIndexChanged += CmbNpcList_SelectedIndexChanged;
            // 
            // lstMapNpc
            // 
            lstMapNpc.BackColor = Color.FromArgb(45, 45, 48);
            lstMapNpc.ForeColor = SystemColors.Window;
            lstMapNpc.FormattingEnabled = true;
            lstMapNpc.Location = new Point(19, 111);
            lstMapNpc.Margin = new Padding(7, 6, 7, 6);
            lstMapNpc.Name = "lstMapNpc";
            lstMapNpc.Size = new Size(387, 900);
            lstMapNpc.TabIndex = 69;
            // 
            // ComboBox23
            // 
            ComboBox23.DrawMode = DrawMode.OwnerDrawVariable;
            ComboBox23.FormattingEnabled = true;
            ComboBox23.Location = new Point(739, 1154);
            ComboBox23.Margin = new Padding(7, 6, 7, 6);
            ComboBox23.Name = "ComboBox23";
            ComboBox23.Size = new Size(283, 40);
            ComboBox23.TabIndex = 68;
            // 
            // tpSettings
            // 
            tpSettings.BackColor = Color.FromArgb(45, 45, 48);
            tpSettings.Controls.Add(fraMapSettings);
            tpSettings.Controls.Add(fraMapLinks);
            tpSettings.Controls.Add(fraBootSettings);
            tpSettings.Controls.Add(fraMaxSizes);
            tpSettings.Controls.Add(GroupBox2);
            tpSettings.Controls.Add(txtName);
            tpSettings.Controls.Add(Label6);
            tpSettings.Location = new Point(8, 46);
            tpSettings.Margin = new Padding(7, 6, 7, 6);
            tpSettings.Name = "tpSettings";
            tpSettings.Padding = new Padding(7, 6, 7, 6);
            tpSettings.Size = new Size(1005, 1288);
            tpSettings.TabIndex = 2;
            tpSettings.Text = "Settings";
            // 
            // fraMapSettings
            // 
            fraMapSettings.BorderColor = Color.FromArgb(51, 51, 51);
            fraMapSettings.Controls.Add(Label22);
            fraMapSettings.Controls.Add(lstShop);
            fraMapSettings.Controls.Add(Label8);
            fraMapSettings.Controls.Add(lstMoral);
            fraMapSettings.Location = new Point(13, 79);
            fraMapSettings.Margin = new Padding(7, 6, 7, 6);
            fraMapSettings.Name = "fraMapSettings";
            fraMapSettings.Padding = new Padding(7, 6, 7, 6);
            fraMapSettings.Size = new Size(503, 169);
            fraMapSettings.TabIndex = 15;
            fraMapSettings.TabStop = false;
            fraMapSettings.Text = "Settings";
            // 
            // Label22
            // 
            Label22.AutoSize = true;
            Label22.ForeColor = Color.FromArgb(220, 220, 220);
            Label22.Location = new Point(7, 87);
            Label22.Margin = new Padding(7, 0, 7, 0);
            Label22.Name = "Label22";
            Label22.Size = new Size(74, 32);
            Label22.TabIndex = 40;
            Label22.Text = "Shop:";
            // 
            // lstShop
            // 
            lstShop.DrawMode = DrawMode.OwnerDrawVariable;
            lstShop.FormattingEnabled = true;
            lstShop.Location = new Point(97, 85);
            lstShop.Margin = new Padding(7, 6, 7, 6);
            lstShop.Name = "lstShop";
            lstShop.Size = new Size(387, 40);
            lstShop.TabIndex = 39;
            // 
            // Label8
            // 
            Label8.AutoSize = true;
            Label8.ForeColor = Color.FromArgb(220, 220, 220);
            Label8.Location = new Point(7, 36);
            Label8.Margin = new Padding(7, 0, 7, 0);
            Label8.Name = "Label8";
            Label8.Size = new Size(81, 32);
            Label8.TabIndex = 38;
            Label8.Text = "Moral:";
            // 
            // lstMoral
            // 
            lstMoral.DrawMode = DrawMode.OwnerDrawVariable;
            lstMoral.FormattingEnabled = true;
            lstMoral.Location = new Point(97, 30);
            lstMoral.Margin = new Padding(7, 6, 7, 6);
            lstMoral.Name = "lstMoral";
            lstMoral.Size = new Size(387, 40);
            lstMoral.TabIndex = 37;
            // 
            // fraMapLinks
            // 
            fraMapLinks.BorderColor = Color.FromArgb(51, 51, 51);
            fraMapLinks.Controls.Add(txtDown);
            fraMapLinks.Controls.Add(txtLeft);
            fraMapLinks.Controls.Add(lblMap);
            fraMapLinks.Controls.Add(txtRight);
            fraMapLinks.Controls.Add(txtUp);
            fraMapLinks.Location = new Point(13, 260);
            fraMapLinks.Margin = new Padding(7, 6, 7, 6);
            fraMapLinks.Name = "fraMapLinks";
            fraMapLinks.Padding = new Padding(7, 6, 7, 6);
            fraMapLinks.Size = new Size(503, 275);
            fraMapLinks.TabIndex = 14;
            fraMapLinks.TabStop = false;
            fraMapLinks.Text = "Borders";
            // 
            // txtDown
            // 
            txtDown.BackColor = Color.FromArgb(69, 73, 74);
            txtDown.BorderStyle = BorderStyle.FixedSingle;
            txtDown.ForeColor = Color.FromArgb(220, 220, 220);
            txtDown.Location = new Point(195, 211);
            txtDown.Margin = new Padding(7, 6, 7, 6);
            txtDown.Name = "txtDown";
            txtDown.Size = new Size(106, 39);
            txtDown.TabIndex = 6;
            txtDown.Text = "0";
            // 
            // txtLeft
            // 
            txtLeft.BackColor = Color.FromArgb(69, 73, 74);
            txtLeft.BorderStyle = BorderStyle.FixedSingle;
            txtLeft.ForeColor = Color.FromArgb(220, 220, 220);
            txtLeft.Location = new Point(15, 115);
            txtLeft.Margin = new Padding(7, 6, 7, 6);
            txtLeft.Name = "txtLeft";
            txtLeft.Size = new Size(91, 39);
            txtLeft.TabIndex = 5;
            txtLeft.Text = "0";
            // 
            // lblMap
            // 
            lblMap.AutoSize = true;
            lblMap.ForeColor = Color.FromArgb(220, 220, 220);
            lblMap.Location = new Point(193, 124);
            lblMap.Margin = new Padding(7, 0, 7, 0);
            lblMap.Name = "lblMap";
            lblMap.Size = new Size(87, 32);
            lblMap.TabIndex = 4;
            lblMap.Text = "Map: 0";
            // 
            // txtRight
            // 
            txtRight.BackColor = Color.FromArgb(69, 73, 74);
            txtRight.BorderStyle = BorderStyle.FixedSingle;
            txtRight.ForeColor = Color.FromArgb(220, 220, 220);
            txtRight.Location = new Point(383, 115);
            txtRight.Margin = new Padding(7, 6, 7, 6);
            txtRight.Name = "txtRight";
            txtRight.Size = new Size(106, 39);
            txtRight.TabIndex = 3;
            txtRight.Text = "0";
            // 
            // txtUp
            // 
            txtUp.BackColor = Color.FromArgb(69, 73, 74);
            txtUp.BorderStyle = BorderStyle.FixedSingle;
            txtUp.ForeColor = Color.FromArgb(220, 220, 220);
            txtUp.Location = new Point(193, 26);
            txtUp.Margin = new Padding(7, 6, 7, 6);
            txtUp.Name = "txtUp";
            txtUp.Size = new Size(106, 39);
            txtUp.TabIndex = 1;
            txtUp.Text = "0";
            // 
            // fraBootSettings
            // 
            fraBootSettings.BorderColor = Color.FromArgb(51, 51, 51);
            fraBootSettings.Controls.Add(chkIndoors);
            fraBootSettings.Controls.Add(chkNoMapRespawn);
            fraBootSettings.Controls.Add(txtBootMap);
            fraBootSettings.Controls.Add(Label5);
            fraBootSettings.Controls.Add(txtBootY);
            fraBootSettings.Controls.Add(Label3);
            fraBootSettings.Controls.Add(txtBootX);
            fraBootSettings.Controls.Add(Label4);
            fraBootSettings.Location = new Point(13, 550);
            fraBootSettings.Margin = new Padding(7, 6, 7, 6);
            fraBootSettings.Name = "fraBootSettings";
            fraBootSettings.Padding = new Padding(7, 6, 7, 6);
            fraBootSettings.Size = new Size(503, 267);
            fraBootSettings.TabIndex = 13;
            fraBootSettings.TabStop = false;
            fraBootSettings.Text = "Respawn Settings";
            // 
            // chkIndoors
            // 
            chkIndoors.AutoSize = true;
            chkIndoors.Location = new Point(15, 218);
            chkIndoors.Margin = new Padding(7, 6, 7, 6);
            chkIndoors.Name = "chkIndoors";
            chkIndoors.Size = new Size(126, 36);
            chkIndoors.TabIndex = 42;
            chkIndoors.Text = "Indoors";
            chkIndoors.CheckedChanged += chkIndoors_CheckedChanged;
            // 
            // chkNoMapRespawn
            // 
            chkNoMapRespawn.AutoSize = true;
            chkNoMapRespawn.Location = new Point(266, 218);
            chkNoMapRespawn.Margin = new Padding(7, 6, 7, 6);
            chkNoMapRespawn.Name = "chkNoMapRespawn";
            chkNoMapRespawn.Size = new Size(233, 36);
            chkNoMapRespawn.TabIndex = 19;
            chkNoMapRespawn.Text = "No Map Respawn";
            chkNoMapRespawn.CheckedChanged += chkRespawn_CheckedChanged;
            // 
            // txtBootMap
            // 
            txtBootMap.BackColor = Color.FromArgb(69, 73, 74);
            txtBootMap.BorderStyle = BorderStyle.FixedSingle;
            txtBootMap.ForeColor = Color.FromArgb(220, 220, 220);
            txtBootMap.Location = new Point(381, 28);
            txtBootMap.Margin = new Padding(7, 6, 7, 6);
            txtBootMap.Name = "txtBootMap";
            txtBootMap.Size = new Size(106, 39);
            txtBootMap.TabIndex = 5;
            txtBootMap.Text = "0";
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.ForeColor = Color.FromArgb(220, 220, 220);
            Label5.Location = new Point(13, 38);
            Label5.Margin = new Padding(7, 0, 7, 0);
            Label5.Name = "Label5";
            Label5.Size = new Size(67, 32);
            Label5.TabIndex = 4;
            Label5.Text = "Map:";
            // 
            // txtBootY
            // 
            txtBootY.BackColor = Color.FromArgb(69, 73, 74);
            txtBootY.BorderStyle = BorderStyle.FixedSingle;
            txtBootY.ForeColor = Color.FromArgb(220, 220, 220);
            txtBootY.Location = new Point(381, 156);
            txtBootY.Margin = new Padding(7, 6, 7, 6);
            txtBootY.Name = "txtBootY";
            txtBootY.Size = new Size(106, 39);
            txtBootY.TabIndex = 3;
            txtBootY.Text = "0";
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.ForeColor = Color.FromArgb(220, 220, 220);
            Label3.Location = new Point(13, 160);
            Label3.Margin = new Padding(7, 0, 7, 0);
            Label3.Name = "Label3";
            Label3.Size = new Size(32, 32);
            Label3.TabIndex = 2;
            Label3.Text = "Y:";
            // 
            // txtBootX
            // 
            txtBootX.BackColor = Color.FromArgb(69, 73, 74);
            txtBootX.BorderStyle = BorderStyle.FixedSingle;
            txtBootX.ForeColor = Color.FromArgb(220, 220, 220);
            txtBootX.Location = new Point(381, 92);
            txtBootX.Margin = new Padding(7, 6, 7, 6);
            txtBootX.Name = "txtBootX";
            txtBootX.Size = new Size(106, 39);
            txtBootX.TabIndex = 1;
            txtBootX.Text = "0";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.ForeColor = Color.FromArgb(220, 220, 220);
            Label4.Location = new Point(13, 92);
            Label4.Margin = new Padding(7, 0, 7, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(33, 32);
            Label4.TabIndex = 0;
            Label4.Text = "X:";
            // 
            // fraMaxSizes
            // 
            fraMaxSizes.BorderColor = Color.FromArgb(51, 51, 51);
            fraMaxSizes.Controls.Add(txtMaxY);
            fraMaxSizes.Controls.Add(Label2);
            fraMaxSizes.Controls.Add(txtMaxX);
            fraMaxSizes.Controls.Add(Label7);
            fraMaxSizes.Location = new Point(529, 550);
            fraMaxSizes.Margin = new Padding(7, 6, 7, 6);
            fraMaxSizes.Name = "fraMaxSizes";
            fraMaxSizes.Padding = new Padding(7, 6, 7, 6);
            fraMaxSizes.Size = new Size(462, 192);
            fraMaxSizes.TabIndex = 12;
            fraMaxSizes.TabStop = false;
            fraMaxSizes.Text = "Size Settings";
            // 
            // txtMaxY
            // 
            txtMaxY.BackColor = Color.FromArgb(69, 73, 74);
            txtMaxY.BorderStyle = BorderStyle.FixedSingle;
            txtMaxY.ForeColor = Color.FromArgb(220, 220, 220);
            txtMaxY.Location = new Point(269, 102);
            txtMaxY.Margin = new Padding(7, 6, 7, 6);
            txtMaxY.Name = "txtMaxY";
            txtMaxY.Size = new Size(106, 39);
            txtMaxY.TabIndex = 3;
            txtMaxY.Text = "0";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.ForeColor = Color.FromArgb(220, 220, 220);
            Label2.Location = new Point(13, 111);
            Label2.Margin = new Padding(7, 0, 7, 0);
            Label2.Name = "Label2";
            Label2.Size = new Size(84, 32);
            Label2.TabIndex = 2;
            Label2.Text = "Max Y:";
            // 
            // txtMaxX
            // 
            txtMaxX.BackColor = Color.FromArgb(69, 73, 74);
            txtMaxX.BorderStyle = BorderStyle.FixedSingle;
            txtMaxX.ForeColor = Color.FromArgb(220, 220, 220);
            txtMaxX.Location = new Point(269, 38);
            txtMaxX.Margin = new Padding(7, 6, 7, 6);
            txtMaxX.Name = "txtMaxX";
            txtMaxX.Size = new Size(106, 39);
            txtMaxX.TabIndex = 1;
            txtMaxX.Text = "0";
            // 
            // Label7
            // 
            Label7.AutoSize = true;
            Label7.ForeColor = Color.FromArgb(220, 220, 220);
            Label7.Location = new Point(13, 47);
            Label7.Margin = new Padding(7, 0, 7, 0);
            Label7.Name = "Label7";
            Label7.Size = new Size(85, 32);
            Label7.TabIndex = 0;
            Label7.Text = "Max X:";
            // 
            // GroupBox2
            // 
            GroupBox2.BorderColor = Color.FromArgb(51, 51, 51);
            GroupBox2.Controls.Add(btnPreview);
            GroupBox2.Controls.Add(lstMusic);
            GroupBox2.Location = new Point(529, 6);
            GroupBox2.Margin = new Padding(7, 6, 7, 6);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Padding = new Padding(7, 6, 7, 6);
            GroupBox2.Size = new Size(522, 531);
            GroupBox2.TabIndex = 11;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "Music";
            // 
            // btnPreview
            // 
            btnPreview.Image = (Image)resources.GetObject("btnPreview.Image");
            btnPreview.Location = new Point(106, 444);
            btnPreview.Margin = new Padding(7, 6, 7, 6);
            btnPreview.Name = "btnPreview";
            btnPreview.Padding = new Padding(9, 11, 9, 11);
            btnPreview.Size = new Size(301, 70);
            btnPreview.TabIndex = 4;
            btnPreview.Text = "Preview Music";
            btnPreview.Click += BtnPreview_Click;
            // 
            // lstMusic
            // 
            lstMusic.BackColor = Color.FromArgb(45, 45, 48);
            lstMusic.FormattingEnabled = true;
            lstMusic.Location = new Point(13, 47);
            lstMusic.Margin = new Padding(7, 6, 7, 6);
            lstMusic.Name = "lstMusic";
            lstMusic.ScrollAlwaysVisible = true;
            lstMusic.Size = new Size(446, 388);
            lstMusic.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(115, 15);
            txtName.Margin = new Padding(7, 6, 7, 6);
            txtName.Name = "txtName";
            txtName.Size = new Size(398, 39);
            txtName.TabIndex = 10;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.ForeColor = Color.FromArgb(220, 220, 220);
            Label6.Location = new Point(13, 21);
            Label6.Margin = new Padding(7, 0, 7, 0);
            Label6.Name = "Label6";
            Label6.Size = new Size(83, 32);
            Label6.TabIndex = 9;
            Label6.Text = "Name:";
            // 
            // tpDirBlock
            // 
            tpDirBlock.BackColor = Color.FromArgb(45, 45, 48);
            tpDirBlock.Controls.Add(Label12);
            tpDirBlock.Location = new Point(8, 46);
            tpDirBlock.Margin = new Padding(7, 6, 7, 6);
            tpDirBlock.Name = "tpDirBlock";
            tpDirBlock.Padding = new Padding(7, 6, 7, 6);
            tpDirBlock.Size = new Size(1005, 1288);
            tpDirBlock.TabIndex = 4;
            tpDirBlock.Text = "Directional Block";
            // 
            // Label12
            // 
            Label12.AutoSize = true;
            Label12.ForeColor = Color.FromArgb(220, 220, 220);
            Label12.Location = new Point(48, 58);
            Label12.Margin = new Padding(7, 0, 7, 0);
            Label12.Name = "Label12";
            Label12.Size = new Size(543, 32);
            Label12.TabIndex = 0;
            Label12.Text = "Just press the arrows to block that side of the tile.";
            // 
            // tpEvents
            // 
            tpEvents.BackColor = Color.FromArgb(45, 45, 48);
            tpEvents.Controls.Add(lblPasteMode);
            tpEvents.Controls.Add(lblCopyMode);
            tpEvents.Controls.Add(btnPasteEvent);
            tpEvents.Controls.Add(Label16);
            tpEvents.Controls.Add(btnCopyEvent);
            tpEvents.Controls.Add(Label15);
            tpEvents.Controls.Add(Label13);
            tpEvents.Location = new Point(8, 46);
            tpEvents.Margin = new Padding(7, 6, 7, 6);
            tpEvents.Name = "tpEvents";
            tpEvents.Padding = new Padding(7, 6, 7, 6);
            tpEvents.Size = new Size(1005, 1288);
            tpEvents.TabIndex = 5;
            tpEvents.Text = "Events";
            // 
            // lblPasteMode
            // 
            lblPasteMode.AutoSize = true;
            lblPasteMode.ForeColor = Color.FromArgb(220, 220, 220);
            lblPasteMode.Location = new Point(225, 420);
            lblPasteMode.Margin = new Padding(7, 0, 7, 0);
            lblPasteMode.Name = "lblPasteMode";
            lblPasteMode.Size = new Size(173, 32);
            lblPasteMode.TabIndex = 6;
            lblPasteMode.Text = "PasteMode Off";
            // 
            // lblCopyMode
            // 
            lblCopyMode.AutoSize = true;
            lblCopyMode.ForeColor = Color.FromArgb(220, 220, 220);
            lblCopyMode.Location = new Point(225, 275);
            lblCopyMode.Margin = new Padding(7, 0, 7, 0);
            lblCopyMode.Name = "lblCopyMode";
            lblCopyMode.Size = new Size(173, 32);
            lblCopyMode.TabIndex = 5;
            lblCopyMode.Text = "CopyMode Off";
            // 
            // btnPasteEvent
            // 
            btnPasteEvent.Location = new Point(50, 410);
            btnPasteEvent.Margin = new Padding(7, 6, 7, 6);
            btnPasteEvent.Name = "btnPasteEvent";
            btnPasteEvent.Padding = new Padding(9, 11, 9, 11);
            btnPasteEvent.Size = new Size(163, 58);
            btnPasteEvent.TabIndex = 4;
            btnPasteEvent.Text = "Paste Event";
            btnPasteEvent.Click += BtnPasteEvent_Click;
            // 
            // Label16
            // 
            Label16.AutoSize = true;
            Label16.ForeColor = Color.FromArgb(220, 220, 220);
            Label16.Location = new Point(43, 367);
            Label16.Margin = new Padding(7, 0, 7, 0);
            Label16.Name = "Label16";
            Label16.Size = new Size(882, 32);
            Label16.TabIndex = 3;
            Label16.Text = "To paste a copied Event, press the paste button, then click on the map to place it.";
            // 
            // btnCopyEvent
            // 
            btnCopyEvent.Location = new Point(50, 262);
            btnCopyEvent.Margin = new Padding(7, 6, 7, 6);
            btnCopyEvent.Name = "btnCopyEvent";
            btnCopyEvent.Padding = new Padding(9, 11, 9, 11);
            btnCopyEvent.Size = new Size(163, 58);
            btnCopyEvent.TabIndex = 2;
            btnCopyEvent.Text = "Copy Event";
            btnCopyEvent.Click += BtnCopyEvent_Click;
            // 
            // Label15
            // 
            Label15.AutoSize = true;
            Label15.ForeColor = Color.FromArgb(220, 220, 220);
            Label15.Location = new Point(43, 213);
            Label15.Margin = new Padding(7, 0, 7, 0);
            Label15.Name = "Label15";
            Label15.Size = new Size(692, 32);
            Label15.TabIndex = 1;
            Label15.Text = "To copy a existing Event, press the copy button, then the event.";
            // 
            // Label13
            // 
            Label13.AutoSize = true;
            Label13.ForeColor = Color.FromArgb(220, 220, 220);
            Label13.Location = new Point(43, 51);
            Label13.Margin = new Padding(7, 0, 7, 0);
            Label13.Name = "Label13";
            Label13.Size = new Size(539, 32);
            Label13.TabIndex = 0;
            Label13.Text = "Click on the map where you want to add a event.";
            // 
            // tpEffects
            // 
            tpEffects.BackColor = Color.FromArgb(45, 45, 48);
            tpEffects.Controls.Add(GroupBox6);
            tpEffects.Controls.Add(GroupBox5);
            tpEffects.Controls.Add(GroupBox4);
            tpEffects.Controls.Add(GroupBox3);
            tpEffects.Controls.Add(GroupBox1);
            tpEffects.Location = new Point(8, 46);
            tpEffects.Margin = new Padding(7, 6, 7, 6);
            tpEffects.Name = "tpEffects";
            tpEffects.Padding = new Padding(7, 6, 7, 6);
            tpEffects.Size = new Size(1005, 1288);
            tpEffects.TabIndex = 6;
            tpEffects.Text = "Effects";
            // 
            // GroupBox6
            // 
            GroupBox6.BorderColor = Color.FromArgb(51, 51, 51);
            GroupBox6.Controls.Add(scrlMapBrightness);
            GroupBox6.Location = new Point(24, 553);
            GroupBox6.Margin = new Padding(7, 6, 7, 6);
            GroupBox6.Name = "GroupBox6";
            GroupBox6.Padding = new Padding(7, 6, 7, 6);
            GroupBox6.Size = new Size(511, 96);
            GroupBox6.TabIndex = 22;
            GroupBox6.TabStop = false;
            GroupBox6.Text = "Brightness";
            // 
            // scrlMapBrightness
            // 
            scrlMapBrightness.LargeChange = 1;
            scrlMapBrightness.Location = new Point(7, 41);
            scrlMapBrightness.Maximum = 255;
            scrlMapBrightness.Name = "scrlMapBrightness";
            scrlMapBrightness.Size = new Size(487, 17);
            scrlMapBrightness.TabIndex = 10;
            scrlMapBrightness.Scroll += scrMapBrightness_Scroll;
            // 
            // GroupBox5
            // 
            GroupBox5.BorderColor = Color.FromArgb(51, 51, 51);
            GroupBox5.Controls.Add(cmbParallax);
            GroupBox5.Location = new Point(548, 410);
            GroupBox5.Margin = new Padding(7, 6, 7, 6);
            GroupBox5.Name = "GroupBox5";
            GroupBox5.Padding = new Padding(7, 6, 7, 6);
            GroupBox5.Size = new Size(511, 130);
            GroupBox5.TabIndex = 21;
            GroupBox5.TabStop = false;
            GroupBox5.Text = "Parallax";
            // 
            // cmbParallax
            // 
            cmbParallax.DrawMode = DrawMode.OwnerDrawVariable;
            cmbParallax.FormattingEnabled = true;
            cmbParallax.Location = new Point(17, 45);
            cmbParallax.Margin = new Padding(7, 6, 7, 6);
            cmbParallax.Name = "cmbParallax";
            cmbParallax.Size = new Size(427, 40);
            cmbParallax.TabIndex = 0;
            cmbParallax.SelectedIndexChanged += CmbParallax_SelectedIndexChanged;
            // 
            // GroupBox4
            // 
            GroupBox4.BorderColor = Color.FromArgb(51, 51, 51);
            GroupBox4.Controls.Add(cmbPanorama);
            GroupBox4.Location = new Point(13, 410);
            GroupBox4.Margin = new Padding(7, 6, 7, 6);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Padding = new Padding(7, 6, 7, 6);
            GroupBox4.Size = new Size(522, 130);
            GroupBox4.TabIndex = 20;
            GroupBox4.TabStop = false;
            GroupBox4.Text = "Panorama";
            // 
            // cmbPanorama
            // 
            cmbPanorama.DrawMode = DrawMode.OwnerDrawVariable;
            cmbPanorama.FormattingEnabled = true;
            cmbPanorama.Location = new Point(19, 49);
            cmbPanorama.Margin = new Padding(7, 6, 7, 6);
            cmbPanorama.Name = "cmbPanorama";
            cmbPanorama.Size = new Size(483, 40);
            cmbPanorama.TabIndex = 0;
            cmbPanorama.SelectedIndexChanged += CmbPanorama_SelectedIndexChanged;
            // 
            // GroupBox3
            // 
            GroupBox3.BorderColor = Color.FromArgb(51, 51, 51);
            GroupBox3.Controls.Add(chkTint);
            GroupBox3.Controls.Add(lblMapAlpha);
            GroupBox3.Controls.Add(lblMapBlue);
            GroupBox3.Controls.Add(lblMapGreen);
            GroupBox3.Controls.Add(lblMapRed);
            GroupBox3.Controls.Add(scrlMapAlpha);
            GroupBox3.Controls.Add(scrlMapBlue);
            GroupBox3.Controls.Add(scrlMapGreen);
            GroupBox3.Controls.Add(scrlMapRed);
            GroupBox3.Location = new Point(548, 15);
            GroupBox3.Margin = new Padding(7, 6, 7, 6);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Padding = new Padding(7, 6, 7, 6);
            GroupBox3.Size = new Size(511, 380);
            GroupBox3.TabIndex = 19;
            GroupBox3.TabStop = false;
            GroupBox3.Text = "Tint";
            // 
            // chkTint
            // 
            chkTint.AutoSize = true;
            chkTint.Location = new Point(13, 47);
            chkTint.Margin = new Padding(7, 6, 7, 6);
            chkTint.Name = "chkTint";
            chkTint.Size = new Size(117, 36);
            chkTint.TabIndex = 18;
            chkTint.Text = "Enable";
            chkTint.CheckedChanged += ChkUseTint_CheckedChanged;
            // 
            // lblMapAlpha
            // 
            lblMapAlpha.AutoSize = true;
            lblMapAlpha.ForeColor = Color.FromArgb(220, 220, 220);
            lblMapAlpha.Location = new Point(17, 237);
            lblMapAlpha.Margin = new Padding(7, 0, 7, 0);
            lblMapAlpha.Name = "lblMapAlpha";
            lblMapAlpha.Size = new Size(100, 32);
            lblMapAlpha.TabIndex = 17;
            lblMapAlpha.Text = "Alpha: 0";
            // 
            // lblMapBlue
            // 
            lblMapBlue.AutoSize = true;
            lblMapBlue.ForeColor = Color.FromArgb(220, 220, 220);
            lblMapBlue.Location = new Point(17, 190);
            lblMapBlue.Margin = new Padding(7, 0, 7, 0);
            lblMapBlue.Name = "lblMapBlue";
            lblMapBlue.Size = new Size(86, 32);
            lblMapBlue.TabIndex = 16;
            lblMapBlue.Text = "Blue: 0";
            // 
            // lblMapGreen
            // 
            lblMapGreen.AutoSize = true;
            lblMapGreen.ForeColor = Color.FromArgb(220, 220, 220);
            lblMapGreen.Location = new Point(17, 143);
            lblMapGreen.Margin = new Padding(7, 0, 7, 0);
            lblMapGreen.Name = "lblMapGreen";
            lblMapGreen.Size = new Size(103, 32);
            lblMapGreen.TabIndex = 15;
            lblMapGreen.Text = "Green: 0";
            // 
            // lblMapRed
            // 
            lblMapRed.AutoSize = true;
            lblMapRed.ForeColor = Color.FromArgb(220, 220, 220);
            lblMapRed.Location = new Point(13, 96);
            lblMapRed.Margin = new Padding(7, 0, 7, 0);
            lblMapRed.Name = "lblMapRed";
            lblMapRed.Size = new Size(79, 32);
            lblMapRed.TabIndex = 14;
            lblMapRed.Text = "Red: 0";
            // 
            // scrlMapAlpha
            // 
            scrlMapAlpha.LargeChange = 1;
            scrlMapAlpha.Location = new Point(137, 233);
            scrlMapAlpha.Maximum = 255;
            scrlMapAlpha.Name = "scrlMapAlpha";
            scrlMapAlpha.Size = new Size(314, 17);
            scrlMapAlpha.TabIndex = 13;
            scrlMapAlpha.ValueChanged += ScrlMapAlpha_Scroll;
            // 
            // scrlMapBlue
            // 
            scrlMapBlue.LargeChange = 1;
            scrlMapBlue.Location = new Point(137, 188);
            scrlMapBlue.Maximum = 255;
            scrlMapBlue.Name = "scrlMapBlue";
            scrlMapBlue.Size = new Size(314, 17);
            scrlMapBlue.TabIndex = 12;
            scrlMapBlue.ValueChanged += ScrlMapBlue_Scroll;
            // 
            // scrlMapGreen
            // 
            scrlMapGreen.LargeChange = 1;
            scrlMapGreen.Location = new Point(137, 137);
            scrlMapGreen.Maximum = 255;
            scrlMapGreen.Name = "scrlMapGreen";
            scrlMapGreen.Size = new Size(314, 17);
            scrlMapGreen.TabIndex = 11;
            scrlMapGreen.ValueChanged += ScrlMapGreen_Scroll;
            // 
            // scrlMapRed
            // 
            scrlMapRed.LargeChange = 1;
            scrlMapRed.Location = new Point(137, 98);
            scrlMapRed.Maximum = 255;
            scrlMapRed.Name = "scrlMapRed";
            scrlMapRed.Size = new Size(314, 17);
            scrlMapRed.TabIndex = 10;
            scrlMapRed.ValueChanged += ScrlMapRed_Scroll;
            // 
            // GroupBox1
            // 
            GroupBox1.BorderColor = Color.FromArgb(51, 51, 51);
            GroupBox1.Controls.Add(scrlFogOpacity);
            GroupBox1.Controls.Add(lblFogOpacity);
            GroupBox1.Controls.Add(scrlFogSpeed);
            GroupBox1.Controls.Add(lblFogSpeed);
            GroupBox1.Controls.Add(scrlIntensity);
            GroupBox1.Controls.Add(lblIntensity);
            GroupBox1.Controls.Add(scrlFog);
            GroupBox1.Controls.Add(lblFogIndex);
            GroupBox1.Controls.Add(Label14);
            GroupBox1.Controls.Add(cmbWeather);
            GroupBox1.Location = new Point(13, 15);
            GroupBox1.Margin = new Padding(7, 6, 7, 6);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(7, 6, 7, 6);
            GroupBox1.Size = new Size(522, 380);
            GroupBox1.TabIndex = 18;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Weather";
            // 
            // scrlFogOpacity
            // 
            scrlFogOpacity.LargeChange = 1;
            scrlFogOpacity.Location = new Point(195, 305);
            scrlFogOpacity.Maximum = 255;
            scrlFogOpacity.Name = "scrlFogOpacity";
            scrlFogOpacity.Size = new Size(314, 17);
            scrlFogOpacity.TabIndex = 9;
            scrlFogOpacity.ValueChanged += ScrlFogOpacity_Scroll;
            // 
            // lblFogOpacity
            // 
            lblFogOpacity.AutoSize = true;
            lblFogOpacity.ForeColor = Color.FromArgb(220, 220, 220);
            lblFogOpacity.Location = new Point(13, 309);
            lblFogOpacity.Margin = new Padding(7, 0, 7, 0);
            lblFogOpacity.Name = "lblFogOpacity";
            lblFogOpacity.Size = new Size(147, 32);
            lblFogOpacity.TabIndex = 8;
            lblFogOpacity.Text = "Fog Alpha: 0";
            // 
            // scrlFogSpeed
            // 
            scrlFogSpeed.LargeChange = 1;
            scrlFogSpeed.Location = new Point(195, 250);
            scrlFogSpeed.Name = "scrlFogSpeed";
            scrlFogSpeed.Size = new Size(314, 17);
            scrlFogSpeed.TabIndex = 7;
            scrlFogSpeed.ValueChanged += ScrlFogSpeed_Scroll;
            // 
            // lblFogSpeed
            // 
            lblFogSpeed.AutoSize = true;
            lblFogSpeed.ForeColor = Color.FromArgb(220, 220, 220);
            lblFogSpeed.Location = new Point(13, 258);
            lblFogSpeed.Margin = new Padding(7, 0, 7, 0);
            lblFogSpeed.Name = "lblFogSpeed";
            lblFogSpeed.Size = new Size(172, 32);
            lblFogSpeed.TabIndex = 6;
            lblFogSpeed.Text = "FogSpeed: 100";
            // 
            // scrlIntensity
            // 
            scrlIntensity.LargeChange = 1;
            scrlIntensity.Location = new Point(195, 126);
            scrlIntensity.Name = "scrlIntensity";
            scrlIntensity.Size = new Size(314, 17);
            scrlIntensity.TabIndex = 5;
            scrlIntensity.ValueChanged += ScrlIntensity_Scroll;
            // 
            // lblIntensity
            // 
            lblIntensity.AutoSize = true;
            lblIntensity.ForeColor = Color.FromArgb(220, 220, 220);
            lblIntensity.Location = new Point(13, 130);
            lblIntensity.Margin = new Padding(7, 0, 7, 0);
            lblIntensity.Name = "lblIntensity";
            lblIntensity.Size = new Size(156, 32);
            lblIntensity.TabIndex = 4;
            lblIntensity.Text = "Intensity: 100";
            // 
            // scrlFog
            // 
            scrlFog.LargeChange = 1;
            scrlFog.Location = new Point(195, 198);
            scrlFog.Name = "scrlFog";
            scrlFog.Size = new Size(314, 17);
            scrlFog.TabIndex = 3;
            scrlFog.ValueChanged += ScrlFog_Scroll;
            // 
            // lblFogIndex
            // 
            lblFogIndex.AutoSize = true;
            lblFogIndex.ForeColor = Color.FromArgb(220, 220, 220);
            lblFogIndex.Location = new Point(13, 203);
            lblFogIndex.Margin = new Padding(7, 0, 7, 0);
            lblFogIndex.Name = "lblFogIndex";
            lblFogIndex.Size = new Size(79, 32);
            lblFogIndex.TabIndex = 2;
            lblFogIndex.Text = "Fog: 1";
            // 
            // Label14
            // 
            Label14.AutoSize = true;
            Label14.ForeColor = Color.FromArgb(220, 220, 220);
            Label14.Location = new Point(13, 62);
            Label14.Margin = new Padding(7, 0, 7, 0);
            Label14.Name = "Label14";
            Label14.Size = new Size(166, 32);
            Label14.TabIndex = 1;
            Label14.Text = "Weather Type:";
            // 
            // cmbWeather
            // 
            cmbWeather.DrawMode = DrawMode.OwnerDrawVariable;
            cmbWeather.FormattingEnabled = true;
            cmbWeather.Items.AddRange(new object[] { "None", "Rain", "Snow", "Hail", "Sand Storm", "Storm", "Fog" });
            cmbWeather.Location = new Point(195, 53);
            cmbWeather.Margin = new Padding(7, 6, 7, 6);
            cmbWeather.Name = "cmbWeather";
            cmbWeather.Size = new Size(309, 40);
            cmbWeather.TabIndex = 0;
            cmbWeather.SelectedIndexChanged += CmbWeather_SelectedIndexChanged;
            // 
            // frmEditor_Map
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1025, 1291);
            Controls.Add(tabpages);
            Controls.Add(ToolStrip);
            Controls.Add(pnlAttributes);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(7, 6, 7, 6);
            MaximizeBox = false;
            Name = "frmEditor_Map";
            Text = "Map Editor";
            Activated += frmEditor_Map_Activated;
            FormClosing += frmEditor_Map_FormClosing;
            Load += frmEditor_Map_Load;
            Resize += frmEditor_Map_Resize;
            pnlBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBackSelect).EndInit();
            pnlAttributes.ResumeLayout(false);
            fraAnimation.ResumeLayout(false);
            fraMapWarp.ResumeLayout(false);
            fraMapWarp.PerformLayout();
            fraNpcSpawn.ResumeLayout(false);
            fraNpcSpawn.PerformLayout();
            fraHeal.ResumeLayout(false);
            fraHeal.PerformLayout();
            fraShop.ResumeLayout(false);
            fraResource.ResumeLayout(false);
            fraResource.PerformLayout();
            fraMapItem.ResumeLayout(false);
            fraMapItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picMapItem).EndInit();
            fraTrap.ResumeLayout(false);
            fraTrap.PerformLayout();
            ToolStrip.ResumeLayout(false);
            ToolStrip.PerformLayout();
            tabpages.ResumeLayout(false);
            tpTiles.ResumeLayout(false);
            tpTiles.PerformLayout();
            tpAttributes.ResumeLayout(false);
            tpAttributes.PerformLayout();
            tpNpcs.ResumeLayout(false);
            fraNpcs.ResumeLayout(false);
            fraNpcs.PerformLayout();
            tpSettings.ResumeLayout(false);
            tpSettings.PerformLayout();
            fraMapSettings.ResumeLayout(false);
            fraMapSettings.PerformLayout();
            fraMapLinks.ResumeLayout(false);
            fraMapLinks.PerformLayout();
            fraBootSettings.ResumeLayout(false);
            fraBootSettings.PerformLayout();
            fraMaxSizes.ResumeLayout(false);
            fraMaxSizes.PerformLayout();
            GroupBox2.ResumeLayout(false);
            tpDirBlock.ResumeLayout(false);
            tpDirBlock.PerformLayout();
            tpEvents.ResumeLayout(false);
            tpEvents.PerformLayout();
            tpEffects.ResumeLayout(false);
            GroupBox6.ResumeLayout(false);
            GroupBox5.ResumeLayout(false);
            GroupBox4.ResumeLayout(false);
            GroupBox3.ResumeLayout(false);
            GroupBox3.PerformLayout();
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            ResumeLayout(false);
        }

        internal Panel pnlBack;
        internal DarkRadioButton optTrap;
        internal DarkRadioButton optHeal;
        internal DarkRadioButton optBank;
        internal DarkRadioButton optShop;
        internal DarkRadioButton optNpcSpawn;
        internal DarkRadioButton optResource;
        internal DarkRadioButton optNpcAvoid;
        internal DarkRadioButton optItem;
        internal DarkRadioButton optWarp;
        internal DarkRadioButton optBlocked;
        internal DarkButton btnClearAttribute;
        internal Panel pnlAttributes;
        internal DarkGroupBox fraMapWarp;
        internal DarkLabel lblMapWarpY;
        internal DarkLabel lblMapWarpX;
        internal DarkLabel lblMapWarpMap;
        internal HScrollBar scrlMapWarpY;
        internal HScrollBar scrlMapWarpX;
        internal HScrollBar scrlMapWarpMap;
        internal DarkButton btnMapWarp;
        internal DarkGroupBox fraMapItem;
        internal DarkButton btnMapItem;
        internal HScrollBar scrlMapItemValue;
        internal HScrollBar scrlMapItem;
        internal DarkLabel lblMapItem;
        internal PictureBox picMapItem;
        internal DarkGroupBox fraResource;
        internal DarkButton btnResourceOk;
        internal HScrollBar scrlResource;
        internal DarkLabel lblResource;
        internal DarkGroupBox fraNpcSpawn;
        internal DarkButton btnNpcSpawn;
        internal HScrollBar scrlNpcDir;
        internal DarkLabel lblNpcDir;
        internal DarkComboBox lstNpc;
        internal DarkGroupBox fraShop;
        internal DarkComboBox cmbShop;
        internal DarkButton btnShop;
        internal DarkGroupBox fraHeal;
        internal DarkLabel lblHeal;
        internal DarkComboBox cmbHeal;
        internal DarkButton btnHeal;
        internal HScrollBar scrlHeal;
        internal DarkGroupBox fraTrap;
        internal DarkButton btnTrap;
        internal HScrollBar scrlTrap;
        internal DarkLabel lblTrap;
        internal ToolStrip ToolStrip;
        internal ToolStripButton tsbSave;
        internal ToolStripButton tsbDiscard;
        internal TabControl tabpages;
        internal TabPage tpTiles;
        internal TabPage tpNpcs;
        internal TabPage tpSettings;
        internal DarkGroupBox fraNpcs;
        internal DarkComboBox ComboBox23;
        internal DarkTextBox txtName;
        internal DarkLabel Label6;
        internal DarkGroupBox fraMapLinks;
        internal DarkTextBox txtDown;
        internal DarkTextBox txtLeft;
        internal DarkLabel lblMap;
        internal DarkTextBox txtRight;
        internal DarkTextBox txtUp;
        internal DarkGroupBox fraBootSettings;
        internal DarkTextBox txtBootMap;
        internal DarkLabel Label5;
        internal DarkTextBox txtBootY;
        internal DarkLabel Label3;
        internal DarkTextBox txtBootX;
        internal DarkLabel Label4;
        internal DarkGroupBox fraMaxSizes;
        internal DarkTextBox txtMaxY;
        internal DarkLabel Label2;
        internal DarkTextBox txtMaxX;
        internal DarkLabel Label7;
        internal DarkGroupBox GroupBox2;
        internal ListBox lstMusic;
        internal DarkGroupBox fraMapSettings;
        internal DarkLabel Label8;
        internal DarkComboBox lstMoral;
        internal ToolStripSeparator ToolStripSeparator1;
        internal DarkComboBox cmbNpcList;
        internal ListBox lstMapNpc;
        internal TabPage tpAttributes;
        internal DarkComboBox cmbTileSets;
        internal DarkComboBox cmbAutoTile;
        internal DarkLabel Label11;
        internal DarkLabel Label10;
        internal DarkComboBox cmbLayers;
        internal DarkLabel Label9;
        internal TabPage tpDirBlock;
        internal TabPage tpEvents;
        internal DarkLabel Label12;
        internal DarkLabel Label13;
        internal ToolStripButton tsbMapGrid;
        internal DarkButton btnPreview;
        internal ToolStripButton tsbFill;
        internal ToolStripButton tsbEyeDropper;
        internal ToolStripSeparator ToolStripSeparator2;
        internal DarkButton btnPasteEvent;
        internal DarkLabel Label16;
        internal DarkButton btnCopyEvent;
        internal DarkLabel Label15;
        internal DarkLabel lblPasteMode;
        internal DarkLabel lblCopyMode;
        internal TabPage tpEffects;
        internal DarkGroupBox GroupBox3;
        internal DarkCheckBox chkTint;
        internal DarkLabel lblMapAlpha;
        internal DarkLabel lblMapBlue;
        internal DarkLabel lblMapGreen;
        internal DarkLabel lblMapRed;
        internal HScrollBar scrlMapAlpha;
        internal HScrollBar scrlMapBlue;
        internal HScrollBar scrlMapGreen;
        internal HScrollBar scrlMapRed;
        internal DarkGroupBox GroupBox1;
        internal HScrollBar scrlFogOpacity;
        internal DarkLabel lblFogOpacity;
        internal HScrollBar scrlFogSpeed;
        internal DarkLabel lblFogSpeed;
        internal HScrollBar scrlIntensity;
        internal DarkLabel lblIntensity;
        internal HScrollBar scrlFog;
        internal DarkLabel lblFogIndex;
        internal DarkLabel Label14;
        internal DarkComboBox cmbWeather;
        internal DarkLabel Label18;
        internal DarkLabel Label17;
        internal DarkGroupBox GroupBox5;
        internal DarkLabel Label20;
        internal DarkComboBox cmbParallax;
        internal DarkGroupBox GroupBox4;
        internal DarkLabel Label19;
        internal DarkComboBox cmbPanorama;
        internal DarkGroupBox GroupBox6;
        internal DarkLabel lblMapBrightness;
        internal HScrollBar scrlMapBrightness;
        internal PictureBox picBackSelect;
        internal ToolStripButton tsbClear;
        internal ToolStripButton tsbCopyMap;
        internal ToolStripButton tsbUndo;
        internal ToolStripButton tsbRedo;
        internal ToolStripButton tsbOpacity;
        internal ToolStripButton tsbScreenshot;
        internal DarkRadioButton optAnimation;
        internal DarkGroupBox fraAnimation;
        internal DarkComboBox cmbAnimation;
        internal DarkButton brnAnimation;
        internal DarkLabel Label21;
        internal DarkLabel Label22;
        internal DarkComboBox lstShop;
        internal DarkCheckBox chkNoMapRespawn;
        internal DarkCheckBox chkIndoors;
        internal DarkLabel Label23;
        internal DarkComboBox cmbAttribute;
        internal ToolStripButton tsbDeleteMap;
        internal DarkRadioButton optInfo;
        internal DarkButton btnFillAttributes;
        internal DarkRadioButton optNoCrossing;
        private ToolStripButton tsbTileset;
    }
}