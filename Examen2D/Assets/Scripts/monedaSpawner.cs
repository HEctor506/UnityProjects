using UnityEngine;

public class monedaSpawner : MonoBehaviour
{
    public GameObject monedaPrefab; // Prefab de la moneda
    public float intervaloSpawn = 2f; // Intervalo de tiempo entre spawns
    public float rangoX = 10f; // Rango en el eje X donde se generarán las monedas

    private float tiempoSiguienteSpawn; // Tiempo para el próximo spawn

    void Start()
    {
        // Inicializar el tiempo para el primer spawn
        tiempoSiguienteSpawn = Time.time + intervaloSpawn;
    }

    void Update()
    {
        // Verificar si es hora de generar una nueva moneda
        if (Time.time >= tiempoSiguienteSpawn)
        {
            SpawnMoneda();
            // Configurar el tiempo para el siguiente spawn
            tiempoSiguienteSpawn = Time.time + intervaloSpawn;
        }
    }

    void SpawnMoneda()
    {
        // Generar una posición aleatoria en el eje X dentro del rango
        float posicionX = Random.Range(-rangoX, rangoX);
        Vector3 posicionSpawn = new Vector3(posicionX, transform.position.y, transform.position.z);

        // Instanciar la moneda en la posición generada
        Instantiate(monedaPrefab, posicionSpawn, Quaternion.identity);
    }
}
