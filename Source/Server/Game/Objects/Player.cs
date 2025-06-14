using Core;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using System;
using System.Reflection;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Packets;
using static Core.Type;

namespace Server
{

    public class Player
    {

        public static bool CanPlayerAttackPlayer(int attacker, int victim, bool IsSkill = false)
        {
            try
            {
                bool i;
                i = Script.Instance?.CanPlayerAttackPlayer(attacker, victim, IsSkill);
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public static bool CanPlayerBlockHit(int index)
        {
            try
            {
                bool i;
                i = Script.Instance?.CanPlayerBlockHit(index);
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public static bool CanPlayerCriticalHit(int index)
        {
            try
            {
                bool i;
                i = Script.Instance?.CanPlayerCriticialHit(index);
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public static int GetPlayerDamage(int index)
        {
            try
            {
                int i;
                i = Script.Instance?.GetPlayerDamage(index);
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        public static int GetPlayerProtection(int index)
        {
            try
            {
                int i;
                i = Script.Instance?.GetPlayerProtection(index);
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        public static void AttackPlayer(int attacker, int victim, int damage, int skillNum = 0, int NPCNum = 0)
        {
            try
            {
                Script.Instance?.AttackPlayer(attacker, victim);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void StunPlayer(int index, int skillNum)
        {
            try
            {
                Script.Instance?.StunPlayer(index, skillNum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool CanPlayerAttackNPC(int attacker, int MapNPCNum, bool IsSkill = false)
        {
            try
            {
                bool i;
                i = Script.Instance?.CanPlayerAttackNPC(attacker, MapNPCNum, IsSkill);
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public static void StunNPC(int index, int mapNum, int skillNum)
        {
            try
            {
                Script.Instance?.StunNPC(index, mapNum, Skill);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void PlayerAttackNPC(int attacker, int MapNPCNum, int Damage)
        {
            try
            {
                Script.Instance?.PlayerAttackNPC(attacker, MapNPCNum, Damage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool IsInRange(int range, int x1, int y1, int x2, int y2)
        {
            bool IsInRangeRet = default;
            int nVal;
            IsInRangeRet = false;
            nVal = (int)Math.Round(Math.Sqrt(Math.Pow(x1 - x2, 2d) + Math.Pow(y1 - y2, 2d)));
            if (nVal <= range)
                IsInRangeRet = Conversions.ToBoolean(1);
            return IsInRangeRet;
        }

        public static bool CanPlayerDodge(int index)
        {
            bool CanPlayerDodgeRet = default;
            int rate;
            int rndNum;

            CanPlayerDodgeRet = false;

            rate = GetPlayerStat(index, StatType.Luck) / 4;
            rndNum = (int)Math.Round(General.GetRandom.NextDouble(1d, 100d));

            if (rndNum <= rate)
            {
                CanPlayerDodgeRet = Conversions.ToBoolean(1);
            }

            return CanPlayerDodgeRet;

        }

        public static bool CanPlayerParry(int index)
        {
            bool CanPlayerParryRet = default;
            int rate;
            int rndNum;

            CanPlayerParryRet = Conversions.ToBoolean(0);

            rate = GetPlayerStat(index, StatType.Luck) / 6;
            rndNum = (int)Math.Round(General.GetRandom.NextDouble(1d, 100d));

            if (rndNum <= rate)
            {
                CanPlayerParryRet = Conversions.ToBoolean(1);
            }

            return CanPlayerParryRet;

        }

        public static void TryPlayerAttackPlayer(int attacker, int victim)
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

                    var loopTo = EquipmentType.Count;
                    for (i = 0; i < (int)loopTo; i++)
                    {
                        if (GetPlayerEquipment(victim, (EquipmentType)i) >= 0)
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
            if (NetworkConfig.IsPlaying(attacker) == false | Conversions.ToInteger(NetworkConfig.IsPlaying(victim)) == 0 | Damage < 0)
            {
                return;
            }

            // Check if our assailant has a weapon.
            int Weapon = 0;
            if (GetPlayerEquipment(attacker, EquipmentType.Weapon) >= 0)
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

        public static void TryPlayerAttackNPC(int index, int MapNPCNum)
        {
            try
            {
                Script.Instance?.TryPlayerAttackNPC(index, MapNPCNum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool IsPlayerDead(int index)
        {
            bool IsPlayerDeadRet = false;
            IsPlayerDeadRet = false;
            if (index < 0 | index >= Core.Constant.MAX_PLAYERS | !Core.Type.TempPlayer[index].InGame)
                return IsPlayerDeadRet;
            if (GetPlayerVital(index, VitalType.HP) < 0)
                IsPlayerDeadRet = true;
            return IsPlayerDeadRet;
        }

        public static void HandlePlayerKillPlayer(int attacker, int victim)
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

        public static void HandlePlayerKillNPC(int mapNum, int index, int MapNPCNum)
        {
            // Set our attacker's target to nothing.
            NetworkSend.SendTarget(index, 0, 0);

            // Hand out player experience
            HandleNPCKillExperience(index, (int)Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Num);

            // Drop items if we can.
            NPC.DropNPCItems(mapNum, MapNPCNum);

            // Set our NPC's data to default so we know it's dead.
            Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Num = -1;
            Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].SpawnWait = General.GetTimeMs();
            Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Vital[(byte) VitalType.HP] = 0;

            // Notify all our clients that the NPC has died.
            NPC.SendNPCDead(mapNum, (int)MapNPCNum);

            // Check if our dead NPC is targeted by another player and remove their targets.
            foreach (var p in Core.Type.TempPlayer
                .Select((x, i) => new { Player = x, Index = i })
                .Where(x => x.Player.InGame
                            && GetPlayerMap(x.Index + 1) == mapNum
                            && x.Player.TargetType == (byte)TargetType.NPC
                            && x.Player.Target == MapNPCNum)
                .Select(x => x.Index + 1))
            {
                Core.Type.TempPlayer[p].Target = 0;
                Core.Type.TempPlayer[p].TargetType = 0;
            }

        }

        public static void HandlePlayerKilledPK(int attacker, int victim)
        {
            // TODO: Redo this method, it is horrendous.
            int z;
            var eqcount = default(int);
            int invcount = default, j = default;
            if (GetPlayerPK(victim) == false)
            {
                if (GetPlayerPK(attacker) == false)
                {
                    SetPlayerPK(attacker, true);
                    NetworkSend.SendPlayerData(attacker);
                    NetworkSend.GlobalMsg(GetPlayerName(attacker) + " has been deemed a Player Killer!!!");
                }
            }
            else
            {
                NetworkSend.GlobalMsg(GetPlayerName(victim) + " has paid the price for being a Player Killer!!!");
            }

            if ((int)Core.Type.Map[GetPlayerMap(victim)].Moral >= 0)
            {
                if (Core.Type.Moral[Core.Type.Map[GetPlayerMap(victim)].Moral].DropItems)
                {
                    if (GetPlayerLevel(victim) >= 10)
                    {

                        var loopTo = Core.Constant.MAX_INV;
                        for (z = 0; z < (int)loopTo; z++)
                        {
                            if (GetPlayerInv(victim, z) > 0)
                            {
                                invcount += 0;
                            }
                        }

                        var loopTo1 = EquipmentType.Count;
                        for (z = 0; z < (int)loopTo1; z++)
                        {
                            if (GetPlayerEquipment(victim, (EquipmentType)z) > 0)
                            {
                                eqcount += 0;
                            }
                        }
                        z = (int)Math.Round(General.GetRandom.NextDouble(1d, invcount + eqcount));

                        if (z == 0)
                            z = 0;
                        if (z > invcount + eqcount)
                            z = invcount + eqcount;
                        if (z > invcount)
                        {
                            z -= invcount;

                            for (int x = 0, loopTo2 = (int)(EquipmentType.Count); x < (int)loopTo2; x++)
                            {
                                if (GetPlayerEquipment(victim, (EquipmentType)x) >= 0)
                                {
                                    j += 0;

                                    if (j == z)
                                    {
                                        // Here it is, drop this piece of equipment!
                                        NetworkSend.PlayerMsg(victim, "In death you lost grip on your " + Core.Type.Item[GetPlayerEquipment(victim, (EquipmentType)x)].Name, (int) ColorType.BrightRed);
                                        Item.SpawnItem(GetPlayerEquipment(victim, (EquipmentType)x), 1, GetPlayerMap(victim), GetPlayerX(victim), GetPlayerY(victim));
                                        SetPlayerEquipment(victim, -1, (EquipmentType)x);
                                        NetworkSend.SendWornEquipment(victim);
                                        NetworkSend.SendMapEquipment(victim);
                                    }
                                }
                            }
                        }
                        else
                        {

                            for (int x = 1, loopTo3 = Core.Constant.MAX_INV; x < (int)loopTo3; x++)
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
            try
            {
                Script.Instance?.CheckPlayerLevelUp(index);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Incoming Packets

        public static void HandleUseChar(int index)
        {
            // Set the flag so we know the person is in the game
            Core.Type.TempPlayer[index].InGame = true;

            // Send an ok to client to start receiving in game data
            NetworkSend.SendLoginOK(index);
            JoinGame(index);
            string text = string.Format("{0} | {1} has began playing {2}.", GetPlayerLogin(index), GetPlayerName(index), SettingsManager.Instance.GameName);
            Core.Log.Add(text, Constant.PLAYER_LOG);
            Console.WriteLine(text);
            
        }

        #endregion

        #region Outgoing Packets

        public static void SendLeaveMap(int index, int mapNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SLeftMap);
            buffer.WriteInt32(index);
            NetworkConfig.SendDataToMapBut(index, mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        #endregion

        #region Movement
        public static void PlayerWarp(int index, int mapNum, int x, int y, int dir)
        {
            int OldMap;
            int i;
            ByteStream buffer;

            // Check for subscript out of range
            if (NetworkConfig.IsPlaying(index) == false | mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return;

            // Check if you are out of bounds
            if (x > Core.Type.Map[mapNum].MaxX)
                x = Core.Type.Map[mapNum].MaxX;

            if (y > Core.Type.Map[mapNum].MaxY)
                y = Core.Type.Map[mapNum].MaxY;

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

            if (OldMap != mapNum)
            {
                SendLeaveMap(index, OldMap);
            }

            SetPlayerMap(index, mapNum);
            SetPlayerX(index, x);
            SetPlayerY(index, y);
            SetPlayerDir(index, dir);

            if (Pet.PetAlive(index))
            {
                Pet.SetPetX(index, x);
                Pet.SetPetY(index, y);
                Core.Type.TempPlayer[index].PetTarget = 0;
                Core.Type.TempPlayer[index].PetTargetType = 0;
                Pet.SendPetXy(index, x, y);
            }

            NetworkSend.SendPlayerXY(index);

            // send equipment of all people on new map
            if (GameLogic.GetTotalMapPlayers(mapNum) > 0)
            {
                var loopTo = NetworkConfig.Socket.HighIndex;
                for (i = 0; i < loopTo; i++)
                {
                    if (NetworkConfig.IsPlaying(i))
                    {
                        if (GetPlayerMap(i) == mapNum)
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
                var loopTo1 = Core.Constant.MAX_MAP_NPCS;
                for (i = 0; i < loopTo1; i++)
                {
                    if (Core.Type.MapNPC[OldMap].NPC[i].Num >= 0)
                    {
                        Core.Type.MapNPC[OldMap].NPC[i].Vital[(byte) VitalType.HP] = GameLogic.GetNPCMaxVital((int)Core.Type.MapNPC[OldMap].NPC[i].Num, VitalType.HP);
                    }

                }

            }

            // Sets it so we know to process npcs on the map
            PlayersOnMap[mapNum] = true;
            Core.Type.TempPlayer[index].GettingMap = true;

            Moral.SendUpdateMoralTo(index, Core.Type.Map[mapNum].Moral);

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SCheckForMap);
            buffer.WriteInt32(mapNum);
            buffer.WriteInt32(Core.Type.Map[mapNum].Revision);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

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
            if (Dir < (int)DirectionType.Up || Dir > (int)DirectionType.DownRight || Movement < (int)MovementType.Standing || Movement > (int)MovementType.Running)
            {
                return;
            }

            // Prevent player from moving if they have casted a skill
            if (Core.Type.TempPlayer[index].SkillBuffer >= 0)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            // Cant move if in the bank
            if (Core.Type.TempPlayer[index].InBank)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            // if stunned, stop them moving
            if (Core.Type.TempPlayer[index].StunDuration > 0)
            {
                NetworkSend.SendPlayerXY(index);
                return;
            }

            if (Core.Type.TempPlayer[index].InShop >= 0 || Core.Type.TempPlayer[index].InBank)
            {
                return;
            }

            SetPlayerDir(index, Dir);
            Moved = false;
            mapNum = GetPlayerMap(index);

            switch ((DirectionType)Dir)
            {
                case DirectionType.Up:
                    if (GetPlayerY(index) > 0)
                    {
                        x = GetPlayerX(index);
                        y = GetPlayerY(index) - 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.Up))
                        {
                            break;
                        }

                        SetPlayerY(index, GetPlayerY(index) - 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo2 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo2; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    else if (Core.Type.Map[mapNum].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing && Core.Type.Map[mapNum].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                    {
                        if (Core.Type.Map[GetPlayerMap(index)].Up > 0)
                        {
                            NewMapY = Core.Type.Map[Core.Type.Map[GetPlayerMap(index)].Up].MaxY;
                            PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Up, GetPlayerX(index), NewMapY, (int)DirectionType.Up);
                            DidWarp = true;
                            Moved = true;
                        }
                    }
                    break;

                case DirectionType.Down:
                    if (GetPlayerY(index) < Core.Type.Map[mapNum].MaxY - 1)
                    {
                        x = GetPlayerX(index);
                        y = GetPlayerY(index) + 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.Down))
                        {
                            break;
                        }

                        SetPlayerY(index, GetPlayerY(index) + 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo1 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo1; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing && Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                    {
                        if (Core.Type.Map[GetPlayerMap(index)].Down > 0)
                        {
                            PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Down, GetPlayerX(index), 0, (int)DirectionType.Down);
                            DidWarp = true;
                            Moved = true;
                        }
                    }
                    break;

                case DirectionType.Left:
                    if (GetPlayerX(index) > 0)
                    {
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index);

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.Left))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) - 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo2 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo2; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing && Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                    {
                        if (Core.Type.Map[GetPlayerMap(index)].Left > 0)
                        {
                            NewMapX = Core.Type.Map[Core.Type.Map[GetPlayerMap(index)].Left].MaxX;
                            PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Left, NewMapX, GetPlayerY(index), (int)DirectionType.Left);
                            DidWarp = true;
                            Moved = true;
                        }
                    }
                    break;

                case DirectionType.Right:
                    if (GetPlayerX(index) < Core.Type.Map[mapNum].MaxX - 1)
                    {
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index);

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.Right))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) + 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo3 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo3; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    else if (Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type != TileType.NoXing && Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)].Type2 != TileType.NoXing)
                    {
                        if (Core.Type.Map[GetPlayerMap(index)].Right > 0)
                        {
                            PlayerWarp(index, Core.Type.Map[GetPlayerMap(index)].Right, 0, GetPlayerY(index), (int)DirectionType.Right);
                            DidWarp = true;
                            Moved = true;
                        }
                    }
                    break;

                case DirectionType.UpRight:
                    if (GetPlayerY(index) > 0 && GetPlayerX(index) < Core.Type.Map[mapNum].MaxX - 1)
                    {
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index) - 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.UpRight))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) + 1);
                        SetPlayerY(index, GetPlayerY(index) - 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo4 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo4; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    break;

                case DirectionType.UpLeft:
                    if (GetPlayerY(index) > 0 && GetPlayerX(index) > 0)
                    {
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index) - 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.UpLeft))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) - 1);
                        SetPlayerY(index, GetPlayerY(index) - 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo5 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo5; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    break;

                case DirectionType.DownRight:
                    if (GetPlayerY(index) < Core.Type.Map[mapNum].MaxY - 1 && GetPlayerX(index) < Core.Type.Map[mapNum].MaxX - 1)
                    {
                        x = GetPlayerX(index) + 1;
                        y = GetPlayerY(index) + 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.DownRight))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) + 1);
                        SetPlayerY(index, GetPlayerY(index) + 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo6 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo6; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    break;

                case DirectionType.DownLeft:
                    if (GetPlayerY(index) < Core.Type.Map[mapNum].MaxY - 1 && GetPlayerX(index) > 0)
                    {
                        x = GetPlayerX(index) - 1;
                        y = GetPlayerY(index) + 1;

                        if (IsTileBlocked(index, mapNum, x, y, DirectionType.DownLeft))
                        {
                            break;
                        }

                        SetPlayerX(index, GetPlayerX(index) - 1);
                        SetPlayerY(index, GetPlayerY(index) + 1);
                        NetworkSend.SendPlayerMove(index, Movement);
                        Moved = true;

                        for (int i = 0, loopTo7 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo7; i++)
                            EventLogic.TriggerEvent(index, i, 1, GetPlayerX(index), GetPlayerY(index));
                    }
                    break;
            }

            if (GetPlayerX(index) >= 0 && GetPlayerY(index) >= 0 && GetPlayerX(index) < Map[GetPlayerMap(index)].MaxX && GetPlayerY(index) < Map[GetPlayerMap(index)].MaxY)
            {
                ref var withBlock = ref Core.Type.Map[GetPlayerMap(index)].Tile[GetPlayerX(index), GetPlayerY(index)];
                mapNum = -1;
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

                if (mapNum >= 0)
                {
                    PlayerWarp(index, (int)mapNum, x, y, (int)DirectionType.Down);

                    DidWarp = Conversions.ToBoolean(1);
                    Moved = Conversions.ToBoolean(1);
                }

                x = -1;
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

                if (x >= 0) // shop exists?
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
                    Moved = true;
                }

            }

            // They tried to hack
            if (Moved == false | ExpectingWarp & !DidWarp)
            {
                PlayerWarp(index, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index), (byte)Core.Enum.DirectionType.Down);
            }

            x = GetPlayerX(index);
            y = GetPlayerY(index);

            if (Moved)
            {
                if (Core.Type.TempPlayer[index].EventMap.CurrentEvents > 0)
                {
                    for (int i = 0, loopTo8 = Core.Type.TempPlayer[index].EventMap.CurrentEvents; i < loopTo8; i++)
                    {
                        begineventprocessing = Conversions.ToBoolean(0);

                        if (Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId >= 0)
                        {
                            if ((int)Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Globals == 1)
                            {
                                if (Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].X == x & Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Y == y & (int)Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId].Trigger == 1 & Core.Type.TempPlayer[index].EventMap.EventPages[i].Visible == true)
                                    begineventprocessing = Conversions.ToBoolean(1);
                            }
                            else if (Core.Type.TempPlayer[index].EventMap.EventPages[i].X == x & Core.Type.TempPlayer[index].EventMap.EventPages[i].Y == y & (int)Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId].Trigger == 1 & Core.Type.TempPlayer[index].EventMap.EventPages[i].Visible == true)
                                begineventprocessing = Conversions.ToBoolean(1);
                          
                            if (Conversions.ToInteger(begineventprocessing) == 1)
                            {
                                // Process this event, it is on-touch and everything checks out.
                                if (Core.Type.Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId].CommandListCount > 0)
                                {
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].Active = 0;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].ActionTimer = General.GetTimeMs();
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].CurList = 0;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].CurSlot = 0;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].EventId = Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].PageId = Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId;
                                    Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId].WaitingForResponse = 0;

                                    int EventId = Core.Type.TempPlayer[index].EventMap.EventPages[i].EventId;
                                    int PageId = Core.Type.TempPlayer[index].EventMap.EventPages[i].PageId;
                                    int commandListCount = Core.Type.Map[GetPlayerMap(index)].Event[EventId].Pages[PageId].CommandListCount;

                                    Array.Resize(ref Core.Type.TempPlayer[index].EventProcessing[EventId].ListLeftOff, commandListCount);
                                }
                                begineventprocessing = false;
                            }
                        }
                    }
                }
            }
        }


        public static bool IsTileBlocked(int index, int mapNum, int x, int y, DirectionType dir)
        {      
            // Check for NPC and player blocking  
            var loopTo = NetworkConfig.Socket.HighIndex;
            for (int i = 0; i < loopTo; i++)
            {
                if (Core.Type.Moral[Map[mapNum].Moral].PlayerBlock)
                {
                    if (NetworkConfig.IsPlaying(i) & GetPlayerMap(i) == mapNum)
                    {
                        if (GetPlayerX(i) == x && GetPlayerY(i) == y)
                        {
                            return true;
                        }
                    }
                }
            }

            var loopTo2 = Core.Constant.MAX_MAP_NPCS;
            for (int i = 0; i < loopTo2; i++)
            {
                if (Core.Type.Moral[Core.Type.Map[mapNum].Moral].NPCBlock)
                {
                    if (Core.Type.MapNPC[mapNum].NPC[i].Num >= 0)
                    {
                        if (Core.Type.MapNPC[mapNum].NPC[i].X == x && Core.Type.MapNPC[mapNum].NPC[i].Y == y)
                        {
                            return true;
                        }
                    }
                }
            }

            // Check to make sure that the tile is walkable  
            if (IsDirBlocked(ref Map[mapNum].Tile[x, y].DirBlock, (byte)dir))
            {
                return true;
            }

            if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.Blocked || Core.Type.Map[mapNum].Tile[x, y].Type2 == TileType.Blocked)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Inventory

        public static int HasItem(int index, int itemNum)
        {
            int HasItemRet = default;
            int i;

            // Check for subscript out of range
            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                return HasItemRet;
            }

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo; i++)
            {
                // Check to see if the player has the item
                if (GetPlayerInv(index, i) == itemNum)
                {
                    if (Core.Type.Item[itemNum].Type == (byte)ItemType.Currency | Core.Type.Item[itemNum].Stackable == 1)
                    {
                        HasItemRet += GetPlayerInvValue(index, i);
                    }
                    else
                    {
                        HasItemRet += 1;
                    }
                }
            }

            return HasItemRet;

        }

        public static int FindItemSlot(int index, int itemNum)
        {
            int FindItemSlotRet = default;
            int i;

            FindItemSlotRet = 0;

            // Check for subscript out of range
            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                return FindItemSlotRet;
            }

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo; i++)
            {
                // Check to see if the player has the item
                if (GetPlayerInv(index, i) == itemNum)
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

            var loopTo = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                // See if theres even an item here
                if (Core.Type.MapItem[mapNum, i].Num >= 0 & Core.Type.MapItem[mapNum, i].Num < Core.Constant.MAX_ITEMS)
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
                                n = FindOpenInvSlot(index, (int)Core.Type.MapItem[mapNum, i].Num);

                                // Open slot available?
                                if (n != -1)
                                {
                                    // Set item in players inventor
                                    itemnum = (int)MapItem[mapNum, i].Num;

                                    SetPlayerInv(index, n, (int)MapItem[mapNum, i].Num);

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
                                    Item.SpawnItemSlot(i, -1, 0, GetPlayerMap(index), MapItem[mapNum, i].X, MapItem[mapNum, i].Y);
                                    NetworkSend.SendInventoryUpdate(index, n);                                 
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

        public static bool CanPlayerPickupItem(int index, int mapitemNum)
        {
            bool CanPlayerPickupItemRet = default;
            int mapNum;

            mapNum = GetPlayerMap(index);

            if (Core.Type.Map[mapNum].Moral >= 0)
            {
                if (Core.Type.Moral[Core.Type.Map[mapNum].Moral].CanPickupItem)
                {
                    // no lock or locked to player?
                    if (string.IsNullOrEmpty(Core.Type.MapItem[mapNum, mapitemNum].PlayerName) | Core.Type.MapItem[mapNum, mapitemNum].PlayerName == GetPlayerName(index))
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

        public static int FindOpenInvSlot(int index, int itemNum)
        {
            int FindOpenInvSlotRet = default;
            int i;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                return FindOpenInvSlotRet;
            }

            if (Core.Type.Item[itemNum].Type == (byte)ItemType.Currency | Core.Type.Item[itemNum].Stackable == 1)
            {
                // If currency then check to see if they already have an instance of the item and add it to that
                var loopTo = Core.Constant.MAX_INV;
                for (i = 0; i < loopTo; i++)
                {
                    if (GetPlayerInv(index, i) == itemNum)
                    {
                        FindOpenInvSlotRet = i;
                        return FindOpenInvSlotRet;
                    }
                }
            }

            var loopTo1 = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo1; i++)
            {
                // Try to find an open free slot
                if (GetPlayerInv(index, i) == -1)
                {
                    FindOpenInvSlotRet = i;
                    return FindOpenInvSlotRet;
                }
            }

            FindOpenInvSlotRet = -1;
            return FindOpenInvSlotRet;
        }

        public static bool TakeInv(int index, int itemNum, int ItemVal)
        {
            bool TakeInvRet = default;
            int i;

            TakeInvRet = Conversions.ToBoolean(0);

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                return TakeInvRet;
            }

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo; i++)
            {

                // Check to see if the player has the item
                if (GetPlayerInv(index, i) == itemNum)
                {
                    if (Core.Type.Item[itemNum].Type == (byte)ItemType.Currency | Core.Type.Item[itemNum].Stackable == 1)
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
                        SetPlayerInv(index, i, -1);
                        SetPlayerInvValue(index, i, 0);
                        // Send the inventory update
                        NetworkSend.SendInventoryUpdate(index, i);
                        return TakeInvRet;
                    }
                }

            }

            return TakeInvRet;

        }

