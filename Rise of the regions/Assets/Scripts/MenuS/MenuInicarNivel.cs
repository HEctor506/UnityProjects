using UnityEngine;
using UnityEngine.EventSystems;

public class MenuInicarNivel : MonoBehaviour
{
    public GameObject popUP;
    public static string SelectedTag { get; private set; } // Guardamos el tag
    
    public void MostrarPopUp()
    {
        popUP.SetActive(true); //que es el canvas
    }

    public void OcultarPopUp()
    {
        popUP.SetActive(false); //que es el canvas
    }

    public void OnRegionButtonPressed()
    {
        GameObject pressedButton = EventSystem.current.currentSelectedGameObject;
        if (pressedButton != null)
        {
            SelectedTag = pressedButton.tag;
            Debug.Log($"Seleccionaste la región: {SelectedTag}");
            // Aquí abres el pop-up si quieres
        }
    }

    

}
