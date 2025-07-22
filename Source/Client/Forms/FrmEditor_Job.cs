﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;

namespace Client
{

    internal partial class frmEditor_Job
    {
        public frmEditor_Job()
        {
            InitializeComponent();
        }

        #region Frm

        private void frmEditor_Job_Load(object sender, EventArgs e)
        {
            nudMaleSprite.Maximum = GameState.NumCharacters;
            nudFemaleSprite.Maximum = GameState.NumCharacters;

            cmbItems.Items.Clear();

            for (int i = 0; i < Constant.MAX_ITEMS; i++)
                cmbItems.Items.Add(i + 1 + ": " + Core.Data.Item[i].Name);

            lstIndex.Items.Clear();

            for (int i = 0; i < Constant.MAX_JOBS; i++)
                lstIndex.Items.Add(i + 1 + ": " + Data.Job[i].Name);

            lstStartItems.Items.Clear();

            for (int i = 0; i < Constant.MAX_DROP_ITEMS; i++)
                lstStartItems.Items.Add(Core.Data.Item[Data.Job[GameState.EditorIndex].StartItem[i]].Name + " X " + Data.Job[GameState.EditorIndex].StartValue[i]);

            lstStartItems.SelectedIndex = 0;
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.JobEditorOK();
            Dispose();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.JobEditorCancel();
            Dispose();
        }

        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].Desc = txtDescription.Text;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Data.Job[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Data.Job[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        #endregion

        #region Sprites

        public void DrawPreview()
        {

            if (File.Exists(Core.Path.Graphics + @"Characters\" + nudMaleSprite.Value + GameState.GfxExt))
            {
                picMale.Width = Image.FromFile(Core.Path.Graphics + @"characters\" + nudMaleSprite.Value + GameState.GfxExt).Width / 4;
                picMale.Height = Image.FromFile(Core.Path.Graphics + @"characters\" + nudMaleSprite.Value + GameState.GfxExt).Height / 4;
                picMale.BackgroundImage = Image.FromFile(Core.Path.Graphics + @"Characters\" + nudMaleSprite.Value + GameState.GfxExt);
            }
            else
            {
                picMale.BackgroundImage = null;
            }

            if (File.Exists(Core.Path.Graphics + @"Characters\" + nudFemaleSprite.Value + GameState.GfxExt))
            {
                picFemale.Width = Image.FromFile(Core.Path.Graphics + @"characters\" + nudFemaleSprite.Value + GameState.GfxExt).Width / 4;
                picFemale.Height = Image.FromFile(Core.Path.Graphics + @"characters\" + nudFemaleSprite.Value + GameState.GfxExt).Height / 4;
                picFemale.BackgroundImage = Image.FromFile(Core.Path.Graphics + @"Characters\" + nudFemaleSprite.Value + GameState.GfxExt);
            }
            else
            {
                picFemale.BackgroundImage = null;
            }

        }

        #endregion

        #region Stats

        private void NumStrength_ValueChanged(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].Stat[(int)Core.Stat.Strength] = (int)Math.Round(nudStrength.Value);
        }

        private void NumLuck_ValueChanged(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].Stat[(int)Core.Stat.Luck] = (int)Math.Round(nudLuck.Value);
        }

        private void NumEndurance_ValueChanged(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].Stat[(int)Core.Stat.Luck] = (int)Math.Round(nudEndurance.Value);
        }

        private void NumIntelligence_ValueChanged(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].Stat[(int)Core.Stat.Intelligence] = (int)Math.Round(nudIntelligence.Value);
        }

        private void NumVitality_ValueChanged(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].Stat[(int)Core.Stat.Vitality] = (int)Math.Round(nudVitality.Value);
        }

        private void NumSpirit_ValueChanged(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].Stat[(int)Core.Stat.Spirit] = (int)Math.Round(nudSpirit.Value);
        }

        private void NumBaseExp_ValueChanged(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].BaseExp = (int)Math.Round(nudBaseExp.Value);
        }

        #endregion

        #region Start Items

        private void BtnItemAdd_Click(object sender, EventArgs e)
        {
            if (lstStartItems.SelectedIndex < 0)
                return;

            Data.Job[GameState.EditorIndex].StartItem[lstStartItems.SelectedIndex] = cmbItems.SelectedIndex;
            Data.Job[GameState.EditorIndex].StartValue[lstStartItems.SelectedIndex] = (int)Math.Round(nudItemAmount.Value);

            lstStartItems.Items.Clear();
            for (int i = 0; i < Constant.MAX_DROP_ITEMS; i++)
                lstStartItems.Items.Add(Core.Data.Item[Data.Job[GameState.EditorIndex].StartItem[i]].Name + " X " + Data.Job[GameState.EditorIndex].StartValue[i]);
            lstStartItems.SelectedIndex = 0;
        }

        #endregion

        #region Starting Point

        private void NumStartMap_Click(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].StartMap = (int)Math.Round(nudStartMap.Value);
        }

        private void NumStartX_Click(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].StartX = (byte)Math.Round(nudStartX.Value);
        }

        private void NumStartY_Click(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].StartY = (byte)Math.Round(nudStartY.Value);
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.JobEditorInit();
        }

        private void frmEditor_Job_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.JobEditorCancel();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Database.ClearJob(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Data.Job[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.JobEditorInit();
        }

        private void nudFemaleSprite_Click(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].FemaleSprite = (int)Math.Round(nudFemaleSprite.Value);
            DrawPreview();
        }

        private void nudMaleSprite_Click(object sender, EventArgs e)
        {
            Data.Job[GameState.EditorIndex].MaleSprite = (int)Math.Round(nudMaleSprite.Value);
            DrawPreview();
        }

        #endregion

    }
}