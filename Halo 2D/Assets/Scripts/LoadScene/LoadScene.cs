using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	Button ButtonLoad;  // Button que vas ha usar para cambiar la Scene
	public string Scene; // Name of the Scene
	public SceneLoader ScriptBarra;

	// Use this for initialization
	void Start () {

		ButtonLoad = GetComponent<Button> ();

	}
	
	// Update is called once per frame
	void Update () {

		ButtonLoad.onClick.AddListener (LoadSceneFuntion);
		
	}

	public void LoadSceneFuntion () {

		//ScriptBarra.Scene = Scene;


	}
}
