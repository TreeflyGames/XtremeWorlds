using Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MonoGame.Extended.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static Core.Type;

namespace Core.Globals
{
    /// <summary>
    /// Represents a dynamic entity that can be a Player, Npc, or Pet.
    /// Allows unified access to common fields for logic processing.
    /// </summary>
    public class Entity
    {
        public static List<Entity> Instances = new List<Entity>();

        public enum EntityType
        {
            Player,
            Npc,
        }

        public static int Count(Entity entity)
        {
            // Returns the count of entities of the specified type
            return Instances.FindAll(e => e.Type == entity.Type).Count;
        }

        public static int Count()
        {
            return Instances.Count;
        }

        public static int Index(Entity entity)
        {
            if (entity == null || Instances == null)
                return -1; // Handle null cases

            // Get all entities of the same type, sorted by Id
            var entities = Instances = Instances
                .OrderBy(e => e.Map)
                .ThenBy(e => e.Id)
                .ToList();

            // Find the index of the input entity in the sorted list
            return entities.FindIndex(e => e.Id == entity.Id);
        }

        public EntityType Type { get; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Pk { get; set; }
        public byte Sex { get; set; } 
        public byte Job { get; set; } 
        public byte Level { get; set; }
        public int[] Vital { get; set; } 
        public byte[] Stat { get; set; }
        public byte Points { get; set; } 
        public int[] Equipment { get; set; } 
        public object[] Inv { get; set; } 
        public object[] PlayerSkill { get; set; } 
        public int Map { get; set; } 
        public int X { get; set; } 
        public int Y { get; set; } 
        public byte Dir { get; set; } 
        public int Sprite { get; set; }
        public int Exp { get; set; }
        public byte Access { get; set; } 
        public object[] Hotbar { get; set; } 
        public byte[] Switches { get; set; } 
        public int[] Variables { get; set; } 
        public object Pet { get; set; } 
        public byte Moving { get; set; } 
        public byte Attacking { get; set; } 
        public int AttackTimer { get; set; } 
        public int MapGetTimer { get; set; } 
        public byte Steps { get; set; } 
        public int Emote { get; set; } 
        public int EmoteTimer { get; set; } 
        public int EventTimer { get; set; } 
        public object[] Quests { get; set; } 
        public int GuildId { get; set; } 
        public int[] DropChance { get; set; }
        public int[] DropItem { get; set; }
        public int[] DropItemValue { get; set; }
        public string AttackSay { get; set; }
        public byte SpawnTime { get; set; }
        public int SpawnSecs { get; set; }
        public byte Behaviour { get; set; }
        public byte Range { get; set; }
        public int Animation { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }
        public int[] Skill { get; set; }
        public byte Faction { get; set; }
        public int Num { get; set; }
        public int SkillBuffer { get; set; }
        public int SkillBufferTimer { get; set; }
        public int SpawnWait { get; set; }
        public byte TargetType { get; set; }
        public int Target { get; set; }
        public int StunDuration { get; set; }
        public int StunTimer { get; set; }
        public byte StopRegen { get; set; }
        public ResourceType[] GatherSkills { get; set; }
        public object Raw { get; }

        private Entity(EntityType type, int id, object raw)
        {
            Type = type;
            Id = id;
            Raw = raw;
        }

        public static Entity FromPlayer(int id, Player player)
        {
            return new Entity(EntityType.Player, id, player)
            {
                Name = player.Name,
                Pk = player.Pk,
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
                Moving = player.Moving,
                Attacking = player.Attacking,
                AttackTimer = player.AttackTimer,
                MapGetTimer = player.MapGetTimer,
                Steps = player.Steps,
                Emote = player.Emote,
                EmoteTimer = player.EmoteTimer,
                EventTimer = player.EventTimer,
                Quests = player.Quests != null ? Array.ConvertAll(player.Quests, x => (object)x) : null,
                GuildId = player.GuildId,
                GatherSkills = player.GatherSkills
            };
        }

        public static Entity FromNpc(int id, Type.MapNpc npc)
        {
            var entity = new Entity(EntityType.Npc, id, npc)
            {
                Num = npc.Num,
                Target = npc.Target,
                TargetType = npc.TargetType,
                Vital = npc.Vital,
                X = npc.X,
                Y = npc.Y,
                Dir = (byte)npc.Dir,
                AttackTimer = npc.AttackTimer,
                SpawnWait = npc.SpawnWait,
                StunDuration = npc.StunDuration,
                StunTimer = npc.StunTimer,
                SkillBuffer = npc.SkillBuffer,
                SkillBufferTimer = npc.SkillBufferTimer,
                Skill = npc.SkillCD != null ? (int[])npc.SkillCD.Clone() : null,
                Attacking = npc.Attacking,
            };
            return entity;
        }
    
        /// <summary>
        /// Converts a Npc Entity to a MapNpc for npc-specific data.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <param name="entity">The Npc Entity to convert.</param>
        /// <returns>A Npc struct with mapped properties.</returns>
        public static MapNpc ToNpc(int id, Entity entity)
        {
            return new MapNpc
            {
                Num = entity.Num,
                Target = entity.Target,
                TargetType = entity.TargetType,
                Vital = entity.Vital != null ? (int[])entity.Vital.Clone() : new int[0],
                X = entity.X,
                Y = entity.Y,
                Dir = entity.Dir,
                AttackTimer = entity.AttackTimer,
                SpawnWait = entity.SpawnWait,
                StunDuration = entity.StunDuration,
                StunTimer = entity.StunTimer,
                SkillBuffer = entity.SkillBuffer,
                SkillBufferTimer = entity.SkillBufferTimer,
                SkillCD = entity.Skill != null ? (int[])entity.Skill.Clone() : new int[0],
                StopRegen = entity.StopRegen,
                StopRegenTimer = 0, 
                Moving = entity.Moving,
                Attacking = entity.Attacking,
                Steps = entity.Steps
            };
        }

        /// <summary>
        /// Converts a Player Entity to a Player for player-specific data.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <param name="entity">The Player Entity to convert.</param>
        /// <returns>A Player with mapped properties.</returns>
        public static Player ToPlayer(int id, Entity entity)
        {
            return new Player
            {
                Name = entity.Name ?? string.Empty,
                Sex = entity.Sex,
                Job = entity.Job,
                Sprite = entity.Sprite,
                Level = entity.Level,
                Exp = entity.Exp,
                Access = entity.Access,
                Pk = entity.Pk,
                Vital = entity.Vital != null ? (int[])entity.Vital.Clone() : new int[0],
                Stat = entity.Stat != null ? (byte[])entity.Stat.Clone() : new byte[0],
                Points = entity.Points,
                Equipment = entity.Equipment != null ? (int[])entity.Equipment.Clone() : new int[0],
                Inv = entity.Inv != null ? entity.Inv.Cast<PlayerInv>().ToArray() : new PlayerInv[0],
                Skill = entity.PlayerSkill != null ? entity.PlayerSkill.Cast<PlayerSkill>().ToArray() : new PlayerSkill[0],
                Map = entity.Map,
                X = entity.X,
                Y = entity.Y,
                Dir = entity.Dir,
                Hotbar = entity.Hotbar != null ? entity.Hotbar.Cast<Hotbar>().ToArray() : new Hotbar[0],
                Switches = entity.Switches != null ? (byte[])entity.Switches.Clone() : new byte[0],
                Variables = entity.Variables != null ? (int[])entity.Variables.Clone() : new int[0],
                GatherSkills = entity.GatherSkills,
                Moving = entity.Moving,
                Attacking = entity.Attacking,
                AttackTimer = entity.AttackTimer,
                MapGetTimer = entity.MapGetTimer,
                Steps = entity.Steps,
                Emote = entity.Emote,
                EmoteTimer = entity.EmoteTimer,
                EventTimer = entity.EventTimer,
                Quests = entity.Quests != null ? entity.Quests.Cast<PlayerQuest>().ToArray() : new PlayerQuest[0],
                GuildId = entity.GuildId
            };
        }
    }
}
