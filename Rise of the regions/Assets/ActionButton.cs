using UnityEngine;

public class ActionButton : MonoBehaviour
{

    /// <summary>
    ///Necesito un gameobject persistente para el cambio escenas, su transicion, tener su script
    /// aunque tambien puedo tener un prefab del gameobject y ponerlo en las escenas donde necesito de su ayuda
    /// Modificar el codigo para determinar si es que quiero ir hacia adelante o hacia atras en las escenas
    /// </summary>

    public void OnPlayButtonPressed()
    {
        Debug.Log($"Se va a cargar el nivel de: {MenuInicarNivel.SelectedTag}");
        // Aquí cargas la escena dependiendo de SelectedTag
    }
    
    
}
