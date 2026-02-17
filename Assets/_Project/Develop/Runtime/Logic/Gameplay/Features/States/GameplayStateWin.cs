using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Reward;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
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
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly WalletService _walletService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly GameProgressionStatsService _gameProgressionStatsService;
        private readonly RewardService _rewardService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly IPlayerInputService _playerInput;
        
        public GameplayStateWin(
            SceneSwitcherService sceneSwitcherService,
            WalletService walletService,
            GameProgressionStatsService gameProgressionStatsService,
            PlayerDataProvider playerDataProvider,
            RewardService rewardService,
            ICoroutinesPerformer coroutinesPerformer,
            IPlayerInputService playerInput)
        {
            _gameProgressionStatsService = gameProgressionStatsService;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider = playerDataProvider;
            _walletService = walletService;
            _rewardService = rewardService;
            _playerInput = playerInput;
        }
    
        public override void OnEnter()
        {
            base.OnEnter();
            
            _walletService.Add(CurrencyTypes.Gold, _rewardService.GetRewardFor(RewardTypes.Win));
            _gameProgressionStatsService.Add(ProgressStatTypes.Win);
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
            
            Debug.Log("Добавили: " + _rewardService.GetRewardFor(RewardTypes.Win) + " монет"); // Перевести в UI
            Debug.Log("Кошелек: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value + " монет");
            Debug.Log($"Ваш счет: {_gameProgressionStatsService.GetStat(ProgressStatTypes.Lose).Value} поражений и {_gameProgressionStatsService.GetStat(ProgressStatTypes.Win).Value} побед");

            Debug.Log("Вы выйграли");
            Debug.Log("=== Нажмите пробел для выхода из игры ===");

            _playerInput.OnJump += OnJumpPressed;
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            _playerInput.OnJump -= OnJumpPressed;
        }
        
        private void OnJumpPressed() => _coroutinesPerformer.StartPerform(
            _sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
    }
}