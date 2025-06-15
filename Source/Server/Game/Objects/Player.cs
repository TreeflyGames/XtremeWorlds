using Core;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using System;
using System.Reflection;
using System.Text;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Packets;
using static Core.Type;

namespace Server
{

    public class Player
    {
        #region Data

        public static void CheckPlayerLevelUp(int index)
        {
            try
            {
                Script.Instance?.CheckPlayerLevelUp(index);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Incoming Packets

        public static void HandleUseChar(int index)
        {
            // Set the flag so we know the person is in the game
            Core.Type.TempPlayer[index].InGame = true;

            // Send an ok to client to start receiving in game data
            NetworkSend.SendLoginOK(index);
            JoinGame(index);
            string text = string.Format("{0} | {1} has began playing {2}.", GetAccountLogin(index), GetPlayerName(index), SettingsManager.Instance.GameName);
            Core.Log.Add(text, Constant.PLAYER_LOG);
            Console.WriteLine(text);
            
        }

        #endregion

        #region Outgoing Packets

        public static void SendLeaveMap(int index, int mapNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SLeftMap);
            buffer.WriteInt32(index);
            NetworkConfig.SendDataToMapBut(index, mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        #endregion

        #region Movement
        public static void PlayerWarp(int index, int mapNum, int x, int y, int dir)
        {
            int OldMap;
            int i;
            ByteStream buffer;

            // Check for subscript out of range
            if (NetworkConfig.IsPlaying(index) == false | mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return;

            // Check if you are out of bounds
            if (x > Core.Type.Map[mapNum].MaxX)
                x = Core.Type.Map[mapNum].MaxX;

            if (y > Core.Type.Map[mapNum].MaxY)
                y = Core.Type.Map[mapNum].MaxY;

            Core.Type.TempPlayer[index].EventProcessingCount = 0;
            Core.Type.TempPlayer[index].EventMap.CurrentEvents = 0;

            // clear target
            Core.Type.TempPlayer[index].Target = 0;
            Core.Type.TempPlayer[index].TargetType = 0;
            NetworkSend.SendTarget(index, 0, 0);

            // clear events
            Core.Type.TempPlayer[index].EventMap.CurrentEvents = 0;

            // Save old map to send erase player data to
            OldMap = GetPlayerMap(index);

            if (OldMap != mapNum)
            {
                SendLeaveMap(index, OldMap);
            }

            SetPlayerMap(index, mapNum);
            SetPlayerX(index, x);
            SetPlayerY(index, y);
            SetPlayerDir(index, dir);

            NetworkSend.SendPlayerXY(index);

            // send equipment of all people on new map
            if (GameLogic.GetTotalMapPlayers(mapNum) > 0)
            {
                var loopTo = NetworkConfig.Socket.HighIndex;
                for (i = 0; i < loopTo; i++)
                {
                    if (NetworkConfig.IsPlaying(i))
                    {
                        if (GetPlayerMap(i) == mapNum)
                        {
                            NetworkSend.SendMapEquipmentTo(i, index);
                        }
                    }
                }
            }

            // Now we check if there were any players left on the map the player just left, and if not stop processing npcs
            if (GameLogic.GetTotalMapPlayers(OldMap) == 0)
            {
                // Regenerate all NPCs' health
                var loopTo1 = Core.Constant.MAX_MAP_NPCS;
                for (i = 0; i < loopTo1; i++)
                {
                    if (Core.Type.MapNPC[OldMap].NPC[i].Num >= 0)
                    {
                        Core.Type.MapNPC[OldMap].NPC[i].Vital[(byte) VitalType.HP] = GameLogic.GetNPCMaxVital((int)Core.Type.MapNPC[OldMap].NPC[i].Num, VitalType.HP);
                    }

                }
            }

            // Sets it so we know to process npcs on the map
            Core.Type.TempPlayer[index].GettingMap = true;

            Moral.SendUpdateMoralTo(index, Core.Type.Map[mapNum].Moral);

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SCheckForMap);
            buffer.WriteInt32(mapNum);
            buffer.WriteInt32(Core.Type.Map[mapNum].Revision);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();

        }

        public static void PlayerMove(int index, int Dir, int Movement, bool ExpectingWarp)
        {
            int mapNum;
            int x;
            int y;
            bool begineventprocessing;
            bool Moved;
            var DidWarp = default(bool);
            byte NewMapX;
            byte NewMapY;
            var vital = default(int);
            int Color;
            var amount = default(int);

            // Check for subscript out of range
           if (Dir < (int)DirectionType.Up || Dir > (int)DirectionType.DownRight || Movement < (int)MovementType.Standing || Movement > (int)MovementType.Running)
            {
                return;
            }

            // Prevent player from moving if they have casted a skill
            if (Core.Type.TempPlayer[index].SkillBuffer >= 0)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            // Cant move if in the bank
            if (Core.Type.TempPlayer[index].InBank)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            // if stunned, stop them moving
            if (Core.Type.TempPlayer[index].StunDuration > 0)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            if (Core.Type.TempPlayer[index].InShop >= 0 || Core.Type.TempPlayer[index].InBank)
            {
                return;
            }

            try
            {
                Script.Instance?.PlayerMove(index);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            SetPlayerDir(index, Dir);
            Moved = false;
            mapNum = GetPlayerMap(index);

            switch ((DirectionType)Dir)
            {
                case DirectionType.Up:
                    if (GetPlayerY(index) > 0)
                    {
                        x = GetPlayerX(index);
                        y = GetPlayerY(index) - 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.Up))
                        {
                            break;
                        }

                        SetPlayerY(index, GetPlayerY(index) - 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo2 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo2; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    else if (Core.Type.Map[mapNum].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing && Core.Type.Map[mapNum].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                    {
                        if (Core.Type.Map[GetPlayerMap(index)].Up > 0)
                        {
                            NewMapY = Core.Type.Map[Core.Type.Map[GetPlayerMap(index)].Up].MaxY;
                            PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Up, GetPlayerX(index), NewMapY, (int)DirectionType.Up);
                            DidWarp = true;
                            Moved = true;
                        }
                    }
                    break;

                case DirectionType.Down:
                    if (GetPlayerY(index) < Core.Type.Map[mapNum].MaxY - 1)
                    {
                        x = GetPlayerX(index);
                        y = GetPlayerY(index) + 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.Down))
                        {
                            break;
                        }

                        SetPlayerY(index, GetPlayerY(index) + 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo1 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo1; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing && Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                    {
                        if (Core.Type.Map[GetPlayerMap(index)].Down > 0)
                        {
                            PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Down, GetPlayerX(index), 0, (int)DirectionType.Down);
                            DidWarp = true;
                            Moved = true;
                        }
                    }
                    break;

                case DirectionType.Left:
                    if (GetPlayerX(index) > 0)
                    {
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index);

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.Left))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) - 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo2 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo2; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing && Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                    {
                        if (Core.Type.Map[GetPlayerMap(index)].Left > 0)
                        {
                            NewMapX = Core.Type.Map[Core.Type.Map[GetPlayerMap(index)].Left].MaxX;
                            PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Left, NewMapX, GetPlayerY(index), (int)DirectionType.Left);
                            DidWarp = true;
                            Moved = true;
                        }
                    }
                    break;

                case DirectionType.Right:
                    if (GetPlayerX(index) < Core.Type.Map[mapNum].MaxX - 1)
                    {
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index);

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.Right))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) + 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo3 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo3; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing && Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                    {
                        if (Core.Type.Map[GetPlayerMap(index)].Right > 0)
                        {
                            PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Right, 0, GetPlayerY(index), (int)DirectionType.Right);
                            DidWarp = true;
                            Moved = true;
                        }
                    }
                    break;

                case DirectionType.UpRight:
                    if (GetPlayerY(index) > 0 && GetPlayerX(index) < Core.Type.Map[mapNum].MaxX - 1)
                    {
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index) - 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.UpRight))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) + 1);
                        SetPlayerY(index, GetPlayerY(index) - 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo4 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo4; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    break;

                case DirectionType.UpLeft:
                    if (GetPlayerY(index) > 0 && GetPlayerX(index) > 0)
                    {
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index) - 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.UpLeft))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) - 1);
                        SetPlayerY(index, GetPlayerY(index) - 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo5 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo5; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    break;

                case DirectionType.DownRight:
                    if (GetPlayerY(index) < Core.Type.Map[mapNum].MaxY - 1 && GetPlayerX(index) < Core.Type.Map[mapNum].MaxX - 1)
                    {
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index) + 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.DownRight))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) + 1);
                        SetPlayerY(index, GetPlayerY(index) + 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo6 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo6; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    break;

                case DirectionType.DownLeft:
                    if (GetPlayerY(index) < Core.Type.Map[mapNum].MaxY - 1 && GetPlayerX(index) > 0)
                    {
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index) + 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.DownLeft))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) - 1);
                        SetPlayerY(index, GetPlayerY(index) + 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo7 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo7; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    break;
            }

            if (GetPlayerX(index) >= 0 && GetPlayerY(index) >= 0 && GetPlayerX(index) < Map[GetPlayerMap(index)].MaxX && GetPlayerY(index) < Map[GetPlayerMap(index)].MaxY)
            {
                ref var withBlock = ref Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)];
                mapNum = -1;
                x = 0;
                y = 0;

                // Check to see if the tile is a warp tile, and if so warp them
                if (withBlock.Type == TileType.Warp)
                {
                    mapNum = withBlock.Data1;
                    x = withBlock.Data2;
                    y = withBlock.Data3;
                }

                if (withBlock.Type2 == TileType.Warp)
                {
                    mapNum = withBlock.Data1_2;
                    x = withBlock.Data2_2;
                    y = withBlock.Data3_2;
                }

                if (mapNum >= 0)
                {
                    PlayerWarp(index, (int)mapNum, x, y, (int)DirectionType.Down);

                    DidWarp = true;
                    Moved = true;
                }

                x = -1;
                y = 0;

                // Check for a shop, and if so open it
                if (withBlock.Type == TileType.Shop)
                {
                    x = withBlock.Data1;
                }

                if (withBlock.Type2 == TileType.Shop)
                {
                    x = withBlock.Data1_2;
                }

                if (x >= 0) // shop exists?
                {
                    if (Strings.Len(Core.Type.Shop[x].Name) > 0) // name exists?
                    {
                        NetworkSend.SendOpenShop(index, x);
                        Core.Type.TempPlayer[index].InShop = x; // stops movement and the like
                    }
                }

                // Check to see if the tile is a bank, and if so send bank
                if (withBlock.Type == TileType.Bank | withBlock.Type2 == TileType.Bank)
                {
                    NetworkSend.SendBank(index);
                    Core.Type.TempPlayer[index].InBank = true;
                    Moved = true;
                }

                // Check if it's a heal tile
                if (withBlock.Type == TileType.Heal)
                {
                    vital = withBlock.Data1;
                    amount = withBlock.Data2;
                }

                if (withBlock.Type2 == TileType.Heal)
                {
                    vital = withBlock.Data1_2;
                    amount += withBlock.Data2_2;
                }

                if (vital > 0)
                {
                    if (!(GetPlayerVital(index, (VitalType)vital) == GetPlayerMaxVital(index, (VitalType)vital)))
                    {
                        if (vital == (byte)VitalType.HP)
                        {
                            Color = (int) ColorType.BrightGreen;
                        }
                        else
                        {
                            Color = (int) ColorType.BrightBlue;
                        }

                        NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + amount, Color, (byte)ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32, 1);
                        SetPlayerVital(index, (VitalType)vital, GetPlayerVital(index, (VitalType)vital) + amount);
                        NetworkSend.PlayerMsg(index, "You feel rejuvenating forces coursing through your body.", (int) ColorType.BrightGreen);
                        NetworkSend.SendVital(index, (VitalType)vital);
                    }
                    Moved = true;
                }

                // Check if it's a trap tile
                if (withBlock.Type == TileType.Trap)
                {
                    amount = withBlock.Data1;
                }

                if (withBlock.Type2 == TileType.Trap)
                {
                    amount += withBlock.Data1_2;
                }

                if (amount > 0)
                {
                    NetworkSend.SendActionMsg(GetPlayerMap(index), "-" + amount, (int) ColorType.BrightRed, (byte)ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32, 1);
                    if (GetPlayerVital(index, (VitalType)VitalType.HP) - amount < 0)
                    {
                        KillPlayer(index);
                        NetworkSend.PlayerMsg(index, "You've been killed by a trap.", (int) ColorType.BrightRed);
                    }
                    else
                    {
                        SetPlayerVital(index, (VitalType)VitalType.HP, GetPlayerVital(index, (VitalType)VitalType.HP) - amount);
                        NetworkSend.PlayerMsg(index, "You've been injured by a trap.", (int) ColorType.BrightRed);
                        NetworkSend.SendVital(index, (VitalType)VitalType.HP);
                    }
                    Moved = true;
                }

            }

            // They tried to hack
            if (Moved == false | ExpectingWarp & !DidWarp)
            {
                PlayerWarp(index, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index), (byte)Core.Enum.DirectionType.Down);
            }

            x = GetPlayerX(index);
            y = GetPlayerY(index);

            if (Moved)
            {
                if (Core.Type.TempPlayer[index].EventMap.CurrentEvents > 0)
                {
                    for (int i = 0, loopTo8 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo8; i++)
                    {
                        begineventprocessing = false;

                        if (Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId >= 0)
                        {
                            if ((int)Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Globals == 1)
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].X == x & Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Y == y & (int)Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId].Trigger == 1 & Core.Type.TempPlayer[index].EventMap.EventPages[i].Visible == true)
                                    begineventprocessing = true;
                            }
                            else if (Core.Type.TempPlayer[index].EventMap.EventPages[i].X == x & Core.Type.TempPlayer[index].EventMap.EventPages[i].Y == y & (int)Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId].Trigger == 1 & Core.Type.TempPlayer[index].EventMap.EventPages[i].Visible == true)
                                begineventprocessing = true;
                          
                            if (Conversions.ToInteger(begineventprocessing) == 1)
                            {
                                // Process this event, it is on-touch and everything checks out.
                                if (Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId].CommandListCount > 0)
                                {
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Active = 0;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].ActionTimer = General.GetTimeMs();
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].CurList = 0;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].CurSlot = 0;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].EventId = Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].PageId = Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].WaitingForResponse = 0;

                                    int EventId = Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId;
                                    int PageId = Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId;
                                    int commandListCount = Core.Type.Map[GetPlayerMap(index)].Event[EventId].Pages[PageId].CommandListCount;

                                    Array.Resize(ref Core.Type.TempPlayer[index].EventProcessing[EventId].ListLeftOff, commandListCount);
                                }
                                begineventprocessing = false;
                            }
                        }
                    }
                }
            }
        }

        public static bool IsTileBlocked(int index, int mapNum, int x, int y, DirectionType dir)
        {      
            // Check for NPC and player blocking  
            var loopTo = NetworkConfig.Socket.HighIndex;
            for (int i = 0; i < loopTo; i++)
            {
                if (Core.Type.Moral[Map[mapNum].Moral].PlayerBlock)
                {
                    if (NetworkConfig.IsPlaying(i) & GetPlayerMap(i) == mapNum)
                    {
                        if (GetPlayerX(i) == x && GetPlayerY(i) == y)
                        {
                            return true;
                        }
                    }
                }
            }

            var loopTo2 = Core.Constant.MAX_MAP_NPCS;
            for (int i = 0; i < loopTo2; i++)
            {
                if (Core.Type.Moral[Core.Type.Map[mapNum].Moral].NPCBlock)
                {
                    if (Core.Type.MapNPC[mapNum].NPC[i].Num >= 0)
                    {
                        if (Core.Type.MapNPC[mapNum].NPC[i].X == x && Core.Type.MapNPC[mapNum].NPC[i].Y == y)
                        {
                            return true;
                        }
                    }
                }
            }

            // Check to make sure that the tile is walkable  
            if (IsDirBlocked(ref Map[mapNum].Tile[x, y].DirBlock, (byte)dir))
            {
                return true;
            }

            if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.Blocked || Core.Type.Map[mapNum].Tile[x, y].Type2 == TileType.Blocked)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Inventory

        public static int HasItem(int index, int itemNum)
        {
            int HasItemRet = default;
            int i;

            // Check for subscript out of range
            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                return HasItemRet;
            }

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo; i++)
            {
                // Check to see if the player has the item
                if (GetPlayerInv(index, i) == itemNum)
                {
                    if (Core.Type.Item[itemNum].Type == (byte)ItemType.Currency | Core.Type.Item[itemNum].Stackable == 1)
                    {
                        HasItemRet += GetPlayerInvValue(index, i);
                    }
                    else
                    {
                        HasItemRet += 1;
                    }
                }
            }

            return HasItemRet;

        }

        public static int FindItemSlot(int index, int itemNum)
        {
            int FindItemSlotRet = default;
            int i;

            FindItemSlotRet = 0;

            // Check for subscript out of range
            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                return FindItemSlotRet;
            }

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo; i++)
            {
                // Check to see if the player has the item
                if (GetPlayerInv(index, i) == itemNum)
                {
                    FindItemSlotRet = i;
                    return FindItemSlotRet;
                }
            }

            return FindItemSlotRet;

        }

        public static void PlayerMapGetItem(int index)
        {
            int i;
            int itemnum;
            int n;
            int mapNum;
            string Msg;

            mapNum = GetPlayerMap(index);

            var loopTo = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                // See if theres even an item here
                if (Core.Type.MapItem[mapNum, i].Num >= 0 & Core.Type.MapItem[mapNum, i].Num < Core.Constant.MAX_ITEMS)
                {
                    // our drop?
                    if (CanPlayerPickupItem(index, i))
                    {
                        // Check if item is at the same location as the player
                        if (Core.Type.MapItem[mapNum, i].X == GetPlayerX(index))
                        {
                            if (Core.Type.MapItem[mapNum, i].Y == GetPlayerY(index))
                            {
                                // Find open slot
                                n = FindOpenInvSlot(index, (int)Core.Type.MapItem[mapNum, i].Num);

                                // Open slot available?
                                if (n != -1)
                                {
                                    // Set item in players inventor
                                    itemnum = (int)MapItem[mapNum, i].Num;

                                    SetPlayerInv(index, n, (int)MapItem[mapNum, i].Num);

                                    if (Core.Type.Item[GetPlayerInv(index, n)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerInv(index, n)].Stackable == 1)
                                    {
                                        SetPlayerInvValue(index, n, GetPlayerInvValue(index, n) + MapItem[mapNum, i].Value);
                                        Msg = MapItem[mapNum, i].Value + " " + Core.Type.Item[GetPlayerInv(index, n)].Name;
                                    }
                                    else
                                    {
                                        SetPlayerInvValue(index, n, 1);
                                        Msg = Core.Type.Item[GetPlayerInv(index, n)].Name;
                                    }

                                    // Erase item from the map
                                    Item.SpawnItemSlot(i, -1, 0, GetPlayerMap(index), MapItem[mapNum, i].X, MapItem[mapNum, i].Y);
                                    NetworkSend.SendInventoryUpdate(index, n);                                 
                                    NetworkSend.SendActionMsg(GetPlayerMap(index), Msg, (int) ColorType.White, (byte)ActionMsgType.Static, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                    break;
                                }
                                else
                                {
                                    NetworkSend.PlayerMsg(index, "Your inventory is full.", (int) ColorType.BrightRed);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool CanPlayerPickupItem(int index, int mapitemNum)
        {
            bool CanPlayerPickupItemRet = default;
            int mapNum;

            mapNum = GetPlayerMap(index);

            if (Core.Type.Map[mapNum].Moral >= 0)
            {
                if (Core.Type.Moral[Core.Type.Map[mapNum].Moral].CanPickupItem)
                {
                    // no lock or locked to player?
                    if (string.IsNullOrEmpty(Core.Type.MapItem[mapNum, mapitemNum].PlayerName) | Core.Type.MapItem[mapNum, mapitemNum].PlayerName == GetPlayerName(index))
                    {
                        CanPlayerPickupItemRet = true;
                        return CanPlayerPickupItemRet;
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "You can't pickup items here!", (int) ColorType.BrightRed);
                }
            }

            CanPlayerPickupItemRet = false;
            return CanPlayerPickupItemRet;
        }

        public static int FindOpenInvSlot(int index, int itemNum)
        {
            int FindOpenInvSlotRet = default;
            int i;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                return FindOpenInvSlotRet;
            }

            if (Core.Type.Item[itemNum].Type == (byte)ItemType.Currency | Core.Type.Item[itemNum].Stackable == 1)
            {
                // If currency then check to see if they already have an instance of the item and add it to that
                var loopTo = Core.Constant.MAX_INV;
                for (i = 0; i < loopTo; i++)
                {
                    if (GetPlayerInv(index, i) == itemNum)
                    {
                        FindOpenInvSlotRet = i;
                        return FindOpenInvSlotRet;
                    }
                }
            }

            var loopTo1 = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo1; i++)
            {
                // Try to find an open free slot
                if (GetPlayerInv(index, i) == -1)
                {
                    FindOpenInvSlotRet = i;
                    return FindOpenInvSlotRet;
                }
            }

            FindOpenInvSlotRet = -1;
            return FindOpenInvSlotRet;
        }

        public static bool TakeInv(int index, int itemNum, int ItemVal)
        {
            bool TakeInvRet = default;
            int i;

            TakeInvRet = false;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                return TakeInvRet;
            }

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo; i++)
            {

                // Check to see if the player has the item
                if (GetPlayerInv(index, i) == itemNum)
                {
                    if (Core.Type.Item[itemNum].Type == (byte)ItemType.Currency | Core.Type.Item[itemNum].Stackable == 1)
                    {

                        // Is what we are trying to take away more then what they have?  If so just set it to zero
                        if (ItemVal >= GetPlayerInvValue(index, i))
                        {
                            TakeInvRet = true;
                        }
                        else
                        {
                            SetPlayerInvValue(index, i, GetPlayerInvValue(index, i) - ItemVal);
                            NetworkSend.SendInventoryUpdate(index, i);
                        }
                    }
                    else
                    {
                        TakeInvRet = true;
                    }

                    if (TakeInvRet)
                    {
                        SetPlayerInv(index, i, -1);
                        SetPlayerInvValue(index, i, 0);
                        // Send the inventory update
                        NetworkSend.SendInventoryUpdate(index, i);
                        return TakeInvRet;
                    }
                }

            }

            return TakeInvRet;

        }

        public static bool GiveInv(int index, int itemNum, int ItemVal, bool SendUpdate = true)
        {
            bool GiveInvRet = default;
            int i;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                GiveInvRet = false;
                return GiveInvRet;
            }

            i = FindOpenInvSlot(index, itemNum);

            // Check to see if inventory is full
            if (i != -1)
            {
                if (ItemVal == 0)
                    ItemVal = 1;

                SetPlayerInv(index, i, itemNum);
                SetPlayerInvValue(index, i, GetPlayerInvValue(index, i) + ItemVal);
                if (SendUpdate)
                    NetworkSend.SendInventoryUpdate(index, i);
                GiveInvRet = true;
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Your inventory is full.", (int)ColorType.BrightRed);
                GiveInvRet = false;
            }

            return GiveInvRet;

        }

        public static void PlayerMapDropItem(int index, int invNum, int amount)
        {
            int i;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | invNum < 0 | invNum > Core.Constant.MAX_INV)
            {
                return;
            }

            // check the player isn't doing something
            if (Core.Type.TempPlayer[index].InBank | Core.Type.TempPlayer[index].InShop >= 0 | Core.Type.TempPlayer[index].InTrade >= 0)
                return;

            if (Conversions.ToInteger(Core.Type.Moral[GetPlayerMap(index)].CanDropItem) == 0)
            {
                NetworkSend.PlayerMsg(index, "You can't drop items here!", (int) ColorType.BrightRed);
                return;
            }

            if (GetPlayerInv(index, invNum) >= 0)
            {
                if (GetPlayerInv(index, invNum) < Core.Constant.MAX_ITEMS)
                {
                    i = Item.FindOpenMapItemSlot(GetPlayerMap(index));

                    if (i != 0)
                    {
                        {
                            var withBlock = MapItem[GetPlayerMap(index), i];
                            withBlock.Num = GetPlayerInv(index, invNum);
                            withBlock.X = (byte)GetPlayerX(index);
                            withBlock.Y = (byte)GetPlayerY(index);
                            withBlock.PlayerName = GetPlayerName(index);
                            withBlock.PlayerTimer = General.GetTimeMs() + Constant.ITEM_SPAWN_TIME;

                            withBlock.CanDespawn = true;
                            withBlock.DespawnTimer = General.GetTimeMs() + Constant.ITEM_DESPAWN_TIME;

                            if (Core.Type.Item[GetPlayerInv(index, invNum)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerInv(index, invNum)].Stackable == 1)
                            {
                                // Check if its more then they have and if so drop it all
                                if (amount >= GetPlayerInvValue(index, invNum))
                                {
                                    amount = GetPlayerInvValue(index, invNum);
                                    withBlock.Value = amount;
                                    SetPlayerInv(index, invNum, -1);
                                    SetPlayerInvValue(index, invNum, 0);
                                }
                                else
                                {
                                    withBlock.Value = amount;
                                    SetPlayerInvValue(index, invNum, GetPlayerInvValue(index, invNum) - amount);
                                }
                                NetworkSend.MapMsg(GetPlayerMap(index), string.Format("{0} has dropped {1} ({2}x).", GetPlayerName(index), GameLogic.CheckGrammar(Core.Type.Item[GetPlayerInv(index, invNum)].Name), amount), (int) ColorType.Yellow);
                            }
                            else
                            {
                                // It's not a currency object so this is easy
                                withBlock.Value = 0;

                                // send message
                                NetworkSend.MapMsg(GetPlayerMap(index), string.Format("{0} has dropped {1}.", GetPlayerName(index), GameLogic.CheckGrammar(Core.Type.Item[GetPlayerInv(index, invNum)].Name)), (int) ColorType.Yellow);
                                SetPlayerInv(index, invNum, -1);
                                SetPlayerInvValue(index, invNum, 0);
                            }

                            // Send inventory update
                            NetworkSend.SendInventoryUpdate(index, invNum);
                            // Spawn the item before we set the num or we'll get a different free map item slot
                            Item.SpawnItemSlot(i, (int)withBlock.Num, amount, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index));
                        }
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "Too many items already on the ground.", (int) ColorType.Yellow);
                    }
                }
            }

        }

        public static bool TakeInvSlot(int index, int InvSlot, int ItemVal)
        {
            bool TakeInvSlotRet = default;
            object itemNum;

            TakeInvSlotRet = false;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | InvSlot < 0 | InvSlot > Core.Constant.MAX_ITEMS)
                return TakeInvSlotRet;

            itemNum = GetPlayerInv(index, InvSlot);

            if (Core.Type.Item[Conversions.ToInteger(itemNum)].Type == (byte)ItemType.Currency | Core.Type.Item[Conversions.ToInteger(itemNum)].Stackable == 1)
            {

                // Is what we are trying to take away more then what they have?  If so just set it to zero
                if (ItemVal >= GetPlayerInvValue(index, InvSlot))
                {
                    TakeInvSlotRet = true;
                }
                else
                {
                    SetPlayerInvValue(index, InvSlot, GetPlayerInvValue(index, InvSlot) - ItemVal);
                }
            }
            else
            {
                TakeInvSlotRet = true;
            }

            if (TakeInvSlotRet)
            {
                SetPlayerInv(index, InvSlot, -1);
                SetPlayerInvValue(index, InvSlot, 0);
                return TakeInvSlotRet;
            }

            return TakeInvSlotRet;

        }

        public static bool CanPlayerUseItem(int index, int itemNum)
        {
            int i;

            if ((int)Core.Type.Map[GetPlayerMap(index)].Moral >= 0)
            {
                if (Conversions.ToInteger(Core.Type.Moral[Core.Type.Map[GetPlayerMap(index)].Moral].CanUseItem) == 0)
                {
                    NetworkSend.PlayerMsg(index, "You can't use items here!", (int) ColorType.BrightRed);
                    return false;
                }
            }

            var loopTo = (byte)StatType.Count;
            for (i = 0; i < loopTo; i++)
            {
                if (GetPlayerStat(index, (StatType)i) < Core.Type.Item[itemNum].Stat_Req[i])
                {
                    NetworkSend.PlayerMsg(index, "You do not meet the stat requirements to use this item.", (int) ColorType.BrightRed);
                    return false;
                }
            }

            if (Core.Type.Item[itemNum].LevelReq > GetPlayerLevel(index))
            {
                NetworkSend.PlayerMsg(index, "You do not meet the level requirements to use this item.", (int) ColorType.BrightRed);
                return false;
            }

            // Make sure they are the right job
            if (!(Core.Type.Item[itemNum].JobReq == GetPlayerJob(index)) & !(Core.Type.Item[itemNum].JobReq == -1))
            {
                NetworkSend.PlayerMsg(index, "You do not meet the job requirements to use this item.", (int) ColorType.BrightRed);
                return false;
            }

            // access requirement
            if (!(GetPlayerAccess(index) >= Core.Type.Item[itemNum].AccessReq))
            {
                NetworkSend.PlayerMsg(index, "You do not meet the access requirement to equip this item.", (int) ColorType.BrightRed);
                return false;
            }

            // check the player isn't doing something
            if (Core.Type.TempPlayer[index].InBank == true | Core.Type.TempPlayer[index].InShop >= 0 | Core.Type.TempPlayer[index].InTrade >= 0)
            {
                NetworkSend.PlayerMsg(index, "You can't use items while in a bank, shop, or trade!", (int) ColorType.BrightRed);
                return false;
            }

            return true;

        }

        public static void UseItem(int index, int InvNum)
        {
            int itemNum;
            int i;
            int n;
            var tempitem = default(int);
            int m;
            var tempdata = new int[(int)(StatType.Count + 3 + 1)];
            var tempstr = new string[3];

            // Prevent hacking
            if (InvNum < 0 | InvNum > Core.Constant.MAX_INV)
                return;

            itemNum = GetPlayerInv(index, InvNum);

            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
                return;

            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(CanPlayerUseItem(index, itemNum), 0, false)))
                return;

            // Find out what kind of item it is
            switch (Core.Type.Item[itemNum].Type)
            {
                case (byte)ItemType.Equipment:
                    {
                        switch (Core.Type.Item[itemNum].SubType)
                        {
                            case (byte)EquipmentType.Weapon:
                                {

                                    if (GetPlayerEquipment(index, EquipmentType.Weapon) >= 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Weapon);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Weapon);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem >= 0) // give back the stored item
                                    {
                                        m = FindOpenInvSlot(index, tempitem);
                                        SetPlayerInv(index, m, tempitem);
                                        SetPlayerInvValue(index, m, 0);
                                    }

                                    NetworkSend.SendWornEquipment(index);
                                    NetworkSend.SendMapEquipment(index);
                                    NetworkSend.SendInventory(index);
                                    NetworkSend.SendInventoryUpdate(index, InvNum);
                                    NetworkSend.SendStats(index);

                                    // send vitals
                                    NetworkSend.SendVitals(index);
                                    break;
                                }

                            case (byte)EquipmentType.Armor:
                                {
                                    if (GetPlayerEquipment(index, EquipmentType.Armor) >= 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Armor);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Armor);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem >= 0) // Return their old equipment to their inventory.
                                    {
                                        m = FindOpenInvSlot(index, tempitem);
                                        SetPlayerInv(index, m, tempitem);
                                        SetPlayerInvValue(index, m, 0);
                                    }

                                    NetworkSend.SendWornEquipment(index);
                                    NetworkSend.SendMapEquipment(index);

                                    NetworkSend.SendInventory(index);
                                    NetworkSend.SendStats(index);

                                    // send vitals
                                    NetworkSend.SendVitals(index);
                                    break;
                                }

                            case (byte)EquipmentType.Helmet:
                                {
                                    if (GetPlayerEquipment(index, EquipmentType.Helmet) >= 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Helmet);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Helmet);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem >= 0) // give back the stored item
                                    {
                                        m = FindOpenInvSlot(index, tempitem);
                                        SetPlayerInv(index, m, tempitem);
                                        SetPlayerInvValue(index, m, 0);
                                    }

                                    NetworkSend.SendWornEquipment(index);
                                    NetworkSend.SendMapEquipment(index);
                                    NetworkSend.SendInventory(index);
                                    NetworkSend.SendStats(index);

                                    // send vitals
                                    NetworkSend.SendVitals(index);
                                    break;
                                }

                            case (byte)EquipmentType.Shield:
                                {
                                    if (GetPlayerEquipment(index, EquipmentType.Shield) >= 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Shield);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Shield);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem >= 0) // give back the stored item
                                    {
                                        m = FindOpenInvSlot(index, tempitem);
                                        SetPlayerInv(index, m, tempitem);
                                        SetPlayerInvValue(index, m, 0);
                                    }

                                    NetworkSend.SendWornEquipment(index);
                                    NetworkSend.SendMapEquipment(index);
                                    NetworkSend.SendInventory(index);
                                    NetworkSend.SendStats(index);

                                    // send vitals
                                    NetworkSend.SendVitals(index);
                                    break;
                                }

                        }

                        break;
                    }

                case (byte)ItemType.Consumable:
                    {
                        switch (Core.Type.Item[itemNum].SubType)
                        {
                            case (byte)ConsumableType.HP:
                                {
                                    NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + Core.Type.Item[itemNum].Data1, (int) ColorType.BrightGreen, (byte) ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                    Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                    SetPlayerVital(index, VitalType.HP, GetPlayerVital(index, VitalType.HP) + Core.Type.Item[itemNum].Data1);
                                    if (Core.Type.Item[itemNum].Stackable == 1)
                                    {
                                        TakeInv(index, itemNum, 1);
                                    }
                                    else
                                    {
                                        TakeInv(index, itemNum, 0);
                                    }
                                    NetworkSend.SendVital(index, VitalType.HP);
                                    break;
                                }

                            case (byte)ConsumableType.MP:
                                {
                                    NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + Core.Type.Item[itemNum].Data1, (int) ColorType.BrightBlue, (byte) ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                    Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                    SetPlayerVital(index, VitalType.SP, GetPlayerVital(index, VitalType.SP) + Core.Type.Item[itemNum].Data1);
                                    if (Core.Type.Item[itemNum].Stackable == 1)
                                    {
                                        TakeInv(index, itemNum, 1);
                                    }
                                    else
                                    {
                                        TakeInv(index, itemNum, 0);
                                    }
                                    NetworkSend.SendVital(index, VitalType.SP);
                                    break;
                                }

                            case (byte)ConsumableType.SP:
                                {
                                    Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                    SetPlayerVital(index, VitalType.SP, GetPlayerVital(index, VitalType.SP) + Core.Type.Item[itemNum].Data1);
                                    if (Core.Type.Item[itemNum].Stackable == 1)
                                    {
                                        TakeInv(index, itemNum, 1);
                                    }
                                    else
                                    {
                                        TakeInv(index, itemNum, 0);
                                    }
                                    NetworkSend.SendVital(index, VitalType.SP);
                                    break;
                                }

                            case (byte)ConsumableType.Exp:
                                {
                                    Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                    SetPlayerExp(index, GetPlayerExp(index) + Core.Type.Item[itemNum].Data1);
                                    if (Core.Type.Item[itemNum].Stackable == 1)
                                    {
                                        TakeInv(index, itemNum, 1);
                                    }
                                    else
                                    {
                                        TakeInv(index, itemNum, 0);
                                    }
                                    NetworkSend.SendExp(index);
                                    break;
                                }

                        }

                        break;
                    }

                case (byte)ItemType.Projectile:
                    {
                        if (Core.Type.Item[itemNum].Ammo > 0)
                        {
                            if (Conversions.ToBoolean(HasItem(index, Core.Type.Item[itemNum].Ammo)))
                            {
                                TakeInv(index, Core.Type.Item[itemNum].Ammo, 1);
                                Projectile.PlayerFireProjectile(index);
                            }
                            else
                            {
                                NetworkSend.PlayerMsg(index, "No More " + Core.Type.Item[Core.Type.Item[GetPlayerEquipment(index, EquipmentType.Weapon)].Ammo].Name + " !", (int) ColorType.BrightRed);
                                return;
                            }
                        }
                        else
                        {
                            Projectile.PlayerFireProjectile(index);
                            return;
                        }

                        break;
                    }

                case (byte)ItemType.Event:
                    {
                        n = Core.Type.Item[itemNum].Data1;

                        switch (Core.Type.Item[itemNum].SubType)
                        {
                            case (byte)CommonEventType.Variable:
                                {
                                    Core.Type.Player[index].Variables[n] = Core.Type.Item[itemNum].Data2;
                                    break;
                                }
                            case (byte)CommonEventType.Switch:
                                {
                                    Core.Type.Player[index].Switches[n] = (byte)Core.Type.Item[itemNum].Data2;
                                    break;
                                }
                            case (byte)CommonEventType.Key:
                                {
                                    EventLogic.TriggerEvent(index, 1, 0, GetPlayerX(index), GetPlayerY(index));
                                    break;
                                }
                        }

                        break;
                    }

                case (byte)ItemType.Skill:
                    {
                        PlayerLearnSkill(index, itemNum);
                        break;
                    }

                case (byte)ItemType.Pet:
                    {
                        TakeInv(index, itemNum, 1);
                        n = Core.Type.Item[itemNum].Data1;
                        break;
                    }
            }
        }

        public static void PlayerLearnSkill(int index, int itemNum, int skillNum = -1)
        {
            int n;
            int i;

            // Get the skill num
            if (skillNum >= 0)
            {
                n = skillNum;
            }
            else
            {
                n = Core.Type.Item[itemNum].Data1;
            }

            if (n < 0 | n > Core.Constant.MAX_SKILLS)
                return;

            // Make sure they are the right class
            if (Core.Type.Skill[n].JobReq == GetPlayerJob(index) | Core.Type.Skill[n].JobReq == -1)
            {
                // Make sure they are the right level
                i = Core.Type.Skill[n].LevelReq;

                if (i <= GetPlayerLevel(index))
                {
                    i = FindOpenSkill(index);

                    // Make sure they have an open skill slot
                    if (i >= 0)
                    {
                        // Make sure they dont already have the skill
                        if (!HasSkill(index, n))
                        {
                            SetPlayerSkill(index, i, n);
                            if (itemNum >= 0)
                            {
                                Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                TakeInv(index, itemNum, 0);
                            }
                            NetworkSend.PlayerMsg(index, "You study the skill carefully.", (int) ColorType.Yellow);
                            NetworkSend.PlayerMsg(index, "You have learned a new skill!", (int) ColorType.BrightGreen);
                            NetworkSend.SendPlayerSkills(index);
                        }
                        else
                        {
                            NetworkSend.PlayerMsg(index, "You have already learned this skill!", (int) ColorType.BrightRed);
                        }
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "You have learned all that you can learn!", (int) ColorType.BrightRed);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "You must be level " + i + " to learn this skill.", (int) ColorType.Yellow);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, string.Format("Only {0} can use this skill.", GameLogic.CheckGrammar(Core.Type.Job[Core.Type.Skill[n].JobReq].Name, 1)), (int) ColorType.BrightRed);
            }
        }

        public static void PlayerSwitchInvSlots(int index, int OldSlot, int NewSlot)
        {
            int OldNum;
            int OldValue;
            int OldRarity;
            string OldPrefix;
            string OldSuffix;
            int OldSpeed;
            int OldDamage;
            int NewNum;
            int NewValue;
            int NewRarity;
            string NewPrefix;
            string NewSuffix;
            int NewSpeed;
            int NewDamage;

            if (OldSlot == -1 | NewSlot == -1)
                return;

            OldNum = GetPlayerInv(index, OldSlot);
            OldValue = GetPlayerInvValue(index, OldSlot);
            NewNum = GetPlayerInv(index, NewSlot);
            NewValue = GetPlayerInvValue(index, NewSlot);

            if (NewNum >= 0)
            {
                if (OldNum == NewNum & Core.Type.Item[NewNum].Stackable == 1) // same item, if we can stack it, lets do that :P
                {
                    SetPlayerInv(index, NewSlot, NewNum);
                    SetPlayerInvValue(index, NewSlot, OldValue + NewValue);
                    SetPlayerInv(index, OldSlot, 0);
                    SetPlayerInvValue(index, OldSlot, 0);
                }
                else
                {
                    SetPlayerInv(index, NewSlot, OldNum);
                    SetPlayerInvValue(index, NewSlot, OldValue);
                    SetPlayerInv(index, OldSlot, NewNum);
                    SetPlayerInvValue(index, OldSlot, NewValue);
                }
            }
            else
            {
                SetPlayerInv(index, NewSlot, OldNum);
                SetPlayerInvValue(index, NewSlot, OldValue);
                SetPlayerInv(index, OldSlot, NewNum);
                SetPlayerInvValue(index, OldSlot, NewValue);
            }

            NetworkSend.SendInventory(index);
        }

        public static void PlayerSwitchSkillSlots(int index, int OldSlot, int NewSlot)
        {
            double OldNum;
            int OldValue;
            int OldRarity;
            string OldPrefix;
            string OldSuffix;
            int OldSpeed;
            int OldDamage;
            int NewNum;
            int NewValue;
            int NewRarity;
            string NewPrefix;
            string NewSuffix;
            int NewSpeed;
            int NewDamage;

            if (OldSlot == -1 | NewSlot == -1)
                return;

            OldNum = GetPlayerSkill(index, (int)OldSlot);
            OldValue = GetPlayerSkillCD(index, (int)OldSlot);
            NewNum = GetPlayerSkill(index, (int)NewSlot);
            NewValue = GetPlayerSkillCD(index, (int)NewSlot);

            if (NewNum >= 0)
            {
                if (OldNum == NewNum & Core.Type.Item[(int)NewNum].Stackable == 1) // same item, if we can stack it, lets do that :P
                {
                    SetPlayerSkill(index, (int)NewSlot, NewNum);
                    SetPlayerSkillCD(index, (int)NewSlot, NewValue);
                    SetPlayerSkill(index, (int)OldSlot, 0);
                    SetPlayerSkillCD(index, (int)OldSlot, 0);
                }
                else
                {
                    SetPlayerSkill(index, (int)NewSlot, (int)OldNum);
                    SetPlayerSkillCD(index, (int)NewSlot, OldValue);
                    SetPlayerSkill(index, (int)OldSlot, (int)NewNum);
                    SetPlayerSkillCD(index, (int)OldSlot, NewValue);
                }
            }
            else
            {
                SetPlayerSkill(index, (int)NewSlot, (int)OldNum);
                SetPlayerSkillCD(index, (int)NewSlot, OldValue);
                SetPlayerSkill(index, (int)OldSlot, (int)NewNum);
                SetPlayerSkillCD(index, (int)OldSlot, NewValue);
            }

            NetworkSend.SendPlayerSkills(index);
        }

        #endregion

        #region Equipment

        public static void CheckEquippedItems(int index)
        {
            double itemNum;
            int i;

            // We want to check incase an admin takes away an object but they had it equipped
            var loopTo = EquipmentType.Count;
            for (i = 0; i < (int)loopTo; i++)
            {
                itemNum = GetPlayerEquipment(index, (EquipmentType)i);

                if (itemNum >= 0)
                {

                    switch (i)
                    {
                        case (byte)EquipmentType.Weapon:
                            {

                                if (Core.Type.Item[(int)itemNum].SubType != (byte)EquipmentType.Weapon)
                                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                                break;
                            }
                        case (byte)EquipmentType.Armor:
                            {

                                if (Core.Type.Item[(int)itemNum].SubType != (byte)EquipmentType.Armor)
                                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                                break;
                            }
                        case (byte)EquipmentType.Helmet:
                            {

                                if (Core.Type.Item[(int)itemNum].SubType != (byte)EquipmentType.Helmet)
                                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                                break;
                            }
                        case (byte)EquipmentType.Shield:
                            {

                                if (Core.Type.Item[(int)itemNum].SubType != (byte)EquipmentType.Shield)
                                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                                break;
                            }
                    }
                }
                else
                {
                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                }

            }

        }

        public static void PlayerUnequipItem(int index, int EqSlot)
        {
            int i;
            int m;
            int itemNum;

            if (EqSlot < 1 | EqSlot > (byte)EquipmentType.Count)
                return; // exit out early if error'd

            if (GetPlayerEquipment(index, (EquipmentType)EqSlot) < 0 || GetPlayerEquipment(index, (EquipmentType)EqSlot) > Core.Constant.MAX_ITEMS)
                return;

            if (FindOpenInvSlot(index, GetPlayerEquipment(index, (EquipmentType)EqSlot)) >= 0)
            {
                itemNum = GetPlayerEquipment(index, (EquipmentType)EqSlot);

                m = FindOpenInvSlot(index, (int)Core.Type.Player[index].Equipment[EqSlot]);
                SetPlayerInv(index, m, Core.Type.Player[index].Equipment[EqSlot]);
                SetPlayerInvValue(index, m, 0);

                NetworkSend.PlayerMsg(index, "You unequip " + GameLogic.CheckGrammar(Core.Type.Item[GetPlayerEquipment(index, (EquipmentType)EqSlot)].Name), (int) ColorType.Yellow);

                // remove equipment
                SetPlayerEquipment(index, -1, (EquipmentType)EqSlot);
                NetworkSend.SendWornEquipment(index);
                NetworkSend.SendMapEquipment(index);
                NetworkSend.SendStats(index);
                NetworkSend.SendInventory(index);

                // send vitals
                NetworkSend.SendVitals(index);
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Your inventory is full.", (int) ColorType.BrightRed);
            }

        }

        public static void JoinGame(int index)
        {
            try
            {
                Script.Instance?.JoinGame(index);

                General.UpdateCaption();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void LeftGame(int index)
        {
            try
            {
                Script.Instance?.LeftGame(index);

                if (Core.Type.TempPlayer[index].InGame)
                {
                    Database.SaveCharacter(index, Core.Type.TempPlayer[index].Slot);
                    Database.SaveBank(index);
                }

                Database.ClearPlayer(index);

                General.UpdateCaption();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static int KillPlayer(int index)
        {
            try
            {
                int exp = Script.Instance?.KillPlayer(index);
                return exp;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;

        }

        public static void OnDeath(int index)
        {
            try
            {
                // Clear skill casting
                Core.Type.TempPlayer[index].SkillBuffer = -1;
                Core.Type.TempPlayer[index].SkillBufferTimer = 0;
                NetworkSend.SendClearSkillBuffer(index);

                Script.Instance?.OnDeath();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        #endregion

        #region Bank

        public static void GiveBank(int index, int InvSlot, int Amount)
        {
            byte bankSlot;
            int itemNum;

            if (InvSlot < 0 | InvSlot > Core.Constant.MAX_INV)
                return;

            if (Amount < 0)
                Amount = 0;

            if (GetPlayerInvValue(index, InvSlot) < Amount & GetPlayerInv(index, InvSlot) == 0)
                return;

            bankSlot = FindOpenbankSlot(index, GetPlayerInv(index, InvSlot));
            itemNum = GetPlayerInv(index, InvSlot);

            if (bankSlot >= 0)
            {
                if (Core.Type.Item[GetPlayerInv(index, InvSlot)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerInv(index, InvSlot)].Stackable == 1)
                {
                    if (GetPlayerBank(index, bankSlot) == GetPlayerInv(index, InvSlot))
                    {
                        SetPlayerBankValue(index, bankSlot, GetPlayerBankValue(index, bankSlot) + Amount);
                        TakeInv(index, GetPlayerInv(index, InvSlot), Amount);
                    }
                    else
                    {
                        SetPlayerBank(index, bankSlot, GetPlayerInv(index, InvSlot));
                        SetPlayerBankValue(index, bankSlot, Amount);
                        TakeInv(index, GetPlayerInv(index, InvSlot), Amount);
                    }
                }
                else if (GetPlayerBank(index, bankSlot) == GetPlayerInv(index, InvSlot))
                {
                    SetPlayerBankValue(index, bankSlot, GetPlayerBankValue(index, bankSlot) + 1);
                    TakeInv(index, GetPlayerInv(index, InvSlot), 0);
                }
                else
                {
                    SetPlayerBank(index, bankSlot, itemNum);
                    SetPlayerBankValue(index, bankSlot, 1);
                    TakeInv(index, GetPlayerInv(index, InvSlot), 0);
                }

                NetworkSend.SendBank(index);
            }

        }

        public static int GetPlayerBank(int index, byte bankSlot)
        {
            int GetPlayerBankRet = default;
            GetPlayerBankRet = Bank[index].Item[bankSlot].Num;
            return GetPlayerBankRet;
        }

        public static void SetPlayerBank(int index, byte bankSlot, int itemNum)
        {
            Bank[index].Item[bankSlot].Num = itemNum;
        }

        public static int GetPlayerBankValue(int index, byte bankSlot)
        {
            int GetPlayerBankValueRet = default;
            GetPlayerBankValueRet = Bank[index].Item[bankSlot].Value;
            return GetPlayerBankValueRet;
        }

        public static void SetPlayerBankValue(int index, byte bankSlot, int Value)
        {
            Bank[index].Item[bankSlot].Value = Value;
        }

        public static byte FindOpenbankSlot(int index, int itemNum)
        {
            byte FindOpenbankSlotRet = default;
            int i;

            if (!NetworkConfig.IsPlaying(index))
                return FindOpenbankSlotRet;
            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
                return FindOpenbankSlotRet;

            if (Core.Type.Item[itemNum].Type == (byte)ItemType.Currency | Core.Type.Item[itemNum].Stackable == 1)
            {
                var loopTo = Core.Constant.MAX_BANK;
                for (i = 0; i < loopTo; i++)
                {
                    if (GetPlayerBank(index, (byte)i) == itemNum)
                    {
                        FindOpenbankSlotRet = (byte)i;
                        return FindOpenbankSlotRet;
                    }
                }
            }

            var loopTo1 = Core.Constant.MAX_BANK;
            for (i = 0; i < loopTo1; i++)
            {
                if (GetPlayerBank(index, (byte)i) == -1)
                {
                    FindOpenbankSlotRet = (byte)i;
                    return FindOpenbankSlotRet;
                }
            }

            return FindOpenbankSlotRet;

        }

        public static void TakeBank(int index, byte bankSlot, int Amount)
        {
            int invSlot;

            if (bankSlot < 0 | bankSlot > Core.Constant.MAX_BANK)
                return;

            if (Amount < 0)
                Amount = 0;

            if (GetPlayerBankValue(index, bankSlot) < Amount)
                return;

            invSlot = FindOpenInvSlot(index, GetPlayerBank(index, bankSlot));

            if (invSlot >= 0)
            {
                if (Core.Type.Item[GetPlayerBank(index, bankSlot)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerBank(index, bankSlot)].Stackable == 1)
                {
                    GiveInv(index, GetPlayerBank(index, bankSlot), Amount);
                    SetPlayerBankValue(index, bankSlot, GetPlayerBankValue(index, bankSlot) - Amount);
                    if (GetPlayerBankValue(index, bankSlot) < 0)
                    {
                        SetPlayerBank(index, bankSlot, 0);
                        SetPlayerBankValue(index, bankSlot, 0);
                    }
                }
                else if (GetPlayerBank(index, bankSlot) == GetPlayerInv(index, (int)invSlot))
                {
                    if (GetPlayerBankValue(index, bankSlot) > 1)
                    {
                        GiveInv(index, GetPlayerBank(index, bankSlot), 0);
                        SetPlayerBankValue(index, bankSlot, GetPlayerBankValue(index, bankSlot) - 1);
                    }
                }
                else
                {
                    GiveInv(index, GetPlayerBank(index, bankSlot), 0);
                    SetPlayerBank(index, bankSlot, -1);
                    SetPlayerBankValue(index, bankSlot, 0);
                }

            }

            NetworkSend.SendBank(index);
        }

        public static void PlayerSwitchbankSlots(int index, int OldSlot, int NewSlot)
        {
            int OldNum;
            int OldValue;
            int NewNum;
            int NewValue;
            int i;

            if (OldSlot == -1 | NewSlot == -1)
                return;

            OldNum = GetPlayerBank(index, (byte)OldSlot);
            OldValue = GetPlayerBankValue(index, (byte)OldSlot);
            NewNum = GetPlayerBank(index, (byte)NewSlot);
            NewValue = GetPlayerBankValue(index, (byte)NewSlot);

            SetPlayerBank(index, (byte)NewSlot, OldNum);
            SetPlayerBankValue(index, (byte)NewSlot, OldValue);

            SetPlayerBank(index, (byte)OldSlot, NewNum);
            SetPlayerBankValue(index, (byte)OldSlot, NewValue);

            NetworkSend.SendBank(index);
        }

        #endregion

    }
}