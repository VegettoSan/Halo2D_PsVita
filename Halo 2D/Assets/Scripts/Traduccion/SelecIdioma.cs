using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelecIdioma : MonoBehaviour {

	public int NIdioma;


	// Use this for initialization
	void Start () {
		
		NIdioma = PlayerPrefs.GetInt ("Idioma", 2);

	}
	
	// Update is called once per frame
	void Update () {

		NIdioma = PlayerPrefs.GetInt("Idioma", 2);

	}

	public void Español() {

		NIdioma = 1;
		PlayerPrefs.SetInt ("Idioma", NIdioma);
		PlayerPrefs.Save();
	}

	public void Ingles() {

		NIdioma = 2;
		PlayerPrefs.SetInt ("Idioma", NIdioma);
		PlayerPrefs.Save();
	}

	public void Portugues() {

		NIdioma = 3;
		PlayerPrefs.SetInt ("Idioma", NIdioma);
		PlayerPrefs.Save();
	}
}
