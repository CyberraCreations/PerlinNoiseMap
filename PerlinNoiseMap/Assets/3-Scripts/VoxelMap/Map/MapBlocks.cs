
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
            set { filler = value; }
        }

        [SerializeField]
        private Block top;
        public Block Top
        {
            get { return top; }
            set { top = value; }
        }
        #endregion
    }
}
