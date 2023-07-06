using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    public bool Fire;
    public Transform Origin;
    public float Speed;
    public GameObject Bullet;
    public float Cadencia = 5f;
    float NextBullet;

    public AudioClip FireClip;
    public AudioSource Source;
    void Start()
    {
        //Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Fire)
        {
            Shot();
        }
        
    }

    void Shot()
    {
        if (Time.time >= NextBullet)
        {
            Source.PlayOneShot(FireClip);
            NextBullet = Time.time + 1 / Cadencia;
            GameObject spawnedObject = PoolingPlasmaBullets.Instance.GetObjectFromPool(Bullet);
            spawnedObject.transform.position = Origin.position;
            //spawnedObject.transform.rotation = Origin.rotation;
            spawnedObject.SetActive(true);
            Rigidbody2D clone = spawnedObject.GetComponent<Rigidbody2D>();
            clone.velocity = Origin.TransformDirection(Vector2.right * Speed);
            Vector3 v = transform.localScale;
            if (v.x == 1f)
            {
                clone.transform.localScale = new Vector3(-1f, 1f, 0f);
            }
            else if (v.x == -1f)
            {
                clone.transform.localScale = new Vector3(1f, 1f, 0f);
            }
        }
    }
}
