
using System;
using UnityEngine;

namespace PerlinNoiseMap
{
    [Serializable]
    public class MapBlocks : ScriptableObject
    {
        #region Fields
        [SerializeField]
        private Block filler;
        public Block Filler
        {
            get { return filler; }
        }

        [SerializeField]
        private Block top;
        public Block Top
        {
            get { return top; }
        }
        #endregion
    }
}
