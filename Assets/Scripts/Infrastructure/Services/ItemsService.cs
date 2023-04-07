using Path;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Items.Level;
using Components.Enemy;
using Items;

namespace Infrastructure.Services
{
    public class ItemsService
    {
        private PlayerItem _playerItem;
        private Dictionary<string, LevelItem> _levels;
        private Dictionary<EnemyType, EnemyItem> _enemies;
        private Dictionary<string, SpawnerItem> _waves;


        public ItemsService()
        {
            LoadAllItems();
        }

        private void LoadAllItems()
        {
            LoadPlayerItem();
            LoadLevelItems();
            LoadEnemyItems();
            LoadSpawnWave();
        }

        private void LoadPlayerItem()
        {
            _playerItem = Resources.Load<PlayerItem>(ItemsPath.PlayerItemPath);
        }

        private void LoadLevelItems()
        {
            _levels = Resources.LoadAll<LevelItem>(ItemsPath.LevelItemsPath).ToDictionary(
                k => k.SceneName, v => v);
        }

        private void LoadEnemyItems()
        {
            _enemies = Resources.LoadAll<EnemyItem>(ItemsPath.EnemyItemsPath).ToDictionary(
                k => k.Type, v => v);
        }

        private void LoadSpawnWave()
        {
            _waves = Resources.LoadAll<SpawnerItem>(ItemsPath.SpawnerItemsPath).ToDictionary(
                k => k.SceneName, v => v);
        }

        public PlayerItem PlayerItem => _playerItem;

        public LevelItem GetLevelItem(string sceneName)
        {
            return _levels.TryGetValue(sceneName, out var item) ? item : null;
        }

        public EnemyItem GetEnemyItem(EnemyType type)
        {
            return _enemies.TryGetValue(type, out var item) ? item : null;
        }

        public SpawnerItem GetSpawnWave(string sceneName)
        {
            return _waves.TryGetValue(sceneName, out var item) ? item : null;
        }
    }
}
