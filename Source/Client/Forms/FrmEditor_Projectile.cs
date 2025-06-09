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
            Editors.ProjectileEditorInit();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.ProjectileEditorOK();
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
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Projectile[GameState.EditorIndex].Name);
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
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Projectile[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.ProjectileEditorInit();
        }

        private void frmEditor_Projectile_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.ProjectileEditorCancel();
        }

        public void Drawicon()
        {
            int iconNum;

            iconNum = (int)Math.Round(nudPic.Value);

            if (iconNum < 1 | iconNum > GameState.NumProjectiles)
            {
                picProjectile.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Projectiles, iconNum + GameState.GfxExt)))
            {
                picProjectile.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Projectiles, iconNum + GameState.GfxExt));
            }

        }

    }
}