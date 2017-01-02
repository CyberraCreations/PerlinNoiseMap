
using UnityEditor;
using UnityEngine;

namespace PerlinNoiseMap
{
    [CustomEditor(typeof(Map))]
    [ExecuteInEditMode]
    public class MapEditor : Editor
    {
        #region Fields
        private Map map = null;
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
                content.text = "Init Map";
                if (GUILayout.Button(content, new GUILayoutOption[] { GUILayout.ExpandWidth(true) }))
                {
                    map.Init();
                }
            }
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}
