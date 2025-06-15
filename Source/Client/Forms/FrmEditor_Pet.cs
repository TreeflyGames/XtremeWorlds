using System;
using System.IO;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;

namespace Client
{
    internal partial class frmEditor_Pet
    {
        public frmEditor_Pet()
        {
            InitializeComponent();
        }

        #region Basics

        private void frmEditor_Pet_Load(object sender, EventArgs e)
        {
            nudSprite.Maximum = GameState.NumCharacters;
            nudRange.Maximum = 50m;
            nudLevel.Maximum = Constant.MAX_LEVEL;
            nudMaxLevel.Maximum = Constant.MAX_LEVEL;

            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i < Constant.MAX_PETS; i++)
                lstIndex.Items.Add(i + 1 + ": " + Core.Type.Pet[i].Name);

            cmbEvolve.Items.Clear();
            // Add the names
            for (int i = 0; i < Constant.MAX_PETS; i++)
                cmbEvolve.Items.Add(i + 1 + ": " + Core.Type.Pet[i].Name);
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

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.PetEditorInit();
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Core.Type.Pet[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Pet[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void NudSprite_Click(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Sprite = (int)Math.Round(nudSprite.Value);

            EditorPet_DrawPet();
        }

        internal void EditorPet_DrawPet()
        {
            int petNum;

            petNum = (int)Math.Round(nudSprite.Value);

            if (petNum <= 0 | petNum > GameState.NumCharacters)
            {
                picSprite.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Characters, petNum + GameState.GfxExt)))
            {
                picSprite.Width = (int)Math.Round(System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Characters, petNum + GameState.GfxExt)).Width / 4d);
                picSprite.Height = (int)Math.Round(System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Characters, petNum + GameState.GfxExt)).Height / 4d);
                picSprite.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Characters, petNum + GameState.GfxExt));
            }
            else
            {
                picSprite.BackgroundImage = null;
            }

        }

        private void NudRange_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Range = (int)Math.Round(nudRange.Value);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.PetEditorOK();
            Dispose();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.PetEditorCancel();
            Dispose();
        }

        #endregion
        #region Stats

        private void OptCustomStats_CheckedChanged(object sender, EventArgs e)
        {
            if (optCustomStats.Checked == true)
            {
                pnlCustomStats.Visible = true;
                Core.Type.Pet[GameState.EditorIndex].StatType = 1;
            }
            else
            {
                pnlCustomStats.Visible = false;
                Core.Type.Pet[GameState.EditorIndex].StatType = 0;
            }
        }

        private void OptAdoptStats_CheckedChanged(object sender, EventArgs e)
        {
            if (optAdoptStats.Checked == true)
            {
                pnlCustomStats.Visible = false;
                Core.Type.Pet[GameState.EditorIndex].StatType = 0;
            }
            else
            {
                pnlCustomStats.Visible = true;
                Core.Type.Pet[GameState.EditorIndex].StatType = 1;
            }
        }

        private void NudStrength_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Strength] = (byte)Math.Round(nudStrength.Value);
        }

        private void NudVitality_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Vitality] = (byte)Math.Round(nudVitality.Value);
        }

        private void NudLuck_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Luck] = (byte)Math.Round(nudLuck.Value);
        }

        private void NudIntelligence_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Intelligence] = (byte)Math.Round(nudIntelligence.Value);
        }

        private void NudSpirit_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Spirit] = (byte)Math.Round(nudSpirit.Value);
        }

        private void NudLevel_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Level = (byte)Math.Round(nudLevel.Value);
        }




        #endregion
        #region Leveling

        private void NudPetExp_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].ExpGain = (int)Math.Round(nudPetExp.Value);
        }

        private void NudPetPnts_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Points = (byte)Math.Round(nudPetPnts.Value);
        }

        private void NudMaxLevel_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].MaxLevel = (int)Math.Round(nudMaxLevel.Value);
        }

        private void OptLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (optLevel.Checked == true)
            {
                pnlPetlevel.Visible = true;
                Core.Type.Pet[GameState.EditorIndex].LevelingType = 1;
            }
        }

        private void OptDoNotLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (optDoNotLevel.Checked == true)
            {
                pnlPetlevel.Visible = false;
                Core.Type.Pet[GameState.EditorIndex].LevelingType = 0;
            }
        }




        #endregion
        #region Skills

        private void CmbSkill1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Skill[0] = cmbSkill1.SelectedIndex;
        }

        private void CmbSkill2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Skill[1] = cmbSkill2.SelectedIndex;
        }

        private void CmbSkill3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Skill[2] = cmbSkill3.SelectedIndex;
        }

        private void CmbSkill4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].Skill[3] = cmbSkill4.SelectedIndex;
        }

        #endregion
        #region Evolving

        private void ChkEvolve_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEvolve.Checked == true)
            {
                Core.Type.Pet[GameState.EditorIndex].Evolvable = 1;
            }
            else
            {
                Core.Type.Pet[GameState.EditorIndex].Evolvable = 0;
            }
        }

        private void NudEvolveLvl_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].EvolveLevel = (int)Math.Round(nudEvolveLvl.Value);
        }

        private void CmbEvolve_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Pet[GameState.EditorIndex].EvolveNum = cmbEvolve.SelectedIndex;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Pet.ClearPet(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Pet[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.PetEditorInit();
        }

        private void frmEditor_Pet_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.PetEditorCancel();
        }

        #endregion

    }
}