using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{

    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    internal partial class frmEditor_Job : Form
    {

        // Shared instance of the form
        private static frmEditor_Job _instance;

        // Public property to get the shared instance
        public static frmEditor_Job Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Job();
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
            DarkGroupBox7 = new DarkUI.Controls.DarkGroupBox();
            nudStartY = new DarkUI.Controls.DarkNumericUpDown();
            nudStartY.Click += new EventHandler(NumStartY_Click);
            DarkLabel15 = new DarkUI.Controls.DarkLabel();
            nudStartX = new DarkUI.Controls.DarkNumericUpDown();
            nudStartX.Click += new EventHandler(NumStartX_Click);
            DarkLabel14 = new DarkUI.Controls.DarkLabel();
            nudStartMap = new DarkUI.Controls.DarkNumericUpDown();
            nudStartMap.Click += new EventHandler(NumStartMap_Click);
            DarkLabel13 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox6 = new DarkUI.Controls.DarkGroupBox();
            btnItemAdd = new DarkUI.Controls.DarkButton();
            btnItemAdd.Click += new EventHandler(BtnItemAdd_Click);
            nudItemAmount = new DarkUI.Controls.DarkNumericUpDown();
            DarkLabel12 = new DarkUI.Controls.DarkLabel();
            cmbItems = new DarkUI.Controls.DarkComboBox();
            DarkLabel11 = new DarkUI.Controls.DarkLabel();
            lstStartItems = new ListBox();
            DarkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
            nudBaseExp = new DarkUI.Controls.DarkNumericUpDown();
            nudBaseExp.Click += new EventHandler(NumBaseExp_ValueChanged);
            DarkLabel10 = new DarkUI.Controls.DarkLabel();
            nudSpirit = new DarkUI.Controls.DarkNumericUpDown();
            nudSpirit.Click += new EventHandler(NumSpirit_ValueChanged);
            DarkLabel8 = new DarkUI.Controls.DarkLabel();
            DarkLabel9 = new DarkUI.Controls.DarkLabel();
            nudVitality = new DarkUI.Controls.DarkNumericUpDown();
            nudVitality.Click += new EventHandler(NumVitality_ValueChanged);
            nudLuck = new DarkUI.Controls.DarkNumericUpDown();
            nudLuck.Click += new EventHandler(NumLuck_ValueChanged);
            DarkLabel6 = new DarkUI.Controls.DarkLabel();
            DarkLabel7 = new DarkUI.Controls.DarkLabel();
            nudIntelligence = new DarkUI.Controls.DarkNumericUpDown();
            nudIntelligence.Click += new EventHandler(NumIntelligence_ValueChanged);
            nudStrength = new DarkUI.Controls.DarkNumericUpDown();
            nudStrength.Click += new EventHandler(NumStrength_ValueChanged);
            DarkLabel5 = new DarkUI.Controls.DarkLabel();
            DarkLabel3 = new DarkUI.Controls.DarkLabel();
            DarkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
            nudFemaleSprite = new DarkUI.Controls.DarkNumericUpDown();
            nudFemaleSprite.Click += new EventHandler(nudFemaleSprite_Click);
            DarkLabel4 = new DarkUI.Controls.DarkLabel();
            picFemale = new PictureBox();
            DarkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
            nudMaleSprite = new DarkUI.Controls.DarkNumericUpDown();
            nudMaleSprite.Click += new EventHandler(nudMaleSprite_Click);
            lblMaleSprite = new DarkUI.Controls.DarkLabel();
            picMale = new PictureBox();
            txtDescription = new DarkUI.Controls.DarkTextBox();
            txtDescription.TextChanged += new EventHandler(TxtDescription_TextChanged);
            DarkLabel2 = new DarkUI.Controls.DarkLabel();
            txtName = new DarkUI.Controls.DarkTextBox();
            txtName.TextChanged += new EventHandler(TxtName_TextChanged);
            DarkLabel1 = new DarkUI.Controls.DarkLabel();
            btnCancel = new DarkUI.Controls.DarkButton();
            btnCancel.Click += new EventHandler(BtnCancel_Click);
            btnSave = new DarkUI.Controls.DarkButton();
            btnSave.Click += new EventHandler(BtnSave_Click);
            btnDelete = new DarkUI.Controls.DarkButton();
            btnDelete.Click += new EventHandler(btnDelete_Click);
            DarkGroupBox1.SuspendLayout();
            DarkGroupBox2.SuspendLayout();
            DarkGroupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudStartY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStartX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStartMap).BeginInit();
            DarkGroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudItemAmount).BeginInit();
            DarkGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudBaseExp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSpirit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVitality).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLuck).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudIntelligence).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStrength).BeginInit();
            DarkGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFemaleSprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picFemale).BeginInit();
            DarkGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMaleSprite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMale).BeginInit();
            SuspendLayout();
            // 
            // DarkGroupBox1
            // 
            DarkGroupBox1.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox1.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox1.Controls.Add(lstIndex);
            DarkGroupBox1.ForeColor = Color.Gainsboro;
            DarkGroupBox1.Location = new Point(2, 3);
            DarkGroupBox1.Margin = new Padding(5, 5, 5, 5);
            DarkGroupBox1.Name = "DarkGroupBox1";
            DarkGroupBox1.Padding = new Padding(5, 5, 5, 5);
            DarkGroupBox1.Size = new Size(288, 742);
            DarkGroupBox1.TabIndex = 0;
            DarkGroupBox1.TabStop = false;
            DarkGroupBox1.Text = "Job List";
            // 
            // lstIndex
            // 
            lstIndex.BackColor = Color.FromArgb(45, 45, 48);
            lstIndex.BorderStyle = BorderStyle.FixedSingle;
            lstIndex.ForeColor = Color.Gainsboro;
            lstIndex.FormattingEnabled = true;
            lstIndex.ItemHeight = 25;
            lstIndex.Location = new Point(10, 27);
            lstIndex.Margin = new Padding(5, 5, 5, 5);
            lstIndex.Name = "lstIndex";
            lstIndex.Size = new Size(265, 702);
            lstIndex.TabIndex = 0;
            // 
            // DarkGroupBox2
            // 
            DarkGroupBox2.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox2.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox2.Controls.Add(DarkGroupBox7);
            DarkGroupBox2.Controls.Add(DarkGroupBox6);
            DarkGroupBox2.Controls.Add(DarkGroupBox5);
            DarkGroupBox2.Controls.Add(DarkGroupBox4);
            DarkGroupBox2.Controls.Add(DarkGroupBox3);
            DarkGroupBox2.Controls.Add(txtDescription);
            DarkGroupBox2.Controls.Add(DarkLabel2);
            DarkGroupBox2.Controls.Add(txtName);
            DarkGroupBox2.Controls.Add(DarkLabel1);
            DarkGroupBox2.ForeColor = Color.Gainsboro;
            DarkGroupBox2.Location = new Point(302, 3);
            DarkGroupBox2.Margin = new Padding(5, 5, 5, 5);
            DarkGroupBox2.Name = "DarkGroupBox2";
            DarkGroupBox2.Padding = new Padding(5, 5, 5, 5);
            DarkGroupBox2.Size = new Size(568, 909);
            DarkGroupBox2.TabIndex = 1;
            DarkGroupBox2.TabStop = false;
            DarkGroupBox2.Text = "Properties";
            // 
            // DarkGroupBox7
            // 
            DarkGroupBox7.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox7.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox7.Controls.Add(nudStartY);
            DarkGroupBox7.Controls.Add(DarkLabel15);
            DarkGroupBox7.Controls.Add(nudStartX);
            DarkGroupBox7.Controls.Add(DarkLabel14);
            DarkGroupBox7.Controls.Add(nudStartMap);
            DarkGroupBox7.Controls.Add(DarkLabel13);
            DarkGroupBox7.ForeColor = Color.Gainsboro;
            DarkGroupBox7.Location = new Point(10, 812);
            DarkGroupBox7.Margin = new Padding(5, 5, 5, 5);
            DarkGroupBox7.Name = "DarkGroupBox7";
            DarkGroupBox7.Padding = new Padding(5, 5, 5, 5);
            DarkGroupBox7.Size = new Size(547, 84);
            DarkGroupBox7.TabIndex = 8;
            DarkGroupBox7.TabStop = false;
            DarkGroupBox7.Text = "Starting Point";
            // 
            // nudStartY
            // 
            nudStartY.Location = new Point(457, 27);
            nudStartY.Margin = new Padding(5, 5, 5, 5);
            nudStartY.Name = "nudStartY";
            nudStartY.Size = new Size(80, 31);
            nudStartY.TabIndex = 5;
            // 
            // DarkLabel15
            // 
            DarkLabel15.AutoSize = true;
            DarkLabel15.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel15.Location = new Point(377, 30);
            DarkLabel15.Margin = new Padding(5, 0, 5, 0);
            DarkLabel15.Name = "DarkLabel15";
            DarkLabel15.Size = new Size(57, 25);
            DarkLabel15.TabIndex = 4;
            DarkLabel15.Text = "Start :";
            // 
            // nudStartX
            // 
            nudStartX.Location = new Point(280, 27);
            nudStartX.Margin = new Padding(5, 5, 5, 5);
            nudStartX.Name = "nudStartX";
            nudStartX.Size = new Size(80, 31);
            nudStartX.TabIndex = 3;
            // 
            // DarkLabel14
            // 
            DarkLabel14.AutoSize = true;
            DarkLabel14.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel14.Location = new Point(200, 30);
            DarkLabel14.Margin = new Padding(5, 0, 5, 0);
            DarkLabel14.Name = "DarkLabel14";
            DarkLabel14.Size = new Size(68, 25);
            DarkLabel14.TabIndex = 2;
            DarkLabel14.Text = "Start X:";
            // 
            // nudStartMap
            // 
            nudStartMap.Location = new Point(113, 27);
            nudStartMap.Margin = new Padding(5, 5, 5, 5);
            nudStartMap.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudStartMap.Name = "nudStartMap";
            nudStartMap.Size = new Size(77, 31);
            nudStartMap.TabIndex = 1;
            nudStartMap.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DarkLabel13
            // 
            DarkLabel13.AutoSize = true;
            DarkLabel13.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel13.Location = new Point(10, 30);
            DarkLabel13.Margin = new Padding(5, 0, 5, 0);
            DarkLabel13.Name = "DarkLabel13";
            DarkLabel13.Size = new Size(93, 25);
            DarkLabel13.TabIndex = 0;
            DarkLabel13.Text = "Start Map:";
            // 
            // DarkGroupBox6
            // 
            DarkGroupBox6.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox6.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox6.Controls.Add(btnItemAdd);
            DarkGroupBox6.Controls.Add(nudItemAmount);
            DarkGroupBox6.Controls.Add(DarkLabel12);
            DarkGroupBox6.Controls.Add(cmbItems);
            DarkGroupBox6.Controls.Add(DarkLabel11);
            DarkGroupBox6.Controls.Add(lstStartItems);
            DarkGroupBox6.ForeColor = Color.Gainsboro;
            DarkGroupBox6.Location = new Point(10, 597);
            DarkGroupBox6.Margin = new Padding(5, 5, 5, 5);
            DarkGroupBox6.Name = "DarkGroupBox6";
            DarkGroupBox6.Padding = new Padding(5, 5, 5, 5);
            DarkGroupBox6.Size = new Size(547, 203);
            DarkGroupBox6.TabIndex = 7;
            DarkGroupBox6.TabStop = false;
            DarkGroupBox6.Text = "Starting Items";
            // 
            // btnItemAdd
            // 
            btnItemAdd.Location = new Point(305, 140);
            btnItemAdd.Margin = new Padding(5, 5, 5, 5);
            btnItemAdd.Name = "btnItemAdd";
            btnItemAdd.Padding = new Padding(8, 10, 8, 10);
            btnItemAdd.Size = new Size(232, 50);
            btnItemAdd.TabIndex = 6;
            btnItemAdd.Text = "Update";
            // 
            // nudItemAmount
            // 
            nudItemAmount.Location = new Point(392, 97);
            nudItemAmount.Margin = new Padding(5, 5, 5, 5);
            nudItemAmount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudItemAmount.Name = "nudItemAmount";
            nudItemAmount.Size = new Size(145, 31);
            nudItemAmount.TabIndex = 5;
            nudItemAmount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DarkLabel12
            // 
            DarkLabel12.AutoSize = true;
            DarkLabel12.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel12.Location = new Point(305, 100);
            DarkLabel12.Margin = new Padding(5, 0, 5, 0);
            DarkLabel12.Name = "DarkLabel12";
            DarkLabel12.Size = new Size(81, 25);
            DarkLabel12.TabIndex = 4;
            DarkLabel12.Text = "Amount:";
            // 
            // cmbItems
            // 
            cmbItems.DrawMode = DrawMode.OwnerDrawFixed;
            cmbItems.FormattingEnabled = true;
            cmbItems.Location = new Point(305, 45);
            cmbItems.Margin = new Padding(5, 5, 5, 5);
            cmbItems.Name = "cmbItems";
            cmbItems.Size = new Size(229, 32);
            cmbItems.TabIndex = 3;
            // 
            // DarkLabel11
            // 
            DarkLabel11.AutoSize = true;
            DarkLabel11.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel11.Location = new Point(305, 13);
            DarkLabel11.Margin = new Padding(5, 0, 5, 0);
            DarkLabel11.Name = "DarkLabel11";
            DarkLabel11.Size = new Size(89, 25);
            DarkLabel11.TabIndex = 2;
            DarkLabel11.Text = "Start Item";
            // 
            // lstStartItems
            // 
            lstStartItems.BackColor = Color.FromArgb(45, 45, 48);
            lstStartItems.BorderStyle = BorderStyle.FixedSingle;
            lstStartItems.ForeColor = Color.Gainsboro;
            lstStartItems.FormattingEnabled = true;
            lstStartItems.ItemHeight = 25;
            lstStartItems.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            lstStartItems.Location = new Point(10, 37);
            lstStartItems.Margin = new Padding(5, 5, 5, 5);
            lstStartItems.Name = "lstStartItems";
            lstStartItems.Size = new Size(284, 152);
            lstStartItems.TabIndex = 1;
            // 
            // DarkGroupBox5
            // 
            DarkGroupBox5.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox5.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox5.Controls.Add(nudBaseExp);
            DarkGroupBox5.Controls.Add(DarkLabel10);
            DarkGroupBox5.Controls.Add(nudSpirit);
            DarkGroupBox5.Controls.Add(DarkLabel8);
            DarkGroupBox5.Controls.Add(DarkLabel9);
            DarkGroupBox5.Controls.Add(nudVitality);
            DarkGroupBox5.Controls.Add(nudLuck);
            DarkGroupBox5.Controls.Add(DarkLabel6);
            DarkGroupBox5.Controls.Add(DarkLabel7);
            DarkGroupBox5.Controls.Add(nudIntelligence);
            DarkGroupBox5.Controls.Add(nudStrength);
            DarkGroupBox5.Controls.Add(DarkLabel5);
            DarkGroupBox5.Controls.Add(DarkLabel3);
            DarkGroupBox5.ForeColor = Color.Gainsboro;
            DarkGroupBox5.Location = new Point(10, 391);
            DarkGroupBox5.Margin = new Padding(5, 5, 5, 5);
            DarkGroupBox5.Name = "DarkGroupBox5";
            DarkGroupBox5.Padding = new Padding(5, 5, 5, 5);
            DarkGroupBox5.Size = new Size(547, 191);
            DarkGroupBox5.TabIndex = 6;
            DarkGroupBox5.TabStop = false;
            DarkGroupBox5.Text = "Start Stats";
            // 
            // nudBaseExp
            // 
            nudBaseExp.Location = new Point(170, 135);
            nudBaseExp.Margin = new Padding(5, 5, 5, 5);
            nudBaseExp.Name = "nudBaseExp";
            nudBaseExp.Size = new Size(172, 31);
            nudBaseExp.TabIndex = 13;
            // 
            // DarkLabel10
            // 
            DarkLabel10.AutoSize = true;
            DarkLabel10.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel10.Location = new Point(10, 138);
            DarkLabel10.Margin = new Padding(5, 0, 5, 0);
            DarkLabel10.Name = "DarkLabel10";
            DarkLabel10.Size = new Size(85, 25);
            DarkLabel10.TabIndex = 12;
            DarkLabel10.Text = "Base Exp:";
            // 
            // nudSpirit
            // 
            nudSpirit.Location = new Point(462, 77);
            nudSpirit.Margin = new Padding(5, 5, 5, 5);
            nudSpirit.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSpirit.Name = "nudSpirit";
            nudSpirit.Size = new Size(75, 31);
            nudSpirit.TabIndex = 11;
            nudSpirit.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DarkLabel8
            // 
            DarkLabel8.AutoSize = true;
            DarkLabel8.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel8.Location = new Point(352, 80);
            DarkLabel8.Margin = new Padding(5, 0, 5, 0);
            DarkLabel8.Name = "DarkLabel8";
            DarkLabel8.Size = new Size(57, 25);
            DarkLabel8.TabIndex = 9;
            DarkLabel8.Text = "Spirit:";
            // 
            // DarkLabel9
            // 
            DarkLabel9.AutoSize = true;
            DarkLabel9.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel9.Location = new Point(352, 30);
            DarkLabel9.Margin = new Padding(5, 0, 5, 0);
            DarkLabel9.Name = "DarkLabel9";
            DarkLabel9.Size = new Size(98, 25);
            DarkLabel9.TabIndex = 8;
            DarkLabel9.Text = "Endurance:";
            // 
            // nudVitality
            // 
            nudVitality.Location = new Point(267, 77);
            nudVitality.Margin = new Padding(5, 5, 5, 5);
            nudVitality.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudVitality.Name = "nudVitality";
            nudVitality.Size = new Size(75, 31);
            nudVitality.TabIndex = 7;
            nudVitality.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nudLuck
            // 
            nudLuck.Location = new Point(267, 27);
            nudLuck.Margin = new Padding(5, 5, 5, 5);
            nudLuck.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudLuck.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLuck.Name = "nudLuck";
            nudLuck.Size = new Size(75, 31);
            nudLuck.TabIndex = 6;
            nudLuck.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DarkLabel6
            // 
            DarkLabel6.AutoSize = true;
            DarkLabel6.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel6.Location = new Point(200, 80);
            DarkLabel6.Margin = new Padding(5, 0, 5, 0);
            DarkLabel6.Name = "DarkLabel6";
            DarkLabel6.Size = new Size(69, 25);
            DarkLabel6.TabIndex = 5;
            DarkLabel6.Text = "Vitality:";
            // 
            // DarkLabel7
            // 
            DarkLabel7.AutoSize = true;
            DarkLabel7.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel7.Location = new Point(200, 30);
            DarkLabel7.Margin = new Padding(5, 0, 5, 0);
            DarkLabel7.Name = "DarkLabel7";
            DarkLabel7.Size = new Size(51, 25);
            DarkLabel7.TabIndex = 4;
            DarkLabel7.Text = "Luck:";
            // 
            // nudIntelligence
            // 
            nudIntelligence.Location = new Point(115, 77);
            nudIntelligence.Margin = new Padding(5, 5, 5, 5);
            nudIntelligence.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudIntelligence.Name = "nudIntelligence";
            nudIntelligence.Size = new Size(75, 31);
            nudIntelligence.TabIndex = 3;
            nudIntelligence.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nudStrength
            // 
            nudStrength.Location = new Point(115, 27);
            nudStrength.Margin = new Padding(5, 5, 5, 5);
            nudStrength.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            nudStrength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudStrength.Name = "nudStrength";
            nudStrength.Size = new Size(75, 31);
            nudStrength.TabIndex = 2;
            nudStrength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DarkLabel5
            // 
            DarkLabel5.AutoSize = true;
            DarkLabel5.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel5.Location = new Point(10, 80);
            DarkLabel5.Margin = new Padding(5, 0, 5, 0);
            DarkLabel5.Name = "DarkLabel5";
            DarkLabel5.Size = new Size(105, 25);
            DarkLabel5.TabIndex = 1;
            DarkLabel5.Text = "Intelligence:";
            // 
            // DarkLabel3
            // 
            DarkLabel3.AutoSize = true;
            DarkLabel3.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel3.Location = new Point(10, 30);
            DarkLabel3.Margin = new Padding(5, 0, 5, 0);
            DarkLabel3.Name = "DarkLabel3";
            DarkLabel3.Size = new Size(83, 25);
            DarkLabel3.TabIndex = 0;
            DarkLabel3.Text = "Strength:";
            // 
            // DarkGroupBox4
            // 
            DarkGroupBox4.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox4.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox4.Controls.Add(nudFemaleSprite);
            DarkGroupBox4.Controls.Add(DarkLabel4);
            DarkGroupBox4.Controls.Add(picFemale);
            DarkGroupBox4.ForeColor = Color.Gainsboro;
            DarkGroupBox4.Location = new Point(288, 166);
            DarkGroupBox4.Margin = new Padding(5, 5, 5, 5);
            DarkGroupBox4.Name = "DarkGroupBox4";
            DarkGroupBox4.Padding = new Padding(5, 5, 5, 5);
            DarkGroupBox4.Size = new Size(268, 213);
            DarkGroupBox4.TabIndex = 5;
            DarkGroupBox4.TabStop = false;
            DarkGroupBox4.Text = "Female Sprite";
            // 
            // nudFemaleSprite
            // 
            nudFemaleSprite.Location = new Point(80, 162);
            nudFemaleSprite.Margin = new Padding(5, 5, 5, 5);
            nudFemaleSprite.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudFemaleSprite.Name = "nudFemaleSprite";
            nudFemaleSprite.Size = new Size(92, 31);
            nudFemaleSprite.TabIndex = 18;
            nudFemaleSprite.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DarkLabel4
            // 
            DarkLabel4.AutoSize = true;
            DarkLabel4.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel4.Location = new Point(12, 165);
            DarkLabel4.Margin = new Padding(5, 0, 5, 0);
            DarkLabel4.Name = "DarkLabel4";
            DarkLabel4.Size = new Size(62, 25);
            DarkLabel4.TabIndex = 17;
            DarkLabel4.Text = "Sprite:";
            // 
            // picFemale
            // 
            picFemale.BackColor = Color.FromArgb(64, 64, 64);
            picFemale.BackgroundImageLayout = ImageLayout.None;
            picFemale.Location = new Point(178, 20);
            picFemale.Margin = new Padding(5, 5, 5, 5);
            picFemale.Name = "picFemale";
            picFemale.Size = new Size(80, 123);
            picFemale.TabIndex = 14;
            picFemale.TabStop = false;
            // 
            // DarkGroupBox3
            // 
            DarkGroupBox3.BackColor = Color.FromArgb(45, 45, 48);
            DarkGroupBox3.BorderColor = Color.FromArgb(90, 90, 90);
            DarkGroupBox3.Controls.Add(nudMaleSprite);
            DarkGroupBox3.Controls.Add(lblMaleSprite);
            DarkGroupBox3.Controls.Add(picMale);
            DarkGroupBox3.ForeColor = Color.Gainsboro;
            DarkGroupBox3.Location = new Point(10, 166);
            DarkGroupBox3.Margin = new Padding(5, 5, 5, 5);
            DarkGroupBox3.Name = "DarkGroupBox3";
            DarkGroupBox3.Padding = new Padding(5, 5, 5, 5);
            DarkGroupBox3.Size = new Size(268, 213);
            DarkGroupBox3.TabIndex = 4;
            DarkGroupBox3.TabStop = false;
            DarkGroupBox3.Text = "Male Sprite";
            // 
            // nudMaleSprite
            // 
            nudMaleSprite.Location = new Point(80, 162);
            nudMaleSprite.Margin = new Padding(5, 5, 5, 5);
            nudMaleSprite.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudMaleSprite.Name = "nudMaleSprite";
            nudMaleSprite.Size = new Size(92, 31);
            nudMaleSprite.TabIndex = 12;
            nudMaleSprite.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblMaleSprite
            // 
            lblMaleSprite.AutoSize = true;
            lblMaleSprite.ForeColor = Color.FromArgb(220, 220, 220);
            lblMaleSprite.Location = new Point(12, 165);
            lblMaleSprite.Margin = new Padding(5, 0, 5, 0);
            lblMaleSprite.Name = "lblMaleSprite";
            lblMaleSprite.Size = new Size(62, 25);
            lblMaleSprite.TabIndex = 11;
            lblMaleSprite.Text = "Sprite:";
            // 
            // picMale
            // 
            picMale.BackColor = Color.FromArgb(64, 64, 64);
            picMale.BackgroundImageLayout = ImageLayout.None;
            picMale.Location = new Point(178, 20);
            picMale.Margin = new Padding(5, 5, 5, 5);
            picMale.Name = "picMale";
            picMale.Size = new Size(80, 123);
            picMale.TabIndex = 8;
            picMale.TabStop = false;
            // 
            // txtDescription
            // 
            txtDescription.BackColor = Color.FromArgb(69, 73, 74);
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 8.25f);
            txtDescription.ForeColor = Color.FromArgb(220, 220, 220);
            txtDescription.Location = new Point(125, 90);
            txtDescription.Margin = new Padding(5, 5, 5, 5);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(430, 64);
            txtDescription.TabIndex = 3;
            // 
            // DarkLabel2
            // 
            DarkLabel2.AutoSize = true;
            DarkLabel2.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel2.Location = new Point(12, 110);
            DarkLabel2.Margin = new Padding(5, 0, 5, 0);
            DarkLabel2.Name = "DarkLabel2";
            DarkLabel2.Size = new Size(106, 25);
            DarkLabel2.TabIndex = 2;
            DarkLabel2.Text = "Description:";
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(69, 73, 74);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 8.25f);
            txtName.ForeColor = Color.FromArgb(220, 220, 220);
            txtName.Location = new Point(83, 27);
            txtName.Margin = new Padding(5, 5, 5, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(472, 29);
            txtName.TabIndex = 1;
            // 
            // DarkLabel1
            // 
            DarkLabel1.AutoSize = true;
            DarkLabel1.ForeColor = Color.FromArgb(220, 220, 220);
            DarkLabel1.Location = new Point(10, 30);
            DarkLabel1.Margin = new Padding(5, 0, 5, 0);
            DarkLabel1.Name = "DarkLabel1";
            DarkLabel1.Size = new Size(63, 25);
            DarkLabel1.TabIndex = 0;
            DarkLabel1.Text = "Name:";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(12, 867);
            btnCancel.Margin = new Padding(5, 5, 5, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(8, 10, 8, 10);
            btnCancel.Size = new Size(265, 45);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 756);
            btnSave.Margin = new Padding(5, 5, 5, 5);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(8, 10, 8, 10);
            btnSave.Size = new Size(265, 45);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(12, 812);
            btnDelete.Margin = new Padding(5, 5, 5, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(8, 10, 8, 10);
            btnDelete.Size = new Size(265, 45);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            // 
            // frmEditor_Job
            // 
            AutoScaleDimensions = new SizeF(10.0f, 25.0f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(879, 920);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(DarkGroupBox2);
            Controls.Add(DarkGroupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(5, 5, 5, 5);
            Name = "frmEditor_Job";
            Text = "Job Editor";
            DarkGroupBox1.ResumeLayout(false);
            DarkGroupBox2.ResumeLayout(false);
            DarkGroupBox2.PerformLayout();
            DarkGroupBox7.ResumeLayout(false);
            DarkGroupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudStartY).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStartX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStartMap).EndInit();
            DarkGroupBox6.ResumeLayout(false);
            DarkGroupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudItemAmount).EndInit();
            DarkGroupBox5.ResumeLayout(false);
            DarkGroupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudBaseExp).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSpirit).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVitality).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLuck).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudIntelligence).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStrength).EndInit();
            DarkGroupBox4.ResumeLayout(false);
            DarkGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudFemaleSprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)picFemale).EndInit();
            DarkGroupBox3.ResumeLayout(false);
            DarkGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMaleSprite).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMale).EndInit();
            Load += new EventHandler(frmEditor_Job_Load);
            FormClosing += new FormClosingEventHandler(frmEditor_Job_FormClosing);
            ResumeLayout(false);
        }

        internal DarkUI.Controls.DarkGroupBox DarkGroupBox1;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox2;
        internal DarkUI.Controls.DarkTextBox txtName;
        internal DarkUI.Controls.DarkLabel DarkLabel1;
        internal DarkUI.Controls.DarkTextBox txtDescription;
        internal DarkUI.Controls.DarkLabel DarkLabel2;
        internal ListBox lstIndex;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox4;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox3;
        internal PictureBox picMale;
        internal DarkUI.Controls.DarkLabel DarkLabel4;
        internal PictureBox picFemale;
        internal DarkUI.Controls.DarkLabel lblMaleSprite;
        internal DarkUI.Controls.DarkButton btnCancel;
        internal DarkUI.Controls.DarkButton btnSave;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox5;
        internal DarkUI.Controls.DarkNumericUpDown nudFemaleSprite;
        internal DarkUI.Controls.DarkNumericUpDown nudMaleSprite;
        internal DarkUI.Controls.DarkLabel DarkLabel5;
        internal DarkUI.Controls.DarkLabel DarkLabel3;
        internal DarkUI.Controls.DarkNumericUpDown nudIntelligence;
        internal DarkUI.Controls.DarkNumericUpDown nudStrength;
        internal DarkUI.Controls.DarkNumericUpDown nudVitality;
        internal DarkUI.Controls.DarkNumericUpDown nudLuck;
        internal DarkUI.Controls.DarkLabel DarkLabel6;
        internal DarkUI.Controls.DarkLabel DarkLabel7;
        internal DarkUI.Controls.DarkNumericUpDown nudSpirit;
        internal DarkUI.Controls.DarkNumericUpDown nudEndurance;
        internal DarkUI.Controls.DarkLabel DarkLabel8;
        internal DarkUI.Controls.DarkLabel DarkLabel9;
        internal DarkUI.Controls.DarkNumericUpDown nudBaseExp;
        internal DarkUI.Controls.DarkLabel DarkLabel10;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox6;
        internal ListBox lstStartItems;
        internal DarkUI.Controls.DarkComboBox cmbItems;
        internal DarkUI.Controls.DarkLabel DarkLabel11;
        internal DarkUI.Controls.DarkNumericUpDown nudItemAmount;
        internal DarkUI.Controls.DarkLabel DarkLabel12;
        internal DarkUI.Controls.DarkButton btnItemAdd;
        internal DarkUI.Controls.DarkGroupBox DarkGroupBox7;
        internal DarkUI.Controls.DarkNumericUpDown nudStartMap;
        internal DarkUI.Controls.DarkLabel DarkLabel13;
        internal DarkUI.Controls.DarkNumericUpDown nudStartY;
        internal DarkUI.Controls.DarkLabel DarkLabel15;
        internal DarkUI.Controls.DarkNumericUpDown nudStartX;
        internal DarkUI.Controls.DarkLabel DarkLabel14;
        internal DarkUI.Controls.DarkButton btnDelete;
    }
}