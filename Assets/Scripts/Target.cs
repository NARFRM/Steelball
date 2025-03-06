using UnityEngine;
using UnityEngine.SceneManagement; // Importamos SceneManager para gestionar cambios de escena.

public class Target : MonoBehaviour
{   
    // Método que se ejecuta cuando otro objeto entra en el collider de este objeto.
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó tiene la etiqueta "Player".
        if (other.CompareTag("Player"))
        {
            // Llama al método del GameManager para cambiar a la siguiente escena.
            GameManager.Instance.GameOverWin(true);
        }
    }
}
