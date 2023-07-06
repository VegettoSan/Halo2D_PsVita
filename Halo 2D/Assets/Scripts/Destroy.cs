using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public bool Timer;
    public float Time;

    private void Start()
    {
        if (Timer)
        {
            DestroyObjT();
        }
    }
    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
    public void DestroyObjT()
    {
        Destroy(this.gameObject, Time);
    }
}
