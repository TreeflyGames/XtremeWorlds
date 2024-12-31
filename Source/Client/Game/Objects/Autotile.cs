using Microsoft.VisualBasic.CompilerServices;

namespace Client
{

    internal static class Autotile
    {
        public static void ClearAutotiles()
        {
            int x;
            int y;
            int i;

            Core.Type.Autotile = new Core.Type.AutotileStruct[(Core.Type.MyMap.MaxX), (Core.Type.MyMap.MaxY)];

            var loopTo = (int)Core.Type.MyMap.MaxX;
            for (x = 0; x < loopTo; x++)
            {
                var loopTo1 = (int)Core.Type.MyMap.MaxY;
                for (y = 0; y < loopTo1; y++)
                {
                    Core.Type.Autotile[x, y].Layer = new Core.Type.QuarterTileStruct[10];
                    for (i = 0; i <= (int)Core.Enum.LayerType.Count - 1; i++)
                    {
                        Core.Type.Autotile[x, y].Layer[i].SrcX = new int[5];
                        Core.Type.Autotile[x, y].Layer[i].SrcY = new int[5];
                        Core.Type.Autotile[x, y].Layer[i].QuarterTile = new Core.Type.PointStruct[5];
                    }
                }
            }
        }

        // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        // All of this code is for auto tiles and the math behind generating them.
        // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        internal static void PlaceAutotile(int layerNum, int x, int y, byte tileQuarter, string autoTileLetter)
        {

            if (layerNum > (int)Core.Enum.LayerType.Count - 1)
            {
                layerNum = layerNum - ((int)Core.Enum.LayerType.Count - 1);
                {
                    ref var withBlock = ref Core.Type.Autotile[x, y].ExLayer[layerNum].QuarterTile[tileQuarter];
                    switch (autoTileLetter ?? "")
                    {
                        case "a":
                            {
                                withBlock.X = Core.Type.AutoIn[1].X;
                                withBlock.Y = Core.Type.AutoIn[1].Y;
                                break;
                            }
                        case "b":
                            {
                                withBlock.X = Core.Type.AutoIn[2].X;
                                withBlock.Y = Core.Type.AutoIn[2].Y;
                                break;
                            }
                        case "c":
                            {
                                withBlock.X = Core.Type.AutoIn[3].X;
                                withBlock.Y = Core.Type.AutoIn[3].Y;
                                break;
                            }
                        case "d":
                            {
                                withBlock.X = Core.Type.AutoIn[4].X;
                                withBlock.Y = Core.Type.AutoIn[4].Y;
                                break;
                            }
                        case "e":
                            {
                                withBlock.X = Core.Type.AutoNw[1].X;
                                withBlock.Y = Core.Type.AutoNw[1].Y;
                                break;
                            }
                        case "f":
                            {
                                withBlock.X = Core.Type.AutoNw[2].X;
                                withBlock.Y = Core.Type.AutoNw[2].Y;
                                break;
                            }
                        case "g":
                            {
                                withBlock.X = Core.Type.AutoNw[3].X;
                                withBlock.Y = Core.Type.AutoNw[3].Y;
                                break;
                            }
                        case "h":
                            {
                                withBlock.X = Core.Type.AutoNw[4].X;
                                withBlock.Y = Core.Type.AutoNw[4].Y;
                                break;
                            }
                        case "i":
                            {
                                withBlock.X = Core.Type.AutoNe[1].X;
                                withBlock.Y = Core.Type.AutoNe[1].Y;
                                break;
                            }
                        case "j":
                            {
                                withBlock.X = Core.Type.AutoNe[2].X;
                                withBlock.Y = Core.Type.AutoNe[2].Y;
                                break;
                            }
                        case "k":
                            {
                                withBlock.X = Core.Type.AutoNe[3].X;
                                withBlock.Y = Core.Type.AutoNe[3].Y;
                                break;
                            }
                        case "l":
                            {
                                withBlock.X = Core.Type.AutoNe[4].X;
                                withBlock.Y = Core.Type.AutoNe[4].Y;
                                break;
                            }
                        case "m":
                            {
                                withBlock.X = Core.Type.AutoSw[1].X;
                                withBlock.Y = Core.Type.AutoSw[1].Y;
                                break;
                            }
                        case "n":
                            {
                                withBlock.X = Core.Type.AutoSw[2].X;
                                withBlock.Y = Core.Type.AutoSw[2].Y;
                                break;
                            }
                        case "o":
                            {
                                withBlock.X = Core.Type.AutoSw[3].X;
                                withBlock.Y = Core.Type.AutoSw[3].Y;
                                break;
                            }
                        case "p":
                            {
                                withBlock.X = Core.Type.AutoSw[4].X;
                                withBlock.Y = Core.Type.AutoSw[4].Y;
                                break;
                            }
                        case "q":
                            {
                                withBlock.X = Core.Type.AutoSe[1].X;
                                withBlock.Y = Core.Type.AutoSe[1].Y;
                                break;
                            }
                        case "r":
                            {
                                withBlock.X = Core.Type.AutoSe[2].X;
                                withBlock.Y = Core.Type.AutoSe[2].Y;
                                break;
                            }
                        case "s":
                            {
                                withBlock.X = Core.Type.AutoSe[3].X;
                                withBlock.Y = Core.Type.AutoSe[3].Y;
                                break;
                            }
                        case "t":
                            {
                                withBlock.X = Core.Type.AutoSe[4].X;
                                withBlock.Y = Core.Type.AutoSe[4].Y;
                                break;
                            }
                    }
                }
            }
            else
            {
                {
                    ref var withBlock1 = ref Core.Type.Autotile[x, y].Layer[layerNum].QuarterTile[tileQuarter];
                    switch (autoTileLetter ?? "")
                    {
                        case "a":
                            {
                                withBlock1.X = Core.Type.AutoIn[1].X;
                                withBlock1.Y = Core.Type.AutoIn[1].Y;
                                break;
                            }
                        case "b":
                            {
                                withBlock1.X = Core.Type.AutoIn[2].X;
                                withBlock1.Y = Core.Type.AutoIn[2].Y;
                                break;
                            }
                        case "c":
                            {
                                withBlock1.X = Core.Type.AutoIn[3].X;
                                withBlock1.Y = Core.Type.AutoIn[3].Y;
                                break;
                            }
                        case "d":
                            {
                                withBlock1.X = Core.Type.AutoIn[4].X;
                                withBlock1.Y = Core.Type.AutoIn[4].Y;
                                break;
                            }
                        case "e":
                            {
                                withBlock1.X = Core.Type.AutoNw[1].X;
                                withBlock1.Y = Core.Type.AutoNw[1].Y;
                                break;
                            }
                        case "f":
                            {
                                withBlock1.X = Core.Type.AutoNw[2].X;
                                withBlock1.Y = Core.Type.AutoNw[2].Y;
                                break;
                            }
                        case "g":
                            {
                                withBlock1.X = Core.Type.AutoNw[3].X;
                                withBlock1.Y = Core.Type.AutoNw[3].Y;
                                break;
                            }
                        case "h":
                            {
                                withBlock1.X = Core.Type.AutoNw[4].X;
                                withBlock1.Y = Core.Type.AutoNw[4].Y;
                                break;
                            }
                        case "i":
                            {
                                withBlock1.X = Core.Type.AutoNe[1].X;
                                withBlock1.Y = Core.Type.AutoNe[1].Y;
                                break;
                            }
                        case "j":
                            {
                                withBlock1.X = Core.Type.AutoNe[2].X;
                                withBlock1.Y = Core.Type.AutoNe[2].Y;
                                break;
                            }
                        case "k":
                            {
                                withBlock1.X = Core.Type.AutoNe[3].X;
                                withBlock1.Y = Core.Type.AutoNe[3].Y;
                                break;
                            }
                        case "l":
                            {
                                withBlock1.X = Core.Type.AutoNe[4].X;
                                withBlock1.Y = Core.Type.AutoNe[4].Y;
                                break;
                            }
                        case "m":
                            {
                                withBlock1.X = Core.Type.AutoSw[1].X;
                                withBlock1.Y = Core.Type.AutoSw[1].Y;
                                break;
                            }
                        case "n":
                            {
                                withBlock1.X = Core.Type.AutoSw[2].X;
                                withBlock1.Y = Core.Type.AutoSw[2].Y;
                                break;
                            }
                        case "o":
                            {
                                withBlock1.X = Core.Type.AutoSw[3].X;
                                withBlock1.Y = Core.Type.AutoSw[3].Y;
                                break;
                            }
                        case "p":
                            {
                                withBlock1.X = Core.Type.AutoSw[4].X;
                                withBlock1.Y = Core.Type.AutoSw[4].Y;
                                break;
                            }
                        case "q":
                            {
                                withBlock1.X = Core.Type.AutoSe[1].X;
                                withBlock1.Y = Core.Type.AutoSe[1].Y;
                                break;
                            }
                        case "r":
                            {
                                withBlock1.X = Core.Type.AutoSe[2].X;
                                withBlock1.Y = Core.Type.AutoSe[2].Y;
                                break;
                            }
                        case "s":
                            {
                                withBlock1.X = Core.Type.AutoSe[3].X;
                                withBlock1.Y = Core.Type.AutoSe[3].Y;
                                break;
                            }
                        case "t":
                            {
                                withBlock1.X = Core.Type.AutoSe[4].X;
                                withBlock1.Y = Core.Type.AutoSe[4].Y;
                                break;
                            }
                    }
                }
            }

        }

