using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Items;
using Items.Level;
using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class InitLevelState : IState
    {
        private IGameStateMachine _gameStateMachine;
        private UiFactory _uiFactory;
        private GameObjectsFactory _gameObjectsFactory;
        private ItemsService _items;
        private GameData _gameData;
        private SaveLoadService _saveLoadService;

        public InitLevelState(IGameStateMachine gameStateMachine, UiFactory uiFactory,
            GameObjectsFactory gameObjectsFactory, ItemsService itemService, GameData gameData, SaveLoadService saveLoadService)
        {         
            _gameStateMachine = gameStateMachine;        
            _uiFactory = uiFactory;
            _gameObjectsFactory = gameObjectsFactory;
            _items = itemService;
            _gameData = gameData;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            InitLevel();
        }

        private void InitLevel()
        {                             
            _gameData = _saveLoadService.LoadProgress();

            string sceneName = SceneManager.GetActiveScene().name;
            LevelItem levelItem = _items.GetLevelItem(sceneName);
            LevelType type = levelItem.LevelType;

            if (type == LevelType.MainMenu)
            {
                _uiFactory.CreateMenu(PrefabsPath.MenuPath, _gameStateMachine, _gameData, _saveLoadService);
            }

            if (type == LevelType.Level)
            {
                SpawnerItem spawnerItem = _items.GetSpawnWave(levelItem.SceneName);
                _gameData.CurrentGoalKill = _gameData.StartGoalKill = spawnerItem.GoalKill;
                GameObject baseObject = _gameObjectsFactory.CreateBase(levelItem.BasePosition, _gameData);
                _gameObjectsFactory.CreatePlayer(levelItem.StartPlayerPosition, _gameData);
                _gameObjectsFactory.CreateSpawnController(spawnerItem, baseObject.transform, _gameData);
                _uiFactory.CreateGameMenu(_gameData);
                _uiFactory.CreateHud(_gameData);
            }
        }
    }
}
