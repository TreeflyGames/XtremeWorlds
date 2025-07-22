using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using System.Net.Security;
using static Core.Global.Command;

namespace Client
{

    public class Player
    {
        #region Database
        public static void ClearPlayers()
        {
            Core.Data.Account = new Core.Type.Account[Constant.MAX_PLAYERS];
            Core.Data.Player = new Core.Type.Player[Constant.MAX_PLAYERS];
            Core.Data.TempPlayer = new Core.Type.TempPlayer[Core.Constant.MAX_PLAYERS];

            for (int i = 0; i < Constant.MAX_PLAYERS; i++)
            {
                ClearPlayer(i);
            }
        }

        public static void ClearAccount(int index)
        {
            Core.Data.Account[index].Login = "";
            Core.Data.Account[index].Password = "";
        }

        public static void ClearPlayer(int index)
        {
            ClearAccount(index);

            Core.Data.Player[index].Name = "";
            Core.Data.Player[index].Attacking = 0;
            Core.Data.Player[index].AttackTimer = 0;
            Core.Data.Player[index].Job = 0;
            Core.Data.Player[index].Dir = 0;
            Core.Data.Player[index].Access = (byte)AccessLevel.Player;

            Core.Data.Player[index].Equipment = new int[Enum.GetValues(typeof(Equipment)).Length];
            for (int y = 0; y < Core.Data.Player[index].Equipment.Length; y++)
                Core.Data.Player[index].Equipment[y] = -1;

            Core.Data.Player[index].Exp = 0;
            Core.Data.Player[index].Level = 0;
            Core.Data.Player[index].Map = 0;
            Core.Data.Player[index].MapGetTimer = 0;
            Core.Data.Player[index].Moving = 0;
            Core.Data.Player[index].PK = false;
            Core.Data.Player[index].Points = 0;
            Core.Data.Player[index].Sprite = 0;

            Core.Data.Player[index].Inv = new Core.Type.PlayerInv[Constant.MAX_INV];
            for (int x = 0; x < Constant.MAX_INV; x++)
            {
                Core.Data.Player[index].Inv[x].Num = -1;
                Core.Data.Player[index].Inv[x].Value = 0;
                Data.TradeTheirOffer[x].Num = -1;
                Data.TradeYourOffer[x].Num = -1;
            }

            Core.Data.Player[index].Skill = new Core.Type.PlayerSkill[Constant.MAX_PLAYER_SKILLS];
            for (int x = 0; x < Constant.MAX_PLAYER_SKILLS; x++)
            {
                Core.Data.Player[index].Skill[x].Num = -1;
                Core.Data.Player[index].Skill[x].CD = 0;
            }

            Core.Data.Player[index].Stat = new byte[Enum.GetValues(typeof(Core.Stat)).Length];
            foreach (Core.Stat stat in Enum.GetValues(typeof(Core.Stat)))
                Core.Data.Player[index].Stat[(int)stat] = 0;

            Core.Data.Player[index].Steps = 0;

            int vitalCount = Enum.GetValues(typeof(Core.Vital)).Length;
            Core.Data.Player[index].Vital = new int[vitalCount];
            foreach (Core.Vital vital in Enum.GetValues(typeof(Core.Vital)))
                Core.Data.Player[index].Vital[(int)vital] = 0;

            Core.Data.Player[index].X = 0;
            Core.Data.Player[index].XOffset = 0;
            Core.Data.Player[index].Y = 0;
            Core.Data.Player[index].YOffset = 0;

            Core.Data.Player[index].Hotbar = new Core.Type.Hotbar[Constant.MAX_HOTBAR];
            Core.Data.Player[index].GatherSkills = new Core.Type.ResourceType[Enum.GetValues(typeof(Core.ResourceSkill)).Length];

            Trade.InTrade = -1;
        }
        #endregion

