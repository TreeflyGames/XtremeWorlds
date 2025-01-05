using System;
using System.IO;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;

namespace Client
{

    internal partial class frmEditor_NPC
    {
        public frmEditor_NPC()
        {
            InitializeComponent();
        }

        #region Form Code

        private void frmEditor_NPC_Load(object sender, EventArgs e)
        {
            nudSprite.Maximum = GameState.NumCharacters;

            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i <= Constant.MAX_NPCS - 1; i++)
                lstIndex.Items.Add(i + 1 + ": " + Strings.Trim(Core.Type.NPC[i].Name));

            // populate combo boxes
            cmbAnimation.Items.Clear();
            cmbAnimation.Items.Add("None");
            for (int i = 0; i < Constant.MAX_ANIMATIONS; i++)
                cmbAnimation.Items.Add(i + 1 + ": " + Core.Type.Animation[i].Name);

            cmbSkill1.Items.Clear();
            cmbSkill1.Items.Add("None");
            cmbSkill2.Items.Clear();
            cmbSkill2.Items.Add("None");
            cmbSkill3.Items.Clear();
            cmbSkill3.Items.Add("None");
            cmbSkill4.Items.Clear();
            cmbSkill4.Items.Add("None");
            cmbSkill5.Items.Clear();
            cmbSkill5.Items.Add("None");
            cmbSkill6.Items.Clear();
            cmbSkill6.Items.Add("None");
            for (int i = 0; i < Constant.MAX_SKILLS; i++)
            {
                cmbSkill1.Items.Add(i + 1 + ": " + Core.Type.Skill[i].Name);
                cmbSkill2.Items.Add(i + 1 + ": " + Core.Type.Skill[i].Name);
                cmbSkill3.Items.Add(i + 1 + ": " + Core.Type.Skill[i].Name);
                cmbSkill4.Items.Add(i + 1 + ": " + Core.Type.Skill[i].Name);
                cmbSkill5.Items.Add(i + 1 + ": " + Core.Type.Skill[i].Name);
                cmbSkill6.Items.Add(i + 1 + ": " + Core.Type.Skill[i].Name);
            }

            cmbItem.Items.Clear();
            cmbItem.Items.Add("None");
            for (int i = 0; i < Constant.MAX_ITEMS; i++)
                cmbItem.Items.Add(i + 1 + ": " + Core.Type.Item[i].Name);
        }

        private void DrawSprite()
        {
            int Sprite;

            Sprite = (int)Math.Round(nudSprite.Value);

            if (Sprite < 1 | Sprite > GameState.NumCharacters)
            {
                picSprite.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Characters, Sprite + GameState.GfxExt)))
            {
                picSprite.Width = (int)Math.Round(System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Characters, Sprite + GameState.GfxExt)).Width / 4d);
                picSprite.Height = (int)Math.Round(System.Drawing.Image.FromFile(System.IO.Path.Combine(Sprite.ToString(), GameState.GfxExt)).Height / 4d);
                picSprite.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Characters, Sprite + GameState.GfxExt));
            }
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.NPCEditorInit();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.NPCEditorOK();
            Dispose();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Database.ClearNPC(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.NPC[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.NPCEditorInit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.NPCEditorCancel();
            Dispose();
        }

        #endregion

        #region Properties

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Core.Type.NPC[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.NPC[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void TxtAttackSay_TextChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].AttackSay = txtAttackSay.Text;
        }

        private void NudSprite_Click(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Sprite = (int)Math.Round(nudSprite.Value);

            DrawSprite();
        }

        private void NudRange_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Range = (byte)Math.Round(nudRange.Value);
        }

        private void CmbBehavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Behaviour = (byte)cmbBehaviour.SelectedIndex;
        }

        private void CmbFaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Faction = (byte)cmbFaction.SelectedIndex;
        }

        private void CmbAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Animation = cmbAnimation.SelectedIndex;
        }

        private void NudSpawnSecs_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].SpawnSecs = (int)Math.Round(nudSpawnSecs.Value);
        }

        private void NudHp_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].HP = (int)Math.Round(nudHp.Value);
        }

        private void NudExp_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Exp = (int)Math.Round(nudExp.Value);
        }

        private void NudLevel_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Level = (int)Math.Round(nudLevel.Value);
        }

        private void NudDamage_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Damage = (int)Math.Round(nudDamage.Value);
        }

        private void CmbSpawnPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].SpawnTime = (byte)cmbSpawnPeriod.SelectedIndex;
        }

        #endregion

        #region Stats

        private void NudStrength_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Strength] = (byte)Math.Round(nudStrength.Value);
        }

        private void NudVitality_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Vitality] = (byte)Math.Round(nudVitality.Value);
        }

        private void NudLuck_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Luck] = (byte)Math.Round(nudLuck.Value);
        }

        private void NudIntelligence_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Intelligence] = (byte)Math.Round(nudIntelligence.Value);
        }

        private void NudSpirit_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Spirit] = (byte)Math.Round(nudSpirit.Value);
        }

        #endregion

        #region Drop Items

        private void CmbDropSlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbItem.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].DropItem[cmbDropSlot.SelectedIndex];

            nudAmount.Value = Core.Type.NPC[GameState.EditorIndex].DropItemValue[cmbDropSlot.SelectedIndex];

            nudChance.Value = Core.Type.NPC[GameState.EditorIndex].DropChance[cmbDropSlot.SelectedIndex];
        }

        private void CmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].DropItem[cmbDropSlot.SelectedIndex] = cmbItem.SelectedIndex;
        }

        private void ScrlValue_Scroll(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].DropItemValue[cmbDropSlot.SelectedIndex] = (int)Math.Round(nudAmount.Value);
        }

        private void NudChance_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].DropChance[cmbDropSlot.SelectedIndex] = (int)Math.Round(nudChance.Value);
        }

        #endregion

        #region Skills

        private void CmbSkill1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Skill[1] = (byte)cmbSkill1.SelectedIndex;
        }

        private void CmbSkill2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Skill[2] = (byte)cmbSkill2.SelectedIndex;
        }

        private void CmbSkill3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Skill[3] = (byte)cmbSkill3.SelectedIndex;
        }

        private void CmbSkill4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Skill[4] = (byte)cmbSkill4.SelectedIndex;
        }

        private void CmbSkill5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Skill[5] = (byte)cmbSkill5.SelectedIndex;
        }

        private void CmbSkill6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.NPC[GameState.EditorIndex].Skill[6] = (byte)cmbSkill6.SelectedIndex;
        }

        private void frmEditor_NPC_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.NPCEditorCancel();
        }

        #endregion

    }
}