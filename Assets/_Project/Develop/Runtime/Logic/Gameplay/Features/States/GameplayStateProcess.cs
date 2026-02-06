using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence;
using _Project.Develop.Runtime.Utilities.InputManagement;
using _Project.Develop.Runtime.Utilities.StateMachine;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using UnityEngine;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.States
{
    public class GameplayStateProcess : State
    {
        private readonly SequenceFactory _sequenceFactory;
        private readonly GameplayInputArgs _args;
        private readonly IStateChanger _stateChanger;
        private readonly IPlayerInputService _playerInput;
        
        private string[] _sequence;
        private List<string> _currentInput = new();
        private bool _isCompleted;
        
        public GameplayStateProcess(
            SequenceFactory sequenceFactory,
            GameplayInputArgs args,
            IStateChanger stateChanger,
            IPlayerInputService playerInput)
        {
            _args = args;
            _playerInput = playerInput;
            _stateChanger = stateChanger;
            _sequenceFactory = sequenceFactory;
        }
    
        public override void OnEnter()
        {
            base.OnEnter();
            
            Reset();
            
            Debug.Log("=== Запуск игры ===");
            
            _sequence = _sequenceFactory.Create(_args.SequenceCount, _args.GameModeType);
            
            Debug.Log($"Сгенерировано {_sequence.Length} элементов:");
            foreach (string value in _sequence)
                Debug.Log(value);
        }
    
        public override void Update(float deltaTime)
        {
            if (_isCompleted)
                return;
            
            PlayerInputProcess();
        }
        
        public override void OnExit()
        {
            base.OnExit();
            Reset();
        }

        private void PlayerInputProcess()
        {
            string input = _playerInput.GetKeyboardInput();
            
            if (string.IsNullOrEmpty(input))
                return;
            
            _currentInput.Add(input);
            
            Debug.Log($"Ваш ввод: {input}");
            
            if (_currentInput.Count > _sequence.Length || 
                !_sequence[_currentInput.Count - 1].Equals(input))
            {
                StartDefeat();
                return;
            }

            if (_currentInput.Count == _sequence.Length)
                StartWin();
        }

        private void StartWin()
        {
            _stateChanger.ChangeState<GameplayStateWin>();
            _isCompleted = true;
        }

        private void StartDefeat()
        {
            Debug.Log($"ОШИБКА! Ожидалось: {_sequence[_currentInput.Count - 1]}");
            _stateChanger.ChangeState<GameplayStateDefeat>();
            _isCompleted = true;
        }
        
        private void Reset()
        {
            _sequence = null;
            _currentInput.Clear();
            _isCompleted = false;
        }
    }
}