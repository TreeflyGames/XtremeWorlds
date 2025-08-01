﻿using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using static Core.Global.Command;
using static Core.Packets;

namespace Server
{

    public class Item
    {

        #region Database

        public static void SaveItems()
        {
            int i;

            var loopTo = Core.Constant.MAX_ITEMS - 1;
            for (i = 0; i < loopTo; i++)
                SaveItem(i);

        }

        public static void SaveItem(int itemNum)
        {
            string json = JsonConvert.SerializeObject(Core.Data.Item[itemNum]).ToString();

            if (Database.RowExists(itemNum, "item"))
            {
                Database.UpdateRow(itemNum, json, "item", "data");
            }
            else
            {
                Database.InsertRow(itemNum, json, "item");
            }
        }

        public static async System.Threading.Tasks.Task LoadItemsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_ITEMS).Select(i => Task.Run(() => LoadItemAsync(i)));
            await System.Threading.Tasks.Task.WhenAll(tasks);
        }

        public static async System.Threading.Tasks.Task LoadItemAsync(int itemNum)
        {
            JObject data;

            data = await Database.SelectRowAsync(itemNum, "item", "data");

            if (data is null)
            {
                ClearItem(itemNum);
                return;
            }

            var itemData = JObject.FromObject(data).ToObject<Core.Type.Item>();
            Core.Data.Item[itemNum] = itemData;
        }

        public static void ClearItem(int index)
        {
            Core.Data.Item[index].Name = "";
            Core.Data.Item[index].Description = "";
            Core.Data.Item[index].Stackable = 1;

            var statCount = Enum.GetNames(typeof(Stat)).Length;
            for (int i = 0, loopTo = Core.Constant.MAX_ITEMS; i < loopTo; i++)
            {
                Core.Data.Item[(i)].Add_Stat = new byte[statCount];
                Core.Data.Item[i].Stat_Req = new byte[statCount];
            }
        }

        public static byte[] ItemData(int itemNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(itemNum);
            buffer.WriteInt32(Core.Data.Item[itemNum].AccessReq);

            var statCount = Enum.GetNames(typeof(Stat)).Length;
            for (int i = 0, loopTo = statCount; i < loopTo; i++)
                buffer.WriteInt32(Core.Data.Item[itemNum].Add_Stat[i]);

            buffer.WriteInt32(Core.Data.Item[itemNum].Animation);
            buffer.WriteInt32(Core.Data.Item[itemNum].BindType);
            buffer.WriteInt32(Core.Data.Item[itemNum].JobReq);
            buffer.WriteInt32(Core.Data.Item[itemNum].Data1);
            buffer.WriteInt32(Core.Data.Item[itemNum].Data2);
            buffer.WriteInt32(Core.Data.Item[itemNum].Data3);
            buffer.WriteInt32(Core.Data.Item[itemNum].LevelReq);
            buffer.WriteInt32(Core.Data.Item[itemNum].Mastery);
            buffer.WriteString(Core.Data.Item[itemNum].Name);
            buffer.WriteInt32(Core.Data.Item[itemNum].Paperdoll);
            buffer.WriteInt32(Core.Data.Item[itemNum].Icon);
            buffer.WriteInt32(Core.Data.Item[itemNum].Price);
            buffer.WriteInt32(Core.Data.Item[itemNum].Rarity);
            buffer.WriteInt32(Core.Data.Item[itemNum].Speed);

            buffer.WriteInt32(Core.Data.Item[itemNum].Stackable);
            buffer.WriteString(Core.Data.Item[itemNum].Description);

            for (int i = 0, loopTo1 = statCount; i < loopTo1; i++)
                buffer.WriteInt32(Core.Data.Item[itemNum].Stat_Req[i]);

            buffer.WriteInt32(Core.Data.Item[itemNum].Type);
            buffer.WriteInt32(Core.Data.Item[itemNum].SubType);

            buffer.WriteInt32(Core.Data.Item[itemNum].ItemLevel);

            buffer.WriteInt32(Core.Data.Item[itemNum].KnockBack);
            buffer.WriteInt32(Core.Data.Item[itemNum].KnockBackTiles);
            buffer.WriteInt32(Core.Data.Item[itemNum].Projectile);
            buffer.WriteInt32(Core.Data.Item[itemNum].Ammo);
            return buffer.ToArray();
        }

        #endregion

        #region Map Items
        public static void SendMapItemsTo(int index, int mapNum)
        {
            int i;
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapItemData);

            var loopTo = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32((int)Data.MapItem[mapNum, i].Num);
                buffer.WriteInt32(Data.MapItem[mapNum, i].Value);
                buffer.WriteInt32(Data.MapItem[mapNum, i].X);
                buffer.WriteInt32(Data.MapItem[mapNum, i].Y);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendMapItemsToAll(int mapNum)
        {
            int i;
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SMapItemData);

            var loopTo = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32((int)Data.MapItem[mapNum, i].Num);
                buffer.WriteInt32(Data.MapItem[mapNum, i].Value);
                buffer.WriteInt32(Data.MapItem[mapNum, i].X);
                buffer.WriteInt32(Data.MapItem[mapNum, i].Y);
            }

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SpawnItem(int itemNum, int ItemVal, int mapNum, int x, int y)
        {
            int i;

            // Check for subscript out of range
            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS | mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return;

            // Find open map item slot
            i = FindOpenMapItemSlot(mapNum);

            if (i == -1)
                return;

            SpawnItemSlot(i, itemNum, ItemVal, mapNum, x, y);
        }

        public static void SpawnItemSlot(int MapItemSlot, int itemNum, int ItemVal, int mapNum, int x, int y)
        {
            int i;
            var buffer = new ByteStream(4);

            // Check for subscript out of range
            if (MapItemSlot < 0 | MapItemSlot > Core.Constant.MAX_MAP_ITEMS | itemNum > Core.Constant.MAX_ITEMS | mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return;

            i = MapItemSlot;

            if (i != -1)
            {
                Data.MapItem[mapNum, i].Num = itemNum;
                Data.MapItem[mapNum, i].Value = ItemVal;
                Data.MapItem[mapNum, i].X = x * 32;
                Data.MapItem[mapNum, i].Y = y * 32;

                buffer.WriteInt32((int) ServerPackets.SSpawnItem);
                buffer.WriteInt32(i);
                buffer.WriteInt32(itemNum);
                buffer.WriteInt32(ItemVal);
                buffer.WriteInt32(x);
                buffer.WriteInt32(y);

                NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            }

            buffer.Dispose();
        }

        public static int FindOpenMapItemSlot(int mapNum)
        {
            int FindOpenMapItemSlotRet = default;
            int i;

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return FindOpenMapItemSlotRet;

            FindOpenMapItemSlotRet = -1;

            var loopTo = Core.Constant.MAX_MAP_ITEMS;
            for (i = 0; i < loopTo; i++)
            {
                if (Data.MapItem[mapNum, i].Num == -1)
                {
                    FindOpenMapItemSlotRet = i;
                    return FindOpenMapItemSlotRet;
                }
            }

            return FindOpenMapItemSlotRet;

        }

        public static async System.Threading.Tasks.Task SpawnAllMapsItemsAsync()
        {
            int i;

            var loopTo = Core.Constant.MAX_MAPS;
            for (i = 0; i < loopTo; i++)
                await SpawnMapItemsAsync(i);
        }

        public static async System.Threading.Tasks.Task SpawnMapItemsAsync(int mapNum)
        {
            int x;
            int y;

            // Check for subscript out of range  
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return;

            if (Data.Map[mapNum].NoRespawn)
                return;

            // Spawn what we have  
            var loopTo = (int)Data.Map[mapNum].MaxX;
            for (x = 0; x < (int)loopTo; x++)
            {
                var loopTo1 = (int)Data.Map[mapNum].MaxY;
                for (y = 0; y < (int)loopTo1; y++)
                {
                    // Check if the tile type is an item or a saved tile incase someone drops something  
                    if (Data.Map[mapNum].Tile[x, y].Type == Core.TileType.Item)
                    {
                        // Check to see if its a currency and if they set the value to 0 set it to 1 automatically  
                        if (Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1].Type == (byte)ItemCategory.Currency | Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1].Stackable == 1)
                        {
                            if (Data.Map[mapNum].Tile[x, y].Data2 < 1)
                            {
                                await SpawnItemAsync(Data.Map[mapNum].Tile[x, y].Data1, 1, mapNum, x, y);
                            }
                            else
                            {
                                await SpawnItemAsync(Data.Map[mapNum].Tile[x, y].Data1, Data.Map[mapNum].Tile[x, y].Data2, mapNum, x, y);
                            }
                        }
                        else
                        {
                            await SpawnItemAsync(Data.Map[mapNum].Tile[x, y].Data1, Data.Map[mapNum].Tile[x, y].Data2, mapNum, x, y);
                        }
                    }

                    // Check if the tile type is an item or a saved tile incase someone drops something  
                    if (Data.Map[mapNum].Tile[x, y].Type2 == Core.TileType.Item)
                    {
                        // Check to see if its a currency and if they set the value to 0 set it to 1 automatically  
                        if (Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1_2].Type == (byte)Core.ItemCategory.Currency | Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1_2].Stackable == 1 | Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1_2].Type == (byte)ItemCategory.Currency | Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1_2].Stackable == 1)
                        {
                            if (Data.Map[mapNum].Tile[x, y].Data2_2 < 1)
                            {
                                await SpawnItemAsync(Data.Map[mapNum].Tile[x, y].Data1_2, 1, mapNum, x, y);
                            }
                            else
                            {
                                await SpawnItemAsync(Data.Map[mapNum].Tile[x, y].Data1_2, Data.Map[mapNum].Tile[x, y].Data2_2, mapNum, x, y);
                            }
                        }
                        else
                        {
                            await SpawnItemAsync(Data.Map[mapNum].Tile[x, y].Data1_2, Data.Map[mapNum].Tile[x, y].Data2_2, mapNum, x, y);
                        }
                    }
                }
            }
        }

        public static async System.Threading.Tasks.Task SpawnItemAsync(int itemNum, int ItemVal, int mapNum, int x, int y)
        {
            int i;

            // Check for subscript out of range  
            if (itemNum < 0 | itemNum > Core.Constant.MAX_ITEMS | mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return;

            // Find open map item slot  
            i = FindOpenMapItemSlot(mapNum);

            if (i == -1)
                return;

            await SpawnItemSlotAsync(i, itemNum, ItemVal, mapNum, x, y);
        }

        public static async System.Threading.Tasks.Task SpawnItemSlotAsync(int MapItemSlot, int itemNum, int ItemVal, int mapNum, int x, int y)
        {
            var buffer = new ByteStream(4);

            // Check for subscript out of range  
            if (MapItemSlot < 0 || MapItemSlot > Core.Constant.MAX_MAP_ITEMS || itemNum > Core.Constant.MAX_ITEMS || mapNum < 0 || mapNum > Core.Constant.MAX_MAPS)
                return;

            if (MapItemSlot != -1)
            {
                Data.MapItem[mapNum, MapItemSlot].Num = itemNum;
                Data.MapItem[mapNum, MapItemSlot].Value = ItemVal;
                Data.MapItem[mapNum, MapItemSlot].X = x * 32;
                Data.MapItem[mapNum, MapItemSlot].Y = y * 32;

                buffer.WriteInt32((int)ServerPackets.SSpawnItem);
                buffer.WriteInt32(MapItemSlot);
                buffer.WriteInt32(itemNum);
                buffer.WriteInt32(ItemVal);
                buffer.WriteInt32(x * 32);
                buffer.WriteInt32(y * 32);

                NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            }

            buffer.Dispose();
        }

        public static void SpawnMapItems(int mapNum)
        {
            int x;
            int y;

            // Check for subscript out of range
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return;

            if (Data.Map[mapNum].NoRespawn)
                return;

            // Spawn what we have
            var loopTo = (int)Data.Map[mapNum].MaxX;
            for (x = 0; x < (int)loopTo; x++)
            {
                var loopTo1 = (int)Data.Map[mapNum].MaxY;
                for (y = 0; y < (int)loopTo1; y++)
                {
                    // Check if the tile type is an item or a saved tile incase someone drops something
                    if (Data.Map[mapNum].Tile[x, y].Type == Core.TileType.Item)
                    {
                        // Check to see if its a currency and if they set the value to 0 set it to 1 automatically
                        if (Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1].Type == (byte)ItemCategory.Currency | Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1].Stackable == 1)
                        {
                            if (Data.Map[mapNum].Tile[x, y].Data2 < 1)
                            {
                                SpawnItem(Data.Map[mapNum].Tile[x, y].Data1, 1, mapNum, x, y);
                            }
                            else
                            {
                                SpawnItem(Data.Map[mapNum].Tile[x, y].Data1, Data.Map[mapNum].Tile[x, y].Data2, mapNum, x, y);
                            }
                        }
                        else
                        {
                            SpawnItem(Data.Map[mapNum].Tile[x, y].Data1, Data.Map[mapNum].Tile[x, y].Data2, mapNum, x, y);
                        }
                    }

                    // Check if the tile type is an item or a saved tile incase someone drops something
                    if (Data.Map[mapNum].Tile[x, y].Type2 == Core.TileType.Item)
                    {
                        // Check to see if its a currency and if they set the value to 0 set it to 1 automatically
                        if (Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1_2].Type == (byte)ItemCategory.Currency | Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1_2].Stackable == 1 | Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1_2].Type == (byte)ItemCategory.Currency | Core.Data.Item[Data.Map[mapNum].Tile[x, y].Data1_2].Stackable == 1)
                        {
                            if (Data.Map[mapNum].Tile[x, y].Data2_2 < 1)
                            {
                                SpawnItem(Data.Map[mapNum].Tile[x, y].Data1_2, 1, mapNum, x, y);
                            }
                            else
                            {
                                SpawnItem(Data.Map[mapNum].Tile[x, y].Data1_2, Data.Map[mapNum].Tile[x, y].Data2_2, mapNum, x, y);
                            }
                        }
                        else
                        {
                            SpawnItem(Data.Map[mapNum].Tile[x, y].Data1_2, Data.Map[mapNum].Tile[x, y].Data2_2, mapNum, x, y);
                        }
                    }
                }
            }

        }

        #endregion

        #region Incoming Packets

        public static void Packet_RequestItem(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;

            n = buffer.ReadInt32();

            if (n < 0 | n > Core.Constant.MAX_ITEMS)
                return;

            SendUpdateItemTo(index, n);
        }

        public static void Packet_RequestEditItem(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Mapper)
                return;

            string user;

            user = IsEditorLocked(index, (byte) Core.EditorType.Item);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) Color.BrightRed);
                return;
            }

            Core.Data.TempPlayer[index].Editor = (byte) Core.EditorType.Item;

            Animation.SendAnimations(index);
            Projectile.SendProjectiles(index);
            NetworkSend.SendJobs(index);
            SendItems(index);

            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SItemEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void Packet_SaveItem(int index, ref byte[] data)
        {
            int n;
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessLevel.Developer)
                return;

            n = buffer.ReadInt32();

            if (n < 0 | n > Core.Constant.MAX_ITEMS)
                return;

            // Update the item
            Core.Data.Item[n].AccessReq = buffer.ReadInt32();

            var statCount = Enum.GetNames(typeof(Stat)).Length;
            for (int i = 0, loopTo = statCount; i < loopTo; i++)
                Core.Data.Item[n].Add_Stat[i] = (byte)buffer.ReadInt32();

            Core.Data.Item[n].Animation = buffer.ReadInt32();
            Core.Data.Item[n].BindType = (byte)buffer.ReadInt32();
            Core.Data.Item[n].JobReq = buffer.ReadInt32();
            Core.Data.Item[n].Data1 = buffer.ReadInt32();
            Core.Data.Item[n].Data2 = buffer.ReadInt32();
            Core.Data.Item[n].Data3 = buffer.ReadInt32();
            Core.Data.Item[n].LevelReq = buffer.ReadInt32();
            Core.Data.Item[n].Mastery = (byte)buffer.ReadInt32();
            Core.Data.Item[n].Name = buffer.ReadString();
            Core.Data.Item[n].Paperdoll = buffer.ReadInt32();
            Core.Data.Item[n].Icon = buffer.ReadInt32();
            Core.Data.Item[n].Price = buffer.ReadInt32();
            Core.Data.Item[n].Rarity = (byte)buffer.ReadInt32();
            Core.Data.Item[n].Speed = buffer.ReadInt32();

            Core.Data.Item[n].Stackable = (byte)buffer.ReadInt32();
            Core.Data.Item[n].Description = buffer.ReadString();

            for (int i = 0, loopTo1 = statCount; i < loopTo1; i++)
                Core.Data.Item[n].Stat_Req[i] = (byte)buffer.ReadInt32();

            Core.Data.Item[n].Type = (byte)buffer.ReadInt32();
            Core.Data.Item[n].SubType = (byte)buffer.ReadInt32();

            Core.Data.Item[n].ItemLevel = (byte)buffer.ReadInt32();

            Core.Data.Item[n].KnockBack = (byte)buffer.ReadInt32();
            Core.Data.Item[n].KnockBackTiles = (byte)buffer.ReadInt32();

            Core.Data.Item[n].Projectile = buffer.ReadInt32();
            Core.Data.Item[n].Ammo = buffer.ReadInt32();

            // Save it
            SaveItem(n);
            SendUpdateItemToAll(n);
            Core.Log.Add(GetAccountLogin(index) + " saved item #" + n + ".", Constant.ADMIN_LOG);
            buffer.Dispose();
        }

        public static void Packet_GetItem(int index, ref byte[] data)
        {
            Player.PlayerMapGetItem(index);
        }

        public static void Packet_DropItem(int index, ref byte[] data)
        {
            int invNum;
            int amount;
            var buffer = new ByteStream(data);

            invNum = buffer.ReadInt32();
            amount = buffer.ReadInt32();
            buffer.Dispose();

            if (Core.Data.TempPlayer[index].InBank | Core.Data.TempPlayer[index].InShop >= 0)
                return;

            // Prevent hacking
            if (invNum < 0 | invNum > Core.Constant.MAX_INV)
                return;

            if (GetPlayerInv(index, invNum) < 0 | GetPlayerInv(index, invNum) > Core.Constant.MAX_ITEMS)
                return;

            if (Core.Data.Item[(int)GetPlayerInv(index, invNum)].Type == (byte)ItemCategory.Currency | Core.Data.Item[(int)GetPlayerInv(index, invNum)].Stackable == 1)
            {
                if (amount < 0 | amount > GetPlayerInvValue(index, invNum))
                    return;
            }

            // everything worked out fine
            Player.PlayerMapDropItem(index, invNum, amount);
        }

        #endregion

        #region Outgoing Packets

        public static void SendItems(int index)
        {
            int i;

            var loopTo = Core.Constant.MAX_ITEMS - 1;
            for (i = 0; i < loopTo; i++)
            {
                if (Strings.Len(Core.Data.Item[i].Name) > 0)
                {
                    SendUpdateItemTo(index, i);
                }
            }

        }

        public static void SendUpdateItemTo(int index, int itemNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateItem);
            buffer.WriteBlock(ItemData(itemNum));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateItemToAll(int itemNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateItem);
            buffer.WriteBlock(ItemData(itemNum));

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        #endregion

    }
}