using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Para mostrar el timer en pantalla

public class bombSpawner : MonoBehaviour
{
    public GameObject bombPrefab; // Prefab de la bomba
    public float initialSpawnRate = 1.5f; // Tiempo inicial entre spawns
    public float spawnRateDecrease = 0.3f; // Disminución en tiempo de spawn
    public float minX; // Límite izquierdo de la pantalla
    public float maxX; // Límite derecho de la pantalla
    public float spawnHeight = 10f; // Altura desde la cual caen las bombas
    public Text timerText; // Referencia al texto del timer en pantalla
    public int initialTime = 30; // Tiempo inicial en segundos

    private float currentSpawnRate;
    private int timer;

    private void Start()
    {
        currentSpawnRate = initialSpawnRate;
        timer = initialTime;

        // Inicia el temporizador y la generación de bombas
        StartCoroutine(TimerCountdown());
        StartCoroutine(SpawnBombs());
    }

    private IEnumerator SpawnBombs()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnRate);

            // Generar posición aleatoria dentro de los límites
            float randomX = Random.Range(minX, maxX);
            Vector2 spawnPosition = new Vector2(randomX, spawnHeight);

            // Crear la bomba en la posición aleatoria
            Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator TimerCountdown()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);

            // Reducir el tiempo y actualizar el texto del temporizador
            timer--;
            UpdateTimerText();
        }

        // Aumentar la dificultad al llegar a 0
        IncreaseDifficulty();
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + timer;
        }
    }

    private void IncreaseDifficulty()
    {
        // Aumentar la velocidad de generación al reducir el tiempo entre spawns
        currentSpawnRate = Mathf.Max(0.5f, currentSpawnRate - spawnRateDecrease);

        // Reinicia el temporizador para otra cuenta regresiva
        timer = initialTime;
        StartCoroutine(TimerCountdown());
    }
}
