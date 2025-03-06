using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] AudioSource sfxAudio, musicAudio;
    public AudioClip initialMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        sfxAudio = transform.GetChild(0).GetComponent<AudioSource>();
        musicAudio = transform.GetChild(1).GetComponent<AudioSource>();
        InitialPlayMusic(initialMusic);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicAudio.Stop();
        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true;
    }

    void InitialPlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicAudio.clip = clip;
            musicAudio.Play();
            musicAudio.loop = true;
        }
    }

    // Funciones para mutear música y efectos de sonido
    public void MuteMusic(bool mute)
    {
        musicAudio.mute = mute;
    }

    public void MuteSFX(bool mute)
    {
        sfxAudio.mute = mute;
    }

    // Funciones para alternar (toggle) el mute
    public void ToggleMuteMusic()
    {
        musicAudio.mute = !musicAudio.mute;
    }

    public void ToggleMuteSFX()
    {
        sfxAudio.mute = !sfxAudio.mute;
    }

    // Prueba de mute con teclas (puedes eliminar esto si no lo necesitas)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMuteMusic();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleMuteSFX();
        }
    }
}
