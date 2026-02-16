using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.UI.Common;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.Features.StatsProgression
{
    public class StatListProgressPresenter : IPresenter
    {
        private readonly GameProgressionStatsService _gameProgression;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly IconTextListView _view;

        private readonly List<StatProgressPresenter> _statPresenters = new();

        public StatListProgressPresenter(
            GameProgressionStatsService gameProgression, 
            ProjectPresentersFactory presentersFactory, 
            ViewsFactory viewsFactory, 
            IconTextListView view)
        {
            _gameProgression = gameProgression;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            foreach (ProgressStatTypes type in _gameProgression.AvailableStats)
            {
                IconTextView statView = _viewsFactory.Create<IconTextView>();

                _view.Add(statView);

                StatProgressPresenter statPresenter = _presentersFactory.CreateStatProgressPresenter(
                    statView,
                    _gameProgression.GetStat(type),
                    type);

                statPresenter.Initialize();
                _statPresenters.Add(statPresenter);
            }
        }

        public void Dispose()
        {
            foreach (StatProgressPresenter statPresenter in _statPresenters)
            {
                _view.Remove(statPresenter.View);
                _viewsFactory.Release(statPresenter.View);
                statPresenter.Dispose();
            }

            _statPresenters.Clear();
        }
    }
}