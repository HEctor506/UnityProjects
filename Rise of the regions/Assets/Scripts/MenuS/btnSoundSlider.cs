using UnityEngine;
using UnityEngine.UI;

public class BtnSoundSlider : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (SoundManager.instance == null) return;

        // Opcional: inicializar sliders con el volumen actual
        sfxSlider.value = SoundManager.instance.GetSFXVolume();
        musicSlider.value = SoundManager.instance.GetMusicVolume();

        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void SetSFXVolume(float volume)
    {
        SoundManager.instance.SetSFXVolume(volume);
    }

    private void SetMusicVolume(float volume)
    {
        SoundManager.instance.SetMusicVolume(volume);
    }
}
