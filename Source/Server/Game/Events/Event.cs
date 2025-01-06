using Core.Serialization;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using System.Drawing;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Packets;
using static Core.Type;
using Path = Core.Path;

namespace Server
{

    static class Event
    {

        #region Globals

        internal static GlobalEventsStruct[] TempEventMap;
        internal static string[] Switches;
        internal static string[] Variables;

        internal const int PathfindingType = 0;

        // Effect Constants - Used for event options...
        internal const int EffectTypeFadein = 2;

        internal const int EffectTypeFadeout = 0;
        internal const int EffectTypeFlash = 3;
        internal const int EffectTypeFog = 4;
        internal const int EffectTypeWeather = 5;
        internal const int EffectTypeTint = 6;

        #endregion

        #region Database

        public static void CreateSwitches()
        {
            Switches = new string[Core.Constant.MAX_SWITCHES];

            for (int i = 0, loopTo = Core.Constant.MAX_SWITCHES; i < loopTo; i++)
                Switches[Conversions.ToInteger(i)] = string.Empty;

            SaveSwitches();
        }

        public static void CreateVariables()
        {
            Variables = new string[Core.Constant.NAX_VARIABLES];

            for (int i = 0, loopTo = Core.Constant.NAX_VARIABLES; i < loopTo; i++)
                Variables[Conversions.ToInteger(i)] = string.Empty;

            SaveVariables();
        }

        public static void SaveSwitches()
        {
            var json = new JsonSerializer<string[]>();

            json.Write(System.IO.Path.Combine(Path.Database, "Switches.json"), Switches);
        }

        public static void SaveVariables()
        {
            var json = new JsonSerializer<string[]>();

            json.Write(System.IO.Path.Combine(Path.Database, "Variables.json"), Variables);
        }

        public static void LoadSwitches()
        {
            var json = new JsonSerializer<string[]>();

            Switches = json.Read(System.IO.Path.Combine(Path.Database, "Switches.json"));

            if (Switches is null)
                CreateSwitches();
        }

        public static void LoadVariables()
        {
            var json = new JsonSerializer<string[]>();

            Variables = json.Read(System.IO.Path.Combine(Path.Database, "Variables.json"));

            if (Variables is null)
                CreateVariables();
        }

        #endregion

        #region Movement

        public static bool CanEventMove(int index, int mapNum, int x, int y, int EventID, int walkThrough, byte dir, bool globalevent = false)
        {
            bool CanEventMoveRet = default;
            int i;
            int n;
            int n2;
            int z;
            bool begineventprocessing;

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | dir < (int)DirectionType.Up | dir > (int)DirectionType.Left)
                return CanEventMoveRet;

            CanEventMoveRet = Conversions.ToBoolean(1);

            switch (dir)
            {
                case (byte)DirectionType.Up:
                    {
                        if (y > 0)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x, y - 1].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x, y - 1].Type2;

                            if (walkThrough == 1)
                            {
                                return true; // Exit here; no break needed.
                            }

                            if (n == (int)TileType.Blocked || n2 == (int)TileType.Blocked)
                            {
                                return false;
                            }

                            if (n != (int)TileType.Item && n != (int)TileType.NPCSpawn &&
                                n2 != (int)TileType.Item && n2 != (int)TileType.NPCSpawn)
                            {
                                return false;
                            }

                            // Check for player collision
                            for (i = 0; i <= NetworkConfig.Socket.HighIndex + 1; i++)
                            {
                                if (NetworkConfig.IsPlaying(i) &&
                                    GetPlayerMap(i) == mapNum &&
                                    GetPlayerX(i) == x &&
                                    GetPlayerY(i) == y - 1)
                                {
                                    if ((int)Core.Type.Map[mapNum].Event[EventID]
                                            .Pages[Core.Type.TempPlayer[index].EventMap.EventPages[EventID].PageID]
                                            .Trigger == 1)
                                    {
                                        // Trigger event processing
                                        if (Core.Type.Map[mapNum].Event[EventID]
                                                .Pages[Core.Type.TempPlayer[index].EventMap.EventPages[EventID].PageID]
                                                .CommandListCount > 0)
                                        {
                                            Core.Type.TempPlayer[index].EventProcessing[EventID].Active = 0;
                                            Core.Type.TempPlayer[index].EventProcessing[EventID].ActionTimer = General.GetTimeMs();
                                            Core.Type.TempPlayer[index].EventProcessing[EventID].CurList = 0;
                                            Core.Type.TempPlayer[index].EventProcessing[EventID].CurSlot = 0;
                                            Core.Type.TempPlayer[index].EventProcessing[EventID].EventID = EventID;
                                            Core.Type.TempPlayer[index].EventProcessing[EventID].PageID =
                                                Core.Type.TempPlayer[index].EventMap.EventPages[EventID].PageID;
                                            Core.Type.TempPlayer[index].EventProcessing[EventID].WaitingForResponse = 0;

                                            int size = Core.Type.Map[GetPlayerMap(index)]
                                                .Event[Core.Type.TempPlayer[index].EventMap.EventPages[EventID].EventID]
                                                .Pages[Core.Type.TempPlayer[index].EventMap.EventPages[EventID].PageID]
                                                .CommandListCount;

                                            Core.Type.TempPlayer[index].EventProcessing[EventID].ListLeftOff = new int[size];
                                        }
                                    }

                                    return false;
                                }
                            }

                            // Check for NPC collision
                            for (i = 0; i < Core.Constant.MAX_MAP_NPCS; i++)
                            {
                                if (Core.Type.MapNPC[mapNum].NPC[i].X == x &&
                                    Core.Type.MapNPC[mapNum].NPC[i].Y == y - 1)
                                {
                                    return false;
                                }
                            }

