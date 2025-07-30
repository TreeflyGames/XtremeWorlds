using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Mirage.Sharp.Asfw.Network;
using Newtonsoft.Json.Linq;
using Npgsql.Replication.PgOutput.Messages;
using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;
using static Core.Global.Command;
using static Core.Packets;
using static Core.Type;

namespace Server
{

    public class NetworkReceive
    {
        public static void PacketRouter()
        {
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CCheckPing] = Packet_Ping;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CLogin] = Packet_Login;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRegister] = Packet_Register;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CAddChar] = Packet_AddChar;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CUseChar] = Packet_UseChar;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CDelChar] = Packet_DelChar;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSayMsg] = Packet_SayMessage;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CBroadcastMsg] = Packet_BroadCastMsg;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CPlayerMsg] = Packet_PlayerMsg;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CAdminMsg] = Packet_AdminMsg;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CPlayerMove] = Packet_PlayerMove;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CStopPlayerMove] = Packet_StopPlayerMove;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CPlayerDir] = Packet_PlayerDirection;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CUseItem] = Packet_UseItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CAttack] = Packet_Attack;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CPlayerInfoRequest] = Packet_PlayerInfo;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CWarpMeTo] = Packet_WarpMeTo;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CWarpToMe] = Packet_WarpToMe;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CWarpTo] = Packet_WarpTo;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSetSprite] = Packet_SetSprite;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CGetStats] = Packet_GetStats;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestNewMap] = Packet_RequestNewMap;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveMap] = Packet_MapData;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CNeedMap] = Packet_NeedMap;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CMapGetItem] = Item.Packet_GetItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CMapDropItem] = Item.Packet_DropItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CMapRespawn] = Packet_RespawnMap;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CMapReport] = Packet_MapReport;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CKickPlayer] = Packet_KickPlayer;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CBanList] = Packet_Banlist;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CBanDestroy] = Packet_DestroyBans;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CBanPlayer] = Packet_BanPlayer;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditMap] = Packet_RequestEditMap;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSetAccess] = Packet_SetAccess;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CWhosOnline] = Packet_WhosOnline;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSetMotd] = Packet_SetMotd;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSearch] = Packet_PlayerSearch;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSkills] = Packet_Skills;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CCast] = Packet_Cast;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSwapInvSlots] = Packet_SwapInvSlots;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSwapSkillSlots] = Packet_SwapSkillSlots;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CCheckPing] = Packet_CheckPing;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CUnequip] = Packet_Unequip;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestPlayerData] = Packet_RequestPlayerData;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestItem] = Item.Packet_RequestItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestNpc] = Packet_RequestNpc;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestResource] = Resource.Packet_RequestResource;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSpawnItem] = Packet_SpawnItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CTrainStat] = Packet_TrainStat;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestAnimation] = Animation.Packet_RequestAnimation;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestSkill] = Packet_RequestSkill;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestShop] = Packet_RequestShop;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestLevelUp] = Packet_RequestLevelUp;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CForgetSkill] = Packet_ForgetSkill;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CCloseShop] = Packet_CloseShop;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CBuyItem] = Packet_BuyItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSellItem] = Packet_SellItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CChangeBankSlots] = Packet_ChangeBankSlots;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CDepositItem] = Packet_DepositItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CWithdrawItem] = Packet_WithdrawItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CCloseBank] = Packet_CloseBank;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CAdminWarp] = Packet_AdminWarp;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CTradeInvite] = Packet_TradeInvite;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CHandleTradeInvite] = Packet_HandleTradeInvite;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CAcceptTrade] = Packet_AcceptTrade;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CDeclineTrade] = Packet_DeclineTrade;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CTradeItem] = Packet_TradeItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CUntradeItem] = Packet_UntradeItem;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CAdmin] = Packet_Admin;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSetHotbarSlot] = Packet_SetHotbarSlot;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CDeleteHotbarSlot] = Packet_DeleteHotbarSlot;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CUseHotbarSlot] = Packet_UseHotbarSlot;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSkillLearn] = Packet_SkillLearn;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CEventChatReply] = Event.Packet_EventChatReply;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CEvent] = Event.Packet_Event;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestSwitchesAndVariables] = Event.Packet_RequestSwitchesAndVariables;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSwitchesAndVariables] = Event.Packet_SwitchesAndVariables;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestProjectile] = Projectile.HandleRequestProjectile;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CClearProjectile] = Projectile.HandleClearProjectile;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CEmote] = Packet_Emote;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestParty] = Party.Packet_PartyRquest;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CAcceptParty] = Party.Packet_AcceptParty;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CDeclineParty] = Party.Packet_DeclineParty;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CLeaveParty] = Party.Packet_LeaveParty;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CPartyChatMsg] = Party.Packet_PartyChatMsg;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditItem] = Item.Packet_RequestEditItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveItem] = Item.Packet_SaveItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditNpc] = Npc.Packet_RequestEditNpc;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveNpc] = Npc.Packet_SaveNpc;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditShop] = Packet_RequestEditShop;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveShop] = Packet_SaveShop;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditSkill] = Packet_RequestEditSkill;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveSkill] = Packet_SaveSkill;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditResource] = Resource.Packet_RequestEditResource;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveResource] = Resource.Packet_SaveResource;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditAnimation] = Animation.Packet_RequestEditAnimation;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveAnimation] = Animation.Packet_SaveAnimation;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditProjectile] = Projectile.HandleRequestEditProjectile;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveProjectile] = Projectile.HandleSaveProjectile;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditJob] = Packet_RequestEditJob;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveJob] = Packet_SaveJob;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestMoral] = Moral.Packet_RequestMoral;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditMoral] = Moral.Packet_RequestEditMoral;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveMoral] = Moral.Packet_SaveMoral;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditScript] = Script.Packet_RequestEditScript;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveScript] = Script.Packet_SaveScript;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CCloseEditor] = Packet_CloseEditor;

        }

        private static void Packet_Ping(int index, ref byte[] data)
        {
            Core.Data.TempPlayer[index].DataPackets = Core.Data.TempPlayer[index].DataPackets + 1;
        }

        private static void Packet_Login(int index, ref byte[] data)
        {
            string username;
            string IP;
            string password;
            int i;
            var buffer = new ByteStream(data);

            if (!NetworkConfig.IsPlaying(index))
            {
                if (!NetworkConfig.IsLoggedIn(index))
                {
                    // check if its banned
                    // Cut off last portion of ip
                    IP = NetworkConfig.Socket.ClientIP(index);

                    if (General.GetShutDownTimer != null && General.GetShutDownTimer.IsRunning)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.ServerMaintenance, (byte)Menu.Login);
                        return;
                    }

                    // Get the data
                    // Use the same pattern as Packet_Register for AES lookup
                    Reoria.Engine.Common.Security.Encryption.AesEncryption aes;
                    General.Aes.TryGetValue(index, out aes);
                    byte[] usernameBytes = buffer.ReadBytes().ToArray();
                    username = System.Text.Encoding.UTF8.GetString(aes.Decrypt(usernameBytes)).ToLower().Replace("\0", "");

                    byte[] passwordBytes = buffer.ReadBytes().ToArray();
                    password = System.Text.Encoding.UTF8.GetString(aes.Decrypt(passwordBytes)).Replace("\0", "");

                    // Get the current executing assembly
                    var assembly = Assembly.GetExecutingAssembly();

                    // Retrieve the version information
                    byte[] clientVersionBytes = buffer.ReadBytes().ToArray();
                    var serverVersion = assembly.GetName().Version.ToString();
                    var clientVersion = System.Text.Encoding.UTF8.GetString(aes.Decrypt(clientVersionBytes));

                    // Check versions
                    if (clientVersion != serverVersion)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.ClientOutdated, (byte)Menu.Login);
                        return;
                    }

                    if (username.Length > Core.Constant.NAME_LENGTH | username.Length < Core.Constant.MIN_NAME_LENGTH)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.NameLengthInvalid);
                        return;
                    }

                    if (NetworkConfig.IsMultiLogin(index, username))
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.MultipleAccountsNotAllowed, (byte)Menu.Login);
                        return;
                    }

                    if (!Database.LoadAccount(index, username))
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.Login, (byte)Menu.Login);
                        return;
                    }

                    if (GetPlayerPassword(index) != password)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.WrongPassword, (byte)Menu.Login);
                        return;
                    }

                    if (Database.IsBanned(index, IP))
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.Banned, (byte)Menu.Login);
                        return;
                    }

                    if (GetAccountLogin(index) == "")
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.DatabaseError, (byte)Menu.Login);
                        return;
                    }

                    // Show the player up on the socket status
                    Log.Add(GetAccountLogin(index) + " has logged in from " + NetworkConfig.Socket.ClientIP(index) + ".", Constant.PLAYER_LOG);
                    Console.WriteLine(GetAccountLogin(index) + " has logged in from " + NetworkConfig.Socket.ClientIP(index) + ".");

                    // send them to the character portal
                    NetworkSend.SendPlayerChars(index);
                    NetworkSend.SendJobs(index);
                }
            }
            else
            {
                NetworkSend.AlertMsg(index, (byte)SystemMessage.Connection, (byte)Menu.Login);
            }
        }

        private static void Packet_Register(int index, ref byte[] data)
        {
            string username;
            string IP;
            string password;
            int i;
            int n;
            var buffer = new ByteStream(data);
            JObject userData;

            if (!NetworkConfig.IsPlaying(index))
            {
                if (!NetworkConfig.IsLoggedIn(index))
                {
                    // check if its banned
                    // Cut off last portion of ip
                    IP = NetworkConfig.Socket.ClientIP(index);

                    if (Database.IsBanned(index, IP))
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.Banned, (byte)Menu.Register);
                        return;
                    }

                    if (General.GetShutDownTimer.IsRunning)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.ServerMaintenance, (byte)Menu.Register);
                        return;
                    }

                    // Get the data
                    Reoria.Engine.Common.Security.Encryption.AesEncryption aes;
                    General.Aes.TryGetValue(index, out aes);
                    byte[] usernameBytes = buffer.ReadBytes().ToArray();
                    username = System.Text.Encoding.UTF8.GetString(aes.Decrypt(usernameBytes)).ToLower().Replace("\0", "");

                    byte[] passwordBytes = buffer.ReadBytes().ToArray();
                    password = System.Text.Encoding.UTF8.GetString(aes.Decrypt(passwordBytes)).Replace("\0", "");

                    // Get the current executing assembly
                    var assembly = Assembly.GetExecutingAssembly();

                    // Retrieve the version information
                    byte[] clientVersionBytes = buffer.ReadBytes().ToArray();
                    var serverVersion = assembly.GetName().Version.ToString();
                    var clientVersion = System.Text.Encoding.UTF8.GetString(aes.Decrypt(clientVersionBytes));

                    // Check versions
                    if (clientVersion != serverVersion)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.ClientOutdated, (byte)Menu.Register);
                        return;
                    }

                    int x = General.IsValidUsername(username);

                    // Check if the username is valid
                    if (x == -1)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.NameContainsIllegalChars, (byte)Menu.Register);
                        return;
                    }
                    else if (x == 0)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.NameLengthInvalid, (byte)Menu.Register);
                        return;
                    }

                    if (NetworkConfig.IsMultiLogin(index, username))
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.MultipleAccountsNotAllowed, (byte)Menu.Register);
                        return;
                    }

                    if (NetworkConfig.IsMultiLogin(index, username))
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.MultipleAccountsNotAllowed, (byte)Menu.Register);
                        return;
                    }

                    userData = Database.SelectRowByColumn("id", Database.GetStringHash(username), "account", "data");

                    if (userData is not null)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.NameTaken, (byte)Menu.Register);
                        return;
                    }

                    Database.RegisterAccount(index, username, password);

                    // send them to the character portal
                    NetworkSend.SendPlayerChars(index);
                    NetworkSend.SendJobs(index);
                }
            }
        }

        private static void Packet_UseChar(int index, ref byte[] data)
        {
            byte slot;
            var buffer = new ByteStream(data);

            if (!NetworkConfig.IsPlaying(index))
            {
                if (NetworkConfig.IsLoggedIn(index))
                {
                    slot = buffer.ReadByte();

                    if (slot < 1 | slot > Core.Constant.MAX_CHARS)
                    {
                        NetworkSend.AlertMsg(index, (byte)SystemMessage.MaxCharactersReached, (byte)Menu.CharacterSelect);
                        return;
                    }

                    NetworkConfig.LoadAccount(index, Core.Data.Account[index].Login, slot);
                }
            }
            else
            {
                NetworkSend.AlertMsg(index, (byte)SystemMessage.Connection, (byte)Menu.Login);
            }
        }

        private static void Packet_AddChar(int index, ref byte[] data)
        {
            string name;
            byte slot;
            int sexNum;
            int jobNum;
            int sprite;
            int i;
            int n;
            var buffer = new ByteStream(data);

            if (!NetworkConfig.IsPlaying(index))
            {
                slot = buffer.ReadByte();
                name = buffer.ReadString();
                sexNum = buffer.ReadInt32();
                jobNum = buffer.ReadInt32();

                if (slot < 1 | slot > Core.Constant.MAX_CHARS)
                {
                    NetworkSend.AlertMsg(index, (byte)SystemMessage.MaxCharactersReached, (byte)Menu.CharacterSelect);
                    return;
                }

                if (Database.LoadCharacter(index, slot))
                {
                    NetworkSend.SendPlayerChars(index);
                    return;
                }

                int x = General.IsValidUsername(name);

                // Check if the username is valid
                if (x == -1)
                {
                    NetworkSend.AlertMsg(index, (byte)SystemMessage.NameContainsIllegalChars, (byte)Menu.Register);
                    return;
                }
                else if (x == 0)
                {
                    NetworkSend.AlertMsg(index, (byte)SystemMessage.NameLengthInvalid, (byte)Menu.Register);
                    return;
                }

                // Check if name is already in use
                if (Data.Char.Contains(name))
                {
                    NetworkSend.AlertMsg(index, (byte)SystemMessage.NameTaken, (byte)Menu.NewCharacter);
                    return;
                }

                if (sexNum < (byte) Sex.Male | sexNum > (byte) Sex.Female)
                    return;

                if (jobNum < 0 | jobNum > Core.Constant.MAX_JOBS)
                    return;

                if (sexNum == (byte) Sex.Male)
                {
                    sprite = Data.Job[jobNum].MaleSprite;
                }
                else
                {
                    sprite = Data.Job[jobNum].FemaleSprite;
                }

                if (sprite == 0)
                {
                    sprite = 1;
                }

                // Everything went ok, add the character
                Data.Char.Add(name);
                Database.AddChar(index, slot, name, (byte)sexNum, (byte)jobNum, sprite);

                if (Data.Char.Count == 1)
                    SetPlayerAccess(index, (int)AccessLevel.Owner);

                Log.Add("Character " + name + " added to " + GetAccountLogin(index) + "'s account.", Constant.PLAYER_LOG);
                Player.HandleUseChar(index);

                buffer.Dispose();
            }
        }

        private static void Packet_DelChar(int index, ref byte[] data)
        {
            byte slot;
            var buffer = new ByteStream(data);

            if (!NetworkConfig.IsPlaying(index))
            {
                slot = buffer.ReadByte();

                if (slot < 1 | slot > Core.Constant.MAX_CHARS)
                {
                    NetworkSend.AlertMsg(index, (byte)SystemMessage.MaxCharactersReached, (byte)Menu.CharacterSelect);
                    return;
                }

                Database.LoadCharacter(index, slot);
                Data.Char.Remove(GetPlayerName(index));
                Database.ClearCharacter(index);
                Database.SaveCharacter(index, slot);

                // send them to the character portal
                NetworkSend.SendPlayerChars(index);

                buffer.Dispose();
            }
            else
            {
                NetworkSend.AlertMsg(index, (byte)SystemMessage.Connection, (byte)Menu.Login);
            }
        }

        private static void Packet_SayMessage(int index, ref byte[] data)
        {
            string msg;
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();

            Log.Add("Map #" + GetPlayerMap(index) + ": " + GetPlayerName(index) + " says, '" + msg + "'", Constant.PLAYER_LOG);

            NetworkSend.SayMsg_Map(GetPlayerMap(index), index, msg, (int) Color.White);
            NetworkSend.SendChatBubble(GetPlayerMap(index), index, (int)TargetType.Player, msg, (int) Color.White);

            buffer.Dispose();
        }

        private static void Packet_BroadCastMsg(int index, ref byte[] data)
        {
            string msg;
            string s;
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();

            s = "[Global] " + GetPlayerName(index) + ": " + msg;
            NetworkSend.SayMsg_Global(index, msg, (int) Color.White);
            Log.Add(s, Constant.PLAYER_LOG);
            Console.WriteLine(s);

            buffer.Dispose();
        }

        public static void Packet_PlayerMsg(int index, ref byte[] data)
        {
            string OtherPlayer;
            string Msg;
            int OtherPlayerindex;
            var buffer = new ByteStream(data);

            OtherPlayer = buffer.ReadString();
            Msg = buffer.ReadString();
            buffer.Dispose();

            OtherPlayerindex = GameLogic.FindPlayer(OtherPlayer);
            if (OtherPlayerindex != index)
            {
                if (OtherPlayerindex >= 0)
                {
                    Log.Add(GetPlayerName(index) + " tells " + GetPlayerName(index) + ", '" + Msg + "'", Constant.PLAYER_LOG);
                    NetworkSend.PlayerMsg(OtherPlayerindex, GetPlayerName(index) + " tells you, '" + Msg + "'", (int) Color.Pink);
                    NetworkSend.PlayerMsg(index, "You tell " + GetPlayerName(OtherPlayerindex) + ", '" + Msg + "'", (int) Color.Pink);
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) Color.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Cannot message your self!", (int) Color.BrightRed);
            }
        }

        private static void Packet_AdminMsg(int index, ref byte[] data)
        {
            string msg;
            var s = default(string);
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();

            NetworkSend.AdminMsg(GetPlayerMap(index), msg, (int) Color.BrightCyan);
            Log.Add(s, Constant.PLAYER_LOG);
            Console.WriteLine(s);

            buffer.Dispose();
        }

        private static void Packet_PlayerMove(int index, ref byte[] data)
        {
            byte Dir;
            byte movement;
            int tmpX;
            int tmpY;
            var buffer = new ByteStream(data);

            if (Core.Data.TempPlayer[index].GettingMap)
                return;

            Dir = buffer.ReadByte();
            movement = buffer.ReadByte();
            tmpX = buffer.ReadInt32();
            tmpY = buffer.ReadInt32();
            buffer.Dispose();

            SetPlayerDir(index, Dir);
            Core.Data.Player[index].Moving = movement;
            Core.Data.Player[index].IsMoving = true;

            // Set movement offset directly to 32 or -32 based on direction
            int offset = Core.Constant.TILE_SIZE;

            switch (Dir)
            {
                case (int)Direction.Up:
                    Core.Data.Player[index].YOffset -= offset;
                    break;
                case (int)Direction.Down:
                    Core.Data.Player[index].YOffset += offset;
                    break;
                case (int)Direction.Left:
                    Core.Data.Player[index].XOffset -= offset;
                    break;
                case (int)Direction.Right:
                    Core.Data.Player[index].XOffset += offset;
                    break;
                case (int)Direction.UpRight:
                    Core.Data.Player[index].XOffset += offset;
                    Core.Data.Player[index].YOffset -= offset;
                    break;
                case (int)Direction.UpLeft:
                    Core.Data.Player[index].XOffset -= offset;
                    Core.Data.Player[index].YOffset -= offset;
                    break;
                case (int)Direction.DownRight:
                    Core.Data.Player[index].XOffset += offset;
                    Core.Data.Player[index].YOffset += offset;
                    break;
                case (int)Direction.DownLeft:
                    Core.Data.Player[index].XOffset -= offset;
                    Core.Data.Player[index].YOffset += offset;
                    break;
            }

            buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SPlayerXYOffset);
            buffer.WriteByte(GetPlayerDir(index));
            buffer.WriteInt32(Core.Data.Player[index].XOffset);
            buffer.WriteInt32(Core.Data.Player[index].YOffset);
            buffer.WriteByte(Core.Data.Player[index].Moving);
            NetworkConfig.SendDataToMapBut(index, GetPlayerMap(index), buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }
        
        public static void Packet_StopPlayerMove(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);

            if (Core.Data.TempPlayer[index].GettingMap)
                return;

            Core.Data.Player[index].IsMoving = false;
            Core.Data.Player[index].Moving = 0;

            // Reset offsets
            Core.Data.Player[index].XOffset = 0;
            Core.Data.Player[index].YOffset = 0;

            buffer.Dispose();
        }

        public static void Packet_PlayerDirection(int index, ref byte[] data)
        {
            int dir;
            var buffer = new ByteStream(data);

            if (Core.Data.TempPlayer[index].GettingMap == true)
                return;

            dir = buffer.ReadInt32();
            buffer.Dispose();

            // Prevent hacking
            if (dir < (byte) Direction.Up | dir > (byte) Direction.DownRight)
                return;

            SetPlayerDir(index, dir);

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SPlayerDir);
            buffer.WriteInt32(index);
            buffer.WriteByte(GetPlayerDir(index));
            NetworkConfig.SendDataToMapBut(index, GetPlayerMap(index), buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();

        }

        public static void Packet_UseItem(int index, ref byte[] data)
        {
            int invNum;
            var buffer = new ByteStream(data);

            invNum = buffer.ReadInt32();
            buffer.Dispose();

            Player.UseItem(index, invNum);
        }

        public static void Packet_Attack(int index, ref byte[] data)
        {
            int i;
            int Tempindex;
            var x = default(int);
            var y = default(int);
            var buffer = new ByteStream(data);

            // can't attack whilst casting
            if (Core.Data.TempPlayer[index].SkillBuffer >= 0)
                return;

            // can't attack whilst stunned
            if (Core.Data.TempPlayer[index].StunDuration > 0)
                return;

            NetworkSend.SendPlayerAttack(index);

            // Projectile check
            if (GetPlayerEquipment(index, Equipment.Weapon) >= 0)
            {
                if (Core.Data.Item[GetPlayerEquipment(index, Equipment.Weapon)].Projectile > 0) // Item has a projectile
                {
                    if (Core.Data.Item[GetPlayerEquipment(index, Equipment.Weapon)].Ammo > 0)
                    {
                        if (Conversions.ToBoolean(Player.HasItem(index, Core.Data.Item[GetPlayerEquipment(index, Equipment.Weapon)].Ammo)))
                        {
                            Player.TakeInv(index, Core.Data.Item[GetPlayerEquipment(index, Equipment.Weapon)].Ammo, 1);
                            Projectile.PlayerFireProjectile(index);
                            return;
                        }
                        else
                        {
                            NetworkSend.PlayerMsg(index, "No more " + Core.Data.Item[Core.Data.Item[GetPlayerEquipment(index, Equipment.Weapon)].Ammo].Name + " !", (int)Core.Color.BrightRed);
                            return;
                        }
                    }
                    else
                    {
                        Projectile.PlayerFireProjectile(index);
                        return;
                    }
                }
            }

            // Check tradeskills
            switch (GetPlayerDir(index))
            {
                case  (byte) Direction.Up:
                    {

                        if (GetPlayerY(index) == 0)
                            return;
                        x = GetPlayerX(index);
                        y = GetPlayerY(index) - 1;
                        break;
                    }
                case (byte) Direction.Down:
                    {

                        if (GetPlayerY(index) == Data.Map[GetPlayerMap(index)].MaxY)
                            return;
                        x = GetPlayerX(index);
                        y = GetPlayerY(index) + 1;
                        break;
                    }
                case (byte) Direction.Left:
                    {

                        if (GetPlayerX(index) == 0)
                            return;
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index);
                        break;
                    }
                case (byte) Direction.Right:
                    {

                        if (GetPlayerX(index) == Data.Map[GetPlayerMap(index)].MaxX)
                            return;
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index);
                        break;
                    }

                case var case4 when case4 == (byte) Direction.UpRight:
                    {

                        if (GetPlayerX(index) == Data.Map[GetPlayerMap(index)].MaxX)
                            return;
                        if (GetPlayerY(index) == Data.Map[GetPlayerMap(index)].MaxY)
                            return;
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index) - 1;
                        break;
                    }

                case var case5 when case5 == (byte) Direction.UpLeft:
                    {

                        if (GetPlayerX(index) == Data.Map[GetPlayerMap(index)].MaxX)
                            return;
                        if (GetPlayerY(index) == Data.Map[GetPlayerMap(index)].MaxY)
                            return;
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index) - 1;
                        break;
                    }

                case var case6 when case6 == (byte) Direction.DownRight:
                    {

                        if (GetPlayerX(index) == Data.Map[GetPlayerMap(index)].MaxX)
                            return;
                        if (GetPlayerY(index) == Data.Map[GetPlayerMap(index)].MaxY)
                            return;
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index) + 1;
                        break;
                    }

                case var case7 when case7 == (byte) Direction.DownLeft:
                    {

                        if (GetPlayerX(index) == Data.Map[GetPlayerMap(index)].MaxX)
                            return;
                        if (GetPlayerY(index) == Data.Map[GetPlayerMap(index)].MaxY)
                            return;
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index) + 1;
                        break;
                    }
            }

            Resource.CheckResource(index, x, y);

            buffer.Dispose();
        }

        public static void Packet_PlayerInfo(int index, ref byte[] data)
        {
            int i;
            int n;
            string name;
            var buffer = new ByteStream(data);

            name = buffer.ReadString();
            i = GameLogic.FindPlayer(name);

            if (i >= 0)
            {
                NetworkSend.PlayerMsg(index, "Account:  " + GetAccountLogin(i) + ", Name: " + GetPlayerName(i), (int) Color.Yellow);

                if (GetPlayerAccess(index) > (byte)AccessLevel.Moderator)
                {
                    NetworkSend.PlayerMsg(index, " Stats for " + GetPlayerName(i) + " ", (int) Color.Yellow);
                    NetworkSend.PlayerMsg(index, "Level: " + GetPlayerLevel(i) + "  Exp: " + GetPlayerExp(i) + "/" + GetPlayerNextLevel(i), (int) Color.Yellow);
                    NetworkSend.PlayerMsg(index, "HP: " + GetPlayerVital(i, Vital.Health) + "/" + GetPlayerMaxVital(i, Vital.Health) + "  MP: " + GetPlayerVital(i, Vital.Stamina) + "/" + GetPlayerMaxVital(i, Vital.Stamina) + "  SP: " + GetPlayerVital(i, Vital.Stamina) + "/" + GetPlayerMaxVital(i, Vital.Stamina), (int) Color.Yellow);
                    NetworkSend.PlayerMsg(index, "Strength: " + GetPlayerStat(i, Stat.Strength) + "  Defense: " + GetPlayerStat(i, Stat.Luck) + "  Magic: " + GetPlayerStat(i, Stat.Intelligence) + "  Speed: " + GetPlayerStat(i, Stat.Spirit), (int) Color.Yellow);
                    n = GetPlayerStat(i, Stat.Strength) / 2 + GetPlayerLevel(i) / 2;
                    i = GetPlayerStat(i, Stat.Luck) / 2 + GetPlayerLevel(i) / 2;

                    if (n > 100)
                        n = 100;
                    if (i > 100)
                        i = 100;
                    NetworkSend.PlayerMsg(index, "Critical Hit Chance: " + n + "%, Block Chance: " + i + "%", (int) Color.Yellow);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Player is not online.", (int) Color.BrightRed);
            }

            buffer.Dispose();
        }

        public static void Packet_WarpMeTo(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Mapper)
                return;

            // The player
            n = GameLogic.FindPlayer(buffer.ReadString());
            buffer.Dispose();

            if (n != index)
            {
                if (n >= 0)
                {
                    Player.PlayerWarp(index, GetPlayerMap(n), GetPlayerX(n), GetPlayerY(n), (byte)Direction.Down);
                    NetworkSend.PlayerMsg(n, GetPlayerName(index) + " has warped to you.", (int) Color.Yellow);
                    NetworkSend.PlayerMsg(index, "You have been warped to " + GetPlayerName(n) + ".", (int) Color.Yellow);
                    Log.Add(GetPlayerName(index) + " has warped to " + GetPlayerName(n) + ", map #" + GetPlayerMap(n) + ".", Constant.ADMIN_LOG);
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) Color.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You cannot warp to yourself, dumbass!", (int) Color.BrightRed);
            }

        }

        public static void Packet_WarpToMe(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Mapper)
                return;

            // The player
            n = GameLogic.FindPlayer(buffer.ReadString());
            buffer.Dispose();

            if (n != index)
            {
                if (n >= 0)
                {
                    Player.PlayerWarp(n, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index), (byte)Direction.Down);
                    NetworkSend.PlayerMsg(n, "You have been summoned by " + GetPlayerName(index) + ".", (int) Color.Yellow);
                    NetworkSend.PlayerMsg(index, GetPlayerName(n) + " has been summoned.", (int) Color.Yellow);
                    Log.Add(GetPlayerName(index) + " has warped " + GetPlayerName(n) + " to self, map #" + GetPlayerMap(index) + ".", Constant.ADMIN_LOG);
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) Color.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You cannot warp yourself to yourself, dumbass!", (int) Color.BrightRed);
            }

        }

        public static void Packet_WarpTo(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Mapper)
                return;

            // The map
            n = buffer.ReadInt32();
            buffer.Dispose();

            // Prevent hacking
            if (n < 0 | n > Core.Constant.MAX_MAPS)
                return;

            Player.PlayerWarp(index, n, GetPlayerX(index), GetPlayerY(index), (byte)Direction.Down);
            NetworkSend.PlayerMsg(index, "You have been warped to map #" + n, (int) Color.Yellow);
            Log.Add(GetPlayerName(index) + " warped to map #" + n + ".", Constant.ADMIN_LOG);
        }

        public static void Packet_SetSprite(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Mapper)
                return;

            // The sprite
            n = buffer.ReadInt32();
            buffer.Dispose();

            SetPlayerSprite(index, n);
            NetworkSend.SendPlayerData(index);

        }

        public static void Packet_GetStats(int index, ref byte[] data)
        {
            int i;
            int n;
            var buffer = new ByteStream(data);

            NetworkSend.PlayerMsg(index, "Stats: " + GetPlayerName(index), (int) Color.Yellow);
            NetworkSend.PlayerMsg(index, "Level: " + GetPlayerLevel(index) + "  Exp: " + GetPlayerExp(index) + "/" + GetPlayerNextLevel(index), (int) Color.Yellow);
            NetworkSend.PlayerMsg(index, "HP: " + GetPlayerVital(index, Vital.Health) + "/" + GetPlayerMaxVital(index, Vital.Health) + "  MP: " + GetPlayerVital(index, Vital.Stamina) + "/" + GetPlayerMaxVital(index, Vital.Stamina) + "  SP: " + GetPlayerVital(index, Vital.Stamina) + "/" + GetPlayerMaxVital(index, Vital.Stamina), (int) Color.Yellow);
            NetworkSend.PlayerMsg(index, "STR: " + GetPlayerStat(index, Stat.Strength) + "  DEF: " + GetPlayerStat(index, Stat.Luck) + "  MAGI: " + GetPlayerStat(index, Stat.Intelligence) + "  Speed: " + GetPlayerStat(index, Stat.Spirit), (int) Color.Yellow);
            n = GetPlayerStat(index, Stat.Strength) / 2 + GetPlayerLevel(index) / 2;
            i = GetPlayerStat(index, Stat.Luck) / 2 + GetPlayerLevel(index) / 2;

            if (n > 100)
                n = 100;
            if (i > 100)
                i = 100;
            NetworkSend.PlayerMsg(index, "Critical Hit Chance: " + n + "%, Block Chance: " + i + "%", (int) Color.Yellow);
            buffer.Dispose();
        }

        public static void Packet_RequestNewMap(int index, ref byte[] data)
        {
            int dir;
            var buffer = new ByteStream(data);

            dir = buffer.ReadInt32();
            buffer.Dispose();

            Player.PlayerMove(index, dir, 1, true);
        }

        public static void Packet_MapData(int index, ref byte[] data)
        {
            int i;
            int mapNum;
            int x;
            int y;

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Mapper)
                return;

            var buffer = new ByteStream(Mirage.Sharp.Asfw.IO.Compression.DecompressBytes(data));

            mapNum = GetPlayerMap(index);

            i = Data.Map[mapNum].Revision + 1;
            Database.ClearMap(mapNum);

            Data.Map[mapNum].Name = buffer.ReadString();
            Data.Map[mapNum].Music = buffer.ReadString();
            Data.Map[mapNum].Revision = i;
            Data.Map[mapNum].Moral = (byte)buffer.ReadInt32();
            Data.Map[mapNum].Tileset = buffer.ReadInt32();
            Data.Map[mapNum].Up = buffer.ReadInt32();
            Data.Map[mapNum].Down = buffer.ReadInt32();
            Data.Map[mapNum].Left = buffer.ReadInt32();
            Data.Map[mapNum].Right = buffer.ReadInt32();
            Data.Map[mapNum].BootMap = buffer.ReadInt32();
            Data.Map[mapNum].BootX = (byte)buffer.ReadInt32();
            Data.Map[mapNum].BootY = (byte)buffer.ReadInt32();
            Data.Map[mapNum].MaxX = (byte)buffer.ReadInt32();
            Data.Map[mapNum].MaxY = (byte)buffer.ReadInt32();
            Data.Map[mapNum].Weather = (byte)buffer.ReadInt32();
            Data.Map[mapNum].Fog = buffer.ReadInt32();
            Data.Map[mapNum].WeatherIntensity = buffer.ReadInt32();
            Data.Map[mapNum].FogOpacity = (byte)buffer.ReadInt32();
            Data.Map[mapNum].FogSpeed = (byte)buffer.ReadInt32();
            Data.Map[mapNum].MapTint = buffer.ReadBoolean();
            Data.Map[mapNum].MapTintR = (byte)buffer.ReadInt32();
            Data.Map[mapNum].MapTintG = (byte)buffer.ReadInt32();
            Data.Map[mapNum].MapTintB = (byte)buffer.ReadInt32();
            Data.Map[mapNum].MapTintA = (byte)buffer.ReadInt32();
            Data.Map[mapNum].Panorama = buffer.ReadByte();
            Data.Map[mapNum].Parallax = buffer.ReadByte();
            Data.Map[mapNum].Brightness = buffer.ReadByte();
            Data.Map[mapNum].NoRespawn = buffer.ReadBoolean();
            Data.Map[mapNum].Indoors = buffer.ReadBoolean();
            Data.Map[mapNum].Shop = buffer.ReadInt32();

            Data.Map[mapNum].Tile = new Core.Type.Tile[(Data.Map[mapNum].MaxX), (Data.Map[mapNum].MaxY)];

            var loopTo = Core.Constant.MAX_MAP_NPCS;
            for (x = 0; x < loopTo; x++)
            {
                Database.ClearMapNpc(x, mapNum);
                Data.Map[mapNum].Npc[x] = buffer.ReadInt32();
            }

            {
                ref var withBlock = ref Data.Map[mapNum];
                var loopTo1 = (int)withBlock.MaxX;
                for (x = 0; x < loopTo1; x++)
                {
                    var loopTo2 = (int)withBlock.MaxY;
                    for (y = 0; y < loopTo2; y++)
                    {
                        withBlock.Tile[x, y].Data1 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data2 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data3 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data1_2 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data2_2 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data3_2 = buffer.ReadInt32();
                        withBlock.Tile[x, y].DirBlock = (byte)buffer.ReadInt32();
                        var loopTo3 = System.Enum.GetValues(typeof(MapLayer)).Length;
						withBlock.Tile[x, y].Layer = new Core.Type.Layer[loopTo3];
                        for (i = 0; i < (int)loopTo3; i++)
                        {
                            withBlock.Tile[x, y].Layer[i].Tileset = buffer.ReadInt32();
                            withBlock.Tile[x, y].Layer[i].X = buffer.ReadInt32();
                            withBlock.Tile[x, y].Layer[i].Y = buffer.ReadInt32();
                            withBlock.Tile[x, y].Layer[i].AutoTile = (byte)buffer.ReadInt32();
                        }
                        withBlock.Tile[x, y].Type = (TileType)buffer.ReadInt32();
                        withBlock.Tile[x, y].Type2 = (TileType)buffer.ReadInt32();
                    }
                }

            }

            Data.Map[mapNum].EventCount = buffer.ReadInt32();

            if (Data.Map[mapNum].EventCount > 0)
            {
                Data.Map[mapNum].Event = new Core.Type.Event[Data.Map[mapNum].EventCount];
                var loopTo4 = Data.Map[mapNum].EventCount;
                for (i = 0; i < loopTo4; i++)
                {
                    {
                        ref var withBlock1 = ref Data.Map[mapNum].Event[i];
                        withBlock1.Name = buffer.ReadString();
                        withBlock1.Globals = buffer.ReadByte();
                        withBlock1.X = buffer.ReadInt32();
                        withBlock1.Y = buffer.ReadInt32();
                        withBlock1.PageCount = buffer.ReadInt32();
                    }

                    if (Data.Map[mapNum].Event[i].PageCount > 0)
                    {
                        Data.Map[mapNum].Event[i].Pages = new Core.Type.EventPage[Data.Map[mapNum].Event[i].PageCount];
                        ;
                        Array.Resize(ref Core.Data.TempPlayer[i].EventMap.EventPages, Data.Map[mapNum].Event[i].PageCount);

                        var loopTo5 = Data.Map[mapNum].Event[i].PageCount;
                        for (x = 0; x < (int)loopTo5; x++)
                        {
                            {
                                ref var withBlock2 = ref Data.Map[mapNum].Event[i].Pages[x];
                                withBlock2.ChkVariable = buffer.ReadInt32();
                                withBlock2.VariableIndex = buffer.ReadInt32();
                                withBlock2.VariableCondition = buffer.ReadInt32();
                                withBlock2.VariableCompare = buffer.ReadInt32();

                                withBlock2.ChkSwitch = buffer.ReadInt32();
                                withBlock2.SwitchIndex = buffer.ReadInt32();
                                withBlock2.SwitchCompare = buffer.ReadInt32();

                                withBlock2.ChkHasItem = buffer.ReadInt32();
                                withBlock2.HasItemIndex = buffer.ReadInt32();
                                withBlock2.HasItemAmount = buffer.ReadInt32();

                                withBlock2.ChkSelfSwitch = buffer.ReadInt32();
                                withBlock2.SelfSwitchIndex = buffer.ReadInt32();
                                withBlock2.SelfSwitchCompare = buffer.ReadInt32();

                                withBlock2.GraphicType = buffer.ReadByte();
                                withBlock2.Graphic = buffer.ReadInt32();
                                withBlock2.GraphicX = buffer.ReadInt32();
                                withBlock2.GraphicY = buffer.ReadInt32();
                                withBlock2.GraphicX2 = buffer.ReadInt32();
                                withBlock2.GraphicY2 = buffer.ReadInt32();

                                withBlock2.MoveType = buffer.ReadByte();
                                withBlock2.MoveSpeed = buffer.ReadByte();
                                withBlock2.MoveFreq = buffer.ReadByte();
                                withBlock2.MoveRouteCount = buffer.ReadInt32();
                                withBlock2.IgnoreMoveRoute = buffer.ReadInt32();
                                withBlock2.RepeatMoveRoute = buffer.ReadInt32();

                                if (withBlock2.MoveRouteCount > 0)
                                {
                                    Data.Map[mapNum].Event[i].Pages[x].MoveRoute = new Core.Type.MoveRoute[withBlock2.MoveRouteCount];
                                    var loopTo6 = withBlock2.MoveRouteCount;
                                    for (y = 0; y < (int)loopTo6; y++)
                                    {
                                        withBlock2.MoveRoute[y].Index = buffer.ReadInt32();
                                        withBlock2.MoveRoute[y].Data1 = buffer.ReadInt32();
                                        withBlock2.MoveRoute[y].Data2 = buffer.ReadInt32();
                                        withBlock2.MoveRoute[y].Data3 = buffer.ReadInt32();
                                        withBlock2.MoveRoute[y].Data4 = buffer.ReadInt32();
                                        withBlock2.MoveRoute[y].Data5 = buffer.ReadInt32();
                                        withBlock2.MoveRoute[y].Data6 = buffer.ReadInt32();
                                    }
                                }

                                withBlock2.WalkAnim = buffer.ReadInt32();
                                withBlock2.DirFix = buffer.ReadInt32();
                                withBlock2.WalkThrough = buffer.ReadInt32();
                                withBlock2.ShowName = buffer.ReadInt32();
                                withBlock2.Trigger = buffer.ReadByte();
                                withBlock2.CommandListCount = buffer.ReadInt32();
                                withBlock2.Position = buffer.ReadByte();
                            }

                            if (Data.Map[mapNum].Event[i].Pages[x].CommandListCount > 0)
                            {
                                Data.Map[mapNum].Event[i].Pages[x].CommandList = new Core.Type.CommandList[Data.Map[mapNum].Event[i].Pages[x].CommandListCount];
                                var loopTo7 = Data.Map[mapNum].Event[i].Pages[x].CommandListCount;
                                for (y = 0; y < (int)loopTo7; y++)
                                {
                                    Data.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount = buffer.ReadInt32();
                                    Data.Map[mapNum].Event[i].Pages[x].CommandList[y].ParentList = buffer.ReadInt32();
                                    if (Data.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                    {
                                        Data.Map[mapNum].Event[i].Pages[x].CommandList[y].Commands = new Core.Type.EventCommand[Data.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount];
                                        for (int z = 0, loopTo8 = Data.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount; z < (int)loopTo8; z++)
                                        {
                                            {
                                                ref var withBlock3 = ref Data.Map[mapNum].Event[i].Pages[x].CommandList[y].Commands[z];
                                                withBlock3.Index = buffer.ReadInt32();
                                                withBlock3.Text1 = buffer.ReadString();
                                                withBlock3.Text2 = buffer.ReadString();
                                                withBlock3.Text3 = buffer.ReadString();
                                                withBlock3.Text4 = buffer.ReadString();
                                                withBlock3.Text5 = buffer.ReadString();
                                                withBlock3.Data1 = buffer.ReadInt32();
                                                withBlock3.Data2 = buffer.ReadInt32();
                                                withBlock3.Data3 = buffer.ReadInt32();
                                                withBlock3.Data4 = buffer.ReadInt32();
                                                withBlock3.Data5 = buffer.ReadInt32();
                                                withBlock3.Data6 = buffer.ReadInt32();
                                                withBlock3.ConditionalBranch.CommandList = buffer.ReadInt32();
                                                withBlock3.ConditionalBranch.Condition = buffer.ReadInt32();
                                                withBlock3.ConditionalBranch.Data1 = buffer.ReadInt32();
                                                withBlock3.ConditionalBranch.Data2 = buffer.ReadInt32();
                                                withBlock3.ConditionalBranch.Data3 = buffer.ReadInt32();
                                                withBlock3.ConditionalBranch.ElseCommandList = buffer.ReadInt32();
                                                withBlock3.MoveRouteCount = buffer.ReadInt32();
                                                int tmpCount = withBlock3.MoveRouteCount;
                                                if (tmpCount > 0)
                                                {
                                                    Array.Resize(ref withBlock3.MoveRoute, tmpCount);
                                                    for (int w = 0, loopTo9 = tmpCount; w < (int)loopTo9; w++)
                                                    {
                                                        withBlock3.MoveRoute[w].Index = buffer.ReadInt32();
                                                        withBlock3.MoveRoute[w].Data1 = buffer.ReadInt32();
                                                        withBlock3.MoveRoute[w].Data2 = buffer.ReadInt32();
                                                        withBlock3.MoveRoute[w].Data3 = buffer.ReadInt32();
                                                        withBlock3.MoveRoute[w].Data4 = buffer.ReadInt32();
                                                        withBlock3.MoveRoute[w].Data5 = buffer.ReadInt32();
                                                        withBlock3.MoveRoute[w].Data6 = buffer.ReadInt32();
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

            var loopTo13 = Data.Map[mapNum].EventCount;
            for (i = 0; i < loopTo13 - 1; i++)
            {
                if (Data.Map[mapNum].Event[i].PageCount == 0)
                {
                    Data.Map[mapNum].Event[i] = Data.Map[mapNum].Event[i + 1];
                    Data.Map[mapNum].Event[i + 1] = default;
                    Data.Map[mapNum].EventCount = Data.Map[mapNum].EventCount - 1;
                }
            }

            // Save the map
            Database.SaveMap(mapNum); 
            Npc.SpawnMapNpcs(mapNum);
            EventLogic.SpawnGlobalEvents(mapNum);
            
            var loopTo10 = NetworkConfig.Socket.HighIndex;
            for (i = 0; i < loopTo10; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    if (Core.Data.Player[i].Map == mapNum)
                    {
                        EventLogic.SpawnMapEventsFor(i, mapNum);
                    }
                }
            }

            // Clear it all out
            var loopTo11 = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo11; i++)
            {
                Item.SpawnItemSlot(i, -1, 0, GetPlayerMap(index), Data.MapItem[GetPlayerMap(index), i].X, Data.MapItem[GetPlayerMap(index), i].Y);
                Database.ClearMapItem(i, GetPlayerMap(index));
            }

            // Respawn
            Item.SpawnMapItems(GetPlayerMap(index));
            Resource.CacheResources(mapNum);

            // Refresh map for everyone online
            var loopTo12 = NetworkConfig.Socket.HighIndex;
            for (i = 0; i < loopTo12; i++)
            {
                if (NetworkConfig.IsPlaying(i) & GetPlayerMap(i) == mapNum)
                {
                    Player.PlayerWarp(i, mapNum, GetPlayerX(i), GetPlayerY(i), (byte)Direction.Down);
                    NetworkSend.SendMapData(i, mapNum, true);
                }
            }

            buffer.Dispose();
        }

        private static void Packet_NeedMap(int index, ref byte[] data)
        {
            int s;
            var buffer = new ByteStream(data);

            // Get yes/no value
            s = buffer.ReadInt32();
            buffer.Dispose();

            // Check if data is needed to be sent
            if (s == 1)
            {
                NetworkSend.SendMapData(index, GetPlayerMap(index), true);
            }
            else
            {
                NetworkSend.SendMapData(index, GetPlayerMap(index), false);
            }

            if (Data.Map[GetPlayerMap(index)].Shop >= 0 && Data.Map[GetPlayerMap(index)].Shop < Core.Constant.MAX_SHOPS)
            {
                if (!string.IsNullOrEmpty(Data.Shop[Data.Map[GetPlayerMap(index)].Shop].Name))
                {
                    Core.Data.TempPlayer[index].InShop = Data.Map[GetPlayerMap(index)].Shop;
                    NetworkSend.SendOpenShop(index, Data.Map[GetPlayerMap(index)].Shop);
                }
            }

            Core.Data.TempPlayer[index].GettingMap = false;
        }

        public static void Packet_RespawnMap(int index, ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Mapper)
                return;

            // Clear out it all
            var loopTo = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                Item.SpawnItemSlot(i, -1, 0, GetPlayerMap(index), Data.MapItem[GetPlayerMap(index), i].X, Data.MapItem[GetPlayerMap(index), i].Y);
                Database.ClearMapItem(i, GetPlayerMap(index));
            }

            // Respawn
            Item.SpawnMapItems(GetPlayerMap(index));

            // Respawn NpcS
            var loopTo1 = Core.Constant.MAX_MAP_NPCS;
            for (i = 0; i < loopTo1; i++)
                Npc.SpawnNpc(i, GetPlayerMap(index));

            EventLogic.SpawnMapEventsFor(index, GetPlayerMap(index));

            Resource.CacheResources(GetPlayerMap(index));
            NetworkSend.PlayerMsg(index, "Map respawned.", (int) Color.BrightGreen);
            Log.Add(GetPlayerName(index) + " has respawned map #" + GetPlayerMap(index), Constant.ADMIN_LOG);

            buffer.Dispose();
        }

        public static void Packet_KickPlayer(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Moderator)
            {
                return;
            }

            // The player index
            n = GameLogic.FindPlayer(buffer.ReadString());
            buffer.Dispose();

            if (n != index)
            {
                if (n >= 0)
                {
                    if (GetPlayerAccess(n) < GetPlayerAccess(index))
                    {
                        NetworkSend.GlobalMsg(GetPlayerName(n) + " has been kicked from " + SettingsManager.Instance.GameName + " by " + GetPlayerName(index) + "!");
                        Log.Add(GetPlayerName(index) + " has kicked " + GetPlayerName(n) + ".", Constant.ADMIN_LOG);
                        NetworkSend.AlertMsg(n, (byte)SystemMessage.Kicked, (byte)Menu.Login);
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "That is a higher or same access admin then you!", (int) Color.BrightRed);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) Color.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You cannot kick yourself!", (int) Color.BrightRed);
            }
        }

        public static void Packet_Banlist(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Moderator)
            {
                return;
            }

            NetworkSend.PlayerMsg(index, "Command /banlist is not available.", (int) Color.Yellow);
        }

        public static void Packet_DestroyBans(int index, ref byte[] data)
        {
            string filename;

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Owner)
                return;

            filename = System.IO.Path.Combine(Core.Path.Database, "banlist.txt");

            if (File.Exists(filename))
                FileSystem.Kill(filename);

            NetworkSend.PlayerMsg(index, "Ban list destroyed.", (int) Color.BrightGreen);
        }

        public static void Packet_BanPlayer(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Moderator)
                return;

            // The player index
            n = GameLogic.FindPlayer(buffer.ReadString());
            buffer.Dispose();

            if (n != index)
            {
                if (n >= 0)
                {
                    if (GetPlayerAccess(n) < GetPlayerAccess(index))
                    {
                        Database.BanPlayer(n, index);
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "That is a higher or same access admin then you!", (int) Color.BrightRed);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) Color.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You cannot ban yourself!", (int) Color.BrightRed);
            }

        }

        private static void Packet_RequestEditMap(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Mapper)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Map);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) Color.BrightRed);
                return;
            }

            Npc.SendNpcs(index);
            Item.SendItems(index);
            Animation.SendAnimations(index);
            NetworkSend.SendShops(index);
            Resource.SendResources(index);
            Event.SendMapEventData(index);
            Moral.SendMorals(index);

            Core.Data.TempPlayer[index].Editor = (byte) EditorType.Map;

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SEditMap);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void Packet_RequestEditShop(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Shop);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) Color.BrightRed);
                return;
            }

            Core.Data.TempPlayer[index].Editor = (byte) EditorType.Shop;

            Item.SendItems(index);
            NetworkSend.SendShops(index);

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SShopEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void Packet_SaveShop(int index, ref byte[] data)
        {
            int ShopNum;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            ShopNum = buffer.ReadInt32();

            // Prevent hacking
            if (ShopNum < 0 | ShopNum > Core.Constant.MAX_SHOPS)
                return;

            Data.Shop[ShopNum].BuyRate = buffer.ReadInt32();
            Data.Shop[ShopNum].Name = buffer.ReadString();

            for (int i = 0, loopTo = Core.Constant.MAX_TRADES; i < loopTo; i++)
            {
                Data.Shop[ShopNum].TradeItem[i].CostItem = buffer.ReadInt32();
                Data.Shop[ShopNum].TradeItem[i].CostValue = buffer.ReadInt32();
                Data.Shop[ShopNum].TradeItem[i].Item = buffer.ReadInt32();
                Data.Shop[ShopNum].TradeItem[i].ItemValue = buffer.ReadInt32();
            }

            buffer.Dispose();

            // Save it
            NetworkSend.SendUpdateShopToAll(ShopNum);
            Database.SaveShop(ShopNum);
            Log.Add(GetAccountLogin(index) + " saving shop #" + ShopNum + ".", Constant.ADMIN_LOG);
        }

        public static void Packet_RequestEditSkill(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Skill);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) Color.BrightRed);
                return;
            }

            Core.Data.TempPlayer[index].Editor = (byte) EditorType.Skill;

            NetworkSend.SendJobs(index);
            Projectile.SendProjectiles(index);
            Animation.SendAnimations(index);
            NetworkSend.SendSkills(index);

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SSkillEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void Packet_SaveSkill(int index, ref byte[] data)
        {
            int skillNum;
            var buffer = new ByteStream(data);

            skillNum = buffer.ReadInt32();

            // Prevent hacking
            if (skillNum < 0 | skillNum > Core.Constant.MAX_SKILLS)
                return;

            Data.Skill[skillNum].AccessReq = buffer.ReadInt32();
            Data.Skill[skillNum].AoE = buffer.ReadInt32();
            Data.Skill[skillNum].CastAnim = buffer.ReadInt32();
            Data.Skill[skillNum].CastTime = buffer.ReadInt32();
            Data.Skill[skillNum].CdTime = buffer.ReadInt32();
            Data.Skill[skillNum].JobReq = buffer.ReadInt32();
            Data.Skill[skillNum].Dir = (byte)buffer.ReadInt32();
            Data.Skill[skillNum].Duration = buffer.ReadInt32();
            Data.Skill[skillNum].Icon = buffer.ReadInt32();
            Data.Skill[skillNum].Interval = buffer.ReadInt32();
            Data.Skill[skillNum].IsAoE = Conversions.ToBoolean(buffer.ReadInt32());
            Data.Skill[skillNum].LevelReq = buffer.ReadInt32();
            Data.Skill[skillNum].Map = buffer.ReadInt32();
            Data.Skill[skillNum].MpCost = buffer.ReadInt32();
            Data.Skill[skillNum].Name = buffer.ReadString();
            Data.Skill[skillNum].Range = buffer.ReadInt32();
            Data.Skill[skillNum].SkillAnim = buffer.ReadInt32();
            Data.Skill[skillNum].StunDuration = buffer.ReadInt32();
            Data.Skill[skillNum].Type = (byte)buffer.ReadInt32();
            Data.Skill[skillNum].Vital = buffer.ReadInt32();
            Data.Skill[skillNum].X = buffer.ReadInt32();
            Data.Skill[skillNum].Y = buffer.ReadInt32();

            // projectiles
            Data.Skill[skillNum].IsProjectile = buffer.ReadInt32();
            Data.Skill[skillNum].Projectile = buffer.ReadInt32();

            Data.Skill[skillNum].KnockBack = (byte)buffer.ReadInt32();
            Data.Skill[skillNum].KnockBackTiles = (byte)buffer.ReadInt32();

            // Save it
            NetworkSend.SendUpdateSkillToAll(skillNum);
            Database.SaveSkill(skillNum);
            Log.Add(GetAccountLogin(index) + " saved Skill #" + skillNum + ".", Constant.ADMIN_LOG);

            buffer.Dispose();
        }

        public static void Packet_SetAccess(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;
            int i;

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Owner)
                return;

            // The index
            n = GameLogic.FindPlayer(buffer.ReadString());

            // The access
            i = buffer.ReadInt32();

            // Check for invalid access level
            if (i >= (int)AccessLevel.Player && i <= (int)AccessLevel.Owner)
            {
                // Check if player is on
                if (n >= 0)
                {
                    if (n != index)
                    {
                        // check to see if same level access is trying to change another access of the very same level and boot them if they are.
                        if (GetPlayerAccess(n) == GetPlayerAccess(index))
                        {
                            NetworkSend.PlayerMsg(index, "Invalid access level.", (int)Core.Color.BrightRed);
                            return;
                        }
                    }

                    if (GetPlayerAccess(n) == (int)AccessLevel.Player && i > (int)AccessLevel.Player)
                    {
                        NetworkSend.GlobalMsg(GetPlayerName(n) + " has been blessed with administrative access.");
                    }

                    SetPlayerAccess(n, i);
                    NetworkSend.SendPlayerData(n);
                    Log.Add(GetPlayerName(index) + " has modified " + GetPlayerName(n) + "'s access.", Constant.ADMIN_LOG);
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) Color.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Invalid access level.", (int) Color.BrightRed);
            }

            buffer.Dispose();
        }

        public static void Packet_WhosOnline(int index, ref byte[] data)
        {
            NetworkSend.SendWhosOnline(index);
        }

        public static void Packet_SetMotd(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Mapper)
                return;

            SettingsManager.Instance.Welcome = buffer.ReadString();
            SettingsManager.Save();

            NetworkSend.GlobalMsg("Welcome changed to: " + SettingsManager.Instance.Welcome);
            Log.Add(GetPlayerName(index) + " changed welcome to: " + SettingsManager.Instance.Welcome, Constant.ADMIN_LOG);

            buffer.Dispose();
        }

        public static void Packet_PlayerSearch(int index, ref byte[] data)
        {
            byte rclick;
            int x;
            int y;
            int i;
            var buffer = new ByteStream(data);

            x = buffer.ReadInt32();
            y = buffer.ReadInt32();
            rclick = (byte)buffer.ReadInt32();

            // Prevent subscript out of range
            if (x < 0 | x > (int)Data.Map[GetPlayerMap(index)].MaxX | y < 0 | y > (int)Data.Map[GetPlayerMap(index)].MaxY)
                return;

            // Check for a player   
            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i < loopTo; i++)
            {
                if (GetPlayerMap(index) == GetPlayerMap(i))
                {
                    if (GetPlayerX(i) == x)
                    {
                        if (GetPlayerY(i) == y)
                        {

                            // Consider the player
                            if (i != index)
                            {
                                if (GetPlayerLevel(i) >= GetPlayerLevel(index) + 5)
                                {
                                    NetworkSend.PlayerMsg(index, "You wouldn't stand a chance.", (int) Color.BrightRed);
                                }

                                else if (GetPlayerLevel(i) > GetPlayerLevel(index))
                                {
                                    NetworkSend.PlayerMsg(index, "This one seems to have an advantage over you.", (int) Color.Yellow);
                                }

                                else if (GetPlayerLevel(i) == GetPlayerLevel(index))
                                {
                                    NetworkSend.PlayerMsg(index, "This would be an even fight.", (int) Color.White);
                                }

                                else if (GetPlayerLevel(index) >= GetPlayerLevel(i) + 5)
                                {
                                    NetworkSend.PlayerMsg(index, "You could slaughter that player.", (int) Color.BrightBlue);
                                }

                                else if (GetPlayerLevel(index) > GetPlayerLevel(i))
                                {
                                    NetworkSend.PlayerMsg(index, "You would have an advantage over that player.", (int) Color.BrightCyan);
                                }
                            }

                            // Change target
                            if (Core.Data.TempPlayer[index].TargetType == 0 | i != Core.Data.TempPlayer[index].Target)
                            {
                                Core.Data.TempPlayer[index].Target = i;
                                Core.Data.TempPlayer[index].TargetType = (byte)TargetType.Player;
                            }
                            else
                            {
                                Core.Data.TempPlayer[index].Target = -1;
                                Core.Data.TempPlayer[index].TargetType = 0;
                            }

                            if (Core.Data.TempPlayer[index].Target >= 0)
                            {
                                NetworkSend.PlayerMsg(index, "Your target is now " + GetPlayerName(i) + ".", (int)Core.Color.Yellow);
                            }

                            NetworkSend.SendTarget(index, Core.Data.TempPlayer[index].Target, Core.Data.TempPlayer[index].TargetType);
                            if (rclick == 1)
                                NetworkSend.SendRightClick(index);
                            return;
                        }
                    }
                }

            }

            // Check for an item
            var loopTo1 = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo1; i++)
            {
                if (Data.MapItem[GetPlayerMap(index), i].Num >= 0)
                {
                    if (!string.IsNullOrEmpty(Core.Data.Item[(int)Data.MapItem[GetPlayerMap(index), i].Num].Name))
                    {
                        if ((int)Data.MapItem[GetPlayerMap(index), i].X == x)
                        {
                            if ((int)Data.MapItem[GetPlayerMap(index), i].Y == y)
                            {
                                NetworkSend.PlayerMsg(index, "You see " + Data.MapItem[GetPlayerMap(index), i].Value + " " + Core.Data.Item[(int)Data.MapItem[GetPlayerMap(index), i].Num].Name + ".", (int) Color.BrightGreen);
                                return;
                            }
                        }
                    }
                }
            }

            // Check for an npc
            var loopTo2 = Core.Constant.MAX_MAP_NPCS;
            for (i = 0; i < loopTo2; i++)
            {
                if (Data.MapNpc[GetPlayerMap(index)].Npc[i].Num >= 0)
                {
                    if (Data.MapNpc[GetPlayerMap(index)].Npc[i].X == x)
                    {
                        if (Data.MapNpc[GetPlayerMap(index)].Npc[i].Y == y)
                        {
                            // Change target
                            if (Core.Data.TempPlayer[index].TargetType == 0)
                            {
                                Core.Data.TempPlayer[index].Target = i;
                                Core.Data.TempPlayer[index].TargetType = (byte)TargetType.Npc;
                            }
                            else
                            {
                                Core.Data.TempPlayer[index].Target = -1;
                                Core.Data.TempPlayer[index].TargetType = 0;
                            }

                            if (Core.Data.TempPlayer[index].Target >= 0)
                            {
                                NetworkSend.PlayerMsg(index, "Your target is now " + GameLogic.CheckGrammar(Data.Npc[(int)Data.MapNpc[GetPlayerMap(index)].Npc[i].Num].Name) + ".", (int)Core.Color.Yellow);
                            }
                            NetworkSend.SendTarget(index, Core.Data.TempPlayer[index].Target, Core.Data.TempPlayer[index].TargetType);
                            return;
                        }
                    }
                }

            }

            buffer.Dispose();
        }

        public static void Packet_Skills(int index, ref byte[] data)
        {
            NetworkSend.SendPlayerSkills(index);
        }

        public static void Packet_Cast(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Skill slot
            n = buffer.ReadInt32();
            buffer.Dispose();

            if ((int)Data.Map[GetPlayerMap(index)].Moral >= 0)
            {
                if (Data.Moral[Data.Map[GetPlayerMap(index)].Moral].CanCast)
                {
                    try
                    {
                        Script.Instance?.BufferSkill(index, n);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public static void Packet_SwapInvSlots(int index, ref byte[] data)
        {
            double oldSlot;
            double newSlot;
            var buffer = new ByteStream(data);

            if (Core.Data.TempPlayer[index].InTrade >= 0 | Core.Data.TempPlayer[index].InBank | Core.Data.TempPlayer[index].InShop >= 0)
                return;

            // Old Slot
            oldSlot = buffer.ReadInt32();
            newSlot = buffer.ReadInt32();
            buffer.Dispose();

            Player.PlayerSwitchInvSlots(index, (int)oldSlot, (int)newSlot);

            buffer.Dispose();
        }

        public static void Packet_SwapSkillSlots(int index, ref byte[] data)
        {
            double oldSlot;
            double newSlot;
            var buffer = new ByteStream(data);

            if (Core.Data.TempPlayer[index].InTrade >= 0 | Core.Data.TempPlayer[index].InBank | Core.Data.TempPlayer[index].InShop >= 0)
                return;

            // Old Slot
            oldSlot = buffer.ReadInt32();
            newSlot = buffer.ReadInt32();
            buffer.Dispose();

            Player.PlayerSwitchSkillSlots(index, (int)oldSlot, (int)newSlot);

            buffer.Dispose();
        }

        public static void Packet_CheckPing(int index, ref byte[] data)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SSendPing);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void Packet_Unequip(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);

            Player.PlayerUnequipItem(index, buffer.ReadInt32());

            buffer.Dispose();
        }

        public static void Packet_RequestPlayerData(int index, ref byte[] data)
        {
            NetworkSend.SendPlayerData(index);
        }

        public static void Packet_RequestNpc(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;

            n = buffer.ReadInt32();

            if (n < 0 | n > Core.Constant.MAX_NPCS)
                return;

            Npc.SendUpdateNpcTo(index, n);
        }

        public static void Packet_SpawnItem(int index, ref byte[] data)
        {
            int tmpItem;
            int tmpAmount;
            var buffer = new ByteStream(data);

            // item
            tmpItem = buffer.ReadInt32();
            tmpAmount = buffer.ReadInt32();

            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            Item.SpawnItem(tmpItem, tmpAmount, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index));
            buffer.Dispose();
        }

        public static void Packet_TrainStat(int index, ref byte[] data)
        {
            int tmpStat;
            var buffer = new ByteStream(data);

            // check points
            if (GetPlayerPoints(index) == 0)
                return;

            // stat
            tmpStat = buffer.ReadInt32();

            try
            {
                Script.Instance?.TrainStat(index, tmpStat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            buffer.Dispose();
        }

        public static void Packet_RequestSkill(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;

            n = buffer.ReadInt32();

            if (n < 0 | n > Core.Constant.MAX_SKILLS)
                return;

            NetworkSend.SendUpdateSkillTo(index, n);
        }

        public static void Packet_RequestShop(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;

            n = buffer.ReadInt32();

            if (n < 0 | n > Core.Constant.MAX_SHOPS)
                return;

            NetworkSend.SendUpdateShopTo(index, n);
        }

        public static void Packet_RequestLevelUp(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            SetPlayerExp(index, GetPlayerNextLevel(index));
            Player.CheckPlayerLevelUp(index);
        }

        public static void Packet_ForgetSkill(int index, ref byte[] data)
        {
            int SkillSlot;
            var buffer = new ByteStream(data);

            SkillSlot = buffer.ReadInt32();

            // Check for subscript out of range
            if (SkillSlot < 0 | SkillSlot > Core.Constant.MAX_PLAYER_SKILLS)
                return;

            // dont let them forget a skill which is in CD
            if (Core.Data.TempPlayer[index].SkillCD[SkillSlot] > 0)
            {
                NetworkSend.PlayerMsg(index, "Cannot forget a skill which is cooling down!", (int) Color.BrightRed);
                return;
            }

            // dont let them forget a skill which is buffered
            if (Core.Data.TempPlayer[index].SkillBuffer == SkillSlot)
            {
                NetworkSend.PlayerMsg(index, "Cannot forget a skill which you are casting!", (int) Color.BrightRed);
                return;
            }

            Core.Data.Player[index].Skill[SkillSlot].Num = -1;
            NetworkSend.SendPlayerSkills(index);

            buffer.Dispose();
        }

        public static void Packet_CloseShop(int index, ref byte[] data)
        {
            Core.Data.TempPlayer[index].InShop = -1;
        }

        public static void Packet_BuyItem(int index, ref byte[] data)
        {
            int shopSlot;
            double shopMum;
            int itemAmount;
            var buffer = new ByteStream(data);

            shopSlot = buffer.ReadInt32();

            // not in shop, exit out
            shopMum = Core.Data.TempPlayer[index].InShop;

            if (shopMum < 0 | shopMum > Core.Constant.MAX_SHOPS)
                return;

            ref var withBlock = ref Data.Shop[(int)shopMum].TradeItem[shopSlot];

            // check trade exists
            if (withBlock.Item < 0)
                return;

            // check has the cost item
            itemAmount = Player.HasItem(index, withBlock.CostItem);
            if (itemAmount == 0 | itemAmount < withBlock.CostValue)
            {
                NetworkSend.PlayerMsg(index, "You do not have enough to buy this item.", (int) Color.BrightRed);
                NetworkSend.ResetShopAction(index);
                return;
            }

            // it's fine, let's go ahead
            for (int i = 0, loopTo = withBlock.CostValue; i < loopTo; i++)
                Player.TakeInv(index, withBlock.CostItem, withBlock.CostValue);
            Player.GiveInv(index, withBlock.Item, withBlock.ItemValue);

            // send confirmation message & reset their shop action
            NetworkSend.PlayerMsg(index, "Trade successful.", (int) Color.BrightGreen);
            NetworkSend.ResetShopAction(index);

            buffer.Dispose();
        }

        public static void Packet_SellItem(int index, ref byte[] data)
        {
            int invSlot;
            double itemNum;
            int price;
            double multiplier;
            double shopNum;
            var buffer = new ByteStream(data);

            invSlot = buffer.ReadInt32();

            // if invalid, exit out
            if (invSlot < 0 | invSlot > Core.Constant.MAX_INV)
                return;

            // has item?
            if (GetPlayerInv(index, invSlot) < 0 | GetPlayerInv(index, invSlot) > Core.Constant.MAX_ITEMS)
                return;

            // seems to be valid
            itemNum = GetPlayerInv(index, invSlot);
            shopNum = Core.Data.TempPlayer[index].InShop;

            if (shopNum < 0 || shopNum > Core.Constant.MAX_SHOPS)
            {
                return;
            }

            // work out price
            multiplier = Data.Shop[(int)shopNum].BuyRate / 100d;
            price = (int)Math.Round(Core.Data.Item[(int)itemNum].Price * multiplier);

            // item has cost?
            if (price < 0)
            {
                NetworkSend.PlayerMsg(index, "The shop doesn't want that item.", (int) Color.Yellow);
                NetworkSend.ResetShopAction(index);
                return;
            }

            // take item and give gold
            Player.TakeInv(index, (int)itemNum, 1);
            Player.GiveInv(index, 0, price);

            // send confirmation message & reset their shop action
            NetworkSend.PlayerMsg(index, "Sold the " + Core.Data.Item[(int)itemNum].Name + " for " + price + " " + Core.Data.Item[(int)itemNum].Name + "!", (int) Color.BrightGreen);
            NetworkSend.ResetShopAction(index);

            buffer.Dispose();
        }

        public static void Packet_ChangeBankSlots(int index, ref byte[] data)
        {
            int oldslot;
            int newslot;
            var buffer = new ByteStream(data);

            oldslot = buffer.ReadInt32();
            newslot = buffer.ReadInt32();

            Player.PlayerSwitchbankSlots(index, oldslot, newslot);

            buffer.Dispose();
        }

        public static void Packet_DepositItem(int index, ref byte[] data)
        {
            int invslot;
            int amount;
            var buffer = new ByteStream(data);

            invslot = buffer.ReadInt32();
            amount = buffer.ReadInt32();

            Player.GiveBank(index, invslot, amount);

            buffer.Dispose();
        }

        public static void Packet_WithdrawItem(int index, ref byte[] data)
        {
            byte bankSlot;
            int amount;
            var buffer = new ByteStream(data);

            bankSlot = buffer.ReadByte();
            amount = buffer.ReadInt32();

            Player.TakeBank(index, bankSlot, amount);

            buffer.Dispose();
        }

        public static void Packet_CloseBank(int index, ref byte[] data)
        {
            Core.Data.TempPlayer[index].InBank = false;
        }

        public static void Packet_AdminWarp(int index, ref byte[] data)
        {
            int x;
            int y;
            var buffer = new ByteStream(data);

            x = buffer.ReadInt32();
            y = buffer.ReadInt32();

            if (x < 0 || x > Data.Map[GetPlayerMap(index)].MaxX || y < 0 || y > Data.Map[GetPlayerMap(index)].MaxY)
                return;

            if (GetPlayerAccess(index) >= (byte) AccessLevel.Mapper)
            {
                Core.Data.Player[index].IsMoving = false;

                // Set the information
                SetPlayerX(index, x);
                SetPlayerY(index, y);
                SetPlayerDir(index, (byte)Direction.Down);

                NetworkSend.SendPlayerMove(index, 0);
            }

            buffer.Dispose();
        }

        public static void Packet_TradeInvite(int index, ref byte[] data)
        {
            string Name;
            int tradeTarget;
            var buffer = new ByteStream(data);

            Name = buffer.ReadString();

            buffer.Dispose();

            // Check for a player
            tradeTarget = GameLogic.FindPlayer(Name);

            if (tradeTarget < 0 | tradeTarget >= Core.Constant.MAX_PLAYERS)
                return;

            // can't trade with yourself..
            if (tradeTarget == index)
            {
                NetworkSend.PlayerMsg(index, "You can't trade with yourself!", (int) Color.BrightRed);
                return;
            }

            // send the trade request
            Core.Data.TempPlayer[index].TradeRequest = tradeTarget;
            Core.Data.TempPlayer[tradeTarget].TradeRequest = index;

            NetworkSend.PlayerMsg(tradeTarget, GetPlayerName(index) + " has invited you to trade.", (int) Color.Yellow);
            NetworkSend.PlayerMsg(index, "You have invited " + GetPlayerName(tradeTarget) + " to trade.", (int) Color.BrightGreen);

            NetworkSend.SendTradeInvite(tradeTarget, index);
        }

        public static void Packet_HandleTradeInvite(int index, ref byte[] data)
        {
            int tradeTarget;
            byte status;
            var buffer = new ByteStream(data);

            status = (byte)buffer.ReadInt32();

            buffer.Dispose();

            tradeTarget = Core.Data.TempPlayer[index].TradeRequest;

            if (tradeTarget < 0 | tradeTarget >= Core.Constant.MAX_PLAYERS)
                return;

            if (status == 0)
            {
                NetworkSend.PlayerMsg(tradeTarget, GetPlayerName(index) + " has declined your trade request.", (int) Color.BrightRed);
                NetworkSend.PlayerMsg(index, "You have declined the trade with " + GetPlayerName(tradeTarget) + ".", (int) Color.BrightRed);
                Core.Data.TempPlayer[index].TradeRequest = -1;
                return;
            }

            // Let them tradetradeTarget
            if (Core.Data.TempPlayer[tradeTarget].TradeRequest == index)
            {
                // let them know they're trading
                NetworkSend.PlayerMsg(index, "You have accepted " + GetPlayerName(tradeTarget) + "'s trade request.", (int) Color.Yellow);
                NetworkSend.PlayerMsg(tradeTarget, GetPlayerName(index) + " has accepted your trade request.", (int) Color.BrightGreen);

                // clear the tradeRequest server-side
                Core.Data.TempPlayer[index].TradeRequest = -1;
                Core.Data.TempPlayer[tradeTarget].TradeRequest = -1;

                // set that they're trading with each other
                Core.Data.TempPlayer[index].InTrade = tradeTarget;

                // clear out their trade offers
                Core.Data.TempPlayer[tradeTarget].InTrade = index;
                ;
                Array.Resize(ref Core.Data.TempPlayer[index].TradeOffer, Core.Constant.MAX_INV);
                Array.Resize(ref Core.Data.TempPlayer[tradeTarget].TradeOffer, Core.Constant.MAX_INV);

                for (int i = 0, loopTo = Core.Constant.MAX_INV; i < loopTo; i++)
                {
                    Core.Data.TempPlayer[index].TradeOffer[i].Num = -1;
                    Core.Data.TempPlayer[index].TradeOffer[i].Value = 0;
                    Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Num = -1;
                    Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Value = 0;
                }

                // Used to init the trade window clientside
                NetworkSend.SendTrade(index, tradeTarget);
                NetworkSend.SendTrade(tradeTarget, index);

                // Send the offer data - Used to clear their client
                NetworkSend.SendTradeUpdate(index, 0);
                NetworkSend.SendTradeUpdate(index, 1);
                NetworkSend.SendTradeUpdate(tradeTarget, 0);
                NetworkSend.SendTradeUpdate(tradeTarget, 1);
            }
        }

        public static void Packet_TradeInviteDecline(int index, ref byte[] data)
        {
            Core.Data.TempPlayer[index].TradeRequest = -1;
        }

        public static void Packet_AcceptTrade(int index, ref byte[] data)
        {
            int itemNum;
            int tradeTarget;
            int i;
            var tmpTradeItem = new PlayerInv[Core.Constant.MAX_INV];
            var tmpTradeItem2 = new PlayerInv[Core.Constant.MAX_INV];

            Core.Data.TempPlayer[index].AcceptTrade = true;

            tradeTarget = (int)Core.Data.TempPlayer[index].InTrade;

            // if not both of them accept, then exit
            if (!Core.Data.TempPlayer[tradeTarget].AcceptTrade)
            {
                NetworkSend.SendTradeStatus(index, 2);
                NetworkSend.SendTradeStatus(tradeTarget, 1);
                return;
            }

            // take their items
            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo; i++)
            {
                tmpTradeItem[i].Num = -1;
                tmpTradeItem2[i].Num = -1;

                // player
                if (Core.Data.TempPlayer[index].TradeOffer[i].Num >= 0)
                {
                    itemNum = (int)Core.Data.Player[index].Inv[(int)Core.Data.TempPlayer[index].TradeOffer[i].Num].Num;
                    if (itemNum >= 0)
                    {
                        // store temp
                        tmpTradeItem[i].Num = itemNum;
                        tmpTradeItem[i].Value = Core.Data.TempPlayer[index].TradeOffer[i].Value;
                        // take item
                        Player.TakeInvSlot(index, (int)Core.Data.TempPlayer[index].TradeOffer[i].Num, tmpTradeItem[i].Value);
                    }
                }
                // target
                if (Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Num >= 0)
                {
                    itemNum = GetPlayerInv(tradeTarget, (int)Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Num);
                    if (itemNum >= 0)
                    {
                        // store temp
                        tmpTradeItem2[i].Num = itemNum;
                        tmpTradeItem2[i].Value = Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Value;
                        // take item
                        Player.TakeInvSlot(tradeTarget, (int)Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Num, tmpTradeItem2[i].Value);
                    }
                }
            }

            // taken all items. now they can't not get items because of no inventory space.
            var loopTo1 = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo1; i++)
            {
                // player
                if (tmpTradeItem2[i].Num >= 0)
                {
                    // give away!
                    Player.GiveInv(index, (int)tmpTradeItem2[i].Num, tmpTradeItem2[i].Value, false);
                }
                // target
                if (tmpTradeItem[i].Num >= 0)
                {
                    // give away!
                    Player.GiveInv(tradeTarget, (int)tmpTradeItem[i].Num, tmpTradeItem[i].Value, false);
                }
            }

            NetworkSend.SendInventory(index);
            NetworkSend.SendInventory(tradeTarget);

            // they now have all the items. Clear out values + let them out of the trade.
            var loopTo2 = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo2; i++)
            {
                Core.Data.TempPlayer[index].TradeOffer[i].Num = -1;
                Core.Data.TempPlayer[index].TradeOffer[i].Value = 0;
                Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Num = -1;
                Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Value = 0;
            }

            Core.Data.TempPlayer[index].InTrade = -1;
            Core.Data.TempPlayer[tradeTarget].InTrade = -1;

            NetworkSend.PlayerMsg(index, "Trade completed.", (int) Color.BrightGreen);
            NetworkSend.PlayerMsg(tradeTarget, "Trade completed.", (int) Color.BrightGreen);

            NetworkSend.SendCloseTrade(index);
            NetworkSend.SendCloseTrade(tradeTarget);
        }

        public static void Packet_DeclineTrade(int index, ref byte[] data)
        {
            int tradeTarget;

            tradeTarget = (int)Core.Data.TempPlayer[index].InTrade;

            for (int i = 0, loopTo = Core.Constant.MAX_INV; i < loopTo; i++)
            {
                Core.Data.TempPlayer[index].TradeOffer[i].Num = -1;
                Core.Data.TempPlayer[index].TradeOffer[i].Value = 0;
                Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Num = -1;
                Core.Data.TempPlayer[tradeTarget].TradeOffer[i].Value = 0;
            }

            Core.Data.TempPlayer[index].InTrade = -1;
            Core.Data.TempPlayer[tradeTarget].InTrade = -1;

            NetworkSend.PlayerMsg(index, "You declined the trade.", (int) Color.BrightRed);
            NetworkSend.PlayerMsg(tradeTarget, GetPlayerName(index) + " has declined the trade.", (int) Color.BrightRed);

            NetworkSend.SendCloseTrade(index);
            NetworkSend.SendCloseTrade(tradeTarget);
        }

        public static void Packet_TradeItem(int index, ref byte[] data)
        {
            int itemnum;
            int invslot;
            int amount;
            var emptyslot = default(int);
            int i;
            var buffer = new ByteStream(data);

            invslot = buffer.ReadInt32();
            amount = buffer.ReadInt32();

            buffer.Dispose();

            if (invslot < 0 | invslot > Core.Constant.MAX_INV)
                return;

            itemnum = GetPlayerInv(index, invslot);

            if (itemnum < 0 | itemnum > Core.Constant.MAX_ITEMS)
                return;

            // make sure they have the amount they offer
            if (amount < 0 | amount > GetPlayerInvValue(index, invslot))
                return;

            if (Core.Data.Item[itemnum].Type == (byte)ItemCategory.Currency | Core.Data.Item[itemnum].Stackable == 1)
            {

                // check if already offering same currency item
                var loopTo = Core.Constant.MAX_INV;
                for (i = 0; i < loopTo; i++)
                {

                    if (Core.Data.TempPlayer[index].TradeOffer[i].Num == invslot)
                    {
                        // add amount
                        Core.Data.TempPlayer[index].TradeOffer[i].Value = Core.Data.TempPlayer[index].TradeOffer[i].Value + amount;

                        // clamp to limits
                        if (Core.Data.TempPlayer[index].TradeOffer[i].Value > GetPlayerInvValue(index, invslot))
                        {
                            Core.Data.TempPlayer[index].TradeOffer[i].Value = GetPlayerInvValue(index, invslot);
                        }

                        // cancel any trade agreement
                        Core.Data.TempPlayer[index].AcceptTrade = false;
                        Core.Data.TempPlayer[(int)Core.Data.TempPlayer[index].InTrade].AcceptTrade = false;

                        NetworkSend.SendTradeStatus(index, 0);
                        NetworkSend.SendTradeStatus((int)Core.Data.TempPlayer[index].InTrade, 1);

                        NetworkSend.SendTradeUpdate(index, 0);
                        NetworkSend.SendTradeUpdate(index, 1);
                        NetworkSend.SendTradeUpdate((int)Core.Data.TempPlayer[index].InTrade, 0);
                        NetworkSend.SendTradeUpdate((int)Core.Data.TempPlayer[index].InTrade, 1);
                        return;
                    }
                }
            }
            else
            {
                // make sure they're not already offering it
                var loopTo1 = Core.Constant.MAX_INV;
                for (i = 0; i < loopTo1; i++)
                {
                    if (Core.Data.TempPlayer[index].TradeOffer[i].Num == invslot)
                    {
                        NetworkSend.PlayerMsg(index, "You've already offered this item.", (int) Color.BrightRed);
                        return;
                    }
                }
            }

            // not already offering - find earliest empty slot
            var loopTo2 = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo2; i++)
            {
                if (Core.Data.TempPlayer[index].TradeOffer[i].Num == -1)
                {
                    emptyslot = i;
                    break;
                }
            }
            Core.Data.TempPlayer[index].TradeOffer[emptyslot].Num = invslot;
            Core.Data.TempPlayer[index].TradeOffer[emptyslot].Value = amount;

            // cancel any trade agreement and send new data
            Core.Data.TempPlayer[index].AcceptTrade = false;
            Core.Data.TempPlayer[(int)Core.Data.TempPlayer[index].InTrade].AcceptTrade = false;

            NetworkSend.SendTradeStatus(index, 0);
            NetworkSend.SendTradeStatus((int)Core.Data.TempPlayer[index].InTrade, 0);

            NetworkSend.SendTradeUpdate(index, 0);
            NetworkSend.SendTradeUpdate(index, 1);
            NetworkSend.SendTradeUpdate((int)Core.Data.TempPlayer[index].InTrade, 0);
            NetworkSend.SendTradeUpdate((int)Core.Data.TempPlayer[index].InTrade, 1);
        }

        public static void Packet_UntradeItem(int index, ref byte[] data)
        {
            int tradeslot;
            var buffer = new ByteStream(data);

            tradeslot = buffer.ReadInt32();

            buffer.Dispose();

            if (tradeslot < 0 | tradeslot > Core.Constant.MAX_INV)
                return;

            if (Core.Data.TempPlayer[index].TradeOffer[tradeslot].Num < 0)
                return;

            Core.Data.TempPlayer[index].TradeOffer[tradeslot].Num = -1;
            Core.Data.TempPlayer[index].TradeOffer[tradeslot].Value = 0;

            if (Core.Data.TempPlayer[index].AcceptTrade)
                Core.Data.TempPlayer[index].AcceptTrade = false;
            if (Core.Data.TempPlayer[(int)Core.Data.TempPlayer[index].InTrade].AcceptTrade)
                Core.Data.TempPlayer[(int)Core.Data.TempPlayer[index].InTrade].AcceptTrade = false;

            NetworkSend.SendTradeStatus(index, 0);
            NetworkSend.SendTradeStatus((int)Core.Data.TempPlayer[index].InTrade, 0);

            NetworkSend.SendTradeUpdate(index, 0);
            NetworkSend.SendTradeUpdate((int)Core.Data.TempPlayer[index].InTrade, 1);
        }

        public static void HackingAttempt(int index, string Reason)
        {

            if (index > 0 & NetworkConfig.IsPlaying(index))
            {
                NetworkSend.GlobalMsg(GetAccountLogin(index) + "/" + GetPlayerName(index) + " has been booted for (" + Reason + ")");

                NetworkSend.AlertMsg(index, (byte)SystemMessage.Connection, (byte)Menu.Login);
            }

        }

        public static void Packet_MapReport(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Mapper)
                return;

            NetworkSend.SendMapReport(index);
        }

        public static void Packet_Admin(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Moderator)
                return;

            NetworkSend.SendAdminPanel(index);
        }

        public static void Packet_SetHotbarSlot(int index, ref byte[] data)
        {
            int newSlot;
            int skill;
            byte @type;
            int oldSlot;
            var buffer = new ByteStream(data);

            type = (byte)buffer.ReadInt32();
            newSlot = buffer.ReadInt32();
            oldSlot = buffer.ReadInt32();
            skill = buffer.ReadInt32();

            if (newSlot < 0 | newSlot > Core.Constant.MAX_HOTBAR)
                return;

            if (type == (byte)PartOrigin.Hotbar)
            {
                if (oldSlot < 0 | oldSlot > Core.Constant.MAX_HOTBAR)
                    return;

                int oldItem = Core.Data.Player[index].Hotbar[oldSlot].Slot;
                byte oldType = Core.Data.Player[index].Hotbar[oldSlot].SlotType;
                int newItem = Core.Data.Player[index].Hotbar[newSlot].Slot;
                byte newType = Core.Data.Player[index].Hotbar[newSlot].SlotType;

                Core.Data.Player[index].Hotbar[newSlot].Slot = oldItem;
                Core.Data.Player[index].Hotbar[newSlot].SlotType = oldType;

                Core.Data.Player[index].Hotbar[oldSlot].Slot = newItem;
                Core.Data.Player[index].Hotbar[oldSlot].SlotType = newType;
            }
            else
            {
                Core.Data.Player[index].Hotbar[newSlot].Slot = skill;
                Core.Data.Player[index].Hotbar[newSlot].SlotType = type;
            }

            NetworkSend.SendHotbar(index);

            buffer.Dispose();
        }

        public static void Packet_DeleteHotbarSlot(int index, ref byte[] data)
        {
            int slot;
            var buffer = new ByteStream(data);

            slot = buffer.ReadInt32();

            if (slot < 0 | slot > Core.Constant.MAX_HOTBAR)
                return;

            Core.Data.Player[index].Hotbar[slot].Slot = -1;
            Core.Data.Player[index].Hotbar[slot].SlotType = 0;

            NetworkSend.SendHotbar(index);

            buffer.Dispose();
        }

        public static void Packet_UseHotbarSlot(int index, ref byte[] data)
        {
            int slot;
            var buffer = new ByteStream(data);

            slot = buffer.ReadInt32();
            buffer.Dispose();

            if (slot < 0 | slot > Core.Constant.MAX_HOTBAR)
                return;

            if (Core.Data.Player[index].Hotbar[slot].Slot >= 0)
            {
                if (Core.Data.Player[index].Hotbar[slot].SlotType == (byte)DraggablePartType.Item)
                {
                    Player.UseItem(index, Player.FindItemSlot(index, (int)Core.Data.Player[index].Hotbar[slot].Slot));
                }
            }

            NetworkSend.SendHotbar(index);
        }

        public static void Packet_SkillLearn(int index, ref byte[] data)
        {
            int skillNum;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Developer)
                return;

            skillNum = buffer.ReadInt32();

            try
            {
                Script.Instance?.LearnSkill(index, -1, skillNum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static void Packet_RequestEditJob(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Developer)
                return;

            string user;

            user = IsEditorLocked(index, (byte)EditorType.Job);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) Color.BrightRed);
                return;
            }

            Item.SendItems(index);
            NetworkSend.SendJobs(index);

            Core.Data.TempPlayer[index].Editor = (byte)EditorType.Job;

            NetworkSend.SendJobs(index);

            NetworkSend.SendJobEditor(index);
        }

        public static void Packet_SaveJob(int index, ref byte[] data)
        {
            int i;
            int z;
            int x;
            int jobNum;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            jobNum = buffer.ReadInt32();

            {
                ref var withBlock = ref Data.Job[jobNum];
                withBlock.Name = buffer.ReadString();
                withBlock.Desc = buffer.ReadString();

                withBlock.MaleSprite = buffer.ReadInt32();
                withBlock.FemaleSprite = buffer.ReadInt32();

				var loopTo = Enum.GetNames(typeof(Stat)).Length;
                for (x = 0; x < loopTo; x++)
                    withBlock.Stat[x] = buffer.ReadInt32();

                for (int q = 0; q < Core.Constant.MAX_START_ITEMS; q++)
                {
                    withBlock.StartItem[q] = buffer.ReadInt32();
                    withBlock.StartValue[q] = buffer.ReadInt32();
                }

                withBlock.StartMap = buffer.ReadInt32();
                withBlock.StartX = buffer.ReadByte();
                withBlock.StartY = (byte)buffer.ReadInt32();
                withBlock.BaseExp = buffer.ReadInt32();
            }

            buffer.Dispose();

            Database.SaveJob(jobNum);
            NetworkSend.SendJobToAll(index);
        }

        private static void Packet_Emote(int index, ref byte[] data)
        {
            int Emote;
            var buffer = new ByteStream(data);

            Emote = buffer.ReadInt32();

            NetworkSend.SendEmote(index, Emote);

            buffer.Dispose();
        }

        private static void Packet_CloseEditor(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Mapper)
                return;

            if (Core.Data.TempPlayer[index].Editor == -1)
                return;

            Core.Data.TempPlayer[index].Editor = -1;
        }

    }
}