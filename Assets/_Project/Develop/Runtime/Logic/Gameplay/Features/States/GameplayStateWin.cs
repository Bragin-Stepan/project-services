using _Project.Develop.Runtime.Utilities.InputManagement;
using _Project.Develop.Runtime.Utilities.StateMachine;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.States
{
    public class GameplayStateWin : State
    {
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly IPlayerInputService _playerInput;
        
        public GameplayStateWin(
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            IPlayerInputService playerInput)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _playerInput = playerInput;
        }
    
        public override void OnEnter()
        {
            base.OnEnter();
            
            Debug.Log("Вы выйграли");
            Debug.Log("=== Нажмите пробел для выхода из игры ===");

            _playerInput.OnJump += OnJumpPressed;
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            _playerInput.OnJump -= OnJumpPressed;
        }
        
        private void OnJumpPressed()
            => _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
    }
}