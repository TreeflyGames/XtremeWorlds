using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Global.Command;
using Path = Core.Path;

namespace Client
{

    static class Pet
    {

        #region Globals etc

        internal const byte PetbarTop = 2;
        internal const byte PetbarLeft = 2;
        internal const byte PetbarOffsetX = 4;
        internal const byte MaxPetbar = 7;
        internal const int PetHpBarWidth = 129;
        internal const int PetMpBarWidth = 129;

        internal static int PetSkillBuffer;
        internal static int PetSkillBufferTimer;
        internal static int[] PetSkillCd;

        internal const byte PetBehaviourFollow = 0; // The pet will attack all NPCs around

        internal const byte PetBehaviourGoto = 1; // If attacked, the pet will fight back
        internal const byte PetAttackBehaviourAttackonsight = 2; // The pet will attack all NPCs around
        internal const byte PetAttackBehaviourGuard = 3; // If attacked, the pet will fight back
        internal const byte PetAttackBehaviourDonothing = 4; // The pet will not attack even if attacked

        #endregion

        #region Database

        public static void ClearPet(int index)
        {
            Core.Type.Pet[index] = default;
            Core.Type.Pet[index].Name = "";

            Core.Type.Pet[index].Stat = new byte[(int)Core.Enum.StatType.Count];
            Core.Type.Pet[index].Skill = new int[5];
            GameState.Pet_Loaded[index] = 0;
        }

        public static void ClearPets()
        {
            int i;

            Core.Type.Pet = new Core.Type.PetStruct[101];
            PetSkillCd = new int[5];

            for (i = 0; i < Constant.MAX_PETS; i++)
                ClearPet(i);

        }

        public static void StreamPet(int petNum)
        {
            if (Conversions.ToBoolean(Operators.OrObject(petNum > 0 & string.IsNullOrEmpty(Core.Type.Pet[petNum].Name), Operators.ConditionalCompareObjectEqual(GameState.Pet_Loaded[petNum], 0, false))))
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
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        internal static void SendPetBehaviour(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSetBehaviour);

            buffer.WriteInt32(index);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendTrainPetStat(byte statNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CPetUseStatPoint);

            buffer.WriteInt32(statNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendRequestPets()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestPets);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendUsePetSkill(int skill)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CPetSkill);
            buffer.WriteInt32(skill);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

            PetSkillBuffer = skill;
            PetSkillBufferTimer = General.GetTickCount();
        }

