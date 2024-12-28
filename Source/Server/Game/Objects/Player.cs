using System;
using System.Linq;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Enum;
using static Core.Type;
using static Core.Global.Command;
using static Core.Packets;

namespace Server
{

    static class Player
    {

        public static bool CanPlayerAttackPlayer(int attacker, int victim, bool IsSkill = false)
        {
            bool CanPlayerAttackPlayerRet = default;

            if (!IsSkill)
            {
                // Check attack timer
                if (GetPlayerEquipment(attacker, EquipmentType.Weapon) > 0)
                {
                    if (General.GetTimeMs() < Core.Type.TempPlayer[attacker].AttackTimer + Core.Type.Item[GetPlayerEquipment(attacker, EquipmentType.Weapon)].Speed)
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
                    case (int)DirectionType.Up:
                        {
                            if (!(GetPlayerY(victim) + 1 == GetPlayerY(attacker) & GetPlayerX(victim) == GetPlayerX(attacker)))
                                return CanPlayerAttackPlayerRet;
                            break;
                        }
                    case (int)DirectionType.Down:
                        {
                            if (!(GetPlayerY(victim) - 1 == GetPlayerY(attacker) & GetPlayerX(victim) == GetPlayerX(attacker)))
                                return CanPlayerAttackPlayerRet;
                            break;
                        }
                    case (int)DirectionType.Left:
                        {
                            if (!(GetPlayerY(victim) == GetPlayerY(attacker) & GetPlayerX(victim) + 1 == GetPlayerX(attacker)))
                                return CanPlayerAttackPlayerRet;
                            break;
                        }
                    case (int)DirectionType.Right:
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
            if ((int)Core.Type.Map[GetPlayerMap(attacker)].Moral > 0)
            {
                if (!Core.Type.Moral[Core.Type.Map[GetPlayerMap(attacker)].Moral].CanPK)
                {
                    if (GetPlayerPK(victim) == 0)
                    {
                        NetworkSend.PlayerMsg(attacker, "This is a safe zone!", (int)(int) ColorType.BrightRed);
                        return CanPlayerAttackPlayerRet;
                    }
                }
            }

            // Make sure they have more then 0 hp
            if (GetPlayerVital(victim, VitalType.HP) <= 0)
                return CanPlayerAttackPlayerRet;

            // Check to make sure that they dont have access
            if ((int)GetPlayerAccess(attacker) > (int)AccessType.Moderator)
            {
                NetworkSend.PlayerMsg(attacker, "You cannot attack any player for thou art an admin!", (int)(int) ColorType.BrightRed);
                return CanPlayerAttackPlayerRet;
            }

            // Check to make sure the victim isn't an admin
            if ((int)GetPlayerAccess(victim) > (int)AccessType.Moderator)
            {
                NetworkSend.PlayerMsg(attacker, "You cannot attack " + GetPlayerName(victim) + "!", (int)(int) ColorType.BrightRed);
                return CanPlayerAttackPlayerRet;
            }

            // Make sure attacker is high enough level
            if (GetPlayerLevel(attacker) < 10)
            {
                NetworkSend.PlayerMsg(attacker, "You are below level 10, you cannot attack another player yet!", (int)(int) ColorType.BrightRed);
                return CanPlayerAttackPlayerRet;
            }

            // Make sure victim is high enough level
            if (GetPlayerLevel(victim) < 10)
            {
                NetworkSend.PlayerMsg(attacker, GetPlayerName(victim) + " is below level 10, you cannot attack this player yet!", (int)(int) ColorType.BrightRed);
                return CanPlayerAttackPlayerRet;
            }

            CanPlayerAttackPlayerRet = Conversions.ToBoolean(1);
            return CanPlayerAttackPlayerRet;
        }

        public static bool CanPlayerBlockHit(int index)
        {
            bool CanPlayerBlockHitRet = default;
            int i;
            int n;
            int ShieldSlot;
            ShieldSlot = GetPlayerEquipment(index, EquipmentType.Shield);

            CanPlayerBlockHitRet = Conversions.ToBoolean(0);

            if (ShieldSlot > 0)
            {
                n = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 2f));

                if (n == 1)
                {
                    i = GetPlayerStat(index, StatType.Luck) / 2 + GetPlayerLevel(index) / 2;
                    n = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 100f) + 1f);

                    if (n <= i)
                    {
                        CanPlayerBlockHitRet = Conversions.ToBoolean(1);
                    }
                }
            }

