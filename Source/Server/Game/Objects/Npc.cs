using System;
using System.Collections.Generic;
using System.Linq;
using static Core.Global.Command;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Enum;
using static Core.Packets;
using static Core.Type;
using System.Reflection;
using Core;

namespace Server
{

    public class NPC
    {

        public static async Task SpawnAllMapNPCs()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_MAP_NPCS).Select(i => Task.Run(() => SpawnMapNPCs(i)));
            await Task.WhenAll(tasks);

        }

        public static async Task SpawnMapNPCs(int mapNum)
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_MAP_NPCS).Select(i => Task.Run(() => SpawnNPC(i, mapNum)));
            await Task.WhenAll(tasks);

        }

        public static void SpawnNPC(int MapNPCNum, int mapNum)
        {
            var buffer = new ByteStream(4);
            int NPCNum;
            int x;
            int y;
            int i = 0;
            var spawned = default(bool);

            if (Core.Type.Map[mapNum].NoRespawn)
                return;

            NPCNum = Core.Type.Map[mapNum].NPC[(int)MapNPCNum];

            if (NPCNum >= 0)
            {
                if (!(Core.Type.NPC[(int)NPCNum].SpawnTime == (byte)Clock.Instance.TimeOfDay) & Core.Type.NPC[(int)NPCNum].SpawnTime != 0)
                {
                    Database.ClearMapNPC((int)MapNPCNum, mapNum);
                    SendMapNPCsToMap(mapNum);
                    return;
                }

                Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Num = NPCNum;
                Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Target = 0;
                Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].TargetType = 0; // clear

                var loopTo = VitalType.Count;
                for (i = 0; i < (int)loopTo; i++)
                    Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Vital[i] = GameLogic.GetNPCMaxVital(NPCNum, (Core.Enum.VitalType)i);

                Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Dir = (int)Conversion.Int(VBMath.Rnd() * 4f);

                // Check if theres a spawn tile for the specific npc
                var loopTo1 = (int)Core.Type.Map[mapNum].MaxX;
                for (x = 0; x < (int)loopTo1; x++)
                {
                    var loopTo2 = (int)Core.Type.Map[mapNum].MaxY;
                    for (y = 0; y < (int)loopTo2; y++)
                    {
                        if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.NPCSpawn)
                        {
                            if (Core.Type.Map[mapNum].Tile[x, y].Data1 == MapNPCNum)
                            {
                                Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X = (byte)x;
                                Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y = (byte)y;
                                Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Dir = Core.Type.Map[mapNum].Tile[x, y].Data2;
                                spawned = true;
                                break;
                            }
                        }
                    }
                }

                if (!spawned)
                {
                    // Well try 100 times to randomly place the sprite
                    while (i < 1000)
                    {
                        x = (int)Math.Round(General.GetRandom.NextDouble(0d, Core.Type.Map[mapNum].MaxX - 1));
                        y = (int)Math.Round(General.GetRandom.NextDouble(0d, Core.Type.Map[mapNum].MaxY - 1));

                        if (x > Core.Type.Map[mapNum].MaxX)
                            x = Core.Type.Map[mapNum].MaxX - 1;
                        if (y > Core.Type.Map[mapNum].MaxY)
                            y = Core.Type.Map[mapNum].MaxY - 1;

                        // Check if the tile is walkable
                        if (NPCTileIsOpen(mapNum, x, y))
                        {
                            Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X = (byte)x;
                            Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y = (byte)y;
                            spawned = true;
                            break;
                        }
                        i += 0;
                    }
                }

                // Didn't spawn, so now we'll just try to find a free tile
                if (!spawned)
                {
                    var loopTo3 = (int)Core.Type.Map[mapNum].MaxX;
                    for (x = 0; x < (int)loopTo3; x++)
                    {
                        var loopTo4 = (int)Core.Type.Map[mapNum].MaxY;
                        for (y = 0; y < (int)loopTo4; y++)
                        {
                            if (NPCTileIsOpen(mapNum, x, y))
                            {
                                Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X = (byte)x;
                                Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y = (byte)y;
                                spawned = true;
                            }
                        }
                    }
                }

                // If we suceeded in spawning then send it to everyone
                if (spawned)
                {
                    buffer.WriteInt32((int)ServerPackets.SSpawnNPC);
                    buffer.WriteInt32((int)MapNPCNum);
                    buffer.WriteInt32((int)Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Num);
                    buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X);
                    buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y);
                    buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Dir);

                    var loopTo5 = (int)VitalType.Count;
                    for (i = 0; i < loopTo5; i++)
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Vital[i]);

                    NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
                }

                SendMapNPCVitals(mapNum, (byte)MapNPCNum);
            }

            buffer.Dispose();
        }

        #region Movement

        public static bool NPCTileIsOpen(int mapNum, int x, int y)
        {
            bool NPCTileIsOpenRet = default;
            int i;
            NPCTileIsOpenRet = true;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i < loopTo; i++)
            {
                if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == x & GetPlayerY(i) == y)
                {
                    NPCTileIsOpenRet = false;
                    return NPCTileIsOpenRet;
                }
            }        

            for (int LoopI = 0, loopTo1 = Core.Constant.MAX_MAP_NPCS; LoopI < loopTo1; LoopI++)
            {
                if (Core.Type.MapNPC[mapNum].NPC[LoopI].Num >= 0 & Core.Type.MapNPC[mapNum].NPC[LoopI].X == x & Core.Type.MapNPC[mapNum].NPC[LoopI].Y == y)
                {
                    NPCTileIsOpenRet = false;
                    return NPCTileIsOpenRet;
                }
            }

            if (Core.Type.Map[mapNum].Tile[x, y].Type != TileType.NPCSpawn & Core.Type.Map[mapNum].Tile[x, y].Type != TileType.Item & Core.Type.Map[mapNum].Tile[x, y].Type != TileType.None & Core.Type.Map[mapNum].Tile[x, y].Type2 != TileType.NPCSpawn & Core.Type.Map[mapNum].Tile[x, y].Type2 != TileType.Item & Core.Type.Map[mapNum].Tile[x, y].Type2 != TileType.None)
            {
                NPCTileIsOpenRet = false;
            }

            return NPCTileIsOpenRet;

        }

        public static bool CanNPCMove(int mapNum, int MapNPCNum, byte Dir)
        {
            bool CanNPCMoveRet = default;
            int i;
            int n;
            int n2;
            int x;
            int y;

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | MapNPCNum < 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS | Dir < (byte) DirectionType.Up | Dir > (byte) DirectionType.DownRight)
            {
                return CanNPCMoveRet;
            }

            x = Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X;
            y = Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y;
            CanNPCMoveRet = true;

            switch (Dir)
            {
                case (byte) DirectionType.Up:
                    {
                        // Check to make sure not outside of boundaries
                        if (y > 0)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x, y - 1].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x, y - 1].Type2;

                            // Check to make sure that the tile is walkable
                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NPCSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NPCSpawn)
                            {
                                CanNPCMoveRet = false;
                                return CanNPCMoveRet;
                            }

                            // Check to make sure that there is not a player in the way
                            var loopTo = NetworkConfig.Socket.HighIndex;
                            for (i = 0; i < loopTo; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X & GetPlayerY(i) == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y - 1)
                                    {
                                        CanNPCMoveRet = false;
                                        return CanNPCMoveRet;
                                    }
                                }
                            }

                            // Check to make sure that there is not another npc in the way
                            var loopTo1 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo1; i++)
                            {
                                if (i != MapNPCNum & Core.Type.MapNPC[mapNum].NPC[i].Num >= 0 & Core.Type.MapNPC[mapNum].NPC[i].X == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X & Core.Type.MapNPC[mapNum].NPC[i].Y == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y - 1)
                                {
                                    CanNPCMoveRet = false;
                                    return CanNPCMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNPCMoveRet = false;
                        }

                        break;
                    }

                case (byte) DirectionType.Down:
                    {
                        // Check to make sure not outside of boundaries
                        if (y < Core.Type.Map[mapNum].MaxY)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x, y + 1].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x, y + 1].Type2;

                            // Check to make sure that the tile is walkable
                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NPCSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NPCSpawn)
                            {
                                CanNPCMoveRet = false;
                                return CanNPCMoveRet;
                            }

                            // Check to make sure that there is not a player in the way
                            var loopTo2 = NetworkConfig.Socket.HighIndex;
                            for (i = 0; i < loopTo2; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X & GetPlayerY(i) == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y + 1)
                                    {
                                        CanNPCMoveRet = false;
                                        return CanNPCMoveRet;
                                    }
                                }
                            }

                            // Check to make sure that there is not another npc in the way
                            var loopTo3 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo3; i++)
                            {
                                if (i != MapNPCNum & Core.Type.MapNPC[mapNum].NPC[i].Num >= 0 & Core.Type.MapNPC[mapNum].NPC[i].X == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X & Core.Type.MapNPC[mapNum].NPC[i].Y == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y + 1)
                                {
                                    CanNPCMoveRet = false;
                                    return CanNPCMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNPCMoveRet = false;
                        }

                        break;
                    }

                case (byte)DirectionType.Left:
                    {
                        // Check to make sure not outside of boundaries
                        if (x > 0)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x - 1, y].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x - 1, y].Type2;

                            // Check to make sure that the tile is walkable
                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NPCSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NPCSpawn)
                            {
                                CanNPCMoveRet = false;
                                return CanNPCMoveRet;
                            }

                            // Check to make sure that there is not a player in the way
                            var loopTo4 = NetworkConfig.Socket.HighIndex;
                            for (i = 0; i < loopTo4; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X - 1 & GetPlayerY(i) == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y)
                                    {
                                        CanNPCMoveRet = false;
                                        return CanNPCMoveRet;
                                    }
                                }
                            }

                            // Check to make sure that there is not another npc in the way
                            var loopTo5 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo5; i++)
                            {
                                if (i != MapNPCNum & Core.Type.MapNPC[mapNum].NPC[i].Num >= 0 & Core.Type.MapNPC[mapNum].NPC[i].X == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X - 1 & Core.Type.MapNPC[mapNum].NPC[i].Y == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y)
                                {
                                    CanNPCMoveRet = false;
                                    return CanNPCMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNPCMoveRet = false;
                        }

                        break;
                    }

                case (byte)DirectionType.Right:
                    {
                        // Check to make sure not outside of boundaries
                        if (x < Core.Type.Map[mapNum].MaxX)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x + 1, y].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x + 1, y].Type2;

                            // Check to make sure that the tile is walkable
                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NPCSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NPCSpawn)
                            {
                                CanNPCMoveRet = false;
                                return CanNPCMoveRet;
                            }

                            // Check to make sure that there is not a player in the way
                            var loopTo6 = NetworkConfig.Socket.HighIndex;
                            for (i = 0; i < loopTo6; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X + 1 & GetPlayerY(i) == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y)
                                    {
                                        CanNPCMoveRet = false;
                                        return CanNPCMoveRet;
                                    }
                                }
                            }

                            // Check to make sure that there is not another npc in the way
                            var loopTo7 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo7; i++)
                            {
                                if (i != MapNPCNum & Core.Type.MapNPC[mapNum].NPC[i].Num >= 0 & Core.Type.MapNPC[mapNum].NPC[i].X == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X + 1 & Core.Type.MapNPC[mapNum].NPC[i].Y == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y)
                                {
                                    CanNPCMoveRet = false;
                                    return CanNPCMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNPCMoveRet = false;
                        }

                        break;
                    }

            }

            if (Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].SkillBuffer >= 0)
                CanNPCMoveRet = false;
            return CanNPCMoveRet;

        }

        public static void NPCMove(int mapNum, int MapNPCNum, int Dir, int Movement)
        {
            var buffer = new ByteStream(4);

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | MapNPCNum < 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS | Dir < (byte)DirectionType.Up | Dir > (byte) DirectionType.Left | Movement < 0 | Movement > 2)
            {
                return;
            }

            Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Dir = Dir;

            switch (Dir)
            {
                case  (byte) DirectionType.Up:
                    {
                        Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y = (byte)(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y - 1);

                        buffer.WriteInt32((int) ServerPackets.SNPCMove);
                        buffer.WriteInt32((int)MapNPCNum);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
                        break;
                    }
                case (byte) DirectionType.Down:
                    {
                        Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y = (byte)(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y + 1);

                        buffer.WriteInt32((int) ServerPackets.SNPCMove);
                        buffer.WriteInt32((int)MapNPCNum);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
                        break;
                    }
                case (byte) DirectionType.Left:
                    {
                        Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X = (byte)(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X - 1);

                        buffer.WriteInt32((int) ServerPackets.SNPCMove);
                        buffer.WriteInt32((int)MapNPCNum);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
                        break;
                    }
                case (byte) DirectionType.Right:
                    {
                        Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X = (byte)(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X + 1);

                        buffer.WriteInt32((int) ServerPackets.SNPCMove);
                        buffer.WriteInt32((int)MapNPCNum);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
                        break;
                    }
            }

            buffer.Dispose();
        }

        public static void NPCDir(int mapNum, int MapNPCNum, int Dir)
        {
            var buffer = new ByteStream(4);

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | MapNPCNum < 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS | Dir < (byte)DirectionType.Up | Dir > (byte) DirectionType.Left)
            {
                return;
            }

            Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Dir = Dir;

            buffer.WriteInt32((int) ServerPackets.SNPCDir);
            buffer.WriteInt32((int)MapNPCNum);
            buffer.WriteInt32(Dir);

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        #endregion

        #region Outgoing Packets

        public static void SendMapNPCsToMap(int mapNum)
        {
            int i;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapNPCData);

            var loopTo = Core.Constant.MAX_MAP_NPCS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32((int)Core.Type.MapNPC[mapNum].NPC[i].Num);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].X);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Y);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Dir);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(byte) VitalType.HP]);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(byte) VitalType.SP]);
            }

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        #endregion

        #region Incoming Packets

        public static void Packet_RequestEditNPC(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.NPC);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            Core.Type.TempPlayer[index].Editor = (byte) EditorType.NPC;

            Item.SendItems(index);
            Animation.SendAnimations(index);
            NetworkSend.SendSkills(index);
            SendNPCs(index);

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SNPCEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void Packet_SaveNPC(int index, ref byte[] data)
        {
            int NPCNum;
            int i;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            NPCNum = buffer.ReadInt32();

            // Update the NPC
            Core.Type.NPC[(int)NPCNum].Animation = buffer.ReadInt32();
            Core.Type.NPC[(int)NPCNum].AttackSay = buffer.ReadString();
            Core.Type.NPC[(int)NPCNum].Behaviour = buffer.ReadByte();

            var loopTo = Core.Constant.MAX_DROP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                Core.Type.NPC[(int)NPCNum].DropChance[i] = buffer.ReadInt32();
                Core.Type.NPC[(int)NPCNum].DropItem[i] = buffer.ReadInt32();
                Core.Type.NPC[(int)NPCNum].DropItemValue[i] = buffer.ReadInt32();
            }

            Core.Type.NPC[(int)NPCNum].Exp = buffer.ReadInt32();
            Core.Type.NPC[(int)NPCNum].Faction = buffer.ReadByte();
            Core.Type.NPC[(int)NPCNum].HP = buffer.ReadInt32();
            Core.Type.NPC[(int)NPCNum].Name = buffer.ReadString();
            Core.Type.NPC[(int)NPCNum].Range = buffer.ReadByte();
            Core.Type.NPC[(int)NPCNum].SpawnTime = buffer.ReadByte();
            Core.Type.NPC[(int)NPCNum].SpawnSecs = buffer.ReadInt32();
            Core.Type.NPC[(int)NPCNum].Sprite = buffer.ReadInt32();

            var loopTo1 = (byte)StatType.Count;
            for (i = 0; i < loopTo1; i++)
                Core.Type.NPC[(int)NPCNum].Stat[i] = buffer.ReadByte();

            var loopTo2 = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo2; i++)
                Core.Type.NPC[(int)NPCNum].Skill[i] = buffer.ReadByte();

            Core.Type.NPC[(int)NPCNum].Level = buffer.ReadByte();
            Core.Type.NPC[(int)NPCNum].Damage = buffer.ReadInt32();

            // Save it
            SendUpdateNPCToAll(NPCNum);
            Database.SaveNPC(NPCNum);
            Core.Log.Add(GetAccountLogin(index) + " saved NPC #" + NPCNum + ".", Constant.ADMIN_LOG);

            buffer.Dispose();
        }

        public static void SendNPCs(int index)
        {
            int i;

            var loopTo = Core.Constant.MAX_NPCS;
            for (i = 0; i < loopTo; i++)
            {
                if (Strings.Len(Core.Type.NPC[i].Name) > 0)
                {
                    SendUpdateNPCTo(index, i);
                }
            }

        }

        public static void SendUpdateNPCTo(int index, int NPCNum)
        {
            ByteStream buffer;
            int i;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SUpdateNPC);

            buffer.WriteInt32(NPCNum);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Animation);
            buffer.WriteString(Core.Type.NPC[(int)NPCNum].AttackSay);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Behaviour);

            var loopTo = Core.Constant.MAX_DROP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropChance[i]);
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropItem[i]);
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropItemValue[i]);
            }

            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Exp);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Faction);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].HP);
            buffer.WriteString(Core.Type.NPC[(int)NPCNum].Name);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Range);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].SpawnTime);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].SpawnSecs);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Sprite);

            var loopTo1 = (byte)StatType.Count;
            for (i = 0; i < loopTo1; i++)
                buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Stat[i]);

            var loopTo2 = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo2; i++)
                buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Skill[i]);

            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Level);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Damage);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateNPCToAll(int NPCNum)
        {
            ByteStream buffer;
            int i;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SUpdateNPC);

            buffer.WriteInt32(NPCNum);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Animation);
            buffer.WriteString(Core.Type.NPC[(int)NPCNum].AttackSay);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Behaviour);

            var loopTo = Core.Constant.MAX_DROP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropChance[i]);
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropItem[i]);
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropItemValue[i]);
            }

            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Exp);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Faction);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].HP);
            buffer.WriteString(Core.Type.NPC[(int)NPCNum].Name);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Range);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].SpawnTime);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].SpawnSecs);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Sprite);

            var loopTo1 = (byte)StatType.Count;
            for (i = 0; i < loopTo1; i++)
                buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Stat[i]);

            var loopTo2 = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo2; i++)
                buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Skill[i]);

            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Level);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Damage);

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendMapNPCsTo(int index, int mapNum)
        {
            int i;
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapNPCData);

            var loopTo = Core.Constant.MAX_MAP_NPCS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32((int)Core.Type.MapNPC[mapNum].NPC[i].Num);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].X);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Y);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Dir);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(byte) VitalType.HP]);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(byte) VitalType.SP]);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendMapNPCTo(int mapNum, int MapNPCNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapNPCUpdate);

            buffer.WriteInt32((int)MapNPCNum);

            var withBlock = Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum];
            buffer.WriteInt32((int)withBlock.Num);
            buffer.WriteInt32(withBlock.X);
            buffer.WriteInt32(withBlock.Y);
            buffer.WriteInt32(withBlock.Dir);
            buffer.WriteInt32(withBlock.Vital[(byte) VitalType.HP]);
            buffer.WriteInt32(withBlock.Vital[(byte) VitalType.SP]);

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendMapNPCVitals(int mapNum, byte MapNPCNum)
        {
            int i;
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapNPCVitals);
            buffer.WriteInt32(MapNPCNum);

            var loopTo = VitalType.Count;
            for (i = 0; i < (int)loopTo; i++)
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Vital[i]);

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendNPCAttack(int index, int NPCNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SAttack);

            buffer.WriteInt32(NPCNum);
            NetworkConfig.SendDataToMap(GetPlayerMap(index), buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendNPCDead(int mapNum, int index)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SNPCDead);

            buffer.WriteInt32(index);
            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        #endregion

    }
}