using _Project.Develop.Runtime.Utils.ReactiveManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;

namespace _Project.Develop.Runtime.Logic.Meta.Features.GameProgressionStatsService
{
    public class GameProgressionStatsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private ReactiveVariable<int> _winCount = new (0);
        private ReactiveVariable<int> _loseCount = new (0);

        public GameProgressionStatsService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }
        
        public IReadOnlyVariable<int> WinCount => _winCount;
        
        public IReadOnlyVariable<int> LoseCount => _loseCount;

        public void IncrementWinCount() => _winCount.Value += 1;
        
        public void IncrementLoseCount() => _loseCount.Value += 1;
        
        public void ResetWinCount() => _winCount.Value = 0;
        
        public void ResetLoseCount() => _loseCount.Value = 0;

        public void ReadFrom(PlayerData data)
        {
            _winCount.Value = data.WinCount;
            _loseCount.Value = data.LoseCount;
        }

        public void WriteTo(PlayerData data)
        {
            data.WinCount = _winCount.Value;
            data.LoseCount = _loseCount.Value;
        }
    }
}