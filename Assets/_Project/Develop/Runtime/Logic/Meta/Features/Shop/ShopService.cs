using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;

namespace _Project.Develop.Runtime.Logic.Meta.Features.Shop
{
    public class ShopService
    {
        public event Action<ShopItem> ItemAdded;
        public event Action<ShopItem> ItemRemoved;
        public event Action<ShopItem> ItemBought;
        
        private readonly WalletService _wallet;
        private readonly List<ShopItem> _items;
        private readonly PlayerDataProvider _playerData;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        
        public ShopService(
            WalletService wallet,
            PlayerDataProvider playerData,
            ICoroutinesPerformer coroutinesPerformer,
            List<ShopItem> items)
        {
            _wallet = wallet;
            _playerData = playerData;
            _coroutinesPerformer = coroutinesPerformer;
            
            _items = new List<ShopItem>(items);
        }
        
        public IReadOnlyList<ShopItem> Items => _items;
        
        public ShopItem GetItemBy(ItemShopNames name) => _items.Find(item => item.Name == name);

        public bool CanBuy(ShopItem item)
        {
            if (FindItem(item) == false)
                return false;
            
            return _wallet.Enough(item.Currency, item.Price);
        }
        
        public void AddItem(ShopItem item)
        {
            if (FindItem(item) == false)
                throw new ArgumentException($"Item {item.Name} already exists in shop");
            
            _items.Add(item);
            ItemAdded?.Invoke(item);
        }

        public void RemoveItem(ShopItem item)
        {
            if (FindItem(item) == false)
                throw new ArgumentException($"Item {item.Name} not found in shop");
            
            ItemRemoved?.Invoke(item);
            _items.Remove(item);
        }
        
        public bool TryBuy(ShopItem item)
        {
            if (CanBuy(item) == false)
                return false;
            
            _wallet.Spend(item.Currency, item.Price);
            
            item.Effect.Activate();
            ItemBought?.Invoke(item);

            _coroutinesPerformer.StartPerform(_playerData.SaveAsync());
            
            return true;
        }

        private bool FindItem(ShopItem item) => _items.Contains(item);
    }
}