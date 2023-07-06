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
            GruntHealth G = collision.gameObject.GetComponent<GruntHealth>();
            if(G != null)
            {
                G.Damage(Damage);
            }
        }
        if (collision.CompareTag("Elite") || collision.CompareTag("EliteSword"))
        {
            GruntHealth E = collision.gameObject.GetComponent<GruntHealth>();
            if (E != null)
            {
                E.Damage(Damage);
            }
        }

        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
