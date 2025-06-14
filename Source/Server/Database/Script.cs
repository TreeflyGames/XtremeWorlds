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

    public void BufferSkill(int index, int skillNum)
    {

    }

    public void UpdateMapAI()
    {
        var now = General.GetTimeMs();
        var maxMaps = Core.Constant.MAX_MAPS;
        var maxMapItems = Core.Constant.MAX_MAP_ITEMS;
        var maxMapNpcs = Core.Constant.MAX_MAP_NPCS;

        for (int mapNum = 0; mapNum < maxMaps; mapNum++)
        {
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