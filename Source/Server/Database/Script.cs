using System;
using Core;
using Server;
using Microsoft.VisualBasic;
using static Server.NetworkSend;
using static Core.Global.Command;
using static Core.Enum;
using static Core.Packets;
using static Core.Type;
using static Server.Animation;
using static Server.Player;
using static Server.NPC;
using static Server.Party;
using static Server.Event;
using static Server.Pet;
using static Server.Projectile;
using static Server.Resource;
using static Server.Item;
using static Server.Moral;

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

    public int KillPlayer(int index)
    {
        // culate exp to give attacker
        int exp = GetPlayerExp(index) / 3;

        // Make sure we dont get less then 0
        if (exp < 0)
            exp = 0;

        SetPlayerExp(index, GetPlayerExp(index) - exp);

        OnDeath(index);

        return exp;
    }

    public int GetPlayerVitalRegen(int index, Core.Enum.VitalType vital)
    {
        int GetPlayerVitalRegenRet = default;
        var i = default(int);

        // Prevent subscript out of range
        if (NetworkConfig.IsPlaying(index) == false | index < 0 | index >= Core.Constant.MAX_PLAYERS)
        {
            GetPlayerVitalRegenRet = 0;
            return GetPlayerVitalRegenRet;
        }

        switch (vital)
        {
            case Core.Enum.VitalType.HP:
                {
                    i = GetPlayerStat(index, Core.Enum.StatType.Vitality) / 2;
                    break;
                }
            case Core.Enum.VitalType.SP:
                {
                    i = GetPlayerStat(index, Core.Enum.StatType.Spirit) / 2;
                    break;
                }
        }

        if (i < 2)
            i = 2;

        GetPlayerVitalRegenRet = i;
        return GetPlayerVitalRegenRet;
    }
    
    public int GetNPCVitalRegen(int NPCNum, Core.Enum.VitalType vital)
    {
        int GetNPCVitalRegenRet = default;
        int i;

        GetNPCVitalRegenRet = 0;

        // Prevent subscript out of range
        if (NPCNum < 0 | NPCNum > Core.Constant.MAX_NPCS)
        {
            return GetNPCVitalRegenRet;
        }

        switch (vital)
        {
            case var @case when @case == Core.Enum.VitalType.HP:
                {
                    i = (int)Core.Type.NPC[(int)NPCNum].Stat[(int)Core.Enum.StatType.Vitality] / 3;

                    if (i < 0)
                        i = 0;
                    GetNPCVitalRegenRet = i;
                    break;
                }
            case var case1 when case1 == Core.Enum.VitalType.SP:
                {
                    i = (int)Core.Type.NPC[(int)NPCNum].Stat[(int)Core.Enum.StatType.Intelligence] / 3;

                    if (i < 0)
                        i = 0;
                    GetNPCVitalRegenRet = i;
                    break;
                }
        }

        return GetNPCVitalRegenRet;
    }

    public bool CanPlayerBlockHit(int index)
    {
        bool CanPlayerBlockHitRet = default;
        int i;
        int n;
        double ShieldSlot;
        ShieldSlot = GetPlayerEquipment(index, Core.Enum.EquipmentType.Shield);

        CanPlayerBlockHitRet = false;

        if (ShieldSlot >= 0)
        {
            n = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 2f));

            if (n == 1)
            {
                i = GetPlayerStat(index, Core.Enum.StatType.Luck) / 2 + GetPlayerLevel(index) / 2;
                n = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 100f) + 1f);

                if (n <= i)
                {
                    CanPlayerBlockHitRet = true;
                }
            }
        }

        return CanPlayerBlockHitRet;
    }

    public bool CanPlayerCriticalHitRet(int index)
    {
        bool CanPlayerCriticalHitRet = false;

        int i;
        int n;

        if (GetPlayerEquipment(index, Core.Enum.EquipmentType.Weapon) >= 0)
        {
            n = (int)Math.Round(VBMath.Rnd() * 2f);

            if (n == 1)
            {
                i = GetPlayerStat(index, Core.Enum.StatType.Strength) / 2 + GetPlayerLevel(index) / 2;
                n = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 100f) + 1f);

                if (n <= i)
                {
                    CanPlayerCriticalHitRet = true;
                }
            }
        }
    
        return CanPlayerCriticalHitRet;
    }

    public int GetPlayerDamage(int index)
    {
        int GetPlayerDamageRet = default;
        int weaponNum;

        GetPlayerDamageRet = 0;

        // Check for subscript out of range
        if (NetworkConfig.IsPlaying(index) == false | index < 0 | index >= Core.Constant.MAX_PLAYERS)
        {
            return GetPlayerDamageRet;
        }

        if (GetPlayerEquipment(index, Core.Enum.EquipmentType.Weapon) >= 0)
        {
            weaponNum = GetPlayerEquipment(index, Core.Enum.EquipmentType.Weapon);
            GetPlayerDamageRet = (int)(GetPlayerStat(index, Core.Enum.StatType.Strength) * 2 + Core.Type.Item[weaponNum].Data2 * 2 + GetPlayerLevel(index) * 3 + General.GetRandom.NextDouble(0d, 20d));
        }
        else
        {
            GetPlayerDamageRet = (int)(GetPlayerStat(index, Core.Enum.StatType.Strength) * 2 + GetPlayerLevel(index) * 3 + General.GetRandom.NextDouble(0d, 20d));
        }

        return GetPlayerDamageRet;
    }

    public int GetPlayerProtection(int index)
    {
        int GetPlayerProtectionRet = default;
        double Armor;
        double Helm;
        double Shield;
        GetPlayerProtectionRet = 0;

        // Check for subscript out of range
        if (NetworkConfig.IsPlaying(index) == false | index < 0 | index >= Core.Constant.MAX_PLAYERS)
        {
            return GetPlayerProtectionRet;
        }

        Armor = GetPlayerEquipment(index, Core.Enum.EquipmentType.Armor);
        Helm = GetPlayerEquipment(index, Core.Enum.EquipmentType.Helmet);
        Shield = GetPlayerEquipment(index, Core.Enum.EquipmentType.Shield);

        if (Armor >= 0)
        {
            GetPlayerProtectionRet += Core.Type.Item[(int)Armor].Data2;
        }

        if (Helm >= 0)
        {
            GetPlayerProtectionRet += Core.Type.Item[(int)Helm].Data2;
        }

        if (Shield >= 0)
        {
            GetPlayerProtectionRet += Core.Type.Item[(int)Shield].Data2;
        }

        GetPlayerProtectionRet = (int)Math.Round(GetPlayerProtectionRet / 6d);
        GetPlayerProtectionRet += GetPlayerStat(index, Core.Enum.StatType.Luck) / 5;
        return GetPlayerProtectionRet;
    }

    public bool CanPlayerAttackPlayer(int attacker, int victim, bool IsSkill)
    {
        bool CanPlayerAttackPlayerRet = default;

        if (!IsSkill)
        {
            // Check attack timer
            if (GetPlayerEquipment(attacker, Core.Enum.EquipmentType.Weapon) >= 0)
            {
                if (General.GetTimeMs() < Core.Type.TempPlayer[attacker].AttackTimer + Core.Type.Item[GetPlayerEquipment(attacker, Core.Enum.EquipmentType.Weapon)].Speed)
                    return CanPlayerAttackPlayerRet;
            }
            else if (General.GetTimeMs() < Core.Type.TempPlayer[attacker].AttackTimer + 1000)
                return CanPlayerAttackPlayerRet;
        }

        // Check for subscript out of range
        if (!NetworkConfig.IsPlaying(victim))
            return CanPlayerAttackPlayerRet;

        // Make sure they are on the same map
        if (!(GetPlayerMap(attacker) == GetPlayerMap(victim)))
            return CanPlayerAttackPlayerRet;

        // Make sure we dont attack the player if they are switching maps
        if (Core.Type.TempPlayer[victim].GettingMap == true)
            return CanPlayerAttackPlayerRet;

        if (!IsSkill)
        {
            // Check if at same coordinates
            switch (GetPlayerDir(attacker))
            {
                case (int)Core.Enum.DirectionType.Up:
                    {
                        if (!(GetPlayerY(victim) + 1 == GetPlayerY(attacker) & GetPlayerX(victim) == GetPlayerX(attacker)))
                            return CanPlayerAttackPlayerRet;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Down:
                    {
                        if (!(GetPlayerY(victim) - 1 == GetPlayerY(attacker) & GetPlayerX(victim) == GetPlayerX(attacker)))
                            return CanPlayerAttackPlayerRet;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Left:
                    {
                        if (!(GetPlayerY(victim) == GetPlayerY(attacker) & GetPlayerX(victim) + 1 == GetPlayerX(attacker)))
                            return CanPlayerAttackPlayerRet;
                        break;
                    }
                case (int)Core.Enum.DirectionType.Right:
                    {
                        if (!(GetPlayerY(victim) == GetPlayerY(attacker) & GetPlayerX(victim) - 1 == GetPlayerX(attacker)))
                            return CanPlayerAttackPlayerRet;
                        break;
                    }
                default:
                    {
                        return CanPlayerAttackPlayerRet;
                    }
            }
        }

        // CheckIf Type.Map is attackable
        if ((int)Core.Type.Map[GetPlayerMap(attacker)].Moral >= 0)
        {
            if (!Core.Type.Moral[Core.Type.Map[GetPlayerMap(attacker)].Moral].CanPK)
            {
                if (GetPlayerPK(victim) == false)
                {
                    NetworkSend.PlayerMsg(attacker, "This is a safe zone!", (int)Core.Enum.ColorType.BrightRed);
                    return CanPlayerAttackPlayerRet;
                }
            }
        }

        // Make sure they have more then 0 hp
        if (GetPlayerVital(victim, Core.Enum.VitalType.HP) < 0)
            return CanPlayerAttackPlayerRet;

        // Check to make sure that they dont have access
        if (GetPlayerAccess(attacker) > (int)Core.Enum.AccessType.Moderator)
        {
            NetworkSend.PlayerMsg(attacker, "You cannot attack any player for thou art an admin!", (int)Core.Enum.ColorType.BrightRed);
            return CanPlayerAttackPlayerRet;
        }

        // Check to make sure the victim isn't an admin
        if (GetPlayerAccess(victim) > (int)Core.Enum.AccessType.Moderator)
        {
            NetworkSend.PlayerMsg(attacker, "You cannot attack " + GetPlayerName(victim) + "!", (int)Core.Enum.ColorType.BrightRed);
            return CanPlayerAttackPlayerRet;
        }

        // Make sure attacker is high enough level
        if (GetPlayerLevel(attacker) < 10)
        {
            NetworkSend.PlayerMsg(attacker, "You are below level 10, you cannot attack another player yet!", (int)Core.Enum.ColorType.BrightRed);
            return CanPlayerAttackPlayerRet;
        }

        // Make sure victim is high enough level
        if (GetPlayerLevel(victim) < 10)
        {
            NetworkSend.PlayerMsg(attacker, GetPlayerName(victim) + " is below level 10, you cannot attack this player yet!", (int)Core.Enum.ColorType.BrightRed);
            return CanPlayerAttackPlayerRet;
        }

        CanPlayerAttackPlayerRet = true;
        return CanPlayerAttackPlayerRet;
    }

    public void AttackPlayer(int attacker, int victim, int damage, int skillNum, int NPCNum)
    {
        int exp;
        int mapNum;
        int n;

        if (NPCNum == -1)
        {
            // Check for subscript out of range
            if (NetworkConfig.IsPlaying(attacker) == false | NetworkConfig.IsPlaying(victim) == false | damage < 0)
            {
                return;
            }

            // Check for weapon
            if (GetPlayerEquipment(attacker, Core.Enum.EquipmentType.Weapon) >= 0)
            {
                n = GetPlayerEquipment(attacker, Core.Enum.EquipmentType.Weapon);
            }

            NetworkSend.SendPlayerAttack(attacker);  

            if (damage >= GetPlayerVital(victim, (Core.Enum.VitalType)Core.Enum.VitalType.HP))
            {
                NetworkSend.SendActionMsg(GetPlayerMap(victim), "-" + damage, (int)Core.Enum.ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);

                // Player is dead
                NetworkSend.GlobalMsg(GetPlayerName(victim) + " has been killed by " + GetPlayerName(attacker));

                if ((int)Core.Type.Map[GetPlayerMap(victim)].Moral >= 0)
                {
                    if (Core.Type.Moral[Core.Type.Map[GetPlayerMap(victim)].Moral].LoseExp)
                    {
                        // Calculate exp to give attacker
                        exp = (int)Math.Round(GetPlayerExp(victim) / 3.0);

                        // Make sure we dont get less then 0
                        if (exp < 0)
                        {
                            exp = 0;
                        }

                        if (exp == 0)
                        {
                            NetworkSend.PlayerMsg(victim, "You lost no experience.", (int)Core.Enum.ColorType.BrightGreen);
                            NetworkSend.PlayerMsg(attacker, "You received no experience.", (int)Core.Enum.ColorType.BrightRed);
                        }
                        else
                        {
                            SetPlayerExp(victim, GetPlayerExp(victim) - exp);
                            NetworkSend.SendExp(victim);
                            NetworkSend.PlayerMsg(victim, "You lost " + exp + " experience.", (int)Core.Enum.ColorType.BrightRed);
                            SetPlayerExp(attacker, GetPlayerExp(attacker) + exp);
                            NetworkSend.SendExp(attacker);
                            NetworkSend.PlayerMsg(attacker, "You received " + exp + " experience.", (int)Core.Enum.ColorType.BrightGreen);
                        }

                        // Check for a level up
                        CheckPlayerLevelUp(attacker);
                    }
                }

                // Check if target is player who died and if so set target to 0
                if (Core.Type.TempPlayer[attacker].TargetType == (byte)Core.Enum.TargetType.Player)
                {
                    if (Core.Type.TempPlayer[attacker].Target == victim)
                    {
                        Core.Type.TempPlayer[attacker].Target = 0;
                        Core.Type.TempPlayer[attacker].TargetType = 0;
                    }
                }

                if (GetPlayerPK(victim) == false)
                {
                    if (GetPlayerPK(attacker) == false)
                    {
                        SetPlayerPK(attacker, true);
                        NetworkSend.SendPlayerData(attacker);
                        NetworkSend.GlobalMsg(GetPlayerName(attacker) + " has been deemed a Player Killer!");
                    }
                }
                else
                {
                    NetworkSend.GlobalMsg(GetPlayerName(victim) + " has paid the price for being a Player Killer!");
                }

                OnDeath(victim);
            }
            else
            {
                // Player not dead, just do the damage
                SetPlayerVital(victim, (Core.Enum.VitalType)Core.Enum.VitalType.HP, GetPlayerVital(victim, (Core.Enum.VitalType)Core.Enum.VitalType.HP) - damage);
                NetworkSend.SendVital(victim, (Core.Enum.VitalType)Core.Enum.VitalType.HP);
                NetworkSend.SendActionMsg(GetPlayerMap(victim), "-" + damage,(int)Core.Enum.ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);

                // if a stunning skill, stun the player
                if (skillNum >= 0)
                {
                    if (Core.Type.Skill[skillNum].StunDuration > 0)
                        StunPlayer(victim, skillNum);
                }
            }

            // Reset attack timer
            Core.Type.TempPlayer[attacker].AttackTimer = General.GetTimeMs();
        }
        else // npc to player
        {
            // Check for subscript out of range
            if (NetworkConfig.IsPlaying(victim) == false | damage < 0)
                return;

            mapNum = GetPlayerMap(victim);

            NetworkSend.SendNPCAttack(mapNum, victim);

            if (damage >= GetPlayerVital(victim, (Core.Enum.VitalType)Core.Enum.VitalType.HP))
            {

                NetworkSend.SendActionMsg(mapNum, "-" + damage,(int)Core.Enum.ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);

                // Player is dead
                NetworkSend.GlobalMsg(GetPlayerName(victim) + " has been killed by " + Core.Type.NPC[(int)Core.Type.MapNPC[mapNum].NPC[attacker].Num].Name);

                // Check if target is player who died and if so set target to 0
                if (Core.Type.TempPlayer[attacker].TargetType == (byte)Core.Enum.TargetType.Player)
                {
                    if (Core.Type.TempPlayer[attacker].Target == victim)
                    {
                        Core.Type.TempPlayer[attacker].Target = 0;
                        Core.Type.TempPlayer[attacker].TargetType = 0;
                    }
                }

                OnDeath(victim);
            }
            else
            {
                // Player not dead, just do the damage
                SetPlayerVital(victim, (Core.Enum.VitalType)Core.Enum.VitalType.HP, GetPlayerVital(victim, (Core.Enum.VitalType)Core.Enum.VitalType.HP) - damage);
                NetworkSend.SendVital(victim, (Core.Enum.VitalType)Core.Enum.VitalType.HP);
                NetworkSend.SendActionMsg(mapNum, "-" + damage,(int)Core.Enum.ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);

                // if a stunning skill, stun the player
                if (skillNum >= 0)
                {
                    if (Core.Type.Skill[skillNum].StunDuration > 0)
                        StunPlayer(victim, skillNum);
                }
            }

            // Reset attack timer
            Core.Type.MapNPC[mapNum].NPC[attacker].AttackTimer = General.GetTimeMs();
        }
    }

    public void StunPlayer(int index, int skillNum)
    {
        // check if it's a stunning skill
        if (Core.Type.Skill[skillNum].StunDuration > 0)
        {
            // set the values on index
            Core.Type.TempPlayer[index].StunDuration = Core.Type.Skill[skillNum].StunDuration;
            Core.Type.TempPlayer[index].StunTimer = General.GetTimeMs();

            // send it to the index
            NetworkSend.SendStunned(index);

            // tell him he's stunned
            NetworkSend.PlayerMsg(index, "You have been stunned!", (int)Core.Enum.ColorType.Yellow);
        }
    }

    public void StunNPC(int index, int mapNum, int skillNum)
    {
        // check if it's a stunning skill
        if (Core.Type.Skill[skillNum].StunDuration > 0)
        {
            // set the values on index
            Core.Type.MapNPC[mapNum].NPC[index].StunDuration = Core.Type.Skill[skillNum].StunDuration;
            Core.Type.MapNPC[mapNum].NPC[index].StunTimer = General.GetTimeMs();
        }
    }

    public void PlayerAttackNPC(int attacker, int MapNPCNum, int Damage)
    {
        // Check for subscript out of range
        if (NetworkConfig.IsPlaying(attacker) == false | MapNPCNum < 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS | Damage < 0)
            return;

        var mapNum = GetPlayerMap(attacker);
        var npcId = Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Num;
        string Name = Core.Type.NPC[(int)npcId].Name;

        // Check for weapon
        int Weapon = 0;
        if (GetPlayerEquipment(attacker, EquipmentType.Weapon) >= 0)
        {
            Weapon = GetPlayerEquipment(attacker, EquipmentType.Weapon);
        }

        // Deal damage to our NPC.
        Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Vital[(int)VitalType.HP] = Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Vital[(int)VitalType.HP] - Damage;

        // Set the NPC target to the player so they can come after them.
        Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].TargetType = (int)TargetType.Player;
        Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Target = attacker;

        // Check for any mobs on the map with the Guard behaviour so they can come after our player.
        if (Core.Type.NPC[(int)Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Num].Behaviour == (byte)NPCBehavior.Guard)
        {
            // Set the target for each guard NPC
            for (int i = 0; i < Core.Type.MapNPC[mapNum].NPC.Length; i++)
            {
                if (Core.Type.MapNPC[mapNum].NPC[i].Num == npcId)
                {
                    Core.Type.MapNPC[mapNum].NPC[i].Target = attacker;
                    Core.Type.MapNPC[mapNum].NPC[i].TargetType = (byte)TargetType.Player;
                }
            }
        }

        // Send our general visual stuff.
        NetworkSend.SendActionMsg(mapNum, "-" + Damage, (int)ColorType.BrightRed, 1, Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X * 32, Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y * 32);
        NetworkSend.SendBlood(GetPlayerMap(attacker), Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X, Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y);
        NetworkSend.SendPlayerAttack(attacker);

        if (Weapon >= 0)
        {
            if (GetPlayerEquipment(attacker, EquipmentType.Weapon) >= 0)
            {
                Server.Animation.SendAnimation(mapNum, Core.Type.Item[GetPlayerEquipment(attacker, EquipmentType.Weapon)].Animation, 0, 0, (byte)TargetType.NPC, (int)MapNPCNum);
            }
        }

        // Reset our attack timer.
        Core.Type.TempPlayer[attacker].AttackTimer = General.GetTimeMs();

        if (!Server.NPC.IsNPCDead(mapNum, MapNPCNum))
        {
            // Check if our NPC has something to share with our player.
            if (Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].TargetType == 0)
            {
                if ((Core.Type.NPC[(int)npcId].AttackSay.Length) > 0)
                {
                    NetworkSend.PlayerMsg(attacker, string.Format("{0} says: '{1}'", Core.Type.NPC[(int)npcId].Name, Core.Type.NPC[(int)npcId].AttackSay), (int)ColorType.Yellow);
                }
            }

            Server.NPC.SendMapNPCTo(mapNum, MapNPCNum);
        }
        else
        {
            HandlePlayerKillNPC(mapNum, attacker, MapNPCNum);
        }
    }

    public bool CanPlayerAttackNPC(int attacker, int MapNPCNum, bool IsSkill)
    {
        int mapNum;
        int NPCNum;
        var atkX = default(int);
        var atkY = default(int);
        int attackSpeed;
        bool CanPlayerAttackNPCRet = false;

        // Check for subscript out of range
        if (NetworkConfig.IsPlaying(attacker) == false | MapNPCNum < 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS)
        {
            return CanPlayerAttackNPCRet;
        }

        // Check for subscript out of range
        if (Core.Type.MapNPC[GetPlayerMap(attacker)].NPC[(int)MapNPCNum].Num < 0)
        {
            return CanPlayerAttackNPCRet;
        }

        mapNum = GetPlayerMap(attacker);
        NPCNum = Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Num;

        if (NPCNum < 0 | NPCNum > Core.Constant.MAX_NPCS)
        {
            return CanPlayerAttackNPCRet;
        }

        // Make sure the npc isn't already dead
        if (Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Vital[(int)VitalType.HP] < 0)
        {
            return CanPlayerAttackNPCRet;
        }

        // Make sure they are on the same map

        // attack speed from weapon
        if (GetPlayerEquipment(attacker, EquipmentType.Weapon) >= 0)
        {
            attackSpeed = Core.Type.Item[GetPlayerEquipment(attacker, EquipmentType.Weapon)].Speed;
        }
        else
        {
            attackSpeed = 1000;
        }

        if (NPCNum >= 0 & General.GetTimeMs() > Core.Type.TempPlayer[attacker].AttackTimer + attackSpeed)
        {
            // exit out early
            if (IsSkill)
            {
                if (Core.Type.NPC[(int)NPCNum].Behaviour != (byte)NPCBehavior.Friendly & Core.Type.NPC[(int)NPCNum].Behaviour != (byte)NPCBehavior.ShopKeeper)
                {
                    CanPlayerAttackNPCRet = true;
                    return CanPlayerAttackNPCRet;
                }
            }

            // Check if at same coordinates
            switch (GetPlayerDir(attacker))
            {
                case (byte)DirectionType.Up:
                    {
                        atkX = GetPlayerX(attacker);
                        atkY = GetPlayerY(attacker) - 1;
                        break;
                    }
                case (byte)DirectionType.Down:
                    {
                        atkX = GetPlayerX(attacker);
                        atkY = GetPlayerY(attacker) + 1;
                        break;
                    }
                case (byte)DirectionType.Left:
                    {
                        atkX = GetPlayerX(attacker) - 1;
                        atkY = GetPlayerY(attacker);
                        break;
                    }
                case (byte)DirectionType.Right:
                    {
                        atkX = GetPlayerX(attacker) + 1;
                        atkY = GetPlayerY(attacker);
                        break;
                    }
            }

            if (atkX == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X)
            {
                if (atkY == Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y)
                {
                    if (Core.Type.NPC[(int)NPCNum].Behaviour != (byte)NPCBehavior.Friendly & Core.Type.NPC[(int)NPCNum].Behaviour != (byte)NPCBehavior.ShopKeeper & Core.Type.NPC[(int)NPCNum].Behaviour != (byte)NPCBehavior.Quest)
                    {
                        CanPlayerAttackNPCRet = true;
                    }
                    else if (Strings.Len(Core.Type.NPC[(int)NPCNum].AttackSay) > 0)
                    {
                        NetworkSend.PlayerMsg(attacker, Core.Type.NPC[(int)NPCNum].Name + ": " + Core.Type.NPC[(int)NPCNum].AttackSay, (int)ColorType.Yellow);
                    }
                }
            }
        }

        return CanPlayerAttackNPCRet;
    }
}