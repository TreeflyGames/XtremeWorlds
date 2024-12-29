using System;
using System.IO;
using System.Windows.Forms;
using Core;
using static Core.Enum;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mirage.Sharp.Asfw;

namespace Client
{

    public partial class frmEditor_Map
    {
        public frmEditor_Map()
        {
            InitializeComponent();
        }
        #region Frm
        private void frmEditor_Map_Load(object sender, EventArgs e)
        {
            pnlAttributes.BringToFront();
            pnlAttributes.Visible = false;
            pnlAttributes.Left = 4;
            pnlAttributes.Top = 28;
            optBlocked.Checked = true;
            tabpages.SelectedIndex = 0;

            GameState.DirArrowX[(int)DirectionType.Up] = 12;
            GameState.DirArrowY[(int)DirectionType.Up] = 0;
            GameState.DirArrowX[(int)DirectionType.Down] = 12;
            GameState.DirArrowY[(int)DirectionType.Down] = 23;
            GameState.DirArrowX[(int)DirectionType.Left] = 0;
            GameState.DirArrowY[(int)DirectionType.Left] = 12;
            GameState.DirArrowX[(int)DirectionType.Right] = 23;
            GameState.DirArrowY[(int)DirectionType.Right] = 12;

            scrlFog.Maximum = GameState.NumFogs;
        }

