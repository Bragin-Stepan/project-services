using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using _Project.Develop.Runtime.Utilities.GameMode;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameModeRunner _gameRunner;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _gameRunner = _container.Resolve<GameModeRunner>(); 

            yield break;
        }

        public override void Run()
        { }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _gameRunner.Run(GameModeType.Chars);
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
                _gameRunner.Run(GameModeType.Numbers);
        }
    }
}
