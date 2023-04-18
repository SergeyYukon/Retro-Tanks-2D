using Items.Level;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelItem))]
    public class LevelItemEditor : UnityEditor.Editor
    {
        private const string StartPlayer1PositionTag = "Player1Position";
        private const string StartPlayer2PositionTag = "Player2Position";
        private const string BasePositionTag = "BasePosition";

        private SerializedProperty _type;
        private SerializedProperty _sceneName;
        private SerializedProperty _startPlayer1Position;
        private SerializedProperty _startPlayer2Position;
        private SerializedProperty _basePosition;

        private void OnEnable()
        {
            _type = serializedObject.FindProperty("levelType");
            _sceneName = serializedObject.FindProperty("sceneName");
            _startPlayer1Position = serializedObject.FindProperty("startPlayer1Position");
            _startPlayer2Position = serializedObject.FindProperty("startPlayer2Position");
            _basePosition = serializedObject.FindProperty("basePosition");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            LevelItem levelItem = (LevelItem)target;

            EditorGUILayout.PropertyField(_type);
            EditorGUILayout.PropertyField(_sceneName);

            LevelType type = _type.GetEnumValue<LevelType>();

            if (type == LevelType.Level)
            {
                EditorGUILayout.PropertyField(_startPlayer1Position);
                EditorGUILayout.PropertyField(_startPlayer2Position);
                EditorGUILayout.PropertyField(_basePosition);

                if (GUILayout.Button("Init"))
                {
                    levelItem.StartPlayer1Position = GameObject.FindWithTag(StartPlayer1PositionTag).transform.position;
                    levelItem.StartPlayer2Position = GameObject.FindWithTag(StartPlayer2PositionTag).transform.position;
                    levelItem.BasePosition = GameObject.FindWithTag(BasePositionTag).transform.position;
                }
            }
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }
    }
}
