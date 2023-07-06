using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFondoAleatoria : MonoBehaviour {

	AudioSource Source;
	public AudioClip[] Songs;

	private int previousIndex = -1;

	void Start () {
	
		Source = GetComponent<AudioSource> ();
	}

	void Update () {

		/*if (!Source.isPlaying) {

			Invoke ("BackgroundRandom", 1f);

		}*/

		if (!Source.isPlaying || Source.volume <= 0f) {

			//BackgroundRandom();
			Invoke("BackgroundRandom", 1f);

		}
	}

	/*void NextSong () {

		int Clip = Random.Range (0, Songs.Length);

		Source.clip = Songs [Clip];
		Source.Play();

	}*/

	void BackgroundRandom()
	{
		// Elegir un índice aleatorio, excluyendo el índice anterior
		int randomIndex = GetRandomIndex();

		// Activar el objeto correspondiente al índice aleatorio
		Source.clip = Songs[randomIndex];
		Source.Play();
	}

	private int GetRandomIndex()
	{
		int randomIndex = UnityEngine.Random.Range(0, Songs.Length);

		// Verificar si el objeto activado anteriormente tiene el mismo índice
		while (randomIndex == previousIndex)
		{
			randomIndex = UnityEngine.Random.Range(0, Songs.Length);
		}

		previousIndex = randomIndex;
		return randomIndex;
	}
}
