using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BarraDeCargaEscena : MonoBehaviour {

	public Slider BarraCarga;
	public TMP_Text PorcentajeCarga;
	public string Scene;

	void Start () {

		StartCoroutine (LoadScena ());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator LoadScena()
	{
		AsyncOperation loading;

		//Iniciamos la carga asíncrona de la escena y guardamos el proceso en 'loading'
		loading = SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Single);

		//Bloqueamos el salto automático entre escenas para asegurarnos el control durante el proceso
		loading.allowSceneActivation = false;

		//Cuando la escena llega al 90% de carga, se produce el cambio de escena
		while (loading.progress < 0.9f) {
			
			float porcentaje = BarraCarga.value * 10f;

			//Actualizamos el % de carga de una forma optima 
			//(concatenar con + tiene un alto coste en el rendimiento)
			PorcentajeCarga.text = string.Format ("{0}%", loading.progress * 100);
			//PorcentajeCarga.text = porcentaje.ToString();

			//Actualizamos la barra de carga
			BarraCarga.value = loading.progress;

			//Esperamos un frame
			yield return null;
		}

		//Mostramos la carga como finalizada
		PorcentajeCarga.text = "100%";
		BarraCarga.value = 100f;

		//Activamos el salto de escena.
		loading.allowSceneActivation = true;


	}
}