        internal static void InitAutotiles()
        {
            int x;
            int y;
            int layerNum;
            // Procedure used to cache autotile positions. All positioning is
            // independant from the tileset. Calculations are convoluted and annoying.
            // Maths is not my strong point. Luckily we're caching them so it's a one-off
            // thing when the map is originally loaded. As such optimisation isn't an issue.
            // For simplicity's sake we cache all subtile SOURCE positions in to an array.
            // We also give letters to each subtile for easy rendering tweaks. ;]
            // First, we need to re-size the array

            Core.Type.Autotile = new Core.Type.AutotileStruct[(Core.Type.MyMap.MaxX), (Core.Type.MyMap.MaxY)];
            var loopTo = (int)Core.Type.MyMap.MaxX;
            for (x = 0; x < loopTo; x++)
            {
                var loopTo1 = (int)Core.Type.MyMap.MaxY;
                for (y = 0; y < loopTo1; y++)
                {
                    Core.Type.Autotile[x, y].Layer = new Core.Type.QuarterTileStruct[10];
                    for (int i = 0; i < (int)Core.Enum.LayerType.Count - 1; i++)
                    {
                        Core.Type.Autotile[x, y].Layer[i].SrcX = new int[5];
                        Core.Type.Autotile[x, y].Layer[i].SrcY = new int[5];
                        Core.Type.Autotile[x, y].Layer[i].QuarterTile = new Core.Type.PointStruct[5];
                    }
                }
            }

            // Inner tiles (Top right subtile region)
            // NW - a
            Core.Type.AutoIn[1].X = 32;
            Core.Type.AutoIn[1].Y = 0;
            // NE - b
            Core.Type.AutoIn[2].X = 48;
            Core.Type.AutoIn[2].Y = 0;
            // SW - c
            Core.Type.AutoIn[3].X = 32;
            Core.Type.AutoIn[3].Y = 16;
            // SE - d
            Core.Type.AutoIn[4].X = 48;
            Core.Type.AutoIn[4].Y = 16;
            // Outer Tiles - NW (bottom subtile region)
            // NW - e
            Core.Type.AutoNw[1].X = 0;
            Core.Type.AutoNw[1].Y = 32;
            // NE - f
            Core.Type.AutoNw[2].X = 16;
            Core.Type.AutoNw[2].Y = 32;
            // SW - g
            Core.Type.AutoNw[3].X = 0;
            Core.Type.AutoNw[3].Y = 48;
            // SE - h
            Core.Type.AutoNw[4].X = 16;
            Core.Type.AutoNw[4].Y = 48;
            // Outer Tiles - NE (bottom subtile region)
            // NW - i
            Core.Type.AutoNe[1].X = 32;
            Core.Type.AutoNe[1].Y = 32;
            // NE - g
            Core.Type.AutoNe[2].X = 48;
            Core.Type.AutoNe[2].Y = 32;
            // SW - k
            Core.Type.AutoNe[3].X = 32;
            Core.Type.AutoNe[3].Y = 48;
            // SE - l
            Core.Type.AutoNe[4].X = 48;
            Core.Type.AutoNe[4].Y = 48;
            // Outer Tiles - SW (bottom subtile region)
            // NW - m
            Core.Type.AutoSw[1].X = 0;
            Core.Type.AutoSw[1].Y = 64;
            // NE - n
            Core.Type.AutoSw[2].X = 16;
            Core.Type.AutoSw[2].Y = 64;
            // SW - o
            Core.Type.AutoSw[3].X = 0;
            Core.Type.AutoSw[3].Y = 80;
            // SE - p
            Core.Type.AutoSw[4].X = 16;
            Core.Type.AutoSw[4].Y = 80;
            // Outer Tiles - SE (bottom subtile region)
            // NW - q
            Core.Type.AutoSe[1].X = 32;
            Core.Type.AutoSe[1].Y = 64;
            // NE - r
            Core.Type.AutoSe[2].X = 48;
            Core.Type.AutoSe[2].Y = 64;
            // SW - s
            Core.Type.AutoSe[3].X = 32;
            Core.Type.AutoSe[3].Y = 80;
            // SE - t
            Core.Type.AutoSe[4].X = 48;
            Core.Type.AutoSe[4].Y = 80;

            var loopTo2 = (int)Core.Type.MyMap.MaxX - 1;
            for (x = 0; x <= loopTo2; x++)
            {
                var loopTo3 = (int)Core.Type.MyMap.MaxY - 1;
                for (y = 0; y <= loopTo3; y++)
                {
                    for (layerNum = 0; layerNum < (int)Core.Enum.LayerType.Count - 1; layerNum++)
                    {
                        // calculate the subtile positions and place them
                        CalculateAutotile(x, y, layerNum);
                        // cache the rendering state of the tiles and set them
                        CacheRenderState(x, y, layerNum);
                    }
                }
            }

        }

