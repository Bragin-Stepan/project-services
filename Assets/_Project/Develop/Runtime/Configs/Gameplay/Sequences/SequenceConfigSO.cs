using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Sequences
{
    public abstract class SequenceConfigSO<T>  : ScriptableObject
    {
        [field: SerializeField] public T[] Values { get; private set; }
    }
}