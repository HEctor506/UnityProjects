using UnityEngine;
using System.Linq;

public class ControladorEnemigos : MonoBehaviour
{
    [SerializeField] private Transform[] puntosEtapa1; // Puntos para la primera etapa
    [SerializeField] private Transform[] puntosEtapa2; // Puntos para la segunda etapa
    [SerializeField] private Transform[] puntosEtapa3; // Puntos para la tercera etapa

    [SerializeField] private GameObject[] enemigos;
    [SerializeField] private float[] tiemposEntreSpawns = { 5f, 3f, 3f }; // Tiempos por etapa
    public int[] plazos = { 3, 5, 7 }; // Cantidad de enemigos por etapa

    private float tiempoSiguienteEnemigo;
    private int enemigosGenerados = 0;  // Contador de enemigos generados
    private int etapaActual = 0;
    private float tiempoCambioEtapa;
    public AudioClip lastCurtain;

    private bool sonidoReproducido = false; // Variable para asegurar que el sonido se reproduce solo una vez

    void Update()
    {
        tiempoSiguienteEnemigo += Time.deltaTime;
        tiempoCambioEtapa += Time.deltaTime;

        if (tiempoCambioEtapa >= 20f) // Cambiar de etapa después de 15 segundos
        {
            if (etapaActual < plazos.Length - 1)
            {
                etapaActual++;
                enemigosGenerados = 0; // Reiniciar el contador de enemigos generados para la nueva etapa
                tiempoCambioEtapa = 0; // Reiniciar el tiempo de cambio de etapa

                // Si es la tercera etapa, reproducir el sonido una sola vez
                if (etapaActual == 2 && !sonidoReproducido)
                {
                    SoundManager.instance.PlaySound(lastCurtain);
                    sonidoReproducido = true; // Marcar que el sonido ya ha sido reproducido
                }
            }
        }

        if (enemigosGenerados < plazos[etapaActual] && tiempoSiguienteEnemigo >= tiemposEntreSpawns[etapaActual])
        {
            tiempoSiguienteEnemigo = 0;
            CrearEnemigo();
            enemigosGenerados++;
        }
    }

    private void CrearEnemigo()
    {
        int numeroEnemigo = Random.Range(0, enemigos.Length); // Seleccionar enemigo aleatorio

        // Elegir punto aleatorio dentro del arreglo de puntos de la etapa actual
        Transform[] puntos = GetPuntosEtapaActual();
        Transform puntoAleatorio = puntos[Random.Range(0, puntos.Length)];

        // Instanciar enemigo en el punto elegido
        Instantiate(enemigos[numeroEnemigo], puntoAleatorio.position, Quaternion.identity);
    }

    private Transform[] GetPuntosEtapaActual()
    {
        switch (etapaActual)
        {
            case 0:
                return puntosEtapa1;
            case 1:
                return puntosEtapa2;
            case 2:
                return puntosEtapa3;
            default:
                return puntosEtapa1;
        }
    }
}
