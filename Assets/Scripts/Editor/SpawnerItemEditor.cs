using Items;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SpawnerItem))]
    public class SpawnerItemEditor : UnityEditor.Editor
    {
        private const string EnemySpawnerTag = "SpawnPosition";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SpawnerItem spawnerItem = (SpawnerItem) target;
            if (GUILayout.Button("Init"))
            {
                var spawnPositions = spawnerItem.SpawnPositions;
                spawnPositions.Clear();
                
                var enemySpawnerMarkers = GameObject.FindGameObjectsWithTag(EnemySpawnerTag);
                foreach (var spawnerMarker in enemySpawnerMarkers)
                {
                    spawnPositions.Add(spawnerMarker.transform.position);
                }
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}