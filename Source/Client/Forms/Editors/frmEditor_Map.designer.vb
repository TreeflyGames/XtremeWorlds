<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEditor_Map
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditor_Map))
        Me.btnClearAttribute = New System.Windows.Forms.Button()
        Me.optTrap = New System.Windows.Forms.RadioButton()
        Me.optHeal = New System.Windows.Forms.RadioButton()
        Me.optBank = New System.Windows.Forms.RadioButton()
        Me.optShop = New System.Windows.Forms.RadioButton()
        Me.optNPCSpawn = New System.Windows.Forms.RadioButton()
        Me.optResource = New System.Windows.Forms.RadioButton()
        Me.optNPCAvoid = New System.Windows.Forms.RadioButton()
        Me.optItem = New System.Windows.Forms.RadioButton()
        Me.optWarp = New System.Windows.Forms.RadioButton()
        Me.optBlocked = New System.Windows.Forms.RadioButton()
        Me.pnlBack = New System.Windows.Forms.Panel()
        Me.picBackSelect = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlAttributes = New System.Windows.Forms.Panel()
        Me.fraMapLight = New System.Windows.Forms.GroupBox()
        Me.lblRadius = New System.Windows.Forms.Label()
        Me.scrlLight = New System.Windows.Forms.HScrollBar()
        Me.chkShadow = New System.Windows.Forms.CheckBox()
        Me.chkFlicker = New System.Windows.Forms.CheckBox()
        Me.btnLight = New System.Windows.Forms.Button()
        Me.fraAnimation = New System.Windows.Forms.GroupBox()
        Me.cmbAnimation = New System.Windows.Forms.ComboBox()
        Me.brnAnimation = New System.Windows.Forms.Button()
        Me.fraMapWarp = New System.Windows.Forms.GroupBox()
        Me.btnMapWarp = New System.Windows.Forms.Button()
        Me.scrlMapWarpY = New System.Windows.Forms.HScrollBar()
        Me.scrlMapWarpX = New System.Windows.Forms.HScrollBar()
        Me.scrlMapWarpMap = New System.Windows.Forms.HScrollBar()
        Me.lblMapWarpY = New System.Windows.Forms.Label()
        Me.lblMapWarpX = New System.Windows.Forms.Label()
        Me.lblMapWarpMap = New System.Windows.Forms.Label()
        Me.fraNpcSpawn = New System.Windows.Forms.GroupBox()
        Me.lstNpc = New System.Windows.Forms.ComboBox()
        Me.btnNpcSpawn = New System.Windows.Forms.Button()
        Me.scrlNpcDir = New System.Windows.Forms.HScrollBar()
        Me.lblNpcDir = New System.Windows.Forms.Label()
        Me.fraHeal = New System.Windows.Forms.GroupBox()
        Me.scrlHeal = New System.Windows.Forms.HScrollBar()
        Me.lblHeal = New System.Windows.Forms.Label()
        Me.cmbHeal = New System.Windows.Forms.ComboBox()
        Me.btnHeal = New System.Windows.Forms.Button()
        Me.fraShop = New System.Windows.Forms.GroupBox()
        Me.cmbShop = New System.Windows.Forms.ComboBox()
        Me.btnShop = New System.Windows.Forms.Button()
        Me.fraResource = New System.Windows.Forms.GroupBox()
        Me.btnResourceOk = New System.Windows.Forms.Button()
        Me.scrlResource = New System.Windows.Forms.HScrollBar()
        Me.lblResource = New System.Windows.Forms.Label()
        Me.fraMapItem = New System.Windows.Forms.GroupBox()
        Me.picMapItem = New System.Windows.Forms.PictureBox()
        Me.btnMapItem = New System.Windows.Forms.Button()
        Me.scrlMapItemValue = New System.Windows.Forms.HScrollBar()
        Me.scrlMapItem = New System.Windows.Forms.HScrollBar()
        Me.lblMapItem = New System.Windows.Forms.Label()
        Me.fraTrap = New System.Windows.Forms.GroupBox()
        Me.btnTrap = New System.Windows.Forms.Button()
        Me.scrlTrap = New System.Windows.Forms.HScrollBar()
        Me.lblTrap = New System.Windows.Forms.Label()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbDiscard = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbMapGrid = New System.Windows.Forms.ToolStripButton()
        Me.tsbOpacity = New System.Windows.Forms.ToolStripButton()
        Me.tsbLight = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbFill = New System.Windows.Forms.ToolStripButton()
        Me.tsbClear = New System.Windows.Forms.ToolStripButton()
        Me.tsbEyeDropper = New System.Windows.Forms.ToolStripButton()
        Me.tsbCopyMap = New System.Windows.Forms.ToolStripButton()
        Me.tsbUndo = New System.Windows.Forms.ToolStripButton()
        Me.tsbRedo = New System.Windows.Forms.ToolStripButton()
        Me.tsbScreenshot = New System.Windows.Forms.ToolStripButton()
        Me.tabpages = New System.Windows.Forms.TabControl()
        Me.tpTiles = New System.Windows.Forms.TabPage()
        Me.cmbAutoTile = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbLayers = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbTileSets = New System.Windows.Forms.ComboBox()
        Me.tpAttributes = New System.Windows.Forms.TabPage()
        Me.optAnimation = New System.Windows.Forms.RadioButton()
        Me.optLight = New System.Windows.Forms.RadioButton()
        Me.tpNpcs = New System.Windows.Forms.TabPage()
        Me.fraNpcs = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbNpcList = New System.Windows.Forms.ComboBox()
        Me.lstMapNpc = New System.Windows.Forms.ListBox()
        Me.ComboBox23 = New System.Windows.Forms.ComboBox()
        Me.tpSettings = New System.Windows.Forms.TabPage()
        Me.fraMapSettings = New System.Windows.Forms.GroupBox()
        Me.chkInstance = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbMoral = New System.Windows.Forms.ComboBox()
        Me.fraMapLinks = New System.Windows.Forms.GroupBox()
        Me.txtDown = New System.Windows.Forms.TextBox()
        Me.txtLeft = New System.Windows.Forms.TextBox()
        Me.lblMap = New System.Windows.Forms.Label()
        Me.txtRight = New System.Windows.Forms.TextBox()
        Me.txtUp = New System.Windows.Forms.TextBox()
        Me.fraBootSettings = New System.Windows.Forms.GroupBox()
        Me.txtBootMap = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBootY = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBootX = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.fraMaxSizes = New System.Windows.Forms.GroupBox()
        Me.txtMaxY = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMaxX = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.lstMusic = New System.Windows.Forms.ListBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tpDirBlock = New System.Windows.Forms.TabPage()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tpEvents = New System.Windows.Forms.TabPage()
        Me.lblPasteMode = New System.Windows.Forms.Label()
        Me.lblCopyMode = New System.Windows.Forms.Label()
        Me.btnPasteEvent = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnCopyEvent = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.lblMapBrightness = New System.Windows.Forms.Label()
        Me.scrlMapBrightness = New System.Windows.Forms.HScrollBar()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbParallax = New System.Windows.Forms.ComboBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbPanorama = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkUseTint = New System.Windows.Forms.CheckBox()
        Me.lblMapAlpha = New System.Windows.Forms.Label()
        Me.lblMapBlue = New System.Windows.Forms.Label()
        Me.lblMapGreen = New System.Windows.Forms.Label()
        Me.lblMapRed = New System.Windows.Forms.Label()
        Me.scrlMapAlpha = New System.Windows.Forms.HScrollBar()
        Me.scrlMapBlue = New System.Windows.Forms.HScrollBar()
        Me.scrlMapGreen = New System.Windows.Forms.HScrollBar()
        Me.scrlMapRed = New System.Windows.Forms.HScrollBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.scrlFogAlpha = New System.Windows.Forms.HScrollBar()
        Me.lblFogAlpha = New System.Windows.Forms.Label()
        Me.scrlFogSpeed = New System.Windows.Forms.HScrollBar()
        Me.lblFogSpeed = New System.Windows.Forms.Label()
        Me.scrlIntensity = New System.Windows.Forms.HScrollBar()
        Me.lblIntensity = New System.Windows.Forms.Label()
        Me.scrlFog = New System.Windows.Forms.HScrollBar()
        Me.lblFogIndex = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbWeather = New System.Windows.Forms.ComboBox()
        Me.pnlBack.SuspendLayout()
        CType(Me.picBackSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAttributes.SuspendLayout()
        Me.fraMapLight.SuspendLayout()
        Me.fraAnimation.SuspendLayout()
        Me.fraMapWarp.SuspendLayout()
        Me.fraNpcSpawn.SuspendLayout()
        Me.fraHeal.SuspendLayout()
        Me.fraShop.SuspendLayout()
        Me.fraResource.SuspendLayout()
        Me.fraMapItem.SuspendLayout()
        CType(Me.picMapItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTrap.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.tabpages.SuspendLayout()
        Me.tpTiles.SuspendLayout()
        Me.tpAttributes.SuspendLayout()
        Me.tpNpcs.SuspendLayout()
        Me.fraNpcs.SuspendLayout()
        Me.tpSettings.SuspendLayout()
        Me.fraMapSettings.SuspendLayout()
        Me.fraMapLinks.SuspendLayout()
        Me.fraBootSettings.SuspendLayout()
        Me.fraMaxSizes.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.tpDirBlock.SuspendLayout()
        Me.tpEvents.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClearAttribute
        '
        Me.btnClearAttribute.Location = New System.Drawing.Point(342, 553)
        Me.btnClearAttribute.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnClearAttribute.Name = "btnClearAttribute"
        Me.btnClearAttribute.Size = New System.Drawing.Size(192, 29)
        Me.btnClearAttribute.TabIndex = 14
        Me.btnClearAttribute.Text = "Clear All Attributes"
        Me.btnClearAttribute.UseVisualStyleBackColor = True
        '
        'optTrap
        '
        Me.optTrap.AutoSize = True
        Me.optTrap.Location = New System.Drawing.Point(373, 58)
        Me.optTrap.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optTrap.Name = "optTrap"
        Me.optTrap.Size = New System.Drawing.Size(47, 19)
        Me.optTrap.TabIndex = 12
        Me.optTrap.Text = "Trap"
        Me.optTrap.UseVisualStyleBackColor = True
        '
        'optHeal
        '
        Me.optHeal.AutoSize = True
        Me.optHeal.Location = New System.Drawing.Point(276, 58)
        Me.optHeal.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optHeal.Name = "optHeal"
        Me.optHeal.Size = New System.Drawing.Size(49, 19)
        Me.optHeal.TabIndex = 11
        Me.optHeal.Text = "Heal"
        Me.optHeal.UseVisualStyleBackColor = True
        '
        'optBank
        '
        Me.optBank.AutoSize = True
        Me.optBank.Location = New System.Drawing.Point(118, 58)
        Me.optBank.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optBank.Name = "optBank"
        Me.optBank.Size = New System.Drawing.Size(51, 19)
        Me.optBank.TabIndex = 10
        Me.optBank.Text = "Bank"
        Me.optBank.UseVisualStyleBackColor = True
        '
        'optShop
        '
        Me.optShop.AutoSize = True
        Me.optShop.Location = New System.Drawing.Point(477, 16)
        Me.optShop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optShop.Name = "optShop"
        Me.optShop.Size = New System.Drawing.Size(52, 19)
        Me.optShop.TabIndex = 9
        Me.optShop.Text = "Shop"
        Me.optShop.UseVisualStyleBackColor = True
        '
        'optNPCSpawn
        '
        Me.optNPCSpawn.AutoSize = True
        Me.optNPCSpawn.Location = New System.Drawing.Point(373, 16)
        Me.optNPCSpawn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optNPCSpawn.Name = "optNPCSpawn"
        Me.optNPCSpawn.Size = New System.Drawing.Size(87, 19)
        Me.optNPCSpawn.TabIndex = 8
        Me.optNPCSpawn.Text = "NPC Spawn"
        Me.optNPCSpawn.UseVisualStyleBackColor = True
        '
        'optResource
        '
        Me.optResource.AutoSize = True
        Me.optResource.Location = New System.Drawing.Point(12, 58)
        Me.optResource.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optResource.Name = "optResource"
        Me.optResource.Size = New System.Drawing.Size(73, 19)
        Me.optResource.TabIndex = 6
        Me.optResource.Text = "Resource"
        Me.optResource.UseVisualStyleBackColor = True
        '
        'optNPCAvoid
        '
        Me.optNPCAvoid.AutoSize = True
        Me.optNPCAvoid.Location = New System.Drawing.Point(276, 16)
        Me.optNPCAvoid.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optNPCAvoid.Name = "optNPCAvoid"
        Me.optNPCAvoid.Size = New System.Drawing.Size(83, 19)
        Me.optNPCAvoid.TabIndex = 3
        Me.optNPCAvoid.Text = "NPC Avoid"
        Me.optNPCAvoid.UseVisualStyleBackColor = True
        '
        'optItem
        '
        Me.optItem.AutoSize = True
        Me.optItem.Location = New System.Drawing.Point(202, 16)
        Me.optItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optItem.Name = "optItem"
        Me.optItem.Size = New System.Drawing.Size(49, 19)
        Me.optItem.TabIndex = 2
        Me.optItem.Text = "Item"
        Me.optItem.UseVisualStyleBackColor = True
        '
        'optWarp
        '
        Me.optWarp.AutoSize = True
        Me.optWarp.Location = New System.Drawing.Point(118, 16)
        Me.optWarp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optWarp.Name = "optWarp"
        Me.optWarp.Size = New System.Drawing.Size(53, 19)
        Me.optWarp.TabIndex = 1
        Me.optWarp.Text = "Warp"
        Me.optWarp.UseVisualStyleBackColor = True
        '
        'optBlocked
        '
        Me.optBlocked.AutoSize = True
        Me.optBlocked.Checked = True
        Me.optBlocked.Location = New System.Drawing.Point(12, 16)
        Me.optBlocked.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optBlocked.Name = "optBlocked"
        Me.optBlocked.Size = New System.Drawing.Size(67, 19)
        Me.optBlocked.TabIndex = 0
        Me.optBlocked.TabStop = True
        Me.optBlocked.Text = "Blocked"
        Me.optBlocked.UseVisualStyleBackColor = True
        '
        'pnlBack
        '
        Me.pnlBack.Controls.Add(Me.picBackSelect)
        Me.pnlBack.Location = New System.Drawing.Point(7, 9)
        Me.pnlBack.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlBack.Name = "pnlBack"
        Me.pnlBack.Size = New System.Drawing.Size(526, 532)
        Me.pnlBack.TabIndex = 9
        '
        'picBackSelect
        '
        Me.picBackSelect.BackColor = System.Drawing.Color.Black
        Me.picBackSelect.Location = New System.Drawing.Point(11, 3)
        Me.picBackSelect.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picBackSelect.Name = "picBackSelect"
        Me.picBackSelect.Size = New System.Drawing.Size(512, 512)
        Me.picBackSelect.TabIndex = 22
        Me.picBackSelect.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 544)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 15)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Drag Mouse to Select Multiple Tiles"
        '
        'pnlAttributes
        '
        Me.pnlAttributes.Controls.Add(Me.fraMapLight)
        Me.pnlAttributes.Controls.Add(Me.fraAnimation)
        Me.pnlAttributes.Controls.Add(Me.fraMapWarp)
        Me.pnlAttributes.Controls.Add(Me.fraNpcSpawn)
        Me.pnlAttributes.Controls.Add(Me.fraHeal)
        Me.pnlAttributes.Controls.Add(Me.fraShop)
        Me.pnlAttributes.Controls.Add(Me.fraResource)
        Me.pnlAttributes.Controls.Add(Me.fraMapItem)
        Me.pnlAttributes.Controls.Add(Me.fraTrap)
        Me.pnlAttributes.Location = New System.Drawing.Point(555, 56)
        Me.pnlAttributes.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlAttributes.Name = "pnlAttributes"
        Me.pnlAttributes.Size = New System.Drawing.Size(586, 567)
        Me.pnlAttributes.TabIndex = 12
        Me.pnlAttributes.Visible = False
        '
        'fraMapLight
        '
        Me.fraMapLight.Controls.Add(Me.lblRadius)
        Me.fraMapLight.Controls.Add(Me.scrlLight)
        Me.fraMapLight.Controls.Add(Me.chkShadow)
        Me.fraMapLight.Controls.Add(Me.chkFlicker)
        Me.fraMapLight.Controls.Add(Me.btnLight)
        Me.fraMapLight.Location = New System.Drawing.Point(394, 147)
        Me.fraMapLight.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapLight.Name = "fraMapLight"
        Me.fraMapLight.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapLight.Size = New System.Drawing.Size(172, 138)
        Me.fraMapLight.TabIndex = 19
        Me.fraMapLight.TabStop = False
        Me.fraMapLight.Text = "Light"
        Me.fraMapLight.Visible = False
        '
        'lblRadius
        '
        Me.lblRadius.AutoSize = True
        Me.lblRadius.Location = New System.Drawing.Point(8, 16)
        Me.lblRadius.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRadius.Name = "lblRadius"
        Me.lblRadius.Size = New System.Drawing.Size(54, 15)
        Me.lblRadius.TabIndex = 41
        Me.lblRadius.Text = "Radius: 0"
        '
        'scrlLight
        '
        Me.scrlLight.Location = New System.Drawing.Point(7, 31)
        Me.scrlLight.Name = "scrlLight"
        Me.scrlLight.Size = New System.Drawing.Size(165, 17)
        Me.scrlLight.TabIndex = 40
        '
        'chkShadow
        '
        Me.chkShadow.AutoSize = True
        Me.chkShadow.Location = New System.Drawing.Point(7, 73)
        Me.chkShadow.Name = "chkShadow"
        Me.chkShadow.Size = New System.Drawing.Size(73, 19)
        Me.chkShadow.TabIndex = 39
        Me.chkShadow.Text = "Shadows"
        Me.chkShadow.UseVisualStyleBackColor = True
        '
        'chkFlicker
        '
        Me.chkFlicker.AutoSize = True
        Me.chkFlicker.Location = New System.Drawing.Point(7, 51)
        Me.chkFlicker.Name = "chkFlicker"
        Me.chkFlicker.Size = New System.Drawing.Size(60, 19)
        Me.chkFlicker.TabIndex = 38
        Me.chkFlicker.Text = "Flicker"
        Me.chkFlicker.UseVisualStyleBackColor = True
        '
        'btnLight
        '
        Me.btnLight.Location = New System.Drawing.Point(34, 98)
        Me.btnLight.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnLight.Name = "btnLight"
        Me.btnLight.Size = New System.Drawing.Size(105, 32)
        Me.btnLight.TabIndex = 6
        Me.btnLight.Text = "Accept"
        Me.btnLight.UseVisualStyleBackColor = True
        '
        'fraAnimation
        '
        Me.fraAnimation.Controls.Add(Me.cmbAnimation)
        Me.fraAnimation.Controls.Add(Me.brnAnimation)
        Me.fraAnimation.Location = New System.Drawing.Point(214, 291)
        Me.fraAnimation.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraAnimation.Name = "fraAnimation"
        Me.fraAnimation.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraAnimation.Size = New System.Drawing.Size(203, 130)
        Me.fraAnimation.TabIndex = 17
        Me.fraAnimation.TabStop = False
        Me.fraAnimation.Text = "Animation"
        Me.fraAnimation.Visible = False
        '
        'cmbAnimation
        '
        Me.cmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnimation.FormattingEnabled = True
        Me.cmbAnimation.Items.AddRange(New Object() {"Heal HP", "Heal MP"})
        Me.cmbAnimation.Location = New System.Drawing.Point(7, 22)
        Me.cmbAnimation.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbAnimation.Name = "cmbAnimation"
        Me.cmbAnimation.Size = New System.Drawing.Size(180, 23)
        Me.cmbAnimation.TabIndex = 37
        '
        'brnAnimation
        '
        Me.brnAnimation.Location = New System.Drawing.Point(43, 88)
        Me.brnAnimation.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.brnAnimation.Name = "brnAnimation"
        Me.brnAnimation.Size = New System.Drawing.Size(105, 32)
        Me.brnAnimation.TabIndex = 6
        Me.brnAnimation.Text = "Accept"
        Me.brnAnimation.UseVisualStyleBackColor = True
        '
        'fraMapWarp
        '
        Me.fraMapWarp.Controls.Add(Me.btnMapWarp)
        Me.fraMapWarp.Controls.Add(Me.scrlMapWarpY)
        Me.fraMapWarp.Controls.Add(Me.scrlMapWarpX)
        Me.fraMapWarp.Controls.Add(Me.scrlMapWarpMap)
        Me.fraMapWarp.Controls.Add(Me.lblMapWarpY)
        Me.fraMapWarp.Controls.Add(Me.lblMapWarpX)
        Me.fraMapWarp.Controls.Add(Me.lblMapWarpMap)
        Me.fraMapWarp.Location = New System.Drawing.Point(10, 427)
        Me.fraMapWarp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapWarp.Name = "fraMapWarp"
        Me.fraMapWarp.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapWarp.Size = New System.Drawing.Size(294, 137)
        Me.fraMapWarp.TabIndex = 0
        Me.fraMapWarp.TabStop = False
        Me.fraMapWarp.Text = "Map Warp"
        '
        'btnMapWarp
        '
        Me.btnMapWarp.Location = New System.Drawing.Point(93, 102)
        Me.btnMapWarp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMapWarp.Name = "btnMapWarp"
        Me.btnMapWarp.Size = New System.Drawing.Size(105, 32)
        Me.btnMapWarp.TabIndex = 6
        Me.btnMapWarp.Text = "Accept"
        Me.btnMapWarp.UseVisualStyleBackColor = True
        '
        'scrlMapWarpY
        '
        Me.scrlMapWarpY.Location = New System.Drawing.Point(72, 73)
        Me.scrlMapWarpY.Name = "scrlMapWarpY"
        Me.scrlMapWarpY.Size = New System.Drawing.Size(188, 18)
        Me.scrlMapWarpY.TabIndex = 5
        '
        'scrlMapWarpX
        '
        Me.scrlMapWarpX.Location = New System.Drawing.Point(72, 47)
        Me.scrlMapWarpX.Name = "scrlMapWarpX"
        Me.scrlMapWarpX.Size = New System.Drawing.Size(188, 18)
        Me.scrlMapWarpX.TabIndex = 4
        '
        'scrlMapWarpMap
        '
        Me.scrlMapWarpMap.Location = New System.Drawing.Point(72, 23)
        Me.scrlMapWarpMap.Name = "scrlMapWarpMap"
        Me.scrlMapWarpMap.Size = New System.Drawing.Size(188, 18)
        Me.scrlMapWarpMap.TabIndex = 3
        '
        'lblMapWarpY
        '
        Me.lblMapWarpY.AutoSize = True
        Me.lblMapWarpY.Location = New System.Drawing.Point(8, 77)
        Me.lblMapWarpY.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMapWarpY.Name = "lblMapWarpY"
        Me.lblMapWarpY.Size = New System.Drawing.Size(26, 15)
        Me.lblMapWarpY.TabIndex = 2
        Me.lblMapWarpY.Text = "Y: 1"
        '
        'lblMapWarpX
        '
        Me.lblMapWarpX.AutoSize = True
        Me.lblMapWarpX.Location = New System.Drawing.Point(8, 53)
        Me.lblMapWarpX.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMapWarpX.Name = "lblMapWarpX"
        Me.lblMapWarpX.Size = New System.Drawing.Size(26, 15)
        Me.lblMapWarpX.TabIndex = 1
        Me.lblMapWarpX.Text = "X: 1"
        '
        'lblMapWarpMap
        '
        Me.lblMapWarpMap.AutoSize = True
        Me.lblMapWarpMap.Location = New System.Drawing.Point(7, 29)
        Me.lblMapWarpMap.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMapWarpMap.Name = "lblMapWarpMap"
        Me.lblMapWarpMap.Size = New System.Drawing.Size(43, 15)
        Me.lblMapWarpMap.TabIndex = 0
        Me.lblMapWarpMap.Text = "Map: 1"
        '
        'fraNpcSpawn
        '
        Me.fraNpcSpawn.Controls.Add(Me.lstNpc)
        Me.fraNpcSpawn.Controls.Add(Me.btnNpcSpawn)
        Me.fraNpcSpawn.Controls.Add(Me.scrlNpcDir)
        Me.fraNpcSpawn.Controls.Add(Me.lblNpcDir)
        Me.fraNpcSpawn.Location = New System.Drawing.Point(4, 7)
        Me.fraNpcSpawn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraNpcSpawn.Name = "fraNpcSpawn"
        Me.fraNpcSpawn.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraNpcSpawn.Size = New System.Drawing.Size(203, 130)
        Me.fraNpcSpawn.TabIndex = 11
        Me.fraNpcSpawn.TabStop = False
        Me.fraNpcSpawn.Text = "Npc Spawn"
        '
        'lstNpc
        '
        Me.lstNpc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstNpc.FormattingEnabled = True
        Me.lstNpc.Location = New System.Drawing.Point(7, 18)
        Me.lstNpc.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstNpc.Name = "lstNpc"
        Me.lstNpc.Size = New System.Drawing.Size(180, 23)
        Me.lstNpc.TabIndex = 37
        '
        'btnNpcSpawn
        '
        Me.btnNpcSpawn.Location = New System.Drawing.Point(46, 88)
        Me.btnNpcSpawn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnNpcSpawn.Name = "btnNpcSpawn"
        Me.btnNpcSpawn.Size = New System.Drawing.Size(105, 32)
        Me.btnNpcSpawn.TabIndex = 6
        Me.btnNpcSpawn.Text = "Accept"
        Me.btnNpcSpawn.UseVisualStyleBackColor = True
        '
        'scrlNpcDir
        '
        Me.scrlNpcDir.LargeChange = 1
        Me.scrlNpcDir.Location = New System.Drawing.Point(9, 63)
        Me.scrlNpcDir.Maximum = 3
        Me.scrlNpcDir.Name = "scrlNpcDir"
        Me.scrlNpcDir.Size = New System.Drawing.Size(178, 18)
        Me.scrlNpcDir.TabIndex = 3
        '
        'lblNpcDir
        '
        Me.lblNpcDir.AutoSize = True
        Me.lblNpcDir.Location = New System.Drawing.Point(6, 46)
        Me.lblNpcDir.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNpcDir.Name = "lblNpcDir"
        Me.lblNpcDir.Size = New System.Drawing.Size(76, 15)
        Me.lblNpcDir.TabIndex = 0
        Me.lblNpcDir.Text = "Direction: Up"
        '
        'fraHeal
        '
        Me.fraHeal.Controls.Add(Me.scrlHeal)
        Me.fraHeal.Controls.Add(Me.lblHeal)
        Me.fraHeal.Controls.Add(Me.cmbHeal)
        Me.fraHeal.Controls.Add(Me.btnHeal)
        Me.fraHeal.Location = New System.Drawing.Point(4, 290)
        Me.fraHeal.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraHeal.Name = "fraHeal"
        Me.fraHeal.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraHeal.Size = New System.Drawing.Size(203, 130)
        Me.fraHeal.TabIndex = 15
        Me.fraHeal.TabStop = False
        Me.fraHeal.Text = "Heal"
        '
        'scrlHeal
        '
        Me.scrlHeal.Location = New System.Drawing.Point(5, 65)
        Me.scrlHeal.Name = "scrlHeal"
        Me.scrlHeal.Size = New System.Drawing.Size(181, 17)
        Me.scrlHeal.TabIndex = 39
        '
        'lblHeal
        '
        Me.lblHeal.AutoSize = True
        Me.lblHeal.Location = New System.Drawing.Point(4, 50)
        Me.lblHeal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeal.Name = "lblHeal"
        Me.lblHeal.Size = New System.Drawing.Size(63, 15)
        Me.lblHeal.TabIndex = 38
        Me.lblHeal.Text = "Amount: 0"
        '
        'cmbHeal
        '
        Me.cmbHeal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHeal.FormattingEnabled = True
        Me.cmbHeal.Items.AddRange(New Object() {"Heal HP", "Heal MP"})
        Me.cmbHeal.Location = New System.Drawing.Point(7, 22)
        Me.cmbHeal.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbHeal.Name = "cmbHeal"
        Me.cmbHeal.Size = New System.Drawing.Size(180, 23)
        Me.cmbHeal.TabIndex = 37
        '
        'btnHeal
        '
        Me.btnHeal.Location = New System.Drawing.Point(43, 88)
        Me.btnHeal.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnHeal.Name = "btnHeal"
        Me.btnHeal.Size = New System.Drawing.Size(105, 32)
        Me.btnHeal.TabIndex = 6
        Me.btnHeal.Text = "Accept"
        Me.btnHeal.UseVisualStyleBackColor = True
        '
        'fraShop
        '
        Me.fraShop.Controls.Add(Me.cmbShop)
        Me.fraShop.Controls.Add(Me.btnShop)
        Me.fraShop.Location = New System.Drawing.Point(394, 9)
        Me.fraShop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShop.Name = "fraShop"
        Me.fraShop.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraShop.Size = New System.Drawing.Size(172, 138)
        Me.fraShop.TabIndex = 12
        Me.fraShop.TabStop = False
        Me.fraShop.Text = "Shop"
        '
        'cmbShop
        '
        Me.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShop.FormattingEnabled = True
        Me.cmbShop.Location = New System.Drawing.Point(7, 22)
        Me.cmbShop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbShop.Name = "cmbShop"
        Me.cmbShop.Size = New System.Drawing.Size(154, 23)
        Me.cmbShop.TabIndex = 37
        '
        'btnShop
        '
        Me.btnShop.Location = New System.Drawing.Point(34, 98)
        Me.btnShop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShop.Name = "btnShop"
        Me.btnShop.Size = New System.Drawing.Size(105, 32)
        Me.btnShop.TabIndex = 6
        Me.btnShop.Text = "Accept"
        Me.btnShop.UseVisualStyleBackColor = True
        '
        'fraResource
        '
        Me.fraResource.Controls.Add(Me.btnResourceOk)
        Me.fraResource.Controls.Add(Me.scrlResource)
        Me.fraResource.Controls.Add(Me.lblResource)
        Me.fraResource.Location = New System.Drawing.Point(214, 7)
        Me.fraResource.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraResource.Name = "fraResource"
        Me.fraResource.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraResource.Size = New System.Drawing.Size(172, 130)
        Me.fraResource.TabIndex = 10
        Me.fraResource.TabStop = False
        Me.fraResource.Text = "Resource"
        '
        'btnResourceOk
        '
        Me.btnResourceOk.Location = New System.Drawing.Point(33, 88)
        Me.btnResourceOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnResourceOk.Name = "btnResourceOk"
        Me.btnResourceOk.Size = New System.Drawing.Size(105, 32)
        Me.btnResourceOk.TabIndex = 6
        Me.btnResourceOk.Text = "Accept"
        Me.btnResourceOk.UseVisualStyleBackColor = True
        '
        'scrlResource
        '
        Me.scrlResource.Location = New System.Drawing.Point(4, 42)
        Me.scrlResource.Name = "scrlResource"
        Me.scrlResource.Size = New System.Drawing.Size(159, 18)
        Me.scrlResource.TabIndex = 3
        '
        'lblResource
        '
        Me.lblResource.AutoSize = True
        Me.lblResource.Location = New System.Drawing.Point(0, 18)
        Me.lblResource.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblResource.Name = "lblResource"
        Me.lblResource.Size = New System.Drawing.Size(45, 15)
        Me.lblResource.TabIndex = 0
        Me.lblResource.Text = "Object:"
        '
        'fraMapItem
        '
        Me.fraMapItem.Controls.Add(Me.picMapItem)
        Me.fraMapItem.Controls.Add(Me.btnMapItem)
        Me.fraMapItem.Controls.Add(Me.scrlMapItemValue)
        Me.fraMapItem.Controls.Add(Me.scrlMapItem)
        Me.fraMapItem.Controls.Add(Me.lblMapItem)
        Me.fraMapItem.Location = New System.Drawing.Point(4, 137)
        Me.fraMapItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapItem.Name = "fraMapItem"
        Me.fraMapItem.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapItem.Size = New System.Drawing.Size(203, 137)
        Me.fraMapItem.TabIndex = 7
        Me.fraMapItem.TabStop = False
        Me.fraMapItem.Text = "Map Item"
        '
        'picMapItem
        '
        Me.picMapItem.BackColor = System.Drawing.Color.Black
        Me.picMapItem.Location = New System.Drawing.Point(155, 42)
        Me.picMapItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.picMapItem.Name = "picMapItem"
        Me.picMapItem.Size = New System.Drawing.Size(37, 37)
        Me.picMapItem.TabIndex = 7
        Me.picMapItem.TabStop = False
        '
        'btnMapItem
        '
        Me.btnMapItem.Location = New System.Drawing.Point(46, 97)
        Me.btnMapItem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMapItem.Name = "btnMapItem"
        Me.btnMapItem.Size = New System.Drawing.Size(105, 32)
        Me.btnMapItem.TabIndex = 6
        Me.btnMapItem.Text = "Accept"
        Me.btnMapItem.UseVisualStyleBackColor = True
        '
        'scrlMapItemValue
        '
        Me.scrlMapItemValue.Location = New System.Drawing.Point(10, 68)
        Me.scrlMapItemValue.Name = "scrlMapItemValue"
        Me.scrlMapItemValue.Size = New System.Drawing.Size(140, 18)
        Me.scrlMapItemValue.TabIndex = 4
        '
        'scrlMapItem
        '
        Me.scrlMapItem.Location = New System.Drawing.Point(10, 43)
        Me.scrlMapItem.Name = "scrlMapItem"
        Me.scrlMapItem.Size = New System.Drawing.Size(140, 18)
        Me.scrlMapItem.TabIndex = 3
        '
        'lblMapItem
        '
        Me.lblMapItem.AutoSize = True
        Me.lblMapItem.Location = New System.Drawing.Point(7, 25)
        Me.lblMapItem.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMapItem.Name = "lblMapItem"
        Me.lblMapItem.Size = New System.Drawing.Size(51, 15)
        Me.lblMapItem.TabIndex = 0
        Me.lblMapItem.Text = "None x0"
        '
        'fraTrap
        '
        Me.fraTrap.Controls.Add(Me.btnTrap)
        Me.fraTrap.Controls.Add(Me.scrlTrap)
        Me.fraTrap.Controls.Add(Me.lblTrap)
        Me.fraTrap.Location = New System.Drawing.Point(214, 144)
        Me.fraTrap.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraTrap.Name = "fraTrap"
        Me.fraTrap.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraTrap.Size = New System.Drawing.Size(172, 138)
        Me.fraTrap.TabIndex = 16
        Me.fraTrap.TabStop = False
        Me.fraTrap.Text = "Trap"
        '
        'btnTrap
        '
        Me.btnTrap.Location = New System.Drawing.Point(33, 98)
        Me.btnTrap.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnTrap.Name = "btnTrap"
        Me.btnTrap.Size = New System.Drawing.Size(105, 32)
        Me.btnTrap.TabIndex = 42
        Me.btnTrap.Text = "Accept"
        Me.btnTrap.UseVisualStyleBackColor = True
        '
        'scrlTrap
        '
        Me.scrlTrap.Location = New System.Drawing.Point(13, 38)
        Me.scrlTrap.Name = "scrlTrap"
        Me.scrlTrap.Size = New System.Drawing.Size(149, 17)
        Me.scrlTrap.TabIndex = 41
        '
        'lblTrap
        '
        Me.lblTrap.AutoSize = True
        Me.lblTrap.Location = New System.Drawing.Point(7, 18)
        Me.lblTrap.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTrap.Name = "lblTrap"
        Me.lblTrap.Size = New System.Drawing.Size(63, 15)
        Me.lblTrap.TabIndex = 40
        Me.lblTrap.Text = "Amount: 0"
        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.tsbDiscard, Me.ToolStripSeparator1, Me.tsbMapGrid, Me.tsbOpacity, Me.tsbLight, Me.ToolStripSeparator2, Me.tsbFill, Me.tsbClear, Me.tsbEyeDropper, Me.tsbCopyMap, Me.tsbUndo, Me.tsbRedo, Me.tsbScreenshot})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(552, 25)
        Me.ToolStrip.TabIndex = 13
        Me.ToolStrip.Text = "ToolStrip1"
        '
        'tsbSave
        '
        Me.tsbSave.Image = Global.Mirage.Basic.Client.My.Resources.Resources.Save
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(23, 22)
        Me.tsbSave.ToolTipText = "Save"
        '
        'tsbDiscard
        '
        Me.tsbDiscard.Image = Global.Mirage.Basic.Client.My.Resources.Resources._Exit
        Me.tsbDiscard.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDiscard.Name = "tsbDiscard"
        Me.tsbDiscard.Size = New System.Drawing.Size(23, 22)
        Me.tsbDiscard.ToolTipText = "Discard"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tsbMapGrid
        '
        Me.tsbMapGrid.Image = Global.Mirage.Basic.Client.My.Resources.Resources.Grid
        Me.tsbMapGrid.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbMapGrid.Name = "tsbMapGrid"
        Me.tsbMapGrid.Size = New System.Drawing.Size(23, 22)
        Me.tsbMapGrid.Tag = "Map Grid"
        '
        'tsbOpacity
        '
        Me.tsbOpacity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbOpacity.Image = Global.Mirage.Basic.Client.My.Resources.Resources.Opacity
        Me.tsbOpacity.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOpacity.Name = "tsbOpacity"
        Me.tsbOpacity.Size = New System.Drawing.Size(23, 22)
        Me.tsbOpacity.Text = "ToolStripButton1"
        Me.tsbOpacity.ToolTipText = "Opacity"
        '
        'tsbLight
        '
        Me.tsbLight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbLight.Image = CType(resources.GetObject("tsbLight.Image"), System.Drawing.Image)
        Me.tsbLight.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbLight.Name = "tsbLight"
        Me.tsbLight.Size = New System.Drawing.Size(23, 22)
        Me.tsbLight.ToolTipText = "Light"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsbFill
        '
        Me.tsbFill.Image = Global.Mirage.Basic.Client.My.Resources.Resources.Fill
        Me.tsbFill.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFill.Name = "tsbFill"
        Me.tsbFill.Size = New System.Drawing.Size(23, 22)
        Me.tsbFill.Tag = "Fill"
        Me.tsbFill.ToolTipText = "Fill Layer"
        '
        'tsbClear
        '
        Me.tsbClear.Image = Global.Mirage.Basic.Client.My.Resources.Resources.Clear
        Me.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClear.Name = "tsbClear"
        Me.tsbClear.Size = New System.Drawing.Size(23, 22)
        Me.tsbClear.ToolTipText = "Erase"
        '
        'tsbEyeDropper
        '
        Me.tsbEyeDropper.Image = Global.Mirage.Basic.Client.My.Resources.Resources.Wand
        Me.tsbEyeDropper.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbEyeDropper.Name = "tsbEyeDropper"
        Me.tsbEyeDropper.Size = New System.Drawing.Size(23, 22)
        Me.tsbEyeDropper.ToolTipText = "Eye Dropper"
        '
        'tsbCopyMap
        '
        Me.tsbCopyMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbCopyMap.Image = Global.Mirage.Basic.Client.My.Resources.Resources.Clipboard
        Me.tsbCopyMap.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCopyMap.Name = "tsbCopyMap"
        Me.tsbCopyMap.Size = New System.Drawing.Size(23, 22)
        Me.tsbCopyMap.ToolTipText = "Copy"
        '
        'tsbUndo
        '
        Me.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbUndo.Image = Global.Mirage.Basic.Client.My.Resources.Resources.Undo
        Me.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUndo.Name = "tsbUndo"
        Me.tsbUndo.Size = New System.Drawing.Size(23, 22)
        Me.tsbUndo.ToolTipText = "Undo"
        '
        'tsbRedo
        '
        Me.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbRedo.Image = Global.Mirage.Basic.Client.My.Resources.Resources.Redo
        Me.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRedo.Name = "tsbRedo"
        Me.tsbRedo.Size = New System.Drawing.Size(23, 22)
        Me.tsbRedo.ToolTipText = "Redo"
        '
        'tsbScreenshot
        '
        Me.tsbScreenshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbScreenshot.Image = Global.Mirage.Basic.Client.My.Resources.Resources.ScreenShot
        Me.tsbScreenshot.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbScreenshot.Name = "tsbScreenshot"
        Me.tsbScreenshot.Size = New System.Drawing.Size(23, 22)
        Me.tsbScreenshot.ToolTipText = "Screenshot"
        '
        'tabpages
        '
        Me.tabpages.Controls.Add(Me.tpTiles)
        Me.tabpages.Controls.Add(Me.tpAttributes)
        Me.tabpages.Controls.Add(Me.tpNpcs)
        Me.tabpages.Controls.Add(Me.tpSettings)
        Me.tabpages.Controls.Add(Me.tpDirBlock)
        Me.tabpages.Controls.Add(Me.tpEvents)
        Me.tabpages.Controls.Add(Me.TabPage1)
        Me.tabpages.Location = New System.Drawing.Point(5, 32)
        Me.tabpages.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabpages.Name = "tabpages"
        Me.tabpages.SelectedIndex = 0
        Me.tabpages.Size = New System.Drawing.Size(550, 629)
        Me.tabpages.TabIndex = 14
        '
        'tpTiles
        '
        Me.tpTiles.Controls.Add(Me.cmbAutoTile)
        Me.tpTiles.Controls.Add(Me.Label11)
        Me.tpTiles.Controls.Add(Me.Label10)
        Me.tpTiles.Controls.Add(Me.cmbLayers)
        Me.tpTiles.Controls.Add(Me.Label9)
        Me.tpTiles.Controls.Add(Me.cmbTileSets)
        Me.tpTiles.Controls.Add(Me.pnlBack)
        Me.tpTiles.Controls.Add(Me.Label1)
        Me.tpTiles.Location = New System.Drawing.Point(4, 24)
        Me.tpTiles.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpTiles.Name = "tpTiles"
        Me.tpTiles.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpTiles.Size = New System.Drawing.Size(542, 601)
        Me.tpTiles.TabIndex = 0
        Me.tpTiles.Text = "Tiles"
        Me.tpTiles.UseVisualStyleBackColor = True
        '
        'cmbAutoTile
        '
        Me.cmbAutoTile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAutoTile.FormattingEnabled = True
        Me.cmbAutoTile.Items.AddRange(New Object() {"Normal", "AutoTile (VX)", "Fake (VX)", "Animated (VX)", "Cliff (VX)", "Waterfall (VX)"})
        Me.cmbAutoTile.Location = New System.Drawing.Point(428, 566)
        Me.cmbAutoTile.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbAutoTile.Name = "cmbAutoTile"
        Me.cmbAutoTile.Size = New System.Drawing.Size(110, 23)
        Me.cmbAutoTile.TabIndex = 17
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(364, 570)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(54, 15)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "AutoTile:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(167, 570)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 15)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Layer:"
        '
        'cmbLayers
        '
        Me.cmbLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLayers.FormattingEnabled = True
        Me.cmbLayers.Items.AddRange(New Object() {"Ground", "Mask", "Cover", "Fringe", "Roof"})
        Me.cmbLayers.Location = New System.Drawing.Point(216, 566)
        Me.cmbLayers.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbLayers.Name = "cmbLayers"
        Me.cmbLayers.Size = New System.Drawing.Size(112, 23)
        Me.cmbLayers.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 570)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(43, 15)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Tileset:"
        '
        'cmbTileSets
        '
        Me.cmbTileSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTileSets.FormattingEnabled = True
        Me.cmbTileSets.Location = New System.Drawing.Point(63, 566)
        Me.cmbTileSets.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbTileSets.Name = "cmbTileSets"
        Me.cmbTileSets.Size = New System.Drawing.Size(68, 23)
        Me.cmbTileSets.TabIndex = 12
        '
        'tpAttributes
        '
        Me.tpAttributes.Controls.Add(Me.optAnimation)
        Me.tpAttributes.Controls.Add(Me.optLight)
        Me.tpAttributes.Controls.Add(Me.btnClearAttribute)
        Me.tpAttributes.Controls.Add(Me.optTrap)
        Me.tpAttributes.Controls.Add(Me.optBlocked)
        Me.tpAttributes.Controls.Add(Me.optHeal)
        Me.tpAttributes.Controls.Add(Me.optWarp)
        Me.tpAttributes.Controls.Add(Me.optBank)
        Me.tpAttributes.Controls.Add(Me.optItem)
        Me.tpAttributes.Controls.Add(Me.optShop)
        Me.tpAttributes.Controls.Add(Me.optNPCAvoid)
        Me.tpAttributes.Controls.Add(Me.optNPCSpawn)
        Me.tpAttributes.Controls.Add(Me.optResource)
        Me.tpAttributes.Location = New System.Drawing.Point(4, 24)
        Me.tpAttributes.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpAttributes.Name = "tpAttributes"
        Me.tpAttributes.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpAttributes.Size = New System.Drawing.Size(542, 601)
        Me.tpAttributes.TabIndex = 3
        Me.tpAttributes.Text = "Attributes"
        Me.tpAttributes.UseVisualStyleBackColor = True
        '
        'optAnimation
        '
        Me.optAnimation.AutoSize = True
        Me.optAnimation.Location = New System.Drawing.Point(448, 58)
        Me.optAnimation.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optAnimation.Name = "optAnimation"
        Me.optAnimation.Size = New System.Drawing.Size(81, 19)
        Me.optAnimation.TabIndex = 19
        Me.optAnimation.Text = "Animation"
        Me.optAnimation.UseVisualStyleBackColor = True
        '
        'optLight
        '
        Me.optLight.AutoSize = True
        Me.optLight.Location = New System.Drawing.Point(202, 58)
        Me.optLight.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optLight.Name = "optLight"
        Me.optLight.Size = New System.Drawing.Size(52, 19)
        Me.optLight.TabIndex = 18
        Me.optLight.Text = "Light"
        Me.optLight.UseVisualStyleBackColor = True
        '
        'tpNpcs
        '
        Me.tpNpcs.Controls.Add(Me.fraNpcs)
        Me.tpNpcs.Location = New System.Drawing.Point(4, 24)
        Me.tpNpcs.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpNpcs.Name = "tpNpcs"
        Me.tpNpcs.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpNpcs.Size = New System.Drawing.Size(542, 601)
        Me.tpNpcs.TabIndex = 1
        Me.tpNpcs.Text = "NPC's"
        Me.tpNpcs.UseVisualStyleBackColor = True
        '
        'fraNpcs
        '
        Me.fraNpcs.Controls.Add(Me.Label18)
        Me.fraNpcs.Controls.Add(Me.Label17)
        Me.fraNpcs.Controls.Add(Me.cmbNpcList)
        Me.fraNpcs.Controls.Add(Me.lstMapNpc)
        Me.fraNpcs.Controls.Add(Me.ComboBox23)
        Me.fraNpcs.Location = New System.Drawing.Point(7, 9)
        Me.fraNpcs.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraNpcs.Name = "fraNpcs"
        Me.fraNpcs.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraNpcs.Size = New System.Drawing.Size(559, 492)
        Me.fraNpcs.TabIndex = 11
        Me.fraNpcs.TabStop = False
        Me.fraNpcs.Text = "NPCs"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(228, 33)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(77, 15)
        Me.Label18.TabIndex = 72
        Me.Label18.Text = "2. Select NPC"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(7, 33)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 15)
        Me.Label17.TabIndex = 71
        Me.Label17.Text = "1. NPC LIst"
        '
        'cmbNpcList
        '
        Me.cmbNpcList.FormattingEnabled = True
        Me.cmbNpcList.Location = New System.Drawing.Point(228, 52)
        Me.cmbNpcList.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbNpcList.Name = "cmbNpcList"
        Me.cmbNpcList.Size = New System.Drawing.Size(299, 23)
        Me.cmbNpcList.TabIndex = 70
        '
        'lstMapNpc
        '
        Me.lstMapNpc.FormattingEnabled = True
        Me.lstMapNpc.ItemHeight = 15
        Me.lstMapNpc.Location = New System.Drawing.Point(10, 52)
        Me.lstMapNpc.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstMapNpc.Name = "lstMapNpc"
        Me.lstMapNpc.Size = New System.Drawing.Size(210, 424)
        Me.lstMapNpc.TabIndex = 69
        '
        'ComboBox23
        '
        Me.ComboBox23.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox23.FormattingEnabled = True
        Me.ComboBox23.Location = New System.Drawing.Point(398, 541)
        Me.ComboBox23.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ComboBox23.Name = "ComboBox23"
        Me.ComboBox23.Size = New System.Drawing.Size(154, 23)
        Me.ComboBox23.TabIndex = 68
        '
        'tpSettings
        '
        Me.tpSettings.Controls.Add(Me.fraMapSettings)
        Me.tpSettings.Controls.Add(Me.fraMapLinks)
        Me.tpSettings.Controls.Add(Me.fraBootSettings)
        Me.tpSettings.Controls.Add(Me.fraMaxSizes)
        Me.tpSettings.Controls.Add(Me.GroupBox2)
        Me.tpSettings.Controls.Add(Me.txtName)
        Me.tpSettings.Controls.Add(Me.Label6)
        Me.tpSettings.Location = New System.Drawing.Point(4, 24)
        Me.tpSettings.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpSettings.Name = "tpSettings"
        Me.tpSettings.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpSettings.Size = New System.Drawing.Size(542, 601)
        Me.tpSettings.TabIndex = 2
        Me.tpSettings.Text = "Settings"
        Me.tpSettings.UseVisualStyleBackColor = True
        '
        'fraMapSettings
        '
        Me.fraMapSettings.Controls.Add(Me.chkInstance)
        Me.fraMapSettings.Controls.Add(Me.Label8)
        Me.fraMapSettings.Controls.Add(Me.cmbMoral)
        Me.fraMapSettings.Location = New System.Drawing.Point(7, 37)
        Me.fraMapSettings.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapSettings.Name = "fraMapSettings"
        Me.fraMapSettings.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapSettings.Size = New System.Drawing.Size(271, 78)
        Me.fraMapSettings.TabIndex = 15
        Me.fraMapSettings.TabStop = False
        Me.fraMapSettings.Text = "Map Settings"
        '
        'chkInstance
        '
        Me.chkInstance.AutoSize = True
        Me.chkInstance.Location = New System.Drawing.Point(7, 52)
        Me.chkInstance.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkInstance.Name = "chkInstance"
        Me.chkInstance.Size = New System.Drawing.Size(82, 19)
        Me.chkInstance.TabIndex = 40
        Me.chkInstance.Text = "Instanced?"
        Me.chkInstance.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 17)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 15)
        Me.Label8.TabIndex = 38
        Me.Label8.Text = "Moral:"
        '
        'cmbMoral
        '
        Me.cmbMoral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMoral.FormattingEnabled = True
        Me.cmbMoral.Items.AddRange(New Object() {"None", "Safe Zone", "Indoors"})
        Me.cmbMoral.Location = New System.Drawing.Point(52, 14)
        Me.cmbMoral.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbMoral.Name = "cmbMoral"
        Me.cmbMoral.Size = New System.Drawing.Size(210, 23)
        Me.cmbMoral.TabIndex = 37
        '
        'fraMapLinks
        '
        Me.fraMapLinks.Controls.Add(Me.txtDown)
        Me.fraMapLinks.Controls.Add(Me.txtLeft)
        Me.fraMapLinks.Controls.Add(Me.lblMap)
        Me.fraMapLinks.Controls.Add(Me.txtRight)
        Me.fraMapLinks.Controls.Add(Me.txtUp)
        Me.fraMapLinks.Location = New System.Drawing.Point(7, 122)
        Me.fraMapLinks.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapLinks.Name = "fraMapLinks"
        Me.fraMapLinks.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMapLinks.Size = New System.Drawing.Size(271, 129)
        Me.fraMapLinks.TabIndex = 14
        Me.fraMapLinks.TabStop = False
        Me.fraMapLinks.Text = "Map Links"
        '
        'txtDown
        '
        Me.txtDown.Location = New System.Drawing.Point(105, 99)
        Me.txtDown.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtDown.Name = "txtDown"
        Me.txtDown.Size = New System.Drawing.Size(58, 23)
        Me.txtDown.TabIndex = 6
        Me.txtDown.Text = "0"
        '
        'txtLeft
        '
        Me.txtLeft.Location = New System.Drawing.Point(8, 54)
        Me.txtLeft.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtLeft.Name = "txtLeft"
        Me.txtLeft.Size = New System.Drawing.Size(50, 23)
        Me.txtLeft.TabIndex = 5
        Me.txtLeft.Text = "0"
        '
        'lblMap
        '
        Me.lblMap.AutoSize = True
        Me.lblMap.Location = New System.Drawing.Point(88, 58)
        Me.lblMap.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMap.Name = "lblMap"
        Me.lblMap.Size = New System.Drawing.Size(86, 15)
        Me.lblMap.TabIndex = 4
        Me.lblMap.Text = "Current Map: 0"
        '
        'txtRight
        '
        Me.txtRight.Location = New System.Drawing.Point(206, 54)
        Me.txtRight.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtRight.Name = "txtRight"
        Me.txtRight.Size = New System.Drawing.Size(58, 23)
        Me.txtRight.TabIndex = 3
        Me.txtRight.Text = "0"
        '
        'txtUp
        '
        Me.txtUp.Location = New System.Drawing.Point(104, 12)
        Me.txtUp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtUp.Name = "txtUp"
        Me.txtUp.Size = New System.Drawing.Size(58, 23)
        Me.txtUp.TabIndex = 1
        Me.txtUp.Text = "0"
        '
        'fraBootSettings
        '
        Me.fraBootSettings.Controls.Add(Me.txtBootMap)
        Me.fraBootSettings.Controls.Add(Me.Label5)
        Me.fraBootSettings.Controls.Add(Me.txtBootY)
        Me.fraBootSettings.Controls.Add(Me.Label3)
        Me.fraBootSettings.Controls.Add(Me.txtBootX)
        Me.fraBootSettings.Controls.Add(Me.Label4)
        Me.fraBootSettings.Location = New System.Drawing.Point(7, 258)
        Me.fraBootSettings.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraBootSettings.Name = "fraBootSettings"
        Me.fraBootSettings.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraBootSettings.Size = New System.Drawing.Size(271, 105)
        Me.fraBootSettings.TabIndex = 13
        Me.fraBootSettings.TabStop = False
        Me.fraBootSettings.Text = "Respawn Settings"
        '
        'txtBootMap
        '
        Me.txtBootMap.Location = New System.Drawing.Point(205, 13)
        Me.txtBootMap.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBootMap.Name = "txtBootMap"
        Me.txtBootMap.Size = New System.Drawing.Size(58, 23)
        Me.txtBootMap.TabIndex = 5
        Me.txtBootMap.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 18)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 15)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Respawn Map:"
        '
        'txtBootY
        '
        Me.txtBootY.Location = New System.Drawing.Point(205, 73)
        Me.txtBootY.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBootY.Name = "txtBootY"
        Me.txtBootY.Size = New System.Drawing.Size(58, 23)
        Me.txtBootY.TabIndex = 3
        Me.txtBootY.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 75)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Respawn Y:"
        '
        'txtBootX
        '
        Me.txtBootX.Location = New System.Drawing.Point(205, 43)
        Me.txtBootX.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBootX.Name = "txtBootX"
        Me.txtBootX.Size = New System.Drawing.Size(58, 23)
        Me.txtBootX.TabIndex = 1
        Me.txtBootX.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 43)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 15)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Respawn X:"
        '
        'fraMaxSizes
        '
        Me.fraMaxSizes.Controls.Add(Me.txtMaxY)
        Me.fraMaxSizes.Controls.Add(Me.Label2)
        Me.fraMaxSizes.Controls.Add(Me.txtMaxX)
        Me.fraMaxSizes.Controls.Add(Me.Label7)
        Me.fraMaxSizes.Location = New System.Drawing.Point(285, 258)
        Me.fraMaxSizes.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMaxSizes.Name = "fraMaxSizes"
        Me.fraMaxSizes.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.fraMaxSizes.Size = New System.Drawing.Size(249, 90)
        Me.fraMaxSizes.TabIndex = 12
        Me.fraMaxSizes.TabStop = False
        Me.fraMaxSizes.Text = "Map Sizes"
        '
        'txtMaxY
        '
        Me.txtMaxY.Location = New System.Drawing.Point(145, 48)
        Me.txtMaxY.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMaxY.Name = "txtMaxY"
        Me.txtMaxY.Size = New System.Drawing.Size(58, 23)
        Me.txtMaxY.TabIndex = 3
        Me.txtMaxY.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 52)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Maximum Y:"
        '
        'txtMaxX
        '
        Me.txtMaxX.Location = New System.Drawing.Point(145, 18)
        Me.txtMaxX.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtMaxX.Name = "txtMaxX"
        Me.txtMaxX.Size = New System.Drawing.Size(58, 23)
        Me.txtMaxX.TabIndex = 1
        Me.txtMaxX.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 22)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 15)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Maximum X:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnPreview)
        Me.GroupBox2.Controls.Add(Me.lstMusic)
        Me.GroupBox2.Location = New System.Drawing.Point(285, 3)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox2.Size = New System.Drawing.Size(281, 249)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Music"
        '
        'btnPreview
        '
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(57, 208)
        Me.btnPreview.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(162, 33)
        Me.btnPreview.TabIndex = 4
        Me.btnPreview.Text = "Preview Music"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'lstMusic
        '
        Me.lstMusic.FormattingEnabled = True
        Me.lstMusic.ItemHeight = 15
        Me.lstMusic.Location = New System.Drawing.Point(7, 22)
        Me.lstMusic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstMusic.Name = "lstMusic"
        Me.lstMusic.ScrollAlwaysVisible = True
        Me.lstMusic.Size = New System.Drawing.Size(242, 184)
        Me.lstMusic.TabIndex = 3
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(62, 7)
        Me.txtName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(215, 23)
        Me.txtName.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 10)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 15)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Name:"
        '
        'tpDirBlock
        '
        Me.tpDirBlock.Controls.Add(Me.Label12)
        Me.tpDirBlock.Location = New System.Drawing.Point(4, 24)
        Me.tpDirBlock.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpDirBlock.Name = "tpDirBlock"
        Me.tpDirBlock.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpDirBlock.Size = New System.Drawing.Size(542, 601)
        Me.tpDirBlock.TabIndex = 4
        Me.tpDirBlock.Text = "Directional Block"
        Me.tpDirBlock.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(26, 27)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(265, 15)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Just press the arrows to block that side of the tile."
        '
        'tpEvents
        '
        Me.tpEvents.Controls.Add(Me.lblPasteMode)
        Me.tpEvents.Controls.Add(Me.lblCopyMode)
        Me.tpEvents.Controls.Add(Me.btnPasteEvent)
        Me.tpEvents.Controls.Add(Me.Label16)
        Me.tpEvents.Controls.Add(Me.btnCopyEvent)
        Me.tpEvents.Controls.Add(Me.Label15)
        Me.tpEvents.Controls.Add(Me.Label13)
        Me.tpEvents.Location = New System.Drawing.Point(4, 24)
        Me.tpEvents.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpEvents.Name = "tpEvents"
        Me.tpEvents.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tpEvents.Size = New System.Drawing.Size(542, 601)
        Me.tpEvents.TabIndex = 5
        Me.tpEvents.Text = "Events"
        Me.tpEvents.UseVisualStyleBackColor = True
        '
        'lblPasteMode
        '
        Me.lblPasteMode.AutoSize = True
        Me.lblPasteMode.Location = New System.Drawing.Point(121, 197)
        Me.lblPasteMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPasteMode.Name = "lblPasteMode"
        Me.lblPasteMode.Size = New System.Drawing.Size(86, 15)
        Me.lblPasteMode.TabIndex = 6
        Me.lblPasteMode.Text = "PasteMode Off"
        '
        'lblCopyMode
        '
        Me.lblCopyMode.AutoSize = True
        Me.lblCopyMode.Location = New System.Drawing.Point(121, 129)
        Me.lblCopyMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCopyMode.Name = "lblCopyMode"
        Me.lblCopyMode.Size = New System.Drawing.Size(86, 15)
        Me.lblCopyMode.TabIndex = 5
        Me.lblCopyMode.Text = "CopyMode Off"
        '
        'btnPasteEvent
        '
        Me.btnPasteEvent.Location = New System.Drawing.Point(27, 192)
        Me.btnPasteEvent.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPasteEvent.Name = "btnPasteEvent"
        Me.btnPasteEvent.Size = New System.Drawing.Size(88, 27)
        Me.btnPasteEvent.TabIndex = 4
        Me.btnPasteEvent.Text = "Paste Event"
        Me.btnPasteEvent.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(23, 172)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(432, 15)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "To paste a copied Event, press the paste button, then click on the map to place i" &
    "t."
        '
        'btnCopyEvent
        '
        Me.btnCopyEvent.Location = New System.Drawing.Point(27, 123)
        Me.btnCopyEvent.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCopyEvent.Name = "btnCopyEvent"
        Me.btnCopyEvent.Size = New System.Drawing.Size(88, 27)
        Me.btnCopyEvent.TabIndex = 2
        Me.btnCopyEvent.Text = "Copy Event"
        Me.btnCopyEvent.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(23, 100)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(339, 15)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "To copy a existing Event, press the copy button, then the event."
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(23, 24)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(265, 15)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Click on the map where you want to add a event."
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Size = New System.Drawing.Size(542, 601)
        Me.TabPage1.TabIndex = 6
        Me.TabPage1.Text = "Map Effects"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.lblMapBrightness)
        Me.GroupBox6.Controls.Add(Me.scrlMapBrightness)
        Me.GroupBox6.Location = New System.Drawing.Point(13, 259)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox6.Size = New System.Drawing.Size(275, 45)
        Me.GroupBox6.TabIndex = 22
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Map Brightness"
        '
        'lblMapBrightness
        '
        Me.lblMapBrightness.AutoSize = True
        Me.lblMapBrightness.Location = New System.Drawing.Point(1, 19)
        Me.lblMapBrightness.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMapBrightness.Name = "lblMapBrightness"
        Me.lblMapBrightness.Size = New System.Drawing.Size(74, 15)
        Me.lblMapBrightness.TabIndex = 14
        Me.lblMapBrightness.Text = "Brightness: 0"
        '
        'scrlMapBrightness
        '
        Me.scrlMapBrightness.LargeChange = 1
        Me.scrlMapBrightness.Location = New System.Drawing.Point(98, 19)
        Me.scrlMapBrightness.Maximum = 255
        Me.scrlMapBrightness.Name = "scrlMapBrightness"
        Me.scrlMapBrightness.Size = New System.Drawing.Size(169, 17)
        Me.scrlMapBrightness.TabIndex = 10
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label20)
        Me.GroupBox5.Controls.Add(Me.cmbParallax)
        Me.GroupBox5.Location = New System.Drawing.Point(295, 192)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox5.Size = New System.Drawing.Size(275, 61)
        Me.GroupBox5.TabIndex = 21
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Map Parallax"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(0, 25)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(51, 15)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Parallax:"
        '
        'cmbParallax
        '
        Me.cmbParallax.FormattingEnabled = True
        Me.cmbParallax.Location = New System.Drawing.Point(53, 21)
        Me.cmbParallax.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbParallax.Name = "cmbParallax"
        Me.cmbParallax.Size = New System.Drawing.Size(186, 23)
        Me.cmbParallax.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.cmbPanorama)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 192)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox4.Size = New System.Drawing.Size(281, 61)
        Me.GroupBox4.TabIndex = 20
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Map Panorama"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(7, 25)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 15)
        Me.Label19.TabIndex = 1
        Me.Label19.Text = "Panorama:"
        '
        'cmbPanorama
        '
        Me.cmbPanorama.FormattingEnabled = True
        Me.cmbPanorama.Location = New System.Drawing.Point(82, 22)
        Me.cmbPanorama.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPanorama.Name = "cmbPanorama"
        Me.cmbPanorama.Size = New System.Drawing.Size(192, 23)
        Me.cmbPanorama.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkUseTint)
        Me.GroupBox3.Controls.Add(Me.lblMapAlpha)
        Me.GroupBox3.Controls.Add(Me.lblMapBlue)
        Me.GroupBox3.Controls.Add(Me.lblMapGreen)
        Me.GroupBox3.Controls.Add(Me.lblMapRed)
        Me.GroupBox3.Controls.Add(Me.scrlMapAlpha)
        Me.GroupBox3.Controls.Add(Me.scrlMapBlue)
        Me.GroupBox3.Controls.Add(Me.scrlMapGreen)
        Me.GroupBox3.Controls.Add(Me.scrlMapRed)
        Me.GroupBox3.Location = New System.Drawing.Point(295, 7)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(275, 178)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Map Tint"
        '
        'chkUseTint
        '
        Me.chkUseTint.AutoSize = True
        Me.chkUseTint.Location = New System.Drawing.Point(7, 22)
        Me.chkUseTint.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkUseTint.Name = "chkUseTint"
        Me.chkUseTint.Size = New System.Drawing.Size(97, 19)
        Me.chkUseTint.TabIndex = 18
        Me.chkUseTint.Text = "Use MapTint?"
        Me.chkUseTint.UseVisualStyleBackColor = True
        '
        'lblMapAlpha
        '
        Me.lblMapAlpha.AutoSize = True
        Me.lblMapAlpha.Location = New System.Drawing.Point(9, 111)
        Me.lblMapAlpha.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMapAlpha.Name = "lblMapAlpha"
        Me.lblMapAlpha.Size = New System.Drawing.Size(50, 15)
        Me.lblMapAlpha.TabIndex = 17
        Me.lblMapAlpha.Text = "Alpha: 0"
        '
        'lblMapBlue
        '
        Me.lblMapBlue.AutoSize = True
        Me.lblMapBlue.Location = New System.Drawing.Point(9, 89)
        Me.lblMapBlue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMapBlue.Name = "lblMapBlue"
        Me.lblMapBlue.Size = New System.Drawing.Size(42, 15)
        Me.lblMapBlue.TabIndex = 16
        Me.lblMapBlue.Text = "Blue: 0"
        '
        'lblMapGreen
        '
        Me.lblMapGreen.AutoSize = True
        Me.lblMapGreen.Location = New System.Drawing.Point(9, 67)
        Me.lblMapGreen.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMapGreen.Name = "lblMapGreen"
        Me.lblMapGreen.Size = New System.Drawing.Size(50, 15)
        Me.lblMapGreen.TabIndex = 15
        Me.lblMapGreen.Text = "Green: 0"
        '
        'lblMapRed
        '
        Me.lblMapRed.AutoSize = True
        Me.lblMapRed.Location = New System.Drawing.Point(7, 45)
        Me.lblMapRed.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMapRed.Name = "lblMapRed"
        Me.lblMapRed.Size = New System.Drawing.Size(39, 15)
        Me.lblMapRed.TabIndex = 14
        Me.lblMapRed.Text = "Red: 0"
        '
        'scrlMapAlpha
        '
        Me.scrlMapAlpha.LargeChange = 1
        Me.scrlMapAlpha.Location = New System.Drawing.Point(74, 109)
        Me.scrlMapAlpha.Maximum = 255
        Me.scrlMapAlpha.Name = "scrlMapAlpha"
        Me.scrlMapAlpha.Size = New System.Drawing.Size(169, 17)
        Me.scrlMapAlpha.TabIndex = 13
        '
        'scrlMapBlue
        '
        Me.scrlMapBlue.LargeChange = 1
        Me.scrlMapBlue.Location = New System.Drawing.Point(74, 88)
        Me.scrlMapBlue.Maximum = 255
        Me.scrlMapBlue.Name = "scrlMapBlue"
        Me.scrlMapBlue.Size = New System.Drawing.Size(169, 17)
        Me.scrlMapBlue.TabIndex = 12
        '
        'scrlMapGreen
        '
        Me.scrlMapGreen.LargeChange = 1
        Me.scrlMapGreen.Location = New System.Drawing.Point(74, 64)
        Me.scrlMapGreen.Maximum = 255
        Me.scrlMapGreen.Name = "scrlMapGreen"
        Me.scrlMapGreen.Size = New System.Drawing.Size(169, 17)
        Me.scrlMapGreen.TabIndex = 11
        '
        'scrlMapRed
        '
        Me.scrlMapRed.LargeChange = 1
        Me.scrlMapRed.Location = New System.Drawing.Point(74, 46)
        Me.scrlMapRed.Maximum = 255
        Me.scrlMapRed.Name = "scrlMapRed"
        Me.scrlMapRed.Size = New System.Drawing.Size(169, 17)
        Me.scrlMapRed.TabIndex = 10
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.scrlFogAlpha)
        Me.GroupBox1.Controls.Add(Me.lblFogAlpha)
        Me.GroupBox1.Controls.Add(Me.scrlFogSpeed)
        Me.GroupBox1.Controls.Add(Me.lblFogSpeed)
        Me.GroupBox1.Controls.Add(Me.scrlIntensity)
        Me.GroupBox1.Controls.Add(Me.lblIntensity)
        Me.GroupBox1.Controls.Add(Me.scrlFog)
        Me.GroupBox1.Controls.Add(Me.lblFogIndex)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.cmbWeather)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 7)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(281, 178)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Map Weather"
        '
        'scrlFogAlpha
        '
        Me.scrlFogAlpha.LargeChange = 1
        Me.scrlFogAlpha.Location = New System.Drawing.Point(105, 143)
        Me.scrlFogAlpha.Maximum = 255
        Me.scrlFogAlpha.Name = "scrlFogAlpha"
        Me.scrlFogAlpha.Size = New System.Drawing.Size(169, 17)
        Me.scrlFogAlpha.TabIndex = 9
        '
        'lblFogAlpha
        '
        Me.lblFogAlpha.AutoSize = True
        Me.lblFogAlpha.Location = New System.Drawing.Point(7, 145)
        Me.lblFogAlpha.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFogAlpha.Name = "lblFogAlpha"
        Me.lblFogAlpha.Size = New System.Drawing.Size(85, 15)
        Me.lblFogAlpha.TabIndex = 8
        Me.lblFogAlpha.Text = "Fog Alpha: 255"
        '
        'scrlFogSpeed
        '
        Me.scrlFogSpeed.LargeChange = 1
        Me.scrlFogSpeed.Location = New System.Drawing.Point(105, 117)
        Me.scrlFogSpeed.Name = "scrlFogSpeed"
        Me.scrlFogSpeed.Size = New System.Drawing.Size(169, 17)
        Me.scrlFogSpeed.TabIndex = 7
        '
        'lblFogSpeed
        '
        Me.lblFogSpeed.AutoSize = True
        Me.lblFogSpeed.Location = New System.Drawing.Point(7, 121)
        Me.lblFogSpeed.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFogSpeed.Name = "lblFogSpeed"
        Me.lblFogSpeed.Size = New System.Drawing.Size(83, 15)
        Me.lblFogSpeed.TabIndex = 6
        Me.lblFogSpeed.Text = "FogSpeed: 100"
        '
        'scrlIntensity
        '
        Me.scrlIntensity.LargeChange = 1
        Me.scrlIntensity.Location = New System.Drawing.Point(105, 59)
        Me.scrlIntensity.Name = "scrlIntensity"
        Me.scrlIntensity.Size = New System.Drawing.Size(169, 17)
        Me.scrlIntensity.TabIndex = 5
        '
        'lblIntensity
        '
        Me.lblIntensity.AutoSize = True
        Me.lblIntensity.Location = New System.Drawing.Point(7, 61)
        Me.lblIntensity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIntensity.Name = "lblIntensity"
        Me.lblIntensity.Size = New System.Drawing.Size(76, 15)
        Me.lblIntensity.TabIndex = 4
        Me.lblIntensity.Text = "Intensity: 100"
        '
        'scrlFog
        '
        Me.scrlFog.LargeChange = 1
        Me.scrlFog.Location = New System.Drawing.Point(105, 93)
        Me.scrlFog.Name = "scrlFog"
        Me.scrlFog.Size = New System.Drawing.Size(169, 17)
        Me.scrlFog.TabIndex = 3
        '
        'lblFogIndex
        '
        Me.lblFogIndex.AutoSize = True
        Me.lblFogIndex.Location = New System.Drawing.Point(7, 95)
        Me.lblFogIndex.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFogIndex.Name = "lblFogIndex"
        Me.lblFogIndex.Size = New System.Drawing.Size(39, 15)
        Me.lblFogIndex.TabIndex = 2
        Me.lblFogIndex.Text = "Fog: 1"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(7, 29)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(81, 15)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Weather Type:"
        '
        'cmbWeather
        '
        Me.cmbWeather.FormattingEnabled = True
        Me.cmbWeather.Items.AddRange(New Object() {"None", "Rain", "Snow", "Hail", "Sand Storm", "Storm", "Fog"})
        Me.cmbWeather.Location = New System.Drawing.Point(105, 25)
        Me.cmbWeather.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbWeather.Name = "cmbWeather"
        Me.cmbWeather.Size = New System.Drawing.Size(168, 23)
        Me.cmbWeather.TabIndex = 0
        '
        'frmEditor_Map
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(552, 658)
        Me.Controls.Add(Me.tabpages)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.pnlAttributes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.Name = "frmEditor_Map"
        Me.Text = "Map Editor"
        Me.pnlBack.ResumeLayout(False)
        CType(Me.picBackSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAttributes.ResumeLayout(False)
        Me.fraMapLight.ResumeLayout(False)
        Me.fraMapLight.PerformLayout()
        Me.fraAnimation.ResumeLayout(False)
        Me.fraMapWarp.ResumeLayout(False)
        Me.fraMapWarp.PerformLayout()
        Me.fraNpcSpawn.ResumeLayout(False)
        Me.fraNpcSpawn.PerformLayout()
        Me.fraHeal.ResumeLayout(False)
        Me.fraHeal.PerformLayout()
        Me.fraShop.ResumeLayout(False)
        Me.fraResource.ResumeLayout(False)
        Me.fraResource.PerformLayout()
        Me.fraMapItem.ResumeLayout(False)
        Me.fraMapItem.PerformLayout()
        CType(Me.picMapItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTrap.ResumeLayout(False)
        Me.fraTrap.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.tabpages.ResumeLayout(False)
        Me.tpTiles.ResumeLayout(False)
        Me.tpTiles.PerformLayout()
        Me.tpAttributes.ResumeLayout(False)
        Me.tpAttributes.PerformLayout()
        Me.tpNpcs.ResumeLayout(False)
        Me.fraNpcs.ResumeLayout(False)
        Me.fraNpcs.PerformLayout()
        Me.tpSettings.ResumeLayout(False)
        Me.tpSettings.PerformLayout()
        Me.fraMapSettings.ResumeLayout(False)
        Me.fraMapSettings.PerformLayout()
        Me.fraMapLinks.ResumeLayout(False)
        Me.fraMapLinks.PerformLayout()
        Me.fraBootSettings.ResumeLayout(False)
        Me.fraBootSettings.PerformLayout()
        Me.fraMaxSizes.ResumeLayout(False)
        Me.fraMaxSizes.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.tpDirBlock.ResumeLayout(False)
        Me.tpDirBlock.PerformLayout()
        Me.tpEvents.ResumeLayout(False)
        Me.tpEvents.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout

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
    Friend WithEvents ToolStrip As Windows.Forms.ToolStrip
    Friend WithEvents tsbSave As Windows.Forms.ToolStripButton
    Friend WithEvents tsbDiscard As Windows.Forms.ToolStripButton
    Friend WithEvents tabpages As Windows.Forms.TabControl
    Friend WithEvents tpTiles As Windows.Forms.TabPage
    Friend WithEvents tpNpcs As Windows.Forms.TabPage
    Friend WithEvents tpSettings As Windows.Forms.TabPage
    Friend WithEvents fraNpcs As Windows.Forms.GroupBox
    Friend WithEvents ComboBox23 As Windows.Forms.ComboBox
    Friend WithEvents txtName As Windows.Forms.TextBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents fraMapLinks As Windows.Forms.GroupBox
    Friend WithEvents txtDown As Windows.Forms.TextBox
    Friend WithEvents txtLeft As Windows.Forms.TextBox
    Friend WithEvents lblMap As Windows.Forms.Label
    Friend WithEvents txtRight As Windows.Forms.TextBox
    Friend WithEvents txtUp As Windows.Forms.TextBox
    Friend WithEvents fraBootSettings As Windows.Forms.GroupBox
    Friend WithEvents txtBootMap As Windows.Forms.TextBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtBootY As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents txtBootX As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents fraMaxSizes As Windows.Forms.GroupBox
    Friend WithEvents txtMaxY As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents txtMaxX As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents lstMusic As Windows.Forms.ListBox
    Friend WithEvents fraMapSettings As Windows.Forms.GroupBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents cmbMoral As Windows.Forms.ComboBox
    Friend WithEvents ToolStripSeparator1 As Windows.Forms.ToolStripSeparator
    Friend WithEvents cmbNpcList As Windows.Forms.ComboBox
    Friend WithEvents lstMapNpc As Windows.Forms.ListBox
    Friend WithEvents tpAttributes As Windows.Forms.TabPage
    Friend WithEvents cmbTileSets As Windows.Forms.ComboBox
    Friend WithEvents cmbAutoTile As Windows.Forms.ComboBox
    Friend WithEvents Label11 As Windows.Forms.Label
    Friend WithEvents Label10 As Windows.Forms.Label
    Friend WithEvents cmbLayers As Windows.Forms.ComboBox
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents tpDirBlock As Windows.Forms.TabPage
    Friend WithEvents tpEvents As Windows.Forms.TabPage
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents tsbMapGrid As Windows.Forms.ToolStripButton
    Friend WithEvents btnPreview As Windows.Forms.Button
    Friend WithEvents tsbFill As Windows.Forms.ToolStripButton
    Friend WithEvents tsbEyeDropper As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents chkInstance As Windows.Forms.CheckBox
    Friend WithEvents optLight As Windows.Forms.RadioButton
    Friend WithEvents btnPasteEvent As Windows.Forms.Button
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents btnCopyEvent As Windows.Forms.Button
    Friend WithEvents Label15 As Windows.Forms.Label
    Friend WithEvents lblPasteMode As Windows.Forms.Label
    Friend WithEvents lblCopyMode As Windows.Forms.Label
    Friend WithEvents TabPage1 As Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents chkUseTint As Windows.Forms.CheckBox
    Friend WithEvents lblMapAlpha As Windows.Forms.Label
    Friend WithEvents lblMapBlue As Windows.Forms.Label
    Friend WithEvents lblMapGreen As Windows.Forms.Label
    Friend WithEvents lblMapRed As Windows.Forms.Label
    Friend WithEvents scrlMapAlpha As Windows.Forms.HScrollBar
    Friend WithEvents scrlMapBlue As Windows.Forms.HScrollBar
    Friend WithEvents scrlMapGreen As Windows.Forms.HScrollBar
    Friend WithEvents scrlMapRed As Windows.Forms.HScrollBar
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents scrlFogAlpha As Windows.Forms.HScrollBar
    Friend WithEvents lblFogAlpha As Windows.Forms.Label
    Friend WithEvents scrlFogSpeed As Windows.Forms.HScrollBar
    Friend WithEvents lblFogSpeed As Windows.Forms.Label
    Friend WithEvents scrlIntensity As Windows.Forms.HScrollBar
    Friend WithEvents lblIntensity As Windows.Forms.Label
    Friend WithEvents scrlFog As Windows.Forms.HScrollBar
    Friend WithEvents lblFogIndex As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents cmbWeather As Windows.Forms.ComboBox
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents Label17 As Windows.Forms.Label
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents Label20 As Windows.Forms.Label
    Friend WithEvents cmbParallax As Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents Label19 As Windows.Forms.Label
    Friend WithEvents cmbPanorama As Windows.Forms.ComboBox
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
End Class
