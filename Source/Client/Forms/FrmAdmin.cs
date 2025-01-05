using System;
using Microsoft.VisualBasic;
using static Core.Global.Command;

namespace Client
{

    internal partial class FrmAdmin
    {
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.WarpTo((int)Math.Round(nudAdminMap.Value));
        }

        private void BtnAdminBan_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendBan(Strings.Trim(txtAdminName.Text));
        }

        private void BtnAdminKick_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendKick(Strings.Trim(txtAdminName.Text));
        }

        private void BtnAdminWarp2Me_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            if (Information.IsNumeric(Strings.Trim(txtAdminName.Text)) | cmbAccess.SelectedIndex < 0)
            {
                return;
            }

            NetworkSend.SendSetAccess(Strings.Trim(txtAdminName.Text), (byte)cmbAccess.SelectedIndex);
        }

        private void BtnAdminSetSprite_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            Projectile.SendRequestEditProjectile();
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditSkill();
        }

        #endregion

        #region Map Report

        private void BtnMapReport_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }
            NetworkSend.SendRequestMapReport();
        }

        private void LstMaps_DoubleClick(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestLevelUp();
        }

        private void BtnALoc_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            GameState.BLoc = !GameState.BLoc;
        }

        private void BtnRespawn_Click(object sender, EventArgs e)
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
            {
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
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
                Client.Text.AddText("You need to be a high enough staff member to do this!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            NetworkSend.SendRequestEditMoral();
        }

        #endregion

    }
}