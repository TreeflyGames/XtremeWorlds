using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdmin));
            btnRespawn = new Button();
            btnRespawn.Click += new EventHandler(BtnRespawn_Click);
            btnMapReport = new Button();
            btnMapReport.Click += new EventHandler(BtnMapReport_Click);
            btnALoc = new Button();
            btnALoc.Click += new EventHandler(BtnALoc_Click);
            btnAdminSetSprite = new Button();
            btnAdminSetSprite.Click += new EventHandler(BtnAdminSetSprite_Click);
            btnAdminWarpTo = new Button();
            btnAdminWarpTo.Click += new EventHandler(BtnAdminWarpTo_Click);
            Label5 = new Label();
            Label4 = new Label();
            btnAdminSetAccess = new Button();
            btnAdminSetAccess.Click += new EventHandler(BtnAdminSetAccess_Click);
            btnAdminWarpMe2 = new Button();
            btnAdminWarpMe2.Click += new EventHandler(BtnAdminWarpMe2_Click);
            btnAdminWarp2Me = new Button();
            btnAdminWarp2Me.Click += new EventHandler(BtnAdminWarp2Me_Click);
            btnAdminBan = new Button();
            btnAdminBan.Click += new EventHandler(BtnAdminBan_Click);
            btnAdminKick = new Button();
            btnAdminKick.Click += new EventHandler(BtnAdminKick_Click);
            txtAdminName = new TextBox();
            Label3 = new Label();
            Label2 = new Label();
            lstMaps = new ListView();
            lstMaps.DoubleClick += new EventHandler(LstMaps_DoubleClick);
            ColumnHeader1 = new ColumnHeader();
            ColumnHeader2 = new ColumnHeader();
            TabControl1 = new TabControl();
            tabModeration = new TabPage();
            nudAdminSprite = new NumericUpDown();
            nudAdminMap = new NumericUpDown();
            btnLevelUp = new Button();
            btnLevelUp.Click += new EventHandler(BtnLevelUp_Click);
            cmbAccess = new ComboBox();
            tabMapList = new TabPage();
            tabMapTools = new TabPage();
            tabEditors = new TabPage();
            btnMoralEditor = new Button();
            btnMoralEditor.Click += new EventHandler(btnMoralEditor_Click);
            btnPetEditor = new Button();
            btnPetEditor.Click += new EventHandler(btnPetEditor_Click);
            btnJobEditor = new Button();
            btnJobEditor.Click += new EventHandler(btnJobEditor_Click);
            btnProjectiles = new Button();
            btnProjectiles.Click += new EventHandler(btnProjectiles_Click);
            btnMapEditor = new Button();
            btnMapEditor.Click += new EventHandler(BtnMapEditor_Click);
            btnItemEditor = new Button();
            btnItemEditor.Click += new EventHandler(btnItemEditor_Click);
            btnResourceEditor = new Button();
            btnResourceEditor.Click += new EventHandler(btnResourceEditor_Click);
            btnNPCEditor = new Button();
            btnNPCEditor.Click += new EventHandler(btnNPCEditor_Click);
            btnSkillEditor = new Button();
            btnSkillEditor.Click += new EventHandler(btnSkillEditor_Click);
            btnShopEditor = new Button();
            btnShopEditor.Click += new EventHandler(btnShopEditor_Click);
            btnAnimationEditor = new Button();
            btnAnimationEditor.Click += new EventHandler(btnAnimationEditor_Click);
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
            btnRespawn.Location = new Point(227, 30);
            btnRespawn.Margin = new Padding(5);
            btnRespawn.Name = "btnRespawn";
            btnRespawn.Size = new Size(177, 41);
            btnRespawn.TabIndex = 34;
            btnRespawn.Text = "Respawn Map";
            btnRespawn.UseVisualStyleBackColor = true;
            // 
            // btnMapReport
            // 
            btnMapReport.Location = new Point(10, 402);
            btnMapReport.Margin = new Padding(5);
            btnMapReport.Name = "btnMapReport";
            btnMapReport.Size = new Size(397, 41);
            btnMapReport.TabIndex = 33;
            btnMapReport.Text = "Refresh List";
            btnMapReport.UseVisualStyleBackColor = true;
            // 
            // btnALoc
            // 
            btnALoc.Location = new Point(23, 30);
            btnALoc.Margin = new Padding(5);
            btnALoc.Name = "btnALoc";
            btnALoc.Size = new Size(177, 41);
            btnALoc.TabIndex = 31;
            btnALoc.Text = "Location";
            btnALoc.UseVisualStyleBackColor = true;
            // 
            // btnAdminSetSprite
            // 
            btnAdminSetSprite.Location = new Point(223, 391);
            btnAdminSetSprite.Margin = new Padding(5);
            btnAdminSetSprite.Name = "btnAdminSetSprite";
            btnAdminSetSprite.Size = new Size(180, 47);
            btnAdminSetSprite.TabIndex = 16;
            btnAdminSetSprite.Text = "Set Player Sprite";
            btnAdminSetSprite.UseVisualStyleBackColor = true;
            // 
            // btnAdminWarpTo
            // 
            btnAdminWarpTo.Location = new Point(223, 338);
            btnAdminWarpTo.Margin = new Padding(5);
            btnAdminWarpTo.Name = "btnAdminWarpTo";
            btnAdminWarpTo.Size = new Size(180, 47);
            btnAdminWarpTo.TabIndex = 15;
            btnAdminWarpTo.Text = "Warp To Map";
            btnAdminWarpTo.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(10, 403);
            Label5.Margin = new Padding(5, 0, 5, 0);
            Label5.Name = "Label5";
            Label5.Size = new Size(62, 25);
            Label5.TabIndex = 13;
            Label5.Text = "Sprite:";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(10, 350);
            Label4.Margin = new Padding(5, 0, 5, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(122, 25);
            Label4.TabIndex = 11;
            Label4.Text = "Map Number:";
            // 
            // btnAdminSetAccess
            // 
            btnAdminSetAccess.Location = new Point(15, 285);
            btnAdminSetAccess.Margin = new Padding(5);
            btnAdminSetAccess.Name = "btnAdminSetAccess";
            btnAdminSetAccess.Size = new Size(388, 41);
            btnAdminSetAccess.TabIndex = 9;
            btnAdminSetAccess.Text = "Set Access";
            btnAdminSetAccess.UseVisualStyleBackColor = true;
            // 
            // btnAdminWarpMe2
            // 
            btnAdminWarpMe2.Location = new Point(212, 120);
            btnAdminWarpMe2.Margin = new Padding(5);
            btnAdminWarpMe2.Name = "btnAdminWarpMe2";
            btnAdminWarpMe2.Size = new Size(192, 41);
            btnAdminWarpMe2.TabIndex = 8;
            btnAdminWarpMe2.Text = "Warp Me To Player";
            btnAdminWarpMe2.UseVisualStyleBackColor = true;
            // 
            // btnAdminWarp2Me
            // 
            btnAdminWarp2Me.Location = new Point(10, 120);
            btnAdminWarp2Me.Margin = new Padding(5);
            btnAdminWarp2Me.Name = "btnAdminWarp2Me";
            btnAdminWarp2Me.Size = new Size(192, 41);
            btnAdminWarp2Me.TabIndex = 7;
            btnAdminWarp2Me.Text = "Warp Player To Me";
            btnAdminWarp2Me.UseVisualStyleBackColor = true;
            // 
            // btnAdminBan
            // 
            btnAdminBan.Location = new Point(212, 65);
            btnAdminBan.Margin = new Padding(5);
            btnAdminBan.Name = "btnAdminBan";
            btnAdminBan.Size = new Size(192, 41);
            btnAdminBan.TabIndex = 6;
            btnAdminBan.Text = "Ban Player";
            btnAdminBan.UseVisualStyleBackColor = true;
            // 
            // btnAdminKick
            // 
            btnAdminKick.Location = new Point(10, 65);
            btnAdminKick.Margin = new Padding(5);
            btnAdminKick.Name = "btnAdminKick";
            btnAdminKick.Size = new Size(192, 41);
            btnAdminKick.TabIndex = 5;
            btnAdminKick.Text = "Kick Player";
            btnAdminKick.UseVisualStyleBackColor = true;
            // 
            // txtAdminName
            // 
            txtAdminName.Location = new Point(137, 15);
            txtAdminName.Margin = new Padding(5);
            txtAdminName.Name = "txtAdminName";
            txtAdminName.Size = new Size(264, 31);
            txtAdminName.TabIndex = 3;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(10, 238);
            Label3.Margin = new Padding(5, 0, 5, 0);
            Label3.Name = "Label3";
            Label3.Size = new Size(69, 25);
            Label3.TabIndex = 2;
            Label3.Text = "Access:";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(10, 22);
            Label2.Margin = new Padding(5, 0, 5, 0);
            Label2.Name = "Label2";
            Label2.Size = new Size(115, 25);
            Label2.TabIndex = 1;
            Label2.Text = "Player Name:";
            // 
            // lstMaps
            // 
            lstMaps.Columns.AddRange(new ColumnHeader[] { ColumnHeader1, ColumnHeader2 });
            lstMaps.FullRowSelect = true;
            lstMaps.GridLines = true;
            lstMaps.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstMaps.Location = new Point(10, 12);
            lstMaps.Margin = new Padding(5);
            lstMaps.MultiSelect = false;
            lstMaps.Name = "lstMaps";
            lstMaps.Size = new Size(396, 376);
            lstMaps.TabIndex = 4;
            lstMaps.UseCompatibleStateImageBehavior = false;
            lstMaps.View = View.Details;
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
            TabControl1.Location = new Point(3, 3);
            TabControl1.Margin = new Padding(5);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(430, 498);
            TabControl1.TabIndex = 38;
            // 
            // tabModeration
            // 
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
            tabModeration.Location = new Point(4, 34);
            tabModeration.Margin = new Padding(5);
            tabModeration.Name = "tabModeration";
            tabModeration.Padding = new Padding(5);
            tabModeration.Size = new Size(422, 460);
            tabModeration.TabIndex = 0;
            tabModeration.Text = "Moderation";
            tabModeration.UseVisualStyleBackColor = true;
            // 
            // nudAdminSprite
            // 
            nudAdminSprite.Location = new Point(133, 400);
            nudAdminSprite.Margin = new Padding(5);
            nudAdminSprite.Name = "nudAdminSprite";
            nudAdminSprite.Size = new Size(80, 31);
            nudAdminSprite.TabIndex = 33;
            // 
            // nudAdminMap
            // 
            nudAdminMap.Location = new Point(133, 341);
            nudAdminMap.Margin = new Padding(5);
            nudAdminMap.Name = "nudAdminMap";
            nudAdminMap.Size = new Size(80, 31);
            nudAdminMap.TabIndex = 32;
            // 
            // btnLevelUp
            // 
            btnLevelUp.Location = new Point(53, 173);
            btnLevelUp.Margin = new Padding(5);
            btnLevelUp.Name = "btnLevelUp";
            btnLevelUp.Size = new Size(313, 41);
            btnLevelUp.TabIndex = 31;
            btnLevelUp.Text = "Level Up";
            btnLevelUp.UseVisualStyleBackColor = true;
            // 
            // cmbAccess
            // 
            cmbAccess.FormattingEnabled = true;
            cmbAccess.Items.AddRange(new object[] { "Normal Player", "Moderator (GM)", "Mapper", "Developer", "Owner" });
            cmbAccess.Location = new Point(95, 234);
            cmbAccess.Margin = new Padding(5);
            cmbAccess.Name = "cmbAccess";
            cmbAccess.Size = new Size(306, 33);
            cmbAccess.TabIndex = 17;
            // 
            // tabMapList
            // 
            tabMapList.Controls.Add(lstMaps);
            tabMapList.Controls.Add(btnMapReport);
            tabMapList.Location = new Point(4, 34);
            tabMapList.Margin = new Padding(5);
            tabMapList.Name = "tabMapList";
            tabMapList.Padding = new Padding(5);
            tabMapList.Size = new Size(422, 460);
            tabMapList.TabIndex = 2;
            tabMapList.Text = "Map List";
            tabMapList.UseVisualStyleBackColor = true;
            // 
            // tabMapTools
            // 
            tabMapTools.Controls.Add(btnRespawn);
            tabMapTools.Controls.Add(btnALoc);
            tabMapTools.Location = new Point(4, 34);
            tabMapTools.Margin = new Padding(5);
            tabMapTools.Name = "tabMapTools";
            tabMapTools.Padding = new Padding(5);
            tabMapTools.Size = new Size(422, 460);
            tabMapTools.TabIndex = 3;
            tabMapTools.Text = "Map Tools";
            tabMapTools.UseVisualStyleBackColor = true;
            // 
            // tabEditors
            // 
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
            tabEditors.Location = new Point(4, 34);
            tabEditors.Margin = new Padding(5);
            tabEditors.Name = "tabEditors";
            tabEditors.Padding = new Padding(5);
            tabEditors.Size = new Size(422, 460);
            tabEditors.TabIndex = 4;
            tabEditors.Text = "Editors";
            tabEditors.UseVisualStyleBackColor = true;
            // 
            // btnMoralEditor
            // 
            btnMoralEditor.Location = new Point(217, 185);
            btnMoralEditor.Margin = new Padding(5);
            btnMoralEditor.Name = "btnMoralEditor";
            btnMoralEditor.Size = new Size(187, 48);
            btnMoralEditor.TabIndex = 69;
            btnMoralEditor.Text = "Moral Editor";
            btnMoralEditor.UseVisualStyleBackColor = true;
            // 
            // btnPetEditor
            // 
            btnPetEditor.Location = new Point(217, 10);
            btnPetEditor.Margin = new Padding(5);
            btnPetEditor.Name = "btnPetEditor";
            btnPetEditor.Size = new Size(187, 48);
            btnPetEditor.TabIndex = 68;
            btnPetEditor.Text = "Pet Editor";
            btnPetEditor.UseVisualStyleBackColor = true;
            // 
            // btnJobEditor
            // 
            btnJobEditor.Location = new Point(10, 68);
            btnJobEditor.Margin = new Padding(5);
            btnJobEditor.Name = "btnJobEditor";
            btnJobEditor.Size = new Size(187, 48);
            btnJobEditor.TabIndex = 66;
            btnJobEditor.Text = "Job Editor";
            btnJobEditor.UseVisualStyleBackColor = true;
            // 
            // btnProjectiles
            // 
            btnProjectiles.Location = new Point(217, 68);
            btnProjectiles.Margin = new Padding(5);
            btnProjectiles.Name = "btnProjectiles";
            btnProjectiles.Size = new Size(187, 48);
            btnProjectiles.TabIndex = 64;
            btnProjectiles.Text = "Projectile Editor";
            btnProjectiles.UseVisualStyleBackColor = true;
            // 
            // btnMapEditor
            // 
            btnMapEditor.Location = new Point(12, 302);
            btnMapEditor.Margin = new Padding(5);
            btnMapEditor.Name = "btnMapEditor";
            btnMapEditor.Size = new Size(187, 48);
            btnMapEditor.TabIndex = 55;
            btnMapEditor.Text = "Map Editor";
            btnMapEditor.UseVisualStyleBackColor = true;
            // 
            // btnItemEditor
            // 
            btnItemEditor.Location = new Point(12, 185);
            btnItemEditor.Margin = new Padding(5);
            btnItemEditor.Name = "btnItemEditor";
            btnItemEditor.Size = new Size(187, 48);
            btnItemEditor.TabIndex = 56;
            btnItemEditor.Text = "Item Editor";
            btnItemEditor.UseVisualStyleBackColor = true;
            // 
            // btnResourceEditor
            // 
            btnResourceEditor.Location = new Point(217, 127);
            btnResourceEditor.Margin = new Padding(5);
            btnResourceEditor.Name = "btnResourceEditor";
            btnResourceEditor.Size = new Size(187, 48);
            btnResourceEditor.TabIndex = 57;
            btnResourceEditor.Text = "Resource Editor";
            btnResourceEditor.UseVisualStyleBackColor = true;
            // 
            // btnNPCEditor
            // 
            btnNPCEditor.Location = new Point(12, 127);
            btnNPCEditor.Margin = new Padding(5);
            btnNPCEditor.Name = "btnNPCEditor";
            btnNPCEditor.Size = new Size(187, 48);
            btnNPCEditor.TabIndex = 58;
            btnNPCEditor.Text = "NPC Editor";
            btnNPCEditor.UseVisualStyleBackColor = true;
            // 
            // btnSkillEditor
            // 
            btnSkillEditor.Location = new Point(12, 243);
            btnSkillEditor.Margin = new Padding(5);
            btnSkillEditor.Name = "btnSkillEditor";
            btnSkillEditor.Size = new Size(187, 48);
            btnSkillEditor.TabIndex = 59;
            btnSkillEditor.Text = "Skill Editor";
            btnSkillEditor.UseVisualStyleBackColor = true;
            // 
            // btnShopEditor
            // 
            btnShopEditor.Location = new Point(12, 360);
            btnShopEditor.Margin = new Padding(5);
            btnShopEditor.Name = "btnShopEditor";
            btnShopEditor.Size = new Size(187, 48);
            btnShopEditor.TabIndex = 60;
            btnShopEditor.Text = "Shop Editor";
            btnShopEditor.UseVisualStyleBackColor = true;
            // 
            // btnAnimationEditor
            // 
            btnAnimationEditor.Location = new Point(12, 10);
            btnAnimationEditor.Margin = new Padding(5);
            btnAnimationEditor.Name = "btnAnimationEditor";
            btnAnimationEditor.Size = new Size(187, 48);
            btnAnimationEditor.TabIndex = 61;
            btnAnimationEditor.Text = "Animation Editor";
            btnAnimationEditor.UseVisualStyleBackColor = true;
            // 
            // FrmAdmin
            // 
            AutoScaleDimensions = new SizeF(10.0f, 25.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 498);
            Controls.Add(TabControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAdmin";
            ShowIcon = false;
            Text = "Admin Panel";
            TabControl1.ResumeLayout(false);
            tabModeration.ResumeLayout(false);
            tabModeration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAdminSprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAdminMap).EndInit();
            tabMapList.ResumeLayout(false);
            tabMapTools.ResumeLayout(false);
            tabEditors.ResumeLayout(false);
            Load += new EventHandler(FrmAdmin_Load);
            ResumeLayout(false);

        }

        internal Button btnRespawn;
        internal Button btnMapReport;
        internal Button btnALoc;
        internal Button btnAdminSetSprite;
        internal Button btnAdminWarpTo;
        internal Label Label5;
        internal Label Label4;
        internal Button btnAdminSetAccess;
        internal Button btnAdminWarpMe2;
        internal Button btnAdminWarp2Me;
        internal Button btnAdminBan;
        internal Button btnAdminKick;
        internal TextBox txtAdminName;
        internal Label Label3;
        internal Label Label2;
        internal ListView lstMaps;
        internal ColumnHeader ColumnHeader1;
        internal ColumnHeader ColumnHeader2;
        internal TabControl TabControl1;
        internal TabPage tabModeration;
        internal TabPage tabMapList;
        internal TabPage tabMapTools;
        internal ComboBox cmbAccess;
        internal NumericUpDown nudAdminSprite;
        internal NumericUpDown nudAdminMap;
        internal Button btnLevelUp;
        internal TabPage tabEditors;
        internal Button btnPetEditor;
        internal Button btnJobEditor;
        internal Button btnProjectiles;
        internal Button btnMapEditor;
        internal Button btnItemEditor;
        internal Button btnResourceEditor;
        internal Button btnNPCEditor;
        internal Button btnSkillEditor;
        internal Button btnShopEditor;
        internal Button btnAnimationEditor;
        internal Button btnMoralEditor;
    }
}