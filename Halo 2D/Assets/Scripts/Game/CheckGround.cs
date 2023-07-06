using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{

	PlayerController player;

	void Start()
	{

		player = GetComponent<PlayerController> ();

	}

	void Update()
	{

	}

	void OnCollisionStay2D(Collision2D col)
	{

		if (col.gameObject.tag == "GroudLand"
			|| col.gameObject.tag == "GroudMetal"
			|| col.gameObject.tag == "GroudSand"
			|| col.gameObject.tag == "GroudConcrete")
		{

			player.grounded = true;

		}
	}

	void OnCollisionExit2D(Collision2D col)
	{

		if (col.gameObject.tag == "GroudLand"
			|| col.gameObject.tag == "GroudMetal"
			|| col.gameObject.tag == "GroudSand"
			|| col.gameObject.tag == "GroudConcrete")
		{

			player.grounded = false;

		}
	}
}