        public static bool GiveInv(int index, int itemNum, int ItemVal, bool SendUpdate = true)
        {
            bool GiveInvRet = default;
            int i;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
            {
                GiveInvRet = Conversions.ToBoolean(0);
                return GiveInvRet;
            }

            i = FindOpenInvSlot(index, itemNum);

            // Check to see if inventory is full
            if (i != -1)
            {
                if (ItemVal == 0)
                    ItemVal = 1;

                SetPlayerInv(index, i, itemNum);
                SetPlayerInvValue(index, i, GetPlayerInvValue(index, i) + ItemVal);
                if (SendUpdate)
                    NetworkSend.SendInventoryUpdate(index, i);
                GiveInvRet = Conversions.ToBoolean(1);
            }
            else
            {
                NetworkSend.PlayerMsg(index, "Your inventory is full.", (int)ColorType.BrightRed);
                GiveInvRet = Conversions.ToBoolean(0);
            }

            return GiveInvRet;

        }

        public static void PlayerMapDropItem(int index, int invNum, int amount)
        {
            int i;

            // Check for subscript out of range
            if (Conversions.ToInteger(NetworkConfig.IsPlaying(index)) == 0 | invNum < 0 | invNum > Core.Constant.MAX_INV)
            {
                return;
            }

            // check the player isn't doing something
            if (Core.Type.TempPlayer[index].InBank | Core.Type.TempPlayer[index].InShop >= 0 | Core.Type.TempPlayer[index].InTrade >= 0)
                return;

            if (Conversions.ToInteger(Core.Type.Moral[GetPlayerMap(index)].CanDropItem) == 0)
            {
                NetworkSend.PlayerMsg(index, "You can't drop items here!", (int) ColorType.BrightRed);
                return;
            }

            if (GetPlayerInv(index, invNum) >= 0)
            {
                if (GetPlayerInv(index, invNum) < Core.Constant.MAX_ITEMS)
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
                                    SetPlayerInv(index, invNum, -1);
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
                                SetPlayerInv(index, invNum, -1);
                                SetPlayerInvValue(index, invNum, 0);
                            }

                            // Send inventory update
                            NetworkSend.SendInventoryUpdate(index, invNum);
                            // Spawn the item before we set the num or we'll get a different free map item slot
                            Item.SpawnItemSlot(i, (int)withBlock.Num, amount, GetPlayerMap(index), GetPlayerX(index), GetPlayerY(index));
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
                SetPlayerInv(index, InvSlot, -1);
                SetPlayerInvValue(index, InvSlot, 0);
                return TakeInvSlotRet;
            }

