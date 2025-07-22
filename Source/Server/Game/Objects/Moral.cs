using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Core.Packets;
using static Core.Type;
using static Core.Global.Command;

namespace Server
{

    public class Moral
    {

        #region Database
        public static void ClearMoral(int moralNum)
        {
            Core.Data.Moral[moralNum].Name = "";
            Core.Data.Moral[moralNum].Color = 0;
            Core.Data.Moral[moralNum].CanCast = false;
            Core.Data.Moral[moralNum].CanDropItem = false;
            Core.Data.Moral[moralNum].CanPK = false;
            Core.Data.Moral[moralNum].CanPickupItem = false;
            Core.Data.Moral[moralNum].CanUseItem = false;
            Core.Data.Moral[moralNum].DropItems = false;
            Core.Data.Moral[moralNum].LoseExp = false;
            Core.Data.Moral[moralNum].NpcBlock = false;
            Core.Data.Moral[moralNum].PlayerBlock = false;
        }

        public static async System.Threading.Tasks.Task LoadMoralAsync(int moralNum)
        {
            JObject data;

            data = await Database.SelectRowAsync(moralNum, "moral", "data");

            if (data is null)
            {
                ClearMoral(moralNum);
                return;
            }

            var moralData = JObject.FromObject(data).ToObject<Core.Type.Moral>();
            Core.Data.Moral[moralNum] = moralData;
        }

        public static async System.Threading.Tasks.Task LoadMoralsAsync()
        {
            int i;

            var loopTo = Core.Constant.MAX_MORALS;
            for (i = 0; i < loopTo; i++)
                await System.Threading.Tasks.Task.Run(() => LoadMoralAsync(i));
        }

        public static void SaveMoral(int moralNum)
        {
            string json = JsonConvert.SerializeObject(Core.Data.Moral[moralNum]).ToString();

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
            buffer.WriteString(Core.Data.Moral[moralNum].Name);
            buffer.WriteByte(Core.Data.Moral[moralNum].Color);
            buffer.WriteBoolean(Core.Data.Moral[moralNum].NpcBlock);
            buffer.WriteBoolean(Core.Data.Moral[moralNum].PlayerBlock);
            buffer.WriteBoolean(Core.Data.Moral[moralNum].DropItems);
            buffer.WriteBoolean(Core.Data.Moral[moralNum].CanCast);
            buffer.WriteBoolean(Core.Data.Moral[moralNum].CanDropItem);
            buffer.WriteBoolean(Core.Data.Moral[moralNum].CanPickupItem);
            buffer.WriteBoolean(Core.Data.Moral[moralNum].CanPK);
            buffer.WriteBoolean(Core.Data.Moral[moralNum].DropItems);
            buffer.WriteBoolean(Core.Data.Moral[moralNum].LoseExp);

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
                if (Strings.Len(Core.Data.Moral[i].Name) > 0)
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

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateMoralToAll(int moralNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SUpdateMoral);

            buffer.WriteBlock(MoralData(moralNum));

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }


        #endregion

        #region Incoming Packets
        public static void Packet_RequestEditMoral(int index, ref byte[] data)
        {
            var buffer = new ByteStream(4);

            if (GetPlayerAccess(index) < (byte) Core.AccessLevel.Developer)
                return;

            string user;

            user = IsEditorLocked(index, (byte) Core.EditorType.Moral);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) Core.Color.BrightRed);
                return;
            }

            SendMorals(index);

            Core.Data.TempPlayer[index].Editor = (byte) Core.EditorType.Moral;

            buffer.WriteInt32((int) ServerPackets.SMoralEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();

        }

        public static void Packet_SaveMoral(int index, ref byte[] data)
        {
            int moralNum;
            int i;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) Core.AccessLevel.Developer)
                return;

            moralNum = buffer.ReadInt32();

            // Prevent hacking
            if (moralNum < 0 | moralNum > Core.Constant.MAX_MORALS)
                return;

            {
                ref var withBlock = ref Core.Data.Moral[moralNum];
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
                withBlock.NpcBlock = buffer.ReadBoolean();
            }

            // Save it
            SendUpdateMoralToAll(moralNum);
            SaveMoral(moralNum);
            Core.Log.Add(GetAccountLogin(index) + " saved moral #" + moralNum + ".", Constant.ADMIN_LOG);
            SendMorals(index);
        }

        public static void Packet_RequestMoral(int index, ref byte[] data)
        {
            SendMorals(index);
        }
        #endregion
    }
}