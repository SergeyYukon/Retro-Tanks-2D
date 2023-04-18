using Infrastructure.Services;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Infrastructure.Data
{
    public class GameSettings : MonoBehaviour
    {
        [Header("Video")]
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [Header("Audio")]
        [SerializeField] private Slider sliderMusic;
        [SerializeField] private Slider sliderSound;
        [SerializeField] private AudioMixerGroup mixerMaster;

        private const string musicMixer = "Music";
        private const string soundsMixer = "Sounds";
        private const float muteVolume = -80;
        private const float normalVolume = 0;
        private const float minVolume = -20;

        private Resolution[] resolutions;
        private GameData _gameData;
        private SaveLoadService _saveLoadService;

        public void Construct(GameData gameData, SaveLoadService saveLoadService)
        {
            _gameData = gameData;
            _saveLoadService = saveLoadService;

            SetSettings();
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

        public void ChangeVolumeMusic(float volume)
        {
            mixerMaster.audioMixer.SetFloat(musicMixer, Mathf.Lerp(minVolume, normalVolume, volume));
            if (volume == 0)
            {
                mixerMaster.audioMixer.SetFloat(musicMixer, muteVolume);
            }
            _gameData.MusicVolume = volume;
        }

        public void ChangeVolumeSounds(float volume)
        {
            mixerMaster.audioMixer.SetFloat(soundsMixer, Mathf.Lerp(minVolume, normalVolume, volume));
            if (volume == 0)
            {
                mixerMaster.audioMixer.SetFloat(soundsMixer, muteVolume);
            }
            _gameData.SoundsVolume = volume;
        }

        public void SettingsChanged()
        {
            _gameData.CustomSettings = true;
            _gameData.FullscreenSettings = fullscreenToggle.isOn;
            _gameData.QualitySettings = qualityDropdown.value;
            _gameData.ResolutionSettings = resolutionDropdown.value;
            _saveLoadService.SaveProgress(_gameData, SaveLoadKeys.GameDataPlayer1Key);
        }

        private void GetResolutions()
        {
            resolutions = Screen.resolutions;
            var resolutionsList = new List<string>();
            foreach (var item in resolutions)
            {
                resolutionsList.Add($"{item.width}x{item.height}");
            }
            resolutionDropdown.ClearOptions();
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

            const float defaultVolume = 1;
            sliderMusic.value = defaultVolume;
            sliderSound.value = defaultVolume;
            ChangeVolumeMusic(defaultVolume);
            ChangeVolumeSounds(defaultVolume);
        }

        private void CustomSettings()
        {
            fullscreenToggle.isOn = _gameData.FullscreenSettings;
            qualityDropdown.value = _gameData.QualitySettings;
            resolutionDropdown.value = _gameData.ResolutionSettings;
            Screen.fullScreen = _gameData.FullscreenSettings;
            QualitySettings.SetQualityLevel(_gameData.QualitySettings);
            Screen.SetResolution(resolutions[_gameData.ResolutionSettings].width, resolutions[_gameData.ResolutionSettings].height, _gameData.FullscreenSettings);

            ChangeVolumeMusic(_gameData.MusicVolume);
            ChangeVolumeSounds(_gameData.SoundsVolume);
            sliderMusic.value = _gameData.MusicVolume;
            sliderSound.value = _gameData.SoundsVolume;
        }
    }
}
