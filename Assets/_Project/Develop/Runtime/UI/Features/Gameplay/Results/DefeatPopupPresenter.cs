using _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.GameMode;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace _Project.Develop.Runtime.UI.Features.Gameplay.Results
{
    public class DefeatPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "YOU LOOSE!";

        private readonly DefeatPopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameModeRunner _gameModeRunner;
        private readonly GameSessionService _gameSessionService;

        public DefeatPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            DefeatPopupView view,
            SceneSwitcherService sceneSwitcher,
            GameModeRunner gameModeRunner,
            GameSessionService gameSessionService) : base(coroutinesPerformer)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _view = view;
            _sceneSwitcher = sceneSwitcher;
            _gameModeRunner = gameModeRunner;
            _gameSessionService = gameSessionService;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(TitleName);

            _view.ExitClicked += OnExitClicked;
            _view.RestartClicked += OnRestartClicked;
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _view.ExitClicked -= OnExitClicked;
            _view.RestartClicked -= OnRestartClicked;
        }

        public override void Dispose()
        {
            base.Dispose();

            _view.ExitClicked -= OnExitClicked;
            _view.RestartClicked -= OnRestartClicked;
        }

        private void OnExitClicked()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
            OnCloseRequest();
        }

        private void OnRestartClicked()
        {
            _gameModeRunner.Run(_gameSessionService.GameModeType);
            OnCloseRequest();
        }
    }
}