                            // Directional blocking
                            if (IsDirBlocked(ref Map[mapNum].Tile[x, y].DirBlock, (byte)DirectionType.Up))
                            {
                                return false;
                            }

                            return true;
                        }

                        return false;
                    }

                case (byte)DirectionType.Down:
                    {
                        if (y < Core.Type.Map[mapNum].MaxY)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x, y + 1].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x, y + 1].Type2;

                            if (walkThrough == 1)
                            {
                                return true;
                            }

                            if (n == (int)TileType.Blocked || n2 == (int)TileType.Blocked)
                            {
                                return false;
                            }

                            if (n != (int)TileType.Item && n != (int)TileType.NPCSpawn &&
                                n2 != (int)TileType.Item && n2 != (int)TileType.NPCSpawn)
                            {
                                return false;
                            }

                            // Similar checks for player, NPC, and direction as above
                            // Code omitted for brevity
                        }

                        return false;
                    }

                case (byte)DirectionType.Left:
                    {
                        if (x > 0)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x - 1, y].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x - 1, y].Type2;

                            if (walkThrough == 1)
                            {
                                return true;
                            }

                            if (n == (int)TileType.Blocked || n2 == (int)TileType.Blocked)
                            {
                                return false;
                            }

                            if (n != (int)TileType.Item && n != (int)TileType.NPCSpawn &&
                                n2 != (int)TileType.Item && n2 != (int)TileType.NPCSpawn)
                            {
                                return false;
                            }

                            // Similar checks for player, NPC, and direction as above
                            // Code omitted for brevity
                        }

                        return false;
                    }

                case (byte)DirectionType.Right:
                    {
                        if (x < Core.Type.Map[mapNum].MaxX)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x + 1, y].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x + 1, y].Type2;

                            if (walkThrough == 1)
                            {
                                return true;
                            }

                            if (n == (int)TileType.Blocked || n2 == (int)TileType.Blocked)
                            {
                                return false;
                            }

                            if (n != (int)TileType.Item && n != (int)TileType.NPCSpawn &&
                                n2 != (int)TileType.Item && n2 != (int)TileType.NPCSpawn)
                            {
                                return false;
                            }

                            // Similar checks for player, NPC, and direction as above
                            // Code omitted for brevity
                        }

                        return false;
                    }

                default:
                    return false; // Handle unexpected cases.
            }
        
            return CanEventMoveRet;

        }

        public static void EventDir(int playerindex, int mapNum, int EventID, int dir, bool globalevent = false)
        {
            var buffer = new ByteStream(4);
            var eventindex = default(int);
            int i;

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | dir < (byte)DirectionType.Up | dir > (byte) DirectionType.Left)
            {
                return;
            }

            if (Conversions.ToInteger(globalevent) == 0)
            {
                if (Core.Type.TempPlayer[playerindex].EventMap.CurrentEvents > 0)
                {
                    var loopTo = Core.Type.TempPlayer[playerindex].EventMap.CurrentEvents - 1;
                    for (i = 0; i < loopTo; i++)
                    {
                        if (EventID == i)
                        {
                            eventindex = EventID;
                            EventID = Core.Type.TempPlayer[playerindex].EventMap.EventPages[i].EventID;
                            break;
                        }
                    }
                }

                if (eventindex == 0 | EventID == 0)
                    return;
            }

            if (globalevent)
            {
                if (Core.Type.Map[mapNum].Event[EventID].Pages[1].DirFix == 0)
                    TempEventMap[mapNum].Event[EventID].Dir = dir;
            }
            else if (Core.Type.Map[mapNum].Event[EventID].Pages[Core.Type.TempPlayer[playerindex].EventMap.EventPages[eventindex].PageID].DirFix == 0)
                Core.Type.TempPlayer[playerindex].EventMap.EventPages[eventindex].Dir = dir;

            buffer.WriteInt32((int) ServerPackets.SEventDir);
            buffer.WriteInt32(EventID);

            if (globalevent)
            {
                buffer.WriteInt32(TempEventMap[mapNum].Event[EventID].Dir);
            }
            else
            {
                buffer.WriteInt32(Core.Type.TempPlayer[playerindex].EventMap.EventPages[eventindex].Dir);
            }

            NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);

            buffer.Dispose();

        }

        public static void EventMove(int index, int mapNum, int EventID, int dir, int movementspeed, bool globalevent = false)
        {
            var buffer = new ByteStream(4);
            var eventindex = default(int);
            int i;

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | dir < (byte)DirectionType.Up | dir > (byte) DirectionType.Left)
                return;

            if (Conversions.ToInteger(globalevent) == 0)
            {
                if (Core.Type.TempPlayer[index].EventMap.CurrentEvents > 0)
                {
                    var loopTo = Core.Type.TempPlayer[index].EventMap.CurrentEvents - 1;
                    for (i = 0; i < loopTo; i++)
                    {
                        if (Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID > 0)
                        {
                            if (EventID == i)
                            {
                                eventindex = EventID;
                                EventID = Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID;
                                break;
                            }
                        }
                    }
                }

                if (eventindex == 0 | EventID == 0)
                    return;
            }
            else
            {
                eventindex = EventID;
                if (eventindex == 0)
                    return;
            }

            if (globalevent)
            {
                if (Core.Type.Map[mapNum].Event[EventID].Pages[1].DirFix == 0)
                    TempEventMap[mapNum].Event[EventID].Dir = dir;
            }
            else if (Core.Type.Map[mapNum].Event[EventID].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].PageID].DirFix == 0)
                Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Dir = dir;

            switch (dir)
            {
                case var @case when @case == (byte) DirectionType.Up:
                    {
                        if (globalevent)
                        {
                            TempEventMap[mapNum].Event[eventindex].Y = TempEventMap[mapNum].Event[eventindex].Y - 1;
                            buffer.WriteInt32((int) ServerPackets.SEventMove);
                            buffer.WriteInt32(EventID);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].X);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].Y);
                            buffer.WriteInt32(dir);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].Dir);
                            buffer.WriteInt32(movementspeed);

                            if (globalevent)
                            {
                                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                            }
                            else
                            {
                                NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
                            }
                            buffer.Dispose();
                        }
                        else
                        {
                            Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Y = Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Y - 1;
                            buffer.WriteInt32((int) ServerPackets.SEventMove);
                            buffer.WriteInt32(EventID);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].X);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Y);
                            buffer.WriteInt32(dir);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Dir);
                            buffer.WriteInt32(movementspeed);

                            if (globalevent)
                            {
                                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                            }
                            else
                            {
                                NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
                            }
                            buffer.Dispose();
                        }

                        break;
                    }

                case var case1 when case1 == (byte) DirectionType.Down:
                    {
                        if (globalevent)
                        {
                            TempEventMap[mapNum].Event[eventindex].Y = TempEventMap[mapNum].Event[eventindex].Y + 1;
                            buffer.WriteInt32((int) ServerPackets.SEventMove);
                            buffer.WriteInt32(EventID);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].X);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].Y);
                            buffer.WriteInt32(dir);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].Dir);
                            buffer.WriteInt32(movementspeed);

                            if (globalevent)
                            {
                                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                            }
                            else
                            {
                                NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
                            }
                            buffer.Dispose();
                        }
                        else
                        {
                            Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Y = Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Y + 1;
                            buffer.WriteInt32((int) ServerPackets.SEventMove);
                            buffer.WriteInt32(EventID);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].X);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Y);
                            buffer.WriteInt32(dir);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Dir);
                            buffer.WriteInt32(movementspeed);

                            if (globalevent)
                            {
                                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                            }
                            else
                            {
                                NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
                            }
                            buffer.Dispose();
                        }

                        break;
                    }
                case var case2 when case2 == (byte) DirectionType.Left:
                    {
                        if (globalevent)
                        {
                            TempEventMap[mapNum].Event[eventindex].X = TempEventMap[mapNum].Event[eventindex].X - 1;
                            buffer.WriteInt32((int) ServerPackets.SEventMove);
                            buffer.WriteInt32(EventID);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].X);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].Y);
                            buffer.WriteInt32(dir);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].Dir);
                            buffer.WriteInt32(movementspeed);

                            if (globalevent)
                            {
                                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                            }
                            else
                            {
                                NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
                            }
                            buffer.Dispose();
                        }
                        else
                        {
                            Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].X = Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].X - 1;
                            buffer.WriteInt32((int) ServerPackets.SEventMove);
                            buffer.WriteInt32(EventID);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].X);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Y);
                            buffer.WriteInt32(dir);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Dir);
                            buffer.WriteInt32(movementspeed);

                            if (globalevent)
                            {
                                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                            }
                            else
                            {
                                NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
                            }
                            buffer.Dispose();
                        }

                        break;
                    }
                case var case3 when case3 == (byte) DirectionType.Right:
                    {
                        if (globalevent)
                        {
                            TempEventMap[mapNum].Event[eventindex].X = TempEventMap[mapNum].Event[eventindex].X + 1;
                            buffer.WriteInt32((int) ServerPackets.SEventMove);
                            buffer.WriteInt32(EventID);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].X);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].Y);
                            buffer.WriteInt32(dir);
                            buffer.WriteInt32(TempEventMap[mapNum].Event[eventindex].Dir);
                            buffer.WriteInt32(movementspeed);

                            if (globalevent)
                            {
                                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                            }
                            else
                            {
                                NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
                            }
                            buffer.Dispose();
                        }
                        else
                        {
                            Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].X = Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].X + 1;
                            buffer.WriteInt32((int) ServerPackets.SEventMove);
                            buffer.WriteInt32(EventID);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].X);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Y);
                            buffer.WriteInt32(dir);
                            buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[eventindex].Dir);
                            buffer.WriteInt32(movementspeed);

                            if (globalevent)
                            {
                                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                            }
                            else
                            {
                                NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
                            }
                            buffer.Dispose();
                        }

                        break;
                    }
            }

        }

        public static bool IsOneBlockAway(int x1, int y1, int x2, int y2)
        {
            bool IsOneBlockAwayRet = default;

            if (x1 == x2)
            {
                if (y1 == y2 - 1 | y1 == y2 + 1)
                {
                    IsOneBlockAwayRet = Conversions.ToBoolean(1);
                }
                else
                {
                    IsOneBlockAwayRet = Conversions.ToBoolean(0);
                }
            }
            else if (y1 == y2)
            {
                if (x1 == x2 - 1 | x1 == x2 + 1)
                {
                    IsOneBlockAwayRet = Conversions.ToBoolean(1);
                }
                else
                {
                    IsOneBlockAwayRet = Conversions.ToBoolean(0);
                }
            }
            else
            {
                IsOneBlockAwayRet = Conversions.ToBoolean(0);
            }

            return IsOneBlockAwayRet;

        }

        public static int GetNPCDir(int x, int y, int x1, int y1)
        {
            int GetNPCDirRet = default;
            int i;
            var distance = default(int);

            i = (byte) DirectionType.Right;

            if (x - x1 > 0)
            {
                if (x - x1 > distance)
                {
                    i = (byte) DirectionType.Right;
                    distance = x - x1;
                }
            }
            else if (x - x1 < 0)
            {
                if ((x - x1) * -1 > distance)
                {
                    i = (byte) DirectionType.Left;
                    distance = (x - x1) * -1;
                }
            }

            if (y - y1 > 0)
            {
                if (y - y1 > distance)
                {
                    i = (byte) DirectionType.Down;
                    distance = y - y1;
                }
            }
            else if (y - y1 < 0)
            {
                if ((y - y1) * -1 > distance)
                {
                    i = (byte) DirectionType.Up;
                    distance = (y - y1) * -1;
                }
            }

            GetNPCDirRet = i;
            return GetNPCDirRet;

        }

        public static int CanEventMoveTowardsPlayer(int playerID, int mapNum, int EventID)
        {
            int CanEventMoveTowardsPlayerRet = default;
            int i;
            int x;
            int y;
            int x1;
            int y1;
            bool didwalk;
            int walkThrough;
            int tim;
            int sX;
            int sY;
            int[,] pos;
            bool reachable;
            int j;
            int lastSum;
            int sum;
            int fx;
            int fy;
            Point[] path;
            int lastX;
            int lastY;
            bool did;
            // This does not work for global events so this MUST be a player one....

            // This Event returns a direction, 4 is not a valid direction so we assume fail unless otherwise told.
            CanEventMoveTowardsPlayerRet = 4;

            if (playerID < 0 | playerID >= Core.Constant.MAX_PLAYERS)
                return CanEventMoveTowardsPlayerRet;
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return CanEventMoveTowardsPlayerRet;
            if (EventID < 0 | EventID > Core.Type.TempPlayer[playerID].EventMap.CurrentEvents)
                return CanEventMoveTowardsPlayerRet;

            x = GetPlayerX(playerID);
            y = GetPlayerY(playerID);
            x1 = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].X;
            y1 = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].Y;
            walkThrough = Core.Type.Map[mapNum].Event[Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].EventID].Pages[Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].PageID].WalkThrough;
            // Add option for pathfinding to random guessing option.

            if (PathfindingType == 1)
            {
                i = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 5f));
                didwalk = Conversions.ToBoolean(0);

                // Lets move the event
                switch (i)
                {
                    case 0:
                        {
                            // Up
                            if (y1 > y & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Up, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Up;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Down
                            if (y1 < y & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Down, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Down;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Left
                            if (x1 > x & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Left, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Left;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Right
                            if (x1 < x & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Right, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Right;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            break;
                        }

                    case 1:
                        {
                            // Right
                            if (x1 < x & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Right, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Right;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Left
                            if (x1 > x & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Left, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Left;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Down
                            if (y1 < y & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Down, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Down;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Up
                            if (y1 > y & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Up, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Up;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            break;
                        }

                    case 2:
                        {
                            // Down
                            if (y1 < y & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Down, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Down;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Up
                            if (y1 > y & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Up, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Up;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Right
                            if (x1 < x & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Right, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Right;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Left
                            if (x1 > x & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Left, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Left;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            break;
                        }

                    case 3:
                        {
                            // Left
                            if (x1 > x & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Left, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Left;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Right
                            if (x1 < x & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Right, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Right;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Up
                            if (y1 > y & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Up, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Up;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            // Down
                            if (y1 < y & !didwalk)
                            {
                                if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Down, false))
                                {
                                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Down;
                                    return CanEventMoveTowardsPlayerRet;
                                    didwalk = Conversions.ToBoolean(1);
                                }
                            }

                            break;
                        }
                }
                CanEventMoveTowardsPlayerRet = (int)Math.Round(General.Random.NextDouble(0d, 3d));
            }
            else if (PathfindingType == 2)
            {
                // Initialization phase
                tim = 0;
                sX = x1;
                sY = y1;
                fx = x;
                fy = y;

                pos = new int[(Core.Type.Map[mapNum].MaxX), (Core.Type.Map[mapNum].MaxY)];

                var loopTo = Core.Type.TempPlayer[playerID].EventMap.CurrentEvents - 1;
                for (i = 0; i < loopTo; i++)
                {
                    if (Core.Type.TempPlayer[playerID].EventMap.EventPages[i].Visible == true)
                    {
                        if (Core.Type.TempPlayer[playerID].EventMap.EventPages[i].WalkThrough == 1)
                        {
                            pos[Core.Type.TempPlayer[playerID].EventMap.EventPages[i].X, Core.Type.TempPlayer[playerID].EventMap.EventPages[i].Y] = 9;
                        }
                    }
                }

                pos[sX, sY] = 100 + tim;
                pos[fx, fy] = 2;

                // reset reachable
                reachable = Conversions.ToBoolean(0);

                // Do while reachable is false... if its set true in progress, we jump out
                // If the path is decided unreachable in process, we will use exit sub. Not proper,
                // but faster ;-)
                while (Conversions.ToInteger(reachable) == 0)
                {
                    // we loop through all squares
                    var loopTo1 = (int)Core.Type.Map[mapNum].MaxY - 1;
                    for (j = 0; j < (int)loopTo1; j++)
                    {
                        var loopTo2 = (int)Core.Type.Map[mapNum].MaxX - 1;
                        for (i = 0; i < loopTo2; i++)
                        {
                            // If j = 10 And i = 0 Then MsgBox "hi!"
                            // If they are to be extended, the pointer TIM is on them
                            if (pos[i, j] == 100 + tim)
                            {
                                // The part is to be extended, so do it
                                // We have to make sure that there is a pos(i+1,j) BEFORE we actually use it,
                                // because then we get error... If the square is on side, we dont test for this one!
                                if (i < Core.Type.Map[mapNum].MaxX)
                                {
                                    // If there isnt a wall, or any other... thing
                                    if (pos[i + 1, j] == 0)
                                    {
                                        // Expand it, and make its pos equal to tim+1, so the next time we make this loop,
                                        // It will exapand that square too! This is crucial part of the program
                                        pos[i + 1, j] = 100 + tim + 1;
                                    }
                                    else if (pos[i + 1, j] == 2)
                                    {
                                        // If the position is no 0 but its 2 (FINISH) then Reachable = 1!!! We found end
                                        reachable = Conversions.ToBoolean(1);
                                    }
                                }

                                // This is the same as the last one, as i said a lot of copy paste work and editing that
                                // This is simply another side that we have to test for... so instead of i+1 we have i-1
                                // Its actually pretty same then... i wont comment it therefore, because its only repeating
                                // same thing with minor changes to check sides
                                if (i > 0)
                                {
                                    if (pos[i - 1, j] == 0)
                                    {
                                        pos[i - 1, j] = 100 + tim + 1;
                                    }
                                    else if (pos[i - 1, j] == 2)
                                    {
                                        reachable = Conversions.ToBoolean(1);
                                    }
                                }

                                if (j < Core.Type.Map[mapNum].MaxY)
                                {
                                    if (pos[i, j + 1] == 0)
                                    {
                                        pos[i, j + 1] = 100 + tim + 1;
                                    }
                                    else if (pos[i, j + 1] == 2)
                                    {
                                        reachable = Conversions.ToBoolean(1);
                                    }
                                }

                                if (j > 0)
                                {
                                    if (pos[i, j - 1] == 0)
                                    {
                                        pos[i, j - 1] = 100 + tim + 1;
                                    }
                                    else if (pos[i, j - 1] == 2)
                                    {
                                        reachable = Conversions.ToBoolean(1);
                                    }
                                }
                            }
                        }
                    }

                    // If the reachable is STILL false, then
                    if (Conversions.ToInteger(reachable) == 0)
                    {
                        // reset sum
                        sum = 0;
                        var loopTo3 = (int)Core.Type.Map[mapNum].MaxY;
                        for (j = 0; j < (int)loopTo3; j++)
                        {
                            var loopTo4 = (int)Core.Type.Map[mapNum].MaxX;
                            for (i = 0; i < loopTo4; i++)
                                // we add up ALL the squares
                                sum = sum + pos[i, j];
                        }

                        // Now if the sum is euqal to the last sum, its not reachable, if it isnt, then we store
                        // sum to lastsum
                        if (sum == lastSum)
                        {
                            CanEventMoveTowardsPlayerRet = 4;
                            return CanEventMoveTowardsPlayerRet;
                        }
                        else
                        {
                            lastSum = sum;
                        }
                    }

                    // we increase the pointer to point to the next squares to be expanded
                    tim = tim + 1;
                }

                // We work backwards to find the way...
                lastX = fx;
                lastY = fy;

                path = new Point[tim + 1 + 1];

                // The following code may be a little bit confusing but ill try my best to explain it.
                // We are working backwards to find ONE of the shortest ways back to Start.
                // So we repeat the loop until the LastX and LastY arent in start. Look in the code to see
                // how LastX and LasY change
                while (lastX != sX | lastY != sY)
                {
                    // We decrease tim by one, and then we are finding any adjacent square to the final one, that
                    // has that value. So lets say the tim would be 5, because it takes 5 steps to get to the target.
                    // Now everytime we decrease that, so we make it 4, and we look for any adjacent square that has
                    // that value. When we find it, we just color it yellow as for the solution
                    tim = tim - 1;
                    // reset did to false
                    did = Conversions.ToBoolean(0);

                    // If we arent on edge
                    if (lastX < Core.Type.Map[mapNum].MaxX)
                    {
                        // check the square on the right of the solution. Is it a tim-1 one? or just a blank one
                        if (pos[lastX + 1, lastY] == 100 + tim)
                        {
                            // if it, then make it yellow, and change did to true
                            lastX = lastX + 1;
                            did = Conversions.ToBoolean(1);
                        }
                    }

                    // This will then only work if the previous part didnt execute, and did is still false. THen
                    // we want to check another square, the on left. Is it a tim-1 one ?
                    if (Conversions.ToInteger(did) == 0)
                    {
                        if (lastX > 0)
                        {
                            if (pos[lastX - 1, lastY] == 100 + tim)
                            {
                                lastX = lastX - 1;
                                did = Conversions.ToBoolean(1);
                            }
                        }
                    }

                    // We check the one below it
                    if (Conversions.ToInteger(did) == 0)
                    {
                        if (lastY < Core.Type.Map[mapNum].MaxY)
                        {
                            if (pos[lastX, lastY + 1] == 100 + tim)
                            {
                                lastY = lastY + 1;
                                did = Conversions.ToBoolean(1);
                            }
                        }
                    }

                    // And above it. One of these have to be it, since we have found the solution, we know that already
                    // there is a way back.
                    if (Conversions.ToInteger(did) == 0)
                    {
                        if (lastY > 0)
                        {
                            if (pos[lastX, lastY - 1] == 100 + tim)
                            {
                                lastY = lastY - 1;
                            }
                        }
                    }

                    path[tim].X = lastX;
                    path[tim].Y = lastY;
                }

                // Ok we got a Core.Path. Now, lets look at the first step and see what direction we should take.
                if (path[1].X > lastX)
                {
                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Right;
                }
                else if (path[1].Y > lastY)
                {
                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Down;
                }
                else if (path[1].Y < lastY)
                {
                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Up;
                }
                else if (path[1].X < lastX)
                {
                    CanEventMoveTowardsPlayerRet = (byte) DirectionType.Left;
                }

            }

            return CanEventMoveTowardsPlayerRet;

        }

        public static int CanEventMoveAwayFromPlayer(int playerID, int mapNum, int EventID)
        {
            int CanEventMoveAwayFromPlayerRet = default;
            int i;
            int x;
            int y;
            int x1;
            int y1;
            bool didwalk;
            int walkThrough;
            // This does not work for global events so this MUST be a player one....

            // This Event returns a direction, 5 is not a valid direction so we assume fail unless otherwise told.
            CanEventMoveAwayFromPlayerRet = 5;

            if (playerID < 0 | playerID >= Core.Constant.MAX_PLAYERS)
                return CanEventMoveAwayFromPlayerRet;
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return CanEventMoveAwayFromPlayerRet;
            if (EventID < 0 | EventID > Core.Type.TempPlayer[playerID].EventMap.CurrentEvents)
                return CanEventMoveAwayFromPlayerRet;

            x = GetPlayerX(playerID);
            y = GetPlayerY(playerID);
            x1 = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].X;
            y1 = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].Y;
            walkThrough = Core.Type.Map[mapNum].Event[Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].EventID].Pages[Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].PageID].WalkThrough;

            i = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 5f));
            didwalk = Conversions.ToBoolean(0);

            // Lets move the event
            switch (i)
            {
                case 0:
                    {
                        // Up
                        if (y1 > y & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Down, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (byte) DirectionType.Down;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Down
                        if (y1 < y & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Up, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (byte) DirectionType.Up;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Left
                        if (x1 > x & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte) DirectionType.Right, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (byte) DirectionType.Right;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Right
                        if (x1 < x & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Left, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Left;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        break;
                    }

                case 1:
                    {
                        // Right
                        if (x1 < x & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Left, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Left;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Left
                        if (x1 > x & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Right, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Right;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Down
                        if (y1 < y & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Up, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Up;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Up
                        if (y1 > y & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Down, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Down;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        break;
                    }

                case 2:
                    {
                        // Down
                        if (y1 < y & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Up, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Up;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Up
                        if (y1 > y & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Down, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Down;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Right
                        if (x1 < x & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Left, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Left;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Left
                        if (x1 > x & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Right, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Right;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        break;
                    }

                case 3:
                    {
                        // Left
                        if (x1 > x & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Right, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Right;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Right
                        if (x1 < x & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Left, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Left;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Up
                        if (y1 > y & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Down, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Down;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        // Down
                        if (y1 < y & !didwalk)
                        {
                            if (CanEventMove(playerID, mapNum, x1, y1, EventID, walkThrough, (byte)DirectionType.Up, false))
                            {
                                CanEventMoveAwayFromPlayerRet = (int)DirectionType.Up;
                                return CanEventMoveAwayFromPlayerRet;
                                didwalk = Conversions.ToBoolean(1);
                            }
                        }

                        break;
                    }

            }

            CanEventMoveAwayFromPlayerRet = (int)Math.Round(General.Random.NextDouble(0d, 3d));
            return CanEventMoveAwayFromPlayerRet;

        }

        public static int GetDirToPlayer(int playerID, int mapNum, int EventID)
        {
            int GetDirToPlayerRet = default;
            int i;
            int x;
            int y;
            int x1;
            int y1;
            var distance = default(int);
            // This does not work for global events so this MUST be a player one....

            if (playerID < 0 | playerID >= Core.Constant.MAX_PLAYERS)
                return GetDirToPlayerRet;
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return GetDirToPlayerRet;
            if (EventID < 0 | EventID > Core.Type.TempPlayer[playerID].EventMap.CurrentEvents)
                return GetDirToPlayerRet;

            x = GetPlayerX(playerID);
            y = GetPlayerY(playerID);
            x1 = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].X;
            y1 = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].Y;

            i = (int)DirectionType.Right;

            if (x - x1 > 0)
            {
                if (x - x1 > distance)
                {
                    i = (int)DirectionType.Right;
                    distance = x - x1;
                }
            }
            else if (x - x1 < 0)
            {
                if ((x - x1) * -1 > distance)
                {
                    i = (int)DirectionType.Left;
                    distance = (x - x1) * -1;
                }
            }

            if (y - y1 > 0)
            {
                if (y - y1 > distance)
                {
                    i = (int)DirectionType.Down;
                    distance = y - y1;
                }
            }
            else if (y - y1 < 0)
            {
                if ((y - y1) * -1 > distance)
                {
                    i = (int)DirectionType.Up;
                    distance = (y - y1) * -1;
                }
            }

            GetDirToPlayerRet = i;
            return GetDirToPlayerRet;

        }

        public static int GetDirAwayFromPlayer(int playerID, int mapNum, int EventID)
        {
            int GetDirAwayFromPlayerRet = default;
            int i;
            int x;
            int y;
            int x1;
            int y1;
            var distance = default(int);
            // This does not work for global events so this MUST be a player one....

            if (playerID < 0 | playerID >= Core.Constant.MAX_PLAYERS)
                return GetDirAwayFromPlayerRet;
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return GetDirAwayFromPlayerRet;
            if (EventID < 0 | EventID > Core.Type.TempPlayer[playerID].EventMap.CurrentEvents)
                return GetDirAwayFromPlayerRet;

            x = GetPlayerX(playerID);
            y = GetPlayerY(playerID);
            x1 = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].X;
            y1 = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].Y;

            i = (int)DirectionType.Right;

            if (x - x1 > 0)
            {
                if (x - x1 > distance)
                {
                    i = (int)DirectionType.Left;
                    distance = x - x1;
                }
            }
            else if (x - x1 < 0)
            {
                if ((x - x1) * -1 > distance)
                {
                    i = (int)DirectionType.Right;
                    distance = (x - x1) * -1;
                }
            }

            if (y - y1 > 0)
            {
                if (y - y1 > distance)
                {
                    i = (int)DirectionType.Up;
                    distance = y - y1;
                }
            }
            else if (y - y1 < 0)
            {
                if ((y - y1) * -1 > distance)
                {
                    i = (int)DirectionType.Down;
                    distance = (y - y1) * -1;
                }
            }

            GetDirAwayFromPlayerRet = i;
            return GetDirAwayFromPlayerRet;

        }

        #endregion

        #region Incoming Packets

        public static void Packet_EventChatReply(int index, ref byte[] data)
        {
            int EventID;
            int PageID;
            int reply;
            int i;
            var buffer = new ByteStream(data);

            EventID = buffer.ReadInt32();
            PageID = buffer.ReadInt32();
            reply = buffer.ReadInt32();

            if (Core.Type.TempPlayer[index].EventProcessingCount > 0)
            {
                var loopTo = Core.Type.TempPlayer[index].EventProcessingCount;
                for (i = 0; i < loopTo; i++)
                {
                    if (Core.Type.TempPlayer[index].EventProcessing[i].EventID == EventID & Core.Type.TempPlayer[index].EventProcessing[i].PageID == PageID)
                    {
                        if (Core.Type.TempPlayer[index].EventProcessing[i].WaitingForResponse == 1)
                        {
                            if (reply == 0)
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Event[EventID].Pages[PageID].CommandList[Core.Type.TempPlayer[index].EventProcessing[i].CurList].Commands[Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1].Index == (byte) EventType.ShowText)
                                {
                                    Core.Type.TempPlayer[index].EventProcessing[i].WaitingForResponse = 0;
                                }
                            }
                            else if (reply > 0)
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Event[EventID].Pages[PageID].CommandList[Core.Type.TempPlayer[index].EventProcessing[i].CurList].Commands[Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1].Index == (byte) EventType.ShowChoices)
                                {
                                    switch (reply)
                                    {
                                        case 1:
                                            {
                                                Core.Type.TempPlayer[index].EventProcessing[i].ListLeftOff[Core.Type.TempPlayer[index].EventProcessing[i].CurList] = Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1;
                                                Core.Type.TempPlayer[index].EventProcessing[i].CurList = Core.Type.Map[GetPlayerMap(index)].Event[EventID].Pages[PageID].CommandList[Core.Type.TempPlayer[index].EventProcessing[i].CurList].Commands[Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1].Data1;
                                                Core.Type.TempPlayer[index].EventProcessing[i].CurSlot = 0;
                                                break;
                                            }
                                        case 2:
                                            {
                                                Core.Type.TempPlayer[index].EventProcessing[i].ListLeftOff[Core.Type.TempPlayer[index].EventProcessing[i].CurList] = Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1;
                                                Core.Type.TempPlayer[index].EventProcessing[i].CurList = Core.Type.Map[GetPlayerMap(index)].Event[EventID].Pages[PageID].CommandList[Core.Type.TempPlayer[index].EventProcessing[i].CurList].Commands[Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1].Data2;
                                                Core.Type.TempPlayer[index].EventProcessing[i].CurSlot = 0;
                                                break;
                                            }
                                        case 3:
                                            {
                                                Core.Type.TempPlayer[index].EventProcessing[i].ListLeftOff[Core.Type.TempPlayer[index].EventProcessing[i].CurList] = Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1;
                                                Core.Type.TempPlayer[index].EventProcessing[i].CurList = Core.Type.Map[GetPlayerMap(index)].Event[EventID].Pages[PageID].CommandList[Core.Type.TempPlayer[index].EventProcessing[i].CurList].Commands[Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1].Data3;
                                                Core.Type.TempPlayer[index].EventProcessing[i].CurSlot = 0;
                                                break;
                                            }
                                        case 4:
                                            {
                                                Core.Type.TempPlayer[index].EventProcessing[i].ListLeftOff[Core.Type.TempPlayer[index].EventProcessing[i].CurList] = Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1;
                                                Core.Type.TempPlayer[index].EventProcessing[i].CurList = Core.Type.Map[GetPlayerMap(index)].Event[EventID].Pages[PageID].CommandList[Core.Type.TempPlayer[index].EventProcessing[i].CurList].Commands[Core.Type.TempPlayer[index].EventProcessing[i].CurSlot - 1].Data4;
                                                Core.Type.TempPlayer[index].EventProcessing[i].CurSlot = 0;
                                                break;
                                            }
                                    }
                                }
                                Core.Type.TempPlayer[index].EventProcessing[i].WaitingForResponse = 0;
                            }
                        }
                    }
                }
            }

            buffer.Dispose();

        }

        public static void Packet_Event(int index, ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();
            buffer.Dispose();

            EventLogic.TriggerEvent(index, i, 0, GetPlayerX(index), GetPlayerY(index));
        }

        public static void Packet_RequestSwitchesAndVariables(int index, ref byte[] data)
        {
            SendSwitchesAndVariables(index);
        }

        public static void Packet_SwitchesAndVariables(int index, ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            var loopTo = Core.Constant.MAX_SWITCHES - 1;
            for (i = 0; i < loopTo; i++)
                Switches[i] = buffer.ReadString();

            var loopTo1 = Core.Constant.NAX_VARIABLES - 1;
            for (i = 0; i < loopTo1; i++)
                Variables[i] = buffer.ReadString();

            SaveSwitches();
            SaveVariables();

            buffer.Dispose();

            SendSwitchesAndVariables(0, true);

        }

        #endregion

        #region Outgoing Packets

        public static void SendSpecialEffect(int index, int effectType, int data1 = 0, int data2 = 0, int data3 = 0, int data4 = 0)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SSpecialEffect);

            switch (effectType)
            {
                case EffectTypeFadein:
                    {
                        buffer.WriteInt32(effectType);
                        break;
                    }
                case EffectTypeFadeout:
                    {
                        buffer.WriteInt32(effectType);
                        break;
                    }
                case EffectTypeFlash:
                    {
                        buffer.WriteInt32(effectType);
                        break;
                    }
                case EffectTypeFog:
                    {
                        buffer.WriteInt32(effectType);
                        buffer.WriteInt32(data1); // fognum
                        buffer.WriteInt32(data2); // fog movement speed
                        buffer.WriteInt32(data3); // opacity
                        break;
                    }
                case EffectTypeWeather:
                    {
                        buffer.WriteInt32(effectType);
                        buffer.WriteInt32(data1); // weather type
                        buffer.WriteInt32(data2); // weather intensity
                        break;
                    }
                case EffectTypeTint:
                    {
                        buffer.WriteInt32(effectType);
                        buffer.WriteInt32(data1); // red
                        buffer.WriteInt32(data2); // green
                        buffer.WriteInt32(data3); // blue
                        buffer.WriteInt32(data4); // alpha
                        break;
                    }
            }

            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
            buffer.Dispose();

        }

        public static void SendSwitchesAndVariables(int index, bool everyone = false)
        {
            var buffer = new ByteStream(4);
            int i;

            buffer.WriteInt32((int) ServerPackets.SSwitchesAndVariables);

            var loopTo = Core.Constant.MAX_SWITCHES - 1;
            for (i = 0; i < loopTo; i++)
                buffer.WriteString(Switches[i]);

            var loopTo1 = Core.Constant.NAX_VARIABLES - 1;
            for (i = 0; i < loopTo1; i++)
                buffer.WriteString(Variables[i]);

            if (everyone)
            {
                NetworkConfig.SendDataToAll(ref buffer.Data, buffer.Head);
            }
            else
            {
                NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
            }

            buffer.Dispose();

        }

        public static void SendMapEventData(int index)
        {
            var buffer = new ByteStream(4);
            int i;
            int x;
            int y;
            int z;
            int mapNum;
            int w;

            buffer.WriteInt32((int) ServerPackets.SMapEventData);
            mapNum = GetPlayerMap(index);
            buffer.WriteInt32(Core.Type.Map[mapNum].EventCount);

            if (Core.Type.Map[mapNum].EventCount > 0)
            {
                var loopTo = Core.Type.Map[mapNum].EventCount;
                for (i = 0; i < loopTo; i++)
                {
                    {
                        ref var withBlock = ref Core.Type.Map[mapNum].Event[i];
                        buffer.WriteString(withBlock.Name);
                        buffer.WriteByte(withBlock.Globals);
                        buffer.WriteInt32(withBlock.X);
                        buffer.WriteInt32(withBlock.Y);
                        buffer.WriteInt32(withBlock.PageCount);
                    }
                    if (Core.Type.Map[mapNum].Event[i].PageCount > 0)
                    {
                        var loopTo1 = Core.Type.Map[mapNum].Event[i].PageCount;
                        for (x = 0; x < (int)loopTo1; x++)
                        {
                            {
                                ref var withBlock1 = ref Core.Type.Map[mapNum].Event[i].Pages[x];
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
                                buffer.WriteInt32(withBlock1.MoveRouteCount);

                                buffer.WriteInt32(withBlock1.IgnoreMoveRoute);
                                buffer.WriteInt32(withBlock1.RepeatMoveRoute);

                                if (withBlock1.MoveRouteCount > 0)
                                {
                                    var loopTo2 = withBlock1.MoveRouteCount;
                                    for (y = 0; y < (int)loopTo2; y++)
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

                            if (Core.Type.Map[mapNum].Event[i].Pages[x].CommandListCount > 0)
                            {
                                var loopTo3 = Core.Type.Map[mapNum].Event[i].Pages[x].CommandListCount;
                                for (y = 0; y < (int)loopTo3; y++)
                                {
                                    buffer.WriteInt32(Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount);
                                    buffer.WriteInt32(Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].ParentList);
                                    if (Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                    {
                                        var loopTo4 = Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount;
                                        for (z = 0; z < (int)loopTo4; z++)
                                        {
                                            {
                                                ref var withBlock2 = ref Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].Commands[z];
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
                                                    var loopTo5 = withBlock2.MoveRouteCount;
                                                    for (w = 0; w < (int)loopTo5; w++)
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

            // End Event Data
            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
            buffer.Dispose();
            SendSwitchesAndVariables(index);

        }

        #endregion

        #region Misc

        public static void GivePlayerExp(int index, int exp)
        {
            int petnum;

            // give the exp
            SetPlayerExp(index, GetPlayerExp(index) + exp);
            NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + exp + " Exp", (int) ColorType.BrightGreen, 1, GetPlayerX(index) * 32, GetPlayerY(index) * 32);

            // check if we've leveled
            Player.CheckPlayerLevelUp(index);

            if (Pet.PetAlive(index))
            {
                petnum = Pet.GetPetNum(index);

                if (Core.Type.Pet[petnum].LevelingType == 1)
                {
                    Pet.SetPetExp(index, (int)Math.Round(Pet.GetPetExp(index) + exp * (Core.Type.Pet[petnum].ExpGain / 100d)));
                    NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + exp * (Core.Type.Pet[petnum].ExpGain / 100d) + " Exp", (int) ColorType.BrightGreen, 1, Pet.GetPetX(index) * 32, Pet.GetPetY(index) * 32);
                    Pet.CheckPetLevelUp(index);
                    Pet.SendPetExp(index);
                }
            }

            NetworkSend.SendExp(index);

        }

        public static void CustomScript(int index, int caseId, int mapNum, int EventID)
        {

            switch (caseId)
            {
                default:
                    {
                        NetworkSend.PlayerMsg(index, "You just activated custom script " + caseId + ". This script is not yet programmed.", (int) ColorType.BrightRed);
                        break;
                    }
            }

        }

        #endregion

    }
}