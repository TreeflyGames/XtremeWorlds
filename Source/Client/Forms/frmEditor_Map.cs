using System.Windows.Forms;
using Assimp.Configs;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using Mirage.Sharp.Asfw;
using MonoGame.Extended.Content.Tiled;
using static Core.Enum;
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
            GameState.DirArrowX[(int)DirectionType.Up] = 12;
            GameState.DirArrowY[(int)DirectionType.Up] = 0;
            GameState.DirArrowX[(int)DirectionType.Down] = 12;
            GameState.DirArrowY[(int)DirectionType.Down] = 23;
            GameState.DirArrowX[(int)DirectionType.Left] = 0;
            GameState.DirArrowY[(int)DirectionType.Left] = 12;
            GameState.DirArrowX[(int)DirectionType.Right] = 23;
            GameState.DirArrowY[(int)DirectionType.Right] = 12;
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

            itemNum = Core.Type.Item[scrlMapItem.Value].Icon;

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
            Core.Type.TileStruct[,] tempArr;

            if (!Information.IsNumeric(Instance.txtMaxX.Text))
                Instance.txtMaxX.Text = Core.Type.MyMap.MaxX.ToString();

            if (Conversion.Val(Instance.txtMaxX.Text) < SettingsManager.Instance.CameraWidth)

                Instance.txtMaxX.Text = SettingsManager.Instance.CameraWidth.ToString();
            if (Conversion.Val(Instance.txtMaxX.Text) > byte.MaxValue)

                Instance.txtMaxX.Text = byte.MaxValue.ToString();
            if (!Information.IsNumeric(Instance.txtMaxY.Text))
                Instance.txtMaxY.Text = Core.Type.MyMap.MaxY.ToString();

            if (Conversion.Val(Instance.txtMaxY.Text) < SettingsManager.Instance.CameraHeight)
                Instance.txtMaxY.Text = SettingsManager.Instance.CameraHeight.ToString();

            if (Conversion.Val(Instance.txtMaxY.Text) > byte.MaxValue)
                Instance.txtMaxY.Text = byte.MaxValue.ToString();

            {
                ref var withBlock = ref Core.Type.MyMap;
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
                tempArr = (Core.Type.TileStruct[,])withBlock.Tile.Clone();

                x2 = withBlock.MaxX;
                y2 = withBlock.MaxY;

                // change the data  
                withBlock.MaxX = (byte)Math.Round(Conversion.Val(Instance.txtMaxX.Text));
                withBlock.MaxY = (byte)Math.Round(Conversion.Val(Instance.txtMaxY.Text));

                withBlock.Tile = new Core.Type.TileStruct[(withBlock.MaxX), (withBlock.MaxY)];

                for (int i = 0; i < GameState.MaxTileHistory; i++)
                    Core.Type.TileHistory[i].Tile = new Core.Type.TileStruct[(withBlock.MaxX), (withBlock.MaxY)];

                Core.Type.Autotile = new Core.Type.AutotileStruct[(withBlock.MaxX), (withBlock.MaxY)];

                if (x2 > withBlock.MaxX)
                    x2 = withBlock.MaxX;

                if (y2 > withBlock.MaxY)
                    y2 = withBlock.MaxY;

                var loopTo = (int)withBlock.MaxX;
                for (int x = 0; x < loopTo; x++)
                {
                    var loopTo1 = (int)withBlock.MaxY;
                    for (int y = 0; y < loopTo1; y++)
                    {
                        withBlock.Tile[x, y].Layer = new Core.Type.TileDataStruct[(int)Core.Enum.LayerType.Count];
                        Core.Type.Autotile[x, y].Layer = new Core.Type.QuarterTileStruct[(int)Core.Enum.LayerType.Count];

                        for (int i = 0; i < GameState.MaxTileHistory; i++)
                            Core.Type.TileHistory[i].Tile[x, y].Layer = new Core.Type.TileDataStruct[(int)Core.Enum.LayerType.Count];

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

            Core.Type.MyMap.Tileset = GameState.CurTileset;

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
            lblMapItem.Text = (scrlMapItem.Value + 1) + ". " + Core.Type.Item[scrlMapItem.Value].Name + " x" + scrlMapItemValue.Value;
        }

        private void ScrlMapItemValue_ValueChanged(object sender, EventArgs e)
        {
            lblMapItem.Text = (scrlMapItem.Value + 1) + ". " + Core.Type.Item[scrlMapItem.Value].Name + " x" + scrlMapItemValue.Value;
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

            lblMapItem.Text = Core.Type.Item[scrlMapItem.Value].Name + " x" + scrlMapItemValue.Value;
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
            lblResource.Text = "Resource: " + Core.Type.Resource[scrlResource.Value].Name;
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

            for (n = 0; n < Constant.MAX_MAP_NPCS; n++)
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
            if (lstMapNPC.SelectedIndex >= 0)
            {
                lstMapNPC.Items[lstMapNPC.SelectedIndex] = lstMapNPC.SelectedIndex + 1 + ": " + Core.Type.NPC[cmbNPCList.SelectedIndex].Name;
                Core.Type.MyMap.NPC[lstMapNPC.SelectedIndex] = cmbNPCList.SelectedIndex;
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

        public static void MapPropertiesInit()
        {
            int x;
            int y;
            int i;

            Instance.txtName.Text = Strings.Trim(Core.Type.MyMap.Name);

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
                if ((Instance.lstMusic.Items[i].ToString() ?? "") == (Core.Type.MyMap.Music ?? ""))
                {
                    Instance.lstMusic.SelectedIndex = i;
                    break;
                }
            }

            // find the shop we have set
            Instance.lstShop.Items.Clear();

            for (i = 0; i < Constant.MAX_SHOPS; i++)
                Instance.lstShop.Items.Add(Core.Type.Shop[i].Name);

            Instance.lstShop.SelectedIndex = 0;

            var loopTo2 = Instance.lstShop.Items.Count;
            for (i = 0; i < loopTo2; i++)
            {
                if ((Instance.lstShop.Items[i].ToString() ?? "") == (Core.Type.Shop[Core.Type.MyMap.Shop].Name ?? ""))
                {
                    Instance.lstShop.SelectedIndex = i;
                    break;
                }
            }

            // find the shop we have set
            Instance.lstMoral.Items.Clear();

            for (i = 0; i < Constant.MAX_MORALS; i++)
                Instance.lstMoral.Items.Add(Core.Type.Moral[i].Name);

            Instance.lstMoral.SelectedIndex = 0;

            var loopTo3 = Instance.lstMoral.Items.Count;
            for (i = 0; i < loopTo3; i++)
            {
                if ((Instance.lstMoral.Items[i].ToString() ?? "") == (Core.Type.Moral[Core.Type.MyMap.Moral].Name ?? ""))
                {
                    Instance.lstMoral.SelectedIndex = i;
                    break;
                }
            }

            Instance.chkTint.Checked = Core.Type.MyMap.MapTint;
            Instance.chkNoMapRespawn.Checked = Core.Type.MyMap.NoRespawn;
            Instance.chkIndoors.Checked = Core.Type.MyMap.Indoors;

            // rest of it
            Instance.txtUp.Text = Core.Type.MyMap.Up.ToString();
            Instance.txtDown.Text = Core.Type.MyMap.Down.ToString();
            Instance.txtLeft.Text = Core.Type.MyMap.Left.ToString();
            Instance.txtRight.Text = Core.Type.MyMap.Right.ToString();

            Instance.txtBootMap.Text = Core.Type.MyMap.BootMap.ToString();
            Instance.txtBootX.Text = Core.Type.MyMap.BootX.ToString();
            Instance.txtBootY.Text = Core.Type.MyMap.BootY.ToString();

            Instance.lstMapNPC.Items.Clear();

            for (x = 0; x < Constant.MAX_MAP_NPCS; x++)
            {
                if (Core.Type.MyMap.NPC[x] >= 0 && Core.Type.MyMap.NPC[x] <= Core.Constant.MAX_NPCS)
                {
                    Instance.lstMapNPC.Items.Add(x + 1 + ": " + Strings.Trim(Core.Type.NPC[Core.Type.MyMap.NPC[x]].Name));
                }
                else
                {
                    Instance.lstMapNPC.Items.Add(x + 1 + ": None");
                }
            }

            Instance.lstMapNPC.SelectedIndex = 0;

            for (y = 0; y < Constant.MAX_NPCS; y++)
                Instance.cmbNPCList.Items.Add(y + 1 + ": " + Strings.Trim(Core.Type.NPC[y].Name));

            Instance.cmbNPCList.SelectedIndex = 0;

            Instance.cmbAnimation.Items.Clear();

            for (y = 0; y < Constant.MAX_ANIMATIONS; y++)
                Instance.cmbAnimation.Items.Add(y + 1 + ": " + Core.Type.Animation[y].Name);

            Instance.cmbAnimation.SelectedIndex = 0;

            Instance.lblMap.Text = "Map: ";
            Instance.txtMaxX.Text = Core.Type.MyMap.MaxX.ToString();
            Instance.txtMaxY.Text = Core.Type.MyMap.MaxY.ToString();

            Instance.cmbWeather.SelectedIndex = Core.Type.MyMap.Weather;
            Instance.scrlFog.Value = Core.Type.MyMap.Fog;
            Instance.lblFogIndex.Text = "Fog: " + Instance.scrlFog.Value;
            Instance.scrlIntensity.Value = Core.Type.MyMap.WeatherIntensity;
            Instance.lblIntensity.Text = "Intensity: " + Instance.scrlIntensity.Value;
            Instance.scrlFogOpacity.Value = Core.Type.MyMap.FogOpacity;
            Instance.scrlFogSpeed.Value = Core.Type.MyMap.FogSpeed;

            Instance.cmbPanorama.Items.Clear();

            var loopTo4 = GameState.NumPanoramas;
            for (i = 0; i < loopTo4; i++)
                Instance.cmbPanorama.Items.Add(i + 1);

            Instance.cmbPanorama.SelectedIndex = Core.Type.MyMap.Panorama;

            Instance.cmbParallax.Items.Clear();

            var loopTo5 = GameState.NumParallax;
            for (i = 0; i < loopTo5; i++)
                Instance.cmbParallax.Items.Add(i + 1);

            Instance.cmbParallax.SelectedIndex = Core.Type.MyMap.Parallax;

            Instance.tabpages.SelectedIndex = 0;
            Instance.scrlMapBrightness.Value = Core.Type.MyMap.Brightness;
            Instance.chkTint.Checked = Core.Type.MyMap.MapTint;
            Instance.scrlMapRed.Value = Core.Type.MyMap.MapTintR;
            Instance.scrlMapGreen.Value = Core.Type.MyMap.MapTintG;
            Instance.scrlMapBlue.Value = Core.Type.MyMap.MapTintB;
            Instance.scrlMapAlpha.Value = Core.Type.MyMap.MapTintA;

            // show the form
            Instance.Visible = true;
        }

        public static void MapEditorInit()
        {
            // set the scrolly bars
            if (Core.Type.MyMap.Tileset < 1 || Core.Type.MyMap.Tileset > GameState.NumTileSets)
                Core.Type.MyMap.Tileset = 1;

            GameState.EditorTileSelStart = new Point(0, 0);
            GameState.EditorTileSelEnd = new Point(1, 1);

            GameState.CurTileset = Core.Type.MyMap.Tileset;

            // set shops for the shop attribute
            for (int i = 0; i < Constant.MAX_SHOPS; i++)
                Instance.cmbShop.Items.Add(i + 1 + ": " + Core.Type.Shop[i].Name);

            // we're not in a shop
            Instance.cmbShop.SelectedIndex = 0;
            Instance.cmbAttribute.SelectedIndex = 0;

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

            if (GameState.CurX < 0 || GameState.CurY < 0 || GameState.CurX >= Core.Type.MyMap.MaxX || GameState.CurY >= Core.Type.MyMap.MaxY)
                return;

            if (!GameLogic.IsInBounds())
                return;

            if (GameState.EyeDropper)
            {
                MapEditorEyeDropper();
                return;
            }

            var withBlock = Core.Type.MyMap.Tile[x, y];

            if (GameClient.IsMouseButtonDown(MouseButton.Left))
            {
                if (Instance.optInfo.Checked)
                {
                    if (GameState.Info == false)
                    {                     
                        if (Instance.cmbAttribute.SelectedIndex == 1)
                        {
                            GameLogic.Dialogue("Map Editor", "Info: " + System.Enum.GetName(Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Type), " Data 1: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data1 + " Data 2: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data2 + " Data 3: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data3, (byte)DialogueType.Info, (byte)DialogueStyle.Okay);
                        }
                        else
                        {
                            GameLogic.Dialogue("Map Editor", "Info: " + System.Enum.GetName(Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Type2), " Data 1: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data1_2 + " Data 2: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data2_2 + " Data 3: " + Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].Data3_2, (byte)DialogueType.Info, (byte)DialogueStyle.Okay);
                        }                   
                    }
                }

                if (GameState.MapTab == (int)MapTab.Tiles)
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
                else if (GameState.MapTab == (int)MapTab.Attributes)
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
                else if (GameState.MapTab == (int)MapTab.Directions)
                {
                    // Convert adjusted coordinates to game world coordinates
                    x= (int)Math.Round(GameState.TileView.Left + Math.Floor((GameState.CurMouseX + GameState.Camera.Left) % GameState.PicX));
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
                                bool localIsDirBlocked() { byte argdir = (byte)i; var ret = GameLogic.IsDirBlocked(ref Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].DirBlock, ref argdir); return ret; }

                                byte argdir = (byte)i;
                                GameLogic.SetDirBlock(ref Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY].DirBlock, ref argdir, !localIsDirBlocked());
                                break;
                            }
                        }
                    }
                }
                else if (GameState.MapTab == (int)MapTab.Events)
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
                if (GameState.MapTab == (int)MapTab.Tiles)
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
                else if (GameState.MapTab == (int)MapTab.Attributes)
                {
                    ref var withBlock2 = ref Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY];
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
                else if (GameState.MapTab == (int)MapTab.Events)
                    Event.DeleteEvent(GameState.CurX, GameState.CurY);
            }

            MapEditorHistory();

            x = 0;

            for (int x2 = 0, loopTo = Core.Type.MyMap.MaxX; x2 < loopTo; x2++)
            {
                for (int y2 = 0, loopTo1 = Core.Type.MyMap.MaxY; y2 < loopTo1; y2++)
                {
                    for (int i2 = 0, loopTo2 = (int)Core.Enum.LayerType.Count; i2 < loopTo2; i2++)
                    {
                        ref var currentTile = ref Core.Type.MyMap.Tile[x2, y2];
                        ref var historyTile = ref Core.Type.TileHistory[GameState.TileHistoryIndex].Tile[x2, y2];

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
                MapEditorReplaceTile((LayerType)GameState.CurLayer, GameState.CurX, GameState.CurY, withBlock);
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
                ref var withBlock = ref Core.Type.MyMap.Tile[x, y];
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
                ref var withBlock1 = ref Core.Type.MyMap.Tile[x, y];
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
                        if (x >= 0 & x < Core.Type.MyMap.MaxX)
                        {
                            if (y >= 0 & y < Core.Type.MyMap.MaxY)
                            {
                                ref var withBlock2 = ref Core.Type.MyMap.Tile[x, y];
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
                    Core.Type.TileHistory[(int)i] = Core.Type.TileHistory[(int)(i + 1)];
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

        public static void MapEditorClearLayer(LayerType layer)
        {
            GameLogic.Dialogue("Map Editor", "Clear Layer: " + layer.ToString(), "Are you sure you wish to clear this layer?", (byte)DialogueType.ClearLayer, (byte)DialogueStyle.YesNo, GameState.CurLayer, GameState.CurAutotileType);
        }

        public static void MapEditorFillLayer(LayerType layer, byte theAutotile = 0, byte tileX = 0, byte tileY = 0)
        {
            GameLogic.Dialogue("Map Editor", "Fill Layer: " + layer.ToString(), "Are you sure you wish to fill this layer?", (byte)DialogueType.FillLayer, (byte)DialogueStyle.YesNo, GameState.CurLayer, GameState.CurAutotileType, tileX, tileY, Instance.cmbTileSets.SelectedIndex + 1);
        }

        public static void MapEditorEyeDropper()
        {
            int CurLayer;

            CurLayer = GameState.CurLayer;

            {
                ref var withBlock = ref Core.Type.MyMap.Tile[GameState.CurX, GameState.CurY];
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

            for (int x = 0, loopTo = Core.Type.MyMap.MaxX; x < loopTo; x++)
            {
                for (int y = 0, loopTo1 = Core.Type.MyMap.MaxY; y < loopTo1; y++)
                {
                    for (int i = 0; i < (int)LayerType.Count; i++)
                    {
                        ref var currentTile = ref Core.Type.MyMap.Tile[x, y];
                        ref var historyTile = ref Core.Type.TileHistory[GameState.TileHistoryIndex].Tile[x, y];

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

            for (int x = 0, loopTo = Core.Type.MyMap.MaxX; x < loopTo; x++)
            {
                for (int y = 0, loopTo1 = Core.Type.MyMap.MaxY; y < loopTo1; y++)
                {
                    for (int i = 0; i < (int)LayerType.Count; i++)
                    {
                        ref var currentTile = ref Core.Type.MyMap.Tile[x, y];
                        ref var historyTile = ref Core.Type.TileHistory[GameState.TileHistoryIndex].Tile[x, y];

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

        public static void MapEditorCopyMap()
        {
            int i;
            int x;
            int y;

            if (GameState.CopyMap == false)
            {
                Core.Type.TempTile = new Core.Type.TileStruct[Core.Type.MyMap.MaxX, Core.Type.MyMap.MaxY];
                GameState.TmpMaxX = Core.Type.MyMap.MaxX;
                GameState.TmpMaxY = Core.Type.MyMap.MaxY;

                var loopTo = (int)Core.Type.MyMap.MaxX;
                for (x = 0; x < loopTo; x++)
                {
                    var loopTo1 = (int)Core.Type.MyMap.MaxY;
                    for (y = 0; y < loopTo1; y++)
                    {
                        ref var withBlock = ref Core.Type.MyMap.Tile[x, y];
                        Core.Type.TempTile[x, y].Layer = new Core.Type.TileDataStruct[(int)Core.Enum.LayerType.Count];

                        Core.Type.TempTile[x, y].Data1 = withBlock.Data1;
                        Core.Type.TempTile[x, y].Data2 = withBlock.Data2;
                        Core.Type.TempTile[x, y].Data3 = withBlock.Data3;
                        Core.Type.TempTile[x, y].Type = withBlock.Type;
                        Core.Type.TempTile[x, y].Data1_2 = withBlock.Data1_2;
                        Core.Type.TempTile[x, y].Data2_2 = withBlock.Data2_2;
                        Core.Type.TempTile[x, y].Data3_2 = withBlock.Data3_2;
                        Core.Type.TempTile[x, y].Type2 = withBlock.Type2;
                        Core.Type.TempTile[x, y].DirBlock = withBlock.DirBlock;

                        for (i = 0; i < (int)LayerType.Count; i++)
                        {
                            Core.Type.TempTile[x, y].Layer[i].X = withBlock.Layer[i].X;
                            Core.Type.TempTile[x, y].Layer[i].Y = withBlock.Layer[i].Y;
                            Core.Type.TempTile[x, y].Layer[i].Tileset = withBlock.Layer[i].Tileset;
                            Core.Type.TempTile[x, y].Layer[i].AutoTile = withBlock.Layer[i].AutoTile;
                        }
                    }
                }

                GameState.CopyMap = true;
                GameLogic.Dialogue("Map Editor", "Map Copy: ", "Press the button again to paste.", (byte)DialogueType.CopyMap, (byte)DialogueStyle.Okay);
            }
            else
            {
                Core.Type.MyMap.MaxX = GameState.TmpMaxX;
                Core.Type.MyMap.MaxY = GameState.TmpMaxY;

                var loopTo2 = (int)Core.Type.MyMap.MaxX;
                for (x = 0; x < loopTo2; x++)
                {
                    var loopTo3 = (int)Core.Type.MyMap.MaxY;
                    for (y = 0; y < loopTo3; y++)
                    {
                        ref var withBlock1 = ref Core.Type.MyMap.Tile[x, y];
                        Array.Resize(ref Core.Type.MyMap.Tile[x, y].Layer, (int)Core.Enum.LayerType.Count);
                        Array.Resize(ref Core.Type.Autotile[x, y].Layer, (int)Core.Enum.LayerType.Count);

                        withBlock1.Data1 = Core.Type.TempTile[x, y].Data1;
                        withBlock1.Data2 = Core.Type.TempTile[x, y].Data2;
                        withBlock1.Data3 = Core.Type.TempTile[x, y].Data3;
                        withBlock1.Type = Core.Type.TempTile[x, y].Type;
                        withBlock1.Data1_2 = Core.Type.TempTile[x, y].Data1_2;
                        withBlock1.Data2_2 = Core.Type.TempTile[x, y].Data2_2;
                        withBlock1.Data3_2 = Core.Type.TempTile[x, y].Data3_2;
                        withBlock1.Type2 = Core.Type.TempTile[x, y].Type2;
                        withBlock1.DirBlock = Core.Type.TempTile[x, y].DirBlock;

                        for (i = 0; i < (int)LayerType.Count; i++)
                        {
                            withBlock1.Layer[i].X = Core.Type.TempTile[x, y].Layer[i].X;
                            withBlock1.Layer[i].Y = Core.Type.TempTile[x, y].Layer[i].Y;
                            withBlock1.Layer[i].Tileset = Core.Type.TempTile[x, y].Layer[i].Tileset;
                            withBlock1.Layer[i].AutoTile = Core.Type.TempTile[x, y].Layer[i].AutoTile;
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
                Core.Type.MyMap.NoRespawn = true;
            }
            else
            {
                Core.Type.MyMap.NoRespawn = false;
            }
        }

        private void chkIndoors_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndoors.Checked)
            {
                Core.Type.MyMap.Indoors = true;
            }
            else
            {
                Core.Type.MyMap.Indoors = false;
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
            GameState.MapTab = Instance.tabpages.SelectedIndex;
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
        public static void MapEditorReplaceTile(LayerType layer, int tileX, int tileY, Core.Type.TileStruct oldTile)
        {
            int maxX = Core.Type.MyMap.MaxX;
            int maxY = Core.Type.MyMap.MaxY;

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    ref var tile = ref Core.Type.MyMap.Tile[x, y];
                    if ((int)MapTab.Tiles == GameState.MapTab)
                    {
                        if (tile.Layer[(int)layer].X == oldTile.Layer[(int)layer].X && tile.Layer[(int)layer].Y == oldTile.Layer[(int)layer].Y)
                        {
                            if (GameClient.IsMouseButtonDown(MouseButton.Left))
                            {
                                tile.Layer[(int)layer].X = Core.Type.MyMap.Tile[tileX, tileY].Layer[(int)layer].X;
                                tile.Layer[(int)layer].Y = Core.Type.MyMap.Tile[tileX, tileY].Layer[(int)layer].Y;
                                tile.Layer[(int)layer].Tileset = Core.Type.MyMap.Tile[tileX, tileY].Layer[(int)layer].Tileset;
                            }
                            else if (GameClient.IsMouseButtonDown(MouseButton.Right))
                            {
                                tile.Layer[(int)layer].X = 0;
                                tile.Layer[(int)layer].Y = 0;
                                tile.Layer[(int)layer].Tileset = 0;
                            }
                            else
                            {
                                continue;
                            }

                            tile.Layer[(int)layer].AutoTile = 0;
                            Autotile.CacheRenderState(x, y, (int)layer);
                        }                      
                    }
                    else if ((int)MapTab.Attributes == GameState.MapTab)
                    {
                        if (GameClient.IsMouseButtonDown(MouseButton.Left))
                        {
                            if (GameState.EditorAttribute == 1)
                            {
                                tile.Data1 = Core.Type.MyMap.Tile[tileX, tileY].Data1;
                                tile.Data2 = Core.Type.MyMap.Tile[tileX, tileY].Data2;
                                tile.Data3 = Core.Type.MyMap.Tile[tileX, tileY].Data3;
                                tile.Type = Core.Type.MyMap.Tile[tileX, tileY].Type;
                            }
                            else
                            {
                                tile.Data1_2 = Core.Type.MyMap.Tile[tileX, tileY].Data1_2;
                                tile.Data2_2 = Core.Type.MyMap.Tile[tileX, tileY].Data2_2;
                                tile.Data3_2 = Core.Type.MyMap.Tile[tileX, tileY].Data3_2;
                                tile.Type2 = Core.Type.MyMap.Tile[tileX, tileY].Type2;
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