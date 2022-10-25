<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOptions
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
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbScreenSize = New System.Windows.Forms.ComboBox()
        Me.lblVolume = New System.Windows.Forms.Label()
        Me.scrlVolume = New System.Windows.Forms.HScrollBar()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optSOff = New System.Windows.Forms.RadioButton()
        Me.optSOn = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optMOff = New System.Windows.Forms.RadioButton()
        Me.optMOn = New System.Windows.Forms.RadioButton()
        Me.chkVsync = New System.Windows.Forms.CheckBox()
        Me.chkNpcBars = New System.Windows.Forms.CheckBox()
        Me.chkFullscreen = New System.Windows.Forms.CheckBox()
        Me.chkOpenAdminPanelOnLogin = New System.Windows.Forms.CheckBox()
        Me.chkDynamicLighting = New System.Windows.Forms.CheckBox()
        Me.GroupBox2.SuspendLayout
        Me.GroupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.ForeColor = System.Drawing.Color.Black
        Me.btnSaveSettings.Location = New System.Drawing.Point(14, 272)
        Me.btnSaveSettings.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(239, 27)
        Me.btnSaveSettings.TabIndex = 14
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(14, 115)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 15)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Screen Size"
        '
        'cmbScreenSize
        '
        Me.cmbScreenSize.FormattingEnabled = true
        Me.cmbScreenSize.Items.AddRange(New Object() {"1024x768", "1152x864", "1280x720", "1336x768", "1600x900", "1920x1080", "2560x1440", "3840x2160"})
        Me.cmbScreenSize.Location = New System.Drawing.Point(13, 134)
        Me.cmbScreenSize.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbScreenSize.Name = "cmbScreenSize"
        Me.cmbScreenSize.Size = New System.Drawing.Size(240, 23)
        Me.cmbScreenSize.TabIndex = 12
        '
        'lblVolume
        '
        Me.lblVolume.AutoSize = true
        Me.lblVolume.BackColor = System.Drawing.Color.Transparent
        Me.lblVolume.ForeColor = System.Drawing.Color.Black
        Me.lblVolume.Location = New System.Drawing.Point(14, 62)
        Me.lblVolume.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVolume.Name = "lblVolume"
        Me.lblVolume.Size = New System.Drawing.Size(53, 15)
        Me.lblVolume.TabIndex = 11
        Me.lblVolume.Text = "Volume: "
        '
        'scrlVolume
        '
        Me.scrlVolume.LargeChange = 1
        Me.scrlVolume.Location = New System.Drawing.Point(14, 80)
        Me.scrlVolume.Name = "scrlVolume"
        Me.scrlVolume.Size = New System.Drawing.Size(239, 17)
        Me.scrlVolume.TabIndex = 10
        Me.scrlVolume.Value = 100
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optSOff)
        Me.GroupBox2.Controls.Add(Me.optSOn)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(138, 15)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox2.Size = New System.Drawing.Size(117, 44)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Sound"
        '
        'optSOff
        '
        Me.optSOff.AutoSize = true
        Me.optSOff.Location = New System.Drawing.Point(57, 22)
        Me.optSOff.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optSOff.Name = "optSOff"
        Me.optSOff.Size = New System.Drawing.Size(42, 19)
        Me.optSOff.TabIndex = 5
        Me.optSOff.TabStop = true
        Me.optSOff.Text = "Off"
        Me.optSOff.UseVisualStyleBackColor = true
        '
        'optSOn
        '
        Me.optSOn.AutoSize = true
        Me.optSOn.Location = New System.Drawing.Point(5, 22)
        Me.optSOn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optSOn.Name = "optSOn"
        Me.optSOn.Size = New System.Drawing.Size(41, 19)
        Me.optSOn.TabIndex = 4
        Me.optSOn.TabStop = true
        Me.optSOn.Text = "On"
        Me.optSOn.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optMOff)
        Me.GroupBox1.Controls.Add(Me.optMOn)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(14, 14)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(117, 45)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Music"
        '
        'optMOff
        '
        Me.optMOff.AutoSize = true
        Me.optMOff.Location = New System.Drawing.Point(57, 20)
        Me.optMOff.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optMOff.Name = "optMOff"
        Me.optMOff.Size = New System.Drawing.Size(42, 19)
        Me.optMOff.TabIndex = 2
        Me.optMOff.TabStop = true
        Me.optMOff.Text = "Off"
        Me.optMOff.UseVisualStyleBackColor = true
        '
        'optMOn
        '
        Me.optMOn.AutoSize = true
        Me.optMOn.Location = New System.Drawing.Point(5, 20)
        Me.optMOn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.optMOn.Name = "optMOn"
        Me.optMOn.Size = New System.Drawing.Size(41, 19)
        Me.optMOn.TabIndex = 1
        Me.optMOn.TabStop = true
        Me.optMOn.Text = "On"
        Me.optMOn.UseVisualStyleBackColor = true
        '
        'chkVsync
        '
        Me.chkVsync.AutoSize = true
        Me.chkVsync.Location = New System.Drawing.Point(14, 165)
        Me.chkVsync.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkVsync.Name = "chkVsync"
        Me.chkVsync.Size = New System.Drawing.Size(57, 19)
        Me.chkVsync.TabIndex = 15
        Me.chkVsync.Text = "Vsync"
        Me.chkVsync.UseVisualStyleBackColor = true
        '
        'chkNpcBars
        '
        Me.chkNpcBars.AutoSize = true
        Me.chkNpcBars.Location = New System.Drawing.Point(13, 192)
        Me.chkNpcBars.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkNpcBars.Name = "chkNpcBars"
        Me.chkNpcBars.Size = New System.Drawing.Size(105, 19)
        Me.chkNpcBars.TabIndex = 16
        Me.chkNpcBars.Text = "Show Npc Bars"
        Me.chkNpcBars.UseVisualStyleBackColor = true
        '
        'chkFullscreen
        '
        Me.chkFullscreen.AutoSize = true
        Me.chkFullscreen.Location = New System.Drawing.Point(168, 165)
        Me.chkFullscreen.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkFullscreen.Name = "chkFullscreen"
        Me.chkFullscreen.Size = New System.Drawing.Size(85, 19)
        Me.chkFullscreen.TabIndex = 17
        Me.chkFullscreen.Text = "Fullscreeen"
        Me.chkFullscreen.UseVisualStyleBackColor = true
        '
        'chkOpenAdminPanelOnLogin
        '
        Me.chkOpenAdminPanelOnLogin.AutoSize = true
        Me.chkOpenAdminPanelOnLogin.Location = New System.Drawing.Point(14, 242)
        Me.chkOpenAdminPanelOnLogin.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkOpenAdminPanelOnLogin.Name = "chkOpenAdminPanelOnLogin"
        Me.chkOpenAdminPanelOnLogin.Size = New System.Drawing.Size(178, 19)
        Me.chkOpenAdminPanelOnLogin.TabIndex = 18
        Me.chkOpenAdminPanelOnLogin.Text = "Open Admin Panel On Login"
        Me.chkOpenAdminPanelOnLogin.UseVisualStyleBackColor = true
        '
        'chkDynamicLighting
        '
        Me.chkDynamicLighting.AutoSize = true
        Me.chkDynamicLighting.Location = New System.Drawing.Point(13, 217)
        Me.chkDynamicLighting.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkDynamicLighting.Name = "chkDynamicLighting"
        Me.chkDynamicLighting.Size = New System.Drawing.Size(120, 19)
        Me.chkDynamicLighting.TabIndex = 19
        Me.chkDynamicLighting.Text = "Dynamic Lighting"
        Me.chkDynamicLighting.UseVisualStyleBackColor = true
        '
        'FrmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 15!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = true
        Me.ClientSize = New System.Drawing.Size(271, 305)
        Me.Controls.Add(Me.chkDynamicLighting)
        Me.Controls.Add(Me.chkOpenAdminPanelOnLogin)
        Me.Controls.Add(Me.chkFullscreen)
        Me.Controls.Add(Me.chkNpcBars)
        Me.Controls.Add(Me.chkVsync)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmbScreenSize)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblVolume)
        Me.Controls.Add(Me.scrlVolume)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "FrmOptions"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Game Options"
        Me.TopMost = true
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btnSaveSettings As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents cmbScreenSize As Windows.Forms.ComboBox
    Friend WithEvents lblVolume As Windows.Forms.Label
    Friend WithEvents scrlVolume As Windows.Forms.HScrollBar
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents optSOff As Windows.Forms.RadioButton
    Friend WithEvents optSOn As Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents optMOff As Windows.Forms.RadioButton
    Friend WithEvents optMOn As Windows.Forms.RadioButton
    Friend WithEvents chkVsync As Windows.Forms.CheckBox
    Friend WithEvents chkNpcBars As Windows.Forms.CheckBox
    Friend WithEvents chkFullscreen As CheckBox
    Friend WithEvents chkOpenAdminPanelOnLogin As CheckBox
    Friend WithEvents chkDynamicLighting As CheckBox
End Class
