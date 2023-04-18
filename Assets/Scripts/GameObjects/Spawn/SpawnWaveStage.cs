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

        public EnemyType EnemyType
        {
            get => enemyType;
            set => enemyType = value;
        }

        public int Amount 
        { 
            get => amount;
            set => amount = value;
        }
    }
}
