using System;
using System.Collections.Generic;
using UnityEngine;

namespace PerlinNoiseMap
{
    [Serializable]
    public class MapStats : ScriptableObject
    {
        [SerializeField]
        private Dictionary<Vector2, int> posHeight;
        public Dictionary<Vector2, int> PosHeight
        {
            get { return posHeight; }
        }

        [SerializeField]
        private Vector2 mapSize;
        public Vector2 MapSize
        {
            get { return mapSize; }
            set { mapSize = value; }
        }

        public void Init()
        {
            posHeight = new Dictionary<Vector2, int>();
        }

        public void Add(int x, int z, int height)
        {
            posHeight.Add(new Vector2(x, z), height);
        }

        public void SmoothMap()
        {
            for (int x = 1; x < (int)mapSize.x + 1; x++)
            {
                for (int z = 1; z < (int)mapSize.y + 1; z++)
                {
                    int currentHeight = 0;
                    posHeight.TryGetValue(new Vector2(x, z), out currentHeight);

                    int left = 0;
                    posHeight.TryGetValue(new Vector2(x, z - 1), out left);
                    if (left != 0)
                    {
                        int diff = currentHeight - left;
                        if (Mathf.Abs(diff) > 1)
                        {
                            left = Mathf.Sign(diff) >= 0 ? left + 1 : left - 1;
                            posHeight[new Vector2(x, z - 1)] = left;
                        }
                    }

                    int downLeft = 0;
                    posHeight.TryGetValue(new Vector2(x - 1, z - 1), out downLeft);
                    if (downLeft != 0)
                    {
                        int diff = currentHeight - downLeft;
                        if (Mathf.Abs(diff) > 1)
                        {
                            downLeft = Mathf.Sign(diff) >= 0 ? downLeft + 1 : downLeft - 1;
                            posHeight[new Vector2(x - 1, z - 1)] = downLeft;
                        }
                    }

                    int down = 0;
                    posHeight.TryGetValue(new Vector2(x - 1, z), out down);
                    if (down != 0)
                    {
                        int diff = currentHeight - down;
                        if (Mathf.Abs(diff) > 1)
                        {
                            down = Mathf.Sign(diff) >= 0 ? down + 1 : down - 1;
                            posHeight[new Vector2(x - 1, z)] = down;
                        }
                    }

                    int downRight = 0;
                    posHeight.TryGetValue(new Vector2(x - 1, z + 1), out downRight);
                    if (downRight != 0)
                    {
                        int diff = currentHeight - downRight;
                        if (Mathf.Abs(diff) > 1)
                        {
                            downRight = Mathf.Sign(diff) >= 0 ? downRight + 1 : downRight - 1;
                            posHeight[new Vector2(x - 1, z + 1)] = downRight;
                        }
                    }

                    int right = 0;
                    posHeight.TryGetValue(new Vector2(x, z + 1), out right);
                    if (right != 0)
                    {
                        int diff = currentHeight - right;
                        if (Mathf.Abs(diff) > 1)
                        {
                            right = Mathf.Sign(diff) >= 0 ? right + 1 : right - 1;
                            posHeight[new Vector2(x, z + 1)] = right;
                        }
                    }

                    int upRight = 0;
                    posHeight.TryGetValue(new Vector2(x + 1, z + 1), out upRight);
                    if (upRight != 0)
                    {
                        int diff = currentHeight - upRight;
                        if (Mathf.Abs(diff) > 1)
                        {
                            upRight = Mathf.Sign(diff) >= 0 ? upRight + 1 : upRight - 1;
                            posHeight[new Vector2(x + 1, z + 1)] = upRight;
                        }
                    }

                    int up = 0;
                    posHeight.TryGetValue(new Vector2(x + 1, z), out up);
                    if (up != 0)
                    {
                        int diff = currentHeight - up;
                        if (Mathf.Abs(diff) > 1)
                        {
                            up = Mathf.Sign(diff) >= 0 ? up + 1 : up - 1;
                            posHeight[new Vector2(x + 1, z)] = up;
                        }
                    }

                    int upLeft = 0;
                    posHeight.TryGetValue(new Vector2(x + 1, z - 1), out upLeft);
                    if (upLeft != 0)
                    {
                        int diff = currentHeight - upLeft;
                        if (Mathf.Abs(diff) > 1)
                        {
                            upLeft = Mathf.Sign(diff) >= 0 ? upLeft + 1 : upLeft - 1;
                            posHeight[new Vector2(x + 1, z - 1)] = upLeft;
                        }
                    }
                }
            }
        }

        public void BuildBlocks(Map map, int x, int z)
        {
            int height = -1;
            posHeight.TryGetValue(new Vector2(x, z), out height);
            for (int y = 1; y < height + 1; y++)
            {
                Block block = Instantiate(y != height ? map.MapBlocks.Filler : map.MapBlocks.Top);
                block.transform.SetParent(map.transform);
                block.Init(x, y, z/*, height*/);
                map.CurrentBlocks.Add(block);
            }
        }
    }
}
