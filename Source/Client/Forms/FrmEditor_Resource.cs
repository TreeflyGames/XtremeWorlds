using System;
using System.IO;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;

namespace Client
{
    internal partial class frmEditor_Resource
    {
        public frmEditor_Resource()
        {
            InitializeComponent();
        }

        private void ScrlNormalPic_Scroll(object sender, EventArgs e)
        {
            DrawSprite();
            Core.Type.Resource[GameState.EditorIndex].ResourceImage = (int)Math.Round(nudNormalPic.Value);
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].ResourceType = cmbType.SelectedIndex;
        }

        private void ScrlExhaustedPic_Scroll(object sender, EventArgs e)
        {
            DrawSprite();
            Core.Type.Resource[GameState.EditorIndex].ExhaustedImage = (int)Math.Round(nudExhaustedPic.Value);
        }

        private void ScrlRewardItem_Scroll(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].ItemReward = cmbRewardItem.SelectedIndex;
        }

        private void ScrlRewardExp_Scroll(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].ExpReward = (int)Math.Round(nudRewardExp.Value);
        }

        private void ScrlLvlReq_Scroll(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].LvlRequired = (int)Math.Round(nudLvlReq.Value);
        }

        private void CmbTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].ToolRequired = cmbTool.SelectedIndex;
        }

        private void ScrlHealth_Scroll(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].Health = (int)Math.Round(nudHealth.Value);
        }

        private void ScrlRespawn_Scroll(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].RespawnTime = (int)Math.Round(nudRespawn.Value);
        }

        private void ScrlAnim_Scroll(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].Animation = cmbAnimation.SelectedIndex;
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.ResourceEditorInit();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.ResourceEditorOk();
            Dispose();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Resource.ClearResource(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex - 1);
            lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex + 1 + ": " + Core.Type.Resource[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.ResourceEditorInit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.ResourceEditorCancel();
            Dispose();
        }

        private void frmEditor_Resource_Load(object sender, EventArgs e)
        {
            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i <= Constant.MAX_RESOURCES - 1; i++)
                lstIndex.Items.Add(i + 1 + ": " + Core.Type.Resource[i].Name);

            // populate combo boxes
            cmbRewardItem.Items.Clear();
            cmbRewardItem.Items.Add("None");
            for (int i = 0; i < Constant.MAX_ITEMS; i++)
                cmbRewardItem.Items.Add(i + 1 + ": " + Core.Type.Item[i].Name);

            cmbAnimation.Items.Clear();
            cmbAnimation.Items.Add("None");
            for (int i = 0; i < Constant.MAX_ANIMATIONS; i++)
                cmbAnimation.Items.Add(i + 1 + ": " + Core.Type.Animation[i].Name);

            nudExhaustedPic.Maximum = GameState.NumResources;
            nudNormalPic.Maximum = GameState.NumResources;
            nudRespawn.Maximum = 1000000m;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Core.Type.Resource[GameState.EditorIndex].Name = txtName.Text;
            lstIndex.Items.RemoveAt(GameState.EditorIndex - 1);
            lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex + 1 + ": " + Core.Type.Resource[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void TxtMessage_TextChanged(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].SuccessMessage = Strings.Trim(txtMessage.Text);
        }

        private void TxtMessage2_TextChanged(object sender, EventArgs e)
        {
            Core.Type.Resource[GameState.EditorIndex].EmptyMessage = Strings.Trim(txtMessage2.Text);
        }

        private void frmEditor_Resource_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.ResourceEditorCancel();
        }

        private void DrawSprite()
        {
            int Sprite;

            // normal sprite
            Sprite = (int)Math.Round(nudNormalPic.Value);

            if (Sprite < 1 | Sprite > GameState.NumResources)
            {
                picNormalpic.BackgroundImage = null;
            }
            else if (File.Exists(System.IO.Path.Combine(Core.Path.Resources, Sprite + GameState.GfxExt)))
            {
                picNormalpic.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Resources, Sprite + GameState.GfxExt));

            }

            // exhausted sprite
            Sprite = (int)Math.Round(nudExhaustedPic.Value);

            if (Sprite < 1 | Sprite > GameState.NumResources)
            {
                picExhaustedPic.BackgroundImage = null;
            }
            else if (File.Exists(System.IO.Path.Combine(Core.Path.Resources, Sprite + GameState.GfxExt)))
            {
                picExhaustedPic.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Resources, Sprite + GameState.GfxExt));
            }
        }

    }
}