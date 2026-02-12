using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence;
using _Project.Develop.Runtime.Utilities.InputManagement;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using UnityEngine;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession
{
    public class GameSessionService
    {
         public event Action GameWon;
        public event Action GameLost;
        
        private readonly SequenceFactory _sequenceFactory;
        private readonly IPlayerInputService _playerInput;

        private GameplayInputArgs _args;
        private string[] _sequence;
        private List<string> _currentInput = new();
        private bool _isCompleted;

        public GameSessionService(SequenceFactory sequenceFactory, IPlayerInputService playerInput)
        {
            _sequenceFactory = sequenceFactory;
            _playerInput = playerInput;
        }

        public void StartGame(GameplayInputArgs args)
        {
            _args = args;
            
            Reset();
            
            Debug.Log("=== Запуск игры ===");
            
            _sequence = _sequenceFactory.Create(_args.SequenceCount, _args.GameModeType);
            
            Debug.Log($"Сгенерировано {_sequence.Length} элементов:");
            
            foreach (string value in _sequence)
                Debug.Log(value);
        }

        public void Update()
        {
            if (_isCompleted)
                return;

            string input = _playerInput.GetKeyboardInput();
            if (string.IsNullOrEmpty(input))
                return;

            _currentInput.Add(input);
            Debug.Log($"Ваш ввод: {input}");

            if (_currentInput.Count > _sequence.Length || 
                !_sequence[_currentInput.Count - 1].Equals(input))
            {
                Debug.Log($"ОШИБКА! Ожидалось: {_sequence[_currentInput.Count - 1]}");
                _isCompleted = true;
                GameLost?.Invoke();
                return;
            }

            if (_currentInput.Count == _sequence.Length)
            {
                _isCompleted = true;
                GameWon?.Invoke();
            }
        }

        public void Reset()
        {
            _sequence = null;
            _currentInput.Clear();
            _isCompleted = false;
        }
    }
}