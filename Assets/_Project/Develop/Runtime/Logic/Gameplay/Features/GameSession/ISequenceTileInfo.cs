using _Project.Develop.Runtime.Utils.ReactiveManagement;

namespace _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession
{
    public interface ISequenceTileInfo
    {
        string Value { get; }
        IReadOnlyVariable<bool> IsCorrect { get; }
    }
}