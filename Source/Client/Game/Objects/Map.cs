using System;
using System.Drawing;
using Core;
using static Core.Global.Command;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using Mirage.Sharp.Asfw;
using Mirage.Sharp.Asfw.IO;
using Microsoft.VisualBasic;

namespace Client
{

    public class Map
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

            for (i = 0; i < Constant.MAX_WEATHER_PARTICLES; i++)
            {
                if (Conversions.ToBoolean(GameState.WeatherParticle[i].InUse))
                {
                    if (GameState.WeatherParticle[i].Type == (int)Core.WeatherType.Storm)
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
            float alpha;
            var rect = new Rectangle(0, 0, 0, 0);

            // Check if the map or its tile data is not ready
            if (GameState.GettingMap || !GameState.MapData)
                return;

            // Ensure x and y are within the bounds of the map
            if (x < 0 || y < 0 || x >= Data.MyMap.MaxX || y >= Data.MyMap.MaxY)
                return;

            try
            {
                for (i = (int)MapLayer.Ground; i <= (int)MapLayer.CoverAnimation; i++)
                {
                    int layerIndex = i;

                    // Handle animated layers
                    if (GameState.MapAnim)
                    {
                        switch (i)
                        {
                            case (int)MapLayer.Mask:
                                if (Data.MyMap.Tile[x, y].Layer != null &&
                                    Data.MyMap.Tile[x, y].Layer.Length > (int)MapLayer.MaskAnimation &&
                                    Data.MyMap.Tile[x, y].Layer[(int)MapLayer.MaskAnimation].Tileset > 0)
                                    layerIndex = (int)MapLayer.MaskAnimation;
                                break;
                            case (int)MapLayer.Cover:
                                if (Data.MyMap.Tile[x, y].Layer != null &&
                                    Data.MyMap.Tile[x, y].Layer.Length > (int)MapLayer.CoverAnimation &&
                                    Data.MyMap.Tile[x, y].Layer[(int)MapLayer.CoverAnimation].Tileset > 0)
                                    layerIndex = (int)MapLayer.CoverAnimation;
                                break;
                        }
                    }
                    else
                    {
                        // Skip non-animated layers
                        if (i == (int)MapLayer.MaskAnimation || i == (int)MapLayer.CoverAnimation)
                            continue;
                    }

                    // Check if this layer has a valid tileset and array is large enough
                    if (Data.MyMap.Tile[x, y].Layer != null &&
                        Data.MyMap.Tile[x, y].Layer.Length > layerIndex &&
                        Data.Autotile[x, y].Layer != null &&
                        Data.Autotile[x, y].Layer.Length > layerIndex &&
                        Data.MyMap.Tile[x, y].Layer[layerIndex].Tileset > 0 &&
                        Data.MyMap.Tile[x, y].Layer[layerIndex].Tileset <= GameState.NumTileSets)
                    {
                        // Normal rendering state
                        if (Data.Autotile[x, y].Layer[layerIndex].RenderState == GameState.RenderStateNormal)
                        {
                            rect.X = Data.MyMap.Tile[x, y].Layer[layerIndex].X * GameState.PicX;
                            rect.Y = Data.MyMap.Tile[x, y].Layer[layerIndex].Y * GameState.PicY;
                            rect.Width = GameState.PicX;
                            rect.Height = GameState.PicY;

                            alpha = 1.0f;

                            if (GameState.MyEditorType == (int)EditorType.Map)
                            {
                                if (GameState.HideLayers)
                                {
                                    if (i != GameState.CurLayer)
                                    {
                                        alpha = 0.5f;
                                    }
                                }
                            }

                            // Render the tile
                            string argpath = System.IO.Path.Combine(Core.Path.Tilesets, Data.MyMap.Tile[x, y].Layer[layerIndex].Tileset.ToString());
                            GameClient.RenderTexture(ref argpath, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY), rect.X, rect.Y, rect.Width, rect.Height, rect.Width, rect.Height, alpha);
                        }

                        // Autotile rendering state
                        else if (Data.Autotile[x, y].Layer[layerIndex].RenderState == GameState.RenderStateAutotile)
                        {
                            if (SettingsManager.Instance.Autotile)
                            {
                                DrawAutoTile(layerIndex, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY), 1, x, y, 0, false);
                                DrawAutoTile(layerIndex, GameLogic.ConvertMapX(x * GameState.PicX) + 16, GameLogic.ConvertMapY(y * GameState.PicY), 2, x, y, 0, false);
                                DrawAutoTile(layerIndex, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY) + 16, 3, x, y, 0, false);
                                DrawAutoTile(layerIndex, GameLogic.ConvertMapX(x * GameState.PicX) + 16, GameLogic.ConvertMapY(y * GameState.PicY) + 16, 4, x, y, 0, false);
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
            float alpha;
            var rect = default(Rectangle);

            // Exit early if map is still loading or tile data is not available
            if (GameState.GettingMap || !GameState.MapData)
                return;

            // Ensure x and y are within valid map bounds
            if (x < 0 || y < 0 || x >= Data.MyMap.MaxX || y >= Data.MyMap.MaxY)
                return;

            try
            {
                // Loop through the layers from Fringe to RoofAnim
                for (i = (int)MapLayer.Fringe; i <= (int)MapLayer.RoofAnimation; i++)
                {
                    int layerIndex = i;

                    // Handle animated layers
                    if (GameState.MapAnim)
                    {
                        switch (i)
                        {
                            case (int)MapLayer.Fringe:
                                if (Data.MyMap.Tile[x, y].Layer?.Length > (int)MapLayer.FringeAnimation &&
                                    Data.MyMap.Tile[x, y].Layer[(int)MapLayer.FringeAnimation].Tileset > 0)
                                    layerIndex = (int)MapLayer.FringeAnimation;
                                break;
                            case (int)MapLayer.Roof:
                                if (Data.MyMap.Tile[x, y].Layer.Length > (int)MapLayer.RoofAnimation &&
                                    Data.MyMap.Tile[x, y].Layer[(int)MapLayer.RoofAnimation].Tileset > 0)
                                    layerIndex = (int)MapLayer.RoofAnimation;
                                break;
                        }
                    }
                    else
                    {
                        // Skip non-animated layers
                        if (i == (int)MapLayer.FringeAnimation || i == (int)MapLayer.RoofAnimation)
                            continue;
                    }

                    // Check if this layer has a valid tileset and array is large enough
                    if (Data.MyMap.Tile[x, y].Layer != null &&
                        Data.MyMap.Tile[x, y].Layer.Length > layerIndex &&
                        Data.Autotile[x, y].Layer != null &&
                        Data.Autotile[x, y].Layer.Length > layerIndex &&
                        Data.MyMap.Tile[x, y].Layer[layerIndex].Tileset > 0 &&
                        Data.MyMap.Tile[x, y].Layer[layerIndex].Tileset <= GameState.NumTileSets)
                    {
                        // Check if the render state is normal and render the tile
                        if (Data.Autotile[x, y].Layer[layerIndex].RenderState == GameState.RenderStateNormal)
                        {
                            rect.X = Data.MyMap.Tile[x, y].Layer[layerIndex].X * GameState.PicX;
                            rect.Y = Data.MyMap.Tile[x, y].Layer[layerIndex].Y * GameState.PicY;
                            rect.Width = GameState.PicX;
                            rect.Height = GameState.PicY;

                            alpha = 1.0f;

                            if (GameState.MyEditorType == (int)EditorType.Map)
                            {
                                if (GameState.HideLayers)
                                {
                                    if (i != GameState.CurLayer)
                                    {
                                        alpha = 0.5f;
                                    }
                                }
                            }

                            // Render the tile with the calculated rectangle and transparency
                            string argpath = System.IO.Path.Combine(Core.Path.Tilesets, Data.MyMap.Tile[x, y].Layer[layerIndex].Tileset.ToString());
                            GameClient.RenderTexture(ref argpath, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY), rect.X, rect.Y, rect.Width, rect.Height, rect.Width, rect.Height, alpha);
                        }
                        // Handle autotile rendering
                        else if (Data.Autotile[x, y].Layer[layerIndex].RenderState == GameState.RenderStateAutotile)
                        {
                            if (SettingsManager.Instance.Autotile)
                            {
                                // Render autotiles
                                DrawAutoTile(layerIndex, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY), 1, x, y, 0, false);
                                DrawAutoTile(layerIndex, GameLogic.ConvertMapX(x * GameState.PicX) + 16, GameLogic.ConvertMapY(y * GameState.PicY), 2, x, y, 0, false);
                                DrawAutoTile(layerIndex, GameLogic.ConvertMapX(x * GameState.PicX), GameLogic.ConvertMapY(y * GameState.PicY) + 16, 3, x, y, 0, false);
                                DrawAutoTile(layerIndex, GameLogic.ConvertMapX(x * GameState.PicX) + 16, GameLogic.ConvertMapY(y * GameState.PicY) + 16, 4, x, y, 0, false);
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

            switch (Data.MyMap.Tile[x, y].Layer[layerNum].AutoTile)
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

            if (Data.MyMap.Tile[x, y].Layer is null)
                return;
            string argpath = System.IO.Path.Combine(Core.Path.Tilesets, Data.MyMap.Tile[x, y].Layer[layerNum].Tileset.ToString());
            GameClient.RenderTexture(ref argpath, dX, dY, Data.Autotile[x, y].Layer[layerNum].SrcX[quarterNum] + xOffset, Data.Autotile[x, y].Layer[layerNum].SrcY[quarterNum] + yOffset, 16, 16, 16, 16);
        }

        public static void DrawMapTint()
        {
            if (Conversions.ToInteger(Data.MyMap.MapTint) == 0)
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
            if (Data.MyMap.Indoors)
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

            if (Data.MyMap.Moral == Conversions.ToShort(Data.MyMap.Indoors))
                return;

            if (index < 1 | index > GameState.NumParallax)
                return;

            // Calculate horizontal and vertical offsets based
            // yer position
            horz = GameLogic.ConvertMapX(GetPlayerX(GameState.MyIndex)) * 2.5f - 50f;
            vert = GameLogic.ConvertMapY(GetPlayerY(GameState.MyIndex)) * 2.5f - 50f;

            string argpath = System.IO.Path.Combine(Core.Path.Parallax, index.ToString());
            GameClient.RenderTexture(ref argpath, (int)Math.Round(horz), (int)Math.Round(vert), 0, 0, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Parallax, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Parallax, index.ToString())).Height, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Parallax, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Parallax, index.ToString())).Height);
        }

        public static void DrawPicture(int index = 0, int type = 0)
        {
            if (index == 0)
            {
                index = Event.Picture.Index;
            }

            if (type == 0)
            {
                type = Event.Picture.SpriteType;
            }

            // Use enum values for comparison
            if (index < 1 || index > GameState.NumPictures)
                return;

            if (type < (int)PictureOrigin.TopLeft || type > (int)PictureOrigin.CenterOnPlayer)
                return;

            int posX = 0;
            int posY = 0;

            // Determine position based on type
            switch ((PictureOrigin)type)
            {
                case PictureOrigin.TopLeft:
                    posX = 0 - Event.Picture.xOffset;
                    posY = 0 - Event.Picture.yOffset;
                    break;

                case PictureOrigin.CenterScreen:
                    posX = (int)Math.Round(GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Width / 2d - GameClient.GetGfxInfo(Core.Path.Pictures + index).Width / 2d - Event.Picture.xOffset);
                    posY = (int)Math.Round(GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Height / 2d - GameClient.GetGfxInfo(Core.Path.Pictures + index).Height / 2d - Event.Picture.yOffset);
                    break;

                case PictureOrigin.CenterOnEvent:
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
                    posX = (int)Math.Round(GameLogic.ConvertMapX(Data.MapEvents[Event.Picture.EventId].X * 32) / 2d - Event.Picture.xOffset);
                    posY = (int)Math.Round(GameLogic.ConvertMapY(Data.MapEvents[Event.Picture.EventId].Y * 32) / 2d - Event.Picture.yOffset);
                    break;

                case PictureOrigin.CenterOnPlayer:
                    posX = (int)Math.Round(GameLogic.ConvertMapX(Core.Data.Player[GameState.MyIndex].X * 32) / 2d - Event.Picture.xOffset);
                    posY = (int)Math.Round(GameLogic.ConvertMapY(Core.Data.Player[GameState.MyIndex].Y * 32) / 2d - Event.Picture.yOffset);
                    break;
            }

            string argpath = System.IO.Path.Combine(Core.Path.Pictures, index.ToString());
            GameClient.RenderTexture(ref argpath, posX, posY, 0, 0, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Height, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Width, GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Pictures, index.ToString())).Height);
        }

