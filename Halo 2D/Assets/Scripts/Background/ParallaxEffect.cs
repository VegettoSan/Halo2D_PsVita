using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform[] backgrounds;          // Array de los fondos o capas que se moverán con efecto parallax
    public float[] parallaxScales;           // Array de los factores de escala de parallax para cada capa
    public float parallaxSpeed = 0.2f;       // Velocidad de movimiento parallax

    private Transform cam;                   // Referencia a la cámara principal
    private Vector3 previousCamPos;          // Posición de la cámara en el fotograma anterior

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPos = cam.position;
    }

    void Update()
    {
        // Calcula el desplazamiento desde el fotograma anterior hasta el actual
        float parallax = (previousCamPos.x - cam.position.x) * parallaxSpeed;

        // Para cada fondo en el array, aplica el efecto parallax según su factor de escala
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float backgroundTargetPosX = backgrounds[i].position.x + parallax * parallaxScales[i];
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, Time.deltaTime);
        }

        // Guarda la posición actual de la cámara para el siguiente fotograma
        previousCamPos = cam.position;
    }
}

