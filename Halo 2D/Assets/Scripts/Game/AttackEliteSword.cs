using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEliteSword : MonoBehaviour
{
    [SerializeField] EnemyController Controller;
    [SerializeField] Animator Anim;

    private void Start()
    {
        Anim.SetBool("Attack", false);
    }
    void Update()
    {
        if (Controller.Target != null)
        {
            if (Controller.ddistance <= Controller.DistanceStop)
            {
                Anim.SetBool("Attack", true);
            }
            else if (Controller.ddistance > Controller.DistanceStop)
            {
                Anim.SetBool("Attack", false);
            }
        }
    }
}
