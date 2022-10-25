<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAdmin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAdmin))
        Me.btnRespawn = New System.Windows.Forms.Button()
        Me.btnMapReport = New System.Windows.Forms.Button()
        Me.btnALoc = New System.Windows.Forms.Button()
        Me.btnAdminSetSprite = New System.Windows.Forms.Button()
        Me.btnAdminWarpTo = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnAdminSetAccess = New System.Windows.Forms.Button()
        Me.btnAdminWarpMe2 = New System.Windows.Forms.Button()
        Me.btnAdminWarp2Me = New System.Windows.Forms.Button()
        Me.btnAdminBan = New System.Windows.Forms.Button()
        Me.btnAdminKick = New System.Windows.Forms.Button()
        Me.txtAdminName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstMaps = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabModeration = New System.Windows.Forms.TabPage()
        Me.nudAdminSprite = New System.Windows.Forms.NumericUpDown()
        Me.nudAdminMap = New System.Windows.Forms.NumericUpDown()
        Me.btnLevelUp = New System.Windows.Forms.Button()
        Me.cmbAccess = New System.Windows.Forms.ComboBox()
        Me.tabMapList = New System.Windows.Forms.TabPage()
        Me.tabMapTools = New System.Windows.Forms.TabPage()
        Me.tabEditors = New System.Windows.Forms.TabPage()
        Me.btnPetEditor = New System.Windows.Forms.Button()
        Me.btnJobEditor = New System.Windows.Forms.Button()
        Me.btnProjectiles = New System.Windows.Forms.Button()
        Me.btnMapEditor = New System.Windows.Forms.Button()
        Me.btnItemEditor = New System.Windows.Forms.Button()
        Me.btnResourceEditor = New System.Windows.Forms.Button()
        Me.btnNPCEditor = New System.Windows.Forms.Button()
        Me.btnSkillEditor = New System.Windows.Forms.Button()
        Me.btnShopEditor = New System.Windows.Forms.Button()
        Me.btnAnimationEditor = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.tabModeration.SuspendLayout()
        CType(Me.nudAdminSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudAdminMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMapList.SuspendLayout()
        Me.tabMapTools.SuspendLayout()
        Me.tabEditors.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRespawn
        '
        Me.btnRespawn.Location = New System.Drawing.Point(159, 18)
        Me.btnRespawn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRespawn.Name = "btnRespawn"
        Me.btnRespawn.Size = New System.Drawing.Size(124, 25)
        Me.btnRespawn.TabIndex = 34
        Me.btnRespawn.Text = "Respawn Map"
        Me.btnRespawn.UseVisualStyleBackColor = True
        '
        'btnMapReport
        '
        Me.btnMapReport.Location = New System.Drawing.Point(7, 241)
        Me.btnMapReport.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMapReport.Name = "btnMapReport"
        Me.btnMapReport.Size = New System.Drawing.Size(278, 25)
        Me.btnMapReport.TabIndex = 33
        Me.btnMapReport.Text = "Refresh List"
        Me.btnMapReport.UseVisualStyleBackColor = True
        '
        'btnALoc
        '
        Me.btnALoc.Location = New System.Drawing.Point(16, 18)
        Me.btnALoc.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnALoc.Name = "btnALoc"
        Me.btnALoc.Size = New System.Drawing.Size(124, 25)
        Me.btnALoc.TabIndex = 31
        Me.btnALoc.Text = "Location"
        Me.btnALoc.UseVisualStyleBackColor = True
        '
        'btnAdminSetSprite
        '
        Me.btnAdminSetSprite.Location = New System.Drawing.Point(156, 235)
        Me.btnAdminSetSprite.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAdminSetSprite.Name = "btnAdminSetSprite"
        Me.btnAdminSetSprite.Size = New System.Drawing.Size(126, 28)
        Me.btnAdminSetSprite.TabIndex = 16
        Me.btnAdminSetSprite.Text = "Set Player Sprite"
        Me.btnAdminSetSprite.UseVisualStyleBackColor = True
        '
        'btnAdminWarpTo
        '
        Me.btnAdminWarpTo.Location = New System.Drawing.Point(156, 203)
        Me.btnAdminWarpTo.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAdminWarpTo.Name = "btnAdminWarpTo"
        Me.btnAdminWarpTo.Size = New System.Drawing.Size(126, 28)
        Me.btnAdminWarpTo.TabIndex = 15
        Me.btnAdminWarpTo.Text = "Warp To Map"
        Me.btnAdminWarpTo.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 242)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 15)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Sprite:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 210)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 15)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Map Number:"
        '
        'btnAdminSetAccess
        '
        Me.btnAdminSetAccess.Location = New System.Drawing.Point(10, 171)
        Me.btnAdminSetAccess.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAdminSetAccess.Name = "btnAdminSetAccess"
        Me.btnAdminSetAccess.Size = New System.Drawing.Size(272, 25)
        Me.btnAdminSetAccess.TabIndex = 9
        Me.btnAdminSetAccess.Text = "Set Access"
        Me.btnAdminSetAccess.UseVisualStyleBackColor = True
        '
        'btnAdminWarpMe2
        '
        Me.btnAdminWarpMe2.Location = New System.Drawing.Point(148, 72)
        Me.btnAdminWarpMe2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAdminWarpMe2.Name = "btnAdminWarpMe2"
        Me.btnAdminWarpMe2.Size = New System.Drawing.Size(134, 25)
        Me.btnAdminWarpMe2.TabIndex = 8
        Me.btnAdminWarpMe2.Text = "Warp Me To Player"
        Me.btnAdminWarpMe2.UseVisualStyleBackColor = True
        '
        'btnAdminWarp2Me
        '
        Me.btnAdminWarp2Me.Location = New System.Drawing.Point(7, 72)
        Me.btnAdminWarp2Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAdminWarp2Me.Name = "btnAdminWarp2Me"
        Me.btnAdminWarp2Me.Size = New System.Drawing.Size(134, 25)
        Me.btnAdminWarp2Me.TabIndex = 7
        Me.btnAdminWarp2Me.Text = "Warp Player To Me"
        Me.btnAdminWarp2Me.UseVisualStyleBackColor = True
        '
        'btnAdminBan
        '
        Me.btnAdminBan.Location = New System.Drawing.Point(148, 39)
        Me.btnAdminBan.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAdminBan.Name = "btnAdminBan"
        Me.btnAdminBan.Size = New System.Drawing.Size(134, 25)
        Me.btnAdminBan.TabIndex = 6
        Me.btnAdminBan.Text = "Ban Player"
        Me.btnAdminBan.UseVisualStyleBackColor = True
        '
        'btnAdminKick
        '
        Me.btnAdminKick.Location = New System.Drawing.Point(7, 39)
        Me.btnAdminKick.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAdminKick.Name = "btnAdminKick"
        Me.btnAdminKick.Size = New System.Drawing.Size(134, 25)
        Me.btnAdminKick.TabIndex = 5
        Me.btnAdminKick.Text = "Kick Player"
        Me.btnAdminKick.UseVisualStyleBackColor = True
        '
        'txtAdminName
        '
        Me.txtAdminName.Location = New System.Drawing.Point(96, 9)
        Me.txtAdminName.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtAdminName.Name = "txtAdminName"
        Me.txtAdminName.Size = New System.Drawing.Size(186, 23)
        Me.txtAdminName.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 143)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Access:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 13)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Player Name:"
        '
        'lstMaps
        '
        Me.lstMaps.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lstMaps.FullRowSelect = True
        Me.lstMaps.GridLines = True
        Me.lstMaps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstMaps.Location = New System.Drawing.Point(7, 7)
        Me.lstMaps.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstMaps.MultiSelect = False
        Me.lstMaps.Name = "lstMaps"
        Me.lstMaps.Size = New System.Drawing.Size(278, 227)
        Me.lstMaps.TabIndex = 4
        Me.lstMaps.UseCompatibleStateImageBehavior = False
        Me.lstMaps.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "#"
        Me.ColumnHeader1.Width = 30
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Name"
        Me.ColumnHeader2.Width = 200
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabModeration)
        Me.TabControl1.Controls.Add(Me.tabMapList)
        Me.TabControl1.Controls.Add(Me.tabMapTools)
        Me.TabControl1.Controls.Add(Me.tabEditors)
        Me.TabControl1.Location = New System.Drawing.Point(2, 2)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(301, 299)
        Me.TabControl1.TabIndex = 38
        '
        'tabModeration
        '
        Me.tabModeration.Controls.Add(Me.nudAdminSprite)
        Me.tabModeration.Controls.Add(Me.nudAdminMap)
        Me.tabModeration.Controls.Add(Me.btnLevelUp)
        Me.tabModeration.Controls.Add(Me.cmbAccess)
        Me.tabModeration.Controls.Add(Me.Label2)
        Me.tabModeration.Controls.Add(Me.Label3)
        Me.tabModeration.Controls.Add(Me.txtAdminName)
        Me.tabModeration.Controls.Add(Me.btnAdminKick)
        Me.tabModeration.Controls.Add(Me.btnAdminBan)
        Me.tabModeration.Controls.Add(Me.btnAdminWarp2Me)
        Me.tabModeration.Controls.Add(Me.btnAdminWarpMe2)
        Me.tabModeration.Controls.Add(Me.btnAdminSetAccess)
        Me.tabModeration.Controls.Add(Me.Label4)
        Me.tabModeration.Controls.Add(Me.Label5)
        Me.tabModeration.Controls.Add(Me.btnAdminWarpTo)
        Me.tabModeration.Controls.Add(Me.btnAdminSetSprite)
        Me.tabModeration.Location = New System.Drawing.Point(4, 24)
        Me.tabModeration.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabModeration.Name = "tabModeration"
        Me.tabModeration.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabModeration.Size = New System.Drawing.Size(293, 271)
        Me.tabModeration.TabIndex = 0
        Me.tabModeration.Text = "Moderation"
        Me.tabModeration.UseVisualStyleBackColor = True
        '
        'nudAdminSprite
        '
        Me.nudAdminSprite.Location = New System.Drawing.Point(93, 240)
        Me.nudAdminSprite.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudAdminSprite.Name = "nudAdminSprite"
        Me.nudAdminSprite.Size = New System.Drawing.Size(56, 23)
        Me.nudAdminSprite.TabIndex = 33
        '
        'nudAdminMap
        '
        Me.nudAdminMap.Location = New System.Drawing.Point(93, 205)
        Me.nudAdminMap.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.nudAdminMap.Name = "nudAdminMap"
        Me.nudAdminMap.Size = New System.Drawing.Size(56, 23)
        Me.nudAdminMap.TabIndex = 32
        '
        'btnLevelUp
        '
        Me.btnLevelUp.Location = New System.Drawing.Point(37, 104)
        Me.btnLevelUp.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnLevelUp.Name = "btnLevelUp"
        Me.btnLevelUp.Size = New System.Drawing.Size(219, 25)
        Me.btnLevelUp.TabIndex = 31
        Me.btnLevelUp.Text = "Level Up"
        Me.btnLevelUp.UseVisualStyleBackColor = True
        '
        'cmbAccess
        '
        Me.cmbAccess.FormattingEnabled = True
        Me.cmbAccess.Items.AddRange(New Object() {"Normal Player", "Moderator (GM)", "Mapper", "Developer", "Creator"})
        Me.cmbAccess.Location = New System.Drawing.Point(66, 140)
        Me.cmbAccess.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbAccess.Name = "cmbAccess"
        Me.cmbAccess.Size = New System.Drawing.Size(215, 23)
        Me.cmbAccess.TabIndex = 17
        '
        'tabMapList
        '
        Me.tabMapList.Controls.Add(Me.lstMaps)
        Me.tabMapList.Controls.Add(Me.btnMapReport)
        Me.tabMapList.Location = New System.Drawing.Point(4, 24)
        Me.tabMapList.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabMapList.Name = "tabMapList"
        Me.tabMapList.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabMapList.Size = New System.Drawing.Size(293, 271)
        Me.tabMapList.TabIndex = 2
        Me.tabMapList.Text = "Map List"
        Me.tabMapList.UseVisualStyleBackColor = True
        '
        'tabMapTools
        '
        Me.tabMapTools.Controls.Add(Me.btnRespawn)
        Me.tabMapTools.Controls.Add(Me.btnALoc)
        Me.tabMapTools.Location = New System.Drawing.Point(4, 24)
        Me.tabMapTools.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabMapTools.Name = "tabMapTools"
        Me.tabMapTools.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabMapTools.Size = New System.Drawing.Size(293, 271)
        Me.tabMapTools.TabIndex = 3
        Me.tabMapTools.Text = "Map Tools"
        Me.tabMapTools.UseVisualStyleBackColor = True
        '
        'tabEditors
        '
        Me.tabEditors.Controls.Add(Me.btnPetEditor)
        Me.tabEditors.Controls.Add(Me.btnJobEditor)
        Me.tabEditors.Controls.Add(Me.btnProjectiles)
        Me.tabEditors.Controls.Add(Me.btnMapEditor)
        Me.tabEditors.Controls.Add(Me.btnItemEditor)
        Me.tabEditors.Controls.Add(Me.btnResourceEditor)
        Me.tabEditors.Controls.Add(Me.btnNPCEditor)
        Me.tabEditors.Controls.Add(Me.btnSkillEditor)
        Me.tabEditors.Controls.Add(Me.btnShopEditor)
        Me.tabEditors.Controls.Add(Me.btnAnimationEditor)
        Me.tabEditors.Location = New System.Drawing.Point(4, 24)
        Me.tabEditors.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabEditors.Name = "tabEditors"
        Me.tabEditors.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tabEditors.Size = New System.Drawing.Size(293, 271)
        Me.tabEditors.TabIndex = 4
        Me.tabEditors.Text = "Editors"
        Me.tabEditors.UseVisualStyleBackColor = True
        '
        'btnPetEditor
        '
        Me.btnPetEditor.Location = New System.Drawing.Point(152, 6)
        Me.btnPetEditor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnPetEditor.Name = "btnPetEditor"
        Me.btnPetEditor.Size = New System.Drawing.Size(131, 29)
        Me.btnPetEditor.TabIndex = 68
        Me.btnPetEditor.Text = "Pet Editor"
        Me.btnPetEditor.UseVisualStyleBackColor = True
        '
        'btnJobEditor
        '
        Me.btnJobEditor.Location = New System.Drawing.Point(7, 41)
        Me.btnJobEditor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnJobEditor.Name = "btnJobEditor"
        Me.btnJobEditor.Size = New System.Drawing.Size(131, 29)
        Me.btnJobEditor.TabIndex = 66
        Me.btnJobEditor.Text = "Job Editor"
        Me.btnJobEditor.UseVisualStyleBackColor = True
        '
        'btnProjectiles
        '
        Me.btnProjectiles.Location = New System.Drawing.Point(152, 41)
        Me.btnProjectiles.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnProjectiles.Name = "btnProjectiles"
        Me.btnProjectiles.Size = New System.Drawing.Size(131, 29)
        Me.btnProjectiles.TabIndex = 64
        Me.btnProjectiles.Text = "Projectiles Editor"
        Me.btnProjectiles.UseVisualStyleBackColor = True
        '
        'btnMapEditor
        '
        Me.btnMapEditor.Location = New System.Drawing.Point(8, 181)
        Me.btnMapEditor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnMapEditor.Name = "btnMapEditor"
        Me.btnMapEditor.Size = New System.Drawing.Size(131, 29)
        Me.btnMapEditor.TabIndex = 55
        Me.btnMapEditor.Text = "Map Editor"
        Me.btnMapEditor.UseVisualStyleBackColor = True
        '
        'btnItemEditor
        '
        Me.btnItemEditor.Location = New System.Drawing.Point(8, 111)
        Me.btnItemEditor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnItemEditor.Name = "btnItemEditor"
        Me.btnItemEditor.Size = New System.Drawing.Size(131, 29)
        Me.btnItemEditor.TabIndex = 56
        Me.btnItemEditor.Text = "Item Editor"
        Me.btnItemEditor.UseVisualStyleBackColor = True
        '
        'btnResourceEditor
        '
        Me.btnResourceEditor.Location = New System.Drawing.Point(152, 76)
        Me.btnResourceEditor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnResourceEditor.Name = "btnResourceEditor"
        Me.btnResourceEditor.Size = New System.Drawing.Size(131, 29)
        Me.btnResourceEditor.TabIndex = 57
        Me.btnResourceEditor.Text = "Resource Editor"
        Me.btnResourceEditor.UseVisualStyleBackColor = true
        '
        'btnNPCEditor
        '
        Me.btnNPCEditor.Location = New System.Drawing.Point(8, 76)
        Me.btnNPCEditor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnNPCEditor.Name = "btnNPCEditor"
        Me.btnNPCEditor.Size = New System.Drawing.Size(131, 29)
        Me.btnNPCEditor.TabIndex = 58
        Me.btnNPCEditor.Text = "NPC Editor"
        Me.btnNPCEditor.UseVisualStyleBackColor = true
        '
        'btnSkillEditor
        '
        Me.btnSkillEditor.Location = New System.Drawing.Point(8, 146)
        Me.btnSkillEditor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSkillEditor.Name = "btnSkillEditor"
        Me.btnSkillEditor.Size = New System.Drawing.Size(131, 29)
        Me.btnSkillEditor.TabIndex = 59
        Me.btnSkillEditor.Text = "Skill Editor"
        Me.btnSkillEditor.UseVisualStyleBackColor = true
        '
        'btnShopEditor
        '
        Me.btnShopEditor.Location = New System.Drawing.Point(8, 216)
        Me.btnShopEditor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnShopEditor.Name = "btnShopEditor"
        Me.btnShopEditor.Size = New System.Drawing.Size(131, 29)
        Me.btnShopEditor.TabIndex = 60
        Me.btnShopEditor.Text = "Shop Editor"
        Me.btnShopEditor.UseVisualStyleBackColor = true
        '
        'btnAnimationEditor
        '
        Me.btnAnimationEditor.Location = New System.Drawing.Point(8, 6)
        Me.btnAnimationEditor.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAnimationEditor.Name = "btnAnimationEditor"
        Me.btnAnimationEditor.Size = New System.Drawing.Size(131, 29)
        Me.btnAnimationEditor.TabIndex = 61
        Me.btnAnimationEditor.Text = "Animation Editor"
        Me.btnAnimationEditor.UseVisualStyleBackColor = true
        '
        'FrmAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 15!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(305, 299)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "FrmAdmin"
        Me.ShowIcon = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Admin Panel"
        Me.TabControl1.ResumeLayout(false)
        Me.tabModeration.ResumeLayout(false)
        Me.tabModeration.PerformLayout
        CType(Me.nudAdminSprite,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudAdminMap,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabMapList.ResumeLayout(false)
        Me.tabMapTools.ResumeLayout(false)
        Me.tabEditors.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents btnRespawn As Windows.Forms.Button
    Friend WithEvents btnMapReport As Windows.Forms.Button
    Friend WithEvents btnALoc As Windows.Forms.Button
    Friend WithEvents btnAdminSetSprite As Windows.Forms.Button
    Friend WithEvents btnAdminWarpTo As Windows.Forms.Button
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents btnAdminSetAccess As Windows.Forms.Button
    Friend WithEvents btnAdminWarpMe2 As Windows.Forms.Button
    Friend WithEvents btnAdminWarp2Me As Windows.Forms.Button
    Friend WithEvents btnAdminBan As Windows.Forms.Button
    Friend WithEvents btnAdminKick As Windows.Forms.Button
    Friend WithEvents txtAdminName As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents lstMaps As Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As Windows.Forms.ColumnHeader
    Friend WithEvents TabControl1 As Windows.Forms.TabControl
    Friend WithEvents tabModeration As Windows.Forms.TabPage
    Friend WithEvents tabMapList As Windows.Forms.TabPage
    Friend WithEvents tabMapTools As Windows.Forms.TabPage
    Friend WithEvents cmbAccess As Windows.Forms.ComboBox
    Friend WithEvents nudAdminSprite As Windows.Forms.NumericUpDown
    Friend WithEvents nudAdminMap As Windows.Forms.NumericUpDown
    Friend WithEvents btnLevelUp As Windows.Forms.Button
    Friend WithEvents tabEditors As Windows.Forms.TabPage
    Friend WithEvents btnPetEditor As Windows.Forms.Button
    Friend WithEvents btnJobEditor As Windows.Forms.Button
    Friend WithEvents btnProjectiles As Windows.Forms.Button
    Friend WithEvents btnMapEditor As Windows.Forms.Button
    Friend WithEvents btnItemEditor As Windows.Forms.Button
    Friend WithEvents btnResourceEditor As Windows.Forms.Button
    Friend WithEvents btnNPCEditor As Windows.Forms.Button
    Friend WithEvents btnSkillEditor As Windows.Forms.Button
    Friend WithEvents btnShopEditor As Windows.Forms.Button
    Friend WithEvents btnAnimationEditor As Windows.Forms.Button
End Class
