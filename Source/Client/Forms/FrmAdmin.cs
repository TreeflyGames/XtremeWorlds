using System;
using Microsoft.VisualBasic;
using static Core.Global.Command;

namespace Client
{

    internal partial class FrmAdmin
    {
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

        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            NetworkSend.SendRequestMapReport();
            cmbAccess.SelectedIndex = 0;
        }

        #region Moderation

        private void BtnAdminWarpTo_Click(object sender, EventArgs e)
        {

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.WarpTo((int)Math.Round(nudAdminMap.Value));
        }

        private void BtnAdminBan_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendBan(Strings.Trim(txtAdminName.Text));
        }

        private void BtnAdminKick_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendKick(Strings.Trim(txtAdminName.Text));
        }

        private void BtnAdminWarp2Me_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            if (Information.IsNumeric(Strings.Trim(txtAdminName.Text)))
                return;

            NetworkSend.WarpToMe(Strings.Trim(txtAdminName.Text));
        }

        private void BtnAdminWarpMe2_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            if (Information.IsNumeric(Strings.Trim(txtAdminName.Text)))
            {
                return;
            }

            NetworkSend.WarpMeTo(Strings.Trim(txtAdminName.Text));
        }

        private void BtnAdminSetAccess_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Owner)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            if (Information.IsNumeric(Strings.Trim(txtAdminName.Text)) | cmbAccess.SelectedIndex < 0)
            {
                return;
            }

            NetworkSend.SendSetAccess(txtAdminName.Text, (byte)(cmbAccess.SelectedIndex + 1));
        }

        private void BtnAdminSetSprite_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendSetSprite((int)Math.Round(nudAdminSprite.Value));
        }

        #endregion

        #region Editors

        private void btnAnimationEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditAnimation();
        }

        private void btnJobEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditJob();
        }

        private void btnItemEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditItem();
        }

        private void BtnMapEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            Map.SendRequestEditMap();
        }

        private void btnNPCEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditNPC();
        }

        private void btnPetEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            Pet.SendRequestEditPet();
        }

        private void btnProjectiles_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            Projectile.SendRequestEditProjectiles();
        }

        private void btnResourceEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditResource();
        }

        private void btnShopEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditShop();
        }

        private void btnSkillEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditSkill();
        }

        private void FrmAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameState.AdminPanel = false;
        }

        private void BtnMapReport_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }
            NetworkSend.SendRequestMapReport();
        }

        private void LstMaps_DoubleClick(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            if (lstMaps.FocusedItem.Index == 0)
                return;

            NetworkSend.WarpTo(lstMaps.FocusedItem.Index + 1);
        }

        #endregion

        #region Misc
        private void BtnLevelUp_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestLevelUp();
        }

        private void BtnALoc_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            GameState.BLoc = !GameState.BLoc;
        }

        private void BtnRespawn_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            Map.SendMapRespawn();
        }

        private void btnMoralEditor_Click(object sender, EventArgs e)
        {
            if (GameState.MyEditorType != -1)
            {
                Interaction.MsgBox("You are already in an Editor. Please close the editor to use another one.");
                return;
            }

            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
            {
                Client.Text.AddText(Core.LocalesManager.Language.GetValueByKey("Chat", "AccessDenied"), (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditMoral();
        }

        #endregion
    }
}