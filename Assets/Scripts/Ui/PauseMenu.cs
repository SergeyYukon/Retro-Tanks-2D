using Infrastructure;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class PauseMenu : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Continue()
        {
            _gameStateMachine.Enter<PauseResumeState>();
            Destroy(gameObject);
        }

        public void Restart()
        {
            string scene = SceneManager.GetActiveScene().name;
            _gameStateMachine.EnterScene(scene);
            _gameStateMachine.Enter<PauseResumeState>();
        }

        public void ToMenu()
        {
            _gameStateMachine.EnterScene(SceneNames.Menu);
            _gameStateMachine.Enter<PauseResumeState>();
        }
    }
}
