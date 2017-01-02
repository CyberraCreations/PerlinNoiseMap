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

        [SerializeField]
        private bool smoothMap = false;

        private List<Block> currentBlocks = new List<Block>(); 
        #endregion

        #region Methods
        public void Init()
        {
            currentBlocks.Clear();
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            for (int x = 1; x < (int)mapSize.x + 1; x++)
            {
                for (int z = 1; z < (int)mapSize.y + 1; z++)
                {
                    int height = Mathf.CeilToInt(maxHeight * GetHeightAt((int)mapSize.x, (int)mapSize.y, x, z));
                    if (smoothMap)
                    {
                        int down = GetBlockHeight(x - 1, z);
                        if (down != -1)
                        {
                            int diff = height - down;
                            if (Mathf.Abs(diff) > 1)
                            {
                                height = Mathf.Sign(diff) >= 0 ? down + 1 : down - 1;
                            }
                        }

                        int left = GetBlockHeight(x, z - 1);
                        if (left != -1)
                        {
                            int diff = height - left;
                            if (Mathf.Abs(diff) > 1)
                            {
                                height = Mathf.Sign(diff) >= 0 ? left + 1 : left - 1;
                            }
                        }

                        int downLeft = GetBlockHeight(x - 1, z - 1);
                        if (downLeft != -1)
                        {
                            int diff = height - downLeft;
                            if (Mathf.Abs(diff) > 1)
                            {
                                height = Mathf.Sign(diff) >= 0 ? downLeft + 1 : downLeft - 1;
                            }
                        }

                        int downRight = GetBlockHeight(x - 1, z + 1);
                        if (downRight != -1)
                        {
                            int diff = height - downRight;
                            if (Mathf.Abs(diff) > 1)
                            {
                                height = Mathf.Sign(diff) >= 0 ? downRight + 1 : downRight - 1;
                            }
                        }

                        for (int y = 1; y < height + 1; y++)
                        {
                            Block block = Instantiate(y != height ? mapBlocks.Filler : mapBlocks.Top);
                            block.transform.SetParent(transform);
                            block.Init(x, y, z, height);
                            currentBlocks.Add(block);
                        }
                    }
                    else
                    {
                        for (int y = 1; y < height + 1; y++)
                        {
                            Block block = Instantiate(y != height ? mapBlocks.Filler : mapBlocks.Top);
                            block.transform.SetParent(transform);
                            block.Init(x, y, z, height);
                            currentBlocks.Add(block);
                        }
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

        private int GetBlockHeight(int x, int z)
        {
            int result = -1;
            for (int i = 0; i < currentBlocks.Count; i++)
            {
                if (currentBlocks[i].Position.x - x == 0 && currentBlocks[i].Position.z - z == 0)
                {
                    result = currentBlocks[i].HeightValue;
                    break;
                }
            }

            return result;
        }
        #endregion
    }
}
