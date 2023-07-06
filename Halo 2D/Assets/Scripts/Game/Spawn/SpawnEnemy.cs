using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

    public GameObject[] Enemy;
    public string Tag;
    public int Health = 100;
    public Transform[] PositionsSpawn;
    public int Total = 5;
    public float Timer = 5f;

    GameObject[] TotalBot;
    int TotalSpawn;

    private void Start()
    {
        StartCoroutine(ActivateRandomObjectCoroutine());
    }

    private IEnumerator ActivateRandomObjectCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Timer);
            EnemySpawn();
        }
    }

    void EnemySpawn()
    {
        TotalBot = GameObject.FindGameObjectsWithTag(Tag);
        //TotalHealthPack = GameObject.FindGameObjectsWithTag("HealthPack");

        TotalSpawn = TotalBot.Length;

        if (TotalSpawn < Total)
        {
            int i = UnityEngine.Random.Range(0, Enemy.Length);
            int p = UnityEngine.Random.Range(0, PositionsSpawn.Length);

            GameObject spawnedObject = PoolingEnemy.Instance.GetObjectFromPool(Enemy[i]);
            spawnedObject.transform.position = PositionsSpawn[p].position;

            spawnedObject.GetComponent<CapsuleCollider2D>().enabled = true;
            spawnedObject.GetComponent<Rigidbody2D>().simulated = true;
            spawnedObject.GetComponent<EnemyController>().enabled = true;
            spawnedObject.GetComponent<GruntHealth>().enabled = true;
            spawnedObject.GetComponent<GruntHealth>().Health = Health;
            spawnedObject.GetComponent<FireEnemy>().enabled = true;

            spawnedObject.SetActive(true);
        }
    }
}
