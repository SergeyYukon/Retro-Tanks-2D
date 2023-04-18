using UnityEngine;

namespace Items.Level
{
    [CreateAssetMenu(fileName = "LevelItem", menuName = "Items/LevelItem")]
    public class LevelItem : ScriptableObject
    {
        [Header("Base")]
        [SerializeField] private LevelType levelType;
        [SerializeField] private string sceneName;
        [SerializeField] private Vector3 startPlayer1Position;
        [SerializeField] private Vector3 startPlayer2Position;
        [SerializeField] private Vector3 basePosition;

        public LevelType LevelType => levelType;
        public string SceneName => sceneName;

        public Vector3 StartPlayer1Position           
        {
            get => startPlayer1Position;
            set => startPlayer1Position = value;
        }

        public Vector3 StartPlayer2Position
        {
            get => startPlayer2Position;
            set => startPlayer2Position = value;
        }

        public Vector3 BasePosition
        {
            get => basePosition;
            set => basePosition = value;
        }
    }
}
