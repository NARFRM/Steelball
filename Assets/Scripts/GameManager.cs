using UnityEngine;
using UnityEngine.SceneManagement; // Importamos SceneManager para gestionar las escenas.
using System.Collections; // Importamos System.Collections para usar IEnumerator.


public class GameManager : MonoBehaviour
{
    
   
    public bool didLose; // Variable para saber si el jugador perdió.
    
    public bool isPaused; // Variable para saber si el juego está pausado.
    
    // Singleton: Permite que solo haya una instancia de GameManager en la escena.
    public static GameManager Instance { get; private set; } // Propiedad para acceder a la instancia.

    private void Awake()
    {
        // Verifica si ya existe una instancia del GameManager.
        if (Instance == null)
        {
            Instance = this; // Asigna esta instancia a la variable estática.
            DontDestroyOnLoad(this.gameObject); // Evita que el objeto se destruya al cargar otra escena.            
        }
        else
        {
            Destroy(this.gameObject); // Destruye el objeto si ya existe una instancia.
        }
    }

    void Start()
    {
        didLose = false; // Inicializa la variable en false.   
        isPaused = false; // Inicializa la variable en false.     
    }    
     public void GamePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Freeze the game
            // Display pause menu
            // Example: UIManager.Instance.ShowPauseMenu();
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            // Hide pause menu
            // Example: UIManager.Instance.HidePauseMenu();
        }
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.P))
    {
        GamePause();
    }
}

    public void GameOverLose (bool lose)
    {
       if (lose) // Si el jugador perdió
       {
            RestartScene(); // Reinicia la escena.
            Debug.Log("Game Over"); // Muestra un mensaje en la consola.
       }
       
    }

    IEnumerator LoadFinishScene()
{
    yield return new WaitForSeconds(1f); // Espera 1 segundo antes de recargar
    SceneManager.LoadScene("Finish");
}

    public void GameOverWin (bool winner)
    {
        if (winner) // Si el jugador ganó
        {
            CountScenes(); // Carga la siguiente escena.
            Debug.Log("You Win!"); // Muestra un mensaje en la consola.            
            StartCoroutine(LoadFinishScene());
        }

        
    }     

    // Método para cambiar a la siguiente escena en la lista de compilación.
   public void CountScenes()
{
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
        Debug.Log("Cargando escena: " + nextSceneIndex);
        SceneManager.LoadScene(nextSceneIndex); // Carga la siguiente escena
    }
    else
    {
        Debug.Log("Última escena alcanzada. Cargando escena de reinicio...");
        SceneManager.LoadScene("Finish"); // Asegurar que carga la escena de reinicio
    }
}
    // Método para reiniciar el juego cargando la escena actual de nuevo.
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual.
    }

    public void RestartGame()
    {
        // Resetear cualquier variable global
    didLose = false;
    isPaused = false;

    // Cargar la primera escena
    SceneManager.LoadScene(1);

    // Opcional: Si GameManager usa DontDestroyOnLoad, puedes resetearlo manualmente
    Instance = null;
    Destroy(gameObject);
    }

   
}