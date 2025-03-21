using System;
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

    internal static class Resource
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

        public static async Task LoadResourcesAsync()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < Core.Constant.MAX_RESOURCES; i++)
            {
                tasks.Add(Task.Run(() => LoadResourceAsync(i)));
            }

            await Task.WhenAll(tasks);
        }

        public static async Task LoadResourceAsync(int resourceNum)
        {
            JObject data;

            data = await Database.SelectRowAsync(resourceNum, "resource", "data");

            if (data is null)
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
        }

        public static void CacheResources(int mapNum)
        {
            int x;
            int y;
            var Resource_Count = default(int);

            var loopTo = (int)Core.Type.Map[mapNum].MaxX;
            for (x = 0; x < (int)loopTo; x++)
            {
                var loopTo1 = (int)Core.Type.Map[mapNum].MaxY;
                for (y = 0; y < (int)loopTo1; y++)
                {
                    if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.Resource || Core.Type.Map[mapNum].Tile[x, y].Type2 == TileType.Resource)
                    {
                        Resource_Count += 1;
                        Array.Resize(ref Core.Type.MapResource[mapNum].ResourceData, Resource_Count);
                        Core.Type.MapResource[mapNum].ResourceData[Resource_Count - 1].X = x;
                        Core.Type.MapResource[mapNum].ResourceData[Resource_Count - 1].Y = y;
                        Core.Type.MapResource[mapNum].ResourceData[Resource_Count - 1].Health = (byte)Core.Type.Resource[Core.Type.Map[mapNum].Tile[x, y].Data1].Health;
                    }

                }
            }

            Core.Type.MapResource[mapNum].ResourceCount = Resource_Count;
        }

        public static byte[] ResourcesData()
        {
            var buffer = new ByteStream(4);
            for (int i = 0, loopTo = Core.Constant.MAX_RESOURCES; i < loopTo; i++)
            {
                if (!(Strings.Len(Core.Type.Resource[i].Name) > 0))
                    continue;
                buffer.WriteBlock(ResourceData(i));
            }
            return buffer.ToArray();
        }

        public static byte[] ResourceData(int ResourceNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32(ResourceNum);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].Animation);
            buffer.WriteString(Core.Type.Resource[ResourceNum].EmptyMessage);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ExhaustedImage);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].Health);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ExpReward);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ItemReward);
            buffer.WriteString(Core.Type.Resource[ResourceNum].Name);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ResourceImage);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ResourceType);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].RespawnTime);
            buffer.WriteString(Core.Type.Resource[ResourceNum].SuccessMessage);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].LvlRequired);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ToolRequired);
            buffer.WriteInt32(Conversions.ToInteger(Core.Type.Resource[ResourceNum].Walkthrough));
            return buffer.ToArray();
        }

        #endregion

        #region Gather Skills
        public static void CheckResourceLevelUp(int index, int SkillSlot)
        {
            int expRollover;
            int level_count;

            level_count = 0;

            if (GetPlayerGatherSkillLvl(index, SkillSlot) == Core.Constant.MAX_LEVEL)
                return;

            while (GetPlayerGatherSkillExp(index, SkillSlot) >= GetPlayerGatherSkillMaxExp(index, SkillSlot))
            {
                expRollover = GetPlayerGatherSkillExp(index, SkillSlot) - GetPlayerGatherSkillMaxExp(index, SkillSlot);
                SetPlayerGatherSkillLvl(index, SkillSlot, GetPlayerGatherSkillLvl(index, SkillSlot) + 1);
                SetPlayerGatherSkillExp(index, SkillSlot, expRollover);
                SetPlayerGatherSkillMaxExp(index, SkillSlot, GetSkillNextLevel(index, SkillSlot));
                level_count =+ 1;
            }

            if (level_count > 0)
            {
                if (level_count == 1)
                {
                    // singular
                    NetworkSend.PlayerMsg(index, string.Format("Your {0} has gone up a level!", GetResourceSkillName((ResourceType)SkillSlot)), (int) ColorType.BrightGreen);
                }
                else
                {
                    // plural
                    NetworkSend.PlayerMsg(index, string.Format("Your {0} has gone up by {1} levels!", GetResourceSkillName((ResourceType)SkillSlot), level_count), (int) ColorType.BrightGreen);
                }

                NetworkSend.SendPlayerData(index);
            }
        }

        #endregion

        #region Incoming Packets

        public static void Packet_EditResource(int index, ref byte[] data)
        {
            var buffer = new ByteStream(4);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Resource);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            Core.Type.TempPlayer[index].Editor = (byte) EditorType.Resource;

            Item.SendItems(index);
            Animation.SendAnimations(index);
            SendResources(index);

            buffer.WriteInt32((int) ServerPackets.SResourceEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void Packet_SaveResource(int index, ref byte[] data)
        {
            int resourcenum;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            resourcenum = buffer.ReadInt32();

            // Prevent hacking
            if (resourcenum < 0 | resourcenum > Core.Constant.MAX_RESOURCES)
                return;

            Core.Type.Resource[resourcenum].Animation = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].EmptyMessage = buffer.ReadString();
            Core.Type.Resource[resourcenum].ExhaustedImage = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].Health = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].ExpReward = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].ItemReward = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].Name = buffer.ReadString();
            Core.Type.Resource[resourcenum].ResourceImage = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].ResourceType = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].RespawnTime = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].SuccessMessage = buffer.ReadString();
            Core.Type.Resource[resourcenum].LvlRequired = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].ToolRequired = buffer.ReadInt32();
            Core.Type.Resource[resourcenum].Walkthrough = Conversions.ToBoolean(buffer.ReadInt32());

            // Save it
            SendUpdateResourceToAll(resourcenum);
            SaveResource(resourcenum);

            Core.Log.Add(GetPlayerLogin(index) + " saved Resource #" + resourcenum + ".", Constant.ADMIN_LOG);

            buffer.Dispose();
        }

        public static void Packet_RequestResource(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;

            n = buffer.ReadInt32();

            if (n < 0 | n > Core.Constant.MAX_RESOURCES)
                return;

            SendUpdateResourceTo(index, n);
        }

        #endregion

        #region Outgoing Packets

        public static void SendMapResourceTo(int index, long resourceNum)
        {
            int i;
            int mapnum;
            var buffer = new ByteStream(4);

            mapnum = GetPlayerMap(index);

            buffer.WriteInt32((int) ServerPackets.SMapResource);
            buffer.WriteInt32(Core.Type.MapResource[mapnum].ResourceCount);

            if (Core.Type.MapResource[mapnum].ResourceCount > 0)
            {           
                var loopTo = Core.Type.MapResource[mapnum].ResourceCount;
                for (i = 0; i < loopTo; i++)
                {
                    buffer.WriteByte(Core.Type.MapResource[mapnum].ResourceData[i].State);
                    buffer.WriteInt32(Core.Type.MapResource[mapnum].ResourceData[i].X);
                    buffer.WriteInt32(Core.Type.MapResource[mapnum].ResourceData[i].Y);
                }

            }

            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendMapResourceToMap(int mapNum, int resourceNum)
        {
            int i;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapResource);
            buffer.WriteInt32(Core.Type.MapResource[mapNum].ResourceCount);

            if (Core.Type.MapResource[mapNum].ResourceCount > 0)
            {

                var loopTo = Core.Type.MapResource[mapNum].ResourceCount;
                for (i = 0; i < loopTo; i++)
                {
                    buffer.WriteByte(Core.Type.MapResource[mapNum].ResourceData[i].State);
                    buffer.WriteInt32(Core.Type.MapResource[mapNum].ResourceData[i].X);
                    buffer.WriteInt32(Core.Type.MapResource[mapNum].ResourceData[i].Y);
                }

            }

            NetworkConfig.SendDataToMap(mapNum, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendResources(int index)
        {
            var loopTo = Core.Constant.MAX_RESOURCES;
            for (int i = 0; i < loopTo; i++)
            {
                if (Strings.Len(Core.Type.Resource[i].Name) > 0)
                {
                    SendUpdateResourceTo(index, i);
                }

            }
        }

        public static void SendUpdateResourceTo(int index, int ResourceNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateResource);

            buffer.WriteBlock(ResourceData(ResourceNum));

            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendUpdateResourceToAll(int ResourceNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateResource);

            buffer.WriteBlock(ResourceData(ResourceNum));

            NetworkConfig.SendDataToAll(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

        #region Functions

        public static void CheckResource(int index, int x, int y)
        {
            int resourceNum;
            byte ResourceType;
            int Resource_index;
            int rX;
            int rY;
            int Damage;
            int mapNum;

            mapNum = GetPlayerMap(index);

            if (x < 0 || y < 0 || x >= Core.Type.MyMap.MaxX || y >= Core.Type.MyMap.MaxY)
                return;

            if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.Resource | Core.Type.Map[mapNum].Tile[x, y].Type2 == TileType.Resource)
            {
                resourceNum = 0;
                Resource_index = Core.Type.Map[mapNum].Tile[x, y].Data1;
                ResourceType = (byte)Core.Type.Resource[Resource_index].ResourceType;

                // Get the cache number
                for (int i = 0, loopTo = Core.Type.MapResource[mapNum].ResourceCount; i < loopTo; i++)
                {
                    if (Core.Type.MapResource[mapNum].ResourceData[i].X == x)
                    {
                        if (Core.Type.MapResource[mapNum].ResourceData[i].Y == y)
                        {
                            resourceNum = i;
                        }
                    }
                }

                if (resourceNum >= 0)
                {
                    if (GetPlayerEquipment(index, EquipmentType.Weapon) >= 0 | Core.Type.Resource[Resource_index].ToolRequired == 0)
                    {
                        if (Core.Type.Item[GetPlayerEquipment(index, EquipmentType.Weapon)].Data3 == Core.Type.Resource[Resource_index].ToolRequired)
                        {

                            // inv space?
                            if (Core.Type.Resource[Resource_index].ItemReward > 0)
                            {
                                if (Player.FindOpenInvSlot(index, Core.Type.Resource[Resource_index].ItemReward) == 0)
                                {
                                    NetworkSend.PlayerMsg(index, "You have no inventory space.", (int) ColorType.Yellow);
                                    return;
                                }
                            }

                            // required lvl?
                            if (Core.Type.Resource[Resource_index].LvlRequired > GetPlayerGatherSkillLvl(index, ResourceType))
                            {
                                NetworkSend.PlayerMsg(index, "Your level is too low!", (int) ColorType.Yellow);
                                return;
                            }

                            // check if already cut down
                            if (Core.Type.MapResource[mapNum].ResourceData[resourceNum].State == 0)
                            {

                                rX = Core.Type.MapResource[mapNum].ResourceData[resourceNum].X;
                                rY = Core.Type.MapResource[mapNum].ResourceData[resourceNum].Y;

                                if (Core.Type.Resource[Resource_index].ToolRequired == 0)
                                {
                                    Damage = 1 * GetPlayerGatherSkillLvl(index, ResourceType);
                                }
                                else
                                {
                                    Damage = Core.Type.Item[GetPlayerEquipment(index, EquipmentType.Weapon)].Data2;
                                }

                                // check if damage is more than health
                                if (Damage > 0)
                                {
                                    // cut it down!
                                    if (Core.Type.MapResource[mapNum].ResourceData[resourceNum].Health - Damage < 0)
                                    {
                                        Core.Type.MapResource[mapNum].ResourceData[resourceNum].State = 0; // Cut
                                        Core.Type.MapResource[mapNum].ResourceData[resourceNum].Timer = General.GetTimeMs();
                                        SendMapResourceToMap(mapNum, resourceNum);
                                        NetworkSend.SendActionMsg(mapNum, Core.Type.Resource[Resource_index].SuccessMessage, (int) ColorType.BrightGreen, 1, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                        Player.GiveInv(index, Core.Type.Resource[Resource_index].ItemReward, 1);
                                        Animation.SendAnimation(mapNum, Core.Type.Resource[Resource_index].Animation, rX, rY);
                                        SetPlayerGatherSkillExp(index, ResourceType, GetPlayerGatherSkillExp(index, ResourceType) + Core.Type.Resource[Resource_index].ExpReward);
                                        // send msg
                                        NetworkSend.PlayerMsg(index, string.Format("Your {0} has earned {1} experience. ({2}/{3})", GetResourceSkillName((ResourceType)ResourceType), Core.Type.Resource[Resource_index].ExpReward, GetPlayerGatherSkillExp(index, ResourceType), GetPlayerGatherSkillMaxExp(index, ResourceType)), (int) ColorType.BrightGreen);
                                        NetworkSend.SendPlayerData(index);

                                        CheckResourceLevelUp(index, ResourceType);
                                    }
                                    else
                                    {
                                        // just do the damage
                                        Core.Type.MapResource[mapNum].ResourceData[resourceNum].Health = (byte)(Core.Type.MapResource[mapNum].ResourceData[resourceNum].Health - Damage);
                                        NetworkSend.SendActionMsg(mapNum, "-" + Damage, (int) ColorType.BrightRed, 1, rX * 32, rY * 32);
                                        Animation.SendAnimation(mapNum, Core.Type.Resource[Resource_index].Animation, rX, rY);
                                    }
                                }
                                else
                                {
                                    // too weak
                                    NetworkSend.SendActionMsg(mapNum, "Miss!", (int) ColorType.BrightRed, 1, rX * 32, rY * 32);
                                }
                            }
                            else
                            {
                                NetworkSend.SendActionMsg(mapNum, Core.Type.Resource[Resource_index].EmptyMessage, (int) ColorType.BrightRed, 1, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                            }
                        }
                        else
                        {
                            NetworkSend.PlayerMsg(index, "You have the wrong type of tool equiped.", (int) ColorType.Yellow);
                        }
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "You need a tool to gather this resource.", (int) ColorType.Yellow);
                    }
                }
            }
        }

        #endregion

    }
}