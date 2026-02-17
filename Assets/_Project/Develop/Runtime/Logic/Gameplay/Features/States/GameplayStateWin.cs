using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Reward;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Screens.Gameplay;
using _Project.Develop.Runtime.Utilities.InputManagement;
using _Project.Develop.Runtime.Utilities.StateMachine;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.States
{
    public class GameplayStateWin : State
    {
        private readonly WalletService _walletService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly GameProgressionStatsService _gameProgressionStatsService;
        private readonly RewardService _rewardService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayPopupService _popupService;
        
        public GameplayStateWin(
            WalletService walletService,
            GameProgressionStatsService gameProgressionStatsService,
            PlayerDataProvider playerDataProvider,
            RewardService rewardService,
            ICoroutinesPerformer coroutinesPerformer,
            GameplayPopupService popupService)
        {
            _gameProgressionStatsService = gameProgressionStatsService;
            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider = playerDataProvider;
            _walletService = walletService;
            _rewardService = rewardService;
            _popupService = popupService;
        }
    
        public override void OnEnter()
        {
            base.OnEnter();
            
            _walletService.Add(CurrencyTypes.Gold, _rewardService.GetRewardFor(RewardTypes.Win));
            _gameProgressionStatsService.Add(ProgressStatTypes.Win);
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());

            _popupService.OpenWinPopup();
        }
    }
}