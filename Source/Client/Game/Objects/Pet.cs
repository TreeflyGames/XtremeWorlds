using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Global.Command;
using Path = Core.Path;

namespace Client
{

    public class Pet
    {
        #region Database

        public static void ClearPet(int index)
        {
            Data.Pet[index] = default;
            Data.Pet[index].Name = "";

            int statCount = System.Enum.GetValues(typeof(Stat)).Length;
            Data.Pet[index].Stat = new byte[statCount];
            Data.Pet[index].Skill = new int[Constant.MAX_PET_SKILLS];

            for (int i = 0; i < Constant.MAX_PET_SKILLS; i++)
                Data.Pet[index].Skill[i] = -1;

            GameState.Pet_Loaded[index] = 0;
        }

        public static void ClearPets()
        {
            int i;

            Data.Pet = new Core.Type.Pet[Constant.MAX_PETS];

            for (i = 0; i < Constant.MAX_PETS; i++)
                ClearPet(i);

        }

        public static void StreamPet(int petNum)
        {
            if (petNum >= 0 && string.IsNullOrEmpty(Data.Pet[petNum].Name) && GameState.Pet_Loaded[petNum] == 0)
            {
                GameState.Pet_Loaded[petNum] = 1;
                SendRequestPet(petNum);
            }
        }

        #endregion

        #region Outgoing Packets
        public static void SendRequestPet(int petNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestPet);

            buffer.WriteInt32(petNum);
            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendRequestPets()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestPets);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }

        public static void SendRequestEditPet()
        {
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditPet);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();

        }

        public static void SendSavePet(int petNum)
        {
            ByteStream buffer;
            int i;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CSavePet);
            buffer.WriteInt32(petNum);

            ref var withBlock = ref Data.Pet[petNum];
            buffer.WriteInt32(withBlock.Num);
            buffer.WriteString(withBlock.Name);
            buffer.WriteInt32(withBlock.Sprite);
            buffer.WriteInt32(withBlock.Range);
            buffer.WriteByte(withBlock.Level);
            buffer.WriteInt32(withBlock.MaxLevel);
            buffer.WriteInt32(withBlock.ExpGain);
            buffer.WriteByte(withBlock.Points);
            buffer.WriteByte(withBlock.StatType);
            buffer.WriteInt32(withBlock.LevelingType);

            int statCount = System.Enum.GetValues(typeof(Stat)).Length;
            for (i = 0; i < statCount; i++)
                buffer.WriteInt32(withBlock.Stat[i]);

            for (i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                buffer.WriteInt32(withBlock.Skill[i]);

            buffer.WriteInt32(withBlock.Evolvable);
            buffer.WriteInt32(withBlock.EvolveLevel);
            buffer.WriteInt32(withBlock.EvolveNum);
            
            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();

        }
        #endregion        

    }
}