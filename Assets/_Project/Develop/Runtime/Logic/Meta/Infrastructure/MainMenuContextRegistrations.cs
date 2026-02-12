using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Meta.Features.Shop;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateShopService);
            container.RegisterAsSingle(CreateShopItemFactory);
            container.RegisterAsSingle(CreateShopItemActionFactory);
        }
        
        private static ShopItemFactory CreateShopItemFactory(DIContainer c) => new(c);
        private static ShopItemActionFactory CreateShopItemActionFactory(DIContainer c) => new(c);
        
        private static ShopService CreateShopService(DIContainer c)
        {
            ShopItemFactory factory = c.Resolve<ShopItemFactory>();
            
            List<ShopItem> items = new();

            foreach (ItemShopNames itemName in Enum.GetValues(typeof(ItemShopNames)))
                factory.Create(itemName);
            
            return new ShopService(c.Resolve<WalletService>(), items);
        }
    }
}
