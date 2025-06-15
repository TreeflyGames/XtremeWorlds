using Core;
using Core.Globals;
using CSScripting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualBasic;
using Server;
using System;
using System.ComponentModel.DataAnnotations;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Packets;
using static Core.Type;
using static Server.Animation;
using static Server.Event;
using static Server.Item;
using static Server.Moral;
using static Server.NetworkSend;
using static Server.NPC;
using static Server.Party;
using static Server.Pet;
using static Server.Player;
using static Server.Projectile;
using static Server.Resource;
using static System.Net.Mime.MediaTypeNames;

public class Script
{
    public void Loop()
    {

    }

    public void JoinGame(int index)
    {
        // Warp the player to his saved location
        PlayerWarp(index, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index), (byte)Core.Enum.DirectionType.Down);
    }

    public void LeftGame(int index)
    {

    }

    public void OnDeath(int index)
    {
        // Set HP to nothing
        SetPlayerVital(index, Core.Enum.VitalType.HP, 0);

        // Warp player away
        SetPlayerDir(index, (byte)Core.Enum.DirectionType.Down);

        // Restore vitals
        for (int i = 0, loopTo = (byte)Core.Enum.VitalType.Count; i < loopTo; i++)
            SetPlayerVital(index, (Core.Enum.VitalType)i, GetPlayerMaxVital(index, (Core.Enum.VitalType)i));

        // If the player the attacker killed was a pk then take it away
        if (GetPlayerPK(index))
        {
            SetPlayerPK(index, false);
        }

        ref var withBlock = ref Core.Type.Map[GetPlayerMap(index)];

        // to the bootmap if it is set
        if (withBlock.BootMap > 0)
        {
            PlayerWarp(index, withBlock.BootMap, withBlock.BootX, withBlock.BootY, (int)Core.Enum.DirectionType.Down);
        }
        else
        {
            PlayerWarp(index, Core.Type.Job[GetPlayerJob(index)].StartMap, Core.Type.Job[GetPlayerJob(index)].StartX, Core.Type.Job[GetPlayerJob(index)].StartY, (int)Core.Enum.DirectionType.Down);
        }
    }

    public void BufferSkill(int mapNum, int index, int skillNum)
    {
  
    }

    public int KillPlayer(int index)
    {
        int exp = GetPlayerExp(index) / 3;
        
        if (exp == 0)
        {
            NetworkSend.PlayerMsg(index, "You've lost no experience.", (int)ColorType.BrightGreen);
        }
        else
        {                   
            NetworkSend.SendExp(index);
            NetworkSend.PlayerMsg(index, string.Format("You've lost {0} experience.", exp), (int)ColorType.BrightRed);
        }

        return exp;
    }

    public void UpdateMapAI()
    {
        var now = General.GetTimeMs();
        var mapCount = Core.Constant.MAX_MAPS;
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
                        Server.Item.SendMapItemsToAll(mapNum);
                    }
                    if (item.CanDespawn && item.DespawnTimer < now)
                    {
                        Database.ClearMapItem(i, mapNum);
                        Server.Item.SendMapItemsToAll(mapNum);
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
                                Server.Resource.SendMapResourceToMap(mapNum, i);
                            }
                        }
                    }
                }
            }


            long tickCount = General.GetTimeMs();

            // Use entities from Entity class
            var entities = Core.Globals.Entity.Instances;
            for (int x = 0; x < entities.Count; x++)
            {
                var entity = entities[x];
                if (entity == null || entity.Map != mapNum) continue;

                // Only process entities that are NPCs
                if (entity.Num < 0) continue;

                // check if they've completed casting, and if so set the actual skill going
                if (entity.SkillBuffer >= 0)
                {
                    if (General.GetTimeMs() > entity.SkillBufferTimer + Core.Type.Skill[entity.SkillBuffer >= 0 && entity.SkillBuffer < entity.Skill.Length ? entity.Skill[entity.SkillBuffer] : 0].CastTime * 1000)
                    {
                        if (Core.Type.Moral[Map[mapNum].Moral].CanCast)
                        {
                            BufferSkill(mapNum, x, entity.SkillBuffer);
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
                                    var targetEntity = entities[target];
                                    if (targetEntity != null && targetEntity.Num >= 0 && targetEntity.Map == mapNum)
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

                            if (entity.Type == Entity.EntityType.NPC)
                            {
                                 if (targetVerify)
                                {
                                    if (!Event.IsOneBlockAway(targetX, targetY, (int)entity.X, (int)entity.Y))
                                    {
                                        int i = EventLogic.FindNPCPath(mapNum, x, targetX, targetY);
                                        if (i < 4)
                                        {
                                            if (Server.NPC.CanNPCMove(mapNum, x, (byte)i))
                                            {
                                                Server.NPC.NPCMove(mapNum, x, i, (int)MovementType.Walking);
                                            }
                                        }
                                        else
                                        {
                                            i = (int)Math.Round(new Random().NextDouble() * 3) + 1;
                                            if (i == 1)
                                            {
                                                i = (int)Math.Round(new Random().NextDouble() * 3) + 1;
                                                if (Server.NPC.CanNPCMove(mapNum, x, (byte)i))
                                                {
                                                    Server.NPC.NPCMove(mapNum, x, i, (int)MovementType.Walking);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Server.NPC.NPCDir(mapNum, x, Event.GetNPCDir(targetX, targetY, (int)entity.X, (int)entity.Y));
                                    }
                                }
                                else
                                {
                                    int i = (int)Math.Round(new Random().NextDouble() * 4);
                                    if (i == 1)
                                    {
                                        i = (int)Math.Round(new Random().NextDouble() * 4);
                                        if (Server.NPC.CanNPCMove(mapNum, x, (byte)i))
                                        {
                                            Server.NPC.NPCMove(mapNum, x, i, (int)MovementType.Walking);
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
                        if (entity.Type == Entity.EntityType.NPC)
                        {
                            if (entity.Num == -1)
                            {
                                if (entity.SpawnSecs > 0)
                                {
                                    if (tickCount > entity.SpawnWait + entity.SpawnSecs * 1000)
                                    {
                                        Server.NPC.SpawnNPC(x, mapNum);
                                    }
                                }
                            }
                        }
                    }
                }

                // copy modified class to struct type
                switch (entity.Type)
                {
                    case Entity.EntityType.NPC:
                        Core.Type.MapNPC[mapNum].NPC[Core.Globals.Entity.Index(entity)] = Core.Globals.Entity.ToNPC(entity.Id, entity);
                        break;
                    case Entity.EntityType.Player:
                        Core.Type.Player[Core.Globals.Entity.Index(entity)] = Core.Globals.Entity.ToPlayer(entity.Id, entity);
                        break;
                    case Entity.EntityType.Pet:
                        Core.Type.Pet[Core.Globals.Entity.Index(entity)] = Core.Globals.Entity.ToPet(entity.Id, entity);
                        break;
                }
            }
        }
    }
}