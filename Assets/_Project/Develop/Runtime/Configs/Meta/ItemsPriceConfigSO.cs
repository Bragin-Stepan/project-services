using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Logic.Meta.Features.Shop;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta
{
    [CreateAssetMenu(menuName = "Configs/Meta/Shop/NewItemsPriceConfig", fileName = "ItemsPriceConfig")]
    public class ItemsPriceConfigSO : ScriptableObject
    {
        [SerializeField] private List<ItemPriceConfig> _values;

        public int GetValueFor(ItemShopNames itemName)
            => _values.First(config => config.ItemName == itemName).Value;
        
        public CurrencyTypes GetCurrencyFor(ItemShopNames itemName)
            => _values.First(config => config.ItemName == itemName).Currency;

        [Serializable]
        private class ItemPriceConfig
        {
            [field: SerializeField] public ItemShopNames ItemName { get; private set; } // Возможно стоит сделать string
            [field: SerializeField] public CurrencyTypes Currency { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}