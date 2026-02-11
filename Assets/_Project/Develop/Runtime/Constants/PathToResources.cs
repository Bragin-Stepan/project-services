using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Configs.Gameplay.Sequences;
using _Project.Develop.Runtime.Configs.Meta;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public static class PathToResources
    {
        public static IReadOnlyDictionary<Type, string> ScriptableObject => _scriptableObject;
        
        public static class Util
        {
            public const string Coroutine = "Utilities/CoroutinesPerformer";
        }
        
        public static class UI
        {
            public static class LoadingScreen
            {
                public const string Standard = "Utilities/StandardLoadingScreen";
            }
        }
        
        private static readonly Dictionary<Type, string> _scriptableObject = new()
        {
            { typeof(NumbersSequenceConfigSO), "Configs/Gameplay/Sequences/SequenceConfigsNumber" },
            { typeof(CharsSequenceConfigSO), "Configs/Gameplay/Sequences/SequenceConfigsChars" },
            
            { typeof(LevelsListConfigSO), "Configs/Levels/LevelsListConfig" },
            
            { typeof(StartWalletConfigSO), "Configs/Meta/Wallet/StartWalletConfig" },
            { typeof(CurrencyIconsConfigSO), "Configs/Meta/Wallet/CurrencyIconsConfig" },
        };
    }
}