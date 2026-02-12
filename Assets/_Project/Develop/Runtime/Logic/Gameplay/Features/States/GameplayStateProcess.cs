using _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession;
using _Project.Develop.Runtime.Utilities.StateMachine;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.States
{
    public class GameplayStateProcess : State
    {
        private readonly GameplayInputArgs _args;
        private readonly IStateChanger _stateChanger;
        private readonly GameSessionService _gameSession;

        public GameplayStateProcess(
            GameplayInputArgs args,
            IStateChanger stateChanger,
            GameSessionService gameSession)
        {
            _args = args;
            _stateChanger = stateChanger;
            _gameSession = gameSession;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _gameSession.GameWon += OnGameWon;
            _gameSession.GameLost += OnGameLost;
            _gameSession.StartGame(_args);
        }

        public override void Update(float deltaTime)
        {
            _gameSession?.Update();
        }

        public override void OnExit()
        {
            base.OnExit();
            
            if (_gameSession != null)
            {
                _gameSession.GameWon -= OnGameWon;
                _gameSession.GameLost -= OnGameLost;
                _gameSession.Reset();
            }
        }

        private void OnGameWon() => _stateChanger.ChangeState<GameplayStateWin>();
        private void OnGameLost() => _stateChanger.ChangeState<GameplayStateDefeat>();
    }
}