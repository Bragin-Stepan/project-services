using _Project.Develop.Runtime.Utilities.InputManagement;
using _Project.Develop.Runtime.Utilities.StateMachine;
using UnityEngine;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.States
{
    public class GameplayStateDefeat : State
    {
        private readonly IStateChanger _stateChanger;
        private readonly IPlayerInputService _playerInput;
        
        public GameplayStateDefeat(IStateChanger stateChanger, IPlayerInputService playerInput)
        {
            _stateChanger = stateChanger;
            _playerInput = playerInput;
        }
    
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Вы проиграли");
            Debug.Log("=== Нажмите пробел для перезапуска ===");

            _playerInput.OnJump += OnJumpPressed;
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            _playerInput.OnJump -= OnJumpPressed;
        }
        
        private void OnJumpPressed()
        {
            Debug.Log("OnJumpPressed");
            _stateChanger.ChangeState<GameplayStateProcess>();
        }
    }
}