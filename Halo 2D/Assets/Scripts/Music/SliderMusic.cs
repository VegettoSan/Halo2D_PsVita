using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderMusic : MonoBehaviour {

	public bool Musica;
	public bool Efectos;
	public bool Personajes;
	public TMP_Text Porcentaje;

	Slider sliderSound;

	void Start () {

		sliderSound = GetComponent<Slider> ();
		//Porcentaje = GetComponentInChildren<Text> ();

		if (Musica) {

			sliderSound.value = PlayerPrefs.GetFloat ("Musica", 0.5f);

		}

		if (Efectos) {

			sliderSound.value = PlayerPrefs.GetFloat ("Efectos", 1f);

		}

		if (Personajes) {

			sliderSound.value = PlayerPrefs.GetFloat ("Personajes", 1f);

		}

	}

	void Update () {

		float Porcent = sliderSound.value * 100f;

		Porcentaje.text = Porcent.ToString();
		//Porcentaje.text = sliderSound.value.ToString ();

		if (Musica) {

			PlayerPrefs.SetFloat ("Musica", sliderSound.value);
			PlayerPrefs.GetFloat ("Musica", 0.5f);
			PlayerPrefs.Save();
			Musica = true;
			Efectos = false;
			Personajes = false;

		}

		if (Efectos) {

			PlayerPrefs.SetFloat ("Efectos", sliderSound.value);
			PlayerPrefs.GetFloat ("Efectos", 1f);
			PlayerPrefs.Save();
			Musica = false;
			Efectos = true;
			Personajes = false;

		}

		if (Personajes) {

			PlayerPrefs.SetFloat ("Personajes", sliderSound.value);
			PlayerPrefs.GetFloat ("Personajes", 1f);
			PlayerPrefs.Save();
			Musica = false;
			Efectos = false;
			Personajes = true;

		}
	}
}
