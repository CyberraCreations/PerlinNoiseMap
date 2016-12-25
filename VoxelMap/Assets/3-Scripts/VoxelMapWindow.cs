
using UnityEditor;
using UnityEngine;

public class VoxelMapWindow : EditorWindow
{
    #region Fields
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
    #endregion
}
