<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMenu
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(FrmMenu))
        pnlLogin = New Panel()
        btnLogin = New PictureBox()
        chkRememberPassword = New CheckBox()
        txtPassword = New TextBox()
        lblLoginPass = New Label()
        txtLogin = New TextBox()
        lblLoginName = New Label()
        lblLogin = New Label()
        pnlRegister = New Panel()
        txtRPass2 = New TextBox()
        btnCreateAccount = New PictureBox()
        lblNewAccPass2 = New Label()
        txtRPass = New TextBox()
        lblNewAccPass = New Label()
        txtRuser = New TextBox()
        lblNewAccName = New Label()
        lblNewAccount = New Label()
        pnlCredits = New Panel()
        lblCreditsTop = New Label()
        lblScrollingCredits = New Label()
        tmrCredits = New Timer(components)
        pnlNewChar = New Panel()
        btnCreateCharacter = New PictureBox()
        txtDescription = New TextBox()
        lblNewCharSprite = New Label()
        placeholderforsprite = New PictureBox()
        rdoFemale = New RadioButton()
        rdoMale = New RadioButton()
        cmbJob = New ComboBox()
        lblNewCharGender = New Label()
        lblNewCharJob = New Label()
        txtCharName = New TextBox()
        lblNewCharName = New Label()
        lblNewChar = New Label()
        lblStatusHeader = New Label()
        lblServerStatus = New Label()
        pnlMainMenu = New Panel()
        lblNewsHeader = New Label()
        lblNews = New Label()
        picLogo = New PictureBox()
        pnlCharSelect = New Panel()
        btnDelChar = New PictureBox()
        btnUseChar = New PictureBox()
        btnNewChar = New PictureBox()
        picChar3 = New PictureBox()
        picChar2 = New PictureBox()
        picChar1 = New PictureBox()
        lblCharSelect = New Label()
        Label16 = New Label()
        btnPlay = New PictureBox()
        btnRegister = New PictureBox()
        btnExit = New PictureBox()
        PictureBox1 = New PictureBox()
        pnlLogin.SuspendLayout()
        CType(btnLogin, ComponentModel.ISupportInitialize).BeginInit()
        pnlRegister.SuspendLayout()
        CType(btnCreateAccount, ComponentModel.ISupportInitialize).BeginInit()
        pnlCredits.SuspendLayout()
        pnlNewChar.SuspendLayout()
        CType(btnCreateCharacter, ComponentModel.ISupportInitialize).BeginInit()
        CType(placeholderforsprite, ComponentModel.ISupportInitialize).BeginInit()
        pnlMainMenu.SuspendLayout()
        CType(picLogo, ComponentModel.ISupportInitialize).BeginInit()
        pnlCharSelect.SuspendLayout()
        CType(btnDelChar, ComponentModel.ISupportInitialize).BeginInit()
        CType(btnUseChar, ComponentModel.ISupportInitialize).BeginInit()
        CType(btnNewChar, ComponentModel.ISupportInitialize).BeginInit()
        CType(picChar3, ComponentModel.ISupportInitialize).BeginInit()
        CType(picChar2, ComponentModel.ISupportInitialize).BeginInit()
        CType(picChar1, ComponentModel.ISupportInitialize).BeginInit()
        CType(btnPlay, ComponentModel.ISupportInitialize).BeginInit()
        CType(btnRegister, ComponentModel.ISupportInitialize).BeginInit()
        CType(btnExit, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pnlLogin
        ' 
        pnlLogin.BackColor = Color.Transparent
        pnlLogin.BackgroundImage = CType(resources.GetObject("pnlLogin.BackgroundImage"), Image)
        pnlLogin.BackgroundImageLayout = ImageLayout.Stretch
        pnlLogin.Controls.Add(btnLogin)
        pnlLogin.Controls.Add(chkRememberPassword)
        pnlLogin.Controls.Add(txtPassword)
        pnlLogin.Controls.Add(lblLoginPass)
        pnlLogin.Controls.Add(txtLogin)
        pnlLogin.Controls.Add(lblLoginName)
        pnlLogin.Controls.Add(lblLogin)
        pnlLogin.ForeColor = Color.White
        pnlLogin.Location = New Point(191, 180)
        pnlLogin.Name = "pnlLogin"
        pnlLogin.Size = New Size(400, 192)
        pnlLogin.TabIndex = 37
        pnlLogin.Visible = False
        ' 
        ' btnLogin
        ' 
        btnLogin.Image = CType(resources.GetObject("btnLogin.Image"), Image)
        btnLogin.Location = New Point(263, 144)
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(130, 40)
        btnLogin.TabIndex = 26
        btnLogin.TabStop = False
        ' 
        ' chkRememberPassword
        ' 
        chkRememberPassword.AutoSize = True
        chkRememberPassword.BackColor = Color.Transparent
        chkRememberPassword.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        chkRememberPassword.Location = New Point(110, 128)
        chkRememberPassword.Name = "chkRememberPassword"
        chkRememberPassword.Size = New Size(236, 40)
        chkRememberPassword.TabIndex = 25
        chkRememberPassword.Text = "Save Password?"
        chkRememberPassword.UseVisualStyleBackColor = False
        ' 
        ' txtPassword
        ' 
        txtPassword.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        txtPassword.Location = New Point(180, 98)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "*"c
        txtPassword.Size = New Size(110, 42)
        txtPassword.TabIndex = 24
        ' 
        ' lblLoginPass
        ' 
        lblLoginPass.AutoSize = True
        lblLoginPass.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblLoginPass.Location = New Point(107, 101)
        lblLoginPass.Name = "lblLoginPass"
        lblLoginPass.Size = New Size(136, 36)
        lblLoginPass.TabIndex = 23
        lblLoginPass.Text = "Password:"
        ' 
        ' txtLogin
        ' 
        txtLogin.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        txtLogin.Location = New Point(180, 63)
        txtLogin.Name = "txtLogin"
        txtLogin.Size = New Size(110, 42)
        txtLogin.TabIndex = 17
        ' 
        ' lblLoginName
        ' 
        lblLoginName.AutoSize = True
        lblLoginName.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblLoginName.Location = New Point(107, 66)
        lblLoginName.Name = "lblLoginName"
        lblLoginName.Size = New Size(95, 36)
        lblLoginName.TabIndex = 16
        lblLoginName.Text = "Name:"
        ' 
        ' lblLogin
        ' 
        lblLogin.AutoSize = True
        lblLogin.Font = New Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point)
        lblLogin.Location = New Point(156, 9)
        lblLogin.Name = "lblLogin"
        lblLogin.Size = New Size(167, 72)
        lblLogin.TabIndex = 15
        lblLogin.Text = "Login"
        ' 
        ' pnlRegister
        ' 
        pnlRegister.BackColor = Color.Transparent
        pnlRegister.BackgroundImage = CType(resources.GetObject("pnlRegister.BackgroundImage"), Image)
        pnlRegister.BackgroundImageLayout = ImageLayout.Stretch
        pnlRegister.Controls.Add(txtRPass2)
        pnlRegister.Controls.Add(btnCreateAccount)
        pnlRegister.Controls.Add(lblNewAccPass2)
        pnlRegister.Controls.Add(txtRPass)
        pnlRegister.Controls.Add(lblNewAccPass)
        pnlRegister.Controls.Add(txtRuser)
        pnlRegister.Controls.Add(lblNewAccName)
        pnlRegister.Controls.Add(lblNewAccount)
        pnlRegister.ForeColor = Color.White
        pnlRegister.Location = New Point(191, 180)
        pnlRegister.Name = "pnlRegister"
        pnlRegister.Size = New Size(400, 192)
        pnlRegister.TabIndex = 38
        pnlRegister.Visible = False
        ' 
        ' txtRPass2
        ' 
        txtRPass2.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        txtRPass2.Location = New Point(189, 125)
        txtRPass2.Name = "txtRPass2"
        txtRPass2.PasswordChar = "*"c
        txtRPass2.Size = New Size(110, 42)
        txtRPass2.TabIndex = 21
        ' 
        ' btnCreateAccount
        ' 
        btnCreateAccount.Image = CType(resources.GetObject("btnCreateAccount.Image"), Image)
        btnCreateAccount.Location = New Point(263, 144)
        btnCreateAccount.Name = "btnCreateAccount"
        btnCreateAccount.Size = New Size(130, 40)
        btnCreateAccount.TabIndex = 22
        btnCreateAccount.TabStop = False
        ' 
        ' lblNewAccPass2
        ' 
        lblNewAccPass2.AutoSize = True
        lblNewAccPass2.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblNewAccPass2.Location = New Point(61, 128)
        lblNewAccPass2.Name = "lblNewAccPass2"
        lblNewAccPass2.Size = New Size(227, 36)
        lblNewAccPass2.TabIndex = 20
        lblNewAccPass2.Text = "Retype Password:"
        ' 
        ' txtRPass
        ' 
        txtRPass.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        txtRPass.Location = New Point(189, 93)
        txtRPass.Name = "txtRPass"
        txtRPass.PasswordChar = "*"c
        txtRPass.Size = New Size(110, 42)
        txtRPass.TabIndex = 19
        ' 
        ' lblNewAccPass
        ' 
        lblNewAccPass.AutoSize = True
        lblNewAccPass.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblNewAccPass.Location = New Point(107, 96)
        lblNewAccPass.Name = "lblNewAccPass"
        lblNewAccPass.Size = New Size(136, 36)
        lblNewAccPass.TabIndex = 18
        lblNewAccPass.Text = "Password:"
        ' 
        ' txtRuser
        ' 
        txtRuser.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        txtRuser.Location = New Point(189, 59)
        txtRuser.Name = "txtRuser"
        txtRuser.Size = New Size(110, 42)
        txtRuser.TabIndex = 17
        ' 
        ' lblNewAccName
        ' 
        lblNewAccName.AutoSize = True
        lblNewAccName.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblNewAccName.Location = New Point(107, 61)
        lblNewAccName.Name = "lblNewAccName"
        lblNewAccName.Size = New Size(144, 36)
        lblNewAccName.TabIndex = 16
        lblNewAccName.Text = "Username:"
        ' 
        ' lblNewAccount
        ' 
        lblNewAccount.AutoSize = True
        lblNewAccount.Font = New Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblNewAccount.Location = New Point(122, 12)
        lblNewAccount.Name = "lblNewAccount"
        lblNewAccount.Size = New Size(385, 77)
        lblNewAccount.TabIndex = 15
        lblNewAccount.Text = "New Account"
        ' 
        ' pnlCredits
        ' 
        pnlCredits.BackColor = Color.Transparent
        pnlCredits.BackgroundImage = CType(resources.GetObject("pnlCredits.BackgroundImage"), Image)
        pnlCredits.BackgroundImageLayout = ImageLayout.Stretch
        pnlCredits.Controls.Add(lblCreditsTop)
        pnlCredits.Controls.Add(lblScrollingCredits)
        pnlCredits.ForeColor = Color.White
        pnlCredits.Location = New Point(191, 180)
        pnlCredits.Name = "pnlCredits"
        pnlCredits.Size = New Size(400, 192)
        pnlCredits.TabIndex = 39
        pnlCredits.Visible = False
        ' 
        ' lblCreditsTop
        ' 
        lblCreditsTop.BackColor = Color.Transparent
        lblCreditsTop.Font = New Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblCreditsTop.Location = New Point(78, 6)
        lblCreditsTop.Name = "lblCreditsTop"
        lblCreditsTop.Size = New Size(247, 33)
        lblCreditsTop.TabIndex = 15
        lblCreditsTop.Text = "Credits"
        lblCreditsTop.TextAlign = ContentAlignment.TopCenter
        ' 
        ' lblScrollingCredits
        ' 
        lblScrollingCredits.AutoSize = True
        lblScrollingCredits.BackColor = Color.Transparent
        lblScrollingCredits.Location = New Point(70, 179)
        lblScrollingCredits.Name = "lblScrollingCredits"
        lblScrollingCredits.Size = New Size(0, 30)
        lblScrollingCredits.TabIndex = 17
        lblScrollingCredits.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' tmrCredits
        ' 
        tmrCredits.Enabled = True
        tmrCredits.Interval = 1000
        ' 
        ' pnlNewChar
        ' 
        pnlNewChar.BackColor = Color.Transparent
        pnlNewChar.BackgroundImage = CType(resources.GetObject("pnlNewChar.BackgroundImage"), Image)
        pnlNewChar.BackgroundImageLayout = ImageLayout.Stretch
        pnlNewChar.Controls.Add(btnCreateCharacter)
        pnlNewChar.Controls.Add(txtDescription)
        pnlNewChar.Controls.Add(lblNewCharSprite)
        pnlNewChar.Controls.Add(placeholderforsprite)
        pnlNewChar.Controls.Add(rdoFemale)
        pnlNewChar.Controls.Add(rdoMale)
        pnlNewChar.Controls.Add(cmbJob)
        pnlNewChar.Controls.Add(lblNewCharGender)
        pnlNewChar.Controls.Add(lblNewCharJob)
        pnlNewChar.Controls.Add(txtCharName)
        pnlNewChar.Controls.Add(lblNewCharName)
        pnlNewChar.Controls.Add(lblNewChar)
        pnlNewChar.ForeColor = Color.White
        pnlNewChar.Location = New Point(191, 180)
        pnlNewChar.Name = "pnlNewChar"
        pnlNewChar.Size = New Size(400, 192)
        pnlNewChar.TabIndex = 43
        pnlNewChar.Visible = False
        ' 
        ' btnCreateCharacter
        ' 
        btnCreateCharacter.Image = CType(resources.GetObject("btnCreateCharacter.Image"), Image)
        btnCreateCharacter.Location = New Point(263, 144)
        btnCreateCharacter.Name = "btnCreateCharacter"
        btnCreateCharacter.Size = New Size(130, 40)
        btnCreateCharacter.TabIndex = 45
        btnCreateCharacter.TabStop = False
        ' 
        ' txtDescription
        ' 
        txtDescription.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtDescription.Location = New Point(227, 76)
        txtDescription.Multiline = True
        txtDescription.Name = "txtDescription"
        txtDescription.Size = New Size(157, 62)
        txtDescription.TabIndex = 44
        ' 
        ' lblNewCharSprite
        ' 
        lblNewCharSprite.AutoSize = True
        lblNewCharSprite.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        lblNewCharSprite.Location = New Point(49, 71)
        lblNewCharSprite.Name = "lblNewCharSprite"
        lblNewCharSprite.Size = New Size(82, 36)
        lblNewCharSprite.TabIndex = 43
        lblNewCharSprite.Text = "Sprite"
        ' 
        ' placeholderforsprite
        ' 
        placeholderforsprite.Location = New Point(50, 91)
        placeholderforsprite.Name = "placeholderforsprite"
        placeholderforsprite.Size = New Size(48, 60)
        placeholderforsprite.TabIndex = 41
        placeholderforsprite.TabStop = False
        placeholderforsprite.Visible = False
        ' 
        ' rdoFemale
        ' 
        rdoFemale.AutoSize = True
        rdoFemale.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        rdoFemale.Location = New Point(135, 118)
        rdoFemale.Name = "rdoFemale"
        rdoFemale.Size = New Size(128, 40)
        rdoFemale.TabIndex = 38
        rdoFemale.TabStop = True
        rdoFemale.Text = "Female"
        rdoFemale.UseVisualStyleBackColor = True
        ' 
        ' rdoMale
        ' 
        rdoMale.AutoSize = True
        rdoMale.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        rdoMale.Location = New Point(135, 93)
        rdoMale.Name = "rdoMale"
        rdoMale.Size = New Size(102, 40)
        rdoMale.TabIndex = 37
        rdoMale.TabStop = True
        rdoMale.Text = "Male"
        rdoMale.UseVisualStyleBackColor = True
        ' 
        ' cmbJob
        ' 
        cmbJob.DropDownStyle = ComboBoxStyle.DropDownList
        cmbJob.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        cmbJob.FormattingEnabled = True
        cmbJob.Location = New Point(227, 43)
        cmbJob.Name = "cmbJob"
        cmbJob.Size = New Size(157, 44)
        cmbJob.TabIndex = 36
        ' 
        ' lblNewCharGender
        ' 
        lblNewCharGender.AutoSize = True
        lblNewCharGender.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        lblNewCharGender.Location = New Point(133, 75)
        lblNewCharGender.Name = "lblNewCharGender"
        lblNewCharGender.Size = New Size(106, 36)
        lblNewCharGender.TabIndex = 34
        lblNewCharGender.Text = "Gender:"
        ' 
        ' lblNewCharJob
        ' 
        lblNewCharJob.AutoSize = True
        lblNewCharJob.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        lblNewCharJob.Location = New Point(189, 45)
        lblNewCharJob.Name = "lblNewCharJob"
        lblNewCharJob.Size = New Size(60, 36)
        lblNewCharJob.TabIndex = 33
        lblNewCharJob.Text = "Job:"
        ' 
        ' txtCharName
        ' 
        txtCharName.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtCharName.Location = New Point(59, 42)
        txtCharName.Name = "txtCharName"
        txtCharName.Size = New Size(121, 42)
        txtCharName.TabIndex = 32
        ' 
        ' lblNewCharName
        ' 
        lblNewCharName.AutoSize = True
        lblNewCharName.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        lblNewCharName.ForeColor = Color.White
        lblNewCharName.Location = New Point(13, 45)
        lblNewCharName.Name = "lblNewCharName"
        lblNewCharName.Size = New Size(89, 36)
        lblNewCharName.TabIndex = 31
        lblNewCharName.Text = "Name:"
        ' 
        ' lblNewChar
        ' 
        lblNewChar.AutoSize = True
        lblNewChar.Font = New Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblNewChar.ForeColor = Color.White
        lblNewChar.Location = New Point(85, 1)
        lblNewChar.Name = "lblNewChar"
        lblNewChar.Size = New Size(469, 77)
        lblNewChar.TabIndex = 30
        lblNewChar.Text = "Create Character"
        ' 
        ' lblStatusHeader
        ' 
        lblStatusHeader.AutoSize = True
        lblStatusHeader.BackColor = Color.Transparent
        lblStatusHeader.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblStatusHeader.ForeColor = Color.White
        lblStatusHeader.Location = New Point(12, 570)
        lblStatusHeader.Name = "lblStatusHeader"
        lblStatusHeader.Size = New Size(225, 45)
        lblStatusHeader.TabIndex = 44
        lblStatusHeader.Text = "Server Status:"
        ' 
        ' lblServerStatus
        ' 
        lblServerStatus.AutoSize = True
        lblServerStatus.BackColor = Color.Transparent
        lblServerStatus.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblServerStatus.ForeColor = Color.Red
        lblServerStatus.Location = New Point(119, 570)
        lblServerStatus.Name = "lblServerStatus"
        lblServerStatus.Size = New Size(122, 45)
        lblServerStatus.TabIndex = 45
        lblServerStatus.Text = "Offline"
        ' 
        ' pnlMainMenu
        ' 
        pnlMainMenu.BackColor = Color.Transparent
        pnlMainMenu.BackgroundImage = CType(resources.GetObject("pnlMainMenu.BackgroundImage"), Image)
        pnlMainMenu.BackgroundImageLayout = ImageLayout.Stretch
        pnlMainMenu.Controls.Add(lblNewsHeader)
        pnlMainMenu.Controls.Add(lblNews)
        pnlMainMenu.ForeColor = Color.White
        pnlMainMenu.Location = New Point(191, 180)
        pnlMainMenu.Name = "pnlMainMenu"
        pnlMainMenu.Size = New Size(400, 192)
        pnlMainMenu.TabIndex = 46
        ' 
        ' lblNewsHeader
        ' 
        lblNewsHeader.AutoSize = True
        lblNewsHeader.BackColor = Color.Transparent
        lblNewsHeader.Font = New Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point)
        lblNewsHeader.Location = New Point(112, 16)
        lblNewsHeader.Name = "lblNewsHeader"
        lblNewsHeader.Size = New Size(341, 77)
        lblNewsHeader.TabIndex = 36
        lblNewsHeader.Text = "Latest News"
        ' 
        ' lblNews
        ' 
        lblNews.BackColor = Color.Transparent
        lblNews.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        lblNews.Location = New Point(17, 55)
        lblNews.Name = "lblNews"
        lblNews.RightToLeft = RightToLeft.Yes
        lblNews.Size = New Size(366, 121)
        lblNews.TabIndex = 37
        lblNews.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' picLogo
        ' 
        picLogo.BackColor = Color.Transparent
        picLogo.BackgroundImage = CType(resources.GetObject("picLogo.BackgroundImage"), Image)
        picLogo.BackgroundImageLayout = ImageLayout.Zoom
        picLogo.Location = New Point(110, 30)
        picLogo.Name = "picLogo"
        picLogo.Size = New Size(560, 144)
        picLogo.TabIndex = 52
        picLogo.TabStop = False
        ' 
        ' pnlCharSelect
        ' 
        pnlCharSelect.BackColor = Color.Transparent
        pnlCharSelect.BackgroundImage = CType(resources.GetObject("pnlCharSelect.BackgroundImage"), Image)
        pnlCharSelect.BackgroundImageLayout = ImageLayout.Stretch
        pnlCharSelect.Controls.Add(btnDelChar)
        pnlCharSelect.Controls.Add(btnUseChar)
        pnlCharSelect.Controls.Add(btnNewChar)
        pnlCharSelect.Controls.Add(picChar3)
        pnlCharSelect.Controls.Add(picChar2)
        pnlCharSelect.Controls.Add(picChar1)
        pnlCharSelect.Controls.Add(lblCharSelect)
        pnlCharSelect.Controls.Add(Label16)
        pnlCharSelect.ForeColor = Color.White
        pnlCharSelect.Location = New Point(191, 180)
        pnlCharSelect.Name = "pnlCharSelect"
        pnlCharSelect.Size = New Size(400, 192)
        pnlCharSelect.TabIndex = 57
        pnlCharSelect.Visible = False
        ' 
        ' btnDelChar
        ' 
        btnDelChar.BackgroundImageLayout = ImageLayout.None
        btnDelChar.Image = CType(resources.GetObject("btnDelChar.Image"), Image)
        btnDelChar.Location = New Point(263, 144)
        btnDelChar.Name = "btnDelChar"
        btnDelChar.Size = New Size(130, 40)
        btnDelChar.TabIndex = 55
        btnDelChar.TabStop = False
        ' 
        ' btnUseChar
        ' 
        btnUseChar.BackgroundImageLayout = ImageLayout.None
        btnUseChar.Image = CType(resources.GetObject("btnUseChar.Image"), Image)
        btnUseChar.Location = New Point(136, 144)
        btnUseChar.Name = "btnUseChar"
        btnUseChar.Size = New Size(130, 40)
        btnUseChar.TabIndex = 54
        btnUseChar.TabStop = False
        ' 
        ' btnNewChar
        ' 
        btnNewChar.BackgroundImageLayout = ImageLayout.None
        btnNewChar.Location = New Point(9, 144)
        btnNewChar.Name = "btnNewChar"
        btnNewChar.Size = New Size(130, 40)
        btnNewChar.TabIndex = 53
        btnNewChar.TabStop = False
        ' 
        ' picChar3
        ' 
        picChar3.BackColor = Color.Transparent
        picChar3.BackgroundImageLayout = ImageLayout.None
        picChar3.Location = New Point(300, 52)
        picChar3.Name = "picChar3"
        picChar3.Size = New Size(48, 60)
        picChar3.TabIndex = 44
        picChar3.TabStop = False
        ' 
        ' picChar2
        ' 
        picChar2.BackColor = Color.Transparent
        picChar2.BackgroundImageLayout = ImageLayout.None
        picChar2.Location = New Point(175, 52)
        picChar2.Name = "picChar2"
        picChar2.Size = New Size(48, 60)
        picChar2.TabIndex = 43
        picChar2.TabStop = False
        ' 
        ' picChar1
        ' 
        picChar1.BackColor = Color.Transparent
        picChar1.BackgroundImageLayout = ImageLayout.None
        picChar1.Location = New Point(52, 52)
        picChar1.Name = "picChar1"
        picChar1.Size = New Size(48, 60)
        picChar1.TabIndex = 42
        picChar1.TabStop = False
        ' 
        ' lblCharSelect
        ' 
        lblCharSelect.BackColor = Color.Transparent
        lblCharSelect.Font = New Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblCharSelect.Location = New Point(44, 12)
        lblCharSelect.Name = "lblCharSelect"
        lblCharSelect.Size = New Size(312, 33)
        lblCharSelect.TabIndex = 15
        lblCharSelect.Text = "Character Selection"
        lblCharSelect.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.BackColor = Color.Transparent
        Label16.Location = New Point(70, 179)
        Label16.Name = "Label16"
        Label16.Size = New Size(0, 30)
        Label16.TabIndex = 17
        Label16.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnPlay
        ' 
        btnPlay.BackColor = Color.Transparent
        btnPlay.BackgroundImage = CType(resources.GetObject("btnPlay.BackgroundImage"), Image)
        btnPlay.BackgroundImageLayout = ImageLayout.Center
        btnPlay.Location = New Point(290, 415)
        btnPlay.Name = "btnPlay"
        btnPlay.Size = New Size(204, 30)
        btnPlay.TabIndex = 59
        btnPlay.TabStop = False
        ' 
        ' btnRegister
        ' 
        btnRegister.BackColor = Color.Transparent
        btnRegister.BackgroundImage = CType(resources.GetObject("btnRegister.BackgroundImage"), Image)
        btnRegister.BackgroundImageLayout = ImageLayout.Center
        btnRegister.Location = New Point(290, 446)
        btnRegister.Name = "btnRegister"
        btnRegister.Size = New Size(204, 30)
        btnRegister.TabIndex = 60
        btnRegister.TabStop = False
        ' 
        ' btnExit
        ' 
        btnExit.BackColor = Color.Transparent
        btnExit.BackgroundImage = CType(resources.GetObject("btnExit.BackgroundImage"), Image)
        btnExit.BackgroundImageLayout = ImageLayout.Center
        btnExit.Location = New Point(290, 477)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(204, 30)
        btnExit.TabIndex = 61
        btnExit.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(279, 400)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(227, 121)
        PictureBox1.TabIndex = 62
        PictureBox1.TabStop = False
        ' 
        ' FrmMenu
        ' 
        AutoScaleDimensions = New SizeF(12F, 30F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(800, 600)
        Controls.Add(lblServerStatus)
        Controls.Add(lblStatusHeader)
        Controls.Add(picLogo)
        Controls.Add(btnPlay)
        Controls.Add(btnRegister)
        Controls.Add(btnExit)
        Controls.Add(PictureBox1)
        Controls.Add(pnlCharSelect)
        Controls.Add(pnlNewChar)
        Controls.Add(pnlCredits)
        Controls.Add(pnlRegister)
        Controls.Add(pnlLogin)
        Controls.Add(pnlMainMenu)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point)
        ForeColor = Color.Black
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        Name = "FrmMenu"
        StartPosition = FormStartPosition.CenterScreen
        Text = "frmMenu"
        pnlLogin.ResumeLayout(False)
        pnlLogin.PerformLayout()
        CType(btnLogin, ComponentModel.ISupportInitialize).EndInit()
        pnlRegister.ResumeLayout(False)
        pnlRegister.PerformLayout()
        CType(btnCreateAccount, ComponentModel.ISupportInitialize).EndInit()
        pnlCredits.ResumeLayout(False)
        pnlCredits.PerformLayout()
        pnlNewChar.ResumeLayout(False)
        pnlNewChar.PerformLayout()
        CType(btnCreateCharacter, ComponentModel.ISupportInitialize).EndInit()
        CType(placeholderforsprite, ComponentModel.ISupportInitialize).EndInit()
        pnlMainMenu.ResumeLayout(False)
        pnlMainMenu.PerformLayout()
        CType(picLogo, ComponentModel.ISupportInitialize).EndInit()
        pnlCharSelect.ResumeLayout(False)
        pnlCharSelect.PerformLayout()
        CType(btnDelChar, ComponentModel.ISupportInitialize).EndInit()
        CType(btnUseChar, ComponentModel.ISupportInitialize).EndInit()
        CType(btnNewChar, ComponentModel.ISupportInitialize).EndInit()
        CType(picChar3, ComponentModel.ISupportInitialize).EndInit()
        CType(picChar2, ComponentModel.ISupportInitialize).EndInit()
        CType(picChar1, ComponentModel.ISupportInitialize).EndInit()
        CType(btnPlay, ComponentModel.ISupportInitialize).EndInit()
        CType(btnRegister, ComponentModel.ISupportInitialize).EndInit()
        CType(btnExit, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents pnlLogin As System.Windows.Forms.Panel
    Friend WithEvents chkRememberPassword As System.Windows.Forms.CheckBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblLoginPass As System.Windows.Forms.Label
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents lblLoginName As System.Windows.Forms.Label
    Friend WithEvents lblLogin As System.Windows.Forms.Label
    Friend WithEvents pnlRegister As System.Windows.Forms.Panel
    Friend WithEvents txtRPass2 As System.Windows.Forms.TextBox
    Friend WithEvents lblNewAccPass2 As System.Windows.Forms.Label
    Friend WithEvents txtRPass As System.Windows.Forms.TextBox
    Friend WithEvents lblNewAccPass As System.Windows.Forms.Label
    Friend WithEvents txtRuser As System.Windows.Forms.TextBox
    Friend WithEvents lblNewAccName As System.Windows.Forms.Label
    Friend WithEvents lblNewAccount As System.Windows.Forms.Label
    Friend WithEvents pnlCredits As System.Windows.Forms.Panel
    Friend WithEvents lblCreditsTop As System.Windows.Forms.Label
    Friend WithEvents lblScrollingCredits As System.Windows.Forms.Label
    Friend WithEvents tmrCredits As System.Windows.Forms.Timer
    Friend WithEvents pnlNewChar As System.Windows.Forms.Panel
    Friend WithEvents placeholderforsprite As System.Windows.Forms.PictureBox
    Friend WithEvents rdoFemale As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMale As System.Windows.Forms.RadioButton
    Friend WithEvents cmbJob As System.Windows.Forms.ComboBox
    Friend WithEvents lblNewCharGender As System.Windows.Forms.Label
    Friend WithEvents lblNewCharJob As System.Windows.Forms.Label
    Friend WithEvents txtCharName As System.Windows.Forms.TextBox
    Friend WithEvents lblNewCharName As System.Windows.Forms.Label
    Friend WithEvents lblNewChar As System.Windows.Forms.Label
    Friend WithEvents lblStatusHeader As System.Windows.Forms.Label
    Friend WithEvents lblServerStatus As System.Windows.Forms.Label
    Friend WithEvents pnlMainMenu As System.Windows.Forms.Panel
    Friend WithEvents lblNewsHeader As System.Windows.Forms.Label
    Friend WithEvents lblNews As System.Windows.Forms.Label
    Friend WithEvents picLogo As Windows.Forms.PictureBox
    Friend WithEvents pnlCharSelect As Windows.Forms.Panel
    Friend WithEvents lblCharSelect As Windows.Forms.Label
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents picChar3 As Windows.Forms.PictureBox
    Friend WithEvents picChar2 As Windows.Forms.PictureBox
    Friend WithEvents picChar1 As Windows.Forms.PictureBox
    Friend WithEvents txtDescription As Windows.Forms.TextBox
    Friend WithEvents lblNewCharSprite As Windows.Forms.Label
    Friend WithEvents btnPlay As PictureBox
    Friend WithEvents btnRegister As PictureBox
    Friend WithEvents btnExit As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnNewChar As PictureBox
    Friend WithEvents btnDelChar As PictureBox
    Friend WithEvents btnUseChar As PictureBox
    Friend WithEvents btnCreateCharacter As PictureBox
    Friend WithEvents btnCreateAccount As PictureBox
    Friend WithEvents btnLogin As PictureBox
End Class
