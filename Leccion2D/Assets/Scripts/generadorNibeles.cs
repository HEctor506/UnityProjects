using UnityEngine;

public class generadorNibeles : MonoBehaviour
{
    [SerializeField] private GameObject[] PartesNivel;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private Transform PuntoFinal;
    [SerializeField] private int cantidadInicial;
    private Transform jugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 0; i < cantidadInicial ; i++){
            generarParteNivel();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(jugador.position, PuntoFinal.position) < distanciaMinima){
            generarParteNivel();

        }
        
    }

    private void generarParteNivel()
    {
        int numeroAleatorio = Random.Range(0, PartesNivel.Length);
        GameObject nivel = Instantiate(PartesNivel[numeroAleatorio], PuntoFinal.position, Quaternion.identity);
        PuntoFinal = BuscarPuntoFinal(nivel, "PuntoFinal");
   
    }

    private Transform BuscarPuntoFinal(GameObject parteNivel, string etiqueta)
    {
        Transform punto = null;

        foreach(Transform ubicacion in parteNivel.transform)
        {
            if(ubicacion.CompareTag(etiqueta)){
                punto = ubicacion;
                break;
            }

        }

        return punto;
    }    
}
