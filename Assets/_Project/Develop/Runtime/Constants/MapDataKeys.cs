using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public static class MapDataKeys
    {
        public static readonly IReadOnlyDictionary<Type, string> Dictionary = new Dictionary<Type, string>()
        {
            // { typeof(GameData), "GameData" },
            { typeof(PlayerData), "PlayerData" },
            // { typeof(SettingsData), "SettingsData" }
        };
    }
}