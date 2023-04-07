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
        private Transform _basePosition;

        public void Construct (GameObjectsFactory factory, Transform basePosition, Transform playerTransform)
        {
            _factory = factory;
            _basePosition = basePosition;
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
            _factory.CreateEnemy(transform, enemyType, _basePosition, gameData);
        }

        public bool IsStopSpawn => isStopSpawn;
    }
}
