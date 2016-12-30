
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PerlinNoiseMap
{
    public class PerlinNoiseGenerator : MonoBehaviour
    {
        [SerializeField]
        private float xOrg = 0;
        public float XOrg
        {
            get { return xOrg; }
        }

        [SerializeField]
        private float yOrg = 0;
        public float YOrg
        {
            get { return yOrg; }
        }

        [SerializeField]
        private float scale = 1f;
        public float Scale
        {
            get { return scale; }
        }

        [SerializeField]
        private Material materialUsed = null;
        public Material MaterialUsed
        {
            get { return materialUsed; }
        }

        [SerializeField]
        private Texture2D textureUsed = null;
        public Texture2D TextureUsed
        {
            get { return textureUsed; }
            set { textureUsed = value; }
        }
    }
}
