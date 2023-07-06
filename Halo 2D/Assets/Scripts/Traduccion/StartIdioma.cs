using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIdioma : MonoBehaviour
{
    public Animator Anim;
    int Key;

    private void Awake()
    {
        Key = PlayerPrefs.GetInt("StartLanguague", 0);
        if (Key != 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void On()
    {
        Key = 1;
        PlayerPrefs.SetInt("StartLanguague", Key);
        PlayerPrefs.Save();
        Anim.SetBool("On", true);
        Destroy(this.gameObject, 0.3f);
    }
}
