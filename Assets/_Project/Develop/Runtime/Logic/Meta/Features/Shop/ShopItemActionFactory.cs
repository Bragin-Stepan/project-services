using System;
using Assets._Project.Develop.Runtime.Infrastructure.DI;

namespace _Project.Develop.Runtime.Logic.Meta.Features.Shop
{
    public class ShopItemActionFactory
    {
        private readonly GameProgressionStatsService _progressionStats;

        public ShopItemActionFactory(DIContainer container)
        {
            _progressionStats = container.Resolve<GameProgressionStatsService>();
        }

        public IBuyableAction Create(ItemShopNames name)
        {
            return name switch
            {
                ItemShopNames.ResetGameStats => new ResetStatsShopItemAction(_progressionStats),
                ItemShopNames.ResetLoseStat => new ResetLoseStatShopItemAction(_progressionStats), // Просто как пример
                _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
            };
        }
    }
}