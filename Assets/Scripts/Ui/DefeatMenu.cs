using Infrastructure;
using Infrastructure.States;
using UnityEngine;

namespace Ui
{
    public class DefeatMenu : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void ToMenu()
        {
            _gameStateMachine.EnterScene(SceneNames.Menu);
            _gameStateMachine.Enter<PauseResumeState>();
        }
    }
}
