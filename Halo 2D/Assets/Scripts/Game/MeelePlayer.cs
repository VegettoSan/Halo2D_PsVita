using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeelePlayer : MonoBehaviour
{
	Animator Anim;
	void Start()
	{
		Anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.F))
		{
			Anim.SetTrigger("Meele");
		}
	}
}
