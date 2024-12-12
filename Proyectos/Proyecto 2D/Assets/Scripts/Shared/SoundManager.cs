using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {get; private set;} //(SingleTons)which allows this instnace outsoude scripts
    private AudioSource source;
    
    public void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

}
