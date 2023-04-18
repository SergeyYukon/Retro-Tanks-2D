using Components;
using Components.Enemy;
using Components.Player;
using Components.Spawn;
using Components.Weapon;
using Infrastructure.Data;
using Infrastructure.Services;
using Items;
using LevelEdit;
using Path;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameObjectsFactory 
    {
        private readonly InputServicePlayer1 _inputPlayer1;
        private readonly InputServicePlayer2 _inputPlayer2;
        private readonly ItemsService _items;
        private readonly LootGeneratorService _lootGeneratorService;

        private Transform _baseTransform;
        private Transform _player1Transform;
        private Transform _player2Transform;

        public GameObjectsFactory(InputServicePlayer1 inputPlayer1, InputServicePlayer2 inputPlayer2, ItemsService items, LootGeneratorService lootGeneratorService)
        {
            _inputPlayer1 = inputPlayer1;
            _inputPlayer2 = inputPlayer2;
            _items = items;
            _lootGeneratorService = lootGeneratorService;
        }

        public GameObject CreatePlayer(Vector3 startPosition, GameData gameDataPlayer1, GameData gameDataPlayer2, int numberPlayer)
        {
            PlayerItem item = _items.PlayerItem;
            GameObject player = Object.Instantiate(item.PlayerPrefab, startPosition, Quaternion.identity);

            if (numberPlayer == 1)
            {
                PlayerHealth health = player.GetComponent<PlayerHealth>();
                health.Construct(item.Health, gameDataPlayer1);
                Shooter shooter = player.GetComponent<Shooter>();
                shooter.Construct(this, item.Damage, item.PushPowerShoot, item.CooldownShoot, item.LayerToAttack,
                    item.BulletPrefab, item.Immunity, gameDataPlayer1);
                _player1Transform = player.transform;
                PlayerMovement movement = player.GetComponent<PlayerMovement>();
                movement.Construct(_inputPlayer1, item.MovementSpeed, item.RotationSpeed, gameDataPlayer1);
                SpriteRenderer player1Color = player.GetComponentInChildren<SpriteRenderer>();
                player1Color.color = gameDataPlayer1.Player1Color;
            }
            else if(numberPlayer == 2)                  
            {
                PlayerHealth health = player.GetComponent<PlayerHealth>();
                health.Construct(item.Health, gameDataPlayer2);
                Shooter shooter = player.GetComponent<Shooter>();
                shooter.Construct(this, item.Damage, item.PushPowerShoot, item.CooldownShoot, item.LayerToAttack,
                    item.BulletPrefab, item.Immunity, gameDataPlayer2);
                _player2Transform = player.transform;
                PlayerMovement movement = player.GetComponent<PlayerMovement>();
                movement.Construct(_inputPlayer2, item.MovementSpeed, item.RotationSpeed, gameDataPlayer2);
                SpriteRenderer player2Color = player.GetComponentInChildren<SpriteRenderer>();
                player2Color.color = gameDataPlayer1.Player2Color;
            }

            return player;
        }

        public GameObject CreateEnemy(Transform parrentTransform, EnemyType enemyType, GameData gameData)
        {
            EnemyItem item = _items.GetEnemyItem(enemyType);
            GameObject enemyGameObject = Object.Instantiate(item.EnemyPrefab, parrentTransform.position, new Quaternion(0,0,180,0), parrentTransform);
            EnemyHealth health = enemyGameObject.GetComponent<EnemyHealth>();
            health.Construct(item.Health, gameData, _lootGeneratorService);

            if (enemyType != EnemyType.PatrolEnemy)
            {
                EnemyMovement movement = enemyGameObject.GetComponent<EnemyMovement>();
                movement.Construct(_baseTransform, _player1Transform, _player2Transform, item.MovementSpeed, item.RotationSpeed,
                    item.DistanceToAttackTarget, enemyType);
            }
            else
            {
                PatrolEnemyMovement movement = enemyGameObject.GetComponent<PatrolEnemyMovement>();
                movement.Construct(_player1Transform, _player2Transform, item.MovementSpeed, item.RotationSpeed,
                    item.DistanceToAttackTarget, item.PatrolDistance, item.DistanceToPlayerTarget);
            }

            Shooter shooter = enemyGameObject.GetComponent<Shooter>();
            shooter.Construct(this, item.Damage, item.PushPowerShoot, item.CooldownShoot, item.LayerToAttack,
                item.BulletPrefab, item.Immunity, new GameData()); 
            return enemyGameObject;
        }

        public GameObject CreateSpawnPoint(Vector3 position, Transform parrent)
        {
            GameObject spawnPoint = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.SpawnPointPath), position, Quaternion.identity, parrent);
            SpawnPoint point = spawnPoint.GetComponent<SpawnPoint>();
            point.Construct(this);

            return spawnPoint;
        }

        public GameObject CreateBase(Vector3 position, GameData gameData)
        {           
            GameObject baseObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.BasePath), position, Quaternion.identity);
            BaseHealth health = baseObject.GetComponent<BaseHealth>();
            health.Construct(gameData);
            _baseTransform = baseObject.transform;
            return baseObject;
        }

        public void CreateSpawnController(SpawnerItem spawnerItem, GameData gameData)
        {
            GameObject spawnControllerObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.SpawnControllerPath));
            SpawnController spawnController = spawnControllerObject.GetComponent<SpawnController>();
            spawnController.Construct(spawnerItem, this, gameData);
        }

        public void CreateSpawnController(float cooldown, List<SpawnWaveStage> spawnWave, List<Vector3> spawners, GameData gameData)
        {
            GameObject spawnControllerObject = Object.Instantiate(Resources.Load<GameObject>(PrefabsPath.SpawnControllerPath));
            SpawnController spawnController = spawnControllerObject.GetComponent<SpawnController>();
            spawnController.ConstructEditLevel(cooldown, spawnWave, spawners, this, gameData);
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
