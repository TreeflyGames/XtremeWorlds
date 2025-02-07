using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;

namespace Client
{

    static class Shop
    {

        #region Database

        public static void ClearShop(int index)
        {
            Core.Type.Shop[index] = default;
            Core.Type.Shop[index].Name = "";
            Core.Type.Shop[index].TradeItem = new Core.Type.TradeItemStruct[Constant.MAX_TRADES];
            for (int x = 0; x < Constant.MAX_TRADES; x++)
            {            
                Core.Type.Shop[index].TradeItem[x].Item = -1;
                Core.Type.Shop[index].TradeItem[x].CostItem = - 1;
            }
            GameState.Shop_Loaded[index] = 0;
        }

        public static void ClearShops()
        {
            int i;

            Core.Type.Shop = new Core.Type.ShopStruct[Constant.MAX_SHOPS];

            for (i = 0; i < Constant.MAX_SHOPS; i++)
                ClearShop(i);

        }

        public static void StreamShop(int shopNum)
        {
            if (Conversions.ToBoolean(Operators.OrObject(shopNum >= 0 & string.IsNullOrEmpty(Core.Type.Shop[shopNum].Name), Operators.ConditionalCompareObjectEqual(GameState.Shop_Loaded[shopNum], 0, false))))
            {
                GameState.Shop_Loaded[shopNum] = 1;
                SendRequestShop(shopNum);
            }
        }

        #endregion

        #region Incoming Packets

        internal static void Packet_OpenShop(ref byte[] data)
        {
            int shopnum;
            var buffer = new ByteStream(data);

            shopnum = buffer.ReadInt32();

            GameLogic.OpenShop(shopnum);

            buffer.Dispose();
        }

        internal static void Packet_ResetShopAction(ref byte[] data)
        {
            GameState.ShopAction = 0;
        }

        internal static void Packet_UpdateShop(ref byte[] data)
        {
            int shopnum;
            var buffer = new ByteStream(data);
            shopnum = buffer.ReadInt32();

            Core.Type.Shop[shopnum].BuyRate = buffer.ReadInt32();
            Core.Type.Shop[shopnum].Name = buffer.ReadString();

            for (int i = 0; i < Constant.MAX_TRADES; i++)
            {
                Core.Type.Shop[shopnum].TradeItem[i].CostItem = buffer.ReadInt32();
                Core.Type.Shop[shopnum].TradeItem[i].CostValue = buffer.ReadInt32();
                Core.Type.Shop[shopnum].TradeItem[i].Item = buffer.ReadInt32();
                Core.Type.Shop[shopnum].TradeItem[i].ItemValue = buffer.ReadInt32();
            }

            if (Core.Type.Shop[shopnum].Name is null)
                Core.Type.Shop[shopnum].Name = "";

            buffer.Dispose();
        }

        #endregion

        #region Outgoing Packets

        internal static void SendRequestShop(int shopNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestShop);
            buffer.WriteInt32(shopNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        internal static void BuyItem(int shopSlot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CBuyItem);
            buffer.WriteInt32(shopSlot);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        internal static void SellItem(int invslot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSellItem);
            buffer.WriteInt32(invslot);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

    }
}