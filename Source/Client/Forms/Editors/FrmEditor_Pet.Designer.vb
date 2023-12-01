<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditor_Pet
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        DarkGroupBox1 = New DarkUI.Controls.DarkGroupBox()
        lstIndex = New ListBox()
        DarkGroupBox2 = New DarkUI.Controls.DarkGroupBox()
        DarkGroupBox6 = New DarkUI.Controls.DarkGroupBox()
        cmbSkill4 = New DarkUI.Controls.DarkComboBox()
        DarkLabel19 = New DarkUI.Controls.DarkLabel()
        cmbSkill3 = New DarkUI.Controls.DarkComboBox()
        DarkLabel18 = New DarkUI.Controls.DarkLabel()
        cmbSkill2 = New DarkUI.Controls.DarkComboBox()
        DarkLabel17 = New DarkUI.Controls.DarkLabel()
        cmbSkill1 = New DarkUI.Controls.DarkComboBox()
        DarkLabel16 = New DarkUI.Controls.DarkLabel()
        DarkGroupBox4 = New DarkUI.Controls.DarkGroupBox()
        pnlPetlevel = New Panel()
        DarkGroupBox5 = New DarkUI.Controls.DarkGroupBox()
        cmbEvolve = New DarkUI.Controls.DarkComboBox()
        DarkLabel15 = New DarkUI.Controls.DarkLabel()
        nudEvolveLvl = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel14 = New DarkUI.Controls.DarkLabel()
        chkEvolve = New DarkUI.Controls.DarkCheckBox()
        nudMaxLevel = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel12 = New DarkUI.Controls.DarkLabel()
        nudPetPnts = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel13 = New DarkUI.Controls.DarkLabel()
        nudPetExp = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel11 = New DarkUI.Controls.DarkLabel()
        optDoNotLevel = New DarkUI.Controls.DarkRadioButton()
        optLevel = New DarkUI.Controls.DarkRadioButton()
        DarkGroupBox3 = New DarkUI.Controls.DarkGroupBox()
        pnlCustomStats = New Panel()
        nudLevel = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel10 = New DarkUI.Controls.DarkLabel()
        nudSpirit = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel7 = New DarkUI.Controls.DarkLabel()
        nudIntelligence = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel8 = New DarkUI.Controls.DarkLabel()
        nudLuck = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel9 = New DarkUI.Controls.DarkLabel()
        nudVitality = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel6 = New DarkUI.Controls.DarkLabel()
        nudEndurance = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel5 = New DarkUI.Controls.DarkLabel()
        nudStrength = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel4 = New DarkUI.Controls.DarkLabel()
        optAdoptStats = New DarkUI.Controls.DarkRadioButton()
        optCustomStats = New DarkUI.Controls.DarkRadioButton()
        DarkLabel3 = New DarkUI.Controls.DarkLabel()
        nudRange = New DarkUI.Controls.DarkNumericUpDown()
        DarkLabel2 = New DarkUI.Controls.DarkLabel()
        nudSprite = New DarkUI.Controls.DarkNumericUpDown()
        picSprite = New PictureBox()
        txtName = New DarkUI.Controls.DarkTextBox()
        DarkLabel1 = New DarkUI.Controls.DarkLabel()
        btnSave = New DarkUI.Controls.DarkButton()
        btnCancel = New DarkUI.Controls.DarkButton()
        btnDelete = New DarkUI.Controls.DarkButton()
        DarkGroupBox1.SuspendLayout()
        DarkGroupBox2.SuspendLayout()
        DarkGroupBox6.SuspendLayout()
        DarkGroupBox4.SuspendLayout()
        pnlPetlevel.SuspendLayout()
        DarkGroupBox5.SuspendLayout()
        CType(nudEvolveLvl, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudMaxLevel, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPetPnts, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPetExp, ComponentModel.ISupportInitialize).BeginInit()
        DarkGroupBox3.SuspendLayout()
        pnlCustomStats.SuspendLayout()
        CType(nudLevel, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudSpirit, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudIntelligence, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudLuck, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudVitality, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudEndurance, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudStrength, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudRange, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudSprite, ComponentModel.ISupportInitialize).BeginInit()
        CType(picSprite, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DarkGroupBox1
        ' 
        DarkGroupBox1.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox1.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox1.Controls.Add(lstIndex)
        DarkGroupBox1.ForeColor = Color.Gainsboro
        DarkGroupBox1.Location = New Point(2, 8)
        DarkGroupBox1.Margin = New Padding(10, 8, 10, 8)
        DarkGroupBox1.Name = "DarkGroupBox1"
        DarkGroupBox1.Padding = New Padding(10, 8, 10, 8)
        DarkGroupBox1.Size = New Size(593, 1214)
        DarkGroupBox1.TabIndex = 0
        DarkGroupBox1.TabStop = False
        DarkGroupBox1.Text = "Pet List"
        ' 
        ' lstIndex
        ' 
        lstIndex.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        lstIndex.BorderStyle = BorderStyle.FixedSingle
        lstIndex.ForeColor = Color.Gainsboro
        lstIndex.FormattingEnabled = True
        lstIndex.ItemHeight = 41
        lstIndex.Location = New Point(17, 44)
        lstIndex.Margin = New Padding(10, 8, 10, 8)
        lstIndex.Name = "lstIndex"
        lstIndex.Size = New Size(558, 1109)
        lstIndex.TabIndex = 1
        ' 
        ' DarkGroupBox2
        ' 
        DarkGroupBox2.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox2.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox2.Controls.Add(DarkGroupBox6)
        DarkGroupBox2.Controls.Add(DarkGroupBox4)
        DarkGroupBox2.Controls.Add(DarkGroupBox3)
        DarkGroupBox2.Controls.Add(DarkLabel3)
        DarkGroupBox2.Controls.Add(nudRange)
        DarkGroupBox2.Controls.Add(DarkLabel2)
        DarkGroupBox2.Controls.Add(nudSprite)
        DarkGroupBox2.Controls.Add(picSprite)
        DarkGroupBox2.Controls.Add(txtName)
        DarkGroupBox2.Controls.Add(DarkLabel1)
        DarkGroupBox2.ForeColor = Color.Gainsboro
        DarkGroupBox2.Location = New Point(612, 8)
        DarkGroupBox2.Margin = New Padding(10, 8, 10, 8)
        DarkGroupBox2.Name = "DarkGroupBox2"
        DarkGroupBox2.Padding = New Padding(10, 8, 10, 8)
        DarkGroupBox2.Size = New Size(1161, 1490)
        DarkGroupBox2.TabIndex = 1
        DarkGroupBox2.TabStop = False
        DarkGroupBox2.Text = "Properties"
        ' 
        ' DarkGroupBox6
        ' 
        DarkGroupBox6.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox6.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox6.Controls.Add(cmbSkill4)
        DarkGroupBox6.Controls.Add(DarkLabel19)
        DarkGroupBox6.Controls.Add(cmbSkill3)
        DarkGroupBox6.Controls.Add(DarkLabel18)
        DarkGroupBox6.Controls.Add(cmbSkill2)
        DarkGroupBox6.Controls.Add(DarkLabel17)
        DarkGroupBox6.Controls.Add(cmbSkill1)
        DarkGroupBox6.Controls.Add(DarkLabel16)
        DarkGroupBox6.ForeColor = Color.Gainsboro
        DarkGroupBox6.Location = New Point(17, 1230)
        DarkGroupBox6.Margin = New Padding(10, 8, 10, 8)
        DarkGroupBox6.Name = "DarkGroupBox6"
        DarkGroupBox6.Padding = New Padding(10, 8, 10, 8)
        DarkGroupBox6.Size = New Size(1127, 241)
        DarkGroupBox6.TabIndex = 10
        DarkGroupBox6.TabStop = False
        DarkGroupBox6.Text = "Start Skills"
        ' 
        ' cmbSkill4
        ' 
        cmbSkill4.DrawMode = DrawMode.OwnerDrawFixed
        cmbSkill4.FormattingEnabled = True
        cmbSkill4.Location = New Point(704, 145)
        cmbSkill4.Margin = New Padding(10, 8, 10, 8)
        cmbSkill4.Name = "cmbSkill4"
        cmbSkill4.Size = New Size(383, 48)
        cmbSkill4.TabIndex = 7
        ' 
        ' DarkLabel19
        ' 
        DarkLabel19.AutoSize = True
        DarkLabel19.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel19.Location = New Point(580, 156)
        DarkLabel19.Margin = New Padding(10, 0, 10, 0)
        DarkLabel19.Name = "DarkLabel19"
        DarkLabel19.Size = New Size(101, 41)
        DarkLabel19.TabIndex = 6
        DarkLabel19.Text = "Skill 4:"
        ' 
        ' cmbSkill3
        ' 
        cmbSkill3.DrawMode = DrawMode.OwnerDrawFixed
        cmbSkill3.FormattingEnabled = True
        cmbSkill3.Location = New Point(136, 145)
        cmbSkill3.Margin = New Padding(10, 8, 10, 8)
        cmbSkill3.Name = "cmbSkill3"
        cmbSkill3.Size = New Size(383, 48)
        cmbSkill3.TabIndex = 5
        ' 
        ' DarkLabel18
        ' 
        DarkLabel18.AutoSize = True
        DarkLabel18.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel18.Location = New Point(12, 156)
        DarkLabel18.Margin = New Padding(10, 0, 10, 0)
        DarkLabel18.Name = "DarkLabel18"
        DarkLabel18.Size = New Size(101, 41)
        DarkLabel18.TabIndex = 4
        DarkLabel18.Text = "Skill 3:"
        ' 
        ' cmbSkill2
        ' 
        cmbSkill2.DrawMode = DrawMode.OwnerDrawFixed
        cmbSkill2.FormattingEnabled = True
        cmbSkill2.Location = New Point(704, 60)
        cmbSkill2.Margin = New Padding(10, 8, 10, 8)
        cmbSkill2.Name = "cmbSkill2"
        cmbSkill2.Size = New Size(383, 48)
        cmbSkill2.TabIndex = 3
        ' 
        ' DarkLabel17
        ' 
        DarkLabel17.AutoSize = True
        DarkLabel17.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel17.Location = New Point(580, 68)
        DarkLabel17.Margin = New Padding(10, 0, 10, 0)
        DarkLabel17.Name = "DarkLabel17"
        DarkLabel17.Size = New Size(101, 41)
        DarkLabel17.TabIndex = 2
        DarkLabel17.Text = "Skill 2:"
        ' 
        ' cmbSkill1
        ' 
        cmbSkill1.DrawMode = DrawMode.OwnerDrawFixed
        cmbSkill1.FormattingEnabled = True
        cmbSkill1.Location = New Point(136, 60)
        cmbSkill1.Margin = New Padding(10, 8, 10, 8)
        cmbSkill1.Name = "cmbSkill1"
        cmbSkill1.Size = New Size(383, 48)
        cmbSkill1.TabIndex = 1
        ' 
        ' DarkLabel16
        ' 
        DarkLabel16.AutoSize = True
        DarkLabel16.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel16.Location = New Point(12, 68)
        DarkLabel16.Margin = New Padding(10, 0, 10, 0)
        DarkLabel16.Name = "DarkLabel16"
        DarkLabel16.Size = New Size(101, 41)
        DarkLabel16.TabIndex = 0
        DarkLabel16.Text = "Skill 1:"
        ' 
        ' DarkGroupBox4
        ' 
        DarkGroupBox4.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox4.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox4.Controls.Add(pnlPetlevel)
        DarkGroupBox4.Controls.Add(optDoNotLevel)
        DarkGroupBox4.Controls.Add(optLevel)
        DarkGroupBox4.ForeColor = Color.Gainsboro
        DarkGroupBox4.Location = New Point(17, 689)
        DarkGroupBox4.Margin = New Padding(10, 8, 10, 8)
        DarkGroupBox4.Name = "DarkGroupBox4"
        DarkGroupBox4.Padding = New Padding(10, 8, 10, 8)
        DarkGroupBox4.Size = New Size(1127, 525)
        DarkGroupBox4.TabIndex = 9
        DarkGroupBox4.TabStop = False
        DarkGroupBox4.Text = "Leveling"
        ' 
        ' pnlPetlevel
        ' 
        pnlPetlevel.Controls.Add(DarkGroupBox5)
        pnlPetlevel.Controls.Add(nudMaxLevel)
        pnlPetlevel.Controls.Add(DarkLabel12)
        pnlPetlevel.Controls.Add(nudPetPnts)
        pnlPetlevel.Controls.Add(DarkLabel13)
        pnlPetlevel.Controls.Add(nudPetExp)
        pnlPetlevel.Controls.Add(DarkLabel11)
        pnlPetlevel.Location = New Point(17, 131)
        pnlPetlevel.Margin = New Padding(10, 8, 10, 8)
        pnlPetlevel.Name = "pnlPetlevel"
        pnlPetlevel.Size = New Size(1093, 372)
        pnlPetlevel.TabIndex = 2
        ' 
        ' DarkGroupBox5
        ' 
        DarkGroupBox5.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox5.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox5.Controls.Add(cmbEvolve)
        DarkGroupBox5.Controls.Add(DarkLabel15)
        DarkGroupBox5.Controls.Add(nudEvolveLvl)
        DarkGroupBox5.Controls.Add(DarkLabel14)
        DarkGroupBox5.Controls.Add(chkEvolve)
        DarkGroupBox5.ForeColor = Color.Gainsboro
        DarkGroupBox5.Location = New Point(17, 120)
        DarkGroupBox5.Margin = New Padding(10, 8, 10, 8)
        DarkGroupBox5.Name = "DarkGroupBox5"
        DarkGroupBox5.Padding = New Padding(10, 8, 10, 8)
        DarkGroupBox5.Size = New Size(1056, 238)
        DarkGroupBox5.TabIndex = 7
        DarkGroupBox5.TabStop = False
        DarkGroupBox5.Text = "Evolution"
        ' 
        ' cmbEvolve
        ' 
        cmbEvolve.DrawMode = DrawMode.OwnerDrawFixed
        cmbEvolve.FormattingEnabled = True
        cmbEvolve.Location = New Point(284, 142)
        cmbEvolve.Margin = New Padding(10, 8, 10, 8)
        cmbEvolve.Name = "cmbEvolve"
        cmbEvolve.Size = New Size(750, 48)
        cmbEvolve.TabIndex = 4
        ' 
        ' DarkLabel15
        ' 
        DarkLabel15.AutoSize = True
        DarkLabel15.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel15.Location = New Point(17, 150)
        DarkLabel15.Margin = New Padding(10, 0, 10, 0)
        DarkLabel15.Name = "DarkLabel15"
        DarkLabel15.Size = New Size(182, 41)
        DarkLabel15.TabIndex = 3
        DarkLabel15.Text = "Evolves into:"
        ' 
        ' nudEvolveLvl
        ' 
        nudEvolveLvl.Location = New Point(865, 66)
        nudEvolveLvl.Margin = New Padding(10, 8, 10, 8)
        nudEvolveLvl.Name = "nudEvolveLvl"
        nudEvolveLvl.Size = New Size(175, 47)
        nudEvolveLvl.TabIndex = 2
        ' 
        ' DarkLabel14
        ' 
        DarkLabel14.AutoSize = True
        DarkLabel14.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel14.Location = New Point(585, 74)
        DarkLabel14.Margin = New Padding(10, 0, 10, 0)
        DarkLabel14.Name = "DarkLabel14"
        DarkLabel14.Size = New Size(240, 41)
        DarkLabel14.TabIndex = 1
        DarkLabel14.Text = "Evolves on Level:"
        ' 
        ' chkEvolve
        ' 
        chkEvolve.AutoSize = True
        chkEvolve.Location = New Point(17, 60)
        chkEvolve.Margin = New Padding(10, 8, 10, 8)
        chkEvolve.Name = "chkEvolve"
        chkEvolve.Size = New Size(249, 45)
        chkEvolve.TabIndex = 0
        chkEvolve.Text = "Pet Can Evolve"
        ' 
        ' nudMaxLevel
        ' 
        nudMaxLevel.Location = New Point(920, 38)
        nudMaxLevel.Margin = New Padding(10, 8, 10, 8)
        nudMaxLevel.Name = "nudMaxLevel"
        nudMaxLevel.Size = New Size(134, 47)
        nudMaxLevel.TabIndex = 6
        nudMaxLevel.Value = New Decimal(New Integer() {100, 0, 0, 0})
        ' 
        ' DarkLabel12
        ' 
        DarkLabel12.AutoSize = True
        DarkLabel12.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel12.Location = New Point(736, 44)
        DarkLabel12.Margin = New Padding(10, 0, 10, 0)
        DarkLabel12.Name = "DarkLabel12"
        DarkLabel12.Size = New Size(156, 41)
        DarkLabel12.TabIndex = 5
        DarkLabel12.Text = "Max Level:"
        ' 
        ' nudPetPnts
        ' 
        nudPetPnts.Location = New Point(614, 38)
        nudPetPnts.Margin = New Padding(10, 8, 10, 8)
        nudPetPnts.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        nudPetPnts.Name = "nudPetPnts"
        nudPetPnts.Size = New Size(102, 47)
        nudPetPnts.TabIndex = 4
        nudPetPnts.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' DarkLabel13
        ' 
        DarkLabel13.AutoSize = True
        DarkLabel13.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel13.Location = New Point(359, 44)
        DarkLabel13.Margin = New Padding(10, 0, 10, 0)
        DarkLabel13.Name = "DarkLabel13"
        DarkLabel13.Size = New Size(231, 41)
        DarkLabel13.TabIndex = 3
        DarkLabel13.Text = "Points Per Level:"
        ' 
        ' nudPetExp
        ' 
        nudPetExp.Location = New Point(209, 38)
        nudPetExp.Margin = New Padding(10, 8, 10, 8)
        nudPetExp.Name = "nudPetExp"
        nudPetExp.Size = New Size(134, 47)
        nudPetExp.TabIndex = 1
        nudPetExp.Value = New Decimal(New Integer() {100, 0, 0, 0})
        ' 
        ' DarkLabel11
        ' 
        DarkLabel11.AutoSize = True
        DarkLabel11.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel11.Location = New Point(12, 44)
        DarkLabel11.Margin = New Padding(10, 0, 10, 0)
        DarkLabel11.Name = "DarkLabel11"
        DarkLabel11.Size = New Size(173, 41)
        DarkLabel11.TabIndex = 0
        DarkLabel11.Text = "Exp Gain %:"
        ' 
        ' optDoNotLevel
        ' 
        optDoNotLevel.AutoSize = True
        optDoNotLevel.Location = New Point(748, 60)
        optDoNotLevel.Margin = New Padding(10, 8, 10, 8)
        optDoNotLevel.Name = "optDoNotLevel"
        optDoNotLevel.Size = New Size(295, 45)
        optDoNotLevel.TabIndex = 1
        optDoNotLevel.Text = "Does Not LevelUp"
        ' 
        ' optLevel
        ' 
        optLevel.AutoSize = True
        optLevel.Location = New Point(17, 60)
        optLevel.Margin = New Padding(10, 8, 10, 8)
        optLevel.Name = "optLevel"
        optLevel.Size = New Size(314, 45)
        optLevel.TabIndex = 0
        optLevel.Text = "Level by Experience"
        ' 
        ' DarkGroupBox3
        ' 
        DarkGroupBox3.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        DarkGroupBox3.BorderColor = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        DarkGroupBox3.Controls.Add(pnlCustomStats)
        DarkGroupBox3.Controls.Add(optAdoptStats)
        DarkGroupBox3.Controls.Add(optCustomStats)
        DarkGroupBox3.ForeColor = Color.Gainsboro
        DarkGroupBox3.Location = New Point(17, 243)
        DarkGroupBox3.Margin = New Padding(10, 8, 10, 8)
        DarkGroupBox3.Name = "DarkGroupBox3"
        DarkGroupBox3.Padding = New Padding(10, 8, 10, 8)
        DarkGroupBox3.Size = New Size(1122, 426)
        DarkGroupBox3.TabIndex = 8
        DarkGroupBox3.TabStop = False
        DarkGroupBox3.Text = "Starting Stats"
        ' 
        ' pnlCustomStats
        ' 
        pnlCustomStats.Controls.Add(nudLevel)
        pnlCustomStats.Controls.Add(DarkLabel10)
        pnlCustomStats.Controls.Add(nudSpirit)
        pnlCustomStats.Controls.Add(DarkLabel7)
        pnlCustomStats.Controls.Add(nudIntelligence)
        pnlCustomStats.Controls.Add(DarkLabel8)
        pnlCustomStats.Controls.Add(nudLuck)
        pnlCustomStats.Controls.Add(DarkLabel9)
        pnlCustomStats.Controls.Add(nudVitality)
        pnlCustomStats.Controls.Add(DarkLabel6)
        pnlCustomStats.Controls.Add(nudEndurance)
        pnlCustomStats.Controls.Add(DarkLabel5)
        pnlCustomStats.Controls.Add(nudStrength)
        pnlCustomStats.Controls.Add(DarkLabel4)
        pnlCustomStats.Location = New Point(17, 131)
        pnlCustomStats.Margin = New Padding(10, 8, 10, 8)
        pnlCustomStats.Name = "pnlCustomStats"
        pnlCustomStats.Size = New Size(1088, 279)
        pnlCustomStats.TabIndex = 2
        ' 
        ' nudLevel
        ' 
        nudLevel.Location = New Point(168, 189)
        nudLevel.Margin = New Padding(10, 8, 10, 8)
        nudLevel.Name = "nudLevel"
        nudLevel.Size = New Size(153, 47)
        nudLevel.TabIndex = 13
        ' 
        ' DarkLabel10
        ' 
        DarkLabel10.AutoSize = True
        DarkLabel10.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel10.Location = New Point(12, 197)
        DarkLabel10.Margin = New Padding(10, 0, 10, 0)
        DarkLabel10.Name = "DarkLabel10"
        DarkLabel10.Size = New Size(92, 41)
        DarkLabel10.TabIndex = 12
        DarkLabel10.Text = "Level:"
        ' 
        ' nudSpirit
        ' 
        nudSpirit.Location = New Point(920, 107)
        nudSpirit.Margin = New Padding(10, 8, 10, 8)
        nudSpirit.Name = "nudSpirit"
        nudSpirit.Size = New Size(153, 47)
        nudSpirit.TabIndex = 11
        ' 
        ' DarkLabel7
        ' 
        DarkLabel7.AutoSize = True
        DarkLabel7.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel7.Location = New Point(765, 115)
        DarkLabel7.Margin = New Padding(10, 0, 10, 0)
        DarkLabel7.Name = "DarkLabel7"
        DarkLabel7.Size = New Size(93, 41)
        DarkLabel7.TabIndex = 10
        DarkLabel7.Text = "Spirit:"
        ' 
        ' nudIntelligence
        ' 
        nudIntelligence.Location = New Point(563, 107)
        nudIntelligence.Margin = New Padding(10, 8, 10, 8)
        nudIntelligence.Name = "nudIntelligence"
        nudIntelligence.Size = New Size(153, 47)
        nudIntelligence.TabIndex = 9
        ' 
        ' DarkLabel8
        ' 
        DarkLabel8.AutoSize = True
        DarkLabel8.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel8.Location = New Point(374, 115)
        DarkLabel8.Margin = New Padding(10, 0, 10, 0)
        DarkLabel8.Name = "DarkLabel8"
        DarkLabel8.Size = New Size(178, 41)
        DarkLabel8.TabIndex = 8
        DarkLabel8.Text = "Intelligence:"
        ' 
        ' nudLuck
        ' 
        nudLuck.Location = New Point(168, 107)
        nudLuck.Margin = New Padding(10, 8, 10, 8)
        nudLuck.Name = "nudLuck"
        nudLuck.Size = New Size(153, 47)
        nudLuck.TabIndex = 7
        ' 
        ' DarkLabel9
        ' 
        DarkLabel9.AutoSize = True
        DarkLabel9.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel9.Location = New Point(12, 115)
        DarkLabel9.Margin = New Padding(10, 0, 10, 0)
        DarkLabel9.Name = "DarkLabel9"
        DarkLabel9.Size = New Size(85, 41)
        DarkLabel9.TabIndex = 6
        DarkLabel9.Text = "Luck:"
        ' 
        ' nudVitality
        ' 
        nudVitality.Location = New Point(920, 25)
        nudVitality.Margin = New Padding(10, 8, 10, 8)
        nudVitality.Name = "nudVitality"
        nudVitality.Size = New Size(153, 47)
        nudVitality.TabIndex = 5
        ' 
        ' DarkLabel6
        ' 
        DarkLabel6.AutoSize = True
        DarkLabel6.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel6.Location = New Point(763, 33)
        DarkLabel6.Margin = New Padding(10, 0, 10, 0)
        DarkLabel6.Name = "DarkLabel6"
        DarkLabel6.Size = New Size(115, 41)
        DarkLabel6.TabIndex = 4
        DarkLabel6.Text = "Vitality:"
        ' 
        ' nudEndurance
        ' 
        nudEndurance.Location = New Point(563, 25)
        nudEndurance.Margin = New Padding(10, 8, 10, 8)
        nudEndurance.Name = "nudEndurance"
        nudEndurance.Size = New Size(153, 47)
        nudEndurance.TabIndex = 3
        ' 
        ' DarkLabel5
        ' 
        DarkLabel5.AutoSize = True
        DarkLabel5.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel5.Location = New Point(372, 33)
        DarkLabel5.Margin = New Padding(10, 0, 10, 0)
        DarkLabel5.Name = "DarkLabel5"
        DarkLabel5.Size = New Size(164, 41)
        DarkLabel5.TabIndex = 2
        DarkLabel5.Text = "Endurance:"
        ' 
        ' nudStrength
        ' 
        nudStrength.Location = New Point(168, 25)
        nudStrength.Margin = New Padding(10, 8, 10, 8)
        nudStrength.Name = "nudStrength"
        nudStrength.Size = New Size(153, 47)
        nudStrength.TabIndex = 1
        ' 
        ' DarkLabel4
        ' 
        DarkLabel4.AutoSize = True
        DarkLabel4.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel4.Location = New Point(10, 33)
        DarkLabel4.Margin = New Padding(10, 0, 10, 0)
        DarkLabel4.Name = "DarkLabel4"
        DarkLabel4.Size = New Size(138, 41)
        DarkLabel4.TabIndex = 0
        DarkLabel4.Text = "Strength:"
        ' 
        ' optAdoptStats
        ' 
        optAdoptStats.AutoSize = True
        optAdoptStats.Location = New Point(763, 60)
        optAdoptStats.Margin = New Padding(10, 8, 10, 8)
        optAdoptStats.Name = "optAdoptStats"
        optAdoptStats.Size = New Size(325, 45)
        optAdoptStats.TabIndex = 1
        optAdoptStats.TabStop = True
        optAdoptStats.Text = "Adopt Owner's Stats"
        ' 
        ' optCustomStats
        ' 
        optCustomStats.AutoSize = True
        optCustomStats.Location = New Point(17, 60)
        optCustomStats.Margin = New Padding(10, 8, 10, 8)
        optCustomStats.Name = "optCustomStats"
        optCustomStats.Size = New Size(229, 45)
        optCustomStats.TabIndex = 0
        optCustomStats.TabStop = True
        optCustomStats.Text = "Custom Stats"
        ' 
        ' DarkLabel3
        ' 
        DarkLabel3.AutoSize = True
        DarkLabel3.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel3.Location = New Point(17, 150)
        DarkLabel3.Margin = New Padding(10, 0, 10, 0)
        DarkLabel3.Name = "DarkLabel3"
        DarkLabel3.Size = New Size(109, 41)
        DarkLabel3.TabIndex = 7
        DarkLabel3.Text = "Range:"
        ' 
        ' nudRange
        ' 
        nudRange.Location = New Point(194, 145)
        nudRange.Margin = New Padding(10, 8, 10, 8)
        nudRange.Name = "nudRange"
        nudRange.Size = New Size(216, 47)
        nudRange.TabIndex = 6
        ' 
        ' DarkLabel2
        ' 
        DarkLabel2.AutoSize = True
        DarkLabel2.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel2.Location = New Point(636, 150)
        DarkLabel2.Margin = New Padding(10, 0, 10, 0)
        DarkLabel2.Name = "DarkLabel2"
        DarkLabel2.Size = New Size(102, 41)
        DarkLabel2.TabIndex = 5
        DarkLabel2.Text = "Sprite:"
        ' 
        ' nudSprite
        ' 
        nudSprite.Location = New Point(780, 145)
        nudSprite.Margin = New Padding(10, 8, 10, 8)
        nudSprite.Name = "nudSprite"
        nudSprite.Size = New Size(216, 47)
        nudSprite.TabIndex = 4
        ' 
        ' picSprite
        ' 
        picSprite.BackColor = Color.Black
        picSprite.Location = New Point(1003, 74)
        picSprite.Margin = New Padding(10, 8, 10, 8)
        picSprite.Name = "picSprite"
        picSprite.Size = New Size(78, 87)
        picSprite.TabIndex = 3
        picSprite.TabStop = False
        ' 
        ' txtName
        ' 
        txtName.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtName.BorderStyle = BorderStyle.FixedSingle
        txtName.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtName.Location = New Point(194, 57)
        txtName.Margin = New Padding(10, 8, 10, 8)
        txtName.Name = "txtName"
        txtName.Size = New Size(786, 47)
        txtName.TabIndex = 1
        ' 
        ' DarkLabel1
        ' 
        DarkLabel1.AutoSize = True
        DarkLabel1.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel1.Location = New Point(17, 63)
        DarkLabel1.Margin = New Padding(10, 0, 10, 0)
        DarkLabel1.Name = "DarkLabel1"
        DarkLabel1.Size = New Size(104, 41)
        DarkLabel1.TabIndex = 0
        DarkLabel1.Text = "Name:"
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(19, 1238)
        btnSave.Margin = New Padding(10, 8, 10, 8)
        btnSave.Name = "btnSave"
        btnSave.Padding = New Padding(15, 16, 15, 16)
        btnSave.Size = New Size(561, 74)
        btnSave.TabIndex = 2
        btnSave.Text = "Save"
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(19, 1419)
        btnCancel.Margin = New Padding(10, 8, 10, 8)
        btnCancel.Name = "btnCancel"
        btnCancel.Padding = New Padding(15, 16, 15, 16)
        btnCancel.Size = New Size(561, 74)
        btnCancel.TabIndex = 3
        btnCancel.Text = "Cancel"
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(19, 1328)
        btnDelete.Margin = New Padding(10, 8, 10, 8)
        btnDelete.Name = "btnDelete"
        btnDelete.Padding = New Padding(15, 16, 15, 16)
        btnDelete.Size = New Size(561, 74)
        btnDelete.TabIndex = 4
        btnDelete.Text = "Delete"
        ' 
        ' frmEditor_Pet
        ' 
        AutoScaleDimensions = New SizeF(17F, 41F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        ClientSize = New Size(1790, 1509)
        Controls.Add(btnDelete)
        Controls.Add(btnCancel)
        Controls.Add(btnSave)
        Controls.Add(DarkGroupBox2)
        Controls.Add(DarkGroupBox1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(10, 8, 10, 8)
        Name = "frmEditor_Pet"
        Text = "Pet Editor"
        DarkGroupBox1.ResumeLayout(False)
        DarkGroupBox2.ResumeLayout(False)
        DarkGroupBox2.PerformLayout()
        DarkGroupBox6.ResumeLayout(False)
        DarkGroupBox6.PerformLayout()
        DarkGroupBox4.ResumeLayout(False)
        DarkGroupBox4.PerformLayout()
        pnlPetlevel.ResumeLayout(False)
        pnlPetlevel.PerformLayout()
        DarkGroupBox5.ResumeLayout(False)
        DarkGroupBox5.PerformLayout()
        CType(nudEvolveLvl, ComponentModel.ISupportInitialize).EndInit()
        CType(nudMaxLevel, ComponentModel.ISupportInitialize).EndInit()
        CType(nudPetPnts, ComponentModel.ISupportInitialize).EndInit()
        CType(nudPetExp, ComponentModel.ISupportInitialize).EndInit()
        DarkGroupBox3.ResumeLayout(False)
        DarkGroupBox3.PerformLayout()
        pnlCustomStats.ResumeLayout(False)
        pnlCustomStats.PerformLayout()
        CType(nudLevel, ComponentModel.ISupportInitialize).EndInit()
        CType(nudSpirit, ComponentModel.ISupportInitialize).EndInit()
        CType(nudIntelligence, ComponentModel.ISupportInitialize).EndInit()
        CType(nudLuck, ComponentModel.ISupportInitialize).EndInit()
        CType(nudVitality, ComponentModel.ISupportInitialize).EndInit()
        CType(nudEndurance, ComponentModel.ISupportInitialize).EndInit()
        CType(nudStrength, ComponentModel.ISupportInitialize).EndInit()
        CType(nudRange, ComponentModel.ISupportInitialize).EndInit()
        CType(nudSprite, ComponentModel.ISupportInitialize).EndInit()
        CType(picSprite, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents DarkGroupBox1 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents lstIndex As System.Windows.Forms.ListBox
    Friend WithEvents DarkGroupBox2 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents txtName As DarkUI.Controls.DarkTextBox
    Friend WithEvents DarkLabel1 As DarkUI.Controls.DarkLabel
    Friend WithEvents picSprite As System.Windows.Forms.PictureBox
    Friend WithEvents DarkLabel2 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudSprite As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel3 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudRange As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkGroupBox3 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents optCustomStats As DarkUI.Controls.DarkRadioButton
    Friend WithEvents optAdoptStats As DarkUI.Controls.DarkRadioButton
    Friend WithEvents pnlCustomStats As System.Windows.Forms.Panel
    Friend WithEvents nudVitality As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel6 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudEndurance As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel5 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudStrength As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel4 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudSpirit As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel7 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudIntelligence As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel8 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudLuck As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel9 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudLevel As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel10 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkGroupBox4 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents optLevel As DarkUI.Controls.DarkRadioButton
    Friend WithEvents optDoNotLevel As DarkUI.Controls.DarkRadioButton
    Friend WithEvents pnlPetlevel As System.Windows.Forms.Panel
    Friend WithEvents nudPetExp As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel11 As DarkUI.Controls.DarkLabel
    Friend WithEvents DarkLabel13 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudMaxLevel As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel12 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudPetPnts As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkGroupBox5 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents chkEvolve As DarkUI.Controls.DarkCheckBox
    Friend WithEvents DarkLabel14 As DarkUI.Controls.DarkLabel
    Friend WithEvents nudEvolveLvl As DarkUI.Controls.DarkNumericUpDown
    Friend WithEvents DarkLabel15 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbEvolve As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkGroupBox6 As DarkUI.Controls.DarkGroupBox
    Friend WithEvents cmbSkill4 As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel19 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbSkill3 As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel18 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbSkill2 As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel17 As DarkUI.Controls.DarkLabel
    Friend WithEvents cmbSkill1 As DarkUI.Controls.DarkComboBox
    Friend WithEvents DarkLabel16 As DarkUI.Controls.DarkLabel
    Friend WithEvents btnSave As DarkUI.Controls.DarkButton
    Friend WithEvents btnCancel As DarkUI.Controls.DarkButton
    Friend WithEvents btnDelete As DarkUI.Controls.DarkButton
End Class
