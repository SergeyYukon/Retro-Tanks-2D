using Components.Enemy;
using Components.Spawn;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.States;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace LevelEdit 
{
    public class LevelEditor : MonoBehaviour
    {
        [Header("Tilemaps")]
        [SerializeField] private Tilemap obstaclesTilemap;
        [SerializeField] private Tilemap waterTilemap;
        [SerializeField] private Tilemap camouflageTilemap;
        [SerializeField] private Tilemap swampTilemap;
        [Header("Tiles")]
        [SerializeField] private TileBase obstaclesile;
        [SerializeField] private TileBase waterTile;
        [SerializeField] private TileBase camouflageTile;
        [SerializeField] private TileBase swampTile;
        [Header("Tools")]
        [SerializeField] private GameObject _player1;
        [SerializeField] private GameObject _player2;
        [SerializeField] private GameObject _spawner;
        [SerializeField] private GameObject _base;
        [Header("Enemies")]
        [SerializeField] private Slider _pinkEnemySlider;
        [SerializeField] private Slider _blueEnemySlider;
        [SerializeField] private Slider _greenEnemySlider;
        [SerializeField] private Slider _attackPlayerEnemySlider;
        [SerializeField] private Slider _patrolEnemySlider;
        [SerializeField] private Slider _cooldownSpawnSlider;

        private float _pinkEnemyAmount;
        private float _blueEnemyAmount;
        private float _greenEnemyAmount;
        private float _attackPlayerEnemyAmount;
        private float _patrolEnemyAmount;
        private float _cooldownSpawn;

        private const float coeffAmountEnemies = 50;
        private const float coeffCooldown = 10;

        private enum Enemy { PinkEnemyAmount, BlueEnemyAmount, GreenEnemyAmount, AttackPlayerEnemyAmount, PatrolEnemyAmount }

        private enum ToolsType { Empty, Player1Position, Player2Position, Spawner, Base}
        private ToolsType toolsType;

        private Vector2 _player1Position;
        private GameObject _player1Instance;
        private Vector2 _player2Position;
        private GameObject _player2Instance;
        private List<Vector3> _spawners;
        private List<GameObject> _spawnersInstance;
        private Vector2 _basePosition;
        private GameObject _baseInstance;

        private Vector3 _clickCameraPosition;
        private Vector3Int _clickToCell;

        private IGameStateMachine _gameStateMachine;
        private GameData _gameData;
        private SaveLoadService _saveLoadService;

        private enum TileType { Obstacle, Water, Camouflage, Swamp }
        private Tilemap _currentTilemap;
        private TileBase _currentTileBase;

        public void Construct(IGameStateMachine gameStateMachine, GameData gameData, SaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _gameData = gameData;
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            _spawners = new List<Vector3>();
            _spawnersInstance = new List<GameObject>();
            _currentTilemap = obstaclesTilemap;
            _currentTileBase = obstaclesile;

            DefaultSettings();
        }

        private void Update()
        {
            _clickCameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _clickToCell = _currentTilemap.WorldToCell(_clickCameraPosition);

            if (Input.GetMouseButtonDown(0))
            {
                switch (toolsType)
                {
                    case ToolsType.Empty:

                        if (_currentTileBase != null)
                        {
                            _currentTilemap.SetTile(_clickToCell, _currentTileBase);
                        }
                        else
                        {
                            Clear();
                        }
                        break;

                    case ToolsType.Player1Position:
                        if(_player1Instance == null)
                        {
                            _player1Position = _clickCameraPosition;
                            _player1Instance = Instantiate(_player1, _player1Position, Quaternion.identity);
                        }
                        break;

                    case ToolsType.Player2Position:
                        if (_player2Instance == null)
                        {
                            _player2Position = _clickCameraPosition;
                            _player2Instance = Instantiate(_player2, _player2Position, Quaternion.identity);
                        }
                        break;

                    case ToolsType.Spawner:
                        GameObject current = Instantiate(_spawner, (Vector2)_clickCameraPosition, Quaternion.identity);
                        _spawnersInstance.Add(current);
                        _spawners.Add(current.transform.position);
                        break;

                    case ToolsType.Base:
                        if (_baseInstance == null)
                        {
                            _basePosition = _clickCameraPosition;
                            _baseInstance = Instantiate(_base, _basePosition, new Quaternion(0, 0, 180, 0));
                        }
                        break;
                }
            }
        }

        public void ChooseTile(string tileName)
        {
            toolsType = ToolsType.Empty;
            Enum.TryParse(tileName, out TileType tileType);
            if (tileType == TileType.Obstacle)
            {
                _currentTilemap = obstaclesTilemap;
                _currentTileBase = obstaclesile;
            }
            else if (tileType == TileType.Water)
            {
                _currentTilemap = waterTilemap;
                _currentTileBase = waterTile;
            }
            else if (tileType == TileType.Camouflage)
            {
                _currentTilemap = camouflageTilemap;
                _currentTileBase = camouflageTile;
            }
            else if (tileType == TileType.Swamp)
            {
                _currentTilemap = swampTilemap;
                _currentTileBase = swampTile;
            }
        }

        public void Eraser()
        {
            _currentTileBase = null;
        }

        private void Clear()
        {
            obstaclesTilemap.SetTile(_clickToCell, _currentTileBase);
            waterTilemap.SetTile(_clickToCell, _currentTileBase);
            camouflageTilemap.SetTile(_clickToCell, _currentTileBase);
            swampTilemap.SetTile(_clickToCell, _currentTileBase);
        }

        public void SetPlayer1Position()
        {
            toolsType = ToolsType.Player1Position;
        }

        public void SetPlayer2Position()
        {
            toolsType = ToolsType.Player2Position;
        }

        public void SetSpawner()
        {
            toolsType = ToolsType.Spawner;
        }

        public void SetBasePosition()
        {
            toolsType = ToolsType.Base;
        }

        public void ErasePositions()
        {
            Destroy(_player1Instance);
            _player1Position = Vector2.zero;
            Destroy(_player2Instance);
            _player2Position = Vector2.zero;
            foreach (var item in _spawnersInstance)
            {
                Destroy(item);
                _spawners.Clear();
            }
            Destroy(_baseInstance);
            _basePosition = Vector2.zero;
        }

        public void SetPinkEnemy(float value)
        {
            _pinkEnemyAmount = value * coeffAmountEnemies;
        }

        public void SetBlueEnemy(float value)
        {
            _blueEnemyAmount = value * coeffAmountEnemies;
        }

        public void SetGreenEnemy(float value)
        {
            _greenEnemyAmount = value * coeffAmountEnemies;
        }

        public void SetAttackingEnemy(float value)
        {
            _attackPlayerEnemyAmount = value * coeffAmountEnemies;
        }

        public void SetPatrolEnemy(float value)
        {
            _patrolEnemyAmount = value * coeffAmountEnemies;
        }

        public void SetCooldown(float value)
        {
            _cooldownSpawn = value * coeffCooldown;
        }

        public void SaveLevelAndExit()
        {
            LevelData levelData = new LevelData();

            SaveCurrentTilemap(obstaclesTilemap, ref levelData.ObstaclesTiles, ref levelData.ObstaclesPositions);
            SaveCurrentTilemap(waterTilemap, ref levelData.WaterTiles, ref levelData.WaterTilesPositions);
            SaveCurrentTilemap(camouflageTilemap, ref levelData.CamouflageTiles, ref levelData.CamouflagePositions);
            SaveCurrentTilemap(swampTilemap, ref levelData.SwampTiles, ref levelData.SwampPositions);

            levelData.Player1Position = _player1Position;
            levelData.Player2Position = _player2Position;
            if(_spawners.Count == 0)
            {
                _spawners.Add(new Vector2(5, 0));
            }
            levelData.Spawners = _spawners;
            levelData.BasePosition = _basePosition;

            SpawnWaveStage pinkEnemy = new SpawnWaveStage();
            pinkEnemy.EnemyType = EnemyType.PinkEnemy;
            pinkEnemy.Amount = (int)_pinkEnemyAmount;
            levelData.PinkEnemy = pinkEnemy;

            SpawnWaveStage blueEnemy = new SpawnWaveStage();
            blueEnemy.EnemyType = EnemyType.BlueEnemy;
            blueEnemy.Amount = (int)_blueEnemyAmount;
            levelData.BlueEnemy = blueEnemy;

            SpawnWaveStage greenEnemy = new SpawnWaveStage();
            greenEnemy.EnemyType = EnemyType.GreenEnemy;
            greenEnemy.Amount = (int)_greenEnemyAmount;
            levelData.GreenEnemy = greenEnemy;

            SpawnWaveStage attackingEnemy = new SpawnWaveStage();
            attackingEnemy.EnemyType = EnemyType.AttackPlayerEnemy;
            attackingEnemy.Amount = (int)_attackPlayerEnemyAmount;
            levelData.AttackPlayerEnemy = attackingEnemy;

            SpawnWaveStage patrolEnemy = new SpawnWaveStage();
            patrolEnemy.EnemyType = EnemyType.PatrolEnemy;
            patrolEnemy.Amount = (int)_patrolEnemyAmount;
            levelData.PatrolEnemy = patrolEnemy;

            levelData.Cooldown = _cooldownSpawn;
            levelData.Goal = pinkEnemy.Amount + blueEnemy.Amount + greenEnemy.Amount + attackingEnemy.Amount + patrolEnemy.Amount;

            string json = JsonUtility.ToJson(levelData, true);
            File.WriteAllText(Application.dataPath + "/myLevel.json", json);

            _gameData.IsLevelEdit = true;
            _saveLoadService.SaveProgress(_gameData, SaveLoadKeys.GameDataPlayer1Key);
            ToMenu();
        }

        private void DefaultSettings()
        {
            _basePosition = new Vector2(-5, -5);
            _player1Position = new Vector2(-5, 0);
            _player2Position = new Vector2(-5, 5);

            _pinkEnemyAmount = _pinkEnemySlider.value * coeffAmountEnemies;
            _blueEnemyAmount = _blueEnemySlider.value * coeffAmountEnemies;
            _greenEnemyAmount = _greenEnemySlider.value * coeffAmountEnemies;
            _attackPlayerEnemyAmount = _attackPlayerEnemySlider.value * coeffAmountEnemies;
            _patrolEnemyAmount = _patrolEnemySlider.value * coeffAmountEnemies;

            _cooldownSpawn = _cooldownSpawnSlider.value * coeffCooldown / 4;
        }

        private void SaveCurrentTilemap(Tilemap _allTilemap, ref List<TileBase> currentTileBase, ref List<Vector3Int> currentTilePositions)
        {
            _allTilemap.CompressBounds();
            BoundsInt bounds = _allTilemap.cellBounds;
            for (int x = bounds.min.x; x < bounds.max.x; x++)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    TileBase temp = _allTilemap.GetTile(new Vector3Int(x, y, 0));
                    if (temp != null)
                    {
                        currentTileBase.Add(temp);
                        currentTilePositions.Add(new Vector3Int(x, y, 0));
                    }
                }
            }
        }

        private void ToMenu()
        {
            _gameStateMachine.EnterScene(SceneNames.Menu);
        }
    }
} 


