using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Features.Wallet;
using TMPro.SpriteAssetUtilities;

namespace _Project.Develop.Runtime.UI.Screens.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;

        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private readonly List<IPresenter> _childPresenters = new();

        public GameplayScreenPresenter(
            GameplayScreenView screen,
            GameplayPresentersFactory gameplayPresentersFactory,
            ProjectPresentersFactory projectPresentersFactory)
        {
            _screen = screen;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _projectPresentersFactory = projectPresentersFactory;
        }

        public void Initialize()
        {
            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }
    }
}
