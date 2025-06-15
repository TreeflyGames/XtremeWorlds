using System;
using System.Collections.Generic;
using Core;

namespace Core.Globals
{
    /// <summary>
    /// Represents a dynamic entity that can be a Player, NPC, or Pet.
    /// Allows unified access to common fields for logic processing.
    /// </summary>
    public class Entity
    {
        public enum EntityType
        {
            Player,
            NPC,
            Pet
        }

        public EntityType Type { get; }
        public int Id { get; }
        public string Name { get; set; }
        public byte Sex { get; set; } // Player
        public byte Job { get; set; } // Player
        public int Level { get; set; }
        public int[] Vital { get; set; } // Player
        public byte[] Stat { get; set; }
        public byte Points { get; set; } // Player
        public int[] Equipment { get; set; } // Player
        public object[] Inv { get; set; } // Player: PlayerInvStruct[]
        public object[] PlayerSkill { get; set; } // Player: PlayerSkillStruct[]
        public int Map { get; set; } // Player: int, NPC: not present, Pet: not present
        public byte X { get; set; } // Player
        public byte Y { get; set; } // Player
        public byte Dir { get; set; } // Player
        public int Sprite { get; set; }
        public int Exp { get; set; }
        public byte Access { get; set; } // Player
        public object[] Hotbar { get; set; } // Player: HotbarStruct[]
        public byte[] Switches { get; set; } // Player
        public int[] Variables { get; set; } // Player
        public object PetStruct { get; set; } // Player: PlayerPetStruct
        public int XOffset { get; set; } // Player
        public int YOffset { get; set; } // Player
        public byte Moving { get; set; } // Player
        public byte Attacking { get; set; } // Player
        public int AttackTimer { get; set; } // Player
        public int MapGetTimer { get; set; } // Player
        public byte Steps { get; set; } // Player
        public int Emote { get; set; } // Player
        public int EmoteTimer { get; set; } // Player
        public int EventTimer { get; set; } // Player
        public object[] Quests { get; set; } // Player: PlayerQuestStruct[]
        public int GuildId { get; set; } // Player
        public int[] DropChance { get; set; } // NPC
        public int[] DropItem { get; set; } // NPC
        public int[] DropItemValue { get; set; } // NPC
        public string AttackSay { get; set; } // NPC
        public byte SpawnTime { get; set; } // NPC
        public int SpawnSecs { get; set; } // NPC
        public byte Behaviour { get; set; } // NPC
        public byte Range { get; set; } // NPC, Pet
        public int Animation { get; set; } // NPC
        public int HP { get; set; } // NPC
        public int Damage { get; set; } // NPC
        public int[] Skill { get; set; } // NPC: byte[], Pet: int[]
        public byte Faction { get; set; } // NPC
        public int Num { get; set; } // Pet
        public int MaxLevel { get; set; } // Pet
        public int ExpGain { get; set; } // Pet
        public int LevelPnts { get; set; } // Pet
        public byte StatType { get; set; } // Pet
        public byte LevelingType { get; set; } // Pet
        public byte Evolvable { get; set; } // Pet
        public int EvolveLevel { get; set; } // Pet
        public int EvolveNum { get; set; } // Pet
        public int SkillBuffer { get; set; }
        public int SkillBufferTimer { get; set; }

        public int SpawnWait { get; set; } // NPC
        public byte TargetType { get; set; }
        public int Target { get; set; }
        public int StunDuration { get; set; }
        public int StunTimer { get; set; }
        public object RawStruct { get; }

        private Entity(EntityType type, int id, object rawStruct)
        {
            Type = type;
            Id = id;
            RawStruct = rawStruct;
        }

        public static Entity FromPlayer(int id, Type.PlayerStruct player)
        {
            return new Entity(EntityType.Player, id, player)
            {
                Name = player.Name,
                Sex = player.Sex,
                Job = player.Job,
                Sprite = player.Sprite,
                Level = player.Level,
                Exp = player.Exp,
                Access = player.Access,
                Vital = player.Vital,
                Stat = player.Stat,
                Points = player.Points,
                Equipment = player.Equipment,
                Inv = player.Inv != null ? Array.ConvertAll(player.Inv, x => (object)x) : null,
                PlayerSkill = player.Skill != null ? Array.ConvertAll(player.Skill, x => (object)x) : null,
                Map = player.Map,
                X = player.X,
                Y = player.Y,
                Dir = player.Dir,
                Hotbar = player.Hotbar != null ? Array.ConvertAll(player.Hotbar, x => (object)x) : null,
                Switches = player.Switches,
                Variables = player.Variables,
                XOffset = player.XOffset,
                YOffset = player.YOffset,
                Moving = player.Moving,
                Attacking = player.Attacking,
                AttackTimer = player.AttackTimer,
                MapGetTimer = player.MapGetTimer,
                Steps = player.Steps,
                Emote = player.Emote,
                EmoteTimer = player.EmoteTimer,
                EventTimer = player.EventTimer,
                Quests = player.Quests != null ? Array.ConvertAll(player.Quests, x => (object)x) : null,
                GuildId = player.GuildId
            };
        }

        public static Entity FromNPC(int id, Type.NPCStruct npc)
        {
            return new Entity(EntityType.NPC, id, npc)
            {
                Name = npc.Name,
                AttackSay = npc.AttackSay,
                Sprite = npc.Sprite,
                SpawnTime = npc.SpawnTime,
                SpawnSecs = npc.SpawnSecs,
                Behaviour = npc.Behaviour,
                Range = npc.Range,
                DropChance = npc.DropChance,
                DropItem = npc.DropItem,
                DropItemValue = npc.DropItemValue,
                Stat = npc.Stat,
                Faction = npc.Faction,
                HP = npc.HP,
                Exp = npc.Exp,
                Animation = npc.Animation,
                Skill = npc.Skill != null ? Array.ConvertAll(npc.Skill, b => (int)b) : null,
                Level = npc.Level,
                Damage = npc.Damage
            };
        }

        public static Entity FromPet(int id, Type.PetStruct pet)
        {
            return new Entity(EntityType.Pet, id, pet)
            {
                Num = pet.Num,
                Name = pet.Name,
                Sprite = pet.Sprite,
                Range = (byte)pet.Range,
                Level = pet.Level,
                MaxLevel = pet.MaxLevel,
                ExpGain = pet.ExpGain,
                LevelPnts = pet.LevelPnts,
                StatType = pet.StatType,
                LevelingType = pet.LevelingType,
                Stat = pet.Stat,
                Skill = pet.Skill,
                Evolvable = pet.Evolvable,
                EvolveLevel = pet.EvolveLevel,
                EvolveNum = pet.EvolveNum
            };
        }
    }
}
