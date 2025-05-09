using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource voiceAudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }


    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void ChangeMusic(AudioClip nuevaMusica)
    {
        if (nuevaMusica == null)
            return;

        if (musicSource.clip == nuevaMusica)
            return; // Ya está sonando

        musicSource.clip = nuevaMusica;
        musicSource.Play();
    }

    public void PlayVoice(AudioClip audioClip, float pitch = 1f)
    {
        voiceAudioSource.pitch = pitch;
        voiceAudioSource.PlayOneShot(audioClip);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }


    //Codigo para poder reproducir EFECTOS de sonido desde cualquier script
    //Necesito como atributo un audioClip
    //SoundManager.instance.PlaySound(fireShootSound);

    /*PARA CAMBIAR LA MUSICA
    public AudioClip nuevaCancion; // Asignas en el Inspector

        void CambiarMusica()
        {
            SoundManager.instance.ChangeMusic(nuevaCancion);
        }
    */
    
}
