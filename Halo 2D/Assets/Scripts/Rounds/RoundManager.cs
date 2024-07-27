using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    public TMP_Text Text, TextGame;

    public GameObject[] Enemy; // El prefab que se instanciará en cada ronda
    public List<Transform> spawnLocations; // La lista de ubicaciones donde se pueden instanciar los clones del prefab
    public float waitTime = 10f; // El tiempo de espera al inicio de cada ronda
    public int initialSpawnCount = 5; // La cantidad inicial de clones del prefab que se instanciarán en la primera ronda
    public int spawnIncrement = 2; // El incremento en la cantidad de clones del prefab que se instanciarán en cada ronda

    [Space]
    public int currentRound = 0; // La ronda actual
    private int currentSpawnCount; // La cantidad de clones del prefab que se instanciarán en la ronda actual
    private List<GameObject> spawnedObjects = new List<GameObject>(); // La lista de objetos instanciados en la ronda actual

    int e;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        spawnLocations = new List<Transform>();
        GameObject[] spawnObjects = GameObject.FindGameObjectsWithTag("SpawnEnemy"); // Busca todos los objetos con el tag "SpawnEnemy"
        foreach (GameObject spawnObject in spawnObjects)
        {
            spawnLocations.Add(spawnObject.transform); // Agrega la posición del objeto a la lista de posiciones de Spawn
        }

        currentSpawnCount = initialSpawnCount; // Inicializa la cantidad de clones del prefab que se instanciarán en la primera ronda
        StartCoroutine(RoundRoutine()); // Inicia la corutina que controla el ciclo de las rondas
    }

    IEnumerator RoundRoutine()
    {
        while (true) // Ciclo infinito para repetir las rondas indefinidamente
        {
            spawnedObjects.Clear(); // Reinicia la lista de objetos instanciados al inicio de cada ronda
            yield return new WaitForSeconds(waitTime); // Espera al inicio de cada ronda
            RoundCurrent.Instance.PlayFx();
            yield return StartCoroutine(SpawnObjects()); // Instancia los clones del prefab uno por uno con un intervalo de tiempo aleatorio entre 2 y 5 segundos
            yield return new WaitUntil(() => AreAllObjectsDisabled()); // Espera a que todos los prefabs instanciados estén desactivados
            RoundCurrent.Instance.PlayFxEnd();
            currentRound++; // Incrementa el contador de rondas
            currentSpawnCount += spawnIncrement; // Incrementa la cantidad de clones del prefab que se instanciarán en la siguiente ronda
            Text.text = currentRound.ToString();
            TextGame.text = currentRound.ToString();
        }
    }

    IEnumerator SpawnObjects()
    {
        for (int i = 0; i < currentSpawnCount; i++) // Ciclo para instanciar todos los clones del prefab para la ronda actual
        {
            if (currentRound < 10)
            {
                e = UnityEngine.Random.Range(0, 2);
            }
            if (currentRound >= 10)
            {
                e = UnityEngine.Random.Range(0, Enemy.Length);
            }
            int p = UnityEngine.Random.Range(0, spawnLocations.Count);

            GameObject spawnedObject = PoolingEnemy.Instance.GetObjectFromPool(Enemy[e]); // Instancia un clone del prefab en la ubicación seleccionada
            spawnedObject.transform.position = spawnLocations[p].position; // Selecciona una ubicación aleatoria de la lista de ubicaciones
            spawnedObjects.Add(spawnedObject); // Agrega el objeto instanciado a la lista de objetos instanciados en la ronda actual

            //spawnedObject.GetComponentInChildren<EnemyHealth>().Health = spawnedObject.GetComponentInChildren<EnemyHealth>().Health + (10 * currentRound);
            spawnedObject.GetComponent<CapsuleCollider2D>().enabled = true;
            spawnedObject.GetComponent<Rigidbody2D>().simulated = true;
            spawnedObject.GetComponent<EnemyController>().enabled = true;
            spawnedObject.GetComponent<EnemyHealth>().enabled = true;
            switch (spawnedObject.tag)
            {
                case "Grunt":
                    spawnedObject.GetComponent<EnemyHealth>().Health = 100;
                    spawnedObject.GetComponent<EnemyController>().Retroceder = true;
                    break;
                case "Elite":
                    spawnedObject.GetComponent<EnemyHealth>().Health = 200;
                    spawnedObject.GetComponent<EnemyController>().Retroceder = true;
                    break;
                case "EliteSword":
                    spawnedObject.GetComponent<EnemyHealth>().Health = 300;
                    spawnedObject.GetComponent<EnemyController>().Retroceder = true;
                    break;
            }
            if(spawnedObject.GetComponent<FireEnemy>() != null)
            {
                spawnedObject.GetComponent<FireEnemy>().enabled = true;
            }

            spawnedObject.SetActive(true);
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Espera un tiempo aleatorio entre 2 y 5 segundos antes de instanciar el siguiente clone del prefab
        }
    }

    bool AreAllObjectsDisabled()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj.activeInHierarchy)
                return false;
        }
        return true;
    }
}
