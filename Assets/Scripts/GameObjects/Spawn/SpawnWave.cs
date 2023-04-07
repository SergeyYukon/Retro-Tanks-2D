using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Spawn
{
    [Serializable]
    public class SpawnWave 
    {
        [SerializeField] private float spawnCoolDown;
        [SerializeField] private float timeToNextStage;
        [SerializeField] private List<SpawnWaveStage> stages;

        public float SpawnCoolDown => spawnCoolDown;
        public float TimeToNextStage => timeToNextStage;
        public List<SpawnWaveStage> Stages => stages;
    }
}
