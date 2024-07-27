using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFriend : MonoBehaviour
{
    public int Damage = 3;

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
        if (collision.CompareTag("Grunt"))
        {
            EnemyHealth G = collision.gameObject.GetComponent<EnemyHealth>();
            if(G != null)
            {
                G.Damage(Damage);
            }
        }
        if (collision.CompareTag("Elite") || collision.CompareTag("EliteSword"))
        {
            EnemyHealth E = collision.gameObject.GetComponent<EnemyHealth>();
            if (E != null)
            {
                E.Damage(Damage);
            }
        }

        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }

    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
