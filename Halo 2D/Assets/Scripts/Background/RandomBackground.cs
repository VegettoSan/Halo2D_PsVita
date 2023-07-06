using System;
using System.Collections;
using UnityEngine;

public class RandomBackground : MonoBehaviour
{
    public float Timer = 5f, TimerFade = 0.5f;
    public GameObject[] Maps;
    public Animator Anim;

    private int previousIndex = -1;

    private void Start()
    {
        //BackgroundRandom();
        StartCoroutine(ActivateRandomObjectCoroutine());
    }

    private IEnumerator ActivateRandomObjectCoroutine()
    {
        while (true)
        {
            Anim.SetTrigger("Fade");
            yield return new WaitForSeconds(TimerFade);

            // Desactivar todos los objetos del arreglo Map
            foreach (GameObject obj in Maps)
            {
                obj.SetActive(false);
            }

            // Activar un objeto aleatorio del arreglo Map
            BackgroundRandom();

            yield return new WaitForSeconds(Timer);
        }
    }

    void BackgroundRandom()
    {
        /*int randomIndex = UnityEngine.Random.Range(0, Maps.Length);
        Maps[randomIndex].SetActive(true);*/

        // Elegir un índice aleatorio, excluyendo el índice anterior
        int randomIndex = GetRandomIndex();

        // Activar el objeto correspondiente al índice aleatorio
        Maps[randomIndex].SetActive(true);
    }

    private int GetRandomIndex()
    {
        int randomIndex = UnityEngine.Random.Range(0, Maps.Length);

        // Verificar si el objeto activado anteriormente tiene el mismo índice
        while (randomIndex == previousIndex)
        {
            randomIndex = UnityEngine.Random.Range(0, Maps.Length);
        }

        previousIndex = randomIndex;
        return randomIndex;
    }
}
