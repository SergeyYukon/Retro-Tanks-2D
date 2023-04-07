using System;
using System.Collections;
using System.Collections.Generic;
using Components.Enemy;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components.Spawn
{
    public class SpawnController : MonoBehaviour
    {
        private List<SpawnPoint> _spawnPoints;
        private List<SpawnWaveStage> _currentSpawnStages;
        private List<EnemyType> _types;
        private List<int> _amounts;
        
        private int _allStageEnemies;
        private float _cooldownSpawn;
        private float _timeToNextStage;
        private float _currentCooldownTime;
        private bool _isInitStage;
        private bool _isWaitNextStage;
        private bool _isFinishWave;
        private int _numberStage;

        private SpawnerItem _spawnerItem;
        private GameObjectsFactory _factory;
        private Transform _baseTransform;
        private GameData _gameData;

        public void Construct(SpawnerItem spawnerItem, GameObjectsFactory factory, Transform baseTransform, GameData gameData)
        {
            _spawnerItem = spawnerItem;
            _factory = factory;
            _baseTransform = baseTransform;
            _spawnPoints = new List<SpawnPoint>();
            _gameData = gameData;
            
            CreateSpawnPoints();
        }

        private void CreateSpawnPoints()
        {
            List<Vector3> positions = _spawnerItem.SpawnPositions;
            foreach (var position in positions)
            {
                GameObject pointObject = _factory.CreateSpawnPoint(position, _baseTransform, transform);
                SpawnPoint point = pointObject.GetComponent<SpawnPoint>();
                _spawnPoints.Add(point);
            }
        }

        private void Update()
        {           
            if (!_isFinishWave && !_isWaitNextStage)
            {
                UpdateCooldown();
                Spawn();
            }               
        }

        private void Spawn()
        {
            if (!CooldownIsUp())
            {
                return;
            }

            _currentCooldownTime = _cooldownSpawn;

            if (!_isInitStage)
            {
                InitStageWave(_numberStage);
            }

            for (int i = 0; i < _spawnPoints.Count && _allStageEnemies > 0; i++)
            {
                int random = Random.Range(0, _types.Count);
                if (_amounts[random] > 0)
                {
                    if (!_spawnPoints[i].IsStopSpawn)
                    {
                        _amounts[random]--;
                        _spawnPoints[i].CreateEnemy(_types[random], _gameData);
                        _allStageEnemies--;
                    }
                }
                else
                {
                    _types.RemoveAt(random);
                    _amounts.RemoveAt(random);
                }
            }

            if (CheckStage())
            {
                if (_numberStage < _spawnerItem.Waves.Count - 1)
                {
                    StartCoroutine(WaitNextStage());
                }
                else
                {
                    _isFinishWave = true;
                }
            }
        }

        private void InitStageWave(int waveIndex)
        {
            _isInitStage = true;
            
            SpawnWave spawnWave = _spawnerItem.Waves[waveIndex];

            _currentSpawnStages = new List<SpawnWaveStage>(spawnWave.Stages);
            _cooldownSpawn = spawnWave.SpawnCoolDown;
            _timeToNextStage = spawnWave.TimeToNextStage;
            _currentCooldownTime = _cooldownSpawn;
            
            _types = new List<EnemyType>();
            _amounts = new List<int>();

            for (int i = 0; i < _currentSpawnStages.Count; i++)
            {
                _types.Add(_currentSpawnStages[i].EnemyType);
                _amounts.Add(_currentSpawnStages[i].Amount);
                _allStageEnemies += _amounts[i];
            }
        }

        private bool CheckStage()
        {
            return _allStageEnemies <= 0;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _currentCooldownTime -= Time.deltaTime;
            }
        }

        private bool CooldownIsUp()
        {
            return _currentCooldownTime <= 0f;
        }

        private IEnumerator WaitNextStage()
        {
            _isWaitNextStage = true;
            yield return new WaitForSeconds(_timeToNextStage);
            _numberStage++;
            _isWaitNextStage = false;
            _isInitStage = false;
        }
    }
}
