using Components.Spawn;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEdit
{
    public class LevelData
    {
        public List<TileBase> ObstaclesTiles = new List<TileBase>();
        public List<Vector3Int> ObstaclesPositions = new List<Vector3Int>();

        public List<TileBase> WaterTiles = new List<TileBase>();
        public List<Vector3Int> WaterTilesPositions = new List<Vector3Int>();

        public List<TileBase> CamouflageTiles = new List<TileBase>();
        public List<Vector3Int> CamouflagePositions = new List<Vector3Int>();

        public List<TileBase> SwampTiles = new List<TileBase>();
        public List<Vector3Int> SwampPositions = new List<Vector3Int>();

        public Vector3 Player1Position;
        public Vector3 Player2Position;
        public List<Vector3> Spawners;
        public Vector3 BasePosition;

        public float Cooldown;
        public int Goal;

        public SpawnWaveStage PinkEnemy;
        public SpawnWaveStage BlueEnemy;
        public SpawnWaveStage GreenEnemy;
        public SpawnWaveStage AttackPlayerEnemy;
        public SpawnWaveStage PatrolEnemy;

        public List<SpawnWaveStage> SpawnWave = new List<SpawnWaveStage>();
    }
}
