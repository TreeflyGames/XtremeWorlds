using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;

namespace Client
{

    public class Item
    {

        #region Database
        public static void ClearItem(int index)
        {
            int statCount = System.Enum.GetValues(typeof(Stat)).Length;
                
            Core.Data.Item[index] = default;
            Array.Resize(ref Core.Data.Item[index].Add_Stat, statCount);
            Array.Resize(ref Core.Data.Item[index].Stat_Req, statCount);

            Core.Data.Item[index].Name = "";
            Core.Data.Item[index].Description = "";
            GameState.Item_Loaded[index] = 0;
        }

        public static void ClearItems()
        {
            int i;

            Core.Data.Item = new Core.Type.Item[Core.Constant.MAX_ITEMS];

            for (i = 0; i < Constant.MAX_ITEMS; i++)
                ClearItem(i);

        }

        public static void ClearChangedItem()
        {
            GameState.Item_Changed = new bool[Core.Constant.MAX_ITEMS];
        }

        public static void StreamItem(int itemNum)
        {
            if (itemNum >= 0 && string.IsNullOrEmpty(Core.Data.Item[itemNum].Name) && GameState.Item_Loaded[itemNum] == 0)
            {
                GameState.Item_Loaded[itemNum] = 1;
                SendRequestItem(itemNum);
            }
        }

        #endregion

        #region Incoming Packets

        public static void Packet_UpdateItem(ref byte[] data)
        {
            int n;
            int i;
            var buffer = new ByteStream(data);

            n = buffer.ReadInt32();

            // Update the item
            Core.Data.Item[n].AccessReq = buffer.ReadInt32();

            int statCount = System.Enum.GetValues(typeof(Stat)).Length;
            for (i = 0; i < statCount; i++)
                Core.Data.Item[n].Add_Stat[i] = (byte)buffer.ReadInt32();

            Core.Data.Item[n].Animation = buffer.ReadInt32();
            Core.Data.Item[n].BindType = (byte)buffer.ReadInt32();
            Core.Data.Item[n].JobReq = buffer.ReadInt32();
            Core.Data.Item[n].Data1 = buffer.ReadInt32();
            Core.Data.Item[n].Data2 = buffer.ReadInt32();
            Core.Data.Item[n].Data3 = buffer.ReadInt32();
            Core.Data.Item[n].LevelReq = buffer.ReadInt32();
            Core.Data.Item[n].Mastery = (byte)buffer.ReadInt32();
            Core.Data.Item[n].Name = buffer.ReadString();
            Core.Data.Item[n].Paperdoll = buffer.ReadInt32();
            Core.Data.Item[n].Icon = buffer.ReadInt32();
            Core.Data.Item[n].Price = buffer.ReadInt32();
            Core.Data.Item[n].Rarity = (byte)buffer.ReadInt32();
            Core.Data.Item[n].Speed = buffer.ReadInt32();

            Core.Data.Item[n].Stackable = (byte)buffer.ReadInt32();
            Core.Data.Item[n].Description = buffer.ReadString();

            for (i = 0; i < statCount; i++)
                Core.Data.Item[n].Stat_Req[i] = (byte)buffer.ReadInt32();

            Core.Data.Item[n].Type = (byte)buffer.ReadInt32();
            Core.Data.Item[n].SubType = (byte)buffer.ReadInt32();

            Core.Data.Item[n].KnockBack = (byte)buffer.ReadInt32();
            Core.Data.Item[n].KnockBackTiles = (byte)buffer.ReadInt32();

            Core.Data.Item[n].Projectile = buffer.ReadInt32();
            Core.Data.Item[n].Ammo = buffer.ReadInt32();

            if (n == GameState.descLastItem)
            {
                GameState.descLastType = 0;
                GameState.descLastItem = 0L;
            }

            buffer.Dispose();

        }

        #endregion

        #region Outgoing Packets

        public static void SendRequestItem(int itemNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestItem);
            buffer.WriteInt32(itemNum);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        #endregion

    }
}