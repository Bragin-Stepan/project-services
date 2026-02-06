using _Project.Develop.Runtime.Utilities.GameMode;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameModeType GameModeType { get; }
        public int SequenceCount { get; }
        
        public GameplayInputArgs(
            GameModeType gameModeType,
            int sequenceCount)
        {
            GameModeType = gameModeType;
            SequenceCount = sequenceCount;
        }
    }
}
