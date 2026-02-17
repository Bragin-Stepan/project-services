using _Project.Develop.Runtime.Utils.ReactiveManagement;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession
{
    public class SequenceTileInfo : ISequenceTileInfo
    {
        public string Value { get; }
        
        private ReactiveVariable<bool> _isCorrect = new();
            
        public SequenceTileInfo(string value)
        {
            Value = value;
        }
        
        public IReadOnlyVariable<bool> IsCorrect => _isCorrect;
        
        public void SetCorrect(bool isCorrect) => _isCorrect.Value = isCorrect;
    }
}