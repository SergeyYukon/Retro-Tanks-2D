using System;
using Infrastructure.Data;
using UnityEngine;

namespace Components
{
    public class BaseHealth : Health
    {
        [SerializeField] private float _maxHealth;
        private float _currentHealth;
        private Action<float, float> _onHealthChanged;
        private GameData _gameData;

        public void Construct(GameData gameData)
        {
            _gameData = gameData;
            _currentHealth = _maxHealth;
        }

        public override void GetDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _gameData.OnDefeat?.Invoke();
            }
            _onHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        public Action<float, float> OnHealthChanged
        {
            get => _onHealthChanged;
            set => _onHealthChanged = value;
        }
    }
}
