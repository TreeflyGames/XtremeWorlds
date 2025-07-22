using Assimp;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmEditor_Script : Form
    {
        public frmEditor_Script()
        {
            InitializeComponent();
        }

        private void btnOpenScript_Click(object sender, EventArgs e)
        {
            // Open code in temp file
            System.IO.File.WriteAllLines(Script.TempFile, Core.Data.Script.Code);

            // Open with default text editor
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = Script.TempFile,
                UseShellExecute = true
            });
        }

        private void buttonSaveScript_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Script.TempFile))
            {
                Interaction.MsgBox("Open a sript before saving.");
                return;
            }

            // Read the script file and set the script code to the file contents
            Core.Data.Script.Code = File.ReadAllLines(Script.TempFile);
            Script.SendSaveScript();
        }

        private void frmEditor_Script_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetworkSend.SendCloseEditor();
            GameState.MyEditorType = -1;
            Dispose();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEACTIVATE = 0x0021;
            const int WM_NCHITTEST = 0x0084;

            if (m.Msg == WM_MOUSEACTIVATE)
            {
                // Immediately activate and process the click.
                m.Result = new IntPtr(1); // MA_ACTIVATE
                return;
            }
            else if (m.Msg == WM_NCHITTEST)
            {
                // Let the window know the mouse is in client area.
                m.Result = new IntPtr(1); // HTCLIENT
                return;
            }

            base.WndProc(ref m);
        }
    }
}
