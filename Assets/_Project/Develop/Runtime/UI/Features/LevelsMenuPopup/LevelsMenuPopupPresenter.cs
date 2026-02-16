using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;

namespace _Project.Develop.Runtime.UI.Features.LevelsMenuPopup
{
    public class LevelsMenuPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "Levels";

        private readonly ConfigsProviderService _configsProviderService;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly LevelsMenuPopupView _view;

        private readonly List<LevelTilePresenter> _levelTilePresenters = new();

        public LevelsMenuPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer, 
            ConfigsProviderService configsProviderService, 
            ProjectPresentersFactory presentersFactory, 
            ViewsFactory viewsFactory, 
            LevelsMenuPopupView view) : base(coroutinesPerformer)
        {
            _configsProviderService = configsProviderService;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(TitleName);

            LevelsListConfigSO levelsListConfig = _configsProviderService.GetConfig<LevelsListConfigSO>();

            foreach (LevelsConfig level in levelsListConfig.Levels)
            {
                LevelTileView levelTileView = _viewsFactory.Create<LevelTileView>();
                
                _view.LevelTilesListView.Add(levelTileView);
                
                LevelTilePresenter levelTilePresenter = _presentersFactory.CreateLevelTilePresenter(levelTileView, level.GameModeType);
                
                levelTilePresenter.Initialize();
                _levelTilePresenters.Add(levelTilePresenter);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (LevelTilePresenter levelTilePresenter in _levelTilePresenters)
            {
                _view.LevelTilesListView.Remove(levelTilePresenter.View);
                _viewsFactory.Release(levelTilePresenter.View);
                levelTilePresenter.Dispose();
            }

            _levelTilePresenters.Clear();
        }

        protected override void OnPreShow()
        {
            base.OnPreShow();

            foreach (LevelTilePresenter levelTilePresenter in _levelTilePresenters)
                levelTilePresenter.Subscribe();
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            foreach (LevelTilePresenter levelTilePresenter in _levelTilePresenters)
                levelTilePresenter.Unsubscribe();
        }
    }
}
