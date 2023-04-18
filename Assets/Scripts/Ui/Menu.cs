using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private TextMeshProUGUI continueText;
        [SerializeField] private Color lockColor;
        [SerializeField] private Image levelEditImage;
        [SerializeField] private Button levelEditButton;
        [SerializeField] private Image[] levelsImage;
        [SerializeField] private Button[] levelsButton;
        private IGameStateMachine _gameStateMachine;
        private GameData _gameData;
        private SaveLoadService _saveLoadService;

        public void Construct(IGameStateMachine gameStateMachine, GameData gameData, SaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _gameData = gameData;
            _saveLoadService = saveLoadService;

            CheckUnlock();
        }

        public void CheckUnlock()
        {
            for (int i = 0; i < levelsImage.Length; i++)
            {
                if (_gameData.LevelsEnd == 0)
                {
                    continueButton.interactable = false;
                    continueText.color = lockColor;
                }

                if (i > _gameData.LevelsEnd)
                {
                    levelsButton[i].interactable = false;
                    var tempColor = levelsImage[i].color;
                    tempColor.a = 0.5f;
                    levelsImage[i].color = tempColor;
                }
            }

            if (!_gameData.IsLevelEdit)
            {
                levelEditButton.interactable = false;
                var tempColor = levelEditImage.color;
                tempColor.a = 0.5f;
                levelEditImage.color = tempColor;
            }
        }

        public void NewGame()
        {
            _gameData.ResetPlayerData();
            CheckUnlock();
        }

        public void SelectScene(string sceneName)
        {
            _saveLoadService.SaveProgress(_gameData, SaveLoadKeys.GameDataPlayer1Key);
            _gameStateMachine.EnterScene(sceneName);
        }

        public void GameType(bool multiplayer)
        {
            _gameData.Multiplayer = multiplayer;
            _saveLoadService.SaveProgress(_gameData, SaveLoadKeys.GameDataPlayer1Key);
        } 

        public void Exit()
        {
            _saveLoadService.SaveProgress(_gameData, SaveLoadKeys.GameDataPlayer1Key);
            Application.Quit();
        }
    }
}
