using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyLookAt : MonoBehaviour {

    public Transform Bullet;

    public bool ChageDirection;
    public float Direction;

    public string[] EnemysTags;
    public Transform target; // El objetivo al que quieres que el objeto mire
    public bool WithTarget = false;
    public float TimeRotate = 2.5f;
    public float rotateSpeed = 180f; // Velocidad de rotación del objeto

    private bool shouldRotate = false;


    public float moveSpeed = 10f; // Velocidad de movimiento al presionar la tecla espacio
    private Rigidbody2D rb;


    public float detectionDistance = 10f;
    public float detectionAngle = 90f;
    Vector2 directionToPlayer;
    float angleToPlayer;

    Quaternion targetRotation;

    private void Start()
    {
        rb = Bullet.GetComponent<Rigidbody2D>();
        ChageDirection = true;
    }

    private void Update()
    {
        // Verifica si se presionó la tecla espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Aplica una velocidad hacia la derecha
            //rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            rb.velocity = Bullet.TransformDirection(Vector2.right * moveSpeed);
        }

        if (target != null)
        {

            // Calcula la dirección del área de detección
            directionToPlayer = target.position - Bullet.position;

            if (ChageDirection)
            {
                if (Direction < 0)
                {
                    angleToPlayer = Vector2.Angle(Vector2.left, directionToPlayer);
                }
                if (Direction > 0)
                {
                    angleToPlayer = Vector2.Angle(Vector2.right, directionToPlayer);
                }
            }
            // Verifica si el jugador está dentro del área
            if (angleToPlayer <= detectionAngle / 2 && directionToPlayer.magnitude <= detectionDistance)
            {
                // Ejecuta la función deseada
                ChageDirection = false;
                shouldRotate = true;
                StartCoroutine(RotateTowardsTarget());
            }
            else
            {
                // Ejecuta otra función cuando el jugador NO está dentro del área
                ChageDirection = true;
                shouldRotate = false;
                WithTarget = false;
            }
        }
        else
        {

            if (!WithTarget)
            {

                foreach (string tag in EnemysTags)
                {

                    foreach (GameObject enemi in GameObject.FindGameObjectsWithTag(tag))
                    {
                        if (target == null)
                        {

                            target = enemi.transform;

                        }
                        else
                        {

                            if (Vector3.Distance(transform.position, enemi.transform.position) < Vector3.Distance(transform.position, target.transform.position))
                            {

                                target = enemi.transform;

                            }
                        }
                    }
                }

                WithTarget = true;

            }
            else
            {



            }
        }
    }

    private IEnumerator RotateTowardsTarget()
    {

        Vector2 dirToTarget = target.position + new Vector3(0f,1f,0f) - Bullet.position;
        float angle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;
        if (Direction < 0)
        {
            targetRotation = Quaternion.Euler(0f, 0f, -angle);
        }
        if (Direction > 0)
        {
            targetRotation = Quaternion.Euler(0f, 0f, angle);
        }
        //Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        Bullet.rotation = Quaternion.RotateTowards(Bullet.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        if (Direction < 0)
        {
            rb.velocity = Bullet.TransformDirection(Vector2.left * moveSpeed);
        }
        if (Direction > 0)
        {
            rb.velocity = Bullet.TransformDirection(Vector2.right * moveSpeed);
        }

        while (shouldRotate)
        {
            yield return new WaitForSeconds(TimeRotate); // Espera 1 segundo antes de la siguiente rotación
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Calcula la dirección del área de detección
        Vector3 forward = Bullet.right;
        Vector3 from = Quaternion.Euler(0, -detectionAngle / 2, 0) * forward;
        Vector3 to = Quaternion.Euler(0, detectionAngle / 2, 0) * forward;

        // Dibuja el arco de detección
        Gizmos.DrawRay(Bullet.position, from * detectionDistance);
        Gizmos.DrawRay(Bullet.position, to * detectionDistance);

        // Dibuja una esfera en el extremo del arco
        Vector3 spherePosition = Bullet.position + forward * detectionDistance;
        Gizmos.DrawWireSphere(spherePosition, 0.1f);
    }
}
