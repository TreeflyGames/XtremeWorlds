using Core;
using Mirage.Sharp.Asfw;

namespace Client
{

    static class Trade
    {

        #region Globals & Type

        internal static int InTrade;
        internal static int TradeX;
        internal static int TradeY;
        internal static string TheirWorth;
        internal static string YourWorth;

        #endregion

        #region Incoming Packets
        public static void Packet_TradeInvite(ref byte[] data)
        {
            int requester;
            var buffer = new ByteStream(data);

            requester = buffer.ReadInt32();
            GameLogic.Dialogue("Trade Invite", string.Format(Languages.Language.Trade.Request, Core.Type.Player[requester].Name), "", (byte)Core.Enum.DialogueType.Trade, (byte)Core.Enum.DialogueStyle.YesNo);

            buffer.Dispose();
        }

        public static void Packet_Trade(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            InTrade = buffer.ReadInt32();

            GameLogic.ShowTrade();

            buffer.Dispose();
        }

        public static void Packet_CloseTrade(ref byte[] data)
        {
            InTrade = 0;
            Gui.HideWindow(Gui.GetWindowIndex("winTrade"));
        }

        public static void Packet_TradeUpdate(ref byte[] data)
        {
            int datatype;
            var buffer = new ByteStream(data);

            datatype = buffer.ReadInt32();

            if (datatype == 0) // ours!
            {
                for (int i = 0; i <= Constant.MAX_INV; i++)
                {
                    Core.Type.TradeYourOffer[i].Num = buffer.ReadInt32();
                    Core.Type.TradeYourOffer[i].Value = buffer.ReadInt32();
                }
                YourWorth = buffer.ReadInt32().ToString();
                Gui.Windows[Gui.GetWindowIndex("winTrade")].Controls[(int)Gui.GetControlIndex("winTrade", "lblYourValue")].Text = YourWorth + "g";
            }
            else if (datatype == 1) // theirs
            {
                for (int i = 0; i <= Constant.MAX_INV; i++)
                {
                    Core.Type.TradeTheirOffer[i].Num = buffer.ReadInt32();
                    Core.Type.TradeTheirOffer[i].Value = buffer.ReadInt32();
                }
                TheirWorth = buffer.ReadInt32().ToString();
                Gui.Windows[Gui.GetWindowIndex("winTrade")].Controls[(int)Gui.GetControlIndex("winTrade", "lblTheirValue")].Text = TheirWorth + "g";
            }

            buffer.Dispose();
        }

        public static void Packet_TradeStatus(ref byte[] data)
        {
            int tradestatus;
            var buffer = new ByteStream(data);

            tradestatus = buffer.ReadInt32();

            switch (tradestatus)
            {
                case 0: // clear
                    {
                        Gui.Windows[Gui.GetWindowIndex("winTrade")].Controls[(int)Gui.GetControlIndex("winTrade", "lblStatus")].Text = "Choose items to offer.";
                        break;
                    }
                case 1: // they've accepted
                    {
                        Gui.Windows[Gui.GetWindowIndex("winTrade")].Controls[(int)Gui.GetControlIndex("winTrade", "lblStatus")].Text = "Other player has accepted.";
                        break;
                    }
                case 2: // you've accepted
                    {
                        Gui.Windows[Gui.GetWindowIndex("winTrade")].Controls[(int)Gui.GetControlIndex("winTrade", "lblStatus")].Text = "Waiting for other player to accept.";
                        break;
                    }
                case 3: // no room
                    {
                        Gui.Windows[Gui.GetWindowIndex("winTrade")].Controls[(int)Gui.GetControlIndex("winTrade", "lblStatus")].Text = "Not enough inventory space.";
                        break;
                    }
            }

            buffer.Dispose();
        }

        #endregion

        #region Outgoing Packets

        internal static void SendAcceptTrade()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CAcceptTrade);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        internal static void SendDeclineTrade()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CDeclineTrade);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendTradeRequest(string name)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CTradeInvite);
            buffer.WriteString(name);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendHandleTradeInvite(byte answer)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CHandleTradeInvite);
            buffer.WriteInt32(answer);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        internal static void TradeItem(int invslot, int amount)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CTradeItem);
            buffer.WriteInt32(invslot);
            buffer.WriteInt32(amount);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        internal static void UntradeItem(int invslot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CUntradeItem);
            buffer.WriteInt32(invslot);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

    }
}