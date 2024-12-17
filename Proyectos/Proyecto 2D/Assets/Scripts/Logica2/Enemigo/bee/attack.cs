using Unity.VisualScripting;
using UnityEngine;

public class attack : MonoBehaviour
{
    public Transform controladorDisparo;
    public float distanciaLinea;
    public LayerMask capaJugador;
    public bool jugadorEnRango;
    public float tiempoEntreDisparo;
    public float tiempoUltimoDisparo;
    public GameObject prefabBeeBullet;
    public float tiempoEsperaDisparo;

    void Update()
    {
        jugadorEnRango = Physics2D.Raycast(controladorDisparo.position, Vector2.down, distanciaLinea, capaJugador);

        if(jugadorEnRango)
        {
            if(Time.time > tiempoEntreDisparo + tiempoUltimoDisparo){
                tiempoUltimoDisparo = Time.time;
                Invoke(nameof(Disparar), tiempoEsperaDisparo);
            }
        }
        
    }

    private void Disparar()
    {
        Instantiate(prefabBeeBullet, controladorDisparo.position, controladorDisparo.rotation);

    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;   
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + Vector3.down * distanciaLinea);
          
    }
}
