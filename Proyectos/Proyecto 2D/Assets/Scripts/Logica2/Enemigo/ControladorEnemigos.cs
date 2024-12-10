using UnityEngine;
using System.Linq;

public class ControladorEnemigos : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;
    [SerializeField] private float tiempoEnemigos;
    private float tiempoSiguenteEnemigo;
    private int numeroEnemigos = 1;
 
    void Start()
    {
        maxX = puntos.Max(puntos => puntos.position.x);
        minX = puntos.Max(puntos => puntos.position.x);
        maxY = puntos.Max(puntos => puntos.position.y);
        minY = puntos.Max(puntos => puntos.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        tiempoSiguenteEnemigo += Time.deltaTime;
        if(numeroEnemigos <= 3){
            if(tiempoSiguenteEnemigo >= tiempoEnemigos)
            {
            tiempoSiguenteEnemigo = 0;
            CrearEnemigo();
            this.numeroEnemigos++;
            }
        }
    }

    private void CrearEnemigo()
    {
        int numeroEnemigo = Random.Range(0, enemigos.Length);
        Vector2 posicionAleatorioa = new Vector2(Random.Range(minX, maxX), Random.Range(maxY, minY));

        Instantiate(enemigos[numeroEnemigo], posicionAleatorioa, Quaternion.identity);
    }
}
