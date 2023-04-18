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
        private GameData _gameDataPlayer1;
        private GameData _gameDataPlayer2;
        private UiFactory _uiFactory;
        private InputServicePlayer1 _input;
        private GameObject _instancePauseMenu;
        private IGameStateMachine _gameStateMachine;
        private SaveLoadService _saveLoadService;

        public void Construct(GameData gameDataPlayer1, GameData gameDataPlayer2, UiFactory uiFactory, InputServicePlayer1 input,
            IGameStateMachine gameStateMachine, SaveLoadService saveLoadService)
        {
            _gameDataPlayer1 = gameDataPlayer1;
            _gameDataPlayer2 = gameDataPlayer2;
            _uiFactory = uiFactory;
            _input = input;
            _gameStateMachine = gameStateMachine;
            _gameDataPlayer1.OnDefeat += DefeatWindow;
            _gameDataPlayer2.OnDefeat += DefeatWindow;
            _gameDataPlayer1.OnWin += WinWindow;
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
            if (scene > _gameDataPlayer1.LevelsEnd)
            {
                _gameDataPlayer1.LevelsEnd++;
            }
            _saveLoadService.SaveProgress(_gameDataPlayer1, SaveLoadKeys.GameDataPlayer1Key);
            _saveLoadService.SaveProgress(_gameDataPlayer2, SaveLoadKeys.GameDataPlayer2Key);
            _uiFactory.CreateWinMenu(_gameDataPlayer1);
            _gameStateMachine.Enter<PauseResumeState>();
        }

        private void OnDestroy()
        {
            _gameDataPlayer1.OnDefeat -= DefeatWindow;
            _gameDataPlayer2.OnDefeat -= DefeatWindow;
            _gameDataPlayer1.OnWin -= WinWindow;
        }
    }
}
