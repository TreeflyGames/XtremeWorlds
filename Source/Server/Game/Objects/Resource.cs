using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static Core.Enum;
using static Core.Global.Command;
using static Core.Packets;

namespace Server
{
    public class Resource
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager();

        #region Database

        public static async Task SaveResourceAsync(int resourceNum)
        {
            try
            {
                string json = JsonConvert.SerializeObject(Core.Type.Resource[resourceNum]);
                if (await Database.RowExistsAsync(resourceNum, "resource"))
                {
                    await Database.UpdateRowAsync(resourceNum, json, "resource", "data");
                }
                else
                {
                    await Database.InsertRowAsync(resourceNum, json, "resource");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving resource {resourceNum}: {ex.Message}");
            }
        }

        public static async Task SaveAllResourcesAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_RESOURCES)
                .Select(i => SaveResourceAsync(i));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadResourcesAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_RESOURCES)
                .Select(i => LoadResourceAsync(i));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadResourceAsync(int resourceNum)
        {
            try
            {
                string data = await Database.SelectRowAsync(resourceNum, "resource", "data");
                if (string.IsNullOrEmpty(data))
                {
                    ClearResource(resourceNum);
                    return;
                }
                var resourceData = JsonConvert.DeserializeObject<Core.Type.ResourceStruct>(data);
                Core.Type.Resource[resourceNum] = resourceData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading resource {resourceNum}: {ex.Message}");
                ClearResource(resourceNum);
            }
        }

        public static void ClearResource(int index)
        {
            Core.Type.Resource[index].Name = "";
            Core.Type.Resource[index].EmptyMessage = "";
            Core.Type.Resource[index].SuccessMessage = "";
            Core.Type.Resource[index].Quality = ResourceQuality.Common;
            Core.Type.Resource[index].DepletionStatus = DepletionStatus.Active;
        }

        #endregion

        #region Resource Nodes and Manager

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

        public class ResourceManager
        {
            private readonly Dictionary<int, Dictionary<(int, int), ResourceNode>> _mapResources = new();

            public void CacheResources(int mapNum)
            {
                var nodes = new Dictionary<(int, int), ResourceNode>();
                for (int x = 0; x <= Core.Type.Map[mapNum].MaxX; x++)
                {
                    for (int y = 0; y <= Core.Type.Map[mapNum].MaxY; y++)
                    {
                        if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.Resource ||
                            Core.Type.Map[mapNum].Tile[x, y].Type2 == TileType.Resource)
                        {
                            int resourceIndex = Core.Type.Map[mapNum].Tile[x, y].Data1;
                            nodes[(x, y)] = new ResourceNode
                            {
                                X = x,
                                Y = y,
                                Health = (byte)Core.Type.Resource[resourceIndex].Health,
                                Quality = (ResourceQuality)Core.Type.Resource[resourceIndex].Quality,
                                RespawnTimer = DateTime.MinValue
                            };
                        }
                    }
                }
                _mapResources[mapNum] = nodes;
                Core.Type.MapResource[mapNum].ResourceCount = nodes.Count;
                Core.Type.MapResource[mapNum].ResourceData = nodes.Values.ToArray();
            }

            public ResourceNode GetResourceNode(int mapNum, int x, int y)
            {
                return _mapResources.TryGetValue(mapNum, out var nodes) && nodes.TryGetValue((x, y), out var node) ? node : null;
            }

            public void DepleteResource(int mapNum, ResourceNode node)
            {
                node.State = ResourceState.Depleted;
                node.RespawnTimer = DateTime.Now.AddSeconds(Core.Type.Resource[Core.Type.Map[mapNum].Tile[node.X, node.Y].Data1].RespawnTime);
            }

            public void CheckRespawn(int mapNum)
            {
                if (!_mapResources.TryGetValue(mapNum, out var nodes)) return;
                int index = 0;
                foreach (var node in nodes.Values)
                {
                    if (node.State == ResourceState.Depleted && DateTime.Now >= node.RespawnTimer)
                    {
                        node.State = ResourceState.Available;
                        node.Health = (byte)Core.Type.Resource[Core.Type.Map[mapNum].Tile[node.X, node.Y].Data1].Health;
                        SendResourceRespawn(mapNum, index);
                    }
                    index++;
                }
            }
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
            buffer.WriteInt32((int)Core.Type.Resource[resourceNum].Quality);
            buffer.WriteInt32((int)Core.Type.Resource[resourceNum].DepletionStatus);
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
                    var node = Core.Type.MapResource[mapNum].ResourceData[i];
                    buffer.WriteByte((byte)node.State);
                    buffer.WriteInt32(node.X);
                    buffer.WriteInt32(node.Y);
                    buffer.WriteByte((byte)node.Quality);
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

            if (Core.Type.Map[mapNum].Tile[x, y].Type != TileType.Resource &&
                Core.Type.Map[mapNum].Tile[x, y].Type2 != TileType.Resource) return;

            int resourceIndex = Core.Type.Map[mapNum].Tile[x, y].Data1;
            byte resourceType = (byte)Core.Type.Resource[resourceIndex].ResourceType;
            var node = _resourceManager.GetResourceNode(mapNum, x, y);
            if (node == null || node.State != ResourceState.Available)
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
                        if (node.Health <= damage)
                        {
                            int baseYield = 1;
                            int qualityBonus = (int)node.Quality;
                            int skillBonus = GetPlayerGatherSkillLvl(index, resourceType) / 10; // +1 per 10 levels
                            int totalYield = baseYield + qualityBonus + skillBonus;

                            _resourceManager.DepleteResource(mapNum, node);
                            SendMapResourceToMap(mapNum, Array.IndexOf(Core.Type.MapResource[mapNum].ResourceData, node));
                            NetworkSend.SendActionMsg(mapNum, Core.Type.Resource[resourceIndex].SuccessMessage, (int)ColorType.BrightGreen, 1, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                            Player.GiveInv(index, Core.Type.Resource[resourceIndex].ItemReward, totalYield);
                            Animation.SendAnimation(mapNum, Core.Type.Resource[resourceIndex].Animation, node.X, node.Y);
                            int expGain = Core.Type.Resource[resourceIndex].ExpReward * ((int)node.Quality + 1);
                            SetPlayerGatherSkillExp(index, resourceType, GetPlayerGatherSkillExp(index, resourceType) + expGain);
                            NetworkSend.PlayerMsg(index, $"Your {GetResourceSkillName((ResourceType)resourceType)} has earned {expGain} experience. ({GetPlayerGatherSkillExp(index, resourceType)}/{GetPlayerGatherSkillMaxExp(index, resourceType)})", (int)ColorType.BrightGreen);
                            NetworkSend.SendPlayerData(index);
                            CheckResourceLevelUp(index, resourceType);
                        }
                        else
                        {
                            node.Health -= (byte)damage;
                            NetworkSend.SendActionMsg(mapNum, $"-{damage}", (int)ColorType.BrightRed, 1, node.X * 32, node.Y * 32);
                            Animation.SendAnimation(mapNum, Core.Type.Resource[resourceIndex].Animation, node.X, node.Y);
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

        #endregion

        #region Respawn Logic

        public static void CheckResourceRespawn(int mapNum, int resourceNum)
        {
            _resourceManager.CheckRespawn(mapNum);
        }

        #endregion

        #region Crafting System

        public class CraftingRecipe
        {
            public int ResultItem { get; set; }
            public Dictionary<int, int> Ingredients { get; set; } // resourceId, quantity
        }

        public static readonly List<CraftingRecipe> Recipes = new List<CraftingRecipe>
        {
            new CraftingRecipe
            {
                ResultItem = 1, // Example: Sword
                Ingredients = new Dictionary<int, int> { { 1, 5 }, { 2, 3 } } // 5 of resource 1, 3 of resource 2
            }
            // Add more recipes as needed
        };

        public static void CraftItem(int index, int recipeIndex)
        {
            if (recipeIndex < 0 || recipeIndex >= Recipes.Count) return;

            var recipe = Recipes[recipeIndex];
            bool canCraft = true;

            foreach (var ingredient in recipe.Ingredients)
            {
                int resourceId = ingredient.Key;
                int requiredQuantity = ingredient.Value;
                int playerQuantity = Player.GetInvItemQuantity(index, resourceId);
                if (playerQuantity < requiredQuantity)
                {
                    canCraft = false;
                    break;
                }
            }

            if (canCraft)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    Player.TakeInv(index, ingredient.Key, ingredient.Value);
                }
                Player.GiveInv(index, recipe.ResultItem, 1);
                NetworkSend.PlayerMsg(index, "You crafted a new item!", (int)ColorType.BrightGreen);
            }
            else
            {
                NetworkSend.PlayerMsg(index, "You don't have the required ingredients.", (int)ColorType.Yellow);
            }
        }

        #endregion

        #region Trading (Placeholder)

        public static void TradeResource(int sellerIndex, int buyerIndex, int resourceId, int quantity)
        {
            // TODO: Implement trading logic
            NetworkSend.PlayerMsg(sellerIndex, "Trading not yet implemented.", (int)ColorType.Yellow);
        }

        #endregion
    }
}
