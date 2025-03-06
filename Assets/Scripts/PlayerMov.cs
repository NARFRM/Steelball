using UnityEngine;
using System.Collections;

public class PlayerMov : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public float torqueForce = 10f;
    public Transform cameraTransform;
    private Rigidbody rb;
    private bool isGrounded;
    private bool hasJumped; 

    public AudioSource jumpAudioSource;
    public AudioSource landAudioSource;
    public AudioSource moveAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20f;
        hasJumped = false; // No ha saltado al inicio.
        isGrounded = true; // Al iniciar, asumimos que está en el suelo.
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 torqueDirection = (right * moveZ - forward * moveX).normalized;
        rb.AddTorque(torqueDirection * torqueForce, ForceMode.Force);

        if (Mathf.Abs(moveX) > 0.1f || Mathf.Abs(moveZ) > 0.1f)
        {
            if (moveAudioSource != null && !moveAudioSource.isPlaying)
            {
                moveAudioSource.Play();
            }
        }
        else
        {
            if (moveAudioSource != null && moveAudioSource.isPlaying)
            {
                moveAudioSource.Pause();
            }
        }

        // Si se presiona espacio y el jugador está en el suelo, salta
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            hasJumped = true; // Ahora sí ha saltado.

            if (jumpAudioSource != null)
            {
                jumpAudioSource.Play();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Se asegura de que el objeto está tocando el suelo con suficiente normal en Y (>0.5 significa que está en el suelo).
        if (collision.contacts[0].normal.y > 0.5f)
        {
            // Solo marcar como aterrizado si antes estaba en el aire
            if (!isGrounded)
            {
                isGrounded = true;

                // Solo reproducir el sonido de aterrizaje si realmente ha saltado antes
                if (hasJumped && landAudioSource != null)
                {
                    StartCoroutine(WaitForJumpSound());
                }

                hasJumped = false; // Reiniciamos el estado de salto después de aterrizar.
            }
        }
    }

    IEnumerator WaitForJumpSound()
    {
        yield return new WaitWhile(() => jumpAudioSource.isPlaying);
        landAudioSource.Play();
    }
}
