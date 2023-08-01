using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class RoundCurrent : MonoBehaviour
{
    public static RoundCurrent Instance;

    public TMP_Text Text;

    int currentRound;
    RoundManager Manager;
    //AudioSource Source;
    Animator Anim;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Manager = GetComponentInParent<RoundManager>();
        //Source = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFx()
    {
        Text.text = "ROUND " + Manager.currentRound;
        //Source.Play();
        Anim.SetTrigger("Play");
    }
    public void PlayFxEnd()
    {
        Text.text = "RONDA FINALIZADA";
        //Source.Play();
        Anim.SetTrigger("Play");
    }
}
