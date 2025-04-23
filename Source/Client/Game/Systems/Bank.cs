﻿using Core;
using static Core.Global.Command;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;

namespace Client
{

    public class Bank
    {

        #region Database

        public static void ClearBanks()
        {
            int i;
            int x;

            for (x = 0; x < Constant.MAX_PLAYERS; x++)
            {
                Core.Type.Bank[x].Item = new Core.Type.PlayerInvStruct[(Constant.MAX_BANK + 1)];

                for (i = 0; i < Constant.MAX_BANK; i++)
                {
                    Core.Type.Bank[x].Item[i].Num = -1;
                    Core.Type.Bank[x].Item[i].Value = 0;
                }
            }
        }

        #endregion

        #region Incoming Packets

        public static void Packet_OpenBank(ref byte[] data)
        {
            int i;
            int x;
            var buffer = new ByteStream(data);

            for (i = 0; i < Constant.MAX_BANK; i++)
            {
                SetBank(GameState.MyIndex, (byte)i, buffer.ReadInt32());
                SetBankValue(GameState.MyIndex, (byte)i, buffer.ReadInt32());
            }

            GameState.InBank = Conversions.ToBoolean(1);

            if (!(Gui.Windows[Gui.GetWindowIndex("winBank")].Visible == true))
            {
                Gui.ShowWindow(Gui.GetWindowIndex("winBank"), resetPosition: false);
            }

            buffer.Dispose();
        }

        #endregion

        #region Outgoing Packets

        public static void DepositItem(int invslot, int amount)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CDepositItem);
            buffer.WriteInt32(invslot);
            buffer.WriteInt32(amount);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void WithdrawItem(byte bankSlot, int amount)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CWithdrawItem);
            buffer.WriteByte(bankSlot);
            buffer.WriteInt32(amount);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void ChangeBankSlots(int oldSlot, int newSlot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CChangeBankSlots);
            buffer.WriteInt32(oldSlot);
            buffer.WriteInt32(newSlot);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void CloseBank()
        {
            if (Gui.Windows[Gui.GetWindowIndex("winBank")].Visible == true)
            {
                Gui.HideWindow(Gui.GetWindowIndex("winBank"));
                Gui.HideWindow(Gui.GetWindowIndex("winDescription"));
            }

            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CCloseBank);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

            GameState.InBank = Conversions.ToBoolean(0);
        }

        #endregion

    }
}