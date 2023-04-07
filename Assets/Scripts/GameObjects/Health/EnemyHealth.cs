using Infrastructure.Data;
using Infrastructure.Services;

namespace Components
{
    public class EnemyHealth : Health
    {
        private float _currentHealth;
        private GameData _gameData;
        private LootGeneratorService _lootGeneratorService;

        public void Construct(float maxHealth, GameData gameData, LootGeneratorService lootGeneratorService)
        {
            _currentHealth = maxHealth;
            _gameData = gameData;
            _lootGeneratorService = lootGeneratorService;
        }

        public override void GetDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _gameData.CurrentGoalKill--;
                _lootGeneratorService.GetLoot(transform.position);
                Destroy(gameObject);
            }
        }
    }
}
