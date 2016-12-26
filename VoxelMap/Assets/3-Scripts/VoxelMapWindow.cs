
using System;
using UnityEditor;
using UnityEngine;

public class VoxelMapWindow : EditorWindow
{
    #region Fields
    public MapBlocks blocks;

    private MapBlocks mapBlocks = null;
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
        GUILayout.BeginArea(new Rect(5, 10, Screen.width - 15, 100));
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Map blocks: ");
                mapBlocks = (MapBlocks)EditorGUILayout.ObjectField("", mapBlocks, typeof(MapBlocks), false);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();
    }
    #endregion
}
