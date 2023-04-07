using Items.Level;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelItem))]
    public class LevelItemEditor : UnityEditor.Editor
    {
        private const string StartPositionTag = "PlayerPosition";
        private const string BasePositionTag = "BasePosition";

        private SerializedProperty _type;
        private SerializedProperty _sceneName;
        private SerializedProperty _startPlayerPosition;
        private SerializedProperty _basePosition;

        private void OnEnable()
        {
            _type = serializedObject.FindProperty("levelType");
            _sceneName = serializedObject.FindProperty("sceneName");
            _startPlayerPosition = serializedObject.FindProperty("startPlayerPosition");
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
                EditorGUILayout.PropertyField(_startPlayerPosition);
                EditorGUILayout.PropertyField(_basePosition);

                if (GUILayout.Button("Init"))
                {
                    levelItem.StartPlayerPosition = GameObject.FindWithTag(StartPositionTag).transform.position;
                    levelItem.BasePosition = GameObject.FindWithTag(BasePositionTag).transform.position;
                }
            }
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }
    }
}
