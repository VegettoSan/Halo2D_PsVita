using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float h;
	public float maxSpeed;
	public float speed;
	public bool grounded;
	

	private Rigidbody2D rb2d;
	private Animator anim;
	private SpriteRenderer spr;

	public bool Retroceder;
	private float Distancee;
	public float ddistance;
	public float DistanceBack, DistanceStop, DistanceFire, DistanceMax;
	private bool movement = true;
	public string[] EnemysTags;
	public GameObject Target;
	public bool WithTarget = false;

	[SerializeField] FireEnemy FireController;


	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spr = GetComponentInChildren<SpriteRenderer>();
		//FireController = GetComponent<FireEnemy>();

		anim.SetBool("Attack", false);
		anim.SetBool("Back", false);
	}


	void Update()
	{
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));


		if (Target != null)
		{
			if (Target.tag == "null")
			{
				Target = null;
				WithTarget = false;
				anim.SetBool("Attack", false);
				anim.SetBool("Back", false);
			}
			else if (Target.tag != "null")
			{

				Distancee = Vector3.Distance(transform.position, Target.transform.position);
				ddistance = Distancee;
				if (Distancee >= DistanceMax)
				{
					WithTarget = false;
					Target = null;
				}
				if (Distancee > DistanceStop && Distancee < DistanceMax)
				{
					if (Target.transform.position.x < transform.position.x)
					{
						h = -1f;
						LookBack();
					}
					else if (Target.transform.position.x > transform.position.x)
					{
						h = 1f;
						LookForward();
					}
					anim.SetBool("Attack", false);
					anim.SetBool("Back", false);
				}
				if (Distancee <= DistanceFire && Distancee > DistanceStop)
				{
					anim.SetBool("Attack", false);
					anim.SetBool("Back", false);
					if (FireController != null)
					{
						FireController.Fire = true;
					}
				}
				else if (Distancee > DistanceFire)
				{
					anim.SetBool("Attack", false);
					anim.SetBool("Back", false);
					if (FireController != null)
					{
						FireController.Fire = false;
					}
				}
				if (!Retroceder)
				{
					if (Distancee <= DistanceStop)
					{
						anim.SetBool("Attack", true);
						anim.SetBool("Back", false);
						h = 0;
						if (Target.transform.position.x < transform.position.x)
						{
							LookBack();
						}
						else if (Target.transform.position.x > transform.position.x)
						{
							LookForward();
						}
					}
				}
                else
                {
					if (Distancee <= DistanceStop && Distancee > DistanceBack)
					{
						anim.SetBool("Attack", true);
						anim.SetBool("Back", false);
						h = 0;
						if (Target.transform.position.x < transform.position.x)
						{
							LookBack();
						}
						else if (Target.transform.position.x > transform.position.x)
						{
							LookForward();
						}
					}
					if (Distancee <= DistanceBack)
					{
						anim.SetBool("Attack", false);
						anim.SetBool("Back", true);
						
						if (Target.transform.position.x < transform.position.x)
						{
							h = 1;
							LookBack();
						}
						else if (Target.transform.position.x > transform.position.x)
						{
							h = -1;
							LookForward();
						}
					}
				}

			}
		}
		else
		{
			anim.SetBool("Attack", false);
			if (FireController != null)
			{
				FireController.Fire = false;
			}
			WithTarget = false;
			h = 0f;
		}

		if (!WithTarget)
		{

			foreach (string tag in EnemysTags)
			{

				foreach (GameObject enemi in GameObject.FindGameObjectsWithTag(tag))
				{
					if (Target == null)
					{

						Target = enemi;

					}
					else
					{

						if (Vector3.Distance(transform.position, enemi.transform.position) < Vector3.Distance(transform.position, Target.transform.position))
						{

							Target = enemi;

						}
					}
				}
			}

			WithTarget = true;

		}
		else
		{



		}
	}

	void FixedUpdate()
	{

		Vector3 fixedVelocity = rb2d.velocity;
		fixedVelocity.x *= 0.75f;

		if (grounded)
		{
			rb2d.velocity = fixedVelocity;
		}

		//h = InputManager.GetAxis("Joystick", "Horizontal");
		if (!movement) h = 0;

		rb2d.AddForce(Vector2.right * speed * h);

		float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
		rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

		/*if (h > 0.01f)
		{
			LookForward();
		}

		if (h < -0.01f)
		{
			LookBack();
		}*/

	}
	
	void LookForward()
    {
		transform.localScale = new Vector3(1f, 1f, 0f);
	}
	void LookBack()
	{
		transform.localScale = new Vector3(-1f, 1f, 0f);
	}
}
