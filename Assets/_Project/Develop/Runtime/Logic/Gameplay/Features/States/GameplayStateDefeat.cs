using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Reward;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.InputManagement;
using _Project.Develop.Runtime.Utilities.StateMachine;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using UnityEngine;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.States
{
    public class GameplayStateDefeat : State
    {
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly GameProgressionStatsService _gameProgressionStatsService;
        private readonly WalletService _walletService;
        private readonly RewardService _rewardService;
        private readonly IStateChanger _stateChanger;
        private readonly IPlayerInputService _playerInput;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        
        public GameplayStateDefeat(
            IStateChanger stateChanger,
            IPlayerInputService playerInput,
            WalletService walletService,
            RewardService rewardService,
            GameProgressionStatsService gameProgressionStatsService,
            PlayerDataProvider playerDataProvider,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _gameProgressionStatsService = gameProgressionStatsService;
            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider = playerDataProvider;
            _walletService = walletService;
            _rewardService = rewardService;
            _stateChanger = stateChanger;
            _playerInput = playerInput;
        }
    
        public override void OnEnter()
        {
            base.OnEnter();
            
            _walletService.Spend(CurrencyTypes.Gold, _rewardService.GetRewardFor(RewardTypes.Lose));
            _gameProgressionStatsService.Add(ProgressStatTypes.Lose);
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
            
            Debug.Log("Забрали: " + _rewardService.GetRewardFor(RewardTypes.Lose) + " монет"); // Перевести в UI
            Debug.Log("Кошелек: " + _walletService.GetCurrency(CurrencyTypes.Gold) + " монет");
            Debug.Log($"Ваш счет: {_gameProgressionStatsService.GetStat(ProgressStatTypes.Lose).Value} поражений и {_gameProgressionStatsService.GetStat(ProgressStatTypes.Win).Value} побед");
            
            Debug.Log("Вы проиграли");
            Debug.Log("=== Нажмите пробел для перезапуска ===");

            _playerInput.OnJump += OnJumpPressed;
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            _playerInput.OnJump -= OnJumpPressed;
        }
        
        private void OnJumpPressed()
        {
            Debug.Log("OnJumpPressed");
            _stateChanger.ChangeState<GameplayStateProcess>();
        }
    }
}