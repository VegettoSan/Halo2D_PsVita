using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public GameObject MenuPause;

	void Start () 
    {
        Time.timeScale = 1f;
        MenuPause.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () 
    {
		if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Escape))
        {
			TogglePause();
		}
	}

    public void TogglePause()
    {
        if (Time.timeScale == 1f)
        {
            MenuPause.SetActive(true);
            Time.timeScale = 0f; // Pausar el tiempo
            Debug.Log("Juego pausado");
        }
        else if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // Reanudar el tiempo
            MenuPause.SetActive(false);
            Debug.Log("Juego reanudado");
        }
    }
}
