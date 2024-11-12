Imports System.Drawing
Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEditor_Map
    Inherits System.Windows.Forms.Form

    ' Shared instance of the form
    Private Shared _instance As frmEditor_Map

    ' Public property to get the shared instance
    Public Shared ReadOnly Property Instance() As frmEditor_Map
        Get
            ' Create a new instance if one does not exist or if it has been disposed
            If _instance Is Nothing OrElse _instance.IsDisposed Then
                _instance = New frmEditor_Map()
            End If
            Return _instance
        End Get
    End Property

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing And components IsNot Nothing Then
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditor_Map))
        btnClearAttribute = New Button()
        optTrap = New RadioButton()
        optHeal = New RadioButton()
        optBank = New RadioButton()
        optShop = New RadioButton()
        optNPCSpawn = New RadioButton()
        optResource = New RadioButton()
        optNPCAvoid = New RadioButton()
        optItem = New RadioButton()
        optWarp = New RadioButton()
        optBlocked = New RadioButton()
        pnlBack = New Panel()
        picBackSelect = New PictureBox()
        Label1 = New Label()
        pnlAttributes = New Panel()
        fraMapLight = New GroupBox()
        lblRadius = New Label()
        scrlLight = New HScrollBar()
        chkShadow = New CheckBox()
        chkFlicker = New CheckBox()
        btnLight = New Button()
        fraAnimation = New GroupBox()
        cmbAnimation = New ComboBox()
        brnAnimation = New Button()
        fraMapWarp = New GroupBox()
        btnMapWarp = New Button()
        scrlMapWarpY = New HScrollBar()
        scrlMapWarpX = New HScrollBar()
        scrlMapWarpMap = New HScrollBar()
        lblMapWarpY = New Label()
        lblMapWarpX = New Label()
        lblMapWarpMap = New Label()
        fraNpcSpawn = New GroupBox()
        lstNpc = New ComboBox()
        btnNpcSpawn = New Button()
        scrlNpcDir = New HScrollBar()
        lblNpcDir = New Label()
        fraHeal = New GroupBox()
        scrlHeal = New HScrollBar()
        lblHeal = New Label()
        cmbHeal = New ComboBox()
        btnHeal = New Button()
        fraShop = New GroupBox()
        cmbShop = New ComboBox()
        btnShop = New Button()
        fraResource = New GroupBox()
        btnResourceOk = New Button()
        scrlResource = New HScrollBar()
        lblResource = New Label()
        fraMapItem = New GroupBox()
        picMapItem = New PictureBox()
        btnMapItem = New Button()
        scrlMapItemValue = New HScrollBar()
        scrlMapItem = New HScrollBar()
        lblMapItem = New Label()
        fraTrap = New GroupBox()
        btnTrap = New Button()
        scrlTrap = New HScrollBar()
        lblTrap = New Label()
        ToolStrip = New ToolStrip()
        tsbSave = New ToolStripButton()
        tsbDiscard = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        tsbMapGrid = New ToolStripButton()
        tsbOpacity = New ToolStripButton()
        tsbLight = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        tsbFill = New ToolStripButton()
        tsbClear = New ToolStripButton()
        tsbEyeDropper = New ToolStripButton()
        tsbCopyMap = New ToolStripButton()
        tsbDeleteMap = New ToolStripButton()
        tsbUndo = New ToolStripButton()
        tsbRedo = New ToolStripButton()
        tsbScreenshot = New ToolStripButton()
        tabpages = New TabControl()
        tpTiles = New TabPage()
        cmbAutoTile = New ComboBox()
        Label11 = New Label()
        Label10 = New Label()
        cmbLayers = New ComboBox()
        Label9 = New Label()
        cmbTileSets = New ComboBox()
        tpAttributes = New TabPage()
        optNoXing = New RadioButton()
        optInfo = New RadioButton()
        Label23 = New Label()
        cmbAttribute = New ComboBox()
        optAnimation = New RadioButton()
        optLight = New RadioButton()
        tpNpcs = New TabPage()
        fraNpcs = New GroupBox()
        Label18 = New Label()
        Label17 = New Label()
        cmbNpcList = New ComboBox()
        lstMapNpc = New ListBox()
        ComboBox23 = New ComboBox()
        tpSettings = New TabPage()
        fraMapSettings = New GroupBox()
        Label22 = New Label()
        lstShop = New ComboBox()
        Label8 = New Label()
        lstMoral = New ComboBox()
        fraMapLinks = New GroupBox()
        txtDown = New TextBox()
        txtLeft = New TextBox()
        lblMap = New Label()
        txtRight = New TextBox()
        txtUp = New TextBox()
        fraBootSettings = New GroupBox()
        chkIndoors = New CheckBox()
        chkNoMapRespawn = New CheckBox()
        txtBootMap = New TextBox()
        Label5 = New Label()
        txtBootY = New TextBox()
        Label3 = New Label()
        txtBootX = New TextBox()
        Label4 = New Label()
        fraMaxSizes = New GroupBox()
        txtMaxY = New TextBox()
        Label2 = New Label()
        txtMaxX = New TextBox()
        Label7 = New Label()
        GroupBox2 = New GroupBox()
        btnPreview = New Button()
        lstMusic = New ListBox()
        txtName = New TextBox()
        Label6 = New Label()
        tpDirBlock = New TabPage()
        Label12 = New Label()
        tpEvents = New TabPage()
        lblPasteMode = New Label()
        lblCopyMode = New Label()
        btnPasteEvent = New Button()
        Label16 = New Label()
        btnCopyEvent = New Button()
        Label15 = New Label()
        Label13 = New Label()
        tpEffects = New TabPage()
        GroupBox6 = New GroupBox()
        scrlMapBrightness = New HScrollBar()
        GroupBox5 = New GroupBox()
        cmbParallax = New ComboBox()
        GroupBox4 = New GroupBox()
        cmbPanorama = New ComboBox()
        GroupBox3 = New GroupBox()
        chkTint = New CheckBox()
        lblMapAlpha = New Label()
        lblMapBlue = New Label()
        lblMapGreen = New Label()
        lblMapRed = New Label()
        scrlMapAlpha = New HScrollBar()
        scrlMapBlue = New HScrollBar()
        scrlMapGreen = New HScrollBar()
        scrlMapRed = New HScrollBar()
        GroupBox1 = New GroupBox()
        scrlFogOpacity = New HScrollBar()
        lblFogOpacity = New Label()
        scrlFogSpeed = New HScrollBar()
        lblFogSpeed = New Label()
        scrlIntensity = New HScrollBar()
        lblIntensity = New Label()
        scrlFog = New HScrollBar()
        lblFogIndex = New Label()
        Label14 = New Label()
        cmbWeather = New ComboBox()
        pnlBack.SuspendLayout()
        CType(picBackSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        pnlAttributes.SuspendLayout()
        fraMapLight.SuspendLayout()
        fraAnimation.SuspendLayout()
        fraMapWarp.SuspendLayout()
        fraNpcSpawn.SuspendLayout()
        fraHeal.SuspendLayout()
        fraShop.SuspendLayout()
        fraResource.SuspendLayout()
        fraMapItem.SuspendLayout()
        CType(picMapItem, System.ComponentModel.ISupportInitialize).BeginInit()
        fraTrap.SuspendLayout()
        ToolStrip.SuspendLayout()
        tabpages.SuspendLayout()
        tpTiles.SuspendLayout()
        tpAttributes.SuspendLayout()
        tpNpcs.SuspendLayout()
        fraNpcs.SuspendLayout()
        tpSettings.SuspendLayout()
        fraMapSettings.SuspendLayout()
        fraMapLinks.SuspendLayout()
        fraBootSettings.SuspendLayout()
        fraMaxSizes.SuspendLayout()
        GroupBox2.SuspendLayout()
        tpDirBlock.SuspendLayout()
        tpEvents.SuspendLayout()
        tpEffects.SuspendLayout()
        GroupBox6.SuspendLayout()
        GroupBox5.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnClearAttribute
        ' 
        btnClearAttribute.Location = New Point(344, 569)
        btnClearAttribute.Margin = New Padding(4, 3, 4, 3)
        btnClearAttribute.Name = "btnClearAttribute"
        btnClearAttribute.Size = New Size(192, 29)
        btnClearAttribute.TabIndex = 14
        btnClearAttribute.Text = "Clear All Attributes"
        btnClearAttribute.UseVisualStyleBackColor = True
        ' 
        ' optTrap
        ' 
        optTrap.AutoSize = True
        optTrap.Location = New Point(373, 58)
        optTrap.Margin = New Padding(4, 3, 4, 3)
        optTrap.Name = "optTrap"
        optTrap.Size = New Size(48, 19)
        optTrap.TabIndex = 12
        optTrap.Text = "Trap"
        optTrap.UseVisualStyleBackColor = True
        ' 
        ' optHeal
        ' 
        optHeal.AutoSize = True
        optHeal.Location = New Point(276, 58)
        optHeal.Margin = New Padding(4, 3, 4, 3)
        optHeal.Name = "optHeal"
        optHeal.Size = New Size(49, 19)
        optHeal.TabIndex = 11
        optHeal.Text = "Heal"
        optHeal.UseVisualStyleBackColor = True
        ' 
        ' optBank
        ' 
        optBank.AutoSize = True
        optBank.Location = New Point(118, 58)
        optBank.Margin = New Padding(4, 3, 4, 3)
        optBank.Name = "optBank"
        optBank.Size = New Size(51, 19)
        optBank.TabIndex = 10
        optBank.Text = "Bank"
        optBank.UseVisualStyleBackColor = True
        ' 
        ' optShop
        ' 
        optShop.AutoSize = True
        optShop.Location = New Point(477, 16)
        optShop.Margin = New Padding(4, 3, 4, 3)
        optShop.Name = "optShop"
        optShop.Size = New Size(52, 19)
        optShop.TabIndex = 9
        optShop.Text = "Shop"
        optShop.UseVisualStyleBackColor = True
        ' 
        ' optNPCSpawn
        ' 
        optNPCSpawn.AutoSize = True
        optNPCSpawn.Location = New Point(373, 16)
        optNPCSpawn.Margin = New Padding(4, 3, 4, 3)
        optNPCSpawn.Name = "optNPCSpawn"
        optNPCSpawn.Size = New Size(87, 19)
        optNPCSpawn.TabIndex = 8
        optNPCSpawn.Text = "NPC Spawn"
        optNPCSpawn.UseVisualStyleBackColor = True
        ' 
        ' optResource
        ' 
        optResource.AutoSize = True
        optResource.Location = New Point(12, 58)
        optResource.Margin = New Padding(4, 3, 4, 3)
        optResource.Name = "optResource"
        optResource.Size = New Size(73, 19)
        optResource.TabIndex = 6
        optResource.Text = "Resource"
        optResource.UseVisualStyleBackColor = True
        ' 
        ' optNPCAvoid
        ' 
        optNPCAvoid.AutoSize = True
        optNPCAvoid.Location = New Point(276, 16)
        optNPCAvoid.Margin = New Padding(4, 3, 4, 3)
        optNPCAvoid.Name = "optNPCAvoid"
        optNPCAvoid.Size = New Size(83, 19)
        optNPCAvoid.TabIndex = 3
        optNPCAvoid.Text = "NPC Avoid"
        optNPCAvoid.UseVisualStyleBackColor = True
        ' 
        ' optItem
        ' 
        optItem.AutoSize = True
        optItem.Location = New Point(202, 16)
        optItem.Margin = New Padding(4, 3, 4, 3)
        optItem.Name = "optItem"
        optItem.Size = New Size(49, 19)
        optItem.TabIndex = 2
        optItem.Text = "Item"
        optItem.UseVisualStyleBackColor = True
        ' 
        ' optWarp
        ' 
        optWarp.AutoSize = True
        optWarp.Location = New Point(118, 16)
        optWarp.Margin = New Padding(4, 3, 4, 3)
        optWarp.Name = "optWarp"
        optWarp.Size = New Size(53, 19)
        optWarp.TabIndex = 1
        optWarp.Text = "Warp"
        optWarp.UseVisualStyleBackColor = True
        ' 
        ' optBlocked
        ' 
        optBlocked.AutoSize = True
        optBlocked.Checked = True
        optBlocked.Location = New Point(12, 16)
        optBlocked.Margin = New Padding(4, 3, 4, 3)
        optBlocked.Name = "optBlocked"
        optBlocked.Size = New Size(67, 19)
        optBlocked.TabIndex = 0
        optBlocked.TabStop = True
        optBlocked.Text = "Blocked"
        optBlocked.UseVisualStyleBackColor = True
        ' 
        ' pnlBack
        ' 
        pnlBack.Controls.Add(picBackSelect)
        pnlBack.Location = New Point(7, 9)
        pnlBack.Margin = New Padding(4, 3, 4, 3)
        pnlBack.Name = "pnlBack"
        pnlBack.Size = New Size(526, 532)
        pnlBack.TabIndex = 9
        ' 
        ' picBackSelect
        ' 
        picBackSelect.BackColor = Color.Black
        picBackSelect.Location = New Point(11, 3)
        picBackSelect.Margin = New Padding(4, 3, 4, 3)
        picBackSelect.Name = "picBackSelect"
        picBackSelect.Size = New Size(512, 512)
        picBackSelect.TabIndex = 22
        picBackSelect.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(8, 544)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(193, 15)
        Label1.TabIndex = 11
        Label1.Text = "Drag Mouse to Select Multiple Tiles"
        ' 
        ' pnlAttributes
        ' 
        pnlAttributes.Controls.Add(fraMapLight)
        pnlAttributes.Controls.Add(fraAnimation)
        pnlAttributes.Controls.Add(fraMapWarp)
        pnlAttributes.Controls.Add(fraNpcSpawn)
        pnlAttributes.Controls.Add(fraHeal)
        pnlAttributes.Controls.Add(fraShop)
        pnlAttributes.Controls.Add(fraResource)
        pnlAttributes.Controls.Add(fraMapItem)
        pnlAttributes.Controls.Add(fraTrap)
        pnlAttributes.Location = New Point(555, 56)
        pnlAttributes.Margin = New Padding(4, 3, 4, 3)
        pnlAttributes.Name = "pnlAttributes"
        pnlAttributes.Size = New Size(586, 567)
        pnlAttributes.TabIndex = 12
        pnlAttributes.Visible = False
        ' 
        ' fraMapLight
        ' 
        fraMapLight.Controls.Add(lblRadius)
        fraMapLight.Controls.Add(scrlLight)
        fraMapLight.Controls.Add(chkShadow)
        fraMapLight.Controls.Add(chkFlicker)
        fraMapLight.Controls.Add(btnLight)
        fraMapLight.Location = New Point(394, 147)
        fraMapLight.Margin = New Padding(4, 3, 4, 3)
        fraMapLight.Name = "fraMapLight"
        fraMapLight.Padding = New Padding(4, 3, 4, 3)
        fraMapLight.Size = New Size(172, 138)
        fraMapLight.TabIndex = 19
        fraMapLight.TabStop = False
        fraMapLight.Text = "Light"
        fraMapLight.Visible = False
        ' 
        ' lblRadius
        ' 
        lblRadius.AutoSize = True
        lblRadius.Location = New Point(8, 16)
        lblRadius.Margin = New Padding(4, 0, 4, 0)
        lblRadius.Name = "lblRadius"
        lblRadius.Size = New Size(54, 15)
        lblRadius.TabIndex = 41
        lblRadius.Text = "Radius: 0"
        ' 
        ' scrlLight
        ' 
        scrlLight.Location = New Point(7, 31)
        scrlLight.Name = "scrlLight"
        scrlLight.Size = New Size(165, 17)
        scrlLight.TabIndex = 40
        ' 
        ' chkShadow
        ' 
        chkShadow.AutoSize = True
        chkShadow.Location = New Point(7, 73)
        chkShadow.Name = "chkShadow"
        chkShadow.Size = New Size(73, 19)
        chkShadow.TabIndex = 39
        chkShadow.Text = "Shadows"
        chkShadow.UseVisualStyleBackColor = True
        ' 
        ' chkFlicker
        ' 
        chkFlicker.AutoSize = True
        chkFlicker.Location = New Point(7, 51)
        chkFlicker.Name = "chkFlicker"
        chkFlicker.Size = New Size(60, 19)
        chkFlicker.TabIndex = 38
        chkFlicker.Text = "Flicker"
        chkFlicker.UseVisualStyleBackColor = True
        ' 
        ' btnLight
        ' 
        btnLight.Location = New Point(34, 98)
        btnLight.Margin = New Padding(4, 3, 4, 3)
        btnLight.Name = "btnLight"
        btnLight.Size = New Size(105, 32)
        btnLight.TabIndex = 6
        btnLight.Text = "Accept"
        btnLight.UseVisualStyleBackColor = True
        ' 
        ' fraAnimation
        ' 
        fraAnimation.Controls.Add(cmbAnimation)
        fraAnimation.Controls.Add(brnAnimation)
        fraAnimation.Location = New Point(214, 291)
        fraAnimation.Margin = New Padding(4, 3, 4, 3)
        fraAnimation.Name = "fraAnimation"
        fraAnimation.Padding = New Padding(4, 3, 4, 3)
        fraAnimation.Size = New Size(203, 130)
        fraAnimation.TabIndex = 17
        fraAnimation.TabStop = False
        fraAnimation.Text = "Animation"
        fraAnimation.Visible = False
        ' 
        ' cmbAnimation
        ' 
        cmbAnimation.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAnimation.FormattingEnabled = True
        cmbAnimation.Items.AddRange(New Object() {"Heal HP", "Heal MP"})
        cmbAnimation.Location = New Point(7, 22)
        cmbAnimation.Margin = New Padding(4, 3, 4, 3)
        cmbAnimation.Name = "cmbAnimation"
        cmbAnimation.Size = New Size(180, 23)
        cmbAnimation.TabIndex = 37
        ' 
        ' brnAnimation
        ' 
        brnAnimation.Location = New Point(43, 88)
        brnAnimation.Margin = New Padding(4, 3, 4, 3)
        brnAnimation.Name = "brnAnimation"
        brnAnimation.Size = New Size(105, 32)
        brnAnimation.TabIndex = 6
        brnAnimation.Text = "Accept"
        brnAnimation.UseVisualStyleBackColor = True
        ' 
        ' fraMapWarp
        ' 
        fraMapWarp.Controls.Add(btnMapWarp)
        fraMapWarp.Controls.Add(scrlMapWarpY)
        fraMapWarp.Controls.Add(scrlMapWarpX)
        fraMapWarp.Controls.Add(scrlMapWarpMap)
        fraMapWarp.Controls.Add(lblMapWarpY)
        fraMapWarp.Controls.Add(lblMapWarpX)
        fraMapWarp.Controls.Add(lblMapWarpMap)
        fraMapWarp.Location = New Point(10, 427)
        fraMapWarp.Margin = New Padding(4, 3, 4, 3)
        fraMapWarp.Name = "fraMapWarp"
        fraMapWarp.Padding = New Padding(4, 3, 4, 3)
        fraMapWarp.Size = New Size(294, 137)
        fraMapWarp.TabIndex = 0
        fraMapWarp.TabStop = False
        fraMapWarp.Text = "Map Warp"
        ' 
        ' btnMapWarp
        ' 
        btnMapWarp.Location = New Point(93, 102)
        btnMapWarp.Margin = New Padding(4, 3, 4, 3)
        btnMapWarp.Name = "btnMapWarp"
        btnMapWarp.Size = New Size(105, 32)
        btnMapWarp.TabIndex = 6
        btnMapWarp.Text = "Accept"
        btnMapWarp.UseVisualStyleBackColor = True
        ' 
        ' scrlMapWarpY
        ' 
        scrlMapWarpY.Location = New Point(72, 73)
        scrlMapWarpY.Name = "scrlMapWarpY"
        scrlMapWarpY.Size = New Size(188, 18)
        scrlMapWarpY.TabIndex = 5
        ' 
        ' scrlMapWarpX
        ' 
        scrlMapWarpX.Location = New Point(72, 47)
        scrlMapWarpX.Name = "scrlMapWarpX"
        scrlMapWarpX.Size = New Size(188, 18)
        scrlMapWarpX.TabIndex = 4
        ' 
        ' scrlMapWarpMap
        ' 
        scrlMapWarpMap.Location = New Point(72, 23)
        scrlMapWarpMap.Name = "scrlMapWarpMap"
        scrlMapWarpMap.Size = New Size(188, 18)
        scrlMapWarpMap.TabIndex = 3
        ' 
        ' lblMapWarpY
        ' 
        lblMapWarpY.AutoSize = True
        lblMapWarpY.Location = New Point(8, 77)
        lblMapWarpY.Margin = New Padding(4, 0, 4, 0)
        lblMapWarpY.Name = "lblMapWarpY"
        lblMapWarpY.Size = New Size(26, 15)
        lblMapWarpY.TabIndex = 2
        lblMapWarpY.Text = "Y: 1"
        ' 
        ' lblMapWarpX
        ' 
        lblMapWarpX.AutoSize = True
        lblMapWarpX.Location = New Point(8, 53)
        lblMapWarpX.Margin = New Padding(4, 0, 4, 0)
        lblMapWarpX.Name = "lblMapWarpX"
        lblMapWarpX.Size = New Size(26, 15)
        lblMapWarpX.TabIndex = 1
        lblMapWarpX.Text = "X: 1"
        ' 
        ' lblMapWarpMap
        ' 
        lblMapWarpMap.AutoSize = True
        lblMapWarpMap.Location = New Point(7, 29)
        lblMapWarpMap.Margin = New Padding(4, 0, 4, 0)
        lblMapWarpMap.Name = "lblMapWarpMap"
        lblMapWarpMap.Size = New Size(43, 15)
        lblMapWarpMap.TabIndex = 0
        lblMapWarpMap.Text = "Map: 1"
        ' 
        ' fraNpcSpawn
        ' 
        fraNpcSpawn.Controls.Add(lstNpc)
        fraNpcSpawn.Controls.Add(btnNpcSpawn)
        fraNpcSpawn.Controls.Add(scrlNpcDir)
        fraNpcSpawn.Controls.Add(lblNpcDir)
        fraNpcSpawn.Location = New Point(4, 7)
        fraNpcSpawn.Margin = New Padding(4, 3, 4, 3)
        fraNpcSpawn.Name = "fraNpcSpawn"
        fraNpcSpawn.Padding = New Padding(4, 3, 4, 3)
        fraNpcSpawn.Size = New Size(203, 130)
        fraNpcSpawn.TabIndex = 11
        fraNpcSpawn.TabStop = False
        fraNpcSpawn.Text = "Npc Spawn"
        ' 
        ' lstNpc
        ' 
        lstNpc.DropDownStyle = ComboBoxStyle.DropDownList
        lstNpc.FormattingEnabled = True
        lstNpc.Location = New Point(7, 18)
        lstNpc.Margin = New Padding(4, 3, 4, 3)
        lstNpc.Name = "lstNpc"
        lstNpc.Size = New Size(180, 23)
        lstNpc.TabIndex = 37
        ' 
        ' btnNpcSpawn
        ' 
        btnNpcSpawn.Location = New Point(46, 88)
        btnNpcSpawn.Margin = New Padding(4, 3, 4, 3)
        btnNpcSpawn.Name = "btnNpcSpawn"
        btnNpcSpawn.Size = New Size(105, 32)
        btnNpcSpawn.TabIndex = 6
        btnNpcSpawn.Text = "Accept"
        btnNpcSpawn.UseVisualStyleBackColor = True
        ' 
        ' scrlNpcDir
        ' 
        scrlNpcDir.LargeChange = 1
        scrlNpcDir.Location = New Point(9, 63)
        scrlNpcDir.Maximum = 3
        scrlNpcDir.Name = "scrlNpcDir"
        scrlNpcDir.Size = New Size(178, 18)
        scrlNpcDir.TabIndex = 3
        ' 
        ' lblNpcDir
        ' 
        lblNpcDir.AutoSize = True
        lblNpcDir.Location = New Point(6, 46)
        lblNpcDir.Margin = New Padding(4, 0, 4, 0)
        lblNpcDir.Name = "lblNpcDir"
        lblNpcDir.Size = New Size(76, 15)
        lblNpcDir.TabIndex = 0
        lblNpcDir.Text = "Direction: Up"
        ' 
        ' fraHeal
        ' 
        fraHeal.Controls.Add(scrlHeal)
        fraHeal.Controls.Add(lblHeal)
        fraHeal.Controls.Add(cmbHeal)
        fraHeal.Controls.Add(btnHeal)
        fraHeal.Location = New Point(4, 290)
        fraHeal.Margin = New Padding(4, 3, 4, 3)
        fraHeal.Name = "fraHeal"
        fraHeal.Padding = New Padding(4, 3, 4, 3)
        fraHeal.Size = New Size(203, 130)
        fraHeal.TabIndex = 15
        fraHeal.TabStop = False
        fraHeal.Text = "Heal"
        ' 
        ' scrlHeal
        ' 
        scrlHeal.Location = New Point(5, 65)
        scrlHeal.Name = "scrlHeal"
        scrlHeal.Size = New Size(181, 17)
        scrlHeal.TabIndex = 39
        ' 
        ' lblHeal
        ' 
        lblHeal.AutoSize = True
        lblHeal.Location = New Point(4, 50)
        lblHeal.Margin = New Padding(4, 0, 4, 0)
        lblHeal.Name = "lblHeal"
        lblHeal.Size = New Size(63, 15)
        lblHeal.TabIndex = 38
        lblHeal.Text = "Amount: 0"
        ' 
        ' cmbHeal
        ' 
        cmbHeal.DropDownStyle = ComboBoxStyle.DropDownList
        cmbHeal.FormattingEnabled = True
        cmbHeal.Items.AddRange(New Object() {"Heal HP", "Heal MP"})
        cmbHeal.Location = New Point(7, 22)
        cmbHeal.Margin = New Padding(4, 3, 4, 3)
        cmbHeal.Name = "cmbHeal"
        cmbHeal.Size = New Size(180, 23)
        cmbHeal.TabIndex = 37
        ' 
        ' btnHeal
        ' 
        btnHeal.Location = New Point(43, 88)
        btnHeal.Margin = New Padding(4, 3, 4, 3)
        btnHeal.Name = "btnHeal"
        btnHeal.Size = New Size(105, 32)
        btnHeal.TabIndex = 6
        btnHeal.Text = "Accept"
        btnHeal.UseVisualStyleBackColor = True
        ' 
        ' fraShop
        ' 
        fraShop.Controls.Add(cmbShop)
        fraShop.Controls.Add(btnShop)
        fraShop.Location = New Point(394, 9)
        fraShop.Margin = New Padding(4, 3, 4, 3)
        fraShop.Name = "fraShop"
        fraShop.Padding = New Padding(4, 3, 4, 3)
        fraShop.Size = New Size(172, 138)
        fraShop.TabIndex = 12
        fraShop.TabStop = False
        fraShop.Text = "Shop"
        ' 
        ' cmbShop
        ' 
        cmbShop.DropDownStyle = ComboBoxStyle.DropDownList
        cmbShop.FormattingEnabled = True
        cmbShop.Location = New Point(7, 22)
        cmbShop.Margin = New Padding(4, 3, 4, 3)
        cmbShop.Name = "cmbShop"
        cmbShop.Size = New Size(154, 23)
        cmbShop.TabIndex = 37
        ' 
        ' btnShop
        ' 
        btnShop.Location = New Point(34, 98)
        btnShop.Margin = New Padding(4, 3, 4, 3)
        btnShop.Name = "btnShop"
        btnShop.Size = New Size(105, 32)
        btnShop.TabIndex = 6
        btnShop.Text = "Accept"
        btnShop.UseVisualStyleBackColor = True
        ' 
        ' fraResource
        ' 
        fraResource.Controls.Add(btnResourceOk)
        fraResource.Controls.Add(scrlResource)
        fraResource.Controls.Add(lblResource)
        fraResource.Location = New Point(214, 7)
        fraResource.Margin = New Padding(4, 3, 4, 3)
        fraResource.Name = "fraResource"
        fraResource.Padding = New Padding(4, 3, 4, 3)
        fraResource.Size = New Size(172, 130)
        fraResource.TabIndex = 10
        fraResource.TabStop = False
        fraResource.Text = "Resource"
        ' 
        ' btnResourceOk
        ' 
        btnResourceOk.Location = New Point(33, 88)
        btnResourceOk.Margin = New Padding(4, 3, 4, 3)
        btnResourceOk.Name = "btnResourceOk"
        btnResourceOk.Size = New Size(105, 32)
        btnResourceOk.TabIndex = 6
        btnResourceOk.Text = "Accept"
        btnResourceOk.UseVisualStyleBackColor = True
        ' 
        ' scrlResource
        ' 
        scrlResource.Location = New Point(4, 42)
        scrlResource.Name = "scrlResource"
        scrlResource.Size = New Size(159, 18)
        scrlResource.TabIndex = 3
        ' 
        ' lblResource
        ' 
        lblResource.AutoSize = True
        lblResource.Location = New Point(0, 18)
        lblResource.Margin = New Padding(4, 0, 4, 0)
        lblResource.Name = "lblResource"
        lblResource.Size = New Size(45, 15)
        lblResource.TabIndex = 0
        lblResource.Text = "Object:"
        ' 
        ' fraMapItem
        ' 
        fraMapItem.Controls.Add(picMapItem)
        fraMapItem.Controls.Add(btnMapItem)
        fraMapItem.Controls.Add(scrlMapItemValue)
        fraMapItem.Controls.Add(scrlMapItem)
        fraMapItem.Controls.Add(lblMapItem)
        fraMapItem.Location = New Point(4, 137)
        fraMapItem.Margin = New Padding(4, 3, 4, 3)
        fraMapItem.Name = "fraMapItem"
        fraMapItem.Padding = New Padding(4, 3, 4, 3)
        fraMapItem.Size = New Size(203, 137)
        fraMapItem.TabIndex = 7
        fraMapItem.TabStop = False
        fraMapItem.Text = "Map Item"
        ' 
        ' picMapItem
        ' 
        picMapItem.BackColor = Color.Black
        picMapItem.Location = New Point(155, 42)
        picMapItem.Margin = New Padding(4, 3, 4, 3)
        picMapItem.Name = "picMapItem"
        picMapItem.Size = New Size(37, 37)
        picMapItem.TabIndex = 7
        picMapItem.TabStop = False
        ' 
        ' btnMapItem
        ' 
        btnMapItem.Location = New Point(46, 97)
        btnMapItem.Margin = New Padding(4, 3, 4, 3)
        btnMapItem.Name = "btnMapItem"
        btnMapItem.Size = New Size(105, 32)
        btnMapItem.TabIndex = 6
        btnMapItem.Text = "Accept"
        btnMapItem.UseVisualStyleBackColor = True
        ' 
        ' scrlMapItemValue
        ' 
        scrlMapItemValue.Location = New Point(10, 68)
        scrlMapItemValue.Name = "scrlMapItemValue"
        scrlMapItemValue.Size = New Size(140, 18)
        scrlMapItemValue.TabIndex = 4
        ' 
        ' scrlMapItem
        ' 
        scrlMapItem.Location = New Point(10, 43)
        scrlMapItem.Name = "scrlMapItem"
        scrlMapItem.Size = New Size(140, 18)
        scrlMapItem.TabIndex = 3
        ' 
        ' lblMapItem
        ' 
        lblMapItem.AutoSize = True
        lblMapItem.Location = New Point(7, 25)
        lblMapItem.Margin = New Padding(4, 0, 4, 0)
        lblMapItem.Name = "lblMapItem"
        lblMapItem.Size = New Size(50, 15)
        lblMapItem.TabIndex = 0
        lblMapItem.Text = "None x0"
        ' 
        ' fraTrap
        ' 
        fraTrap.Controls.Add(btnTrap)
        fraTrap.Controls.Add(scrlTrap)
        fraTrap.Controls.Add(lblTrap)
        fraTrap.Location = New Point(214, 144)
        fraTrap.Margin = New Padding(4, 3, 4, 3)
        fraTrap.Name = "fraTrap"
        fraTrap.Padding = New Padding(4, 3, 4, 3)
        fraTrap.Size = New Size(172, 138)
        fraTrap.TabIndex = 16
        fraTrap.TabStop = False
        fraTrap.Text = "Trap"
        ' 
        ' btnTrap
        ' 
        btnTrap.Location = New Point(33, 98)
        btnTrap.Margin = New Padding(4, 3, 4, 3)
        btnTrap.Name = "btnTrap"
        btnTrap.Size = New Size(105, 32)
        btnTrap.TabIndex = 42
        btnTrap.Text = "Accept"
        btnTrap.UseVisualStyleBackColor = True
        ' 
        ' scrlTrap
        ' 
        scrlTrap.Location = New Point(13, 38)
        scrlTrap.Name = "scrlTrap"
        scrlTrap.Size = New Size(149, 17)
        scrlTrap.TabIndex = 41
        ' 
        ' lblTrap
        ' 
        lblTrap.AutoSize = True
        lblTrap.Location = New Point(7, 18)
        lblTrap.Margin = New Padding(4, 0, 4, 0)
        lblTrap.Name = "lblTrap"
        lblTrap.Size = New Size(63, 15)
        lblTrap.TabIndex = 40
        lblTrap.Text = "Amount: 0"
        ' 
        ' ToolStrip
        ' 
        ToolStrip.ImageScalingSize = New Size(24, 24)
        ToolStrip.Items.AddRange(New ToolStripItem() {tsbSave, tsbDiscard, ToolStripSeparator1, tsbMapGrid, tsbOpacity, tsbLight, ToolStripSeparator2, tsbFill, tsbClear, tsbEyeDropper, tsbCopyMap, tsbDeleteMap, tsbUndo, tsbRedo, tsbScreenshot})
        ToolStrip.Location = New Point(0, 0)
        ToolStrip.Name = "ToolStrip"
        ToolStrip.Padding = New Padding(0, 0, 2, 0)
        ToolStrip.Size = New Size(552, 31)
        ToolStrip.TabIndex = 13
        ToolStrip.Text = "ToolStrip1"
        ' 
        ' tsbSave
        ' 
        tsbSave.Image = CType(resources.GetObject("tsbSave.Image"), Image)
        tsbSave.ImageTransparentColor = Color.Magenta
        tsbSave.Name = "tsbSave"
        tsbSave.Size = New Size(28, 28)
        tsbSave.ToolTipText = "Save"
        ' 
        ' tsbDiscard
        ' 
        tsbDiscard.Image = CType(resources.GetObject("tsbDiscard.Image"), Image)
        tsbDiscard.ImageTransparentColor = Color.Magenta
        tsbDiscard.Name = "tsbDiscard"
        tsbDiscard.Size = New Size(28, 28)
        tsbDiscard.ToolTipText = "Discard"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 31)
        ' 
        ' tsbMapGrid
        ' 
        tsbMapGrid.Image = CType(resources.GetObject("tsbMapGrid.Image"), Image)
        tsbMapGrid.ImageTransparentColor = Color.Magenta
        tsbMapGrid.Name = "tsbMapGrid"
        tsbMapGrid.Size = New Size(28, 28)
        tsbMapGrid.Tag = "Map Grid"
        ' 
        ' tsbOpacity
        ' 
        tsbOpacity.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbOpacity.Image = CType(resources.GetObject("tsbOpacity.Image"), Image)
        tsbOpacity.ImageTransparentColor = Color.Magenta
        tsbOpacity.Name = "tsbOpacity"
        tsbOpacity.Size = New Size(28, 28)
        tsbOpacity.Text = "ToolStripButton1"
        tsbOpacity.ToolTipText = "Opacity"
        ' 
        ' tsbLight
        ' 
        tsbLight.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbLight.Image = CType(resources.GetObject("tsbLight.Image"), Image)
        tsbLight.ImageTransparentColor = Color.Magenta
        tsbLight.Name = "tsbLight"
        tsbLight.Size = New Size(28, 28)
        tsbLight.ToolTipText = "Light"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 31)
        ' 
        ' tsbFill
        ' 
        tsbFill.Image = CType(resources.GetObject("tsbFill.Image"), Image)
        tsbFill.ImageTransparentColor = Color.Magenta
        tsbFill.Name = "tsbFill"
        tsbFill.Size = New Size(28, 28)
        tsbFill.Tag = "Fill"
        tsbFill.ToolTipText = "Fill Layer"
        ' 
        ' tsbClear
        ' 
        tsbClear.Image = CType(resources.GetObject("tsbClear.Image"), Image)
        tsbClear.ImageTransparentColor = Color.Magenta
        tsbClear.Name = "tsbClear"
        tsbClear.Size = New Size(28, 28)
        tsbClear.ToolTipText = "Erase"
        ' 
        ' tsbEyeDropper
        ' 
        tsbEyeDropper.Image = CType(resources.GetObject("tsbEyeDropper.Image"), Image)
        tsbEyeDropper.ImageTransparentColor = Color.Magenta
        tsbEyeDropper.Name = "tsbEyeDropper"
        tsbEyeDropper.Size = New Size(28, 28)
        tsbEyeDropper.ToolTipText = "Eye Dropper"
        ' 
        ' tsbCopyMap
        ' 
        tsbCopyMap.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbCopyMap.Image = CType(resources.GetObject("tsbCopyMap.Image"), Image)
        tsbCopyMap.ImageTransparentColor = Color.Magenta
        tsbCopyMap.Name = "tsbCopyMap"
        tsbCopyMap.Size = New Size(28, 28)
        tsbCopyMap.ToolTipText = "Copy"
        ' 
        ' tsbDeleteMap
        ' 
        tsbDeleteMap.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbDeleteMap.Image = CType(resources.GetObject("tsbDeleteMap.Image"), Image)
        tsbDeleteMap.ImageTransparentColor = Color.Magenta
        tsbDeleteMap.Name = "tsbDeleteMap"
        tsbDeleteMap.Size = New Size(28, 28)
        tsbDeleteMap.ToolTipText = "Delete Map"
        ' 
        ' tsbUndo
        ' 
        tsbUndo.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbUndo.Image = CType(resources.GetObject("tsbUndo.Image"), Image)
        tsbUndo.ImageTransparentColor = Color.Magenta
        tsbUndo.Name = "tsbUndo"
        tsbUndo.Size = New Size(28, 28)
        tsbUndo.ToolTipText = "Undo"
        ' 
        ' tsbRedo
        ' 
        tsbRedo.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbRedo.Image = CType(resources.GetObject("tsbRedo.Image"), Image)
        tsbRedo.ImageTransparentColor = Color.Magenta
        tsbRedo.Name = "tsbRedo"
        tsbRedo.Size = New Size(28, 28)
        tsbRedo.ToolTipText = "Redo"
        ' 
        ' tsbScreenshot
        ' 
        tsbScreenshot.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbScreenshot.Image = CType(resources.GetObject("tsbScreenshot.Image"), Image)
        tsbScreenshot.ImageTransparentColor = Color.Magenta
        tsbScreenshot.Name = "tsbScreenshot"
        tsbScreenshot.Size = New Size(28, 28)
        tsbScreenshot.ToolTipText = "Screenshot"
        ' 
        ' tabpages
        ' 
        tabpages.Controls.Add(tpTiles)
        tabpages.Controls.Add(tpAttributes)
        tabpages.Controls.Add(tpNpcs)
        tabpages.Controls.Add(tpSettings)
        tabpages.Controls.Add(tpDirBlock)
        tabpages.Controls.Add(tpEvents)
        tabpages.Controls.Add(tpEffects)
        tabpages.Location = New Point(5, 32)
        tabpages.Margin = New Padding(4, 3, 4, 3)
        tabpages.Name = "tabpages"
        tabpages.SelectedIndex = 0
        tabpages.Size = New Size(550, 629)
        tabpages.TabIndex = 14
        ' 
        ' tpTiles
        ' 
        tpTiles.Controls.Add(cmbAutoTile)
        tpTiles.Controls.Add(Label11)
        tpTiles.Controls.Add(Label10)
        tpTiles.Controls.Add(cmbLayers)
        tpTiles.Controls.Add(Label9)
        tpTiles.Controls.Add(cmbTileSets)
        tpTiles.Controls.Add(pnlBack)
        tpTiles.Controls.Add(Label1)
        tpTiles.Location = New Point(4, 24)
        tpTiles.Margin = New Padding(4, 3, 4, 3)
        tpTiles.Name = "tpTiles"
        tpTiles.Padding = New Padding(4, 3, 4, 3)
        tpTiles.Size = New Size(542, 601)
        tpTiles.TabIndex = 0
        tpTiles.Text = "Tiles"
        tpTiles.UseVisualStyleBackColor = True
        ' 
        ' cmbAutoTile
        ' 
        cmbAutoTile.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAutoTile.FormattingEnabled = True
        cmbAutoTile.Items.AddRange(New Object() {"Normal", "AutoTile (VX)", "Fake (VX)", "Animated (VX)", "Cliff (VX)", "Waterfall (VX)"})
        cmbAutoTile.Location = New Point(428, 566)
        cmbAutoTile.Margin = New Padding(4, 3, 4, 3)
        cmbAutoTile.Name = "cmbAutoTile"
        cmbAutoTile.Size = New Size(110, 23)
        cmbAutoTile.TabIndex = 17
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(364, 570)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(52, 15)
        Label11.TabIndex = 16
        Label11.Text = "Autotile:"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(167, 570)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(38, 15)
        Label10.TabIndex = 15
        Label10.Text = "Layer:"
        ' 
        ' cmbLayers
        ' 
        cmbLayers.DropDownStyle = ComboBoxStyle.DropDownList
        cmbLayers.FormattingEnabled = True
        cmbLayers.Items.AddRange(New Object() {"Ground", "Mask", "Mask 2 Anim", "Cover", "Cover 2 Anim", "Fringe", "Fringe Anim", "Roof", "Roof Anim"})
        cmbLayers.Location = New Point(216, 566)
        cmbLayers.Margin = New Padding(4, 3, 4, 3)
        cmbLayers.Name = "cmbLayers"
        cmbLayers.Size = New Size(112, 23)
        cmbLayers.TabIndex = 14
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(8, 570)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(44, 15)
        Label9.TabIndex = 13
        Label9.Text = "Tileset:"
        ' 
        ' cmbTileSets
        ' 
        cmbTileSets.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTileSets.FormattingEnabled = True
        cmbTileSets.Location = New Point(63, 566)
        cmbTileSets.Margin = New Padding(4, 3, 4, 3)
        cmbTileSets.Name = "cmbTileSets"
        cmbTileSets.Size = New Size(68, 23)
        cmbTileSets.TabIndex = 12
        ' 
        ' tpAttributes
        ' 
        tpAttributes.Controls.Add(optNoXing)
        tpAttributes.Controls.Add(optInfo)
        tpAttributes.Controls.Add(Label23)
        tpAttributes.Controls.Add(cmbAttribute)
        tpAttributes.Controls.Add(optAnimation)
        tpAttributes.Controls.Add(optLight)
        tpAttributes.Controls.Add(btnClearAttribute)
        tpAttributes.Controls.Add(optTrap)
        tpAttributes.Controls.Add(optBlocked)
        tpAttributes.Controls.Add(optHeal)
        tpAttributes.Controls.Add(optWarp)
        tpAttributes.Controls.Add(optBank)
        tpAttributes.Controls.Add(optItem)
        tpAttributes.Controls.Add(optShop)
        tpAttributes.Controls.Add(optNPCAvoid)
        tpAttributes.Controls.Add(optNPCSpawn)
        tpAttributes.Controls.Add(optResource)
        tpAttributes.Location = New Point(4, 24)
        tpAttributes.Margin = New Padding(4, 3, 4, 3)
        tpAttributes.Name = "tpAttributes"
        tpAttributes.Padding = New Padding(4, 3, 4, 3)
        tpAttributes.Size = New Size(542, 601)
        tpAttributes.TabIndex = 3
        tpAttributes.Text = "Attributes"
        tpAttributes.UseVisualStyleBackColor = True
        ' 
        ' optNoXing
        ' 
        optNoXing.AutoSize = True
        optNoXing.Checked = True
        optNoXing.Location = New Point(12, 99)
        optNoXing.Margin = New Padding(4, 3, 4, 3)
        optNoXing.Name = "optNoXing"
        optNoXing.Size = New Size(68, 19)
        optNoXing.TabIndex = 23
        optNoXing.TabStop = True
        optNoXing.Text = "No Xing"
        optNoXing.UseVisualStyleBackColor = True
        ' 
        ' optInfo
        ' 
        optInfo.AutoSize = True
        optInfo.Location = New Point(137, 575)
        optInfo.Margin = New Padding(4, 3, 4, 3)
        optInfo.Name = "optInfo"
        optInfo.Size = New Size(46, 19)
        optInfo.TabIndex = 22
        optInfo.Text = "Info"
        optInfo.UseVisualStyleBackColor = True
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(8, 576)
        Label23.Margin = New Padding(4, 0, 4, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(35, 15)
        Label23.TabIndex = 21
        Label23.Text = "Type:"
        ' 
        ' cmbAttribute
        ' 
        cmbAttribute.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAttribute.FormattingEnabled = True
        cmbAttribute.Items.AddRange(New Object() {"Layer 1", "Layer 2"})
        cmbAttribute.Location = New Point(62, 574)
        cmbAttribute.Margin = New Padding(4, 3, 4, 3)
        cmbAttribute.Name = "cmbAttribute"
        cmbAttribute.Size = New Size(68, 23)
        cmbAttribute.TabIndex = 20
        ' 
        ' optAnimation
        ' 
        optAnimation.AutoSize = True
        optAnimation.Location = New Point(448, 58)
        optAnimation.Margin = New Padding(4, 3, 4, 3)
        optAnimation.Name = "optAnimation"
        optAnimation.Size = New Size(81, 19)
        optAnimation.TabIndex = 19
        optAnimation.Text = "Animation"
        optAnimation.UseVisualStyleBackColor = True
        ' 
        ' optLight
        ' 
        optLight.AutoSize = True
        optLight.Location = New Point(202, 58)
        optLight.Margin = New Padding(4, 3, 4, 3)
        optLight.Name = "optLight"
        optLight.Size = New Size(52, 19)
        optLight.TabIndex = 18
        optLight.Text = "Light"
        optLight.UseVisualStyleBackColor = True
        ' 
        ' tpNpcs
        ' 
        tpNpcs.Controls.Add(fraNpcs)
        tpNpcs.Location = New Point(4, 24)
        tpNpcs.Margin = New Padding(4, 3, 4, 3)
        tpNpcs.Name = "tpNpcs"
        tpNpcs.Padding = New Padding(4, 3, 4, 3)
        tpNpcs.Size = New Size(542, 601)
        tpNpcs.TabIndex = 1
        tpNpcs.Text = "NPCs"
        tpNpcs.UseVisualStyleBackColor = True
        ' 
        ' fraNpcs
        ' 
        fraNpcs.Controls.Add(Label18)
        fraNpcs.Controls.Add(Label17)
        fraNpcs.Controls.Add(cmbNpcList)
        fraNpcs.Controls.Add(lstMapNpc)
        fraNpcs.Controls.Add(ComboBox23)
        fraNpcs.Location = New Point(7, 9)
        fraNpcs.Margin = New Padding(4, 3, 4, 3)
        fraNpcs.Name = "fraNpcs"
        fraNpcs.Padding = New Padding(4, 3, 4, 3)
        fraNpcs.Size = New Size(559, 492)
        fraNpcs.TabIndex = 11
        fraNpcs.TabStop = False
        fraNpcs.Text = "NPCs"
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(228, 33)
        Label18.Margin = New Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(77, 15)
        Label18.TabIndex = 72
        Label18.Text = "2. Select NPC"
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(7, 33)
        Label17.Margin = New Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(64, 15)
        Label17.TabIndex = 71
        Label17.Text = "1. NPC LIst"
        ' 
        ' cmbNpcList
        ' 
        cmbNpcList.FormattingEnabled = True
        cmbNpcList.Location = New Point(228, 52)
        cmbNpcList.Margin = New Padding(4, 3, 4, 3)
        cmbNpcList.Name = "cmbNpcList"
        cmbNpcList.Size = New Size(299, 23)
        cmbNpcList.TabIndex = 70
        ' 
        ' lstMapNpc
        ' 
        lstMapNpc.FormattingEnabled = True
        lstMapNpc.ItemHeight = 15
        lstMapNpc.Location = New Point(10, 52)
        lstMapNpc.Margin = New Padding(4, 3, 4, 3)
        lstMapNpc.Name = "lstMapNpc"
        lstMapNpc.Size = New Size(210, 424)
        lstMapNpc.TabIndex = 69
        ' 
        ' ComboBox23
        ' 
        ComboBox23.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox23.FormattingEnabled = True
        ComboBox23.Location = New Point(398, 541)
        ComboBox23.Margin = New Padding(4, 3, 4, 3)
        ComboBox23.Name = "ComboBox23"
        ComboBox23.Size = New Size(154, 23)
        ComboBox23.TabIndex = 68
        ' 
        ' tpSettings
        ' 
        tpSettings.Controls.Add(fraMapSettings)
        tpSettings.Controls.Add(fraMapLinks)
        tpSettings.Controls.Add(fraBootSettings)
        tpSettings.Controls.Add(fraMaxSizes)
        tpSettings.Controls.Add(GroupBox2)
        tpSettings.Controls.Add(txtName)
        tpSettings.Controls.Add(Label6)
        tpSettings.Location = New Point(4, 24)
        tpSettings.Margin = New Padding(4, 3, 4, 3)
        tpSettings.Name = "tpSettings"
        tpSettings.Padding = New Padding(4, 3, 4, 3)
        tpSettings.Size = New Size(542, 601)
        tpSettings.TabIndex = 2
        tpSettings.Text = "Settings"
        tpSettings.UseVisualStyleBackColor = True
        ' 
        ' fraMapSettings
        ' 
        fraMapSettings.Controls.Add(Label22)
        fraMapSettings.Controls.Add(lstShop)
        fraMapSettings.Controls.Add(Label8)
        fraMapSettings.Controls.Add(lstMoral)
        fraMapSettings.Location = New Point(7, 37)
        fraMapSettings.Margin = New Padding(4, 3, 4, 3)
        fraMapSettings.Name = "fraMapSettings"
        fraMapSettings.Padding = New Padding(4, 3, 4, 3)
        fraMapSettings.Size = New Size(271, 79)
        fraMapSettings.TabIndex = 15
        fraMapSettings.TabStop = False
        fraMapSettings.Text = "Settings"
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(4, 41)
        Label22.Margin = New Padding(4, 0, 4, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(37, 15)
        Label22.TabIndex = 40
        Label22.Text = "Shop:"
        ' 
        ' lstShop
        ' 
        lstShop.DropDownStyle = ComboBoxStyle.DropDownList
        lstShop.FormattingEnabled = True
        lstShop.Location = New Point(52, 40)
        lstShop.Margin = New Padding(4, 3, 4, 3)
        lstShop.Name = "lstShop"
        lstShop.Size = New Size(210, 23)
        lstShop.TabIndex = 39
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(4, 17)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(41, 15)
        Label8.TabIndex = 38
        Label8.Text = "Moral:"
        ' 
        ' lstMoral
        ' 
        lstMoral.DropDownStyle = ComboBoxStyle.DropDownList
        lstMoral.FormattingEnabled = True
        lstMoral.Location = New Point(52, 14)
        lstMoral.Margin = New Padding(4, 3, 4, 3)
        lstMoral.Name = "lstMoral"
        lstMoral.Size = New Size(210, 23)
        lstMoral.TabIndex = 37
        ' 
        ' fraMapLinks
        ' 
        fraMapLinks.Controls.Add(txtDown)
        fraMapLinks.Controls.Add(txtLeft)
        fraMapLinks.Controls.Add(lblMap)
        fraMapLinks.Controls.Add(txtRight)
        fraMapLinks.Controls.Add(txtUp)
        fraMapLinks.Location = New Point(7, 122)
        fraMapLinks.Margin = New Padding(4, 3, 4, 3)
        fraMapLinks.Name = "fraMapLinks"
        fraMapLinks.Padding = New Padding(4, 3, 4, 3)
        fraMapLinks.Size = New Size(271, 129)
        fraMapLinks.TabIndex = 14
        fraMapLinks.TabStop = False
        fraMapLinks.Text = "Borders"
        ' 
        ' txtDown
        ' 
        txtDown.Location = New Point(105, 99)
        txtDown.Margin = New Padding(4, 3, 4, 3)
        txtDown.Name = "txtDown"
        txtDown.Size = New Size(58, 23)
        txtDown.TabIndex = 6
        txtDown.Text = "0"
        ' 
        ' txtLeft
        ' 
        txtLeft.Location = New Point(8, 54)
        txtLeft.Margin = New Padding(4, 3, 4, 3)
        txtLeft.Name = "txtLeft"
        txtLeft.Size = New Size(50, 23)
        txtLeft.TabIndex = 5
        txtLeft.Text = "0"
        ' 
        ' lblMap
        ' 
        lblMap.AutoSize = True
        lblMap.Location = New Point(104, 58)
        lblMap.Margin = New Padding(4, 0, 4, 0)
        lblMap.Name = "lblMap"
        lblMap.Size = New Size(43, 15)
        lblMap.TabIndex = 4
        lblMap.Text = "Map: 0"
        ' 
        ' txtRight
        ' 
        txtRight.Location = New Point(206, 54)
        txtRight.Margin = New Padding(4, 3, 4, 3)
        txtRight.Name = "txtRight"
        txtRight.Size = New Size(58, 23)
        txtRight.TabIndex = 3
        txtRight.Text = "0"
        ' 
        ' txtUp
        ' 
        txtUp.Location = New Point(104, 12)
        txtUp.Margin = New Padding(4, 3, 4, 3)
        txtUp.Name = "txtUp"
        txtUp.Size = New Size(58, 23)
        txtUp.TabIndex = 1
        txtUp.Text = "0"
        ' 
        ' fraBootSettings
        ' 
        fraBootSettings.Controls.Add(chkIndoors)
        fraBootSettings.Controls.Add(chkNoMapRespawn)
        fraBootSettings.Controls.Add(txtBootMap)
        fraBootSettings.Controls.Add(Label5)
        fraBootSettings.Controls.Add(txtBootY)
        fraBootSettings.Controls.Add(Label3)
        fraBootSettings.Controls.Add(txtBootX)
        fraBootSettings.Controls.Add(Label4)
        fraBootSettings.Location = New Point(7, 258)
        fraBootSettings.Margin = New Padding(4, 3, 4, 3)
        fraBootSettings.Name = "fraBootSettings"
        fraBootSettings.Padding = New Padding(4, 3, 4, 3)
        fraBootSettings.Size = New Size(271, 125)
        fraBootSettings.TabIndex = 13
        fraBootSettings.TabStop = False
        fraBootSettings.Text = "Respawn Settings"
        ' 
        ' chkIndoors
        ' 
        chkIndoors.AutoSize = True
        chkIndoors.Location = New Point(8, 100)
        chkIndoors.Margin = New Padding(4, 3, 4, 3)
        chkIndoors.Name = "chkIndoors"
        chkIndoors.Size = New Size(66, 19)
        chkIndoors.TabIndex = 42
        chkIndoors.Text = "Indoors"
        chkIndoors.UseVisualStyleBackColor = True
        ' 
        ' chkNoMapRespawn
        ' 
        chkNoMapRespawn.AutoSize = True
        chkNoMapRespawn.Location = New Point(138, 98)
        chkNoMapRespawn.Margin = New Padding(4, 3, 4, 3)
        chkNoMapRespawn.Name = "chkNoMapRespawn"
        chkNoMapRespawn.Size = New Size(119, 19)
        chkNoMapRespawn.TabIndex = 19
        chkNoMapRespawn.Text = "No Map Respawn"
        chkNoMapRespawn.UseVisualStyleBackColor = True
        ' 
        ' txtBootMap
        ' 
        txtBootMap.Location = New Point(205, 13)
        txtBootMap.Margin = New Padding(4, 3, 4, 3)
        txtBootMap.Name = "txtBootMap"
        txtBootMap.Size = New Size(58, 23)
        txtBootMap.TabIndex = 5
        txtBootMap.Text = "0"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(7, 18)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(34, 15)
        Label5.TabIndex = 4
        Label5.Text = "Map:"
        ' 
        ' txtBootY
        ' 
        txtBootY.Location = New Point(205, 73)
        txtBootY.Margin = New Padding(4, 3, 4, 3)
        txtBootY.Name = "txtBootY"
        txtBootY.Size = New Size(58, 23)
        txtBootY.TabIndex = 3
        txtBootY.Text = "0"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(7, 75)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(17, 15)
        Label3.TabIndex = 2
        Label3.Text = "Y:"
        ' 
        ' txtBootX
        ' 
        txtBootX.Location = New Point(205, 43)
        txtBootX.Margin = New Padding(4, 3, 4, 3)
        txtBootX.Name = "txtBootX"
        txtBootX.Size = New Size(58, 23)
        txtBootX.TabIndex = 1
        txtBootX.Text = "0"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(7, 43)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(17, 15)
        Label4.TabIndex = 0
        Label4.Text = "X:"
        ' 
        ' fraMaxSizes
        ' 
        fraMaxSizes.Controls.Add(txtMaxY)
        fraMaxSizes.Controls.Add(Label2)
        fraMaxSizes.Controls.Add(txtMaxX)
        fraMaxSizes.Controls.Add(Label7)
        fraMaxSizes.Location = New Point(285, 258)
        fraMaxSizes.Margin = New Padding(4, 3, 4, 3)
        fraMaxSizes.Name = "fraMaxSizes"
        fraMaxSizes.Padding = New Padding(4, 3, 4, 3)
        fraMaxSizes.Size = New Size(249, 90)
        fraMaxSizes.TabIndex = 12
        fraMaxSizes.TabStop = False
        fraMaxSizes.Text = "Size Settings"
        ' 
        ' txtMaxY
        ' 
        txtMaxY.Location = New Point(145, 48)
        txtMaxY.Margin = New Padding(4, 3, 4, 3)
        txtMaxY.Name = "txtMaxY"
        txtMaxY.Size = New Size(58, 23)
        txtMaxY.TabIndex = 3
        txtMaxY.Text = "0"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(7, 52)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(42, 15)
        Label2.TabIndex = 2
        Label2.Text = "Max Y:"
        ' 
        ' txtMaxX
        ' 
        txtMaxX.Location = New Point(145, 18)
        txtMaxX.Margin = New Padding(4, 3, 4, 3)
        txtMaxX.Name = "txtMaxX"
        txtMaxX.Size = New Size(58, 23)
        txtMaxX.TabIndex = 1
        txtMaxX.Text = "0"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(7, 22)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(42, 15)
        Label7.TabIndex = 0
        Label7.Text = "Max X:"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(btnPreview)
        GroupBox2.Controls.Add(lstMusic)
        GroupBox2.Location = New Point(285, 3)
        GroupBox2.Margin = New Padding(4, 3, 4, 3)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(4, 3, 4, 3)
        GroupBox2.Size = New Size(281, 249)
        GroupBox2.TabIndex = 11
        GroupBox2.TabStop = False
        GroupBox2.Text = "Music"
        ' 
        ' btnPreview
        ' 
        btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), Image)
        btnPreview.ImageAlign = ContentAlignment.MiddleLeft
        btnPreview.Location = New Point(57, 208)
        btnPreview.Margin = New Padding(4, 3, 4, 3)
        btnPreview.Name = "btnPreview"
        btnPreview.Size = New Size(162, 33)
        btnPreview.TabIndex = 4
        btnPreview.Text = "Preview Music"
        btnPreview.UseVisualStyleBackColor = True
        ' 
        ' lstMusic
        ' 
        lstMusic.FormattingEnabled = True
        lstMusic.ItemHeight = 15
        lstMusic.Location = New Point(7, 22)
        lstMusic.Margin = New Padding(4, 3, 4, 3)
        lstMusic.Name = "lstMusic"
        lstMusic.ScrollAlwaysVisible = True
        lstMusic.Size = New Size(242, 184)
        lstMusic.TabIndex = 3
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(62, 7)
        txtName.Margin = New Padding(4, 3, 4, 3)
        txtName.Name = "txtName"
        txtName.Size = New Size(215, 23)
        txtName.TabIndex = 10
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(7, 10)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(42, 15)
        Label6.TabIndex = 9
        Label6.Text = "Name:"
        ' 
        ' tpDirBlock
        ' 
        tpDirBlock.Controls.Add(Label12)
        tpDirBlock.Location = New Point(4, 24)
        tpDirBlock.Margin = New Padding(4, 3, 4, 3)
        tpDirBlock.Name = "tpDirBlock"
        tpDirBlock.Padding = New Padding(4, 3, 4, 3)
        tpDirBlock.Size = New Size(542, 601)
        tpDirBlock.TabIndex = 4
        tpDirBlock.Text = "Directional Block"
        tpDirBlock.UseVisualStyleBackColor = True
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(26, 27)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(265, 15)
        Label12.TabIndex = 0
        Label12.Text = "Just press the arrows to block that side of the tile."
        ' 
        ' tpEvents
        ' 
        tpEvents.Controls.Add(lblPasteMode)
        tpEvents.Controls.Add(lblCopyMode)
        tpEvents.Controls.Add(btnPasteEvent)
        tpEvents.Controls.Add(Label16)
        tpEvents.Controls.Add(btnCopyEvent)
        tpEvents.Controls.Add(Label15)
        tpEvents.Controls.Add(Label13)
        tpEvents.Location = New Point(4, 24)
        tpEvents.Margin = New Padding(4, 3, 4, 3)
        tpEvents.Name = "tpEvents"
        tpEvents.Padding = New Padding(4, 3, 4, 3)
        tpEvents.Size = New Size(542, 601)
        tpEvents.TabIndex = 5
        tpEvents.Text = "Events"
        tpEvents.UseVisualStyleBackColor = True
        ' 
        ' lblPasteMode
        ' 
        lblPasteMode.AutoSize = True
        lblPasteMode.Location = New Point(121, 197)
        lblPasteMode.Margin = New Padding(4, 0, 4, 0)
        lblPasteMode.Name = "lblPasteMode"
        lblPasteMode.Size = New Size(86, 15)
        lblPasteMode.TabIndex = 6
        lblPasteMode.Text = "PasteMode Off"
        ' 
        ' lblCopyMode
        ' 
        lblCopyMode.AutoSize = True
        lblCopyMode.Location = New Point(121, 129)
        lblCopyMode.Margin = New Padding(4, 0, 4, 0)
        lblCopyMode.Name = "lblCopyMode"
        lblCopyMode.Size = New Size(86, 15)
        lblCopyMode.TabIndex = 5
        lblCopyMode.Text = "CopyMode Off"
        ' 
        ' btnPasteEvent
        ' 
        btnPasteEvent.Location = New Point(27, 192)
        btnPasteEvent.Margin = New Padding(4, 3, 4, 3)
        btnPasteEvent.Name = "btnPasteEvent"
        btnPasteEvent.Size = New Size(88, 27)
        btnPasteEvent.TabIndex = 4
        btnPasteEvent.Text = "Paste Event"
        btnPasteEvent.UseVisualStyleBackColor = True
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(23, 172)
        Label16.Margin = New Padding(4, 0, 4, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(433, 15)
        Label16.TabIndex = 3
        Label16.Text = "To paste a copied Event, press the paste button, then click on the map to place it."
        ' 
        ' btnCopyEvent
        ' 
        btnCopyEvent.Location = New Point(27, 123)
        btnCopyEvent.Margin = New Padding(4, 3, 4, 3)
        btnCopyEvent.Name = "btnCopyEvent"
        btnCopyEvent.Size = New Size(88, 27)
        btnCopyEvent.TabIndex = 2
        btnCopyEvent.Text = "Copy Event"
        btnCopyEvent.UseVisualStyleBackColor = True
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(23, 100)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(339, 15)
        Label15.TabIndex = 1
        Label15.Text = "To copy a existing Event, press the copy button, then the event."
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(23, 24)
        Label13.Margin = New Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(265, 15)
        Label13.TabIndex = 0
        Label13.Text = "Click on the map where you want to add a event."
        ' 
        ' tpEffects
        ' 
        tpEffects.Controls.Add(GroupBox6)
        tpEffects.Controls.Add(GroupBox5)
        tpEffects.Controls.Add(GroupBox4)
        tpEffects.Controls.Add(GroupBox3)
        tpEffects.Controls.Add(GroupBox1)
        tpEffects.Location = New Point(4, 24)
        tpEffects.Margin = New Padding(4, 3, 4, 3)
        tpEffects.Name = "tpEffects"
        tpEffects.Padding = New Padding(4, 3, 4, 3)
        tpEffects.Size = New Size(542, 601)
        tpEffects.TabIndex = 6
        tpEffects.Text = "Effects"
        tpEffects.UseVisualStyleBackColor = True
        ' 
        ' GroupBox6
        ' 
        GroupBox6.Controls.Add(scrlMapBrightness)
        GroupBox6.Location = New Point(13, 259)
        GroupBox6.Margin = New Padding(4, 3, 4, 3)
        GroupBox6.Name = "GroupBox6"
        GroupBox6.Padding = New Padding(4, 3, 4, 3)
        GroupBox6.Size = New Size(275, 45)
        GroupBox6.TabIndex = 22
        GroupBox6.TabStop = False
        GroupBox6.Text = "Brightness"
        ' 
        ' scrlMapBrightness
        ' 
        scrlMapBrightness.LargeChange = 1
        scrlMapBrightness.Location = New Point(4, 19)
        scrlMapBrightness.Maximum = 255
        scrlMapBrightness.Name = "scrlMapBrightness"
        scrlMapBrightness.Size = New Size(262, 17)
        scrlMapBrightness.TabIndex = 10
        ' 
        ' GroupBox5
        ' 
        GroupBox5.Controls.Add(cmbParallax)
        GroupBox5.Location = New Point(295, 192)
        GroupBox5.Margin = New Padding(4, 3, 4, 3)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Padding = New Padding(4, 3, 4, 3)
        GroupBox5.Size = New Size(275, 61)
        GroupBox5.TabIndex = 21
        GroupBox5.TabStop = False
        GroupBox5.Text = "Parallax"
        ' 
        ' cmbParallax
        ' 
        cmbParallax.FormattingEnabled = True
        cmbParallax.Location = New Point(9, 21)
        cmbParallax.Margin = New Padding(4, 3, 4, 3)
        cmbParallax.Name = "cmbParallax"
        cmbParallax.Size = New Size(232, 23)
        cmbParallax.TabIndex = 0
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(cmbPanorama)
        GroupBox4.Location = New Point(7, 192)
        GroupBox4.Margin = New Padding(4, 3, 4, 3)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Padding = New Padding(4, 3, 4, 3)
        GroupBox4.Size = New Size(281, 61)
        GroupBox4.TabIndex = 20
        GroupBox4.TabStop = False
        GroupBox4.Text = "Panorama"
        ' 
        ' cmbPanorama
        ' 
        cmbPanorama.FormattingEnabled = True
        cmbPanorama.Location = New Point(10, 23)
        cmbPanorama.Margin = New Padding(4, 3, 4, 3)
        cmbPanorama.Name = "cmbPanorama"
        cmbPanorama.Size = New Size(262, 23)
        cmbPanorama.TabIndex = 0
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(chkTint)
        GroupBox3.Controls.Add(lblMapAlpha)
        GroupBox3.Controls.Add(lblMapBlue)
        GroupBox3.Controls.Add(lblMapGreen)
        GroupBox3.Controls.Add(lblMapRed)
        GroupBox3.Controls.Add(scrlMapAlpha)
        GroupBox3.Controls.Add(scrlMapBlue)
        GroupBox3.Controls.Add(scrlMapGreen)
        GroupBox3.Controls.Add(scrlMapRed)
        GroupBox3.Location = New Point(295, 7)
        GroupBox3.Margin = New Padding(4, 3, 4, 3)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Padding = New Padding(4, 3, 4, 3)
        GroupBox3.Size = New Size(275, 178)
        GroupBox3.TabIndex = 19
        GroupBox3.TabStop = False
        GroupBox3.Text = "Tint"
        ' 
        ' chkTint
        ' 
        chkTint.AutoSize = True
        chkTint.Location = New Point(7, 22)
        chkTint.Margin = New Padding(4, 3, 4, 3)
        chkTint.Name = "chkTint"
        chkTint.Size = New Size(61, 19)
        chkTint.TabIndex = 18
        chkTint.Text = "Enable"
        chkTint.UseVisualStyleBackColor = True
        ' 
        ' lblMapAlpha
        ' 
        lblMapAlpha.AutoSize = True
        lblMapAlpha.Location = New Point(9, 111)
        lblMapAlpha.Margin = New Padding(4, 0, 4, 0)
        lblMapAlpha.Name = "lblMapAlpha"
        lblMapAlpha.Size = New Size(50, 15)
        lblMapAlpha.TabIndex = 17
        lblMapAlpha.Text = "Alpha: 0"
        ' 
        ' lblMapBlue
        ' 
        lblMapBlue.AutoSize = True
        lblMapBlue.Location = New Point(9, 89)
        lblMapBlue.Margin = New Padding(4, 0, 4, 0)
        lblMapBlue.Name = "lblMapBlue"
        lblMapBlue.Size = New Size(42, 15)
        lblMapBlue.TabIndex = 16
        lblMapBlue.Text = "Blue: 0"
        ' 
        ' lblMapGreen
        ' 
        lblMapGreen.AutoSize = True
        lblMapGreen.Location = New Point(9, 67)
        lblMapGreen.Margin = New Padding(4, 0, 4, 0)
        lblMapGreen.Name = "lblMapGreen"
        lblMapGreen.Size = New Size(50, 15)
        lblMapGreen.TabIndex = 15
        lblMapGreen.Text = "Green: 0"
        ' 
        ' lblMapRed
        ' 
        lblMapRed.AutoSize = True
        lblMapRed.Location = New Point(7, 45)
        lblMapRed.Margin = New Padding(4, 0, 4, 0)
        lblMapRed.Name = "lblMapRed"
        lblMapRed.Size = New Size(39, 15)
        lblMapRed.TabIndex = 14
        lblMapRed.Text = "Red: 0"
        ' 
        ' scrlMapAlpha
        ' 
        scrlMapAlpha.LargeChange = 1
        scrlMapAlpha.Location = New Point(74, 109)
        scrlMapAlpha.Maximum = 255
        scrlMapAlpha.Name = "scrlMapAlpha"
        scrlMapAlpha.Size = New Size(169, 17)
        scrlMapAlpha.TabIndex = 13
        ' 
        ' scrlMapBlue
        ' 
        scrlMapBlue.LargeChange = 1
        scrlMapBlue.Location = New Point(74, 88)
        scrlMapBlue.Maximum = 255
        scrlMapBlue.Name = "scrlMapBlue"
        scrlMapBlue.Size = New Size(169, 17)
        scrlMapBlue.TabIndex = 12
        ' 
        ' scrlMapGreen
        ' 
        scrlMapGreen.LargeChange = 1
        scrlMapGreen.Location = New Point(74, 64)
        scrlMapGreen.Maximum = 255
        scrlMapGreen.Name = "scrlMapGreen"
        scrlMapGreen.Size = New Size(169, 17)
        scrlMapGreen.TabIndex = 11
        ' 
        ' scrlMapRed
        ' 
        scrlMapRed.LargeChange = 1
        scrlMapRed.Location = New Point(74, 46)
        scrlMapRed.Maximum = 255
        scrlMapRed.Name = "scrlMapRed"
        scrlMapRed.Size = New Size(169, 17)
        scrlMapRed.TabIndex = 10
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(scrlFogOpacity)
        GroupBox1.Controls.Add(lblFogOpacity)
        GroupBox1.Controls.Add(scrlFogSpeed)
        GroupBox1.Controls.Add(lblFogSpeed)
        GroupBox1.Controls.Add(scrlIntensity)
        GroupBox1.Controls.Add(lblIntensity)
        GroupBox1.Controls.Add(scrlFog)
        GroupBox1.Controls.Add(lblFogIndex)
        GroupBox1.Controls.Add(Label14)
        GroupBox1.Controls.Add(cmbWeather)
        GroupBox1.Location = New Point(7, 7)
        GroupBox1.Margin = New Padding(4, 3, 4, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(4, 3, 4, 3)
        GroupBox1.Size = New Size(281, 178)
        GroupBox1.TabIndex = 18
        GroupBox1.TabStop = False
        GroupBox1.Text = "Weather"
        ' 
        ' scrlFogOpacity
        ' 
        scrlFogOpacity.LargeChange = 1
        scrlFogOpacity.Location = New Point(105, 143)
        scrlFogOpacity.Maximum = 255
        scrlFogOpacity.Name = "scrlFogOpacity"
        scrlFogOpacity.Size = New Size(169, 17)
        scrlFogOpacity.TabIndex = 9
        ' 
        ' lblFogOpacity
        ' 
        lblFogOpacity.AutoSize = True
        lblFogOpacity.Location = New Point(7, 145)
        lblFogOpacity.Margin = New Padding(4, 0, 4, 0)
        lblFogOpacity.Name = "lblFogOpacity"
        lblFogOpacity.Size = New Size(73, 15)
        lblFogOpacity.TabIndex = 8
        lblFogOpacity.Text = "Fog Alpha: 0"
        ' 
        ' scrlFogSpeed
        ' 
        scrlFogSpeed.LargeChange = 1
        scrlFogSpeed.Location = New Point(105, 117)
        scrlFogSpeed.Name = "scrlFogSpeed"
        scrlFogSpeed.Size = New Size(169, 17)
        scrlFogSpeed.TabIndex = 7
        ' 
        ' lblFogSpeed
        ' 
        lblFogSpeed.AutoSize = True
        lblFogSpeed.Location = New Point(7, 121)
        lblFogSpeed.Margin = New Padding(4, 0, 4, 0)
        lblFogSpeed.Name = "lblFogSpeed"
        lblFogSpeed.Size = New Size(83, 15)
        lblFogSpeed.TabIndex = 6
        lblFogSpeed.Text = "FogSpeed: 100"
        ' 
        ' scrlIntensity
        ' 
        scrlIntensity.LargeChange = 1
        scrlIntensity.Location = New Point(105, 59)
        scrlIntensity.Name = "scrlIntensity"
        scrlIntensity.Size = New Size(169, 17)
        scrlIntensity.TabIndex = 5
        ' 
        ' lblIntensity
        ' 
        lblIntensity.AutoSize = True
        lblIntensity.Location = New Point(7, 61)
        lblIntensity.Margin = New Padding(4, 0, 4, 0)
        lblIntensity.Name = "lblIntensity"
        lblIntensity.Size = New Size(76, 15)
        lblIntensity.TabIndex = 4
        lblIntensity.Text = "Intensity: 100"
        ' 
        ' scrlFog
        ' 
        scrlFog.LargeChange = 1
        scrlFog.Location = New Point(105, 93)
        scrlFog.Name = "scrlFog"
        scrlFog.Size = New Size(169, 17)
        scrlFog.TabIndex = 3
        ' 
        ' lblFogIndex
        ' 
        lblFogIndex.AutoSize = True
        lblFogIndex.Location = New Point(7, 95)
        lblFogIndex.Margin = New Padding(4, 0, 4, 0)
        lblFogIndex.Name = "lblFogIndex"
        lblFogIndex.Size = New Size(39, 15)
        lblFogIndex.TabIndex = 2
        lblFogIndex.Text = "Fog: 1"
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(7, 29)
        Label14.Margin = New Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(82, 15)
        Label14.TabIndex = 1
        Label14.Text = "Weather Type:"
        ' 
        ' cmbWeather
        ' 
        cmbWeather.FormattingEnabled = True
        cmbWeather.Items.AddRange(New Object() {"None", "Rain", "Snow", "Hail", "Sand Storm", "Storm", "Fog"})
        cmbWeather.Location = New Point(105, 25)
        cmbWeather.Margin = New Padding(4, 3, 4, 3)
        cmbWeather.Name = "cmbWeather"
        cmbWeather.Size = New Size(168, 23)
        cmbWeather.TabIndex = 0
        ' 
        ' frmEditor_Map
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        BackColor = SystemColors.Control
        ClientSize = New Size(552, 658)
        Controls.Add(tabpages)
        Controls.Add(ToolStrip)
        Controls.Add(pnlAttributes)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        Name = "frmEditor_Map"
        Text = "Map Editor"
        pnlBack.ResumeLayout(False)
        CType(picBackSelect, System.ComponentModel.ISupportInitialize).EndInit()
        pnlAttributes.ResumeLayout(False)
        fraMapLight.ResumeLayout(False)
        fraMapLight.PerformLayout()
        fraAnimation.ResumeLayout(False)
        fraMapWarp.ResumeLayout(False)
        fraMapWarp.PerformLayout()
        fraNpcSpawn.ResumeLayout(False)
        fraNpcSpawn.PerformLayout()
        fraHeal.ResumeLayout(False)
        fraHeal.PerformLayout()
        fraShop.ResumeLayout(False)
        fraResource.ResumeLayout(False)
        fraResource.PerformLayout()
        fraMapItem.ResumeLayout(False)
        fraMapItem.PerformLayout()
        CType(picMapItem, System.ComponentModel.ISupportInitialize).EndInit()
        fraTrap.ResumeLayout(False)
        fraTrap.PerformLayout()
        ToolStrip.ResumeLayout(False)
        ToolStrip.PerformLayout()
        tabpages.ResumeLayout(False)
        tpTiles.ResumeLayout(False)
        tpTiles.PerformLayout()
        tpAttributes.ResumeLayout(False)
        tpAttributes.PerformLayout()
        tpNpcs.ResumeLayout(False)
        fraNpcs.ResumeLayout(False)
        fraNpcs.PerformLayout()
        tpSettings.ResumeLayout(False)
        tpSettings.PerformLayout()
        fraMapSettings.ResumeLayout(False)
        fraMapSettings.PerformLayout()
        fraMapLinks.ResumeLayout(False)
        fraMapLinks.PerformLayout()
        fraBootSettings.ResumeLayout(False)
        fraBootSettings.PerformLayout()
        fraMaxSizes.ResumeLayout(False)
        fraMaxSizes.PerformLayout()
        GroupBox2.ResumeLayout(False)
        tpDirBlock.ResumeLayout(False)
        tpDirBlock.PerformLayout()
        tpEvents.ResumeLayout(False)
        tpEvents.PerformLayout()
        tpEffects.ResumeLayout(False)
        GroupBox6.ResumeLayout(False)
        GroupBox5.ResumeLayout(False)
        GroupBox4.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents pnlBack As System.Windows.Forms.Panel
    Friend WithEvents optTrap As System.Windows.Forms.RadioButton
    Friend WithEvents optHeal As System.Windows.Forms.RadioButton
    Friend WithEvents optBank As System.Windows.Forms.RadioButton
    Friend WithEvents optShop As System.Windows.Forms.RadioButton
    Friend WithEvents optNPCSpawn As System.Windows.Forms.RadioButton
    Friend WithEvents optResource As System.Windows.Forms.RadioButton
    Friend WithEvents optNPCAvoid As System.Windows.Forms.RadioButton
    Friend WithEvents optItem As System.Windows.Forms.RadioButton
    Friend WithEvents optWarp As System.Windows.Forms.RadioButton
    Friend WithEvents optBlocked As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClearAttribute As System.Windows.Forms.Button
    Friend WithEvents pnlAttributes As System.Windows.Forms.Panel
    Friend WithEvents fraMapWarp As System.Windows.Forms.GroupBox
    Friend WithEvents lblMapWarpY As System.Windows.Forms.Label
    Friend WithEvents lblMapWarpX As System.Windows.Forms.Label
    Friend WithEvents lblMapWarpMap As System.Windows.Forms.Label
    Friend WithEvents scrlMapWarpY As System.Windows.Forms.HScrollBar
    Friend WithEvents scrlMapWarpX As System.Windows.Forms.HScrollBar
    Friend WithEvents scrlMapWarpMap As System.Windows.Forms.HScrollBar
    Friend WithEvents btnMapWarp As System.Windows.Forms.Button
    Friend WithEvents fraMapItem As System.Windows.Forms.GroupBox
    Friend WithEvents btnMapItem As System.Windows.Forms.Button
    Friend WithEvents scrlMapItemValue As System.Windows.Forms.HScrollBar
    Friend WithEvents scrlMapItem As System.Windows.Forms.HScrollBar
    Friend WithEvents lblMapItem As System.Windows.Forms.Label
    Friend WithEvents picMapItem As System.Windows.Forms.PictureBox
    Friend WithEvents fraResource As System.Windows.Forms.GroupBox
    Friend WithEvents btnResourceOk As System.Windows.Forms.Button
    Friend WithEvents scrlResource As System.Windows.Forms.HScrollBar
    Friend WithEvents lblResource As System.Windows.Forms.Label
    Friend WithEvents fraNpcSpawn As System.Windows.Forms.GroupBox
    Friend WithEvents btnNpcSpawn As System.Windows.Forms.Button
    Friend WithEvents scrlNpcDir As System.Windows.Forms.HScrollBar
    Friend WithEvents lblNpcDir As System.Windows.Forms.Label
    Friend WithEvents lstNpc As System.Windows.Forms.ComboBox
    Friend WithEvents fraShop As System.Windows.Forms.GroupBox
    Friend WithEvents cmbShop As System.Windows.Forms.ComboBox
    Friend WithEvents btnShop As System.Windows.Forms.Button
    Friend WithEvents fraHeal As System.Windows.Forms.GroupBox
    Friend WithEvents lblHeal As System.Windows.Forms.Label
    Friend WithEvents cmbHeal As System.Windows.Forms.ComboBox
    Friend WithEvents btnHeal As System.Windows.Forms.Button
    Friend WithEvents scrlHeal As System.Windows.Forms.HScrollBar
    Friend WithEvents fraTrap As System.Windows.Forms.GroupBox
    Friend WithEvents btnTrap As System.Windows.Forms.Button
    Friend WithEvents scrlTrap As System.Windows.Forms.HScrollBar
    Friend WithEvents lblTrap As System.Windows.Forms.Label
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDiscard As System.Windows.Forms.ToolStripButton
    Friend WithEvents tabpages As System.Windows.Forms.TabControl
    Friend WithEvents tpTiles As System.Windows.Forms.TabPage
    Friend WithEvents tpNpcs As System.Windows.Forms.TabPage
    Friend WithEvents tpSettings As System.Windows.Forms.TabPage
    Friend WithEvents fraNpcs As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox23 As System.Windows.Forms.ComboBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents fraMapLinks As System.Windows.Forms.GroupBox
    Friend WithEvents txtDown As System.Windows.Forms.TextBox
    Friend WithEvents txtLeft As System.Windows.Forms.TextBox
    Friend WithEvents lblMap As System.Windows.Forms.Label
    Friend WithEvents txtRight As System.Windows.Forms.TextBox
    Friend WithEvents txtUp As System.Windows.Forms.TextBox
    Friend WithEvents fraBootSettings As System.Windows.Forms.GroupBox
    Friend WithEvents txtBootMap As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBootY As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBootX As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents fraMaxSizes As System.Windows.Forms.GroupBox
    Friend WithEvents txtMaxY As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMaxX As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstMusic As System.Windows.Forms.ListBox
    Friend WithEvents fraMapSettings As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lstMoral As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmbNpcList As System.Windows.Forms.ComboBox
    Friend WithEvents lstMapNpc As System.Windows.Forms.ListBox
    Friend WithEvents tpAttributes As System.Windows.Forms.TabPage
    Friend WithEvents cmbTileSets As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAutoTile As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbLayers As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tpDirBlock As System.Windows.Forms.TabPage
    Friend WithEvents tpEvents As System.Windows.Forms.TabPage
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tsbMapGrid As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents tsbFill As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbEyeDropper As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents optLight As System.Windows.Forms.RadioButton
    Friend WithEvents btnPasteEvent As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnCopyEvent As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblPasteMode As System.Windows.Forms.Label
    Friend WithEvents lblCopyMode As System.Windows.Forms.Label
    Friend WithEvents tpEffects As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkTint As System.Windows.Forms.CheckBox
    Friend WithEvents lblMapAlpha As System.Windows.Forms.Label
    Friend WithEvents lblMapBlue As System.Windows.Forms.Label
    Friend WithEvents lblMapGreen As System.Windows.Forms.Label
    Friend WithEvents lblMapRed As System.Windows.Forms.Label
    Friend WithEvents scrlMapAlpha As System.Windows.Forms.HScrollBar
    Friend WithEvents scrlMapBlue As System.Windows.Forms.HScrollBar
    Friend WithEvents scrlMapGreen As System.Windows.Forms.HScrollBar
    Friend WithEvents scrlMapRed As System.Windows.Forms.HScrollBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents scrlFogOpacity As System.Windows.Forms.HScrollBar
    Friend WithEvents lblFogOpacity As System.Windows.Forms.Label
    Friend WithEvents scrlFogSpeed As System.Windows.Forms.HScrollBar
    Friend WithEvents lblFogSpeed As System.Windows.Forms.Label
    Friend WithEvents scrlIntensity As System.Windows.Forms.HScrollBar
    Friend WithEvents lblIntensity As System.Windows.Forms.Label
    Friend WithEvents scrlFog As System.Windows.Forms.HScrollBar
    Friend WithEvents lblFogIndex As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbWeather As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbParallax As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbPanorama As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents lblMapBrightness As Label
    Friend WithEvents scrlMapBrightness As HScrollBar
    Friend WithEvents picBackSelect As PictureBox
    Friend WithEvents tsbClear As ToolStripButton
    Friend WithEvents tsbCopyMap As ToolStripButton
    Friend WithEvents tsbUndo As ToolStripButton
    Friend WithEvents tsbRedo As ToolStripButton
    Friend WithEvents tsbOpacity As ToolStripButton
    Friend WithEvents tsbLight As ToolStripButton
    Friend WithEvents tsbScreenshot As ToolStripButton
    Friend WithEvents optAnimation As RadioButton
    Friend WithEvents fraAnimation As GroupBox
    Friend WithEvents cmbAnimation As ComboBox
    Friend WithEvents brnAnimation As Button
    Friend WithEvents fraMapLight As GroupBox
    Friend WithEvents btnLight As Button
    Friend WithEvents scrlLight As HScrollBar
    Friend WithEvents chkShadow As CheckBox
    Friend WithEvents chkFlicker As CheckBox
    Friend WithEvents Label21 As Label
    Friend WithEvents lblRadius As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents lstShop As ComboBox
    Friend WithEvents chkNoMapRespawn As CheckBox
    Friend WithEvents chkIndoors As CheckBox
    Friend WithEvents Label23 As Label
    Friend WithEvents cmbAttribute As ComboBox
    Friend WithEvents tsbDeleteMap As ToolStripButton
    Friend WithEvents optInfo As RadioButton
    Friend WithEvents optNoXing As RadioButton
End Class
