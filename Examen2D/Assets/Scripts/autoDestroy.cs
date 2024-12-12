using UnityEngine;

public class autoDestroy : MonoBehaviour
{
    public float tiempoDeVida = 3f; // Tiempo antes de destruir la moneda si no es recogida
    private float tiempoRestante;

    void Start()
    {
        // Inicializar el tiempo de vida
        tiempoRestante = tiempoDeVida;
    }

    void Update()
    {
        // Reducir el tiempo restante
        tiempoRestante -= Time.deltaTime;

        // Si el tiempo se agotó, destruir la moneda
        if (tiempoRestante <= 0)
        {
            Destroy(gameObject);
        }
    }
}
