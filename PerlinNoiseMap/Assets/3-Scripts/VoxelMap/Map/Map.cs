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
        private MapStats mapStats;
        public MapStats MapStats
        {
            get { return mapStats; }
            set { mapStats = value; }
        }

        [SerializeField]
        private Texture perlinNoiseTexture = null;

        [SerializeField]
        private int maxHeight = 1;

        [SerializeField]
        private bool smoothMap = false;
        
        private List<Block> currentBlocks = new List<Block>();
        public List<Block> CurrentBlocks
        {
            get { return currentBlocks; }
        }
        #endregion

        #region Methods
        public void Init()
        {
            mapStats.Init();
            
            currentBlocks.Clear();
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            // Populate heights of map.
            for (int x = 1; x < (int)mapStats.MapSize.x + 1; x++)
            {
                for (int z = 1; z < (int)mapStats.MapSize.y + 1; z++)
                {
                    mapStats.Add(x, z, Mathf.CeilToInt(maxHeight * GetHeightAt((int)mapStats.MapSize.x, (int)mapStats.MapSize.y, x, z)));
                }
            }

            // Is map smooth?
            if (smoothMap)
            {
                mapStats.SmoothMap();
            }

            // Create blocks.
            for (int x = 1; x < (int)mapStats.MapSize.x + 1; x++)
            {
                for (int z = 1; z < (int)mapStats.MapSize.y + 1; z++)
                {
                    mapStats.BuildBlocks(this, x, z);
                }
            }
        }

        private float GetHeightAt(int sizeX, int sizeZ, int x, int z)
        {
            int marginX = perlinNoiseTexture.width / sizeX;
            int marginZ = perlinNoiseTexture.width / sizeZ;
            return (perlinNoiseTexture as Texture2D).GetPixel(x * marginX, z * marginZ).grayscale;
        }

        //private int GetBlockHeight(int x, int z)
        //{
        //    int result = -1;
        //    for (int i = 0; i < currentBlocks.Count; i++)
        //    {
        //        if (currentBlocks[i].Position.x - x == 0 && currentBlocks[i].Position.z - z == 0)
        //        {
        //            result = currentBlocks[i].HeightValue;
        //            break;
        //        }
        //    }

        //    return result;
        //}
        #endregion
    }
}
