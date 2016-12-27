
using UnityEditor;
using UnityEngine;

namespace VoxelMap
{
    [CustomEditor(typeof(Map))]
    [ExecuteInEditMode]
    public class MapEditor : Editor
    {
        #region Fields
        private Map map = null;
        private Vector2 mapSize = new Vector2();
        #endregion

        #region Methods
        private void OnEnable()
        {
            map = (Map)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (map == null)
            {
                return;
            }

            InitMap();
        }

        private void InitMap()
        {
            GUILayout.BeginHorizontal();
            {
                GUIContent content = new GUIContent();
                content.text = "Map size: ";
                mapSize = EditorGUILayout.Vector2Field(content, mapSize);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUIContent content = new GUIContent();
                content.text = "Init Map";
                if (GUILayout.Button(content, new GUILayoutOption[] { GUILayout.ExpandWidth(true) }))
                {
                    map.Init((int)mapSize.x, 2, (int)mapSize.y);
                }
            }
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}
