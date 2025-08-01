using Core;
using Core.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Mirage.Sharp.Asfw;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Core.Global.Command;
using static Core.Packets;
using static Core.Type;
using Path = Core.Path;

namespace Server
{
    public class Event
    {
        #region Globals

        public static GlobalEvents[] TempEventMap = new GlobalEvents[Core.Constant.MAX_MAPS + 1];
        public static string[] Switches = new string[Core.Constant.MAX_SWITCHES];
        public static string[] Variables = new string[Core.Constant.NAX_VARIABLES];
        private static readonly ConcurrentBag<ScheduledEvent> ScheduledEvents = new ConcurrentBag<ScheduledEvent>();
        private static readonly object TempEventLock = new object();

        internal const int PathfindingType = 0; // 0: None, 1: Random, 2: BFS (existing), 3: A* (new)

        // Effect Constants
        internal const int EffectTypeFadein = 2;
        internal const int EffectTypeFadeout = 0;
        internal const int EffectTypeFlash = 3;
        internal const int EffectTypeFog = 4;
        internal const int EffectTypeWeather = 5;
        internal const int EffectTypeTint = 6;
        internal const int EffectTypeScreenShake = 7; // New effect

        #endregion

        #region Database

        public static void CreateSwitches()
        {
            Switches = new string[Core.Constant.MAX_SWITCHES];
            Array.Fill(Switches, string.Empty);
            SaveSwitches();
            General.Logger.LogInformation("Switches initialized and saved.");
        }

        public static void CreateVariables()
        {
            Variables = new string[Core.Constant.NAX_VARIABLES];
            Array.Fill(Variables, string.Empty);
            SaveVariables();
            General.Logger.LogInformation("Variables initialized and saved.");
        }

        public static void SaveSwitches()
        {
            try
            {
                var json = new JsonSerializer<string[]>();
                json.Write(System.IO.Path.Combine(Path.Database, "Switches.json"), Switches);
            }
            catch (Exception ex)
            {
                General.Logger.LogError(ex, "Failed to save Switches.");
                throw;
            }
        }

        public static void SaveVariables()
        {
            try
            {
                var json = new JsonSerializer<string[]>();
                json.Write(System.IO.Path.Combine(Path.Database, "Variables.json"), Variables);
            }
            catch (Exception ex)
            {
                General.Logger.LogError(ex, "Failed to save Variables.");
                throw;
            }
        }

        public static async System.Threading.Tasks.Task LoadSwitchesAsync()
        {
            var json = new JsonSerializer<string[]>();
            try
            {
                Switches = await System.Threading.Tasks.Task.Run(() => json.Read(System.IO.Path.Combine(Path.Database, "Switches.json")));
                if (Switches == null || Switches.Length != Core.Constant.MAX_SWITCHES)
                {
                    General.Logger.LogWarning("Switches.json not found or invalid. Creating new switches.");
                    CreateSwitches();
                }
            }
            catch (Exception ex)
            {
                General.Logger.LogError(ex, "Failed to load Switches.json. Creating new switches.");
                CreateSwitches();
            }
        }

        public static async System.Threading.Tasks.Task LoadVariablesAsync()
        {
            var json = new JsonSerializer<string[]>();
            try
            {
                Variables = await System.Threading.Tasks.Task.Run(() => json.Read(System.IO.Path.Combine(Path.Database, "Variables.json")));
                if (Variables == null || Variables.Length != Core.Constant.NAX_VARIABLES)
                {
                    General.Logger.LogWarning("Variables.json not found or invalid. Creating new variables.");
                    CreateVariables();
                }
            }
            catch (Exception ex)
            {
                General.Logger.LogError(ex, "Failed to load Variables.json. Creating new variables.");
                CreateVariables();
            }
        }

        #endregion

        #region Movement

        // Helper methods for CanEventMove
        private static bool IsTileWalkable(int mapNum, int x, int y)
        {
            if (x < 0 || x > Data.Map[mapNum].MaxX || y < 0 || y > Data.Map[mapNum].MaxY) return false;
            var tile = Data.Map[mapNum].Tile[x, y];
            return tile.Type != TileType.Blocked && tile.Type2 != TileType.Blocked &&
                   (tile.Type == TileType.Item || tile.Type == TileType.NpcSpawn ||
                    tile.Type2 == TileType.Item || tile.Type2 == TileType.NpcSpawn);
        }

        private static bool IsPlayerBlocking(int index, int mapNum, int x, int y, int eventId)
        {
            for (int i = 0; i <= NetworkConfig.Socket.HighIndex; i++)
            {
                if (NetworkConfig.IsPlaying(i) && GetPlayerMap(i) == mapNum && GetPlayerX(i) == x && GetPlayerY(i) == y)
                {
                    if (Data.Map[mapNum].Event[eventId].Pages[Core.Data.TempPlayer[index].EventMap.EventPages[eventId].PageId].Trigger == 1)
                    {
                        StartEventProcessing(index, eventId, mapNum);
                    }
                    return true;
                }
            }
            return false;
        }

