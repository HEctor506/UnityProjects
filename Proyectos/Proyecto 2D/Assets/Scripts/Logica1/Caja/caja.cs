using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class caja : MonoBehaviour
{
    [SerializeField] private GameObject cajaRota;
    [SerializeField] private GameObject[] objetosAInstanciar;  // Arreglo de prefabs a instanciar
    [SerializeField] private float distanciaMaxima = 5f; // Distancia máxima en el eje X donde se pueden instanciar los objetos
    [SerializeField] private int cantidadObjetos = 3; // Cantidad de objetos a instanciar

    private List<float> posicionesXUsadas = new List<float>(); // Lista para llevar un control de las posiciones X utilizadas

    public void Destruir()
    {
        // Instanciamos la explosión de la caja rota
        GameObject explosion = Instantiate(cajaRota, transform.position, Quaternion.identity);

        // Aplicamos una fuerza aleatoria a los fragmentos
        foreach (Transform fragmento in explosion.transform)
        {
            Rigidbody2D rb = fragmento.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Random.insideUnitCircle * 5f, ForceMode2D.Impulse);
            }
        }

        // Destruimos la caja original
        Destroy(gameObject);

        // Instanciamos los objetos adicionales en posiciones aleatorias a lo largo del eje X
        InstanciarObjetosAleatorios();
    }

    private void InstanciarObjetosAleatorios()
    {
        for (int i = 0; i < cantidadObjetos; i++)
        {
            // Elegir un prefab aleatorio
            GameObject objetoInstanciado = objetosAInstanciar[Random.Range(0, objetosAInstanciar.Length)];

            // Elegir una posición X aleatoria que no haya sido utilizada
            float posicionX = ObtenerPosicionXValida();

            // Instanciar el objeto en la posición deseada
            Instantiate(objetoInstanciado, new Vector3(transform.position.x + posicionX, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    private float ObtenerPosicionXValida()
    {
        float posicionX;
        do
        {
            // Generar una posición aleatoria entre -distanciaMaxima y +distanciaMaxima
            posicionX = Random.Range(-distanciaMaxima, distanciaMaxima);
        }
        while (posicionesXUsadas.Contains(posicionX)); // Asegurarse de que la posición no se haya usado previamente

        // Agregar la posición a la lista de posiciones usadas
        posicionesXUsadas.Add(posicionX);

        return posicionX;
    }
    
}
