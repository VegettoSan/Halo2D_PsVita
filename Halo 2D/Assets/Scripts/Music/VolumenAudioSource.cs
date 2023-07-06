using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumenAudioSource : MonoBehaviour {

	AudioSource Source;
	public bool Musica;
	public bool Efectos;
	public bool Personajes;

	void Start () {

		Source = GetComponent<AudioSource> ();
		
	}

	void Update () {

		if (Musica) {

			Source.volume = PlayerPrefs.GetFloat ("Musica", 0.5f);
			Musica = true;
			Efectos = false;
			Personajes = false;

		}

		if (Efectos) {

			Source.volume = PlayerPrefs.GetFloat ("Efectos", 1f);
			Musica = false;
			Efectos = true;
			Personajes = false;

		}

		if (Personajes) {

			Source.volume = PlayerPrefs.GetFloat ("Personajes", 1f);
			Musica = false;
			Efectos = false;
			Personajes = true;

		}
	}
}
