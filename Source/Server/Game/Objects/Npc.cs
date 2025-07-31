using System;
using System.Collections.Generic;
using System.Linq;
using static Core.Global.Command;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Packets;
using static Core.Type;
using System.Reflection;
using Core;

namespace Server
{

    public class Npc
    {

        public static async System.Threading.Tasks.Task SpawnAllMapNpcs()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_MAP_NPCS).Select(i => System.Threading.Tasks.Task.Run(() => SpawnMapNpcs(i)));
            await System.Threading.Tasks.Task.WhenAll(tasks);

        }

        public static async System.Threading.Tasks.Task SpawnMapNpcs(int mapNum)
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_MAP_NPCS).Select(i => System.Threading.Tasks.Task.Run(() => SpawnNpc(i, mapNum)));
            await System.Threading.Tasks.Task.WhenAll(tasks);

        }

        public static void SpawnNpc(int mapNpcNum, int mapNum)
        {
            var buffer = new ByteStream(4);
            int npcNum;
            int x;
            int y;
            int i = 0;
            var spawned = default(bool);

            if (Data.Map[mapNum].NoRespawn)
                return;

            npcNum = Data.Map[mapNum].Npc[mapNpcNum];

            if (mapNpcNum == 0 || npcNum < 0 || npcNum > Core.Constant.MAX_NPCS)
            {
                return;
            }

            if (!(Data.Npc[(int)npcNum].SpawnTime == (byte)Clock.Instance.TimeOfDay) & Data.Npc[(int)npcNum].SpawnTime != 0)
            {
                Database.ClearMapNpc(mapNpcNum, mapNum);
                SendMapNpcsToMap(mapNum);
                return;
            }

            Data.MapNpc[mapNum].Npc[mapNpcNum].Num = npcNum;
            Data.MapNpc[mapNum].Npc[mapNpcNum].Target = 0;
            Data.MapNpc[mapNum].Npc[mapNpcNum].TargetType = 0; // clear

            var loopTo = System.Enum.GetValues(typeof(Core.Vital)).Length;
            for (i = 0; i < (int)loopTo; i++)
                Data.MapNpc[mapNum].Npc[mapNpcNum].Vital[i] = GameLogic.GetNpcMaxVital(npcNum, (Core.Vital)i);

            Data.MapNpc[mapNum].Npc[mapNpcNum].Dir = (byte)(VBMath.Rnd() * 4f);

            // Check if theres a spawn tile for the specific npc
            var loopTo1 = (int)Data.Map[mapNum].MaxX;
            for (x = 0; x < (int)loopTo1; x++)
            {
                var loopTo2 = (int)Data.Map[mapNum].MaxY;
                for (y = 0; y < (int)loopTo2; y++)
                {
                    if (Data.Map[mapNum].Tile[x, y].Type == TileType.NpcSpawn)
                    {
                        if (Data.Map[mapNum].Tile[x, y].Data1 == mapNpcNum)
                        {
                            Data.MapNpc[mapNum].Npc[mapNpcNum].X = x * 32;
                            Data.MapNpc[mapNum].Npc[mapNpcNum].Dir = (byte)Data.Map[mapNum].Tile[x, y].Data2;
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
                    x = (int)Math.Round(General.GetRandom.NextDouble(0d, Data.Map[mapNum].MaxX - 1));
                    y = (int)Math.Round(General.GetRandom.NextDouble(0d, Data.Map[mapNum].MaxY - 1));

                    if (x > Data.Map[mapNum].MaxX)
                        x = Data.Map[mapNum].MaxX - 1;

                    if (y > Data.Map[mapNum].MaxY)
                        y = Data.Map[mapNum].MaxY - 1;

                    // Check if the tile is walkable
                    if (NpcTileIsOpen(mapNum, x, y))
                    {
                        Data.MapNpc[mapNum].Npc[mapNpcNum].X = x * 32;
                        Data.MapNpc[mapNum].Npc[mapNpcNum].Y = y * 32;
                        spawned = true;
                        break;
                    }
                    i += 1;
                }
            }

            // Didn't spawn, so now we'll just try to find a free tile
            if (!spawned)
            {
                var loopTo3 = (int)Data.Map[mapNum].MaxX;
                for (x = 0; x < (int)loopTo3; x++)
                {
                    var loopTo4 = (int)Data.Map[mapNum].MaxY;
                    for (y = 0; y < (int)loopTo4; y++)
                    {
                        if (NpcTileIsOpen(mapNum, x, y))
                        {
                            Data.MapNpc[mapNum].Npc[mapNpcNum].X = x * 32;
                            Data.MapNpc[mapNum].Npc[mapNpcNum].Y = y * 32;
                            spawned = true;
                        }
                    }
                }
            }

            // If we suceeded in spawning then send it to everyone
            if (spawned)
            {
                buffer.WriteInt32((int)ServerPackets.SSpawnNpc);
                buffer.WriteInt32(mapNpcNum);
                buffer.WriteInt32((int)Data.MapNpc[mapNum].Npc[mapNpcNum].Num);
                buffer.WriteInt32(Data.MapNpc[mapNum].Npc[mapNpcNum].X);
                buffer.WriteInt32(Data.MapNpc[mapNum].Npc[mapNpcNum].Y);
                buffer.WriteByte(Data.MapNpc[mapNum].Npc[mapNpcNum].Dir);

                var loopTo5 = (int)System.Enum.GetValues(typeof(Core.Vital)).Length;
                for (i = 0; i < loopTo5; i++)
                    buffer.WriteInt32(Data.MapNpc[mapNum].Npc[mapNpcNum].Vital[i]);

                NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            }

            SendMapNpcVitals(mapNum, (byte)mapNpcNum);

            buffer.Dispose();
        }

        #region Movement

        public static bool NpcTileIsOpen(int mapNum, int x, int y)
        {
            bool NpcTileIsOpenRet = default;
            int i;
            NpcTileIsOpenRet = true;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i < loopTo; i++)
            {
                if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == x & GetPlayerY(i) == y)
                {
                    NpcTileIsOpenRet = false;
                    return NpcTileIsOpenRet;
                }
            }        

            for (int LoopI = 0, loopTo1 = Core.Constant.MAX_MAP_NPCS; LoopI < loopTo1; LoopI++)
            {
                if (Data.MapNpc[mapNum].Npc[LoopI].Num >= 0 & Data.MapNpc[mapNum].Npc[LoopI].X == x & Data.MapNpc[mapNum].Npc[LoopI].Y == y)
                {
                    NpcTileIsOpenRet = false;
                    return NpcTileIsOpenRet;
                }
            }

            if (Data.Map[mapNum].Tile[x, y].Type != TileType.NpcSpawn & Data.Map[mapNum].Tile[x, y].Type != TileType.Item & Data.Map[mapNum].Tile[x, y].Type != TileType.None & Data.Map[mapNum].Tile[x, y].Type2 != TileType.NpcSpawn & Data.Map[mapNum].Tile[x, y].Type2 != TileType.Item & Data.Map[mapNum].Tile[x, y].Type2 != TileType.None)
            {
                NpcTileIsOpenRet = false;
            }

            return NpcTileIsOpenRet;

        }

        public static bool CanNpcMove(int mapNum, int MapNpcNum, byte Dir)
        {
            bool CanNpcMoveRet = default;
            int i;
            int n;
            int n2;
            int x;
            int y;
            int tileX;
            int tileY;
            int npcX;
            int npcY;

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | MapNpcNum < 0 | MapNpcNum >= Core.Constant.MAX_MAP_NPCS | Dir < (byte)Direction.Up | Dir > (byte)Direction.DownRight)
            {
                return CanNpcMoveRet;
            }

            x = Data.MapNpc[mapNum].Npc[MapNpcNum].X;
            y = Data.MapNpc[mapNum].Npc[MapNpcNum].Y;
            tileX = (int)Math.Floor((double)x / 32);
            tileY = (int)Math.Floor((double)y / 32);

            CanNpcMoveRet = true;

            switch (Dir)
            {
                case (byte)Direction.Up:
                    {
                        // Check to make sure not outside of boundaries
                        if (tileY > 0)
                        {
                            n = (int)Data.Map[mapNum].Tile[tileX, tileY - 1].Type;
                            n2 = (int)Data.Map[mapNum].Tile[tileX, tileY - 1].Type2;

                            // Check to make sure that the tile is walkable
                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NpcSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NpcSpawn)
                            {
                                CanNpcMoveRet = false;
                                return CanNpcMoveRet;
                            }

                            // Check to make sure that there is not a player in the way
                            var loopTo = NetworkConfig.Socket.HighIndex;
                            for (i = 0; i < loopTo; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == tileX & GetPlayerY(i) == tileY - 1)
                                    {
                                        CanNpcMoveRet = false;
                                        return CanNpcMoveRet;
                                    }
                                }
                            }

                            // Check to make sure that there is not another npc in the way
                            var loopTo1 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo1; i++)
                            {
                                npcX = (int)Math.Floor((double)Data.MapNpc[mapNum].Npc[i].X / 32);
                                npcY = (int)Math.Floor((double)Data.MapNpc[mapNum].Npc[i].Y / 32);
                                if (i != MapNpcNum & Data.MapNpc[mapNum].Npc[i].Num >= 0 & npcX == tileX & npcY == tileY - 1)
                                {
                                    CanNpcMoveRet = false;
                                    return CanNpcMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNpcMoveRet = false;
                        }

                        break;
                    }

                case (byte)Direction.Down:
                    {
                        if (tileY < Data.Map[mapNum].MaxY - 1)
                        {
                            n = (int)Data.Map[mapNum].Tile[tileX, tileY + 1].Type;
                            n2 = (int)Data.Map[mapNum].Tile[tileX, tileY + 1].Type2;

                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NpcSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NpcSpawn)
                            {
                                CanNpcMoveRet = false;
                                return CanNpcMoveRet;
                            }

                            var loopTo2 = NetworkConfig.Socket.HighIndex;
                            for (i = 0; i < loopTo2; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == tileX & GetPlayerY(i) == tileY + 1)
                                    {
                                        CanNpcMoveRet = false;
                                        return CanNpcMoveRet;
                                    }
                                }
                            }

                            var loopTo3 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo3; i++)
                            {
                                npcX = (int)Math.Floor((double)Data.MapNpc[mapNum].Npc[i].X / 32);
                                npcY = (int)Math.Floor((double)Data.MapNpc[mapNum].Npc[i].Y / 32);
                                if (i != MapNpcNum & Data.MapNpc[mapNum].Npc[i].Num >= 0 & npcX == tileX & npcY == tileY + 1)
                                {
                                    CanNpcMoveRet = false;
                                    return CanNpcMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNpcMoveRet = false;
                        }

                        break;
                    }

                case (byte)Direction.Left:
                    {
                        if (tileX > 0)
                        {
                            n = (int)Data.Map[mapNum].Tile[tileX - 1, tileY].Type;
                            n2 = (int)Data.Map[mapNum].Tile[tileX - 1, tileY].Type2;

                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NpcSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NpcSpawn)
                            {
                                CanNpcMoveRet = false;
                                return CanNpcMoveRet;
                            }

                            var loopTo4 = NetworkConfig.Socket.HighIndex;
                            for (i = 0; i < loopTo4; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == tileX - 1 & GetPlayerY(i) == tileY)
                                    {
                                        CanNpcMoveRet = false;
                                        return CanNpcMoveRet;
                                    }
                                }
                            }

                            var loopTo5 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo5; i++)
                            {
                                npcX = (int)Math.Floor((double)Data.MapNpc[mapNum].Npc[i].X / 32);
                                npcY = (int)Math.Floor((double)Data.MapNpc[mapNum].Npc[i].Y / 32);
                                if (i != MapNpcNum & Data.MapNpc[mapNum].Npc[i].Num >= 0 & npcX == tileX - 1 & npcY == tileY)
                                {
                                    CanNpcMoveRet = false;
                                    return CanNpcMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNpcMoveRet = false;
                        }

                        break;
                    }

                case (byte)Direction.Right:
                    {
                        if (tileX < Data.Map[mapNum].MaxX - 1)
                        {
                            n = (int)Data.Map[mapNum].Tile[tileX + 1, tileY].Type;
                            n2 = (int)Data.Map[mapNum].Tile[tileX + 1, tileY].Type2;

                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NpcSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NpcSpawn)
                            {
                                CanNpcMoveRet = false;
                                return CanNpcMoveRet;
                            }

                            var loopTo6 = NetworkConfig.Socket.HighIndex;
                            for (i = 0; i < loopTo6; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == tileX + 1 & GetPlayerY(i) == tileY)
                                    {
                                        CanNpcMoveRet = false;
                                        return CanNpcMoveRet;
                                    }
                                }
                            }

                            var loopTo7 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo7; i++)
                            {
                                npcX = (int)Math.Floor((double)Data.MapNpc[mapNum].Npc[i].X / 32);
                                npcY = (int)Math.Floor((double)Data.MapNpc[mapNum].Npc[i].Y / 32);
                                if (i != MapNpcNum & Data.MapNpc[mapNum].Npc[i].Num >= 0 & npcX == tileX + 1 & npcY == tileY)
                                {
                                    CanNpcMoveRet = false;
                                    return CanNpcMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNpcMoveRet = false;
                        }

                        break;
                    }
            }

            if (Data.MapNpc[mapNum].Npc[MapNpcNum].SkillBuffer >= 0)
                CanNpcMoveRet = false;

            return CanNpcMoveRet;
        }

        public static void NpcMove(int mapNum, int MapNpcNum, byte Dir, int Movement)
        {
            var buffer = new ByteStream(4);

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | MapNpcNum < 0 | MapNpcNum > Core.Constant.MAX_MAP_NPCS | Dir < (byte)Direction.Up | Dir > (byte) Direction.DownRight | Movement < 0 | Movement > 2)
            {
                return;
            }

            Data.MapNpc[mapNum].Npc[MapNpcNum].Dir = Dir;

            switch (Dir)
            {
                case  (byte) Direction.Up:
                    {
                        Data.MapNpc[mapNum].Npc[MapNpcNum].Y -= 1;

                        buffer.WriteInt32((int)ServerPackets.SNpcMove);
                        buffer.WriteInt32(MapNpcNum);
                        buffer.WriteInt32(Data.MapNpc[mapNum].Npc[MapNpcNum].X);
                        buffer.WriteInt32(Data.MapNpc[mapNum].Npc[MapNpcNum].Y);
                        buffer.WriteByte(Data.MapNpc[mapNum].Npc[MapNpcNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
                        break;
                    }
                case (byte) Direction.Down:
                    {
                        Data.MapNpc[mapNum].Npc[MapNpcNum].Y += 1;

                        buffer.WriteInt32((int) ServerPackets.SNpcMove);
                        buffer.WriteInt32(MapNpcNum);
                        buffer.WriteInt32(Data.MapNpc[mapNum].Npc[MapNpcNum].X);
                        buffer.WriteInt32(Data.MapNpc[mapNum].Npc[MapNpcNum].Y);
                        buffer.WriteByte(Data.MapNpc[mapNum].Npc[MapNpcNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
                        break;
                    }
                case (byte) Direction.Left:
                    {
                        Data.MapNpc[mapNum].Npc[MapNpcNum].X -= 1;

                        buffer.WriteInt32((int) ServerPackets.SNpcMove);
                        buffer.WriteInt32(MapNpcNum);
                        buffer.WriteInt32(Data.MapNpc[mapNum].Npc[MapNpcNum].X);
                        buffer.WriteInt32(Data.MapNpc[mapNum].Npc[MapNpcNum].Y);
                        buffer.WriteByte(Data.MapNpc[mapNum].Npc[MapNpcNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
                        break;
                    }
                case (byte) Direction.Right:
                    {
                        Data.MapNpc[mapNum].Npc[MapNpcNum].X += 1;

                        buffer.WriteInt32((int) ServerPackets.SNpcMove);
                        buffer.WriteInt32(MapNpcNum);
                        buffer.WriteInt32(Data.MapNpc[mapNum].Npc[MapNpcNum].X);
                        buffer.WriteInt32(Data.MapNpc[mapNum].Npc[MapNpcNum].Y);
                        buffer.WriteByte(Data.MapNpc[mapNum].Npc[MapNpcNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
                        break;
                    }
            }

            buffer.Dispose();
        }

        public static void NpcDir(int mapNum, int MapNpcNum, byte Dir)
        {
            var buffer = new ByteStream(4);

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | MapNpcNum < 0 | MapNpcNum > Core.Constant.MAX_MAP_NPCS | Dir < (byte)Direction.Up | Dir > (byte) Direction.DownRight)
            {
                return;
            }

            Data.MapNpc[mapNum].Npc[MapNpcNum].Dir = Dir;

            buffer.WriteInt32((int) ServerPackets.SNpcDir);
            buffer.WriteInt32(MapNpcNum);
            buffer.WriteByte(Dir);

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        #endregion

        #region Outgoing Packets

        public static void SendMapNpcsToMap(int mapNum)
        {
            int i;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapNpcData);

            var loopTo = Core.Constant.MAX_MAP_NPCS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32((int)Data.MapNpc[mapNum].Npc[i].Num);
                buffer.WriteInt32(Data.MapNpc[mapNum].Npc[i].X);
                buffer.WriteInt32(Data.MapNpc[mapNum].Npc[i].Y);
                buffer.WriteByte(Data.MapNpc[mapNum].Npc[i].Dir);
            }

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        #endregion

        #region Incoming Packets

        public static void Packet_RequestEditNpc(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Npc);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) Color.BrightRed);
                return;
            }

            Core.Data.TempPlayer[index].Editor = (byte) EditorType.Npc;

            Item.SendItems(index);
            Animation.SendAnimations(index);
            NetworkSend.SendSkills(index);
            SendNpcs(index);

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SNpcEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void Packet_SaveNpc(int index, ref byte[] data)
        {
            int NpcNum;
            int i;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            NpcNum = buffer.ReadInt32();

            // Update the Npc
            Data.Npc[(int)NpcNum].Animation = buffer.ReadInt32();
            Data.Npc[(int)NpcNum].AttackSay = buffer.ReadString();
            Data.Npc[(int)NpcNum].Behaviour = buffer.ReadByte();

            var loopTo = Core.Constant.MAX_DROP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                Data.Npc[(int)NpcNum].DropChance[i] = buffer.ReadInt32();
                Data.Npc[(int)NpcNum].DropItem[i] = buffer.ReadInt32();
                Data.Npc[(int)NpcNum].DropItemValue[i] = buffer.ReadInt32();
            }

            Data.Npc[(int)NpcNum].Exp = buffer.ReadInt32();
            Data.Npc[(int)NpcNum].Faction = buffer.ReadByte();
            Data.Npc[(int)NpcNum].HP = buffer.ReadInt32();
            Data.Npc[(int)NpcNum].Name = buffer.ReadString();
            Data.Npc[(int)NpcNum].Range = buffer.ReadByte();
            Data.Npc[(int)NpcNum].SpawnTime = buffer.ReadByte();
            Data.Npc[(int)NpcNum].SpawnSecs = buffer.ReadInt32();
            Data.Npc[(int)NpcNum].Sprite = buffer.ReadInt32();

            int loopTo1 = Enum.GetValues(typeof(Core.Stat)).Length;
            for (i = 0; i < loopTo1; i++)
                Data.Npc[(int)NpcNum].Stat[i] = buffer.ReadByte();

            var loopTo2 = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo2; i++)
                Data.Npc[(int)NpcNum].Skill[i] = buffer.ReadByte();

            Data.Npc[(int)NpcNum].Level = buffer.ReadByte();
            Data.Npc[(int)NpcNum].Damage = buffer.ReadInt32();

            // Save it
            SendUpdateNpcToAll(NpcNum);
            Database.SaveNpc(NpcNum);
            Core.Log.Add(GetAccountLogin(index) + " saved Npc #" + NpcNum + ".", Constant.ADMIN_LOG);

            buffer.Dispose();
        }

        public static void SendNpcs(int index)
        {
            int i;

            var loopTo = Core.Constant.MAX_NPCS;
            for (i = 0; i < loopTo; i++)
            {
                if (Strings.Len(Data.Npc[i].Name) > 0)
                {
                    SendUpdateNpcTo(index, i);
                }
            }

        }

        public static void SendUpdateNpcTo(int index, int NpcNum)
        {
            ByteStream buffer;
            int i;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SUpdateNpc);

            buffer.WriteInt32(NpcNum);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].Animation);
            buffer.WriteString(Data.Npc[(int)NpcNum].AttackSay);
            buffer.WriteByte(Data.Npc[(int)NpcNum].Behaviour);

            var loopTo = Core.Constant.MAX_DROP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32(Data.Npc[(int)NpcNum].DropChance[i]);
                buffer.WriteInt32(Data.Npc[(int)NpcNum].DropItem[i]);
                buffer.WriteInt32(Data.Npc[(int)NpcNum].DropItemValue[i]);
            }

            buffer.WriteInt32(Data.Npc[(int)NpcNum].Exp);
            buffer.WriteByte(Data.Npc[(int)NpcNum].Faction);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].HP);
            buffer.WriteString(Data.Npc[(int)NpcNum].Name);
            buffer.WriteByte(Data.Npc[(int)NpcNum].Range);
            buffer.WriteByte(Data.Npc[(int)NpcNum].SpawnTime);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].SpawnSecs);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].Sprite);

            int loopTo1 = Enum.GetValues(typeof(Core.Stat)).Length;
            for (i = 0; i < loopTo1; i++)
                buffer.WriteByte(Data.Npc[(int)NpcNum].Stat[i]);

            var loopTo2 = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo2; i++)
                buffer.WriteByte(Data.Npc[(int)NpcNum].Skill[i]);

            buffer.WriteInt32(Data.Npc[(int)NpcNum].Level);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].Damage);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateNpcToAll(int NpcNum)
        {
            ByteStream buffer;
            int i;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SUpdateNpc);

            buffer.WriteInt32(NpcNum);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].Animation);
            buffer.WriteString(Data.Npc[(int)NpcNum].AttackSay);
            buffer.WriteByte(Data.Npc[(int)NpcNum].Behaviour);

            var loopTo = Core.Constant.MAX_DROP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32(Data.Npc[(int)NpcNum].DropChance[i]);
                buffer.WriteInt32(Data.Npc[(int)NpcNum].DropItem[i]);
                buffer.WriteInt32(Data.Npc[(int)NpcNum].DropItemValue[i]);
            }

            buffer.WriteInt32(Data.Npc[(int)NpcNum].Exp);
            buffer.WriteByte(Data.Npc[(int)NpcNum].Faction);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].HP);
            buffer.WriteString(Data.Npc[(int)NpcNum].Name);
            buffer.WriteByte(Data.Npc[(int)NpcNum].Range);
            buffer.WriteByte(Data.Npc[(int)NpcNum].SpawnTime);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].SpawnSecs);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].Sprite);

            int loopTo1 = Enum.GetValues(typeof(Core.Stat)).Length;
            for (i = 0; i < loopTo1; i++)
                buffer.WriteByte(Data.Npc[(int)NpcNum].Stat[i]);

            var loopTo2 = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo2; i++)
                buffer.WriteByte(Data.Npc[(int)NpcNum].Skill[i]);

            buffer.WriteInt32(Data.Npc[(int)NpcNum].Level);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].Damage);

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendMapNpcsTo(int index, int mapNum)
        {
            int i;
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapNpcData);

            var loopTo = Core.Constant.MAX_MAP_NPCS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32((int)Data.MapNpc[mapNum].Npc[i].Num);
                buffer.WriteInt32(Data.MapNpc[mapNum].Npc[i].X);
                buffer.WriteInt32(Data.MapNpc[mapNum].Npc[i].Y);
                buffer.WriteByte(Data.MapNpc[mapNum].Npc[i].Dir);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendMapNpcTo(int mapNum, int MapNpcNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SMapNpcUpdate);

            buffer.WriteInt32(MapNpcNum);

            var withBlock = Data.MapNpc[mapNum].Npc[MapNpcNum];
            buffer.WriteInt32(withBlock.Num);
            buffer.WriteInt32(withBlock.X);
            buffer.WriteInt32(withBlock.Y);
            buffer.WriteByte(withBlock.Dir);

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendMapNpcVitals(int mapNum, byte MapNpcNum)
        {
            int i;
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapNpcVitals);
            buffer.WriteInt32(MapNpcNum);

            var loopTo = System.Enum.GetValues(typeof(Core.Vital)).Length;
            for (i = 0; i < (int)loopTo; i++)
                buffer.WriteInt32(Data.MapNpc[mapNum].Npc[MapNpcNum].Vital[i]);

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendNpcAttack(int index, int NpcNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SAttack);

            buffer.WriteInt32(NpcNum);
            NetworkConfig.SendDataToMap(GetPlayerMap(index), buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendNpcDead(int mapNum, int index)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SNpcDead);

            buffer.WriteInt32(index);
            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        #endregion

    }
}