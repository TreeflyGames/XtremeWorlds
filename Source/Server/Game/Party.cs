using System;
using System.Linq;
using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Packets;
using static Core.Global.Command;

namespace Server
{

    public class Party
    {

        #region Outgoing Packets

        public static void SendDataToParty(int partyNum, ReadOnlySpan<byte> data)
        {
            int i;

            var loopTo = Data.Party[partyNum].MemberCount;
            for (i = 0; i < loopTo; i++)
            {
                if (Data.Party[partyNum].Member[i] > 0)
                {
                    var dataSize = data.Length;
                    NetworkConfig.Socket.SendDataTo(Data.Party[partyNum].Member[i], data, dataSize);
                }
            }
        }

        public static void SendPartyInvite(int index, int target)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((byte)Packets.ServerPackets.SPartyInvite);

            buffer.WriteString(Core.Data.Player[target].Name);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendPartyUpdate(int partyNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SPartyUpdate);

            if (Data.Party[partyNum].Leader == -1)
            {
                buffer.WriteInt32(0);
            }
            else
            {
                buffer.WriteInt32(1);
            }
            buffer.WriteInt32(Data.Party[partyNum].Leader);
            for (int i = 0, loopTo = Core.Constant.MAX_PARTY_MEMBERS; i < loopTo; i++)
                buffer.WriteInt32(Data.Party[partyNum].Member[i]);
            buffer.WriteInt32(Data.Party[partyNum].MemberCount);

            var argdata = buffer.ToArray();
            SendDataToParty(partyNum, argdata);
            buffer.Dispose();
        }

