using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Personaje")]
public class Personajes : ScriptableObject 
{
    public GameObject peronajeJugable;
    public Sprite imagen;
    public string nombre;

}
