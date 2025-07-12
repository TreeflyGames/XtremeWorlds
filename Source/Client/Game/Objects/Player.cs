using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Global.Command;

namespace Client
{

    public class Player
    {
        #region Database
        public static void ClearPlayers()
        {
            Core.Type.Account = new Core.Type.AccountStruct[Constant.MAX_PLAYERS];
            Core.Type.Player = new Core.Type.PlayerStruct[Constant.MAX_PLAYERS];
            Core.Type.TempPlayer = new Core.Type.TempPlayerStruct[Core.Constant.MAX_PLAYERS];

            for (int i = 0; i < Constant.MAX_PLAYERS; i++)
            {
                ClearPlayer(i);
            }
        }

        public static void ClearAccount(int index)
        {
            Core.Type.Account[index].Login = "";
            Core.Type.Account[index].Password = "";
        }

        public static void ClearPlayer(int index)
        {
            ClearAccount(index);

            Core.Type.Player[index].Name = "";
            Core.Type.Player[index].Attacking = 0;
            Core.Type.Player[index].AttackTimer = 0;
            Core.Type.Player[index].Job = 0;
            Core.Type.Player[index].Dir = 0;
            Core.Type.Player[index].Access = (byte)Core.Enum.AccessType.Player;

            Core.Type.Player[index].Equipment = new int[(int)Core.Enum.EquipmentType.Count];
            for (int y = 0; y < (int)Core.Enum.EquipmentType.Count; y++)
                Core.Type.Player[index].Equipment[y] = -1;

            Core.Type.Player[index].Exp = 0;
            Core.Type.Player[index].Level = 0;
            Core.Type.Player[index].Map = 0;
            Core.Type.Player[index].MapGetTimer = 0;
            Core.Type.Player[index].Moving = 0;
            Core.Type.Player[index].PK = false;
            Core.Type.Player[index].Points = 0;
            Core.Type.Player[index].Sprite = 0;

            Core.Type.Player[index].Inv = new Core.Type.PlayerInvStruct[(Constant.MAX_INV)];
            for (int x = 0; x < Constant.MAX_INV; x++)
            {
                Core.Type.Player[index].Inv[x].Num = -1;
                Core.Type.Player[index].Inv[x].Value = 0;
                Core.Type.TradeTheirOffer[x].Num = -1;
                Core.Type.TradeYourOffer[x].Num = -1;
            }

            Core.Type.Player[index].Skill = new Core.Type.PlayerSkillStruct[(Constant.MAX_PLAYER_SKILLS)];
            for (int x = 0; x < Constant.MAX_PLAYER_SKILLS; x++)
            {
                Core.Type.Player[index].Skill[x].Num = -1;
                Core.Type.Player[index].Skill[x].CD = 0;
            }

            Core.Type.Player[index].Stat = new byte[(int)Core.Enum.StatType.Count];
            for (int x = 0; x < (int)Core.Enum.StatType.Count; x++)
                Core.Type.Player[index].Stat[x] = 0;

            Core.Type.Player[index].Steps = 0;

            Core.Type.Player[index].Vital = new int[(int)Core.Enum.VitalType.Count];
            for (int x = 0; x < (int)Core.Enum.VitalType.Count; x++)
                Core.Type.Player[index].Vital[x] = 0;

            Core.Type.Player[index].X = 0;
            Core.Type.Player[index].XOffset = 0;
            Core.Type.Player[index].Y = 0;
            Core.Type.Player[index].YOffset = 0;

            Core.Type.Player[index].Hotbar = new Core.Type.HotbarStruct[(Constant.MAX_HOTBAR)];
            Core.Type.Player[index].GatherSkills = new Core.Type.ResourceTypetruct[(int)Core.Enum.ResourceType.Count];
            Core.Type.Player[index].GatherSkills = new Core.Type.ResourceTypetruct[(int)Core.Enum.ResourceType.Count];

            Trade.InTrade = -1;
        }
        #endregion

