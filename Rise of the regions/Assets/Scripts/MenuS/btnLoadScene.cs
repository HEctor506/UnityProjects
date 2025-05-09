using UnityEngine;

public class btnLoadScene : MonoBehaviour
{
    [SerializeField] private string nombreEscena;

    [Header("btn_clip")]
    [SerializeField] private AudioClip btn_sound;

    public void CargarEscena()
    {
        if (LevelLoader.Instancia != null)
        {
            LevelLoader.Instancia.CargarNivel(nombreEscena);
        }
        else
        {
            Debug.LogError("LevelLoader no encontrado en la escena.");
        }
    }

    public void CargarEscenaRegiones()
    {
        SoundManager.instance.PlaySFX(btn_sound);
        if (LevelLoader.Instancia != null)
        {
            LevelLoader.Instancia.CargarNivel();
        }
        else
        {
            Debug.LogError("LevelLoader no encontrado en la escena.");
        }
    }
}
