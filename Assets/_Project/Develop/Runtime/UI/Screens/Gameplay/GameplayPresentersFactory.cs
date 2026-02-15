using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;

namespace _Project.Develop.Runtime.UI.Screens.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(
                view, 
                _container.Resolve<GameplayPresentersFactory>(),
                _container.Resolve<ProjectPresentersFactory>());
        }
    }
}
