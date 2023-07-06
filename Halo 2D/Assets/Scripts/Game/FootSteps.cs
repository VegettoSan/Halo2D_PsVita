using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    PlayerController Controller;
    AudioSource Source;
    public AudioClip[] Land;
    public AudioClip[] Metal;
    public AudioClip[] Sand;
    public AudioClip[] Concrete;

    public float Cadencia = 5f;
    float NextStep;
    void Start()
    {
        Source = GetComponent<AudioSource>();
        Controller = GetComponent<PlayerController>();
    }


    void Update()
    {
        //Cadencia *= Controller.h;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (Controller.grounded && Controller.h != 0f)
        {
            if (col.gameObject.tag == "GroudLand")
            {
                if (Time.time >= NextStep)
                {
                    NextStep = Time.time + 1 / Cadencia;
                    RamdonStepLand();
                }
            }
            if (col.gameObject.tag == "GroudMetal")
            {
                if (Time.time >= NextStep)
                {
                    NextStep = Time.time + 1 / Cadencia;
                    RamdonStepMetal();
                }
            }
            if (col.gameObject.tag == "GroudSand")
            {
                if (Time.time >= NextStep)
                {
                    NextStep = Time.time + 1 / Cadencia;
                    RamdonStepSand();
                }
            }
            if (col.gameObject.tag == "GroudConcrete")
            {
                if (Time.time >= NextStep)
                {
                    NextStep = Time.time + 1 / Cadencia;
                    RamdonStepConcrete();
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (!Controller.grounded)
        {
            NextStep = 0f;
        }
    }

    void RamdonStepLand()
    {
        int Clip = Random.Range(0, Land.Length);
        Source.PlayOneShot(Land[Clip]);
    }
    void RamdonStepMetal()
    {
        int Clip = Random.Range(0, Metal.Length);
        Source.PlayOneShot(Metal[Clip]);
    }
    void RamdonStepSand()
    {
        int Clip = Random.Range(0, Sand.Length);
        Source.PlayOneShot(Sand[Clip]);
    }
    void RamdonStepConcrete()
    {
        int Clip = Random.Range(0, Concrete.Length);
        Source.PlayOneShot(Concrete[Clip]);
    }
}
