using _Project.Develop.Runtime.Configs.Meta;

namespace _Project.Develop.Runtime.Logic.Meta.Features.Reward
{
    public class RewardService
    {
        private readonly RewardsConfigSO _configs;
        
        public RewardService(RewardsConfigSO configs)
        {
            _configs = configs;
        }
        
        public int GetRewardFor(RewardTypes rewardTypes) => _configs.GetValueFor(rewardTypes);
    }
}