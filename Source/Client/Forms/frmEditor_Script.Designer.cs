using System.Diagnostics;

namespace Client
{
    partial class frmEditor_Script : Form
    {
        // Shared instance of the form
        private static frmEditor_Script _instance;

        // Public property to get the shared instance
        public static frmEditor_Script Instance
        {
            get
            {
                // Create a new instance if one does not exist or if it has been disposed
                if (_instance is null || _instance.IsDisposed)
                {
                    _instance = new frmEditor_Script();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            optButton = new DarkUI.Controls.DarkRadioButton();
            optButton2 = new DarkUI.Controls.DarkRadioButton();
            darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            btnOpenScript = new DarkUI.Controls.DarkButton();
            darkGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // optButton
            // 
            optButton.AutoSize = true;
            optButton.Location = new Point(6, 22);
            optButton.Name = "optButton";
            optButton.Size = new Size(40, 19);
            optButton.TabIndex = 0;
            optButton.TabStop = true;
            optButton.Text = "C#";
            optButton.CheckedChanged += optButton_CheckedChanged;
            // 
            // optButton2
            // 
            optButton2.AutoSize = true;
            optButton2.Location = new Point(6, 47);
            optButton2.Name = "optButton2";
            optButton2.Size = new Size(39, 19);
            optButton2.TabIndex = 1;
            optButton2.TabStop = true;
            optButton2.Text = "VB";
            optButton2.CheckedChanged += optButton2_CheckedChanged;
            // 
            // darkGroupBox1
            // 
            darkGroupBox1.BorderColor = Color.FromArgb(51, 51, 51);
            darkGroupBox1.Controls.Add(optButton2);
            darkGroupBox1.Controls.Add(optButton);
            darkGroupBox1.Location = new Point(12, 12);
            darkGroupBox1.Name = "darkGroupBox1";
            darkGroupBox1.Size = new Size(121, 82);
            darkGroupBox1.TabIndex = 2;
            darkGroupBox1.TabStop = false;
            darkGroupBox1.Text = "Script Language";
            // 
            // btnOpenScript
            // 
            btnOpenScript.Location = new Point(12, 100);
            btnOpenScript.Name = "btnOpenScript";
            btnOpenScript.Padding = new Padding(5);
            btnOpenScript.Size = new Size(121, 23);
            btnOpenScript.TabIndex = 3;
            btnOpenScript.Text = "Open Script";
            btnOpenScript.Click += btnOpenScript_Click;
            // 
            // frmEditor_Script
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(139, 130);
            Controls.Add(btnOpenScript);
            Controls.Add(darkGroupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmEditor_Script";
            Text = "Script Editor";
            darkGroupBox1.ResumeLayout(false);
            darkGroupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        internal DarkUI.Controls.DarkRadioButton optButton;
        internal DarkUI.Controls.DarkRadioButton optButton2;
        internal DarkUI.Controls.DarkGroupBox darkGroupBox1;
        internal DarkUI.Controls.DarkButton btnOpenScript;
    }
}