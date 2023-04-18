using Infrastructure;
using Infrastructure.Data;
using Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textCounter;
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] private string endGameText;
        private IGameStateMachine _gameStateMachine;
        private GameData _gameData;

        public void Construct(IGameStateMachine gameStateMachine, GameData gameData)
        {
            _gameStateMachine = gameStateMachine;
            _gameData = gameData;
        }

        private void Start()
        {
            textCounter.text = _gameData.StartGoalKill.ToString();
        }

        public void ToMenu()
        {
            _gameStateMachine.EnterScene(SceneNames.Menu);
            _gameStateMachine.Enter<PauseResumeState>();
        }

        public void NextLevel()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            if (index == 10)
            {
                winText.text = endGameText;
            }
            else
            {
                index++;
                _gameStateMachine.EnterScene(index);
                _gameStateMachine.Enter<PauseResumeState>();
            }
        }
    }
}
