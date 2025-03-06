using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy; // Guarda la referencia del Prefab del enemigo
    private Vector3 coordinates = new Vector3((float)0.5, (float)3, (float)-48); // Posición de aparición

    // Start is called before the first frame update
    void Start()
    {
        // Parámetros (Prefab del enemigo, posición, rotación)
        Instantiate(enemy, coordinates, enemy.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
