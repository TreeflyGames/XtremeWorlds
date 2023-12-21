Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEditor_Events
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim TreeNode1 As TreeNode = New TreeNode("Show Text")
        Dim TreeNode2 As TreeNode = New TreeNode("Show Choices")
        Dim TreeNode3 As TreeNode = New TreeNode("Add Chatbox Text")
        Dim TreeNode4 As TreeNode = New TreeNode("Show ChatBubble")
        Dim TreeNode5 As TreeNode = New TreeNode("Messages", New TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4})
        Dim TreeNode6 As TreeNode = New TreeNode("Set Player Variable")
        Dim TreeNode7 As TreeNode = New TreeNode("Set Player Switch")
        Dim TreeNode8 As TreeNode = New TreeNode("Set Self Switch")
        Dim TreeNode9 As TreeNode = New TreeNode("Event Processing", New TreeNode() {TreeNode6, TreeNode7, TreeNode8})
        Dim TreeNode10 As TreeNode = New TreeNode("Conditional Branch")
        Dim TreeNode11 As TreeNode = New TreeNode("Stop Event Processing")
        Dim TreeNode12 As TreeNode = New TreeNode("Label")
        Dim TreeNode13 As TreeNode = New TreeNode("GoTo Label")
        Dim TreeNode14 As TreeNode = New TreeNode("Flow Control", New TreeNode() {TreeNode10, TreeNode11, TreeNode12, TreeNode13})
        Dim TreeNode15 As TreeNode = New TreeNode("Change Items")
        Dim TreeNode16 As TreeNode = New TreeNode("Restore HP")
        Dim TreeNode17 As TreeNode = New TreeNode("Restore MP")
        Dim TreeNode18 As TreeNode = New TreeNode("Level Up")
        Dim TreeNode19 As TreeNode = New TreeNode("Change Level")
        Dim TreeNode20 As TreeNode = New TreeNode("Change Skills")
        Dim TreeNode21 As TreeNode = New TreeNode("Change Job")
        Dim TreeNode22 As TreeNode = New TreeNode("Change Sprite")
        Dim TreeNode23 As TreeNode = New TreeNode("Change Gender")
        Dim TreeNode24 As TreeNode = New TreeNode("Change PK")
        Dim TreeNode25 As TreeNode = New TreeNode("Give Experience")
        Dim TreeNode26 As TreeNode = New TreeNode("Player Options", New TreeNode() {TreeNode15, TreeNode16, TreeNode17, TreeNode18, TreeNode19, TreeNode20, TreeNode21, TreeNode22, TreeNode23, TreeNode24, TreeNode25})
        Dim TreeNode27 As TreeNode = New TreeNode("Warp Player")
        Dim TreeNode28 As TreeNode = New TreeNode("Set Move Route")
        Dim TreeNode29 As TreeNode = New TreeNode("Wait for Route Completion")
        Dim TreeNode30 As TreeNode = New TreeNode("Force Spawn Npc")
        Dim TreeNode31 As TreeNode = New TreeNode("Hold Player")
        Dim TreeNode32 As TreeNode = New TreeNode("Release Player")
        Dim TreeNode33 As TreeNode = New TreeNode("Movement", New TreeNode() {TreeNode27, TreeNode28, TreeNode29, TreeNode30, TreeNode31, TreeNode32})
        Dim TreeNode34 As TreeNode = New TreeNode("Animation")
        Dim TreeNode35 As TreeNode = New TreeNode("Animation", New TreeNode() {TreeNode34})
        Dim TreeNode36 As TreeNode = New TreeNode("Begin Quest")
        Dim TreeNode37 As TreeNode = New TreeNode("Complete Task")
        Dim TreeNode38 As TreeNode = New TreeNode("End Quest")
        Dim TreeNode39 As TreeNode = New TreeNode("Questing", New TreeNode() {TreeNode36, TreeNode37, TreeNode38})
        Dim TreeNode40 As TreeNode = New TreeNode("Set Fog")
        Dim TreeNode41 As TreeNode = New TreeNode("Set Weather")
        Dim TreeNode42 As TreeNode = New TreeNode("Set Map Tinting")
        Dim TreeNode43 As TreeNode = New TreeNode("Map Functions", New TreeNode() {TreeNode40, TreeNode41, TreeNode42})
        Dim TreeNode44 As TreeNode = New TreeNode("Play BGM")
        Dim TreeNode45 As TreeNode = New TreeNode("Stop BGM")
        Dim TreeNode46 As TreeNode = New TreeNode("Play Sound")
        Dim TreeNode47 As TreeNode = New TreeNode("Stop Sounds")
        Dim TreeNode48 As TreeNode = New TreeNode("Music and Sound", New TreeNode() {TreeNode44, TreeNode45, TreeNode46, TreeNode47})
        Dim TreeNode49 As TreeNode = New TreeNode("Wait...")
        Dim TreeNode50 As TreeNode = New TreeNode("Set Access")
        Dim TreeNode51 As TreeNode = New TreeNode("Custom Script")
        Dim TreeNode52 As TreeNode = New TreeNode("Etc...", New TreeNode() {TreeNode49, TreeNode50, TreeNode51})
        Dim TreeNode53 As TreeNode = New TreeNode("Open Bank")
        Dim TreeNode54 As TreeNode = New TreeNode("Open Shop")
        Dim TreeNode55 As TreeNode = New TreeNode("Shop and Bank", New TreeNode() {TreeNode53, TreeNode54})
        Dim TreeNode56 As TreeNode = New TreeNode("Fade In")
        Dim TreeNode57 As TreeNode = New TreeNode("Fade Out")
        Dim TreeNode58 As TreeNode = New TreeNode("Flash White")
        Dim TreeNode59 As TreeNode = New TreeNode("Show Picture")
        Dim TreeNode60 As TreeNode = New TreeNode("Hide Picture")
        Dim TreeNode61 As TreeNode = New TreeNode("Cutscene Options", New TreeNode() {TreeNode56, TreeNode57, TreeNode58, TreeNode59, TreeNode60})
        Dim ListViewGroup1 As ListViewGroup = New ListViewGroup("Movement", HorizontalAlignment.Left)
        Dim ListViewGroup2 As ListViewGroup = New ListViewGroup("Wait", HorizontalAlignment.Left)
        Dim ListViewGroup3 As ListViewGroup = New ListViewGroup("Turning", HorizontalAlignment.Left)
        Dim ListViewGroup4 As ListViewGroup = New ListViewGroup("Speed", HorizontalAlignment.Left)
        Dim ListViewGroup5 As ListViewGroup = New ListViewGroup("Walk Animation", HorizontalAlignment.Left)
        Dim ListViewGroup6 As ListViewGroup = New ListViewGroup("Fixed Direction", HorizontalAlignment.Left)
        Dim ListViewGroup7 As ListViewGroup = New ListViewGroup("WalkThrough", HorizontalAlignment.Left)
        Dim ListViewGroup8 As ListViewGroup = New ListViewGroup("Set Position", HorizontalAlignment.Left)
        Dim ListViewGroup9 As ListViewGroup = New ListViewGroup("Set Graphic", HorizontalAlignment.Left)
        Dim ListViewItem1 As ListViewItem = New ListViewItem("Move Up")
        Dim ListViewItem2 As ListViewItem = New ListViewItem("Move Down")
        Dim ListViewItem3 As ListViewItem = New ListViewItem("Move left")
        Dim ListViewItem4 As ListViewItem = New ListViewItem("Move Right")
        Dim ListViewItem5 As ListViewItem = New ListViewItem("Move Randomly")
        Dim ListViewItem6 As ListViewItem = New ListViewItem("Move To Player***")
        Dim ListViewItem7 As ListViewItem = New ListViewItem("Move from Player***")
        Dim ListViewItem8 As ListViewItem = New ListViewItem("Step Forwards")
        Dim ListViewItem9 As ListViewItem = New ListViewItem("Step Backwards")
        Dim ListViewItem10 As ListViewItem = New ListViewItem("Wait 100Ms")
        Dim ListViewItem11 As ListViewItem = New ListViewItem("Wait 500Ms")
        Dim ListViewItem12 As ListViewItem = New ListViewItem("Wait 1000Ms")
        Dim ListViewItem13 As ListViewItem = New ListViewItem("Turn Up")
        Dim ListViewItem14 As ListViewItem = New ListViewItem("Turn Down")
        Dim ListViewItem15 As ListViewItem = New ListViewItem("Turn Left")
        Dim ListViewItem16 As ListViewItem = New ListViewItem("Turn Right")
        Dim ListViewItem17 As ListViewItem = New ListViewItem("Turn 90DG Right")
        Dim ListViewItem18 As ListViewItem = New ListViewItem("Turn 90DG Left")
        Dim ListViewItem19 As ListViewItem = New ListViewItem("Turn 180DG")
        Dim ListViewItem20 As ListViewItem = New ListViewItem("Turn Randomly")
        Dim ListViewItem21 As ListViewItem = New ListViewItem("Turn To Player***")
        Dim ListViewItem22 As ListViewItem = New ListViewItem("Turn From Player***")
        Dim ListViewItem23 As ListViewItem = New ListViewItem("Set Speed 8x Slower")
        Dim ListViewItem24 As ListViewItem = New ListViewItem("Set Speed 4x Slower")
        Dim ListViewItem25 As ListViewItem = New ListViewItem("Set Speed 2x Slower")
        Dim ListViewItem26 As ListViewItem = New ListViewItem("Set Speed To Normal")
        Dim ListViewItem27 As ListViewItem = New ListViewItem("Set Speed 2x Faster")
        Dim ListViewItem28 As ListViewItem = New ListViewItem("Set Speed 4x Faster")
        Dim ListViewItem29 As ListViewItem = New ListViewItem("Set Freq. To Lowest")
        Dim ListViewItem30 As ListViewItem = New ListViewItem("Set Freq. To Lower")
        Dim ListViewItem31 As ListViewItem = New ListViewItem("Set Freq. To Normal")
        Dim ListViewItem32 As ListViewItem = New ListViewItem("Set Freq. To Higher")
        Dim ListViewItem33 As ListViewItem = New ListViewItem("Set Freq. To Highest")
        Dim ListViewItem34 As ListViewItem = New ListViewItem("Walking Animation ON")
        Dim ListViewItem35 As ListViewItem = New ListViewItem("Walking Animation OFF")
        Dim ListViewItem36 As ListViewItem = New ListViewItem("Fixed Direction ON")
        Dim ListViewItem37 As ListViewItem = New ListViewItem("Fixed Direction OFF")
        Dim ListViewItem38 As ListViewItem = New ListViewItem("Walkthrough ON")
        Dim ListViewItem39 As ListViewItem = New ListViewItem("Walkthrough ON")
        Dim ListViewItem40 As ListViewItem = New ListViewItem("Set Position Below Player")
        Dim ListViewItem41 As ListViewItem = New ListViewItem("Set PositionWith Player")
        Dim ListViewItem42 As ListViewItem = New ListViewItem("Set Position Above Player")
        Dim ListViewItem43 As ListViewItem = New ListViewItem("Set Graphic...")
        tvCommands = New TreeView()
        fraPageSetUp = New DarkUI.Controls.DarkGroupBox()
        chkGlobal = New DarkUI.Controls.DarkCheckBox()
        btnClearPage = New DarkUI.Controls.DarkButton()
        btnDeletePage = New DarkUI.Controls.DarkButton()
        btnPastePage = New DarkUI.Controls.DarkButton()
        btnCopyPage = New DarkUI.Controls.DarkButton()
        btnNewPage = New DarkUI.Controls.DarkButton()
        txtName = New DarkUI.Controls.DarkTextBox()
        DarkLabel1 = New DarkUI.Controls.DarkLabel()
        tabPages = New TabControl()
        TabPage1 = New TabPage()
        pnlTabPage = New Panel()
        DarkGroupBox2 = New DarkUI.Controls.DarkGroupBox()
        cmbPositioning = New DarkUI.Controls.DarkComboBox()
        fraGraphicPic = New DarkUI.Controls.DarkGroupBox()
        picGraphic = New PictureBox()
        DarkGroupBox6 = New DarkUI.Controls.DarkGroupBox()
        chkShowName = New DarkUI.Controls.DarkCheckBox()
        chkWalkThrough = New DarkUI.Controls.DarkCheckBox()
        chkDirFix = New DarkUI.Controls.DarkCheckBox()
        chkWalkAnim = New DarkUI.Controls.DarkCheckBox()
        DarkGroupBox7 = New DarkUI.Controls.DarkGroupBox()
        DarkGroupBox5 = New DarkUI.Controls.DarkGroupBox()
        cmbTrigger = New DarkUI.Controls.DarkComboBox()
        DarkGroupBox4 = New DarkUI.Controls.DarkGroupBox()
        picGraphicSel = New PictureBox()
        DarkGroupBox3 = New DarkUI.Controls.DarkGroupBox()
        DarkLabel7 = New DarkUI.Controls.DarkLabel()
        cmbMoveFreq = New DarkUI.Controls.DarkComboBox()
        DarkLabel6 = New DarkUI.Controls.DarkLabel()
        cmbMoveSpeed = New DarkUI.Controls.DarkComboBox()
        btnMoveRoute = New DarkUI.Controls.DarkButton()
        cmbMoveType = New DarkUI.Controls.DarkComboBox()
        DarkLabel5 = New DarkUI.Controls.DarkLabel()
        DarkGroupBox1 = New DarkUI.Controls.DarkGroupBox()
        cmbSelfSwitchCompare = New DarkUI.Controls.DarkComboBox()
        DarkLabel4 = New DarkUI.Controls.DarkLabel()
        cmbSelfSwitch = New DarkUI.Controls.DarkComboBox()
        chkSelfSwitch = New DarkUI.Controls.DarkCheckBox()
        cmbHasItem = New DarkUI.Controls.DarkComboBox()
        chkHasItem = New DarkUI.Controls.DarkCheckBox()
        cmbPlayerSwitchCompare = New DarkUI.Controls.DarkComboBox()
        DarkLabel3 = New DarkUI.Controls.DarkLabel()
        cmbPlayerSwitch = New DarkUI.Controls.DarkComboBox()
        chkPlayerSwitch = New DarkUI.Controls.DarkCheckBox()
        nudPlayerVariable = New DarkUI.Controls.DarkNumericUpDown()
        cmbPlayervarCompare = New DarkUI.Controls.DarkComboBox()
        DarkLabel2 = New DarkUI.Controls.DarkLabel()
        cmbPlayerVar = New DarkUI.Controls.DarkComboBox()
        chkPlayerVar = New DarkUI.Controls.DarkCheckBox()
        fraGraphic = New DarkUI.Controls.DarkGroupBox()
        btnGraphicOk = New DarkUI.Controls.DarkButton()
        btnGraphicCancel = New DarkUI.Controls.DarkButton()
        DarkLabel13 = New DarkUI.Controls.DarkLabel()
        nudGraphic = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel12 = New DarkUI.Controls.DarkLabel()
        cmbGraphic = New DarkUI.Controls.DarkComboBox()
        DarkLabel11 = New DarkUI.Controls.DarkLabel()
        fraCommands = New Panel()
        btnCancelCommand = New DarkUI.Controls.DarkButton()
        lstCommands = New ListBox()
        DarkGroupBox8 = New DarkUI.Controls.DarkGroupBox()
        btnClearCommand = New DarkUI.Controls.DarkButton()
        btnDeleteCommand = New DarkUI.Controls.DarkButton()
        btnEditCommand = New DarkUI.Controls.DarkButton()
        btnAddCommand = New DarkUI.Controls.DarkButton()
        btnLabeling = New DarkUI.Controls.DarkButton()
        btnCancel = New DarkUI.Controls.DarkButton()
        btnOk = New DarkUI.Controls.DarkButton()
        fraMoveRoute = New DarkUI.Controls.DarkGroupBox()
        btnMoveRouteOk = New DarkUI.Controls.DarkButton()
        btnMoveRouteCancel = New DarkUI.Controls.DarkButton()
        chkRepeatRoute = New DarkUI.Controls.DarkCheckBox()
        chkIgnoreMove = New DarkUI.Controls.DarkCheckBox()
        DarkGroupBox10 = New DarkUI.Controls.DarkGroupBox()
        lstvwMoveRoute = New ListView()
        ColumnHeader3 = New ColumnHeader()
        ColumnHeader4 = New ColumnHeader()
        lstMoveRoute = New ListBox()
        cmbEvent = New DarkUI.Controls.DarkComboBox()
        pnlGraphicSel = New Panel()
        fraDialogue = New DarkUI.Controls.DarkGroupBox()
        fraConditionalBranch = New DarkUI.Controls.DarkGroupBox()
        cmbCondition_Time = New DarkUI.Controls.DarkComboBox()
        optCondition9 = New DarkUI.Controls.DarkRadioButton()
        btnConditionalBranchOk = New DarkUI.Controls.DarkButton()
        btnConditionalBranchCancel = New DarkUI.Controls.DarkButton()
        cmbCondition_Gender = New DarkUI.Controls.DarkComboBox()
        optCondition8 = New DarkUI.Controls.DarkRadioButton()
        cmbCondition_SelfSwitchCondition = New DarkUI.Controls.DarkComboBox()
        DarkLabel17 = New DarkUI.Controls.DarkLabel()
        cmbCondition_SelfSwitch = New DarkUI.Controls.DarkComboBox()
        optCondition6 = New DarkUI.Controls.DarkRadioButton()
        nudCondition_LevelAmount = New DarkUI.Controls.DarkNumericUpDown()
        optCondition5 = New DarkUI.Controls.DarkRadioButton()
        cmbCondition_LevelCompare = New DarkUI.Controls.DarkComboBox()
        cmbCondition_LearntSkill = New DarkUI.Controls.DarkComboBox()
        optCondition4 = New DarkUI.Controls.DarkRadioButton()
        cmbCondition_JobIs = New DarkUI.Controls.DarkComboBox()
        optCondition3 = New DarkUI.Controls.DarkRadioButton()
        nudCondition_HasItem = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel16 = New DarkUI.Controls.DarkLabel()
        cmbCondition_HasItem = New DarkUI.Controls.DarkComboBox()
        optCondition2 = New DarkUI.Controls.DarkRadioButton()
        optCondition1 = New DarkUI.Controls.DarkRadioButton()
        DarkLabel15 = New DarkUI.Controls.DarkLabel()
        cmbCondtion_PlayerSwitchCondition = New DarkUI.Controls.DarkComboBox()
        cmbCondition_PlayerSwitch = New DarkUI.Controls.DarkComboBox()
        nudCondition_PlayerVarCondition = New DarkUI.Controls.DarkNumericUpDown()
        cmbCondition_PlayerVarCompare = New DarkUI.Controls.DarkComboBox()
        DarkLabel14 = New DarkUI.Controls.DarkLabel()
        cmbCondition_PlayerVarIndex = New DarkUI.Controls.DarkComboBox()
        optCondition0 = New DarkUI.Controls.DarkRadioButton()
        fraMoveRouteWait = New DarkUI.Controls.DarkGroupBox()
        btnMoveWaitCancel = New DarkUI.Controls.DarkButton()
        btnMoveWaitOk = New DarkUI.Controls.DarkButton()
        DarkLabel79 = New DarkUI.Controls.DarkLabel()
        cmbMoveWait = New DarkUI.Controls.DarkComboBox()
        fraCustomScript = New DarkUI.Controls.DarkGroupBox()
        nudCustomScript = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel78 = New DarkUI.Controls.DarkLabel()
        btnCustomScriptCancel = New DarkUI.Controls.DarkButton()
        btnCustomScriptOk = New DarkUI.Controls.DarkButton()
        fraSetWeather = New DarkUI.Controls.DarkGroupBox()
        btnSetWeatherOk = New DarkUI.Controls.DarkButton()
        btnSetWeatherCancel = New DarkUI.Controls.DarkButton()
        DarkLabel76 = New DarkUI.Controls.DarkLabel()
        nudWeatherIntensity = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel75 = New DarkUI.Controls.DarkLabel()
        CmbWeather = New DarkUI.Controls.DarkComboBox()
        fraSpawnNpc = New DarkUI.Controls.DarkGroupBox()
        btnSpawnNpcOk = New DarkUI.Controls.DarkButton()
        btnSpawnNpcCancel = New DarkUI.Controls.DarkButton()
        cmbSpawnNpc = New DarkUI.Controls.DarkComboBox()
        fraGiveExp = New DarkUI.Controls.DarkGroupBox()
        btnGiveExpOk = New DarkUI.Controls.DarkButton()
        btnGiveExpCancel = New DarkUI.Controls.DarkButton()
        nudGiveExp = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel77 = New DarkUI.Controls.DarkLabel()
        fraEndQuest = New DarkUI.Controls.DarkGroupBox()
        btnEndQuestOk = New DarkUI.Controls.DarkButton()
        btnEndQuestCancel = New DarkUI.Controls.DarkButton()
        cmbEndQuest = New DarkUI.Controls.DarkComboBox()
        fraSetAccess = New DarkUI.Controls.DarkGroupBox()
        btnSetAccessOk = New DarkUI.Controls.DarkButton()
        btnSetAccessCancel = New DarkUI.Controls.DarkButton()
        cmbSetAccess = New DarkUI.Controls.DarkComboBox()
        fraOpenShop = New DarkUI.Controls.DarkGroupBox()
        btnOpenShopOk = New DarkUI.Controls.DarkButton()
        btnOpenShopCancel = New DarkUI.Controls.DarkButton()
        cmbOpenShop = New DarkUI.Controls.DarkComboBox()
        fraChangeLevel = New DarkUI.Controls.DarkGroupBox()
        btnChangeLevelOk = New DarkUI.Controls.DarkButton()
        btnChangeLevelCancel = New DarkUI.Controls.DarkButton()
        DarkLabel65 = New DarkUI.Controls.DarkLabel()
        nudChangeLevel = New DarkUI.Controls.DarkNumericUpDown()
        fraChangeGender = New DarkUI.Controls.DarkGroupBox()
        btnChangeGenderOk = New DarkUI.Controls.DarkButton()
        btnChangeGenderCancel = New DarkUI.Controls.DarkButton()
        optChangeSexFemale = New DarkUI.Controls.DarkRadioButton()
        optChangeSexMale = New DarkUI.Controls.DarkRadioButton()
        fraGoToLabel = New DarkUI.Controls.DarkGroupBox()
        btnGoToLabelOk = New DarkUI.Controls.DarkButton()
        btnGoToLabelCancel = New DarkUI.Controls.DarkButton()
        txtGotoLabel = New DarkUI.Controls.DarkTextBox()
        DarkLabel60 = New DarkUI.Controls.DarkLabel()
        fraShowChoices = New DarkUI.Controls.DarkGroupBox()
        txtChoices4 = New DarkUI.Controls.DarkTextBox()
        txtChoices3 = New DarkUI.Controls.DarkTextBox()
        txtChoices2 = New DarkUI.Controls.DarkTextBox()
        txtChoices1 = New DarkUI.Controls.DarkTextBox()
        DarkLabel56 = New DarkUI.Controls.DarkLabel()
        DarkLabel57 = New DarkUI.Controls.DarkLabel()
        DarkLabel55 = New DarkUI.Controls.DarkLabel()
        DarkLabel54 = New DarkUI.Controls.DarkLabel()
        DarkLabel52 = New DarkUI.Controls.DarkLabel()
        txtChoicePrompt = New DarkUI.Controls.DarkTextBox()
        btnShowChoicesOk = New DarkUI.Controls.DarkButton()
        picShowChoicesFace = New PictureBox()
        btnShowChoicesCancel = New DarkUI.Controls.DarkButton()
        DarkLabel53 = New DarkUI.Controls.DarkLabel()
        nudShowChoicesFace = New DarkUI.Controls.DarkNumericUpDown()
        fraPlayerVariable = New DarkUI.Controls.DarkGroupBox()
        nudVariableData2 = New DarkUI.Controls.DarkNumericUpDown()
        optVariableAction2 = New DarkUI.Controls.DarkRadioButton()
        btnPlayerVarOk = New DarkUI.Controls.DarkButton()
        btnPlayerVarCancel = New DarkUI.Controls.DarkButton()
        DarkLabel51 = New DarkUI.Controls.DarkLabel()
        DarkLabel50 = New DarkUI.Controls.DarkLabel()
        nudVariableData4 = New DarkUI.Controls.DarkNumericUpDown()
        nudVariableData3 = New DarkUI.Controls.DarkNumericUpDown()
        optVariableAction3 = New DarkUI.Controls.DarkRadioButton()
        optVariableAction1 = New DarkUI.Controls.DarkRadioButton()
        nudVariableData1 = New DarkUI.Controls.DarkNumericUpDown()
        nudVariableData0 = New DarkUI.Controls.DarkNumericUpDown()
        optVariableAction0 = New DarkUI.Controls.DarkRadioButton()
        cmbVariable = New DarkUI.Controls.DarkComboBox()
        DarkLabel49 = New DarkUI.Controls.DarkLabel()
        fraChangeSprite = New DarkUI.Controls.DarkGroupBox()
        btnChangeSpriteOk = New DarkUI.Controls.DarkButton()
        btnChangeSpriteCancel = New DarkUI.Controls.DarkButton()
        DarkLabel48 = New DarkUI.Controls.DarkLabel()
        nudChangeSprite = New DarkUI.Controls.DarkNumericUpDown()
        picChangeSprite = New PictureBox()
        fraSetSelfSwitch = New DarkUI.Controls.DarkGroupBox()
        btnSelfswitchOk = New DarkUI.Controls.DarkButton()
        btnSelfswitchCancel = New DarkUI.Controls.DarkButton()
        DarkLabel47 = New DarkUI.Controls.DarkLabel()
        cmbSetSelfSwitchTo = New DarkUI.Controls.DarkComboBox()
        DarkLabel46 = New DarkUI.Controls.DarkLabel()
        cmbSetSelfSwitch = New DarkUI.Controls.DarkComboBox()
        fraMapTint = New DarkUI.Controls.DarkGroupBox()
        btnMapTintOk = New DarkUI.Controls.DarkButton()
        btnMapTintCancel = New DarkUI.Controls.DarkButton()
        DarkLabel42 = New DarkUI.Controls.DarkLabel()
        nudMapTintData3 = New DarkUI.Controls.DarkNumericUpDown()
        nudMapTintData2 = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel43 = New DarkUI.Controls.DarkLabel()
        DarkLabel44 = New DarkUI.Controls.DarkLabel()
        nudMapTintData1 = New DarkUI.Controls.DarkNumericUpDown()
        nudMapTintData0 = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel45 = New DarkUI.Controls.DarkLabel()
        fraShowChatBubble = New DarkUI.Controls.DarkGroupBox()
        btnShowChatBubbleOk = New DarkUI.Controls.DarkButton()
        btnShowChatBubbleCancel = New DarkUI.Controls.DarkButton()
        DarkLabel41 = New DarkUI.Controls.DarkLabel()
        cmbChatBubbleTarget = New DarkUI.Controls.DarkComboBox()
        cmbChatBubbleTargetType = New DarkUI.Controls.DarkComboBox()
        DarkLabel40 = New DarkUI.Controls.DarkLabel()
        txtChatbubbleText = New DarkUI.Controls.DarkTextBox()
        DarkLabel39 = New DarkUI.Controls.DarkLabel()
        fraPlaySound = New DarkUI.Controls.DarkGroupBox()
        btnPlaySoundOk = New DarkUI.Controls.DarkButton()
        btnPlaySoundCancel = New DarkUI.Controls.DarkButton()
        cmbPlaySound = New DarkUI.Controls.DarkComboBox()
        fraChangePK = New DarkUI.Controls.DarkGroupBox()
        btnChangePkOk = New DarkUI.Controls.DarkButton()
        btnChangePkCancel = New DarkUI.Controls.DarkButton()
        cmbSetPK = New DarkUI.Controls.DarkComboBox()
        fraCreateLabel = New DarkUI.Controls.DarkGroupBox()
        btnCreatelabelOk = New DarkUI.Controls.DarkButton()
        btnCreatelabelCancel = New DarkUI.Controls.DarkButton()
        txtLabelName = New DarkUI.Controls.DarkTextBox()
        lblLabelName = New DarkUI.Controls.DarkLabel()
        fraChangeJob = New DarkUI.Controls.DarkGroupBox()
        btnChangeJobOk = New DarkUI.Controls.DarkButton()
        btnChangeJobCancel = New DarkUI.Controls.DarkButton()
        cmbChangeJob = New DarkUI.Controls.DarkComboBox()
        DarkLabel38 = New DarkUI.Controls.DarkLabel()
        fraChangeSkills = New DarkUI.Controls.DarkGroupBox()
        btnChangeSkillsOk = New DarkUI.Controls.DarkButton()
        btnChangeSkillsCancel = New DarkUI.Controls.DarkButton()
        optChangeSkillsRemove = New DarkUI.Controls.DarkRadioButton()
        optChangeSkillsAdd = New DarkUI.Controls.DarkRadioButton()
        cmbChangeSkills = New DarkUI.Controls.DarkComboBox()
        DarkLabel37 = New DarkUI.Controls.DarkLabel()
        fraPlayerWarp = New DarkUI.Controls.DarkGroupBox()
        btnPlayerWarpOk = New DarkUI.Controls.DarkButton()
        btnPlayerWarpCancel = New DarkUI.Controls.DarkButton()
        DarkLabel31 = New DarkUI.Controls.DarkLabel()
        cmbWarpPlayerDir = New DarkUI.Controls.DarkComboBox()
        nudWPY = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel32 = New DarkUI.Controls.DarkLabel()
        nudWPX = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel33 = New DarkUI.Controls.DarkLabel()
        nudWPMap = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel34 = New DarkUI.Controls.DarkLabel()
        fraSetFog = New DarkUI.Controls.DarkGroupBox()
        btnSetFogOk = New DarkUI.Controls.DarkButton()
        btnSetFogCancel = New DarkUI.Controls.DarkButton()
        DarkLabel30 = New DarkUI.Controls.DarkLabel()
        DarkLabel29 = New DarkUI.Controls.DarkLabel()
        DarkLabel28 = New DarkUI.Controls.DarkLabel()
        nudFogData2 = New DarkUI.Controls.DarkNumericUpDown()
        nudFogData1 = New DarkUI.Controls.DarkNumericUpDown()
        nudFogData0 = New DarkUI.Controls.DarkNumericUpDown()
        fraAddText = New DarkUI.Controls.DarkGroupBox()
        btnAddTextOk = New DarkUI.Controls.DarkButton()
        btnAddTextCancel = New DarkUI.Controls.DarkButton()
        optAddText_Global = New DarkUI.Controls.DarkRadioButton()
        optAddText_Map = New DarkUI.Controls.DarkRadioButton()
        optAddText_Player = New DarkUI.Controls.DarkRadioButton()
        DarkLabel25 = New DarkUI.Controls.DarkLabel()
        txtAddText_Text = New DarkUI.Controls.DarkTextBox()
        DarkLabel24 = New DarkUI.Controls.DarkLabel()
        fraPlayerSwitch = New DarkUI.Controls.DarkGroupBox()
        btnSetPlayerSwitchOk = New DarkUI.Controls.DarkButton()
        btnSetPlayerswitchCancel = New DarkUI.Controls.DarkButton()
        cmbPlayerSwitchSet = New DarkUI.Controls.DarkComboBox()
        DarkLabel23 = New DarkUI.Controls.DarkLabel()
        cmbSwitch = New DarkUI.Controls.DarkComboBox()
        DarkLabel22 = New DarkUI.Controls.DarkLabel()
        fraChangeItems = New DarkUI.Controls.DarkGroupBox()
        btnChangeItemsOk = New DarkUI.Controls.DarkButton()
        btnChangeItemsCancel = New DarkUI.Controls.DarkButton()
        nudChangeItemsAmount = New DarkUI.Controls.DarkNumericUpDown()
        optChangeItemRemove = New DarkUI.Controls.DarkRadioButton()
        optChangeItemAdd = New DarkUI.Controls.DarkRadioButton()
        optChangeItemSet = New DarkUI.Controls.DarkRadioButton()
        cmbChangeItemIndex = New DarkUI.Controls.DarkComboBox()
        DarkLabel21 = New DarkUI.Controls.DarkLabel()
        fraPlayBGM = New DarkUI.Controls.DarkGroupBox()
        btnPlayBgmOk = New DarkUI.Controls.DarkButton()
        btnPlayBgmCancel = New DarkUI.Controls.DarkButton()
        cmbPlayBGM = New DarkUI.Controls.DarkComboBox()
        fraPlayAnimation = New DarkUI.Controls.DarkGroupBox()
        btnPlayAnimationOk = New DarkUI.Controls.DarkButton()
        btnPlayAnimationCancel = New DarkUI.Controls.DarkButton()
        lblPlayAnimY = New DarkUI.Controls.DarkLabel()
        lblPlayAnimX = New DarkUI.Controls.DarkLabel()
        cmbPlayAnimEvent = New DarkUI.Controls.DarkComboBox()
        DarkLabel62 = New DarkUI.Controls.DarkLabel()
        cmbAnimTargetType = New DarkUI.Controls.DarkComboBox()
        nudPlayAnimTileY = New DarkUI.Controls.DarkNumericUpDown()
        nudPlayAnimTileX = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel61 = New DarkUI.Controls.DarkLabel()
        cmbPlayAnim = New DarkUI.Controls.DarkComboBox()
        fraSetWait = New DarkUI.Controls.DarkGroupBox()
        btnSetWaitOk = New DarkUI.Controls.DarkButton()
        btnSetWaitCancel = New DarkUI.Controls.DarkButton()
        DarkLabel74 = New DarkUI.Controls.DarkLabel()
        DarkLabel72 = New DarkUI.Controls.DarkLabel()
        DarkLabel73 = New DarkUI.Controls.DarkLabel()
        nudWaitAmount = New DarkUI.Controls.DarkNumericUpDown()
        fraShowPic = New DarkUI.Controls.DarkGroupBox()
        btnShowPicOk = New DarkUI.Controls.DarkButton()
        btnShowPicCancel = New DarkUI.Controls.DarkButton()
        DarkLabel71 = New DarkUI.Controls.DarkLabel()
        DarkLabel70 = New DarkUI.Controls.DarkLabel()
        DarkLabel67 = New DarkUI.Controls.DarkLabel()
        DarkLabel68 = New DarkUI.Controls.DarkLabel()
        nudPicOffsetY = New DarkUI.Controls.DarkNumericUpDown()
        nudPicOffsetX = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel69 = New DarkUI.Controls.DarkLabel()
        cmbPicLoc = New DarkUI.Controls.DarkComboBox()
        nudShowPicture = New DarkUI.Controls.DarkNumericUpDown()
        picShowPic = New PictureBox()
        fraShowText = New DarkUI.Controls.DarkGroupBox()
        DarkLabel27 = New DarkUI.Controls.DarkLabel()
        txtShowText = New DarkUI.Controls.DarkTextBox()
        btnShowTextCancel = New DarkUI.Controls.DarkButton()
        btnShowTextOk = New DarkUI.Controls.DarkButton()
        picShowTextFace = New PictureBox()
        DarkLabel26 = New DarkUI.Controls.DarkLabel()
        nudShowTextFace = New DarkUI.Controls.DarkNumericUpDown()
        pnlVariableSwitches = New Panel()
        FraRenaming = New GroupBox()
        btnRename_Cancel = New Button()
        btnRename_Ok = New Button()
        fraRandom10 = New GroupBox()
        txtRename = New TextBox()
        lblEditing = New Label()
        fraLabeling = New DarkUI.Controls.DarkGroupBox()
        lstSwitches = New ListBox()
        lstVariables = New ListBox()
        btnLabel_Cancel = New Button()
        lblRandomLabel36 = New Label()
        btnRenameVariable = New Button()
        lblRandomLabel25 = New Label()
        btnRenameSwitch = New Button()
        btnLabel_Ok = New Button()
        fraPageSetUp.SuspendLayout()
        tabPages.SuspendLayout()
        pnlTabPage.SuspendLayout()
        DarkGroupBox2.SuspendLayout()
        fraGraphicPic.SuspendLayout()
        CType(picGraphic, System.ComponentModel.ISupportInitialize).BeginInit()
        DarkGroupBox6.SuspendLayout()
        DarkGroupBox5.SuspendLayout()
        DarkGroupBox4.SuspendLayout()
        CType(picGraphicSel, System.ComponentModel.ISupportInitialize).BeginInit()
        DarkGroupBox3.SuspendLayout()
        DarkGroupBox1.SuspendLayout()
        CType(nudPlayerVariable, System.ComponentModel.ISupportInitialize).BeginInit()
        fraGraphic.SuspendLayout()
        CType(nudGraphic, System.ComponentModel.ISupportInitialize).BeginInit()
        fraCommands.SuspendLayout()
        DarkGroupBox8.SuspendLayout()
        fraMoveRoute.SuspendLayout()
        DarkGroupBox10.SuspendLayout()
        fraDialogue.SuspendLayout()
        fraConditionalBranch.SuspendLayout()
        CType(nudCondition_LevelAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudCondition_HasItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudCondition_PlayerVarCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        fraMoveRouteWait.SuspendLayout()
        fraCustomScript.SuspendLayout()
        CType(nudCustomScript, System.ComponentModel.ISupportInitialize).BeginInit()
        fraSetWeather.SuspendLayout()
        CType(nudWeatherIntensity, System.ComponentModel.ISupportInitialize).BeginInit()
        fraSpawnNpc.SuspendLayout()
        fraGiveExp.SuspendLayout()
        CType(nudGiveExp, System.ComponentModel.ISupportInitialize).BeginInit()
        fraEndQuest.SuspendLayout()
        fraSetAccess.SuspendLayout()
        fraOpenShop.SuspendLayout()
        fraChangeLevel.SuspendLayout()
        CType(nudChangeLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        fraChangeGender.SuspendLayout()
        fraGoToLabel.SuspendLayout()
        fraShowChoices.SuspendLayout()
        CType(picShowChoicesFace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudShowChoicesFace, System.ComponentModel.ISupportInitialize).BeginInit()
        fraPlayerVariable.SuspendLayout()
        CType(nudVariableData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudVariableData4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudVariableData3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudVariableData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudVariableData0, System.ComponentModel.ISupportInitialize).BeginInit()
        fraChangeSprite.SuspendLayout()
        CType(nudChangeSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(picChangeSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        fraSetSelfSwitch.SuspendLayout()
        fraMapTint.SuspendLayout()
        CType(nudMapTintData3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudMapTintData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudMapTintData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudMapTintData0, System.ComponentModel.ISupportInitialize).BeginInit()
        fraShowChatBubble.SuspendLayout()
        fraPlaySound.SuspendLayout()
        fraChangePK.SuspendLayout()
        fraCreateLabel.SuspendLayout()
        fraChangeJob.SuspendLayout()
        fraChangeSkills.SuspendLayout()
        fraPlayerWarp.SuspendLayout()
        CType(nudWPY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudWPX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudWPMap, System.ComponentModel.ISupportInitialize).BeginInit()
        fraSetFog.SuspendLayout()
        CType(nudFogData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudFogData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudFogData0, System.ComponentModel.ISupportInitialize).BeginInit()
        fraAddText.SuspendLayout()
        fraPlayerSwitch.SuspendLayout()
        fraChangeItems.SuspendLayout()
        CType(nudChangeItemsAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        fraPlayBGM.SuspendLayout()
        fraPlayAnimation.SuspendLayout()
        CType(nudPlayAnimTileY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPlayAnimTileX, System.ComponentModel.ISupportInitialize).BeginInit()
        fraSetWait.SuspendLayout()
        CType(nudWaitAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        fraShowPic.SuspendLayout()
        CType(nudPicOffsetY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPicOffsetX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudShowPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(picShowPic, System.ComponentModel.ISupportInitialize).BeginInit()
        fraShowText.SuspendLayout()
        CType(picShowTextFace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudShowTextFace, System.ComponentModel.ISupportInitialize).BeginInit()
        pnlVariableSwitches.SuspendLayout()
        FraRenaming.SuspendLayout()
        fraRandom10.SuspendLayout()
        fraLabeling.SuspendLayout()
        SuspendLayout()
        ' 
        ' tvCommands
        ' 
        tvCommands.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        tvCommands.BorderStyle = BorderStyle.FixedSingle
        tvCommands.ForeColor = Color.Gainsboro
        tvCommands.Location = New Point(13, 6)
        tvCommands.Margin = New Padding(7, 6, 7, 6)
        tvCommands.Name = "tvCommands"
        TreeNode1.Name = "Node1"
        TreeNode1.Text = "Show Text"
        TreeNode2.Name = "Node2"
        TreeNode2.Text = "Show Choices"
        TreeNode3.Name = "Node3"
        TreeNode3.Text = "Add Chatbox Text"
        TreeNode4.Name = "Node5"
        TreeNode4.Text = "Show ChatBubble"
        TreeNode5.Name = "NodeMessages"
        TreeNode5.Text = "Messages"
        TreeNode6.Name = "Node1"
        TreeNode6.Text = "Set Player Variable"
        TreeNode7.Name = "Node2"
        TreeNode7.Text = "Set Player Switch"
        TreeNode8.Name = "Node3"
        TreeNode8.Text = "Set Self Switch"
        TreeNode9.Name = "NodeProcessing"
        TreeNode9.Text = "Event Processing"
        TreeNode10.Name = "Node1"
        TreeNode10.Text = "Conditional Branch"
        TreeNode11.Name = "Node2"
        TreeNode11.Text = "Stop Event Processing"
        TreeNode12.Name = "Node3"
        TreeNode12.Text = "Label"
        TreeNode13.Name = "Node4"
        TreeNode13.Text = "GoTo Label"
        TreeNode14.Name = "NodeFlowControl"
        TreeNode14.Text = "Flow Control"
        TreeNode15.Name = "Node1"
        TreeNode15.Text = "Change Items"
        TreeNode16.Name = "Node2"
        TreeNode16.Text = "Restore HP"
        TreeNode17.Name = "Node3"
        TreeNode17.Text = "Restore MP"
        TreeNode18.Name = "Node4"
        TreeNode18.Text = "Level Up"
        TreeNode19.Name = "Node5"
        TreeNode19.Text = "Change Level"
        TreeNode20.Name = "Node6"
        TreeNode20.Text = "Change Skills"
        TreeNode21.Name = "Node7"
        TreeNode21.Text = "Change Job"
        TreeNode22.Name = "Node8"
        TreeNode22.Text = "Change Sprite"
        TreeNode23.Name = "Node9"
        TreeNode23.Text = "Change Gender"
        TreeNode24.Name = "Node10"
        TreeNode24.Text = "Change PK"
        TreeNode25.Name = "Node11"
        TreeNode25.Text = "Give Experience"
        TreeNode26.Name = "NodePlayerOptions"
        TreeNode26.Text = "Player Options"
        TreeNode27.Name = "Node1"
        TreeNode27.Text = "Warp Player"
        TreeNode28.Name = "Node2"
        TreeNode28.Text = "Set Move Route"
        TreeNode29.Name = "Node3"
        TreeNode29.Text = "Wait for Route Completion"
        TreeNode30.Name = "Node4"
        TreeNode30.Text = "Force Spawn Npc"
        TreeNode31.Name = "Node5"
        TreeNode31.Text = "Hold Player"
        TreeNode32.Name = "Node6"
        TreeNode32.Text = "Release Player"
        TreeNode33.Name = "NodeMovement"
        TreeNode33.Text = "Movement"
        TreeNode34.Name = "Node1"
        TreeNode34.Text = "Animation"
        TreeNode35.Name = "NodeAnimation"
        TreeNode35.Text = "Animation"
        TreeNode36.Name = "Node1"
        TreeNode36.Text = "Begin Quest"
        TreeNode37.Name = "Node2"
        TreeNode37.Text = "Complete Task"
        TreeNode38.Name = "Node3"
        TreeNode38.Text = "End Quest"
        TreeNode39.Name = "NodeQuesting"
        TreeNode39.Text = "Questing"
        TreeNode40.Name = "Node1"
        TreeNode40.Text = "Set Fog"
        TreeNode41.Name = "Node2"
        TreeNode41.Text = "Set Weather"
        TreeNode42.Name = "Node3"
        TreeNode42.Text = "Set Map Tinting"
        TreeNode43.Name = "NodeMapFunctions"
        TreeNode43.Text = "Map Functions"
        TreeNode44.Name = "Node1"
        TreeNode44.Text = "Play BGM"
        TreeNode45.Name = "Node2"
        TreeNode45.Text = "Stop BGM"
        TreeNode46.Name = "Node3"
        TreeNode46.Text = "Play Sound"
        TreeNode47.Name = "Node4"
        TreeNode47.Text = "Stop Sounds"
        TreeNode48.Name = "NodeSound"
        TreeNode48.Text = "Music and Sound"
        TreeNode49.Name = "Node1"
        TreeNode49.Text = "Wait..."
        TreeNode50.Name = "Node2"
        TreeNode50.Text = "Set Access"
        TreeNode51.Name = "Node3"
        TreeNode51.Text = "Custom Script"
        TreeNode52.Name = "NodeEtc"
        TreeNode52.Text = "Etc..."
        TreeNode53.Name = "Node1"
        TreeNode53.Text = "Open Bank"
        TreeNode54.Name = "Node2"
        TreeNode54.Text = "Open Shop"
        TreeNode55.Name = "NodeShopBank"
        TreeNode55.Text = "Shop and Bank"
        TreeNode56.Name = "Node1"
        TreeNode56.Text = "Fade In"
        TreeNode57.Name = "Node2"
        TreeNode57.Text = "Fade Out"
        TreeNode58.Name = "Node12"
        TreeNode58.Text = "Flash White"
        TreeNode59.Name = "Node13"
        TreeNode59.Text = "Show Picture"
        TreeNode60.Name = "Node14"
        TreeNode60.Text = "Hide Picture"
        TreeNode61.Name = "Node0"
        TreeNode61.Text = "Cutscene Options"
        tvCommands.Nodes.AddRange(New TreeNode() {TreeNode5, TreeNode9, TreeNode14, TreeNode26, TreeNode33, TreeNode35, TreeNode39, TreeNode43, TreeNode48, TreeNode52, TreeNode55, TreeNode61})
        tvCommands.Size = New Size(823, 1088)
        tvCommands.TabIndex = 1
        ' 
        ' fraPageSetUp
        ' 
        fraPageSetUp.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraPageSetUp.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraPageSetUp.Controls.Add(chkGlobal)
        fraPageSetUp.Controls.Add(btnClearPage)
        fraPageSetUp.Controls.Add(btnDeletePage)
        fraPageSetUp.Controls.Add(btnPastePage)
        fraPageSetUp.Controls.Add(btnCopyPage)
        fraPageSetUp.Controls.Add(btnNewPage)
        fraPageSetUp.Controls.Add(txtName)
        fraPageSetUp.Controls.Add(DarkLabel1)
        fraPageSetUp.ForeColor = Color.Gainsboro
        fraPageSetUp.Location = New Point(7, 6)
        fraPageSetUp.Margin = New Padding(7, 6, 7, 6)
        fraPageSetUp.Name = "fraPageSetUp"
        fraPageSetUp.Padding = New Padding(7, 6, 7, 6)
        fraPageSetUp.Size = New Size(1714, 124)
        fraPageSetUp.TabIndex = 2
        fraPageSetUp.TabStop = False
        fraPageSetUp.Text = "General"
        ' 
        ' chkGlobal
        ' 
        chkGlobal.AutoSize = True
        chkGlobal.Location = New Point(607, 49)
        chkGlobal.Margin = New Padding(7, 6, 7, 6)
        chkGlobal.Name = "chkGlobal"
        chkGlobal.Size = New Size(180, 36)
        chkGlobal.TabIndex = 7
        chkGlobal.Text = "Global Event"
        ' 
        ' btnClearPage
        ' 
        btnClearPage.Location = New Point(1532, 38)
        btnClearPage.Margin = New Padding(7, 6, 7, 6)
        btnClearPage.Name = "btnClearPage"
        btnClearPage.Padding = New Padding(11, 13, 11, 13)
        btnClearPage.Size = New Size(163, 58)
        btnClearPage.TabIndex = 6
        btnClearPage.Text = "Clear Page"
        ' 
        ' btnDeletePage
        ' 
        btnDeletePage.Location = New Point(1348, 38)
        btnDeletePage.Margin = New Padding(7, 6, 7, 6)
        btnDeletePage.Name = "btnDeletePage"
        btnDeletePage.Padding = New Padding(11, 13, 11, 13)
        btnDeletePage.Size = New Size(171, 58)
        btnDeletePage.TabIndex = 5
        btnDeletePage.Text = "Delete Page"
        ' 
        ' btnPastePage
        ' 
        btnPastePage.Location = New Point(1172, 38)
        btnPastePage.Margin = New Padding(7, 6, 7, 6)
        btnPastePage.Name = "btnPastePage"
        btnPastePage.Padding = New Padding(11, 13, 11, 13)
        btnPastePage.Size = New Size(163, 58)
        btnPastePage.TabIndex = 4
        btnPastePage.Text = "Paste Page"
        ' 
        ' btnCopyPage
        ' 
        btnCopyPage.Location = New Point(997, 38)
        btnCopyPage.Margin = New Padding(7, 6, 7, 6)
        btnCopyPage.Name = "btnCopyPage"
        btnCopyPage.Padding = New Padding(11, 13, 11, 13)
        btnCopyPage.Size = New Size(163, 58)
        btnCopyPage.TabIndex = 3
        btnCopyPage.Text = "Copy Page"
        ' 
        ' btnNewPage
        ' 
        btnNewPage.Location = New Point(821, 38)
        btnNewPage.Margin = New Padding(7, 6, 7, 6)
        btnNewPage.Name = "btnNewPage"
        btnNewPage.Padding = New Padding(11, 13, 11, 13)
        btnNewPage.Size = New Size(163, 58)
        btnNewPage.TabIndex = 2
        btnNewPage.Text = "New Page"
        ' 
        ' txtName
        ' 
        txtName.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtName.BorderStyle = BorderStyle.FixedSingle
        txtName.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtName.Location = New Point(182, 47)
        txtName.Margin = New Padding(7, 6, 7, 6)
        txtName.Name = "txtName"
        txtName.Size = New Size(409, 39)
        txtName.TabIndex = 1
        ' 
        ' DarkLabel1
        ' 
        DarkLabel1.AutoSize = True
        DarkLabel1.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel1.Location = New Point(19, 51)
        DarkLabel1.Margin = New Padding(7, 0, 7, 0)
        DarkLabel1.Name = "DarkLabel1"
        DarkLabel1.Size = New Size(149, 32)
        DarkLabel1.TabIndex = 0
        DarkLabel1.Text = "Event Name:"
        ' 
        ' tabPages
        ' 
        tabPages.Controls.Add(TabPage1)
        tabPages.Location = New Point(26, 145)
        tabPages.Margin = New Padding(7, 6, 7, 6)
        tabPages.Name = "tabPages"
        tabPages.SelectedIndex = 0
        tabPages.Size = New Size(1536, 47)
        tabPages.TabIndex = 3
        ' 
        ' TabPage1
        ' 
        TabPage1.BackColor = Color.DimGray
        TabPage1.Location = New Point(8, 46)
        TabPage1.Margin = New Padding(7, 6, 7, 6)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(7, 6, 7, 6)
        TabPage1.Size = New Size(1520, 0)
        TabPage1.TabIndex = 0
        TabPage1.Text = "1"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' pnlTabPage
        ' 
        pnlTabPage.Controls.Add(DarkGroupBox2)
        pnlTabPage.Controls.Add(fraGraphicPic)
        pnlTabPage.Controls.Add(DarkGroupBox6)
        pnlTabPage.Controls.Add(DarkGroupBox7)
        pnlTabPage.Controls.Add(DarkGroupBox5)
        pnlTabPage.Controls.Add(DarkGroupBox4)
        pnlTabPage.Controls.Add(DarkGroupBox3)
        pnlTabPage.Controls.Add(DarkGroupBox1)
        pnlTabPage.Controls.Add(fraGraphic)
        pnlTabPage.Controls.Add(fraCommands)
        pnlTabPage.Controls.Add(lstCommands)
        pnlTabPage.Controls.Add(DarkGroupBox8)
        pnlTabPage.Location = New Point(7, 198)
        pnlTabPage.Margin = New Padding(7, 6, 7, 6)
        pnlTabPage.Name = "pnlTabPage"
        pnlTabPage.Size = New Size(1714, 1222)
        pnlTabPage.TabIndex = 4
        ' 
        ' DarkGroupBox2
        ' 
        DarkGroupBox2.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox2.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox2.Controls.Add(cmbPositioning)
        DarkGroupBox2.ForeColor = Color.Gainsboro
        DarkGroupBox2.Location = New Point(396, 939)
        DarkGroupBox2.Margin = New Padding(7, 6, 7, 6)
        DarkGroupBox2.Name = "DarkGroupBox2"
        DarkGroupBox2.Padding = New Padding(7, 6, 7, 6)
        DarkGroupBox2.Size = New Size(433, 122)
        DarkGroupBox2.TabIndex = 15
        DarkGroupBox2.TabStop = False
        DarkGroupBox2.Text = "Poisition"
        ' 
        ' cmbPositioning
        ' 
        cmbPositioning.DrawMode = DrawMode.OwnerDrawFixed
        cmbPositioning.FormattingEnabled = True
        cmbPositioning.Items.AddRange(New Object() {"Below Characters", "Same as Characters", "Above Characters"})
        cmbPositioning.Location = New Point(15, 47)
        cmbPositioning.Margin = New Padding(7, 6, 7, 6)
        cmbPositioning.Name = "cmbPositioning"
        cmbPositioning.Size = New Size(405, 40)
        cmbPositioning.TabIndex = 1
        ' 
        ' fraGraphicPic
        ' 
        fraGraphicPic.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraGraphicPic.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraGraphicPic.Controls.Add(picGraphic)
        fraGraphicPic.ForeColor = Color.Gainsboro
        fraGraphicPic.Location = New Point(7, 333)
        fraGraphicPic.Margin = New Padding(7, 6, 7, 6)
        fraGraphicPic.Name = "fraGraphicPic"
        fraGraphicPic.Padding = New Padding(7, 6, 7, 6)
        fraGraphicPic.Size = New Size(375, 572)
        fraGraphicPic.TabIndex = 12
        fraGraphicPic.TabStop = False
        fraGraphicPic.Text = "Graphic"
        ' 
        ' picGraphic
        ' 
        picGraphic.BackgroundImageLayout = ImageLayout.None
        picGraphic.Location = New Point(13, 47)
        picGraphic.Margin = New Padding(7, 6, 7, 6)
        picGraphic.Name = "picGraphic"
        picGraphic.Size = New Size(349, 510)
        picGraphic.TabIndex = 1
        picGraphic.TabStop = False
        ' 
        ' DarkGroupBox6
        ' 
        DarkGroupBox6.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox6.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox6.Controls.Add(chkShowName)
        DarkGroupBox6.Controls.Add(chkWalkThrough)
        DarkGroupBox6.Controls.Add(chkDirFix)
        DarkGroupBox6.Controls.Add(chkWalkAnim)
        DarkGroupBox6.ForeColor = Color.Gainsboro
        DarkGroupBox6.Location = New Point(7, 917)
        DarkGroupBox6.Margin = New Padding(7, 6, 7, 6)
        DarkGroupBox6.Name = "DarkGroupBox6"
        DarkGroupBox6.Padding = New Padding(7, 6, 7, 6)
        DarkGroupBox6.Size = New Size(381, 275)
        DarkGroupBox6.TabIndex = 10
        DarkGroupBox6.TabStop = False
        DarkGroupBox6.Text = "Options"
        ' 
        ' chkShowName
        ' 
        chkShowName.AutoSize = True
        chkShowName.Location = New Point(15, 218)
        chkShowName.Margin = New Padding(7, 6, 7, 6)
        chkShowName.Name = "chkShowName"
        chkShowName.Size = New Size(175, 36)
        chkShowName.TabIndex = 3
        chkShowName.Text = "Show Name"
        ' 
        ' chkWalkThrough
        ' 
        chkWalkThrough.AutoSize = True
        chkWalkThrough.Location = New Point(15, 160)
        chkWalkThrough.Margin = New Padding(7, 6, 7, 6)
        chkWalkThrough.Name = "chkWalkThrough"
        chkWalkThrough.Size = New Size(195, 36)
        chkWalkThrough.TabIndex = 2
        chkWalkThrough.Text = "Walk Through"
        ' 
        ' chkDirFix
        ' 
        chkDirFix.AutoSize = True
        chkDirFix.Location = New Point(15, 102)
        chkDirFix.Margin = New Padding(7, 6, 7, 6)
        chkDirFix.Name = "chkDirFix"
        chkDirFix.Size = New Size(206, 36)
        chkDirFix.TabIndex = 1
        chkDirFix.Text = "Direction Fixed"
        ' 
        ' chkWalkAnim
        ' 
        chkWalkAnim.AutoSize = True
        chkWalkAnim.Location = New Point(15, 47)
        chkWalkAnim.Margin = New Padding(7, 6, 7, 6)
        chkWalkAnim.Name = "chkWalkAnim"
        chkWalkAnim.Size = New Size(253, 36)
        chkWalkAnim.TabIndex = 0
        chkWalkAnim.Text = "No Walk Animation"
        ' 
        ' DarkGroupBox7
        ' 
        DarkGroupBox7.BorderColor = Color.FromArgb(CByte(51), CByte(51), CByte(51))
        DarkGroupBox7.Location = New Point(0, 0)
        DarkGroupBox7.Margin = New Padding(6, 6, 6, 6)
        DarkGroupBox7.Name = "DarkGroupBox7"
        DarkGroupBox7.Padding = New Padding(6, 6, 6, 6)
        DarkGroupBox7.Size = New Size(371, 213)
        DarkGroupBox7.TabIndex = 16
        DarkGroupBox7.TabStop = False
        ' 
        ' DarkGroupBox5
        ' 
        DarkGroupBox5.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox5.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox5.Controls.Add(cmbTrigger)
        DarkGroupBox5.ForeColor = Color.Gainsboro
        DarkGroupBox5.Location = New Point(403, 798)
        DarkGroupBox5.Margin = New Padding(7, 6, 7, 6)
        DarkGroupBox5.Name = "DarkGroupBox5"
        DarkGroupBox5.Padding = New Padding(7, 6, 7, 6)
        DarkGroupBox5.Size = New Size(433, 122)
        DarkGroupBox5.TabIndex = 4
        DarkGroupBox5.TabStop = False
        DarkGroupBox5.Text = "Trigger"
        ' 
        ' cmbTrigger
        ' 
        cmbTrigger.DrawMode = DrawMode.OwnerDrawFixed
        cmbTrigger.FormattingEnabled = True
        cmbTrigger.Items.AddRange(New Object() {"Action Button", "Player Touch", "Parallel Process"})
        cmbTrigger.Location = New Point(13, 47)
        cmbTrigger.Margin = New Padding(7, 6, 7, 6)
        cmbTrigger.Name = "cmbTrigger"
        cmbTrigger.Size = New Size(405, 40)
        cmbTrigger.TabIndex = 0
        ' 
        ' DarkGroupBox4
        ' 
        DarkGroupBox4.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox4.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox4.Controls.Add(picGraphicSel)
        DarkGroupBox4.ForeColor = Color.Gainsboro
        DarkGroupBox4.Location = New Point(394, 657)
        DarkGroupBox4.Margin = New Padding(7, 6, 7, 6)
        DarkGroupBox4.Name = "DarkGroupBox4"
        DarkGroupBox4.Padding = New Padding(7, 6, 7, 6)
        DarkGroupBox4.Size = New Size(433, 117)
        DarkGroupBox4.TabIndex = 3
        DarkGroupBox4.TabStop = False
        DarkGroupBox4.Text = "Positioning"
        ' 
        ' picGraphicSel
        ' 
        picGraphicSel.BackgroundImageLayout = ImageLayout.None
        picGraphicSel.Location = New Point(-409, -721)
        picGraphicSel.Margin = New Padding(7, 6, 7, 6)
        picGraphicSel.Name = "picGraphicSel"
        picGraphicSel.Size = New Size(1738, 1265)
        picGraphicSel.TabIndex = 5
        picGraphicSel.TabStop = False
        ' 
        ' DarkGroupBox3
        ' 
        DarkGroupBox3.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox3.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox3.Controls.Add(DarkLabel7)
        DarkGroupBox3.Controls.Add(cmbMoveFreq)
        DarkGroupBox3.Controls.Add(DarkLabel6)
        DarkGroupBox3.Controls.Add(cmbMoveSpeed)
        DarkGroupBox3.Controls.Add(btnMoveRoute)
        DarkGroupBox3.Controls.Add(cmbMoveType)
        DarkGroupBox3.Controls.Add(DarkLabel5)
        DarkGroupBox3.ForeColor = Color.Gainsboro
        DarkGroupBox3.Location = New Point(397, 339)
        DarkGroupBox3.Margin = New Padding(7, 6, 7, 6)
        DarkGroupBox3.Name = "DarkGroupBox3"
        DarkGroupBox3.Padding = New Padding(7, 6, 7, 6)
        DarkGroupBox3.Size = New Size(433, 303)
        DarkGroupBox3.TabIndex = 2
        DarkGroupBox3.TabStop = False
        DarkGroupBox3.Text = "Movement"
        ' 
        ' DarkLabel7
        ' 
        DarkLabel7.AutoSize = True
        DarkLabel7.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel7.Location = New Point(13, 245)
        DarkLabel7.Margin = New Padding(7, 0, 7, 0)
        DarkLabel7.Name = "DarkLabel7"
        DarkLabel7.Size = New Size(125, 32)
        DarkLabel7.TabIndex = 6
        DarkLabel7.Text = "Frequency"
        ' 
        ' cmbMoveFreq
        ' 
        cmbMoveFreq.DrawMode = DrawMode.OwnerDrawFixed
        cmbMoveFreq.FormattingEnabled = True
        cmbMoveFreq.Items.AddRange(New Object() {"Lowest", "Lower", "Normal", "Higher", "Highest"})
        cmbMoveFreq.Location = New Point(149, 239)
        cmbMoveFreq.Margin = New Padding(7, 6, 7, 6)
        cmbMoveFreq.Name = "cmbMoveFreq"
        cmbMoveFreq.Size = New Size(266, 40)
        cmbMoveFreq.TabIndex = 5
        ' 
        ' DarkLabel6
        ' 
        DarkLabel6.AutoSize = True
        DarkLabel6.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel6.Location = New Point(13, 179)
        DarkLabel6.Margin = New Padding(7, 0, 7, 0)
        DarkLabel6.Name = "DarkLabel6"
        DarkLabel6.Size = New Size(86, 32)
        DarkLabel6.TabIndex = 4
        DarkLabel6.Text = "Speed:"
        ' 
        ' cmbMoveSpeed
        ' 
        cmbMoveSpeed.DrawMode = DrawMode.OwnerDrawFixed
        cmbMoveSpeed.FormattingEnabled = True
        cmbMoveSpeed.Items.AddRange(New Object() {"8x Slower", "4x Slower", "2x Slower", "Normal", "2x Faster", "4x Faster"})
        cmbMoveSpeed.Location = New Point(149, 173)
        cmbMoveSpeed.Margin = New Padding(7, 6, 7, 6)
        cmbMoveSpeed.Name = "cmbMoveSpeed"
        cmbMoveSpeed.Size = New Size(266, 40)
        cmbMoveSpeed.TabIndex = 3
        ' 
        ' btnMoveRoute
        ' 
        btnMoveRoute.Location = New Point(258, 100)
        btnMoveRoute.Margin = New Padding(7, 6, 7, 6)
        btnMoveRoute.Name = "btnMoveRoute"
        btnMoveRoute.Padding = New Padding(11, 13, 11, 13)
        btnMoveRoute.Size = New Size(163, 58)
        btnMoveRoute.TabIndex = 2
        btnMoveRoute.Text = "Move Route"
        ' 
        ' cmbMoveType
        ' 
        cmbMoveType.DrawMode = DrawMode.OwnerDrawFixed
        cmbMoveType.FormattingEnabled = True
        cmbMoveType.Items.AddRange(New Object() {"Fixed Position", "Random", "Move Route"})
        cmbMoveType.Location = New Point(149, 34)
        cmbMoveType.Margin = New Padding(7, 6, 7, 6)
        cmbMoveType.Name = "cmbMoveType"
        cmbMoveType.Size = New Size(266, 40)
        cmbMoveType.TabIndex = 1
        ' 
        ' DarkLabel5
        ' 
        DarkLabel5.AutoSize = True
        DarkLabel5.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel5.Location = New Point(13, 43)
        DarkLabel5.Margin = New Padding(7, 0, 7, 0)
        DarkLabel5.Name = "DarkLabel5"
        DarkLabel5.Size = New Size(70, 32)
        DarkLabel5.TabIndex = 0
        DarkLabel5.Text = "Type:"
        ' 
        ' DarkGroupBox1
        ' 
        DarkGroupBox1.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox1.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox1.Controls.Add(cmbSelfSwitchCompare)
        DarkGroupBox1.Controls.Add(DarkLabel4)
        DarkGroupBox1.Controls.Add(cmbSelfSwitch)
        DarkGroupBox1.Controls.Add(chkSelfSwitch)
        DarkGroupBox1.Controls.Add(cmbHasItem)
        DarkGroupBox1.Controls.Add(chkHasItem)
        DarkGroupBox1.Controls.Add(cmbPlayerSwitchCompare)
        DarkGroupBox1.Controls.Add(DarkLabel3)
        DarkGroupBox1.Controls.Add(cmbPlayerSwitch)
        DarkGroupBox1.Controls.Add(chkPlayerSwitch)
        DarkGroupBox1.Controls.Add(nudPlayerVariable)
        DarkGroupBox1.Controls.Add(cmbPlayervarCompare)
        DarkGroupBox1.Controls.Add(DarkLabel2)
        DarkGroupBox1.Controls.Add(cmbPlayerVar)
        DarkGroupBox1.Controls.Add(chkPlayerVar)
        DarkGroupBox1.ForeColor = Color.Gainsboro
        DarkGroupBox1.Location = New Point(7, 15)
        DarkGroupBox1.Margin = New Padding(7, 6, 7, 6)
        DarkGroupBox1.Name = "DarkGroupBox1"
        DarkGroupBox1.Padding = New Padding(7, 6, 7, 6)
        DarkGroupBox1.Size = New Size(823, 309)
        DarkGroupBox1.TabIndex = 0
        DarkGroupBox1.TabStop = False
        DarkGroupBox1.Text = "Conditions"
        ' 
        ' cmbSelfSwitchCompare
        ' 
        cmbSelfSwitchCompare.DrawMode = DrawMode.OwnerDrawFixed
        cmbSelfSwitchCompare.FormattingEnabled = True
        cmbSelfSwitchCompare.Items.AddRange(New Object() {"False = 0", "True = 1"})
        cmbSelfSwitchCompare.Location = New Point(483, 241)
        cmbSelfSwitchCompare.Margin = New Padding(7, 6, 7, 6)
        cmbSelfSwitchCompare.Name = "cmbSelfSwitchCompare"
        cmbSelfSwitchCompare.Size = New Size(188, 40)
        cmbSelfSwitchCompare.TabIndex = 14
        ' 
        ' DarkLabel4
        ' 
        DarkLabel4.AutoSize = True
        DarkLabel4.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel4.Location = New Point(440, 250)
        DarkLabel4.Margin = New Padding(7, 0, 7, 0)
        DarkLabel4.Name = "DarkLabel4"
        DarkLabel4.Size = New Size(30, 32)
        DarkLabel4.TabIndex = 13
        DarkLabel4.Text = "is"
        ' 
        ' cmbSelfSwitch
        ' 
        cmbSelfSwitch.DrawMode = DrawMode.OwnerDrawFixed
        cmbSelfSwitch.FormattingEnabled = True
        cmbSelfSwitch.Items.AddRange(New Object() {"None", "1 - A", "2 - B", "3 - C", "4 - D"})
        cmbSelfSwitch.Location = New Point(234, 241)
        cmbSelfSwitch.Margin = New Padding(7, 6, 7, 6)
        cmbSelfSwitch.Name = "cmbSelfSwitch"
        cmbSelfSwitch.Size = New Size(188, 40)
        cmbSelfSwitch.TabIndex = 12
        ' 
        ' chkSelfSwitch
        ' 
        chkSelfSwitch.AutoSize = True
        chkSelfSwitch.Location = New Point(13, 245)
        chkSelfSwitch.Margin = New Padding(7, 6, 7, 6)
        chkSelfSwitch.Name = "chkSelfSwitch"
        chkSelfSwitch.Size = New Size(162, 36)
        chkSelfSwitch.TabIndex = 11
        chkSelfSwitch.Text = "Self Switch"
        ' 
        ' cmbHasItem
        ' 
        cmbHasItem.DrawMode = DrawMode.OwnerDrawFixed
        cmbHasItem.FormattingEnabled = True
        cmbHasItem.Location = New Point(234, 175)
        cmbHasItem.Margin = New Padding(7, 6, 7, 6)
        cmbHasItem.Name = "cmbHasItem"
        cmbHasItem.Size = New Size(437, 40)
        cmbHasItem.TabIndex = 10
        ' 
        ' chkHasItem
        ' 
        chkHasItem.AutoSize = True
        chkHasItem.Location = New Point(13, 179)
        chkHasItem.Margin = New Padding(7, 6, 7, 6)
        chkHasItem.Name = "chkHasItem"
        chkHasItem.Size = New Size(211, 36)
        chkHasItem.TabIndex = 9
        chkHasItem.Text = "Player Has Item"
        ' 
        ' cmbPlayerSwitchCompare
        ' 
        cmbPlayerSwitchCompare.DrawMode = DrawMode.OwnerDrawFixed
        cmbPlayerSwitchCompare.FormattingEnabled = True
        cmbPlayerSwitchCompare.Items.AddRange(New Object() {"False = 0", "True = 1"})
        cmbPlayerSwitchCompare.Location = New Point(483, 109)
        cmbPlayerSwitchCompare.Margin = New Padding(7, 6, 7, 6)
        cmbPlayerSwitchCompare.Name = "cmbPlayerSwitchCompare"
        cmbPlayerSwitchCompare.Size = New Size(188, 40)
        cmbPlayerSwitchCompare.TabIndex = 8
        ' 
        ' DarkLabel3
        ' 
        DarkLabel3.AutoSize = True
        DarkLabel3.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel3.Location = New Point(440, 115)
        DarkLabel3.Margin = New Padding(7, 0, 7, 0)
        DarkLabel3.Name = "DarkLabel3"
        DarkLabel3.Size = New Size(30, 32)
        DarkLabel3.TabIndex = 7
        DarkLabel3.Text = "is"
        ' 
        ' cmbPlayerSwitch
        ' 
        cmbPlayerSwitch.DrawMode = DrawMode.OwnerDrawFixed
        cmbPlayerSwitch.FormattingEnabled = True
        cmbPlayerSwitch.Location = New Point(234, 109)
        cmbPlayerSwitch.Margin = New Padding(7, 6, 7, 6)
        cmbPlayerSwitch.Name = "cmbPlayerSwitch"
        cmbPlayerSwitch.Size = New Size(188, 40)
        cmbPlayerSwitch.TabIndex = 6
        ' 
        ' chkPlayerSwitch
        ' 
        chkPlayerSwitch.AutoSize = True
        chkPlayerSwitch.Location = New Point(13, 113)
        chkPlayerSwitch.Margin = New Padding(7, 6, 7, 6)
        chkPlayerSwitch.Name = "chkPlayerSwitch"
        chkPlayerSwitch.Size = New Size(186, 36)
        chkPlayerSwitch.TabIndex = 5
        chkPlayerSwitch.Text = "Player Switch"
        ' 
        ' nudPlayerVariable
        ' 
        nudPlayerVariable.Location = New Point(689, 45)
        nudPlayerVariable.Margin = New Padding(7, 6, 7, 6)
        nudPlayerVariable.Name = "nudPlayerVariable"
        nudPlayerVariable.Size = New Size(121, 39)
        nudPlayerVariable.TabIndex = 4
        ' 
        ' cmbPlayervarCompare
        ' 
        cmbPlayervarCompare.DrawMode = DrawMode.OwnerDrawFixed
        cmbPlayervarCompare.FormattingEnabled = True
        cmbPlayervarCompare.Items.AddRange(New Object() {"Equal To", "Great Than OrElse Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal"})
        cmbPlayervarCompare.Location = New Point(483, 43)
        cmbPlayervarCompare.Margin = New Padding(7, 6, 7, 6)
        cmbPlayervarCompare.Name = "cmbPlayervarCompare"
        cmbPlayervarCompare.Size = New Size(188, 40)
        cmbPlayervarCompare.TabIndex = 3
        ' 
        ' DarkLabel2
        ' 
        DarkLabel2.AutoSize = True
        DarkLabel2.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel2.Location = New Point(440, 58)
        DarkLabel2.Margin = New Padding(7, 0, 7, 0)
        DarkLabel2.Name = "DarkLabel2"
        DarkLabel2.Size = New Size(30, 32)
        DarkLabel2.TabIndex = 2
        DarkLabel2.Text = "is"
        ' 
        ' cmbPlayerVar
        ' 
        cmbPlayerVar.DrawMode = DrawMode.OwnerDrawFixed
        cmbPlayerVar.FormattingEnabled = True
        cmbPlayerVar.Location = New Point(234, 43)
        cmbPlayerVar.Margin = New Padding(7, 6, 7, 6)
        cmbPlayerVar.Name = "cmbPlayerVar"
        cmbPlayerVar.Size = New Size(188, 40)
        cmbPlayerVar.TabIndex = 1
        ' 
        ' chkPlayerVar
        ' 
        chkPlayerVar.AutoSize = True
        chkPlayerVar.Location = New Point(13, 47)
        chkPlayerVar.Margin = New Padding(7, 6, 7, 6)
        chkPlayerVar.Name = "chkPlayerVar"
        chkPlayerVar.Size = New Size(201, 36)
        chkPlayerVar.TabIndex = 0
        chkPlayerVar.Text = "Player Variable"
        ' 
        ' fraGraphic
        ' 
        fraGraphic.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraGraphic.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraGraphic.Controls.Add(btnGraphicOk)
        fraGraphic.Controls.Add(btnGraphicCancel)
        fraGraphic.Controls.Add(DarkLabel13)
        fraGraphic.Controls.Add(nudGraphic)
        fraGraphic.Controls.Add(DarkLabel12)
        fraGraphic.Controls.Add(cmbGraphic)
        fraGraphic.Controls.Add(DarkLabel11)
        fraGraphic.ForeColor = Color.Gainsboro
        fraGraphic.Location = New Point(843, 13)
        fraGraphic.Margin = New Padding(7, 6, 7, 6)
        fraGraphic.Name = "fraGraphic"
        fraGraphic.Padding = New Padding(7, 6, 7, 6)
        fraGraphic.Size = New Size(852, 1188)
        fraGraphic.TabIndex = 14
        fraGraphic.TabStop = False
        fraGraphic.Text = "Graphic Selection"
        fraGraphic.Visible = False
        ' 
        ' btnGraphicOk
        ' 
        btnGraphicOk.Location = New Point(1413, 1404)
        btnGraphicOk.Margin = New Padding(7, 6, 7, 6)
        btnGraphicOk.Name = "btnGraphicOk"
        btnGraphicOk.Padding = New Padding(11, 13, 11, 13)
        btnGraphicOk.Size = New Size(163, 58)
        btnGraphicOk.TabIndex = 8
        btnGraphicOk.Text = "Ok"
        ' 
        ' btnGraphicCancel
        ' 
        btnGraphicCancel.Location = New Point(1588, 1404)
        btnGraphicCancel.Margin = New Padding(7, 6, 7, 6)
        btnGraphicCancel.Name = "btnGraphicCancel"
        btnGraphicCancel.Padding = New Padding(11, 13, 11, 13)
        btnGraphicCancel.Size = New Size(163, 58)
        btnGraphicCancel.TabIndex = 7
        btnGraphicCancel.Text = "Cancel"
        ' 
        ' DarkLabel13
        ' 
        DarkLabel13.AutoSize = True
        DarkLabel13.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel13.Location = New Point(22, 1406)
        DarkLabel13.Margin = New Padding(7, 0, 7, 0)
        DarkLabel13.Name = "DarkLabel13"
        DarkLabel13.Size = New Size(368, 32)
        DarkLabel13.TabIndex = 6
        DarkLabel13.Text = "Hold Shift to select multiple tiles."
        ' 
        ' nudGraphic
        ' 
        nudGraphic.Location = New Point(225, 122)
        nudGraphic.Margin = New Padding(7, 6, 7, 6)
        nudGraphic.Name = "nudGraphic"
        nudGraphic.Size = New Size(468, 39)
        nudGraphic.TabIndex = 3
        ' 
        ' DarkLabel12
        ' 
        DarkLabel12.AutoSize = True
        DarkLabel12.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel12.Location = New Point(45, 126)
        DarkLabel12.Margin = New Padding(7, 0, 7, 0)
        DarkLabel12.Name = "DarkLabel12"
        DarkLabel12.Size = New Size(107, 32)
        DarkLabel12.TabIndex = 2
        DarkLabel12.Text = "Number:"
        ' 
        ' cmbGraphic
        ' 
        cmbGraphic.DrawMode = DrawMode.OwnerDrawFixed
        cmbGraphic.FormattingEnabled = True
        cmbGraphic.Items.AddRange(New Object() {"None", "Character", "Tileset"})
        cmbGraphic.Location = New Point(225, 45)
        cmbGraphic.Margin = New Padding(7, 6, 7, 6)
        cmbGraphic.Name = "cmbGraphic"
        cmbGraphic.Size = New Size(465, 40)
        cmbGraphic.TabIndex = 1
        ' 
        ' DarkLabel11
        ' 
        DarkLabel11.AutoSize = True
        DarkLabel11.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel11.Location = New Point(45, 51)
        DarkLabel11.Margin = New Padding(7, 0, 7, 0)
        DarkLabel11.Name = "DarkLabel11"
        DarkLabel11.Size = New Size(168, 32)
        DarkLabel11.TabIndex = 0
        DarkLabel11.Text = "Graphics Type:"
        ' 
        ' fraCommands
        ' 
        fraCommands.Controls.Add(btnCancelCommand)
        fraCommands.Controls.Add(tvCommands)
        fraCommands.Location = New Point(843, 15)
        fraCommands.Margin = New Padding(7, 6, 7, 6)
        fraCommands.Name = "fraCommands"
        fraCommands.Size = New Size(851, 1186)
        fraCommands.TabIndex = 6
        fraCommands.Visible = False
        ' 
        ' btnCancelCommand
        ' 
        btnCancelCommand.Location = New Point(676, 1114)
        btnCancelCommand.Margin = New Padding(7, 6, 7, 6)
        btnCancelCommand.Name = "btnCancelCommand"
        btnCancelCommand.Padding = New Padding(11, 13, 11, 13)
        btnCancelCommand.Size = New Size(163, 58)
        btnCancelCommand.TabIndex = 2
        btnCancelCommand.Text = "Cancel"
        ' 
        ' lstCommands
        ' 
        lstCommands.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        lstCommands.BorderStyle = BorderStyle.FixedSingle
        lstCommands.ForeColor = Color.Gainsboro
        lstCommands.FormattingEnabled = True
        lstCommands.Location = New Point(843, 15)
        lstCommands.Margin = New Padding(7, 6, 7, 6)
        lstCommands.Name = "lstCommands"
        lstCommands.Size = New Size(849, 1058)
        lstCommands.TabIndex = 8
        ' 
        ' DarkGroupBox8
        ' 
        DarkGroupBox8.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox8.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox8.Controls.Add(btnClearCommand)
        DarkGroupBox8.Controls.Add(btnDeleteCommand)
        DarkGroupBox8.Controls.Add(btnEditCommand)
        DarkGroupBox8.Controls.Add(btnAddCommand)
        DarkGroupBox8.ForeColor = Color.Gainsboro
        DarkGroupBox8.Location = New Point(843, 1082)
        DarkGroupBox8.Margin = New Padding(7, 6, 7, 6)
        DarkGroupBox8.Name = "DarkGroupBox8"
        DarkGroupBox8.Padding = New Padding(7, 6, 7, 6)
        DarkGroupBox8.Size = New Size(851, 122)
        DarkGroupBox8.TabIndex = 9
        DarkGroupBox8.TabStop = False
        DarkGroupBox8.Text = "Commands"
        ' 
        ' btnClearCommand
        ' 
        btnClearCommand.Location = New Point(676, 47)
        btnClearCommand.Margin = New Padding(7, 6, 7, 6)
        btnClearCommand.Name = "btnClearCommand"
        btnClearCommand.Padding = New Padding(11, 13, 11, 13)
        btnClearCommand.Size = New Size(163, 58)
        btnClearCommand.TabIndex = 3
        btnClearCommand.Text = "Clear"
        ' 
        ' btnDeleteCommand
        ' 
        btnDeleteCommand.Location = New Point(459, 47)
        btnDeleteCommand.Margin = New Padding(7, 6, 7, 6)
        btnDeleteCommand.Name = "btnDeleteCommand"
        btnDeleteCommand.Padding = New Padding(11, 13, 11, 13)
        btnDeleteCommand.Size = New Size(163, 58)
        btnDeleteCommand.TabIndex = 2
        btnDeleteCommand.Text = "Delete"
        ' 
        ' btnEditCommand
        ' 
        btnEditCommand.Location = New Point(234, 47)
        btnEditCommand.Margin = New Padding(7, 6, 7, 6)
        btnEditCommand.Name = "btnEditCommand"
        btnEditCommand.Padding = New Padding(11, 13, 11, 13)
        btnEditCommand.Size = New Size(163, 58)
        btnEditCommand.TabIndex = 1
        btnEditCommand.Text = "Edit"
        ' 
        ' btnAddCommand
        ' 
        btnAddCommand.Location = New Point(13, 47)
        btnAddCommand.Margin = New Padding(7, 6, 7, 6)
        btnAddCommand.Name = "btnAddCommand"
        btnAddCommand.Padding = New Padding(11, 13, 11, 13)
        btnAddCommand.Size = New Size(163, 58)
        btnAddCommand.TabIndex = 0
        btnAddCommand.Text = "Add"
        ' 
        ' btnLabeling
        ' 
        btnLabeling.Location = New Point(24, 1427)
        btnLabeling.Margin = New Padding(7, 6, 7, 6)
        btnLabeling.Name = "btnLabeling"
        btnLabeling.Padding = New Padding(11, 13, 11, 13)
        btnLabeling.Size = New Size(381, 58)
        btnLabeling.TabIndex = 6
        btnLabeling.Text = "Edit Variables/Switches"
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(1540, 1427)
        btnCancel.Margin = New Padding(7, 6, 7, 6)
        btnCancel.Name = "btnCancel"
        btnCancel.Padding = New Padding(11, 13, 11, 13)
        btnCancel.Size = New Size(163, 58)
        btnCancel.TabIndex = 7
        btnCancel.Text = "Cancel"
        ' 
        ' btnOk
        ' 
        btnOk.Location = New Point(1361, 1427)
        btnOk.Margin = New Padding(7, 6, 7, 6)
        btnOk.Name = "btnOk"
        btnOk.Padding = New Padding(11, 13, 11, 13)
        btnOk.Size = New Size(163, 58)
        btnOk.TabIndex = 8
        btnOk.Text = "Save"
        ' 
        ' fraMoveRoute
        ' 
        fraMoveRoute.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraMoveRoute.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraMoveRoute.Controls.Add(btnMoveRouteOk)
        fraMoveRoute.Controls.Add(btnMoveRouteCancel)
        fraMoveRoute.Controls.Add(chkRepeatRoute)
        fraMoveRoute.Controls.Add(chkIgnoreMove)
        fraMoveRoute.Controls.Add(DarkGroupBox10)
        fraMoveRoute.Controls.Add(lstMoveRoute)
        fraMoveRoute.Controls.Add(cmbEvent)
        fraMoveRoute.ForeColor = Color.Gainsboro
        fraMoveRoute.Location = New Point(1733, 30)
        fraMoveRoute.Margin = New Padding(7, 6, 7, 6)
        fraMoveRoute.Name = "fraMoveRoute"
        fraMoveRoute.Padding = New Padding(7, 6, 7, 6)
        fraMoveRoute.Size = New Size(201, 209)
        fraMoveRoute.TabIndex = 0
        fraMoveRoute.TabStop = False
        fraMoveRoute.Text = "Move Route"
        fraMoveRoute.Visible = False
        ' 
        ' btnMoveRouteOk
        ' 
        btnMoveRouteOk.Location = New Point(1391, 1060)
        btnMoveRouteOk.Margin = New Padding(7, 6, 7, 6)
        btnMoveRouteOk.Name = "btnMoveRouteOk"
        btnMoveRouteOk.Padding = New Padding(11, 13, 11, 13)
        btnMoveRouteOk.Size = New Size(163, 58)
        btnMoveRouteOk.TabIndex = 7
        btnMoveRouteOk.Text = "Ok"
        ' 
        ' btnMoveRouteCancel
        ' 
        btnMoveRouteCancel.Location = New Point(1567, 1060)
        btnMoveRouteCancel.Margin = New Padding(7, 6, 7, 6)
        btnMoveRouteCancel.Name = "btnMoveRouteCancel"
        btnMoveRouteCancel.Padding = New Padding(11, 13, 11, 13)
        btnMoveRouteCancel.Size = New Size(163, 58)
        btnMoveRouteCancel.TabIndex = 6
        btnMoveRouteCancel.Text = "Cancel"
        ' 
        ' chkRepeatRoute
        ' 
        chkRepeatRoute.AutoSize = True
        chkRepeatRoute.Location = New Point(13, 1118)
        chkRepeatRoute.Margin = New Padding(7, 6, 7, 6)
        chkRepeatRoute.Name = "chkRepeatRoute"
        chkRepeatRoute.Size = New Size(188, 36)
        chkRepeatRoute.TabIndex = 5
        chkRepeatRoute.Text = "Repeat Route"
        ' 
        ' chkIgnoreMove
        ' 
        chkIgnoreMove.AutoSize = True
        chkIgnoreMove.Location = New Point(13, 1060)
        chkIgnoreMove.Margin = New Padding(7, 6, 7, 6)
        chkIgnoreMove.Name = "chkIgnoreMove"
        chkIgnoreMove.Size = New Size(328, 36)
        chkIgnoreMove.TabIndex = 4
        chkIgnoreMove.Text = "Ignore if event can't move"
        ' 
        ' DarkGroupBox10
        ' 
        DarkGroupBox10.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox10.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox10.Controls.Add(lstvwMoveRoute)
        DarkGroupBox10.ForeColor = Color.Gainsboro
        DarkGroupBox10.Location = New Point(440, 26)
        DarkGroupBox10.Margin = New Padding(7, 6, 7, 6)
        DarkGroupBox10.Name = "DarkGroupBox10"
        DarkGroupBox10.Padding = New Padding(7, 6, 7, 6)
        DarkGroupBox10.Size = New Size(1289, 1022)
        DarkGroupBox10.TabIndex = 3
        DarkGroupBox10.TabStop = False
        DarkGroupBox10.Text = "Commands"
        ' 
        ' lstvwMoveRoute
        ' 
        lstvwMoveRoute.AutoArrange = False
        lstvwMoveRoute.BackColor = Color.DimGray
        lstvwMoveRoute.BorderStyle = BorderStyle.None
        lstvwMoveRoute.Columns.AddRange(New ColumnHeader() {ColumnHeader3, ColumnHeader4})
        lstvwMoveRoute.Dock = DockStyle.Top
        lstvwMoveRoute.Font = New Font("Microsoft Sans Serif", 8.25F)
        lstvwMoveRoute.ForeColor = Color.Gainsboro
        ListViewGroup1.Header = "Movement"
        ListViewGroup1.Name = "lstVgMovement"
        ListViewGroup2.Header = "Wait"
        ListViewGroup2.Name = "lstVgWait"
        ListViewGroup3.Header = "Turning"
        ListViewGroup3.Name = "lstVgTurn"
        ListViewGroup4.Header = "Speed"
        ListViewGroup4.Name = "lstVgSpeed"
        ListViewGroup5.Header = "Walk Animation"
        ListViewGroup5.Name = "lstVgWalk"
        ListViewGroup6.Header = "Fixed Direction"
        ListViewGroup6.Name = "lstVgDirection"
        ListViewGroup7.Header = "WalkThrough"
        ListViewGroup7.Name = "lstVgWalkThrough"
        ListViewGroup8.Header = "Set Position"
        ListViewGroup8.Name = "lstVgSetposition"
        ListViewGroup9.Header = "Set Graphic"
        ListViewGroup9.Name = "lstVgSetGraphic"
        lstvwMoveRoute.Groups.AddRange(New ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3, ListViewGroup4, ListViewGroup5, ListViewGroup6, ListViewGroup7, ListViewGroup8, ListViewGroup9})
        lstvwMoveRoute.HeaderStyle = ColumnHeaderStyle.None
        ListViewItem1.Group = ListViewGroup1
        ListViewItem2.Group = ListViewGroup1
        ListViewItem2.IndentCount = 1
        ListViewItem3.Group = ListViewGroup1
        ListViewItem4.Group = ListViewGroup1
        ListViewItem4.IndentCount = 1
        ListViewItem5.Group = ListViewGroup1
        ListViewItem6.Group = ListViewGroup1
        ListViewItem7.Group = ListViewGroup1
        ListViewItem8.Group = ListViewGroup1
        ListViewItem9.Group = ListViewGroup1
        ListViewItem10.Group = ListViewGroup2
        ListViewItem11.Group = ListViewGroup2
        ListViewItem12.Group = ListViewGroup2
        ListViewItem13.Group = ListViewGroup3
        ListViewItem14.Group = ListViewGroup3
        ListViewItem15.Group = ListViewGroup3
        ListViewItem16.Group = ListViewGroup3
        ListViewItem17.Group = ListViewGroup3
        ListViewItem18.Group = ListViewGroup3
        ListViewItem19.Group = ListViewGroup3
        ListViewItem20.Group = ListViewGroup3
        ListViewItem21.Group = ListViewGroup3
        ListViewItem22.Group = ListViewGroup3
        ListViewItem23.Group = ListViewGroup4
        ListViewItem24.Group = ListViewGroup4
        ListViewItem25.Group = ListViewGroup4
        ListViewItem26.Group = ListViewGroup4
        ListViewItem27.Group = ListViewGroup4
        ListViewItem28.Group = ListViewGroup4
        ListViewItem29.Group = ListViewGroup4
        ListViewItem30.Group = ListViewGroup4
        ListViewItem31.Group = ListViewGroup4
        ListViewItem32.Group = ListViewGroup4
        ListViewItem33.Group = ListViewGroup4
        ListViewItem34.Group = ListViewGroup5
        ListViewItem35.Group = ListViewGroup5
        ListViewItem36.Group = ListViewGroup6
        ListViewItem37.Group = ListViewGroup6
        ListViewItem38.Group = ListViewGroup7
        ListViewItem39.Group = ListViewGroup7
        ListViewItem40.Group = ListViewGroup8
        ListViewItem41.Group = ListViewGroup8
        ListViewItem42.Group = ListViewGroup8
        ListViewItem43.Group = ListViewGroup9
        lstvwMoveRoute.Items.AddRange(New ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10, ListViewItem11, ListViewItem12, ListViewItem13, ListViewItem14, ListViewItem15, ListViewItem16, ListViewItem17, ListViewItem18, ListViewItem19, ListViewItem20, ListViewItem21, ListViewItem22, ListViewItem23, ListViewItem24, ListViewItem25, ListViewItem26, ListViewItem27, ListViewItem28, ListViewItem29, ListViewItem30, ListViewItem31, ListViewItem32, ListViewItem33, ListViewItem34, ListViewItem35, ListViewItem36, ListViewItem37, ListViewItem38, ListViewItem39, ListViewItem40, ListViewItem41, ListViewItem42, ListViewItem43})
        lstvwMoveRoute.LabelWrap = False
        lstvwMoveRoute.Location = New Point(7, 38)
        lstvwMoveRoute.Margin = New Padding(7, 6, 7, 6)
        lstvwMoveRoute.MultiSelect = False
        lstvwMoveRoute.Name = "lstvwMoveRoute"
        lstvwMoveRoute.Size = New Size(1275, 977)
        lstvwMoveRoute.TabIndex = 5
        lstvwMoveRoute.UseCompatibleStateImageBehavior = False
        lstvwMoveRoute.View = View.Tile
        ' 
        ' ColumnHeader3
        ' 
        ColumnHeader3.Text = ""
        ColumnHeader3.Width = 150
        ' 
        ' ColumnHeader4
        ' 
        ColumnHeader4.Text = ""
        ColumnHeader4.Width = 150
        ' 
        ' lstMoveRoute
        ' 
        lstMoveRoute.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        lstMoveRoute.BorderStyle = BorderStyle.FixedSingle
        lstMoveRoute.ForeColor = Color.Gainsboro
        lstMoveRoute.FormattingEnabled = True
        lstMoveRoute.Location = New Point(13, 113)
        lstMoveRoute.Margin = New Padding(7, 6, 7, 6)
        lstMoveRoute.Name = "lstMoveRoute"
        lstMoveRoute.Size = New Size(411, 930)
        lstMoveRoute.TabIndex = 2
        ' 
        ' cmbEvent
        ' 
        cmbEvent.DrawMode = DrawMode.OwnerDrawFixed
        cmbEvent.FormattingEnabled = True
        cmbEvent.Location = New Point(13, 47)
        cmbEvent.Margin = New Padding(7, 6, 7, 6)
        cmbEvent.Name = "cmbEvent"
        cmbEvent.Size = New Size(409, 40)
        cmbEvent.TabIndex = 0
        ' 
        ' pnlGraphicSel
        ' 
        pnlGraphicSel.AutoScroll = True
        pnlGraphicSel.Location = New Point(7, 196)
        pnlGraphicSel.Margin = New Padding(7, 6, 7, 6)
        pnlGraphicSel.Name = "pnlGraphicSel"
        pnlGraphicSel.Size = New Size(1714, 1222)
        pnlGraphicSel.TabIndex = 9
        ' 
        ' fraDialogue
        ' 
        fraDialogue.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraDialogue.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraDialogue.Controls.Add(fraConditionalBranch)
        fraDialogue.Controls.Add(fraMoveRouteWait)
        fraDialogue.Controls.Add(fraCustomScript)
        fraDialogue.Controls.Add(fraSetWeather)
        fraDialogue.Controls.Add(fraSpawnNpc)
        fraDialogue.Controls.Add(fraGiveExp)
        fraDialogue.Controls.Add(fraEndQuest)
        fraDialogue.Controls.Add(fraSetAccess)
        fraDialogue.Controls.Add(fraOpenShop)
        fraDialogue.Controls.Add(fraChangeLevel)
        fraDialogue.Controls.Add(fraChangeGender)
        fraDialogue.Controls.Add(fraGoToLabel)
        fraDialogue.Controls.Add(fraShowChoices)
        fraDialogue.Controls.Add(fraPlayerVariable)
        fraDialogue.Controls.Add(fraChangeSprite)
        fraDialogue.Controls.Add(fraSetSelfSwitch)
        fraDialogue.Controls.Add(fraMapTint)
        fraDialogue.Controls.Add(fraShowChatBubble)
        fraDialogue.Controls.Add(fraPlaySound)
        fraDialogue.Controls.Add(fraChangePK)
        fraDialogue.Controls.Add(fraCreateLabel)
        fraDialogue.Controls.Add(fraChangeJob)
        fraDialogue.Controls.Add(fraChangeSkills)
        fraDialogue.Controls.Add(fraPlayerWarp)
        fraDialogue.Controls.Add(fraSetFog)
        fraDialogue.Controls.Add(fraAddText)
        fraDialogue.Controls.Add(fraPlayerSwitch)
        fraDialogue.Controls.Add(fraChangeItems)
        fraDialogue.Controls.Add(fraPlayBGM)
        fraDialogue.Controls.Add(fraPlayAnimation)
        fraDialogue.Controls.Add(fraSetWait)
        fraDialogue.Controls.Add(fraShowPic)
        fraDialogue.Controls.Add(fraShowText)
        fraDialogue.ForeColor = Color.Gainsboro
        fraDialogue.Location = New Point(1961, 30)
        fraDialogue.Margin = New Padding(7, 6, 7, 6)
        fraDialogue.Name = "fraDialogue"
        fraDialogue.Padding = New Padding(7, 6, 7, 6)
        fraDialogue.Size = New Size(1441, 1466)
        fraDialogue.TabIndex = 10
        fraDialogue.TabStop = False
        fraDialogue.Visible = False
        ' 
        ' fraConditionalBranch
        ' 
        fraConditionalBranch.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraConditionalBranch.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraConditionalBranch.Controls.Add(cmbCondition_Time)
        fraConditionalBranch.Controls.Add(optCondition9)
        fraConditionalBranch.Controls.Add(btnConditionalBranchOk)
        fraConditionalBranch.Controls.Add(btnConditionalBranchCancel)
        fraConditionalBranch.Controls.Add(cmbCondition_Gender)
        fraConditionalBranch.Controls.Add(optCondition8)
        fraConditionalBranch.Controls.Add(cmbCondition_SelfSwitchCondition)
        fraConditionalBranch.Controls.Add(DarkLabel17)
        fraConditionalBranch.Controls.Add(cmbCondition_SelfSwitch)
        fraConditionalBranch.Controls.Add(optCondition6)
        fraConditionalBranch.Controls.Add(nudCondition_LevelAmount)
        fraConditionalBranch.Controls.Add(optCondition5)
        fraConditionalBranch.Controls.Add(cmbCondition_LevelCompare)
        fraConditionalBranch.Controls.Add(cmbCondition_LearntSkill)
        fraConditionalBranch.Controls.Add(optCondition4)
        fraConditionalBranch.Controls.Add(cmbCondition_JobIs)
        fraConditionalBranch.Controls.Add(optCondition3)
        fraConditionalBranch.Controls.Add(nudCondition_HasItem)
        fraConditionalBranch.Controls.Add(DarkLabel16)
        fraConditionalBranch.Controls.Add(cmbCondition_HasItem)
        fraConditionalBranch.Controls.Add(optCondition2)
        fraConditionalBranch.Controls.Add(optCondition1)
        fraConditionalBranch.Controls.Add(DarkLabel15)
        fraConditionalBranch.Controls.Add(cmbCondtion_PlayerSwitchCondition)
        fraConditionalBranch.Controls.Add(cmbCondition_PlayerSwitch)
        fraConditionalBranch.Controls.Add(nudCondition_PlayerVarCondition)
        fraConditionalBranch.Controls.Add(cmbCondition_PlayerVarCompare)
        fraConditionalBranch.Controls.Add(DarkLabel14)
        fraConditionalBranch.Controls.Add(cmbCondition_PlayerVarIndex)
        fraConditionalBranch.Controls.Add(optCondition0)
        fraConditionalBranch.ForeColor = Color.Gainsboro
        fraConditionalBranch.Location = New Point(13, 17)
        fraConditionalBranch.Margin = New Padding(7, 6, 7, 6)
        fraConditionalBranch.Name = "fraConditionalBranch"
        fraConditionalBranch.Padding = New Padding(7, 6, 7, 6)
        fraConditionalBranch.Size = New Size(843, 1101)
        fraConditionalBranch.TabIndex = 0
        fraConditionalBranch.TabStop = False
        fraConditionalBranch.Text = "Conditional Branch"
        fraConditionalBranch.Visible = False
        ' 
        ' cmbCondition_Time
        ' 
        cmbCondition_Time.DrawMode = DrawMode.OwnerDrawVariable
        cmbCondition_Time.FormattingEnabled = True
        cmbCondition_Time.Items.AddRange(New Object() {"Day", "Night", "Dawn", "Dusk"})
        cmbCondition_Time.Location = New Point(518, 570)
        cmbCondition_Time.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_Time.Name = "cmbCondition_Time"
        cmbCondition_Time.Size = New Size(307, 40)
        cmbCondition_Time.TabIndex = 33
        ' 
        ' optCondition9
        ' 
        optCondition9.AutoSize = True
        optCondition9.Location = New Point(13, 572)
        optCondition9.Margin = New Padding(7, 6, 7, 6)
        optCondition9.Name = "optCondition9"
        optCondition9.Size = New Size(203, 36)
        optCondition9.TabIndex = 32
        optCondition9.TabStop = True
        optCondition9.Text = "Time of Day is:"
        ' 
        ' btnConditionalBranchOk
        ' 
        btnConditionalBranchOk.Location = New Point(490, 1024)
        btnConditionalBranchOk.Margin = New Padding(7, 6, 7, 6)
        btnConditionalBranchOk.Name = "btnConditionalBranchOk"
        btnConditionalBranchOk.Padding = New Padding(11, 13, 11, 13)
        btnConditionalBranchOk.Size = New Size(163, 58)
        btnConditionalBranchOk.TabIndex = 31
        btnConditionalBranchOk.Text = "Ok"
        ' 
        ' btnConditionalBranchCancel
        ' 
        btnConditionalBranchCancel.Location = New Point(665, 1024)
        btnConditionalBranchCancel.Margin = New Padding(7, 6, 7, 6)
        btnConditionalBranchCancel.Name = "btnConditionalBranchCancel"
        btnConditionalBranchCancel.Padding = New Padding(11, 13, 11, 13)
        btnConditionalBranchCancel.Size = New Size(163, 58)
        btnConditionalBranchCancel.TabIndex = 30
        btnConditionalBranchCancel.Text = "Cancel"
        ' 
        ' cmbCondition_Gender
        ' 
        cmbCondition_Gender.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_Gender.FormattingEnabled = True
        cmbCondition_Gender.Items.AddRange(New Object() {"Male", "Female"})
        cmbCondition_Gender.Location = New Point(518, 503)
        cmbCondition_Gender.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_Gender.Name = "cmbCondition_Gender"
        cmbCondition_Gender.Size = New Size(307, 40)
        cmbCondition_Gender.TabIndex = 29
        ' 
        ' optCondition8
        ' 
        optCondition8.AutoSize = True
        optCondition8.Location = New Point(13, 506)
        optCondition8.Margin = New Padding(7, 6, 7, 6)
        optCondition8.Name = "optCondition8"
        optCondition8.Size = New Size(217, 36)
        optCondition8.TabIndex = 28
        optCondition8.TabStop = True
        optCondition8.Text = "Player Gender is"
        ' 
        ' cmbCondition_SelfSwitchCondition
        ' 
        cmbCondition_SelfSwitchCondition.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_SelfSwitchCondition.FormattingEnabled = True
        cmbCondition_SelfSwitchCondition.Items.AddRange(New Object() {"False", "True"})
        cmbCondition_SelfSwitchCondition.Location = New Point(568, 450)
        cmbCondition_SelfSwitchCondition.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_SelfSwitchCondition.Name = "cmbCondition_SelfSwitchCondition"
        cmbCondition_SelfSwitchCondition.Size = New Size(257, 40)
        cmbCondition_SelfSwitchCondition.TabIndex = 23
        ' 
        ' DarkLabel17
        ' 
        DarkLabel17.AutoSize = True
        DarkLabel17.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel17.Location = New Point(507, 459)
        DarkLabel17.Margin = New Padding(7, 0, 7, 0)
        DarkLabel17.Name = "DarkLabel17"
        DarkLabel17.Size = New Size(30, 32)
        DarkLabel17.TabIndex = 22
        DarkLabel17.Text = "is"
        ' 
        ' cmbCondition_SelfSwitch
        ' 
        cmbCondition_SelfSwitch.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_SelfSwitch.FormattingEnabled = True
        cmbCondition_SelfSwitch.Location = New Point(232, 450)
        cmbCondition_SelfSwitch.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_SelfSwitch.Name = "cmbCondition_SelfSwitch"
        cmbCondition_SelfSwitch.Size = New Size(257, 40)
        cmbCondition_SelfSwitch.TabIndex = 21
        ' 
        ' optCondition6
        ' 
        optCondition6.AutoSize = True
        optCondition6.Location = New Point(13, 452)
        optCondition6.Margin = New Padding(7, 6, 7, 6)
        optCondition6.Name = "optCondition6"
        optCondition6.Size = New Size(161, 36)
        optCondition6.TabIndex = 20
        optCondition6.TabStop = True
        optCondition6.Text = "Self Switch"
        ' 
        ' nudCondition_LevelAmount
        ' 
        nudCondition_LevelAmount.Location = New Point(583, 386)
        nudCondition_LevelAmount.Margin = New Padding(7, 6, 7, 6)
        nudCondition_LevelAmount.Name = "nudCondition_LevelAmount"
        nudCondition_LevelAmount.Size = New Size(245, 39)
        nudCondition_LevelAmount.TabIndex = 19
        ' 
        ' optCondition5
        ' 
        optCondition5.AutoSize = True
        optCondition5.Location = New Point(13, 386)
        optCondition5.Margin = New Padding(7, 6, 7, 6)
        optCondition5.Name = "optCondition5"
        optCondition5.Size = New Size(123, 36)
        optCondition5.TabIndex = 18
        optCondition5.TabStop = True
        optCondition5.Text = "Level is"
        ' 
        ' cmbCondition_LevelCompare
        ' 
        cmbCondition_LevelCompare.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_LevelCompare.FormattingEnabled = True
        cmbCondition_LevelCompare.Items.AddRange(New Object() {"Equal To", "Great Than OrElse Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal"})
        cmbCondition_LevelCompare.Location = New Point(232, 384)
        cmbCondition_LevelCompare.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_LevelCompare.Name = "cmbCondition_LevelCompare"
        cmbCondition_LevelCompare.Size = New Size(333, 40)
        cmbCondition_LevelCompare.TabIndex = 17
        ' 
        ' cmbCondition_LearntSkill
        ' 
        cmbCondition_LearntSkill.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_LearntSkill.FormattingEnabled = True
        cmbCondition_LearntSkill.Location = New Point(232, 318)
        cmbCondition_LearntSkill.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_LearntSkill.Name = "cmbCondition_LearntSkill"
        cmbCondition_LearntSkill.Size = New Size(593, 40)
        cmbCondition_LearntSkill.TabIndex = 16
        ' 
        ' optCondition4
        ' 
        optCondition4.AutoSize = True
        optCondition4.Location = New Point(13, 320)
        optCondition4.Margin = New Padding(7, 6, 7, 6)
        optCondition4.Name = "optCondition4"
        optCondition4.Size = New Size(164, 36)
        optCondition4.TabIndex = 15
        optCondition4.TabStop = True
        optCondition4.Text = "Knows Skill"
        ' 
        ' cmbCondition_JobIs
        ' 
        cmbCondition_JobIs.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_JobIs.FormattingEnabled = True
        cmbCondition_JobIs.Location = New Point(232, 252)
        cmbCondition_JobIs.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_JobIs.Name = "cmbCondition_JobIs"
        cmbCondition_JobIs.Size = New Size(593, 40)
        cmbCondition_JobIs.TabIndex = 14
        ' 
        ' optCondition3
        ' 
        optCondition3.AutoSize = True
        optCondition3.Location = New Point(13, 254)
        optCondition3.Margin = New Padding(7, 6, 7, 6)
        optCondition3.Name = "optCondition3"
        optCondition3.Size = New Size(105, 36)
        optCondition3.TabIndex = 13
        optCondition3.TabStop = True
        optCondition3.Text = "Job Is"
        ' 
        ' nudCondition_HasItem
        ' 
        nudCondition_HasItem.Location = New Point(568, 188)
        nudCondition_HasItem.Margin = New Padding(7, 6, 7, 6)
        nudCondition_HasItem.Name = "nudCondition_HasItem"
        nudCondition_HasItem.Size = New Size(260, 39)
        nudCondition_HasItem.TabIndex = 12
        ' 
        ' DarkLabel16
        ' 
        DarkLabel16.AutoSize = True
        DarkLabel16.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel16.Location = New Point(507, 192)
        DarkLabel16.Margin = New Padding(7, 0, 7, 0)
        DarkLabel16.Name = "DarkLabel16"
        DarkLabel16.Size = New Size(28, 32)
        DarkLabel16.TabIndex = 11
        DarkLabel16.Text = "X"
        ' 
        ' cmbCondition_HasItem
        ' 
        cmbCondition_HasItem.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_HasItem.FormattingEnabled = True
        cmbCondition_HasItem.Location = New Point(232, 186)
        cmbCondition_HasItem.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_HasItem.Name = "cmbCondition_HasItem"
        cmbCondition_HasItem.Size = New Size(257, 40)
        cmbCondition_HasItem.TabIndex = 10
        ' 
        ' optCondition2
        ' 
        optCondition2.AutoSize = True
        optCondition2.Location = New Point(13, 188)
        optCondition2.Margin = New Padding(7, 6, 7, 6)
        optCondition2.Name = "optCondition2"
        optCondition2.Size = New Size(139, 36)
        optCondition2.TabIndex = 9
        optCondition2.TabStop = True
        optCondition2.Text = "Has Item"
        ' 
        ' optCondition1
        ' 
        optCondition1.AutoSize = True
        optCondition1.Location = New Point(13, 122)
        optCondition1.Margin = New Padding(7, 6, 7, 6)
        optCondition1.Name = "optCondition1"
        optCondition1.Size = New Size(185, 36)
        optCondition1.TabIndex = 8
        optCondition1.TabStop = True
        optCondition1.Text = "Player Switch"
        ' 
        ' DarkLabel15
        ' 
        DarkLabel15.AutoSize = True
        DarkLabel15.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel15.Location = New Point(507, 126)
        DarkLabel15.Margin = New Padding(7, 0, 7, 0)
        DarkLabel15.Name = "DarkLabel15"
        DarkLabel15.Size = New Size(30, 32)
        DarkLabel15.TabIndex = 7
        DarkLabel15.Text = "is"
        ' 
        ' cmbCondtion_PlayerSwitchCondition
        ' 
        cmbCondtion_PlayerSwitchCondition.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondtion_PlayerSwitchCondition.FormattingEnabled = True
        cmbCondtion_PlayerSwitchCondition.Items.AddRange(New Object() {"False", "True"})
        cmbCondtion_PlayerSwitchCondition.Location = New Point(568, 117)
        cmbCondtion_PlayerSwitchCondition.Margin = New Padding(7, 6, 7, 6)
        cmbCondtion_PlayerSwitchCondition.Name = "cmbCondtion_PlayerSwitchCondition"
        cmbCondtion_PlayerSwitchCondition.Size = New Size(257, 40)
        cmbCondtion_PlayerSwitchCondition.TabIndex = 6
        ' 
        ' cmbCondition_PlayerSwitch
        ' 
        cmbCondition_PlayerSwitch.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_PlayerSwitch.FormattingEnabled = True
        cmbCondition_PlayerSwitch.Location = New Point(232, 117)
        cmbCondition_PlayerSwitch.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_PlayerSwitch.Name = "cmbCondition_PlayerSwitch"
        cmbCondition_PlayerSwitch.Size = New Size(257, 40)
        cmbCondition_PlayerSwitch.TabIndex = 5
        ' 
        ' nudCondition_PlayerVarCondition
        ' 
        nudCondition_PlayerVarCondition.Location = New Point(726, 53)
        nudCondition_PlayerVarCondition.Margin = New Padding(7, 6, 7, 6)
        nudCondition_PlayerVarCondition.Name = "nudCondition_PlayerVarCondition"
        nudCondition_PlayerVarCondition.Size = New Size(102, 39)
        nudCondition_PlayerVarCondition.TabIndex = 4
        ' 
        ' cmbCondition_PlayerVarCompare
        ' 
        cmbCondition_PlayerVarCompare.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_PlayerVarCompare.FormattingEnabled = True
        cmbCondition_PlayerVarCompare.Items.AddRange(New Object() {"Equal To", "Great Than OrElse Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal"})
        cmbCondition_PlayerVarCompare.Location = New Point(511, 51)
        cmbCondition_PlayerVarCompare.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_PlayerVarCompare.Name = "cmbCondition_PlayerVarCompare"
        cmbCondition_PlayerVarCompare.Size = New Size(186, 40)
        cmbCondition_PlayerVarCompare.TabIndex = 3
        ' 
        ' DarkLabel14
        ' 
        DarkLabel14.AutoSize = True
        DarkLabel14.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel14.Location = New Point(468, 60)
        DarkLabel14.Margin = New Padding(7, 0, 7, 0)
        DarkLabel14.Name = "DarkLabel14"
        DarkLabel14.Size = New Size(30, 32)
        DarkLabel14.TabIndex = 2
        DarkLabel14.Text = "is"
        ' 
        ' cmbCondition_PlayerVarIndex
        ' 
        cmbCondition_PlayerVarIndex.DrawMode = DrawMode.OwnerDrawFixed
        cmbCondition_PlayerVarIndex.FormattingEnabled = True
        cmbCondition_PlayerVarIndex.Location = New Point(232, 51)
        cmbCondition_PlayerVarIndex.Margin = New Padding(7, 6, 7, 6)
        cmbCondition_PlayerVarIndex.Name = "cmbCondition_PlayerVarIndex"
        cmbCondition_PlayerVarIndex.Size = New Size(218, 40)
        cmbCondition_PlayerVarIndex.TabIndex = 1
        ' 
        ' optCondition0
        ' 
        optCondition0.AutoSize = True
        optCondition0.Location = New Point(13, 53)
        optCondition0.Margin = New Padding(7, 6, 7, 6)
        optCondition0.Name = "optCondition0"
        optCondition0.Size = New Size(200, 36)
        optCondition0.TabIndex = 0
        optCondition0.TabStop = True
        optCondition0.Text = "Player Variable"
        ' 
        ' fraMoveRouteWait
        ' 
        fraMoveRouteWait.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraMoveRouteWait.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraMoveRouteWait.Controls.Add(btnMoveWaitCancel)
        fraMoveRouteWait.Controls.Add(btnMoveWaitOk)
        fraMoveRouteWait.Controls.Add(DarkLabel79)
        fraMoveRouteWait.Controls.Add(cmbMoveWait)
        fraMoveRouteWait.ForeColor = Color.Gainsboro
        fraMoveRouteWait.Location = New Point(869, 1218)
        fraMoveRouteWait.Margin = New Padding(7, 6, 7, 6)
        fraMoveRouteWait.Name = "fraMoveRouteWait"
        fraMoveRouteWait.Padding = New Padding(7, 6, 7, 6)
        fraMoveRouteWait.Size = New Size(537, 186)
        fraMoveRouteWait.TabIndex = 48
        fraMoveRouteWait.TabStop = False
        fraMoveRouteWait.Text = "Move Route Wait"
        fraMoveRouteWait.Visible = False
        ' 
        ' btnMoveWaitCancel
        ' 
        btnMoveWaitCancel.Location = New Point(362, 113)
        btnMoveWaitCancel.Margin = New Padding(7, 6, 7, 6)
        btnMoveWaitCancel.Name = "btnMoveWaitCancel"
        btnMoveWaitCancel.Padding = New Padding(11, 13, 11, 13)
        btnMoveWaitCancel.Size = New Size(163, 58)
        btnMoveWaitCancel.TabIndex = 26
        btnMoveWaitCancel.Text = "Cancel"
        ' 
        ' btnMoveWaitOk
        ' 
        btnMoveWaitOk.Location = New Point(186, 113)
        btnMoveWaitOk.Margin = New Padding(7, 6, 7, 6)
        btnMoveWaitOk.Name = "btnMoveWaitOk"
        btnMoveWaitOk.Padding = New Padding(11, 13, 11, 13)
        btnMoveWaitOk.Size = New Size(163, 58)
        btnMoveWaitOk.TabIndex = 27
        btnMoveWaitOk.Text = "Ok"
        ' 
        ' DarkLabel79
        ' 
        DarkLabel79.AutoSize = True
        DarkLabel79.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel79.Location = New Point(15, 53)
        DarkLabel79.Margin = New Padding(7, 0, 7, 0)
        DarkLabel79.Name = "DarkLabel79"
        DarkLabel79.Size = New Size(78, 32)
        DarkLabel79.TabIndex = 1
        DarkLabel79.Text = "Event:"
        ' 
        ' cmbMoveWait
        ' 
        cmbMoveWait.DrawMode = DrawMode.OwnerDrawFixed
        cmbMoveWait.FormattingEnabled = True
        cmbMoveWait.Location = New Point(110, 47)
        cmbMoveWait.Margin = New Padding(7, 6, 7, 6)
        cmbMoveWait.Name = "cmbMoveWait"
        cmbMoveWait.Size = New Size(409, 40)
        cmbMoveWait.TabIndex = 0
        ' 
        ' fraCustomScript
        ' 
        fraCustomScript.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraCustomScript.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraCustomScript.Controls.Add(nudCustomScript)
        fraCustomScript.Controls.Add(DarkLabel78)
        fraCustomScript.Controls.Add(btnCustomScriptCancel)
        fraCustomScript.Controls.Add(btnCustomScriptOk)
        fraCustomScript.ForeColor = Color.Gainsboro
        fraCustomScript.Location = New Point(869, 975)
        fraCustomScript.Margin = New Padding(7, 6, 7, 6)
        fraCustomScript.Name = "fraCustomScript"
        fraCustomScript.Padding = New Padding(7, 6, 7, 6)
        fraCustomScript.Size = New Size(537, 235)
        fraCustomScript.TabIndex = 47
        fraCustomScript.TabStop = False
        fraCustomScript.Text = "Execute Custom Script"
        fraCustomScript.Visible = False
        ' 
        ' nudCustomScript
        ' 
        nudCustomScript.Location = New Point(145, 47)
        nudCustomScript.Margin = New Padding(7, 6, 7, 6)
        nudCustomScript.Name = "nudCustomScript"
        nudCustomScript.Size = New Size(366, 39)
        nudCustomScript.TabIndex = 1
        ' 
        ' DarkLabel78
        ' 
        DarkLabel78.AutoSize = True
        DarkLabel78.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel78.Location = New Point(22, 51)
        DarkLabel78.Margin = New Padding(7, 0, 7, 0)
        DarkLabel78.Name = "DarkLabel78"
        DarkLabel78.Size = New Size(69, 32)
        DarkLabel78.TabIndex = 0
        DarkLabel78.Text = "Case:"
        ' 
        ' btnCustomScriptCancel
        ' 
        btnCustomScriptCancel.Location = New Point(349, 111)
        btnCustomScriptCancel.Margin = New Padding(7, 6, 7, 6)
        btnCustomScriptCancel.Name = "btnCustomScriptCancel"
        btnCustomScriptCancel.Padding = New Padding(11, 13, 11, 13)
        btnCustomScriptCancel.Size = New Size(163, 58)
        btnCustomScriptCancel.TabIndex = 24
        btnCustomScriptCancel.Text = "Cancel"
        ' 
        ' btnCustomScriptOk
        ' 
        btnCustomScriptOk.Location = New Point(173, 111)
        btnCustomScriptOk.Margin = New Padding(7, 6, 7, 6)
        btnCustomScriptOk.Name = "btnCustomScriptOk"
        btnCustomScriptOk.Padding = New Padding(11, 13, 11, 13)
        btnCustomScriptOk.Size = New Size(163, 58)
        btnCustomScriptOk.TabIndex = 25
        btnCustomScriptOk.Text = "Ok"
        ' 
        ' fraSetWeather
        ' 
        fraSetWeather.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraSetWeather.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraSetWeather.Controls.Add(btnSetWeatherOk)
        fraSetWeather.Controls.Add(btnSetWeatherCancel)
        fraSetWeather.Controls.Add(DarkLabel76)
        fraSetWeather.Controls.Add(nudWeatherIntensity)
        fraSetWeather.Controls.Add(DarkLabel75)
        fraSetWeather.Controls.Add(CmbWeather)
        fraSetWeather.ForeColor = Color.Gainsboro
        fraSetWeather.Location = New Point(869, 866)
        fraSetWeather.Margin = New Padding(7, 6, 7, 6)
        fraSetWeather.Name = "fraSetWeather"
        fraSetWeather.Padding = New Padding(7, 6, 7, 6)
        fraSetWeather.Size = New Size(537, 235)
        fraSetWeather.TabIndex = 44
        fraSetWeather.TabStop = False
        fraSetWeather.Text = "Set Weather"
        fraSetWeather.Visible = False
        ' 
        ' btnSetWeatherOk
        ' 
        btnSetWeatherOk.Location = New Point(100, 162)
        btnSetWeatherOk.Margin = New Padding(7, 6, 7, 6)
        btnSetWeatherOk.Name = "btnSetWeatherOk"
        btnSetWeatherOk.Padding = New Padding(11, 13, 11, 13)
        btnSetWeatherOk.Size = New Size(163, 58)
        btnSetWeatherOk.TabIndex = 34
        btnSetWeatherOk.Text = "Ok"
        ' 
        ' btnSetWeatherCancel
        ' 
        btnSetWeatherCancel.Location = New Point(275, 162)
        btnSetWeatherCancel.Margin = New Padding(7, 6, 7, 6)
        btnSetWeatherCancel.Name = "btnSetWeatherCancel"
        btnSetWeatherCancel.Padding = New Padding(11, 13, 11, 13)
        btnSetWeatherCancel.Size = New Size(163, 58)
        btnSetWeatherCancel.TabIndex = 33
        btnSetWeatherCancel.Text = "Cancel"
        ' 
        ' DarkLabel76
        ' 
        DarkLabel76.AutoSize = True
        DarkLabel76.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel76.Location = New Point(17, 109)
        DarkLabel76.Margin = New Padding(7, 0, 7, 0)
        DarkLabel76.Name = "DarkLabel76"
        DarkLabel76.Size = New Size(110, 32)
        DarkLabel76.TabIndex = 32
        DarkLabel76.Text = "Intensity:"
        ' 
        ' nudWeatherIntensity
        ' 
        nudWeatherIntensity.Location = New Point(189, 100)
        nudWeatherIntensity.Margin = New Padding(7, 6, 7, 6)
        nudWeatherIntensity.Name = "nudWeatherIntensity"
        nudWeatherIntensity.Size = New Size(336, 39)
        nudWeatherIntensity.TabIndex = 31
        ' 
        ' DarkLabel75
        ' 
        DarkLabel75.AutoSize = True
        DarkLabel75.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel75.Location = New Point(13, 45)
        DarkLabel75.Margin = New Padding(7, 0, 7, 0)
        DarkLabel75.Name = "DarkLabel75"
        DarkLabel75.Size = New Size(161, 32)
        DarkLabel75.TabIndex = 1
        DarkLabel75.Text = "Weather Type"
        ' 
        ' CmbWeather
        ' 
        CmbWeather.DrawMode = DrawMode.OwnerDrawFixed
        CmbWeather.FormattingEnabled = True
        CmbWeather.Items.AddRange(New Object() {"None", "Rain", "Snow", "Hail", "Sand Storm", "Storm"})
        CmbWeather.Location = New Point(186, 36)
        CmbWeather.Margin = New Padding(7, 6, 7, 6)
        CmbWeather.Name = "CmbWeather"
        CmbWeather.Size = New Size(331, 40)
        CmbWeather.TabIndex = 0
        ' 
        ' fraSpawnNpc
        ' 
        fraSpawnNpc.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraSpawnNpc.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraSpawnNpc.Controls.Add(btnSpawnNpcOk)
        fraSpawnNpc.Controls.Add(btnSpawnNpcCancel)
        fraSpawnNpc.Controls.Add(cmbSpawnNpc)
        fraSpawnNpc.ForeColor = Color.Gainsboro
        fraSpawnNpc.Location = New Point(869, 1013)
        fraSpawnNpc.Margin = New Padding(7, 6, 7, 6)
        fraSpawnNpc.Name = "fraSpawnNpc"
        fraSpawnNpc.Padding = New Padding(7, 6, 7, 6)
        fraSpawnNpc.Size = New Size(537, 190)
        fraSpawnNpc.TabIndex = 46
        fraSpawnNpc.TabStop = False
        fraSpawnNpc.Text = "Spawn Npc"
        fraSpawnNpc.Visible = False
        ' 
        ' btnSpawnNpcOk
        ' 
        btnSpawnNpcOk.Location = New Point(100, 115)
        btnSpawnNpcOk.Margin = New Padding(7, 6, 7, 6)
        btnSpawnNpcOk.Name = "btnSpawnNpcOk"
        btnSpawnNpcOk.Padding = New Padding(11, 13, 11, 13)
        btnSpawnNpcOk.Size = New Size(163, 58)
        btnSpawnNpcOk.TabIndex = 27
        btnSpawnNpcOk.Text = "Ok"
        ' 
        ' btnSpawnNpcCancel
        ' 
        btnSpawnNpcCancel.Location = New Point(275, 115)
        btnSpawnNpcCancel.Margin = New Padding(7, 6, 7, 6)
        btnSpawnNpcCancel.Name = "btnSpawnNpcCancel"
        btnSpawnNpcCancel.Padding = New Padding(11, 13, 11, 13)
        btnSpawnNpcCancel.Size = New Size(163, 58)
        btnSpawnNpcCancel.TabIndex = 26
        btnSpawnNpcCancel.Text = "Cancel"
        ' 
        ' cmbSpawnNpc
        ' 
        cmbSpawnNpc.DrawMode = DrawMode.OwnerDrawFixed
        cmbSpawnNpc.FormattingEnabled = True
        cmbSpawnNpc.Location = New Point(13, 47)
        cmbSpawnNpc.Margin = New Padding(7, 6, 7, 6)
        cmbSpawnNpc.Name = "cmbSpawnNpc"
        cmbSpawnNpc.Size = New Size(502, 40)
        cmbSpawnNpc.TabIndex = 0
        ' 
        ' fraGiveExp
        ' 
        fraGiveExp.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraGiveExp.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraGiveExp.Controls.Add(btnGiveExpOk)
        fraGiveExp.Controls.Add(btnGiveExpCancel)
        fraGiveExp.Controls.Add(nudGiveExp)
        fraGiveExp.Controls.Add(DarkLabel77)
        fraGiveExp.ForeColor = Color.Gainsboro
        fraGiveExp.Location = New Point(869, 866)
        fraGiveExp.Margin = New Padding(7, 6, 7, 6)
        fraGiveExp.Name = "fraGiveExp"
        fraGiveExp.Padding = New Padding(7, 6, 7, 6)
        fraGiveExp.Size = New Size(537, 179)
        fraGiveExp.TabIndex = 45
        fraGiveExp.TabStop = False
        fraGiveExp.Text = "Give Experience"
        fraGiveExp.Visible = False
        ' 
        ' btnGiveExpOk
        ' 
        btnGiveExpOk.Location = New Point(108, 111)
        btnGiveExpOk.Margin = New Padding(7, 6, 7, 6)
        btnGiveExpOk.Name = "btnGiveExpOk"
        btnGiveExpOk.Padding = New Padding(11, 13, 11, 13)
        btnGiveExpOk.Size = New Size(163, 58)
        btnGiveExpOk.TabIndex = 27
        btnGiveExpOk.Text = "Ok"
        ' 
        ' btnGiveExpCancel
        ' 
        btnGiveExpCancel.Location = New Point(284, 111)
        btnGiveExpCancel.Margin = New Padding(7, 6, 7, 6)
        btnGiveExpCancel.Name = "btnGiveExpCancel"
        btnGiveExpCancel.Padding = New Padding(11, 13, 11, 13)
        btnGiveExpCancel.Size = New Size(163, 58)
        btnGiveExpCancel.TabIndex = 26
        btnGiveExpCancel.Text = "Cancel"
        ' 
        ' nudGiveExp
        ' 
        nudGiveExp.Location = New Point(167, 47)
        nudGiveExp.Margin = New Padding(7, 6, 7, 6)
        nudGiveExp.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        nudGiveExp.Name = "nudGiveExp"
        nudGiveExp.Size = New Size(357, 39)
        nudGiveExp.TabIndex = 20
        ' 
        ' DarkLabel77
        ' 
        DarkLabel77.AutoSize = True
        DarkLabel77.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel77.Location = New Point(13, 51)
        DarkLabel77.Margin = New Padding(7, 0, 7, 0)
        DarkLabel77.Name = "DarkLabel77"
        DarkLabel77.Size = New Size(110, 32)
        DarkLabel77.TabIndex = 0
        DarkLabel77.Text = "Give Exp:"
        ' 
        ' fraEndQuest
        ' 
        fraEndQuest.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraEndQuest.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraEndQuest.Controls.Add(btnEndQuestOk)
        fraEndQuest.Controls.Add(btnEndQuestCancel)
        fraEndQuest.Controls.Add(cmbEndQuest)
        fraEndQuest.ForeColor = Color.Gainsboro
        fraEndQuest.Location = New Point(869, 1024)
        fraEndQuest.Margin = New Padding(7, 6, 7, 6)
        fraEndQuest.Name = "fraEndQuest"
        fraEndQuest.Padding = New Padding(7, 6, 7, 6)
        fraEndQuest.Size = New Size(537, 179)
        fraEndQuest.TabIndex = 43
        fraEndQuest.TabStop = False
        fraEndQuest.Text = "End Quest"
        fraEndQuest.Visible = False
        ' 
        ' btnEndQuestOk
        ' 
        btnEndQuestOk.Location = New Point(100, 109)
        btnEndQuestOk.Margin = New Padding(7, 6, 7, 6)
        btnEndQuestOk.Name = "btnEndQuestOk"
        btnEndQuestOk.Padding = New Padding(11, 13, 11, 13)
        btnEndQuestOk.Size = New Size(163, 58)
        btnEndQuestOk.TabIndex = 30
        btnEndQuestOk.Text = "Ok"
        ' 
        ' btnEndQuestCancel
        ' 
        btnEndQuestCancel.Location = New Point(275, 109)
        btnEndQuestCancel.Margin = New Padding(7, 6, 7, 6)
        btnEndQuestCancel.Name = "btnEndQuestCancel"
        btnEndQuestCancel.Padding = New Padding(11, 13, 11, 13)
        btnEndQuestCancel.Size = New Size(163, 58)
        btnEndQuestCancel.TabIndex = 29
        btnEndQuestCancel.Text = "Cancel"
        ' 
        ' cmbEndQuest
        ' 
        cmbEndQuest.DrawMode = DrawMode.OwnerDrawFixed
        cmbEndQuest.FormattingEnabled = True
        cmbEndQuest.Location = New Point(71, 36)
        cmbEndQuest.Margin = New Padding(7, 6, 7, 6)
        cmbEndQuest.Name = "cmbEndQuest"
        cmbEndQuest.Size = New Size(403, 40)
        cmbEndQuest.TabIndex = 28
        ' 
        ' fraSetAccess
        ' 
        fraSetAccess.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraSetAccess.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraSetAccess.Controls.Add(btnSetAccessOk)
        fraSetAccess.Controls.Add(btnSetAccessCancel)
        fraSetAccess.Controls.Add(cmbSetAccess)
        fraSetAccess.ForeColor = Color.Gainsboro
        fraSetAccess.Location = New Point(869, 868)
        fraSetAccess.Margin = New Padding(7, 6, 7, 6)
        fraSetAccess.Name = "fraSetAccess"
        fraSetAccess.Padding = New Padding(7, 6, 7, 6)
        fraSetAccess.Size = New Size(537, 196)
        fraSetAccess.TabIndex = 42
        fraSetAccess.TabStop = False
        fraSetAccess.Text = "Set Access"
        fraSetAccess.Visible = False
        ' 
        ' btnSetAccessOk
        ' 
        btnSetAccessOk.Location = New Point(100, 117)
        btnSetAccessOk.Margin = New Padding(7, 6, 7, 6)
        btnSetAccessOk.Name = "btnSetAccessOk"
        btnSetAccessOk.Padding = New Padding(11, 13, 11, 13)
        btnSetAccessOk.Size = New Size(163, 58)
        btnSetAccessOk.TabIndex = 27
        btnSetAccessOk.Text = "Ok"
        ' 
        ' btnSetAccessCancel
        ' 
        btnSetAccessCancel.Location = New Point(275, 117)
        btnSetAccessCancel.Margin = New Padding(7, 6, 7, 6)
        btnSetAccessCancel.Name = "btnSetAccessCancel"
        btnSetAccessCancel.Padding = New Padding(11, 13, 11, 13)
        btnSetAccessCancel.Size = New Size(163, 58)
        btnSetAccessCancel.TabIndex = 26
        btnSetAccessCancel.Text = "Cancel"
        ' 
        ' cmbSetAccess
        ' 
        cmbSetAccess.DrawMode = DrawMode.OwnerDrawFixed
        cmbSetAccess.FormattingEnabled = True
        cmbSetAccess.Items.AddRange(New Object() {"0: Player", "1: Moderator", "2: Mapper", "3: Developer", "4: Creator"})
        cmbSetAccess.Location = New Point(71, 47)
        cmbSetAccess.Margin = New Padding(7, 6, 7, 6)
        cmbSetAccess.Name = "cmbSetAccess"
        cmbSetAccess.Size = New Size(403, 40)
        cmbSetAccess.TabIndex = 0
        ' 
        ' fraOpenShop
        ' 
        fraOpenShop.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraOpenShop.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraOpenShop.Controls.Add(btnOpenShopOk)
        fraOpenShop.Controls.Add(btnOpenShopCancel)
        fraOpenShop.Controls.Add(cmbOpenShop)
        fraOpenShop.ForeColor = Color.Gainsboro
        fraOpenShop.Location = New Point(873, 533)
        fraOpenShop.Margin = New Padding(7, 6, 7, 6)
        fraOpenShop.Name = "fraOpenShop"
        fraOpenShop.Padding = New Padding(7, 6, 7, 6)
        fraOpenShop.Size = New Size(533, 194)
        fraOpenShop.TabIndex = 39
        fraOpenShop.TabStop = False
        fraOpenShop.Text = "Open Shop"
        fraOpenShop.Visible = False
        ' 
        ' btnOpenShopOk
        ' 
        btnOpenShopOk.Location = New Point(95, 115)
        btnOpenShopOk.Margin = New Padding(7, 6, 7, 6)
        btnOpenShopOk.Name = "btnOpenShopOk"
        btnOpenShopOk.Padding = New Padding(11, 13, 11, 13)
        btnOpenShopOk.Size = New Size(163, 58)
        btnOpenShopOk.TabIndex = 27
        btnOpenShopOk.Text = "Ok"
        ' 
        ' btnOpenShopCancel
        ' 
        btnOpenShopCancel.Location = New Point(271, 115)
        btnOpenShopCancel.Margin = New Padding(7, 6, 7, 6)
        btnOpenShopCancel.Name = "btnOpenShopCancel"
        btnOpenShopCancel.Padding = New Padding(11, 13, 11, 13)
        btnOpenShopCancel.Size = New Size(163, 58)
        btnOpenShopCancel.TabIndex = 26
        btnOpenShopCancel.Text = "Cancel"
        ' 
        ' cmbOpenShop
        ' 
        cmbOpenShop.DrawMode = DrawMode.OwnerDrawFixed
        cmbOpenShop.FormattingEnabled = True
        cmbOpenShop.Location = New Point(19, 49)
        cmbOpenShop.Margin = New Padding(7, 6, 7, 6)
        cmbOpenShop.Name = "cmbOpenShop"
        cmbOpenShop.Size = New Size(485, 40)
        cmbOpenShop.TabIndex = 0
        ' 
        ' fraChangeLevel
        ' 
        fraChangeLevel.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraChangeLevel.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraChangeLevel.Controls.Add(btnChangeLevelOk)
        fraChangeLevel.Controls.Add(btnChangeLevelCancel)
        fraChangeLevel.Controls.Add(DarkLabel65)
        fraChangeLevel.Controls.Add(nudChangeLevel)
        fraChangeLevel.ForeColor = Color.Gainsboro
        fraChangeLevel.Location = New Point(869, 721)
        fraChangeLevel.Margin = New Padding(7, 6, 7, 6)
        fraChangeLevel.Name = "fraChangeLevel"
        fraChangeLevel.Padding = New Padding(7, 6, 7, 6)
        fraChangeLevel.Size = New Size(537, 177)
        fraChangeLevel.TabIndex = 38
        fraChangeLevel.TabStop = False
        fraChangeLevel.Text = "Change Level"
        fraChangeLevel.Visible = False
        ' 
        ' btnChangeLevelOk
        ' 
        btnChangeLevelOk.Location = New Point(100, 111)
        btnChangeLevelOk.Margin = New Padding(7, 6, 7, 6)
        btnChangeLevelOk.Name = "btnChangeLevelOk"
        btnChangeLevelOk.Padding = New Padding(11, 13, 11, 13)
        btnChangeLevelOk.Size = New Size(163, 58)
        btnChangeLevelOk.TabIndex = 27
        btnChangeLevelOk.Text = "Ok"
        ' 
        ' btnChangeLevelCancel
        ' 
        btnChangeLevelCancel.Location = New Point(275, 111)
        btnChangeLevelCancel.Margin = New Padding(7, 6, 7, 6)
        btnChangeLevelCancel.Name = "btnChangeLevelCancel"
        btnChangeLevelCancel.Padding = New Padding(11, 13, 11, 13)
        btnChangeLevelCancel.Size = New Size(163, 58)
        btnChangeLevelCancel.TabIndex = 26
        btnChangeLevelCancel.Text = "Cancel"
        ' 
        ' DarkLabel65
        ' 
        DarkLabel65.AutoSize = True
        DarkLabel65.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel65.Location = New Point(15, 51)
        DarkLabel65.Margin = New Padding(7, 0, 7, 0)
        DarkLabel65.Name = "DarkLabel65"
        DarkLabel65.Size = New Size(74, 32)
        DarkLabel65.TabIndex = 24
        DarkLabel65.Text = "Level:"
        ' 
        ' nudChangeLevel
        ' 
        nudChangeLevel.Location = New Point(130, 47)
        nudChangeLevel.Margin = New Padding(7, 6, 7, 6)
        nudChangeLevel.Name = "nudChangeLevel"
        nudChangeLevel.Size = New Size(260, 39)
        nudChangeLevel.TabIndex = 23
        ' 
        ' fraChangeGender
        ' 
        fraChangeGender.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraChangeGender.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraChangeGender.Controls.Add(btnChangeGenderOk)
        fraChangeGender.Controls.Add(btnChangeGenderCancel)
        fraChangeGender.Controls.Add(optChangeSexFemale)
        fraChangeGender.Controls.Add(optChangeSexMale)
        fraChangeGender.ForeColor = Color.Gainsboro
        fraChangeGender.Location = New Point(869, 896)
        fraChangeGender.Margin = New Padding(7, 6, 7, 6)
        fraChangeGender.Name = "fraChangeGender"
        fraChangeGender.Padding = New Padding(7, 6, 7, 6)
        fraChangeGender.Size = New Size(537, 177)
        fraChangeGender.TabIndex = 37
        fraChangeGender.TabStop = False
        fraChangeGender.Text = "Change Player Gender"
        fraChangeGender.Visible = False
        ' 
        ' btnChangeGenderOk
        ' 
        btnChangeGenderOk.Location = New Point(85, 102)
        btnChangeGenderOk.Margin = New Padding(7, 6, 7, 6)
        btnChangeGenderOk.Name = "btnChangeGenderOk"
        btnChangeGenderOk.Padding = New Padding(11, 13, 11, 13)
        btnChangeGenderOk.Size = New Size(163, 58)
        btnChangeGenderOk.TabIndex = 27
        btnChangeGenderOk.Text = "Ok"
        ' 
        ' btnChangeGenderCancel
        ' 
        btnChangeGenderCancel.Location = New Point(260, 102)
        btnChangeGenderCancel.Margin = New Padding(7, 6, 7, 6)
        btnChangeGenderCancel.Name = "btnChangeGenderCancel"
        btnChangeGenderCancel.Padding = New Padding(11, 13, 11, 13)
        btnChangeGenderCancel.Size = New Size(163, 58)
        btnChangeGenderCancel.TabIndex = 26
        btnChangeGenderCancel.Text = "Cancel"
        ' 
        ' optChangeSexFemale
        ' 
        optChangeSexFemale.AutoSize = True
        optChangeSexFemale.Location = New Point(305, 47)
        optChangeSexFemale.Margin = New Padding(7, 6, 7, 6)
        optChangeSexFemale.Name = "optChangeSexFemale"
        optChangeSexFemale.Size = New Size(122, 36)
        optChangeSexFemale.TabIndex = 1
        optChangeSexFemale.TabStop = True
        optChangeSexFemale.Text = "Female"
        ' 
        ' optChangeSexMale
        ' 
        optChangeSexMale.AutoSize = True
        optChangeSexMale.Location = New Point(113, 47)
        optChangeSexMale.Margin = New Padding(7, 6, 7, 6)
        optChangeSexMale.Name = "optChangeSexMale"
        optChangeSexMale.Size = New Size(98, 36)
        optChangeSexMale.TabIndex = 0
        optChangeSexMale.TabStop = True
        optChangeSexMale.Text = "Male"
        ' 
        ' fraGoToLabel
        ' 
        fraGoToLabel.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraGoToLabel.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraGoToLabel.Controls.Add(btnGoToLabelOk)
        fraGoToLabel.Controls.Add(btnGoToLabelCancel)
        fraGoToLabel.Controls.Add(txtGotoLabel)
        fraGoToLabel.Controls.Add(DarkLabel60)
        fraGoToLabel.ForeColor = Color.Gainsboro
        fraGoToLabel.Location = New Point(869, 627)
        fraGoToLabel.Margin = New Padding(7, 6, 7, 6)
        fraGoToLabel.Name = "fraGoToLabel"
        fraGoToLabel.Padding = New Padding(7, 6, 7, 6)
        fraGoToLabel.Size = New Size(537, 179)
        fraGoToLabel.TabIndex = 35
        fraGoToLabel.TabStop = False
        fraGoToLabel.Text = "GoTo Label"
        fraGoToLabel.Visible = False
        ' 
        ' btnGoToLabelOk
        ' 
        btnGoToLabelOk.Location = New Point(186, 109)
        btnGoToLabelOk.Margin = New Padding(7, 6, 7, 6)
        btnGoToLabelOk.Name = "btnGoToLabelOk"
        btnGoToLabelOk.Padding = New Padding(11, 13, 11, 13)
        btnGoToLabelOk.Size = New Size(163, 58)
        btnGoToLabelOk.TabIndex = 27
        btnGoToLabelOk.Text = "Ok"
        ' 
        ' btnGoToLabelCancel
        ' 
        btnGoToLabelCancel.Location = New Point(362, 109)
        btnGoToLabelCancel.Margin = New Padding(7, 6, 7, 6)
        btnGoToLabelCancel.Name = "btnGoToLabelCancel"
        btnGoToLabelCancel.Padding = New Padding(11, 13, 11, 13)
        btnGoToLabelCancel.Size = New Size(163, 58)
        btnGoToLabelCancel.TabIndex = 26
        btnGoToLabelCancel.Text = "Cancel"
        ' 
        ' txtGotoLabel
        ' 
        txtGotoLabel.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtGotoLabel.BorderStyle = BorderStyle.FixedSingle
        txtGotoLabel.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtGotoLabel.Location = New Point(169, 45)
        txtGotoLabel.Margin = New Padding(7, 6, 7, 6)
        txtGotoLabel.Name = "txtGotoLabel"
        txtGotoLabel.Size = New Size(353, 39)
        txtGotoLabel.TabIndex = 1
        ' 
        ' DarkLabel60
        ' 
        DarkLabel60.AutoSize = True
        DarkLabel60.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel60.Location = New Point(7, 49)
        DarkLabel60.Margin = New Padding(7, 0, 7, 0)
        DarkLabel60.Name = "DarkLabel60"
        DarkLabel60.Size = New Size(146, 32)
        DarkLabel60.TabIndex = 0
        DarkLabel60.Text = "Label Name:"
        ' 
        ' fraShowChoices
        ' 
        fraShowChoices.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraShowChoices.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraShowChoices.Controls.Add(txtChoices4)
        fraShowChoices.Controls.Add(txtChoices3)
        fraShowChoices.Controls.Add(txtChoices2)
        fraShowChoices.Controls.Add(txtChoices1)
        fraShowChoices.Controls.Add(DarkLabel56)
        fraShowChoices.Controls.Add(DarkLabel57)
        fraShowChoices.Controls.Add(DarkLabel55)
        fraShowChoices.Controls.Add(DarkLabel54)
        fraShowChoices.Controls.Add(DarkLabel52)
        fraShowChoices.Controls.Add(txtChoicePrompt)
        fraShowChoices.Controls.Add(btnShowChoicesOk)
        fraShowChoices.Controls.Add(picShowChoicesFace)
        fraShowChoices.Controls.Add(btnShowChoicesCancel)
        fraShowChoices.Controls.Add(DarkLabel53)
        fraShowChoices.Controls.Add(nudShowChoicesFace)
        fraShowChoices.ForeColor = Color.Gainsboro
        fraShowChoices.Location = New Point(869, 254)
        fraShowChoices.Margin = New Padding(7, 6, 7, 6)
        fraShowChoices.Name = "fraShowChoices"
        fraShowChoices.Padding = New Padding(7, 6, 7, 6)
        fraShowChoices.Size = New Size(537, 819)
        fraShowChoices.TabIndex = 32
        fraShowChoices.TabStop = False
        fraShowChoices.Text = "Show Choices"
        fraShowChoices.Visible = False
        ' 
        ' txtChoices4
        ' 
        txtChoices4.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtChoices4.BorderStyle = BorderStyle.FixedSingle
        txtChoices4.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtChoices4.Location = New Point(305, 429)
        txtChoices4.Margin = New Padding(7, 6, 7, 6)
        txtChoices4.Name = "txtChoices4"
        txtChoices4.Size = New Size(214, 39)
        txtChoices4.TabIndex = 34
        ' 
        ' txtChoices3
        ' 
        txtChoices3.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtChoices3.BorderStyle = BorderStyle.FixedSingle
        txtChoices3.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtChoices3.Location = New Point(13, 427)
        txtChoices3.Margin = New Padding(7, 6, 7, 6)
        txtChoices3.Name = "txtChoices3"
        txtChoices3.Size = New Size(214, 39)
        txtChoices3.TabIndex = 33
        ' 
        ' txtChoices2
        ' 
        txtChoices2.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtChoices2.BorderStyle = BorderStyle.FixedSingle
        txtChoices2.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtChoices2.Location = New Point(305, 331)
        txtChoices2.Margin = New Padding(7, 6, 7, 6)
        txtChoices2.Name = "txtChoices2"
        txtChoices2.Size = New Size(214, 39)
        txtChoices2.TabIndex = 32
        ' 
        ' txtChoices1
        ' 
        txtChoices1.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtChoices1.BorderStyle = BorderStyle.FixedSingle
        txtChoices1.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtChoices1.Location = New Point(13, 331)
        txtChoices1.Margin = New Padding(7, 6, 7, 6)
        txtChoices1.Name = "txtChoices1"
        txtChoices1.Size = New Size(214, 39)
        txtChoices1.TabIndex = 31
        ' 
        ' DarkLabel56
        ' 
        DarkLabel56.AutoSize = True
        DarkLabel56.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel56.Location = New Point(299, 386)
        DarkLabel56.Margin = New Padding(7, 0, 7, 0)
        DarkLabel56.Name = "DarkLabel56"
        DarkLabel56.Size = New Size(107, 32)
        DarkLabel56.TabIndex = 30
        DarkLabel56.Text = "Choice 4"
        ' 
        ' DarkLabel57
        ' 
        DarkLabel57.AutoSize = True
        DarkLabel57.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel57.Location = New Point(15, 386)
        DarkLabel57.Margin = New Padding(7, 0, 7, 0)
        DarkLabel57.Name = "DarkLabel57"
        DarkLabel57.Size = New Size(107, 32)
        DarkLabel57.TabIndex = 29
        DarkLabel57.Text = "Choice 3"
        ' 
        ' DarkLabel55
        ' 
        DarkLabel55.AutoSize = True
        DarkLabel55.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel55.Location = New Point(299, 290)
        DarkLabel55.Margin = New Padding(7, 0, 7, 0)
        DarkLabel55.Name = "DarkLabel55"
        DarkLabel55.Size = New Size(107, 32)
        DarkLabel55.TabIndex = 28
        DarkLabel55.Text = "Choice 2"
        ' 
        ' DarkLabel54
        ' 
        DarkLabel54.AutoSize = True
        DarkLabel54.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel54.Location = New Point(13, 290)
        DarkLabel54.Margin = New Padding(7, 0, 7, 0)
        DarkLabel54.Name = "DarkLabel54"
        DarkLabel54.Size = New Size(107, 32)
        DarkLabel54.TabIndex = 27
        DarkLabel54.Text = "Choice 1"
        ' 
        ' DarkLabel52
        ' 
        DarkLabel52.AutoSize = True
        DarkLabel52.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel52.Location = New Point(15, 47)
        DarkLabel52.Margin = New Padding(7, 0, 7, 0)
        DarkLabel52.Name = "DarkLabel52"
        DarkLabel52.Size = New Size(92, 32)
        DarkLabel52.TabIndex = 26
        DarkLabel52.Text = "Prompt"
        ' 
        ' txtChoicePrompt
        ' 
        txtChoicePrompt.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtChoicePrompt.BorderStyle = BorderStyle.FixedSingle
        txtChoicePrompt.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtChoicePrompt.Location = New Point(19, 94)
        txtChoicePrompt.Margin = New Padding(7, 6, 7, 6)
        txtChoicePrompt.Multiline = True
        txtChoicePrompt.Name = "txtChoicePrompt"
        txtChoicePrompt.Size = New Size(492, 188)
        txtChoicePrompt.TabIndex = 21
        ' 
        ' btnShowChoicesOk
        ' 
        btnShowChoicesOk.Location = New Point(182, 751)
        btnShowChoicesOk.Margin = New Padding(7, 6, 7, 6)
        btnShowChoicesOk.Name = "btnShowChoicesOk"
        btnShowChoicesOk.Padding = New Padding(11, 13, 11, 13)
        btnShowChoicesOk.Size = New Size(163, 58)
        btnShowChoicesOk.TabIndex = 25
        btnShowChoicesOk.Text = "Ok"
        ' 
        ' picShowChoicesFace
        ' 
        picShowChoicesFace.BackColor = Color.Black
        picShowChoicesFace.BackgroundImageLayout = ImageLayout.Zoom
        picShowChoicesFace.Location = New Point(13, 491)
        picShowChoicesFace.Margin = New Padding(7, 6, 7, 6)
        picShowChoicesFace.Name = "picShowChoicesFace"
        picShowChoicesFace.Size = New Size(217, 228)
        picShowChoicesFace.TabIndex = 2
        picShowChoicesFace.TabStop = False
        ' 
        ' btnShowChoicesCancel
        ' 
        btnShowChoicesCancel.Location = New Point(357, 751)
        btnShowChoicesCancel.Margin = New Padding(7, 6, 7, 6)
        btnShowChoicesCancel.Name = "btnShowChoicesCancel"
        btnShowChoicesCancel.Padding = New Padding(11, 13, 11, 13)
        btnShowChoicesCancel.Size = New Size(163, 58)
        btnShowChoicesCancel.TabIndex = 24
        btnShowChoicesCancel.Text = "Cancel"
        ' 
        ' DarkLabel53
        ' 
        DarkLabel53.AutoSize = True
        DarkLabel53.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel53.Location = New Point(236, 674)
        DarkLabel53.Margin = New Padding(7, 0, 7, 0)
        DarkLabel53.Name = "DarkLabel53"
        DarkLabel53.Size = New Size(66, 32)
        DarkLabel53.TabIndex = 22
        DarkLabel53.Text = "Face:"
        ' 
        ' nudShowChoicesFace
        ' 
        nudShowChoicesFace.Location = New Point(316, 670)
        nudShowChoicesFace.Margin = New Padding(7, 6, 7, 6)
        nudShowChoicesFace.Name = "nudShowChoicesFace"
        nudShowChoicesFace.Size = New Size(199, 39)
        nudShowChoicesFace.TabIndex = 23
        ' 
        ' fraPlayerVariable
        ' 
        fraPlayerVariable.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraPlayerVariable.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraPlayerVariable.Controls.Add(nudVariableData2)
        fraPlayerVariable.Controls.Add(optVariableAction2)
        fraPlayerVariable.Controls.Add(btnPlayerVarOk)
        fraPlayerVariable.Controls.Add(btnPlayerVarCancel)
        fraPlayerVariable.Controls.Add(DarkLabel51)
        fraPlayerVariable.Controls.Add(DarkLabel50)
        fraPlayerVariable.Controls.Add(nudVariableData4)
        fraPlayerVariable.Controls.Add(nudVariableData3)
        fraPlayerVariable.Controls.Add(optVariableAction3)
        fraPlayerVariable.Controls.Add(optVariableAction1)
        fraPlayerVariable.Controls.Add(nudVariableData1)
        fraPlayerVariable.Controls.Add(nudVariableData0)
        fraPlayerVariable.Controls.Add(optVariableAction0)
        fraPlayerVariable.Controls.Add(cmbVariable)
        fraPlayerVariable.Controls.Add(DarkLabel49)
        fraPlayerVariable.ForeColor = Color.Gainsboro
        fraPlayerVariable.Location = New Point(869, 693)
        fraPlayerVariable.Margin = New Padding(7, 6, 7, 6)
        fraPlayerVariable.Name = "fraPlayerVariable"
        fraPlayerVariable.Padding = New Padding(7, 6, 7, 6)
        fraPlayerVariable.Size = New Size(533, 380)
        fraPlayerVariable.TabIndex = 31
        fraPlayerVariable.TabStop = False
        fraPlayerVariable.Text = "Player Variable"
        fraPlayerVariable.Visible = False
        ' 
        ' nudVariableData2
        ' 
        nudVariableData2.Location = New Point(260, 177)
        nudVariableData2.Margin = New Padding(7, 6, 7, 6)
        nudVariableData2.Name = "nudVariableData2"
        nudVariableData2.Size = New Size(260, 39)
        nudVariableData2.TabIndex = 29
        ' 
        ' optVariableAction2
        ' 
        optVariableAction2.AutoSize = True
        optVariableAction2.Location = New Point(13, 177)
        optVariableAction2.Margin = New Padding(7, 6, 7, 6)
        optVariableAction2.Name = "optVariableAction2"
        optVariableAction2.Size = New Size(133, 36)
        optVariableAction2.TabIndex = 28
        optVariableAction2.TabStop = True
        optVariableAction2.Text = "Subtract"
        ' 
        ' btnPlayerVarOk
        ' 
        btnPlayerVarOk.Location = New Point(182, 305)
        btnPlayerVarOk.Margin = New Padding(7, 6, 7, 6)
        btnPlayerVarOk.Name = "btnPlayerVarOk"
        btnPlayerVarOk.Padding = New Padding(11, 13, 11, 13)
        btnPlayerVarOk.Size = New Size(163, 58)
        btnPlayerVarOk.TabIndex = 27
        btnPlayerVarOk.Text = "Ok"
        ' 
        ' btnPlayerVarCancel
        ' 
        btnPlayerVarCancel.Location = New Point(357, 305)
        btnPlayerVarCancel.Margin = New Padding(7, 6, 7, 6)
        btnPlayerVarCancel.Name = "btnPlayerVarCancel"
        btnPlayerVarCancel.Padding = New Padding(11, 13, 11, 13)
        btnPlayerVarCancel.Size = New Size(163, 58)
        btnPlayerVarCancel.TabIndex = 26
        btnPlayerVarCancel.Text = "Cancel"
        ' 
        ' DarkLabel51
        ' 
        DarkLabel51.AutoSize = True
        DarkLabel51.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel51.Location = New Point(163, 245)
        DarkLabel51.Margin = New Padding(7, 0, 7, 0)
        DarkLabel51.Name = "DarkLabel51"
        DarkLabel51.Size = New Size(61, 32)
        DarkLabel51.TabIndex = 16
        DarkLabel51.Text = "Low:"
        ' 
        ' DarkLabel50
        ' 
        DarkLabel50.AutoSize = True
        DarkLabel50.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel50.Location = New Point(342, 245)
        DarkLabel50.Margin = New Padding(7, 0, 7, 0)
        DarkLabel50.Name = "DarkLabel50"
        DarkLabel50.Size = New Size(70, 32)
        DarkLabel50.TabIndex = 15
        DarkLabel50.Text = "High:"
        ' 
        ' nudVariableData4
        ' 
        nudVariableData4.Location = New Point(425, 241)
        nudVariableData4.Margin = New Padding(7, 6, 7, 6)
        nudVariableData4.Name = "nudVariableData4"
        nudVariableData4.Size = New Size(95, 39)
        nudVariableData4.TabIndex = 14
        ' 
        ' nudVariableData3
        ' 
        nudVariableData3.Location = New Point(241, 241)
        nudVariableData3.Margin = New Padding(7, 6, 7, 6)
        nudVariableData3.Name = "nudVariableData3"
        nudVariableData3.Size = New Size(95, 39)
        nudVariableData3.TabIndex = 13
        ' 
        ' optVariableAction3
        ' 
        optVariableAction3.AutoSize = True
        optVariableAction3.Location = New Point(13, 241)
        optVariableAction3.Margin = New Padding(7, 6, 7, 6)
        optVariableAction3.Name = "optVariableAction3"
        optVariableAction3.Size = New Size(134, 36)
        optVariableAction3.TabIndex = 12
        optVariableAction3.TabStop = True
        optVariableAction3.Text = "Random"
        ' 
        ' optVariableAction1
        ' 
        optVariableAction1.AutoSize = True
        optVariableAction1.Location = New Point(316, 113)
        optVariableAction1.Margin = New Padding(7, 6, 7, 6)
        optVariableAction1.Name = "optVariableAction1"
        optVariableAction1.Size = New Size(88, 36)
        optVariableAction1.TabIndex = 11
        optVariableAction1.TabStop = True
        optVariableAction1.Text = "Add"
        ' 
        ' nudVariableData1
        ' 
        nudVariableData1.Location = New Point(425, 113)
        nudVariableData1.Margin = New Padding(7, 6, 7, 6)
        nudVariableData1.Name = "nudVariableData1"
        nudVariableData1.Size = New Size(95, 39)
        nudVariableData1.TabIndex = 10
        ' 
        ' nudVariableData0
        ' 
        nudVariableData0.Location = New Point(134, 113)
        nudVariableData0.Margin = New Padding(7, 6, 7, 6)
        nudVariableData0.Name = "nudVariableData0"
        nudVariableData0.Size = New Size(95, 39)
        nudVariableData0.TabIndex = 9
        ' 
        ' optVariableAction0
        ' 
        optVariableAction0.AutoSize = True
        optVariableAction0.Location = New Point(13, 113)
        optVariableAction0.Margin = New Padding(7, 6, 7, 6)
        optVariableAction0.Name = "optVariableAction0"
        optVariableAction0.Size = New Size(79, 36)
        optVariableAction0.TabIndex = 2
        optVariableAction0.TabStop = True
        optVariableAction0.Text = "Set"
        ' 
        ' cmbVariable
        ' 
        cmbVariable.DrawMode = DrawMode.OwnerDrawFixed
        cmbVariable.FormattingEnabled = True
        cmbVariable.Location = New Point(130, 47)
        cmbVariable.Margin = New Padding(7, 6, 7, 6)
        cmbVariable.Name = "cmbVariable"
        cmbVariable.Size = New Size(383, 40)
        cmbVariable.TabIndex = 1
        ' 
        ' DarkLabel49
        ' 
        DarkLabel49.AutoSize = True
        DarkLabel49.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel49.Location = New Point(13, 53)
        DarkLabel49.Margin = New Padding(7, 0, 7, 0)
        DarkLabel49.Name = "DarkLabel49"
        DarkLabel49.Size = New Size(103, 32)
        DarkLabel49.TabIndex = 0
        DarkLabel49.Text = "Variable:"
        ' 
        ' fraChangeSprite
        ' 
        fraChangeSprite.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraChangeSprite.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraChangeSprite.Controls.Add(btnChangeSpriteOk)
        fraChangeSprite.Controls.Add(btnChangeSpriteCancel)
        fraChangeSprite.Controls.Add(DarkLabel48)
        fraChangeSprite.Controls.Add(nudChangeSprite)
        fraChangeSprite.Controls.Add(picChangeSprite)
        fraChangeSprite.ForeColor = Color.Gainsboro
        fraChangeSprite.Location = New Point(869, 689)
        fraChangeSprite.Margin = New Padding(7, 6, 7, 6)
        fraChangeSprite.Name = "fraChangeSprite"
        fraChangeSprite.Padding = New Padding(7, 6, 7, 6)
        fraChangeSprite.Size = New Size(533, 288)
        fraChangeSprite.TabIndex = 30
        fraChangeSprite.TabStop = False
        fraChangeSprite.Text = "Change Sprite"
        fraChangeSprite.Visible = False
        ' 
        ' btnChangeSpriteOk
        ' 
        btnChangeSpriteOk.Location = New Point(182, 220)
        btnChangeSpriteOk.Margin = New Padding(7, 6, 7, 6)
        btnChangeSpriteOk.Name = "btnChangeSpriteOk"
        btnChangeSpriteOk.Padding = New Padding(11, 13, 11, 13)
        btnChangeSpriteOk.Size = New Size(163, 58)
        btnChangeSpriteOk.TabIndex = 30
        btnChangeSpriteOk.Text = "Ok"
        ' 
        ' btnChangeSpriteCancel
        ' 
        btnChangeSpriteCancel.Location = New Point(357, 220)
        btnChangeSpriteCancel.Margin = New Padding(7, 6, 7, 6)
        btnChangeSpriteCancel.Name = "btnChangeSpriteCancel"
        btnChangeSpriteCancel.Padding = New Padding(11, 13, 11, 13)
        btnChangeSpriteCancel.Size = New Size(163, 58)
        btnChangeSpriteCancel.TabIndex = 29
        btnChangeSpriteCancel.Text = "Cancel"
        ' 
        ' DarkLabel48
        ' 
        DarkLabel48.AutoSize = True
        DarkLabel48.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel48.Location = New Point(173, 164)
        DarkLabel48.Margin = New Padding(7, 0, 7, 0)
        DarkLabel48.Name = "DarkLabel48"
        DarkLabel48.Size = New Size(76, 32)
        DarkLabel48.TabIndex = 28
        DarkLabel48.Text = "Sprite"
        ' 
        ' nudChangeSprite
        ' 
        nudChangeSprite.Location = New Point(260, 156)
        nudChangeSprite.Margin = New Padding(7, 6, 7, 6)
        nudChangeSprite.Name = "nudChangeSprite"
        nudChangeSprite.Size = New Size(260, 39)
        nudChangeSprite.TabIndex = 27
        ' 
        ' picChangeSprite
        ' 
        picChangeSprite.BackColor = Color.Black
        picChangeSprite.BackgroundImageLayout = ImageLayout.Zoom
        picChangeSprite.Location = New Point(13, 47)
        picChangeSprite.Margin = New Padding(7, 6, 7, 6)
        picChangeSprite.Name = "picChangeSprite"
        picChangeSprite.Size = New Size(152, 228)
        picChangeSprite.TabIndex = 3
        picChangeSprite.TabStop = False
        ' 
        ' fraSetSelfSwitch
        ' 
        fraSetSelfSwitch.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraSetSelfSwitch.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraSetSelfSwitch.Controls.Add(btnSelfswitchOk)
        fraSetSelfSwitch.Controls.Add(btnSelfswitchCancel)
        fraSetSelfSwitch.Controls.Add(DarkLabel47)
        fraSetSelfSwitch.Controls.Add(cmbSetSelfSwitchTo)
        fraSetSelfSwitch.Controls.Add(DarkLabel46)
        fraSetSelfSwitch.Controls.Add(cmbSetSelfSwitch)
        fraSetSelfSwitch.ForeColor = Color.Gainsboro
        fraSetSelfSwitch.Location = New Point(869, 444)
        fraSetSelfSwitch.Margin = New Padding(7, 6, 7, 6)
        fraSetSelfSwitch.Name = "fraSetSelfSwitch"
        fraSetSelfSwitch.Padding = New Padding(7, 6, 7, 6)
        fraSetSelfSwitch.Size = New Size(533, 245)
        fraSetSelfSwitch.TabIndex = 29
        fraSetSelfSwitch.TabStop = False
        fraSetSelfSwitch.Text = "Self Switches"
        fraSetSelfSwitch.Visible = False
        ' 
        ' btnSelfswitchOk
        ' 
        btnSelfswitchOk.Location = New Point(182, 179)
        btnSelfswitchOk.Margin = New Padding(7, 6, 7, 6)
        btnSelfswitchOk.Name = "btnSelfswitchOk"
        btnSelfswitchOk.Padding = New Padding(11, 13, 11, 13)
        btnSelfswitchOk.Size = New Size(163, 58)
        btnSelfswitchOk.TabIndex = 27
        btnSelfswitchOk.Text = "Ok"
        ' 
        ' btnSelfswitchCancel
        ' 
        btnSelfswitchCancel.Location = New Point(357, 179)
        btnSelfswitchCancel.Margin = New Padding(7, 6, 7, 6)
        btnSelfswitchCancel.Name = "btnSelfswitchCancel"
        btnSelfswitchCancel.Padding = New Padding(11, 13, 11, 13)
        btnSelfswitchCancel.Size = New Size(163, 58)
        btnSelfswitchCancel.TabIndex = 26
        btnSelfswitchCancel.Text = "Cancel"
        ' 
        ' DarkLabel47
        ' 
        DarkLabel47.AutoSize = True
        DarkLabel47.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel47.Location = New Point(13, 122)
        DarkLabel47.Margin = New Padding(7, 0, 7, 0)
        DarkLabel47.Name = "DarkLabel47"
        DarkLabel47.Size = New Size(80, 32)
        DarkLabel47.TabIndex = 3
        DarkLabel47.Text = "Set To"
        ' 
        ' cmbSetSelfSwitchTo
        ' 
        cmbSetSelfSwitchTo.DrawMode = DrawMode.OwnerDrawFixed
        cmbSetSelfSwitchTo.FormattingEnabled = True
        cmbSetSelfSwitchTo.Items.AddRange(New Object() {"Off", "On"})
        cmbSetSelfSwitchTo.Location = New Point(156, 113)
        cmbSetSelfSwitchTo.Margin = New Padding(7, 6, 7, 6)
        cmbSetSelfSwitchTo.Name = "cmbSetSelfSwitchTo"
        cmbSetSelfSwitchTo.Size = New Size(359, 40)
        cmbSetSelfSwitchTo.TabIndex = 2
        ' 
        ' DarkLabel46
        ' 
        DarkLabel46.AutoSize = True
        DarkLabel46.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel46.Location = New Point(13, 53)
        DarkLabel46.Margin = New Padding(7, 0, 7, 0)
        DarkLabel46.Name = "DarkLabel46"
        DarkLabel46.Size = New Size(135, 32)
        DarkLabel46.TabIndex = 1
        DarkLabel46.Text = "Self Switch:"
        ' 
        ' cmbSetSelfSwitch
        ' 
        cmbSetSelfSwitch.DrawMode = DrawMode.OwnerDrawFixed
        cmbSetSelfSwitch.FormattingEnabled = True
        cmbSetSelfSwitch.Location = New Point(156, 47)
        cmbSetSelfSwitch.Margin = New Padding(7, 6, 7, 6)
        cmbSetSelfSwitch.Name = "cmbSetSelfSwitch"
        cmbSetSelfSwitch.Size = New Size(359, 40)
        cmbSetSelfSwitch.TabIndex = 0
        ' 
        ' fraMapTint
        ' 
        fraMapTint.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraMapTint.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraMapTint.Controls.Add(btnMapTintOk)
        fraMapTint.Controls.Add(btnMapTintCancel)
        fraMapTint.Controls.Add(DarkLabel42)
        fraMapTint.Controls.Add(nudMapTintData3)
        fraMapTint.Controls.Add(nudMapTintData2)
        fraMapTint.Controls.Add(DarkLabel43)
        fraMapTint.Controls.Add(DarkLabel44)
        fraMapTint.Controls.Add(nudMapTintData1)
        fraMapTint.Controls.Add(nudMapTintData0)
        fraMapTint.Controls.Add(DarkLabel45)
        fraMapTint.ForeColor = Color.Gainsboro
        fraMapTint.Location = New Point(869, 446)
        fraMapTint.Margin = New Padding(7, 6, 7, 6)
        fraMapTint.Name = "fraMapTint"
        fraMapTint.Padding = New Padding(7, 6, 7, 6)
        fraMapTint.Size = New Size(533, 356)
        fraMapTint.TabIndex = 28
        fraMapTint.TabStop = False
        fraMapTint.Text = "Map Tinting"
        fraMapTint.Visible = False
        ' 
        ' btnMapTintOk
        ' 
        btnMapTintOk.Location = New Point(182, 284)
        btnMapTintOk.Margin = New Padding(7, 6, 7, 6)
        btnMapTintOk.Name = "btnMapTintOk"
        btnMapTintOk.Padding = New Padding(11, 13, 11, 13)
        btnMapTintOk.Size = New Size(163, 58)
        btnMapTintOk.TabIndex = 45
        btnMapTintOk.Text = "Ok"
        ' 
        ' btnMapTintCancel
        ' 
        btnMapTintCancel.Location = New Point(357, 284)
        btnMapTintCancel.Margin = New Padding(7, 6, 7, 6)
        btnMapTintCancel.Name = "btnMapTintCancel"
        btnMapTintCancel.Padding = New Padding(11, 13, 11, 13)
        btnMapTintCancel.Size = New Size(163, 58)
        btnMapTintCancel.TabIndex = 44
        btnMapTintCancel.Text = "Cancel"
        ' 
        ' DarkLabel42
        ' 
        DarkLabel42.AutoSize = True
        DarkLabel42.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel42.Location = New Point(11, 228)
        DarkLabel42.Margin = New Padding(7, 0, 7, 0)
        DarkLabel42.Name = "DarkLabel42"
        DarkLabel42.Size = New Size(100, 32)
        DarkLabel42.TabIndex = 43
        DarkLabel42.Text = "Opacity:"
        ' 
        ' nudMapTintData3
        ' 
        nudMapTintData3.Location = New Point(206, 220)
        nudMapTintData3.Margin = New Padding(7, 6, 7, 6)
        nudMapTintData3.Name = "nudMapTintData3"
        nudMapTintData3.Size = New Size(312, 39)
        nudMapTintData3.TabIndex = 42
        ' 
        ' nudMapTintData2
        ' 
        nudMapTintData2.Location = New Point(206, 158)
        nudMapTintData2.Margin = New Padding(7, 6, 7, 6)
        nudMapTintData2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        nudMapTintData2.Name = "nudMapTintData2"
        nudMapTintData2.Size = New Size(312, 39)
        nudMapTintData2.TabIndex = 41
        ' 
        ' DarkLabel43
        ' 
        DarkLabel43.AutoSize = True
        DarkLabel43.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel43.Location = New Point(11, 162)
        DarkLabel43.Margin = New Padding(7, 0, 7, 0)
        DarkLabel43.Name = "DarkLabel43"
        DarkLabel43.Size = New Size(66, 32)
        DarkLabel43.TabIndex = 40
        DarkLabel43.Text = "Blue:"
        ' 
        ' DarkLabel44
        ' 
        DarkLabel44.AutoSize = True
        DarkLabel44.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel44.Location = New Point(9, 107)
        DarkLabel44.Margin = New Padding(7, 0, 7, 0)
        DarkLabel44.Name = "DarkLabel44"
        DarkLabel44.Size = New Size(83, 32)
        DarkLabel44.TabIndex = 39
        DarkLabel44.Text = "Green:"
        ' 
        ' nudMapTintData1
        ' 
        nudMapTintData1.Location = New Point(206, 96)
        nudMapTintData1.Margin = New Padding(7, 6, 7, 6)
        nudMapTintData1.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        nudMapTintData1.Name = "nudMapTintData1"
        nudMapTintData1.Size = New Size(312, 39)
        nudMapTintData1.TabIndex = 38
        ' 
        ' nudMapTintData0
        ' 
        nudMapTintData0.Location = New Point(206, 34)
        nudMapTintData0.Margin = New Padding(7, 6, 7, 6)
        nudMapTintData0.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        nudMapTintData0.Name = "nudMapTintData0"
        nudMapTintData0.Size = New Size(312, 39)
        nudMapTintData0.TabIndex = 37
        ' 
        ' DarkLabel45
        ' 
        DarkLabel45.AutoSize = True
        DarkLabel45.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel45.Location = New Point(11, 38)
        DarkLabel45.Margin = New Padding(7, 0, 7, 0)
        DarkLabel45.Name = "DarkLabel45"
        DarkLabel45.Size = New Size(59, 32)
        DarkLabel45.TabIndex = 36
        DarkLabel45.Text = "Red:"
        ' 
        ' fraShowChatBubble
        ' 
        fraShowChatBubble.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraShowChatBubble.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraShowChatBubble.Controls.Add(btnShowChatBubbleOk)
        fraShowChatBubble.Controls.Add(btnShowChatBubbleCancel)
        fraShowChatBubble.Controls.Add(DarkLabel41)
        fraShowChatBubble.Controls.Add(cmbChatBubbleTarget)
        fraShowChatBubble.Controls.Add(cmbChatBubbleTargetType)
        fraShowChatBubble.Controls.Add(DarkLabel40)
        fraShowChatBubble.Controls.Add(txtChatbubbleText)
        fraShowChatBubble.Controls.Add(DarkLabel39)
        fraShowChatBubble.ForeColor = Color.Gainsboro
        fraShowChatBubble.Location = New Point(869, 446)
        fraShowChatBubble.Margin = New Padding(7, 6, 7, 6)
        fraShowChatBubble.Name = "fraShowChatBubble"
        fraShowChatBubble.Padding = New Padding(7, 6, 7, 6)
        fraShowChatBubble.Size = New Size(533, 348)
        fraShowChatBubble.TabIndex = 27
        fraShowChatBubble.TabStop = False
        fraShowChatBubble.Text = "Show ChatBubble"
        fraShowChatBubble.Visible = False
        ' 
        ' btnShowChatBubbleOk
        ' 
        btnShowChatBubbleOk.Location = New Point(182, 275)
        btnShowChatBubbleOk.Margin = New Padding(7, 6, 7, 6)
        btnShowChatBubbleOk.Name = "btnShowChatBubbleOk"
        btnShowChatBubbleOk.Padding = New Padding(11, 13, 11, 13)
        btnShowChatBubbleOk.Size = New Size(163, 58)
        btnShowChatBubbleOk.TabIndex = 31
        btnShowChatBubbleOk.Text = "Ok"
        ' 
        ' btnShowChatBubbleCancel
        ' 
        btnShowChatBubbleCancel.Location = New Point(357, 275)
        btnShowChatBubbleCancel.Margin = New Padding(7, 6, 7, 6)
        btnShowChatBubbleCancel.Name = "btnShowChatBubbleCancel"
        btnShowChatBubbleCancel.Padding = New Padding(11, 13, 11, 13)
        btnShowChatBubbleCancel.Size = New Size(163, 58)
        btnShowChatBubbleCancel.TabIndex = 30
        btnShowChatBubbleCancel.Text = "Cancel"
        ' 
        ' DarkLabel41
        ' 
        DarkLabel41.AutoSize = True
        DarkLabel41.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel41.Location = New Point(13, 218)
        DarkLabel41.Margin = New Padding(7, 0, 7, 0)
        DarkLabel41.Name = "DarkLabel41"
        DarkLabel41.Size = New Size(77, 32)
        DarkLabel41.TabIndex = 29
        DarkLabel41.Text = "Index:"
        ' 
        ' cmbChatBubbleTarget
        ' 
        cmbChatBubbleTarget.DrawMode = DrawMode.OwnerDrawFixed
        cmbChatBubbleTarget.FormattingEnabled = True
        cmbChatBubbleTarget.Location = New Point(175, 209)
        cmbChatBubbleTarget.Margin = New Padding(7, 6, 7, 6)
        cmbChatBubbleTarget.Name = "cmbChatBubbleTarget"
        cmbChatBubbleTarget.Size = New Size(340, 40)
        cmbChatBubbleTarget.TabIndex = 28
        ' 
        ' cmbChatBubbleTargetType
        ' 
        cmbChatBubbleTargetType.DrawMode = DrawMode.OwnerDrawFixed
        cmbChatBubbleTargetType.FormattingEnabled = True
        cmbChatBubbleTargetType.Items.AddRange(New Object() {"Player", "Npc", "Event"})
        cmbChatBubbleTargetType.Location = New Point(175, 143)
        cmbChatBubbleTargetType.Margin = New Padding(7, 6, 7, 6)
        cmbChatBubbleTargetType.Name = "cmbChatBubbleTargetType"
        cmbChatBubbleTargetType.Size = New Size(340, 40)
        cmbChatBubbleTargetType.TabIndex = 27
        ' 
        ' DarkLabel40
        ' 
        DarkLabel40.AutoSize = True
        DarkLabel40.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel40.Location = New Point(13, 149)
        DarkLabel40.Margin = New Padding(7, 0, 7, 0)
        DarkLabel40.Name = "DarkLabel40"
        DarkLabel40.Size = New Size(142, 32)
        DarkLabel40.TabIndex = 2
        DarkLabel40.Text = "Target Type:"
        ' 
        ' txtChatbubbleText
        ' 
        txtChatbubbleText.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtChatbubbleText.BorderStyle = BorderStyle.FixedSingle
        txtChatbubbleText.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtChatbubbleText.Location = New Point(13, 79)
        txtChatbubbleText.Margin = New Padding(7, 6, 7, 6)
        txtChatbubbleText.Name = "txtChatbubbleText"
        txtChatbubbleText.Size = New Size(505, 39)
        txtChatbubbleText.TabIndex = 1
        ' 
        ' DarkLabel39
        ' 
        DarkLabel39.AutoSize = True
        DarkLabel39.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel39.Location = New Point(13, 38)
        DarkLabel39.Margin = New Padding(7, 0, 7, 0)
        DarkLabel39.Name = "DarkLabel39"
        DarkLabel39.Size = New Size(188, 32)
        DarkLabel39.TabIndex = 0
        DarkLabel39.Text = "ChatBubble Text"
        ' 
        ' fraPlaySound
        ' 
        fraPlaySound.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraPlaySound.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraPlaySound.Controls.Add(btnPlaySoundOk)
        fraPlaySound.Controls.Add(btnPlaySoundCancel)
        fraPlaySound.Controls.Add(cmbPlaySound)
        fraPlaySound.ForeColor = Color.Gainsboro
        fraPlaySound.Location = New Point(869, 442)
        fraPlaySound.Margin = New Padding(7, 6, 7, 6)
        fraPlaySound.Name = "fraPlaySound"
        fraPlaySound.Padding = New Padding(7, 6, 7, 6)
        fraPlaySound.Size = New Size(533, 188)
        fraPlaySound.TabIndex = 26
        fraPlaySound.TabStop = False
        fraPlaySound.Text = "Play Sound"
        fraPlaySound.Visible = False
        ' 
        ' btnPlaySoundOk
        ' 
        btnPlaySoundOk.Location = New Point(182, 113)
        btnPlaySoundOk.Margin = New Padding(7, 6, 7, 6)
        btnPlaySoundOk.Name = "btnPlaySoundOk"
        btnPlaySoundOk.Padding = New Padding(11, 13, 11, 13)
        btnPlaySoundOk.Size = New Size(163, 58)
        btnPlaySoundOk.TabIndex = 27
        btnPlaySoundOk.Text = "Ok"
        ' 
        ' btnPlaySoundCancel
        ' 
        btnPlaySoundCancel.Location = New Point(357, 113)
        btnPlaySoundCancel.Margin = New Padding(7, 6, 7, 6)
        btnPlaySoundCancel.Name = "btnPlaySoundCancel"
        btnPlaySoundCancel.Padding = New Padding(11, 13, 11, 13)
        btnPlaySoundCancel.Size = New Size(163, 58)
        btnPlaySoundCancel.TabIndex = 26
        btnPlaySoundCancel.Text = "Cancel"
        ' 
        ' cmbPlaySound
        ' 
        cmbPlaySound.DrawMode = DrawMode.OwnerDrawFixed
        cmbPlaySound.FormattingEnabled = True
        cmbPlaySound.Location = New Point(13, 47)
        cmbPlaySound.Margin = New Padding(7, 6, 7, 6)
        cmbPlaySound.Name = "cmbPlaySound"
        cmbPlaySound.Size = New Size(502, 40)
        cmbPlaySound.TabIndex = 0
        ' 
        ' fraChangePK
        ' 
        fraChangePK.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraChangePK.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraChangePK.Controls.Add(btnChangePkOk)
        fraChangePK.Controls.Add(btnChangePkCancel)
        fraChangePK.Controls.Add(cmbSetPK)
        fraChangePK.ForeColor = Color.Gainsboro
        fraChangePK.Location = New Point(869, 256)
        fraChangePK.Margin = New Padding(7, 6, 7, 6)
        fraChangePK.Name = "fraChangePK"
        fraChangePK.Padding = New Padding(7, 6, 7, 6)
        fraChangePK.Size = New Size(533, 186)
        fraChangePK.TabIndex = 25
        fraChangePK.TabStop = False
        fraChangePK.Text = "Set Player PK"
        fraChangePK.Visible = False
        ' 
        ' btnChangePkOk
        ' 
        btnChangePkOk.Location = New Point(173, 113)
        btnChangePkOk.Margin = New Padding(7, 6, 7, 6)
        btnChangePkOk.Name = "btnChangePkOk"
        btnChangePkOk.Padding = New Padding(11, 13, 11, 13)
        btnChangePkOk.Size = New Size(163, 58)
        btnChangePkOk.TabIndex = 27
        btnChangePkOk.Text = "Ok"
        ' 
        ' btnChangePkCancel
        ' 
        btnChangePkCancel.Location = New Point(349, 113)
        btnChangePkCancel.Margin = New Padding(7, 6, 7, 6)
        btnChangePkCancel.Name = "btnChangePkCancel"
        btnChangePkCancel.Padding = New Padding(11, 13, 11, 13)
        btnChangePkCancel.Size = New Size(163, 58)
        btnChangePkCancel.TabIndex = 26
        btnChangePkCancel.Text = "Cancel"
        ' 
        ' cmbSetPK
        ' 
        cmbSetPK.DrawMode = DrawMode.OwnerDrawFixed
        cmbSetPK.FormattingEnabled = True
        cmbSetPK.Items.AddRange(New Object() {"No", "Yes"})
        cmbSetPK.Location = New Point(22, 47)
        cmbSetPK.Margin = New Padding(7, 6, 7, 6)
        cmbSetPK.Name = "cmbSetPK"
        cmbSetPK.Size = New Size(485, 40)
        cmbSetPK.TabIndex = 18
        ' 
        ' fraCreateLabel
        ' 
        fraCreateLabel.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraCreateLabel.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraCreateLabel.Controls.Add(btnCreatelabelOk)
        fraCreateLabel.Controls.Add(btnCreatelabelCancel)
        fraCreateLabel.Controls.Add(txtLabelName)
        fraCreateLabel.Controls.Add(lblLabelName)
        fraCreateLabel.ForeColor = Color.Gainsboro
        fraCreateLabel.Location = New Point(869, 324)
        fraCreateLabel.Margin = New Padding(7, 6, 7, 6)
        fraCreateLabel.Name = "fraCreateLabel"
        fraCreateLabel.Padding = New Padding(7, 6, 7, 6)
        fraCreateLabel.Size = New Size(533, 181)
        fraCreateLabel.TabIndex = 24
        fraCreateLabel.TabStop = False
        fraCreateLabel.Text = "Create Label"
        fraCreateLabel.Visible = False
        ' 
        ' btnCreatelabelOk
        ' 
        btnCreatelabelOk.Location = New Point(182, 111)
        btnCreatelabelOk.Margin = New Padding(7, 6, 7, 6)
        btnCreatelabelOk.Name = "btnCreatelabelOk"
        btnCreatelabelOk.Padding = New Padding(11, 13, 11, 13)
        btnCreatelabelOk.Size = New Size(163, 58)
        btnCreatelabelOk.TabIndex = 27
        btnCreatelabelOk.Text = "Ok"
        ' 
        ' btnCreatelabelCancel
        ' 
        btnCreatelabelCancel.Location = New Point(357, 111)
        btnCreatelabelCancel.Margin = New Padding(7, 6, 7, 6)
        btnCreatelabelCancel.Name = "btnCreatelabelCancel"
        btnCreatelabelCancel.Padding = New Padding(11, 13, 11, 13)
        btnCreatelabelCancel.Size = New Size(163, 58)
        btnCreatelabelCancel.TabIndex = 26
        btnCreatelabelCancel.Text = "Cancel"
        ' 
        ' txtLabelName
        ' 
        txtLabelName.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtLabelName.BorderStyle = BorderStyle.FixedSingle
        txtLabelName.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtLabelName.Location = New Point(173, 47)
        txtLabelName.Margin = New Padding(7, 6, 7, 6)
        txtLabelName.Name = "txtLabelName"
        txtLabelName.Size = New Size(344, 39)
        txtLabelName.TabIndex = 1
        ' 
        ' lblLabelName
        ' 
        lblLabelName.AutoSize = True
        lblLabelName.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        lblLabelName.Location = New Point(15, 51)
        lblLabelName.Margin = New Padding(7, 0, 7, 0)
        lblLabelName.Name = "lblLabelName"
        lblLabelName.Size = New Size(146, 32)
        lblLabelName.TabIndex = 0
        lblLabelName.Text = "Label Name:"
        ' 
        ' fraChangeJob
        ' 
        fraChangeJob.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraChangeJob.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraChangeJob.Controls.Add(btnChangeJobOk)
        fraChangeJob.Controls.Add(btnChangeJobCancel)
        fraChangeJob.Controls.Add(cmbChangeJob)
        fraChangeJob.Controls.Add(DarkLabel38)
        fraChangeJob.ForeColor = Color.Gainsboro
        fraChangeJob.Location = New Point(869, 269)
        fraChangeJob.Margin = New Padding(7, 6, 7, 6)
        fraChangeJob.Name = "fraChangeJob"
        fraChangeJob.Padding = New Padding(7, 6, 7, 6)
        fraChangeJob.Size = New Size(533, 188)
        fraChangeJob.TabIndex = 23
        fraChangeJob.TabStop = False
        fraChangeJob.Text = "Change Player Job"
        fraChangeJob.Visible = False
        ' 
        ' btnChangeJobOk
        ' 
        btnChangeJobOk.Location = New Point(182, 113)
        btnChangeJobOk.Margin = New Padding(7, 6, 7, 6)
        btnChangeJobOk.Name = "btnChangeJobOk"
        btnChangeJobOk.Padding = New Padding(11, 13, 11, 13)
        btnChangeJobOk.Size = New Size(163, 58)
        btnChangeJobOk.TabIndex = 27
        btnChangeJobOk.Text = "Ok"
        ' 
        ' btnChangeJobCancel
        ' 
        btnChangeJobCancel.Location = New Point(357, 113)
        btnChangeJobCancel.Margin = New Padding(7, 6, 7, 6)
        btnChangeJobCancel.Name = "btnChangeJobCancel"
        btnChangeJobCancel.Padding = New Padding(11, 13, 11, 13)
        btnChangeJobCancel.Size = New Size(163, 58)
        btnChangeJobCancel.TabIndex = 26
        btnChangeJobCancel.Text = "Cancel"
        ' 
        ' cmbChangeJob
        ' 
        cmbChangeJob.DrawMode = DrawMode.OwnerDrawFixed
        cmbChangeJob.FormattingEnabled = True
        cmbChangeJob.Location = New Point(106, 47)
        cmbChangeJob.Margin = New Padding(7, 6, 7, 6)
        cmbChangeJob.Name = "cmbChangeJob"
        cmbChangeJob.Size = New Size(409, 40)
        cmbChangeJob.TabIndex = 1
        ' 
        ' DarkLabel38
        ' 
        DarkLabel38.AutoSize = True
        DarkLabel38.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel38.Location = New Point(17, 53)
        DarkLabel38.Margin = New Padding(7, 0, 7, 0)
        DarkLabel38.Name = "DarkLabel38"
        DarkLabel38.Size = New Size(56, 32)
        DarkLabel38.TabIndex = 0
        DarkLabel38.Text = "Job:"
        ' 
        ' fraChangeSkills
        ' 
        fraChangeSkills.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraChangeSkills.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraChangeSkills.Controls.Add(btnChangeSkillsOk)
        fraChangeSkills.Controls.Add(btnChangeSkillsCancel)
        fraChangeSkills.Controls.Add(optChangeSkillsRemove)
        fraChangeSkills.Controls.Add(optChangeSkillsAdd)
        fraChangeSkills.Controls.Add(cmbChangeSkills)
        fraChangeSkills.Controls.Add(DarkLabel37)
        fraChangeSkills.ForeColor = Color.Gainsboro
        fraChangeSkills.Location = New Point(869, 267)
        fraChangeSkills.Margin = New Padding(7, 6, 7, 6)
        fraChangeSkills.Name = "fraChangeSkills"
        fraChangeSkills.Padding = New Padding(7, 6, 7, 6)
        fraChangeSkills.Size = New Size(533, 241)
        fraChangeSkills.TabIndex = 22
        fraChangeSkills.TabStop = False
        fraChangeSkills.Text = "Change Player Skills"
        fraChangeSkills.Visible = False
        ' 
        ' btnChangeSkillsOk
        ' 
        btnChangeSkillsOk.Location = New Point(182, 164)
        btnChangeSkillsOk.Margin = New Padding(7, 6, 7, 6)
        btnChangeSkillsOk.Name = "btnChangeSkillsOk"
        btnChangeSkillsOk.Padding = New Padding(11, 13, 11, 13)
        btnChangeSkillsOk.Size = New Size(163, 58)
        btnChangeSkillsOk.TabIndex = 27
        btnChangeSkillsOk.Text = "Ok"
        ' 
        ' btnChangeSkillsCancel
        ' 
        btnChangeSkillsCancel.Location = New Point(357, 164)
        btnChangeSkillsCancel.Margin = New Padding(7, 6, 7, 6)
        btnChangeSkillsCancel.Name = "btnChangeSkillsCancel"
        btnChangeSkillsCancel.Padding = New Padding(11, 13, 11, 13)
        btnChangeSkillsCancel.Size = New Size(163, 58)
        btnChangeSkillsCancel.TabIndex = 26
        btnChangeSkillsCancel.Text = "Cancel"
        ' 
        ' optChangeSkillsRemove
        ' 
        optChangeSkillsRemove.AutoSize = True
        optChangeSkillsRemove.Location = New Point(319, 109)
        optChangeSkillsRemove.Margin = New Padding(7, 6, 7, 6)
        optChangeSkillsRemove.Name = "optChangeSkillsRemove"
        optChangeSkillsRemove.Size = New Size(114, 36)
        optChangeSkillsRemove.TabIndex = 3
        optChangeSkillsRemove.TabStop = True
        optChangeSkillsRemove.Text = "Forget"
        ' 
        ' optChangeSkillsAdd
        ' 
        optChangeSkillsAdd.AutoSize = True
        optChangeSkillsAdd.Location = New Point(141, 109)
        optChangeSkillsAdd.Margin = New Padding(7, 6, 7, 6)
        optChangeSkillsAdd.Name = "optChangeSkillsAdd"
        optChangeSkillsAdd.Size = New Size(106, 36)
        optChangeSkillsAdd.TabIndex = 2
        optChangeSkillsAdd.TabStop = True
        optChangeSkillsAdd.Text = "Teach"
        ' 
        ' cmbChangeSkills
        ' 
        cmbChangeSkills.DrawMode = DrawMode.OwnerDrawFixed
        cmbChangeSkills.FormattingEnabled = True
        cmbChangeSkills.Location = New Point(89, 43)
        cmbChangeSkills.Margin = New Padding(7, 6, 7, 6)
        cmbChangeSkills.Name = "cmbChangeSkills"
        cmbChangeSkills.Size = New Size(424, 40)
        cmbChangeSkills.TabIndex = 1
        ' 
        ' DarkLabel37
        ' 
        DarkLabel37.AutoSize = True
        DarkLabel37.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel37.Location = New Point(13, 49)
        DarkLabel37.Margin = New Padding(7, 0, 7, 0)
        DarkLabel37.Name = "DarkLabel37"
        DarkLabel37.Size = New Size(62, 32)
        DarkLabel37.TabIndex = 0
        DarkLabel37.Text = "Skill:"
        ' 
        ' fraPlayerWarp
        ' 
        fraPlayerWarp.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraPlayerWarp.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraPlayerWarp.Controls.Add(btnPlayerWarpOk)
        fraPlayerWarp.Controls.Add(btnPlayerWarpCancel)
        fraPlayerWarp.Controls.Add(DarkLabel31)
        fraPlayerWarp.Controls.Add(cmbWarpPlayerDir)
        fraPlayerWarp.Controls.Add(nudWPY)
        fraPlayerWarp.Controls.Add(DarkLabel32)
        fraPlayerWarp.Controls.Add(nudWPX)
        fraPlayerWarp.Controls.Add(DarkLabel33)
        fraPlayerWarp.Controls.Add(nudWPMap)
        fraPlayerWarp.Controls.Add(DarkLabel34)
        fraPlayerWarp.ForeColor = Color.Gainsboro
        fraPlayerWarp.Location = New Point(869, 15)
        fraPlayerWarp.Margin = New Padding(7, 6, 7, 6)
        fraPlayerWarp.Name = "fraPlayerWarp"
        fraPlayerWarp.Padding = New Padding(7, 6, 7, 6)
        fraPlayerWarp.Size = New Size(533, 239)
        fraPlayerWarp.TabIndex = 19
        fraPlayerWarp.TabStop = False
        fraPlayerWarp.Text = "Warp Player"
        fraPlayerWarp.Visible = False
        ' 
        ' btnPlayerWarpOk
        ' 
        btnPlayerWarpOk.Location = New Point(180, 166)
        btnPlayerWarpOk.Margin = New Padding(7, 6, 7, 6)
        btnPlayerWarpOk.Name = "btnPlayerWarpOk"
        btnPlayerWarpOk.Padding = New Padding(11, 13, 11, 13)
        btnPlayerWarpOk.Size = New Size(163, 58)
        btnPlayerWarpOk.TabIndex = 46
        btnPlayerWarpOk.Text = "Ok"
        ' 
        ' btnPlayerWarpCancel
        ' 
        btnPlayerWarpCancel.Location = New Point(355, 166)
        btnPlayerWarpCancel.Margin = New Padding(7, 6, 7, 6)
        btnPlayerWarpCancel.Name = "btnPlayerWarpCancel"
        btnPlayerWarpCancel.Padding = New Padding(11, 13, 11, 13)
        btnPlayerWarpCancel.Size = New Size(163, 58)
        btnPlayerWarpCancel.TabIndex = 45
        btnPlayerWarpCancel.Text = "Cancel"
        ' 
        ' DarkLabel31
        ' 
        DarkLabel31.AutoSize = True
        DarkLabel31.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel31.Location = New Point(17, 109)
        DarkLabel31.Margin = New Padding(7, 0, 7, 0)
        DarkLabel31.Name = "DarkLabel31"
        DarkLabel31.Size = New Size(116, 32)
        DarkLabel31.TabIndex = 44
        DarkLabel31.Text = "Direction:"
        ' 
        ' cmbWarpPlayerDir
        ' 
        cmbWarpPlayerDir.DrawMode = DrawMode.OwnerDrawFixed
        cmbWarpPlayerDir.FormattingEnabled = True
        cmbWarpPlayerDir.Items.AddRange(New Object() {"Retain Direction", "Up", "Down", "Left", "Right"})
        cmbWarpPlayerDir.Location = New Point(208, 100)
        cmbWarpPlayerDir.Margin = New Padding(7, 6, 7, 6)
        cmbWarpPlayerDir.Name = "cmbWarpPlayerDir"
        cmbWarpPlayerDir.Size = New Size(305, 40)
        cmbWarpPlayerDir.TabIndex = 43
        ' 
        ' nudWPY
        ' 
        nudWPY.Location = New Point(433, 36)
        nudWPY.Margin = New Padding(7, 6, 7, 6)
        nudWPY.Name = "nudWPY"
        nudWPY.Size = New Size(85, 39)
        nudWPY.TabIndex = 42
        ' 
        ' DarkLabel32
        ' 
        DarkLabel32.AutoSize = True
        DarkLabel32.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel32.Location = New Point(383, 43)
        DarkLabel32.Margin = New Padding(7, 0, 7, 0)
        DarkLabel32.Name = "DarkLabel32"
        DarkLabel32.Size = New Size(32, 32)
        DarkLabel32.TabIndex = 41
        DarkLabel32.Text = "Y:"
        ' 
        ' nudWPX
        ' 
        nudWPX.Location = New Point(282, 36)
        nudWPX.Margin = New Padding(7, 6, 7, 6)
        nudWPX.Name = "nudWPX"
        nudWPX.Size = New Size(85, 39)
        nudWPX.TabIndex = 40
        ' 
        ' DarkLabel33
        ' 
        DarkLabel33.AutoSize = True
        DarkLabel33.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel33.Location = New Point(232, 43)
        DarkLabel33.Margin = New Padding(7, 0, 7, 0)
        DarkLabel33.Name = "DarkLabel33"
        DarkLabel33.Size = New Size(33, 32)
        DarkLabel33.TabIndex = 39
        DarkLabel33.Text = "X:"
        ' 
        ' nudWPMap
        ' 
        nudWPMap.Location = New Point(93, 36)
        nudWPMap.Margin = New Padding(7, 6, 7, 6)
        nudWPMap.Name = "nudWPMap"
        nudWPMap.Size = New Size(126, 39)
        nudWPMap.TabIndex = 38
        ' 
        ' DarkLabel34
        ' 
        DarkLabel34.AutoSize = True
        DarkLabel34.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel34.Location = New Point(13, 43)
        DarkLabel34.Margin = New Padding(7, 0, 7, 0)
        DarkLabel34.Name = "DarkLabel34"
        DarkLabel34.Size = New Size(67, 32)
        DarkLabel34.TabIndex = 37
        DarkLabel34.Text = "Map:"
        ' 
        ' fraSetFog
        ' 
        fraSetFog.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraSetFog.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraSetFog.Controls.Add(btnSetFogOk)
        fraSetFog.Controls.Add(btnSetFogCancel)
        fraSetFog.Controls.Add(DarkLabel30)
        fraSetFog.Controls.Add(DarkLabel29)
        fraSetFog.Controls.Add(DarkLabel28)
        fraSetFog.Controls.Add(nudFogData2)
        fraSetFog.Controls.Add(nudFogData1)
        fraSetFog.Controls.Add(nudFogData0)
        fraSetFog.ForeColor = Color.Gainsboro
        fraSetFog.Location = New Point(869, 17)
        fraSetFog.Margin = New Padding(7, 6, 7, 6)
        fraSetFog.Name = "fraSetFog"
        fraSetFog.Padding = New Padding(7, 6, 7, 6)
        fraSetFog.Size = New Size(533, 237)
        fraSetFog.TabIndex = 18
        fraSetFog.TabStop = False
        fraSetFog.Text = "Set Fog"
        fraSetFog.Visible = False
        ' 
        ' btnSetFogOk
        ' 
        btnSetFogOk.Location = New Point(182, 164)
        btnSetFogOk.Margin = New Padding(7, 6, 7, 6)
        btnSetFogOk.Name = "btnSetFogOk"
        btnSetFogOk.Padding = New Padding(11, 13, 11, 13)
        btnSetFogOk.Size = New Size(163, 58)
        btnSetFogOk.TabIndex = 41
        btnSetFogOk.Text = "Ok"
        ' 
        ' btnSetFogCancel
        ' 
        btnSetFogCancel.Location = New Point(357, 164)
        btnSetFogCancel.Margin = New Padding(7, 6, 7, 6)
        btnSetFogCancel.Name = "btnSetFogCancel"
        btnSetFogCancel.Padding = New Padding(11, 13, 11, 13)
        btnSetFogCancel.Size = New Size(163, 58)
        btnSetFogCancel.TabIndex = 40
        btnSetFogCancel.Text = "Cancel"
        ' 
        ' DarkLabel30
        ' 
        DarkLabel30.AutoSize = True
        DarkLabel30.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel30.Location = New Point(269, 102)
        DarkLabel30.Margin = New Padding(7, 0, 7, 0)
        DarkLabel30.Name = "DarkLabel30"
        DarkLabel30.Size = New Size(147, 32)
        DarkLabel30.TabIndex = 39
        DarkLabel30.Text = "Fog Opacity:"
        ' 
        ' DarkLabel29
        ' 
        DarkLabel29.AutoSize = True
        DarkLabel29.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel29.Location = New Point(15, 102)
        DarkLabel29.Margin = New Padding(7, 0, 7, 0)
        DarkLabel29.Name = "DarkLabel29"
        DarkLabel29.Size = New Size(133, 32)
        DarkLabel29.TabIndex = 38
        DarkLabel29.Text = "Fog Speed:"
        ' 
        ' DarkLabel28
        ' 
        DarkLabel28.AutoSize = True
        DarkLabel28.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel28.Location = New Point(15, 36)
        DarkLabel28.Margin = New Padding(7, 0, 7, 0)
        DarkLabel28.Name = "DarkLabel28"
        DarkLabel28.Size = New Size(59, 32)
        DarkLabel28.TabIndex = 37
        DarkLabel28.Text = "Fog:"
        ' 
        ' nudFogData2
        ' 
        nudFogData2.Location = New Point(414, 96)
        nudFogData2.Margin = New Padding(7, 6, 7, 6)
        nudFogData2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        nudFogData2.Name = "nudFogData2"
        nudFogData2.Size = New Size(106, 39)
        nudFogData2.TabIndex = 36
        ' 
        ' nudFogData1
        ' 
        nudFogData1.Location = New Point(156, 98)
        nudFogData1.Margin = New Padding(7, 6, 7, 6)
        nudFogData1.Name = "nudFogData1"
        nudFogData1.Size = New Size(104, 39)
        nudFogData1.TabIndex = 35
        ' 
        ' nudFogData0
        ' 
        nudFogData0.Location = New Point(210, 30)
        nudFogData0.Margin = New Padding(7, 6, 7, 6)
        nudFogData0.Name = "nudFogData0"
        nudFogData0.Size = New Size(310, 39)
        nudFogData0.TabIndex = 34
        ' 
        ' fraAddText
        ' 
        fraAddText.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraAddText.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraAddText.Controls.Add(btnAddTextOk)
        fraAddText.Controls.Add(btnAddTextCancel)
        fraAddText.Controls.Add(optAddText_Global)
        fraAddText.Controls.Add(optAddText_Map)
        fraAddText.Controls.Add(optAddText_Player)
        fraAddText.Controls.Add(DarkLabel25)
        fraAddText.Controls.Add(txtAddText_Text)
        fraAddText.Controls.Add(DarkLabel24)
        fraAddText.ForeColor = Color.Gainsboro
        fraAddText.Location = New Point(13, 894)
        fraAddText.Margin = New Padding(7, 6, 7, 6)
        fraAddText.Name = "fraAddText"
        fraAddText.Padding = New Padding(7, 6, 7, 6)
        fraAddText.Size = New Size(505, 461)
        fraAddText.TabIndex = 3
        fraAddText.TabStop = False
        fraAddText.Text = "Add Text"
        fraAddText.Visible = False
        ' 
        ' btnAddTextOk
        ' 
        btnAddTextOk.Location = New Point(119, 384)
        btnAddTextOk.Margin = New Padding(7, 6, 7, 6)
        btnAddTextOk.Name = "btnAddTextOk"
        btnAddTextOk.Padding = New Padding(11, 13, 11, 13)
        btnAddTextOk.Size = New Size(163, 58)
        btnAddTextOk.TabIndex = 9
        btnAddTextOk.Text = "Ok"
        ' 
        ' btnAddTextCancel
        ' 
        btnAddTextCancel.Location = New Point(295, 384)
        btnAddTextCancel.Margin = New Padding(7, 6, 7, 6)
        btnAddTextCancel.Name = "btnAddTextCancel"
        btnAddTextCancel.Padding = New Padding(11, 13, 11, 13)
        btnAddTextCancel.Size = New Size(163, 58)
        btnAddTextCancel.TabIndex = 8
        btnAddTextCancel.Text = "Cancel"
        ' 
        ' optAddText_Global
        ' 
        optAddText_Global.AutoSize = True
        optAddText_Global.Location = New Point(375, 326)
        optAddText_Global.Margin = New Padding(7, 6, 7, 6)
        optAddText_Global.Name = "optAddText_Global"
        optAddText_Global.Size = New Size(113, 36)
        optAddText_Global.TabIndex = 5
        optAddText_Global.TabStop = True
        optAddText_Global.Text = "Global"
        ' 
        ' optAddText_Map
        ' 
        optAddText_Map.AutoSize = True
        optAddText_Map.Location = New Point(262, 326)
        optAddText_Map.Margin = New Padding(7, 6, 7, 6)
        optAddText_Map.Name = "optAddText_Map"
        optAddText_Map.Size = New Size(93, 36)
        optAddText_Map.TabIndex = 4
        optAddText_Map.TabStop = True
        optAddText_Map.Text = "Map"
        ' 
        ' optAddText_Player
        ' 
        optAddText_Player.AutoSize = True
        optAddText_Player.Location = New Point(132, 326)
        optAddText_Player.Margin = New Padding(7, 6, 7, 6)
        optAddText_Player.Name = "optAddText_Player"
        optAddText_Player.Size = New Size(109, 36)
        optAddText_Player.TabIndex = 3
        optAddText_Player.TabStop = True
        optAddText_Player.Text = "Player"
        ' 
        ' DarkLabel25
        ' 
        DarkLabel25.AutoSize = True
        DarkLabel25.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel25.Location = New Point(13, 333)
        DarkLabel25.Margin = New Padding(7, 0, 7, 0)
        DarkLabel25.Name = "DarkLabel25"
        DarkLabel25.Size = New Size(107, 32)
        DarkLabel25.TabIndex = 2
        DarkLabel25.Text = "Channel:"
        ' 
        ' txtAddText_Text
        ' 
        txtAddText_Text.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtAddText_Text.BorderStyle = BorderStyle.FixedSingle
        txtAddText_Text.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtAddText_Text.Location = New Point(13, 77)
        txtAddText_Text.Margin = New Padding(7, 6, 7, 6)
        txtAddText_Text.Multiline = True
        txtAddText_Text.Name = "txtAddText_Text"
        txtAddText_Text.Size = New Size(479, 232)
        txtAddText_Text.TabIndex = 1
        ' 
        ' DarkLabel24
        ' 
        DarkLabel24.AutoSize = True
        DarkLabel24.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel24.Location = New Point(13, 36)
        DarkLabel24.Margin = New Padding(7, 0, 7, 0)
        DarkLabel24.Name = "DarkLabel24"
        DarkLabel24.Size = New Size(57, 32)
        DarkLabel24.TabIndex = 0
        DarkLabel24.Text = "Text"
        ' 
        ' fraPlayerSwitch
        ' 
        fraPlayerSwitch.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraPlayerSwitch.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraPlayerSwitch.Controls.Add(btnSetPlayerSwitchOk)
        fraPlayerSwitch.Controls.Add(btnSetPlayerswitchCancel)
        fraPlayerSwitch.Controls.Add(cmbPlayerSwitchSet)
        fraPlayerSwitch.Controls.Add(DarkLabel23)
        fraPlayerSwitch.Controls.Add(cmbSwitch)
        fraPlayerSwitch.Controls.Add(DarkLabel22)
        fraPlayerSwitch.ForeColor = Color.Gainsboro
        fraPlayerSwitch.Location = New Point(461, 960)
        fraPlayerSwitch.Margin = New Padding(7, 6, 7, 6)
        fraPlayerSwitch.Name = "fraPlayerSwitch"
        fraPlayerSwitch.Padding = New Padding(7, 6, 7, 6)
        fraPlayerSwitch.Size = New Size(394, 245)
        fraPlayerSwitch.TabIndex = 2
        fraPlayerSwitch.TabStop = False
        fraPlayerSwitch.Text = "Change Items"
        fraPlayerSwitch.Visible = False
        ' 
        ' btnSetPlayerSwitchOk
        ' 
        btnSetPlayerSwitchOk.Location = New Point(43, 177)
        btnSetPlayerSwitchOk.Margin = New Padding(7, 6, 7, 6)
        btnSetPlayerSwitchOk.Name = "btnSetPlayerSwitchOk"
        btnSetPlayerSwitchOk.Padding = New Padding(11, 13, 11, 13)
        btnSetPlayerSwitchOk.Size = New Size(163, 58)
        btnSetPlayerSwitchOk.TabIndex = 9
        btnSetPlayerSwitchOk.Text = "Ok"
        ' 
        ' btnSetPlayerswitchCancel
        ' 
        btnSetPlayerswitchCancel.Location = New Point(219, 177)
        btnSetPlayerswitchCancel.Margin = New Padding(7, 6, 7, 6)
        btnSetPlayerswitchCancel.Name = "btnSetPlayerswitchCancel"
        btnSetPlayerswitchCancel.Padding = New Padding(11, 13, 11, 13)
        btnSetPlayerswitchCancel.Size = New Size(163, 58)
        btnSetPlayerswitchCancel.TabIndex = 8
        btnSetPlayerswitchCancel.Text = "Cancel"
        ' 
        ' cmbPlayerSwitchSet
        ' 
        cmbPlayerSwitchSet.DrawMode = DrawMode.OwnerDrawFixed
        cmbPlayerSwitchSet.FormattingEnabled = True
        cmbPlayerSwitchSet.Items.AddRange(New Object() {"False", "True"})
        cmbPlayerSwitchSet.Location = New Point(110, 100)
        cmbPlayerSwitchSet.Margin = New Padding(7, 6, 7, 6)
        cmbPlayerSwitchSet.Name = "cmbPlayerSwitchSet"
        cmbPlayerSwitchSet.Size = New Size(266, 40)
        cmbPlayerSwitchSet.TabIndex = 3
        ' 
        ' DarkLabel23
        ' 
        DarkLabel23.AutoSize = True
        DarkLabel23.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel23.Location = New Point(13, 113)
        DarkLabel23.Margin = New Padding(7, 0, 7, 0)
        DarkLabel23.Name = "DarkLabel23"
        DarkLabel23.Size = New Size(77, 32)
        DarkLabel23.TabIndex = 2
        DarkLabel23.Text = "Set to"
        ' 
        ' cmbSwitch
        ' 
        cmbSwitch.DrawMode = DrawMode.OwnerDrawFixed
        cmbSwitch.FormattingEnabled = True
        cmbSwitch.Location = New Point(110, 32)
        cmbSwitch.Margin = New Padding(7, 6, 7, 6)
        cmbSwitch.Name = "cmbSwitch"
        cmbSwitch.Size = New Size(266, 40)
        cmbSwitch.TabIndex = 1
        ' 
        ' DarkLabel22
        ' 
        DarkLabel22.AutoSize = True
        DarkLabel22.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel22.Location = New Point(13, 38)
        DarkLabel22.Margin = New Padding(7, 0, 7, 0)
        DarkLabel22.Name = "DarkLabel22"
        DarkLabel22.Size = New Size(83, 32)
        DarkLabel22.TabIndex = 0
        DarkLabel22.Text = "Switch"
        ' 
        ' fraChangeItems
        ' 
        fraChangeItems.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraChangeItems.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraChangeItems.Controls.Add(btnChangeItemsOk)
        fraChangeItems.Controls.Add(btnChangeItemsCancel)
        fraChangeItems.Controls.Add(nudChangeItemsAmount)
        fraChangeItems.Controls.Add(optChangeItemRemove)
        fraChangeItems.Controls.Add(optChangeItemAdd)
        fraChangeItems.Controls.Add(optChangeItemSet)
        fraChangeItems.Controls.Add(cmbChangeItemIndex)
        fraChangeItems.Controls.Add(DarkLabel21)
        fraChangeItems.ForeColor = Color.Gainsboro
        fraChangeItems.Location = New Point(13, 960)
        fraChangeItems.Margin = New Padding(7, 6, 7, 6)
        fraChangeItems.Name = "fraChangeItems"
        fraChangeItems.Padding = New Padding(7, 6, 7, 6)
        fraChangeItems.Size = New Size(405, 294)
        fraChangeItems.TabIndex = 1
        fraChangeItems.TabStop = False
        fraChangeItems.Text = "Change Items"
        fraChangeItems.Visible = False
        ' 
        ' btnChangeItemsOk
        ' 
        btnChangeItemsOk.Location = New Point(54, 224)
        btnChangeItemsOk.Margin = New Padding(7, 6, 7, 6)
        btnChangeItemsOk.Name = "btnChangeItemsOk"
        btnChangeItemsOk.Padding = New Padding(11, 13, 11, 13)
        btnChangeItemsOk.Size = New Size(163, 58)
        btnChangeItemsOk.TabIndex = 7
        btnChangeItemsOk.Text = "Ok"
        ' 
        ' btnChangeItemsCancel
        ' 
        btnChangeItemsCancel.Location = New Point(230, 224)
        btnChangeItemsCancel.Margin = New Padding(7, 6, 7, 6)
        btnChangeItemsCancel.Name = "btnChangeItemsCancel"
        btnChangeItemsCancel.Padding = New Padding(11, 13, 11, 13)
        btnChangeItemsCancel.Size = New Size(163, 58)
        btnChangeItemsCancel.TabIndex = 6
        btnChangeItemsCancel.Text = "Cancel"
        ' 
        ' nudChangeItemsAmount
        ' 
        nudChangeItemsAmount.Location = New Point(19, 160)
        nudChangeItemsAmount.Margin = New Padding(7, 6, 7, 6)
        nudChangeItemsAmount.Name = "nudChangeItemsAmount"
        nudChangeItemsAmount.Size = New Size(373, 39)
        nudChangeItemsAmount.TabIndex = 5
        ' 
        ' optChangeItemRemove
        ' 
        optChangeItemRemove.AutoSize = True
        optChangeItemRemove.Location = New Point(262, 102)
        optChangeItemRemove.Margin = New Padding(7, 6, 7, 6)
        optChangeItemRemove.Name = "optChangeItemRemove"
        optChangeItemRemove.Size = New Size(92, 36)
        optChangeItemRemove.TabIndex = 4
        optChangeItemRemove.TabStop = True
        optChangeItemRemove.Text = "Take"
        ' 
        ' optChangeItemAdd
        ' 
        optChangeItemAdd.AutoSize = True
        optChangeItemAdd.Location = New Point(147, 102)
        optChangeItemAdd.Margin = New Padding(7, 6, 7, 6)
        optChangeItemAdd.Name = "optChangeItemAdd"
        optChangeItemAdd.Size = New Size(92, 36)
        optChangeItemAdd.TabIndex = 3
        optChangeItemAdd.TabStop = True
        optChangeItemAdd.Text = "Give"
        ' 
        ' optChangeItemSet
        ' 
        optChangeItemSet.AutoSize = True
        optChangeItemSet.Location = New Point(19, 102)
        optChangeItemSet.Margin = New Padding(7, 6, 7, 6)
        optChangeItemSet.Name = "optChangeItemSet"
        optChangeItemSet.Size = New Size(108, 36)
        optChangeItemSet.TabIndex = 2
        optChangeItemSet.TabStop = True
        optChangeItemSet.Text = "Set to"
        ' 
        ' cmbChangeItemIndex
        ' 
        cmbChangeItemIndex.DrawMode = DrawMode.OwnerDrawFixed
        cmbChangeItemIndex.FormattingEnabled = True
        cmbChangeItemIndex.Location = New Point(91, 32)
        cmbChangeItemIndex.Margin = New Padding(7, 6, 7, 6)
        cmbChangeItemIndex.Name = "cmbChangeItemIndex"
        cmbChangeItemIndex.Size = New Size(297, 40)
        cmbChangeItemIndex.TabIndex = 1
        ' 
        ' DarkLabel21
        ' 
        DarkLabel21.AutoSize = True
        DarkLabel21.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel21.Location = New Point(13, 38)
        DarkLabel21.Margin = New Padding(7, 0, 7, 0)
        DarkLabel21.Name = "DarkLabel21"
        DarkLabel21.Size = New Size(67, 32)
        DarkLabel21.TabIndex = 0
        DarkLabel21.Text = "Item:"
        ' 
        ' fraPlayBGM
        ' 
        fraPlayBGM.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraPlayBGM.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraPlayBGM.Controls.Add(btnPlayBgmOk)
        fraPlayBGM.Controls.Add(btnPlayBgmCancel)
        fraPlayBGM.Controls.Add(cmbPlayBGM)
        fraPlayBGM.ForeColor = Color.Gainsboro
        fraPlayBGM.Location = New Point(869, 2)
        fraPlayBGM.Margin = New Padding(7, 6, 7, 6)
        fraPlayBGM.Name = "fraPlayBGM"
        fraPlayBGM.Padding = New Padding(7, 6, 7, 6)
        fraPlayBGM.Size = New Size(533, 186)
        fraPlayBGM.TabIndex = 21
        fraPlayBGM.TabStop = False
        fraPlayBGM.Text = "Play BGM"
        fraPlayBGM.Visible = False
        ' 
        ' btnPlayBgmOk
        ' 
        btnPlayBgmOk.Location = New Point(100, 113)
        btnPlayBgmOk.Margin = New Padding(7, 6, 7, 6)
        btnPlayBgmOk.Name = "btnPlayBgmOk"
        btnPlayBgmOk.Padding = New Padding(11, 13, 11, 13)
        btnPlayBgmOk.Size = New Size(163, 58)
        btnPlayBgmOk.TabIndex = 27
        btnPlayBgmOk.Text = "Ok"
        ' 
        ' btnPlayBgmCancel
        ' 
        btnPlayBgmCancel.Location = New Point(275, 113)
        btnPlayBgmCancel.Margin = New Padding(7, 6, 7, 6)
        btnPlayBgmCancel.Name = "btnPlayBgmCancel"
        btnPlayBgmCancel.Padding = New Padding(11, 13, 11, 13)
        btnPlayBgmCancel.Size = New Size(163, 58)
        btnPlayBgmCancel.TabIndex = 26
        btnPlayBgmCancel.Text = "Cancel"
        ' 
        ' cmbPlayBGM
        ' 
        cmbPlayBGM.DrawMode = DrawMode.OwnerDrawFixed
        cmbPlayBGM.FormattingEnabled = True
        cmbPlayBGM.Location = New Point(13, 47)
        cmbPlayBGM.Margin = New Padding(7, 6, 7, 6)
        cmbPlayBGM.Name = "cmbPlayBGM"
        cmbPlayBGM.Size = New Size(500, 40)
        cmbPlayBGM.TabIndex = 0
        ' 
        ' fraPlayAnimation
        ' 
        fraPlayAnimation.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraPlayAnimation.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraPlayAnimation.Controls.Add(btnPlayAnimationOk)
        fraPlayAnimation.Controls.Add(btnPlayAnimationCancel)
        fraPlayAnimation.Controls.Add(lblPlayAnimY)
        fraPlayAnimation.Controls.Add(lblPlayAnimX)
        fraPlayAnimation.Controls.Add(cmbPlayAnimEvent)
        fraPlayAnimation.Controls.Add(DarkLabel62)
        fraPlayAnimation.Controls.Add(cmbAnimTargetType)
        fraPlayAnimation.Controls.Add(nudPlayAnimTileY)
        fraPlayAnimation.Controls.Add(nudPlayAnimTileX)
        fraPlayAnimation.Controls.Add(DarkLabel61)
        fraPlayAnimation.Controls.Add(cmbPlayAnim)
        fraPlayAnimation.ForeColor = Color.Gainsboro
        fraPlayAnimation.Location = New Point(869, 634)
        fraPlayAnimation.Margin = New Padding(7, 6, 7, 6)
        fraPlayAnimation.Name = "fraPlayAnimation"
        fraPlayAnimation.Padding = New Padding(7, 6, 7, 6)
        fraPlayAnimation.Size = New Size(537, 399)
        fraPlayAnimation.TabIndex = 36
        fraPlayAnimation.TabStop = False
        fraPlayAnimation.Text = "Play Animation"
        fraPlayAnimation.Visible = False
        ' 
        ' btnPlayAnimationOk
        ' 
        btnPlayAnimationOk.Location = New Point(186, 324)
        btnPlayAnimationOk.Margin = New Padding(7, 6, 7, 6)
        btnPlayAnimationOk.Name = "btnPlayAnimationOk"
        btnPlayAnimationOk.Padding = New Padding(11, 13, 11, 13)
        btnPlayAnimationOk.Size = New Size(163, 58)
        btnPlayAnimationOk.TabIndex = 36
        btnPlayAnimationOk.Text = "Ok"
        ' 
        ' btnPlayAnimationCancel
        ' 
        btnPlayAnimationCancel.Location = New Point(362, 324)
        btnPlayAnimationCancel.Margin = New Padding(7, 6, 7, 6)
        btnPlayAnimationCancel.Name = "btnPlayAnimationCancel"
        btnPlayAnimationCancel.Padding = New Padding(11, 13, 11, 13)
        btnPlayAnimationCancel.Size = New Size(163, 58)
        btnPlayAnimationCancel.TabIndex = 35
        btnPlayAnimationCancel.Text = "Cancel"
        ' 
        ' lblPlayAnimY
        ' 
        lblPlayAnimY.AutoSize = True
        lblPlayAnimY.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        lblPlayAnimY.Location = New Point(284, 260)
        lblPlayAnimY.Margin = New Padding(7, 0, 7, 0)
        lblPlayAnimY.Name = "lblPlayAnimY"
        lblPlayAnimY.Size = New Size(132, 32)
        lblPlayAnimY.TabIndex = 34
        lblPlayAnimY.Text = "Map Tile Y:"
        ' 
        ' lblPlayAnimX
        ' 
        lblPlayAnimX.AutoSize = True
        lblPlayAnimX.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        lblPlayAnimX.Location = New Point(13, 260)
        lblPlayAnimX.Margin = New Padding(7, 0, 7, 0)
        lblPlayAnimX.Name = "lblPlayAnimX"
        lblPlayAnimX.Size = New Size(133, 32)
        lblPlayAnimX.TabIndex = 33
        lblPlayAnimX.Text = "Map Tile X:"
        ' 
        ' cmbPlayAnimEvent
        ' 
        cmbPlayAnimEvent.DrawMode = DrawMode.OwnerDrawFixed
        cmbPlayAnimEvent.FormattingEnabled = True
        cmbPlayAnimEvent.Location = New Point(180, 179)
        cmbPlayAnimEvent.Margin = New Padding(7, 6, 7, 6)
        cmbPlayAnimEvent.Name = "cmbPlayAnimEvent"
        cmbPlayAnimEvent.Size = New Size(340, 40)
        cmbPlayAnimEvent.TabIndex = 32
        ' 
        ' DarkLabel62
        ' 
        DarkLabel62.AutoSize = True
        DarkLabel62.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel62.Location = New Point(9, 122)
        DarkLabel62.Margin = New Padding(7, 0, 7, 0)
        DarkLabel62.Name = "DarkLabel62"
        DarkLabel62.Size = New Size(137, 32)
        DarkLabel62.TabIndex = 31
        DarkLabel62.Text = "Target Type"
        ' 
        ' cmbAnimTargetType
        ' 
        cmbAnimTargetType.DrawMode = DrawMode.OwnerDrawFixed
        cmbAnimTargetType.FormattingEnabled = True
        cmbAnimTargetType.Items.AddRange(New Object() {"Player", "Event", "Tile"})
        cmbAnimTargetType.Location = New Point(180, 113)
        cmbAnimTargetType.Margin = New Padding(7, 6, 7, 6)
        cmbAnimTargetType.Name = "cmbAnimTargetType"
        cmbAnimTargetType.Size = New Size(340, 40)
        cmbAnimTargetType.TabIndex = 30
        ' 
        ' nudPlayAnimTileY
        ' 
        nudPlayAnimTileY.Location = New Point(429, 256)
        nudPlayAnimTileY.Margin = New Padding(7, 6, 7, 6)
        nudPlayAnimTileY.Name = "nudPlayAnimTileY"
        nudPlayAnimTileY.Size = New Size(95, 39)
        nudPlayAnimTileY.TabIndex = 29
        ' 
        ' nudPlayAnimTileX
        ' 
        nudPlayAnimTileX.Location = New Point(158, 256)
        nudPlayAnimTileX.Margin = New Padding(7, 6, 7, 6)
        nudPlayAnimTileX.Name = "nudPlayAnimTileX"
        nudPlayAnimTileX.Size = New Size(95, 39)
        nudPlayAnimTileX.TabIndex = 28
        ' 
        ' DarkLabel61
        ' 
        DarkLabel61.AutoSize = True
        DarkLabel61.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel61.Location = New Point(13, 53)
        DarkLabel61.Margin = New Padding(7, 0, 7, 0)
        DarkLabel61.Name = "DarkLabel61"
        DarkLabel61.Size = New Size(129, 32)
        DarkLabel61.TabIndex = 1
        DarkLabel61.Text = "Animation:"
        ' 
        ' cmbPlayAnim
        ' 
        cmbPlayAnim.DrawMode = DrawMode.OwnerDrawFixed
        cmbPlayAnim.FormattingEnabled = True
        cmbPlayAnim.Location = New Point(134, 47)
        cmbPlayAnim.Margin = New Padding(7, 6, 7, 6)
        cmbPlayAnim.Name = "cmbPlayAnim"
        cmbPlayAnim.Size = New Size(385, 40)
        cmbPlayAnim.TabIndex = 0
        ' 
        ' fraSetWait
        ' 
        fraSetWait.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraSetWait.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraSetWait.Controls.Add(btnSetWaitOk)
        fraSetWait.Controls.Add(btnSetWaitCancel)
        fraSetWait.Controls.Add(DarkLabel74)
        fraSetWait.Controls.Add(DarkLabel72)
        fraSetWait.Controls.Add(DarkLabel73)
        fraSetWait.Controls.Add(nudWaitAmount)
        fraSetWait.ForeColor = Color.Gainsboro
        fraSetWait.Location = New Point(869, 651)
        fraSetWait.Margin = New Padding(7, 6, 7, 6)
        fraSetWait.Name = "fraSetWait"
        fraSetWait.Padding = New Padding(7, 6, 7, 6)
        fraSetWait.Size = New Size(537, 220)
        fraSetWait.TabIndex = 41
        fraSetWait.TabStop = False
        fraSetWait.Text = "Wait..."
        fraSetWait.Visible = False
        ' 
        ' btnSetWaitOk
        ' 
        btnSetWaitOk.Location = New Point(108, 143)
        btnSetWaitOk.Margin = New Padding(7, 6, 7, 6)
        btnSetWaitOk.Name = "btnSetWaitOk"
        btnSetWaitOk.Padding = New Padding(11, 13, 11, 13)
        btnSetWaitOk.Size = New Size(163, 58)
        btnSetWaitOk.TabIndex = 37
        btnSetWaitOk.Text = "Ok"
        ' 
        ' btnSetWaitCancel
        ' 
        btnSetWaitCancel.Location = New Point(284, 143)
        btnSetWaitCancel.Margin = New Padding(7, 6, 7, 6)
        btnSetWaitCancel.Name = "btnSetWaitCancel"
        btnSetWaitCancel.Padding = New Padding(11, 13, 11, 13)
        btnSetWaitCancel.Size = New Size(163, 58)
        btnSetWaitCancel.TabIndex = 36
        btnSetWaitCancel.Text = "Cancel"
        ' 
        ' DarkLabel74
        ' 
        DarkLabel74.AutoSize = True
        DarkLabel74.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel74.Location = New Point(152, 102)
        DarkLabel74.Margin = New Padding(7, 0, 7, 0)
        DarkLabel74.Name = "DarkLabel74"
        DarkLabel74.Size = New Size(249, 32)
        DarkLabel74.TabIndex = 35
        DarkLabel74.Text = "Hint: 1000 Ms = 1 Sec"
        ' 
        ' DarkLabel72
        ' 
        DarkLabel72.AutoSize = True
        DarkLabel72.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel72.Location = New Point(472, 58)
        DarkLabel72.Margin = New Padding(7, 0, 7, 0)
        DarkLabel72.Name = "DarkLabel72"
        DarkLabel72.Size = New Size(46, 32)
        DarkLabel72.TabIndex = 34
        DarkLabel72.Text = "Ms"
        ' 
        ' DarkLabel73
        ' 
        DarkLabel73.AutoSize = True
        DarkLabel73.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel73.Location = New Point(33, 58)
        DarkLabel73.Margin = New Padding(7, 0, 7, 0)
        DarkLabel73.Name = "DarkLabel73"
        DarkLabel73.Size = New Size(61, 32)
        DarkLabel73.TabIndex = 33
        DarkLabel73.Text = "Wait"
        ' 
        ' nudWaitAmount
        ' 
        nudWaitAmount.Location = New Point(108, 47)
        nudWaitAmount.Margin = New Padding(7, 6, 7, 6)
        nudWaitAmount.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        nudWaitAmount.Name = "nudWaitAmount"
        nudWaitAmount.Size = New Size(353, 39)
        nudWaitAmount.TabIndex = 32
        ' 
        ' fraShowPic
        ' 
        fraShowPic.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraShowPic.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraShowPic.Controls.Add(btnShowPicOk)
        fraShowPic.Controls.Add(btnShowPicCancel)
        fraShowPic.Controls.Add(DarkLabel71)
        fraShowPic.Controls.Add(DarkLabel70)
        fraShowPic.Controls.Add(DarkLabel67)
        fraShowPic.Controls.Add(DarkLabel68)
        fraShowPic.Controls.Add(nudPicOffsetY)
        fraShowPic.Controls.Add(nudPicOffsetX)
        fraShowPic.Controls.Add(DarkLabel69)
        fraShowPic.Controls.Add(cmbPicLoc)
        fraShowPic.Controls.Add(nudShowPicture)
        fraShowPic.Controls.Add(picShowPic)
        fraShowPic.ForeColor = Color.Gainsboro
        fraShowPic.Location = New Point(2, 2)
        fraShowPic.Margin = New Padding(7, 6, 7, 6)
        fraShowPic.Name = "fraShowPic"
        fraShowPic.Padding = New Padding(7, 6, 7, 6)
        fraShowPic.Size = New Size(1439, 1463)
        fraShowPic.TabIndex = 40
        fraShowPic.TabStop = False
        fraShowPic.Text = "Show Picture"
        fraShowPic.Visible = False
        ' 
        ' btnShowPicOk
        ' 
        btnShowPicOk.Location = New Point(1083, 1389)
        btnShowPicOk.Margin = New Padding(7, 6, 7, 6)
        btnShowPicOk.Name = "btnShowPicOk"
        btnShowPicOk.Padding = New Padding(11, 13, 11, 13)
        btnShowPicOk.Size = New Size(163, 58)
        btnShowPicOk.TabIndex = 55
        btnShowPicOk.Text = "Ok"
        ' 
        ' btnShowPicCancel
        ' 
        btnShowPicCancel.Location = New Point(1261, 1389)
        btnShowPicCancel.Margin = New Padding(7, 6, 7, 6)
        btnShowPicCancel.Name = "btnShowPicCancel"
        btnShowPicCancel.Padding = New Padding(11, 13, 11, 13)
        btnShowPicCancel.Size = New Size(163, 58)
        btnShowPicCancel.TabIndex = 54
        btnShowPicCancel.Text = "Cancel"
        ' 
        ' DarkLabel71
        ' 
        DarkLabel71.AutoSize = True
        DarkLabel71.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel71.Location = New Point(533, 58)
        DarkLabel71.Margin = New Padding(7, 0, 7, 0)
        DarkLabel71.Name = "DarkLabel71"
        DarkLabel71.Size = New Size(239, 32)
        DarkLabel71.TabIndex = 53
        DarkLabel71.Text = "Offset from Location:"
        ' 
        ' DarkLabel70
        ' 
        DarkLabel70.AutoSize = True
        DarkLabel70.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel70.Location = New Point(241, 139)
        DarkLabel70.Margin = New Padding(7, 0, 7, 0)
        DarkLabel70.Name = "DarkLabel70"
        DarkLabel70.Size = New Size(104, 32)
        DarkLabel70.TabIndex = 52
        DarkLabel70.Text = "Location"
        ' 
        ' DarkLabel67
        ' 
        DarkLabel67.AutoSize = True
        DarkLabel67.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel67.Location = New Point(808, 115)
        DarkLabel67.Margin = New Padding(7, 0, 7, 0)
        DarkLabel67.Name = "DarkLabel67"
        DarkLabel67.Size = New Size(32, 32)
        DarkLabel67.TabIndex = 51
        DarkLabel67.Text = "Y:"
        ' 
        ' DarkLabel68
        ' 
        DarkLabel68.AutoSize = True
        DarkLabel68.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel68.Location = New Point(533, 119)
        DarkLabel68.Margin = New Padding(7, 0, 7, 0)
        DarkLabel68.Name = "DarkLabel68"
        DarkLabel68.Size = New Size(33, 32)
        DarkLabel68.TabIndex = 50
        DarkLabel68.Text = "X:"
        ' 
        ' nudPicOffsetY
        ' 
        nudPicOffsetY.Location = New Point(904, 111)
        nudPicOffsetY.Margin = New Padding(7, 6, 7, 6)
        nudPicOffsetY.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudPicOffsetY.Name = "nudPicOffsetY"
        nudPicOffsetY.Size = New Size(123, 39)
        nudPicOffsetY.TabIndex = 49
        ' 
        ' nudPicOffsetX
        ' 
        nudPicOffsetX.Location = New Point(624, 111)
        nudPicOffsetX.Margin = New Padding(7, 6, 7, 6)
        nudPicOffsetX.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudPicOffsetX.Name = "nudPicOffsetX"
        nudPicOffsetX.Size = New Size(123, 39)
        nudPicOffsetX.TabIndex = 48
        ' 
        ' DarkLabel69
        ' 
        DarkLabel69.AutoSize = True
        DarkLabel69.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel69.Location = New Point(241, 55)
        DarkLabel69.Margin = New Padding(7, 0, 7, 0)
        DarkLabel69.Name = "DarkLabel69"
        DarkLabel69.Size = New Size(92, 32)
        DarkLabel69.TabIndex = 47
        DarkLabel69.Text = "Picture:"
        ' 
        ' cmbPicLoc
        ' 
        cmbPicLoc.DrawMode = DrawMode.OwnerDrawFixed
        cmbPicLoc.FormattingEnabled = True
        cmbPicLoc.Items.AddRange(New Object() {"Top Left of Screen", "Center Screen", "Centered on Event", "Centered on Player"})
        cmbPicLoc.Location = New Point(247, 183)
        cmbPicLoc.Margin = New Padding(7, 6, 7, 6)
        cmbPicLoc.Name = "cmbPicLoc"
        cmbPicLoc.Size = New Size(264, 40)
        cmbPicLoc.TabIndex = 46
        ' 
        ' nudShowPicture
        ' 
        nudShowPicture.Location = New Point(344, 51)
        nudShowPicture.Margin = New Padding(7, 6, 7, 6)
        nudShowPicture.Name = "nudShowPicture"
        nudShowPicture.Size = New Size(163, 39)
        nudShowPicture.TabIndex = 45
        ' 
        ' picShowPic
        ' 
        picShowPic.BackColor = Color.Black
        picShowPic.BackgroundImageLayout = ImageLayout.Stretch
        picShowPic.Location = New Point(17, 45)
        picShowPic.Margin = New Padding(7, 6, 7, 6)
        picShowPic.Name = "picShowPic"
        picShowPic.Size = New Size(217, 228)
        picShowPic.TabIndex = 42
        picShowPic.TabStop = False
        ' 
        ' fraShowText
        ' 
        fraShowText.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraShowText.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraShowText.Controls.Add(DarkLabel27)
        fraShowText.Controls.Add(txtShowText)
        fraShowText.Controls.Add(btnShowTextCancel)
        fraShowText.Controls.Add(btnShowTextOk)
        fraShowText.Controls.Add(picShowTextFace)
        fraShowText.Controls.Add(DarkLabel26)
        fraShowText.Controls.Add(nudShowTextFace)
        fraShowText.ForeColor = Color.Gainsboro
        fraShowText.Location = New Point(13, 749)
        fraShowText.Margin = New Padding(7, 6, 7, 6)
        fraShowText.Name = "fraShowText"
        fraShowText.Padding = New Padding(7, 6, 7, 6)
        fraShowText.Size = New Size(537, 700)
        fraShowText.TabIndex = 17
        fraShowText.TabStop = False
        fraShowText.Text = "Show Text"
        fraShowText.Visible = False
        ' 
        ' DarkLabel27
        ' 
        DarkLabel27.AutoSize = True
        DarkLabel27.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel27.Location = New Point(15, 47)
        DarkLabel27.Margin = New Padding(7, 0, 7, 0)
        DarkLabel27.Name = "DarkLabel27"
        DarkLabel27.Size = New Size(57, 32)
        DarkLabel27.TabIndex = 26
        DarkLabel27.Text = "Text"
        ' 
        ' txtShowText
        ' 
        txtShowText.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtShowText.BorderStyle = BorderStyle.FixedSingle
        txtShowText.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtShowText.Location = New Point(19, 94)
        txtShowText.Margin = New Padding(7, 6, 7, 6)
        txtShowText.Multiline = True
        txtShowText.Name = "txtShowText"
        txtShowText.Size = New Size(492, 256)
        txtShowText.TabIndex = 21
        ' 
        ' btnShowTextCancel
        ' 
        btnShowTextCancel.Location = New Point(362, 621)
        btnShowTextCancel.Margin = New Padding(7, 6, 7, 6)
        btnShowTextCancel.Name = "btnShowTextCancel"
        btnShowTextCancel.Padding = New Padding(11, 13, 11, 13)
        btnShowTextCancel.Size = New Size(163, 58)
        btnShowTextCancel.TabIndex = 24
        btnShowTextCancel.Text = "Cancel"
        ' 
        ' btnShowTextOk
        ' 
        btnShowTextOk.Location = New Point(186, 621)
        btnShowTextOk.Margin = New Padding(7, 6, 7, 6)
        btnShowTextOk.Name = "btnShowTextOk"
        btnShowTextOk.Padding = New Padding(11, 13, 11, 13)
        btnShowTextOk.Size = New Size(163, 58)
        btnShowTextOk.TabIndex = 25
        btnShowTextOk.Text = "Ok"
        ' 
        ' picShowTextFace
        ' 
        picShowTextFace.BackColor = Color.Black
        picShowTextFace.BackgroundImageLayout = ImageLayout.Zoom
        picShowTextFace.Location = New Point(15, 367)
        picShowTextFace.Margin = New Padding(7, 6, 7, 6)
        picShowTextFace.Name = "picShowTextFace"
        picShowTextFace.Size = New Size(217, 228)
        picShowTextFace.TabIndex = 2
        picShowTextFace.TabStop = False
        ' 
        ' DarkLabel26
        ' 
        DarkLabel26.AutoSize = True
        DarkLabel26.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel26.Location = New Point(238, 550)
        DarkLabel26.Margin = New Padding(7, 0, 7, 0)
        DarkLabel26.Name = "DarkLabel26"
        DarkLabel26.Size = New Size(66, 32)
        DarkLabel26.TabIndex = 22
        DarkLabel26.Text = "Face:"
        ' 
        ' nudShowTextFace
        ' 
        nudShowTextFace.Location = New Point(319, 546)
        nudShowTextFace.Margin = New Padding(7, 6, 7, 6)
        nudShowTextFace.Name = "nudShowTextFace"
        nudShowTextFace.Size = New Size(199, 39)
        nudShowTextFace.TabIndex = 23
        ' 
        ' pnlVariableSwitches
        ' 
        pnlVariableSwitches.Controls.Add(FraRenaming)
        pnlVariableSwitches.Controls.Add(fraLabeling)
        pnlVariableSwitches.Location = New Point(1733, 495)
        pnlVariableSwitches.Margin = New Padding(7, 6, 7, 6)
        pnlVariableSwitches.Name = "pnlVariableSwitches"
        pnlVariableSwitches.Size = New Size(201, 224)
        pnlVariableSwitches.TabIndex = 11
        ' 
        ' FraRenaming
        ' 
        FraRenaming.Controls.Add(btnRename_Cancel)
        FraRenaming.Controls.Add(btnRename_Ok)
        FraRenaming.Controls.Add(fraRandom10)
        FraRenaming.ForeColor = Color.Gainsboro
        FraRenaming.Location = New Point(511, 1056)
        FraRenaming.Margin = New Padding(7, 6, 7, 6)
        FraRenaming.Name = "FraRenaming"
        FraRenaming.Padding = New Padding(7, 6, 7, 6)
        FraRenaming.Size = New Size(789, 352)
        FraRenaming.TabIndex = 8
        FraRenaming.TabStop = False
        FraRenaming.Text = "Renaming Variable/Switch"
        FraRenaming.Visible = False
        ' 
        ' btnRename_Cancel
        ' 
        btnRename_Cancel.ForeColor = Color.Black
        btnRename_Cancel.Location = New Point(496, 252)
        btnRename_Cancel.Margin = New Padding(7, 6, 7, 6)
        btnRename_Cancel.Name = "btnRename_Cancel"
        btnRename_Cancel.Size = New Size(163, 58)
        btnRename_Cancel.TabIndex = 2
        btnRename_Cancel.Text = "Cancel"
        btnRename_Cancel.UseVisualStyleBackColor = True
        ' 
        ' btnRename_Ok
        ' 
        btnRename_Ok.ForeColor = Color.Black
        btnRename_Ok.Location = New Point(117, 252)
        btnRename_Ok.Margin = New Padding(7, 6, 7, 6)
        btnRename_Ok.Name = "btnRename_Ok"
        btnRename_Ok.Size = New Size(163, 58)
        btnRename_Ok.TabIndex = 1
        btnRename_Ok.Text = "Ok"
        btnRename_Ok.UseVisualStyleBackColor = True
        ' 
        ' fraRandom10
        ' 
        fraRandom10.Controls.Add(txtRename)
        fraRandom10.Controls.Add(lblEditing)
        fraRandom10.ForeColor = Color.Gainsboro
        fraRandom10.Location = New Point(13, 47)
        fraRandom10.Margin = New Padding(7, 6, 7, 6)
        fraRandom10.Name = "fraRandom10"
        fraRandom10.Padding = New Padding(7, 6, 7, 6)
        fraRandom10.Size = New Size(763, 190)
        fraRandom10.TabIndex = 0
        fraRandom10.TabStop = False
        fraRandom10.Text = "Editing Variable/Switch"
        ' 
        ' txtRename
        ' 
        txtRename.Location = New Point(13, 100)
        txtRename.Margin = New Padding(7, 6, 7, 6)
        txtRename.Name = "txtRename"
        txtRename.Size = New Size(732, 39)
        txtRename.TabIndex = 1
        ' 
        ' lblEditing
        ' 
        lblEditing.AutoSize = True
        lblEditing.Location = New Point(7, 62)
        lblEditing.Margin = New Padding(7, 0, 7, 0)
        lblEditing.Name = "lblEditing"
        lblEditing.Size = New Size(224, 32)
        lblEditing.TabIndex = 0
        lblEditing.Text = "Naming Variable #1"
        ' 
        ' fraLabeling
        ' 
        fraLabeling.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        fraLabeling.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        fraLabeling.Controls.Add(lstSwitches)
        fraLabeling.Controls.Add(lstVariables)
        fraLabeling.Controls.Add(btnLabel_Cancel)
        fraLabeling.Controls.Add(lblRandomLabel36)
        fraLabeling.Controls.Add(btnRenameVariable)
        fraLabeling.Controls.Add(lblRandomLabel25)
        fraLabeling.Controls.Add(btnRenameSwitch)
        fraLabeling.Controls.Add(btnLabel_Ok)
        fraLabeling.ForeColor = Color.Gainsboro
        fraLabeling.Location = New Point(422, 70)
        fraLabeling.Margin = New Padding(7, 6, 7, 6)
        fraLabeling.Name = "fraLabeling"
        fraLabeling.Padding = New Padding(7, 6, 7, 6)
        fraLabeling.Size = New Size(988, 954)
        fraLabeling.TabIndex = 0
        fraLabeling.TabStop = False
        fraLabeling.Text = "Label Variables and  Switches   "
        ' 
        ' lstSwitches
        ' 
        lstSwitches.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        lstSwitches.BorderStyle = BorderStyle.FixedSingle
        lstSwitches.ForeColor = Color.Gainsboro
        lstSwitches.FormattingEnabled = True
        lstSwitches.Location = New Point(511, 96)
        lstSwitches.Margin = New Padding(7, 6, 7, 6)
        lstSwitches.Name = "lstSwitches"
        lstSwitches.Size = New Size(442, 706)
        lstSwitches.TabIndex = 7
        ' 
        ' lstVariables
        ' 
        lstVariables.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        lstVariables.BorderStyle = BorderStyle.FixedSingle
        lstVariables.ForeColor = Color.Gainsboro
        lstVariables.FormattingEnabled = True
        lstVariables.Location = New Point(30, 96)
        lstVariables.Margin = New Padding(7, 6, 7, 6)
        lstVariables.Name = "lstVariables"
        lstVariables.Size = New Size(442, 706)
        lstVariables.TabIndex = 6
        ' 
        ' btnLabel_Cancel
        ' 
        btnLabel_Cancel.ForeColor = Color.Black
        btnLabel_Cancel.Location = New Point(511, 838)
        btnLabel_Cancel.Margin = New Padding(7, 6, 7, 6)
        btnLabel_Cancel.Name = "btnLabel_Cancel"
        btnLabel_Cancel.Size = New Size(163, 58)
        btnLabel_Cancel.TabIndex = 12
        btnLabel_Cancel.Text = "Cancel"
        btnLabel_Cancel.UseVisualStyleBackColor = True
        ' 
        ' lblRandomLabel36
        ' 
        lblRandomLabel36.AutoSize = True
        lblRandomLabel36.Location = New Point(635, 58)
        lblRandomLabel36.Margin = New Padding(7, 0, 7, 0)
        lblRandomLabel36.Name = "lblRandomLabel36"
        lblRandomLabel36.Size = New Size(177, 32)
        lblRandomLabel36.TabIndex = 5
        lblRandomLabel36.Text = "Player Switches"
        ' 
        ' btnRenameVariable
        ' 
        btnRenameVariable.ForeColor = Color.Black
        btnRenameVariable.Location = New Point(30, 838)
        btnRenameVariable.Margin = New Padding(7, 6, 7, 6)
        btnRenameVariable.Name = "btnRenameVariable"
        btnRenameVariable.Size = New Size(230, 58)
        btnRenameVariable.TabIndex = 9
        btnRenameVariable.Text = "Rename Variable"
        btnRenameVariable.UseVisualStyleBackColor = True
        ' 
        ' lblRandomLabel25
        ' 
        lblRandomLabel25.AutoSize = True
        lblRandomLabel25.Location = New Point(173, 51)
        lblRandomLabel25.Margin = New Padding(7, 0, 7, 0)
        lblRandomLabel25.Name = "lblRandomLabel25"
        lblRandomLabel25.Size = New Size(179, 32)
        lblRandomLabel25.TabIndex = 4
        lblRandomLabel25.Text = "Player Variables"
        ' 
        ' btnRenameSwitch
        ' 
        btnRenameSwitch.ForeColor = Color.Black
        btnRenameSwitch.Location = New Point(719, 838)
        btnRenameSwitch.Margin = New Padding(7, 6, 7, 6)
        btnRenameSwitch.Name = "btnRenameSwitch"
        btnRenameSwitch.Size = New Size(236, 58)
        btnRenameSwitch.TabIndex = 10
        btnRenameSwitch.Text = "Rename Switch"
        btnRenameSwitch.UseVisualStyleBackColor = True
        ' 
        ' btnLabel_Ok
        ' 
        btnLabel_Ok.ForeColor = Color.Black
        btnLabel_Ok.Location = New Point(312, 838)
        btnLabel_Ok.Margin = New Padding(7, 6, 7, 6)
        btnLabel_Ok.Name = "btnLabel_Ok"
        btnLabel_Ok.Size = New Size(163, 58)
        btnLabel_Ok.TabIndex = 11
        btnLabel_Ok.Text = "Ok"
        btnLabel_Ok.UseVisualStyleBackColor = True
        ' 
        ' FrmEditor_Events
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        ClientSize = New Size(3444, 1399)
        Controls.Add(pnlVariableSwitches)
        Controls.Add(fraDialogue)
        Controls.Add(fraMoveRoute)
        Controls.Add(btnOk)
        Controls.Add(btnCancel)
        Controls.Add(btnLabeling)
        Controls.Add(tabPages)
        Controls.Add(fraPageSetUp)
        Controls.Add(pnlTabPage)
        Controls.Add(pnlGraphicSel)
        ForeColor = Color.Gainsboro
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(7, 6, 7, 6)
        Name = "FrmEditor_Events"
        Text = "Event Editor"
        fraPageSetUp.ResumeLayout(False)
        fraPageSetUp.PerformLayout()
        tabPages.ResumeLayout(False)
        pnlTabPage.ResumeLayout(False)
        DarkGroupBox2.ResumeLayout(False)
        fraGraphicPic.ResumeLayout(False)
        CType(picGraphic, System.ComponentModel.ISupportInitialize).EndInit()
        DarkGroupBox6.ResumeLayout(False)
        DarkGroupBox6.PerformLayout()
        DarkGroupBox5.ResumeLayout(False)
        DarkGroupBox4.ResumeLayout(False)
        CType(picGraphicSel, System.ComponentModel.ISupportInitialize).EndInit()
        DarkGroupBox3.ResumeLayout(False)
        DarkGroupBox3.PerformLayout()
        DarkGroupBox1.ResumeLayout(False)
        DarkGroupBox1.PerformLayout()
        CType(nudPlayerVariable, System.ComponentModel.ISupportInitialize).EndInit()
        fraGraphic.ResumeLayout(False)
        fraGraphic.PerformLayout()
        CType(nudGraphic, System.ComponentModel.ISupportInitialize).EndInit()
        fraCommands.ResumeLayout(False)
        DarkGroupBox8.ResumeLayout(False)
        fraMoveRoute.ResumeLayout(False)
        fraMoveRoute.PerformLayout()
        DarkGroupBox10.ResumeLayout(False)
        fraDialogue.ResumeLayout(False)
        fraConditionalBranch.ResumeLayout(False)
        fraConditionalBranch.PerformLayout()
        CType(nudCondition_LevelAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudCondition_HasItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudCondition_PlayerVarCondition, System.ComponentModel.ISupportInitialize).EndInit()
        fraMoveRouteWait.ResumeLayout(False)
        fraMoveRouteWait.PerformLayout()
        fraCustomScript.ResumeLayout(False)
        fraCustomScript.PerformLayout()
        CType(nudCustomScript, System.ComponentModel.ISupportInitialize).EndInit()
        fraSetWeather.ResumeLayout(False)
        fraSetWeather.PerformLayout()
        CType(nudWeatherIntensity, System.ComponentModel.ISupportInitialize).EndInit()
        fraSpawnNpc.ResumeLayout(False)
        fraGiveExp.ResumeLayout(False)
        fraGiveExp.PerformLayout()
        CType(nudGiveExp, System.ComponentModel.ISupportInitialize).EndInit()
        fraEndQuest.ResumeLayout(False)
        fraSetAccess.ResumeLayout(False)
        fraOpenShop.ResumeLayout(False)
        fraChangeLevel.ResumeLayout(False)
        fraChangeLevel.PerformLayout()
        CType(nudChangeLevel, System.ComponentModel.ISupportInitialize).EndInit()
        fraChangeGender.ResumeLayout(False)
        fraChangeGender.PerformLayout()
        fraGoToLabel.ResumeLayout(False)
        fraGoToLabel.PerformLayout()
        fraShowChoices.ResumeLayout(False)
        fraShowChoices.PerformLayout()
        CType(picShowChoicesFace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudShowChoicesFace, System.ComponentModel.ISupportInitialize).EndInit()
        fraPlayerVariable.ResumeLayout(False)
        fraPlayerVariable.PerformLayout()
        CType(nudVariableData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudVariableData4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudVariableData3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudVariableData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudVariableData0, System.ComponentModel.ISupportInitialize).EndInit()
        fraChangeSprite.ResumeLayout(False)
        fraChangeSprite.PerformLayout()
        CType(nudChangeSprite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(picChangeSprite, System.ComponentModel.ISupportInitialize).EndInit()
        fraSetSelfSwitch.ResumeLayout(False)
        fraSetSelfSwitch.PerformLayout()
        fraMapTint.ResumeLayout(False)
        fraMapTint.PerformLayout()
        CType(nudMapTintData3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudMapTintData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudMapTintData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudMapTintData0, System.ComponentModel.ISupportInitialize).EndInit()
        fraShowChatBubble.ResumeLayout(False)
        fraShowChatBubble.PerformLayout()
        fraPlaySound.ResumeLayout(False)
        fraChangePK.ResumeLayout(False)
        fraCreateLabel.ResumeLayout(False)
        fraCreateLabel.PerformLayout()
        fraChangeJob.ResumeLayout(False)
        fraChangeJob.PerformLayout()
        fraChangeSkills.ResumeLayout(False)
        fraChangeSkills.PerformLayout()
        fraPlayerWarp.ResumeLayout(False)
        fraPlayerWarp.PerformLayout()
        CType(nudWPY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudWPX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudWPMap, System.ComponentModel.ISupportInitialize).EndInit()
        fraSetFog.ResumeLayout(False)
        fraSetFog.PerformLayout()
        CType(nudFogData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudFogData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudFogData0, System.ComponentModel.ISupportInitialize).EndInit()
        fraAddText.ResumeLayout(False)
        fraAddText.PerformLayout()
        fraPlayerSwitch.ResumeLayout(False)
        fraPlayerSwitch.PerformLayout()
        fraChangeItems.ResumeLayout(False)
        fraChangeItems.PerformLayout()
        CType(nudChangeItemsAmount, System.ComponentModel.ISupportInitialize).EndInit()
        fraPlayBGM.ResumeLayout(False)
        fraPlayAnimation.ResumeLayout(False)
        fraPlayAnimation.PerformLayout()
        CType(nudPlayAnimTileY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudPlayAnimTileX, System.ComponentModel.ISupportInitialize).EndInit()
        fraSetWait.ResumeLayout(False)
        fraSetWait.PerformLayout()
        CType(nudWaitAmount, System.ComponentModel.ISupportInitialize).EndInit()
        fraShowPic.ResumeLayout(False)
        fraShowPic.PerformLayout()
        CType(nudPicOffsetY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudPicOffsetX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudShowPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(picShowPic, System.ComponentModel.ISupportInitialize).EndInit()
        fraShowText.ResumeLayout(False)
        fraShowText.PerformLayout()
        CType(picShowTextFace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudShowTextFace, System.ComponentModel.ISupportInitialize).EndInit()
        pnlVariableSwitches.ResumeLayout(False)
        FraRenaming.ResumeLayout(False)
        fraRandom10.ResumeLayout(False)
        fraRandom10.PerformLayout()
        fraLabeling.ResumeLayout(False)
        fraLabeling.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents tvCommands As TreeView
    Friend WithEvents fraPageSetUp As DarkUI.Controls.DarkGroupBox
    Friend WithEvents tabPages As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents txtName As DarkUI.Controls.DarkTextBox
    Friend WithEvents DarkLabel1 As DarkUI.Controls.DarkLabel
    Friend WithEvents btnNewPage As DarkUI.Controls.DarkButton
    Friend WithEvents btnCopyPage As DarkUI.Controls.DarkButton
    Friend WithEvents btnPastePage As DarkUI.Controls.DarkButton
    Friend WithEvents btnClearPage As DarkUI.Controls.DarkButton
    Friend WithEvents btnDeletePage As DarkUI.Controls.DarkButton
    Friend WithEvents pnlTabPage As Panel
    Friend WithEvents DarkGroupBox1 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents chkPlayerVar As DarkUI.Controls.DarkCheckBox
    Friend WithEvents cmbPlayerVar As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel2 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbPlayervarCompare As DarkUI.Controls.DarkComboBox
    Friend WithEvents nudPlayerVariable As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents chkPlayerSwitch As DarkUI.Controls.DarkCheckBox
    Friend WithEvents cmbPlayerSwitch As DarkUI.Controls.DarkComboBox
    Friend WithEvents cmbPlayerSwitchCompare As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel3 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbHasItem As DarkUI.Controls.DarkComboBox
    Friend WithEvents chkHasItem As DarkUI.Controls.DarkCheckBox
    Friend WithEvents cmbSelfSwitch As DarkUI.Controls.DarkComboBox
    Friend WithEvents chkSelfSwitch As DarkUI.Controls.DarkCheckBox
    Friend WithEvents cmbSelfSwitchCompare As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel4 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkGroupBox3 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents chkGlobal As DarkUI.Controls.DarkCheckBox
    Friend WithEvents DarkLabel5 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbMoveType As DarkUI.Controls.DarkComboBox
    Friend WithEvents btnMoveRoute As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel6 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbMoveSpeed As DarkUI.Controls.DarkComboBox
    Friend WithEvents cmbMoveFreq As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel7 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkGroupBox4 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkGroupBox5 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbTrigger As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkGroupBox7 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkLabel8 As DarkUI.Controls.DarkLabel
    Friend WithEvents lstCommands As ListBox
    Friend WithEvents DarkGroupBox8 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnAddCommand As DarkUI.Controls.DarkButton
    Friend WithEvents btnDeleteCommand As DarkUI.Controls.DarkButton
    Friend WithEvents btnEditCommand As DarkUI.Controls.DarkButton
    Friend WithEvents btnClearCommand As DarkUI.Controls.DarkButton
    Friend WithEvents fraCommands As Panel
    Friend WithEvents btnLabeling As DarkUI.Controls.DarkButton
    Friend WithEvents btnCancel As DarkUI.Controls.DarkButton
    Friend WithEvents btnOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnCancelCommand As DarkUI.Controls.DarkButton
    Friend WithEvents fraMoveRoute As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbEvent As DarkUI.Controls.DarkComboBox
    Friend WithEvents lstMoveRoute As ListBox
    Friend WithEvents DarkGroupBox10 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents lstvwMoveRoute As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents chkRepeatRoute As DarkUI.Controls.DarkCheckBox
    Friend WithEvents chkIgnoreMove As DarkUI.Controls.DarkCheckBox
    Friend WithEvents btnMoveRouteOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnMoveRouteCancel As DarkUI.Controls.DarkButton
    Friend WithEvents picGraphicSel As PictureBox
    Friend WithEvents fraDialogue As DarkUI.Controls.DarkGroupBox
    Friend WithEvents fraConditionalBranch As DarkUI.Controls.DarkGroupBox
    Friend WithEvents optCondition0 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents cmbCondition_PlayerVarIndex As DarkUI.Controls.DarkComboBox
    Friend WithEvents nudCondition_PlayerVarCondition As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents cmbCondition_PlayerVarCompare As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel14 As DarkUI.Controls.DarkLabel
    Friend WithEvents optCondition1 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents DarkLabel15 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbCondtion_PlayerSwitchCondition As DarkUI.Controls.DarkComboBox
    Friend WithEvents cmbCondition_PlayerSwitch As DarkUI.Controls.DarkComboBox
    Friend WithEvents optCondition2 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents nudCondition_HasItem As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel16 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbCondition_HasItem As DarkUI.Controls.DarkComboBox
    Friend WithEvents optCondition3 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents cmbCondition_JobIs As DarkUI.Controls.DarkComboBox
    Friend WithEvents optCondition4 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents cmbCondition_LearntSkill As DarkUI.Controls.DarkComboBox
    Friend WithEvents optCondition5 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents cmbCondition_LevelCompare As DarkUI.Controls.DarkComboBox
    Friend WithEvents nudCondition_LevelAmount As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents optCondition6 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents cmbCondition_SelfSwitchCondition As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel17 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbCondition_SelfSwitch As DarkUI.Controls.DarkComboBox
    Friend WithEvents optCondition8 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents cmbCondition_Gender As DarkUI.Controls.DarkComboBox
    Friend WithEvents btnConditionalBranchOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnConditionalBranchCancel As DarkUI.Controls.DarkButton
    Friend WithEvents fraChangeItems As DarkUI.Controls.DarkGroupBox
    Friend WithEvents fraPlayerSwitch As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbChangeItemIndex As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel21 As DarkUI.Controls.DarkLabel
    Friend WithEvents optChangeItemSet As DarkUI.Controls.DarkRadioButton
    Friend WithEvents optChangeItemRemove As DarkUI.Controls.DarkRadioButton
    Friend WithEvents optChangeItemAdd As DarkUI.Controls.DarkRadioButton
    Friend WithEvents nudChangeItemsAmount As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents btnChangeItemsOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnChangeItemsCancel As DarkUI.Controls.DarkButton
    Friend WithEvents cmbSwitch As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel22 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel23 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbPlayerSwitchSet As DarkUI.Controls.DarkComboBox
    Friend WithEvents btnSetPlayerSwitchOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnSetPlayerswitchCancel As DarkUI.Controls.DarkButton
    Friend WithEvents fraAddText As DarkUI.Controls.DarkGroupBox
    Friend WithEvents txtAddText_Text As DarkUI.Controls.DarkTextBox
    Friend WithEvents DarkLabel24 As DarkUI.Controls.DarkLabel
    Friend WithEvents optAddText_Player As DarkUI.Controls.DarkRadioButton
    Friend WithEvents DarkLabel25 As DarkUI.Controls.DarkLabel
    Friend WithEvents optAddText_Map As DarkUI.Controls.DarkRadioButton
    Friend WithEvents btnAddTextOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnAddTextCancel As DarkUI.Controls.DarkButton
    Friend WithEvents optAddText_Global As DarkUI.Controls.DarkRadioButton
    Friend WithEvents btnShowTextOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnShowTextCancel As DarkUI.Controls.DarkButton
    Friend WithEvents nudShowTextFace As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel26 As DarkUI.Controls.DarkLabel
    Friend WithEvents txtShowText As DarkUI.Controls.DarkTextBox
    Friend WithEvents picShowTextFace As PictureBox
    Friend WithEvents DarkLabel27 As DarkUI.Controls.DarkLabel
    Friend WithEvents fraShowText As DarkUI.Controls.DarkGroupBox
    Friend WithEvents fraSetFog As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnSetFogOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnSetFogCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel30 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel29 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel28 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudFogData2 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudFogData1 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudFogData0 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents fraPlayerWarp As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnPlayerWarpOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnPlayerWarpCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel31 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbWarpPlayerDir As DarkUI.Controls.DarkComboBox
    Friend WithEvents nudWPY As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel32 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudWPX As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel33 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudWPMap As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel34 As DarkUI.Controls.DarkLabel
    Friend WithEvents fraPlayBGM As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbPlayBGM As DarkUI.Controls.DarkComboBox
    Friend WithEvents btnPlayBgmOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnPlayBgmCancel As DarkUI.Controls.DarkButton
    Friend WithEvents fraChangeSkills As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbChangeSkills As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel37 As DarkUI.Controls.DarkLabel
    Friend WithEvents optChangeSkillsAdd As DarkUI.Controls.DarkRadioButton
    Friend WithEvents btnChangeSkillsOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnChangeSkillsCancel As DarkUI.Controls.DarkButton
    Friend WithEvents optChangeSkillsRemove As DarkUI.Controls.DarkRadioButton
    Friend WithEvents fraChangeJob As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbChangeJob As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel38 As DarkUI.Controls.DarkLabel
    Friend WithEvents btnChangeJobOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnChangeJobCancel As DarkUI.Controls.DarkButton
    Friend WithEvents fraCreateLabel As DarkUI.Controls.DarkGroupBox
    Friend WithEvents lblLabelName As DarkUI.Controls.DarkLabel
    Friend WithEvents txtLabelName As DarkUI.Controls.DarkTextBox
    Friend WithEvents btnCreatelabelOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnCreatelabelCancel As DarkUI.Controls.DarkButton
    Friend WithEvents fraChangePK As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnChangePkOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnChangePkCancel As DarkUI.Controls.DarkButton
    Friend WithEvents cmbSetPK As DarkUI.Controls.DarkComboBox
    Friend WithEvents fraPlaySound As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnPlaySoundOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnPlaySoundCancel As DarkUI.Controls.DarkButton
    Friend WithEvents cmbPlaySound As DarkUI.Controls.DarkComboBox
    Friend WithEvents fraShowChatBubble As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkLabel39 As DarkUI.Controls.DarkLabel
    Friend WithEvents txtChatbubbleText As DarkUI.Controls.DarkTextBox
    Friend WithEvents DarkLabel40 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbChatBubbleTarget As DarkUI.Controls.DarkComboBox
    Friend WithEvents cmbChatBubbleTargetType As DarkUI.Controls.DarkComboBox
    Friend WithEvents btnShowChatBubbleOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnShowChatBubbleCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel41 As DarkUI.Controls.DarkLabel
    Friend WithEvents fraMapTint As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnMapTintOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnMapTintCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel42 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudMapTintData3 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudMapTintData2 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel43 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel44 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudMapTintData1 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudMapTintData0 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel45 As DarkUI.Controls.DarkLabel
    Friend WithEvents fraSetSelfSwitch As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbSetSelfSwitch As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel46 As DarkUI.Controls.DarkLabel
    Friend WithEvents btnSelfswitchOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnSelfswitchCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel47 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbSetSelfSwitchTo As DarkUI.Controls.DarkComboBox
    Friend WithEvents fraChangeSprite As DarkUI.Controls.DarkGroupBox
    Friend WithEvents picChangeSprite As PictureBox
    Friend WithEvents btnChangeSpriteOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnChangeSpriteCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel48 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudChangeSprite As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents fraPlayerVariable As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbVariable As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel49 As DarkUI.Controls.DarkLabel
    Friend WithEvents optVariableAction0 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents optVariableAction1 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents nudVariableData1 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudVariableData0 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents optVariableAction3 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents nudVariableData3 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents optVariableAction2 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents btnPlayerVarOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnPlayerVarCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel51 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel50 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudVariableData4 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudVariableData2 As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents fraShowChoices As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkLabel52 As DarkUI.Controls.DarkLabel
    Friend WithEvents txtChoicePrompt As DarkUI.Controls.DarkTextBox
    Friend WithEvents btnShowChoicesOk As DarkUI.Controls.DarkButton
    Friend WithEvents picShowChoicesFace As PictureBox
    Friend WithEvents btnShowChoicesCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel53 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudShowChoicesFace As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel56 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel57 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel55 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel54 As DarkUI.Controls.DarkLabel
    Friend WithEvents txtChoices4 As DarkUI.Controls.DarkTextBox
    Friend WithEvents txtChoices3 As DarkUI.Controls.DarkTextBox
    Friend WithEvents txtChoices2 As DarkUI.Controls.DarkTextBox
    Friend WithEvents txtChoices1 As DarkUI.Controls.DarkTextBox
    Friend WithEvents fraGoToLabel As DarkUI.Controls.DarkGroupBox
    Friend WithEvents txtGotoLabel As DarkUI.Controls.DarkTextBox
    Friend WithEvents DarkLabel60 As DarkUI.Controls.DarkLabel
    Friend WithEvents btnGoToLabelOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnGoToLabelCancel As DarkUI.Controls.DarkButton
    Friend WithEvents fraPlayAnimation As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkLabel61 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbPlayAnim As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel62 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbAnimTargetType As DarkUI.Controls.DarkComboBox
    Friend WithEvents nudPlayAnimTileY As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudPlayAnimTileX As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents cmbPlayAnimEvent As DarkUI.Controls.DarkComboBox
    Friend WithEvents btnPlayAnimationOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnPlayAnimationCancel As DarkUI.Controls.DarkButton
    Friend WithEvents lblPlayAnimY As DarkUI.Controls.DarkLabel
    Friend WithEvents lblPlayAnimX As DarkUI.Controls.DarkLabel
    Friend WithEvents fraChangeGender As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnChangeGenderOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnChangeGenderCancel As DarkUI.Controls.DarkButton
    Friend WithEvents optChangeSexFemale As DarkUI.Controls.DarkRadioButton
    Friend WithEvents optChangeSexMale As DarkUI.Controls.DarkRadioButton
    Friend WithEvents fraChangeLevel As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnChangeLevelOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnChangeLevelCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel65 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudChangeLevel As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents fraOpenShop As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbOpenShop As DarkUI.Controls.DarkComboBox
    Friend WithEvents btnOpenShopOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnOpenShopCancel As DarkUI.Controls.DarkButton
    Friend WithEvents fraShowPic As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkLabel67 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel68 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudPicOffsetY As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents nudPicOffsetX As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel69 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbPicLoc As DarkUI.Controls.DarkComboBox
    Friend WithEvents nudShowPicture As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents picShowPic As PictureBox
    Friend WithEvents btnShowPicOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnShowPicCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel71 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel70 As DarkUI.Controls.DarkLabel
    Friend WithEvents fraSetWait As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnSetWaitOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnSetWaitCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel74 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel72 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel73 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudWaitAmount As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents fraSetAccess As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnSetAccessOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnSetAccessCancel As DarkUI.Controls.DarkButton
    Friend WithEvents cmbSetAccess As DarkUI.Controls.DarkComboBox
    Friend WithEvents fraEndQuest As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnEndQuestOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnEndQuestCancel As DarkUI.Controls.DarkButton
    Friend WithEvents cmbEndQuest As DarkUI.Controls.DarkComboBox
    Friend WithEvents fraSetWeather As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkLabel75 As DarkUI.Controls.DarkLabel
    Friend WithEvents CmbWeather As DarkUI.Controls.DarkComboBox
    Friend WithEvents btnSetWeatherOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnSetWeatherCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel76 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudWeatherIntensity As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents fraGiveExp As DarkUI.Controls.DarkGroupBox
    Friend WithEvents DarkLabel77 As DarkUI.Controls.DarkLabel
    Friend WithEvents fraSpawnNpc As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbSpawnNpc As DarkUI.Controls.DarkComboBox
    Friend WithEvents btnGiveExpOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnGiveExpCancel As DarkUI.Controls.DarkButton
    Friend WithEvents nudGiveExp As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents btnSpawnNpcOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnSpawnNpcCancel As DarkUI.Controls.DarkButton
    Friend WithEvents fraCustomScript As DarkUI.Controls.DarkGroupBox
    Friend WithEvents nudCustomScript As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel78 As DarkUI.Controls.DarkLabel
    Friend WithEvents btnCustomScriptCancel As DarkUI.Controls.DarkButton
    Friend WithEvents btnCustomScriptOk As DarkUI.Controls.DarkButton
    Friend WithEvents fraMoveRouteWait As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnMoveWaitCancel As DarkUI.Controls.DarkButton
    Friend WithEvents btnMoveWaitOk As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel79 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbMoveWait As DarkUI.Controls.DarkComboBox
    Friend WithEvents pnlVariableSwitches As Panel
    Friend WithEvents fraLabeling As DarkUI.Controls.DarkGroupBox
    Friend WithEvents lstSwitches As ListBox
    Friend WithEvents lstVariables As ListBox
    Friend WithEvents lblRandomLabel36 As Label
    Friend WithEvents lblRandomLabel25 As Label
    Friend WithEvents FraRenaming As GroupBox
    Friend WithEvents btnRename_Cancel As Button
    Friend WithEvents btnRename_Ok As Button
    Friend WithEvents fraRandom10 As GroupBox
    Friend WithEvents txtRename As TextBox
    Friend WithEvents lblEditing As Label
    Friend WithEvents btnLabel_Cancel As Button
    Friend WithEvents btnRenameVariable As Button
    Friend WithEvents btnRenameSwitch As Button
    Friend WithEvents btnLabel_Ok As Button
    Friend WithEvents pnlGraphicSel As Panel
    Friend WithEvents cmbCondition_Time As DarkUI.Controls.DarkComboBox
    Friend WithEvents optCondition9 As DarkUI.Controls.DarkRadioButton
    Friend WithEvents DarkGroupBox6 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents chkShowName As DarkUI.Controls.DarkCheckBox
    Friend WithEvents chkWalkThrough As DarkUI.Controls.DarkCheckBox
    Friend WithEvents chkDirFix As DarkUI.Controls.DarkCheckBox
    Friend WithEvents chkWalkAnim As DarkUI.Controls.DarkCheckBox
    Friend WithEvents fraGraphicPic As DarkUI.Controls.DarkGroupBox
    Friend WithEvents picGraphic As PictureBox
    Friend WithEvents fraGraphic As DarkUI.Controls.DarkGroupBox
    Friend WithEvents btnGraphicOk As DarkUI.Controls.DarkButton
    Friend WithEvents btnGraphicCancel As DarkUI.Controls.DarkButton
    Friend WithEvents DarkLabel13 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudGraphic As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel12 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbGraphic As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel11 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkGroupBox2 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbPositioning As DarkUI.Controls.DarkComboBox
End Class
