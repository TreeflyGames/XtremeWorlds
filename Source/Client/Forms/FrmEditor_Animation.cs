using Core;
using DarkUI.Controls;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Graphics;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using RenderTargetUsage = Microsoft.Xna.Framework.Graphics.RenderTargetUsage;

namespace Client
{
    internal partial class frmEditor_Animation
    {

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

        private void NudSprite0_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].Sprite[0] = (int)Math.Round(nudSprite0.Value);
        }

        private void NudSprite1_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].Sprite[1] = (int)Math.Round(nudSprite1.Value);
        }

        private void NudLoopCount0_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].LoopCount[0] = (int)Math.Round(nudLoopCount0.Value);
        }

        private void NudLoopCount1_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].LoopCount[1] = (int)Math.Round(nudLoopCount1.Value);
        }

        private void NudFrameCount0_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].Frames[0] = (int)Math.Round(nudFrameCount0.Value);
        }

        private void NudFrameCount1_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].Frames[1] = (int)Math.Round(nudFrameCount1.Value);
        }

        private void NudLoopTime0_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].LoopTime[0] = (int)Math.Round(nudLoopTime0.Value);
        }

        private void NudLoopTime1_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].LoopTime[1] = (int)Math.Round(nudLoopTime1.Value);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Editors.AnimationEditorOK();
            Dispose();
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            int tmpindex;
            tmpindex = lstIndex.SelectedIndex;
            Core.Type.Animation[GameState.EditorIndex].Name = Strings.Trim(txtName.Text);
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Animation[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;
        }

        private void lstIndex_Click(object sender, EventArgs e)
        {
            Editors.AnimationEditorInit();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int tmpindex;

            Animation.ClearAnimation(GameState.EditorIndex);

            tmpindex = lstIndex.SelectedIndex;
            lstIndex.Items.RemoveAt(GameState.EditorIndex);
            lstIndex.Items.Insert(GameState.EditorIndex, GameState.EditorIndex + 1 + ": " + Core.Type.Animation[GameState.EditorIndex].Name);
            lstIndex.SelectedIndex = tmpindex;

            Editors.AnimationEditorInit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Editors.AnimationEditorCancel();
            Dispose();
        }

        private void frmEditor_Animation_Load(object sender, EventArgs e)
        {
            lstIndex.Items.Clear();

            // Add the names
            for (int i = 0; i < Core.Constant.MAX_ANIMATIONS; i++)
                lstIndex.Items.Add(i + 1 + ": " + Core.Type.Animation[i].Name);

            // find the music we have set
            cmbSound.Items.Clear();

            General.CacheSound();

            for (int i = 0, loopTo = Information.UBound(Sound.SoundCache); i < loopTo; i++)
                cmbSound.Items.Add(Sound.SoundCache[i]);

            nudSprite0.Maximum = GameState.NumAnimations;
            nudSprite1.Maximum = GameState.NumAnimations;
        }

        private void CmbSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].Sound = cmbSound.SelectedItem.ToString();
        }

        private void frmEditor_Animation_FormClosing(object sender, FormClosingEventArgs e)
        {
            Editors.AnimationEditorCancel();
        }

        public void ProcessAnimation(ref DarkNumericUpDown animationControl, ref DarkNumericUpDown frameCountControl, ref DarkNumericUpDown loopCountControl, int animationTimerIndex, RenderTarget2D renderTarget, ref PictureBox backgroundColorControl, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            try
            {
                int animationNum = (int)Math.Round(animationControl.Value);
                if (animationNum <= 0 || animationNum > GameState.NumAnimations)
                {
                    if (backgroundColorControl.Image != null)
                        backgroundColorControl.Image = null;
                    return;
                }

                var imagePath = System.IO.Path.Combine(Core.Path.Animations, animationNum + GameState.GfxExt);
                if (!System.IO.File.Exists(imagePath))
                {
                    backgroundColorControl.Image = null;
                    return;
                }

                using (var img = System.Drawing.Image.FromFile(imagePath))
                {
                    int columns = (int)Math.Round(frameCountControl.Value);
                    if (columns <= 0)
                    {
                        backgroundColorControl.Image = (System.Drawing.Image)img.Clone();
                        return;
                    }

                    int frameWidth = img.Width / columns;
                    int frameHeight = img.Height;
                    int rows = frameHeight > 0 ? img.Height / frameHeight : 1;
                    int frameCount = rows * columns;

                    int looptime = (int)Math.Round(loopCountControl.Value);
                    if (GameState.AnimEditorTimer[animationTimerIndex] + looptime <= Environment.TickCount)
                    {
                        if (GameState.AnimEditorFrame[animationTimerIndex] >= frameCount)
                        {
                            GameState.AnimEditorFrame[animationTimerIndex] = 1;
                        }
                        else
                        {
                            GameState.AnimEditorFrame[animationTimerIndex] += 1;
                        }
                        GameState.AnimEditorTimer[animationTimerIndex] = Environment.TickCount;
                    }

                    if (frameCountControl.Value > 0m)
                    {
                        int frameIndex = GameState.AnimEditorFrame[animationTimerIndex] - 1;
                        int column = frameIndex % columns;
                        int row = frameIndex / columns;

                        var sRECT = new System.Drawing.Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
                        var bmp = new System.Drawing.Bitmap(frameWidth, frameHeight);
                        using (var g = System.Drawing.Graphics.FromImage(bmp))
                        {
                            g.Clear(backgroundColorControl.BackColor);
                            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, frameWidth, frameHeight), sRECT, System.Drawing.GraphicsUnit.Pixel);
                        }
                        backgroundColorControl.Image = bmp;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing animation: {ex.Message}");
                backgroundColorControl.Image = null;
            }
        }

        private void picSprite0_Paint(object sender, PaintEventArgs e)
        {
            // Call ProcessAnimation for Sprite0
            ProcessAnimation(
                ref nudSprite0,
                ref nudFrameCount0,
                ref nudLoopTime0,
                0,
                null,
                ref picSprite0,
                null
            );
        }

        private void picSprite1_Paint(object sender, PaintEventArgs e)
        {
            // Call ProcessAnimation for Sprite1
            ProcessAnimation(
                ref nudSprite1,
                ref nudFrameCount1,
                ref nudLoopTime1,
                1,
                null,
                ref picSprite1,
                null
            );
        }
    }
}