
using System;
using UnityEditor;
using UnityEngine;

namespace VoxelMap
{
    public class VoxelMapWindow : EditorWindow
    {
        #region Fields
        private const string BLOCK_DEFINITION = "Assets/Resources/MapBlocks/Definition/Definition";
        private const string BLOCK_PREFAB = "Assets/Resources/MapBlocks/Prefab/Prefab";

        public MapBlocks blocks;

        private Map map = null;
        private string mapName = string.Empty;
        #endregion

        #region Methods
        private void OnEnable()
        {
            EditorApplication.update += RefreshOnPlay;
        }

        private void OnDisable()
        {
            EditorApplication.update -= RefreshOnPlay;
        }

        private void RefreshOnPlay()
        {
            if (EditorApplication.isPlaying)
            {
                Repaint();
            }
        }

        [MenuItem("Window/VoxelMap")]
        static void Init()
        {
            VoxelMapWindow mapCreator = GetWindow<VoxelMapWindow>();
            GUIContent title = new GUIContent();
            title.text = "VoxelMap";
            mapCreator.titleContent = title;
        }

        private void OnGUI()
        {
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            {
                GUIContent content = new GUIContent();
                content.text = "Map name: ";
                mapName = EditorGUILayout.TextField(content, mapName);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUIContent content = new GUIContent();
                content.text = "Create Map";
                if (GUILayout.Button(content, new GUILayoutOption[] { GUILayout.ExpandWidth(true) }))
                {
                    MapBlocks blockDefinition = CreateInstance<MapBlocks>();
                    AssetDatabase.CreateAsset(blockDefinition, BLOCK_DEFINITION + ".asset");
                    AssetDatabase.RenameAsset(BLOCK_DEFINITION + ".asset", mapName);

                    GameObject mapObject = new GameObject(mapName);
                    map = mapObject.AddComponent<Map>();
                    map.MapBlocks = blockDefinition;

                    GameObject prefab = PrefabUtility.CreatePrefab(BLOCK_PREFAB + ".prefab", mapObject);
                    Map mapPrefab = prefab.GetComponent<Map>();
                    mapPrefab.MapBlocks = blockDefinition;
                    AssetDatabase.RenameAsset(BLOCK_PREFAB + ".prefab", mapName);
                    DestroyImmediate(mapObject);

                    GameObject prefabObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    PrefabUtility.ReplacePrefab(prefabObject, PrefabUtility.GetPrefabParent(prefabObject), ReplacePrefabOptions.ConnectToPrefab);
                    DestroyImmediate(prefabObject);
                }
            }
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}
