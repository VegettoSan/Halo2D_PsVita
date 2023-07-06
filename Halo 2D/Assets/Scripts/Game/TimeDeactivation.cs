using System.Collections;
using UnityEngine;

public class TimeDeactivation : MonoBehaviour
{
    public float Timer;
   private IEnumerator ContadorCoroutine()
    {
        yield return new WaitForSeconds(Timer); // Esperar

        this.gameObject.SetActive(false); // Desactivar el GameObject

        // Aquí puedes agregar cualquier otra lógica que deseas ejecutar después de desactivar el GameObject

        yield break; // Finalizar la corrutina
    }

    public void CountStart()
    {
        StartCoroutine(ContadorCoroutine());
    }
    public void Deactive()
    {
        this.gameObject.SetActive(false);
    }
}
