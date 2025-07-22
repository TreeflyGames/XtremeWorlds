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

        #region Frm

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
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Data.Item[GameState.EditorIndex].Name);
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
                cmbAnimation.Items.Add(i + 1 + ": " + Data.Animation[i].Name);

            cmbAmmo.Items.Clear();
            for (int i = 0; i < Constant.MAX_ITEMS; i++)
                cmbAmmo.Items.Add(i + 1 + ": " + Core.Data.Item[i].Name);

            cmbProjectile.Items.Clear();
            for (int i = 0; i < Constant.MAX_PROJECTILES; i++)
                cmbProjectile.Items.Add(i + 1 + ": " + Data.Projectile[i].Name);

            cmbSkills.Items.Clear();
            for (int i = 0; i < Constant.MAX_SKILLS; i++)
                cmbSkills.Items.Add(i + 1 + ": " + Data.Skill[i].Name);

            cmbPet.Items.Clear();
            for (int i = 0; i < Constant.MAX_PETS; i++)
                cmbPet.Items.Add(i + 1 + ": " + Data.Pet[i].Name);

            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i < Constant.MAX_ITEMS; i++)
                lstIndex.Items.Add(i + 1 + ": " + Core.Data.Item[i].Name);
            nudPaperdoll.Maximum = GameState.NumPaperdolls;
            nudSpanwAmount.Maximum = int.MaxValue;
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
            Core.Data.Item[GameState.EditorIndex].Icon = (int)Math.Round(nudIcon.Value);
            DrawIcon();
        }

        private void CmbBind_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].BindType = (byte)cmbBind.SelectedIndex;
        }

        private void NudRarity_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Rarity = (byte)Math.Round(nudRarity.Value);
        }

        private void CmbAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Animation = cmbAnimation.SelectedIndex;
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSubType.Enabled = false;

            if (cmbType.SelectedIndex == (int)ItemCategory.Equipment)
            {
                fraEquipment.Visible = true;

                // Build subtype cmb
                cmbSubType.Items.Clear();

                cmbSubType.Items.Add("Armor");
                cmbSubType.Items.Add("Helmet");
                cmbSubType.Items.Add("Shield");

                cmbSubType.Enabled = true;
                cmbSubType.SelectedIndex = Core.Data.Item[GameState.EditorIndex].SubType;

                if (Core.Data.Item[GameState.EditorIndex].SubType == (byte)Equipment.Weapon)
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

            if (cmbType.SelectedIndex == (int)ItemCategory.Consumable)
            {
                fraVitals.Visible = true;

                // Build subtype cmb
                cmbSubType.Items.Clear();

                cmbSubType.Items.Add("HP");
                cmbSubType.Items.Add("SP");
                cmbSubType.Items.Add("Exp");

                cmbSubType.Enabled = true;
                cmbSubType.SelectedIndex = Core.Data.Item[GameState.EditorIndex].SubType;
            }
            else
            {
                fraVitals.Visible = false;
            }

            if (cmbType.SelectedIndex == (int)ItemCategory.Skill)
            {
                fraSkill.Visible = true;
            }
            else
            {
                fraSkill.Visible = false;
            }

            if (cmbType.SelectedIndex == (int)ItemCategory.Projectile)
            {
                fraProjectile.Visible = true;
                fraEquipment.Visible = true;
            }
            else if (cmbType.SelectedIndex != (int)ItemCategory.Equipment)
            {
                fraProjectile.Visible = false;
            }

            if (cmbType.SelectedIndex == (int)ItemCategory.Pet)
            {
                fraPet.Visible = true;
                fraEquipment.Visible = true;
            }
            else
            {
                fraPet.Visible = false;
            }

            if (cmbType.SelectedIndex == (int)ItemCategory.Event)
            {
                fraEvents.Visible = true;

                // Build subtype cmb
                cmbSubType.Items.Clear();

                cmbSubType.Items.Add("Switches");
                cmbSubType.Items.Add("Variables");
                cmbSubType.Items.Add("Custom Script");
                cmbSubType.Items.Add("Key");

                cmbSubType.Enabled = true;
                cmbSubType.SelectedIndex = Core.Data.Item[GameState.EditorIndex].SubType;
            }
            else
            {
                fraEvents.Visible = false;
            }

            Core.Data.Item[GameState.EditorIndex].Type = (byte)cmbType.SelectedIndex;
        }

        private void NudVitalMod_Click(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Data1 = (int)Math.Round(nudVitalMod.Value);
        }

        private void CmbSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Data1 = cmbSkills.SelectedIndex;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Core.Data.Item[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Data.Item[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void NudPrice_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Price = (int)Math.Round(nudPrice.Value);
        }

        private void ChkStackable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStackable.Checked == true)
            {
                Core.Data.Item[GameState.EditorIndex].Stackable = 1;
            }
            else
            {
                Core.Data.Item[GameState.EditorIndex].Stackable = 0;
            }
        }

        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Description = Strings.Trim(txtDescription.Text);
        }

        private void CmbSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].SubType = (byte)cmbSubType.SelectedIndex;

            if (Core.Data.Item[GameState.EditorIndex].SubType == (byte)Equipment.Weapon)
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
            Core.Data.Item[GameState.EditorIndex].ItemLevel = (byte)Math.Round(nudItemLvl.Value);
        }

        private void CmbPet_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Data1 = cmbPet.SelectedIndex;
        }

        private void nudEvents_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Data1 = (int)Math.Round(nudVitalMod.Value);
        }

        #endregion

        #region Requirements

        private void CmbJobReq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].JobReq = cmbJobReq.SelectedIndex;
        }

        private void CmbAccessReq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].AccessReq = cmbAccessReq.SelectedIndex;
        }

        private void NudLevelReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].LevelReq = (int)Math.Round(nudLevelReq.Value);
        }

        private void NudStrReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Stat_Req[(int)Core.Stat.Strength] = (byte)Math.Round(nudStrReq.Value);
        }

        private void NudVitReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Stat_Req[(int)Core.Stat.Vitality] = (byte)Math.Round(nudVitReq.Value);
        }

        private void NudLuckReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Stat_Req[(int)Core.Stat.Luck] = (byte)Math.Round(nudLuckReq.Value);
        }

        private void NudIntReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Stat_Req[(int)Core.Stat.Intelligence] = (byte)Math.Round(nudIntReq.Value);
        }

        private void NudSprReq_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Stat_Req[(int)Core.Stat.Spirit] = (byte)Math.Round(nudSprReq.Value);
        }

        #endregion

        #region Equipment

        private void CmbTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Data3 = cmbTool.SelectedIndex;
        }

        private void NudDamage_ValueChanged(object sender, EventArgs e)
        {

            Core.Data.Item[GameState.EditorIndex].Data2 = (int)Math.Round(nudDamage.Value);
        }

        private void NudSpeed_ValueChanged(object sender, EventArgs e)
        {
            lblSpeed.Text = "Speed: " + nudSpeed.Value / 1000m + " sec";
            Core.Data.Item[GameState.EditorIndex].Speed = (int)Math.Round(nudSpeed.Value);
        }

        private void NudPaperdoll_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Paperdoll = (int)Math.Round(nudPaperdoll.Value);
            DrawPaperdoll();
        }

        private void NudStrength_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Add_Stat[(int)Core.Stat.Strength] = (byte)Math.Round(nudStrength.Value);
        }

        private void NudLuck_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Add_Stat[(int)Core.Stat.Luck] = (byte)Math.Round(nudLuck.Value);
        }

        private void NudIntelligence_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Add_Stat[(int)Core.Stat.Intelligence] = (byte)Math.Round(nudIntelligence.Value);
        }

        private void NudVitality_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Add_Stat[(int)Core.Stat.Vitality] = (byte)Math.Round(nudVitality.Value);
        }

        private void NudSpirit_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Add_Stat[(int)Core.Stat.Spirit] = (byte)Math.Round(nudSpirit.Value);
        }

        private void ChkKnockBack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKnockBack.Checked == true)
            {
                Core.Data.Item[GameState.EditorIndex].KnockBack = 1;
            }
            else
            {
                Core.Data.Item[GameState.EditorIndex].KnockBack = 0;
            }
        }

        private void CmbKnockBackTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].KnockBackTiles = (byte)cmbKnockBackTiles.SelectedIndex;
        }

        private void CmbProjectile_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Projectile = cmbProjectile.SelectedIndex;
        }

        private void CmbAmmo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Item[GameState.EditorIndex].Ammo = cmbAmmo.SelectedIndex;
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

            picItem.Size = new Size(32, 32);
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
    }
}