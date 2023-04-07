using Components;
using Components.Enemy;
using Components.Player;
using Components.Spawn;
using Components.Weapon;
using Infrastructure.Data;
using Infrastructure.Services;
using Items;
using Path;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameObjectsFactory 
    {
        private InputService _input;
        private ItemsService _items;
        private LootGeneratorService _lootGeneratorService;

        private GameObject player;

        public GameObjectsFactory(InputService input, ItemsService items, LootGeneratorService lootGeneratorService)
        {
            _input = input;
            _items = items;
            _lootGeneratorService = lootGeneratorService;
        }

        public GameObject CreatePlayer(Vector3 startPosition, GameData gameData)
        {
            PlayerItem item = _items.PlayerItem;
            player = Object.Instantiate(item.PlayerPrefab, startPosition, Quaternion.identity);
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            health.Construct(item.Health, gameData);
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            movement.Construct(_input, item.MovementSpeed, item.RotationSpeed, gameData);
            Shooter shooter = player.GetComponent<Shooter>();
            shooter.Construct(this, item.Damage, item.PushPowerShoot, item.CooldownShoot, item.LayerToAttack, 
                item.BulletPrefab, item.Immunity, gameData);

            return player;
        }

        public GameObject CreateEnemy(Transform parrentTransform, EnemyType enemyType, Transform baseTransform, GameData gameData)
        {
            EnemyItem item = _items.GetEnemyItem(enemyType);
            GameObject enemyGameObject = Object.Instantiate(item.EnemyPrefab, parrentTransform.position, Quaternion.identity, parrentTransform);
            EnemyHealth health = enemyGameObject.GetComponent<EnemyHealth>();
            health.Construct(item.Health, gameData, _lootGeneratorService);
            EnemyMovement movement = enemyGameObject.GetComponent<EnemyMovement>();
            movement.Construct(baseTransform, item.MovementSpeed, item.RotationSpeed);
            Shooter shooter = enemyGameObject.GetComponent<Shooter>();
            shooter.Construct(this, item.Damage, item.PushPowerShoot, item.CooldownShoot, item.LayerToAttack,
                item.BulletPrefab, item.Immunity, new GameData()); 
            return enemyGameObject;
        }

        public GameObject CreateSpawnPoint(Vector3 position, Transform baseTransform, Transform parrent)
        {
            GameObject spawnPoint = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.SpawnPointPath), position, Quaternion.identity, parrent);
            SpawnPoint point = spawnPoint.GetComponent<SpawnPoint>();
            point.Construct(this, baseTransform, player.transform);

            return spawnPoint;
        }

        public GameObject CreateBase(Vector3 position, GameData gameData)
        {
            GameObject baseObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.BasePath), position, Quaternion.identity);
            BaseHealth health = baseObject.GetComponent<BaseHealth>();
            health.Construct(gameData);
            return baseObject;
        }

        public void CreateSpawnController(SpawnerItem spawnerItem, Transform baseTransform, GameData gameData)
        {
            GameObject spawnControllerObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.SpawnControllerPath));
            SpawnController spawnController = spawnControllerObject.GetComponent<SpawnController>();
            spawnController.Construct(spawnerItem, this, baseTransform, gameData);
        }

        public GameObject CreateBullet(Vector3 position, float damage, LayerMask layerToAttack, GameObject bulletPrefab, LayerMask immunity)
        {
            GameObject bulletObject = Object.Instantiate(bulletPrefab, position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.Construct(damage, layerToAttack, immunity);
            return bulletObject;
        }
    }
}
