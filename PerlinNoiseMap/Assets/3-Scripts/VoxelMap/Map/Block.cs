using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PerlinNoiseMap
{
    public class Block : MonoBehaviour
    {
        #region Fields
        private int posX = 0;
        private int posY = 0;
        private int posZ = 0;

        public Vector3 Position
        {
            get
            {
                return new Vector3(posX, posY, posZ);
            }
            set
            {
                posX = (int)value.x;
                posY = (int)value.y;
                posZ = (int)value.z;
            }
        }

        //[SerializeField]
        //private int heightValue = 0;
        //public int HeightValue
        //{
        //    get { return heightValue; }
        //}
        #endregion

        #region Methods
        public void Init(int posX, int posY, int posZ/*, int heightValue*/)
        {
            Position = new Vector3(posX, posY, posZ);
            transform.position = Position;
            transform.localScale = Vector3.one;
            //this.heightValue = heightValue;
        }
        #endregion
    }
}
