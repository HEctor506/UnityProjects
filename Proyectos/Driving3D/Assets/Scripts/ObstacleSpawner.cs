using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles; // Array de prefabs de obstáculos
    public Transform player;       // Referencia al jugador (el auto)
    public float spawnDistance = 20f; // Distancia delante del jugador para generar obstáculos
    public float spawnInterval = 2f; // Tiempo entre generación de obstáculos
    public float roadWidth = 5f;   // Ancho de la carretera

    public int poolSize = 10;      // Tamaño del pool
    private Queue<GameObject> objectPool; // Cola de objetos para el pool
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializePool(); // Inicializamos el pool
        StartCoroutine(SpawnObstacles()); // Iniciamos la generación de obstáculos
    }

    // Inicializa el pool de objetos
    void InitializePool()
    {
        objectPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            // Elegimos un prefab al azar para diversificar el pool
            GameObject obj = Instantiate(obstacles[Random.Range(0, obstacles.Length)]);
            obj.SetActive(false); // Lo desactivamos inicialmente
            objectPool.Enqueue(obj); // Lo añadimos al pool
        }
    }

    // Obtiene un objeto del pool
    GameObject GetPooledObject()
    {
        GameObject obj = objectPool.Dequeue(); // Sacamos un objeto del pool
        obj.SetActive(true); // Lo activamos
        objectPool.Enqueue(obj); // Lo devolvemos al final de la cola
        return obj;
    }

    // Corrutina para generar obstáculos periódicamente
    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObstacle();
        }
    }

    // Lógica para generar un obstáculo en la carretera
    void SpawnObstacle()
    {
        // Obtén un objeto del pool
        GameObject obstacle = GetPooledObject();

        // Calcula una posición aleatoria en la carretera
        float randomX = Random.Range(-roadWidth / 2, roadWidth / 2);
        Vector3 spawnPosition = new Vector3(randomX, 0, player.position.z + spawnDistance);

        // Coloca el obstáculo en la posición calculada
        obstacle.transform.position = spawnPosition;
    }

    // Opcional: Desactiva obstáculos que pasaron detrás del jugador
    void Update()
    {
        foreach (GameObject obstacle in objectPool)
        {
            if (obstacle.activeSelf && obstacle.transform.position.z < player.position.z - 10f)
            {
                obstacle.SetActive(false); // Desactívalo si está fuera del campo de visión
            }
        }
    }
}
