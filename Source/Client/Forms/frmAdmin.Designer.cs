using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class FrmAdmin : Form
    {

        // Shared instance of the form
        private static FrmAdmin _instance;

        // Public property to get the shared instance
        public static FrmAdmin Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new FrmAdmin();
                }
                return _instance;
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdmin));
            btnRespawn = new DarkButton();
            btnMapReport = new DarkButton();
            btnALoc = new DarkButton();
            btnAdminSetSprite = new DarkButton();
            btnAdminWarpTo = new DarkButton();
            Label5 = new DarkLabel();
            Label4 = new DarkLabel();
            btnAdminSetAccess = new DarkButton();
            btnAdminWarpMe2 = new DarkButton();
            btnAdminWarp2Me = new DarkButton();
            btnAdminBan = new DarkButton();
            btnAdminKick = new DarkButton();
            txtAdminName = new DarkTextBox();
            Label3 = new DarkLabel();
            Label2 = new DarkLabel();
            lstMaps = new DarkListView();
            ColumnHeader1 = new ColumnHeader();
            ColumnHeader2 = new ColumnHeader();
            TabControl1 = new TabControl();
            tabModeration = new TabPage();
            nudAdminSprite = new DarkNumericUpDown();
            nudAdminMap = new DarkNumericUpDown();
            btnLevelUp = new DarkButton();
            cmbAccess = new DarkComboBox();
            tabMapList = new TabPage();
            tabMapTools = new TabPage();
            tabEditors = new TabPage();
            btnScriptEditor = new DarkButton();
            btnMoralEditor = new DarkButton();
            btnJobEditor = new DarkButton();
            btnProjectiles = new DarkButton();
            btnMapEditor = new DarkButton();
            btnItemEditor = new DarkButton();
            btnResourceEditor = new DarkButton();
            btnNpcEditor = new DarkButton();
            btnSkillEditor = new DarkButton();
            btnShopEditor = new DarkButton();
            btnAnimationEditor = new DarkButton();
            TabControl1.SuspendLayout();
            tabModeration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAdminSprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAdminMap).BeginInit();
            tabMapList.SuspendLayout();
            tabMapTools.SuspendLayout();
            tabEditors.SuspendLayout();
            SuspendLayout();
            // 
            // btnRespawn
            // 
            btnRespawn.Location = new Point(295, 38);
            btnRespawn.Margin = new Padding(7, 6, 7, 6);
            btnRespawn.Name = "btnRespawn";
            btnRespawn.Padding = new Padding(9, 11, 9, 11);
            btnRespawn.Size = new Size(230, 53);
            btnRespawn.TabIndex = 34;
            btnRespawn.Text = "Respawn Map";
            btnRespawn.Click += BtnRespawn_Click;
            // 
            // btnMapReport
            // 
            btnMapReport.Location = new Point(13, 514);
            btnMapReport.Margin = new Padding(7, 6, 7, 6);
            btnMapReport.Name = "btnMapReport";
            btnMapReport.Padding = new Padding(9, 11, 9, 11);
            btnMapReport.Size = new Size(516, 53);
            btnMapReport.TabIndex = 33;
            btnMapReport.Text = "Refresh List";
            btnMapReport.Click += BtnMapReport_Click;
            // 
            // btnALoc
            // 
            btnALoc.Location = new Point(30, 38);
            btnALoc.Margin = new Padding(7, 6, 7, 6);
            btnALoc.Name = "btnALoc";
            btnALoc.Padding = new Padding(9, 11, 9, 11);
            btnALoc.Size = new Size(230, 53);
            btnALoc.TabIndex = 31;
            btnALoc.Text = "Location";
            btnALoc.Click += BtnALoc_Click;
            // 
            // btnAdminSetSprite
            // 
            btnAdminSetSprite.Location = new Point(290, 501);
            btnAdminSetSprite.Margin = new Padding(7, 6, 7, 6);
            btnAdminSetSprite.Name = "btnAdminSetSprite";
            btnAdminSetSprite.Padding = new Padding(9, 11, 9, 11);
            btnAdminSetSprite.Size = new Size(234, 60);
            btnAdminSetSprite.TabIndex = 16;
            btnAdminSetSprite.Text = "Set Player Sprite";
            btnAdminSetSprite.Click += BtnAdminSetSprite_Click;
            // 
            // btnAdminWarpTo
            // 
            btnAdminWarpTo.Location = new Point(290, 433);
            btnAdminWarpTo.Margin = new Padding(7, 6, 7, 6);
            btnAdminWarpTo.Name = "btnAdminWarpTo";
            btnAdminWarpTo.Padding = new Padding(9, 11, 9, 11);
            btnAdminWarpTo.Size = new Size(234, 60);
            btnAdminWarpTo.TabIndex = 15;
            btnAdminWarpTo.Text = "Warp To Map";
            btnAdminWarpTo.Click += BtnAdminWarpTo_Click;
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.ForeColor = Color.FromArgb(220, 220, 220);
            Label5.Location = new Point(13, 516);
            Label5.Margin = new Padding(7, 0, 7, 0);
            Label5.Name = "Label5";
            Label5.Size = new Size(81, 32);
            Label5.TabIndex = 13;
            Label5.Text = "Sprite:";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.ForeColor = Color.FromArgb(220, 220, 220);
            Label4.Location = new Point(13, 448);
            Label4.Margin = new Padding(7, 0, 7, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(162, 32);
            Label4.TabIndex = 11;
            Label4.Text = "Map Number:";
            // 
            // btnAdminSetAccess
            // 
            btnAdminSetAccess.Location = new Point(19, 365);
            btnAdminSetAccess.Margin = new Padding(7, 6, 7, 6);
            btnAdminSetAccess.Name = "btnAdminSetAccess";
            btnAdminSetAccess.Padding = new Padding(9, 11, 9, 11);
            btnAdminSetAccess.Size = new Size(505, 53);
            btnAdminSetAccess.TabIndex = 9;
            btnAdminSetAccess.Text = "Set Access";
            btnAdminSetAccess.Click += BtnAdminSetAccess_Click;
            // 
            // btnAdminWarpMe2
            // 
            btnAdminWarpMe2.Location = new Point(275, 154);
            btnAdminWarpMe2.Margin = new Padding(7, 6, 7, 6);
            btnAdminWarpMe2.Name = "btnAdminWarpMe2";
            btnAdminWarpMe2.Padding = new Padding(9, 11, 9, 11);
            btnAdminWarpMe2.Size = new Size(249, 53);
            btnAdminWarpMe2.TabIndex = 8;
            btnAdminWarpMe2.Text = "Warp Me To Player";
            btnAdminWarpMe2.Click += BtnAdminWarpMe2_Click;
            // 
            // btnAdminWarp2Me
            // 
            btnAdminWarp2Me.Location = new Point(13, 154);
            btnAdminWarp2Me.Margin = new Padding(7, 6, 7, 6);
            btnAdminWarp2Me.Name = "btnAdminWarp2Me";
            btnAdminWarp2Me.Padding = new Padding(9, 11, 9, 11);
            btnAdminWarp2Me.Size = new Size(249, 53);
            btnAdminWarp2Me.TabIndex = 7;
            btnAdminWarp2Me.Text = "Warp Player To Me";
            btnAdminWarp2Me.Click += BtnAdminWarp2Me_Click;
            // 
            // btnAdminBan
            // 
            btnAdminBan.Location = new Point(275, 83);
            btnAdminBan.Margin = new Padding(7, 6, 7, 6);
            btnAdminBan.Name = "btnAdminBan";
            btnAdminBan.Padding = new Padding(9, 11, 9, 11);
            btnAdminBan.Size = new Size(249, 53);
            btnAdminBan.TabIndex = 6;
            btnAdminBan.Text = "Ban Player";
            btnAdminBan.Click += BtnAdminBan_Click;
            // 
            // btnAdminKick
            // 
            btnAdminKick.Location = new Point(13, 83);
            btnAdminKick.Margin = new Padding(7, 6, 7, 6);
            btnAdminKick.Name = "btnAdminKick";
            btnAdminKick.Padding = new Padding(9, 11, 9, 11);
            btnAdminKick.Size = new Size(249, 53);
            btnAdminKick.TabIndex = 5;
            btnAdminKick.Text = "Kick Player";
            btnAdminKick.Click += BtnAdminKick_Click;
            // 
            // txtAdminName
            // 
            txtAdminName.BackColor = Color.FromArgb(69, 73, 74);
            txtAdminName.BorderStyle = BorderStyle.FixedSingle;
            txtAdminName.ForeColor = Color.FromArgb(220, 220, 220);
            txtAdminName.Location = new Point(178, 19);
            txtAdminName.Margin = new Padding(7, 6, 7, 6);
            txtAdminName.Name = "txtAdminName";
            txtAdminName.Size = new Size(344, 39);
            txtAdminName.TabIndex = 3;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.ForeColor = Color.FromArgb(220, 220, 220);
            Label3.Location = new Point(13, 305);
            Label3.Margin = new Padding(7, 0, 7, 0);
            Label3.Name = "Label3";
            Label3.Size = new Size(89, 32);
            Label3.TabIndex = 2;
            Label3.Text = "Access:";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.ForeColor = Color.FromArgb(220, 220, 220);
            Label2.Location = new Point(13, 28);
            Label2.Margin = new Padding(7, 0, 7, 0);
            Label2.Name = "Label2";
            Label2.Size = new Size(154, 32);
            Label2.TabIndex = 1;
            Label2.Text = "Player Name:";
            // 
            // lstMaps
            // 
            lstMaps.Location = new Point(13, 15);
            lstMaps.Margin = new Padding(7, 6, 7, 6);
            lstMaps.Name = "lstMaps";
            lstMaps.Size = new Size(516, 484);
            lstMaps.TabIndex = 4;
            lstMaps.DoubleClick += LstMaps_DoubleClick;
            // 
            // ColumnHeader1
            // 
            ColumnHeader1.Text = "#";
            ColumnHeader1.Width = 30;
            // 
            // ColumnHeader2
            // 
            ColumnHeader2.Text = "Name";
            ColumnHeader2.Width = 200;
            // 
            // TabControl1
            // 
            TabControl1.Controls.Add(tabModeration);
            TabControl1.Controls.Add(tabMapList);
            TabControl1.Controls.Add(tabMapTools);
            TabControl1.Controls.Add(tabEditors);
            TabControl1.Location = new Point(4, 4);
            TabControl1.Margin = new Padding(7, 6, 7, 6);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(559, 638);
            TabControl1.TabIndex = 38;
            // 
            // tabModeration
            // 
            tabModeration.BackColor = Color.FromArgb(45, 45, 48);
            tabModeration.Controls.Add(nudAdminSprite);
            tabModeration.Controls.Add(nudAdminMap);
            tabModeration.Controls.Add(btnLevelUp);
            tabModeration.Controls.Add(cmbAccess);
            tabModeration.Controls.Add(Label2);
            tabModeration.Controls.Add(Label3);
            tabModeration.Controls.Add(txtAdminName);
            tabModeration.Controls.Add(btnAdminKick);
            tabModeration.Controls.Add(btnAdminBan);
            tabModeration.Controls.Add(btnAdminWarp2Me);
            tabModeration.Controls.Add(btnAdminWarpMe2);
            tabModeration.Controls.Add(btnAdminSetAccess);
            tabModeration.Controls.Add(Label4);
            tabModeration.Controls.Add(Label5);
            tabModeration.Controls.Add(btnAdminWarpTo);
            tabModeration.Controls.Add(btnAdminSetSprite);
            tabModeration.Location = new Point(8, 46);
            tabModeration.Margin = new Padding(7, 6, 7, 6);
            tabModeration.Name = "tabModeration";
            tabModeration.Padding = new Padding(7, 6, 7, 6);
            tabModeration.Size = new Size(543, 584);
            tabModeration.TabIndex = 0;
            tabModeration.Text = "Moderation";
            // 
            // nudAdminSprite
            // 
            nudAdminSprite.Location = new Point(173, 512);
            nudAdminSprite.Margin = new Padding(7, 6, 7, 6);
            nudAdminSprite.Name = "nudAdminSprite";
            nudAdminSprite.Size = new Size(104, 39);
            nudAdminSprite.TabIndex = 33;
            // 
            // nudAdminMap
            // 
            nudAdminMap.Location = new Point(173, 437);
            nudAdminMap.Margin = new Padding(7, 6, 7, 6);
            nudAdminMap.Name = "nudAdminMap";
            nudAdminMap.Size = new Size(104, 39);
            nudAdminMap.TabIndex = 32;
            // 
            // btnLevelUp
            // 
            btnLevelUp.Location = new Point(69, 222);
            btnLevelUp.Margin = new Padding(7, 6, 7, 6);
            btnLevelUp.Name = "btnLevelUp";
            btnLevelUp.Padding = new Padding(9, 11, 9, 11);
            btnLevelUp.Size = new Size(407, 53);
            btnLevelUp.TabIndex = 31;
            btnLevelUp.Text = "Level Up";
            btnLevelUp.Click += BtnLevelUp_Click;
            // 
            // cmbAccess
            // 
            cmbAccess.DrawMode = DrawMode.OwnerDrawVariable;
            cmbAccess.FormattingEnabled = true;
            cmbAccess.Items.AddRange(new object[] { "Normal Player", "Moderator (GM)", "Mapper", "Developer", "Owner" });
            cmbAccess.Location = new Point(123, 299);
            cmbAccess.Margin = new Padding(7, 6, 7, 6);
            cmbAccess.Name = "cmbAccess";
            cmbAccess.Size = new Size(396, 40);
            cmbAccess.TabIndex = 17;
            // 
            // tabMapList
            // 
            tabMapList.BackColor = Color.FromArgb(45, 45, 48);
            tabMapList.Controls.Add(lstMaps);
            tabMapList.Controls.Add(btnMapReport);
            tabMapList.Location = new Point(8, 46);
            tabMapList.Margin = new Padding(7, 6, 7, 6);
            tabMapList.Name = "tabMapList";
            tabMapList.Padding = new Padding(7, 6, 7, 6);
            tabMapList.Size = new Size(543, 584);
            tabMapList.TabIndex = 2;
            tabMapList.Text = "Map List";
            // 
            // tabMapTools
            // 
            tabMapTools.BackColor = Color.FromArgb(45, 45, 48);
            tabMapTools.Controls.Add(btnRespawn);
            tabMapTools.Controls.Add(btnALoc);
            tabMapTools.Location = new Point(8, 46);
            tabMapTools.Margin = new Padding(7, 6, 7, 6);
            tabMapTools.Name = "tabMapTools";
            tabMapTools.Padding = new Padding(7, 6, 7, 6);
            tabMapTools.Size = new Size(543, 584);
            tabMapTools.TabIndex = 3;
            tabMapTools.Text = "Map Tools";
            // 
            // tabEditors
            // 
            tabEditors.BackColor = Color.FromArgb(45, 45, 48);
            tabEditors.Controls.Add(btnScriptEditor);
            tabEditors.Controls.Add(btnMoralEditor);
            tabEditors.Controls.Add(btnJobEditor);
            tabEditors.Controls.Add(btnProjectiles);
            tabEditors.Controls.Add(btnMapEditor);
            tabEditors.Controls.Add(btnItemEditor);
            tabEditors.Controls.Add(btnResourceEditor);
            tabEditors.Controls.Add(btnNpcEditor);
            tabEditors.Controls.Add(btnSkillEditor);
            tabEditors.Controls.Add(btnShopEditor);
            tabEditors.Controls.Add(btnAnimationEditor);
            tabEditors.Location = new Point(8, 46);
            tabEditors.Margin = new Padding(7, 6, 7, 6);
            tabEditors.Name = "tabEditors";
            tabEditors.Padding = new Padding(7, 6, 7, 6);
            tabEditors.Size = new Size(543, 584);
            tabEditors.TabIndex = 4;
            tabEditors.Text = "Editors";
            // 
            // btnScriptEditor
            // 
            btnScriptEditor.Location = new Point(286, 271);
            btnScriptEditor.Margin = new Padding(7, 6, 7, 6);
            btnScriptEditor.Name = "btnScriptEditor";
            btnScriptEditor.Padding = new Padding(9, 11, 9, 11);
            btnScriptEditor.Size = new Size(243, 62);
            btnScriptEditor.TabIndex = 70;
            btnScriptEditor.Text = "Script Editor";
            btnScriptEditor.Click += btnScriptEditor_Click;
            // 
            // btnMoralEditor
            // 
            btnMoralEditor.Location = new Point(286, 196);
            btnMoralEditor.Margin = new Padding(7, 6, 7, 6);
            btnMoralEditor.Name = "btnMoralEditor";
            btnMoralEditor.Padding = new Padding(9, 11, 9, 11);
            btnMoralEditor.Size = new Size(243, 62);
            btnMoralEditor.TabIndex = 69;
            btnMoralEditor.Text = "Moral Editor";
            btnMoralEditor.Click += btnMoralEditor_Click;
            // 
            // btnJobEditor
            // 
            btnJobEditor.Location = new Point(15, 122);
            btnJobEditor.Margin = new Padding(7, 6, 7, 6);
            btnJobEditor.Name = "btnJobEditor";
            btnJobEditor.Padding = new Padding(9, 11, 9, 11);
            btnJobEditor.Size = new Size(243, 62);
            btnJobEditor.TabIndex = 66;
            btnJobEditor.Text = "Job Editor";
            btnJobEditor.Click += btnJobEditor_Click;
            // 
            // btnProjectiles
            // 
            btnProjectiles.Location = new Point(286, 47);
            btnProjectiles.Margin = new Padding(7, 6, 7, 6);
            btnProjectiles.Name = "btnProjectiles";
            btnProjectiles.Padding = new Padding(9, 11, 9, 11);
            btnProjectiles.Size = new Size(243, 62);
            btnProjectiles.TabIndex = 64;
            btnProjectiles.Text = "Projectile Editor";
            btnProjectiles.Click += btnProjectiles_Click;
            // 
            // btnMapEditor
            // 
            btnMapEditor.Location = new Point(15, 420);
            btnMapEditor.Margin = new Padding(7, 6, 7, 6);
            btnMapEditor.Name = "btnMapEditor";
            btnMapEditor.Padding = new Padding(9, 11, 9, 11);
            btnMapEditor.Size = new Size(243, 62);
            btnMapEditor.TabIndex = 55;
            btnMapEditor.Text = "Map Editor";
            btnMapEditor.Click += BtnMapEditor_Click;
            // 
            // btnItemEditor
            // 
            btnItemEditor.Location = new Point(15, 271);
            btnItemEditor.Margin = new Padding(7, 6, 7, 6);
            btnItemEditor.Name = "btnItemEditor";
            btnItemEditor.Padding = new Padding(9, 11, 9, 11);
            btnItemEditor.Size = new Size(243, 62);
            btnItemEditor.TabIndex = 56;
            btnItemEditor.Text = "Item Editor";
            btnItemEditor.Click += btnItemEditor_Click;
            // 
            // btnResourceEditor
            // 
            btnResourceEditor.Location = new Point(286, 121);
            btnResourceEditor.Margin = new Padding(7, 6, 7, 6);
            btnResourceEditor.Name = "btnResourceEditor";
            btnResourceEditor.Padding = new Padding(9, 11, 9, 11);
            btnResourceEditor.Size = new Size(243, 62);
            btnResourceEditor.TabIndex = 57;
            btnResourceEditor.Text = "Resource Editor";
            btnResourceEditor.Click += btnResourceEditor_Click;
            // 
            // btnNpcEditor
            // 
            btnNpcEditor.Location = new Point(15, 196);
            btnNpcEditor.Margin = new Padding(7, 6, 7, 6);
            btnNpcEditor.Name = "btnNpcEditor";
            btnNpcEditor.Padding = new Padding(9, 11, 9, 11);
            btnNpcEditor.Size = new Size(243, 62);
            btnNpcEditor.TabIndex = 58;
            btnNpcEditor.Text = "Npc Editor";
            btnNpcEditor.Click += btnNpcEditor_Click;
            // 
            // btnSkillEditor
            // 
            btnSkillEditor.Location = new Point(15, 346);
            btnSkillEditor.Margin = new Padding(7, 6, 7, 6);
            btnSkillEditor.Name = "btnSkillEditor";
            btnSkillEditor.Padding = new Padding(9, 11, 9, 11);
            btnSkillEditor.Size = new Size(243, 62);
            btnSkillEditor.TabIndex = 59;
            btnSkillEditor.Text = "Skill Editor";
            btnSkillEditor.Click += btnSkillEditor_Click;
            // 
            // btnShopEditor
            // 
            btnShopEditor.Location = new Point(13, 495);
            btnShopEditor.Margin = new Padding(7, 6, 7, 6);
            btnShopEditor.Name = "btnShopEditor";
            btnShopEditor.Padding = new Padding(9, 11, 9, 11);
            btnShopEditor.Size = new Size(243, 62);
            btnShopEditor.TabIndex = 60;
            btnShopEditor.Text = "Shop Editor";
            btnShopEditor.Click += btnShopEditor_Click;
            // 
            // btnAnimationEditor
            // 
            btnAnimationEditor.Location = new Point(13, 47);
            btnAnimationEditor.Margin = new Padding(7, 6, 7, 6);
            btnAnimationEditor.Name = "btnAnimationEditor";
            btnAnimationEditor.Padding = new Padding(9, 11, 9, 11);
            btnAnimationEditor.Size = new Size(243, 62);
            btnAnimationEditor.TabIndex = 61;
            btnAnimationEditor.Text = "Animation Editor";
            btnAnimationEditor.Click += btnAnimationEditor_Click;
            // 
            // FrmAdmin
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 638);
            Controls.Add(TabControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(7, 6, 7, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAdmin";
            ShowIcon = false;
            Text = "Admin Panel";
            FormClosing += FrmAdmin_FormClosing;
            Load += FrmAdmin_Load;
            TabControl1.ResumeLayout(false);
            tabModeration.ResumeLayout(false);
            tabModeration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAdminSprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAdminMap).EndInit();
            tabMapList.ResumeLayout(false);
            tabMapTools.ResumeLayout(false);
            tabEditors.ResumeLayout(false);
            ResumeLayout(false);
        }

        internal DarkButton btnRespawn;
        internal DarkButton btnMapReport;
        internal DarkButton btnALoc;
        internal DarkButton btnAdminSetSprite;
        internal DarkButton btnAdminWarpTo;
        internal DarkLabel Label5;
        internal DarkLabel Label4;
        internal DarkButton btnAdminSetAccess;
        internal DarkButton btnAdminWarpMe2;
        internal DarkButton btnAdminWarp2Me;
        internal DarkButton btnAdminBan;
        internal DarkButton btnAdminKick;
        internal DarkTextBox txtAdminName;
        internal DarkLabel Label3;
        internal DarkLabel Label2;
        internal DarkListView lstMaps;
        internal ColumnHeader ColumnHeader1;
        internal ColumnHeader ColumnHeader2;
        internal TabControl TabControl1;
        internal TabPage tabModeration;
        internal TabPage tabMapList;
        internal TabPage tabMapTools;
        internal DarkComboBox cmbAccess;
        internal DarkNumericUpDown nudAdminSprite;
        internal DarkNumericUpDown nudAdminMap;
        internal DarkButton btnLevelUp;
        internal TabPage tabEditors;
        internal DarkButton btnJobEditor;
        internal DarkButton btnProjectiles;
        internal DarkButton btnMapEditor;
        internal DarkButton btnItemEditor;
        internal DarkButton btnResourceEditor;
        internal DarkButton btnNpcEditor;
        internal DarkButton btnSkillEditor;
        internal DarkButton btnShopEditor;
        internal DarkButton btnAnimationEditor;
        internal DarkButton btnMoralEditor;
        internal DarkButton btnScriptEditor;
    }
}