        #region Movement
        public static void CheckMovement()
        {
            if (IsTryingToMove() && CanMove())
            {
                // Check if player has the shift key down for running
                if (GameState.VbKeyShift)
                {
                    Core.Data.Player[GameState.MyIndex].Moving = (byte)MovementState.Walking;
                }
                else
                {
                    Core.Data.Player[GameState.MyIndex].Moving = (byte)MovementState.Running;
                }

                NetworkSend.SendPlayerMove();

                if (Core.Data.Player[GameState.MyIndex].XOffset == 0 & Core.Data.Player[GameState.MyIndex].YOffset == 0)
                {
                    if (Data.MyMap.Tile[GetPlayerX(GameState.MyIndex), GetPlayerY(GameState.MyIndex)].Type == TileType.Warp | Data.MyMap.Tile[GetPlayerX(GameState.MyIndex), GetPlayerY(GameState.MyIndex)].Type2 == TileType.Warp)
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
                case (int)Direction.Up:
                    {

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && GetPlayerY(GameState.MyIndex) <= 0)
                        {
                            GameState.DirUp = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Down);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Direction.Down:
                    {

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Down == 0 && GetPlayerY(GameState.MyIndex) >= Data.MyMap.MaxY)
                        {
                            GameState.DirDown = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Up);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Direction.Left:
                    {

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Left == 0 && GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirLeft = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Right);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Direction.Right:
                    {

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) >= Data.MyMap.MaxX)
                        {
                            GameState.DirRight = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Left);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Direction.UpLeft:
                    {

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && Data.Map[GetPlayerMap(GameState.MyIndex)].Left == 0 && GetPlayerY(GameState.MyIndex) <= 0 & GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirUp = false;
                            GameState.DirDown = true;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Down);
                            GameState.DirLeft = false;
                            GameState.DirRight = true;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Right);
                            return CanMoveRet;
                        }

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Down == 0 && GetPlayerY(GameState.MyIndex) <= 0)
                        {
                            GameState.DirUp = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Down);
                            return CanMoveRet;
                        }

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirLeft = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Right);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Direction.UpRight:
                    {

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && Data.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerY(GameState.MyIndex) >= Data.MyMap.MaxY & GetPlayerX(GameState.MyIndex) >= Data.MyMap.MaxX)
                        {
                            GameState.DirUp = false;
                            GameState.DirDown = true;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Down);
                            GameState.DirRight = false;
                            GameState.DirLeft = true;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Left);
                            return CanMoveRet;
                        }

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && GetPlayerY(GameState.MyIndex) <= 0)
                        {
                            GameState.DirUp = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Down);
                            return CanMoveRet;
                        }

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirLeft = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Right);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Direction.DownLeft:
                    {

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && Data.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerY(GameState.MyIndex) >= Data.MyMap.MaxY & GetPlayerX(GameState.MyIndex) < 0)
                        {
                            GameState.DirDown = false;
                            GameState.DirUp = true;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Up);
                            GameState.DirLeft = false;
                            GameState.DirRight = true;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Right);
                            return CanMoveRet;
                        }

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Up == 0 && GetPlayerY(GameState.MyIndex) <= 0)
                        {
                            GameState.DirDown = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Up);
                            return CanMoveRet;
                        }

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) <= 0)
                        {
                            GameState.DirLeft = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Right);
                            return CanMoveRet;
                        }

                        break;
                    }

                case (int)Direction.DownRight:
                    {

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Down == 0 && Data.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerY(GameState.MyIndex) >= Data.MyMap.MaxY & GetPlayerX(GameState.MyIndex) >= Data.MyMap.MaxX)
                        {
                            GameState.DirDown = false;
                            GameState.DirUp = true;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Up);
                            GameState.DirRight = false;
                            GameState.DirLeft = true;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Left);
                            return CanMoveRet;
                        }

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Down == 0 && GetPlayerY(GameState.MyIndex) >= Data.MyMap.MaxY)
                        {
                            GameState.DirDown = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Up);
                            return CanMoveRet;
                        }

                        if (Data.Map[GetPlayerMap(GameState.MyIndex)].Right == 0 && GetPlayerX(GameState.MyIndex) >= Data.MyMap.MaxX)
                        {
                            GameState.DirRight = false;
                            SetPlayerDir(GameState.MyIndex, (int)Direction.Left);
                            return CanMoveRet;
                        }

                        break;
                    }

            }

            // Check for cardinal movements if no diagonal movements
            if (GameState.DirUp)
            {
                SetPlayerDir(GameState.MyIndex, (int)Direction.Up);
                if (GetPlayerY(GameState.MyIndex) > 0)
                {
                    if (CheckDirection((byte)Direction.Up))
                    {
                        CanMoveRet = false;
                        if (d != (int)Direction.Up)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Data.MyMap.Up > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }

            if (GameState.DirDown)
            {
                SetPlayerDir(GameState.MyIndex, (int)Direction.Down);
                if (GetPlayerY(GameState.MyIndex) < Data.MyMap.MaxY - 1)
                {
                    if (CheckDirection((byte)Direction.Down))
                    {
                        CanMoveRet = false;
                        if (d != (int)Direction.Down)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Data.MyMap.Down > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }

            if (GameState.DirLeft)
            {
                SetPlayerDir(GameState.MyIndex, (int)Direction.Left);
                if (GetPlayerX(GameState.MyIndex) > 0)
                {
                    if (CheckDirection((byte)Direction.Left))
                    {
                        CanMoveRet = false;
                        if (d != (int)Direction.Left)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Data.MyMap.Left > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }

            if (GameState.DirRight)
            {
                SetPlayerDir(GameState.MyIndex, (int)Direction.Right);
                if (GetPlayerX(GameState.MyIndex) < Data.MyMap.MaxX)
                {
                    if (CheckDirection((byte)Direction.Right))
                    {
                        CanMoveRet = false;
                        if (d != (int)Direction.Right)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Data.MyMap.Right > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }

            // Check for diagonal movements first
            if (GameState.DirUp & GameState.DirRight)
            {
                SetPlayerDir(GameState.MyIndex, (int)Direction.UpRight);
                if (GetPlayerY(GameState.MyIndex) > 0 & GetPlayerX(GameState.MyIndex) < Data.MyMap.MaxX)
                {
                    if (CheckDirection((byte)Direction.UpRight))
                    {
                        CanMoveRet = false;
                        if (d != (int)Direction.UpRight)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Data.MyMap.Up > 0 & Data.MyMap.Right > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }
            else if (GameState.DirUp & GameState.DirLeft)
            {
                SetPlayerDir(GameState.MyIndex, (int)Direction.UpLeft);
                if (GetPlayerY(GameState.MyIndex) > 0 & GetPlayerX(GameState.MyIndex) > 0)
                {
                    if (CheckDirection((byte)Direction.UpLeft))
                    {
                        CanMoveRet = false;
                        if (d != (int)Direction.UpLeft)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Data.MyMap.Up > 0 & Data.MyMap.Left > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }
            else if (GameState.DirDown & GameState.DirRight)
            {
                SetPlayerDir(GameState.MyIndex, (int)Direction.DownRight);
                if (GetPlayerY(GameState.MyIndex) < Data.MyMap.MaxY & GetPlayerX(GameState.MyIndex) < Data.MyMap.MaxX)
                {
                    if (CheckDirection((byte)Direction.DownRight))
                    {
                        CanMoveRet = false;
                        if (d != (int)Direction.DownRight)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Data.MyMap.Down > 0 & Data.MyMap.Right > 0)
                {
                    Map.SendPlayerRequestNewMap();
                    CanMoveRet = false;
                    return CanMoveRet;
                }
            }
            else if (GameState.DirDown & GameState.DirLeft)
            {
                SetPlayerDir(GameState.MyIndex, (int)Direction.DownLeft);
                if (GetPlayerY(GameState.MyIndex) < Data.MyMap.MaxY & GetPlayerX(GameState.MyIndex) > 0)
                {
                    if (CheckDirection((byte)Direction.DownLeft))
                    {
                        CanMoveRet = false;
                        if (d != (int)Direction.DownLeft)
                        {
                            NetworkSend.SendPlayerDir();
                        }
                        return CanMoveRet;
                    }
                }
                else if (Data.MyMap.Down > 0 & Data.MyMap.Left > 0)
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

            if (GetPlayerX(GameState.MyIndex) >= Data.Map[GetPlayerMap(GameState.MyIndex)].MaxX || GetPlayerY(GameState.MyIndex) >= Data.Map[GetPlayerMap(GameState.MyIndex)].MaxY)
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            // check directional blocking
            if (GameLogic.IsDirBlocked(ref Data.MyMap.Tile[GetPlayerX(GameState.MyIndex), GetPlayerY(GameState.MyIndex)].DirBlock, ref direction))
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            switch (direction)
            {
                case (byte)Direction.Up:
                    {
                        x = GetPlayerX(GameState.MyIndex);
                        y = GetPlayerY(GameState.MyIndex) - 1;
                        break;
                    }
                case (byte)Direction.Down:
                    {
                        x = GetPlayerX(GameState.MyIndex);
                        y = GetPlayerY(GameState.MyIndex) + 1;
                        break;
                    }
                case (byte)Direction.Left:
                    {
                        x = GetPlayerX(GameState.MyIndex) - 1;
                        y = GetPlayerY(GameState.MyIndex);
                        break;
                    }
                case (byte)Direction.Right:
                    {
                        x = GetPlayerX(GameState.MyIndex) + 1;
                        y = GetPlayerY(GameState.MyIndex);
                        break;
                    }
                case (byte)Direction.UpLeft:
                    {
                        x = GetPlayerX(GameState.MyIndex) - 1;
                        y = GetPlayerY(GameState.MyIndex) - 1;
                        break;
                    }
                case (byte)Direction.UpRight:
                    {
                        x = GetPlayerX(GameState.MyIndex) + 1;
                        y = GetPlayerY(GameState.MyIndex) - 1;
                        break;
                    }
                case (byte)Direction.DownLeft:
                    {
                        x = GetPlayerX(GameState.MyIndex) - 1;
                        y = GetPlayerY(GameState.MyIndex) + 1;
                        break;
                    }
                case (byte)Direction.DownRight:
                    {
                        x = GetPlayerX(GameState.MyIndex) + 1;
                        y = GetPlayerY(GameState.MyIndex) + 1;
                        break;
                    }
            }

            if (x < 0 || y < 0 || x >= Data.MyMap.MaxX || y >= Data.MyMap.MaxY)
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            // Check to see if the map tile is blocked or not
            if (Data.MyMap.Tile[x, y].Type == TileType.Blocked | Data.MyMap.Tile[x, y].Type2 == TileType.Blocked)
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            // Check to see if the map tile is tree or not
            if (Data.MyMap.Tile[x, y].Type == TileType.Resource | Data.MyMap.Tile[x, y].Type2 == TileType.Resource)
            {
                CheckDirectionRet = true;
                return CheckDirectionRet;
            }

            // Check to see if a player is already on that tile
            if (Data.MyMap.Moral > 0)
            {
                if (Data.Moral[Data.MyMap.Moral].PlayerBlock)
                {
                    for (i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i))
                        {
                            if (Core.Data.Player[i].X == x & Core.Data.Player[i].Y == y)
                            {
                                CheckDirectionRet = true;
                                return CheckDirectionRet;
                            }
                        }
                    }
                }

                // Check to see if a Npc is already on that tile
                if (Data.Moral[Data.MyMap.Moral].NpcBlock)
                {
                    for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                    {
                        if (Data.MyMapNpc[i].Num >= 0 & Data.MyMapNpc[i].X == x & Data.MyMapNpc[i].Y == y)
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
                if (Data.MapEvents[i].Visible == true)
                {
                    if (Data.MapEvents[i].X == x & Data.MapEvents[i].Y == y)
                    {
                        if (Data.MapEvents[i].WalkThrough == 0)
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
            switch (Core.Data.Player[index].Moving)
            {
                case (byte)MovementState.Walking:
                    {
                        GameState.MovementSpeed = GameState.ElapsedTime / 1000.0d * Core.Constant.WALK_SPEED * GameState.SizeX; // Adjust speed by elapsed time
                        break;
                    }
                case (byte)MovementState.Running:
                    {
                        GameState.MovementSpeed = GameState.ElapsedTime / 1000.0d * Core.Constant.RUN_SPEED * GameState.SizeX; // Adjust speed by elapsed time
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
                case (int)Direction.Up:
                    {
                        Core.Data.Player[index].YOffset = (int)Math.Round(Core.Data.Player[index].YOffset - GameState.MovementSpeed);
                        if (Core.Data.Player[index].YOffset < 0)
                            Core.Data.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Direction.Down:
                    {
                        Core.Data.Player[index].YOffset = (int)Math.Round(Core.Data.Player[index].YOffset + GameState.MovementSpeed);
                        if (Core.Data.Player[index].YOffset > 0)
                            Core.Data.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        Core.Data.Player[index].XOffset = (int)Math.Round(Core.Data.Player[index].XOffset - GameState.MovementSpeed);
                        if (Core.Data.Player[index].XOffset < 0)
                            Core.Data.Player[index].XOffset = 0;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        Core.Data.Player[index].XOffset = (int)Math.Round(Core.Data.Player[index].XOffset + GameState.MovementSpeed);
                        if (Core.Data.Player[index].XOffset > 0)
                            Core.Data.Player[index].XOffset = 0;
                        break;
                    }
                case (int)Direction.UpRight:
                    {
                        Core.Data.Player[index].XOffset = (int)Math.Round(Core.Data.Player[index].XOffset + GameState.MovementSpeed);
                        Core.Data.Player[index].YOffset = (int)Math.Round(Core.Data.Player[index].YOffset - GameState.MovementSpeed);
                        if (Core.Data.Player[index].XOffset > 0)
                            Core.Data.Player[index].XOffset = 0;
                        if (Core.Data.Player[index].YOffset < 0)
                            Core.Data.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Direction.UpLeft:
                    {
                        Core.Data.Player[index].XOffset = (int)Math.Round(Core.Data.Player[index].XOffset - GameState.MovementSpeed);
                        Core.Data.Player[index].YOffset = (int)Math.Round(Core.Data.Player[index].YOffset - GameState.MovementSpeed);
                        if (Core.Data.Player[index].XOffset < 0)
                            Core.Data.Player[index].XOffset = 0;
                        if (Core.Data.Player[index].YOffset < 0)
                            Core.Data.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Direction.DownRight:
                    {
                        Core.Data.Player[index].XOffset = (int)Math.Round(Core.Data.Player[index].XOffset + GameState.MovementSpeed);
                        Core.Data.Player[index].YOffset = (int)Math.Round(Core.Data.Player[index].YOffset + GameState.MovementSpeed);
                        if (Core.Data.Player[index].XOffset > 0)
                            Core.Data.Player[index].XOffset = 0;
                        if (Core.Data.Player[index].YOffset > 0)
                            Core.Data.Player[index].YOffset = 0;
                        break;
                    }
                case (int)Direction.DownLeft:
                    {
                        Core.Data.Player[index].XOffset = (int)Math.Round(Core.Data.Player[index].XOffset - GameState.MovementSpeed);
                        Core.Data.Player[index].YOffset = (int)Math.Round(Core.Data.Player[index].YOffset + GameState.MovementSpeed);
                        if (Core.Data.Player[index].XOffset < 0)
                            Core.Data.Player[index].XOffset = 0;
                        if (Core.Data.Player[index].YOffset > 0)
                            Core.Data.Player[index].YOffset = 0;
                        break;
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
                if (GetPlayerEquipment(GameState.MyIndex, Equipment.Weapon) >= 0)
                {
                    attackSpeed = Core.Data.Item[GetPlayerEquipment(GameState.MyIndex, Equipment.Weapon)].Speed * 1000;
                }
                else
                {
                    attackSpeed = 1000;
                }

                if (Core.Data.Player[GameState.MyIndex].AttackTimer + attackSpeed < General.GetTickCount())
                {
                    if (Core.Data.Player[GameState.MyIndex].Attacking == 0)
                    {
                        {
                            ref var withBlock = ref Core.Data.Player[GameState.MyIndex];
                            withBlock.Attacking = 1;
                            withBlock.AttackTimer = General.GetTickCount();
                        }

                        NetworkSend.SendAttack();
                    }
                }

                switch (Core.Data.Player[GameState.MyIndex].Dir)
                {
                    case (byte)Direction.Up:
                        {
                            x = GetPlayerX(GameState.MyIndex);
                            y = GetPlayerY(GameState.MyIndex) - 1;
                            break;
                        }

                    case (byte)Direction.Down:
                        {
                            x = GetPlayerX(GameState.MyIndex);
                            y = GetPlayerY(GameState.MyIndex) + 1;
                            break;
                        }

                    case (byte)Direction.Left:
                        {
                            x = GetPlayerX(GameState.MyIndex) - 1;
                            y = GetPlayerY(GameState.MyIndex);
                            break;
                        }
                    case (byte)Direction.Right:
                        {
                            x = GetPlayerX(GameState.MyIndex) + 1;
                            y = GetPlayerY(GameState.MyIndex);
                            break;
                        }

                    case (byte)Direction.UpRight:
                        {
                            x = GetPlayerX(GameState.MyIndex) + 1;
                            y = GetPlayerY(GameState.MyIndex) - 1;
                            break;
                        }

                    case (byte)Direction.UpLeft:
                        {
                            x = GetPlayerX(GameState.MyIndex) - 1;
                            y = GetPlayerY(GameState.MyIndex) - 1;
                            break;
                        }

                    case (byte)Direction.DownRight:
                        {
                            x = GetPlayerX(GameState.MyIndex) + 1;
                            y = GetPlayerY(GameState.MyIndex) + 1;
                            break;
                        }

                    case (byte)Direction.DownLeft:
                        {
                            x = GetPlayerX(GameState.MyIndex) - 1;
                            y = GetPlayerY(GameState.MyIndex) + 1;
                            break;
                        }
                }

                if (General.GetTickCount() > Core.Data.Player[GameState.MyIndex].EventTimer)
                {
                    for (int i = 0, loopTo = GameState.CurrentEvents; i < loopTo; i++)
                    {
                        if (Data.MapEvents.Length < GameState.CurrentEvents)
                            break;

                        if (Data.MapEvents[i].Visible == true)
                        {
                            if (Data.MapEvents[i].X == x & Data.MapEvents[i].Y == y)
                            {
                                buffer = new ByteStream(4);
                                buffer.WriteInt32((int)Packets.ClientPackets.CEvent);
                                buffer.WriteInt32(i);
                                NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
                                buffer.Dispose();
                                Core.Data.Player[GameState.MyIndex].EventTimer = General.GetTickCount() + 200;
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

            if (Core.Data.Player[GameState.MyIndex].Skill[skillSlot].CD > 0)
            {
                Text.AddText("Skill has not cooled down yet!", (int)Core.Color.BrightRed);
                return;
            }

            if (Core.Data.Player[GameState.MyIndex].Skill[skillSlot].Num < 0)
                return;

            // Check if player has enough MP
            if (GetPlayerVital(GameState.MyIndex, Core.Vital.Stamina) < Data.Skill[(int)Core.Data.Player[GameState.MyIndex].Skill[skillSlot].Num].MpCost)
            {
                Text.AddText("Not enough MP to cast " + Data.Skill[(int)Core.Data.Player[GameState.MyIndex].Skill[skillSlot].Num].Name + ".", (int)Core.Color.BrightRed);
                return;
            }

            if (Core.Data.Player[GameState.MyIndex].Skill[skillSlot].Num >= 0)
            {
                if (General.GetTickCount() > Core.Data.Player[GameState.MyIndex].AttackTimer + 1000)
                {
                    if (Core.Data.Player[GameState.MyIndex].Moving == 0)
                    {
                        if (Data.MyMap.Moral > 0)
                        {
                            if (Data.Moral[Data.MyMap.Moral].CanCast)
                            {
                                NetworkSend.SendCast(skillSlot);
                            }
                            else
                            {
                                Text.AddText("Cannot cast here!", (int)Core.Color.BrightRed);
                            }
                        }
                    }
                    else
                    {
                        Text.AddText("Cannot cast while walking!", (int)Core.Color.BrightRed);
                    }
                }
            }
            else
            {
                Text.AddText("No skill here.", (int)Core.Color.BrightRed);
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

            SetPlayerVital(GameState.MyIndex, Core.Vital.Health, buffer.ReadInt32());

            // set max width
            if (GetPlayerVital(GameState.MyIndex, Core.Vital.Health) > 0)
            {
                GameState.BarWidth_GuiHP_Max = (long)Math.Round(GetPlayerVital(GameState.MyIndex, Core.Vital.Health) / 209d / (GetPlayerMaxVital(GameState.MyIndex, Core.Vital.Health) / 209d) * 209d);
            }
            else
            {
                GameState.BarWidth_GuiHP_Max = 0L;
            }

            Gui.UpdateStats_UI();

            buffer.Dispose();
        }

        public static void Packet_PlayerMP(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            SetPlayerVital(GameState.MyIndex, Core.Vital.Mana, buffer.ReadInt32());

            // set max width
            if (GetPlayerVital(GameState.MyIndex, Core.Vital.Health) > 0)
            {
                //GameState.BarWidth_GuiHP_Max = (long)Math.Round(GetPlayerVital(GameState.MyIndex, Core.Vital.Health) / 209d / (GetPlayerMaxVital(GameState.MyIndex, Core.Vital.Health) / 209d) * 209d);
            }
            else
            {
                //GameState.BarWidth_GuiHP_Max = 0L;
            }

            Gui.UpdateStats_UI();

            buffer.Dispose();
        }

        public static void Packet_PlayerSP(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            SetPlayerVital(GameState.MyIndex, Core.Vital.Stamina, buffer.ReadInt32());

            // set max width
            if (GetPlayerVital(GameState.MyIndex, Core.Vital.Stamina) > 0)
            {
                GameState.BarWidth_GuiSP_Max = (long)Math.Round(GetPlayerVital(GameState.MyIndex, Core.Vital.Stamina) / 209d / (GetPlayerMaxVital(GameState.MyIndex, Core.Vital.Stamina) / 209d) * 209d);
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

            int statCount = Enum.GetValues(typeof(Core.Stat)).Length;
            for (i = 0; i < statCount; i++)
                SetPlayerStat(index, (Core.Stat)i, buffer.ReadInt32());

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
            Core.Data.Player[i].Moving = 0;

            int statCount = Enum.GetValues(typeof(Core.Stat)).Length;
            for (x = 0; x < statCount; x++)
                SetPlayerStat(i, (Core.Stat)x, buffer.ReadInt32());

            int resourceSkillCount = Enum.GetValues(typeof(Core.ResourceSkill)).Length;
            for (x = 0; x < resourceSkillCount; x++)
            {
                Core.Data.Player[i].GatherSkills[x].SkillLevel = buffer.ReadInt32();
                Core.Data.Player[i].GatherSkills[x].SkillCurExp = buffer.ReadInt32();
                Core.Data.Player[i].GatherSkills[x].SkillNextLvlExp = buffer.ReadInt32();
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
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblJob")].Text = "Job";
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblLevel")].Text = "Level";
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblGuild")].Text = "Guild";
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblName2")].Text = GetPlayerName(GameState.MyIndex);
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblJob2")].Text = Data.Job[GetPlayerJob(GameState.MyIndex)].Name;
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblLevel2")].Text = GetPlayerLevel(GameState.MyIndex).ToString();
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblGuild2")].Text = "None";
                    Gui.UpdateStats_UI();

                    // stats
                    for (x = 0; x < statCount; x++)
                        withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblStat_" + (x + 1))].Text = GetPlayerStat(GameState.MyIndex, (Core.Stat)x).ToString();

                    // points
                    withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "lblPoints")].Text = GetPlayerPoints(GameState.MyIndex).ToString();

                    // grey out buttons
                    if (GetPlayerPoints(GameState.MyIndex) == 0)
                    {
                        for (x = 0; x < statCount; x++)
                            withBlock.Controls[(int)Gui.GetControlIndex("winCharacter", "btnGreyStat_" + (x + 1))].Visible = true;
                    }
                    else
                    {
                        for (x = 0; x < statCount; x++)
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
            Core.Data.Player[i].XOffset = 0;
            Core.Data.Player[i].YOffset = 0;
            Core.Data.Player[i].Moving = n;

            switch (GetPlayerDir(i))
            {
                case (int)Direction.Up:
                    {
                        Core.Data.Player[i].YOffset = GameState.PicY;
                        break;
                    }
                case (int)Direction.Down:
                    {
                        Core.Data.Player[i].YOffset = GameState.PicY * -1;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        Core.Data.Player[i].XOffset = GameState.PicX;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        Core.Data.Player[i].XOffset = GameState.PicX * -1;
                        break;
                    }
                case (int)Direction.UpRight:
                    {
                        Core.Data.Player[i].YOffset = GameState.PicY;
                        Core.Data.Player[i].XOffset = GameState.PicX * -1;
                        break;

                    }
                case (int)Direction.UpLeft:
                    {
                        Core.Data.Player[i].YOffset = GameState.PicY;
                        Core.Data.Player[i].XOffset = GameState.PicX;
                        break;
                    }
                case (int)Direction.DownRight:
                    {
                        Core.Data.Player[i].YOffset = GameState.PicY * -1;
                        Core.Data.Player[i].XOffset = GameState.PicX * -1;
                        break;
                    }
                case (int)Direction.DownLeft:
                    {
                        Core.Data.Player[i].YOffset = GameState.PicY * -1;
                        Core.Data.Player[i].XOffset = GameState.PicX;
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
            dir = buffer.ReadByte();

            SetPlayerDir(i, dir);

            ref var withBlock = ref Core.Data.Player[i];
            withBlock.XOffset = 0;
            withBlock.YOffset = 0;
            withBlock.Moving = 0;

            buffer.Dispose();
        }

        public static void Packet_PlayerXYOffset(ref byte[] data)
        {
            int dir;
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();
            dir = buffer.ReadByte();

            SetPlayerDir(i, dir);

            ref var withBlock = ref Core.Data.Player[i];
            withBlock.XOffset = buffer.ReadInt32();
            withBlock.YOffset = buffer.ReadInt32();
            withBlock.Moving = buffer.ReadByte();

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
            return;
            var buffer = new ByteStream(data);

            index = buffer.ReadInt32();
            x = buffer.ReadInt32();
            y = buffer.ReadInt32();
            dir = buffer.ReadByte();

            SetPlayerX(index, x);
            SetPlayerY(index, y);
            SetPlayerDir(index, dir);

            // Make sure they aren't walking
            Core.Data.Player[index].Moving = 0;
            Core.Data.Player[index].XOffset = 0;
            Core.Data.Player[index].YOffset = 0;

            buffer.Dispose();
        }
        #endregion

    }
}