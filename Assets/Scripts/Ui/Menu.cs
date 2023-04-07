using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Image continueImage;
        [SerializeField] private Button continueButton;
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

            if (_gameData.LevelsEnd == 0)
            {
                continueButton.interactable = false;
                var tempColor = continueImage.color;
                tempColor.a = 0.5f;
                continueImage.color = tempColor;
            }
        }

        public void CheckUnlockLevels()
        {
            for (int i = 0; i < levelsImage.Length; i++)
            {
                if (i > _gameData.LevelsEnd)
                {
                    levelsButton[i].interactable = false;
                    var tempColor = levelsImage[i].color;
                    tempColor.a = 0.5f;
                    levelsImage[i].color = tempColor;
                }
            }
        }

        public void NewGame()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            _gameData = _saveLoadService.LoadProgress();
            CheckUnlockLevels();
        }

        public void SelectScene(string sceneName)
        {
            _gameStateMachine.EnterScene(sceneName);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
