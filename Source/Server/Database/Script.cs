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
        
    }
}