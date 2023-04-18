using Components.Enemy;
using Infrastructure.Data;
using Infrastructure.Factory;
using UnityEngine;

namespace Components.Spawn
{
    public class SpawnPoint : MonoBehaviour
    {
        private const int playerLayer = 6;
        private GameObjectsFactory _factory;
        private bool isStopSpawn;

        public void Construct (GameObjectsFactory factory)
        {
            _factory = factory;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == playerLayer)
            {
                isStopSpawn = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == playerLayer)
            {
                isStopSpawn = false;
            }
        }

        public void CreateEnemy(EnemyType enemyType, GameData gameData)
        {
            _factory.CreateEnemy(transform, enemyType, gameData);
        }

        public bool IsStopSpawn => isStopSpawn;
    }
}
