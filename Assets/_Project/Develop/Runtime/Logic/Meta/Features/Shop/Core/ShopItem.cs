using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;

namespace _Project.Develop.Runtime.Logic.Meta.Features.Shop
{
    public class ShopItem
    {
        public int Price { get; }
        public CurrencyTypes Currency { get; }
        public ItemShopNames Name { get; }
        public IBuyableAction Action { get; }

        public ShopItem(
            int price,
            CurrencyTypes currency,
            ItemShopNames name,
            IBuyableAction action)
        {
            Price = price;
            Currency = currency;
            Name = name;
            Action = action;
        }
    }
}