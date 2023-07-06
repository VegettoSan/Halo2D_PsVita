using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsButtons : MonoBehaviour
{
    [SerializeField]
    AudioClip Select, Back, Forward;
    [SerializeField]
    AudioSource Source;

    public void SelectedButton()
    {
        Source.PlayOneShot(Select);
    }
    public void BackButton()
    {
        Source.PlayOneShot(Back);
    }
    public void ForwardButton()
    {
        Source.PlayOneShot(Forward);
    }
}
