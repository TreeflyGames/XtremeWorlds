using System;
using System.IO;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;

namespace Client
{

    internal partial class frmEditor_Item
    {
        public frmEditor_Item()
        {
            InitializeComponent();
        }

        #region Form Code

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.ItemEditorOK();
            Dispose();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.ItemEditorCancel();
            Dispose();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Item.ClearItem(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Item[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.ItemEditorInit();
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.ItemEditorInit();
        }

        private void frmEditor_Item_Load(object sender, EventArgs e)
        {
            nudIcon.Maximum = GameState.NumItems;
            nudPaperdoll.Maximum = GameState.NumPaperdolls;

            // populate combo boxes
            cmbAnimation.Items.Clear();
            for (int i = 0; i < Constant.MAX_ANIMATIONS; i++)
                cmbAnimation.Items.Add(i + 1 + ": " + Core.Type.Animation[i].Name);

            cmbAmmo.Items.Clear();
            for (int i = 0; i < Constant.MAX_ITEMS; i++)
                cmbAmmo.Items.Add(i + 1 + ": " + Core.Type.Item[i].Name);

            cmbProjectile.Items.Clear();
            for (int i = 0; i < Constant.MAX_PROJECTILES; i++)
                cmbProjectile.Items.Add(i + 1 + ": " + Core.Type.Projectile[i].Name);

            cmbSkills.Items.Clear();
            for (int i = 0; i < Constant.MAX_SKILLS; i++)
                cmbSkills.Items.Add(i + 1 + ": " + Core.Type.Skill[i].Name);

            cmbPet.Items.Clear();
            for (int i = 0; i < Constant.MAX_PETS; i++)
                cmbPet.Items.Add(i + 1 + ": " + Core.Type.Pet[i].Name);

            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i < Constant.MAX_ITEMS; i++)
                lstIndex.Items.Add(i + 1 + ": " + Core.Type.Item[i].Name);
            nudPaperdoll.Maximum = GameState.NumPaperdolls;
            nudSpanwAmount.Maximum = int.MaxValue;
        }

        private void BtnBasics_Click(object sender, EventArgs e)
        {
            fraBasics.Visible = true;
            fraRequirements.Visible = false;
        }

        private void BtnRequirements_Click(object sender, EventArgs e)
        {
            fraBasics.Visible = false;
            fraRequirements.Visible = true;
        }

        #endregion

        #region Basics

        private void NudPic_Click(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Icon = (int)Math.Round(nudIcon.Value);
            DrawIcon();
        }

