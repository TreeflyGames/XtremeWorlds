using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Client
{
    public class Shop
    {
        #region Fields and Properties

        // Shop-specific states
        private static Dictionary<int, DateTime> ShopLastAccessed { get; set; } = new Dictionary<int, DateTime>();
        private static Dictionary<int, int> ShopPopularity { get; set; } = new Dictionary<int, int>();
        private static readonly object LockObject = new object();

        // Configuration
        private const int DEFAULT_BUY_RATE = 100;
        private const int SHOP_TIMEOUT_SECONDS = 300; // 5 minutes timeout for inactive shops

        // Event for shop updates
        public static event EventHandler<ShopUpdatedEventArgs> OnShopUpdated;

        #endregion

        #region Core Shop Operations

        /// <summary>
        /// Closes the shop interface and resets shop-related states.
        /// </summary>
        public static void CloseShop()
        {
            try
            {
                NetworkSend.SendCloseShop();
                Gui.HideWindow(Gui.GetWindowIndex("winShop"));
                Gui.HideWindow(Gui.GetWindowIndex("winDescription"));
                ResetShopSelection();
                GameState.InShop = -1;
            }
            catch (Exception ex)
            {
                LogError("Error closing shop", ex);
                throw;
            }
        }

        /// <summary>
        /// Resets shop selection states to default values.
        /// </summary>
        private static void ResetShopSelection()
        {
            GameState.shopSelectedSlot = 0L;
            GameState.shopSelectedItem = 0L;
            GameState.shopIsSelling = false;
            GameState.ShopAction = 0;
        }

        #endregion

        #region Database Operations

        /// <summary>
        /// Clears data for a specific shop by index.
        /// </summary>
        /// <param name="index">The shop index to clear.</param>
        public static void ClearShop(int index)
        {
            ValidateShopIndex(index);
            lock (LockObject)
            {
                Core.Type.Shop[index] = new Core.Type.ShopStruct
                {
                    Name = string.Empty,
                    TradeItem = InitializeTradeItems(),
                    BuyRate = DEFAULT_BUY_RATE,
                    Categories = new Dictionary<string, List<int>>() // Initialize categories
                };
                GameState.Shop_Loaded[index] = 0;
                ShopPopularity[index] = 0;
            }
        }

        /// <summary>
        /// Clears all shops in the system.
        /// </summary>
        public static void ClearShops()
        {
            lock (LockObject)
            {
                Core.Type.Shop = new Core.Type.ShopStruct[Constant.MAX_SHOPS];
                ShopPopularity.Clear();
                ShopLastAccessed.Clear();
                for (int i = 0; i < Constant.MAX_SHOPS; i++)
                    ClearShop(i);
            }
        }

        /// <summary>
        /// Streams shop data if not already loaded.
        /// </summary>
        /// <param name="shopNum">The shop number to stream.</param>
        public static void StreamShop(int shopNum)
        {
            ValidateShopIndex(shopNum);
            if (ShouldStreamShop(shopNum))
            {
                lock (LockObject)
                {
                    GameState.Shop_Loaded[shopNum] = 1;
                    SendRequestShop(shopNum);
                    ShopLastAccessed[shopNum] = DateTime.Now;
                }
            }
        }

        private static bool ShouldStreamShop(int shopNum)
        {
            return shopNum >= 0 &&
                   string.IsNullOrEmpty(Core.Type.Shop[shopNum].Name) &&
                   GameState.Shop_Loaded[shopNum] == 0;
        }

        private static Core.Type.TradeItemStruct[] InitializeTradeItems()
        {
            var tradeItems = new Core.Type.TradeItemStruct[Constant.MAX_TRADES];
            for (int x = 0; x < Constant.MAX_TRADES; x++)
            {
                tradeItems[x] = new Core.Type.TradeItemStruct
                {
                    Item = -1,
                    CostItem = -1,
                    ItemValue = 0,
                    CostValue = 0,
                    CurrencyType = 0 // Default currency type
                };
            }
            return tradeItems;
        }

        #endregion

        #region Incoming Packets

        /// <summary>
        /// Handles the packet to open a shop.
        /// </summary>
        public static void Packet_OpenShop(ref byte[] data)
        {
            using (var buffer = new ByteStream(data))
            {
                int shopNum = buffer.ReadInt32();
                ValidateShopIndex(shopNum);
                GameLogic.OpenShop(shopNum);
                TrackShopAccess(shopNum);
            }
        }

        /// <summary>
        /// Resets the shop action state.
        /// </summary>
        public static void Packet_ResetShopAction(ref byte[] data)
        {
            GameState.ShopAction = 0;
        }

        /// <summary>
        /// Updates shop data from a network packet.
        /// </summary>
        public static void Packet_UpdateShop(ref byte[] data)
        {
            using (var buffer = new ByteStream(data))
            {
                int shopNum = buffer.ReadInt32();
                ValidateShopIndex(shopNum);
                lock (LockObject)
                {
                    Core.Type.Shop[shopNum].BuyRate = buffer.ReadInt32();
                    Core.Type.Shop[shopNum].Name = buffer.ReadString() ?? string.Empty;
                    UpdateTradeItems(shopNum, buffer);
                    OnShopUpdated?.Invoke(null, new ShopUpdatedEventArgs(shopNum));
                }
            }
        }

        private static void UpdateTradeItems(int shopNum, ByteStream buffer)
        {
            for (int i = 0; i < Constant.MAX_TRADES; i++)
            {
                Core.Type.Shop[shopNum].TradeItem[i].CostItem = buffer.ReadInt32();
                Core.Type.Shop[shopNum].TradeItem[i].CostValue = buffer.ReadInt32();
                Core.Type.Shop[shopNum].TradeItem[i].Item = buffer.ReadInt32();
                Core.Type.Shop[shopNum].TradeItem[i].ItemValue = buffer.ReadInt32();
                Core.Type.Shop[shopNum].TradeItem[i].CurrencyType = buffer.ReadInt32(); // Read currency type
            }
        }

        #endregion

        #region Outgoing Packets

        /// <summary>
        /// Sends a request to load shop data.
        /// </summary>
        public static void SendRequestShop(int shopNum)
        {
            using (var buffer = new ByteStream(4))
            {
                buffer.WriteInt32((int)Packets.ClientPackets.CRequestShop);
                buffer.WriteInt32(shopNum);
                SendData(buffer);
            }
        }

        /// <summary>
        /// Sends a request to buy an item from the shop.
        /// </summary>
        public static void BuyItem(int shopSlot)
        {
            ValidateSlot(shopSlot);
            if (!CanBuyItem(shopSlot)) return;
            using (var buffer = new ByteStream(4))
            {
                buffer.WriteInt32((int)Packets.ClientPackets.CBuyItem);
                buffer.WriteInt32(shopSlot);
                SendData(buffer);
            }
        }

        /// <summary>
        /// Sends a request to sell an item to the shop.
        /// </summary>
        public static void SellItem(int invSlot)
        {
            ValidateSlot(invSlot);
            if (!CanSellItem(invSlot)) return;
            using (var buffer = new ByteStream(4))
            {
                buffer.WriteInt32((int)Packets.ClientPackets.CSellItem);
                buffer.WriteInt32(invSlot);
                SendData(buffer);
            }
        }

        private static void SendData(ByteStream buffer)
        {
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
        }

        #endregion

        #region New Features

        #region Inventory Integration

        /// <summary>
        /// Checks if the player can buy the item (e.g., has enough currency).
        /// </summary>
        public static bool CanBuyItem(int shopSlot)
        {
            var shop = Core.Type.Shop[GameState.InShop];
            var item = shop.TradeItem[shopSlot];
            var currencyType = item.CurrencyType;
            var cost = item.CostValue;

            // Assuming GameState.Player has a method to check currency
            return GameState.Player.HasCurrency(currencyType, cost);
        }

        /// <summary>
        /// Checks if the player can sell the item (e.g., item is in inventory).
        /// </summary>
        public static bool CanSellItem(int invSlot)
        {
            // Assuming GameState.Player has a method to check inventory
            return GameState.Player.HasItemInInventory(invSlot);
        }

        #endregion

        #region Currency System

        /// <summary>
        /// Gets the list of supported currency types.
        /// </summary>
        public static List<int> GetSupportedCurrencies()
        {
            // Assuming currency types are defined in Core.Type
            return Core.Type.CurrencyTypes.ToList();
        }

        /// <summary>
        /// Sets the currency type for a trade item.
        /// </summary>
        public static void SetTradeItemCurrency(int shopNum, int slot, int currencyType)
        {
            ValidateShopIndex(shopNum);
            ValidateSlot(slot);
            if (!GetSupportedCurrencies().Contains(currencyType))
                throw new ArgumentException("Invalid currency type.");

            lock (LockObject)
            {
                Core.Type.Shop[shopNum].TradeItem[slot].CurrencyType = currencyType;
            }
        }

        #endregion

        #region Shop Categories

        /// <summary>
        /// Adds a category to the shop.
        /// </summary>
        public static void AddCategory(int shopNum, string categoryName)
        {
            ValidateShopIndex(shopNum);
            lock (LockObject)
            {
                if (!Core.Type.Shop[shopNum].Categories.ContainsKey(categoryName))
                {
                    Core.Type.Shop[shopNum].Categories[categoryName] = new List<int>();
                }
            }
        }

        /// <summary>
        /// Assigns a trade item to a category.
        /// </summary>
        public static void AssignItemToCategory(int shopNum, int slot, string categoryName)
        {
            ValidateShopIndex(shopNum);
            ValidateSlot(slot);
            lock (LockObject)
            {
                if (Core.Type.Shop[shopNum].Categories.TryGetValue(categoryName, out var items))
                {
                    if (!items.Contains(slot))
                        items.Add(slot);
                }
                else
                {
                    throw new InvalidOperationException("Category does not exist.");
                }
            }
        }

        /// <summary>
        /// Retrieves items in a specific category.
        /// </summary>
        public static List<int> GetItemsInCategory(int shopNum, string categoryName)
        {
            ValidateShopIndex(shopNum);
            lock (LockObject)
            {
                if (Core.Type.Shop[shopNum].Categories.TryGetValue(categoryName, out var items))
                {
                    return new List<int>(items);
                }
                return new List<int>();
            }
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Saves shop data to a file.
        /// </summary>
        public static void SaveShopData(string filePath)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, Core.Type.Shop);
                }
            }
            catch (Exception ex)
            {
                LogError("Error saving shop data", ex);
                throw;
            }
        }

        /// <summary>
        /// Loads shop data from a file.
        /// </summary>
        public static void LoadShopData(string filePath)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Core.Type.Shop = (Core.Type.ShopStruct[])formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                LogError("Error loading shop data", ex);
                throw;
            }
        }

        #endregion

        /// <summary>
        /// Calculates the total value of items in a shop.
        /// </summary>
        public static long CalculateShopValue(int shopNum)
        {
            ValidateShopIndex(shopNum);
            return Core.Type.Shop[shopNum].TradeItem
                .Where(t => t.Item >= 0)
                .Sum(t => t.ItemValue * t.CostValue);
        }

        /// <summary>
        /// Checks if the shop has expired based on last access time.
        /// </summary>
        public static bool IsShopExpired(int shopNum)
        {
            if (!ShopLastAccessed.ContainsKey(shopNum)) return true;
            return (DateTime.Now - ShopLastAccessed[shopNum]).TotalSeconds > SHOP_TIMEOUT_SECONDS;
        }

        /// <summary>
        /// Applies a discount to the shop's buy rate asynchronously.
        /// </summary>
        public static async Task ApplyDiscountAsync(int shopNum, int discountPercentage)
        {
            ValidateShopIndex(shopNum);
            if (discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentException("Discount percentage must be between 0 and 100.");

            await Task.Run(() =>
            {
                lock (LockObject)
                {
                    int newRate = Core.Type.Shop[shopNum].BuyRate * (100 - discountPercentage) / 100;
                    Core.Type.Shop[shopNum].BuyRate = Math.Max(1, newRate); // Ensure minimum rate
                }
            });
        }

        /// <summary>
        /// Retrieves the most popular shop based on access frequency.
        /// </summary>
        public static int GetMostPopularShop()
        {
            lock (LockObject)
            {
                return ShopPopularity.Any() ? ShopPopularity.OrderByDescending(kv => kv.Value).First().Key : -1;
            }
        }

        /// <summary>
        /// Adds a new trade item to the shop.
        /// </summary>
        public static void AddTradeItem(int shopNum, int itemId, int costItemId, int itemValue, int costValue, int currencyType = 0)
        {
            ValidateShopIndex(shopNum);
            lock (LockObject)
            {
                var tradeItems = Core.Type.Shop[shopNum].TradeItem;
                int freeSlot = Array.FindIndex(tradeItems, t => t.Item == -1);
                if (freeSlot == -1) throw new InvalidOperationException("No free trade slots available.");

                tradeItems[freeSlot] = new Core.Type.TradeItemStruct
                {
                    Item = itemId,
                    CostItem = costItemId,
                    ItemValue = itemValue,
                    CostValue = costValue,
                    CurrencyType = currencyType
                };
            }
        }

        #endregion

        #region Helper Methods

        private static void ValidateShopIndex(int index)
        {
            if (index < 0 || index >= Constant.MAX_SHOPS)
                throw new ArgumentOutOfRangeException(nameof(index), $"Shop index must be between 0 and {Constant.MAX_SHOPS - 1}.");
        }

        private static void ValidateSlot(int slot)
        {
            if (slot < 0 || slot >= Constant.MAX_TRADES)
                throw new ArgumentOutOfRangeException(nameof(slot), $"Slot must be between 0 and {Constant.MAX_TRADES - 1}.");
        }

        private static void TrackShopAccess(int shopNum)
        {
            lock (LockObject)
            {
                ShopPopularity[shopNum] = ShopPopularity.GetValueOrDefault(shopNum) + 1;
                ShopLastAccessed[shopNum] = DateTime.Now;
            }
        }

        private static void LogError(string message, Exception ex)
        {
            // Placeholder for actual logging implementation
            Console.WriteLine($"{message}: {ex.Message}");
        }

        #endregion
    }

    #region EventArgs

    public class ShopUpdatedEventArgs : EventArgs
    {
        public int ShopNumber { get; }

        public ShopUpdatedEventArgs(int shopNumber)
        {
            ShopNumber = shopNumber;
        }
    }

    #endregion
}
