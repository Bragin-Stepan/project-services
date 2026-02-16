using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Logic.Meta.Features;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta
{
    [CreateAssetMenu(menuName = "Configs/Meta/Stats/NewProgressionStatIconsConfig", fileName = "ProgressionStatIconsConfig")]
    public class ProgressStatIconsConfigSO : ScriptableObject
    {
        [SerializeField] private List<StatConfig> _configs;

        public Sprite GetSpriteFor(ProgressStatTypes currencyType)
            => _configs.First(config => config.Type == currencyType).Sprite;

        [Serializable]
        private class StatConfig
        {
            [field: SerializeField] public ProgressStatTypes Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}