        private static void StartEventProcessing(int index, int eventId, int mapNum)
        {
            var pageId = Core.Data.TempPlayer[index].EventMap.EventPages[eventId].PageId;
            if (Data.Map[mapNum].Event[eventId].Pages[pageId].CommandListCount <= 0) return;

            var processing = Core.Data.TempPlayer[index].EventProcessing[eventId];
            processing.Active = 0;
            processing.ActionTimer = General.GetTimeMs();
            processing.CurList = 0;
            processing.CurSlot = 0;
            processing.EventId = eventId;
            processing.PageId = pageId;
            processing.WaitingForResponse = 0;
            processing.ListLeftOff = new int[Data.Map[mapNum].Event[eventId].Pages[pageId].CommandListCount];
        }

        private static bool IsNpcBlocking(int mapNum, int x, int y)
        {
            for (int i = 0; i < Core.Constant.MAX_MAP_NPCS; i++)
            {
                if (Data.MapNpc[mapNum].Npc[i].X == x && Data.MapNpc[mapNum].Npc[i].Y == y)
                    return true;
            }
            return false;
        }

        private static bool IsDirectionBlocked(int mapNum, int x, int y, byte dir) =>
            IsDirBlocked(ref Data.Map[mapNum].Tile[x, y].DirBlock, dir);

        public static bool CanEventMove(int index, int mapNum, int x, int y, int eventId, int walkThrough, byte dir, bool globalEvent = false)
        {
            if (!IsValidMapAndDirection(mapNum, dir)) return false;

            int targetX = x, targetY = y;
            switch (dir)
            {
                case (byte)Direction.Up: targetY--; break;
                case (byte)Direction.Down: targetY++; break;
                case (byte)Direction.Left: targetX--; break;
                case (byte)Direction.Right: targetX++; break;
                default: return false;
            }

            if (targetX < 0 || targetX > Data.Map[mapNum].MaxX || targetY < 0 || targetY > Data.Map[mapNum].MaxY) return false;
            if (walkThrough == 1) return true;

            return IsTileWalkable(mapNum, targetX, targetY) &&
                   !IsPlayerBlocking(index, mapNum, targetX, targetY, eventId) &&
                   !IsNpcBlocking(mapNum, targetX, targetY) &&
                   !IsDirectionBlocked(mapNum, x, y, dir);
        }

        private static bool IsValidMapAndDirection(int mapNum, byte dir) =>
            mapNum >= 0 && mapNum <= Core.Constant.MAX_MAPS && dir >= (byte)Direction.Up && dir <= (byte)Direction.DownRight;

        public static void EventDir(int playerIndex, int mapNum, int eventId, int dir, bool globalEvent = false)
        {
            if (!IsValidMapAndDirection(mapNum, (byte)dir)) return;

            int eventIndex = GetEventIndex(playerIndex, eventId, globalEvent);
            if (eventIndex == -1) return;

            lock (TempEventLock)
            {
                if (globalEvent)
                {
                    if (Data.Map[mapNum].Event[eventId].Pages[0].DirFix == 0)
                        TempEventMap[mapNum].Event[eventId].Dir = dir;
                }
                else if (Data.Map[mapNum].Event[eventId].Pages[Core.Data.TempPlayer[playerIndex].EventMap.EventPages[eventIndex].PageId].DirFix == 0)
                    Core.Data.TempPlayer[playerIndex].EventMap.EventPages[eventIndex].Dir = dir;
            }

            SendEventDirection(mapNum, eventId, dir, globalEvent ? TempEventMap[mapNum].Event[eventId].Dir : Core.Data.TempPlayer[playerIndex].EventMap.EventPages[eventIndex].Dir);
        }

        private static int GetEventIndex(int playerIndex, int eventId, bool globalEvent)
        {
            if (globalEvent) return eventId;
            if (Core.Data.TempPlayer[playerIndex].EventMap.CurrentEvents <= 0) return -1;

            for (int i = 0; i < Core.Data.TempPlayer[playerIndex].EventMap.CurrentEvents; i++)
            {
                if (eventId == i)
                    return i;
            }
            return -1;
        }