            return TakeInvSlotRet;

        }

        public static object CanPlayerUseItem(int index, int itemNum)
        {
            object CanPlayerUseItemRet = default;
            int i;

            if ((int)Core.Type.Map[GetPlayerMap(index)].Moral >= 0)
            {
                if (Conversions.ToInteger(Core.Type.Moral[Core.Type.Map[GetPlayerMap(index)].Moral].CanUseItem) == 0)
                {
                    NetworkSend.PlayerMsg(index, "You can't use items here!", (int) ColorType.BrightRed);
                    return CanPlayerUseItemRet;
                }
            }

            var loopTo = (byte)StatType.Count;
            for (i = 0; i < loopTo; i++)
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
            if (!(Core.Type.Item[itemNum].JobReq == GetPlayerJob(index)) & !(Core.Type.Item[itemNum].JobReq == -1))
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
            if (Core.Type.TempPlayer[index].InBank == true | Core.Type.TempPlayer[index].InShop >= 0 | Core.Type.TempPlayer[index].InTrade >= 0)
            {
                NetworkSend.PlayerMsg(index, "You can't use items while in a bank, shop, or trade!", (int) ColorType.BrightRed);
                return CanPlayerUseItemRet;
            }

            CanPlayerUseItemRet = 0;
            return CanPlayerUseItemRet;
        }

