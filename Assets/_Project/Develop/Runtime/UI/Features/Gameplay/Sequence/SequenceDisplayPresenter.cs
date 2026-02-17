using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Screens.Gameplay;
using _Project.Develop.Runtime.Utils.ReactiveManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Features.Gameplay.Sequence
{
    public class SequenceDisplayPresenter : IPresenter
    {
        private readonly GameSessionService _gameSession;
        private readonly ViewsFactory _viewsFactory;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        
        private readonly SequenceTilesListView _view;
        
        private readonly List<SequenceTilePresenter> _tilePresenters = new();
        
        public SequenceDisplayPresenter(
            GameSessionService gameSession,
            ViewsFactory viewsFactory,
            GameplayPresentersFactory gameplayPresentersFactory,
            SequenceTilesListView view)
        {
            _gameSession = gameSession;
            _viewsFactory = viewsFactory;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _view = view;
        }
        
        public void Initialize()
        {
            _gameSession.SessionStart += OnSessionStart;
        }

        private void OnSessionStart()
        {
            foreach (ISequenceTileInfo item in _gameSession.Sequence)
            {
                Debug.Log($"SequenceDisplayPresenter: OnSessionStart: {item} {item.Value}");
                
                SequenceTileView tileView = _viewsFactory.Create<SequenceTileView>();
                
                _view.Add(tileView);
                
                SequenceTilePresenter tilePresenter = _gameplayPresentersFactory.CreateSequenceTilePresenter(tileView, item.Value, item.IsCorrect);
                tilePresenter.Initialize();
                tilePresenter.Subscribe();
                
                _tilePresenters.Add(tilePresenter);
            }
        }

        public void Dispose()
        {
            _gameSession.SessionStart -= OnSessionStart;
            
            foreach (SequenceTilePresenter presenter in _tilePresenters)
            {
                _view.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Unsubscribe();
                presenter.Dispose();
            }
            
            _tilePresenters.Clear();
        }
    }
}