using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionApp : MonoBehaviour
{
    TMP_Text Texto;
    public string TextStart = "ver ";
    void Start()
    {
        Texto = GetComponent<TMP_Text>();
        Texto.text = TextStart + Application.version.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
