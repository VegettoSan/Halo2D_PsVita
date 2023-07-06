using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xp_Enemy : MonoBehaviour
{
    public Transform Enemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Enemy.localScale.x == -1f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, 0f);
        }
        if (Enemy.localScale.x == 1f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, 0f);
        }
    }
}
