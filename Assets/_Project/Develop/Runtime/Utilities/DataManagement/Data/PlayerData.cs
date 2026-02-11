using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Meta.Features.Wallet;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
    }
}
