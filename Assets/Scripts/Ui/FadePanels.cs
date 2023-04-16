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
        [SerializeField] private CanvasGroup videoSettingsLayout;
        [SerializeField] private CanvasGroup levelsLayout;

        private Tweener _fadeOnMain;
        private Tweener _fadeOffMain;
        private Tweener _fadeOnSettings;
        private Tweener _fadeOffSettings;
        private Tweener _fadeOnVideoSettings;
        private Tweener _fadeOffVideoSettings;
        private Tweener _fadeOnLevels;
        private Tweener _fadeOffLevels;

        public void ToSettings()
        {
            StartCoroutine(ToSettingsFade());
        }

        public void ToMenu()
        {
            StartCoroutine(ToMenuFade());
        }

        public void ToVideoSettings()
        {
            StartCoroutine(ToVideoSettingsFade());
        }

        public void ToLevels()
        {
            StartCoroutine(ToLevelsFade());
        }

        private IEnumerator ToSettingsFade()
        {
            _fadeOffMain = mainLayout.DOFade(0, durationFade);
            _fadeOffVideoSettings = videoSettingsLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            mainLayout.blocksRaycasts = false;
            videoSettingsLayout.blocksRaycasts = false;
            settingsLayout.blocksRaycasts = true;

            _fadeOnSettings = settingsLayout.DOFade(1, durationFade);
        }

        private IEnumerator ToMenuFade()
        {
            _fadeOffSettings = settingsLayout.DOFade(0, durationFade);
            _fadeOffLevels = levelsLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            settingsLayout.blocksRaycasts = false;
            levelsLayout.blocksRaycasts = false;
            mainLayout.blocksRaycasts = true;

            _fadeOnMain = mainLayout.DOFade(1, durationFade);
        }

        private IEnumerator ToVideoSettingsFade()
        {
            _fadeOffSettings = settingsLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            settingsLayout.blocksRaycasts = false;
            videoSettingsLayout.blocksRaycasts = true;

            _fadeOnVideoSettings = videoSettingsLayout.DOFade(1, durationFade);
        }

        private IEnumerator ToLevelsFade()
        {
            _fadeOffMain = mainLayout.DOFade(0, durationFade);

            yield return new WaitForSeconds(durationFade);

            mainLayout.blocksRaycasts = false;
            levelsLayout.blocksRaycasts = true;

            _fadeOnLevels = levelsLayout.DOFade(1, durationFade);
        }

        private void OnDestroy()
        {
            _fadeOffMain.Kill();
            _fadeOnMain.Kill();
            _fadeOffSettings.Kill();
            _fadeOnSettings.Kill();
            _fadeOffVideoSettings.Kill();
            _fadeOnVideoSettings.Kill();
            _fadeOffLevels.Kill();
            _fadeOnLevels.Kill();
        }
    }
}
