﻿using Core;
using DarkUI.Controls;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using RenderTargetUsage = Microsoft.Xna.Framework.Graphics.RenderTargetUsage;

namespace Client
{
    internal partial class frmEditor_Animation
    {
        int selectedAnim = 0;

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
            selectedAnim = 0;
        }

        private void NudSprite1_ValueChanged(object sender, EventArgs e)
        {
            Core.Type.Animation[GameState.EditorIndex].Sprite[1] = (int)Math.Round(nudSprite1.Value);
            selectedAnim = 1;
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

        public void ProcessAnimation(ref DarkNumericUpDown animationControl, ref DarkNumericUpDown frameCountControl, ref DarkNumericUpDown loopCountControl, int animationTimerIndex, RenderTarget2D renderTarget, ref PictureBox backgroundColorControl, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // Retrieve the animation number and check its validity
            int animationNum = (int)Math.Round(animationControl.Value);
            if (animationNum <= 0 || animationNum > GameState.NumAnimations)
            {
                spriteBatch.GraphicsDevice.Clear(GameClient.ToMonoGameColor(backgroundColorControl.BackColor));
                if (backgroundColorControl.Image != null)
                    backgroundColorControl.Image = null;
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
            }

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

        private void picSprite0_Paint(object sender, PaintEventArgs e)
        {
            var withBlock = Instance;
            if (!General.Client.IsActive && selectedAnim == 0)
            {         
                // Ensure spriteBatch is created and disposed properly  
                using (var spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GameClient.Graphics.GraphicsDevice))
                {
                    using (var renderTarget0 = new RenderTarget2D(GameClient.Graphics.GraphicsDevice, withBlock.picSprite0.Width, withBlock.picSprite0.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents))
                    {
                        ProcessAnimation(ref withBlock.nudSprite0, ref withBlock.nudFrameCount0, ref withBlock.nudLoopTime0, 0, renderTarget0, ref withBlock.picSprite0, spriteBatch);
                    }
                }
            }

            picSpriteAnimations(); // Load the image if it quits animating
        }

        private void picSpriteAnimations()
        {
            var withBlock = Instance;

            // Load the image without DirectX if it quits animating
            if (withBlock.picSprite0.Image == null)
            {
                var animationNum = (int)Math.Round(withBlock.nudSprite0.Value);
                if (animationNum > 0 && animationNum <= GameState.NumAnimations)
                {
                    var imagePath = System.IO.Path.Combine(Core.Path.Animations, animationNum + GameState.GfxExt);
                    if (System.IO.File.Exists(imagePath))
                    {
                        withBlock.picSprite0.Image = System.Drawing.Image.FromFile(imagePath);
                    }
                }
            }

            // Load the image without DirectX if it quits animating
            if (withBlock.picSprite1.Image == null)
            {
                var animationNum = (int)Math.Round(withBlock.nudSprite1.Value);
                if (animationNum > 0 && animationNum <= GameState.NumAnimations)
                {
                    var imagePath = System.IO.Path.Combine(Core.Path.Animations, animationNum + GameState.GfxExt);
                    if (System.IO.File.Exists(imagePath))
                    {
                        withBlock.picSprite1.Image = System.Drawing.Image.FromFile(imagePath);
                    }
                }
            }
        }

        private void picSprite1_Paint(object sender, PaintEventArgs e)
        {
            if (!General.Client.IsActive && selectedAnim == 1)
            {
                var withBlock = Instance;
                // Ensure spriteBatch is created and disposed properly  
                using (var spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GameClient.Graphics.GraphicsDevice))
                { 
                    using (var renderTarget1 = new RenderTarget2D(GameClient.Graphics.GraphicsDevice, withBlock.picSprite1.Width, withBlock.picSprite1.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents))
                    {
                        //withBlock.picSprite0.Image = null; // Reset image before drawing
                        ProcessAnimation(ref withBlock.nudSprite1, ref withBlock.nudFrameCount1, ref withBlock.nudLoopTime1, 1, renderTarget1, ref withBlock.picSprite1, spriteBatch);
                    }
                }
            }

            picSpriteAnimations(); // Load the image if it quits animating
        }
    }
}