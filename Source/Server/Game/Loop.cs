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
using static Core.Global.Command;
using static Core.Type;

namespace Server
{

    public class Loop
    {

        public static async System.Threading.Tasks.Task ServerAsync()
        {
            int tick;
            var tmr25 = default(int);
            var tmr500 = default(int);
            var tmr250 = default(int);
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
                    Debugger.Break(); var onlinePlayers = Core.Data.TempPlayer.Where(player => player.InGame).Select((player, index) => new { Index = Operators.AddObject(index, 1), player }).ToArray();

                await General.CheckShutDownCountDownAsync();

                if (tick > tmr25)
                {                
                    // Update all our available events.
                    EventLogic.UpdateEventLogic();

                    // Move the timer up 25ms.
                    tmr25 = General.GetTimeMs() + 25;
                }
                
                if (tick > tmr250)
                {
                    for (int index = 0; index < NetworkConfig.Socket.HighIndex; index++)
                    {
                        if (Core.Data.Player[index].Moving > 0 && Core.Data.Player[index].IsMoving)
                        {
                            Player.PlayerMove(index, Core.Data.Player[index].Dir, Core.Data.Player[index].Moving, false);
                        }
                    }
                    
                    // Move the timer up 250ms.
                    tmr250 = General.GetTimeMs() + 250;
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

                await System.Threading.Tasks.Task.Delay(1);
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
                    Database.SaveCharacter(i, Core.Data.TempPlayer[i].Slot);
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
                // Add Npcs
                for (int i = 0; i < Core.Constant.MAX_MAP_NPCS; i++)
                {
                    var npc = Core.Globals.Entity.FromNpc(i, Data.MapNpc[mapNum].Npc[i]);
                    if (npc.Num >= 0)
                    {
                        npc.Map = mapNum;
                        entities.Add(npc);
                    }
                }

                // Add Players
                for (int i = 0; i < NetworkConfig.Socket.HighIndex; i++)
                {
                    if (Core.Data.Player[i].Map == mapNum)
                    {
                        var player = Core.Globals.Entity.FromPlayer(i, Core.Data.Player[i]);
                        if (player.Num >= 0)
                        {
                            player.Map = mapNum;
                            entities.Add(player);
                        }
                    }
                }
            }
            
            Script.Instance?.UpdateMapAI();

            foreach (Core.Globals.Entity entity in Core.Globals.Entity.Instances)
            {
                switch (entity.Type)
                {
                    case Core.Globals.Entity.EntityType.Npc:
                        Data.MapNpc[entity.Map].Npc[Core.Globals.Entity.Index(entity)] = Core.Globals.Entity.ToNpc(entity.Id, entity);
                        break;
                    case Core.Globals.Entity.EntityType.Player:
                        Core.Data.Player[Core.Globals.Entity.Index(entity)] = Core.Globals.Entity.ToPlayer(entity.Id, entity);
                        break;
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