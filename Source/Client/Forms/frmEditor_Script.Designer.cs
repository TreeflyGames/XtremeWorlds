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
            btnOpenScript = new DarkUI.Controls.DarkButton();
            buttonSaveScript = new DarkUI.Controls.DarkButton();
            SuspendLayout();
            // 
            // btnOpenScript
            // 
            btnOpenScript.Location = new Point(12, 12);
            btnOpenScript.Name = "btnOpenScript";
            btnOpenScript.Padding = new Padding(5);
            btnOpenScript.Size = new Size(129, 33);
            btnOpenScript.TabIndex = 3;
            btnOpenScript.Text = "Open Script";
            btnOpenScript.Click += btnOpenScript_Click;
            // 
            // buttonSaveScript
            // 
            buttonSaveScript.Location = new Point(12, 51);
            buttonSaveScript.Name = "buttonSaveScript";
            buttonSaveScript.Padding = new Padding(5);
            buttonSaveScript.Size = new Size(129, 33);
            buttonSaveScript.TabIndex = 4;
            buttonSaveScript.Text = "Save Script";
            buttonSaveScript.Click += buttonSaveScript_Click;
            // 
            // frmEditor_Script
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(153, 96);
            Controls.Add(buttonSaveScript);
            Controls.Add(btnOpenScript);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmEditor_Script";
            Text = "Script Editor";
            ResumeLayout(false);
        }

        #endregion
        internal DarkUI.Controls.DarkButton btnOpenScript;
        internal DarkUI.Controls.DarkButton buttonSaveScript;
    }
}