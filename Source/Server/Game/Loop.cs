﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Core.Common;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Type;

namespace Server
{

    static class Loop
    {

        public static void Server()
        {
            int tick;
            var tmr25 = default(int);
            var tmr300 = default(int);
            var tmr500 = default(int);
            var tmr1000 = default(int);
            var lastUpdateSavePlayers = default(int);
            var lastUpdateMapSpawnItems = default(int);
            var lastUpdatePlayerVitals = default(int);

            do
            {
                // Update our current tick value.
                tick = General.GetTimeMs();

                // Don't process anything else if we're going down.
                if (General.ServerDestroyed)

                    // Get all our online players.
                    Debugger.Break(); var onlinePlayers = Core.Type.TempPlayer.Where(player => player.InGame).Select((player, index) => new { Index = Operators.AddObject(index, 1), player }).ToArray();

                General.CheckShutDownCountDown();

                if (tick > tmr25)
                {
                    // Check if any of our players has completed casting and get their skill going if they have.
                    var playerskills = (from p in onlinePlayers
                                        where p.player.SkillBuffer > 0 && General.GetTimeMs() > p.player.SkillBufferTimer + Core.Type.Skill[p.player.SkillBuffer].CastTime * 1000
                                        select new { p.Index, Success = HandleCastSkill((int)p.Index) }).ToArray();

                    // Check if we need to clear any of our players from being stunned.
                    var playerstuns = (from p in onlinePlayers
                                       where p.player.StunDuration > 0 && General.GetTimeMs() > p.player.StunTimer + p.player.StunDuration * 1000
                                       select new { p.Index, Success = HandleClearStun((int)p.Index) }).ToArray();

                    // Check if any of our pets has completed casting and get their skill going if they have.
                    var petskills = (from p in onlinePlayers
                                     where (int)Core.Type.Player[(int)p.Index].Pet.Alive == 1 && Core.Type.TempPlayer[(int)p.Index].PetSkillBuffer.Skill > 0 && General.GetTimeMs() > p.player.PetSkillBuffer.Timer + Core.Type.Skill[Core.Type.Player[(int)p.Index].Pet.Skill[p.player.PetSkillBuffer.Skill]].CastTime * 1000
                                     select new { p.Index, Success = HandlePetSkill((int)p.Index) }).ToArray();

                    // Check if we need to clear any of our pets from being stunned.
                    var petstuns = (from p in onlinePlayers
                                    where p.player.PetStunDuration > 0 && General.GetTimeMs() > p.player.PetStunTimer + p.player.PetStunDuration * 1000
                                    select new { p.Index, Success = HandleClearPetStun((int)p.Index) }).ToArray();

                    // check pet regen timer
                    var petregen = (from p in onlinePlayers
                                    where p.player.PetStopRegen && p.player.PetStopRegenTimer + 5000 < General.GetTimeMs()
                                    select new { p.Index, Success = HandleStopPetRegen((int)p.Index) }).ToArray();

                    // HoT and DoT logic
                    // For x = 1 To Core.Constant.MAX_COTS
                    // HandleDoT_Pet i, x
                    // HandleHoT_Pet i, x
                    // Next

                    // Update all our available events.
                    EventLogic.UpdateEventLogic();

                    // Move the timer up 25ms.
                    tmr25 = General.GetTimeMs() + 25;
                }

                if (tick > tmr1000)
                {
                    TimeType.Instance.Tick();

                    // Move the timer up 1000ms.
                    tmr1000 = General.GetTimeMs() + 1000;
                }

                if (tick > tmr500)
                {
                    // Move the timer up 500ms.
                    tmr500 = General.GetTimeMs() + 500;
                }

                if (General.GetTimeMs() > tmr300)
                {
                    UpdateNPCAi();
                    Pet.UpdatePetAi();
                    tmr300 = General.GetTimeMs() + 300;
                }

                // Checks to update player vitals every 5 seconds
                if (tick > lastUpdatePlayerVitals)
                {
                    UpdatePlayerVitals();
                    lastUpdatePlayerVitals = General.GetTimeMs() + 5000;
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

                Thread.Sleep(1);
            }
            while (true);
        }

        public static void UpdateSavePlayers()
        {
            int i;

            if (NetworkConfig.Socket.HighIndex > 0)
            {
                Console.WriteLine("Saving all online players...");

                var loopTo = NetworkConfig.Socket.HighIndex;
                for (i = 0; i <= (int)loopTo; i++)
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
            var loopTo = Core.Constant.MAX_MAPS - 1;
            for (y = 0; y <= (int)loopTo; y++)
            {

                // Make sure no one is on the map when it respawns
                if (!PlayersOnMap[y])
                {

                    // Clear out unnecessary junk
                    var loopTo1 = Core.Constant.MAX_MAP_ITEMS - 1;
                    for (x = 0; x <= (int)loopTo1; x++)
                        Database.ClearMapItem(x, y);

                    // Spawn the items
                    Item.SpawnMapItems(y);
                    Item.SendMapItemsToAll(y);
                }

            }

        }

        private static void UpdatePlayerVitals()
        {
            int i;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= (int)loopTo; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    for (int x = 1, loopTo1 = (int) VitalType.Count - 1; x <= (int)loopTo1; x++)
                    {
                        if (GetPlayerVital(i, (VitalType)x) != GetPlayerMaxVital(i, (VitalType)x))
                        {
                            SetPlayerVital(i, (VitalType)x, GetPlayerVital(i, (VitalType)x) + Player.GetPlayerVitalRegen(i, (VitalType)x));
                            NetworkSend.SendVital(i, (VitalType)x);
                        }
                    }
                }
            }

        }

