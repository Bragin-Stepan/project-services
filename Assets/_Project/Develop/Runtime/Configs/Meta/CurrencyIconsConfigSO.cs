using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/NewCurrencyIconsConfig", fileName = "CurrencyIconsConfig")]
    public class CurrencyIconsConfigSO : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _configs;

        public Sprite GetSpriteFor(CurrencyTypes currencyType)
            => _configs.First(config => config.Type == currencyType).Sprite;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}