using Infrastructure.States;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain loadingCurtain;
        private IGameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine(this, Instantiate(loadingCurtain));
            _gameStateMachine.EnterScene(SceneNames.Menu);

            DontDestroyOnLoad(this);
        }
    }
}