        private void CmbBind_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].BindType = (byte)cmbBind.SelectedIndex;
        }

        private void NudRarity_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Rarity = (byte)Math.Round(nudRarity.Value);
        }

        private void CmbAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Animation = cmbAnimation.SelectedIndex;
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubType.Enabled = false;

            if (cmbType.SelectedIndex == (int)Core.Enum.ItemType.Equipment)
            {
                fraEquipment.Visible = true;

                // Build subtype cmb
                cmbSubType.Items.Clear();

                cmbSubType.Items.Add("Armor");
                cmbSubType.Items.Add("Helmet");
                cmbSubType.Items.Add("Shield");

                cmbSubType.Enabled = true;
                cmbSubType.SelectedIndex = Core.Type.Item[GameState.EditorIndex].SubType;

                if (Core.Type.Item[GameState.EditorIndex].SubType == (byte)Core.Enum.EquipmentType.Weapon)
                {
                    fraProjectile.Visible = true;
                }
                else
                {
                    fraProjectile.Visible = false;
                }
            }
            else
            {
                fraEquipment.Visible = false;
            }

            if (cmbType.SelectedIndex == (int)Core.Enum.ItemType.Consumable)
            {
                fraVitals.Visible = true;

                // Build subtype cmb
                cmbSubType.Items.Clear();

                cmbSubType.Items.Add("HP");
                cmbSubType.Items.Add("SP");
                cmbSubType.Items.Add("Exp");

                cmbSubType.Enabled = true;
                cmbSubType.SelectedIndex = Core.Type.Item[GameState.EditorIndex].SubType;
            }
            else
            {
                fraVitals.Visible = false;
            }

            if (cmbType.SelectedIndex == (int)Core.Enum.ItemType.Skill)
            {
                fraSkill.Visible = true;
            }
            else
            {
                fraSkill.Visible = false;
            }

            if (cmbType.SelectedIndex == (int)Core.Enum.ItemType.Projectile)
            {
                fraProjectile.Visible = true;
                fraEquipment.Visible = true;
            }
            else if (cmbType.SelectedIndex != (int)Core.Enum.ItemType.Equipment)
            {
                fraProjectile.Visible = false;
            }

            if (cmbType.SelectedIndex == (int)Core.Enum.ItemType.Pet)
            {
                fraPet.Visible = true;
                fraEquipment.Visible = true;
            }
            else
            {
                fraPet.Visible = false;
            }

            if (cmbType.SelectedIndex == (int)Core.Enum.ItemType.CommonEvent)
            {
                fraEvents.Visible = true;

                // Build subtype cmb
                cmbSubType.Items.Clear();

                cmbSubType.Items.Add("Switches");
                cmbSubType.Items.Add("Variables");
                cmbSubType.Items.Add("Custom Script");
                cmbSubType.Items.Add("Key");

                cmbSubType.Enabled = true;
                cmbSubType.SelectedIndex = Core.Type.Item[GameState.EditorIndex].SubType;
            }
            else
            {
                fraEvents.Visible = false;
            }

            Core.Type.Item[GameState.EditorIndex].Type = (byte)cmbType.SelectedIndex;
        }

        private void NudVitalMod_Click(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Data1 = (int)Math.Round(nudVitalMod.Value);
        }

        private void CmbSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Data1 = cmbSkills.SelectedIndex;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Core.Type.Item[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Item[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void NudPrice_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Price = (int)Math.Round(nudPrice.Value);
        }

        private void ChkStackable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStackable.Checked == true)
            {
                Core.Type.Item[GameState.EditorIndex].Stackable = 1;
            }
            else
            {
                Core.Type.Item[GameState.EditorIndex].Stackable = 0;
            }
        }

        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Description = Strings.Trim(txtDescription.Text);
        }

        private void CmbSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].SubType = (byte)cmbSubType.SelectedIndex;

            if (Core.Type.Item[GameState.EditorIndex].SubType == (byte)Core.Enum.EquipmentType.Weapon)
            {
                fraProjectile.Visible = true;
            }
            else
            {
                fraProjectile.Visible = false;
            }
        }

        private void NuditemLvl_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].ItemLevel = (byte)Math.Round(nudItemLvl.Value);
        }

        private void CmbPet_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Data1 = cmbPet.SelectedIndex;
        }

        private void nudEvents_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Data1 = (int)Math.Round(nudVitalMod.Value);
        }

        #endregion

        #region Requirements

        private void CmbJobReq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].JobReq = cmbJobReq.SelectedIndex;
        }

        private void CmbAccessReq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].AccessReq = cmbAccessReq.SelectedIndex;
        }

        private void NudLevelReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].LevelReq = (int)Math.Round(nudLevelReq.Value);
        }

        private void NudStrReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Stat_Req[(int)Core.Enum.StatType.Strength] = (byte)Math.Round(nudStrReq.Value);
        }

        private void NudVitReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Stat_Req[(int)Core.Enum.StatType.Vitality] = (byte)Math.Round(nudVitReq.Value);
        }

        private void NudLuckReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Stat_Req[(int)Core.Enum.StatType.Luck] = (byte)Math.Round(nudLuckReq.Value);
        }

        private void NudIntReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Stat_Req[(int)Core.Enum.StatType.Intelligence] = (byte)Math.Round(nudIntReq.Value);
        }

        private void NudSprReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Stat_Req[(int)Core.Enum.StatType.Spirit] = (byte)Math.Round(nudSprReq.Value);
        }

        #endregion

        #region Equipment

        private void CmbTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Data3 = cmbTool.SelectedIndex;
        }

        private void NudDamage_ValueChanged(object sender, EventArgs e)
        {

            Core.Type.Item[GameState.EditorIndex].Data2 = (int)Math.Round(nudDamage.Value);
        }

        private void NudSpeed_ValueChanged(object sender, EventArgs e)
        {
            lblSpeed.Text = "Speed: " + nudSpeed.Value / 1000m + " sec";
            Core.Type.Item[GameState.EditorIndex].Speed = (int)Math.Round(nudSpeed.Value);
        }

        private void NudPaperdoll_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Paperdoll = (int)Math.Round(nudPaperdoll.Value);
            DrawPaperdoll();
        }

        private void NudStrength_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Add_Stat[(int)Core.Enum.StatType.Strength] = (byte)Math.Round(nudStrength.Value);
        }

        private void NudLuck_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Add_Stat[(int)Core.Enum.StatType.Luck] = (byte)Math.Round(nudLuck.Value);
        }

        private void NudIntelligence_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Add_Stat[(int)Core.Enum.StatType.Intelligence] = (byte)Math.Round(nudIntelligence.Value);
        }

        private void NudVitality_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Add_Stat[(int)Core.Enum.StatType.Vitality] = (byte)Math.Round(nudVitality.Value);
        }

        private void NudSpirit_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Add_Stat[(int)Core.Enum.StatType.Spirit] = (byte)Math.Round(nudSpirit.Value);
        }

        private void ChkKnockBack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKnockBack.Checked == true)
            {
                Core.Type.Item[GameState.EditorIndex].KnockBack = 1;
            }
            else
            {
                Core.Type.Item[GameState.EditorIndex].KnockBack = 0;
            }
        }

        private void CmbKnockBackTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].KnockBackTiles = (byte)cmbKnockBackTiles.SelectedIndex;
        }

        private void CmbProjectile_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Projectile = cmbProjectile.SelectedIndex;
        }

        private void CmbAmmo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Item[GameState.EditorIndex].Ammo = cmbAmmo.SelectedIndex;
        }

        private void btnSpawn_Click(object sender, EventArgs e)
        {
            NetworkSend.SendSpawnItem(GameState.EditorIndex, (int)Math.Round(nudSpanwAmount.Value));
        }

        private void frmEditor_Item_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.ItemEditorCancel();
        }

        public void DrawIcon()
        {
            int itemnum;

            itemnum = (int)Math.Round(nudIcon.Value);

            if (itemnum < 1 | itemnum > GameState.NumItems)
            {
                picItem.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Items, itemnum + GameState.GfxExt)))
            {
                picItem.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Items, itemnum + GameState.GfxExt));
            }
            else
            {
                picItem.BackgroundImage = null;
            }
        }

        private void DrawPaperdoll()
        {
            int Sprite;

            Sprite = (int)Math.Round(nudPaperdoll.Value);

            if (Sprite < 1 | Sprite > GameState.NumPaperdolls)
            {
                picPaperdoll.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Paperdolls, Sprite + GameState.GfxExt)))
            {
                picPaperdoll.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Paperdolls, Sprite + GameState.GfxExt));
            }
        }

        #endregion

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}