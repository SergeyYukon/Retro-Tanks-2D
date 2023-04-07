using System;
using Components.Enemy;
using UnityEngine;

namespace Components.Spawn
{
    [Serializable]
    public class SpawnWaveStage
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private int amount;

        public EnemyType EnemyType => enemyType;
        public int Amount => amount;
    }
}
