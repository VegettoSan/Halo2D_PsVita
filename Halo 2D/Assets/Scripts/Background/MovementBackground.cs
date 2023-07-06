using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBackground : MonoBehaviour
{
    public Camera Camera;
    public Color Color;
    public Vector3 PositionForward;
    public Vector3 PositionBack;
    public bool Next, Opuesto;
    public GameObject Background;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!Opuesto)
            {
                if (!Next)
                {
                    Background.transform.localPosition = PositionForward;
                    Camera.backgroundColor = Color;
                    transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                    Next = true;
                }
                else if (Next)
                {
                    Background.transform.localPosition = PositionBack;
                    Camera.backgroundColor = Color;
                    transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                    Next = false;
                }
            }else if (Opuesto)
            {
                if (!Next)
                {
                    Background.transform.localPosition = PositionForward;
                    Camera.backgroundColor = Color;
                    transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                    Next = true;
                }
                else if (Next)
                {
                    Background.transform.localPosition = PositionBack;
                    Camera.backgroundColor = Color;
                    transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                    Next = false;
                }
            }
        }
    }
}