        private static void SendEventDirection(int mapNum, int eventId, int dir, int currentDir)
        {
            var buffer = new ByteStream(12);
            buffer.WriteInt32((int)ServerPackets.SEventDir);
            buffer.WriteInt32(eventId);
            buffer.WriteInt32(currentDir);
            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void EventMove(int index, int mapNum, int eventId, int dir, int movementSpeed, bool globalEvent = false)
        {
            if (!IsValidMapAndDirection(mapNum, (byte)dir)) return;

            int eventIndex = GetEventIndex(index, eventId, globalEvent);
            if (eventIndex == -1) return;

            lock (TempEventLock)
            {
                if (globalEvent)
                {
                    var eventData = TempEventMap[mapNum].Event[eventIndex];
                    if (Data.Map[mapNum].Event[eventId].Pages[0].DirFix == 0)
                        eventData.Dir = dir;

                    switch (dir)
                    {
                        case (byte)Direction.Up: eventData.Y--; break;
                        case (byte)Direction.Down: eventData.Y++; break;
                        case (byte)Direction.Left: eventData.X--; break;
                        case (byte)Direction.Right: eventData.X++; break;
                    }

                    SendEventMove(mapNum, eventId, eventData.X, eventData.Y, dir, eventData.Dir, movementSpeed, 0);
                }
                else
                {
                    var eventData = Core.Data.TempPlayer[index].EventMap.EventPages[eventIndex];
                    if (Data.Map[mapNum].Event[eventId].Pages[Core.Data.TempPlayer[index].EventMap.EventPages[eventIndex].PageId].DirFix == 0)
                        eventData.Dir = dir;

                    switch (dir)
                    {
                        case (byte)Direction.Up: eventData.Y--; break;
                        case (byte)Direction.Down: eventData.Y++; break;
                        case (byte)Direction.Left: eventData.X--; break;
                        case (byte)Direction.Right: eventData.X++; break;
                    }

                    SendEventMove(mapNum, eventId, eventData.X, eventData.Y, dir, eventData.Dir, movementSpeed, index);
                }
            }
        }

        private static void SendEventMove(int mapNum, int eventId, int x, int y, int dir, int currentDir, int speed, int index = -1)
        {
            var buffer = new ByteStream(24);
            buffer.WriteInt32((int)ServerPackets.SEventMove);
            buffer.WriteInt32(eventId);
            buffer.WriteInt32(x);
            buffer.WriteInt32(y);
            buffer.WriteInt32(dir);
            buffer.WriteInt32(currentDir);
            buffer.WriteInt32(speed);
            if (index == -1)
                NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            else
                NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static bool IsOneBlockAway(int x1, int y1, int x2, int y2) =>
            (x1 == x2 && (y1 == y2 - 1 || y1 == y2 + 1)) || (y1 == y2 && (x1 == x2 - 1 || x1 == x2 + 1));

        public static byte GetNpcDir(int x, int y, int x1, int y1)
        {
            byte direction = (int)Direction.Right;
            int maxDistance = 0;
            UpdateDirectionAndDistance(x - x1, (int)Direction.Right, (int)Direction.Left, ref direction, ref maxDistance);
            UpdateDirectionAndDistance(y - y1, (int)Direction.Down, (int)Direction.Up, ref direction, ref maxDistance);
            return direction;
        }

        private static void UpdateDirectionAndDistance(int diff, int posDir, int negDir, ref byte direction, ref int maxDistance)
        {
            int absDiff = Math.Abs(diff);
            if (absDiff > maxDistance)
            {
                direction = (byte)(diff > 0 ? posDir : negDir);
                maxDistance = absDiff;
            }
        }

        public static int CanEventMoveTowardsPlayer(int playerId, int mapNum, int eventId)
        {
            if (!IsValidPlayerEvent(playerId, mapNum, eventId)) return 4; // Invalid direction as failure

            var (px, py, ex, ey, walkThrough) = GetPlayerAndEventPositions(playerId, mapNum, eventId);
            return PathfindingType switch
            {
                1 => RandomMoveTowardsPlayer(playerId, mapNum, eventId, ex, ey, px, py, walkThrough),
                2 => BFSMoveTowardsPlayer(playerId, mapNum, eventId, ex, ey, px, py, walkThrough),
                3 => AStarMoveTowardsPlayer(playerId, mapNum, eventId, ex, ey, px, py, walkThrough), // New A* pathfinding
                _ => RandomDirection()
            };
        }

        private static bool IsValidPlayerEvent(int playerId, int mapNum, int eventId) =>
            playerId >= 0 && playerId < Core.Constant.MAX_PLAYERS &&
            mapNum >= 0 && mapNum <= Core.Constant.MAX_MAPS &&
            eventId >= 0 && eventId < Core.Data.TempPlayer[playerId].EventMap.CurrentEvents;

        private static (int px, int py, int ex, int ey, int walkThrough) GetPlayerAndEventPositions(int playerId, int mapNum, int eventId)
        {
            int px = GetPlayerX(playerId), py = GetPlayerY(playerId);
            var eventPage = Core.Data.TempPlayer[playerId].EventMap.EventPages[eventId];
            return (px, py, eventPage.X, eventPage.Y,
                Data.Map[mapNum].Event[eventPage.EventId].Pages[eventPage.PageId].WalkThrough);
        }

        private static int RandomMoveTowardsPlayer(int playerId, int mapNum, int eventId, int ex, int ey, int px, int py, int walkThrough)
        {
            int i = General.GetRandom.NextInt(0, 4);
            foreach (var dir in GetDirectionOrder(i))
            {
                if (ShouldMoveTowards(ex, ey, px, py, dir) && CanEventMove(playerId, mapNum, ex, ey, eventId, walkThrough, (byte)dir, false))
                    return dir;
            }
            return RandomDirection();
        }

        private static IEnumerable<int> GetDirectionOrder(int start) =>
            Enumerable.Range(0, 4).Select(i => (start + i) % 4);

        private static bool ShouldMoveTowards(int ex, int ey, int px, int py, int dir) =>
            dir switch
            {
                (int)Direction.Up => ey > py,
                (int)Direction.Down => ey < py,
                (int)Direction.Left => ex > px,
                (int)Direction.Right => ex < px,
                _ => false
            };

        private static int BFSMoveTowardsPlayer(int playerId, int mapNum, int eventId, int ex, int ey, int px, int py, int walkThrough)
        {
            // Existing BFS implementation (simplified here for brevity)
            var queue = new Queue<(int x, int y)>();
            var visited = new HashSet<(int, int)>();
            var parent = new Dictionary<(int, int), (int, int)>();
            queue.Enqueue((ex, ey));
            visited.Add((ex, ey));

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                if (x == px && y == py)
                {
                    var current = (x, y);
                    while (parent[current] != (ex, ey))
                        current = parent[current];
                    return GetDirectionFromStep(ex, ey, current.Item1, current.Item2);
                }

                foreach (var (dx, dy, dir) in new[] { (0, -1, (int)Direction.Up), (0, 1, (int)Direction.Down), (-1, 0, (int)Direction.Left), (1, 0, (int)Direction.Right) })
                {
                    int nx = x + dx, ny = y + dy;
                    if (IsValidMove(playerId, mapNum, eventId, nx, ny, walkThrough, visited))
                    {
                        queue.Enqueue((nx, ny));
                        visited.Add((nx, ny));
                        parent[(nx, ny)] = (x, y);
                    }
                }
            }
            return 4; // No path found
        }

        private static bool IsValidMove(int playerId, int mapNum, int eventId, int x, int y, int walkThrough, HashSet<(int, int)> visited) =>
            x >= 0 && x <= Data.Map[mapNum].MaxX && y >= 0 && y <= Data.Map[mapNum].MaxY &&
            !visited.Contains((x, y)) && CanEventMove(playerId, mapNum, x, y, eventId, walkThrough, 0, false);

        private static int GetDirectionFromStep(int ex, int ey, int nx, int ny) =>
            nx > ex ? (int)Direction.Right : nx < ex ? (int)Direction.Left : ny > ey ? (int)Direction.Down : (int)Direction.Up;

        // New A* Pathfinding
        private static int AStarMoveTowardsPlayer(int playerId, int mapNum, int eventId, int ex, int ey, int px, int py, int walkThrough)
        {
            var openSet = new PriorityQueue<(int x, int y, int fScore)>(Comparer<(int x, int y, int fScore)>.Create((a, b) => a.fScore.CompareTo(b.fScore)));
            var cameFrom = new Dictionary<(int, int), (int, int)>();
            var gScore = new Dictionary<(int, int), int> { [(ex, ey)] = 0 };
            var fScore = new Dictionary<(int, int), int> { [(ex, ey)] = Heuristic(ex, ey, px, py) };
            openSet.Enqueue((ex, ey, fScore[(ex, ey)]));

            while (openSet.Count > 0)
            {
                var (x, y, _) = openSet.Dequeue();
                if (x == px && y == py)
                {
                    var current = (x, y);
                    while (cameFrom[current] != (ex, ey))
                        current = cameFrom[current];
                    return GetDirectionFromStep(ex, ey, current.Item1, current.Item2);
                }

                foreach (var (dx, dy, dir) in new[] { (0, -1, (int)Direction.Up), (0, 1, (int)Direction.Down), (-1, 0, (int)Direction.Left), (1, 0, (int)Direction.Right) })
                {
                    int nx = x + dx, ny = y + dy;
                    if (!IsWithinMapBounds(mapNum, nx, ny) || !CanEventMove(playerId, mapNum, x, y, eventId, walkThrough, (byte)dir, false)) continue;

                    int tentativeGScore = gScore[(x, y)] + 1;
                    if (!gScore.ContainsKey((nx, ny)) || tentativeGScore < gScore[(nx, ny)])
                    {
                        cameFrom[(nx, ny)] = (x, y);
                        gScore[(nx, ny)] = tentativeGScore;
                        fScore[(nx, ny)] = gScore[(nx, ny)] + Heuristic(nx, ny, px, py);
                        openSet.Enqueue((nx, ny, fScore[(nx, ny)]));
                    }
                }
            }
            return 4; // No path found
        }

        private static bool IsWithinMapBounds(int mapNum, int x, int y) =>
            x >= 0 && x <= Data.Map[mapNum].MaxX && y >= 0 && y <= Data.Map[mapNum].MaxY;

        private static int Heuristic(int x1, int y1, int x2, int y2) => Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

        private static int RandomDirection() => General.GetRandom.NextInt(0, 4);

        public static int CanEventMoveAwayFromPlayer(int playerId, int mapNum, int eventId)
        {
            if (!IsValidPlayerEvent(playerId, mapNum, eventId)) return 5;

            var (px, py, ex, ey, walkThrough) = GetPlayerAndEventPositions(playerId, mapNum, eventId);
            int i = General.GetRandom.NextInt(0, 4);
            foreach (var dir in GetDirectionOrder(i))
            {
                if (ShouldMoveAway(ex, ey, px, py, dir) && CanEventMove(playerId, mapNum, ex, ey, eventId, walkThrough, (byte)dir, false))
                    return dir;
            }
            return RandomDirection();
        }

        private static bool ShouldMoveAway(int ex, int ey, int px, int py, int dir) =>
            dir switch
            {
                (int)Direction.Up => ey < py,
                (int)Direction.Down => ey > py,
                (int)Direction.Left => ex < px,
                (int)Direction.Right => ex > px,
                _ => false
            };

        public static int GetDirToPlayer(int playerId, int mapNum, int eventId)
        {
            if (!IsValidPlayerEvent(playerId, mapNum, eventId)) return (int)Direction.Right;
            var (px, py, ex, ey, _) = GetPlayerAndEventPositions(playerId, mapNum, eventId);
            return GetNpcDir(ex, ey, px, py);
        }

        public static int GetDirAwayFromPlayer(int playerId, int mapNum, int eventId)
        {
            if (!IsValidPlayerEvent(playerId, mapNum, eventId)) return (int)Direction.Right;
            var (px, py, ex, ey, _) = GetPlayerAndEventPositions(playerId, mapNum, eventId);
            byte direction = (int)Direction.Right;
            int maxDistance = 0;
            UpdateDirectionAndDistance(px - ex, (int)Direction.Left, (int)Direction.Right, ref direction, ref maxDistance);
            UpdateDirectionAndDistance(py - ey, (int)Direction.Up, (int)Direction.Down, ref direction, ref maxDistance);
            return direction;
        }

        // New Movement Behaviors
        public static void PatrolEvent(int index, int mapNum, int eventId, List<(int x, int y)> patrolPath, int speed, bool globalEvent = false)
        {
            if (!patrolPath.Any()) return;
            int currentStep = TempEventMap[mapNum].Event[eventId].PatrolStep % patrolPath.Count;
            var (targetX, targetY) = patrolPath[currentStep];
            int dir = GetDirectionToTarget(TempEventMap[mapNum].Event[eventId].X, TempEventMap[mapNum].Event[eventId].Y, targetX, targetY);
            if (CanEventMove(index, mapNum, TempEventMap[mapNum].Event[eventId].X, TempEventMap[mapNum].Event[eventId].Y, eventId, 0, (byte)dir, globalEvent))
            {
                EventMove(index, mapNum, eventId, dir, speed, globalEvent);
                if (TempEventMap[mapNum].Event[eventId].X == targetX && TempEventMap[mapNum].Event[eventId].Y == targetY)
                    TempEventMap[mapNum].Event[eventId].PatrolStep++;
            }
        }

        private static int GetDirectionToTarget(int x, int y, int tx, int ty) =>
            tx > x ? (int)Direction.Right : tx < x ? (int)Direction.Left : ty > y ? (int)Direction.Down : (int)Direction.Up;

        public static void FollowPlayer(int index, int mapNum, int eventId, int targetPlayerId, int speed, bool globalEvent = false)
        {
            int dir = CanEventMoveTowardsPlayer(targetPlayerId, mapNum, eventId);
            if (dir != 4)
                EventMove(index, mapNum, eventId, dir, speed, globalEvent);
        }

        #endregion

        #region Incoming Packets

        public static void Packet_EventChatReply(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int eventId = buffer.ReadInt32(), PageId = buffer.ReadInt32(), reply = buffer.ReadInt32();
            buffer.Dispose();

            General.Logger.LogInformation($"Player {index} responded to event {eventId} with reply {reply}");
            ProcessEventReply(index, eventId, PageId, reply);
        }

        private static void ProcessEventReply(int index, int eventId, int PageId, int reply)
        {
            for (int i = 0; i < Core.Data.TempPlayer[index].EventProcessingCount; i++)
            {
                var proc = Core.Data.TempPlayer[index].EventProcessing[i];
                if (proc.EventId != eventId || proc.PageId != PageId || proc.WaitingForResponse != 1) continue;

                var cmd = Data.Map[GetPlayerMap(index)].Event[eventId].Pages[PageId].CommandList[proc.CurList].Commands[proc.CurSlot - 1];
                if (reply == 0 && cmd.Index == (byte)Core.EventCommand.ShowText)
                    proc.WaitingForResponse = 0;
                else if (reply > 0 && cmd.Index == (byte)Core.EventCommand.ShowChoices)
                    UpdateEventProcessing(index, i, reply, cmd);
            }
        }

        private static void UpdateEventProcessing(int index, int procIndex, int reply, Core.Type.EventCommand cmd)
        {
            var proc = Core.Data.TempPlayer[index].EventProcessing[procIndex];
            proc.ListLeftOff[proc.CurList] = proc.CurSlot - 1;
            proc.CurList = reply switch
            {
                1 => cmd.Data1,
                2 => cmd.Data2,
                3 => cmd.Data3,
                4 => cmd.Data4,
                _ => proc.CurList
            };
            proc.CurSlot = 0;
            proc.WaitingForResponse = 0;
        }

        public static void Packet_Event(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int eventId = buffer.ReadInt32();
            buffer.Dispose();
            EventLogic.TriggerEvent(index, eventId, 0, GetPlayerX(index), GetPlayerY(index));
        }

        public static void Packet_RequestSwitchesAndVariables(int index, ref byte[] data) => SendSwitchesAndVariables(index);

        public static void Packet_SwitchesAndVariables(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            for (int i = 0; i < Core.Constant.MAX_SWITCHES; i++) Switches[i] = buffer.ReadString();
            for (int i = 0; i < Core.Constant.NAX_VARIABLES; i++) Variables[i] = buffer.ReadString();
            buffer.Dispose();

            SaveSwitches();
            SaveVariables();
            SendSwitchesAndVariables(0, true);
        }

        #endregion

        #region Outgoing Packets

        public static void SendSpecialEffect(int index, int effectType, int data1 = 0, int data2 = 0, int data3 = 0, int data4 = 0)
        {
            var buffer = new ByteStream(24);
            buffer.WriteInt32((int)ServerPackets.SSpecialEffect);
            buffer.WriteInt32(effectType);

            switch (effectType)
            {
                case EffectTypeFadein:
                case EffectTypeFadeout:
                case EffectTypeFlash:
                    break;
                case EffectTypeFog:
                    buffer.WriteInt32(data1); // Fog number
                    buffer.WriteInt32(data2); // Movement speed
                    buffer.WriteInt32(data3); // Opacity
                    break;
                case EffectTypeWeather:
                    buffer.WriteInt32(data1); // Weather type
                    buffer.WriteInt32(data2); // Intensity
                    break;
                case EffectTypeTint:
                    buffer.WriteInt32(data1); // Red
                    buffer.WriteInt32(data2); // Green
                    buffer.WriteInt32(data3); // Blue
                    buffer.WriteInt32(data4); // Alpha
                    break;
                case EffectTypeScreenShake:
                    buffer.WriteInt32(data1); // Intensity
                    buffer.WriteInt32(data2); // Duration
                    break;
                default:
                    General.Logger.LogWarning($"Unknown effect type {effectType} sent to player {index}");
                    return;
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendSwitchesAndVariables(int index, bool everyone = false)
        {
            var buffer = new ByteStream(4 + (Core.Constant.MAX_SWITCHES + Core.Constant.NAX_VARIABLES) * 256);
            buffer.WriteInt32((int)ServerPackets.SSwitchesAndVariables);
            for (int i = 0; i < Core.Constant.MAX_SWITCHES; i++) buffer.WriteString(Switches[i]);
            for (int i = 0; i < Core.Constant.NAX_VARIABLES; i++) buffer.WriteString(Variables[i]);

            if (everyone)
                NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            else
                NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendMapEventData(int index)
        {
            var buffer = new ByteStream(4);
            int mapNum = GetPlayerMap(index);
            buffer.WriteInt32((int)ServerPackets.SMapEventData);
            buffer.WriteInt32(Data.Map[mapNum].EventCount);

            if (Data.Map[mapNum].EventCount > 0)
            {
                SerializeMapEvents(buffer, mapNum);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
            SendSwitchesAndVariables(index);
        }

        private static void SerializeMapEvents(ByteStream buffer, int mapNum)
        {
            for (int i = 0; i < Data.Map[mapNum].EventCount; i++)
            {
                var ev = Data.Map[mapNum].Event[i];
                buffer.WriteString(ev.Name);
                buffer.WriteByte(ev.Globals);
                buffer.WriteInt32(ev.X);
                buffer.WriteInt32(ev.Y);
                buffer.WriteInt32(ev.PageCount);

                if (ev.PageCount > 0)
                    SerializeEventPages(buffer, mapNum, i, ev.PageCount);
            }
        }

        private static void SerializeEventPages(ByteStream buffer, int mapNum, int eventIndex, int pageCount)
        {
            for (int x = 0; x < pageCount; x++)
            {
                var page = Data.Map[mapNum].Event[eventIndex].Pages[x];
                SerializePageConditions(buffer, page);
                SerializePageGraphics(buffer, page);
                SerializePageMovement(buffer, page);
                SerializePageCommands(buffer, mapNum, eventIndex, x, page);
            }
        }

        private static void SerializePageConditions(ByteStream buffer, EventPage page)
        {
            buffer.WriteInt32(page.ChkVariable);
            buffer.WriteInt32(page.VariableIndex);
            buffer.WriteInt32(page.VariableCondition);
            buffer.WriteInt32(page.VariableCompare);
            buffer.WriteInt32(page.ChkSwitch);
            buffer.WriteInt32(page.SwitchIndex);
            buffer.WriteInt32(page.SwitchCompare);
            buffer.WriteInt32(page.ChkHasItem);
            buffer.WriteInt32(page.HasItemIndex);
            buffer.WriteInt32(page.HasItemAmount);
            buffer.WriteInt32(page.ChkSelfSwitch);
            buffer.WriteInt32(page.SelfSwitchIndex);
            buffer.WriteInt32(page.SelfSwitchCompare);
        }

        private static void SerializePageGraphics(ByteStream buffer, EventPage page)
        {
            buffer.WriteByte(page.GraphicType);
            buffer.WriteInt32(page.Graphic);
            buffer.WriteInt32(page.GraphicX);
            buffer.WriteInt32(page.GraphicY);
            buffer.WriteInt32(page.GraphicX2);
            buffer.WriteInt32(page.GraphicY2);
        }

        private static void SerializePageMovement(ByteStream buffer, EventPage page)
        {
            buffer.WriteByte(page.MoveType);
            buffer.WriteByte(page.MoveSpeed);
            buffer.WriteByte(page.MoveFreq);
            buffer.WriteInt32(page.MoveRouteCount);
            buffer.WriteInt32(page.IgnoreMoveRoute);
            buffer.WriteInt32(page.RepeatMoveRoute);
            if (page.MoveRouteCount > 0)
            {
                for (int y = 0; y < page.MoveRouteCount; y++)
                {
                    var route = page.MoveRoute[y];
                    buffer.WriteInt32(route.Index);
                    buffer.WriteInt32(route.Data1);
                    buffer.WriteInt32(route.Data2);
                    buffer.WriteInt32(route.Data3);
                    buffer.WriteInt32(route.Data4);
                    buffer.WriteInt32(route.Data5);
                    buffer.WriteInt32(route.Data6);
                }
            }
            buffer.WriteInt32(page.WalkAnim);
            buffer.WriteInt32(page.DirFix);
            buffer.WriteInt32(page.WalkThrough);
            buffer.WriteInt32(page.ShowName);
            buffer.WriteByte(page.Trigger);
            buffer.WriteInt32(page.CommandListCount);
            buffer.WriteByte(page.Position);
        }

        private static void SerializePageCommands(ByteStream buffer, int mapNum, int eventIndex, int pageIndex, EventPage page)
        {
            if (page.CommandListCount <= 0) return;
            for (int y = 0; y < page.CommandListCount; y++)
            {
                var cmdList = Data.Map[mapNum].Event[eventIndex].Pages[pageIndex].CommandList[y];
                buffer.WriteInt32(cmdList.CommandCount);
                buffer.WriteInt32(cmdList.ParentList);
                if (cmdList.CommandCount > 0)
                {
                    for (int z = 0; z < cmdList.CommandCount; z++)
                    {
                        var cmd = cmdList.Commands[z];
                        SerializeCommand(buffer, cmd);
                    }
                }
            }
        }

        private static void SerializeCommand(ByteStream buffer, Core.Type.EventCommand cmd)
        {
            buffer.WriteInt32(cmd.Index);
            buffer.WriteString(cmd.Text1);
            buffer.WriteString(cmd.Text2);
            buffer.WriteString(cmd.Text3);
            buffer.WriteString(cmd.Text4);
            buffer.WriteString(cmd.Text5);
            buffer.WriteInt32(cmd.Data1);
            buffer.WriteInt32(cmd.Data2);
            buffer.WriteInt32(cmd.Data3);
            buffer.WriteInt32(cmd.Data4);
            buffer.WriteInt32(cmd.Data5);
            buffer.WriteInt32(cmd.Data6);
            buffer.WriteInt32(cmd.ConditionalBranch.CommandList);
            buffer.WriteInt32(cmd.ConditionalBranch.Condition);
            buffer.WriteInt32(cmd.ConditionalBranch.Data1);
            buffer.WriteInt32(cmd.ConditionalBranch.Data2);
            buffer.WriteInt32(cmd.ConditionalBranch.Data3);
            buffer.WriteInt32(cmd.ConditionalBranch.ElseCommandList);
            buffer.WriteInt32(cmd.MoveRouteCount);
            if (cmd.MoveRouteCount > 0)
            {
                for (int w = 0; w < cmd.MoveRouteCount; w++)
                {
                    var route = cmd.MoveRoute[w];
                    buffer.WriteInt32(route.Index);
                    buffer.WriteInt32(route.Data1);
                    buffer.WriteInt32(route.Data2);
                    buffer.WriteInt32(route.Data3);
                    buffer.WriteInt32(route.Data4);
                    buffer.WriteInt32(route.Data5);
                    buffer.WriteInt32(route.Data6);
                }
            }
        }

        #endregion

        #region New Features

        // Scheduled Events
        public struct ScheduledEvent
        {
            public int EventId;
            public DateTime TriggerTime;
            public int MapNum;
        }

        public static void ScheduleEvent(int eventId, DateTime triggerTime, int mapNum)
        {
            ScheduledEvents.Add(new ScheduledEvent { EventId = eventId, TriggerTime = triggerTime, MapNum = mapNum });
            General.Logger.LogInformation($"Scheduled event {eventId} on map {mapNum} for {triggerTime}");
        }

        public static void CheckScheduledEvents()
        {
            var now = DateTime.Now;
            foreach (var ev in ScheduledEvents.ToList())
            {
                if (now >= ev.TriggerTime)
                {
                    TriggerScheduledEvent(ev);
                    ScheduledEvents.TryTake(out _);
                }
            }
        }

        private static void TriggerScheduledEvent(ScheduledEvent ev)
        {
            for (int i = 0; i <= NetworkConfig.Socket.HighIndex; i++)
            {
                if (NetworkConfig.IsPlaying(i) && GetPlayerMap(i) == ev.MapNum)
                    EventLogic.TriggerEvent(i, ev.EventId, 0, TempEventMap[ev.MapNum].Event[ev.EventId].X, TempEventMap[ev.MapNum].Event[ev.EventId].Y);
            }
            General.Logger.LogInformation($"Triggered scheduled event {ev.EventId} on map {ev.MapNum}");
        }

        // Action-Based Triggers
        public static void TriggerOnPlayerAction(int index, string actionType, int value)
        {
            int mapNum = GetPlayerMap(index);
            for (int i = 0; i < Data.Map[mapNum].EventCount; i++)
            {
                var page = Data.Map[mapNum].Event[i].Pages[Core.Data.TempPlayer[index].EventMap.EventPages[i].PageId];
                if (page.ChkVariable == 1 && page.VariableIndex == GetActionVariableIndex(actionType) && page.VariableCompare == value)
                    EventLogic.TriggerEvent(index, i, 0, GetPlayerX(index), GetPlayerY(index));
            }
        }

        private static int GetActionVariableIndex(string actionType) =>
            actionType switch
            {
                "Kills" => 1,
                "ItemsCollected" => 2,
                _ => 0
            };

        // Environment Effects
        public static void ChangeMapWeather(int mapNum, int weatherType, int intensity)
        {
            for (int i = 0; i <= NetworkConfig.Socket.HighIndex; i++)
            {
                if (NetworkConfig.IsPlaying(i) && GetPlayerMap(i) == mapNum)
                    SendSpecialEffect(i, EffectTypeWeather, weatherType, intensity);
            }
        }

        #endregion

        #region Helper Classes

        // Simple Priority Queue for A* Pathfinding
        private class PriorityQueue<T>
        {
            private readonly List<T> items = new List<T>();
            private readonly IComparer<T> comparer;

            public PriorityQueue(IComparer<T> comparer) => this.comparer = comparer;
            public int Count => items.Count;

            public void Enqueue(T item)
            {
                items.Add(item);
                items.Sort(comparer);
            }

            public T Dequeue()
            {
                if (items.Count == 0) throw new InvalidOperationException("Queue is empty");
                var item = items[0];
                items.RemoveAt(0);
                return item;
            }
        }

        #endregion
    }
}
