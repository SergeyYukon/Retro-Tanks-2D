using Infrastructure.States;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;

        public SceneLoader(ICoroutineRunner coroutineRunner, IGameStateMachine gameStateMachine, LoadingCurtain loadingCurtain)
        {
            _coroutineRunner = coroutineRunner;
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        public void Load(int indexScene, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(indexScene, onLoaded));
        }

        public void Enter(string sceneName)
        {
             Load(sceneName, OnSceneLoaded);
        }

        public void Enter(int indexScene)
        {
            Load(indexScene, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            _gameStateMachine.Enter<InitLevelState>();
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            _loadingCurtain.Show();
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            while (!waitNextScene.isDone)
            {
                yield return null;
            }
            _loadingCurtain.Hide();
            onLoaded?.Invoke();
        }

        private IEnumerator LoadScene(int indexScene, Action onLoaded = null)
        {
            _loadingCurtain.Show();
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(indexScene);
            while (!waitNextScene.isDone)
            {
                yield return null;
            }
            _loadingCurtain.Hide();
            onLoaded?.Invoke();
        }
    }
}
