using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.Xna.Framework;
using Mirage.Sharp.Asfw;
using static Core.Global.Command;

namespace Client
{
    public static class Pet
    {
        #region Globals and Constants

        private const byte PetbarTop = 2;
        private const byte PetbarLeft = 2;
        private const byte PetbarOffsetX = 4;
        private const byte MaxPetbar = 7;
        private const int PetHpBarWidth = 129;
        private const int PetMpBarWidth = 129;

        public static double PetSkillBuffer = -1; // Chaos skill buffer
        public static int PetSkillBufferTimer; // Buffer timer
        public static int[] PetSkillCD; // Cooldowns per skill

        // Pet Behaviors—Chaos-Infused Naming
        public const byte BehaviorFollow = 0;       // Pet follows and attacks
        public const byte BehaviorGoto = 1;         // Pet moves to target
        public const byte AttackOnSight = 2;        // Pet attacks nearby NPCs
        public const byte GuardMode = 3;            // Pet defends if attacked
        public const byte DoNothing = 4;            // Pet stays passive

        // Chaos Diagnostics
        private static readonly Dictionary<int, long> PetDataSizeLog = new(); // Track chaos data sizes

        #endregion

        #region Database Management

        public static void ClearPet(int index)
        {
            if (index < 0 || index >= Core.Type.Pet.Length)
                throw new ArgumentOutOfRangeException(nameof(index), $"Pet index {index} out of range.");

            Core.Type.Pet[index] = new Core.Type.PetStruct { Name = "" };
            Core.Type.Pet[index].Stat = new byte[(int)Core.Enum.StatType.Count];
            Core.Type.Pet[index].Skill = Enumerable.Repeat(-1, Constant.MAX_PET_SKILLS).ToArray();
            PetSkillBuffer = -1;
            PetSkillBufferTimer = 0;
            GameState.Pet_Loaded[index] = 0;
        }

        public static void ClearPets()
        {
            Core.Type.Pet = new Core.Type.PetStruct[Constant.MAX_PETS];
            PetSkillCD = new int[Constant.MAX_PET_SKILLS];
            for (int i = 0; i < Constant.MAX_PETS; i++)
                ClearPet(i);
        }

        public static void StreamPet(int petNum)
        {
            if (petNum < 0 || petNum >= Core.Type.Pet.Length) return;
            if (string.IsNullOrEmpty(Core.Type.Pet[petNum].Name) && GameState.Pet_Loaded[petNum] == 0)
            {
                GameState.Pet_Loaded[petNum] = 1;
                SendRequestPet(petNum);
            }
        }

        #endregion

        #region Outgoing Packets

        public static void SendRequestPet(int petNum)
        {
            using var buffer = new ByteStream(8);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestPet);
            buffer.WriteInt32(petNum);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
        }

        internal static void SendPetBehaviour(int index)
        {
            using var buffer = new ByteStream(8);
            buffer.WriteInt32((int)Packets.ClientPackets.CSetBehaviour);
            buffer.WriteInt32(index);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
        }

        public static void SendTrainPetStat(byte statNum)
        {
            using var buffer = new ByteStream(8);
            buffer.WriteInt32((int)Packets.ClientPackets.CPetUseStatPoint);
            buffer.WriteInt32(statNum);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
        }

