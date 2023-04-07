using UnityEngine;

namespace Items.Level
{
    [CreateAssetMenu(fileName = "LevelItem", menuName = "Items/LevelItem")]
    public class LevelItem : ScriptableObject
    {
        [Header("Base")]
        [SerializeField] private LevelType levelType;
        [SerializeField] private string sceneName;
        [SerializeField] private Vector3 startPlayerPosition;
        [SerializeField] private Vector3 basePosition;

        public LevelType LevelType => levelType;
        public string SceneName => sceneName;

        public Vector3 StartPlayerPosition           
        {
            get => startPlayerPosition;
            set => startPlayerPosition = value;
        }

        public Vector3 BasePosition
        {
            get => basePosition;
            set => basePosition = value;
        }
    }
}
