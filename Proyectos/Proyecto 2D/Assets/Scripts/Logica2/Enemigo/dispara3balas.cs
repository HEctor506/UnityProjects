using UnityEngine;

public class dispara3balas : MonoBehaviour
{
    [SerializeField] private GameObject balaEnemigo; // Prefab de la bala
    [SerializeField] private Transform controladorDisparo; // Punto de salida de la bala
    [SerializeField] private float distanciaLinea = 5f; // Distancia para el raycast
    [SerializeField] private LayerMask capaJugador; // Capa del jugador
    [SerializeField] private float tiempoEntreDisparo = 1f; // Intervalo entre disparos
    [SerializeField] private float tiempoEsperaDisparo = 0.1f; // Delay antes de disparar
    [SerializeField] private float[] velocidadesBalas = { 12f, 10f, 8f }; // Velocidades de las balas

    private float tiempoUltimoDisparo;
    private bool jugadorEnRango;

    void Update()
    {
        // Raycast para detectar al jugador
        jugadorEnRango = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);

        if (jugadorEnRango)
        {
            if (Time.time > tiempoEntreDisparo + tiempoUltimoDisparo)
            {
                tiempoUltimoDisparo = Time.time;
                Invoke(nameof(Disparar), tiempoEsperaDisparo);
            }
        }
    }

    private void Disparar()
    {
        for (int i = 0; i < velocidadesBalas.Length; i++)
        {
            // Crear la bala
            GameObject bala = Instantiate(balaEnemigo, controladorDisparo.position, controladorDisparo.rotation);

            // Configurar la velocidad de la bala
            Bala balaScript = bala.GetComponent<Bala>();
            if (balaScript != null)
            {
                balaScript.velocidad = velocidadesBalas[i];
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaLinea);
    }
}
