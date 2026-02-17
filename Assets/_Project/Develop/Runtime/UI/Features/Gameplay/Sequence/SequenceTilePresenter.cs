using System;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utils.ReactiveManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Features.Gameplay.Sequence
{
    public class SequenceTilePresenter : ISubscribedPresenter
    {
        private readonly SequenceTileView _view;
        private readonly IReadOnlyVariable<bool> _isCorrect;
        private readonly string _text;
        
        IDisposable _isCorrectSubscription;
        
        public SequenceTilePresenter(
            SequenceTileView view,
            string text,
            IReadOnlyVariable<bool> isCorrect)
        {
            _view = view;
            _text = text;
            _isCorrect = isCorrect;
        }
        
        public SequenceTileView View => _view;

        public void Initialize()
        {
            _view.SetText(_text);
        }
        
        private void OnChanged(bool arg1, bool newValue)
        {
            if (newValue)
                _view.SetComplete();
        }

        public void Subscribe()
        {
            _isCorrectSubscription = _isCorrect.Subscribe(OnChanged);
        }
        
        public void Unsubscribe()
        {
            _isCorrectSubscription?.Dispose();
        }

        public void Dispose()
        {
            _isCorrectSubscription?.Dispose();
        }
    }
}