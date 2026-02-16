using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Meta;
using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly ConfigsProviderService _configsProviderService;

        public PlayerDataProvider(
            ISaveLoadService saveLoadService, 
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configsProviderService = configsProviderService;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                WalletData = InitWalletData(),
                StatsData = InitStatsData()
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> data = new();

            StartWalletConfigSO config = _configsProviderService.GetConfig<StartWalletConfigSO>();

            foreach (CurrencyTypes type in Enum.GetValues(typeof(CurrencyTypes)))
                data[type] = config.GetValueFor(type);

            return data;
        }
        
        private Dictionary<ProgressStatTypes, int> InitStatsData()
        {
            Dictionary<ProgressStatTypes, int> data = new();
            
            foreach (ProgressStatTypes type in Enum.GetValues(typeof(CurrencyTypes)))
                data[type] = 0;

            return data;
        }
    }
}
