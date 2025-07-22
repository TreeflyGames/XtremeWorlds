using System;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Core.Global.Command;
using static Core.Packets;

namespace Server
{

    public class Resource
    {

        #region Database

        public static void SaveResource(int resourceNum)
        {
            string json = JsonConvert.SerializeObject(Data.Resource[resourceNum]).ToString();

            if (Database.RowExists(resourceNum, "resource"))
            {
                Database.UpdateRow(resourceNum, json, "resource", "data");
            }
            else
            {
                Database.InsertRow(resourceNum, json, "resource");
            }
        }

        public static async System.Threading.Tasks.Task LoadResourcesAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_RESOURCES).Select(i => Task.Run(() => LoadResourceAsync(i)));
            await System.Threading.Tasks.Task.WhenAll(tasks);
        }

        public static async System.Threading.Tasks.Task LoadResourceAsync(int resourceNum)
        {
            JObject data;

            data = await Database.SelectRowAsync(resourceNum, "resource", "data");

            if (data is null)
            {
                ClearResource(resourceNum);
                return;
            }

            var resourceData = JObject.FromObject(data).ToObject<Core.Type.Resource>();
            Data.Resource[resourceNum] = resourceData;
        }

        public static void ClearResource(int index)
        {
            Data.Resource[index].Name = "";
            Data.Resource[index].EmptyMessage = "";
            Data.Resource[index].SuccessMessage = "";
        }

        public static void CacheResources(int mapNum)
        {
            int x;
            int y;
            var Resource_Count = default(int);

            var loopTo = (int)Data.Map[mapNum].MaxX;
            for (x = 0; x < (int)loopTo; x++)
            {
                var loopTo1 = (int)Data.Map[mapNum].MaxY;
                for (y = 0; y < (int)loopTo1; y++)
                {
                    if (Core.Data.Map[mapNum].Tile[x, y].Type == Core.TileType.Resource || Data.Map[mapNum].Tile[x, y].Type2 == Core.TileType.Resource)
                    {
                        Resource_Count += 1;
                        Array.Resize(ref Data.MapResource[mapNum].ResourceData, Resource_Count);
                        Data.MapResource[mapNum].ResourceData[Resource_Count - 1].X = x;
                        Data.MapResource[mapNum].ResourceData[Resource_Count - 1].Y = y;
                        Data.MapResource[mapNum].ResourceData[Resource_Count - 1].Health = (byte)Data.Resource[Data.Map[mapNum].Tile[x, y].Data1].Health;
                    }

                }
            }

            Data.MapResource[mapNum].ResourceCount = Resource_Count;
        }

        public static byte[] ResourcesData()
        {
            var buffer = new ByteStream(4);
            for (int i = 0, loopTo = Core.Constant.MAX_RESOURCES; i < loopTo; i++)
            {
                if (!(Strings.Len(Data.Resource[i].Name) > 0))
                    continue;
                buffer.WriteBlock(ResourceData(i));
            }
            return buffer.ToArray();
        }

        public static byte[] ResourceData(int ResourceNum)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32(ResourceNum);
            buffer.WriteInt32(Data.Resource[ResourceNum].Animation);
            buffer.WriteString(Data.Resource[ResourceNum].EmptyMessage);
            buffer.WriteInt32(Data.Resource[ResourceNum].ExhaustedImage);
            buffer.WriteInt32(Data.Resource[ResourceNum].Health);
            buffer.WriteInt32(Data.Resource[ResourceNum].ExpReward);
            buffer.WriteInt32(Data.Resource[ResourceNum].ItemReward);
            buffer.WriteString(Data.Resource[ResourceNum].Name);
            buffer.WriteInt32(Data.Resource[ResourceNum].ResourceImage);
            buffer.WriteInt32(Data.Resource[ResourceNum].ResourceType);
            buffer.WriteInt32(Data.Resource[ResourceNum].RespawnTime);
            buffer.WriteString(Data.Resource[ResourceNum].SuccessMessage);
            buffer.WriteInt32(Data.Resource[ResourceNum].LvlRequired);
            buffer.WriteInt32(Data.Resource[ResourceNum].ToolRequired);
            buffer.WriteInt32(Conversions.ToInteger(Data.Resource[ResourceNum].Walkthrough));
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
                    NetworkSend.PlayerMsg(index, string.Format("Your {0} has gone up a level!", GetResourceSkillName((Core.ResourceSkill)SkillSlot)), (int) Core.Color.BrightGreen);
                }
                else
                {
                    // plural
                    NetworkSend.PlayerMsg(index, string.Format("Your {0} has gone up by {1} levels!", GetResourceSkillName((Core.ResourceSkill)SkillSlot), level_count), (int) Core.Color.BrightGreen);
                }

                NetworkSend.SendPlayerData(index);
            }
        }

        #endregion

        #region Incoming Packets

        public static void Packet_RequestEditResource(int index, ref byte[] data)
        {
            var buffer = new ByteStream(4);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Resource);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) Color.BrightRed);
                return;
            }

            Core.Data.TempPlayer[index].Editor = (byte) EditorType.Resource;

            Item.SendItems(index);
            Animation.SendAnimations(index);
            SendResources(index);

            buffer.WriteInt32((int) ServerPackets.SResourceEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void Packet_SaveResource(int index, ref byte[] data)
        {
            int resourcenum;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            resourcenum = buffer.ReadInt32();

            // Prevent hacking
            if (resourcenum < 0 | resourcenum > Core.Constant.MAX_RESOURCES)
                return;

            Data.Resource[resourcenum].Animation = buffer.ReadInt32();
            Data.Resource[resourcenum].EmptyMessage = buffer.ReadString();
            Data.Resource[resourcenum].ExhaustedImage = buffer.ReadInt32();
            Data.Resource[resourcenum].Health = buffer.ReadInt32();
            Data.Resource[resourcenum].ExpReward = buffer.ReadInt32();
            Data.Resource[resourcenum].ItemReward = buffer.ReadInt32();
            Data.Resource[resourcenum].Name = buffer.ReadString();
            Data.Resource[resourcenum].ResourceImage = buffer.ReadInt32();
            Data.Resource[resourcenum].ResourceType = buffer.ReadInt32();
            Data.Resource[resourcenum].RespawnTime = buffer.ReadInt32();
            Data.Resource[resourcenum].SuccessMessage = buffer.ReadString();
            Data.Resource[resourcenum].LvlRequired = buffer.ReadInt32();
            Data.Resource[resourcenum].ToolRequired = buffer.ReadInt32();
            Data.Resource[resourcenum].Walkthrough = Conversions.ToBoolean(buffer.ReadInt32());

            // Save it
            SendUpdateResourceToAll(resourcenum);
            SaveResource(resourcenum);

            Core.Log.Add(GetAccountLogin(index) + " saved Resource #" + resourcenum + ".", Constant.ADMIN_LOG);

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
            buffer.WriteInt32(Data.MapResource[mapnum].ResourceCount);

            if (Data.MapResource[mapnum].ResourceCount > 0)
            {           
                var loopTo = Data.MapResource[mapnum].ResourceCount;
                for (i = 0; i < loopTo; i++)
                {
                    buffer.WriteByte(Data.MapResource[mapnum].ResourceData[i].State);
                    buffer.WriteInt32(Data.MapResource[mapnum].ResourceData[i].X);
                    buffer.WriteInt32(Data.MapResource[mapnum].ResourceData[i].Y);
                }

            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendMapResourceToMap(int mapNum, int resourceNum)
        {
            int i;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapResource);
            buffer.WriteInt32(Data.MapResource[mapNum].ResourceCount);

            if (Data.MapResource[mapNum].ResourceCount > 0)
            {

                var loopTo = Data.MapResource[mapNum].ResourceCount;
                for (i = 0; i < loopTo; i++)
                {
                    buffer.WriteByte(Data.MapResource[mapNum].ResourceData[i].State);
                    buffer.WriteInt32(Data.MapResource[mapNum].ResourceData[i].X);
                    buffer.WriteInt32(Data.MapResource[mapNum].ResourceData[i].Y);
                }

            }

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendResources(int index)
        {
            var loopTo = Core.Constant.MAX_RESOURCES;
            for (int i = 0; i < loopTo; i++)
            {
                if (Strings.Len(Data.Resource[i].Name) > 0)
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

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateResourceToAll(int ResourceNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateResource);

            buffer.WriteBlock(ResourceData(ResourceNum));

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
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

            if (x < 0 || y < 0 || x >= Data.MyMap.MaxX || y >= Data.MyMap.MaxY)
                return;

            if (Data.Map[mapNum].Tile[x, y].Type == Core.TileType.Resource | Data.Map[mapNum].Tile[x, y].Type2 == Core.TileType.Resource)
            {
                resourceNum = 0;
                Resource_index = Data.Map[mapNum].Tile[x, y].Data1;
                ResourceType = (byte)Data.Resource[Resource_index].ResourceType;

                // Get the cache number
                for (int i = 0, loopTo = Data.MapResource[mapNum].ResourceCount; i < loopTo; i++)
                {
                    if (Data.MapResource[mapNum].ResourceData[i].X == x)
                    {
                        if (Data.MapResource[mapNum].ResourceData[i].Y == y)
                        {
                            resourceNum = i;
                        }
                    }
                }

                if (resourceNum >= 0)
                {
                    if (GetPlayerEquipment(index, Core.Equipment.Weapon) >= 0 | Core.Data.Resource[Resource_index].ToolRequired == 0)
                    {
                        if (Core.Data.Item[GetPlayerEquipment(index, Core.Equipment.Weapon)].Data3 == Core.Data.Resource[Resource_index].ToolRequired)
                        {

                            // inv space?
                            if (Core.Data.Resource[Resource_index].ItemReward > 0)
                            {
                                if (Player.FindOpenInvSlot(index, Core.Data.Resource[Resource_index].ItemReward) == 0)
                                {
                                    NetworkSend.PlayerMsg(index, "You have no inventory space.", (int) Color.Yellow);
                                    return;
                                }
                            }

                            // required lvl?
                            if (Data.Resource[Resource_index].LvlRequired > GetPlayerGatherSkillLvl(index, ResourceType))
                            {
                                NetworkSend.PlayerMsg(index, "Your level is too low!", (int) Color.Yellow);
                                return;
                            }

                            // check if already cut down
                            if (Data.MapResource[mapNum].ResourceData[resourceNum].State == 0)
                            {

                                rX = Data.MapResource[mapNum].ResourceData[resourceNum].X;
                                rY = Data.MapResource[mapNum].ResourceData[resourceNum].Y;

                                if (Data.Resource[Resource_index].ToolRequired == 0)
                                {
                                    Damage = 1 * GetPlayerGatherSkillLvl(index, ResourceType);
                                }
                                else
                                {
                                    Damage = Core.Data.Item[GetPlayerEquipment(index, Equipment.Weapon)].Data2;
                                }

                                // check if damage is more than health
                                if (Damage > 0)
                                {
                                    // cut it down!
                                    if (Data.MapResource[mapNum].ResourceData[resourceNum].Health - Damage < 0)
                                    {
                                        Data.MapResource[mapNum].ResourceData[resourceNum].State = 0; // Cut
                                        Data.MapResource[mapNum].ResourceData[resourceNum].Timer = General.GetTimeMs();
                                        SendMapResourceToMap(mapNum, resourceNum);
                                        NetworkSend.SendActionMsg(mapNum, Data.Resource[Resource_index].SuccessMessage, (int) Color.BrightGreen, 1, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                                        Player.GiveInv(index, Data.Resource[Resource_index].ItemReward, 1);
                                        Animation.SendAnimation(mapNum, Data.Resource[Resource_index].Animation, rX, rY);
                                        SetPlayerGatherSkillExp(index, ResourceType, GetPlayerGatherSkillExp(index, ResourceType) + Data.Resource[Resource_index].ExpReward);
                                        // send msg
                                        NetworkSend.PlayerMsg(index, string.Format("Your {0} has earned {1} experience. ({2}/{3})", GetResourceSkillName((ResourceSkill)ResourceType), Core.Data.Resource[Resource_index].ExpReward, GetPlayerGatherSkillExp(index, ResourceType), GetPlayerGatherSkillMaxExp(index, ResourceType)), (int) Core.Color.BrightGreen);
                                        NetworkSend.SendPlayerData(index);

                                        CheckResourceLevelUp(index, ResourceType);
                                    }
                                    else
                                    {
                                        // just do the damage
                                        Data.MapResource[mapNum].ResourceData[resourceNum].Health = (byte)(Data.MapResource[mapNum].ResourceData[resourceNum].Health - Damage);
                                        NetworkSend.SendActionMsg(mapNum, "-" + Damage, (int) Color.BrightRed, 1, rX * 32, rY * 32);
                                        Animation.SendAnimation(mapNum, Data.Resource[Resource_index].Animation, rX, rY);
                                    }
                                }
                                else
                                {
                                    // too weak
                                    NetworkSend.SendActionMsg(mapNum, "Miss!", (int) Color.BrightRed, 1, rX * 32, rY * 32);
                                }
                            }
                            else
                            {
                                NetworkSend.SendActionMsg(mapNum, Data.Resource[Resource_index].EmptyMessage, (int) Color.BrightRed, 1, GetPlayerX(index) * 32, GetPlayerY(index) * 32);
                            }
                        }
                        else
                        {
                            NetworkSend.PlayerMsg(index, "You have the wrong type of tool equiped.", (int) Color.Yellow);
                        }
                    }
                    else
                    {
                        NetworkSend.PlayerMsg(index, "You need a tool to gather this resource.", (int) Color.Yellow);
                    }
                }
            }
        }

        #endregion

    }
}