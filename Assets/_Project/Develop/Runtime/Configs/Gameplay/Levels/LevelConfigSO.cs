using System.Collections.Generic;
using _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence;
using _Project.Develop.Runtime.Utilities.GameMode;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfigSO : ScriptableObject
    {
        [field: SerializeField] public SequenceMode SequenceMode { get; private set; }
        [field: SerializeField] public int SequenceCount { get; private set; }
    }
}