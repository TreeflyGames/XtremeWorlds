using System.Windows.Forms;
using Assimp.Configs;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mirage.Sharp.Asfw;
using MonoGame.Extended.Collisions.Layers;
using MonoGame.Extended.Content.Tiled;
using static Core.Type;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

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

            UpdateDirBlock();

            ToolStrip.BringToFront();

            scrlFog.Maximum = GameState.NumFogs;
            scrlMapItem.Maximum = Constant.MAX_ITEMS;
        }

        private static void UpdateDirBlock()
        {
            GameState.DirArrowX[(int)Direction.Up] = 12;
            GameState.DirArrowY[(int)Direction.Up] = 0;
            GameState.DirArrowX[(int)Direction.Down] = 12;
            GameState.DirArrowY[(int)Direction.Down] = 23;
            GameState.DirArrowX[(int)Direction.Left] = 0;
            GameState.DirArrowY[(int)Direction.Left] = 12;
            GameState.DirArrowX[(int)Direction.Right] = 23;
            GameState.DirArrowY[(int)Direction.Right] = 12;
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

        private void frmEditor_Map_Resize(object sender, EventArgs e)
        {
            this.AutoScroll = true;
            this.PerformLayout();
        }

        private void frmEditor_Map_Activated(object sender, EventArgs e)
        {
            this.AutoScroll = true;
        }

        private void DrawItem()
        {
            int itemNum;

            itemNum = Core.Data.Item[scrlMapItem.Value].Icon;

            if (itemNum < 0 | itemNum > GameState.NumItems)
            {
                picMapItem.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Items, itemNum + GameState.GfxExt)))
            {
                picMapItem.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.Combine(Core.Path.Items, itemNum + GameState.GfxExt));
            }

        }

        public static void DrawTileset()
        {
            int tilesetIndex;

            // Ensure a tileset is selected
            if (Instance.cmbTileSets.SelectedIndex == -1)
            {
                return;
            }

            // Get the selected tileset index
            tilesetIndex = GameState.CurTileset;

            if (tilesetIndex == 0)
            {
                return;
            }

            // Get the graphics information for the selected tileset
            string tilesetPath = System.IO.Path.Combine(Core.Path.Tilesets, tilesetIndex.ToString());
            var gfxInfo = GameClient.GetGfxInfo(tilesetPath);

            // Handle varying tileset sizes
            if (!System.IO.File.Exists(tilesetPath + GameState.GfxExt))
            {
                Instance.picBackSelect.Image = null;
                return;
            }

            using (var srcImage = System.Drawing.Image.FromFile(tilesetPath + GameState.GfxExt))
            {
                int srcWidth = srcImage.Width;
                int srcHeight = srcImage.Height;
                int picWidth = Instance.picBackSelect.Width;
                int picHeight = Instance.picBackSelect.Height;

                // Calculate scale to fit PictureBox while preserving aspect ratio
                float scaleX = (float)picWidth / srcWidth;
                float scaleY = (float)picHeight / srcHeight;
                float scale = Math.Min(scaleX, scaleY);

                int destWidth = (int)Math.Round(srcWidth * scale);
                int destHeight = (int)Math.Round(srcHeight * scale);

                using (var bmp = new System.Drawing.Bitmap(picWidth, picHeight))
                using (var g = System.Drawing.Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.Black);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    // Center the image
                    int offsetX = (picWidth - destWidth) / 2;
                    int offsetY = (picHeight - destHeight) / 2;

                    g.DrawImage(srcImage, offsetX, offsetY, destWidth, destHeight);

                    // Draw selection rectangle
                    int scaledX = (int)Math.Round(GameState.EditorTileSelStart.X * GameState.PicX * scale) + offsetX;
                    int scaledY = (int)Math.Round(GameState.EditorTileSelStart.Y * GameState.PicY * scale) + offsetY;
                    int scaledWidth = (int)Math.Round(GameState.EditorTileWidth * GameState.PicX * scale);
                    int scaledHeight = (int)Math.Round(GameState.EditorTileHeight * GameState.PicY * scale);

                    using (var pen = new System.Drawing.Pen(System.Drawing.Color.Red, Math.Max(1, (int)Math.Round(1f * scale))))
                    {
                        g.DrawRectangle(pen, scaledX, scaledY, scaledWidth, scaledHeight);
                    }

                    Instance.picBackSelect.Image?.Dispose();
                    Instance.picBackSelect.Image = (System.Drawing.Image)bmp.Clone();
                }
            }
        }

        public static void DrawSelectionRectangle(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, float scale)
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

        #endregion

        #region Toolbar

        private void TsbSave_Click(object sender, EventArgs e)
        {
            UpdateMap();
            Dispose();
        }

        private static void UpdateMap()
        {
            int x2;
            int y2;
            Tile[,] tempArr;

            if (!Information.IsNumeric(Instance.txtMaxX.Text))
                Instance.txtMaxX.Text = Data.MyMap.MaxX.ToString();

            if (Conversion.Val(Instance.txtMaxX.Text) < SettingsManager.Instance.CameraWidth)
                Instance.txtMaxX.Text = SettingsManager.Instance.CameraWidth.ToString();

            if (Conversion.Val(Instance.txtMaxX.Text) > System.Byte.MaxValue)
                Instance.txtMaxX.Text = System.Byte.MaxValue.ToString();

            if (!Information.IsNumeric(Instance.txtMaxY.Text))
                Instance.txtMaxY.Text = Data.MyMap.MaxY.ToString();

            if (Conversion.Val(Instance.txtMaxY.Text) < SettingsManager.Instance.CameraHeight)
                Instance.txtMaxY.Text = SettingsManager.Instance.CameraHeight.ToString();

            if (Conversion.Val(Instance.txtMaxY.Text) > System.Byte.MaxValue)
                Instance.txtMaxY.Text = System.Byte.MaxValue.ToString();

            {
                ref var withBlock = ref Data.MyMap;
                withBlock.Name = Instance.txtName.Text;
                if (Instance.lstMusic.SelectedIndex >= 0)
                {
                    withBlock.Music = Instance.lstMusic.Items[Instance.lstMusic.SelectedIndex].ToString();
                }
                else
                {
                    withBlock.Music = "";
                }

                if (Instance.lstShop.SelectedIndex >= 0)
                {
                    withBlock.Shop = Instance.lstShop.SelectedIndex;
                }
                else
                {
                    withBlock.Shop = 0;
                }

                withBlock.Up = (int)Math.Round(Conversion.Val(Instance.txtUp.Text));
                withBlock.Down = (int)Math.Round(Conversion.Val(Instance.txtDown.Text));
                withBlock.Left = (int)Math.Round(Conversion.Val(Instance.txtLeft.Text));
                withBlock.Right = (int)Math.Round(Conversion.Val(Instance.txtRight.Text));
                withBlock.Moral = (byte)Instance.lstMoral.SelectedIndex;
                withBlock.BootMap = (int)Math.Round(Conversion.Val(Instance.txtBootMap.Text));
                withBlock.BootX = (byte)Math.Round(Conversion.Val(Instance.txtBootX.Text));
                withBlock.BootY = (byte)Math.Round(Conversion.Val(Instance.txtBootY.Text));

                // set the data before changing it  
                tempArr = (Tile[,])withBlock.Tile.Clone();

                x2 = withBlock.MaxX;
                y2 = withBlock.MaxY;

                // change the data  
                withBlock.MaxX = (byte)Math.Round(Conversion.Val(Instance.txtMaxX.Text));
                withBlock.MaxY = (byte)Math.Round(Conversion.Val(Instance.txtMaxY.Text));

                withBlock.Tile = new Core.Type.Tile[(withBlock.MaxX), (withBlock.MaxY)];

                for (int i = 0; i < GameState.MaxTileHistory; i++)
                    Data.TileHistory[i].Tile = new Tile[(withBlock.MaxX), (withBlock.MaxY)];

                Data.Autotile = new Core.Type.Autotile[(withBlock.MaxX), (withBlock.MaxY)];

                if (x2 > withBlock.MaxX)
                    x2 = withBlock.MaxX;

                if (y2 > withBlock.MaxY)
                    y2 = withBlock.MaxY;

                int layerCount = System.Enum.GetValues(typeof(MapLayer)).Length;

                var loopTo = (int)withBlock.MaxX;
                for (int x = 0; x < loopTo; x++)
                {
                    var loopTo1 = (int)withBlock.MaxY;
                    for (int y = 0; y < loopTo1; y++)
                    {
                        withBlock.Tile[x, y].Layer = new Core.Type.Layer[layerCount];
                        Data.Autotile[x, y].Layer = new Core.Type.QuarterTile[layerCount];

                        for (int i = 0; i < GameState.MaxTileHistory; i++)
                            Data.TileHistory[i].Tile[x, y].Layer = new Core.Type.Layer[layerCount];

                        if (x < x2)
                        {
                            if (y < y2)
                            {
                                withBlock.Tile[x, y] = tempArr[x, y];
                            }
                        }
                    }
                }
            }

            MapEditorSend();
            GameState.GettingMap = true;
        }

        private void TsbFill_Click(object sender, EventArgs e)
        {
            MapLayer layer = (MapLayer)cmbLayers.SelectedIndex;
            MapEditorFillLayer(layer, (byte)cmbAutoTile.SelectedIndex, (byte)GameState.EditorTileX, (byte)GameState.EditorTileY);
        }

        private void TsbClear_Click(object sender, EventArgs e)
        {
            MapLayer layer = (MapLayer)Enum.ToObject(typeof(MapLayer), cmbLayers.SelectedIndex);
            MapEditorClearLayer(layer);
        }

        private void TsbEyeDropper_Click(object sender, EventArgs e)
        {
            UpdateEyeDropper();
        }

        private static void UpdateEyeDropper()
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
            if (GameState.CurTileset > GameState.NumTileSets)
            {
                cmbTileSets.SelectedIndex = 0;
            }

            Data.MyMap.Tileset = GameState.CurTileset;

            GameState.EditorTileSelStart = new Point(0, 0);
            GameState.EditorTileSelEnd = new Point(1, 1);
        }

        private void CmbAutoTile_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameState.CurAutotileType = cmbAutoTile.SelectedIndex;
            if (cmbAutoTile.SelectedIndex == 0)
            {
                GameState.EditorTileWidth = 1;
                GameState.EditorTileHeight = 1;
            }

            MapEditorChooseTile((int)MouseButtons.Left, GameState.EditorTileX, GameState.EditorTileY);
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

            scrlMapWarpMap.Maximum = Constant.MAX_MAPS;
            scrlMapWarpMap.Value = 1;
            scrlMapWarpX.Maximum = byte.MaxValue;
            scrlMapWarpY.Maximum = byte.MaxValue;
            scrlMapWarpX.Value = 0;
            scrlMapWarpY.Value = 0;
        }

        private void ScrlMapItem_ValueChanged(object sender, EventArgs e)
        {
            if (Core.Data.Item[scrlMapItem.Value].Type == (byte)ItemCategory.Currency | Core.Data.Item[scrlMapItem.Value].Stackable == 1)
            {
                scrlMapItemValue.Enabled = true;
            }
            else
            {
                scrlMapItemValue.Value = 1;
                scrlMapItemValue.Enabled = false;
            }

            DrawItem();
            lblMapItem.Text = (scrlMapItem.Value + 1) + ". " + Core.Data.Item[scrlMapItem.Value].Name + " x" + scrlMapItemValue.Value;
        }

        private void ScrlMapItemValue_ValueChanged(object sender, EventArgs e)
        {
            lblMapItem.Text = (scrlMapItem.Value + 1) + ". " + Core.Data.Item[scrlMapItem.Value].Name + " x" + scrlMapItemValue.Value;
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

            lblMapItem.Text = Core.Data.Item[scrlMapItem.Value].Name + " x" + scrlMapItemValue.Value;
            ScrlMapItem_ValueChanged(sender, e);
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
            lblResource.Text = "Resource: " + Data.Resource[scrlResource.Value].Name;
        }

        private void OptResource_CheckedChanged(object sender, EventArgs e)
        {
            if (optResource.Checked == false)
                return;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraResource.Visible = true;
            ScrlResource_ValueChanged(sender, e);
        }

        private void BtnNpcSpawn_Click(object sender, EventArgs e)
        {
            GameState.SpawnNpcNum = lstNpc.SelectedIndex;
            GameState.SpawnNpcDir = scrlNpcDir.Value;
            pnlAttributes.Visible = false;
            fraNpcSpawn.Visible = false;
        }

        private void OptNpcSpawn_CheckedChanged(object sender, EventArgs e)
        {
            int n;

            if (optNpcSpawn.Checked == false)
                return;

            lstNpc.Items.Clear();

            for (n = 0; n < Constant.MAX_MAP_NPCS; n++)
            {
                if (Data.MyMap.Npc[n] > 0)
                {
                    lstNpc.Items.Add(n + ": " + Data.Npc[Data.MyMap.Npc[n]].Name);
                }
                else
                {
                    lstNpc.Items.Add(n);
                }
            }

            scrlNpcDir.Value = 0;
            lstNpc.SelectedIndex = 0;

            ClearAttributeDialogue();
            pnlAttributes.Visible = true;
            fraNpcSpawn.Visible = true;
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
            cmbHeal.SelectedIndex = 0;
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

        private void ScrlNpcDir_Scroll(object sender, EventArgs e)
        {
            switch (scrlNpcDir.Value)
            {
                case 0:
                    {
                        lblNpcDir.Text = "Direction: Up";
                        break;
                    }
                case 1:
                    {
                        lblNpcDir.Text = "Direction: Down";
                        break;
                    }
                case 2:
                    {
                        lblNpcDir.Text = "Direction: Left";
                        break;
                    }
                case 3:
                    {
                        lblNpcDir.Text = "Direction: Right";
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

        #region Npc's

        private void CmbNpcList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMapNpc.SelectedIndex > 0)
            {
                lstMapNpc.Items[lstMapNpc.SelectedIndex] = lstMapNpc.SelectedIndex + 1 + ": " + Data.Npc[cmbNpcList.SelectedIndex].Name;
                Data.MyMap.Npc[lstMapNpc.SelectedIndex] = cmbNpcList.SelectedIndex;
            }
        }

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            if (lstMusic.SelectedIndex > 0)
            {
                string selectedFile = lstMusic.Items[lstMusic.SelectedIndex].ToString();

                // If the selected music file is a MIDI file
                if (SettingsManager.Instance.MusicExt == ".mid")
                {
                    Sound.PlayMidi(System.IO.Path.Combine(Core.Path.Music, selectedFile));
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
            Data.MyMap.Weather = (byte)cmbWeather.SelectedIndex;
        }

        private void ScrlFog_Scroll(object sender, EventArgs e)
        {
            Data.MyMap.Fog = scrlFog.Value;
            lblFogIndex.Text = "Fog: " + scrlFog.Value;
        }

        private void ScrlIntensity_Scroll(object sender, EventArgs e)
        {
            Data.MyMap.WeatherIntensity = scrlIntensity.Value;
            lblIntensity.Text = "Intensity: " + scrlIntensity.Value;
        }

        private void ScrlFogSpeed_Scroll(object sender, EventArgs e)
        {
            Data.MyMap.FogSpeed = (byte)scrlFogSpeed.Value;
            lblFogSpeed.Text = "FogSpeed: " + scrlFogSpeed.Value;
        }

        private void ScrlFogOpacity_Scroll(object sender, EventArgs e)
        {
            Data.MyMap.FogOpacity = (byte)scrlFogOpacity.Value;
            lblFogOpacity.Text = "Fog Alpha: " + scrlFogOpacity.Value;
        }

        private void ChkUseTint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTint.Checked == true)
            {
                Data.MyMap.MapTint = true;
            }
            else
            {
                Data.MyMap.MapTint = false;
            }
        }

        private void ScrlMapRed_Scroll(object sender, EventArgs e)
        {
            Data.MyMap.MapTintR = (byte)scrlMapRed.Value;
            lblMapRed.Text = "Red: " + scrlMapRed.Value;
        }

        private void ScrlMapGreen_Scroll(object sender, EventArgs e)
        {
            Data.MyMap.MapTintG = (byte)scrlMapGreen.Value;
            lblMapGreen.Text = "Green: " + scrlMapGreen.Value;
        }

        private void ScrlMapBlue_Scroll(object sender, EventArgs e)
        {
            Data.MyMap.MapTintB = (byte)scrlMapBlue.Value;
            lblMapBlue.Text = "Blue: " + scrlMapBlue.Value;
        }

        private void ScrlMapAlpha_Scroll(object sender, EventArgs e)
        {
            Data.MyMap.MapTintA = (byte)scrlMapAlpha.Value;
            lblMapAlpha.Text = "Alpha: " + scrlMapAlpha.Value;
        }

        private void CmbPanorama_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.MyMap.Panorama = (byte)cmbPanorama.SelectedIndex;
        }

        private void CmbParallax_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.MyMap.Parallax = (byte)cmbParallax.SelectedIndex;
        }

        public static void MapPropertiesInit()
        {
            int x;
            int y;
            int i;

            Instance.txtName.Text = Strings.Trim(Data.MyMap.Name);

            // find the music we have set
            Instance.lstMusic.Items.Clear();
            Instance.lstMusic.Items.Add("None");
            Instance.lstMusic.SelectedIndex = 0;

            General.CacheMusic();

            var loopTo = Information.UBound(Sound.MusicCache);
            for (i = 0; i < loopTo; i++)
                Instance.lstMusic.Items.Add(Sound.MusicCache[i]);

            var loopTo1 = Instance.lstMusic.Items.Count;
            for (i = 0; i < loopTo1; i++)
            {
                if ((Instance.lstMusic.Items[i].ToString() ?? "") == (Data.MyMap.Music ?? ""))
                {
                    Instance.lstMusic.SelectedIndex = i;
                    break;
                }
            }

            // find the shop we have set
            Instance.lstShop.Items.Clear();

            for (i = 0; i < Constant.MAX_SHOPS; i++)
                Instance.lstShop.Items.Add(Data.Shop[i].Name);

            Instance.lstShop.SelectedIndex = 0;

            var loopTo2 = Instance.lstShop.Items.Count;
            for (i = 0; i < loopTo2; i++)
            {
                if ((Instance.lstShop.Items[i].ToString() ?? "") == (Data.Shop[Data.MyMap.Shop].Name ?? ""))
                {
                    Instance.lstShop.SelectedIndex = i;
                    break;
                }
            }

            // find the shop we have set
            Instance.lstMoral.Items.Clear();

            for (i = 0; i < Constant.MAX_MORALS; i++)
                Instance.lstMoral.Items.Add(Data.Moral[i].Name);

            Instance.lstMoral.SelectedIndex = 0;

            var loopTo3 = Instance.lstMoral.Items.Count;
            for (i = 0; i < loopTo3; i++)
            {
                if ((Instance.lstMoral.Items[i].ToString() ?? "") == (Data.Moral[Data.MyMap.Moral].Name ?? ""))
                {
                    Instance.lstMoral.SelectedIndex = i;
                    break;
                }
            }

            Instance.chkTint.Checked = Data.MyMap.MapTint;
            Instance.chkNoMapRespawn.Checked = Data.MyMap.NoRespawn;
            Instance.chkIndoors.Checked = Data.MyMap.Indoors;

            // rest of it
            Instance.txtUp.Text = Data.MyMap.Up.ToString();
            Instance.txtDown.Text = Data.MyMap.Down.ToString();
            Instance.txtLeft.Text = Data.MyMap.Left.ToString();
            Instance.txtRight.Text = Data.MyMap.Right.ToString();

            Instance.txtBootMap.Text = Data.MyMap.BootMap.ToString();
            Instance.txtBootX.Text = Data.MyMap.BootX.ToString();
            Instance.txtBootY.Text = Data.MyMap.BootY.ToString();

            Instance.lstMapNpc.Items.Clear();

            for (x = 0; x < Constant.MAX_MAP_NPCS; x++)
            {
                if (x == 0)
                {
                    Instance.lstMapNpc.Items.Add("None");
                    continue;
                }

                if (Data.MyMap.Npc[x] >= 0 && Data.MyMap.Npc[x] <= Core.Constant.MAX_NPCS)
                {
                    Instance.lstMapNpc.Items.Add(x + ": " + Strings.Trim(Data.Npc[Data.MyMap.Npc[x]].Name));
                }
                else
                {
                    Instance.lstMapNpc.Items.Add(x + ": None");
                }
            }

            Instance.lstMapNpc.SelectedIndex = 0;

            for (y = 0; y < Constant.MAX_NPCS; y++)
                Instance.cmbNpcList.Items.Add(y + 1 + ": " + Strings.Trim(Data.Npc[y].Name));

            Instance.cmbNpcList.SelectedIndex = 0;

            Instance.cmbAnimation.Items.Clear();

            for (y = 0; y < Constant.MAX_ANIMATIONS; y++)
                Instance.cmbAnimation.Items.Add(y + 1 + ": " + Data.Animation[y].Name);

            Instance.cmbAnimation.SelectedIndex = 0;

            Instance.lblMap.Text = "Map: ";
            Instance.txtMaxX.Text = Data.MyMap.MaxX.ToString();
            Instance.txtMaxY.Text = Data.MyMap.MaxY.ToString();

            Instance.cmbWeather.SelectedIndex = Data.MyMap.Weather;
            Instance.scrlFog.Value = Data.MyMap.Fog;
            Instance.lblFogIndex.Text = "Fog: " + Instance.scrlFog.Value;
            Instance.scrlIntensity.Value = Data.MyMap.WeatherIntensity;
            Instance.lblIntensity.Text = "Intensity: " + Instance.scrlIntensity.Value;
            Instance.scrlFogOpacity.Value = Data.MyMap.FogOpacity;
            Instance.scrlFogSpeed.Value = Data.MyMap.FogSpeed;

            Instance.cmbPanorama.Items.Clear();

            var loopTo4 = GameState.NumPanoramas;
            for (i = 0; i < loopTo4; i++)
                Instance.cmbPanorama.Items.Add(i + 1);

            Instance.cmbPanorama.SelectedIndex = Data.MyMap.Panorama;

            Instance.cmbParallax.Items.Clear();

            var loopTo5 = GameState.NumParallax;
            for (i = 0; i < loopTo5; i++)
                Instance.cmbParallax.Items.Add(i + 1);

            Instance.cmbParallax.SelectedIndex = Data.MyMap.Parallax;

            Instance.tabpages.SelectedIndex = 0;
            Instance.scrlMapBrightness.Value = Data.MyMap.Brightness;
            Instance.chkTint.Checked = Data.MyMap.MapTint;
            Instance.scrlMapRed.Value = Data.MyMap.MapTintR;
            Instance.scrlMapGreen.Value = Data.MyMap.MapTintG;
            Instance.scrlMapBlue.Value = Data.MyMap.MapTintB;
            Instance.scrlMapAlpha.Value = Data.MyMap.MapTintA;

            // show the form
            Instance.Visible = true;
        }

        public static void MapEditorInit()
        {
            // set the scrolly bars
            if (Data.MyMap.Tileset < 1 || Data.MyMap.Tileset > GameState.NumTileSets)
                Data.MyMap.Tileset = 1;

            GameState.EditorTileSelStart = new Point(0, 0);
            GameState.EditorTileSelEnd = new Point(1, 1);

            GameState.CurTileset = Data.MyMap.Tileset;

            // set shops for the shop attribute
            for (int i = 0; i < Constant.MAX_SHOPS; i++)
                Instance.cmbShop.Items.Add(i + 1 + ": " + Data.Shop[i].Name);

            // we're not in a shop
            Instance.cmbShop.SelectedIndex = 0;

            Instance.optBlocked.Checked = true;

            Instance.cmbTileSets.Items.Clear();
            for (int i = 0, loopTo = GameState.NumTileSets; i < loopTo; i++)
                Instance.cmbTileSets.Items.Add(i + 1);

            Instance.cmbTileSets.SelectedIndex = 0;
            Instance.cmbAutoTile.SelectedIndex = 0;
            Instance.cmbLayers.SelectedIndex = 0;
            Instance.cmbAttribute.SelectedIndex = 0;

            GameState.CurLayer = 0;
            GameState.CurAutotileType = 0;
            Instance.scrlMapItemValue.Value = 1;
            Instance.scrlTrap.Maximum = 32767;
            Instance.scrlHeal.Maximum = 32767;

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

                if (GameState.CurAutotileType > 0)
                {
                    switch (GameState.CurAutotileType)
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

        public static void MapEditorDrag(int Button, float X, float Y)
        {
            if (Button == (int)MouseButtons.Left) // Left Mouse Button
            {
                // convert the pixel number to tile number
                X = (long)Math.Round(X) / GameState.PicX + 1L;
                Y = (long)Math.Round(Y) / GameState.PicY + 1L;

                // check it's not out of bounds
                if (X < 0f)
                    X = 0f;

                if ((double)X > Instance.picBackSelect.Width / (double)GameState.PicX)
                    X = (float)(Instance.picBackSelect.Width / (double)GameState.PicX);
                if (Y < 0f)
                    Y = 0f;

                if ((double)Y > Instance.picBackSelect.Height / (double)GameState.PicY)
                    Y = (float)(Instance.picBackSelect.Height / (double)GameState.PicY);

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

        public static void MapEditorMouseDown(int x, int y, bool movedMouse = true)
        {
            int i;
            bool isModified = false;

            General.SetWindowFocus(General.Client.Window.Handle);

            if (GameState.CurX < 0 || GameState.CurY < 0 || GameState.CurX >= Data.MyMap.MaxX || GameState.CurY >= Data.MyMap.MaxY)
                return;

            if (!GameLogic.IsInBounds())
                return;

            if (GameState.EyeDropper)
            {
                MapEditorEyeDropper();
                return;
            }

            var withBlock = Data.MyMap.Tile[x, y];

            if (GameClient.IsMouseButtonDown(MouseButton.Left))
            {
                if (Instance.optInfo.Checked)
                {
                    if (GameState.Info == false)
                    {
                        if (GameState.EditorAttribute == 1)
                        {
                            GameLogic.Dialogue("Map Editor", "Info: " + System.Enum.GetName(Data.MyMap.Tile[GameState.CurX, GameState.CurY].Type), " Data 1: " + Data.MyMap.Tile[GameState.CurX, GameState.CurY].Data1 + " Data 2: " + Data.MyMap.Tile[GameState.CurX, GameState.CurY].Data2 + " Data 3: " + Data.MyMap.Tile[GameState.CurX, GameState.CurY].Data3, (byte)DialogueType.Information, (byte)DialogueStyle.Okay);
                        }
                        else
                        {
                            GameLogic.Dialogue("Map Editor", "Info: " + System.Enum.GetName(Data.MyMap.Tile[GameState.CurX, GameState.CurY].Type2), " Data 1: " + Data.MyMap.Tile[GameState.CurX, GameState.CurY].Data1_2 + " Data 2: " + Data.MyMap.Tile[GameState.CurX, GameState.CurY].Data2_2 + " Data 3: " + Data.MyMap.Tile[GameState.CurX, GameState.CurY].Data3_2, (byte)DialogueType.Information, (byte)DialogueStyle.Okay);
                        }
                    }
                }

                if (GameState.MapEditorTab == (int)MapEditorTab.Tiles)
                {
                    if (GameState.EditorTileWidth == 1 & GameState.EditorTileHeight == 1) // single tile
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, GameState.CurLayer, false, (byte)GameState.CurAutotileType);
                    }
                    else if (GameState.CurAutotileType == 0) // multi tile!
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, GameState.CurLayer, true);
                    }
                    else
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, GameState.CurLayer, true, (byte)GameState.CurAutotileType);
                    }
                }
                else if (GameState.MapEditorTab == (int)MapEditorTab.Attributes)
                {
                    ref var withBlock1 = ref Data.MyMap.Tile[GameState.CurX, GameState.CurY];
                    // blocked tile
                    if (Instance.optBlocked.Checked == true)
                    {
                        if (GameState.EditorAttribute == 1)
                        {
                            withBlock1.Type = Core.TileType.Blocked;
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

                    // Npc avoid
                    if (Instance.optNpcAvoid.Checked == true)
                    {
                        if (GameState.EditorAttribute == 1)
                        {
                            withBlock1.Type = TileType.NpcAvoid;
                            withBlock1.Data1 = 0;
                            withBlock1.Data2 = 0;
                            withBlock1.Data3 = 0;
                        }
                        else
                        {
                            withBlock1.Type2 = TileType.NpcAvoid;
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

                    // Npc spawn
                    if (Instance.optNpcSpawn.Checked == true)
                    {
                        if (GameState.EditorAttribute == 1)
                        {
                            withBlock1.Type = TileType.NpcSpawn;
                            withBlock1.Data1 = GameState.SpawnNpcNum;
                            withBlock1.Data2 = GameState.SpawnNpcDir;
                            withBlock1.Data3 = 0;
                        }
                        else
                        {
                            withBlock1.Type2 = TileType.NpcSpawn;
                            withBlock1.Data1_2 = GameState.SpawnNpcNum;
                            withBlock1.Data2_2 = GameState.SpawnNpcDir;
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
                    if (Instance.optNoCrossing.Checked == true)
                    {
                        if (GameState.EditorAttribute == 1)
                        {
                            withBlock1.Type = TileType.NoCrossing;
                            withBlock1.Data1 = 0;
                            withBlock1.Data2 = 0;
                            withBlock1.Data3 = 0;
                        }
                        else
                        {
                            withBlock1.Type2 = TileType.NoCrossing;
                            withBlock1.Data1_2 = 0;
                            withBlock1.Data2_2 = 0;
                            withBlock1.Data3_2 = 0;
                        }
                    }
                }
                else if (GameState.MapEditorTab == (int)MapEditorTab.Directions)
                {
                    // Convert adjusted coordinates to game world coordinates
                    x = (int)Math.Round(GameState.TileView.Left + Math.Floor((GameState.CurMouseX + GameState.Camera.Left) % GameState.PicX));
                    y = (int)Math.Round(GameState.TileView.Top + Math.Floor((GameState.CurMouseY + GameState.Camera.Top) % GameState.PicY));

                    // see if it hits an arrow
                    for (i = 0; i < 4; i++)
                    {
                        // flip the value.
                        if (x >= GameState.DirArrowX[i] & x <= GameState.DirArrowX[i] + 16)
                        {
                            if (y >= GameState.DirArrowY[i] & y <= GameState.DirArrowY[i] + 16)
                            {
                                // flip the value.
                                bool localIsDirBlocked() { byte argdir = (byte)i; var ret = GameLogic.IsDirBlocked(ref Data.MyMap.Tile[GameState.CurX, GameState.CurY].DirBlock, ref argdir); return ret; }

                                byte argdir = (byte)i;
                                GameLogic.SetDirBlock(ref Data.MyMap.Tile[GameState.CurX, GameState.CurY].DirBlock, ref argdir, !localIsDirBlocked());
                                break;
                            }
                        }
                    }
                }
                else if (GameState.MapEditorTab == (int)MapEditorTab.Events)
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
                if (GameState.MapEditorTab == (int)MapEditorTab.Tiles)
                {
                    if (GameState.EditorTileWidth == 1 & GameState.EditorTileHeight == 1) // single tile
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, GameState.CurLayer, false, (byte)GameState.CurAutotileType, 1);
                    }
                    else if (GameState.CurAutotileType == 0) // multi tile!
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, GameState.CurLayer, true, 0, 1);
                    }
                    else
                    {
                        MapEditorSetTile(GameState.CurX, GameState.CurY, GameState.CurLayer, true, (byte)GameState.CurAutotileType, 1);
                    }
                }
                else if (GameState.MapEditorTab == (int)MapEditorTab.Attributes)
                {
                    ref var withBlock2 = ref Data.MyMap.Tile[GameState.CurX, GameState.CurY];
                    // clear attribute
                    withBlock2.Type = 0;
                    withBlock2.Data1 = 0;
                    withBlock2.Data2 = 0;
                    withBlock2.Data3 = 0;
                    withBlock2.Type2 = 0;
                    withBlock2.Data1_2 = 0;
                    withBlock2.Data2_2 = 0;
                    withBlock2.Data3_2 = 0;
                }
                else if (GameState.MapEditorTab == (int)MapEditorTab.Events)
                    Event.DeleteEvent(GameState.CurX, GameState.CurY);
            }

            MapEditorHistory();

            x = 0;

            for (int x2 = 0, loopTo = Data.MyMap.MaxX; x2 < loopTo; x2++)
            {
                for (int y2 = 0, loopTo1 = Data.MyMap.MaxY; y2 < loopTo1; y2++)
                {
                    // Use Layer.Length instead of MapLayer.Count
                    for (int i2 = 0, loopTo2 = Data.MyMap.Tile[x2, y2].Layer != null ? Data.MyMap.Tile[x2, y2].Layer.Length : 0; i2 < loopTo2; i2++)
                    {
                        ref var currentTile = ref Data.MyMap.Tile[x2, y2];
                        ref var historyTile = ref Data.TileHistory[GameState.TileHistoryIndex].Tile[x2, y2];

                        // Check Layer array length for both tiles
                        if (currentTile.Layer == null || currentTile.Layer.Length <= i2 || historyTile.Layer == null || historyTile.Layer.Length <= i2)
                        {
                            continue; // Skip processing if Layer is not properly initialized
                        }

                        // Check if the tile is modified
                        isModified = currentTile.Data1 != historyTile.Data1 ||
                                            currentTile.Data2 != historyTile.Data2 ||
                                            currentTile.Data3 != historyTile.Data3 ||
                                            currentTile.Data1_2 != historyTile.Data1_2 ||
                                            currentTile.Data2_2 != historyTile.Data2_2 ||
                                            currentTile.Data3_2 != historyTile.Data3_2 ||
                                            currentTile.Type != historyTile.Type ||
                                            currentTile.Type2 != historyTile.Type2 ||
                                            currentTile.DirBlock != historyTile.DirBlock ||
                                            currentTile.Layer[i2].X != historyTile.Layer[i2].X ||
                                            currentTile.Layer[i2].Y != historyTile.Layer[i2].Y ||
                                            currentTile.Layer[i2].Tileset != historyTile.Layer[i2].Tileset ||
                                            currentTile.Layer[i2].AutoTile != historyTile.Layer[i2].AutoTile;

                        if (isModified)
                        {
                            historyTile.Data1 = currentTile.Data1;
                            historyTile.Data2 = currentTile.Data2;
                            historyTile.Data3 = currentTile.Data3;
                            historyTile.Data1_2 = currentTile.Data1_2;
                            historyTile.Data2_2 = currentTile.Data2_2;
                            historyTile.Data3_2 = currentTile.Data3_2;
                            historyTile.Type = currentTile.Type;
                            historyTile.Type2 = currentTile.Type2;
                            historyTile.DirBlock = currentTile.DirBlock;
                            historyTile.Layer[i2].X = currentTile.Layer[i2].X;
                            historyTile.Layer[i2].Y = currentTile.Layer[i2].Y;
                            historyTile.Layer[i2].Tileset = currentTile.Layer[i2].Tileset;
                            historyTile.Layer[i2].AutoTile = currentTile.Layer[i2].AutoTile;

                            if (historyTile.Layer[i2].AutoTile > 0)
                            {
                                x = 1;
                            }

                            Autotile.CacheRenderState(x2, y2, i2);
                        }
                    }
                }
            }

            if (GameClient.CurrentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) || GameClient.CurrentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightControl))
            {
                MapEditorReplaceTile((MapLayer)GameState.CurLayer, GameState.CurX, GameState.CurY, withBlock);
            }

            if (x == 1)
            {
                // do a re-init so we can see our changes
                Autotile.InitAutotiles();
            }
        }

        public static void MapEditorCancel()
        {
            ByteStream buffer;

            if (GameState.MyEditorType != (int)EditorType.Map)
                return;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CNeedMap);
            buffer.WriteInt32(1);
            NetworkConfig.Socket?.SendData(buffer.UnreadData, buffer.WritePosition);
            GameState.MyEditorType = -1;
            GameState.GettingMap = true;
            NetworkSend.SendCloseEditor();

            // show gui
            Gui.ShowWindow(Gui.GetWindowIndex("winHotbar"), resetPosition: false);
            Gui.ShowWindow(Gui.GetWindowIndex("winMenu"), resetPosition: false);
            Gui.ShowWindow(Gui.GetWindowIndex("winBars"), resetPosition: false);
            Gui.HideChat();

            frmEditor_Event.Instance?.Dispose();

            GameState.TileHistoryHighIndex = 0;
            GameState.TileHistoryIndex = 0;
        }

        public static void MapEditorSend()
        {
            Map.SendMap();
            GameState.MyEditorType = -1;
            GameState.GettingMap = true;
            NetworkSend.SendCloseEditor();

            // show gui
            Gui.ShowWindow(Gui.GetWindowIndex("winHotbar"), resetPosition: false);
            Gui.ShowWindow(Gui.GetWindowIndex("winMenu"), resetPosition: false);
            Gui.ShowWindow(Gui.GetWindowIndex("winBars"), resetPosition: false);
            Gui.HideChat();

            frmEditor_Event.Instance?.Dispose();

            GameState.TileHistoryHighIndex = 0;
            GameState.TileHistoryIndex = 0;
        }

        public static void MapEditorSetTile(int x, int y, int CurLayer, bool multitile = false, byte theAutotile = 0, byte eraseTile = 0)
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
                ref var withBlock = ref Data.MyMap.Tile[x, y];
                // set layer
                withBlock.Layer[CurLayer].X = newTileX;
                withBlock.Layer[CurLayer].Y = newTileY;
                if (Conversions.ToBoolean(eraseTile))
                {
                    withBlock.Layer[CurLayer].Tileset = 0;
                }
                else
                {
                    withBlock.Layer[CurLayer].Tileset = GameState.CurTileset;
                }
                withBlock.Layer[CurLayer].AutoTile = theAutotile;
                Autotile.CacheRenderState(x, y, CurLayer);

                // do a re-init so we can see our changes
                Autotile.InitAutotiles();
                return;
            }

            if (!multitile) // single
            {
                ref var withBlock1 = ref Data.MyMap.Tile[x, y];
                // set layer
                withBlock1.Layer[CurLayer].X = newTileX;
                withBlock1.Layer[CurLayer].Y = newTileY;
                if (Conversions.ToBoolean(eraseTile))
                {
                    withBlock1.Layer[CurLayer].Tileset = 0;
                }
                else
                {
                    withBlock1.Layer[CurLayer].Tileset = GameState.CurTileset;
                }
                withBlock1.Layer[CurLayer].AutoTile = 0;
                Autotile.CacheRenderState(x, y, CurLayer);
            }
            else // multitile
            {
                y2 = 0; // starting tile for y axis
                var loopTo = GameState.CurY + GameState.EditorTileHeight;
                for (y = GameState.CurY; y < loopTo; y++)
                {
                    x2 = 0; // re-set x count every y loop
                    var loopTo1 = GameState.CurX + GameState.EditorTileWidth;
                    for (x = GameState.CurX; x < loopTo1; x++)
                    {
                        if (x >= 0 & x < Data.MyMap.MaxX)
                        {
                            if (y >= 0 & y < Data.MyMap.MaxY)
                            {
                                ref var withBlock2 = ref Data.MyMap.Tile[x, y];
                                withBlock2.Layer[CurLayer].X = newTileX + x2;
                                withBlock2.Layer[CurLayer].Y = newTileY + y2;
                                if (Conversions.ToBoolean(eraseTile))
                                {
                                    withBlock2.Layer[CurLayer].Tileset = 0;
                                }
                                else
                                {
                                    withBlock2.Layer[CurLayer].Tileset = GameState.CurTileset;
                                }
                                withBlock2.Layer[CurLayer].AutoTile = 0;
                                Autotile.CacheRenderState(x, y, CurLayer);
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
            if (GameState.TileHistoryIndex <= 0)
                GameState.TileHistoryIndex = 0;

            if (GameState.TileHistoryIndex >= GameState.MaxTileHistory - 1)
            {
                for (int i = 0; i < GameState.TileHistoryIndex; i++)
                {
                    Data.TileHistory[(int)i] = Data.TileHistory[(int)(i + 1)];
                }
            }
            else
            {
                GameState.TileHistoryIndex++;
                GameState.TileHistoryHighIndex++;

                if (GameState.TileHistoryHighIndex > GameState.MaxTileHistory)
                    GameState.TileHistoryHighIndex = GameState.MaxTileHistory;

            }

        }

        public static void MapEditorClearLayer(MapLayer layer)
        {
            GameLogic.Dialogue("Map Editor", "Clear Layer: " + layer.ToString(), "Are you sure you wish to clear this layer?", (byte)DialogueType.ClearLayer, (byte)DialogueStyle.YesNo, GameState.CurLayer, GameState.CurAutotileType);
        }

        public static void MapEditorFillLayer(MapLayer layer, byte theAutotile = 0, byte tileX = 0, byte tileY = 0)
        {
            GameLogic.Dialogue("Map Editor", "Fill Layer: " + layer.ToString(), "Are you sure you wish to fill this layer?", (byte)DialogueType.FillLayer, (byte)DialogueStyle.YesNo, GameState.CurLayer, GameState.CurAutotileType, tileX, tileY, Instance.cmbTileSets.SelectedIndex + 1);
        }

        public static void MapEditorEyeDropper()
        {
            int CurLayer;

            CurLayer = GameState.CurLayer;

            {
                ref var withBlock = ref Data.MyMap.Tile[GameState.CurX, GameState.CurY];
                GameState.CurTileset = withBlock.Layer[CurLayer].Tileset;
                MapEditorChooseTile((int)MouseButtons.Left, withBlock.Layer[CurLayer].X * GameState.PicX, withBlock.Layer[CurLayer].Y * GameState.PicY);
                GameState.EyeDropper = !GameState.EyeDropper;
            }
        }

        public static void MapEditorUndo()
        {
            bool isModified = false;

            if (GameState.TileHistoryIndex <= 0)
            {
                General.SetWindowFocus(General.Client.Window.Handle);
                return;
            }

            int layerCount = Enum.GetValues(typeof(MapLayer)).Length;

            for (int x = 0, loopTo = Data.MyMap.MaxX; x < loopTo; x++)
            {
                for (int y = 0, loopTo1 = Data.MyMap.MaxY; y < loopTo1; y++)
                {
                    for (int i = 0; i < layerCount; i++)
                    {
                        ref var currentTile = ref Data.MyMap.Tile[x, y];
                        ref var historyTile = ref Data.TileHistory[GameState.TileHistoryIndex].Tile[x, y];

                        if (currentTile.Layer == null || currentTile.Layer.Length <= i || historyTile.Layer == null || historyTile.Layer.Length <= i)
                        {
                            continue; // Skip processing if Layer is not properly initialized
                        }

                        if (!isModified)
                        {
                            // Check if the tile is modified
                            isModified = currentTile.Data1 != historyTile.Data1 ||
                                                currentTile.Data2 != historyTile.Data2 ||
                                                currentTile.Data3 != historyTile.Data3 ||
                                                currentTile.Data1_2 != historyTile.Data1_2 ||
                                                currentTile.Data2_2 != historyTile.Data2_2 ||
                                                currentTile.Data3_2 != historyTile.Data3_2 ||
                                                currentTile.Type != historyTile.Type ||
                                                currentTile.Type2 != historyTile.Type2 ||
                                                currentTile.DirBlock != historyTile.DirBlock ||
                                                currentTile.Layer[i].X != historyTile.Layer[i].X ||
                                                currentTile.Layer[i].Y != historyTile.Layer[i].Y ||
                                                currentTile.Layer[i].Tileset != historyTile.Layer[i].Tileset ||
                                                currentTile.Layer[i].AutoTile != historyTile.Layer[i].AutoTile;
                        }

                        currentTile.Data1 = historyTile.Data1;
                        currentTile.Data2 = historyTile.Data2;
                        currentTile.Data3 = historyTile.Data3;
                        currentTile.Data1_2 = historyTile.Data1_2;
                        currentTile.Data2_2 = historyTile.Data2_2;
                        currentTile.Data3_2 = historyTile.Data3_2;
                        currentTile.Type = historyTile.Type;
                        currentTile.Type2 = historyTile.Type2;
                        currentTile.DirBlock = historyTile.DirBlock;
                        currentTile.Layer[i].X = historyTile.Layer[i].X;
                        currentTile.Layer[i].Y = historyTile.Layer[i].Y;
                        currentTile.Layer[i].Tileset = historyTile.Layer[i].Tileset;
                        currentTile.Layer[i].AutoTile = historyTile.Layer[i].AutoTile;
                        Autotile.CacheRenderState(x, y, i);

                        if (currentTile.Layer[i].AutoTile > 0)
                        {
                            // do a re-init so we can see our changes
                            Autotile.InitAutotiles();
                        }
                    }
                }
            }

            GameState.TileHistoryIndex -= 1;

            if (isModified)
                General.SetWindowFocus(General.Client.Window.Handle);
            else
                MapEditorUndo();
        }

        public static void MapEditorRedo()
        {
            bool isModified = false;

            if (GameState.TileHistoryIndex > GameState.TileHistoryHighIndex)
            {
                GameState.TileHistoryIndex--;
                General.SetWindowFocus(General.Client.Window.Handle);
                return;
            }

            int layerCount = Enum.GetValues(typeof(MapLayer)).Length;

            for (int x = 0, loopTo = Data.MyMap.MaxX; x < loopTo; x++)
            {
                for (int y = 0, loopTo1 = Data.MyMap.MaxY; y < loopTo1; y++)
                {
                    for (int i = 0; i < layerCount; i++)
                    {
                        ref var currentTile = ref Data.MyMap.Tile[x, y];
                        ref var historyTile = ref Data.TileHistory[GameState.TileHistoryIndex].Tile[x, y];

                        if (currentTile.Layer == null || currentTile.Layer.Length <= i || historyTile.Layer == null || historyTile.Layer.Length <= i)
                        {
                            continue; // Skip processing if Layer is not properly initialized
                        }

                        if (!isModified)
                        {
                            // Check if the tile is modified
                            isModified = currentTile.Data1 != historyTile.Data1 ||
                                                currentTile.Data2 != historyTile.Data2 ||
                                                currentTile.Data3 != historyTile.Data3 ||
                                                currentTile.Data1_2 != historyTile.Data1_2 ||
                                                currentTile.Data2_2 != historyTile.Data2_2 ||
                                                currentTile.Data3_2 != historyTile.Data3_2 ||
                                                currentTile.Type != historyTile.Type ||
                                                currentTile.Type2 != historyTile.Type2 ||
                                                currentTile.DirBlock != historyTile.DirBlock ||
                                                currentTile.Layer[i].X != historyTile.Layer[i].X ||
                                                currentTile.Layer[i].Y != historyTile.Layer[i].Y ||
                                                currentTile.Layer[i].Tileset != historyTile.Layer[i].Tileset ||
                                                currentTile.Layer[i].AutoTile != historyTile.Layer[i].AutoTile;
                        }

                        currentTile.Data1 = historyTile.Data1;
                        currentTile.Data2 = historyTile.Data2;
                        currentTile.Data3 = historyTile.Data3;
                        currentTile.Data1_2 = historyTile.Data1_2;
                        currentTile.Data2_2 = historyTile.Data2_2;
                        currentTile.Data3_2 = historyTile.Data3_2;
                        currentTile.Type = historyTile.Type;
                        currentTile.Type2 = historyTile.Type2;
                        currentTile.DirBlock = historyTile.DirBlock;
                        currentTile.Layer[i].X = historyTile.Layer[i].X;
                        currentTile.Layer[i].Y = historyTile.Layer[i].Y;
                        currentTile.Layer[i].Tileset = historyTile.Layer[i].Tileset;
                        currentTile.Layer[i].AutoTile = historyTile.Layer[i].AutoTile;
                        Autotile.CacheRenderState(x, y, i);

                        if (currentTile.Layer[i].AutoTile > 0)
                        {
                            // do a re-init so we can see our changes
                            Autotile.InitAutotiles();
                        }
                    }
                }
            }

            GameState.TileHistoryIndex++;

            if (isModified)
                General.SetWindowFocus(General.Client.Window.Handle);
            else
                MapEditorRedo();
        }

        public void ClearAttributeDialogue()
        {
            fraNpcSpawn.Visible = false;
            fraResource.Visible = false;
            fraMapItem.Visible = false;
            fraMapWarp.Visible = false;
            fraShop.Visible = false;
            fraHeal.Visible = false;
            fraTrap.Visible = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Data.MyMap.Name = txtName.Text;
        }

        private void frmEditor_Map_FormClosing(object sender, FormClosingEventArgs e)
        {
            MapEditorCancel();
        }

        private void scrMapBrightness_Scroll(object sender, ScrollEventArgs e)
        {
            Data.MyMap.Brightness = (byte)scrlMapBrightness.Value;
            lblMapBrightness.Text = "Brightness: " + scrlMapBrightness.Value;
        }

        public static void MapEditorCopyMap()
        {
            int i;
            int x;
            int y;

            // Get the number of layers from the MapLayer enum
            int layerCount = Enum.GetValues(typeof(MapLayer)).Length;

            if (GameState.CopyMap == false)
            {
                Data.TempTile = new Tile[Data.MyMap.MaxX, Data.MyMap.MaxY];
                GameState.TmpMaxX = Data.MyMap.MaxX;
                GameState.TmpMaxY = Data.MyMap.MaxY;

                var loopTo = (int)Data.MyMap.MaxX;
                for (x = 0; x < loopTo; x++)
                {
                    var loopTo1 = (int)Data.MyMap.MaxY;
                    for (y = 0; y < loopTo1; y++)
                    {
                        ref var withBlock = ref Data.MyMap.Tile[x, y];
                        Data.TempTile[x, y].Layer = new Core.Type.Layer[layerCount];

                        Data.TempTile[x, y].Data1 = withBlock.Data1;
                        Data.TempTile[x, y].Data2 = withBlock.Data2;
                        Data.TempTile[x, y].Data3 = withBlock.Data3;
                        Data.TempTile[x, y].Type = withBlock.Type;
                        Data.TempTile[x, y].Data1_2 = withBlock.Data1_2;
                        Data.TempTile[x, y].Data2_2 = withBlock.Data2_2;
                        Data.TempTile[x, y].Data3_2 = withBlock.Data3_2;
                        Data.TempTile[x, y].Type2 = withBlock.Type2;
                        Data.TempTile[x, y].DirBlock = withBlock.DirBlock;

                        for (i = 0; i < layerCount; i++)
                        {
                            Data.TempTile[x, y].Layer[i].X = withBlock.Layer[i].X;
                            Data.TempTile[x, y].Layer[i].Y = withBlock.Layer[i].Y;
                            Data.TempTile[x, y].Layer[i].Tileset = withBlock.Layer[i].Tileset;
                            Data.TempTile[x, y].Layer[i].AutoTile = withBlock.Layer[i].AutoTile;
                        }
                    }
                }

                GameState.CopyMap = true;
                GameLogic.Dialogue("Map Editor", "Map Copy: ", "Press the button again to paste.", (byte)DialogueType.CopyMap, (byte)DialogueStyle.Okay);
            }
            else
            {
                Data.MyMap.MaxX = GameState.TmpMaxX;
                Data.MyMap.MaxY = GameState.TmpMaxY;

                var loopTo2 = (int)Data.MyMap.MaxX;
                for (x = 0; x < loopTo2; x++)
                {
                    var loopTo3 = (int)Data.MyMap.MaxY;
                    for (y = 0; y < loopTo3; y++)
                    {
                        ref var withBlock1 = ref Data.MyMap.Tile[x, y];
                        Array.Resize(ref Data.MyMap.Tile[x, y].Layer, layerCount);
                        Array.Resize(ref Data.Autotile[x, y].Layer, layerCount);

                        withBlock1.Data1 = Data.TempTile[x, y].Data1;
                        withBlock1.Data2 = Data.TempTile[x, y].Data2;
                        withBlock1.Data3 = Data.TempTile[x, y].Data3;
                        withBlock1.Type = Data.TempTile[x, y].Type;
                        withBlock1.Data1_2 = Data.TempTile[x, y].Data1_2;
                        withBlock1.Data2_2 = Data.TempTile[x, y].Data2_2;
                        withBlock1.Data3_2 = Data.TempTile[x, y].Data3_2;
                        withBlock1.Type2 = Data.TempTile[x, y].Type2;
                        withBlock1.DirBlock = Data.TempTile[x, y].DirBlock;

                        for (i = 0; i < layerCount; i++)
                        {
                            withBlock1.Layer[i].X = Data.TempTile[x, y].Layer[i].X;
                            withBlock1.Layer[i].Y = Data.TempTile[x, y].Layer[i].Y;
                            withBlock1.Layer[i].Tileset = Data.TempTile[x, y].Layer[i].Tileset;
                            withBlock1.Layer[i].AutoTile = Data.TempTile[x, y].Layer[i].AutoTile;
                            Autotile.CacheRenderState(x, y, i);
                        }
                    }
                }

                GameLogic.Dialogue("Map Editor", "Map Paste: ", "Map has been updated.", (byte)DialogueType.PasteMap, (byte)DialogueStyle.Okay);

                // do a re-init so we can see our changes
                Autotile.InitAutotiles();

                GameState.CopyMap = false;
            }
        }

        private void tsbCopyMap_Click(object sender, EventArgs e)
        {
            MapEditorCopyMap();
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

        private void tsbScreenshot_Click(object sender, EventArgs e)
        {
            GameClient.TakeScreenshot();
        }

        private void tsbTileset_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < Data.MyMap.MaxY; y++)
            {
                for (int x = 0; x < Data.MyMap.MaxX; x++)
                {
                    for (int i = 0; i < Data.MyMap.Tile[x, y].Layer.Length; i++)
                    {
                        ref var tile = ref Data.MyMap.Tile[x, y];

                        if (tile.Layer[i].Tileset == 0)
                            continue;

                        tile.Layer[i].Tileset++;

                        Autotile.CacheRenderState(x, y, i);
                    }
                }
            }
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

        private void chkRespawn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoMapRespawn.Checked)
            {
                Data.MyMap.NoRespawn = true;
            }
            else
            {
                Data.MyMap.NoRespawn = false;
            }
        }

        private void chkIndoors_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndoors.Checked)
            {
                Data.MyMap.Indoors = true;
            }
            else
            {
                Data.MyMap.Indoors = false;
            }
        }

        private void cmbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameState.EditorAttribute = (byte)(cmbAttribute.SelectedIndex);
        }

        private void tsbDeleteMap_Click(object sender, EventArgs e)
        {
            GameLogic.Dialogue("Map Editor", "Clear Map: ", "Are you sure you want to clear this map?", (byte)DialogueType.ClearMap, (byte)DialogueStyle.YesNo);
        }

        private void picBackSelect_Paint(object sender, PaintEventArgs e)
        {
            DrawTileset();
        }

        private void btnFillAttributes_Click(object sender, EventArgs e)
        {
            GameLogic.Dialogue("Map Editor", "Fill Attributes: ", "Are you sure you wish to fill attributes?", (byte)DialogueType.FillAttributes, (byte)DialogueStyle.YesNo);
        }

        private void ToolStrip_MouseHover(object sender, EventArgs e)
        {
            Activate();
        }

        private void tabpages_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameState.MapEditorTab = Instance.tabpages.SelectedIndex;

            if (GameState.MapEditorTab == (int)MapEditorTab.Attributes)
            {
                cmbAttribute.SelectedIndex = 1;
            }
        }

        private void cmbTileSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameState.CurTileset = cmbTileSets.SelectedIndex + 1;
        }

        private void cmbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameState.CurLayer = cmbLayers.SelectedIndex;
        }

        /// <summary>
        /// Replaces the X/Y coordinates of all tiles in the given layer with the specified values.
        /// </summary>
        /// <param name="layer">The layer to update.</param>
        /// <param name="tileX">The new X coordinate to set.</param>
        /// <param name="tileY">The new Y coordinate to set.</param>
        public static void MapEditorReplaceTile(MapLayer layer, int tileX, int tileY, Core.Type.Tile oldTile)
        {
            int maxX = Data.MyMap.MaxX;
            int maxY = Data.MyMap.MaxY;

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    ref var tile = ref Data.MyMap.Tile[x, y];
                    if ((int)MapEditorTab.Tiles == GameState.MapEditorTab)
                    {
                        if (tile.Layer[(int)layer].X == oldTile.Layer[(int)layer].X && tile.Layer[(int)layer].Y == oldTile.Layer[(int)layer].Y)
                        {
                            if (GameClient.IsMouseButtonDown(MouseButton.Left))
                            {
                                tile.Layer[(int)layer].X = Data.MyMap.Tile[tileX, tileY].Layer[(int)layer].X;
                                tile.Layer[(int)layer].Y = Data.MyMap.Tile[tileX, tileY].Layer[(int)layer].Y;
                                tile.Layer[(int)layer].Tileset = Data.MyMap.Tile[tileX, tileY].Layer[(int)layer].Tileset;
                            }
                            else if (GameClient.IsMouseButtonDown(MouseButton.Right))
                            {
                                tile.Layer[(int)layer].X = 0;
                                tile.Layer[(int)layer].Y = 0;
                                tile.Layer[(int)layer].Tileset = 0;
                            }
                            else
                            {
                                return; // No mouse button pressed, exit early
                            }

                            tile.Layer[(int)layer].AutoTile = 0;
                            Autotile.CacheRenderState(x, y, (int)layer);
                        }
                    }
                    else if ((int)MapEditorTab.Attributes == GameState.MapEditorTab)
                    {
                        if (GameClient.IsMouseButtonDown(MouseButton.Left))
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                tile.Data1 = Data.MyMap.Tile[tileX, tileY].Data1;
                                tile.Data2 = Data.MyMap.Tile[tileX, tileY].Data2;
                                tile.Data3 = Data.MyMap.Tile[tileX, tileY].Data3;
                                tile.Type = Data.MyMap.Tile[tileX, tileY].Type;
                            }
                            else
                            {
                                tile.Data1_2 = Data.MyMap.Tile[tileX, tileY].Data1_2;
                                tile.Data2_2 = Data.MyMap.Tile[tileX, tileY].Data2_2;
                                tile.Data3_2 = Data.MyMap.Tile[tileX, tileY].Data3_2;
                                tile.Type2 = Data.MyMap.Tile[tileX, tileY].Type2;
                            }
                        }

                        if (GameClient.IsMouseButtonDown(MouseButton.Right))
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.Type = 0;
                            }
                            else
                            {
                                tile.Data1_2 = 0;
                                tile.Data2_2 = 0;
                                tile.Data3_2 = 0;
                                tile.Type2 = 0;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}