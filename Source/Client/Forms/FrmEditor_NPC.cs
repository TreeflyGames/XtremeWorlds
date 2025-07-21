using System;
using System.IO;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;

namespace Client
{

    internal partial class frmEditor_Npc
    {
        public frmEditor_Npc()
        {
            InitializeComponent();
        }

        #region Frm

        private void frmEditor_Npc_Load(object sender, EventArgs e)
        {
            nudSprite.Maximum = GameState.NumCharacters;

            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i < Constant.MAX_NPCS; i++)
                lstIndex.Items.Add(i + 1 + ": " + Strings.Trim(Data.Npc[i].Name));

            // populate combo boxes
            cmbAnimation.Items.Clear();
            for (int i = 0; i < Constant.MAX_ANIMATIONS; i++)
                cmbAnimation.Items.Add(i + 1 + ": " + Data.Animation[i].Name);

            cmbSkill1.Items.Clear();
            cmbSkill2.Items.Clear();
            cmbSkill3.Items.Clear();
            cmbSkill4.Items.Clear();
            cmbSkill5.Items.Clear();
            cmbSkill6.Items.Clear();
            for (int i = 0; i < Constant.MAX_SKILLS; i++)
            {
                cmbSkill1.Items.Add(i + 1 + ": " + Data.Skill[i].Name);
                cmbSkill2.Items.Add(i + 1 + ": " + Data.Skill[i].Name);
                cmbSkill3.Items.Add(i + 1 + ": " + Data.Skill[i].Name);
                cmbSkill4.Items.Add(i + 1 + ": " + Data.Skill[i].Name);
                cmbSkill5.Items.Add(i + 1 + ": " + Data.Skill[i].Name);
                cmbSkill6.Items.Add(i + 1 + ": " + Data.Skill[i].Name);
            }

            cmbItem.Items.Clear();
            for (int i = 0; i < Constant.MAX_ITEMS; i++)
                cmbItem.Items.Add(i + 1 + ": " + Core.Data.Item[i].Name);
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

        public void DrawSprite()
        {
            int Sprite = (int)Math.Round(nudSprite.Value);

            if (Sprite < 1 | Sprite > GameState.NumCharacters)
            {
                picSprite.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Characters, Sprite + GameState.GfxExt)))
            {
                picSprite.Width = (int)Math.Round(System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Characters, Sprite + GameState.GfxExt)).Width / 4d);
                picSprite.Height = (int)Math.Round(System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Characters, Sprite + GameState.GfxExt)).Height / 4d);
                picSprite.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Characters, Sprite + GameState.GfxExt));
            }
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.NpcEditorInit();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.NpcEditorOK();
            Dispose();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Database.ClearNpc(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Data.Npc[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.NpcEditorInit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.NpcEditorCancel();
            Dispose();
        }

        #endregion

        #region Properties

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Data.Npc[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Data.Npc[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void TxtAttackSay_TextChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].AttackSay = txtAttackSay.Text;
        }

        private void NudSprite_Click(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Sprite = (int)Math.Round(nudSprite.Value);

            DrawSprite();
        }

        private void NudRange_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Range = (byte)Math.Round(nudRange.Value);
        }

        private void CmbBehavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Behaviour = (byte)cmbBehaviour.SelectedIndex;
        }

        private void CmbFaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Faction = (byte)cmbFaction.SelectedIndex;
        }

        private void CmbAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Animation = cmbAnimation.SelectedIndex;
        }

        private void NudSpawnSecs_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].SpawnSecs = (int)Math.Round(nudSpawnSecs.Value);
        }

        private void NudHp_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].HP = (int)Math.Round(nudHp.Value);
        }

        private void NudExp_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Exp = (int)Math.Round(nudExp.Value);
        }

        private void NudLevel_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Level = (byte)Math.Round(nudLevel.Value);
        }

        private void NudDamage_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Damage = (int)Math.Round(nudDamage.Value);
        }

        private void CmbSpawnPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].SpawnTime = (byte)cmbSpawnPeriod.SelectedIndex;
        }

        #endregion

        #region Stats

        private void NudStrength_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Stat[(int)Core.Stat.Strength] = (byte)Math.Round(nudStrength.Value);
        }

        private void NudVitality_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Stat[(int)Core.Stat.Vitality] = (byte)Math.Round(nudVitality.Value);
        }

        private void NudLuck_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Stat[(int)Core.Stat.Luck] = (byte)Math.Round(nudLuck.Value);
        }

        private void NudIntelligence_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Stat[(int)Core.Stat.Intelligence] = (byte)Math.Round(nudIntelligence.Value);
        }

        private void NudSpirit_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Stat[(int)Core.Stat.Spirit] = (byte)Math.Round(nudSpirit.Value);
        }

        #endregion

        #region Drop Items

        private void CmbDropSlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbItem.SelectedIndex = Data.Npc[GameState.EditorIndex].DropItem[cmbDropSlot.SelectedIndex];

            nudAmount.Value = Data.Npc[GameState.EditorIndex].DropItemValue[cmbDropSlot.SelectedIndex];

            nudChance.Value = Data.Npc[GameState.EditorIndex].DropChance[cmbDropSlot.SelectedIndex];
        }

        private void CmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].DropItem[cmbDropSlot.SelectedIndex] = cmbItem.SelectedIndex;
        }

        private void ScrlValue_Scroll(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].DropItemValue[cmbDropSlot.SelectedIndex] = (int)Math.Round(nudAmount.Value);
        }

        private void NudChance_ValueChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].DropChance[cmbDropSlot.SelectedIndex] = (int)Math.Round(nudChance.Value);
        }

        #endregion

        #region Skills

        private void CmbSkill1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Skill[1] = (byte)cmbSkill1.SelectedIndex;
        }

        private void CmbSkill2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Skill[2] = (byte)cmbSkill2.SelectedIndex;
        }

        private void CmbSkill3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Skill[3] = (byte)cmbSkill3.SelectedIndex;
        }

        private void CmbSkill4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Skill[4] = (byte)cmbSkill4.SelectedIndex;
        }

        private void CmbSkill5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Skill[5] = (byte)cmbSkill5.SelectedIndex;
        }

        private void CmbSkill6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.Npc[GameState.EditorIndex].Skill[6] = (byte)cmbSkill6.SelectedIndex;
        }

        private void frmEditor_Npc_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.NpcEditorCancel();
        }

        #endregion

    }
}