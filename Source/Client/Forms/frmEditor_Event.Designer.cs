using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_Event : Form
    {

        // Shared instance of the form
        private static frmEditor_Event _instance;

        // Public property to get the shared instance
        public static frmEditor_Event Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Event();
                }
                return _instance;
            }
        }

        // Private constructor to prevent instantiation from outside
        private frmEditor_Event()
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
            var TreeNode1 = new TreeNode("Show Text");
            var TreeNode2 = new TreeNode("Show Choices");
            var TreeNode3 = new TreeNode("Add Chatbox Text");
            var TreeNode4 = new TreeNode("Show ChatBubble");
            var TreeNode5 = new TreeNode("Messages", new TreeNode[] { TreeNode1, TreeNode2, TreeNode3, TreeNode4 });
            var TreeNode6 = new TreeNode("Set Player Variable");
            var TreeNode7 = new TreeNode("Set Player Switch");
            var TreeNode8 = new TreeNode("Set Self Switch");
            var TreeNode9 = new TreeNode("Event Processing", new TreeNode[] { TreeNode6, TreeNode7, TreeNode8 });
            var TreeNode10 = new TreeNode("Conditional Branch");
            var TreeNode11 = new TreeNode("Stop Event Processing");
            var TreeNode12 = new TreeNode("Label");
            var TreeNode13 = new TreeNode("GoTo Label");
            var TreeNode14 = new TreeNode("Flow Control", new TreeNode[] { TreeNode10, TreeNode11, TreeNode12, TreeNode13 });
            var TreeNode15 = new TreeNode("Change Items");
            var TreeNode16 = new TreeNode("Restore HP");
            var TreeNode17 = new TreeNode("Restore MP");
            var TreeNode18 = new TreeNode("Level Up");
            var TreeNode19 = new TreeNode("Change Level");
            var TreeNode20 = new TreeNode("Change Skills");
            var TreeNode21 = new TreeNode("Change Job");
            var TreeNode22 = new TreeNode("Change Sprite");
            var TreeNode23 = new TreeNode("Change Gender");
            var TreeNode24 = new TreeNode("Change PK");
            var TreeNode25 = new TreeNode("Give Experience");
            var TreeNode26 = new TreeNode("Player Options", new TreeNode[] { TreeNode15, TreeNode16, TreeNode17, TreeNode18, TreeNode19, TreeNode20, TreeNode21, TreeNode22, TreeNode23, TreeNode24, TreeNode25 });
            var TreeNode27 = new TreeNode("Warp Player");
            var TreeNode28 = new TreeNode("Set Move Route");
            var TreeNode29 = new TreeNode("Wait for Route Completion");
            var TreeNode30 = new TreeNode("Force Spawn NPC");
            var TreeNode31 = new TreeNode("Hold Player");
            var TreeNode32 = new TreeNode("Release Player");
            var TreeNode33 = new TreeNode("Movement", new TreeNode[] { TreeNode27, TreeNode28, TreeNode29, TreeNode30, TreeNode31, TreeNode32 });
            var TreeNode34 = new TreeNode("Animation");
            var TreeNode35 = new TreeNode("Animation", new TreeNode[] { TreeNode34 });
            var TreeNode36 = new TreeNode("Begin Quest");
            var TreeNode37 = new TreeNode("Complete Task");
            var TreeNode38 = new TreeNode("End Quest");
            var TreeNode39 = new TreeNode("Questing", new TreeNode[] { TreeNode36, TreeNode37, TreeNode38 });
            var TreeNode40 = new TreeNode("Set Fog");
            var TreeNode41 = new TreeNode("Set Weather");
            var TreeNode42 = new TreeNode("Set Map Tinting");
            var TreeNode43 = new TreeNode("Map Functions", new TreeNode[] { TreeNode40, TreeNode41, TreeNode42 });
            var TreeNode44 = new TreeNode("Play BGM");
            var TreeNode45 = new TreeNode("Stop BGM");
            var TreeNode46 = new TreeNode("Play Sound");
            var TreeNode47 = new TreeNode("Stop Sounds");
            var TreeNode48 = new TreeNode("Music and Sound", new TreeNode[] { TreeNode44, TreeNode45, TreeNode46, TreeNode47 });
            var TreeNode49 = new TreeNode("Wait...");
            var TreeNode50 = new TreeNode("Set Access");
            var TreeNode51 = new TreeNode("Custom Script");
            var TreeNode52 = new TreeNode("Etc...", new TreeNode[] { TreeNode49, TreeNode50, TreeNode51 });
            var TreeNode53 = new TreeNode("Open Bank");
            var TreeNode54 = new TreeNode("Open Shop");
            var TreeNode55 = new TreeNode("Shop and Bank", new TreeNode[] { TreeNode53, TreeNode54 });
            var TreeNode56 = new TreeNode("Fade In");
            var TreeNode57 = new TreeNode("Fade Out");
            var TreeNode58 = new TreeNode("Flash White");
            var TreeNode59 = new TreeNode("Show Picture");
            var TreeNode60 = new TreeNode("Hide Picture");
            var TreeNode61 = new TreeNode("Cutscene Options", new TreeNode[] { TreeNode56, TreeNode57, TreeNode58, TreeNode59, TreeNode60 });
            var ListViewGroup1 = new ListViewGroup("Movement", HorizontalAlignment.Left);
            var ListViewGroup2 = new ListViewGroup("Wait", HorizontalAlignment.Left);
            var ListViewGroup3 = new ListViewGroup("Turning", HorizontalAlignment.Left);
            var ListViewGroup4 = new ListViewGroup("Speed", HorizontalAlignment.Left);
            var ListViewGroup5 = new ListViewGroup("Walk Animation", HorizontalAlignment.Left);
            var ListViewGroup6 = new ListViewGroup("Fixed Direction", HorizontalAlignment.Left);
            var ListViewGroup7 = new ListViewGroup("WalkThrough", HorizontalAlignment.Left);
            var ListViewGroup8 = new ListViewGroup("Set Position", HorizontalAlignment.Left);
            var ListViewGroup9 = new ListViewGroup("Set Graphic", HorizontalAlignment.Left);
            var ListViewItem1 = new ListViewItem("Move Up");
            var ListViewItem2 = new ListViewItem("Move Down");
            var ListViewItem3 = new ListViewItem("Move left");
            var ListViewItem4 = new ListViewItem("Move Right");
            var ListViewItem5 = new ListViewItem("Move Randomly");
            var ListViewItem6 = new ListViewItem("Move To Player***");
            var ListViewItem7 = new ListViewItem("Move from Player***");
            var ListViewItem8 = new ListViewItem("Step Forwards");
            var ListViewItem9 = new ListViewItem("Step Backwards");
            var ListViewItem10 = new ListViewItem("Wait 100Ms");
            var ListViewItem11 = new ListViewItem("Wait 500Ms");
            var ListViewItem12 = new ListViewItem("Wait 1000Ms");
            var ListViewItem13 = new ListViewItem("Turn Up");
            var ListViewItem14 = new ListViewItem("Turn Down");
            var ListViewItem15 = new ListViewItem("Turn Left");
            var ListViewItem16 = new ListViewItem("Turn Right");
            var ListViewItem17 = new ListViewItem("Turn 90DG Right");
            var ListViewItem18 = new ListViewItem("Turn 90DG Left");
            var ListViewItem19 = new ListViewItem("Turn 180DG");
            var ListViewItem20 = new ListViewItem("Turn Randomly");
            var ListViewItem21 = new ListViewItem("Turn To Player***");
            var ListViewItem22 = new ListViewItem("Turn From Player***");
            var ListViewItem23 = new ListViewItem("Set Speed 8x Slower");
            var ListViewItem24 = new ListViewItem("Set Speed 4x Slower");
            var ListViewItem25 = new ListViewItem("Set Speed 2x Slower");
            var ListViewItem26 = new ListViewItem("Set Speed To Normal");
            var ListViewItem27 = new ListViewItem("Set Speed 2x Faster");
            var ListViewItem28 = new ListViewItem("Set Speed 4x Faster");
            var ListViewItem29 = new ListViewItem("Set Freq. To Lowest");
            var ListViewItem30 = new ListViewItem("Set Freq. To Lower");
            var ListViewItem31 = new ListViewItem("Set Freq. To Normal");
            var ListViewItem32 = new ListViewItem("Set Freq. To Higher");
            var ListViewItem33 = new ListViewItem("Set Freq. To Highest");
            var ListViewItem34 = new ListViewItem("Walking Animation ON");
            var ListViewItem35 = new ListViewItem("Walking Animation OFF");
            var ListViewItem36 = new ListViewItem("Fixed Direction ON");
            var ListViewItem37 = new ListViewItem("Fixed Direction OFF");
            var ListViewItem38 = new ListViewItem("Walkthrough ON");
            var ListViewItem39 = new ListViewItem("Walkthrough ON");
            var ListViewItem40 = new ListViewItem("Set Position Below Player");
            var ListViewItem41 = new ListViewItem("Set PositionWith Player");
            var ListViewItem42 = new ListViewItem("Set Position Above Player");
            var ListViewItem43 = new ListViewItem("Set Graphic...");
            tvCommands = new TreeView();
            tvCommands.AfterSelect += new TreeViewEventHandler(TvCommands_AfterSelect);
            fraPageSetUp = new DarkUI.Controls.DarkGroupBox();
            chkGlobal = new DarkUI.Controls.DarkCheckBox();
            chkGlobal.CheckedChanged += new EventHandler(ChkGlobal_CheckedChanged);
            btnClearPage = new DarkUI.Controls.DarkButton();
            btnClearPage.Click += new EventHandler(BtnClearPage_Click);
            btnDeletePage = new DarkUI.Controls.DarkButton();
            btnDeletePage.Click += new EventHandler(BtnDeletePage_Click);
            btnPastePage = new DarkUI.Controls.DarkButton();
            btnPastePage.Click += new EventHandler(BtnPastePage_Click);
            btnCopyPage = new DarkUI.Controls.DarkButton();
            btnCopyPage.Click += new EventHandler(BtnCopyPage_Click);
            btnNewPage = new DarkUI.Controls.DarkButton();
            btnNewPage.Click += new EventHandler(BtnNewPage_Click);
            txtName = new DarkUI.Controls.DarkTextBox();
            txtName.TextChanged += new EventHandler(TxtName_TextChanged);
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            tabPages = new TabControl();
            tabPages.Click += new EventHandler(TabPages_Click);
            TabPage1 = new TabPage();
            pnlTabPage = new Panel();
            DarkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            cmbPositioning = new DarkUI.Controls.DarkComboBox();
            cmbPositioning.SelectedIndexChanged += new EventHandler(CmbPositioning_SelectedIndexChanged);
            fraGraphicPic = new DarkUI.Controls.DarkGroupBox();
            picGraphic = new PictureBox();
            picGraphic.Click += new EventHandler(PicGraphic_Click);
            DarkGroupBox6 = new DarkUI.Controls.DarkGroupBox();
            chkShowName = new DarkUI.Controls.DarkCheckBox();
            chkShowName.CheckedChanged += new EventHandler(ChkShowName_CheckedChanged);
            chkWalkThrough = new DarkUI.Controls.DarkCheckBox();
            chkWalkThrough.CheckedChanged += new EventHandler(ChkWalkThrough_CheckedChanged);
            chkDirFix = new DarkUI.Controls.DarkCheckBox();
            chkDirFix.CheckedChanged += new EventHandler(ChkDirFix_CheckedChanged);
            chkWalkAnim = new DarkUI.Controls.DarkCheckBox();
            chkWalkAnim.CheckedChanged += new EventHandler(ChkWalkAnim_CheckedChanged);
            DarkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            cmbTrigger = new DarkUI.Controls.DarkComboBox();
            cmbTrigger.SelectedIndexChanged += new EventHandler(CmbTrigger_SelectedIndexChanged);
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            picGraphicSel = new PictureBox();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            cmbMoveFreq = new DarkUI.Controls.DarkComboBox();
            cmbMoveFreq.SelectedIndexChanged += new EventHandler(CmbMoveFreq_SelectedIndexChanged);
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            cmbMoveSpeed = new DarkUI.Controls.DarkComboBox();
            cmbMoveSpeed.SelectedIndexChanged += new EventHandler(CmbMoveSpeed_SelectedIndexChanged);
            btnMoveRoute = new DarkUI.Controls.DarkButton();
            btnMoveRoute.Click += new EventHandler(BtnMoveRoute_Click);
            cmbMoveType = new DarkUI.Controls.DarkComboBox();
            cmbMoveType.SelectedIndexChanged += new EventHandler(CmbMoveType_SelectedIndexChanged);
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            cmbSelfSwitchCompare = new DarkUI.Controls.DarkComboBox();
            cmbSelfSwitchCompare.SelectedIndexChanged += new EventHandler(CmbSelfSwitchCompare_SelectedIndexChanged);
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            cmbSelfSwitch = new DarkUI.Controls.DarkComboBox();
            cmbSelfSwitch.SelectedIndexChanged += new EventHandler(CmbSelfSwitch_SelectedIndexChanged);
            chkSelfSwitch = new DarkUI.Controls.DarkCheckBox();
            chkSelfSwitch.CheckedChanged += new EventHandler(ChkSelfSwitch_CheckedChanged);
            cmbHasItem = new DarkUI.Controls.DarkComboBox();
            cmbHasItem.SelectedIndexChanged += new EventHandler(CmbHasItem_SelectedIndexChanged);
            chkHasItem = new DarkUI.Controls.DarkCheckBox();
            chkHasItem.CheckedChanged += new EventHandler(ChkHasItem_CheckedChanged);
            cmbPlayerSwitchCompare = new DarkUI.Controls.DarkComboBox();
            cmbPlayerSwitchCompare.SelectedIndexChanged += new EventHandler(CmbPlayerSwitchCompare_SelectedIndexChanged);
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            cmbPlayerSwitch = new DarkUI.Controls.DarkComboBox();
            cmbPlayerSwitch.SelectedIndexChanged += new EventHandler(CmbPlayerSwitch_SelectedIndexChanged);
            chkPlayerSwitch = new DarkUI.Controls.DarkCheckBox();
            chkPlayerSwitch.CheckedChanged += new EventHandler(ChkPlayerSwitch_CheckedChanged);
            nudPlayerVariable = new DarkUI.Controls.DarkNumericUpDown();
            nudPlayerVariable.ValueChanged += new EventHandler(NudPlayerVariable_ValueChanged);
            cmbPlayervarCompare = new DarkUI.Controls.DarkComboBox();
            cmbPlayervarCompare.SelectedIndexChanged += new EventHandler(CmbPlayervarCompare_SelectedIndexChanged);
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            cmbPlayerVar = new DarkUI.Controls.DarkComboBox();
            cmbPlayerVar.SelectedIndexChanged += new EventHandler(CmbPlayerVar_SelectedIndexChanged);
            chkPlayerVar = new DarkUI.Controls.DarkCheckBox();
            chkPlayerVar.CheckedChanged += new EventHandler(ChkPlayerVar_CheckedChanged);
            DarkGroupBox8 = new DarkUI.Controls.DarkGroupBox();
            btnClearCommand = new DarkUI.Controls.DarkButton();
            btnClearCommand.Click += new EventHandler(BtnClearCommand_Click);
            btnDeleteCommand = new DarkUI.Controls.DarkButton();
            btnDeleteCommand.Click += new EventHandler(BtnDeleteComand_Click);
            btnEditCommand = new DarkUI.Controls.DarkButton();
            btnEditCommand.Click += new EventHandler(BtnEditCommand_Click);
            btnAddCommand = new DarkUI.Controls.DarkButton();
            btnAddCommand.Click += new EventHandler(BtnAddCommand_Click);
            fraGraphic = new DarkUI.Controls.DarkGroupBox();
            btnGraphicOk = new DarkUI.Controls.DarkButton();
            btnGraphicCancel = new DarkUI.Controls.DarkButton();
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            nudGraphic = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            cmbGraphic = new DarkUI.Controls.DarkComboBox();
            cmbGraphic.SelectedIndexChanged += new EventHandler(CmbGraphic_SelectedIndexChanged);
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            fraCommands = new Panel();
            btnCancelCommand = new DarkUI.Controls.DarkButton();
            btnCancelCommand.Click += new EventHandler(BtnCancelCommand_Click);
            lstCommands = new ListBox();
            lstCommands.SelectedIndexChanged += new EventHandler(LstCommands_SelectedIndexChanged);
            btnLabeling = new DarkUI.Controls.DarkButton();
            btnLabeling.Click += new EventHandler(BtnLabeling_Click);
            btnCancel = new DarkUI.Controls.DarkButton();
            btnCancel.Click += new EventHandler(BtnCancel_Click);
            btnOk = new DarkUI.Controls.DarkButton();
            btnOk.Click += new EventHandler(BtnOK_Click);
            fraMoveRoute = new DarkUI.Controls.DarkGroupBox();
            btnMoveRouteOk = new DarkUI.Controls.DarkButton();
            btnMoveRouteOk.Click += new EventHandler(BtnMoveRouteOk_Click);
            btnMoveRouteCancel = new DarkUI.Controls.DarkButton();
            btnMoveRouteCancel.Click += new EventHandler(BtnMoveRouteCancel_Click);
            chkRepeatRoute = new DarkUI.Controls.DarkCheckBox();
            chkRepeatRoute.CheckedChanged += new EventHandler(ChkRepeatRoute_CheckedChanged);
            chkIgnoreMove = new DarkUI.Controls.DarkCheckBox();
            chkIgnoreMove.CheckedChanged += new EventHandler(ChkIgnoreMove_CheckedChanged);
            DarkGroupBox10 = new DarkUI.Controls.DarkGroupBox();
            lstvwMoveRoute = new ListView();
            lstvwMoveRoute.Click += new EventHandler(LstvwMoveRoute_SelectedIndexChanged);
            ColumnHeader3 = new ColumnHeader();
            ColumnHeader4 = new ColumnHeader();
            lstMoveRoute = new ListBox();
            lstMoveRoute.KeyDown += new KeyEventHandler(LstMoveRoute_KeyDown);
            cmbEvent = new DarkUI.Controls.DarkComboBox();
            pnlGraphicSel = new Panel();
            fraDialogue = new DarkUI.Controls.DarkGroupBox();
            fraShowChatBubble = new DarkUI.Controls.DarkGroupBox();
            btnShowChatBubbleOk = new DarkUI.Controls.DarkButton();
            btnShowChatBubbleOk.Click += new EventHandler(BtnShowChatBubbleOK_Click);
            btnShowChatBubbleCancel = new DarkUI.Controls.DarkButton();
            btnShowChatBubbleCancel.Click += new EventHandler(BtnShowChatBubbleCancel_Click);
            DarkLabel41 = new DarkUI.Controls.DarkLabel();
            cmbChatBubbleTarget = new DarkUI.Controls.DarkComboBox();
            cmbChatBubbleTargetType = new DarkUI.Controls.DarkComboBox();
            cmbChatBubbleTargetType.SelectedIndexChanged += new EventHandler(CmbChatBubbleTargetType_SelectedIndexChanged);
            DarkLabel40 = new DarkUI.Controls.DarkLabel();
            txtChatbubbleText = new DarkUI.Controls.DarkTextBox();
            DarkLabel39 = new DarkUI.Controls.DarkLabel();
            fraOpenShop = new DarkUI.Controls.DarkGroupBox();
            btnOpenShopOk = new DarkUI.Controls.DarkButton();
            btnOpenShopOk.Click += new EventHandler(BtnOpenShopOK_Click);
            btnOpenShopCancel = new DarkUI.Controls.DarkButton();
            btnOpenShopCancel.Click += new EventHandler(BtnOpenShopCancel_Click);
            cmbOpenShop = new DarkUI.Controls.DarkComboBox();
            fraSetSelfSwitch = new DarkUI.Controls.DarkGroupBox();
            btnSelfswitchOk = new DarkUI.Controls.DarkButton();
            btnSelfswitchOk.Click += new EventHandler(BtnSelfswitchOk_Click);
            btnSelfswitchCancel = new DarkUI.Controls.DarkButton();
            btnSelfswitchCancel.Click += new EventHandler(BtnSelfswitchCancel_Click);
            DarkLabel47 = new DarkUI.Controls.DarkLabel();
            cmbSetSelfSwitchTo = new DarkUI.Controls.DarkComboBox();
            DarkLabel46 = new DarkUI.Controls.DarkLabel();
            cmbSetSelfSwitch = new DarkUI.Controls.DarkComboBox();
            fraPlaySound = new DarkUI.Controls.DarkGroupBox();
            btnPlaySoundOk = new DarkUI.Controls.DarkButton();
            btnPlaySoundOk.Click += new EventHandler(BtnPlaySoundOK_Click);
            btnPlaySoundCancel = new DarkUI.Controls.DarkButton();
            btnPlaySoundCancel.Click += new EventHandler(BtnPlaySoundCancel_Click);
            cmbPlaySound = new DarkUI.Controls.DarkComboBox();
            fraChangePK = new DarkUI.Controls.DarkGroupBox();
            btnChangePkOk = new DarkUI.Controls.DarkButton();
            btnChangePkOk.Click += new EventHandler(BtnChangePkOK_Click);
            btnChangePkCancel = new DarkUI.Controls.DarkButton();
            btnChangePkCancel.Click += new EventHandler(BtnChangePkCancel_Click);
            cmbSetPK = new DarkUI.Controls.DarkComboBox();
            fraCreateLabel = new DarkUI.Controls.DarkGroupBox();
            btnCreatelabelOk = new DarkUI.Controls.DarkButton();
            btnCreatelabelOk.Click += new EventHandler(BtnCreatelabelOk_Click);
            btnCreatelabelCancel = new DarkUI.Controls.DarkButton();
            btnCreatelabelCancel.Click += new EventHandler(BtnCreateLabelCancel_Click);
            txtLabelName = new DarkUI.Controls.DarkTextBox();
            lblLabelName = new DarkUI.Controls.DarkLabel();
            fraChangeJob = new DarkUI.Controls.DarkGroupBox();
            btnChangeJobOk = new DarkUI.Controls.DarkButton();
            btnChangeJobOk.Click += new EventHandler(BtnChangeJobOK_Click);
            btnChangeJobCancel = new DarkUI.Controls.DarkButton();
            btnChangeJobCancel.Click += new EventHandler(BtnChangeJobCancel_Click);
            cmbChangeJob = new DarkUI.Controls.DarkComboBox();
            DarkLabel38 = new DarkUI.Controls.DarkLabel();
            fraChangeSkills = new DarkUI.Controls.DarkGroupBox();
            btnChangeSkillsOk = new DarkUI.Controls.DarkButton();
            btnChangeSkillsOk.Click += new EventHandler(BtnChangeSkillsOK_Click);
            btnChangeSkillsCancel = new DarkUI.Controls.DarkButton();
            btnChangeSkillsCancel.Click += new EventHandler(BtnChangeSkillsCancel_Click);
            optChangeSkillsRemove = new DarkUI.Controls.DarkRadioButton();
            optChangeSkillsAdd = new DarkUI.Controls.DarkRadioButton();
            cmbChangeSkills = new DarkUI.Controls.DarkComboBox();
            DarkLabel37 = new DarkUI.Controls.DarkLabel();
            fraPlayerSwitch = new DarkUI.Controls.DarkGroupBox();
            btnSetPlayerSwitchOk = new DarkUI.Controls.DarkButton();
            btnSetPlayerSwitchOk.Click += new EventHandler(BtnSetPlayerSwitchOk_Click);
            btnSetPlayerswitchCancel = new DarkUI.Controls.DarkButton();
            btnSetPlayerswitchCancel.Click += new EventHandler(BtnSetPlayerswitchCancel_Click);
            cmbPlayerSwitchSet = new DarkUI.Controls.DarkComboBox();
            DarkLabel23 = new DarkUI.Controls.DarkLabel();
            cmbSwitch = new DarkUI.Controls.DarkComboBox();
            DarkLabel22 = new DarkUI.Controls.DarkLabel();
            fraSetWait = new DarkUI.Controls.DarkGroupBox();
            btnSetWaitOk = new DarkUI.Controls.DarkButton();
            btnSetWaitOk.Click += new EventHandler(BtnSetWaitOK_Click);
            btnSetWaitCancel = new DarkUI.Controls.DarkButton();
            btnSetWaitCancel.Click += new EventHandler(BtnSetWaitCancel_Click);
            DarkLabel74 = new DarkUI.Controls.DarkLabel();
            DarkLabel72 = new DarkUI.Controls.DarkLabel();
            DarkLabel73 = new DarkUI.Controls.DarkLabel();
            nudWaitAmount = new DarkUI.Controls.DarkNumericUpDown();
            fraMoveRouteWait = new DarkUI.Controls.DarkGroupBox();
            btnMoveWaitCancel = new DarkUI.Controls.DarkButton();
            btnMoveWaitCancel.Click += new EventHandler(BtnMoveWaitCancel_Click);
            btnMoveWaitOk = new DarkUI.Controls.DarkButton();
            btnMoveWaitOk.Click += new EventHandler(BtnMoveWaitOK_Click);
            DarkLabel79 = new DarkUI.Controls.DarkLabel();
            cmbMoveWait = new DarkUI.Controls.DarkComboBox();
            fraCustomScript = new DarkUI.Controls.DarkGroupBox();
            nudCustomScript = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel78 = new DarkUI.Controls.DarkLabel();
            btnCustomScriptCancel = new DarkUI.Controls.DarkButton();
            btnCustomScriptCancel.Click += new EventHandler(BtnCustomScriptCancel_Click);
            btnCustomScriptOk = new DarkUI.Controls.DarkButton();
            btnCustomScriptOk.Click += new EventHandler(BtnCustomScriptOK_Click);
            fraSpawnNPC = new DarkUI.Controls.DarkGroupBox();
            btnSpawnNPCOk = new DarkUI.Controls.DarkButton();
            btnSpawnNPCOk.Click += new EventHandler(BtnSpawnNPCOK_Click);
            btnSpawnNPCancel = new DarkUI.Controls.DarkButton();
            btnSpawnNPCancel.Click += new EventHandler(BtnSpawnNPCancel_Click);
            cmbSpawnNPC = new DarkUI.Controls.DarkComboBox();
            fraSetWeather = new DarkUI.Controls.DarkGroupBox();
            btnSetWeatherOk = new DarkUI.Controls.DarkButton();
            btnSetWeatherOk.Click += new EventHandler(BtnSetWeatherOK_Click);
            btnSetWeatherCancel = new DarkUI.Controls.DarkButton();
            btnSetWeatherCancel.Click += new EventHandler(BtnSetWeatherCancel_Click);
            DarkLabel76 = new DarkUI.Controls.DarkLabel();
            nudWeatherIntensity = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel75 = new DarkUI.Controls.DarkLabel();
            CmbWeather = new DarkUI.Controls.DarkComboBox();
            fraGiveExp = new DarkUI.Controls.DarkGroupBox();
            btnGiveExpOk = new DarkUI.Controls.DarkButton();
            btnGiveExpOk.Click += new EventHandler(BtnGiveExpOK_Click);
            btnGiveExpCancel = new DarkUI.Controls.DarkButton();
            btnGiveExpCancel.Click += new EventHandler(BtnGiveExpCancel_Click);
            nudGiveExp = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel77 = new DarkUI.Controls.DarkLabel();
            fraSetAccess = new DarkUI.Controls.DarkGroupBox();
            btnSetAccessOk = new DarkUI.Controls.DarkButton();
            btnSetAccessOk.Click += new EventHandler(BtnSetAccessOK_Click);
            btnSetAccessCancel = new DarkUI.Controls.DarkButton();
            btnSetAccessCancel.Click += new EventHandler(BtnSetAccessCancel_Click);
            cmbSetAccess = new DarkUI.Controls.DarkComboBox();
            fraChangeGender = new DarkUI.Controls.DarkGroupBox();
            btnChangeGenderOk = new DarkUI.Controls.DarkButton();
            btnChangeGenderOk.Click += new EventHandler(BtnChangeGenderOK_Click);
            btnChangeGenderCancel = new DarkUI.Controls.DarkButton();
            btnChangeGenderCancel.Click += new EventHandler(BtnChangeGenderCancel_Click);
            optChangeSexFemale = new DarkUI.Controls.DarkRadioButton();
            optChangeSexMale = new DarkUI.Controls.DarkRadioButton();
            fraShowChoices = new DarkUI.Controls.DarkGroupBox();
            txtChoices4 = new DarkUI.Controls.DarkTextBox();
            txtChoices3 = new DarkUI.Controls.DarkTextBox();
            txtChoices2 = new DarkUI.Controls.DarkTextBox();
            txtChoices1 = new DarkUI.Controls.DarkTextBox();
            DarkLabel56 = new DarkUI.Controls.DarkLabel();
            DarkLabel57 = new DarkUI.Controls.DarkLabel();
            DarkLabel55 = new DarkUI.Controls.DarkLabel();
            DarkLabel54 = new DarkUI.Controls.DarkLabel();
            DarkLabel52 = new DarkUI.Controls.DarkLabel();
            txtChoicePrompt = new DarkUI.Controls.DarkTextBox();
            btnShowChoicesOk = new DarkUI.Controls.DarkButton();
            btnShowChoicesOk.Click += new EventHandler(BtnShowChoicesOk_Click);
            btnShowChoicesCancel = new DarkUI.Controls.DarkButton();
            btnShowChoicesCancel.Click += new EventHandler(BtnShowChoicesCancel_Click);
            fraChangeLevel = new DarkUI.Controls.DarkGroupBox();
            btnChangeLevelOk = new DarkUI.Controls.DarkButton();
            btnChangeLevelOk.Click += new EventHandler(BtnChangeLevelOK_Click);
            btnChangeLevelCancel = new DarkUI.Controls.DarkButton();
            btnChangeLevelCancel.Click += new EventHandler(BtnChangeLevelCancel_Click);
            DarkLabel65 = new DarkUI.Controls.DarkLabel();
            nudChangeLevel = new DarkUI.Controls.DarkNumericUpDown();
            fraPlayerVariable = new DarkUI.Controls.DarkGroupBox();
            nudVariableData2 = new DarkUI.Controls.DarkNumericUpDown();
            optVariableAction2 = new DarkUI.Controls.DarkRadioButton();
            optVariableAction2.CheckedChanged += new EventHandler(OptVariableAction2_CheckedChanged);
            btnPlayerVarOk = new DarkUI.Controls.DarkButton();
            btnPlayerVarOk.Click += new EventHandler(BtnPlayerVarOk_Click);
            btnPlayerVarCancel = new DarkUI.Controls.DarkButton();
            btnPlayerVarCancel.Click += new EventHandler(BtnPlayerVarCancel_Click);
            DarkLabel51 = new DarkUI.Controls.DarkLabel();
            DarkLabel50 = new DarkUI.Controls.DarkLabel();
            nudVariableData4 = new DarkUI.Controls.DarkNumericUpDown();
            nudVariableData3 = new DarkUI.Controls.DarkNumericUpDown();
            optVariableAction3 = new DarkUI.Controls.DarkRadioButton();
            optVariableAction3.CheckedChanged += new EventHandler(OptVariableAction3_CheckedChanged);
            optVariableAction1 = new DarkUI.Controls.DarkRadioButton();
            optVariableAction1.CheckedChanged += new EventHandler(OptVariableAction1_CheckedChanged);
            nudVariableData1 = new DarkUI.Controls.DarkNumericUpDown();
            nudVariableData0 = new DarkUI.Controls.DarkNumericUpDown();
            optVariableAction0 = new DarkUI.Controls.DarkRadioButton();
            optVariableAction0.CheckedChanged += new EventHandler(OptVariableAction0_CheckedChanged);
            cmbVariable = new DarkUI.Controls.DarkComboBox();
            DarkLabel49 = new DarkUI.Controls.DarkLabel();
            fraPlayAnimation = new DarkUI.Controls.DarkGroupBox();
            btnPlayAnimationOk = new DarkUI.Controls.DarkButton();
            btnPlayAnimationOk.Click += new EventHandler(BtnPlayAnimationOK_Click);
            btnPlayAnimationCancel = new DarkUI.Controls.DarkButton();
            btnPlayAnimationCancel.Click += new EventHandler(BtnPlayAnimationCancel_Click);
            lblPlayAnimY = new DarkUI.Controls.DarkLabel();
            lblPlayAnimX = new DarkUI.Controls.DarkLabel();
            cmbPlayAnimEvent = new DarkUI.Controls.DarkComboBox();
            DarkLabel62 = new DarkUI.Controls.DarkLabel();
            cmbAnimTargetType = new DarkUI.Controls.DarkComboBox();
            nudPlayAnimTileY = new DarkUI.Controls.DarkNumericUpDown();
            nudPlayAnimTileX = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel61 = new DarkUI.Controls.DarkLabel();
            cmbPlayAnim = new DarkUI.Controls.DarkComboBox();
            fraChangeSprite = new DarkUI.Controls.DarkGroupBox();
            btnChangeSpriteOk = new DarkUI.Controls.DarkButton();
            btnChangeSpriteOk.Click += new EventHandler(BtnChangeSpriteOK_Click);
            btnChangeSpriteCancel = new DarkUI.Controls.DarkButton();
            btnChangeSpriteCancel.Click += new EventHandler(BtnChangeSpriteCancel_Click);
            DarkLabel48 = new DarkUI.Controls.DarkLabel();
            nudChangeSprite = new DarkUI.Controls.DarkNumericUpDown();
            picChangeSprite = new PictureBox();
            fraGoToLabel = new DarkUI.Controls.DarkGroupBox();
            btnGoToLabelOk = new DarkUI.Controls.DarkButton();
            btnGoToLabelOk.Click += new EventHandler(BtnGoToLabelOk_Click);
            btnGoToLabelCancel = new DarkUI.Controls.DarkButton();
            btnGoToLabelCancel.Click += new EventHandler(BtnGoToLabelCancel_Click);
            txtGoToLabel = new DarkUI.Controls.DarkTextBox();
            DarkLabel60 = new DarkUI.Controls.DarkLabel();
            fraMapTint = new DarkUI.Controls.DarkGroupBox();
            btnMapTintOk = new DarkUI.Controls.DarkButton();
            btnMapTintOk.Click += new EventHandler(BtnMapTintOK_Click);
            btnMapTintCancel = new DarkUI.Controls.DarkButton();
            btnMapTintCancel.Click += new EventHandler(BtnMapTintCancel_Click);
            DarkLabel42 = new DarkUI.Controls.DarkLabel();
            nudMapTintData3 = new DarkUI.Controls.DarkNumericUpDown();
            nudMapTintData2 = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel43 = new DarkUI.Controls.DarkLabel();
            DarkLabel44 = new DarkUI.Controls.DarkLabel();
            nudMapTintData1 = new DarkUI.Controls.DarkNumericUpDown();
            nudMapTintData0 = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel45 = new DarkUI.Controls.DarkLabel();
            fraShowPic = new DarkUI.Controls.DarkGroupBox();
            btnShowPicOk = new DarkUI.Controls.DarkButton();
            btnShowPicOk.Click += new EventHandler(BtnShowPicOK_Click);
            btnShowPicCancel = new DarkUI.Controls.DarkButton();
            btnShowPicCancel.Click += new EventHandler(BtnShowPicCancel_Click);
            DarkLabel71 = new DarkUI.Controls.DarkLabel();
            DarkLabel70 = new DarkUI.Controls.DarkLabel();
            DarkLabel67 = new DarkUI.Controls.DarkLabel();
            DarkLabel68 = new DarkUI.Controls.DarkLabel();
            nudPicOffsetY = new DarkUI.Controls.DarkNumericUpDown();
            nudPicOffsetX = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel69 = new DarkUI.Controls.DarkLabel();
            cmbPicLoc = new DarkUI.Controls.DarkComboBox();
            nudShowPicture = new DarkUI.Controls.DarkNumericUpDown();
            nudShowPicture.Click += new EventHandler(nudShowPicture_Click);
            picShowPic = new PictureBox();
            fraConditionalBranch = new DarkUI.Controls.DarkGroupBox();
            cmbCondition_Time = new DarkUI.Controls.DarkComboBox();
            optCondition9 = new DarkUI.Controls.DarkRadioButton();
            optCondition9.CheckedChanged += new EventHandler(OptCondition9_CheckedChanged);
            btnConditionalBranchOk = new DarkUI.Controls.DarkButton();
            btnConditionalBranchOk.Click += new EventHandler(BtnConditionalBranchOk_Click);
            btnConditionalBranchCancel = new DarkUI.Controls.DarkButton();
            btnConditionalBranchCancel.Click += new EventHandler(BtnConditionalBranchCancel_Click);
            cmbCondition_Gender = new DarkUI.Controls.DarkComboBox();
            optCondition8 = new DarkUI.Controls.DarkRadioButton();
            optCondition8.CheckedChanged += new EventHandler(OptCondition8_CheckedChanged);
            cmbCondition_SelfSwitchCondition = new DarkUI.Controls.DarkComboBox();
            DarkLabel17 = new DarkUI.Controls.DarkLabel();
            cmbCondition_SelfSwitch = new DarkUI.Controls.DarkComboBox();
            optCondition6 = new DarkUI.Controls.DarkRadioButton();
            optCondition6.CheckedChanged += new EventHandler(OptCondition6_CheckedChanged);
            nudCondition_LevelAmount = new DarkUI.Controls.DarkNumericUpDown();
            optCondition5 = new DarkUI.Controls.DarkRadioButton();
            optCondition5.CheckedChanged += new EventHandler(OptCondition5_CheckedChanged);
            cmbCondition_LevelCompare = new DarkUI.Controls.DarkComboBox();
            cmbCondition_LearntSkill = new DarkUI.Controls.DarkComboBox();
            optCondition4 = new DarkUI.Controls.DarkRadioButton();
            optCondition4.CheckedChanged += new EventHandler(OptCondition4_CheckedChanged);
            cmbCondition_JobIs = new DarkUI.Controls.DarkComboBox();
            optCondition3 = new DarkUI.Controls.DarkRadioButton();
            optCondition3.CheckedChanged += new EventHandler(OptCondition3_CheckedChanged);
            nudCondition_HasItem = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel16 = new DarkUI.Controls.DarkLabel();
            cmbCondition_HasItem = new DarkUI.Controls.DarkComboBox();
            optCondition2 = new DarkUI.Controls.DarkRadioButton();
            optCondition2.CheckedChanged += new EventHandler(OptCondition2_CheckedChanged);
            optCondition1 = new DarkUI.Controls.DarkRadioButton();
            optCondition1.CheckedChanged += new EventHandler(OptCondition1_CheckedChanged);
            DarkLabel15 = new DarkUI.Controls.DarkLabel();
            cmbCondtion_PlayerSwitchCondition = new DarkUI.Controls.DarkComboBox();
            cmbCondition_PlayerSwitch = new DarkUI.Controls.DarkComboBox();
            nudCondition_PlayerVarCondition = new DarkUI.Controls.DarkNumericUpDown();
            cmbCondition_PlayerVarCompare = new DarkUI.Controls.DarkComboBox();
            DarkLabel14 = new DarkUI.Controls.DarkLabel();
            cmbCondition_PlayerVarIndex = new DarkUI.Controls.DarkComboBox();
            optCondition0 = new DarkUI.Controls.DarkRadioButton();
            optCondition0.CheckedChanged += new EventHandler(OptCondition_Index0_CheckedChanged);
            fraPlayBGM = new DarkUI.Controls.DarkGroupBox();
            btnPlayBgmOk = new DarkUI.Controls.DarkButton();
            btnPlayBgmOk.Click += new EventHandler(BtnPlayBgmOK_Click);
            btnPlayBgmCancel = new DarkUI.Controls.DarkButton();
            btnPlayBgmCancel.Click += new EventHandler(BtnPlayBgmCancel_Click);
            cmbPlayBGM = new DarkUI.Controls.DarkComboBox();
            fraPlayerWarp = new DarkUI.Controls.DarkGroupBox();
            btnPlayerWarpOk = new DarkUI.Controls.DarkButton();
            btnPlayerWarpOk.Click += new EventHandler(BtnPlayerWarpOK_Click);
            btnPlayerWarpCancel = new DarkUI.Controls.DarkButton();
            btnPlayerWarpCancel.Click += new EventHandler(BtnPlayerWarpCancel_Click);
            DarkLabel31 = new DarkUI.Controls.DarkLabel();
            cmbWarpPlayerDir = new DarkUI.Controls.DarkComboBox();
            nudWPY = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel32 = new DarkUI.Controls.DarkLabel();
            nudWPX = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel33 = new DarkUI.Controls.DarkLabel();
            nudWPMap = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel34 = new DarkUI.Controls.DarkLabel();
            fraSetFog = new DarkUI.Controls.DarkGroupBox();
            btnSetFogOk = new DarkUI.Controls.DarkButton();
            btnSetFogOk.Click += new EventHandler(BtnSetFogOK_Click);
            btnSetFogCancel = new DarkUI.Controls.DarkButton();
            btnSetFogCancel.Click += new EventHandler(BtnSetFogCancel_Click);
            DarkLabel30 = new DarkUI.Controls.DarkLabel();
            DarkLabel29 = new DarkUI.Controls.DarkLabel();
            DarkLabel28 = new DarkUI.Controls.DarkLabel();
            nudFogData2 = new DarkUI.Controls.DarkNumericUpDown();
            nudFogData1 = new DarkUI.Controls.DarkNumericUpDown();
            nudFogData0 = new DarkUI.Controls.DarkNumericUpDown();
            fraShowText = new DarkUI.Controls.DarkGroupBox();
            DarkLabel27 = new DarkUI.Controls.DarkLabel();
            txtShowText = new DarkUI.Controls.DarkTextBox();
            btnShowTextCancel = new DarkUI.Controls.DarkButton();
            btnShowTextCancel.Click += new EventHandler(BtnShowTextCancel_Click);
            btnShowTextOk = new DarkUI.Controls.DarkButton();
            btnShowTextOk.Click += new EventHandler(BtnShowTextOk_Click);
            fraAddText = new DarkUI.Controls.DarkGroupBox();
            btnAddTextOk = new DarkUI.Controls.DarkButton();
            btnAddTextOk.Click += new EventHandler(BtnAddTextOk_Click);
            btnAddTextCancel = new DarkUI.Controls.DarkButton();
            btnAddTextCancel.Click += new EventHandler(BtnAddTextCancel_Click);
            optAddText_Global = new DarkUI.Controls.DarkRadioButton();
            optAddText_Map = new DarkUI.Controls.DarkRadioButton();
            optAddText_Player = new DarkUI.Controls.DarkRadioButton();
            DarkLabel25 = new DarkUI.Controls.DarkLabel();
            txtAddText_Text = new DarkUI.Controls.DarkTextBox();
            DarkLabel24 = new DarkUI.Controls.DarkLabel();
            fraChangeItems = new DarkUI.Controls.DarkGroupBox();
            btnChangeItemsOk = new DarkUI.Controls.DarkButton();
            btnChangeItemsOk.Click += new EventHandler(BtnChangeItemsOk_Click);
            btnChangeItemsCancel = new DarkUI.Controls.DarkButton();
            btnChangeItemsCancel.Click += new EventHandler(BtnChangeItemsCancel_Click);
            nudChangeItemsAmount = new DarkUI.Controls.DarkNumericUpDown();
            optChangeItemRemove = new DarkUI.Controls.DarkRadioButton();
            optChangeItemAdd = new DarkUI.Controls.DarkRadioButton();
            optChangeItemSet = new DarkUI.Controls.DarkRadioButton();
            cmbChangeItemIndex = new DarkUI.Controls.DarkComboBox();
            DarkLabel21 = new DarkUI.Controls.DarkLabel();
            pnlVariableSwitches = new Panel();
            FraRenaming = new GroupBox();
            btnRename_Cancel = new Button();
            btnRename_Cancel.Click += new EventHandler(BtnRename_Cancel_Click);
            btnRename_Ok = new Button();
            btnRename_Ok.Click += new EventHandler(BtnRename_Ok_Click);
            fraRandom10 = new GroupBox();
            txtRename = new TextBox();
            txtRename.TextChanged += new EventHandler(TxtRename_TextChanged);
            lblEditing = new Label();
            fraLabeling = new DarkUI.Controls.DarkGroupBox();
            lstSwitches = new ListBox();
            lstSwitches.Click += new EventHandler(lstSwitches_Click);
            lstSwitches.DoubleClick += new EventHandler(LstSwitches_DoubleClick);
            lstVariables = new ListBox();
            lstVariables.Click += new EventHandler(lstVariables_Click);
            lstVariables.DoubleClick += new EventHandler(LstVariables_DoubleClick);
            btnLabel_Cancel = new DarkUI.Controls.DarkButton();
            btnLabel_Cancel.Click += new EventHandler(BtnLabel_Cancel_Click);
            lblRandomLabel36 = new DarkUI.Controls.DarkLabel();
            btnRenameVariable = new DarkUI.Controls.DarkButton();
            btnRenameVariable.Click += new EventHandler(BtnRenameVariable_Click);
            lblRandomLabel25 = new DarkUI.Controls.DarkLabel();
            btnRenameSwitch = new DarkUI.Controls.DarkButton();
            btnRenameSwitch.Click += new EventHandler(BtnRenameSwitch_Click);
            btnLabel_Ok = new DarkUI.Controls.DarkButton();
            btnLabel_Ok.Click += new EventHandler(BtnLabel_Ok_Click);
            fraPageSetUp.SuspendLayout();
            tabPages.SuspendLayout();
            pnlTabPage.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            fraGraphicPic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picGraphic).BeginInit();
            DarkGroupBox6.SuspendLayout();
            DarkGroupBox5.SuspendLayout();
            DarkGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picGraphicSel).BeginInit();
            DarkGroupBox3.SuspendLayout();
            DarkGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPlayerVariable).BeginInit();
            DarkGroupBox8.SuspendLayout();
            fraGraphic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudGraphic).BeginInit();
            fraCommands.SuspendLayout();
            fraMoveRoute.SuspendLayout();
            DarkGroupBox10.SuspendLayout();
            fraDialogue.SuspendLayout();
            fraShowChatBubble.SuspendLayout();
            fraOpenShop.SuspendLayout();
            fraSetSelfSwitch.SuspendLayout();
            fraPlaySound.SuspendLayout();
            fraChangePK.SuspendLayout();
            fraCreateLabel.SuspendLayout();
            fraChangeJob.SuspendLayout();
            fraChangeSkills.SuspendLayout();
            fraPlayerSwitch.SuspendLayout();
            fraSetWait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudWaitAmount).BeginInit();
            fraMoveRouteWait.SuspendLayout();
            fraCustomScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCustomScript).BeginInit();
            fraSpawnNPC.SuspendLayout();
            fraSetWeather.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudWeatherIntensity).BeginInit();
            fraGiveExp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiveExp).BeginInit();
            fraSetAccess.SuspendLayout();
            fraChangeGender.SuspendLayout();
            fraShowChoices.SuspendLayout();
            fraChangeLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudChangeLevel).BeginInit();
            fraPlayerVariable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudVariableData2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVariableData4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVariableData3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVariableData1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVariableData0).BeginInit();
            fraPlayAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPlayAnimTileY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPlayAnimTileX).BeginInit();
            fraChangeSprite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudChangeSprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picChangeSprite).BeginInit();
            fraGoToLabel.SuspendLayout();
            fraMapTint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMapTintData3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMapTintData2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMapTintData1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMapTintData0).BeginInit();
            fraShowPic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPicOffsetY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPicOffsetX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudShowPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picShowPic).BeginInit();
            fraConditionalBranch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCondition_LevelAmount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCondition_HasItem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCondition_PlayerVarCondition).BeginInit();
            fraPlayBGM.SuspendLayout();
            fraPlayerWarp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudWPY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWPX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudWPMap).BeginInit();
            fraSetFog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFogData2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudFogData1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudFogData0).BeginInit();
            fraShowText.SuspendLayout();
            fraAddText.SuspendLayout();
            fraChangeItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudChangeItemsAmount).BeginInit();
            pnlVariableSwitches.SuspendLayout();
            FraRenaming.SuspendLayout();
            fraRandom10.SuspendLayout();
            fraLabeling.SuspendLayout();
            SuspendLayout();
            // 
            // tvCommands
            // 
            tvCommands.BackColor = Color.FromArgb(45, 45, 48);
            tvCommands.BorderStyle = BorderStyle.FixedSingle;
            tvCommands.ForeColor = Color.Gainsboro;
            tvCommands.Location = new Point(10, 5);
            tvCommands.Margin = new Padding(5);
            tvCommands.Name = "tvCommands";
            TreeNode1.Name = "Node1";
            TreeNode1.Text = "Show Text";
            TreeNode2.Name = "Node2";
            TreeNode2.Text = "Show Choices";
            TreeNode3.Name = "Node3";
            TreeNode3.Text = "Add Chatbox Text";
            TreeNode4.Name = "Node5";
            TreeNode4.Text = "Show ChatBubble";
            TreeNode5.Name = "NodeMessages";
            TreeNode5.Text = "Messages";
            TreeNode6.Name = "Node1";
            TreeNode6.Text = "Set Player Variable";
            TreeNode7.Name = "Node2";
            TreeNode7.Text = "Set Player Switch";
            TreeNode8.Name = "Node3";
            TreeNode8.Text = "Set Self Switch";
            TreeNode9.Name = "NodeProcessing";
            TreeNode9.Text = "Event Processing";
            TreeNode10.Name = "Node1";
            TreeNode10.Text = "Conditional Branch";
            TreeNode11.Name = "Node2";
            TreeNode11.Text = "Stop Event Processing";
            TreeNode12.Name = "Node3";
            TreeNode12.Text = "Label";
            TreeNode13.Name = "Node4";
            TreeNode13.Text = "GoTo Label";
            TreeNode14.Name = "NodeFlowControl";
            TreeNode14.Text = "Flow Control";
            TreeNode15.Name = "Node1";
            TreeNode15.Text = "Change Items";
            TreeNode16.Name = "Node2";
            TreeNode16.Text = "Restore HP";
            TreeNode17.Name = "Node3";
            TreeNode17.Text = "Restore MP";
            TreeNode18.Name = "Node4";
            TreeNode18.Text = "Level Up";
            TreeNode19.Name = "Node5";
            TreeNode19.Text = "Change Level";
            TreeNode20.Name = "Node6";
            TreeNode20.Text = "Change Skills";
            TreeNode21.Name = "Node7";
            TreeNode21.Text = "Change Job";
            TreeNode22.Name = "Node8";
            TreeNode22.Text = "Change Sprite";
            TreeNode23.Name = "Node9";
            TreeNode23.Text = "Change Gender";
            TreeNode24.Name = "Node10";
            TreeNode24.Text = "Change PK";
            TreeNode25.Name = "Node11";
            TreeNode25.Text = "Give Experience";
            TreeNode26.Name = "NodePlayerOptions";
            TreeNode26.Text = "Player Options";
            TreeNode27.Name = "Node1";
            TreeNode27.Text = "Warp Player";
            TreeNode28.Name = "Node2";
            TreeNode28.Text = "Set Move Route";
            TreeNode29.Name = "Node3";
            TreeNode29.Text = "Wait for Route Completion";
            TreeNode30.Name = "Node4";
            TreeNode30.Text = "Force Spawn NPC";
            TreeNode31.Name = "Node5";
            TreeNode31.Text = "Hold Player";
            TreeNode32.Name = "Node6";
            TreeNode32.Text = "Release Player";
            TreeNode33.Name = "NodeMovement";
            TreeNode33.Text = "Movement";
            TreeNode34.Name = "Node1";
            TreeNode34.Text = "Animation";
            TreeNode35.Name = "NodeAnimation";
            TreeNode35.Text = "Animation";
            TreeNode36.Name = "Node1";
            TreeNode36.Text = "Begin Quest";
            TreeNode37.Name = "Node2";
            TreeNode37.Text = "Complete Task";
            TreeNode38.Name = "Node3";
            TreeNode38.Text = "End Quest";
            TreeNode39.Name = "NodeQuesting";
            TreeNode39.Text = "Questing";
            TreeNode40.Name = "Node1";
            TreeNode40.Text = "Set Fog";
            TreeNode41.Name = "Node2";
            TreeNode41.Text = "Set Weather";
            TreeNode42.Name = "Node3";
            TreeNode42.Text = "Set Map Tinting";
            TreeNode43.Name = "NodeMapFunctions";
            TreeNode43.Text = "Map Functions";
            TreeNode44.Name = "Node1";
            TreeNode44.Text = "Play BGM";
            TreeNode45.Name = "Node2";
            TreeNode45.Text = "Stop BGM";
            TreeNode46.Name = "Node3";
            TreeNode46.Text = "Play Sound";
            TreeNode47.Name = "Node4";
            TreeNode47.Text = "Stop Sounds";
            TreeNode48.Name = "NodeSound";
            TreeNode48.Text = "Music and Sound";
            TreeNode49.Name = "Node1";
            TreeNode49.Text = "Wait...";
            TreeNode50.Name = "Node2";
            TreeNode50.Text = "Set Access";
            TreeNode51.Name = "Node3";
            TreeNode51.Text = "Custom Script";
            TreeNode52.Name = "NodeEtc";
            TreeNode52.Text = "Etc...";
            TreeNode53.Name = "Node1";
            TreeNode53.Text = "Open Bank";
            TreeNode54.Name = "Node2";
            TreeNode54.Text = "Open Shop";
            TreeNode55.Name = "NodeShopBank";
            TreeNode55.Text = "Shop and Bank";
            TreeNode56.Name = "Node1";
            TreeNode56.Text = "Fade In";
            TreeNode57.Name = "Node2";
            TreeNode57.Text = "Fade Out";
            TreeNode58.Name = "Node12";
            TreeNode58.Text = "Flash White";
            TreeNode59.Name = "Node13";
            TreeNode59.Text = "Show Picture";
            TreeNode60.Name = "Node14";
            TreeNode60.Text = "Hide Picture";
            TreeNode61.Name = "Node0";
            TreeNode61.Text = "Cutscene Options";
            tvCommands.Nodes.AddRange(new TreeNode[] { TreeNode5, TreeNode9, TreeNode14, TreeNode26, TreeNode33, TreeNode35, TreeNode39, TreeNode43, TreeNode48, TreeNode52, TreeNode55, TreeNode61 });
            tvCommands.Size = new Size(634, 850);
            tvCommands.TabIndex = 1;
            // 
            // fraPageSetUp
            // 
            fraPageSetUp.BackColor = Color.FromArgb(45, 45, 48);
            fraPageSetUp.BorderColor = Color.FromArgb(90, 90, 90);
            fraPageSetUp.Controls.Add(chkGlobal);
            fraPageSetUp.Controls.Add(btnClearPage);
            fraPageSetUp.Controls.Add(btnDeletePage);
            fraPageSetUp.Controls.Add(btnPastePage);
            fraPageSetUp.Controls.Add(btnCopyPage);
            fraPageSetUp.Controls.Add(btnNewPage);
            fraPageSetUp.Controls.Add(txtName);
            fraPageSetUp.Controls.Add(DarkLabel1);
            fraPageSetUp.ForeColor = Color.Gainsboro;
            fraPageSetUp.Location = new Point(5, 5);
            fraPageSetUp.Margin = new Padding(5);
            fraPageSetUp.Name = "fraPageSetUp";
            fraPageSetUp.Padding = new Padding(5);
            fraPageSetUp.Size = new Size(1318, 97);
            fraPageSetUp.TabIndex = 2;
            fraPageSetUp.TabStop = false;
            fraPageSetUp.Text = "General";
            // 
            // chkGlobal
            // 
            chkGlobal.AutoSize = true;
            chkGlobal.Location = new Point(467, 38);
            chkGlobal.Margin = new Padding(5);
            chkGlobal.Name = "chkGlobal";
            chkGlobal.Size = new Size(137, 29);
            chkGlobal.TabIndex = 7;
            chkGlobal.Text = "Global Event";
            // 
            // btnClearPage
            // 
            btnClearPage.Location = new Point(1178, 30);
            btnClearPage.Margin = new Padding(5);
            btnClearPage.Name = "btnClearPage";
            btnClearPage.Padding = new Padding(8, 10, 8, 10);
            btnClearPage.Size = new Size(125, 45);
            btnClearPage.TabIndex = 6;
            btnClearPage.Text = "Clear Page";
            // 
            // btnDeletePage
            // 
            btnDeletePage.Location = new Point(1037, 30);
            btnDeletePage.Margin = new Padding(5);
            btnDeletePage.Name = "btnDeletePage";
            btnDeletePage.Padding = new Padding(8, 10, 8, 10);
            btnDeletePage.Size = new Size(132, 45);
            btnDeletePage.TabIndex = 5;
            btnDeletePage.Text = "Delete Page";
            // 
            // btnPastePage
            // 
            btnPastePage.Location = new Point(902, 30);
            btnPastePage.Margin = new Padding(5);
            btnPastePage.Name = "btnPastePage";
            btnPastePage.Padding = new Padding(8, 10, 8, 10);
            btnPastePage.Size = new Size(125, 45);
            btnPastePage.TabIndex = 4;
            btnPastePage.Text = "Paste Page";
            // 
            // btnCopyPage
            // 
            btnCopyPage.Location = new Point(767, 30);
            btnCopyPage.Margin = new Padding(5);
            btnCopyPage.Name = "btnCopyPage";
            btnCopyPage.Padding = new Padding(8, 10, 8, 10);
            btnCopyPage.Size = new Size(125, 45);
            btnCopyPage.TabIndex = 3;
            btnCopyPage.Text = "Copy Page";
            // 
            // btnNewPage
            // 
            btnNewPage.Location = new Point(632, 30);
            btnNewPage.Margin = new Padding(5);
            btnNewPage.Name = "btnNewPage";
            btnNewPage.Padding = new Padding(8, 10, 8, 10);
            btnNewPage.Size = new Size(125, 45);
            btnNewPage.TabIndex = 2;
            btnNewPage.Text = "New Page";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(140, 37);
            txtName.Margin = new Padding(5);
            txtName.Name = "txtName";
            txtName.Size = new Size(315, 31);
            txtName.TabIndex = 1;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(15, 40);
            DarkLabel1.Margin = new Padding(5, 0, 5, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(111, 25);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Event Name:";
            // 
            // tabPages
            // 
            tabPages.Controls.Add(TabPage1);
            tabPages.Location = new Point(20, 113);
            tabPages.Margin = new Padding(5);
            tabPages.Name = "tabPages";
            tabPages.SelectedIndex = 0;
            tabPages.Size = new Size(1182, 37);
            tabPages.TabIndex = 3;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.DimGray;
            TabPage1.Location = new Point(4, 34);
            TabPage1.Margin = new Padding(5);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(5);
            TabPage1.Size = new Size(1174, 0);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "1";
            TabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlTabPage
            // 
            pnlTabPage.Controls.Add(DarkGroupBox2);
            pnlTabPage.Controls.Add(fraGraphicPic);
            pnlTabPage.Controls.Add(DarkGroupBox6);
            pnlTabPage.Controls.Add(DarkGroupBox5);
            pnlTabPage.Controls.Add(DarkGroupBox4);
            pnlTabPage.Controls.Add(DarkGroupBox3);
            pnlTabPage.Controls.Add(DarkGroupBox1);
            pnlTabPage.Controls.Add(DarkGroupBox8);
            pnlTabPage.Controls.Add(fraGraphic);
            pnlTabPage.Controls.Add(fraCommands);
            pnlTabPage.Controls.Add(lstCommands);
            pnlTabPage.Location = new Point(5, 155);
            pnlTabPage.Margin = new Padding(5);
            pnlTabPage.Name = "pnlTabPage";
            pnlTabPage.Size = new Size(1318, 955);
            pnlTabPage.TabIndex = 4;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(cmbPositioning);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(305, 734);
            DarkGroupBox2.Margin = new Padding(5);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(5);
            DarkGroupBox2.Size = new Size(333, 95);
            DarkGroupBox2.TabIndex = 15;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Poisition";
            // 
            // cmbPositioning
            // 
            cmbPositioning.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPositioning.FormattingEnabled = true;
            cmbPositioning.Items.AddRange(new object[] { "Below Characters", "Same as Characters", "Above Characters" });
            cmbPositioning.Location = new Point(12, 37);
            cmbPositioning.Margin = new Padding(5);
            cmbPositioning.Name = "cmbPositioning";
            cmbPositioning.Size = new Size(312, 32);
            cmbPositioning.TabIndex = 1;
            // 
            // fraGraphicPic
            // 
            fraGraphicPic.BackColor = Color.FromArgb(45, 45, 48);
            fraGraphicPic.BorderColor = Color.FromArgb(90, 90, 90);
            fraGraphicPic.Controls.Add(picGraphic);
            fraGraphicPic.ForeColor = Color.Gainsboro;
            fraGraphicPic.Location = new Point(5, 260);
            fraGraphicPic.Margin = new Padding(5);
            fraGraphicPic.Name = "fraGraphicPic";
            fraGraphicPic.Padding = new Padding(5);
            fraGraphicPic.Size = new Size(288, 447);
            fraGraphicPic.TabIndex = 12;
            fraGraphicPic.TabStop = false;
            fraGraphicPic.Text = "Graphic";
            // 
            // picGraphic
            // 
            picGraphic.BackgroundImageLayout = ImageLayout.None;
            picGraphic.Location = new Point(10, 37);
            picGraphic.Margin = new Padding(5);
            picGraphic.Name = "picGraphic";
            picGraphic.Size = new Size(268, 398);
            picGraphic.TabIndex = 1;
            picGraphic.TabStop = false;
            // 
            // DarkGroupBox6
            // 
            DarkGroupBox6.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox6.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox6.Controls.Add(chkShowName);
            DarkGroupBox6.Controls.Add(chkWalkThrough);
            DarkGroupBox6.Controls.Add(chkDirFix);
            DarkGroupBox6.Controls.Add(chkWalkAnim);
            DarkGroupBox6.ForeColor = Color.Gainsboro;
            DarkGroupBox6.Location = new Point(5, 716);
            DarkGroupBox6.Margin = new Padding(5);
            DarkGroupBox6.Name = "DarkGroupBox6";
            DarkGroupBox6.Padding = new Padding(5);
            DarkGroupBox6.Size = new Size(293, 215);
            DarkGroupBox6.TabIndex = 10;
            DarkGroupBox6.TabStop = false;
            DarkGroupBox6.Text = "Options";
            // 
            // chkShowName
            // 
            chkShowName.AutoSize = true;
            chkShowName.Location = new Point(12, 170);
            chkShowName.Margin = new Padding(5);
            chkShowName.Name = "chkShowName";
            chkShowName.Size = new Size(134, 29);
            chkShowName.TabIndex = 3;
            chkShowName.Text = "Show Name";
            // 
            // chkWalkThrough
            // 
            chkWalkThrough.AutoSize = true;
            chkWalkThrough.Location = new Point(12, 125);
            chkWalkThrough.Margin = new Padding(5);
            chkWalkThrough.Name = "chkWalkThrough";
            chkWalkThrough.Size = new Size(148, 29);
            chkWalkThrough.TabIndex = 2;
            chkWalkThrough.Text = "Walk Through";
            // 
            // chkDirFix
            // 
            chkDirFix.AutoSize = true;
            chkDirFix.Location = new Point(12, 80);
            chkDirFix.Margin = new Padding(5);
            chkDirFix.Name = "chkDirFix";
            chkDirFix.Size = new Size(155, 29);
            chkDirFix.TabIndex = 1;
            chkDirFix.Text = "Direction Fixed";
            // 
            // chkWalkAnim
            // 
            chkWalkAnim.AutoSize = true;
            chkWalkAnim.Location = new Point(12, 37);
            chkWalkAnim.Margin = new Padding(5);
            chkWalkAnim.Name = "chkWalkAnim";
            chkWalkAnim.Size = new Size(192, 29);
            chkWalkAnim.TabIndex = 0;
            chkWalkAnim.Text = "No Walk Animation";
            // 
            // DarkGroupBox5
            // 
            DarkGroupBox5.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox5.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox5.Controls.Add(cmbTrigger);
            DarkGroupBox5.ForeColor = Color.Gainsboro;
            DarkGroupBox5.Location = new Point(310, 623);
            DarkGroupBox5.Margin = new Padding(5);
            DarkGroupBox5.Name = "DarkGroupBox5";
            DarkGroupBox5.Padding = new Padding(5);
            DarkGroupBox5.Size = new Size(333, 95);
            DarkGroupBox5.TabIndex = 4;
            DarkGroupBox5.TabStop = false;
            DarkGroupBox5.Text = "Trigger";
            // 
            // cmbTrigger
            // 
            cmbTrigger.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTrigger.FormattingEnabled = true;
            cmbTrigger.Items.AddRange(new object[] { "Action Button", "Player Touch", "Parallel Process" });
            cmbTrigger.Location = new Point(10, 37);
            cmbTrigger.Margin = new Padding(5);
            cmbTrigger.Name = "cmbTrigger";
            cmbTrigger.Size = new Size(312, 32);
            cmbTrigger.TabIndex = 0;
            // 
            // DarkGroupBox4
            // 
            DarkGroupBox4.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox4.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox4.Controls.Add(picGraphicSel);
            DarkGroupBox4.ForeColor = Color.Gainsboro;
            DarkGroupBox4.Location = new Point(303, 513);
            DarkGroupBox4.Margin = new Padding(5);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(5);
            DarkGroupBox4.Size = new Size(333, 91);
            DarkGroupBox4.TabIndex = 3;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Positioning";
            // 
            // picGraphicSel
            // 
            picGraphicSel.BackgroundImageLayout = ImageLayout.None;
            picGraphicSel.Location = new Point(-315, -563);
            picGraphicSel.Margin = new Padding(5);
            picGraphicSel.Name = "picGraphicSel";
            picGraphicSel.Size = new Size(1337, 988);
            picGraphicSel.TabIndex = 5;
            picGraphicSel.TabStop = false;
            // 
            // DarkGroupBox3
            // 
            DarkGroupBox3.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox3.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox3.Controls.Add(DarkLabel7);
            DarkGroupBox3.Controls.Add(cmbMoveFreq);
            DarkGroupBox3.Controls.Add(DarkLabel6);
            DarkGroupBox3.Controls.Add(cmbMoveSpeed);
            DarkGroupBox3.Controls.Add(btnMoveRoute);
            DarkGroupBox3.Controls.Add(cmbMoveType);
            DarkGroupBox3.Controls.Add(DarkLabel5);
            DarkGroupBox3.ForeColor = Color.Gainsboro;
            DarkGroupBox3.Location = new Point(305, 265);
            DarkGroupBox3.Margin = new Padding(5);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(5);
            DarkGroupBox3.Size = new Size(333, 237);
            DarkGroupBox3.TabIndex = 2;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Movement";
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(10, 191);
            DarkLabel7.Margin = new Padding(5, 0, 5, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(93, 25);
            DarkLabel7.TabIndex = 6;
            DarkLabel7.Text = "Frequency";
            // 
            // cmbMoveFreq
            // 
            cmbMoveFreq.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMoveFreq.FormattingEnabled = true;
            cmbMoveFreq.Items.AddRange(new object[] { "Lowest", "Lower", "Normal", "Higher", "Highest" });
            cmbMoveFreq.Location = new Point(115, 187);
            cmbMoveFreq.Margin = new Padding(5);
            cmbMoveFreq.Name = "cmbMoveFreq";
            cmbMoveFreq.Size = new Size(206, 32);
            cmbMoveFreq.TabIndex = 5;
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(10, 140);
            DarkLabel6.Margin = new Padding(5, 0, 5, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(66, 25);
            DarkLabel6.TabIndex = 4;
            DarkLabel6.Text = "Speed:";
            // 
            // cmbMoveSpeed
            // 
            cmbMoveSpeed.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMoveSpeed.FormattingEnabled = true;
            cmbMoveSpeed.Items.AddRange(new object[] { "8x Slower", "4x Slower", "2x Slower", "Normal", "2x Faster", "4x Faster" });
            cmbMoveSpeed.Location = new Point(115, 135);
            cmbMoveSpeed.Margin = new Padding(5);
            cmbMoveSpeed.Name = "cmbMoveSpeed";
            cmbMoveSpeed.Size = new Size(206, 32);
            cmbMoveSpeed.TabIndex = 3;
            // 
            // btnMoveRoute
            // 
            btnMoveRoute.Location = new Point(198, 78);
            btnMoveRoute.Margin = new Padding(5);
            btnMoveRoute.Name = "btnMoveRoute";
            btnMoveRoute.Padding = new Padding(8, 10, 8, 10);
            btnMoveRoute.Size = new Size(125, 45);
            btnMoveRoute.TabIndex = 2;
            btnMoveRoute.Text = "Move Route";
            // 
            // cmbMoveType
            // 
            cmbMoveType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMoveType.FormattingEnabled = true;
            cmbMoveType.Items.AddRange(new object[] { "Fixed Position", "Random", "Move Route" });
            cmbMoveType.Location = new Point(115, 27);
            cmbMoveType.Margin = new Padding(5);
            cmbMoveType.Name = "cmbMoveType";
            cmbMoveType.Size = new Size(206, 32);
            cmbMoveType.TabIndex = 1;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(10, 34);
            DarkLabel5.Margin = new Padding(5, 0, 5, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(53, 25);
            DarkLabel5.TabIndex = 0;
            DarkLabel5.Text = "Type:";
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(cmbSelfSwitchCompare);
            DarkGroupBox1.Controls.Add(DarkLabel4);
            DarkGroupBox1.Controls.Add(cmbSelfSwitch);
            DarkGroupBox1.Controls.Add(chkSelfSwitch);
            DarkGroupBox1.Controls.Add(cmbHasItem);
            DarkGroupBox1.Controls.Add(chkHasItem);
            DarkGroupBox1.Controls.Add(cmbPlayerSwitchCompare);
            DarkGroupBox1.Controls.Add(DarkLabel3);
            DarkGroupBox1.Controls.Add(cmbPlayerSwitch);
            DarkGroupBox1.Controls.Add(chkPlayerSwitch);
            DarkGroupBox1.Controls.Add(nudPlayerVariable);
            DarkGroupBox1.Controls.Add(cmbPlayervarCompare);
            DarkGroupBox1.Controls.Add(DarkLabel2);
            DarkGroupBox1.Controls.Add(cmbPlayerVar);
            DarkGroupBox1.Controls.Add(chkPlayerVar);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(5, 12);
            DarkGroupBox1.Margin = new Padding(5);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(5);
            DarkGroupBox1.Size = new Size(633, 241);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Conditions";
            // 
            // cmbSelfSwitchCompare
            // 
            cmbSelfSwitchCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSelfSwitchCompare.FormattingEnabled = true;
            cmbSelfSwitchCompare.Items.AddRange(new object[] { "False = 0", "True = 1" });
            cmbSelfSwitchCompare.Location = new Point(372, 188);
            cmbSelfSwitchCompare.Margin = new Padding(5);
            cmbSelfSwitchCompare.Name = "cmbSelfSwitchCompare";
            cmbSelfSwitchCompare.Size = new Size(146, 32);
            cmbSelfSwitchCompare.TabIndex = 14;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(338, 195);
            DarkLabel4.Margin = new Padding(5, 0, 5, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(24, 25);
            DarkLabel4.TabIndex = 13;
            DarkLabel4.Text = "is";
            // 
            // cmbSelfSwitch
            // 
            cmbSelfSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSelfSwitch.FormattingEnabled = true;
            cmbSelfSwitch.Items.AddRange(new object[] { "None", "1 - A", "2 - B", "3 - C", "4 - D" });
            cmbSelfSwitch.Location = new Point(180, 188);
            cmbSelfSwitch.Margin = new Padding(5);
            cmbSelfSwitch.Name = "cmbSelfSwitch";
            cmbSelfSwitch.Size = new Size(146, 32);
            cmbSelfSwitch.TabIndex = 12;
            // 
            // chkSelfSwitch
            // 
            chkSelfSwitch.AutoSize = true;
            chkSelfSwitch.Location = new Point(10, 191);
            chkSelfSwitch.Margin = new Padding(5);
            chkSelfSwitch.Name = "chkSelfSwitch";
            chkSelfSwitch.Size = new Size(123, 29);
            chkSelfSwitch.TabIndex = 11;
            chkSelfSwitch.Text = "Self Switch";
            // 
            // cmbHasItem
            // 
            cmbHasItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbHasItem.FormattingEnabled = true;
            cmbHasItem.Location = new Point(180, 137);
            cmbHasItem.Margin = new Padding(5);
            cmbHasItem.Name = "cmbHasItem";
            cmbHasItem.Size = new Size(337, 32);
            cmbHasItem.TabIndex = 10;
            // 
            // chkHasItem
            // 
            chkHasItem.AutoSize = true;
            chkHasItem.Location = new Point(10, 140);
            chkHasItem.Margin = new Padding(5);
            chkHasItem.Name = "chkHasItem";
            chkHasItem.Size = new Size(161, 29);
            chkHasItem.TabIndex = 9;
            chkHasItem.Text = "Player Has Item";
            // 
            // cmbPlayerSwitchCompare
            // 
            cmbPlayerSwitchCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayerSwitchCompare.FormattingEnabled = true;
            cmbPlayerSwitchCompare.Items.AddRange(new object[] { "False = 0", "True = 1" });
            cmbPlayerSwitchCompare.Location = new Point(372, 85);
            cmbPlayerSwitchCompare.Margin = new Padding(5);
            cmbPlayerSwitchCompare.Name = "cmbPlayerSwitchCompare";
            cmbPlayerSwitchCompare.Size = new Size(146, 32);
            cmbPlayerSwitchCompare.TabIndex = 8;
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(338, 90);
            DarkLabel3.Margin = new Padding(5, 0, 5, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(24, 25);
            DarkLabel3.TabIndex = 7;
            DarkLabel3.Text = "is";
            // 
            // cmbPlayerSwitch
            // 
            cmbPlayerSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayerSwitch.FormattingEnabled = true;
            cmbPlayerSwitch.Location = new Point(180, 85);
            cmbPlayerSwitch.Margin = new Padding(5);
            cmbPlayerSwitch.Name = "cmbPlayerSwitch";
            cmbPlayerSwitch.Size = new Size(146, 32);
            cmbPlayerSwitch.TabIndex = 6;
            // 
            // chkPlayerSwitch
            // 
            chkPlayerSwitch.AutoSize = true;
            chkPlayerSwitch.Location = new Point(10, 88);
            chkPlayerSwitch.Margin = new Padding(5);
            chkPlayerSwitch.Name = "chkPlayerSwitch";
            chkPlayerSwitch.Size = new Size(141, 29);
            chkPlayerSwitch.TabIndex = 5;
            chkPlayerSwitch.Text = "Player Switch";
            // 
            // nudPlayerVariable
            // 
            nudPlayerVariable.Location = new Point(530, 35);
            nudPlayerVariable.Margin = new Padding(5);
            nudPlayerVariable.Name = "nudPlayerVariable";
            nudPlayerVariable.Size = new Size(93, 31);
            nudPlayerVariable.TabIndex = 4;
            // 
            // cmbPlayervarCompare
            // 
            cmbPlayervarCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayervarCompare.FormattingEnabled = true;
            cmbPlayervarCompare.Items.AddRange(new object[] { "Equal To", "Great Than Or Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal" });
            cmbPlayervarCompare.Location = new Point(372, 34);
            cmbPlayervarCompare.Margin = new Padding(5);
            cmbPlayervarCompare.Name = "cmbPlayervarCompare";
            cmbPlayervarCompare.Size = new Size(146, 32);
            cmbPlayervarCompare.TabIndex = 3;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(338, 45);
            DarkLabel2.Margin = new Padding(5, 0, 5, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(24, 25);
            DarkLabel2.TabIndex = 2;
            DarkLabel2.Text = "is";
            // 
            // cmbPlayerVar
            // 
            cmbPlayerVar.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayerVar.FormattingEnabled = true;
            cmbPlayerVar.Location = new Point(180, 34);
            cmbPlayerVar.Margin = new Padding(5);
            cmbPlayerVar.Name = "cmbPlayerVar";
            cmbPlayerVar.Size = new Size(146, 32);
            cmbPlayerVar.TabIndex = 1;
            // 
            // chkPlayerVar
            // 
            chkPlayerVar.AutoSize = true;
            chkPlayerVar.Location = new Point(10, 37);
            chkPlayerVar.Margin = new Padding(5);
            chkPlayerVar.Name = "chkPlayerVar";
            chkPlayerVar.Size = new Size(152, 29);
            chkPlayerVar.TabIndex = 0;
            chkPlayerVar.Text = "Player Variable";
            // 
            // DarkGroupBox8
            // 
            DarkGroupBox8.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox8.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox8.Controls.Add(btnClearCommand);
            DarkGroupBox8.Controls.Add(btnDeleteCommand);
            DarkGroupBox8.Controls.Add(btnEditCommand);
            DarkGroupBox8.Controls.Add(btnAddCommand);
            DarkGroupBox8.ForeColor = Color.Gainsboro;
            DarkGroupBox8.Location = new Point(648, 845);
            DarkGroupBox8.Margin = new Padding(5);
            DarkGroupBox8.Name = "DarkGroupBox8";
            DarkGroupBox8.Padding = new Padding(5);
            DarkGroupBox8.Size = new Size(655, 95);
            DarkGroupBox8.TabIndex = 9;
            DarkGroupBox8.TabStop = false;
            DarkGroupBox8.Text = "Commands";
            // 
            // btnClearCommand
            // 
            btnClearCommand.Location = new Point(520, 37);
            btnClearCommand.Margin = new Padding(5);
            btnClearCommand.Name = "btnClearCommand";
            btnClearCommand.Padding = new Padding(8, 10, 8, 10);
            btnClearCommand.Size = new Size(125, 45);
            btnClearCommand.TabIndex = 3;
            btnClearCommand.Text = "Clear";
            // 
            // btnDeleteCommand
            // 
            btnDeleteCommand.Location = new Point(353, 37);
            btnDeleteCommand.Margin = new Padding(5);
            btnDeleteCommand.Name = "btnDeleteCommand";
            btnDeleteCommand.Padding = new Padding(8, 10, 8, 10);
            btnDeleteCommand.Size = new Size(125, 45);
            btnDeleteCommand.TabIndex = 2;
            btnDeleteCommand.Text = "Delete";
            // 
            // btnEditCommand
            // 
            btnEditCommand.Location = new Point(180, 37);
            btnEditCommand.Margin = new Padding(5);
            btnEditCommand.Name = "btnEditCommand";
            btnEditCommand.Padding = new Padding(8, 10, 8, 10);
            btnEditCommand.Size = new Size(125, 45);
            btnEditCommand.TabIndex = 1;
            btnEditCommand.Text = "Edit";
            // 
            // btnAddCommand
            // 
            btnAddCommand.Location = new Point(10, 37);
            btnAddCommand.Margin = new Padding(5);
            btnAddCommand.Name = "btnAddCommand";
            btnAddCommand.Padding = new Padding(8, 10, 8, 10);
            btnAddCommand.Size = new Size(125, 45);
            btnAddCommand.TabIndex = 0;
            btnAddCommand.Text = "Add";
            // 
            // fraGraphic
            // 
            fraGraphic.BackColor = Color.FromArgb(45, 45, 48);
            fraGraphic.BorderColor = Color.FromArgb(90, 90, 90);
            fraGraphic.Controls.Add(btnGraphicOk);
            fraGraphic.Controls.Add(btnGraphicCancel);
            fraGraphic.Controls.Add(DarkLabel13);
            fraGraphic.Controls.Add(nudGraphic);
            fraGraphic.Controls.Add(DarkLabel12);
            fraGraphic.Controls.Add(cmbGraphic);
            fraGraphic.Controls.Add(DarkLabel11);
            fraGraphic.ForeColor = Color.Gainsboro;
            fraGraphic.Location = new Point(648, 10);
            fraGraphic.Margin = new Padding(5);
            fraGraphic.Name = "fraGraphic";
            fraGraphic.Padding = new Padding(5);
            fraGraphic.Size = new Size(655, 928);
            fraGraphic.TabIndex = 14;
            fraGraphic.TabStop = false;
            fraGraphic.Text = "Graphic Selection";
            fraGraphic.Visible = false;
            // 
            // btnGraphicOk
            // 
            btnGraphicOk.Location = new Point(1087, 1097);
            btnGraphicOk.Margin = new Padding(5);
            btnGraphicOk.Name = "btnGraphicOk";
            btnGraphicOk.Padding = new Padding(8, 10, 8, 10);
            btnGraphicOk.Size = new Size(125, 45);
            btnGraphicOk.TabIndex = 8;
            btnGraphicOk.Text = "Ok";
            // 
            // btnGraphicCancel
            // 
            btnGraphicCancel.Location = new Point(1222, 1097);
            btnGraphicCancel.Margin = new Padding(5);
            btnGraphicCancel.Name = "btnGraphicCancel";
            btnGraphicCancel.Padding = new Padding(8, 10, 8, 10);
            btnGraphicCancel.Size = new Size(125, 45);
            btnGraphicCancel.TabIndex = 7;
            btnGraphicCancel.Text = "Cancel";
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(17, 1098);
            DarkLabel13.Margin = new Padding(5, 0, 5, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(272, 25);
            DarkLabel13.TabIndex = 6;
            DarkLabel13.Text = "Hold Shift to select multiple tiles.";
            // 
            // nudGraphic
            // 
            nudGraphic.Location = new Point(173, 95);
            nudGraphic.Margin = new Padding(5);
            nudGraphic.Name = "nudGraphic";
            nudGraphic.Size = new Size(360, 31);
            nudGraphic.TabIndex = 3;
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(35, 98);
            DarkLabel12.Margin = new Padding(5, 0, 5, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(81, 25);
            DarkLabel12.TabIndex = 2;
            DarkLabel12.Text = "Number:";
            // 
            // cmbGraphic
            // 
            cmbGraphic.DrawMode = DrawMode.OwnerDrawFixed;
            cmbGraphic.FormattingEnabled = true;
            cmbGraphic.Items.AddRange(new object[] { "None", "Character", "Tileset" });
            cmbGraphic.Location = new Point(173, 35);
            cmbGraphic.Margin = new Padding(5);
            cmbGraphic.Name = "cmbGraphic";
            cmbGraphic.Size = new Size(359, 32);
            cmbGraphic.TabIndex = 1;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(35, 40);
            DarkLabel11.Margin = new Padding(5, 0, 5, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(126, 25);
            DarkLabel11.TabIndex = 0;
            DarkLabel11.Text = "Graphics Type:";
            // 
            // fraCommands
            // 
            fraCommands.Controls.Add(btnCancelCommand);
            fraCommands.Controls.Add(tvCommands);
            fraCommands.Location = new Point(648, 12);
            fraCommands.Margin = new Padding(5);
            fraCommands.Name = "fraCommands";
            fraCommands.Size = new Size(655, 927);
            fraCommands.TabIndex = 6;
            fraCommands.Visible = false;
            // 
            // btnCancelCommand
            // 
            btnCancel.Location = new Point(520, 870);
            btnCancel.Margin = new Padding(5);
            btnCancel.Name = "btnCancelCommand";
            btnCancel.Padding = new Padding(8, 10, 8, 10);
            btnCancel.Size = new Size(125, 45);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            // 
            // lstCommands
            // 
            lstCommands.BackColor = Color.FromArgb(45, 45, 48);
            lstCommands.BorderStyle = BorderStyle.FixedSingle;
            lstCommands.ForeColor = Color.Gainsboro;
            lstCommands.FormattingEnabled = true;
            lstCommands.ItemHeight = 25;
            lstCommands.Location = new Point(648, 12);
            lstCommands.Margin = new Padding(5);
            lstCommands.Name = "lstCommands";
            lstCommands.Size = new Size(654, 827);
            lstCommands.TabIndex = 8;
            // 
            // btnLabeling
            // 
            btnLabeling.Location = new Point(10, 1097);
            btnLabeling.Margin = new Padding(5);
            btnLabeling.Name = "btnLabeling";
            btnLabeling.Padding = new Padding(8, 10, 8, 10);
            btnLabeling.Size = new Size(293, 45);
            btnLabeling.TabIndex = 6;
            btnLabeling.Text = "Edit Variables/Switches";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(1183, 1105);
            btnCancel.Margin = new Padding(5);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(8, 10, 8, 10);
            btnCancel.Size = new Size(125, 45);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(1048, 1105);
            btnOk.Margin = new Padding(5);
            btnOk.Name = "btnOk";
            btnOk.Padding = new Padding(8, 10, 8, 10);
            btnOk.Size = new Size(125, 45);
            btnOk.TabIndex = 8;
            btnOk.Text = "Ok";
            // 
            // fraMoveRoute
            // 
            fraMoveRoute.BackColor = Color.FromArgb(45, 45, 48);
            fraMoveRoute.BorderColor = Color.FromArgb(90, 90, 90);
            fraMoveRoute.Controls.Add(btnMoveRouteOk);
            fraMoveRoute.Controls.Add(btnMoveRouteCancel);
            fraMoveRoute.Controls.Add(chkRepeatRoute);
            fraMoveRoute.Controls.Add(chkIgnoreMove);
            fraMoveRoute.Controls.Add(DarkGroupBox10);
            fraMoveRoute.Controls.Add(lstMoveRoute);
            fraMoveRoute.Controls.Add(cmbEvent);
            fraMoveRoute.ForeColor = Color.Gainsboro;
            fraMoveRoute.Location = new Point(1333, 23);
            fraMoveRoute.Margin = new Padding(5);
            fraMoveRoute.Name = "fraMoveRoute";
            fraMoveRoute.Padding = new Padding(5);
            fraMoveRoute.Size = new Size(155, 163);
            fraMoveRoute.TabIndex = 0;
            fraMoveRoute.TabStop = false;
            fraMoveRoute.Text = "Move Route";
            fraMoveRoute.Visible = false;
            // 
            // btnMoveRouteOk
            // 
            btnMoveRouteOk.Location = new Point(1070, 828);
            btnMoveRouteOk.Margin = new Padding(5);
            btnMoveRouteOk.Name = "btnMoveRouteOk";
            btnMoveRouteOk.Padding = new Padding(8, 10, 8, 10);
            btnMoveRouteOk.Size = new Size(125, 45);
            btnMoveRouteOk.TabIndex = 7;
            btnMoveRouteOk.Text = "Ok";
            // 
            // btnMoveRouteCancel
            // 
            btnMoveRouteCancel.Location = new Point(1205, 828);
            btnMoveRouteCancel.Margin = new Padding(5);
            btnMoveRouteCancel.Name = "btnMoveRouteCancel";
            btnMoveRouteCancel.Padding = new Padding(8, 10, 8, 10);
            btnMoveRouteCancel.Size = new Size(125, 45);
            btnMoveRouteCancel.TabIndex = 6;
            btnMoveRouteCancel.Text = "Cancel";
            // 
            // chkRepeatRoute
            // 
            chkRepeatRoute.AutoSize = true;
            chkRepeatRoute.Location = new Point(10, 873);
            chkRepeatRoute.Margin = new Padding(5);
            chkRepeatRoute.Name = "chkRepeatRoute";
            chkRepeatRoute.Size = new Size(143, 29);
            chkRepeatRoute.TabIndex = 5;
            chkRepeatRoute.Text = "Repeat Route";
            // 
            // chkIgnoreMove
            // 
            chkIgnoreMove.AutoSize = true;
            chkIgnoreMove.Location = new Point(10, 828);
            chkIgnoreMove.Margin = new Padding(5);
            chkIgnoreMove.Name = "chkIgnoreMove";
            chkIgnoreMove.Size = new Size(245, 29);
            chkIgnoreMove.TabIndex = 4;
            chkIgnoreMove.Text = "Ignore if event can't move";
            // 
            // DarkGroupBox10
            // 
            DarkGroupBox10.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox10.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox10.Controls.Add(lstvwMoveRoute);
            DarkGroupBox10.ForeColor = Color.Gainsboro;
            DarkGroupBox10.Location = new Point(338, 20);
            DarkGroupBox10.Margin = new Padding(5);
            DarkGroupBox10.Name = "DarkGroupBox10";
            DarkGroupBox10.Padding = new Padding(5);
            DarkGroupBox10.Size = new Size(992, 798);
            DarkGroupBox10.TabIndex = 3;
            DarkGroupBox10.TabStop = false;
            DarkGroupBox10.Text = "Commands";
            // 
            // lstvwMoveRoute
            // 
            lstvwMoveRoute.AutoArrange = false;
            lstvwMoveRoute.BackColor = Color.DimGray;
            lstvwMoveRoute.BorderStyle = BorderStyle.None;
            lstvwMoveRoute.Columns.AddRange(new ColumnHeader[] { ColumnHeader3, ColumnHeader4 });
            lstvwMoveRoute.Dock = DockStyle.Top;
            lstvwMoveRoute.Font = new Font("Microsoft Sans Serif", 8.25f);
            lstvwMoveRoute.ForeColor = Color.Gainsboro;
            ListViewGroup1.Header = "Movement";
            ListViewGroup1.Name = "lstVgMovement";
            ListViewGroup2.Header = "Wait";
            ListViewGroup2.Name = "lstVgWait";
            ListViewGroup3.Header = "Turning";
            ListViewGroup3.Name = "lstVgTurn";
            ListViewGroup4.Header = "Speed";
            ListViewGroup4.Name = "lstVgSpeed";
            ListViewGroup5.Header = "Walk Animation";
            ListViewGroup5.Name = "lstVgWalk";
            ListViewGroup6.Header = "Fixed Direction";
            ListViewGroup6.Name = "lstVgDirection";
            ListViewGroup7.Header = "WalkThrough";
            ListViewGroup7.Name = "lstVgWalkThrough";
            ListViewGroup8.Header = "Set Position";
            ListViewGroup8.Name = "lstVgSetposition";
            ListViewGroup9.Header = "Set Graphic";
            ListViewGroup9.Name = "lstVgSetGraphic";
            lstvwMoveRoute.Groups.AddRange(new ListViewGroup[] { ListViewGroup1, ListViewGroup2, ListViewGroup3, ListViewGroup4, ListViewGroup5, ListViewGroup6, ListViewGroup7, ListViewGroup8, ListViewGroup9 });
            lstvwMoveRoute.HeaderStyle = ColumnHeaderStyle.None;
            ListViewItem1.Group = ListViewGroup1;
            ListViewItem2.Group = ListViewGroup1;
            ListViewItem2.IndentCount = 1;
            ListViewItem3.Group = ListViewGroup1;
            ListViewItem4.Group = ListViewGroup1;
            ListViewItem4.IndentCount = 1;
            ListViewItem5.Group = ListViewGroup1;
            ListViewItem6.Group = ListViewGroup1;
            ListViewItem7.Group = ListViewGroup1;
            ListViewItem8.Group = ListViewGroup1;
            ListViewItem9.Group = ListViewGroup1;
            ListViewItem10.Group = ListViewGroup2;
            ListViewItem11.Group = ListViewGroup2;
            ListViewItem12.Group = ListViewGroup2;
            ListViewItem13.Group = ListViewGroup3;
            ListViewItem14.Group = ListViewGroup3;
            ListViewItem15.Group = ListViewGroup3;
            ListViewItem16.Group = ListViewGroup3;
            ListViewItem17.Group = ListViewGroup3;
            ListViewItem18.Group = ListViewGroup3;
            ListViewItem19.Group = ListViewGroup3;
            ListViewItem20.Group = ListViewGroup3;
            ListViewItem21.Group = ListViewGroup3;
            ListViewItem22.Group = ListViewGroup3;
            ListViewItem23.Group = ListViewGroup4;
            ListViewItem24.Group = ListViewGroup4;
            ListViewItem25.Group = ListViewGroup4;
            ListViewItem26.Group = ListViewGroup4;
            ListViewItem27.Group = ListViewGroup4;
            ListViewItem28.Group = ListViewGroup4;
            ListViewItem29.Group = ListViewGroup4;
            ListViewItem30.Group = ListViewGroup4;
            ListViewItem31.Group = ListViewGroup4;
            ListViewItem32.Group = ListViewGroup4;
            ListViewItem33.Group = ListViewGroup4;
            ListViewItem34.Group = ListViewGroup5;
            ListViewItem35.Group = ListViewGroup5;
            ListViewItem36.Group = ListViewGroup6;
            ListViewItem37.Group = ListViewGroup6;
            ListViewItem38.Group = ListViewGroup7;
            ListViewItem39.Group = ListViewGroup7;
            ListViewItem40.Group = ListViewGroup8;
            ListViewItem41.Group = ListViewGroup8;
            ListViewItem42.Group = ListViewGroup8;
            ListViewItem43.Group = ListViewGroup9;
            lstvwMoveRoute.Items.AddRange(new ListViewItem[] { ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10, ListViewItem11, ListViewItem12, ListViewItem13, ListViewItem14, ListViewItem15, ListViewItem16, ListViewItem17, ListViewItem18, ListViewItem19, ListViewItem20, ListViewItem21, ListViewItem22, ListViewItem23, ListViewItem24, ListViewItem25, ListViewItem26, ListViewItem27, ListViewItem28, ListViewItem29, ListViewItem30, ListViewItem31, ListViewItem32, ListViewItem33, ListViewItem34, ListViewItem35, ListViewItem36, ListViewItem37, ListViewItem38, ListViewItem39, ListViewItem40, ListViewItem41, ListViewItem42, ListViewItem43 });
            lstvwMoveRoute.LabelWrap = false;
            lstvwMoveRoute.Location = new Point(5, 29);
            lstvwMoveRoute.Margin = new Padding(5);
            lstvwMoveRoute.MultiSelect = false;
            lstvwMoveRoute.Name = "lstvwMoveRoute";
            lstvwMoveRoute.Size = new Size(982, 763);
            lstvwMoveRoute.TabIndex = 5;
            lstvwMoveRoute.UseCompatibleStateImageBehavior = false;
            lstvwMoveRoute.View = View.Tile;
            // 
            // ColumnHeader3
            // 
            ColumnHeader3.Text = "";
            ColumnHeader3.Width = 150;
            // 
            // ColumnHeader4
            // 
            ColumnHeader4.Text = "";
            ColumnHeader4.Width = 150;
            // 
            // lstMoveRoute
            // 
            lstMoveRoute.BackColor = Color.FromArgb(45, 45, 48);
            lstMoveRoute.BorderStyle = BorderStyle.FixedSingle;
            lstMoveRoute.ForeColor = Color.Gainsboro;
            lstMoveRoute.FormattingEnabled = true;
            lstMoveRoute.ItemHeight = 25;
            lstMoveRoute.Location = new Point(10, 88);
            lstMoveRoute.Margin = new Padding(5);
            lstMoveRoute.Name = "lstMoveRoute";
            lstMoveRoute.Size = new Size(317, 727);
            lstMoveRoute.TabIndex = 2;
            // 
            // cmbEvent
            // 
            cmbEvent.DrawMode = DrawMode.OwnerDrawFixed;
            cmbEvent.FormattingEnabled = true;
            cmbEvent.Location = new Point(10, 37);
            cmbEvent.Margin = new Padding(5);
            cmbEvent.Name = "cmbEvent";
            cmbEvent.Size = new Size(316, 32);
            cmbEvent.TabIndex = 0;
            // 
            // pnlGraphicSel
            // 
            pnlGraphicSel.AutoScroll = true;
            pnlGraphicSel.Location = new Point(5, 153);
            pnlGraphicSel.Margin = new Padding(5);
            pnlGraphicSel.Name = "pnlGraphicSel";
            pnlGraphicSel.Size = new Size(1318, 955);
            pnlGraphicSel.TabIndex = 9;
            // 
            // fraDialogue
            // 
            fraDialogue.BackColor = Color.FromArgb(45, 45, 48);
            fraDialogue.BorderColor = Color.FromArgb(90, 90, 90);
            fraDialogue.Controls.Add(fraShowChatBubble);
            fraDialogue.Controls.Add(fraOpenShop);
            fraDialogue.Controls.Add(fraSetSelfSwitch);
            fraDialogue.Controls.Add(fraPlaySound);
            fraDialogue.Controls.Add(fraChangePK);
            fraDialogue.Controls.Add(fraCreateLabel);
            fraDialogue.Controls.Add(fraChangeJob);
            fraDialogue.Controls.Add(fraChangeSkills);
            fraDialogue.Controls.Add(fraPlayerSwitch);
            fraDialogue.Controls.Add(fraSetWait);
            fraDialogue.Controls.Add(fraMoveRouteWait);
            fraDialogue.Controls.Add(fraCustomScript);
            fraDialogue.Controls.Add(fraSpawnNPC);
            fraDialogue.Controls.Add(fraSetWeather);
            fraDialogue.Controls.Add(fraGiveExp);
            fraDialogue.Controls.Add(fraSetAccess);
            fraDialogue.Controls.Add(fraChangeGender);
            fraDialogue.Controls.Add(fraShowChoices);
            fraDialogue.Controls.Add(fraChangeLevel);
            fraDialogue.Controls.Add(fraPlayerVariable);
            fraDialogue.Controls.Add(fraPlayAnimation);
            fraDialogue.Controls.Add(fraChangeSprite);
            fraDialogue.Controls.Add(fraGoToLabel);
            fraDialogue.Controls.Add(fraMapTint);
            fraDialogue.Controls.Add(fraShowPic);
            fraDialogue.Controls.Add(fraConditionalBranch);
            fraDialogue.Controls.Add(fraPlayBGM);
            fraDialogue.Controls.Add(fraPlayerWarp);
            fraDialogue.Controls.Add(fraSetFog);
            fraDialogue.Controls.Add(fraShowText);
            fraDialogue.Controls.Add(fraAddText);
            fraDialogue.Controls.Add(fraChangeItems);
            fraDialogue.ForeColor = Color.Gainsboro;
            fraDialogue.Location = new Point(1508, 23);
            fraDialogue.Margin = new Padding(5);
            fraDialogue.Name = "fraDialogue";
            fraDialogue.Padding = new Padding(5);
            fraDialogue.Size = new Size(1108, 1145);
            fraDialogue.TabIndex = 10;
            fraDialogue.TabStop = false;
            fraDialogue.Visible = false;
            // 
            // fraShowChatBubble
            // 
            fraShowChatBubble.BackColor = Color.FromArgb(45, 45, 48);
            fraShowChatBubble.BorderColor = Color.FromArgb(90, 90, 90);
            fraShowChatBubble.Controls.Add(btnShowChatBubbleOk);
            fraShowChatBubble.Controls.Add(btnShowChatBubbleCancel);
            fraShowChatBubble.Controls.Add(DarkLabel41);
            fraShowChatBubble.Controls.Add(cmbChatBubbleTarget);
            fraShowChatBubble.Controls.Add(cmbChatBubbleTargetType);
            fraShowChatBubble.Controls.Add(DarkLabel40);
            fraShowChatBubble.Controls.Add(txtChatbubbleText);
            fraShowChatBubble.Controls.Add(DarkLabel39);
            fraShowChatBubble.ForeColor = Color.Gainsboro;
            fraShowChatBubble.Location = new Point(668, 348);
            fraShowChatBubble.Margin = new Padding(5);
            fraShowChatBubble.Name = "fraShowChatBubble";
            fraShowChatBubble.Padding = new Padding(5);
            fraShowChatBubble.Size = new Size(410, 272);
            fraShowChatBubble.TabIndex = 27;
            fraShowChatBubble.TabStop = false;
            fraShowChatBubble.Text = "Show ChatBubble";
            fraShowChatBubble.Visible = false;
            // 
            // btnShowChatBubbleOk
            // 
            btnShowChatBubbleOk.Location = new Point(140, 215);
            btnShowChatBubbleOk.Margin = new Padding(5);
            btnShowChatBubbleOk.Name = "btnShowChatBubbleOk";
            btnShowChatBubbleOk.Padding = new Padding(8, 10, 8, 10);
            btnShowChatBubbleOk.Size = new Size(125, 45);
            btnShowChatBubbleOk.TabIndex = 31;
            btnShowChatBubbleOk.Text = "Ok";
            // 
            // btnShowChatBubbleCancel
            // 
            btnShowChatBubbleCancel.Location = new Point(275, 215);
            btnShowChatBubbleCancel.Margin = new Padding(5);
            btnShowChatBubbleCancel.Name = "btnShowChatBubbleCancel";
            btnShowChatBubbleCancel.Padding = new Padding(8, 10, 8, 10);
            btnShowChatBubbleCancel.Size = new Size(125, 45);
            btnShowChatBubbleCancel.TabIndex = 30;
            btnShowChatBubbleCancel.Text = "Cancel";
            // 
            // DarkLabel41
            // 
            DarkLabel41.AutoSize = true;
            DarkLabel41.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel41.Location = new Point(10, 170);
            DarkLabel41.Margin = new Padding(5, 0, 5, 0);
            DarkLabel41.Name = "DarkLabel41";
            DarkLabel41.Size = new Size(59, 25);
            DarkLabel41.TabIndex = 29;
            DarkLabel41.Text = "Index:";
            // 
            // cmbChatBubbleTarget
            // 
            cmbChatBubbleTarget.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChatBubbleTarget.FormattingEnabled = true;
            cmbChatBubbleTarget.Location = new Point(135, 163);
            cmbChatBubbleTarget.Margin = new Padding(5);
            cmbChatBubbleTarget.Name = "cmbChatBubbleTarget";
            cmbChatBubbleTarget.Size = new Size(262, 32);
            cmbChatBubbleTarget.TabIndex = 28;
            // 
            // cmbChatBubbleTargetType
            // 
            cmbChatBubbleTargetType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChatBubbleTargetType.FormattingEnabled = true;
            cmbChatBubbleTargetType.Items.AddRange(new object[] { "Player", "NPC", "Event" });
            cmbChatBubbleTargetType.Location = new Point(135, 112);
            cmbChatBubbleTargetType.Margin = new Padding(5);
            cmbChatBubbleTargetType.Name = "cmbChatBubbleTargetType";
            cmbChatBubbleTargetType.Size = new Size(262, 32);
            cmbChatBubbleTargetType.TabIndex = 27;
            // 
            // DarkLabel40
            // 
            DarkLabel40.AutoSize = true;
            DarkLabel40.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel40.Location = new Point(10, 116);
            DarkLabel40.Margin = new Padding(5, 0, 5, 0);
            DarkLabel40.Name = "DarkLabel40";
            DarkLabel40.Size = new Size(106, 25);
            DarkLabel40.TabIndex = 2;
            DarkLabel40.Text = "Target Type:";
            // 
            // txtChatbubbleText
            // 
            txtChatbubbleText.BackColor = Color.FromArgb(69, 73, 74);
            txtChatbubbleText.BorderStyle = BorderStyle.FixedSingle;
            txtChatbubbleText.ForeColor = Color.FromArgb(220, 220, 220);
            txtChatbubbleText.Location = new Point(10, 62);
            txtChatbubbleText.Margin = new Padding(5);
            txtChatbubbleText.Name = "txtChatbubbleText";
            txtChatbubbleText.Size = new Size(389, 31);
            txtChatbubbleText.TabIndex = 1;
            // 
            // DarkLabel39
            // 
            DarkLabel39.AutoSize = true;
            DarkLabel39.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel39.Location = new Point(10, 30);
            DarkLabel39.Margin = new Padding(5, 0, 5, 0);
            DarkLabel39.Name = "DarkLabel39";
            DarkLabel39.Size = new Size(138, 25);
            DarkLabel39.TabIndex = 0;
            DarkLabel39.Text = "ChatBubble Text";
            // 
            // fraOpenShop
            // 
            fraOpenShop.BackColor = Color.FromArgb(45, 45, 48);
            fraOpenShop.BorderColor = Color.FromArgb(90, 90, 90);
            fraOpenShop.Controls.Add(btnOpenShopOk);
            fraOpenShop.Controls.Add(btnOpenShopCancel);
            fraOpenShop.Controls.Add(cmbOpenShop);
            fraOpenShop.ForeColor = Color.Gainsboro;
            fraOpenShop.Location = new Point(672, 416);
            fraOpenShop.Margin = new Padding(5);
            fraOpenShop.Name = "fraOpenShop";
            fraOpenShop.Padding = new Padding(5);
            fraOpenShop.Size = new Size(410, 152);
            fraOpenShop.TabIndex = 39;
            fraOpenShop.TabStop = false;
            fraOpenShop.Text = "Open Shop";
            fraOpenShop.Visible = false;
            // 
            // btnOpenShopOk
            // 
            btnOpenShopOk.Location = new Point(73, 90);
            btnOpenShopOk.Margin = new Padding(5);
            btnOpenShopOk.Name = "btnOpenShopOk";
            btnOpenShopOk.Padding = new Padding(8, 10, 8, 10);
            btnOpenShopOk.Size = new Size(125, 45);
            btnOpenShopOk.TabIndex = 27;
            btnOpenShopOk.Text = "Ok";
            // 
            // btnOpenShopCancel
            // 
            btnOpenShopCancel.Location = new Point(208, 90);
            btnOpenShopCancel.Margin = new Padding(5);
            btnOpenShopCancel.Name = "btnOpenShopCancel";
            btnOpenShopCancel.Padding = new Padding(8, 10, 8, 10);
            btnOpenShopCancel.Size = new Size(125, 45);
            btnOpenShopCancel.TabIndex = 26;
            btnOpenShopCancel.Text = "Cancel";
            // 
            // cmbOpenShop
            // 
            cmbOpenShop.DrawMode = DrawMode.OwnerDrawFixed;
            cmbOpenShop.FormattingEnabled = true;
            cmbOpenShop.Location = new Point(15, 38);
            cmbOpenShop.Margin = new Padding(5);
            cmbOpenShop.Name = "cmbOpenShop";
            cmbOpenShop.Size = new Size(374, 32);
            cmbOpenShop.TabIndex = 0;
            // 
            // fraSetSelfSwitch
            // 
            fraSetSelfSwitch.BackColor = Color.FromArgb(45, 45, 48);
            fraSetSelfSwitch.BorderColor = Color.FromArgb(90, 90, 90);
            fraSetSelfSwitch.Controls.Add(btnSelfswitchOk);
            fraSetSelfSwitch.Controls.Add(btnSelfswitchCancel);
            fraSetSelfSwitch.Controls.Add(DarkLabel47);
            fraSetSelfSwitch.Controls.Add(cmbSetSelfSwitchTo);
            fraSetSelfSwitch.Controls.Add(DarkLabel46);
            fraSetSelfSwitch.Controls.Add(cmbSetSelfSwitch);
            fraSetSelfSwitch.ForeColor = Color.Gainsboro;
            fraSetSelfSwitch.Location = new Point(668, 347);
            fraSetSelfSwitch.Margin = new Padding(5);
            fraSetSelfSwitch.Name = "fraSetSelfSwitch";
            fraSetSelfSwitch.Padding = new Padding(5);
            fraSetSelfSwitch.Size = new Size(410, 191);
            fraSetSelfSwitch.TabIndex = 29;
            fraSetSelfSwitch.TabStop = false;
            fraSetSelfSwitch.Text = "Self Switches";
            fraSetSelfSwitch.Visible = false;
            // 
            // btnSelfswitchOk
            // 
            btnSelfswitchOk.Location = new Point(140, 140);
            btnSelfswitchOk.Margin = new Padding(5);
            btnSelfswitchOk.Name = "btnSelfswitchOk";
            btnSelfswitchOk.Padding = new Padding(8, 10, 8, 10);
            btnSelfswitchOk.Size = new Size(125, 45);
            btnSelfswitchOk.TabIndex = 27;
            btnSelfswitchOk.Text = "Ok";
            // 
            // btnSelfswitchCancel
            // 
            btnSelfswitchCancel.Location = new Point(275, 140);
            btnSelfswitchCancel.Margin = new Padding(5);
            btnSelfswitchCancel.Name = "btnSelfswitchCancel";
            btnSelfswitchCancel.Padding = new Padding(8, 10, 8, 10);
            btnSelfswitchCancel.Size = new Size(125, 45);
            btnSelfswitchCancel.TabIndex = 26;
            btnSelfswitchCancel.Text = "Cancel";
            // 
            // DarkLabel47
            // 
            DarkLabel47.AutoSize = true;
            DarkLabel47.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel47.Location = new Point(10, 95);
            DarkLabel47.Margin = new Padding(5, 0, 5, 0);
            DarkLabel47.Name = "DarkLabel47";
            DarkLabel47.Size = new Size(60, 25);
            DarkLabel47.TabIndex = 3;
            DarkLabel47.Text = "Set To";
            // 
            // cmbSetSelfSwitchTo
            // 
            cmbSetSelfSwitchTo.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSetSelfSwitchTo.FormattingEnabled = true;
            cmbSetSelfSwitchTo.Items.AddRange(new object[] { "Off", "On" });
            cmbSetSelfSwitchTo.Location = new Point(120, 88);
            cmbSetSelfSwitchTo.Margin = new Padding(5);
            cmbSetSelfSwitchTo.Name = "cmbSetSelfSwitchTo";
            cmbSetSelfSwitchTo.Size = new Size(277, 32);
            cmbSetSelfSwitchTo.TabIndex = 2;
            // 
            // DarkLabel46
            // 
            DarkLabel46.AutoSize = true;
            DarkLabel46.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel46.Location = new Point(10, 41);
            DarkLabel46.Margin = new Padding(5, 0, 5, 0);
            DarkLabel46.Name = "DarkLabel46";
            DarkLabel46.Size = new Size(101, 25);
            DarkLabel46.TabIndex = 1;
            DarkLabel46.Text = "Self Switch:";
            // 
            // cmbSetSelfSwitch
            // 
            cmbSetSelfSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSetSelfSwitch.FormattingEnabled = true;
            cmbSetSelfSwitch.Location = new Point(120, 37);
            cmbSetSelfSwitch.Margin = new Padding(5);
            cmbSetSelfSwitch.Name = "cmbSetSelfSwitch";
            cmbSetSelfSwitch.Size = new Size(277, 32);
            cmbSetSelfSwitch.TabIndex = 0;
            // 
            // fraPlaySound
            // 
            fraPlaySound.BackColor = Color.FromArgb(45, 45, 48);
            fraPlaySound.BorderColor = Color.FromArgb(90, 90, 90);
            fraPlaySound.Controls.Add(btnPlaySoundOk);
            fraPlaySound.Controls.Add(btnPlaySoundCancel);
            fraPlaySound.Controls.Add(cmbPlaySound);
            fraPlaySound.ForeColor = Color.Gainsboro;
            fraPlaySound.Location = new Point(668, 345);
            fraPlaySound.Margin = new Padding(5);
            fraPlaySound.Name = "fraPlaySound";
            fraPlaySound.Padding = new Padding(5);
            fraPlaySound.Size = new Size(410, 147);
            fraPlaySound.TabIndex = 26;
            fraPlaySound.TabStop = false;
            fraPlaySound.Text = "Play Sound";
            fraPlaySound.Visible = false;
            // 
            // btnPlaySoundOk
            // 
            btnPlaySoundOk.Location = new Point(140, 88);
            btnPlaySoundOk.Margin = new Padding(5);
            btnPlaySoundOk.Name = "btnPlaySoundOk";
            btnPlaySoundOk.Padding = new Padding(8, 10, 8, 10);
            btnPlaySoundOk.Size = new Size(125, 45);
            btnPlaySoundOk.TabIndex = 27;
            btnPlaySoundOk.Text = "Ok";
            // 
            // btnPlaySoundCancel
            // 
            btnPlaySoundCancel.Location = new Point(275, 88);
            btnPlaySoundCancel.Margin = new Padding(5);
            btnPlaySoundCancel.Name = "btnPlaySoundCancel";
            btnPlaySoundCancel.Padding = new Padding(8, 10, 8, 10);
            btnPlaySoundCancel.Size = new Size(125, 45);
            btnPlaySoundCancel.TabIndex = 26;
            btnPlaySoundCancel.Text = "Cancel";
            // 
            // cmbPlaySound
            // 
            cmbPlaySound.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlaySound.FormattingEnabled = true;
            cmbPlaySound.Location = new Point(10, 37);
            cmbPlaySound.Margin = new Padding(5);
            cmbPlaySound.Name = "cmbPlaySound";
            cmbPlaySound.Size = new Size(387, 32);
            cmbPlaySound.TabIndex = 0;
            // 
            // fraChangePK
            // 
            fraChangePK.BackColor = Color.FromArgb(45, 45, 48);
            fraChangePK.BorderColor = Color.FromArgb(90, 90, 90);
            fraChangePK.Controls.Add(btnChangePkOk);
            fraChangePK.Controls.Add(btnChangePkCancel);
            fraChangePK.Controls.Add(cmbSetPK);
            fraChangePK.ForeColor = Color.Gainsboro;
            fraChangePK.Location = new Point(668, 200);
            fraChangePK.Margin = new Padding(5);
            fraChangePK.Name = "fraChangePK";
            fraChangePK.Padding = new Padding(5);
            fraChangePK.Size = new Size(410, 145);
            fraChangePK.TabIndex = 25;
            fraChangePK.TabStop = false;
            fraChangePK.Text = "Set Player PK";
            fraChangePK.Visible = false;
            // 
            // btnChangePkOk
            // 
            btnChangePkOk.Location = new Point(133, 88);
            btnChangePkOk.Margin = new Padding(5);
            btnChangePkOk.Name = "btnChangePkOk";
            btnChangePkOk.Padding = new Padding(8, 10, 8, 10);
            btnChangePkOk.Size = new Size(125, 45);
            btnChangePkOk.TabIndex = 27;
            btnChangePkOk.Text = "Ok";
            // 
            // btnChangePkCancel
            // 
            btnChangePkCancel.Location = new Point(268, 88);
            btnChangePkCancel.Margin = new Padding(5);
            btnChangePkCancel.Name = "btnChangePkCancel";
            btnChangePkCancel.Padding = new Padding(8, 10, 8, 10);
            btnChangePkCancel.Size = new Size(125, 45);
            btnChangePkCancel.TabIndex = 26;
            btnChangePkCancel.Text = "Cancel";
            // 
            // cmbSetPK
            // 
            cmbSetPK.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSetPK.FormattingEnabled = true;
            cmbSetPK.Items.AddRange(new object[] { "No", "Yes" });
            cmbSetPK.Location = new Point(17, 37);
            cmbSetPK.Margin = new Padding(5);
            cmbSetPK.Name = "cmbSetPK";
            cmbSetPK.Size = new Size(374, 32);
            cmbSetPK.TabIndex = 18;
            // 
            // fraCreateLabel
            // 
            fraCreateLabel.BackColor = Color.FromArgb(45, 45, 48);
            fraCreateLabel.BorderColor = Color.FromArgb(90, 90, 90);
            fraCreateLabel.Controls.Add(btnCreatelabelOk);
            fraCreateLabel.Controls.Add(btnCreatelabelCancel);
            fraCreateLabel.Controls.Add(txtLabelName);
            fraCreateLabel.Controls.Add(lblLabelName);
            fraCreateLabel.ForeColor = Color.Gainsboro;
            fraCreateLabel.Location = new Point(668, 253);
            fraCreateLabel.Margin = new Padding(5);
            fraCreateLabel.Name = "fraCreateLabel";
            fraCreateLabel.Padding = new Padding(5);
            fraCreateLabel.Size = new Size(410, 141);
            fraCreateLabel.TabIndex = 24;
            fraCreateLabel.TabStop = false;
            fraCreateLabel.Text = "Create Label";
            fraCreateLabel.Visible = false;
            // 
            // btnCreatelabelOk
            // 
            btnCreatelabelOk.Location = new Point(140, 87);
            btnCreatelabelOk.Margin = new Padding(5);
            btnCreatelabelOk.Name = "btnCreatelabelOk";
            btnCreatelabelOk.Padding = new Padding(8, 10, 8, 10);
            btnCreatelabelOk.Size = new Size(125, 45);
            btnCreatelabelOk.TabIndex = 27;
            btnCreatelabelOk.Text = "Ok";
            // 
            // btnCreatelabelCancel
            // 
            btnCreatelabelCancel.Location = new Point(275, 87);
            btnCreatelabelCancel.Margin = new Padding(5);
            btnCreatelabelCancel.Name = "btnCreatelabelCancel";
            btnCreatelabelCancel.Padding = new Padding(8, 10, 8, 10);
            btnCreatelabelCancel.Size = new Size(125, 45);
            btnCreatelabelCancel.TabIndex = 26;
            btnCreatelabelCancel.Text = "Cancel";
            // 
            // txtLabelName
            // 
            txtLabelName.BackColor = Color.FromArgb(69, 73, 74);
            txtLabelName.BorderStyle = BorderStyle.FixedSingle;
            txtLabelName.ForeColor = Color.FromArgb(220, 220, 220);
            txtLabelName.Location = new Point(133, 37);
            txtLabelName.Margin = new Padding(5);
            txtLabelName.Name = "txtLabelName";
            txtLabelName.Size = new Size(265, 31);
            txtLabelName.TabIndex = 1;
            // 
            // lblLabelName
            // 
            lblLabelName.AutoSize = true;
            lblLabelName.ForeColor = Color.FromArgb(220, 220, 220);
            lblLabelName.Location = new Point(12, 40);
            lblLabelName.Margin = new Padding(5, 0, 5, 0);
            lblLabelName.Name = "lblLabelName";
            lblLabelName.Size = new Size(109, 25);
            lblLabelName.TabIndex = 0;
            lblLabelName.Text = "Label Name:";
            // 
            // fraChangeJob
            // 
            fraChangeJob.BackColor = Color.FromArgb(45, 45, 48);
            fraChangeJob.BorderColor = Color.FromArgb(90, 90, 90);
            fraChangeJob.Controls.Add(btnChangeJobOk);
            fraChangeJob.Controls.Add(btnChangeJobCancel);
            fraChangeJob.Controls.Add(cmbChangeJob);
            fraChangeJob.Controls.Add(DarkLabel38);
            fraChangeJob.ForeColor = Color.Gainsboro;
            fraChangeJob.Location = new Point(668, 210);
            fraChangeJob.Margin = new Padding(5);
            fraChangeJob.Name = "fraChangeJob";
            fraChangeJob.Padding = new Padding(5);
            fraChangeJob.Size = new Size(410, 147);
            fraChangeJob.TabIndex = 23;
            fraChangeJob.TabStop = false;
            fraChangeJob.Text = "Change Player Job";
            fraChangeJob.Visible = false;
            // 
            // btnChangeJobOk
            // 
            btnChangeJobOk.Location = new Point(140, 88);
            btnChangeJobOk.Margin = new Padding(5);
            btnChangeJobOk.Name = "btnChangeJobOk";
            btnChangeJobOk.Padding = new Padding(8, 10, 8, 10);
            btnChangeJobOk.Size = new Size(125, 45);
            btnChangeJobOk.TabIndex = 27;
            btnChangeJobOk.Text = "Ok";
            // 
            // btnChangeJobCancel
            // 
            btnChangeJobCancel.Location = new Point(275, 88);
            btnChangeJobCancel.Margin = new Padding(5);
            btnChangeJobCancel.Name = "btnChangeJobCancel";
            btnChangeJobCancel.Padding = new Padding(8, 10, 8, 10);
            btnChangeJobCancel.Size = new Size(125, 45);
            btnChangeJobCancel.TabIndex = 26;
            btnChangeJobCancel.Text = "Cancel";
            // 
            // cmbChangeJob
            // 
            cmbChangeJob.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChangeJob.FormattingEnabled = true;
            cmbChangeJob.Location = new Point(82, 37);
            cmbChangeJob.Margin = new Padding(5);
            cmbChangeJob.Name = "cmbChangeJob";
            cmbChangeJob.Size = new Size(316, 32);
            cmbChangeJob.TabIndex = 1;
            // 
            // DarkLabel38
            // 
            DarkLabel38.AutoSize = true;
            DarkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel38.Location = new Point(13, 41);
            DarkLabel38.Margin = new Padding(5, 0, 5, 0);
            DarkLabel38.Name = "DarkLabel38";
            DarkLabel38.Size = new Size(44, 25);
            DarkLabel38.TabIndex = 0;
            DarkLabel38.Text = "Job:";
            // 
            // fraChangeSkills
            // 
            fraChangeSkills.BackColor = Color.FromArgb(45, 45, 48);
            fraChangeSkills.BorderColor = Color.FromArgb(90, 90, 90);
            fraChangeSkills.Controls.Add(btnChangeSkillsOk);
            fraChangeSkills.Controls.Add(btnChangeSkillsCancel);
            fraChangeSkills.Controls.Add(optChangeSkillsRemove);
            fraChangeSkills.Controls.Add(optChangeSkillsAdd);
            fraChangeSkills.Controls.Add(cmbChangeSkills);
            fraChangeSkills.Controls.Add(DarkLabel37);
            fraChangeSkills.ForeColor = Color.Gainsboro;
            fraChangeSkills.Location = new Point(668, 209);
            fraChangeSkills.Margin = new Padding(5);
            fraChangeSkills.Name = "fraChangeSkills";
            fraChangeSkills.Padding = new Padding(5);
            fraChangeSkills.Size = new Size(410, 188);
            fraChangeSkills.TabIndex = 22;
            fraChangeSkills.TabStop = false;
            fraChangeSkills.Text = "Change Player Skills";
            fraChangeSkills.Visible = false;
            // 
            // btnChangeSkillsOk
            // 
            btnChangeSkillsOk.Location = new Point(140, 128);
            btnChangeSkillsOk.Margin = new Padding(5);
            btnChangeSkillsOk.Name = "btnChangeSkillsOk";
            btnChangeSkillsOk.Padding = new Padding(8, 10, 8, 10);
            btnChangeSkillsOk.Size = new Size(125, 45);
            btnChangeSkillsOk.TabIndex = 27;
            btnChangeSkillsOk.Text = "Ok";
            // 
            // btnChangeSkillsCancel
            // 
            btnChangeSkillsCancel.Location = new Point(275, 128);
            btnChangeSkillsCancel.Margin = new Padding(5);
            btnChangeSkillsCancel.Name = "btnChangeSkillsCancel";
            btnChangeSkillsCancel.Padding = new Padding(8, 10, 8, 10);
            btnChangeSkillsCancel.Size = new Size(125, 45);
            btnChangeSkillsCancel.TabIndex = 26;
            btnChangeSkillsCancel.Text = "Cancel";
            // 
            // optChangeSkillsRemove
            // 
            optChangeSkillsRemove.AutoSize = true;
            optChangeSkillsRemove.Location = new Point(245, 85);
            optChangeSkillsRemove.Margin = new Padding(5);
            optChangeSkillsRemove.Name = "optChangeSkillsRemove";
            optChangeSkillsRemove.Size = new Size(89, 29);
            optChangeSkillsRemove.TabIndex = 3;
            optChangeSkillsRemove.TabStop = true;
            optChangeSkillsRemove.Text = "Forget";
            // 
            // optChangeSkillsAdd
            // 
            optChangeSkillsAdd.AutoSize = true;
            optChangeSkillsAdd.Location = new Point(108, 85);
            optChangeSkillsAdd.Margin = new Padding(5);
            optChangeSkillsAdd.Name = "optChangeSkillsAdd";
            optChangeSkillsAdd.Size = new Size(80, 29);
            optChangeSkillsAdd.TabIndex = 2;
            optChangeSkillsAdd.TabStop = true;
            optChangeSkillsAdd.Text = "Teach";
            // 
            // cmbChangeSkills
            // 
            cmbChangeSkills.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChangeSkills.FormattingEnabled = true;
            cmbChangeSkills.Location = new Point(68, 34);
            cmbChangeSkills.Margin = new Padding(5);
            cmbChangeSkills.Name = "cmbChangeSkills";
            cmbChangeSkills.Size = new Size(327, 32);
            cmbChangeSkills.TabIndex = 1;
            // 
            // DarkLabel37
            // 
            DarkLabel37.AutoSize = true;
            DarkLabel37.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel37.Location = new Point(10, 38);
            DarkLabel37.Margin = new Padding(5, 0, 5, 0);
            DarkLabel37.Name = "DarkLabel37";
            DarkLabel37.Size = new Size(47, 25);
            DarkLabel37.TabIndex = 0;
            DarkLabel37.Text = "Skill:";
            // 
            // fraPlayerSwitch
            // 
            fraPlayerSwitch.BackColor = Color.FromArgb(45, 45, 48);
            fraPlayerSwitch.BorderColor = Color.FromArgb(90, 90, 90);
            fraPlayerSwitch.Controls.Add(btnSetPlayerSwitchOk);
            fraPlayerSwitch.Controls.Add(btnSetPlayerswitchCancel);
            fraPlayerSwitch.Controls.Add(cmbPlayerSwitchSet);
            fraPlayerSwitch.Controls.Add(DarkLabel23);
            fraPlayerSwitch.Controls.Add(cmbSwitch);
            fraPlayerSwitch.Controls.Add(DarkLabel22);
            fraPlayerSwitch.ForeColor = Color.Gainsboro;
            fraPlayerSwitch.Location = new Point(355, 750);
            fraPlayerSwitch.Margin = new Padding(5);
            fraPlayerSwitch.Name = "fraPlayerSwitch";
            fraPlayerSwitch.Padding = new Padding(5);
            fraPlayerSwitch.Size = new Size(303, 191);
            fraPlayerSwitch.TabIndex = 2;
            fraPlayerSwitch.TabStop = false;
            fraPlayerSwitch.Text = "Change Items";
            fraPlayerSwitch.Visible = false;
            // 
            // btnSetPlayerSwitchOk
            // 
            btnSetPlayerSwitchOk.Location = new Point(33, 138);
            btnSetPlayerSwitchOk.Margin = new Padding(5);
            btnSetPlayerSwitchOk.Name = "btnSetPlayerSwitchOk";
            btnSetPlayerSwitchOk.Padding = new Padding(8, 10, 8, 10);
            btnSetPlayerSwitchOk.Size = new Size(125, 45);
            btnSetPlayerSwitchOk.TabIndex = 9;
            btnSetPlayerSwitchOk.Text = "Ok";
            // 
            // btnSetPlayerswitchCancel
            // 
            btnSetPlayerswitchCancel.Location = new Point(168, 138);
            btnSetPlayerswitchCancel.Margin = new Padding(5);
            btnSetPlayerswitchCancel.Name = "btnSetPlayerswitchCancel";
            btnSetPlayerswitchCancel.Padding = new Padding(8, 10, 8, 10);
            btnSetPlayerswitchCancel.Size = new Size(125, 45);
            btnSetPlayerswitchCancel.TabIndex = 8;
            btnSetPlayerswitchCancel.Text = "Cancel";
            // 
            // cmbPlayerSwitchSet
            // 
            cmbPlayerSwitchSet.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayerSwitchSet.FormattingEnabled = true;
            cmbPlayerSwitchSet.Items.AddRange(new object[] { "False", "True" });
            cmbPlayerSwitchSet.Location = new Point(85, 78);
            cmbPlayerSwitchSet.Margin = new Padding(5);
            cmbPlayerSwitchSet.Name = "cmbPlayerSwitchSet";
            cmbPlayerSwitchSet.Size = new Size(206, 32);
            cmbPlayerSwitchSet.TabIndex = 3;
            // 
            // DarkLabel23
            // 
            DarkLabel23.AutoSize = true;
            DarkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel23.Location = new Point(10, 88);
            DarkLabel23.Margin = new Padding(5, 0, 5, 0);
            DarkLabel23.Name = "DarkLabel23";
            DarkLabel23.Size = new Size(59, 25);
            DarkLabel23.TabIndex = 2;
            DarkLabel23.Text = "Set to";
            // 
            // cmbSwitch
            // 
            cmbSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSwitch.FormattingEnabled = true;
            cmbSwitch.Location = new Point(85, 25);
            cmbSwitch.Margin = new Padding(5);
            cmbSwitch.Name = "cmbSwitch";
            cmbSwitch.Size = new Size(206, 32);
            cmbSwitch.TabIndex = 1;
            // 
            // DarkLabel22
            // 
            DarkLabel22.AutoSize = true;
            DarkLabel22.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel22.Location = new Point(10, 30);
            DarkLabel22.Margin = new Padding(5, 0, 5, 0);
            DarkLabel22.Name = "DarkLabel22";
            DarkLabel22.Size = new Size(63, 25);
            DarkLabel22.TabIndex = 0;
            DarkLabel22.Text = "Switch";
            // 
            // fraSetWait
            // 
            fraSetWait.BackColor = Color.FromArgb(45, 45, 48);
            fraSetWait.BorderColor = Color.FromArgb(90, 90, 90);
            fraSetWait.Controls.Add(btnSetWaitOk);
            fraSetWait.Controls.Add(btnSetWaitCancel);
            fraSetWait.Controls.Add(DarkLabel74);
            fraSetWait.Controls.Add(DarkLabel72);
            fraSetWait.Controls.Add(DarkLabel73);
            fraSetWait.Controls.Add(nudWaitAmount);
            fraSetWait.ForeColor = Color.Gainsboro;
            fraSetWait.Location = new Point(668, 509);
            fraSetWait.Margin = new Padding(5);
            fraSetWait.Name = "fraSetWait";
            fraSetWait.Padding = new Padding(5);
            fraSetWait.Size = new Size(413, 172);
            fraSetWait.TabIndex = 41;
            fraSetWait.TabStop = false;
            fraSetWait.Text = "Wait...";
            fraSetWait.Visible = false;
            // 
            // btnSetWaitOk
            // 
            btnSetWaitOk.Location = new Point(83, 112);
            btnSetWaitOk.Margin = new Padding(5);
            btnSetWaitOk.Name = "btnSetWaitOk";
            btnSetWaitOk.Padding = new Padding(8, 10, 8, 10);
            btnSetWaitOk.Size = new Size(125, 45);
            btnSetWaitOk.TabIndex = 37;
            btnSetWaitOk.Text = "Ok";
            // 
            // btnSetWaitCancel
            // 
            btnSetWaitCancel.Location = new Point(218, 112);
            btnSetWaitCancel.Margin = new Padding(5);
            btnSetWaitCancel.Name = "btnSetWaitCancel";
            btnSetWaitCancel.Padding = new Padding(8, 10, 8, 10);
            btnSetWaitCancel.Size = new Size(125, 45);
            btnSetWaitCancel.TabIndex = 36;
            btnSetWaitCancel.Text = "Cancel";
            // 
            // DarkLabel74
            // 
            DarkLabel74.AutoSize = true;
            DarkLabel74.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel74.Location = new Point(117, 80);
            DarkLabel74.Margin = new Padding(5, 0, 5, 0);
            DarkLabel74.Name = "DarkLabel74";
            DarkLabel74.Size = new Size(187, 25);
            DarkLabel74.TabIndex = 35;
            DarkLabel74.Text = "Hint: 1000 Ms = 1 Sec";
            // 
            // DarkLabel72
            // 
            DarkLabel72.AutoSize = true;
            DarkLabel72.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel72.Location = new Point(363, 45);
            DarkLabel72.Margin = new Padding(5, 0, 5, 0);
            DarkLabel72.Name = "DarkLabel72";
            DarkLabel72.Size = new Size(36, 25);
            DarkLabel72.TabIndex = 34;
            DarkLabel72.Text = "Ms";
            // 
            // DarkLabel73
            // 
            DarkLabel73.AutoSize = true;
            DarkLabel73.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel73.Location = new Point(25, 45);
            DarkLabel73.Margin = new Padding(5, 0, 5, 0);
            DarkLabel73.Name = "DarkLabel73";
            DarkLabel73.Size = new Size(47, 25);
            DarkLabel73.TabIndex = 33;
            DarkLabel73.Text = "Wait";
            // 
            // nudWaitAmount
            // 
            nudWaitAmount.Location = new Point(83, 37);
            nudWaitAmount.Margin = new Padding(5);
            nudWaitAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudWaitAmount.Name = "nudWaitAmount";
            nudWaitAmount.Size = new Size(272, 31);
            nudWaitAmount.TabIndex = 32;
            // 
            // fraMoveRouteWait
            // 
            fraMoveRouteWait.BackColor = Color.FromArgb(45, 45, 48);
            fraMoveRouteWait.BorderColor = Color.FromArgb(90, 90, 90);
            fraMoveRouteWait.Controls.Add(btnMoveWaitCancel);
            fraMoveRouteWait.Controls.Add(btnMoveWaitOk);
            fraMoveRouteWait.Controls.Add(DarkLabel79);
            fraMoveRouteWait.Controls.Add(cmbMoveWait);
            fraMoveRouteWait.ForeColor = Color.Gainsboro;
            fraMoveRouteWait.Location = new Point(668, 952);
            fraMoveRouteWait.Margin = new Padding(5);
            fraMoveRouteWait.Name = "fraMoveRouteWait";
            fraMoveRouteWait.Padding = new Padding(5);
            fraMoveRouteWait.Size = new Size(413, 145);
            fraMoveRouteWait.TabIndex = 48;
            fraMoveRouteWait.TabStop = false;
            fraMoveRouteWait.Text = "Move Route Wait";
            fraMoveRouteWait.Visible = false;
            // 
            // btnMoveWaitCancel
            // 
            btnMoveWaitCancel.Location = new Point(278, 88);
            btnMoveWaitCancel.Margin = new Padding(5);
            btnMoveWaitCancel.Name = "btnMoveWaitCancel";
            btnMoveWaitCancel.Padding = new Padding(8, 10, 8, 10);
            btnMoveWaitCancel.Size = new Size(125, 45);
            btnMoveWaitCancel.TabIndex = 26;
            btnMoveWaitCancel.Text = "Cancel";
            // 
            // btnMoveWaitOk
            // 
            btnMoveWaitOk.Location = new Point(143, 88);
            btnMoveWaitOk.Margin = new Padding(5);
            btnMoveWaitOk.Name = "btnMoveWaitOk";
            btnMoveWaitOk.Padding = new Padding(8, 10, 8, 10);
            btnMoveWaitOk.Size = new Size(125, 45);
            btnMoveWaitOk.TabIndex = 27;
            btnMoveWaitOk.Text = "Ok";
            // 
            // DarkLabel79
            // 
            DarkLabel79.AutoSize = true;
            DarkLabel79.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel79.Location = new Point(12, 41);
            DarkLabel79.Margin = new Padding(5, 0, 5, 0);
            DarkLabel79.Name = "DarkLabel79";
            DarkLabel79.Size = new Size(59, 25);
            DarkLabel79.TabIndex = 1;
            DarkLabel79.Text = "Event:";
            // 
            // cmbMoveWait
            // 
            cmbMoveWait.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMoveWait.FormattingEnabled = true;
            cmbMoveWait.Location = new Point(85, 37);
            cmbMoveWait.Margin = new Padding(5);
            cmbMoveWait.Name = "cmbMoveWait";
            cmbMoveWait.Size = new Size(316, 32);
            cmbMoveWait.TabIndex = 0;
            // 
            // fraCustomScript
            // 
            fraCustomScript.BackColor = Color.FromArgb(45, 45, 48);
            fraCustomScript.BorderColor = Color.FromArgb(90, 90, 90);
            fraCustomScript.Controls.Add(nudCustomScript);
            fraCustomScript.Controls.Add(DarkLabel78);
            fraCustomScript.Controls.Add(btnCustomScriptCancel);
            fraCustomScript.Controls.Add(btnCustomScriptOk);
            fraCustomScript.ForeColor = Color.Gainsboro;
            fraCustomScript.Location = new Point(668, 762);
            fraCustomScript.Margin = new Padding(5);
            fraCustomScript.Name = "fraCustomScript";
            fraCustomScript.Padding = new Padding(5);
            fraCustomScript.Size = new Size(413, 184);
            fraCustomScript.TabIndex = 47;
            fraCustomScript.TabStop = false;
            fraCustomScript.Text = "Execute Custom Script";
            fraCustomScript.Visible = false;
            // 
            // nudCustomScript
            // 
            nudCustomScript.Location = new Point(112, 37);
            nudCustomScript.Margin = new Padding(5);
            nudCustomScript.Name = "nudCustomScript";
            nudCustomScript.Size = new Size(282, 31);
            nudCustomScript.TabIndex = 1;
            // 
            // DarkLabel78
            // 
            DarkLabel78.AutoSize = true;
            DarkLabel78.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel78.Location = new Point(17, 40);
            DarkLabel78.Margin = new Padding(5, 0, 5, 0);
            DarkLabel78.Name = "DarkLabel78";
            DarkLabel78.Size = new Size(53, 25);
            DarkLabel78.TabIndex = 0;
            DarkLabel78.Text = "Case:";
            // 
            // btnCustomScriptCancel
            // 
            btnCustomScriptCancel.Location = new Point(268, 87);
            btnCustomScriptCancel.Margin = new Padding(5);
            btnCustomScriptCancel.Name = "btnCustomScriptCancel";
            btnCustomScriptCancel.Padding = new Padding(8, 10, 8, 10);
            btnCustomScriptCancel.Size = new Size(125, 45);
            btnCustomScriptCancel.TabIndex = 24;
            btnCustomScriptCancel.Text = "Cancel";
            // 
            // btnCustomScriptOk
            // 
            btnCustomScriptOk.Location = new Point(133, 87);
            btnCustomScriptOk.Margin = new Padding(5);
            btnCustomScriptOk.Name = "btnCustomScriptOk";
            btnCustomScriptOk.Padding = new Padding(8, 10, 8, 10);
            btnCustomScriptOk.Size = new Size(125, 45);
            btnCustomScriptOk.TabIndex = 25;
            btnCustomScriptOk.Text = "Ok";
            // 
            // fraSpawnNPC
            // 
            fraSpawnNPC.BackColor = Color.FromArgb(45, 45, 48);
            fraSpawnNPC.BorderColor = Color.FromArgb(90, 90, 90);
            fraSpawnNPC.Controls.Add(btnSpawnNPCOk);
            fraSpawnNPC.Controls.Add(btnSpawnNPCancel);
            fraSpawnNPC.Controls.Add(cmbSpawnNPC);
            fraSpawnNPC.ForeColor = Color.Gainsboro;
            fraSpawnNPC.Location = new Point(668, 791);
            fraSpawnNPC.Margin = new Padding(5);
            fraSpawnNPC.Name = "fraSpawnNPC";
            fraSpawnNPC.Padding = new Padding(5);
            fraSpawnNPC.Size = new Size(413, 148);
            fraSpawnNPC.TabIndex = 46;
            fraSpawnNPC.TabStop = false;
            fraSpawnNPC.Text = "Spawn NPC";
            fraSpawnNPC.Visible = false;
            // 
            // btnSpawnNPCOk
            // 
            btnSpawnNPCOk.Location = new Point(77, 90);
            btnSpawnNPCOk.Margin = new Padding(5);
            btnSpawnNPCOk.Name = "btnSpawnNPCOk";
            btnSpawnNPCOk.Padding = new Padding(8, 10, 8, 10);
            btnSpawnNPCOk.Size = new Size(125, 45);
            btnSpawnNPCOk.TabIndex = 27;
            btnSpawnNPCOk.Text = "Ok";
            // 
            // btnSpawnNPCancel
            // 
            btnSpawnNPCancel.Location = new Point(212, 90);
            btnSpawnNPCancel.Margin = new Padding(5);
            btnSpawnNPCancel.Name = "btnSpawnNPCancel";
            btnSpawnNPCancel.Padding = new Padding(8, 10, 8, 10);
            btnSpawnNPCancel.Size = new Size(125, 45);
            btnSpawnNPCancel.TabIndex = 26;
            btnSpawnNPCancel.Text = "Cancel";
            // 
            // cmbSpawnNPC
            // 
            cmbSpawnNPC.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSpawnNPC.FormattingEnabled = true;
            cmbSpawnNPC.Location = new Point(10, 37);
            cmbSpawnNPC.Margin = new Padding(5);
            cmbSpawnNPC.Name = "cmbSpawnNPC";
            cmbSpawnNPC.Size = new Size(387, 32);
            cmbSpawnNPC.TabIndex = 0;
            // 
            // fraSetWeather
            // 
            fraSetWeather.BackColor = Color.FromArgb(45, 45, 48);
            fraSetWeather.BorderColor = Color.FromArgb(90, 90, 90);
            fraSetWeather.Controls.Add(btnSetWeatherOk);
            fraSetWeather.Controls.Add(btnSetWeatherCancel);
            fraSetWeather.Controls.Add(DarkLabel76);
            fraSetWeather.Controls.Add(nudWeatherIntensity);
            fraSetWeather.Controls.Add(DarkLabel75);
            fraSetWeather.Controls.Add(CmbWeather);
            fraSetWeather.ForeColor = Color.Gainsboro;
            fraSetWeather.Location = new Point(668, 677);
            fraSetWeather.Margin = new Padding(5);
            fraSetWeather.Name = "fraSetWeather";
            fraSetWeather.Padding = new Padding(5);
            fraSetWeather.Size = new Size(413, 184);
            fraSetWeather.TabIndex = 44;
            fraSetWeather.TabStop = false;
            fraSetWeather.Text = "Set Weather";
            fraSetWeather.Visible = false;
            // 
            // btnSetWeatherOk
            // 
            btnSetWeatherOk.Location = new Point(77, 127);
            btnSetWeatherOk.Margin = new Padding(5);
            btnSetWeatherOk.Name = "btnSetWeatherOk";
            btnSetWeatherOk.Padding = new Padding(8, 10, 8, 10);
            btnSetWeatherOk.Size = new Size(125, 45);
            btnSetWeatherOk.TabIndex = 34;
            btnSetWeatherOk.Text = "Ok";
            // 
            // btnSetWeatherCancel
            // 
            btnSetWeatherCancel.Location = new Point(212, 127);
            btnSetWeatherCancel.Margin = new Padding(5);
            btnSetWeatherCancel.Name = "btnSetWeatherCancel";
            btnSetWeatherCancel.Padding = new Padding(8, 10, 8, 10);
            btnSetWeatherCancel.Size = new Size(125, 45);
            btnSetWeatherCancel.TabIndex = 33;
            btnSetWeatherCancel.Text = "Cancel";
            // 
            // DarkLabel76
            // 
            DarkLabel76.AutoSize = true;
            DarkLabel76.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel76.Location = new Point(13, 85);
            DarkLabel76.Margin = new Padding(5, 0, 5, 0);
            DarkLabel76.Name = "DarkLabel76";
            DarkLabel76.Size = new Size(83, 25);
            DarkLabel76.TabIndex = 32;
            DarkLabel76.Text = "Intensity:";
            // 
            // nudWeatherIntensity
            // 
            nudWeatherIntensity.Location = new Point(145, 78);
            nudWeatherIntensity.Margin = new Padding(5);
            nudWeatherIntensity.Name = "nudWeatherIntensity";
            nudWeatherIntensity.Size = new Size(258, 31);
            nudWeatherIntensity.TabIndex = 31;
            // 
            // DarkLabel75
            // 
            DarkLabel75.AutoSize = true;
            DarkLabel75.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel75.Location = new Point(10, 35);
            DarkLabel75.Margin = new Padding(5, 0, 5, 0);
            DarkLabel75.Name = "DarkLabel75";
            DarkLabel75.Size = new Size(119, 25);
            DarkLabel75.TabIndex = 1;
            DarkLabel75.Text = "Weather Type";
            // 
            // CmbWeather
            // 
            CmbWeather.DrawMode = DrawMode.OwnerDrawFixed;
            CmbWeather.FormattingEnabled = true;
            CmbWeather.Items.AddRange(new object[] { "None", "Rain", "Snow", "Hail", "Sand Storm", "Storm" });
            CmbWeather.Location = new Point(143, 28);
            CmbWeather.Margin = new Padding(5);
            CmbWeather.Name = "CmbWeather";
            CmbWeather.Size = new Size(256, 32);
            CmbWeather.TabIndex = 0;
            // 
            // fraGiveExp
            // 
            fraGiveExp.BackColor = Color.FromArgb(45, 45, 48);
            fraGiveExp.BorderColor = Color.FromArgb(90, 90, 90);
            fraGiveExp.Controls.Add(btnGiveExpOk);
            fraGiveExp.Controls.Add(btnGiveExpCancel);
            fraGiveExp.Controls.Add(nudGiveExp);
            fraGiveExp.Controls.Add(DarkLabel77);
            fraGiveExp.ForeColor = Color.Gainsboro;
            fraGiveExp.Location = new Point(668, 677);
            fraGiveExp.Margin = new Padding(5);
            fraGiveExp.Name = "fraGiveExp";
            fraGiveExp.Padding = new Padding(5);
            fraGiveExp.Size = new Size(413, 140);
            fraGiveExp.TabIndex = 45;
            fraGiveExp.TabStop = false;
            fraGiveExp.Text = "Give Experience";
            fraGiveExp.Visible = false;
            // 
            // btnGiveExpOk
            // 
            btnGiveExpOk.Location = new Point(83, 87);
            btnGiveExpOk.Margin = new Padding(5);
            btnGiveExpOk.Name = "btnGiveExpOk";
            btnGiveExpOk.Padding = new Padding(8, 10, 8, 10);
            btnGiveExpOk.Size = new Size(125, 45);
            btnGiveExpOk.TabIndex = 27;
            btnGiveExpOk.Text = "Ok";
            // 
            // btnGiveExpCancel
            // 
            btnGiveExpCancel.Location = new Point(218, 87);
            btnGiveExpCancel.Margin = new Padding(5);
            btnGiveExpCancel.Name = "btnGiveExpCancel";
            btnGiveExpCancel.Padding = new Padding(8, 10, 8, 10);
            btnGiveExpCancel.Size = new Size(125, 45);
            btnGiveExpCancel.TabIndex = 26;
            btnGiveExpCancel.Text = "Cancel";
            // 
            // nudGiveExp
            // 
            nudGiveExp.Location = new Point(128, 37);
            nudGiveExp.Margin = new Padding(5);
            nudGiveExp.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudGiveExp.Name = "nudGiveExp";
            nudGiveExp.Size = new Size(275, 31);
            nudGiveExp.TabIndex = 20;
            // 
            // DarkLabel77
            // 
            DarkLabel77.AutoSize = true;
            DarkLabel77.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel77.Location = new Point(10, 40);
            DarkLabel77.Margin = new Padding(5, 0, 5, 0);
            DarkLabel77.Name = "DarkLabel77";
            DarkLabel77.Size = new Size(83, 25);
            DarkLabel77.TabIndex = 0;
            DarkLabel77.Text = "Give Exp:";
            // 
            // fraSetAccess
            // 
            fraSetAccess.BackColor = Color.FromArgb(45, 45, 48);
            fraSetAccess.BorderColor = Color.FromArgb(90, 90, 90);
            fraSetAccess.Controls.Add(btnSetAccessOk);
            fraSetAccess.Controls.Add(btnSetAccessCancel);
            fraSetAccess.Controls.Add(cmbSetAccess);
            fraSetAccess.ForeColor = Color.Gainsboro;
            fraSetAccess.Location = new Point(668, 678);
            fraSetAccess.Margin = new Padding(5);
            fraSetAccess.Name = "fraSetAccess";
            fraSetAccess.Padding = new Padding(5);
            fraSetAccess.Size = new Size(413, 153);
            fraSetAccess.TabIndex = 42;
            fraSetAccess.TabStop = false;
            fraSetAccess.Text = "Set Access";
            fraSetAccess.Visible = false;
            // 
            // btnSetAccessOk
            // 
            btnSetAccessOk.Location = new Point(77, 91);
            btnSetAccessOk.Margin = new Padding(5);
            btnSetAccessOk.Name = "btnSetAccessOk";
            btnSetAccessOk.Padding = new Padding(8, 10, 8, 10);
            btnSetAccessOk.Size = new Size(125, 45);
            btnSetAccessOk.TabIndex = 27;
            btnSetAccessOk.Text = "Ok";
            // 
            // btnSetAccessCancel
            // 
            btnSetAccessCancel.Location = new Point(212, 91);
            btnSetAccessCancel.Margin = new Padding(5);
            btnSetAccessCancel.Name = "btnSetAccessCancel";
            btnSetAccessCancel.Padding = new Padding(8, 10, 8, 10);
            btnSetAccessCancel.Size = new Size(125, 45);
            btnSetAccessCancel.TabIndex = 26;
            btnSetAccessCancel.Text = "Cancel";
            // 
            // cmbSetAccess
            // 
            cmbSetAccess.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSetAccess.FormattingEnabled = true;
            cmbSetAccess.Items.AddRange(new object[] { "0: Player", "1: Moderator", "2: Mapper", "3: Developer", "4: Owner" });
            cmbSetAccess.Location = new Point(55, 37);
            cmbSetAccess.Margin = new Padding(5);
            cmbSetAccess.Name = "cmbSetAccess";
            cmbSetAccess.Size = new Size(311, 32);
            cmbSetAccess.TabIndex = 0;
            // 
            // fraChangeGender
            // 
            fraChangeGender.BackColor = Color.FromArgb(45, 45, 48);
            fraChangeGender.BorderColor = Color.FromArgb(90, 90, 90);
            fraChangeGender.Controls.Add(btnChangeGenderOk);
            fraChangeGender.Controls.Add(btnChangeGenderCancel);
            fraChangeGender.Controls.Add(optChangeSexFemale);
            fraChangeGender.Controls.Add(optChangeSexMale);
            fraChangeGender.ForeColor = Color.Gainsboro;
            fraChangeGender.Location = new Point(668, 700);
            fraChangeGender.Margin = new Padding(5);
            fraChangeGender.Name = "fraChangeGender";
            fraChangeGender.Padding = new Padding(5);
            fraChangeGender.Size = new Size(413, 138);
            fraChangeGender.TabIndex = 37;
            fraChangeGender.TabStop = false;
            fraChangeGender.Text = "Change Player Gender";
            fraChangeGender.Visible = false;
            // 
            // btnChangeGenderOk
            // 
            btnChangeGenderOk.Location = new Point(65, 80);
            btnChangeGenderOk.Margin = new Padding(5);
            btnChangeGenderOk.Name = "btnChangeGenderOk";
            btnChangeGenderOk.Padding = new Padding(8, 10, 8, 10);
            btnChangeGenderOk.Size = new Size(125, 45);
            btnChangeGenderOk.TabIndex = 27;
            btnChangeGenderOk.Text = "Ok";
            // 
            // btnChangeGenderCancel
            // 
            btnChangeGenderCancel.Location = new Point(200, 80);
            btnChangeGenderCancel.Margin = new Padding(5);
            btnChangeGenderCancel.Name = "btnChangeGenderCancel";
            btnChangeGenderCancel.Padding = new Padding(8, 10, 8, 10);
            btnChangeGenderCancel.Size = new Size(125, 45);
            btnChangeGenderCancel.TabIndex = 26;
            btnChangeGenderCancel.Text = "Cancel";
            // 
            // optChangeSexFemale
            // 
            optChangeSexFemale.AutoSize = true;
            optChangeSexFemale.Location = new Point(235, 37);
            optChangeSexFemale.Margin = new Padding(5);
            optChangeSexFemale.Name = "optChangeSexFemale";
            optChangeSexFemale.Size = new Size(93, 29);
            optChangeSexFemale.TabIndex = 1;
            optChangeSexFemale.TabStop = true;
            optChangeSexFemale.Text = "Female";
            // 
            // optChangeSexMale
            // 
            optChangeSexMale.AutoSize = true;
            optChangeSexMale.Location = new Point(87, 37);
            optChangeSexMale.Margin = new Padding(5);
            optChangeSexMale.Name = "optChangeSexMale";
            optChangeSexMale.Size = new Size(75, 29);
            optChangeSexMale.TabIndex = 0;
            optChangeSexMale.TabStop = true;
            optChangeSexMale.Text = "Male";
            // 
            // fraShowChoices
            // 
            fraShowChoices.BackColor = Color.FromArgb(45, 45, 48);
            fraShowChoices.BorderColor = Color.FromArgb(90, 90, 90);
            fraShowChoices.Controls.Add(txtChoices4);
            fraShowChoices.Controls.Add(txtChoices3);
            fraShowChoices.Controls.Add(txtChoices2);
            fraShowChoices.Controls.Add(txtChoices1);
            fraShowChoices.Controls.Add(DarkLabel56);
            fraShowChoices.Controls.Add(DarkLabel57);
            fraShowChoices.Controls.Add(DarkLabel55);
            fraShowChoices.Controls.Add(DarkLabel54);
            fraShowChoices.Controls.Add(DarkLabel52);
            fraShowChoices.Controls.Add(txtChoicePrompt);
            fraShowChoices.Controls.Add(btnShowChoicesOk);
            fraShowChoices.Controls.Add(btnShowChoicesCancel);
            fraShowChoices.ForeColor = Color.Gainsboro;
            fraShowChoices.Location = new Point(668, 198);
            fraShowChoices.Margin = new Padding(5);
            fraShowChoices.Name = "fraShowChoices";
            fraShowChoices.Padding = new Padding(5);
            fraShowChoices.Size = new Size(413, 640);
            fraShowChoices.TabIndex = 32;
            fraShowChoices.TabStop = false;
            fraShowChoices.Text = "Show Choices";
            fraShowChoices.Visible = false;
            // 
            // txtChoices4
            // 
            txtChoices4.BackColor = Color.FromArgb(69, 73, 74);
            txtChoices4.BorderStyle = BorderStyle.FixedSingle;
            txtChoices4.ForeColor = Color.FromArgb(220, 220, 220);
            txtChoices4.Location = new Point(235, 335);
            txtChoices4.Margin = new Padding(5);
            txtChoices4.Name = "txtChoices4";
            txtChoices4.Size = new Size(165, 31);
            txtChoices4.TabIndex = 34;
            // 
            // txtChoices3
            // 
            txtChoices3.BackColor = Color.FromArgb(69, 73, 74);
            txtChoices3.BorderStyle = BorderStyle.FixedSingle;
            txtChoices3.ForeColor = Color.FromArgb(220, 220, 220);
            txtChoices3.Location = new Point(10, 334);
            txtChoices3.Margin = new Padding(5);
            txtChoices3.Name = "txtChoices3";
            txtChoices3.Size = new Size(165, 31);
            txtChoices3.TabIndex = 33;
            // 
            // txtChoices2
            // 
            txtChoices2.BackColor = Color.FromArgb(69, 73, 74);
            txtChoices2.BorderStyle = BorderStyle.FixedSingle;
            txtChoices2.ForeColor = Color.FromArgb(220, 220, 220);
            txtChoices2.Location = new Point(235, 259);
            txtChoices2.Margin = new Padding(5);
            txtChoices2.Name = "txtChoices2";
            txtChoices2.Size = new Size(165, 31);
            txtChoices2.TabIndex = 32;
            // 
            // txtChoices1
            // 
            txtChoices1.BackColor = Color.FromArgb(69, 73, 74);
            txtChoices1.BorderStyle = BorderStyle.FixedSingle;
            txtChoices1.ForeColor = Color.FromArgb(220, 220, 220);
            txtChoices1.Location = new Point(10, 259);
            txtChoices1.Margin = new Padding(5);
            txtChoices1.Name = "txtChoices1";
            txtChoices1.Size = new Size(165, 31);
            txtChoices1.TabIndex = 31;
            // 
            // DarkLabel56
            // 
            DarkLabel56.AutoSize = true;
            DarkLabel56.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel56.Location = new Point(230, 302);
            DarkLabel56.Margin = new Padding(5, 0, 5, 0);
            DarkLabel56.Name = "DarkLabel56";
            DarkLabel56.Size = new Size(80, 25);
            DarkLabel56.TabIndex = 30;
            DarkLabel56.Text = "Choice 4";
            // 
            // DarkLabel57
            // 
            DarkLabel57.AutoSize = true;
            DarkLabel57.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel57.Location = new Point(12, 302);
            DarkLabel57.Margin = new Padding(5, 0, 5, 0);
            DarkLabel57.Name = "DarkLabel57";
            DarkLabel57.Size = new Size(80, 25);
            DarkLabel57.TabIndex = 29;
            DarkLabel57.Text = "Choice 3";
            // 
            // DarkLabel55
            // 
            DarkLabel55.AutoSize = true;
            DarkLabel55.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel55.Location = new Point(230, 227);
            DarkLabel55.Margin = new Padding(5, 0, 5, 0);
            DarkLabel55.Name = "DarkLabel55";
            DarkLabel55.Size = new Size(80, 25);
            DarkLabel55.TabIndex = 28;
            DarkLabel55.Text = "Choice 2";
            // 
            // DarkLabel54
            // 
            DarkLabel54.AutoSize = true;
            DarkLabel54.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel54.Location = new Point(10, 227);
            DarkLabel54.Margin = new Padding(5, 0, 5, 0);
            DarkLabel54.Name = "DarkLabel54";
            DarkLabel54.Size = new Size(80, 25);
            DarkLabel54.TabIndex = 27;
            DarkLabel54.Text = "Choice 1";
            // 
            // DarkLabel52
            // 
            DarkLabel52.AutoSize = true;
            DarkLabel52.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel52.Location = new Point(12, 37);
            DarkLabel52.Margin = new Padding(5, 0, 5, 0);
            DarkLabel52.Name = "DarkLabel52";
            DarkLabel52.Size = new Size(72, 25);
            DarkLabel52.TabIndex = 26;
            DarkLabel52.Text = "Prompt";
            // 
            // txtChoicePrompt
            // 
            txtChoicePrompt.BackColor = Color.FromArgb(69, 73, 74);
            txtChoicePrompt.BorderStyle = BorderStyle.FixedSingle;
            txtChoicePrompt.ForeColor = Color.FromArgb(220, 220, 220);
            txtChoicePrompt.Location = new Point(15, 73);
            txtChoicePrompt.Margin = new Padding(5);
            txtChoicePrompt.Multiline = true;
            txtChoicePrompt.Name = "txtChoicePrompt";
            txtChoicePrompt.Size = new Size(379, 147);
            txtChoicePrompt.TabIndex = 21;
            // 
            // btnShowChoicesOk
            // 
            btnShowChoicesOk.Location = new Point(140, 587);
            btnShowChoicesOk.Margin = new Padding(5);
            btnShowChoicesOk.Name = "btnShowChoicesOk";
            btnShowChoicesOk.Padding = new Padding(8, 10, 8, 10);
            btnShowChoicesOk.Size = new Size(125, 45);
            btnShowChoicesOk.TabIndex = 25;
            btnShowChoicesOk.Text = "Ok";
            // 
            // btnShowChoicesCancel
            // 
            btnShowChoicesCancel.Location = new Point(275, 587);
            btnShowChoicesCancel.Margin = new Padding(5);
            btnShowChoicesCancel.Name = "btnShowChoicesCancel";
            btnShowChoicesCancel.Padding = new Padding(8, 10, 8, 10);
            btnShowChoicesCancel.Size = new Size(125, 45);
            btnShowChoicesCancel.TabIndex = 24;
            btnShowChoicesCancel.Text = "Cancel";
            // 
            // fraChangeLevel
            // 
            fraChangeLevel.BackColor = Color.FromArgb(45, 45, 48);
            fraChangeLevel.BorderColor = Color.FromArgb(90, 90, 90);
            fraChangeLevel.Controls.Add(btnChangeLevelOk);
            fraChangeLevel.Controls.Add(btnChangeLevelCancel);
            fraChangeLevel.Controls.Add(DarkLabel65);
            fraChangeLevel.Controls.Add(nudChangeLevel);
            fraChangeLevel.ForeColor = Color.Gainsboro;
            fraChangeLevel.Location = new Point(668, 563);
            fraChangeLevel.Margin = new Padding(5);
            fraChangeLevel.Name = "fraChangeLevel";
            fraChangeLevel.Padding = new Padding(5);
            fraChangeLevel.Size = new Size(413, 138);
            fraChangeLevel.TabIndex = 38;
            fraChangeLevel.TabStop = false;
            fraChangeLevel.Text = "Change Level";
            fraChangeLevel.Visible = false;
            // 
            // btnChangeLevelOk
            // 
            btnChangeLevelOk.Location = new Point(77, 87);
            btnChangeLevelOk.Margin = new Padding(5);
            btnChangeLevelOk.Name = "btnChangeLevelOk";
            btnChangeLevelOk.Padding = new Padding(8, 10, 8, 10);
            btnChangeLevelOk.Size = new Size(125, 45);
            btnChangeLevelOk.TabIndex = 27;
            btnChangeLevelOk.Text = "Ok";
            // 
            // btnChangeLevelCancel
            // 
            btnChangeLevelCancel.Location = new Point(212, 87);
            btnChangeLevelCancel.Margin = new Padding(5);
            btnChangeLevelCancel.Name = "btnChangeLevelCancel";
            btnChangeLevelCancel.Padding = new Padding(8, 10, 8, 10);
            btnChangeLevelCancel.Size = new Size(125, 45);
            btnChangeLevelCancel.TabIndex = 26;
            btnChangeLevelCancel.Text = "Cancel";
            // 
            // DarkLabel65
            // 
            DarkLabel65.AutoSize = true;
            DarkLabel65.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel65.Location = new Point(12, 40);
            DarkLabel65.Margin = new Padding(5, 0, 5, 0);
            DarkLabel65.Name = "DarkLabel65";
            DarkLabel65.Size = new Size(55, 25);
            DarkLabel65.TabIndex = 24;
            DarkLabel65.Text = "Level:";
            // 
            // nudChangeLevel
            // 
            nudChangeLevel.Location = new Point(100, 37);
            nudChangeLevel.Margin = new Padding(5);
            nudChangeLevel.Name = "nudChangeLevel";
            nudChangeLevel.Size = new Size(200, 31);
            nudChangeLevel.TabIndex = 23;
            // 
            // fraPlayerVariable
            // 
            fraPlayerVariable.BackColor = Color.FromArgb(45, 45, 48);
            fraPlayerVariable.BorderColor = Color.FromArgb(90, 90, 90);
            fraPlayerVariable.Controls.Add(nudVariableData2);
            fraPlayerVariable.Controls.Add(optVariableAction2);
            fraPlayerVariable.Controls.Add(btnPlayerVarOk);
            fraPlayerVariable.Controls.Add(btnPlayerVarCancel);
            fraPlayerVariable.Controls.Add(DarkLabel51);
            fraPlayerVariable.Controls.Add(DarkLabel50);
            fraPlayerVariable.Controls.Add(nudVariableData4);
            fraPlayerVariable.Controls.Add(nudVariableData3);
            fraPlayerVariable.Controls.Add(optVariableAction3);
            fraPlayerVariable.Controls.Add(optVariableAction1);
            fraPlayerVariable.Controls.Add(nudVariableData1);
            fraPlayerVariable.Controls.Add(nudVariableData0);
            fraPlayerVariable.Controls.Add(optVariableAction0);
            fraPlayerVariable.Controls.Add(cmbVariable);
            fraPlayerVariable.Controls.Add(DarkLabel49);
            fraPlayerVariable.ForeColor = Color.Gainsboro;
            fraPlayerVariable.Location = new Point(668, 541);
            fraPlayerVariable.Margin = new Padding(5);
            fraPlayerVariable.Name = "fraPlayerVariable";
            fraPlayerVariable.Padding = new Padding(5);
            fraPlayerVariable.Size = new Size(410, 297);
            fraPlayerVariable.TabIndex = 31;
            fraPlayerVariable.TabStop = false;
            fraPlayerVariable.Text = "Player Variable";
            fraPlayerVariable.Visible = false;
            // 
            // nudVariableData2
            // 
            nudVariableData2.Location = new Point(200, 138);
            nudVariableData2.Margin = new Padding(5);
            nudVariableData2.Name = "nudVariableData2";
            nudVariableData2.Size = new Size(200, 31);
            nudVariableData2.TabIndex = 29;
            // 
            // optVariableAction2
            // 
            optVariableAction2.AutoSize = true;
            optVariableAction2.Location = new Point(10, 138);
            optVariableAction2.Margin = new Padding(5);
            optVariableAction2.Name = "optVariableAction2";
            optVariableAction2.Size = new Size(103, 29);
            optVariableAction2.TabIndex = 28;
            optVariableAction2.TabStop = true;
            optVariableAction2.Text = "Subtract";
            // 
            // btnPlayerVarOk
            // 
            btnPlayerVarOk.Location = new Point(140, 238);
            btnPlayerVarOk.Margin = new Padding(5);
            btnPlayerVarOk.Name = "btnPlayerVarOk";
            btnPlayerVarOk.Padding = new Padding(8, 10, 8, 10);
            btnPlayerVarOk.Size = new Size(125, 45);
            btnPlayerVarOk.TabIndex = 27;
            btnPlayerVarOk.Text = "Ok";
            // 
            // btnPlayerVarCancel
            // 
            btnPlayerVarCancel.Location = new Point(275, 238);
            btnPlayerVarCancel.Margin = new Padding(5);
            btnPlayerVarCancel.Name = "btnPlayerVarCancel";
            btnPlayerVarCancel.Padding = new Padding(8, 10, 8, 10);
            btnPlayerVarCancel.Size = new Size(125, 45);
            btnPlayerVarCancel.TabIndex = 26;
            btnPlayerVarCancel.Text = "Cancel";
            // 
            // DarkLabel51
            // 
            DarkLabel51.AutoSize = true;
            DarkLabel51.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel51.Location = new Point(125, 191);
            DarkLabel51.Margin = new Padding(5, 0, 5, 0);
            DarkLabel51.Name = "DarkLabel51";
            DarkLabel51.Size = new Size(48, 25);
            DarkLabel51.TabIndex = 16;
            DarkLabel51.Text = "Low:";
            // 
            // DarkLabel50
            // 
            DarkLabel50.AutoSize = true;
            DarkLabel50.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel50.Location = new Point(263, 191);
            DarkLabel50.Margin = new Padding(5, 0, 5, 0);
            DarkLabel50.Name = "DarkLabel50";
            DarkLabel50.Size = new Size(54, 25);
            DarkLabel50.TabIndex = 15;
            DarkLabel50.Text = "High:";
            // 
            // nudVariableData4
            // 
            nudVariableData4.Location = new Point(327, 188);
            nudVariableData4.Margin = new Padding(5);
            nudVariableData4.Name = "nudVariableData4";
            nudVariableData4.Size = new Size(73, 31);
            nudVariableData4.TabIndex = 14;
            // 
            // nudVariableData3
            // 
            nudVariableData3.Location = new Point(185, 188);
            nudVariableData3.Margin = new Padding(5);
            nudVariableData3.Name = "nudVariableData3";
            nudVariableData3.Size = new Size(73, 31);
            nudVariableData3.TabIndex = 13;
            // 
            // optVariableAction3
            // 
            optVariableAction3.AutoSize = true;
            optVariableAction3.Location = new Point(10, 188);
            optVariableAction3.Margin = new Padding(5);
            optVariableAction3.Name = "optVariableAction3";
            optVariableAction3.Size = new Size(105, 29);
            optVariableAction3.TabIndex = 12;
            optVariableAction3.TabStop = true;
            optVariableAction3.Text = "Random";
            // 
            // optVariableAction1
            // 
            optVariableAction1.AutoSize = true;
            optVariableAction1.Location = new Point(243, 88);
            optVariableAction1.Margin = new Padding(5);
            optVariableAction1.Name = "optVariableAction1";
            optVariableAction1.Size = new Size(71, 29);
            optVariableAction1.TabIndex = 11;
            optVariableAction1.TabStop = true;
            optVariableAction1.Text = "Add";
            // 
            // nudVariableData1
            // 
            nudVariableData1.Location = new Point(327, 88);
            nudVariableData1.Margin = new Padding(5);
            nudVariableData1.Name = "nudVariableData1";
            nudVariableData1.Size = new Size(73, 31);
            nudVariableData1.TabIndex = 10;
            // 
            // nudVariableData0
            // 
            nudVariableData0.Location = new Point(103, 88);
            nudVariableData0.Margin = new Padding(5);
            nudVariableData0.Name = "nudVariableData0";
            nudVariableData0.Size = new Size(73, 31);
            nudVariableData0.TabIndex = 9;
            // 
            // optVariableAction0
            // 
            optVariableAction0.AutoSize = true;
            optVariableAction0.Location = new Point(10, 88);
            optVariableAction0.Margin = new Padding(5);
            optVariableAction0.Name = "optVariableAction0";
            optVariableAction0.Size = new Size(62, 29);
            optVariableAction0.TabIndex = 2;
            optVariableAction0.TabStop = true;
            optVariableAction0.Text = "Set";
            // 
            // cmbVariable
            // 
            cmbVariable.DrawMode = DrawMode.OwnerDrawFixed;
            cmbVariable.FormattingEnabled = true;
            cmbVariable.Location = new Point(100, 37);
            cmbVariable.Margin = new Padding(5);
            cmbVariable.Name = "cmbVariable";
            cmbVariable.Size = new Size(296, 32);
            cmbVariable.TabIndex = 1;
            // 
            // DarkLabel49
            // 
            DarkLabel49.AutoSize = true;
            DarkLabel49.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel49.Location = new Point(10, 41);
            DarkLabel49.Margin = new Padding(5, 0, 5, 0);
            DarkLabel49.Name = "DarkLabel49";
            DarkLabel49.Size = new Size(78, 25);
            DarkLabel49.TabIndex = 0;
            DarkLabel49.Text = "Variable:";
            // 
            // fraPlayAnimation
            // 
            fraPlayAnimation.BackColor = Color.FromArgb(45, 45, 48);
            fraPlayAnimation.BorderColor = Color.FromArgb(90, 90, 90);
            fraPlayAnimation.Controls.Add(btnPlayAnimationOk);
            fraPlayAnimation.Controls.Add(btnPlayAnimationCancel);
            fraPlayAnimation.Controls.Add(lblPlayAnimY);
            fraPlayAnimation.Controls.Add(lblPlayAnimX);
            fraPlayAnimation.Controls.Add(cmbPlayAnimEvent);
            fraPlayAnimation.Controls.Add(DarkLabel62);
            fraPlayAnimation.Controls.Add(cmbAnimTargetType);
            fraPlayAnimation.Controls.Add(nudPlayAnimTileY);
            fraPlayAnimation.Controls.Add(nudPlayAnimTileX);
            fraPlayAnimation.Controls.Add(DarkLabel61);
            fraPlayAnimation.Controls.Add(cmbPlayAnim);
            fraPlayAnimation.ForeColor = Color.Gainsboro;
            fraPlayAnimation.Location = new Point(668, 495);
            fraPlayAnimation.Margin = new Padding(5);
            fraPlayAnimation.Name = "fraPlayAnimation";
            fraPlayAnimation.Padding = new Padding(5);
            fraPlayAnimation.Size = new Size(413, 312);
            fraPlayAnimation.TabIndex = 36;
            fraPlayAnimation.TabStop = false;
            fraPlayAnimation.Text = "Play Animation";
            fraPlayAnimation.Visible = false;
            // 
            // btnPlayAnimationOk
            // 
            btnPlayAnimationOk.Location = new Point(143, 253);
            btnPlayAnimationOk.Margin = new Padding(5);
            btnPlayAnimationOk.Name = "btnPlayAnimationOk";
            btnPlayAnimationOk.Padding = new Padding(8, 10, 8, 10);
            btnPlayAnimationOk.Size = new Size(125, 45);
            btnPlayAnimationOk.TabIndex = 36;
            btnPlayAnimationOk.Text = "Ok";
            // 
            // btnPlayAnimationCancel
            // 
            btnPlayAnimationCancel.Location = new Point(278, 253);
            btnPlayAnimationCancel.Margin = new Padding(5);
            btnPlayAnimationCancel.Name = "btnPlayAnimationCancel";
            btnPlayAnimationCancel.Padding = new Padding(8, 10, 8, 10);
            btnPlayAnimationCancel.Size = new Size(125, 45);
            btnPlayAnimationCancel.TabIndex = 35;
            btnPlayAnimationCancel.Text = "Cancel";
            // 
            // lblPlayAnimY
            // 
            lblPlayAnimY.AutoSize = true;
            lblPlayAnimY.ForeColor = Color.FromArgb(220, 220, 220);
            lblPlayAnimY.Location = new Point(218, 203);
            lblPlayAnimY.Margin = new Padding(5, 0, 5, 0);
            lblPlayAnimY.Name = "lblPlayAnimY";
            lblPlayAnimY.Size = new Size(98, 25);
            lblPlayAnimY.TabIndex = 34;
            lblPlayAnimY.Text = "Map Tile Y:";
            // 
            // lblPlayAnimX
            // 
            lblPlayAnimX.AutoSize = true;
            lblPlayAnimX.ForeColor = Color.FromArgb(220, 220, 220);
            lblPlayAnimX.Location = new Point(10, 203);
            lblPlayAnimX.Margin = new Padding(5, 0, 5, 0);
            lblPlayAnimX.Name = "lblPlayAnimX";
            lblPlayAnimX.Size = new Size(99, 25);
            lblPlayAnimX.TabIndex = 33;
            lblPlayAnimX.Text = "Map Tile X:";
            // 
            // cmbPlayAnimEvent
            // 
            cmbPlayAnimEvent.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayAnimEvent.FormattingEnabled = true;
            cmbPlayAnimEvent.Location = new Point(138, 140);
            cmbPlayAnimEvent.Margin = new Padding(5);
            cmbPlayAnimEvent.Name = "cmbPlayAnimEvent";
            cmbPlayAnimEvent.Size = new Size(262, 32);
            cmbPlayAnimEvent.TabIndex = 32;
            // 
            // DarkLabel62
            // 
            DarkLabel62.AutoSize = true;
            DarkLabel62.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel62.Location = new Point(7, 95);
            DarkLabel62.Margin = new Padding(5, 0, 5, 0);
            DarkLabel62.Name = "DarkLabel62";
            DarkLabel62.Size = new Size(102, 25);
            DarkLabel62.TabIndex = 31;
            DarkLabel62.Text = "Target Type";
            // 
            // cmbAnimTargetType
            // 
            cmbAnimTargetType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnimTargetType.FormattingEnabled = true;
            cmbAnimTargetType.Items.AddRange(new object[] { "Player", "Event", "Tile" });
            cmbAnimTargetType.Location = new Point(138, 88);
            cmbAnimTargetType.Margin = new Padding(5);
            cmbAnimTargetType.Name = "cmbAnimTargetType";
            cmbAnimTargetType.Size = new Size(262, 32);
            cmbAnimTargetType.TabIndex = 30;
            // 
            // nudPlayAnimTileY
            // 
            nudPlayAnimTileY.Location = new Point(330, 200);
            nudPlayAnimTileY.Margin = new Padding(5);
            nudPlayAnimTileY.Name = "nudPlayAnimTileY";
            nudPlayAnimTileY.Size = new Size(73, 31);
            nudPlayAnimTileY.TabIndex = 29;
            // 
            // nudPlayAnimTileX
            // 
            nudPlayAnimTileX.Location = new Point(122, 200);
            nudPlayAnimTileX.Margin = new Padding(5);
            nudPlayAnimTileX.Name = "nudPlayAnimTileX";
            nudPlayAnimTileX.Size = new Size(73, 31);
            nudPlayAnimTileX.TabIndex = 28;
            // 
            // DarkLabel61
            // 
            DarkLabel61.AutoSize = true;
            DarkLabel61.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel61.Location = new Point(10, 41);
            DarkLabel61.Margin = new Padding(5, 0, 5, 0);
            DarkLabel61.Name = "DarkLabel61";
            DarkLabel61.Size = new Size(98, 25);
            DarkLabel61.TabIndex = 1;
            DarkLabel61.Text = "Animation:";
            // 
            // cmbPlayAnim
            // 
            cmbPlayAnim.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayAnim.FormattingEnabled = true;
            cmbPlayAnim.Location = new Point(103, 37);
            cmbPlayAnim.Margin = new Padding(5);
            cmbPlayAnim.Name = "cmbPlayAnim";
            cmbPlayAnim.Size = new Size(297, 32);
            cmbPlayAnim.TabIndex = 0;
            // 
            // fraChangeSprite
            // 
            fraChangeSprite.BackColor = Color.FromArgb(45, 45, 48);
            fraChangeSprite.BorderColor = Color.FromArgb(90, 90, 90);
            fraChangeSprite.Controls.Add(btnChangeSpriteOk);
            fraChangeSprite.Controls.Add(btnChangeSpriteCancel);
            fraChangeSprite.Controls.Add(DarkLabel48);
            fraChangeSprite.Controls.Add(nudChangeSprite);
            fraChangeSprite.Controls.Add(picChangeSprite);
            fraChangeSprite.ForeColor = Color.Gainsboro;
            fraChangeSprite.Location = new Point(668, 538);
            fraChangeSprite.Margin = new Padding(5);
            fraChangeSprite.Name = "fraChangeSprite";
            fraChangeSprite.Padding = new Padding(5);
            fraChangeSprite.Size = new Size(410, 225);
            fraChangeSprite.TabIndex = 30;
            fraChangeSprite.TabStop = false;
            fraChangeSprite.Text = "Change Sprite";
            fraChangeSprite.Visible = false;
            // 
            // btnChangeSpriteOk
            // 
            btnChangeSpriteOk.Location = new Point(140, 172);
            btnChangeSpriteOk.Margin = new Padding(5);
            btnChangeSpriteOk.Name = "btnChangeSpriteOk";
            btnChangeSpriteOk.Padding = new Padding(8, 10, 8, 10);
            btnChangeSpriteOk.Size = new Size(125, 45);
            btnChangeSpriteOk.TabIndex = 30;
            btnChangeSpriteOk.Text = "Ok";
            // 
            // btnChangeSpriteCancel
            // 
            btnChangeSpriteCancel.Location = new Point(275, 172);
            btnChangeSpriteCancel.Margin = new Padding(5);
            btnChangeSpriteCancel.Name = "btnChangeSpriteCancel";
            btnChangeSpriteCancel.Padding = new Padding(8, 10, 8, 10);
            btnChangeSpriteCancel.Size = new Size(125, 45);
            btnChangeSpriteCancel.TabIndex = 29;
            btnChangeSpriteCancel.Text = "Cancel";
            // 
            // DarkLabel48
            // 
            DarkLabel48.AutoSize = true;
            DarkLabel48.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel48.Location = new Point(133, 128);
            DarkLabel48.Margin = new Padding(5, 0, 5, 0);
            DarkLabel48.Name = "DarkLabel48";
            DarkLabel48.Size = new Size(58, 25);
            DarkLabel48.TabIndex = 28;
            DarkLabel48.Text = "Sprite";
            // 
            // nudChangeSprite
            // 
            nudChangeSprite.Location = new Point(200, 122);
            nudChangeSprite.Margin = new Padding(5);
            nudChangeSprite.Name = "nudChangeSprite";
            nudChangeSprite.Size = new Size(200, 31);
            nudChangeSprite.TabIndex = 27;
            // 
            // picChangeSprite
            // 
            picChangeSprite.BackColor = Color.Black;
            picChangeSprite.BackgroundImageLayout = ImageLayout.Zoom;
            picChangeSprite.Location = new Point(10, 37);
            picChangeSprite.Margin = new Padding(5);
            picChangeSprite.Name = "picChangeSprite";
            picChangeSprite.Size = new Size(117, 178);
            picChangeSprite.TabIndex = 3;
            picChangeSprite.TabStop = false;
            // 
            // fraGoToLabel
            // 
            fraGoToLabel.BackColor = Color.FromArgb(45, 45, 48);
            fraGoToLabel.BorderColor = Color.FromArgb(90, 90, 90);
            fraGoToLabel.Controls.Add(btnGoToLabelOk);
            fraGoToLabel.Controls.Add(btnGoToLabelCancel);
            fraGoToLabel.Controls.Add(txtGoToLabel);
            fraGoToLabel.Controls.Add(DarkLabel60);
            fraGoToLabel.ForeColor = Color.Gainsboro;
            fraGoToLabel.Location = new Point(668, 490);
            fraGoToLabel.Margin = new Padding(5);
            fraGoToLabel.Name = "fraGoToLabel";
            fraGoToLabel.Padding = new Padding(5);
            fraGoToLabel.Size = new Size(413, 140);
            fraGoToLabel.TabIndex = 35;
            fraGoToLabel.TabStop = false;
            fraGoToLabel.Text = "GoTo Label";
            fraGoToLabel.Visible = false;
            // 
            // btnGoToLabelOk
            // 
            btnGoToLabelOk.Location = new Point(143, 85);
            btnGoToLabelOk.Margin = new Padding(5);
            btnGoToLabelOk.Name = "btnGoToLabelOk";
            btnGoToLabelOk.Padding = new Padding(8, 10, 8, 10);
            btnGoToLabelOk.Size = new Size(125, 45);
            btnGoToLabelOk.TabIndex = 27;
            btnGoToLabelOk.Text = "Ok";
            // 
            // btnGoToLabelCancel
            // 
            btnGoToLabelCancel.Location = new Point(278, 85);
            btnGoToLabelCancel.Margin = new Padding(5);
            btnGoToLabelCancel.Name = "btnGoToLabelCancel";
            btnGoToLabelCancel.Padding = new Padding(8, 10, 8, 10);
            btnGoToLabelCancel.Size = new Size(125, 45);
            btnGoToLabelCancel.TabIndex = 26;
            btnGoToLabelCancel.Text = "Cancel";
            // 
            // txtGoToLabel
            // 
            txtGoToLabel.BackColor = Color.FromArgb(69, 73, 74);
            txtGoToLabel.BorderStyle = BorderStyle.FixedSingle;
            txtGoToLabel.ForeColor = Color.FromArgb(220, 220, 220);
            txtGoToLabel.Location = new Point(130, 35);
            txtGoToLabel.Margin = new Padding(5);
            txtGoToLabel.Name = "txtGoToLabel";
            txtGoToLabel.Size = new Size(272, 31);
            txtGoToLabel.TabIndex = 1;
            // 
            // DarkLabel60
            // 
            DarkLabel60.AutoSize = true;
            DarkLabel60.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel60.Location = new Point(5, 38);
            DarkLabel60.Margin = new Padding(5, 0, 5, 0);
            DarkLabel60.Name = "DarkLabel60";
            DarkLabel60.Size = new Size(109, 25);
            DarkLabel60.TabIndex = 0;
            DarkLabel60.Text = "Label Name:";
            // 
            // fraMapTint
            // 
            fraMapTint.BackColor = Color.FromArgb(45, 45, 48);
            fraMapTint.BorderColor = Color.FromArgb(90, 90, 90);
            fraMapTint.Controls.Add(btnMapTintOk);
            fraMapTint.Controls.Add(btnMapTintCancel);
            fraMapTint.Controls.Add(DarkLabel42);
            fraMapTint.Controls.Add(nudMapTintData3);
            fraMapTint.Controls.Add(nudMapTintData2);
            fraMapTint.Controls.Add(DarkLabel43);
            fraMapTint.Controls.Add(DarkLabel44);
            fraMapTint.Controls.Add(nudMapTintData1);
            fraMapTint.Controls.Add(nudMapTintData0);
            fraMapTint.Controls.Add(DarkLabel45);
            fraMapTint.ForeColor = Color.Gainsboro;
            fraMapTint.Location = new Point(668, 348);
            fraMapTint.Margin = new Padding(5);
            fraMapTint.Name = "fraMapTint";
            fraMapTint.Padding = new Padding(5);
            fraMapTint.Size = new Size(410, 278);
            fraMapTint.TabIndex = 28;
            fraMapTint.TabStop = false;
            fraMapTint.Text = "Map Tinting";
            fraMapTint.Visible = false;
            // 
            // btnMapTintOk
            // 
            btnMapTintOk.Location = new Point(140, 222);
            btnMapTintOk.Margin = new Padding(5);
            btnMapTintOk.Name = "btnMapTintOk";
            btnMapTintOk.Padding = new Padding(8, 10, 8, 10);
            btnMapTintOk.Size = new Size(125, 45);
            btnMapTintOk.TabIndex = 45;
            btnMapTintOk.Text = "Ok";
            // 
            // btnMapTintCancel
            // 
            btnMapTintCancel.Location = new Point(275, 222);
            btnMapTintCancel.Margin = new Padding(5);
            btnMapTintCancel.Name = "btnMapTintCancel";
            btnMapTintCancel.Padding = new Padding(8, 10, 8, 10);
            btnMapTintCancel.Size = new Size(125, 45);
            btnMapTintCancel.TabIndex = 44;
            btnMapTintCancel.Text = "Cancel";
            // 
            // DarkLabel42
            // 
            DarkLabel42.AutoSize = true;
            DarkLabel42.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel42.Location = new Point(8, 178);
            DarkLabel42.Margin = new Padding(5, 0, 5, 0);
            DarkLabel42.Name = "DarkLabel42";
            DarkLabel42.Size = new Size(77, 25);
            DarkLabel42.TabIndex = 43;
            DarkLabel42.Text = "Opacity:";
            // 
            // nudMapTintData3
            // 
            nudMapTintData3.Location = new Point(158, 172);
            nudMapTintData3.Margin = new Padding(5);
            nudMapTintData3.Name = "nudMapTintData3";
            nudMapTintData3.Size = new Size(240, 31);
            nudMapTintData3.TabIndex = 42;
            // 
            // nudMapTintData2
            // 
            nudMapTintData2.Location = new Point(158, 123);
            nudMapTintData2.Margin = new Padding(5);
            nudMapTintData2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudMapTintData2.Name = "nudMapTintData2";
            nudMapTintData2.Size = new Size(240, 31);
            nudMapTintData2.TabIndex = 41;
            // 
            // DarkLabel43
            // 
            DarkLabel43.AutoSize = true;
            DarkLabel43.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel43.Location = new Point(8, 127);
            DarkLabel43.Margin = new Padding(5, 0, 5, 0);
            DarkLabel43.Name = "DarkLabel43";
            DarkLabel43.Size = new Size(49, 25);
            DarkLabel43.TabIndex = 40;
            DarkLabel43.Text = "Blue:";
            // 
            // DarkLabel44
            // 
            DarkLabel44.AutoSize = true;
            DarkLabel44.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel44.Location = new Point(7, 84);
            DarkLabel44.Margin = new Padding(5, 0, 5, 0);
            DarkLabel44.Name = "DarkLabel44";
            DarkLabel44.Size = new Size(62, 25);
            DarkLabel44.TabIndex = 39;
            DarkLabel44.Text = "Green:";
            // 
            // nudMapTintData1
            // 
            nudMapTintData1.Location = new Point(158, 75);
            nudMapTintData1.Margin = new Padding(5);
            nudMapTintData1.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudMapTintData1.Name = "nudMapTintData1";
            nudMapTintData1.Size = new Size(240, 31);
            nudMapTintData1.TabIndex = 38;
            // 
            // nudMapTintData0
            // 
            nudMapTintData0.Location = new Point(158, 27);
            nudMapTintData0.Margin = new Padding(5);
            nudMapTintData0.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudMapTintData0.Name = "nudMapTintData0";
            nudMapTintData0.Size = new Size(240, 31);
            nudMapTintData0.TabIndex = 37;
            // 
            // DarkLabel45
            // 
            DarkLabel45.AutoSize = true;
            DarkLabel45.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel45.Location = new Point(8, 30);
            DarkLabel45.Margin = new Padding(5, 0, 5, 0);
            DarkLabel45.Name = "DarkLabel45";
            DarkLabel45.Size = new Size(46, 25);
            DarkLabel45.TabIndex = 36;
            DarkLabel45.Text = "Red:";
            // 
            // fraShowPic
            // 
            fraShowPic.BackColor = Color.FromArgb(45, 45, 48);
            fraShowPic.BorderColor = Color.FromArgb(90, 90, 90);
            fraShowPic.Controls.Add(btnShowPicOk);
            fraShowPic.Controls.Add(btnShowPicCancel);
            fraShowPic.Controls.Add(DarkLabel71);
            fraShowPic.Controls.Add(DarkLabel70);
            fraShowPic.Controls.Add(DarkLabel67);
            fraShowPic.Controls.Add(DarkLabel68);
            fraShowPic.Controls.Add(nudPicOffsetY);
            fraShowPic.Controls.Add(nudPicOffsetX);
            fraShowPic.Controls.Add(DarkLabel69);
            fraShowPic.Controls.Add(cmbPicLoc);
            fraShowPic.Controls.Add(nudShowPicture);
            fraShowPic.Controls.Add(picShowPic);
            fraShowPic.ForeColor = Color.Gainsboro;
            fraShowPic.Location = new Point(1, 0);
            fraShowPic.Margin = new Padding(5);
            fraShowPic.Name = "fraShowPic";
            fraShowPic.Padding = new Padding(5);
            fraShowPic.Size = new Size(1107, 1143);
            fraShowPic.TabIndex = 40;
            fraShowPic.TabStop = false;
            fraShowPic.Text = "Show Picture";
            fraShowPic.Visible = false;
            // 
            // btnShowPicOk
            // 
            btnShowPicOk.Location = new Point(833, 1085);
            btnShowPicOk.Margin = new Padding(5);
            btnShowPicOk.Name = "btnShowPicOk";
            btnShowPicOk.Padding = new Padding(8, 10, 8, 10);
            btnShowPicOk.Size = new Size(125, 45);
            btnShowPicOk.TabIndex = 55;
            btnShowPicOk.Text = "Ok";
            // 
            // btnShowPicCancel
            // 
            btnShowPicCancel.Location = new Point(970, 1085);
            btnShowPicCancel.Margin = new Padding(5);
            btnShowPicCancel.Name = "btnShowPicCancel";
            btnShowPicCancel.Padding = new Padding(8, 10, 8, 10);
            btnShowPicCancel.Size = new Size(125, 45);
            btnShowPicCancel.TabIndex = 54;
            btnShowPicCancel.Text = "Cancel";
            // 
            // DarkLabel71
            // 
            DarkLabel71.AutoSize = true;
            DarkLabel71.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel71.Location = new Point(410, 45);
            DarkLabel71.Margin = new Padding(5, 0, 5, 0);
            DarkLabel71.Name = "DarkLabel71";
            DarkLabel71.Size = new Size(181, 25);
            DarkLabel71.TabIndex = 53;
            DarkLabel71.Text = "Offset from Location:";
            // 
            // DarkLabel70
            // 
            DarkLabel70.AutoSize = true;
            DarkLabel70.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel70.Location = new Point(185, 109);
            DarkLabel70.Margin = new Padding(5, 0, 5, 0);
            DarkLabel70.Name = "DarkLabel70";
            DarkLabel70.Size = new Size(79, 25);
            DarkLabel70.TabIndex = 52;
            DarkLabel70.Text = "Location";
            // 
            // DarkLabel67
            // 
            DarkLabel67.AutoSize = true;
            DarkLabel67.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel67.Location = new Point(622, 90);
            DarkLabel67.Margin = new Padding(5, 0, 5, 0);
            DarkLabel67.Name = "DarkLabel67";
            DarkLabel67.Size = new Size(26, 25);
            DarkLabel67.TabIndex = 51;
            DarkLabel67.Text = "Y:";
            // 
            // DarkLabel68
            // 
            DarkLabel68.AutoSize = true;
            DarkLabel68.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel68.Location = new Point(410, 93);
            DarkLabel68.Margin = new Padding(5, 0, 5, 0);
            DarkLabel68.Name = "DarkLabel68";
            DarkLabel68.Size = new Size(27, 25);
            DarkLabel68.TabIndex = 50;
            DarkLabel68.Text = "X:";
            // 
            // nudPicOffsetY
            // 
            nudPicOffsetY.Location = new Point(695, 87);
            nudPicOffsetY.Margin = new Padding(5);
            nudPicOffsetY.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudPicOffsetY.Name = "nudPicOffsetY";
            nudPicOffsetY.Size = new Size(95, 31);
            nudPicOffsetY.TabIndex = 49;
            // 
            // nudPicOffsetX
            // 
            nudPicOffsetX.Location = new Point(480, 87);
            nudPicOffsetX.Margin = new Padding(5);
            nudPicOffsetX.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudPicOffsetX.Name = "nudPicOffsetX";
            nudPicOffsetX.Size = new Size(95, 31);
            nudPicOffsetX.TabIndex = 48;
            // 
            // DarkLabel69
            // 
            DarkLabel69.AutoSize = true;
            DarkLabel69.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel69.Location = new Point(185, 43);
            DarkLabel69.Margin = new Padding(5, 0, 5, 0);
            DarkLabel69.Name = "DarkLabel69";
            DarkLabel69.Size = new Size(69, 25);
            DarkLabel69.TabIndex = 47;
            DarkLabel69.Text = "Picture:";
            // 
            // cmbPicLoc
            // 
            cmbPicLoc.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPicLoc.FormattingEnabled = true;
            cmbPicLoc.Items.AddRange(new object[] { "Top Left of Screen", "Center Screen", "Centered on Event", "Centered on Player" });
            cmbPicLoc.Location = new Point(190, 143);
            cmbPicLoc.Margin = new Padding(5);
            cmbPicLoc.Name = "cmbPicLoc";
            cmbPicLoc.Size = new Size(204, 32);
            cmbPicLoc.TabIndex = 46;
            // 
            // nudShowPicture
            // 
            nudShowPicture.Location = new Point(265, 40);
            nudShowPicture.Margin = new Padding(5);
            nudShowPicture.Name = "nudShowPicture";
            nudShowPicture.Size = new Size(125, 31);
            nudShowPicture.TabIndex = 45;
            // 
            // picShowPic
            // 
            picShowPic.BackColor = Color.Black;
            picShowPic.BackgroundImageLayout = ImageLayout.Stretch;
            picShowPic.Location = new Point(13, 35);
            picShowPic.Margin = new Padding(5);
            picShowPic.Name = "picShowPic";
            picShowPic.Size = new Size(167, 178);
            picShowPic.TabIndex = 42;
            picShowPic.TabStop = false;
            // 
            // fraConditionalBranch
            // 
            fraConditionalBranch.BackColor = Color.FromArgb(45, 45, 48);
            fraConditionalBranch.BorderColor = Color.FromArgb(90, 90, 90);
            fraConditionalBranch.Controls.Add(cmbCondition_Time);
            fraConditionalBranch.Controls.Add(optCondition9);
            fraConditionalBranch.Controls.Add(btnConditionalBranchOk);
            fraConditionalBranch.Controls.Add(btnConditionalBranchCancel);
            fraConditionalBranch.Controls.Add(cmbCondition_Gender);
            fraConditionalBranch.Controls.Add(optCondition8);
            fraConditionalBranch.Controls.Add(cmbCondition_SelfSwitchCondition);
            fraConditionalBranch.Controls.Add(DarkLabel17);
            fraConditionalBranch.Controls.Add(cmbCondition_SelfSwitch);
            fraConditionalBranch.Controls.Add(optCondition6);
            fraConditionalBranch.Controls.Add(nudCondition_LevelAmount);
            fraConditionalBranch.Controls.Add(optCondition5);
            fraConditionalBranch.Controls.Add(cmbCondition_LevelCompare);
            fraConditionalBranch.Controls.Add(cmbCondition_LearntSkill);
            fraConditionalBranch.Controls.Add(optCondition4);
            fraConditionalBranch.Controls.Add(cmbCondition_JobIs);
            fraConditionalBranch.Controls.Add(optCondition3);
            fraConditionalBranch.Controls.Add(nudCondition_HasItem);
            fraConditionalBranch.Controls.Add(DarkLabel16);
            fraConditionalBranch.Controls.Add(cmbCondition_HasItem);
            fraConditionalBranch.Controls.Add(optCondition2);
            fraConditionalBranch.Controls.Add(optCondition1);
            fraConditionalBranch.Controls.Add(DarkLabel15);
            fraConditionalBranch.Controls.Add(cmbCondtion_PlayerSwitchCondition);
            fraConditionalBranch.Controls.Add(cmbCondition_PlayerSwitch);
            fraConditionalBranch.Controls.Add(nudCondition_PlayerVarCondition);
            fraConditionalBranch.Controls.Add(cmbCondition_PlayerVarCompare);
            fraConditionalBranch.Controls.Add(DarkLabel14);
            fraConditionalBranch.Controls.Add(cmbCondition_PlayerVarIndex);
            fraConditionalBranch.Controls.Add(optCondition0);
            fraConditionalBranch.ForeColor = Color.Gainsboro;
            fraConditionalBranch.Location = new Point(10, 13);
            fraConditionalBranch.Margin = new Padding(5);
            fraConditionalBranch.Name = "fraConditionalBranch";
            fraConditionalBranch.Padding = new Padding(5);
            fraConditionalBranch.Size = new Size(648, 860);
            fraConditionalBranch.TabIndex = 0;
            fraConditionalBranch.TabStop = false;
            fraConditionalBranch.Text = "Conditional Branch";
            fraConditionalBranch.Visible = false;
            // 
            // cmbCondition_Time
            // 
            cmbCondition_Time.DrawMode = DrawMode.OwnerDrawVariable;
            cmbCondition_Time.FormattingEnabled = true;
            cmbCondition_Time.Items.AddRange(new object[] { "Day", "Night", "Dawn", "Dusk" });
            cmbCondition_Time.Location = new Point(398, 445);
            cmbCondition_Time.Margin = new Padding(5);
            cmbCondition_Time.Name = "cmbCondition_Time";
            cmbCondition_Time.Size = new Size(237, 32);
            cmbCondition_Time.TabIndex = 33;
            // 
            // optCondition9
            // 
            optCondition9.AutoSize = true;
            optCondition9.Location = new Point(10, 447);
            optCondition9.Margin = new Padding(5);
            optCondition9.Name = "optCondition9";
            optCondition9.Size = new Size(154, 29);
            optCondition9.TabIndex = 32;
            optCondition9.TabStop = true;
            optCondition9.Text = "Time of Day is:";
            // 
            // btnConditionalBranchOk
            // 
            btnConditionalBranchOk.Location = new Point(377, 800);
            btnConditionalBranchOk.Margin = new Padding(5);
            btnConditionalBranchOk.Name = "btnConditionalBranchOk";
            btnConditionalBranchOk.Padding = new Padding(8, 10, 8, 10);
            btnConditionalBranchOk.Size = new Size(125, 45);
            btnConditionalBranchOk.TabIndex = 31;
            btnConditionalBranchOk.Text = "Ok";
            // 
            // btnConditionalBranchCancel
            // 
            btnConditionalBranchCancel.Location = new Point(512, 800);
            btnConditionalBranchCancel.Margin = new Padding(5);
            btnConditionalBranchCancel.Name = "btnConditionalBranchCancel";
            btnConditionalBranchCancel.Padding = new Padding(8, 10, 8, 10);
            btnConditionalBranchCancel.Size = new Size(125, 45);
            btnConditionalBranchCancel.TabIndex = 30;
            btnConditionalBranchCancel.Text = "Cancel";
            // 
            // cmbCondition_Gender
            // 
            cmbCondition_Gender.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_Gender.FormattingEnabled = true;
            cmbCondition_Gender.Items.AddRange(new object[] { "Male", "Female" });
            cmbCondition_Gender.Location = new Point(398, 393);
            cmbCondition_Gender.Margin = new Padding(5);
            cmbCondition_Gender.Name = "cmbCondition_Gender";
            cmbCondition_Gender.Size = new Size(237, 32);
            cmbCondition_Gender.TabIndex = 29;
            // 
            // optCondition8
            // 
            optCondition8.AutoSize = true;
            optCondition8.Location = new Point(10, 395);
            optCondition8.Margin = new Padding(5);
            optCondition8.Name = "optCondition8";
            optCondition8.Size = new Size(163, 29);
            optCondition8.TabIndex = 28;
            optCondition8.TabStop = true;
            optCondition8.Text = "Player Gender is";
            // 
            // cmbCondition_SelfSwitchCondition
            // 
            cmbCondition_SelfSwitchCondition.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_SelfSwitchCondition.FormattingEnabled = true;
            cmbCondition_SelfSwitchCondition.Items.AddRange(new object[] { "False", "True" });
            cmbCondition_SelfSwitchCondition.Location = new Point(437, 352);
            cmbCondition_SelfSwitchCondition.Margin = new Padding(5);
            cmbCondition_SelfSwitchCondition.Name = "cmbCondition_SelfSwitchCondition";
            cmbCondition_SelfSwitchCondition.Size = new Size(199, 32);
            cmbCondition_SelfSwitchCondition.TabIndex = 23;
            // 
            // DarkLabel17
            // 
            DarkLabel17.AutoSize = true;
            DarkLabel17.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel17.Location = new Point(390, 359);
            DarkLabel17.Margin = new Padding(5, 0, 5, 0);
            DarkLabel17.Name = "DarkLabel17";
            DarkLabel17.Size = new Size(24, 25);
            DarkLabel17.TabIndex = 22;
            DarkLabel17.Text = "is";
            // 
            // cmbCondition_SelfSwitch
            // 
            cmbCondition_SelfSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_SelfSwitch.FormattingEnabled = true;
            cmbCondition_SelfSwitch.Location = new Point(178, 352);
            cmbCondition_SelfSwitch.Margin = new Padding(5);
            cmbCondition_SelfSwitch.Name = "cmbCondition_SelfSwitch";
            cmbCondition_SelfSwitch.Size = new Size(199, 32);
            cmbCondition_SelfSwitch.TabIndex = 21;
            // 
            // optCondition6
            // 
            optCondition6.AutoSize = true;
            optCondition6.Location = new Point(10, 353);
            optCondition6.Margin = new Padding(5);
            optCondition6.Name = "optCondition6";
            optCondition6.Size = new Size(122, 29);
            optCondition6.TabIndex = 20;
            optCondition6.TabStop = true;
            optCondition6.Text = "Self Switch";
            // 
            // nudCondition_LevelAmount
            // 
            nudCondition_LevelAmount.Location = new Point(448, 302);
            nudCondition_LevelAmount.Margin = new Padding(5);
            nudCondition_LevelAmount.Name = "nudCondition_LevelAmount";
            nudCondition_LevelAmount.Size = new Size(188, 31);
            nudCondition_LevelAmount.TabIndex = 19;
            // 
            // optCondition5
            // 
            optCondition5.AutoSize = true;
            optCondition5.Location = new Point(10, 302);
            optCondition5.Margin = new Padding(5);
            optCondition5.Name = "optCondition5";
            optCondition5.Size = new Size(93, 29);
            optCondition5.TabIndex = 18;
            optCondition5.TabStop = true;
            optCondition5.Text = "Level is";
            // 
            // cmbCondition_LevelCompare
            // 
            cmbCondition_LevelCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_LevelCompare.FormattingEnabled = true;
            cmbCondition_LevelCompare.Items.AddRange(new object[] { "Equal To", "Great Than Or Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal" });
            cmbCondition_LevelCompare.Location = new Point(178, 300);
            cmbCondition_LevelCompare.Margin = new Padding(5);
            cmbCondition_LevelCompare.Name = "cmbCondition_LevelCompare";
            cmbCondition_LevelCompare.Size = new Size(257, 32);
            cmbCondition_LevelCompare.TabIndex = 17;
            // 
            // cmbCondition_LearntSkill
            // 
            cmbCondition_LearntSkill.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_LearntSkill.FormattingEnabled = true;
            cmbCondition_LearntSkill.Location = new Point(178, 248);
            cmbCondition_LearntSkill.Margin = new Padding(5);
            cmbCondition_LearntSkill.Name = "cmbCondition_LearntSkill";
            cmbCondition_LearntSkill.Size = new Size(457, 32);
            cmbCondition_LearntSkill.TabIndex = 16;
            // 
            // optCondition4
            // 
            optCondition4.AutoSize = true;
            optCondition4.Location = new Point(10, 250);
            optCondition4.Margin = new Padding(5);
            optCondition4.Name = "optCondition4";
            optCondition4.Size = new Size(125, 29);
            optCondition4.TabIndex = 15;
            optCondition4.TabStop = true;
            optCondition4.Text = "Knows Skill";
            // 
            // cmbCondition_JobIs
            // 
            cmbCondition_JobIs.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_JobIs.FormattingEnabled = true;
            cmbCondition_JobIs.Location = new Point(178, 197);
            cmbCondition_JobIs.Margin = new Padding(5);
            cmbCondition_JobIs.Name = "cmbCondition_JobIs";
            cmbCondition_JobIs.Size = new Size(457, 32);
            cmbCondition_JobIs.TabIndex = 14;
            // 
            // optCondition3
            // 
            optCondition3.AutoSize = true;
            optCondition3.Location = new Point(10, 198);
            optCondition3.Margin = new Padding(5);
            optCondition3.Name = "optCondition3";
            optCondition3.Size = new Size(83, 29);
            optCondition3.TabIndex = 13;
            optCondition3.TabStop = true;
            optCondition3.Text = "Job Is";
            // 
            // nudCondition_HasItem
            // 
            nudCondition_HasItem.Location = new Point(437, 147);
            nudCondition_HasItem.Margin = new Padding(5);
            nudCondition_HasItem.Name = "nudCondition_HasItem";
            nudCondition_HasItem.Size = new Size(200, 31);
            nudCondition_HasItem.TabIndex = 12;
            // 
            // DarkLabel16
            // 
            DarkLabel16.AutoSize = true;
            DarkLabel16.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel16.Location = new Point(390, 150);
            DarkLabel16.Margin = new Padding(5, 0, 5, 0);
            DarkLabel16.Name = "DarkLabel16";
            DarkLabel16.Size = new Size(23, 25);
            DarkLabel16.TabIndex = 11;
            DarkLabel16.Text = "X";
            // 
            // cmbCondition_HasItem
            // 
            cmbCondition_HasItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_HasItem.FormattingEnabled = true;
            cmbCondition_HasItem.Location = new Point(178, 145);
            cmbCondition_HasItem.Margin = new Padding(5);
            cmbCondition_HasItem.Name = "cmbCondition_HasItem";
            cmbCondition_HasItem.Size = new Size(199, 32);
            cmbCondition_HasItem.TabIndex = 10;
            // 
            // optCondition2
            // 
            optCondition2.AutoSize = true;
            optCondition2.Location = new Point(10, 147);
            optCondition2.Margin = new Padding(5);
            optCondition2.Name = "optCondition2";
            optCondition2.Size = new Size(108, 29);
            optCondition2.TabIndex = 9;
            optCondition2.TabStop = true;
            optCondition2.Text = "Has Item";
            // 
            // optCondition1
            // 
            optCondition1.AutoSize = true;
            optCondition1.Location = new Point(10, 95);
            optCondition1.Margin = new Padding(5);
            optCondition1.Name = "optCondition1";
            optCondition1.Size = new Size(140, 29);
            optCondition1.TabIndex = 8;
            optCondition1.TabStop = true;
            optCondition1.Text = "Player Switch";
            // 
            // DarkLabel15
            // 
            DarkLabel15.AutoSize = true;
            DarkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel15.Location = new Point(390, 98);
            DarkLabel15.Margin = new Padding(5, 0, 5, 0);
            DarkLabel15.Name = "DarkLabel15";
            DarkLabel15.Size = new Size(24, 25);
            DarkLabel15.TabIndex = 7;
            DarkLabel15.Text = "is";
            // 
            // cmbCondtion_PlayerSwitchCondition
            // 
            cmbCondtion_PlayerSwitchCondition.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondtion_PlayerSwitchCondition.FormattingEnabled = true;
            cmbCondtion_PlayerSwitchCondition.Items.AddRange(new object[] { "False", "True" });
            cmbCondtion_PlayerSwitchCondition.Location = new Point(437, 91);
            cmbCondtion_PlayerSwitchCondition.Margin = new Padding(5);
            cmbCondtion_PlayerSwitchCondition.Name = "cmbCondtion_PlayerSwitchCondition";
            cmbCondtion_PlayerSwitchCondition.Size = new Size(199, 32);
            cmbCondtion_PlayerSwitchCondition.TabIndex = 6;
            // 
            // cmbCondition_PlayerSwitch
            // 
            cmbCondition_PlayerSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_PlayerSwitch.FormattingEnabled = true;
            cmbCondition_PlayerSwitch.Location = new Point(178, 91);
            cmbCondition_PlayerSwitch.Margin = new Padding(5);
            cmbCondition_PlayerSwitch.Name = "cmbCondition_PlayerSwitch";
            cmbCondition_PlayerSwitch.Size = new Size(199, 32);
            cmbCondition_PlayerSwitch.TabIndex = 5;
            // 
            // nudCondition_PlayerVarCondition
            // 
            nudCondition_PlayerVarCondition.Location = new Point(558, 41);
            nudCondition_PlayerVarCondition.Margin = new Padding(5);
            nudCondition_PlayerVarCondition.Name = "nudCondition_PlayerVarCondition";
            nudCondition_PlayerVarCondition.Size = new Size(78, 31);
            nudCondition_PlayerVarCondition.TabIndex = 4;
            // 
            // cmbCondition_PlayerVarCompare
            // 
            cmbCondition_PlayerVarCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_PlayerVarCompare.FormattingEnabled = true;
            cmbCondition_PlayerVarCompare.Items.AddRange(new object[] { "Equal To", "Great Than Or Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal" });
            cmbCondition_PlayerVarCompare.Location = new Point(393, 40);
            cmbCondition_PlayerVarCompare.Margin = new Padding(5);
            cmbCondition_PlayerVarCompare.Name = "cmbCondition_PlayerVarCompare";
            cmbCondition_PlayerVarCompare.Size = new Size(144, 32);
            cmbCondition_PlayerVarCompare.TabIndex = 3;
            // 
            // DarkLabel14
            // 
            DarkLabel14.AutoSize = true;
            DarkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel14.Location = new Point(360, 47);
            DarkLabel14.Margin = new Padding(5, 0, 5, 0);
            DarkLabel14.Name = "DarkLabel14";
            DarkLabel14.Size = new Size(24, 25);
            DarkLabel14.TabIndex = 2;
            DarkLabel14.Text = "is";
            // 
            // cmbCondition_PlayerVarIndex
            // 
            cmbCondition_PlayerVarIndex.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_PlayerVarIndex.FormattingEnabled = true;
            cmbCondition_PlayerVarIndex.Location = new Point(178, 40);
            cmbCondition_PlayerVarIndex.Margin = new Padding(5);
            cmbCondition_PlayerVarIndex.Name = "cmbCondition_PlayerVarIndex";
            cmbCondition_PlayerVarIndex.Size = new Size(169, 32);
            cmbCondition_PlayerVarIndex.TabIndex = 1;
            // 
            // optCondition0
            // 
            optCondition0.AutoSize = true;
            optCondition0.Location = new Point(10, 41);
            optCondition0.Margin = new Padding(5);
            optCondition0.Name = "optCondition0";
            optCondition0.Size = new Size(151, 29);
            optCondition0.TabIndex = 0;
            optCondition0.TabStop = true;
            optCondition0.Text = "Player Variable";
            // 
            // fraPlayBGM
            // 
            fraPlayBGM.BackColor = Color.FromArgb(45, 45, 48);
            fraPlayBGM.BorderColor = Color.FromArgb(90, 90, 90);
            fraPlayBGM.Controls.Add(btnPlayBgmOk);
            fraPlayBGM.Controls.Add(btnPlayBgmCancel);
            fraPlayBGM.Controls.Add(cmbPlayBGM);
            fraPlayBGM.ForeColor = Color.Gainsboro;
            fraPlayBGM.Location = new Point(668, 2);
            fraPlayBGM.Margin = new Padding(5);
            fraPlayBGM.Name = "fraPlayBGM";
            fraPlayBGM.Padding = new Padding(5);
            fraPlayBGM.Size = new Size(410, 145);
            fraPlayBGM.TabIndex = 21;
            fraPlayBGM.TabStop = false;
            fraPlayBGM.Text = "Play BGM";
            fraPlayBGM.Visible = false;
            // 
            // btnPlayBgmOk
            // 
            btnPlayBgmOk.Location = new Point(77, 88);
            btnPlayBgmOk.Margin = new Padding(5);
            btnPlayBgmOk.Name = "btnPlayBgmOk";
            btnPlayBgmOk.Padding = new Padding(8, 10, 8, 10);
            btnPlayBgmOk.Size = new Size(125, 45);
            btnPlayBgmOk.TabIndex = 27;
            btnPlayBgmOk.Text = "Ok";
            // 
            // btnPlayBgmCancel
            // 
            btnPlayBgmCancel.Location = new Point(212, 88);
            btnPlayBgmCancel.Margin = new Padding(5);
            btnPlayBgmCancel.Name = "btnPlayBgmCancel";
            btnPlayBgmCancel.Padding = new Padding(8, 10, 8, 10);
            btnPlayBgmCancel.Size = new Size(125, 45);
            btnPlayBgmCancel.TabIndex = 26;
            btnPlayBgmCancel.Text = "Cancel";
            // 
            // cmbPlayBGM
            // 
            cmbPlayBGM.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayBGM.FormattingEnabled = true;
            cmbPlayBGM.Location = new Point(10, 37);
            cmbPlayBGM.Margin = new Padding(5);
            cmbPlayBGM.Name = "cmbPlayBGM";
            cmbPlayBGM.Size = new Size(386, 32);
            cmbPlayBGM.TabIndex = 0;
            // 
            // fraPlayerWarp
            // 
            fraPlayerWarp.BackColor = Color.FromArgb(45, 45, 48);
            fraPlayerWarp.BorderColor = Color.FromArgb(90, 90, 90);
            fraPlayerWarp.Controls.Add(btnPlayerWarpOk);
            fraPlayerWarp.Controls.Add(btnPlayerWarpCancel);
            fraPlayerWarp.Controls.Add(DarkLabel31);
            fraPlayerWarp.Controls.Add(cmbWarpPlayerDir);
            fraPlayerWarp.Controls.Add(nudWPY);
            fraPlayerWarp.Controls.Add(DarkLabel32);
            fraPlayerWarp.Controls.Add(nudWPX);
            fraPlayerWarp.Controls.Add(DarkLabel33);
            fraPlayerWarp.Controls.Add(nudWPMap);
            fraPlayerWarp.Controls.Add(DarkLabel34);
            fraPlayerWarp.ForeColor = Color.Gainsboro;
            fraPlayerWarp.Location = new Point(668, 12);
            fraPlayerWarp.Margin = new Padding(5);
            fraPlayerWarp.Name = "fraPlayerWarp";
            fraPlayerWarp.Padding = new Padding(5);
            fraPlayerWarp.Size = new Size(410, 187);
            fraPlayerWarp.TabIndex = 19;
            fraPlayerWarp.TabStop = false;
            fraPlayerWarp.Text = "Warp Player";
            fraPlayerWarp.Visible = false;
            // 
            // btnPlayerWarpOk
            // 
            btnPlayerWarpOk.Location = new Point(138, 130);
            btnPlayerWarpOk.Margin = new Padding(5);
            btnPlayerWarpOk.Name = "btnPlayerWarpOk";
            btnPlayerWarpOk.Padding = new Padding(8, 10, 8, 10);
            btnPlayerWarpOk.Size = new Size(125, 45);
            btnPlayerWarpOk.TabIndex = 46;
            btnPlayerWarpOk.Text = "Ok";
            // 
            // btnPlayerWarpCancel
            // 
            btnPlayerWarpCancel.Location = new Point(273, 130);
            btnPlayerWarpCancel.Margin = new Padding(5);
            btnPlayerWarpCancel.Name = "btnPlayerWarpCancel";
            btnPlayerWarpCancel.Padding = new Padding(8, 10, 8, 10);
            btnPlayerWarpCancel.Size = new Size(125, 45);
            btnPlayerWarpCancel.TabIndex = 45;
            btnPlayerWarpCancel.Text = "Cancel";
            // 
            // DarkLabel31
            // 
            DarkLabel31.AutoSize = true;
            DarkLabel31.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel31.Location = new Point(13, 85);
            DarkLabel31.Margin = new Padding(5, 0, 5, 0);
            DarkLabel31.Name = "DarkLabel31";
            DarkLabel31.Size = new Size(87, 25);
            DarkLabel31.TabIndex = 44;
            DarkLabel31.Text = "Direction:";
            // 
            // cmbWarpPlayerDir
            // 
            cmbWarpPlayerDir.DrawMode = DrawMode.OwnerDrawFixed;
            cmbWarpPlayerDir.FormattingEnabled = true;
            cmbWarpPlayerDir.Items.AddRange(new object[] { "Retain Direction", "Up", "Down", "Left", "Right" });
            cmbWarpPlayerDir.Location = new Point(160, 78);
            cmbWarpPlayerDir.Margin = new Padding(5);
            cmbWarpPlayerDir.Name = "cmbWarpPlayerDir";
            cmbWarpPlayerDir.Size = new Size(236, 32);
            cmbWarpPlayerDir.TabIndex = 43;
            // 
            // nudWPY
            // 
            nudWPY.Location = new Point(333, 28);
            nudWPY.Margin = new Padding(5);
            nudWPY.Name = "nudWPY";
            nudWPY.Size = new Size(65, 31);
            nudWPY.TabIndex = 42;
            // 
            // DarkLabel32
            // 
            DarkLabel32.AutoSize = true;
            DarkLabel32.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel32.Location = new Point(295, 34);
            DarkLabel32.Margin = new Padding(5, 0, 5, 0);
            DarkLabel32.Name = "DarkLabel32";
            DarkLabel32.Size = new Size(26, 25);
            DarkLabel32.TabIndex = 41;
            DarkLabel32.Text = "Y:";
            // 
            // nudWPX
            // 
            nudWPX.Location = new Point(217, 28);
            nudWPX.Margin = new Padding(5);
            nudWPX.Name = "nudWPX";
            nudWPX.Size = new Size(65, 31);
            nudWPX.TabIndex = 40;
            // 
            // DarkLabel33
            // 
            DarkLabel33.AutoSize = true;
            DarkLabel33.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel33.Location = new Point(178, 34);
            DarkLabel33.Margin = new Padding(5, 0, 5, 0);
            DarkLabel33.Name = "DarkLabel33";
            DarkLabel33.Size = new Size(27, 25);
            DarkLabel33.TabIndex = 39;
            DarkLabel33.Text = "X:";
            // 
            // nudWPMap
            // 
            nudWPMap.Location = new Point(72, 28);
            nudWPMap.Margin = new Padding(5);
            nudWPMap.Name = "nudWPMap";
            nudWPMap.Size = new Size(97, 31);
            nudWPMap.TabIndex = 38;
            // 
            // DarkLabel34
            // 
            DarkLabel34.AutoSize = true;
            DarkLabel34.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel34.Location = new Point(10, 34);
            DarkLabel34.Margin = new Padding(5, 0, 5, 0);
            DarkLabel34.Name = "DarkLabel34";
            DarkLabel34.Size = new Size(52, 25);
            DarkLabel34.TabIndex = 37;
            DarkLabel34.Text = "Map:";
            // 
            // fraSetFog
            // 
            fraSetFog.BackColor = Color.FromArgb(45, 45, 48);
            fraSetFog.BorderColor = Color.FromArgb(90, 90, 90);
            fraSetFog.Controls.Add(btnSetFogOk);
            fraSetFog.Controls.Add(btnSetFogCancel);
            fraSetFog.Controls.Add(DarkLabel30);
            fraSetFog.Controls.Add(DarkLabel29);
            fraSetFog.Controls.Add(DarkLabel28);
            fraSetFog.Controls.Add(nudFogData2);
            fraSetFog.Controls.Add(nudFogData1);
            fraSetFog.Controls.Add(nudFogData0);
            fraSetFog.ForeColor = Color.Gainsboro;
            fraSetFog.Location = new Point(668, 13);
            fraSetFog.Margin = new Padding(5);
            fraSetFog.Name = "fraSetFog";
            fraSetFog.Padding = new Padding(5);
            fraSetFog.Size = new Size(410, 185);
            fraSetFog.TabIndex = 18;
            fraSetFog.TabStop = false;
            fraSetFog.Text = "Set Fog";
            fraSetFog.Visible = false;
            // 
            // btnSetFogOk
            // 
            btnSetFogOk.Location = new Point(140, 128);
            btnSetFogOk.Margin = new Padding(5);
            btnSetFogOk.Name = "btnSetFogOk";
            btnSetFogOk.Padding = new Padding(8, 10, 8, 10);
            btnSetFogOk.Size = new Size(125, 45);
            btnSetFogOk.TabIndex = 41;
            btnSetFogOk.Text = "Ok";
            // 
            // btnSetFogCancel
            // 
            btnSetFogCancel.Location = new Point(275, 128);
            btnSetFogCancel.Margin = new Padding(5);
            btnSetFogCancel.Name = "btnSetFogCancel";
            btnSetFogCancel.Padding = new Padding(8, 10, 8, 10);
            btnSetFogCancel.Size = new Size(125, 45);
            btnSetFogCancel.TabIndex = 40;
            btnSetFogCancel.Text = "Cancel";
            // 
            // DarkLabel30
            // 
            DarkLabel30.AutoSize = true;
            DarkLabel30.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel30.Location = new Point(207, 80);
            DarkLabel30.Margin = new Padding(5, 0, 5, 0);
            DarkLabel30.Name = "DarkLabel30";
            DarkLabel30.Size = new Size(113, 25);
            DarkLabel30.TabIndex = 39;
            DarkLabel30.Text = "Fog Opacity:";
            // 
            // DarkLabel29
            // 
            DarkLabel29.AutoSize = true;
            DarkLabel29.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel29.Location = new Point(12, 80);
            DarkLabel29.Margin = new Padding(5, 0, 5, 0);
            DarkLabel29.Name = "DarkLabel29";
            DarkLabel29.Size = new Size(102, 25);
            DarkLabel29.TabIndex = 38;
            DarkLabel29.Text = "Fog Speed:";
            // 
            // DarkLabel28
            // 
            DarkLabel28.AutoSize = true;
            DarkLabel28.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel28.Location = new Point(12, 28);
            DarkLabel28.Margin = new Padding(5, 0, 5, 0);
            DarkLabel28.Name = "DarkLabel28";
            DarkLabel28.Size = new Size(47, 25);
            DarkLabel28.TabIndex = 37;
            DarkLabel28.Text = "Fog:";
            // 
            // nudFogData2
            // 
            nudFogData2.Location = new Point(318, 75);
            nudFogData2.Margin = new Padding(5);
            nudFogData2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudFogData2.Name = "nudFogData2";
            nudFogData2.Size = new Size(82, 31);
            nudFogData2.TabIndex = 36;
            // 
            // nudFogData1
            // 
            nudFogData1.Location = new Point(120, 77);
            nudFogData1.Margin = new Padding(5);
            nudFogData1.Name = "nudFogData1";
            nudFogData1.Size = new Size(80, 31);
            nudFogData1.TabIndex = 35;
            // 
            // nudFogData0
            // 
            nudFogData0.Location = new Point(162, 23);
            nudFogData0.Margin = new Padding(5);
            nudFogData0.Name = "nudFogData0";
            nudFogData0.Size = new Size(238, 31);
            nudFogData0.TabIndex = 34;
            // 
            // fraShowText
            // 
            fraShowText.BackColor = Color.FromArgb(45, 45, 48);
            fraShowText.BorderColor = Color.FromArgb(90, 90, 90);
            fraShowText.Controls.Add(DarkLabel27);
            fraShowText.Controls.Add(txtShowText);
            fraShowText.Controls.Add(btnShowTextCancel);
            fraShowText.Controls.Add(btnShowTextOk);
            fraShowText.ForeColor = Color.Gainsboro;
            fraShowText.Location = new Point(10, 585);
            fraShowText.Margin = new Padding(5);
            fraShowText.Name = "fraShowText";
            fraShowText.Padding = new Padding(5);
            fraShowText.Size = new Size(413, 547);
            fraShowText.TabIndex = 17;
            fraShowText.TabStop = false;
            fraShowText.Text = "Show Text";
            fraShowText.Visible = false;
            // 
            // DarkLabel27
            // 
            DarkLabel27.AutoSize = true;
            DarkLabel27.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel27.Location = new Point(12, 37);
            DarkLabel27.Margin = new Padding(5, 0, 5, 0);
            DarkLabel27.Name = "DarkLabel27";
            DarkLabel27.Size = new Size(42, 25);
            DarkLabel27.TabIndex = 26;
            DarkLabel27.Text = "Text";
            // 
            // txtShowText
            // 
            txtShowText.BackColor = Color.FromArgb(69, 73, 74);
            txtShowText.BorderStyle = BorderStyle.FixedSingle;
            txtShowText.ForeColor = Color.FromArgb(220, 220, 220);
            txtShowText.Location = new Point(15, 73);
            txtShowText.Margin = new Padding(5);
            txtShowText.Multiline = true;
            txtShowText.Name = "txtShowText";
            txtShowText.Size = new Size(379, 200);
            txtShowText.TabIndex = 21;
            // 
            // btnShowTextCancel
            // 
            btnShowTextCancel.Location = new Point(278, 485);
            btnShowTextCancel.Margin = new Padding(5);
            btnShowTextCancel.Name = "btnShowTextCancel";
            btnShowTextCancel.Padding = new Padding(8, 10, 8, 10);
            btnShowTextCancel.Size = new Size(125, 45);
            btnShowTextCancel.TabIndex = 24;
            btnShowTextCancel.Text = "Cancel";
            // 
            // btnShowTextOk
            // 
            btnShowTextOk.Location = new Point(143, 485);
            btnShowTextOk.Margin = new Padding(5);
            btnShowTextOk.Name = "btnShowTextOk";
            btnShowTextOk.Padding = new Padding(8, 10, 8, 10);
            btnShowTextOk.Size = new Size(125, 45);
            btnShowTextOk.TabIndex = 25;
            btnShowTextOk.Text = "Ok";
            // 
            // fraAddText
            // 
            fraAddText.BackColor = Color.FromArgb(45, 45, 48);
            fraAddText.BorderColor = Color.FromArgb(90, 90, 90);
            fraAddText.Controls.Add(btnAddTextOk);
            fraAddText.Controls.Add(btnAddTextCancel);
            fraAddText.Controls.Add(optAddText_Global);
            fraAddText.Controls.Add(optAddText_Map);
            fraAddText.Controls.Add(optAddText_Player);
            fraAddText.Controls.Add(DarkLabel25);
            fraAddText.Controls.Add(txtAddText_Text);
            fraAddText.Controls.Add(DarkLabel24);
            fraAddText.ForeColor = Color.Gainsboro;
            fraAddText.Location = new Point(10, 698);
            fraAddText.Margin = new Padding(5);
            fraAddText.Name = "fraAddText";
            fraAddText.Padding = new Padding(5);
            fraAddText.Size = new Size(388, 360);
            fraAddText.TabIndex = 3;
            fraAddText.TabStop = false;
            fraAddText.Text = "Add Text";
            fraAddText.Visible = false;
            // 
            // btnAddTextOk
            // 
            btnAddTextOk.Location = new Point(92, 300);
            btnAddTextOk.Margin = new Padding(5);
            btnAddTextOk.Name = "btnAddTextOk";
            btnAddTextOk.Padding = new Padding(8, 10, 8, 10);
            btnAddTextOk.Size = new Size(125, 45);
            btnAddTextOk.TabIndex = 9;
            btnAddTextOk.Text = "Ok";
            // 
            // btnAddTextCancel
            // 
            btnAddTextCancel.Location = new Point(227, 300);
            btnAddTextCancel.Margin = new Padding(5);
            btnAddTextCancel.Name = "btnAddTextCancel";
            btnAddTextCancel.Padding = new Padding(8, 10, 8, 10);
            btnAddTextCancel.Size = new Size(125, 45);
            btnAddTextCancel.TabIndex = 8;
            btnAddTextCancel.Text = "Cancel";
            // 
            // optAddText_Global
            // 
            optAddText_Global.AutoSize = true;
            optAddText_Global.Location = new Point(288, 255);
            optAddText_Global.Margin = new Padding(5);
            optAddText_Global.Name = "optAddText_Global";
            optAddText_Global.Size = new Size(88, 29);
            optAddText_Global.TabIndex = 5;
            optAddText_Global.TabStop = true;
            optAddText_Global.Text = "Global";
            // 
            // optAddText_Map
            // 
            optAddText_Map.AutoSize = true;
            optAddText_Map.Location = new Point(202, 255);
            optAddText_Map.Margin = new Padding(5);
            optAddText_Map.Name = "optAddText_Map";
            optAddText_Map.Size = new Size(73, 29);
            optAddText_Map.TabIndex = 4;
            optAddText_Map.TabStop = true;
            optAddText_Map.Text = "Map";
            // 
            // optAddText_Player
            // 
            optAddText_Player.AutoSize = true;
            optAddText_Player.Location = new Point(102, 255);
            optAddText_Player.Margin = new Padding(5);
            optAddText_Player.Name = "optAddText_Player";
            optAddText_Player.Size = new Size(84, 29);
            optAddText_Player.TabIndex = 3;
            optAddText_Player.TabStop = true;
            optAddText_Player.Text = "Player";
            // 
            // DarkLabel25
            // 
            DarkLabel25.AutoSize = true;
            DarkLabel25.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel25.Location = new Point(10, 260);
            DarkLabel25.Margin = new Padding(5, 0, 5, 0);
            DarkLabel25.Name = "DarkLabel25";
            DarkLabel25.Size = new Size(79, 25);
            DarkLabel25.TabIndex = 2;
            DarkLabel25.Text = "Channel:";
            // 
            // txtAddText_Text
            // 
            txtAddText_Text.BackColor = Color.FromArgb(69, 73, 74);
            txtAddText_Text.BorderStyle = BorderStyle.FixedSingle;
            txtAddText_Text.ForeColor = Color.FromArgb(220, 220, 220);
            txtAddText_Text.Location = new Point(10, 60);
            txtAddText_Text.Margin = new Padding(5);
            txtAddText_Text.Multiline = true;
            txtAddText_Text.Name = "txtAddText_Text";
            txtAddText_Text.Size = new Size(369, 182);
            txtAddText_Text.TabIndex = 1;
            // 
            // DarkLabel24
            // 
            DarkLabel24.AutoSize = true;
            DarkLabel24.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel24.Location = new Point(10, 28);
            DarkLabel24.Margin = new Padding(5, 0, 5, 0);
            DarkLabel24.Name = "DarkLabel24";
            DarkLabel24.Size = new Size(42, 25);
            DarkLabel24.TabIndex = 0;
            DarkLabel24.Text = "Text";
            // 
            // fraChangeItems
            // 
            fraChangeItems.BackColor = Color.FromArgb(45, 45, 48);
            fraChangeItems.BorderColor = Color.FromArgb(90, 90, 90);
            fraChangeItems.Controls.Add(btnChangeItemsOk);
            fraChangeItems.Controls.Add(btnChangeItemsCancel);
            fraChangeItems.Controls.Add(nudChangeItemsAmount);
            fraChangeItems.Controls.Add(optChangeItemRemove);
            fraChangeItems.Controls.Add(optChangeItemAdd);
            fraChangeItems.Controls.Add(optChangeItemSet);
            fraChangeItems.Controls.Add(cmbChangeItemIndex);
            fraChangeItems.Controls.Add(DarkLabel21);
            fraChangeItems.ForeColor = Color.Gainsboro;
            fraChangeItems.Location = new Point(10, 750);
            fraChangeItems.Margin = new Padding(5);
            fraChangeItems.Name = "fraChangeItems";
            fraChangeItems.Padding = new Padding(5);
            fraChangeItems.Size = new Size(312, 230);
            fraChangeItems.TabIndex = 1;
            fraChangeItems.TabStop = false;
            fraChangeItems.Text = "Change Items";
            fraChangeItems.Visible = false;
            // 
            // btnChangeItemsOk
            // 
            btnChangeItemsOk.Location = new Point(42, 175);
            btnChangeItemsOk.Margin = new Padding(5);
            btnChangeItemsOk.Name = "btnChangeItemsOk";
            btnChangeItemsOk.Padding = new Padding(8, 10, 8, 10);
            btnChangeItemsOk.Size = new Size(125, 45);
            btnChangeItemsOk.TabIndex = 7;
            btnChangeItemsOk.Text = "Ok";
            // 
            // btnChangeItemsCancel
            // 
            btnChangeItemsCancel.Location = new Point(177, 175);
            btnChangeItemsCancel.Margin = new Padding(5);
            btnChangeItemsCancel.Name = "btnChangeItemsCancel";
            btnChangeItemsCancel.Padding = new Padding(8, 10, 8, 10);
            btnChangeItemsCancel.Size = new Size(125, 45);
            btnChangeItemsCancel.TabIndex = 6;
            btnChangeItemsCancel.Text = "Cancel";
            // 
            // nudChangeItemsAmount
            // 
            nudChangeItemsAmount.Location = new Point(15, 125);
            nudChangeItemsAmount.Margin = new Padding(5);
            nudChangeItemsAmount.Name = "nudChangeItemsAmount";
            nudChangeItemsAmount.Size = new Size(287, 31);
            nudChangeItemsAmount.TabIndex = 5;
            // 
            // optChangeItemRemove
            // 
            optChangeItemRemove.AutoSize = true;
            optChangeItemRemove.Location = new Point(202, 80);
            optChangeItemRemove.Margin = new Padding(5);
            optChangeItemRemove.Name = "optChangeItemRemove";
            optChangeItemRemove.Size = new Size(71, 29);
            optChangeItemRemove.TabIndex = 4;
            optChangeItemRemove.TabStop = true;
            optChangeItemRemove.Text = "Take";
            // 
            // optChangeItemAdd
            // 
            optChangeItemAdd.AutoSize = true;
            optChangeItemAdd.Location = new Point(113, 80);
            optChangeItemAdd.Margin = new Padding(5);
            optChangeItemAdd.Name = "optChangeItemAdd";
            optChangeItemAdd.Size = new Size(71, 29);
            optChangeItemAdd.TabIndex = 3;
            optChangeItemAdd.TabStop = true;
            optChangeItemAdd.Text = "Give";
            // 
            // optChangeItemSet
            // 
            optChangeItemSet.AutoSize = true;
            optChangeItemSet.Location = new Point(15, 80);
            optChangeItemSet.Margin = new Padding(5);
            optChangeItemSet.Name = "optChangeItemSet";
            optChangeItemSet.Size = new Size(84, 29);
            optChangeItemSet.TabIndex = 2;
            optChangeItemSet.TabStop = true;
            optChangeItemSet.Text = "Set to";
            // 
            // cmbChangeItemIndex
            // 
            cmbChangeItemIndex.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChangeItemIndex.FormattingEnabled = true;
            cmbChangeItemIndex.Location = new Point(70, 25);
            cmbChangeItemIndex.Margin = new Padding(5);
            cmbChangeItemIndex.Name = "cmbChangeItemIndex";
            cmbChangeItemIndex.Size = new Size(229, 32);
            cmbChangeItemIndex.TabIndex = 1;
            // 
            // DarkLabel21
            // 
            DarkLabel21.AutoSize = true;
            DarkLabel21.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel21.Location = new Point(10, 30);
            DarkLabel21.Margin = new Padding(5, 0, 5, 0);
            DarkLabel21.Name = "DarkLabel21";
            DarkLabel21.Size = new Size(52, 25);
            DarkLabel21.TabIndex = 0;
            DarkLabel21.Text = "Item:";
            // 
            // pnlVariableSwitches
            // 
            pnlVariableSwitches.Controls.Add(FraRenaming);
            pnlVariableSwitches.Controls.Add(fraLabeling);
            pnlVariableSwitches.Location = new Point(1333, 387);
            pnlVariableSwitches.Margin = new Padding(5);
            pnlVariableSwitches.Name = "pnlVariableSwitches";
            pnlVariableSwitches.Size = new Size(155, 175);
            pnlVariableSwitches.TabIndex = 11;
            // 
            // FraRenaming
            // 
            FraRenaming.Controls.Add(btnRename_Cancel);
            FraRenaming.Controls.Add(btnRename_Ok);
            FraRenaming.Controls.Add(fraRandom10);
            FraRenaming.ForeColor = Color.Gainsboro;
            FraRenaming.Location = new Point(393, 825);
            FraRenaming.Margin = new Padding(5);
            FraRenaming.Name = "FraRenaming";
            FraRenaming.Padding = new Padding(5);
            FraRenaming.Size = new Size(607, 275);
            FraRenaming.TabIndex = 8;
            FraRenaming.TabStop = false;
            FraRenaming.Text = "Renaming Variable/Switch";
            FraRenaming.Visible = false;
            // 
            // btnRename_Cancel
            // 
            btnRename_Cancel.ForeColor = Color.Black;
            btnRename_Cancel.Location = new Point(382, 197);
            btnRename_Cancel.Margin = new Padding(5);
            btnRename_Cancel.Name = "btnRename_Cancel";
            btnRename_Cancel.Size = new Size(125, 45);
            btnRename_Cancel.TabIndex = 2;
            btnRename_Cancel.Text = "Cancel";
            btnRename_Cancel.UseVisualStyleBackColor = true;
            // 
            // btnRename_Ok
            // 
            btnRename_Ok.ForeColor = Color.Black;
            btnRename_Ok.Location = new Point(90, 197);
            btnRename_Ok.Margin = new Padding(5);
            btnRename_Ok.Name = "btnRename_Ok";
            btnRename_Ok.Size = new Size(125, 45);
            btnRename_Ok.TabIndex = 1;
            btnRename_Ok.Text = "Ok";
            btnRename_Ok.UseVisualStyleBackColor = true;
            // 
            // fraRandom10
            // 
            fraRandom10.Controls.Add(txtRename);
            fraRandom10.Controls.Add(lblEditing);
            fraRandom10.ForeColor = Color.Gainsboro;
            fraRandom10.Location = new Point(10, 37);
            fraRandom10.Margin = new Padding(5);
            fraRandom10.Name = "fraRandom10";
            fraRandom10.Padding = new Padding(5);
            fraRandom10.Size = new Size(587, 148);
            fraRandom10.TabIndex = 0;
            fraRandom10.TabStop = false;
            fraRandom10.Text = "Editing Variable/Switch";
            // 
            // txtRename
            // 
            txtRename.Location = new Point(10, 78);
            txtRename.Margin = new Padding(5);
            txtRename.Name = "txtRename";
            txtRename.Size = new Size(564, 31);
            txtRename.TabIndex = 1;
            // 
            // lblEditing
            // 
            lblEditing.AutoSize = true;
            lblEditing.Location = new Point(5, 48);
            lblEditing.Margin = new Padding(5, 0, 5, 0);
            lblEditing.Name = "lblEditing";
            lblEditing.Size = new Size(168, 25);
            lblEditing.TabIndex = 0;
            lblEditing.Text = "Naming Variable #1";
            // 
            // fraLabeling
            // 
            fraLabeling.BackColor = Color.FromArgb(45, 45, 48);
            fraLabeling.BorderColor = Color.FromArgb(90, 90, 90);
            fraLabeling.Controls.Add(lstSwitches);
            fraLabeling.Controls.Add(lstVariables);
            fraLabeling.Controls.Add(btnLabel_Cancel);
            fraLabeling.Controls.Add(lblRandomLabel36);
            fraLabeling.Controls.Add(btnRenameVariable);
            fraLabeling.Controls.Add(lblRandomLabel25);
            fraLabeling.Controls.Add(btnRenameSwitch);
            fraLabeling.Controls.Add(btnLabel_Ok);
            fraLabeling.ForeColor = Color.Gainsboro;
            fraLabeling.Location = new Point(325, 55);
            fraLabeling.Margin = new Padding(5);
            fraLabeling.Name = "fraLabeling";
            fraLabeling.Padding = new Padding(5);
            fraLabeling.Size = new Size(760, 745);
            fraLabeling.TabIndex = 0;
            fraLabeling.TabStop = false;
            fraLabeling.Text = "Label Variables and  Switches   ";
            // 
            // lstSwitches
            // 
            lstSwitches.BackColor = Color.FromArgb(45, 45, 48);
            lstSwitches.BorderStyle = BorderStyle.FixedSingle;
            lstSwitches.ForeColor = Color.Gainsboro;
            lstSwitches.FormattingEnabled = true;
            lstSwitches.ItemHeight = 25;
            lstSwitches.Location = new Point(393, 75);
            lstSwitches.Margin = new Padding(5);
            lstSwitches.Name = "lstSwitches";
            lstSwitches.Size = new Size(340, 552);
            lstSwitches.TabIndex = 7;
            // 
            // lstVariables
            // 
            lstVariables.BackColor = Color.FromArgb(45, 45, 48);
            lstVariables.BorderStyle = BorderStyle.FixedSingle;
            lstVariables.ForeColor = Color.Gainsboro;
            lstVariables.FormattingEnabled = true;
            lstVariables.ItemHeight = 25;
            lstVariables.Location = new Point(23, 75);
            lstVariables.Margin = new Padding(5);
            lstVariables.Name = "lstVariables";
            lstVariables.Size = new Size(340, 552);
            lstVariables.TabIndex = 6;
            // 
            // btnLabel_Cancel
            // 
            btnLabel_Cancel.ForeColor = Color.Black;
            btnLabel_Cancel.Location = new Point(393, 655);
            btnLabel_Cancel.Margin = new Padding(5);
            btnLabel_Cancel.Name = "btnLabel_Cancel";
            btnLabel_Cancel.Padding = new Padding(5);
            btnLabel_Cancel.Size = new Size(125, 45);
            btnLabel_Cancel.TabIndex = 12;
            btnLabel_Cancel.Text = "Cancel";
            // 
            // lblRandomLabel36
            // 
            lblRandomLabel36.AutoSize = true;
            lblRandomLabel36.ForeColor = Color.FromArgb(220, 220, 220);
            lblRandomLabel36.Location = new Point(488, 45);
            lblRandomLabel36.Margin = new Padding(5, 0, 5, 0);
            lblRandomLabel36.Name = "lblRandomLabel36";
            lblRandomLabel36.Size = new Size(132, 25);
            lblRandomLabel36.TabIndex = 5;
            lblRandomLabel36.Text = "Player Switches";
            // 
            // btnRenameVariable
            // 
            btnRenameVariable.ForeColor = Color.Black;
            btnRenameVariable.Location = new Point(23, 655);
            btnRenameVariable.Margin = new Padding(5);
            btnRenameVariable.Name = "btnRenameVariable";
            btnRenameVariable.Padding = new Padding(5);
            btnRenameVariable.Size = new Size(177, 45);
            btnRenameVariable.TabIndex = 9;
            btnRenameVariable.Text = "Rename Variable";
            // 
            // lblRandomLabel25
            // 
            lblRandomLabel25.AutoSize = true;
            lblRandomLabel25.ForeColor = Color.FromArgb(220, 220, 220);
            lblRandomLabel25.Location = new Point(133, 40);
            lblRandomLabel25.Margin = new Padding(5, 0, 5, 0);
            lblRandomLabel25.Name = "lblRandomLabel25";
            lblRandomLabel25.Size = new Size(134, 25);
            lblRandomLabel25.TabIndex = 4;
            lblRandomLabel25.Text = "Player Variables";
            // 
            // btnRenameSwitch
            // 
            btnRenameSwitch.ForeColor = Color.Black;
            btnRenameSwitch.Location = new Point(553, 655);
            btnRenameSwitch.Margin = new Padding(5);
            btnRenameSwitch.Name = "btnRenameSwitch";
            btnRenameSwitch.Padding = new Padding(5);
            btnRenameSwitch.Size = new Size(182, 45);
            btnRenameSwitch.TabIndex = 10;
            btnRenameSwitch.Text = "Rename Switch";
            // 
            // btnLabel_Ok
            // 
            btnLabel_Ok.ForeColor = Color.Black;
            btnLabel_Ok.Location = new Point(240, 655);
            btnLabel_Ok.Margin = new Padding(5);
            btnLabel_Ok.Name = "btnLabel_Ok";
            btnLabel_Ok.Padding = new Padding(5);
            btnLabel_Ok.Size = new Size(125, 45);
            btnLabel_Ok.TabIndex = 11;
            btnLabel_Ok.Text = "Ok";
            // 
            // frmEditor_Events
            // 
            AutoScaleDimensions = new SizeF(10.0f, 25.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(2718, 1182);
            Controls.Add(pnlVariableSwitches);
            Controls.Add(fraDialogue);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(btnLabeling);
            Controls.Add(tabPages);
            Controls.Add(fraPageSetUp);
            Controls.Add(pnlTabPage);
            Controls.Add(pnlGraphicSel);
            Controls.Add(fraMoveRoute);
            ForeColor = Color.Gainsboro;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(5);
            Name = "frmEditor_Events";
            Text = "Event Editor";
            fraPageSetUp.ResumeLayout(false);
            fraPageSetUp.PerformLayout();
            tabPages.ResumeLayout(false);
            pnlTabPage.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            fraGraphicPic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picGraphic).EndInit();
            DarkGroupBox6.ResumeLayout(false);
            DarkGroupBox6.PerformLayout();
            DarkGroupBox5.ResumeLayout(false);
            DarkGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picGraphicSel).EndInit();
            DarkGroupBox3.ResumeLayout(false);
            DarkGroupBox3.PerformLayout();
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPlayerVariable).EndInit();
            DarkGroupBox8.ResumeLayout(false);
            fraGraphic.ResumeLayout(false);
            fraGraphic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudGraphic).EndInit();
            fraCommands.ResumeLayout(false);
            fraMoveRoute.ResumeLayout(false);
            fraMoveRoute.PerformLayout();
            DarkGroupBox10.ResumeLayout(false);
            fraDialogue.ResumeLayout(false);
            fraShowChatBubble.ResumeLayout(false);
            fraShowChatBubble.PerformLayout();
            fraOpenShop.ResumeLayout(false);
            fraSetSelfSwitch.ResumeLayout(false);
            fraSetSelfSwitch.PerformLayout();
            fraPlaySound.ResumeLayout(false);
            fraChangePK.ResumeLayout(false);
            fraCreateLabel.ResumeLayout(false);
            fraCreateLabel.PerformLayout();
            fraChangeJob.ResumeLayout(false);
            fraChangeJob.PerformLayout();
            fraChangeSkills.ResumeLayout(false);
            fraChangeSkills.PerformLayout();
            fraPlayerSwitch.ResumeLayout(false);
            fraPlayerSwitch.PerformLayout();
            fraSetWait.ResumeLayout(false);
            fraSetWait.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudWaitAmount).EndInit();
            fraMoveRouteWait.ResumeLayout(false);
            fraMoveRouteWait.PerformLayout();
            fraCustomScript.ResumeLayout(false);
            fraCustomScript.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCustomScript).EndInit();
            fraSpawnNPC.ResumeLayout(false);
            fraSetWeather.ResumeLayout(false);
            fraSetWeather.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudWeatherIntensity).EndInit();
            fraGiveExp.ResumeLayout(false);
            fraGiveExp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiveExp).EndInit();
            fraSetAccess.ResumeLayout(false);
            fraChangeGender.ResumeLayout(false);
            fraChangeGender.PerformLayout();
            fraShowChoices.ResumeLayout(false);
            fraShowChoices.PerformLayout();
            fraChangeLevel.ResumeLayout(false);
            fraChangeLevel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudChangeLevel).EndInit();
            fraPlayerVariable.ResumeLayout(false);
            fraPlayerVariable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudVariableData2).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVariableData4).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVariableData3).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVariableData1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVariableData0).EndInit();
            fraPlayAnimation.ResumeLayout(false);
            fraPlayAnimation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPlayAnimTileY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPlayAnimTileX).EndInit();
            fraChangeSprite.ResumeLayout(false);
            fraChangeSprite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudChangeSprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)picChangeSprite).EndInit();
            fraGoToLabel.ResumeLayout(false);
            fraGoToLabel.PerformLayout();
            fraMapTint.ResumeLayout(false);
            fraMapTint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMapTintData3).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMapTintData2).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMapTintData1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMapTintData0).EndInit();
            fraShowPic.ResumeLayout(false);
            fraShowPic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPicOffsetY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPicOffsetX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudShowPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)picShowPic).EndInit();
            fraConditionalBranch.ResumeLayout(false);
            fraConditionalBranch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCondition_LevelAmount).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCondition_HasItem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCondition_PlayerVarCondition).EndInit();
            fraPlayBGM.ResumeLayout(false);
            fraPlayerWarp.ResumeLayout(false);
            fraPlayerWarp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudWPY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWPX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWPMap).EndInit();
            fraSetFog.ResumeLayout(false);
            fraSetFog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudFogData2).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudFogData1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudFogData0).EndInit();
            fraShowText.ResumeLayout(false);
            fraShowText.PerformLayout();
            fraAddText.ResumeLayout(false);
            fraAddText.PerformLayout();
            fraChangeItems.ResumeLayout(false);
            fraChangeItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudChangeItemsAmount).EndInit();
            pnlVariableSwitches.ResumeLayout(false);
            FraRenaming.ResumeLayout(false);
            fraRandom10.ResumeLayout(false);
            fraRandom10.PerformLayout();
            fraLabeling.ResumeLayout(false);
            fraLabeling.PerformLayout();
            Load += new EventHandler(frmEditor_Events_Load);
            FormClosing += new FormClosingEventHandler(frmEditor_Events_FormClosing);
            ResumeLayout(false);

        }

        internal TreeView tvCommands;
        internal DarkUI.Controls.DarkGroupBox fraPageSetUp;
        internal TabControl tabPages;
        internal TabPage TabPage1;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal DarkUI.Controls.DarkButton btnNewPage;
        internal DarkUI.Controls.DarkButton btnCopyPage;
        internal DarkUI.Controls.DarkButton btnPastePage;
        internal DarkUI.Controls.DarkButton btnClearPage;
        internal DarkUI.Controls.DarkButton btnDeletePage;
        internal Panel pnlTabPage;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal DarkUI.Controls.DarkCheckBox chkPlayerVar;
        internal DarkUI.Controls.DarkComboBox cmbPlayerVar;
        internal DarkUI.Controls.DarkLabel DarkLabel2;
        internal DarkUI.Controls.DarkComboBox cmbPlayervarCompare;
        internal DarkUI.Controls.DarkNumericUpDown nudPlayerVariable;
        internal DarkUI.Controls.DarkCheckBox chkPlayerSwitch;
        internal DarkUI.Controls.DarkComboBox cmbPlayerSwitch;
        internal DarkUI.Controls.DarkComboBox cmbPlayerSwitchCompare;
        internal DarkUI.Controls.DarkLabel DarkLabel3;
        internal DarkUI.Controls.DarkComboBox cmbHasItem;
        internal DarkUI.Controls.DarkCheckBox chkHasItem;
        internal DarkUI.Controls.DarkComboBox cmbSelfSwitch;
        internal DarkUI.Controls.DarkCheckBox chkSelfSwitch;
        internal DarkUI.Controls.DarkComboBox cmbSelfSwitchCompare;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox3;
        internal DarkUI.Controls.DarkCheckBox chkGlobal;
        internal DarkUI.Controls.DarkLabel DarkLabel5;
        internal DarkUI.Controls.DarkComboBox cmbMoveType;
        internal DarkUI.Controls.DarkButton btnMoveRoute;
        internal DarkUI.Controls.DarkLabel DarkLabel6;
        internal DarkUI.Controls.DarkComboBox cmbMoveSpeed;
        internal DarkUI.Controls.DarkComboBox cmbMoveFreq;
        internal DarkUI.Controls.DarkLabel DarkLabel7;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox4;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox5;
        internal DarkUI.Controls.DarkComboBox cmbTrigger;
        internal DarkUI.Controls.DarkLabel DarkLabel8;
        internal ListBox lstCommands;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox8;
        internal DarkUI.Controls.DarkButton btnAddCommand;
        internal DarkUI.Controls.DarkButton btnDeleteCommand;
        internal DarkUI.Controls.DarkButton btnEditCommand;
        internal DarkUI.Controls.DarkButton btnClearCommand;
        internal Panel fraCommands;
        internal DarkUI.Controls.DarkButton btnLabeling;
        internal DarkUI.Controls.DarkButton btnCancel;
        internal DarkUI.Controls.DarkButton btnOk;
        internal DarkUI.Controls.DarkButton btnCancelCommand;
        internal DarkUI.Controls.DarkGroupBox fraMoveRoute;
        internal DarkUI.Controls.DarkComboBox cmbEvent;
        internal ListBox lstMoveRoute;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox10;
        internal ListView lstvwMoveRoute;
        internal ColumnHeader ColumnHeader3;
        internal ColumnHeader ColumnHeader4;
        internal DarkUI.Controls.DarkCheckBox chkRepeatRoute;
        internal DarkUI.Controls.DarkCheckBox chkIgnoreMove;
        internal DarkUI.Controls.DarkButton btnMoveRouteOk;
        internal DarkUI.Controls.DarkButton btnMoveRouteCancel;
        internal PictureBox picGraphicSel;
        internal DarkUI.Controls.DarkGroupBox fraDialogue;
        internal DarkUI.Controls.DarkGroupBox fraConditionalBranch;
        internal DarkUI.Controls.DarkRadioButton optCondition0;
        internal DarkUI.Controls.DarkComboBox cmbCondition_PlayerVarIndex;
        internal DarkUI.Controls.DarkNumericUpDown nudCondition_PlayerVarCondition;
        internal DarkUI.Controls.DarkComboBox cmbCondition_PlayerVarCompare;
        internal DarkUI.Controls.DarkLabel DarkLabel14;
        internal DarkUI.Controls.DarkRadioButton optCondition1;
        internal DarkUI.Controls.DarkLabel DarkLabel15;
        internal DarkUI.Controls.DarkComboBox cmbCondtion_PlayerSwitchCondition;
        internal DarkUI.Controls.DarkComboBox cmbCondition_PlayerSwitch;
        internal DarkUI.Controls.DarkRadioButton optCondition2;
        internal DarkUI.Controls.DarkNumericUpDown nudCondition_HasItem;
        internal DarkUI.Controls.DarkLabel DarkLabel16;
        internal DarkUI.Controls.DarkComboBox cmbCondition_HasItem;
        internal DarkUI.Controls.DarkRadioButton optCondition3;
        internal DarkUI.Controls.DarkComboBox cmbCondition_JobIs;
        internal DarkUI.Controls.DarkRadioButton optCondition4;
        internal DarkUI.Controls.DarkComboBox cmbCondition_LearntSkill;
        internal DarkUI.Controls.DarkRadioButton optCondition5;
        internal DarkUI.Controls.DarkComboBox cmbCondition_LevelCompare;
        internal DarkUI.Controls.DarkNumericUpDown nudCondition_LevelAmount;
        internal DarkUI.Controls.DarkRadioButton optCondition6;
        internal DarkUI.Controls.DarkComboBox cmbCondition_SelfSwitchCondition;
        internal DarkUI.Controls.DarkLabel DarkLabel17;
        internal DarkUI.Controls.DarkComboBox cmbCondition_SelfSwitch;
        internal DarkUI.Controls.DarkRadioButton optCondition8;
        internal DarkUI.Controls.DarkComboBox cmbCondition_Gender;
        internal DarkUI.Controls.DarkButton btnConditionalBranchOk;
        internal DarkUI.Controls.DarkButton btnConditionalBranchCancel;
        internal DarkUI.Controls.DarkGroupBox fraChangeItems;
        internal DarkUI.Controls.DarkGroupBox fraPlayerSwitch;
        internal DarkUI.Controls.DarkComboBox cmbChangeItemIndex;
        internal DarkUI.Controls.DarkLabel DarkLabel21;
        internal DarkUI.Controls.DarkRadioButton optChangeItemSet;
        internal DarkUI.Controls.DarkRadioButton optChangeItemRemove;
        internal DarkUI.Controls.DarkRadioButton optChangeItemAdd;
        internal DarkUI.Controls.DarkNumericUpDown nudChangeItemsAmount;
        internal DarkUI.Controls.DarkButton btnChangeItemsOk;
        internal DarkUI.Controls.DarkButton btnChangeItemsCancel;
        internal DarkUI.Controls.DarkComboBox cmbSwitch;
        internal DarkUI.Controls.DarkLabel DarkLabel22;
        internal DarkUI.Controls.DarkLabel DarkLabel23;
        internal DarkUI.Controls.DarkComboBox cmbPlayerSwitchSet;
        internal DarkUI.Controls.DarkButton btnSetPlayerSwitchOk;
        internal DarkUI.Controls.DarkButton btnSetPlayerswitchCancel;
        internal DarkUI.Controls.DarkGroupBox fraAddText;
        internal DarkUI.Controls.DarkTextBox txtAddText_Text;
        internal DarkUI.Controls.DarkLabel DarkLabel24;
        internal DarkUI.Controls.DarkRadioButton optAddText_Player;
        internal DarkUI.Controls.DarkLabel DarkLabel25;
        internal DarkUI.Controls.DarkRadioButton optAddText_Map;
        internal DarkUI.Controls.DarkButton btnAddTextOk;
        internal DarkUI.Controls.DarkButton btnAddTextCancel;
        internal DarkUI.Controls.DarkRadioButton optAddText_Global;
        internal DarkUI.Controls.DarkButton btnShowTextOk;
        internal DarkUI.Controls.DarkButton btnShowTextCancel;
        internal DarkUI.Controls.DarkTextBox txtShowText;
        internal DarkUI.Controls.DarkLabel DarkLabel27;
        internal DarkUI.Controls.DarkGroupBox fraShowText;
        internal DarkUI.Controls.DarkGroupBox fraSetFog;
        internal DarkUI.Controls.DarkButton btnSetFogOk;
        internal DarkUI.Controls.DarkButton btnSetFogCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel30;
        internal DarkUI.Controls.DarkLabel DarkLabel29;
        internal DarkUI.Controls.DarkLabel DarkLabel28;
        internal DarkUI.Controls.DarkNumericUpDown nudFogData2;
        internal DarkUI.Controls.DarkNumericUpDown nudFogData1;
        internal DarkUI.Controls.DarkNumericUpDown nudFogData0;
        internal DarkUI.Controls.DarkGroupBox fraPlayerWarp;
        internal DarkUI.Controls.DarkButton btnPlayerWarpOk;
        internal DarkUI.Controls.DarkButton btnPlayerWarpCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel31;
        internal DarkUI.Controls.DarkComboBox cmbWarpPlayerDir;
        internal DarkUI.Controls.DarkNumericUpDown nudWPY;
        internal DarkUI.Controls.DarkLabel DarkLabel32;
        internal DarkUI.Controls.DarkNumericUpDown nudWPX;
        internal DarkUI.Controls.DarkLabel DarkLabel33;
        internal DarkUI.Controls.DarkNumericUpDown nudWPMap;
        internal DarkUI.Controls.DarkLabel DarkLabel34;
        internal DarkUI.Controls.DarkGroupBox fraPlayBGM;
        internal DarkUI.Controls.DarkComboBox cmbPlayBGM;
        internal DarkUI.Controls.DarkButton btnPlayBgmOk;
        internal DarkUI.Controls.DarkButton btnPlayBgmCancel;
        internal DarkUI.Controls.DarkGroupBox fraChangeSkills;
        internal DarkUI.Controls.DarkComboBox cmbChangeSkills;
        internal DarkUI.Controls.DarkLabel DarkLabel37;
        internal DarkUI.Controls.DarkRadioButton optChangeSkillsAdd;
        internal DarkUI.Controls.DarkButton btnChangeSkillsOk;
        internal DarkUI.Controls.DarkButton btnChangeSkillsCancel;
        internal DarkUI.Controls.DarkRadioButton optChangeSkillsRemove;
        internal DarkUI.Controls.DarkGroupBox fraChangeJob;
        internal DarkUI.Controls.DarkComboBox cmbChangeJob;
        internal DarkUI.Controls.DarkLabel DarkLabel38;
        internal DarkUI.Controls.DarkButton btnChangeJobOk;
        internal DarkUI.Controls.DarkButton btnChangeJobCancel;
        internal DarkUI.Controls.DarkGroupBox fraCreateLabel;
        internal DarkUI.Controls.DarkLabel lblLabelName;
        internal DarkUI.Controls.DarkTextBox txtLabelName;
        internal DarkUI.Controls.DarkButton btnCreatelabelOk;
        internal DarkUI.Controls.DarkButton btnCreatelabelCancel;
        internal DarkUI.Controls.DarkGroupBox fraChangePK;
        internal DarkUI.Controls.DarkButton btnChangePkOk;
        internal DarkUI.Controls.DarkButton btnChangePkCancel;
        internal DarkUI.Controls.DarkComboBox cmbSetPK;
        internal DarkUI.Controls.DarkGroupBox fraPlaySound;
        internal DarkUI.Controls.DarkButton btnPlaySoundOk;
        internal DarkUI.Controls.DarkButton btnPlaySoundCancel;
        internal DarkUI.Controls.DarkComboBox cmbPlaySound;
        internal DarkUI.Controls.DarkGroupBox fraShowChatBubble;
        internal DarkUI.Controls.DarkLabel DarkLabel39;
        internal DarkUI.Controls.DarkTextBox txtChatbubbleText;
        internal DarkUI.Controls.DarkLabel DarkLabel40;
        internal DarkUI.Controls.DarkComboBox cmbChatBubbleTarget;
        internal DarkUI.Controls.DarkComboBox cmbChatBubbleTargetType;
        internal DarkUI.Controls.DarkButton btnShowChatBubbleOk;
        internal DarkUI.Controls.DarkButton btnShowChatBubbleCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel41;
        internal DarkUI.Controls.DarkGroupBox fraMapTint;
        internal DarkUI.Controls.DarkButton btnMapTintOk;
        internal DarkUI.Controls.DarkButton btnMapTintCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel42;
        internal DarkUI.Controls.DarkNumericUpDown nudMapTintData3;
        internal DarkUI.Controls.DarkNumericUpDown nudMapTintData2;
        internal DarkUI.Controls.DarkLabel DarkLabel43;
        internal DarkUI.Controls.DarkLabel DarkLabel44;
        internal DarkUI.Controls.DarkNumericUpDown nudMapTintData1;
        internal DarkUI.Controls.DarkNumericUpDown nudMapTintData0;
        internal DarkUI.Controls.DarkLabel DarkLabel45;
        internal DarkUI.Controls.DarkGroupBox fraSetSelfSwitch;
        internal DarkUI.Controls.DarkComboBox cmbSetSelfSwitch;
        internal DarkUI.Controls.DarkLabel DarkLabel46;
        internal DarkUI.Controls.DarkButton btnSelfswitchOk;
        internal DarkUI.Controls.DarkButton btnSelfswitchCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel47;
        internal DarkUI.Controls.DarkComboBox cmbSetSelfSwitchTo;
        internal DarkUI.Controls.DarkGroupBox fraChangeSprite;
        internal PictureBox picChangeSprite;
        internal DarkUI.Controls.DarkButton btnChangeSpriteOk;
        internal DarkUI.Controls.DarkButton btnChangeSpriteCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel48;
        internal DarkUI.Controls.DarkNumericUpDown nudChangeSprite;
        internal DarkUI.Controls.DarkGroupBox fraPlayerVariable;
        internal DarkUI.Controls.DarkComboBox cmbVariable;
        internal DarkUI.Controls.DarkLabel DarkLabel49;
        internal DarkUI.Controls.DarkRadioButton optVariableAction0;
        internal DarkUI.Controls.DarkRadioButton optVariableAction1;
        internal DarkUI.Controls.DarkNumericUpDown nudVariableData1;
        internal DarkUI.Controls.DarkNumericUpDown nudVariableData0;
        internal DarkUI.Controls.DarkRadioButton optVariableAction3;
        internal DarkUI.Controls.DarkNumericUpDown nudVariableData3;
        internal DarkUI.Controls.DarkRadioButton optVariableAction2;
        internal DarkUI.Controls.DarkButton btnPlayerVarOk;
        internal DarkUI.Controls.DarkButton btnPlayerVarCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel51;
        internal DarkUI.Controls.DarkLabel DarkLabel50;
        internal DarkUI.Controls.DarkNumericUpDown nudVariableData4;
        internal DarkUI.Controls.DarkNumericUpDown nudVariableData2;
        internal DarkUI.Controls.DarkGroupBox fraShowChoices;
        internal DarkUI.Controls.DarkLabel DarkLabel52;
        internal DarkUI.Controls.DarkTextBox txtChoicePrompt;
        internal DarkUI.Controls.DarkButton btnShowChoicesOk;
        internal DarkUI.Controls.DarkButton btnShowChoicesCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel56;
        internal DarkUI.Controls.DarkLabel DarkLabel57;
        internal DarkUI.Controls.DarkLabel DarkLabel55;
        internal DarkUI.Controls.DarkLabel DarkLabel54;
        internal DarkUI.Controls.DarkTextBox txtChoices4;
        internal DarkUI.Controls.DarkTextBox txtChoices3;
        internal DarkUI.Controls.DarkTextBox txtChoices2;
        internal DarkUI.Controls.DarkTextBox txtChoices1;
        internal DarkUI.Controls.DarkGroupBox fraGoToLabel;
        internal DarkUI.Controls.DarkTextBox txtGoToLabel;
        internal DarkUI.Controls.DarkLabel DarkLabel60;
        internal DarkUI.Controls.DarkButton btnGoToLabelOk;
        internal DarkUI.Controls.DarkButton btnGoToLabelCancel;
        internal DarkUI.Controls.DarkGroupBox fraPlayAnimation;
        internal DarkUI.Controls.DarkLabel DarkLabel61;
        internal DarkUI.Controls.DarkComboBox cmbPlayAnim;
        internal DarkUI.Controls.DarkLabel DarkLabel62;
        internal DarkUI.Controls.DarkComboBox cmbAnimTargetType;
        internal DarkUI.Controls.DarkNumericUpDown nudPlayAnimTileY;
        internal DarkUI.Controls.DarkNumericUpDown nudPlayAnimTileX;
        internal DarkUI.Controls.DarkComboBox cmbPlayAnimEvent;
        internal DarkUI.Controls.DarkButton btnPlayAnimationOk;
        internal DarkUI.Controls.DarkButton btnPlayAnimationCancel;
        internal DarkUI.Controls.DarkLabel lblPlayAnimY;
        internal DarkUI.Controls.DarkLabel lblPlayAnimX;
        internal DarkUI.Controls.DarkGroupBox fraChangeGender;
        internal DarkUI.Controls.DarkButton btnChangeGenderOk;
        internal DarkUI.Controls.DarkButton btnChangeGenderCancel;
        internal DarkUI.Controls.DarkRadioButton optChangeSexFemale;
        internal DarkUI.Controls.DarkRadioButton optChangeSexMale;
        internal DarkUI.Controls.DarkGroupBox fraChangeLevel;
        internal DarkUI.Controls.DarkButton btnChangeLevelOk;
        internal DarkUI.Controls.DarkButton btnChangeLevelCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel65;
        internal DarkUI.Controls.DarkNumericUpDown nudChangeLevel;
        internal DarkUI.Controls.DarkGroupBox fraOpenShop;
        internal DarkUI.Controls.DarkComboBox cmbOpenShop;
        internal DarkUI.Controls.DarkButton btnOpenShopOk;
        internal DarkUI.Controls.DarkButton btnOpenShopCancel;
        internal DarkUI.Controls.DarkGroupBox fraShowPic;
        internal DarkUI.Controls.DarkLabel DarkLabel67;
        internal DarkUI.Controls.DarkLabel DarkLabel68;
        internal DarkUI.Controls.DarkNumericUpDown nudPicOffsetY;
        internal DarkUI.Controls.DarkNumericUpDown nudPicOffsetX;
        internal DarkUI.Controls.DarkLabel DarkLabel69;
        internal DarkUI.Controls.DarkComboBox cmbPicLoc;
        internal DarkUI.Controls.DarkNumericUpDown nudShowPicture;
        internal PictureBox picShowPic;
        internal DarkUI.Controls.DarkButton btnShowPicOk;
        internal DarkUI.Controls.DarkButton btnShowPicCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel71;
        internal DarkUI.Controls.DarkLabel DarkLabel70;
        internal DarkUI.Controls.DarkGroupBox fraSetWait;
        internal DarkUI.Controls.DarkButton btnSetWaitOk;
        internal DarkUI.Controls.DarkButton btnSetWaitCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel74;
        internal DarkUI.Controls.DarkLabel DarkLabel72;
        internal DarkUI.Controls.DarkLabel DarkLabel73;
        internal DarkUI.Controls.DarkNumericUpDown nudWaitAmount;
        internal DarkUI.Controls.DarkGroupBox fraSetAccess;
        internal DarkUI.Controls.DarkButton btnSetAccessOk;
        internal DarkUI.Controls.DarkButton btnSetAccessCancel;
        internal DarkUI.Controls.DarkComboBox cmbSetAccess;
        internal DarkUI.Controls.DarkGroupBox fraSetWeather;
        internal DarkUI.Controls.DarkLabel DarkLabel75;
        internal DarkUI.Controls.DarkComboBox CmbWeather;
        internal DarkUI.Controls.DarkButton btnSetWeatherOk;
        internal DarkUI.Controls.DarkButton btnSetWeatherCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel76;
        internal DarkUI.Controls.DarkNumericUpDown nudWeatherIntensity;
        internal DarkUI.Controls.DarkGroupBox fraGiveExp;
        internal DarkUI.Controls.DarkLabel DarkLabel77;
        internal DarkUI.Controls.DarkGroupBox fraSpawnNPC;
        internal DarkUI.Controls.DarkComboBox cmbSpawnNPC;
        internal DarkUI.Controls.DarkButton btnGiveExpOk;
        internal DarkUI.Controls.DarkButton btnGiveExpCancel;
        internal DarkUI.Controls.DarkNumericUpDown nudGiveExp;
        internal DarkUI.Controls.DarkButton btnSpawnNPCOk;
        internal DarkUI.Controls.DarkButton btnSpawnNPCancel;
        internal DarkUI.Controls.DarkGroupBox fraCustomScript;
        internal DarkUI.Controls.DarkNumericUpDown nudCustomScript;
        internal DarkUI.Controls.DarkLabel DarkLabel78;
        internal DarkUI.Controls.DarkButton btnCustomScriptCancel;
        internal DarkUI.Controls.DarkButton btnCustomScriptOk;
        internal DarkUI.Controls.DarkGroupBox fraMoveRouteWait;
        internal DarkUI.Controls.DarkButton btnMoveWaitCancel;
        internal DarkUI.Controls.DarkButton btnMoveWaitOk;
        internal DarkUI.Controls.DarkLabel DarkLabel79;
        internal DarkUI.Controls.DarkComboBox cmbMoveWait;
        internal Panel pnlVariableSwitches;
        internal DarkUI.Controls.DarkGroupBox fraLabeling;
        internal ListBox lstSwitches;
        internal ListBox lstVariables;
        internal GroupBox FraRenaming;
        internal Button btnRename_Cancel;
        internal Button btnRename_Ok;
        internal GroupBox fraRandom10;
        internal TextBox txtRename;
        internal Label lblEditing;
        internal Panel pnlGraphicSel;
        internal DarkUI.Controls.DarkComboBox cmbCondition_Time;
        internal DarkUI.Controls.DarkRadioButton optCondition9;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox6;
        internal DarkUI.Controls.DarkCheckBox chkShowName;
        internal DarkUI.Controls.DarkCheckBox chkWalkThrough;
        internal DarkUI.Controls.DarkCheckBox chkDirFix;
        internal DarkUI.Controls.DarkCheckBox chkWalkAnim;
        internal DarkUI.Controls.DarkGroupBox fraGraphicPic;
        internal PictureBox picGraphic;
        internal DarkUI.Controls.DarkGroupBox fraGraphic;
        internal DarkUI.Controls.DarkButton btnGraphicOk;
        internal DarkUI.Controls.DarkButton btnGraphicCancel;
        internal DarkUI.Controls.DarkLabel DarkLabel13;
        internal DarkUI.Controls.DarkNumericUpDown nudGraphic;
        internal DarkUI.Controls.DarkLabel DarkLabel12;
        internal DarkUI.Controls.DarkComboBox cmbGraphic;
        internal DarkUI.Controls.DarkLabel DarkLabel11;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkComboBox cmbPositioning;
        internal DarkUI.Controls.DarkLabel lblRandomLabel36;
        internal DarkUI.Controls.DarkLabel lblRandomLabel25;
        internal DarkUI.Controls.DarkButton btnLabel_Cancel;
        internal DarkUI.Controls.DarkButton btnRenameVariable;
        internal DarkUI.Controls.DarkButton btnRenameSwitch;
        internal DarkUI.Controls.DarkButton btnLabel_Ok;
    }
}