        internal static void CacheRenderState(int x, int y, int layerNum)
        {
            int quarterNum;

            if (x < 0 | x > Core.Type.MyMap.MaxX | y < 0 | y > Core.Type.MyMap.MaxY)
                return;

            {
                ref var withBlock = ref Core.Type.MyMap.Tile[x, y];
                // check if the tile can be rendered
                if (withBlock.Layer[layerNum].Tileset <= 0 | withBlock.Layer[layerNum].Tileset > GameState.NumTileSets)
                {
                    Core.Type.Autotile[x, y].Layer[layerNum].RenderState = GameState.RenderStateNone;
                    return;
                }

                // check if it needs to be rendered as an autotile
                if (withBlock.Layer[layerNum].AutoTile == GameState.AutotileNone | withBlock.Layer[layerNum].AutoTile == GameState.AutotileFake)
                {
                    // default to... default
                    Core.Type.Autotile[x, y].Layer[layerNum].RenderState = GameState.RenderStateNormal;
                }
                else
                {
                    Core.Type.Autotile[x, y].Layer[layerNum].RenderState = GameState.RenderStateAutotile;
                    // cache tileset positioning
                    for (quarterNum = 0; quarterNum <= 4; quarterNum++)
                    {
                        Core.Type.Autotile[x, y].Layer[layerNum].SrcX[quarterNum] = Core.Type.MyMap.Tile[x, y].Layer[layerNum].X * 32 + Core.Type.Autotile[x, y].Layer[layerNum].QuarterTile[quarterNum].X;
                        Core.Type.Autotile[x, y].Layer[layerNum].SrcY[quarterNum] = Core.Type.MyMap.Tile[x, y].Layer[layerNum].Y * 32 + Core.Type.Autotile[x, y].Layer[layerNum].QuarterTile[quarterNum].Y;
                    }
                }
            }

        }

