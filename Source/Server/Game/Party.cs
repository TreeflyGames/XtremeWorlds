using System;
using System.Linq;
using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Enum;
using static Core.Packets;
using static Core.Global.Command;

namespace Server
{

    static class Party
    {

        #region Type and Globals

        internal static PartyRec[] PartyField = new PartyRec[Core.Constant.MAX_PARTIES + 1];

        public struct PartyRec
        {
            public int Leader;
            public int[] Member;
            public int MemberCount;
        }

        #endregion

        #region Outgoing Packets

        public static void SendDataToParty(int partyNum, ReadOnlySpan<byte> data)
        {
            int i;

            var loopTo = PartyField[partyNum].MemberCount;
            for (i = 0; i < loopTo; i++)
            {
                if (PartyField[partyNum].Member[i] > 0)
                {
                    var dataSize = data.Length;
                    NetworkConfig.Socket.SendDataTo(PartyField[partyNum].Member[i], data, dataSize);
                }
            }
        }

        public static void SendPartyInvite(int index, int target)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((byte)Packets.ServerPackets.SPartyInvite);

            buffer.WriteString(Core.Type.Player[target].Name);

            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendPartyUpdate(int partyNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SPartyUpdate);

            if (PartyField[partyNum].Leader == -1)
            {
                buffer.WriteInt32(0);
            }
            else
            {
                buffer.WriteInt32(1);
            }
            buffer.WriteInt32(PartyField[partyNum].Leader);
            for (int i = 0, loopTo = Core.Constant.MAX_PARTY_MEMBERS; i < loopTo; i++)
                buffer.WriteInt32(PartyField[partyNum].Member[i]);
            buffer.WriteInt32(PartyField[partyNum].MemberCount);

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
            partyNum = Core.Type.TempPlayer[index].InParty;
            if (partyNum >= 0)
            {
                // send party data
                buffer.WriteInt32(1);
                buffer.WriteInt32(PartyField[partyNum].Leader);
                var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
                for (i = 0; i < loopTo; i++)
                    buffer.WriteInt32(PartyField[partyNum].Member[i]);
                buffer.WriteInt32(PartyField[partyNum].MemberCount);
            }
            else
            {
                // send clear command
                buffer.WriteInt32(0);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendPartyVitals(int partyNum, int index)
        {
            ByteStream buffer;
            int i;

            buffer = new ByteStream(4);
            buffer.WriteInt32((byte)ServerPackets.SPartyVitals);
            buffer.WriteInt32(index);

            var loopTo = (byte)VitalType.Count;
            for (i = 0; i < loopTo; i++)
                buffer.WriteInt32(Core.Type.Player[index].Vital[i]);

            byte[] data = buffer.ToArray();
            SendDataToParty(partyNum, data);
            buffer.Dispose();
        }

        #endregion

        #region Incoming Packets

        internal static void Packet_PartyRquest(int index, ref byte[] data)
        {
            // Prevent partying with self
            if (Core.Type.TempPlayer[index].Target == index)
                return;

            // make sure it's a valid target
            if (Core.Type.TempPlayer[index].TargetType != (byte)TargetType.Player)
                return;

            // make sure they're connected and on the same map
            if (GetPlayerMap(Core.Type.TempPlayer[index].Target) != GetPlayerMap(index))
                return;

            // init the request
            Invite(index, Core.Type.TempPlayer[index].Target);
        }

        internal static void Packet_AcceptParty(int index, ref byte[] data)
        {
            InviteAccept(Core.Type.TempPlayer[index].PartyInvite, index);
        }

        internal static void Packet_DeclineParty(int index, ref byte[] data)
        {
            InviteDecline(Core.Type.TempPlayer[index].PartyInvite, index);
        }

        internal static void Packet_LeaveParty(int index, ref byte[] data)
        {
            PlayerLeave(index);
        }

        internal static void Packet_PartyChatMsg(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);

            PartyMsg(index, buffer.ReadString());

            buffer.Dispose();
        }

