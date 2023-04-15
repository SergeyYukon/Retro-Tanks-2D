using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class GameData
    {
        [SerializeField] private bool _fullscreenSettings = false;
        [SerializeField] private int _qualitySettings = 5;
        [SerializeField] private int _resolutionSettings = 16;

        [SerializeField] private int _levelsEnd;
        [SerializeField] private float _upgradeMoveSpeed;
        [SerializeField] private float _upgradeCooldownShoot;

        private int _startGoalKill;
        private int _currentGoalKill;
        private float _maxHealth;
        private float _currentHealth;
        private Action _onChangedHealth;
        private Action _onChangedGoalKill;

        private Action _onDefeat;
        private Action _onWin;

        public bool FullscreenSettings
        {
            get => _fullscreenSettings;
            set => _fullscreenSettings = value;
        }

        public int QualitySettings
        {
            get => _qualitySettings;
            set => _qualitySettings = value;
        }

        public int ResolutionSettings
        {
            get => _resolutionSettings;
            set => _resolutionSettings = value;
        }

        public int LevelsEnd
        {
            get => _levelsEnd;
            set => _levelsEnd = value;
        }

        public float UpgradeMoveSpeed
        {
            get => _upgradeMoveSpeed;
            set => _upgradeMoveSpeed = value;
        }

        public float UpgradeCooldownShoot
        {
            get => _upgradeCooldownShoot;
            set => _upgradeCooldownShoot = value;
        }

        public int StartGoalKill
        {
            get => _startGoalKill;
            set => _startGoalKill = value;
        }

        public int CurrentGoalKill
        {
            get => _currentGoalKill;
            set
            {
                _currentGoalKill = value;
                _onChangedGoalKill?.Invoke();
                if(_currentGoalKill <= 0)
                {
                    _onWin?.Invoke();
                }
            }
        }

        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                _onChangedHealth?.Invoke();
            }
        }

        public Action OnChangedHealth
        {
            get => _onChangedHealth;
            set => _onChangedHealth = value;
        }

        public Action OnChangedGoalKill
        {
            get => _onChangedGoalKill;
            set => _onChangedGoalKill = value;
        }

        public Action OnDefeat
        {
            get => _onDefeat;
            set => _onDefeat = value;
        }

        public Action OnWin
        {
            get => _onWin;
            set => _onWin = value;
        }
    }
}