        internal static void CalculateAutotile(int x, int y, int layerNum)
        {
            // Right, so we've split the tile block in to an easy to remember
            // collection of letters. We now need to do the calculations to find
            // out which little lettered block needs to be rendered. We do this
            // by reading the surrounding tiles to check for matches.
            // First we check to make sure an autotile situation is actually there.
            // Then we calculate exactly which situation has arisen.
            // The situations are "inner", "outer", "horizontal", "vertical" and "fill".
            // Exit out if we don't have an autotile

            if (Core.Type.MyMap.Tile[x, y].Layer[layerNum].AutoTile == 0)
                return;
            // Okay, we have autotiling but which one?
            switch (Core.Type.MyMap.Tile[x, y].Layer[layerNum].AutoTile)
            {
                // Normal or animated - same difference
                case GameState.AutotileNormal:
                case GameState.AutotileAnim:
                    {
                        // North West Quarter
                        CalculateNW_Normal(layerNum, x, y);
                        // North East Quarter
                        CalculateNE_Normal(layerNum, x, y);
                        // South West Quarter
                        CalculateSW_Normal(layerNum, x, y);
                        // South East Quarter
                        CalculateSE_Normal(layerNum, x, y);
                        break;
                    }
                // Cliff
                case GameState.AutotileCliff:
                    {
                        // North West Quarter
                        CalculateNW_Cliff(layerNum, x, y);
                        // North East Quarter
                        CalculateNE_Cliff(layerNum, x, y);
                        // South West Quarter
                        CalculateSW_Cliff(layerNum, x, y);
                        // South East Quarter
                        CalculateSE_Cliff(layerNum, x, y);
                        break;
                    }
                // Waterfalls
                case GameState.AutotileWaterfall:
                    {
                        // North West Quarter
                        CalculateNW_Waterfall(layerNum, x, y);
                        // North East Quarter
                        CalculateNE_Waterfall(layerNum, x, y);
                        // South West Quarter
                        CalculateSW_Waterfall(layerNum, x, y);
                        // South East Quarter
                        CalculateSE_Waterfall(layerNum, x, y);
                        break;
                    }
                    // Anything else
            }

        }

