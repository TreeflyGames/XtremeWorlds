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
            btnClearAttribute = new Button();
            optTrap = new RadioButton();
            optHeal = new RadioButton();
            optBank = new RadioButton();
            optShop = new RadioButton();
            optNPCSpawn = new RadioButton();
            optResource = new RadioButton();
            optNPCAvoid = new RadioButton();
            optItem = new RadioButton();
            optWarp = new RadioButton();
            optBlocked = new RadioButton();
            pnlBack = new Panel();
            picBackSelect = new PictureBox();
            Label1 = new Label();
            pnlAttributes = new Panel();
            fraMapLight = new GroupBox();
            lblRadius = new Label();
            scrlLight = new HScrollBar();
            chkShadow = new CheckBox();
            chkFlicker = new CheckBox();
            btnLight = new Button();
            fraAnimation = new GroupBox();
            cmbAnimation = new ComboBox();
            brnAnimation = new Button();
            fraMapWarp = new GroupBox();
            btnMapWarp = new Button();
            scrlMapWarpY = new HScrollBar();
            scrlMapWarpX = new HScrollBar();
            scrlMapWarpMap = new HScrollBar();
            lblMapWarpY = new Label();
            lblMapWarpX = new Label();
            lblMapWarpMap = new Label();
            fraNPCSpawn = new GroupBox();
            lstNPC = new ComboBox();
            btnNPCSpawn = new Button();
            scrlNPCDir = new HScrollBar();
            lblNPCDir = new Label();
            fraHeal = new GroupBox();
            scrlHeal = new HScrollBar();
            lblHeal = new Label();
            cmbHeal = new ComboBox();
            btnHeal = new Button();
            fraShop = new GroupBox();
            cmbShop = new ComboBox();
            btnShop = new Button();
            fraResource = new GroupBox();
            btnResourceOk = new Button();
            scrlResource = new HScrollBar();
            lblResource = new Label();
            fraMapItem = new GroupBox();
            picMapItem = new PictureBox();
            btnMapItem = new Button();
            scrlMapItemValue = new HScrollBar();
            scrlMapItem = new HScrollBar();
            lblMapItem = new Label();
            fraTrap = new GroupBox();
            btnTrap = new Button();
            scrlTrap = new HScrollBar();
            lblTrap = new Label();
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
            tabpages = new TabControl();
            tpTiles = new TabPage();
            cmbAutoTile = new ComboBox();
            Label11 = new Label();
            Label10 = new Label();
            cmbLayers = new ComboBox();
            Label9 = new Label();
            cmbTileSets = new ComboBox();
            tpAttributes = new TabPage();
            optNoXing = new RadioButton();
            btnFillAttributes = new Button();
            optInfo = new RadioButton();
            Label23 = new Label();
            cmbAttribute = new ComboBox();
            optAnimation = new RadioButton();
            tpNPCs = new TabPage();
            fraNPCs = new GroupBox();
            Label18 = new Label();
            Label17 = new Label();
            cmbNPCList = new ComboBox();
            lstMapNPC = new ListBox();
            ComboBox23 = new ComboBox();
            tpSettings = new TabPage();
            fraMapSettings = new GroupBox();
            Label22 = new Label();
            lstShop = new ComboBox();
            Label8 = new Label();
            lstMoral = new ComboBox();
            fraMapLinks = new GroupBox();
            txtDown = new TextBox();
            txtLeft = new TextBox();
            lblMap = new Label();
            txtRight = new TextBox();
            txtUp = new TextBox();
            fraBootSettings = new GroupBox();
            chkIndoors = new CheckBox();
            chkNoMapRespawn = new CheckBox();
            txtBootMap = new TextBox();
            Label5 = new Label();
            txtBootY = new TextBox();
            Label3 = new Label();
            txtBootX = new TextBox();
            Label4 = new Label();
            fraMaxSizes = new GroupBox();
            txtMaxY = new TextBox();
            Label2 = new Label();
            txtMaxX = new TextBox();
            Label7 = new Label();
            GroupBox2 = new GroupBox();
            btnPreview = new Button();
            lstMusic = new ListBox();
            txtName = new TextBox();
            Label6 = new Label();
            tpDirBlock = new TabPage();
            Label12 = new Label();
            tpEvents = new TabPage();
            lblPasteMode = new Label();
            lblCopyMode = new Label();
            btnPasteEvent = new Button();
            Label16 = new Label();
            btnCopyEvent = new Button();
            Label15 = new Label();
            Label13 = new Label();
            tpEffects = new TabPage();
            GroupBox6 = new GroupBox();
            scrlMapBrightness = new HScrollBar();
            GroupBox5 = new GroupBox();
            cmbParallax = new ComboBox();
            GroupBox4 = new GroupBox();
            cmbPanorama = new ComboBox();
            GroupBox3 = new GroupBox();
            chkTint = new CheckBox();
            lblMapAlpha = new Label();
            lblMapBlue = new Label();
            lblMapGreen = new Label();
            lblMapRed = new Label();
            scrlMapAlpha = new HScrollBar();
            scrlMapBlue = new HScrollBar();
            scrlMapGreen = new HScrollBar();
            scrlMapRed = new HScrollBar();
            GroupBox1 = new GroupBox();
            scrlFogOpacity = new HScrollBar();
            lblFogOpacity = new Label();
            scrlFogSpeed = new HScrollBar();
            lblFogSpeed = new Label();
            scrlIntensity = new HScrollBar();
            lblIntensity = new Label();
            scrlFog = new HScrollBar();
            lblFogIndex = new Label();
            Label14 = new Label();
            cmbWeather = new ComboBox();
            pnlBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBackSelect).BeginInit();
            pnlAttributes.SuspendLayout();
            fraMapLight.SuspendLayout();
            fraAnimation.SuspendLayout();
            fraMapWarp.SuspendLayout();
            fraNPCSpawn.SuspendLayout();
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
            tpNPCs.SuspendLayout();
            fraNPCs.SuspendLayout();
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
            btnClearAttribute.Location = new Point(400, 569);
            btnClearAttribute.Margin = new Padding(4, 3, 4, 3);
            btnClearAttribute.Name = "btnClearAttribute";
            btnClearAttribute.Size = new Size(136, 29);
            btnClearAttribute.TabIndex = 14;
            btnClearAttribute.Text = "Clear Attributes";
            btnClearAttribute.UseVisualStyleBackColor = true;
            btnClearAttribute.Click += BtnClearAttribute_Click;
            // 
            // optTrap
            // 
            optTrap.AutoSize = true;
            optTrap.Location = new Point(276, 58);
            optTrap.Margin = new Padding(4, 3, 4, 3);
            optTrap.Name = "optTrap";
            optTrap.Size = new Size(47, 19);
            optTrap.TabIndex = 12;
            optTrap.Text = "Trap";
            optTrap.UseVisualStyleBackColor = true;
            optTrap.CheckedChanged += OptTrap_CheckedChanged;
            // 
            // optHeal
            // 
            optHeal.AutoSize = true;
            optHeal.Location = new Point(202, 58);
            optHeal.Margin = new Padding(4, 3, 4, 3);
            optHeal.Name = "optHeal";
            optHeal.Size = new Size(49, 19);
            optHeal.TabIndex = 11;
            optHeal.Text = "Heal";
            optHeal.UseVisualStyleBackColor = true;
            optHeal.CheckedChanged += OptHeal_CheckedChanged;
            // 
            // optBank
            // 
            optBank.AutoSize = true;
            optBank.Location = new Point(118, 58);
            optBank.Margin = new Padding(4, 3, 4, 3);
            optBank.Name = "optBank";
            optBank.Size = new Size(51, 19);
            optBank.TabIndex = 10;
            optBank.Text = "Bank";
            optBank.UseVisualStyleBackColor = true;
            // 
            // optShop
            // 
            optShop.AutoSize = true;
            optShop.Location = new Point(477, 16);
            optShop.Margin = new Padding(4, 3, 4, 3);
            optShop.Name = "optShop";
            optShop.Size = new Size(52, 19);
            optShop.TabIndex = 9;
            optShop.Text = "Shop";
            optShop.UseVisualStyleBackColor = true;
            optShop.CheckedChanged += OptShop_CheckedChanged;
            // 
            // optNPCSpawn
            // 
            optNPCSpawn.AutoSize = true;
            optNPCSpawn.Location = new Point(373, 16);
            optNPCSpawn.Margin = new Padding(4, 3, 4, 3);
            optNPCSpawn.Name = "optNPCSpawn";
            optNPCSpawn.Size = new Size(87, 19);
            optNPCSpawn.TabIndex = 8;
            optNPCSpawn.Text = "NPC Spawn";
            optNPCSpawn.UseVisualStyleBackColor = true;
            optNPCSpawn.CheckedChanged += OptNPCSpawn_CheckedChanged;
            // 
            // optResource
            // 
            optResource.AutoSize = true;
            optResource.Location = new Point(12, 58);
            optResource.Margin = new Padding(4, 3, 4, 3);
            optResource.Name = "optResource";
            optResource.Size = new Size(73, 19);
            optResource.TabIndex = 6;
            optResource.Text = "Resource";
            optResource.UseVisualStyleBackColor = true;
            optResource.CheckedChanged += OptResource_CheckedChanged;
            // 
            // optNPCAvoid
            // 
            optNPCAvoid.AutoSize = true;
            optNPCAvoid.Location = new Point(276, 16);
            optNPCAvoid.Margin = new Padding(4, 3, 4, 3);
            optNPCAvoid.Name = "optNPCAvoid";
            optNPCAvoid.Size = new Size(83, 19);
            optNPCAvoid.TabIndex = 3;
            optNPCAvoid.Text = "NPC Avoid";
            optNPCAvoid.UseVisualStyleBackColor = true;
            // 
            // optItem
            // 
            optItem.AutoSize = true;
            optItem.Location = new Point(202, 16);
            optItem.Margin = new Padding(4, 3, 4, 3);
            optItem.Name = "optItem";
            optItem.Size = new Size(49, 19);
            optItem.TabIndex = 2;
            optItem.Text = "Item";
            optItem.UseVisualStyleBackColor = true;
            optItem.CheckedChanged += OptItem_CheckedChanged;
            // 
            // optWarp
            // 
            optWarp.AutoSize = true;
            optWarp.Location = new Point(118, 16);
            optWarp.Margin = new Padding(4, 3, 4, 3);
            optWarp.Name = "optWarp";
            optWarp.Size = new Size(53, 19);
            optWarp.TabIndex = 1;
            optWarp.Text = "Warp";
            optWarp.UseVisualStyleBackColor = true;
            optWarp.CheckedChanged += OptWarp_CheckedChanged;
            // 
            // optBlocked
            // 
            optBlocked.AutoSize = true;
            optBlocked.Checked = true;
            optBlocked.Location = new Point(12, 16);
            optBlocked.Margin = new Padding(4, 3, 4, 3);
            optBlocked.Name = "optBlocked";
            optBlocked.Size = new Size(67, 19);
            optBlocked.TabIndex = 0;
            optBlocked.TabStop = true;
            optBlocked.Text = "Blocked";
            optBlocked.UseVisualStyleBackColor = true;
            optBlocked.CheckedChanged += OptBlocked_CheckedChanged;
            // 
            // pnlBack
            // 
            pnlBack.Controls.Add(picBackSelect);
            pnlBack.Location = new Point(7, 9);
            pnlBack.Margin = new Padding(4, 3, 4, 3);
            pnlBack.Name = "pnlBack";
            pnlBack.Size = new Size(526, 532);
            pnlBack.TabIndex = 9;
            // 
            // picBackSelect
            // 
            picBackSelect.BackColor = Color.Black;
            picBackSelect.Location = new Point(11, 3);
            picBackSelect.Margin = new Padding(4, 3, 4, 3);
            picBackSelect.Name = "picBackSelect";
            picBackSelect.Size = new Size(512, 512);
            picBackSelect.TabIndex = 22;
            picBackSelect.TabStop = false;
            picBackSelect.Paint += picBackSelect_Paint;
            picBackSelect.MouseDown += PicBackSelect_MouseDown;
            picBackSelect.MouseMove += PicBackSelect_MouseMove;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(8, 544);
            Label1.Margin = new Padding(4, 0, 4, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(192, 15);
            Label1.TabIndex = 11;
            Label1.Text = "Drag Mouse to Select Multiple Tiles";
            // 
            // pnlAttributes
            // 
            pnlAttributes.Controls.Add(fraMapLight);
            pnlAttributes.Controls.Add(fraAnimation);
            pnlAttributes.Controls.Add(fraMapWarp);
            pnlAttributes.Controls.Add(fraNPCSpawn);
            pnlAttributes.Controls.Add(fraHeal);
            pnlAttributes.Controls.Add(fraShop);
            pnlAttributes.Controls.Add(fraResource);
            pnlAttributes.Controls.Add(fraMapItem);
            pnlAttributes.Controls.Add(fraTrap);
            pnlAttributes.Location = new Point(555, 56);
            pnlAttributes.Margin = new Padding(4, 3, 4, 3);
            pnlAttributes.Name = "pnlAttributes";
            pnlAttributes.Size = new Size(586, 567);
            pnlAttributes.TabIndex = 12;
            pnlAttributes.Visible = false;
            // 
            // fraMapLight
            // 
            fraMapLight.Controls.Add(lblRadius);
            fraMapLight.Controls.Add(scrlLight);
            fraMapLight.Controls.Add(chkShadow);
            fraMapLight.Controls.Add(chkFlicker);
            fraMapLight.Controls.Add(btnLight);
            fraMapLight.Location = new Point(394, 147);
            fraMapLight.Margin = new Padding(4, 3, 4, 3);
            fraMapLight.Name = "fraMapLight";
            fraMapLight.Padding = new Padding(4, 3, 4, 3);
            fraMapLight.Size = new Size(172, 138);
            fraMapLight.TabIndex = 19;
            fraMapLight.TabStop = false;
            fraMapLight.Text = "Light";
            fraMapLight.Visible = false;
            // 
            // lblRadius
            // 
            lblRadius.AutoSize = true;
            lblRadius.Location = new Point(8, 16);
            lblRadius.Margin = new Padding(4, 0, 4, 0);
            lblRadius.Name = "lblRadius";
            lblRadius.Size = new Size(54, 15);
            lblRadius.TabIndex = 41;
            lblRadius.Text = "Radius: 0";
            // 
            // scrlLight
            // 
            scrlLight.Location = new Point(7, 31);
            scrlLight.Name = "scrlLight";
            scrlLight.Size = new Size(165, 17);
            scrlLight.TabIndex = 40;
            scrlLight.ValueChanged += scrlLight_ValueChanged;
            // 
            // chkShadow
            // 
            chkShadow.AutoSize = true;
            chkShadow.Location = new Point(7, 73);
            chkShadow.Name = "chkShadow";
            chkShadow.Size = new Size(73, 19);
            chkShadow.TabIndex = 39;
            chkShadow.Text = "Shadows";
            chkShadow.UseVisualStyleBackColor = true;
            // 
            // chkFlicker
            // 
            chkFlicker.AutoSize = true;
            chkFlicker.Location = new Point(7, 51);
            chkFlicker.Name = "chkFlicker";
            chkFlicker.Size = new Size(60, 19);
            chkFlicker.TabIndex = 38;
            chkFlicker.Text = "Flicker";
            chkFlicker.UseVisualStyleBackColor = true;
            // 
            // btnLight
            // 
            btnLight.Location = new Point(0, 0);
            btnLight.Name = "btnLight";
            btnLight.Size = new Size(75, 23);
            btnLight.TabIndex = 42;
            // 
            // fraAnimation
            // 
            fraAnimation.Controls.Add(cmbAnimation);
            fraAnimation.Controls.Add(brnAnimation);
            fraAnimation.Location = new Point(214, 291);
            fraAnimation.Margin = new Padding(4, 3, 4, 3);
            fraAnimation.Name = "fraAnimation";
            fraAnimation.Padding = new Padding(4, 3, 4, 3);
            fraAnimation.Size = new Size(203, 130);
            fraAnimation.TabIndex = 17;
            fraAnimation.TabStop = false;
            fraAnimation.Text = "Animation";
            fraAnimation.Visible = false;
            // 
            // cmbAnimation
            // 
            cmbAnimation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAnimation.FormattingEnabled = true;
            cmbAnimation.Items.AddRange(new object[] { "Heal HP", "Heal MP" });
            cmbAnimation.Location = new Point(7, 22);
            cmbAnimation.Margin = new Padding(4, 3, 4, 3);
            cmbAnimation.Name = "cmbAnimation";
            cmbAnimation.Size = new Size(180, 23);
            cmbAnimation.TabIndex = 37;
            // 
            // brnAnimation
            // 
            brnAnimation.Location = new Point(43, 88);
            brnAnimation.Margin = new Padding(4, 3, 4, 3);
            brnAnimation.Name = "brnAnimation";
            brnAnimation.Size = new Size(105, 32);
            brnAnimation.TabIndex = 6;
            brnAnimation.Text = "Accept";
            brnAnimation.UseVisualStyleBackColor = true;
            brnAnimation.Click += brnAnimation_Click;
            // 
            // fraMapWarp
            // 
            fraMapWarp.Controls.Add(btnMapWarp);
            fraMapWarp.Controls.Add(scrlMapWarpY);
            fraMapWarp.Controls.Add(scrlMapWarpX);
            fraMapWarp.Controls.Add(scrlMapWarpMap);
            fraMapWarp.Controls.Add(lblMapWarpY);
            fraMapWarp.Controls.Add(lblMapWarpX);
            fraMapWarp.Controls.Add(lblMapWarpMap);
            fraMapWarp.Location = new Point(10, 427);
            fraMapWarp.Margin = new Padding(4, 3, 4, 3);
            fraMapWarp.Name = "fraMapWarp";
            fraMapWarp.Padding = new Padding(4, 3, 4, 3);
            fraMapWarp.Size = new Size(294, 137);
            fraMapWarp.TabIndex = 0;
            fraMapWarp.TabStop = false;
            fraMapWarp.Text = "Map Warp";
            // 
            // btnMapWarp
            // 
            btnMapWarp.Location = new Point(93, 102);
            btnMapWarp.Margin = new Padding(4, 3, 4, 3);
            btnMapWarp.Name = "btnMapWarp";
            btnMapWarp.Size = new Size(105, 32);
            btnMapWarp.TabIndex = 6;
            btnMapWarp.Text = "Accept";
            btnMapWarp.UseVisualStyleBackColor = true;
            btnMapWarp.Click += BtnMapWarp_Click;
            // 
            // scrlMapWarpY
            // 
            scrlMapWarpY.Location = new Point(72, 73);
            scrlMapWarpY.Name = "scrlMapWarpY";
            scrlMapWarpY.Size = new Size(188, 18);
            scrlMapWarpY.TabIndex = 5;
            scrlMapWarpY.ValueChanged += ScrlMapWarpY_Scroll;
            // 
            // scrlMapWarpX
            // 
            scrlMapWarpX.Location = new Point(72, 47);
            scrlMapWarpX.Name = "scrlMapWarpX";
            scrlMapWarpX.Size = new Size(188, 18);
            scrlMapWarpX.TabIndex = 4;
            scrlMapWarpX.ValueChanged += ScrlMapWarpX_Scroll;
            // 
            // scrlMapWarpMap
            // 
            scrlMapWarpMap.Location = new Point(72, 23);
            scrlMapWarpMap.Name = "scrlMapWarpMap";
            scrlMapWarpMap.Size = new Size(188, 18);
            scrlMapWarpMap.TabIndex = 3;
            scrlMapWarpMap.ValueChanged += ScrlMapWarpMap_Scroll;
            // 
            // lblMapWarpY
            // 
            lblMapWarpY.AutoSize = true;
            lblMapWarpY.Location = new Point(8, 77);
            lblMapWarpY.Margin = new Padding(4, 0, 4, 0);
            lblMapWarpY.Name = "lblMapWarpY";
            lblMapWarpY.Size = new Size(26, 15);
            lblMapWarpY.TabIndex = 2;
            lblMapWarpY.Text = "Y: 1";
            // 
            // lblMapWarpX
            // 
            lblMapWarpX.AutoSize = true;
            lblMapWarpX.Location = new Point(8, 53);
            lblMapWarpX.Margin = new Padding(4, 0, 4, 0);
            lblMapWarpX.Name = "lblMapWarpX";
            lblMapWarpX.Size = new Size(26, 15);
            lblMapWarpX.TabIndex = 1;
            lblMapWarpX.Text = "X: 1";
            // 
            // lblMapWarpMap
            // 
            lblMapWarpMap.AutoSize = true;
            lblMapWarpMap.Location = new Point(7, 29);
            lblMapWarpMap.Margin = new Padding(4, 0, 4, 0);
            lblMapWarpMap.Name = "lblMapWarpMap";
            lblMapWarpMap.Size = new Size(43, 15);
            lblMapWarpMap.TabIndex = 0;
            lblMapWarpMap.Text = "Map: 1";
            // 
            // fraNPCSpawn
            // 
            fraNPCSpawn.Controls.Add(lstNPC);
            fraNPCSpawn.Controls.Add(btnNPCSpawn);
            fraNPCSpawn.Controls.Add(scrlNPCDir);
            fraNPCSpawn.Controls.Add(lblNPCDir);
            fraNPCSpawn.Location = new Point(4, 7);
            fraNPCSpawn.Margin = new Padding(4, 3, 4, 3);
            fraNPCSpawn.Name = "fraNPCSpawn";
            fraNPCSpawn.Padding = new Padding(4, 3, 4, 3);
            fraNPCSpawn.Size = new Size(203, 130);
            fraNPCSpawn.TabIndex = 11;
            fraNPCSpawn.TabStop = false;
            fraNPCSpawn.Text = "NPC Spawn";
            // 
            // lstNPC
            // 
            lstNPC.DropDownStyle = ComboBoxStyle.DropDownList;
            lstNPC.FormattingEnabled = true;
            lstNPC.Location = new Point(7, 18);
            lstNPC.Margin = new Padding(4, 3, 4, 3);
            lstNPC.Name = "lstNPC";
            lstNPC.Size = new Size(180, 23);
            lstNPC.TabIndex = 37;
            // 
            // btnNPCSpawn
            // 
            btnNPCSpawn.Location = new Point(46, 88);
            btnNPCSpawn.Margin = new Padding(4, 3, 4, 3);
            btnNPCSpawn.Name = "btnNPCSpawn";
            btnNPCSpawn.Size = new Size(105, 32);
            btnNPCSpawn.TabIndex = 6;
            btnNPCSpawn.Text = "Accept";
            btnNPCSpawn.UseVisualStyleBackColor = true;
            btnNPCSpawn.Click += BtnNPCSpawn_Click;
            // 
            // scrlNPCDir
            // 
            scrlNPCDir.LargeChange = 1;
            scrlNPCDir.Location = new Point(9, 63);
            scrlNPCDir.Maximum = 3;
            scrlNPCDir.Name = "scrlNPCDir";
            scrlNPCDir.Size = new Size(178, 18);
            scrlNPCDir.TabIndex = 3;
            scrlNPCDir.ValueChanged += ScrlNPCDir_Scroll;
            // 
            // lblNPCDir
            // 
            lblNPCDir.AutoSize = true;
            lblNPCDir.Location = new Point(6, 46);
            lblNPCDir.Margin = new Padding(4, 0, 4, 0);
            lblNPCDir.Name = "lblNPCDir";
            lblNPCDir.Size = new Size(76, 15);
            lblNPCDir.TabIndex = 0;
            lblNPCDir.Text = "Direction: Up";
            // 
            // fraHeal
            // 
            fraHeal.Controls.Add(scrlHeal);
            fraHeal.Controls.Add(lblHeal);
            fraHeal.Controls.Add(cmbHeal);
            fraHeal.Controls.Add(btnHeal);
            fraHeal.Location = new Point(4, 290);
            fraHeal.Margin = new Padding(4, 3, 4, 3);
            fraHeal.Name = "fraHeal";
            fraHeal.Padding = new Padding(4, 3, 4, 3);
            fraHeal.Size = new Size(203, 130);
            fraHeal.TabIndex = 15;
            fraHeal.TabStop = false;
            fraHeal.Text = "Heal";
            // 
            // scrlHeal
            // 
            scrlHeal.Location = new Point(5, 65);
            scrlHeal.Name = "scrlHeal";
            scrlHeal.Size = new Size(181, 17);
            scrlHeal.TabIndex = 39;
            scrlHeal.ValueChanged += ScrlHeal_Scroll;
            // 
            // lblHeal
            // 
            lblHeal.AutoSize = true;
            lblHeal.Location = new Point(4, 50);
            lblHeal.Margin = new Padding(4, 0, 4, 0);
            lblHeal.Name = "lblHeal";
            lblHeal.Size = new Size(63, 15);
            lblHeal.TabIndex = 38;
            lblHeal.Text = "Amount: 0";
            // 
            // cmbHeal
            // 
            cmbHeal.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHeal.FormattingEnabled = true;
            cmbHeal.Items.AddRange(new object[] { "Heal HP", "Heal MP" });
            cmbHeal.Location = new Point(7, 22);
            cmbHeal.Margin = new Padding(4, 3, 4, 3);
            cmbHeal.Name = "cmbHeal";
            cmbHeal.Size = new Size(180, 23);
            cmbHeal.TabIndex = 37;
            // 
            // btnHeal
            // 
            btnHeal.Location = new Point(43, 88);
            btnHeal.Margin = new Padding(4, 3, 4, 3);
            btnHeal.Name = "btnHeal";
            btnHeal.Size = new Size(105, 32);
            btnHeal.TabIndex = 6;
            btnHeal.Text = "Accept";
            btnHeal.UseVisualStyleBackColor = true;
            btnHeal.Click += BtnHeal_Click;
            // 
            // fraShop
            // 
            fraShop.Controls.Add(cmbShop);
            fraShop.Controls.Add(btnShop);
            fraShop.Location = new Point(394, 9);
            fraShop.Margin = new Padding(4, 3, 4, 3);
            fraShop.Name = "fraShop";
            fraShop.Padding = new Padding(4, 3, 4, 3);
            fraShop.Size = new Size(172, 138);
            fraShop.TabIndex = 12;
            fraShop.TabStop = false;
            fraShop.Text = "Shop";
            // 
            // cmbShop
            // 
            cmbShop.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShop.FormattingEnabled = true;
            cmbShop.Location = new Point(7, 22);
            cmbShop.Margin = new Padding(4, 3, 4, 3);
            cmbShop.Name = "cmbShop";
            cmbShop.Size = new Size(154, 23);
            cmbShop.TabIndex = 37;
            // 
            // btnShop
            // 
            btnShop.Location = new Point(34, 98);
            btnShop.Margin = new Padding(4, 3, 4, 3);
            btnShop.Name = "btnShop";
            btnShop.Size = new Size(105, 32);
            btnShop.TabIndex = 6;
            btnShop.Text = "Accept";
            btnShop.UseVisualStyleBackColor = true;
            btnShop.Click += BtnShop_Click;
            // 
            // fraResource
            // 
            fraResource.Controls.Add(btnResourceOk);
            fraResource.Controls.Add(scrlResource);
            fraResource.Controls.Add(lblResource);
            fraResource.Location = new Point(214, 7);
            fraResource.Margin = new Padding(4, 3, 4, 3);
            fraResource.Name = "fraResource";
            fraResource.Padding = new Padding(4, 3, 4, 3);
            fraResource.Size = new Size(172, 130);
            fraResource.TabIndex = 10;
            fraResource.TabStop = false;
            fraResource.Text = "Resource";
            // 
            // btnResourceOk
            // 
            btnResourceOk.Location = new Point(33, 88);
            btnResourceOk.Margin = new Padding(4, 3, 4, 3);
            btnResourceOk.Name = "btnResourceOk";
            btnResourceOk.Size = new Size(105, 32);
            btnResourceOk.TabIndex = 6;
            btnResourceOk.Text = "Accept";
            btnResourceOk.UseVisualStyleBackColor = true;
            btnResourceOk.Click += BtnResourceOk_Click;
            // 
            // scrlResource
            // 
            scrlResource.Location = new Point(4, 42);
            scrlResource.Name = "scrlResource";
            scrlResource.Size = new Size(159, 18);
            scrlResource.TabIndex = 3;
            scrlResource.ValueChanged += ScrlResource_ValueChanged;
            // 
            // lblResource
            // 
            lblResource.AutoSize = true;
            lblResource.Location = new Point(0, 18);
            lblResource.Margin = new Padding(4, 0, 4, 0);
            lblResource.Name = "lblResource";
            lblResource.Size = new Size(45, 15);
            lblResource.TabIndex = 0;
            lblResource.Text = "Object:";
            // 
            // fraMapItem
            // 
            fraMapItem.Controls.Add(picMapItem);
            fraMapItem.Controls.Add(btnMapItem);
            fraMapItem.Controls.Add(scrlMapItemValue);
            fraMapItem.Controls.Add(scrlMapItem);
            fraMapItem.Controls.Add(lblMapItem);
            fraMapItem.Location = new Point(4, 137);
            fraMapItem.Margin = new Padding(4, 3, 4, 3);
            fraMapItem.Name = "fraMapItem";
            fraMapItem.Padding = new Padding(4, 3, 4, 3);
            fraMapItem.Size = new Size(203, 137);
            fraMapItem.TabIndex = 7;
            fraMapItem.TabStop = false;
            fraMapItem.Text = "Map Item";
            // 
            // picMapItem
            // 
            picMapItem.BackColor = Color.Black;
            picMapItem.Location = new Point(155, 42);
            picMapItem.Margin = new Padding(4, 3, 4, 3);
            picMapItem.Name = "picMapItem";
            picMapItem.Size = new Size(37, 37);
            picMapItem.TabIndex = 7;
            picMapItem.TabStop = false;
            // 
            // btnMapItem
            // 
            btnMapItem.Location = new Point(46, 97);
            btnMapItem.Margin = new Padding(4, 3, 4, 3);
            btnMapItem.Name = "btnMapItem";
            btnMapItem.Size = new Size(105, 32);
            btnMapItem.TabIndex = 6;
            btnMapItem.Text = "Accept";
            btnMapItem.UseVisualStyleBackColor = true;
            btnMapItem.Click += BtnMapItem_Click;
            // 
            // scrlMapItemValue
            // 
            scrlMapItemValue.Location = new Point(10, 68);
            scrlMapItemValue.Name = "scrlMapItemValue";
            scrlMapItemValue.Size = new Size(140, 18);
            scrlMapItemValue.TabIndex = 4;
            scrlMapItemValue.ValueChanged += ScrlMapItemValue_ValueChanged;
            // 
            // scrlMapItem
            // 
            scrlMapItem.Location = new Point(10, 43);
            scrlMapItem.Name = "scrlMapItem";
            scrlMapItem.Size = new Size(140, 18);
            scrlMapItem.TabIndex = 3;
            scrlMapItem.ValueChanged += ScrlMapItem_ValueChanged;
            // 
            // lblMapItem
            // 
            lblMapItem.AutoSize = true;
            lblMapItem.Location = new Point(7, 25);
            lblMapItem.Margin = new Padding(4, 0, 4, 0);
            lblMapItem.Name = "lblMapItem";
            lblMapItem.Size = new Size(51, 15);
            lblMapItem.TabIndex = 0;
            lblMapItem.Text = "None x0";
            // 
            // fraTrap
            // 
            fraTrap.Controls.Add(btnTrap);
            fraTrap.Controls.Add(scrlTrap);
            fraTrap.Controls.Add(lblTrap);
            fraTrap.Location = new Point(214, 144);
            fraTrap.Margin = new Padding(4, 3, 4, 3);
            fraTrap.Name = "fraTrap";
            fraTrap.Padding = new Padding(4, 3, 4, 3);
            fraTrap.Size = new Size(172, 138);
            fraTrap.TabIndex = 16;
            fraTrap.TabStop = false;
            fraTrap.Text = "Trap";
            // 
            // btnTrap
            // 
            btnTrap.Location = new Point(33, 98);
            btnTrap.Margin = new Padding(4, 3, 4, 3);
            btnTrap.Name = "btnTrap";
            btnTrap.Size = new Size(105, 32);
            btnTrap.TabIndex = 42;
            btnTrap.Text = "Accept";
            btnTrap.UseVisualStyleBackColor = true;
            btnTrap.Click += BtnTrap_Click;
            // 
            // scrlTrap
            // 
            scrlTrap.Location = new Point(13, 38);
            scrlTrap.Name = "scrlTrap";
            scrlTrap.Size = new Size(149, 17);
            scrlTrap.TabIndex = 41;
            scrlTrap.ValueChanged += ScrlTrap_ValueChanged;
            // 
            // lblTrap
            // 
            lblTrap.AutoSize = true;
            lblTrap.Location = new Point(7, 18);
            lblTrap.Margin = new Padding(4, 0, 4, 0);
            lblTrap.Name = "lblTrap";
            lblTrap.Size = new Size(63, 15);
            lblTrap.TabIndex = 40;
            lblTrap.Text = "Amount: 0";
            // 
            // ToolStrip
            // 
            ToolStrip.AutoSize = false;
            ToolStrip.ImageScalingSize = new Size(24, 24);
            ToolStrip.Items.AddRange(new ToolStripItem[] { tsbSave, tsbDiscard, ToolStripSeparator1, tsbMapGrid, tsbOpacity, ToolStripSeparator2, tsbFill, tsbClear, tsbEyeDropper, tsbCopyMap, tsbDeleteMap, tsbUndo, tsbRedo, tsbScreenshot });
            ToolStrip.Location = new Point(0, 0);
            ToolStrip.Name = "ToolStrip";
            ToolStrip.Padding = new Padding(0, 0, 2, 0);
            ToolStrip.Size = new Size(1141, 28);
            ToolStrip.TabIndex = 13;
            ToolStrip.Text = "ToolStrip1";
            ToolStrip.MouseHover += ToolStrip_MouseHover;
            // 
            // tsbSave
            // 
            tsbSave.Image = (Image)resources.GetObject("tsbSave.Image");
            tsbSave.ImageTransparentColor = Color.Magenta;
            tsbSave.Name = "tsbSave";
            tsbSave.Size = new Size(28, 25);
            tsbSave.ToolTipText = "Save";
            tsbSave.Click += TsbSave_Click;
            // 
            // tsbDiscard
            // 
            tsbDiscard.Image = (Image)resources.GetObject("tsbDiscard.Image");
            tsbDiscard.ImageTransparentColor = Color.Magenta;
            tsbDiscard.Name = "tsbDiscard";
            tsbDiscard.Size = new Size(28, 25);
            tsbDiscard.ToolTipText = "Discard";
            tsbDiscard.Click += TsbDiscard_Click;
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(6, 28);
            // 
            // tsbMapGrid
            // 
            tsbMapGrid.Image = (Image)resources.GetObject("tsbMapGrid.Image");
            tsbMapGrid.ImageTransparentColor = Color.Magenta;
            tsbMapGrid.Name = "tsbMapGrid";
            tsbMapGrid.Size = new Size(28, 25);
            tsbMapGrid.Tag = "Map Grid";
            tsbMapGrid.Click += TsbMapGrid_Click;
            // 
            // tsbOpacity
            // 
            tsbOpacity.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbOpacity.Image = (Image)resources.GetObject("tsbOpacity.Image");
            tsbOpacity.ImageTransparentColor = Color.Magenta;
            tsbOpacity.Name = "tsbOpacity";
            tsbOpacity.Size = new Size(28, 25);
            tsbOpacity.Text = "ToolStripButton1";
            tsbOpacity.ToolTipText = "Opacity";
            tsbOpacity.Click += tsbOpacity_Click;
            // 
            // ToolStripSeparator2
            // 
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            ToolStripSeparator2.Size = new Size(6, 28);
            // 
            // tsbFill
            // 
            tsbFill.Image = (Image)resources.GetObject("tsbFill.Image");
            tsbFill.ImageTransparentColor = Color.Magenta;
            tsbFill.Name = "tsbFill";
            tsbFill.Size = new Size(28, 25);
            tsbFill.Tag = "Fill";
            tsbFill.ToolTipText = "Fill Layer";
            tsbFill.Click += TsbFill_Click;
            // 
            // tsbClear
            // 
            tsbClear.Image = (Image)resources.GetObject("tsbClear.Image");
            tsbClear.ImageTransparentColor = Color.Magenta;
            tsbClear.Name = "tsbClear";
            tsbClear.Size = new Size(28, 25);
            tsbClear.ToolTipText = "Erase";
            tsbClear.Click += TsbClear_Click;
            // 
            // tsbEyeDropper
            // 
            tsbEyeDropper.Image = (Image)resources.GetObject("tsbEyeDropper.Image");
            tsbEyeDropper.ImageTransparentColor = Color.Magenta;
            tsbEyeDropper.Name = "tsbEyeDropper";
            tsbEyeDropper.Size = new Size(28, 25);
            tsbEyeDropper.ToolTipText = "Eye Dropper";
            tsbEyeDropper.Click += TsbEyeDropper_Click;
            // 
            // tsbCopyMap
            // 
            tsbCopyMap.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbCopyMap.Image = (Image)resources.GetObject("tsbCopyMap.Image");
            tsbCopyMap.ImageTransparentColor = Color.Magenta;
            tsbCopyMap.Name = "tsbCopyMap";
            tsbCopyMap.Size = new Size(28, 25);
            tsbCopyMap.ToolTipText = "Copy";
            tsbCopyMap.Click += tsbCopyMap_Click;
            // 
            // tsbDeleteMap
            // 
            tsbDeleteMap.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbDeleteMap.Image = (Image)resources.GetObject("tsbDeleteMap.Image");
            tsbDeleteMap.ImageTransparentColor = Color.Magenta;
            tsbDeleteMap.Name = "tsbDeleteMap";
            tsbDeleteMap.Size = new Size(28, 25);
            tsbDeleteMap.ToolTipText = "Delete Map";
            tsbDeleteMap.Click += tsbDeleteMap_Click;
            // 
            // tsbUndo
            // 
            tsbUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbUndo.Image = (Image)resources.GetObject("tsbUndo.Image");
            tsbUndo.ImageTransparentColor = Color.Magenta;
            tsbUndo.Name = "tsbUndo";
            tsbUndo.Size = new Size(28, 25);
            tsbUndo.ToolTipText = "Undo";
            tsbUndo.Click += tsbUndo_Click;
            // 
            // tsbRedo
            // 
            tsbRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRedo.Image = (Image)resources.GetObject("tsbRedo.Image");
            tsbRedo.ImageTransparentColor = Color.Magenta;
            tsbRedo.Name = "tsbRedo";
            tsbRedo.Size = new Size(28, 25);
            tsbRedo.ToolTipText = "Redo";
            tsbRedo.Click += tsbRedo_Click;
            // 
            // tsbScreenshot
            // 
            tsbScreenshot.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbScreenshot.Image = (Image)resources.GetObject("tsbScreenshot.Image");
            tsbScreenshot.ImageTransparentColor = Color.Magenta;
            tsbScreenshot.Name = "tsbScreenshot";
            tsbScreenshot.Size = new Size(28, 25);
            tsbScreenshot.ToolTipText = "Screenshot";
            tsbScreenshot.Click += tsbScreenshot_Click;
            // 
            // tabpages
            // 
            tabpages.Controls.Add(tpTiles);
            tabpages.Controls.Add(tpAttributes);
            tabpages.Controls.Add(tpNPCs);
            tabpages.Controls.Add(tpSettings);
            tabpages.Controls.Add(tpDirBlock);
            tabpages.Controls.Add(tpEvents);
            tabpages.Controls.Add(tpEffects);
            tabpages.Location = new Point(5, 32);
            tabpages.Margin = new Padding(4, 3, 4, 3);
            tabpages.Name = "tabpages";
            tabpages.SelectedIndex = 0;
            tabpages.Size = new Size(550, 629);
            tabpages.TabIndex = 14;
            // 
            // tpTiles
            // 
            tpTiles.Controls.Add(cmbAutoTile);
            tpTiles.Controls.Add(Label11);
            tpTiles.Controls.Add(Label10);
            tpTiles.Controls.Add(cmbLayers);
            tpTiles.Controls.Add(Label9);
            tpTiles.Controls.Add(cmbTileSets);
            tpTiles.Controls.Add(pnlBack);
            tpTiles.Controls.Add(Label1);
            tpTiles.Location = new Point(4, 24);
            tpTiles.Margin = new Padding(4, 3, 4, 3);
            tpTiles.Name = "tpTiles";
            tpTiles.Padding = new Padding(4, 3, 4, 3);
            tpTiles.Size = new Size(542, 601);
            tpTiles.TabIndex = 0;
            tpTiles.Text = "Tiles";
            tpTiles.UseVisualStyleBackColor = true;
            // 
            // cmbAutoTile
            // 
            cmbAutoTile.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAutoTile.FormattingEnabled = true;
            cmbAutoTile.Items.AddRange(new object[] { "Normal", "AutoTile (VX)", "Fake (VX)", "Animated (VX)", "Cliff (VX)", "Waterfall (VX)" });
            cmbAutoTile.Location = new Point(428, 566);
            cmbAutoTile.Margin = new Padding(4, 3, 4, 3);
            cmbAutoTile.Name = "cmbAutoTile";
            cmbAutoTile.Size = new Size(110, 23);
            cmbAutoTile.TabIndex = 17;
            cmbAutoTile.SelectedIndexChanged += CmbAutoTile_SelectedIndexChanged;
            // 
            // Label11
            // 
            Label11.AutoSize = true;
            Label11.Location = new Point(364, 570);
            Label11.Margin = new Padding(4, 0, 4, 0);
            Label11.Name = "Label11";
            Label11.Size = new Size(52, 15);
            Label11.TabIndex = 16;
            Label11.Text = "Autotile:";
            // 
            // Label10
            // 
            Label10.AutoSize = true;
            Label10.Location = new Point(167, 570);
            Label10.Margin = new Padding(4, 0, 4, 0);
            Label10.Name = "Label10";
            Label10.Size = new Size(38, 15);
            Label10.TabIndex = 15;
            Label10.Text = "Layer:";
            // 
            // cmbLayers
            // 
            cmbLayers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLayers.FormattingEnabled = true;
            cmbLayers.Items.AddRange(new object[] { "Ground", "Mask", "Mask Anim", "Cover", "Cover Anim", "Fringe", "Fringe Anim", "Roof", "Roof Anim" });
            cmbLayers.Location = new Point(216, 566);
            cmbLayers.Margin = new Padding(4, 3, 4, 3);
            cmbLayers.Name = "cmbLayers";
            cmbLayers.Size = new Size(112, 23);
            cmbLayers.TabIndex = 14;
            // 
            // Label9
            // 
            Label9.AutoSize = true;
            Label9.Location = new Point(8, 570);
            Label9.Margin = new Padding(4, 0, 4, 0);
            Label9.Name = "Label9";
            Label9.Size = new Size(43, 15);
            Label9.TabIndex = 13;
            Label9.Text = "Tileset:";
            // 
            // cmbTileSets
            // 
            cmbTileSets.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTileSets.FormattingEnabled = true;
            cmbTileSets.Location = new Point(63, 566);
            cmbTileSets.Margin = new Padding(4, 3, 4, 3);
            cmbTileSets.Name = "cmbTileSets";
            cmbTileSets.Size = new Size(68, 23);
            cmbTileSets.TabIndex = 12;
            cmbTileSets.Click += CmbTileSets_Click;
            // 
            // tpAttributes
            // 
            tpAttributes.Controls.Add(optNoXing);
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
            tpAttributes.Controls.Add(optNPCAvoid);
            tpAttributes.Controls.Add(optNPCSpawn);
            tpAttributes.Controls.Add(optResource);
            tpAttributes.Location = new Point(4, 24);
            tpAttributes.Margin = new Padding(4, 3, 4, 3);
            tpAttributes.Name = "tpAttributes";
            tpAttributes.Padding = new Padding(4, 3, 4, 3);
            tpAttributes.Size = new Size(542, 601);
            tpAttributes.TabIndex = 3;
            tpAttributes.Text = "Attributes";
            tpAttributes.UseVisualStyleBackColor = true;
            // 
            // optNoXing
            // 
            optNoXing.AutoSize = true;
            optNoXing.Location = new Point(12, 95);
            optNoXing.Margin = new Padding(4, 3, 4, 3);
            optNoXing.Name = "optNoXing";
            optNoXing.Size = new Size(68, 19);
            optNoXing.TabIndex = 25;
            optNoXing.Text = "No Xing";
            optNoXing.UseVisualStyleBackColor = true;
            // 
            // btnFillAttributes
            // 
            btnFillAttributes.Location = new Point(256, 568);
            btnFillAttributes.Margin = new Padding(4, 3, 4, 3);
            btnFillAttributes.Name = "btnFillAttributes";
            btnFillAttributes.Size = new Size(136, 29);
            btnFillAttributes.TabIndex = 24;
            btnFillAttributes.Text = "Fill Attributes";
            btnFillAttributes.UseVisualStyleBackColor = true;
            btnFillAttributes.Click += btnFillAttributes_Click;
            // 
            // optInfo
            // 
            optInfo.AutoSize = true;
            optInfo.Location = new Point(137, 575);
            optInfo.Margin = new Padding(4, 3, 4, 3);
            optInfo.Name = "optInfo";
            optInfo.Size = new Size(46, 19);
            optInfo.TabIndex = 22;
            optInfo.Text = "Info";
            optInfo.UseVisualStyleBackColor = true;
            // 
            // Label23
            // 
            Label23.AutoSize = true;
            Label23.Location = new Point(8, 576);
            Label23.Margin = new Padding(4, 0, 4, 0);
            Label23.Name = "Label23";
            Label23.Size = new Size(34, 15);
            Label23.TabIndex = 21;
            Label23.Text = "Type:";
            // 
            // cmbAttribute
            // 
            cmbAttribute.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAttribute.FormattingEnabled = true;
            cmbAttribute.Items.AddRange(new object[] { "Layer 1", "Layer 2" });
            cmbAttribute.Location = new Point(62, 574);
            cmbAttribute.Margin = new Padding(4, 3, 4, 3);
            cmbAttribute.Name = "cmbAttribute";
            cmbAttribute.Size = new Size(68, 23);
            cmbAttribute.TabIndex = 20;
            cmbAttribute.SelectedIndexChanged += cmbAttribute_SelectedIndexChanged;
            // 
            // optAnimation
            // 
            optAnimation.AutoSize = true;
            optAnimation.Location = new Point(373, 58);
            optAnimation.Margin = new Padding(4, 3, 4, 3);
            optAnimation.Name = "optAnimation";
            optAnimation.Size = new Size(81, 19);
            optAnimation.TabIndex = 19;
            optAnimation.Text = "Animation";
            optAnimation.UseVisualStyleBackColor = true;
            optAnimation.CheckedChanged += optAnimation_CheckedChanged;
            // 
            // tpNPCs
            // 
            tpNPCs.Controls.Add(fraNPCs);
            tpNPCs.Location = new Point(4, 24);
            tpNPCs.Margin = new Padding(4, 3, 4, 3);
            tpNPCs.Name = "tpNPCs";
            tpNPCs.Padding = new Padding(4, 3, 4, 3);
            tpNPCs.Size = new Size(542, 601);
            tpNPCs.TabIndex = 1;
            tpNPCs.Text = "NPCs";
            tpNPCs.UseVisualStyleBackColor = true;
            // 
            // fraNPCs
            // 
            fraNPCs.Controls.Add(Label18);
            fraNPCs.Controls.Add(Label17);
            fraNPCs.Controls.Add(cmbNPCList);
            fraNPCs.Controls.Add(lstMapNPC);
            fraNPCs.Controls.Add(ComboBox23);
            fraNPCs.Location = new Point(7, 9);
            fraNPCs.Margin = new Padding(4, 3, 4, 3);
            fraNPCs.Name = "fraNPCs";
            fraNPCs.Padding = new Padding(4, 3, 4, 3);
            fraNPCs.Size = new Size(559, 492);
            fraNPCs.TabIndex = 11;
            fraNPCs.TabStop = false;
            fraNPCs.Text = "NPCs";
            // 
            // Label18
            // 
            Label18.AutoSize = true;
            Label18.Location = new Point(228, 33);
            Label18.Margin = new Padding(4, 0, 4, 0);
            Label18.Name = "Label18";
            Label18.Size = new Size(77, 15);
            Label18.TabIndex = 72;
            Label18.Text = "2. Select NPC";
            // 
            // Label17
            // 
            Label17.AutoSize = true;
            Label17.Location = new Point(7, 33);
            Label17.Margin = new Padding(4, 0, 4, 0);
            Label17.Name = "Label17";
            Label17.Size = new Size(64, 15);
            Label17.TabIndex = 71;
            Label17.Text = "1. NPC LIst";
            // 
            // cmbNPCList
            // 
            cmbNPCList.FormattingEnabled = true;
            cmbNPCList.Location = new Point(228, 52);
            cmbNPCList.Margin = new Padding(4, 3, 4, 3);
            cmbNPCList.Name = "cmbNPCList";
            cmbNPCList.Size = new Size(299, 23);
            cmbNPCList.TabIndex = 70;
            cmbNPCList.SelectedIndexChanged += CmbNPCList_SelectedIndexChanged;
            // 
            // lstMapNPC
            // 
            lstMapNPC.FormattingEnabled = true;
            lstMapNPC.Location = new Point(10, 52);
            lstMapNPC.Margin = new Padding(4, 3, 4, 3);
            lstMapNPC.Name = "lstMapNPC";
            lstMapNPC.Size = new Size(210, 424);
            lstMapNPC.TabIndex = 69;
            // 
            // ComboBox23
            // 
            ComboBox23.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox23.FormattingEnabled = true;
            ComboBox23.Location = new Point(398, 541);
            ComboBox23.Margin = new Padding(4, 3, 4, 3);
            ComboBox23.Name = "ComboBox23";
            ComboBox23.Size = new Size(154, 23);
            ComboBox23.TabIndex = 68;
            // 
            // tpSettings
            // 
            tpSettings.Controls.Add(fraMapSettings);
            tpSettings.Controls.Add(fraMapLinks);
            tpSettings.Controls.Add(fraBootSettings);
            tpSettings.Controls.Add(fraMaxSizes);
            tpSettings.Controls.Add(GroupBox2);
            tpSettings.Controls.Add(txtName);
            tpSettings.Controls.Add(Label6);
            tpSettings.Location = new Point(4, 24);
            tpSettings.Margin = new Padding(4, 3, 4, 3);
            tpSettings.Name = "tpSettings";
            tpSettings.Padding = new Padding(4, 3, 4, 3);
            tpSettings.Size = new Size(542, 601);
            tpSettings.TabIndex = 2;
            tpSettings.Text = "Settings";
            tpSettings.UseVisualStyleBackColor = true;
            // 
            // fraMapSettings
            // 
            fraMapSettings.Controls.Add(Label22);
            fraMapSettings.Controls.Add(lstShop);
            fraMapSettings.Controls.Add(Label8);
            fraMapSettings.Controls.Add(lstMoral);
            fraMapSettings.Location = new Point(7, 37);
            fraMapSettings.Margin = new Padding(4, 3, 4, 3);
            fraMapSettings.Name = "fraMapSettings";
            fraMapSettings.Padding = new Padding(4, 3, 4, 3);
            fraMapSettings.Size = new Size(271, 79);
            fraMapSettings.TabIndex = 15;
            fraMapSettings.TabStop = false;
            fraMapSettings.Text = "Settings";
            // 
            // Label22
            // 
            Label22.AutoSize = true;
            Label22.Location = new Point(4, 41);
            Label22.Margin = new Padding(4, 0, 4, 0);
            Label22.Name = "Label22";
            Label22.Size = new Size(37, 15);
            Label22.TabIndex = 40;
            Label22.Text = "Shop:";
            // 
            // lstShop
            // 
            lstShop.DropDownStyle = ComboBoxStyle.DropDownList;
            lstShop.FormattingEnabled = true;
            lstShop.Location = new Point(52, 40);
            lstShop.Margin = new Padding(4, 3, 4, 3);
            lstShop.Name = "lstShop";
            lstShop.Size = new Size(210, 23);
            lstShop.TabIndex = 39;
            // 
            // Label8
            // 
            Label8.AutoSize = true;
            Label8.Location = new Point(4, 17);
            Label8.Margin = new Padding(4, 0, 4, 0);
            Label8.Name = "Label8";
            Label8.Size = new Size(41, 15);
            Label8.TabIndex = 38;
            Label8.Text = "Moral:";
            // 
            // lstMoral
            // 
            lstMoral.DropDownStyle = ComboBoxStyle.DropDownList;
            lstMoral.FormattingEnabled = true;
            lstMoral.Location = new Point(52, 14);
            lstMoral.Margin = new Padding(4, 3, 4, 3);
            lstMoral.Name = "lstMoral";
            lstMoral.Size = new Size(210, 23);
            lstMoral.TabIndex = 37;
            // 
            // fraMapLinks
            // 
            fraMapLinks.Controls.Add(txtDown);
            fraMapLinks.Controls.Add(txtLeft);
            fraMapLinks.Controls.Add(lblMap);
            fraMapLinks.Controls.Add(txtRight);
            fraMapLinks.Controls.Add(txtUp);
            fraMapLinks.Location = new Point(7, 122);
            fraMapLinks.Margin = new Padding(4, 3, 4, 3);
            fraMapLinks.Name = "fraMapLinks";
            fraMapLinks.Padding = new Padding(4, 3, 4, 3);
            fraMapLinks.Size = new Size(271, 129);
            fraMapLinks.TabIndex = 14;
            fraMapLinks.TabStop = false;
            fraMapLinks.Text = "Borders";
            // 
            // txtDown
            // 
            txtDown.Location = new Point(105, 99);
            txtDown.Margin = new Padding(4, 3, 4, 3);
            txtDown.Name = "txtDown";
            txtDown.Size = new Size(58, 23);
            txtDown.TabIndex = 6;
            txtDown.Text = "0";
            // 
            // txtLeft
            // 
            txtLeft.Location = new Point(8, 54);
            txtLeft.Margin = new Padding(4, 3, 4, 3);
            txtLeft.Name = "txtLeft";
            txtLeft.Size = new Size(50, 23);
            txtLeft.TabIndex = 5;
            txtLeft.Text = "0";
            // 
            // lblMap
            // 
            lblMap.AutoSize = true;
            lblMap.Location = new Point(104, 58);
            lblMap.Margin = new Padding(4, 0, 4, 0);
            lblMap.Name = "lblMap";
            lblMap.Size = new Size(43, 15);
            lblMap.TabIndex = 4;
            lblMap.Text = "Map: 0";
            // 
            // txtRight
            // 
            txtRight.Location = new Point(206, 54);
            txtRight.Margin = new Padding(4, 3, 4, 3);
            txtRight.Name = "txtRight";
            txtRight.Size = new Size(58, 23);
            txtRight.TabIndex = 3;
            txtRight.Text = "0";
            // 
            // txtUp
            // 
            txtUp.Location = new Point(104, 12);
            txtUp.Margin = new Padding(4, 3, 4, 3);
            txtUp.Name = "txtUp";
            txtUp.Size = new Size(58, 23);
            txtUp.TabIndex = 1;
            txtUp.Text = "0";
            // 
            // fraBootSettings
            // 
            fraBootSettings.Controls.Add(chkIndoors);
            fraBootSettings.Controls.Add(chkNoMapRespawn);
            fraBootSettings.Controls.Add(txtBootMap);
            fraBootSettings.Controls.Add(Label5);
            fraBootSettings.Controls.Add(txtBootY);
            fraBootSettings.Controls.Add(Label3);
            fraBootSettings.Controls.Add(txtBootX);
            fraBootSettings.Controls.Add(Label4);
            fraBootSettings.Location = new Point(7, 258);
            fraBootSettings.Margin = new Padding(4, 3, 4, 3);
            fraBootSettings.Name = "fraBootSettings";
            fraBootSettings.Padding = new Padding(4, 3, 4, 3);
            fraBootSettings.Size = new Size(271, 125);
            fraBootSettings.TabIndex = 13;
            fraBootSettings.TabStop = false;
            fraBootSettings.Text = "Respawn Settings";
            // 
            // chkIndoors
            // 
            chkIndoors.AutoSize = true;
            chkIndoors.Location = new Point(8, 102);
            chkIndoors.Margin = new Padding(4, 3, 4, 3);
            chkIndoors.Name = "chkIndoors";
            chkIndoors.Size = new Size(66, 19);
            chkIndoors.TabIndex = 42;
            chkIndoors.Text = "Indoors";
            chkIndoors.UseVisualStyleBackColor = true;
            chkIndoors.CheckedChanged += chkIndoors_CheckedChanged;
            // 
            // chkNoMapRespawn
            // 
            chkNoMapRespawn.AutoSize = true;
            chkNoMapRespawn.Location = new Point(143, 102);
            chkNoMapRespawn.Margin = new Padding(4, 3, 4, 3);
            chkNoMapRespawn.Name = "chkNoMapRespawn";
            chkNoMapRespawn.Size = new Size(119, 19);
            chkNoMapRespawn.TabIndex = 19;
            chkNoMapRespawn.Text = "No Map Respawn";
            chkNoMapRespawn.UseVisualStyleBackColor = true;
            chkNoMapRespawn.CheckedChanged += chkRespawn_CheckedChanged;
            // 
            // txtBootMap
            // 
            txtBootMap.Location = new Point(205, 13);
            txtBootMap.Margin = new Padding(4, 3, 4, 3);
            txtBootMap.Name = "txtBootMap";
            txtBootMap.Size = new Size(58, 23);
            txtBootMap.TabIndex = 5;
            txtBootMap.Text = "0";
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(7, 18);
            Label5.Margin = new Padding(4, 0, 4, 0);
            Label5.Name = "Label5";
            Label5.Size = new Size(34, 15);
            Label5.TabIndex = 4;
            Label5.Text = "Map:";
            // 
            // txtBootY
            // 
            txtBootY.Location = new Point(205, 73);
            txtBootY.Margin = new Padding(4, 3, 4, 3);
            txtBootY.Name = "txtBootY";
            txtBootY.Size = new Size(58, 23);
            txtBootY.TabIndex = 3;
            txtBootY.Text = "0";
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(7, 75);
            Label3.Margin = new Padding(4, 0, 4, 0);
            Label3.Name = "Label3";
            Label3.Size = new Size(17, 15);
            Label3.TabIndex = 2;
            Label3.Text = "Y:";
            // 
            // txtBootX
            // 
            txtBootX.Location = new Point(205, 43);
            txtBootX.Margin = new Padding(4, 3, 4, 3);
            txtBootX.Name = "txtBootX";
            txtBootX.Size = new Size(58, 23);
            txtBootX.TabIndex = 1;
            txtBootX.Text = "0";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(7, 43);
            Label4.Margin = new Padding(4, 0, 4, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(17, 15);
            Label4.TabIndex = 0;
            Label4.Text = "X:";
            // 
            // fraMaxSizes
            // 
            fraMaxSizes.Controls.Add(txtMaxY);
            fraMaxSizes.Controls.Add(Label2);
            fraMaxSizes.Controls.Add(txtMaxX);
            fraMaxSizes.Controls.Add(Label7);
            fraMaxSizes.Location = new Point(285, 258);
            fraMaxSizes.Margin = new Padding(4, 3, 4, 3);
            fraMaxSizes.Name = "fraMaxSizes";
            fraMaxSizes.Padding = new Padding(4, 3, 4, 3);
            fraMaxSizes.Size = new Size(249, 90);
            fraMaxSizes.TabIndex = 12;
            fraMaxSizes.TabStop = false;
            fraMaxSizes.Text = "Size Settings";
            // 
            // txtMaxY
            // 
            txtMaxY.Location = new Point(145, 48);
            txtMaxY.Margin = new Padding(4, 3, 4, 3);
            txtMaxY.Name = "txtMaxY";
            txtMaxY.Size = new Size(58, 23);
            txtMaxY.TabIndex = 3;
            txtMaxY.Text = "0";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(7, 52);
            Label2.Margin = new Padding(4, 0, 4, 0);
            Label2.Name = "Label2";
            Label2.Size = new Size(43, 15);
            Label2.TabIndex = 2;
            Label2.Text = "Max Y:";
            // 
            // txtMaxX
            // 
            txtMaxX.Location = new Point(145, 18);
            txtMaxX.Margin = new Padding(4, 3, 4, 3);
            txtMaxX.Name = "txtMaxX";
            txtMaxX.Size = new Size(58, 23);
            txtMaxX.TabIndex = 1;
            txtMaxX.Text = "0";
            // 
            // Label7
            // 
            Label7.AutoSize = true;
            Label7.Location = new Point(7, 22);
            Label7.Margin = new Padding(4, 0, 4, 0);
            Label7.Name = "Label7";
            Label7.Size = new Size(43, 15);
            Label7.TabIndex = 0;
            Label7.Text = "Max X:";
            // 
            // GroupBox2
            // 
            GroupBox2.Controls.Add(btnPreview);
            GroupBox2.Controls.Add(lstMusic);
            GroupBox2.Location = new Point(285, 3);
            GroupBox2.Margin = new Padding(4, 3, 4, 3);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Padding = new Padding(4, 3, 4, 3);
            GroupBox2.Size = new Size(281, 249);
            GroupBox2.TabIndex = 11;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "Music";
            // 
            // btnPreview
            // 
            btnPreview.Image = (Image)resources.GetObject("btnPreview.Image");
            btnPreview.ImageAlign = ContentAlignment.MiddleLeft;
            btnPreview.Location = new Point(57, 208);
            btnPreview.Margin = new Padding(4, 3, 4, 3);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(162, 33);
            btnPreview.TabIndex = 4;
            btnPreview.Text = "Preview Music";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += BtnPreview_Click;
            // 
            // lstMusic
            // 
            lstMusic.FormattingEnabled = true;
            lstMusic.Location = new Point(7, 22);
            lstMusic.Margin = new Padding(4, 3, 4, 3);
            lstMusic.Name = "lstMusic";
            lstMusic.ScrollAlwaysVisible = true;
            lstMusic.Size = new Size(242, 184);
            lstMusic.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.Location = new Point(62, 7);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(215, 23);
            txtName.TabIndex = 10;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.Location = new Point(7, 10);
            Label6.Margin = new Padding(4, 0, 4, 0);
            Label6.Name = "Label6";
            Label6.Size = new Size(42, 15);
            Label6.TabIndex = 9;
            Label6.Text = "Name:";
            // 
            // tpDirBlock
            // 
            tpDirBlock.Controls.Add(Label12);
            tpDirBlock.Location = new Point(4, 24);
            tpDirBlock.Margin = new Padding(4, 3, 4, 3);
            tpDirBlock.Name = "tpDirBlock";
            tpDirBlock.Padding = new Padding(4, 3, 4, 3);
            tpDirBlock.Size = new Size(542, 601);
            tpDirBlock.TabIndex = 4;
            tpDirBlock.Text = "Directional Block";
            tpDirBlock.UseVisualStyleBackColor = true;
            // 
            // Label12
            // 
            Label12.AutoSize = true;
            Label12.Location = new Point(26, 27);
            Label12.Margin = new Padding(4, 0, 4, 0);
            Label12.Name = "Label12";
            Label12.Size = new Size(265, 15);
            Label12.TabIndex = 0;
            Label12.Text = "Just press the arrows to block that side of the tile.";
            // 
            // tpEvents
            // 
            tpEvents.Controls.Add(lblPasteMode);
            tpEvents.Controls.Add(lblCopyMode);
            tpEvents.Controls.Add(btnPasteEvent);
            tpEvents.Controls.Add(Label16);
            tpEvents.Controls.Add(btnCopyEvent);
            tpEvents.Controls.Add(Label15);
            tpEvents.Controls.Add(Label13);
            tpEvents.Location = new Point(4, 24);
            tpEvents.Margin = new Padding(4, 3, 4, 3);
            tpEvents.Name = "tpEvents";
            tpEvents.Padding = new Padding(4, 3, 4, 3);
            tpEvents.Size = new Size(542, 601);
            tpEvents.TabIndex = 5;
            tpEvents.Text = "Events";
            tpEvents.UseVisualStyleBackColor = true;
            // 
            // lblPasteMode
            // 
            lblPasteMode.AutoSize = true;
            lblPasteMode.Location = new Point(121, 197);
            lblPasteMode.Margin = new Padding(4, 0, 4, 0);
            lblPasteMode.Name = "lblPasteMode";
            lblPasteMode.Size = new Size(86, 15);
            lblPasteMode.TabIndex = 6;
            lblPasteMode.Text = "PasteMode Off";
            // 
            // lblCopyMode
            // 
            lblCopyMode.AutoSize = true;
            lblCopyMode.Location = new Point(121, 129);
            lblCopyMode.Margin = new Padding(4, 0, 4, 0);
            lblCopyMode.Name = "lblCopyMode";
            lblCopyMode.Size = new Size(86, 15);
            lblCopyMode.TabIndex = 5;
            lblCopyMode.Text = "CopyMode Off";
            // 
            // btnPasteEvent
            // 
            btnPasteEvent.Location = new Point(27, 192);
            btnPasteEvent.Margin = new Padding(4, 3, 4, 3);
            btnPasteEvent.Name = "btnPasteEvent";
            btnPasteEvent.Size = new Size(88, 27);
            btnPasteEvent.TabIndex = 4;
            btnPasteEvent.Text = "Paste Event";
            btnPasteEvent.UseVisualStyleBackColor = true;
            btnPasteEvent.Click += BtnPasteEvent_Click;
            // 
            // Label16
            // 
            Label16.AutoSize = true;
            Label16.Location = new Point(23, 172);
            Label16.Margin = new Padding(4, 0, 4, 0);
            Label16.Name = "Label16";
            Label16.Size = new Size(432, 15);
            Label16.TabIndex = 3;
            Label16.Text = "To paste a copied Event, press the paste button, then click on the map to place it.";
            // 
            // btnCopyEvent
            // 
            btnCopyEvent.Location = new Point(27, 123);
            btnCopyEvent.Margin = new Padding(4, 3, 4, 3);
            btnCopyEvent.Name = "btnCopyEvent";
            btnCopyEvent.Size = new Size(88, 27);
            btnCopyEvent.TabIndex = 2;
            btnCopyEvent.Text = "Copy Event";
            btnCopyEvent.UseVisualStyleBackColor = true;
            btnCopyEvent.Click += BtnCopyEvent_Click;
            // 
            // Label15
            // 
            Label15.AutoSize = true;
            Label15.Location = new Point(23, 100);
            Label15.Margin = new Padding(4, 0, 4, 0);
            Label15.Name = "Label15";
            Label15.Size = new Size(339, 15);
            Label15.TabIndex = 1;
            Label15.Text = "To copy a existing Event, press the copy button, then the event.";
            // 
            // Label13
            // 
            Label13.AutoSize = true;
            Label13.Location = new Point(23, 24);
            Label13.Margin = new Padding(4, 0, 4, 0);
            Label13.Name = "Label13";
            Label13.Size = new Size(265, 15);
            Label13.TabIndex = 0;
            Label13.Text = "Click on the map where you want to add a event.";
            // 
            // tpEffects
            // 
            tpEffects.Controls.Add(GroupBox6);
            tpEffects.Controls.Add(GroupBox5);
            tpEffects.Controls.Add(GroupBox4);
            tpEffects.Controls.Add(GroupBox3);
            tpEffects.Controls.Add(GroupBox1);
            tpEffects.Location = new Point(4, 24);
            tpEffects.Margin = new Padding(4, 3, 4, 3);
            tpEffects.Name = "tpEffects";
            tpEffects.Padding = new Padding(4, 3, 4, 3);
            tpEffects.Size = new Size(542, 601);
            tpEffects.TabIndex = 6;
            tpEffects.Text = "Effects";
            tpEffects.UseVisualStyleBackColor = true;
            // 
            // GroupBox6
            // 
            GroupBox6.Controls.Add(scrlMapBrightness);
            GroupBox6.Location = new Point(13, 259);
            GroupBox6.Margin = new Padding(4, 3, 4, 3);
            GroupBox6.Name = "GroupBox6";
            GroupBox6.Padding = new Padding(4, 3, 4, 3);
            GroupBox6.Size = new Size(275, 45);
            GroupBox6.TabIndex = 22;
            GroupBox6.TabStop = false;
            GroupBox6.Text = "Brightness";
            // 
            // scrlMapBrightness
            // 
            scrlMapBrightness.LargeChange = 1;
            scrlMapBrightness.Location = new Point(4, 19);
            scrlMapBrightness.Maximum = 255;
            scrlMapBrightness.Name = "scrlMapBrightness";
            scrlMapBrightness.Size = new Size(262, 17);
            scrlMapBrightness.TabIndex = 10;
            scrlMapBrightness.Scroll += scrMapBrightness_Scroll;
            // 
            // GroupBox5
            // 
            GroupBox5.Controls.Add(cmbParallax);
            GroupBox5.Location = new Point(295, 192);
            GroupBox5.Margin = new Padding(4, 3, 4, 3);
            GroupBox5.Name = "GroupBox5";
            GroupBox5.Padding = new Padding(4, 3, 4, 3);
            GroupBox5.Size = new Size(275, 61);
            GroupBox5.TabIndex = 21;
            GroupBox5.TabStop = false;
            GroupBox5.Text = "Parallax";
            // 
            // cmbParallax
            // 
            cmbParallax.FormattingEnabled = true;
            cmbParallax.Location = new Point(9, 21);
            cmbParallax.Margin = new Padding(4, 3, 4, 3);
            cmbParallax.Name = "cmbParallax";
            cmbParallax.Size = new Size(232, 23);
            cmbParallax.TabIndex = 0;
            cmbParallax.SelectedIndexChanged += CmbParallax_SelectedIndexChanged;
            // 
            // GroupBox4
            // 
            GroupBox4.Controls.Add(cmbPanorama);
            GroupBox4.Location = new Point(7, 192);
            GroupBox4.Margin = new Padding(4, 3, 4, 3);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Padding = new Padding(4, 3, 4, 3);
            GroupBox4.Size = new Size(281, 61);
            GroupBox4.TabIndex = 20;
            GroupBox4.TabStop = false;
            GroupBox4.Text = "Panorama";
            // 
            // cmbPanorama
            // 
            cmbPanorama.FormattingEnabled = true;
            cmbPanorama.Location = new Point(10, 23);
            cmbPanorama.Margin = new Padding(4, 3, 4, 3);
            cmbPanorama.Name = "cmbPanorama";
            cmbPanorama.Size = new Size(262, 23);
            cmbPanorama.TabIndex = 0;
            cmbPanorama.SelectedIndexChanged += CmbPanorama_SelectedIndexChanged;
            // 
            // GroupBox3
            // 
            GroupBox3.Controls.Add(chkTint);
            GroupBox3.Controls.Add(lblMapAlpha);
            GroupBox3.Controls.Add(lblMapBlue);
            GroupBox3.Controls.Add(lblMapGreen);
            GroupBox3.Controls.Add(lblMapRed);
            GroupBox3.Controls.Add(scrlMapAlpha);
            GroupBox3.Controls.Add(scrlMapBlue);
            GroupBox3.Controls.Add(scrlMapGreen);
            GroupBox3.Controls.Add(scrlMapRed);
            GroupBox3.Location = new Point(295, 7);
            GroupBox3.Margin = new Padding(4, 3, 4, 3);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Padding = new Padding(4, 3, 4, 3);
            GroupBox3.Size = new Size(275, 178);
            GroupBox3.TabIndex = 19;
            GroupBox3.TabStop = false;
            GroupBox3.Text = "Tint";
            // 
            // chkTint
            // 
            chkTint.AutoSize = true;
            chkTint.Location = new Point(7, 22);
            chkTint.Margin = new Padding(4, 3, 4, 3);
            chkTint.Name = "chkTint";
            chkTint.Size = new Size(61, 19);
            chkTint.TabIndex = 18;
            chkTint.Text = "Enable";
            chkTint.UseVisualStyleBackColor = true;
            chkTint.CheckedChanged += ChkUseTint_CheckedChanged;
            // 
            // lblMapAlpha
            // 
            lblMapAlpha.AutoSize = true;
            lblMapAlpha.Location = new Point(9, 111);
            lblMapAlpha.Margin = new Padding(4, 0, 4, 0);
            lblMapAlpha.Name = "lblMapAlpha";
            lblMapAlpha.Size = new Size(50, 15);
            lblMapAlpha.TabIndex = 17;
            lblMapAlpha.Text = "Alpha: 0";
            // 
            // lblMapBlue
            // 
            lblMapBlue.AutoSize = true;
            lblMapBlue.Location = new Point(9, 89);
            lblMapBlue.Margin = new Padding(4, 0, 4, 0);
            lblMapBlue.Name = "lblMapBlue";
            lblMapBlue.Size = new Size(42, 15);
            lblMapBlue.TabIndex = 16;
            lblMapBlue.Text = "Blue: 0";
            // 
            // lblMapGreen
            // 
            lblMapGreen.AutoSize = true;
            lblMapGreen.Location = new Point(9, 67);
            lblMapGreen.Margin = new Padding(4, 0, 4, 0);
            lblMapGreen.Name = "lblMapGreen";
            lblMapGreen.Size = new Size(50, 15);
            lblMapGreen.TabIndex = 15;
            lblMapGreen.Text = "Green: 0";
            // 
            // lblMapRed
            // 
            lblMapRed.AutoSize = true;
            lblMapRed.Location = new Point(7, 45);
            lblMapRed.Margin = new Padding(4, 0, 4, 0);
            lblMapRed.Name = "lblMapRed";
            lblMapRed.Size = new Size(39, 15);
            lblMapRed.TabIndex = 14;
            lblMapRed.Text = "Red: 0";
            // 
            // scrlMapAlpha
            // 
            scrlMapAlpha.LargeChange = 1;
            scrlMapAlpha.Location = new Point(74, 109);
            scrlMapAlpha.Maximum = 255;
            scrlMapAlpha.Name = "scrlMapAlpha";
            scrlMapAlpha.Size = new Size(169, 17);
            scrlMapAlpha.TabIndex = 13;
            scrlMapAlpha.ValueChanged += ScrlMapAlpha_Scroll;
            // 
            // scrlMapBlue
            // 
            scrlMapBlue.LargeChange = 1;
            scrlMapBlue.Location = new Point(74, 88);
            scrlMapBlue.Maximum = 255;
            scrlMapBlue.Name = "scrlMapBlue";
            scrlMapBlue.Size = new Size(169, 17);
            scrlMapBlue.TabIndex = 12;
            scrlMapBlue.ValueChanged += ScrlMapBlue_Scroll;
            // 
            // scrlMapGreen
            // 
            scrlMapGreen.LargeChange = 1;
            scrlMapGreen.Location = new Point(74, 64);
            scrlMapGreen.Maximum = 255;
            scrlMapGreen.Name = "scrlMapGreen";
            scrlMapGreen.Size = new Size(169, 17);
            scrlMapGreen.TabIndex = 11;
            scrlMapGreen.ValueChanged += ScrlMapGreen_Scroll;
            // 
            // scrlMapRed
            // 
            scrlMapRed.LargeChange = 1;
            scrlMapRed.Location = new Point(74, 46);
            scrlMapRed.Maximum = 255;
            scrlMapRed.Name = "scrlMapRed";
            scrlMapRed.Size = new Size(169, 17);
            scrlMapRed.TabIndex = 10;
            scrlMapRed.ValueChanged += ScrlMapRed_Scroll;
            // 
            // GroupBox1
            // 
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
            GroupBox1.Location = new Point(7, 7);
            GroupBox1.Margin = new Padding(4, 3, 4, 3);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(4, 3, 4, 3);
            GroupBox1.Size = new Size(281, 178);
            GroupBox1.TabIndex = 18;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Weather";
            // 
            // scrlFogOpacity
            // 
            scrlFogOpacity.LargeChange = 1;
            scrlFogOpacity.Location = new Point(105, 143);
            scrlFogOpacity.Maximum = 255;
            scrlFogOpacity.Name = "scrlFogOpacity";
            scrlFogOpacity.Size = new Size(169, 17);
            scrlFogOpacity.TabIndex = 9;
            scrlFogOpacity.ValueChanged += ScrlFogOpacity_Scroll;
            // 
            // lblFogOpacity
            // 
            lblFogOpacity.AutoSize = true;
            lblFogOpacity.Location = new Point(7, 145);
            lblFogOpacity.Margin = new Padding(4, 0, 4, 0);
            lblFogOpacity.Name = "lblFogOpacity";
            lblFogOpacity.Size = new Size(73, 15);
            lblFogOpacity.TabIndex = 8;
            lblFogOpacity.Text = "Fog Alpha: 0";
            // 
            // scrlFogSpeed
            // 
            scrlFogSpeed.LargeChange = 1;
            scrlFogSpeed.Location = new Point(105, 117);
            scrlFogSpeed.Name = "scrlFogSpeed";
            scrlFogSpeed.Size = new Size(169, 17);
            scrlFogSpeed.TabIndex = 7;
            scrlFogSpeed.ValueChanged += ScrlFogSpeed_Scroll;
            // 
            // lblFogSpeed
            // 
            lblFogSpeed.AutoSize = true;
            lblFogSpeed.Location = new Point(7, 121);
            lblFogSpeed.Margin = new Padding(4, 0, 4, 0);
            lblFogSpeed.Name = "lblFogSpeed";
            lblFogSpeed.Size = new Size(83, 15);
            lblFogSpeed.TabIndex = 6;
            lblFogSpeed.Text = "FogSpeed: 100";
            // 
            // scrlIntensity
            // 
            scrlIntensity.LargeChange = 1;
            scrlIntensity.Location = new Point(105, 59);
            scrlIntensity.Name = "scrlIntensity";
            scrlIntensity.Size = new Size(169, 17);
            scrlIntensity.TabIndex = 5;
            scrlIntensity.ValueChanged += ScrlIntensity_Scroll;
            // 
            // lblIntensity
            // 
            lblIntensity.AutoSize = true;
            lblIntensity.Location = new Point(7, 61);
            lblIntensity.Margin = new Padding(4, 0, 4, 0);
            lblIntensity.Name = "lblIntensity";
            lblIntensity.Size = new Size(76, 15);
            lblIntensity.TabIndex = 4;
            lblIntensity.Text = "Intensity: 100";
            // 
            // scrlFog
            // 
            scrlFog.LargeChange = 1;
            scrlFog.Location = new Point(105, 93);
            scrlFog.Name = "scrlFog";
            scrlFog.Size = new Size(169, 17);
            scrlFog.TabIndex = 3;
            scrlFog.ValueChanged += ScrlFog_Scroll;
            // 
            // lblFogIndex
            // 
            lblFogIndex.AutoSize = true;
            lblFogIndex.Location = new Point(7, 95);
            lblFogIndex.Margin = new Padding(4, 0, 4, 0);
            lblFogIndex.Name = "lblFogIndex";
            lblFogIndex.Size = new Size(39, 15);
            lblFogIndex.TabIndex = 2;
            lblFogIndex.Text = "Fog: 1";
            // 
            // Label14
            // 
            Label14.AutoSize = true;
            Label14.Location = new Point(7, 29);
            Label14.Margin = new Padding(4, 0, 4, 0);
            Label14.Name = "Label14";
            Label14.Size = new Size(81, 15);
            Label14.TabIndex = 1;
            Label14.Text = "Weather Type:";
            // 
            // cmbWeather
            // 
            cmbWeather.FormattingEnabled = true;
            cmbWeather.Items.AddRange(new object[] { "None", "Rain", "Snow", "Hail", "Sand Storm", "Storm", "Fog" });
            cmbWeather.Location = new Point(105, 25);
            cmbWeather.Margin = new Padding(4, 3, 4, 3);
            cmbWeather.Name = "cmbWeather";
            cmbWeather.Size = new Size(168, 23);
            cmbWeather.TabIndex = 0;
            cmbWeather.SelectedIndexChanged += CmbWeather_SelectedIndexChanged;
            // 
            // frmEditor_Map
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            BackColor = SystemColors.Control;
            ClientSize = new Size(552, 658);
            Controls.Add(tabpages);
            Controls.Add(ToolStrip);
            Controls.Add(pnlAttributes);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
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
            fraMapLight.ResumeLayout(false);
            fraMapLight.PerformLayout();
            fraAnimation.ResumeLayout(false);
            fraMapWarp.ResumeLayout(false);
            fraMapWarp.PerformLayout();
            fraNPCSpawn.ResumeLayout(false);
            fraNPCSpawn.PerformLayout();
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
            tpNPCs.ResumeLayout(false);
            fraNPCs.ResumeLayout(false);
            fraNPCs.PerformLayout();
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
        internal RadioButton optTrap;
        internal RadioButton optHeal;
        internal RadioButton optBank;
        internal RadioButton optShop;
        internal RadioButton optNPCSpawn;
        internal RadioButton optResource;
        internal RadioButton optNPCAvoid;
        internal RadioButton optItem;
        internal RadioButton optWarp;
        internal RadioButton optBlocked;
        internal Label Label1;
        internal Button btnClearAttribute;
        internal Panel pnlAttributes;
        internal GroupBox fraMapWarp;
        internal Label lblMapWarpY;
        internal Label lblMapWarpX;
        internal Label lblMapWarpMap;
        internal HScrollBar scrlMapWarpY;
        internal HScrollBar scrlMapWarpX;
        internal HScrollBar scrlMapWarpMap;
        internal Button btnMapWarp;
        internal GroupBox fraMapItem;
        internal Button btnMapItem;
        internal HScrollBar scrlMapItemValue;
        internal HScrollBar scrlMapItem;
        internal Label lblMapItem;
        internal PictureBox picMapItem;
        internal GroupBox fraResource;
        internal Button btnResourceOk;
        internal HScrollBar scrlResource;
        internal Label lblResource;
        internal GroupBox fraNPCSpawn;
        internal Button btnNPCSpawn;
        internal HScrollBar scrlNPCDir;
        internal Label lblNPCDir;
        internal ComboBox lstNPC;
        internal GroupBox fraShop;
        internal ComboBox cmbShop;
        internal Button btnShop;
        internal GroupBox fraHeal;
        internal Label lblHeal;
        internal ComboBox cmbHeal;
        internal Button btnHeal;
        internal HScrollBar scrlHeal;
        internal GroupBox fraTrap;
        internal Button btnTrap;
        internal HScrollBar scrlTrap;
        internal Label lblTrap;
        internal ToolStrip ToolStrip;
        internal ToolStripButton tsbSave;
        internal ToolStripButton tsbDiscard;
        internal TabControl tabpages;
        internal TabPage tpTiles;
        internal TabPage tpNPCs;
        internal TabPage tpSettings;
        internal GroupBox fraNPCs;
        internal ComboBox ComboBox23;
        internal TextBox txtName;
        internal Label Label6;
        internal GroupBox fraMapLinks;
        internal TextBox txtDown;
        internal TextBox txtLeft;
        internal Label lblMap;
        internal TextBox txtRight;
        internal TextBox txtUp;
        internal GroupBox fraBootSettings;
        internal TextBox txtBootMap;
        internal Label Label5;
        internal TextBox txtBootY;
        internal Label Label3;
        internal TextBox txtBootX;
        internal Label Label4;
        internal GroupBox fraMaxSizes;
        internal TextBox txtMaxY;
        internal Label Label2;
        internal TextBox txtMaxX;
        internal Label Label7;
        internal GroupBox GroupBox2;
        internal ListBox lstMusic;
        internal GroupBox fraMapSettings;
        internal Label Label8;
        internal ComboBox lstMoral;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ComboBox cmbNPCList;
        internal ListBox lstMapNPC;
        internal TabPage tpAttributes;
        internal ComboBox cmbTileSets;
        internal ComboBox cmbAutoTile;
        internal Label Label11;
        internal Label Label10;
        internal ComboBox cmbLayers;
        internal Label Label9;
        internal TabPage tpDirBlock;
        internal TabPage tpEvents;
        internal Label Label12;
        internal Label Label13;
        internal ToolStripButton tsbMapGrid;
        internal Button btnPreview;
        internal ToolStripButton tsbFill;
        internal ToolStripButton tsbEyeDropper;
        internal ToolStripSeparator ToolStripSeparator2;
        internal Button btnPasteEvent;
        internal Label Label16;
        internal Button btnCopyEvent;
        internal Label Label15;
        internal Label lblPasteMode;
        internal Label lblCopyMode;
        internal TabPage tpEffects;
        internal GroupBox GroupBox3;
        internal CheckBox chkTint;
        internal Label lblMapAlpha;
        internal Label lblMapBlue;
        internal Label lblMapGreen;
        internal Label lblMapRed;
        internal HScrollBar scrlMapAlpha;
        internal HScrollBar scrlMapBlue;
        internal HScrollBar scrlMapGreen;
        internal HScrollBar scrlMapRed;
        internal GroupBox GroupBox1;
        internal HScrollBar scrlFogOpacity;
        internal Label lblFogOpacity;
        internal HScrollBar scrlFogSpeed;
        internal Label lblFogSpeed;
        internal HScrollBar scrlIntensity;
        internal Label lblIntensity;
        internal HScrollBar scrlFog;
        internal Label lblFogIndex;
        internal Label Label14;
        internal ComboBox cmbWeather;
        internal Label Label18;
        internal Label Label17;
        internal GroupBox GroupBox5;
        internal Label Label20;
        internal ComboBox cmbParallax;
        internal GroupBox GroupBox4;
        internal Label Label19;
        internal ComboBox cmbPanorama;
        internal GroupBox GroupBox6;
        internal Label lblMapBrightness;
        internal HScrollBar scrlMapBrightness;
        internal PictureBox picBackSelect;
        internal ToolStripButton tsbClear;
        internal ToolStripButton tsbCopyMap;
        internal ToolStripButton tsbUndo;
        internal ToolStripButton tsbRedo;
        internal ToolStripButton tsbOpacity;
        internal ToolStripButton tsbScreenshot;
        internal RadioButton optAnimation;
        internal GroupBox fraAnimation;
        internal ComboBox cmbAnimation;
        internal Button brnAnimation;
        internal GroupBox fraMapLight;
        internal Button btnLight;
        internal HScrollBar scrlLight;
        internal CheckBox chkShadow;
        internal CheckBox chkFlicker;
        internal Label Label21;
        internal Label lblRadius;
        internal Label Label22;
        internal ComboBox lstShop;
        internal CheckBox chkNoMapRespawn;
        internal CheckBox chkIndoors;
        internal Label Label23;
        internal ComboBox cmbAttribute;
        internal ToolStripButton tsbDeleteMap;
        internal RadioButton optInfo;
        internal Button btnFillAttributes;
        internal RadioButton optNoXing;
    }
}