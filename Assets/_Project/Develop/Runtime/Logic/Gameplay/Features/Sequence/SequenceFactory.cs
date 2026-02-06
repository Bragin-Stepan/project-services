using System;
using _Project.Develop.Runtime.Configs.Gameplay.Sequences;
using _Project.Develop.Runtime.Utilities.GameMode;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence
{
    public class SequenceFactory
    {
        private readonly ConfigsProviderService _configsLoader;
        
        public SequenceFactory(DIContainer container)
        {
            _configsLoader = container.Resolve<ConfigsProviderService>();
        }
        
        public string[] Create(int sequenceCount, GameModeType gameModeType, SequenceMode sequenceMode = SequenceMode.Random)
        {
            return gameModeType switch
            {
                GameModeType.Numbers => ConvertToStringArray(Generate<NumbersSequenceConfigSO, int>(sequenceCount, sequenceMode)),
                GameModeType.Chars => ConvertToStringArray(Generate<CharsSequenceConfigSO, char>(sequenceCount, sequenceMode)),
                _ => throw new ArgumentException(nameof(gameModeType))
            };
        }
        
        private TValue[] Generate<TSequence, TValue>(int count, SequenceMode mode)
            where TSequence : SequenceConfigSO<TValue>
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            TSequence config = _configsLoader.GetConfig<TSequence>();
            
            if (config == null || config.Values == null || config.Values.Length == 0)
                throw new ArgumentException("Config or values are invalid");
        
            return mode switch
            {
                SequenceMode.Random => GenerateRandom(config.Values, count),
                _ => throw new ArgumentException(nameof(mode))
            };
        }
        
        private string[] ConvertToStringArray<TValue>(TValue[] values)
        {
            if (values == null || values.Length == 0)
                return Array.Empty<string>();
            
            string[] result = new string[values.Length];
            
            for (int i = 0; i < values.Length; i++)
                result[i] = ConvertValueToString(values[i]);
            
            return result;
        }
        
        private string ConvertValueToString<TValue>(TValue value)
        {
            if (value == null)
                return string.Empty;
            
            return value switch
            {
                int intValue => intValue.ToString(),
                string stringValue => stringValue.ToUpper(),
                _ => value.ToString().ToUpper()
            };
        }
        
        private TValue[] GenerateRandom<TValue>(TValue[] values, int count)
        {
            TValue[] result = new TValue[count];
            
            for (int i = 0; i < count; i++)
                result[i] = values[Random.Range(0, values.Length)];
            
            return result;
        }
    }
}