        // Normal autotiling
        internal static void CalculateNW_Normal(int layerNum, int x, int y)
        {
            var tmpTile = new bool[4];
            var situation = default(byte);

            // North West
            if (CheckTileMatch(layerNum, x, y, x - 1, y - 1))
                tmpTile[1] = Conversions.ToBoolean(1);

            // North
            if (CheckTileMatch(layerNum, x, y, x, y - 1))
                tmpTile[2] = Conversions.ToBoolean(1);

            // West
            if (CheckTileMatch(layerNum, x, y, x - 1, y))
                tmpTile[3] = Conversions.ToBoolean(1);

            // Calculate Situation - Inner
            if (!tmpTile[2] & !tmpTile[3])
                situation = GameState.AutoInner;

            // Horizontal
            if (!tmpTile[2] & tmpTile[3])
                situation = GameState.AutoHorizontal;

            // Vertical
            if (tmpTile[2] & !tmpTile[3])
                situation = GameState.AutoVertical;

            // Outer
            if (!tmpTile[1] & tmpTile[2] & tmpTile[3])
                situation = GameState.AutoOuter;

            // Fill
            if (tmpTile[1] & tmpTile[2] & tmpTile[3])
                situation = GameState.AutoFill;

            // Actually place the subtile
            switch (situation)
            {
                case GameState.AutoInner:
                    {
                        PlaceAutotile(layerNum, x, y, 1, "e");
                        break;
                    }
                case GameState.AutoOuter:
                    {
                        PlaceAutotile(layerNum, x, y, 1, "a");
                        break;
                    }
                case GameState.AutoHorizontal:
                    {
                        PlaceAutotile(layerNum, x, y, 1, "i");
                        break;
                    }
                case GameState.AutoVertical:
                    {
                        PlaceAutotile(layerNum, x, y, 1, "m");
                        break;
                    }
                case GameState.AutoFill:
                    {
                        PlaceAutotile(layerNum, x, y, 1, "q");
                        break;
                    }
            }

        }

