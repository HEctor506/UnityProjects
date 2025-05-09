using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    private int index;
    [SerializeField] private Image imagen;
    [SerializeField] private TextMeshProUGUI nombre;
    [SerializeField] private TMP_InputField inputNombre;
    private GameManager gameManager;

    [Header("btn_clip")]
    [SerializeField] private AudioClip btn_sound;

    private void Start()
    {
        gameManager = GameManager.Instance;

        index = PlayerPrefs.GetInt("JugadorIndex", 0);
        if(index >= gameManager.personajes.Count)
            index = 0;

        inputNombre.text = PlayerPrefs.GetString("JugadorNombre", "");

        CambiarPantalla();
    }

    private void CambiarPantalla()
    {
        imagen.sprite = gameManager.personajes[index].imagen;
        nombre.text = gameManager.personajes[index].nombre;
    }

    public void SiguentePersonaje()
    {
        SoundManager.instance.PlaySFX(btn_sound);
        if(index == gameManager.personajes.Count -1)
        {
            index = 0;
        }
        else{
            index +=1;
        }
        CambiarPantalla();
    }

    public void AnteriorPersonaje()
    {
        SoundManager.instance.PlaySFX(btn_sound);
        if(index == 0)
        {
            index = gameManager.personajes.Count -1;
        }
        else
        {
            index -=1;
        }
        
        CambiarPantalla();
    }

    public void GuardarSeleccion()
    {
        SoundManager.instance.PlaySFX(btn_sound);
        PlayerPrefs.SetInt("JugadorIndex", index);
        PlayerPrefs.SetString("JugadorNombre", inputNombre.text);
        PlayerPrefs.Save();

        // Si hay otro panel, forzar su actualización manual (opcional)
        var reflejo = Object.FindFirstObjectByType<ReflejoSeleccionPersonaje>();
        if (reflejo != null)
        {
            reflejo.ActualizarDatos();
        }
    }
}
