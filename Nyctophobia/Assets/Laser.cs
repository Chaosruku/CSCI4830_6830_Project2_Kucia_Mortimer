using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float length
    {
        get
        {
            return this.transform.localScale.z;
        }

        set
        {
            this.transform.localScale = new Vector3(1, 1, value);
        }
    }
}
