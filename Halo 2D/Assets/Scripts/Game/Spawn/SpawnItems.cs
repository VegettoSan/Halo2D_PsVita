using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{

    public GameObject[] Items;
    public Transform[] PositionsSpawn;
    public int Total = 5;
    public float Timer = 5f;

    GameObject[] TotalRifle, TotalHealthPack;
    public int MaxRifle, MaxHealthPack;
    int TotalSpawn;

    private void Start()
    {
        //BackgroundRandom();
        StartCoroutine(ActivateRandomObjectCoroutine());
    }

    private IEnumerator ActivateRandomObjectCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Timer);
            ItemSpawn();
        }
    }

    void ItemSpawn()
    {
        TotalRifle = GameObject.FindGameObjectsWithTag("Rifle");
        TotalHealthPack = GameObject.FindGameObjectsWithTag("HealthPack");

        TotalSpawn = TotalRifle.Length + TotalHealthPack.Length;

        int i = UnityEngine.Random.Range(0, Items.Length);
        int p = UnityEngine.Random.Range(0, PositionsSpawn.Length);

        if (TotalSpawn < Total)
        {
            if (TotalRifle.Length <= MaxRifle && TotalHealthPack.Length <= MaxHealthPack)
            {
                GameObject spawnedObject = PoolingItems.Instance.GetObjectFromPool(Items[i]);
                spawnedObject.transform.position = PositionsSpawn[p].position;
                spawnedObject.SetActive(true);
            }
            if(TotalRifle.Length >= MaxRifle)
            {
                GameObject spawnedObject = PoolingItems.Instance.GetObjectFromPool(Items[0]);
                spawnedObject.transform.position = PositionsSpawn[p].position;
                spawnedObject.SetActive(true);
            }
            else if (TotalHealthPack.Length >= MaxHealthPack)
            {
                GameObject spawnedObject = PoolingItems.Instance.GetObjectFromPool(Items[1]);
                spawnedObject.transform.position = PositionsSpawn[p].position;
                spawnedObject.SetActive(true);
            }
        }
    }
}
