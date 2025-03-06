using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // Referencia al jugador que la cámara seguirá.
    
    // Desplazamiento inicial de la cámara respecto al jugador.
    private Vector3 offset = new Vector3(0f, 5f, -6f); 

    public float RotationSpeed = 200.0f; // Velocidad de rotación de la cámara con el mouse.
    public float zoomSpeed = 2f; // Velocidad del zoom con la rueda del mouse.
    public float minZoom = 5f; // Límite máximo de alejamiento de la cámara.
    public float maxZoom = 20f;  // Límite mínimo de acercamiento de la cámara.
    public float zoomSmoothness = 5f; // Factor de suavizado del zoom.

    private float yaw = 0f; // Rotación horizontal acumulada.
    private float pitch = 0f; // Rotación vertical acumulada.
    private float targetZoom; // Valor objetivo del zoom.

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla y lo oculta.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Inicializa el zoom en la posición actual de la cámara.
        targetZoom = offset.z;
    }

    void LateUpdate() // Se ejecuta después de Update para evitar movimientos bruscos.
    {
        // Captura el movimiento del mouse para rotar la cámara.
        float mouseX = Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;

        yaw += mouseX;  // Acumula la rotación horizontal.
        pitch -= mouseY; // Acumula la rotación vertical (invertida).
        pitch = Mathf.Clamp(pitch, -30f, 60f); // Restringe el ángulo de inclinación.

        // Captura la entrada del scroll del mouse para el zoom.
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetZoom += scroll * zoomSpeed; // Ajusta el valor objetivo del zoom.
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom); // Limita el zoom dentro de los valores permitidos.

        // Suaviza la transición del zoom con interpolación lineal (Lerp).
        offset.z = Mathf.Lerp(offset.z, targetZoom, Time.deltaTime * zoomSmoothness);

        // Aplica la rotación de la cámara con base en los valores de yaw y pitch.
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 rotatedOffset = rotation * offset;

        // Posiciona la cámara con respecto al jugador manteniendo el offset.
        transform.position = player.transform.position + rotatedOffset;

        // Hace que la cámara siempre mire al jugador.
        transform.LookAt(player.transform.position);
    }
}
