using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    private GameObject damageScreen; // Referencia a DamageScreen
    public AudioClip damageSound; // Sonido de daño
    private float screenTime = 1f; // Tiempo de pantalla de daño
    private bool hasDamaged = false;

    private void Start()
    {
        // Encuentra DamageScreen automáticamente
        damageScreen = GameObject.Find("Canvas/Damage/DamageScreen");

        if (damageScreen == null)
        {
            Debug.LogError("¡DamageScreen no encontrado! Verifica la ruta en la jerarquía.");
        }
        else
        {
            damageScreen.SetActive(false); // Asegura que inicie desactivado
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Si el Player choca con el enemigo
        {
            Debug.Log("Jugador recibió daño");

            if (AudioManager.Instance != null && damageSound != null)
            {
                AudioManager.Instance.PlaySFX(damageSound);
            }

            if (damageScreen != null)
            {
                damageScreen.SetActive(true); // Muestra la pantalla de daño
                hasDamaged = true;
            }

            // Retrasa el Game Over para que la pantalla de daño sea visible
            Invoke("GameOver", 1f);
        }
    }

    private void Update()
    {
        if (hasDamaged)
        {
            screenTime -= Time.deltaTime;
            if (screenTime <= 0)
            {
                damageScreen.SetActive(false); // Oculta la pantalla de daño
                hasDamaged = false;
                screenTime = 1f; // Reinicia el tiempo
            }
        }
    }

    void GameOver()
    {
        GameManager.Instance.GameOverLose(true);
    }
}
