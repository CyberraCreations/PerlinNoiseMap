
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PerlinNoiseMap
{
    public class PerlinNoiseGenerator : EditorWindow
    {
        private Color[] pix = null;
        private Texture2D textureUsed = null;
        private float xOrg = 0;
        private float yOrg = 0;
        private float scale = 1f;

        [MenuItem("Window/PerlinNoiseGenerator")]
        static void Init()
        {
            PerlinNoiseGenerator mapCreator = GetWindow<PerlinNoiseGenerator>();
            GUIContent title = new GUIContent();
            title.text = "PerlinNoiseGenerator";
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

            GUILayout.Box("ENTER DESIRED VALUES", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(130) });
            InitVariables();

            if (textureUsed != null)
            {
                CreateNewTexture();
            }
        }

        private void InitVariables()
        {
            GUILayout.Space(5);

            EditorGUI.BeginChangeCheck();
            GUILayout.BeginArea(new Rect(10, 25, EditorGUIUtility.currentViewWidth - 25f, 20));
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("X Origin: ");
                    xOrg = EditorGUILayout.FloatField(xOrg);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(10, 45, EditorGUIUtility.currentViewWidth - 25f, 20));
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Y Origin: ");
                    yOrg = EditorGUILayout.FloatField(yOrg);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
            
            GUILayout.BeginArea(new Rect(10, 65, EditorGUIUtility.currentViewWidth - 25f, 20));
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Noise scale: ");
                    scale = EditorGUILayout.FloatField(scale);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(10, 85, EditorGUIUtility.currentViewWidth - 25f, 20));
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Texture used: ");
                    textureUsed = (Texture2D)EditorGUILayout.ObjectField(textureUsed, typeof(Texture2D), false);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
            EditorGUI.EndChangeCheck();
        }

        private void CreateNewTexture()
        {
            EditorGUI.BeginChangeCheck();
            GUILayout.BeginHorizontal();
            {
                GUIContent content = new GUIContent();
                content.text = "Create New Perlin Noise Texture";
                if (GUILayout.Button(content))
                {
                    pix = new Color[textureUsed.width * textureUsed.height];

                    float y = 0.0F;
                    while (y < textureUsed.height)
                    {
                        float x = 0.0F;
                        while (x < textureUsed.width)
                        {
                            float xCoord = xOrg + x / textureUsed.width * scale;
                            float yCoord = yOrg + y / textureUsed.height * scale;
                            float sample = Mathf.PerlinNoise(xCoord, yCoord);
                            pix[(int)(y * textureUsed.width + x)] = new Color(sample, sample, sample);
                            x++;
                        }
                        y++;
                    }
                    textureUsed.SetPixels(pix);
                    textureUsed.Apply();
                }
            }
            GUILayout.EndHorizontal();
            EditorGUI.EndChangeCheck();
        }
    }
}
