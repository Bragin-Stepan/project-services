using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Meta.Features.Shop;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Screens.MainMenu;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateMainMenuUIRoot).NonLazy();
            container.RegisterAsSingle(CreateMainMenuPresentersFactory);
            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateMainMenuPopupService);
            
            container.RegisterAsSingle(CreateShopService);
            container.RegisterAsSingle(CreateShopItemFactory);
            container.RegisterAsSingle(CreateShopItemActionFactory);
        }
        
        private static ShopItemFactory CreateShopItemFactory(DIContainer c) => new(c);
        private static ShopItemActionFactory CreateShopItemActionFactory(DIContainer c) => new(c);
        
        private static MainMenuPresentersFactory CreateMainMenuPresentersFactory(DIContainer c) => new(c);
        
        private static ShopService CreateShopService(DIContainer c)
        {
            ShopItemFactory factory = c.Resolve<ShopItemFactory>();
            
            List<ShopItem> items = new();

            foreach (ItemShopNames itemName in Enum.GetValues(typeof(ItemShopNames)))
                items.Add(factory.Create(itemName));
            
            return new ShopService(
                c.Resolve<WalletService>(),
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<ICoroutinesPerformer>(),
                items);
        }
        
        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<MainMenuUIRoot>());
        }

        private static MainMenuUIRoot CreateMainMenuUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader loader = c.Resolve<ResourcesAssetsLoader>();
            MainMenuUIRoot uiRootPrefab = loader.Load<MainMenuUIRoot>(PathToResources.UI.Screens.MainMenu);

            return Object.Instantiate(uiRootPrefab);
        }
        

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer c)
        {
            MainMenuUIRoot uiRoot = c.Resolve<MainMenuUIRoot>();
            MainMenuScreenView view = c.Resolve<ViewsFactory>().Create<MainMenuScreenView>(uiRoot.HUDLayer);
            MainMenuScreenPresenter presenter = c.Resolve<MainMenuPresentersFactory>().CreateMainMenuScreen(view);

            return presenter;
        }
    }
}
