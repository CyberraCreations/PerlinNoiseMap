using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int x = 0;
    private int y = 0;
    private int z = 0;

    public Vector3 Position
    {
        get
        {
            return new Vector3(x, y, z);
        }
        set
        {
            x = (int)value.x;
            y = (int)value.y;
            z = (int)value.z;
        }
    }
}