        private void DrawItem()
        {
            int itemnum;

            itemnum = Core.Type.Item[scrlMapItem.Value].Icon;

            if (itemnum <= 0 | itemnum > GameState.NumItems)
            {
                picMapItem.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Items, itemnum + GameState.GfxExt)))
            {
                picMapItem.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Items, itemnum + GameState.GfxExt));
            }

        }

        public void DrawTileset()
        {
            int tilesetIndex;

            // Ensure a tileset is selected
            if (cmbTileSets.SelectedIndex == -1)
            {
                return;
            }

            // Get the selected tileset index
            tilesetIndex = cmbTileSets.SelectedIndex;

            // Get the graphics information for the selected tileset
            string tilesetPath = System.IO.Path.Combine(Core.Path.Tilesets, tilesetIndex.ToString());
            var gfxInfo = GameClient.GetGfxInfo(tilesetPath);

            // Handle varying tileset sizes
            var texture = GameClient.GetTexture(tilesetPath);
            if (texture is null)
            {
                return;
            }

            // Use the dimensions of the PictureBox (picBackSelect)
            int picWidth = Instance.picBackSelect.Width;
            int picHeight = Instance.picBackSelect.Height;

            // Create a render target for drawing
            var renderTarget = new RenderTarget2D(GameClient.Graphics.GraphicsDevice, picWidth, picHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
            GameClient.Graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            GameClient.Graphics.GraphicsDevice.Clear(Color.Black);

            // Create a SpriteBatch for rendering
            var spriteBatch = new SpriteBatch(GameClient.Graphics.GraphicsDevice);

            // Begin the SpriteBatch with appropriate settings
            spriteBatch.Begin();

            // Calculate the source rectangle
            var sourceRect = new Rectangle(0, 0, gfxInfo.Width, gfxInfo.Height);

            // Calculate the destination rectangle, preserving aspect ratio
            float scaleX = (float)(picWidth / (double)gfxInfo.Width);
            float scaleY = (float)(picHeight / (double)gfxInfo.Height);
            float scale = Math.Min(scaleX, scaleY);

            int destWidth = (int)Math.Round(gfxInfo.Width * scale);
            int destHeight = (int)Math.Round(gfxInfo.Height * scale);
            var destRect = new Rectangle(0, 0, destWidth, destHeight);

            // Draw the tileset texture at the top-left
            spriteBatch.Draw(texture, destRect, sourceRect, Color.White);
            DrawSelectionRectangle(spriteBatch, scale);

            // End the SpriteBatch
            spriteBatch.End();

            // Reset the render target to the back buffer
            GameClient.Graphics.GraphicsDevice.SetRenderTarget(null);

            // Convert the render target to a Texture2D and set it as the PictureBox background
            using (var stream = new MemoryStream())
            {
                renderTarget.SaveAsPng(stream, renderTarget.Width, renderTarget.Height);
                stream.Position = 0L;
                picBackSelect.Image = System.Drawing.Image.FromStream(stream);
            }

            // Dispose of the render target and sprite batch
            renderTarget.Dispose();
            spriteBatch.Dispose();
        }

        public void DrawSelectionRectangle(SpriteBatch spriteBatch, float scale)
        {
            // Scale the selection rectangle based on the scale factor
            int scaledX = (int)Math.Round(GameState.EditorTileSelStart.X * GameState.PicX * scale);
            int scaledY = (int)Math.Round(GameState.EditorTileSelStart.Y * GameState.PicY * scale);
            int scaledWidth = (int)Math.Round(GameState.EditorTileWidth * GameState.PicX * scale);
            int scaledHeight = (int)Math.Round(GameState.EditorTileHeight * GameState.PicY * scale);

            // Define the scaled selection rectangle
            var selectionRect = new Rectangle(scaledX, scaledY, scaledWidth, scaledHeight);

            // Line thickness in pixels (adjust based on scaling if needed)
            int lineThickness = (int)Math.Round(1f * scale);

            // Top border
            spriteBatch.Draw(GameClient.PixelTexture, new Rectangle(selectionRect.X, selectionRect.Y, selectionRect.Width, lineThickness), Color.Red);

            // Bottom border
            spriteBatch.Draw(GameClient.PixelTexture, new Rectangle(selectionRect.X, selectionRect.Y + selectionRect.Height - lineThickness, selectionRect.Width, lineThickness), Color.Red);

            // Left border
            spriteBatch.Draw(GameClient.PixelTexture, new Rectangle(selectionRect.X, selectionRect.Y, lineThickness, selectionRect.Height), Color.Red);

            // Right border
            spriteBatch.Draw(GameClient.PixelTexture, new Rectangle(selectionRect.X + selectionRect.Width - lineThickness, selectionRect.Y, lineThickness, selectionRect.Height), Color.Red);
        }

        private void DrawRectangleOutline(SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            // Draw lines to form a rectangle outline
            int lineThickness = 1; // Change as needed
            spriteBatch.Draw(GameClient.PixelTexture, new Rectangle(rect.X, rect.Y, rect.Width, lineThickness), color); // Top
            spriteBatch.Draw(GameClient.PixelTexture, new Rectangle(rect.X, rect.Y, lineThickness, rect.Height), color); // Left
            spriteBatch.Draw(GameClient.PixelTexture, new Rectangle(rect.X + rect.Width - lineThickness, rect.Y, lineThickness, rect.Height), color); // Right
            spriteBatch.Draw(GameClient.PixelTexture, new Rectangle(rect.X, rect.Y + rect.Height - lineThickness, rect.Width, lineThickness), color); // Bottom
        }

        #endregion

        #region Toolbar

        private void TsbSave_Click(object sender, EventArgs e)
        {
            int X;
            int x2;
            int Y;
            int y2;
            Core.Type.TileStruct[,] tempArr;

            if (!Information.IsNumeric(txtMaxX.Text))
                txtMaxX.Text = Core.Type.MyMap.MaxX.ToString();
            if (Conversion.Val(txtMaxX.Text) < Settings.CameraWidth)
                txtMaxX.Text = Settings.CameraWidth.ToString();
            if (Conversion.Val(txtMaxX.Text) > byte.MaxValue)
                txtMaxX.Text = byte.MaxValue.ToString();
            if (!Information.IsNumeric(txtMaxY.Text))
                txtMaxY.Text = Core.Type.MyMap.MaxY.ToString();
            if (Conversion.Val(txtMaxY.Text) < Settings.CameraHeight)
                txtMaxY.Text = Settings.CameraHeight.ToString();
            if (Conversion.Val(txtMaxY.Text) > byte.MaxValue)
                txtMaxY.Text = byte.MaxValue.ToString();

            {
                ref var withBlock = ref Core.Type.MyMap;
                withBlock.Name = Strings.Trim(txtName.Text);
                if (lstMusic.SelectedIndex >= 0)
                {
                    withBlock.Music = lstMusic.Items[lstMusic.SelectedIndex].ToString();
                }
                else
                {
                    withBlock.Music = "";
                }

                if (lstShop.SelectedIndex >= 0)
                {
                    withBlock.Shop = lstShop.SelectedIndex;
                }
                else
                {
                    withBlock.Shop = 0;
                }

                withBlock.Up = (int)Math.Round(Conversion.Val(txtUp.Text));
                withBlock.Down = (int)Math.Round(Conversion.Val(txtDown.Text));
                withBlock.Left = (int)Math.Round(Conversion.Val(txtLeft.Text));
                withBlock.Right = (int)Math.Round(Conversion.Val(txtRight.Text));
                withBlock.Moral = (byte)lstMoral.SelectedIndex;
                withBlock.BootMap = (int)Math.Round(Conversion.Val(txtBootMap.Text));
                withBlock.BootX = (byte)Math.Round(Conversion.Val(txtBootX.Text));
                withBlock.BootY = (byte)Math.Round(Conversion.Val(txtBootY.Text));

                // set the data before changing it
                tempArr = (Core.Type.TileStruct[,])withBlock.Tile.Clone();

                x2 = withBlock.MaxX;
                y2 = withBlock.MaxY;

                // change the data
                withBlock.MaxX = (byte)Math.Round(Conversion.Val(txtMaxX.Text));
                withBlock.MaxY = (byte)Math.Round(Conversion.Val(txtMaxY.Text));

                withBlock.Tile = new Core.Type.TileStruct[(withBlock.MaxX + 1), (withBlock.MaxY + 1)];
                Core.Type.Autotile = new Core.Type.AutotileStruct[(withBlock.MaxX + 1), (withBlock.MaxY + 1)];

                for (int i = 0; i <= GameState.MaxTileHistory; i++)
                    Core.Type.TileHistory[i].Tile = new Core.Type.TileStruct[(withBlock.MaxX + 1), (withBlock.MaxY + 1)];

                if (x2 > withBlock.MaxX)
                    x2 = withBlock.MaxX;
                if (y2 > withBlock.MaxY)
                    y2 = withBlock.MaxY;

                var loopTo = (int)withBlock.MaxX;
                for (X = 0; X <= loopTo; X++)
                {
                    var loopTo1 = (int)withBlock.MaxY;
                    for (Y = 0; Y <= loopTo1; Y++)
                    {
                        withBlock.Tile[X, Y].Layer = new Core.Type.TileDataStruct[10];
                        Core.Type.Autotile[X, Y].Layer = new Core.Type.QuarterTileStruct[10];

                        for (int i = 0; i <= GameState.MaxTileHistory; i++)
                            Core.Type.TileHistory[i].Tile[X, Y].Layer = new Core.Type.TileDataStruct[10];

                        if (X <= x2)
                        {
                            if (Y <= y2)
                            {
                                withBlock.Tile[X, Y] = tempArr[X, Y];
                            }
                        }
                    }
                }
            }

            MapEditorSend();
            GameState.GettingMap = true;
            Dispose();
        }

        private void TsbFill_Click(object sender, EventArgs e)
        {
            LayerType layer = (LayerType)(cmbLayers.SelectedIndex);
            MapEditorFillLayer(layer, (byte)(cmbAutoTile.SelectedIndex), (byte)GameState.EditorTileX, (byte)GameState.EditorTileY);
        }

        private void TsbClear_Click(object sender, EventArgs e)
        {
            LayerType layer = (LayerType)(cmbLayers.SelectedIndex);
            MapEditorClearLayer(layer);
        }

        private void TsbEyeDropper_Click(object sender, EventArgs e)
        {
            GameState.EyeDropper = !GameState.EyeDropper;
        }

        private void TsbDiscard_Click(object sender, EventArgs e)
        {
            MapEditorCancel();
            Dispose();
        }

        private void TsbMapGrid_Click(object sender, EventArgs e)
        {
            GameState.MapGrid = !GameState.MapGrid;
        }

        #endregion

        #region Tiles
        private void PicBackSelect_MouseDown(object sender, MouseEventArgs e)
        {
            MapEditorChooseTile((int)e.Button, e.X, e.Y);
        }

        private void PicBackSelect_MouseMove(object sender, MouseEventArgs e)
        {
            MapEditorDrag((int)e.Button, e.X, e.Y);
        }

        private void CmbTileSets_Click(object sender, EventArgs e)
        {
            if (cmbTileSets.SelectedIndex > GameState.NumTileSets)
            {
                cmbTileSets.SelectedIndex = 0;
            }

            Core.Type.MyMap.Tileset = cmbTileSets.SelectedIndex;

            GameState.EditorTileSelStart = new Point(0, 0);
            GameState.EditorTileSelEnd = new Point(1, 1);
        }

        private void CmbAutoTile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAutoTile.SelectedIndex == 0)
            {
                GameState.EditorTileWidth = 1;
                GameState.EditorTileHeight = 1;
            }
        }

        #endregion

        #region Attributes

        private void ScrlMapWarpMap_Scroll(object sender, EventArgs e)
        {
            lblMapWarpMap.Text = "Map: " + scrlMapWarpMap.Value;
        }

        private void ScrlMapWarpX_Scroll(object sender, EventArgs e)
        {
            lblMapWarpX.Text = "X: " + scrlMapWarpX.Value;
        }

        private void ScrlMapWarpY_Scroll(object sender, EventArgs e)
        {
            lblMapWarpY.Text = "Y: " + scrlMapWarpY.Value;
        }

        private void BtnMapWarp_Click(object sender, EventArgs e)
        {
            GameState.EditorWarpMap = scrlMapWarpMap.Value;

            GameState.EditorWarpX = scrlMapWarpX.Value;
            GameState.EditorWarpY = scrlMapWarpY.Value;
            pnlAttributes.Visible = false;
            fraMapWarp.Visible = false;
        }

        private void OptWarp_CheckedChanged(object sender, EventArgs e)
        {
            if (optWarp.Checked == false)
                return;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraMapWarp.Visible = true;

            scrlMapWarpMap.Maximum = Constant.MAX_MAPS - 1;
            scrlMapWarpMap.Value = 1;
            scrlMapWarpX.Maximum = byte.MaxValue;
            scrlMapWarpY.Maximum = byte.MaxValue;
            scrlMapWarpX.Value = 0;
            scrlMapWarpY.Value = 0;
        }

        private void ScrlMapItem_ValueChanged(object sender, EventArgs e)
        {
            if (Core.Type.Item[scrlMapItem.Value].Type == (byte)ItemType.Currency | Core.Type.Item[scrlMapItem.Value].Stackable == 1)
            {
                scrlMapItemValue.Enabled = true;
            }
            else
            {
                scrlMapItemValue.Value = 1;
                scrlMapItemValue.Enabled = false;
            }

            DrawItem();
            lblMapItem.Text = scrlMapItem.Value + ". " + Core.Type.Item[scrlMapItem.Value].Name + " x" + scrlMapItemValue.Value;
        }

        private void ScrlMapItemValue_ValueChanged(object sender, EventArgs e)
        {
            lblMapItem.Text = Core.Type.Item[scrlMapItem.Value].Name + " x" + scrlMapItemValue.Value;
        }

        private void BtnMapItem_Click(object sender, EventArgs e)
        {
            GameState.ItemEditorNum = scrlMapItem.Value;
            GameState.ItemEditorValue = scrlMapItemValue.Value;
            pnlAttributes.Visible = false;
            fraMapItem.Visible = false;
        }

        private void OptItem_CheckedChanged(object sender, EventArgs e)
        {
            if (optItem.Checked == false)
                return;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraMapItem.Visible = true;

            scrlMapItem.Maximum = Constant.MAX_ITEMS - 1;
            scrlMapItem.Value = 1;
            lblMapItem.Text = Core.Type.Item[scrlMapItem.Value].Name + " x" + scrlMapItemValue.Value;
            DrawItem();
        }

        private void BtnResourceOk_Click(object sender, EventArgs e)
        {
            GameState.ResourceEditorNum = scrlResource.Value;
            pnlAttributes.Visible = false;
            fraResource.Visible = false;
        }

        private void ScrlResource_ValueChanged(object sender, EventArgs e)
        {
            lblResource.Text = "Resource: " + Core.Type.Resource[scrlResource.Value].Name;
        }

        private void OptResource_CheckedChanged(object sender, EventArgs e)
        {
            if (optResource.Checked == false)
                return;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraResource.Visible = true;
        }

        private void BtnNPCSpawn_Click(object sender, EventArgs e)
        {
            GameState.SpawnNPCNum = lstNPC.SelectedIndex;
            GameState.SpawnNPCDir = scrlNPCDir.Value;
            pnlAttributes.Visible = false;
            fraNPCSpawn.Visible = false;
        }

        private void OptNPCSpawn_CheckedChanged(object sender, EventArgs e)
        {
            int n;

            if (optNPCSpawn.Checked == false)
                return;

            lstNPC.Items.Clear();

            for (n = 1; n <= Constant.MAX_MAP_NPCS - 1; n++)
            {
                if (Core.Type.MyMap.NPC[n] > 0)
                {
                    lstNPC.Items.Add(n + ": " + Core.Type.NPC[Core.Type.MyMap.NPC[n]].Name);
                }
                else
                {
                    lstNPC.Items.Add(n);
                }
            }

            scrlNPCDir.Value = 0;
            lstNPC.SelectedIndex = 0;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraNPCSpawn.Visible = true;
        }

        private void BtnShop_Click(object sender, EventArgs e)
        {
            GameState.EditorShop = cmbShop.SelectedIndex;
            pnlAttributes.Visible = false;
            fraShop.Visible = false;
        }

        private void OptShop_CheckedChanged(object sender, EventArgs e)
        {
            if (optShop.Checked == false)
                return;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraShop.Visible = true;
        }

        private void BtnHeal_Click(object sender, EventArgs e)
        {
            GameState.MapEditorHealType = cmbHeal.SelectedIndex;
            GameState.MapEditorHealAmount = scrlHeal.Value;
            pnlAttributes.Visible = false;
            fraHeal.Visible = false;
        }

        private void ScrlHeal_Scroll(object sender, EventArgs e)
        {
            lblHeal.Text = "Amount: " + scrlHeal.Value;
        }

        private void OptHeal_CheckedChanged(object sender, EventArgs e)
        {
            if (optHeal.Checked == false)
                return;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraHeal.Visible = true;
        }

        private void ScrlTrap_ValueChanged(object sender, EventArgs e)
        {
            lblTrap.Text = "Amount: " + scrlTrap.Value;
        }

        private void BtnTrap_Click(object sender, EventArgs e)
        {
            GameState.MapEditorHealAmount = scrlTrap.Value;
            pnlAttributes.Visible = false;
            fraTrap.Visible = false;
        }

        private void OptTrap_CheckedChanged(object sender, EventArgs e)
        {
            if (optTrap.Checked == false)
                return;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraTrap.Visible = true;
        }

        private void BtnClearAttribute_Click(object sender, EventArgs e)
        {
            GameLogic.Dialogue("Map Editor", "Clear Attributes: ", "Are you sure you wish to clear attributes?", (byte)DialogueType.ClearAttributes, (byte)DialogueStyle.YesNo);
        }

        private void ScrlNPCDir_Scroll(object sender, EventArgs e)
        {
            switch (scrlNPCDir.Value)
            {
                case 0:
                    {
                        lblNPCDir.Text = "Direction: Up";
                        break;
                    }
                case 1:
                    {
                        lblNPCDir.Text = "Direction: Down";
                        break;
                    }
                case 2:
                    {
                        lblNPCDir.Text = "Direction: Left";
                        break;
                    }
                case 3:
                    {
                        lblNPCDir.Text = "Direction: Right";
                        break;
                    }
            }
        }

        private void OptBlocked_CheckedChanged(object sender, EventArgs e)
        {
            if (optBlocked.Checked)
                pnlAttributes.Visible = false;
        }
        #endregion

        #region NPC's

        private void CmbNPCList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMapNPC.SelectedIndex > 0)
            {
                lstMapNPC.Items[lstMapNPC.SelectedIndex] = lstMapNPC.SelectedIndex + ": " + Core.Type.NPC[cmbNPCList.SelectedIndex].Name;
                Core.Type.MyMap.NPC[lstMapNPC.SelectedIndex] = cmbNPCList.SelectedIndex;
            }
        }

        #endregion

        #region Settings

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            if (lstMusic.SelectedIndex > 0)
            {
                string selectedFile = lstMusic.Items[lstMusic.SelectedIndex].ToString();

                // If the selected music file is a MIDi file
                if (Settings.MusicExt == ".mid")
                {
                    Sound.PlayMidi(Core.Path.Music + selectedFile);
                }
                else
                {
                    Sound.PlayMusic(selectedFile);
                }
            }
        }

        #endregion

        #region Events

        private void BtnCopyEvent_Click(object sender, EventArgs e)
        {
            if (Event.EventCopy == false)
            {
                Event.EventCopy = true;
                lblCopyMode.Text = "CopyMode On";
                Event.EventPaste = false;
                lblPasteMode.Text = "PasteMode Off";
            }
            else
            {
                Event.EventCopy = false;
                lblCopyMode.Text = "CopyMode Off";
            }
        }

        private void BtnPasteEvent_Click(object sender, EventArgs e)
        {
            if (Event.EventPaste == false)
            {
                Event.EventPaste = true;
                lblPasteMode.Text = "PasteMode On";
                Event.EventCopy = false;
                lblCopyMode.Text = "CopyMode Off";
            }
            else
            {
                Event.EventPaste = false;
                lblPasteMode.Text = "PasteMode Off";
            }
        }

        #endregion

        #region Map Effects

        private void CmbWeather_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.MyMap.Weather = (byte)cmbWeather.SelectedIndex;
        }

        private void ScrlFog_Scroll(object sender, EventArgs e)
        {
            Core.Type.MyMap.Fog = scrlFog.Value;
            lblFogIndex.Text = "Fog: " + scrlFog.Value;
        }

        private void ScrlIntensity_Scroll(object sender, EventArgs e)
        {
            Core.Type.MyMap.WeatherIntensity = scrlIntensity.Value;
            lblIntensity.Text = "Intensity: " + scrlIntensity.Value;
        }

        private void ScrlFogSpeed_Scroll(object sender, EventArgs e)
        {
            Core.Type.MyMap.FogSpeed = (byte)scrlFogSpeed.Value;
            lblFogSpeed.Text = "FogSpeed: " + scrlFogSpeed.Value;
        }

        private void ScrlFogOpacity_Scroll(object sender, EventArgs e)
        {
            Core.Type.MyMap.FogOpacity = (byte)scrlFogOpacity.Value;
            lblFogOpacity.Text = "Fog Alpha: " + scrlFogOpacity.Value;
        }

        private void ChkUseTint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTint.Checked == true)
            {
                Core.Type.MyMap.MapTint = Conversions.ToBoolean(1);
            }
            else
            {
                Core.Type.MyMap.MapTint = Conversions.ToBoolean(0);
            }
        }

        private void ScrlMapRed_Scroll(object sender, EventArgs e)
        {
            Core.Type.MyMap.MapTintR = (byte)scrlMapRed.Value;
            lblMapRed.Text = "Red: " + scrlMapRed.Value;
        }

        private void ScrlMapGreen_Scroll(object sender, EventArgs e)
        {
            Core.Type.MyMap.MapTintG = (byte)scrlMapGreen.Value;
            lblMapGreen.Text = "Green: " + scrlMapGreen.Value;
        }

        private void ScrlMapBlue_Scroll(object sender, EventArgs e)
        {
            Core.Type.MyMap.MapTintB = (byte)scrlMapBlue.Value;
            lblMapBlue.Text = "Blue: " + scrlMapBlue.Value;
        }

        private void ScrlMapAlpha_Scroll(object sender, EventArgs e)
        {
            Core.Type.MyMap.MapTintA = (byte)scrlMapAlpha.Value;
            lblMapAlpha.Text = "Alpha: " + scrlMapAlpha.Value;
        }

        private void CmbPanorama_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.MyMap.Panorama = (byte)cmbPanorama.SelectedIndex;
        }

        private void CmbParallax_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Type.MyMap.Parallax = (byte)cmbParallax.SelectedIndex;
        }

        #endregion

        #region Map Editor

        public void MapPropertiesInit()
        {
            int X;
            int Y;
            int i;

            txtName.Text = Strings.Trim(Core.Type.MyMap.Name);

            // find the music we have set
            lstMusic.Items.Clear();
            lstMusic.Items.Add("None");
            lstMusic.SelectedIndex = 0;

            General.CacheMusic();

            var loopTo = Information.UBound(Sound.MusicCache);
            for (i = 0; i <= loopTo; i++)
                lstMusic.Items.Add(Sound.MusicCache[i]);

            var loopTo1 = lstMusic.Items.Count - 1;
            for (i = 0; i <= loopTo1; i++)
            {
                if ((lstMusic.Items[i].ToString() ?? "") == (Core.Type.MyMap.Music ?? ""))
                {
                    lstMusic.SelectedIndex = i;
                    break;
                }
            }

            // find the shop we have set
            lstShop.Items.Clear();
            lstShop.Items.Add("None");
            lstShop.SelectedIndex = 0;

            for (i = 0; i <= Constant.MAX_SHOPS - 1; i++)
                lstShop.Items.Add(Core.Type.Shop[i].Name);

            var loopTo2 = lstShop.Items.Count - 1;
            for (i = 0; i <= loopTo2; i++)
            {
                if ((lstShop.Items[i].ToString() ?? "") == (Core.Type.Shop[Core.Type.MyMap.Shop].Name ?? ""))
                {
                    lstShop.SelectedIndex = i;
                    break;
                }
            }

            // find the shop we have set
            lstMoral.Items.Clear();
            lstMoral.Items.Add("None");
            lstMoral.SelectedIndex = 0;

            for (i = 0; i <= Constant.MAX_MORALS; i++)
                lstMoral.Items.Add(Core.Type.Moral[i].Name);

            var loopTo3 = lstMoral.Items.Count - 1;
            for (i = 0; i <= loopTo3; i++)
            {
                if ((lstMoral.Items[i].ToString() ?? "") == (Core.Type.Moral[Core.Type.MyMap.Moral].Name ?? ""))
                {
                    lstMoral.SelectedIndex = i;
                    break;
                }
            }

            chkTint.Checked = Core.Type.MyMap.MapTint;
            chkNoMapRespawn.Checked = Core.Type.MyMap.NoRespawn;
            chkIndoors.Checked = Core.Type.MyMap.Indoors;

            // rest of it
            txtUp.Text = Core.Type.MyMap.Up.ToString();
            txtDown.Text = Core.Type.MyMap.Down.ToString();
            txtLeft.Text = Core.Type.MyMap.Left.ToString();
            txtRight.Text = Core.Type.MyMap.Right.ToString();

            txtBootMap.Text = Core.Type.MyMap.BootMap.ToString();
            txtBootX.Text = Core.Type.MyMap.BootX.ToString();
            txtBootY.Text = Core.Type.MyMap.BootY.ToString();

            lstMapNPC.Items.Clear();
            lstMapNPC.Items.Add("None");
            lstMapNPC.SelectedIndex = 0;

            for (X = 1; X <= Constant.MAX_MAP_NPCS - 1; X++)
                lstMapNPC.Items.Add(X + ": " + Strings.Trim(Core.Type.NPC[Core.Type.MyMap.NPC[X]].Name));

            cmbNPCList.Items.Add("None");
            cmbNPCList.SelectedIndex = 0;

            for (Y = 1; Y <= Constant.MAX_NPCS - 1; Y++)
                cmbNPCList.Items.Add(Y + ": " + Strings.Trim(Core.Type.NPC[Y].Name));

            cmbAnimation.Items.Clear();
            cmbAnimation.Items.Add("None");
            cmbAnimation.SelectedIndex = 0;

            for (Y = 1; Y <= Constant.MAX_ANIMATIONS - 1; Y++)
                cmbAnimation.Items.Add(Y + ": " + Core.Type.Animation[Y].Name);

            lblMap.Text = "Map: ";
            txtMaxX.Text = Core.Type.MyMap.MaxX.ToString();
            txtMaxY.Text = Core.Type.MyMap.MaxY.ToString();

            cmbWeather.SelectedIndex = Core.Type.MyMap.Weather;
            scrlFog.Value = Core.Type.MyMap.Fog;
            lblFogIndex.Text = "Fog: " + scrlFog.Value;
            scrlIntensity.Value = Core.Type.MyMap.WeatherIntensity;
            lblIntensity.Text = "Intensity: " + scrlIntensity.Value;
            scrlFogOpacity.Value = Core.Type.MyMap.FogOpacity;
            scrlFogSpeed.Value = Core.Type.MyMap.FogSpeed;

            cmbPanorama.Items.Clear();
            cmbPanorama.Items.Add("None");

            var loopTo4 = GameState.NumPanoramas;
            for (i = 0; i <= loopTo4; i++)
                cmbPanorama.Items.Add(i);

            cmbPanorama.SelectedIndex = Core.Type.MyMap.Panorama;

            cmbParallax.Items.Clear();
            cmbParallax.Items.Add("None");

            var loopTo5 = GameState.NumParallax;
            for (i = 0; i <= loopTo5; i++)
                cmbParallax.Items.Add(i);

            cmbParallax.SelectedIndex = Core.Type.MyMap.Parallax;

            tabpages.SelectedIndex = 0;
            scrlMapBrightness.Value = Core.Type.MyMap.Brightness;
            chkTint.Checked = Core.Type.MyMap.MapTint;
            scrlMapRed.Value = Core.Type.MyMap.MapTintR;
            scrlMapGreen.Value = Core.Type.MyMap.MapTintG;
            scrlMapBlue.Value = Core.Type.MyMap.MapTintB;
            scrlMapAlpha.Value = Core.Type.MyMap.MapTintA;

            // show the form
            Visible = true;
        }

        public void MapEditorInit()
        {
            // set the scrolly bars
            if (Core.Type.MyMap.Tileset == 0)
                Core.Type.MyMap.Tileset = 1;
            if (Core.Type.MyMap.Tileset > GameState.NumTileSets)
                Core.Type.MyMap.Tileset = 1;

            GameState.EditorTileSelStart = new Point(0, 0);
            GameState.EditorTileSelEnd = new Point(1, 1);

            // set shops for the shop attribute
            for (int i = 0; i <= Constant.MAX_SHOPS - 1; i++)
                cmbShop.Items.Add(i + ": " + Core.Type.Shop[i].Name);

            // we're not in a shop
            cmbShop.SelectedIndex = 0;
            cmbAttribute.SelectedIndex = 0;

            optBlocked.Checked = true;

            cmbTileSets.Items.Clear();
            for (int i = 0, loopTo = GameState.NumTileSets; i <= loopTo; i++)
                cmbTileSets.Items.Add(i);

            cmbTileSets.SelectedIndex = 0;
            cmbLayers.SelectedIndex = 0;
            cmbAutoTile.SelectedIndex = 0;

            MapPropertiesInit();

            if (GameState.MapData == true)
                GameState.GettingMap = false;
        }

        public static void MapEditorChooseTile(int Button, float X, float Y)
        {
            if (Button == (int)MouseButtons.Left) // Left Mouse Button
            {
                GameState.EditorTileWidth = 1;
                GameState.EditorTileHeight = 1;

                if (Instance.cmbAutoTile.SelectedIndex > 0)
                {
                    switch (Instance.cmbAutoTile.SelectedIndex)
                    {
                        case 1: // autotile
                            {
                                GameState.EditorTileWidth = 2;
                                GameState.EditorTileHeight = 3;
                                break;
                            }
                        case 2: // fake autotile
                            {
                                GameState.EditorTileWidth = 1;
                                GameState.EditorTileHeight = 1;
                                break;
                            }
                        case 3: // animated
                            {
                                GameState.EditorTileWidth = 6;
                                GameState.EditorTileHeight = 3;
                                break;
                            }
                        case 4: // cliff
                            {
                                GameState.EditorTileWidth = 2;
                                GameState.EditorTileHeight = 2;
                                break;
                            }
                        case 5: // waterfall
                            {
                                GameState.EditorTileWidth = 2;
                                GameState.EditorTileHeight = 3;
                                break;
                            }
                    }
                }

                GameState.EditorTileX = (int)((long)Math.Round(X) / GameState.PicX);
                GameState.EditorTileY = (int)((long)Math.Round(Y) / GameState.PicY);

                GameState.EditorTileSelStart = new Point(GameState.EditorTileX, GameState.EditorTileY);
                GameState.EditorTileSelEnd = new Point(GameState.EditorTileX + GameState.EditorTileWidth, GameState.EditorTileY + GameState.EditorTileHeight);
            }
        }

        public void MapEditorDrag(int Button, float X, float Y)
        {
            if (Button == (int)MouseButtons.Left) // Left Mouse Button
            {
                // convert the pixel number to tile number
                X = (long)Math.Round(X) / GameState.PicX + 1L;
                Y = (long)Math.Round(Y) / GameState.PicY + 1L;

                // check it's not out of bounds
                if (X < 0f)
                    X = 0f;
                if ((double)X > picBackSelect.Width / (double)GameState.PicX)
                    X = (float)(picBackSelect.Width / (double)GameState.PicX);
                if (Y < 0f)
                    Y = 0f;
                if ((double)Y > picBackSelect.Height / (double)GameState.PicY)
                    Y = (float)(picBackSelect.Height / (double)GameState.PicY);

                // find out what to set the width + height of map editor to
                if (X > GameState.EditorTileX) // drag right
                {
                    GameState.EditorTileWidth = (int)Math.Round(X - GameState.EditorTileX);
                }

                if (Y > GameState.EditorTileY) // drag down
                {
                    GameState.EditorTileHeight = (int)Math.Round(Y - GameState.EditorTileY);
                }

                GameState.EditorTileSelStart = new Point(GameState.EditorTileX, GameState.EditorTileY);
                GameState.EditorTileSelEnd = new Point(GameState.EditorTileWidth, GameState.EditorTileHeight);
            }

        }

        public void MapEditorMouseDown(int X, int Y, bool movedMouse = true)
        {
            int i;
            int CurLayer;
            var tileChanged = default(bool);

            CurLayer = Instance.cmbLayers.SelectedIndex;

            if (GameState.EyeDropper)
            {
                MapEditorEyeDropper();
                return;
            }

            for (int x2 = 0, loopTo = Core.Type.MyMap.MaxX; x2 <= loopTo; x2++)
            {
                for (int y2 = 0, loopTo1 = Core.Type.MyMap.MaxY; y2 <= loopTo1; y2++)
                {
                    {
                        ref var withBlock = ref Core.Type.MyMap.Tile[x2, y2];
                        if (withBlock.Layer[CurLayer].Tileset > 0)
                        {
                            if (!tileChanged)
                            {
                                MapEditorHistory();
                                tileChanged = true;
                            }

                            Core.Type.TileHistory[GameState.HistoryIndex].Tile[x2, y2].Data1 = withBlock.Data1;
                            Core.Type.TileHistory[GameState.HistoryIndex].Tile[x2, y2].Data2 = withBlock.Data2;
                            Core.Type.TileHistory[GameState.HistoryIndex].Tile[x2, y2].Data3 = withBlock.Data3;
                            Core.Type.TileHistory[GameState.HistoryIndex].Tile[x2, y2].Type = withBlock.Type;
                            Core.Type.TileHistory[GameState.HistoryIndex].Tile[x2, y2].DirBlock = withBlock.DirBlock;

                            Core.Type.TileHistory[GameState.HistoryIndex].Tile[x2, y2].Layer[CurLayer].X = withBlock.Layer[CurLayer].X;
                            Core.Type.TileHistory[GameState.HistoryIndex].Tile[x2, y2].Layer[CurLayer].Y = withBlock.Layer[CurLayer].Y;
                            Core.Type.TileHistory[GameState.HistoryIndex].Tile[x2, y2].Layer[CurLayer].Tileset = withBlock.Layer[CurLayer].Tileset;
                            Core.Type.TileHistory[GameState.HistoryIndex].Tile[x2, y2].Layer[CurLayer].AutoTile = withBlock.Layer[CurLayer].AutoTile;
                        }
                    }
                }
            }

            if (GameLogic.IsInBounds())
                return;
            if (Instance.cmbAutoTile.SelectedIndex == -1)
                return;

            if (GameClient.IsMouseButtonDown(MouseButton.Left))
            {
                if (Instance.optInfo.Checked)
                {
                    switch (Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Type)
                    {
                        case TileType.Warp:
                            {
                                Client.Text.AddText("Map: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data1.ToString() + " X: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data2.ToString() + " Y:" + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data3.ToString(), (int)ColorType.Gray);
                                break;
                            }
                    }

                    switch (Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Type2)
                    {
                        case TileType.Warp:
                            {
                                Client.Text.AddText("Map: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data1_2.ToString() + " X: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data2_2.ToString() + " Y:" + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data3_2.ToString(), (int)ColorType.Gray);
                                break;
                            }
                    }
                }

                if (ReferenceEquals(Instance.tabpages.SelectedTab, Instance.tpTiles))
                {
                    if (GameState.EditorTileWidth == 1 & GameState.EditorTileHeight == 1) // single tile
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, CurLayer, false, (byte)Instance.cmbAutoTile.SelectedIndex);
                    }
                    else if (Instance.cmbAutoTile.SelectedIndex == 0) // multi tile!
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, CurLayer, true);
                    }
                    else
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, CurLayer, true, (byte)Instance.cmbAutoTile.SelectedIndex);
                    }
                }
                else if (ReferenceEquals(Instance.tabpages.SelectedTab, Instance.tpAttributes))
                {
                    {
                        ref var withBlock1 = ref Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY];
                        // blocked tile
                        if (Instance.optBlocked.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Blocked;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Blocked;
                            }
                        }

                        // warp tile
                        if (Instance.optWarp.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Warp;
                                withBlock1.Data1 = GameState.EditorWarpMap;
                                withBlock1.Data2 = GameState.EditorWarpX;
                                withBlock1.Data3 = GameState.EditorWarpY;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Warp;
                                withBlock1.Data1_2 = GameState.EditorWarpMap;
                                withBlock1.Data2_2 = GameState.EditorWarpX;
                                withBlock1.Data3_2 = GameState.EditorWarpY;
                            }
                        }

                        // item spawn
                        if (Instance.optItem.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Item;
                                withBlock1.Data1 = GameState.ItemEditorNum;
                                withBlock1.Data2 = GameState.ItemEditorValue;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Item;
                                withBlock1.Data1_2 = GameState.ItemEditorNum;
                                withBlock1.Data2_2 = GameState.ItemEditorValue;
                                withBlock1.Data3_2 = 0;
                            }
                        }

                        // NPC avoid
                        if (Instance.optNPCAvoid.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.NPCAvoid;
                                withBlock1.Data1 = 0;
                                withBlock1.Data2 = 0;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.NPCAvoid;
                                withBlock1.Data1_2 = 0;
                                withBlock1.Data2_2 = 0;
                                withBlock1.Data3_2 = 0;
                            }
                        }

                        // resource
                        if (Instance.optResource.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Resource;
                                withBlock1.Data1 = GameState.ResourceEditorNum;
                                withBlock1.Data2 = 0;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Resource;
                                withBlock1.Data1_2 = GameState.ResourceEditorNum;
                                withBlock1.Data2_2 = 0;
                                withBlock1.Data3_2 = 0;
                            }
                        }

                        // NPC spawn
                        if (Instance.optNPCSpawn.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.NPCSpawn;
                                withBlock1.Data1 = GameState.SpawnNPCNum;
                                withBlock1.Data2 = GameState.SpawnNPCDir;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.NPCSpawn;
                                withBlock1.Data1_2 = GameState.SpawnNPCNum;
                                withBlock1.Data2_2 = GameState.SpawnNPCDir;
                                withBlock1.Data3_2 = 0;
                            }
                        }

                        // shop
                        if (Instance.optShop.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Shop;
                                withBlock1.Data1 = GameState.EditorShop;
                                withBlock1.Data2 = 0;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Shop;
                                withBlock1.Data1_2 = GameState.EditorShop;
                                withBlock1.Data2_2 = 0;
                                withBlock1.Data3_2 = 0;
                            }
                        }

                        // bank
                        if (Instance.optBank.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Bank;
                                withBlock1.Data1 = 0;
                                withBlock1.Data2 = 0;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Bank;
                                withBlock1.Data1_2 = 0;
                                withBlock1.Data2_2 = 0;
                                withBlock1.Data3_2 = 0;
                            }
                        }

                        // heal
                        if (Instance.optHeal.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Heal;
                                withBlock1.Data1 = GameState.MapEditorHealType;
                                withBlock1.Data2 = GameState.MapEditorHealAmount;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Heal;
                                withBlock1.Data1_2 = GameState.MapEditorHealType;
                                withBlock1.Data2_2 = GameState.MapEditorHealAmount;
                                withBlock1.Data3_2 = 0;
                            }
                        }

                        // trap
                        if (Instance.optTrap.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Trap;
                                withBlock1.Data1 = GameState.MapEditorHealAmount;
                                withBlock1.Data2 = 0;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Trap;
                                withBlock1.Data1_2 = GameState.MapEditorHealAmount;
                                withBlock1.Data2_2 = 0;
                                withBlock1.Data3_2 = 0;
                            }
                        }

                        // light
                        if (Instance.optLight.Checked)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Light;
                                withBlock1.Data1 = GameState.EditorLight;
                                withBlock1.Data2 = GameState.EditorFlicker;
                                withBlock1.Data3 = GameState.EditorShadow;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Light;
                                withBlock1.Data1_2 = GameState.EditorLight;
                                withBlock1.Data2_2 = GameState.EditorFlicker;
                                withBlock1.Data3_2 = GameState.EditorShadow;
                            }
                        }

                        // Animation
                        if (Instance.optAnimation.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.Animation;
                                withBlock1.Data1 = GameState.EditorAnimation;
                                withBlock1.Data2 = 0;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.Animation;
                                withBlock1.Data1_2 = GameState.EditorAnimation;
                                withBlock1.Data2_2 = 0;
                                withBlock1.Data3_2 = 0;
                            }
                        }

                        // No Xing
                        if (Instance.optNoXing.Checked == true)
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                withBlock1.Type = TileType.NoXing;
                                withBlock1.Data1 = 0;
                                withBlock1.Data2 = 0;
                                withBlock1.Data3 = 0;
                            }
                            else
                            {
                                withBlock1.Type2 = TileType.NoXing;
                                withBlock1.Data1_2 = 0;
                                withBlock1.Data2_2 = 0;
                                withBlock1.Data3_2 = 0;
                            }
                        }
                    }
                }
                else if (ReferenceEquals(Instance.tabpages.SelectedTab, Instance.tpDirBlock))
                {
                    // find what tile it is
                    X -= X / GameState.PicX * GameState.PicX;
                    Y -= Y / GameState.PicY * GameState.PicY;

                    // see if it hits an arrow
                    for (i = 0; i <= 4; i++)
                    {
                        // flip the value.
                        if (X >= GameState.DirArrowX[i] & X <= GameState.DirArrowX[i] + 8)
                        {
                            if (Y >= GameState.DirArrowY[i] & Y <= GameState.DirArrowY[i] + 8)
                            {
                                // flip the value.
                                bool localIsDirBlocked() { byte argdir = (byte)i; var ret = GameLogic.IsDirBlocked(ref Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].DirBlock, ref argdir); return ret; }

                                byte argdir = (byte)i;
                                GameLogic.SetDirBlock(ref Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].DirBlock, ref argdir, !localIsDirBlocked());
                                break;
                            }
                        }
                    }
                }
                else if (ReferenceEquals(Instance.tabpages.SelectedTab, Instance.tpEvents))
                {
                    if (frmEditor_Event.Instance.Visible == false)
                    {
                        if (Event.EventCopy)
                        {
                            Event.CopyEvent_Map(GameState.CurX, GameState.CurY);
                        }
                        else if (Event.EventPaste)
                        {
                            Event.PasteEvent_Map(GameState.CurX, GameState.CurY);
                        }
                        else
                        {
                            Event.AddEvent(GameState.CurX, GameState.CurY);
                        }
                    }
                }
            }

            if (GameClient.IsMouseButtonDown(MouseButton.Right))
            {
                if (ReferenceEquals(Instance.tabpages.SelectedTab, Instance.tpTiles))
                {
                    if (GameState.EditorTileWidth == 1 & GameState.EditorTileHeight == 1) // single tile
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, CurLayer, false, (byte)Instance.cmbAutoTile.SelectedIndex, 1);
                    }
                    else if (Instance.cmbAutoTile.SelectedIndex == 0) // multi tile!
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, CurLayer, true, 0, 1);
                    }
                    else
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, CurLayer, true, (byte)Instance.cmbAutoTile.SelectedIndex, 1);
                    }
                }
                else if (ReferenceEquals(Instance.tabpages.SelectedTab, Instance.tpAttributes))
                {
                    {
                        ref var withBlock2 = ref Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY];
                        // clear attribute
                        withBlock2.Type = 0;
                        withBlock2.Data1 = 0;
                        withBlock2.Data2 = 0;
                        withBlock2.Data3 = 0;
                    }
                }
                else if (ReferenceEquals(Instance.tabpages.SelectedTab, Instance.tpEvents))
                {
                    Event.DeleteEvent(GameState.CurX, GameState.CurY);
                }
            }
        }

        public void MapEditorCancel()
        {
            ByteStream buffer;

            if (GameState.MyEditorType != (int)EditorType.Map)
                return;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CNeedMap);
            buffer.WriteInt32(1);
            NetworkConfig.Socket?.SendData(buffer.Data, buffer.Head);
            GameState.MyEditorType = -1;
            GameState.GettingMap = true;
            NetworkSend.SendCloseEditor();

            frmEditor_Event.Instance.Dispose();
        }

        public void MapEditorSend()
        {
            Map.SendMap();
            GameState.MyEditorType = -1;
            GameState.GettingMap = true;
            NetworkSend.SendCloseEditor();
        }

        public static void MapEditorSetTile(int X, int Y, int CurLayer, bool multitile = false, byte theAutotile = 0, byte eraseTile = 0)
        {
            int x2;
            int y2;
            int newTileX;
            int newTileY;

            newTileX = GameState.EditorTileX;
            newTileY = GameState.EditorTileY;

            if (Conversions.ToBoolean(eraseTile))
            {
                newTileX = 0;
                newTileY = 0;
            }

            if (theAutotile > 0)
            {
                {
                    ref var withBlock = ref Core.Type.MyMap.Tile[X, Y];
                    // set layer
                    withBlock.Layer[CurLayer].X = newTileX;
                    withBlock.Layer[CurLayer].Y = newTileY;
                    if (Conversions.ToBoolean(eraseTile))
                    {
                        withBlock.Layer[CurLayer].Tileset = 0;
                    }
                    else
                    {
                        withBlock.Layer[CurLayer].Tileset = Instance.cmbTileSets.SelectedIndex;
                    }
                    withBlock.Layer[CurLayer].AutoTile = theAutotile;
                    Autotile.CacheRenderState(X, Y, CurLayer);
                }

                // do a re-init so we can see our changes
                Autotile.InitAutotiles();
                return;
            }

            if (!multitile) // single
            {
                {
                    ref var withBlock1 = ref Core.Type.MyMap.Tile[X, Y];
                    // set layer
                    withBlock1.Layer[CurLayer].X = newTileX;
                    withBlock1.Layer[CurLayer].Y = newTileY;
                    if (Conversions.ToBoolean(eraseTile))
                    {
                        withBlock1.Layer[CurLayer].Tileset = 0;
                    }
                    else
                    {
                        withBlock1.Layer[CurLayer].Tileset = Instance.cmbTileSets.SelectedIndex;
                    }
                    withBlock1.Layer[CurLayer].AutoTile = 0;
                    Autotile.CacheRenderState(X, Y, CurLayer);
                }
            }
            else // multitile
            {
                y2 = 0; // starting tile for y axis
                var loopTo = GameState.CurY + GameState.EditorTileHeight - 1;
                for (Y = GameState.CurY; Y <= loopTo; Y++)
                {
                    x2 = 0; // re-set x count every y loop
                    var loopTo1 = GameState.CurX + GameState.EditorTileWidth - 1;
                    for (X = GameState.CurX; X <= loopTo1; X++)
                    {
                        if (X >= 0 & X <= Core.Type.MyMap.MaxX)
                        {
                            if (Y >= 0 & Y <= Core.Type.MyMap.MaxY)
                            {
                                {
                                    ref var withBlock2 = ref Core.Type.MyMap.Tile[X, Y];
                                    withBlock2.Layer[CurLayer].X = newTileX + x2;
                                    withBlock2.Layer[CurLayer].Y = newTileY + y2;
                                    if (Conversions.ToBoolean(eraseTile))
                                    {
                                        withBlock2.Layer[CurLayer].Tileset = 0;
                                    }
                                    else
                                    {
                                        withBlock2.Layer[CurLayer].Tileset = Instance.cmbTileSets.SelectedIndex;
                                    }
                                    withBlock2.Layer[CurLayer].AutoTile = 0;
                                    Autotile.CacheRenderState(X, Y, CurLayer);
                                }
                            }
                        }
                        x2 += 1;
                    }
                    y2 += 1;
                }
            }
        }

        public static void MapEditorHistory()
        {
            long i;

            if (GameState.HistoryIndex == GameState.MaxTileHistory)
            {
                for (i = 0L; i <= GameState.MaxTileHistory - 1; i++)
                {
                    Core.Type.TileHistory[(int)i] = Core.Type.TileHistory[(int)(i + 1L)];
                    GameState.TileHistoryHighIndex = GameState.TileHistoryHighIndex - 1;
                }
            }
            else
            {
                GameState.HistoryIndex = GameState.HistoryIndex + 1;
                GameState.TileHistoryHighIndex = GameState.TileHistoryHighIndex + 1;
                if (GameState.TileHistoryHighIndex > GameState.HistoryIndex)
                {
                    GameState.TileHistoryHighIndex = GameState.HistoryIndex;
                }
            }

        }

        public void MapEditorClearLayer(LayerType layer)
        {
            GameLogic.Dialogue("Map Editor", "Clear Layer: " + layer.ToString(), "Are you sure you wish to clear this layer?", (byte)DialogueType.ClearLayer, (byte)DialogueStyle.YesNo, cmbLayers.SelectedIndex, cmbAutoTile.SelectedIndex);
        }

        public void MapEditorFillLayer(LayerType layer, byte theAutotile = 0, byte tileX = 0, byte tileY = 0)
        {
            GameLogic.Dialogue("Map Editor", "Fill Layer: " + layer.ToString(), "Are you sure you wish to fill this layer?", (byte)DialogueType.FillLayer, (byte)DialogueStyle.YesNo, cmbLayers.SelectedIndex, cmbAutoTile.SelectedIndex, tileX, tileY, cmbTileSets.SelectedIndex);
        }

        public static void MapEditorEyeDropper()
        {
            int CurLayer;

            CurLayer = Instance.cmbLayers.SelectedIndex;

            {
                ref var withBlock = ref Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY];
                if (withBlock.Layer[CurLayer].Tileset > 0)
                {
                    Instance.cmbTileSets.SelectedIndex = withBlock.Layer[CurLayer].Tileset - 1;
                }
                else
                {
                    Instance.cmbTileSets.SelectedIndex = 1;
                }
                MapEditorChooseTile((int)MouseButtons.Left, withBlock.Layer[CurLayer].X * GameState.PicX, withBlock.Layer[CurLayer].Y * GameState.PicY);
                GameState.EyeDropper = !GameState.EyeDropper;
            }
        }

        public void MapEditorUndo()
        {
            var tileChanged = default(bool);

            if (GameState.HistoryIndex == 0)
                return;

            GameState.HistoryIndex = GameState.HistoryIndex - 1;

            for (int x = 0, loopTo = Core.Type.MyMap.MaxX - 1; x <= loopTo; x++)
            {
                for (int y = 0, loopTo1 = Core.Type.MyMap.MaxY - 1; y <= loopTo1; y++)
                {
                    for (int i = 0; i < (int)LayerType.Count - 1; i++)
                    {
                        {
                            ref var withBlock = ref Core.Type.MyMap.Tile[x, y];
                            if (!(Core.Type.MyMap.Tile[x, y].Type == Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Type) | !(withBlock.Layer[i].X == Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].X) | !(withBlock.Layer[i].Y == Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].Y) | !(withBlock.Layer[i].Tileset == Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].Tileset))
                            {
                                tileChanged = true;
                            }

                            withBlock.Data1 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Data1;
                            withBlock.Data2 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Data2;
                            withBlock.Data3 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Data3;
                            withBlock.Data1_2 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Data1_2;
                            withBlock.Data2_2 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Data2_2;
                            withBlock.Data3_2 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Data3_2;
                            withBlock.Type = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Type;
                            withBlock.Type2 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Type2;
                            withBlock.DirBlock = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].DirBlock;
                            withBlock.Layer[i].X = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].X;
                            withBlock.Layer[i].Y = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].Y;
                            withBlock.Layer[i].Tileset = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].Tileset;
                            withBlock.Layer[i].AutoTile = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].AutoTile;
                            Autotile.CacheRenderState(x, y, i);
                        }
                    }
                }
            }

            if (!tileChanged)
            {
                MapEditorUndo();
            }
        }

        public void MapEditorRedo()
        {
            var tileChanged = default(bool);

            if (GameState.TileHistoryHighIndex > 0 & (GameState.TileHistoryHighIndex == GameState.HistoryIndex | GameState.HistoryIndex == GameState.MaxTileHistory))
            {
                return;
            }

            if (GameState.HistoryIndex == GameState.MaxTileHistory)
                return;

            GameState.HistoryIndex = GameState.HistoryIndex + 1;

            for (int x = 0, loopTo = Core.Type.MyMap.MaxX -1; x <= loopTo; x++)
            {
                for (int y = 0, loopTo1 = Core.Type.MyMap.MaxY - 1; y <= loopTo1; y++)
                {
                    for (int i = 0; i < (int)LayerType.Count - 1; i++)
                    {
                        {
                            ref var withBlock = ref Core.Type.MyMap.Tile[x, y];
                            if (!(Core.Type.MyMap.Tile[x, y].Type == Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Type) | !(withBlock.Layer[i].X == Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].X) | !(withBlock.Layer[i].Y == Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].Y) | !(withBlock.Layer[i].Tileset == Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].Tileset))
                            {
                                tileChanged = true;
                            }

                            withBlock.Data1 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Data1;
                            withBlock.Data2 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Data2;
                            withBlock.Data3 = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Data3;
                            withBlock.Type = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Type;
                            withBlock.DirBlock = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].DirBlock;

                            withBlock.Layer[i].X = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].X;
                            withBlock.Layer[i].Y = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].Y;
                            withBlock.Layer[i].Tileset = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].Tileset;
                            withBlock.Layer[i].AutoTile = Core.Type.TileHistory[GameState.HistoryIndex].Tile[x, y].Layer[i].AutoTile;
                            Autotile.CacheRenderState(x, y, i);
                        }
                    }
                }
            }

            if (!tileChanged)
            {
                MapEditorRedo();
            }
        }

        public void ClearAttributeDialogue()
        {
            fraNPCSpawn.Visible = false;
            fraResource.Visible = false;
            fraMapItem.Visible = false;
            fraMapWarp.Visible = false;
            fraShop.Visible = false;
            fraHeal.Visible = false;
            fraTrap.Visible = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Core.Type.MyMap.Name = txtName.Text;
        }

        private void frmEditor_Map_FormClosing(object sender, FormClosingEventArgs e)
        {
            MapEditorCancel();
        }

        private void scrMapBrightness_Scroll(object sender, ScrollEventArgs e)
        {
            Core.Type.MyMap.Brightness = (byte)scrlMapBrightness.Value;
            lblMapBrightness.Text = "Brightness: " + scrlMapBrightness.Value;
        }

        #endregion

        #region Drawing

        private void tsbCopyMap_Click(object sender, EventArgs e)
        {
            int i;
            int X;
            int Y;

            if (GameState.CopyMap == false)
            {
                Core.Type.Tile = new Core.Type.TileStruct[(Core.Type.MyMap.MaxX + 1), (Core.Type.MyMap.MaxY + 1)];
                GameState.TmpMaxX = Core.Type.MyMap.MaxX;
                GameState.TmpMaxY = Core.Type.MyMap.MaxY;

                var loopTo = (int)Core.Type.MyMap.MaxX;
                for (X = 0; X <= loopTo; X++)
                {
                    var loopTo1 = (int)Core.Type.MyMap.MaxY;
                    for (Y = 0; Y <= loopTo1; Y++)
                    {
                        {
                            ref var withBlock = ref Core.Type.MyMap.Tile[X, Y];
                            Core.Type.Tile[X, Y].Layer = new Core.Type.TileDataStruct[10];

                            Core.Type.Tile[X, Y].Data1 = withBlock.Data1;
                            Core.Type.Tile[X, Y].Data2 = withBlock.Data2;
                            Core.Type.Tile[X, Y].Data3 = withBlock.Data3;
                            Core.Type.Tile[X, Y].Type = withBlock.Type;
                            Core.Type.Tile[X, Y].DirBlock = withBlock.DirBlock;

                            for (i = 0; i < (int)LayerType.Count - 1; i++)
                            {
                                Core.Type.Tile[X, Y].Layer[i].X = withBlock.Layer[i].X;
                                Core.Type.Tile[X, Y].Layer[i].Y = withBlock.Layer[i].Y;
                                Core.Type.Tile[X, Y].Layer[i].Tileset = withBlock.Layer[i].Tileset;
                                Core.Type.Tile[X, Y].Layer[i].AutoTile = withBlock.Layer[i].AutoTile;
                            }
                        }
                    }
                }

                GameState.CopyMap = true;
                Interaction.MsgBox("Map copied. Go to another map to paste it.");
            }
            else
            {
                Core.Type.MyMap.Tile = new Core.Type.TileStruct[(GameState.TmpMaxX + 1), (GameState.TmpMaxY + 1)];
                Core.Type.Autotile = new Core.Type.AutotileStruct[(GameState.TmpMaxX + 1), (GameState.TmpMaxY + 1)];
                Core.Type.MyMap.MaxX = GameState.TmpMaxX;
                Core.Type.MyMap.MaxY = GameState.TmpMaxY;

                var loopTo2 = (int)Core.Type.MyMap.MaxX;
                for (X = 0; X <= loopTo2; X++)
                {
                    var loopTo3 = (int)Core.Type.MyMap.MaxY;
                    for (Y = 0; Y <= loopTo3; Y++)
                    {
                        {
                            ref var withBlock1 = ref Core.Type.MyMap.Tile[X, Y];
                            Array.Resize(ref Core.Type.MyMap.Tile[X, Y].Layer, 10);
                            Array.Resize(ref Core.Type.Autotile[X, Y].Layer, 10);

                            withBlock1.Data1 = Core.Type.Tile[X, Y].Data1;
                            withBlock1.Data2 = Core.Type.Tile[X, Y].Data2;
                            withBlock1.Data3 = Core.Type.Tile[X, Y].Data3;
                            withBlock1.Type = Core.Type.Tile[X, Y].Type;
                            withBlock1.DirBlock = Core.Type.Tile[X, Y].DirBlock;

                            for (i = 0; i < (int)LayerType.Count - 1; i++)
                            {
                                withBlock1.Layer[i].X = Core.Type.Tile[X, Y].Layer[i].X;
                                withBlock1.Layer[i].Y = Core.Type.Tile[X, Y].Layer[i].Y;
                                withBlock1.Layer[i].Tileset = Core.Type.Tile[X, Y].Layer[i].Tileset;
                                withBlock1.Layer[i].AutoTile = Core.Type.Tile[X, Y].Layer[i].AutoTile;
                                Autotile.CacheRenderState(X, Y, i);
                            }
                        }
                    }
                }
                GameState.CopyMap = false;
            }
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            MapEditorUndo();
        }

        private void tsbRedo_Click(object sender, EventArgs e)
        {
            MapEditorRedo();
        }

        private void tsbOpacity_Click(object sender, EventArgs e)
        {
            GameState.HideLayers = !GameState.HideLayers;
        }

        private void tsbLight_Click(object sender, EventArgs e)
        {
            GameState.Night = !GameState.Night;
        }

        private void tsbScreenshot_Click(object sender, EventArgs e)
        {
            GameClient.TakeScreenshot();
        }

        private void optAnimation_CheckedChanged(object sender, EventArgs e)
        {
            if (optAnimation.Checked == false)
                return;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraAnimation.Visible = true;
        }

        private void brnAnimation_Click(object sender, EventArgs e)
        {
            GameState.EditorAnimation = cmbAnimation.SelectedIndex;
            pnlAttributes.Visible = false;
            fraAnimation.Visible = false;
        }

        private void btnLight_Click(object sender, EventArgs e)
        {
            GameState.EditorLight = scrlLight.Value;
            if (chkFlicker.Checked)
            {
                GameState.EditorFlicker = 1;
            }
            else
            {
                GameState.EditorFlicker = 0;
            }

            if (chkShadow.Checked)
            {
                GameState.EditorShadow = 1;
            }
            else
            {
                GameState.EditorShadow = 0;
            }

            pnlAttributes.Visible = false;
            fraMapLight.Visible = false;
        }

        private void optLight_CheckedChanged(object sender, EventArgs e)
        {
            if (optLight.Checked == false)
                return;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraMapLight.Visible = true;
        }

        private void scrlLight_ValueChanged(object sender, EventArgs e)
        {
            lblRadius.Text = "Radius: " + scrlLight.Value;
        }

        private void chkRespawn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoMapRespawn.Checked == true)
            {
                Core.Type.MyMap.NoRespawn = Conversions.ToBoolean(1);
            }
            else
            {
                Core.Type.MyMap.NoRespawn = Conversions.ToBoolean(0);
            }
        }

        private void chkIndoors_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndoors.Checked == true)
            {
                Core.Type.MyMap.Indoors = Conversions.ToBoolean(1);
            }
            else
            {
                Core.Type.MyMap.Indoors = Conversions.ToBoolean(0);
            }
        }

        private void cmbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameState.EditorAttribute = (byte)(cmbAttribute.SelectedIndex);
        }

        private void tsbDeleteMap_Click(object sender, EventArgs e)
        {
            Map.ClearMap();
        }

        private void picBackSelect_Paint(object sender, PaintEventArgs e)
        {
            if (!General.Client.IsActive)
            {
                DrawTileset();
            }
        }

        #endregion

    }
}