        #region Movement
        public static void CheckMovement()
        {
            if (IsTryingToMove() && CanMove() && Core.Type.Player[GameState.MyIndex].XOffset == 0 && Core.Type.Player[GameState.MyIndex].YOffset == 0)
            {
                // Check if player has the shift key down for running
                if (GameState.VbKeyShift)
                {
                    Core.Type.Player[GameState.MyIndex].Moving = (byte)Core.Enum.MovementType.Walking;
                }
                else
                {
                    Core.Type.Player[GameState.MyIndex].Moving = (byte)Core.Enum.MovementType.Running;
                }

                switch (GetPlayerDir(GameState.MyIndex))
                {
                    case (int)Core.Enum.DirectionType.Up:
                        {
                            NetworkSend.SendPlayerMove();
                            Core.Type.Player[GameState.MyIndex].YOffset = GameState.PicY;
                            SetPlayerY(GameState.MyIndex, GetPlayerY(GameState.MyIndex) - 1);
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Down:
                        {
                            NetworkSend.SendPlayerMove();
                            Core.Type.Player[GameState.MyIndex].YOffset = GameState.PicY * -1;
                            SetPlayerY(GameState.MyIndex, GetPlayerY(GameState.MyIndex) + 1);
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Left:
                        {
                            NetworkSend.SendPlayerMove();
                            Core.Type.Player[GameState.MyIndex].XOffset = GameState.PicX;
                            SetPlayerX(GameState.MyIndex, GetPlayerX(GameState.MyIndex) - 1);
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Right:
                        {
                            NetworkSend.SendPlayerMove();
                            Core.Type.Player[GameState.MyIndex].XOffset = GameState.PicX * -1;
                            SetPlayerX(GameState.MyIndex, GetPlayerX(GameState.MyIndex) + 1);
                            break;
                        }
                    case (int)Core.Enum.DirectionType.UpLeft:
                        {
                            NetworkSend.SendPlayerMove();
                            Core.Type.Player[GameState.MyIndex].XOffset = GameState.PicX;
                            SetPlayerX(GameState.MyIndex, GetPlayerX(GameState.MyIndex) - 1);
                            Core.Type.Player[GameState.MyIndex].YOffset = GameState.PicY;
                            SetPlayerY(GameState.MyIndex, GetPlayerY(GameState.MyIndex) - 1);
                            break;
                        }
                    case (int)Core.Enum.DirectionType.UpRight:
                        {
                            NetworkSend.SendPlayerMove();
                            Core.Type.Player[GameState.MyIndex].XOffset = GameState.PicX * -1;
                            SetPlayerX(GameState.MyIndex, GetPlayerX(GameState.MyIndex) + 1);
                            Core.Type.Player[GameState.MyIndex].YOffset = GameState.PicY;
                            SetPlayerY(GameState.MyIndex, GetPlayerY(GameState.MyIndex) - 1);
                            break;
                        }
                    case (int)Core.Enum.DirectionType.DownLeft:
                        {
                            NetworkSend.SendPlayerMove();
                            Core.Type.Player[GameState.MyIndex].XOffset = GameState.PicX;
                            SetPlayerX(GameState.MyIndex, GetPlayerX(GameState.MyIndex) - 1);
                            Core.Type.Player[GameState.MyIndex].YOffset = GameState.PicY * -1;
                            SetPlayerY(GameState.MyIndex, GetPlayerY(GameState.MyIndex) + 1);
                            break;
                        }
                    case (int)Core.Enum.DirectionType.DownRight:
                        {
                            NetworkSend.SendPlayerMove();
                            Core.Type.Player[GameState.MyIndex].XOffset = GameState.PicX * -1;
                            SetPlayerX(GameState.MyIndex, GetPlayerX(GameState.MyIndex) + 1);
                            Core.Type.Player[GameState.MyIndex].YOffset = GameState.PicY * -1;
                            SetPlayerY(GameState.MyIndex, GetPlayerY(GameState.MyIndex) + 1);
                            break;
                        }
                }

                if (Core.Type.Player[GameState.MyIndex].XOffset == 0 & Core.Type.Player[GameState.MyIndex].YOffset == 0)
                {
                    if (Core.Type.MyMap.Tile[GetPlayerX(GameState.MyIndex), GetPlayerY(GameState.MyIndex)].Type == Core.Enum.TileType.Warp | Core.Type.MyMap.Tile[GetPlayerX(GameState.MyIndex), GetPlayerY(GameState.MyIndex)].Type2 == Core.Enum.TileType.Warp)
                    {
                        GameState.GettingMap = true;
                    }
                }

            }
        }

        public static bool IsTryingToMove()
        {
            bool IsTryingToMoveRet = default;

            if (GameState.DirUp | GameState.DirDown | GameState.DirLeft | GameState.DirRight)
            {
                IsTryingToMoveRet = true;
            }

            return IsTryingToMoveRet;

        }

        public static bool CanMove()
        {
            bool CanMoveRet = default;
            int d;

            CanMoveRet = true;

            if (Core.Type.Player[GameState.MyIndex].XOffset != 0 | Core.Type.Player[GameState.MyIndex].YOffset != 0)
            {
                CanMoveRet = false;
                return CanMoveRet;
            }

            if (Conversions.ToInteger(Event.HoldPlayer) == 1)
            {
                CanMoveRet = false;
                return CanMoveRet;
            }

            if (Conversions.ToInteger(GameState.GettingMap) == 1)
            {
                CanMoveRet = false;
                return CanMoveRet;
            }

            // Make sure they aren't trying to move when they are already moving
            if (Core.Type.Player[GameState.MyIndex].Moving != 0)
            {
                CanMoveRet = false;
                return CanMoveRet;
            }

            // Make sure they haven't just casted a skill
            if (GameState.SkillBuffer >= 0)
            {
                CanMoveRet = false;
                return CanMoveRet;
            }

            // make sure they're not stunned
            if (GameState.StunDuration > 0)
            {
                CanMoveRet = false;
                return CanMoveRet;
            }

            if (Event.InEvent)
            {
                CanMoveRet = false;
                return CanMoveRet;
            }

            if (!GameState.inSmallChat)
            {
                CanMoveRet = false;
                return CanMoveRet;
            }

            if (Trade.InTrade >= 0)
            {
                Trade.SendDeclineTrade();
            }

            if (GameState.InShop >= 0)
            {
                Shop.CloseShop();
            }

            if (GameState.InBank)
            {
                Bank.CloseBank();
            }

            d = GetPlayerDir(GameState.MyIndex);

            switch (d)
            {
                case (int)Core.Enum.DirectionType.Up:
                    {

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && GetPlayerY(GameState.MyIndex) <= 0)
                        {
                            GameState.DirUp = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Down);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Core.Enum.DirectionType.Down:
                    {

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Down == 0 && GetPlayerY(GameState.MyIndex) >= Core.Type.MyMap.MaxY)
                        {
                            GameState.DirDown = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Up);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Core.Enum.DirectionType.Left:
                    {

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Left == 0 && GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirLeft = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Right);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Core.Enum.DirectionType.Right:
                    {

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) >= Core.Type.MyMap.MaxX)
                        {
                            GameState.DirRight = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Left);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Core.Enum.DirectionType.UpLeft:
                    {

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Left == 0 && GetPlayerY(GameState.MyIndex) <= 0 & GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirUp = false;
                            GameState.DirDown = true;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Down);
                            GameState.DirLeft = false;
                            GameState.DirRight = true;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Right);
                            return CanMoveRet;
                        }

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Down == 0 && GetPlayerY(GameState.MyIndex) <= 0)
                        {
                            GameState.DirUp = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Down);
                            return CanMoveRet;
                        }

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirLeft = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Right);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Core.Enum.DirectionType.UpRight:
                    {

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerY(GameState.MyIndex) >= Core.Type.MyMap.MaxY & GetPlayerX(GameState.MyIndex) >= Core.Type.MyMap.MaxX)
                        {
                            GameState.DirUp = false;
                            GameState.DirDown = true;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Down);
                            GameState.DirRight = false;
                            GameState.DirLeft = true;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Left);
                            return CanMoveRet;
                        }

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && GetPlayerY(GameState.MyIndex) <= 0)
                        {
                            GameState.DirUp = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Down);
                            return CanMoveRet;
                        }

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirLeft = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Right);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Core.Enum.DirectionType.DownLeft:
                    {

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerY(GameState.MyIndex) >= Core.Type.MyMap.MaxY & GetPlayerX(GameState.MyIndex) < 0)
                        {
                            GameState.DirDown = false;
                            GameState.DirUp = true;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Up);
                            GameState.DirLeft = false;
                            GameState.DirRight = true;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Right);
                            return CanMoveRet;
                        }

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && GetPlayerY(GameState.MyIndex) <= 0)
                        {
                            GameState.DirDown = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Up);
                            return CanMoveRet;
                        }

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirLeft = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Right);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Core.Enum.DirectionType.DownRight:
                    {

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Down == 0 && Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerY(GameState.MyIndex) >= Core.Type.MyMap.MaxY & GetPlayerX(GameState.MyIndex) >= Core.Type.MyMap.MaxX)
                        {
                            GameState.DirDown = false;
                            GameState.DirUp = true;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Up);
                            GameState.DirRight = false;
                            GameState.DirLeft = true;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Left);
                            return CanMoveRet;
                        }

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Down == 0 && GetPlayerY(GameState.MyIndex) >= Core.Type.MyMap.MaxY)
                        {
                            GameState.DirDown = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Up);
                            return CanMoveRet;
                        }

                        if (Core.Type.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) >= Core.Type.MyMap.MaxX)
                        {
                            GameState.DirRight = false;
                            SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Left);
                            return CanMoveRet;
                        }

                        break;
                    }

            }

            // Check for cardinal movements if no diagonal movements
            if (GameState.DirUp)
            {
                SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Up);
                if (GetPlayerY(GameState.MyIndex) > 0)
                {
                    if (CheckDirection((byte)Core.Enum.DirectionType.Up))
                    {
                        CanMoveRet = false;
                        if (d != (int)Core.Enum.DirectionType.Up)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Core.Type.MyMap.Up > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }

            if (GameState.DirDown)
            {
                SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Down);
                if (GetPlayerY(GameState.MyIndex) < Core.Type.MyMap.MaxY - 1)
                {
                    if (CheckDirection((byte)Core.Enum.DirectionType.Down))
                    {
                        CanMoveRet = false;
                        if (d != (int)Core.Enum.DirectionType.Down)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Core.Type.MyMap.Down > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }

            if (GameState.DirLeft)
            {
                SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Left);
                if (GetPlayerX(GameState.MyIndex) > 0)
                {
                    if (CheckDirection((byte)Core.Enum.DirectionType.Left))
                    {
                        CanMoveRet = false;
                        if (d != (int)Core.Enum.DirectionType.Left)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Core.Type.MyMap.Left > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }

            if (GameState.DirRight)
            {
                SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.Right);
                if (GetPlayerX(GameState.MyIndex) < Core.Type.MyMap.MaxX)
                {
                    if (CheckDirection((byte)Core.Enum.DirectionType.Right))
                    {
                        CanMoveRet = false;
                        if (d != (int)Core.Enum.DirectionType.Right)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Core.Type.MyMap.Right > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }

            // Check for diagonal movements first
            if (GameState.DirUp & GameState.DirRight)
            {
                SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.UpRight);
                if (GetPlayerY(GameState.MyIndex) > 0 & GetPlayerX(GameState.MyIndex) < Core.Type.MyMap.MaxX)
                {
                    if (CheckDirection((byte)Core.Enum.DirectionType.UpRight))
                    {
                        CanMoveRet = false;
                        if (d != (int)Core.Enum.DirectionType.UpRight)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Core.Type.MyMap.Up > 0 & Core.Type.MyMap.Right > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }
            else if (GameState.DirUp & GameState.DirLeft)
            {
                SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.UpLeft);
                if (GetPlayerY(GameState.MyIndex) > 0 & GetPlayerX(GameState.MyIndex) > 0)
                {
                    if (CheckDirection((byte)Core.Enum.DirectionType.UpLeft))
                    {
                        CanMoveRet = false;
                        if (d != (int)Core.Enum.DirectionType.UpLeft)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Core.Type.MyMap.Up > 0 & Core.Type.MyMap.Left > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }
            else if (GameState.DirDown & GameState.DirRight)
            {
                SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.DownRight);
                if (GetPlayerY(GameState.MyIndex) < Core.Type.MyMap.MaxY & GetPlayerX(GameState.MyIndex) < Core.Type.MyMap.MaxX)
                {
                    if (CheckDirection((byte)Core.Enum.DirectionType.DownRight))
                    {
                        CanMoveRet = false;
                        if (d != (int)Core.Enum.DirectionType.DownRight)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Core.Type.MyMap.Down > 0 & Core.Type.MyMap.Right > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }
            else if (GameState.DirDown & GameState.DirLeft)
            {
                SetPlayerDir(GameState.MyIndex, (int)Core.Enum.DirectionType.DownLeft);
                if (GetPlayerY(GameState.MyIndex) < Core.Type.MyMap.MaxY & GetPlayerX(GameState.MyIndex) > 0)
                {
                    if (CheckDirection((byte)Core.Enum.DirectionType.DownLeft))
                    {
                        CanMoveRet = false;
                        if (d != (int)Core.Enum.DirectionType.DownLeft)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Core.Type.MyMap.Down > 0 & Core.Type.MyMap.Left > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }

            return CanMoveRet;

        }

        public static bool CheckDirection(byte direction)
        {
            bool CheckDirectionRet = default;
            var x = default(int);
            var y = default(int);
            int i;

            if (GetPlayerX(GameState.MyIndex) >= Core.Type.Map[GetPlayerMap(GameState.MyIndex)].MaxX || GetPlayerY(GameState.MyIndex) >= Core.Type.Map[GetPlayerMap(GameState.MyIndex)].MaxY)
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            // check directional blocking
            if (GameLogic.IsDirBlocked(ref Core.Type.MyMap.Tile[GetPlayerX(GameState.MyIndex), GetPlayerY(GameState.MyIndex)].DirBlock, ref direction))
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            switch (direction)
            {
                case (byte)Core.Enum.DirectionType.Up:
                    {
                        x = GetPlayerX(GameState.MyIndex);
                        y = GetPlayerY(GameState.MyIndex) - 1;
                        break;
                    }
                case (byte)Core.Enum.DirectionType.Down:
                    {
                        x = GetPlayerX(GameState.MyIndex);
                        y = GetPlayerY(GameState.MyIndex) + 1;
                        break;
                    }
                case (byte)Core.Enum.DirectionType.Left:
                    {
                        x = GetPlayerX(GameState.MyIndex) - 1;
                        y = GetPlayerY(GameState.MyIndex);
                        break;
                    }
                case (byte)Core.Enum.DirectionType.Right:
                    {
                        x = GetPlayerX(GameState.MyIndex) + 1;
                        y = GetPlayerY(GameState.MyIndex);
                        break;
                    }
                case (byte)Core.Enum.DirectionType.UpLeft:
                    {
                        x = GetPlayerX(GameState.MyIndex) - 1;
                        y = GetPlayerY(GameState.MyIndex) - 1;
                        break;
                    }
                case (byte)Core.Enum.DirectionType.UpRight:
                    {
                        x = GetPlayerX(GameState.MyIndex) + 1;
                        y = GetPlayerY(GameState.MyIndex) - 1;
                        break;
                    }
                case (byte)Core.Enum.DirectionType.DownLeft:
                    {
                        x = GetPlayerX(GameState.MyIndex) - 1;
                        y = GetPlayerY(GameState.MyIndex) + 1;
                        break;
                    }
                case (byte)Core.Enum.DirectionType.DownRight:
                    {
                        x = GetPlayerX(GameState.MyIndex) + 1;
                        y = GetPlayerY(GameState.MyIndex) + 1;
                        break;
                    }
            }

            if (x < 0 || y < 0 || x >= Core.Type.MyMap.MaxX || y >= Core.Type.MyMap.MaxY)
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            // Check to see if the map tile is blocked or not
            if (Core.Type.MyMap.Tile[x, y].Type == Core.Enum.TileType.Blocked | Core.Type.MyMap.Tile[x, y].Type2 == Core.Enum.TileType.Blocked)
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            // Check to see if the map tile is tree or not
            if (Core.Type.MyMap.Tile[x, y].Type == Core.Enum.TileType.Resource | Core.Type.MyMap.Tile[x, y].Type2 == Core.Enum.TileType.Resource)
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            // Check to see if a player is already on that tile
            if (Core.Type.MyMap.Moral > 0)
            {
                if (Core.Type.Moral[Core.Type.MyMap.Moral].PlayerBlock)
                {
                    for (i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i))
                        {
                            if (Core.Type.Player[i].X == x & Core.Type.Player[i].Y == y)
                            {
                                CheckDirectionRet = true;
                                return CheckDirectionRet;
                            }
                        }
                    }
                }

                // Check to see if a NPC is already on that tile
                if (Core.Type.Moral[Core.Type.MyMap.Moral].NPCBlock)
                {
                    for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                    {
                        if (Core.Type.MyMapNPC[i].Num >= 0 & Core.Type.MyMapNPC[i].X == x & Core.Type.MyMapNPC[i].Y == y)
                        {
                            CheckDirectionRet = true;
                            return CheckDirectionRet;
                        }
                    }
                }
            }

            var loopTo = GameState.CurrentEvents;
            for (i = 0; i < loopTo; i++)
            {
                if (Core.Type.MapEvents[i].Visible == true)
                {
                    if (Core.Type.MapEvents[i].X == x & Core.Type.MapEvents[i].Y == y)
                    {
                        if (Core.Type.MapEvents[i].WalkThrough == 0)
                        {
                            CheckDirectionRet = true;
                            return CheckDirectionRet;
                        }
                    }
                }
            }

            return CheckDirectionRet;

        }

        public static void ProcessPlayerMovement(int index)
        {
            // Check if player is walking or running, and if so process moving them over
            switch (Core.Type.Player[index].Moving)
            {
                case (byte)Core.Enum.MovementType.Walking:
                    {
                        GameState.MovementSpeed = GameState.ElapsedTime / 1000.0d * GameState.WalkSpeed * GameState.SizeX; // Adjust speed by elapsed time
                        break;
                    }
                case (byte)Core.Enum.MovementType.Running:
                    {
                        GameState.MovementSpeed = GameState.ElapsedTime / 1000.0d * GameState.RunSpeed * GameState.SizeX; // Adjust speed by elapsed time
                        break;
                    }

                default:
                    {
                        return;
                    }
            }

            GameState.MovementSpeed = Math.Round(GameState.MovementSpeed);

            // Update player offsets based on direction
            switch (GetPlayerDir(index))
            {
                case (int)Core.Enum.DirectionType.Up:
                    {
                        Core.Type.Player[index].YOffset = (int)Math.Round(Core.Type.Player[index].YOffset - GameState.MovementSpeed);
                        if (Core.Type.Player[index].YOffset < 0)
                            Core.Type.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Down:
                    {
                        Core.Type.Player[index].YOffset = (int)Math.Round(Core.Type.Player[index].YOffset + GameState.MovementSpeed);
                        if (Core.Type.Player[index].YOffset > 0)
                            Core.Type.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Left:
                    {
                        Core.Type.Player[index].XOffset = (int)Math.Round(Core.Type.Player[index].XOffset - GameState.MovementSpeed);
                        if (Core.Type.Player[index].XOffset < 0)
                            Core.Type.Player[index].XOffset = 0;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Right:
                    {
                        Core.Type.Player[index].XOffset = (int)Math.Round(Core.Type.Player[index].XOffset + GameState.MovementSpeed);
                        if (Core.Type.Player[index].XOffset > 0)
                            Core.Type.Player[index].XOffset = 0;
                        break;
                    }
                case (int)Core.Enum.DirectionType.UpRight:
                    {
                        Core.Type.Player[index].XOffset = (int)Math.Round(Core.Type.Player[index].XOffset + GameState.MovementSpeed);
                        Core.Type.Player[index].YOffset = (int)Math.Round(Core.Type.Player[index].YOffset - GameState.MovementSpeed);
                        if (Core.Type.Player[index].XOffset > 0)
                            Core.Type.Player[index].XOffset = 0;
                        if (Core.Type.Player[index].YOffset < 0)
                            Core.Type.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Core.Enum.DirectionType.UpLeft:
                    {
                        Core.Type.Player[index].XOffset = (int)Math.Round(Core.Type.Player[index].XOffset - GameState.MovementSpeed);
                        Core.Type.Player[index].YOffset = (int)Math.Round(Core.Type.Player[index].YOffset - GameState.MovementSpeed);
                        if (Core.Type.Player[index].XOffset < 0)
                            Core.Type.Player[index].XOffset = 0;
                        if (Core.Type.Player[index].YOffset < 0)
                            Core.Type.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Core.Enum.DirectionType.DownRight:
                    {
                        Core.Type.Player[index].XOffset = (int)Math.Round(Core.Type.Player[index].XOffset + GameState.MovementSpeed);
                        Core.Type.Player[index].YOffset = (int)Math.Round(Core.Type.Player[index].YOffset + GameState.MovementSpeed);
                        if (Core.Type.Player[index].XOffset > 0)
                            Core.Type.Player[index].XOffset = 0;
                        if (Core.Type.Player[index].YOffset > 0)
                            Core.Type.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Core.Enum.DirectionType.DownLeft:
                    {
                        Core.Type.Player[index].XOffset = (int)Math.Round(Core.Type.Player[index].XOffset - GameState.MovementSpeed);
                        Core.Type.Player[index].YOffset = (int)Math.Round(Core.Type.Player[index].YOffset + GameState.MovementSpeed);
                        if (Core.Type.Player[index].XOffset < 0)
                            Core.Type.Player[index].XOffset = 0;
                        if (Core.Type.Player[index].YOffset > 0)
                            Core.Type.Player[index].YOffset = 0;
                        break;
                    }
            }

            // Check if completed walking over to the next tile
            if (Core.Type.Player[index].Moving > 0)
            {
                if (Core.Type.Player[index].XOffset == 0 & Core.Type.Player[index].YOffset == 0)
                {
                    Core.Type.Player[index].Moving = 0;
                    if (Core.Type.Player[index].Steps == 1)
                    {
                        Core.Type.Player[index].Steps = 3;
                    }
                    else
                    {
                        Core.Type.Player[index].Steps = 1;
                    }
                }
            }

        }


        #endregion

        #region Attacking
        public static void CheckAttack(bool mouse = false)
        {
            int attackSpeed;
            var x = default(int);
            var y = default(int);
            var buffer = new ByteStream(4);

            if (GameState.VbKeyControl | mouse)
            {
                if (GameState.MyIndex < 0 | GameState.MyIndex > Constant.MAX_PLAYERS)
                    return;

                if (Event.InEvent)
                    return;

                if (GameState.SkillBuffer >= 0)
                    return; // currently casting a skill, can't attack

                if (GameState.StunDuration > 0)
                    return; // stunned, can't attack

                // speed from weapon
                if (GetPlayerEquipment(GameState.MyIndex, Core.Enum.EquipmentType.Weapon) >= 0)
                {
                    attackSpeed = Core.Type.Item[GetPlayerEquipment(GameState.MyIndex, Core.Enum.EquipmentType.Weapon)].Speed * 1000;
                }
                else
                {
                    attackSpeed = 1000;
                }

                if (Core.Type.Player[GameState.MyIndex].AttackTimer + attackSpeed < General.GetTickCount())
                {
                    if (Core.Type.Player[GameState.MyIndex].Attacking == 0)
                    {
                        {
                            ref var withBlock = ref Core.Type.Player[GameState.MyIndex];
                            withBlock.Attacking = 1;
                            withBlock.AttackTimer = General.GetTickCount();
                        }

                        NetworkSend.SendAttack();
                    }
                }

                switch (Core.Type.Player[GameState.MyIndex].Dir)
                {
                    case (byte)Core.Enum.DirectionType.Up:
                        {
                            x = GetPlayerX(GameState.MyIndex);
                            y = GetPlayerY(GameState.MyIndex) - 1;
                            break;
                        }

                    case (byte)Core.Enum.DirectionType.Down:
                        {
                            x = GetPlayerX(GameState.MyIndex);
                            y = GetPlayerY(GameState.MyIndex) + 1;
                            break;
                        }

                    case (byte)Core.Enum.DirectionType.Left:
                        {
                            x = GetPlayerX(GameState.MyIndex) - 1;
                            y = GetPlayerY(GameState.MyIndex);
                            break;
                        }
                    case (byte)Core.Enum.DirectionType.Right:
                        {
                            x = GetPlayerX(GameState.MyIndex) + 1;
                            y = GetPlayerY(GameState.MyIndex);
                            break;
                        }

                    case (byte)Core.Enum.DirectionType.UpRight:
                        {
                            x = GetPlayerX(GameState.MyIndex) + 1;
                            y = GetPlayerY(GameState.MyIndex) - 1;
                            break;
                        }

                    case (byte)Core.Enum.DirectionType.UpLeft:
                        {
                            x = GetPlayerX(GameState.MyIndex) - 1;
                            y = GetPlayerY(GameState.MyIndex) - 1;
                            break;
                        }

                    case (byte)Core.Enum.DirectionType.DownRight:
                        {
                            x = GetPlayerX(GameState.MyIndex) + 1;
                            y = GetPlayerY(GameState.MyIndex) + 1;
                            break;
                        }

                    case (byte)Core.Enum.DirectionType.DownLeft:
                        {
                            x = GetPlayerX(GameState.MyIndex) - 1;
                            y = GetPlayerY(GameState.MyIndex) + 1;
                            break;
                        }
                }

                if (General.GetTickCount() > Core.Type.Player[GameState.MyIndex].EventTimer)
                {
                    for (int i = 0, loopTo = GameState.CurrentEvents; i < loopTo; i++)
                    {
                        if (Core.Type.MapEvents.Length < GameState.CurrentEvents)
                            break;

                        if (Core.Type.MapEvents[i].Visible == true)
                        {
                            if (Core.Type.MapEvents[i].X == x & Core.Type.MapEvents[i].Y == y)
                            {
                                buffer = new ByteStream(4);
                                buffer.WriteInt32((int)Packets.ClientPackets.CEvent);
                                buffer.WriteInt32(i);
                                NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
                                buffer.Dispose();
                                Core.Type.Player[GameState.MyIndex].EventTimer = General.GetTickCount() + 200;
                            }
                        }
                    }
                }
            }

        }

        public static void PlayerCastSkill(int skillSlot)
        {
            var buffer = new ByteStream(4);

            // Check for subscript out of range
            if (skillSlot < 0 | skillSlot > Constant.MAX_PLAYER_SKILLS)
                return;

            if (Core.Type.Player[GameState.MyIndex].Skill[skillSlot].CD > 0)
            {
                Text.AddText("Skill has not cooled down yet!", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            if (Core.Type.Player[GameState.MyIndex].Skill[skillSlot].Num < 0)
                return;

            // Check if player has enough MP
            if (GetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.SP) < Core.Type.Skill[(int)Core.Type.Player[GameState.MyIndex].Skill[skillSlot].Num].MpCost)
            {
                Text.AddText("Not enough MP to cast " + Core.Type.Skill[(int)Core.Type.Player[GameState.MyIndex].Skill[skillSlot].Num].Name + ".", (int)Core.Enum.ColorType.BrightRed);
                return;
            }

            if (Core.Type.Player[GameState.MyIndex].Skill[skillSlot].Num >= 0)
            {
                if (General.GetTickCount() > Core.Type.Player[GameState.MyIndex].AttackTimer + 1000)
                {
                    if (Core.Type.Player[GameState.MyIndex].Moving == 0)
                    {
                        if (Core.Type.MyMap.Moral > 0)
                        {
                            if (Core.Type.Moral[Core.Type.MyMap.Moral].CanCast)
                            {
                                NetworkSend.SendCast(skillSlot);
                            }
                            else
                            {
                                Text.AddText("Cannot cast here!", (int)Core.Enum.ColorType.BrightRed);
                            }
                        }
                    }
                    else
                    {
                        Text.AddText("Cannot cast while walking!", (int)Core.Enum.ColorType.BrightRed);
                    }
                }
            }
            else
            {
                Text.AddText("No skill here.", (int)Core.Enum.ColorType.BrightRed);
            }

        }

        public static int FindSkill(int skillNum)
        {
            int FindSkillRet = default;
            int i;

            FindSkillRet = 0;

            // Check for subscript out of range
            if (skillNum < 0 | skillNum > Constant.MAX_SKILLS)
            {
                return FindSkillRet;
            }

            for (i = 0; i < Constant.MAX_PLAYER_SKILLS; i++)
            {
                // Check to see if the player has the skill
                if (GetPlayerSkill(GameState.MyIndex, i) == skillNum)
                {
                    FindSkillRet = i;
                    return FindSkillRet;
                }
            }

            return FindSkillRet;

        }

        #endregion

        #region Outgoing Traffic

        #endregion

        #region Incoming Traffic
        public static void Packet_PlayerHP(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            SetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.HP, buffer.ReadInt32());

            // set max width
            if (GetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.HP) > 0)
            {
                GameState.BarWidth_GuiHP_Max = (long)Math.Round(GetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.HP) / 209d / (GetPlayerMaxVital(GameState.MyIndex, Core.Enum.VitalType.HP) / 209d) * 209d);
            }
            else
            {
                GameState.BarWidth_GuiHP_Max = 0L;
            }

            Gui.UpdateStats_UI();

            buffer.Dispose();
        }

        public static void Packet_PlayerSP(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            SetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.SP, buffer.ReadInt32());

            // set max width
            if (GetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.SP) > 0)
            {
                GameState.BarWidth_GuiSP_Max = (long)Math.Round(GetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.SP) / 209d / (GetPlayerMaxVital(GameState.MyIndex, Core.Enum.VitalType.SP) / 209d) * 209d);
            }
            else
            {
                GameState.BarWidth_GuiSP_Max = 0L;
            }

            Gui.UpdateStats_UI();

            buffer.Dispose();
        }

        public static void Packet_PlayerStats(ref byte[] data)
        {
            int i;
            int index;
            var buffer = new ByteStream(data);

            index = buffer.ReadInt32();

            for (i = 0; i < (int)Core.Enum.StatType.Count; i++)
                SetPlayerStat(index, (Core.Enum.StatType)i, buffer.ReadInt32());

            buffer.Dispose();
        }

        public static void Packet_PlayerData(ref byte[] data)
        {
            int i;
            int x;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();
            SetPlayerName(i, buffer.ReadString());
            SetPlayerJob(i, buffer.ReadInt32());
            SetPlayerLevel(i, buffer.ReadInt32());
            SetPlayerPoints(i, buffer.ReadInt32());
            SetPlayerSprite(i, buffer.ReadInt32());
            SetPlayerMap(i, buffer.ReadInt32());
            SetPlayerAccess(i, buffer.ReadInt32());
            SetPlayerPK(i, buffer.ReadBoolean());

            for (x = 0; x < (int)Core.Enum.StatType.Count; x++)
                SetPlayerStat(i, (Core.Enum.StatType)x, buffer.ReadInt32());

            for (x = 0; x < (int)Core.Enum.ResourceType.Count; x++)
            {
                Core.Type.Player[i].GatherSkills[x].SkillLevel = buffer.ReadInt32();
                Core.Type.Player[i].GatherSkills[x].SkillCurExp = buffer.ReadInt32();
                Core.Type.Player[i].GatherSkills[x].SkillNextLvlExp = buffer.ReadInt32();
            }

            // Check if the player is the client player
            if (i == GameState.MyIndex)
            {
                // Reset directions
                GameState.DirUp = false;
                GameState.DirDown = false;
                GameState.DirLeft = false;
                GameState.DirRight = false;

                // set form
                {
                    var withBlock = Gui.Windows[Gui.GetWindowIndex("winCharacter")];
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblName")].Text = "Name";
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblClass")].Text = "Class";
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblLevel")].Text = "Level";
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblGuild")].Text = "Guild";
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblName2")].Text = GetPlayerName(GameState.MyIndex);
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblClass2")].Text = Core.Type.Job[GetPlayerJob(GameState.MyIndex)].Name;
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblLevel2")].Text = GetPlayerLevel(GameState.MyIndex).ToString();
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblGuild2")].Text = "None";
                    Gui.UpdateStats_UI();

                    // stats
                    for (x = 0; x < (int)Core.Enum.StatType.Count; x++)
                        withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblStat_" + (x + 1))].Text = GetPlayerStat(GameState.MyIndex, (Core.Enum.StatType)x).ToString();

                    // points
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblPoints")].Text = GetPlayerPoints(GameState.MyIndex).ToString();

                    // grey out buttons
                    if (GetPlayerPoints(GameState.MyIndex) == 0)
                    {
                        for (x = 0; x < (int)Core.Enum.StatType.Count; x++)
                            withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "btnGreyStat_" + (x + 1))].Visible = true;
                    }
                    else
                    {
                        for (x = 0; x < (int)Core.Enum.StatType.Count; x++)
                            withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "btnGreyStat_" + (x + 1))].Visible = false;
                    }
                }
                GameState.PlayerData = true;
            }

            buffer.Dispose();
        }

        public static void Packet_PlayerMove(ref byte[] data)
        {
            int i;
            int x;
            int y;
            int dir;
            byte n;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();
            x = buffer.ReadInt32();
            y = buffer.ReadInt32();
            dir = buffer.ReadInt32();
            n = (byte)buffer.ReadInt32();

            SetPlayerX(i, x);
            SetPlayerY(i, y);
            SetPlayerDir(i, dir);
            Core.Type.Player[i].XOffset = 0;
            Core.Type.Player[i].YOffset = 0;
            Core.Type.Player[i].Moving = n;

            switch (GetPlayerDir(i))
            {
                case (int)Core.Enum.DirectionType.Up:
                    {
                        Core.Type.Player[i].YOffset = GameState.PicY;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Down:
                    {
                        Core.Type.Player[i].YOffset = GameState.PicY * -1;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Left:
                    {
                        Core.Type.Player[i].XOffset = GameState.PicX;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Right:
                    {
                        Core.Type.Player[i].XOffset = GameState.PicX * -1;
                        break;
                    }
                case (int)Core.Enum.DirectionType.UpRight:
                    {
                        Core.Type.Player[i].YOffset = GameState.PicY;
                        Core.Type.Player[i].XOffset = GameState.PicX * -1;
                        break;

                    }
                case (int)Core.Enum.DirectionType.UpLeft:
                    {
                        Core.Type.Player[i].YOffset = GameState.PicY;
                        Core.Type.Player[i].XOffset = GameState.PicX;
                        break;
                    }
                case (int)Core.Enum.DirectionType.DownRight:
                    {
                        Core.Type.Player[i].YOffset = GameState.PicY * -1;
                        Core.Type.Player[i].XOffset = GameState.PicX * -1;
                        break;
                    }
                case (int)Core.Enum.DirectionType.DownLeft:
                    {
                        Core.Type.Player[i].YOffset = GameState.PicY * -1;
                        Core.Type.Player[i].XOffset = GameState.PicX;
                        break;
                    }
            }

            buffer.Dispose();
        }

        public static void Packet_PlayerDir(ref byte[] data)
        {
            int dir;
            int i;
            var buffer = new ByteStream(data);
            i = buffer.ReadInt32();
            dir = buffer.ReadInt32();

            SetPlayerDir(i, dir);

            ref var withBlock = ref Core.Type.Player[i];
            withBlock.XOffset = 0;
            withBlock.YOffset = 0;
            withBlock.Moving = 0;

            buffer.Dispose();
        }

        public static void Packet_PlayerExp(ref byte[] data)
        {
            int index;
            int tnl;
            var buffer = new ByteStream(data);

            index = buffer.ReadInt32();
            SetPlayerExp(index, buffer.ReadInt32());

            tnl = buffer.ReadInt32();
            GameState.NextlevelExp = tnl;

            // set max width
            if (GetPlayerLevel(GameState.MyIndex) < Constant.MAX_LEVEL)
            {
                if (GetPlayerExp(GameState.MyIndex) > 0)
                {
                    GameState.BarWidth_GuiEXP_Max = (long)Math.Round(GetPlayerExp(GameState.MyIndex) / 209d / (tnl / 209d) * 209d);
                }
                else
                {
                    GameState.BarWidth_GuiEXP_Max = 0L;
                }
            }
            else
            {
                GameState.BarWidth_GuiEXP_Max = 209L;
            }

            // Update GUI
            Gui.UpdateStats_UI();

            buffer.Dispose();
        }

        public static void Packet_PlayerXY(ref byte[] data)
        {
            int x;
            int y;
            int dir;
            int index;
            var buffer = new ByteStream(data);

            index = buffer.ReadInt32();
            x = buffer.ReadInt32();
            y = buffer.ReadInt32();
            dir = buffer.ReadInt32();

            SetPlayerX(index, x);
            SetPlayerY(index, y);
            SetPlayerDir(index, dir);

            // Make sure they aren't walking
            Core.Type.Player[index].Moving = 0;
            Core.Type.Player[index].XOffset = 0;
            Core.Type.Player[index].YOffset = 0;

            buffer.Dispose();
        }
        #endregion

    }
}