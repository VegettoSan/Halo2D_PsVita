using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdiomaContent : MonoBehaviour
{
    public GameObject Español, Ingles;
    int Key;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Key = PlayerPrefs.GetInt("Idioma", 2);

        switch (Key)
        {
            case 1:

                Español.SetActive(true);
                Ingles.SetActive(false);

                break;

            case 2:

                Español.SetActive(false);
                Ingles.SetActive(true);

                break;
        }
    }
}
