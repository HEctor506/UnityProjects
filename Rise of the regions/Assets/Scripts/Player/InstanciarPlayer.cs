using Unity.Cinemachine;
using UnityEngine;

public class InstanciarPlayer : MonoBehaviour
{
    public SaveController saveController; // Asigna en Inspector
    private GameObject jugador;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    void Awake()
    {
        CinemachineConfiner2D confiner = FindAnyObjectByType<CinemachineConfiner2D>();
        Personajes personajeSeleccionado = GameManager.Instance.ObtenerPersonajeSeleccionado();

        PolygonCollider2D mapBoundary = confiner.BoundingShape2D as PolygonCollider2D;
        if(mapBoundary == null)
        {
            Debug.LogError("El boundingshape 2d no es polygonCollider");
        }

        Vector3 startPosition = mapBoundary.bounds.center;

        if (personajeSeleccionado != null && personajeSeleccionado.peronajeJugable != null)
        {
                jugador = Instantiate(
                personajeSeleccionado.peronajeJugable,
                startPosition,
                Quaternion.identity
            );

            jugador.tag = "Player";
            

        }
        else
        {
            Debug.LogError("No se pudo instanciar el personaje seleccionado.");
        }
        
        if(jugador != null)
        {
            cinemachineCamera.Follow = jugador.transform;
        }
        
        // Ahora sí puedes llamar LoadGame porque ya existe el personaje
        // saveController.LoadGame();
    }

    // void Start()
    // {
    //     string mapBoundary = FindAnyObjectByType<CinemachineConfiner2D>().BoundingShape2D.gameObject.name;

    // }
}
