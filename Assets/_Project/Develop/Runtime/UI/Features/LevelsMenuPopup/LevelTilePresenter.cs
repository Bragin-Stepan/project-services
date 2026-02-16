using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.GameMode;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Features.LevelsMenuPopup
{
    public class LevelTilePresenter : ISubscribedPresenter
    {
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly GameModeRunner _gameRunner;
        private readonly GameModeType _gameMode;

        private readonly LevelTileView _view;

        public LevelTilePresenter(
            SceneSwitcherService sceneSwitcherService, 
            GameModeRunner gameRunner,
            GameModeType gameMode, 
            LevelTileView view)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _gameMode = gameMode;
            _gameRunner = gameRunner;
            _view = view;
        }

        public LevelTileView View => _view;

        public void Initialize()
        {
            _view.SetLevel(_gameMode.ToString());
            _view.SetActive();
        }

        public void Dispose()
        {
            _view.Clicked -= OnViewClicked;
        }

        public void Subscribe()
        {
            _view.Clicked += OnViewClicked;
        }

        public void Unsubscribe()
        {
            _view.Clicked -= OnViewClicked;
        }

        private void OnViewClicked()
        {
            _gameRunner.Run(_gameMode);
        }
    }
}
