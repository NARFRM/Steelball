using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables
    public float speed = 50.0f;
    private Rigidbody enemyRb; // Rigidbody del enemigo
    private GameObject player; // Guarda la referencia del jugador "Player"

    // Start is called before the first frame update
    void Start()
    {
        // Leer la referencia del Rigidbody en el objeto
        enemyRb = GetComponent<Rigidbody>();

        // Localiza el GameObject "Player" en la escena
        player = GameObject.FindWithTag("Player");
        // player = GameObject.Find("Player");
    }
    void FixedUpdate()
    {
        // Fuerza de movimiento del enemigo hacia el jugador
        transform.LookAt(player.transform.position);
        enemyRb.AddForce((player.transform.position - transform.position).normalized * speed);

    }
}
