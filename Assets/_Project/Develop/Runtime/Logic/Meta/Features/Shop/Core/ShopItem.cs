using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;

namespace _Project.Develop.Runtime.Logic.Meta.Features.Shop
{
    public class ShopItem
    {
        public int Price { get; }
        public CurrencyTypes Currency { get; }
        public ItemShopNames Name { get; }
        public IPurchaseEffect Effect { get; }

        public ShopItem(
            int price,
            CurrencyTypes currency,
            ItemShopNames name,
            IPurchaseEffect effect)
        {
            Price = price;
            Currency = currency;
            Name = name;
            Effect = effect;
        }
    }
}