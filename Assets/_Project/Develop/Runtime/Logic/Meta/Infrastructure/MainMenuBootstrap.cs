using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Shop;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.GameMode;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameModeRunner _gameRunner;
        private ShopService _shop;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _gameRunner = _container.Resolve<GameModeRunner>();
            _shop = _container.Resolve<ShopService>();

            yield break;
        }

        public override void Run()
        { }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _gameRunner.Run(GameModeType.Chars);
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
                _gameRunner.Run(GameModeType.Numbers);

            if (Input.GetKeyDown(KeyCode.I))
                foreach (ShopItem item in _shop.Items)
                    Debug.Log($"Item: {item.Name}, Price: {item.Price}");

            if (Input.GetKeyDown(KeyCode.R))
            {
                ShopItem resetGameStats = _shop.GetItemBy(ItemShopNames.ResetGameStats);

                if (_shop.TryBuy(resetGameStats))
                    Debug.Log($"Вы сбросили статистику игр за {resetGameStats.Price} монет");
                else
                    Debug.Log($"Не хватает монет, нужно еще {resetGameStats.Price - _container.Resolve<WalletService>().GetCurrency(CurrencyTypes.Gold).Value}");
            }
        }
    }
}
