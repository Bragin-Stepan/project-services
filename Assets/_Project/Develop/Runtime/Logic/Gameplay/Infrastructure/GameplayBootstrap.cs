using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using System.Collections;
using _Project.Develop.Runtime.Logic.Gameplay.Features.States;
using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.GameMode;
using _Project.Develop.Runtime.Utilities.InputManagement;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private GameplayStatesFactory _statesFactory;
        private GameplayStateMachine _stateMachine;
        
        private IPlayerInputService _playerInput;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Инициализация режима: {_inputArgs.GameModeType}");
            
            _statesFactory = _container.Resolve<GameplayStatesFactory>();
            _stateMachine = _container.Resolve<GameplayStateMachine>();
            _playerInput = _container.Resolve<IPlayerInputService>();

            _stateMachine
                .Add(_statesFactory.CreateDefeat())
                .Add(_statesFactory.CreateWin())
                .Add(_statesFactory.CreateProcess(_inputArgs));
            
            yield break;
        }

        public override void Run()
        {
            _stateMachine.ChangeState<GameplayStateProcess>();
        }

        private void Update()
        {
            _stateMachine?.Update(Time.deltaTime);
            _playerInput?.Update(Time.deltaTime);
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 20), 
                "Gameplay");
                
            GUI.Label(new Rect(10, 30, 100, 20), 
                "Монет: " + _container.Resolve<WalletService>().GetCurrency(CurrencyTypes.Gold).Value);
            
            GUI.Label(new Rect(10, 50, 100, 20), 
                "Побед: " + _container.Resolve<GameProgressionStatsService>().WinCount.Value);
            
            GUI.Label(new Rect(10, 70, 100, 20), 
                "Поражений: " + _container.Resolve<GameProgressionStatsService>().LoseCount.Value);
        }
    }
}
