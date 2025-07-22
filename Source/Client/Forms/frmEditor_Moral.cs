using System;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;

namespace Client
{

    public partial class frmEditor_Moral
    {
        public frmEditor_Moral()
        {
            InitializeComponent();
        }
        #region Frm

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.MoralEditorOK();
            Dispose();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.MoralEditorCancel();
            Dispose();
        }

        private void frmEditor_Moral_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.MoralEditorCancel();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Moral.ClearMoral(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Data.Moral[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.MoralEditorInit();
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.MoralEditorInit();
        }

        private void frmEditor_Moral_Load(object sender, EventArgs e)
        {
            lstIndex.Items.Clear();

            for (int i = 0; i < Constant.MAX_MORALS; i++)
                lstIndex.Items.Add(i + 1 + ": " + Data.Moral[i].Name);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Data.Moral[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Data.Moral[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void chkCanCast_CheckedChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].CanCast = chkCanCast.Checked;
        }

        private void chkCanPK_CheckedChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].CanPK = chkCanPK.Checked;
        }

        private void chkCanPickupItem_CheckedChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].CanPickupItem = chkCanPickupItem.Checked;
        }

        private void chkCanDropItem_CheckedChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].CanDropItem = chkCanDropItem.Checked;
        }

        private void chkCanUseItem_CheckedChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].CanUseItem = chkCanUseItem.Checked;
        }

        private void chkDropItems_CheckedChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].DropItems = chkDropItems.Checked;
        }

        private void chkLoseExp_CheckedChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].LoseExp = chkLoseExp.Checked;
        }

        private void chkPlayerBlock_CheckedChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].PlayerBlock = chkPlayerBlock.Checked;
        }

        private void chkNpcBlock_CheckedChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].NpcBlock = chkNpcBlock.Checked;
        }

        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Moral[GameState.EditorIndex].Color = (byte)cmbColor.SelectedIndex;
        }

        #endregion

    }
}