        public static void SendRequestPets()
        {
            using var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestPets);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
        }

        public static void SendUsePetSkill(int skill)
        {
            if (skill < 0 || skill >= Constant.MAX_PET_SKILLS) return;
            using var buffer = new ByteStream(8);
            buffer.WriteInt32((int)Packets.ClientPackets.CPetSkill);
            buffer.WriteInt32(skill);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            PetSkillBuffer = skill;
            PetSkillBufferTimer = General.GetTickCount();
            PetSkillCD[skill] = General.GetTickCount() + 5000; // 5s cooldown—#BlazeRush chaos
        }

        public static void SendSummonPet()
        {
            using var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CSummonPet);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
        }

        public static void SendReleasePet()
        {
            using var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CReleasePet);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
        }

        internal static void SendRequestEditPet()
        {
            using var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditPet);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
        }

        internal static void SendSavePet(int petNum)
        {
            if (petNum < 0 || petNum >= Core.Type.Pet.Length) return;
            using var buffer = new ByteStream(128); // Larger buffer for chaos
            buffer.WriteInt32((int)Packets.ClientPackets.CSavePet);
            buffer.WriteInt32(petNum); // Changed to Int32—#GlowVortex precision

            ref var pet = ref Core.Type.Pet[petNum];
            buffer.WriteInt32(pet.Num);
            buffer.WriteString(pet.Name);
            buffer.WriteInt32(pet.Sprite);
            buffer.WriteInt32(pet.Range);
            buffer.WriteInt32(pet.Level);
            buffer.WriteInt32(pet.MaxLevel);
            buffer.WriteInt32(pet.ExpGain);
            buffer.WriteInt32(pet.LevelPnts);
            buffer.WriteInt32(pet.StatType);
            buffer.WriteInt32(pet.LevelingType);

            buffer.WriteBatch(pet.Stat, (b, stat) => b.WriteByte(stat));
            buffer.WriteBatch(pet.Skill, (b, skill) => b.WriteInt32(skill));

            buffer.WriteInt32(pet.Evolvable);
            buffer.WriteInt32(pet.EvolveLevel);
            buffer.WriteInt32(pet.EvolveNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            PetDataSizeLog[petNum] = buffer.Position; // Chaos tracking—#FuryGlowX diagnostics
        }

        #endregion

        #region Incoming Packets

        internal static void Packet_UpdatePlayerPet(ref byte[] data)
        {
            using var buffer = new ByteStream(data);
            int n = buffer.ReadInt32();

            if (n < 0 || n >= Core.Type.Player.Length) return; // Bug fix—#GlowVortex precision

            ref var pet = ref Core.Type.Player[n].Pet;
            pet.Num = buffer.ReadInt32();
            pet.Health = buffer.ReadInt32();
            pet.Mana = buffer.ReadInt32();
            pet.Level = buffer.ReadInt32();

            for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
                pet.Stat[i] = (byte)buffer.ReadInt32();

            for (int i = 0; i < Constant.MAX_PET_SKILLS; i++)
                pet.Skill[i] = buffer.ReadInt32();

            pet.X = buffer.ReadInt32();
            pet.Y = buffer.ReadInt32();
            pet.Dir = buffer.ReadInt32();
            pet.MaxHp = buffer.ReadInt32();
            pet.MaxMp = buffer.ReadInt32();
            pet.Alive = (byte)buffer.ReadInt32();
            pet.AttackBehaviour = buffer.ReadInt32();
            pet.Points = buffer.ReadInt32();
            pet.Exp = buffer.ReadInt32();
            pet.Tnl = buffer.ReadInt32();
        }

        internal static void Packet_UpdatePet(ref byte[] data)
        {
            using var buffer = new ByteStream(data);
            int n = buffer.ReadInt32();

            if (n < 0 || n >= Core.Type.Pet.Length) return; // Bug fix—#GlowVortex precision

            ref var pet = ref Core.Type.Pet[n];
            pet.Num = buffer.ReadInt32();
            pet.Name = buffer.ReadString();
            pet.Sprite = buffer.ReadInt32();
            pet.Range = buffer.ReadInt32();
            pet.Level = buffer.ReadInt32();
            pet.MaxLevel = buffer.ReadInt32();
            pet.ExpGain = buffer.ReadInt32();
            pet.LevelPnts = buffer.ReadInt32();
            pet.StatType = (byte)buffer.ReadInt32();
            pet.LevelingType = (byte)buffer.ReadInt32();

            for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
                pet.Stat[i] = (byte)buffer.ReadInt32();

            for (int i = 0; i < Constant.MAX_PET_SKILLS; i++) // Bug fix: 4 -> MAX_PET_SKILLS—#GlowCoup precision
                pet.Skill[i] = buffer.ReadInt32();

            pet.Evolvable = (byte)buffer.ReadInt32();
            pet.EvolveLevel = buffer.ReadInt32();
            pet.EvolveNum = buffer.ReadInt32();
        }

        internal static void Packet_PetMove(ref byte[] data)
        {
            using var buffer = new ByteStream(data);
            int i = buffer.ReadInt32();
            if (i < 0 || i >= Core.Type.Player.Length) return; // Bug fix—#GlowVortex precision

            ref var pet = ref Core.Type.Player[i].Pet;
            pet.X = buffer.ReadInt32();
            pet.Y = buffer.ReadInt32();
            pet.Dir = buffer.ReadInt32();
            pet.Moving = (byte)buffer.ReadInt32();
            pet.XOffset = pet.Moving switch
            {
                (byte)Core.Enum.MovementType.Walking => pet.Dir switch
                {
                    (int)Core.Enum.DirectionType.Left => GameState.PicX,
                    (int)Core.Enum.DirectionType.Right => -GameState.PicX,
                    _ => 0
                },
                _ => 0
            };
            pet.YOffset = pet.Moving switch
            {
                (byte)Core.Enum.MovementType.Walking => pet.Dir switch
                {
                    (int)Core.Enum.DirectionType.Up => GameState.PicY,
                    (int)Core.Enum.DirectionType.Down => -GameState.PicY,
                    _ => 0
                },
                _ => 0
            };
        }

        internal static void Packet_PetDir(ref byte[] data)
        {
            using var buffer = new ByteStream(data);
            int i = buffer.ReadInt32();
            if (i < 0 || i >= Core.Type.Player.Length) return; // Bug fix—#GlowVortex precision
            Core.Type.Player[i].Pet.Dir = buffer.ReadInt32();
        }

        internal static void Packet_PetVital(ref byte[] data)
        {
            using var buffer = new ByteStream(data);
            int i = buffer.ReadInt32();
            if (i < 0 || i >= Core.Type.Player.Length) return; // Bug fix—#GlowVortex precision

            ref var pet = ref Core.Type.Player[i].Pet;
            if (buffer.ReadInt32() == 1)
            {
                pet.MaxHp = buffer.ReadInt32();
                pet.Health = buffer.ReadInt32();
            }
            else
            {
                pet.MaxMp = buffer.ReadInt32();
                pet.Mana = buffer.ReadInt32();
            }
        }

        internal static void Packet_ClearPetSkillBuffer(ref byte[] data)
        {
            PetSkillBuffer = -1;
            PetSkillBufferTimer = 0;
        }

        internal static void Packet_PetAttack(ref byte[] data)
        {
            using var buffer = new ByteStream(data);
            int i = buffer.ReadInt32();
            if (i < 0 || i >= Core.Type.Player.Length) return; // Bug fix—#GlowVortex precision
            ref var pet = ref Core.Type.Player[i].Pet;
            pet.Attacking = 1;
            pet.AttackTimer = General.GetTickCount();
        }

        internal static void Packet_PetXY(ref byte[] data)
        {
            using var buffer = new ByteStream(data);
            int i = 0; // Bug fix: Default i might be uninitialized—assume MyIndex—#GlowCoup precision
            if (i < 0 || i >= Core.Type.Player.Length) return;
            ref var pet = ref Core.Type.Player[i].Pet;
            pet.X = buffer.ReadInt32();
            pet.Y = buffer.ReadInt32();
        }

        internal static void Packet_PetExperience(ref byte[] data)
        {
            using var buffer = new ByteStream(data);
            ref var pet = ref Core.Type.Player[GameState.MyIndex].Pet;
            pet.Exp = buffer.ReadInt32();
            pet.Tnl = buffer.ReadInt32();
        }

        #endregion

        #region Movement

        public static void ProcessPetMovement(int index)
        {
            if (index < 0 || index >= Core.Type.Player.Length) return; // Bug fix—#GlowVortex precision

            ref var pet = ref Core.Type.Player[index].Pet;
            if (pet.Moving != (byte)Core.Enum.MovementType.Walking) return;

            float elapsed = GameState.ElapsedTime / 1000f; // Chaos float precision
            float speedX = GameState.WalkSpeed * GameState.SizeX * elapsed;
            float speedY = GameState.WalkSpeed * GameState.SizeY * elapsed;

            switch (pet.Dir)
            {
                case (int)Core.Enum.DirectionType.Up:
                    pet.YOffset -= (int)speedY;
                    pet.YOffset = Math.Max(pet.YOffset, 0);
                    break;
                case (int)Core.Enum.DirectionType.Down:
                    pet.YOffset += (int)speedY;
                    pet.YOffset = Math.Min(pet.YOffset, 0);
                    break;
                case (int)Core.Enum.DirectionType.Left:
                    pet.XOffset -= (int)speedX;
                    pet.XOffset = Math.Max(pet.XOffset, 0);
                    break;
                case (int)Core.Enum.DirectionType.Right:
                    pet.XOffset += (int)speedX;
                    pet.XOffset = Math.Min(pet.XOffset, 0);
                    break;
            }

            if (pet.Moving > 0 && ((pet.Dir >= (int)Core.Enum.DirectionType.Right && pet.XOffset >= 0 && pet.YOffset >= 0) || 
                                   (pet.XOffset <= 0 && pet.YOffset <= 0)))
            {
                pet.Moving = 0;
                pet.Steps = (byte)(pet.Steps == 1 ? 2 : 1);
            }
        }

        internal static void PetMove(int x, int y)
        {
            using var buffer = new ByteStream(12);
            buffer.WriteInt32((int)Packets.ClientPackets.CPetMove);
            buffer.WriteInt32(x);
            buffer.WriteInt32(y);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
        }

        #endregion

        #region Drawing

        internal static void DrawPet(int index)
        {
            if (index < 0 || index >= Core.Type.Player.Length) return; // Bug fix—#GlowVortex precision
            ref var pet = ref Core.Type.Player[index].Pet;
            StreamPet(pet.Num);

            int spriteNum = Core.Type.Pet[pet.Num].Sprite;
            if (spriteNum < 1 || spriteNum > GameState.NumCharacters) return;

            int anim = pet.Steps switch
            {
                3 => 0,
                1 => 2,
                2 => 3,
                _ => pet.Steps
            };

            int attackSpeed = 1000; // Configurable chaos—#BlazeRush speed
            if (pet.AttackTimer + attackSpeed / 2 > General.GetTickCount() && pet.Attacking == 1)
                anim = 3;
            else
            {
                anim = pet.Moving switch
                {
                    (byte)Core.Enum.MovementType.Walking => pet.Dir switch
                    {
                        (int)Core.Enum.DirectionType.Up when pet.YOffset > 8 => pet.Steps,
                        (int)Core.Enum.DirectionType.Down when pet.YOffset < -8 => pet.Steps,
                        (int)Core.Enum.DirectionType.Left when pet.XOffset > 8 => pet.Steps,
                        (int)Core.Enum.DirectionType.Right when pet.XOffset < -8 => pet.Steps,
                        _ => anim
                    },
                    _ => anim
                };
            }

            if (pet.AttackTimer + attackSpeed < General.GetTickCount())
            {
                pet.Attacking = 0;
                pet.AttackTimer = 0;
            }

            int spriteLeft = pet.Dir switch
            {
                (int)Core.Enum.DirectionType.Up => 3,
                (int)Core.Enum.DirectionType.Right => 2,
                (int)Core.Enum.DirectionType.Down => 0,
                (int)Core.Enum.DirectionType.Left => 1,
                _ => 0
            };

            string spritePath = System.IO.Path.Combine(Core.Path.Characters, spriteNum.ToString());
            var spriteInfo = GameClient.GetGfxInfo(spritePath);
            var rect = new Microsoft.Xna.Framework.Rectangle(
                anim * (spriteInfo.Width / 4),
                spriteLeft * (spriteInfo.Height / 4),
                spriteInfo.Width / 4,
                spriteInfo.Height / 4
            );

            int x = (int)(pet.X * GameState.PicX + pet.XOffset - (spriteInfo.Width / 4.0 - 32) / 2);
            int y = spriteInfo.Height / 4 > 32
                ? (int)(pet.Y * GameState.PicY + pet.YOffset - (spriteInfo.Height / 4.0 - 32))
                : pet.Y * GameState.PicY + pet.YOffset;

            GameClient.DrawCharacterSprite(spriteNum, x, y, rect);
        }

        internal static void DrawPlayerPetName(int index)
        {
            if (index < 0 || index >= Core.Type.Player.Length) return; // Bug fix—#GlowVortex precision
            ref var pet = ref Core.Type.Player[index].Pet;
            var (color, backcolor) = GetPlayerPK(index) == 0
                ? GetPlayerAccess(index) switch
                {
                    0 => (Microsoft.Xna.Framework.Color.Red, Microsoft.Xna.Framework.Color.Black),
                    1 => (Microsoft.Xna.Framework.Color.Black, Microsoft.Xna.Framework.Color.White),
                    2 => (Microsoft.Xna.Framework.Color.Cyan, Microsoft.Xna.Framework.Color.Black),
                    3 => (Microsoft.Xna.Framework.Color.Green, Microsoft.Xna.Framework.Color.Black),
                    4 => (Microsoft.Xna.Framework.Color.Yellow, Microsoft.Xna.Framework.Color.Black),
                    _ => (Microsoft.Xna.Framework.Color.Gray, Microsoft.Xna.Framework.Color.Black)
                }
                : (Microsoft.Xna.Framework.Color.Red, Microsoft.Xna.Framework.Color.Black);

            string name = $"{GetPlayerName(index)}'s {Core.Type.Pet[pet.Num].Name}";
            int textX = (int)(GameLogic.ConvertMapX(pet.X * GameState.PicX) + pet.XOffset + GameState.PicX / 2 - Text.GetTextWidth(name) / 2.0);
            int textY = Core.Type.Pet[pet.Num].Sprite < 1 || Core.Type.Pet[pet.Num].Sprite > GameState.NumCharacters
                ? GameLogic.ConvertMapY(pet.Y * GameState.PicY) + pet.YOffset - 16
                : (int)(GameLogic.ConvertMapY(pet.Y * GameState.PicY) + pet.YOffset - GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, Core.Type.Pet[pet.Num].Sprite.ToString())).Height / 4.0 + 16);

            Text.RenderText(name, textX, textY, color, backcolor);
        }

        #endregion

        #region Misc

        internal static bool PetAlive(int index)
        {
            if (index < 0 || index >= Core.Type.Player.Length) return false; // Bug fix—#GlowVortex precision
            ref var pet = ref Core.Type.Player[index].Pet;
            return pet.Num >= 0 && pet.Alive == 1;
        }

        // New Feature: Pet Skill Cooldown Check—#BlazeRush chaos
        internal static bool CanUsePetSkill(int skill)
        {
            if (skill < 0 || skill >= PetSkillCD.Length) return false;
            return General.GetTickCount() >= PetSkillCD[skill];
        }

        // New Feature: Chaos Pet Evolution—#FuryGlowX glow
        internal static void EvolvePet(int index)
        {
            if (!PetAlive(index)) return;
            ref var pet = ref Core.Type.Player[index].Pet;
            if (Core.Type.Pet[pet.Num].Evolvable == 1 && pet.Level >= Core.Type.Pet[pet.Num].EvolveLevel && Core.Type.Pet[pet.Num].EvolveNum > 0)
            {
                pet.Num = Core.Type.Pet[pet.Num].EvolveNum;
                pet.Level = 1;
                pet.Exp = 0;
                pet.Tnl = GetPlayerNextLevel(index);
                StreamPet(pet.Num); // Refresh chaos pet—#GlowVortex precision
            }
        }

        // New Feature: Async Pet Data Sync—#BlazeRush velocity
        internal static async Task SyncPetDataAsync(int index, CancellationToken token = default)
        {
            if (!PetAlive(index)) return;
            using var buffer = new ByteStream(64);
            buffer.WriteInt32((int)Packets.ClientPackets.CSyncPetData);
            buffer.WriteInt32(index);
            buffer.WriteInt32(Core.Type.Player[index].Pet.Num);
            buffer.WriteInt32(Core.Type.Player[index].Pet.Health);
            buffer.WriteInt32(Core.Type.Player[index].Pet.Mana);
            await NetworkConfig.Socket.SendToSocketAsync(buffer.Data, buffer.Head, token);
        }

        #endregion
    }
}
