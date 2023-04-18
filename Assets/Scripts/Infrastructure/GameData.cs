using System;
using UnityEngine;

namespace Infrastructure.Data
{
    [Serializable]
    public class GameData
    {
        [SerializeField] private bool _customSettings;
        [SerializeField] private bool _fullscreenSettings;
        [SerializeField] private int _qualitySettings;
        [SerializeField] private int _resolutionSettings;
        [SerializeField] private float _musicVolume;
        [SerializeField] private float _soundsVolume;

        [SerializeField] private bool _customColor;
        [SerializeField] private Color _player1Color;
        [SerializeField] private Color _player2Color;

        [SerializeField] private int _levelsEnd;
        [SerializeField] private float _upgradeMoveSpeed;
        [SerializeField] private float _upgradeCooldownShoot;
        [SerializeField] private float _upgradeMaxHealth;
        [SerializeField] private float _upgradeDamage;

        [SerializeField] private bool _multiplayer;
        [SerializeField] private bool _isLevelEdit;

        private float _currentSpeed;
        private float _currentCooldown;
        private float _currentDamage;
        private Action _onChangedCurrentSpeed;
        private Action _onChangedCurrentCooldown;
        private Action _onChangedCurrentDamage;

        private const float _maxSpeed = 5;
        private const float _minCooldownShoot = 0.3f;
        private const float _maxUpgradedHealth = 800;
        private const float _maxUpgradeDamage = 200;

        private int _startGoalKill;
        private int _currentGoalKill;
        private float _maxHealth;
        private float _currentHealth;
        private Action _onChangedHealth;
        private Action _onChangedGoalKill;

        private Action _onDefeat;
        private Action _onWin;

        public void ResetPlayerData()
        {
            _levelsEnd = 0;
            _upgradeMoveSpeed = 0;
            _upgradeCooldownShoot = 0;
            _upgradeMaxHealth = 0;
            _upgradeDamage = 0;
        }

        public bool CustomSettings
        {
            get => _customSettings;
            set => _customSettings = value;
        }

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

        public float MusicVolume
        {
            get => _musicVolume;
            set => _musicVolume = value;
        }

        public float SoundsVolume
        {
            get => _soundsVolume;
            set => _soundsVolume = value;
        }

        public bool CustomColor
        {
            get => _customColor;
            set => _customColor = value;
        }

        public Color Player1Color
        {
            get => _player1Color;
            set => _player1Color = value;
        }

        public Color Player2Color
        {
            get => _player2Color;
            set => _player2Color = value;
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

        public float UpgradeMaxHealth
        {
            get => _upgradeMaxHealth;
            set => _upgradeMaxHealth = value;
        }

        public float UpgradeDamage
        {
            get => _upgradeDamage;
            set => _upgradeDamage = value;
        }

        public float MaxSpeed => _maxSpeed;
        public float MinCooldownShoot => _minCooldownShoot;
        public float MaxUpgradedHealth => _maxUpgradedHealth;
        public float MaxUpgradeDamage => _maxUpgradeDamage;

        public float CurrentSpeed
        {
            get => _currentSpeed;
            set
            {
                _currentSpeed = value;
                _onChangedCurrentSpeed?.Invoke();
            }
        }

        public float CurrentCooldown
        {
            get => _currentCooldown;
            set 
            { 
                _currentCooldown = value;
                _onChangedCurrentCooldown?.Invoke();
            }
        }

        public float CurrentDamage
        {
            get => _currentDamage;
            set
            {
                _currentDamage = value;
                _onChangedCurrentDamage?.Invoke();
            }
        }

        public Action OnChangedCurrentSpeed
        {
            get => _onChangedCurrentSpeed;
            set => _onChangedCurrentSpeed = value;
        }

        public Action OnChangedCurrentCooldown
        {
            get => _onChangedCurrentCooldown;
            set => _onChangedCurrentCooldown = value;
        }

        public Action OnChangedCurrentDamage
        {
            get => _onChangedCurrentDamage;
            set => _onChangedCurrentDamage = value;
        }

        public bool Multiplayer
        {
            get => _multiplayer;
            set => _multiplayer = value;
        }

        public bool IsLevelEdit
        {
            get => _isLevelEdit;
            set => _isLevelEdit = value;
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
            set
            {
                _maxHealth = value;
                _onChangedHealth?.Invoke();
            }
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
