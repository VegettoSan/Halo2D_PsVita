using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingWarningRifle : MonoBehaviour {

    public static PoolingWarningRifle Instance;
    public GameObject Warnings;
    public GameObject prefab;
    public int initialPoolSize = 5;

    private List<GameObject> pooledObjects = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObject();
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive object is found, create a new one
        GameObject newObj = CreateNewObject();
        newObj.SetActive(true);
        return newObj;
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(prefab);
        newObj.transform.SetParent(Warnings.transform);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }
}
