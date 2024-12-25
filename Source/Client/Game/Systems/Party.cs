using Core;
using Mirage.Sharp.Asfw;

namespace Client
{

    static class Party
    {

        #region Database

        public static void ClearParty()
        {
            Core.Type.Party = new Core.Type.PartyStruct()
            {
                Leader = 0,
                MemberCount = 0
            };
            Core.Type.Party.Member = new int[5];
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
            if (inParty == 0)
            {
                ClearParty();
                Gui.UpdatePartyInterface();
                // exit out early
                buffer.Dispose();
                return;
            }

            // carry on otherwise
            Core.Type.Party.Leader = buffer.ReadInt32();
            for (i = 0; i <= Constant.MAX_PARTY_MEMBERS; i++)
                Core.Type.Party.Member[i] = buffer.ReadInt32();
            Core.Type.Party.MemberCount = buffer.ReadInt32();

            Gui.UpdatePartyInterface();

            buffer.Dispose();
        }

        public static void Packet_PartyVitals(ref byte[] data)
        {
            int playerNum;
            var partyindex = default(int);
            var buffer = new ByteStream(data);

            // which player?
            playerNum = buffer.ReadInt32();

            // find the party number
            for (int i = 0; i <= Constant.MAX_PARTY_MEMBERS; i++)
            {
                if (Core.Type.Party.Member[i] == playerNum)
                {
                    partyindex = i;
                }
            }

            // exit out if wrong data
            if (partyindex <= 0 | partyindex > Constant.MAX_PARTY_MEMBERS)
                return;

            // set vitals
            for (int i = 0; i <= (int)Core.Enum.VitalType.Count - 1; i++)
                Core.Type.Player[playerNum].Vital[i] = buffer.ReadInt32();

            GameLogic.UpdatePartyBars();

            buffer.Dispose();
        }

        #endregion

        #region Outgoing Packets

        internal static void SendPartyRequest(string name)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestParty);
            buffer.WriteString(name);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        internal static void SendAcceptParty()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CAcceptParty);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        internal static void SendDeclineParty()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CDeclineParty);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        internal static void SendLeaveParty()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CLeaveParty);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        internal static void SendPartyChatMsg(string text)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CPartyChatMsg);
            buffer.WriteString(text);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

    }
}