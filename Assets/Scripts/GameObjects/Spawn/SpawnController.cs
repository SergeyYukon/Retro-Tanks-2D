using System.Collections;
using System.Collections.Generic;
using Components.Enemy;
using Infrastructure.Data;
using Infrastructure.Factory;
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

        private float _cooldownEditLevel;
        private List<SpawnWaveStage> _spawnWaveEditLevel;

        private SpawnerItem _spawnerItem;
        private GameObjectsFactory _factory;
        private GameData _gameData;

        public void Construct(SpawnerItem spawnerItem, GameObjectsFactory factory, GameData gameData)
        {
            _spawnerItem = spawnerItem;
            _factory = factory;
            _spawnPoints = new List<SpawnPoint>();
            _gameData = gameData;
            
            CreateSpawnPoints(_spawnerItem.SpawnPositions);
        }

        public void ConstructEditLevel(float cooldown, List<SpawnWaveStage> spawnWave, List<Vector3> spawners, GameObjectsFactory factory, GameData gameData)
        {                     
            _factory = factory;
            _gameData = gameData;
            _spawnPoints = new List<SpawnPoint>();

            _cooldownEditLevel = cooldown;
            _spawnWaveEditLevel = spawnWave;

            CreateSpawnPoints(spawners);
        }

        private void CreateSpawnPoints(List<Vector3> positions)
        {
            foreach (var position in positions)
            {
                GameObject pointObject = _factory.CreateSpawnPoint(position, transform);
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
                if (_spawnerItem != null)
                {
                    InitStageWave(_numberStage, _spawnerItem.Waves[_numberStage].SpawnCoolDown,
                        _spawnerItem.Waves[_numberStage].TimeToNextStage, _spawnerItem.Waves[_numberStage].Stages);
                }
                else
                {
                    InitStageWave(_numberStage, _cooldownEditLevel, 0, _spawnWaveEditLevel);
                }
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
                if (_spawnerItem != null)
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
                else
                {
                    _isFinishWave = true;
                }
            }
        }

        private void InitStageWave(int waveIndex, float cooldown, float timeToNextStage, List<SpawnWaveStage> spawnWave)
        {
            _isInitStage = true;
            _currentSpawnStages = new List<SpawnWaveStage>(spawnWave);
            _cooldownSpawn = cooldown;
            _timeToNextStage = timeToNextStage;
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
