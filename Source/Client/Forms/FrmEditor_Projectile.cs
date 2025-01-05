using System;
using System.IO;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;

namespace Client
{

    internal partial class frmEditor_Projectile
    {
        public frmEditor_Projectile()
        {
            InitializeComponent();
        }

        private void frmEditor_Projectile_Load(object sender, EventArgs e)
        {
            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i < Constant.MAX_PROJECTILES;  i++)
                lstIndex.Items.Add(i + 1 + ": " + Core.Type.Projectile[i].Name);
            nudPic.Maximum = GameState.NumProjectiles;
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.ProjectileEditorInit();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.ProjectileEditorOk();
            Dispose();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.ProjectileEditorCancel();
            Dispose();
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;

            tmpindex = lstIndex.SelectedIndex;
            Core.Type.Projectile[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex - 1);
            lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex + 1 + ": " + Core.Type.Projectile[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void NudPic_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Projectile[GameState.EditorIndex].Sprite = (int)Math.Round(nudPic.Value);
            Drawicon();
        }

        private void NudRange_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Projectile[GameState.EditorIndex].Range = (byte)Math.Round(nudRange.Value);
        }

        private void NudSpeed_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Projectile[GameState.EditorIndex].Speed = (int)Math.Round(nudSpeed.Value);
        }

        private void NudDamage_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Projectile[GameState.EditorIndex].Damage = (int)Math.Round(nudDamage.Value);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Projectile.ClearProjectile(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex - 1);
            lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex + 1 + ": " + Core.Type.Projectile[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.ProjectileEditorInit();
        }

        private void frmEditor_Projectile_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.ProjectileEditorCancel();
        }

        private void Drawicon()
        {
            int iconnum;

            iconnum = (int)Math.Round(nudPic.Value);

            if (iconnum < 1 | iconnum > GameState.NumProjectiles)
            {
                picProjectile.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Projectiles, iconnum + GameState.GfxExt)))
            {
                picProjectile.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Projectiles, iconnum + GameState.GfxExt));
            }

        }

    }
}