        #endregion

        public static void ClearParty()
        {
            int i;

            var loopTo = Core.Constant.MAX_PARTIES;
            for (i = 0; i < loopTo; i++)
                ClearParty(i);

        }

        public static void ClearParty(int partyNum)
        {
            PartyField[partyNum].Leader = -1;
            PartyField[partyNum].MemberCount = 0;
            PartyField[partyNum].Member = new int[Core.Constant.MAX_PARTY_MEMBERS];
        }

        internal static void PartyMsg(int partyNum, string msg)
        {
            int i;

            // send message to all people
            var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
            for (i = 0; i < loopTo; i++)
            {
                // exist?
                if (PartyField[partyNum].Member[i] >= 0)
                {
                    // make sure they're logged on
                    NetworkSend.PlayerMsg(PartyField[partyNum].Member[i], msg, (int) ColorType.BrightBlue);
                }
            }
        }

        private static void RemoveFromParty(int index, int partyNum)
        {
            for (int i = 0, loopTo = Core.Constant.MAX_PARTY_MEMBERS; i < loopTo; i++)
            {
                if (PartyField[partyNum].Member[i] == index)
                {
                    PartyField[partyNum].Member[i] = -1;
                    Core.Type.TempPlayer[index].InParty = -1;
                    Core.Type.TempPlayer[index].PartyInvite = -1;
                    break;
                }
            }

            CountMembers(partyNum);
            SendPartyUpdate(partyNum);
            SendPartyUpdateTo(index);
        }

