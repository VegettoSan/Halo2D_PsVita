using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeelePlayer : MonoBehaviour
{
	Animator Anim;

	public Slider IndicatorMelee;
	public GameObject EnableMeleeButton;

	public bool Melee;
	public bool grounded;

	public int Damage = 300;
	public Collider2D AreaMelee;

	private Rigidbody2D rb2d;
	public float maxSpeed;
	public float speed;

	public float TiempoMax = 5f;
	float Timer;
	void Start()
	{
		Anim = GetComponentInParent<Animator>();
		rb2d = GetComponentInParent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if(Timer <= TiempoMax)
        {
			Timer += Time.deltaTime * 1f;
        }

		IndicatorMelee.value = Timer;

		if (Timer >= TiempoMax)
		{
			EnableMeleeButton.SetActive(true);
			if (grounded)
			{
				if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.F))
				{
					Anim.SetTrigger("Meele");
					Melee = true;
					Timer = 0f;
					EnableMeleeButton.SetActive(false);
				}
			}
		}
		AreaMelee.enabled = Melee;
	}
	void FixedUpdate()
	{

		float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
		rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

		if (Melee)
		{
			if(transform.localScale.x < 0)
            {
				rb2d.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
			}
			if (transform.localScale.x > 0)
			{
				rb2d.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
			}
		}
	}

	public void MeleeOff()
    {
		Melee = false;
		rb2d.velocity = new Vector2(0, rb2d.velocity.y);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Grunt"))
		{
			EnemyHealth G = collision.gameObject.GetComponent<EnemyHealth>();
			if (G != null)
			{
				G.Damage(Damage);
			}
			EnemyController C = collision.gameObject.GetComponent<EnemyController>();
			if (C != null)
			{
				C.Retroceder = false;
			}
		}
		if (collision.CompareTag("Elite") || AreaMelee.CompareTag("EliteSword"))
		{
			EnemyHealth E = collision.gameObject.GetComponent<EnemyHealth>();
			if (E != null)
			{
				E.Damage(Damage);
			}
			EnemyController C = collision.gameObject.GetComponent<EnemyController>();
			if (C != null)
			{
				C.Retroceder = false;
			}
		}
	}
}
