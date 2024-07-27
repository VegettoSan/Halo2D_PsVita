using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    enum Character { Grunt, Elite, EliteSword }
    [SerializeField]
    Character Personaje;
    public int Health = 100;
    Animator Anim;
    AudioSource Source;
    EnemyController Controller;
    FireEnemy FireController;
    Rigidbody2D Rig;
    public CapsuleCollider2D Collider;
    public AudioClip Death;
    void Start()
    {
        Anim = GetComponent<Animator>();
        Source = GetComponent<AudioSource>();
        Controller = GetComponent<EnemyController>();
        FireController = GetComponent<FireEnemy>();
        Rig = GetComponent<Rigidbody2D>();

        Anim.SetBool("Attack", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Controller.Retroceder = false;
            Anim.SetBool("Death", true);
        }
    }

    public void Damage(int total)
    {
        Health -= total;
    }

    public void Dead()
    {
        //Score Score = GameObject.FindGameObjectWithTag("GameController").GetComponent<Score>();
        if (Personaje == Character.Elite)
        {
            Score.include.Elite++;
            Score.include.ScoreMax += 200;
        }
        if (Personaje == Character.EliteSword)
        {
            Score.include.Elite++;
            Score.include.ScoreMax += 500;
        }
        if (Personaje == Character.Grunt)
        {
            Score.include.Grunt++;
            Score.include.ScoreMax += 80;
        }
        Source.PlayOneShot(Death);
        Rig.simulated = false;
        Collider.enabled = false;
        Controller.enabled = false;
        if (FireController != null)
        {
            FireController.enabled = false;
        }
    }
    public void Destroy()
    {
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MeeleSpartan"))
        {
            Health = 0;
            Damage(300);
        }
    }*/
}

