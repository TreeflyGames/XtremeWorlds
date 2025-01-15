using System;
using System.Drawing;
using Core;
using static Core.Global.Command;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using Mirage.Sharp.Asfw;
using Mirage.Sharp.Asfw.IO;
using static Core.Enum;

namespace Client
{

    static class Map
    {
        #region Drawing
        public static void DrawThunderEffect()
        {
            if (GameState.DrawThunder > 0)
            {
                // Create a temporary texture matching the camera size
                using (var thunderTexture = new Texture2D(GameClient.Graphics.GraphicsDevice, GameState.ResolutionWidth, GameState.ResolutionHeight))
                {
                    // Create an array to store pixel data
                    var whitePixels = new Microsoft.Xna.Framework.Color[(GameState.ResolutionWidth * GameState.ResolutionHeight)];

                    // Fill the pixel array with semi-transparent white pixels
                    for (int i = 0, loopTo = whitePixels.Length; i < loopTo; i++)
                        whitePixels[i] = new Microsoft.Xna.Framework.Color(255, 255, 255, 150); // White with 150 alpha

                    // Set the pixel data for the texture
                    thunderTexture.SetData(whitePixels);

                    // Begin SpriteBatch to render the thunder effect
                    GameClient.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                    GameClient.SpriteBatch.Draw(thunderTexture, new Microsoft.Xna.Framework.Rectangle(0, 0, GameState.ResolutionWidth, GameState.ResolutionHeight), Microsoft.Xna.Framework.Color.White);
                    GameClient.SpriteBatch.End();
                }

                // Decrease the thunder counter
                GameState.DrawThunder -= 1;
            }
        }

        public static void DrawWeather()
        {
            int i;
            int spriteLeft;

            for (i = 0; i < GameState.MaxWeatherParticles; i++)
            {
                if (Conversions.ToBoolean(GameState.WeatherParticle[i].InUse))
                {
                    if (GameState.WeatherParticle[i].Type == (int)Core.Enum.Weather.Storm)
                    {
                        spriteLeft = 0;
                    }
                    else
                    {
                        spriteLeft = GameState.WeatherParticle[i].Type - 1;
                    }

                    string argpath = System.IO.Path.Combine(Core.Path.Misc, "Weather");
                    GameClient.RenderTexture(ref argpath, GameLogic.ConvertMapX(GameState.WeatherParticle[i].X), GameLogic.ConvertMapY(GameState.WeatherParticle[i].Y), spriteLeft * 32, 0, 32, 32, 32, 32);
                }
            }
        }

