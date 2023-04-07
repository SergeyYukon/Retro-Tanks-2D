using System.Collections.Generic;
using Components.Spawn;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "SpawnerItem", menuName = "Items/SpawnerItem")]
    public class SpawnerItem : ScriptableObject
    {
        [SerializeField] private string sceneName;
        [SerializeField] private int goalKill;
        [SerializeField] private List<SpawnWave> waves;
        [SerializeField] private List<Vector3> spawnPositions;

        public int GoalKill => goalKill;
        public string SceneName => sceneName;
        public List<SpawnWave> Waves => waves;
        public List<Vector3> SpawnPositions => spawnPositions;
    }
}
