using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Logic.Meta.Features.Reward;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta
{
    [CreateAssetMenu(menuName = "Configs/Meta/Rewards/NewRewardsConfig", fileName = "RewardsConfig")]
    public class RewardsConfigSO : ScriptableObject
    {
        [SerializeField] private List<RewardsConfig> _values;

        public int GetValueFor(RewardTypes rewardTypes)
            => _values.First(config => config.Type == rewardTypes).Value;

        [Serializable]
        private class RewardsConfig
        {
            [field: SerializeField] public RewardTypes Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}