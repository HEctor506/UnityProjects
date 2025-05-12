using UnityEngine;

public class Configuraciones : MonoBehaviour
{
    public GameObject settingsCanvas;

    [Header("btn_clip")]
    [SerializeField] private AudioClip btn_sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsCanvas.SetActive(false);
    }

    
    public void mostrarMenu()
    {
        Time.timeScale = 0;
        SoundManager.instance.PlaySFX(btn_sound);
        settingsCanvas.SetActive(true);
    }

    public void closeMenu()
    {
        Time.timeScale = 1;
        SoundManager.instance.PlaySFX(btn_sound);
        settingsCanvas.SetActive(false);
    }

    public void OnExitClick()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
}
