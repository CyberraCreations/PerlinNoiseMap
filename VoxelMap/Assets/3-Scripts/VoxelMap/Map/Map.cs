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
            set { mapBlocks = value; }
        }

        [SerializeField]
        private Texture perlinNoiseTexture = null;

        [SerializeField]
        private int maxHeight = 1;

        [SerializeField]
        private Vector2 mapSize = Vector2.one;
        #endregion

        #region Methods
        public void Init()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            for (int x = 1; x < (int)mapSize.x + 1; x++)
            {
                for (int z = 1; z < (int)mapSize.y + 1; z++)
                {
                    int height = Mathf.CeilToInt(maxHeight * GetHeightAt((int)mapSize.x, (int)mapSize.y, x, z));
                    for (int y = 1; y < height + 1; y++)
                    {
                        Block block = Instantiate(y != height ? mapBlocks.filler : mapBlocks.top);
                        block.transform.SetParent(transform);
                        block.Init(x, y, z, GetHeightAt((int)mapSize.x, (int)mapSize.y, x, z));
                    }
                }
            }
        }

        private float GetHeightAt(int x, int z, int sizeX, int sizeZ)
        {
            int marginX = perlinNoiseTexture.width / sizeX;
            int marginZ = perlinNoiseTexture.width / sizeZ;
            return (perlinNoiseTexture as Texture2D).GetPixel(x * marginX, z * marginZ).grayscale;
        }
        #endregion
    }
}
