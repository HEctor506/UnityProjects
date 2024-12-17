using UnityEngine;

public class beeMovement : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float velocidadMovimiento;
    private int siguientePlataforma = 1;
    private bool ordenPlataformas = true; //para el orden en que se movera segun los puntos


    // Update is called once per frame
    void Update()
    {
        if (puntosMovimiento.Length < 2)
        {
            Debug.LogWarning("Debes asignar al menos dos puntos de movimiento en el inspector.");
            return; // Salir si no hay suficientes puntos
        }

        // Moverse hacia el siguiente punto
        transform.position = Vector2.MoveTowards(
            transform.position,
            puntosMovimiento[siguientePlataforma].position,
            velocidadMovimiento * Time.deltaTime
        );

        // Revisar si se alcanzó el punto objetivo
        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePlataforma].position) < 0.1f)
        {
            if (ordenPlataformas)
            {
                siguientePlataforma++;
                if (siguientePlataforma >= puntosMovimiento.Length)
                {
                    ordenPlataformas = false; // Cambiar dirección al llegar al último punto
                    siguientePlataforma -= 2; // Retroceder al penúltimo punto
                }
            }
            else
            {
                siguientePlataforma--;
                if (siguientePlataforma < 0)
                {
                    ordenPlataformas = true; // Cambiar dirección al llegar al primer punto
                    siguientePlataforma += 2; // Avanzar al segundo punto
                }
            }
        }
    }
}