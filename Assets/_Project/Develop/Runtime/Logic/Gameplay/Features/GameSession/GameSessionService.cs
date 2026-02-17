using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence;
using _Project.Develop.Runtime.Utilities.GameMode;
using _Project.Develop.Runtime.Utilities.InputManagement;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using UnityEngine;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession
{
    public class GameSessionService
    {
        public event Action GameWon;
        public event Action GameDefeat;
        public event Action SessionStart;
        
        private readonly SequenceFactory _sequenceFactory;
        private readonly IPlayerInputService _playerInput;

        private GameplayInputArgs _args;
        private List<SequenceTileInfo> _sequence = new();
        private List<string> _currentInput = new();
        private bool _isCompleted;

        public GameSessionService(SequenceFactory sequenceFactory, IPlayerInputService playerInput)
        {
            _sequenceFactory = sequenceFactory;
            _playerInput = playerInput;
        }
        
        public GameModeType GameModeType => _args.GameModeType;
        
        public List<ISequenceTileInfo> Sequence => _sequence.Cast<ISequenceTileInfo>().ToList();

        public void StartGame(GameplayInputArgs args)
        {
            _args = args;
            
            Reset();
            
            foreach (string item in _sequenceFactory.Create(_args.SequenceCount, _args.GameModeType))
                _sequence.Add(new SequenceTileInfo(item));

            SessionStart?.Invoke();
        }

        public void Update()
        {
            if (_isCompleted)
                return;

            string input = _playerInput.GetKeyboardInput();
            if (string.IsNullOrEmpty(input))
                return;

            _currentInput.Add(input);
            
            if (_currentInput.Count > _sequence.Count || _sequence.ElementAt(_currentInput.Count - 1).Value != input)
            {
                _isCompleted = true;
                GameDefeat?.Invoke();
                return;
            }
            
            _sequence.ElementAt(_currentInput.Count - 1).SetCorrect(true);

            if (_currentInput.Count == _sequence.Count)
            {
                _isCompleted = true;
                GameWon?.Invoke();
            }
        }

        public void Reset()
        {
            _sequence.Clear();
            _currentInput.Clear();
            _isCompleted = false;
        }
    }
}