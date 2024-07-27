using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public int Damage = 3;
    //[SerializeField] bool Destroyed = true;
    private IEnumerator ContadorCoroutine()
    {
        yield return new WaitForSeconds(5f); // Esperar

        this.gameObject.SetActive(false); // Desactivar el GameObject

        // Aquí puedes agregar cualquier otra lógica que deseas ejecutar después de desactivar el GameObject

        yield break; // Finalizar la corrutina
    }

    void Update()
    {
        //Destroy(this.gameObject, 5f);
        StartCoroutine(ContadorCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthPlayer P = collision.gameObject.GetComponent<HealthPlayer>();
            if (P != null)
            {
                P.Damage(Damage);
            }
        }
        /*if (Destroyed)
        {
            Destroy(this.gameObject);
        }*/
        this.gameObject.SetActive(false);
    }

    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
