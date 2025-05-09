using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Singletoon
    public List<Personajes> personajes;

    private void Awake()
    {
        if(GameManager.Instance == null){
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public Personajes ObtenerPersonajeSeleccionado()
    {
        int index = PlayerPrefs.GetInt("JugadorIndex", 0);
        if (index >= personajes.Count) index = 0;
        return personajes[index];
    }

    public string ObtenerNombreJugador()
    {
        return PlayerPrefs.GetString("JugadorNombre", "");
    }


}
