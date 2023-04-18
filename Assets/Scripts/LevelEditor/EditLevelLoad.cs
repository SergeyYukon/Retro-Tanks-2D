using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEdit
{
    public class EditLevelLoad 
    {
        public void LoadLevelTilemap(Tilemap obstaclesTilemap, Tilemap waterTilemap, Tilemap camouflageTilemap, Tilemap swampTilemap)
        {
            string json = File.ReadAllText(Application.dataPath + "/myLevel.json");
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            obstaclesTilemap.ClearAllTiles();
            waterTilemap.ClearAllTiles();
            camouflageTilemap.ClearAllTiles();
            swampTilemap.ClearAllTiles();

            LoadCurrentTilemap(obstaclesTilemap, levelData.ObstaclesTiles, levelData.ObstaclesPositions);
            LoadCurrentTilemap(waterTilemap, levelData.WaterTiles, levelData.WaterTilesPositions);
            LoadCurrentTilemap(camouflageTilemap, levelData.CamouflageTiles, levelData.CamouflagePositions);
            LoadCurrentTilemap(swampTilemap, levelData.SwampTiles, levelData.SwampPositions);
        }

        public LevelData LoadStats()
        {
            string json = File.ReadAllText(Application.dataPath + "/myLevel.json");
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            levelData.SpawnWave.Add(levelData.PinkEnemy);
            levelData.SpawnWave.Add(levelData.BlueEnemy);
            levelData.SpawnWave.Add(levelData.GreenEnemy);
            levelData.SpawnWave.Add(levelData.AttackPlayerEnemy);
            levelData.SpawnWave.Add(levelData.PatrolEnemy);

            return levelData;
        }

        private void LoadCurrentTilemap(Tilemap _allTilemap, List<TileBase> currentTileBase, List<Vector3Int> currentTilePositions)
        {
            for (int j = 0; j < currentTileBase.Count; j++)
            {
                _allTilemap.SetTile(currentTilePositions[j], currentTileBase[j]);
            }
        }
    }
}
