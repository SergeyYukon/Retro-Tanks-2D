using Infrastructure.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;
        [Header("Player 1 Stats")]
        [SerializeField] private Image healthFillPlayer1;
        [SerializeField] private TextMeshProUGUI maxHealth1;
        [SerializeField] private TextMeshProUGUI speed1;
        [SerializeField] private TextMeshProUGUI damage1;
        [SerializeField] private TextMeshProUGUI cooldown1;
        [Header("Player 2 Stats")]
        [SerializeField] private GameObject statsPlayer2;
        [SerializeField] private Image healthFillPlayer2;
        [SerializeField] private TextMeshProUGUI maxHealth2;
        [SerializeField] private TextMeshProUGUI speed2;
        [SerializeField] private TextMeshProUGUI damage2;
        [SerializeField] private TextMeshProUGUI cooldown2;

        private GameData _gameDataPlayer1;
        private GameData _gameDataPlayer2;

        public void Construct(GameData gameDataPlayer1, GameData gameDataPlayer2)
        {
            _gameDataPlayer1 = gameDataPlayer1;
            _gameDataPlayer2 = gameDataPlayer2;
            _gameDataPlayer1.OnChangedGoalKill += UpdateGoalCounter;

            _gameDataPlayer1.OnChangedHealth += UpdateStats;
            _gameDataPlayer1.OnChangedCurrentSpeed += UpdateStats;
            _gameDataPlayer1.OnChangedCurrentCooldown += UpdateStats;
            _gameDataPlayer1.OnChangedCurrentDamage += UpdateStats;

            if (gameDataPlayer1.Multiplayer)
            {
                _gameDataPlayer2.OnChangedHealth += UpdateStats;
                _gameDataPlayer2.OnChangedCurrentSpeed += UpdateStats;
                _gameDataPlayer2.OnChangedCurrentCooldown += UpdateStats;
                _gameDataPlayer2.OnChangedCurrentDamage += UpdateStats;
            }
            else
            {
                statsPlayer2.SetActive(false);
            }
        }

        private void Start()
        {
            UpdateStats();
            UpdateGoalCounter();
        }

        private void UpdateGoalCounter()
        {
            counter.text = _gameDataPlayer1.CurrentGoalKill.ToString();
        }

        private void UpdateStats()
        {
            healthFillPlayer1.fillAmount = _gameDataPlayer1.CurrentHealth / _gameDataPlayer1.MaxHealth;
            healthFillPlayer2.fillAmount = _gameDataPlayer2.CurrentHealth / _gameDataPlayer2.MaxHealth;

            maxHealth1.text = _gameDataPlayer1.MaxHealth.ToString("0");
            speed1.text = _gameDataPlayer1.CurrentSpeed.ToString("0.0");
            damage1.text = _gameDataPlayer1.CurrentDamage.ToString("0");
            cooldown1.text = _gameDataPlayer1.CurrentCooldown.ToString("0.00");

            maxHealth2.text = _gameDataPlayer2.MaxHealth.ToString("0");
            speed2.text = _gameDataPlayer2.CurrentSpeed.ToString("0.0");
            damage2.text = _gameDataPlayer2.CurrentDamage.ToString("0");
            cooldown2.text = _gameDataPlayer2.CurrentCooldown.ToString("0.00");
        }

        private void OnDestroy()
        {
            _gameDataPlayer1.OnChangedGoalKill -= UpdateGoalCounter;

            _gameDataPlayer1.OnChangedHealth -= UpdateStats;
            _gameDataPlayer1.OnChangedCurrentSpeed -= UpdateStats;
            _gameDataPlayer1.OnChangedCurrentCooldown -= UpdateStats;
            _gameDataPlayer1.OnChangedCurrentDamage -= UpdateStats;

            _gameDataPlayer2.OnChangedHealth -= UpdateStats;
            _gameDataPlayer2.OnChangedCurrentSpeed -= UpdateStats;
            _gameDataPlayer2.OnChangedCurrentCooldown -= UpdateStats;
            _gameDataPlayer2.OnChangedCurrentDamage -= UpdateStats;
        }
    }
}
