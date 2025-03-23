using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Packets;

namespace Server
{
    public class Resource
    {
        #region Database

        public static void SaveResource(int resourceNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Resource[resourceNum]).ToString();
            if (Database.RowExists(resourceNum, "resource"))
            {
                Database.UpdateRow(resourceNum, json, "resource", "data");
            }
            else
            {
                Database.InsertRow(resourceNum, json, "resource");
            }
        }

        public static async Task SaveAllResourcesAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_RESOURCES)
                .Select(i => Task.Run(() => SaveResource(i)));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadResourcesAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_RESOURCES)
                .Select(i => Task.Run(() => LoadResourceAsync(i)));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadResourceAsync(int resourceNum)
        {
            JObject data = await Database.SelectRowAsync(resourceNum, "resource", "data");
            if (data == null)
            {
                ClearResource(resourceNum);
                return;
            }
            var resourceData = JObject.FromObject(data).ToObject<Core.Type.ResourceStruct>();
            Core.Type.Resource[resourceNum] = resourceData;
        }

        public static void ClearResource(int index)
        {
            Core.Type.Resource[index].Name = "";
            Core.Type.Resource[index].EmptyMessage = "";
            Core.Type.Resource[index].SuccessMessage = "";
            Core.Type.Resource[index].Quality = ResourceQuality.Common; // Default quality
            Core.Type.Resource[index].DepletionStatus = DepletionStatus.Active; // Default status
        }

        #endregion

        #region Resource Nodes

        public class ResourceNode
        {
            public int X { get; set; }
            public int Y { get; set; }
            public byte Health { get; set; }
            public ResourceState State { get; set; } = ResourceState.Available;
            public DateTime RespawnTimer { get; set; }
            public ResourceQuality Quality { get; set; } = ResourceQuality.Common;
        }

        public enum ResourceState
        {
            Available,
            Depleted,
            Regenerating
        }

        public enum ResourceQuality
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary
        }

        public enum DepletionStatus
        {
            Active,
            Depleted
        }

        #endregion

        #region Caching

        public static void CacheResources(int mapNum)
        {
            int resourceCount = 0;
            var nodes = new List<ResourceNode>();

            for (int x = 0; x <= Core.Type.Map[mapNum].MaxX; x++)
            {
                for (int y = 0; y <= Core.Type.Map[mapNum].MaxY; y++)
                {
                    if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.Resource ||
                        Core.Type.Map[mapNum].Tile[x, y].Type2 == TileType.Resource)
                    {
                        resourceCount++;
                        nodes.Add(new ResourceNode
                        {
                            X = x,
                            Y = y,
                            Health = (byte)Core.Type.Resource[Core.Type.Map[mapNum].Tile[x, y].Data1].Health,
                            Quality = (ResourceQuality)Core.Type.Resource[Core.Type.Map[mapNum].Tile[x, y].Data1].Quality
                        });
                    }
                }
            }

            Core.Type.MapResource[mapNum].ResourceData = nodes.ToArray();
            Core.Type.MapResource[mapNum].ResourceCount = resourceCount;
        }

        #endregion

        #region Packet Handling

        public static byte[] ResourcesData()
        {
            var buffer = new ByteStream(4);
            for (int i = 0; i < Core.Constant.MAX_RESOURCES; i++)
            {
                if (string.IsNullOrEmpty(Core.Type.Resource[i].Name)) continue;
                buffer.WriteBlock(ResourceData(i));
            }
            return buffer.ToArray();
        }

        public static byte[] ResourceData(int resourceNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32(resourceNum);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].Animation);
            buffer.WriteString(Core.Type.Resource[resourceNum].EmptyMessage);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].ExhaustedImage);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].Health);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].ExpReward);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].ItemReward);
            buffer.WriteString(Core.Type.Resource[resourceNum].Name);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].ResourceImage);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].ResourceType);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].RespawnTime);
            buffer.WriteString(Core.Type.Resource[resourceNum].SuccessMessage);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].LvlRequired);
            buffer.WriteInt32(Core.Type.Resource[resourceNum].ToolRequired);
            buffer.WriteInt32(Convert.ToInt32(Core.Type.Resource[resourceNum].Walkthrough));
            buffer.WriteInt32((int)Core.Type.Resource[resourceNum].Quality); // New: Quality
            buffer.WriteInt32((int)Core.Type.Resource[resourceNum].DepletionStatus); // New: Depletion Status
            return buffer.ToArray();
        }

        public static void SendMapResourceTo(int index, long resourceNum)
        {
            int mapNum = GetPlayerMap(index);
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SMapResource);
            buffer.WriteInt32(Core.Type.MapResource[mapNum].ResourceCount);

            if (Core.Type.MapResource[mapNum].ResourceCount > 0)
            {
                for (int i = 0; i < Core.Type.MapResource[mapNum].ResourceCount; i++)
                {
                    buffer.WriteByte((byte)Core.Type.MapResource[mapNum].ResourceData[i].State);
                    buffer.WriteInt32(Core.Type.MapResource[mapNum].ResourceData[i].X);
                    buffer.WriteInt32(Core.Type.MapResource[mapNum].ResourceData[i].Y);
                    buffer.WriteByte((byte)Core.Type.MapResource[mapNum].ResourceData[i].Quality); // New: Quality
                }
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendResourceRespawn(int mapNum, int resourceIndex)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SResourceRespawn);
            buffer.WriteInt32(resourceIndex);
            NetworkConfig.SendDataToMap(mapNum, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

        #region Gather Skills

        public static void CheckResourceLevelUp(int index, int skillSlot)
        {
            int expRollover;
            int levelCount = 0;

            if (GetPlayerGatherSkillLvl(index, skillSlot) >= Core.Constant.MAX_LEVEL) return;

            while (GetPlayerGatherSkillExp(index, skillSlot) >= GetPlayerGatherSkillMaxExp(index, skillSlot))
            {
                expRollover = GetPlayerGatherSkillExp(index, skillSlot) - GetPlayerGatherSkillMaxExp(index, skillSlot);
                SetPlayerGatherSkillLvl(index, skillSlot, GetPlayerGatherSkillLvl(index, skillSlot) + 1);
                SetPlayerGatherSkillExp(index, skillSlot, expRollover);
                SetPlayerGatherSkillMaxExp(index, skillSlot, GetSkillNextLevel(index, skillSlot));
                levelCount++;
            }

            if (levelCount > 0)
            {
                string message = levelCount == 1
                    ? $"Your {GetResourceSkillName((ResourceType)skillSlot)} has gone up a level!"
                    : $"Your {GetResourceSkillName((ResourceType)skillSlot)} has gone up by {levelCount} levels!";
                NetworkSend.PlayerMsg(index, message, (int)ColorType.BrightGreen);
                NetworkSend.SendPlayerData(index);
            }
        }

        #endregion

        #region Resource Interaction

        public static void CheckResource(int index, int x, int y)
        {
            int mapNum = GetPlayerMap(index);
            if (x < 0 || y < 0 || x >= Core.Type.MyMap.MaxX || y >= Core.Type.MyMap.MaxY) return;

            if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.Resource ||
                Core.Type.Map[mapNum].Tile[x, y].Type2 == TileType.Resource)
            {
                int resourceIndex = Core.Type.Map[mapNum].Tile[x, y].Data1;
                byte resourceType = (byte)Core.Type.Resource[resourceIndex].ResourceType;
                int resourceNum = -1;

                for (int i = 0; i < Core.Type.MapResource[mapNum].ResourceCount; i++)
                {
                    if (Core.Type.MapResource[mapNum].ResourceData[i].X == x &&
                        Core.Type.MapResource[mapNum].ResourceData[i].Y == y)
                    {
                        resourceNum = i;
                        break;
                    }
                }

                if (resourceNum < 0) return;

                var node = Core.Type.MapResource[mapNum].ResourceData[resourceNum];
                if (node.State != ResourceState.Available)
                {
                    NetworkSend.PlayerMsg(index, "This resource is not available right now.", (int)ColorType.Yellow);
                    return;
                }

                int equippedTool = GetPlayerEquipment(index, EquipmentType.Weapon);
                if (equippedTool >= 0 || Core.Type.Resource[resourceIndex].ToolRequired == 0)
                {
                    if (equippedTool < 0 || Core.Type.Item[equippedTool].Data3 == Core.Type.Resource[resourceIndex].ToolRequired)
                    {
                        if (Core.Type.Resource[resourceIndex].ItemReward > 0 &&
                            Player.FindOpenInvSlot(index, Core.Type.Resource[resourceIndex].ItemReward) == 0)
                        {
                            NetworkSend.PlayerMsg(index, "You have no inventory space.", (int)ColorType.Yellow);
                            return;
                        }

                        if (Core.Type.Resource[resourceIndex].LvlRequired > GetPlayerGatherSkillLvl(index, resourceType))
                        {
                            NetworkSend.PlayerMsg(index, "Your level is too low!", (int)ColorType.Yellow);
                            return;
                        }

                        int damage = Core.Type.Resource[resourceIndex].ToolRequired == 0
                            ? 1 * GetPlayerGatherSkillLvl(index, resourceType)
                            : Core.Type.Item[equippedTool].Data2;

                        if (damage > 0)
                        {
                            int rX = node.X;
                            int rY = node.Y;

                            if (node.Health <= damage)
                            {
                                node.State = ResourceState.Depleted;
                                node.RespawnTimer = DateTime.Now.AddSeconds(Core.Type.Resource[resourceIndex].RespawnTime);
                                SendMapResourceToMap(mapNum, resourceNum);
                                NetworkSend.SendActionMsg(mapNum, Core.Type.Resource[resourceIndex].SuccessMessage, (int)ColorType.BrightGreen, 1, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                Player.GiveInv(index, Core.Type.Resource[resourceIndex].ItemReward, 1);
                                Animation.SendAnimation(mapNum, Core.Type.Resource[resourceIndex].Animation, rX, rY);
                                int expGain = Core.Type.Resource[resourceIndex].ExpReward * (int)node.Quality + 1;
                                SetPlayerGatherSkillExp(index, resourceType, GetPlayerGatherSkillExp(index, resourceType) + expGain);
                                NetworkSend.PlayerMsg(index, $"Your {GetResourceSkillName((ResourceType)resourceType)} has earned {expGain} experience. ({GetPlayerGatherSkillExp(index, resourceType)}/{GetPlayerGatherSkillMaxExp(index, resourceType)})", (int)ColorType.BrightGreen);
                                NetworkSend.SendPlayerData(index);
                                CheckResourceLevelUp(index, resourceType);
                            }
                            else
                            {
                                node.Health -= (byte)damage;
                                NetworkSend.SendActionMsg(mapNum, $"-{damage}", (int)ColorType.BrightRed, 1, rX * 32, rY * 32);
                                Animation.SendAnimation(mapNum, Core.Type.Resource[resourceIndex].Animation, rX, rY);
                            }
                        }
                        else
                        {
                            NetworkSend.SendActionMsg(mapNum, "Miss!", (int)ColorType.BrightRed, 1, node.X * 32, node.Y * 32);
                        }
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "You have the wrong type of tool equipped.", (int)ColorType.Yellow);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "You need a tool to gather this resource.", (int)ColorType.Yellow);
                }
            }
        }

        #endregion

        #region Respawn Logic

        public static void CheckResourceRespawn(int mapNum, int resourceNum)
        {
            var node = Core.Type.MapResource[mapNum].ResourceData[resourceNum];
            if (node.State == ResourceState.Depleted && DateTime.Now >= node.RespawnTimer)
            {
                node.State = ResourceState.Available;
                node.Health = (byte)Core.Type.Resource[Core.Type.Map[mapNum].Tile[node.X, node.Y].Data1].Health;
                SendResourceRespawn(mapNum, resourceNum);
            }
        }

        #endregion

        // Placeholder methods for future expansion
        public static void CraftItem(int index, int resourceId, int quantity) { /* TODO */ }
        public static void TradeResource(int sellerIndex, int buyerIndex, int resourceId, int quantity) { /* TODO */ }
    }
}
