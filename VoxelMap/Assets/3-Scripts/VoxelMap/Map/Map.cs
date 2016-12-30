using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PerlinNoiseMap
{
    public class Map : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private MapBlocks mapBlocks;
        public MapBlocks MapBlocks
        {
            get { return mapBlocks; }
            set { mapBlocks = value; }
        }

        [SerializeField]
        private Texture perlinNoiseTexture = null;
        public Texture PerlinNoiseTexture
        {
            get { return perlinNoiseTexture; }
            set { perlinNoiseTexture = value; }
        }

        private Vector3 size = new Vector3();
        #endregion

        #region Methods
        public void Init(int sizeX, int sizeY, int sizeZ)
        {
            size = new Vector3(sizeX, sizeY, sizeZ);

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    for (int z = 0; z < sizeZ; z++)
                    {
                        Block block = Instantiate(mapBlocks.filler);
                        block.transform.SetParent(transform);
                        block.Init(x, y, z);
                    }
                }
            }
        }
        #endregion
    }
}
