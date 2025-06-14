using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Core;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Packets;
using static Core.Type;

namespace Server
{


    public class Pet
    {
        #region Database

        public static void SavePets()
        {
            int i;

            var loopTo = Core.Constant.MAX_PETS;
            for (i = 0; i < loopTo; i++)
                SavePet(i);

        }

        public static void SavePet(int petNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Pet[petNum]).ToString();

            if (Database.RowExists(petNum, "pet"))
            {
                Database.UpdateRow(petNum, json, "pet", "data");
            }
            else
            {
                Database.InsertRow(petNum, json, "pet");
            }
        }

        public static async Task LoadPetsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_PETS).Select(i => Task.Run(() => LoadPetAsync(i)));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadPetAsync(int petNum)
        {
            JObject data;
            data = await Database.SelectRowAsync(petNum, "pet", "data");

            if (data is null)
            {
                ClearPet(petNum);
                return;
            }

            var petData = JObject.FromObject(data).ToObject<PetStruct>();
            Core.Type.Pet[petNum] = petData;
        }

        public static void ClearPet(int petNum)
        {
            Core.Type.Pet[petNum].Name = "";

            Core.Type.Pet[petNum].Stat = new byte[(byte)StatType.Count];
            Core.Type.Pet[petNum].Skill = new int[Core.Constant.MAX_PET_SKILLS];
        }

        #endregion

        #region Outgoing Packets

        public static void SendPets(int index)
        {
            int i;

            var loopTo = Core.Constant.MAX_PETS;
            for (i = 0; i < loopTo; i++)
            {
                if (Core.Type.Pet[i].Name.Length > 0)
                {
                    SendUpdatePetTo(index, i);
                }
            }

        }

        public static void SendPet(int index, int petNum)
        {
            SendUpdatePetTo(index, petNum);
        }

        public static void SendUpdatePetToAll(int petNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SUpdatePet);

            buffer.WriteInt32(petNum);

            ref var withBlock = ref Core.Type.Pet[petNum];
            buffer.WriteInt32(withBlock.Num);
            buffer.WriteString(withBlock.Name);
            buffer.WriteInt32(withBlock.Sprite);
            buffer.WriteInt32(withBlock.Range);
            buffer.WriteInt32(withBlock.Level);
            buffer.WriteInt32(withBlock.MaxLevel);
            buffer.WriteInt32(withBlock.ExpGain);
            buffer.WriteInt32(withBlock.LevelPnts);
            buffer.WriteInt32(withBlock.StatType);
            buffer.WriteInt32(withBlock.LevelingType);

            for (int i = 0, loopTo = (int)(StatType.Count); i < loopTo; i++)
                buffer.WriteInt32(withBlock.Stat[i]);

            for (int i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                buffer.WriteInt32(withBlock.Skill[i]);

            buffer.WriteInt32(withBlock.Evolvable);
            buffer.WriteInt32(withBlock.EvolveLevel);
            buffer.WriteInt32(withBlock.EvolveNum);

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendUpdatePetTo(int index, int petNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SUpdatePet);

            buffer.WriteInt32(petNum);

            {
                ref var withBlock = ref Core.Type.Pet[petNum];
                buffer.WriteInt32(withBlock.Num);
                buffer.WriteString(withBlock.Name);
                buffer.WriteInt32(withBlock.Sprite);
                buffer.WriteInt32(withBlock.Range);
                buffer.WriteInt32(withBlock.Level);
                buffer.WriteInt32(withBlock.MaxLevel);
                buffer.WriteInt32(withBlock.ExpGain);
                buffer.WriteInt32(withBlock.LevelPnts);
                buffer.WriteInt32(withBlock.StatType);
                buffer.WriteInt32(withBlock.LevelingType);

                for (int i = 0, loopTo = (byte)StatType.Count; i < loopTo; i++)
                    buffer.WriteInt32(withBlock.Stat[i]);

                for (int i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                    buffer.WriteInt32(withBlock.Skill[i]);

                buffer.WriteInt32(withBlock.Evolvable);
                buffer.WriteInt32(withBlock.EvolveLevel);
                buffer.WriteInt32(withBlock.EvolveNum);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();

        }

    #endregion

        #region Incoming Packets

        public static void Packet_RequestEditPet(int index, ref byte[] data)
        {
            var buffer = new ByteStream(4);

            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Pet);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            Core.Type.TempPlayer[index].Editor = (byte) EditorType.Pet;

            Pet.SendPets(index);

            buffer.WriteInt32((int) ServerPackets.SPetEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();

        }

        public static void Packet_UpdatePet(ref byte[] data)
        {
            int n;
            int i;
            var buffer = new ByteStream(data);
            n = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.Pet[n];
                withBlock.Num = buffer.ReadInt32();
                withBlock.Name = buffer.ReadString();
                withBlock.Sprite = buffer.ReadInt32();
                withBlock.Range = buffer.ReadInt32();
                withBlock.Level = buffer.ReadInt32();
                withBlock.MaxLevel = buffer.ReadInt32();
                withBlock.ExpGain = buffer.ReadInt32();
                withBlock.LevelPnts = buffer.ReadInt32();
                withBlock.StatType = (byte)buffer.ReadInt32();
                withBlock.LevelingType = (byte)buffer.ReadInt32();

                for (i = 0; i < (int)Core.Enum.StatType.Count; i++)
                    withBlock.Stat[i] = (byte)buffer.ReadInt32();

                for (i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                    withBlock.Skill[i] = buffer.ReadInt32();

                withBlock.Evolvable = (byte)buffer.ReadInt32();
                withBlock.EvolveLevel = buffer.ReadInt32();
                withBlock.EvolveNum = buffer.ReadInt32();
            }

            buffer.Dispose();

        }


        public static void Packet_SavePet(int index, ref byte[] data)
        {
            int petNum;
            int i;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            petNum = buffer.ReadInt32();

            // Prevent hacking
            if (petNum < 0 | petNum > Core.Constant.MAX_PETS)
                return;
           
            ref var withBlock = ref Core.Type.Pet[petNum];
            withBlock.Num = buffer.ReadInt32();
            withBlock.Name = buffer.ReadString();
            withBlock.Sprite = buffer.ReadInt32();
            withBlock.Range = buffer.ReadInt32();
            withBlock.Level = buffer.ReadInt32();
            withBlock.MaxLevel = buffer.ReadInt32();
            withBlock.ExpGain = buffer.ReadInt32();
            withBlock.LevelPnts = buffer.ReadInt32();
            withBlock.StatType = (byte)buffer.ReadInt32();
            withBlock.LevelingType = (byte)buffer.ReadInt32();

            var loopTo = (byte)StatType.Count;
            for (i = 0; i < loopTo; i++)
                withBlock.Stat[i] = (byte)buffer.ReadInt32();

            for (i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                withBlock.Skill[i] = buffer.ReadInt32();

            withBlock.Evolvable = (byte)buffer.ReadInt32();
            withBlock.EvolveLevel = buffer.ReadInt32();
            withBlock.EvolveNum = buffer.ReadInt32();
            

            // Save it
            SendUpdatePetToAll(petNum);
            SavePet(petNum);
            Core.Log.Add(GetAccountLogin(index) + " saved Pet #" + petNum + ".", Constant.ADMIN_LOG);
            SendPets(index);
        }

        public static void Packet_RequestPets(int index, ref byte[] data)
        {
            SendPets(index);
        }

        public static void Packet_RequestPet(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;

            n = buffer.ReadInt32();

            if (n < 0 | n > Core.Constant.MAX_RESOURCES)
                return;

            SendUpdatePetTo(index, n);
        }

        #endregion
    }
}