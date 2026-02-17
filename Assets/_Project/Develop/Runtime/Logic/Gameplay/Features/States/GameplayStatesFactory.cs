using _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession;
using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Reward;
using _Project.Develop.Runtime.UI.Screens.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.States
{
    public class GameplayStatesFactory
    {
        private readonly DIContainer _container;
        public GameplayStatesFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayStateProcess CreateProcess(GameplayInputArgs inputArgs)
        {
            return new GameplayStateProcess(inputArgs,
                _container.Resolve<GameplayStateMachine>(),
                _container.Resolve<GameSessionService>());
        }
        
        public GameplayStateWin CreateWin()
        {
            return new GameplayStateWin(
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<WalletService>(),
                _container.Resolve<GameProgressionStatsService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<RewardService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<GameplayPopupService>());
        }
        
        public GameplayStateDefeat CreateDefeat()
        {
            return new GameplayStateDefeat(
                _container.Resolve<WalletService>(),
                _container.Resolve<RewardService>(),
                _container.Resolve<GameProgressionStatsService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<GameplayPopupService>());
        }
    }
}