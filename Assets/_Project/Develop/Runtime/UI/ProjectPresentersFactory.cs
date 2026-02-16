using _Project.Develop.Runtime.Configs.Meta;
using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Common;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Features.StatsProgression;
using _Project.Develop.Runtime.UI.Features.Wallet;
using _Project.Develop.Runtime.Utils.ReactiveManagement;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace _Project.Develop.Runtime.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType)
        {
            return new CurrencyPresenter(
                currency,
                currencyType,
                _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfigSO>(),
                view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new WalletPresenter(
                _container.Resolve<WalletService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }
        
        public StatProgressPresenter CreateStatProgressPresenter(
            IconTextView view,
            IReadOnlyVariable<int> stat,
            ProgressStatTypes type)
        {
            return new StatProgressPresenter(
                stat,
                type,
                _container.Resolve<ConfigsProviderService>().GetConfig<ProgressStatIconsConfigSO>(),
                view);
        }
        
        public StatListProgressPresenter CreateStatListProgressPresenter(IconTextListView view)
        {
            return new StatListProgressPresenter(
                _container.Resolve<GameProgressionStatsService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }
        
        // public TestPopupPresenter CreateTestPopupPresenter(TestPopupView view)
        // {
        //     return new TestPopupPresenter(
        //         view,
        //         _container.Resolve<ICoroutinesPerformer>());
        // }

        // public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, int levelNumber)
        // {
        //     return new LevelTilePresenter(
        //         _container.Resolve<LevelsProgressionService>(),
        //         _container.Resolve<SceneSwitcherService>(),
        //         _container.Resolve<ICoroutinesPerformer>(),
        //         levelNumber,
        //         view);
        // }
    
        // public LevelsMenuPopupPresenter CreateLevelsMenuPopupPresenter(LevelsMenuPopupView view)
        // {
        //     return new LevelsMenuPopupPresenter(
        //         _container.Resolve<ICoroutinesPerformer>(),
        //         _container.Resolve<ConfigsProviderService>(),
        //         this,
        //         _container.Resolve<ViewsFactory>(),
        //         view);
        // }
    }
}
