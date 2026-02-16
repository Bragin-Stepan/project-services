using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Features.StatsProgression;
using _Project.Develop.Runtime.UI.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using TMPro.SpriteAssetUtilities;

namespace _Project.Develop.Runtime.UI.Screens.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;

        private readonly WalletService _walletService;

        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private readonly List<IPresenter> _childPresenters = new();

        public GameplayScreenPresenter(
            GameplayScreenView screen,
            WalletService walletService,
            GameplayPresentersFactory gameplayPresentersFactory,
            ProjectPresentersFactory projectPresentersFactory)
        {
            _screen = screen;
            
            _walletService = walletService;
            
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _projectPresentersFactory = projectPresentersFactory;
        }

        public void Initialize()
        {
            CreateCoins();
            CreateStatList();
            
            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }
        
        private void CreateCoins()
        {
            CurrencyPresenter presenter = _projectPresentersFactory.CreateCurrencyPresenter(
                _screen.CoinsView,
                _walletService.GetCurrency(CurrencyTypes.Gold), 
                CurrencyTypes.Gold);

            _childPresenters.Add(presenter);
        }
        
        private void CreateStatList()
        {
            StatListProgressPresenter presenter = _projectPresentersFactory.CreateStatListProgressPresenter(_screen.StatsView);
            
            _childPresenters.Add(presenter);
        }

        public void Dispose()
        {
            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }
    }
}