        internal static void PlayerLeave(int index)
        {
            int partyNum;
            int i;

            partyNum = Core.Type.TempPlayer[index].InParty;

            if (partyNum >= 0)
            {
                // find out how many members we have
                CountMembers(partyNum);

                // make sure there's more than 2 people
                if (PartyField[partyNum].MemberCount > 2)
                {

                    // check if leader
                    if (PartyField[partyNum].Leader == index)
                    {
                        // set next person down as leader
                        var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
                        for (i = 0; i < loopTo; i++)
                        {
                            if (PartyField[partyNum].Member[i] > 0 & PartyField[partyNum].Member[i] != index)
                            {
                                PartyField[partyNum].Leader = PartyField[partyNum].Member[i];
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
                    RemoveFromParty(PartyField[partyNum].Leader, partyNum);

                    // clear out everyone's party
                    var loopTo1 = Core.Constant.MAX_PARTY_MEMBERS;
                    for (i = 0; i < loopTo1; i++)
                    {
                        index = PartyField[partyNum].Member[i];
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

        internal static void Invite(int index, int target)
        {
            int partyNum;
            int i;

            // make sure they're not busy
            if (Core.Type.TempPlayer[target].PartyInvite >= 0 | Core.Type.TempPlayer[target].TradeRequest >= 0)
            {
                // they've already got a request for trade/party
                NetworkSend.PlayerMsg(index, "This player is busy.", (int) ColorType.BrightRed);
                return;
            }

            // make syure they're not in a party
            if (Core.Type.TempPlayer[target].InParty >= 0)
            {
                // they're already in a party
                NetworkSend.PlayerMsg(index, "This player is already in a party.", (int) ColorType.BrightRed);
                return;
            }

            // check if we're in a party
            if (Core.Type.TempPlayer[index].InParty >= 0)
            {
                partyNum = Core.Type.TempPlayer[index].InParty;
                // make sure we're the leader
                if (PartyField[partyNum].Leader == index)
                {
                    // got a blank slot?
                    var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
                    for (i = 0; i < loopTo; i++)
                    {
                        if (PartyField[partyNum].Member[i] == -1)
                        {
                            // send the invitation
                            SendPartyInvite(target, index);

                            // set the invite target
                            Core.Type.TempPlayer[target].PartyInvite = index;

                            // let them know
                            NetworkSend.PlayerMsg(index, "Party invitation sent.", (int) ColorType.Pink);
                            return;
                        }
                    }
                    // no room
                    NetworkSend.PlayerMsg(index, "Party is full.", (int) ColorType.BrightRed);
                    return;
                }
                else
                {
                    // not the leader
                    NetworkSend.PlayerMsg(index, "You are not the party leader.", (int) ColorType.BrightRed);
                    return;
                }
            }
            else
            {
                // not in a party - doesn't matter!
                SendPartyInvite(target, index);

                // set the invite target
                Core.Type.TempPlayer[target].PartyInvite = index;

                // let them know
                NetworkSend.PlayerMsg(index, "Party invitation sent.", (int) ColorType.Pink);
                return;
            }
        }

        internal static void InviteAccept(int index, int target)
        {
            var partyNum = default(int);
            int i;

            // check if already in a party
            if (Core.Type.TempPlayer[index].InParty >= 0)
            {
                // get the partynumber
                partyNum = Core.Type.TempPlayer[index].InParty;
                // got a blank slot?
                var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
                for (i = 0; i < loopTo; i++)
                {
                    if (PartyField[partyNum].Member[i] == -1)
                    {
                        // add to the party
                        PartyField[partyNum].Member[i] = target;
                        // recount party
                        CountMembers(partyNum);
                        // send update to all - including new player
                        SendPartyUpdate(partyNum);
                        SendPartyVitals(partyNum, target);
                        // let everyone know they've joined
                        PartyMsg(partyNum, string.Format("{0} has joined the party.", GetPlayerName(target)));
                        // add them in
                        Core.Type.TempPlayer[target].InParty = (byte)partyNum;
                        return;
                    }
                }
                // no empty slots - let them know
                NetworkSend.PlayerMsg(index, "Party is full.", (int) ColorType.BrightRed);
                NetworkSend.PlayerMsg(target, "Party is full.", (int) ColorType.BrightRed);
                return;
            }
            else
            {
                // not in a party. Create one with the new person.
                var loopTo1 = Core.Constant.MAX_PARTIES;
                for (i = 0; i < loopTo1; i++)
                {
                    // find blank party
                    if (!(PartyField[i].Leader > -1))
                    {
                        partyNum = i;
                        break;
                    }
                }
                // create the party
                PartyField[partyNum].MemberCount = 2;
                PartyField[partyNum].Leader = index;
                PartyField[partyNum].Member[1] = index;
                PartyField[partyNum].Member[2] = target;
                SendPartyUpdate(partyNum);
                SendPartyVitals(partyNum, index);
                SendPartyVitals(partyNum, target);

                // let them know it's created
                PartyMsg(partyNum, "Party created.");
                PartyMsg(partyNum, string.Format("{0} has joined the party.", GetPlayerName(index)));

                // clear the invitation
                Core.Type.TempPlayer[target].PartyInvite = -1;

                // add them to the party
                Core.Type.TempPlayer[index].InParty = (byte)partyNum;
                Core.Type.TempPlayer[target].InParty = (byte)partyNum;
                return;
            }
        }

        internal static void InviteDecline(int index, int target)
        {
            NetworkSend.PlayerMsg(index, string.Format("{0} has declined to join your party.", GetPlayerName(target)), (int) ColorType.BrightRed);
            NetworkSend.PlayerMsg(target, "You declined to join the party.", (int) ColorType.Yellow);

            // clear the invitation
            Core.Type.TempPlayer[target].PartyInvite = -1;
        }

        internal static void CountMembers(int partyNum)
        {
            int i;
            var highindex = default(int);
            int x;

            // find the high index
            for (i = Core.Constant.MAX_PARTY_MEMBERS - 1; i >= 0; i -= 1)
            {
                if (PartyField[partyNum].Member[i] >= 0)
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
                if (PartyField[partyNum].Member[i] == -1)
                {
                    // is it lower than the high index?
                    if (i < highindex)
                    {
                        // move everyone down a slot
                        var loopTo1 = Core.Constant.MAX_PARTY_MEMBERS;
                        for (x = i; x < (int)loopTo1; x++)
                        {
                            PartyField[partyNum].Member[x] = PartyField[partyNum].Member[x + 1];
                            PartyField[partyNum].Member[x + 1] = 0;
                        }
                    }
                    else
                    {
                        // not lower - highindex is count
                        PartyField[partyNum].MemberCount = highindex;
                        return;
                    }
                }

                // check if we've reached the max party members
                if (i == Core.Constant.MAX_PARTY_MEMBERS - 1)
                {
                    if (highindex == i)
                    {
                        PartyField[partyNum].MemberCount = Core.Constant.MAX_PARTY_MEMBERS;
                        return;
                    }
                }
            }

            // if we're here it means that we need to re-count again
            CountMembers(partyNum);
        }

        internal static void ShareExp(int partyNum, int exp, int index, int mapNum)
        {
            int expShare;
            int leftOver;
            int i;
            int tmpindex;
            var loseMemberCount = default(byte);

            // check if it's worth sharing
            if (!(exp >= PartyField[partyNum].MemberCount))
            {
                // no party - keep exp for self
                Event.GivePlayerExp(index, exp);
                return;
            }

            // check members in others maps
            var loopTo = Core.Constant.MAX_PARTY_MEMBERS;
            for (i = 0; i < loopTo; i++)
            {
                tmpindex = PartyField[partyNum].Member[i];
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
            expShare = exp / (PartyField[partyNum].MemberCount - loseMemberCount);
            leftOver = exp % (PartyField[partyNum].MemberCount - loseMemberCount);

            // loop through and give everyone exp
            var loopTo1 = Core.Constant.MAX_PARTY_MEMBERS;
            for (i = 0; i < loopTo1; i++)
            {
                tmpindex = PartyField[partyNum].Member[i];
                // existing member?
                if (tmpindex > -1)
                {
                    // playing?
                    if (NetworkConfig.Socket.IsConnected(tmpindex) & NetworkConfig.IsPlaying(tmpindex))
                    {
                        if (GetPlayerMap(tmpindex) == mapNum)
                        {
                            // give them their share
                            Event.GivePlayerExp(tmpindex, expShare);
                        }
                    }
                }
            }

            // give the remainder to a random member
            if (!(leftOver == 0))
            {
                tmpindex = PartyField[partyNum].Member[(int)Math.Round(General.Random.NextDouble(1d, PartyField[partyNum].MemberCount))];
                // give the exp
                Event.GivePlayerExp(tmpindex, leftOver);
            }

        }

        public static void PartyWarp(int index, int mapNum, int x, int y)
        {
            int i;

            if (Core.Type.TempPlayer[index].InParty >= 0)
            {
                if (Conversions.ToBoolean(PartyField[Core.Type.TempPlayer[index].InParty].Leader))
                {
                    var loopTo = PartyField[Core.Type.TempPlayer[index].InParty].MemberCount;
                    for (i = 0; i < loopTo; i++)
                        Player.PlayerWarp(PartyField[Core.Type.TempPlayer[index].InParty].Member[i], mapNum, x, y);
                }
            }

        }

        public static bool IsPlayerInParty(int index)
        {
            bool IsPlayerInPartyRet = default;
            if (index < 0 | index >= Core.Constant.MAX_PLAYERS | !Core.Type.TempPlayer[index].InGame)
                return IsPlayerInPartyRet;

            if (Core.Type.TempPlayer[index].InParty >= 0)
                IsPlayerInPartyRet = Conversions.ToBoolean(1);
            return IsPlayerInPartyRet;
        }

        internal static int GetPlayerParty(int index)
        {
            int GetPlayerPartyRet = default;
            if (index < 0 | index >= Core.Constant.MAX_PLAYERS | !Core.Type.TempPlayer[index].InGame)
                return GetPlayerPartyRet;
            GetPlayerPartyRet = Core.Type.TempPlayer[index].InParty;
            return GetPlayerPartyRet;
        }

    }
}