using _Project.Develop.Runtime.Logic.Meta.Features.Shop;
using Assets._Project.Develop.Runtime.Infrastructure.DI;

namespace _Project.Develop.Runtime.UI.Screens.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(
                view,
                _container.Resolve<ShopService>(),
                _container.Resolve<ProjectPresentersFactory>(),
                _container.Resolve<MainMenuPopupService>());
        }
    }
}
