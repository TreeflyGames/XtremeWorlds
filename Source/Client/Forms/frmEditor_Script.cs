using Client.Game.Objects;
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

        private void optButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (optButton2.Checked)
            {
                Core.Type.Script.Type = false;
            }
            else
            {
                Core.Type.Script.Type = true;
            }

        }

        private void optButton_CheckedChanged(object sender, EventArgs e)
        {
            if (optButton.Checked)
            {
                Core.Type.Script.Type = true;
            }
            else
            {
                Core.Type.Script.Type = false;
            }
        }

        private void btnOpenScript_Click(object sender, EventArgs e)
        {
            Script.SendRequestEditScript();
        }
    }
}
