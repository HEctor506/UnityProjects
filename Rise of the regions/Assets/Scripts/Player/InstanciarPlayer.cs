using Unity.Cinemachine;
using UnityEngine;

public class InstanciarPlayer : MonoBehaviour
{
    public SaveController saveController; // Asigna en Inspector
    private GameObject jugador;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    void Awake()
    {
        Personajes personajeSeleccionado = GameManager.Instance.ObtenerPersonajeSeleccionado();

        if (personajeSeleccionado != null && personajeSeleccionado.peronajeJugable != null)
        {
                jugador = Instantiate(
                personajeSeleccionado.peronajeJugable,
                Vector3.zero,
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
        saveController.LoadGame();
    }

    // void Start()
    // {
    //     playerMovement jugadorScript = jugador.GetComponent<playerMovement>();
    //     jugadorScript.movementJoystick 

    // }
}
