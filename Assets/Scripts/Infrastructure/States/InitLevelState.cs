using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Items;
using Items.Level;
using LevelEdit;
using Path;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class InitLevelState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly UiFactory _uiFactory;
        private readonly GameObjectsFactory _gameObjectsFactory;
        private readonly ItemsService _items;
        private readonly SaveLoadService _saveLoadService;
        private GameData _gameDataPlayer1;
        private GameData _gameDataPlayer2;

        public InitLevelState(IGameStateMachine gameStateMachine, UiFactory uiFactory,
            GameObjectsFactory gameObjectsFactory, ItemsService itemService, GameData gameData, SaveLoadService saveLoadService)
        {         
            _gameStateMachine = gameStateMachine;        
            _uiFactory = uiFactory;
            _gameObjectsFactory = gameObjectsFactory;
            _items = itemService;
            _gameDataPlayer1 = gameData;
            _gameDataPlayer2 = gameData;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            InitLevel();
        }

        private void InitLevel()
        {                             
            _gameDataPlayer1 = _saveLoadService.LoadProgress(SaveLoadKeys.GameDataPlayer1Key);
            _gameDataPlayer2 = _saveLoadService.LoadProgress(SaveLoadKeys.GameDataPlayer2Key);

            string sceneName = SceneManager.GetActiveScene().name;
            LevelItem levelItem = _items.GetLevelItem(sceneName);
            LevelType type = levelItem.LevelType;

            if (type == LevelType.MainMenu)
            {
                _uiFactory.CreateMenu(PrefabsPath.MenuPath, _gameStateMachine, _gameDataPlayer1, _saveLoadService);
            }

            if (type == LevelType.Level)
            {
                SpawnerItem spawnerItem = _items.GetSpawnWave(levelItem.SceneName);
                _gameDataPlayer1.CurrentGoalKill = _gameDataPlayer1.StartGoalKill = spawnerItem.GoalKill;
                GameObject baseObject = _gameObjectsFactory.CreateBase(levelItem.BasePosition, _gameDataPlayer1);

                _gameObjectsFactory.CreatePlayer(levelItem.StartPlayer1Position, _gameDataPlayer1, _gameDataPlayer2, numberPlayer: 1);

                if (_gameDataPlayer1.Multiplayer)
                {
                    _gameObjectsFactory.CreatePlayer(levelItem.StartPlayer2Position, _gameDataPlayer1, _gameDataPlayer2, numberPlayer: 2);
                }

                _gameObjectsFactory.CreateSpawnController(spawnerItem, _gameDataPlayer1);
                _uiFactory.CreateGameMenu(_gameDataPlayer1, _gameDataPlayer2);
                _uiFactory.CreateHud(_gameDataPlayer1, _gameDataPlayer2);
            }

            if(type == LevelType.EditLevel)
            {
                EditLevelLoad levelLoad = new EditLevelLoad();
                LevelData levelData = levelLoad.LoadStats();
                _gameDataPlayer1.CurrentGoalKill = _gameDataPlayer1.StartGoalKill = levelData.Goal;
                _gameObjectsFactory.CreateBase(levelData.BasePosition, _gameDataPlayer1);

                _gameObjectsFactory.CreatePlayer(levelData.Player1Position, _gameDataPlayer1, _gameDataPlayer2, numberPlayer: 1);

                if (_gameDataPlayer1.Multiplayer)
                {
                    _gameObjectsFactory.CreatePlayer(levelData.Player2Position, _gameDataPlayer1, _gameDataPlayer2, numberPlayer: 2);
                }

                _gameObjectsFactory.CreateSpawnController(levelData.Cooldown, levelData.SpawnWave, levelData.Spawners, _gameDataPlayer1);
                _uiFactory.CreateGameMenu(_gameDataPlayer1, _gameDataPlayer2);
                _uiFactory.CreateHud(_gameDataPlayer1, _gameDataPlayer2);
            }

            if(type == LevelType.LevelEditor) 
            {
                _uiFactory.CreateGameMenu(_gameDataPlayer1, _gameDataPlayer2);

                GameObject levelEditor = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.LevelEditorPath));
                LevelEditor edit = levelEditor.GetComponent<LevelEditor>();
                edit.Construct(_gameStateMachine, _gameDataPlayer1, _saveLoadService);
            }
        }
    }
}
