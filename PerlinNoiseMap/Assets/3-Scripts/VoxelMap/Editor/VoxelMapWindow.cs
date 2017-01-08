
using System;
using UnityEditor;
using UnityEngine;

namespace PerlinNoiseMap
{
    public class VoxelMapWindow : EditorWindow
    {
        #region Fields
        private const string BLOCK_DEFINITION = "Assets/Resources/MapBlocks/Definition/Definition";
        private const string BLOCK_PREFAB = "Assets/Resources/MapBlocks/Prefab/Prefab";

        public MapBlocks blocks;

        private Map map = null;
        private Block filler = null;
        private Block top = null;
        private Texture perlinNoiseTexture = null;
        private Vector2 mapSize = Vector2.one;
        private int maxHeight = 1;
        private string mapName = string.Empty;
        #endregion

        #region Methods
        [MenuItem("Window/VoxelMap")]
        static void Init()
        {
            VoxelMapWindow mapCreator = GetWindow<VoxelMapWindow>();
            GUIContent title = new GUIContent();
            title.text = "VoxelMap";
            mapCreator.titleContent = title;
        }

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
                GUILayout.Label("Map size: ");
                mapSize = EditorGUILayout.Vector2Field("",  mapSize);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Max height: ");
                maxHeight = EditorGUILayout.IntField("", maxHeight);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Filling block: ");
                filler = (Block)EditorGUILayout.ObjectField(filler, typeof(Block), false);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Top block: ");
                top = (Block)EditorGUILayout.ObjectField(top, typeof(Block), false);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Perlin noise texture: ");
                perlinNoiseTexture = (Texture)EditorGUILayout.ObjectField(perlinNoiseTexture, typeof(Texture), false);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUIContent content = new GUIContent();
                content.text = "Create Map";
                if (GUILayout.Button(content, new GUILayoutOption[] { GUILayout.ExpandWidth(true) }))
                {
                    MapBlocks mapBlocks = CreateInstance<MapBlocks>();
                    AssetDatabase.CreateAsset(mapBlocks, BLOCK_DEFINITION + ".asset");
                    AssetDatabase.RenameAsset(BLOCK_DEFINITION + ".asset", mapName + "Blocks");

                    MapStats mapStats = CreateInstance<MapStats>();
                    AssetDatabase.CreateAsset(mapStats, BLOCK_DEFINITION + ".asset");
                    AssetDatabase.RenameAsset(BLOCK_DEFINITION + ".asset", mapName + "Stats");

                    mapStats.MapSize = mapSize;
                    mapBlocks.Top = top;
                    mapBlocks.Filler = filler;

                    GameObject mapObject = new GameObject(mapName);
                    map = mapObject.AddComponent<Map>();
                    map.MaxHeight = maxHeight;
                    map.PerlinNoiseTexture = perlinNoiseTexture;
                    map.MapBlocks = mapBlocks;
                    map.MapStats = mapStats;

                    GameObject prefab = PrefabUtility.CreatePrefab(BLOCK_PREFAB + ".prefab", mapObject);
                    Map mapPrefab = prefab.GetComponent<Map>();
                    mapPrefab.MapBlocks = mapBlocks;
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
