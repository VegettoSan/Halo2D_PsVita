using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{

	PlayerController player;
	MeelePlayer melee;

	void Start()
	{

		player = GetComponent<PlayerController> ();
		melee = GetComponent<MeelePlayer> ();

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
			melee.grounded = true;

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
			melee.grounded = false;

		}
	}
}
