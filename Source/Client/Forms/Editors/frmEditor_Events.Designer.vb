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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Show Text")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Show Choices")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Add Chatbox Text")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Show ChatBubble")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Messages", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4})
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Player Variable")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Player Switch")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Self Switch")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Event Processing", New System.Windows.Forms.TreeNode() {TreeNode6, TreeNode7, TreeNode8})
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Conditional Branch")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stop Event Processing")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Label")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("GoTo Label")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Flow Control", New System.Windows.Forms.TreeNode() {TreeNode10, TreeNode11, TreeNode12, TreeNode13})
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Items")
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Restore HP")
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Restore MP")
        Dim TreeNode18 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Level Up")
        Dim TreeNode19 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Level")
        Dim TreeNode20 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Skills")
        Dim TreeNode21 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Job")
        Dim TreeNode22 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Sprite")
        Dim TreeNode23 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change Gender")
        Dim TreeNode24 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Change PK")
        Dim TreeNode25 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Give Experience")
        Dim TreeNode26 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Player Options", New System.Windows.Forms.TreeNode() {TreeNode15, TreeNode16, TreeNode17, TreeNode18, TreeNode19, TreeNode20, TreeNode21, TreeNode22, TreeNode23, TreeNode24, TreeNode25})
        Dim TreeNode27 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Warp Player")
        Dim TreeNode28 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Move Route")
        Dim TreeNode29 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Wait for Route Completion")
        Dim TreeNode30 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Force Spawn Npc")
        Dim TreeNode31 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Hold Player")
        Dim TreeNode32 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Release Player")
        Dim TreeNode33 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Movement", New System.Windows.Forms.TreeNode() {TreeNode27, TreeNode28, TreeNode29, TreeNode30, TreeNode31, TreeNode32})
        Dim TreeNode34 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Animation")
        Dim TreeNode35 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Animation", New System.Windows.Forms.TreeNode() {TreeNode34})
        Dim TreeNode36 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Begin Quest")
        Dim TreeNode37 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Complete Task")
        Dim TreeNode38 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("End Quest")
        Dim TreeNode39 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Questing", New System.Windows.Forms.TreeNode() {TreeNode36, TreeNode37, TreeNode38})
        Dim TreeNode40 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Fog")
        Dim TreeNode41 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Weather")
        Dim TreeNode42 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Map Tinting")
        Dim TreeNode43 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Map Functions", New System.Windows.Forms.TreeNode() {TreeNode40, TreeNode41, TreeNode42})
        Dim TreeNode44 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Play BGM")
        Dim TreeNode45 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stop BGM")
        Dim TreeNode46 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Play Sound")
        Dim TreeNode47 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stop Sounds")
        Dim TreeNode48 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Music and Sound", New System.Windows.Forms.TreeNode() {TreeNode44, TreeNode45, TreeNode46, TreeNode47})
        Dim TreeNode49 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Wait...")
        Dim TreeNode50 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Set Access")
        Dim TreeNode51 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Custom Script")
        Dim TreeNode52 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Etc...", New System.Windows.Forms.TreeNode() {TreeNode49, TreeNode50, TreeNode51})
        Dim TreeNode53 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Open Bank")
        Dim TreeNode54 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Open Shop")
        Dim TreeNode55 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Shop and Bank", New System.Windows.Forms.TreeNode() {TreeNode53, TreeNode54})
        Dim TreeNode56 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fade In")
        Dim TreeNode57 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fade Out")
        Dim TreeNode58 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Flash White")
        Dim TreeNode59 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Show Picture")
        Dim TreeNode60 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Hide Picture")
        Dim TreeNode61 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Cutscene Options", New System.Windows.Forms.TreeNode() {TreeNode56, TreeNode57, TreeNode58, TreeNode59, TreeNode60})
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Movement", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Wait", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Turning", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Speed", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Walk Animation", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Fixed Direction", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup7 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("WalkThrough", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup8 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Set Position", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup9 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Set Graphic", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move Up")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move Down")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move left")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move Right")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move Randomly")
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move To Player***")
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Move from Player***")
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Step Forwards")
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Step Backwards")
        Dim ListViewItem10 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Wait 100Ms")
        Dim ListViewItem11 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Wait 500Ms")
        Dim ListViewItem12 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Wait 1000Ms")
        Dim ListViewItem13 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Up")
        Dim ListViewItem14 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Down")
        Dim ListViewItem15 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Left")
        Dim ListViewItem16 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Right")
        Dim ListViewItem17 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn 90DG Right")
        Dim ListViewItem18 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn 90DG Left")
        Dim ListViewItem19 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn 180DG")
        Dim ListViewItem20 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn Randomly")
        Dim ListViewItem21 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn To Player***")
        Dim ListViewItem22 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Turn From Player***")
        Dim ListViewItem23 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 8x Slower")
        Dim ListViewItem24 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 4x Slower")
        Dim ListViewItem25 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 2x Slower")
        Dim ListViewItem26 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed To Normal")
        Dim ListViewItem27 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 2x Faster")
        Dim ListViewItem28 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Speed 4x Faster")
        Dim ListViewItem29 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Lowest")
        Dim ListViewItem30 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Lower")
        Dim ListViewItem31 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Normal")
        Dim ListViewItem32 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Higher")
        Dim ListViewItem33 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Freq. To Highest")
        Dim ListViewItem34 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Walking Animation ON")
        Dim ListViewItem35 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Walking Animation OFF")
        Dim ListViewItem36 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Fixed Direction ON")
        Dim ListViewItem37 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Fixed Direction OFF")
        Dim ListViewItem38 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Walkthrough ON")
        Dim ListViewItem39 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Walkthrough ON")
        Dim ListViewItem40 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Position Below Player")
        Dim ListViewItem41 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set PositionWith Player")
        Dim ListViewItem42 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Position Above Player")
        Dim ListViewItem43 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Set Graphic...")
        Me.tvCommands = New System.Windows.Forms.TreeView()
        Me.fraPageSetUp = New DarkUI.Controls.DarkGroupBox()
        Me.chkGlobal = New DarkUI.Controls.DarkCheckBox()
        Me.btnClearPage = New DarkUI.Controls.DarkButton()
        Me.btnDeletePage = New DarkUI.Controls.DarkButton()
        Me.btnPastePage = New DarkUI.Controls.DarkButton()
        Me.btnCopyPage = New DarkUI.Controls.DarkButton()
        Me.btnNewPage = New DarkUI.Controls.DarkButton()
        Me.txtName = New DarkUI.Controls.DarkTextBox()
        Me.DarkLabel1 = New DarkUI.Controls.DarkLabel()
        Me.tabPages = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.pnlTabPage = New System.Windows.Forms.Panel()
        Me.DarkGroupBox2 = New DarkUI.Controls.DarkGroupBox()
        Me.cmbPositioning = New DarkUI.Controls.DarkComboBox()
        Me.fraGraphicPic = New DarkUI.Controls.DarkGroupBox()
        Me.picGraphic = New System.Windows.Forms.PictureBox()
        Me.DarkGroupBox6 = New DarkUI.Controls.DarkGroupBox()
        Me.chkShowName = New DarkUI.Controls.DarkCheckBox()
        Me.chkWalkThrough = New DarkUI.Controls.DarkCheckBox()
        Me.chkDirFix = New DarkUI.Controls.DarkCheckBox()
        Me.chkWalkAnim = New DarkUI.Controls.DarkCheckBox()
        Me.DarkGroupBox7 = New DarkUI.Controls.DarkGroupBox()
        Me.DarkGroupBox5 = New DarkUI.Controls.DarkGroupBox()
        Me.cmbTrigger = New DarkUI.Controls.DarkComboBox()
        Me.DarkGroupBox4 = New DarkUI.Controls.DarkGroupBox()
        Me.picGraphicSel = New System.Windows.Forms.PictureBox()
        Me.DarkGroupBox3 = New DarkUI.Controls.DarkGroupBox()
        Me.DarkLabel7 = New DarkUI.Controls.DarkLabel()
        Me.cmbMoveFreq = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel6 = New DarkUI.Controls.DarkLabel()
        Me.cmbMoveSpeed = New DarkUI.Controls.DarkComboBox()
        Me.btnMoveRoute = New DarkUI.Controls.DarkButton()
        Me.cmbMoveType = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel5 = New DarkUI.Controls.DarkLabel()
        Me.DarkGroupBox1 = New DarkUI.Controls.DarkGroupBox()
        Me.cmbSelfSwitchCompare = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel4 = New DarkUI.Controls.DarkLabel()
        Me.cmbSelfSwitch = New DarkUI.Controls.DarkComboBox()
        Me.chkSelfSwitch = New DarkUI.Controls.DarkCheckBox()
        Me.cmbHasItem = New DarkUI.Controls.DarkComboBox()
        Me.chkHasItem = New DarkUI.Controls.DarkCheckBox()
        Me.cmbPlayerSwitchCompare = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel3 = New DarkUI.Controls.DarkLabel()
        Me.cmbPlayerSwitch = New DarkUI.Controls.DarkComboBox()
        Me.chkPlayerSwitch = New DarkUI.Controls.DarkCheckBox()
        Me.nudPlayerVariable = New DarkUI.Controls.DarkNumericUpDown()
        Me.cmbPlayervarCompare = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel2 = New DarkUI.Controls.DarkLabel()
        Me.cmbPlayerVar = New DarkUI.Controls.DarkComboBox()
        Me.chkPlayerVar = New DarkUI.Controls.DarkCheckBox()
        Me.fraGraphic = New DarkUI.Controls.DarkGroupBox()
        Me.btnGraphicOk = New DarkUI.Controls.DarkButton()
        Me.btnGraphicCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel13 = New DarkUI.Controls.DarkLabel()
        Me.nudGraphic = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel12 = New DarkUI.Controls.DarkLabel()
        Me.cmbGraphic = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel11 = New DarkUI.Controls.DarkLabel()
        Me.fraCommands = New System.Windows.Forms.Panel()
        Me.btnCancelCommand = New DarkUI.Controls.DarkButton()
        Me.lstCommands = New System.Windows.Forms.ListBox()
        Me.DarkGroupBox8 = New DarkUI.Controls.DarkGroupBox()
        Me.btnClearCommand = New DarkUI.Controls.DarkButton()
        Me.btnDeleteCommand = New DarkUI.Controls.DarkButton()
        Me.btnEditCommand = New DarkUI.Controls.DarkButton()
        Me.btnAddCommand = New DarkUI.Controls.DarkButton()
        Me.btnLabeling = New DarkUI.Controls.DarkButton()
        Me.btnCancel = New DarkUI.Controls.DarkButton()
        Me.btnOk = New DarkUI.Controls.DarkButton()
        Me.fraMoveRoute = New DarkUI.Controls.DarkGroupBox()
        Me.btnMoveRouteOk = New DarkUI.Controls.DarkButton()
        Me.btnMoveRouteCancel = New DarkUI.Controls.DarkButton()
        Me.chkRepeatRoute = New DarkUI.Controls.DarkCheckBox()
        Me.chkIgnoreMove = New DarkUI.Controls.DarkCheckBox()
        Me.DarkGroupBox10 = New DarkUI.Controls.DarkGroupBox()
        Me.lstvwMoveRoute = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader()
        Me.lstMoveRoute = New System.Windows.Forms.ListBox()
        Me.cmbEvent = New DarkUI.Controls.DarkComboBox()
        Me.pnlGraphicSel = New System.Windows.Forms.Panel()
        Me.fraDialogue = New DarkUI.Controls.DarkGroupBox()
        Me.fraShowPic = New DarkUI.Controls.DarkGroupBox()
        Me.btnShowPicOk = New DarkUI.Controls.DarkButton()
        Me.btnShowPicCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel71 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel70 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel67 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel68 = New DarkUI.Controls.DarkLabel()
        Me.nudPicOffsetY = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudPicOffsetX = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel69 = New DarkUI.Controls.DarkLabel()
        Me.cmbPicLoc = New DarkUI.Controls.DarkComboBox()
        Me.nudShowPicture = New DarkUI.Controls.DarkNumericUpDown()
        Me.picShowPic = New System.Windows.Forms.PictureBox()
        Me.fraMoveRouteWait = New DarkUI.Controls.DarkGroupBox()
        Me.btnMoveWaitCancel = New DarkUI.Controls.DarkButton()
        Me.btnMoveWaitOk = New DarkUI.Controls.DarkButton()
        Me.DarkLabel79 = New DarkUI.Controls.DarkLabel()
        Me.cmbMoveWait = New DarkUI.Controls.DarkComboBox()
        Me.fraCustomScript = New DarkUI.Controls.DarkGroupBox()
        Me.nudCustomScript = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel78 = New DarkUI.Controls.DarkLabel()
        Me.btnCustomScriptCancel = New DarkUI.Controls.DarkButton()
        Me.btnCustomScriptOk = New DarkUI.Controls.DarkButton()
        Me.fraSetWeather = New DarkUI.Controls.DarkGroupBox()
        Me.btnSetWeatherOk = New DarkUI.Controls.DarkButton()
        Me.btnSetWeatherCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel76 = New DarkUI.Controls.DarkLabel()
        Me.nudWeatherIntensity = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel75 = New DarkUI.Controls.DarkLabel()
        Me.CmbWeather = New DarkUI.Controls.DarkComboBox()
        Me.fraSpawnNpc = New DarkUI.Controls.DarkGroupBox()
        Me.btnSpawnNpcOk = New DarkUI.Controls.DarkButton()
        Me.btnSpawnNpcCancel = New DarkUI.Controls.DarkButton()
        Me.cmbSpawnNpc = New DarkUI.Controls.DarkComboBox()
        Me.fraGiveExp = New DarkUI.Controls.DarkGroupBox()
        Me.btnGiveExpOk = New DarkUI.Controls.DarkButton()
        Me.btnGiveExpCancel = New DarkUI.Controls.DarkButton()
        Me.nudGiveExp = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel77 = New DarkUI.Controls.DarkLabel()
        Me.fraEndQuest = New DarkUI.Controls.DarkGroupBox()
        Me.btnEndQuestOk = New DarkUI.Controls.DarkButton()
        Me.btnEndQuestCancel = New DarkUI.Controls.DarkButton()
        Me.cmbEndQuest = New DarkUI.Controls.DarkComboBox()
        Me.fraSetAccess = New DarkUI.Controls.DarkGroupBox()
        Me.btnSetAccessOk = New DarkUI.Controls.DarkButton()
        Me.btnSetAccessCancel = New DarkUI.Controls.DarkButton()
        Me.cmbSetAccess = New DarkUI.Controls.DarkComboBox()
        Me.fraOpenShop = New DarkUI.Controls.DarkGroupBox()
        Me.btnOpenShopOk = New DarkUI.Controls.DarkButton()
        Me.btnOpenShopCancel = New DarkUI.Controls.DarkButton()
        Me.cmbOpenShop = New DarkUI.Controls.DarkComboBox()
        Me.fraChangeLevel = New DarkUI.Controls.DarkGroupBox()
        Me.btnChangeLevelOk = New DarkUI.Controls.DarkButton()
        Me.btnChangeLevelCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel65 = New DarkUI.Controls.DarkLabel()
        Me.nudChangeLevel = New DarkUI.Controls.DarkNumericUpDown()
        Me.fraChangeGender = New DarkUI.Controls.DarkGroupBox()
        Me.btnChangeGenderOk = New DarkUI.Controls.DarkButton()
        Me.btnChangeGenderCancel = New DarkUI.Controls.DarkButton()
        Me.optChangeSexFemale = New DarkUI.Controls.DarkRadioButton()
        Me.optChangeSexMale = New DarkUI.Controls.DarkRadioButton()
        Me.fraGoToLabel = New DarkUI.Controls.DarkGroupBox()
        Me.btnGoToLabelOk = New DarkUI.Controls.DarkButton()
        Me.btnGoToLabelCancel = New DarkUI.Controls.DarkButton()
        Me.txtGotoLabel = New DarkUI.Controls.DarkTextBox()
        Me.DarkLabel60 = New DarkUI.Controls.DarkLabel()
        Me.fraShowChoices = New DarkUI.Controls.DarkGroupBox()
        Me.txtChoices4 = New DarkUI.Controls.DarkTextBox()
        Me.txtChoices3 = New DarkUI.Controls.DarkTextBox()
        Me.txtChoices2 = New DarkUI.Controls.DarkTextBox()
        Me.txtChoices1 = New DarkUI.Controls.DarkTextBox()
        Me.DarkLabel56 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel57 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel55 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel54 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel52 = New DarkUI.Controls.DarkLabel()
        Me.txtChoicePrompt = New DarkUI.Controls.DarkTextBox()
        Me.btnShowChoicesOk = New DarkUI.Controls.DarkButton()
        Me.picShowChoicesFace = New System.Windows.Forms.PictureBox()
        Me.btnShowChoicesCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel53 = New DarkUI.Controls.DarkLabel()
        Me.nudShowChoicesFace = New DarkUI.Controls.DarkNumericUpDown()
        Me.fraPlayerVariable = New DarkUI.Controls.DarkGroupBox()
        Me.nudVariableData2 = New DarkUI.Controls.DarkNumericUpDown()
        Me.optVariableAction2 = New DarkUI.Controls.DarkRadioButton()
        Me.btnPlayerVarOk = New DarkUI.Controls.DarkButton()
        Me.btnPlayerVarCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel51 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel50 = New DarkUI.Controls.DarkLabel()
        Me.nudVariableData4 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudVariableData3 = New DarkUI.Controls.DarkNumericUpDown()
        Me.optVariableAction3 = New DarkUI.Controls.DarkRadioButton()
        Me.optVariableAction1 = New DarkUI.Controls.DarkRadioButton()
        Me.nudVariableData1 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudVariableData0 = New DarkUI.Controls.DarkNumericUpDown()
        Me.optVariableAction0 = New DarkUI.Controls.DarkRadioButton()
        Me.cmbVariable = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel49 = New DarkUI.Controls.DarkLabel()
        Me.fraChangeSprite = New DarkUI.Controls.DarkGroupBox()
        Me.btnChangeSpriteOk = New DarkUI.Controls.DarkButton()
        Me.btnChangeSpriteCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel48 = New DarkUI.Controls.DarkLabel()
        Me.nudChangeSprite = New DarkUI.Controls.DarkNumericUpDown()
        Me.picChangeSprite = New System.Windows.Forms.PictureBox()
        Me.fraSetSelfSwitch = New DarkUI.Controls.DarkGroupBox()
        Me.btnSelfswitchOk = New DarkUI.Controls.DarkButton()
        Me.btnSelfswitchCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel47 = New DarkUI.Controls.DarkLabel()
        Me.cmbSetSelfSwitchTo = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel46 = New DarkUI.Controls.DarkLabel()
        Me.cmbSetSelfSwitch = New DarkUI.Controls.DarkComboBox()
        Me.fraMapTint = New DarkUI.Controls.DarkGroupBox()
        Me.btnMapTintOk = New DarkUI.Controls.DarkButton()
        Me.btnMapTintCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel42 = New DarkUI.Controls.DarkLabel()
        Me.nudMapTintData3 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudMapTintData2 = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel43 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel44 = New DarkUI.Controls.DarkLabel()
        Me.nudMapTintData1 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudMapTintData0 = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel45 = New DarkUI.Controls.DarkLabel()
        Me.fraShowChatBubble = New DarkUI.Controls.DarkGroupBox()
        Me.btnShowChatBubbleOk = New DarkUI.Controls.DarkButton()
        Me.btnShowChatBubbleCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel41 = New DarkUI.Controls.DarkLabel()
        Me.cmbChatBubbleTarget = New DarkUI.Controls.DarkComboBox()
        Me.cmbChatBubbleTargetType = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel40 = New DarkUI.Controls.DarkLabel()
        Me.txtChatbubbleText = New DarkUI.Controls.DarkTextBox()
        Me.DarkLabel39 = New DarkUI.Controls.DarkLabel()
        Me.fraPlaySound = New DarkUI.Controls.DarkGroupBox()
        Me.btnPlaySoundOk = New DarkUI.Controls.DarkButton()
        Me.btnPlaySoundCancel = New DarkUI.Controls.DarkButton()
        Me.cmbPlaySound = New DarkUI.Controls.DarkComboBox()
        Me.fraChangePK = New DarkUI.Controls.DarkGroupBox()
        Me.btnChangePkOk = New DarkUI.Controls.DarkButton()
        Me.btnChangePkCancel = New DarkUI.Controls.DarkButton()
        Me.cmbSetPK = New DarkUI.Controls.DarkComboBox()
        Me.fraCreateLabel = New DarkUI.Controls.DarkGroupBox()
        Me.btnCreatelabelOk = New DarkUI.Controls.DarkButton()
        Me.btnCreatelabelCancel = New DarkUI.Controls.DarkButton()
        Me.txtLabelName = New DarkUI.Controls.DarkTextBox()
        Me.lblLabelName = New DarkUI.Controls.DarkLabel()
        Me.fraChangeJob = New DarkUI.Controls.DarkGroupBox()
        Me.btnChangeJobOk = New DarkUI.Controls.DarkButton()
        Me.btnChangeJobCancel = New DarkUI.Controls.DarkButton()
        Me.cmbChangeJob = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel38 = New DarkUI.Controls.DarkLabel()
        Me.fraChangeSkills = New DarkUI.Controls.DarkGroupBox()
        Me.btnChangeSkillsOk = New DarkUI.Controls.DarkButton()
        Me.btnChangeSkillsCancel = New DarkUI.Controls.DarkButton()
        Me.optChangeSkillsRemove = New DarkUI.Controls.DarkRadioButton()
        Me.optChangeSkillsAdd = New DarkUI.Controls.DarkRadioButton()
        Me.cmbChangeSkills = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel37 = New DarkUI.Controls.DarkLabel()
        Me.fraPlayerWarp = New DarkUI.Controls.DarkGroupBox()
        Me.btnPlayerWarpOk = New DarkUI.Controls.DarkButton()
        Me.btnPlayerWarpCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel31 = New DarkUI.Controls.DarkLabel()
        Me.cmbWarpPlayerDir = New DarkUI.Controls.DarkComboBox()
        Me.nudWPY = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel32 = New DarkUI.Controls.DarkLabel()
        Me.nudWPX = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel33 = New DarkUI.Controls.DarkLabel()
        Me.nudWPMap = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel34 = New DarkUI.Controls.DarkLabel()
        Me.fraSetFog = New DarkUI.Controls.DarkGroupBox()
        Me.btnSetFogOk = New DarkUI.Controls.DarkButton()
        Me.btnSetFogCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel30 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel29 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel28 = New DarkUI.Controls.DarkLabel()
        Me.nudFogData2 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudFogData1 = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudFogData0 = New DarkUI.Controls.DarkNumericUpDown()
        Me.fraShowText = New DarkUI.Controls.DarkGroupBox()
        Me.DarkLabel27 = New DarkUI.Controls.DarkLabel()
        Me.txtShowText = New DarkUI.Controls.DarkTextBox()
        Me.btnShowTextCancel = New DarkUI.Controls.DarkButton()
        Me.btnShowTextOk = New DarkUI.Controls.DarkButton()
        Me.picShowTextFace = New System.Windows.Forms.PictureBox()
        Me.DarkLabel26 = New DarkUI.Controls.DarkLabel()
        Me.nudShowTextFace = New DarkUI.Controls.DarkNumericUpDown()
        Me.fraAddText = New DarkUI.Controls.DarkGroupBox()
        Me.btnAddTextOk = New DarkUI.Controls.DarkButton()
        Me.btnAddTextCancel = New DarkUI.Controls.DarkButton()
        Me.optAddText_Global = New DarkUI.Controls.DarkRadioButton()
        Me.optAddText_Map = New DarkUI.Controls.DarkRadioButton()
        Me.optAddText_Player = New DarkUI.Controls.DarkRadioButton()
        Me.DarkLabel25 = New DarkUI.Controls.DarkLabel()
        Me.txtAddText_Text = New DarkUI.Controls.DarkTextBox()
        Me.DarkLabel24 = New DarkUI.Controls.DarkLabel()
        Me.fraPlayerSwitch = New DarkUI.Controls.DarkGroupBox()
        Me.btnSetPlayerSwitchOk = New DarkUI.Controls.DarkButton()
        Me.btnSetPlayerswitchCancel = New DarkUI.Controls.DarkButton()
        Me.cmbPlayerSwitchSet = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel23 = New DarkUI.Controls.DarkLabel()
        Me.cmbSwitch = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel22 = New DarkUI.Controls.DarkLabel()
        Me.fraChangeItems = New DarkUI.Controls.DarkGroupBox()
        Me.btnChangeItemsOk = New DarkUI.Controls.DarkButton()
        Me.btnChangeItemsCancel = New DarkUI.Controls.DarkButton()
        Me.nudChangeItemsAmount = New DarkUI.Controls.DarkNumericUpDown()
        Me.optChangeItemRemove = New DarkUI.Controls.DarkRadioButton()
        Me.optChangeItemAdd = New DarkUI.Controls.DarkRadioButton()
        Me.optChangeItemSet = New DarkUI.Controls.DarkRadioButton()
        Me.cmbChangeItemIndex = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel21 = New DarkUI.Controls.DarkLabel()
        Me.fraPlayBGM = New DarkUI.Controls.DarkGroupBox()
        Me.btnPlayBgmOk = New DarkUI.Controls.DarkButton()
        Me.btnPlayBgmCancel = New DarkUI.Controls.DarkButton()
        Me.cmbPlayBGM = New DarkUI.Controls.DarkComboBox()
        Me.fraConditionalBranch = New DarkUI.Controls.DarkGroupBox()
        Me.cmbCondition_Time = New DarkUI.Controls.DarkComboBox()
        Me.optCondition9 = New DarkUI.Controls.DarkRadioButton()
        Me.btnConditionalBranchOk = New DarkUI.Controls.DarkButton()
        Me.btnConditionalBranchCancel = New DarkUI.Controls.DarkButton()
        Me.cmbCondition_Gender = New DarkUI.Controls.DarkComboBox()
        Me.optCondition8 = New DarkUI.Controls.DarkRadioButton()
        Me.cmbCondition_SelfSwitchCondition = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel17 = New DarkUI.Controls.DarkLabel()
        Me.cmbCondition_SelfSwitch = New DarkUI.Controls.DarkComboBox()
        Me.optCondition6 = New DarkUI.Controls.DarkRadioButton()
        Me.nudCondition_LevelAmount = New DarkUI.Controls.DarkNumericUpDown()
        Me.optCondition5 = New DarkUI.Controls.DarkRadioButton()
        Me.cmbCondition_LevelCompare = New DarkUI.Controls.DarkComboBox()
        Me.cmbCondition_LearntSkill = New DarkUI.Controls.DarkComboBox()
        Me.optCondition4 = New DarkUI.Controls.DarkRadioButton()
        Me.cmbCondition_JobIs = New DarkUI.Controls.DarkComboBox()
        Me.optCondition3 = New DarkUI.Controls.DarkRadioButton()
        Me.nudCondition_HasItem = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel16 = New DarkUI.Controls.DarkLabel()
        Me.cmbCondition_HasItem = New DarkUI.Controls.DarkComboBox()
        Me.optCondition2 = New DarkUI.Controls.DarkRadioButton()
        Me.optCondition1 = New DarkUI.Controls.DarkRadioButton()
        Me.DarkLabel15 = New DarkUI.Controls.DarkLabel()
        Me.cmbCondtion_PlayerSwitchCondition = New DarkUI.Controls.DarkComboBox()
        Me.cmbCondition_PlayerSwitch = New DarkUI.Controls.DarkComboBox()
        Me.nudCondition_PlayerVarCondition = New DarkUI.Controls.DarkNumericUpDown()
        Me.cmbCondition_PlayerVarCompare = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel14 = New DarkUI.Controls.DarkLabel()
        Me.cmbCondition_PlayerVarIndex = New DarkUI.Controls.DarkComboBox()
        Me.optCondition0 = New DarkUI.Controls.DarkRadioButton()
        Me.fraPlayAnimation = New DarkUI.Controls.DarkGroupBox()
        Me.btnPlayAnimationOk = New DarkUI.Controls.DarkButton()
        Me.btnPlayAnimationCancel = New DarkUI.Controls.DarkButton()
        Me.lblPlayAnimY = New DarkUI.Controls.DarkLabel()
        Me.lblPlayAnimX = New DarkUI.Controls.DarkLabel()
        Me.cmbPlayAnimEvent = New DarkUI.Controls.DarkComboBox()
        Me.DarkLabel62 = New DarkUI.Controls.DarkLabel()
        Me.cmbAnimTargetType = New DarkUI.Controls.DarkComboBox()
        Me.nudPlayAnimTileY = New DarkUI.Controls.DarkNumericUpDown()
        Me.nudPlayAnimTileX = New DarkUI.Controls.DarkNumericUpDown()
        Me.DarkLabel61 = New DarkUI.Controls.DarkLabel()
        Me.cmbPlayAnim = New DarkUI.Controls.DarkComboBox()
        Me.fraSetWait = New DarkUI.Controls.DarkGroupBox()
        Me.btnSetWaitOk = New DarkUI.Controls.DarkButton()
        Me.btnSetWaitCancel = New DarkUI.Controls.DarkButton()
        Me.DarkLabel74 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel72 = New DarkUI.Controls.DarkLabel()
        Me.DarkLabel73 = New DarkUI.Controls.DarkLabel()
        Me.nudWaitAmount = New DarkUI.Controls.DarkNumericUpDown()
        Me.pnlVariableSwitches = New System.Windows.Forms.Panel()
        Me.FraRenaming = New System.Windows.Forms.GroupBox()
        Me.btnRename_Cancel = New System.Windows.Forms.Button()
        Me.btnRename_Ok = New System.Windows.Forms.Button()
        Me.fraRandom10 = New System.Windows.Forms.GroupBox()
        Me.txtRename = New System.Windows.Forms.TextBox()
        Me.lblEditing = New System.Windows.Forms.Label()
        Me.fraLabeling = New DarkUI.Controls.DarkGroupBox()
        Me.lstSwitches = New System.Windows.Forms.ListBox()
        Me.lstVariables = New System.Windows.Forms.ListBox()
        Me.btnLabel_Cancel = New System.Windows.Forms.Button()
        Me.lblRandomLabel36 = New System.Windows.Forms.Label()
        Me.btnRenameVariable = New System.Windows.Forms.Button()
        Me.lblRandomLabel25 = New System.Windows.Forms.Label()
        Me.btnRenameSwitch = New System.Windows.Forms.Button()
        Me.btnLabel_Ok = New System.Windows.Forms.Button()
        Me.fraPageSetUp.SuspendLayout()
        Me.tabPages.SuspendLayout()
        Me.pnlTabPage.SuspendLayout()
        Me.DarkGroupBox2.SuspendLayout()
        Me.fraGraphicPic.SuspendLayout()
        CType(Me.picGraphic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DarkGroupBox6.SuspendLayout()
        Me.DarkGroupBox7.SuspendLayout()
        Me.DarkGroupBox5.SuspendLayout()
        Me.DarkGroupBox4.SuspendLayout()
        CType(Me.picGraphicSel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DarkGroupBox3.SuspendLayout()
        Me.DarkGroupBox1.SuspendLayout()
        CType(Me.nudPlayerVariable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraGraphic.SuspendLayout()
        CType(Me.nudGraphic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCommands.SuspendLayout()
        Me.DarkGroupBox8.SuspendLayout()
        Me.fraMoveRoute.SuspendLayout()
        Me.DarkGroupBox10.SuspendLayout()
        Me.fraDialogue.SuspendLayout()
        Me.fraShowPic.SuspendLayout()
        CType(Me.nudPicOffsetY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudPicOffsetX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudShowPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picShowPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraMoveRouteWait.SuspendLayout()
        Me.fraCustomScript.SuspendLayout()
        CType(Me.nudCustomScript, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSetWeather.SuspendLayout()
        CType(Me.nudWeatherIntensity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSpawnNpc.SuspendLayout()
        Me.fraGiveExp.SuspendLayout()
        CType(Me.nudGiveExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraEndQuest.SuspendLayout()
        Me.fraSetAccess.SuspendLayout()
        Me.fraOpenShop.SuspendLayout()
        Me.fraChangeLevel.SuspendLayout()
        CType(Me.nudChangeLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraChangeGender.SuspendLayout()
        Me.fraGoToLabel.SuspendLayout()
        Me.fraShowChoices.SuspendLayout()
        CType(Me.picShowChoicesFace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudShowChoicesFace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPlayerVariable.SuspendLayout()
        CType(Me.nudVariableData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudVariableData4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudVariableData3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudVariableData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudVariableData0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraChangeSprite.SuspendLayout()
        CType(Me.nudChangeSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picChangeSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSetSelfSwitch.SuspendLayout()
        Me.fraMapTint.SuspendLayout()
        CType(Me.nudMapTintData3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMapTintData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMapTintData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMapTintData0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraShowChatBubble.SuspendLayout()
        Me.fraPlaySound.SuspendLayout()
        Me.fraChangePK.SuspendLayout()
        Me.fraCreateLabel.SuspendLayout()
        Me.fraChangeJob.SuspendLayout()
        Me.fraChangeSkills.SuspendLayout()
        Me.fraPlayerWarp.SuspendLayout()
        CType(Me.nudWPY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWPX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWPMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSetFog.SuspendLayout()
        CType(Me.nudFogData2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudFogData1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudFogData0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraShowText.SuspendLayout()
        CType(Me.picShowTextFace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudShowTextFace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAddText.SuspendLayout()
        Me.fraPlayerSwitch.SuspendLayout()
        Me.fraChangeItems.SuspendLayout()
        CType(Me.nudChangeItemsAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPlayBGM.SuspendLayout()
        Me.fraConditionalBranch.SuspendLayout()
        CType(Me.nudCondition_LevelAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCondition_HasItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCondition_PlayerVarCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraPlayAnimation.SuspendLayout()
        CType(Me.nudPlayAnimTileY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudPlayAnimTileX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSetWait.SuspendLayout()
        CType(Me.nudWaitAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlVariableSwitches.SuspendLayout()
        Me.FraRenaming.SuspendLayout()
        Me.fraRandom10.SuspendLayout()
        Me.fraLabeling.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvCommands
        '
        Me.tvCommands.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.tvCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tvCommands.ForeColor = System.Drawing.Color.Gainsboro
        Me.tvCommands.Location = New System.Drawing.Point(7, 3)
        Me.tvCommands.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tvCommands.Name = "tvCommands"
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
        Me.tvCommands.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode5, TreeNode9, TreeNode14, TreeNode26, TreeNode33, TreeNode35, TreeNode39, TreeNode43, TreeNode48, TreeNode52, TreeNode55, TreeNode61})
        Me.tvCommands.Size = New System.Drawing.Size(444, 511)
        Me.tvCommands.TabIndex = 1
        '
        'fraPageSetUp
        '
        Me.fraPageSetUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPageSetUp.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraPageSetUp.Controls.Add(Me.chkGlobal)
        Me.fraPageSetUp.Controls.Add(Me.btnClearPage)
        Me.fraPageSetUp.Controls.Add(Me.btnDeletePage)
        Me.fraPageSetUp.Controls.Add(Me.btnPastePage)
        Me.fraPageSetUp.Controls.Add(Me.btnCopyPage)
        Me.fraPageSetUp.Controls.Add(Me.btnNewPage)
        Me.fraPageSetUp.Controls.Add(Me.txtName)
        Me.fraPageSetUp.Controls.Add(Me.DarkLabel1)
        Me.fraPageSetUp.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPageSetUp.Location = New System.Drawing.Point(4, 3)
        Me.fraPageSetUp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPageSetUp.Name = "fraPageSetUp"
        Me.fraPageSetUp.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPageSetUp.Size = New System.Drawing.Size(923, 58)
        Me.fraPageSetUp.TabIndex = 2
        Me.fraPageSetUp.TabStop = False
        Me.fraPageSetUp.Text = "General"
        '
        'chkGlobal
        '
        Me.chkGlobal.AutoSize = True
        Me.chkGlobal.Location = New System.Drawing.Point(327, 23)
        Me.chkGlobal.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkGlobal.Name = "chkGlobal"
        Me.chkGlobal.Size = New System.Drawing.Size(92, 19)
        Me.chkGlobal.TabIndex = 7
        Me.chkGlobal.Text = "Global Event"
        '
        'btnClearPage
        '
        Me.btnClearPage.Location = New System.Drawing.Point(825, 18)
        Me.btnClearPage.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnClearPage.Name = "btnClearPage"
        Me.btnClearPage.Padding = New System.Windows.Forms.Padding(6)
        Me.btnClearPage.Size = New System.Drawing.Size(88, 27)
        Me.btnClearPage.TabIndex = 6
        Me.btnClearPage.Text = "Clear Page"
        '
        'btnDeletePage
        '
        Me.btnDeletePage.Location = New System.Drawing.Point(726, 18)
        Me.btnDeletePage.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDeletePage.Name = "btnDeletePage"
        Me.btnDeletePage.Padding = New System.Windows.Forms.Padding(6)
        Me.btnDeletePage.Size = New System.Drawing.Size(92, 27)
        Me.btnDeletePage.TabIndex = 5
        Me.btnDeletePage.Text = "Delete Page"
        '
        'btnPastePage
        '
        Me.btnPastePage.Location = New System.Drawing.Point(631, 18)
        Me.btnPastePage.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPastePage.Name = "btnPastePage"
        Me.btnPastePage.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPastePage.Size = New System.Drawing.Size(88, 27)
        Me.btnPastePage.TabIndex = 4
        Me.btnPastePage.Text = "Paste Page"
        '
        'btnCopyPage
        '
        Me.btnCopyPage.Location = New System.Drawing.Point(537, 18)
        Me.btnCopyPage.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCopyPage.Name = "btnCopyPage"
        Me.btnCopyPage.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCopyPage.Size = New System.Drawing.Size(88, 27)
        Me.btnCopyPage.TabIndex = 3
        Me.btnCopyPage.Text = "Copy Page"
        '
        'btnNewPage
        '
        Me.btnNewPage.Location = New System.Drawing.Point(442, 18)
        Me.btnNewPage.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnNewPage.Name = "btnNewPage"
        Me.btnNewPage.Padding = New System.Windows.Forms.Padding(6)
        Me.btnNewPage.Size = New System.Drawing.Size(88, 27)
        Me.btnNewPage.TabIndex = 2
        Me.btnNewPage.Text = "New Page"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(98, 22)
        Me.txtName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(221, 23)
        Me.txtName.TabIndex = 1
        '
        'DarkLabel1
        '
        Me.DarkLabel1.AutoSize = True
        Me.DarkLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel1.Location = New System.Drawing.Point(10, 24)
        Me.DarkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel1.Name = "DarkLabel1"
        Me.DarkLabel1.Size = New System.Drawing.Size(74, 15)
        Me.DarkLabel1.TabIndex = 0
        Me.DarkLabel1.Text = "Event Name:"
        '
        'tabPages
        '
        Me.tabPages.Controls.Add(Me.TabPage1)
        Me.tabPages.Location = New System.Drawing.Point(14, 68)
        Me.tabPages.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabPages.Name = "tabPages"
        Me.tabPages.SelectedIndex = 0
        Me.tabPages.Size = New System.Drawing.Size(827, 22)
        Me.tabPages.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DimGray
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Size = New System.Drawing.Size(819, 0)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'pnlTabPage
        '
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox2)
        Me.pnlTabPage.Controls.Add(Me.fraGraphicPic)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox6)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox7)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox5)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox4)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox3)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox1)
        Me.pnlTabPage.Controls.Add(Me.fraGraphic)
        Me.pnlTabPage.Controls.Add(Me.fraCommands)
        Me.pnlTabPage.Controls.Add(Me.lstCommands)
        Me.pnlTabPage.Controls.Add(Me.DarkGroupBox8)
        Me.pnlTabPage.Location = New System.Drawing.Point(4, 93)
        Me.pnlTabPage.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlTabPage.Name = "pnlTabPage"
        Me.pnlTabPage.Size = New System.Drawing.Size(923, 573)
        Me.pnlTabPage.TabIndex = 4
        '
        'DarkGroupBox2
        '
        Me.DarkGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox2.Controls.Add(Me.cmbPositioning)
        Me.DarkGroupBox2.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox2.Location = New System.Drawing.Point(213, 440)
        Me.DarkGroupBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox2.Name = "DarkGroupBox2"
        Me.DarkGroupBox2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox2.Size = New System.Drawing.Size(233, 57)
        Me.DarkGroupBox2.TabIndex = 15
        Me.DarkGroupBox2.TabStop = False
        Me.DarkGroupBox2.Text = "Poisition"
        '
        'cmbPositioning
        '
        Me.cmbPositioning.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPositioning.FormattingEnabled = True
        Me.cmbPositioning.Items.AddRange(New Object() {"Below Characters", "Same as Characters", "Above Characters"})
        Me.cmbPositioning.Location = New System.Drawing.Point(8, 22)
        Me.cmbPositioning.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPositioning.Name = "cmbPositioning"
        Me.cmbPositioning.Size = New System.Drawing.Size(220, 24)
        Me.cmbPositioning.TabIndex = 1
        '
        'fraGraphicPic
        '
        Me.fraGraphicPic.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraGraphicPic.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraGraphicPic.Controls.Add(Me.picGraphic)
        Me.fraGraphicPic.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraGraphicPic.Location = New System.Drawing.Point(4, 156)
        Me.fraGraphicPic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraGraphicPic.Name = "fraGraphicPic"
        Me.fraGraphicPic.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraGraphicPic.Size = New System.Drawing.Size(202, 268)
        Me.fraGraphicPic.TabIndex = 12
        Me.fraGraphicPic.TabStop = False
        Me.fraGraphicPic.Text = "Graphic"
        '
        'picGraphic
        '
        Me.picGraphic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picGraphic.Location = New System.Drawing.Point(7, 22)
        Me.picGraphic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picGraphic.Name = "picGraphic"
        Me.picGraphic.Size = New System.Drawing.Size(188, 239)
        Me.picGraphic.TabIndex = 1
        Me.picGraphic.TabStop = False
        '
        'DarkGroupBox6
        '
        Me.DarkGroupBox6.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox6.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox6.Controls.Add(Me.chkShowName)
        Me.DarkGroupBox6.Controls.Add(Me.chkWalkThrough)
        Me.DarkGroupBox6.Controls.Add(Me.chkDirFix)
        Me.DarkGroupBox6.Controls.Add(Me.chkWalkAnim)
        Me.DarkGroupBox6.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox6.Location = New System.Drawing.Point(4, 430)
        Me.DarkGroupBox6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox6.Name = "DarkGroupBox6"
        Me.DarkGroupBox6.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox6.Size = New System.Drawing.Size(205, 129)
        Me.DarkGroupBox6.TabIndex = 10
        Me.DarkGroupBox6.TabStop = False
        Me.DarkGroupBox6.Text = "Options"
        '
        'chkShowName
        '
        Me.chkShowName.AutoSize = True
        Me.chkShowName.Location = New System.Drawing.Point(8, 102)
        Me.chkShowName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkShowName.Name = "chkShowName"
        Me.chkShowName.Size = New System.Drawing.Size(90, 19)
        Me.chkShowName.TabIndex = 3
        Me.chkShowName.Text = "Show Name"
        '
        'chkWalkThrough
        '
        Me.chkWalkThrough.AutoSize = True
        Me.chkWalkThrough.Location = New System.Drawing.Point(8, 75)
        Me.chkWalkThrough.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkWalkThrough.Name = "chkWalkThrough"
        Me.chkWalkThrough.Size = New System.Drawing.Size(100, 19)
        Me.chkWalkThrough.TabIndex = 2
        Me.chkWalkThrough.Text = "Walk Through"
        '
        'chkDirFix
        '
        Me.chkDirFix.AutoSize = True
        Me.chkDirFix.Location = New System.Drawing.Point(8, 48)
        Me.chkDirFix.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkDirFix.Name = "chkDirFix"
        Me.chkDirFix.Size = New System.Drawing.Size(105, 19)
        Me.chkDirFix.TabIndex = 1
        Me.chkDirFix.Text = "Direction Fixed"
        '
        'chkWalkAnim
        '
        Me.chkWalkAnim.AutoSize = True
        Me.chkWalkAnim.Location = New System.Drawing.Point(8, 22)
        Me.chkWalkAnim.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkWalkAnim.Name = "chkWalkAnim"
        Me.chkWalkAnim.Size = New System.Drawing.Size(130, 19)
        Me.chkWalkAnim.TabIndex = 0
        Me.chkWalkAnim.Text = "No Walk Animation"
        '
        'DarkLabel8
        '
        Me.DarkLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel8.Location = New System.Drawing.Point(8, 23)
        Me.DarkLabel8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel8.Name = "DarkLabel8"
        Me.DarkLabel8.Size = New System.Drawing.Size(41, 15)
        Me.DarkLabel8.TabIndex = 0
        Me.DarkLabel8.Text = "Quest:"
        '
        'DarkGroupBox5
        '
        Me.DarkGroupBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox5.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox5.Controls.Add(Me.cmbTrigger)
        Me.DarkGroupBox5.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox5.Location = New System.Drawing.Point(217, 374)
        Me.DarkGroupBox5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox5.Name = "DarkGroupBox5"
        Me.DarkGroupBox5.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox5.Size = New System.Drawing.Size(233, 57)
        Me.DarkGroupBox5.TabIndex = 4
        Me.DarkGroupBox5.TabStop = False
        Me.DarkGroupBox5.Text = "Trigger"
        '
        'cmbTrigger
        '
        Me.cmbTrigger.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbTrigger.FormattingEnabled = True
        Me.cmbTrigger.Items.AddRange(New Object() {"Action Button", "Player Touch", "Parallel Process"})
        Me.cmbTrigger.Location = New System.Drawing.Point(7, 22)
        Me.cmbTrigger.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbTrigger.Name = "cmbTrigger"
        Me.cmbTrigger.Size = New System.Drawing.Size(220, 24)
        Me.cmbTrigger.TabIndex = 0
        '
        'DarkGroupBox4
        '
        Me.DarkGroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox4.Controls.Add(Me.picGraphicSel)
        Me.DarkGroupBox4.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox4.Location = New System.Drawing.Point(212, 308)
        Me.DarkGroupBox4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox4.Name = "DarkGroupBox4"
        Me.DarkGroupBox4.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox4.Size = New System.Drawing.Size(233, 55)
        Me.DarkGroupBox4.TabIndex = 3
        Me.DarkGroupBox4.TabStop = False
        Me.DarkGroupBox4.Text = "Positioning"
        '
        'picGraphicSel
        '
        Me.picGraphicSel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picGraphicSel.Location = New System.Drawing.Point(-220, -338)
        Me.picGraphicSel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picGraphicSel.Name = "picGraphicSel"
        Me.picGraphicSel.Size = New System.Drawing.Size(936, 593)
        Me.picGraphicSel.TabIndex = 5
        Me.picGraphicSel.TabStop = False
        '
        'DarkGroupBox3
        '
        Me.DarkGroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel7)
        Me.DarkGroupBox3.Controls.Add(Me.cmbMoveFreq)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel6)
        Me.DarkGroupBox3.Controls.Add(Me.cmbMoveSpeed)
        Me.DarkGroupBox3.Controls.Add(Me.btnMoveRoute)
        Me.DarkGroupBox3.Controls.Add(Me.cmbMoveType)
        Me.DarkGroupBox3.Controls.Add(Me.DarkLabel5)
        Me.DarkGroupBox3.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox3.Location = New System.Drawing.Point(214, 159)
        Me.DarkGroupBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox3.Name = "DarkGroupBox3"
        Me.DarkGroupBox3.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox3.Size = New System.Drawing.Size(233, 142)
        Me.DarkGroupBox3.TabIndex = 2
        Me.DarkGroupBox3.TabStop = False
        Me.DarkGroupBox3.Text = "Movement"
        '
        'DarkLabel7
        '
        Me.DarkLabel7.AutoSize = True
        Me.DarkLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel7.Location = New System.Drawing.Point(7, 115)
        Me.DarkLabel7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel7.Name = "DarkLabel7"
        Me.DarkLabel7.Size = New System.Drawing.Size(62, 15)
        Me.DarkLabel7.TabIndex = 6
        Me.DarkLabel7.Text = "Frequency"
        '
        'cmbMoveFreq
        '
        Me.cmbMoveFreq.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoveFreq.FormattingEnabled = True
        Me.cmbMoveFreq.Items.AddRange(New Object() {"Lowest", "Lower", "Normal", "Higher", "Highest"})
        Me.cmbMoveFreq.Location = New System.Drawing.Point(80, 112)
        Me.cmbMoveFreq.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbMoveFreq.Name = "cmbMoveFreq"
        Me.cmbMoveFreq.Size = New System.Drawing.Size(145, 24)
        Me.cmbMoveFreq.TabIndex = 5
        '
        'DarkLabel6
        '
        Me.DarkLabel6.AutoSize = True
        Me.DarkLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel6.Location = New System.Drawing.Point(7, 84)
        Me.DarkLabel6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel6.Name = "DarkLabel6"
        Me.DarkLabel6.Size = New System.Drawing.Size(42, 15)
        Me.DarkLabel6.TabIndex = 4
        Me.DarkLabel6.Text = "Speed:"
        '
        'cmbMoveSpeed
        '
        Me.cmbMoveSpeed.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoveSpeed.FormattingEnabled = True
        Me.cmbMoveSpeed.Items.AddRange(New Object() {"8x Slower", "4x Slower", "2x Slower", "Normal", "2x Faster", "4x Faster"})
        Me.cmbMoveSpeed.Location = New System.Drawing.Point(80, 81)
        Me.cmbMoveSpeed.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbMoveSpeed.Name = "cmbMoveSpeed"
        Me.cmbMoveSpeed.Size = New System.Drawing.Size(145, 24)
        Me.cmbMoveSpeed.TabIndex = 3
        '
        'btnMoveRoute
        '
        Me.btnMoveRoute.Location = New System.Drawing.Point(139, 47)
        Me.btnMoveRoute.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMoveRoute.Name = "btnMoveRoute"
        Me.btnMoveRoute.Padding = New System.Windows.Forms.Padding(6)
        Me.btnMoveRoute.Size = New System.Drawing.Size(88, 27)
        Me.btnMoveRoute.TabIndex = 2
        Me.btnMoveRoute.Text = "Move Route"
        '
        'cmbMoveType
        '
        Me.cmbMoveType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoveType.FormattingEnabled = True
        Me.cmbMoveType.Items.AddRange(New Object() {"Fixed Position", "Random", "Move Route"})
        Me.cmbMoveType.Location = New System.Drawing.Point(80, 16)
        Me.cmbMoveType.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbMoveType.Name = "cmbMoveType"
        Me.cmbMoveType.Size = New System.Drawing.Size(145, 24)
        Me.cmbMoveType.TabIndex = 1
        '
        'DarkLabel5
        '
        Me.DarkLabel5.AutoSize = True
        Me.DarkLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel5.Location = New System.Drawing.Point(7, 20)
        Me.DarkLabel5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel5.Name = "DarkLabel5"
        Me.DarkLabel5.Size = New System.Drawing.Size(34, 15)
        Me.DarkLabel5.TabIndex = 0
        Me.DarkLabel5.Text = "Type:"
        '
        'DarkGroupBox1
        '
        Me.DarkGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox1.Controls.Add(Me.cmbSelfSwitchCompare)
        Me.DarkGroupBox1.Controls.Add(Me.DarkLabel4)
        Me.DarkGroupBox1.Controls.Add(Me.cmbSelfSwitch)
        Me.DarkGroupBox1.Controls.Add(Me.chkSelfSwitch)
        Me.DarkGroupBox1.Controls.Add(Me.cmbHasItem)
        Me.DarkGroupBox1.Controls.Add(Me.chkHasItem)
        Me.DarkGroupBox1.Controls.Add(Me.cmbPlayerSwitchCompare)
        Me.DarkGroupBox1.Controls.Add(Me.DarkLabel3)
        Me.DarkGroupBox1.Controls.Add(Me.cmbPlayerSwitch)
        Me.DarkGroupBox1.Controls.Add(Me.chkPlayerSwitch)
        Me.DarkGroupBox1.Controls.Add(Me.nudPlayerVariable)
        Me.DarkGroupBox1.Controls.Add(Me.cmbPlayervarCompare)
        Me.DarkGroupBox1.Controls.Add(Me.DarkLabel2)
        Me.DarkGroupBox1.Controls.Add(Me.cmbPlayerVar)
        Me.DarkGroupBox1.Controls.Add(Me.chkPlayerVar)
        Me.DarkGroupBox1.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox1.Location = New System.Drawing.Point(4, 7)
        Me.DarkGroupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox1.Name = "DarkGroupBox1"
        Me.DarkGroupBox1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox1.Size = New System.Drawing.Size(443, 145)
        Me.DarkGroupBox1.TabIndex = 0
        Me.DarkGroupBox1.TabStop = False
        Me.DarkGroupBox1.Text = "Conditions"
        '
        'cmbSelfSwitchCompare
        '
        Me.cmbSelfSwitchCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSelfSwitchCompare.FormattingEnabled = True
        Me.cmbSelfSwitchCompare.Items.AddRange(New Object() {"False = 0", "True = 1"})
        Me.cmbSelfSwitchCompare.Location = New System.Drawing.Point(260, 113)
        Me.cmbSelfSwitchCompare.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbSelfSwitchCompare.Name = "cmbSelfSwitchCompare"
        Me.cmbSelfSwitchCompare.Size = New System.Drawing.Size(103, 24)
        Me.cmbSelfSwitchCompare.TabIndex = 14
        '
        'DarkLabel4
        '
        Me.DarkLabel4.AutoSize = True
        Me.DarkLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel4.Location = New System.Drawing.Point(237, 117)
        Me.DarkLabel4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel4.Name = "DarkLabel4"
        Me.DarkLabel4.Size = New System.Drawing.Size(15, 15)
        Me.DarkLabel4.TabIndex = 13
        Me.DarkLabel4.Text = "is"
        '
        'cmbSelfSwitch
        '
        Me.cmbSelfSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSelfSwitch.FormattingEnabled = True
        Me.cmbSelfSwitch.Items.AddRange(New Object() {"None", "1 - A", "2 - B", "3 - C", "4 - D"})
        Me.cmbSelfSwitch.Location = New System.Drawing.Point(126, 113)
        Me.cmbSelfSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbSelfSwitch.Name = "cmbSelfSwitch"
        Me.cmbSelfSwitch.Size = New System.Drawing.Size(103, 24)
        Me.cmbSelfSwitch.TabIndex = 12
        '
        'chkSelfSwitch
        '
        Me.chkSelfSwitch.AutoSize = True
        Me.chkSelfSwitch.Location = New System.Drawing.Point(7, 115)
        Me.chkSelfSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkSelfSwitch.Name = "chkSelfSwitch"
        Me.chkSelfSwitch.Size = New System.Drawing.Size(83, 19)
        Me.chkSelfSwitch.TabIndex = 11
        Me.chkSelfSwitch.Text = "Self Switch"
        '
        'cmbHasItem
        '
        Me.cmbHasItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbHasItem.FormattingEnabled = True
        Me.cmbHasItem.Location = New System.Drawing.Point(126, 82)
        Me.cmbHasItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbHasItem.Name = "cmbHasItem"
        Me.cmbHasItem.Size = New System.Drawing.Size(237, 24)
        Me.cmbHasItem.TabIndex = 10
        '
        'chkHasItem
        '
        Me.chkHasItem.AutoSize = True
        Me.chkHasItem.Location = New System.Drawing.Point(7, 84)
        Me.chkHasItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkHasItem.Name = "chkHasItem"
        Me.chkHasItem.Size = New System.Drawing.Size(108, 19)
        Me.chkHasItem.TabIndex = 9
        Me.chkHasItem.Text = "Player Has Item"
        '
        'cmbPlayerSwitchCompare
        '
        Me.cmbPlayerSwitchCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayerSwitchCompare.FormattingEnabled = True
        Me.cmbPlayerSwitchCompare.Items.AddRange(New Object() {"False = 0", "True = 1"})
        Me.cmbPlayerSwitchCompare.Location = New System.Drawing.Point(260, 51)
        Me.cmbPlayerSwitchCompare.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPlayerSwitchCompare.Name = "cmbPlayerSwitchCompare"
        Me.cmbPlayerSwitchCompare.Size = New System.Drawing.Size(103, 24)
        Me.cmbPlayerSwitchCompare.TabIndex = 8
        '
        'DarkLabel3
        '
        Me.DarkLabel3.AutoSize = True
        Me.DarkLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel3.Location = New System.Drawing.Point(237, 54)
        Me.DarkLabel3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel3.Name = "DarkLabel3"
        Me.DarkLabel3.Size = New System.Drawing.Size(15, 15)
        Me.DarkLabel3.TabIndex = 7
        Me.DarkLabel3.Text = "is"
        '
        'cmbPlayerSwitch
        '
        Me.cmbPlayerSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayerSwitch.FormattingEnabled = True
        Me.cmbPlayerSwitch.Location = New System.Drawing.Point(126, 51)
        Me.cmbPlayerSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPlayerSwitch.Name = "cmbPlayerSwitch"
        Me.cmbPlayerSwitch.Size = New System.Drawing.Size(103, 24)
        Me.cmbPlayerSwitch.TabIndex = 6
        '
        'chkPlayerSwitch
        '
        Me.chkPlayerSwitch.AutoSize = True
        Me.chkPlayerSwitch.Location = New System.Drawing.Point(7, 53)
        Me.chkPlayerSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkPlayerSwitch.Name = "chkPlayerSwitch"
        Me.chkPlayerSwitch.Size = New System.Drawing.Size(96, 19)
        Me.chkPlayerSwitch.TabIndex = 5
        Me.chkPlayerSwitch.Text = "Player Switch"
        '
        'nudPlayerVariable
        '
        Me.nudPlayerVariable.Location = New System.Drawing.Point(371, 21)
        Me.nudPlayerVariable.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudPlayerVariable.Name = "nudPlayerVariable"
        Me.nudPlayerVariable.Size = New System.Drawing.Size(65, 23)
        Me.nudPlayerVariable.TabIndex = 4
        '
        'cmbPlayervarCompare
        '
        Me.cmbPlayervarCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayervarCompare.FormattingEnabled = True
        Me.cmbPlayervarCompare.Items.AddRange(New Object() {"Equal To", "Great Than OrElse Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal"})
        Me.cmbPlayervarCompare.Location = New System.Drawing.Point(260, 20)
        Me.cmbPlayervarCompare.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPlayervarCompare.Name = "cmbPlayervarCompare"
        Me.cmbPlayervarCompare.Size = New System.Drawing.Size(103, 24)
        Me.cmbPlayervarCompare.TabIndex = 3
        '
        'DarkLabel2
        '
        Me.DarkLabel2.AutoSize = True
        Me.DarkLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel2.Location = New System.Drawing.Point(237, 27)
        Me.DarkLabel2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel2.Name = "DarkLabel2"
        Me.DarkLabel2.Size = New System.Drawing.Size(15, 15)
        Me.DarkLabel2.TabIndex = 2
        Me.DarkLabel2.Text = "is"
        '
        'cmbPlayerVar
        '
        Me.cmbPlayerVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayerVar.FormattingEnabled = True
        Me.cmbPlayerVar.Location = New System.Drawing.Point(126, 20)
        Me.cmbPlayerVar.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPlayerVar.Name = "cmbPlayerVar"
        Me.cmbPlayerVar.Size = New System.Drawing.Size(103, 24)
        Me.cmbPlayerVar.TabIndex = 1
        '
        'chkPlayerVar
        '
        Me.chkPlayerVar.AutoSize = True
        Me.chkPlayerVar.Location = New System.Drawing.Point(7, 22)
        Me.chkPlayerVar.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkPlayerVar.Name = "chkPlayerVar"
        Me.chkPlayerVar.Size = New System.Drawing.Size(102, 19)
        Me.chkPlayerVar.TabIndex = 0
        Me.chkPlayerVar.Text = "Player Variable"
        '
        'fraGraphic
        '
        Me.fraGraphic.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraGraphic.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraGraphic.Controls.Add(Me.btnGraphicOk)
        Me.fraGraphic.Controls.Add(Me.btnGraphicCancel)
        Me.fraGraphic.Controls.Add(Me.DarkLabel13)
        Me.fraGraphic.Controls.Add(Me.nudGraphic)
        Me.fraGraphic.Controls.Add(Me.DarkLabel12)
        Me.fraGraphic.Controls.Add(Me.cmbGraphic)
        Me.fraGraphic.Controls.Add(Me.DarkLabel11)
        Me.fraGraphic.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraGraphic.Location = New System.Drawing.Point(454, 6)
        Me.fraGraphic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraGraphic.Name = "fraGraphic"
        Me.fraGraphic.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraGraphic.Size = New System.Drawing.Size(459, 557)
        Me.fraGraphic.TabIndex = 14
        Me.fraGraphic.TabStop = False
        Me.fraGraphic.Text = "Graphic Selection"
        Me.fraGraphic.Visible = False
        '
        'btnGraphicOk
        '
        Me.btnGraphicOk.Location = New System.Drawing.Point(761, 658)
        Me.btnGraphicOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnGraphicOk.Name = "btnGraphicOk"
        Me.btnGraphicOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnGraphicOk.Size = New System.Drawing.Size(88, 27)
        Me.btnGraphicOk.TabIndex = 8
        Me.btnGraphicOk.Text = "Ok"
        '
        'btnGraphicCancel
        '
        Me.btnGraphicCancel.Location = New System.Drawing.Point(855, 658)
        Me.btnGraphicCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnGraphicCancel.Name = "btnGraphicCancel"
        Me.btnGraphicCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnGraphicCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnGraphicCancel.TabIndex = 7
        Me.btnGraphicCancel.Text = "Cancel"
        '
        'DarkLabel13
        '
        Me.DarkLabel13.AutoSize = True
        Me.DarkLabel13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel13.Location = New System.Drawing.Point(12, 659)
        Me.DarkLabel13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel13.Name = "DarkLabel13"
        Me.DarkLabel13.Size = New System.Drawing.Size(181, 15)
        Me.DarkLabel13.TabIndex = 6
        Me.DarkLabel13.Text = "Hold Shift to select multiple tiles."
        '
        'nudGraphic
        '
        Me.nudGraphic.Location = New System.Drawing.Point(121, 57)
        Me.nudGraphic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudGraphic.Name = "nudGraphic"
        Me.nudGraphic.Size = New System.Drawing.Size(252, 23)
        Me.nudGraphic.TabIndex = 3
        '
        'DarkLabel12
        '
        Me.DarkLabel12.AutoSize = True
        Me.DarkLabel12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel12.Location = New System.Drawing.Point(24, 59)
        Me.DarkLabel12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel12.Name = "DarkLabel12"
        Me.DarkLabel12.Size = New System.Drawing.Size(54, 15)
        Me.DarkLabel12.TabIndex = 2
        Me.DarkLabel12.Text = "Number:"
        '
        'cmbGraphic
        '
        Me.cmbGraphic.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbGraphic.FormattingEnabled = True
        Me.cmbGraphic.Items.AddRange(New Object() {"None", "Character", "Tileset"})
        Me.cmbGraphic.Location = New System.Drawing.Point(121, 21)
        Me.cmbGraphic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbGraphic.Name = "cmbGraphic"
        Me.cmbGraphic.Size = New System.Drawing.Size(252, 24)
        Me.cmbGraphic.TabIndex = 1
        '
        'DarkLabel11
        '
        Me.DarkLabel11.AutoSize = True
        Me.DarkLabel11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel11.Location = New System.Drawing.Point(24, 24)
        Me.DarkLabel11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel11.Name = "DarkLabel11"
        Me.DarkLabel11.Size = New System.Drawing.Size(83, 15)
        Me.DarkLabel11.TabIndex = 0
        Me.DarkLabel11.Text = "Graphics Type:"
        '
        'fraCommands
        '
        Me.fraCommands.Controls.Add(Me.btnCancelCommand)
        Me.fraCommands.Controls.Add(Me.tvCommands)
        Me.fraCommands.Location = New System.Drawing.Point(454, 7)
        Me.fraCommands.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraCommands.Name = "fraCommands"
        Me.fraCommands.Size = New System.Drawing.Size(458, 556)
        Me.fraCommands.TabIndex = 6
        Me.fraCommands.Visible = False
        '
        'btnCancelCommand
        '
        Me.btnCancelCommand.Location = New System.Drawing.Point(364, 522)
        Me.btnCancelCommand.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCancelCommand.Name = "btnCancelCommand"
        Me.btnCancelCommand.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCancelCommand.Size = New System.Drawing.Size(88, 27)
        Me.btnCancelCommand.TabIndex = 2
        Me.btnCancelCommand.Text = "Cancel"
        '
        'lstCommands
        '
        Me.lstCommands.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstCommands.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstCommands.FormattingEnabled = True
        Me.lstCommands.ItemHeight = 15
        Me.lstCommands.Location = New System.Drawing.Point(454, 7)
        Me.lstCommands.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstCommands.Name = "lstCommands"
        Me.lstCommands.Size = New System.Drawing.Size(458, 497)
        Me.lstCommands.TabIndex = 8
        '
        'DarkGroupBox8
        '
        Me.DarkGroupBox8.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox8.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox8.Controls.Add(Me.btnClearCommand)
        Me.DarkGroupBox8.Controls.Add(Me.btnDeleteCommand)
        Me.DarkGroupBox8.Controls.Add(Me.btnEditCommand)
        Me.DarkGroupBox8.Controls.Add(Me.btnAddCommand)
        Me.DarkGroupBox8.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox8.Location = New System.Drawing.Point(454, 507)
        Me.DarkGroupBox8.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox8.Name = "DarkGroupBox8"
        Me.DarkGroupBox8.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox8.Size = New System.Drawing.Size(458, 57)
        Me.DarkGroupBox8.TabIndex = 9
        Me.DarkGroupBox8.TabStop = False
        Me.DarkGroupBox8.Text = "Commands"
        '
        'btnClearCommand
        '
        Me.btnClearCommand.Location = New System.Drawing.Point(364, 22)
        Me.btnClearCommand.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnClearCommand.Name = "btnClearCommand"
        Me.btnClearCommand.Padding = New System.Windows.Forms.Padding(6)
        Me.btnClearCommand.Size = New System.Drawing.Size(88, 27)
        Me.btnClearCommand.TabIndex = 3
        Me.btnClearCommand.Text = "Clear"
        '
        'btnDeleteCommand
        '
        Me.btnDeleteCommand.Location = New System.Drawing.Point(247, 22)
        Me.btnDeleteCommand.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnDeleteCommand.Name = "btnDeleteCommand"
        Me.btnDeleteCommand.Padding = New System.Windows.Forms.Padding(6)
        Me.btnDeleteCommand.Size = New System.Drawing.Size(88, 27)
        Me.btnDeleteCommand.TabIndex = 2
        Me.btnDeleteCommand.Text = "Delete"
        '
        'btnEditCommand
        '
        Me.btnEditCommand.Location = New System.Drawing.Point(126, 22)
        Me.btnEditCommand.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnEditCommand.Name = "btnEditCommand"
        Me.btnEditCommand.Padding = New System.Windows.Forms.Padding(6)
        Me.btnEditCommand.Size = New System.Drawing.Size(88, 27)
        Me.btnEditCommand.TabIndex = 1
        Me.btnEditCommand.Text = "Edit"
        '
        'btnAddCommand
        '
        Me.btnAddCommand.Location = New System.Drawing.Point(7, 22)
        Me.btnAddCommand.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAddCommand.Name = "btnAddCommand"
        Me.btnAddCommand.Padding = New System.Windows.Forms.Padding(6)
        Me.btnAddCommand.Size = New System.Drawing.Size(88, 27)
        Me.btnAddCommand.TabIndex = 0
        Me.btnAddCommand.Text = "Add"
        '
        'btnLabeling
        '
        Me.btnLabeling.Location = New System.Drawing.Point(13, 669)
        Me.btnLabeling.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnLabeling.Name = "btnLabeling"
        Me.btnLabeling.Padding = New System.Windows.Forms.Padding(6)
        Me.btnLabeling.Size = New System.Drawing.Size(205, 27)
        Me.btnLabeling.TabIndex = 6
        Me.btnLabeling.Text = "Edit Variables/Switches"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(829, 669)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(733, 669)
        Me.btnOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnOk.Size = New System.Drawing.Size(88, 27)
        Me.btnOk.TabIndex = 8
        Me.btnOk.Text = "Save"
        '
        'fraMoveRoute
        '
        Me.fraMoveRoute.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraMoveRoute.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraMoveRoute.Controls.Add(Me.btnMoveRouteOk)
        Me.fraMoveRoute.Controls.Add(Me.btnMoveRouteCancel)
        Me.fraMoveRoute.Controls.Add(Me.chkRepeatRoute)
        Me.fraMoveRoute.Controls.Add(Me.chkIgnoreMove)
        Me.fraMoveRoute.Controls.Add(Me.DarkGroupBox10)
        Me.fraMoveRoute.Controls.Add(Me.lstMoveRoute)
        Me.fraMoveRoute.Controls.Add(Me.cmbEvent)
        Me.fraMoveRoute.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraMoveRoute.Location = New System.Drawing.Point(933, 14)
        Me.fraMoveRoute.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMoveRoute.Name = "fraMoveRoute"
        Me.fraMoveRoute.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMoveRoute.Size = New System.Drawing.Size(108, 98)
        Me.fraMoveRoute.TabIndex = 0
        Me.fraMoveRoute.TabStop = False
        Me.fraMoveRoute.Text = "Move Route"
        Me.fraMoveRoute.Visible = False
        '
        'btnMoveRouteOk
        '
        Me.btnMoveRouteOk.Location = New System.Drawing.Point(749, 497)
        Me.btnMoveRouteOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMoveRouteOk.Name = "btnMoveRouteOk"
        Me.btnMoveRouteOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnMoveRouteOk.Size = New System.Drawing.Size(88, 27)
        Me.btnMoveRouteOk.TabIndex = 7
        Me.btnMoveRouteOk.Text = "Ok"
        '
        'btnMoveRouteCancel
        '
        Me.btnMoveRouteCancel.Location = New System.Drawing.Point(844, 497)
        Me.btnMoveRouteCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMoveRouteCancel.Name = "btnMoveRouteCancel"
        Me.btnMoveRouteCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnMoveRouteCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnMoveRouteCancel.TabIndex = 6
        Me.btnMoveRouteCancel.Text = "Cancel"
        '
        'chkRepeatRoute
        '
        Me.chkRepeatRoute.AutoSize = True
        Me.chkRepeatRoute.Location = New System.Drawing.Point(7, 524)
        Me.chkRepeatRoute.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkRepeatRoute.Name = "chkRepeatRoute"
        Me.chkRepeatRoute.Size = New System.Drawing.Size(96, 19)
        Me.chkRepeatRoute.TabIndex = 5
        Me.chkRepeatRoute.Text = "Repeat Route"
        '
        'chkIgnoreMove
        '
        Me.chkIgnoreMove.AutoSize = True
        Me.chkIgnoreMove.Location = New System.Drawing.Point(7, 497)
        Me.chkIgnoreMove.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkIgnoreMove.Name = "chkIgnoreMove"
        Me.chkIgnoreMove.Size = New System.Drawing.Size(164, 19)
        Me.chkIgnoreMove.TabIndex = 4
        Me.chkIgnoreMove.Text = "Ignore if event can't move"
        '
        'DarkGroupBox10
        '
        Me.DarkGroupBox10.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DarkGroupBox10.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DarkGroupBox10.Controls.Add(Me.lstvwMoveRoute)
        Me.DarkGroupBox10.ForeColor = System.Drawing.Color.Gainsboro
        Me.DarkGroupBox10.Location = New System.Drawing.Point(237, 12)
        Me.DarkGroupBox10.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox10.Name = "DarkGroupBox10"
        Me.DarkGroupBox10.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DarkGroupBox10.Size = New System.Drawing.Size(694, 479)
        Me.DarkGroupBox10.TabIndex = 3
        Me.DarkGroupBox10.TabStop = False
        Me.DarkGroupBox10.Text = "Commands"
        '
        'lstvwMoveRoute
        '
        Me.lstvwMoveRoute.AutoArrange = False
        Me.lstvwMoveRoute.BackColor = System.Drawing.Color.DimGray
        Me.lstvwMoveRoute.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstvwMoveRoute.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lstvwMoveRoute.Dock = System.Windows.Forms.DockStyle.Top
        Me.lstvwMoveRoute.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lstvwMoveRoute.ForeColor = System.Drawing.Color.Gainsboro
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
        Me.lstvwMoveRoute.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3, ListViewGroup4, ListViewGroup5, ListViewGroup6, ListViewGroup7, ListViewGroup8, ListViewGroup9})
        Me.lstvwMoveRoute.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
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
        Me.lstvwMoveRoute.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10, ListViewItem11, ListViewItem12, ListViewItem13, ListViewItem14, ListViewItem15, ListViewItem16, ListViewItem17, ListViewItem18, ListViewItem19, ListViewItem20, ListViewItem21, ListViewItem22, ListViewItem23, ListViewItem24, ListViewItem25, ListViewItem26, ListViewItem27, ListViewItem28, ListViewItem29, ListViewItem30, ListViewItem31, ListViewItem32, ListViewItem33, ListViewItem34, ListViewItem35, ListViewItem36, ListViewItem37, ListViewItem38, ListViewItem39, ListViewItem40, ListViewItem41, ListViewItem42, ListViewItem43})
        Me.lstvwMoveRoute.LabelWrap = False
        Me.lstvwMoveRoute.Location = New System.Drawing.Point(4, 19)
        Me.lstvwMoveRoute.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstvwMoveRoute.MultiSelect = False
        Me.lstvwMoveRoute.Name = "lstvwMoveRoute"
        Me.lstvwMoveRoute.Size = New System.Drawing.Size(686, 458)
        Me.lstvwMoveRoute.TabIndex = 5
        Me.lstvwMoveRoute.UseCompatibleStateImageBehavior = False
        Me.lstvwMoveRoute.View = System.Windows.Forms.View.Tile
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = ""
        Me.ColumnHeader3.Width = 150
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = ""
        Me.ColumnHeader4.Width = 150
        '
        'lstMoveRoute
        '
        Me.lstMoveRoute.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstMoveRoute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstMoveRoute.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstMoveRoute.FormattingEnabled = True
        Me.lstMoveRoute.ItemHeight = 15
        Me.lstMoveRoute.Location = New System.Drawing.Point(7, 53)
        Me.lstMoveRoute.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstMoveRoute.Name = "lstMoveRoute"
        Me.lstMoveRoute.Size = New System.Drawing.Size(222, 437)
        Me.lstMoveRoute.TabIndex = 2
        '
        'cmbEvent
        '
        Me.cmbEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbEvent.FormattingEnabled = True
        Me.cmbEvent.Location = New System.Drawing.Point(7, 22)
        Me.cmbEvent.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbEvent.Name = "cmbEvent"
        Me.cmbEvent.Size = New System.Drawing.Size(222, 24)
        Me.cmbEvent.TabIndex = 0
        '
        'pnlGraphicSel
        '
        Me.pnlGraphicSel.AutoScroll = True
        Me.pnlGraphicSel.Location = New System.Drawing.Point(4, 92)
        Me.pnlGraphicSel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlGraphicSel.Name = "pnlGraphicSel"
        Me.pnlGraphicSel.Size = New System.Drawing.Size(923, 573)
        Me.pnlGraphicSel.TabIndex = 9
        '
        'fraDialogue
        '
        Me.fraDialogue.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraDialogue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraDialogue.Controls.Add(Me.fraConditionalBranch)
        Me.fraDialogue.Controls.Add(Me.fraMoveRouteWait)
        Me.fraDialogue.Controls.Add(Me.fraCustomScript)
        Me.fraDialogue.Controls.Add(Me.fraSetWeather)
        Me.fraDialogue.Controls.Add(Me.fraSpawnNpc)
        Me.fraDialogue.Controls.Add(Me.fraGiveExp)
        Me.fraDialogue.Controls.Add(Me.fraEndQuest)
        Me.fraDialogue.Controls.Add(Me.fraSetAccess)
        Me.fraDialogue.Controls.Add(Me.fraOpenShop)
        Me.fraDialogue.Controls.Add(Me.fraChangeLevel)
        Me.fraDialogue.Controls.Add(Me.fraChangeGender)
        Me.fraDialogue.Controls.Add(Me.fraGoToLabel)
        Me.fraDialogue.Controls.Add(Me.fraShowChoices)
        Me.fraDialogue.Controls.Add(Me.fraPlayerVariable)
        Me.fraDialogue.Controls.Add(Me.fraChangeSprite)
        Me.fraDialogue.Controls.Add(Me.fraSetSelfSwitch)
        Me.fraDialogue.Controls.Add(Me.fraMapTint)
        Me.fraDialogue.Controls.Add(Me.fraShowChatBubble)
        Me.fraDialogue.Controls.Add(Me.fraPlaySound)
        Me.fraDialogue.Controls.Add(Me.fraChangePK)
        Me.fraDialogue.Controls.Add(Me.fraCreateLabel)
        Me.fraDialogue.Controls.Add(Me.fraChangeJob)
        Me.fraDialogue.Controls.Add(Me.fraChangeSkills)
        Me.fraDialogue.Controls.Add(Me.fraPlayerWarp)
        Me.fraDialogue.Controls.Add(Me.fraSetFog)
        Me.fraDialogue.Controls.Add(Me.fraAddText)
        Me.fraDialogue.Controls.Add(Me.fraPlayerSwitch)
        Me.fraDialogue.Controls.Add(Me.fraChangeItems)
        Me.fraDialogue.Controls.Add(Me.fraPlayBGM)
        Me.fraDialogue.Controls.Add(Me.fraPlayAnimation)
        Me.fraDialogue.Controls.Add(Me.fraSetWait)
        Me.fraDialogue.Controls.Add(Me.fraShowPic)
        Me.fraDialogue.Controls.Add(Me.fraShowText)
        Me.fraDialogue.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraDialogue.Location = New System.Drawing.Point(1056, 14)
        Me.fraDialogue.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraDialogue.Name = "fraDialogue"
        Me.fraDialogue.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraDialogue.Size = New System.Drawing.Size(776, 687)
        Me.fraDialogue.TabIndex = 10
        Me.fraDialogue.TabStop = False
        Me.fraDialogue.Visible = False
        '
        'fraShowPic
        '
        Me.fraShowPic.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraShowPic.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraShowPic.Controls.Add(Me.btnShowPicOk)
        Me.fraShowPic.Controls.Add(Me.btnShowPicCancel)
        Me.fraShowPic.Controls.Add(Me.DarkLabel71)
        Me.fraShowPic.Controls.Add(Me.DarkLabel70)
        Me.fraShowPic.Controls.Add(Me.DarkLabel67)
        Me.fraShowPic.Controls.Add(Me.DarkLabel68)
        Me.fraShowPic.Controls.Add(Me.nudPicOffsetY)
        Me.fraShowPic.Controls.Add(Me.nudPicOffsetX)
        Me.fraShowPic.Controls.Add(Me.DarkLabel69)
        Me.fraShowPic.Controls.Add(Me.cmbPicLoc)
        Me.fraShowPic.Controls.Add(Me.nudShowPicture)
        Me.fraShowPic.Controls.Add(Me.picShowPic)
        Me.fraShowPic.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraShowPic.Location = New System.Drawing.Point(1, 1)
        Me.fraShowPic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShowPic.Name = "fraShowPic"
        Me.fraShowPic.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShowPic.Size = New System.Drawing.Size(775, 686)
        Me.fraShowPic.TabIndex = 40
        Me.fraShowPic.TabStop = False
        Me.fraShowPic.Text = "Show Picture"
        Me.fraShowPic.Visible = False
        '
        'btnShowPicOk
        '
        Me.btnShowPicOk.Location = New System.Drawing.Point(583, 651)
        Me.btnShowPicOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShowPicOk.Name = "btnShowPicOk"
        Me.btnShowPicOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnShowPicOk.Size = New System.Drawing.Size(88, 27)
        Me.btnShowPicOk.TabIndex = 55
        Me.btnShowPicOk.Text = "Ok"
        '
        'btnShowPicCancel
        '
        Me.btnShowPicCancel.Location = New System.Drawing.Point(679, 651)
        Me.btnShowPicCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShowPicCancel.Name = "btnShowPicCancel"
        Me.btnShowPicCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnShowPicCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnShowPicCancel.TabIndex = 54
        Me.btnShowPicCancel.Text = "Cancel"
        '
        'DarkLabel71
        '
        Me.DarkLabel71.AutoSize = True
        Me.DarkLabel71.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel71.Location = New System.Drawing.Point(287, 27)
        Me.DarkLabel71.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel71.Name = "DarkLabel71"
        Me.DarkLabel71.Size = New System.Drawing.Size(120, 15)
        Me.DarkLabel71.TabIndex = 53
        Me.DarkLabel71.Text = "Offset from Location:"
        '
        'DarkLabel70
        '
        Me.DarkLabel70.AutoSize = True
        Me.DarkLabel70.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel70.Location = New System.Drawing.Point(130, 65)
        Me.DarkLabel70.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel70.Name = "DarkLabel70"
        Me.DarkLabel70.Size = New System.Drawing.Size(53, 15)
        Me.DarkLabel70.TabIndex = 52
        Me.DarkLabel70.Text = "Location"
        '
        'DarkLabel67
        '
        Me.DarkLabel67.AutoSize = True
        Me.DarkLabel67.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel67.Location = New System.Drawing.Point(435, 54)
        Me.DarkLabel67.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel67.Name = "DarkLabel67"
        Me.DarkLabel67.Size = New System.Drawing.Size(17, 15)
        Me.DarkLabel67.TabIndex = 51
        Me.DarkLabel67.Text = "Y:"
        '
        'DarkLabel68
        '
        Me.DarkLabel68.AutoSize = True
        Me.DarkLabel68.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel68.Location = New System.Drawing.Point(287, 56)
        Me.DarkLabel68.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel68.Name = "DarkLabel68"
        Me.DarkLabel68.Size = New System.Drawing.Size(17, 15)
        Me.DarkLabel68.TabIndex = 50
        Me.DarkLabel68.Text = "X:"
        '
        'nudPicOffsetY
        '
        Me.nudPicOffsetY.Location = New System.Drawing.Point(487, 52)
        Me.nudPicOffsetY.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudPicOffsetY.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudPicOffsetY.Name = "nudPicOffsetY"
        Me.nudPicOffsetY.Size = New System.Drawing.Size(66, 23)
        Me.nudPicOffsetY.TabIndex = 49
        '
        'nudPicOffsetX
        '
        Me.nudPicOffsetX.Location = New System.Drawing.Point(336, 52)
        Me.nudPicOffsetX.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudPicOffsetX.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudPicOffsetX.Name = "nudPicOffsetX"
        Me.nudPicOffsetX.Size = New System.Drawing.Size(66, 23)
        Me.nudPicOffsetX.TabIndex = 48
        '
        'DarkLabel69
        '
        Me.DarkLabel69.AutoSize = True
        Me.DarkLabel69.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel69.Location = New System.Drawing.Point(130, 26)
        Me.DarkLabel69.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel69.Name = "DarkLabel69"
        Me.DarkLabel69.Size = New System.Drawing.Size(47, 15)
        Me.DarkLabel69.TabIndex = 47
        Me.DarkLabel69.Text = "Picture:"
        '
        'cmbPicLoc
        '
        Me.cmbPicLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPicLoc.FormattingEnabled = True
        Me.cmbPicLoc.Items.AddRange(New Object() {"Top Left of Screen", "Center Screen", "Centered on Event", "Centered on Player"})
        Me.cmbPicLoc.Location = New System.Drawing.Point(133, 86)
        Me.cmbPicLoc.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPicLoc.Name = "cmbPicLoc"
        Me.cmbPicLoc.Size = New System.Drawing.Size(144, 24)
        Me.cmbPicLoc.TabIndex = 46
        '
        'nudShowPicture
        '
        Me.nudShowPicture.Location = New System.Drawing.Point(185, 24)
        Me.nudShowPicture.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudShowPicture.Name = "nudShowPicture"
        Me.nudShowPicture.Size = New System.Drawing.Size(88, 23)
        Me.nudShowPicture.TabIndex = 45
        '
        'picShowPic
        '
        Me.picShowPic.BackColor = System.Drawing.Color.Black
        Me.picShowPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picShowPic.Location = New System.Drawing.Point(9, 21)
        Me.picShowPic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picShowPic.Name = "picShowPic"
        Me.picShowPic.Size = New System.Drawing.Size(117, 107)
        Me.picShowPic.TabIndex = 42
        Me.picShowPic.TabStop = False
        '
        'fraMoveRouteWait
        '
        Me.fraMoveRouteWait.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraMoveRouteWait.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraMoveRouteWait.Controls.Add(Me.btnMoveWaitCancel)
        Me.fraMoveRouteWait.Controls.Add(Me.btnMoveWaitOk)
        Me.fraMoveRouteWait.Controls.Add(Me.DarkLabel79)
        Me.fraMoveRouteWait.Controls.Add(Me.cmbMoveWait)
        Me.fraMoveRouteWait.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraMoveRouteWait.Location = New System.Drawing.Point(468, 571)
        Me.fraMoveRouteWait.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMoveRouteWait.Name = "fraMoveRouteWait"
        Me.fraMoveRouteWait.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMoveRouteWait.Size = New System.Drawing.Size(289, 87)
        Me.fraMoveRouteWait.TabIndex = 48
        Me.fraMoveRouteWait.TabStop = False
        Me.fraMoveRouteWait.Text = "Move Route Wait"
        Me.fraMoveRouteWait.Visible = False
        '
        'btnMoveWaitCancel
        '
        Me.btnMoveWaitCancel.Location = New System.Drawing.Point(195, 53)
        Me.btnMoveWaitCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMoveWaitCancel.Name = "btnMoveWaitCancel"
        Me.btnMoveWaitCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnMoveWaitCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnMoveWaitCancel.TabIndex = 26
        Me.btnMoveWaitCancel.Text = "Cancel"
        '
        'btnMoveWaitOk
        '
        Me.btnMoveWaitOk.Location = New System.Drawing.Point(100, 53)
        Me.btnMoveWaitOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMoveWaitOk.Name = "btnMoveWaitOk"
        Me.btnMoveWaitOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnMoveWaitOk.Size = New System.Drawing.Size(88, 27)
        Me.btnMoveWaitOk.TabIndex = 27
        Me.btnMoveWaitOk.Text = "Ok"
        '
        'DarkLabel79
        '
        Me.DarkLabel79.AutoSize = True
        Me.DarkLabel79.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel79.Location = New System.Drawing.Point(8, 25)
        Me.DarkLabel79.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel79.Name = "DarkLabel79"
        Me.DarkLabel79.Size = New System.Drawing.Size(39, 15)
        Me.DarkLabel79.TabIndex = 1
        Me.DarkLabel79.Text = "Event:"
        '
        'cmbMoveWait
        '
        Me.cmbMoveWait.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoveWait.FormattingEnabled = True
        Me.cmbMoveWait.Location = New System.Drawing.Point(59, 22)
        Me.cmbMoveWait.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbMoveWait.Name = "cmbMoveWait"
        Me.cmbMoveWait.Size = New System.Drawing.Size(222, 24)
        Me.cmbMoveWait.TabIndex = 0
        '
        'fraCustomScript
        '
        Me.fraCustomScript.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraCustomScript.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraCustomScript.Controls.Add(Me.nudCustomScript)
        Me.fraCustomScript.Controls.Add(Me.DarkLabel78)
        Me.fraCustomScript.Controls.Add(Me.btnCustomScriptCancel)
        Me.fraCustomScript.Controls.Add(Me.btnCustomScriptOk)
        Me.fraCustomScript.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraCustomScript.Location = New System.Drawing.Point(468, 457)
        Me.fraCustomScript.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraCustomScript.Name = "fraCustomScript"
        Me.fraCustomScript.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraCustomScript.Size = New System.Drawing.Size(289, 110)
        Me.fraCustomScript.TabIndex = 47
        Me.fraCustomScript.TabStop = False
        Me.fraCustomScript.Text = "Execute Custom Script"
        Me.fraCustomScript.Visible = False
        '
        'nudCustomScript
        '
        Me.nudCustomScript.Location = New System.Drawing.Point(78, 22)
        Me.nudCustomScript.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudCustomScript.Name = "nudCustomScript"
        Me.nudCustomScript.Size = New System.Drawing.Size(197, 23)
        Me.nudCustomScript.TabIndex = 1
        '
        'DarkLabel78
        '
        Me.DarkLabel78.AutoSize = True
        Me.DarkLabel78.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel78.Location = New System.Drawing.Point(12, 24)
        Me.DarkLabel78.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel78.Name = "DarkLabel78"
        Me.DarkLabel78.Size = New System.Drawing.Size(35, 15)
        Me.DarkLabel78.TabIndex = 0
        Me.DarkLabel78.Text = "Case:"
        '
        'btnCustomScriptCancel
        '
        Me.btnCustomScriptCancel.Location = New System.Drawing.Point(188, 52)
        Me.btnCustomScriptCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCustomScriptCancel.Name = "btnCustomScriptCancel"
        Me.btnCustomScriptCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCustomScriptCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnCustomScriptCancel.TabIndex = 24
        Me.btnCustomScriptCancel.Text = "Cancel"
        '
        'btnCustomScriptOk
        '
        Me.btnCustomScriptOk.Location = New System.Drawing.Point(93, 52)
        Me.btnCustomScriptOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCustomScriptOk.Name = "btnCustomScriptOk"
        Me.btnCustomScriptOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCustomScriptOk.Size = New System.Drawing.Size(88, 27)
        Me.btnCustomScriptOk.TabIndex = 25
        Me.btnCustomScriptOk.Text = "Ok"
        '
        'fraSetWeather
        '
        Me.fraSetWeather.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetWeather.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraSetWeather.Controls.Add(Me.btnSetWeatherOk)
        Me.fraSetWeather.Controls.Add(Me.btnSetWeatherCancel)
        Me.fraSetWeather.Controls.Add(Me.DarkLabel76)
        Me.fraSetWeather.Controls.Add(Me.nudWeatherIntensity)
        Me.fraSetWeather.Controls.Add(Me.DarkLabel75)
        Me.fraSetWeather.Controls.Add(Me.CmbWeather)
        Me.fraSetWeather.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetWeather.Location = New System.Drawing.Point(468, 406)
        Me.fraSetWeather.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetWeather.Name = "fraSetWeather"
        Me.fraSetWeather.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetWeather.Size = New System.Drawing.Size(289, 110)
        Me.fraSetWeather.TabIndex = 44
        Me.fraSetWeather.TabStop = False
        Me.fraSetWeather.Text = "Set Weather"
        Me.fraSetWeather.Visible = False
        '
        'btnSetWeatherOk
        '
        Me.btnSetWeatherOk.Location = New System.Drawing.Point(54, 76)
        Me.btnSetWeatherOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetWeatherOk.Name = "btnSetWeatherOk"
        Me.btnSetWeatherOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetWeatherOk.Size = New System.Drawing.Size(88, 27)
        Me.btnSetWeatherOk.TabIndex = 34
        Me.btnSetWeatherOk.Text = "Ok"
        '
        'btnSetWeatherCancel
        '
        Me.btnSetWeatherCancel.Location = New System.Drawing.Point(148, 76)
        Me.btnSetWeatherCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetWeatherCancel.Name = "btnSetWeatherCancel"
        Me.btnSetWeatherCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetWeatherCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnSetWeatherCancel.TabIndex = 33
        Me.btnSetWeatherCancel.Text = "Cancel"
        '
        'DarkLabel76
        '
        Me.DarkLabel76.AutoSize = True
        Me.DarkLabel76.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel76.Location = New System.Drawing.Point(9, 51)
        Me.DarkLabel76.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel76.Name = "DarkLabel76"
        Me.DarkLabel76.Size = New System.Drawing.Size(55, 15)
        Me.DarkLabel76.TabIndex = 32
        Me.DarkLabel76.Text = "Intensity:"
        '
        'nudWeatherIntensity
        '
        Me.nudWeatherIntensity.Location = New System.Drawing.Point(102, 47)
        Me.nudWeatherIntensity.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudWeatherIntensity.Name = "nudWeatherIntensity"
        Me.nudWeatherIntensity.Size = New System.Drawing.Size(181, 23)
        Me.nudWeatherIntensity.TabIndex = 31
        '
        'DarkLabel75
        '
        Me.DarkLabel75.AutoSize = True
        Me.DarkLabel75.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel75.Location = New System.Drawing.Point(7, 21)
        Me.DarkLabel75.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel75.Name = "DarkLabel75"
        Me.DarkLabel75.Size = New System.Drawing.Size(78, 15)
        Me.DarkLabel75.TabIndex = 1
        Me.DarkLabel75.Text = "Weather Type"
        '
        'CmbWeather
        '
        Me.CmbWeather.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbWeather.FormattingEnabled = True
        Me.CmbWeather.Items.AddRange(New Object() {"None", "Rain", "Snow", "Hail", "Sand Storm", "Storm"})
        Me.CmbWeather.Location = New System.Drawing.Point(100, 17)
        Me.CmbWeather.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CmbWeather.Name = "CmbWeather"
        Me.CmbWeather.Size = New System.Drawing.Size(180, 24)
        Me.CmbWeather.TabIndex = 0
        '
        'fraSpawnNpc
        '
        Me.fraSpawnNpc.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSpawnNpc.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraSpawnNpc.Controls.Add(Me.btnSpawnNpcOk)
        Me.fraSpawnNpc.Controls.Add(Me.btnSpawnNpcCancel)
        Me.fraSpawnNpc.Controls.Add(Me.cmbSpawnNpc)
        Me.fraSpawnNpc.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSpawnNpc.Location = New System.Drawing.Point(468, 475)
        Me.fraSpawnNpc.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSpawnNpc.Name = "fraSpawnNpc"
        Me.fraSpawnNpc.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSpawnNpc.Size = New System.Drawing.Size(289, 89)
        Me.fraSpawnNpc.TabIndex = 46
        Me.fraSpawnNpc.TabStop = False
        Me.fraSpawnNpc.Text = "Spawn Npc"
        Me.fraSpawnNpc.Visible = False
        '
        'btnSpawnNpcOk
        '
        Me.btnSpawnNpcOk.Location = New System.Drawing.Point(54, 54)
        Me.btnSpawnNpcOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSpawnNpcOk.Name = "btnSpawnNpcOk"
        Me.btnSpawnNpcOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSpawnNpcOk.Size = New System.Drawing.Size(88, 27)
        Me.btnSpawnNpcOk.TabIndex = 27
        Me.btnSpawnNpcOk.Text = "Ok"
        '
        'btnSpawnNpcCancel
        '
        Me.btnSpawnNpcCancel.Location = New System.Drawing.Point(148, 54)
        Me.btnSpawnNpcCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSpawnNpcCancel.Name = "btnSpawnNpcCancel"
        Me.btnSpawnNpcCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSpawnNpcCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnSpawnNpcCancel.TabIndex = 26
        Me.btnSpawnNpcCancel.Text = "Cancel"
        '
        'cmbSpawnNpc
        '
        Me.cmbSpawnNpc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSpawnNpc.FormattingEnabled = True
        Me.cmbSpawnNpc.Location = New System.Drawing.Point(7, 22)
        Me.cmbSpawnNpc.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbSpawnNpc.Name = "cmbSpawnNpc"
        Me.cmbSpawnNpc.Size = New System.Drawing.Size(272, 24)
        Me.cmbSpawnNpc.TabIndex = 0
        '
        'fraGiveExp
        '
        Me.fraGiveExp.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraGiveExp.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraGiveExp.Controls.Add(Me.btnGiveExpOk)
        Me.fraGiveExp.Controls.Add(Me.btnGiveExpCancel)
        Me.fraGiveExp.Controls.Add(Me.nudGiveExp)
        Me.fraGiveExp.Controls.Add(Me.DarkLabel77)
        Me.fraGiveExp.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraGiveExp.Location = New System.Drawing.Point(468, 406)
        Me.fraGiveExp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraGiveExp.Name = "fraGiveExp"
        Me.fraGiveExp.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraGiveExp.Size = New System.Drawing.Size(289, 84)
        Me.fraGiveExp.TabIndex = 45
        Me.fraGiveExp.TabStop = False
        Me.fraGiveExp.Text = "Give Experience"
        Me.fraGiveExp.Visible = False
        '
        'btnGiveExpOk
        '
        Me.btnGiveExpOk.Location = New System.Drawing.Point(58, 52)
        Me.btnGiveExpOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnGiveExpOk.Name = "btnGiveExpOk"
        Me.btnGiveExpOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnGiveExpOk.Size = New System.Drawing.Size(88, 27)
        Me.btnGiveExpOk.TabIndex = 27
        Me.btnGiveExpOk.Text = "Ok"
        '
        'btnGiveExpCancel
        '
        Me.btnGiveExpCancel.Location = New System.Drawing.Point(153, 52)
        Me.btnGiveExpCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnGiveExpCancel.Name = "btnGiveExpCancel"
        Me.btnGiveExpCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnGiveExpCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnGiveExpCancel.TabIndex = 26
        Me.btnGiveExpCancel.Text = "Cancel"
        '
        'nudGiveExp
        '
        Me.nudGiveExp.Location = New System.Drawing.Point(90, 22)
        Me.nudGiveExp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudGiveExp.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudGiveExp.Name = "nudGiveExp"
        Me.nudGiveExp.Size = New System.Drawing.Size(192, 23)
        Me.nudGiveExp.TabIndex = 20
        '
        'DarkLabel77
        '
        Me.DarkLabel77.AutoSize = True
        Me.DarkLabel77.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel77.Location = New System.Drawing.Point(7, 24)
        Me.DarkLabel77.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel77.Name = "DarkLabel77"
        Me.DarkLabel77.Size = New System.Drawing.Size(55, 15)
        Me.DarkLabel77.TabIndex = 0
        Me.DarkLabel77.Text = "Give Exp:"
        '
        'fraEndQuest
        '
        Me.fraEndQuest.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraEndQuest.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraEndQuest.Controls.Add(Me.btnEndQuestOk)
        Me.fraEndQuest.Controls.Add(Me.btnEndQuestCancel)
        Me.fraEndQuest.Controls.Add(Me.cmbEndQuest)
        Me.fraEndQuest.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraEndQuest.Location = New System.Drawing.Point(468, 480)
        Me.fraEndQuest.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraEndQuest.Name = "fraEndQuest"
        Me.fraEndQuest.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraEndQuest.Size = New System.Drawing.Size(289, 84)
        Me.fraEndQuest.TabIndex = 43
        Me.fraEndQuest.TabStop = False
        Me.fraEndQuest.Text = "End Quest"
        Me.fraEndQuest.Visible = False
        '
        'btnEndQuestOk
        '
        Me.btnEndQuestOk.Location = New System.Drawing.Point(54, 51)
        Me.btnEndQuestOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnEndQuestOk.Name = "btnEndQuestOk"
        Me.btnEndQuestOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnEndQuestOk.Size = New System.Drawing.Size(88, 27)
        Me.btnEndQuestOk.TabIndex = 30
        Me.btnEndQuestOk.Text = "Ok"
        '
        'btnEndQuestCancel
        '
        Me.btnEndQuestCancel.Location = New System.Drawing.Point(148, 51)
        Me.btnEndQuestCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnEndQuestCancel.Name = "btnEndQuestCancel"
        Me.btnEndQuestCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnEndQuestCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnEndQuestCancel.TabIndex = 29
        Me.btnEndQuestCancel.Text = "Cancel"
        '
        'cmbEndQuest
        '
        Me.cmbEndQuest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbEndQuest.FormattingEnabled = True
        Me.cmbEndQuest.Location = New System.Drawing.Point(38, 17)
        Me.cmbEndQuest.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbEndQuest.Name = "cmbEndQuest"
        Me.cmbEndQuest.Size = New System.Drawing.Size(219, 24)
        Me.cmbEndQuest.TabIndex = 28
        '
        'fraSetAccess
        '
        Me.fraSetAccess.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetAccess.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraSetAccess.Controls.Add(Me.btnSetAccessOk)
        Me.fraSetAccess.Controls.Add(Me.btnSetAccessCancel)
        Me.fraSetAccess.Controls.Add(Me.cmbSetAccess)
        Me.fraSetAccess.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetAccess.Location = New System.Drawing.Point(468, 407)
        Me.fraSetAccess.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetAccess.Name = "fraSetAccess"
        Me.fraSetAccess.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetAccess.Size = New System.Drawing.Size(289, 92)
        Me.fraSetAccess.TabIndex = 42
        Me.fraSetAccess.TabStop = False
        Me.fraSetAccess.Text = "Set Access"
        Me.fraSetAccess.Visible = False
        '
        'btnSetAccessOk
        '
        Me.btnSetAccessOk.Location = New System.Drawing.Point(54, 55)
        Me.btnSetAccessOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetAccessOk.Name = "btnSetAccessOk"
        Me.btnSetAccessOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetAccessOk.Size = New System.Drawing.Size(88, 27)
        Me.btnSetAccessOk.TabIndex = 27
        Me.btnSetAccessOk.Text = "Ok"
        '
        'btnSetAccessCancel
        '
        Me.btnSetAccessCancel.Location = New System.Drawing.Point(148, 55)
        Me.btnSetAccessCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetAccessCancel.Name = "btnSetAccessCancel"
        Me.btnSetAccessCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetAccessCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnSetAccessCancel.TabIndex = 26
        Me.btnSetAccessCancel.Text = "Cancel"
        '
        'cmbSetAccess
        '
        Me.cmbSetAccess.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSetAccess.FormattingEnabled = True
        Me.cmbSetAccess.Items.AddRange(New Object() {"0: Player", "1: Moderator", "2: Mapper", "3: Developer", "4: Creator"})
        Me.cmbSetAccess.Location = New System.Drawing.Point(38, 22)
        Me.cmbSetAccess.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbSetAccess.Name = "cmbSetAccess"
        Me.cmbSetAccess.Size = New System.Drawing.Size(219, 24)
        Me.cmbSetAccess.TabIndex = 0
        '
        'fraOpenShop
        '
        Me.fraOpenShop.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraOpenShop.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraOpenShop.Controls.Add(Me.btnOpenShopOk)
        Me.fraOpenShop.Controls.Add(Me.btnOpenShopCancel)
        Me.fraOpenShop.Controls.Add(Me.cmbOpenShop)
        Me.fraOpenShop.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraOpenShop.Location = New System.Drawing.Point(470, 250)
        Me.fraOpenShop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraOpenShop.Name = "fraOpenShop"
        Me.fraOpenShop.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraOpenShop.Size = New System.Drawing.Size(287, 91)
        Me.fraOpenShop.TabIndex = 39
        Me.fraOpenShop.TabStop = False
        Me.fraOpenShop.Text = "Open Shop"
        Me.fraOpenShop.Visible = False
        '
        'btnOpenShopOk
        '
        Me.btnOpenShopOk.Location = New System.Drawing.Point(51, 54)
        Me.btnOpenShopOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnOpenShopOk.Name = "btnOpenShopOk"
        Me.btnOpenShopOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnOpenShopOk.Size = New System.Drawing.Size(88, 27)
        Me.btnOpenShopOk.TabIndex = 27
        Me.btnOpenShopOk.Text = "Ok"
        '
        'btnOpenShopCancel
        '
        Me.btnOpenShopCancel.Location = New System.Drawing.Point(146, 54)
        Me.btnOpenShopCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnOpenShopCancel.Name = "btnOpenShopCancel"
        Me.btnOpenShopCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnOpenShopCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnOpenShopCancel.TabIndex = 26
        Me.btnOpenShopCancel.Text = "Cancel"
        '
        'cmbOpenShop
        '
        Me.cmbOpenShop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbOpenShop.FormattingEnabled = True
        Me.cmbOpenShop.Location = New System.Drawing.Point(10, 23)
        Me.cmbOpenShop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbOpenShop.Name = "cmbOpenShop"
        Me.cmbOpenShop.Size = New System.Drawing.Size(263, 24)
        Me.cmbOpenShop.TabIndex = 0
        '
        'fraChangeLevel
        '
        Me.fraChangeLevel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeLevel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraChangeLevel.Controls.Add(Me.btnChangeLevelOk)
        Me.fraChangeLevel.Controls.Add(Me.btnChangeLevelCancel)
        Me.fraChangeLevel.Controls.Add(Me.DarkLabel65)
        Me.fraChangeLevel.Controls.Add(Me.nudChangeLevel)
        Me.fraChangeLevel.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeLevel.Location = New System.Drawing.Point(468, 338)
        Me.fraChangeLevel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeLevel.Name = "fraChangeLevel"
        Me.fraChangeLevel.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeLevel.Size = New System.Drawing.Size(289, 83)
        Me.fraChangeLevel.TabIndex = 38
        Me.fraChangeLevel.TabStop = False
        Me.fraChangeLevel.Text = "Change Level"
        Me.fraChangeLevel.Visible = False
        '
        'btnChangeLevelOk
        '
        Me.btnChangeLevelOk.Location = New System.Drawing.Point(54, 52)
        Me.btnChangeLevelOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeLevelOk.Name = "btnChangeLevelOk"
        Me.btnChangeLevelOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeLevelOk.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeLevelOk.TabIndex = 27
        Me.btnChangeLevelOk.Text = "Ok"
        '
        'btnChangeLevelCancel
        '
        Me.btnChangeLevelCancel.Location = New System.Drawing.Point(148, 52)
        Me.btnChangeLevelCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeLevelCancel.Name = "btnChangeLevelCancel"
        Me.btnChangeLevelCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeLevelCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeLevelCancel.TabIndex = 26
        Me.btnChangeLevelCancel.Text = "Cancel"
        '
        'DarkLabel65
        '
        Me.DarkLabel65.AutoSize = True
        Me.DarkLabel65.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel65.Location = New System.Drawing.Point(8, 24)
        Me.DarkLabel65.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel65.Name = "DarkLabel65"
        Me.DarkLabel65.Size = New System.Drawing.Size(37, 15)
        Me.DarkLabel65.TabIndex = 24
        Me.DarkLabel65.Text = "Level:"
        '
        'nudChangeLevel
        '
        Me.nudChangeLevel.Location = New System.Drawing.Point(70, 22)
        Me.nudChangeLevel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudChangeLevel.Name = "nudChangeLevel"
        Me.nudChangeLevel.Size = New System.Drawing.Size(140, 23)
        Me.nudChangeLevel.TabIndex = 23
        '
        'fraChangeGender
        '
        Me.fraChangeGender.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeGender.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraChangeGender.Controls.Add(Me.btnChangeGenderOk)
        Me.fraChangeGender.Controls.Add(Me.btnChangeGenderCancel)
        Me.fraChangeGender.Controls.Add(Me.optChangeSexFemale)
        Me.fraChangeGender.Controls.Add(Me.optChangeSexMale)
        Me.fraChangeGender.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeGender.Location = New System.Drawing.Point(468, 420)
        Me.fraChangeGender.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeGender.Name = "fraChangeGender"
        Me.fraChangeGender.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeGender.Size = New System.Drawing.Size(289, 83)
        Me.fraChangeGender.TabIndex = 37
        Me.fraChangeGender.TabStop = False
        Me.fraChangeGender.Text = "Change Player Gender"
        Me.fraChangeGender.Visible = False
        '
        'btnChangeGenderOk
        '
        Me.btnChangeGenderOk.Location = New System.Drawing.Point(46, 48)
        Me.btnChangeGenderOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeGenderOk.Name = "btnChangeGenderOk"
        Me.btnChangeGenderOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeGenderOk.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeGenderOk.TabIndex = 27
        Me.btnChangeGenderOk.Text = "Ok"
        '
        'btnChangeGenderCancel
        '
        Me.btnChangeGenderCancel.Location = New System.Drawing.Point(140, 48)
        Me.btnChangeGenderCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeGenderCancel.Name = "btnChangeGenderCancel"
        Me.btnChangeGenderCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeGenderCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeGenderCancel.TabIndex = 26
        Me.btnChangeGenderCancel.Text = "Cancel"
        '
        'optChangeSexFemale
        '
        Me.optChangeSexFemale.AutoSize = True
        Me.optChangeSexFemale.Location = New System.Drawing.Point(164, 22)
        Me.optChangeSexFemale.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optChangeSexFemale.Name = "optChangeSexFemale"
        Me.optChangeSexFemale.Size = New System.Drawing.Size(63, 19)
        Me.optChangeSexFemale.TabIndex = 1
        Me.optChangeSexFemale.TabStop = True
        Me.optChangeSexFemale.Text = "Female"
        '
        'optChangeSexMale
        '
        Me.optChangeSexMale.AutoSize = True
        Me.optChangeSexMale.Location = New System.Drawing.Point(61, 22)
        Me.optChangeSexMale.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optChangeSexMale.Name = "optChangeSexMale"
        Me.optChangeSexMale.Size = New System.Drawing.Size(51, 19)
        Me.optChangeSexMale.TabIndex = 0
        Me.optChangeSexMale.TabStop = True
        Me.optChangeSexMale.Text = "Male"
        '
        'fraGoToLabel
        '
        Me.fraGoToLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraGoToLabel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraGoToLabel.Controls.Add(Me.btnGoToLabelOk)
        Me.fraGoToLabel.Controls.Add(Me.btnGoToLabelCancel)
        Me.fraGoToLabel.Controls.Add(Me.txtGotoLabel)
        Me.fraGoToLabel.Controls.Add(Me.DarkLabel60)
        Me.fraGoToLabel.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraGoToLabel.Location = New System.Drawing.Point(468, 294)
        Me.fraGoToLabel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraGoToLabel.Name = "fraGoToLabel"
        Me.fraGoToLabel.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraGoToLabel.Size = New System.Drawing.Size(289, 84)
        Me.fraGoToLabel.TabIndex = 35
        Me.fraGoToLabel.TabStop = False
        Me.fraGoToLabel.Text = "GoTo Label"
        Me.fraGoToLabel.Visible = False
        '
        'btnGoToLabelOk
        '
        Me.btnGoToLabelOk.Location = New System.Drawing.Point(100, 51)
        Me.btnGoToLabelOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnGoToLabelOk.Name = "btnGoToLabelOk"
        Me.btnGoToLabelOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnGoToLabelOk.Size = New System.Drawing.Size(88, 27)
        Me.btnGoToLabelOk.TabIndex = 27
        Me.btnGoToLabelOk.Text = "Ok"
        '
        'btnGoToLabelCancel
        '
        Me.btnGoToLabelCancel.Location = New System.Drawing.Point(195, 51)
        Me.btnGoToLabelCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnGoToLabelCancel.Name = "btnGoToLabelCancel"
        Me.btnGoToLabelCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnGoToLabelCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnGoToLabelCancel.TabIndex = 26
        Me.btnGoToLabelCancel.Text = "Cancel"
        '
        'txtGotoLabel
        '
        Me.txtGotoLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtGotoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGotoLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtGotoLabel.Location = New System.Drawing.Point(91, 21)
        Me.txtGotoLabel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtGotoLabel.Name = "txtGotoLabel"
        Me.txtGotoLabel.Size = New System.Drawing.Size(191, 23)
        Me.txtGotoLabel.TabIndex = 1
        '
        'DarkLabel60
        '
        Me.DarkLabel60.AutoSize = True
        Me.DarkLabel60.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel60.Location = New System.Drawing.Point(4, 23)
        Me.DarkLabel60.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel60.Name = "DarkLabel60"
        Me.DarkLabel60.Size = New System.Drawing.Size(73, 15)
        Me.DarkLabel60.TabIndex = 0
        Me.DarkLabel60.Text = "Label Name:"
        '
        'fraShowChoices
        '
        Me.fraShowChoices.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraShowChoices.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraShowChoices.Controls.Add(Me.txtChoices4)
        Me.fraShowChoices.Controls.Add(Me.txtChoices3)
        Me.fraShowChoices.Controls.Add(Me.txtChoices2)
        Me.fraShowChoices.Controls.Add(Me.txtChoices1)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel56)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel57)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel55)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel54)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel52)
        Me.fraShowChoices.Controls.Add(Me.txtChoicePrompt)
        Me.fraShowChoices.Controls.Add(Me.btnShowChoicesOk)
        Me.fraShowChoices.Controls.Add(Me.picShowChoicesFace)
        Me.fraShowChoices.Controls.Add(Me.btnShowChoicesCancel)
        Me.fraShowChoices.Controls.Add(Me.DarkLabel53)
        Me.fraShowChoices.Controls.Add(Me.nudShowChoicesFace)
        Me.fraShowChoices.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraShowChoices.Location = New System.Drawing.Point(468, 119)
        Me.fraShowChoices.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShowChoices.Name = "fraShowChoices"
        Me.fraShowChoices.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShowChoices.Size = New System.Drawing.Size(289, 384)
        Me.fraShowChoices.TabIndex = 32
        Me.fraShowChoices.TabStop = False
        Me.fraShowChoices.Text = "Show Choices"
        Me.fraShowChoices.Visible = False
        '
        'txtChoices4
        '
        Me.txtChoices4.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoices4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoices4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoices4.Location = New System.Drawing.Point(164, 201)
        Me.txtChoices4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtChoices4.Name = "txtChoices4"
        Me.txtChoices4.Size = New System.Drawing.Size(116, 23)
        Me.txtChoices4.TabIndex = 34
        '
        'txtChoices3
        '
        Me.txtChoices3.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoices3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoices3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoices3.Location = New System.Drawing.Point(7, 200)
        Me.txtChoices3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtChoices3.Name = "txtChoices3"
        Me.txtChoices3.Size = New System.Drawing.Size(116, 23)
        Me.txtChoices3.TabIndex = 33
        '
        'txtChoices2
        '
        Me.txtChoices2.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoices2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoices2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoices2.Location = New System.Drawing.Point(164, 155)
        Me.txtChoices2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtChoices2.Name = "txtChoices2"
        Me.txtChoices2.Size = New System.Drawing.Size(116, 23)
        Me.txtChoices2.TabIndex = 32
        '
        'txtChoices1
        '
        Me.txtChoices1.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoices1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoices1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoices1.Location = New System.Drawing.Point(7, 155)
        Me.txtChoices1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtChoices1.Name = "txtChoices1"
        Me.txtChoices1.Size = New System.Drawing.Size(116, 23)
        Me.txtChoices1.TabIndex = 31
        '
        'DarkLabel56
        '
        Me.DarkLabel56.AutoSize = True
        Me.DarkLabel56.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel56.Location = New System.Drawing.Point(161, 181)
        Me.DarkLabel56.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel56.Name = "DarkLabel56"
        Me.DarkLabel56.Size = New System.Drawing.Size(53, 15)
        Me.DarkLabel56.TabIndex = 30
        Me.DarkLabel56.Text = "Choice 4"
        '
        'DarkLabel57
        '
        Me.DarkLabel57.AutoSize = True
        Me.DarkLabel57.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel57.Location = New System.Drawing.Point(8, 181)
        Me.DarkLabel57.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel57.Name = "DarkLabel57"
        Me.DarkLabel57.Size = New System.Drawing.Size(53, 15)
        Me.DarkLabel57.TabIndex = 29
        Me.DarkLabel57.Text = "Choice 3"
        '
        'DarkLabel55
        '
        Me.DarkLabel55.AutoSize = True
        Me.DarkLabel55.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel55.Location = New System.Drawing.Point(161, 136)
        Me.DarkLabel55.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel55.Name = "DarkLabel55"
        Me.DarkLabel55.Size = New System.Drawing.Size(53, 15)
        Me.DarkLabel55.TabIndex = 28
        Me.DarkLabel55.Text = "Choice 2"
        '
        'DarkLabel54
        '
        Me.DarkLabel54.AutoSize = True
        Me.DarkLabel54.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel54.Location = New System.Drawing.Point(7, 136)
        Me.DarkLabel54.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel54.Name = "DarkLabel54"
        Me.DarkLabel54.Size = New System.Drawing.Size(53, 15)
        Me.DarkLabel54.TabIndex = 27
        Me.DarkLabel54.Text = "Choice 1"
        '
        'DarkLabel52
        '
        Me.DarkLabel52.AutoSize = True
        Me.DarkLabel52.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel52.Location = New System.Drawing.Point(8, 22)
        Me.DarkLabel52.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel52.Name = "DarkLabel52"
        Me.DarkLabel52.Size = New System.Drawing.Size(47, 15)
        Me.DarkLabel52.TabIndex = 26
        Me.DarkLabel52.Text = "Prompt"
        '
        'txtChoicePrompt
        '
        Me.txtChoicePrompt.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChoicePrompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChoicePrompt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChoicePrompt.Location = New System.Drawing.Point(10, 44)
        Me.txtChoicePrompt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtChoicePrompt.Multiline = True
        Me.txtChoicePrompt.Name = "txtChoicePrompt"
        Me.txtChoicePrompt.Size = New System.Drawing.Size(266, 89)
        Me.txtChoicePrompt.TabIndex = 21
        '
        'btnShowChoicesOk
        '
        Me.btnShowChoicesOk.Location = New System.Drawing.Point(98, 352)
        Me.btnShowChoicesOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShowChoicesOk.Name = "btnShowChoicesOk"
        Me.btnShowChoicesOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnShowChoicesOk.Size = New System.Drawing.Size(88, 27)
        Me.btnShowChoicesOk.TabIndex = 25
        Me.btnShowChoicesOk.Text = "Ok"
        '
        'picShowChoicesFace
        '
        Me.picShowChoicesFace.BackColor = System.Drawing.Color.Black
        Me.picShowChoicesFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picShowChoicesFace.Location = New System.Drawing.Point(7, 230)
        Me.picShowChoicesFace.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picShowChoicesFace.Name = "picShowChoicesFace"
        Me.picShowChoicesFace.Size = New System.Drawing.Size(117, 107)
        Me.picShowChoicesFace.TabIndex = 2
        Me.picShowChoicesFace.TabStop = False
        '
        'btnShowChoicesCancel
        '
        Me.btnShowChoicesCancel.Location = New System.Drawing.Point(192, 352)
        Me.btnShowChoicesCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShowChoicesCancel.Name = "btnShowChoicesCancel"
        Me.btnShowChoicesCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnShowChoicesCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnShowChoicesCancel.TabIndex = 24
        Me.btnShowChoicesCancel.Text = "Cancel"
        '
        'DarkLabel53
        '
        Me.DarkLabel53.AutoSize = True
        Me.DarkLabel53.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel53.Location = New System.Drawing.Point(127, 316)
        Me.DarkLabel53.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel53.Name = "DarkLabel53"
        Me.DarkLabel53.Size = New System.Drawing.Size(34, 15)
        Me.DarkLabel53.TabIndex = 22
        Me.DarkLabel53.Text = "Face:"
        '
        'nudShowChoicesFace
        '
        Me.nudShowChoicesFace.Location = New System.Drawing.Point(170, 314)
        Me.nudShowChoicesFace.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudShowChoicesFace.Name = "nudShowChoicesFace"
        Me.nudShowChoicesFace.Size = New System.Drawing.Size(107, 23)
        Me.nudShowChoicesFace.TabIndex = 23
        '
        'fraPlayerVariable
        '
        Me.fraPlayerVariable.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayerVariable.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData2)
        Me.fraPlayerVariable.Controls.Add(Me.optVariableAction2)
        Me.fraPlayerVariable.Controls.Add(Me.btnPlayerVarOk)
        Me.fraPlayerVariable.Controls.Add(Me.btnPlayerVarCancel)
        Me.fraPlayerVariable.Controls.Add(Me.DarkLabel51)
        Me.fraPlayerVariable.Controls.Add(Me.DarkLabel50)
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData4)
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData3)
        Me.fraPlayerVariable.Controls.Add(Me.optVariableAction3)
        Me.fraPlayerVariable.Controls.Add(Me.optVariableAction1)
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData1)
        Me.fraPlayerVariable.Controls.Add(Me.nudVariableData0)
        Me.fraPlayerVariable.Controls.Add(Me.optVariableAction0)
        Me.fraPlayerVariable.Controls.Add(Me.cmbVariable)
        Me.fraPlayerVariable.Controls.Add(Me.DarkLabel49)
        Me.fraPlayerVariable.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayerVariable.Location = New System.Drawing.Point(468, 325)
        Me.fraPlayerVariable.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayerVariable.Name = "fraPlayerVariable"
        Me.fraPlayerVariable.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayerVariable.Size = New System.Drawing.Size(287, 178)
        Me.fraPlayerVariable.TabIndex = 31
        Me.fraPlayerVariable.TabStop = False
        Me.fraPlayerVariable.Text = "Player Variable"
        Me.fraPlayerVariable.Visible = False
        '
        'nudVariableData2
        '
        Me.nudVariableData2.Location = New System.Drawing.Point(140, 83)
        Me.nudVariableData2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudVariableData2.Name = "nudVariableData2"
        Me.nudVariableData2.Size = New System.Drawing.Size(140, 23)
        Me.nudVariableData2.TabIndex = 29
        '
        'optVariableAction2
        '
        Me.optVariableAction2.AutoSize = True
        Me.optVariableAction2.Location = New System.Drawing.Point(7, 83)
        Me.optVariableAction2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optVariableAction2.Name = "optVariableAction2"
        Me.optVariableAction2.Size = New System.Drawing.Size(69, 19)
        Me.optVariableAction2.TabIndex = 28
        Me.optVariableAction2.TabStop = True
        Me.optVariableAction2.Text = "Subtract"
        '
        'btnPlayerVarOk
        '
        Me.btnPlayerVarOk.Location = New System.Drawing.Point(98, 143)
        Me.btnPlayerVarOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlayerVarOk.Name = "btnPlayerVarOk"
        Me.btnPlayerVarOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlayerVarOk.Size = New System.Drawing.Size(88, 27)
        Me.btnPlayerVarOk.TabIndex = 27
        Me.btnPlayerVarOk.Text = "Ok"
        '
        'btnPlayerVarCancel
        '
        Me.btnPlayerVarCancel.Location = New System.Drawing.Point(192, 143)
        Me.btnPlayerVarCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlayerVarCancel.Name = "btnPlayerVarCancel"
        Me.btnPlayerVarCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlayerVarCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnPlayerVarCancel.TabIndex = 26
        Me.btnPlayerVarCancel.Text = "Cancel"
        '
        'DarkLabel51
        '
        Me.DarkLabel51.AutoSize = True
        Me.DarkLabel51.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel51.Location = New System.Drawing.Point(88, 115)
        Me.DarkLabel51.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel51.Name = "DarkLabel51"
        Me.DarkLabel51.Size = New System.Drawing.Size(32, 15)
        Me.DarkLabel51.TabIndex = 16
        Me.DarkLabel51.Text = "Low:"
        '
        'DarkLabel50
        '
        Me.DarkLabel50.AutoSize = True
        Me.DarkLabel50.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel50.Location = New System.Drawing.Point(184, 115)
        Me.DarkLabel50.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel50.Name = "DarkLabel50"
        Me.DarkLabel50.Size = New System.Drawing.Size(36, 15)
        Me.DarkLabel50.TabIndex = 15
        Me.DarkLabel50.Text = "High:"
        '
        'nudVariableData4
        '
        Me.nudVariableData4.Location = New System.Drawing.Point(229, 113)
        Me.nudVariableData4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudVariableData4.Name = "nudVariableData4"
        Me.nudVariableData4.Size = New System.Drawing.Size(51, 23)
        Me.nudVariableData4.TabIndex = 14
        '
        'nudVariableData3
        '
        Me.nudVariableData3.Location = New System.Drawing.Point(130, 113)
        Me.nudVariableData3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudVariableData3.Name = "nudVariableData3"
        Me.nudVariableData3.Size = New System.Drawing.Size(51, 23)
        Me.nudVariableData3.TabIndex = 13
        '
        'optVariableAction3
        '
        Me.optVariableAction3.AutoSize = True
        Me.optVariableAction3.Location = New System.Drawing.Point(7, 113)
        Me.optVariableAction3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optVariableAction3.Name = "optVariableAction3"
        Me.optVariableAction3.Size = New System.Drawing.Size(70, 19)
        Me.optVariableAction3.TabIndex = 12
        Me.optVariableAction3.TabStop = True
        Me.optVariableAction3.Text = "Random"
        '
        'optVariableAction1
        '
        Me.optVariableAction1.AutoSize = True
        Me.optVariableAction1.Location = New System.Drawing.Point(170, 53)
        Me.optVariableAction1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optVariableAction1.Name = "optVariableAction1"
        Me.optVariableAction1.Size = New System.Drawing.Size(47, 19)
        Me.optVariableAction1.TabIndex = 11
        Me.optVariableAction1.TabStop = True
        Me.optVariableAction1.Text = "Add"
        '
        'nudVariableData1
        '
        Me.nudVariableData1.Location = New System.Drawing.Point(229, 53)
        Me.nudVariableData1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudVariableData1.Name = "nudVariableData1"
        Me.nudVariableData1.Size = New System.Drawing.Size(51, 23)
        Me.nudVariableData1.TabIndex = 10
        '
        'nudVariableData0
        '
        Me.nudVariableData0.Location = New System.Drawing.Point(72, 53)
        Me.nudVariableData0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudVariableData0.Name = "nudVariableData0"
        Me.nudVariableData0.Size = New System.Drawing.Size(51, 23)
        Me.nudVariableData0.TabIndex = 9
        '
        'optVariableAction0
        '
        Me.optVariableAction0.AutoSize = True
        Me.optVariableAction0.Location = New System.Drawing.Point(7, 53)
        Me.optVariableAction0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optVariableAction0.Name = "optVariableAction0"
        Me.optVariableAction0.Size = New System.Drawing.Size(41, 19)
        Me.optVariableAction0.TabIndex = 2
        Me.optVariableAction0.TabStop = True
        Me.optVariableAction0.Text = "Set"
        '
        'cmbVariable
        '
        Me.cmbVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbVariable.FormattingEnabled = True
        Me.cmbVariable.Location = New System.Drawing.Point(70, 22)
        Me.cmbVariable.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbVariable.Name = "cmbVariable"
        Me.cmbVariable.Size = New System.Drawing.Size(208, 24)
        Me.cmbVariable.TabIndex = 1
        '
        'DarkLabel49
        '
        Me.DarkLabel49.AutoSize = True
        Me.DarkLabel49.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel49.Location = New System.Drawing.Point(7, 25)
        Me.DarkLabel49.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel49.Name = "DarkLabel49"
        Me.DarkLabel49.Size = New System.Drawing.Size(51, 15)
        Me.DarkLabel49.TabIndex = 0
        Me.DarkLabel49.Text = "Variable:"
        '
        'fraChangeSprite
        '
        Me.fraChangeSprite.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeSprite.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraChangeSprite.Controls.Add(Me.btnChangeSpriteOk)
        Me.fraChangeSprite.Controls.Add(Me.btnChangeSpriteCancel)
        Me.fraChangeSprite.Controls.Add(Me.DarkLabel48)
        Me.fraChangeSprite.Controls.Add(Me.nudChangeSprite)
        Me.fraChangeSprite.Controls.Add(Me.picChangeSprite)
        Me.fraChangeSprite.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeSprite.Location = New System.Drawing.Point(468, 323)
        Me.fraChangeSprite.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeSprite.Name = "fraChangeSprite"
        Me.fraChangeSprite.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeSprite.Size = New System.Drawing.Size(287, 135)
        Me.fraChangeSprite.TabIndex = 30
        Me.fraChangeSprite.TabStop = False
        Me.fraChangeSprite.Text = "Change Sprite"
        Me.fraChangeSprite.Visible = False
        '
        'btnChangeSpriteOk
        '
        Me.btnChangeSpriteOk.Location = New System.Drawing.Point(98, 103)
        Me.btnChangeSpriteOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeSpriteOk.Name = "btnChangeSpriteOk"
        Me.btnChangeSpriteOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeSpriteOk.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeSpriteOk.TabIndex = 30
        Me.btnChangeSpriteOk.Text = "Ok"
        '
        'btnChangeSpriteCancel
        '
        Me.btnChangeSpriteCancel.Location = New System.Drawing.Point(192, 103)
        Me.btnChangeSpriteCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeSpriteCancel.Name = "btnChangeSpriteCancel"
        Me.btnChangeSpriteCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeSpriteCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeSpriteCancel.TabIndex = 29
        Me.btnChangeSpriteCancel.Text = "Cancel"
        '
        'DarkLabel48
        '
        Me.DarkLabel48.AutoSize = True
        Me.DarkLabel48.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel48.Location = New System.Drawing.Point(93, 77)
        Me.DarkLabel48.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel48.Name = "DarkLabel48"
        Me.DarkLabel48.Size = New System.Drawing.Size(37, 15)
        Me.DarkLabel48.TabIndex = 28
        Me.DarkLabel48.Text = "Sprite"
        '
        'nudChangeSprite
        '
        Me.nudChangeSprite.Location = New System.Drawing.Point(140, 73)
        Me.nudChangeSprite.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudChangeSprite.Name = "nudChangeSprite"
        Me.nudChangeSprite.Size = New System.Drawing.Size(140, 23)
        Me.nudChangeSprite.TabIndex = 27
        '
        'picChangeSprite
        '
        Me.picChangeSprite.BackColor = System.Drawing.Color.Black
        Me.picChangeSprite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picChangeSprite.Location = New System.Drawing.Point(7, 22)
        Me.picChangeSprite.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picChangeSprite.Name = "picChangeSprite"
        Me.picChangeSprite.Size = New System.Drawing.Size(82, 107)
        Me.picChangeSprite.TabIndex = 3
        Me.picChangeSprite.TabStop = False
        '
        'fraSetSelfSwitch
        '
        Me.fraSetSelfSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetSelfSwitch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraSetSelfSwitch.Controls.Add(Me.btnSelfswitchOk)
        Me.fraSetSelfSwitch.Controls.Add(Me.btnSelfswitchCancel)
        Me.fraSetSelfSwitch.Controls.Add(Me.DarkLabel47)
        Me.fraSetSelfSwitch.Controls.Add(Me.cmbSetSelfSwitchTo)
        Me.fraSetSelfSwitch.Controls.Add(Me.DarkLabel46)
        Me.fraSetSelfSwitch.Controls.Add(Me.cmbSetSelfSwitch)
        Me.fraSetSelfSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetSelfSwitch.Location = New System.Drawing.Point(468, 208)
        Me.fraSetSelfSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetSelfSwitch.Name = "fraSetSelfSwitch"
        Me.fraSetSelfSwitch.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetSelfSwitch.Size = New System.Drawing.Size(287, 115)
        Me.fraSetSelfSwitch.TabIndex = 29
        Me.fraSetSelfSwitch.TabStop = False
        Me.fraSetSelfSwitch.Text = "Self Switches"
        Me.fraSetSelfSwitch.Visible = False
        '
        'btnSelfswitchOk
        '
        Me.btnSelfswitchOk.Location = New System.Drawing.Point(98, 84)
        Me.btnSelfswitchOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSelfswitchOk.Name = "btnSelfswitchOk"
        Me.btnSelfswitchOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSelfswitchOk.Size = New System.Drawing.Size(88, 27)
        Me.btnSelfswitchOk.TabIndex = 27
        Me.btnSelfswitchOk.Text = "Ok"
        '
        'btnSelfswitchCancel
        '
        Me.btnSelfswitchCancel.Location = New System.Drawing.Point(192, 84)
        Me.btnSelfswitchCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSelfswitchCancel.Name = "btnSelfswitchCancel"
        Me.btnSelfswitchCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSelfswitchCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnSelfswitchCancel.TabIndex = 26
        Me.btnSelfswitchCancel.Text = "Cancel"
        '
        'DarkLabel47
        '
        Me.DarkLabel47.AutoSize = True
        Me.DarkLabel47.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel47.Location = New System.Drawing.Point(7, 57)
        Me.DarkLabel47.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel47.Name = "DarkLabel47"
        Me.DarkLabel47.Size = New System.Drawing.Size(38, 15)
        Me.DarkLabel47.TabIndex = 3
        Me.DarkLabel47.Text = "Set To"
        '
        'cmbSetSelfSwitchTo
        '
        Me.cmbSetSelfSwitchTo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSetSelfSwitchTo.FormattingEnabled = True
        Me.cmbSetSelfSwitchTo.Items.AddRange(New Object() {"Off", "On"})
        Me.cmbSetSelfSwitchTo.Location = New System.Drawing.Point(84, 53)
        Me.cmbSetSelfSwitchTo.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbSetSelfSwitchTo.Name = "cmbSetSelfSwitchTo"
        Me.cmbSetSelfSwitchTo.Size = New System.Drawing.Size(195, 24)
        Me.cmbSetSelfSwitchTo.TabIndex = 2
        '
        'DarkLabel46
        '
        Me.DarkLabel46.AutoSize = True
        Me.DarkLabel46.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel46.Location = New System.Drawing.Point(7, 25)
        Me.DarkLabel46.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel46.Name = "DarkLabel46"
        Me.DarkLabel46.Size = New System.Drawing.Size(67, 15)
        Me.DarkLabel46.TabIndex = 1
        Me.DarkLabel46.Text = "Self Switch:"
        '
        'cmbSetSelfSwitch
        '
        Me.cmbSetSelfSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSetSelfSwitch.FormattingEnabled = True
        Me.cmbSetSelfSwitch.Location = New System.Drawing.Point(84, 22)
        Me.cmbSetSelfSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbSetSelfSwitch.Name = "cmbSetSelfSwitch"
        Me.cmbSetSelfSwitch.Size = New System.Drawing.Size(195, 24)
        Me.cmbSetSelfSwitch.TabIndex = 0
        '
        'fraMapTint
        '
        Me.fraMapTint.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraMapTint.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraMapTint.Controls.Add(Me.btnMapTintOk)
        Me.fraMapTint.Controls.Add(Me.btnMapTintCancel)
        Me.fraMapTint.Controls.Add(Me.DarkLabel42)
        Me.fraMapTint.Controls.Add(Me.nudMapTintData3)
        Me.fraMapTint.Controls.Add(Me.nudMapTintData2)
        Me.fraMapTint.Controls.Add(Me.DarkLabel43)
        Me.fraMapTint.Controls.Add(Me.DarkLabel44)
        Me.fraMapTint.Controls.Add(Me.nudMapTintData1)
        Me.fraMapTint.Controls.Add(Me.nudMapTintData0)
        Me.fraMapTint.Controls.Add(Me.DarkLabel45)
        Me.fraMapTint.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraMapTint.Location = New System.Drawing.Point(468, 209)
        Me.fraMapTint.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapTint.Name = "fraMapTint"
        Me.fraMapTint.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapTint.Size = New System.Drawing.Size(287, 167)
        Me.fraMapTint.TabIndex = 28
        Me.fraMapTint.TabStop = False
        Me.fraMapTint.Text = "Map Tinting"
        Me.fraMapTint.Visible = False
        '
        'btnMapTintOk
        '
        Me.btnMapTintOk.Location = New System.Drawing.Point(98, 133)
        Me.btnMapTintOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMapTintOk.Name = "btnMapTintOk"
        Me.btnMapTintOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnMapTintOk.Size = New System.Drawing.Size(88, 27)
        Me.btnMapTintOk.TabIndex = 45
        Me.btnMapTintOk.Text = "Ok"
        '
        'btnMapTintCancel
        '
        Me.btnMapTintCancel.Location = New System.Drawing.Point(192, 133)
        Me.btnMapTintCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMapTintCancel.Name = "btnMapTintCancel"
        Me.btnMapTintCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnMapTintCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnMapTintCancel.TabIndex = 44
        Me.btnMapTintCancel.Text = "Cancel"
        '
        'DarkLabel42
        '
        Me.DarkLabel42.AutoSize = True
        Me.DarkLabel42.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel42.Location = New System.Drawing.Point(6, 107)
        Me.DarkLabel42.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel42.Name = "DarkLabel42"
        Me.DarkLabel42.Size = New System.Drawing.Size(51, 15)
        Me.DarkLabel42.TabIndex = 43
        Me.DarkLabel42.Text = "Opacity:"
        '
        'nudMapTintData3
        '
        Me.nudMapTintData3.Location = New System.Drawing.Point(111, 103)
        Me.nudMapTintData3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudMapTintData3.Name = "nudMapTintData3"
        Me.nudMapTintData3.Size = New System.Drawing.Size(168, 23)
        Me.nudMapTintData3.TabIndex = 42
        '
        'nudMapTintData2
        '
        Me.nudMapTintData2.Location = New System.Drawing.Point(111, 74)
        Me.nudMapTintData2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudMapTintData2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudMapTintData2.Name = "nudMapTintData2"
        Me.nudMapTintData2.Size = New System.Drawing.Size(168, 23)
        Me.nudMapTintData2.TabIndex = 41
        '
        'DarkLabel43
        '
        Me.DarkLabel43.AutoSize = True
        Me.DarkLabel43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel43.Location = New System.Drawing.Point(6, 76)
        Me.DarkLabel43.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel43.Name = "DarkLabel43"
        Me.DarkLabel43.Size = New System.Drawing.Size(33, 15)
        Me.DarkLabel43.TabIndex = 40
        Me.DarkLabel43.Text = "Blue:"
        '
        'DarkLabel44
        '
        Me.DarkLabel44.AutoSize = True
        Me.DarkLabel44.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel44.Location = New System.Drawing.Point(5, 50)
        Me.DarkLabel44.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel44.Name = "DarkLabel44"
        Me.DarkLabel44.Size = New System.Drawing.Size(41, 15)
        Me.DarkLabel44.TabIndex = 39
        Me.DarkLabel44.Text = "Green:"
        '
        'nudMapTintData1
        '
        Me.nudMapTintData1.Location = New System.Drawing.Point(111, 45)
        Me.nudMapTintData1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudMapTintData1.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudMapTintData1.Name = "nudMapTintData1"
        Me.nudMapTintData1.Size = New System.Drawing.Size(168, 23)
        Me.nudMapTintData1.TabIndex = 38
        '
        'nudMapTintData0
        '
        Me.nudMapTintData0.Location = New System.Drawing.Point(111, 16)
        Me.nudMapTintData0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudMapTintData0.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudMapTintData0.Name = "nudMapTintData0"
        Me.nudMapTintData0.Size = New System.Drawing.Size(168, 23)
        Me.nudMapTintData0.TabIndex = 37
        '
        'DarkLabel45
        '
        Me.DarkLabel45.AutoSize = True
        Me.DarkLabel45.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel45.Location = New System.Drawing.Point(6, 18)
        Me.DarkLabel45.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel45.Name = "DarkLabel45"
        Me.DarkLabel45.Size = New System.Drawing.Size(30, 15)
        Me.DarkLabel45.TabIndex = 36
        Me.DarkLabel45.Text = "Red:"
        '
        'fraShowChatBubble
        '
        Me.fraShowChatBubble.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraShowChatBubble.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraShowChatBubble.Controls.Add(Me.btnShowChatBubbleOk)
        Me.fraShowChatBubble.Controls.Add(Me.btnShowChatBubbleCancel)
        Me.fraShowChatBubble.Controls.Add(Me.DarkLabel41)
        Me.fraShowChatBubble.Controls.Add(Me.cmbChatBubbleTarget)
        Me.fraShowChatBubble.Controls.Add(Me.cmbChatBubbleTargetType)
        Me.fraShowChatBubble.Controls.Add(Me.DarkLabel40)
        Me.fraShowChatBubble.Controls.Add(Me.txtChatbubbleText)
        Me.fraShowChatBubble.Controls.Add(Me.DarkLabel39)
        Me.fraShowChatBubble.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraShowChatBubble.Location = New System.Drawing.Point(468, 209)
        Me.fraShowChatBubble.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShowChatBubble.Name = "fraShowChatBubble"
        Me.fraShowChatBubble.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShowChatBubble.Size = New System.Drawing.Size(287, 163)
        Me.fraShowChatBubble.TabIndex = 27
        Me.fraShowChatBubble.TabStop = False
        Me.fraShowChatBubble.Text = "Show ChatBubble"
        Me.fraShowChatBubble.Visible = False
        '
        'btnShowChatBubbleOk
        '
        Me.btnShowChatBubbleOk.Location = New System.Drawing.Point(98, 129)
        Me.btnShowChatBubbleOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShowChatBubbleOk.Name = "btnShowChatBubbleOk"
        Me.btnShowChatBubbleOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnShowChatBubbleOk.Size = New System.Drawing.Size(88, 27)
        Me.btnShowChatBubbleOk.TabIndex = 31
        Me.btnShowChatBubbleOk.Text = "Ok"
        '
        'btnShowChatBubbleCancel
        '
        Me.btnShowChatBubbleCancel.Location = New System.Drawing.Point(192, 129)
        Me.btnShowChatBubbleCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShowChatBubbleCancel.Name = "btnShowChatBubbleCancel"
        Me.btnShowChatBubbleCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnShowChatBubbleCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnShowChatBubbleCancel.TabIndex = 30
        Me.btnShowChatBubbleCancel.Text = "Cancel"
        '
        'DarkLabel41
        '
        Me.DarkLabel41.AutoSize = True
        Me.DarkLabel41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel41.Location = New System.Drawing.Point(7, 102)
        Me.DarkLabel41.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel41.Name = "DarkLabel41"
        Me.DarkLabel41.Size = New System.Drawing.Size(39, 15)
        Me.DarkLabel41.TabIndex = 29
        Me.DarkLabel41.Text = "Index:"
        '
        'cmbChatBubbleTarget
        '
        Me.cmbChatBubbleTarget.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChatBubbleTarget.FormattingEnabled = True
        Me.cmbChatBubbleTarget.Location = New System.Drawing.Point(94, 98)
        Me.cmbChatBubbleTarget.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbChatBubbleTarget.Name = "cmbChatBubbleTarget"
        Me.cmbChatBubbleTarget.Size = New System.Drawing.Size(185, 24)
        Me.cmbChatBubbleTarget.TabIndex = 28
        '
        'cmbChatBubbleTargetType
        '
        Me.cmbChatBubbleTargetType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChatBubbleTargetType.FormattingEnabled = True
        Me.cmbChatBubbleTargetType.Items.AddRange(New Object() {"Player", "Npc", "Event"})
        Me.cmbChatBubbleTargetType.Location = New System.Drawing.Point(94, 67)
        Me.cmbChatBubbleTargetType.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbChatBubbleTargetType.Name = "cmbChatBubbleTargetType"
        Me.cmbChatBubbleTargetType.Size = New System.Drawing.Size(185, 24)
        Me.cmbChatBubbleTargetType.TabIndex = 27
        '
        'DarkLabel40
        '
        Me.DarkLabel40.AutoSize = True
        Me.DarkLabel40.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel40.Location = New System.Drawing.Point(7, 70)
        Me.DarkLabel40.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel40.Name = "DarkLabel40"
        Me.DarkLabel40.Size = New System.Drawing.Size(69, 15)
        Me.DarkLabel40.TabIndex = 2
        Me.DarkLabel40.Text = "Target Type:"
        '
        'txtChatbubbleText
        '
        Me.txtChatbubbleText.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtChatbubbleText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChatbubbleText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtChatbubbleText.Location = New System.Drawing.Point(7, 37)
        Me.txtChatbubbleText.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtChatbubbleText.Name = "txtChatbubbleText"
        Me.txtChatbubbleText.Size = New System.Drawing.Size(273, 23)
        Me.txtChatbubbleText.TabIndex = 1
        '
        'DarkLabel39
        '
        Me.DarkLabel39.AutoSize = True
        Me.DarkLabel39.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel39.Location = New System.Drawing.Point(7, 18)
        Me.DarkLabel39.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel39.Name = "DarkLabel39"
        Me.DarkLabel39.Size = New System.Drawing.Size(93, 15)
        Me.DarkLabel39.TabIndex = 0
        Me.DarkLabel39.Text = "ChatBubble Text"
        '
        'fraPlaySound
        '
        Me.fraPlaySound.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlaySound.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraPlaySound.Controls.Add(Me.btnPlaySoundOk)
        Me.fraPlaySound.Controls.Add(Me.btnPlaySoundCancel)
        Me.fraPlaySound.Controls.Add(Me.cmbPlaySound)
        Me.fraPlaySound.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlaySound.Location = New System.Drawing.Point(468, 207)
        Me.fraPlaySound.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlaySound.Name = "fraPlaySound"
        Me.fraPlaySound.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlaySound.Size = New System.Drawing.Size(287, 88)
        Me.fraPlaySound.TabIndex = 26
        Me.fraPlaySound.TabStop = False
        Me.fraPlaySound.Text = "Play Sound"
        Me.fraPlaySound.Visible = False
        '
        'btnPlaySoundOk
        '
        Me.btnPlaySoundOk.Location = New System.Drawing.Point(98, 53)
        Me.btnPlaySoundOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlaySoundOk.Name = "btnPlaySoundOk"
        Me.btnPlaySoundOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlaySoundOk.Size = New System.Drawing.Size(88, 27)
        Me.btnPlaySoundOk.TabIndex = 27
        Me.btnPlaySoundOk.Text = "Ok"
        '
        'btnPlaySoundCancel
        '
        Me.btnPlaySoundCancel.Location = New System.Drawing.Point(192, 53)
        Me.btnPlaySoundCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlaySoundCancel.Name = "btnPlaySoundCancel"
        Me.btnPlaySoundCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlaySoundCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnPlaySoundCancel.TabIndex = 26
        Me.btnPlaySoundCancel.Text = "Cancel"
        '
        'cmbPlaySound
        '
        Me.cmbPlaySound.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlaySound.FormattingEnabled = True
        Me.cmbPlaySound.Location = New System.Drawing.Point(7, 22)
        Me.cmbPlaySound.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPlaySound.Name = "cmbPlaySound"
        Me.cmbPlaySound.Size = New System.Drawing.Size(272, 24)
        Me.cmbPlaySound.TabIndex = 0
        '
        'fraChangePK
        '
        Me.fraChangePK.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangePK.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraChangePK.Controls.Add(Me.btnChangePkOk)
        Me.fraChangePK.Controls.Add(Me.btnChangePkCancel)
        Me.fraChangePK.Controls.Add(Me.cmbSetPK)
        Me.fraChangePK.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangePK.Location = New System.Drawing.Point(468, 120)
        Me.fraChangePK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangePK.Name = "fraChangePK"
        Me.fraChangePK.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangePK.Size = New System.Drawing.Size(287, 87)
        Me.fraChangePK.TabIndex = 25
        Me.fraChangePK.TabStop = False
        Me.fraChangePK.Text = "Set Player PK"
        Me.fraChangePK.Visible = False
        '
        'btnChangePkOk
        '
        Me.btnChangePkOk.Location = New System.Drawing.Point(93, 53)
        Me.btnChangePkOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangePkOk.Name = "btnChangePkOk"
        Me.btnChangePkOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangePkOk.Size = New System.Drawing.Size(88, 27)
        Me.btnChangePkOk.TabIndex = 27
        Me.btnChangePkOk.Text = "Ok"
        '
        'btnChangePkCancel
        '
        Me.btnChangePkCancel.Location = New System.Drawing.Point(188, 53)
        Me.btnChangePkCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangePkCancel.Name = "btnChangePkCancel"
        Me.btnChangePkCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangePkCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnChangePkCancel.TabIndex = 26
        Me.btnChangePkCancel.Text = "Cancel"
        '
        'cmbSetPK
        '
        Me.cmbSetPK.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSetPK.FormattingEnabled = True
        Me.cmbSetPK.Items.AddRange(New Object() {"No", "Yes"})
        Me.cmbSetPK.Location = New System.Drawing.Point(12, 22)
        Me.cmbSetPK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbSetPK.Name = "cmbSetPK"
        Me.cmbSetPK.Size = New System.Drawing.Size(263, 24)
        Me.cmbSetPK.TabIndex = 18
        '
        'fraCreateLabel
        '
        Me.fraCreateLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraCreateLabel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraCreateLabel.Controls.Add(Me.btnCreatelabelOk)
        Me.fraCreateLabel.Controls.Add(Me.btnCreatelabelCancel)
        Me.fraCreateLabel.Controls.Add(Me.txtLabelName)
        Me.fraCreateLabel.Controls.Add(Me.lblLabelName)
        Me.fraCreateLabel.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraCreateLabel.Location = New System.Drawing.Point(468, 152)
        Me.fraCreateLabel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraCreateLabel.Name = "fraCreateLabel"
        Me.fraCreateLabel.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraCreateLabel.Size = New System.Drawing.Size(287, 85)
        Me.fraCreateLabel.TabIndex = 24
        Me.fraCreateLabel.TabStop = False
        Me.fraCreateLabel.Text = "Create Label"
        Me.fraCreateLabel.Visible = False
        '
        'btnCreatelabelOk
        '
        Me.btnCreatelabelOk.Location = New System.Drawing.Point(98, 52)
        Me.btnCreatelabelOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCreatelabelOk.Name = "btnCreatelabelOk"
        Me.btnCreatelabelOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCreatelabelOk.Size = New System.Drawing.Size(88, 27)
        Me.btnCreatelabelOk.TabIndex = 27
        Me.btnCreatelabelOk.Text = "Ok"
        '
        'btnCreatelabelCancel
        '
        Me.btnCreatelabelCancel.Location = New System.Drawing.Point(192, 52)
        Me.btnCreatelabelCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCreatelabelCancel.Name = "btnCreatelabelCancel"
        Me.btnCreatelabelCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnCreatelabelCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnCreatelabelCancel.TabIndex = 26
        Me.btnCreatelabelCancel.Text = "Cancel"
        '
        'txtLabelName
        '
        Me.txtLabelName.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtLabelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabelName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtLabelName.Location = New System.Drawing.Point(93, 22)
        Me.txtLabelName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtLabelName.Name = "txtLabelName"
        Me.txtLabelName.Size = New System.Drawing.Size(186, 23)
        Me.txtLabelName.TabIndex = 1
        '
        'lblLabelName
        '
        Me.lblLabelName.AutoSize = True
        Me.lblLabelName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.lblLabelName.Location = New System.Drawing.Point(8, 24)
        Me.lblLabelName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLabelName.Name = "lblLabelName"
        Me.lblLabelName.Size = New System.Drawing.Size(73, 15)
        Me.lblLabelName.TabIndex = 0
        Me.lblLabelName.Text = "Label Name:"
        '
        'fraChangeJob
        '
        Me.fraChangeJob.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeJob.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraChangeJob.Controls.Add(Me.btnChangeJobOk)
        Me.fraChangeJob.Controls.Add(Me.btnChangeJobCancel)
        Me.fraChangeJob.Controls.Add(Me.cmbChangeJob)
        Me.fraChangeJob.Controls.Add(Me.DarkLabel38)
        Me.fraChangeJob.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeJob.Location = New System.Drawing.Point(468, 126)
        Me.fraChangeJob.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeJob.Name = "fraChangeJob"
        Me.fraChangeJob.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeJob.Size = New System.Drawing.Size(287, 88)
        Me.fraChangeJob.TabIndex = 23
        Me.fraChangeJob.TabStop = False
        Me.fraChangeJob.Text = "Change Player Job"
        Me.fraChangeJob.Visible = False
        '
        'btnChangeJobOk
        '
        Me.btnChangeJobOk.Location = New System.Drawing.Point(98, 53)
        Me.btnChangeJobOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeJobOk.Name = "btnChangeJobOk"
        Me.btnChangeJobOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeJobOk.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeJobOk.TabIndex = 27
        Me.btnChangeJobOk.Text = "Ok"
        '
        'btnChangeJobCancel
        '
        Me.btnChangeJobCancel.Location = New System.Drawing.Point(192, 53)
        Me.btnChangeJobCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeJobCancel.Name = "btnChangeJobCancel"
        Me.btnChangeJobCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeJobCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeJobCancel.TabIndex = 26
        Me.btnChangeJobCancel.Text = "Cancel"
        '
        'cmbChangeJob
        '
        Me.cmbChangeJob.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChangeJob.FormattingEnabled = True
        Me.cmbChangeJob.Location = New System.Drawing.Point(57, 22)
        Me.cmbChangeJob.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbChangeJob.Name = "cmbChangeJob"
        Me.cmbChangeJob.Size = New System.Drawing.Size(222, 24)
        Me.cmbChangeJob.TabIndex = 1
        '
        'DarkLabel38
        '
        Me.DarkLabel38.AutoSize = True
        Me.DarkLabel38.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel38.Location = New System.Drawing.Point(9, 25)
        Me.DarkLabel38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel38.Name = "DarkLabel38"
        Me.DarkLabel38.Size = New System.Drawing.Size(28, 15)
        Me.DarkLabel38.TabIndex = 0
        Me.DarkLabel38.Text = "Job:"
        '
        'fraChangeSkills
        '
        Me.fraChangeSkills.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeSkills.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraChangeSkills.Controls.Add(Me.btnChangeSkillsOk)
        Me.fraChangeSkills.Controls.Add(Me.btnChangeSkillsCancel)
        Me.fraChangeSkills.Controls.Add(Me.optChangeSkillsRemove)
        Me.fraChangeSkills.Controls.Add(Me.optChangeSkillsAdd)
        Me.fraChangeSkills.Controls.Add(Me.cmbChangeSkills)
        Me.fraChangeSkills.Controls.Add(Me.DarkLabel37)
        Me.fraChangeSkills.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeSkills.Location = New System.Drawing.Point(468, 125)
        Me.fraChangeSkills.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeSkills.Name = "fraChangeSkills"
        Me.fraChangeSkills.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeSkills.Size = New System.Drawing.Size(287, 113)
        Me.fraChangeSkills.TabIndex = 22
        Me.fraChangeSkills.TabStop = False
        Me.fraChangeSkills.Text = "Change Player Skills"
        Me.fraChangeSkills.Visible = False
        '
        'btnChangeSkillsOk
        '
        Me.btnChangeSkillsOk.Location = New System.Drawing.Point(98, 77)
        Me.btnChangeSkillsOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeSkillsOk.Name = "btnChangeSkillsOk"
        Me.btnChangeSkillsOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeSkillsOk.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeSkillsOk.TabIndex = 27
        Me.btnChangeSkillsOk.Text = "Ok"
        '
        'btnChangeSkillsCancel
        '
        Me.btnChangeSkillsCancel.Location = New System.Drawing.Point(192, 77)
        Me.btnChangeSkillsCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeSkillsCancel.Name = "btnChangeSkillsCancel"
        Me.btnChangeSkillsCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeSkillsCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeSkillsCancel.TabIndex = 26
        Me.btnChangeSkillsCancel.Text = "Cancel"
        '
        'optChangeSkillsRemove
        '
        Me.optChangeSkillsRemove.AutoSize = True
        Me.optChangeSkillsRemove.Location = New System.Drawing.Point(172, 51)
        Me.optChangeSkillsRemove.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optChangeSkillsRemove.Name = "optChangeSkillsRemove"
        Me.optChangeSkillsRemove.Size = New System.Drawing.Size(59, 19)
        Me.optChangeSkillsRemove.TabIndex = 3
        Me.optChangeSkillsRemove.TabStop = True
        Me.optChangeSkillsRemove.Text = "Forget"
        '
        'optChangeSkillsAdd
        '
        Me.optChangeSkillsAdd.AutoSize = True
        Me.optChangeSkillsAdd.Location = New System.Drawing.Point(76, 51)
        Me.optChangeSkillsAdd.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optChangeSkillsAdd.Name = "optChangeSkillsAdd"
        Me.optChangeSkillsAdd.Size = New System.Drawing.Size(55, 19)
        Me.optChangeSkillsAdd.TabIndex = 2
        Me.optChangeSkillsAdd.TabStop = True
        Me.optChangeSkillsAdd.Text = "Teach"
        '
        'cmbChangeSkills
        '
        Me.cmbChangeSkills.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChangeSkills.FormattingEnabled = True
        Me.cmbChangeSkills.Location = New System.Drawing.Point(48, 20)
        Me.cmbChangeSkills.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbChangeSkills.Name = "cmbChangeSkills"
        Me.cmbChangeSkills.Size = New System.Drawing.Size(230, 24)
        Me.cmbChangeSkills.TabIndex = 1
        '
        'DarkLabel37
        '
        Me.DarkLabel37.AutoSize = True
        Me.DarkLabel37.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel37.Location = New System.Drawing.Point(7, 23)
        Me.DarkLabel37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel37.Name = "DarkLabel37"
        Me.DarkLabel37.Size = New System.Drawing.Size(31, 15)
        Me.DarkLabel37.TabIndex = 0
        Me.DarkLabel37.Text = "Skill:"
        '
        'fraPlayerWarp
        '
        Me.fraPlayerWarp.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayerWarp.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraPlayerWarp.Controls.Add(Me.btnPlayerWarpOk)
        Me.fraPlayerWarp.Controls.Add(Me.btnPlayerWarpCancel)
        Me.fraPlayerWarp.Controls.Add(Me.DarkLabel31)
        Me.fraPlayerWarp.Controls.Add(Me.cmbWarpPlayerDir)
        Me.fraPlayerWarp.Controls.Add(Me.nudWPY)
        Me.fraPlayerWarp.Controls.Add(Me.DarkLabel32)
        Me.fraPlayerWarp.Controls.Add(Me.nudWPX)
        Me.fraPlayerWarp.Controls.Add(Me.DarkLabel33)
        Me.fraPlayerWarp.Controls.Add(Me.nudWPMap)
        Me.fraPlayerWarp.Controls.Add(Me.DarkLabel34)
        Me.fraPlayerWarp.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayerWarp.Location = New System.Drawing.Point(468, 7)
        Me.fraPlayerWarp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayerWarp.Name = "fraPlayerWarp"
        Me.fraPlayerWarp.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayerWarp.Size = New System.Drawing.Size(287, 112)
        Me.fraPlayerWarp.TabIndex = 19
        Me.fraPlayerWarp.TabStop = False
        Me.fraPlayerWarp.Text = "Warp Player"
        Me.fraPlayerWarp.Visible = False
        '
        'btnPlayerWarpOk
        '
        Me.btnPlayerWarpOk.Location = New System.Drawing.Point(97, 78)
        Me.btnPlayerWarpOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlayerWarpOk.Name = "btnPlayerWarpOk"
        Me.btnPlayerWarpOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlayerWarpOk.Size = New System.Drawing.Size(88, 27)
        Me.btnPlayerWarpOk.TabIndex = 46
        Me.btnPlayerWarpOk.Text = "Ok"
        '
        'btnPlayerWarpCancel
        '
        Me.btnPlayerWarpCancel.Location = New System.Drawing.Point(191, 78)
        Me.btnPlayerWarpCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlayerWarpCancel.Name = "btnPlayerWarpCancel"
        Me.btnPlayerWarpCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlayerWarpCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnPlayerWarpCancel.TabIndex = 45
        Me.btnPlayerWarpCancel.Text = "Cancel"
        '
        'DarkLabel31
        '
        Me.DarkLabel31.AutoSize = True
        Me.DarkLabel31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel31.Location = New System.Drawing.Point(9, 51)
        Me.DarkLabel31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel31.Name = "DarkLabel31"
        Me.DarkLabel31.Size = New System.Drawing.Size(58, 15)
        Me.DarkLabel31.TabIndex = 44
        Me.DarkLabel31.Text = "Direction:"
        '
        'cmbWarpPlayerDir
        '
        Me.cmbWarpPlayerDir.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbWarpPlayerDir.FormattingEnabled = True
        Me.cmbWarpPlayerDir.Items.AddRange(New Object() {"Retain Direction", "Up", "Down", "Left", "Right"})
        Me.cmbWarpPlayerDir.Location = New System.Drawing.Point(112, 47)
        Me.cmbWarpPlayerDir.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbWarpPlayerDir.Name = "cmbWarpPlayerDir"
        Me.cmbWarpPlayerDir.Size = New System.Drawing.Size(166, 24)
        Me.cmbWarpPlayerDir.TabIndex = 43
        '
        'nudWPY
        '
        Me.nudWPY.Location = New System.Drawing.Point(233, 17)
        Me.nudWPY.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudWPY.Name = "nudWPY"
        Me.nudWPY.Size = New System.Drawing.Size(46, 23)
        Me.nudWPY.TabIndex = 42
        '
        'DarkLabel32
        '
        Me.DarkLabel32.AutoSize = True
        Me.DarkLabel32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel32.Location = New System.Drawing.Point(206, 20)
        Me.DarkLabel32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel32.Name = "DarkLabel32"
        Me.DarkLabel32.Size = New System.Drawing.Size(17, 15)
        Me.DarkLabel32.TabIndex = 41
        Me.DarkLabel32.Text = "Y:"
        '
        'nudWPX
        '
        Me.nudWPX.Location = New System.Drawing.Point(152, 17)
        Me.nudWPX.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudWPX.Name = "nudWPX"
        Me.nudWPX.Size = New System.Drawing.Size(46, 23)
        Me.nudWPX.TabIndex = 40
        '
        'DarkLabel33
        '
        Me.DarkLabel33.AutoSize = True
        Me.DarkLabel33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel33.Location = New System.Drawing.Point(125, 20)
        Me.DarkLabel33.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel33.Name = "DarkLabel33"
        Me.DarkLabel33.Size = New System.Drawing.Size(17, 15)
        Me.DarkLabel33.TabIndex = 39
        Me.DarkLabel33.Text = "X:"
        '
        'nudWPMap
        '
        Me.nudWPMap.Location = New System.Drawing.Point(50, 17)
        Me.nudWPMap.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudWPMap.Name = "nudWPMap"
        Me.nudWPMap.Size = New System.Drawing.Size(68, 23)
        Me.nudWPMap.TabIndex = 38
        '
        'DarkLabel34
        '
        Me.DarkLabel34.AutoSize = True
        Me.DarkLabel34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel34.Location = New System.Drawing.Point(7, 20)
        Me.DarkLabel34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel34.Name = "DarkLabel34"
        Me.DarkLabel34.Size = New System.Drawing.Size(34, 15)
        Me.DarkLabel34.TabIndex = 37
        Me.DarkLabel34.Text = "Map:"
        '
        'fraSetFog
        '
        Me.fraSetFog.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetFog.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraSetFog.Controls.Add(Me.btnSetFogOk)
        Me.fraSetFog.Controls.Add(Me.btnSetFogCancel)
        Me.fraSetFog.Controls.Add(Me.DarkLabel30)
        Me.fraSetFog.Controls.Add(Me.DarkLabel29)
        Me.fraSetFog.Controls.Add(Me.DarkLabel28)
        Me.fraSetFog.Controls.Add(Me.nudFogData2)
        Me.fraSetFog.Controls.Add(Me.nudFogData1)
        Me.fraSetFog.Controls.Add(Me.nudFogData0)
        Me.fraSetFog.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetFog.Location = New System.Drawing.Point(468, 8)
        Me.fraSetFog.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetFog.Name = "fraSetFog"
        Me.fraSetFog.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetFog.Size = New System.Drawing.Size(287, 111)
        Me.fraSetFog.TabIndex = 18
        Me.fraSetFog.TabStop = False
        Me.fraSetFog.Text = "Set Fog"
        Me.fraSetFog.Visible = False
        '
        'btnSetFogOk
        '
        Me.btnSetFogOk.Location = New System.Drawing.Point(98, 77)
        Me.btnSetFogOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetFogOk.Name = "btnSetFogOk"
        Me.btnSetFogOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetFogOk.Size = New System.Drawing.Size(88, 27)
        Me.btnSetFogOk.TabIndex = 41
        Me.btnSetFogOk.Text = "Ok"
        '
        'btnSetFogCancel
        '
        Me.btnSetFogCancel.Location = New System.Drawing.Point(192, 77)
        Me.btnSetFogCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetFogCancel.Name = "btnSetFogCancel"
        Me.btnSetFogCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetFogCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnSetFogCancel.TabIndex = 40
        Me.btnSetFogCancel.Text = "Cancel"
        '
        'DarkLabel30
        '
        Me.DarkLabel30.AutoSize = True
        Me.DarkLabel30.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel30.Location = New System.Drawing.Point(145, 48)
        Me.DarkLabel30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel30.Name = "DarkLabel30"
        Me.DarkLabel30.Size = New System.Drawing.Size(74, 15)
        Me.DarkLabel30.TabIndex = 39
        Me.DarkLabel30.Text = "Fog Opacity:"
        '
        'DarkLabel29
        '
        Me.DarkLabel29.AutoSize = True
        Me.DarkLabel29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel29.Location = New System.Drawing.Point(8, 48)
        Me.DarkLabel29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel29.Name = "DarkLabel29"
        Me.DarkLabel29.Size = New System.Drawing.Size(65, 15)
        Me.DarkLabel29.TabIndex = 38
        Me.DarkLabel29.Text = "Fog Speed:"
        '
        'DarkLabel28
        '
        Me.DarkLabel28.AutoSize = True
        Me.DarkLabel28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel28.Location = New System.Drawing.Point(8, 17)
        Me.DarkLabel28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel28.Name = "DarkLabel28"
        Me.DarkLabel28.Size = New System.Drawing.Size(30, 15)
        Me.DarkLabel28.TabIndex = 37
        Me.DarkLabel28.Text = "Fog:"
        '
        'nudFogData2
        '
        Me.nudFogData2.Location = New System.Drawing.Point(223, 45)
        Me.nudFogData2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudFogData2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudFogData2.Name = "nudFogData2"
        Me.nudFogData2.Size = New System.Drawing.Size(57, 23)
        Me.nudFogData2.TabIndex = 36
        '
        'nudFogData1
        '
        Me.nudFogData1.Location = New System.Drawing.Point(84, 46)
        Me.nudFogData1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudFogData1.Name = "nudFogData1"
        Me.nudFogData1.Size = New System.Drawing.Size(56, 23)
        Me.nudFogData1.TabIndex = 35
        '
        'nudFogData0
        '
        Me.nudFogData0.Location = New System.Drawing.Point(113, 14)
        Me.nudFogData0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudFogData0.Name = "nudFogData0"
        Me.nudFogData0.Size = New System.Drawing.Size(167, 23)
        Me.nudFogData0.TabIndex = 34
        '
        'fraShowText
        '
        Me.fraShowText.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraShowText.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraShowText.Controls.Add(Me.DarkLabel27)
        Me.fraShowText.Controls.Add(Me.txtShowText)
        Me.fraShowText.Controls.Add(Me.btnShowTextCancel)
        Me.fraShowText.Controls.Add(Me.btnShowTextOk)
        Me.fraShowText.Controls.Add(Me.picShowTextFace)
        Me.fraShowText.Controls.Add(Me.DarkLabel26)
        Me.fraShowText.Controls.Add(Me.nudShowTextFace)
        Me.fraShowText.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraShowText.Location = New System.Drawing.Point(7, 351)
        Me.fraShowText.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShowText.Name = "fraShowText"
        Me.fraShowText.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShowText.Size = New System.Drawing.Size(289, 328)
        Me.fraShowText.TabIndex = 17
        Me.fraShowText.TabStop = False
        Me.fraShowText.Text = "Show Text"
        Me.fraShowText.Visible = False
        '
        'DarkLabel27
        '
        Me.DarkLabel27.AutoSize = True
        Me.DarkLabel27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel27.Location = New System.Drawing.Point(8, 22)
        Me.DarkLabel27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel27.Name = "DarkLabel27"
        Me.DarkLabel27.Size = New System.Drawing.Size(28, 15)
        Me.DarkLabel27.TabIndex = 26
        Me.DarkLabel27.Text = "Text"
        '
        'txtShowText
        '
        Me.txtShowText.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtShowText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShowText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtShowText.Location = New System.Drawing.Point(10, 44)
        Me.txtShowText.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtShowText.Multiline = True
        Me.txtShowText.Name = "txtShowText"
        Me.txtShowText.Size = New System.Drawing.Size(266, 121)
        Me.txtShowText.TabIndex = 21
        '
        'btnShowTextCancel
        '
        Me.btnShowTextCancel.Location = New System.Drawing.Point(195, 291)
        Me.btnShowTextCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShowTextCancel.Name = "btnShowTextCancel"
        Me.btnShowTextCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnShowTextCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnShowTextCancel.TabIndex = 24
        Me.btnShowTextCancel.Text = "Cancel"
        '
        'btnShowTextOk
        '
        Me.btnShowTextOk.Location = New System.Drawing.Point(100, 291)
        Me.btnShowTextOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShowTextOk.Name = "btnShowTextOk"
        Me.btnShowTextOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnShowTextOk.Size = New System.Drawing.Size(88, 27)
        Me.btnShowTextOk.TabIndex = 25
        Me.btnShowTextOk.Text = "Ok"
        '
        'picShowTextFace
        '
        Me.picShowTextFace.BackColor = System.Drawing.Color.Black
        Me.picShowTextFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picShowTextFace.Location = New System.Drawing.Point(8, 172)
        Me.picShowTextFace.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picShowTextFace.Name = "picShowTextFace"
        Me.picShowTextFace.Size = New System.Drawing.Size(117, 107)
        Me.picShowTextFace.TabIndex = 2
        Me.picShowTextFace.TabStop = False
        '
        'DarkLabel26
        '
        Me.DarkLabel26.AutoSize = True
        Me.DarkLabel26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel26.Location = New System.Drawing.Point(128, 258)
        Me.DarkLabel26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel26.Name = "DarkLabel26"
        Me.DarkLabel26.Size = New System.Drawing.Size(34, 15)
        Me.DarkLabel26.TabIndex = 22
        Me.DarkLabel26.Text = "Face:"
        '
        'nudShowTextFace
        '
        Me.nudShowTextFace.Location = New System.Drawing.Point(172, 256)
        Me.nudShowTextFace.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudShowTextFace.Name = "nudShowTextFace"
        Me.nudShowTextFace.Size = New System.Drawing.Size(107, 23)
        Me.nudShowTextFace.TabIndex = 23
        '
        'fraAddText
        '
        Me.fraAddText.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraAddText.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraAddText.Controls.Add(Me.btnAddTextOk)
        Me.fraAddText.Controls.Add(Me.btnAddTextCancel)
        Me.fraAddText.Controls.Add(Me.optAddText_Global)
        Me.fraAddText.Controls.Add(Me.optAddText_Map)
        Me.fraAddText.Controls.Add(Me.optAddText_Player)
        Me.fraAddText.Controls.Add(Me.DarkLabel25)
        Me.fraAddText.Controls.Add(Me.txtAddText_Text)
        Me.fraAddText.Controls.Add(Me.DarkLabel24)
        Me.fraAddText.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraAddText.Location = New System.Drawing.Point(7, 419)
        Me.fraAddText.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraAddText.Name = "fraAddText"
        Me.fraAddText.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraAddText.Size = New System.Drawing.Size(272, 216)
        Me.fraAddText.TabIndex = 3
        Me.fraAddText.TabStop = False
        Me.fraAddText.Text = "Add Text"
        Me.fraAddText.Visible = False
        '
        'btnAddTextOk
        '
        Me.btnAddTextOk.Location = New System.Drawing.Point(64, 180)
        Me.btnAddTextOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAddTextOk.Name = "btnAddTextOk"
        Me.btnAddTextOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnAddTextOk.Size = New System.Drawing.Size(88, 27)
        Me.btnAddTextOk.TabIndex = 9
        Me.btnAddTextOk.Text = "Ok"
        '
        'btnAddTextCancel
        '
        Me.btnAddTextCancel.Location = New System.Drawing.Point(159, 180)
        Me.btnAddTextCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAddTextCancel.Name = "btnAddTextCancel"
        Me.btnAddTextCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnAddTextCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnAddTextCancel.TabIndex = 8
        Me.btnAddTextCancel.Text = "Cancel"
        '
        'optAddText_Global
        '
        Me.optAddText_Global.AutoSize = True
        Me.optAddText_Global.Location = New System.Drawing.Point(202, 153)
        Me.optAddText_Global.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optAddText_Global.Name = "optAddText_Global"
        Me.optAddText_Global.Size = New System.Drawing.Size(59, 19)
        Me.optAddText_Global.TabIndex = 5
        Me.optAddText_Global.TabStop = True
        Me.optAddText_Global.Text = "Global"
        '
        'optAddText_Map
        '
        Me.optAddText_Map.AutoSize = True
        Me.optAddText_Map.Location = New System.Drawing.Point(141, 153)
        Me.optAddText_Map.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optAddText_Map.Name = "optAddText_Map"
        Me.optAddText_Map.Size = New System.Drawing.Size(49, 19)
        Me.optAddText_Map.TabIndex = 4
        Me.optAddText_Map.TabStop = True
        Me.optAddText_Map.Text = "Map"
        '
        'optAddText_Player
        '
        Me.optAddText_Player.AutoSize = True
        Me.optAddText_Player.Location = New System.Drawing.Point(71, 153)
        Me.optAddText_Player.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optAddText_Player.Name = "optAddText_Player"
        Me.optAddText_Player.Size = New System.Drawing.Size(57, 19)
        Me.optAddText_Player.TabIndex = 3
        Me.optAddText_Player.TabStop = True
        Me.optAddText_Player.Text = "Player"
        '
        'DarkLabel25
        '
        Me.DarkLabel25.AutoSize = True
        Me.DarkLabel25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel25.Location = New System.Drawing.Point(7, 156)
        Me.DarkLabel25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel25.Name = "DarkLabel25"
        Me.DarkLabel25.Size = New System.Drawing.Size(54, 15)
        Me.DarkLabel25.TabIndex = 2
        Me.DarkLabel25.Text = "Channel:"
        '
        'txtAddText_Text
        '
        Me.txtAddText_Text.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtAddText_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddText_Text.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.txtAddText_Text.Location = New System.Drawing.Point(7, 36)
        Me.txtAddText_Text.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtAddText_Text.Multiline = True
        Me.txtAddText_Text.Name = "txtAddText_Text"
        Me.txtAddText_Text.Size = New System.Drawing.Size(259, 110)
        Me.txtAddText_Text.TabIndex = 1
        '
        'DarkLabel24
        '
        Me.DarkLabel24.AutoSize = True
        Me.DarkLabel24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel24.Location = New System.Drawing.Point(7, 17)
        Me.DarkLabel24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel24.Name = "DarkLabel24"
        Me.DarkLabel24.Size = New System.Drawing.Size(28, 15)
        Me.DarkLabel24.TabIndex = 0
        Me.DarkLabel24.Text = "Text"
        '
        'fraPlayerSwitch
        '
        Me.fraPlayerSwitch.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayerSwitch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraPlayerSwitch.Controls.Add(Me.btnSetPlayerSwitchOk)
        Me.fraPlayerSwitch.Controls.Add(Me.btnSetPlayerswitchCancel)
        Me.fraPlayerSwitch.Controls.Add(Me.cmbPlayerSwitchSet)
        Me.fraPlayerSwitch.Controls.Add(Me.DarkLabel23)
        Me.fraPlayerSwitch.Controls.Add(Me.cmbSwitch)
        Me.fraPlayerSwitch.Controls.Add(Me.DarkLabel22)
        Me.fraPlayerSwitch.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayerSwitch.Location = New System.Drawing.Point(248, 450)
        Me.fraPlayerSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayerSwitch.Name = "fraPlayerSwitch"
        Me.fraPlayerSwitch.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayerSwitch.Size = New System.Drawing.Size(212, 115)
        Me.fraPlayerSwitch.TabIndex = 2
        Me.fraPlayerSwitch.TabStop = False
        Me.fraPlayerSwitch.Text = "Change Items"
        Me.fraPlayerSwitch.Visible = False
        '
        'btnSetPlayerSwitchOk
        '
        Me.btnSetPlayerSwitchOk.Location = New System.Drawing.Point(23, 83)
        Me.btnSetPlayerSwitchOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetPlayerSwitchOk.Name = "btnSetPlayerSwitchOk"
        Me.btnSetPlayerSwitchOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetPlayerSwitchOk.Size = New System.Drawing.Size(88, 27)
        Me.btnSetPlayerSwitchOk.TabIndex = 9
        Me.btnSetPlayerSwitchOk.Text = "Ok"
        '
        'btnSetPlayerswitchCancel
        '
        Me.btnSetPlayerswitchCancel.Location = New System.Drawing.Point(118, 83)
        Me.btnSetPlayerswitchCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetPlayerswitchCancel.Name = "btnSetPlayerswitchCancel"
        Me.btnSetPlayerswitchCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetPlayerswitchCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnSetPlayerswitchCancel.TabIndex = 8
        Me.btnSetPlayerswitchCancel.Text = "Cancel"
        '
        'cmbPlayerSwitchSet
        '
        Me.cmbPlayerSwitchSet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayerSwitchSet.FormattingEnabled = True
        Me.cmbPlayerSwitchSet.Items.AddRange(New Object() {"False", "True"})
        Me.cmbPlayerSwitchSet.Location = New System.Drawing.Point(59, 47)
        Me.cmbPlayerSwitchSet.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPlayerSwitchSet.Name = "cmbPlayerSwitchSet"
        Me.cmbPlayerSwitchSet.Size = New System.Drawing.Size(145, 24)
        Me.cmbPlayerSwitchSet.TabIndex = 3
        '
        'DarkLabel23
        '
        Me.DarkLabel23.AutoSize = True
        Me.DarkLabel23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel23.Location = New System.Drawing.Point(7, 53)
        Me.DarkLabel23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel23.Name = "DarkLabel23"
        Me.DarkLabel23.Size = New System.Drawing.Size(37, 15)
        Me.DarkLabel23.TabIndex = 2
        Me.DarkLabel23.Text = "Set to"
        '
        'cmbSwitch
        '
        Me.cmbSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSwitch.FormattingEnabled = True
        Me.cmbSwitch.Location = New System.Drawing.Point(59, 15)
        Me.cmbSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbSwitch.Name = "cmbSwitch"
        Me.cmbSwitch.Size = New System.Drawing.Size(145, 24)
        Me.cmbSwitch.TabIndex = 1
        '
        'DarkLabel22
        '
        Me.DarkLabel22.AutoSize = True
        Me.DarkLabel22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel22.Location = New System.Drawing.Point(7, 18)
        Me.DarkLabel22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel22.Name = "DarkLabel22"
        Me.DarkLabel22.Size = New System.Drawing.Size(42, 15)
        Me.DarkLabel22.TabIndex = 0
        Me.DarkLabel22.Text = "Switch"
        '
        'fraChangeItems
        '
        Me.fraChangeItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraChangeItems.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraChangeItems.Controls.Add(Me.btnChangeItemsOk)
        Me.fraChangeItems.Controls.Add(Me.btnChangeItemsCancel)
        Me.fraChangeItems.Controls.Add(Me.nudChangeItemsAmount)
        Me.fraChangeItems.Controls.Add(Me.optChangeItemRemove)
        Me.fraChangeItems.Controls.Add(Me.optChangeItemAdd)
        Me.fraChangeItems.Controls.Add(Me.optChangeItemSet)
        Me.fraChangeItems.Controls.Add(Me.cmbChangeItemIndex)
        Me.fraChangeItems.Controls.Add(Me.DarkLabel21)
        Me.fraChangeItems.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraChangeItems.Location = New System.Drawing.Point(7, 450)
        Me.fraChangeItems.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeItems.Name = "fraChangeItems"
        Me.fraChangeItems.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraChangeItems.Size = New System.Drawing.Size(218, 138)
        Me.fraChangeItems.TabIndex = 1
        Me.fraChangeItems.TabStop = False
        Me.fraChangeItems.Text = "Change Items"
        Me.fraChangeItems.Visible = False
        '
        'btnChangeItemsOk
        '
        Me.btnChangeItemsOk.Location = New System.Drawing.Point(29, 105)
        Me.btnChangeItemsOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeItemsOk.Name = "btnChangeItemsOk"
        Me.btnChangeItemsOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeItemsOk.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeItemsOk.TabIndex = 7
        Me.btnChangeItemsOk.Text = "Ok"
        '
        'btnChangeItemsCancel
        '
        Me.btnChangeItemsCancel.Location = New System.Drawing.Point(124, 105)
        Me.btnChangeItemsCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnChangeItemsCancel.Name = "btnChangeItemsCancel"
        Me.btnChangeItemsCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnChangeItemsCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnChangeItemsCancel.TabIndex = 6
        Me.btnChangeItemsCancel.Text = "Cancel"
        '
        'nudChangeItemsAmount
        '
        Me.nudChangeItemsAmount.Location = New System.Drawing.Point(10, 75)
        Me.nudChangeItemsAmount.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudChangeItemsAmount.Name = "nudChangeItemsAmount"
        Me.nudChangeItemsAmount.Size = New System.Drawing.Size(201, 23)
        Me.nudChangeItemsAmount.TabIndex = 5
        '
        'optChangeItemRemove
        '
        Me.optChangeItemRemove.AutoSize = True
        Me.optChangeItemRemove.Location = New System.Drawing.Point(141, 48)
        Me.optChangeItemRemove.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optChangeItemRemove.Name = "optChangeItemRemove"
        Me.optChangeItemRemove.Size = New System.Drawing.Size(48, 19)
        Me.optChangeItemRemove.TabIndex = 4
        Me.optChangeItemRemove.TabStop = True
        Me.optChangeItemRemove.Text = "Take"
        '
        'optChangeItemAdd
        '
        Me.optChangeItemAdd.AutoSize = True
        Me.optChangeItemAdd.Location = New System.Drawing.Point(79, 48)
        Me.optChangeItemAdd.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optChangeItemAdd.Name = "optChangeItemAdd"
        Me.optChangeItemAdd.Size = New System.Drawing.Size(48, 19)
        Me.optChangeItemAdd.TabIndex = 3
        Me.optChangeItemAdd.TabStop = True
        Me.optChangeItemAdd.Text = "Give"
        '
        'optChangeItemSet
        '
        Me.optChangeItemSet.AutoSize = True
        Me.optChangeItemSet.Location = New System.Drawing.Point(10, 48)
        Me.optChangeItemSet.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optChangeItemSet.Name = "optChangeItemSet"
        Me.optChangeItemSet.Size = New System.Drawing.Size(55, 19)
        Me.optChangeItemSet.TabIndex = 2
        Me.optChangeItemSet.TabStop = True
        Me.optChangeItemSet.Text = "Set to"
        '
        'cmbChangeItemIndex
        '
        Me.cmbChangeItemIndex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbChangeItemIndex.FormattingEnabled = True
        Me.cmbChangeItemIndex.Location = New System.Drawing.Point(49, 15)
        Me.cmbChangeItemIndex.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbChangeItemIndex.Name = "cmbChangeItemIndex"
        Me.cmbChangeItemIndex.Size = New System.Drawing.Size(162, 24)
        Me.cmbChangeItemIndex.TabIndex = 1
        '
        'DarkLabel21
        '
        Me.DarkLabel21.AutoSize = True
        Me.DarkLabel21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel21.Location = New System.Drawing.Point(7, 18)
        Me.DarkLabel21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel21.Name = "DarkLabel21"
        Me.DarkLabel21.Size = New System.Drawing.Size(34, 15)
        Me.DarkLabel21.TabIndex = 0
        Me.DarkLabel21.Text = "Item:"
        '
        'fraPlayBGM
        '
        Me.fraPlayBGM.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayBGM.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraPlayBGM.Controls.Add(Me.btnPlayBgmOk)
        Me.fraPlayBGM.Controls.Add(Me.btnPlayBgmCancel)
        Me.fraPlayBGM.Controls.Add(Me.cmbPlayBGM)
        Me.fraPlayBGM.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayBGM.Location = New System.Drawing.Point(468, 1)
        Me.fraPlayBGM.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayBGM.Name = "fraPlayBGM"
        Me.fraPlayBGM.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayBGM.Size = New System.Drawing.Size(287, 87)
        Me.fraPlayBGM.TabIndex = 21
        Me.fraPlayBGM.TabStop = False
        Me.fraPlayBGM.Text = "Play BGM"
        Me.fraPlayBGM.Visible = False
        '
        'btnPlayBgmOk
        '
        Me.btnPlayBgmOk.Location = New System.Drawing.Point(54, 53)
        Me.btnPlayBgmOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlayBgmOk.Name = "btnPlayBgmOk"
        Me.btnPlayBgmOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlayBgmOk.Size = New System.Drawing.Size(88, 27)
        Me.btnPlayBgmOk.TabIndex = 27
        Me.btnPlayBgmOk.Text = "Ok"
        '
        'btnPlayBgmCancel
        '
        Me.btnPlayBgmCancel.Location = New System.Drawing.Point(148, 53)
        Me.btnPlayBgmCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlayBgmCancel.Name = "btnPlayBgmCancel"
        Me.btnPlayBgmCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlayBgmCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnPlayBgmCancel.TabIndex = 26
        Me.btnPlayBgmCancel.Text = "Cancel"
        '
        'cmbPlayBGM
        '
        Me.cmbPlayBGM.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayBGM.FormattingEnabled = True
        Me.cmbPlayBGM.Location = New System.Drawing.Point(7, 22)
        Me.cmbPlayBGM.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPlayBGM.Name = "cmbPlayBGM"
        Me.cmbPlayBGM.Size = New System.Drawing.Size(271, 24)
        Me.cmbPlayBGM.TabIndex = 0
        '
        'fraConditionalBranch
        '
        Me.fraConditionalBranch.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraConditionalBranch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_Time)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition9)
        Me.fraConditionalBranch.Controls.Add(Me.btnConditionalBranchOk)
        Me.fraConditionalBranch.Controls.Add(Me.btnConditionalBranchCancel)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_Gender)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition8)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_SelfSwitchCondition)
        Me.fraConditionalBranch.Controls.Add(Me.DarkLabel17)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_SelfSwitch)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition6)
        Me.fraConditionalBranch.Controls.Add(Me.nudCondition_LevelAmount)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition5)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_LevelCompare)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_LearntSkill)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition4)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_JobIs)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition3)
        Me.fraConditionalBranch.Controls.Add(Me.nudCondition_HasItem)
        Me.fraConditionalBranch.Controls.Add(Me.DarkLabel16)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_HasItem)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition2)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition1)
        Me.fraConditionalBranch.Controls.Add(Me.DarkLabel15)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondtion_PlayerSwitchCondition)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_PlayerSwitch)
        Me.fraConditionalBranch.Controls.Add(Me.nudCondition_PlayerVarCondition)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_PlayerVarCompare)
        Me.fraConditionalBranch.Controls.Add(Me.DarkLabel14)
        Me.fraConditionalBranch.Controls.Add(Me.cmbCondition_PlayerVarIndex)
        Me.fraConditionalBranch.Controls.Add(Me.optCondition0)
        Me.fraConditionalBranch.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraConditionalBranch.Location = New System.Drawing.Point(7, 8)
        Me.fraConditionalBranch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraConditionalBranch.Name = "fraConditionalBranch"
        Me.fraConditionalBranch.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraConditionalBranch.Size = New System.Drawing.Size(454, 516)
        Me.fraConditionalBranch.TabIndex = 0
        Me.fraConditionalBranch.TabStop = False
        Me.fraConditionalBranch.Text = "Conditional Branch"
        Me.fraConditionalBranch.Visible = False
        '
        'cmbCondition_Time
        '
        Me.cmbCondition_Time.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.cmbCondition_Time.FormattingEnabled = True
        Me.cmbCondition_Time.Items.AddRange(New Object() {"Day", "Night", "Dawn", "Dusk"})
        Me.cmbCondition_Time.Location = New System.Drawing.Point(279, 267)
        Me.cmbCondition_Time.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_Time.Name = "cmbCondition_Time"
        Me.cmbCondition_Time.Size = New System.Drawing.Size(167, 24)
        Me.cmbCondition_Time.TabIndex = 33
        '
        'optCondition9
        '
        Me.optCondition9.AutoSize = True
        Me.optCondition9.Location = New System.Drawing.Point(7, 268)
        Me.optCondition9.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optCondition9.Name = "optCondition9"
        Me.optCondition9.Size = New System.Drawing.Size(102, 19)
        Me.optCondition9.TabIndex = 32
        Me.optCondition9.TabStop = True
        Me.optCondition9.Text = "Time of Day is:"
        '
        'btnConditionalBranchOk
        '
        Me.btnConditionalBranchOk.Location = New System.Drawing.Point(264, 480)
        Me.btnConditionalBranchOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnConditionalBranchOk.Name = "btnConditionalBranchOk"
        Me.btnConditionalBranchOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnConditionalBranchOk.Size = New System.Drawing.Size(88, 27)
        Me.btnConditionalBranchOk.TabIndex = 31
        Me.btnConditionalBranchOk.Text = "Ok"
        '
        'btnConditionalBranchCancel
        '
        Me.btnConditionalBranchCancel.Location = New System.Drawing.Point(358, 480)
        Me.btnConditionalBranchCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnConditionalBranchCancel.Name = "btnConditionalBranchCancel"
        Me.btnConditionalBranchCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnConditionalBranchCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnConditionalBranchCancel.TabIndex = 30
        Me.btnConditionalBranchCancel.Text = "Cancel"
        '
        'cmbCondition_Gender
        '
        Me.cmbCondition_Gender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_Gender.FormattingEnabled = True
        Me.cmbCondition_Gender.Items.AddRange(New Object() {"Male", "Female"})
        Me.cmbCondition_Gender.Location = New System.Drawing.Point(279, 236)
        Me.cmbCondition_Gender.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_Gender.Name = "cmbCondition_Gender"
        Me.cmbCondition_Gender.Size = New System.Drawing.Size(167, 24)
        Me.cmbCondition_Gender.TabIndex = 29
        '
        'optCondition8
        '
        Me.optCondition8.AutoSize = True
        Me.optCondition8.Location = New System.Drawing.Point(7, 237)
        Me.optCondition8.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optCondition8.Name = "optCondition8"
        Me.optCondition8.Size = New System.Drawing.Size(109, 19)
        Me.optCondition8.TabIndex = 28
        Me.optCondition8.TabStop = True
        Me.optCondition8.Text = "Player Gender is"
        '
        'cmbCondition_SelfSwitchCondition
        '
        Me.cmbCondition_SelfSwitchCondition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_SelfSwitchCondition.FormattingEnabled = True
        Me.cmbCondition_SelfSwitchCondition.Items.AddRange(New Object() {"False", "True"})
        Me.cmbCondition_SelfSwitchCondition.Location = New System.Drawing.Point(306, 211)
        Me.cmbCondition_SelfSwitchCondition.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_SelfSwitchCondition.Name = "cmbCondition_SelfSwitchCondition"
        Me.cmbCondition_SelfSwitchCondition.Size = New System.Drawing.Size(140, 24)
        Me.cmbCondition_SelfSwitchCondition.TabIndex = 23
        '
        'DarkLabel17
        '
        Me.DarkLabel17.AutoSize = True
        Me.DarkLabel17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel17.Location = New System.Drawing.Point(273, 215)
        Me.DarkLabel17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel17.Name = "DarkLabel17"
        Me.DarkLabel17.Size = New System.Drawing.Size(15, 15)
        Me.DarkLabel17.TabIndex = 22
        Me.DarkLabel17.Text = "is"
        '
        'cmbCondition_SelfSwitch
        '
        Me.cmbCondition_SelfSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_SelfSwitch.FormattingEnabled = True
        Me.cmbCondition_SelfSwitch.Location = New System.Drawing.Point(125, 211)
        Me.cmbCondition_SelfSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_SelfSwitch.Name = "cmbCondition_SelfSwitch"
        Me.cmbCondition_SelfSwitch.Size = New System.Drawing.Size(140, 24)
        Me.cmbCondition_SelfSwitch.TabIndex = 21
        '
        'optCondition6
        '
        Me.optCondition6.AutoSize = True
        Me.optCondition6.Location = New System.Drawing.Point(7, 212)
        Me.optCondition6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optCondition6.Name = "optCondition6"
        Me.optCondition6.Size = New System.Drawing.Size(82, 19)
        Me.optCondition6.TabIndex = 20
        Me.optCondition6.TabStop = True
        Me.optCondition6.Text = "Self Switch"
        '
        'nudCondition_LevelAmount
        '
        Me.nudCondition_LevelAmount.Location = New System.Drawing.Point(314, 181)
        Me.nudCondition_LevelAmount.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudCondition_LevelAmount.Name = "nudCondition_LevelAmount"
        Me.nudCondition_LevelAmount.Size = New System.Drawing.Size(132, 23)
        Me.nudCondition_LevelAmount.TabIndex = 19
        '
        'optCondition5
        '
        Me.optCondition5.AutoSize = True
        Me.optCondition5.Location = New System.Drawing.Point(7, 181)
        Me.optCondition5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optCondition5.Name = "optCondition5"
        Me.optCondition5.Size = New System.Drawing.Size(63, 19)
        Me.optCondition5.TabIndex = 18
        Me.optCondition5.TabStop = True
        Me.optCondition5.Text = "Level is"
        '
        'cmbCondition_LevelCompare
        '
        Me.cmbCondition_LevelCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_LevelCompare.FormattingEnabled = True
        Me.cmbCondition_LevelCompare.Items.AddRange(New Object() {"Equal To", "Great Than OrElse Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal"})
        Me.cmbCondition_LevelCompare.Location = New System.Drawing.Point(125, 180)
        Me.cmbCondition_LevelCompare.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_LevelCompare.Name = "cmbCondition_LevelCompare"
        Me.cmbCondition_LevelCompare.Size = New System.Drawing.Size(181, 24)
        Me.cmbCondition_LevelCompare.TabIndex = 17
        '
        'cmbCondition_LearntSkill
        '
        Me.cmbCondition_LearntSkill.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_LearntSkill.FormattingEnabled = True
        Me.cmbCondition_LearntSkill.Location = New System.Drawing.Point(125, 149)
        Me.cmbCondition_LearntSkill.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_LearntSkill.Name = "cmbCondition_LearntSkill"
        Me.cmbCondition_LearntSkill.Size = New System.Drawing.Size(321, 24)
        Me.cmbCondition_LearntSkill.TabIndex = 16
        '
        'optCondition4
        '
        Me.optCondition4.AutoSize = True
        Me.optCondition4.Location = New System.Drawing.Point(7, 150)
        Me.optCondition4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optCondition4.Name = "optCondition4"
        Me.optCondition4.Size = New System.Drawing.Size(84, 19)
        Me.optCondition4.TabIndex = 15
        Me.optCondition4.TabStop = True
        Me.optCondition4.Text = "Knows Skill"
        '
        'cmbCondition_JobIs
        '
        Me.cmbCondition_JobIs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_JobIs.FormattingEnabled = True
        Me.cmbCondition_JobIs.Location = New System.Drawing.Point(125, 118)
        Me.cmbCondition_JobIs.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_JobIs.Name = "cmbCondition_JobIs"
        Me.cmbCondition_JobIs.Size = New System.Drawing.Size(321, 24)
        Me.cmbCondition_JobIs.TabIndex = 14
        '
        'optCondition3
        '
        Me.optCondition3.AutoSize = True
        Me.optCondition3.Location = New System.Drawing.Point(7, 119)
        Me.optCondition3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optCondition3.Name = "optCondition3"
        Me.optCondition3.Size = New System.Drawing.Size(54, 19)
        Me.optCondition3.TabIndex = 13
        Me.optCondition3.TabStop = True
        Me.optCondition3.Text = "Job Is"
        '
        'nudCondition_HasItem
        '
        Me.nudCondition_HasItem.Location = New System.Drawing.Point(306, 88)
        Me.nudCondition_HasItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudCondition_HasItem.Name = "nudCondition_HasItem"
        Me.nudCondition_HasItem.Size = New System.Drawing.Size(140, 23)
        Me.nudCondition_HasItem.TabIndex = 12
        '
        'DarkLabel16
        '
        Me.DarkLabel16.AutoSize = True
        Me.DarkLabel16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel16.Location = New System.Drawing.Point(273, 90)
        Me.DarkLabel16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel16.Name = "DarkLabel16"
        Me.DarkLabel16.Size = New System.Drawing.Size(14, 15)
        Me.DarkLabel16.TabIndex = 11
        Me.DarkLabel16.Text = "X"
        '
        'cmbCondition_HasItem
        '
        Me.cmbCondition_HasItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_HasItem.FormattingEnabled = True
        Me.cmbCondition_HasItem.Location = New System.Drawing.Point(125, 87)
        Me.cmbCondition_HasItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_HasItem.Name = "cmbCondition_HasItem"
        Me.cmbCondition_HasItem.Size = New System.Drawing.Size(140, 24)
        Me.cmbCondition_HasItem.TabIndex = 10
        '
        'optCondition2
        '
        Me.optCondition2.AutoSize = True
        Me.optCondition2.Location = New System.Drawing.Point(7, 88)
        Me.optCondition2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optCondition2.Name = "optCondition2"
        Me.optCondition2.Size = New System.Drawing.Size(72, 19)
        Me.optCondition2.TabIndex = 9
        Me.optCondition2.TabStop = True
        Me.optCondition2.Text = "Has Item"
        '
        'optCondition1
        '
        Me.optCondition1.AutoSize = True
        Me.optCondition1.Location = New System.Drawing.Point(7, 57)
        Me.optCondition1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optCondition1.Name = "optCondition1"
        Me.optCondition1.Size = New System.Drawing.Size(95, 19)
        Me.optCondition1.TabIndex = 8
        Me.optCondition1.TabStop = True
        Me.optCondition1.Text = "Player Switch"
        '
        'DarkLabel15
        '
        Me.DarkLabel15.AutoSize = True
        Me.DarkLabel15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel15.Location = New System.Drawing.Point(273, 59)
        Me.DarkLabel15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel15.Name = "DarkLabel15"
        Me.DarkLabel15.Size = New System.Drawing.Size(15, 15)
        Me.DarkLabel15.TabIndex = 7
        Me.DarkLabel15.Text = "is"
        '
        'cmbCondtion_PlayerSwitchCondition
        '
        Me.cmbCondtion_PlayerSwitchCondition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondtion_PlayerSwitchCondition.FormattingEnabled = True
        Me.cmbCondtion_PlayerSwitchCondition.Items.AddRange(New Object() {"False", "True"})
        Me.cmbCondtion_PlayerSwitchCondition.Location = New System.Drawing.Point(306, 55)
        Me.cmbCondtion_PlayerSwitchCondition.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondtion_PlayerSwitchCondition.Name = "cmbCondtion_PlayerSwitchCondition"
        Me.cmbCondtion_PlayerSwitchCondition.Size = New System.Drawing.Size(140, 24)
        Me.cmbCondtion_PlayerSwitchCondition.TabIndex = 6
        '
        'cmbCondition_PlayerSwitch
        '
        Me.cmbCondition_PlayerSwitch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_PlayerSwitch.FormattingEnabled = True
        Me.cmbCondition_PlayerSwitch.Location = New System.Drawing.Point(125, 55)
        Me.cmbCondition_PlayerSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_PlayerSwitch.Name = "cmbCondition_PlayerSwitch"
        Me.cmbCondition_PlayerSwitch.Size = New System.Drawing.Size(140, 24)
        Me.cmbCondition_PlayerSwitch.TabIndex = 5
        '
        'nudCondition_PlayerVarCondition
        '
        Me.nudCondition_PlayerVarCondition.Location = New System.Drawing.Point(391, 25)
        Me.nudCondition_PlayerVarCondition.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudCondition_PlayerVarCondition.Name = "nudCondition_PlayerVarCondition"
        Me.nudCondition_PlayerVarCondition.Size = New System.Drawing.Size(55, 23)
        Me.nudCondition_PlayerVarCondition.TabIndex = 4
        '
        'cmbCondition_PlayerVarCompare
        '
        Me.cmbCondition_PlayerVarCompare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_PlayerVarCompare.FormattingEnabled = True
        Me.cmbCondition_PlayerVarCompare.Items.AddRange(New Object() {"Equal To", "Great Than OrElse Equal To", "Less Than or Equal To", "Greater Than", "Less Than", "Does Not Equal"})
        Me.cmbCondition_PlayerVarCompare.Location = New System.Drawing.Point(275, 24)
        Me.cmbCondition_PlayerVarCompare.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_PlayerVarCompare.Name = "cmbCondition_PlayerVarCompare"
        Me.cmbCondition_PlayerVarCompare.Size = New System.Drawing.Size(102, 24)
        Me.cmbCondition_PlayerVarCompare.TabIndex = 3
        '
        'DarkLabel14
        '
        Me.DarkLabel14.AutoSize = True
        Me.DarkLabel14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel14.Location = New System.Drawing.Point(252, 28)
        Me.DarkLabel14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel14.Name = "DarkLabel14"
        Me.DarkLabel14.Size = New System.Drawing.Size(15, 15)
        Me.DarkLabel14.TabIndex = 2
        Me.DarkLabel14.Text = "is"
        '
        'cmbCondition_PlayerVarIndex
        '
        Me.cmbCondition_PlayerVarIndex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCondition_PlayerVarIndex.FormattingEnabled = True
        Me.cmbCondition_PlayerVarIndex.Location = New System.Drawing.Point(125, 24)
        Me.cmbCondition_PlayerVarIndex.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCondition_PlayerVarIndex.Name = "cmbCondition_PlayerVarIndex"
        Me.cmbCondition_PlayerVarIndex.Size = New System.Drawing.Size(119, 24)
        Me.cmbCondition_PlayerVarIndex.TabIndex = 1
        '
        'optCondition0
        '
        Me.optCondition0.AutoSize = True
        Me.optCondition0.Location = New System.Drawing.Point(7, 25)
        Me.optCondition0.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optCondition0.Name = "optCondition0"
        Me.optCondition0.Size = New System.Drawing.Size(101, 19)
        Me.optCondition0.TabIndex = 0
        Me.optCondition0.TabStop = True
        Me.optCondition0.Text = "Player Variable"
        '
        'fraPlayAnimation
        '
        Me.fraPlayAnimation.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraPlayAnimation.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraPlayAnimation.Controls.Add(Me.btnPlayAnimationOk)
        Me.fraPlayAnimation.Controls.Add(Me.btnPlayAnimationCancel)
        Me.fraPlayAnimation.Controls.Add(Me.lblPlayAnimY)
        Me.fraPlayAnimation.Controls.Add(Me.lblPlayAnimX)
        Me.fraPlayAnimation.Controls.Add(Me.cmbPlayAnimEvent)
        Me.fraPlayAnimation.Controls.Add(Me.DarkLabel62)
        Me.fraPlayAnimation.Controls.Add(Me.cmbAnimTargetType)
        Me.fraPlayAnimation.Controls.Add(Me.nudPlayAnimTileY)
        Me.fraPlayAnimation.Controls.Add(Me.nudPlayAnimTileX)
        Me.fraPlayAnimation.Controls.Add(Me.DarkLabel61)
        Me.fraPlayAnimation.Controls.Add(Me.cmbPlayAnim)
        Me.fraPlayAnimation.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraPlayAnimation.Location = New System.Drawing.Point(468, 297)
        Me.fraPlayAnimation.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayAnimation.Name = "fraPlayAnimation"
        Me.fraPlayAnimation.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraPlayAnimation.Size = New System.Drawing.Size(289, 187)
        Me.fraPlayAnimation.TabIndex = 36
        Me.fraPlayAnimation.TabStop = False
        Me.fraPlayAnimation.Text = "Play Animation"
        Me.fraPlayAnimation.Visible = False
        '
        'btnPlayAnimationOk
        '
        Me.btnPlayAnimationOk.Location = New System.Drawing.Point(100, 152)
        Me.btnPlayAnimationOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlayAnimationOk.Name = "btnPlayAnimationOk"
        Me.btnPlayAnimationOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlayAnimationOk.Size = New System.Drawing.Size(88, 27)
        Me.btnPlayAnimationOk.TabIndex = 36
        Me.btnPlayAnimationOk.Text = "Ok"
        '
        'btnPlayAnimationCancel
        '
        Me.btnPlayAnimationCancel.Location = New System.Drawing.Point(195, 152)
        Me.btnPlayAnimationCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPlayAnimationCancel.Name = "btnPlayAnimationCancel"
        Me.btnPlayAnimationCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnPlayAnimationCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnPlayAnimationCancel.TabIndex = 35
        Me.btnPlayAnimationCancel.Text = "Cancel"
        '
        'lblPlayAnimY
        '
        Me.lblPlayAnimY.AutoSize = True
        Me.lblPlayAnimY.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.lblPlayAnimY.Location = New System.Drawing.Point(153, 122)
        Me.lblPlayAnimY.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPlayAnimY.Name = "lblPlayAnimY"
        Me.lblPlayAnimY.Size = New System.Drawing.Size(65, 15)
        Me.lblPlayAnimY.TabIndex = 34
        Me.lblPlayAnimY.Text = "Map Tile Y:"
        '
        'lblPlayAnimX
        '
        Me.lblPlayAnimX.AutoSize = True
        Me.lblPlayAnimX.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.lblPlayAnimX.Location = New System.Drawing.Point(7, 122)
        Me.lblPlayAnimX.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPlayAnimX.Name = "lblPlayAnimX"
        Me.lblPlayAnimX.Size = New System.Drawing.Size(65, 15)
        Me.lblPlayAnimX.TabIndex = 33
        Me.lblPlayAnimX.Text = "Map Tile X:"
        '
        'cmbPlayAnimEvent
        '
        Me.cmbPlayAnimEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayAnimEvent.FormattingEnabled = True
        Me.cmbPlayAnimEvent.Location = New System.Drawing.Point(97, 84)
        Me.cmbPlayAnimEvent.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPlayAnimEvent.Name = "cmbPlayAnimEvent"
        Me.cmbPlayAnimEvent.Size = New System.Drawing.Size(185, 24)
        Me.cmbPlayAnimEvent.TabIndex = 32
        '
        'DarkLabel62
        '
        Me.DarkLabel62.AutoSize = True
        Me.DarkLabel62.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel62.Location = New System.Drawing.Point(5, 57)
        Me.DarkLabel62.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel62.Name = "DarkLabel62"
        Me.DarkLabel62.Size = New System.Drawing.Size(66, 15)
        Me.DarkLabel62.TabIndex = 31
        Me.DarkLabel62.Text = "Target Type"
        '
        'cmbAnimTargetType
        '
        Me.cmbAnimTargetType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbAnimTargetType.FormattingEnabled = True
        Me.cmbAnimTargetType.Items.AddRange(New Object() {"Player", "Event", "Tile"})
        Me.cmbAnimTargetType.Location = New System.Drawing.Point(97, 53)
        Me.cmbAnimTargetType.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbAnimTargetType.Name = "cmbAnimTargetType"
        Me.cmbAnimTargetType.Size = New System.Drawing.Size(185, 24)
        Me.cmbAnimTargetType.TabIndex = 30
        '
        'nudPlayAnimTileY
        '
        Me.nudPlayAnimTileY.Location = New System.Drawing.Point(231, 120)
        Me.nudPlayAnimTileY.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudPlayAnimTileY.Name = "nudPlayAnimTileY"
        Me.nudPlayAnimTileY.Size = New System.Drawing.Size(51, 23)
        Me.nudPlayAnimTileY.TabIndex = 29
        '
        'nudPlayAnimTileX
        '
        Me.nudPlayAnimTileX.Location = New System.Drawing.Point(85, 120)
        Me.nudPlayAnimTileX.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudPlayAnimTileX.Name = "nudPlayAnimTileX"
        Me.nudPlayAnimTileX.Size = New System.Drawing.Size(51, 23)
        Me.nudPlayAnimTileX.TabIndex = 28
        '
        'DarkLabel61
        '
        Me.DarkLabel61.AutoSize = True
        Me.DarkLabel61.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel61.Location = New System.Drawing.Point(7, 25)
        Me.DarkLabel61.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel61.Name = "DarkLabel61"
        Me.DarkLabel61.Size = New System.Drawing.Size(66, 15)
        Me.DarkLabel61.TabIndex = 1
        Me.DarkLabel61.Text = "Animation:"
        '
        'cmbPlayAnim
        '
        Me.cmbPlayAnim.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPlayAnim.FormattingEnabled = True
        Me.cmbPlayAnim.Location = New System.Drawing.Point(72, 22)
        Me.cmbPlayAnim.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPlayAnim.Name = "cmbPlayAnim"
        Me.cmbPlayAnim.Size = New System.Drawing.Size(209, 24)
        Me.cmbPlayAnim.TabIndex = 0
        '
        'fraSetWait
        '
        Me.fraSetWait.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraSetWait.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraSetWait.Controls.Add(Me.btnSetWaitOk)
        Me.fraSetWait.Controls.Add(Me.btnSetWaitCancel)
        Me.fraSetWait.Controls.Add(Me.DarkLabel74)
        Me.fraSetWait.Controls.Add(Me.DarkLabel72)
        Me.fraSetWait.Controls.Add(Me.DarkLabel73)
        Me.fraSetWait.Controls.Add(Me.nudWaitAmount)
        Me.fraSetWait.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraSetWait.Location = New System.Drawing.Point(468, 305)
        Me.fraSetWait.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetWait.Name = "fraSetWait"
        Me.fraSetWait.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraSetWait.Size = New System.Drawing.Size(289, 103)
        Me.fraSetWait.TabIndex = 41
        Me.fraSetWait.TabStop = False
        Me.fraSetWait.Text = "Wait..."
        Me.fraSetWait.Visible = False
        '
        'btnSetWaitOk
        '
        Me.btnSetWaitOk.Location = New System.Drawing.Point(58, 67)
        Me.btnSetWaitOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetWaitOk.Name = "btnSetWaitOk"
        Me.btnSetWaitOk.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetWaitOk.Size = New System.Drawing.Size(88, 27)
        Me.btnSetWaitOk.TabIndex = 37
        Me.btnSetWaitOk.Text = "Ok"
        '
        'btnSetWaitCancel
        '
        Me.btnSetWaitCancel.Location = New System.Drawing.Point(153, 67)
        Me.btnSetWaitCancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetWaitCancel.Name = "btnSetWaitCancel"
        Me.btnSetWaitCancel.Padding = New System.Windows.Forms.Padding(6)
        Me.btnSetWaitCancel.Size = New System.Drawing.Size(88, 27)
        Me.btnSetWaitCancel.TabIndex = 36
        Me.btnSetWaitCancel.Text = "Cancel"
        '
        'DarkLabel74
        '
        Me.DarkLabel74.AutoSize = True
        Me.DarkLabel74.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel74.Location = New System.Drawing.Point(82, 48)
        Me.DarkLabel74.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel74.Name = "DarkLabel74"
        Me.DarkLabel74.Size = New System.Drawing.Size(120, 15)
        Me.DarkLabel74.TabIndex = 35
        Me.DarkLabel74.Text = "Hint: 1000 Ms = 1 Sec"
        '
        'DarkLabel72
        '
        Me.DarkLabel72.AutoSize = True
        Me.DarkLabel72.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel72.Location = New System.Drawing.Point(254, 27)
        Me.DarkLabel72.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel72.Name = "DarkLabel72"
        Me.DarkLabel72.Size = New System.Drawing.Size(23, 15)
        Me.DarkLabel72.TabIndex = 34
        Me.DarkLabel72.Text = "Ms"
        '
        'DarkLabel73
        '
        Me.DarkLabel73.AutoSize = True
        Me.DarkLabel73.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.DarkLabel73.Location = New System.Drawing.Point(18, 27)
        Me.DarkLabel73.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DarkLabel73.Name = "DarkLabel73"
        Me.DarkLabel73.Size = New System.Drawing.Size(31, 15)
        Me.DarkLabel73.TabIndex = 33
        Me.DarkLabel73.Text = "Wait"
        '
        'nudWaitAmount
        '
        Me.nudWaitAmount.Location = New System.Drawing.Point(58, 22)
        Me.nudWaitAmount.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudWaitAmount.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudWaitAmount.Name = "nudWaitAmount"
        Me.nudWaitAmount.Size = New System.Drawing.Size(190, 23)
        Me.nudWaitAmount.TabIndex = 32
        '
        'pnlVariableSwitches
        '
        Me.pnlVariableSwitches.Controls.Add(Me.FraRenaming)
        Me.pnlVariableSwitches.Controls.Add(Me.fraLabeling)
        Me.pnlVariableSwitches.Location = New System.Drawing.Point(933, 232)
        Me.pnlVariableSwitches.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlVariableSwitches.Name = "pnlVariableSwitches"
        Me.pnlVariableSwitches.Size = New System.Drawing.Size(108, 105)
        Me.pnlVariableSwitches.TabIndex = 11
        '
        'FraRenaming
        '
        Me.FraRenaming.Controls.Add(Me.btnRename_Cancel)
        Me.FraRenaming.Controls.Add(Me.btnRename_Ok)
        Me.FraRenaming.Controls.Add(Me.fraRandom10)
        Me.FraRenaming.ForeColor = System.Drawing.Color.Gainsboro
        Me.FraRenaming.Location = New System.Drawing.Point(275, 495)
        Me.FraRenaming.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.FraRenaming.Name = "FraRenaming"
        Me.FraRenaming.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.FraRenaming.Size = New System.Drawing.Size(425, 165)
        Me.FraRenaming.TabIndex = 8
        Me.FraRenaming.TabStop = False
        Me.FraRenaming.Text = "Renaming Variable/Switch"
        Me.FraRenaming.Visible = False
        '
        'btnRename_Cancel
        '
        Me.btnRename_Cancel.ForeColor = System.Drawing.Color.Black
        Me.btnRename_Cancel.Location = New System.Drawing.Point(267, 118)
        Me.btnRename_Cancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRename_Cancel.Name = "btnRename_Cancel"
        Me.btnRename_Cancel.Size = New System.Drawing.Size(88, 27)
        Me.btnRename_Cancel.TabIndex = 2
        Me.btnRename_Cancel.Text = "Cancel"
        Me.btnRename_Cancel.UseVisualStyleBackColor = True
        '
        'btnRename_Ok
        '
        Me.btnRename_Ok.ForeColor = System.Drawing.Color.Black
        Me.btnRename_Ok.Location = New System.Drawing.Point(63, 118)
        Me.btnRename_Ok.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRename_Ok.Name = "btnRename_Ok"
        Me.btnRename_Ok.Size = New System.Drawing.Size(88, 27)
        Me.btnRename_Ok.TabIndex = 1
        Me.btnRename_Ok.Text = "Ok"
        Me.btnRename_Ok.UseVisualStyleBackColor = True
        '
        'fraRandom10
        '
        Me.fraRandom10.Controls.Add(Me.txtRename)
        Me.fraRandom10.Controls.Add(Me.lblEditing)
        Me.fraRandom10.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraRandom10.Location = New System.Drawing.Point(7, 22)
        Me.fraRandom10.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraRandom10.Name = "fraRandom10"
        Me.fraRandom10.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraRandom10.Size = New System.Drawing.Size(411, 89)
        Me.fraRandom10.TabIndex = 0
        Me.fraRandom10.TabStop = False
        Me.fraRandom10.Text = "Editing Variable/Switch"
        '
        'txtRename
        '
        Me.txtRename.Location = New System.Drawing.Point(7, 47)
        Me.txtRename.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtRename.Name = "txtRename"
        Me.txtRename.Size = New System.Drawing.Size(396, 23)
        Me.txtRename.TabIndex = 1
        '
        'lblEditing
        '
        Me.lblEditing.AutoSize = True
        Me.lblEditing.Location = New System.Drawing.Point(4, 29)
        Me.lblEditing.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEditing.Name = "lblEditing"
        Me.lblEditing.Size = New System.Drawing.Size(110, 15)
        Me.lblEditing.TabIndex = 0
        Me.lblEditing.Text = "Naming Variable #1"
        '
        'fraLabeling
        '
        Me.fraLabeling.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.fraLabeling.BorderColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.fraLabeling.Controls.Add(Me.lstSwitches)
        Me.fraLabeling.Controls.Add(Me.lstVariables)
        Me.fraLabeling.Controls.Add(Me.btnLabel_Cancel)
        Me.fraLabeling.Controls.Add(Me.lblRandomLabel36)
        Me.fraLabeling.Controls.Add(Me.btnRenameVariable)
        Me.fraLabeling.Controls.Add(Me.lblRandomLabel25)
        Me.fraLabeling.Controls.Add(Me.btnRenameSwitch)
        Me.fraLabeling.Controls.Add(Me.btnLabel_Ok)
        Me.fraLabeling.ForeColor = System.Drawing.Color.Gainsboro
        Me.fraLabeling.Location = New System.Drawing.Point(227, 33)
        Me.fraLabeling.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraLabeling.Name = "fraLabeling"
        Me.fraLabeling.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraLabeling.Size = New System.Drawing.Size(532, 447)
        Me.fraLabeling.TabIndex = 0
        Me.fraLabeling.TabStop = False
        Me.fraLabeling.Text = "Label Variables and  Switches   "
        '
        'lstSwitches
        '
        Me.lstSwitches.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstSwitches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstSwitches.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstSwitches.FormattingEnabled = True
        Me.lstSwitches.ItemHeight = 15
        Me.lstSwitches.Location = New System.Drawing.Point(275, 45)
        Me.lstSwitches.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstSwitches.Name = "lstSwitches"
        Me.lstSwitches.Size = New System.Drawing.Size(239, 332)
        Me.lstSwitches.TabIndex = 7
        '
        'lstVariables
        '
        Me.lstVariables.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.lstVariables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstVariables.ForeColor = System.Drawing.Color.Gainsboro
        Me.lstVariables.FormattingEnabled = True
        Me.lstVariables.ItemHeight = 15
        Me.lstVariables.Location = New System.Drawing.Point(16, 45)
        Me.lstVariables.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstVariables.Name = "lstVariables"
        Me.lstVariables.Size = New System.Drawing.Size(239, 332)
        Me.lstVariables.TabIndex = 6
        '
        'btnLabel_Cancel
        '
        Me.btnLabel_Cancel.ForeColor = System.Drawing.Color.Black
        Me.btnLabel_Cancel.Location = New System.Drawing.Point(275, 393)
        Me.btnLabel_Cancel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnLabel_Cancel.Name = "btnLabel_Cancel"
        Me.btnLabel_Cancel.Size = New System.Drawing.Size(88, 27)
        Me.btnLabel_Cancel.TabIndex = 12
        Me.btnLabel_Cancel.Text = "Cancel"
        Me.btnLabel_Cancel.UseVisualStyleBackColor = True
        '
        'lblRandomLabel36
        '
        Me.lblRandomLabel36.AutoSize = True
        Me.lblRandomLabel36.Location = New System.Drawing.Point(342, 27)
        Me.lblRandomLabel36.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRandomLabel36.Name = "lblRandomLabel36"
        Me.lblRandomLabel36.Size = New System.Drawing.Size(88, 15)
        Me.lblRandomLabel36.TabIndex = 5
        Me.lblRandomLabel36.Text = "Player Switches"
        '
        'btnRenameVariable
        '
        Me.btnRenameVariable.ForeColor = System.Drawing.Color.Black
        Me.btnRenameVariable.Location = New System.Drawing.Point(16, 393)
        Me.btnRenameVariable.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRenameVariable.Name = "btnRenameVariable"
        Me.btnRenameVariable.Size = New System.Drawing.Size(124, 27)
        Me.btnRenameVariable.TabIndex = 9
        Me.btnRenameVariable.Text = "Rename Variable"
        Me.btnRenameVariable.UseVisualStyleBackColor = True
        '
        'lblRandomLabel25
        '
        Me.lblRandomLabel25.AutoSize = True
        Me.lblRandomLabel25.Location = New System.Drawing.Point(93, 24)
        Me.lblRandomLabel25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRandomLabel25.Name = "lblRandomLabel25"
        Me.lblRandomLabel25.Size = New System.Drawing.Size(88, 15)
        Me.lblRandomLabel25.TabIndex = 4
        Me.lblRandomLabel25.Text = "Player Variables"
        '
        'btnRenameSwitch
        '
        Me.btnRenameSwitch.ForeColor = System.Drawing.Color.Black
        Me.btnRenameSwitch.Location = New System.Drawing.Point(387, 393)
        Me.btnRenameSwitch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRenameSwitch.Name = "btnRenameSwitch"
        Me.btnRenameSwitch.Size = New System.Drawing.Size(127, 27)
        Me.btnRenameSwitch.TabIndex = 10
        Me.btnRenameSwitch.Text = "Rename Switch"
        Me.btnRenameSwitch.UseVisualStyleBackColor = True
        '
        'btnLabel_Ok
        '
        Me.btnLabel_Ok.ForeColor = System.Drawing.Color.Black
        Me.btnLabel_Ok.Location = New System.Drawing.Point(168, 393)
        Me.btnLabel_Ok.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnLabel_Ok.Name = "btnLabel_Ok"
        Me.btnLabel_Ok.Size = New System.Drawing.Size(88, 27)
        Me.btnLabel_Ok.TabIndex = 11
        Me.btnLabel_Ok.Text = "Ok"
        Me.btnLabel_Ok.UseVisualStyleBackColor = True
        '
        'FrmEditor_Events
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1863, 708)
        Me.Controls.Add(Me.pnlVariableSwitches)
        Me.Controls.Add(Me.fraDialogue)
        Me.Controls.Add(Me.fraMoveRoute)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnLabeling)
        Me.Controls.Add(Me.tabPages)
        Me.Controls.Add(Me.fraPageSetUp)
        Me.Controls.Add(Me.pnlTabPage)
        Me.Controls.Add(Me.pnlGraphicSel)
        Me.ForeColor = System.Drawing.Color.Gainsboro
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "FrmEditor_Events"
        Me.Text = "Event Editor"
        Me.fraPageSetUp.ResumeLayout(False)
        Me.fraPageSetUp.PerformLayout()
        Me.tabPages.ResumeLayout(False)
        Me.pnlTabPage.ResumeLayout(False)
        Me.DarkGroupBox2.ResumeLayout(False)
        Me.fraGraphicPic.ResumeLayout(False)
        CType(Me.picGraphic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DarkGroupBox6.ResumeLayout(False)
        Me.DarkGroupBox6.PerformLayout()
        Me.DarkGroupBox7.ResumeLayout(False)
        Me.DarkGroupBox7.PerformLayout()
        Me.DarkGroupBox5.ResumeLayout(False)
        Me.DarkGroupBox4.ResumeLayout(False)
        CType(Me.picGraphicSel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DarkGroupBox3.ResumeLayout(False)
        Me.DarkGroupBox3.PerformLayout()
        Me.DarkGroupBox1.ResumeLayout(False)
        Me.DarkGroupBox1.PerformLayout()
        CType(Me.nudPlayerVariable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraGraphic.ResumeLayout(False)
        Me.fraGraphic.PerformLayout()
        CType(Me.nudGraphic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCommands.ResumeLayout(False)
        Me.DarkGroupBox8.ResumeLayout(False)
        Me.fraMoveRoute.ResumeLayout(False)
        Me.fraMoveRoute.PerformLayout()
        Me.DarkGroupBox10.ResumeLayout(False)
        Me.fraDialogue.ResumeLayout(False)
        Me.fraShowPic.ResumeLayout(False)
        Me.fraShowPic.PerformLayout()
        CType(Me.nudPicOffsetY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudPicOffsetX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudShowPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picShowPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraMoveRouteWait.ResumeLayout(False)
        Me.fraMoveRouteWait.PerformLayout()
        Me.fraCustomScript.ResumeLayout(False)
        Me.fraCustomScript.PerformLayout()
        CType(Me.nudCustomScript, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSetWeather.ResumeLayout(False)
        Me.fraSetWeather.PerformLayout()
        CType(Me.nudWeatherIntensity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSpawnNpc.ResumeLayout(False)
        Me.fraGiveExp.ResumeLayout(False)
        Me.fraGiveExp.PerformLayout()
        CType(Me.nudGiveExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraEndQuest.ResumeLayout(False)
        Me.fraSetAccess.ResumeLayout(False)
        Me.fraOpenShop.ResumeLayout(False)
        Me.fraChangeLevel.ResumeLayout(False)
        Me.fraChangeLevel.PerformLayout()
        CType(Me.nudChangeLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraChangeGender.ResumeLayout(False)
        Me.fraChangeGender.PerformLayout()
        Me.fraGoToLabel.ResumeLayout(False)
        Me.fraGoToLabel.PerformLayout()
        Me.fraShowChoices.ResumeLayout(False)
        Me.fraShowChoices.PerformLayout()
        CType(Me.picShowChoicesFace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudShowChoicesFace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraPlayerVariable.ResumeLayout(False)
        Me.fraPlayerVariable.PerformLayout()
        CType(Me.nudVariableData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudVariableData4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudVariableData3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudVariableData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudVariableData0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraChangeSprite.ResumeLayout(False)
        Me.fraChangeSprite.PerformLayout()
        CType(Me.nudChangeSprite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picChangeSprite, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSetSelfSwitch.ResumeLayout(False)
        Me.fraSetSelfSwitch.PerformLayout()
        Me.fraMapTint.ResumeLayout(False)
        Me.fraMapTint.PerformLayout()
        CType(Me.nudMapTintData3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMapTintData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMapTintData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMapTintData0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraShowChatBubble.ResumeLayout(False)
        Me.fraShowChatBubble.PerformLayout()
        Me.fraPlaySound.ResumeLayout(False)
        Me.fraChangePK.ResumeLayout(False)
        Me.fraCreateLabel.ResumeLayout(False)
        Me.fraCreateLabel.PerformLayout()
        Me.fraChangeJob.ResumeLayout(False)
        Me.fraChangeJob.PerformLayout()
        Me.fraChangeSkills.ResumeLayout(False)
        Me.fraChangeSkills.PerformLayout()
        Me.fraPlayerWarp.ResumeLayout(False)
        Me.fraPlayerWarp.PerformLayout()
        CType(Me.nudWPY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWPX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWPMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSetFog.ResumeLayout(False)
        Me.fraSetFog.PerformLayout()
        CType(Me.nudFogData2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudFogData1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudFogData0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraShowText.ResumeLayout(False)
        Me.fraShowText.PerformLayout()
        CType(Me.picShowTextFace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudShowTextFace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAddText.ResumeLayout(False)
        Me.fraAddText.PerformLayout()
        Me.fraPlayerSwitch.ResumeLayout(False)
        Me.fraPlayerSwitch.PerformLayout()
        Me.fraChangeItems.ResumeLayout(False)
        Me.fraChangeItems.PerformLayout()
        CType(Me.nudChangeItemsAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraPlayBGM.ResumeLayout(False)
        Me.fraConditionalBranch.ResumeLayout(False)
        Me.fraConditionalBranch.PerformLayout()
        CType(Me.nudCondition_LevelAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCondition_HasItem,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudCondition_PlayerVarCondition,System.ComponentModel.ISupportInitialize).EndInit
        Me.fraPlayAnimation.ResumeLayout(false)
        Me.fraPlayAnimation.PerformLayout
        CType(Me.nudPlayAnimTileY,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudPlayAnimTileX,System.ComponentModel.ISupportInitialize).EndInit
        Me.fraSetWait.ResumeLayout(false)
        Me.fraSetWait.PerformLayout
        CType(Me.nudWaitAmount,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlVariableSwitches.ResumeLayout(false)
        Me.FraRenaming.ResumeLayout(false)
        Me.fraRandom10.ResumeLayout(false)
        Me.fraRandom10.PerformLayout
        Me.fraLabeling.ResumeLayout(false)
        Me.fraLabeling.PerformLayout
        Me.ResumeLayout(false)

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
