using UnityEngine;
using System.Collections;
using UnityEngine.PSVita;

public class GyroCube : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = Input.gyro.attitude;
	}
}
