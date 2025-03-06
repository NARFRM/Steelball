using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameFromFinish : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Reiniciando el juego desde la primera escena.");

        // Validar si la escena existe en Build Settings
        if (SceneManager.sceneCountInBuildSettings > 1)
        {
            SceneManager.LoadScene(1); // Cargar la segunda escena en Build Settings
        }
        else
        {
            Debug.LogError("La escena con índice 1 no está en Build Settings.");
        }
    }
}
