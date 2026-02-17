using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Configs.Gameplay.Sequences;
using _Project.Develop.Runtime.Configs.Meta;
using _Project.Develop.Runtime.UI.Common;
using _Project.Develop.Runtime.UI.Features.LevelsMenuPopup;
using _Project.Develop.Runtime.UI.Screens.Gameplay;
using _Project.Develop.Runtime.UI.Screens.MainMenu;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public static class PathToResources
    {
        public static IReadOnlyDictionary<Type, string> ScriptableObject => _scriptableObject;
        public static IReadOnlyDictionary<Type, string> UIPaths => _uiPaths;
        
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
            
            public static class Screens
            {
                public const string MainMenu = "UI/Screens/MainMenu/MainMenuUIRoot";
                public const string Gameplay = "UI/Screens/Gameplay/GameplayUIRoot";
            }
        }
        
        private static readonly Dictionary<Type, string> _scriptableObject = new()
        {
            { typeof(NumbersSequenceConfigSO), "Configs/Gameplay/Sequences/SequenceConfigsNumber" },
            { typeof(CharsSequenceConfigSO), "Configs/Gameplay/Sequences/SequenceConfigsChars" },
            
            { typeof(LevelsListConfigSO), "Configs/Levels/LevelsListConfig" },
            
            { typeof(StartWalletConfigSO), "Configs/Meta/Wallet/StartWalletConfig" },
            { typeof(CurrencyIconsConfigSO), "Configs/Meta/Wallet/CurrencyIconsConfig" },
            
            { typeof(RewardsConfigSO), "Configs/Meta/Rewards/RewardsConfig" },
            { typeof(ItemsPriceConfigSO), "Configs/Meta/Shop/ItemsPriceConfig" },
            { typeof(ProgressStatIconsConfigSO), "Configs/Meta/Stats/ProgressStatIconsConfig" },
        };
        
        private static readonly Dictionary<Type, string> _uiPaths = new()
        {
            {typeof(MainMenuScreenView), "UI/Screens/MainMenu/MainMenuScreenView" },
            {typeof(GameplayScreenView), "UI/Screens/Gameplay/GameplayScreenView" },
            
            {typeof(LevelTileView), "UI/LevelsMenuPopup/LevelTile" },
            {typeof(LevelsMenuPopupView), "UI/LevelsMenuPopup/LevelsMenuPopup" },

            {typeof(IconTextView), "UI/Common/IconTextView" },
        };
    }
}