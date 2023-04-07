using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.States;
using Path;
using Ui;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class UiFactory 
    {
        private InputService _input;
        private IGameStateMachine _gameStateMachine;
        private SaveLoadService _saveLoadService;

        public UiFactory(InputService input, IGameStateMachine gameStateMachine, SaveLoadService saveLoadService)
        {
            _input = input;
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }

        public GameObject CreateMenu(string prefabPath, IGameStateMachine gameStateMachine, GameData gameData, SaveLoadService saveLoadService)
        {
            GameObject ui = Object.Instantiate(Resources.Load<GameObject>(prefabPath));
            Menu menu = ui.GetComponent<Menu>();
            menu.Construct(gameStateMachine, gameData, saveLoadService);
            return ui;
        }

        public GameObject CreateHud(GameData gameData)
        {
            GameObject hudObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.HudPath));
            Hud hud = hudObject.GetComponent<Hud>();
            hud.Construct(gameData);
            return hudObject;
        }

        public GameObject CreateGameMenu(GameData gameData)
        {
            GameObject gameMenuObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.GameMenuPath));
            GameMenu gameMenu = gameMenuObject.GetComponent<GameMenu>();
            gameMenu.Construct(gameData, this, _input, _gameStateMachine, _saveLoadService);
            return gameMenuObject;
        }

        public GameObject CreatePauseMenu()
        {
            GameObject pauseMenuObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.PauseMenuPath));
            PauseMenu pauseMenu = pauseMenuObject.GetComponent<PauseMenu>();
            pauseMenu.Construct(_gameStateMachine);
            return pauseMenuObject;
        }

        public GameObject CreateDefeatMenu()
        {
            GameObject defeatMenuObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.DefeatMenuPath));
            DefeatMenu defeat = defeatMenuObject.GetComponent<DefeatMenu>();
            defeat.Construct(_gameStateMachine);
            return defeatMenuObject;
        }

        public GameObject CreateWinMenu(GameData gameData)
        {
            GameObject winMenuObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.WinMenuPath));
            WinMenu win = winMenuObject.GetComponent<WinMenu>();
            win.Construct(_gameStateMachine, gameData);
            return winMenuObject;
        }
    }
}
