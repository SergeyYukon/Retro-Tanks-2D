using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Ui
{
    public class FadePanels : MonoBehaviour
    {
        [SerializeField] private float durationFade;

        [SerializeField] private CanvasGroup mainLayout;
        [SerializeField] private CanvasGroup settingsLayout;
        [SerializeField] private CanvasGroup gameTypeLayout;
        [SerializeField] private CanvasGroup videoSettingsLayout;
        [SerializeField] private CanvasGroup volumeLayout;
        [SerializeField] private CanvasGroup levelsLayout;
        [SerializeField] private CanvasGroup colorsLayout;

        private Tweener _fadeOnMain;
        private Tweener _fadeOffMain;
        private Tweener _fadeOnSettings;
        private Tweener _fadeOffSettings;
        private Tweener _fadeOnGameType;
        private Tweener _fadeOffGameType;
        private Tweener _fadeOnVideoSettings;
        private Tweener _fadeOffVideoSettings;
        private Tweener _fadeOnVolume;
        private Tweener _fadeOffVolume;
        private Tweener _fadeOnLevels;
        private Tweener _fadeOffLevels;
        private Tweener _fadeOffColors;
        private Tweener _fadeOnColors;

        public void ToSettings()
        {
            StartCoroutine(ToSettingsFade());
        }

        public void ToMenu()
        {
            StartCoroutine(ToMenuFade());
        }

        public void ToGameType()
        {
            StartCoroutine(ToGameTypeFade());
        }

        public void ToVideoSettings()
        {
            StartCoroutine(ToVideoSettingsFade());
        }

        public void ToVolumeSettings()
        {
            StartCoroutine(ToVolumeFade());
        }

        public void ToLevels()
        {
            StartCoroutine(ToLevelsFade());
        }

        public void ToColors()
        {
            StartCoroutine(ToColorsFade());
        }

        private IEnumerator ToSettingsFade()
        {
            _fadeOffMain = mainLayout.DOFade(0, durationFade);
            _fadeOffVideoSettings = videoSettingsLayout.DOFade(0, durationFade);
            _fadeOffVolume = volumeLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            mainLayout.blocksRaycasts = false;
            videoSettingsLayout.blocksRaycasts = false;
            volumeLayout.blocksRaycasts = false;
            settingsLayout.blocksRaycasts = true;

            _fadeOnSettings = settingsLayout.DOFade(1, durationFade);
        }

        private IEnumerator ToMenuFade()
        {
            _fadeOffSettings = settingsLayout.DOFade(0, durationFade);
            _fadeOffGameType = gameTypeLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            settingsLayout.blocksRaycasts = false;
            gameTypeLayout.blocksRaycasts = false;
            mainLayout.blocksRaycasts = true;

            _fadeOnMain = mainLayout.DOFade(1, durationFade);
        }

        private IEnumerator ToGameTypeFade()
        {
            _fadeOffMain = mainLayout.DOFade(0, durationFade);
            _fadeOffLevels = levelsLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);
         
            mainLayout.blocksRaycasts = false;
            levelsLayout.blocksRaycasts = false;
            gameTypeLayout.blocksRaycasts = true;

            _fadeOnGameType = gameTypeLayout.DOFade(1, durationFade);
        }

        private IEnumerator ToVideoSettingsFade()
        {
            _fadeOffSettings = settingsLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            settingsLayout.blocksRaycasts = false;
            videoSettingsLayout.blocksRaycasts = true;

            _fadeOnVideoSettings = videoSettingsLayout.DOFade(1, durationFade);
        }

        private IEnumerator ToVolumeFade()
        {
            _fadeOffSettings = settingsLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            settingsLayout.blocksRaycasts = false;
            volumeLayout.blocksRaycasts = true;

            _fadeOnVolume = volumeLayout.DOFade(1, durationFade);
        }

        private IEnumerator ToLevelsFade()
        {
            _fadeOffGameType = gameTypeLayout.DOFade(0, durationFade);
            _fadeOffColors = colorsLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            gameTypeLayout.blocksRaycasts = false;
            colorsLayout.blocksRaycasts = false;
            levelsLayout.blocksRaycasts = true;

            _fadeOnLevels = levelsLayout.DOFade(1, durationFade);
        }

        private IEnumerator ToColorsFade()
        {
            _fadeOffLevels = levelsLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            levelsLayout.blocksRaycasts = false;
            colorsLayout.blocksRaycasts = true;

            _fadeOnColors = colorsLayout.DOFade(1, durationFade);
        }

        private void OnDestroy()
        {
            _fadeOffMain.Kill();
            _fadeOnMain.Kill();
            _fadeOffSettings.Kill();
            _fadeOnSettings.Kill();
            _fadeOffGameType.Kill();
            _fadeOnGameType.Kill();
            _fadeOffVideoSettings.Kill();
            _fadeOnVideoSettings.Kill();
            _fadeOffVolume.Kill();
            _fadeOnVolume.Kill();
            _fadeOffLevels.Kill();
            _fadeOnLevels.Kill();
            _fadeOffColors.Kill();
            _fadeOnColors.Kill();
        }
    }
}
