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
            TreeNode treeNode1 = new TreeNode("Show Text");
            TreeNode treeNode2 = new TreeNode("Show Choices");
            TreeNode treeNode3 = new TreeNode("Add Chatbox Text");
            TreeNode treeNode4 = new TreeNode("Show ChatBubble");
            TreeNode treeNode5 = new TreeNode("Messages", new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4 });
            TreeNode treeNode6 = new TreeNode("Set Player Variable");
            TreeNode treeNode7 = new TreeNode("Set Player Switch");
            TreeNode treeNode8 = new TreeNode("Set Self Switch");
            TreeNode treeNode9 = new TreeNode("Event Processing", new TreeNode[] { treeNode6, treeNode7, treeNode8 });
            TreeNode treeNode10 = new TreeNode("Conditional Branch");
            TreeNode treeNode11 = new TreeNode("Stop Event Processing");
            TreeNode treeNode12 = new TreeNode("Label");
            TreeNode treeNode13 = new TreeNode("GoTo Label");
            TreeNode treeNode14 = new TreeNode("Flow Control", new TreeNode[] { treeNode10, treeNode11, treeNode12, treeNode13 });
            TreeNode treeNode15 = new TreeNode("Change Items");
            TreeNode treeNode16 = new TreeNode("Restore HP");
            TreeNode treeNode17 = new TreeNode("Restore MP");
            TreeNode treeNode18 = new TreeNode("Level Up");
            TreeNode treeNode19 = new TreeNode("Change Level");
            TreeNode treeNode20 = new TreeNode("Change Skills");
            TreeNode treeNode21 = new TreeNode("Change Job");
            TreeNode treeNode22 = new TreeNode("Change Sprite");
            TreeNode treeNode23 = new TreeNode("Change Gender");
            TreeNode treeNode24 = new TreeNode("Change PK");
            TreeNode treeNode25 = new TreeNode("Give Experience");
            TreeNode treeNode26 = new TreeNode("Player Options", new TreeNode[] { treeNode15, treeNode16, treeNode17, treeNode18, treeNode19, treeNode20, treeNode21, treeNode22, treeNode23, treeNode24, treeNode25 });
            TreeNode treeNode27 = new TreeNode("Warp Player");
            TreeNode treeNode28 = new TreeNode("Set Move Route");
            TreeNode treeNode29 = new TreeNode("Wait for Route Completion");
            TreeNode treeNode30 = new TreeNode("Force Spawn NPC");
            TreeNode treeNode31 = new TreeNode("Hold Player");
            TreeNode treeNode32 = new TreeNode("Release Player");
            TreeNode treeNode33 = new TreeNode("Movement", new TreeNode[] { treeNode27, treeNode28, treeNode29, treeNode30, treeNode31, treeNode32 });
            TreeNode treeNode34 = new TreeNode("Play Animation");
            TreeNode treeNode35 = new TreeNode("Animation", new TreeNode[] { treeNode34 });
            TreeNode treeNode36 = new TreeNode("Begin Quest");
            TreeNode treeNode37 = new TreeNode("Complete Task");
            TreeNode treeNode38 = new TreeNode("End Quest");
            TreeNode treeNode39 = new TreeNode("Questing", new TreeNode[] { treeNode36, treeNode37, treeNode38 });
            TreeNode treeNode40 = new TreeNode("Set Fog");
            TreeNode treeNode41 = new TreeNode("Set Weather");
            TreeNode treeNode42 = new TreeNode("Set Map Tinting");
            TreeNode treeNode43 = new TreeNode("Map Functions", new TreeNode[] { treeNode40, treeNode41, treeNode42 });
            TreeNode treeNode44 = new TreeNode("Play BGM");
            TreeNode treeNode45 = new TreeNode("Stop BGM");
            TreeNode treeNode46 = new TreeNode("Play Sound");
            TreeNode treeNode47 = new TreeNode("Stop Sounds");
            TreeNode treeNode48 = new TreeNode("Music and Sound", new TreeNode[] { treeNode44, treeNode45, treeNode46, treeNode47 });
            TreeNode treeNode49 = new TreeNode("Wait...");
            TreeNode treeNode50 = new TreeNode("Set Access");
            TreeNode treeNode51 = new TreeNode("Custom Script");
            TreeNode treeNode52 = new TreeNode("Etc...", new TreeNode[] { treeNode49, treeNode50, treeNode51 });
            TreeNode treeNode53 = new TreeNode("Open Bank");
            TreeNode treeNode54 = new TreeNode("Open Shop");
            TreeNode treeNode55 = new TreeNode("Shop and Bank", new TreeNode[] { treeNode53, treeNode54 });
            TreeNode treeNode56 = new TreeNode("Fade In");
            TreeNode treeNode57 = new TreeNode("Fade Out");
            TreeNode treeNode58 = new TreeNode("Flash White");
            TreeNode treeNode59 = new TreeNode("Show Picture");
            TreeNode treeNode60 = new TreeNode("Hide Picture");
            TreeNode treeNode61 = new TreeNode("Cutscene Options", new TreeNode[] { treeNode56, treeNode57, treeNode58, treeNode59, treeNode60 });
            tvCommands = new TreeView();
            fraPageSetUp = new DarkUI.Controls.DarkGroupBox();
            chkGlobal = new DarkUI.Controls.DarkCheckBox();
            btnClearPage = new DarkUI.Controls.DarkButton();
            btnDeletePage = new DarkUI.Controls.DarkButton();
            btnPastePage = new DarkUI.Controls.DarkButton();
            btnCopyPage = new DarkUI.Controls.DarkButton();
            btnNewPage = new DarkUI.Controls.DarkButton();
            txtName = new DarkUI.Controls.DarkTextBox();
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            tabPages = new TabControl();
            TabPage1 = new TabPage();
            pnlTabPage = new Panel();
            DarkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            cmbPositioning = new DarkUI.Controls.DarkComboBox();
            fraGraphicPic = new DarkUI.Controls.DarkGroupBox();
            picGraphic = new PictureBox();
            DarkGroupBox6 = new DarkUI.Controls.DarkGroupBox();
            chkShowName = new DarkUI.Controls.DarkCheckBox();
            chkWalkThrough = new DarkUI.Controls.DarkCheckBox();
            chkDirFix = new DarkUI.Controls.DarkCheckBox();
            chkWalkAnim = new DarkUI.Controls.DarkCheckBox();
            DarkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            cmbTrigger = new DarkUI.Controls.DarkComboBox();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            picGraphicSel = new PictureBox();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            cmbMoveFreq = new DarkUI.Controls.DarkComboBox();
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            cmbMoveSpeed = new DarkUI.Controls.DarkComboBox();
            btnMoveRoute = new DarkUI.Controls.DarkButton();
            cmbMoveType = new DarkUI.Controls.DarkComboBox();
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            cmbSelfSwitchCompare = new DarkUI.Controls.DarkComboBox();
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            cmbSelfSwitch = new DarkUI.Controls.DarkComboBox();
            chkSelfSwitch = new DarkUI.Controls.DarkCheckBox();
            cmbHasItem = new DarkUI.Controls.DarkComboBox();
            chkHasItem = new DarkUI.Controls.DarkCheckBox();
            cmbPlayerSwitchCompare = new DarkUI.Controls.DarkComboBox();
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            cmbPlayerSwitch = new DarkUI.Controls.DarkComboBox();
            chkPlayerSwitch = new DarkUI.Controls.DarkCheckBox();
            nudPlayerVariable = new DarkUI.Controls.DarkNumericUpDown();
            cmbPlayervarCompare = new DarkUI.Controls.DarkComboBox();
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            cmbPlayerVar = new DarkUI.Controls.DarkComboBox();
            chkPlayerVar = new DarkUI.Controls.DarkCheckBox();
            DarkGroupBox8 = new DarkUI.Controls.DarkGroupBox();
            btnClearCommand = new DarkUI.Controls.DarkButton();
            btnDeleteCommand = new DarkUI.Controls.DarkButton();
            btnEditCommand = new DarkUI.Controls.DarkButton();
            btnAddCommand = new DarkUI.Controls.DarkButton();
            fraCommands = new Panel();
            lstCommands = new ListBox();
            fraGraphic = new DarkUI.Controls.DarkGroupBox();
            btnGraphicOk = new DarkUI.Controls.DarkButton();
            btnGraphicCancel = new DarkUI.Controls.DarkButton();
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            nudGraphic = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            cmbGraphic = new DarkUI.Controls.DarkComboBox();
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            btnLabeling = new DarkUI.Controls.DarkButton();
            btnCancel = new DarkUI.Controls.DarkButton();
            btnOk = new DarkUI.Controls.DarkButton();
            fraMoveRoute = new DarkUI.Controls.DarkGroupBox();
            btnMoveRouteOk = new DarkUI.Controls.DarkButton();
            btnMoveRouteCancel = new DarkUI.Controls.DarkButton();
            chkRepeatRoute = new DarkUI.Controls.DarkCheckBox();
            chkIgnoreMove = new DarkUI.Controls.DarkCheckBox();
            DarkGroupBox10 = new DarkUI.Controls.DarkGroupBox();
            lstvwMoveRoute = new DarkUI.Controls.DarkListView();
            lstMoveRoute = new ListBox();
            cmbEvent = new DarkUI.Controls.DarkComboBox();
            ColumnHeader3 = new ColumnHeader();
            ColumnHeader4 = new ColumnHeader();
            pnlGraphicSel = new Panel();
            fraDialogue = new DarkUI.Controls.DarkGroupBox();
            fraShowChatBubble = new DarkUI.Controls.DarkGroupBox();
            btnShowChatBubbleOk = new DarkUI.Controls.DarkButton();
            btnShowChatBubbleCancel = new DarkUI.Controls.DarkButton();
            DarkLabel41 = new DarkUI.Controls.DarkLabel();
            cmbChatBubbleTarget = new DarkUI.Controls.DarkComboBox();
            cmbChatBubbleTargetType = new DarkUI.Controls.DarkComboBox();
            DarkLabel40 = new DarkUI.Controls.DarkLabel();
            txtChatbubbleText = new DarkUI.Controls.DarkTextBox();
            DarkLabel39 = new DarkUI.Controls.DarkLabel();
            fraOpenShop = new DarkUI.Controls.DarkGroupBox();
            btnOpenShopOk = new DarkUI.Controls.DarkButton();
            btnOpenShopCancel = new DarkUI.Controls.DarkButton();
            cmbOpenShop = new DarkUI.Controls.DarkComboBox();
            fraSetSelfSwitch = new DarkUI.Controls.DarkGroupBox();
            btnSelfswitchOk = new DarkUI.Controls.DarkButton();
            btnSelfswitchCancel = new DarkUI.Controls.DarkButton();
            DarkLabel47 = new DarkUI.Controls.DarkLabel();
            cmbSetSelfSwitchTo = new DarkUI.Controls.DarkComboBox();
            DarkLabel46 = new DarkUI.Controls.DarkLabel();
            cmbSetSelfSwitch = new DarkUI.Controls.DarkComboBox();
            fraPlaySound = new DarkUI.Controls.DarkGroupBox();
            btnPlaySoundOk = new DarkUI.Controls.DarkButton();
            btnPlaySoundCancel = new DarkUI.Controls.DarkButton();
            cmbPlaySound = new DarkUI.Controls.DarkComboBox();
            fraChangePK = new DarkUI.Controls.DarkGroupBox();
            btnChangePkOk = new DarkUI.Controls.DarkButton();
            btnChangePkCancel = new DarkUI.Controls.DarkButton();
            cmbSetPK = new DarkUI.Controls.DarkComboBox();
            fraCreateLabel = new DarkUI.Controls.DarkGroupBox();
            btnCreatelabelOk = new DarkUI.Controls.DarkButton();
            btnCreatelabelCancel = new DarkUI.Controls.DarkButton();
            txtLabelName = new DarkUI.Controls.DarkTextBox();
            lblLabelName = new DarkUI.Controls.DarkLabel();
            fraChangeJob = new DarkUI.Controls.DarkGroupBox();
            btnChangeJobOk = new DarkUI.Controls.DarkButton();
            btnChangeJobCancel = new DarkUI.Controls.DarkButton();
            cmbChangeJob = new DarkUI.Controls.DarkComboBox();
            DarkLabel38 = new DarkUI.Controls.DarkLabel();
            fraChangeSkills = new DarkUI.Controls.DarkGroupBox();
            btnChangeSkillsOk = new DarkUI.Controls.DarkButton();
            btnChangeSkillsCancel = new DarkUI.Controls.DarkButton();
            optChangeSkillsRemove = new DarkUI.Controls.DarkRadioButton();
            optChangeSkillsAdd = new DarkUI.Controls.DarkRadioButton();
            cmbChangeSkills = new DarkUI.Controls.DarkComboBox();
            DarkLabel37 = new DarkUI.Controls.DarkLabel();
            fraPlayerSwitch = new DarkUI.Controls.DarkGroupBox();
            btnSetPlayerSwitchOk = new DarkUI.Controls.DarkButton();
            btnSetPlayerswitchCancel = new DarkUI.Controls.DarkButton();
            cmbPlayerSwitchSet = new DarkUI.Controls.DarkComboBox();
            DarkLabel23 = new DarkUI.Controls.DarkLabel();
            cmbSwitch = new DarkUI.Controls.DarkComboBox();
            DarkLabel22 = new DarkUI.Controls.DarkLabel();
            fraSetWait = new DarkUI.Controls.DarkGroupBox();
            btnSetWaitOk = new DarkUI.Controls.DarkButton();
            btnSetWaitCancel = new DarkUI.Controls.DarkButton();
            DarkLabel74 = new DarkUI.Controls.DarkLabel();
            DarkLabel72 = new DarkUI.Controls.DarkLabel();
            DarkLabel73 = new DarkUI.Controls.DarkLabel();
            nudWaitAmount = new DarkUI.Controls.DarkNumericUpDown();
            fraMoveRouteWait = new DarkUI.Controls.DarkGroupBox();
            btnMoveWaitCancel = new DarkUI.Controls.DarkButton();
            btnMoveWaitOk = new DarkUI.Controls.DarkButton();
            DarkLabel79 = new DarkUI.Controls.DarkLabel();
            cmbMoveWait = new DarkUI.Controls.DarkComboBox();
            fraSpawnNPC = new DarkUI.Controls.DarkGroupBox();
            btnSpawnNPCOk = new DarkUI.Controls.DarkButton();
            btnSpawnNPCancel = new DarkUI.Controls.DarkButton();
            cmbSpawnNPC = new DarkUI.Controls.DarkComboBox();
            fraSetWeather = new DarkUI.Controls.DarkGroupBox();
            btnSetWeatherOk = new DarkUI.Controls.DarkButton();
            btnSetWeatherCancel = new DarkUI.Controls.DarkButton();
            DarkLabel76 = new DarkUI.Controls.DarkLabel();
            nudWeatherIntensity = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel75 = new DarkUI.Controls.DarkLabel();
            CmbWeather = new DarkUI.Controls.DarkComboBox();
            fraGiveExp = new DarkUI.Controls.DarkGroupBox();
            btnGiveExpOk = new DarkUI.Controls.DarkButton();
            btnGiveExpCancel = new DarkUI.Controls.DarkButton();
            nudGiveExp = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel77 = new DarkUI.Controls.DarkLabel();
            fraSetAccess = new DarkUI.Controls.DarkGroupBox();
            btnSetAccessOk = new DarkUI.Controls.DarkButton();
            btnSetAccessCancel = new DarkUI.Controls.DarkButton();
            cmbSetAccess = new DarkUI.Controls.DarkComboBox();
            fraChangeGender = new DarkUI.Controls.DarkGroupBox();
            btnChangeGenderOk = new DarkUI.Controls.DarkButton();
            btnChangeGenderCancel = new DarkUI.Controls.DarkButton();
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
            btnShowChoicesCancel = new DarkUI.Controls.DarkButton();
            fraChangeLevel = new DarkUI.Controls.DarkGroupBox();
            btnChangeLevelOk = new DarkUI.Controls.DarkButton();
            btnChangeLevelCancel = new DarkUI.Controls.DarkButton();
            DarkLabel65 = new DarkUI.Controls.DarkLabel();
            nudChangeLevel = new DarkUI.Controls.DarkNumericUpDown();
            fraPlayerVariable = new DarkUI.Controls.DarkGroupBox();
            nudVariableData2 = new DarkUI.Controls.DarkNumericUpDown();
            optVariableAction2 = new DarkUI.Controls.DarkRadioButton();
            btnPlayerVarOk = new DarkUI.Controls.DarkButton();
            btnPlayerVarCancel = new DarkUI.Controls.DarkButton();
            DarkLabel51 = new DarkUI.Controls.DarkLabel();
            DarkLabel50 = new DarkUI.Controls.DarkLabel();
            nudVariableData4 = new DarkUI.Controls.DarkNumericUpDown();
            nudVariableData3 = new DarkUI.Controls.DarkNumericUpDown();
            optVariableAction3 = new DarkUI.Controls.DarkRadioButton();
            optVariableAction1 = new DarkUI.Controls.DarkRadioButton();
            nudVariableData1 = new DarkUI.Controls.DarkNumericUpDown();
            nudVariableData0 = new DarkUI.Controls.DarkNumericUpDown();
            optVariableAction0 = new DarkUI.Controls.DarkRadioButton();
            cmbVariable = new DarkUI.Controls.DarkComboBox();
            DarkLabel49 = new DarkUI.Controls.DarkLabel();
            fraPlayAnimation = new DarkUI.Controls.DarkGroupBox();
            btnPlayAnimationOk = new DarkUI.Controls.DarkButton();
            btnPlayAnimationCancel = new DarkUI.Controls.DarkButton();
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
            btnChangeSpriteCancel = new DarkUI.Controls.DarkButton();
            DarkLabel48 = new DarkUI.Controls.DarkLabel();
            nudChangeSprite = new DarkUI.Controls.DarkNumericUpDown();
            picChangeSprite = new PictureBox();
            fraGoToLabel = new DarkUI.Controls.DarkGroupBox();
            btnGoToLabelOk = new DarkUI.Controls.DarkButton();
            btnGoToLabelCancel = new DarkUI.Controls.DarkButton();
            txtGoToLabel = new DarkUI.Controls.DarkTextBox();
            DarkLabel60 = new DarkUI.Controls.DarkLabel();
            fraMapTint = new DarkUI.Controls.DarkGroupBox();
            btnMapTintOk = new DarkUI.Controls.DarkButton();
            btnMapTintCancel = new DarkUI.Controls.DarkButton();
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
            btnShowPicCancel = new DarkUI.Controls.DarkButton();
            DarkLabel71 = new DarkUI.Controls.DarkLabel();
            DarkLabel70 = new DarkUI.Controls.DarkLabel();
            DarkLabel67 = new DarkUI.Controls.DarkLabel();
            DarkLabel68 = new DarkUI.Controls.DarkLabel();
            nudPicOffsetY = new DarkUI.Controls.DarkNumericUpDown();
            nudPicOffsetX = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel69 = new DarkUI.Controls.DarkLabel();
            cmbPicLoc = new DarkUI.Controls.DarkComboBox();
            nudShowPicture = new DarkUI.Controls.DarkNumericUpDown();
            picShowPic = new PictureBox();
            fraConditionalBranch = new DarkUI.Controls.DarkGroupBox();
            cmbCondition_Time = new DarkUI.Controls.DarkComboBox();
            optCondition9 = new DarkUI.Controls.DarkRadioButton();
            btnConditionalBranchOk = new DarkUI.Controls.DarkButton();
            btnConditionalBranchCancel = new DarkUI.Controls.DarkButton();
            cmbCondition_Gender = new DarkUI.Controls.DarkComboBox();
            optCondition8 = new DarkUI.Controls.DarkRadioButton();
            cmbCondition_SelfSwitchCondition = new DarkUI.Controls.DarkComboBox();
            DarkLabel17 = new DarkUI.Controls.DarkLabel();
            cmbCondition_SelfSwitch = new DarkUI.Controls.DarkComboBox();
            optCondition6 = new DarkUI.Controls.DarkRadioButton();
            nudCondition_LevelAmount = new DarkUI.Controls.DarkNumericUpDown();
            optCondition5 = new DarkUI.Controls.DarkRadioButton();
            cmbCondition_LevelCompare = new DarkUI.Controls.DarkComboBox();
            cmbCondition_LearntSkill = new DarkUI.Controls.DarkComboBox();
            optCondition4 = new DarkUI.Controls.DarkRadioButton();
            cmbCondition_JobIs = new DarkUI.Controls.DarkComboBox();
            optCondition3 = new DarkUI.Controls.DarkRadioButton();
            nudCondition_HasItem = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel16 = new DarkUI.Controls.DarkLabel();
            cmbCondition_HasItem = new DarkUI.Controls.DarkComboBox();
            optCondition2 = new DarkUI.Controls.DarkRadioButton();
            optCondition1 = new DarkUI.Controls.DarkRadioButton();
            DarkLabel15 = new DarkUI.Controls.DarkLabel();
            cmbCondtion_PlayerSwitchCondition = new DarkUI.Controls.DarkComboBox();
            cmbCondition_PlayerSwitch = new DarkUI.Controls.DarkComboBox();
            nudCondition_PlayerVarCondition = new DarkUI.Controls.DarkNumericUpDown();
            cmbCondition_PlayerVarCompare = new DarkUI.Controls.DarkComboBox();
            DarkLabel14 = new DarkUI.Controls.DarkLabel();
            cmbCondition_PlayerVarIndex = new DarkUI.Controls.DarkComboBox();
            optCondition0 = new DarkUI.Controls.DarkRadioButton();
            fraPlayBGM = new DarkUI.Controls.DarkGroupBox();
            btnPlayBgmOk = new DarkUI.Controls.DarkButton();
            btnPlayBgmCancel = new DarkUI.Controls.DarkButton();
            cmbPlayBGM = new DarkUI.Controls.DarkComboBox();
            fraPlayerWarp = new DarkUI.Controls.DarkGroupBox();
            btnPlayerWarpOk = new DarkUI.Controls.DarkButton();
            btnPlayerWarpCancel = new DarkUI.Controls.DarkButton();
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
            btnSetFogCancel = new DarkUI.Controls.DarkButton();
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
            btnShowTextOk = new DarkUI.Controls.DarkButton();
            fraAddText = new DarkUI.Controls.DarkGroupBox();
            btnAddTextOk = new DarkUI.Controls.DarkButton();
            btnAddTextCancel = new DarkUI.Controls.DarkButton();
            optAddText_Global = new DarkUI.Controls.DarkRadioButton();
            optAddText_Map = new DarkUI.Controls.DarkRadioButton();
            optAddText_Player = new DarkUI.Controls.DarkRadioButton();
            DarkLabel25 = new DarkUI.Controls.DarkLabel();
            txtAddText_Text = new DarkUI.Controls.DarkTextBox();
            DarkLabel24 = new DarkUI.Controls.DarkLabel();
            fraChangeItems = new DarkUI.Controls.DarkGroupBox();
            btnChangeItemsOk = new DarkUI.Controls.DarkButton();
            btnChangeItemsCancel = new DarkUI.Controls.DarkButton();
            nudChangeItemsAmount = new DarkUI.Controls.DarkNumericUpDown();
            optChangeItemRemove = new DarkUI.Controls.DarkRadioButton();
            optChangeItemAdd = new DarkUI.Controls.DarkRadioButton();
            optChangeItemSet = new DarkUI.Controls.DarkRadioButton();
            cmbChangeItemIndex = new DarkUI.Controls.DarkComboBox();
            DarkLabel21 = new DarkUI.Controls.DarkLabel();
            pnlVariableSwitches = new Panel();
            FraRenaming = new DarkUI.Controls.DarkGroupBox();
            btnRename_Cancel = new DarkUI.Controls.DarkButton();
            btnRename_Ok = new DarkUI.Controls.DarkButton();
            fraRandom10 = new DarkUI.Controls.DarkGroupBox();
            txtRename = new DarkUI.Controls.DarkTextBox();
            lblEditing = new DarkUI.Controls.DarkLabel();
            fraLabeling = new DarkUI.Controls.DarkGroupBox();
            lstSwitches = new ListBox();
            lstVariables = new ListBox();
            btnLabel_Cancel = new DarkUI.Controls.DarkButton();
            lblRandomLabel36 = new DarkUI.Controls.DarkLabel();
            btnRenameVariable = new DarkUI.Controls.DarkButton();
            lblRandomLabel25 = new DarkUI.Controls.DarkLabel();
            btnRenameSwitch = new DarkUI.Controls.DarkButton();
            btnLabel_Ok = new DarkUI.Controls.DarkButton();
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
            fraCommands.SuspendLayout();
            fraGraphic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudGraphic).BeginInit();
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
            tvCommands.Location = new Point(7, 3);
            tvCommands.Margin = new Padding(4, 3, 4, 3);
            tvCommands.Name = "tvCommands";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Show Text";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Show Choices";
            treeNode3.Name = "Node3";
            treeNode3.Text = "Add Chatbox Text";
            treeNode4.Name = "Node5";
            treeNode4.Text = "Show ChatBubble";
            treeNode5.Name = "NodeMessages";
            treeNode5.Text = "Messages";
            treeNode6.Name = "Node1";
            treeNode6.Text = "Set Player Variable";
            treeNode7.Name = "Node2";
            treeNode7.Text = "Set Player Switch";
            treeNode8.Name = "Node3";
            treeNode8.Text = "Set Self Switch";
            treeNode9.Name = "NodeProcessing";
            treeNode9.Text = "Event Processing";
            treeNode10.Name = "Node1";
            treeNode10.Text = "Conditional Branch";
            treeNode11.Name = "Node2";
            treeNode11.Text = "Stop Event Processing";
            treeNode12.Name = "Node3";
            treeNode12.Text = "Label";
            treeNode13.Name = "Node4";
            treeNode13.Text = "GoTo Label";
            treeNode14.Name = "NodeFlowControl";
            treeNode14.Text = "Flow Control";
            treeNode15.Name = "Node1";
            treeNode15.Text = "Change Items";
            treeNode16.Name = "Node2";
            treeNode16.Text = "Restore HP";
            treeNode17.Name = "Node3";
            treeNode17.Text = "Restore MP";
            treeNode18.Name = "Node4";
            treeNode18.Text = "Level Up";
            treeNode19.Name = "Node5";
            treeNode19.Text = "Change Level";
            treeNode20.Name = "Node6";
            treeNode20.Text = "Change Skills";
            treeNode21.Name = "Node7";
            treeNode21.Text = "Change Job";
            treeNode22.Name = "Node8";
            treeNode22.Text = "Change Sprite";
            treeNode23.Name = "Node9";
            treeNode23.Text = "Change Gender";
            treeNode24.Name = "Node10";
            treeNode24.Text = "Change PK";
            treeNode25.Name = "Node11";
            treeNode25.Text = "Give Experience";
            treeNode26.Name = "NodePlayerOptions";
            treeNode26.Text = "Player Options";
            treeNode27.Name = "Node1";
            treeNode27.Text = "Warp Player";
            treeNode28.Name = "Node2";
            treeNode28.Text = "Set Move Route";
            treeNode29.Name = "Node3";
            treeNode29.Text = "Wait for Route Completion";
            treeNode30.Name = "Node4";
            treeNode30.Text = "Force Spawn NPC";
            treeNode31.Name = "Node5";
            treeNode31.Text = "Hold Player";
            treeNode32.Name = "Node6";
            treeNode32.Text = "Release Player";
            treeNode33.Name = "NodeMovement";
            treeNode33.Text = "Movement";
            treeNode34.Name = "Node1";
            treeNode34.Text = "Play Animation";
            treeNode35.Name = "NodeAnimation";
            treeNode35.Text = "Animation";
            treeNode36.Name = "Node1";
            treeNode36.Text = "Begin Quest";
            treeNode37.Name = "Node2";
            treeNode37.Text = "Complete Task";
            treeNode38.Name = "Node3";
            treeNode38.Text = "End Quest";
            treeNode39.Name = "NodeQuesting";
            treeNode39.Text = "Questing";
            treeNode40.Name = "Node1";
            treeNode40.Text = "Set Fog";
            treeNode41.Name = "Node2";
            treeNode41.Text = "Set Weather";
            treeNode42.Name = "Node3";
            treeNode42.Text = "Set Map Tinting";
            treeNode43.Name = "NodeMapFunctions";
            treeNode43.Text = "Map Functions";
            treeNode44.Name = "Node1";
            treeNode44.Text = "Play BGM";
            treeNode45.Name = "Node2";
            treeNode45.Text = "Stop BGM";
            treeNode46.Name = "Node3";
            treeNode46.Text = "Play Sound";
            treeNode47.Name = "Node4";
            treeNode47.Text = "Stop Sounds";
            treeNode48.Name = "NodeSound";
            treeNode48.Text = "Music and Sound";
            treeNode49.Name = "Node1";
            treeNode49.Text = "Wait...";
            treeNode50.Name = "Node2";
            treeNode50.Text = "Set Access";
            treeNode51.Name = "Node3";
            treeNode51.Text = "Custom Script";
            treeNode52.Name = "NodeEtc";
            treeNode52.Text = "Etc...";
            treeNode53.Name = "Node1";
            treeNode53.Text = "Open Bank";
            treeNode54.Name = "Node2";
            treeNode54.Text = "Open Shop";
            treeNode55.Name = "NodeShopBank";
            treeNode55.Text = "Shop and Bank";
            treeNode56.Name = "Node1";
            treeNode56.Text = "Fade In";
            treeNode57.Name = "Node2";
            treeNode57.Text = "Fade Out";
            treeNode58.Name = "Node12";
            treeNode58.Text = "Flash White";
            treeNode59.Name = "Node13";
            treeNode59.Text = "Show Picture";
            treeNode60.Name = "Node14";
            treeNode60.Text = "Hide Picture";
            treeNode61.Name = "Node0";
            treeNode61.Text = "Cutscene Options";
            tvCommands.Nodes.AddRange(new TreeNode[] { treeNode5, treeNode9, treeNode14, treeNode26, treeNode33, treeNode35, treeNode39, treeNode43, treeNode48, treeNode52, treeNode55, treeNode61 });
            tvCommands.Size = new Size(444, 511);
            tvCommands.TabIndex = 1;
            tvCommands.AfterSelect += TvCommands_AfterSelect;
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
            fraPageSetUp.Location = new Point(4, 3);
            fraPageSetUp.Margin = new Padding(4, 3, 4, 3);
            fraPageSetUp.Name = "fraPageSetUp";
            fraPageSetUp.Padding = new Padding(4, 3, 4, 3);
            fraPageSetUp.Size = new Size(923, 58);
            fraPageSetUp.TabIndex = 2;
            fraPageSetUp.TabStop = false;
            fraPageSetUp.Text = "General";
            // 
            // chkGlobal
            // 
            chkGlobal.AutoSize = true;
            chkGlobal.Location = new Point(327, 23);
            chkGlobal.Margin = new Padding(4, 3, 4, 3);
            chkGlobal.Name = "chkGlobal";
            chkGlobal.Size = new Size(92, 19);
            chkGlobal.TabIndex = 7;
            chkGlobal.Text = "Global Event";
            chkGlobal.CheckedChanged += ChkGlobal_CheckedChanged;
            // 
            // btnClearPage
            // 
            btnClearPage.Location = new Point(825, 18);
            btnClearPage.Margin = new Padding(4, 3, 4, 3);
            btnClearPage.Name = "btnClearPage";
            btnClearPage.Padding = new Padding(6);
            btnClearPage.Size = new Size(88, 27);
            btnClearPage.TabIndex = 6;
            btnClearPage.Text = "Clear Page";
            btnClearPage.Click += BtnClearPage_Click;
            // 
            // btnDeletePage
            // 
            btnDeletePage.Location = new Point(726, 18);
            btnDeletePage.Margin = new Padding(4, 3, 4, 3);
            btnDeletePage.Name = "btnDeletePage";
            btnDeletePage.Padding = new Padding(6);
            btnDeletePage.Size = new Size(92, 27);
            btnDeletePage.TabIndex = 5;
            btnDeletePage.Text = "Delete Page";
            btnDeletePage.Click += BtnDeletePage_Click;
            // 
            // btnPastePage
            // 
            btnPastePage.Location = new Point(631, 18);
            btnPastePage.Margin = new Padding(4, 3, 4, 3);
            btnPastePage.Name = "btnPastePage";
            btnPastePage.Padding = new Padding(6);
            btnPastePage.Size = new Size(88, 27);
            btnPastePage.TabIndex = 4;
            btnPastePage.Text = "Paste Page";
            btnPastePage.Click += BtnPastePage_Click;
            // 
            // btnCopyPage
            // 
            btnCopyPage.Location = new Point(537, 18);
            btnCopyPage.Margin = new Padding(4, 3, 4, 3);
            btnCopyPage.Name = "btnCopyPage";
            btnCopyPage.Padding = new Padding(6);
            btnCopyPage.Size = new Size(88, 27);
            btnCopyPage.TabIndex = 3;
            btnCopyPage.Text = "Copy Page";
            btnCopyPage.Click += BtnCopyPage_Click;
            // 
            // btnNewPage
            // 
            btnNewPage.Location = new Point(442, 18);
            btnNewPage.Margin = new Padding(4, 3, 4, 3);
            btnNewPage.Name = "btnNewPage";
            btnNewPage.Padding = new Padding(6);
            btnNewPage.Size = new Size(88, 27);
            btnNewPage.TabIndex = 2;
            btnNewPage.Text = "New Page";
            btnNewPage.Click += BtnNewPage_Click;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(98, 22);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(221, 23);
            txtName.TabIndex = 1;
            txtName.TextChanged += TxtName_TextChanged;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(10, 24);
            DarkLabel1.Margin = new Padding(4, 0, 4, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(74, 15);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Event Name:";
            // 
            // tabPages
            // 
            tabPages.Controls.Add(TabPage1);
            tabPages.Location = new Point(14, 68);
            tabPages.Margin = new Padding(4, 3, 4, 3);
            tabPages.Name = "tabPages";
            tabPages.SelectedIndex = 0;
            tabPages.Size = new Size(827, 22);
            tabPages.TabIndex = 3;
            tabPages.Click += TabPages_Click;
            // 
            // TabPage1
            // 
            TabPage1.BackColor = Color.DimGray;
            TabPage1.Location = new Point(4, 24);
            TabPage1.Margin = new Padding(4, 3, 4, 3);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(4, 3, 4, 3);
            TabPage1.Size = new Size(819, 0);
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
            pnlTabPage.Controls.Add(fraCommands);
            pnlTabPage.Controls.Add(lstCommands);
            pnlTabPage.Controls.Add(fraGraphic);
            pnlTabPage.Location = new Point(4, 93);
            pnlTabPage.Margin = new Padding(4, 3, 4, 3);
            pnlTabPage.Name = "pnlTabPage";
            pnlTabPage.Size = new Size(923, 573);
            pnlTabPage.TabIndex = 4;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(cmbPositioning);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(214, 440);
            DarkGroupBox2.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox2.Size = new Size(233, 57);
            DarkGroupBox2.TabIndex = 15;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Poisition";
            // 
            // cmbPositioning
            // 
            cmbPositioning.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPositioning.FormattingEnabled = true;
            cmbPositioning.Items.AddRange(new object[] { "Below Characters", "Same as Characters", "Above Characters" });
            cmbPositioning.Location = new Point(8, 22);
            cmbPositioning.Margin = new Padding(4, 3, 4, 3);
            cmbPositioning.Name = "cmbPositioning";
            cmbPositioning.Size = new Size(220, 24);
            cmbPositioning.TabIndex = 1;
            cmbPositioning.SelectedIndexChanged += CmbPositioning_SelectedIndexChanged;
            // 
            // fraGraphicPic
            // 
            fraGraphicPic.BackColor = Color.FromArgb(45, 45, 48);
            fraGraphicPic.BorderColor = Color.FromArgb(90, 90, 90);
            fraGraphicPic.Controls.Add(picGraphic);
            fraGraphicPic.ForeColor = Color.Gainsboro;
            fraGraphicPic.Location = new Point(4, 156);
            fraGraphicPic.Margin = new Padding(4, 3, 4, 3);
            fraGraphicPic.Name = "fraGraphicPic";
            fraGraphicPic.Padding = new Padding(4, 3, 4, 3);
            fraGraphicPic.Size = new Size(202, 268);
            fraGraphicPic.TabIndex = 12;
            fraGraphicPic.TabStop = false;
            fraGraphicPic.Text = "Graphic";
            // 
            // picGraphic
            // 
            picGraphic.BackgroundImageLayout = ImageLayout.None;
            picGraphic.Location = new Point(8, 22);
            picGraphic.Margin = new Padding(4, 3, 4, 3);
            picGraphic.Name = "picGraphic";
            picGraphic.Size = new Size(188, 239);
            picGraphic.TabIndex = 1;
            picGraphic.TabStop = false;
            picGraphic.Click += PicGraphic_Click;
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
            DarkGroupBox6.Location = new Point(4, 430);
            DarkGroupBox6.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox6.Name = "DarkGroupBox6";
            DarkGroupBox6.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox6.Size = new Size(205, 129);
            DarkGroupBox6.TabIndex = 10;
            DarkGroupBox6.TabStop = false;
            DarkGroupBox6.Text = "Options";
            // 
            // chkShowName
            // 
            chkShowName.AutoSize = true;
            chkShowName.Location = new Point(8, 102);
            chkShowName.Margin = new Padding(4, 3, 4, 3);
            chkShowName.Name = "chkShowName";
            chkShowName.Size = new Size(90, 19);
            chkShowName.TabIndex = 3;
            chkShowName.Text = "Show Name";
            chkShowName.CheckedChanged += ChkShowName_CheckedChanged;
            // 
            // chkWalkThrough
            // 
            chkWalkThrough.AutoSize = true;
            chkWalkThrough.Location = new Point(8, 75);
            chkWalkThrough.Margin = new Padding(4, 3, 4, 3);
            chkWalkThrough.Name = "chkWalkThrough";
            chkWalkThrough.Size = new Size(100, 19);
            chkWalkThrough.TabIndex = 2;
            chkWalkThrough.Text = "Walk Through";
            chkWalkThrough.CheckedChanged += ChkWalkThrough_CheckedChanged;
            // 
            // chkDirFix
            // 
            chkDirFix.AutoSize = true;
            chkDirFix.Location = new Point(8, 48);
            chkDirFix.Margin = new Padding(4, 3, 4, 3);
            chkDirFix.Name = "chkDirFix";
            chkDirFix.Size = new Size(105, 19);
            chkDirFix.TabIndex = 1;
            chkDirFix.Text = "Direction Fixed";
            chkDirFix.CheckedChanged += ChkDirFix_CheckedChanged;
            // 
            // chkWalkAnim
            // 
            chkWalkAnim.AutoSize = true;
            chkWalkAnim.Location = new Point(8, 22);
            chkWalkAnim.Margin = new Padding(4, 3, 4, 3);
            chkWalkAnim.Name = "chkWalkAnim";
            chkWalkAnim.Size = new Size(130, 19);
            chkWalkAnim.TabIndex = 0;
            chkWalkAnim.Text = "No Walk Animation";
            chkWalkAnim.CheckedChanged += ChkWalkAnim_CheckedChanged;
            // 
            // DarkGroupBox5
            // 
            DarkGroupBox5.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox5.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox5.Controls.Add(cmbTrigger);
            DarkGroupBox5.ForeColor = Color.Gainsboro;
            DarkGroupBox5.Location = new Point(217, 374);
            DarkGroupBox5.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox5.Name = "DarkGroupBox5";
            DarkGroupBox5.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox5.Size = new Size(233, 57);
            DarkGroupBox5.TabIndex = 4;
            DarkGroupBox5.TabStop = false;
            DarkGroupBox5.Text = "Trigger";
            // 
            // cmbTrigger
            // 
            cmbTrigger.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTrigger.FormattingEnabled = true;
            cmbTrigger.Items.AddRange(new object[] { "Action Button", "Player Touch", "Parallel Process" });
            cmbTrigger.Location = new Point(7, 22);
            cmbTrigger.Margin = new Padding(4, 3, 4, 3);
            cmbTrigger.Name = "cmbTrigger";
            cmbTrigger.Size = new Size(220, 24);
            cmbTrigger.TabIndex = 0;
            cmbTrigger.SelectedIndexChanged += CmbTrigger_SelectedIndexChanged;
            // 
            // DarkGroupBox4
            // 
            DarkGroupBox4.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox4.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox4.Controls.Add(picGraphicSel);
            DarkGroupBox4.ForeColor = Color.Gainsboro;
            DarkGroupBox4.Location = new Point(212, 308);
            DarkGroupBox4.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox4.Size = new Size(233, 55);
            DarkGroupBox4.TabIndex = 3;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Positioning";
            // 
            // picGraphicSel
            // 
            picGraphicSel.BackgroundImageLayout = ImageLayout.None;
            picGraphicSel.Location = new Point(-220, -338);
            picGraphicSel.Margin = new Padding(4, 3, 4, 3);
            picGraphicSel.Name = "picGraphicSel";
            picGraphicSel.Size = new Size(936, 593);
            picGraphicSel.TabIndex = 5;
            picGraphicSel.TabStop = false;
            picGraphicSel.MouseDown += PicGraphicSel_MouseDown;
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
            DarkGroupBox3.Location = new Point(214, 159);
            DarkGroupBox3.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox3.Size = new Size(233, 142);
            DarkGroupBox3.TabIndex = 2;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Movement";
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(7, 115);
            DarkLabel7.Margin = new Padding(4, 0, 4, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(62, 15);
            DarkLabel7.TabIndex = 6;
            DarkLabel7.Text = "Frequency";
            // 
            // cmbMoveFreq
            // 
            cmbMoveFreq.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMoveFreq.FormattingEnabled = true;
            cmbMoveFreq.Items.AddRange(new object[] { "Lowest", "Lower", "Normal", "Higher", "Highest" });
            cmbMoveFreq.Location = new Point(80, 112);
            cmbMoveFreq.Margin = new Padding(4, 3, 4, 3);
            cmbMoveFreq.Name = "cmbMoveFreq";
            cmbMoveFreq.Size = new Size(145, 24);
            cmbMoveFreq.TabIndex = 5;
            cmbMoveFreq.SelectedIndexChanged += CmbMoveFreq_SelectedIndexChanged;
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(7, 84);
            DarkLabel6.Margin = new Padding(4, 0, 4, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(42, 15);
            DarkLabel6.TabIndex = 4;
            DarkLabel6.Text = "Speed:";
            // 
            // cmbMoveSpeed
            // 
            cmbMoveSpeed.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMoveSpeed.FormattingEnabled = true;
            cmbMoveSpeed.Items.AddRange(new object[] { "8x Slower", "4x Slower", "2x Slower", "Normal", "2x Faster", "4x Faster" });
            cmbMoveSpeed.Location = new Point(80, 81);
            cmbMoveSpeed.Margin = new Padding(4, 3, 4, 3);
            cmbMoveSpeed.Name = "cmbMoveSpeed";
            cmbMoveSpeed.Size = new Size(145, 24);
            cmbMoveSpeed.TabIndex = 3;
            cmbMoveSpeed.SelectedIndexChanged += CmbMoveSpeed_SelectedIndexChanged;
            // 
            // btnMoveRoute
            // 
            btnMoveRoute.Location = new Point(139, 47);
            btnMoveRoute.Margin = new Padding(4, 3, 4, 3);
            btnMoveRoute.Name = "btnMoveRoute";
            btnMoveRoute.Padding = new Padding(6);
            btnMoveRoute.Size = new Size(88, 27);
            btnMoveRoute.TabIndex = 2;
            btnMoveRoute.Text = "Move Route";
            btnMoveRoute.Click += BtnMoveRoute_Click;
            // 
            // cmbMoveType
            // 
            cmbMoveType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMoveType.FormattingEnabled = true;
            cmbMoveType.Items.AddRange(new object[] { "Fixed Position", "Random", "Move Route" });
            cmbMoveType.Location = new Point(80, 16);
            cmbMoveType.Margin = new Padding(4, 3, 4, 3);
            cmbMoveType.Name = "cmbMoveType";
            cmbMoveType.Size = new Size(145, 24);
            cmbMoveType.TabIndex = 1;
            cmbMoveType.SelectedIndexChanged += CmbMoveType_SelectedIndexChanged;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(7, 20);
            DarkLabel5.Margin = new Padding(4, 0, 4, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(34, 15);
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
            DarkGroupBox1.Location = new Point(4, 7);
            DarkGroupBox1.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox1.Size = new Size(443, 145);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Conditions";
            // 
            // cmbSelfSwitchCompare
            // 
            cmbSelfSwitchCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSelfSwitchCompare.FormattingEnabled = true;
            cmbSelfSwitchCompare.Items.AddRange(new object[] { "False = 0", "True = 1" });
            cmbSelfSwitchCompare.Location = new Point(260, 113);
            cmbSelfSwitchCompare.Margin = new Padding(4, 3, 4, 3);
            cmbSelfSwitchCompare.Name = "cmbSelfSwitchCompare";
            cmbSelfSwitchCompare.Size = new Size(103, 24);
            cmbSelfSwitchCompare.TabIndex = 14;
            cmbSelfSwitchCompare.SelectedIndexChanged += CmbSelfSwitchCompare_SelectedIndexChanged;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(237, 117);
            DarkLabel4.Margin = new Padding(4, 0, 4, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(15, 15);
            DarkLabel4.TabIndex = 13;
            DarkLabel4.Text = "is";
            // 
            // cmbSelfSwitch
            // 
            cmbSelfSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSelfSwitch.FormattingEnabled = true;
            cmbSelfSwitch.Items.AddRange(new object[] { "None", "1 - A", "2 - B", "3 - C", "4 - D" });
            cmbSelfSwitch.Location = new Point(126, 113);
            cmbSelfSwitch.Margin = new Padding(4, 3, 4, 3);
            cmbSelfSwitch.Name = "cmbSelfSwitch";
            cmbSelfSwitch.Size = new Size(103, 24);
            cmbSelfSwitch.TabIndex = 12;
            cmbSelfSwitch.SelectedIndexChanged += CmbSelfSwitch_SelectedIndexChanged;
            // 
            // chkSelfSwitch
            // 
            chkSelfSwitch.AutoSize = true;
            chkSelfSwitch.Location = new Point(7, 115);
            chkSelfSwitch.Margin = new Padding(4, 3, 4, 3);
            chkSelfSwitch.Name = "chkSelfSwitch";
            chkSelfSwitch.Size = new Size(83, 19);
            chkSelfSwitch.TabIndex = 11;
            chkSelfSwitch.Text = "Self Switch";
            chkSelfSwitch.CheckedChanged += ChkSelfSwitch_CheckedChanged;
            // 
            // cmbHasItem
            // 
            cmbHasItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbHasItem.FormattingEnabled = true;
            cmbHasItem.Location = new Point(126, 82);
            cmbHasItem.Margin = new Padding(4, 3, 4, 3);
            cmbHasItem.Name = "cmbHasItem";
            cmbHasItem.Size = new Size(237, 24);
            cmbHasItem.TabIndex = 10;
            cmbHasItem.SelectedIndexChanged += CmbHasItem_SelectedIndexChanged;
            // 
            // chkHasItem
            // 
            chkHasItem.AutoSize = true;
            chkHasItem.Location = new Point(7, 84);
            chkHasItem.Margin = new Padding(4, 3, 4, 3);
            chkHasItem.Name = "chkHasItem";
            chkHasItem.Size = new Size(108, 19);
            chkHasItem.TabIndex = 9;
            chkHasItem.Text = "Player Has Item";
            chkHasItem.CheckedChanged += ChkHasItem_CheckedChanged;
            // 
            // cmbPlayerSwitchCompare
            // 
            cmbPlayerSwitchCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayerSwitchCompare.FormattingEnabled = true;
            cmbPlayerSwitchCompare.Items.AddRange(new object[] { "False = 0", "True = 1" });
            cmbPlayerSwitchCompare.Location = new Point(260, 51);
            cmbPlayerSwitchCompare.Margin = new Padding(4, 3, 4, 3);
            cmbPlayerSwitchCompare.Name = "cmbPlayerSwitchCompare";
            cmbPlayerSwitchCompare.Size = new Size(103, 24);
            cmbPlayerSwitchCompare.TabIndex = 8;
            cmbPlayerSwitchCompare.SelectedIndexChanged += CmbPlayerSwitchCompare_SelectedIndexChanged;
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(237, 54);
            DarkLabel3.Margin = new Padding(4, 0, 4, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(15, 15);
            DarkLabel3.TabIndex = 7;
            DarkLabel3.Text = "is";
            // 
            // cmbPlayerSwitch
            // 
            cmbPlayerSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayerSwitch.FormattingEnabled = true;
            cmbPlayerSwitch.Location = new Point(126, 51);
            cmbPlayerSwitch.Margin = new Padding(4, 3, 4, 3);
            cmbPlayerSwitch.Name = "cmbPlayerSwitch";
            cmbPlayerSwitch.Size = new Size(103, 24);
            cmbPlayerSwitch.TabIndex = 6;
            cmbPlayerSwitch.SelectedIndexChanged += CmbPlayerSwitch_SelectedIndexChanged;
            // 
            // chkPlayerSwitch
            // 
            chkPlayerSwitch.AutoSize = true;
            chkPlayerSwitch.Location = new Point(7, 53);
            chkPlayerSwitch.Margin = new Padding(4, 3, 4, 3);
            chkPlayerSwitch.Name = "chkPlayerSwitch";
            chkPlayerSwitch.Size = new Size(96, 19);
            chkPlayerSwitch.TabIndex = 5;
            chkPlayerSwitch.Text = "Player Switch";
            chkPlayerSwitch.CheckedChanged += ChkPlayerSwitch_CheckedChanged;
            // 
            // nudPlayerVariable
            // 
            nudPlayerVariable.Location = new Point(371, 21);
            nudPlayerVariable.Margin = new Padding(4, 3, 4, 3);
            nudPlayerVariable.Name = "nudPlayerVariable";
            nudPlayerVariable.Size = new Size(65, 23);
            nudPlayerVariable.TabIndex = 4;
            nudPlayerVariable.ValueChanged += NudPlayerVariable_ValueChanged;
            // 
            // cmbPlayervarCompare
            // 
            cmbPlayervarCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayervarCompare.FormattingEnabled = true;
            cmbPlayervarCompare.Items.AddRange(new object[] { "Equal To", "Great Than Or Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal" });
            cmbPlayervarCompare.Location = new Point(260, 20);
            cmbPlayervarCompare.Margin = new Padding(4, 3, 4, 3);
            cmbPlayervarCompare.Name = "cmbPlayervarCompare";
            cmbPlayervarCompare.Size = new Size(103, 24);
            cmbPlayervarCompare.TabIndex = 3;
            cmbPlayervarCompare.SelectedIndexChanged += CmbPlayervarCompare_SelectedIndexChanged;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(237, 27);
            DarkLabel2.Margin = new Padding(4, 0, 4, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(15, 15);
            DarkLabel2.TabIndex = 2;
            DarkLabel2.Text = "is";
            // 
            // cmbPlayerVar
            // 
            cmbPlayerVar.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayerVar.FormattingEnabled = true;
            cmbPlayerVar.Location = new Point(126, 20);
            cmbPlayerVar.Margin = new Padding(4, 3, 4, 3);
            cmbPlayerVar.Name = "cmbPlayerVar";
            cmbPlayerVar.Size = new Size(103, 24);
            cmbPlayerVar.TabIndex = 1;
            cmbPlayerVar.SelectedIndexChanged += CmbPlayerVar_SelectedIndexChanged;
            // 
            // chkPlayerVar
            // 
            chkPlayerVar.AutoSize = true;
            chkPlayerVar.Location = new Point(7, 22);
            chkPlayerVar.Margin = new Padding(4, 3, 4, 3);
            chkPlayerVar.Name = "chkPlayerVar";
            chkPlayerVar.Size = new Size(102, 19);
            chkPlayerVar.TabIndex = 0;
            chkPlayerVar.Text = "Player Variable";
            chkPlayerVar.CheckedChanged += ChkPlayerVar_CheckedChanged;
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
            DarkGroupBox8.Location = new Point(454, 507);
            DarkGroupBox8.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox8.Name = "DarkGroupBox8";
            DarkGroupBox8.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox8.Size = new Size(458, 57);
            DarkGroupBox8.TabIndex = 9;
            DarkGroupBox8.TabStop = false;
            DarkGroupBox8.Text = "Commands";
            // 
            // btnClearCommand
            // 
            btnClearCommand.Location = new Point(364, 22);
            btnClearCommand.Margin = new Padding(4, 3, 4, 3);
            btnClearCommand.Name = "btnClearCommand";
            btnClearCommand.Padding = new Padding(6);
            btnClearCommand.Size = new Size(88, 27);
            btnClearCommand.TabIndex = 3;
            btnClearCommand.Text = "Clear";
            btnClearCommand.Click += BtnClearCommand_Click;
            // 
            // btnDeleteCommand
            // 
            btnDeleteCommand.Location = new Point(247, 22);
            btnDeleteCommand.Margin = new Padding(4, 3, 4, 3);
            btnDeleteCommand.Name = "btnDeleteCommand";
            btnDeleteCommand.Padding = new Padding(6);
            btnDeleteCommand.Size = new Size(88, 27);
            btnDeleteCommand.TabIndex = 2;
            btnDeleteCommand.Text = "Delete";
            btnDeleteCommand.Click += BtnDeleteComand_Click;
            // 
            // btnEditCommand
            // 
            btnEditCommand.Location = new Point(126, 22);
            btnEditCommand.Margin = new Padding(4, 3, 4, 3);
            btnEditCommand.Name = "btnEditCommand";
            btnEditCommand.Padding = new Padding(6);
            btnEditCommand.Size = new Size(88, 27);
            btnEditCommand.TabIndex = 1;
            btnEditCommand.Text = "Edit";
            btnEditCommand.Click += BtnEditCommand_Click;
            // 
            // btnAddCommand
            // 
            btnAddCommand.Location = new Point(7, 22);
            btnAddCommand.Margin = new Padding(4, 3, 4, 3);
            btnAddCommand.Name = "btnAddCommand";
            btnAddCommand.Padding = new Padding(6);
            btnAddCommand.Size = new Size(88, 27);
            btnAddCommand.TabIndex = 0;
            btnAddCommand.Text = "Add";
            btnAddCommand.Click += BtnAddCommand_Click;
            // 
            // fraCommands
            // 
            fraCommands.Controls.Add(tvCommands);
            fraCommands.Location = new Point(454, 7);
            fraCommands.Margin = new Padding(4, 3, 4, 3);
            fraCommands.Name = "fraCommands";
            fraCommands.Size = new Size(458, 556);
            fraCommands.TabIndex = 6;
            fraCommands.Visible = false;
            // 
            // lstCommands
            // 
            lstCommands.BackColor = Color.FromArgb(45, 45, 48);
            lstCommands.BorderStyle = BorderStyle.FixedSingle;
            lstCommands.ForeColor = Color.Gainsboro;
            lstCommands.FormattingEnabled = true;
            lstCommands.Location = new Point(454, 7);
            lstCommands.Margin = new Padding(4, 3, 4, 3);
            lstCommands.Name = "lstCommands";
            lstCommands.Size = new Size(458, 497);
            lstCommands.TabIndex = 8;
            lstCommands.SelectedIndexChanged += LstCommands_SelectedIndexChanged;
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
            fraGraphic.Location = new Point(454, 6);
            fraGraphic.Margin = new Padding(4, 3, 4, 3);
            fraGraphic.Name = "fraGraphic";
            fraGraphic.Padding = new Padding(4, 3, 4, 3);
            fraGraphic.Size = new Size(458, 557);
            fraGraphic.TabIndex = 14;
            fraGraphic.TabStop = false;
            fraGraphic.Text = "Graphic Selection";
            fraGraphic.Visible = false;
            // 
            // btnGraphicOk
            // 
            btnGraphicOk.Location = new Point(761, 658);
            btnGraphicOk.Margin = new Padding(4, 3, 4, 3);
            btnGraphicOk.Name = "btnGraphicOk";
            btnGraphicOk.Padding = new Padding(6);
            btnGraphicOk.Size = new Size(88, 27);
            btnGraphicOk.TabIndex = 8;
            btnGraphicOk.Text = "Ok";
            // 
            // btnGraphicCancel
            // 
            btnGraphicCancel.Location = new Point(855, 658);
            btnGraphicCancel.Margin = new Padding(4, 3, 4, 3);
            btnGraphicCancel.Name = "btnGraphicCancel";
            btnGraphicCancel.Padding = new Padding(6);
            btnGraphicCancel.Size = new Size(88, 27);
            btnGraphicCancel.TabIndex = 7;
            btnGraphicCancel.Text = "Cancel";
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(12, 659);
            DarkLabel13.Margin = new Padding(4, 0, 4, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(181, 15);
            DarkLabel13.TabIndex = 6;
            DarkLabel13.Text = "Hold Shift to select multiple tiles.";
            // 
            // nudGraphic
            // 
            nudGraphic.Location = new Point(121, 57);
            nudGraphic.Margin = new Padding(4, 3, 4, 3);
            nudGraphic.Name = "nudGraphic";
            nudGraphic.Size = new Size(252, 23);
            nudGraphic.TabIndex = 3;
            nudGraphic.ValueChanged += nudGraphic_ValueChanged;
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(24, 59);
            DarkLabel12.Margin = new Padding(4, 0, 4, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(54, 15);
            DarkLabel12.TabIndex = 2;
            DarkLabel12.Text = "Number:";
            // 
            // cmbGraphic
            // 
            cmbGraphic.DrawMode = DrawMode.OwnerDrawFixed;
            cmbGraphic.FormattingEnabled = true;
            cmbGraphic.Items.AddRange(new object[] { "None", "Character", "Tileset" });
            cmbGraphic.Location = new Point(121, 21);
            cmbGraphic.Margin = new Padding(4, 3, 4, 3);
            cmbGraphic.Name = "cmbGraphic";
            cmbGraphic.Size = new Size(252, 24);
            cmbGraphic.TabIndex = 1;
            cmbGraphic.SelectedIndexChanged += CmbGraphic_SelectedIndexChanged;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(24, 24);
            DarkLabel11.Margin = new Padding(4, 0, 4, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(83, 15);
            DarkLabel11.TabIndex = 0;
            DarkLabel11.Text = "Graphics Type:";
            // 
            // btnLabeling
            // 
            btnLabeling.Location = new Point(7, 658);
            btnLabeling.Margin = new Padding(4, 3, 4, 3);
            btnLabeling.Name = "btnLabeling";
            btnLabeling.Padding = new Padding(6);
            btnLabeling.Size = new Size(205, 27);
            btnLabeling.TabIndex = 6;
            btnLabeling.Text = "Edit Variables/Switches";
            btnLabeling.Click += BtnLabeling_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(828, 663);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(6);
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(734, 663);
            btnOk.Margin = new Padding(4, 3, 4, 3);
            btnOk.Name = "btnOk";
            btnOk.Padding = new Padding(6);
            btnOk.Size = new Size(88, 27);
            btnOk.TabIndex = 8;
            btnOk.Text = "Ok";
            btnOk.Click += BtnOK_Click;
            btnOk.MouseDown += BtnOK_Click;
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
            fraMoveRoute.Location = new Point(933, 14);
            fraMoveRoute.Margin = new Padding(4, 3, 4, 3);
            fraMoveRoute.Name = "fraMoveRoute";
            fraMoveRoute.Padding = new Padding(4, 3, 4, 3);
            fraMoveRoute.Size = new Size(108, 98);
            fraMoveRoute.TabIndex = 0;
            fraMoveRoute.TabStop = false;
            fraMoveRoute.Text = "Move Route";
            fraMoveRoute.Visible = false;
            // 
            // btnMoveRouteOk
            // 
            btnMoveRouteOk.Location = new Point(749, 497);
            btnMoveRouteOk.Margin = new Padding(4, 3, 4, 3);
            btnMoveRouteOk.Name = "btnMoveRouteOk";
            btnMoveRouteOk.Padding = new Padding(6);
            btnMoveRouteOk.Size = new Size(88, 27);
            btnMoveRouteOk.TabIndex = 7;
            btnMoveRouteOk.Text = "Ok";
            btnMoveRouteOk.Click += BtnMoveRouteOk_Click;
            // 
            // btnMoveRouteCancel
            // 
            btnMoveRouteCancel.Location = new Point(844, 497);
            btnMoveRouteCancel.Margin = new Padding(4, 3, 4, 3);
            btnMoveRouteCancel.Name = "btnMoveRouteCancel";
            btnMoveRouteCancel.Padding = new Padding(6);
            btnMoveRouteCancel.Size = new Size(88, 27);
            btnMoveRouteCancel.TabIndex = 6;
            btnMoveRouteCancel.Text = "Cancel";
            btnMoveRouteCancel.Click += BtnMoveRouteCancel_Click;
            // 
            // chkRepeatRoute
            // 
            chkRepeatRoute.AutoSize = true;
            chkRepeatRoute.Location = new Point(7, 524);
            chkRepeatRoute.Margin = new Padding(4, 3, 4, 3);
            chkRepeatRoute.Name = "chkRepeatRoute";
            chkRepeatRoute.Size = new Size(96, 19);
            chkRepeatRoute.TabIndex = 5;
            chkRepeatRoute.Text = "Repeat Route";
            chkRepeatRoute.CheckedChanged += ChkRepeatRoute_CheckedChanged;
            // 
            // chkIgnoreMove
            // 
            chkIgnoreMove.AutoSize = true;
            chkIgnoreMove.Location = new Point(7, 497);
            chkIgnoreMove.Margin = new Padding(4, 3, 4, 3);
            chkIgnoreMove.Name = "chkIgnoreMove";
            chkIgnoreMove.Size = new Size(164, 19);
            chkIgnoreMove.TabIndex = 4;
            chkIgnoreMove.Text = "Ignore if event can't move";
            chkIgnoreMove.CheckedChanged += ChkIgnoreMove_CheckedChanged;
            // 
            // DarkGroupBox10
            // 
            DarkGroupBox10.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox10.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox10.Controls.Add(lstvwMoveRoute);
            DarkGroupBox10.ForeColor = Color.Gainsboro;
            DarkGroupBox10.Location = new Point(237, 12);
            DarkGroupBox10.Margin = new Padding(4, 3, 4, 3);
            DarkGroupBox10.Name = "DarkGroupBox10";
            DarkGroupBox10.Padding = new Padding(4, 3, 4, 3);
            DarkGroupBox10.Size = new Size(694, 479);
            DarkGroupBox10.TabIndex = 3;
            DarkGroupBox10.TabStop = false;
            DarkGroupBox10.Text = "Commands";
            // 
            // lstvwMoveRoute
            // 
            lstvwMoveRoute.BackColor = Color.DimGray;
            lstvwMoveRoute.Dock = DockStyle.Top;
            lstvwMoveRoute.Font = new Font("Microsoft Sans Serif", 8.25F);
            lstvwMoveRoute.ForeColor = Color.Gainsboro;
            lstvwMoveRoute.Location = new Point(4, 19);
            lstvwMoveRoute.Margin = new Padding(4, 3, 4, 3);
            lstvwMoveRoute.Name = "lstvwMoveRoute";
            lstvwMoveRoute.Size = new Size(686, 458);
            lstvwMoveRoute.TabIndex = 5;
            lstvwMoveRoute.Click += LstvwMoveRoute_SelectedIndexChanged;
            // 
            // lstMoveRoute
            // 
            lstMoveRoute.BackColor = Color.FromArgb(45, 45, 48);
            lstMoveRoute.BorderStyle = BorderStyle.FixedSingle;
            lstMoveRoute.ForeColor = Color.Gainsboro;
            lstMoveRoute.FormattingEnabled = true;
            lstMoveRoute.Location = new Point(7, 53);
            lstMoveRoute.Margin = new Padding(4, 3, 4, 3);
            lstMoveRoute.Name = "lstMoveRoute";
            lstMoveRoute.Size = new Size(222, 437);
            lstMoveRoute.TabIndex = 2;
            lstMoveRoute.KeyDown += LstMoveRoute_KeyDown;
            // 
            // cmbEvent
            // 
            cmbEvent.DrawMode = DrawMode.OwnerDrawFixed;
            cmbEvent.FormattingEnabled = true;
            cmbEvent.Location = new Point(7, 22);
            cmbEvent.Margin = new Padding(4, 3, 4, 3);
            cmbEvent.Name = "cmbEvent";
            cmbEvent.Size = new Size(222, 24);
            cmbEvent.TabIndex = 0;
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
            // pnlGraphicSel
            // 
            pnlGraphicSel.AutoScroll = true;
            pnlGraphicSel.Location = new Point(4, 92);
            pnlGraphicSel.Margin = new Padding(4, 3, 4, 3);
            pnlGraphicSel.Name = "pnlGraphicSel";
            pnlGraphicSel.Size = new Size(923, 573);
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
            fraDialogue.Location = new Point(1056, 14);
            fraDialogue.Margin = new Padding(4, 3, 4, 3);
            fraDialogue.Name = "fraDialogue";
            fraDialogue.Padding = new Padding(4, 3, 4, 3);
            fraDialogue.Size = new Size(776, 687);
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
            fraShowChatBubble.Location = new Point(468, 209);
            fraShowChatBubble.Margin = new Padding(4, 3, 4, 3);
            fraShowChatBubble.Name = "fraShowChatBubble";
            fraShowChatBubble.Padding = new Padding(4, 3, 4, 3);
            fraShowChatBubble.Size = new Size(287, 163);
            fraShowChatBubble.TabIndex = 27;
            fraShowChatBubble.TabStop = false;
            fraShowChatBubble.Text = "Show ChatBubble";
            fraShowChatBubble.Visible = false;
            // 
            // btnShowChatBubbleOk
            // 
            btnShowChatBubbleOk.Location = new Point(98, 129);
            btnShowChatBubbleOk.Margin = new Padding(4, 3, 4, 3);
            btnShowChatBubbleOk.Name = "btnShowChatBubbleOk";
            btnShowChatBubbleOk.Padding = new Padding(6);
            btnShowChatBubbleOk.Size = new Size(88, 27);
            btnShowChatBubbleOk.TabIndex = 31;
            btnShowChatBubbleOk.Text = "Ok";
            btnShowChatBubbleOk.Click += BtnShowChatBubbleOK_Click;
            // 
            // btnShowChatBubbleCancel
            // 
            btnShowChatBubbleCancel.Location = new Point(192, 129);
            btnShowChatBubbleCancel.Margin = new Padding(4, 3, 4, 3);
            btnShowChatBubbleCancel.Name = "btnShowChatBubbleCancel";
            btnShowChatBubbleCancel.Padding = new Padding(6);
            btnShowChatBubbleCancel.Size = new Size(88, 27);
            btnShowChatBubbleCancel.TabIndex = 30;
            btnShowChatBubbleCancel.Text = "Cancel";
            btnShowChatBubbleCancel.Click += BtnShowChatBubbleCancel_Click;
            // 
            // DarkLabel41
            // 
            DarkLabel41.AutoSize = true;
            DarkLabel41.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel41.Location = new Point(7, 102);
            DarkLabel41.Margin = new Padding(4, 0, 4, 0);
            DarkLabel41.Name = "DarkLabel41";
            DarkLabel41.Size = new Size(39, 15);
            DarkLabel41.TabIndex = 29;
            DarkLabel41.Text = "Index:";
            // 
            // cmbChatBubbleTarget
            // 
            cmbChatBubbleTarget.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChatBubbleTarget.FormattingEnabled = true;
            cmbChatBubbleTarget.Location = new Point(94, 98);
            cmbChatBubbleTarget.Margin = new Padding(4, 3, 4, 3);
            cmbChatBubbleTarget.Name = "cmbChatBubbleTarget";
            cmbChatBubbleTarget.Size = new Size(185, 24);
            cmbChatBubbleTarget.TabIndex = 28;
            // 
            // cmbChatBubbleTargetType
            // 
            cmbChatBubbleTargetType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChatBubbleTargetType.FormattingEnabled = true;
            cmbChatBubbleTargetType.Items.AddRange(new object[] { "Player", "NPC", "Event" });
            cmbChatBubbleTargetType.Location = new Point(94, 67);
            cmbChatBubbleTargetType.Margin = new Padding(4, 3, 4, 3);
            cmbChatBubbleTargetType.Name = "cmbChatBubbleTargetType";
            cmbChatBubbleTargetType.Size = new Size(185, 24);
            cmbChatBubbleTargetType.TabIndex = 27;
            cmbChatBubbleTargetType.SelectedIndexChanged += CmbChatBubbleTargetType_SelectedIndexChanged;
            // 
            // DarkLabel40
            // 
            DarkLabel40.AutoSize = true;
            DarkLabel40.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel40.Location = new Point(7, 70);
            DarkLabel40.Margin = new Padding(4, 0, 4, 0);
            DarkLabel40.Name = "DarkLabel40";
            DarkLabel40.Size = new Size(69, 15);
            DarkLabel40.TabIndex = 2;
            DarkLabel40.Text = "Target Type:";
            // 
            // txtChatbubbleText
            // 
            txtChatbubbleText.BackColor = Color.FromArgb(69, 73, 74);
            txtChatbubbleText.BorderStyle = BorderStyle.FixedSingle;
            txtChatbubbleText.ForeColor = Color.FromArgb(220, 220, 220);
            txtChatbubbleText.Location = new Point(7, 37);
            txtChatbubbleText.Margin = new Padding(4, 3, 4, 3);
            txtChatbubbleText.Name = "txtChatbubbleText";
            txtChatbubbleText.Size = new Size(273, 23);
            txtChatbubbleText.TabIndex = 1;
            // 
            // DarkLabel39
            // 
            DarkLabel39.AutoSize = true;
            DarkLabel39.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel39.Location = new Point(7, 18);
            DarkLabel39.Margin = new Padding(4, 0, 4, 0);
            DarkLabel39.Name = "DarkLabel39";
            DarkLabel39.Size = new Size(93, 15);
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
            fraOpenShop.Location = new Point(470, 250);
            fraOpenShop.Margin = new Padding(4, 3, 4, 3);
            fraOpenShop.Name = "fraOpenShop";
            fraOpenShop.Padding = new Padding(4, 3, 4, 3);
            fraOpenShop.Size = new Size(287, 91);
            fraOpenShop.TabIndex = 39;
            fraOpenShop.TabStop = false;
            fraOpenShop.Text = "Open Shop";
            fraOpenShop.Visible = false;
            // 
            // btnOpenShopOk
            // 
            btnOpenShopOk.Location = new Point(51, 54);
            btnOpenShopOk.Margin = new Padding(4, 3, 4, 3);
            btnOpenShopOk.Name = "btnOpenShopOk";
            btnOpenShopOk.Padding = new Padding(6);
            btnOpenShopOk.Size = new Size(88, 27);
            btnOpenShopOk.TabIndex = 27;
            btnOpenShopOk.Text = "Ok";
            btnOpenShopOk.Click += BtnOpenShopOK_Click;
            // 
            // btnOpenShopCancel
            // 
            btnOpenShopCancel.Location = new Point(146, 54);
            btnOpenShopCancel.Margin = new Padding(4, 3, 4, 3);
            btnOpenShopCancel.Name = "btnOpenShopCancel";
            btnOpenShopCancel.Padding = new Padding(6);
            btnOpenShopCancel.Size = new Size(88, 27);
            btnOpenShopCancel.TabIndex = 26;
            btnOpenShopCancel.Text = "Cancel";
            btnOpenShopCancel.Click += BtnOpenShopCancel_Click;
            // 
            // cmbOpenShop
            // 
            cmbOpenShop.DrawMode = DrawMode.OwnerDrawFixed;
            cmbOpenShop.FormattingEnabled = true;
            cmbOpenShop.Location = new Point(10, 23);
            cmbOpenShop.Margin = new Padding(4, 3, 4, 3);
            cmbOpenShop.Name = "cmbOpenShop";
            cmbOpenShop.Size = new Size(263, 24);
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
            fraSetSelfSwitch.Location = new Point(468, 208);
            fraSetSelfSwitch.Margin = new Padding(4, 3, 4, 3);
            fraSetSelfSwitch.Name = "fraSetSelfSwitch";
            fraSetSelfSwitch.Padding = new Padding(4, 3, 4, 3);
            fraSetSelfSwitch.Size = new Size(287, 115);
            fraSetSelfSwitch.TabIndex = 29;
            fraSetSelfSwitch.TabStop = false;
            fraSetSelfSwitch.Text = "Self Switches";
            fraSetSelfSwitch.Visible = false;
            // 
            // btnSelfswitchOk
            // 
            btnSelfswitchOk.Location = new Point(98, 84);
            btnSelfswitchOk.Margin = new Padding(4, 3, 4, 3);
            btnSelfswitchOk.Name = "btnSelfswitchOk";
            btnSelfswitchOk.Padding = new Padding(6);
            btnSelfswitchOk.Size = new Size(88, 27);
            btnSelfswitchOk.TabIndex = 27;
            btnSelfswitchOk.Text = "Ok";
            btnSelfswitchOk.Click += BtnSelfswitchOk_Click;
            // 
            // btnSelfswitchCancel
            // 
            btnSelfswitchCancel.Location = new Point(192, 84);
            btnSelfswitchCancel.Margin = new Padding(4, 3, 4, 3);
            btnSelfswitchCancel.Name = "btnSelfswitchCancel";
            btnSelfswitchCancel.Padding = new Padding(6);
            btnSelfswitchCancel.Size = new Size(88, 27);
            btnSelfswitchCancel.TabIndex = 26;
            btnSelfswitchCancel.Text = "Cancel";
            btnSelfswitchCancel.Click += BtnSelfswitchCancel_Click;
            // 
            // DarkLabel47
            // 
            DarkLabel47.AutoSize = true;
            DarkLabel47.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel47.Location = new Point(7, 57);
            DarkLabel47.Margin = new Padding(4, 0, 4, 0);
            DarkLabel47.Name = "DarkLabel47";
            DarkLabel47.Size = new Size(38, 15);
            DarkLabel47.TabIndex = 3;
            DarkLabel47.Text = "Set To";
            // 
            // cmbSetSelfSwitchTo
            // 
            cmbSetSelfSwitchTo.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSetSelfSwitchTo.FormattingEnabled = true;
            cmbSetSelfSwitchTo.Items.AddRange(new object[] { "Off", "On" });
            cmbSetSelfSwitchTo.Location = new Point(84, 53);
            cmbSetSelfSwitchTo.Margin = new Padding(4, 3, 4, 3);
            cmbSetSelfSwitchTo.Name = "cmbSetSelfSwitchTo";
            cmbSetSelfSwitchTo.Size = new Size(195, 24);
            cmbSetSelfSwitchTo.TabIndex = 2;
            // 
            // DarkLabel46
            // 
            DarkLabel46.AutoSize = true;
            DarkLabel46.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel46.Location = new Point(7, 25);
            DarkLabel46.Margin = new Padding(4, 0, 4, 0);
            DarkLabel46.Name = "DarkLabel46";
            DarkLabel46.Size = new Size(67, 15);
            DarkLabel46.TabIndex = 1;
            DarkLabel46.Text = "Self Switch:";
            // 
            // cmbSetSelfSwitch
            // 
            cmbSetSelfSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSetSelfSwitch.FormattingEnabled = true;
            cmbSetSelfSwitch.Location = new Point(84, 22);
            cmbSetSelfSwitch.Margin = new Padding(4, 3, 4, 3);
            cmbSetSelfSwitch.Name = "cmbSetSelfSwitch";
            cmbSetSelfSwitch.Size = new Size(195, 24);
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
            fraPlaySound.Location = new Point(468, 207);
            fraPlaySound.Margin = new Padding(4, 3, 4, 3);
            fraPlaySound.Name = "fraPlaySound";
            fraPlaySound.Padding = new Padding(4, 3, 4, 3);
            fraPlaySound.Size = new Size(287, 88);
            fraPlaySound.TabIndex = 26;
            fraPlaySound.TabStop = false;
            fraPlaySound.Text = "Play Sound";
            fraPlaySound.Visible = false;
            // 
            // btnPlaySoundOk
            // 
            btnPlaySoundOk.Location = new Point(98, 53);
            btnPlaySoundOk.Margin = new Padding(4, 3, 4, 3);
            btnPlaySoundOk.Name = "btnPlaySoundOk";
            btnPlaySoundOk.Padding = new Padding(6);
            btnPlaySoundOk.Size = new Size(88, 27);
            btnPlaySoundOk.TabIndex = 27;
            btnPlaySoundOk.Text = "Ok";
            btnPlaySoundOk.Click += BtnPlaySoundOK_Click;
            // 
            // btnPlaySoundCancel
            // 
            btnPlaySoundCancel.Location = new Point(192, 53);
            btnPlaySoundCancel.Margin = new Padding(4, 3, 4, 3);
            btnPlaySoundCancel.Name = "btnPlaySoundCancel";
            btnPlaySoundCancel.Padding = new Padding(6);
            btnPlaySoundCancel.Size = new Size(88, 27);
            btnPlaySoundCancel.TabIndex = 26;
            btnPlaySoundCancel.Text = "Cancel";
            btnPlaySoundCancel.Click += BtnPlaySoundCancel_Click;
            // 
            // cmbPlaySound
            // 
            cmbPlaySound.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlaySound.FormattingEnabled = true;
            cmbPlaySound.Location = new Point(7, 22);
            cmbPlaySound.Margin = new Padding(4, 3, 4, 3);
            cmbPlaySound.Name = "cmbPlaySound";
            cmbPlaySound.Size = new Size(272, 24);
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
            fraChangePK.Location = new Point(468, 120);
            fraChangePK.Margin = new Padding(4, 3, 4, 3);
            fraChangePK.Name = "fraChangePK";
            fraChangePK.Padding = new Padding(4, 3, 4, 3);
            fraChangePK.Size = new Size(287, 87);
            fraChangePK.TabIndex = 25;
            fraChangePK.TabStop = false;
            fraChangePK.Text = "Set Player PK";
            fraChangePK.Visible = false;
            // 
            // btnChangePkOk
            // 
            btnChangePkOk.Location = new Point(93, 53);
            btnChangePkOk.Margin = new Padding(4, 3, 4, 3);
            btnChangePkOk.Name = "btnChangePkOk";
            btnChangePkOk.Padding = new Padding(6);
            btnChangePkOk.Size = new Size(88, 27);
            btnChangePkOk.TabIndex = 27;
            btnChangePkOk.Text = "Ok";
            btnChangePkOk.Click += BtnChangePkOK_Click;
            // 
            // btnChangePkCancel
            // 
            btnChangePkCancel.Location = new Point(188, 53);
            btnChangePkCancel.Margin = new Padding(4, 3, 4, 3);
            btnChangePkCancel.Name = "btnChangePkCancel";
            btnChangePkCancel.Padding = new Padding(6);
            btnChangePkCancel.Size = new Size(88, 27);
            btnChangePkCancel.TabIndex = 26;
            btnChangePkCancel.Text = "Cancel";
            btnChangePkCancel.Click += BtnChangePkCancel_Click;
            // 
            // cmbSetPK
            // 
            cmbSetPK.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSetPK.FormattingEnabled = true;
            cmbSetPK.Items.AddRange(new object[] { "No", "Yes" });
            cmbSetPK.Location = new Point(12, 22);
            cmbSetPK.Margin = new Padding(4, 3, 4, 3);
            cmbSetPK.Name = "cmbSetPK";
            cmbSetPK.Size = new Size(263, 24);
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
            fraCreateLabel.Location = new Point(468, 152);
            fraCreateLabel.Margin = new Padding(4, 3, 4, 3);
            fraCreateLabel.Name = "fraCreateLabel";
            fraCreateLabel.Padding = new Padding(4, 3, 4, 3);
            fraCreateLabel.Size = new Size(287, 85);
            fraCreateLabel.TabIndex = 24;
            fraCreateLabel.TabStop = false;
            fraCreateLabel.Text = "Create Label";
            fraCreateLabel.Visible = false;
            // 
            // btnCreatelabelOk
            // 
            btnCreatelabelOk.Location = new Point(98, 52);
            btnCreatelabelOk.Margin = new Padding(4, 3, 4, 3);
            btnCreatelabelOk.Name = "btnCreatelabelOk";
            btnCreatelabelOk.Padding = new Padding(6);
            btnCreatelabelOk.Size = new Size(88, 27);
            btnCreatelabelOk.TabIndex = 27;
            btnCreatelabelOk.Text = "Ok";
            btnCreatelabelOk.Click += BtnCreatelabelOk_Click;
            // 
            // btnCreatelabelCancel
            // 
            btnCreatelabelCancel.Location = new Point(192, 52);
            btnCreatelabelCancel.Margin = new Padding(4, 3, 4, 3);
            btnCreatelabelCancel.Name = "btnCreatelabelCancel";
            btnCreatelabelCancel.Padding = new Padding(6);
            btnCreatelabelCancel.Size = new Size(88, 27);
            btnCreatelabelCancel.TabIndex = 26;
            btnCreatelabelCancel.Text = "Cancel";
            btnCreatelabelCancel.Click += BtnCreateLabelCancel_Click;
            // 
            // txtLabelName
            // 
            txtLabelName.BackColor = Color.FromArgb(69, 73, 74);
            txtLabelName.BorderStyle = BorderStyle.FixedSingle;
            txtLabelName.ForeColor = Color.FromArgb(220, 220, 220);
            txtLabelName.Location = new Point(93, 22);
            txtLabelName.Margin = new Padding(4, 3, 4, 3);
            txtLabelName.Name = "txtLabelName";
            txtLabelName.Size = new Size(186, 23);
            txtLabelName.TabIndex = 1;
            // 
            // lblLabelName
            // 
            lblLabelName.AutoSize = true;
            lblLabelName.ForeColor = Color.FromArgb(220, 220, 220);
            lblLabelName.Location = new Point(8, 24);
            lblLabelName.Margin = new Padding(4, 0, 4, 0);
            lblLabelName.Name = "lblLabelName";
            lblLabelName.Size = new Size(73, 15);
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
            fraChangeJob.Location = new Point(468, 126);
            fraChangeJob.Margin = new Padding(4, 3, 4, 3);
            fraChangeJob.Name = "fraChangeJob";
            fraChangeJob.Padding = new Padding(4, 3, 4, 3);
            fraChangeJob.Size = new Size(287, 88);
            fraChangeJob.TabIndex = 23;
            fraChangeJob.TabStop = false;
            fraChangeJob.Text = "Change Player Job";
            fraChangeJob.Visible = false;
            // 
            // btnChangeJobOk
            // 
            btnChangeJobOk.Location = new Point(98, 53);
            btnChangeJobOk.Margin = new Padding(4, 3, 4, 3);
            btnChangeJobOk.Name = "btnChangeJobOk";
            btnChangeJobOk.Padding = new Padding(6);
            btnChangeJobOk.Size = new Size(88, 27);
            btnChangeJobOk.TabIndex = 27;
            btnChangeJobOk.Text = "Ok";
            btnChangeJobOk.Click += BtnChangeJobOK_Click;
            // 
            // btnChangeJobCancel
            // 
            btnChangeJobCancel.Location = new Point(192, 53);
            btnChangeJobCancel.Margin = new Padding(4, 3, 4, 3);
            btnChangeJobCancel.Name = "btnChangeJobCancel";
            btnChangeJobCancel.Padding = new Padding(6);
            btnChangeJobCancel.Size = new Size(88, 27);
            btnChangeJobCancel.TabIndex = 26;
            btnChangeJobCancel.Text = "Cancel";
            btnChangeJobCancel.Click += BtnChangeJobCancel_Click;
            // 
            // cmbChangeJob
            // 
            cmbChangeJob.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChangeJob.FormattingEnabled = true;
            cmbChangeJob.Location = new Point(57, 22);
            cmbChangeJob.Margin = new Padding(4, 3, 4, 3);
            cmbChangeJob.Name = "cmbChangeJob";
            cmbChangeJob.Size = new Size(222, 24);
            cmbChangeJob.TabIndex = 1;
            // 
            // DarkLabel38
            // 
            DarkLabel38.AutoSize = true;
            DarkLabel38.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel38.Location = new Point(9, 25);
            DarkLabel38.Margin = new Padding(4, 0, 4, 0);
            DarkLabel38.Name = "DarkLabel38";
            DarkLabel38.Size = new Size(28, 15);
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
            fraChangeSkills.Location = new Point(468, 125);
            fraChangeSkills.Margin = new Padding(4, 3, 4, 3);
            fraChangeSkills.Name = "fraChangeSkills";
            fraChangeSkills.Padding = new Padding(4, 3, 4, 3);
            fraChangeSkills.Size = new Size(287, 113);
            fraChangeSkills.TabIndex = 22;
            fraChangeSkills.TabStop = false;
            fraChangeSkills.Text = "Change Player Skills";
            fraChangeSkills.Visible = false;
            // 
            // btnChangeSkillsOk
            // 
            btnChangeSkillsOk.Location = new Point(98, 77);
            btnChangeSkillsOk.Margin = new Padding(4, 3, 4, 3);
            btnChangeSkillsOk.Name = "btnChangeSkillsOk";
            btnChangeSkillsOk.Padding = new Padding(6);
            btnChangeSkillsOk.Size = new Size(88, 27);
            btnChangeSkillsOk.TabIndex = 27;
            btnChangeSkillsOk.Text = "Ok";
            btnChangeSkillsOk.Click += BtnChangeSkillsOK_Click;
            // 
            // btnChangeSkillsCancel
            // 
            btnChangeSkillsCancel.Location = new Point(192, 77);
            btnChangeSkillsCancel.Margin = new Padding(4, 3, 4, 3);
            btnChangeSkillsCancel.Name = "btnChangeSkillsCancel";
            btnChangeSkillsCancel.Padding = new Padding(6);
            btnChangeSkillsCancel.Size = new Size(88, 27);
            btnChangeSkillsCancel.TabIndex = 26;
            btnChangeSkillsCancel.Text = "Cancel";
            btnChangeSkillsCancel.Click += BtnChangeSkillsCancel_Click;
            // 
            // optChangeSkillsRemove
            // 
            optChangeSkillsRemove.AutoSize = true;
            optChangeSkillsRemove.Location = new Point(172, 51);
            optChangeSkillsRemove.Margin = new Padding(4, 3, 4, 3);
            optChangeSkillsRemove.Name = "optChangeSkillsRemove";
            optChangeSkillsRemove.Size = new Size(59, 19);
            optChangeSkillsRemove.TabIndex = 3;
            optChangeSkillsRemove.TabStop = true;
            optChangeSkillsRemove.Text = "Forget";
            // 
            // optChangeSkillsAdd
            // 
            optChangeSkillsAdd.AutoSize = true;
            optChangeSkillsAdd.Location = new Point(76, 51);
            optChangeSkillsAdd.Margin = new Padding(4, 3, 4, 3);
            optChangeSkillsAdd.Name = "optChangeSkillsAdd";
            optChangeSkillsAdd.Size = new Size(55, 19);
            optChangeSkillsAdd.TabIndex = 2;
            optChangeSkillsAdd.TabStop = true;
            optChangeSkillsAdd.Text = "Teach";
            // 
            // cmbChangeSkills
            // 
            cmbChangeSkills.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChangeSkills.FormattingEnabled = true;
            cmbChangeSkills.Location = new Point(48, 20);
            cmbChangeSkills.Margin = new Padding(4, 3, 4, 3);
            cmbChangeSkills.Name = "cmbChangeSkills";
            cmbChangeSkills.Size = new Size(230, 24);
            cmbChangeSkills.TabIndex = 1;
            // 
            // DarkLabel37
            // 
            DarkLabel37.AutoSize = true;
            DarkLabel37.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel37.Location = new Point(7, 23);
            DarkLabel37.Margin = new Padding(4, 0, 4, 0);
            DarkLabel37.Name = "DarkLabel37";
            DarkLabel37.Size = new Size(31, 15);
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
            fraPlayerSwitch.Location = new Point(248, 450);
            fraPlayerSwitch.Margin = new Padding(4, 3, 4, 3);
            fraPlayerSwitch.Name = "fraPlayerSwitch";
            fraPlayerSwitch.Padding = new Padding(4, 3, 4, 3);
            fraPlayerSwitch.Size = new Size(212, 115);
            fraPlayerSwitch.TabIndex = 2;
            fraPlayerSwitch.TabStop = false;
            fraPlayerSwitch.Text = "Change Items";
            fraPlayerSwitch.Visible = false;
            // 
            // btnSetPlayerSwitchOk
            // 
            btnSetPlayerSwitchOk.Location = new Point(23, 83);
            btnSetPlayerSwitchOk.Margin = new Padding(4, 3, 4, 3);
            btnSetPlayerSwitchOk.Name = "btnSetPlayerSwitchOk";
            btnSetPlayerSwitchOk.Padding = new Padding(6);
            btnSetPlayerSwitchOk.Size = new Size(88, 27);
            btnSetPlayerSwitchOk.TabIndex = 9;
            btnSetPlayerSwitchOk.Text = "Ok";
            btnSetPlayerSwitchOk.Click += BtnSetPlayerSwitchOk_Click;
            // 
            // btnSetPlayerswitchCancel
            // 
            btnSetPlayerswitchCancel.Location = new Point(118, 83);
            btnSetPlayerswitchCancel.Margin = new Padding(4, 3, 4, 3);
            btnSetPlayerswitchCancel.Name = "btnSetPlayerswitchCancel";
            btnSetPlayerswitchCancel.Padding = new Padding(6);
            btnSetPlayerswitchCancel.Size = new Size(88, 27);
            btnSetPlayerswitchCancel.TabIndex = 8;
            btnSetPlayerswitchCancel.Text = "Cancel";
            btnSetPlayerswitchCancel.Click += BtnSetPlayerswitchCancel_Click;
            // 
            // cmbPlayerSwitchSet
            // 
            cmbPlayerSwitchSet.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayerSwitchSet.FormattingEnabled = true;
            cmbPlayerSwitchSet.Items.AddRange(new object[] { "False", "True" });
            cmbPlayerSwitchSet.Location = new Point(60, 47);
            cmbPlayerSwitchSet.Margin = new Padding(4, 3, 4, 3);
            cmbPlayerSwitchSet.Name = "cmbPlayerSwitchSet";
            cmbPlayerSwitchSet.Size = new Size(145, 24);
            cmbPlayerSwitchSet.TabIndex = 3;
            // 
            // DarkLabel23
            // 
            DarkLabel23.AutoSize = true;
            DarkLabel23.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel23.Location = new Point(7, 53);
            DarkLabel23.Margin = new Padding(4, 0, 4, 0);
            DarkLabel23.Name = "DarkLabel23";
            DarkLabel23.Size = new Size(37, 15);
            DarkLabel23.TabIndex = 2;
            DarkLabel23.Text = "Set to";
            // 
            // cmbSwitch
            // 
            cmbSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSwitch.FormattingEnabled = true;
            cmbSwitch.Location = new Point(60, 15);
            cmbSwitch.Margin = new Padding(4, 3, 4, 3);
            cmbSwitch.Name = "cmbSwitch";
            cmbSwitch.Size = new Size(145, 24);
            cmbSwitch.TabIndex = 1;
            // 
            // DarkLabel22
            // 
            DarkLabel22.AutoSize = true;
            DarkLabel22.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel22.Location = new Point(7, 18);
            DarkLabel22.Margin = new Padding(4, 0, 4, 0);
            DarkLabel22.Name = "DarkLabel22";
            DarkLabel22.Size = new Size(42, 15);
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
            fraSetWait.Location = new Point(468, 305);
            fraSetWait.Margin = new Padding(4, 3, 4, 3);
            fraSetWait.Name = "fraSetWait";
            fraSetWait.Padding = new Padding(4, 3, 4, 3);
            fraSetWait.Size = new Size(289, 103);
            fraSetWait.TabIndex = 41;
            fraSetWait.TabStop = false;
            fraSetWait.Text = "Wait...";
            fraSetWait.Visible = false;
            // 
            // btnSetWaitOk
            // 
            btnSetWaitOk.Location = new Point(58, 67);
            btnSetWaitOk.Margin = new Padding(4, 3, 4, 3);
            btnSetWaitOk.Name = "btnSetWaitOk";
            btnSetWaitOk.Padding = new Padding(6);
            btnSetWaitOk.Size = new Size(88, 27);
            btnSetWaitOk.TabIndex = 37;
            btnSetWaitOk.Text = "Ok";
            btnSetWaitOk.Click += BtnSetWaitOK_Click;
            // 
            // btnSetWaitCancel
            // 
            btnSetWaitCancel.Location = new Point(153, 67);
            btnSetWaitCancel.Margin = new Padding(4, 3, 4, 3);
            btnSetWaitCancel.Name = "btnSetWaitCancel";
            btnSetWaitCancel.Padding = new Padding(6);
            btnSetWaitCancel.Size = new Size(88, 27);
            btnSetWaitCancel.TabIndex = 36;
            btnSetWaitCancel.Text = "Cancel";
            btnSetWaitCancel.Click += BtnSetWaitCancel_Click;
            // 
            // DarkLabel74
            // 
            DarkLabel74.AutoSize = true;
            DarkLabel74.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel74.Location = new Point(82, 48);
            DarkLabel74.Margin = new Padding(4, 0, 4, 0);
            DarkLabel74.Name = "DarkLabel74";
            DarkLabel74.Size = new Size(120, 15);
            DarkLabel74.TabIndex = 35;
            DarkLabel74.Text = "Hint: 1000 Ms = 1 Sec";
            // 
            // DarkLabel72
            // 
            DarkLabel72.AutoSize = true;
            DarkLabel72.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel72.Location = new Point(254, 27);
            DarkLabel72.Margin = new Padding(4, 0, 4, 0);
            DarkLabel72.Name = "DarkLabel72";
            DarkLabel72.Size = new Size(23, 15);
            DarkLabel72.TabIndex = 34;
            DarkLabel72.Text = "Ms";
            // 
            // DarkLabel73
            // 
            DarkLabel73.AutoSize = true;
            DarkLabel73.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel73.Location = new Point(18, 27);
            DarkLabel73.Margin = new Padding(4, 0, 4, 0);
            DarkLabel73.Name = "DarkLabel73";
            DarkLabel73.Size = new Size(31, 15);
            DarkLabel73.TabIndex = 33;
            DarkLabel73.Text = "Wait";
            // 
            // nudWaitAmount
            // 
            nudWaitAmount.Location = new Point(58, 22);
            nudWaitAmount.Margin = new Padding(4, 3, 4, 3);
            nudWaitAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudWaitAmount.Name = "nudWaitAmount";
            nudWaitAmount.Size = new Size(190, 23);
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
            fraMoveRouteWait.Location = new Point(468, 571);
            fraMoveRouteWait.Margin = new Padding(4, 3, 4, 3);
            fraMoveRouteWait.Name = "fraMoveRouteWait";
            fraMoveRouteWait.Padding = new Padding(4, 3, 4, 3);
            fraMoveRouteWait.Size = new Size(289, 87);
            fraMoveRouteWait.TabIndex = 48;
            fraMoveRouteWait.TabStop = false;
            fraMoveRouteWait.Text = "Move Route Wait";
            fraMoveRouteWait.Visible = false;
            // 
            // btnMoveWaitCancel
            // 
            btnMoveWaitCancel.Location = new Point(195, 53);
            btnMoveWaitCancel.Margin = new Padding(4, 3, 4, 3);
            btnMoveWaitCancel.Name = "btnMoveWaitCancel";
            btnMoveWaitCancel.Padding = new Padding(6);
            btnMoveWaitCancel.Size = new Size(88, 27);
            btnMoveWaitCancel.TabIndex = 26;
            btnMoveWaitCancel.Text = "Cancel";
            btnMoveWaitCancel.Click += BtnMoveWaitCancel_Click;
            // 
            // btnMoveWaitOk
            // 
            btnMoveWaitOk.Location = new Point(100, 53);
            btnMoveWaitOk.Margin = new Padding(4, 3, 4, 3);
            btnMoveWaitOk.Name = "btnMoveWaitOk";
            btnMoveWaitOk.Padding = new Padding(6);
            btnMoveWaitOk.Size = new Size(88, 27);
            btnMoveWaitOk.TabIndex = 27;
            btnMoveWaitOk.Text = "Ok";
            btnMoveWaitOk.Click += BtnMoveWaitOK_Click;
            // 
            // DarkLabel79
            // 
            DarkLabel79.AutoSize = true;
            DarkLabel79.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel79.Location = new Point(8, 25);
            DarkLabel79.Margin = new Padding(4, 0, 4, 0);
            DarkLabel79.Name = "DarkLabel79";
            DarkLabel79.Size = new Size(39, 15);
            DarkLabel79.TabIndex = 1;
            DarkLabel79.Text = "Event:";
            // 
            // cmbMoveWait
            // 
            cmbMoveWait.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMoveWait.FormattingEnabled = true;
            cmbMoveWait.Location = new Point(60, 22);
            cmbMoveWait.Margin = new Padding(4, 3, 4, 3);
            cmbMoveWait.Name = "cmbMoveWait";
            cmbMoveWait.Size = new Size(222, 24);
            cmbMoveWait.TabIndex = 0;
            // 
            // fraSpawnNPC
            // 
            fraSpawnNPC.BackColor = Color.FromArgb(45, 45, 48);
            fraSpawnNPC.BorderColor = Color.FromArgb(90, 90, 90);
            fraSpawnNPC.Controls.Add(btnSpawnNPCOk);
            fraSpawnNPC.Controls.Add(btnSpawnNPCancel);
            fraSpawnNPC.Controls.Add(cmbSpawnNPC);
            fraSpawnNPC.ForeColor = Color.Gainsboro;
            fraSpawnNPC.Location = new Point(468, 475);
            fraSpawnNPC.Margin = new Padding(4, 3, 4, 3);
            fraSpawnNPC.Name = "fraSpawnNPC";
            fraSpawnNPC.Padding = new Padding(4, 3, 4, 3);
            fraSpawnNPC.Size = new Size(289, 89);
            fraSpawnNPC.TabIndex = 46;
            fraSpawnNPC.TabStop = false;
            fraSpawnNPC.Text = "Spawn NPC";
            fraSpawnNPC.Visible = false;
            // 
            // btnSpawnNPCOk
            // 
            btnSpawnNPCOk.Location = new Point(54, 54);
            btnSpawnNPCOk.Margin = new Padding(4, 3, 4, 3);
            btnSpawnNPCOk.Name = "btnSpawnNPCOk";
            btnSpawnNPCOk.Padding = new Padding(6);
            btnSpawnNPCOk.Size = new Size(88, 27);
            btnSpawnNPCOk.TabIndex = 27;
            btnSpawnNPCOk.Text = "Ok";
            btnSpawnNPCOk.Click += BtnSpawnNPCOK_Click;
            // 
            // btnSpawnNPCancel
            // 
            btnSpawnNPCancel.Location = new Point(148, 54);
            btnSpawnNPCancel.Margin = new Padding(4, 3, 4, 3);
            btnSpawnNPCancel.Name = "btnSpawnNPCancel";
            btnSpawnNPCancel.Padding = new Padding(6);
            btnSpawnNPCancel.Size = new Size(88, 27);
            btnSpawnNPCancel.TabIndex = 26;
            btnSpawnNPCancel.Text = "Cancel";
            btnSpawnNPCancel.Click += BtnSpawnNPCancel_Click;
            // 
            // cmbSpawnNPC
            // 
            cmbSpawnNPC.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSpawnNPC.FormattingEnabled = true;
            cmbSpawnNPC.Location = new Point(7, 22);
            cmbSpawnNPC.Margin = new Padding(4, 3, 4, 3);
            cmbSpawnNPC.Name = "cmbSpawnNPC";
            cmbSpawnNPC.Size = new Size(272, 24);
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
            fraSetWeather.Location = new Point(468, 406);
            fraSetWeather.Margin = new Padding(4, 3, 4, 3);
            fraSetWeather.Name = "fraSetWeather";
            fraSetWeather.Padding = new Padding(4, 3, 4, 3);
            fraSetWeather.Size = new Size(289, 110);
            fraSetWeather.TabIndex = 44;
            fraSetWeather.TabStop = false;
            fraSetWeather.Text = "Set Weather";
            fraSetWeather.Visible = false;
            // 
            // btnSetWeatherOk
            // 
            btnSetWeatherOk.Location = new Point(54, 76);
            btnSetWeatherOk.Margin = new Padding(4, 3, 4, 3);
            btnSetWeatherOk.Name = "btnSetWeatherOk";
            btnSetWeatherOk.Padding = new Padding(6);
            btnSetWeatherOk.Size = new Size(88, 27);
            btnSetWeatherOk.TabIndex = 34;
            btnSetWeatherOk.Text = "Ok";
            btnSetWeatherOk.Click += BtnSetWeatherOK_Click;
            // 
            // btnSetWeatherCancel
            // 
            btnSetWeatherCancel.Location = new Point(148, 76);
            btnSetWeatherCancel.Margin = new Padding(4, 3, 4, 3);
            btnSetWeatherCancel.Name = "btnSetWeatherCancel";
            btnSetWeatherCancel.Padding = new Padding(6);
            btnSetWeatherCancel.Size = new Size(88, 27);
            btnSetWeatherCancel.TabIndex = 33;
            btnSetWeatherCancel.Text = "Cancel";
            btnSetWeatherCancel.Click += BtnSetWeatherCancel_Click;
            // 
            // DarkLabel76
            // 
            DarkLabel76.AutoSize = true;
            DarkLabel76.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel76.Location = new Point(9, 51);
            DarkLabel76.Margin = new Padding(4, 0, 4, 0);
            DarkLabel76.Name = "DarkLabel76";
            DarkLabel76.Size = new Size(55, 15);
            DarkLabel76.TabIndex = 32;
            DarkLabel76.Text = "Intensity:";
            // 
            // nudWeatherIntensity
            // 
            nudWeatherIntensity.Location = new Point(102, 47);
            nudWeatherIntensity.Margin = new Padding(4, 3, 4, 3);
            nudWeatherIntensity.Name = "nudWeatherIntensity";
            nudWeatherIntensity.Size = new Size(181, 23);
            nudWeatherIntensity.TabIndex = 31;
            // 
            // DarkLabel75
            // 
            DarkLabel75.AutoSize = true;
            DarkLabel75.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel75.Location = new Point(7, 21);
            DarkLabel75.Margin = new Padding(4, 0, 4, 0);
            DarkLabel75.Name = "DarkLabel75";
            DarkLabel75.Size = new Size(78, 15);
            DarkLabel75.TabIndex = 1;
            DarkLabel75.Text = "Weather Type";
            // 
            // CmbWeather
            // 
            CmbWeather.DrawMode = DrawMode.OwnerDrawFixed;
            CmbWeather.FormattingEnabled = true;
            CmbWeather.Items.AddRange(new object[] { "None", "Rain", "Snow", "Hail", "Sand Storm", "Storm" });
            CmbWeather.Location = new Point(100, 17);
            CmbWeather.Margin = new Padding(4, 3, 4, 3);
            CmbWeather.Name = "CmbWeather";
            CmbWeather.Size = new Size(180, 24);
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
            fraGiveExp.Location = new Point(468, 406);
            fraGiveExp.Margin = new Padding(4, 3, 4, 3);
            fraGiveExp.Name = "fraGiveExp";
            fraGiveExp.Padding = new Padding(4, 3, 4, 3);
            fraGiveExp.Size = new Size(289, 84);
            fraGiveExp.TabIndex = 45;
            fraGiveExp.TabStop = false;
            fraGiveExp.Text = "Give Experience";
            fraGiveExp.Visible = false;
            // 
            // btnGiveExpOk
            // 
            btnGiveExpOk.Location = new Point(58, 52);
            btnGiveExpOk.Margin = new Padding(4, 3, 4, 3);
            btnGiveExpOk.Name = "btnGiveExpOk";
            btnGiveExpOk.Padding = new Padding(6);
            btnGiveExpOk.Size = new Size(88, 27);
            btnGiveExpOk.TabIndex = 27;
            btnGiveExpOk.Text = "Ok";
            btnGiveExpOk.Click += BtnGiveExpOK_Click;
            // 
            // btnGiveExpCancel
            // 
            btnGiveExpCancel.Location = new Point(153, 52);
            btnGiveExpCancel.Margin = new Padding(4, 3, 4, 3);
            btnGiveExpCancel.Name = "btnGiveExpCancel";
            btnGiveExpCancel.Padding = new Padding(6);
            btnGiveExpCancel.Size = new Size(88, 27);
            btnGiveExpCancel.TabIndex = 26;
            btnGiveExpCancel.Text = "Cancel";
            btnGiveExpCancel.Click += BtnGiveExpCancel_Click;
            // 
            // nudGiveExp
            // 
            nudGiveExp.Location = new Point(90, 22);
            nudGiveExp.Margin = new Padding(4, 3, 4, 3);
            nudGiveExp.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudGiveExp.Name = "nudGiveExp";
            nudGiveExp.Size = new Size(192, 23);
            nudGiveExp.TabIndex = 20;
            // 
            // DarkLabel77
            // 
            DarkLabel77.AutoSize = true;
            DarkLabel77.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel77.Location = new Point(7, 24);
            DarkLabel77.Margin = new Padding(4, 0, 4, 0);
            DarkLabel77.Name = "DarkLabel77";
            DarkLabel77.Size = new Size(55, 15);
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
            fraSetAccess.Location = new Point(468, 407);
            fraSetAccess.Margin = new Padding(4, 3, 4, 3);
            fraSetAccess.Name = "fraSetAccess";
            fraSetAccess.Padding = new Padding(4, 3, 4, 3);
            fraSetAccess.Size = new Size(289, 92);
            fraSetAccess.TabIndex = 42;
            fraSetAccess.TabStop = false;
            fraSetAccess.Text = "Set Access";
            fraSetAccess.Visible = false;
            // 
            // btnSetAccessOk
            // 
            btnSetAccessOk.Location = new Point(54, 55);
            btnSetAccessOk.Margin = new Padding(4, 3, 4, 3);
            btnSetAccessOk.Name = "btnSetAccessOk";
            btnSetAccessOk.Padding = new Padding(6);
            btnSetAccessOk.Size = new Size(88, 27);
            btnSetAccessOk.TabIndex = 27;
            btnSetAccessOk.Text = "Ok";
            btnSetAccessOk.Click += BtnSetAccessOK_Click;
            // 
            // btnSetAccessCancel
            // 
            btnSetAccessCancel.Location = new Point(148, 55);
            btnSetAccessCancel.Margin = new Padding(4, 3, 4, 3);
            btnSetAccessCancel.Name = "btnSetAccessCancel";
            btnSetAccessCancel.Padding = new Padding(6);
            btnSetAccessCancel.Size = new Size(88, 27);
            btnSetAccessCancel.TabIndex = 26;
            btnSetAccessCancel.Text = "Cancel";
            btnSetAccessCancel.Click += BtnSetAccessCancel_Click;
            // 
            // cmbSetAccess
            // 
            cmbSetAccess.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSetAccess.FormattingEnabled = true;
            cmbSetAccess.Items.AddRange(new object[] { "1: Player", "2: Moderator", "3: Mapper", "4: Developer", "5: Owner" });
            cmbSetAccess.Location = new Point(38, 22);
            cmbSetAccess.Margin = new Padding(4, 3, 4, 3);
            cmbSetAccess.Name = "cmbSetAccess";
            cmbSetAccess.Size = new Size(219, 24);
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
            fraChangeGender.Location = new Point(468, 420);
            fraChangeGender.Margin = new Padding(4, 3, 4, 3);
            fraChangeGender.Name = "fraChangeGender";
            fraChangeGender.Padding = new Padding(4, 3, 4, 3);
            fraChangeGender.Size = new Size(289, 83);
            fraChangeGender.TabIndex = 37;
            fraChangeGender.TabStop = false;
            fraChangeGender.Text = "Change Player Gender";
            fraChangeGender.Visible = false;
            // 
            // btnChangeGenderOk
            // 
            btnChangeGenderOk.Location = new Point(46, 48);
            btnChangeGenderOk.Margin = new Padding(4, 3, 4, 3);
            btnChangeGenderOk.Name = "btnChangeGenderOk";
            btnChangeGenderOk.Padding = new Padding(6);
            btnChangeGenderOk.Size = new Size(88, 27);
            btnChangeGenderOk.TabIndex = 27;
            btnChangeGenderOk.Text = "Ok";
            btnChangeGenderOk.Click += BtnChangeGenderOK_Click;
            // 
            // btnChangeGenderCancel
            // 
            btnChangeGenderCancel.Location = new Point(140, 48);
            btnChangeGenderCancel.Margin = new Padding(4, 3, 4, 3);
            btnChangeGenderCancel.Name = "btnChangeGenderCancel";
            btnChangeGenderCancel.Padding = new Padding(6);
            btnChangeGenderCancel.Size = new Size(88, 27);
            btnChangeGenderCancel.TabIndex = 26;
            btnChangeGenderCancel.Text = "Cancel";
            btnChangeGenderCancel.Click += BtnChangeGenderCancel_Click;
            // 
            // optChangeSexFemale
            // 
            optChangeSexFemale.AutoSize = true;
            optChangeSexFemale.Location = new Point(164, 22);
            optChangeSexFemale.Margin = new Padding(4, 3, 4, 3);
            optChangeSexFemale.Name = "optChangeSexFemale";
            optChangeSexFemale.Size = new Size(63, 19);
            optChangeSexFemale.TabIndex = 1;
            optChangeSexFemale.TabStop = true;
            optChangeSexFemale.Text = "Female";
            // 
            // optChangeSexMale
            // 
            optChangeSexMale.AutoSize = true;
            optChangeSexMale.Location = new Point(61, 22);
            optChangeSexMale.Margin = new Padding(4, 3, 4, 3);
            optChangeSexMale.Name = "optChangeSexMale";
            optChangeSexMale.Size = new Size(51, 19);
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
            fraShowChoices.Location = new Point(468, 119);
            fraShowChoices.Margin = new Padding(4, 3, 4, 3);
            fraShowChoices.Name = "fraShowChoices";
            fraShowChoices.Padding = new Padding(4, 3, 4, 3);
            fraShowChoices.Size = new Size(289, 384);
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
            txtChoices4.Location = new Point(164, 201);
            txtChoices4.Margin = new Padding(4, 3, 4, 3);
            txtChoices4.Name = "txtChoices4";
            txtChoices4.Size = new Size(116, 23);
            txtChoices4.TabIndex = 34;
            // 
            // txtChoices3
            // 
            txtChoices3.BackColor = Color.FromArgb(69, 73, 74);
            txtChoices3.BorderStyle = BorderStyle.FixedSingle;
            txtChoices3.ForeColor = Color.FromArgb(220, 220, 220);
            txtChoices3.Location = new Point(7, 200);
            txtChoices3.Margin = new Padding(4, 3, 4, 3);
            txtChoices3.Name = "txtChoices3";
            txtChoices3.Size = new Size(116, 23);
            txtChoices3.TabIndex = 33;
            // 
            // txtChoices2
            // 
            txtChoices2.BackColor = Color.FromArgb(69, 73, 74);
            txtChoices2.BorderStyle = BorderStyle.FixedSingle;
            txtChoices2.ForeColor = Color.FromArgb(220, 220, 220);
            txtChoices2.Location = new Point(164, 155);
            txtChoices2.Margin = new Padding(4, 3, 4, 3);
            txtChoices2.Name = "txtChoices2";
            txtChoices2.Size = new Size(116, 23);
            txtChoices2.TabIndex = 32;
            // 
            // txtChoices1
            // 
            txtChoices1.BackColor = Color.FromArgb(69, 73, 74);
            txtChoices1.BorderStyle = BorderStyle.FixedSingle;
            txtChoices1.ForeColor = Color.FromArgb(220, 220, 220);
            txtChoices1.Location = new Point(7, 155);
            txtChoices1.Margin = new Padding(4, 3, 4, 3);
            txtChoices1.Name = "txtChoices1";
            txtChoices1.Size = new Size(116, 23);
            txtChoices1.TabIndex = 31;
            // 
            // DarkLabel56
            // 
            DarkLabel56.AutoSize = true;
            DarkLabel56.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel56.Location = new Point(161, 181);
            DarkLabel56.Margin = new Padding(4, 0, 4, 0);
            DarkLabel56.Name = "DarkLabel56";
            DarkLabel56.Size = new Size(53, 15);
            DarkLabel56.TabIndex = 30;
            DarkLabel56.Text = "Choice 4";
            // 
            // DarkLabel57
            // 
            DarkLabel57.AutoSize = true;
            DarkLabel57.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel57.Location = new Point(8, 181);
            DarkLabel57.Margin = new Padding(4, 0, 4, 0);
            DarkLabel57.Name = "DarkLabel57";
            DarkLabel57.Size = new Size(53, 15);
            DarkLabel57.TabIndex = 29;
            DarkLabel57.Text = "Choice 3";
            // 
            // DarkLabel55
            // 
            DarkLabel55.AutoSize = true;
            DarkLabel55.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel55.Location = new Point(161, 136);
            DarkLabel55.Margin = new Padding(4, 0, 4, 0);
            DarkLabel55.Name = "DarkLabel55";
            DarkLabel55.Size = new Size(53, 15);
            DarkLabel55.TabIndex = 28;
            DarkLabel55.Text = "Choice 2";
            // 
            // DarkLabel54
            // 
            DarkLabel54.AutoSize = true;
            DarkLabel54.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel54.Location = new Point(7, 136);
            DarkLabel54.Margin = new Padding(4, 0, 4, 0);
            DarkLabel54.Name = "DarkLabel54";
            DarkLabel54.Size = new Size(53, 15);
            DarkLabel54.TabIndex = 27;
            DarkLabel54.Text = "Choice 1";
            // 
            // DarkLabel52
            // 
            DarkLabel52.AutoSize = true;
            DarkLabel52.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel52.Location = new Point(8, 22);
            DarkLabel52.Margin = new Padding(4, 0, 4, 0);
            DarkLabel52.Name = "DarkLabel52";
            DarkLabel52.Size = new Size(47, 15);
            DarkLabel52.TabIndex = 26;
            DarkLabel52.Text = "Prompt";
            // 
            // txtChoicePrompt
            // 
            txtChoicePrompt.BackColor = Color.FromArgb(69, 73, 74);
            txtChoicePrompt.BorderStyle = BorderStyle.FixedSingle;
            txtChoicePrompt.ForeColor = Color.FromArgb(220, 220, 220);
            txtChoicePrompt.Location = new Point(10, 44);
            txtChoicePrompt.Margin = new Padding(4, 3, 4, 3);
            txtChoicePrompt.Multiline = true;
            txtChoicePrompt.Name = "txtChoicePrompt";
            txtChoicePrompt.Size = new Size(266, 89);
            txtChoicePrompt.TabIndex = 21;
            // 
            // btnShowChoicesOk
            // 
            btnShowChoicesOk.Location = new Point(98, 352);
            btnShowChoicesOk.Margin = new Padding(4, 3, 4, 3);
            btnShowChoicesOk.Name = "btnShowChoicesOk";
            btnShowChoicesOk.Padding = new Padding(6);
            btnShowChoicesOk.Size = new Size(88, 27);
            btnShowChoicesOk.TabIndex = 25;
            btnShowChoicesOk.Text = "Ok";
            btnShowChoicesOk.Click += BtnShowChoicesOk_Click;
            // 
            // btnShowChoicesCancel
            // 
            btnShowChoicesCancel.Location = new Point(192, 352);
            btnShowChoicesCancel.Margin = new Padding(4, 3, 4, 3);
            btnShowChoicesCancel.Name = "btnShowChoicesCancel";
            btnShowChoicesCancel.Padding = new Padding(6);
            btnShowChoicesCancel.Size = new Size(88, 27);
            btnShowChoicesCancel.TabIndex = 24;
            btnShowChoicesCancel.Text = "Cancel";
            btnShowChoicesCancel.Click += BtnShowChoicesCancel_Click;
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
            fraChangeLevel.Location = new Point(468, 338);
            fraChangeLevel.Margin = new Padding(4, 3, 4, 3);
            fraChangeLevel.Name = "fraChangeLevel";
            fraChangeLevel.Padding = new Padding(4, 3, 4, 3);
            fraChangeLevel.Size = new Size(289, 83);
            fraChangeLevel.TabIndex = 38;
            fraChangeLevel.TabStop = false;
            fraChangeLevel.Text = "Change Level";
            fraChangeLevel.Visible = false;
            // 
            // btnChangeLevelOk
            // 
            btnChangeLevelOk.Location = new Point(54, 52);
            btnChangeLevelOk.Margin = new Padding(4, 3, 4, 3);
            btnChangeLevelOk.Name = "btnChangeLevelOk";
            btnChangeLevelOk.Padding = new Padding(6);
            btnChangeLevelOk.Size = new Size(88, 27);
            btnChangeLevelOk.TabIndex = 27;
            btnChangeLevelOk.Text = "Ok";
            btnChangeLevelOk.Click += BtnChangeLevelOK_Click;
            // 
            // btnChangeLevelCancel
            // 
            btnChangeLevelCancel.Location = new Point(148, 52);
            btnChangeLevelCancel.Margin = new Padding(4, 3, 4, 3);
            btnChangeLevelCancel.Name = "btnChangeLevelCancel";
            btnChangeLevelCancel.Padding = new Padding(6);
            btnChangeLevelCancel.Size = new Size(88, 27);
            btnChangeLevelCancel.TabIndex = 26;
            btnChangeLevelCancel.Text = "Cancel";
            btnChangeLevelCancel.Click += BtnChangeLevelCancel_Click;
            // 
            // DarkLabel65
            // 
            DarkLabel65.AutoSize = true;
            DarkLabel65.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel65.Location = new Point(8, 24);
            DarkLabel65.Margin = new Padding(4, 0, 4, 0);
            DarkLabel65.Name = "DarkLabel65";
            DarkLabel65.Size = new Size(37, 15);
            DarkLabel65.TabIndex = 24;
            DarkLabel65.Text = "Level:";
            // 
            // nudChangeLevel
            // 
            nudChangeLevel.Location = new Point(70, 22);
            nudChangeLevel.Margin = new Padding(4, 3, 4, 3);
            nudChangeLevel.Name = "nudChangeLevel";
            nudChangeLevel.Size = new Size(140, 23);
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
            fraPlayerVariable.Location = new Point(468, 325);
            fraPlayerVariable.Margin = new Padding(4, 3, 4, 3);
            fraPlayerVariable.Name = "fraPlayerVariable";
            fraPlayerVariable.Padding = new Padding(4, 3, 4, 3);
            fraPlayerVariable.Size = new Size(287, 178);
            fraPlayerVariable.TabIndex = 31;
            fraPlayerVariable.TabStop = false;
            fraPlayerVariable.Text = "Player Variable";
            fraPlayerVariable.Visible = false;
            // 
            // nudVariableData2
            // 
            nudVariableData2.Location = new Point(140, 83);
            nudVariableData2.Margin = new Padding(4, 3, 4, 3);
            nudVariableData2.Name = "nudVariableData2";
            nudVariableData2.Size = new Size(140, 23);
            nudVariableData2.TabIndex = 29;
            // 
            // optVariableAction2
            // 
            optVariableAction2.AutoSize = true;
            optVariableAction2.Location = new Point(7, 83);
            optVariableAction2.Margin = new Padding(4, 3, 4, 3);
            optVariableAction2.Name = "optVariableAction2";
            optVariableAction2.Size = new Size(69, 19);
            optVariableAction2.TabIndex = 28;
            optVariableAction2.TabStop = true;
            optVariableAction2.Text = "Subtract";
            optVariableAction2.CheckedChanged += OptVariableAction2_CheckedChanged;
            // 
            // btnPlayerVarOk
            // 
            btnPlayerVarOk.Location = new Point(98, 143);
            btnPlayerVarOk.Margin = new Padding(4, 3, 4, 3);
            btnPlayerVarOk.Name = "btnPlayerVarOk";
            btnPlayerVarOk.Padding = new Padding(6);
            btnPlayerVarOk.Size = new Size(88, 27);
            btnPlayerVarOk.TabIndex = 27;
            btnPlayerVarOk.Text = "Ok";
            btnPlayerVarOk.Click += BtnPlayerVarOk_Click;
            // 
            // btnPlayerVarCancel
            // 
            btnPlayerVarCancel.Location = new Point(192, 143);
            btnPlayerVarCancel.Margin = new Padding(4, 3, 4, 3);
            btnPlayerVarCancel.Name = "btnPlayerVarCancel";
            btnPlayerVarCancel.Padding = new Padding(6);
            btnPlayerVarCancel.Size = new Size(88, 27);
            btnPlayerVarCancel.TabIndex = 26;
            btnPlayerVarCancel.Text = "Cancel";
            btnPlayerVarCancel.Click += BtnPlayerVarCancel_Click;
            // 
            // DarkLabel51
            // 
            DarkLabel51.AutoSize = true;
            DarkLabel51.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel51.Location = new Point(88, 115);
            DarkLabel51.Margin = new Padding(4, 0, 4, 0);
            DarkLabel51.Name = "DarkLabel51";
            DarkLabel51.Size = new Size(32, 15);
            DarkLabel51.TabIndex = 16;
            DarkLabel51.Text = "Low:";
            // 
            // DarkLabel50
            // 
            DarkLabel50.AutoSize = true;
            DarkLabel50.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel50.Location = new Point(184, 115);
            DarkLabel50.Margin = new Padding(4, 0, 4, 0);
            DarkLabel50.Name = "DarkLabel50";
            DarkLabel50.Size = new Size(36, 15);
            DarkLabel50.TabIndex = 15;
            DarkLabel50.Text = "High:";
            // 
            // nudVariableData4
            // 
            nudVariableData4.Location = new Point(229, 113);
            nudVariableData4.Margin = new Padding(4, 3, 4, 3);
            nudVariableData4.Name = "nudVariableData4";
            nudVariableData4.Size = new Size(51, 23);
            nudVariableData4.TabIndex = 14;
            // 
            // nudVariableData3
            // 
            nudVariableData3.Location = new Point(130, 113);
            nudVariableData3.Margin = new Padding(4, 3, 4, 3);
            nudVariableData3.Name = "nudVariableData3";
            nudVariableData3.Size = new Size(51, 23);
            nudVariableData3.TabIndex = 13;
            // 
            // optVariableAction3
            // 
            optVariableAction3.AutoSize = true;
            optVariableAction3.Location = new Point(7, 113);
            optVariableAction3.Margin = new Padding(4, 3, 4, 3);
            optVariableAction3.Name = "optVariableAction3";
            optVariableAction3.Size = new Size(70, 19);
            optVariableAction3.TabIndex = 12;
            optVariableAction3.TabStop = true;
            optVariableAction3.Text = "Random";
            optVariableAction3.CheckedChanged += OptVariableAction3_CheckedChanged;
            // 
            // optVariableAction1
            // 
            optVariableAction1.AutoSize = true;
            optVariableAction1.Location = new Point(170, 53);
            optVariableAction1.Margin = new Padding(4, 3, 4, 3);
            optVariableAction1.Name = "optVariableAction1";
            optVariableAction1.Size = new Size(47, 19);
            optVariableAction1.TabIndex = 11;
            optVariableAction1.TabStop = true;
            optVariableAction1.Text = "Add";
            optVariableAction1.CheckedChanged += OptVariableAction1_CheckedChanged;
            // 
            // nudVariableData1
            // 
            nudVariableData1.Location = new Point(229, 53);
            nudVariableData1.Margin = new Padding(4, 3, 4, 3);
            nudVariableData1.Name = "nudVariableData1";
            nudVariableData1.Size = new Size(51, 23);
            nudVariableData1.TabIndex = 10;
            // 
            // nudVariableData0
            // 
            nudVariableData0.Location = new Point(72, 53);
            nudVariableData0.Margin = new Padding(4, 3, 4, 3);
            nudVariableData0.Name = "nudVariableData0";
            nudVariableData0.Size = new Size(51, 23);
            nudVariableData0.TabIndex = 9;
            // 
            // optVariableAction0
            // 
            optVariableAction0.AutoSize = true;
            optVariableAction0.Location = new Point(7, 53);
            optVariableAction0.Margin = new Padding(4, 3, 4, 3);
            optVariableAction0.Name = "optVariableAction0";
            optVariableAction0.Size = new Size(41, 19);
            optVariableAction0.TabIndex = 2;
            optVariableAction0.TabStop = true;
            optVariableAction0.Text = "Set";
            optVariableAction0.CheckedChanged += OptVariableAction0_CheckedChanged;
            // 
            // cmbVariable
            // 
            cmbVariable.DrawMode = DrawMode.OwnerDrawFixed;
            cmbVariable.FormattingEnabled = true;
            cmbVariable.Location = new Point(70, 22);
            cmbVariable.Margin = new Padding(4, 3, 4, 3);
            cmbVariable.Name = "cmbVariable";
            cmbVariable.Size = new Size(208, 24);
            cmbVariable.TabIndex = 1;
            // 
            // DarkLabel49
            // 
            DarkLabel49.AutoSize = true;
            DarkLabel49.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel49.Location = new Point(7, 25);
            DarkLabel49.Margin = new Padding(4, 0, 4, 0);
            DarkLabel49.Name = "DarkLabel49";
            DarkLabel49.Size = new Size(51, 15);
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
            fraPlayAnimation.Location = new Point(468, 297);
            fraPlayAnimation.Margin = new Padding(4, 3, 4, 3);
            fraPlayAnimation.Name = "fraPlayAnimation";
            fraPlayAnimation.Padding = new Padding(4, 3, 4, 3);
            fraPlayAnimation.Size = new Size(289, 187);
            fraPlayAnimation.TabIndex = 36;
            fraPlayAnimation.TabStop = false;
            fraPlayAnimation.Text = "Play Animation";
            fraPlayAnimation.Visible = false;
            // 
            // btnPlayAnimationOk
            // 
            btnPlayAnimationOk.Location = new Point(100, 152);
            btnPlayAnimationOk.Margin = new Padding(4, 3, 4, 3);
            btnPlayAnimationOk.Name = "btnPlayAnimationOk";
            btnPlayAnimationOk.Padding = new Padding(6);
            btnPlayAnimationOk.Size = new Size(88, 27);
            btnPlayAnimationOk.TabIndex = 36;
            btnPlayAnimationOk.Text = "Ok";
            btnPlayAnimationOk.Click += BtnPlayAnimationOK_Click;
            // 
            // btnPlayAnimationCancel
            // 
            btnPlayAnimationCancel.Location = new Point(195, 152);
            btnPlayAnimationCancel.Margin = new Padding(4, 3, 4, 3);
            btnPlayAnimationCancel.Name = "btnPlayAnimationCancel";
            btnPlayAnimationCancel.Padding = new Padding(6);
            btnPlayAnimationCancel.Size = new Size(88, 27);
            btnPlayAnimationCancel.TabIndex = 35;
            btnPlayAnimationCancel.Text = "Cancel";
            btnPlayAnimationCancel.Click += BtnPlayAnimationCancel_Click;
            // 
            // lblPlayAnimY
            // 
            lblPlayAnimY.AutoSize = true;
            lblPlayAnimY.ForeColor = Color.FromArgb(220, 220, 220);
            lblPlayAnimY.Location = new Point(153, 122);
            lblPlayAnimY.Margin = new Padding(4, 0, 4, 0);
            lblPlayAnimY.Name = "lblPlayAnimY";
            lblPlayAnimY.Size = new Size(65, 15);
            lblPlayAnimY.TabIndex = 34;
            lblPlayAnimY.Text = "Map Tile Y:";
            // 
            // lblPlayAnimX
            // 
            lblPlayAnimX.AutoSize = true;
            lblPlayAnimX.ForeColor = Color.FromArgb(220, 220, 220);
            lblPlayAnimX.Location = new Point(7, 122);
            lblPlayAnimX.Margin = new Padding(4, 0, 4, 0);
            lblPlayAnimX.Name = "lblPlayAnimX";
            lblPlayAnimX.Size = new Size(65, 15);
            lblPlayAnimX.TabIndex = 33;
            lblPlayAnimX.Text = "Map Tile X:";
            // 
            // cmbPlayAnimEvent
            // 
            cmbPlayAnimEvent.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayAnimEvent.FormattingEnabled = true;
            cmbPlayAnimEvent.Location = new Point(97, 84);
            cmbPlayAnimEvent.Margin = new Padding(4, 3, 4, 3);
            cmbPlayAnimEvent.Name = "cmbPlayAnimEvent";
            cmbPlayAnimEvent.Size = new Size(185, 24);
            cmbPlayAnimEvent.TabIndex = 32;
            // 
            // DarkLabel62
            // 
            DarkLabel62.AutoSize = true;
            DarkLabel62.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel62.Location = new Point(5, 57);
            DarkLabel62.Margin = new Padding(4, 0, 4, 0);
            DarkLabel62.Name = "DarkLabel62";
            DarkLabel62.Size = new Size(66, 15);
            DarkLabel62.TabIndex = 31;
            DarkLabel62.Text = "Target Type";
            // 
            // cmbAnimTargetType
            // 
            cmbAnimTargetType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnimTargetType.FormattingEnabled = true;
            cmbAnimTargetType.Items.AddRange(new object[] { "Player", "Event", "Tile" });
            cmbAnimTargetType.Location = new Point(97, 53);
            cmbAnimTargetType.Margin = new Padding(4, 3, 4, 3);
            cmbAnimTargetType.Name = "cmbAnimTargetType";
            cmbAnimTargetType.Size = new Size(185, 24);
            cmbAnimTargetType.TabIndex = 30;
            // 
            // nudPlayAnimTileY
            // 
            nudPlayAnimTileY.Location = new Point(231, 120);
            nudPlayAnimTileY.Margin = new Padding(4, 3, 4, 3);
            nudPlayAnimTileY.Name = "nudPlayAnimTileY";
            nudPlayAnimTileY.Size = new Size(51, 23);
            nudPlayAnimTileY.TabIndex = 29;
            // 
            // nudPlayAnimTileX
            // 
            nudPlayAnimTileX.Location = new Point(85, 120);
            nudPlayAnimTileX.Margin = new Padding(4, 3, 4, 3);
            nudPlayAnimTileX.Name = "nudPlayAnimTileX";
            nudPlayAnimTileX.Size = new Size(51, 23);
            nudPlayAnimTileX.TabIndex = 28;
            // 
            // DarkLabel61
            // 
            DarkLabel61.AutoSize = true;
            DarkLabel61.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel61.Location = new Point(7, 25);
            DarkLabel61.Margin = new Padding(4, 0, 4, 0);
            DarkLabel61.Name = "DarkLabel61";
            DarkLabel61.Size = new Size(66, 15);
            DarkLabel61.TabIndex = 1;
            DarkLabel61.Text = "Animation:";
            // 
            // cmbPlayAnim
            // 
            cmbPlayAnim.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayAnim.FormattingEnabled = true;
            cmbPlayAnim.Location = new Point(72, 22);
            cmbPlayAnim.Margin = new Padding(4, 3, 4, 3);
            cmbPlayAnim.Name = "cmbPlayAnim";
            cmbPlayAnim.Size = new Size(209, 24);
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
            fraChangeSprite.Location = new Point(468, 323);
            fraChangeSprite.Margin = new Padding(4, 3, 4, 3);
            fraChangeSprite.Name = "fraChangeSprite";
            fraChangeSprite.Padding = new Padding(4, 3, 4, 3);
            fraChangeSprite.Size = new Size(287, 135);
            fraChangeSprite.TabIndex = 30;
            fraChangeSprite.TabStop = false;
            fraChangeSprite.Text = "Change Sprite";
            fraChangeSprite.Visible = false;
            // 
            // btnChangeSpriteOk
            // 
            btnChangeSpriteOk.Location = new Point(98, 103);
            btnChangeSpriteOk.Margin = new Padding(4, 3, 4, 3);
            btnChangeSpriteOk.Name = "btnChangeSpriteOk";
            btnChangeSpriteOk.Padding = new Padding(6);
            btnChangeSpriteOk.Size = new Size(88, 27);
            btnChangeSpriteOk.TabIndex = 30;
            btnChangeSpriteOk.Text = "Ok";
            btnChangeSpriteOk.Click += BtnChangeSpriteOK_Click;
            // 
            // btnChangeSpriteCancel
            // 
            btnChangeSpriteCancel.Location = new Point(192, 103);
            btnChangeSpriteCancel.Margin = new Padding(4, 3, 4, 3);
            btnChangeSpriteCancel.Name = "btnChangeSpriteCancel";
            btnChangeSpriteCancel.Padding = new Padding(6);
            btnChangeSpriteCancel.Size = new Size(88, 27);
            btnChangeSpriteCancel.TabIndex = 29;
            btnChangeSpriteCancel.Text = "Cancel";
            btnChangeSpriteCancel.Click += BtnChangeSpriteCancel_Click;
            // 
            // DarkLabel48
            // 
            DarkLabel48.AutoSize = true;
            DarkLabel48.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel48.Location = new Point(93, 77);
            DarkLabel48.Margin = new Padding(4, 0, 4, 0);
            DarkLabel48.Name = "DarkLabel48";
            DarkLabel48.Size = new Size(37, 15);
            DarkLabel48.TabIndex = 28;
            DarkLabel48.Text = "Sprite";
            // 
            // nudChangeSprite
            // 
            nudChangeSprite.Location = new Point(140, 73);
            nudChangeSprite.Margin = new Padding(4, 3, 4, 3);
            nudChangeSprite.Name = "nudChangeSprite";
            nudChangeSprite.Size = new Size(140, 23);
            nudChangeSprite.TabIndex = 27;
            // 
            // picChangeSprite
            // 
            picChangeSprite.BackColor = Color.Black;
            picChangeSprite.BackgroundImageLayout = ImageLayout.Zoom;
            picChangeSprite.Location = new Point(7, 22);
            picChangeSprite.Margin = new Padding(4, 3, 4, 3);
            picChangeSprite.Name = "picChangeSprite";
            picChangeSprite.Size = new Size(82, 107);
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
            fraGoToLabel.Location = new Point(468, 294);
            fraGoToLabel.Margin = new Padding(4, 3, 4, 3);
            fraGoToLabel.Name = "fraGoToLabel";
            fraGoToLabel.Padding = new Padding(4, 3, 4, 3);
            fraGoToLabel.Size = new Size(289, 84);
            fraGoToLabel.TabIndex = 35;
            fraGoToLabel.TabStop = false;
            fraGoToLabel.Text = "GoTo Label";
            fraGoToLabel.Visible = false;
            // 
            // btnGoToLabelOk
            // 
            btnGoToLabelOk.Location = new Point(100, 51);
            btnGoToLabelOk.Margin = new Padding(4, 3, 4, 3);
            btnGoToLabelOk.Name = "btnGoToLabelOk";
            btnGoToLabelOk.Padding = new Padding(6);
            btnGoToLabelOk.Size = new Size(88, 27);
            btnGoToLabelOk.TabIndex = 27;
            btnGoToLabelOk.Text = "Ok";
            btnGoToLabelOk.Click += BtnGoToLabelOk_Click;
            // 
            // btnGoToLabelCancel
            // 
            btnGoToLabelCancel.Location = new Point(195, 51);
            btnGoToLabelCancel.Margin = new Padding(4, 3, 4, 3);
            btnGoToLabelCancel.Name = "btnGoToLabelCancel";
            btnGoToLabelCancel.Padding = new Padding(6);
            btnGoToLabelCancel.Size = new Size(88, 27);
            btnGoToLabelCancel.TabIndex = 26;
            btnGoToLabelCancel.Text = "Cancel";
            btnGoToLabelCancel.Click += BtnGoToLabelCancel_Click;
            // 
            // txtGoToLabel
            // 
            txtGoToLabel.BackColor = Color.FromArgb(69, 73, 74);
            txtGoToLabel.BorderStyle = BorderStyle.FixedSingle;
            txtGoToLabel.ForeColor = Color.FromArgb(220, 220, 220);
            txtGoToLabel.Location = new Point(91, 21);
            txtGoToLabel.Margin = new Padding(4, 3, 4, 3);
            txtGoToLabel.Name = "txtGoToLabel";
            txtGoToLabel.Size = new Size(191, 23);
            txtGoToLabel.TabIndex = 1;
            // 
            // DarkLabel60
            // 
            DarkLabel60.AutoSize = true;
            DarkLabel60.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel60.Location = new Point(4, 23);
            DarkLabel60.Margin = new Padding(4, 0, 4, 0);
            DarkLabel60.Name = "DarkLabel60";
            DarkLabel60.Size = new Size(73, 15);
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
            fraMapTint.Location = new Point(468, 209);
            fraMapTint.Margin = new Padding(4, 3, 4, 3);
            fraMapTint.Name = "fraMapTint";
            fraMapTint.Padding = new Padding(4, 3, 4, 3);
            fraMapTint.Size = new Size(287, 167);
            fraMapTint.TabIndex = 28;
            fraMapTint.TabStop = false;
            fraMapTint.Text = "Map Tinting";
            fraMapTint.Visible = false;
            // 
            // btnMapTintOk
            // 
            btnMapTintOk.Location = new Point(98, 133);
            btnMapTintOk.Margin = new Padding(4, 3, 4, 3);
            btnMapTintOk.Name = "btnMapTintOk";
            btnMapTintOk.Padding = new Padding(6);
            btnMapTintOk.Size = new Size(88, 27);
            btnMapTintOk.TabIndex = 45;
            btnMapTintOk.Text = "Ok";
            btnMapTintOk.Click += BtnMapTintOK_Click;
            // 
            // btnMapTintCancel
            // 
            btnMapTintCancel.Location = new Point(192, 133);
            btnMapTintCancel.Margin = new Padding(4, 3, 4, 3);
            btnMapTintCancel.Name = "btnMapTintCancel";
            btnMapTintCancel.Padding = new Padding(6);
            btnMapTintCancel.Size = new Size(88, 27);
            btnMapTintCancel.TabIndex = 44;
            btnMapTintCancel.Text = "Cancel";
            btnMapTintCancel.Click += BtnMapTintCancel_Click;
            // 
            // DarkLabel42
            // 
            DarkLabel42.AutoSize = true;
            DarkLabel42.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel42.Location = new Point(6, 107);
            DarkLabel42.Margin = new Padding(4, 0, 4, 0);
            DarkLabel42.Name = "DarkLabel42";
            DarkLabel42.Size = new Size(51, 15);
            DarkLabel42.TabIndex = 43;
            DarkLabel42.Text = "Opacity:";
            // 
            // nudMapTintData3
            // 
            nudMapTintData3.Location = new Point(111, 103);
            nudMapTintData3.Margin = new Padding(4, 3, 4, 3);
            nudMapTintData3.Name = "nudMapTintData3";
            nudMapTintData3.Size = new Size(168, 23);
            nudMapTintData3.TabIndex = 42;
            // 
            // nudMapTintData2
            // 
            nudMapTintData2.Location = new Point(111, 74);
            nudMapTintData2.Margin = new Padding(4, 3, 4, 3);
            nudMapTintData2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudMapTintData2.Name = "nudMapTintData2";
            nudMapTintData2.Size = new Size(168, 23);
            nudMapTintData2.TabIndex = 41;
            // 
            // DarkLabel43
            // 
            DarkLabel43.AutoSize = true;
            DarkLabel43.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel43.Location = new Point(6, 76);
            DarkLabel43.Margin = new Padding(4, 0, 4, 0);
            DarkLabel43.Name = "DarkLabel43";
            DarkLabel43.Size = new Size(33, 15);
            DarkLabel43.TabIndex = 40;
            DarkLabel43.Text = "Blue:";
            // 
            // DarkLabel44
            // 
            DarkLabel44.AutoSize = true;
            DarkLabel44.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel44.Location = new Point(5, 50);
            DarkLabel44.Margin = new Padding(4, 0, 4, 0);
            DarkLabel44.Name = "DarkLabel44";
            DarkLabel44.Size = new Size(41, 15);
            DarkLabel44.TabIndex = 39;
            DarkLabel44.Text = "Green:";
            // 
            // nudMapTintData1
            // 
            nudMapTintData1.Location = new Point(111, 45);
            nudMapTintData1.Margin = new Padding(4, 3, 4, 3);
            nudMapTintData1.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudMapTintData1.Name = "nudMapTintData1";
            nudMapTintData1.Size = new Size(168, 23);
            nudMapTintData1.TabIndex = 38;
            // 
            // nudMapTintData0
            // 
            nudMapTintData0.Location = new Point(111, 16);
            nudMapTintData0.Margin = new Padding(4, 3, 4, 3);
            nudMapTintData0.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudMapTintData0.Name = "nudMapTintData0";
            nudMapTintData0.Size = new Size(168, 23);
            nudMapTintData0.TabIndex = 37;
            // 
            // DarkLabel45
            // 
            DarkLabel45.AutoSize = true;
            DarkLabel45.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel45.Location = new Point(6, 18);
            DarkLabel45.Margin = new Padding(4, 0, 4, 0);
            DarkLabel45.Name = "DarkLabel45";
            DarkLabel45.Size = new Size(30, 15);
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
            fraShowPic.Margin = new Padding(4, 3, 4, 3);
            fraShowPic.Name = "fraShowPic";
            fraShowPic.Padding = new Padding(4, 3, 4, 3);
            fraShowPic.Size = new Size(775, 686);
            fraShowPic.TabIndex = 40;
            fraShowPic.TabStop = false;
            fraShowPic.Text = "Show Picture";
            fraShowPic.Visible = false;
            // 
            // btnShowPicOk
            // 
            btnShowPicOk.Location = new Point(583, 651);
            btnShowPicOk.Margin = new Padding(4, 3, 4, 3);
            btnShowPicOk.Name = "btnShowPicOk";
            btnShowPicOk.Padding = new Padding(6);
            btnShowPicOk.Size = new Size(88, 27);
            btnShowPicOk.TabIndex = 55;
            btnShowPicOk.Text = "Ok";
            btnShowPicOk.Click += BtnShowPicOK_Click;
            // 
            // btnShowPicCancel
            // 
            btnShowPicCancel.Location = new Point(679, 651);
            btnShowPicCancel.Margin = new Padding(4, 3, 4, 3);
            btnShowPicCancel.Name = "btnShowPicCancel";
            btnShowPicCancel.Padding = new Padding(6);
            btnShowPicCancel.Size = new Size(88, 27);
            btnShowPicCancel.TabIndex = 54;
            btnShowPicCancel.Text = "Cancel";
            btnShowPicCancel.Click += BtnShowPicCancel_Click;
            // 
            // DarkLabel71
            // 
            DarkLabel71.AutoSize = true;
            DarkLabel71.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel71.Location = new Point(287, 27);
            DarkLabel71.Margin = new Padding(4, 0, 4, 0);
            DarkLabel71.Name = "DarkLabel71";
            DarkLabel71.Size = new Size(120, 15);
            DarkLabel71.TabIndex = 53;
            DarkLabel71.Text = "Offset from Location:";
            // 
            // DarkLabel70
            // 
            DarkLabel70.AutoSize = true;
            DarkLabel70.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel70.Location = new Point(130, 65);
            DarkLabel70.Margin = new Padding(4, 0, 4, 0);
            DarkLabel70.Name = "DarkLabel70";
            DarkLabel70.Size = new Size(53, 15);
            DarkLabel70.TabIndex = 52;
            DarkLabel70.Text = "Location";
            // 
            // DarkLabel67
            // 
            DarkLabel67.AutoSize = true;
            DarkLabel67.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel67.Location = new Point(435, 54);
            DarkLabel67.Margin = new Padding(4, 0, 4, 0);
            DarkLabel67.Name = "DarkLabel67";
            DarkLabel67.Size = new Size(17, 15);
            DarkLabel67.TabIndex = 51;
            DarkLabel67.Text = "Y:";
            // 
            // DarkLabel68
            // 
            DarkLabel68.AutoSize = true;
            DarkLabel68.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel68.Location = new Point(287, 56);
            DarkLabel68.Margin = new Padding(4, 0, 4, 0);
            DarkLabel68.Name = "DarkLabel68";
            DarkLabel68.Size = new Size(17, 15);
            DarkLabel68.TabIndex = 50;
            DarkLabel68.Text = "X:";
            // 
            // nudPicOffsetY
            // 
            nudPicOffsetY.Location = new Point(486, 52);
            nudPicOffsetY.Margin = new Padding(4, 3, 4, 3);
            nudPicOffsetY.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudPicOffsetY.Name = "nudPicOffsetY";
            nudPicOffsetY.Size = new Size(66, 23);
            nudPicOffsetY.TabIndex = 49;
            // 
            // nudPicOffsetX
            // 
            nudPicOffsetX.Location = new Point(336, 52);
            nudPicOffsetX.Margin = new Padding(4, 3, 4, 3);
            nudPicOffsetX.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudPicOffsetX.Name = "nudPicOffsetX";
            nudPicOffsetX.Size = new Size(66, 23);
            nudPicOffsetX.TabIndex = 48;
            // 
            // DarkLabel69
            // 
            DarkLabel69.AutoSize = true;
            DarkLabel69.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel69.Location = new Point(130, 26);
            DarkLabel69.Margin = new Padding(4, 0, 4, 0);
            DarkLabel69.Name = "DarkLabel69";
            DarkLabel69.Size = new Size(47, 15);
            DarkLabel69.TabIndex = 47;
            DarkLabel69.Text = "Picture:";
            // 
            // cmbPicLoc
            // 
            cmbPicLoc.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPicLoc.FormattingEnabled = true;
            cmbPicLoc.Items.AddRange(new object[] { "Top Left of Screen", "Center Screen", "Centered on Event", "Centered on Player" });
            cmbPicLoc.Location = new Point(133, 86);
            cmbPicLoc.Margin = new Padding(4, 3, 4, 3);
            cmbPicLoc.Name = "cmbPicLoc";
            cmbPicLoc.Size = new Size(144, 24);
            cmbPicLoc.TabIndex = 46;
            // 
            // nudShowPicture
            // 
            nudShowPicture.Location = new Point(186, 24);
            nudShowPicture.Margin = new Padding(4, 3, 4, 3);
            nudShowPicture.Name = "nudShowPicture";
            nudShowPicture.Size = new Size(88, 23);
            nudShowPicture.TabIndex = 45;
            nudShowPicture.Click += nudShowPicture_Click;
            // 
            // picShowPic
            // 
            picShowPic.BackColor = Color.Black;
            picShowPic.BackgroundImageLayout = ImageLayout.Stretch;
            picShowPic.Location = new Point(9, 21);
            picShowPic.Margin = new Padding(4, 3, 4, 3);
            picShowPic.Name = "picShowPic";
            picShowPic.Size = new Size(117, 107);
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
            fraConditionalBranch.Location = new Point(7, 8);
            fraConditionalBranch.Margin = new Padding(4, 3, 4, 3);
            fraConditionalBranch.Name = "fraConditionalBranch";
            fraConditionalBranch.Padding = new Padding(4, 3, 4, 3);
            fraConditionalBranch.Size = new Size(454, 516);
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
            cmbCondition_Time.Location = new Point(279, 267);
            cmbCondition_Time.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_Time.Name = "cmbCondition_Time";
            cmbCondition_Time.Size = new Size(167, 24);
            cmbCondition_Time.TabIndex = 33;
            // 
            // optCondition9
            // 
            optCondition9.AutoSize = true;
            optCondition9.Location = new Point(7, 268);
            optCondition9.Margin = new Padding(4, 3, 4, 3);
            optCondition9.Name = "optCondition9";
            optCondition9.Size = new Size(102, 19);
            optCondition9.TabIndex = 32;
            optCondition9.TabStop = true;
            optCondition9.Text = "Time of Day is:";
            optCondition9.CheckedChanged += OptCondition9_CheckedChanged;
            // 
            // btnConditionalBranchOk
            // 
            btnConditionalBranchOk.Location = new Point(264, 480);
            btnConditionalBranchOk.Margin = new Padding(4, 3, 4, 3);
            btnConditionalBranchOk.Name = "btnConditionalBranchOk";
            btnConditionalBranchOk.Padding = new Padding(6);
            btnConditionalBranchOk.Size = new Size(88, 27);
            btnConditionalBranchOk.TabIndex = 31;
            btnConditionalBranchOk.Text = "Ok";
            btnConditionalBranchOk.Click += BtnConditionalBranchOk_Click;
            // 
            // btnConditionalBranchCancel
            // 
            btnConditionalBranchCancel.Location = new Point(358, 480);
            btnConditionalBranchCancel.Margin = new Padding(4, 3, 4, 3);
            btnConditionalBranchCancel.Name = "btnConditionalBranchCancel";
            btnConditionalBranchCancel.Padding = new Padding(6);
            btnConditionalBranchCancel.Size = new Size(88, 27);
            btnConditionalBranchCancel.TabIndex = 30;
            btnConditionalBranchCancel.Text = "Cancel";
            btnConditionalBranchCancel.Click += BtnConditionalBranchCancel_Click;
            // 
            // cmbCondition_Gender
            // 
            cmbCondition_Gender.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_Gender.FormattingEnabled = true;
            cmbCondition_Gender.Items.AddRange(new object[] { "Male", "Female" });
            cmbCondition_Gender.Location = new Point(279, 236);
            cmbCondition_Gender.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_Gender.Name = "cmbCondition_Gender";
            cmbCondition_Gender.Size = new Size(167, 24);
            cmbCondition_Gender.TabIndex = 29;
            // 
            // optCondition8
            // 
            optCondition8.AutoSize = true;
            optCondition8.Location = new Point(7, 237);
            optCondition8.Margin = new Padding(4, 3, 4, 3);
            optCondition8.Name = "optCondition8";
            optCondition8.Size = new Size(109, 19);
            optCondition8.TabIndex = 28;
            optCondition8.TabStop = true;
            optCondition8.Text = "Player Gender is";
            optCondition8.CheckedChanged += OptCondition8_CheckedChanged;
            // 
            // cmbCondition_SelfSwitchCondition
            // 
            cmbCondition_SelfSwitchCondition.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_SelfSwitchCondition.FormattingEnabled = true;
            cmbCondition_SelfSwitchCondition.Items.AddRange(new object[] { "False", "True" });
            cmbCondition_SelfSwitchCondition.Location = new Point(306, 211);
            cmbCondition_SelfSwitchCondition.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_SelfSwitchCondition.Name = "cmbCondition_SelfSwitchCondition";
            cmbCondition_SelfSwitchCondition.Size = new Size(140, 24);
            cmbCondition_SelfSwitchCondition.TabIndex = 23;
            // 
            // DarkLabel17
            // 
            DarkLabel17.AutoSize = true;
            DarkLabel17.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel17.Location = new Point(273, 215);
            DarkLabel17.Margin = new Padding(4, 0, 4, 0);
            DarkLabel17.Name = "DarkLabel17";
            DarkLabel17.Size = new Size(15, 15);
            DarkLabel17.TabIndex = 22;
            DarkLabel17.Text = "is";
            // 
            // cmbCondition_SelfSwitch
            // 
            cmbCondition_SelfSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_SelfSwitch.FormattingEnabled = true;
            cmbCondition_SelfSwitch.Location = new Point(125, 211);
            cmbCondition_SelfSwitch.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_SelfSwitch.Name = "cmbCondition_SelfSwitch";
            cmbCondition_SelfSwitch.Size = new Size(140, 24);
            cmbCondition_SelfSwitch.TabIndex = 21;
            // 
            // optCondition6
            // 
            optCondition6.AutoSize = true;
            optCondition6.Location = new Point(7, 212);
            optCondition6.Margin = new Padding(4, 3, 4, 3);
            optCondition6.Name = "optCondition6";
            optCondition6.Size = new Size(82, 19);
            optCondition6.TabIndex = 20;
            optCondition6.TabStop = true;
            optCondition6.Text = "Self Switch";
            optCondition6.CheckedChanged += OptCondition6_CheckedChanged;
            // 
            // nudCondition_LevelAmount
            // 
            nudCondition_LevelAmount.Location = new Point(314, 181);
            nudCondition_LevelAmount.Margin = new Padding(4, 3, 4, 3);
            nudCondition_LevelAmount.Name = "nudCondition_LevelAmount";
            nudCondition_LevelAmount.Size = new Size(132, 23);
            nudCondition_LevelAmount.TabIndex = 19;
            // 
            // optCondition5
            // 
            optCondition5.AutoSize = true;
            optCondition5.Location = new Point(7, 181);
            optCondition5.Margin = new Padding(4, 3, 4, 3);
            optCondition5.Name = "optCondition5";
            optCondition5.Size = new Size(63, 19);
            optCondition5.TabIndex = 18;
            optCondition5.TabStop = true;
            optCondition5.Text = "Level is";
            optCondition5.CheckedChanged += OptCondition5_CheckedChanged;
            // 
            // cmbCondition_LevelCompare
            // 
            cmbCondition_LevelCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_LevelCompare.FormattingEnabled = true;
            cmbCondition_LevelCompare.Items.AddRange(new object[] { "Equal To", "Great Than Or Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal" });
            cmbCondition_LevelCompare.Location = new Point(125, 180);
            cmbCondition_LevelCompare.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_LevelCompare.Name = "cmbCondition_LevelCompare";
            cmbCondition_LevelCompare.Size = new Size(181, 24);
            cmbCondition_LevelCompare.TabIndex = 17;
            // 
            // cmbCondition_LearntSkill
            // 
            cmbCondition_LearntSkill.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_LearntSkill.FormattingEnabled = true;
            cmbCondition_LearntSkill.Location = new Point(125, 149);
            cmbCondition_LearntSkill.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_LearntSkill.Name = "cmbCondition_LearntSkill";
            cmbCondition_LearntSkill.Size = new Size(321, 24);
            cmbCondition_LearntSkill.TabIndex = 16;
            // 
            // optCondition4
            // 
            optCondition4.AutoSize = true;
            optCondition4.Location = new Point(7, 150);
            optCondition4.Margin = new Padding(4, 3, 4, 3);
            optCondition4.Name = "optCondition4";
            optCondition4.Size = new Size(84, 19);
            optCondition4.TabIndex = 15;
            optCondition4.TabStop = true;
            optCondition4.Text = "Knows Skill";
            optCondition4.CheckedChanged += OptCondition4_CheckedChanged;
            // 
            // cmbCondition_JobIs
            // 
            cmbCondition_JobIs.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_JobIs.FormattingEnabled = true;
            cmbCondition_JobIs.Location = new Point(125, 118);
            cmbCondition_JobIs.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_JobIs.Name = "cmbCondition_JobIs";
            cmbCondition_JobIs.Size = new Size(321, 24);
            cmbCondition_JobIs.TabIndex = 14;
            // 
            // optCondition3
            // 
            optCondition3.AutoSize = true;
            optCondition3.Location = new Point(7, 119);
            optCondition3.Margin = new Padding(4, 3, 4, 3);
            optCondition3.Name = "optCondition3";
            optCondition3.Size = new Size(54, 19);
            optCondition3.TabIndex = 13;
            optCondition3.TabStop = true;
            optCondition3.Text = "Job Is";
            optCondition3.CheckedChanged += OptCondition3_CheckedChanged;
            // 
            // nudCondition_HasItem
            // 
            nudCondition_HasItem.Location = new Point(306, 88);
            nudCondition_HasItem.Margin = new Padding(4, 3, 4, 3);
            nudCondition_HasItem.Name = "nudCondition_HasItem";
            nudCondition_HasItem.Size = new Size(140, 23);
            nudCondition_HasItem.TabIndex = 12;
            // 
            // DarkLabel16
            // 
            DarkLabel16.AutoSize = true;
            DarkLabel16.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel16.Location = new Point(273, 90);
            DarkLabel16.Margin = new Padding(4, 0, 4, 0);
            DarkLabel16.Name = "DarkLabel16";
            DarkLabel16.Size = new Size(14, 15);
            DarkLabel16.TabIndex = 11;
            DarkLabel16.Text = "X";
            // 
            // cmbCondition_HasItem
            // 
            cmbCondition_HasItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_HasItem.FormattingEnabled = true;
            cmbCondition_HasItem.Location = new Point(125, 87);
            cmbCondition_HasItem.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_HasItem.Name = "cmbCondition_HasItem";
            cmbCondition_HasItem.Size = new Size(140, 24);
            cmbCondition_HasItem.TabIndex = 10;
            // 
            // optCondition2
            // 
            optCondition2.AutoSize = true;
            optCondition2.Location = new Point(7, 88);
            optCondition2.Margin = new Padding(4, 3, 4, 3);
            optCondition2.Name = "optCondition2";
            optCondition2.Size = new Size(72, 19);
            optCondition2.TabIndex = 9;
            optCondition2.TabStop = true;
            optCondition2.Text = "Has Item";
            optCondition2.CheckedChanged += OptCondition2_CheckedChanged;
            // 
            // optCondition1
            // 
            optCondition1.AutoSize = true;
            optCondition1.Location = new Point(7, 57);
            optCondition1.Margin = new Padding(4, 3, 4, 3);
            optCondition1.Name = "optCondition1";
            optCondition1.Size = new Size(95, 19);
            optCondition1.TabIndex = 8;
            optCondition1.TabStop = true;
            optCondition1.Text = "Player Switch";
            optCondition1.CheckedChanged += OptCondition1_CheckedChanged;
            // 
            // DarkLabel15
            // 
            DarkLabel15.AutoSize = true;
            DarkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel15.Location = new Point(273, 59);
            DarkLabel15.Margin = new Padding(4, 0, 4, 0);
            DarkLabel15.Name = "DarkLabel15";
            DarkLabel15.Size = new Size(15, 15);
            DarkLabel15.TabIndex = 7;
            DarkLabel15.Text = "is";
            // 
            // cmbCondtion_PlayerSwitchCondition
            // 
            cmbCondtion_PlayerSwitchCondition.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondtion_PlayerSwitchCondition.FormattingEnabled = true;
            cmbCondtion_PlayerSwitchCondition.Items.AddRange(new object[] { "False", "True" });
            cmbCondtion_PlayerSwitchCondition.Location = new Point(306, 55);
            cmbCondtion_PlayerSwitchCondition.Margin = new Padding(4, 3, 4, 3);
            cmbCondtion_PlayerSwitchCondition.Name = "cmbCondtion_PlayerSwitchCondition";
            cmbCondtion_PlayerSwitchCondition.Size = new Size(140, 24);
            cmbCondtion_PlayerSwitchCondition.TabIndex = 6;
            // 
            // cmbCondition_PlayerSwitch
            // 
            cmbCondition_PlayerSwitch.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_PlayerSwitch.FormattingEnabled = true;
            cmbCondition_PlayerSwitch.Location = new Point(125, 55);
            cmbCondition_PlayerSwitch.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_PlayerSwitch.Name = "cmbCondition_PlayerSwitch";
            cmbCondition_PlayerSwitch.Size = new Size(140, 24);
            cmbCondition_PlayerSwitch.TabIndex = 5;
            // 
            // nudCondition_PlayerVarCondition
            // 
            nudCondition_PlayerVarCondition.Location = new Point(391, 25);
            nudCondition_PlayerVarCondition.Margin = new Padding(4, 3, 4, 3);
            nudCondition_PlayerVarCondition.Name = "nudCondition_PlayerVarCondition";
            nudCondition_PlayerVarCondition.Size = new Size(55, 23);
            nudCondition_PlayerVarCondition.TabIndex = 4;
            // 
            // cmbCondition_PlayerVarCompare
            // 
            cmbCondition_PlayerVarCompare.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_PlayerVarCompare.FormattingEnabled = true;
            cmbCondition_PlayerVarCompare.Items.AddRange(new object[] { "Equal To", "Great Than Or Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal" });
            cmbCondition_PlayerVarCompare.Location = new Point(275, 24);
            cmbCondition_PlayerVarCompare.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_PlayerVarCompare.Name = "cmbCondition_PlayerVarCompare";
            cmbCondition_PlayerVarCompare.Size = new Size(102, 24);
            cmbCondition_PlayerVarCompare.TabIndex = 3;
            // 
            // DarkLabel14
            // 
            DarkLabel14.AutoSize = true;
            DarkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel14.Location = new Point(252, 28);
            DarkLabel14.Margin = new Padding(4, 0, 4, 0);
            DarkLabel14.Name = "DarkLabel14";
            DarkLabel14.Size = new Size(15, 15);
            DarkLabel14.TabIndex = 2;
            DarkLabel14.Text = "is";
            // 
            // cmbCondition_PlayerVarIndex
            // 
            cmbCondition_PlayerVarIndex.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCondition_PlayerVarIndex.FormattingEnabled = true;
            cmbCondition_PlayerVarIndex.Location = new Point(125, 24);
            cmbCondition_PlayerVarIndex.Margin = new Padding(4, 3, 4, 3);
            cmbCondition_PlayerVarIndex.Name = "cmbCondition_PlayerVarIndex";
            cmbCondition_PlayerVarIndex.Size = new Size(120, 24);
            cmbCondition_PlayerVarIndex.TabIndex = 1;
            // 
            // optCondition0
            // 
            optCondition0.AutoSize = true;
            optCondition0.Location = new Point(7, 25);
            optCondition0.Margin = new Padding(4, 3, 4, 3);
            optCondition0.Name = "optCondition0";
            optCondition0.Size = new Size(101, 19);
            optCondition0.TabIndex = 0;
            optCondition0.TabStop = true;
            optCondition0.Text = "Player Variable";
            optCondition0.CheckedChanged += OptCondition_Index0_CheckedChanged;
            // 
            // fraPlayBGM
            // 
            fraPlayBGM.BackColor = Color.FromArgb(45, 45, 48);
            fraPlayBGM.BorderColor = Color.FromArgb(90, 90, 90);
            fraPlayBGM.Controls.Add(btnPlayBgmOk);
            fraPlayBGM.Controls.Add(btnPlayBgmCancel);
            fraPlayBGM.Controls.Add(cmbPlayBGM);
            fraPlayBGM.ForeColor = Color.Gainsboro;
            fraPlayBGM.Location = new Point(468, 1);
            fraPlayBGM.Margin = new Padding(4, 3, 4, 3);
            fraPlayBGM.Name = "fraPlayBGM";
            fraPlayBGM.Padding = new Padding(4, 3, 4, 3);
            fraPlayBGM.Size = new Size(287, 87);
            fraPlayBGM.TabIndex = 21;
            fraPlayBGM.TabStop = false;
            fraPlayBGM.Text = "Play BGM";
            fraPlayBGM.Visible = false;
            // 
            // btnPlayBgmOk
            // 
            btnPlayBgmOk.Location = new Point(54, 53);
            btnPlayBgmOk.Margin = new Padding(4, 3, 4, 3);
            btnPlayBgmOk.Name = "btnPlayBgmOk";
            btnPlayBgmOk.Padding = new Padding(6);
            btnPlayBgmOk.Size = new Size(88, 27);
            btnPlayBgmOk.TabIndex = 27;
            btnPlayBgmOk.Text = "Ok";
            btnPlayBgmOk.Click += BtnPlayBgmOK_Click;
            // 
            // btnPlayBgmCancel
            // 
            btnPlayBgmCancel.Location = new Point(148, 53);
            btnPlayBgmCancel.Margin = new Padding(4, 3, 4, 3);
            btnPlayBgmCancel.Name = "btnPlayBgmCancel";
            btnPlayBgmCancel.Padding = new Padding(6);
            btnPlayBgmCancel.Size = new Size(88, 27);
            btnPlayBgmCancel.TabIndex = 26;
            btnPlayBgmCancel.Text = "Cancel";
            btnPlayBgmCancel.Click += BtnPlayBgmCancel_Click;
            // 
            // cmbPlayBGM
            // 
            cmbPlayBGM.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlayBGM.FormattingEnabled = true;
            cmbPlayBGM.Location = new Point(7, 22);
            cmbPlayBGM.Margin = new Padding(4, 3, 4, 3);
            cmbPlayBGM.Name = "cmbPlayBGM";
            cmbPlayBGM.Size = new Size(271, 24);
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
            fraPlayerWarp.Location = new Point(468, 7);
            fraPlayerWarp.Margin = new Padding(4, 3, 4, 3);
            fraPlayerWarp.Name = "fraPlayerWarp";
            fraPlayerWarp.Padding = new Padding(4, 3, 4, 3);
            fraPlayerWarp.Size = new Size(287, 112);
            fraPlayerWarp.TabIndex = 19;
            fraPlayerWarp.TabStop = false;
            fraPlayerWarp.Text = "Warp Player";
            fraPlayerWarp.Visible = false;
            // 
            // btnPlayerWarpOk
            // 
            btnPlayerWarpOk.Location = new Point(97, 78);
            btnPlayerWarpOk.Margin = new Padding(4, 3, 4, 3);
            btnPlayerWarpOk.Name = "btnPlayerWarpOk";
            btnPlayerWarpOk.Padding = new Padding(6);
            btnPlayerWarpOk.Size = new Size(88, 27);
            btnPlayerWarpOk.TabIndex = 46;
            btnPlayerWarpOk.Text = "Ok";
            btnPlayerWarpOk.Click += BtnPlayerWarpOK_Click;
            // 
            // btnPlayerWarpCancel
            // 
            btnPlayerWarpCancel.Location = new Point(191, 78);
            btnPlayerWarpCancel.Margin = new Padding(4, 3, 4, 3);
            btnPlayerWarpCancel.Name = "btnPlayerWarpCancel";
            btnPlayerWarpCancel.Padding = new Padding(6);
            btnPlayerWarpCancel.Size = new Size(88, 27);
            btnPlayerWarpCancel.TabIndex = 45;
            btnPlayerWarpCancel.Text = "Cancel";
            btnPlayerWarpCancel.Click += BtnPlayerWarpCancel_Click;
            // 
            // DarkLabel31
            // 
            DarkLabel31.AutoSize = true;
            DarkLabel31.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel31.Location = new Point(9, 51);
            DarkLabel31.Margin = new Padding(4, 0, 4, 0);
            DarkLabel31.Name = "DarkLabel31";
            DarkLabel31.Size = new Size(58, 15);
            DarkLabel31.TabIndex = 44;
            DarkLabel31.Text = "Direction:";
            // 
            // cmbWarpPlayerDir
            // 
            cmbWarpPlayerDir.DrawMode = DrawMode.OwnerDrawFixed;
            cmbWarpPlayerDir.FormattingEnabled = true;
            cmbWarpPlayerDir.Items.AddRange(new object[] { "Retain Direction", "Up", "Down", "Left", "Right" });
            cmbWarpPlayerDir.Location = new Point(112, 47);
            cmbWarpPlayerDir.Margin = new Padding(4, 3, 4, 3);
            cmbWarpPlayerDir.Name = "cmbWarpPlayerDir";
            cmbWarpPlayerDir.Size = new Size(166, 24);
            cmbWarpPlayerDir.TabIndex = 43;
            // 
            // nudWPY
            // 
            nudWPY.Location = new Point(233, 17);
            nudWPY.Margin = new Padding(4, 3, 4, 3);
            nudWPY.Name = "nudWPY";
            nudWPY.Size = new Size(46, 23);
            nudWPY.TabIndex = 42;
            // 
            // DarkLabel32
            // 
            DarkLabel32.AutoSize = true;
            DarkLabel32.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel32.Location = new Point(206, 20);
            DarkLabel32.Margin = new Padding(4, 0, 4, 0);
            DarkLabel32.Name = "DarkLabel32";
            DarkLabel32.Size = new Size(17, 15);
            DarkLabel32.TabIndex = 41;
            DarkLabel32.Text = "Y:";
            // 
            // nudWPX
            // 
            nudWPX.Location = new Point(152, 17);
            nudWPX.Margin = new Padding(4, 3, 4, 3);
            nudWPX.Name = "nudWPX";
            nudWPX.Size = new Size(46, 23);
            nudWPX.TabIndex = 40;
            // 
            // DarkLabel33
            // 
            DarkLabel33.AutoSize = true;
            DarkLabel33.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel33.Location = new Point(125, 20);
            DarkLabel33.Margin = new Padding(4, 0, 4, 0);
            DarkLabel33.Name = "DarkLabel33";
            DarkLabel33.Size = new Size(17, 15);
            DarkLabel33.TabIndex = 39;
            DarkLabel33.Text = "X:";
            // 
            // nudWPMap
            // 
            nudWPMap.Location = new Point(50, 17);
            nudWPMap.Margin = new Padding(4, 3, 4, 3);
            nudWPMap.Name = "nudWPMap";
            nudWPMap.Size = new Size(68, 23);
            nudWPMap.TabIndex = 38;
            // 
            // DarkLabel34
            // 
            DarkLabel34.AutoSize = true;
            DarkLabel34.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel34.Location = new Point(7, 20);
            DarkLabel34.Margin = new Padding(4, 0, 4, 0);
            DarkLabel34.Name = "DarkLabel34";
            DarkLabel34.Size = new Size(34, 15);
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
            fraSetFog.Location = new Point(468, 8);
            fraSetFog.Margin = new Padding(4, 3, 4, 3);
            fraSetFog.Name = "fraSetFog";
            fraSetFog.Padding = new Padding(4, 3, 4, 3);
            fraSetFog.Size = new Size(287, 111);
            fraSetFog.TabIndex = 18;
            fraSetFog.TabStop = false;
            fraSetFog.Text = "Set Fog";
            fraSetFog.Visible = false;
            // 
            // btnSetFogOk
            // 
            btnSetFogOk.Location = new Point(98, 77);
            btnSetFogOk.Margin = new Padding(4, 3, 4, 3);
            btnSetFogOk.Name = "btnSetFogOk";
            btnSetFogOk.Padding = new Padding(6);
            btnSetFogOk.Size = new Size(88, 27);
            btnSetFogOk.TabIndex = 41;
            btnSetFogOk.Text = "Ok";
            btnSetFogOk.Click += BtnSetFogOK_Click;
            // 
            // btnSetFogCancel
            // 
            btnSetFogCancel.Location = new Point(192, 77);
            btnSetFogCancel.Margin = new Padding(4, 3, 4, 3);
            btnSetFogCancel.Name = "btnSetFogCancel";
            btnSetFogCancel.Padding = new Padding(6);
            btnSetFogCancel.Size = new Size(88, 27);
            btnSetFogCancel.TabIndex = 40;
            btnSetFogCancel.Text = "Cancel";
            btnSetFogCancel.Click += BtnSetFogCancel_Click;
            // 
            // DarkLabel30
            // 
            DarkLabel30.AutoSize = true;
            DarkLabel30.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel30.Location = new Point(145, 48);
            DarkLabel30.Margin = new Padding(4, 0, 4, 0);
            DarkLabel30.Name = "DarkLabel30";
            DarkLabel30.Size = new Size(74, 15);
            DarkLabel30.TabIndex = 39;
            DarkLabel30.Text = "Fog Opacity:";
            // 
            // DarkLabel29
            // 
            DarkLabel29.AutoSize = true;
            DarkLabel29.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel29.Location = new Point(8, 48);
            DarkLabel29.Margin = new Padding(4, 0, 4, 0);
            DarkLabel29.Name = "DarkLabel29";
            DarkLabel29.Size = new Size(65, 15);
            DarkLabel29.TabIndex = 38;
            DarkLabel29.Text = "Fog Speed:";
            // 
            // DarkLabel28
            // 
            DarkLabel28.AutoSize = true;
            DarkLabel28.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel28.Location = new Point(8, 17);
            DarkLabel28.Margin = new Padding(4, 0, 4, 0);
            DarkLabel28.Name = "DarkLabel28";
            DarkLabel28.Size = new Size(30, 15);
            DarkLabel28.TabIndex = 37;
            DarkLabel28.Text = "Fog:";
            // 
            // nudFogData2
            // 
            nudFogData2.Location = new Point(223, 45);
            nudFogData2.Margin = new Padding(4, 3, 4, 3);
            nudFogData2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudFogData2.Name = "nudFogData2";
            nudFogData2.Size = new Size(57, 23);
            nudFogData2.TabIndex = 36;
            // 
            // nudFogData1
            // 
            nudFogData1.Location = new Point(84, 46);
            nudFogData1.Margin = new Padding(4, 3, 4, 3);
            nudFogData1.Name = "nudFogData1";
            nudFogData1.Size = new Size(56, 23);
            nudFogData1.TabIndex = 35;
            // 
            // nudFogData0
            // 
            nudFogData0.Location = new Point(113, 14);
            nudFogData0.Margin = new Padding(4, 3, 4, 3);
            nudFogData0.Name = "nudFogData0";
            nudFogData0.Size = new Size(167, 23);
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
            fraShowText.Location = new Point(7, 351);
            fraShowText.Margin = new Padding(4, 3, 4, 3);
            fraShowText.Name = "fraShowText";
            fraShowText.Padding = new Padding(4, 3, 4, 3);
            fraShowText.Size = new Size(289, 328);
            fraShowText.TabIndex = 17;
            fraShowText.TabStop = false;
            fraShowText.Text = "Show Text";
            fraShowText.Visible = false;
            // 
            // DarkLabel27
            // 
            DarkLabel27.AutoSize = true;
            DarkLabel27.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel27.Location = new Point(8, 22);
            DarkLabel27.Margin = new Padding(4, 0, 4, 0);
            DarkLabel27.Name = "DarkLabel27";
            DarkLabel27.Size = new Size(28, 15);
            DarkLabel27.TabIndex = 26;
            DarkLabel27.Text = "Text";
            // 
            // txtShowText
            // 
            txtShowText.BackColor = Color.FromArgb(69, 73, 74);
            txtShowText.BorderStyle = BorderStyle.FixedSingle;
            txtShowText.ForeColor = Color.FromArgb(220, 220, 220);
            txtShowText.Location = new Point(10, 44);
            txtShowText.Margin = new Padding(4, 3, 4, 3);
            txtShowText.Multiline = true;
            txtShowText.Name = "txtShowText";
            txtShowText.Size = new Size(266, 121);
            txtShowText.TabIndex = 21;
            // 
            // btnShowTextCancel
            // 
            btnShowTextCancel.Location = new Point(195, 291);
            btnShowTextCancel.Margin = new Padding(4, 3, 4, 3);
            btnShowTextCancel.Name = "btnShowTextCancel";
            btnShowTextCancel.Padding = new Padding(6);
            btnShowTextCancel.Size = new Size(88, 27);
            btnShowTextCancel.TabIndex = 24;
            btnShowTextCancel.Text = "Cancel";
            btnShowTextCancel.Click += BtnShowTextCancel_Click;
            // 
            // btnShowTextOk
            // 
            btnShowTextOk.Location = new Point(100, 291);
            btnShowTextOk.Margin = new Padding(4, 3, 4, 3);
            btnShowTextOk.Name = "btnShowTextOk";
            btnShowTextOk.Padding = new Padding(6);
            btnShowTextOk.Size = new Size(88, 27);
            btnShowTextOk.TabIndex = 25;
            btnShowTextOk.Text = "Ok";
            btnShowTextOk.Click += BtnShowTextOk_Click;
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
            fraAddText.Location = new Point(7, 419);
            fraAddText.Margin = new Padding(4, 3, 4, 3);
            fraAddText.Name = "fraAddText";
            fraAddText.Padding = new Padding(4, 3, 4, 3);
            fraAddText.Size = new Size(272, 216);
            fraAddText.TabIndex = 3;
            fraAddText.TabStop = false;
            fraAddText.Text = "Add Text";
            fraAddText.Visible = false;
            // 
            // btnAddTextOk
            // 
            btnAddTextOk.Location = new Point(64, 180);
            btnAddTextOk.Margin = new Padding(4, 3, 4, 3);
            btnAddTextOk.Name = "btnAddTextOk";
            btnAddTextOk.Padding = new Padding(6);
            btnAddTextOk.Size = new Size(88, 27);
            btnAddTextOk.TabIndex = 9;
            btnAddTextOk.Text = "Ok";
            btnAddTextOk.Click += BtnAddTextOk_Click;
            // 
            // btnAddTextCancel
            // 
            btnAddTextCancel.Location = new Point(159, 180);
            btnAddTextCancel.Margin = new Padding(4, 3, 4, 3);
            btnAddTextCancel.Name = "btnAddTextCancel";
            btnAddTextCancel.Padding = new Padding(6);
            btnAddTextCancel.Size = new Size(88, 27);
            btnAddTextCancel.TabIndex = 8;
            btnAddTextCancel.Text = "Cancel";
            btnAddTextCancel.Click += BtnAddTextCancel_Click;
            // 
            // optAddText_Global
            // 
            optAddText_Global.AutoSize = true;
            optAddText_Global.Location = new Point(202, 153);
            optAddText_Global.Margin = new Padding(4, 3, 4, 3);
            optAddText_Global.Name = "optAddText_Global";
            optAddText_Global.Size = new Size(59, 19);
            optAddText_Global.TabIndex = 5;
            optAddText_Global.TabStop = true;
            optAddText_Global.Text = "Global";
            // 
            // optAddText_Map
            // 
            optAddText_Map.AutoSize = true;
            optAddText_Map.Location = new Point(141, 153);
            optAddText_Map.Margin = new Padding(4, 3, 4, 3);
            optAddText_Map.Name = "optAddText_Map";
            optAddText_Map.Size = new Size(49, 19);
            optAddText_Map.TabIndex = 4;
            optAddText_Map.TabStop = true;
            optAddText_Map.Text = "Map";
            // 
            // optAddText_Player
            // 
            optAddText_Player.AutoSize = true;
            optAddText_Player.Location = new Point(71, 153);
            optAddText_Player.Margin = new Padding(4, 3, 4, 3);
            optAddText_Player.Name = "optAddText_Player";
            optAddText_Player.Size = new Size(57, 19);
            optAddText_Player.TabIndex = 3;
            optAddText_Player.TabStop = true;
            optAddText_Player.Text = "Player";
            // 
            // DarkLabel25
            // 
            DarkLabel25.AutoSize = true;
            DarkLabel25.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel25.Location = new Point(7, 156);
            DarkLabel25.Margin = new Padding(4, 0, 4, 0);
            DarkLabel25.Name = "DarkLabel25";
            DarkLabel25.Size = new Size(54, 15);
            DarkLabel25.TabIndex = 2;
            DarkLabel25.Text = "Channel:";
            // 
            // txtAddText_Text
            // 
            txtAddText_Text.BackColor = Color.FromArgb(69, 73, 74);
            txtAddText_Text.BorderStyle = BorderStyle.FixedSingle;
            txtAddText_Text.ForeColor = Color.FromArgb(220, 220, 220);
            txtAddText_Text.Location = new Point(7, 36);
            txtAddText_Text.Margin = new Padding(4, 3, 4, 3);
            txtAddText_Text.Multiline = true;
            txtAddText_Text.Name = "txtAddText_Text";
            txtAddText_Text.Size = new Size(259, 110);
            txtAddText_Text.TabIndex = 1;
            // 
            // DarkLabel24
            // 
            DarkLabel24.AutoSize = true;
            DarkLabel24.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel24.Location = new Point(7, 17);
            DarkLabel24.Margin = new Padding(4, 0, 4, 0);
            DarkLabel24.Name = "DarkLabel24";
            DarkLabel24.Size = new Size(28, 15);
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
            fraChangeItems.Location = new Point(7, 450);
            fraChangeItems.Margin = new Padding(4, 3, 4, 3);
            fraChangeItems.Name = "fraChangeItems";
            fraChangeItems.Padding = new Padding(4, 3, 4, 3);
            fraChangeItems.Size = new Size(218, 138);
            fraChangeItems.TabIndex = 1;
            fraChangeItems.TabStop = false;
            fraChangeItems.Text = "Change Items";
            fraChangeItems.Visible = false;
            // 
            // btnChangeItemsOk
            // 
            btnChangeItemsOk.Location = new Point(29, 105);
            btnChangeItemsOk.Margin = new Padding(4, 3, 4, 3);
            btnChangeItemsOk.Name = "btnChangeItemsOk";
            btnChangeItemsOk.Padding = new Padding(6);
            btnChangeItemsOk.Size = new Size(88, 27);
            btnChangeItemsOk.TabIndex = 7;
            btnChangeItemsOk.Text = "Ok";
            btnChangeItemsOk.Click += BtnChangeItemsOk_Click;
            // 
            // btnChangeItemsCancel
            // 
            btnChangeItemsCancel.Location = new Point(124, 105);
            btnChangeItemsCancel.Margin = new Padding(4, 3, 4, 3);
            btnChangeItemsCancel.Name = "btnChangeItemsCancel";
            btnChangeItemsCancel.Padding = new Padding(6);
            btnChangeItemsCancel.Size = new Size(88, 27);
            btnChangeItemsCancel.TabIndex = 6;
            btnChangeItemsCancel.Text = "Cancel";
            btnChangeItemsCancel.Click += BtnChangeItemsCancel_Click;
            // 
            // nudChangeItemsAmount
            // 
            nudChangeItemsAmount.Location = new Point(10, 75);
            nudChangeItemsAmount.Margin = new Padding(4, 3, 4, 3);
            nudChangeItemsAmount.Name = "nudChangeItemsAmount";
            nudChangeItemsAmount.Size = new Size(201, 23);
            nudChangeItemsAmount.TabIndex = 5;
            // 
            // optChangeItemRemove
            // 
            optChangeItemRemove.AutoSize = true;
            optChangeItemRemove.Location = new Point(141, 48);
            optChangeItemRemove.Margin = new Padding(4, 3, 4, 3);
            optChangeItemRemove.Name = "optChangeItemRemove";
            optChangeItemRemove.Size = new Size(48, 19);
            optChangeItemRemove.TabIndex = 4;
            optChangeItemRemove.TabStop = true;
            optChangeItemRemove.Text = "Take";
            // 
            // optChangeItemAdd
            // 
            optChangeItemAdd.AutoSize = true;
            optChangeItemAdd.Location = new Point(79, 48);
            optChangeItemAdd.Margin = new Padding(4, 3, 4, 3);
            optChangeItemAdd.Name = "optChangeItemAdd";
            optChangeItemAdd.Size = new Size(48, 19);
            optChangeItemAdd.TabIndex = 3;
            optChangeItemAdd.TabStop = true;
            optChangeItemAdd.Text = "Give";
            // 
            // optChangeItemSet
            // 
            optChangeItemSet.AutoSize = true;
            optChangeItemSet.Location = new Point(10, 48);
            optChangeItemSet.Margin = new Padding(4, 3, 4, 3);
            optChangeItemSet.Name = "optChangeItemSet";
            optChangeItemSet.Size = new Size(55, 19);
            optChangeItemSet.TabIndex = 2;
            optChangeItemSet.TabStop = true;
            optChangeItemSet.Text = "Set to";
            // 
            // cmbChangeItemIndex
            // 
            cmbChangeItemIndex.DrawMode = DrawMode.OwnerDrawFixed;
            cmbChangeItemIndex.FormattingEnabled = true;
            cmbChangeItemIndex.Location = new Point(49, 15);
            cmbChangeItemIndex.Margin = new Padding(4, 3, 4, 3);
            cmbChangeItemIndex.Name = "cmbChangeItemIndex";
            cmbChangeItemIndex.Size = new Size(162, 24);
            cmbChangeItemIndex.TabIndex = 1;
            // 
            // DarkLabel21
            // 
            DarkLabel21.AutoSize = true;
            DarkLabel21.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel21.Location = new Point(7, 18);
            DarkLabel21.Margin = new Padding(4, 0, 4, 0);
            DarkLabel21.Name = "DarkLabel21";
            DarkLabel21.Size = new Size(34, 15);
            DarkLabel21.TabIndex = 0;
            DarkLabel21.Text = "Item:";
            // 
            // pnlVariableSwitches
            // 
            pnlVariableSwitches.Controls.Add(FraRenaming);
            pnlVariableSwitches.Controls.Add(fraLabeling);
            pnlVariableSwitches.Location = new Point(933, 232);
            pnlVariableSwitches.Margin = new Padding(4, 3, 4, 3);
            pnlVariableSwitches.Name = "pnlVariableSwitches";
            pnlVariableSwitches.Size = new Size(108, 105);
            pnlVariableSwitches.TabIndex = 11;
            // 
            // FraRenaming
            // 
            FraRenaming.BorderColor = Color.FromArgb(51, 51, 51);
            FraRenaming.Controls.Add(btnRename_Cancel);
            FraRenaming.Controls.Add(btnRename_Ok);
            FraRenaming.Controls.Add(fraRandom10);
            FraRenaming.ForeColor = Color.Gainsboro;
            FraRenaming.Location = new Point(275, 495);
            FraRenaming.Margin = new Padding(4, 3, 4, 3);
            FraRenaming.Name = "FraRenaming";
            FraRenaming.Padding = new Padding(4, 3, 4, 3);
            FraRenaming.Size = new Size(425, 165);
            FraRenaming.TabIndex = 8;
            FraRenaming.TabStop = false;
            FraRenaming.Text = "Renaming Variable/Switch";
            FraRenaming.Visible = false;
            // 
            // btnRename_Cancel
            // 
            btnRename_Cancel.ForeColor = Color.Black;
            btnRename_Cancel.Location = new Point(267, 118);
            btnRename_Cancel.Margin = new Padding(4, 3, 4, 3);
            btnRename_Cancel.Name = "btnRename_Cancel";
            btnRename_Cancel.Padding = new Padding(5);
            btnRename_Cancel.Size = new Size(88, 27);
            btnRename_Cancel.TabIndex = 2;
            btnRename_Cancel.Text = "Cancel";
            btnRename_Cancel.Click += BtnRename_Cancel_Click;
            // 
            // btnRename_Ok
            // 
            btnRename_Ok.ForeColor = Color.Black;
            btnRename_Ok.Location = new Point(63, 118);
            btnRename_Ok.Margin = new Padding(4, 3, 4, 3);
            btnRename_Ok.Name = "btnRename_Ok";
            btnRename_Ok.Padding = new Padding(5);
            btnRename_Ok.Size = new Size(88, 27);
            btnRename_Ok.TabIndex = 1;
            btnRename_Ok.Text = "Ok";
            btnRename_Ok.Click += BtnRename_Ok_Click;
            // 
            // fraRandom10
            // 
            fraRandom10.BorderColor = Color.FromArgb(51, 51, 51);
            fraRandom10.Controls.Add(txtRename);
            fraRandom10.Controls.Add(lblEditing);
            fraRandom10.ForeColor = Color.Gainsboro;
            fraRandom10.Location = new Point(7, 22);
            fraRandom10.Margin = new Padding(4, 3, 4, 3);
            fraRandom10.Name = "fraRandom10";
            fraRandom10.Padding = new Padding(4, 3, 4, 3);
            fraRandom10.Size = new Size(411, 89);
            fraRandom10.TabIndex = 0;
            fraRandom10.TabStop = false;
            fraRandom10.Text = "Editing Variable/Switch";
            // 
            // txtRename
            // 
            txtRename.BackColor = Color.FromArgb(69, 73, 74);
            txtRename.BorderStyle = BorderStyle.FixedSingle;
            txtRename.ForeColor = Color.FromArgb(220, 220, 220);
            txtRename.Location = new Point(7, 47);
            txtRename.Margin = new Padding(4, 3, 4, 3);
            txtRename.Name = "txtRename";
            txtRename.Size = new Size(396, 23);
            txtRename.TabIndex = 1;
            txtRename.TextChanged += TxtRename_TextChanged;
            // 
            // lblEditing
            // 
            lblEditing.AutoSize = true;
            lblEditing.ForeColor = Color.FromArgb(220, 220, 220);
            lblEditing.Location = new Point(4, 29);
            lblEditing.Margin = new Padding(4, 0, 4, 0);
            lblEditing.Name = "lblEditing";
            lblEditing.Size = new Size(110, 15);
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
            fraLabeling.Location = new Point(228, 33);
            fraLabeling.Margin = new Padding(4, 3, 4, 3);
            fraLabeling.Name = "fraLabeling";
            fraLabeling.Padding = new Padding(4, 3, 4, 3);
            fraLabeling.Size = new Size(532, 447);
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
            lstSwitches.Location = new Point(275, 45);
            lstSwitches.Margin = new Padding(4, 3, 4, 3);
            lstSwitches.Name = "lstSwitches";
            lstSwitches.Size = new Size(239, 332);
            lstSwitches.TabIndex = 7;
            lstSwitches.DoubleClick += LstSwitches_DoubleClick;
            // 
            // lstVariables
            // 
            lstVariables.BackColor = Color.FromArgb(45, 45, 48);
            lstVariables.BorderStyle = BorderStyle.FixedSingle;
            lstVariables.ForeColor = Color.Gainsboro;
            lstVariables.FormattingEnabled = true;
            lstVariables.Location = new Point(16, 45);
            lstVariables.Margin = new Padding(4, 3, 4, 3);
            lstVariables.Name = "lstVariables";
            lstVariables.Size = new Size(239, 332);
            lstVariables.TabIndex = 6;
            lstVariables.DoubleClick += LstVariables_DoubleClick;
            // 
            // btnLabel_Cancel
            // 
            btnLabel_Cancel.ForeColor = Color.Black;
            btnLabel_Cancel.Location = new Point(275, 393);
            btnLabel_Cancel.Margin = new Padding(4, 3, 4, 3);
            btnLabel_Cancel.Name = "btnLabel_Cancel";
            btnLabel_Cancel.Padding = new Padding(4, 3, 4, 3);
            btnLabel_Cancel.Size = new Size(88, 27);
            btnLabel_Cancel.TabIndex = 12;
            btnLabel_Cancel.Text = "Cancel";
            btnLabel_Cancel.Click += BtnLabel_Cancel_Click;
            // 
            // lblRandomLabel36
            // 
            lblRandomLabel36.AutoSize = true;
            lblRandomLabel36.ForeColor = Color.FromArgb(220, 220, 220);
            lblRandomLabel36.Location = new Point(342, 27);
            lblRandomLabel36.Margin = new Padding(4, 0, 4, 0);
            lblRandomLabel36.Name = "lblRandomLabel36";
            lblRandomLabel36.Size = new Size(88, 15);
            lblRandomLabel36.TabIndex = 5;
            lblRandomLabel36.Text = "Player Switches";
            // 
            // btnRenameVariable
            // 
            btnRenameVariable.ForeColor = Color.Black;
            btnRenameVariable.Location = new Point(16, 393);
            btnRenameVariable.Margin = new Padding(4, 3, 4, 3);
            btnRenameVariable.Name = "btnRenameVariable";
            btnRenameVariable.Padding = new Padding(4, 3, 4, 3);
            btnRenameVariable.Size = new Size(124, 27);
            btnRenameVariable.TabIndex = 9;
            btnRenameVariable.Text = "Rename Variable";
            btnRenameVariable.Click += BtnRenameVariable_Click;
            // 
            // lblRandomLabel25
            // 
            lblRandomLabel25.AutoSize = true;
            lblRandomLabel25.ForeColor = Color.FromArgb(220, 220, 220);
            lblRandomLabel25.Location = new Point(93, 24);
            lblRandomLabel25.Margin = new Padding(4, 0, 4, 0);
            lblRandomLabel25.Name = "lblRandomLabel25";
            lblRandomLabel25.Size = new Size(88, 15);
            lblRandomLabel25.TabIndex = 4;
            lblRandomLabel25.Text = "Player Variables";
            // 
            // btnRenameSwitch
            // 
            btnRenameSwitch.ForeColor = Color.Black;
            btnRenameSwitch.Location = new Point(387, 393);
            btnRenameSwitch.Margin = new Padding(4, 3, 4, 3);
            btnRenameSwitch.Name = "btnRenameSwitch";
            btnRenameSwitch.Padding = new Padding(4, 3, 4, 3);
            btnRenameSwitch.Size = new Size(127, 27);
            btnRenameSwitch.TabIndex = 10;
            btnRenameSwitch.Text = "Rename Switch";
            btnRenameSwitch.Click += BtnRenameSwitch_Click;
            // 
            // btnLabel_Ok
            // 
            btnLabel_Ok.ForeColor = Color.Black;
            btnLabel_Ok.Location = new Point(168, 393);
            btnLabel_Ok.Margin = new Padding(4, 3, 4, 3);
            btnLabel_Ok.Name = "btnLabel_Ok";
            btnLabel_Ok.Padding = new Padding(4, 3, 4, 3);
            btnLabel_Ok.Size = new Size(88, 27);
            btnLabel_Ok.TabIndex = 11;
            btnLabel_Ok.Text = "Ok";
            btnLabel_Ok.Click += BtnLabel_Ok_Click;
            // 
            // frmEditor_Event
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1284, 712);
            Controls.Add(pnlVariableSwitches);
            Controls.Add(fraDialogue);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(btnLabeling);
            Controls.Add(tabPages);
            Controls.Add(fraPageSetUp);
            Controls.Add(pnlTabPage);
            Controls.Add(fraMoveRoute);
            Controls.Add(pnlGraphicSel);
            ForeColor = Color.Gainsboro;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            MaximumSize = new Size(1300, 751);
            MinimumSize = new Size(1300, 751);
            Name = "frmEditor_Event";
            Text = "Event Editor";
            Activated += frmEditor_Event_Activated;
            FormClosing += frmEditor_Events_FormClosing;
            Load += frmEditor_Events_Load;
            Resize += frmEditor_Event_Resize;
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
            fraCommands.ResumeLayout(false);
            fraGraphic.ResumeLayout(false);
            fraGraphic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudGraphic).EndInit();
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
        internal DarkUI.Controls.DarkGroupBox fraMoveRoute;
        internal DarkUI.Controls.DarkComboBox cmbEvent;
        internal ListBox lstMoveRoute;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox10;
        internal DarkUI.Controls.DarkListView lstvwMoveRoute;
        internal ColumnHeader ColumnHeader3;
        internal ColumnHeader ColumnHeader4;
        internal DarkUI.Controls.DarkCheckBox chkRepeatRoute;
        internal DarkUI.Controls.DarkCheckBox chkIgnoreMove;
        internal DarkUI.Controls.DarkButton btnMoveRouteOk;
        internal DarkUI.Controls.DarkButton btnMoveRouteCancel;
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
        internal DarkUI.Controls.DarkGroupBox fraMoveRouteWait;
        internal DarkUI.Controls.DarkButton btnMoveWaitCancel;
        internal DarkUI.Controls.DarkButton btnMoveWaitOk;
        internal DarkUI.Controls.DarkLabel DarkLabel79;
        internal DarkUI.Controls.DarkComboBox cmbMoveWait;
        internal Panel pnlVariableSwitches;
        internal DarkUI.Controls.DarkGroupBox fraLabeling;
        internal ListBox lstSwitches;
        internal ListBox lstVariables;
        internal DarkUI.Controls.DarkGroupBox FraRenaming;
        internal DarkUI.Controls.DarkButton btnRename_Cancel;
        internal DarkUI.Controls.DarkButton btnRename_Ok;
        internal DarkUI.Controls.DarkGroupBox fraRandom10;
        internal DarkUI.Controls.DarkTextBox txtRename;
        internal DarkUI.Controls.DarkLabel lblEditing;
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
        internal PictureBox picGraphicSel;
    }
}