using _Project.Develop.Runtime.Configs.Meta;
using _Project.Develop.Runtime.Logic.Gameplay.Features.GameSession;
using _Project.Develop.Runtime.Logic.Gameplay.Features.States;
using _Project.Develop.Runtime.Logic.Gameplay.Features.Sequence;
using _Project.Develop.Runtime.Logic.Meta.Features.Reward;
using _Project.Develop.Runtime.Utilities.InputManagement;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(CreateSequenceFactory);
            container.RegisterAsSingle(CreateGameplayStatesFactory);
            container.RegisterAsSingle(CreateGameplayStateMachine);
            container.RegisterAsSingle(CreateGameSession);
            container.RegisterAsSingle(CreateRewardService);
        }

        private static SequenceFactory CreateSequenceFactory(DIContainer c) => new(c);
        
        private static GameplayStatesFactory CreateGameplayStatesFactory(DIContainer c) => new(c);

        private static GameplayStateMachine CreateGameplayStateMachine(DIContainer c) => new();
        
        private static RewardService CreateRewardService(DIContainer c)
            => new(c.Resolve<ConfigsProviderService>().GetConfig<RewardsConfigSO>());

        private static GameSessionService CreateGameSession(DIContainer c)
        {
            SequenceFactory sequenceFactory = c.Resolve<SequenceFactory>();
            IPlayerInputService playerInput = c.Resolve<IPlayerInputService>();

            return new GameSessionService(sequenceFactory, playerInput);
        }
    }
}
