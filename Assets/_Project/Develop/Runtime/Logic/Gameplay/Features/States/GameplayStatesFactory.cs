using _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence;
using _Project.Develop.Runtime.Utilities.InputManagement;
using _Project.Develop.Runtime.Utilities.StateMachine;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
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
            SequenceFactory sequenceFactory = _container.Resolve<SequenceFactory>();
            IStateChanger stateChanger = _container.Resolve<GameplayStateMachine>();
            IPlayerInputService playerInput = _container.Resolve<IPlayerInputService>();
            
            return new GameplayStateProcess(sequenceFactory, inputArgs, stateChanger, playerInput);
        }
        
        public GameplayStateWin CreateWin()
        {
            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            IPlayerInputService playerInput = _container.Resolve<IPlayerInputService>();
            
            return new GameplayStateWin(sceneSwitcher, coroutinesPerformer, playerInput);
        }
        
        public GameplayStateDefeat CreateDefeat()
        {
            IStateChanger stateChanger = _container.Resolve<GameplayStateMachine>();
            IPlayerInputService playerInput = _container.Resolve<IPlayerInputService>();
            
            return new GameplayStateDefeat(stateChanger, playerInput);
        }
    }
}