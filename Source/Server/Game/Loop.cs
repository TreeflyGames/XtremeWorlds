using Core;
using Core.Globals;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw.Network;
using MonoGame.Extended.ECS;
using Npgsql.Replication.PgOutput.Messages;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Type;

namespace Server
{

    public class Loop
    {

        public static async Task ServerAsync()
        {
            int tick;
            var tmr25 = default(int);
            var tmr500 = default(int);
            var tmr1000 = default(int);
            var tmr60000 = default(int);
            var lastUpdateSavePlayers = default(int);
            var lastUpdateMapSpawnItems = default(int);

            do
            {
                // Update our current tick value.
                tick = General.GetTimeMs();

                // Don't process anything else if we're going down.
                if (General.IsServerDestroyed)

                    // Get all our online players.
                    Debugger.Break(); var onlinePlayers = Core.Type.TempPlayer.Where(player => player.InGame).Select((player, index) => new { Index = Operators.AddObject(index, 1), player }).ToArray();

                await General.CheckShutDownCountDownAsync();

                if (tick > tmr25)
                {                
                    // Update all our available events.
                    EventLogic.UpdateEventLogic();

                    // Move the timer up 25ms.
                    tmr25 = General.GetTimeMs() + 25;
                }

                if (tick > tmr60000)
                {
                    try
                    {                    
                        Script.Instance?.ServerMinute();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    tmr60000 = General.GetTimeMs() + 60000;
                }

                if (tick > tmr1000)
                {
                    try
                    {
                        Script.Instance?.ServerSecond();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    for (int index = 0; index < NetworkConfig.Socket.HighIndex; index++)
                    {
                        if (Core.Type.Player[index].Moving > 0 && Core.Type.Player[index].IsMoving)
                        {
                            Player.PlayerMove(index, Core.Type.Player[index].Dir, Core.Type.Player[index].Moving, false);
                            Core.Type.Player[index].IsMoving = false;
                        }
                    }

                    Clock.Instance.Tick();

                    // Move the timer up 1000ms.
                    tmr1000 = General.GetTimeMs() + 1000;
                }

                if (tick > tmr500)
                {
                    // Check for disconnects
                    for (int i = 0; i < NetworkConfig.Socket.HighIndex; i++)
                    {
                        if (!NetworkConfig.Socket.IsConnected(i))
                        {
                            if (IsPlaying(i))
                            {
                                await Player.LeftGame(i);
                            }
                        }
                    }

                    UpdateMapAI();

                    // Move the timer up 500ms.
                    tmr500 = General.GetTimeMs() + 500;
                }

                // Checks to spawn map items every 1 minute
                if (tick > lastUpdateMapSpawnItems)
                {
                    UpdateMapSpawnItems();
                    lastUpdateMapSpawnItems = General.GetTimeMs() + 60000;
                }

                // Checks to save players every 5 minutes
                if (tick > lastUpdateSavePlayers)
                {
                    UpdateSavePlayers();
                    lastUpdateSavePlayers = General.GetTimeMs() + 300000;
                }

                try
                {
                    Script.Instance?.Loop();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                await Task.Delay(1);
            }
            while (true);
        }

        public static void UpdateSavePlayers()
        {
            int i;

            if (NetworkConfig.Socket?.HighIndex > 0)
            {
                Console.WriteLine("Saving all online players...");

                var loopTo = NetworkConfig.Socket.HighIndex;
                for (i = 0; i < loopTo; i++)
                {
                    Database.SaveCharacter(i, Core.Type.TempPlayer[i].Slot);
                    Database.SaveBank(i);
                }

            }

        }

        private static void UpdateMapSpawnItems()
        {
            int x;
            int y;

            // ///////////////////////////////////////////
            // // This is used for respawning map items //
            // ///////////////////////////////////////////
            var loopTo = Core.Constant.MAX_MAPS;
            for (y = 0; y < loopTo; y++)
            {
                // Clear out unnecessary junk
                var loopTo1 = Core.Constant.MAX_MAP_ITEMS;
                for (x = 0; x < loopTo1; x++)
                    Database.ClearMapItem(x, y);

                // Spawn the items
                Item.SpawnMapItems(y);
                Item.SendMapItemsToAll(y);
            }
            
        }

        private static void UpdateMapAI()
        {
            // Clear the entity list before repopulating to avoid accumulating instances
            Core.Globals.Entity.Instances.Clear();

            var entities = Core.Globals.Entity.Instances;
            var mapCount = Core.Constant.MAX_MAPS;

            // Use entities from Core.Globals.Entity class
            for (int mapNum = 0; mapNum < mapCount; mapNum++)
            {
                // Add NPCs
                for (int i = 0; i < Core.Constant.MAX_MAP_NPCS; i++)
                {
                    var npc = Core.Globals.Entity.FromNPC(i, Core.Type.MapNPC[mapNum].NPC[i]);
                    if (npc.Num >= 0)
                    {
                        entities.Add(npc);
                    }
                }

                // Add Players
                for (int i = 0; i < NetworkConfig.Socket.HighIndex; i++)
                {
                    if (Core.Type.Player[i].Map == mapNum)
                    {
                        var player = Core.Globals.Entity.FromPlayer(i, Core.Type.Player[i]);
                        if (player.Num >= 0)
                        {
                            entities.Add(player);
                        }
                    }
                }

                // Add Pets
                /*
                for (int i = 0; i < Core.Constant.MAX_PETS; i++)
                {
                    if (Core.Type.Pet[i] > 0)
                    {
                        var petEntity = Core.Globals.Entity.FromPet(i, Core.Type.Pet[i]);
                        if (petEntity != null && petEntity.Map == mapNum)
                        {
                            entities.Add(petEntity);
                        }
                    }
                }
                */
            }

            var now = General.GetTimeMs();
            var itemCount = Core.Constant.MAX_MAP_ITEMS;

            for (int mapNum = 0; mapNum < mapCount; mapNum++)
            {
                // Handle map items (public/despawn)
                for (int i = 0; i < itemCount; i++)
                {
                    var item = Core.Type.MapItem[mapNum, i];
                    if (item.Num >= 0 && !string.IsNullOrEmpty(item.PlayerName))
                    {
                        if (item.PlayerTimer < now)
                        {
                            item.PlayerName = "";
                            item.PlayerTimer = 0;
                            Item.SendMapItemsToAll(mapNum);
                        }
                        if (item.CanDespawn && item.DespawnTimer < now)
                        {
                            Database.ClearMapItem(i, mapNum);
                            Item.SendMapItemsToAll(mapNum);
                        }
                    }
                }

                // Respawn resources
                var mapResource = Core.Type.MapResource[mapNum];
                if (mapResource.ResourceCount > 0)
                {
                    for (int i = 0; i < mapResource.ResourceCount; i++)
                    {
                        var resData = mapResource.ResourceData[i];
                        int resourceindex = Core.Type.Map[mapNum].Tile[resData.X, resData.Y].Data1;
                        if (resourceindex > 0)
                        {
                            if (resData.State == 1 || resData.Health < 1)
                            {
                                if (resData.Timer + Core.Type.Resource[resourceindex].RespawnTime * 1000 < now)
                                {
                                    resData.Timer = now;
                                    resData.State = 0;
                                    resData.Health = (byte)Core.Type.Resource[resourceindex].Health;
                                    Resource.SendMapResourceToMap(mapNum, i);
                                }
                            }
                        }
                    }
                }

                long tickCount = General.GetTimeMs();

                for (int x = 0; x < entities.Count; x++)
                {
                    var entity = entities[x];
                    mapNum = entity.Map;
                    if (entity == null) continue;

                    // Only process entities that are NPCs
                    if (entity.Num < 0) continue;

                    // check if they've completed casting, and if so set the actual skill going
                    if (entity.SkillBuffer >= 0)
                    {
                        if (General.GetTimeMs() > entity.SkillBufferTimer + Core.Type.Skill[entity.SkillBuffer].CastTime * 1000)
                        {
                            if (Core.Type.Moral[Map[mapNum].Moral].CanCast)
                            {
                                //BufferSkill(mapNum, [Core.Globals.Entity.Index(entity), entity.SkillBuffer);
                                entity.SkillBuffer = -1;
                                entity.SkillBufferTimer = 0;
                            }
                        }
                    }
                    else
                    {
                        // ATTACKING ON SIGHT
                        if (entity.Behaviour == (byte)NPCBehavior.AttackOnSight || entity.Behaviour == (byte)NPCBehavior.Guard)
                        {
                            // make sure it's not stunned
                            if (!(entity.StunDuration > 0))
                            {
                                int loopTo4 = NetworkConfig.Socket.HighIndex;
                                for (int i = 0; i < loopTo4; i++)
                                {
                                    if (NetworkConfig.IsPlaying(i))
                                    {
                                        if (GetPlayerMap(i) == mapNum && entity.TargetType == 0 && GetPlayerAccess(i) <= (byte)AccessType.Moderator)
                                        {
                                            int n = entity.Range;
                                            int distanceX = entity.X - GetPlayerX(i);
                                            int distanceY = entity.Y - GetPlayerY(i);

                                            if (distanceX < 0) distanceX *= -1;
                                            if (distanceY < 0) distanceY *= -1;

                                            if (distanceX <= n && distanceY <= n)
                                            {
                                                if (entity.Behaviour == (byte)NPCBehavior.AttackOnSight || GetPlayerPK(i))
                                                {
                                                    if (!string.IsNullOrEmpty(entity.AttackSay))
                                                    {
                                                        NetworkSend.PlayerMsg(i, GameLogic.CheckGrammar(entity.Name, 1) + " says, '" + entity.AttackSay + "' to you.", (int)ColorType.Yellow);
                                                    }
                                                    entity.TargetType = (byte)Core.Enum.TargetType.Player;
                                                    entity.Target = i;
                                                }
                                            }
                                        }
                                    }
                                }

                                // Check if target was found for NPC targeting
                                if (entity.TargetType == 0 && entity.Faction > 0)
                                {
                                    for (int i = 0; i < entities.Count; i++)
                                    {
                                        var otherEntity = entities[i];
                                        if (otherEntity != null && otherEntity.Num >= 0)
                                        {
                                            if (otherEntity.Map != mapNum) continue;
                                            if (ReferenceEquals(otherEntity, entity)) continue;
                                            if ((int)otherEntity.Faction > 0 && otherEntity.Faction != entity.Faction)
                                            {
                                                int n = entity.Range;
                                                int distanceX = entity.X - otherEntity.X;
                                                int distanceY = entity.Y - otherEntity.Y;

                                                if (distanceX < 0) distanceX *= -1;
                                                if (distanceY < 0) distanceY *= -1;

                                                if (distanceX <= n && distanceY <= n && entity.Behaviour == (byte)NPCBehavior.AttackOnSight)
                                                {
                                                    entity.TargetType = (byte)Core.Enum.TargetType.NPC;
                                                    entity.Target = i;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        bool targetVerify = false;

                        // NPC walking/targeting
                        if (entity.StunDuration > 0)
                        {
                            if (General.GetTimeMs() > entity.StunTimer + entity.StunDuration * 1000)
                            {
                                entity.StunDuration = 0;
                                entity.StunTimer = 0;
                            }
                        }
                        else
                        {
                            int target = entity.Target;
                            byte targetType = entity.TargetType;
                            int targetX = 0, targetY = 0;

                            if (entity.Behaviour != (byte)NPCBehavior.ShopKeeper && entity.Behaviour != (byte)NPCBehavior.Quest)
                            {
                                if (targetType == (byte)Core.Enum.TargetType.Player)
                                {
                                    if (target > 0)
                                    {
                                        if (GetPlayerMap(target) == mapNum)
                                        {
                                            targetVerify = true;
                                            targetY = GetPlayerY(target);
                                            targetX = GetPlayerX(target);
                                        }
                                        else
                                        {
                                            entity.TargetType = 0;
                                            entity.Target = 0;
                                        }
                                    }
                                }
                                else if (targetType == (byte)Core.Enum.TargetType.NPC)
                                {
                                    if (target > 0 && target < entities.Count)
                                    {
                                        var targetEntity = Core.Type.MapNPC[mapNum].NPC[target];
                                        if (targetEntity.Num >= 0)
                                        {
                                            targetVerify = true;
                                            targetY = targetEntity.Y;
                                            targetX = targetEntity.X;
                                        }
                                        else
                                        {
                                            entity.TargetType = 0;
                                            entity.Target = 0;
                                        }
                                    }
                                }

                                if (entity.Type == Core.Globals.Entity.EntityType.NPC)
                                {
                                    if (targetVerify)
                                    {
                                        if (!Event.IsOneBlockAway(targetX, targetY, (int)entity.X, (int)entity.Y))
                                        {
                                            int i = EventLogic.FindNPCPath(mapNum, Core.Globals.Entity.Index(entity), targetX, targetY);
                                            if (i < 4)
                                            {
                                                if (NPC.CanNPCMove(mapNum, Core.Globals.Entity.Index(entity), (byte)i))
                                                {
                                                    NPC.NPCMove(mapNum, Core.Globals.Entity.Index(entity), i, (int)MovementType.Walking);
                                                }
                                            }
                                            else
                                            {
                                                i = (int)Math.Round(new Random().NextDouble() * 3) + 1;
                                                if (i == 1)
                                                {
                                                    i = (int)Math.Round(new Random().NextDouble() * 3) + 1;
                                                    if (NPC.CanNPCMove(mapNum, Core.Globals.Entity.Index(entity), (byte)i))
                                                    {
                                                        NPC.NPCMove(mapNum, Core.Globals.Entity.Index(entity), i, (int)MovementType.Walking);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            NPC.NPCDir(mapNum, Core.Globals.Entity.Index(entity), Event.GetNPCDir(targetX, targetY, (int)entity.X, (int)entity.Y));
                                        }
                                    }
                                    else
                                    {
                                        int i = (int)Math.Round(new Random().NextDouble() * 4);
                                        if (i == 1)
                                        {
                                            i = (int)Math.Round(new Random().NextDouble() * 4);
                                            if (NPC.CanNPCMove(mapNum, Core.Globals.Entity.Index(entity), (byte)i))
                                            {
                                                NPC.NPCMove(mapNum, Core.Globals.Entity.Index(entity), i, (int)MovementType.Walking);
                                            }
                                        }
                                    }
                                }
                            }

                            // NPCs attack targets
                            int attackTarget = entity.Target;
                            byte attackTargetType = entity.TargetType;

                            if (attackTarget > 0)
                            {
                                if (attackTargetType == (byte)Core.Enum.TargetType.Player)
                                {
                                    if (GetPlayerMap(attackTarget) == mapNum)
                                    {
                                        // Placeholder for attack logic
                                    }
                                    else
                                    {
                                        entity.Target = 0;
                                        entity.TargetType = 0;
                                    }
                                }
                                else if (attackTargetType == (byte)Core.Enum.TargetType.NPC)
                                {
                                    if (attackTarget < entities.Count)
                                    {
                                        var targetEntity = entities[attackTarget];
                                        if (targetEntity != null && targetEntity.Num >= 0 && targetEntity.Map == mapNum)
                                        {
                                            // Placeholder for NPC vs NPC attack logic
                                        }
                                        else
                                        {
                                            entity.Target = 0;
                                            entity.TargetType = 0;
                                        }
                                    }
                                    else
                                    {
                                        entity.Target = 0;
                                        entity.TargetType = 0;
                                    }
                                }
                            }

                            // Regenerate NPC's HP
                            if (entity.Vital[(byte)VitalType.HP] > 0)
                            {
                                // Placeholder for HP regen
                                if (entity.Vital[(byte)VitalType.HP] > GameLogic.GetNPCMaxVital(x, VitalType.HP))
                                {
                                    entity.Vital[(byte)VitalType.HP] = GameLogic.GetNPCMaxVital(x, VitalType.HP);
                                }
                            }

                            if (entity.Vital[(byte)VitalType.SP] > 0)
                            {
                                // Placeholder for SP regen
                                if (entity.Vital[(byte)VitalType.SP] > GameLogic.GetNPCMaxVital(x, VitalType.SP))
                                {
                                    entity.Vital[(byte)VitalType.SP] = GameLogic.GetNPCMaxVital(x, VitalType.SP);
                                }
                            }

                            // Check if the npc is dead or not
                            if (entity.Vital[(byte)VitalType.HP] < 0 && entity.SpawnWait > 0)
                            {
                                entity.Num = 0;
                                entity.SpawnWait = General.GetTimeMs();
                                entity.Vital[(byte)VitalType.HP] = 0;
                            }

                            // Spawning an NPC
                            if (entity.Type == Core.Globals.Entity.EntityType.NPC)
                            {
                                if (entity.Num == -1)
                                {
                                    if (entity.SpawnSecs > 0)
                                    {
                                        if (tickCount > entity.SpawnWait + entity.SpawnSecs * 1000)
                                        {
                                            NPC.SpawnNPC(x, mapNum);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Script.Instance?.UpdateMapAI();

            foreach (Core.Globals.Entity entity in Core.Globals.Entity.Instances)
            {
                switch (entity.Type)
                {
                    case Core.Globals.Entity.EntityType.NPC:
                        Core.Type.MapNPC[entity.Map].NPC[Core.Globals.Entity.Index(entity)] = Core.Globals.Entity.ToNPC(entity.Id, entity);
                        break;
                    case Core.Globals.Entity.EntityType.Player:
                        Core.Type.Player[Core.Globals.Entity.Index(entity)] = Core.Globals.Entity.ToPlayer(entity.Id, entity);
                        break;
                        /*
                        case Core.Globals.Entity.EntityType.Pet:
                            Core.Type.Pet[Core.Globals.Entity.Index(entity)] = Core.Globals.Entity.ToPet(entity.Id, entity);
                            break;
                        */
                }
            }
        }

        public static void CastSkill(int index, int skillSlot)
        {
            try
            {
                Script.Instance?.CastSkill(index, skillSlot);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}