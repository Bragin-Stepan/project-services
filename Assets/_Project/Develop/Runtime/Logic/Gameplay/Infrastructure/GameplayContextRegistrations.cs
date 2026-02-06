using _Project.Develop.Runtime.Logic.Gameplay.Features.States;
using _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence;
using Assets._Project.Develop.Runtime.Infrastructure.DI;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(CreateSequenceFactory);
            container.RegisterAsSingle(CreateGameplayStatesFactory);
            container.RegisterAsSingle(CreateGameplayStateMachine);
        }

        private static SequenceFactory CreateSequenceFactory(DIContainer c) => new(c);
        
        private static GameplayStatesFactory CreateGameplayStatesFactory(DIContainer c) => new(c);

        private static GameplayStateMachine CreateGameplayStateMachine(DIContainer c) => new();
    }
}
