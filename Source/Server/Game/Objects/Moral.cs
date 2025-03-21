using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Core.Enum;
using static Core.Packets;
using static Core.Type;
using static Core.Global.Command;

namespace Server
{

    internal static class Moral
    {

        #region Database
        public static void ClearMorals()
        {
            int i;

            Core.Type.Moral = new Core.Type.MoralStruct[Core.Constant.MAX_MORALS];

            var loopTo = Core.Constant.MAX_MORALS;
            for (i = 0; i < loopTo; i++)
                ClearMoral(i);
        }

        public static void ClearMoral(int moralNum)
        {
            Core.Type.Moral[moralNum].Name = "";
            Core.Type.Moral[moralNum].Color = 0;
            Core.Type.Moral[moralNum].CanCast = Conversions.ToBoolean(0);
            Core.Type.Moral[moralNum].CanDropItem = Conversions.ToBoolean(0);
            Core.Type.Moral[moralNum].CanPK = Conversions.ToBoolean(0);
            Core.Type.Moral[moralNum].CanPickupItem = Conversions.ToBoolean(0);
            Core.Type.Moral[moralNum].CanUseItem = Conversions.ToBoolean(0);
            Core.Type.Moral[moralNum].DropItems = Conversions.ToBoolean(0);
            Core.Type.Moral[moralNum].LoseExp = Conversions.ToBoolean(0);
            Core.Type.Moral[moralNum].NPCBlock = Conversions.ToBoolean(0);
            Core.Type.Moral[moralNum].PlayerBlock = Conversions.ToBoolean(0);
        }

        public static async Task LoadMoralAsync(int moralNum)
        {
            JObject data;

            data = await Database.SelectRowAsync(moralNum, "moral", "data");

            if (data is null)
            {
                ClearMoral(moralNum);
                return;
            }

            var moralData = JObject.FromObject(data).ToObject<MoralStruct>();
            Core.Type.Moral[moralNum] = moralData;
        }

        public static async Task LoadMoralsAsync()
        {
            int i;

            var loopTo = Core.Constant.MAX_MORALS;
            for (i = 0; i < loopTo; i++)
                await Task.Run(() => LoadMoralAsync(i));
        }

        public static void SaveMoral(int moralNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Moral[moralNum]).ToString();

            if (Database.RowExists(moralNum, "moral"))
            {
                Database.UpdateRow(moralNum, json, "moral", "data");
            }
            else
            {
                Database.InsertRow(moralNum, json, "moral");
            }
        }

        public static void SaveMorals()
        {
            int i;

            var loopTo = Core.Constant.MAX_MORALS;
            for (i = 0; i < loopTo; i++)
                SaveMoral(i);
        }

        public static byte[] MoralData(int moralNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(moralNum);
            buffer.WriteString(Core.Type.Moral[moralNum].Name);
            buffer.WriteByte(Core.Type.Moral[moralNum].Color);
            buffer.WriteBoolean(Core.Type.Moral[moralNum].NPCBlock);
            buffer.WriteBoolean(Core.Type.Moral[moralNum].PlayerBlock);
            buffer.WriteBoolean(Core.Type.Moral[moralNum].DropItems);
            buffer.WriteBoolean(Core.Type.Moral[moralNum].CanCast);
            buffer.WriteBoolean(Core.Type.Moral[moralNum].CanDropItem);
            buffer.WriteBoolean(Core.Type.Moral[moralNum].CanPickupItem);
            buffer.WriteBoolean(Core.Type.Moral[moralNum].CanPK);
            buffer.WriteBoolean(Core.Type.Moral[moralNum].DropItems);
            buffer.WriteBoolean(Core.Type.Moral[moralNum].LoseExp);

            return buffer.ToArray();
        }

        #endregion

        #region Outgoing Packets

        public static void SendMorals(int index)
        {
            int i;

            var loopTo = Core.Constant.MAX_MORALS;
            for (i = 0; i < loopTo; i++)
            {
                if (Strings.Len(Core.Type.Moral[i].Name) > 0)
                {
                    SendUpdateMoralTo(index, i);
                }
            }

        }

        public static void SendUpdateMoralTo(int index, int moralNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SUpdateMoral);

            buffer.WriteBlock(MoralData(moralNum));

            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendUpdateMoralToAll(int moralNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SUpdateMoral);

            buffer.WriteBlock(MoralData(moralNum));

            NetworkConfig.SendDataToAll(buffer.Data, buffer.Head);
            buffer.Dispose();
        }


        #endregion

        #region Incoming Packets
        public static void Packet_RequestEditMoral(int index, ref byte[] data)
        {
            var buffer = new ByteStream(4);

            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Moral);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            SendMorals(index);

            Core.Type.TempPlayer[index].Editor = (byte) EditorType.Moral;

            buffer.WriteInt32((int) ServerPackets.SMoralEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);

            buffer.Dispose();

        }

        public static void Packet_SaveMoral(int index, ref byte[] data)
        {
            int moralNum;
            int i;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            moralNum = buffer.ReadInt32();

            // Prevent hacking
            if (moralNum < 0 | moralNum > Core.Constant.MAX_MORALS)
                return;

            {
                ref var withBlock = ref Core.Type.Moral[moralNum];
                withBlock.Name = buffer.ReadString();
                withBlock.Color = buffer.ReadByte();
                withBlock.CanCast = buffer.ReadBoolean();
                withBlock.CanPK = buffer.ReadBoolean();
                withBlock.CanDropItem = buffer.ReadBoolean();
                withBlock.CanPickupItem = buffer.ReadBoolean();
                withBlock.CanUseItem = buffer.ReadBoolean();
                withBlock.DropItems = buffer.ReadBoolean();
                withBlock.LoseExp = buffer.ReadBoolean();
                withBlock.PlayerBlock = buffer.ReadBoolean();
                withBlock.NPCBlock = buffer.ReadBoolean();
            }

            // Save it
            SendUpdateMoralToAll(moralNum);
            SaveMoral(moralNum);
            Core.Log.Add(GetPlayerLogin(index) + " saved moral #" + moralNum + ".", Constant.ADMIN_LOG);
            SendMorals(index);
        }

        public static void Packet_RequestMoral(int index, ref byte[] data)
        {
            SendMorals(index);
        }
        #endregion
    }
}