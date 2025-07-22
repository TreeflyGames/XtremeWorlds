using Core;
using Core.Globals;
using CSScripting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualBasic;
using Server;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Reflection;
using static Core.Global.Command;
using static Core.Packets;
using static Core.Type;
using static Server.Animation;
using static Server.Event;
using static Server.Item;
using static Server.Moral;
using static Server.NetworkSend;
using static Server.Npc;
using static Server.Party;
using static Server.Pet;
using static Server.Player;
using static Server.Projectile;
using static Server.Resource;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Net.Http.Headers;

public class Script
{
    public void Loop()
    {

    }

    public void ServerSecond()
    {

    }


    public void ServerMinute()
    {

    }

    public void JoinGame(int index)
    {
        // Warp the player to his saved location
        PlayerWarp(index, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index), (byte)Direction.Down);

        // Notify everyone that a player has joined the game.
        NetworkSend.GlobalMsg(string.Format("{0} has joined {1}!", GetPlayerName(index), SettingsManager.Instance.GameName));

        // Send all the required game data to the user.
        CheckEquippedItems(index);
        NetworkSend.SendInventory(index);
        NetworkSend.SendWornEquipment(index);
        NetworkSend.SendExp(index);
        NetworkSend.SendHotbar(index);
        NetworkSend.SendPlayerSkills(index);
        NetworkSend.SendStats(index);
        NetworkSend.SendJoinMap(index);

        // Send the flag so they know they can start doing stuff
        NetworkSend.SendInGame(index);

