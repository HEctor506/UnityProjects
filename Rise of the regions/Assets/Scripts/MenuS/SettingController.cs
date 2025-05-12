using UnityEngine;

public class SettingController : MonoBehaviour
{
    public GameObject menuCanvas;


    [Header("btn_clip")]
    [SerializeField] private AudioClip btn_sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    
    public void mostrarMenu()
    {
        Time.timeScale = 0;
        SoundManager.instance.PlaySFX(btn_sound);
        menuCanvas.SetActive(true);
    }

    public void closeMenu()
    {
        Time.timeScale = 1;
        SoundManager.instance.PlaySFX(btn_sound);
        menuCanvas.SetActive(false);
    }

}
