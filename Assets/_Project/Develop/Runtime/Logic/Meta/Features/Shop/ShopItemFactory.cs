using _Project.Develop.Runtime.Configs.Meta;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;

namespace _Project.Develop.Runtime.Logic.Meta.Features.Shop
{
    public class ShopItemFactory
    {
        private readonly ItemsPriceConfigSO _config;
        private readonly ShopItemActionFactory _actionFactory;
        
        public ShopItemFactory(DIContainer container)
        {
            _config = container.Resolve<ConfigsProviderService>().GetConfig<ItemsPriceConfigSO>();
            _actionFactory = container.Resolve<ShopItemActionFactory>();
        }

        public ShopItem Create(ItemShopNames name)
        {
            int price = _config.GetValueFor(name);
            CurrencyTypes currency = _config.GetCurrencyFor(name);
            IPurchaseEffect action = _actionFactory.Create(name);

            return new ShopItem(price, currency, name, action);
        }
    }
}