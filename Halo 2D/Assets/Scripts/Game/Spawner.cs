using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public bool Bot = true;
	public Transform[] Enemy;
	public Transform[] PocionesSpawn;
	private float Timer = 15f;
	public float Tiempo = 15f;

	GameObject[] TotalBots;
	public string Tag;

	[Range(1, 10)]
	public int MaxBots;

	public int Health;

	void Awake()
	{
		Timer = Time.time + Tiempo;
	}

	void Start()
	{

	}

	void Update()
	{

		TotalBots = GameObject.FindGameObjectsWithTag(Tag);

		if (TotalBots.Length < MaxBots)
		{

			Instanciar();

		}
		else if (TotalBots == null)
		{

			Instanciar();

		}

	}

	public void Instanciar()
	{

		if (Timer < Time.time)
		{
			int i = Random.Range(0, Enemy.Length);
			int a = Random.Range(0, PocionesSpawn.Length);
			var E = Instantiate(Enemy[i], PocionesSpawn[a].position,
				PocionesSpawn[a].rotation);
            if (Bot)
            {
				E.GetComponent<GruntHealth>().Health = Health;
			}
			Timer = Time.time + Tiempo;
		}

	}
}