        public static void DrawFog()
        {
            int fogNum = GameState.CurrentFog;

            if (fogNum <= 0 | fogNum > GameState.NumFogs)
                return;

            int sX = 0;
            int sY = 0;
            int sW = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Fogs, fogNum.ToString())).Width;  // Using the full width of the fog texture
            int sH = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Fogs, fogNum.ToString())).Height; // Using the full height of the fog texture

            // These should match the scale calculations for full coverage plus extra area
            int dX = (int)Math.Round(GameState.FogOffsetX * 2.5d - 50d);
            int dY = (int)Math.Round(GameState.FogOffsetY * 3.5d - 50d);
            int dW = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Fogs, fogNum.ToString())).Width + 200;
            int dH = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Fogs, fogNum.ToString())).Height + 200;

            string argpath = System.IO.Path.Combine(Core.Path.Fogs, fogNum.ToString());
            GameClient.RenderTexture(ref argpath, dX, dY, sX, sY, dW, dH, sW, sH, (byte)GameState.CurrentFogOpacity);
        }

        public static void DrawMapGroundTile(int x, int y)
        {
            int i;
            byte alpha;
            var rect = new Rectangle(0, 0, 0, 0);

            // Check if the map or its tile data is not ready
            if (GameState.GettingMap || Conversions.ToInteger(GameState.MapData) == 0)
                return;

            // Ensure x and y are within the bounds of the map
            if (x < 0 || y < 0 || x >= Core.Type.MyMap.MaxX || y >= Core.Type.MyMap.MaxY)
                return;

            try
            {
                for (i = (int)Core.Enum.LayerType.Ground; i <= (int)Core.Enum.LayerType.CoverAnim; i++)
                {
                    // Check if this layer has a valid tileset
                    if (Core.Type.MyMap.Tile[x, y].Layer[i].Tileset > 0 && Core.Type.MyMap.Tile[x, y].Layer[i].Tileset <= GameState.NumTileSets)
                    {
                        // Normal rendering state
                        if (Core.Type.Autotile[x, y].Layer[i].RenderState == GameState.RenderStateNormal)
                        {
                            rect.X = Core.Type.MyMap.Tile[x, y].Layer[i].X * GameState.PicX;
                            rect.Y = Core.Type.MyMap.Tile[x, y].Layer[i].Y * GameState.PicY;
                            rect.Width = GameState.PicX;
                            rect.Height = GameState.PicY;

                            alpha = 255;

                            if (GameState.MyEditorType == (int)EditorType.Map)
                            {
                                if (GameState.HideLayers)
                                {
                                    if (i != frmEditor_Map.Instance.cmbLayers.SelectedIndex)
                                    {
                                        alpha = 128;
                                    }
                                }
                            }

                            // Render the tile
                            string argpath = System.IO.Path.Combine(Core.Path.Tilesets, Core.Type.MyMap.Tile[x, y].Layer[i].Tileset.ToString());
                            GameClient.RenderTexture(ref argpath, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY), rect.X, rect.Y, rect.Width, rect.Height, rect.Width, rect.Height, alpha);
                        }

                        // Autotile rendering state
                        else if (Core.Type.Autotile[x, y].Layer[i].RenderState == GameState.RenderStateAutotile)
                        {
                            if (Settings.Instance.Autotile)
                            {
                                DrawAutoTile(i, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY), 1, x, y, 0, false);
                                DrawAutoTile(i, GameLogic.ConvertMapX(x * GameState.PicX) + 16, GameLogic.ConvertMapY(y * GameState.PicY), 2, x, y, 0, false);
                                DrawAutoTile(i, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY) + 16, 3, x, y, 0, false);
                                DrawAutoTile(i, GameLogic.ConvertMapX(x * GameState.PicX) + 16, GameLogic.ConvertMapY(y * GameState.PicY) + 16, 4, x, y, 0, false);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static void DrawMapRoofTile(int x, int y)
        {
            int i;
            int alpha;
            var rect = default(Rectangle);

            // Exit earlyIf Type.Map is still loading or tile data is not available
            if (GameState.GettingMap || Conversions.ToInteger(GameState.MapData) == 0)
                return;

            // Ensure x and y are within valid map bounds
            if (x < 0 || y < 0 || x >= Core.Type.MyMap.MaxX || y >= Core.Type.MyMap.MaxY)
                return;

            try
            {
                // Loop through the layers from Fringe to RoofAnim
                for (i = (int)Core.Enum.LayerType.Fringe; i <= (int)Core.Enum.LayerType.RoofAnim; i++)
                {
                    // Handle animated layers
                    if (GameState.MapAnim == 1)
                    {
                        switch (i)
                        {
                            case (int)Core.Enum.LayerType.Fringe:
                                {
                                    i = (int)Core.Enum.LayerType.Fringe;
                                    break;
                                }
                            case (int)Core.Enum.LayerType.Roof:
                                {
                                    i = (int)Core.Enum.LayerType.Roof;
                                    break;
                                }
                        }
                    }

                    // Ensure the tileset is valid before proceeding
                    if (Core.Type.MyMap.Tile[x, y].Layer[i].Tileset > 0 && Core.Type.MyMap.Tile[x, y].Layer[i].Tileset <= GameState.NumTileSets)
                    {
                        // Check if the render state is normal and render the tile
                        if (Core.Type.Autotile[x, y].Layer[i].RenderState == GameState.RenderStateNormal)
                        {
                            rect.X = Core.Type.MyMap.Tile[x, y].Layer[i].X * GameState.PicX;
                            rect.Y = Core.Type.MyMap.Tile[x, y].Layer[i].Y * GameState.PicY;
                            rect.Width = GameState.PicX;
                            rect.Height = GameState.PicY;

                            alpha = 255;

                            if (GameState.MyEditorType == (int)EditorType.Map)
                            {
                                if (GameState.HideLayers)
                                {
                                    if (i != frmEditor_Map.Instance.cmbLayers.SelectedIndex)
                                    {
                                        alpha = 128;
                                    }
                                }
                            }

                            // Render the tile with the calculated rectangle and transparency
                            string argpath = System.IO.Path.Combine(Core.Path.Tilesets, Core.Type.MyMap.Tile[x, y].Layer[i].Tileset.ToString());
                            GameClient.RenderTexture(ref argpath, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY), rect.X, rect.Y, rect.Width, rect.Height, rect.Width, rect.Height, (byte)alpha);
                        }

                        // Handle autotile rendering
                        else if (Core.Type.Autotile[x, y].Layer[i].RenderState == GameState.RenderStateAutotile)
                        {
                            if (Settings.Instance.Autotile)
                            {
                                // Render autotiles
                                DrawAutoTile(i, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY), 1, x, y, 0, false);
                                DrawAutoTile(i, GameLogic.ConvertMapX(x * GameState.PicX) + 16, GameLogic.ConvertMapY(y * GameState.PicY), 2, x, y, 0, false);
                                DrawAutoTile(i, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY) + 16, 3, x, y, 0, false);
                                DrawAutoTile(i, GameLogic.ConvertMapX(x * GameState.PicX) + 16, GameLogic.ConvertMapY(y * GameState.PicY) + 16, 4, x, y, 0, false);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static void DrawAutoTile(int layerNum, int dX, int dY, int quarterNum, int x, int y, int forceFrame = 0, bool strict = true)
        {
            var yOffset = default(int);
            var xOffset = default(int);

            // calculate the offset
            if (forceFrame > 0)
            {
                switch (forceFrame - 1)
                {
                    case 0:
                        {
                            GameState.WaterfallFrame = 1;
                            break;
                        }
                    case 1:
                        {
                            GameState.WaterfallFrame = 2;
                            break;
                        }
                    case 2:
                        {
                            GameState.WaterfallFrame = 0;
                            break;
                        }
                }

                // animate autotiles
                switch (forceFrame - 1)
                {
                    case 0:
                        {
                            GameState.AutoTileFrame = 1;
                            break;
                        }
                    case 1:
                        {
                            GameState.AutoTileFrame = 2;
                            break;
                        }
                    case 2:
                        {
                            GameState.AutoTileFrame = 0;
                            break;
                        }
                }
            }

            switch (Core.Type.MyMap.Tile[x, y].Layer[layerNum].AutoTile)
            {
                case GameState.AutotileWaterfall:
                    {
                        yOffset = (GameState.WaterfallFrame - 1) * 32;
                        break;
                    }
                case GameState.AutotileAnim:
                    {
                        xOffset = GameState.AutoTileFrame * 64;
                        break;
                    }
                case GameState.AutotileCliff:
                    {
                        yOffset = -32;
                        break;
                    }
            }

            if (Core.Type.MyMap.Tile[x, y].Layer is null)
                return;
            string argpath = System.IO.Path.Combine(Core.Path.Tilesets, Core.Type.MyMap.Tile[x, y].Layer[layerNum].Tileset.ToString());
            GameClient.RenderTexture(ref argpath, dX, dY, Core.Type.Autotile[x, y].Layer[layerNum].SrcX[quarterNum] + xOffset, Core.Type.Autotile[x, y].Layer[layerNum].SrcY[quarterNum] + yOffset, 16, 16, 16, 16);
        }

        public static void DrawMapTint()
        {
            if (Conversions.ToInteger(Core.Type.MyMap.MapTint) == 0)
                return; // Skip if no tint is applied

            // Create a new texture matching the camera size
            var tintTexture = new Texture2D(GameClient.Graphics.GraphicsDevice, GameState.ResolutionWidth, GameState.ResolutionHeight);
            var tintPixels = new Microsoft.Xna.Framework.Color[(GameState.ResolutionWidth * GameState.ResolutionHeight)];

            // Define the tint color with the given RGBA values
            var tintColor = new Microsoft.Xna.Framework.Color(GameState.CurrentTintR, GameState.CurrentTintG, GameState.CurrentTintB, GameState.CurrentTintA);

            // Fill the texture's pixel array with the tint color
            for (int i = 0, loopTo = tintPixels.Length; i < loopTo; i++)
                tintPixels[i] = tintColor;

            // Set the pixel data on the texture
            tintTexture.SetData(tintPixels);

            // Start the sprite batch
            GameClient.SpriteBatch.Begin();

            // Draw the tinted texture over the entire camera view
            GameClient.SpriteBatch.Draw(tintTexture, new Microsoft.Xna.Framework.Rectangle(0, 0, GameState.ResolutionWidth, GameState.ResolutionHeight), Microsoft.Xna.Framework.Color.White);

            GameClient.SpriteBatch.End();

            // Dispose of the temporary texture to free resources
            tintTexture.Dispose();
        }

        public static void DrawMapFade()
        {
            if (!GameState.UseFade)
                return; // Exit if fading is disabled

            // Create a new texture matching the camera view size
            var fadeTexture = new Texture2D(GameClient.Graphics.GraphicsDevice, GameState.ResolutionWidth, GameState.ResolutionHeight);
            var blackPixels = new Microsoft.Xna.Framework.Color[(GameState.ResolutionWidth * GameState.ResolutionHeight)];

            // Fill the pixel array with black color and specified alpha for the fade effect
            for (int i = 0, loopTo = blackPixels.Length; i < loopTo; i++)
                blackPixels[i] = new Microsoft.Xna.Framework.Color(0, 0, 0, GameState.FadeAmount);

            // Set the texture's pixel data
            fadeTexture.SetData(blackPixels);

            // Start the sprite batch
            GameClient.SpriteBatch.Begin();

            // Draw the fade texture over the entire camera view
            GameClient.SpriteBatch.Draw(fadeTexture, new Microsoft.Xna.Framework.Rectangle(0, 0, GameState.ResolutionWidth, GameState.ResolutionHeight), Microsoft.Xna.Framework.Color.White);

            GameClient.SpriteBatch.End();

            // Dispose of the texture to free resources
            fadeTexture.Dispose();
        }

        public static void DrawPanorama(int index)
        {
            if (Core.Type.MyMap.Indoors)
                return;

            if (index < 1 | index > GameState.NumPanoramas)
                return;

            string argpath = System.IO.Path.Combine(Core.Path.Panoramas, index.ToString());
            GameClient.RenderTexture(ref argpath, 0, 0, 0, 0, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Panoramas, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Panoramas, index.ToString())).Height, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Panoramas, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Panoramas, index.ToString())).Height);
        }

        public static void DrawParallax(int index)
        {
            float horz = 0f;
            float vert = 0f;

            if (Core.Type.MyMap.Moral == Conversions.ToShort(Core.Type.MyMap.Indoors))
                return;

            if (index < 1 | index > GameState.NumParallax)
                return;

            // Calculate horizontal and vertical offsets based on player position
            horz = GameLogic.ConvertMapX(GetPlayerX(GameState.MyIndex)) * 2.5f - 50f;
            vert = GameLogic.ConvertMapY(GetPlayerY(GameState.MyIndex)) * 2.5f - 50f;

            string argpath = System.IO.Path.Combine(Core.Path.Parallax, index.ToString());
            GameClient.RenderTexture(ref argpath, (int)Math.Round(horz), (int)Math.Round(vert), 0, 0, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Parallax, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Parallax, index.ToString())).Height, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Parallax, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Parallax, index.ToString())).Height);
        }

        public static void DrawPicture(int index = 0, int @type = 0)
        {
            if (index == 0)
            {
                index = Event.Picture.Index;
            }

            if (type == 0)
            {
                type = Event.Picture.SpriteType;
            }

            if (index < 1 | index > GameState.NumPictures)
                return;

            if (type < 0 | type >= (int)Core.Enum.PictureType.Count)
                return;

            int posX = 0;
            int posY = 0;

            // Determine position based on type
            switch (type)
            {
                case (int)Core.Enum.PictureType.TopLeft:
                    {
                        posX = 0 - Event.Picture.xOffset;
                        posY = 0 - Event.Picture.yOffset;
                        break;
                    }

                case (int)Core.Enum.PictureType.CenterScreen:
                    {
                        posX = (int)Math.Round(GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Width / 2d - GameClient.GetGfxInfo(Core.Path.Pictures + index).Width / 2d - Event.Picture.xOffset);
                        posY = (int)Math.Round(GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Height / 2d - GameClient.GetGfxInfo(Core.Path.Pictures + index).Height / 2d - Event.Picture.yOffset);
                        break;
                    }

                case (int)Core.Enum.PictureType.CenterEvent:
                    {
                        if (GameState.CurrentEvents < Event.Picture.EventId)
                        {
                            // Reset picture details and exit if event is invalid
                            Event.Picture.EventId = 0;
                            Event.Picture.Index = 0;
                            Event.Picture.SpriteType = 0;
                            Event.Picture.xOffset = 0;
                            Event.Picture.yOffset = 0;
                            return;
                        }
                        posX = (int)Math.Round(GameLogic.ConvertMapX(Core.Type.MapEvents[Event.Picture.EventId].X * 32) / 2d - Event.Picture.xOffset);
                        posY = (int)Math.Round(GameLogic.ConvertMapY(Core.Type.MapEvents[Event.Picture.EventId].Y * 32) / 2d - Event.Picture.yOffset);
                        break;
                    }

                case (int)Core.Enum.PictureType.CenterPlayer:
                    {
                        posX = (int)Math.Round(GameLogic.ConvertMapX(Core.Type.Player[GameState.MyIndex].X * 32) / 2d - Event.Picture.xOffset);
                        posY = (int)Math.Round(GameLogic.ConvertMapY(Core.Type.Player[GameState.MyIndex].Y * 32) / 2d - Event.Picture.yOffset);
                        break;
                    }
            }

            string argpath = System.IO.Path.Combine(Core.Path.Pictures, index.ToString());
            GameClient.RenderTexture(ref argpath, posX, posY, 0, 0, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Height, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Height);
        }

        #endregion

        #region Database

        public static void ClearMap()
        {
            // Reset basic map properties
            Core.Type.MyMap.Name = string.Empty;
            Core.Type.MyMap.Tileset = 1;
            Core.Type.MyMap.MaxX = Constant.MAX_MAPX;
            Core.Type.MyMap.MaxY = Constant.MAX_MAPY;
            Core.Type.MyMap.BootMap = 0;
            Core.Type.MyMap.BootX = 0;
            Core.Type.MyMap.BootY = 0;
            Core.Type.MyMap.Down = 0;
            Core.Type.MyMap.Left = 0;
            Core.Type.MyMap.Moral = 0;
            Core.Type.MyMap.Music = string.Empty;
            Core.Type.MyMap.Revision = 0;
            Core.Type.MyMap.Right = 0;
            Core.Type.MyMap.Up = 0;

            // Initialize NPC and Tile arrays
            Core.Type.MyMap.NPC = new int[Constant.MAX_MAP_NPCS];
            Core.Type.MyMap.Tile = new Core.Type.TileStruct[Core.Type.MyMap.MaxX, Core.Type.MyMap.MaxY];
            Core.Type.TileHistory = new Core.Type.TileHistoryStruct[GameState.MaxTileHistory]; 

            // Reset tile history indices
            GameState.HistoryIndex = 0;
            GameState.TileHistoryHighIndex = 0;

            for (int i = 0; i < GameState.MaxTileHistory; i++)
            {
                Core.Type.TileHistory[i].Tile = new Core.Type.TileStruct[Core.Type.MyMap.MaxX, Core.Type.MyMap.MaxY];
            }
            
            // Reset tiles and tile history
            for (int x = 0; x < Core.Type.MyMap.MaxX; x++)
            {
                for (int y = 0; y < Core.Type.MyMap.MaxY; y++)
                {
                    ResetTile(ref Core.Type.MyMap.Tile[x, y], (int)(Core.Enum.LayerType.Count));

                    for (int i = 0; i < GameState.MaxTileHistory; i++)
                    {
                        ResetTile(ref Core.Type.TileHistory[i].Tile[x, y], (int)(Core.Enum.LayerType.Count));
                    }
                }
            }

            // Clear map events
            ClearMapEvents();
        }

        private static void ResetTile(ref Core.Type.TileStruct tile, int maxLayers)
        {
            tile.Layer = new Core.Type.TileDataStruct[maxLayers];

            for (int l = 0; l < maxLayers; l++)
            {
                tile.Layer[l] = new Core.Type.TileDataStruct
                {
                    Tileset = 0,
                    X = 0,
                    Y = 0,
                    AutoTile = 0
                };
            }

            tile.Data1 = 0;
            tile.Data2 = 0;
            tile.Data3 = 0;
            tile.Data1_2 = 0;
            tile.Data2_2 = 0;
            tile.Data3_2 = 0;
            tile.Type = 0;
            tile.Type2 = 0;
            tile.DirBlock = 0;
        }

        public static void ClearMapItems()
        {
            for (int i = 0; i < Constant.MAX_MAP_ITEMS; i++)
                ClearMapItem(i);

        }

        public static void ClearMapItem(int index)
        {
            Core.Type.MyMapItem[index].Num = -1;
            Core.Type.MyMapItem[index].Value = 0;
            Core.Type.MyMapItem[index].X = 0;
            Core.Type.MyMapItem[index].Y = 0;
        }

        public static void ClearMapNPC(int index)
        {
            ref var withBlock = ref Core.Type.MyMapNPC[index];
            withBlock.Attacking = 0;
            withBlock.AttackTimer = 0;
            withBlock.Dir = 0;
            withBlock.Moving = 0;
            withBlock.Num = -1;
            withBlock.Steps = 0;
            withBlock.Target = 0;
            withBlock.TargetType = 0;
            withBlock.Vital = new int[3];
            withBlock.Vital[(int)Core.Enum.VitalType.HP] = 0;
            withBlock.Vital[(int)Core.Enum.VitalType.SP] = 0;
            withBlock.Vital[(int)Core.Enum.VitalType.SP] = 0;
            withBlock.X = 0;
            withBlock.XOffset = 0;
            withBlock.Y = 0;
            withBlock.YOffset = 0;
        }

        public static void ClearMapNPCs()
        {
            int i;

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                ClearMapNPC(i);

        }

        #endregion

        #region Incoming Packets

        public static void Packet_EditMap(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            GameState.InitMapEditor = true;

            buffer.Dispose();
        }

        public static void Packet_CheckMap(ref byte[] data)
        {
            int x;
            int y;
            int i;
            byte needMap;
            var buffer = new ByteStream(data);

            GameState.GettingMap = Conversions.ToBoolean(1);

            // Erase all players except self
            for (i = 0; i < Constant.MAX_PLAYERS; i++)
            {
                if (i != GameState.MyIndex)
                {
                    SetPlayerMap(i, 0);
                }
            }

            // Erase all temporary tile values
            ClearMapNPCs();
            ClearMapItems();
            Database.ClearBlood();
            ClearMap();
            ClearMapEvents();

            // Get map num
            x = buffer.ReadInt32();

            // Get revision
            y = buffer.ReadInt32();

            needMap = 1;

            // Either the revisions didn't match or we dont have the map, so we need it
            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CNeedMap);
            buffer.WriteInt32(needMap);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void MapData(ref byte[] data)
        {
            int x;
            int y;
            int i;
            int j;
            int mapNum;
            var buffer = new ByteStream(Compression.DecompressBytes(data));

            GameState.MapData = Conversions.ToBoolean(0);

            if (buffer.ReadInt32() == 1)
            {
                mapNum = buffer.ReadInt32();
                Core.Type.MyMap.Name = buffer.ReadString();
                Core.Type.MyMap.Music = buffer.ReadString();
                Core.Type.MyMap.Revision = buffer.ReadInt32();
                Core.Type.MyMap.Moral = (byte)buffer.ReadInt32();
                Core.Type.MyMap.Tileset = buffer.ReadInt32();
                Core.Type.MyMap.Up = buffer.ReadInt32();
                Core.Type.MyMap.Down = buffer.ReadInt32();
                Core.Type.MyMap.Left = buffer.ReadInt32();
                Core.Type.MyMap.Right = buffer.ReadInt32();
                Core.Type.MyMap.BootMap = buffer.ReadInt32();
                Core.Type.MyMap.BootX = (byte)buffer.ReadInt32();
                Core.Type.MyMap.BootY = (byte)buffer.ReadInt32();
                Core.Type.MyMap.MaxX = (byte)buffer.ReadInt32();
                Core.Type.MyMap.MaxY = (byte)buffer.ReadInt32();
                Core.Type.MyMap.Weather = (byte)buffer.ReadInt32();
                Core.Type.MyMap.Fog = buffer.ReadInt32();
                Core.Type.MyMap.WeatherIntensity = buffer.ReadInt32();
                Core.Type.MyMap.FogOpacity = (byte)buffer.ReadInt32();
                Core.Type.MyMap.FogSpeed = (byte)buffer.ReadInt32();
                Core.Type.MyMap.MapTint = Conversions.ToBoolean(buffer.ReadInt32());
                Core.Type.MyMap.MapTintR = (byte)buffer.ReadInt32();
                Core.Type.MyMap.MapTintG = (byte)buffer.ReadInt32();
                Core.Type.MyMap.MapTintB = (byte)buffer.ReadInt32();
                Core.Type.MyMap.MapTintA = (byte)buffer.ReadInt32();
                Core.Type.MyMap.Panorama = buffer.ReadByte();
                Core.Type.MyMap.Parallax = buffer.ReadByte();
                Core.Type.MyMap.Brightness = buffer.ReadByte();
                Core.Type.MyMap.NoRespawn = Conversions.ToBoolean(buffer.ReadInt32());
                Core.Type.MyMap.Indoors = Conversions.ToBoolean(buffer.ReadInt32());
                Core.Type.MyMap.Shop = buffer.ReadInt32();

                Core.Type.MyMap.Tile = new Core.Type.TileStruct[Core.Type.MyMap.MaxX, Core.Type.MyMap.MaxY];
                Core.Type.TileHistory = new Core.Type.TileHistoryStruct[GameState.MaxTileHistory];


                for (i = 0; i < GameState.MaxTileHistory; i++)
                {
                    Core.Type.TileHistory[i].Tile = new Core.Type.TileStruct[Core.Type.MyMap.MaxX, Core.Type.MyMap.MaxY];
                }

                // Reset tiles and tile history
                for (x = 0; x < Core.Type.MyMap.MaxX; x++)
                {
                    for (y = 0; y < Core.Type.MyMap.MaxY; y++)
                    {
                        ResetTile(ref Core.Type.MyMap.Tile[x, y], (int)(Core.Enum.LayerType.Count));

                        for (i = 0; i < GameState.MaxTileHistory; i++)
                        {
                            ResetTile(ref Core.Type.TileHistory[i].Tile[x, y], (int)(Core.Enum.LayerType.Count));
                        }
                    }
                }

                for (x = 0; x < Constant.MAX_MAP_NPCS; x++)
                    Core.Type.MyMap.NPC[x] = buffer.ReadInt32();

                var loopTo = (int)Core.Type.MyMap.MaxX;
                for (x = 0; x < loopTo; x++)
                {
                    var loopTo1 = (int)Core.Type.MyMap.MaxY;
                    for (y = 0; y < loopTo1; y++)
                    {
                        Core.Type.MyMap.Tile[x, y].Data1 = buffer.ReadInt32();
                        Core.Type.MyMap.Tile[x, y].Data2 = buffer.ReadInt32();
                        Core.Type.MyMap.Tile[x, y].Data3 = buffer.ReadInt32();
                        Core.Type.MyMap.Tile[x, y].Data1_2 = buffer.ReadInt32();
                        Core.Type.MyMap.Tile[x, y].Data2_2 = buffer.ReadInt32();
                        Core.Type.MyMap.Tile[x, y].Data3_2 = buffer.ReadInt32();
                        Core.Type.MyMap.Tile[x, y].DirBlock = (byte)buffer.ReadInt32();

                        for (j = 0; j <= GameState.MaxTileHistory - 1; j++)
                        {
                            Core.Type.TileHistory[j].Tile[x, y].Data1 = Core.Type.MyMap.Tile[x, y].Data1;
                            Core.Type.TileHistory[j].Tile[x, y].Data2 = Core.Type.MyMap.Tile[x, y].Data2;
                            Core.Type.TileHistory[j].Tile[x, y].Data3 = Core.Type.MyMap.Tile[x, y].Data3;
                            Core.Type.TileHistory[j].Tile[x, y].Data1_2 = Core.Type.MyMap.Tile[x, y].Data1_2;
                            Core.Type.TileHistory[j].Tile[x, y].Data2_2 = Core.Type.MyMap.Tile[x, y].Data2_2;
                            Core.Type.TileHistory[j].Tile[x, y].Data3_2 = Core.Type.MyMap.Tile[x, y].Data3_2;
                            Core.Type.TileHistory[j].Tile[x, y].DirBlock = Core.Type.MyMap.Tile[x, y].DirBlock;
                            Core.Type.TileHistory[j].Tile[x, y].Type = Core.Type.MyMap.Tile[x, y].Type;
                            Core.Type.TileHistory[j].Tile[x, y].Type2 = Core.Type.MyMap.Tile[x, y].Type2;
                        }

                        for (i = 0; i < (int)Core.Enum.LayerType.Count; i++)
                        {  
                            Core.Type.MyMap.Tile[x, y].Layer[i].Tileset = buffer.ReadInt32();
                            Core.Type.MyMap.Tile[x, y].Layer[i].X = buffer.ReadInt32();
                            Core.Type.MyMap.Tile[x, y].Layer[i].Y = buffer.ReadInt32();
                            Core.Type.MyMap.Tile[x, y].Layer[i].AutoTile = (byte)buffer.ReadInt32();

                            for (j = 0; j < GameState.MaxTileHistory; j++)
                            {
                                Core.Type.TileHistory[j].Tile[x, y].Layer[i].Tileset = Core.Type.MyMap.Tile[x, y].Layer[i].Tileset;
                                Core.Type.TileHistory[j].Tile[x, y].Layer[i].X = Core.Type.MyMap.Tile[x, y].Layer[i].X;
                                Core.Type.TileHistory[j].Tile[x, y].Layer[i].Y = Core.Type.MyMap.Tile[x, y].Layer[i].Y;
                                Core.Type.TileHistory[j].Tile[x, y].Layer[i].AutoTile = Core.Type.MyMap.Tile[x, y].Layer[i].AutoTile;
                            }
                        }

                        Core.Type.MyMap.Tile[x, y].Type = (Core.Enum.TileType)buffer.ReadInt32();
                        Core.Type.MyMap.Tile[x, y].Type2 = (Core.Enum.TileType)buffer.ReadInt32();
                    }
                }

                Core.Type.MyMap.EventCount = buffer.ReadInt32();

                if (Core.Type.MyMap.EventCount > 0)
                {
                    Core.Type.MyMap.Event = new Core.Type.EventStruct[Core.Type.MyMap.EventCount];
                    var loopTo2 = Core.Type.MyMap.EventCount;
                    for (i = 0; i < loopTo2; i++)
                    {
                        {
                            ref var withBlock = ref Core.Type.MyMap.Event[i];
                            withBlock.Name = buffer.ReadString();
                            withBlock.Globals = buffer.ReadByte();
                            withBlock.X = buffer.ReadInt32();
                            withBlock.Y = buffer.ReadInt32();
                            withBlock.PageCount = buffer.ReadInt32();
                        }

                        if (Core.Type.MyMap.Event[i].PageCount > 0)
                        {
                            Core.Type.MyMap.Event[i].Pages = new Core.Type.EventPageStruct[Core.Type.MyMap.Event[i].PageCount];
                            var loopTo3 = Core.Type.MyMap.Event[i].PageCount;
                            for (x = 0; x < loopTo3; x++)
                            {
                                {
                                    ref var withBlock1 = ref Core.Type.MyMap.Event[i].Pages[x];
                                    withBlock1.ChkVariable = buffer.ReadInt32();
                                    withBlock1.VariableIndex = buffer.ReadInt32();
                                    withBlock1.VariableCondition = buffer.ReadInt32();
                                    withBlock1.VariableCompare = buffer.ReadInt32();

                                    withBlock1.ChkSwitch = buffer.ReadInt32();
                                    withBlock1.SwitchIndex = buffer.ReadInt32();
                                    withBlock1.SwitchCompare = buffer.ReadInt32();

                                    withBlock1.ChkHasItem = buffer.ReadInt32();
                                    withBlock1.HasItemIndex = buffer.ReadInt32();
                                    withBlock1.HasItemAmount = buffer.ReadInt32();

                                    withBlock1.ChkSelfSwitch = buffer.ReadInt32();
                                    withBlock1.SelfSwitchIndex = buffer.ReadInt32();
                                    withBlock1.SelfSwitchCompare = buffer.ReadInt32();

                                    withBlock1.GraphicType = buffer.ReadByte();
                                    withBlock1.Graphic = buffer.ReadInt32();
                                    withBlock1.GraphicX = buffer.ReadInt32();
                                    withBlock1.GraphicY = buffer.ReadInt32();
                                    withBlock1.GraphicX2 = buffer.ReadInt32();
                                    withBlock1.GraphicY2 = buffer.ReadInt32();

                                    withBlock1.MoveType = buffer.ReadByte();
                                    withBlock1.MoveSpeed = buffer.ReadByte();
                                    withBlock1.MoveFreq = buffer.ReadByte();
                                    withBlock1.MoveRouteCount = buffer.ReadInt32();
                                    withBlock1.IgnoreMoveRoute = buffer.ReadInt32();
                                    withBlock1.RepeatMoveRoute = buffer.ReadInt32();

                                    if (withBlock1.MoveRouteCount > 0)
                                    {
                                        Core.Type.MyMap.Event[i].Pages[x].MoveRoute = new Core.Type.MoveRouteStruct[withBlock1.MoveRouteCount];
                                        var loopTo4 = withBlock1.MoveRouteCount;
                                        for (y = 0; y < loopTo4; y++)
                                        {
                                            withBlock1.MoveRoute[y].Index = buffer.ReadInt32();
                                            withBlock1.MoveRoute[y].Data1 = buffer.ReadInt32();
                                            withBlock1.MoveRoute[y].Data2 = buffer.ReadInt32();
                                            withBlock1.MoveRoute[y].Data3 = buffer.ReadInt32();
                                            withBlock1.MoveRoute[y].Data4 = buffer.ReadInt32();
                                            withBlock1.MoveRoute[y].Data5 = buffer.ReadInt32();
                                            withBlock1.MoveRoute[y].Data6 = buffer.ReadInt32();
                                        }
                                    }

                                    withBlock1.WalkAnim = buffer.ReadInt32();
                                    withBlock1.DirFix = buffer.ReadInt32();
                                    withBlock1.WalkThrough = buffer.ReadInt32();
                                    withBlock1.ShowName = buffer.ReadInt32();
                                    withBlock1.Trigger = buffer.ReadByte();
                                    withBlock1.CommandListCount = buffer.ReadInt32();
                                    withBlock1.Position = buffer.ReadByte();
                                }

                                if (Core.Type.MyMap.Event[i].Pages[x].CommandListCount > 0)
                                {
                                    Core.Type.MyMap.Event[i].Pages[x].CommandList = new Core.Type.CommandListStruct[Core.Type.MyMap.Event[i].Pages[x].CommandListCount];
                                    var loopTo5 = Core.Type.MyMap.Event[i].Pages[x].CommandListCount;
                                    for (y = 0; y < loopTo5; y++)
                                    {
                                        Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount = buffer.ReadInt32();
                                        Core.Type.MyMap.Event[i].Pages[x].CommandList[y].ParentList = buffer.ReadInt32();
                                        if (Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                        {
                                            Core.Type.MyMap.Event[i].Pages[x].CommandList[y].Commands = new Core.Type.EventCommandStruct[Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount];
                                            for (int z = 0, loopTo6 = Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount; z < loopTo6; z++)
                                            {
                                                {
                                                    ref var withBlock2 = ref Core.Type.MyMap.Event[i].Pages[x].CommandList[y].Commands[z];
                                                    withBlock2.Index = buffer.ReadByte();
                                                    withBlock2.Text1 = buffer.ReadString();
                                                    withBlock2.Text2 = buffer.ReadString();
                                                    withBlock2.Text3 = buffer.ReadString();
                                                    withBlock2.Text4 = buffer.ReadString();
                                                    withBlock2.Text5 = buffer.ReadString();
                                                    withBlock2.Data1 = buffer.ReadInt32();
                                                    withBlock2.Data2 = buffer.ReadInt32();
                                                    withBlock2.Data3 = buffer.ReadInt32();
                                                    withBlock2.Data4 = buffer.ReadInt32();
                                                    withBlock2.Data5 = buffer.ReadInt32();
                                                    withBlock2.Data6 = buffer.ReadInt32();
                                                    withBlock2.ConditionalBranch.CommandList = buffer.ReadInt32();
                                                    withBlock2.ConditionalBranch.Condition = buffer.ReadInt32();
                                                    withBlock2.ConditionalBranch.Data1 = buffer.ReadInt32();
                                                    withBlock2.ConditionalBranch.Data2 = buffer.ReadInt32();
                                                    withBlock2.ConditionalBranch.Data3 = buffer.ReadInt32();
                                                    withBlock2.ConditionalBranch.ElseCommandList = buffer.ReadInt32();
                                                    withBlock2.MoveRouteCount = buffer.ReadInt32();
                                                    if (withBlock2.MoveRouteCount > 0)
                                                    {
                                                        Array.Resize(ref withBlock2.MoveRoute, withBlock2.MoveRouteCount);
                                                        for (int w = 0, loopTo7 = withBlock2.MoveRouteCount; w < loopTo7; w++)
                                                        {
                                                            withBlock2.MoveRoute[w].Index = buffer.ReadInt32();
                                                            withBlock2.MoveRoute[w].Data1 = buffer.ReadInt32();
                                                            withBlock2.MoveRoute[w].Data2 = buffer.ReadInt32();
                                                            withBlock2.MoveRoute[w].Data3 = buffer.ReadInt32();
                                                            withBlock2.MoveRoute[w].Data4 = buffer.ReadInt32();
                                                            withBlock2.MoveRoute[w].Data5 = buffer.ReadInt32();
                                                            withBlock2.MoveRoute[w].Data6 = buffer.ReadInt32();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (i = 0; i < Constant.MAX_MAP_ITEMS; i++)
            {
                Core.Type.MyMapItem[i].Num = buffer.ReadInt32();
                Core.Type.MyMapItem[i].Value = buffer.ReadInt32();
                Core.Type.MyMapItem[i].X = (byte)buffer.ReadInt32();
                Core.Type.MyMapItem[i].Y = (byte)buffer.ReadInt32();
            }

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                Core.Type.MyMapNPC[i].Num = buffer.ReadInt32();
                Core.Type.MyMapNPC[i].X = (byte)buffer.ReadInt32();
                Core.Type.MyMapNPC[i].Y = (byte)buffer.ReadInt32();
                Core.Type.MyMapNPC[i].Dir = buffer.ReadInt32();
                for (int n = 0; n < (int)Core.Enum.VitalType.Count; n++)
                    Core.Type.MyMapNPC[i].Vital[n] = buffer.ReadInt32();
            }

            if (buffer.ReadInt32() == 1)
            {
                GameState.ResourceIndex = buffer.ReadInt32();
                GameState.ResourcesInit = Conversions.ToBoolean(0);
                Core.Type.MapResource = new Core.Type.MapResourceStruct[GameState.ResourceIndex];

                if (GameState.ResourceIndex > 0)
                {
                    var loopTo8 = GameState.ResourceIndex - 1;
                    for (i = 0; i < loopTo8; i++)
                    {
                        Core.Type.MyMapResource[i].State = buffer.ReadByte();
                        Core.Type.MyMapResource[i].X = buffer.ReadInt32();
                        Core.Type.MyMapResource[i].Y = buffer.ReadInt32();
                    }

                    GameState.ResourcesInit = Conversions.ToBoolean(1);
                }
            }

            Core.Type.Map[GetPlayerMap(GameState.MyIndex)] = Core.Type.MyMap;

            buffer.Dispose();

            Autotile.InitAutotiles();

            GameState.MapData = Conversions.ToBoolean(1);

            for (i = 0; i < byte.MaxValue; i++)
                GameLogic.ClearActionMsg((byte)i);

            GameState.CurrentWeather = Core.Type.MyMap.Weather;
            GameState.CurrentWeatherIntensity = Core.Type.MyMap.WeatherIntensity;
            GameState.CurrentFog = Core.Type.MyMap.Fog;
            GameState.CurrentFogSpeed = Core.Type.MyMap.FogSpeed;
            GameState.CurrentFogOpacity = Core.Type.MyMap.FogOpacity;
            GameState.CurrentTintR = Core.Type.MyMap.MapTintR;
            GameState.CurrentTintG = Core.Type.MyMap.MapTintG;
            GameState.CurrentTintB = Core.Type.MyMap.MapTintB;
            GameState.CurrentTintA = Core.Type.MyMap.MapTintA;

            GameLogic.UpdateDrawMapName();

            GameState.GettingMap = Conversions.ToBoolean(0);
            GameState.CanMoveNow = true;

        }

        public static void Packet_MapNPCData(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                    ref var withBlock = ref Core.Type.MyMapNPC[i];
                    withBlock.Num = buffer.ReadInt32();
                    withBlock.X = (byte)buffer.ReadInt32();
                    withBlock.Y = (byte)buffer.ReadInt32();
                    withBlock.Dir = buffer.ReadInt32();
                    withBlock.Vital[(int)Core.Enum.VitalType.HP] = buffer.ReadInt32();
            } 

            buffer.Dispose();
        }

        public static void Packet_MapNPCUpdate(ref byte[] data)
        {
            int NPCNum;
            var buffer = new ByteStream(data);

            NPCNum = buffer.ReadInt32();

            ref var withBlock = ref Core.Type.MyMapNPC[NPCNum];
            withBlock.Num = buffer.ReadInt32();
            withBlock.X = (byte)buffer.ReadInt32();
            withBlock.Y = (byte)buffer.ReadInt32();
            withBlock.Dir = buffer.ReadInt32();
            withBlock.Vital[(int)Core.Enum.VitalType.HP] = buffer.ReadInt32();
            withBlock.Vital[(int)Core.Enum.VitalType.SP] = buffer.ReadInt32();

            buffer.Dispose();
        }

        public static void Packet_MapDone(ref byte[] data)
        {
            int i;

            for (i = 0; i < byte.MaxValue; i++)
                GameLogic.ClearActionMsg((byte)i);

            GameState.CurrentWeather = Core.Type.MyMap.Weather;
            GameState.CurrentWeatherIntensity = Core.Type.MyMap.WeatherIntensity;
            GameState.CurrentFog = Core.Type.MyMap.Fog;
            GameState.CurrentFogSpeed = Core.Type.MyMap.FogSpeed;
            GameState.CurrentFogOpacity = Core.Type.MyMap.FogOpacity;
            GameState.CurrentTintR = Core.Type.MyMap.MapTintR;
            GameState.CurrentTintG = Core.Type.MyMap.MapTintG;
            GameState.CurrentTintB = Core.Type.MyMap.MapTintB;
            GameState.CurrentTintA = Core.Type.MyMap.MapTintA;

            GameLogic.UpdateDrawMapName();

            GameState.GettingMap = Conversions.ToBoolean(0);
            GameState.CanMoveNow = true;

        }

        #endregion

        #region Outgoing Packets

        public static void SendPlayerRequestNewMap()
        {
            if (GameState.GettingMap)
                return;

            if (Core.Type.MyMap.Tile[GetPlayerX(GameState.MyIndex), GetPlayerY(GameState.MyIndex)].Type == Core.Enum.TileType.NoXing | Core.Type.MyMap.Tile[GetPlayerX(GameState.MyIndex), GetPlayerY(GameState.MyIndex)].Type2 == Core.Enum.TileType.NoXing)
            {
                Text.AddText("The pathway is blocked.", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestNewMap);
            buffer.WriteInt32(GetPlayerDir(GameState.MyIndex));

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

            GameState.GettingMap = Conversions.ToBoolean(1);
            GameState.CanMoveNow = false;

        }

        public static void SendRequestEditMap()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditMap);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendMap()
        {
            int x;
            int y;
            int i;
            byte[] data;
            var buffer = new ByteStream(4);

            GameState.CanMoveNow = false;

            buffer.WriteString(Core.Type.MyMap.Name);
            buffer.WriteString(Core.Type.MyMap.Music);
            buffer.WriteInt32(Core.Type.MyMap.Moral);
            buffer.WriteInt32(Core.Type.MyMap.Tileset);
            buffer.WriteInt32(Core.Type.MyMap.Up);
            buffer.WriteInt32(Core.Type.MyMap.Down);
            buffer.WriteInt32(Core.Type.MyMap.Left);
            buffer.WriteInt32(Core.Type.MyMap.Right);
            buffer.WriteInt32(Core.Type.MyMap.BootMap);
            buffer.WriteInt32(Core.Type.MyMap.BootX);
            buffer.WriteInt32(Core.Type.MyMap.BootY);
            buffer.WriteInt32(Core.Type.MyMap.MaxX);
            buffer.WriteInt32(Core.Type.MyMap.MaxY);
            buffer.WriteInt32(Core.Type.MyMap.Weather);
            buffer.WriteInt32(Core.Type.MyMap.Fog);
            buffer.WriteInt32(Core.Type.MyMap.WeatherIntensity);
            buffer.WriteInt32(Core.Type.MyMap.FogOpacity);
            buffer.WriteInt32(Core.Type.MyMap.FogSpeed);
            buffer.WriteInt32(Conversions.ToInteger(Core.Type.MyMap.MapTint));
            buffer.WriteInt32(Core.Type.MyMap.MapTintR);
            buffer.WriteInt32(Core.Type.MyMap.MapTintG);
            buffer.WriteInt32(Core.Type.MyMap.MapTintB);
            buffer.WriteInt32(Core.Type.MyMap.MapTintA);
            buffer.WriteByte(Core.Type.MyMap.Panorama);
            buffer.WriteByte(Core.Type.MyMap.Parallax);
            buffer.WriteByte(Core.Type.MyMap.Brightness);
            buffer.WriteInt32(Conversions.ToInteger(Core.Type.MyMap.NoRespawn));
            buffer.WriteInt32(Conversions.ToInteger(Core.Type.MyMap.Indoors));
            buffer.WriteInt32(Core.Type.MyMap.Shop);

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                buffer.WriteInt32(Core.Type.MyMap.NPC[i]);

            var loopTo = (int)Core.Type.MyMap.MaxX;
            for (x = 0; x < loopTo; x++)
            {
                var loopTo1 = (int)Core.Type.MyMap.MaxY;
                for (y = 0; y < loopTo1; y++)
                {
                    buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Data1);
                    buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Data2);
                    buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Data3);
                    buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Data1_2);
                    buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Data2_2);
                    buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Data3_2);
                    buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].DirBlock);
                    for (i = 0; i < (int)Core.Enum.LayerType.Count; i++)
                    {
                        buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Layer[i].Tileset);
                        buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Layer[i].X);
                        buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Layer[i].Y);
                        buffer.WriteInt32(Core.Type.MyMap.Tile[x, y].Layer[i].AutoTile);
                    }
                    buffer.WriteInt32((int)Core.Type.MyMap.Tile[x, y].Type);
                    buffer.WriteInt32((int)Core.Type.MyMap.Tile[x, y].Type2);
                }
            }

            buffer.WriteInt32(Core.Type.MyMap.EventCount);

            if (Core.Type.MyMap.EventCount > 0)
            {
                var loopTo2 = Core.Type.MyMap.EventCount;
                for (i = 0; i < loopTo2; i++)
                {
                    {
                        ref var withBlock = ref Core.Type.MyMap.Event[i];
                        if (withBlock.Name is null)
                            withBlock.Name = "";
                        buffer.WriteString(withBlock.Name);
                        buffer.WriteByte(withBlock.Globals);
                        buffer.WriteInt32(withBlock.X);
                        buffer.WriteInt32(withBlock.Y);
                        buffer.WriteInt32(withBlock.PageCount);
                    }
                    if (Core.Type.MyMap.Event[i].PageCount > 0)
                    {
                        var loopTo3 = Core.Type.MyMap.Event[i].PageCount;
                        for (x = 0; x < loopTo3; x++)
                        {
                            {
                                ref var withBlock1 = ref Core.Type.MyMap.Event[i].Pages[x];
                                buffer.WriteInt32(withBlock1.ChkVariable);
                                buffer.WriteInt32(withBlock1.VariableIndex);
                                buffer.WriteInt32(withBlock1.VariableCondition);
                                buffer.WriteInt32(withBlock1.VariableCompare);
                                buffer.WriteInt32(withBlock1.ChkSwitch);
                                buffer.WriteInt32(withBlock1.SwitchIndex);
                                buffer.WriteInt32(withBlock1.SwitchCompare);
                                buffer.WriteInt32(withBlock1.ChkHasItem);
                                buffer.WriteInt32(withBlock1.HasItemIndex);
                                buffer.WriteInt32(withBlock1.HasItemAmount);
                                buffer.WriteInt32(withBlock1.ChkSelfSwitch);
                                buffer.WriteInt32(withBlock1.SelfSwitchIndex);
                                buffer.WriteInt32(withBlock1.SelfSwitchCompare);
                                buffer.WriteByte(withBlock1.GraphicType);
                                buffer.WriteInt32(withBlock1.Graphic);
                                buffer.WriteInt32(withBlock1.GraphicX);
                                buffer.WriteInt32(withBlock1.GraphicY);
                                buffer.WriteInt32(withBlock1.GraphicX2);
                                buffer.WriteInt32(withBlock1.GraphicY2);
                                buffer.WriteByte(withBlock1.MoveType);
                                buffer.WriteByte(withBlock1.MoveSpeed);
                                buffer.WriteByte(withBlock1.MoveFreq);
                                buffer.WriteInt32(Core.Type.MyMap.Event[i].Pages[x].MoveRouteCount);
                                buffer.WriteInt32(withBlock1.IgnoreMoveRoute);
                                buffer.WriteInt32(withBlock1.RepeatMoveRoute);

                                if (withBlock1.MoveRouteCount > 0)
                                {
                                    var loopTo4 = withBlock1.MoveRouteCount;
                                    for (y = 0; y < loopTo4; y++)
                                    {
                                        buffer.WriteInt32(withBlock1.MoveRoute[y].Index);
                                        buffer.WriteInt32(withBlock1.MoveRoute[y].Data1);
                                        buffer.WriteInt32(withBlock1.MoveRoute[y].Data2);
                                        buffer.WriteInt32(withBlock1.MoveRoute[y].Data3);
                                        buffer.WriteInt32(withBlock1.MoveRoute[y].Data4);
                                        buffer.WriteInt32(withBlock1.MoveRoute[y].Data5);
                                        buffer.WriteInt32(withBlock1.MoveRoute[y].Data6);
                                    }
                                }

                                buffer.WriteInt32(withBlock1.WalkAnim);
                                buffer.WriteInt32(withBlock1.DirFix);
                                buffer.WriteInt32(withBlock1.WalkThrough);
                                buffer.WriteInt32(withBlock1.ShowName);
                                buffer.WriteByte(withBlock1.Trigger);
                                buffer.WriteInt32(withBlock1.CommandListCount);
                                buffer.WriteByte(withBlock1.Position);
                            }

                            if (Core.Type.MyMap.Event[i].Pages[x].CommandListCount > 0)
                            {
                                var loopTo5 = Core.Type.MyMap.Event[i].Pages[x].CommandListCount;
                                for (y = 0; y < loopTo5; y++)
                                {
                                    buffer.WriteInt32(Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount);
                                    buffer.WriteInt32(Core.Type.MyMap.Event[i].Pages[x].CommandList[y].ParentList);
                                    if (Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                    {
                                        for (int z = 0, loopTo6 = Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount; z < loopTo6; z++)
                                        {
                                            {
                                                ref var withBlock2 = ref Core.Type.MyMap.Event[i].Pages[x].CommandList[y].Commands[z];
                                                buffer.WriteByte(withBlock2.Index);
                                                buffer.WriteString(withBlock2.Text1);
                                                buffer.WriteString(withBlock2.Text2);
                                                buffer.WriteString(withBlock2.Text3);
                                                buffer.WriteString(withBlock2.Text4);
                                                buffer.WriteString(withBlock2.Text5);
                                                buffer.WriteInt32(withBlock2.Data1);
                                                buffer.WriteInt32(withBlock2.Data2);
                                                buffer.WriteInt32(withBlock2.Data3);
                                                buffer.WriteInt32(withBlock2.Data4);
                                                buffer.WriteInt32(withBlock2.Data5);
                                                buffer.WriteInt32(withBlock2.Data6);
                                                buffer.WriteInt32(withBlock2.ConditionalBranch.CommandList);
                                                buffer.WriteInt32(withBlock2.ConditionalBranch.Condition);
                                                buffer.WriteInt32(withBlock2.ConditionalBranch.Data1);
                                                buffer.WriteInt32(withBlock2.ConditionalBranch.Data2);
                                                buffer.WriteInt32(withBlock2.ConditionalBranch.Data3);
                                                buffer.WriteInt32(withBlock2.ConditionalBranch.ElseCommandList);
                                                buffer.WriteInt32(withBlock2.MoveRouteCount);
                                                if (withBlock2.MoveRouteCount > 0)
                                                {
                                                    for (int w = 0, loopTo7 = withBlock2.MoveRouteCount; w < loopTo7; w++)
                                                    {
                                                        buffer.WriteInt32(withBlock2.MoveRoute[w].Index);
                                                        buffer.WriteInt32(withBlock2.MoveRoute[w].Data1);
                                                        buffer.WriteInt32(withBlock2.MoveRoute[w].Data2);
                                                        buffer.WriteInt32(withBlock2.MoveRoute[w].Data3);
                                                        buffer.WriteInt32(withBlock2.MoveRoute[w].Data4);
                                                        buffer.WriteInt32(withBlock2.MoveRoute[w].Data5);
                                                        buffer.WriteInt32(withBlock2.MoveRoute[w].Data6);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            data = buffer.ToArray();

            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CSaveMap);
            buffer.WriteBlock(Compression.CompressBytes(data));

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendMapRespawn()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CMapRespawn);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void ClearMapEvents()
        {
            Core.Type.MapEvents = new Core.Type.MapEventStruct[Core.Type.MyMap.EventCount];

            for (int i = 0, loopTo = Core.Type.MyMap.EventCount; i < loopTo; i++)
                Core.Type.MapEvents[i].Name = "";

            GameState.CurrentEvents = 0;
        }

        #endregion

    }
}