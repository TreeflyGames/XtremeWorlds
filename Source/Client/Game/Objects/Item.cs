﻿using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;

namespace Client
{

    static class Item
    {

        #region Database
        internal static void ClearItem(int index)
        {
            Core.Type.Item[index] = default;
            for (int X = 0; X <= (int)Core.Enum.StatType.Count - 1; X++)
                Core.Type.Item[index].Add_Stat = new byte[X + 1];

            for (int X = 0; X <= (int)Core.Enum.StatType.Count - 1; X++)
                Core.Type.Item[index].Stat_Req = new byte[X + 1];

            Core.Type.Item[index].Name = "";
            Core.Type.Item[index].Description = "";
            GameState.Item_Loaded[index] = 0;
        }

        public static void ClearItems()
        {
            int i;

            Core.Type.Item = new Core.Type.ItemStruct[501];

            for (i = 0; i <= Constant.MAX_ITEMS - 1; i++)
                ClearItem(i);

        }

        internal static void ClearChangedItem()
        {
            GameState.Item_Changed = new bool[501];
        }

        public static void StreamItem(int itemNum)
        {
            if (Conversions.ToBoolean(Operators.OrObject(itemNum > 0 & string.IsNullOrEmpty(Core.Type.Item[itemNum].Name), Operators.ConditionalCompareObjectEqual(GameState.Item_Loaded[itemNum], 0, false))))
            {
                GameState.Item_Loaded[itemNum] = 1;
                SendRequestItem(itemNum);
            }
        }

        #endregion

        #region Incoming Packets

        internal static void Packet_UpdateItem(ref byte[] data)
        {
            int n;
            int i;
            var buffer = new ByteStream(data);

            n = buffer.ReadInt32();

            // Update the item
            Core.Type.Item[n].AccessReq = buffer.ReadInt32();

            for (i = 0; i <= (int)Core.Enum.StatType.Count - 1; i++)
                Core.Type.Item[n].Add_Stat[i] = (byte)buffer.ReadInt32();

            Core.Type.Item[n].Animation = buffer.ReadInt32();
            Core.Type.Item[n].BindType = (byte)buffer.ReadInt32();
            Core.Type.Item[n].JobReq = buffer.ReadInt32();
            Core.Type.Item[n].Data1 = buffer.ReadInt32();
            Core.Type.Item[n].Data2 = buffer.ReadInt32();
            Core.Type.Item[n].Data3 = buffer.ReadInt32();
            Core.Type.Item[n].TwoHanded = buffer.ReadInt32();
            Core.Type.Item[n].LevelReq = buffer.ReadInt32();
            Core.Type.Item[n].Mastery = (byte)buffer.ReadInt32();
            Core.Type.Item[n].Name = buffer.ReadString();
            Core.Type.Item[n].Paperdoll = buffer.ReadInt32();
            Core.Type.Item[n].Icon = buffer.ReadInt32();
            Core.Type.Item[n].Price = buffer.ReadInt32();
            Core.Type.Item[n].Rarity = (byte)buffer.ReadInt32();
            Core.Type.Item[n].Speed = buffer.ReadInt32();

            Core.Type.Item[n].Stackable = (byte)buffer.ReadInt32();
            Core.Type.Item[n].Description = buffer.ReadString();

            for (i = 0; i <= (int)Core.Enum.StatType.Count - 1; i++)
                Core.Type.Item[n].Stat_Req[i] = (byte)buffer.ReadInt32();

            Core.Type.Item[n].Type = (byte)buffer.ReadInt32();
            Core.Type.Item[n].SubType = (byte)buffer.ReadInt32();

            Core.Type.Item[n].KnockBack = (byte)buffer.ReadInt32();
            Core.Type.Item[n].KnockBackTiles = (byte)buffer.ReadInt32();

            Core.Type.Item[n].Projectile = buffer.ReadInt32();
            Core.Type.Item[n].Ammo = buffer.ReadInt32();

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

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

    }
}