        public static void ClearMap()
        {
            // Reset basic map properties
            Data.MyMap.Name = string.Empty;
            Data.MyMap.Tileset = 1;
            Data.MyMap.MaxX = Constant.MAX_MAPX;
            Data.MyMap.MaxY = Constant.MAX_MAPY;
            Data.MyMap.BootMap = 0;
            Data.MyMap.BootX = 0;
            Data.MyMap.BootY = 0;
            Data.MyMap.Down = 0;
            Data.MyMap.Left = 0;
            Data.MyMap.Moral = 0;
            Data.MyMap.Music = string.Empty;
            Data.MyMap.Revision = 0;
            Data.MyMap.Right = 0;
            Data.MyMap.Up = 0;

            // Initialize Npc and Tile arrays
            Data.MyMap.Npc = new int[Constant.MAX_MAP_NPCS];

            for (int i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                Data.MyMap.Npc[i] = -1;
            }

            Data.MyMap.Tile = new Core.Type.Tile[Data.MyMap.MaxX, Data.MyMap.MaxY];
            Data.TileHistory = new Core.Type.TileHistory[GameState.MaxTileHistory]; // Fixed type name

            // Reset tile history indices
            GameState.TileHistoryIndex = 0;

            for (int i = 0; i < GameState.MaxTileHistory; i++)
            {
                Data.TileHistory[i].Tile = new Core.Type.Tile[Data.MyMap.MaxX, Data.MyMap.MaxY];
            }


            int layerCount = System.Enum.GetValues(typeof(MapLayer)).Length;

            // Reset tiles and tile history
            for (int x = 0; x < Data.MyMap.MaxX; x++)
            {
                for (int y = 0; y < Data.MyMap.MaxY; y++)
                {
                    ResetTile(ref Data.MyMap.Tile[x, y], layerCount);
                }
            }

            // Clear map events
            ClearMapEvents();
        }

