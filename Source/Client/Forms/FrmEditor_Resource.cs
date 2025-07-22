﻿using Core;
using Microsoft.VisualBasic;

namespace Client
{
    internal partial class frmEditor_Resource
    {
        public frmEditor_Resource()
        {
            InitializeComponent();
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

        private void ScrlNormalPic_Scroll(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].ResourceImage = (int)Math.Round(nudNormalPic.Value);
            DrawSprite();
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].ResourceType = cmbType.SelectedIndex;
        }

        private void ScrlExhaustedPic_Scroll(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].ExhaustedImage = (int)Math.Round(nudExhaustedPic.Value);
            DrawSprite();
        }

        private void ScrlRewardItem_Scroll(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].ItemReward = cmbRewardItem.SelectedIndex;
        }

        private void ScrlRewardExp_Scroll(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].ExpReward = (int)Math.Round(nudRewardExp.Value);
        }

        private void ScrlLvlReq_Scroll(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].LvlRequired = (int)Math.Round(nudLvlReq.Value);
        }

        private void CmbTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].ToolRequired = cmbTool.SelectedIndex;
        }

        private void ScrlHealth_Scroll(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].Health = (int)Math.Round(nudHealth.Value);
        }

        private void ScrlRespawn_Scroll(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].RespawnTime = (int)Math.Round(nudRespawn.Value);
        }

        private void ScrlAnim_Scroll(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].Animation = cmbAnimation.SelectedIndex;
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.ResourceEditorInit();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.ResourceEditorOK();
            Dispose();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            MapResource.ClearResource(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Data.Resource[GameState.EditorIndex].Name);
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
            for (int i = 0; i < Constant.MAX_RESOURCES; i++)
                lstIndex.Items.Add(i + 1 + ": " + Core.Data.Resource[i].Name);

            // populate combo boxes
            cmbRewardItem.Items.Clear();
            for (int i = 0; i < Constant.MAX_ITEMS; i++)
                cmbRewardItem.Items.Add(i + 1 + ": " + Core.Data.Item[i].Name);

            cmbAnimation.Items.Clear();
            for (int i = 0; i < Constant.MAX_ANIMATIONS; i++)
                cmbAnimation.Items.Add(i + 1 + ": " + Core.Data.Animation[i].Name);

            nudExhaustedPic.Maximum = GameState.NumResources;
            nudNormalPic.Maximum = GameState.NumResources;
            nudRespawn.Maximum = 1000000m;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Core.Data.Resource[GameState.EditorIndex].Name = txtName.Text;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Data.Resource[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void TxtMessage_TextChanged(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].SuccessMessage = Strings.Trim(txtMessage.Text);
        }

        private void TxtMessage2_TextChanged(object sender, EventArgs e)
        {
            Core.Data.Resource[GameState.EditorIndex].EmptyMessage = Strings.Trim(txtMessage2.Text);
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