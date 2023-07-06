using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBulletsPlayer : MonoBehaviour
{
    public FireBulletsPlayer Rifle;
    //public GameObject W_Rifle;
    public Transform Warnings;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rifle"))
        {
            if(Rifle.BulletTotal < Rifle.MaxBullets)
            {
                //var R = Instantiate(W_Rifle, Warnings, false);

                GameObject spawnedObject = PoolingWarningRifle.Instance.GetObjectFromPool();
                spawnedObject.transform.position = Warnings.position;
                spawnedObject.transform.rotation = Warnings.rotation;
                spawnedObject.SetActive(true);

                //Destroy(other.transform.parent.gameObject);
                other.transform.parent.gameObject.SetActive(false);
                Rifle.ReloadBullets();
            }
        }
    }
}
