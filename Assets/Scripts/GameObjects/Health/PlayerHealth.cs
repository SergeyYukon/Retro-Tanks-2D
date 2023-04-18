using Infrastructure.Data;
using UnityEngine;

namespace Components
{
    public class PlayerHealth : Health
    {
        [SerializeField] private AudioSource bonusSound;
        [SerializeField] private AudioSource getDamageSound;
        private GameData _gameData;

        public void Construct(float maxHealth, GameData gameData)
        {
            _gameData = gameData;
            _gameData.MaxHealth = maxHealth + _gameData.UpgradeMaxHealth;
            _gameData.CurrentHealth = _gameData.MaxHealth = Mathf.Min(_gameData.MaxHealth, _gameData.MaxUpgradedHealth);
        }

        public override void GetDamage(float damage)
        {
            _gameData.CurrentHealth -= damage;
            getDamageSound.Play();

            if (_gameData.CurrentHealth <= 0)
            {
                _gameData.OnDefeat?.Invoke();
            }
        }

        public void GetHeal(float amount)
        {
            bonusSound.Play();
            _gameData.CurrentHealth += amount;
            _gameData.CurrentHealth = Mathf.Min(_gameData.CurrentHealth, _gameData.MaxHealth);
        }

        public void UpgradeMaxHealth(float amount)
        {
            bonusSound.Play();
            _gameData.UpgradeMaxHealth += amount;
            _gameData.MaxHealth += amount;
            _gameData.MaxHealth = Mathf.Min(_gameData.MaxHealth, _gameData.MaxUpgradedHealth);
        }
    }
}
