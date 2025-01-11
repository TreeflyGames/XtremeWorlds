using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json.Linq;
using Npgsql.Replication.PgOutput.Messages;
using Server;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Packets;
using static Core.Type;

namespace Server
{

    static class NetworkReceive
    {
        internal static void PacketRouter()
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

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditMap] = Packet_EditMapRequest;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSetAccess] = Packet_SetAccess;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CWhosOnline] = Packet_WhosOnline;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSetMotd] = Packet_SetMotd;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSearch] = Packet_PlayerSearch;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSkills] = Packet_Skills;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CCast] = Packet_Cast;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CQuit] = Packet_QuitGame;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSwapInvSlots] = Packet_SwapInvSlots;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSwapSkillSlots] = Packet_SwapSkillSlots;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CCheckPing] = Packet_CheckPing;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CUnequip] = Packet_Unequip;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestPlayerData] = Packet_RequestPlayerData;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestItem] = Item.Packet_RequestItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestNPC] = Packet_RequestNPC;
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

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestPets] = Pet.Packet_RequestPets;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSummonPet] = Pet.Packet_SummonPet;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CPetMove] = Pet.Packet_PetMove;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSetBehaviour] = Pet.Packet_SetPetBehaviour;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CReleasePet] = Pet.Packet_ReleasePet;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CPetSkill] = Pet.Packet_PetSkill;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CPetUseStatPoint] = Pet.Packet_UsePetStatPoint;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestPet] = Pet.Packet_RequestPet;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditItem] = Item.Packet_EditItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveItem] = Item.Packet_SaveItem;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditNPC] = NPC.Packet_EditNPC;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveNPC] = NPC.Packet_SaveNPC;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditShop] = Packet_EditShop;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveShop] = Packet_SaveShop;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditSkill] = Packet_EditSkill;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveSkill] = Packet_SaveSkill;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditResource] = Resource.Packet_EditResource;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveResource] = Resource.Packet_SaveResource;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditAnimation] = Animation.Packet_EditAnimation;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveAnimation] = Animation.Packet_SaveAnimation;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditProjectile] = Projectile.HandleRequestEditProjectile;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveProjectile] = Projectile.HandleSaveProjectile;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditJob] = Packet_RequestEditJob;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveJob] = Packet_SaveJob;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditPet] = Pet.Packet_RequestEditPet;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSavePet] = Pet.Packet_SavePet;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestMoral] = Moral.Packet_RequestMoral;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CRequestEditMoral] = Moral.Packet_RequestEditMoral;
            NetworkConfig.Socket.PacketID[(int)ClientPackets.CSaveMoral] = Moral.Packet_SaveMoral;

            NetworkConfig.Socket.PacketID[(int)ClientPackets.CCloseEditor] = Packet_CloseEditor;

        }

        private static void Packet_Ping(int index, ref byte[] data)
        {
            Core.Type.TempPlayer[index].DataPackets = Core.Type.TempPlayer[index].DataPackets + 1;
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

                    for (i = Strings.Len(IP); i >= 0; i -= 1)
                    {

                        if (Strings.Mid(IP, i, 1) == ".")
                        {
                            break;
                        }

                    }

                    IP = Strings.Mid(IP, 1, i);

                    if (General.shutDownDuration > 0)
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.Maintenance, (byte)MenuType.Login);
                        return;
                    }

                    // Get the data
                    username = Global.EKeyPair.DecryptString(buffer.ReadString()).ToLower().Replace("\0", "");
                    password = Global.EKeyPair.DecryptString(buffer.ReadString()).Replace("\0", "");

                    // Get the current executing assembly
                    var @assembly = Assembly.GetExecutingAssembly();

                    // Retrieve the version information
                    var version = assembly.GetName().Version;

                    // Check versions
                    if ((Global.EKeyPair.DecryptString(buffer.ReadString()) ?? "") != (version.ToString() ?? ""))
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.Outdated, (byte)MenuType.Login);
                        return;
                    }

                    if (username.Length > Core.Constant.NAME_LENGTH | username.Length < Core.Constant.MIN_NAME_LENGTH)
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.NameLength);
                        return;
                    }

                    if (NetworkConfig.IsMultiAccounts(index, username))
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.MultiAccount, (byte)MenuType.Login);
                        return;
                    }

                    NetworkConfig.CheckMultiAccounts(index, username);

                    if (!Database.LoadAccount(index, username))
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.Login, (byte)MenuType.Login);
                        return;
                    }

                    if (GetPlayerPassword(index) != password)
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.WrongPass, (byte)MenuType.Login);
                        return;
                    }

                    if (Database.IsBanned(index, IP))
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.Banned, (byte)MenuType.Login);
                        return;
                    }

                    if (GetPlayerLogin(index) == "")
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.Database, (byte)MenuType.Login);
                        return;
                    }

                    // Show the player up on the socket status
                    Log.Add(GetPlayerLogin(index) + " has logged in from " + NetworkConfig.Socket.ClientIP(index) + ".", Constant.PLAYER_LOG);
                    Console.WriteLine(GetPlayerLogin(index) + " has logged in from " + NetworkConfig.Socket.ClientIP(index) + ".");

                    // send them to the character portal
                    NetworkSend.SendPlayerChars(index);
                    NetworkSend.SendJobs(index);
                }
            }
            else
            {
                NetworkSend.AlertMsg(index, (byte)DialogueMsg.Connection, (byte)MenuType.Login);
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

                    for (i = Strings.Len(IP); i >= 0; i -= 1)
                    {

                        if (Strings.Mid(IP, i, 1) == ".")
                        {
                            break;
                        }

                    }

                    IP = Strings.Mid(IP, 1, i);

                    if (Database.IsBanned(index, IP))
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.Banned, (byte)MenuType.Register);
                        return;
                    }

                    if (General.shutDownTimer.IsRunning)
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.Maintenance, (byte)MenuType.Register);
                        return;
                    }

                    // Get the data
                    username = Global.EKeyPair.DecryptString(buffer.ReadString()).ToLower().Replace("\0", "");
                    password = Global.EKeyPair.DecryptString(buffer.ReadString()).Replace("\0", "");

                    var loopTo = Strings.Len(username);
                    for (i = 1; i < loopTo; i++)
                    {
                        n = Strings.AscW(Strings.Mid(username, i, 1));

                        if (!General.IsNameLegal(n.ToString()))
                        {
                            NetworkSend.AlertMsg(index, (byte)DialogueMsg.NameIllegal, (byte)MenuType.Register);
                            return;
                        }

                    }

                    // Get the current executing assembly
                    var @assembly = Assembly.GetExecutingAssembly();

                    // Retrieve the version information
                    var version = assembly.GetName().Version;

                    // Check versions
                    if ((Global.EKeyPair.DecryptString(buffer.ReadString()) ?? "") != (version.ToString() ?? ""))
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.Outdated, (byte)MenuType.Register);
                        return;
                    }

                    if (username.Length > Core.Constant.NAME_LENGTH | username.Length < Core.Constant.MIN_NAME_LENGTH)
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.NameLength, (byte)MenuType.Register);
                        return;
                    }

                    if (NetworkConfig.IsMultiAccounts(index, username))
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.MultiAccount, (byte)MenuType.Register);
                        return;
                    }

                    NetworkConfig.CheckMultiAccounts(index, username);

                    userData = Database.SelectRowByColumn("id", Database.GetStringHash(username), "account", "data");

                    if (userData is not null)
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.NameTaken, (byte)MenuType.Register);
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
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.MaxChar, (byte)MenuType.Chars);
                        return;
                    }

                    Database.LoadCharacter(index, slot);
                    Database.LoadBank(index);

                    // Check if character data has been created
                    if (Strings.Len(Core.Type.Player[index].Name) > 0)
                    {
                        // we have a char!
                        Core.Type.TempPlayer[index].Slot = slot;
                        Player.HandleUseChar(index);
                    }
                    else
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.Database, (byte)MenuType.Chars);
                        return;
                    }
                }
            }
            else
            {
                NetworkSend.AlertMsg(index, (byte)DialogueMsg.Connection, (byte)MenuType.Login);
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
                    NetworkSend.AlertMsg(index, (byte)DialogueMsg.MaxChar, (byte)MenuType.Chars);
                    return;
                }

                if (Database.LoadCharacter(index, slot))
                {
                    NetworkSend.SendPlayerChars(index);
                    return;
                }

                if (name.Length > Core.Constant.NAME_LENGTH | name.Length < Core.Constant.MIN_NAME_LENGTH)
                {
                    NetworkSend.AlertMsg(index, (byte)DialogueMsg.NameLength, (byte)MenuType.NewChar);
                    return;
                }

                var loopTo = Strings.Len(name);
                for (i = 1; i < loopTo; i++)
                {
                    n = Strings.AscW(Strings.Mid(name, i, 1));

                    if (!General.IsNameLegal(n.ToString()))
                    {
                        NetworkSend.AlertMsg(index, (byte)DialogueMsg.NameIllegal, (byte)MenuType.NewChar);
                        return;
                    }
                }

                // Check if name is already in use
                if (Core.Type.Char.Find(name))
                {
                    NetworkSend.AlertMsg(index, (byte)DialogueMsg.NameTaken, (byte)MenuType.NewChar);
                    return;
                }

                if (sexNum < (byte) SexType.Male | sexNum > (byte) SexType.Female)
                    return;

                if (jobNum < 0 | jobNum > Core.Constant.MAX_JOBS)
                    return;

                if (sexNum == (byte) SexType.Male)
                {
                    sprite = Core.Type.Job[jobNum].MaleSprite;
                }
                else
                {
                    sprite = Core.Type.Job[jobNum].FemaleSprite;
                }

                if (sprite == 0)
                {
                    sprite = 1;
                }

                // Everything went ok, add the character
                Core.Type.Char.Add(name);
                Database.AddChar(index, slot, name, (byte)sexNum, (byte)jobNum, sprite);
                Log.Add("Character " + name + " added to " + GetPlayerLogin(index) + "'s account.", Constant.PLAYER_LOG);
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
                    NetworkSend.AlertMsg(index, (byte)DialogueMsg.MaxChar, (byte)MenuType.Chars);
                    return;
                }

                Database.LoadCharacter(index, slot);
                Core.Type.Char.Remove(GetPlayerName(index));
                Database.ClearCharacter(index);
                Database.SaveCharacter(index, slot);

                // send them to the character portal
                NetworkSend.SendPlayerChars(index);

                buffer.Dispose();
            }
            else
            {
                NetworkSend.AlertMsg(index, (byte)DialogueMsg.Connection, (byte)MenuType.Login);
            }
        }

        private static void Packet_SayMessage(int index, ref byte[] data)
        {
            string msg;
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();

            Log.Add("Map #" + GetPlayerMap(index) + ": " + GetPlayerName(index) + " says, '" + msg + "'", Constant.PLAYER_LOG);

            NetworkSend.SayMsg_Map(GetPlayerMap(index), index, msg, (int) ColorType.White);
            NetworkSend.SendChatBubble(GetPlayerMap(index), index, (int)TargetType.Player, msg, (int) ColorType.White);

            buffer.Dispose();
        }

        private static void Packet_BroadCastMsg(int index, ref byte[] data)
        {
            string msg;
            string s;
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();

            s = "[Global] " + GetPlayerName(index) + ": " + msg;
            NetworkSend.SayMsg_Global(index, msg, (int) ColorType.White);
            Log.Add(s, Constant.PLAYER_LOG);
            Console.WriteLine(s);

            buffer.Dispose();
        }

        internal static void Packet_PlayerMsg(int index, ref byte[] data)
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
                    NetworkSend.PlayerMsg(OtherPlayerindex, GetPlayerName(index) + " tells you, '" + Msg + "'", (int) ColorType.Pink);
                    NetworkSend.PlayerMsg(index, "You tell " + GetPlayerName(OtherPlayerindex) + ", '" + Msg + "'", (int) ColorType.Pink);
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) ColorType.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Cannot message your self!", (int) ColorType.BrightRed);
            }
        }

        private static void Packet_AdminMsg(int index, ref byte[] data)
        {
            string msg;
            var s = default(string);
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();

            NetworkSend.AdminMsg(GetPlayerMap(index), msg, (int) ColorType.BrightCyan);
            Log.Add(s, Constant.PLAYER_LOG);
            Console.WriteLine(s);

            buffer.Dispose();
        }

        private static void Packet_PlayerMove(int index, ref byte[] data)
        {
            int Dir;
            int movement;
            int tmpX;
            int tmpY;
            var buffer = new ByteStream(data);

            if (Core.Type.TempPlayer[index].GettingMap)
                return;

            Dir = buffer.ReadInt32();
            movement = buffer.ReadInt32();
            tmpX = buffer.ReadInt32();
            tmpY = buffer.ReadInt32();
            buffer.Dispose();

            // Prevent player from moving if they have casted a skill
            if (Core.Type.TempPlayer[index].SkillBuffer >= 0)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            // Cant move if in the bank!
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

            // Prevent player from moving if in shop
            if (Core.Type.TempPlayer[index].InShop >= 0)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            // Desynced
            int x = GetPlayerX(index);
            if (x != tmpX)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            int y = GetPlayerY(index);
            if (y != tmpY)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            Player.PlayerMove(index, Dir, movement, false);

            buffer.Dispose();
        }

        public static void Packet_PlayerDirection(int index, ref byte[] data)
        {
            int dir;
            var buffer = new ByteStream(data);

            if (Core.Type.TempPlayer[index].GettingMap == true)
                return;

            dir = buffer.ReadInt32();
            buffer.Dispose();

            // Prevent hacking
            if (dir < (byte) DirectionType.Up | dir > (byte) DirectionType.Left)
                return;

            SetPlayerDir(index, dir);

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SPlayerDir);
            buffer.WriteInt32(index);
            buffer.WriteInt32(GetPlayerDir(index));
            NetworkConfig.SendDataToMapBut(index, GetPlayerMap(index), ref buffer.Data, buffer.Head);

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
            if (Core.Type.TempPlayer[index].SkillBuffer >= 0)
                return;

            // can't attack whilst stunned
            if (Core.Type.TempPlayer[index].StunDuration > 0)
                return;

            // Send this packet so they can see the person attacking
            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SAttack);
            buffer.WriteInt32(index);
            NetworkConfig.SendDataToMap(GetPlayerMap(index), ref buffer.Data, buffer.Head);
            buffer.Dispose();

            // Projectile check
            if (GetPlayerEquipment(index, EquipmentType.Weapon) >= 0)
            {
                if (Core.Type.Item[(int)GetPlayerEquipment(index, EquipmentType.Weapon)].Projectile > 0) // Item has a projectile
                {
                    if (Core.Type.Item[(int)GetPlayerEquipment(index, EquipmentType.Weapon)].Ammo > 0)
                    {
                        if (Conversions.ToBoolean(Player.HasItem(index, Core.Type.Item[(int)GetPlayerEquipment(index, EquipmentType.Weapon)].Ammo)))
                        {
                            Player.TakeInv(index, Core.Type.Item[(int)GetPlayerEquipment(index, EquipmentType.Weapon)].Ammo, 1);
                            Projectile.PlayerFireProjectile(index);
                            return;
                        }
                        else
                        {
                            NetworkSend.PlayerMsg(index, "No More " + Core.Type.Item[Core.Type.Item[(int)GetPlayerEquipment(index, EquipmentType.Weapon)].Ammo].Name + " !", (int) ColorType.BrightRed);
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

            // Try to attack a player
            var loopTo = NetworkConfig.Socket.HighIndex + 1;
            for (i = 0; i < loopTo; i++)
            {
                Tempindex = i;

                // Make sure we dont try to attack ourselves
                if (Tempindex != index)
                {
                    if (NetworkConfig.IsPlaying(Tempindex))
                    {
                        Player.TryPlayerAttackPlayer(index, i);
                    }
                }
            }

            // Try to attack a npc
            var loopTo1 = Core.Constant.MAX_MAP_NPCS;
            for (i = 0; i < loopTo1; i++)
                Player.TryPlayerAttackNPC(index, i);

            // Check tradeskills
            switch (GetPlayerDir(index))
            {
                case var @case when @case == (byte) DirectionType.Up:
                    {

                        if (GetPlayerY(index) == 0)
                            return;
                        x = GetPlayerX(index);
                        y = GetPlayerY(index) - 1;
                        break;
                    }
                case var case1 when case1 == (byte) DirectionType.Down:
                    {

                        if (GetPlayerY(index) == Core.Type.Map[GetPlayerMap(index)].MaxY)
                            return;
                        x = GetPlayerX(index);
                        y = GetPlayerY(index) + 1;
                        break;
                    }
                case var case2 when case2 == (byte) DirectionType.Left:
                    {

                        if (GetPlayerX(index) == 0)
                            return;
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index);
                        break;
                    }
                case var case3 when case3 == (byte) DirectionType.Right:
                    {

                        if (GetPlayerX(index) == Core.Type.Map[GetPlayerMap(index)].MaxX)
                            return;
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index);
                        break;
                    }

                case var case4 when case4 == (byte) DirectionType.UpRight:
                    {

                        if (GetPlayerX(index) == Core.Type.Map[GetPlayerMap(index)].MaxX)
                            return;
                        if (GetPlayerY(index) == Core.Type.Map[GetPlayerMap(index)].MaxY)
                            return;
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index) - 1;
                        break;
                    }

                case var case5 when case5 == (byte) DirectionType.UpLeft:
                    {

                        if (GetPlayerX(index) == Core.Type.Map[GetPlayerMap(index)].MaxX)
                            return;
                        if (GetPlayerY(index) == Core.Type.Map[GetPlayerMap(index)].MaxY)
                            return;
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index) - 1;
                        break;
                    }

                case var case6 when case6 == (byte) DirectionType.DownRight:
                    {

                        if (GetPlayerX(index) == Core.Type.Map[GetPlayerMap(index)].MaxX)
                            return;
                        if (GetPlayerY(index) == Core.Type.Map[GetPlayerMap(index)].MaxY)
                            return;
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index) + 1;
                        break;
                    }

                case var case7 when case7 == (byte) DirectionType.DownLeft:
                    {

                        if (GetPlayerX(index) == Core.Type.Map[GetPlayerMap(index)].MaxX)
                            return;
                        if (GetPlayerY(index) == Core.Type.Map[GetPlayerMap(index)].MaxY)
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
                NetworkSend.PlayerMsg(index, "Account:  " + GetPlayerLogin(i) + ", Name: " + GetPlayerName(i), (int) ColorType.Yellow);

                if (GetPlayerAccess(index) > (byte)AccessType.Moderator)
                {
                    NetworkSend.PlayerMsg(index, " Stats for " + GetPlayerName(i) + " ", (int) ColorType.Yellow);
                    NetworkSend.PlayerMsg(index, "Level: " + GetPlayerLevel(i) + "  Exp: " + GetPlayerExp(i) + "/" + GetPlayerNextLevel(i), (int) ColorType.Yellow);
                    NetworkSend.PlayerMsg(index, "HP: " + GetPlayerVital(i, VitalType.HP) + "/" + GetPlayerMaxVital(i, VitalType.HP) + "  MP: " + GetPlayerVital(i, VitalType.SP) + "/" + GetPlayerMaxVital(i, VitalType.SP) + "  SP: " + GetPlayerVital(i, VitalType.SP) + "/" + GetPlayerMaxVital(i, VitalType.SP), (int) ColorType.Yellow);
                    NetworkSend.PlayerMsg(index, "Strength: " + GetPlayerStat(i, StatType.Strength) + "  Defense: " + GetPlayerStat(i, StatType.Luck) + "  Magic: " + GetPlayerStat(i, StatType.Intelligence) + "  Speed: " + GetPlayerStat(i, StatType.Spirit), (int) ColorType.Yellow);
                    n = GetPlayerStat(i, StatType.Strength) / 2 + GetPlayerLevel(i) / 2;
                    i = GetPlayerStat(i, StatType.Luck) / 2 + GetPlayerLevel(i) / 2;

                    if (n > 100)
                        n = 100;
                    if (i > 100)
                        i = 100;
                    NetworkSend.PlayerMsg(index, "Critical Hit Chance: " + n + "%, Block Chance: " + i + "%", (int) ColorType.Yellow);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Player is not online.", (int) ColorType.BrightRed);
            }

            buffer.Dispose();
        }

        public static void Packet_WarpMeTo(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Mapper)
                return;

            // The player
            n = GameLogic.FindPlayer(buffer.ReadString());
            buffer.Dispose();

            if (n != index)
            {
                if (n >= 0)
                {
                    Player.PlayerWarp(index, GetPlayerMap(n), GetPlayerX(n), GetPlayerY(n));
                    NetworkSend.PlayerMsg(n, GetPlayerName(index) + " has warped to you.", (int) ColorType.Yellow);
                    NetworkSend.PlayerMsg(index, "You have been warped to " + GetPlayerName(n) + ".", (int) ColorType.Yellow);
                    Log.Add(GetPlayerName(index) + " has warped to " + GetPlayerName(n) + ", map #" + GetPlayerMap(n) + ".", Constant.ADMIN_LOG);
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) ColorType.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You cannot warp to yourself, dumbass!", (int) ColorType.BrightRed);
            }

        }

        public static void Packet_WarpToMe(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Mapper)
                return;

            // The player
            n = GameLogic.FindPlayer(buffer.ReadString());
            buffer.Dispose();

            if (n != index)
            {
                if (n >= 0)
                {
                    Player.PlayerWarp(n, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index));
                    NetworkSend.PlayerMsg(n, "You have been summoned by " + GetPlayerName(index) + ".", (int) ColorType.Yellow);
                    NetworkSend.PlayerMsg(index, GetPlayerName(n) + " has been summoned.", (int) ColorType.Yellow);
                    Log.Add(GetPlayerName(index) + " has warped " + GetPlayerName(n) + " to self, map #" + GetPlayerMap(index) + ".", Constant.ADMIN_LOG);
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) ColorType.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You cannot warp yourself to yourself, dumbass!", (int) ColorType.BrightRed);
            }

        }

        public static void Packet_WarpTo(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Mapper)
                return;

            // The map
            n = buffer.ReadInt32();
            buffer.Dispose();

            // Prevent hacking
            if (n < 0 | n > Core.Constant.MAX_MAPS)
                return;

            Player.PlayerWarp(index, n, GetPlayerX(index), GetPlayerY(index));
            NetworkSend.PlayerMsg(index, "You have been warped to map #" + n, (int) ColorType.Yellow);
            Log.Add(GetPlayerName(index) + " warped to map #" + n + ".", Constant.ADMIN_LOG);
        }

        public static void Packet_SetSprite(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Mapper)
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

            NetworkSend.PlayerMsg(index, "Stats: " + GetPlayerName(index), (int) ColorType.Yellow);
            NetworkSend.PlayerMsg(index, "Level: " + GetPlayerLevel(index) + "  Exp: " + GetPlayerExp(index) + "/" + GetPlayerNextLevel(index), (int) ColorType.Yellow);
            NetworkSend.PlayerMsg(index, "HP: " + GetPlayerVital(index, VitalType.HP) + "/" + GetPlayerMaxVital(index, VitalType.HP) + "  MP: " + GetPlayerVital(index, VitalType.SP) + "/" + GetPlayerMaxVital(index, VitalType.SP) + "  SP: " + GetPlayerVital(index, VitalType.SP) + "/" + GetPlayerMaxVital(index, VitalType.SP), (int) ColorType.Yellow);
            NetworkSend.PlayerMsg(index, "STR: " + GetPlayerStat(index, StatType.Strength) + "  DEF: " + GetPlayerStat(index, StatType.Luck) + "  MAGI: " + GetPlayerStat(index, StatType.Intelligence) + "  Speed: " + GetPlayerStat(index, StatType.Spirit), (int) ColorType.Yellow);
            n = GetPlayerStat(index, StatType.Strength) / 2 + GetPlayerLevel(index) / 2;
            i = GetPlayerStat(index, StatType.Luck) / 2 + GetPlayerLevel(index) / 2;

            if (n > 100)
                n = 100;
            if (i > 100)
                i = 100;
            NetworkSend.PlayerMsg(index, "Critical Hit Chance: " + n + "%, Block Chance: " + i + "%", (int) ColorType.Yellow);
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
            if (GetPlayerAccess(index) < (byte) AccessType.Mapper)
                return;

            var buffer = new ByteStream(Mirage.Sharp.Asfw.IO.Compression.DecompressBytes(data));

            mapNum = GetPlayerMap(index);

            i = Core.Type.Map[mapNum].Revision + 1;
            Database.ClearMap(mapNum);

            Core.Type.Map[mapNum].Name = buffer.ReadString();
            Core.Type.Map[mapNum].Music = buffer.ReadString();
            Core.Type.Map[mapNum].Revision = i;
            Core.Type.Map[mapNum].Moral = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].Tileset = buffer.ReadInt32();
            Core.Type.Map[mapNum].Up = buffer.ReadInt32();
            Core.Type.Map[mapNum].Down = buffer.ReadInt32();
            Core.Type.Map[mapNum].Left = buffer.ReadInt32();
            Core.Type.Map[mapNum].Right = buffer.ReadInt32();
            Core.Type.Map[mapNum].BootMap = buffer.ReadInt32();
            Core.Type.Map[mapNum].BootX = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].BootY = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].MaxX = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].MaxY = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].Weather = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].Fog = buffer.ReadInt32();
            Core.Type.Map[mapNum].WeatherIntensity = buffer.ReadInt32();
            Core.Type.Map[mapNum].FogOpacity = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].FogSpeed = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].MapTint = Conversions.ToBoolean(buffer.ReadInt32());
            Core.Type.Map[mapNum].MapTintR = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].MapTintG = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].MapTintB = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].MapTintA = (byte)buffer.ReadInt32();
            Core.Type.Map[mapNum].Panorama = buffer.ReadByte();
            Core.Type.Map[mapNum].Parallax = buffer.ReadByte();
            Core.Type.Map[mapNum].Brightness = buffer.ReadByte();
            Core.Type.Map[mapNum].NoRespawn = Conversions.ToBoolean(buffer.ReadInt32());
            Core.Type.Map[mapNum].Indoors = Conversions.ToBoolean(buffer.ReadInt32());
            Core.Type.Map[mapNum].Shop = buffer.ReadInt32();

            Core.Type.Map[mapNum].Tile = new Core.Type.TileStruct[(Core.Type.Map[mapNum].MaxX), (Core.Type.Map[mapNum].MaxY)];

            var loopTo = Core.Constant.MAX_MAP_NPCS;
            for (x = 0; x < (int)loopTo; x++)
            {
                Database.ClearMapNPC(x, mapNum);
                Core.Type.Map[mapNum].NPC[x] = buffer.ReadInt32();
            }

            {
                ref var withBlock = ref Core.Type.Map[mapNum];
                var loopTo1 = (int)withBlock.MaxX;
                for (x = 0; x < (int)loopTo1; x++)
                {
                    var loopTo2 = (int)withBlock.MaxY;
                    for (y = 0; y < (int)loopTo2; y++)
                    {
                        withBlock.Tile[x, y].Data1 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data2 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data3 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data1_2 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data2_2 = buffer.ReadInt32();
                        withBlock.Tile[x, y].Data3_2 = buffer.ReadInt32();
                        withBlock.Tile[x, y].DirBlock = (byte)buffer.ReadInt32();
                        withBlock.Tile[x, y].Layer = new Core.Type.TileDataStruct[(int)LayerType.Count];
                        var loopTo3 = LayerType.Count;
                        for (i = 0; i < (int)loopTo3; i++)
                        {
                            withBlock.Tile[x, y].Layer[i].Tileset = buffer.ReadInt32();
                            withBlock.Tile[x, y].Layer[i].X = buffer.ReadInt32();
                            withBlock.Tile[x, y].Layer[i].Y = buffer.ReadInt32();
                            withBlock.Tile[x, y].Layer[i].AutoTile = (byte)buffer.ReadInt32();
                        }
                        withBlock.Tile[x, y].Type = (Core.Enum.TileType)buffer.ReadInt32();
                        withBlock.Tile[x, y].Type2 = (Core.Enum.TileType)buffer.ReadInt32();
                    }
                }

            }

            Core.Type.Map[mapNum].EventCount = buffer.ReadInt32();

            if (Core.Type.Map[mapNum].EventCount > 0)
            {
                Core.Type.Map[mapNum].Event = new Core.Type.EventStruct[Core.Type.Map[mapNum].EventCount];
                var loopTo4 = Core.Type.Map[mapNum].EventCount;
                for (i = 0; i < loopTo4; i++)
                {
                    {
                        ref var withBlock1 = ref Core.Type.Map[mapNum].Event[i];
                        withBlock1.Name = buffer.ReadString();
                        withBlock1.Globals = buffer.ReadByte();
                        withBlock1.X = buffer.ReadInt32();
                        withBlock1.Y = buffer.ReadInt32();
                        withBlock1.PageCount = buffer.ReadInt32();
                    }

                    if (Core.Type.Map[mapNum].Event[i].PageCount > 0)
                    {
                        Core.Type.Map[mapNum].Event[i].Pages = new Core.Type.EventPageStruct[Core.Type.Map[mapNum].Event[i].PageCount];
                        ;
                        Array.Resize(ref Core.Type.TempPlayer[i].EventMap.EventPages, Core.Type.Map[mapNum].Event[i].PageCount);

                        var loopTo5 = Core.Type.Map[mapNum].Event[i].PageCount;
                        for (x = 0; x < (int)loopTo5; x++)
                        {
                            {
                                ref var withBlock2 = ref Core.Type.Map[mapNum].Event[i].Pages[x];
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
                                    Core.Type.Map[mapNum].Event[i].Pages[x].MoveRoute = new Core.Type.MoveRouteStruct[withBlock2.MoveRouteCount];
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

                            if (Core.Type.Map[mapNum].Event[i].Pages[x].CommandListCount > 0)
                            {
                                Core.Type.Map[mapNum].Event[i].Pages[x].CommandList = new Core.Type.CommandListStruct[Core.Type.Map[mapNum].Event[i].Pages[x].CommandListCount];
                                var loopTo7 = Core.Type.Map[mapNum].Event[i].Pages[x].CommandListCount;
                                for (y = 0; y < (int)loopTo7; y++)
                                {
                                    Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount = buffer.ReadInt32();
                                    Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].ParentList = buffer.ReadInt32();
                                    if (Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                    {
                                        Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].Commands = new Core.Type.EventCommandStruct[Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount];
                                        for (int z = 0, loopTo8 = Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].CommandCount; z < (int)loopTo8; z++)
                                        {
                                            {
                                                ref var withBlock3 = ref Core.Type.Map[mapNum].Event[i].Pages[x].CommandList[y].Commands[z];
                                                withBlock3.Index = buffer.ReadByte();
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

            // Save the map
            Database.SaveMap(mapNum);
            NPC.SendMapNPCsToMap(mapNum);
            NPC.SpawnMapNPCs(mapNum);
            EventLogic.SpawnGlobalEvents(mapNum);

            var loopTo10 = NetworkConfig.Socket.HighIndex + 1;
            for (i = 0; i < loopTo10; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    if (Core.Type.Player[i].Map == mapNum)
                    {
                        EventLogic.SpawnMapEventsFor(i, mapNum);
                    }
                }
            }

            // Clear it all out
            var loopTo11 = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo11; i++)
            {
                Item.SpawnItemSlot(i, 0, 0, GetPlayerMap(index), MapItem[GetPlayerMap(index), i].X, MapItem[GetPlayerMap(index), i].Y);
                Database.ClearMapItem(i, GetPlayerMap(index));
            }

            // Respawn
            Item.SpawnMapItems(GetPlayerMap(index));
            Resource.CacheResources(mapNum);

            // Refresh map for everyone online
            var loopTo12 = NetworkConfig.Socket.HighIndex + 1;
            for (i = 0; i < loopTo12; i++)
            {
                if (NetworkConfig.IsPlaying(i) & GetPlayerMap(i) == mapNum)
                {
                    Player.PlayerWarp(i, mapNum, GetPlayerX(i), GetPlayerY(i));
                    // Send map
                    NetworkSend.SendMapData(i, mapNum, true);
                }
            }

            buffer.Dispose();
        }

        private static void Packet_NeedMap(int index, ref byte[] data)
        {
            string s;
            var buffer = new ByteStream(data);

            // Get yes/no value
            s = buffer.ReadInt32().ToString();
            buffer.Dispose();

            // Check if data is needed to be sent
            if (Conversions.ToDouble(s) == 1d)
            {
                NetworkSend.SendMapData(index, GetPlayerMap(index), true);
            }
            else
            {
                NetworkSend.SendMapData(index, GetPlayerMap(index), false);
            }

            if (Core.Type.Map[GetPlayerMap(index)].Shop > 0)
            {
                if (!string.IsNullOrEmpty(Core.Type.Shop[Core.Type.Map[GetPlayerMap(index)].Shop].Name))
                {
                    Core.Type.TempPlayer[index].InShop = Core.Type.Map[GetPlayerMap(index)].Shop;
                    NetworkSend.SendOpenShop(index, Core.Type.Map[GetPlayerMap(index)].Shop);
                }
            }

            EventLogic.SpawnMapEventsFor(index, GetPlayerMap(index));
            NetworkSend.SendJoinMap(index);
            Core.Type.TempPlayer[index].GettingMap = false;
        }

        public static void Packet_RespawnMap(int index, ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Mapper)
                return;

            // Clear out it all
            var loopTo = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                Item.SpawnItemSlot(i, 0, 0, GetPlayerMap(index), MapItem[GetPlayerMap(index), i].X, MapItem[GetPlayerMap(index), i].Y);
                Database.ClearMapItem(i, GetPlayerMap(index));
            }

            // Respawn
            Item.SpawnMapItems(GetPlayerMap(index));

            // Respawn NPCS
            var loopTo1 = Core.Constant.MAX_MAP_NPCS;
            for (i = 0; i < loopTo1; i++)
                NPC.SpawnNPC(i, GetPlayerMap(index));

            EventLogic.SpawnMapEventsFor(index, GetPlayerMap(index));
            EventLogic.SpawnGlobalEvents(GetPlayerMap(index));

            Resource.CacheResources(GetPlayerMap(index));
            NetworkSend.PlayerMsg(index, "Map respawned.", (int) ColorType.BrightGreen);
            Log.Add(GetPlayerName(index) + " has respawned map #" + GetPlayerMap(index), Constant.ADMIN_LOG);

            buffer.Dispose();
        }

        public static void Packet_KickPlayer(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Moderator)
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
                        NetworkSend.GlobalMsg(GetPlayerName(n) + " has been kicked from " + Settings.Instance.GameName + " by " + GetPlayerName(index) + "!");
                        Log.Add(GetPlayerName(index) + " has kicked " + GetPlayerName(n) + ".", Constant.ADMIN_LOG);
                        NetworkSend.AlertMsg(n, (byte)DialogueMsg.Kicked, (byte)MenuType.Login);
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "That is a higher or same access admin then you!", (int) ColorType.BrightRed);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) ColorType.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You cannot kick yourself!", (int) ColorType.BrightRed);
            }
        }

        public static void Packet_Banlist(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Moderator)
            {
                return;
            }

            NetworkSend.PlayerMsg(index, "Command /banlist is not available.", (int) ColorType.Yellow);
        }

        public static void Packet_DestroyBans(int index, ref byte[] data)
        {
            string filename;

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Owner)
                return;

            filename = System.IO.Path.Combine(Core.Path.Database, "banlist.txt");

            if (File.Exists(filename))
                FileSystem.Kill(filename);

            NetworkSend.PlayerMsg(index, "Ban list destroyed.", (int) ColorType.BrightGreen);
        }

        public static void Packet_BanPlayer(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Moderator)
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
                        Database.Banindex(n, index);
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "That is a higher or same access admin then you!", (int) ColorType.BrightRed);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) ColorType.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You cannot ban yourself!", (int) ColorType.BrightRed);
            }

        }

        private static void Packet_EditMapRequest(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Mapper)
                return;

            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            if (GetPlayerMap(index) > Core.Constant.MAX_MAPS)
            {
                NetworkSend.PlayerMsg(index, "Cant edit instanced maps!", (int) ColorType.BrightRed);
                return;
            }

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Map);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            NPC.SendNPCs(index);
            Item.SendItems(index);
            Animation.SendAnimations(index);
            NetworkSend.SendShops(index);
            Resource.SendResources(index);
            Event.SendMapEventData(index);
            Moral.SendMorals(index);

            Core.Type.TempPlayer[index].Editor = (byte) EditorType.Map;

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SEditMap);

            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
            buffer.Dispose();
        }

        public static void Packet_EditShop(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;
            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Shop);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            Core.Type.TempPlayer[index].Editor = (byte) EditorType.Shop;

            Item.SendItems(index);
            NetworkSend.SendShops(index);

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SShopEditor);
            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);

            buffer.Dispose();
        }

        public static void Packet_SaveShop(int index, ref byte[] data)
        {
            int ShopNum;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            ShopNum = buffer.ReadInt32();

            // Prevent hacking
            if (ShopNum < 0 | ShopNum > Core.Constant.MAX_SHOPS)
                return;

            Core.Type.Shop[ShopNum].BuyRate = buffer.ReadInt32();
            Core.Type.Shop[ShopNum].Name = buffer.ReadString();

            for (int i = 0, loopTo = Core.Constant.MAX_TRADES; i < loopTo; i++)
            {
                Core.Type.Shop[ShopNum].TradeItem[Conversions.ToInteger(i)].CostItem = buffer.ReadInt32();
                Core.Type.Shop[ShopNum].TradeItem[Conversions.ToInteger(i)].CostValue = buffer.ReadInt32();
                Core.Type.Shop[ShopNum].TradeItem[Conversions.ToInteger(i)].Item = buffer.ReadInt32();
                Core.Type.Shop[ShopNum].TradeItem[Conversions.ToInteger(i)].ItemValue = buffer.ReadInt32();
            }

            buffer.Dispose();

            // Save it
            NetworkSend.SendUpdateShopToAll(ShopNum);
            Database.SaveShop(ShopNum);
            Log.Add(GetPlayerLogin(index) + " saving shop #" + ShopNum + ".", Constant.ADMIN_LOG);
        }

        public static void Packet_EditSkill(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;
            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Skill);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            Core.Type.TempPlayer[index].Editor = (byte) EditorType.Skill;

            NetworkSend.SendJobs(index);
            Projectile.SendProjectiles(index);
            Animation.SendAnimations(index);
            NetworkSend.SendSkills(index);

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SSkillEditor);
            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);

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

            Core.Type.Skill[skillNum].AccessReq = buffer.ReadInt32();
            Core.Type.Skill[skillNum].AoE = buffer.ReadInt32();
            Core.Type.Skill[skillNum].CastAnim = buffer.ReadInt32();
            Core.Type.Skill[skillNum].CastTime = buffer.ReadInt32();
            Core.Type.Skill[skillNum].CdTime = buffer.ReadInt32();
            Core.Type.Skill[skillNum].JobReq = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Dir = (byte)buffer.ReadInt32();
            Core.Type.Skill[skillNum].Duration = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Icon = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Interval = buffer.ReadInt32();
            Core.Type.Skill[skillNum].IsAoE = Conversions.ToBoolean(buffer.ReadInt32());
            Core.Type.Skill[skillNum].LevelReq = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Map = buffer.ReadInt32();
            Core.Type.Skill[skillNum].MpCost = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Name = buffer.ReadString();
            Core.Type.Skill[skillNum].Range = buffer.ReadInt32();
            Core.Type.Skill[skillNum].SkillAnim = buffer.ReadInt32();
            Core.Type.Skill[skillNum].StunDuration = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Type = (byte)buffer.ReadInt32();
            Core.Type.Skill[skillNum].Vital = buffer.ReadInt32();
            Core.Type.Skill[skillNum].X = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Y = buffer.ReadInt32();

            // projectiles
            Core.Type.Skill[skillNum].IsProjectile = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Projectile = buffer.ReadInt32();

            Core.Type.Skill[skillNum].KnockBack = (byte)buffer.ReadInt32();
            Core.Type.Skill[skillNum].KnockBackTiles = (byte)buffer.ReadInt32();

            // Save it
            NetworkSend.SendUpdateSkillToAll(skillNum);
            Database.SaveSkill(skillNum);
            Log.Add(GetPlayerLogin(index) + " saved Skill #" + skillNum + ".", Constant.ADMIN_LOG);

            buffer.Dispose();
        }

        public static void Packet_SetAccess(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;
            int i;

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Owner)
                return;

            // The index
            n = GameLogic.FindPlayer(buffer.ReadString());

            // The access
            i = buffer.ReadInt32();

            // Check for invalid access level
            if (i >= (int)Core.Enum.AccessType.Player && i <= (int)Core.Enum.AccessType.Owner)
            {
                // Check if player is on
                if (n >= 0)
                {
                    if (n != index)
                    {
                        // check to see if same level access is trying to change another access of the very same level and boot them if they are.
                        if (GetPlayerAccess(n) == GetPlayerAccess(index))
                        {
                            NetworkSend.PlayerMsg(index, "Invalid access level.", (int)ColorType.BrightRed);
                            return;
                        }
                    }

                    if (GetPlayerAccess(n) == (int)Core.Enum.AccessType.Player && i > (int)AccessType.Player)
                    {
                        NetworkSend.GlobalMsg(GetPlayerName(n) + " has been blessed with administrative access.");
                    }

                    SetPlayerAccess(n, i);
                    NetworkSend.SendPlayerData(n);
                    Log.Add(GetPlayerName(index) + " has modified " + GetPlayerName(n) + "'s access.", Constant.ADMIN_LOG);
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Player is not online.", (int) ColorType.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Invalid access level.", (int) ColorType.BrightRed);
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
            if (GetPlayerAccess(index) < (byte)AccessType.Mapper)
                return;

            Settings.Instance.Welcome = buffer.ReadString();
            Settings.Save();

            NetworkSend.GlobalMsg("Welcome changed to: " + Settings.Instance.Welcome);
            Log.Add(GetPlayerName(index) + " changed welcome to: " + Settings.Instance.Welcome, Constant.ADMIN_LOG);

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
            if (x < 0 | x > (int)Core.Type.Map[GetPlayerMap(index)].MaxX | y < 0 | y > (int)Core.Type.Map[GetPlayerMap(index)].MaxY)
                return;

            // Check for a player
            var loopTo = NetworkConfig.Socket.HighIndex + 1;
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
                                    NetworkSend.PlayerMsg(index, "You wouldn't stand a chance.", (int) ColorType.BrightRed);
                                }

                                else if (GetPlayerLevel(i) > GetPlayerLevel(index))
                                {
                                    NetworkSend.PlayerMsg(index, "This one seems to have an advantage over you.", (int) ColorType.Yellow);
                                }

                                else if (GetPlayerLevel(i) == GetPlayerLevel(index))
                                {
                                    NetworkSend.PlayerMsg(index, "This would be an even fight.", (int) ColorType.White);
                                }

                                else if (GetPlayerLevel(index) >= GetPlayerLevel(i) + 5)
                                {
                                    NetworkSend.PlayerMsg(index, "You could slaughter that player.", (int) ColorType.BrightBlue);
                                }

                                else if (GetPlayerLevel(index) > GetPlayerLevel(i))
                                {
                                    NetworkSend.PlayerMsg(index, "You would have an advantage over that player.", (int) ColorType.BrightCyan);
                                }
                            }

                            // Change target
                            if (Core.Type.TempPlayer[index].TargetType == 0 | i != Core.Type.TempPlayer[index].Target)
                            {
                                Core.Type.TempPlayer[index].Target = i;
                                Core.Type.TempPlayer[index].TargetType = (byte)TargetType.Player;
                            }
                            else
                            {
                                Core.Type.TempPlayer[index].Target = 0;
                                Core.Type.TempPlayer[index].TargetType = 0;
                            }

                            NetworkSend.PlayerMsg(index, "Your target is now " + GetPlayerName(i) + ".", (int) ColorType.Yellow);
                            NetworkSend.SendTarget(index, Core.Type.TempPlayer[index].Target, Core.Type.TempPlayer[index].TargetType);
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
                if (Core.Type.MapItem[GetPlayerMap(index), i].Num >= 0)
                {
                    if (!string.IsNullOrEmpty(Core.Type.Item[(int)Core.Type.MapItem[GetPlayerMap(index), i].Num].Name))
                    {
                        if ((int)Core.Type.MapItem[GetPlayerMap(index), i].X == x)
                        {
                            if ((int)Core.Type.MapItem[GetPlayerMap(index), i].Y == y)
                            {
                                NetworkSend.PlayerMsg(index, "You see " + GameLogic.CheckGrammar(Core.Type.Item[(int)Core.Type.MapItem[GetPlayerMap(index), i].Num].Name) + ".", (int) ColorType.BrightGreen);
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
                if (Core.Type.MapNPC[GetPlayerMap(index)].NPC[i].Num >= 0)
                {
                    if (Core.Type.MapNPC[GetPlayerMap(index)].NPC[i].X == x)
                    {
                        if (Core.Type.MapNPC[GetPlayerMap(index)].NPC[i].Y == y)
                        {
                            // Change target
                            if (Core.Type.TempPlayer[index].TargetType == 0)
                            {
                                Core.Type.TempPlayer[index].Target = i;
                                Core.Type.TempPlayer[index].TargetType = (byte)TargetType.NPC;
                            }
                            else
                            {
                                Core.Type.TempPlayer[index].Target = 0;
                                Core.Type.TempPlayer[index].TargetType = 0;
                            }
                            NetworkSend.PlayerMsg(index, "Your target is now " + GameLogic.CheckGrammar(Core.Type.NPC[(int)Core.Type.MapNPC[GetPlayerMap(index)].NPC[i].Num].Name) + ".", (int) ColorType.Yellow);
                            NetworkSend.SendTarget(index, Core.Type.TempPlayer[index].Target, Core.Type.TempPlayer[index].TargetType);
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

            if ((int)Core.Type.Map[GetPlayerMap(index)].Moral > 0)
            {
                if (Core.Type.Moral[Core.Type.Map[GetPlayerMap(index)].Moral].CanCast)
                {
                    // set the skill buffer before casting
                    Player.bufferSkill(index, n);
                }
            }
        }

        public static void Packet_QuitGame(int index, ref byte[] data)
        {
            NetworkSend.SendLeftGame(index);
            Player.LeftGame(index);
        }

        public static void Packet_SwapInvSlots(int index, ref byte[] data)
        {
            double oldSlot;
            double newSlot;
            var buffer = new ByteStream(data);

            if (Core.Type.TempPlayer[index].InTrade >= 0 | Core.Type.TempPlayer[index].InBank | Core.Type.TempPlayer[index].InShop >= 0)
                return;

            // Old Slot
            oldSlot = buffer.ReadDouble();
            newSlot = buffer.ReadDouble();
            buffer.Dispose();

            Player.PlayerSwitchInvSlots(index, (int)oldSlot, (int)newSlot);

            buffer.Dispose();
        }

        public static void Packet_SwapSkillSlots(int index, ref byte[] data)
        {
            double oldSlot;
            double newSlot;
            var buffer = new ByteStream(data);

            if (Core.Type.TempPlayer[index].InTrade >= 0 | Core.Type.TempPlayer[index].InBank | Core.Type.TempPlayer[index].InShop >= 0)
                return;

            // Old Slot
            oldSlot = buffer.ReadDouble();
            newSlot = buffer.ReadDouble();
            buffer.Dispose();

            Player.PlayerSwitchSkillSlots(index, (int)oldSlot, (int)newSlot);

            buffer.Dispose();
        }

        public static void Packet_CheckPing(int index, ref byte[] data)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SSendPing);

            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);

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

        public static void Packet_RequestNPC(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;

            n = buffer.ReadInt32();

            if (n < 0 | n > Core.Constant.MAX_NPCS)
                return;

            NPC.SendUpdateNPCTo(index, n);
        }

        public static void Packet_SpawnItem(int index, ref byte[] data)
        {
            int tmpItem;
            int tmpAmount;
            var buffer = new ByteStream(data);

            // item
            tmpItem = buffer.ReadInt32();
            tmpAmount = buffer.ReadInt32();

            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            Item.SpawnItem(tmpItem, tmpAmount, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index));
            buffer.Dispose();
        }

        public static void Packet_TrainStat(int index, ref byte[] data)
        {
            int tmpstat;
            var buffer = new ByteStream(data);

            // check points
            if (GetPlayerPoints(index) == 0)
                return;

            // stat
            tmpstat = buffer.ReadInt32();

            // make sure there stats are not maxed
            if (GetPlayerRawStat(index, (StatType)tmpstat) >= Core.Constant.MAX_STATS)
            {
                NetworkSend.PlayerMsg(index, "You cannot spend any more points on that stat.", (int)ColorType.BrightRed);
                return;
            }

            // increment stat
            SetPlayerStat(index, (StatType)tmpstat, GetPlayerRawStat(index, (StatType)tmpstat) + 1);

            // decrement points
            SetPlayerPoints(index, GetPlayerPoints(index) - 1);

            // send player new data
            NetworkSend.SendPlayerData(index);
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
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
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
            if (Core.Type.TempPlayer[index].SkillCD[SkillSlot] > 0)
            {
                NetworkSend.PlayerMsg(index, "Cannot forget a skill which is cooling down!", (int) ColorType.BrightRed);
                return;
            }

            // dont let them forget a skill which is buffered
            if (Core.Type.TempPlayer[index].SkillBuffer == SkillSlot)
            {
                NetworkSend.PlayerMsg(index, "Cannot forget a skill which you are casting!", (int) ColorType.BrightRed);
                return;
            }

            Core.Type.Player[index].Skill[SkillSlot].Num = 0;
            NetworkSend.SendPlayerSkills(index);

            buffer.Dispose();
        }

        public static void Packet_CloseShop(int index, ref byte[] data)
        {
            Core.Type.TempPlayer[index].InShop = 0;
        }

        public static void Packet_BuyItem(int index, ref byte[] data)
        {
            int shopslot;
            double shopnum;
            int itemamount;
            var buffer = new ByteStream(data);

            shopslot = buffer.ReadInt32();

            // not in shop, exit out
            shopnum = Core.Type.TempPlayer[index].InShop;
            if (shopnum < 0 | shopnum > Core.Constant.MAX_SHOPS)
                return;

            ref var withBlock = ref Core.Type.Shop[(int)shopnum].TradeItem[shopslot];
            // check trade exists
            if (withBlock.Item < 0)
                return;

            // check has the cost item
            itemamount = Player.HasItem(index, withBlock.CostItem);
            if (itemamount == 0 | itemamount < withBlock.CostValue)
            {
                NetworkSend.PlayerMsg(index, "You do not have enough to buy this item.", (int) ColorType.BrightRed);
                NetworkSend.ResetShopAction(index);
                return;
            }

            // it's fine, let's go ahead
            for (int i = 0, loopTo = withBlock.CostValue; i < loopTo; i++)
                Player.TakeInv(index, withBlock.CostItem, withBlock.CostValue);
            Player.GiveInv(index, withBlock.Item, withBlock.ItemValue);
            

            // send confirmation message & reset their shop action
            NetworkSend.PlayerMsg(index, "Trade successful.", (int) ColorType.BrightGreen);
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
            shopNum = Core.Type.TempPlayer[index].InShop;

            if (shopNum < 0 || shopNum > Core.Constant.MAX_SHOPS)
            {
                return;
            }

            // work out price
            multiplier = Core.Type.Shop[(int)shopNum].BuyRate / 100d;
            price = (int)Math.Round(Core.Type.Item[(int)itemNum].Price * multiplier);

            // item has cost?
            if (price < 0)
            {
                NetworkSend.PlayerMsg(index, "The shop doesn't want that item.", (int) ColorType.Yellow);
                NetworkSend.ResetShopAction(index);
                return;
            }

            // take item and give gold
            Player.TakeInv(index, (int)itemNum, 1);
            Player.GiveInv(index, 1, price);

            // send confirmation message & reset their shop action
            NetworkSend.PlayerMsg(index, "Sold the " + Core.Type.Item[(int)itemNum].Name + " for " + price + " " + Core.Type.Item[(int)itemNum].Name + "!", (int) ColorType.BrightGreen);
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

            Player.PlayerSwitchBankSlots(index, oldslot, newslot);

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
            int bankSlot;
            int amount;
            var buffer = new ByteStream(data);

            bankSlot = buffer.ReadInt32();
            amount = buffer.ReadInt32();

            Player.TakeBank(index, (byte)bankSlot, amount);

            buffer.Dispose();
        }

        public static void Packet_CloseBank(int index, ref byte[] data)
        {
            Core.Type.TempPlayer[index].InBank = false;
        }

        public static void Packet_AdminWarp(int index, ref byte[] data)
        {
            int x;
            int y;
            var buffer = new ByteStream(data);

            x = buffer.ReadInt32();
            y = buffer.ReadInt32();

            if (GetPlayerAccess(index) >= (byte) AccessType.Mapper)
            {
                // Set the information
                SetPlayerX(index, x);
                SetPlayerY(index, y);

                // send the stuff
                NetworkSend.SendPlayerXY(index);
            }

            buffer.Dispose();
        }

        public static void Packet_TradeInvite(int index, ref byte[] data)
        {
            string Name;
            int tradetarget;
            var buffer = new ByteStream(data);

            Name = buffer.ReadString();

            buffer.Dispose();

            // Check for a player
            tradetarget = GameLogic.FindPlayer(Name);

            if (tradetarget < 0 | tradetarget >= Core.Constant.MAX_PLAYERS)
                return;

            // can't trade with yourself..
            if (tradetarget == index)
            {
                NetworkSend.PlayerMsg(index, "You can't trade with yourself!", (int) ColorType.BrightRed);
                return;
            }

            // send the trade request
            Core.Type.TempPlayer[index].TradeRequest = tradetarget;
            Core.Type.TempPlayer[tradetarget].TradeRequest = index;

            NetworkSend.PlayerMsg(tradetarget, GetPlayerName(index) + " has invited you to trade.", (int) ColorType.Yellow);
            NetworkSend.PlayerMsg(index, "You have invited " + GetPlayerName(tradetarget) + " to trade.", (int) ColorType.BrightGreen);

            NetworkSend.SendTradeInvite(tradetarget, index);
        }

        public static void Packet_HandleTradeInvite(int index, ref byte[] data)
        {
            int tradetarget;
            byte status;
            var buffer = new ByteStream(data);

            status = (byte)buffer.ReadInt32();

            buffer.Dispose();

            tradetarget = Core.Type.TempPlayer[index].TradeRequest;

            if (status == 0)
            {
                NetworkSend.PlayerMsg(tradetarget, GetPlayerName(index) + " has declined your trade request.", (int) ColorType.BrightRed);
                NetworkSend.PlayerMsg(index, "You have declined the trade with " + GetPlayerName(tradetarget) + ".", (int) ColorType.BrightRed);
                Core.Type.TempPlayer[index].TradeRequest = 0;
                return;
            }

            // Let them trade!
            if (Core.Type.TempPlayer[tradetarget].TradeRequest == index)
            {
                // let them know they're trading
                NetworkSend.PlayerMsg(index, "You have accepted " + GetPlayerName(tradetarget) + "'s trade request.", (int) ColorType.Yellow);
                NetworkSend.PlayerMsg(tradetarget, GetPlayerName(index) + " has accepted your trade request.", (int) ColorType.BrightGreen);

                // clear the tradeRequest server-side
                Core.Type.TempPlayer[index].TradeRequest = 0;
                Core.Type.TempPlayer[tradetarget].TradeRequest = 0;

                // set that they're trading with each other
                Core.Type.TempPlayer[index].InTrade = tradetarget;

                // clear out their trade offers
                Core.Type.TempPlayer[tradetarget].InTrade = index;
                ;
                Array.Resize(ref Core.Type.TempPlayer[index].TradeOffer, Core.Constant.MAX_INV);
                Array.Resize(ref Core.Type.TempPlayer[tradetarget].TradeOffer, Core.Constant.MAX_INV);

                for (int i = 0, loopTo = Core.Constant.MAX_INV; i < loopTo; i++)
                {
                    Core.Type.TempPlayer[index].TradeOffer[i].Num = 0;
                    Core.Type.TempPlayer[index].TradeOffer[i].Value = 0;
                    Core.Type.TempPlayer[tradetarget].TradeOffer[i].Num = 0;
                    Core.Type.TempPlayer[tradetarget].TradeOffer[i].Value = 0;
                }

                // Used to init the trade window clientside
                NetworkSend.SendTrade(index, tradetarget);
                NetworkSend.SendTrade(tradetarget, index);

                // Send the offer data - Used to clear their client
                NetworkSend.SendTradeUpdate(index, 0);
                NetworkSend.SendTradeUpdate(index, 1);
                NetworkSend.SendTradeUpdate(tradetarget, 0);
                NetworkSend.SendTradeUpdate(tradetarget, 1);
            }
        }

        public static void Packet_TradeInviteDecline(int index, ref byte[] data)
        {
            Core.Type.TempPlayer[index].TradeRequest = 0;
        }

        public static void Packet_AcceptTrade(int index, ref byte[] data)
        {
            int itemNum;
            int tradeTarget;
            int i;
            var tmpTradeItem = new PlayerInvStruct[Core.Constant.MAX_INV];
            var tmpTradeItem2 = new PlayerInvStruct[Core.Constant.MAX_INV];

            Core.Type.TempPlayer[index].AcceptTrade = true;

            tradeTarget = (int)Core.Type.TempPlayer[index].InTrade;

            // if not both of them accept, then exit
            if (!Core.Type.TempPlayer[tradeTarget].AcceptTrade)
            {
                NetworkSend.SendTradeStatus(index, 2);
                NetworkSend.SendTradeStatus(tradeTarget, 1);
                return;
            }

            // take their items
            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo; i++)
            {
                // player
                if (Core.Type.TempPlayer[index].TradeOffer[i].Num >= 0)
                {
                    itemNum = (int)Core.Type.Player[index].Inv[(int)Core.Type.TempPlayer[index].TradeOffer[i].Num].Num;
                    if (itemNum >= 0)
                    {
                        // store temp
                        tmpTradeItem[i].Num = itemNum;
                        tmpTradeItem[i].Value = Core.Type.TempPlayer[index].TradeOffer[i].Value;
                        // take item
                        Player.TakeInvSlot(index, (int)Core.Type.TempPlayer[index].TradeOffer[i].Num, tmpTradeItem[i].Value);
                    }
                }
                // target
                if (Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Num >= 0)
                {
                    itemNum = (int)GetPlayerInv(tradeTarget, (int)Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Num);
                    if (itemNum >= 0)
                    {
                        // store temp
                        tmpTradeItem2[i].Num = itemNum;
                        tmpTradeItem2[i].Value = Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Value;
                        // take item
                        Player.TakeInvSlot(tradeTarget, (int)Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Num, tmpTradeItem2[i].Value);
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
                Core.Type.TempPlayer[index].TradeOffer[i].Num = 0;
                Core.Type.TempPlayer[index].TradeOffer[i].Value = 0;
                Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Num = 0;
                Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Value = 0;
            }

            Core.Type.TempPlayer[index].InTrade = -1;
            Core.Type.TempPlayer[tradeTarget].InTrade = -1;

            NetworkSend.PlayerMsg(index, "Trade completed.", (int) ColorType.BrightGreen);
            NetworkSend.PlayerMsg(tradeTarget, "Trade completed.", (int) ColorType.BrightGreen);

            NetworkSend.SendCloseTrade(index);
            NetworkSend.SendCloseTrade(tradeTarget);
        }

        public static void Packet_DeclineTrade(int index, ref byte[] data)
        {
            int tradeTarget;

            tradeTarget = (int)Core.Type.TempPlayer[index].InTrade;

            for (int i = 0, loopTo = Core.Constant.MAX_INV; i < loopTo; i++)
            {
                Core.Type.TempPlayer[index].TradeOffer[i].Num = 0;
                Core.Type.TempPlayer[index].TradeOffer[i].Value = 0;
                Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Num = 0;
                Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Value = 0;
            }

            Core.Type.TempPlayer[index].InTrade = -1;
            Core.Type.TempPlayer[tradeTarget].InTrade = -1;

            NetworkSend.PlayerMsg(index, "You declined the trade.", (int) ColorType.BrightRed);
            NetworkSend.PlayerMsg(tradeTarget, GetPlayerName(index) + " has declined the trade.", (int) ColorType.BrightRed);

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

            itemnum = (int)GetPlayerInv(index, invslot);

            if (itemnum < 0 | itemnum > Core.Constant.MAX_ITEMS)
                return;

            // make sure they have the amount they offer
            if (amount < 0 | amount > GetPlayerInvValue(index, invslot))
                return;

            if (Core.Type.Item[itemnum].Type == (byte)ItemType.Currency | Core.Type.Item[itemnum].Stackable == 1)
            {

                // check if already offering same currency item
                var loopTo = Core.Constant.MAX_INV;
                for (i = 0; i < loopTo; i++)
                {

                    if (Core.Type.TempPlayer[index].TradeOffer[i].Num == invslot)
                    {
                        // add amount
                        Core.Type.TempPlayer[index].TradeOffer[i].Value = Core.Type.TempPlayer[index].TradeOffer[i].Value + amount;

                        // clamp to limits
                        if (Core.Type.TempPlayer[index].TradeOffer[i].Value > GetPlayerInvValue(index, invslot))
                        {
                            Core.Type.TempPlayer[index].TradeOffer[i].Value = GetPlayerInvValue(index, invslot);
                        }

                        // cancel any trade agreement
                        Core.Type.TempPlayer[index].AcceptTrade = false;
                        Core.Type.TempPlayer[(int)Core.Type.TempPlayer[index].InTrade].AcceptTrade = false;

                        NetworkSend.SendTradeStatus(index, 0);
                        NetworkSend.SendTradeStatus((int)Core.Type.TempPlayer[index].InTrade, 0);

                        NetworkSend.SendTradeUpdate(index, 0);
                        NetworkSend.SendTradeUpdate((int)Core.Type.TempPlayer[index].InTrade, 1);
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
                    if (Core.Type.TempPlayer[index].TradeOffer[i].Num == invslot)
                    {
                        NetworkSend.PlayerMsg(index, "You've already offered this item.", (int) ColorType.BrightRed);
                        return;
                    }
                }
            }

            // not already offering - find earliest empty slot
            var loopTo2 = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo2; i++)
            {
                if (Core.Type.TempPlayer[index].TradeOffer[i].Num == -1)
                {
                    emptyslot = i;
                    break;
                }
            }
            Core.Type.TempPlayer[index].TradeOffer[emptyslot].Num = invslot;
            Core.Type.TempPlayer[index].TradeOffer[emptyslot].Value = amount;

            // cancel any trade agreement and send new data
            Core.Type.TempPlayer[index].AcceptTrade = false;
            Core.Type.TempPlayer[(int)Core.Type.TempPlayer[index].InTrade].AcceptTrade = false;

            NetworkSend.SendTradeStatus(index, 0);
            NetworkSend.SendTradeStatus((int)Core.Type.TempPlayer[index].InTrade, 0);

            NetworkSend.SendTradeUpdate(index, 0);
            NetworkSend.SendTradeUpdate((int)Core.Type.TempPlayer[index].InTrade, 1);
        }

        public static void Packet_UntradeItem(int index, ref byte[] data)
        {
            int tradeslot;
            var buffer = new ByteStream(data);

            tradeslot = buffer.ReadInt32();

            buffer.Dispose();

            if (tradeslot < 0 | tradeslot > Core.Constant.MAX_INV)
                return;
            if (Core.Type.TempPlayer[index].TradeOffer[tradeslot].Num < 0)
                return;

            Core.Type.TempPlayer[index].TradeOffer[tradeslot].Num = 0;
            Core.Type.TempPlayer[index].TradeOffer[tradeslot].Value = 0;

            if (Core.Type.TempPlayer[index].AcceptTrade)
                Core.Type.TempPlayer[index].AcceptTrade = false;
            if (Core.Type.TempPlayer[(int)Core.Type.TempPlayer[index].InTrade].AcceptTrade)
                Core.Type.TempPlayer[(int)Core.Type.TempPlayer[index].InTrade].AcceptTrade = false;

            NetworkSend.SendTradeStatus(index, 0);
            NetworkSend.SendTradeStatus((int)Core.Type.TempPlayer[index].InTrade, 0);

            NetworkSend.SendTradeUpdate(index, 0);
            NetworkSend.SendTradeUpdate((int)Core.Type.TempPlayer[index].InTrade, 1);
        }

        public static void HackingAttempt(int index, string Reason)
        {

            if (index > 0 & NetworkConfig.IsPlaying(index))
            {
                NetworkSend.GlobalMsg(GetPlayerLogin(index) + "/" + GetPlayerName(index) + " has been booted for (" + Reason + ")");

                NetworkSend.AlertMsg(index, (byte)DialogueMsg.Connection, (byte)MenuType.Login);
            }

        }

        public static void Packet_MapReport(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Mapper)
                return;

            NetworkSend.SendMapReport(index);
        }

        public static void Packet_Admin(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Moderator)
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

            if (newSlot < 1 | newSlot > Core.Constant.MAX_HOTBAR)
                return;

            if (type == (byte)PartOriginType.Hotbar)
            {
                if (oldSlot < 1 | oldSlot > Core.Constant.MAX_HOTBAR)
                    return;

                Core.Type.Player[index].Hotbar[newSlot].Slot = skill;
                Core.Type.Player[index].Hotbar[newSlot].SlotType = Core.Type.Player[index].Hotbar[oldSlot].SlotType;

                Core.Type.Player[index].Hotbar[oldSlot].Slot = 0;
                Core.Type.Player[index].Hotbar[oldSlot].SlotType = 0;
            }
            else
            {
                Core.Type.Player[index].Hotbar[newSlot].Slot = skill;
                Core.Type.Player[index].Hotbar[newSlot].SlotType = type;
            }

            NetworkSend.SendHotbar(index);

            buffer.Dispose();
        }

        public static void Packet_DeleteHotbarSlot(int index, ref byte[] data)
        {
            int slot;
            var buffer = new ByteStream(data);

            slot = buffer.ReadInt32();

            if (slot < 1 | slot > Core.Constant.MAX_HOTBAR)
                return;

            Core.Type.Player[index].Hotbar[slot].Slot = 0;
            Core.Type.Player[index].Hotbar[slot].SlotType = 0;

            NetworkSend.SendHotbar(index);

            buffer.Dispose();
        }

        public static void Packet_UseHotbarSlot(int index, ref byte[] data)
        {
            int slot;
            var buffer = new ByteStream(data);

            slot = buffer.ReadInt32();
            buffer.Dispose();

            if (slot < 1 | slot > Core.Constant.MAX_HOTBAR)
                return;

            if (Core.Type.Player[index].Hotbar[slot].Slot >= 0)
            {
                if (Core.Type.Player[index].Hotbar[slot].SlotType == (byte)PartType.Item)
                {
                    Player.UseItem(index, Player.FindItemSlot(index, (int)Core.Type.Player[index].Hotbar[slot].Slot));
                }
            }

            NetworkSend.SendHotbar(index);
        }

        public static void Packet_SkillLearn(int index, ref byte[] data)
        {
            int skillNum;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Developer)
                return;

            skillNum = buffer.ReadInt32();

            Player.PlayerLearnSkill(index, 0, skillNum);
        }

        public static void Packet_RequestEditJob(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Developer)
                return;

            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = IsEditorLocked(index, (byte)EditorType.Job);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            Item.SendItems(index);
            NetworkSend.SendJobs(index);

            Core.Type.TempPlayer[index].Editor = (byte)EditorType.Job;

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
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            jobNum = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.Job[jobNum];
                withBlock.Name = buffer.ReadString();
                withBlock.Desc = buffer.ReadString();

                withBlock.MaleSprite = buffer.ReadInt32();
                withBlock.FemaleSprite = buffer.ReadInt32();

                var loopTo = (byte)StatType.Count;
                for (x = 0; x < loopTo; x++)
                    withBlock.Stat[x] = buffer.ReadInt32();

                for (int q = 0; q <= 4; q++)
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

            Database.SaveJobs();
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
            if (GetPlayerAccess(index) < (byte) AccessType.Mapper)
                return;

            if (Core.Type.TempPlayer[index].Editor == -1)
                return;

            Core.Type.TempPlayer[index].Editor = -1;
        }

    }
}