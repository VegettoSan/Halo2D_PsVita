using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IdiomaText : MonoBehaviour {

	public string Español;
	public string Ingles;
	public string Portugues;

	TMP_Text Texto;

	int NIdioma;

	// Use this for initialization
	void Start () {

		NIdioma = PlayerPrefs.GetInt ("Idioma", 2);
		Texto = GetComponent<TMP_Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		NIdioma = PlayerPrefs.GetInt ("Idioma", 2);

		if (NIdioma == 1) {

			Texto.text = Español;

		}

		if (NIdioma == 2) {

			Texto.text = Ingles;

		}

		if (NIdioma == 3) {

			Texto.text = Portugues;

		}
		
	}
}
