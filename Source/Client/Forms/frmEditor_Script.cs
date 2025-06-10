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
            Script.SendRequestEditScript();
        }

        private void buttonSaveScript_Click(object sender, EventArgs e)
        {
            Script.SendSaveScript();
        }
    }
}
