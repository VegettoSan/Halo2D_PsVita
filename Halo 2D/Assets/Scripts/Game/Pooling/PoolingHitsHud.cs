using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingHitsHud : MonoBehaviour {

    public static PoolingHitsHud Instance;
    public List<GameObject> prefabs;
    public int initialPoolSize = 10;

    private Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (GameObject prefab in prefabs)
        {
            CreateObjectPool(prefab);
        }
    }

    public GameObject GetObjectFromPool(GameObject prefab)
    {
        if (pooledObjects.ContainsKey(prefab))
        {
            List<GameObject> objects = pooledObjects[prefab];

            foreach (GameObject obj in objects)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            // If no inactive object is found, create a new one
            GameObject newObj = CreateNewObject(prefab);
            newObj.SetActive(true);
            return newObj;
        }
        else
        {
            Debug.LogWarning("Prefab not found in the object pool.");
            return null;
        }
    }

    private void CreateObjectPool(GameObject prefab)
    {
        List<GameObject> objects = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject newObj = CreateNewObject(prefab);
            objects.Add(newObj);
        }

        pooledObjects[prefab] = objects;
    }

    private GameObject CreateNewObject(GameObject prefab)
    {
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }
}
