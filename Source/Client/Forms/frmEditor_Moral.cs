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
        #region Form Code

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.MoralEditorOk();
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
            lstIndex.Items.RemoveAt(GameState.EditorIndex - 1);
            lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex + ": " + Core.Type.Moral[GameState.EditorIndex].Name);
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
                lstIndex.Items.Add(i + ": " + Core.Type.Moral[i].Name);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Core.Type.Moral[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex - 1);
            lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex + ": " + Core.Type.Moral[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void chkCanCast_CheckedChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].CanCast = chkCanCast.Checked;
        }

        private void chkCanPK_CheckedChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].CanPK = chkCanPK.Checked;
        }

        private void chkCanPickupItem_CheckedChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].CanPickupItem = chkCanPickupItem.Checked;
        }

        private void chkCanDropItem_CheckedChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].CanDropItem = chkCanDropItem.Checked;
        }

        private void chkCanUseItem_CheckedChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].CanUseItem = chkCanUseItem.Checked;
        }

        private void chkDropItems_CheckedChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].DropItems = chkDropItems.Checked;
        }

        private void chkLoseExp_CheckedChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].LoseExp = chkLoseExp.Checked;
        }

        private void chkPlayerBlock_CheckedChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].PlayerBlock = chkPlayerBlock.Checked;
        }

        private void chkNPCBlock_CheckedChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].NPCBlock = chkNPCBlock.Checked;
        }

        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Moral[GameState.EditorIndex].Color = (byte)cmbColor.SelectedIndex;
        }

        #endregion

    }
}