        public static void SendSummonPet()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSummonPet);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendReleasePet()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CReleasePet);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        internal static void SendRequestEditPet()
        {
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditPet);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();

        }

        internal static void SendSavePet(int petNum)
        {
            ByteStream buffer;
            int i;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CSavePet);
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

                for (i = 0; i < (int)Core.Enum.StatType.Count - 1; i++)
                    buffer.WriteInt32(withBlock.Stat[i]);

                for (i = 0; i <= 4; i++)
                    buffer.WriteInt32(withBlock.Skill[i]);

                buffer.WriteInt32(withBlock.Evolvable);
                buffer.WriteInt32(withBlock.EvolveLevel);
                buffer.WriteInt32(withBlock.EvolveNum);
            }

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();

        }
        #endregion

        #region Incoming Packets

        internal static void Packet_UpdatePlayerPet(ref byte[] data)
        {
            int n;
            int i;
            var buffer = new ByteStream(data);
            n = buffer.ReadInt32();

            // pet
            Core.Type.Player[n].Pet.Num = buffer.ReadInt32();
            Core.Type.Player[n].Pet.Health = buffer.ReadInt32();
            Core.Type.Player[n].Pet.Mana = buffer.ReadInt32();
            Core.Type.Player[n].Pet.Level = buffer.ReadInt32();

            for (i = 0; i < (int)Core.Enum.StatType.Count - 1; i++)
                Core.Type.Player[n].Pet.Stat[i] = (byte)buffer.ReadInt32();

            for (i = 0; i <= 4; i++)
                Core.Type.Player[n].Pet.Skill[i] = buffer.ReadInt32();

            Core.Type.Player[n].Pet.X = buffer.ReadInt32();
            Core.Type.Player[n].Pet.Y = buffer.ReadInt32();
            Core.Type.Player[n].Pet.Dir = buffer.ReadInt32();

            Core.Type.Player[n].Pet.MaxHp = buffer.ReadInt32();
            Core.Type.Player[n].Pet.MaxMp = buffer.ReadInt32();

            Core.Type.Player[n].Pet.Alive = (byte)buffer.ReadInt32();

            Core.Type.Player[n].Pet.AttackBehaviour = buffer.ReadInt32();
            Core.Type.Player[n].Pet.Points = buffer.ReadInt32();
            Core.Type.Player[n].Pet.Exp = buffer.ReadInt32();
            Core.Type.Player[n].Pet.Tnl = buffer.ReadInt32();

            buffer.Dispose();
        }

        internal static void Packet_UpdatePet(ref byte[] data)
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

                for (i = 0; i < (int)Core.Enum.StatType.Count - 1; i++)
                    withBlock.Stat[i] = (byte)buffer.ReadInt32();

                for (i = 0; i <= 4; i++)
                    withBlock.Skill[i] = buffer.ReadInt32();

                withBlock.Evolvable = (byte)buffer.ReadInt32();
                withBlock.EvolveLevel = buffer.ReadInt32();
                withBlock.EvolveNum = buffer.ReadInt32();
            }

            buffer.Dispose();

        }

        internal static void Packet_PetMove(ref byte[] data)
        {
            int i;
            int x;
            int y;
            int dir;
            int movement;
            var buffer = new ByteStream(data);
            i = buffer.ReadInt32();
            x = buffer.ReadInt32();
            y = buffer.ReadInt32();
            dir = buffer.ReadInt32();
            movement = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.Player[i].Pet;
                withBlock.X = x;
                withBlock.Y = y;
                withBlock.Dir = dir;
                withBlock.XOffset = 0;
                withBlock.YOffset = 0;
                withBlock.Moving = (byte)movement;

                switch (withBlock.Dir)
                {
                    case (int)Core.Enum.DirectionType.Up:
                        {
                            withBlock.YOffset = GameState.PicY;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Down:
                        {
                            withBlock.YOffset = GameState.PicY * -1;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Left:
                        {
                            withBlock.XOffset = GameState.PicX;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Right:
                        {
                            withBlock.XOffset = GameState.PicX * -1;
                            break;
                        }
                }
            }

            buffer.Dispose();
        }

        internal static void Packet_PetDir(ref byte[] data)
        {
            int i;
            int dir;
            var buffer = new ByteStream(data);
            i = buffer.ReadInt32();
            dir = buffer.ReadInt32();

            Core.Type.Player[i].Pet.Dir = dir;

            buffer.Dispose();
        }

        internal static void Packet_PetVital(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);
            i = buffer.ReadInt32();

            if (buffer.ReadInt32() == 1)
            {
                Core.Type.Player[i].Pet.MaxHp = buffer.ReadInt32();
                Core.Type.Player[i].Pet.Health = buffer.ReadInt32();
            }
            else
            {
                Core.Type.Player[i].Pet.MaxMp = buffer.ReadInt32();
                Core.Type.Player[i].Pet.Mana = buffer.ReadInt32();
            }

            buffer.Dispose();

        }

        internal static void Packet_ClearPetSkillBuffer(ref byte[] data)
        {
            PetSkillBuffer = 0;
            PetSkillBufferTimer = 0;
        }

        internal static void Packet_PetAttack(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);
            i = buffer.ReadInt32();

            // Set pet to attacking
            Core.Type.Player[i].Pet.Attacking = 1;
            Core.Type.Player[i].Pet.AttackTimer = General.GetTickCount();

            buffer.Dispose();
        }

        internal static void Packet_PetXY(ref byte[] data)
        {
            var i = default(int);
            var buffer = new ByteStream(data);

            Core.Type.Player[i].Pet.X = buffer.ReadInt32();
            Core.Type.Player[i].Pet.Y = buffer.ReadInt32();

            buffer.Dispose();
        }

        internal static void Packet_PetExperience(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            Core.Type.Player[GameState.MyIndex].Pet.Exp = buffer.ReadInt32();
            Core.Type.Player[GameState.MyIndex].Pet.Tnl = buffer.ReadInt32();

            buffer.Dispose();
        }

        #endregion

        #region Movement

        public static void ProcessPetMovement(int index)
        {

            // Check if pet is walking, and if so process moving them over

            if (Core.Type.Player[index].Pet.Moving == (byte)Core.Enum.MovementType.Walking)
            {

                switch (Core.Type.Player[index].Pet.Dir)
                {
                    case (int)Core.Enum.DirectionType.Up:
                        {
                            Core.Type.Player[index].Pet.YOffset = (int)Math.Round(Core.Type.Player[index].Pet.YOffset - GameState.ElapsedTime / 1000d * (GameState.WalkSpeed * GameState.SizeY));
                            if (Core.Type.Player[index].Pet.YOffset < 0)
                                Core.Type.Player[index].Pet.YOffset = 0;
                            break;
                        }

                    case (int)Core.Enum.DirectionType.Down:
                        {
                            Core.Type.Player[index].Pet.YOffset = (int)Math.Round(Core.Type.Player[index].Pet.YOffset + GameState.ElapsedTime / 1000d * (GameState.WalkSpeed * GameState.SizeY));
                            if (Core.Type.Player[index].Pet.YOffset > 0)
                                Core.Type.Player[index].Pet.YOffset = 0;
                            break;
                        }

                    case (int)Core.Enum.DirectionType.Left:
                        {
                            Core.Type.Player[index].Pet.XOffset = (int)Math.Round(Core.Type.Player[index].Pet.XOffset - GameState.ElapsedTime / 1000d * (GameState.WalkSpeed * GameState.SizeX));
                            if (Core.Type.Player[index].Pet.XOffset < 0)
                                Core.Type.Player[index].Pet.XOffset = 0;
                            break;
                        }

                    case (int)Core.Enum.DirectionType.Right:
                        {
                            Core.Type.Player[index].Pet.XOffset = (int)Math.Round(Core.Type.Player[index].Pet.XOffset + GameState.ElapsedTime / 1000d * (GameState.WalkSpeed * GameState.SizeX));
                            if (Core.Type.Player[index].Pet.XOffset > 0)
                                Core.Type.Player[index].Pet.XOffset = 0;
                            break;
                        }

                }

                // Check if completed walking over to the next tile
                if (Core.Type.Player[index].Pet.Moving > 0)
                {
                    if (Core.Type.Player[index].Pet.Dir == (int)Core.Enum.DirectionType.Right | Core.Type.Player[index].Pet.Dir == (int)Core.Enum.DirectionType.Down)
                    {
                        if (Core.Type.Player[index].Pet.XOffset >= 0 & Core.Type.Player[index].Pet.YOffset >= 0)
                        {
                            Core.Type.Player[index].Pet.Moving = 0;
                            if (Core.Type.Player[index].Pet.Steps == 1)
                            {
                                Core.Type.Player[index].Pet.Steps = 2;
                            }
                            else
                            {
                                Core.Type.Player[index].Pet.Steps = 1;
                            }
                        }
                    }
                    else if (Core.Type.Player[index].Pet.XOffset <= 0 & Core.Type.Player[index].Pet.YOffset <= 0)
                    {
                        Core.Type.Player[index].Pet.Moving = 0;
                        if (Core.Type.Player[index].Pet.Steps == 1)
                        {
                            Core.Type.Player[index].Pet.Steps = 2;
                        }
                        else
                        {
                            Core.Type.Player[index].Pet.Steps = 1;
                        }
                    }
                }
            }

        }

        internal static void PetMove(int x, int y)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CPetMove);

            buffer.WriteInt32(x);
            buffer.WriteInt32(y);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        #endregion

        #region Drawing

        internal static void DrawPet(int index)
        {
            var anim = default(byte);
            int x;
            int y;
            int spriteNum;
            var spriteleft = default(int);
            Microsoft.Xna.Framework.Rectangle rect;
            int attackspeed;

            StreamPet(Core.Type.Player[index].Pet.Num);

            spriteNum = Core.Type.Pet[Core.Type.Player[index].Pet.Num].Sprite;

            if (spriteNum < 1 | spriteNum > GameState.NumCharacters)
                return;

            attackspeed = 1000;

            // Reset frame
            if (Core.Type.Player[index].Pet.Steps == 3)
            {
                anim = 0;
            }
            else if (Core.Type.Player[index].Pet.Steps == 1)
            {
                anim = 2;
            }
            else if (Core.Type.Player[index].Pet.Steps == 2)
            {
                anim = 3;
            }

            // Check for attacking animation
            if (Core.Type.Player[index].Pet.AttackTimer + attackspeed / 2d > General.GetTickCount())
            {
                if (Core.Type.Player[index].Pet.Attacking == 1)
                {
                    anim = 3;
                }
            }
            else
            {
                // If not attacking, walk normally
                switch (Core.Type.Player[index].Pet.Dir)
                {
                    case (int)Core.Enum.DirectionType.Up:
                        {
                            if (Core.Type.Player[index].Pet.YOffset > 8)
                                anim = Core.Type.Player[index].Pet.Steps;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Down:
                        {
                            if (Core.Type.Player[index].Pet.YOffset < -8)
                                anim = Core.Type.Player[index].Pet.Steps;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Left:
                        {
                            if (Core.Type.Player[index].Pet.XOffset > 8)
                                anim = Core.Type.Player[index].Pet.Steps;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Right:
                        {
                            if (Core.Type.Player[index].Pet.XOffset < -8)
                                anim = Core.Type.Player[index].Pet.Steps;
                            break;
                        }
                }
            }

            // Check to see if we want to stop making him attack
            {
                ref var withBlock = ref Core.Type.Player[index].Pet;
                if (withBlock.AttackTimer + attackspeed < General.GetTickCount())
                {
                    withBlock.Attacking = 0;
                    withBlock.AttackTimer = 0;
                }
            }

            // Set the left
            switch (Core.Type.Player[index].Pet.Dir)
            {
                case (int)Core.Enum.DirectionType.Up:
                    {
                        spriteleft = 3;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Right:
                    {
                        spriteleft = 2;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Down:
                    {
                        spriteleft = 0;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Left:
                    {
                        spriteleft = 1;
                        break;
                    }
            }

            rect = new Microsoft.Xna.Framework.Rectangle(anim * (GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, spriteNum.ToString())).Width / 4), spriteleft * (GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, spriteNum.ToString())).Height / 4), GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, spriteNum.ToString())).Width / 4, GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, spriteNum.ToString())).Height / 4);

            // Calculate the X
            x = (int)Math.Round(Core.Type.Player[index].Pet.X * GameState.PicX + Core.Type.Player[index].Pet.XOffset - (GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, spriteNum.ToString())).Width / 4d - 32d) / 2d);

            // Is the player's height more than 32..?
            if (GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, spriteNum.ToString())).Height / 4d > 32d)
            {
                // Create a 32 pixel offset for larger sprites
                y = (int)Math.Round(Core.Type.Player[index].Pet.Y * GameState.PicY + Core.Type.Player[index].Pet.YOffset - (GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, spriteNum.ToString())).Width / 4d - 32d));
            }
            else
            {
                // Proceed as normal
                y = Core.Type.Player[index].Pet.Y * GameState.PicY + Core.Type.Player[index].Pet.YOffset;
            }

            // render the actual sprite
            GameClient.DrawCharacterSprite(spriteNum, x, y, rect);

        }

        internal static void DrawPlayerPetName(int index)
        {
            int textX;
            int textY;
            var color = default(Microsoft.Xna.Framework.Color);
            var backcolor = default(Microsoft.Xna.Framework.Color);
            string name;

            // Check access level
            if (GetPlayerPK(index) == 0)
            {

                switch (GetPlayerAccess(index))
                {
                    case 0:
                        {
                            color = Microsoft.Xna.Framework.Color.Red;
                            backcolor = Microsoft.Xna.Framework.Color.Black;
                            break;
                        }
                    case 1:
                        {
                            color = Microsoft.Xna.Framework.Color.Black;
                            backcolor = Microsoft.Xna.Framework.Color.White;
                            break;
                        }
                    case 2:
                        {
                            color = Microsoft.Xna.Framework.Color.Cyan;
                            backcolor = Microsoft.Xna.Framework.Color.Black;
                            break;
                        }
                    case 3:
                        {
                            color = Microsoft.Xna.Framework.Color.Green;
                            backcolor = Microsoft.Xna.Framework.Color.Black;
                            break;
                        }
                    case 4:
                        {
                            color = Microsoft.Xna.Framework.Color.Yellow;
                            backcolor = Microsoft.Xna.Framework.Color.Black;
                            break;
                        }
                }
            }
            else
            {
                color = Microsoft.Xna.Framework.Color.Red;
            }

            name = GetPlayerName(index) + "'s " + Core.Type.Pet[Core.Type.Player[index].Pet.Num].Name;

            // calc pos
            textX = (int)Math.Round(GameLogic.ConvertMapX(Core.Type.Player[index].Pet.X * GameState.PicX) + Core.Type.Player[index].Pet.XOffset + GameState.PicX / 2 - Text.GetTextWidth(name) / 2d);
            if (Core.Type.Pet[Core.Type.Player[index].Pet.Num].Sprite < 1 | Core.Type.Pet[Core.Type.Player[index].Pet.Num].Sprite > GameState.NumCharacters)
            {
                textY = GameLogic.ConvertMapY(Core.Type.Player[index].Pet.Y * GameState.PicY) + Core.Type.Player[index].Pet.YOffset - 16;
            }
            else
            {
                // Determine location for text
                textY = (int)Math.Round(GameLogic.ConvertMapY(Core.Type.Player[index].Pet.Y * GameState.PicY) + Core.Type.Player[index].Pet.YOffset - GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, Core.Type.Pet[Core.Type.Player[index].Pet.Num].Sprite.ToString())).Height / 4d + 16d);
            }

            // Draw name
            Text.RenderText(name, textX, textY, color, backcolor);
        }

        #endregion

        #region Misc

        internal static bool PetAlive(int index)
        {
            bool PetAliveRet = default;
            PetAliveRet = Conversions.ToBoolean(0);

            if (Core.Type.Player[index].Pet.Alive == 1)
            {
                PetAliveRet = Conversions.ToBoolean(1);
            }

            return PetAliveRet;

        }

        #endregion

    }
}