        private static void ResetTile(ref Core.Type.Tile tile, int maxLayers)
        {
            tile.Layer = new Core.Type.Layer[maxLayers];

            for (int l = 0; l < maxLayers; l++)
            {
                tile.Layer[l] = new Core.Type.Layer
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
            Data.MyMapItem[index].Num = -1;
            Data.MyMapItem[index].Value = 0;
            Data.MyMapItem[index].X = 0;
            Data.MyMapItem[index].Y = 0;
        }

        public static void ClearMapNpc(int index)
        {
            ref var withBlock = ref Data.MyMapNpc[index];
            withBlock.Attacking = 0;
            withBlock.AttackTimer = 0;
            withBlock.Dir = 0;
            withBlock.Moving = 0;
            withBlock.Num = -1;
            withBlock.SkillBuffer = -1;
            withBlock.Steps = 0;
            withBlock.Target = 0;
            withBlock.TargetType = 0;
            withBlock.Vital = new int[System.Enum.GetValues(typeof(Core.Vital)).Length];
            for (int i = 0; i < System.Enum.GetValues(typeof(Core.Vital)).Length; i++)
            {
                withBlock.Vital[i] = 0;
            }
            withBlock.X = 0;
            withBlock.Y = 0;
        }

        public static void ClearMapNpcs()
        {
            for (int i = 0; i < Constant.MAX_MAP_NPCS; i++)
                ClearMapNpc(i);

        }

        #endregion

        #region Incoming Packets

        public static void Packet_EditMap(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            GameState.InitMapEditor = true;
            Gui.HideWindows();

            buffer.Dispose();
        }

        public static void Packet_CheckMap(ref byte[] data)
        {
            int x;
            int y;
            int i;
            byte needMap;
            var buffer = new ByteStream(data);

            GameState.GettingMap = true;

            // Erase all players except self
            for (i = 0; i < Constant.MAX_PLAYERS; i++)
            {
                if (i != GameState.MyIndex)
                {
                    SetPlayerMap(i, 0);
                }
            }

            // Erase all temporary tile values
            ClearMapNpcs();
            Database.ClearBlood();
            ClearMap();
            ClearMapItems();
            ClearMapEvents();
            GameLogic.RemoveChatBubbles();
            Animation.ClearAnimInstances();

            GameState.ResourceIndex = 0;
            Data.MyMapResource = default;
            Core.Data.MapResource = default;

            // Get map num
            x = buffer.ReadInt32();

            // Get revision
            y = buffer.ReadInt32();

            needMap = 1;

            // Either the revisions didn't match or we dont have the map, so we need it
            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CNeedMap);
            buffer.WriteInt32(needMap);
            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);

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