        private static void UpdateNPCAi()
        {
            int i;
            int x;
            int n;
            int x1;
            int y1;
            int mapNum;
            int tickCount;
            int damage;
            int distanceX;
            int distanceY;
            int NPCNum;
            int target;
            byte targetType;
            var targetX = default(int);
            var targetY = default(int);
            bool targetVerify;
            int resourceindex;

            var loopTo = Core.Constant.MAX_MAPS - 1;
            for (mapNum = 0; mapNum < (int)loopTo; mapNum++)
            {

                if (General.ServerDestroyed)
                    return;

                // items appearing to everyone
                var loopTo1 = Core.Constant.MAX_MAP_ITEMS - 1;
                for (i = 0; i < (int)loopTo1; i++)
                {
                    if (Core.Type.MapItem[mapNum, i].Num > 0)
                    {
                        if (!string.IsNullOrEmpty(Core.Type.MapItem[mapNum, i].PlayerName))
                        {
                            // make item public?
                            if (Core.Type.MapItem[mapNum, i].PlayerTimer < General.GetTimeMs())
                            {
                                // make it public
                                MapItem[mapNum, i].PlayerName = "";
                                MapItem[mapNum, i].PlayerTimer = 0;
                                // send updates to everyone
                                Item.SendMapItemsToAll(mapNum);
                            }
                            // despawn item?
                            if (Core.Type.MapItem[mapNum, i].CanDespawn)
                            {
                                if (Core.Type.MapItem[mapNum, i].DespawnTimer < General.GetTimeMs())
                                {
                                    // despawn it
                                    Database.ClearMapItem(i, mapNum);
                                    // send updates to everyone
                                    Item.SendMapItemsToAll(mapNum);
                                }
                            }
                        }
                    }
                }

                // Respawning Resources
                if (Core.Type.MapResource[mapNum].ResourceCount > 0)
                {
                    var loopTo2 = Core.Type.MapResource[mapNum].ResourceCount;
                    for (i = 0; i <= (int)loopTo2; i++)
                    {

                        resourceindex = Core.Type.Map[mapNum].Tile[Core.Type.MapResource[mapNum].ResourceData[i].X, Core.Type.MapResource[mapNum].ResourceData[i].Y].Data1;

                        if (resourceindex > 0)
                        {
                            if (Core.Type.MapResource[mapNum].ResourceData[i].State == 1 | Core.Type.MapResource[mapNum].ResourceData[i].Health < 1)  // dead or fucked up
                            
                                if (Core.Type.MapResource[mapNum].ResourceData[i].Timer + Core.Type.Resource[resourceindex].RespawnTime * 1000 < General.GetTimeMs())
                                {
                                    Core.Type.MapResource[mapNum].ResourceData[i].Timer = General.GetTimeMs();
                                    Core.Type.MapResource[mapNum].ResourceData[i].State = 0; // normal
                                                                                                // re-set health to resource root
                                    Core.Type.MapResource[mapNum].ResourceData[i].Health = (byte)Core.Type.Resource[resourceindex].Health;
                                    Resource.SendMapResourceToMap(mapNum, i);
                                }
                            }
                        }
                    }
                }

                if (General.ServerDestroyed)
                    return;

            if (PlayersOnMap[mapNum] == true)
            {
                tickCount = General.GetTimeMs();

                var loopTo3 = Core.Constant.MAX_MAP_NPCS - 1;
                for (x = 0; x <= (int)loopTo3; x++)
                {
                    NPCNum = Core.Type.MapNPC[mapNum].NPC[x].Num;

                    // check if they've completed casting, and if so set the actual skill going
                    if (Core.Type.MapNPC[mapNum].NPC[x].SkillBuffer > 0 & Core.Type.Map[mapNum].NPC[x] > 0 & Core.Type.MapNPC[mapNum].NPC[x].Num > 0)
                    {
                        if (General.GetTimeMs() > Core.Type.MapNPC[mapNum].NPC[x].SkillBufferTimer + Core.Type.Skill[Core.Type.NPC[NPCNum].Skill[Core.Type.MapNPC[mapNum].NPC[x].SkillBuffer]].CastTime * 1000)
                        {
                            CastNPCSkill(x, mapNum, Core.Type.MapNPC[mapNum].NPC[x].SkillBuffer);
                            Core.Type.MapNPC[mapNum].NPC[x].SkillBuffer = 0;
                            Core.Type.MapNPC[mapNum].NPC[x].SkillBufferTimer = 0;
                        }
                    }
                    else
                    {
                        // /////////////////////////////////////////
                        // // This is used for ATTACKING ON SIGHT //
                        // /////////////////////////////////////////
                        // Make sure theres a npc with the map
                        if (Core.Type.Map[mapNum].NPC[x] > 0 & Core.Type.MapNPC[mapNum].NPC[x].Num > 0)
                        {

                            // If the npc is a attack on sight, search for a player on the map
                            if (Core.Type.NPC[NPCNum].Behaviour == (byte)NPCBehavior.AttackOnSight | Core.Type.NPC[NPCNum].Behaviour == (byte)NPCBehavior.Guard)
                            {

                                // make sure it's not stunned
                                if (!(Core.Type.MapNPC[mapNum].NPC[x].StunDuration > 0))
                                {

                                    var loopTo4 = NetworkConfig.Socket.HighIndex;
                                    for (i = 0; i <= (int)loopTo4; i++)
                                    {
                                        if (NetworkConfig.IsPlaying(i))
                                        {
                                            if (GetPlayerMap(i) == mapNum & Core.Type.MapNPC[mapNum].NPC[x].Target == 0 & GetPlayerAccess(i) <= (byte)AccessType.Moderator)
                                            {
                                                if (Pet.PetAlive(i))
                                                {
                                                    n = Core.Type.NPC[NPCNum].Range;
                                                    distanceX = Core.Type.MapNPC[mapNum].NPC[x].X - Core.Type.Player[i].Pet.X;
                                                    distanceY = Core.Type.MapNPC[mapNum].NPC[x].Y - Core.Type.Player[i].Pet.Y;

                                                    // Make sure we get a positive value
                                                    if (distanceX < 0)
                                                        distanceX *= -1;
                                                    if (distanceY < 0)
                                                        distanceY *= -1;

                                                    // Are they in range?  if so GET'M!
                                                    if (distanceX <= n & distanceY <= n)
                                                    {
                                                        if (Core.Type.NPC[NPCNum].Behaviour == (byte)NPCBehavior.AttackOnSight | GetPlayerPK(i) == i)
                                                        {
                                                            if (Strings.Len(Core.Type.NPC[NPCNum].AttackSay) > 0)
                                                            {
                                                                NetworkSend.PlayerMsg(i, Core.Type.NPC[NPCNum].Name + " says: " + Core.Type.NPC[NPCNum].AttackSay, (int)ColorType.White);
                                                            }
                                                            Core.Type.MapNPC[mapNum].NPC[x].TargetType = (byte)Core.Enum.TargetType.Pet;
                                                            Core.Type.MapNPC[mapNum].NPC[x].Target = i;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    n = Core.Type.NPC[NPCNum].Range;
                                                    distanceX = Core.Type.MapNPC[mapNum].NPC[x].X - GetPlayerX(i);
                                                    distanceY = Core.Type.MapNPC[mapNum].NPC[x].Y - GetPlayerY(i);

                                                    // Make sure we get a positive value
                                                    if (distanceX < 0)
                                                        distanceX *= -1;
                                                    if (distanceY < 0)
                                                        distanceY *= -1;

                                                    // Are they in range?  if so GET'M!
                                                    if (distanceX <= n & distanceY <= n)
                                                    {
                                                        if (Core.Type.NPC[NPCNum].Behaviour == (byte)NPCBehavior.AttackOnSight | GetPlayerPK(i) == 1)
                                                        {
                                                            if (Core.Type.NPC[NPCNum].AttackSay.Length > 0)
                                                            {
                                                                NetworkSend.PlayerMsg(i, GameLogic.CheckGrammar(Core.Type.NPC[NPCNum].Name, 1) + " says, '" + Core.Type.NPC[NPCNum].AttackSay + "' to you.", (int)ColorType.Yellow);
                                                            }
                                                            Core.Type.MapNPC[mapNum].NPC[x].TargetType = (byte)Core.Enum.TargetType.Player;
                                                            Core.Type.MapNPC[mapNum].NPC[x].Target = i;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    // Check if target was found for NPC targetting
                                    if (Core.Type.MapNPC[mapNum].NPC[x].Target == 0 & Core.Type.NPC[NPCNum].Faction > 0)
                                    {
                                        // search for npc of another faction to target
                                        var loopTo5 = Core.Constant.MAX_MAP_NPCS - 1;
                                        for (i = 0; i <= (int)loopTo5; i++)
                                        {
                                            // exist?
                                            if (Core.Type.MapNPC[mapNum].NPC[i].Num > 0)
                                            {
                                                // different faction?
                                                if ((int)Core.Type.NPC[Core.Type.MapNPC[mapNum].NPC[i].Num].Faction > 0 & Core.Type.NPC[Core.Type.MapNPC[mapNum].NPC[i].Num].Faction != Core.Type.NPC[NPCNum].Faction)
                                                {
                                                    n = Core.Type.NPC[NPCNum].Range;
                                                    distanceX = (int)(MapNPC[mapNum].NPC[x].X - (long)MapNPC[mapNum].NPC[i].X);
                                                    distanceY = (int)(MapNPC[mapNum].NPC[x].Y - (long)MapNPC[mapNum].NPC[i].Y);

                                                    // Make sure we get a positive value
                                                    if (distanceX < 0)
                                                        distanceX *= -1;
                                                    if (distanceY < 0)
                                                        distanceY *= -1;

                                                    // Are they in range?  if so GET'M!
                                                    if (distanceX <= n & distanceY <= n & Core.Type.NPC[NPCNum].Behaviour == (byte)NPCBehavior.AttackOnSight)
                                                    {
                                                        Core.Type.MapNPC[mapNum].NPC[x].TargetType = 2; // npc
                                                        Core.Type.MapNPC[mapNum].NPC[x].Target = i;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        targetVerify = Conversions.ToBoolean(0);

                        // /////////////////////////////////////////////
                        // // This is used for NPC walking/targetting //
                        // /////////////////////////////////////////////
                        // Make sure theres a npc with the map
                        if (Core.Type.Map[mapNum].NPC[x] > 0 & Core.Type.MapNPC[mapNum].NPC[x].Num > 0)
                        {
                            if (Core.Type.MapNPC[mapNum].NPC[x].StunDuration > 0)
                            {
                                // check if we can unstun them
                                if (General.GetTimeMs() > Core.Type.MapNPC[mapNum].NPC[x].StunTimer + Core.Type.MapNPC[mapNum].NPC[x].StunDuration * 1000)
                                {
                                    Core.Type.MapNPC[mapNum].NPC[x].StunDuration = 0;
                                    Core.Type.MapNPC[mapNum].NPC[x].StunTimer = 0;
                                }
                            }
                            else
                            {

                                target = Core.Type.MapNPC[mapNum].NPC[x].Target;
                                targetType = Core.Type.MapNPC[mapNum].NPC[x].TargetType;

                                // Check to see if its time for the npc to walk
                                if (Core.Type.NPC[NPCNum].Behaviour != (byte)NPCBehavior.ShopKeeper & Core.Type.NPC[NPCNum].Behaviour != (byte)NPCBehavior.Quest)
                                {
                                    if (targetType == (byte)Core.Enum.TargetType.Player) // player
                                    {
                                        // Check to see if we are following a player or not
                                        if (target > 0)
                                        {
                                            // Check if the player is even playing, if so follow'm
                                            if (NetworkConfig.IsPlaying(target) & GetPlayerMap(target) == mapNum)
                                            {
                                                targetVerify = Conversions.ToBoolean(1);
                                                targetY = GetPlayerY(target);
                                                targetX = GetPlayerX(target);
                                            }
                                            else
                                            {
                                                Core.Type.MapNPC[mapNum].NPC[x].TargetType = 0; // clear
                                                Core.Type.MapNPC[mapNum].NPC[x].Target = 0;
                                            }
                                        }
                                    }
                                    else if (targetType == (byte)Core.Enum.TargetType.NPC)
                                    {
                                        if (target > 0)
                                        {
                                            if (Core.Type.MapNPC[mapNum].NPC[target].Num > 0)
                                            {
                                                targetVerify = Conversions.ToBoolean(1);
                                                targetY = Core.Type.MapNPC[mapNum].NPC[target].Y;
                                                targetX = Core.Type.MapNPC[mapNum].NPC[target].X;
                                            }
                                            else
                                            {
                                                Core.Type.MapNPC[mapNum].NPC[x].TargetType = 0; // clear
                                                Core.Type.MapNPC[mapNum].NPC[x].Target = 0;
                                            }
                                        }
                                    }
                                    else if (targetType == (byte)Core.Enum.TargetType.Pet)
                                    {
                                        if (target > 0)
                                        {
                                            if (Conversions.ToInteger(NetworkConfig.IsPlaying(target)) == 1 & GetPlayerMap(target) == mapNum & Pet.PetAlive(target))
                                            {
                                                targetVerify = Conversions.ToBoolean(1);
                                                targetY = Core.Type.Player[target].Pet.Y;
                                                targetX = Core.Type.Player[target].Pet.X;
                                            }
                                            else
                                            {
                                                Core.Type.MapNPC[mapNum].NPC[x].TargetType = 0; // clear
                                                Core.Type.MapNPC[mapNum].NPC[x].Target = 0;
                                            }
                                        }
                                    }

                                    if (targetVerify)
                                    {
                                        // Gonna make the npcs smarter.. Implementing a pathfinding algorithm.. we shall see what happens.
                                        if (Conversions.ToInteger(Event.IsOneBlockAway(targetX, targetY, (int)MapNPC[mapNum].NPC[x].X, (int)MapNPC[mapNum].NPC[x].Y)) == 0)
                                        {

                                            i = EventLogic.FindNPCPath(mapNum, x, targetX, targetY);
                                            if (i < 4) // Returned an answer. Move the NPC
                                            {
                                                if (NPC.CanNPCMove(mapNum, x, (byte)i))
                                                {
                                                    NPC.NPCMove(mapNum, x, i, (int)MovementType.Walking);
                                                }
                                            }
                                            else // No good path found. Move randomly
                                            {
                                                i = (int)Math.Round(General.Random.NextDouble(1d, 4d));
                                                if (i == 1)
                                                {
                                                    i = (int)Math.Round(General.Random.NextDouble(1d, 4d));

                                                    if (NPC.CanNPCMove(mapNum, x, (byte)i))
                                                    {
                                                        NPC.NPCMove(mapNum, x, i, (int)MovementType.Walking);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            NPC.NPCDir(mapNum, x, Event.GetNPCDir(targetX, targetY, (int)MapNPC[mapNum].NPC[x].X, (int)MapNPC[mapNum].NPC[x].Y));
                                        }
                                    }
                                    else
                                    {
                                        i = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 4f));

                                        if (i == 1)
                                        {
                                            i = (int)Math.Round(Conversion.Int(VBMath.Rnd() * 4f));

                                            if (NPC.CanNPCMove(mapNum, x, (byte)i))
                                            {
                                                NPC.NPCMove(mapNum, x, i, (int)MovementType.Walking);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }

                    // /////////////////////////////////////////////
                    // // This is used for npcs to attack targets //
                    // /////////////////////////////////////////////
                    // Make sure theres a npc with the map
                    if (Core.Type.Map[mapNum].NPC[x] > 0 & Core.Type.MapNPC[mapNum].NPC[x].Num > 0)
                    {
                        target = Core.Type.MapNPC[mapNum].NPC[x].Target;
                        targetType = Core.Type.MapNPC[mapNum].NPC[x].TargetType;

                        // Check if the npc can attack the targeted player player
                        if (target > 0)
                        {

                            if (targetType == (byte)Core.Enum.TargetType.Player) // player
                            {

                                // Is the target playing and on the same map?
                                if (NetworkConfig.IsPlaying(target) & GetPlayerMap(target) == mapNum)
                                {
                                    if (NetworkConfig.IsPlaying(target) & GetPlayerMap(target) == mapNum)
                                    {
                                        if (General.Random.NextDouble(1d, 3d) == 1d)
                                        {
                                            int skillnum = NPC.RandomNPCAttack(mapNum, x);
                                            if (skillnum > 0)
                                            {
                                                NPC.BufferNPCSkill(mapNum, x, skillnum);
                                            }
                                            else
                                            {
                                                NPC.TryNPCAttackPlayer(x, target);
                                            } // , Damage)
                                        }
                                        else
                                        {
                                            NPC.TryNPCAttackPlayer(x, target);
                                        }
                                    }
                                    else
                                    {
                                        // Player left map or game, set target to 0
                                        Core.Type.MapNPC[mapNum].NPC[x].Target = 0;
                                        Core.Type.MapNPC[mapNum].NPC[x].TargetType = 0;

                                    } // clear
                                }
                                else
                                {
                                    // Player left map or game, set target to 0
                                    Core.Type.MapNPC[mapNum].NPC[x].Target = 0;
                                    Core.Type.MapNPC[mapNum].NPC[x].TargetType = 0;
                                } // clear
                            }
                            else if (targetType == (byte)Core.Enum.TargetType.NPC)
                            {
                                if (Core.Type.MapNPC[mapNum].NPC[target].Num > 0) // npc exists
                                {
                                    // Can the npc attack the npc?
                                    if (NPC.CanNPCAttackNPC(mapNum, x, target))
                                    {
                                        damage = (int)((long)Core.Type.NPC[NPCNum].Stat[(byte)StatType.Strength] - (long)Core.Type.NPC[target].Stat[(byte)StatType.Luck]);
                                        NPC.NPCAttackNPC(mapNum, x, target, damage);
                                    }
                                }
                                else
                                {
                                    // npc is dead or non-existant
                                    Core.Type.MapNPC[mapNum].NPC[x].Target = 0;
                                    Core.Type.MapNPC[mapNum].NPC[x].TargetType = 0;
                                } // clear
                            }
                            else if (targetType == (byte)Core.Enum.TargetType.Pet)
                            {
                                if (NetworkConfig.IsPlaying(target) & GetPlayerMap(target) == mapNum & Pet.PetAlive(target))
                                {
                                    Pet.TryNPCAttackPet(x, target);
                                }
                                else
                                {
                                    // Player left map or game, set target to 0
                                    Core.Type.MapNPC[mapNum].NPC[x].Target = 0;
                                    Core.Type.MapNPC[mapNum].NPC[x].TargetType = 0;
                                } // clear
                            }
                        }
                    }

                    // ////////////////////////////////////////////
                    // // This is used for regenerating NPC's HP //
                    // ////////////////////////////////////////////
                    // Check to see if we want to regen some of the npc's hp
                    if (Core.Type.MapNPC[mapNum].NPC[x].Num > 0 & tickCount > Global.GiveNPCHPTimer + 10000)
                    {
                        if (Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.HP] > 0)
                        {
                            Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.HP] = Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.HP] + GetNPCVitalRegen(NPCNum, (VitalType)VitalType.HP);

                            // Check if they have more then they should and if so just set it to Core.Constant.MAX
                            if (Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.HP] > GameLogic.GetNPCMaxVital(NPCNum, (VitalType)VitalType.HP))
                            {
                                Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.HP] = GameLogic.GetNPCMaxVital(NPCNum, (VitalType)VitalType.HP);
                            }
                        }
                    }

                    if (Core.Type.MapNPC[mapNum].NPC[x].Num > 0 & tickCount > Global.GiveNPCMPTimer + 10000 & Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.SP] > 0)
                    {
                        Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.SP] = Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.SP] + GetNPCVitalRegen(NPCNum, (VitalType)VitalType.SP);

                        // Check if they have more then they should and if so just set it to Core.Constant.MAX
                        if (Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.SP] > GameLogic.GetNPCMaxVital(NPCNum, (VitalType)VitalType.SP))
                        {
                            Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.SP] = GameLogic.GetNPCMaxVital(NPCNum, (VitalType)VitalType.SP);
                        }
                    }

                    // ////////////////////////////////////////////////////////
                    // // This is used for checking if an NPC is dead or not //
                    // ////////////////////////////////////////////////////////
                    // Check if the npc is dead or not
                    if (Core.Type.MapNPC[mapNum].NPC[x].Num > 0 & Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.HP] < 0 & Core.Type.MapNPC[mapNum].NPC[x].SpawnWait > 0)
                    {
                        Core.Type.MapNPC[mapNum].NPC[x].Num = 0;
                        Core.Type.MapNPC[mapNum].NPC[x].SpawnWait = General.GetTimeMs();
                        Core.Type.MapNPC[mapNum].NPC[x].Vital[(byte)VitalType.HP] = 0;
                    }

                    // //////////////////////////////////////
                    // // This is used for spawning an NPC //
                    // //////////////////////////////////////
                    // Check if we are supposed to spawn an npc or not
                    if (Core.Type.MapNPC[mapNum].NPC[x].Num == 0 & Core.Type.Map[mapNum].NPC[x] > 0 & Core.Type.NPC[Core.Type.Map[mapNum].NPC[x]].SpawnSecs > 0)
                    {
                        if (tickCount > Core.Type.MapNPC[mapNum].NPC[x].SpawnWait + Core.Type.NPC[Core.Type.Map[mapNum].NPC[x]].SpawnSecs * 1000)
                        {
                            NPC.SpawnNPC(x, mapNum);
                        }
                    }
                }
            }

            // Make sure we reset the timer for npc hp regeneration
            if (General.GetTimeMs() > Global.GiveNPCHPTimer + 10000)
                Global.GiveNPCHPTimer = General.GetTimeMs();

            if (General.GetTimeMs() > Global.GiveNPCMPTimer + 10000)
                Global.GiveNPCMPTimer = General.GetTimeMs();

            // Make sure we reset the timer for door closing
            if (General.GetTimeMs() > Global.KeyTimer + 15000)
                Global.KeyTimer = General.GetTimeMs();

        }

        public static int GetNPCVitalRegen(int NPCNum, VitalType vital)
        {
            int GetNPCVitalRegenRet = default;
            int i;

            GetNPCVitalRegenRet = 0;

            // Prevent subscript out of range
            if (NPCNum <= 0 | NPCNum > Core.Constant.MAX_NPCS)
            {
                return GetNPCVitalRegenRet;
            }

            switch (vital)
            {
                case var @case when @case == (VitalType) VitalType.HP:
                    {
                        i = (int)Core.Type.NPC[NPCNum].Stat[(int)StatType.Vitality] / 3;

                        if (i < 0)
                            i = 0;
                        GetNPCVitalRegenRet = i;
                        break;
                    }
                case var case1 when case1 == (VitalType) VitalType.SP:
                    {
                        i = (int)Core.Type.NPC[NPCNum].Stat[(int)StatType.Intelligence] / 3;

                        if (i < 0)
                            i = 0;
                        GetNPCVitalRegenRet = i;
                        break;
                    }
            }

            return GetNPCVitalRegenRet;

        }

        internal static bool HandlePetSkill(int index)
        {
            bool HandlePetSkillRet = default;
            Pet.PetCastSkill(index, Core.Type.TempPlayer[index].PetSkillBuffer.Skill, Core.Type.TempPlayer[index].PetSkillBuffer.Target, Core.Type.TempPlayer[index].PetSkillBuffer.TargetType, true);
            Core.Type.TempPlayer[index].PetSkillBuffer.Skill = 0;
            Core.Type.TempPlayer[index].PetSkillBuffer.Timer = 0;
            Core.Type.TempPlayer[index].PetSkillBuffer.Target = 0;
            Core.Type.TempPlayer[index].PetSkillBuffer.TargetType = 0;
            HandlePetSkillRet = Conversions.ToBoolean(1);
            return HandlePetSkillRet;
        }

        internal static bool HandleClearStun(int index)
        {
            bool HandleClearStunRet = default;
            Core.Type.TempPlayer[index].StunDuration = 0;
            Core.Type.TempPlayer[index].StunTimer = 0;
            NetworkSend.SendStunned(index);
            HandleClearStunRet = Conversions.ToBoolean(1);
            return HandleClearStunRet;
        }

        internal static bool HandleClearPetStun(int index)
        {
            bool HandleClearPetStunRet = default;
            Core.Type.TempPlayer[index].PetStunDuration = 0;
            Core.Type.TempPlayer[index].PetStunTimer = 0;
            HandleClearPetStunRet = Conversions.ToBoolean(1);
            return HandleClearPetStunRet;
        }

        internal static bool HandleStopPetRegen(int index)
        {
            bool HandleStopPetRegenRet = default;
            Core.Type.TempPlayer[index].PetStopRegen = false;
            Core.Type.TempPlayer[index].PetStopRegenTimer = 0;
            HandleStopPetRegenRet = Conversions.ToBoolean(1);
            return HandleStopPetRegenRet;
        }

        internal static bool HandleCastSkill(int index)
        {
            bool HandleCastSkillRet = default;
            CastSkill(index, Core.Type.TempPlayer[index].SkillBuffer);
            Core.Type.TempPlayer[index].SkillBuffer = 0;
            Core.Type.TempPlayer[index].SkillBufferTimer = 0;
            HandleCastSkillRet = Conversions.ToBoolean(1);
            return HandleCastSkillRet;
        }

        internal static void CastSkill(int index, int SkillSlot)
        {
            // Set up some basic variables we'll be using.
            var skillId = GetPlayerSkill(index, SkillSlot);

            // Preventative checks
            if (!NetworkConfig.IsPlaying(index) | SkillSlot < 0 | SkillSlot > Core.Constant.MAX_PLAYER_SKILLS | !HasSkill(index, skillId))
                return;

            // Check if the player is able to cast the Skill.
            if (GetPlayerVital(index, (VitalType) VitalType.SP) < Core.Type.Skill[skillId].MpCost)
            {
                NetworkSend.PlayerMsg(index, "Not enough mana!", (int) ColorType.BrightRed);
                return;
            }
            else if (GetPlayerLevel(index) < Core.Type.Skill[skillId].LevelReq)
            {
                NetworkSend.PlayerMsg(index, string.Format("You must be level {0} to use this skill.", Core.Type.Skill[skillId].LevelReq), (int) ColorType.BrightRed);
                return;
            }
            else if (GetPlayerAccess(index) < Core.Type.Skill[skillId].AccessReq)
            {
                NetworkSend.PlayerMsg(index, "You must be an administrator to use this skill.", (int) ColorType.BrightRed);
                return;
            }
            else if (!(Core.Type.Skill[skillId].JobReq == 0) & Player.GetPlayerJob(index) != Core.Type.Skill[skillId].JobReq)
            {
                NetworkSend.PlayerMsg(index, string.Format("Only {0} can use this skill.", GameLogic.CheckGrammar(Core.Type.Job[Core.Type.Skill[skillId].JobReq].Name, 1)), (int) ColorType.BrightRed);
                return;
            }
            else if (Core.Type.Skill[skillId].Range > 0 & !IsTargetOnMap(index))
            {
                return;
            }
            else if (Core.Type.Skill[skillId].Range > 0 & !IsInSkillRange(index, skillId) & Core.Type.Skill[skillId].IsProjectile == 0)
            {
                NetworkSend.PlayerMsg(index, "Target not in range.", (int) ColorType.BrightRed);
                NetworkSend.SendClearSkillBuffer(index);
                return;
            }

            // Determine what kind of Skill Type we're dealing with and move on to the appropriate methods.
            if (Core.Type.Skill[skillId].IsProjectile == 1)
            {
                Projectile.PlayerFireProjectile(index, skillId);
            }
            else
            {
                if (Core.Type.Skill[skillId].Range == 0 & !Core.Type.Skill[skillId].IsAoE)
                    HandleSelfCastSkill(index, skillId);
                if (Core.Type.Skill[skillId].Range == 0 & Core.Type.Skill[skillId].IsAoE)
                    HandleSelfCastAoESkill(index, skillId);
                if (Core.Type.Skill[skillId].Range > 0 & Core.Type.Skill[skillId].IsAoE)
                    HandleTargetedAoESkill(index, skillId);
                if (Core.Type.Skill[skillId].Range > 0 & !Core.Type.Skill[skillId].IsAoE)
                    HandleTargetedSkill(index, skillId);
            }

            // Do everything we need to do at the end of the cast.
            FinalizeCast(index, GetPlayerSkill(index, skillId), Core.Type.Skill[skillId].MpCost);
        }

        private static void HandleSelfCastAoESkill(int index, int skillId)
        {

            // Set up some variables we'll definitely be using.
            var centerX = GetPlayerX(index);
            var centerY = GetPlayerY(index);

            // Determine what kind of Skill we're dealing with and process it.
            switch (Core.Type.Skill[skillId].Type)
            {
                case (byte)SkillType.DamageHp:
                case (byte)SkillType.DamageMp:
                case (byte)SkillType.HealHp:
                case (byte)SkillType.HealMp:
                    {
                        HandleAoE(index, skillId, centerX, centerY);
                        break;
                    }

                default:
                    {
                        throw new NotImplementedException();
                    }
            }

        }

        private static void HandleTargetedAoESkill(int index, int skillId)
        {

            // Set up some variables we'll definitely be using.
            int centerX;
            int centerY;
            switch (Core.Type.TempPlayer[index].TargetType)
            {
                case (byte)TargetType.NPC:
                    {
                        centerX = Core.Type.MapNPC[GetPlayerMap(index)].NPC[Core.Type.TempPlayer[index].Target].X;
                        centerY = Core.Type.MapNPC[GetPlayerMap(index)].NPC[Core.Type.TempPlayer[index].Target].Y;
                        break;
                    }

                case (byte)TargetType.Player:
                    {
                        centerX = GetPlayerX(Core.Type.TempPlayer[index].Target);
                        centerY = GetPlayerY(Core.Type.TempPlayer[index].Target);
                        break;
                    }

                default:
                    {
                        throw new NotImplementedException();
                    }

            }

            // Determine what kind of Skill we're dealing with and process it.
            switch (Core.Type.Skill[skillId].Type)
            {
                case (byte)SkillType.HealMp:
                case (byte)SkillType.DamageHp:
                case (byte)SkillType.DamageMp:
                case (byte)SkillType.HealHp:
                    {
                        HandleAoE(index, skillId, centerX, centerY);
                        break;
                    }

                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        private static void HandleSelfCastSkill(int index, int skillId)
        {
            // Determine what kind of Skill we're dealing with and process it.
            switch (Core.Type.Skill[skillId].Type)
            {
                case (byte)SkillType.HealHp:
                    {
                        SkillPlayer_Effect((byte) VitalType.HP, true, index, Core.Type.Skill[skillId].Vital, skillId);
                        break;
                    }
                case (byte)SkillType.HealMp:
                    {
                        SkillPlayer_Effect((byte) VitalType.SP, true, index, Core.Type.Skill[skillId].Vital, skillId);
                        break;
                    }
                case (byte)SkillType.Warp:
                    {
                        Animation.SendAnimation(GetPlayerMap(index), Core.Type.Skill[skillId].SkillAnim, 0, 0, (byte)TargetType.Player, index);
                        Player.PlayerWarp(index, Core.Type.Skill[skillId].Map, Core.Type.Skill[skillId].X, Core.Type.Skill[skillId].Y);
                        break;
                    }
            }

            // Play our animation.
            Animation.SendAnimation(GetPlayerMap(index), Core.Type.Skill[skillId].SkillAnim, 0, 0, (byte)TargetType.Player, index);
        }

        private static void HandleTargetedSkill(int index, int skillId)
        {
            // Set up some variables we'll definitely be using.
            VitalType vital;
            bool dealsDamage;
            int amount = Core.Type.Skill[skillId].Vital;
            var target = Core.Type.TempPlayer[index].Target;

            // Determine what vital we need to adjust and how.
            switch (Core.Type.Skill[skillId].Type)
            {
                case (byte)SkillType.DamageHp:
                    {
                        vital = (VitalType) VitalType.HP;
                        dealsDamage = Conversions.ToBoolean(1);
                        break;
                    }

                case (byte)SkillType.DamageMp:
                    {
                        vital = (VitalType) VitalType.SP;
                        dealsDamage = Conversions.ToBoolean(1);
                        break;
                    }

                case (byte)SkillType.HealHp:
                    {
                        vital = (VitalType) VitalType.HP;
                        dealsDamage = Conversions.ToBoolean(0);
                        break;
                    }

                case (byte)SkillType.HealMp:
                    {
                        vital = (VitalType) VitalType.SP;
                        dealsDamage = Conversions.ToBoolean(0);
                        break;
                    }

                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            switch (Core.Type.TempPlayer[index].TargetType)
            {
                case var case4 when case4 == (byte)TargetType.NPC:
                    {
                        // Deal with damaging abilities.
                        if (dealsDamage & Player.CanPlayerAttackNPC(index, target, true))
                            SkillNPC_Effect((byte)vital, false, target, amount, skillId, GetPlayerMap(index));

                        // Deal with healing abilities
                        if (!dealsDamage)
                            SkillNPC_Effect((byte)vital, true, target, amount, skillId, GetPlayerMap(index));

                        // Handle our NPC death if it kills them
                        if (Conversions.ToBoolean(NPC.IsNPCDead(GetPlayerMap(index), Core.Type.TempPlayer[index].Target)))
                        {
                            Player.HandlePlayerKillNPC(GetPlayerMap(index), index, Core.Type.TempPlayer[index].Target);
                        }

                        break;
                    }

                case var case5 when case5 == (byte)TargetType.Player:
                    {

                        // Deal with damaging abilities.
                        if (dealsDamage & Player.CanPlayerAttackPlayer(index, target, true))
                            SkillPlayer_Effect((byte)vital, false, target, amount, skillId);

                        // Deal with healing abilities
                        if (!dealsDamage)
                            SkillPlayer_Effect((byte)vital, true, target, amount, skillId);

                        if (Conversions.ToBoolean(Player.IsPlayerDead(target)))
                        {
                            // Actually kill the player.
                            Player.OnDeath(target);

                            // Handle PK stuff.
                            Player.HandlePlayerKilledPK(index, target);
                        }

                        break;
                    }

                default:
                    {
                        throw new NotImplementedException();
                    }

            }

            // Play our animation.
            Animation.SendAnimation(GetPlayerMap(index), Core.Type.Skill[skillId].SkillAnim, 0, 0, Core.Type.TempPlayer[index].TargetType, target);
        }

        private static void HandleAoE(int index, int skillId, int x, int y)
        {
            // Get some basic things set up.
            var map = GetPlayerMap(index);
            int range = Core.Type.Skill[skillId].Range;
            int amount = Core.Type.Skill[skillId].Vital;
            VitalType vital;
            bool dealsDamage;

            // Determine what vital we need to adjust and how.
            switch (Core.Type.Skill[skillId].Type)
            {
                case (byte)SkillType.DamageHp:
                    {
                        vital = (VitalType)VitalType.HP;
                        dealsDamage = true;
                        break;
                    }

                case (byte)SkillType.DamageMp:
                    {
                        vital = (VitalType)VitalType.SP;
                        dealsDamage = true;
                        break;
                    }

                case (byte)SkillType.HealHp:
                    {
                        vital = (VitalType)VitalType.HP;
                        dealsDamage = false;
                        break;
                    }

                case (byte)SkillType.HealMp:
                    {
                        vital = (VitalType)VitalType.SP;
                        dealsDamage = false;
                        break;
                    }

                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            // Loop through all online players on the current MyMap.
            foreach (var id in Core.Type.TempPlayer.Where(p => p.InGame).Select((p, i) => i + 1).Where(i => GetPlayerMap(i) == map && i != index).ToArray())
            {
                if (Player.IsInRange(range, x, y, GetPlayerX(id), GetPlayerY(id)))
                {

                    // Deal with damaging abilities.
                    if (dealsDamage && Player.CanPlayerAttackPlayer(index, id, true))
                        SkillPlayer_Effect((byte)vital, false, id, amount, skillId);

                    // Deal with healing abilities
                    if (!dealsDamage)
                        SkillPlayer_Effect((byte)vital, true, id, amount, skillId);

                    // Send our animation to the MyMap.
                    Animation.SendAnimation(map, Core.Type.Skill[skillId].SkillAnim, 0, 0, (byte)TargetType.Player, id);

                    if ((bool)Player.IsPlayerDead(id))
                    {
                        // Actually kill the player.
                        Player.OnDeath(id);

                        // Handle PK stuff.
                        Player.HandlePlayerKilledPK(index, id);
                    }
                }
            }

            // Loop through all the NPCs on this map
            foreach (var id in Core.Type.MapNPC[map].NPC.Where(n => n.Num > 0 && n.Vital[(byte)VitalType.HP] > 0).Select((n, i) => i + 1).ToArray())
            {
                if (Player.IsInRange(range, x, y, Core.Type.MapNPC[map].NPC[id].X, Core.Type.MapNPC[map].NPC[id].Y))
                {

                    // Deal with damaging abilities.
                    if (dealsDamage && Player.CanPlayerAttackNPC(index, id, true))
                        SkillNPC_Effect((byte)vital, false, id, amount, skillId, map);

                    // Deal with healing abilities
                    if (!dealsDamage)
                        SkillNPC_Effect((byte)vital, true, id, amount, skillId, map);

                    // Send our animation to the MyMap.
                    Animation.SendAnimation(map, Core.Type.Skill[skillId].SkillAnim, 0, 0, (byte)TargetType.NPC, id);

                    // Handle our NPC death if it kills them
                    if ((bool)NPC.IsNPCDead(map, id))
                    {
                        Player.HandlePlayerKillNPC(map, index, id);
                    }
                }
            }
        }

        private static void FinalizeCast(int index, int SkillSlot, int skillCost)
        {
            SetPlayerVital(index, (VitalType) VitalType.SP, GetPlayerVital(index, (VitalType) VitalType.SP) - skillCost);
            NetworkSend.SendVital(index, (VitalType) VitalType.SP);
            Core.Type.TempPlayer[index].SkillCD[SkillSlot] = General.GetTimeMs() + Core.Type.Skill[SkillSlot].CdTime * 1000;
            NetworkSend.SendCooldown(index, SkillSlot);
        }

        private static bool IsTargetOnMap(int index)
        {
            bool IsTargetOnMapRet = default;
            if (Core.Type.TempPlayer[index].TargetType == (byte)TargetType.Player)
            {
                if (GetPlayerMap(Core.Type.TempPlayer[index].Target) == GetPlayerMap(index))
                    IsTargetOnMapRet = Conversions.ToBoolean(1);
            }
            else if (Core.Type.TempPlayer[index].TargetType == (byte)TargetType.NPC)
            {
                if (Core.Type.TempPlayer[index].Target > 0 & Core.Type.TempPlayer[index].Target <= Core.Constant.MAX_MAP_NPCS & Core.Type.MapNPC[GetPlayerMap(index)].NPC[Core.Type.TempPlayer[index].Target].Vital[(byte) VitalType.HP] > 0)
                    IsTargetOnMapRet = Conversions.ToBoolean(1);
            }

            return IsTargetOnMapRet;
        }

        private static bool IsInSkillRange(int index, int SkillId)
        {
            bool IsInSkillRangeRet = default;
            var targetX = default(int);
            var targetY = default(int);

            if (Core.Type.TempPlayer[index].TargetType == (byte)TargetType.Player)
            {
                targetX = GetPlayerX(Core.Type.TempPlayer[index].Target);
                targetY = GetPlayerY(Core.Type.TempPlayer[index].Target);
            }
            else if (Core.Type.TempPlayer[index].TargetType == (byte)TargetType.NPC)
            {
                targetX = Core.Type.MapNPC[GetPlayerMap(index)].NPC[Core.Type.TempPlayer[index].Target].X;
                targetY = Core.Type.MapNPC[GetPlayerMap(index)].NPC[Core.Type.TempPlayer[index].Target].Y;
            }

            IsInSkillRangeRet = Player.IsInRange(Core.Type.Skill[SkillId].Range, GetPlayerX(index), GetPlayerY(index), targetX, targetY);
            return IsInSkillRangeRet;
        }

        internal static void CastNPCSkill(int NPCNum, int mapNum, int SkillSlot)
        {
            int skillnum;
            int mpCost;
            int vital;
            bool didCast;
            var i = default(int);
            int aoe;
            int range;
            byte vitalType;
            var increment = default(bool);
            var x = default(int);
            var y = default(int);

            byte targetType;
            var target = default(int);
            int skillCastType;

            didCast = Conversions.ToBoolean(0);

            // Prevent subscript out of range
            if (SkillSlot < 0 | SkillSlot > Core.Constant.MAX_NPC_SKILLS)
                return;

            skillnum = NPC.GetNPCSkill(Core.Type.MapNPC[mapNum].NPC[NPCNum].Num, SkillSlot);

            mpCost = Core.Type.Skill[skillnum].MpCost;

            // Check if they have enough MP
            if (Core.Type.MapNPC[mapNum].NPC[NPCNum].Vital[(int)VitalType.SP] < mpCost)
                return;

            // find out what kind of skill it is! self cast, target or AOE
            if (Core.Type.Skill[skillnum].IsProjectile == 1)
            {
                skillCastType = 4; // Projectile
            }
            else if (Core.Type.Skill[skillnum].Range > 0)
            {
                // ranged attack, single target or aoe?
                if (!Core.Type.Skill[skillnum].IsAoE)
                {
                    skillCastType = 2; // targetted
                }
                else
                {
                    skillCastType = 3;
                } // targetted aoe
            }
            else if (!Core.Type.Skill[skillnum].IsAoE)
            {
                skillCastType = 0; // self-cast
            }
            else
            {
                skillCastType = 0;
            } // self-cast AoE

            // set the Core.VitalType
            vital = Core.Type.Skill[skillnum].Vital;
            aoe = Core.Type.Skill[skillnum].AoE;
            range = Core.Type.Skill[skillnum].Range;

            switch (skillCastType)
            {
                case 0: // self-cast target
                    {
                        break;
                    }
                // Select Case Type.Skill(skillNum).Type
                // Case (byte)SkillType.HEALHP
                // SkillPlayer_Effect((VitalType) VitalType.HP, True, NPCNum, Vital, skillnum)
                // DidCast = 1
                // Case (byte)SkillType.HEALMP
                // SkillPlayer_Effect((VitalType) VitalType.SP, True, NPCNum, Vital, skillnum)
                // DidCast = 1
                // Case (byte)SkillType.WARP
                // SendAnimation(MapNum, Type.Skill(skillNum).SkillAnim, 0, 0, TargetType.PLAYER, NPCNum)
                // PlayerWarp(NPCNum, Type.Skill(skillNum).Map, Type.Skill(skillNum).x, Type.Skill(skillNum).y)
                // SendAnimation(GetPlayerMap(NPCNum), Type.Skill(skillNum).SkillAnim, 0, 0, TargetType.PLAYER, NPCNum)
                // DidCast = 1
                // End Select

                case 1:
                case 3: // self-cast AOE & targetted AOE
                    {
                        if (skillCastType == 1)
                        {
                            x = Core.Type.MapNPC[mapNum].NPC[NPCNum].X;
                            y = Core.Type.MapNPC[mapNum].NPC[NPCNum].Y;
                        }
                        else if (skillCastType == 3)
                        {
                            targetType = Core.Type.MapNPC[mapNum].NPC[NPCNum].TargetType;
                            target = Core.Type.MapNPC[mapNum].NPC[NPCNum].Target;

                            if (targetType == 0)
                                return;
                            if (target == 0)
                                return;

                            if (targetType == (byte) Core.Enum.TargetType.Player)
                            {
                                x = GetPlayerX(target);
                                y = GetPlayerY(target);
                            }
                            else
                            {
                                x = Core.Type.MapNPC[mapNum].NPC[target].X;
                                y = Core.Type.MapNPC[mapNum].NPC[target].Y;
                            }

                            if (!Player.IsInRange(range, x, y, GetPlayerX(NPCNum), GetPlayerY(NPCNum)))
                            {
                                return;
                            }
                        }
                        switch (Core.Type.Skill[skillnum].Type)
                        {
                            case var @case when @case == (byte)SkillType.DamageHp:
                                {
                                    didCast = Conversions.ToBoolean(1);
                                    var loopTo = NetworkConfig.Socket.HighIndex;
                                    for (i = 0; i <= (int)loopTo; i++)
                                    {
                                        if (NetworkConfig.IsPlaying(i))
                                        {
                                            if (GetPlayerMap(i) == mapNum)
                                            {
                                                if (Player.IsInRange(aoe, x, y, GetPlayerX(i), GetPlayerY(i)))
                                                {
                                                    if (NPC.CanNPCAttackPlayer(NPCNum, i))
                                                    {
                                                        Animation.SendAnimation(mapNum, Core.Type.Skill[skillnum].SkillAnim, 0, 0, (byte) Core.Enum.TargetType.Player, i);
                                                        NetworkSend.PlayerMsg(i, Core.Type.NPC[Core.Type.MapNPC[mapNum].NPC[NPCNum].Num].Name + " uses " + Core.Type.Skill[skillnum].Name + "!", (int) ColorType.Yellow);
                                                        SkillPlayer_Effect((int)VitalType.HP, false, i, vital, skillnum);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var loopTo1 = Core.Constant.MAX_MAP_NPCS - 1;
                                    for (i = 0; i <= (int)loopTo1; i++)
                                    {
                                        if (Core.Type.MapNPC[mapNum].NPC[i].Num > 0)
                                        {
                                            if (Core.Type.MapNPC[mapNum].NPC[i].Vital[((int)VitalType.HP)] > 0)
                                            {
                                                if (Player.IsInRange(aoe, x, y, Core.Type.MapNPC[mapNum].NPC[i].X, Core.Type.MapNPC[mapNum].NPC[i].Y))
                                                {
                                                    if (Player.CanPlayerAttackNPC(NPCNum, i, true))
                                                    {
                                                        Animation.SendAnimation(mapNum, Core.Type.Skill[skillnum].SkillAnim, 0, 0, (byte) Core.Enum.TargetType.NPC, i);
                                                        SkillNPC_Effect((int)VitalType.HP, false, i, vital, skillnum, mapNum);
                                                        if (Core.Type.Skill[skillnum].KnockBack == 1)
                                                        {
                                                            NPC.KnockBackNPC(NPCNum, target, skillnum);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                            case var case1 when case1 == (byte)SkillType.HealHp:
                            case var case2 when case2 == (byte)SkillType.HealMp:
                            case var case3 when case3 == (byte)SkillType.DamageMp:
                                {
                                    if (Core.Type.Skill[skillnum].Type == (byte)SkillType.HealHp)
                                    {
                                        vital = (int)VitalType.HP;
                                        increment = Conversions.ToBoolean(1);
                                    }
                                    else if (Core.Type.Skill[skillnum].Type == (byte)SkillType.HealMp)
                                    {
                                        vital = (int)VitalType.SP;
                                        increment = Conversions.ToBoolean(1);
                                    }
                                    else if (Core.Type.Skill[skillnum].Type == (byte)SkillType.DamageMp)
                                    {
                                        vital = (int)VitalType.SP;
                                        increment = Conversions.ToBoolean(0);
                                    }

                                    didCast = Conversions.ToBoolean(1);
                                    var loopTo2 = NetworkConfig.Socket.HighIndex;
                                    for (i = 0; i <= (int)loopTo2; i++)
                                    {
                                        if (NetworkConfig.IsPlaying(i) & GetPlayerMap(i) == GetPlayerMap(NPCNum))
                                        {
                                            if (Player.IsInRange(aoe, x, y, GetPlayerX(i), GetPlayerY(i)))
                                            {
                                                SkillPlayer_Effect((byte)vital, increment, i, vital, skillnum);
                                            }
                                        }
                                    }

                                    var loopTo3 = Core.Constant.MAX_MAP_NPCS - 1;
                                    for (i = 0; i <= (int)loopTo3; i++)
                                    {
                                        if (Core.Type.MapNPC[mapNum].NPC[i].Num > 0 & Core.Type.MapNPC[mapNum].NPC[i].Vital[(int)VitalType.HP] > 0)
                                        {
                                            if (Player.IsInRange(aoe, x, y, Core.Type.MapNPC[mapNum].NPC[i].X, Core.Type.MapNPC[mapNum].NPC[i].Y))
                                            {
                                                SkillNPC_Effect((byte)vital, increment, i, vital, skillnum, mapNum);
                                            }
                                        }
                                    }

                                    break;
                                }
                        }

                        break;
                    }

                case 2: // targetted
                    {

                        targetType = Core.Type.MapNPC[mapNum].NPC[NPCNum].TargetType;
                        target = Core.Type.MapNPC[mapNum].NPC[NPCNum].Target;

                        if (targetType == 0 | target == 0)
                            return;

                        if (Core.Type.MapNPC[mapNum].NPC[NPCNum].TargetType == (byte) Core.Enum.TargetType.Player)
                        {
                            x = GetPlayerX(target);
                            y = GetPlayerY(target);
                        }
                        else
                        {
                            x = Core.Type.MapNPC[mapNum].NPC[target].X;
                            y = Core.Type.MapNPC[mapNum].NPC[target].Y;
                        }

                        if (!Player.IsInRange(range, Core.Type.MapNPC[mapNum].NPC[NPCNum].X, Core.Type.MapNPC[mapNum].NPC[NPCNum].Y, x, y))
                            return;

                        switch (Core.Type.Skill[skillnum].Type)
                        {
                            case var case4 when case4 == (byte)SkillType.DamageHp:
                                {
                                    if (Core.Type.MapNPC[mapNum].NPC[NPCNum].TargetType == (byte) Core.Enum.TargetType.Player)
                                    {
                                        if (NPC.CanNPCAttackPlayer(NPCNum, target) & vital > 0)
                                        {
                                            Animation.SendAnimation(mapNum, Core.Type.Skill[skillnum].SkillAnim, 0, 0, (byte) Core.Enum.TargetType.Player, target);
                                            NetworkSend.PlayerMsg(target, Core.Type.NPC[Core.Type.MapNPC[mapNum].NPC[NPCNum].Num].Name + " uses " + Core.Type.Skill[skillnum].Name + "!", (int) ColorType.Yellow);
                                            SkillPlayer_Effect((int)VitalType.HP, false, target, vital, skillnum);
                                            didCast = Conversions.ToBoolean(1);
                                        }
                                    }
                                    else if (Player.CanPlayerAttackNPC(NPCNum, target, true) & vital > 0)
                                    {
                                        Animation.SendAnimation(mapNum, Core.Type.Skill[skillnum].SkillAnim, 0, 0, (byte) Core.Enum.TargetType.NPC, target);
                                        SkillNPC_Effect((int)VitalType.HP, false, i, vital, skillnum, mapNum);

                                        if (Core.Type.Skill[skillnum].KnockBack == 1)
                                        {
                                            NPC.KnockBackNPC(NPCNum, target, skillnum);
                                        }
                                        didCast = Conversions.ToBoolean(1);
                                    }

                                    break;
                                }

                            case var case5 when case5 == (byte)SkillType.DamageMp:
                            case var case6 when case6 == (byte)SkillType.HealMp:
                            case var case7 when case7 == (byte)SkillType.HealHp:
                                {
                                    if (Core.Type.Skill[skillnum].Type == (byte)SkillType.DamageMp)
                                    {
                                        vital = (int)VitalType.SP;
                                        increment = Conversions.ToBoolean(0);
                                    }
                                    else if (Core.Type.Skill[skillnum].Type == (byte)SkillType.HealMp)
                                    {
                                        vital = (int)VitalType.SP;
                                        increment = Conversions.ToBoolean(1);
                                    }
                                    else if (Core.Type.Skill[skillnum].Type == (byte)SkillType.HealHp)
                                    {
                                        vital = (int)VitalType.HP;
                                        increment = Conversions.ToBoolean(1);
                                    }

                                    if (Core.Type.TempPlayer[NPCNum].TargetType == (byte) Core.Enum.TargetType.Player)
                                    {
                                        if (Core.Type.Skill[skillnum].Type == (byte)SkillType.DamageMp)
                                        {
                                            if (Player.CanPlayerAttackPlayer(NPCNum, target, true))
                                            {
                                                SkillPlayer_Effect((byte)vital, increment, target, vital, skillnum);
                                            }
                                        }
                                        else
                                        {
                                            SkillPlayer_Effect((byte)vital, increment, target, vital, skillnum);
                                        }
                                    }
                                    else if (Core.Type.Skill[skillnum].Type == (byte)SkillType.DamageMp)
                                    {
                                        if (Player.CanPlayerAttackNPC(NPCNum, target, true))
                                        {
                                            SkillNPC_Effect((byte)vital, increment, target, vital, skillnum, mapNum);
                                        }
                                    }
                                    else
                                    {
                                        SkillNPC_Effect((byte)vital, increment, target, vital, skillnum, mapNum);
                                    }

                                    break;
                                }
                        }

                        break;
                    }
                case 4: // Projectile
                    {
                        Projectile.PlayerFireProjectile(NPCNum, skillnum);

                        didCast = Conversions.ToBoolean(1);
                        break;
                    }
            }

            if (didCast)
            {
                Core.Type.MapNPC[mapNum].NPC[NPCNum].Vital[(int)VitalType.SP] = Core.Type.MapNPC[mapNum].NPC[NPCNum].Vital[(int)VitalType.SP] - mpCost;
                NPC.SendMapNPCVitals(mapNum, (byte)NPCNum);
                Core.Type.MapNPC[mapNum].NPC[NPCNum].SkillCD[SkillSlot] = General.GetTimeMs() + Core.Type.Skill[skillnum].CdTime * 1000;
            }
        }

        internal static void SkillPlayer_Effect(byte vital, bool increment, int index, int damage, int skillnum)
        {
            string sSymbol;
            var Color = default(int);

            if (damage > 0)
            {
                // Calculate for Magic Resistance.
                damage -= GetPlayerStat(index, StatType.Spirit) * 2 + GetPlayerLevel(index) * 3;

                if (increment)
                {
                    sSymbol = "+";
                    if (vital == (byte)VitalType.HP)
                        Color = (int) ColorType.BrightGreen;
                    if (vital == (byte) VitalType.SP)
                        Color = (int) ColorType.BrightBlue;
                }
                else
                {
                    sSymbol = "-";
                    Color = (int) ColorType.BrightRed;
                }

                // Deal with stun effects.
                if (Core.Type.Skill[skillnum].StunDuration > 0)
                    Player.StunPlayer(index, skillnum);

                NetworkSend.SendActionMsg(GetPlayerMap(index), sSymbol + damage, Color, (byte) ActionMsgType.Scroll, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                if (increment)
                    SetPlayerVital(index, (VitalType)vital, GetPlayerVital(index, (VitalType)vital) + damage);
                if (!increment)
                    SetPlayerVital(index, (VitalType)vital, GetPlayerVital(index, (VitalType)vital) - damage);
                NetworkSend.SendVital(index, (VitalType)vital);
            }
        }

        internal static void SkillNPC_Effect(byte vital, bool increment, int index, int damage, int skillnum, int mapNum)
        {
            string sSymbol;
            var color = default(int);

            if (index <= 0 | index > Core.Constant.MAX_MAP_NPCS | damage <= 0 | Core.Type.MapNPC[mapNum].NPC[index].Vital[vital] <= 0)
                return;

            if (damage > 0)
            {
                if (increment)
                {
                    sSymbol = "+";
                    if (vital == (int) VitalType.HP)
                        color = (int) ColorType.BrightGreen;
                    if (vital == (int) VitalType.SP)
                        color = (int) ColorType.BrightBlue;
                }
                else
                {
                    sSymbol = "-";
                    color = (int) ColorType.BrightRed;
                }

                // Deal with Stun and Knockback effects.
                if (Core.Type.Skill[skillnum].KnockBack == 1)
                    NPC.KnockBackNPC(index, index, skillnum);
                if (Core.Type.Skill[skillnum].StunDuration > 0)
                    Player.StunNPC(index, mapNum, skillnum);

                NetworkSend.SendActionMsg(mapNum, sSymbol + damage, color, (byte) ActionMsgType.Scroll, Core.Type.MapNPC[mapNum].NPC[index].X * 32, Core.Type.MapNPC[mapNum].NPC[index].Y * 32);
                if (increment)
                    Core.Type.MapNPC[mapNum].NPC[index].Vital[vital] = Core.Type.MapNPC[mapNum].NPC[index].Vital[vital] + damage;
                if (!increment)
                    Core.Type.MapNPC[mapNum].NPC[index].Vital[vital] = Core.Type.MapNPC[mapNum].NPC[index].Vital[vital] - damage;
                NPC.SendMapNPCVitals(mapNum, (byte)index);
            }
        }

    }
}