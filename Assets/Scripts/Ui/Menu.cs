using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Ui
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private TextMeshProUGUI continueText;
        [SerializeField] private Color lockColor;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private Image[] levelsImage;
        [SerializeField] private Button[] levelsButton;
        private Resolution[] resolutions;
        private IGameStateMachine _gameStateMachine;
        private GameData _gameData;
        private SaveLoadService _saveLoadService;

        public void Construct(IGameStateMachine gameStateMachine, GameData gameData, SaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _gameData = gameData;
            _saveLoadService = saveLoadService;

            SetSettings();
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
        }

        public void NewGame()
        {
            _gameData.ResetPlayerData();
            CheckUnlock();
        }

        public void SelectScene(string sceneName)
        {
            _saveLoadService.SaveProgress(_gameData);
            _gameStateMachine.EnterScene(sceneName);
        }

        public void SetSettings()
        {
            GetResolutions();
            if (_gameData.CustomSettings)
            {
                CustomSettings();
            }
            else
            {
                DefaultSettings();
            }
        }

        public void Fullscreen(bool toggle)
        {
            Screen.fullScreen = toggle;
        }

        public void Quality(int quality)
        {
            QualitySettings.SetQualityLevel(quality);
        }

        public void Resolution(int resolution)
        {
            Screen.SetResolution(resolutions[resolution].width, resolutions[resolution].height, _gameData.FullscreenSettings);
        }

        public void SettingsChanged()
        {
            _gameData.CustomSettings = true;
            _gameData.FullscreenSettings = fullscreenToggle.isOn;
            _gameData.QualitySettings = qualityDropdown.value;
            _gameData.ResolutionSettings = resolutionDropdown.value;
            _saveLoadService.SaveProgress(_gameData);
        }

        public void Exit()
        {
            _saveLoadService.SaveProgress(_gameData);
            Application.Quit();
        }

        private void GetResolutions()
        {
            resolutions = Screen.resolutions;
            var resolutionsList = new List<string>();
            foreach (var item in resolutions)
            {
                resolutionsList.Add($"{item.width}x{item.height}");
            }
            resolutionDropdown.AddOptions(resolutionsList);
        }

        private void DefaultSettings()
        {
            fullscreenToggle.isOn = true;
            int maxQuality = qualityDropdown.options.Count - 1;
            qualityDropdown.value = maxQuality;
            int maxResolution = resolutionDropdown.options.Count - 1;
            resolutionDropdown.value = maxResolution;
            Screen.fullScreen = true;
            QualitySettings.SetQualityLevel(maxQuality);
            Screen.SetResolution(resolutions[maxResolution].width, resolutions[maxResolution].height, true);
        }

        private void CustomSettings()
        {
            fullscreenToggle.isOn = _gameData.FullscreenSettings;
            qualityDropdown.value = _gameData.QualitySettings;
            resolutionDropdown.value = _gameData.ResolutionSettings;
            Screen.fullScreen = _gameData.FullscreenSettings;
            QualitySettings.SetQualityLevel(_gameData.QualitySettings);
            Screen.SetResolution(resolutions[_gameData.ResolutionSettings].width, resolutions[_gameData.ResolutionSettings].height, _gameData.FullscreenSettings);
        }
    }
}
