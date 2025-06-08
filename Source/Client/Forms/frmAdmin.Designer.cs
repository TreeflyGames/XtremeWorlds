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
            btnMoralEditor = new DarkButton();
            btnPetEditor = new DarkButton();
            btnJobEditor = new DarkButton();
            btnProjectiles = new DarkButton();
            btnMapEditor = new DarkButton();
            btnItemEditor = new DarkButton();
            btnResourceEditor = new DarkButton();
            btnNPCEditor = new DarkButton();
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
            btnRespawn.Location = new Point(159, 18);
            btnRespawn.Margin = new Padding(4, 3, 4, 3);
            btnRespawn.Name = "btnRespawn";
            btnRespawn.Padding = new Padding(5);
            btnRespawn.Size = new Size(124, 25);
            btnRespawn.TabIndex = 34;
            btnRespawn.Text = "Respawn Map";
            btnRespawn.Click += BtnRespawn_Click;
            // 
            // btnMapReport
            // 
            btnMapReport.Location = new Point(7, 241);
            btnMapReport.Margin = new Padding(4, 3, 4, 3);
            btnMapReport.Name = "btnMapReport";
            btnMapReport.Padding = new Padding(5);
            btnMapReport.Size = new Size(278, 25);
            btnMapReport.TabIndex = 33;
            btnMapReport.Text = "Refresh List";
            btnMapReport.Click += BtnMapReport_Click;
            // 
            // btnALoc
            // 
            btnALoc.Location = new Point(16, 18);
            btnALoc.Margin = new Padding(4, 3, 4, 3);
            btnALoc.Name = "btnALoc";
            btnALoc.Padding = new Padding(5);
            btnALoc.Size = new Size(124, 25);
            btnALoc.TabIndex = 31;
            btnALoc.Text = "Location";
            btnALoc.Click += BtnALoc_Click;
            // 
            // btnAdminSetSprite
            // 
            btnAdminSetSprite.Location = new Point(156, 235);
            btnAdminSetSprite.Margin = new Padding(4, 3, 4, 3);
            btnAdminSetSprite.Name = "btnAdminSetSprite";
            btnAdminSetSprite.Padding = new Padding(5);
            btnAdminSetSprite.Size = new Size(126, 28);
            btnAdminSetSprite.TabIndex = 16;
            btnAdminSetSprite.Text = "Set Player Sprite";
            btnAdminSetSprite.Click += BtnAdminSetSprite_Click;
            // 
            // btnAdminWarpTo
            // 
            btnAdminWarpTo.Location = new Point(156, 203);
            btnAdminWarpTo.Margin = new Padding(4, 3, 4, 3);
            btnAdminWarpTo.Name = "btnAdminWarpTo";
            btnAdminWarpTo.Padding = new Padding(5);
            btnAdminWarpTo.Size = new Size(126, 28);
            btnAdminWarpTo.TabIndex = 15;
            btnAdminWarpTo.Text = "Warp To Map";
            btnAdminWarpTo.Click += BtnAdminWarpTo_Click;
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.ForeColor = Color.FromArgb(220, 220, 220);
            Label5.Location = new Point(7, 242);
            Label5.Margin = new Padding(4, 0, 4, 0);
            Label5.Name = "Label5";
            Label5.Size = new Size(40, 15);
            Label5.TabIndex = 13;
            Label5.Text = "Sprite:";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.ForeColor = Color.FromArgb(220, 220, 220);
            Label4.Location = new Point(7, 210);
            Label4.Margin = new Padding(4, 0, 4, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(81, 15);
            Label4.TabIndex = 11;
            Label4.Text = "Map Number:";
            // 
            // btnAdminSetAccess
            // 
            btnAdminSetAccess.Location = new Point(10, 171);
            btnAdminSetAccess.Margin = new Padding(4, 3, 4, 3);
            btnAdminSetAccess.Name = "btnAdminSetAccess";
            btnAdminSetAccess.Padding = new Padding(5);
            btnAdminSetAccess.Size = new Size(272, 25);
            btnAdminSetAccess.TabIndex = 9;
            btnAdminSetAccess.Text = "Set Access";
            btnAdminSetAccess.Click += BtnAdminSetAccess_Click;
            // 
            // btnAdminWarpMe2
            // 
            btnAdminWarpMe2.Location = new Point(148, 72);
            btnAdminWarpMe2.Margin = new Padding(4, 3, 4, 3);
            btnAdminWarpMe2.Name = "btnAdminWarpMe2";
            btnAdminWarpMe2.Padding = new Padding(5);
            btnAdminWarpMe2.Size = new Size(134, 25);
            btnAdminWarpMe2.TabIndex = 8;
            btnAdminWarpMe2.Text = "Warp Me To Player";
            btnAdminWarpMe2.Click += BtnAdminWarpMe2_Click;
            // 
            // btnAdminWarp2Me
            // 
            btnAdminWarp2Me.Location = new Point(7, 72);
            btnAdminWarp2Me.Margin = new Padding(4, 3, 4, 3);
            btnAdminWarp2Me.Name = "btnAdminWarp2Me";
            btnAdminWarp2Me.Padding = new Padding(5);
            btnAdminWarp2Me.Size = new Size(134, 25);
            btnAdminWarp2Me.TabIndex = 7;
            btnAdminWarp2Me.Text = "Warp Player To Me";
            btnAdminWarp2Me.Click += BtnAdminWarp2Me_Click;
            // 
            // btnAdminBan
            // 
            btnAdminBan.Location = new Point(148, 39);
            btnAdminBan.Margin = new Padding(4, 3, 4, 3);
            btnAdminBan.Name = "btnAdminBan";
            btnAdminBan.Padding = new Padding(5);
            btnAdminBan.Size = new Size(134, 25);
            btnAdminBan.TabIndex = 6;
            btnAdminBan.Text = "Ban Player";
            btnAdminBan.Click += BtnAdminBan_Click;
            // 
            // btnAdminKick
            // 
            btnAdminKick.Location = new Point(7, 39);
            btnAdminKick.Margin = new Padding(4, 3, 4, 3);
            btnAdminKick.Name = "btnAdminKick";
            btnAdminKick.Padding = new Padding(5);
            btnAdminKick.Size = new Size(134, 25);
            btnAdminKick.TabIndex = 5;
            btnAdminKick.Text = "Kick Player";
            btnAdminKick.Click += BtnAdminKick_Click;
            // 
            // txtAdminName
            // 
            txtAdminName.BackColor = Color.FromArgb(69, 73, 74);
            txtAdminName.BorderStyle = BorderStyle.FixedSingle;
            txtAdminName.ForeColor = Color.FromArgb(220, 220, 220);
            txtAdminName.Location = new Point(96, 9);
            txtAdminName.Margin = new Padding(4, 3, 4, 3);
            txtAdminName.Name = "txtAdminName";
            txtAdminName.Size = new Size(186, 23);
            txtAdminName.TabIndex = 3;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.ForeColor = Color.FromArgb(220, 220, 220);
            Label3.Location = new Point(7, 143);
            Label3.Margin = new Padding(4, 0, 4, 0);
            Label3.Name = "Label3";
            Label3.Size = new Size(46, 15);
            Label3.TabIndex = 2;
            Label3.Text = "Access:";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.ForeColor = Color.FromArgb(220, 220, 220);
            Label2.Location = new Point(7, 13);
            Label2.Margin = new Padding(4, 0, 4, 0);
            Label2.Name = "Label2";
            Label2.Size = new Size(77, 15);
            Label2.TabIndex = 1;
            Label2.Text = "Player Name:";
            // 
            // lstMaps
            // 
            lstMaps.Location = new Point(7, 7);
            lstMaps.Margin = new Padding(4, 3, 4, 3);
            lstMaps.Name = "lstMaps";
            lstMaps.Size = new Size(278, 227);
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
            TabControl1.Location = new Point(2, 2);
            TabControl1.Margin = new Padding(4, 3, 4, 3);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(301, 299);
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
            tabModeration.Location = new Point(4, 24);
            tabModeration.Margin = new Padding(4, 3, 4, 3);
            tabModeration.Name = "tabModeration";
            tabModeration.Padding = new Padding(4, 3, 4, 3);
            tabModeration.Size = new Size(293, 271);
            tabModeration.TabIndex = 0;
            tabModeration.Text = "Moderation";
            // 
            // nudAdminSprite
            // 
            nudAdminSprite.Location = new Point(93, 240);
            nudAdminSprite.Margin = new Padding(4, 3, 4, 3);
            nudAdminSprite.Name = "nudAdminSprite";
            nudAdminSprite.Size = new Size(56, 23);
            nudAdminSprite.TabIndex = 33;
            // 
            // nudAdminMap
            // 
            nudAdminMap.Location = new Point(93, 205);
            nudAdminMap.Margin = new Padding(4, 3, 4, 3);
            nudAdminMap.Name = "nudAdminMap";
            nudAdminMap.Size = new Size(56, 23);
            nudAdminMap.TabIndex = 32;
            // 
            // btnLevelUp
            // 
            btnLevelUp.Location = new Point(37, 104);
            btnLevelUp.Margin = new Padding(4, 3, 4, 3);
            btnLevelUp.Name = "btnLevelUp";
            btnLevelUp.Padding = new Padding(5);
            btnLevelUp.Size = new Size(219, 25);
            btnLevelUp.TabIndex = 31;
            btnLevelUp.Text = "Level Up";
            btnLevelUp.Click += BtnLevelUp_Click;
            // 
            // cmbAccess
            // 
            cmbAccess.DrawMode = DrawMode.OwnerDrawVariable;
            cmbAccess.FormattingEnabled = true;
            cmbAccess.Items.AddRange(new object[] { "Normal Player", "Moderator (GM)", "Mapper", "Developer", "Owner" });
            cmbAccess.Location = new Point(66, 140);
            cmbAccess.Margin = new Padding(4, 3, 4, 3);
            cmbAccess.Name = "cmbAccess";
            cmbAccess.Size = new Size(215, 24);
            cmbAccess.TabIndex = 17;
            // 
            // tabMapList
            // 
            tabMapList.BackColor = Color.FromArgb(45, 45, 48);
            tabMapList.Controls.Add(lstMaps);
            tabMapList.Controls.Add(btnMapReport);
            tabMapList.Location = new Point(4, 24);
            tabMapList.Margin = new Padding(4, 3, 4, 3);
            tabMapList.Name = "tabMapList";
            tabMapList.Padding = new Padding(4, 3, 4, 3);
            tabMapList.Size = new Size(293, 271);
            tabMapList.TabIndex = 2;
            tabMapList.Text = "Map List";
            // 
            // tabMapTools
            // 
            tabMapTools.BackColor = Color.FromArgb(45, 45, 48);
            tabMapTools.Controls.Add(btnRespawn);
            tabMapTools.Controls.Add(btnALoc);
            tabMapTools.Location = new Point(4, 24);
            tabMapTools.Margin = new Padding(4, 3, 4, 3);
            tabMapTools.Name = "tabMapTools";
            tabMapTools.Padding = new Padding(4, 3, 4, 3);
            tabMapTools.Size = new Size(293, 271);
            tabMapTools.TabIndex = 3;
            tabMapTools.Text = "Map Tools";
            // 
            // tabEditors
            // 
            tabEditors.BackColor = Color.FromArgb(45, 45, 48);
            tabEditors.Controls.Add(btnMoralEditor);
            tabEditors.Controls.Add(btnPetEditor);
            tabEditors.Controls.Add(btnJobEditor);
            tabEditors.Controls.Add(btnProjectiles);
            tabEditors.Controls.Add(btnMapEditor);
            tabEditors.Controls.Add(btnItemEditor);
            tabEditors.Controls.Add(btnResourceEditor);
            tabEditors.Controls.Add(btnNPCEditor);
            tabEditors.Controls.Add(btnSkillEditor);
            tabEditors.Controls.Add(btnShopEditor);
            tabEditors.Controls.Add(btnAnimationEditor);
            tabEditors.Location = new Point(4, 24);
            tabEditors.Margin = new Padding(4, 3, 4, 3);
            tabEditors.Name = "tabEditors";
            tabEditors.Padding = new Padding(4, 3, 4, 3);
            tabEditors.Size = new Size(293, 271);
            tabEditors.TabIndex = 4;
            tabEditors.Text = "Editors";
            // 
            // btnMoralEditor
            // 
            btnMoralEditor.Location = new Point(152, 111);
            btnMoralEditor.Margin = new Padding(4, 3, 4, 3);
            btnMoralEditor.Name = "btnMoralEditor";
            btnMoralEditor.Padding = new Padding(5);
            btnMoralEditor.Size = new Size(131, 29);
            btnMoralEditor.TabIndex = 69;
            btnMoralEditor.Text = "Moral Editor";
            btnMoralEditor.Click += btnMoralEditor_Click;
            // 
            // btnPetEditor
            // 
            btnPetEditor.Location = new Point(152, 6);
            btnPetEditor.Margin = new Padding(4, 3, 4, 3);
            btnPetEditor.Name = "btnPetEditor";
            btnPetEditor.Padding = new Padding(5);
            btnPetEditor.Size = new Size(131, 29);
            btnPetEditor.TabIndex = 68;
            btnPetEditor.Text = "Pet Editor";
            btnPetEditor.Click += btnPetEditor_Click;
            // 
            // btnJobEditor
            // 
            btnJobEditor.Location = new Point(7, 41);
            btnJobEditor.Margin = new Padding(4, 3, 4, 3);
            btnJobEditor.Name = "btnJobEditor";
            btnJobEditor.Padding = new Padding(5);
            btnJobEditor.Size = new Size(131, 29);
            btnJobEditor.TabIndex = 66;
            btnJobEditor.Text = "Job Editor";
            btnJobEditor.Click += btnJobEditor_Click;
            // 
            // btnProjectiles
            // 
            btnProjectiles.Location = new Point(152, 41);
            btnProjectiles.Margin = new Padding(4, 3, 4, 3);
            btnProjectiles.Name = "btnProjectiles";
            btnProjectiles.Padding = new Padding(5);
            btnProjectiles.Size = new Size(131, 29);
            btnProjectiles.TabIndex = 64;
            btnProjectiles.Text = "Projectile Editor";
            btnProjectiles.Click += btnProjectiles_Click;
            // 
            // btnMapEditor
            // 
            btnMapEditor.Location = new Point(8, 181);
            btnMapEditor.Margin = new Padding(4, 3, 4, 3);
            btnMapEditor.Name = "btnMapEditor";
            btnMapEditor.Padding = new Padding(5);
            btnMapEditor.Size = new Size(131, 29);
            btnMapEditor.TabIndex = 55;
            btnMapEditor.Text = "Map Editor";
            btnMapEditor.Click += BtnMapEditor_Click;
            // 
            // btnItemEditor
            // 
            btnItemEditor.Location = new Point(8, 111);
            btnItemEditor.Margin = new Padding(4, 3, 4, 3);
            btnItemEditor.Name = "btnItemEditor";
            btnItemEditor.Padding = new Padding(5);
            btnItemEditor.Size = new Size(131, 29);
            btnItemEditor.TabIndex = 56;
            btnItemEditor.Text = "Item Editor";
            btnItemEditor.Click += btnItemEditor_Click;
            // 
            // btnResourceEditor
            // 
            btnResourceEditor.Location = new Point(152, 76);
            btnResourceEditor.Margin = new Padding(4, 3, 4, 3);
            btnResourceEditor.Name = "btnResourceEditor";
            btnResourceEditor.Padding = new Padding(5);
            btnResourceEditor.Size = new Size(131, 29);
            btnResourceEditor.TabIndex = 57;
            btnResourceEditor.Text = "Resource Editor";
            btnResourceEditor.Click += btnResourceEditor_Click;
            // 
            // btnNPCEditor
            // 
            btnNPCEditor.Location = new Point(8, 76);
            btnNPCEditor.Margin = new Padding(4, 3, 4, 3);
            btnNPCEditor.Name = "btnNPCEditor";
            btnNPCEditor.Padding = new Padding(5);
            btnNPCEditor.Size = new Size(131, 29);
            btnNPCEditor.TabIndex = 58;
            btnNPCEditor.Text = "NPC Editor";
            btnNPCEditor.Click += btnNPCEditor_Click;
            // 
            // btnSkillEditor
            // 
            btnSkillEditor.Location = new Point(8, 146);
            btnSkillEditor.Margin = new Padding(4, 3, 4, 3);
            btnSkillEditor.Name = "btnSkillEditor";
            btnSkillEditor.Padding = new Padding(5);
            btnSkillEditor.Size = new Size(131, 29);
            btnSkillEditor.TabIndex = 59;
            btnSkillEditor.Text = "Skill Editor";
            btnSkillEditor.Click += btnSkillEditor_Click;
            // 
            // btnShopEditor
            // 
            btnShopEditor.Location = new Point(8, 216);
            btnShopEditor.Margin = new Padding(4, 3, 4, 3);
            btnShopEditor.Name = "btnShopEditor";
            btnShopEditor.Padding = new Padding(5);
            btnShopEditor.Size = new Size(131, 29);
            btnShopEditor.TabIndex = 60;
            btnShopEditor.Text = "Shop Editor";
            btnShopEditor.Click += btnShopEditor_Click;
            // 
            // btnAnimationEditor
            // 
            btnAnimationEditor.Location = new Point(8, 6);
            btnAnimationEditor.Margin = new Padding(4, 3, 4, 3);
            btnAnimationEditor.Name = "btnAnimationEditor";
            btnAnimationEditor.Padding = new Padding(5);
            btnAnimationEditor.Size = new Size(131, 29);
            btnAnimationEditor.TabIndex = 61;
            btnAnimationEditor.Text = "Animation Editor";
            btnAnimationEditor.Click += btnAnimationEditor_Click;
            // 
            // FrmAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(304, 299);
            Controls.Add(TabControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
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
        internal DarkButton btnPetEditor;
        internal DarkButton btnJobEditor;
        internal DarkButton btnProjectiles;
        internal DarkButton btnMapEditor;
        internal DarkButton btnItemEditor;
        internal DarkButton btnResourceEditor;
        internal DarkButton btnNPCEditor;
        internal DarkButton btnSkillEditor;
        internal DarkButton btnShopEditor;
        internal DarkButton btnAnimationEditor;
        internal DarkButton btnMoralEditor;
    }
}