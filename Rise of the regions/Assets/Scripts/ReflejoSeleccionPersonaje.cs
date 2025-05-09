using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReflejoSeleccionPersonaje : MonoBehaviour
{
    [SerializeField] private Image imagenPersonaje;
    [SerializeField] private TextMeshProUGUI nombrePersonaje;

    private void Start()
    {
        ActualizarDatos();
    }

    public void ActualizarDatos()
    {
        if (GameManager.Instance == null) return;

        var personaje = GameManager.Instance.ObtenerPersonajeSeleccionado();
        imagenPersonaje.sprite = personaje.imagen;
        nombrePersonaje.text = GameManager.Instance.ObtenerNombreJugador();
    }
}