        public static void SendPartyUpdateTo(int index)
        {
            var buffer = new ByteStream(4);
            int i;
            int partyNum;

            buffer.WriteInt32((int) ServerPackets.SPartyUpdate);

            // check if we're in a party
            partyNum = Core.Data.TempPlayer[index].InParty;
            if (partyNum >= 0)
            {
                // send party data
                buffer.WriteInt32(1);
                buffer.WriteInt32(Data.Party[partyNum].Leader);
                var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
                for (i = 0; i < loopTo; i++)
                    buffer.WriteInt32(Data.Party[partyNum].Member[i]);
                buffer.WriteInt32(Data.Party[partyNum].MemberCount);
            }
            else
            {
                // send clear command
                buffer.WriteInt32(0);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendPartyVitals(int partyNum, int index)
        {
            ByteStream buffer;
            int i;

            buffer = new ByteStream(4);
            buffer.WriteInt32((byte)ServerPackets.SPartyVitals);
            buffer.WriteInt32(index);

            var loopTo = (byte)System.Enum.GetNames(typeof(Core.Vital)).Length;
            for (i = 0; i < loopTo; i++)
                buffer.WriteInt32(Core.Data.Player[index].Vital[i]);

            byte[] data = buffer.ToArray();
            SendDataToParty(partyNum, data);
            buffer.Dispose();
        }

        #endregion

        #region Incoming Packets

        public static void Packet_PartyRquest(int index, ref byte[] data)
        {
            // Prevent partying with self
            if (Core.Data.TempPlayer[index].Target == index)
                return;

            // make sure it's a valid target
            if (Core.Data.TempPlayer[index].TargetType != (byte)TargetType.Player)
                return;

            // make sure they're connected and on the same map
            if (GetPlayerMap(Core.Data.TempPlayer[index].Target) != GetPlayerMap(index))
                return;

            // init the request
            Invite(index, Core.Data.TempPlayer[index].Target);
        }

        public static void Packet_AcceptParty(int index, ref byte[] data)
        {
            InviteAccept(Core.Data.TempPlayer[index].PartyInvite, index);
        }

        public static void Packet_DeclineParty(int index, ref byte[] data)
        {
            InviteDecline(Core.Data.TempPlayer[index].PartyInvite, index);
        }

        public static void Packet_LeaveParty(int index, ref byte[] data)
        {
            PlayerLeave(index);
        }

        public static void Packet_PartyChatMsg(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);

            PartyMsg(index, buffer.ReadString());

            buffer.Dispose();
        }

        #endregion


        public static void ClearParty(int partyNum)
        {
            Data.Party[partyNum].Leader = -1;
            Data.Party[partyNum].MemberCount = 0;
            Data.Party[partyNum].Member = new int[Core.Constant.MAX_PARTY_MEMBERS];
        }

        public static void PartyMsg(int partyNum, string msg)
        {
            int i;

            // send message to all people
            var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
            for (i = 0; i < loopTo; i++)
            {
                // exist?
                if (Data.Party[partyNum].Member[i] >= 0)
                {
                    // make sure they're logged on
                    NetworkSend.PlayerMsg(Data.Party[partyNum].Member[i], msg, (int) Color.BrightBlue);
                }
            }
        }

        private static void RemoveFromParty(int index, int partyNum)
        {
            for (int i = 0, loopTo = Core.Constant.MAX_PARTY_MEMBERS; i < loopTo; i++)
            {
                if (Data.Party[partyNum].Member[i] == index)
                {
                    Data.Party[partyNum].Member[i] = -1;
                    Core.Data.TempPlayer[index].InParty = -1;
                    Core.Data.TempPlayer[index].PartyInvite = -1;
                    break;
                }
            }

            CountMembers(partyNum);
            SendPartyUpdate(partyNum);
            SendPartyUpdateTo(index);
        }

        public static void PlayerLeave(int index)
        {
            int partyNum;
            int i;

            partyNum = Core.Data.TempPlayer[index].InParty;

            if (partyNum >= 0)
            {
                // find out how many members we have
                CountMembers(partyNum);

                // make sure there's more than 2 people
                if (Data.Party[partyNum].MemberCount > 2)
                {
                    // check if leader
                    if (Data.Party[partyNum].Leader == index)
                    {
                        // set next person down as leader
                        var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
                        for (i = 0; i < loopTo; i++)
                        {
                            if (Data.Party[partyNum].Member[i] >= 0 & Data.Party[partyNum].Member[i] != index)
                            {
                                Data.Party[partyNum].Leader = Data.Party[partyNum].Member[i];
                                PartyMsg(partyNum, string.Format("{0} is now the party leader.", GetPlayerName(i)));
                                break;
                            }
                        }
                        // leave party
                        PartyMsg(partyNum, string.Format("{0} has left the party.", GetPlayerName(index)));
                        RemoveFromParty(index, partyNum);
                    }
                    else
                    {
                        // not the leader, just leave
                        PartyMsg(partyNum, string.Format("{0} has left the party.", GetPlayerName(index)));
                        RemoveFromParty(index, partyNum);
                    }
                }
                else
                {
                    // only 2 people, disband
                    PartyMsg(partyNum, "The party has been disbanded.");

                    // remove leader
                    RemoveFromParty(Data.Party[partyNum].Leader, partyNum);

                    // clear out everyone's party
                    var loopTo1 = Core.Constant.MAX_PARTY_MEMBERS;
                    for (i = 0; i < loopTo1; i++)
                    {
                        index = Data.Party[partyNum].Member[i];
                        // player exist?
                        if (index > 0)
                        {
                            RemoveFromParty(index, partyNum);
                        }
                    }

                    // clear out the party itself
                    ClearParty(partyNum);
                }
            }
        }

        public static void Invite(int index, int target)
        {
            int partyNum;
            int i;

            // make sure they're not busy
            if (Core.Data.TempPlayer[target].PartyInvite >= 0 | Core.Data.TempPlayer[target].TradeRequest >= 0)
            {
                // they've already got a request for trade/party
                NetworkSend.PlayerMsg(index, "This player is busy.", (int) Color.BrightRed);
                return;
            }

            // make syure they're not in a party
            if (Core.Data.TempPlayer[target].InParty >= 0)
            {
                // they're already in a party
                NetworkSend.PlayerMsg(index, "This player is already in a party.", (int) Color.BrightRed);
                return;
            }

            // check if we're in a party
            if (Core.Data.TempPlayer[index].InParty >= 0)
            {
                partyNum = Core.Data.TempPlayer[index].InParty;
                // make sure we're the leader
                if (Data.Party[partyNum].Leader == index)
                {
                    // got a blank slot?
                    var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
                    for (i = 0; i < loopTo; i++)
                    {
                        if (Data.Party[partyNum].Member[i] == -1)
                        {
                            // send the invitation
                            SendPartyInvite(target, index);

                            // set the invite target
                            Core.Data.TempPlayer[target].PartyInvite = index;

                            // let them know
                            NetworkSend.PlayerMsg(index, "Party invitation sent.", (int) Color.Pink);
                            return;
                        }
                    }
                    // no room
                    NetworkSend.PlayerMsg(index, "Party is full.", (int) Color.BrightRed);
                    return;
                }
                else
                {
                    // not the leader
                    NetworkSend.PlayerMsg(index, "You are not the party leader.", (int) Color.BrightRed);
                    return;
                }
            }
            else
            {
                // not in a party - doesn't matter!
                SendPartyInvite(target, index);

                // set the invite target
                Core.Data.TempPlayer[target].PartyInvite = index;

                // let them know
                NetworkSend.PlayerMsg(index, "Party invitation sent.", (int) Color.Pink);
                return;
            }
        }

        public static void InviteAccept(int index, int target)
        {
            var partyNum = default(int);
            int i;

            // check if already in a party
            if (Core.Data.TempPlayer[index].InParty >= 0)
            {
                // get the partynumber
                partyNum = Core.Data.TempPlayer[index].InParty;
                // got a blank slot?
                var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
                for (i = 0; i < loopTo; i++)
                {
                    if (Data.Party[partyNum].Member[i] == -1)
                    {
                        // add to the party
                        Data.Party[partyNum].Member[i] = target;

                        // recount party
                        CountMembers(partyNum);

                        // send update to all - including new player
                        SendPartyUpdate(partyNum);
                        SendPartyVitals(partyNum, target);

                        // let everyone know they've joined
                        PartyMsg(partyNum, string.Format("{0} has joined the party.", GetPlayerName(target)));

                        // add them in
                        Core.Data.TempPlayer[target].InParty = (byte)partyNum;
                        return;
                    }
                }
                // no empty slots - let them know
                NetworkSend.PlayerMsg(index, "Party is full.", (int) Color.BrightRed);
                NetworkSend.PlayerMsg(target, "Party is full.", (int) Color.BrightRed);
                return;
            }
            else
            {
                // not in a party. Create one with the new person.
                var loopTo1 = Core.Constant.MAX_PARTY;
                for (i = 0; i < loopTo1; i++)
                {
                    // find blank party
                    if (!(Data.Party[i].Leader > -1))
                    {
                        partyNum = i;
                        break;
                    }
                }
                // create the party
                Data.Party[partyNum].MemberCount = 2;
                Data.Party[partyNum].Leader = index;
                Data.Party[partyNum].Member[0] = index;
                Data.Party[partyNum].Member[1] = target;
                SendPartyUpdate(partyNum);
                SendPartyVitals(partyNum, index);
                SendPartyVitals(partyNum, target);

                // let them know it's created
                PartyMsg(partyNum, "Party created.");
                PartyMsg(partyNum, string.Format("{0} has joined the party.", GetPlayerName(index)));

                // clear the invitation
                Core.Data.TempPlayer[target].PartyInvite = -1;

                // add them to the party
                Core.Data.TempPlayer[index].InParty = (byte)partyNum;
                Core.Data.TempPlayer[target].InParty = (byte)partyNum;
                return;
            }
        }

        public static void InviteDecline(int index, int target)
        {
            NetworkSend.PlayerMsg(index, string.Format("{0} has declined to join your party.", GetPlayerName(target)), (int) Color.BrightRed);
            NetworkSend.PlayerMsg(target, "You declined to join the party.", (int) Color.Yellow);

            // clear the invitation
            Core.Data.TempPlayer[target].PartyInvite = -1;
        }

        public static void CountMembers(int partyNum)
        {
            int i;
            var highindex = default(int);
            int x;

            // find the high index
            for (i = Core.Constant.MAX_PARTY_MEMBERS - 1; i >= 0; i -= 1)
            {
                if (Data.Party[partyNum].Member[i] >= 0)
                {
                    highindex = i;
                    break;
                }
            }

            // count the members
            var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
            for (i = 0; i < loopTo; i++)
            {
                // we've got a blank member
                if (Data.Party[partyNum].Member[i] == -1)
                {
                    // is it lower than the high index?
                    if (i < highindex)
                    {
                        // move everyone down a slot
                        var loopTo1 = Core.Constant.MAX_PARTY_MEMBERS - 1;
                        for (x = i; x < (int)loopTo1; x++)
                        {
                            Data.Party[partyNum].Member[x] = Data.Party[partyNum].Member[x + 1];
                            Data.Party[partyNum].Member[x + 1] = 0;
                        }
                    }
                    else
                    {
                        // not lower - highindex is count
                        Data.Party[partyNum].MemberCount = highindex + 1;
                        return;
                    }
                }

                // check if we've reached the max party members
                if (i == Core.Constant.MAX_PARTY_MEMBERS - 1)
                {
                    if (highindex == i)
                    {
                        Data.Party[partyNum].MemberCount = Core.Constant.MAX_PARTY_MEMBERS;
                        return;
                    }
                }
            }

            // if we're here it means that we need to re-count again
            CountMembers(partyNum);
        }

        public static void ShareExp(int partyNum, int exp, int index, int mapNum)
        {
            int expShare;
            int leftOver;
            int i;
            int tmpindex;
            var loseMemberCount = default(byte);

            // check if it's worth sharing
            if (!(exp >= Data.Party[partyNum].MemberCount))
            {
                // no party - keep exp for self
                SetPlayerExp(index, exp);
                return;
            }

            // check members in others maps
            var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
            for (i = 0; i < loopTo; i++)
            {
                tmpindex = Data.Party[partyNum].Member[i];
                if (tmpindex > -1)
                {
                    if (NetworkConfig.Socket.IsConnected(tmpindex) & NetworkConfig.IsPlaying(tmpindex))
                    {
                        if (GetPlayerMap(tmpindex) != mapNum)
                        {
                            loseMemberCount =+ 1;
                        }
                    }
                }
            }

            // find out the equal share
            if (Data.Party[partyNum].MemberCount > 0)
            {
                expShare = exp / (Data.Party[partyNum].MemberCount - loseMemberCount);
                leftOver = exp % (Data.Party[partyNum].MemberCount - loseMemberCount);

            }
            else
            {
                expShare = exp;
                leftOver = 0;
            }

            // loop through and give everyone exp
            var loopTo1 = Core.Constant.MAX_PARTY_MEMBERS;
            for (i = 0; i < loopTo1; i++)
            {
                tmpindex = Data.Party[partyNum].Member[i];
                // existing member?
                if (tmpindex > -1)
                {
                    // playing?
                    if (NetworkConfig.Socket.IsConnected(tmpindex) & NetworkConfig.IsPlaying(tmpindex))
                    {
                        if (GetPlayerMap(tmpindex) == mapNum)
                        {
                            // give them their share
                            SetPlayerExp(tmpindex, expShare);
                        }
                    }
                }
            }

            // give the remainder to a random member
            if (!(leftOver == 0))
            {
                tmpindex = Data.Party[partyNum].Member[(int)Math.Round(General.GetRandom.NextDouble(1d, Data.Party[partyNum].MemberCount))];
                // give the exp
                SetPlayerExp(tmpindex, leftOver);
            }

        }

        public static void PartyWarp(int index, int mapNum, int x, int y)
        {
            int i;

            if (Core.Data.TempPlayer[index].InParty >= 0)
            {
                if (Data.Party[Core.Data.TempPlayer[index].InParty].Leader >= 0)
                {
                    var loopTo = Data.Party[Core.Data.TempPlayer[index].InParty].MemberCount;
                    for (i = 0; i < loopTo; i++)
                        Player.PlayerWarp(Data.Party[Core.Data.TempPlayer[index].InParty].Member[i], mapNum, x, y, (byte)Direction.Down);
                }
            }

        }

        public static bool IsPlayerInParty(int index)
        {
            bool IsPlayerInPartyRet = default;
            if (index < 0 | index >= Core.Constant.MAX_PLAYERS | !Core.Data.TempPlayer[index].InGame)
                return IsPlayerInPartyRet;

            if (Core.Data.TempPlayer[index].InParty >= 0)
                IsPlayerInPartyRet = true;
            return IsPlayerInPartyRet;
        }

        public static int GetPlayerParty(int index)
        {
            int GetPlayerPartyRet = default;
            if (index < 0 | index >= Core.Constant.MAX_PLAYERS | !Core.Data.TempPlayer[index].InGame)
                return GetPlayerPartyRet;
            GetPlayerPartyRet = Core.Data.TempPlayer[index].InParty;
            return GetPlayerPartyRet;
        }

    }
}