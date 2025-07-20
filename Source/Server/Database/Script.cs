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
        PlayerWarp(index, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index), (byte)Core.Enum.DirectionType.Down);

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
        var tempdata = new int[(int)(StatType.Count + 3 + 1)];
        var tempstr = new string[3];

        // Find out what kind of item it is
        switch (Core.Type.Item[itemNum].Type)
        {
            case (byte)ItemType.Equipment:
                {
                    switch (Core.Type.Item[itemNum].SubType)
                    {
                        case (byte)EquipmentType.Weapon:
                            {

                                if (GetPlayerEquipment(index, EquipmentType.Weapon) >= 0)
                                {
                                    tempItem = GetPlayerEquipment(index, EquipmentType.Weapon);
                                }

                                SetPlayerEquipment(index, itemNum, EquipmentType.Weapon);

                                NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int)ColorType.BrightGreen);
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

                        case (byte)EquipmentType.Armor:
                            {
                                if (GetPlayerEquipment(index, EquipmentType.Armor) >= 0)
                                {
                                    tempItem = GetPlayerEquipment(index, EquipmentType.Armor);
                                }

                                SetPlayerEquipment(index, itemNum, EquipmentType.Armor);

                                NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int)ColorType.BrightGreen);
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

                        case (byte)EquipmentType.Helmet:
                            {
                                if (GetPlayerEquipment(index, EquipmentType.Helmet) >= 0)
                                {
                                    tempItem = GetPlayerEquipment(index, EquipmentType.Helmet);
                                }

                                SetPlayerEquipment(index, itemNum, EquipmentType.Helmet);

                                NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int)ColorType.BrightGreen);
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

                        case (byte)EquipmentType.Shield:
                            {
                                if (GetPlayerEquipment(index, EquipmentType.Shield) >= 0)
                                {
                                    tempItem = GetPlayerEquipment(index, EquipmentType.Shield);
                                }

                                SetPlayerEquipment(index, itemNum, EquipmentType.Shield);

                                NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int)ColorType.BrightGreen);
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

            case (byte)ItemType.Consumable:
                {
                    switch (Core.Type.Item[itemNum].SubType)
                    {
                        case (byte)ConsumableType.HP:
                            {
                                NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + Core.Type.Item[itemNum].Data1, (int)ColorType.BrightGreen, (byte)ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                Server.Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                SetPlayerVital(index, VitalType.HP, GetPlayerVital(index, VitalType.HP) + Core.Type.Item[itemNum].Data1);
                                if (Core.Type.Item[itemNum].Stackable == 1)
                                {
                                    TakeInv(index, itemNum, 1);
                                }
                                else
                                {
                                    TakeInv(index, itemNum, 0);
                                }
                                NetworkSend.SendVital(index, VitalType.HP);
                                break;
                            }

                        case (byte)ConsumableType.MP:
                            {
                                NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + Core.Type.Item[itemNum].Data1, (int)ColorType.BrightBlue, (byte)ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                Server.Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                SetPlayerVital(index, VitalType.SP, GetPlayerVital(index, VitalType.SP) + Core.Type.Item[itemNum].Data1);
                                if (Core.Type.Item[itemNum].Stackable == 1)
                                {
                                    TakeInv(index, itemNum, 1);
                                }
                                else
                                {
                                    TakeInv(index, itemNum, 0);
                                }
                                NetworkSend.SendVital(index, VitalType.SP);
                                break;
                            }

                        case (byte)ConsumableType.SP:
                            {
                                Server.Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                SetPlayerVital(index, VitalType.SP, GetPlayerVital(index, VitalType.SP) + Core.Type.Item[itemNum].Data1);
                                if (Core.Type.Item[itemNum].Stackable == 1)
                                {
                                    TakeInv(index, itemNum, 1);
                                }
                                else
                                {
                                    TakeInv(index, itemNum, 0);
                                }
                                NetworkSend.SendVital(index, VitalType.SP);
                                break;
                            }

                        case (byte)ConsumableType.Exp:
                            {
                                Server.Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                SetPlayerExp(index, GetPlayerExp(index) + Core.Type.Item[itemNum].Data1);
                                if (Core.Type.Item[itemNum].Stackable == 1)
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

            case (byte)ItemType.Projectile:
                {
                    if (Core.Type.Item[itemNum].Ammo > 0)
                    {
                        if (HasItem(index, Core.Type.Item[itemNum].Ammo) > 0)
                        {
                            TakeInv(index, Core.Type.Item[itemNum].Ammo, 1);
                            Server.Projectile.PlayerFireProjectile(index);
                        }
                        else
                        {
                            NetworkSend.PlayerMsg(index, "No More " + Core.Type.Item[Core.Type.Item[GetPlayerEquipment(index, EquipmentType.Weapon)].Ammo].Name + " !", (int)ColorType.BrightRed);
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

            case (byte)ItemType.Event:
                {
                    n = Core.Type.Item[itemNum].Data1;

                    switch (Core.Type.Item[itemNum].SubType)
                    {
                        case (byte)CommonEventType.Variable:
                            {
                                Core.Type.Player[index].Variables[n] = Core.Type.Item[itemNum].Data2;
                                break;
                            }
                        case (byte)CommonEventType.Switch:
                            {
                                Core.Type.Player[index].Switches[n] = (byte)Core.Type.Item[itemNum].Data2;
                                break;
                            }
                        case (byte)CommonEventType.Key:
                            {
                                EventLogic.TriggerEvent(index, 1, 0, GetPlayerX(index), GetPlayerY(index));
                                break;
                            }
                    }

                    break;
                }

            case (byte)ItemType.Skill:
                {
                    PlayerLearnSkill(index, itemNum);
                    break;
                }

            case (byte)ItemType.Pet:
                {
                    TakeInv(index, itemNum, 1);
                    n = Core.Type.Item[itemNum].Data1;
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
            n = Core.Type.Item[itemNum].Data1;
        }

        if (n < 0 | n > Core.Constant.MAX_SKILLS)
            return;

        // Make sure they are the right class
        if (Core.Type.Skill[n].JobReq == GetPlayerJob(index) | Core.Type.Skill[n].JobReq == -1)
        {
            // Make sure they are the right level
            i = Core.Type.Skill[n].LevelReq;

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
                            Server.Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                            TakeInv(index, itemNum, 0);
                        }
                        NetworkSend.PlayerMsg(index, "You study the skill carefully.", (int)ColorType.Yellow);
                        NetworkSend.PlayerMsg(index, "You have learned a new skill!", (int)ColorType.BrightGreen);
                        NetworkSend.SendPlayerSkills(index);
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "You have already learned this skill!", (int)ColorType.BrightRed);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "You have learned all that you can learn!", (int)ColorType.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You must be level " + i + " to learn this skill.", (int)ColorType.Yellow);
            }
        }
        else
        {
            NetworkSend.PlayerMsg(index, string.Format("Only {0} can use this skill.", GameLogic.CheckGrammar(Core.Type.Job[Core.Type.Skill[n].JobReq].Name, 1)), (int)ColorType.BrightRed);
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

    public void LeftGame(int index)
    {

    }

    public void OnDeath(int index)
    {
        // Set HP to nothing
        SetPlayerVital(index, Core.Enum.VitalType.HP, 0);

        // Restore vitals
        for (int i = 0, loopTo = (byte)Core.Enum.VitalType.Count; i < loopTo; i++)
            SetPlayerVital(index, (Core.Enum.VitalType)i, GetPlayerMaxVital(index, (Core.Enum.VitalType)i));

        // If the player the attacker killed was a pk then take it away
        if (GetPlayerPK(index))
        {
            SetPlayerPK(index, false);
        }

        ref var withBlock = ref Core.Type.Map[GetPlayerMap(index)];

        // Warp player away
        SetPlayerDir(index, (byte)Core.Enum.DirectionType.Down);

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

    public void TrainStat(int index, int tmpStat)
    {
        // make sure there stats are not maxed
        if (GetPlayerRawStat(index, (StatType)tmpStat) >= Core.Constant.MAX_STATS)
        {
            NetworkSend.PlayerMsg(index, "You cannot spend any more points on that stat.", (int)ColorType.BrightRed);
            return;
        }

        // increment stat
        SetPlayerStat(index, (StatType)tmpStat, GetPlayerRawStat(index, (StatType)tmpStat) + 1);

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

                        if (entity.Type == Core.Globals.Entity.EntityType.NPC)
                        {
                            if (targetVerify)
                            {
                                if (!Event.IsOneBlockAway(targetX, targetY, (int)entity.X, (int)entity.Y))
                                {
                                    int i = EventLogic.FindNPCPath(mapNum, Core.Globals.Entity.Index(entity), targetX, targetY);
                                    if (i < 4)
                                    {
                                        if (Server.NPC.CanNPCMove(mapNum, Core.Globals.Entity.Index(entity), (byte)i))
                                        {
                                            Server.NPC.NPCMove(mapNum, Core.Globals.Entity.Index(entity), i, (int)MovementType.Walking);
                                        }
                                    }
                                    else
                                    {
                                        i = (int)Math.Round(new Random().NextDouble() * 3) + 1;
                                        if (i == 1)
                                        {
                                            i = (int)Math.Round(new Random().NextDouble() * 3) + 1;
                                            if (Server.NPC.CanNPCMove(mapNum, Core.Globals.Entity.Index(entity), (byte)i))
                                            {
                                                Server.NPC.NPCMove(mapNum, Core.Globals.Entity.Index(entity), i, (int)MovementType.Walking);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Server.NPC.NPCDir(mapNum, Core.Globals.Entity.Index(entity), Event.GetNPCDir(targetX, targetY, (int)entity.X, (int)entity.Y));
                                }
                            }
                            else
                            {
                                int i = (int)Math.Round(new Random().NextDouble() * 4);
                                if (i == 1)
                                {
                                    i = (int)Math.Round(new Random().NextDouble() * 4);
                                    if (Server.NPC.CanNPCMove(mapNum, Core.Globals.Entity.Index(entity), (byte)i))
                                    {
                                        Server.NPC.NPCMove(mapNum, Core.Globals.Entity.Index(entity), i, (int)MovementType.Walking);
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
                                    Server.NPC.SpawnNPC(x, mapNum);
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
        }
    }
}