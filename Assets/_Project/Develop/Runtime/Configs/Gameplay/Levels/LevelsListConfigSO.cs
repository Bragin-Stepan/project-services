using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence;
using _Project.Develop.Runtime.Utilities.GameMode;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/LevelsListConfig", fileName = "LevelsListConfig")]
    public class LevelsListConfigSO : ScriptableObject
    {
        [SerializeField] private LevelsConfig[] _list;
        
        public IReadOnlyList<LevelsConfig> Levels => _list;

        public LevelConfigSO[] GetBy(GameModeType gameModeType) 
            => _list.FirstOrDefault(configs => configs != null && configs.GameModeType == gameModeType)?.Levels;
    }
    
    [Serializable]
    public class LevelsConfig
    {
        [field: SerializeField] public GameModeType GameModeType { get; private set; }
        [field: SerializeField] public LevelConfigSO[] Levels { get; private set; }
    }
}