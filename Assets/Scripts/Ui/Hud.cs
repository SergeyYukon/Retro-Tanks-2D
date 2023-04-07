using Infrastructure.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Image healthFill;
        [SerializeField] private TextMeshProUGUI counter;
        private GameData _gameData;

        public void Construct(GameData gameData)
        {
            _gameData = gameData;
            _gameData.OnChangedHealth += UpdateHealth;
            _gameData.OnChangedGoalKill += UpdateGoalCounter;
        }

        private void Start()
        {
            UpdateHealth();
            UpdateGoalCounter();
        }

        private void UpdateHealth()
        {
            healthFill.fillAmount = _gameData.CurrentHealth / _gameData.MaxHealth;
        }

        private void UpdateGoalCounter()
        {
            counter.text = _gameData.CurrentGoalKill.ToString();
        }

        private void OnDestroy()
        {
            _gameData.OnChangedHealth -= UpdateHealth;
            _gameData.OnChangedGoalKill -= UpdateGoalCounter;
        }
    }
}
