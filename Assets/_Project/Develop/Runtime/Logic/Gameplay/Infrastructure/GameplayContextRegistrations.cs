using _Project.Develop.Runtime.Configs.Meta;
using _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession;
using _Project.Develop.Runtime.Logic.Gameplay.Features.States;
using _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence;
using _Project.Develop.Runtime.Logic.Meta.Features.Reward;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Screens.Gameplay;
using _Project.Develop.Runtime.Utilities.InputManagement;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(CreateSequenceFactory);
            container.RegisterAsSingle(CreateGameplayStatesFactory);
            container.RegisterAsSingle(CreateGameplayStateMachine);
            container.RegisterAsSingle(CreateGameSession);
            container.RegisterAsSingle(CreateRewardService);
            
            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateGameplayPresentersFactory);
            container.RegisterAsSingle(CreateGameplayPopupService);
        }

        private static SequenceFactory CreateSequenceFactory(DIContainer c) => new(c);
        
        private static GameplayStatesFactory CreateGameplayStatesFactory(DIContainer c) => new(c);

        private static GameplayStateMachine CreateGameplayStateMachine(DIContainer c) => new();
        
        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c) => new(c);
        
        private static RewardService CreateRewardService(DIContainer c)
            => new(c.Resolve<ConfigsProviderService>().GetConfig<RewardsConfigSO>());

        private static GameSessionService CreateGameSession(DIContainer c)
        {
            SequenceFactory sequenceFactory = c.Resolve<SequenceFactory>();
            IPlayerInputService playerInput = c.Resolve<IPlayerInputService>();

            return new GameSessionService(sequenceFactory, playerInput);
        }
        
        private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
        {
            return new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<GameplayUIRoot>(),
                c.Resolve<GameplayPresentersFactory>());
        }

        private static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader loader = c.Resolve<ResourcesAssetsLoader>();
            GameplayUIRoot uiRootPrefab = loader.Load<GameplayUIRoot>(PathToResources.UI.Screens.Gameplay);

            return Object.Instantiate(uiRootPrefab);
        }

        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();
            GameplayScreenView view = c.Resolve<ViewsFactory>().Create<GameplayScreenView>(uiRoot.HUDLayer);
            
            return c.Resolve<GameplayPresentersFactory>().CreateGameplayScreenPresenter(view);
        }
    }
}
