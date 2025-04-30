using System;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Core.Enum;
using static Core.Packets;
using static Core.Global.Command;

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
            string json = JsonConvert.SerializeObject(Core.Type.Item[itemNum]).ToString();

            if (Database.RowExists(itemNum, "item"))
            {
                Database.UpdateRow(itemNum, json, "item", "data");
            }
            else
            {
                Database.InsertRow(itemNum, json, "item");
            }
        }

        public static async Task LoadItemsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_ITEMS).Select(i => Task.Run(() => LoadItemAsync(i)));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadItemAsync(int itemNum)
        {
            JObject data;

            data = await Database.SelectRowAsync(itemNum, "item", "data");

            if (data is null)
            {
                ClearItem(itemNum);
                return;
            }

            var itemData = JObject.FromObject(data).ToObject<Core.Type.ItemStruct>();
            Core.Type.Item[itemNum] = itemData;
        }

        public static void ClearItem(int index)
        {
            Core.Type.Item[index].Name = "";
            Core.Type.Item[index].Description = "";
            Core.Type.Item[index].Stackable = 1;

            for (int i = 0, loopTo = Core.Constant.MAX_ITEMS; i < loopTo; i++)
            {
                Core.Type.Item[(i)].Add_Stat = new byte[(int)Core.Enum.StatType.Count];
                Core.Type.Item[i].Stat_Req = new byte[(int)Core.Enum.StatType.Count];
            }
        }

        public static byte[] ItemData(int itemNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(itemNum);
            buffer.WriteInt32(Core.Type.Item[itemNum].AccessReq);

            for (int i = 0, loopTo = (byte)StatType.Count; i < loopTo; i++)
                buffer.WriteInt32(Core.Type.Item[itemNum].Add_Stat[i]);

            buffer.WriteInt32(Core.Type.Item[itemNum].Animation);
            buffer.WriteInt32(Core.Type.Item[itemNum].BindType);
            buffer.WriteInt32(Core.Type.Item[itemNum].JobReq);
            buffer.WriteInt32(Core.Type.Item[itemNum].Data1);
            buffer.WriteInt32(Core.Type.Item[itemNum].Data2);
            buffer.WriteInt32(Core.Type.Item[itemNum].Data3);
            buffer.WriteInt32(Core.Type.Item[itemNum].LevelReq);
            buffer.WriteInt32(Core.Type.Item[itemNum].Mastery);
            buffer.WriteString(Core.Type.Item[itemNum].Name);
            buffer.WriteInt32(Core.Type.Item[itemNum].Paperdoll);
            buffer.WriteInt32(Core.Type.Item[itemNum].Icon);
            buffer.WriteInt32(Core.Type.Item[itemNum].Price);
            buffer.WriteInt32(Core.Type.Item[itemNum].Rarity);
            buffer.WriteInt32(Core.Type.Item[itemNum].Speed);

            buffer.WriteInt32(Core.Type.Item[itemNum].Stackable);
            buffer.WriteString(Core.Type.Item[itemNum].Description);

            for (int i = 0, loopTo1 = (byte)StatType.Count; i < loopTo1; i++)
                buffer.WriteInt32(Core.Type.Item[itemNum].Stat_Req[i]);

            buffer.WriteInt32(Core.Type.Item[itemNum].Type);
            buffer.WriteInt32(Core.Type.Item[itemNum].SubType);

            buffer.WriteInt32(Core.Type.Item[itemNum].ItemLevel);

            buffer.WriteInt32(Core.Type.Item[itemNum].KnockBack);
            buffer.WriteInt32(Core.Type.Item[itemNum].KnockBackTiles);
            buffer.WriteInt32(Core.Type.Item[itemNum].Projectile);
            buffer.WriteInt32(Core.Type.Item[itemNum].Ammo);
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
                buffer.WriteInt32((int)Core.Type.MapItem[mapNum, i].Num);
                buffer.WriteInt32(Core.Type.MapItem[mapNum, i].Value);
                buffer.WriteInt32(Core.Type.MapItem[mapNum, i].X);
                buffer.WriteInt32(Core.Type.MapItem[mapNum, i].Y);
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
                buffer.WriteInt32((int)Core.Type.MapItem[mapNum, i].Num);
                buffer.WriteInt32(Core.Type.MapItem[mapNum, i].Value);
                buffer.WriteInt32(Core.Type.MapItem[mapNum, i].X);
                buffer.WriteInt32(Core.Type.MapItem[mapNum, i].Y);
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
                Core.Type.MapItem[mapNum, i].Num = itemNum;
                Core.Type.MapItem[mapNum, i].Value = ItemVal;
                Core.Type.MapItem[mapNum, i].X = (byte)x;
                Core.Type.MapItem[mapNum, i].Y = (byte)y;

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
                if (Core.Type.MapItem[mapNum, i].Num == -1)
                {
                    FindOpenMapItemSlotRet = i;
                    return FindOpenMapItemSlotRet;
                }
            }

            return FindOpenMapItemSlotRet;

        }

        public static async Task SpawnAllMapsItemsAsync()
        {
            int i;

            var loopTo = Core.Constant.MAX_MAPS;
            for (i = 0; i < loopTo; i++)
                await SpawnMapItemsAsync(i);
        }

        public static async Task SpawnMapItemsAsync(int mapNum)
        {
            int x;
            int y;

            // Check for subscript out of range  
            if (mapNum < 0 | mapNum > Core.Constant.MAX_MAPS)
                return;

            if (Core.Type.Map[mapNum].NoRespawn)
                return;

            // Spawn what we have  
            var loopTo = (int)Core.Type.Map[mapNum].MaxX;
            for (x = 0; x < (int)loopTo; x++)
            {
                var loopTo1 = (int)Core.Type.Map[mapNum].MaxY;
                for (y = 0; y < (int)loopTo1; y++)
                {
                    // Check if the tile type is an item or a saved tile incase someone drops something  
                    if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.Item)
                    {
                        // Check to see if its a currency and if they set the value to 0 set it to 1 automatically  
                        if (Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1].Type == (byte)ItemType.Currency | Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1].Stackable == 1)
                        {
                            if (Core.Type.Map[mapNum].Tile[x, y].Data2 < 1)
                            {
                                await SpawnItemAsync(Core.Type.Map[mapNum].Tile[x, y].Data1, 1, mapNum, x, y);
                            }
                            else
                            {
                                await SpawnItemAsync(Core.Type.Map[mapNum].Tile[x, y].Data1, Core.Type.Map[mapNum].Tile[x, y].Data2, mapNum, x, y);
                            }
                        }
                        else
                        {
                            await SpawnItemAsync(Core.Type.Map[mapNum].Tile[x, y].Data1, Core.Type.Map[mapNum].Tile[x, y].Data2, mapNum, x, y);
                        }
                    }

                    // Check if the tile type is an item or a saved tile incase someone drops something  
                    if (Core.Type.Map[mapNum].Tile[x, y].Type2 == TileType.Item)
                    {
                        // Check to see if its a currency and if they set the value to 0 set it to 1 automatically  
                        if (Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1_2].Type == (byte)ItemType.Currency | Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1_2].Stackable == 1 | Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1_2].Type == (byte)ItemType.Currency | Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1_2].Stackable == 1)
                        {
                            if (Core.Type.Map[mapNum].Tile[x, y].Data2_2 < 1)
                            {
                                await SpawnItemAsync(Core.Type.Map[mapNum].Tile[x, y].Data1_2, 1, mapNum, x, y);
                            }
                            else
                            {
                                await SpawnItemAsync(Core.Type.Map[mapNum].Tile[x, y].Data1_2, Core.Type.Map[mapNum].Tile[x, y].Data2_2, mapNum, x, y);
                            }
                        }
                        else
                        {
                            await SpawnItemAsync(Core.Type.Map[mapNum].Tile[x, y].Data1_2, Core.Type.Map[mapNum].Tile[x, y].Data2_2, mapNum, x, y);
                        }
                    }
                }
            }
        }

        public static async Task SpawnItemAsync(int itemNum, int ItemVal, int mapNum, int x, int y)
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

        public static async Task SpawnItemSlotAsync(int MapItemSlot, int itemNum, int ItemVal, int mapNum, int x, int y)
        {
            var buffer = new ByteStream(4);

            // Check for subscript out of range  
            if (MapItemSlot < 0 || MapItemSlot > Core.Constant.MAX_MAP_ITEMS || itemNum > Core.Constant.MAX_ITEMS || mapNum < 0 || mapNum > Core.Constant.MAX_MAPS)
                return;

            if (MapItemSlot != -1)
            {
                Core.Type.MapItem[mapNum, MapItemSlot].Num = itemNum;
                Core.Type.MapItem[mapNum, MapItemSlot].Value = ItemVal;
                Core.Type.MapItem[mapNum, MapItemSlot].X = (byte)x;
                Core.Type.MapItem[mapNum, MapItemSlot].Y = (byte)y;

                buffer.WriteInt32((int)ServerPackets.SSpawnItem);
                buffer.WriteInt32(MapItemSlot);
                buffer.WriteInt32(itemNum);
                buffer.WriteInt32(ItemVal);
                buffer.WriteInt32(x);
                buffer.WriteInt32(y);

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

            if (Core.Type.Map[mapNum].NoRespawn)
                return;

            // Spawn what we have
            var loopTo = (int)Core.Type.Map[mapNum].MaxX;
            for (x = 0; x < (int)loopTo; x++)
            {
                var loopTo1 = (int)Core.Type.Map[mapNum].MaxY;
                for (y = 0; y < (int)loopTo1; y++)
                {
                    // Check if the tile type is an item or a saved tile incase someone drops something
                    if (Core.Type.Map[mapNum].Tile[x, y].Type == TileType.Item)
                    {
                        // Check to see if its a currency and if they set the value to 0 set it to 1 automatically
                        if (Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1].Type == (byte)ItemType.Currency | Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1].Stackable == 1)
                        {
                            if (Core.Type.Map[mapNum].Tile[x, y].Data2 < 1)
                            {
                                SpawnItem(Core.Type.Map[mapNum].Tile[x, y].Data1, 1, mapNum, x, y);
                            }
                            else
                            {
                                SpawnItem(Core.Type.Map[mapNum].Tile[x, y].Data1, Core.Type.Map[mapNum].Tile[x, y].Data2, mapNum, x, y);
                            }
                        }
                        else
                        {
                            SpawnItem(Core.Type.Map[mapNum].Tile[x, y].Data1, Core.Type.Map[mapNum].Tile[x, y].Data2, mapNum, x, y);
                        }
                    }

                    // Check if the tile type is an item or a saved tile incase someone drops something
                    if (Core.Type.Map[mapNum].Tile[x, y].Type2 == TileType.Item)
                    {
                        // Check to see if its a currency and if they set the value to 0 set it to 1 automatically
                        if (Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1_2].Type == (byte)ItemType.Currency | Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1_2].Stackable == 1 | Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1_2].Type == (byte)ItemType.Currency | Core.Type.Item[Core.Type.Map[mapNum].Tile[x, y].Data1_2].Stackable == 1)
                        {
                            if (Core.Type.Map[mapNum].Tile[x, y].Data2_2 < 1)
                            {
                                SpawnItem(Core.Type.Map[mapNum].Tile[x, y].Data1_2, 1, mapNum, x, y);
                            }
                            else
                            {
                                SpawnItem(Core.Type.Map[mapNum].Tile[x, y].Data1_2, Core.Type.Map[mapNum].Tile[x, y].Data2_2, mapNum, x, y);
                            }
                        }
                        else
                        {
                            SpawnItem(Core.Type.Map[mapNum].Tile[x, y].Data1_2, Core.Type.Map[mapNum].Tile[x, y].Data2_2, mapNum, x, y);
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

        public static void Packet_EditItem(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Mapper)
                return;
            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Item);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            Core.Type.TempPlayer[index].Editor = (byte) EditorType.Item;

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
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;

            n = buffer.ReadInt32();

            if (n < 0 | n > Core.Constant.MAX_ITEMS)
                return;

            // Update the item
            Core.Type.Item[n].AccessReq = buffer.ReadInt32();

            for (int i = 0, loopTo = (byte)StatType.Count; i < loopTo; i++)
                Core.Type.Item[n].Add_Stat[i] = (byte)buffer.ReadInt32();

            Core.Type.Item[n].Animation = buffer.ReadInt32();
            Core.Type.Item[n].BindType = (byte)buffer.ReadInt32();
            Core.Type.Item[n].JobReq = buffer.ReadInt32();
            Core.Type.Item[n].Data1 = buffer.ReadInt32();
            Core.Type.Item[n].Data2 = buffer.ReadInt32();
            Core.Type.Item[n].Data3 = buffer.ReadInt32();
            Core.Type.Item[n].LevelReq = buffer.ReadInt32();
            Core.Type.Item[n].Mastery = (byte)buffer.ReadInt32();
            Core.Type.Item[n].Name = buffer.ReadString();
            Core.Type.Item[n].Paperdoll = buffer.ReadInt32();
            Core.Type.Item[n].Icon = buffer.ReadInt32();
            Core.Type.Item[n].Price = buffer.ReadInt32();
            Core.Type.Item[n].Rarity = (byte)buffer.ReadInt32();
            Core.Type.Item[n].Speed = buffer.ReadInt32();

            Core.Type.Item[n].Stackable = (byte)buffer.ReadInt32();
            Core.Type.Item[n].Description = buffer.ReadString();

            for (int i = 0, loopTo1 = (byte)StatType.Count; i < loopTo1; i++)
                Core.Type.Item[n].Stat_Req[i] = (byte)buffer.ReadInt32();

            Core.Type.Item[n].Type = (byte)buffer.ReadInt32();
            Core.Type.Item[n].SubType = (byte)buffer.ReadInt32();

            Core.Type.Item[n].ItemLevel = (byte)buffer.ReadInt32();

            Core.Type.Item[n].KnockBack = (byte)buffer.ReadInt32();
            Core.Type.Item[n].KnockBackTiles = (byte)buffer.ReadInt32();

            Core.Type.Item[n].Projectile = buffer.ReadInt32();
            Core.Type.Item[n].Ammo = buffer.ReadInt32();

            // Save it
            SaveItem(n);
            SendUpdateItemToAll(n);
            Core.Log.Add(GetPlayerLogin(index) + " saved item #" + n + ".", Constant.ADMIN_LOG);
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

            if (Core.Type.TempPlayer[index].InBank | Core.Type.TempPlayer[index].InShop >= 0)
                return;

            // Prevent hacking
            if (invNum < 0 | invNum > Core.Constant.MAX_INV)
                return;

            if (GetPlayerInv(index, invNum) < 0 | GetPlayerInv(index, invNum) > Core.Constant.MAX_ITEMS)
                return;

            if (Core.Type.Item[(int)GetPlayerInv(index, invNum)].Type == (byte)ItemType.Currency | Core.Type.Item[(int)GetPlayerInv(index, invNum)].Stackable == 1)
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
                if (Strings.Len(Core.Type.Item[i].Name) > 0)
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