            GameState.MapData = false;

            if (buffer.ReadInt32() == 1)
            {
                mapNum = buffer.ReadInt32();
                Data.MyMap.Name = buffer.ReadString();
                Data.MyMap.Music = buffer.ReadString();
                Data.MyMap.Revision = buffer.ReadInt32();
                Data.MyMap.Moral = (byte)buffer.ReadInt32();
                Data.MyMap.Tileset = buffer.ReadInt32();
                Data.MyMap.Up = buffer.ReadInt32();
                Data.MyMap.Down = buffer.ReadInt32();
                Data.MyMap.Left = buffer.ReadInt32();
                Data.MyMap.Right = buffer.ReadInt32();
                Data.MyMap.BootMap = buffer.ReadInt32();
                Data.MyMap.BootX = (byte)buffer.ReadInt32();
                Data.MyMap.BootY = (byte)buffer.ReadInt32();
                Data.MyMap.MaxX = (byte)buffer.ReadInt32();
                Data.MyMap.MaxY = (byte)buffer.ReadInt32();
                Data.MyMap.Weather = (byte)buffer.ReadInt32();
                Data.MyMap.Fog = buffer.ReadInt32();
                Data.MyMap.WeatherIntensity = buffer.ReadInt32();
                Data.MyMap.FogOpacity = (byte)buffer.ReadInt32();
                Data.MyMap.FogSpeed = (byte)buffer.ReadInt32();
                Data.MyMap.MapTint = buffer.ReadBoolean();
                Data.MyMap.MapTintR = (byte)buffer.ReadInt32();
                Data.MyMap.MapTintG = (byte)buffer.ReadInt32();
                Data.MyMap.MapTintB = (byte)buffer.ReadInt32();
                Data.MyMap.MapTintA = (byte)buffer.ReadInt32();
                Data.MyMap.Panorama = buffer.ReadByte();
                Data.MyMap.Parallax = buffer.ReadByte();
                Data.MyMap.Brightness = buffer.ReadByte();
                Data.MyMap.NoRespawn = buffer.ReadBoolean();
                Data.MyMap.Indoors = buffer.ReadBoolean();
                Data.MyMap.Shop = buffer.ReadInt32();

                Data.MyMap.Tile = new Core.Type.Tile[Data.MyMap.MaxX, Data.MyMap.MaxY];
                Data.TileHistory = new Core.Type.TileHistory[GameState.MaxTileHistory];

                for (i = 0; i < GameState.MaxTileHistory; i++)
                {
                    Data.TileHistory[i].Tile = new Core.Type.Tile[Data.MyMap.MaxX, Data.MyMap.MaxY];
                }

                int layerCount = System.Enum.GetValues(typeof(MapLayer)).Length;

                // Reset tiles and tile history
                for (x = 0; x < Data.MyMap.MaxX; x++)
                {
                    for (y = 0; y < Data.MyMap.MaxY; y++)
                    {
                        ResetTile(ref Data.MyMap.Tile[x, y], layerCount);

                        for (i = 0; i < GameState.MaxTileHistory; i++)
                        {
                            ResetTile(ref Data.TileHistory[i].Tile[x, y], layerCount);
                        }
                    }
                }

                for (x = 0; x < Constant.MAX_MAP_NPCS; x++)
                    Data.MyMap.Npc[x] = buffer.ReadInt32();

                var loopTo = (int)Data.MyMap.MaxX;
                for (x = 0; x < loopTo; x++)
                {
                    var loopTo1 = (int)Data.MyMap.MaxY;
                    for (y = 0; y < loopTo1; y++)
                    {
                        Data.MyMap.Tile[x, y].Data1 = buffer.ReadInt32();
                        Data.MyMap.Tile[x, y].Data2 = buffer.ReadInt32();
                        Data.MyMap.Tile[x, y].Data3 = buffer.ReadInt32();
                        Data.MyMap.Tile[x, y].Data1_2 = buffer.ReadInt32();
                        Data.MyMap.Tile[x, y].Data2_2 = buffer.ReadInt32();
                        Data.MyMap.Tile[x, y].Data3_2 = buffer.ReadInt32();
                        Data.MyMap.Tile[x, y].DirBlock = (byte)buffer.ReadInt32();

                        for (j = 0; j < GameState.MaxTileHistory; j++)
                        {
                            Data.TileHistory[j].Tile[x, y].Data1 = Data.MyMap.Tile[x, y].Data1;
                            Data.TileHistory[j].Tile[x, y].Data2 = Data.MyMap.Tile[x, y].Data2;
                            Data.TileHistory[j].Tile[x, y].Data3 = Data.MyMap.Tile[x, y].Data3;
                            Data.TileHistory[j].Tile[x, y].Data1_2 = Data.MyMap.Tile[x, y].Data1_2;
                            Data.TileHistory[j].Tile[x, y].Data2_2 = Data.MyMap.Tile[x, y].Data2_2;
                            Data.TileHistory[j].Tile[x, y].Data3_2 = Data.MyMap.Tile[x, y].Data3_2;
                            Data.TileHistory[j].Tile[x, y].DirBlock = Data.MyMap.Tile[x, y].DirBlock;
                            Data.TileHistory[j].Tile[x, y].Type = Data.MyMap.Tile[x, y].Type;
                            Data.TileHistory[j].Tile[x, y].Type2 = Data.MyMap.Tile[x, y].Type2;
                        }

                        for (i = 0; i < layerCount; i++)
                        {
                            Data.MyMap.Tile[x, y].Layer[i].Tileset = buffer.ReadInt32();
                            Data.MyMap.Tile[x, y].Layer[i].X = buffer.ReadInt32();
                            Data.MyMap.Tile[x, y].Layer[i].Y = buffer.ReadInt32();
                            Data.MyMap.Tile[x, y].Layer[i].AutoTile = (byte)buffer.ReadInt32();

                            for (j = 0; j < GameState.MaxTileHistory; j++)
                            {
                                Data.TileHistory[j].Tile[x, y].Layer[i].Tileset = Data.MyMap.Tile[x, y].Layer[i].Tileset;
                                Data.TileHistory[j].Tile[x, y].Layer[i].X = Data.MyMap.Tile[x, y].Layer[i].X;
                                Data.TileHistory[j].Tile[x, y].Layer[i].Y = Data.MyMap.Tile[x, y].Layer[i].Y;
                                Data.TileHistory[j].Tile[x, y].Layer[i].AutoTile = Data.MyMap.Tile[x, y].Layer[i].AutoTile;
                            }
                        }

                        Data.MyMap.Tile[x, y].Type = (TileType)buffer.ReadInt32();
                        Data.MyMap.Tile[x, y].Type2 = (TileType)buffer.ReadInt32();
                    }
                }

                Data.MyMap.EventCount = buffer.ReadInt32();

                if (Data.MyMap.EventCount > 0)
                {
                    Data.MyMap.Event = new Core.Type.Event[Data.MyMap.EventCount];
                    var loopTo2 = Data.MyMap.EventCount;
                    for (i = 0; i < loopTo2; i++)
                    {
                        {
                            ref var withBlock = ref Data.MyMap.Event[i];
                            withBlock.Name = buffer.ReadString();
                            withBlock.Globals = buffer.ReadByte();
                            withBlock.X = buffer.ReadInt32();
                            withBlock.Y = buffer.ReadInt32();
                            withBlock.PageCount = buffer.ReadInt32();
                        }

                        if (Data.MyMap.Event[i].PageCount > 0)
                        {
                            Data.MyMap.Event[i].Pages = new Core.Type.EventPage[Data.MyMap.Event[i].PageCount];
                            var loopTo3 = Data.MyMap.Event[i].PageCount;
                            for (x = 0; x < loopTo3; x++)
                            {
                                {
                                    ref var withBlock1 = ref Data.MyMap.Event[i].Pages[x];
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
                                        Data.MyMap.Event[i].Pages[x].MoveRoute = new Core.Type.MoveRoute[withBlock1.MoveRouteCount];
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

                                if (Data.MyMap.Event[i].Pages[x].CommandListCount > 0)
                                {
                                    Data.MyMap.Event[i].Pages[x].CommandList = new Core.Type.CommandList[Data.MyMap.Event[i].Pages[x].CommandListCount];
                                    var loopTo5 = Data.MyMap.Event[i].Pages[x].CommandListCount;
                                    for (y = 0; y < loopTo5; y++)
                                    {
                                        Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount = buffer.ReadInt32();
                                        Data.MyMap.Event[i].Pages[x].CommandList[y].ParentList = buffer.ReadInt32();
                                        if (Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                        {
                                            Data.MyMap.Event[i].Pages[x].CommandList[y].Commands = new Core.Type.EventCommand[Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount];
                                            for (int z = 0, loopTo6 = Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount; z < loopTo6; z++)
                                            {
                                                {
                                                    ref var withBlock2 = ref Data.MyMap.Event[i].Pages[x].CommandList[y].Commands[z];
                                                    withBlock2.Index = buffer.ReadInt32();
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
                Data.MyMapItem[i].Num = buffer.ReadInt32();
                Data.MyMapItem[i].Value = buffer.ReadInt32();
                Data.MyMapItem[i].X = (byte)buffer.ReadInt32();
                Data.MyMapItem[i].Y = (byte)buffer.ReadInt32();
            }

            int vitalCount = System.Enum.GetValues(typeof(Core.Vital)).Length;

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                Data.MyMapNpc[i].Num = buffer.ReadInt32();
                Data.MyMapNpc[i].X = (byte)buffer.ReadInt32();
                Data.MyMapNpc[i].Y = (byte)buffer.ReadInt32();
                Data.MyMapNpc[i].Dir = buffer.ReadInt32();
                for (int n = 0; n < vitalCount; n++)
                    Data.MyMapNpc[i].Vital[n] = buffer.ReadInt32();
            }

            if (buffer.ReadInt32() == 1)
            {
                GameState.ResourceIndex = buffer.ReadInt32();
                GameState.ResourcesInit = false;
                Core.Data.MapResource = new Core.Type.MapResource[GameState.ResourceIndex];
                Data.MyMapResource = new Core.Type.MapResourceCache[Constant.MAX_RESOURCES];

                if (GameState.ResourceIndex > 0)
                {
                    var loopTo8 = GameState.ResourceIndex;
                    for (i = 0; i < loopTo8; i++)
                    {
                        Data.MyMapResource[i].State = buffer.ReadByte();
                        Data.MyMapResource[i].X = buffer.ReadInt32();
                        Data.MyMapResource[i].Y = buffer.ReadInt32();
                    }

                    GameState.ResourcesInit = true;
                }
            }

            Data.Map[GetPlayerMap(GameState.MyIndex)] = Data.MyMap;

            buffer.Dispose();

            Autotile.InitAutotiles();

            GameState.MapData = true;

            for (i = 0; i < byte.MaxValue; i++)
                GameLogic.ClearActionMsg((byte)i);

            GameState.CurrentWeather = Data.MyMap.Weather;
            GameState.CurrentWeatherIntensity = Data.MyMap.WeatherIntensity;
            GameState.CurrentFog = Data.MyMap.Fog;
            GameState.CurrentFogSpeed = Data.MyMap.FogSpeed;
            GameState.CurrentFogOpacity = Data.MyMap.FogOpacity;
            GameState.CurrentTintR = Data.MyMap.MapTintR;
            GameState.CurrentTintG = Data.MyMap.MapTintG;
            GameState.CurrentTintB = Data.MyMap.MapTintB;
            GameState.CurrentTintA = Data.MyMap.MapTintA;

            GameLogic.UpdateDrawMapName();

            GameState.GettingMap = false;
            GameState.CanMoveNow = true;
        }

        public static void Packet_MapNpcData(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                ref var withBlock = ref Data.MyMapNpc[i];
                withBlock.Num = buffer.ReadInt32();
                withBlock.X = (byte)buffer.ReadInt32();
                withBlock.Y = (byte)buffer.ReadInt32();
                withBlock.Dir = buffer.ReadInt32();
            } 

            buffer.Dispose();
        }

        public static void Packet_MapNpcUpdate(ref byte[] data)
        {
            int NpcNum;
            var buffer = new ByteStream(data);

            NpcNum = buffer.ReadInt32();

            ref var withBlock = ref Data.MyMapNpc[NpcNum];
            withBlock.Num = buffer.ReadInt32();
            withBlock.X = (byte)buffer.ReadInt32();
            withBlock.Y = (byte)buffer.ReadInt32();
            withBlock.Dir = buffer.ReadInt32();

            buffer.Dispose();  
        }

        #endregion

        #region Outgoing Packets

        public static void SendPlayerRequestNewMap()
        {
            if (GameState.GettingMap)
                return;

            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestNewMap);
            buffer.WriteInt32(GetPlayerDir(GameState.MyIndex));

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

            GameState.GettingMap = true;
            GameState.CanMoveNow = false;

        }

        public static void SendRequestEditMap()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditMap);
            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);

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

            buffer.WriteString(Data.MyMap.Name);
            buffer.WriteString(Data.MyMap.Music);
            buffer.WriteInt32(Data.MyMap.Moral);
            buffer.WriteInt32(Data.MyMap.Tileset);
            buffer.WriteInt32(Data.MyMap.Up);
            buffer.WriteInt32(Data.MyMap.Down);
            buffer.WriteInt32(Data.MyMap.Left);
            buffer.WriteInt32(Data.MyMap.Right);
            buffer.WriteInt32(Data.MyMap.BootMap);
            buffer.WriteInt32(Data.MyMap.BootX);
            buffer.WriteInt32(Data.MyMap.BootY);
            buffer.WriteInt32(Data.MyMap.MaxX);
            buffer.WriteInt32(Data.MyMap.MaxY);
            buffer.WriteInt32(Data.MyMap.Weather);
            buffer.WriteInt32(Data.MyMap.Fog);
            buffer.WriteInt32(Data.MyMap.WeatherIntensity);
            buffer.WriteInt32(Data.MyMap.FogOpacity);
            buffer.WriteInt32(Data.MyMap.FogSpeed);
            buffer.WriteBoolean(Data.MyMap.MapTint);
            buffer.WriteInt32(Data.MyMap.MapTintR);
            buffer.WriteInt32(Data.MyMap.MapTintG);
            buffer.WriteInt32(Data.MyMap.MapTintB);
            buffer.WriteInt32(Data.MyMap.MapTintA);
            buffer.WriteByte(Data.MyMap.Panorama);
            buffer.WriteByte(Data.MyMap.Parallax);
            buffer.WriteByte(Data.MyMap.Brightness);
            buffer.WriteBoolean(Data.MyMap.NoRespawn);
            buffer.WriteBoolean(Data.MyMap.Indoors);
            buffer.WriteInt32(Data.MyMap.Shop);

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                buffer.WriteInt32(Data.MyMap.Npc[i]);

            var loopTo = (int)Data.MyMap.MaxX;
            for (x = 0; x < loopTo; x++)
            {
                var loopTo1 = (int)Data.MyMap.MaxY;
                for (y = 0; y < loopTo1; y++)
                {
                    buffer.WriteInt32(Data.MyMap.Tile[x, y].Data1);
                    buffer.WriteInt32(Data.MyMap.Tile[x, y].Data2);
                    buffer.WriteInt32(Data.MyMap.Tile[x, y].Data3);
                    buffer.WriteInt32(Data.MyMap.Tile[x, y].Data1_2);
                    buffer.WriteInt32(Data.MyMap.Tile[x, y].Data2_2);
                    buffer.WriteInt32(Data.MyMap.Tile[x, y].Data3_2);
                    buffer.WriteInt32(Data.MyMap.Tile[x, y].DirBlock);

                    int layerCount = System.Enum.GetValues(typeof(MapLayer)).Length;
                    for (i = 0; i < layerCount; i++)
                    {
                        buffer.WriteInt32(Data.MyMap.Tile[x, y].Layer[i].Tileset);
                        buffer.WriteInt32(Data.MyMap.Tile[x, y].Layer[i].X);
                        buffer.WriteInt32(Data.MyMap.Tile[x, y].Layer[i].Y);
                        buffer.WriteInt32(Data.MyMap.Tile[x, y].Layer[i].AutoTile);
                    }
                    buffer.WriteInt32((int)Data.MyMap.Tile[x, y].Type);
                    buffer.WriteInt32((int)Data.MyMap.Tile[x, y].Type2);
                }
            }

            buffer.WriteInt32(Data.MyMap.EventCount);

            if (Data.MyMap.EventCount > 0)
            {
                var loopTo2 = Data.MyMap.EventCount;
                for (i = 0; i < loopTo2; i++)
                {
                    {
                        ref var withBlock = ref Data.MyMap.Event[i];
                        buffer.WriteString(withBlock.Name);
                        buffer.WriteByte(withBlock.Globals);
                        buffer.WriteInt32(withBlock.X);
                        buffer.WriteInt32(withBlock.Y);
                        buffer.WriteInt32(withBlock.PageCount);
                    }
                    if (Data.MyMap.Event[i].PageCount > 0)
                    {
                        var loopTo3 = Data.MyMap.Event[i].PageCount;
                        for (x = 0; x < loopTo3; x++)
                        {
                            {
                                ref var withBlock1 = ref Data.MyMap.Event[i].Pages[x];
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
                                buffer.WriteInt32(Data.MyMap.Event[i].Pages[x].MoveRouteCount);
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

                            if (Data.MyMap.Event[i].Pages[x].CommandListCount > 0)
                            {
                                var loopTo5 = Data.MyMap.Event[i].Pages[x].CommandListCount;
                                for (y = 0; y < loopTo5; y++)
                                {
                                    buffer.WriteInt32(Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount);
                                    buffer.WriteInt32(Data.MyMap.Event[i].Pages[x].CommandList[y].ParentList);
                                    if (Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                    {
                                        for (int z = 0, loopTo6 = Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount; z < loopTo6; z++)
                                        {
                                            {
                                                ref var withBlock2 = ref Data.MyMap.Event[i].Pages[x].CommandList[y].Commands[z];
                                                buffer.WriteInt32(withBlock2.Index);
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

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendMapRespawn()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CMapRespawn);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void ClearMapEvents()
        {
            Data.MapEvents = new Core.Type.MapEvent[Data.MyMap.EventCount];

            for (int i = 0, loopTo = Data.MyMap.EventCount; i < loopTo; i++)
            {
                Data.MapEvents = default;
                Data.MyMap.Event = default;
            }

            GameState.CurrentEvents = 0;
        }

        #endregion

    }
}