using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common;
using static Core.Global.Command;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Enum;
using static Core.Packets;
using static Core.Type;

namespace Server
{

    static class NPC
    {

        #region Spawning

        public static void SpawnAllMapNPCs()
        {
            int i;

            var loopTo = Core.Constant.MAX_MAPS;
            for (i = 0; i < loopTo; i++)
                SpawnMapNPCs(i);

        }

        public static void SpawnMapNPCs(int mapNum)
        {
            int i;

            var loopTo = Core.Constant.MAX_MAP_NPCS;
            for (i = 0; i < loopTo; i++)
                SpawnNPC(i, mapNum);

        }

        internal static void SpawnNPC(int MapNPCNum, int mapNum)
        {
            var buffer = new ByteStream(4);
            int NPCNum;
            int x;
            int y;
            int i = 0;
            var spawned = default(bool);

            if (Core.Type.Map[mapNum].NoRespawn)
                return;

            NPCNum = Core.Type.Map[mapNum].NPC[MapNPCNum];

            if (NPCNum > 0)
            {
                if (!(Core.Type.NPC[NPCNum].SpawnTime == (byte)TimeType.Instance.TimeOfDay) & Core.Type.NPC[NPCNum].SpawnTime != 0)
                {
                    Database.ClearMapNPC(MapNPCNum, mapNum);
                    SendMapNPCsToMap(mapNum);
                    return;
                }

                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num = NPCNum;
                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Target = 0;
                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].TargetType = 0; // clear

                var loopTo = VitalType.Count;
                for (i = 0; i < (int)loopTo; i++)
                    Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Vital[i] = GameLogic.GetNPCMaxVital(NPCNum, (Core.Enum.VitalType)i);

                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Dir = (int)Conversion.Int(VBMath.Rnd() * 4f);

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
                                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X = (byte)x;
                                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y = (byte)y;
                                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Dir = Core.Type.Map[mapNum].Tile[x, y].Data2;
                                spawned = Conversions.ToBoolean(1);
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
                        x = (int)Math.Round(General.Random.NextDouble(0d, Core.Type.Map[mapNum].MaxX));
                        y = (int)Math.Round(General.Random.NextDouble(0d, Core.Type.Map[mapNum].MaxY));

                        if (x > Core.Type.Map[mapNum].MaxX)
                            x = Core.Type.Map[mapNum].MaxX;
                        if (y > Core.Type.Map[mapNum].MaxY)
                            y = Core.Type.Map[mapNum].MaxY;

                        // Check if the tile is walkable
                        if (NPCTileIsOpen(mapNum, x, y))
                        {
                            Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X = (byte)x;
                            Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y = (byte)y;
                            spawned = Conversions.ToBoolean(1);
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
                                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X = (byte)x;
                                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y = (byte)y;
                                spawned = Conversions.ToBoolean(1);
                            }
                        }
                    }
                }

                // If we suceeded in spawning then send it to everyone
                if (spawned)
                {
                    buffer.WriteInt32((int) ServerPackets.SSpawnNPC);
                    buffer.WriteInt32(MapNPCNum);
                    buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num);
                    buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X);
                    buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y);
                    buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Dir);

                    var loopTo5 = (int) VitalType.Count;
                    for (i = 0; i < loopTo5; i++)
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Vital[i]);

                    NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                }

                SendMapNPCVitals(mapNum, (byte)MapNPCNum);
            }

            buffer.Dispose();
        }

        #endregion

        #region Movement

        internal static bool NPCTileIsOpen(int mapNum, int x, int y)
        {
            bool NPCTileIsOpenRet = default;
            int i;
            NPCTileIsOpenRet = Conversions.ToBoolean(1);

            if (PlayersOnMap[mapNum])
            {
                var loopTo = NetworkConfig.Socket.HighIndex + 1;
                for (i = 0; i < loopTo; i++)
                {
                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == x & GetPlayerY(i) == y)
                    {
                        NPCTileIsOpenRet = Conversions.ToBoolean(0);
                        return NPCTileIsOpenRet;
                    }
                }
            }

            for (int LoopI = 0, loopTo1 = Core.Constant.MAX_MAP_NPCS; LoopI < loopTo1; LoopI++)
            {
                if (Core.Type.MapNPC[mapNum].NPC[LoopI].Num > 0 & Core.Type.MapNPC[mapNum].NPC[LoopI].X == x & Core.Type.MapNPC[mapNum].NPC[LoopI].Y == y)
                {
                    NPCTileIsOpenRet = Conversions.ToBoolean(0);
                    return NPCTileIsOpenRet;
                }
            }

            if (Core.Type.Map[mapNum].Tile[x, y].Type != TileType.NPCSpawn & Core.Type.Map[mapNum].Tile[x, y].Type != TileType.Item & Core.Type.Map[mapNum].Tile[x, y].Type != TileType.None & Core.Type.Map[mapNum].Tile[x, y].Type2 != TileType.NPCSpawn & Core.Type.Map[mapNum].Tile[x, y].Type2 != TileType.Item & Core.Type.Map[mapNum].Tile[x, y].Type2 != TileType.None)
            {
                NPCTileIsOpenRet = Conversions.ToBoolean(0);
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
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | MapNPCNum < 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS | Dir < (byte) DirectionType.Up | Dir > (byte) DirectionType.Left)
            {
                return CanNPCMoveRet;
            }

            x = Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X;
            y = Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y;
            CanNPCMoveRet = Conversions.ToBoolean(1);

            switch (Dir)
            {
                case var @case when @case == (byte) DirectionType.Up:
                    {
                        // Check to make sure not outside of boundaries
                        if (y > 0)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x, y - 1].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x, y - 1].Type2;

                            // Check to make sure that the tile is walkable
                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NPCSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NPCSpawn)
                            {
                                CanNPCMoveRet = Conversions.ToBoolean(0);
                                return CanNPCMoveRet;
                            }

                            // Check to make sure that there is not a player in the way
                            var loopTo = NetworkConfig.Socket.HighIndex + 1;
                            for (i = 0; i < loopTo; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X & GetPlayerY(i) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y - 1)
                                    {
                                        CanNPCMoveRet = Conversions.ToBoolean(0);
                                        return CanNPCMoveRet;
                                    }
                                }
                            }

                            // Check to make sure that there is not another npc in the way
                            var loopTo1 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo1; i++)
                            {
                                if (i != MapNPCNum & Core.Type.MapNPC[mapNum].NPC[i].Num > 0 & Core.Type.MapNPC[mapNum].NPC[i].X == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X & Core.Type.MapNPC[mapNum].NPC[i].Y == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y - 1)
                                {
                                    CanNPCMoveRet = Conversions.ToBoolean(0);
                                    return CanNPCMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNPCMoveRet = Conversions.ToBoolean(0);
                        }

                        break;
                    }

                case var case1 when case1 == (byte) DirectionType.Down:
                    {
                        // Check to make sure not outside of boundaries
                        if (y < Core.Type.Map[mapNum].MaxY)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x, y + 1].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x, y + 1].Type2;

                            // Check to make sure that the tile is walkable
                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NPCSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NPCSpawn)
                            {
                                CanNPCMoveRet = Conversions.ToBoolean(0);
                                return CanNPCMoveRet;
                            }

                            // Check to make sure that there is not a player in the way
                            var loopTo2 = NetworkConfig.Socket.HighIndex + 1;
                            for (i = 0; i < loopTo2; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X & GetPlayerY(i) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y + 1)
                                    {
                                        CanNPCMoveRet = Conversions.ToBoolean(0);
                                        return CanNPCMoveRet;
                                    }
                                }
                            }

                            // Check to make sure that there is not another npc in the way
                            var loopTo3 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo3; i++)
                            {
                                if (i != MapNPCNum & Core.Type.MapNPC[mapNum].NPC[i].Num > 0 & Core.Type.MapNPC[mapNum].NPC[i].X == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X & Core.Type.MapNPC[mapNum].NPC[i].Y == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y + 1)
                                {
                                    CanNPCMoveRet = Conversions.ToBoolean(0);
                                    return CanNPCMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNPCMoveRet = Conversions.ToBoolean(0);
                        }

                        break;
                    }

                case var case2 when case2 == (byte) DirectionType.Left:
                    {
                        // Check to make sure not outside of boundaries
                        if (x > 0)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x - 1, y].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x - 1, y].Type2;

                            // Check to make sure that the tile is walkable
                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NPCSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NPCSpawn)
                            {
                                CanNPCMoveRet = Conversions.ToBoolean(0);
                                return CanNPCMoveRet;
                            }

                            // Check to make sure that there is not a player in the way
                            var loopTo4 = NetworkConfig.Socket.HighIndex + 1;
                            for (i = 0; i < loopTo4; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X - 1 & GetPlayerY(i) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y)
                                    {
                                        CanNPCMoveRet = Conversions.ToBoolean(0);
                                        return CanNPCMoveRet;
                                    }
                                }
                            }

                            // Check to make sure that there is not another npc in the way
                            var loopTo5 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo5; i++)
                            {
                                if (i != MapNPCNum & Core.Type.MapNPC[mapNum].NPC[i].Num > 0 & Core.Type.MapNPC[mapNum].NPC[i].X == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X - 1 & Core.Type.MapNPC[mapNum].NPC[i].Y == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y)
                                {
                                    CanNPCMoveRet = Conversions.ToBoolean(0);
                                    return CanNPCMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNPCMoveRet = Conversions.ToBoolean(0);
                        }

                        break;
                    }

                case var case3 when case3 == (byte) DirectionType.Right:
                    {
                        // Check to make sure not outside of boundaries
                        if (x < Core.Type.Map[mapNum].MaxX)
                        {
                            n = (int)Core.Type.Map[mapNum].Tile[x + 1, y].Type;
                            n2 = (int)Core.Type.Map[mapNum].Tile[x + 1, y].Type2;

                            // Check to make sure that the tile is walkable
                            if (n != (byte)TileType.None & n != (byte)TileType.Item & n != (byte)TileType.NPCSpawn & n2 != (byte)TileType.None & n2 != (byte)TileType.Item & n2 != (byte)TileType.NPCSpawn)
                            {
                                CanNPCMoveRet = Conversions.ToBoolean(0);
                                return CanNPCMoveRet;
                            }

                            // Check to make sure that there is not a player in the way
                            var loopTo6 = NetworkConfig.Socket.HighIndex + 1;
                            for (i = 0; i < loopTo6; i++)
                            {
                                if (NetworkConfig.IsPlaying(i))
                                {
                                    if (GetPlayerMap(i) == mapNum & GetPlayerX(i) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X + 1 & GetPlayerY(i) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y)
                                    {
                                        CanNPCMoveRet = Conversions.ToBoolean(0);
                                        return CanNPCMoveRet;
                                    }
                                }
                            }

                            // Check to make sure that there is not another npc in the way
                            var loopTo7 = Core.Constant.MAX_MAP_NPCS;
                            for (i = 0; i < loopTo7; i++)
                            {
                                if (i != MapNPCNum & Core.Type.MapNPC[mapNum].NPC[i].Num > 0 & Core.Type.MapNPC[mapNum].NPC[i].X == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X + 1 & Core.Type.MapNPC[mapNum].NPC[i].Y == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y)
                                {
                                    CanNPCMoveRet = Conversions.ToBoolean(0);
                                    return CanNPCMoveRet;
                                }
                            }
                        }
                        else
                        {
                            CanNPCMoveRet = Conversions.ToBoolean(0);
                        }

                        break;
                    }

            }

            if (Core.Type.MapNPC[mapNum].NPC[MapNPCNum].SkillBuffer > 0)
                CanNPCMoveRet = Conversions.ToBoolean(0);
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

            Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Dir = Dir;

            switch (Dir)
            {
                case var @case when @case == (byte) DirectionType.Up:
                    {
                        Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y = (byte)(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y - 1);

                        buffer.WriteInt32((int) ServerPackets.SNPCMove);
                        buffer.WriteInt32(MapNPCNum);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                        break;
                    }
                case var case1 when case1 == (byte) DirectionType.Down:
                    {
                        Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y = (byte)(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y + 1);

                        buffer.WriteInt32((int) ServerPackets.SNPCMove);
                        buffer.WriteInt32(MapNPCNum);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                        break;
                    }
                case var case2 when case2 == (byte) DirectionType.Left:
                    {
                        Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X = (byte)(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X - 1);

                        buffer.WriteInt32((int) ServerPackets.SNPCMove);
                        buffer.WriteInt32(MapNPCNum);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                        break;
                    }
                case var case3 when case3 == (byte) DirectionType.Right:
                    {
                        Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X = (byte)(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X + 1);

                        buffer.WriteInt32((int) ServerPackets.SNPCMove);
                        buffer.WriteInt32(MapNPCNum);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y);
                        buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Dir);
                        buffer.WriteInt32(Movement);

                        NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
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

            Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Dir = Dir;

            buffer.WriteInt32((int) ServerPackets.SNPCDir);
            buffer.WriteInt32(MapNPCNum);
            buffer.WriteInt32(Dir);

            NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        #endregion

        #region NPCombat

        internal static void TryNPCAttackPlayer(int MapNPCNum, int index)
        {

            int mapNum;
            int NPCNum;
            int Damage;
            int i;
            var armor = default(int);

            // Can the npc attack the player?
            if (CanNPCAttackPlayer(MapNPCNum, index))
            {
                mapNum = GetPlayerMap(index);
                NPCNum = Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num;

                // check if PLAYER can avoid the attack
                if (Player.CanPlayerDodge(index))
                {
                    NetworkSend.SendActionMsg(mapNum, "Dodge!", (int) ColorType.Pink, 1, Core.Type.Player[index].X * 32, Core.Type.Player[index].Y * 32);
                    return;
                }

                if (Player.CanPlayerParry(index))
                {
                    NetworkSend.SendActionMsg(mapNum, "Parry!", (int) ColorType.Pink, 1, Core.Type.Player[index].X * 32, Core.Type.Player[index].Y * 32);
                    return;
                }

                // Get the damage we can do
                Damage = GetNPCDamage(NPCNum);

                if (Player.CanPlayerBlockHit(index))
                {
                    NetworkSend.SendActionMsg(mapNum, "Block!", (int) ColorType.Pink, 1, Core.Type.Player[index].X * 32, Core.Type.Player[index].Y * 32);
                    return;
                }
                else
                {

                    var loopTo = EquipmentType.Count;
                    for (i = 2; i < (int)loopTo; i++) // start at 2, so we skip weapon
                    {
                        if (GetPlayerEquipment(index, (EquipmentType)i) > 0)
                        {
                            armor = armor + Core.Type.Item[GetPlayerEquipment(index, (EquipmentType)i)].Data2;
                        }
                    }
                    // take away armour
                    Damage = Damage - (GetPlayerStat(index, StatType.Spirit) * 2 + GetPlayerLevel(index) * 2 + armor);

                    // * 1.5 if crit hit
                    if (CanNPCrit(NPCNum))
                    {
                        Damage = (int)Math.Round(Damage * 1.5d);
                        NetworkSend.SendActionMsg(mapNum, "Critical!", (int) ColorType.BrightCyan, 1, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X * 32, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y * 32);
                    }

                }

                if (Damage > 0)
                {
                    NPCAttackPlayer(MapNPCNum, index, Damage);
                }

            }

        }

        public static bool CanNPCAttackPlayer(int MapNPCNum, int index)
        {
            bool CanNPCAttackPlayerRet = default;
            int mapNum;
            int NPCNum;

            // Check for subscript out of range
            if (MapNPCNum < 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS | !NetworkConfig.IsPlaying(index))
            {
                return CanNPCAttackPlayerRet;
            }

            // Check for subscript out of range
            if (Core.Type.MapNPC[GetPlayerMap(index)].NPC[MapNPCNum].Num < 0)
            {
                return CanNPCAttackPlayerRet;
            }

            mapNum = GetPlayerMap(index);
            NPCNum = Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num;

            // Make sure the npc isn't already dead
            if (Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Vital[(byte) VitalType.HP] < 0)
            {
                return CanNPCAttackPlayerRet;
            }

            // Make sure npcs dont attack more then once a second
            if (General.GetTimeMs() < Core.Type.MapNPC[mapNum].NPC[MapNPCNum].AttackTimer + 1000)
            {
                return CanNPCAttackPlayerRet;
            }

            // Make sure we dont attack the player if they are switching maps
            if (Core.Type.TempPlayer[index].GettingMap == true)
            {
                return CanNPCAttackPlayerRet;
            }

            Core.Type.MapNPC[mapNum].NPC[MapNPCNum].AttackTimer = General.GetTimeMs();

            // Make sure they are on the same map
            if (NetworkConfig.IsPlaying(index))
            {
                if (NPCNum > 0)
                {

                    // Check if at same coordinates
                    if (GetPlayerY(index) + 1 == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y & GetPlayerX(index) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X)
                    {
                        CanNPCAttackPlayerRet = Conversions.ToBoolean(1);
                    }

                    else if (GetPlayerY(index) - 1 == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y & GetPlayerX(index) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X)
                    {
                        CanNPCAttackPlayerRet = Conversions.ToBoolean(1);
                    }

                    else if (GetPlayerY(index) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y & GetPlayerX(index) + 1 == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X)
                    {
                        CanNPCAttackPlayerRet = Conversions.ToBoolean(1);
                    }

                    else if (GetPlayerY(index) == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y & GetPlayerX(index) - 1 == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X)
                    {
                        CanNPCAttackPlayerRet = Conversions.ToBoolean(1);
                    }
                }
            }

            return CanNPCAttackPlayerRet;

        }

        public static bool CanNPCAttackNPC(int mapNum, int attacker, int victim)
        {
            bool CanNPCAttackNPCRet = default;
            int aNPCNum;
            int vNPCNum;
            int victimX;
            int victimY;
            int attackerX;
            int attackerY;

            CanNPCAttackNPCRet = Conversions.ToBoolean(0);

            // Check for subscript out of range
            if (attacker < 0 | attacker > Core.Constant.MAX_MAP_NPCS)
            {
                return CanNPCAttackNPCRet;
            }

            if (victim < 0 | victim > Core.Constant.MAX_MAP_NPCS)
            {
                return CanNPCAttackNPCRet;
            }

            // Check for subscript out of range
            if (Core.Type.MapNPC[mapNum].NPC[attacker].Num < 0)
            {
                return CanNPCAttackNPCRet;
            }

            // Check for subscript out of range
            if (Core.Type.MapNPC[mapNum].NPC[victim].Num < 0)
            {
                return CanNPCAttackNPCRet;
            }

            aNPCNum = Core.Type.MapNPC[mapNum].NPC[attacker].Num;
            vNPCNum = Core.Type.MapNPC[mapNum].NPC[victim].Num;

            if (aNPCNum < 0)
                return CanNPCAttackNPCRet;
            if (vNPCNum < 0)
                return CanNPCAttackNPCRet;

            // Make sure the npcs arent already dead
            if (Core.Type.MapNPC[mapNum].NPC[attacker].Vital[(byte) VitalType.HP] < 0)
            {
                return CanNPCAttackNPCRet;
            }

            // Make sure the npc isn't already dead
            if (Core.Type.MapNPC[mapNum].NPC[victim].Vital[(byte) VitalType.HP] < 0)
            {
                return CanNPCAttackNPCRet;
            }

            // Make sure npcs dont attack more then once a second
            if (General.GetTimeMs() < Core.Type.MapNPC[mapNum].NPC[attacker].AttackTimer + 1000)
            {
                return CanNPCAttackNPCRet;
            }

            Core.Type.MapNPC[mapNum].NPC[attacker].AttackTimer = General.GetTimeMs();

            attackerX = Core.Type.MapNPC[mapNum].NPC[attacker].X;
            attackerY = Core.Type.MapNPC[mapNum].NPC[attacker].Y;
            victimX = Core.Type.MapNPC[mapNum].NPC[victim].X;
            victimY = Core.Type.MapNPC[mapNum].NPC[victim].Y;

            // Check if at same coordinates
            if (victimY + 1 == attackerY & victimX == attackerX)
            {
                CanNPCAttackNPCRet = Conversions.ToBoolean(1);
            }

            else if (victimY - 1 == attackerY & victimX == attackerX)
            {
                CanNPCAttackNPCRet = Conversions.ToBoolean(1);
            }

            else if (victimY == attackerY & victimX + 1 == attackerX)
            {
                CanNPCAttackNPCRet = Conversions.ToBoolean(1);
            }

            else if (victimY == attackerY & victimX - 1 == attackerX)
            {
                CanNPCAttackNPCRet = Conversions.ToBoolean(1);
            }

            return CanNPCAttackNPCRet;

        }

        internal static void NPCAttackPlayer(int MapNPCNum, int victim, int Damage)
        {
            string Name;
            int mapNum;
            int z;
            int InvCount;
            int EqCount;
            int j;
            int x;
            var buffer = new ByteStream(4);

            // Check for subscript out of range
            if (MapNPCNum < 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS | Conversions.ToInteger(NetworkConfig.IsPlaying(victim)) == 0)
                return;

            // Check for subscript out of range
            if (Core.Type.MapNPC[GetPlayerMap(victim)].NPC[MapNPCNum].Num < 0)
                return;

            mapNum = GetPlayerMap(victim);
            Name = Core.Type.NPC[Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num].Name;

            // Send this packet so they can see the npc attacking
            buffer.WriteInt32((int) ServerPackets.SNPCAttack);
            buffer.WriteInt32(MapNPCNum);
            NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
            buffer.Dispose();

            if (Damage < 0)
                return;

            // set the regen timer
            Core.Type.MapNPC[mapNum].NPC[MapNPCNum].StopRegen = 0;
            Core.Type.MapNPC[mapNum].NPC[MapNPCNum].StopRegenTimer = General.GetTimeMs();

            if (Damage >= GetPlayerVital(victim, VitalType.HP))
            {
                // Say damage
                NetworkSend.SendActionMsg(GetPlayerMap(victim), "-" + GetPlayerVital(victim, VitalType.HP), (int) ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);

                // Set NPC target to 0
                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Target = 0;
                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].TargetType = 0;

                // kill player
                Player.KillPlayer(victim);

                // Player is dead
                NetworkSend.GlobalMsg(GetPlayerName(victim) + " has been killed by " + Name);
            }
            else
            {
                // Player not dead, just do the damage
                SetPlayerVital(victim, VitalType.HP, GetPlayerVital(victim, VitalType.HP) - Damage);
                NetworkSend.SendVital(victim, VitalType.HP);
                Animation.SendAnimation(mapNum, Core.Type.NPC[Core.Type.MapNPC[GetPlayerMap(victim)].NPC[MapNPCNum].Num].Animation, 0, 0, (byte)TargetType.Player, victim);

                // send the sound
                // SendMapSound victim, GetPlayerX(victim), GetPlayerY(victim), SoundEntity.seNPC, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num

                // Say damage
                NetworkSend.SendActionMsg(GetPlayerMap(victim), "-" + Damage, (int) ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);
                NetworkSend.SendBlood(GetPlayerMap(victim), GetPlayerX(victim), GetPlayerY(victim));

                // set the regen timer
                Core.Type.TempPlayer[victim].StopRegen = 0;
                Core.Type.TempPlayer[victim].StopRegenTimer = General.GetTimeMs();

            }

        }

        public static void NPCAttackNPC(int mapNum, int attacker, int victim, int Damage)
        {
            var buffer = new ByteStream(4);
            int aNPCNum;
            int vNPCNum;
            int n;

            if (attacker < 0 | attacker > Core.Constant.MAX_MAP_NPCS)
                return;
            if (victim < 0 | victim > Core.Constant.MAX_MAP_NPCS)
                return;

            if (Damage < 0)
                return;

            aNPCNum = Core.Type.MapNPC[mapNum].NPC[attacker].Num;
            vNPCNum = Core.Type.MapNPC[mapNum].NPC[victim].Num;

            if (aNPCNum < 0)
                return;
            if (vNPCNum < 0)
                return;

            // Send this packet so they can see the person attacking
            buffer.WriteInt32((int) ServerPackets.SNPCAttack);
            buffer.WriteInt32(attacker);
            NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
            buffer.Dispose();

            if (Damage >= Core.Type.MapNPC[mapNum].NPC[victim].Vital[(byte) VitalType.HP])
            {
                NetworkSend.SendActionMsg(mapNum, "-" + Damage, (int) ColorType.BrightRed, 1, Core.Type.MapNPC[mapNum].NPC[victim].X * 32, Core.Type.MapNPC[mapNum].NPC[victim].Y * 32);
                NetworkSend.SendBlood(mapNum, Core.Type.MapNPC[mapNum].NPC[victim].X, Core.Type.MapNPC[mapNum].NPC[victim].Y);

                // npc is dead.

                // Set NPC target to 0
                Core.Type.MapNPC[mapNum].NPC[attacker].Target = 0;
                Core.Type.MapNPC[mapNum].NPC[attacker].TargetType = 0;

                // Drop the goods if they get it
                double tmpitem = General.Random.NextDouble(1d, 5d);
                n = (int)Math.Round(Conversion.Int(VBMath.Rnd() * Core.Type.NPC[vNPCNum].DropChance[(int)Math.Round(tmpitem)]) + 1f);
                if (n == 1)
                {
                    Item.SpawnItem(Core.Type.NPC[vNPCNum].DropItem[(int)Math.Round(tmpitem)], Core.Type.NPC[vNPCNum].DropItemValue[(int)Math.Round(tmpitem)], mapNum, Core.Type.MapNPC[mapNum].NPC[victim].X, Core.Type.MapNPC[mapNum].NPC[victim].Y);
                }

                // Reset victim's stuff so it dies in loop
                Core.Type.MapNPC[mapNum].NPC[victim].Num = 0;
                Core.Type.MapNPC[mapNum].NPC[victim].SpawnWait = General.GetTimeMs();
                Core.Type.MapNPC[mapNum].NPC[victim].Vital[(byte) VitalType.HP] = 0;

                // send npc death packet to map
                buffer = new ByteStream(4);
                buffer.WriteInt32((int) ServerPackets.SNPCDead);
                buffer.WriteInt32(victim);
                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                buffer.Dispose();
            }
            else
            {
                // npc not dead, just do the damage
                Core.Type.MapNPC[mapNum].NPC[victim].Vital[(byte) VitalType.HP] = Core.Type.MapNPC[mapNum].NPC[victim].Vital[(byte) VitalType.HP] - Damage;
                // Say damage
                NetworkSend.SendActionMsg(mapNum, "-" + Damage, (int) ColorType.BrightRed, 1, Core.Type.MapNPC[mapNum].NPC[victim].X * 32, Core.Type.MapNPC[mapNum].NPC[victim].Y * 32);
                NetworkSend.SendBlood(mapNum, Core.Type.MapNPC[mapNum].NPC[victim].X, Core.Type.MapNPC[mapNum].NPC[victim].Y);
            }

        }

        internal static void KnockBackNPC(int index, int NPCNum, int IsSkill = 0)
        {
            if (IsSkill > 0)
            {
                for (int i = 0, loopTo = Core.Type.Skill[IsSkill].KnockBackTiles; i < loopTo; i++)
                {
                    if (CanNPCMove(GetPlayerMap(index), NPCNum, (byte)GetPlayerDir(index)))
                    {
                        NPCMove(GetPlayerMap(index), NPCNum, GetPlayerDir(index), (byte) MovementType.Walking);
                    }
                }
                Core.Type.MapNPC[GetPlayerMap(index)].NPC[NPCNum].StunDuration = 0;
                Core.Type.MapNPC[GetPlayerMap(index)].NPC[NPCNum].StunTimer = General.GetTimeMs();
            }
            else if (Core.Type.Item[GetPlayerEquipment(index, Core.Enum.EquipmentType.Weapon)].KnockBack == 1)
            {
                for (int i = 0, loopTo1 = Core.Type.Item[GetPlayerEquipment(index, EquipmentType.Weapon)].KnockBackTiles; i < loopTo1; i++)
                {
                    if (CanNPCMove(GetPlayerMap(index), NPCNum, (byte)GetPlayerDir(index)))
                    {
                        NPCMove(GetPlayerMap(index), NPCNum, GetPlayerDir(index), (byte) MovementType.Walking);
                    }
                }
                Core.Type.MapNPC[GetPlayerMap(index)].NPC[NPCNum].StunDuration = 0;
                Core.Type.MapNPC[GetPlayerMap(index)].NPC[NPCNum].StunTimer = General.GetTimeMs();
            }
        }

        internal static int RandomNPCAttack(int mapNum, int MapNPCNum)
        {
            int RandomNPCAttackRet = default;
            int i;
            var SkillList = new List<byte>();

            RandomNPCAttackRet = 0;

            if (Core.Type.MapNPC[mapNum].NPC[MapNPCNum].SkillBuffer > 0)
                return RandomNPCAttackRet;

            var loopTo = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo; i++)
            {
                if ((int)Core.Type.NPC[Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num].Skill[i] > 0)
                {
                    SkillList.Add(Core.Type.NPC[Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num].Skill[i]);
                }
            }

            if (SkillList.Count > 1)
            {
                RandomNPCAttackRet = SkillList[(int)Math.Round(General.Random.NextDouble(0d, SkillList.Count - 1))];
            }
            else
            {
                RandomNPCAttackRet = 0;
            }

            return RandomNPCAttackRet;

        }

        internal static int GetNPCSkill(int NPCNum, int SkillSlot)
        {
            int GetNPCSkillRet = default;
            GetNPCSkillRet = Core.Type.NPC[NPCNum].Skill[SkillSlot];
            return GetNPCSkillRet;
        }

        internal static void BufferNPCSkill(int mapNum, int MapNPCNum, int SkillSlot)
        {
            int skillnum;
            int MPCost;
            int SkillCastType;
            int range;
            bool HasBuffered;

            byte TargetType;
            int Target;

            // Prevent subscript out of range
            if (SkillSlot < 0 | SkillSlot > Core.Constant.MAX_NPC_SKILLS)
                return;

            skillnum = GetNPCSkill(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num, SkillSlot);

            if (skillnum < 0 | skillnum > Core.Constant.MAX_SKILLS)
                return;

            // see if cooldown has finished
            if (Core.Type.MapNPC[mapNum].NPC[MapNPCNum].SkillCD[SkillSlot] > General.GetTimeMs())
            {
                TryNPCAttackPlayer(MapNPCNum, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Target);
                return;
            }

            MPCost = Core.Type.Skill[skillnum].MpCost;

            // Check if they have enough MP
            if (Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Vital[(byte) VitalType.SP] < MPCost)
                return;

            // find out what kind of skill it is! self cast, target or AOE
            if (Core.Type.Skill[skillnum].Range > 0)
            {
                // ranged attack, single target or aoe?
                if (!Core.Type.Skill[skillnum].IsAoE)
                {
                    SkillCastType = 2; // targetted
                }
                else
                {
                    SkillCastType = 3;
                } // targetted aoe
            }
            else if (!Core.Type.Skill[skillnum].IsAoE)
            {
                SkillCastType = 0; // self-cast
            }
            else
            {
                SkillCastType = 0;
            } // self-cast AoE

            TargetType = Core.Type.MapNPC[mapNum].NPC[MapNPCNum].TargetType;
            Target = Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Target;
            range = Core.Type.Skill[skillnum].Range;
            HasBuffered = Conversions.ToBoolean(0);

            switch (SkillCastType)
            {
                case 0:
                case 1: // self-cast & self-cast AOE
                    {
                        HasBuffered = Conversions.ToBoolean(1);
                        break;
                    }
                case 2:
                case 3: // targeted & targeted AOE
                    {
                        // check if have target
                        if (!(Target > 0))
                        {
                            return;
                        }
                        if (TargetType == (byte)Core.Enum.TargetType.Player)
                        {
                            // if have target, check in range
                            if (!Player.IsInRange(range, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y, GetPlayerX(Target), GetPlayerY(Target)))
                            {
                                return;
                            }
                            else
                            {
                                HasBuffered = Conversions.ToBoolean(1);
                            }
                        }
                        else if (TargetType == (byte) Core.Enum.TargetType.NPC)
                        {
                            // ' if have target, check in range
                            // If Not isInRange(range, GetPlayerX(index), GetPlayerY(index), Core.Type.MapNPC[mapNum].NPC[target].x, Core.Type.MapNPC[mapNum].NPC[target].y) Then
                            // PlayerMsg(index, "Target not in range.")
                            // HasBuffered = 0
                            // Else
                            // ' go through skill Type
                            // If Type.Skill(skillNum).Type <> (byte)SkillType.DAMAGEHP And Type.Skill(skillNum).Type <> (byte)SkillType.DAMAGEMP Then
                            // HasBuffered = 1
                            // Else
                            // If CanAttackNPC(index, Target, True) Then
                            // HasBuffered = 1
                            // End If
                            // End If
                            // End If
                        }

                        break;
                    }
            }

            if (HasBuffered)
            {
                Animation.SendAnimation(mapNum, Core.Type.Skill[skillnum].CastAnim, 0, 0, (byte)Core.Enum.TargetType.Player, Target);
                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].SkillBuffer = SkillSlot;
                Core.Type.MapNPC[mapNum].NPC[MapNPCNum].SkillBufferTimer = General.GetTimeMs();
                return;
            }
        }

        internal static bool CanNPCBlock(int NPCNum)
        {
            bool CanNPCBlockRet = default;
            int rate;
            int stat;
            int rndNum;

            CanNPCBlockRet = Conversions.ToBoolean(0);

            stat = (int)Math.Round((double)Core.Type.NPC[NPCNum].Stat[(byte)StatType.Luck] / 5d);  // guessed shield agility
            rate = (int)Math.Round(stat / 12.08d);
            rndNum = (int)Math.Round(General.Random.NextDouble(1d, 100d));

            if (rndNum <= rate)
                CanNPCBlockRet = Conversions.ToBoolean(1);
            return CanNPCBlockRet;

        }

        internal static bool CanNPCrit(int NPCNum)
        {
            bool CanNPCritRet = default;
            int rate;
            int rndNum;

            CanNPCritRet = Conversions.ToBoolean(0);

            rate = (int)Math.Round((double)Core.Type.NPC[NPCNum].Stat[(byte)StatType.Luck] / 3d);
            rndNum = (int)Math.Round(General.Random.NextDouble(1d, 100d));

            if (rndNum <= rate)
                CanNPCritRet = Conversions.ToBoolean(1);
            return CanNPCritRet;

        }

        internal static bool CanNPCDodge(int NPCNum)
        {
            bool CanNPCDodgeRet = default;
            int rate;
            int rndNum;

            CanNPCDodgeRet = Conversions.ToBoolean(0);

            rate = (int)Math.Round((double)Core.Type.NPC[NPCNum].Stat[(int)StatType.Luck] / 4d);
            rndNum = (int)Math.Round(General.Random.NextDouble(1d, 100d));

            if (rndNum <= rate)
                CanNPCDodgeRet = Conversions.ToBoolean(1);
            return CanNPCDodgeRet;

        }

        internal static bool CanNPCParry(int NPCNum)
        {
            bool CanNPCParryRet = default;
            int rate;
            int rndNum;

            CanNPCParryRet = Conversions.ToBoolean(0);

            rate = (int)Math.Round((double)Core.Type.NPC[NPCNum].Stat[(int)StatType.Luck] / 6d);
            rndNum = (int)Math.Round(General.Random.NextDouble(1d, 100d));

            if (rndNum <= rate)
                CanNPCParryRet = Conversions.ToBoolean(1);
            return CanNPCParryRet;

        }

        public static int GetNPCDamage(int NPCNum)
        {
            int GetNPCDamageRet = default;

            GetNPCDamageRet = (int)Math.Round((double)((int)Core.Type.NPC[NPCNum].Stat[(int)StatType.Strength] * 2 + Core.Type.NPC[NPCNum].Damage * 2 + Core.Type.NPC[NPCNum].Level * 3) + General.Random.NextDouble(1d, 20d));
            return GetNPCDamageRet;

        }

        internal static bool IsNPCDead(int mapNum, int MapNPCNum)
        {
            bool IsNPCDeadRet = false;
            IsNPCDeadRet = false;
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS | MapNPCNum < 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS)
                return IsNPCDeadRet;
            if (Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Vital[(byte) VitalType.HP] < 0)
                IsNPCDeadRet = true;
            return IsNPCDeadRet;
        }

        internal static void DropNPCItems(int mapNum, int MapNPCNum)
        {
            var NPCNum = Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num;
            double tmpitem = General.Random.NextDouble(1d, 5d);
            var n = VBMath.Rnd() * (float)Core.Type.NPC[NPCNum].DropChance[(int)Math.Round(tmpitem)] + 1;

            if (n == 1)
            {
                Item.SpawnItem(Core.Type.NPC[NPCNum].DropItem[(int)Math.Round(tmpitem)], Core.Type.NPC[NPCNum].DropItemValue[(int)Math.Round(tmpitem)], mapNum, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y);
            }
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
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Num);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].X);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Y);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Dir);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(byte) VitalType.HP]);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(byte) VitalType.SP]);
            }

            NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        #endregion

        #region Incoming Packets

        public static void Packet_EditNPC(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;
            if (Core.Type.TempPlayer[index].Editor > 0)
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
            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);

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
            Core.Type.NPC[NPCNum].Animation = buffer.ReadInt32();
            Core.Type.NPC[NPCNum].AttackSay = buffer.ReadString();
            Core.Type.NPC[NPCNum].Behaviour = buffer.ReadByte();

            var loopTo = Core.Constant.MAX_DROP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                Core.Type.NPC[NPCNum].DropChance[i] = buffer.ReadInt32();
                Core.Type.NPC[NPCNum].DropItem[i] = buffer.ReadInt32();
                Core.Type.NPC[NPCNum].DropItemValue[i] = buffer.ReadInt32();
            }

            Core.Type.NPC[NPCNum].Exp = buffer.ReadInt32();
            Core.Type.NPC[NPCNum].Faction = buffer.ReadByte();
            Core.Type.NPC[NPCNum].HP = buffer.ReadInt32();
            Core.Type.NPC[NPCNum].Name = buffer.ReadString();
            Core.Type.NPC[NPCNum].Range = buffer.ReadByte();
            Core.Type.NPC[NPCNum].SpawnTime = buffer.ReadByte();
            Core.Type.NPC[NPCNum].SpawnSecs = buffer.ReadInt32();
            Core.Type.NPC[NPCNum].Sprite = buffer.ReadInt32();

            var loopTo1 = (byte)StatType.Count;
            for (i = 0; i < loopTo1; i++)
                Core.Type.NPC[NPCNum].Stat[i] = buffer.ReadByte();

            var loopTo2 = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo2; i++)
                Core.Type.NPC[NPCNum].Skill[i] = buffer.ReadByte();

            Core.Type.NPC[NPCNum].Level = buffer.ReadInt32();
            Core.Type.NPC[NPCNum].Damage = buffer.ReadInt32();

            // Save it
            SendUpdateNPCToAll(NPCNum);
            Database.SaveNPC(NPCNum);
            Core.Log.Add(GetPlayerLogin(index) + " saved NPC #" + NPCNum + ".", Constant.ADMIN_LOG);

            buffer.Dispose();
        }

        public static void SendNPCs(int index)
        {
            int i;

            var loopTo = Core.Constant.MAX_NPCS - 1;
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
            buffer.WriteInt32(Core.Type.NPC[NPCNum].Animation);
            buffer.WriteString(Core.Type.NPC[NPCNum].AttackSay);
            buffer.WriteByte(Core.Type.NPC[NPCNum].Behaviour);

            var loopTo = Core.Constant.MAX_DROP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32(Core.Type.NPC[NPCNum].DropChance[i]);
                buffer.WriteInt32(Core.Type.NPC[NPCNum].DropItem[i]);
                buffer.WriteInt32(Core.Type.NPC[NPCNum].DropItemValue[i]);
            }

            buffer.WriteInt32(Core.Type.NPC[NPCNum].Exp);
            buffer.WriteByte(Core.Type.NPC[NPCNum].Faction);
            buffer.WriteInt32(Core.Type.NPC[NPCNum].HP);
            buffer.WriteString(Core.Type.NPC[NPCNum].Name);
            buffer.WriteByte(Core.Type.NPC[NPCNum].Range);
            buffer.WriteByte(Core.Type.NPC[NPCNum].SpawnTime);
            buffer.WriteInt32(Core.Type.NPC[NPCNum].SpawnSecs);
            buffer.WriteInt32(Core.Type.NPC[NPCNum].Sprite);

            var loopTo1 = (byte)StatType.Count;
            for (i = 0; i < loopTo1; i++)
                buffer.WriteByte(Core.Type.NPC[NPCNum].Stat[i]);

            var loopTo2 = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo2; i++)
                buffer.WriteByte(Core.Type.NPC[NPCNum].Skill[i]);

            buffer.WriteInt32(Core.Type.NPC[NPCNum].Level);
            buffer.WriteInt32(Core.Type.NPC[NPCNum].Damage);

            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
            buffer.Dispose();
        }

        public static void SendUpdateNPCToAll(int NPCNum)
        {
            ByteStream buffer;
            int i;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SUpdateNPC);

            buffer.WriteInt32(NPCNum);
            buffer.WriteInt32(Core.Type.NPC[NPCNum].Animation);
            buffer.WriteString(Core.Type.NPC[NPCNum].AttackSay);
            buffer.WriteByte(Core.Type.NPC[NPCNum].Behaviour);

            var loopTo = Core.Constant.MAX_DROP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32(Core.Type.NPC[NPCNum].DropChance[i]);
                buffer.WriteInt32(Core.Type.NPC[NPCNum].DropItem[i]);
                buffer.WriteInt32(Core.Type.NPC[NPCNum].DropItemValue[i]);
            }

            buffer.WriteInt32(Core.Type.NPC[NPCNum].Exp);
            buffer.WriteByte(Core.Type.NPC[NPCNum].Faction);
            buffer.WriteInt32(Core.Type.NPC[NPCNum].HP);
            buffer.WriteString(Core.Type.NPC[NPCNum].Name);
            buffer.WriteByte(Core.Type.NPC[NPCNum].Range);
            buffer.WriteByte(Core.Type.NPC[NPCNum].SpawnTime);
            buffer.WriteInt32(Core.Type.NPC[NPCNum].SpawnSecs);
            buffer.WriteInt32(Core.Type.NPC[NPCNum].Sprite);

            var loopTo1 = (byte)StatType.Count;
            for (i = 0; i < loopTo1; i++)
                buffer.WriteByte(Core.Type.NPC[NPCNum].Stat[i]);

            var loopTo2 = Core.Constant.MAX_NPC_SKILLS;
            for (i = 0; i < loopTo2; i++)
                buffer.WriteByte(Core.Type.NPC[NPCNum].Skill[i]);

            buffer.WriteInt32(Core.Type.NPC[NPCNum].Level);
            buffer.WriteInt32(Core.Type.NPC[NPCNum].Damage);

            NetworkConfig.SendDataToAll(ref buffer.Data, buffer.Head);
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
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Num);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].X);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Y);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Dir);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(byte) VitalType.HP]);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(byte) VitalType.SP]);
            }

            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);

            buffer.Dispose();
        }

        public static void SendMapNPCTo(int mapNum, int MapNPCNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapNPCUpdate);

            buffer.WriteInt32(MapNPCNum);

            {
                var withBlock = Core.Type.MapNPC[mapNum].NPC[MapNPCNum];
                buffer.WriteInt32(withBlock.Num);
                buffer.WriteInt32(withBlock.X);
                buffer.WriteInt32(withBlock.Y);
                buffer.WriteInt32(withBlock.Dir);
                buffer.WriteInt32(withBlock.Vital[(byte) VitalType.HP]);
                buffer.WriteInt32(withBlock.Vital[(byte) VitalType.SP]);
            }

            NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);

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
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Vital[i]);

            NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendNPCAttack(int index, int NPCNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SAttack);

            buffer.WriteInt32(NPCNum);
            NetworkConfig.SendDataToMap(GetPlayerMap(index), ref buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendNPCDead(int mapNum, int index)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SNPCDead);

            buffer.WriteInt32(index);
            NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

    }
}