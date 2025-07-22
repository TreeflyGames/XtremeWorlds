﻿using System;
using System.IO;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;

namespace Client
{

    internal partial class frmEditor_Skill
    {
        public frmEditor_Skill()
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

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;
            tmpindex = lstIndex.SelectedIndex;
            Core.Data.Skill[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Data.Skill[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Type = (byte)cmbType.SelectedIndex;
        }

        private void NudMp_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].MpCost = (int)Math.Round(nudMp.Value);
        }

        private void NudLevel_ValueChanged(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].LevelReq = (int)Math.Round(nudLevel.Value);
        }

        private void CmbAccessReq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].AccessReq = cmbAccessReq.SelectedIndex;
        }

        private void CmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].JobReq = cmbJob.SelectedIndex;
        }

        private void NudCast_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].CastTime = (int)Math.Round(nudCast.Value);
        }

        private void NudCool_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].CdTime = (int)Math.Round(nudCool.Value);
        }

        private void NudMap_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Map = (int)Math.Round(nudMap.Value);
        }

        private void CmbDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Dir = (byte)cmbDir.SelectedIndex;
        }

        private void NudX_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].X = (int)Math.Round(nudX.Value);
        }

        private void NudY_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Y = (int)Math.Round(nudY.Value);
        }

        private void NudVital_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Vital = (int)Math.Round(nudVital.Value);
        }

        private void NudDuration_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Duration = (int)Math.Round(nudDuration.Value);
        }

        private void NudInterval_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Interval = (int)Math.Round(nudInterval.Value);
        }

        private void NudRange_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Range = (int)Math.Round(nudRange.Value);
        }

        private void ChkAOE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAoE.Checked == false)
            {
                Core.Data.Skill[GameState.EditorIndex].IsAoE = false;
            }
            else
            {
                Core.Data.Skill[GameState.EditorIndex].IsAoE = true;
            }
        }

        private void NudAoE_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].AoE = (int)Math.Round(nudAoE.Value);
        }

        private void CmbAnimCast_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].CastAnim = cmbAnimCast.SelectedIndex;
        }

        private void CmbAnim_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].SkillAnim = cmbAnim.SelectedIndex;
        }

        private void NudStun_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].StunDuration = (int)Math.Round(nudStun.Value);
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.SkillEditorInit();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.SkillEditorOK();
            Dispose();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Database.ClearSkill(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Data.Skill[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.SkillEditorInit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.SkillEditorCancel();
            Dispose();
        }

        private void frmEditor_Skill_Load(object sender, EventArgs e)
        {
            nudIcon.Maximum = GameState.NumSkills;
            nudAoE.Maximum = byte.MaxValue;
            nudRange.Maximum = byte.MaxValue;
            nudMap.Maximum = Constant.MAX_MAPS;
            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i < Constant.MAX_SKILLS; i++)
                lstIndex.Items.Add(i + 1 + ": " + Strings.Trim(Core.Data.Skill[i].Name));

            cmbAnimCast.Items.Clear();
            cmbAnim.Items.Clear();
            for (int i = 0; i < Constant.MAX_ANIMATIONS; i++)
            {
                cmbAnimCast.Items.Add(i + 1 + ": " + Core.Data.Animation[i].Name);
                cmbAnim.Items.Add(i + 1 + ": " + Core.Data.Animation[i].Name);
            }

            cmbProjectile.Items.Clear();
            for (int i = 0; i < Constant.MAX_ANIMATIONS; i++)
                cmbProjectile.Items.Add(i + 1 + ": " + Core.Data.Projectile[i].Name);

            cmbJob.Items.Clear();
            for (int i = 0; i < Constant.MAX_JOBS; i++)
                cmbJob.Items.Add(i + 1 + ": " + Core.Data.Job[i].Name.Trim());
        }

        private void ChkProjectile_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProjectile.Checked == false)
            {
                Core.Data.Skill[GameState.EditorIndex].IsProjectile = 0;
            }
            else
            {
                Core.Data.Skill[GameState.EditorIndex].IsProjectile = 1;
            }
        }

        private void ScrlProjectile_Scroll(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Projectile = cmbProjectile.SelectedIndex;
        }

        private void ChkKnockBack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKnockBack.Checked == true)
            {
                Core.Data.Skill[GameState.EditorIndex].KnockBack = 1;
            }
            else
            {
                Core.Data.Skill[GameState.EditorIndex].KnockBack = 0;
            }
        }

        private void CmbKnockBackTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].KnockBackTiles = (byte)cmbKnockBackTiles.SelectedIndex;
        }

        private void frmEditor_Skill_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.SkillEditorCancel();
        }

        private void btnLearn_Click(object sender, EventArgs e)
        {
            NetworkSend.SendLearnSkill(GameState.EditorIndex);
        }

        private void nudIcon_Click(object sender, EventArgs e)
        {
            Core.Data.Skill[GameState.EditorIndex].Icon = (int)Math.Round(nudIcon.Value);
            DrawIcon();
        }

        public void DrawIcon()
        {
            int skillNum;
            skillNum = (int)Math.Round(nudIcon.Value);

            if (skillNum < 1 | skillNum > GameState.NumItems)
            {
                picSprite.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Skills, skillNum + GameState.GfxExt)))
            {
                picSprite.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Skills, skillNum + GameState.GfxExt));
            }
            else
            {
                picSprite.BackgroundImage = null;
            }
        }
    }
}