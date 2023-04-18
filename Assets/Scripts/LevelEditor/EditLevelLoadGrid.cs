using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEdit
{
    public class EditLevelLoadGrid : MonoBehaviour
    {
        [SerializeField] private Tilemap obstaclesTilemap;
        [SerializeField] private Tilemap waterTilemap;
        [SerializeField] private Tilemap camouflageTilemap;
        [SerializeField] private Tilemap swampTilemap;

        private EditLevelLoad _editLevelLoad;

        private void Awake()
        {
            _editLevelLoad = new EditLevelLoad();
            _editLevelLoad.LoadLevelTilemap(obstaclesTilemap, waterTilemap, camouflageTilemap, swampTilemap);
        }
    }
}
