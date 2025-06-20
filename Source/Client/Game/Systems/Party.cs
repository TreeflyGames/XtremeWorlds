using Core;
using Mirage.Sharp.Asfw;

namespace Client
{

    public class Party
    {

        #region Database

        public static void ClearParty()
        {
            Core.Type.MyParty = new Core.Type.PartyStruct()
            {
                Leader = 0,
                MemberCount = 0
            };
            Core.Type.MyParty.Member = new int[5];
        }

        #endregion

        #region Incoming Packets

        public static void Packet_PartyInvite(ref byte[] data)
        {
            string name;
            var buffer = new ByteStream(data);

            name = buffer.ReadString();
            GameLogic.Dialogue("Party Invite", name + " has invited you to a party.", "Would you like to join?", (byte)Core.Enum.DialogueType.Party, (byte)Core.Enum.DialogueStyle.YesNo);

            buffer.Dispose();
        }

        public static void Packet_PartyUpdate(ref byte[] data)
        {
            int i;
            int inParty;
            var buffer = new ByteStream(data);

            inParty = buffer.ReadInt32();

            // exit out if we're not in a party
            if (inParty == -1)
            {
                ClearParty();
                Gui.UpdatePartyInterface();
                // exit out early
                buffer.Dispose();
                return;
            }

            // carry on otherwise
            Core.Type.MyParty.Leader = buffer.ReadInt32();
            for (i = 0; i < Constant.MAX_PARTY_MEMBERS; i++)
                Core.Type.MyParty.Member[i] = buffer.ReadInt32();
            Core.Type.MyParty.MemberCount = buffer.ReadInt32();

            Gui.UpdatePartyInterface();

            buffer.Dispose();
        }

        public static void Packet_PartyVitals(ref byte[] data)
        {
            int playerNum;
            var partyindex = -1;
            var buffer = new ByteStream(data);

            // which player?
            playerNum = buffer.ReadInt32();

            // find the party number
            for (int i = 0; i < Constant.MAX_PARTY_MEMBERS; i++)
            {
                if (Core.Type.MyParty.Member[i] == playerNum)
                {
                    partyindex = i;
                }
            }

            // exit out if wrong data
            if (partyindex < 0 | partyindex >= Constant.MAX_PARTY_MEMBERS)
                return;

            // set vitals
            for (int i = 0; i < (int)Core.Enum.VitalType.Count; i++)
                Core.Type.Player[playerNum].Vital[i] = buffer.ReadInt32();

            GameLogic.UpdatePartyBars();

            buffer.Dispose();
        }

        #endregion

        #region Outgoing Packets

        public static void SendPartyRequest(string name)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestParty);
            buffer.WriteString(name);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendAcceptParty()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CAcceptParty);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendDeclineParty()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CDeclineParty);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendLeaveParty()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CLeaveParty);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendPartyChatMsg(string text)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CPartyChatMsg);
            buffer.WriteString(text);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        #endregion

    }
}