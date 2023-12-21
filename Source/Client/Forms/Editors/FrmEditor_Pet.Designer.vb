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
        DarkGroupBox1.Location = New Point(2, 6)
        DarkGroupBox1.Margin = New Padding(8, 6, 8, 6)
        DarkGroupBox1.Name = "DarkGroupBox1"
        DarkGroupBox1.Padding = New Padding(8, 6, 8, 6)
        DarkGroupBox1.Size = New Size(453, 948)
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
        lstIndex.Location = New Point(13, 34)
        lstIndex.Margin = New Padding(8, 6, 8, 6)
        lstIndex.Name = "lstIndex"
        lstIndex.Size = New Size(427, 898)
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
        DarkGroupBox2.Location = New Point(468, 6)
        DarkGroupBox2.Margin = New Padding(8, 6, 8, 6)
        DarkGroupBox2.Name = "DarkGroupBox2"
        DarkGroupBox2.Padding = New Padding(8, 6, 8, 6)
        DarkGroupBox2.Size = New Size(888, 1163)
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
        DarkGroupBox6.Location = New Point(13, 960)
        DarkGroupBox6.Margin = New Padding(8, 6, 8, 6)
        DarkGroupBox6.Name = "DarkGroupBox6"
        DarkGroupBox6.Padding = New Padding(8, 6, 8, 6)
        DarkGroupBox6.Size = New Size(862, 188)
        DarkGroupBox6.TabIndex = 10
        DarkGroupBox6.TabStop = False
        DarkGroupBox6.Text = "Start Skills"
        ' 
        ' cmbSkill4
        ' 
        cmbSkill4.DrawMode = DrawMode.OwnerDrawFixed
        cmbSkill4.FormattingEnabled = True
        cmbSkill4.Location = New Point(538, 113)
        cmbSkill4.Margin = New Padding(8, 6, 8, 6)
        cmbSkill4.Name = "cmbSkill4"
        cmbSkill4.Size = New Size(294, 40)
        cmbSkill4.TabIndex = 7
        ' 
        ' DarkLabel19
        ' 
        DarkLabel19.AutoSize = True
        DarkLabel19.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel19.Location = New Point(444, 122)
        DarkLabel19.Margin = New Padding(8, 0, 8, 0)
        DarkLabel19.Name = "DarkLabel19"
        DarkLabel19.Size = New Size(82, 32)
        DarkLabel19.TabIndex = 6
        DarkLabel19.Text = "Skill 4:"
        ' 
        ' cmbSkill3
        ' 
        cmbSkill3.DrawMode = DrawMode.OwnerDrawFixed
        cmbSkill3.FormattingEnabled = True
        cmbSkill3.Location = New Point(104, 113)
        cmbSkill3.Margin = New Padding(8, 6, 8, 6)
        cmbSkill3.Name = "cmbSkill3"
        cmbSkill3.Size = New Size(294, 40)
        cmbSkill3.TabIndex = 5
        ' 
        ' DarkLabel18
        ' 
        DarkLabel18.AutoSize = True
        DarkLabel18.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel18.Location = New Point(9, 122)
        DarkLabel18.Margin = New Padding(8, 0, 8, 0)
        DarkLabel18.Name = "DarkLabel18"
        DarkLabel18.Size = New Size(82, 32)
        DarkLabel18.TabIndex = 4
        DarkLabel18.Text = "Skill 3:"
        ' 
        ' cmbSkill2
        ' 
        cmbSkill2.DrawMode = DrawMode.OwnerDrawFixed
        cmbSkill2.FormattingEnabled = True
        cmbSkill2.Location = New Point(538, 47)
        cmbSkill2.Margin = New Padding(8, 6, 8, 6)
        cmbSkill2.Name = "cmbSkill2"
        cmbSkill2.Size = New Size(294, 40)
        cmbSkill2.TabIndex = 3
        ' 
        ' DarkLabel17
        ' 
        DarkLabel17.AutoSize = True
        DarkLabel17.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel17.Location = New Point(444, 53)
        DarkLabel17.Margin = New Padding(8, 0, 8, 0)
        DarkLabel17.Name = "DarkLabel17"
        DarkLabel17.Size = New Size(82, 32)
        DarkLabel17.TabIndex = 2
        DarkLabel17.Text = "Skill 2:"
        ' 
        ' cmbSkill1
        ' 
        cmbSkill1.DrawMode = DrawMode.OwnerDrawFixed
        cmbSkill1.FormattingEnabled = True
        cmbSkill1.Location = New Point(104, 47)
        cmbSkill1.Margin = New Padding(8, 6, 8, 6)
        cmbSkill1.Name = "cmbSkill1"
        cmbSkill1.Size = New Size(294, 40)
        cmbSkill1.TabIndex = 1
        ' 
        ' DarkLabel16
        ' 
        DarkLabel16.AutoSize = True
        DarkLabel16.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel16.Location = New Point(9, 53)
        DarkLabel16.Margin = New Padding(8, 0, 8, 0)
        DarkLabel16.Name = "DarkLabel16"
        DarkLabel16.Size = New Size(82, 32)
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
        DarkGroupBox4.Location = New Point(13, 538)
        DarkGroupBox4.Margin = New Padding(8, 6, 8, 6)
        DarkGroupBox4.Name = "DarkGroupBox4"
        DarkGroupBox4.Padding = New Padding(8, 6, 8, 6)
        DarkGroupBox4.Size = New Size(862, 410)
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
        pnlPetlevel.Location = New Point(13, 102)
        pnlPetlevel.Margin = New Padding(8, 6, 8, 6)
        pnlPetlevel.Name = "pnlPetlevel"
        pnlPetlevel.Size = New Size(836, 290)
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
        DarkGroupBox5.Location = New Point(13, 94)
        DarkGroupBox5.Margin = New Padding(8, 6, 8, 6)
        DarkGroupBox5.Name = "DarkGroupBox5"
        DarkGroupBox5.Padding = New Padding(8, 6, 8, 6)
        DarkGroupBox5.Size = New Size(808, 186)
        DarkGroupBox5.TabIndex = 7
        DarkGroupBox5.TabStop = False
        DarkGroupBox5.Text = "Evolution"
        ' 
        ' cmbEvolve
        ' 
        cmbEvolve.DrawMode = DrawMode.OwnerDrawFixed
        cmbEvolve.FormattingEnabled = True
        cmbEvolve.Location = New Point(217, 111)
        cmbEvolve.Margin = New Padding(8, 6, 8, 6)
        cmbEvolve.Name = "cmbEvolve"
        cmbEvolve.Size = New Size(574, 40)
        cmbEvolve.TabIndex = 4
        ' 
        ' DarkLabel15
        ' 
        DarkLabel15.AutoSize = True
        DarkLabel15.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel15.Location = New Point(13, 117)
        DarkLabel15.Margin = New Padding(8, 0, 8, 0)
        DarkLabel15.Name = "DarkLabel15"
        DarkLabel15.Size = New Size(147, 32)
        DarkLabel15.TabIndex = 3
        DarkLabel15.Text = "Evolves into:"
        ' 
        ' nudEvolveLvl
        ' 
        nudEvolveLvl.Location = New Point(661, 52)
        nudEvolveLvl.Margin = New Padding(8, 6, 8, 6)
        nudEvolveLvl.Name = "nudEvolveLvl"
        nudEvolveLvl.Size = New Size(134, 39)
        nudEvolveLvl.TabIndex = 2
        ' 
        ' DarkLabel14
        ' 
        DarkLabel14.AutoSize = True
        DarkLabel14.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel14.Location = New Point(447, 58)
        DarkLabel14.Margin = New Padding(8, 0, 8, 0)
        DarkLabel14.Name = "DarkLabel14"
        DarkLabel14.Size = New Size(195, 32)
        DarkLabel14.TabIndex = 1
        DarkLabel14.Text = "Evolves on Level:"
        ' 
        ' chkEvolve
        ' 
        chkEvolve.AutoSize = True
        chkEvolve.Location = New Point(13, 47)
        chkEvolve.Margin = New Padding(8, 6, 8, 6)
        chkEvolve.Name = "chkEvolve"
        chkEvolve.Size = New Size(203, 36)
        chkEvolve.TabIndex = 0
        chkEvolve.Text = "Pet Can Evolve"
        ' 
        ' nudMaxLevel
        ' 
        nudMaxLevel.Location = New Point(704, 30)
        nudMaxLevel.Margin = New Padding(8, 6, 8, 6)
        nudMaxLevel.Name = "nudMaxLevel"
        nudMaxLevel.Size = New Size(102, 39)
        nudMaxLevel.TabIndex = 6
        nudMaxLevel.Value = New Decimal(New Integer() {100, 0, 0, 0})
        ' 
        ' DarkLabel12
        ' 
        DarkLabel12.AutoSize = True
        DarkLabel12.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel12.Location = New Point(563, 34)
        DarkLabel12.Margin = New Padding(8, 0, 8, 0)
        DarkLabel12.Name = "DarkLabel12"
        DarkLabel12.Size = New Size(126, 32)
        DarkLabel12.TabIndex = 5
        DarkLabel12.Text = "Max Level:"
        ' 
        ' nudPetPnts
        ' 
        nudPetPnts.Location = New Point(470, 30)
        nudPetPnts.Margin = New Padding(8, 6, 8, 6)
        nudPetPnts.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        nudPetPnts.Name = "nudPetPnts"
        nudPetPnts.Size = New Size(78, 39)
        nudPetPnts.TabIndex = 4
        nudPetPnts.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' DarkLabel13
        ' 
        DarkLabel13.AutoSize = True
        DarkLabel13.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel13.Location = New Point(275, 34)
        DarkLabel13.Margin = New Padding(8, 0, 8, 0)
        DarkLabel13.Name = "DarkLabel13"
        DarkLabel13.Size = New Size(185, 32)
        DarkLabel13.TabIndex = 3
        DarkLabel13.Text = "Points Per Level:"
        ' 
        ' nudPetExp
        ' 
        nudPetExp.Location = New Point(160, 30)
        nudPetExp.Margin = New Padding(8, 6, 8, 6)
        nudPetExp.Name = "nudPetExp"
        nudPetExp.Size = New Size(102, 39)
        nudPetExp.TabIndex = 1
        nudPetExp.Value = New Decimal(New Integer() {100, 0, 0, 0})
        ' 
        ' DarkLabel11
        ' 
        DarkLabel11.AutoSize = True
        DarkLabel11.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel11.Location = New Point(9, 34)
        DarkLabel11.Margin = New Padding(8, 0, 8, 0)
        DarkLabel11.Name = "DarkLabel11"
        DarkLabel11.Size = New Size(138, 32)
        DarkLabel11.TabIndex = 0
        DarkLabel11.Text = "Exp Gain %:"
        ' 
        ' optDoNotLevel
        ' 
        optDoNotLevel.AutoSize = True
        optDoNotLevel.Location = New Point(572, 47)
        optDoNotLevel.Margin = New Padding(8, 6, 8, 6)
        optDoNotLevel.Name = "optDoNotLevel"
        optDoNotLevel.Size = New Size(238, 36)
        optDoNotLevel.TabIndex = 1
        optDoNotLevel.Text = "Does Not LevelUp"
        ' 
        ' optLevel
        ' 
        optLevel.AutoSize = True
        optLevel.Location = New Point(13, 47)
        optLevel.Margin = New Padding(8, 6, 8, 6)
        optLevel.Name = "optLevel"
        optLevel.Size = New Size(255, 36)
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
        DarkGroupBox3.Location = New Point(13, 190)
        DarkGroupBox3.Margin = New Padding(8, 6, 8, 6)
        DarkGroupBox3.Name = "DarkGroupBox3"
        DarkGroupBox3.Padding = New Padding(8, 6, 8, 6)
        DarkGroupBox3.Size = New Size(858, 332)
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
        pnlCustomStats.Location = New Point(13, 102)
        pnlCustomStats.Margin = New Padding(8, 6, 8, 6)
        pnlCustomStats.Name = "pnlCustomStats"
        pnlCustomStats.Size = New Size(832, 218)
        pnlCustomStats.TabIndex = 2
        ' 
        ' nudLevel
        ' 
        nudLevel.Location = New Point(128, 148)
        nudLevel.Margin = New Padding(8, 6, 8, 6)
        nudLevel.Name = "nudLevel"
        nudLevel.Size = New Size(117, 39)
        nudLevel.TabIndex = 13
        ' 
        ' DarkLabel10
        ' 
        DarkLabel10.AutoSize = True
        DarkLabel10.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel10.Location = New Point(9, 154)
        DarkLabel10.Margin = New Padding(8, 0, 8, 0)
        DarkLabel10.Name = "DarkLabel10"
        DarkLabel10.Size = New Size(74, 32)
        DarkLabel10.TabIndex = 12
        DarkLabel10.Text = "Level:"
        ' 
        ' nudSpirit
        ' 
        nudSpirit.Location = New Point(704, 84)
        nudSpirit.Margin = New Padding(8, 6, 8, 6)
        nudSpirit.Name = "nudSpirit"
        nudSpirit.Size = New Size(117, 39)
        nudSpirit.TabIndex = 11
        ' 
        ' DarkLabel7
        ' 
        DarkLabel7.AutoSize = True
        DarkLabel7.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel7.Location = New Point(585, 90)
        DarkLabel7.Margin = New Padding(8, 0, 8, 0)
        DarkLabel7.Name = "DarkLabel7"
        DarkLabel7.Size = New Size(74, 32)
        DarkLabel7.TabIndex = 10
        DarkLabel7.Text = "Spirit:"
        ' 
        ' nudIntelligence
        ' 
        nudIntelligence.Location = New Point(431, 84)
        nudIntelligence.Margin = New Padding(8, 6, 8, 6)
        nudIntelligence.Name = "nudIntelligence"
        nudIntelligence.Size = New Size(117, 39)
        nudIntelligence.TabIndex = 9
        ' 
        ' DarkLabel8
        ' 
        DarkLabel8.AutoSize = True
        DarkLabel8.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel8.Location = New Point(286, 90)
        DarkLabel8.Margin = New Padding(8, 0, 8, 0)
        DarkLabel8.Name = "DarkLabel8"
        DarkLabel8.Size = New Size(143, 32)
        DarkLabel8.TabIndex = 8
        DarkLabel8.Text = "Intelligence:"
        ' 
        ' nudLuck
        ' 
        nudLuck.Location = New Point(128, 84)
        nudLuck.Margin = New Padding(8, 6, 8, 6)
        nudLuck.Name = "nudLuck"
        nudLuck.Size = New Size(117, 39)
        nudLuck.TabIndex = 7
        ' 
        ' DarkLabel9
        ' 
        DarkLabel9.AutoSize = True
        DarkLabel9.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel9.Location = New Point(9, 90)
        DarkLabel9.Margin = New Padding(8, 0, 8, 0)
        DarkLabel9.Name = "DarkLabel9"
        DarkLabel9.Size = New Size(67, 32)
        DarkLabel9.TabIndex = 6
        DarkLabel9.Text = "Luck:"
        ' 
        ' nudVitality
        ' 
        nudVitality.Location = New Point(704, 20)
        nudVitality.Margin = New Padding(8, 6, 8, 6)
        nudVitality.Name = "nudVitality"
        nudVitality.Size = New Size(117, 39)
        nudVitality.TabIndex = 5
        ' 
        ' DarkLabel6
        ' 
        DarkLabel6.AutoSize = True
        DarkLabel6.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel6.Location = New Point(583, 26)
        DarkLabel6.Margin = New Padding(8, 0, 8, 0)
        DarkLabel6.Name = "DarkLabel6"
        DarkLabel6.Size = New Size(92, 32)
        DarkLabel6.TabIndex = 4
        DarkLabel6.Text = "Vitality:"
        ' 
        ' nudEndurance
        ' 
        nudEndurance.Location = New Point(431, 20)
        nudEndurance.Margin = New Padding(8, 6, 8, 6)
        nudEndurance.Name = "nudEndurance"
        nudEndurance.Size = New Size(117, 39)
        nudEndurance.TabIndex = 3
        ' 
        ' DarkLabel5
        ' 
        DarkLabel5.AutoSize = True
        DarkLabel5.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel5.Location = New Point(284, 26)
        DarkLabel5.Margin = New Padding(8, 0, 8, 0)
        DarkLabel5.Name = "DarkLabel5"
        DarkLabel5.Size = New Size(131, 32)
        DarkLabel5.TabIndex = 2
        DarkLabel5.Text = "Endurance:"
        ' 
        ' nudStrength
        ' 
        nudStrength.Location = New Point(128, 20)
        nudStrength.Margin = New Padding(8, 6, 8, 6)
        nudStrength.Name = "nudStrength"
        nudStrength.Size = New Size(117, 39)
        nudStrength.TabIndex = 1
        ' 
        ' DarkLabel4
        ' 
        DarkLabel4.AutoSize = True
        DarkLabel4.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel4.Location = New Point(8, 26)
        DarkLabel4.Margin = New Padding(8, 0, 8, 0)
        DarkLabel4.Name = "DarkLabel4"
        DarkLabel4.Size = New Size(110, 32)
        DarkLabel4.TabIndex = 0
        DarkLabel4.Text = "Strength:"
        ' 
        ' optAdoptStats
        ' 
        optAdoptStats.AutoSize = True
        optAdoptStats.Location = New Point(583, 47)
        optAdoptStats.Margin = New Padding(8, 6, 8, 6)
        optAdoptStats.Name = "optAdoptStats"
        optAdoptStats.Size = New Size(260, 36)
        optAdoptStats.TabIndex = 1
        optAdoptStats.TabStop = True
        optAdoptStats.Text = "Adopt Owner's Stats"
        ' 
        ' optCustomStats
        ' 
        optCustomStats.AutoSize = True
        optCustomStats.Location = New Point(13, 47)
        optCustomStats.Margin = New Padding(8, 6, 8, 6)
        optCustomStats.Name = "optCustomStats"
        optCustomStats.Size = New Size(184, 36)
        optCustomStats.TabIndex = 0
        optCustomStats.TabStop = True
        optCustomStats.Text = "Custom Stats"
        ' 
        ' DarkLabel3
        ' 
        DarkLabel3.AutoSize = True
        DarkLabel3.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel3.Location = New Point(13, 117)
        DarkLabel3.Margin = New Padding(8, 0, 8, 0)
        DarkLabel3.Name = "DarkLabel3"
        DarkLabel3.Size = New Size(86, 32)
        DarkLabel3.TabIndex = 7
        DarkLabel3.Text = "Range:"
        ' 
        ' nudRange
        ' 
        nudRange.Location = New Point(148, 113)
        nudRange.Margin = New Padding(8, 6, 8, 6)
        nudRange.Name = "nudRange"
        nudRange.Size = New Size(165, 39)
        nudRange.TabIndex = 6
        ' 
        ' DarkLabel2
        ' 
        DarkLabel2.AutoSize = True
        DarkLabel2.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel2.Location = New Point(486, 117)
        DarkLabel2.Margin = New Padding(8, 0, 8, 0)
        DarkLabel2.Name = "DarkLabel2"
        DarkLabel2.Size = New Size(81, 32)
        DarkLabel2.TabIndex = 5
        DarkLabel2.Text = "Sprite:"
        ' 
        ' nudSprite
        ' 
        nudSprite.Location = New Point(596, 113)
        nudSprite.Margin = New Padding(8, 6, 8, 6)
        nudSprite.Name = "nudSprite"
        nudSprite.Size = New Size(165, 39)
        nudSprite.TabIndex = 4
        ' 
        ' picSprite
        ' 
        picSprite.BackColor = Color.Black
        picSprite.Location = New Point(767, 58)
        picSprite.Margin = New Padding(8, 6, 8, 6)
        picSprite.Name = "picSprite"
        picSprite.Size = New Size(60, 68)
        picSprite.TabIndex = 3
        picSprite.TabStop = False
        ' 
        ' txtName
        ' 
        txtName.BackColor = Color.FromArgb(CByte(69), CByte(73), CByte(74))
        txtName.BorderStyle = BorderStyle.FixedSingle
        txtName.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        txtName.Location = New Point(148, 44)
        txtName.Margin = New Padding(8, 6, 8, 6)
        txtName.Name = "txtName"
        txtName.Size = New Size(602, 39)
        txtName.TabIndex = 1
        ' 
        ' DarkLabel1
        ' 
        DarkLabel1.AutoSize = True
        DarkLabel1.ForeColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        DarkLabel1.Location = New Point(13, 49)
        DarkLabel1.Margin = New Padding(8, 0, 8, 0)
        DarkLabel1.Name = "DarkLabel1"
        DarkLabel1.Size = New Size(83, 32)
        DarkLabel1.TabIndex = 0
        DarkLabel1.Text = "Name:"
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(17, 971)
        btnSave.Margin = New Padding(8, 6, 8, 6)
        btnSave.Name = "btnSave"
        btnSave.Padding = New Padding(11, 12, 11, 12)
        btnSave.Size = New Size(429, 58)
        btnSave.TabIndex = 2
        btnSave.Text = "Save"
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(17, 1111)
        btnCancel.Margin = New Padding(8, 6, 8, 6)
        btnCancel.Name = "btnCancel"
        btnCancel.Padding = New Padding(11, 12, 11, 12)
        btnCancel.Size = New Size(429, 58)
        btnCancel.TabIndex = 3
        btnCancel.Text = "Cancel"
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(17, 1041)
        btnDelete.Margin = New Padding(8, 6, 8, 6)
        btnDelete.Name = "btnDelete"
        btnDelete.Padding = New Padding(11, 12, 11, 12)
        btnDelete.Size = New Size(429, 58)
        btnDelete.TabIndex = 4
        btnDelete.Text = "Delete"
        ' 
        ' frmEditor_Pet
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(48))
        ClientSize = New Size(1368, 1181)
        Controls.Add(btnDelete)
        Controls.Add(btnCancel)
        Controls.Add(btnSave)
        Controls.Add(DarkGroupBox2)
        Controls.Add(DarkGroupBox1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(8, 6, 8, 6)
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
