using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.GameMode;
using _Project.Develop.Runtime.Utilities.InputManagement;
using _Project.Develop.Runtime.Utils.ReactiveManagement;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class ProjectContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);
            container.RegisterAsSingle(CreateConfigsProviderService);
            container.RegisterAsSingle(CreateResourcesAssetsLoader);
            container.RegisterAsSingle(CreateSceneLoaderService);
            container.RegisterAsSingle(CreateSceneSwitcherService);
            
            container.RegisterAsSingle(CreateGameProgressionStatsService).NonLazy();
            container.RegisterAsSingle(CreateGameModeRunner);
            container.RegisterAsSingle<IPlayerInputService>(CreateDesktopPlayerInputService);
            
            container.RegisterAsSingle(CreateSaveLoadFactory);
            container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);
            container.RegisterAsSingle(CreatePlayerDataProvider);
            
            container.RegisterAsSingle(CreateWalletService).NonLazy();
            
            container.RegisterAsSingle(CreateProjectPresentersFactory);
            container.RegisterAsSingle(CreateViewsFactory);
            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
        }
        
        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();
            ResourcesConfigsLoader resourcesConfigsLoader = new(resourcesAssetsLoader);

            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new(
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>(),
                c);

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new();

        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c)
            => new();
        
        private static DesktopPlayerInputService CreateDesktopPlayerInputService(DIContainer c)
            => new();
        
        private static SaveLoadFactory CreateSaveLoadFactory(DIContainer c) 
            => new();
        
        private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
            => new(c.Resolve<ISaveLoadService>(), c.Resolve<ConfigsProviderService>());
        
        private static ProjectPresentersFactory CreateProjectPresentersFactory(DIContainer c)
            => new(c);
        
        private static ViewsFactory CreateViewsFactory(DIContainer c)
            => new(c.Resolve<ResourcesAssetsLoader>());
        
        private static GameProgressionStatsService CreateGameProgressionStatsService(DIContainer c)
            => new (c.Resolve<PlayerDataProvider>());

        private static SaveLoadService CreateSaveLoadService(DIContainer c)
            => c.Resolve<SaveLoadFactory>().CreateDefaultSaveLoad();

        private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinesPerformerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerformer>(PathToResources.Util.Coroutine);

            return Object.Instantiate(coroutinesPerformerPrefab);
        }

        private static StandardLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            StandardLoadingScreen standardLoadingScreenPrefab = resourcesAssetsLoader
                .Load<StandardLoadingScreen>(PathToResources.UI.LoadingScreen.Standard);

            return Object.Instantiate(standardLoadingScreenPrefab);
        }
        
        private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[currencyType] = new ReactiveVariable<int>();

            return new WalletService(currencies, c.Resolve<PlayerDataProvider>());
        }
        
        private static GameModeRunner CreateGameModeRunner(DIContainer c)
        {
            LevelsListConfigSO levelsConfig = c.Resolve<ConfigsProviderService>().GetConfig<LevelsListConfigSO>();
            SceneSwitcherService sceneSwitcher = c.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            
            return new GameModeRunner(coroutinesPerformer, levelsConfig, sceneSwitcher);
        }
    }
}
