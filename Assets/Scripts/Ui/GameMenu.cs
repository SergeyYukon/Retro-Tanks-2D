using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class GameMenu : MonoBehaviour
    {
        private GameData _gameData;
        private UiFactory _uiFactory;
        private InputService _input;
        private GameObject _instancePauseMenu;
        private IGameStateMachine _gameStateMachine;
        private SaveLoadService _saveLoadService;

        public void Construct(GameData gameData, UiFactory uiFactory, InputService input,
            IGameStateMachine gameStateMachine, SaveLoadService saveLoadService)
        {
            _gameData = gameData;
            _uiFactory = uiFactory;
            _input = input;
            _gameStateMachine = gameStateMachine;
            _gameData.OnDefeat += DefeatWindow;
            _gameData.OnWin += WinWindow;
            _saveLoadService = saveLoadService;
        }

        private void Update()
        {
            if (_input.PauseButton && _instancePauseMenu == null)
            {
                _instancePauseMenu = _uiFactory.CreatePauseMenu();
                _gameStateMachine.Enter<PauseResumeState>();
            }
        }

        private void DefeatWindow()
        {
            _uiFactory.CreateDefeatMenu();
            _gameStateMachine.Enter<PauseResumeState>();
        }

        private void WinWindow()
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            if (scene > _gameData.LevelsEnd)
            {
                _gameData.LevelsEnd++;
            }
            _saveLoadService.SaveProgress(_gameData);
            _uiFactory.CreateWinMenu(_gameData);
            _gameStateMachine.Enter<PauseResumeState>();
        }

        private void OnDestroy()
        {
            _gameData.OnDefeat -= DefeatWindow;
            _gameData.OnWin -= WinWindow;
        }
    }
}
