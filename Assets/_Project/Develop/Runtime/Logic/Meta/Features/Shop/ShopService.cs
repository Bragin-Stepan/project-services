using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

namespace _Project.Develop.Runtime.Logic.Meta.Features.Shop
{
    public class ShopService
    {
        public event Action<ShopItem> ItemAdded;
        public event Action<ShopItem> ItemRemoved;
        public event Action<ShopItem> ItemBought;
        
        private readonly WalletService _wallet;
        private readonly List<ShopItem> _items;
        
        public ShopService(
            WalletService wallet,
            List<ShopItem> items)
        {
            _wallet = wallet;
            _items = new List<ShopItem>(items);
        }
        
        public IReadOnlyList<ShopItem> Items => _items;
        
        public bool CanBuy(ShopItem item) => _wallet.Enough(item.Currency, item.Price);
        
        public void AddItem(ShopItem item)
        {
            _items.Add(item);
            ItemAdded?.Invoke(item);
        }

        public void RemoveItem(ShopItem item)
        {
            ItemRemoved?.Invoke(item);
            _items.Remove(item);
        }
        
        public bool TryBuy(ShopItem item)
        {
            if (CanBuy(item) == false)
                return false;
            
            _wallet.Spend(item.Currency, item.Price);
            
            item.Action.Activate();
            
            ItemBought?.Invoke(item);
            
            return true;
        }
    }
}