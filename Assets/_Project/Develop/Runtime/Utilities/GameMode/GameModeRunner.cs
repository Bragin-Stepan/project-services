using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.GameMode
{
     public class GameModeRunner
     {
         private readonly SceneSwitcherService _sceneSwitcher;
         private readonly ICoroutinesPerformer _coroutinesPerformer;
         private readonly LevelsListConfigSO _levelsListConfig;
         
         public GameModeRunner(
             ICoroutinesPerformer coroutinesPerformer,
             LevelsListConfigSO levelsListConfig,
             SceneSwitcherService sceneSwitcher)
         {
             _levelsListConfig = levelsListConfig;
             _sceneSwitcher = sceneSwitcher;
             _coroutinesPerformer = coroutinesPerformer;
         }

         public void Run(GameModeType gameModeType)
         {
             LevelConfigSO[] levelsConfig = _levelsListConfig.GetBy(gameModeType);
             LevelConfigSO levelConfig = levelsConfig[Random.Range(0, levelsConfig.Length)];
             
             _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(
                 Scenes.Gameplay,
                 new GameplayInputArgs(
                     gameModeType,
                     levelConfig.SequenceCount)));
         }
     }
}