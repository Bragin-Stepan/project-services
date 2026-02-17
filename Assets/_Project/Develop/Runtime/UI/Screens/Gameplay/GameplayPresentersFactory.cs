using _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession;
using _Project.Develop.Runtime.Logic.Gameplay.Features.States;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Features.Gameplay.Results;
using _Project.Develop.Runtime.UI.Features.Gameplay.Sequence;
using _Project.Develop.Runtime.Utilities.GameMode;
using _Project.Develop.Runtime.Utils.ReactiveManagement;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace _Project.Develop.Runtime.UI.Screens.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(
                view, 
                _container.Resolve<WalletService>(),
                _container.Resolve<GameplayPresentersFactory>(),
                _container.Resolve<ProjectPresentersFactory>());
        }
        
        public SequenceTilePresenter CreateSequenceTilePresenter(SequenceTileView view, string text, IReadOnlyVariable<bool> isCorrect)
        {
            return new SequenceTilePresenter(view, text, isCorrect);
        }
        
        public SequenceDisplayPresenter CreateSequenceDisplayPresenter(SequenceTilesListView view)
        {
            return new SequenceDisplayPresenter(
                _container.Resolve<GameSessionService>(),
                _container.Resolve<ViewsFactory>(),
                this,
                view);
        }
        
        public WinPopupPresenter CreateWinPopupPresenter(WinPopupView view)
        {
            return new WinPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                _container.Resolve<SceneSwitcherService>());
        }

        public DefeatPopupPresenter CreateDefeatPopupPresenter(DefeatPopupView view)
        {
            return new DefeatPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<GameModeRunner>(),
                _container.Resolve<GameSessionService>());
        }
    }
}
