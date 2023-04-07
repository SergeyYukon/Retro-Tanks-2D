using Infrastructure.Data;
using UnityEngine;

namespace Components
{
    public class PlayerHealth : Health
    {
        private GameData _gameData;

        public void Construct(float maxHealth, GameData gameData)
        {
            _gameData = gameData;
            _gameData.CurrentHealth = _gameData.MaxHealth = maxHealth;
        }

        public override void GetDamage(float damage)
        {
            _gameData.CurrentHealth -= damage;

            if (_gameData.CurrentHealth <= 0)
            {
                _gameData.OnDefeat?.Invoke();
            }
        }

        public void GetHeal(float amount)
        {
            _gameData.CurrentHealth += amount;
            _gameData.CurrentHealth = Mathf.Min(_gameData.CurrentHealth, _gameData.MaxHealth);
        }
    }
}
