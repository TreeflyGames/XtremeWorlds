using Core;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Client
{

    internal partial class frmEditor_Animation
    {
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
            for (int i = 0; i < Constant.MAX_ANIMATIONS; i++)
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

        public void ProcessAnimation(NumericUpDown animationControl, NumericUpDown frameCountControl, NumericUpDown loopCountControl, int animationTimerIndex, RenderTarget2D renderTarget, PictureBox backgroundColorControl, SpriteBatch spriteBatch)
        {

            // Retrieve the animation number and check its validity
            int animationNum = (int)Math.Round(animationControl.Value);
            if (animationNum <= 0 | animationNum > GameState.NumAnimations)
            {
                spriteBatch.GraphicsDevice.Clear(GameClient.ToMonoGameColor(backgroundColorControl.BackColor));
                backgroundColorControl = new PictureBox();
                return;
            }

            // Check whether animationDisplay is Texture2D or System.Drawing.Image
            Texture2D texture;
            texture = GameClient.GetTexture(System.IO.Path.Combine(Core.Path.Animations, animationNum + GameState.GfxExt));
            if (texture is null)
            {
                return;
            }

            // Get dimensions and column count from controls and graphic info
            int totalWidth;
            int totalHeight;

            var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Animations, animationNum + GameState.GfxExt));
            totalWidth = gfxInfo.Width;
            totalHeight = gfxInfo.Height;

            int columns = (int)Math.Round(frameCountControl.Value);

            // Validate columns to avoid division by zero
            if (columns <= 0)
                return;

            // Calculate frame dimensions
            int frameWidth = (int)Math.Round(totalWidth / (double)columns);

            // Assuming square frames for simplicity (adjust if frames are not square)
            int frameHeight = frameWidth;

            var rows = default(int);
            if (frameHeight > 0)
            {
                rows = (int)Math.Round(totalHeight / (double)frameHeight);
            }

            int frameCount = rows * columns;

            // Retrieve loop timing and check frame rendering necessity
            int looptime = (int)Math.Round(loopCountControl.Value);
            if (GameState.AnimEditorTimer[animationTimerIndex] + looptime <= Environment.TickCount)
            {
                if (GameState.AnimEditorFrame[animationTimerIndex] >= frameCount)
                {
                    GameState.AnimEditorFrame[animationTimerIndex] = 1; // Reset to the first frame if it exceeds the count
                }
                else
                {
                    GameState.AnimEditorFrame[animationTimerIndex] += 1;
                }
                GameState.AnimEditorTimer[animationTimerIndex] = Environment.TickCount;

                // Render the frame if necessary
                if (frameCountControl.Value > 0m)
                {
                    int frameIndex = GameState.AnimEditorFrame[animationTimerIndex] - 1;
                    int column = frameIndex % columns;
                    int row = frameIndex / columns;

                    // Calculate the source rectangle for the texture or image
                    var sRECT = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);

                    // Clear the background to the specified color
                    spriteBatch.GraphicsDevice.Clear(GameClient.ToMonoGameColor(backgroundColorControl.BackColor));

                    GameClient.Graphics.GraphicsDevice.SetRenderTarget(renderTarget);

                    // Draw MonoGame texture
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, new Rectangle(0, 0, frameWidth, frameHeight), sRECT, Color.White);
                    spriteBatch.End();

                    GameClient.Graphics.GraphicsDevice.SetRenderTarget(null);

                    // Convert the render target to a Texture2D and set it as the PictureBox background
                    using (var stream = new MemoryStream())
                    {
                        renderTarget.SaveAsPng(stream, renderTarget.Width, renderTarget.Height);
                        stream.Position = 0L;
                        backgroundColorControl.Image = System.Drawing.Image.FromStream(stream);
                    }
                }
            }
        }

        private void picSprite0_Paint(object sender, PaintEventArgs e)
        {
            if (!General.Client.IsActive)
            {
                DrawAnimationSprite0();
            }
        }

        private void picSprite1_Paint(object sender, PaintEventArgs e)
        {
            if (!General.Client.IsActive)
            {
                DrawAnimationSprite1();
            }
        }

        public void DrawAnimationSprite0()
        {
            {
                var withBlock = Instance;
                // Ensure spriteBatch is created and disposed properly
                var spriteBatch = new SpriteBatch(GameClient.Graphics.GraphicsDevice);
                var renderTarget = new RenderTarget2D(GameClient.Graphics.GraphicsDevice, withBlock.picSprite0.Width, withBlock.picSprite0.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

                // Call ProcessAnimation for each animation panel
                ProcessAnimation(withBlock.nudSprite0, withBlock.nudFrameCount0, withBlock.nudLoopTime0, 0, renderTarget, withBlock.picSprite0, spriteBatch);
            }
        }

        public void DrawAnimationSprite1()
        {
            {
                var withBlock = Instance;
                // Ensure spriteBatch is created and disposed properly
                var spriteBatch = new SpriteBatch(GameClient.Graphics.GraphicsDevice);
                var renderTarget = new RenderTarget2D(GameClient.Graphics.GraphicsDevice, withBlock.picSprite1.Width, withBlock.picSprite1.Height);

                // Call ProcessAnimation for each animation panel
                ProcessAnimation(withBlock.nudSprite1, withBlock.nudFrameCount1, withBlock.nudLoopTime1, 1, renderTarget, withBlock.picSprite1, spriteBatch);
            }
        }
    }
}