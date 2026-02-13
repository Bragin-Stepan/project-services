namespace _Project.Develop.Runtime.Logic.Meta.Features.Shop
{
    public class ResetLoseStatShopItemAction : IBuyableAction
    {
        private readonly GameProgressionStatsService _stats;

        public ResetLoseStatShopItemAction(GameProgressionStatsService stats)
        {
            _stats = stats;
        }
        
        public void Activate()
        {
            _stats.ResetLoseCount();
        }
    }
}