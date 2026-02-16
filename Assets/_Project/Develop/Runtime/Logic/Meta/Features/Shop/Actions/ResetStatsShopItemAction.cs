namespace _Project.Develop.Runtime.Logic.Meta.Features.Shop
{
    public class ResetStatsShopItemAction : IBuyableAction
    {
        private readonly GameProgressionStatsService _stats;

        public ResetStatsShopItemAction(GameProgressionStatsService stats)
        {
            _stats = stats;
        }
        
        public void Activate()
        {
            _stats.Reset(ProgressStatTypes.Win);
            _stats.Reset(ProgressStatTypes.Lose);
        }
    }
}