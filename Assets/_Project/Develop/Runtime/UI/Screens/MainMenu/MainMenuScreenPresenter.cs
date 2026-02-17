using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Meta.Features.Shop;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Features.StatsProgression;
using _Project.Develop.Runtime.UI.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Screens.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly MainMenuPopupService _popupService;
        private readonly ShopService _shop;

        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            ShopService shop,
            ProjectPresentersFactory projectPresentersFactory,
            MainMenuPopupService popupService)
        {
            _shop = shop;
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _popupService = popupService;
        }

        public void Initialize()
        {
            _screen.OpenLevelsMenuButtonClicked += OnOpenLevelsMenuButtonClicked;
            _screen.ResetStatsButtonClicked += OnResetStatsButtonClicked;

            CreateWallet();
            CreateStatList();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.OpenLevelsMenuButtonClicked -= OnOpenLevelsMenuButtonClicked;
            _screen.ResetStatsButtonClicked -= OnResetStatsButtonClicked;

            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_screen.WalletView);
            _childPresenters.Add(walletPresenter);
        }
        
        private void CreateStatList()
        {
            StatListProgressPresenter presenter = _projectPresentersFactory.CreateStatListProgressPresenter(_screen.StatsView);
            _childPresenters.Add(presenter);
        }

        private void OnOpenLevelsMenuButtonClicked()
        {
            _popupService.OpenLevelsMenuPopup();
        }
        
        private void OnResetStatsButtonClicked()
        {
            _shop.TryBuy(_shop.GetItemBy(ItemShopNames.ResetGameStats));
        }
    }
}
