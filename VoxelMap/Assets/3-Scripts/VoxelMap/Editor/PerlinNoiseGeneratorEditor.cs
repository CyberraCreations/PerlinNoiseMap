
using UnityEngine;
using UnityEditor;
using System.IO;

namespace PerlinNoiseMap
{
    [CustomEditor(typeof(PerlinNoiseGenerator))]
    [ExecuteInEditMode]
    public class PerlinNoiseGeneratorEditor : Editor
    {
        private PerlinNoiseGenerator perlinNoiseGenerator = null;
        private Color[] pix = null;

        private void OnEnable()
        {
            perlinNoiseGenerator = (PerlinNoiseGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (perlinNoiseGenerator == null)
            {
                return;
            }

            CreateNewTexture();
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
                    pix = new Color[perlinNoiseGenerator.TextureUsed.width * perlinNoiseGenerator.TextureUsed.height];
                    
                    float y = 0.0F;
                    while (y < perlinNoiseGenerator.TextureUsed.height)
                    {
                        float x = 0.0F;
                        while (x < perlinNoiseGenerator.TextureUsed.width)
                        {
                            float xCoord = perlinNoiseGenerator.XOrg + x / perlinNoiseGenerator.TextureUsed.width * perlinNoiseGenerator.Scale;
                            float yCoord = perlinNoiseGenerator.YOrg + y / perlinNoiseGenerator.TextureUsed.height * perlinNoiseGenerator.Scale;
                            float sample = Mathf.PerlinNoise(xCoord, yCoord);
                            pix[(int)(y * perlinNoiseGenerator.TextureUsed.width + x)] = new Color(sample, sample, sample);
                            x++;
                        }
                        y++;
                    }
                    perlinNoiseGenerator.TextureUsed.SetPixels(pix);
                    perlinNoiseGenerator.TextureUsed.Apply();
                    perlinNoiseGenerator.MaterialUsed.mainTexture = perlinNoiseGenerator.TextureUsed;

                    //byte[] bytes = (perlinNoiseGenerator.MaterialUsed.mainTexture as Texture2D).EncodeToPNG();

                    // For testing purposes, also write to a file in the project folder
                    //File.WriteAllBytes(Application.dataPath + "/2-Arts/Textures/NoiseTexture.png", bytes);

                    //DestroyImmediate(noiseTex);
                }
            }
            GUILayout.EndHorizontal();
            EditorGUI.EndChangeCheck();
        }
    }
}
