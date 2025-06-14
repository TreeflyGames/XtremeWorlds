using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Diagnostics;
using System.Linq;
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

                if (tick > tmr1000)
                {
                    Clock.Instance.Tick();

                    // Move the timer up 1000ms.
                    tmr1000 = General.GetTimeMs() + 1000;
                }

                if (tick > tmr500)
                {
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
                // Make sure no one is on the map when it respawns
                if (!PlayersOnMap[y])
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
        }

        private static void UpdateMapAI()
        {
            var now = General.GetTimeMs();
            var maxMaps = Core.Constant.MAX_MAPS;
            var maxMapItems = Core.Constant.MAX_MAP_ITEMS;
            var maxMapNpcs = Core.Constant.MAX_MAP_NPCS;

            for (int mapNum = 0; mapNum < maxMaps; mapNum++)
            {
                if (General.IsServerDestroyed)
                    return;

                // Handle map items (public/despawn)
                for (int i = 0; i < maxMapItems; i++)
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