        public static void UseItem(int index, int InvNum)
        {
            int itemNum;
            int i;
            int n;
            var tempitem = default(int);
            int m;
            var tempdata = new int[(int)(StatType.Count + 3 + 1)];
            var tempstr = new string[3];

            // Prevent hacking
            if (InvNum < 0 | InvNum > Core.Constant.MAX_INV)
                return;

            itemNum = GetPlayerInv(index, InvNum);

            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
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

                                    if (GetPlayerEquipment(index, EquipmentType.Weapon) >= 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Weapon);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Weapon);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem >= 0) // give back the stored item
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
                                    if (GetPlayerEquipment(index, EquipmentType.Armor) >= 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Armor);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Armor);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem >= 0) // Return their old equipment to their inventory.
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
                                    if (GetPlayerEquipment(index, EquipmentType.Helmet) >= 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Helmet);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Helmet);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem >= 0) // give back the stored item
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
                                    if (GetPlayerEquipment(index, EquipmentType.Shield) >= 0)
                                    {
                                        tempitem = GetPlayerEquipment(index, EquipmentType.Shield);
                                    }

                                    SetPlayerEquipment(index, itemNum, EquipmentType.Shield);

                                    NetworkSend.PlayerMsg(index, "You equip " + GameLogic.CheckGrammar(Core.Type.Item[itemNum].Name), (int) ColorType.BrightGreen);
                                    TakeInv(index, itemNum, 1);

                                    if (tempitem >= 0) // give back the stored item
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
                                Animation.SendAnimation(GetPlayerMap(index), Core.Type.Item[itemNum].Animation, 0, 0, (byte)TargetType.Player, index);
                                TakeInv(index, itemNum, 0);
                            }
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

            if (OldSlot == -1 | NewSlot == -1)
                return;

            OldNum = GetPlayerInv(index, OldSlot);
            OldValue = GetPlayerInvValue(index, OldSlot);
            NewNum = GetPlayerInv(index, NewSlot);
            NewValue = GetPlayerInvValue(index, NewSlot);

            if (NewNum >= 0)
            {
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
            double OldNum;
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

            if (OldSlot == -1 | NewSlot == -1)
                return;

            OldNum = GetPlayerSkill(index, (int)OldSlot);
            OldValue = GetPlayerSkillCD(index, (int)OldSlot);
            NewNum = GetPlayerSkill(index, (int)NewSlot);
            NewValue = GetPlayerSkillCD(index, (int)NewSlot);

            if (NewNum >= 0)
            {
                if (OldNum == NewNum & Core.Type.Item[(int)NewNum].Stackable == 1) // same item, if we can stack it, lets do that :P
                {
                    SetPlayerSkill(index, (int)NewSlot, NewNum);
                    SetPlayerSkillCD(index, (int)NewSlot, NewValue);
                    SetPlayerSkill(index, (int)OldSlot, 0);
                    SetPlayerSkillCD(index, (int)OldSlot, 0);
                }
                else
                {
                    SetPlayerSkill(index, (int)NewSlot, (int)OldNum);
                    SetPlayerSkillCD(index, (int)NewSlot, OldValue);
                    SetPlayerSkill(index, (int)OldSlot, (int)NewNum);
                    SetPlayerSkillCD(index, (int)OldSlot, NewValue);
                }
            }
            else
            {
                SetPlayerSkill(index, (int)NewSlot, (int)OldNum);
                SetPlayerSkillCD(index, (int)NewSlot, OldValue);
                SetPlayerSkill(index, (int)OldSlot, (int)NewNum);
                SetPlayerSkillCD(index, (int)OldSlot, NewValue);
            }

            NetworkSend.SendPlayerSkills(index);
        }

        #endregion

        #region Equipment

        public static void CheckEquippedItems(int index)
        {
            double itemNum;
            int i;

            // We want to check incase an admin takes away an object but they had it equipped
            var loopTo = EquipmentType.Count;
            for (i = 0; i < (int)loopTo; i++)
            {
                itemNum = GetPlayerEquipment(index, (EquipmentType)i);

                if (itemNum >= 0)
                {

                    switch (i)
                    {
                        case (byte)EquipmentType.Weapon:
                            {

                                if (Core.Type.Item[(int)itemNum].SubType != (byte)EquipmentType.Weapon)
                                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                                break;
                            }
                        case (byte)EquipmentType.Armor:
                            {

                                if (Core.Type.Item[(int)itemNum].SubType != (byte)EquipmentType.Armor)
                                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                                break;
                            }
                        case (byte)EquipmentType.Helmet:
                            {

                                if (Core.Type.Item[(int)itemNum].SubType != (byte)EquipmentType.Helmet)
                                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                                break;
                            }
                        case (byte)EquipmentType.Shield:
                            {

                                if (Core.Type.Item[(int)itemNum].SubType != (byte)EquipmentType.Shield)
                                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                                break;
                            }
                    }
                }
                else
                {
                    SetPlayerEquipment(index, -1, (EquipmentType)i);
                }

            }

        }

        public static void PlayerUnequipItem(int index, int EqSlot)
        {
            int i;
            int m;
            int itemNum;

            if (EqSlot < 1 | EqSlot > (byte)EquipmentType.Count)
                return; // exit out early if error'd

            if (GetPlayerEquipment(index, (EquipmentType)EqSlot) < 0 || GetPlayerEquipment(index, (EquipmentType)EqSlot) > Core.Constant.MAX_ITEMS)
                return;

            if (FindOpenInvSlot(index, GetPlayerEquipment(index, (EquipmentType)EqSlot)) >= 0)
            {
                itemNum = GetPlayerEquipment(index, (EquipmentType)EqSlot);

                m = FindOpenInvSlot(index, (int)Core.Type.Player[index].Equipment[EqSlot]);
                SetPlayerInv(index, m, Core.Type.Player[index].Equipment[EqSlot]);
                SetPlayerInvValue(index, m, 0);

                NetworkSend.PlayerMsg(index, "You unequip " + GameLogic.CheckGrammar(Core.Type.Item[GetPlayerEquipment(index, (EquipmentType)EqSlot)].Name), (int) ColorType.Yellow);

                // remove equipment
                SetPlayerEquipment(index, -1, (EquipmentType)EqSlot);
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

        public static void JoinGame(int index)
        {
            try
            {
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

                General.UpdateCaption();

                Script.Instance?.JoinGame(index);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void LeftGame(int index)
        {
            try
            {
                int i;
                int tradeTarget;

                if (Core.Type.TempPlayer[index].InGame)
                {
                    NetworkSend.SendLeftMap(index);
                    Core.Type.TempPlayer[index].InGame = false;

                    // Check if the player was in a party, and if so cancel it out so the other player doesn't continue to get half exp
                    // leave party.
                    Party.PlayerLeave(index);

                    // cancel any trade they're in
                    if (Core.Type.TempPlayer[index].InTrade >= 0)
                    {
                        tradeTarget = (int)Core.Type.TempPlayer[index].InTrade;
                        NetworkSend.PlayerMsg(tradeTarget, string.Format("{0} has declined the trade.", GetPlayerName(index)), (int)ColorType.BrightRed);
                        // clear out trade
                        var loopTo = Core.Constant.MAX_INV;
                        for (i = 0; i < loopTo; i++)
                        {
                            Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Num = -1;
                            Core.Type.TempPlayer[tradeTarget].TradeOffer[i].Value = 0;
                        }
                        Core.Type.TempPlayer[tradeTarget].InTrade = -1;
                        NetworkSend.SendCloseTrade(tradeTarget);
                    }

                    // Send a global message that he/she left
                    NetworkSend.GlobalMsg(string.Format("{0} has left {1}!", GetPlayerName(index), SettingsManager.Instance.GameName));

                    Console.WriteLine(string.Format("{0} has left {1}!", GetPlayerName(index), SettingsManager.Instance.GameName));

                    Pet.RecallPet(index);
                    Database.SaveCharacter(index, Core.Type.TempPlayer[index].Slot);
                    Database.SaveBank(index);

                    Script.Instance?.LeftGame(index);
                }

                Database.ClearPlayer(index);

                General.UpdateCaption();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            General.UpdateCaption();
        }

        public static int KillPlayer(int index)
        {
            try
            {
                int exp = Script.Instance?.KillPlayer(index);

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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;

        }

        public static void OnDeath(int index)
        {
            try
            {
                // Clear skill casting
                Core.Type.TempPlayer[index].SkillBuffer = -1;
                Core.Type.TempPlayer[index].SkillBufferTimer = 0;
                NetworkSend.SendClearSkillBuffer(index);

                Script.Instance?.OnDeath();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static int GetPlayerVitalRegen(int index, VitalType Vital)
        {
            try
            {
                int i;
                i = Script.Instance?.GetPlayerVitalRegen(index, Vital);
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        public static void HandleNPCKillExperience(int index, int NPCNum)
        {
            // Get the experience we'll have to hand out. If it's negative, just ignore this method.
            int Experience = Core.Type.NPC[(int)NPCNum].Exp;
            if (Experience < 0)
                return;

            // Is our player in a party? If so, hand out exp to everyone.
            if (Party.IsPlayerInParty(index))
            {
                Party.ShareExp(Party.GetPlayerParty(index), Experience, index, GetPlayerMap(index));
            }
            else
            {
                Event.GivePlayerExp(index, Experience);
            }
        }

        public static void HandlePlayerKillExperience(int attacker, int victim)
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
                    Party.ShareExp(Party.GetPlayerParty(attacker), exp, attacker, GetPlayerMap(attacker));
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
        public static void bufferSkill(int index, int SkillSlot)
        {
            double skillNum;
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

            skillNum = GetPlayerSkill(index, SkillSlot);
            mapNum = GetPlayerMap(index);

            if (skillNum < 0 | skillNum > Core.Constant.MAX_SKILLS)
                return;

            // Make sure player has the skill
            if (!HasSkill(index, (int)skillNum))
                return;

            // see if cooldown has finished
            if (Core.Type.TempPlayer[index].SkillCD[SkillSlot] > General.GetTimeMs())
            {
                NetworkSend.PlayerMsg(index, "Skill hasn't cooled down yet!", (int) ColorType.Yellow);
                return;
            }

            MPCost = Core.Type.Skill[(int)skillNum].MpCost;

            // Check if they have enough MP
            if (GetPlayerVital(index, VitalType.SP) < MPCost)
            {
                NetworkSend.PlayerMsg(index, "Not enough spirit!", (int) ColorType.Yellow);
                return;
            }

            LevelReq = Core.Type.Skill[(int)skillNum].LevelReq;

            // Make sure they are the right level
            if (LevelReq > GetPlayerLevel(index))
            {
                NetworkSend.PlayerMsg(index, "You must be level " + LevelReq + " to use this skill.", (int) ColorType.BrightRed);
                return;
            }

            AccessReq = Core.Type.Skill[(int)skillNum].AccessReq;

            // make sure they have the right access
            if (AccessReq > GetPlayerAccess(index))
            {
                NetworkSend.PlayerMsg(index, "You must be a developer to use this skill.", (int) ColorType.BrightRed);
                return;
            }

            JobReq = Core.Type.Skill[(int)skillNum].JobReq;

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
            if (Core.Type.Skill[(int)skillNum].Range > 0)
            {
                // ranged attack, single target or aoe?
                if (!Core.Type.Skill[(int)skillNum].IsAoE)
                {
                    SkillCastType = 2; // targetted
                }
                else
                {
                    SkillCastType = 3;
                } // targetted aoe
            }
            else if (!Core.Type.Skill[(int)skillNum].IsAoE)
            {
                SkillCastType = 0; // self-cast
            }
            else
            {
                SkillCastType = 0;
            } // self-cast AoE

            TargetType = (TargetType)Core.Type.TempPlayer[index].TargetType;
            Target = Core.Type.TempPlayer[index].Target;
            range = Core.Type.Skill[(int)skillNum].Range;
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
                            else if (Core.Type.Skill[(int)skillNum].Type != (byte)SkillType.DamageHp & Core.Type.Skill[(int)skillNum].Type != (byte)SkillType.DamageMp)
                            {
                                HasBuffered = true;
                            }
                            else if (CanPlayerAttackPlayer(index, Target, true))
                            {
                                HasBuffered = true;
                            }
                        }
                        else if (TargetType == TargetType.NPC)
                        {
                            // if have target, check in range
                            if (!IsInRange(range, GetPlayerX(index), GetPlayerY(index), Core.Type.MapNPC[mapNum].NPC[Target].X, Core.Type.MapNPC[mapNum].NPC[Target].Y))
                            {
                                NetworkSend.PlayerMsg(index, "Target not in range.", (int) ColorType.BrightRed);
                                HasBuffered = false;
                            }
                            // go through skill Type
                            else if (Core.Type.Skill[(int)skillNum].Type != (byte)SkillType.DamageHp & Core.Type.Skill[(int)skillNum].Type != (byte)SkillType.DamageMp)
                            {
                                HasBuffered = true;
                            }
                            else if (CanPlayerAttackNPC(index, Target, true))
                            {
                                HasBuffered = true;
                            }
                        }

                        break;
                    }
            }

            if (HasBuffered)
            {
                Animation.SendAnimation(mapNum, Core.Type.Skill[(int)skillNum].CastAnim, 0, 0, (byte)TargetType.Player, index);
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
            byte bankSlot;
            int itemNum;

            if (InvSlot < 0 | InvSlot > Core.Constant.MAX_INV)
                return;

            if (Amount < 0)
                Amount = 0;

            if (GetPlayerInvValue(index, InvSlot) < Amount & GetPlayerInv(index, InvSlot) == 0)
                return;

            bankSlot = FindOpenbankSlot(index, GetPlayerInv(index, InvSlot));
            itemNum = GetPlayerInv(index, InvSlot);

            if (bankSlot >= 0)
            {
                if (Core.Type.Item[GetPlayerInv(index, InvSlot)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerInv(index, InvSlot)].Stackable == 1)
                {
                    if (GetPlayerBank(index, bankSlot) == GetPlayerInv(index, InvSlot))
                    {
                        SetPlayerBankValue(index, bankSlot, GetPlayerBankValue(index, bankSlot) + Amount);
                        TakeInv(index, GetPlayerInv(index, InvSlot), Amount);
                    }
                    else
                    {
                        SetPlayerBank(index, bankSlot, GetPlayerInv(index, InvSlot));
                        SetPlayerBankValue(index, bankSlot, Amount);
                        TakeInv(index, GetPlayerInv(index, InvSlot), Amount);
                    }
                }
                else if (GetPlayerBank(index, bankSlot) == GetPlayerInv(index, InvSlot))
                {
                    SetPlayerBankValue(index, bankSlot, GetPlayerBankValue(index, bankSlot) + 1);
                    TakeInv(index, GetPlayerInv(index, InvSlot), 0);
                }
                else
                {
                    SetPlayerBank(index, bankSlot, itemNum);
                    SetPlayerBankValue(index, bankSlot, 1);
                    TakeInv(index, GetPlayerInv(index, InvSlot), 0);
                }

                NetworkSend.SendBank(index);
            }

        }

        public static int GetPlayerBank(int index, byte bankSlot)
        {
            int GetPlayerBankRet = default;
            GetPlayerBankRet = Bank[index].Item[bankSlot].Num;
            return GetPlayerBankRet;
        }

        public static void SetPlayerBank(int index, byte bankSlot, int itemNum)
        {
            Bank[index].Item[bankSlot].Num = itemNum;
        }

        public static int GetPlayerBankValue(int index, byte bankSlot)
        {
            int GetPlayerBankValueRet = default;
            GetPlayerBankValueRet = Bank[index].Item[bankSlot].Value;
            return GetPlayerBankValueRet;
        }

        public static void SetPlayerBankValue(int index, byte bankSlot, int Value)
        {
            Bank[index].Item[bankSlot].Value = Value;
        }

        public static byte FindOpenbankSlot(int index, int itemNum)
        {
            byte FindOpenbankSlotRet = default;
            int i;

            if (!NetworkConfig.IsPlaying(index))
                return FindOpenbankSlotRet;
            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS)
                return FindOpenbankSlotRet;

            if (Core.Type.Item[itemNum].Type == (byte)ItemType.Currency | Core.Type.Item[itemNum].Stackable == 1)
            {
                var loopTo = Core.Constant.MAX_BANK;
                for (i = 0; i < loopTo; i++)
                {
                    if (GetPlayerBank(index, (byte)i) == itemNum)
                    {
                        FindOpenbankSlotRet = (byte)i;
                        return FindOpenbankSlotRet;
                    }
                }
            }

            var loopTo1 = Core.Constant.MAX_BANK;
            for (i = 0; i < loopTo1; i++)
            {
                if (GetPlayerBank(index, (byte)i) == -1)
                {
                    FindOpenbankSlotRet = (byte)i;
                    return FindOpenbankSlotRet;
                }
            }

            return FindOpenbankSlotRet;

        }

        public static void TakeBank(int index, byte bankSlot, int Amount)
        {
            int invSlot;

            if (bankSlot < 0 | bankSlot > Core.Constant.MAX_BANK)
                return;

            if (Amount < 0)
                Amount = 0;

            if (GetPlayerBankValue(index, bankSlot) < Amount)
                return;

            invSlot = FindOpenInvSlot(index, GetPlayerBank(index, bankSlot));

            if (invSlot >= 0)
            {
                if (Core.Type.Item[GetPlayerBank(index, bankSlot)].Type == (byte)ItemType.Currency | Core.Type.Item[GetPlayerBank(index, bankSlot)].Stackable == 1)
                {
                    GiveInv(index, GetPlayerBank(index, bankSlot), Amount);
                    SetPlayerBankValue(index, bankSlot, GetPlayerBankValue(index, bankSlot) - Amount);
                    if (GetPlayerBankValue(index, bankSlot) < 0)
                    {
                        SetPlayerBank(index, bankSlot, 0);
                        SetPlayerBankValue(index, bankSlot, 0);
                    }
                }
                else if (GetPlayerBank(index, bankSlot) == GetPlayerInv(index, (int)invSlot))
                {
                    if (GetPlayerBankValue(index, bankSlot) > 1)
                    {
                        GiveInv(index, GetPlayerBank(index, bankSlot), 0);
                        SetPlayerBankValue(index, bankSlot, GetPlayerBankValue(index, bankSlot) - 1);
                    }
                }
                else
                {
                    GiveInv(index, GetPlayerBank(index, bankSlot), 0);
                    SetPlayerBank(index, bankSlot, -1);
                    SetPlayerBankValue(index, bankSlot, 0);
                }

            }

            NetworkSend.SendBank(index);
        }

        public static void PlayerSwitchbankSlots(int index, int OldSlot, int NewSlot)
        {
            int OldNum;
            int OldValue;
            int NewNum;
            int NewValue;
            int i;

            if (OldSlot == -1 | NewSlot == -1)
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