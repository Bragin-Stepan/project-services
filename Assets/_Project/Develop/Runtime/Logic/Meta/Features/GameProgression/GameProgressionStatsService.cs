using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Utils.ReactiveManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;

namespace _Project.Develop.Runtime.Logic.Meta.Features
{
    public class GameProgressionStatsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<ProgressStatTypes, ReactiveVariable<int>> _stats;

        public GameProgressionStatsService(
            Dictionary<ProgressStatTypes, ReactiveVariable<int>> stats, 
            PlayerDataProvider playerDataProvider)
        {
            _stats = new Dictionary<ProgressStatTypes, ReactiveVariable<int>>(stats);
            
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }
        
        public List<ProgressStatTypes> AvailableStats => _stats.Keys.ToList();
        
        public IReadOnlyVariable<int> GetStat(ProgressStatTypes type) => _stats[type];

        public void Reset(ProgressStatTypes type)
        {
            _stats[type].Value = 0;
        }

        public void Add(ProgressStatTypes type, int amount = 1)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
        
            _stats[type].Value += amount;
        }
        
        public void Remove(ProgressStatTypes type, int amount = 1)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
        
            _stats[type].Value -= amount;
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<ProgressStatTypes, int> currency in data.StatsData)
            {
                if (_stats.ContainsKey(currency.Key))
                    _stats[currency.Key].Value = currency.Value;
                else
                    _stats.Add(currency.Key, new ReactiveVariable<int>(currency.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<ProgressStatTypes, ReactiveVariable<int>> currency in _stats)
            {
                if (data.StatsData.ContainsKey(currency.Key))
                    data.StatsData[currency.Key] = currency.Value.Value;
                else
                    data.StatsData.Add(currency.Key, currency.Value.Value);
            }
        }
    }
}