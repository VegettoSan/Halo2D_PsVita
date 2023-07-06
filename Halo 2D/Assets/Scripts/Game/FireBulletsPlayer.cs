using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchControlsKit;
using TMPro;

public class FireBulletsPlayer : MonoBehaviour
{
    enum Gun { Rifle, Pistol, RiflePlasma, PistolPlasma, Needler }
    [SerializeField]
    Gun Guns;

    public int MaxBullets = 600;
    public int BulletCounter = 60, BulletTotal = 600, RestBullets = 60;
    public TMP_Text Counter;
    public bool Automatic;
    //public Rigidbody2D Bullet;
    public Transform Origin,OriginNormal,OriginJump;
    public float Speed;
    public float Cadencia = 5f;
    float NextBullet;

    public Sprite[] BulletsSprites;
    public Image ImageBulletCounter;

    public AudioClip FireClip;
    AudioSource Source;

    PlayerController controller;
    void Start()
    {
        Source = GetComponentInParent<AudioSource>();
        controller = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.grounded)
        {
            Origin = OriginNormal;
        }
        else
        {
            Origin = OriginJump;
        }

        BulletCounter = Mathf.Clamp(BulletCounter, 0, 60);
        BulletTotal = Mathf.Clamp(BulletTotal, 0, 600);
        Counter.text = BulletTotal.ToString();
        CounterBullets();

        if(BulletTotal > 0 && BulletCounter > 0)
        {
            if (Automatic)
            {
                if (Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Mouse0))
                {
                    ShotAutomatic();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Shot();
                }
            }
        }
        
    }
    void ShotAutomatic()
    {
        if (Time.time >= NextBullet)
        {
            BulletCounter--;
            Source.PlayOneShot(FireClip);
            NextBullet = Time.time + 1 / Cadencia;
            /*Rigidbody2D clone = Instantiate(Bullet, Origin.position, Origin.rotation) as Rigidbody2D;
            clone.velocity = Origin.TransformDirection(Vector2.right * Speed);
            Vector3 v = transform.localScale;
            if (v.x == 1f)
            {
                clone.transform.localScale = new Vector3(-1f, 1f, 0f);
            }*/
            SpawnBullet();
        }
        if (BulletCounter == 0)
        {
            if (BulletTotal > 0)
            {
                BulletCounter = 60;
            }
            else if (BulletTotal <= 0)
            {
                BulletCounter = 0;
                BulletTotal = 0;
            }
            BulletTotal -= RestBullets;
        }
    }
    void Shot()
    {
        Cadencia = 0f;
        NextBullet = 0f;
        BulletCounter--;
        Source.PlayOneShot(FireClip);
        /*Rigidbody2D clone = Instantiate(Bullet, Origin.position, Origin.rotation) as Rigidbody2D;
        clone.velocity = Origin.TransformDirection(Vector2.right * Speed);
        Vector3 v = transform.localScale;
        if (v.x == 1f)
        {
            clone.transform.localScale = new Vector3(-1f, 1f, 0f);
        }*/
        SpawnBullet();
        if (BulletCounter == 0)
        {
            if (BulletTotal > 0)
            {
                BulletCounter = 60;
            }
            BulletTotal -= RestBullets;
        }
    }

    void CounterBullets()
    {
        int i = BulletCounter;
        ImageBulletCounter.sprite = BulletsSprites[i];
    }

    public void ReloadBullets()
    {
        if(BulletTotal < MaxBullets)
        {
            if (Guns == Gun.Rifle)
            {
                BulletTotal += 60;
            }
        }
    }

    private void SpawnBullet()
    {
        GameObject spawnedObject = PoolingBulletsHuman.Instance.GetObjectFromPool();
        spawnedObject.transform.position = Origin.position;
        spawnedObject.transform.rotation = Origin.rotation;
        spawnedObject.SetActive(true);
        Rigidbody2D clone = spawnedObject.GetComponent<Rigidbody2D>();
        clone.velocity = Origin.TransformDirection(Vector2.right * Speed);
        Vector3 v = transform.localScale;
        if (v.x == 1f)
        {
            clone.transform.localScale = new Vector3(-1f, 1f, 0f);
        }
    }
}