        internal static void CalculateNE_Normal(int layerNum, int x, int y)
        {
            var tmpTile = new bool[4];
            var situation = default(byte);

            // North
            if (CheckTileMatch(layerNum, x, y, x, y - 1))
                tmpTile[1] = Conversions.ToBoolean(1);

            // North East
            if (CheckTileMatch(layerNum, x, y, x + 1, y - 1))
                tmpTile[2] = Conversions.ToBoolean(1);

            // East
            if (CheckTileMatch(layerNum, x, y, x + 1, y))
                tmpTile[3] = Conversions.ToBoolean(1);

            // Calculate Situation - Inner
            if (!tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoInner;

            // Horizontal
            if (!tmpTile[1] & tmpTile[3])
                situation = GameState.AutoHorizontal;

            // Vertical
            if (tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoVertical;
            // Outer
            if (tmpTile[1] & !tmpTile[2] & tmpTile[3])
                situation = GameState.AutoOuter;
            // Fill
            if (tmpTile[1] & tmpTile[2] & tmpTile[3])
                situation = GameState.AutoFill;
            // Actually place the subtile
            switch (situation)
            {
                case GameState.AutoInner:
                    {
                        PlaceAutotile(layerNum, x, y, 2, "j");
                        break;
                    }
                case GameState.AutoOuter:
                    {
                        PlaceAutotile(layerNum, x, y, 2, "b");
                        break;
                    }
                case GameState.AutoHorizontal:
                    {
                        PlaceAutotile(layerNum, x, y, 2, "f");
                        break;
                    }
                case GameState.AutoVertical:
                    {
                        PlaceAutotile(layerNum, x, y, 2, "r");
                        break;
                    }
                case GameState.AutoFill:
                    {
                        PlaceAutotile(layerNum, x, y, 2, "n");
                        break;
                    }
            }

        }

        internal static void CalculateSW_Normal(int layerNum, int x, int y)
        {
            var tmpTile = new bool[4];
            var situation = default(byte);

            // West
            if (CheckTileMatch(layerNum, x, y, x - 1, y))
                tmpTile[1] = Conversions.ToBoolean(1);

            // South West
            if (CheckTileMatch(layerNum, x, y, x - 1, y + 1))
                tmpTile[2] = Conversions.ToBoolean(1);

            // South
            if (CheckTileMatch(layerNum, x, y, x, y + 1))
                tmpTile[3] = Conversions.ToBoolean(1);

            // Calculate Situation - Inner
            if (!tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoInner;

            // Horizontal
            if (tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoHorizontal;

            // Vertical
            if (!tmpTile[1] & tmpTile[3])
                situation = GameState.AutoVertical;

            // Outer
            if (tmpTile[1] & !tmpTile[2] & tmpTile[3])
                situation = GameState.AutoOuter;

            // Fill
            if (tmpTile[1] & tmpTile[2] & tmpTile[3])
                situation = GameState.AutoFill;

            // Actually place the subtile
            switch (situation)
            {
                case GameState.AutoInner:
                    {
                        PlaceAutotile(layerNum, x, y, 3, "o");
                        break;
                    }
                case GameState.AutoOuter:
                    {
                        PlaceAutotile(layerNum, x, y, 3, "c");
                        break;
                    }
                case GameState.AutoHorizontal:
                    {
                        PlaceAutotile(layerNum, x, y, 3, "s");
                        break;
                    }
                case GameState.AutoVertical:
                    {
                        PlaceAutotile(layerNum, x, y, 3, "g");
                        break;
                    }
                case GameState.AutoFill:
                    {
                        PlaceAutotile(layerNum, x, y, 3, "k");
                        break;
                    }
            }

        }

        internal static void CalculateSE_Normal(int layerNum, int x, int y)
        {
            var tmpTile = new bool[4];
            var situation = default(byte);

            // South
            if (CheckTileMatch(layerNum, x, y, x, y + 1))
                tmpTile[1] = Conversions.ToBoolean(1);

            // South East
            if (CheckTileMatch(layerNum, x, y, x + 1, y + 1))
                tmpTile[2] = Conversions.ToBoolean(1);

            // East
            if (CheckTileMatch(layerNum, x, y, x + 1, y))
                tmpTile[3] = Conversions.ToBoolean(1);

            // Calculate Situation - Inner
            if (!tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoInner;

            // Horizontal
            if (!tmpTile[1] & tmpTile[3])
                situation = GameState.AutoHorizontal;

            // Vertical
            if (tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoVertical;

            // Outer
            if (tmpTile[1] & !tmpTile[2] & tmpTile[3])
                situation = GameState.AutoOuter;

            // Fill
            if (tmpTile[1] & tmpTile[2] & tmpTile[3])
                situation = GameState.AutoFill;

            // Actually place the subtile
            switch (situation)
            {
                case GameState.AutoInner:
                    {
                        PlaceAutotile(layerNum, x, y, 4, "t");
                        break;
                    }
                case GameState.AutoOuter:
                    {
                        PlaceAutotile(layerNum, x, y, 4, "d");
                        break;
                    }
                case GameState.AutoHorizontal:
                    {
                        PlaceAutotile(layerNum, x, y, 4, "p");
                        break;
                    }
                case GameState.AutoVertical:
                    {
                        PlaceAutotile(layerNum, x, y, 4, "l");
                        break;
                    }
                case GameState.AutoFill:
                    {
                        PlaceAutotile(layerNum, x, y, 4, "h");
                        break;
                    }
            }

        }

        // Waterfall autotiling
        internal static void CalculateNW_Waterfall(int layerNum, int x, int y)
        {
            var tmpTile = default(bool);

            // West
            if (CheckTileMatch(layerNum, x, y, x - 1, y))
                tmpTile = Conversions.ToBoolean(1);
            // Actually place the subtile
            if (tmpTile)
            {
                // Extended
                PlaceAutotile(layerNum, x, y, 1, "i");
            }
            else
            {
                // Edge
                PlaceAutotile(layerNum, x, y, 1, "e");
            }

        }

        internal static void CalculateNE_Waterfall(int layerNum, int x, int y)
        {
            var tmpTile = default(bool);

            // East
            if (CheckTileMatch(layerNum, x, y, x + 1, y))
                tmpTile = Conversions.ToBoolean(1);
            // Actually place the subtile
            if (tmpTile)
            {
                // Extended
                PlaceAutotile(layerNum, x, y, 2, "f");
            }
            else
            {
                // Edge
                PlaceAutotile(layerNum, x, y, 2, "j");
            }

        }

        internal static void CalculateSW_Waterfall(int layerNum, int x, int y)
        {
            var tmpTile = default(bool);

            // West
            if (CheckTileMatch(layerNum, x, y, x - 1, y))
                tmpTile = Conversions.ToBoolean(1);
            // Actually place the subtile
            if (tmpTile)
            {
                // Extended
                PlaceAutotile(layerNum, x, y, 3, "k");
            }
            else
            {
                // Edge
                PlaceAutotile(layerNum, x, y, 3, "g");
            }

        }

        internal static void CalculateSE_Waterfall(int layerNum, int x, int y)
        {
            var tmpTile = default(bool);

            // East
            if (CheckTileMatch(layerNum, x, y, x + 1, y))
                tmpTile = Conversions.ToBoolean(1);
            // Actually place the subtile
            if (tmpTile)
            {
                // Extended
                PlaceAutotile(layerNum, x, y, 4, "h");
            }
            else
            {
                // Edge
                PlaceAutotile(layerNum, x, y, 4, "l");
            }

        }

        // Cliff autotiling
        internal static void CalculateNW_Cliff(int layerNum, int x, int y)
        {
            var tmpTile = new bool[4];
            byte situation;

            // North West
            if (CheckTileMatch(layerNum, x, y, x - 1, y - 1))
                tmpTile[1] = Conversions.ToBoolean(1);

            // North
            if (CheckTileMatch(layerNum, x, y, x, y - 1))
                tmpTile[2] = Conversions.ToBoolean(1);

            // West
            if (CheckTileMatch(layerNum, x, y, x - 1, y))
                tmpTile[3] = Conversions.ToBoolean(1);
            situation = GameState.AutoFill;

            // Calculate Situation - Horizontal
            if (!tmpTile[2] & tmpTile[3])
                situation = GameState.AutoHorizontal;

            // Vertical
            if (tmpTile[2] & !tmpTile[3])
                situation = GameState.AutoVertical;

            // Fill
            if (tmpTile[1] & tmpTile[2] & tmpTile[3])
                situation = GameState.AutoFill;

            // Inner
            if (!tmpTile[2] & !tmpTile[3])
                situation = GameState.AutoInner;

            // Actually place the subtile
            switch (situation)
            {
                case GameState.AutoInner:
                    {
                        PlaceAutotile(layerNum, x, y, 1, "e");
                        break;
                    }
                case GameState.AutoHorizontal:
                    {
                        PlaceAutotile(layerNum, x, y, 1, "i");
                        break;
                    }
                case GameState.AutoVertical:
                    {
                        PlaceAutotile(layerNum, x, y, 1, "m");
                        break;
                    }
                case GameState.AutoFill:
                    {
                        PlaceAutotile(layerNum, x, y, 1, "q");
                        break;
                    }
            }

        }

        internal static void CalculateNE_Cliff(int layerNum, int x, int y)
        {
            var tmpTile = new bool[4];
            byte situation;

            // North
            if (CheckTileMatch(layerNum, x, y, x, y - 1))
                tmpTile[1] = Conversions.ToBoolean(1);

            // North East
            if (CheckTileMatch(layerNum, x, y, x + 1, y - 1))
                tmpTile[2] = Conversions.ToBoolean(1);

            // East
            if (CheckTileMatch(layerNum, x, y, x + 1, y))
                tmpTile[3] = Conversions.ToBoolean(1);
            situation = GameState.AutoFill;

            // Calculate Situation - Horizontal
            if (!tmpTile[1] & tmpTile[3])
                situation = GameState.AutoHorizontal;

            // Vertical
            if (tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoVertical;

            // Fill
            if (tmpTile[1] & tmpTile[2] & tmpTile[3])
                situation = GameState.AutoFill;

            // Inner
            if (!tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoInner;

            // Actually place the subtile
            switch (situation)
            {
                case GameState.AutoInner:
                    {
                        PlaceAutotile(layerNum, x, y, 2, "j");
                        break;
                    }
                case GameState.AutoHorizontal:
                    {
                        PlaceAutotile(layerNum, x, y, 2, "f");
                        break;
                    }
                case GameState.AutoVertical:
                    {
                        PlaceAutotile(layerNum, x, y, 2, "r");
                        break;
                    }
                case GameState.AutoFill:
                    {
                        PlaceAutotile(layerNum, x, y, 2, "n");
                        break;
                    }
            }

        }

        internal static void CalculateSW_Cliff(int layerNum, int x, int y)
        {
            var tmpTile = new bool[4];
            byte situation;

            // West
            if (CheckTileMatch(layerNum, x, y, x - 1, y))
                tmpTile[1] = Conversions.ToBoolean(1);

            // South West
            if (CheckTileMatch(layerNum, x, y, x - 1, y + 1))
                tmpTile[2] = Conversions.ToBoolean(1);

            // South
            if (CheckTileMatch(layerNum, x, y, x, y + 1))
                tmpTile[3] = Conversions.ToBoolean(1);
            situation = GameState.AutoFill;

            // Calculate Situation - Horizontal
            if (tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoHorizontal;

            // Vertical
            if (!tmpTile[1] & tmpTile[3])
                situation = GameState.AutoVertical;

            // Fill
            if (tmpTile[1] & tmpTile[2] & tmpTile[3])
                situation = GameState.AutoFill;

            // Inner
            if (!tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoInner;
            // Actually place the subtile
            switch (situation)
            {
                case GameState.AutoInner:
                    {
                        PlaceAutotile(layerNum, x, y, 3, "o");
                        break;
                    }
                case GameState.AutoHorizontal:
                    {
                        PlaceAutotile(layerNum, x, y, 3, "s");
                        break;
                    }
                case GameState.AutoVertical:
                    {
                        PlaceAutotile(layerNum, x, y, 3, "g");
                        break;
                    }
                case GameState.AutoFill:
                    {
                        PlaceAutotile(layerNum, x, y, 3, "k");
                        break;
                    }
            }

        }

        internal static void CalculateSE_Cliff(int layerNum, int x, int y)
        {
            var tmpTile = new bool[4];
            byte situation;

            // South
            if (CheckTileMatch(layerNum, x, y, x, y + 1))
                tmpTile[1] = Conversions.ToBoolean(1);

            // South East
            if (CheckTileMatch(layerNum, x, y, x + 1, y + 1))
                tmpTile[2] = Conversions.ToBoolean(1);

            // East
            if (CheckTileMatch(layerNum, x, y, x + 1, y))
                tmpTile[3] = Conversions.ToBoolean(1);

            situation = GameState.AutoFill;
            // Calculate Situation -  Horizontal
            if (!tmpTile[1] & tmpTile[3])
                situation = GameState.AutoHorizontal;

            // Vertical
            if (tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoVertical;

            // Fill
            if (tmpTile[1] & tmpTile[2] & tmpTile[3])
                situation = GameState.AutoFill;

            // Inner
            if (!tmpTile[1] & !tmpTile[3])
                situation = GameState.AutoInner;

            // Actually place the subtile
            switch (situation)
            {
                case GameState.AutoInner:
                    {
                        PlaceAutotile(layerNum, x, y, 4, "t");
                        break;
                    }
                case GameState.AutoHorizontal:
                    {
                        PlaceAutotile(layerNum, x, y, 4, "p");
                        break;
                    }
                case GameState.AutoVertical:
                    {
                        PlaceAutotile(layerNum, x, y, 4, "l");
                        break;
                    }
                case GameState.AutoFill:
                    {
                        PlaceAutotile(layerNum, x, y, 4, "h");
                        break;
                    }
            }

        }

        internal static bool CheckTileMatch(int layerNum, int x1, int y1, int x2, int y2)
        {
            bool CheckTileMatchRet = default;
            CheckTileMatchRet = Conversions.ToBoolean(1);

            // if it's off the map then set it as autotile and exit out early
            if (x2 < 0 | x2 > Core.Type.MyMap.MaxX | y2 < 0 | y2 > Core.Type.MyMap.MaxY)
            {
                CheckTileMatchRet = Conversions.ToBoolean(1);
                return CheckTileMatchRet;
            }

            // fakes ALWAYS return true
            if (Core.Type.MyMap.Tile[x2, y2].Layer[layerNum].AutoTile == GameState.AutotileFake)
            {
                CheckTileMatchRet = Conversions.ToBoolean(1);
                return CheckTileMatchRet;
            }

            // check neighbour is an autotile
            if (Core.Type.MyMap.Tile[x2, y2].Layer[layerNum].AutoTile == 0)
            {
                CheckTileMatchRet = Conversions.ToBoolean(0);
                return CheckTileMatchRet;
            }

            // check we're a matching
            if (Core.Type.MyMap.Tile[x1, y1].Layer[layerNum].Tileset != Core.Type.MyMap.Tile[x2, y2].Layer[layerNum].Tileset)
            {
                CheckTileMatchRet = Conversions.ToBoolean(0);
                return CheckTileMatchRet;
            }

            // check tiles match
            if (Core.Type.MyMap.Tile[x1, y1].Layer[layerNum].X != Core.Type.MyMap.Tile[x2, y2].Layer[layerNum].X)
            {
                CheckTileMatchRet = Conversions.ToBoolean(0);
                return CheckTileMatchRet;
            }
            else if (Core.Type.MyMap.Tile[x1, y1].Layer[layerNum].Y != Core.Type.MyMap.Tile[x2, y2].Layer[layerNum].Y)
            {
                CheckTileMatchRet = Conversions.ToBoolean(0);
                return CheckTileMatchRet;
            }

            return CheckTileMatchRet;
        }

    }
}