            return CanPlayerBlockHitRet;

        }

        public static bool CanPlayerCriticalHit(int index)
        {
            bool CanPlayerCriticalHitRet = false;

            int i;
            int n;

            if (GetPlayerEquipment(index, EquipmentType.Weapon) > 0)
            {
                n = (int)Math.Round(VBMath.Rnd() * 2f);

                if (n == 1)
                {
                    i = GetPlayerStat(index, StatType.Strength) / 2 + GetPlayerLevel(index) / 2;
                    n = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 100f) + 1f);

                    if (n <= i)
                    {
                        CanPlayerCriticalHitRet = true;
                    }
                }
            }
        
            return CanPlayerCriticalHitRet;
        }

        public static int GetPlayerDamage(int index)
        {
            int GetPlayerDamageRet = default;
            int weaponNum;

            GetPlayerDamageRet = 0;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | index < 0 | index > Core.Constant.MAX_PLAYERS)
            {
                return GetPlayerDamageRet;
            }

            if (GetPlayerEquipment(index, EquipmentType.Weapon) > 0)
            {
                weaponNum = GetPlayerEquipment(index, EquipmentType.Weapon);
                GetPlayerDamageRet = (int)(GetPlayerStat(index, StatType.Strength) * 2 + Core.Type.Item[weaponNum].Data2 * 2 + GetPlayerLevel(index) * 3 + General.Random.NextDouble(0d, 20d));
            }
            else
            {
                GetPlayerDamageRet = (int)(GetPlayerStat(index, StatType.Strength) * 2 + GetPlayerLevel(index) * 3 + General.Random.NextDouble(0d, 20d));
            }

            return GetPlayerDamageRet;

        }

        public static int GetPlayerProtection(int index)
        {
            int GetPlayerProtectionRet = default;
            int Armor;
            int Helm;
            int Shield;
            GetPlayerProtectionRet = 0;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | index < 0 | index > Core.Constant.MAX_PLAYERS)
            {
                return GetPlayerProtectionRet;
            }

            Armor = GetPlayerEquipment(index, EquipmentType.Armor);
            Helm = GetPlayerEquipment(index, EquipmentType.Helmet);
            Shield = GetPlayerEquipment(index, EquipmentType.Shield);

            if (Armor > 0)
            {
                GetPlayerProtectionRet += Core.Type.Item[Armor].Data2;
            }

            if (Helm > 0)
            {
                GetPlayerProtectionRet += Core.Type.Item[Helm].Data2;
            }

            if (Shield > 0)
            {
                GetPlayerProtectionRet += Core.Type.Item[Shield].Data2;
            }

            GetPlayerProtectionRet = (int)Math.Round(GetPlayerProtectionRet / 6d);
            GetPlayerProtectionRet += GetPlayerStat(index, StatType.Luck) / 5;
            return GetPlayerProtectionRet;
        }

        public static void AttackPlayer(int attacker, int victim, int damage, int skillnum = 0, int NPCNum = 0)
        {
            int exp;
            int mapNum;
            int n;
            ByteStream buffer;

            if (NPCNum == 0)
            {
                // Check for subscript out of range
                if (Conversions.ToInteger(NetworkConfig.IsPlaying(attacker)) == 0 | Conversions.ToInteger(NetworkConfig.IsPlaying(victim)) == 0 | damage < 0)
                {
                    return;
                }

                // Check for weapon
                if (GetPlayerEquipment(attacker, EquipmentType.Weapon) > 0)
                {
                    n = GetPlayerEquipment(attacker, EquipmentType.Weapon);
                }

                // Send this packet so they can see the person attacking
                buffer = new ByteStream(4);
                buffer.WriteInt32((byte)ServerPackets.SAttack);
                buffer.WriteInt32(attacker);
                NetworkConfig.SendDataToMapBut(attacker, GetPlayerMap(attacker), ref buffer.Data, buffer.Head);
                buffer.Dispose();

                if (damage >= GetPlayerVital(victim, (VitalType)VitalType.HP))
                {
                    NetworkSend.SendActionMsg(GetPlayerMap(victim), "-" + damage, (int)ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);

                    // Player is dead
                    NetworkSend.GlobalMsg(GetPlayerName(victim) + " has been killed by " + GetPlayerName(attacker));

                    if ((int)Core.Type.Map[GetPlayerMap(victim)].Moral > 0)
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
                                NetworkSend.PlayerMsg(victim, "You lost no exp.", (int)ColorType.BrightGreen);
                                NetworkSend.PlayerMsg(attacker, "You received no exp.", (int)ColorType.BrightRed);
                            }
                            else
                            {
                                SetPlayerExp(victim, GetPlayerExp(victim) - exp);
                                NetworkSend.SendExp(victim);
                                NetworkSend.PlayerMsg(victim, "You lost " + exp + " exp.", (int)ColorType.BrightRed);
                                SetPlayerExp(attacker, GetPlayerExp(attacker) + exp);
                                NetworkSend.SendExp(attacker);
                                NetworkSend.PlayerMsg(attacker, "You received " + exp + " exp.", (int)ColorType.BrightGreen);
                            }

                            // Check for a level up
                            CheckPlayerLevelUp(attacker);
                        }
                    }

                    // Check if target is player who died and if so set target to 0
                    if (Core.Type.TempPlayer[attacker].TargetType == (byte)TargetType.Player)
                    {
                        if (Core.Type.TempPlayer[attacker].Target == victim)
                        {
                            Core.Type.TempPlayer[attacker].Target = 0;
                            Core.Type.TempPlayer[attacker].TargetType = 0;
                        }
                    }

                    if (GetPlayerPK(victim) == 0)
                    {
                        if (GetPlayerPK(attacker) == 0)
                        {
                            SetPlayerPK(attacker, Conversions.ToInteger(true));
                            NetworkSend.SendPlayerData(attacker);
                            NetworkSend.GlobalMsg(GetPlayerName(attacker) + " has been deemed a Player Killer!!!");
                        }
                    }
                    else
                    {
                        NetworkSend.GlobalMsg(GetPlayerName(victim) + " has paid the price for being a Player Killer!!!");
                    }

                    OnDeath(victim);
                }
                else
                {
                    // Player not dead, just do the damage
                    SetPlayerVital(victim, (VitalType)VitalType.HP, GetPlayerVital(victim, (VitalType)VitalType.HP) - damage);
                    NetworkSend.SendVital(victim, (VitalType)VitalType.HP);
                    NetworkSend.SendActionMsg(GetPlayerMap(victim), "-" + damage, (int)ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);

                    // if a stunning skill, stun the player
                    if (skillnum > 0)
                    {
                        if (Core.Type.Skill[skillnum].StunDuration > 0)
                            StunPlayer(victim, skillnum);
                    }
                }

                // Reset attack timer
                Core.Type.TempPlayer[attacker].AttackTimer = General.GetTimeMs();
            }
            else // npc to player
            {
                // Check for subscript out of range
                if (Conversions.ToInteger(NetworkConfig.IsPlaying(victim)) == 0 | damage < 0)
                    return;

                mapNum = GetPlayerMap(victim);

                // Send this packet so they can see the person attacking
                buffer = new ByteStream(4);
                buffer.WriteInt32((byte)ServerPackets.SNPCAttack);
                buffer.WriteInt32(attacker);
                NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);
                buffer.Dispose();

                if (damage >= GetPlayerVital(victim, (VitalType)VitalType.HP))
                {

                    NetworkSend.SendActionMsg(mapNum, "-" + damage, (int)ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);

                    // Player is dead
                    NetworkSend.GlobalMsg(GetPlayerName(victim) + " has been killed by " + Core.Type.NPC[Core.Type.MapNPC[mapNum].NPC[attacker].Num].Name);

                    // Check if target is player who died and if so set target to 0
                    if (Core.Type.TempPlayer[attacker].TargetType == (byte)TargetType.Player)
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
                    SetPlayerVital(victim, (VitalType)VitalType.HP, GetPlayerVital(victim, (VitalType)VitalType.HP) - damage);
                    NetworkSend.SendVital(victim, (VitalType)VitalType.HP);
                    NetworkSend.SendActionMsg(mapNum, "-" + damage, (int)ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);

                    // if a stunning skill, stun the player
                    if (skillnum > 0)
                    {
                        if (Core.Type.Skill[skillnum].StunDuration > 0)
                            StunPlayer(victim, skillnum);
                    }
                }

                // Reset attack timer
                Core.Type.MapNPC[mapNum].NPC[attacker].AttackTimer = General.GetTimeMs();
            }

        }

        internal static void StunPlayer(int index, int skillnum)
        {
            // check if it's a stunning skill
            if (Core.Type.Skill[skillnum].StunDuration > 0)
            {
                // set the values on index
                Core.Type.TempPlayer[index].StunDuration = Core.Type.Skill[skillnum].StunDuration;
                Core.Type.TempPlayer[index].StunTimer = General.GetTimeMs();
                // send it to the index
                NetworkSend.SendStunned(index);
                // tell him he's stunned
                NetworkSend.PlayerMsg(index, "You have been stunned!", (int) ColorType.Yellow);
            }
        }

        public static bool CanPlayerAttackNPC(int attacker, int MapNPCNum, bool IsSkill = false)
        {
            bool CanPlayerAttackNPCRet = default;
            int mapNum;
            int NPCNum;
            var atkX = default(int);
            var atkY = default(int);
            int attackspeed;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(attacker)) == 0 | MapNPCNum <= 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS)
            {
                return CanPlayerAttackNPCRet;
            }

            // Check for subscript out of range
            if (Core.Type.MapNPC[GetPlayerMap(attacker)].NPC[MapNPCNum].Num <= 0)
            {
                return CanPlayerAttackNPCRet;
            }

            mapNum = GetPlayerMap(attacker);
            NPCNum = Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num;

            // Make sure the npc isn't already dead
            if (Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Vital[(int)VitalType.HP] <= 0)
            {
                return CanPlayerAttackNPCRet;
            }

            // Make sure they are on the same map

            // attack speed from weapon
            if (GetPlayerEquipment(attacker, EquipmentType.Weapon) > 0)
            {
                attackspeed = Core.Type.Item[GetPlayerEquipment(attacker, EquipmentType.Weapon)].Speed;
            }
            else
            {
                attackspeed = 1000;
            }

            if (NPCNum > 0 & General.GetTimeMs() > Core.Type.TempPlayer[attacker].AttackTimer + attackspeed)
            {
                // exit out early
                if (IsSkill)
                {
                    if (Core.Type.NPC[NPCNum].Behaviour != (byte)  NPCBehavior.Friendly & Core.Type.NPC[NPCNum].Behaviour != (byte)NPCBehavior.ShopKeeper)
                    {
                        CanPlayerAttackNPCRet = Conversions.ToBoolean(1);
                        return CanPlayerAttackNPCRet;
                    }
                }

                // Check if at same coordinates
                switch (GetPlayerDir(attacker))
                {
                    case var @case when @case == (byte) DirectionType.Up:
                        {
                            atkX = GetPlayerX(attacker);
                            atkY = GetPlayerY(attacker) - 1;
                            break;
                        }
                    case var case1 when case1 == (byte) DirectionType.Down:
                        {
                            atkX = GetPlayerX(attacker);
                            atkY = GetPlayerY(attacker) + 1;
                            break;
                        }
                    case var case2 when case2 == (byte) DirectionType.Left:
                        {
                            atkX = GetPlayerX(attacker) - 1;
                            atkY = GetPlayerY(attacker);
                            break;
                        }
                    case var case3 when case3 == (byte) DirectionType.Right:
                        {
                            atkX = GetPlayerX(attacker) + 1;
                            atkY = GetPlayerY(attacker);
                            break;
                        }
                }

                if (atkX == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X)
                {
                    if (atkY == Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y)
                    {
                        if (Core.Type.NPC[NPCNum].Behaviour != (byte)NPCBehavior.Friendly & Core.Type.NPC[NPCNum].Behaviour != (byte)NPCBehavior.ShopKeeper & Core.Type.NPC[NPCNum].Behaviour != (byte)NPCBehavior.Quest)
                        {
                            CanPlayerAttackNPCRet = Conversions.ToBoolean(1);
                        }
                        else if (Strings.Len(Core.Type.NPC[NPCNum].AttackSay) > 0)
                        {
                            NetworkSend.PlayerMsg(attacker, Core.Type.NPC[NPCNum].Name + ": " + Core.Type.NPC[NPCNum].AttackSay, (int) ColorType.Yellow);
                        }
                    }
                }
            }

            return CanPlayerAttackNPCRet;

        }

        internal static void StunNPC(int index, int mapNum, int skillnum)
        {
            // check if it's a stunning skill
            if (Core.Type.Skill[skillnum].StunDuration > 0)
            {
                // set the values on index
                Core.Type.MapNPC[mapNum].NPC[index].StunDuration = Core.Type.Skill[skillnum].StunDuration;
                Core.Type.MapNPC[mapNum].NPC[index].StunTimer = General.GetTimeMs();
            }
        }

        public static void PlayerAttackNPC(int attacker, int MapNPCNum, int Damage)
        {
            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(attacker)) == 0 | MapNPCNum <= 0 | MapNPCNum > Core.Constant.MAX_MAP_NPCS | Damage <= 0)
                return;

            var MapNum = GetPlayerMap(attacker);
            var NPCNum = Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Num;
            string Name = Core.Type.NPC[NPCNum].Name;

            // Check for weapon
            int Weapon = 0;
            if (GetPlayerEquipment(attacker, EquipmentType.Weapon) > 0)
            {
                Weapon = GetPlayerEquipment(attacker, EquipmentType.Weapon);
            }

            // Deal damage to our NPC.
            Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Vital[(int) VitalType.HP] = Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Vital[(int) VitalType.HP] - Damage;

            // Set the NPC target to the player so they can come after them.
            Core.Type.MapNPC[MapNum].NPC[MapNPCNum].TargetType = (int)TargetType.Player;
            Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Target = attacker;

            // Check for any mobs on the map with the Guard behaviour so they can come after our player.
            if (Core.Type.NPC[Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Num].Behaviour == (byte)NPCBehavior.Guard)
            {
                // Find all NPCs with the same ID as the current NPC in the group
                var guards = Core.Type.MapNPC[MapNum].NPC.Where(npc => Operators.ConditionalCompareObjectEqual(npc.Num, Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Num, false)).Select((npc, index) => index);

                // Set the target for each guard NPC
                foreach (var guardindex in guards)
                {
                    Core.Type.MapNPC[MapNum].NPC[guardindex].Target = attacker;
                    Core.Type.MapNPC[MapNum].NPC[guardindex].TargetType = (byte)TargetType.Player;
                }
            }

            // Send our general visual stuff.
            NetworkSend.SendActionMsg(MapNum, "-" + Damage, (int) ColorType.BrightRed, 1, Core.Type.MapNPC[MapNum].NPC[MapNPCNum].X * 32, Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Y * 32);
            NetworkSend.SendBlood(GetPlayerMap(attacker), Core.Type.MapNPC[MapNum].NPC[MapNPCNum].X, Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Y);
            NetworkSend.SendPlayerAttack(attacker);
            if (Weapon > 0)
            {
                Animation.SendAnimation(MapNum, Core.Type.Item[GetPlayerEquipment(attacker, EquipmentType.Weapon)].Animation, 0, 0, (byte)TargetType.NPC, MapNPCNum);
            }

            // Reset our attack timer.
            Core.Type.TempPlayer[attacker].AttackTimer = General.GetTimeMs();

            if (!NPC.IsNPCDead(MapNum, MapNPCNum))
            {
                // Check if our NPC has something to share with our player.
                if (Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Target == 0)
                {
                    if ((Core.Type.NPC[NPCNum].AttackSay.Length) > 0)
                    {
                        NetworkSend.PlayerMsg(attacker, string.Format("{0} says: '{1}'", Core.Type.NPC[NPCNum].Name, Core.Type.NPC[NPCNum].AttackSay), (int) ColorType.Yellow);
                    }
                }

                NPC.SendMapNPCTo(MapNum, MapNPCNum);
            }
            else
            {
                HandlePlayerKillNPC(MapNum, attacker, MapNPCNum);
            }
        }

        public static bool IsInRange(int range, int x1, int y1, int x2, int y2)
        {
            bool IsInRangeRet = default;
            int nVal;
            IsInRangeRet = Conversions.ToBoolean(0);
            nVal = (int)Math.Round(Math.Sqrt(Math.Pow(x1 - x2, 2d) + Math.Pow(y1 - y2, 2d)));
            if (nVal <= range)
                IsInRangeRet = Conversions.ToBoolean(1);
            return IsInRangeRet;
        }

        internal static bool CanPlayerDodge(int index)
        {
            bool CanPlayerDodgeRet = default;
            int rate;
            int rndNum;

            CanPlayerDodgeRet = Conversions.ToBoolean(0);

            rate = GetPlayerStat(index, StatType.Luck) / 4;
            rndNum = (int)Math.Round(General.Random.NextDouble(1d, 100d));

            if (rndNum <= rate)
            {
                CanPlayerDodgeRet = Conversions.ToBoolean(1);
            }

            return CanPlayerDodgeRet;

        }

        internal static bool CanPlayerParry(int index)
        {
            bool CanPlayerParryRet = default;
            int rate;
            int rndNum;

            CanPlayerParryRet = Conversions.ToBoolean(0);

            rate = GetPlayerStat(index, StatType.Luck) / 6;
            rndNum = (int)Math.Round(General.Random.NextDouble(1d, 100d));

            if (rndNum <= rate)
            {
                CanPlayerParryRet = Conversions.ToBoolean(1);
            }

            return CanPlayerParryRet;

        }

        internal static void TryPlayerAttackPlayer(int attacker, int victim)
        {
            int mapNum;
            int Damage;
            int i;
            var armor = default(int);

            Damage = 0;

            // Can we attack the player?
            if (CanPlayerAttackPlayer(attacker, victim))
            {

                mapNum = GetPlayerMap(attacker);

                // check if NPC can avoid the attack
                if (CanPlayerDodge(victim))
                {
                    NetworkSend.SendActionMsg(mapNum, "Dodge!", (int) ColorType.Pink, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);
                    return;
                }

                if (CanPlayerParry(victim))
                {
                    NetworkSend.SendActionMsg(mapNum, "Parry!", (int) ColorType.Pink, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);
                    return;
                }

                // Get the damage we can do
                Damage = GetPlayerDamage(attacker);

                if (CanPlayerBlockHit(victim))
                {
                    NetworkSend.SendActionMsg(mapNum, "Block!", (int) ColorType.BrightCyan, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);
                    Damage = 0;
                    return;
                }
                else
                {

                    var loopTo = EquipmentType.Count - 1;
                    for (i = 0; i <= (int)loopTo; i++)
                    {
                        if (GetPlayerEquipment(victim, (EquipmentType)i) > 0)
                        {
                            armor += Core.Type.Item[GetPlayerEquipment(victim, (EquipmentType)i)].Data2;
                        }
                    }

                    // take away armour
                    Damage -= GetPlayerStat(victim, StatType.Spirit) * 2 + GetPlayerLevel(victim) * 3 + armor;

                    // * 1.5 if it's a crit!
                    if (CanPlayerCriticalHit(attacker))
                    {
                        Damage = (int)Math.Round(Damage * 1.5d);
                        NetworkSend.SendActionMsg(mapNum, "Critical!", (int) ColorType.BrightCyan, 1, GetPlayerX(attacker) * 32, GetPlayerY(attacker) * 32);
                    }
                }

                if (Damage > 0)
                {
                    PlayerAttackPlayer(attacker, victim, Damage);
                }
                else
                {
                    NetworkSend.PlayerMsg(attacker, "Your attack does nothing.", (int) ColorType.BrightRed);
                }

            }

        }

        public static void PlayerAttackPlayer(int attacker, int victim, int Damage)
        {
            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(attacker)) == 0 | Conversions.ToInteger(NetworkConfig.IsPlaying(victim)) == 0 | Damage < 0)
            {
                return;
            }

            // Check if our assailant has a weapon.
            int Weapon = 0;
            if (GetPlayerEquipment(attacker, EquipmentType.Weapon) > 0)
            {
                Weapon = GetPlayerEquipment(attacker, EquipmentType.Weapon);
            }

            // Stop our player's regeneration abilities.
            Core.Type.TempPlayer[attacker].StopRegen = 0;
            Core.Type.TempPlayer[attacker].StopRegenTimer = General.GetTimeMs();

            // Deal damage to our player.
            SetPlayerVital(victim, VitalType.HP, GetPlayerVital(victim, VitalType.HP) - Damage);

            // Send all the visuals to our player.
            if (Weapon > 0)
            {
                Animation.SendAnimation(GetPlayerMap(victim), Core.Type.Item[Weapon].Animation, 0, 0, (byte)TargetType.Player, victim);
            }
            NetworkSend.SendActionMsg(GetPlayerMap(victim), "-" + Damage, (int) ColorType.BrightRed, 1, GetPlayerX(victim) * 32, GetPlayerY(victim) * 32);
            NetworkSend.SendBlood(GetPlayerMap(victim), GetPlayerX(victim), GetPlayerY(victim));

            // set the regen timer
            Core.Type.TempPlayer[victim].StopRegen = 0;
            Core.Type.TempPlayer[victim].StopRegenTimer = General.GetTimeMs();

            // Reset attack timer
            Core.Type.TempPlayer[attacker].AttackTimer = General.GetTimeMs();

            if (!IsPlayerDead(victim))
            {
                // Send our player's new vitals to everyone that needs them.
                NetworkSend.SendVital(victim, (VitalType)VitalType.HP);
            }
            else
            {
                // Handle our dead player.
                HandlePlayerKillPlayer(attacker, victim);
            }
        }

        internal static void TryPlayerAttackNPC(int index, int MapNPCNum)
        {

            int NPCNum;

            int mapNum;

            int Damage;

            Damage = 0;

            // Can we attack the npc?
            if (CanPlayerAttackNPC(index, MapNPCNum))
            {
                mapNum = GetPlayerMap(index);
                NPCNum = Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Num;

                // check if NPC can avoid the attack
                if (NPC.CanNPCDodge(NPCNum))
                {
                    NetworkSend.SendActionMsg(mapNum, "Dodge!", (int) ColorType.Pink, 1, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X * 32, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y * 32);
                    return;
                }

                if (NPC.CanNPCParry(NPCNum))
                {
                    NetworkSend.SendActionMsg(mapNum, "Parry!", (int) ColorType.Pink, 1, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X * 32, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y * 32);
                    return;
                }

                // Get the damage we can do
                Damage = GetPlayerDamage(index);

                if (NPC.CanNPCBlock(NPCNum))
                {
                    NetworkSend.SendActionMsg(mapNum, "Block!", (int) ColorType.BrightCyan, 1, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].X * 32, Core.Type.MapNPC[mapNum].NPC[MapNPCNum].Y * 32);
                    Damage = 0;
                    return;
                }
                else
                {

                    Damage -= (int)Core.Type.NPC[NPCNum].Stat[(byte)StatType.Spirit] * 2 + Core.Type.NPC[NPCNum].Level * 3;

                    // * 1.5 if it's a crit!
                    if (CanPlayerCriticalHit(index))
                    {
                        Damage = (int)Math.Round(Damage * 1.5d);
                        NetworkSend.SendActionMsg(mapNum, "Critical!", (int) ColorType.BrightCyan, 1, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                    }

                }

                Core.Type.TempPlayer[index].Target = MapNPCNum;
                Core.Type.TempPlayer[index].TargetType = (byte)TargetType.NPC;
                NetworkSend.SendTarget(index, MapNPCNum, (byte)TargetType.NPC);

                if (Damage > 0)
                {
                    PlayerAttackNPC(index, MapNPCNum, Damage);
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Your attack does nothing.", (int) ColorType.BrightRed);
                }

            }

        }

        internal static bool IsPlayerDead(int index)
        {
            bool IsPlayerDeadRet = false;
            IsPlayerDeadRet = false;
            if (index <= 0 | index > Core.Constant.MAX_PLAYERS | !Core.Type.TempPlayer[index].InGame)
                return IsPlayerDeadRet;
            if (GetPlayerVital(index, VitalType.HP) < 0)
                IsPlayerDeadRet = true;
            return IsPlayerDeadRet;
        }

        internal static void HandlePlayerKillPlayer(int attacker, int victim)
        {
            // Notify everyone that our player has bit the dust.
            NetworkSend.GlobalMsg(string.Format("{0} has been killed by {1}!", GetPlayerName(victim), GetPlayerName(attacker)));

            // Hand out player experience
            HandlePlayerKillExperience(attacker, victim);

            // Handle our PK outcomes.
            HandlePlayerKilledPK(attacker, victim);

            // Remove our player from everyone's target list.
            foreach (var p in Core.Type.TempPlayer.Where((x, i) => x.InGame & GetPlayerMap((int)Operators.AddObject(i, 1)) == GetPlayerMap(victim) & Operators.ConditionalCompareObjectEqual(x.TargetType, TargetType.Player, false) & Operators.ConditionalCompareObjectEqual(x.Target, victim, false)).Select((x, i) => Operators.AddObject(i, 1)).ToArray())
            {
                Core.Type.TempPlayer[(int)p].Target = 0;
                Core.Type.TempPlayer[(int)p].TargetType = 0;
                NetworkSend.SendTarget(Conversions.ToInteger(p), 0, 0);
            }

            // Actually kill the player.
            OnDeath(victim);
        }

        internal static void HandlePlayerKillNPC(int MapNum, int index, int MapNPCNum)
        {
            // Set our attacker's target to nothing.
            NetworkSend.SendTarget(index, 0, 0);

            // Hand out player experience
            HandleNPCKillExperience(index, Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Num);

            // Drop items if we can.
            NPC.DropNPCItems(MapNum, MapNPCNum);

            // Set our NPC's data to default so we know it's dead.
            Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Num = 0;
            Core.Type.MapNPC[MapNum].NPC[MapNPCNum].SpawnWait = General.GetTimeMs();
            Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Vital[(byte) VitalType.HP] = 0;

            // Notify all our clients that the NPC has died.
            NPC.SendNPCDead(MapNum, MapNPCNum);

            // Check if our dead NPC is targetted by another player and remove their targets.
            foreach (var p in Core.Type.TempPlayer.Where((x, i) => x.InGame & GetPlayerMap((int)Operators.AddObject(i, 1)) == MapNum & Operators.ConditionalCompareObjectEqual(x.TargetType, TargetType.NPC, false) & Operators.ConditionalCompareObjectEqual(x.Target, MapNPCNum, false)).Select((x, i) => Operators.AddObject(i, 1)).ToArray())
            {
                Core.Type.TempPlayer[(int)p].Target = 0;
                Core.Type.TempPlayer[(int)p].TargetType = 0;
                NetworkSend.SendTarget(Conversions.ToInteger(p), 0, 0);
            }
        }

        internal static void HandlePlayerKilledPK(int attacker, int victim)
        {
            // TODO: Redo this method, it is horrendous.
            int z;
            var eqcount = default(int);
            int invcount = default, j = default;
            if (GetPlayerPK(victim) == 0)
            {
                if (GetPlayerPK(attacker) == 0)
                {
                    SetPlayerPK(attacker, 1);
                    NetworkSend.SendPlayerData(attacker);
                    NetworkSend.GlobalMsg(GetPlayerName(attacker) + " has been deemed a Player Killer!!!");
                }
            }
            else
            {
                NetworkSend.GlobalMsg(GetPlayerName(victim) + " has paid the price for being a Player Killer!!!");
            }

            if ((int)Core.Type.Map[GetPlayerMap(victim)].Moral > 0)
            {
                if (Core.Type.Moral[Core.Type.Map[GetPlayerMap(victim)].Moral].DropItems)
                {
                    if (GetPlayerLevel(victim) >= 10)
                    {

                        var loopTo = Core.Constant.MAX_INV;
                        for (z = 0; z <= (int)loopTo; z++)
                        {
                            if (GetPlayerInv(victim, z) > 0)
                            {
                                invcount += 0;
                            }
                        }

                        var loopTo1 = EquipmentType.Count - 1;
                        for (z = 0; z <= (int)loopTo1; z++)
                        {
                            if (GetPlayerEquipment(victim, (EquipmentType)z) > 0)
                            {
                                eqcount += 0;
                            }
                        }
                        z = (int)Math.Round(General.Random.NextDouble(1d, invcount + eqcount));

                        if (z == 0)
                            z = 0;
                        if (z > invcount + eqcount)
                            z = invcount + eqcount;
                        if (z > invcount)
                        {
                            z -= invcount;

                            for (int x = 0, loopTo2 = (int)(EquipmentType.Count - 1); x <= (int)loopTo2; x++)
                            {
                                if (GetPlayerEquipment(victim, (EquipmentType)x) > 0)
                                {
                                    j += 0;

                                    if (j == z)
                                    {
                                        // Here it is, drop this piece of equipment!
                                        NetworkSend.PlayerMsg(victim, "In death you lost grip on your " + Core.Type.Item[GetPlayerEquipment(victim, (EquipmentType)x)].Name, (int) ColorType.BrightRed);
                                        Item.SpawnItem(GetPlayerEquipment(victim, (EquipmentType)x), 1, GetPlayerMap(victim), GetPlayerX(victim), GetPlayerY(victim));
                                        SetPlayerEquipment(victim, 0, (EquipmentType)x);
                                        NetworkSend.SendWornEquipment(victim);
                                        NetworkSend.SendMapEquipment(victim);
                                    }
                                }
                            }
                        }
                        else
                        {

                            for (int x = 1, loopTo3 = Core.Constant.MAX_INV; x <= (int)loopTo3; x++)
                            {
                                if (GetPlayerInv(victim, x) > 0)
                                {
                                    j += 0;

                                    if (j == z)
                                    {
                                        // Here it is, drop this item!
                                        NetworkSend.PlayerMsg(victim, "In death you lost grip on your " + Core.Type.Item[GetPlayerInv(victim, x)].Name, (int) ColorType.BrightRed);
                                        Item.SpawnItem(GetPlayerInv(victim, x), GetPlayerInvValue(victim, x), GetPlayerMap(victim), GetPlayerX(victim), GetPlayerY(victim));
                                        SetPlayerInv(victim, x, 0);
                                        SetPlayerInvValue(victim, x, 0);
                                        NetworkSend.SendInventory(victim);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Data

        public static void CheckPlayerLevelUp(int index)
        {
            int expRollover;
            int level_count;

            level_count = 0;

            while (GetPlayerExp(index) >= GetPlayerNextLevel(index))
            {
                expRollover = GetPlayerExp(index) - GetPlayerNextLevel(index);
                SetPlayerLevel(index, GetPlayerLevel(index) + 1);
                SetPlayerPoints(index, GetPlayerPoints(index) + Constant.STAT_PER_LEVEL);
                SetPlayerExp(index, expRollover);
                level_count += 0;
            }

            if (level_count > 0)
            {
                if (level_count == 1)
                {
                    // singular
                    NetworkSend.GlobalMsg(GetPlayerName(index) + " has gained " + level_count + " level!");
                }
                else
                {
                    // plural
                    NetworkSend.GlobalMsg(GetPlayerName(index) + " has gained " + level_count + " levels!");
                }
                NetworkSend.SendActionMsg(GetPlayerMap(index), "Level Up", (int) ColorType.Yellow, 1, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                NetworkSend.SendExp(index);
                NetworkSend.SendPlayerData(index);
            }
        }

        public static int GetPlayerJob(int index)
        {
            int GetPlayerJobRet = default;
            if (Core.Type.Player[index].Job == 0)
                Core.Type.Player[index].Job = 0;
            GetPlayerJobRet = Core.Type.Player[index].Job;
            return GetPlayerJobRet;
        }

        public static void SetPlayerPK(int index, int PK)
        {
            Core.Type.Player[index].Pk = (byte)PK;
        }

        #endregion

        #region Incoming Packets

        internal static void HandleUseChar(int index)
        {
            if (NetworkConfig.IsLoggedIn(index) == false)
            {
                NetworkSend.AlertMsg(index, (byte)DialogueMsg.Connection, (byte)MenuType.Login);
                return;
            }

            // Set the flag so we know the person is in the game
            Core.Type.TempPlayer[index].InGame = true;

            // Send an ok to client to start receiving in game data
            NetworkSend.SendLoginOK(index);
            JoinGame(index);
            string text = string.Format("{0} | {1} has began playing {2}.", GetPlayerLogin(index), GetPlayerName(index), Settings.GameName);
            Core.Log.Add(text, Constant.PLAYER_LOG);
            Console.WriteLine(text);
            
        }

        #endregion

        #region Outgoing Packets

        public static void SendLeaveMap(int index, int mapNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((byte) ServerPackets.SLeftMap);
            buffer.WriteInt32(index);
            NetworkConfig.SendDataToMapBut(index, mapNum, ref buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        #endregion

        #region Movement
        public static void PlayerWarp(int index, int MapNum, int X, int Y, bool NoInstance = false)
        {
            int OldMap;
            int i;
            ByteStream buffer;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | MapNum <= 0 | MapNum > Core.Constant.MAX_MAPS)
                return;

            // Check if you are out of bounds
            if (X > Core.Type.Map[MapNum].MaxX)
                X = Core.Type.Map[MapNum].MaxX;
            if (Y > Core.Type.Map[MapNum].MaxY)
                Y = Core.Type.Map[MapNum].MaxY;

            Core.Type.TempPlayer[index].EventProcessingCount = 0;
            Core.Type.TempPlayer[index].EventMap.CurrentEvents = 0;

            // clear target
            Core.Type.TempPlayer[index].Target = 0;
            Core.Type.TempPlayer[index].TargetType = 0;
            NetworkSend.SendTarget(index, 0, 0);

            // clear events
            Core.Type.TempPlayer[index].EventMap.CurrentEvents = 0;

            // Save old map to send erase player data to
            OldMap = GetPlayerMap(index);

            if (OldMap != MapNum)
            {
                SendLeaveMap(index, OldMap);
            }

            SetPlayerMap(index, MapNum);
            SetPlayerX(index, X);
            SetPlayerY(index, Y);
            if (Pet.PetAlive(index))
            {
                Pet.SetPetX(index, X);
                Pet.SetPetY(index, Y);
                Core.Type.TempPlayer[index].PetTarget = 0;
                Core.Type.TempPlayer[index].PetTargetType = 0;
                Pet.SendPetXy(index, X, Y);
            }

            NetworkSend.SendPlayerXY(index);

            // send equipment of all people on new map
            if (GameLogic.GetTotalMapPlayers(MapNum) > 0)
            {
                var loopTo = NetworkConfig.Socket.HighIndex;
                for (i = 0; i <= (int)loopTo; i++)
                {
                    if (NetworkConfig.IsPlaying(i))
                    {
                        if (GetPlayerMap(i) == MapNum)
                        {
                            NetworkSend.SendMapEquipmentTo(i, index);
                        }
                    }
                }
            }

            // Now we check if there were any players left on the map the player just left, and if not stop processing npcs
            if (GameLogic.GetTotalMapPlayers(OldMap) == 0)
            {
                PlayersOnMap[OldMap] = false;

                // Regenerate all NPCs' health
                var loopTo1 = Core.Constant.MAX_MAP_NPCS - 1;
                for (i = 0; i <= (int)loopTo1; i++)
                {

                    if (Core.Type.MapNPC[OldMap].NPC[i].Num > 0)
                    {
                        Core.Type.MapNPC[OldMap].NPC[i].Vital[(byte) VitalType.HP] = GameLogic.GetNPCMaxVital(Core.Type.MapNPC[OldMap].NPC[i].Num, VitalType.HP);
                    }

                }

            }

            // Sets it so we know to process npcs on the map
            PlayersOnMap[MapNum] = true;
            Core.Type.TempPlayer[index].GettingMap = true;

            Moral.SendUpdateMoralTo(index, Core.Type.Map[MapNum].Moral);

            buffer = new ByteStream(4);
            buffer.WriteInt32((byte) ServerPackets.SCheckForMap);
            buffer.WriteInt32(MapNum);
            buffer.WriteInt32(Core.Type.Map[MapNum].Revision);
            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);

            buffer.Dispose();

        }

        public static void PlayerMove(int index, int Dir, int Movement, bool ExpectingWarp)
        {
            int mapNum;
            int x;
            int y;
            bool begineventprocessing;
            bool Moved;
            var DidWarp = default(bool);
            byte NewMapX;
            byte NewMapY;
            var vital = default(int);
            int Color;
            var amount = default(int);

            // Check for subscript out of range
            if (Dir < (byte) DirectionType.Up | Dir > (byte) DirectionType.DownRight | Movement < (byte) MovementType.Standing | Movement > (byte) MovementType.Running)
            {
                return;
            }

            if (Core.Type.TempPlayer[index].InShop > 0 | Core.Type.TempPlayer[index].InBank)
            {
                return;
            }

            SetPlayerDir(index, Dir);
            Moved = Conversions.ToBoolean(0);
            mapNum = GetPlayerMap(index);

            switch (Dir)
            {
                case var @case when @case == (byte) DirectionType.Up:
                    {
                        // Check to make sure not outside of boundaries
                        if (GetPlayerY(index) > 0)
                        {
                            // Check to make sure that the tile is walkable
                            if (!IsDirBlocked(ref Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].DirBlock, (byte) DirectionType.Up))
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index) - 1].Type != TileType.Blocked & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index) - 1].Type2 != TileType.Blocked)
                                {
                                    if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index) - 1].Type != TileType.Resource & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index) - 1].Type2 != TileType.Resource)
                                    {
                                        SetPlayerY(index, GetPlayerY(index) - 1);
                                        NetworkSend.SendPlayerMove(index, Movement);
                                        Moved = Conversions.ToBoolean(1);

                                        // Check for event
                                        for (int i = 0, loopTo = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i <= (int)loopTo; i++)
                                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                                    }
                                }
                            }
                        }
                        else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                        {
                            // Check to see if we can move them to another map
                            if (Core.Type.Map[GetPlayerMap(index)].Up > 0)
                            {
                                NewMapY = Core.Type.Map[Core.Type.Map[GetPlayerMap(index)].Up].MaxY;
                                PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Up, GetPlayerX(index), NewMapY);
                                DidWarp = Conversions.ToBoolean(1);
                                Moved = Conversions.ToBoolean(1);
                            }
                        }

                        break;
                    }

                case var case1 when case1 == (byte) DirectionType.Down:
                    {
                        // Check to make sure not outside of boundaries
                        if (GetPlayerY(index) < Core.Type.Map[mapNum].MaxY)
                        {
                            // Check to make sure that the tile is walkable
                            if (!IsDirBlocked(ref Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].DirBlock, (byte) DirectionType.Down))
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index) + 1].Type != TileType.Blocked & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index) + 1].Type2 != TileType.Blocked)
                                {
                                    if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index) + 1].Type != TileType.Resource & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index) + 1].Type2 != TileType.Resource)
                                    {
                                        SetPlayerY(index, GetPlayerY(index) + 1);
                                        NetworkSend.SendPlayerMove(index, Movement);
                                        Moved = Conversions.ToBoolean(1);

                                        // Check for event
                                        for (int i = 0, loopTo1 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i <= (int)loopTo1; i++)
                                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                                    }
                                }
                            }
                        }
                        else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                        {
                            // Check to see if we can move them to another map
                            if (Core.Type.Map[GetPlayerMap(index)].Down > 0)
                            {
                                PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Down, GetPlayerX(index), 0);
                                DidWarp = Conversions.ToBoolean(1);
                                Moved = Conversions.ToBoolean(1);
                            }
                        }

                        break;
                    }

                case var case2 when case2 == (byte) DirectionType.Left:
                    {
                        // Check to make sure not outside of boundaries
                        if (GetPlayerX(index) > 0)
                        {
                            // Check to make sure that the tile is walkable
                            if (!IsDirBlocked(ref Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].DirBlock, (byte) DirectionType.Left))
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index)].Type != TileType.Blocked & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index)].Type2 != TileType.Blocked)
                                {
                                    if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index)].Type != TileType.Resource & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index)].Type2 != TileType.Resource)
                                    {
                                        SetPlayerX(index, GetPlayerX(index) - 1);
                                        NetworkSend.SendPlayerMove(index, Movement);
                                        Moved = Conversions.ToBoolean(1);

                                        // Check for event
                                        for (int i = 0, loopTo2 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i <= (int)loopTo2; i++)
                                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                                    }
                                }
                            }
                        }
                        else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                        {
                            // Check to see if we can move them to another map
                            if (Core.Type.Map[GetPlayerMap(index)].Left > 0)
                            {
                                NewMapX = Core.Type.Map[Core.Type.Map[GetPlayerMap(index)].Left].MaxX;
                                PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Left, NewMapX, GetPlayerY(index));
                                DidWarp = Conversions.ToBoolean(1);
                                Moved = Conversions.ToBoolean(1);
                            }
                        }

                        break;
                    }

                case var case3 when case3 == (byte) DirectionType.Right:
                    {
                        // Check to make sure not outside of boundaries
                        if (GetPlayerX(index) < Core.Type.Map[mapNum].MaxX)
                        {
                            // Check to make sure that the tile is walkable
                            if (!IsDirBlocked(ref Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].DirBlock, (byte) DirectionType.Right))
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index)].Type != TileType.Blocked & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index)].Type2 != TileType.Blocked)
                                {
                                    if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index)].Type != TileType.Resource & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index)].Type2 != TileType.Resource)
                                    {
                                        SetPlayerX(index, GetPlayerX(index) + 1);
                                        NetworkSend.SendPlayerMove(index, Movement);
                                        Moved = Conversions.ToBoolean(1);

                                        // Check for event
                                        for (int i = 0, loopTo3 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i <= (int)loopTo3; i++)
                                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                                    }
                                }
                            }
                        }
                        else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                        {
                            // Check to see if we can move them to another map
                            if (Core.Type.Map[GetPlayerMap(index)].Right > 0)
                            {
                                PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Right, 0, GetPlayerY(index));
                                DidWarp = Conversions.ToBoolean(1);
                                Moved = Conversions.ToBoolean(1);
                            }
                        }

                        break;
                    }

                case var case4 when case4 == (byte) DirectionType.UpRight:
                    {
                        // Check to make sure not outside of boundaries
                        if (GetPlayerY(index) > 0 && GetPlayerX(index) < Core.Type.Map[mapNum].MaxX)
                        {
                            // Check to make sure that the tile is walkable
                            if (!IsDirBlocked(ref Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].DirBlock, (byte) DirectionType.UpRight))
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index) - 1].Type != TileType.Blocked & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index) - 1].Type2 != TileType.Blocked)
                                {
                                    if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index) - 1].Type != TileType.Resource & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index) - 1].Type2 != TileType.Resource)
                                    {
                                        SetPlayerX(index, GetPlayerX(index) + 1);
                                        SetPlayerY(index, GetPlayerY(index) - 1);
                                        NetworkSend.SendPlayerMove(index, Movement);
                                        Moved = Conversions.ToBoolean(1);

                                        // Check for event
                                        for (int i = 0, loopTo4 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i <= (int)loopTo4; i++)
                                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                                    }
                                }
                            }
                        }

                        break;
                    }

                case var case5 when case5 == (byte) DirectionType.UpLeft:
                    {
                        // Check to make sure not outside of boundaries
                        if (GetPlayerY(index) > 0 && GetPlayerX(index) > 0)
                        {
                            // Check to make sure that the tile is walkable
                            if (!IsDirBlocked(ref Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].DirBlock, (byte) DirectionType.UpLeft))
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index) - 1].Type != TileType.Blocked & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index) - 1].Type2 != TileType.Blocked)
                                {
                                    if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index) - 1].Type != TileType.Resource & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index) - 1].Type2 != TileType.Resource)
                                    {
                                        SetPlayerX(index, GetPlayerX(index) - 1);
                                        SetPlayerY(index, GetPlayerY(index) - 1);
                                        NetworkSend.SendPlayerMove(index, Movement);
                                        Moved = Conversions.ToBoolean(1);

                                        // Check for event
                                        for (int i = 0, loopTo5 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i <= (int)loopTo5; i++)
                                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                                    }
                                }
                            }
                        }

                        break;
                    }

                case var case6 when case6 == (byte) DirectionType.DownRight:
                    {
                        // Check to make sure not outside of boundaries
                        if (GetPlayerY(index) < Core.Type.Map[mapNum].MaxY && GetPlayerX(index) < Core.Type.Map[mapNum].MaxX)
                        {
                            // Check to make sure that the tile is walkable
                            if (!IsDirBlocked(ref Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].DirBlock, (byte) DirectionType.DownRight))
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index) + 1].Type != TileType.Blocked & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index) + 1].Type2 != TileType.Blocked)
                                {
                                    if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index) + 1].Type != TileType.Resource & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) + 1, GetPlayerY(index) + 1].Type2 != TileType.Resource)
                                    {
                                        SetPlayerX(index, GetPlayerX(index) + 1);
                                        SetPlayerY(index, GetPlayerY(index) + 1);
                                        NetworkSend.SendPlayerMove(index, Movement);
                                        Moved = Conversions.ToBoolean(1);

                                        // Check for event
                                        for (int i = 0, loopTo6 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i <= (int)loopTo6; i++)
                                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                                    }
                                }
                            }
                        }

                        break;
                    }

                case var case7 when case7 == (byte) DirectionType.DownLeft:
                    {
                        // Check to make sure not outside of boundaries
                        if (GetPlayerY(index) < Core.Type.Map[mapNum].MaxY && GetPlayerX(index) > 0)
                        {
                            // Check to make sure that the tile is walkable
                            if (!IsDirBlocked(ref Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].DirBlock, (byte) DirectionType.DownLeft))
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index) + 1].Type != TileType.Blocked & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index) + 1].Type2 != TileType.Blocked)
                                {
                                    if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index) + 1].Type != TileType.Resource & Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index) - 1, GetPlayerY(index) + 1].Type2 != TileType.Resource)
                                    {
                                        SetPlayerX(index, GetPlayerX(index) - 1);
                                        SetPlayerY(index, GetPlayerY(index) + 1);
                                        NetworkSend.SendPlayerMove(index, Movement);
                                        Moved = Conversions.ToBoolean(1);

                                        // Check for event
                                        for (int i = 0, loopTo7 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i <= (int)loopTo7; i++)
                                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                                    }
                                }
                            }
                        }

                        break;
                    }
            }

            {
                ref var withBlock = ref Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)];
                mapNum = 0;
                x = 0;
                y = 0;

                // Check to see if the tile is a warp tile, and if so warp them
                if (withBlock.Type == TileType.Warp)
                {
                    mapNum = withBlock.Data1;
                    x = withBlock.Data2;
                    y = withBlock.Data3;
                }

                if (withBlock.Type2 == TileType.Warp)
                {
                    mapNum = withBlock.Data1_2;
                    x = withBlock.Data2_2;
                    y = withBlock.Data3_2;
                }

                if (mapNum > 0)
                {
                    PlayerWarp(index, mapNum, x, y);

                    DidWarp = Conversions.ToBoolean(1);
                    Moved = Conversions.ToBoolean(1);
                }

                mapNum = 0;
                x = 0;
                y = 0;

                // Check for a shop, and if so open it
                if (withBlock.Type == TileType.Shop)
                {
                    x = withBlock.Data1;
                }

                if (withBlock.Type2 == TileType.Shop)
                {
                    x = withBlock.Data1_2;
                }

                if (x > 0) // shop exists?
                {
                    if (Strings.Len(Core.Type.Shop[x].Name) > 0) // name exists?
                    {
                        NetworkSend.SendOpenShop(index, x);
                        Core.Type.TempPlayer[index].InShop = x; // stops movement and the like
                    }
                }

                // Check to see if the tile is a bank, and if so send bank
                if (withBlock.Type == TileType.Bank | withBlock.Type2 == TileType.Bank)
                {
                    NetworkSend.SendBank(index);
                    Core.Type.TempPlayer[index].InBank = true;
                    Moved = Conversions.ToBoolean(1);
                }

                // Check if it's a heal tile
                if (withBlock.Type == TileType.Heal)
                {
                    vital = withBlock.Data1;
                    amount = withBlock.Data2;
                }

                if (withBlock.Type2 == TileType.Heal)
                {
                    vital = withBlock.Data1_2;
                    amount += withBlock.Data2_2;
                }

                if (vital > 0)
                {
                    if (!(GetPlayerVital(index, (VitalType)vital) == GetPlayerMaxVital(index, (VitalType)vital)))
                    {
                        if (vital == (byte)VitalType.HP)
                        {
                            Color = (int) ColorType.BrightGreen;
                        }
                        else
                        {
                            Color = (int) ColorType.BrightBlue;
                        }

                        NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + amount, Color, (byte)ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32, 1);
                        SetPlayerVital(index, (VitalType)vital, GetPlayerVital(index, (VitalType)vital) + amount);
                        NetworkSend.PlayerMsg(index, "You feel rejuvenating forces coursing through your body.", (int) ColorType.BrightGreen);
                        NetworkSend.SendVital(index, (VitalType)vital);
                    }
                    Moved = Conversions.ToBoolean(1);
                }

                // Check if it's a trap tile
                if (withBlock.Type == TileType.Trap)
                {
                    amount = withBlock.Data1;
                }

                if (withBlock.Type2 == TileType.Trap)
                {
                    amount += withBlock.Data1_2;
                }

                if (amount > 0)
                {
                    NetworkSend.SendActionMsg(GetPlayerMap(index), "-" + amount, (int) ColorType.BrightRed, (byte)ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32, 1);
                    if (GetPlayerVital(index, (VitalType)VitalType.HP) - amount < 0)
                    {
                        KillPlayer(index);
                        NetworkSend.PlayerMsg(index, "You've been killed by a trap.", (int) ColorType.BrightRed);
                    }
                    else
                    {
                        SetPlayerVital(index, (VitalType)VitalType.HP, GetPlayerVital(index, (VitalType)VitalType.HP) - amount);
                        NetworkSend.PlayerMsg(index, "You've been injured by a trap.", (int) ColorType.BrightRed);
                        NetworkSend.SendVital(index, (VitalType)VitalType.HP);
                    }
                    Moved = Conversions.ToBoolean(1);
                }

            }

            // They tried to hack
            if (Conversions.ToInteger(Moved) == 0 | ExpectingWarp & !DidWarp)
            {
                PlayerWarp(index, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index));
            }

            x = GetPlayerX(index);
            y = GetPlayerY(index);

            if (Conversions.ToInteger(Moved) == 1)
            {
                if (Core.Type.TempPlayer[index].EventMap.CurrentEvents > 0)
                {
                    for (int i = 0, loopTo8 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i <= (int)loopTo8; i++)
                    {
                        if (Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID > 0)
                        {
                            if ((int)Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].Globals == 1)
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].X == x & Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].Y == y & (int)Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageID].Trigger == 1 & Core.Type.TempPlayer[index].EventMap.EventPages[i].Visible == true)
                                    begineventprocessing = Conversions.ToBoolean(1);
                            }
                            else if (Core.Type.TempPlayer[index].EventMap.EventPages[i].X == x & Core.Type.TempPlayer[index].EventMap.EventPages[i].Y == y & (int)Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageID].Trigger == 1 & Core.Type.TempPlayer[index].EventMap.EventPages[i].Visible == true)
                                begineventprocessing = Conversions.ToBoolean(1);
                            begineventprocessing = Conversions.ToBoolean(0);
                            if (Conversions.ToInteger(begineventprocessing) == 1)
                            {
                                // Process this event, it is on-touch and everything checks out.
                                if (Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageID].CommandListCount > 0)
                                {
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].Active = 0;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].ActionTimer = General.GetTimeMs();
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].CurList = 0;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].CurSlot = 0;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].EventID = Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].PageID = Core.Type.TempPlayer[index].EventMap.EventPages[i].PageID;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].WaitingForResponse = 0;
                                    ;

                                    int EventID = Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID;
                                    int pageID = Core.Type.TempPlayer[index].EventMap.EventPages[i].PageID;
                                    int commandListCount = Core.Type.Map[GetPlayerMap(index)].Event[EventID].Pages[pageID].CommandListCount;

                                    Array.Resize(ref Core.Type.TempPlayer[index].EventProcessing[EventID].ListLeftOff, commandListCount);

                                }
                                begineventprocessing = Conversions.ToBoolean(0);
                            }
                        }
                    }
                }
            }

        }

        #endregion

        #region Inventory

        public static int HasItem(int index, int ItemNum)
        {
            int HasItemRet = default;
            int i;

            // Check for subscript out of range
            if (ItemNum <= 0 | ItemNum > Core.Constant.MAX_ITEMS)
            {
                return HasItemRet;
            }

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i <= (int)loopTo; i++)
            {
                // Check to see if the player has the item
                if (GetPlayerInv(index, i) == ItemNum)
                {
                    if (Core.Type.Item[ItemNum].Type == (byte)ItemType.Currency | Core.Type.Item[ItemNum].Stackable == 1)
                    {
                        HasItemRet += GetPlayerInvValue(index, i);
                    }
                    else
                    {
                        HasItemRet += 0;
                    }
                }
            }

            return HasItemRet;

        }

        public static int FindItemSlot(int index, int ItemNum)
        {
            int FindItemSlotRet = default;
            int i;

            FindItemSlotRet = 0;

            // Check for subscript out of range
            if (ItemNum <= 0 | ItemNum > Core.Constant.MAX_ITEMS)
            {
                return FindItemSlotRet;
            }

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i <= (int)loopTo; i++)
            {
                // Check to see if the player has the item
                if (GetPlayerInv(index, i) == ItemNum)
                {
                    FindItemSlotRet = i;
                    return FindItemSlotRet;
                }
            }

            return FindItemSlotRet;

        }

        public static void PlayerMapGetItem(int index)
        {
            int i;
            int itemnum;
            int n;
            int mapNum;
            string Msg;

            mapNum = GetPlayerMap(index);

            var loopTo = Core.Constant.MAX_MAP_ITEMS - 1;
            for (i = 0; i <= (int)loopTo; i++)
            {

                // See if theres even an item here
                if (Core.Type.MapItem[mapNum, i].Num > 0 & Core.Type.MapItem[mapNum, i].Num <= Core.Constant.MAX_ITEMS)
                {
                    // our drop?
                    if (CanPlayerPickupItem(index, i))
                    {
                        // Check if item is at the same location as the player
                        if (Core.Type.MapItem[mapNum, i].X == GetPlayerX(index))
                        {
                            if (Core.Type.MapItem[mapNum, i].Y == GetPlayerY(index))
                            {
                                // Find open slot
                                n = FindOpenInvSlot(index, Core.Type.MapItem[mapNum, i].Num);

                                // Open slot available?
                                if (n != -1)
                                {
                                    // Set item in players inventor
                                    itemnum = MapItem[mapNum, i].Num;

                                    SetPlayerInv(index, n, MapItem[mapNum, i].Num);

                                    if (Core.Type.Item[GetPlayerInv(index, n)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerInv(index, n)].Stackable == 1)
                                    {
                                        SetPlayerInvValue(index, n, GetPlayerInvValue(index, n) + MapItem[mapNum, i].Value);
                                        Msg = MapItem[mapNum, i].Value + " " + Core.Type.Item[GetPlayerInv(index, n)].Name;
                                    }
                                    else
                                    {
                                        SetPlayerInvValue(index, n, 1);
                                        Msg = Core.Type.Item[GetPlayerInv(index, n)].Name;
                                    }

                                    // Erase item from the map
                                    MapItem[mapNum, i].Num = 0;
                                    MapItem[mapNum, i].Value = 0;
                                    MapItem[mapNum, i].X = 0;
                                    MapItem[mapNum, i].Y = 0;

                                    NetworkSend.SendInventoryUpdate(index, n);
                                    Item.SpawnItemSlot(i, 0, 0, GetPlayerMap(index), 0, 0);
                                    NetworkSend.SendActionMsg(GetPlayerMap(index), Msg, (int) ColorType.White, (byte)ActionMsgType.Static, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                    break;
                                }
                                else
                                {
                                    NetworkSend.PlayerMsg(index, "Your inventory is full.", (int) ColorType.BrightRed);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool CanPlayerPickupItem(int index, int mapItemNum)
        {
            bool CanPlayerPickupItemRet = default;
            int mapNum;

            mapNum = GetPlayerMap(index);

            if (Core.Type.Map[mapNum].Moral > 0)
            {
                if (Core.Type.Moral[Core.Type.Map[mapNum].Moral].CanPickupItem)
                {
                    // no lock or locked to player?
                    if (string.IsNullOrEmpty(Core.Type.MapItem[mapNum, mapItemNum].PlayerName) | Core.Type.MapItem[mapNum, mapItemNum].PlayerName == GetPlayerName(index))
                    {
                        CanPlayerPickupItemRet = Conversions.ToBoolean(1);
                        return CanPlayerPickupItemRet;
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "You can't pickup items here!", (int) ColorType.BrightRed);
                }
            }

            CanPlayerPickupItemRet = Conversions.ToBoolean(0);
            return CanPlayerPickupItemRet;
        }

        public static int FindOpenInvSlot(int index, int ItemNum)
        {
            int FindOpenInvSlotRet = default;
            int i;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | ItemNum <= 0 | ItemNum > Core.Constant.MAX_ITEMS)
            {
                return FindOpenInvSlotRet;
            }

            if (Core.Type.Item[ItemNum].Type == (byte)ItemType.Currency | Core.Type.Item[ItemNum].Stackable == 1)
            {
                // If currency then check to see if they already have an instance of the item and add it to that
                var loopTo = Core.Constant.MAX_INV;
                for (i = 0; i <= (int)loopTo; i++)
                {
                    if (GetPlayerInv(index, i) == ItemNum)
                    {
                        FindOpenInvSlotRet = i;
                        return FindOpenInvSlotRet;
                    }
                }
            }

            var loopTo1 = Core.Constant.MAX_INV;
            for (i = 0; i <= (int)loopTo1; i++)
            {
                // Try to find an open free slot
                if (GetPlayerInv(index, i) == 0)
                {
                    FindOpenInvSlotRet = i;
                    return FindOpenInvSlotRet;
                }
            }

            FindOpenInvSlotRet = -1;
            return FindOpenInvSlotRet;
        }

        public static bool TakeInv(int index, int ItemNum, int ItemVal)
        {
            bool TakeInvRet = default;
            int i;

            TakeInvRet = Conversions.ToBoolean(0);

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | ItemNum <= 0 | ItemNum > Core.Constant.MAX_ITEMS)
            {
                return TakeInvRet;
            }

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i <= (int)loopTo; i++)
            {

                // Check to see if the player has the item
                if (GetPlayerInv(index, i) == ItemNum)
                {
                    if (Core.Type.Item[ItemNum].Type == (byte)ItemType.Currency | Core.Type.Item[ItemNum].Stackable == 1)
                    {

                        // Is what we are trying to take away more then what they have?  If so just set it to zero
                        if (ItemVal >= GetPlayerInvValue(index, i))
                        {
                            TakeInvRet = Conversions.ToBoolean(1);
                        }
                        else
                        {
                            SetPlayerInvValue(index, i, GetPlayerInvValue(index, i) - ItemVal);
                            NetworkSend.SendInventoryUpdate(index, i);
                        }
                    }
                    else
                    {
                        TakeInvRet = Conversions.ToBoolean(1);
                    }

                    if (TakeInvRet)
                    {
                        SetPlayerInv(index, i, 0);
                        SetPlayerInvValue(index, i, 0);
                        // Send the inventory update
                        NetworkSend.SendInventoryUpdate(index, i);
                        return TakeInvRet;
                    }
                }

            }

            return TakeInvRet;

        }

        public static bool GiveInv(int index, int ItemNum, int ItemVal, bool SendUpdate = true)
        {
            bool GiveInvRet = default;
            int i;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | ItemNum <= 0 | ItemNum > Core.Constant.MAX_ITEMS)
            {
                GiveInvRet = Conversions.ToBoolean(0);
                return GiveInvRet;
            }

            i = FindOpenInvSlot(index, ItemNum);

            // Check to see if inventory is full
            if (i != -1)
            {
                SetPlayerInv(index, i, ItemNum);
                SetPlayerInvValue(index, i, GetPlayerInvValue(index, i) + ItemVal);
                if (SendUpdate)
                    NetworkSend.SendInventoryUpdate(index, i);
                GiveInvRet = Conversions.ToBoolean(1);
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Your inventory is full.", (int) ColorType.BrightRed);
                GiveInvRet = Conversions.ToBoolean(0);
            }

            return GiveInvRet;

        }

        public static void PlayerMapDropItem(int index, int invNum, int amount)
        {
            int i;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | invNum <= 0 | invNum > Core.Constant.MAX_INV)
            {
                return;
            }

            // check the player isn't doing something
            if (Core.Type.TempPlayer[index].InBank | Core.Type.TempPlayer[index].InShop > 0 | Core.Type.TempPlayer[index].InTrade > 0)
                return;

            if (Conversions.ToInteger(Core.Type.Moral[GetPlayerMap(index)].CanDropItem) == 0)
            {
                NetworkSend.PlayerMsg(index, "You can't drop items here!", (int) ColorType.BrightRed);
                return;
            }

            if (GetPlayerInv(index, invNum) > 0)
            {
                if (GetPlayerInv(index, invNum) <= Core.Constant.MAX_ITEMS)
                {
                    i = Item.FindOpenMapItemSlot(GetPlayerMap(index));

                    if (i != 0)
                    {
                        {
                            var withBlock = MapItem[GetPlayerMap(index), i];
                            withBlock.Num = GetPlayerInv(index, invNum);
                            withBlock.X = (byte)GetPlayerX(index);
                            withBlock.Y = (byte)GetPlayerY(index);
                            withBlock.PlayerName = GetPlayerName(index);
                            withBlock.PlayerTimer = General.GetTimeMs() + Constant.ITEM_SPAWN_TIME;

                            withBlock.CanDespawn = true;
                            withBlock.DespawnTimer = General.GetTimeMs() + Constant.ITEM_DESPAWN_TIME;

                            if (Core.Type.Item[GetPlayerInv(index, invNum)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerInv(index, invNum)].Stackable == 1)
                            {
                                // Check if its more then they have and if so drop it all
                                if (amount >= GetPlayerInvValue(index, invNum))
                                {
                                    amount = GetPlayerInvValue(index, invNum);
                                    withBlock.Value = amount;
                                    SetPlayerInv(index, invNum, 0);
                                    SetPlayerInvValue(index, invNum, 0);
                                }
                                else
                                {
                                    withBlock.Value = amount;
                                    SetPlayerInvValue(index, invNum, GetPlayerInvValue(index, invNum) - amount);
                                }
                                NetworkSend.MapMsg(GetPlayerMap(index), string.Format("{0} has dropped {1} ({2}x).", GetPlayerName(index), GameLogic.CheckGrammar(Core.Type.Item[GetPlayerInv(index, invNum)].Name), amount), (int) ColorType.Yellow);
                            }
                            else
                            {
                                // It's not a currency object so this is easy
                                withBlock.Value = 0;

                                // send message
                                NetworkSend.MapMsg(GetPlayerMap(index), string.Format("{0} has dropped {1}.", GetPlayerName(index), GameLogic.CheckGrammar(Core.Type.Item[GetPlayerInv(index, invNum)].Name)), (int) ColorType.Yellow);
                                SetPlayerInv(index, invNum, 0);
                                SetPlayerInvValue(index, invNum, 0);
                            }

                            // Send inventory update
                            NetworkSend.SendInventoryUpdate(index, invNum);
                            // Spawn the item before we set the num or we'll get a different free map item slot
                            Item.SpawnItemSlot(i, withBlock.Num, amount, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index));
                        }
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "Too many items already on the ground.", (int) ColorType.Yellow);
                    }
                }
            }

        }

        public static bool TakeInvSlot(int index, int InvSlot, int ItemVal)
        {
            bool TakeInvSlotRet = default;
            object itemNum;

            TakeInvSlotRet = Conversions.ToBoolean(0);

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | InvSlot < 0 | InvSlot > Core.Constant.MAX_ITEMS)
                return TakeInvSlotRet;

            itemNum = GetPlayerInv(index, InvSlot);

            if (Core.Type.Item[Conversions.ToInteger(itemNum)].Type == (byte)ItemType.Currency | Core.Type.Item[Conversions.ToInteger(itemNum)].Stackable == 1)
            {

                // Is what we are trying to take away more then what they have?  If so just set it to zero
                if (ItemVal >= GetPlayerInvValue(index, InvSlot))
                {
                    TakeInvSlotRet = Conversions.ToBoolean(1);
                }
                else
                {
                    SetPlayerInvValue(index, InvSlot, GetPlayerInvValue(index, InvSlot) - ItemVal);
                }
            }
            else
            {
                TakeInvSlotRet = Conversions.ToBoolean(1);
            }

            if (TakeInvSlotRet)
            {
                SetPlayerInv(index, InvSlot, 0);
                SetPlayerInvValue(index, InvSlot, 0);
                return TakeInvSlotRet;
            }

            return TakeInvSlotRet;

        }

        public static object CanPlayerUseItem(int index, int itemNum)
        {
            object CanPlayerUseItemRet = default;
            int i;

            if ((int)Core.Type.Map[GetPlayerMap(index)].Moral > 0)
            {
                if (Conversions.ToInteger(Core.Type.Moral[Core.Type.Map[GetPlayerMap(index)].Moral].CanUseItem) == 0)
                {
                    NetworkSend.PlayerMsg(index, "You can't use items here!", (int) ColorType.BrightRed);
                    return CanPlayerUseItemRet;
                }
            }

            var loopTo = (byte)StatType.Count - 1;
            for (i = 0; i <= (int)loopTo; i++)
            {
                if (GetPlayerStat(index, (StatType)i) < Core.Type.Item[itemNum].Stat_Req[i])
                {
                    NetworkSend.PlayerMsg(index, "You do not meet the stat requirements to use this item.", (int) ColorType.BrightRed);
                    return CanPlayerUseItemRet;
                }
            }

            if (Core.Type.Item[itemNum].LevelReq > GetPlayerLevel(index))
            {
                NetworkSend.PlayerMsg(index, "You do not meet the level requirements to use this item.", (int) ColorType.BrightRed);
                return CanPlayerUseItemRet;
            }

            // Make sure they are the right job
            if (!(Core.Type.Item[itemNum].JobReq == GetPlayerJob(index)) & !(Core.Type.Item[itemNum].JobReq == 0))
            {
                NetworkSend.PlayerMsg(index, "You do not meet the class requirements to use this item.", (int) ColorType.BrightRed);
                return CanPlayerUseItemRet;
            }

            // access requirement
            if (!(GetPlayerAccess(index) >= Core.Type.Item[itemNum].AccessReq))
            {
                NetworkSend.PlayerMsg(index, "You do not meet the access requirement to equip this item.", (int) ColorType.BrightRed);
                return CanPlayerUseItemRet;
            }

            // check the player isn't doing something
            if (Core.Type.TempPlayer[index].InBank == true | Core.Type.TempPlayer[index].InShop > 0 | Core.Type.TempPlayer[index].InTrade > 0)
            {
                NetworkSend.PlayerMsg(index, "You can't use items while in a bank, shop, or trade!", (int) ColorType.BrightRed);
                return CanPlayerUseItemRet;
            }

            CanPlayerUseItemRet = 0;
            return CanPlayerUseItemRet;
        }

        internal static void UseItem(int index, int InvNum)
        {
            int itemNum;
            int i;
            int n;
            var tempitem = default(int);
            int m;
            var tempdata = new int[(int)(StatType.Count + 3 + 1)];
            var tempstr = new string[3];

            // Prevent hacking
            if (InvNum <= 0 | InvNum > Core.Constant.MAX_INV)
                return;

            itemNum = GetPlayerInv(index, InvNum);

            if (itemNum <= 0 | itemNum > Core.Constant.MAX_ITEMS)
                return;

            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(CanPlayerUseItem(index, itemNum), 0, false)))
                return;

            // Find out what kind of item it is
            switch (Core.Type.Item[itemNum].Type)
            {
                case (byte)ItemType.Equipment:
                    {
                        switch (Core.Type.Item[itemNum].SubType)
                        {
                            case (byte)EquipmentType.Weapon:
                                {

                                    if (Core.Type.Item[itemNum].TwoHanded > 0)
                                    {
                                        if (GetPlayerEquipment(index, EquipmentType.Shield) > 0)
                                        {
                                            NetworkSend.PlayerMsg(index, "This is a 2-Handed weapon! Please unequip your shield first.", (int) ColorType.BrightRed);
                                            return;
                                        }
                                    }

                                    if (GetPlayerEquipment(index, EquipmentType.Weapon) > 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Weapon);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Weapon);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);

                                    SetPlayerInv(index, InvNum, 0);
                                    SetPlayerInvValue(index, InvNum, 0);

                                    if (tempitem > 0) // give back the stored item
                                    {
                                        m = FindOpenInvSlot(index, tempitem);
                                        SetPlayerInv(index, m, tempitem);
                                        SetPlayerInvValue(index, m, 0);
                                    }

                                    NetworkSend.SendWornEquipment(index);
                                    NetworkSend.SendMapEquipment(index);
                                    NetworkSend.SendInventory(index);
                                    NetworkSend.SendInventoryUpdate(index, InvNum);
                                    NetworkSend.SendStats(index);

                                    // send vitals
                                    NetworkSend.SendVitals(index);
                                    break;
                                }

                            case (byte)EquipmentType.Armor:
                                {
                                    if (GetPlayerEquipment(index, EquipmentType.Armor) > 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Armor);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Armor);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 0);

                                    if (tempitem > 0) // Return their old equipment to their inventory.
                                    {
                                        m = FindOpenInvSlot(index, tempitem);
                                        SetPlayerInv(index, m, tempitem);
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
                                    if (GetPlayerEquipment(index, EquipmentType.Helmet) > 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Helmet);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Helmet);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem > 0) // give back the stored item
                                    {
                                        m = FindOpenInvSlot(index, tempitem);
                                        SetPlayerInv(index, m, tempitem);
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
                                    if (Core.Type.Item[GetPlayerEquipment(index, EquipmentType.Weapon)].TwoHanded > 0)
                                    {
                                        NetworkSend.PlayerMsg(index, "Please unequip your 2-handed weapon first.", (int) ColorType.BrightRed);
                                        return;
                                    }

                                    if (GetPlayerEquipment(index, EquipmentType.Shield) > 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Shield);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Shield);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem > 0) // give back the stored item
                                    {
                                        m = FindOpenInvSlot(index, tempitem);
                                        SetPlayerInv(index, m, tempitem);
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
                                    NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + Core.Type.Item[itemNum].Data1, (int) ColorType.BrightGreen, (byte) ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                    Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
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
                                    NetworkSend.SendActionMsg(GetPlayerMap(index), "+" + Core.Type.Item[itemNum].Data1, (int) ColorType.BrightBlue, (byte) ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                    Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
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
                                    Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
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
                                    Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
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
                            if (Conversions.ToBoolean(HasItem(index, Core.Type.Item[itemNum].Ammo)))
                            {
                                TakeInv(index, Core.Type.Item[itemNum].Ammo, 1);
                                Projectile.PlayerFireProjectile(index);
                            }
                            else
                            {
                                NetworkSend.PlayerMsg(index, "No More " + Core.Type.Item[Core.Type.Item[GetPlayerEquipment(index, EquipmentType.Weapon)].Ammo].Name + " !", (int) ColorType.BrightRed);
                                return;
                            }
                        }
                        else
                        {
                            Projectile.PlayerFireProjectile(index);
                            return;
                        }

                        break;
                    }

                case (byte)ItemType.CommonEvent:
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
                            case (byte)CommonEventType.CustomScript:
                                {
                                    Event.CustomScript(index, Core.Type.Item[itemNum].Data2, GetPlayerMap(index), n);
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
                        if (Core.Type.Item[itemNum].Stackable == 1)
                        {
                            TakeInv(index, itemNum, 1);
                        }
                        else
                        {
                            TakeInv(index, itemNum, 0);
                        }
                        n = Core.Type.Item[itemNum].Data1;
                        Pet.AdoptPet(index, n);
                        break;
                    }
            }
        }

        public static void PlayerLearnSkill(int index, int itemNum, int skillNum = 0)
        {
            int n;
            int i;

            // Get the skill num
            if (skillNum > 0)
            {
                n = skillNum;
            }
            else
            {
                n = Core.Type.Item[itemNum].Data1;
            }

            if (n < 1 | n > Core.Constant.MAX_SKILLS)
                return;

            if (n > 0)
            {
                // Make sure they are the right class
                if (Core.Type.Skill[n].JobReq == GetPlayerJob(index) | Core.Type.Skill[n].JobReq == 0)
                {
                    // Make sure they are the right level
                    i = Core.Type.Skill[n].LevelReq;

                    if (i <= GetPlayerLevel(index))
                    {
                        i = FindOpenSkill(index);

                        // Make sure they have an open skill slot
                        if (i > 0)
                        {

                            // Make sure they dont already have the skill
                            if (!HasSkill(index, n))
                            {
                                SetPlayerSkill(index, i, n);
                                Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                TakeInv(index, itemNum, 0);
                                NetworkSend.PlayerMsg(index, "You study the skill carefully.", (int) ColorType.Yellow);
                                NetworkSend.PlayerMsg(index, "You have learned a new skill!", (int) ColorType.BrightGreen);
                                NetworkSend.SendPlayerSkills(index);
                            }
                            else
                            {
                                NetworkSend.PlayerMsg(index, "You have already learned this skill!", (int) ColorType.BrightRed);
                            }
                        }
                        else
                        {
                            NetworkSend.PlayerMsg(index, "You have learned all that you can learn!", (int) ColorType.BrightRed);
                        }
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "You must be level " + i + " to learn this skill.", (int) ColorType.Yellow);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, string.Format("Only {0} can use this skill.", GameLogic.CheckGrammar(Core.Type.Job[Core.Type.Skill[n].JobReq].Name, 1)), (int) ColorType.BrightRed);
                }
            }
            else
            {
                NetworkSend.PlayerMsg(index, "This scroll is not connected to a skill, please inform an admin!", (int) ColorType.BrightRed);
            }
        }

        public static void PlayerSwitchInvSlots(int index, int OldSlot, int NewSlot)
        {
            int OldNum;
            int OldValue;
            int OldRarity;
            string OldPrefix;
            string OldSuffix;
            int OldSpeed;
            int OldDamage;
            int NewNum;
            int NewValue;
            int NewRarity;
            string NewPrefix;
            string NewSuffix;
            int NewSpeed;
            int NewDamage;

            if (OldSlot == 0 | NewSlot == 0)
                return;

            OldNum = GetPlayerInv(index, OldSlot);
            OldValue = GetPlayerInvValue(index, OldSlot);
            NewNum = GetPlayerInv(index, NewSlot);
            NewValue = GetPlayerInvValue(index, NewSlot);

            if (OldNum == NewNum & Core.Type.Item[NewNum].Stackable == 1) // same item, if we can stack it, lets do that :P
            {
                SetPlayerInv(index, NewSlot, NewNum);
                SetPlayerInvValue(index, NewSlot, OldValue + NewValue);
                SetPlayerInv(index, OldSlot, 0);
                SetPlayerInvValue(index, OldSlot, 0);
            }
            else
            {
                SetPlayerInv(index, NewSlot, OldNum);
                SetPlayerInvValue(index, NewSlot, OldValue);
                SetPlayerInv(index, OldSlot, NewNum);
                SetPlayerInvValue(index, OldSlot, NewValue);
            }

            NetworkSend.SendInventory(index);
        }

        public static void PlayerSwitchSkillSlots(int index, int OldSlot, int NewSlot)
        {
            int OldNum;
            int OldValue;
            int OldRarity;
            string OldPrefix;
            string OldSuffix;
            int OldSpeed;
            int OldDamage;
            int NewNum;
            int NewValue;
            int NewRarity;
            string NewPrefix;
            string NewSuffix;
            int NewSpeed;
            int NewDamage;

            if (OldSlot == 0 | NewSlot == 0)
                return;

            OldNum = GetPlayerSkill(index, OldSlot);
            OldValue = GetPlayerSkillCD(index, OldSlot);
            NewNum = GetPlayerSkill(index, NewSlot);
            NewValue = GetPlayerSkillCD(index, NewSlot);

            if (OldNum == NewNum & Core.Type.Item[NewNum].Stackable == 1) // same item, if we can stack it, lets do that :P
            {
                SetPlayerSkill(index, NewSlot, NewNum);
                SetPlayerSkillCD(index, NewSlot, NewValue);
                SetPlayerSkill(index, OldSlot, 0);
                SetPlayerSkillCD(index, OldSlot, 0);
            }
            else
            {
                SetPlayerSkill(index, NewSlot, OldNum);
                SetPlayerSkillCD(index, NewSlot, OldValue);
                SetPlayerSkill(index, OldSlot, NewNum);
                SetPlayerSkillCD(index, OldSlot, NewValue);
            }

            NetworkSend.SendPlayerSkills(index);
        }

        #endregion

        #region Equipment

        public static void CheckEquippedItems(int index)
        {
            int itemNum;
            int i;

            // We want to check incase an admin takes away an object but they had it equipped
            var loopTo = EquipmentType.Count - 1;
            for (i = 0; i <= (int)loopTo; i++)
            {
                itemNum = GetPlayerEquipment(index, (EquipmentType)i);

                if (itemNum > 0)
                {

                    switch (i)
                    {
                        case (byte)EquipmentType.Weapon:
                            {

                                if (Core.Type.Item[itemNum].SubType != (byte)EquipmentType.Weapon)
                                    SetPlayerEquipment(index, 0, (EquipmentType)i);
                                break;
                            }
                        case (byte)EquipmentType.Armor:
                            {

                                if (Core.Type.Item[itemNum].SubType != (byte)EquipmentType.Armor)
                                    SetPlayerEquipment(index, 0, (EquipmentType)i);
                                break;
                            }
                        case (byte)EquipmentType.Helmet:
                            {

                                if (Core.Type.Item[itemNum].SubType != (byte)EquipmentType.Helmet)
                                    SetPlayerEquipment(index, 0, (EquipmentType)i);
                                break;
                            }
                        case (byte)EquipmentType.Shield:
                            {

                                if (Core.Type.Item[itemNum].SubType != (byte)EquipmentType.Shield)
                                    SetPlayerEquipment(index, 0, (EquipmentType)i);
                                break;
                            }
                    }
                }
                else
                {
                    SetPlayerEquipment(index, 0, (EquipmentType)i);
                }

            }

        }

        public static void PlayerUnequipItem(int index, int EqSlot)
        {
            int i;
            int m;
            int itemnum;

            if (EqSlot < 1 | EqSlot > (byte)EquipmentType.Count - 1)
                return; // exit out early if error'd

            if (FindOpenInvSlot(index, GetPlayerEquipment(index, (EquipmentType)EqSlot)) > 0)
            {
                itemnum = GetPlayerEquipment(index, (EquipmentType)EqSlot);

                m = FindOpenInvSlot(index, Core.Type.Player[index].Equipment[EqSlot]);
                SetPlayerInv(index, m, Core.Type.Player[index].Equipment[EqSlot]);
                SetPlayerInvValue(index, m, 0);

                NetworkSend.PlayerMsg(index, "You unequip " + GameLogic.CheckGrammar(Core.Type.Item[GetPlayerEquipment(index, (EquipmentType)EqSlot)].Name), (int) ColorType.Yellow);

                // remove equipment
                SetPlayerEquipment(index, 0, (EquipmentType)EqSlot);
                NetworkSend.SendWornEquipment(index);
                NetworkSend.SendMapEquipment(index);
                NetworkSend.SendStats(index);
                NetworkSend.SendInventory(index);

                // send vitals
                NetworkSend.SendVitals(index);
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Your inventory is full.", (int) ColorType.BrightRed);
            }

        }

        #endregion

        #region Misc

        public static void JoinGame(int index)
        {
            int i;

            // Notify everyone that a player has joined the game.
            NetworkSend.GlobalMsg(string.Format("{0} has joined {1}!", GetPlayerName(index), Settings.GameName));

            // Warp the player to his saved location
            PlayerWarp(index, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index));

            // Send all the required game data to the user.
            CheckEquippedItems(index);
            NetworkSend.SendInventory(index);
            NetworkSend.SendWornEquipment(index);
            NetworkSend.SendMapEquipment(index);
            NetworkSend.SendExp(index);
            NetworkSend.SendHotbar(index);
            NetworkSend.SendPlayerSkills(index);
            NetworkSend.SendStats(index);
            NetworkSend.SendJoinMap(index);
            Pet.SendUpdatePlayerPet(index, true);
            Time.SendTimeTo(index);
            Time.SendGameClockTo(index);
            Pet.SendPets(index);
            Projectile.SendProjectiles(index);

            // Send welcome messages
            NetworkSend.SendWelcome(index);

            // Send the flag so they know they can start doing stuff
            NetworkSend.SendInGame(index);

            General.UpdateCaption();
        }

        public static void LeftGame(int index)
        {
            int i;
            int tradeTarget;

            if (Core.Type.TempPlayer[index].InGame)
            {
                NetworkSend.SendLeftMap(index);
                Core.Type.TempPlayer[index].InGame = false;

                // Check if the player was in a party, and if so cancel it out so the other player doesn't continue to get half exp
                // leave party.
                Party.Party_PlayerLeave(index);

                // cancel any trade they're in
                if (Core.Type.TempPlayer[index].InTrade > 0)
                {
                    tradeTarget = Core.Type.TempPlayer[index].InTrade;
                    NetworkSend.PlayerMsg(tradeTarget, string.Format("{0} has declined the trade.", GetPlayerName(index)), (int) ColorType.BrightRed);
                    // clear out trade
                    var loopTo = Core.Constant.MAX_INV;
                    for (i = 0; i <= (int)loopTo; i++)
                    {
                        Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Num = 0;
                        Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Value = 0;
                    }
                    Core.Type.TempPlayer[tradeTarget].InTrade = 0;
                    NetworkSend.SendCloseTrade(tradeTarget);
                }

                // Send a global message that he/she left
                NetworkSend.GlobalMsg(string.Format("{0} has left {1}!", GetPlayerName(index), Settings.GameName));

                Console.WriteLine(string.Format("{0} has left {1}!", GetPlayerName(index), Settings.GameName));

                Pet.RecallPet(index);
                Database.SaveCharacter(index, Core.Type.TempPlayer[index].Slot);
                Database.SaveBank(index);
            }

            Database.ClearAccount(index);
            General.UpdateCaption();
        }

        internal static void KillPlayer(int index)
        {
            int exp;

            // Calculate exp to give attacker
            exp = GetPlayerExp(index) / 3;

            // Make sure we dont get less then 0
            if (exp < 0)
                exp = 0;
            if (exp == 0)
            {
                NetworkSend.PlayerMsg(index, "You've lost no experience.", (int) ColorType.BrightGreen);
            }
            else
            {
                SetPlayerExp(index, GetPlayerExp(index) - exp);
                NetworkSend.SendExp(index);
                NetworkSend.PlayerMsg(index, string.Format("You've lost {0} experience.", exp), (int) ColorType.BrightRed);
            }

            OnDeath(index);
        }

        public static void OnDeath(int index)
        {
            // Set HP to nothing
            SetPlayerVital(index, VitalType.HP, 0);

            // Warp player away
            SetPlayerDir(index, (byte) DirectionType.Down);

            {
                ref var withBlock = ref Core.Type.Map[GetPlayerMap(index)];
                // to the bootmap if it is set
                if (withBlock.BootMap > 0)
                {
                    PlayerWarp(index, withBlock.BootMap, withBlock.BootX, withBlock.BootY);
                }
                else
                {
                    PlayerWarp(index, Core.Type.Job[GetPlayerJob(index)].StartMap, Core.Type.Job[GetPlayerJob(index)].StartX, Core.Type.Job[GetPlayerJob(index)].StartY);
                }
            }

            // Clear skill casting
            Core.Type.TempPlayer[index].SkillBuffer = 0;
            Core.Type.TempPlayer[index].SkillBufferTimer = 0;
            NetworkSend.SendClearSkillBuffer(index);

            // Restore vitals
            for (int i = 0, loopTo = (byte) VitalType.Count - 1; i <= (int)loopTo; i++)
                SetPlayerVital(index, (VitalType)i, GetPlayerMaxVital(index, (VitalType)i));

            NetworkSend.SendVitals(index);

            // If the player the attacker killed was a pk then take it away
            if (GetPlayerPK(index) == 1)
            {
                SetPlayerPK(index, Conversions.ToInteger(false));
                NetworkSend.SendPlayerData(index);
            }

        }

        public static int GetPlayerVitalRegen(int index, VitalType Vital)
        {
            int GetPlayerVitalRegenRet = default;
            var i = default(int);

            // Prevent subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | index < 0 | index > Core.Constant.MAX_PLAYERS)
            {
                GetPlayerVitalRegenRet = 0;
                return GetPlayerVitalRegenRet;
            }

            switch (Vital)
            {
                case VitalType.HP:
                    {
                        i = GetPlayerStat(index, StatType.Vitality) / 2;
                        break;
                    }
                case VitalType.SP:
                    {
                        i = GetPlayerStat(index, StatType.Spirit) / 2;
                        break;
                    }
            }

            if (i < 2)
                i = 2;
            GetPlayerVitalRegenRet = i;
            return GetPlayerVitalRegenRet;
        }

        internal static void HandleNPCKillExperience(int index, int NPCNum)
        {
            // Get the experience we'll have to hand out. If it's negative, just ignore this method.
            int Experience = Core.Type.NPC[NPCNum].Exp;
            if (Experience < 0)
                return;

            // Is our player in a party? If so, hand out exp to everyone.
            if (Party.IsPlayerInParty(index))
            {
                Party.Party_ShareExp(Party.GetPlayerParty(index), Experience, index, GetPlayerMap(index));
            }
            else
            {
                Event.GivePlayerExp(index, Experience);
            }
        }

        internal static void HandlePlayerKillExperience(int attacker, int victim)
        {
            // Calculate exp to give attacker
            var exp = GetPlayerExp(victim) / 10;

            // Make sure we dont get less then 0
            if (exp < 0)
            {
                exp = 0;
            }

            if (exp == 0)
            {
                NetworkSend.PlayerMsg(victim, "You've lost no exp.", (int) ColorType.BrightRed);
                NetworkSend.PlayerMsg(attacker, "You've received no exp.", (int) ColorType.BrightBlue);
            }
            else
            {
                SetPlayerExp(victim, GetPlayerExp(victim) - exp);
                NetworkSend.SendExp(victim);
                NetworkSend.PlayerMsg(victim, string.Format("You've lost {0} exp.", exp), (int) ColorType.BrightRed);

                // check if we're in a party
                if (Conversions.ToInteger(Party.IsPlayerInParty(attacker)) > 0)
                {
                    // pass through party exp share function
                    Party.Party_ShareExp(Party.GetPlayerParty(attacker), exp, attacker, GetPlayerMap(attacker));
                }
                else
                {
                    // not in party, get exp for self
                    Event.GivePlayerExp(attacker, exp);
                }
            }
        }

        #endregion

        #region Skills
        internal static void BufferSkill(int index, int SkillSlot)
        {
            int skillnum;
            int MPCost;
            int LevelReq;
            int mapNum;
            int SkillCastType;
            int JobReq;
            int AccessReq;
            int range;
            bool HasBuffered;

            TargetType TargetType;
            int Target;

            // Prevent subscript out of range
            if (SkillSlot < 0 | SkillSlot > Core.Constant.MAX_PLAYER_SKILLS)
                return;

            skillnum = GetPlayerSkill(index, SkillSlot);
            mapNum = GetPlayerMap(index);

            if (skillnum <= 0 | skillnum > Core.Constant.MAX_SKILLS)
                return;

            // Make sure player has the skill
            if (!HasSkill(index, skillnum))
                return;

            // see if cooldown has finished
            if (Core.Type.TempPlayer[index].SkillCD[SkillSlot] > General.GetTimeMs())
            {
                NetworkSend.PlayerMsg(index, "Skill hasn't cooled down yet!", (int) ColorType.Yellow);
                return;
            }

            MPCost = Core.Type.Skill[skillnum].MpCost;

            // Check if they have enough MP
            if (GetPlayerVital(index, VitalType.SP) < MPCost)
            {
                NetworkSend.PlayerMsg(index, "Not enough mana!", (int) ColorType.Yellow);
                return;
            }

            LevelReq = Core.Type.Skill[skillnum].LevelReq;

            // Make sure they are the right level
            if (LevelReq > GetPlayerLevel(index))
            {
                NetworkSend.PlayerMsg(index, "You must be level " + LevelReq + " to use this skill.", (int) ColorType.BrightRed);
                return;
            }

            AccessReq = Core.Type.Skill[skillnum].AccessReq;

            // make sure they have the right access
            if (AccessReq > GetPlayerAccess(index))
            {
                NetworkSend.PlayerMsg(index, "You must be an administrator to use this skill.", (int) ColorType.BrightRed);
                return;
            }

            JobReq = Core.Type.Skill[skillnum].JobReq;

            // make sure the JobReq > 0
            if (JobReq > 0) // 0 = no req
            {
                if (JobReq != GetPlayerJob(index))
                {
                    NetworkSend.PlayerMsg(index, "Only " + GameLogic.CheckGrammar(Core.Type.Job[JobReq].Name) + " can use this skill.", (int) ColorType.Yellow);
                    return;
                }
            }

            // find out what kind of skill it is! self cast, target or AOE
            if (Core.Type.Skill[skillnum].Range > 0)
            {
                // ranged attack, single target or aoe?
                if (!Core.Type.Skill[skillnum].IsAoE)
                {
                    SkillCastType = 2; // targetted
                }
                else
                {
                    SkillCastType = 3;
                } // targetted aoe
            }
            else if (!Core.Type.Skill[skillnum].IsAoE)
            {
                SkillCastType = 0; // self-cast
            }
            else
            {
                SkillCastType = 0;
            } // self-cast AoE

            TargetType = (TargetType)Core.Type.TempPlayer[index].TargetType;
            Target = Core.Type.TempPlayer[index].Target;
            range = Core.Type.Skill[skillnum].Range;
            HasBuffered = Conversions.ToBoolean(0);

            switch (SkillCastType)
            {
                case 0:
                case 1: // self-cast & self-cast AOE
                    {
                        HasBuffered = Conversions.ToBoolean(1);
                        break;
                    }
                case 2:
                case 3: // targeted & targeted AOE
                    {
                        // check if have target
                        if (!(Target > 0))
                        {
                            NetworkSend.PlayerMsg(index, "You do not have a target.", (int) ColorType.BrightRed);
                        }
                        if (TargetType == TargetType.Player)
                        {
                            // if have target, check in range
                            if (!IsInRange(range, GetPlayerX(index), GetPlayerY(index), GetPlayerX(Target), GetPlayerY(Target)))
                            {
                                NetworkSend.PlayerMsg(index, "Target not in range.", (int) ColorType.BrightRed);
                            }
                            // go through skill Type
                            else if (Core.Type.Skill[skillnum].Type != (byte)SkillType.DamageHp & Core.Type.Skill[skillnum].Type != (byte)SkillType.DamageMp)
                            {
                                HasBuffered = Conversions.ToBoolean(1);
                            }
                            else if (CanPlayerAttackPlayer(index, Target, true))
                            {
                                HasBuffered = Conversions.ToBoolean(1);
                            }
                        }
                        else if (TargetType == TargetType.NPC)
                        {
                            // if have target, check in range
                            if (!IsInRange(range, GetPlayerX(index), GetPlayerY(index), Core.Type.MapNPC[mapNum].NPC[Target].X, Core.Type.MapNPC[mapNum].NPC[Target].Y))
                            {
                                NetworkSend.PlayerMsg(index, "Target not in range.", (int) ColorType.BrightRed);
                                HasBuffered = Conversions.ToBoolean(0);
                            }
                            // go through skill Type
                            else if (Core.Type.Skill[skillnum].Type != (byte)SkillType.DamageHp & Core.Type.Skill[skillnum].Type != (byte)SkillType.DamageMp)
                            {
                                HasBuffered = Conversions.ToBoolean(1);
                            }
                            else if (CanPlayerAttackNPC(index, Target, true))
                            {
                                HasBuffered = Conversions.ToBoolean(1);
                            }
                        }

                        break;
                    }
            }

            if (HasBuffered)
            {
                Animation.SendAnimation(mapNum, Core.Type.Skill[skillnum].CastAnim, 0, 0, (byte)TargetType.Player, index);
                Core.Type.TempPlayer[index].SkillBuffer = SkillSlot;
                Core.Type.TempPlayer[index].SkillBufferTimer = General.GetTimeMs();
                return;
            }
            else
            {
                NetworkSend.SendClearSkillBuffer(index);
            }
        }



        #endregion

        #region Bank

        public static void GiveBank(int index, int InvSlot, int Amount)
        {
            int BankSlot;
            int itemnum;

            if (InvSlot <= 0 | InvSlot > Core.Constant.MAX_INV)
                return;

            if (Amount <= 0)
                Amount = 0;

            if (GetPlayerInvValue(index, InvSlot) < 0)
                return;
            if (GetPlayerInvValue(index, InvSlot) < Amount & GetPlayerInv(index, InvSlot) == 0)
                return;

            BankSlot = FindOpenBankSlot(index, GetPlayerInv(index, InvSlot));
            itemnum = GetPlayerInv(index, InvSlot);

            if (BankSlot > 0)
            {
                if (Core.Type.Item[GetPlayerInv(index, InvSlot)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerInv(index, InvSlot)].Stackable == 1)
                {
                    if (GetPlayerBank(index, (byte)BankSlot) == GetPlayerInv(index, InvSlot))
                    {
                        SetPlayerBankValue(index, (byte)BankSlot, GetPlayerBankValue(index, (byte)BankSlot) + Amount);
                        TakeInv(index, GetPlayerInv(index, InvSlot), Amount);
                    }
                    else
                    {
                        SetPlayerBank(index, (byte)BankSlot, GetPlayerInv(index, InvSlot));
                        SetPlayerBankValue(index, (byte)BankSlot, Amount);
                        TakeInv(index, GetPlayerInv(index, InvSlot), Amount);
                    }
                }
                else if (GetPlayerBank(index, (byte)BankSlot) == GetPlayerInv(index, InvSlot))
                {
                    SetPlayerBankValue(index, (byte)BankSlot, GetPlayerBankValue(index, (byte)BankSlot) + 1);
                    TakeInv(index, GetPlayerInv(index, InvSlot), 0);
                }
                else
                {
                    SetPlayerBank(index, (byte)BankSlot, itemnum);
                    SetPlayerBankValue(index, (byte)BankSlot, 1);
                    TakeInv(index, GetPlayerInv(index, InvSlot), 0);
                }

                NetworkSend.SendBank(index);
            }

        }

        public static int GetPlayerBank(int index, byte BankSlot)
        {
            int GetPlayerBankRet = default;
            GetPlayerBankRet = Bank[index].Item[BankSlot].Num;
            return GetPlayerBankRet;
        }

        public static void SetPlayerBank(int index, byte BankSlot, int ItemNum)
        {
            Bank[index].Item[BankSlot].Num = ItemNum;
        }

        public static int GetPlayerBankValue(int index, byte BankSlot)
        {
            int GetPlayerBankValueRet = default;
            GetPlayerBankValueRet = Bank[index].Item[BankSlot].Value;
            return GetPlayerBankValueRet;
        }

        public static void SetPlayerBankValue(int index, byte BankSlot, int Value)
        {
            Bank[index].Item[BankSlot].Value = Value;
        }

        public static byte FindOpenBankSlot(int index, int ItemNum)
        {
            byte FindOpenBankSlotRet = default;
            int i;

            if (!NetworkConfig.IsPlaying(index))
                return FindOpenBankSlotRet;
            if (ItemNum <= 0 | ItemNum > Core.Constant.MAX_ITEMS)
                return FindOpenBankSlotRet;

            if (Core.Type.Item[ItemNum].Type == (byte)ItemType.Currency | Core.Type.Item[ItemNum].Stackable == 1)
            {
                var loopTo = Core.Constant.MAX_BANK;
                for (i = 0; i <= (int)loopTo; i++)
                {
                    if (GetPlayerBank(index, (byte)i) == ItemNum)
                    {
                        FindOpenBankSlotRet = (byte)i;
                        return FindOpenBankSlotRet;
                    }
                }
            }

            var loopTo1 = Core.Constant.MAX_BANK;
            for (i = 0; i <= (int)loopTo1; i++)
            {
                if (GetPlayerBank(index, (byte)i) == 0)
                {
                    FindOpenBankSlotRet = (byte)i;
                    return FindOpenBankSlotRet;
                }
            }

            return FindOpenBankSlotRet;

        }

        public static void TakeBank(int index, int BankSlot, int Amount)
        {
            object invSlot;

            if (BankSlot <= 0 | BankSlot > Core.Constant.MAX_BANK)
                return;

            if (Amount <= 0)
                Amount = 0;

            if (GetPlayerBankValue(index, (byte)BankSlot) < Amount)
                return;

            invSlot = FindOpenInvSlot(index, GetPlayerBank(index, (byte)BankSlot));

            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(invSlot, 0, false)))
            {
                if (Core.Type.Item[GetPlayerBank(index, (byte)BankSlot)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerBank(index, (byte)BankSlot)].Stackable == 1)
                {
                    GiveInv(index, GetPlayerBank(index, (byte)BankSlot), Amount);
                    SetPlayerBankValue(index, (byte)BankSlot, GetPlayerBankValue(index, (byte)BankSlot) - Amount);
                    if (GetPlayerBankValue(index, (byte)BankSlot) <= 0)
                    {
                        SetPlayerBank(index, (byte)BankSlot, 0);
                        SetPlayerBankValue(index, (byte)BankSlot, 0);
                    }
                }
                else if (GetPlayerBank(index, (byte)BankSlot) == GetPlayerInv(index, (int)invSlot))
                {
                    if (GetPlayerBankValue(index, (byte)BankSlot) > 1)
                    {
                        GiveInv(index, GetPlayerBank(index, (byte)BankSlot), 0);
                        SetPlayerBankValue(index, (byte)BankSlot, GetPlayerBankValue(index, (byte)BankSlot) - 1);
                    }
                }
                else
                {
                    GiveInv(index, GetPlayerBank(index, (byte)BankSlot), 0);
                    SetPlayerBank(index, (byte)BankSlot, 0);
                    SetPlayerBankValue(index, (byte)BankSlot, 0);
                }

            }

            NetworkSend.SendBank(index);
        }

        public static void PlayerSwitchBankSlots(int index, int OldSlot, int NewSlot)
        {
            int OldNum;
            int OldValue;
            int NewNum;
            int NewValue;
            int i;

            if (OldSlot == 0 | NewSlot == 0)
                return;

            OldNum = GetPlayerBank(index, (byte)OldSlot);
            OldValue = GetPlayerBankValue(index, (byte)OldSlot);
            NewNum = GetPlayerBank(index, (byte)NewSlot);
            NewValue = GetPlayerBankValue(index, (byte)NewSlot);

            SetPlayerBank(index, (byte)NewSlot, OldNum);
            SetPlayerBankValue(index, (byte)NewSlot, OldValue);

            SetPlayerBank(index, (byte)OldSlot, NewNum);
            SetPlayerBankValue(index, (byte)OldSlot, NewValue);

            NetworkSend.SendBank(index);
        }

        #endregion

    }
}