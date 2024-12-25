using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_Resource : Form
    {

        // Shared instance of the form
        private static frmEditor_Resource _instance;

        // Public property to get the shared instance
        public static frmEditor_Resource Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Resource();
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
            DarkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            lstIndex = new ListBox();
            lstIndex.Click += new EventHandler(lstIndex_Click);
            DarkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
            DarkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            nudRewardExp = new DarkUI.Controls.DarkNumericUpDown();
            nudRewardExp.ValueChanged += new EventHandler(ScrlRewardExp_Scroll);
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            cmbRewardItem = new DarkUI.Controls.DarkComboBox();
            cmbRewardItem.SelectedIndexChanged += new EventHandler(ScrlRewardItem_Scroll);
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            nudLvlReq = new DarkUI.Controls.DarkNumericUpDown();
            nudLvlReq.ValueChanged += new EventHandler(ScrlLvlReq_Scroll);
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            cmbTool = new DarkUI.Controls.DarkComboBox();
            cmbTool.SelectedIndexChanged += new EventHandler(CmbTool_SelectedIndexChanged);
            DarkLabel10 = new DarkUI.Controls.DarkLabel();
            cmbAnimation = new DarkUI.Controls.DarkComboBox();
            cmbAnimation.SelectedIndexChanged += new EventHandler(ScrlAnim_Scroll);
            DarkLabel9 = new DarkUI.Controls.DarkLabel();
            nudRespawn = new DarkUI.Controls.DarkNumericUpDown();
            nudRespawn.ValueChanged += new EventHandler(ScrlRespawn_Scroll);
            DarkLabel8 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            nudExhaustedPic = new DarkUI.Controls.DarkNumericUpDown();
            nudExhaustedPic.ValueChanged += new EventHandler(ScrlExhaustedPic_Scroll);
            nudNormalPic = new DarkUI.Controls.DarkNumericUpDown();
            nudNormalPic.ValueChanged += new EventHandler(ScrlNormalPic_Scroll);
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            picExhaustedPic = new PictureBox();
            picNormalpic = new PictureBox();
            nudHealth = new DarkUI.Controls.DarkNumericUpDown();
            nudHealth.ValueChanged += new EventHandler(ScrlHealth_Scroll);
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            cmbType = new DarkUI.Controls.DarkComboBox();
            cmbType.SelectedIndexChanged += new EventHandler(CmbType_SelectedIndexChanged);
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            txtMessage2 = new DarkUI.Controls.DarkTextBox();
            txtMessage2.TextChanged += new EventHandler(TxtMessage2_TextChanged);
            txtMessage = new DarkUI.Controls.DarkTextBox();
            txtMessage.TextChanged += new EventHandler(TxtMessage_TextChanged);
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            txtName = new DarkUI.Controls.DarkTextBox();
            txtName.TextChanged += new EventHandler(TxtName_TextChanged);
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            btnSave = new DarkUI.Controls.DarkButton();
            btnSave.Click += new EventHandler(BtnSave_Click);
            btnDelete = new DarkUI.Controls.DarkButton();
            btnDelete.Click += new EventHandler(BtnDelete_Click);
            btnCancel = new DarkUI.Controls.DarkButton();
            btnCancel.Click += new EventHandler(BtnCancel_Click);
            DarkGroupBox1.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            DarkGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRewardExp).BeginInit();
            DarkGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLvlReq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRespawn).BeginInit();
            DarkGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudExhaustedPic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNormalPic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picExhaustedPic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picNormalpic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHealth).BeginInit();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(8, 4);
            DarkGroupBox1.Margin = new Padding(8, 6, 8, 6);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(8, 6, 8, 6);
            DarkGroupBox1.Size = new Size(468, 815);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Resource List";
            // 
            // lstIndex
            // 
            lstIndex.BackColor = Color.FromArgb(45, 45, 48);
            lstIndex.BorderStyle = BorderStyle.FixedSingle;
            lstIndex.ForeColor = Color.Gainsboro;
            lstIndex.FormattingEnabled = true;
            lstIndex.Location = new Point(18, 47);
            lstIndex.Margin = new Padding(8, 6, 8, 6);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(422, 738);
            lstIndex.TabIndex = 1;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(DarkGroupBox5);
            DarkGroupBox2.Controls.Add(DarkGroupBox4);
            DarkGroupBox2.Controls.Add(cmbAnimation);
            DarkGroupBox2.Controls.Add(DarkLabel9);
            DarkGroupBox2.Controls.Add(nudRespawn);
            DarkGroupBox2.Controls.Add(DarkLabel8);
            DarkGroupBox2.Controls.Add(DarkGroupBox3);
            DarkGroupBox2.Controls.Add(nudHealth);
            DarkGroupBox2.Controls.Add(DarkLabel5);
            DarkGroupBox2.Controls.Add(cmbType);
            DarkGroupBox2.Controls.Add(DarkLabel4);
            DarkGroupBox2.Controls.Add(DarkLabel3);
            DarkGroupBox2.Controls.Add(txtMessage2);
            DarkGroupBox2.Controls.Add(txtMessage);
            DarkGroupBox2.Controls.Add(DarkLabel2);
            DarkGroupBox2.Controls.Add(txtName);
            DarkGroupBox2.Controls.Add(DarkLabel1);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(486, 4);
            DarkGroupBox2.Margin = new Padding(8, 6, 8, 6);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(8, 6, 8, 6);
            DarkGroupBox2.Size = new Size(791, 1030);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // DarkGroupBox5
            // 
            DarkGroupBox5.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox5.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox5.Controls.Add(nudRewardExp);
            DarkGroupBox5.Controls.Add(DarkLabel13);
            DarkGroupBox5.Controls.Add(cmbRewardItem);
            DarkGroupBox5.Controls.Add(DarkLabel12);
            DarkGroupBox5.ForeColor = Color.Gainsboro;
            DarkGroupBox5.Location = new Point(18, 908);
            DarkGroupBox5.Margin = new Padding(8, 6, 8, 6);
            DarkGroupBox5.Name = "DarkGroupBox5";
            DarkGroupBox5.Padding = new Padding(8, 6, 8, 6);
            DarkGroupBox5.Size = new Size(750, 108);
            DarkGroupBox5.TabIndex = 16;
            DarkGroupBox5.TabStop = false;
            DarkGroupBox5.Text = "Rewards";
            // 
            // nudRewardExp
            // 
            nudRewardExp.Location = new Point(522, 34);
            nudRewardExp.Margin = new Padding(8, 6, 8, 6);
            nudRewardExp.Name = "nudRewardExp";
            nudRewardExp.Size = new Size(212, 39);
            nudRewardExp.TabIndex = 3;
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(373, 38);
            DarkLabel13.Margin = new Padding(8, 0, 8, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(134, 32);
            DarkLabel13.TabIndex = 2;
            DarkLabel13.Text = "Exp:";
            // 
            // cmbRewardItem
            // 
            cmbRewardItem.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRewardItem.FormattingEnabled = true;
            cmbRewardItem.Location = new Point(91, 32);
            cmbRewardItem.Margin = new Padding(8, 6, 8, 6);
            cmbRewardItem.Name = "cmbRewardItem";
            cmbRewardItem.Size = new Size(256, 40);
            cmbRewardItem.TabIndex = 1;
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(13, 38);
            DarkLabel12.Margin = new Padding(8, 0, 8, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(67, 32);
            DarkLabel12.TabIndex = 0;
            DarkLabel12.Text = "Item:";
            // 
            // DarkGroupBox4
            // 
            DarkGroupBox4.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox4.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox4.Controls.Add(nudLvlReq);
            DarkGroupBox4.Controls.Add(DarkLabel11);
            DarkGroupBox4.Controls.Add(cmbTool);
            DarkGroupBox4.Controls.Add(DarkLabel10);
            DarkGroupBox4.ForeColor = Color.Gainsboro;
            DarkGroupBox4.Location = new Point(18, 785);
            DarkGroupBox4.Margin = new Padding(8, 6, 8, 6);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(8, 6, 8, 6);
            DarkGroupBox4.Size = new Size(750, 108);
            DarkGroupBox4.TabIndex = 15;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Requirements";
            // 
            // nudLvlReq
            // 
            nudLvlReq.Location = new Point(557, 34);
            nudLvlReq.Margin = new Padding(8, 6, 8, 6);
            nudLvlReq.Name = "nudLvlReq";
            nudLvlReq.Size = new Size(178, 39);
            nudLvlReq.TabIndex = 3;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(418, 38);
            DarkLabel11.Margin = new Padding(8, 0, 8, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(124, 32);
            DarkLabel11.TabIndex = 2;
            DarkLabel11.Text = "Skill Level:";
            // 
            // cmbTool
            // 
            cmbTool.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTool.FormattingEnabled = true;
            cmbTool.Items.AddRange(new object[] { "None", "Hatchet", "Pickaxe", "Fishing Rod" });
            cmbTool.Location = new Point(182, 32);
            cmbTool.Margin = new Padding(8, 6, 8, 6);
            cmbTool.Name = "cmbTool";
            cmbTool.Size = new Size(217, 40);
            cmbTool.TabIndex = 1;
            // 
            // DarkLabel10
            // 
            DarkLabel10.AutoSize = true;
            DarkLabel10.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel10.Location = new Point(13, 38);
            DarkLabel10.Margin = new Padding(8, 0, 8, 0);
            DarkLabel10.Name = "DarkLabel10";
            DarkLabel10.Size = new Size(156, 32);
            DarkLabel10.TabIndex = 0;
            DarkLabel10.Text = "Tool Needed:";
            // 
            // cmbAnimation
            // 
            cmbAnimation.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAnimation.FormattingEnabled = true;
            cmbAnimation.Location = new Point(554, 300);
            cmbAnimation.Margin = new Padding(8, 6, 8, 6);
            cmbAnimation.Name = "cmbAnimation";
            cmbAnimation.Size = new Size(212, 40);
            cmbAnimation.TabIndex = 14;
            // 
            // DarkLabel9
            // 
            DarkLabel9.AutoSize = true;
            DarkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel9.Location = new Point(418, 305);
            DarkLabel9.Margin = new Padding(8, 0, 8, 0);
            DarkLabel9.Name = "DarkLabel9";
            DarkLabel9.Size = new Size(129, 32);
            DarkLabel9.TabIndex = 13;
            DarkLabel9.Text = "Animation:";
            // 
            // nudRespawn
            // 
            nudRespawn.Location = new Point(238, 300);
            nudRespawn.Margin = new Padding(8, 6, 8, 6);
            nudRespawn.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudRespawn.Name = "nudRespawn";
            nudRespawn.Size = new Size(167, 39);
            nudRespawn.TabIndex = 12;
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(13, 305);
            DarkLabel8.Margin = new Padding(8, 0, 8, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(172, 32);
            DarkLabel8.TabIndex = 11;
            DarkLabel8.Text = "Respawn Time:";
            // 
            // DarkGroupBox3
            // 
            DarkGroupBox3.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox3.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox3.Controls.Add(nudExhaustedPic);
            DarkGroupBox3.Controls.Add(nudNormalPic);
            DarkGroupBox3.Controls.Add(DarkLabel7);
            DarkGroupBox3.Controls.Add(DarkLabel6);
            DarkGroupBox3.Controls.Add(picExhaustedPic);
            DarkGroupBox3.Controls.Add(picNormalpic);
            DarkGroupBox3.ForeColor = Color.Gainsboro;
            DarkGroupBox3.Location = new Point(18, 367);
            DarkGroupBox3.Margin = new Padding(8, 6, 8, 6);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(8, 6, 8, 6);
            DarkGroupBox3.Size = new Size(750, 404);
            DarkGroupBox3.TabIndex = 10;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Graphics";
            // 
            // nudExhaustedPic
            // 
            nudExhaustedPic.Location = new Point(580, 337);
            nudExhaustedPic.Margin = new Padding(8, 6, 8, 6);
            nudExhaustedPic.Name = "nudExhaustedPic";
            nudExhaustedPic.Size = new Size(156, 39);
            nudExhaustedPic.TabIndex = 49;
            // 
            // nudNormalPic
            // 
            nudNormalPic.Location = new Point(186, 337);
            nudNormalPic.Margin = new Padding(8, 6, 8, 6);
            nudNormalPic.Name = "nudNormalPic";
            nudNormalPic.Size = new Size(156, 39);
            nudNormalPic.TabIndex = 48;
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(401, 341);
            DarkLabel7.Margin = new Padding(8, 0, 8, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(159, 32);
            DarkLabel7.TabIndex = 47;
            DarkLabel7.Text = "Empty Image:";
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(8, 341);
            DarkLabel6.Margin = new Padding(8, 0, 8, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(171, 32);
            DarkLabel6.TabIndex = 46;
            DarkLabel6.Text = "Normal Image:";
            // 
            // picExhaustedPic
            // 
            picExhaustedPic.BackColor = Color.Black;
            picExhaustedPic.BackgroundImageLayout = ImageLayout.Zoom;
            picExhaustedPic.Location = new Point(407, 47);
            picExhaustedPic.Margin = new Padding(8, 6, 8, 6);
            picExhaustedPic.Name = "picExhaustedPic";
            picExhaustedPic.Size = new Size(329, 276);
            picExhaustedPic.TabIndex = 45;
            picExhaustedPic.TabStop = false;
            // 
            // picNormalpic
            // 
            picNormalpic.BackColor = Color.Black;
            picNormalpic.BackgroundImageLayout = ImageLayout.Zoom;
            picNormalpic.Location = new Point(13, 47);
            picNormalpic.Margin = new Padding(8, 6, 8, 6);
            picNormalpic.Name = "picNormalpic";
            picNormalpic.Size = new Size(329, 276);
            picNormalpic.TabIndex = 44;
            picNormalpic.TabStop = false;
            // 
            // nudHealth
            // 
            nudHealth.Location = new Point(637, 236);
            nudHealth.Margin = new Padding(8, 6, 8, 6);
            nudHealth.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudHealth.Name = "nudHealth";
            nudHealth.Size = new Size(132, 39);
            nudHealth.TabIndex = 9;
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(511, 241);
            DarkLabel5.Margin = new Padding(8, 0, 8, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(114, 32);
            DarkLabel5.TabIndex = 8;
            DarkLabel5.Text = "HitPoints:";
            // 
            // cmbType
            // 
            cmbType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "None", "Herb", "Tree", "Mine", "Fishing Spot" });
            cmbType.Location = new Point(236, 235);
            cmbType.Margin = new Padding(8, 6, 8, 6);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(256, 40);
            cmbType.TabIndex = 7;
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(13, 241);
            DarkLabel4.Margin = new Padding(8, 0, 8, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(70, 32);
            DarkLabel4.TabIndex = 6;
            DarkLabel4.Text = "Type:";
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(13, 175);
            DarkLabel3.Margin = new Padding(8, 0, 8, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(155, 32);
            DarkLabel3.TabIndex = 5;
            DarkLabel3.Text = "Fail Message:";
            // 
            // txtMessage2
            // 
            txtMessage2.BackColor = Color.FromArgb(69, 73, 74);
            txtMessage2.BorderStyle = BorderStyle.FixedSingle;
            txtMessage2.ForeColor = Color.FromArgb(220, 220, 220);
            txtMessage2.Location = new Point(236, 171);
            txtMessage2.Margin = new Padding(8, 6, 8, 6);
            txtMessage2.Name = "txtMessage2";
            txtMessage2.Size = new Size(531, 39);
            txtMessage2.TabIndex = 4;
            // 
            // txtMessage
            // 
            txtMessage.BackColor = Color.FromArgb(69, 73, 74);
            txtMessage.BorderStyle = BorderStyle.FixedSingle;
            txtMessage.ForeColor = Color.FromArgb(220, 220, 220);
            txtMessage.Location = new Point(236, 107);
            txtMessage.Margin = new Padding(8, 6, 8, 6);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(531, 39);
            txtMessage.TabIndex = 3;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(13, 111);
            DarkLabel2.Margin = new Padding(8, 0, 8, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(202, 32);
            DarkLabel2.TabIndex = 2;
            DarkLabel2.Text = "Success Message:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(236, 43);
            txtName.Margin = new Padding(8, 6, 8, 6);
            txtName.Name = "txtName";
            txtName.Size = new Size(531, 39);
            txtName.TabIndex = 1;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(13, 47);
            DarkLabel1.Margin = new Padding(8, 0, 8, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(83, 32);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(26, 836);
            btnSave.Margin = new Padding(8, 6, 8, 6);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(11, 12, 11, 12);
            btnSave.Size = new Size(424, 58);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(26, 906);
            btnDelete.Margin = new Padding(8, 6, 8, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(11, 12, 11, 12);
            btnDelete.Size = new Size(424, 58);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(26, 976);
            btnCancel.Margin = new Padding(8, 6, 8, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(11, 12, 11, 12);
            btnCancel.Size = new Size(422, 58);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            // 
            // frmEditor_Resource
            // 
            AutoScaleDimensions = new SizeF(13.0f, 32.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1286, 1047);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(8, 6, 8, 6);
            Name = "frmEditor_Resource";
            Text = "Resource Editor";
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            DarkGroupBox5.ResumeLayout(false);
            DarkGroupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRewardExp).EndInit();
            DarkGroupBox4.ResumeLayout(false);
            DarkGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLvlReq).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRespawn).EndInit();
            DarkGroupBox3.ResumeLayout(false);
            DarkGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudExhaustedPic).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNormalPic).EndInit();
            ((System.ComponentModel.ISupportInitialize)picExhaustedPic).EndInit();
            ((System.ComponentModel.ISupportInitialize)picNormalpic).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHealth).EndInit();
            Load += new EventHandler(frmEditor_Resource_Load);
            FormClosing += new FormClosingEventHandler(frmEditor_Resource_FormClosing);
            ResumeLayout(false);

        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkLabel DarkLabel3;
        internal DarkUI.Controls.DarkTextBox txtMessage2;
        internal DarkUI.Controls.DarkTextBox txtMessage;
        internal DarkUI.Controls.DarkLabel DarkLabel2;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal DarkUI.Controls.DarkComboBox cmbType;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal DarkUI.Controls.DarkNumericUpDown nudHealth;
        internal DarkUI.Controls.DarkLabel DarkLabel5;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox3;
        internal PictureBox picExhaustedPic;
        internal PictureBox picNormalpic;
        internal DarkUI.Controls.DarkNumericUpDown nudExhaustedPic;
        internal DarkUI.Controls.DarkNumericUpDown nudNormalPic;
        internal DarkUI.Controls.DarkLabel DarkLabel7;
        internal DarkUI.Controls.DarkLabel DarkLabel6;
        internal DarkUI.Controls.DarkNumericUpDown nudRespawn;
        internal DarkUI.Controls.DarkLabel DarkLabel8;
        internal DarkUI.Controls.DarkComboBox cmbAnimation;
        internal DarkUI.Controls.DarkLabel DarkLabel9;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox4;
        internal DarkUI.Controls.DarkComboBox cmbTool;
        internal DarkUI.Controls.DarkLabel DarkLabel10;
        internal DarkUI.Controls.DarkNumericUpDown nudLvlReq;
        internal DarkUI.Controls.DarkLabel DarkLabel11;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox5;
        internal DarkUI.Controls.DarkComboBox cmbRewardItem;
        internal DarkUI.Controls.DarkLabel DarkLabel12;
        internal DarkUI.Controls.DarkNumericUpDown nudRewardExp;
        internal DarkUI.Controls.DarkLabel DarkLabel13;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkButton btnDelete;
        internal DarkUI.Controls.DarkButton btnCancel;
    }
}