        // Send welcome messages
        NetworkSend.SendWelcome(index);
    }

    public void UseItem(int index, int itemNum, int invNum)
    {
        int i;
        int n;
        var tempItem = default(int);
        int m;
        var tempdata = new int[Enum.GetValues(typeof(Stat)).Length + 4];
        var tempstr = new string[3];

        // Find out what kind of item it is
        switch (Core.Data.Item[itemNum].Type)
        {
            case (byte)ItemCategory.Equipment:
                {
                    switch (Core.Data.Item[itemNum].SubType)
                    {
                        case (byte)Equipment.Weapon:
                            {

                                if (GetPlayerEquipment(index, Equipment.Weapon) >= 0)
                                {
                                    tempItem = GetPlayerEquipment(index, Equipment.Weapon);
                                }

                                SetPlayerEquipment(index, itemNum, Equipment.Weapon);

                                NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Data.Item[itemNum].Name), (int)Core.Color.BrightGreen);
                                TakeInv(index, itemNum, 1);

                                if (tempItem >= 0) // give back the stored item
                                {
                                    m = FindOpenInvSlot(index, tempItem);
                                    SetPlayerInv(index, m, tempItem);
                                    SetPlayerInvValue(index, m, 0);
                                }

                                NetworkSend.SendWornEquipment(index);
                                NetworkSend.SendMapEquipment(index);
                                NetworkSend.SendInventory(index);
                                NetworkSend.SendInventoryUpdate(index, invNum);
                                NetworkSend.SendStats(index);

                                // send vitals
                                NetworkSend.SendVitals(index);
                                break;
                            }

                        case (byte)Equipment.Armor:
                            {
                                if (GetPlayerEquipment(index, Equipment.Armor) >= 0)
                                {
                                    tempItem = GetPlayerEquipment(index, Equipment.Armor);
                                }

                                SetPlayerEquipment(index, itemNum, Equipment.Armor);

                                NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Data.Item[itemNum].Name), (int)Core.Color.BrightGreen);
                                TakeInv(index, itemNum, 1);

                                if (tempItem >= 0) // Return their old equipment to their inventory.
                                {
                                    m = FindOpenInvSlot(index, tempItem);
                                    SetPlayerInv(index, m, tempItem);
                                    SetPlayerInvValue(index, m, 0);
                                }

                                NetworkSend.SendWornEquipment(index);
                                NetworkSend.SendMapEquipment(index);

                                NetworkSend.SendInventory(index);
                                NetworkSend.SendStats(index);

                                // send vitals
                                NetworkSend.SendVitals(index);
                                break;
                            }

                        case (byte)Equipment.Helmet:
                            {
                                if (GetPlayerEquipment(index, Equipment.Helmet) >= 0)
                                {
                                    tempItem = GetPlayerEquipment(index, Equipment.Helmet);
                                }

                                SetPlayerEquipment(index, itemNum, Equipment.Helmet);

                                NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Data.Item[itemNum].Name), (int)Core.Color.BrightGreen);
                                TakeInv(index, itemNum, 1);

                                if (tempItem >= 0) // give back the stored item
                                {
                                    m = FindOpenInvSlot(index, tempItem);
                                    SetPlayerInv(index, m,  tempItem);
                                    SetPlayerInvValue(index, m, 0);
                                }

                                NetworkSend.SendWornEquipment(index);
                                NetworkSend.SendMapEquipment(index);
                                NetworkSend.SendInventory(index);
                                NetworkSend.SendStats(index);

                                // send vitals
                                NetworkSend.SendVitals(index);
                                break;
                            }

                        case (byte)Equipment.Shield:
                            {
                                if (GetPlayerEquipment(index, Equipment.Shield) >= 0)
                                {
                                    tempItem = GetPlayerEquipment(index, Equipment.Shield);
                                }

                                SetPlayerEquipment(index, itemNum, Equipment.Shield);

                                NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Data.Item[itemNum].Name), (int)Core.Color.BrightGreen);
                                TakeInv(index, itemNum, 1);

                                if (tempItem >= 0) // give back the stored item
                                {
                                    m = FindOpenInvSlot(index, tempItem);
                                    SetPlayerInv(index, m, tempItem);
                                    SetPlayerInvValue(index, m, 0);
                                }

                                NetworkSend.SendWornEquipment(index);
                                NetworkSend.SendMapEquipment(index);
                                NetworkSend.SendInventory(index);
                                NetworkSend.SendStats(index);

                                // send vitals
                                NetworkSend.SendVitals(index);
                                break;
                            }

                    }

                    break;
                }

            case (byte)ItemCategory.Consumable:
                {
                    switch (Core.Data.Item[itemNum].SubType)
                    {
                        case (byte)ConsumableEffect.RestoresHealth:
                            {
                                NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + Core.Data.Item[itemNum].Data1, (int)Core.Color.BrightGreen, (byte)Core.ActionMessageType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                Server.Animation.SendAnimation(GetPlayerMap(index), Core.Data.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                SetPlayerVital(index, Vital.Health, GetPlayerVital(index, Vital.Health) + Core.Data.Item[itemNum].Data1);
                                if (Core.Data.Item[itemNum].Stackable == 1)
                                {
                                    TakeInv(index, itemNum, 1);
                                }
                                else
                                {
                                    TakeInv(index, itemNum, 0);
                                }
                                NetworkSend.SendVital(index, Vital.Health);
                                break;
                            }

                        case (byte)ConsumableEffect.RestoresMana:
                            {
                                NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + Core.Data.Item[itemNum].Data1, (int)Core.Color.BrightBlue, (byte)Core.ActionMessageType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                Server.Animation.SendAnimation(GetPlayerMap(index), Core.Data.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                SetPlayerVital(index, Vital.Stamina, GetPlayerVital(index, Vital.Stamina) + Core.Data.Item[itemNum].Data1);
                                if (Core.Data.Item[itemNum].Stackable == 1)
                                {
                                    TakeInv(index, itemNum, 1);
                                }
                                else
                                {
                                    TakeInv(index, itemNum, 0);
                                }
                                NetworkSend.SendVital(index, Vital.Stamina);
                                break;
                            }

                        case (byte)ConsumableEffect.RestoresStamina:
                            {
                                Server.Animation.SendAnimation(GetPlayerMap(index), Core.Data.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                SetPlayerVital(index, Vital.Stamina, GetPlayerVital(index, Vital.Stamina) + Core.Data.Item[itemNum].Data1);
                                if (Core.Data.Item[itemNum].Stackable == 1)
                                {
                                    TakeInv(index, itemNum, 1);
                                }
                                else
                                {
                                    TakeInv(index, itemNum, 0);
                                }
                                NetworkSend.SendVital(index, Vital.Stamina);
                                break;
                            }

                        case (byte)ConsumableEffect.GrantsExperience:
                            {
                                Server.Animation.SendAnimation(GetPlayerMap(index), Core.Data.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                SetPlayerExp(index, GetPlayerExp(index) + Core.Data.Item[itemNum].Data1);
                                if (Core.Data.Item[itemNum].Stackable == 1)
                                {
                                    TakeInv(index, itemNum, 1);
                                }
                                else
                                {
                                    TakeInv(index, itemNum, 0);
                                }
                                NetworkSend.SendExp(index);
                                break;
                            }

                    }

                    break;
                }

            case (byte)ItemCategory.Projectile:
                {
                    if (Core.Data.Item[itemNum].Ammo > 0)
                    {
                        if (HasItem(index, Core.Data.Item[itemNum].Ammo) > 0)
                        {
                            TakeInv(index, Core.Data.Item[itemNum].Ammo, 1);
                            Server.Projectile.PlayerFireProjectile(index);
                        }
                        else
                        {
                            NetworkSend.PlayerMsg(index, "No More " + Core.Data.Item[Core.Data.Item[GetPlayerEquipment(index, Equipment.Weapon)].Ammo].Name + " !", (int)Core.Color.BrightRed);
                            return;
                        }
                    }
                    else
                    {
                        Server.Projectile.PlayerFireProjectile(index);
                        return;
                    }

                    break;
                }

            case (byte)ItemCategory.Event:
                {
                    n = Core.Data.Item[itemNum].Data1;

                    switch (Core.Data.Item[itemNum].SubType)
                    {
                        case (byte)Core.EventCommand.ModifyVariable:
                            {
                                Core.Data.Player[index].Variables[n] = Core.Data.Item[itemNum].Data2;
                                break;
                            }
                        case (byte)Core.EventCommand.ModifySwitch:
                            {
                                Core.Data.Player[index].Switches[n] = (byte)Core.Data.Item[itemNum].Data2;
                                break;
                            }
                        case (byte)Core.EventCommand.Key:
                            {
                                EventLogic.TriggerEvent(index, 1, 0, GetPlayerX(index), GetPlayerY(index));
                                break;
                            }
                    }

                    break;
                }

            case (byte)ItemCategory.Skill:
                {
                    PlayerLearnSkill(index, itemNum);
                    break;
                }

            case (byte)ItemCategory.Pet:
                {
                    TakeInv(index, itemNum, 1);
                    n = Core.Data.Item[itemNum].Data1;
                    break;
                }
        }
    }

    public static void PlayerLearnSkill(int index, int itemNum, int skillNum = -1)
    {
        int n;
        int i;

        // Get the skill num
        if (skillNum >= 0)
        {
            n = skillNum;
        }
        else
        {
            n = Core.Data.Item[itemNum].Data1;
        }

        if (n < 0 | n > Core.Constant.MAX_SKILLS)
            return;

        // Make sure they are the right class
        if (Data.Skill[n].JobReq == GetPlayerJob(index) | Data.Skill[n].JobReq == -1)
        {
            // Make sure they are the right level
            i = Data.Skill[n].LevelReq;

            if (i <= GetPlayerLevel(index))
            {
                i = FindOpenSkill(index);

                // Make sure they have an open skill slot
                if (i >= 0)
                {
                    // Make sure they dont already have the skill
                    if (!HasSkill(index, n))
                    {
                        SetPlayerSkill(index, i, n);
                        if (itemNum >= 0)
                        {
                            Server.Animation.SendAnimation(GetPlayerMap(index), Core.Data.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                            TakeInv(index, itemNum, 0);
                        }
                        NetworkSend.PlayerMsg(index, "You study the skill carefully.", (int)Core.Color.Yellow);
                        NetworkSend.PlayerMsg(index, "You have learned a new skill!", (int)Core.Color.BrightGreen);
                        NetworkSend.SendPlayerSkills(index);
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "You have already learned this skill!", (int)Core.Color.BrightRed);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "You have learned all that you can learn!", (int)Core.Color.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You must be level " + i + " to learn this skill.", (int)Core.Color.Yellow);
            }
        }
        else
        {
            NetworkSend.PlayerMsg(index, string.Format("Only {0} can use this skill.", GameLogic.CheckGrammar(Data.Job[Data.Skill[n].JobReq].Name, 1)), (int)Core.Color.BrightRed);
        }
    }

    public void JoinMap(int index)
    {
        int i;
        byte[] data;
        int dataSize;
        int mapNum = GetPlayerMap(index);

        // Send all players on current map to index
        var loopTo = NetworkConfig.Socket.HighIndex;
        for (i = 0; i < loopTo; i++)
        {
            if (IsPlaying(i))
            {
                if (i != index)
                {
                    if (GetPlayerMap(i) == mapNum)
                    {
                        data = PlayerData(i);
                        dataSize = data.Length;
                        NetworkConfig.Socket.SendDataTo(index, data, dataSize);
                        SendPlayerXYTo(index, i);
                        NetworkSend.SendMapEquipmentTo(index, i);
                    }
                }
            }
        }

        EventLogic.SpawnMapEventsFor(index, GetPlayerMap(index));

        // Send index's player data to everyone on the map including himself
        data = PlayerData(index);
        NetworkConfig.SendDataToMap(mapNum, data, data.Length);
        SendPlayerXYToMap(index);
        NetworkSend.SendMapEquipment(index);
        NetworkSend.SendVitals(index);
    }

    public void LeaveMap(int index, int mapNum)
    {

    }

    public void LeftGame(int index)
    {

    }

    public void OnDeath(int index)
    {
        // Set HP to nothing
        SetPlayerVital(index, Core.Vital.Health, 0);

        // Restore vitals
        var count = System.Enum.GetValues(typeof(Core.Vital)).Length;
        for (int i = 0, loopTo = count; i < loopTo; i++)
            SetPlayerVital(index, (Core.Vital)i, GetPlayerMaxVital(index, (Core.Vital)i));

        // If the player the attacker killed was a pk then take it away
        if (GetPlayerPK(index))
        {
            SetPlayerPK(index, false);
        }

        ref var withBlock = ref Data.Map[GetPlayerMap(index)];

        // Warp player away
        SetPlayerDir(index, (byte)Direction.Down);

        // to the bootmap if it is set
        if (withBlock.BootMap > 0)
        {
            PlayerWarp(index, withBlock.BootMap, withBlock.BootX, withBlock.BootY, (int)Direction.Down);
        }
        else
        {
            PlayerWarp(index, Data.Job[GetPlayerJob(index)].StartMap, Data.Job[GetPlayerJob(index)].StartX, Data.Job[GetPlayerJob(index)].StartY, (int)Direction.Down);
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
            NetworkSend.PlayerMsg(index, "You've lost no experience.", (int)Core.Color.BrightGreen);
        }
        else
        {                   
            NetworkSend.SendExp(index);
            NetworkSend.PlayerMsg(index, string.Format("You've lost {0} experience.", exp), (int)Core.Color.BrightRed);
        }

        return exp;
    }

    public void TrainStat(int index, int tmpStat)
    {
        // make sure their stats are not maxed
        if (GetPlayerRawStat(index, (Stat)tmpStat) >= Core.Constant.MAX_STATS)
        {
            NetworkSend.PlayerMsg(index, "You cannot spend any more points on that stat.", (int)Core.Color.BrightRed);
            return;
        }

        // increment stat
        SetPlayerStat(index, (Stat)tmpStat, GetPlayerRawStat(index, (Stat)tmpStat) + 1);

        // decrement points
        SetPlayerPoints(index, GetPlayerPoints(index) - 1);

        // send player new data
        NetworkSend.SendPlayerData(index);
    }

    public void PlayerMove(int index)
    {

    }

    public void UpdateMapAI()
    {

        long tickCount = General.GetTimeMs();
        var entities = Core.Globals.Entity.Instances;

        for (int x = 0; x < entities.Count; x++)
        {
            var entity = entities[x];
            var mapNum = entity.Map;
            if (entity == null) continue;

            // Only process entities that are Npcs
            if (entity.Num < 0) continue;

            // check if they've completed casting, and if so set the actual skill going
            if (entity.SkillBuffer >= 0)
            {
                if (General.GetTimeMs() > entity.SkillBufferTimer + Data.Skill[entity.SkillBuffer].CastTime * 1000)
                {
                    if (Data.Moral[Data.Map[mapNum].Moral].CanCast)
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
                if (entity.Behaviour == (byte)NpcBehavior.AttackOnSight || entity.Behaviour == (byte)NpcBehavior.Guard)
                {
                    // make sure it's not stunned
                    if (!(entity.StunDuration > 0))
                    {
                        int loopTo4 = NetworkConfig.Socket.HighIndex;
                        for (int i = 0; i < loopTo4; i++)
                        {
                            if (NetworkConfig.IsPlaying(i))
                            {
                                if (GetPlayerMap(i) == mapNum && entity.TargetType == 0 && GetPlayerAccess(i) <= (byte)AccessLevel.Moderator)
                                {
                                    int n = entity.Range;
                                    int distanceX = entity.X - GetPlayerX(i);
                                    int distanceY = entity.Y - GetPlayerY(i);

                                    if (distanceX < 0) distanceX *= -1;
                                    if (distanceY < 0) distanceY *= -1;

                                    if (distanceX <= n && distanceY <= n)
                                    {
                                        if (entity.Behaviour == (byte)NpcBehavior.AttackOnSight || GetPlayerPK(i))
                                        {
                                            if (!string.IsNullOrEmpty(entity.AttackSay))
                                            {
                                                NetworkSend.PlayerMsg(i, GameLogic.CheckGrammar(entity.Name, 1) + " says, '" + entity.AttackSay + "' to you.", (int)Core.Color.Yellow);
                                            }
                                            entity.TargetType = (byte)TargetType.Player;
                                            entity.Target = i;
                                        }
                                    }
                                }
                            }
                        }

                        // Check if target was found for Npc targeting
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

                                        if (distanceX <= n && distanceY <= n && entity.Behaviour == (byte)NpcBehavior.AttackOnSight)
                                        {
                                            entity.TargetType = (byte)TargetType.Npc;
                                            entity.Target = i;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                bool targetVerify = false;

                // Npc walking/targeting
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

                    if (entity.Type == Core.Globals.Entity.EntityType.Npc)
                    {
                        if (entity.Behaviour != (byte)NpcBehavior.ShopKeeper && entity.Behaviour != (byte)NpcBehavior.QuestGiver)
                        {
                            if (target > 0)
                            {
                                if (entities[mapNum].Map == mapNum)
                                {
                                    targetVerify = true;
                                    targetX = entities[target].X;
                                    targetY = entities[target].Y;
                                }
                                else
                                {
                                    entity.TargetType = 0;
                                    entity.Target = 0;
                                }
                            }

                            if (targetVerify)
                            {
                                if (!Server.Event.IsOneBlockAway(targetX, targetY, (int)entity.X, (int)entity.Y))
                                {
                                    int i = EventLogic.FindNpcPath(mapNum, Core.Globals.Entity.Index(entity), targetX, targetY);
                                    if (i < 4)
                                    {
                                        if (Server.Npc.CanNpcMove(mapNum, Core.Globals.Entity.Index(entity), (byte)i))
                                        {
                                            Server.Npc.NpcMove(mapNum, Core.Globals.Entity.Index(entity), i, (int)MovementState.Walking);
                                        }
                                    }
                                    else
                                    {
                                        i = (int)Math.Round(new Random().NextDouble() * 3) + 1;
                                        if (i == 1)
                                        {
                                            i = (int)Math.Round(new Random().NextDouble() * 3) + 1;
                                            if (Server.Npc.CanNpcMove(mapNum, Core.Globals.Entity.Index(entity), (byte)i))
                                            {
                                                Server.Npc.NpcMove(mapNum, Core.Globals.Entity.Index(entity), i, (int)MovementState.Walking);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Server.Npc.NpcDir(mapNum, Core.Globals.Entity.Index(entity), Server.Event.GetNpcDir(targetX, targetY, (int)entity.X, (int)entity.Y));
                                }
                            }
                            else
                            {
                                int i = (int)Math.Round(new Random().NextDouble() * 4);
                                if (i == 1)
                                {
                                    i = (int)Math.Round(new Random().NextDouble() * 4);
                                    if (Server.Npc.CanNpcMove(mapNum, Core.Globals.Entity.Index(entity), (byte)i))
                                    {
                                        Server.Npc.NpcMove(mapNum, Core.Globals.Entity.Index(entity), i, (int)MovementState.Walking);
                                    }
                                }
                            }
                        }
                    }

                    // Npcs attack targets
                    int attackTarget = entity.Target;
                    byte attackTargetType = entity.TargetType;

                    if (attackTarget > 0)
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

                    // Regenerate HP/MP
                    if (entity.Vital[(byte)Vital.Health] > 0)
                    {
                        // Placeholder for HP regen
                        if (entity.Vital[(byte)Vital.Health] > GameLogic.GetNpcMaxVital(x, Vital.Health))
                        {
                            entity.Vital[(byte)Vital.Health] = GameLogic.GetNpcMaxVital(x, Vital.Health);
                        }
                    }

                    if (entity.Vital[(byte)Vital.Stamina] > 0)
                    {
                        // Placeholder for SP regen
                        if (entity.Vital[(byte)Vital.Stamina] > GameLogic.GetNpcMaxVital(x, Vital.Stamina))
                        {
                            entity.Vital[(byte)Vital.Stamina] = GameLogic.GetNpcMaxVital(x, Vital.Stamina);
                        }
                    }

                    // Check if the npc is dead or not
                    if (entity.Vital[(byte)Vital.Health] < 0 && entity.SpawnWait > 0)
                    {
                        entity.Num = 0;
                        entity.SpawnWait = General.GetTimeMs();
                        entity.Vital[(byte)Vital.Health] = 0;
                    }

                    // Spawning an Npc
                    if (entity.Type == Core.Globals.Entity.EntityType.Npc)
                    {
                        if (entity.Num == -1)
                        {
                            if (entity.SpawnSecs > 0)
                            {
                                if (tickCount > entity.SpawnWait + entity.SpawnSecs * 1000)
                                {
                                    Server.Npc.SpawnNpc(x, mapNum);
                                }
                            }
                        }
                    }
                }
            }
        }

        var now = General.GetTimeMs();
        var itemCount = Core.Constant.MAX_MAP_ITEMS;
        var mapCount = Core.Constant.MAX_MAPS;

        for (int mapNum = 0; mapNum < mapCount; mapNum++)
        {
            // Handle map items (public/despawn)
            for (int i = 0; i < itemCount; i++)
            {
                var item = Data.MapItem[mapNum, i];
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
            var mapResource = Data.MapResource[mapNum];
            if (mapResource.ResourceCount > 0)
            {
                for (int i = 0; i < mapResource.ResourceCount; i++)
                {
                    var resData = mapResource.ResourceData[i];
                    int resourceindex = Data.Map[mapNum].Tile[resData.X, resData.Y].Data1;
                    if (resourceindex > 0)
                    {
                        if (resData.State == 1 || resData.Health < 1)
                        {
                            if (resData.Timer + Data.Resource[resourceindex].RespawnTime * 1000 < now)
                            {
                                resData.Timer = now;
                                resData.State = 0;
                                resData.Health = (byte)Data.Resource[resourceindex].Health;
                                Server.Resource.SendMapResourceToMap(mapNum, i);
                            }
                        }
                    }
                }
            }
        }
    }
}