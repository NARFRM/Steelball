using UnityEngine;

public class TotalTime : MonoBehaviour
{
    float totalTime; // Tiempo total en segundos.
    void Update()
    {
        totalTime += Time.deltaTime; // Incrementa el tiempo transcurrido.        
    }
}
