using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioClip hoverSound;  // Sonido cuando el mouse está sobre el botón
    public AudioClip clickSound;  // Sonido cuando se hace clic
    private AudioSource hoverAudioSource;
    private AudioSource clickAudioSource;
    private bool isClickable = true;

    void Start()
{
    DontDestroyOnLoad(gameObject); // Evita que el objeto se destruya al cambiar de escena

    hoverAudioSource = gameObject.AddComponent<AudioSource>();
    hoverAudioSource.loop = true;
    hoverAudioSource.ignoreListenerPause = true;

    clickAudioSource = gameObject.AddComponent<AudioSource>();
    clickAudioSource.ignoreListenerPause = true;
}


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hoverAudioSource.isPlaying && hoverSound != null) // Solo inicia si no está ya sonando y tiene clip asignado
        {
            hoverAudioSource.clip = hoverSound;
            hoverAudioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverAudioSource.isPlaying)
        {
            hoverAudioSource.Stop(); // Detiene el sonido al salir del botón
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isClickable) return; // Evita múltiples clics seguidos
        isClickable = false; // Bloquea nuevos clics temporalmente

        if (hoverAudioSource.isPlaying)
        {
            hoverAudioSource.Stop(); // Detiene el sonido del hover al hacer clic
        }

        if (clickSound != null)
        {
            clickAudioSource.PlayOneShot(clickSound); // Usa PlayOneShot para evitar cortes
            StartCoroutine(EnableClickAfterDelay(clickSound.length)); // Espera el tiempo exacto del sonido
        }
        else
        {
            Debug.LogWarning("No hay sonido asignado para el clic.");
            StartCoroutine(EnableClickAfterDelay(0.2f)); // Espera un tiempo por defecto si no hay sonido
        }
    }

    IEnumerator EnableClickAfterDelay(float delay)
    {
        Debug.Log("Esperando " + delay + " segundos antes de habilitar el clic.");
        yield return new WaitForSecondsRealtime(delay);
        isClickable = true;
        Debug.Log("Clic habilitado nuevamente.");
    }
}
