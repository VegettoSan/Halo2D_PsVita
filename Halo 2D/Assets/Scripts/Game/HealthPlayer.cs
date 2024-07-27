using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    //public GameObject W_HealthPack;
    public Transform Warnings;

    public GameObject ShieldHit, HealthHit, HealtPackHit;
    public Slider _Shield, _Health;
    public float velocidadCambio = 0.5f; // Velocidad de cambio (ajústala según tus necesidades)
    public int Health = 500, Shield = 300;
    public int Seconds = 3;
    public AudioClip HitShield, ReloadShield;
    public AudioSource Source_hud;
    public Color Red, Yellow, Blue;
    public Image HealthImg;
    public Transform TargetCamera;
    Animator Anim;
    AudioSource Source;
    PlayerController Controller;
    FireBulletsPlayer FireController;
    Rigidbody2D Rig;
    public CapsuleCollider2D Collider;
    public AudioClip Death;
    public GameObject GameOver, hud/*, Controls*/;

    float Timer;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Source = GetComponent<AudioSource>();
        Controller = GetComponent<PlayerController>();
        FireController = GetComponentInChildren<FireBulletsPlayer>();
        Rig = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float ValueShield = Mathf.Lerp(_Shield.value, Shield, velocidadCambio * Time.deltaTime);

        _Health.value = Health;
        _Shield.value = ValueShield;

        Shield = Mathf.Clamp(Shield, 0, 100);
        Health = Mathf.Clamp(Health, 0, 150);

        if (Shield < 100)
        {
            Timer += Time.deltaTime * 1f;

            if(Timer >= Seconds)
            {
                Source_hud.PlayOneShot(ReloadShield);
                Shield = 100;
                Timer = 0f;
            }
        }

        if (Health > 92)
        {
            HealthImg.color = Blue;
        }
        else if (Health < 92 && Health > 40)
        {
            HealthImg.color = Yellow;
        }
        if (Health < 40 && Health > 0)
        {
            HealthImg.color = Red;
        }

        if (Health <= 0)
        {
            Anim.SetBool("Death", true);
            Dead();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.O))
        {
            Damage(5);
        }
#endif
    }

    public void Damage(int total)
    {
        Source_hud.Stop();
        Shield -= total;
        Source.PlayOneShot(HitShield);
        Timer = 0f;

        if(Shield > 0)
        {
            //Instantiate(ShieldHit, transform.position, transform.rotation);
            GameObject spawnedObject = PoolingHitsHud.Instance.GetObjectFromPool(ShieldHit);
            spawnedObject.transform.position = Vector3.zero;
            spawnedObject.SetActive(true);
        }

        if (Shield <= 0)
        {
            Health -= total;
            //Instantiate(HealthHit, transform.position, transform.rotation);
            GameObject spawnedObject = PoolingHitsHud.Instance.GetObjectFromPool(HealthHit);
            spawnedObject.transform.position = Vector3.zero;
            spawnedObject.SetActive(true);
        }
    }

    public void Dead()
    {
        transform.gameObject.tag = "null";
        hud.SetActive(false);
        GameOver.SetActive(true);
        TargetCamera.localPosition = new Vector3(0f, 1f, 0f);
        //Source.PlayOneShot(Death);
        Rig.simulated = false;
        Collider.enabled = false;
        Controller.enabled = false;
        FireController.enabled = false;

        //Destroy(Controls.gameObject);
    }
    public void ReloadHealth()
    {
        Health = 150;
        //Instantiate(HealtPackHit, transform.position, transform.rotation);
        GameObject spawnedObject = PoolingHitsHud.Instance.GetObjectFromPool(HealtPackHit);
        spawnedObject.transform.position = Vector3.zero;
        spawnedObject.SetActive(true);
        //spawnedObject.transform.rotation = Quaternion.zero;
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void SpawnWarningHealthPack()
    {
        GameObject spawnedObject = PoolingWarningHealthPack.Instance.GetObjectFromPool();
        spawnedObject.transform.position = Warnings.position;
        spawnedObject.transform.rotation = Warnings.rotation;
        spawnedObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthPack"))
        {
            if(Health < 150)
            {
                //var R = Instantiate(W_HealthPack, Warnings, false);
                SpawnWarningHealthPack();
                ReloadHealth();
                //Destroy(collision.transform.parent.gameObject);
                collision.transform.parent.gameObject.SetActive(false);
            }
        }
        if (collision.CompareTag("SwordElite"))
        {
            Damage(50);
        }
    }
}
