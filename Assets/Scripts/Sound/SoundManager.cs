using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private  AudioSource musicSource;
    [SerializeField] private  AudioSource ambientSoundSource;

    
    public AudioClip ambientSound;
    public AudioClip musicSound;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        PlayMusic(musicSound);
        PlayAmbient(ambientSound);
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip music, bool loop = true)
    {
        musicSource.clip = music;
        musicSource.loop = loop;
        musicSource.Play();
    }
    
    public void PlayAmbient(AudioClip music, bool loop = true)
    {
        ambientSoundSource.clip = music;
        ambientSoundSource.loop = loop;
        ambientSoundSource.Play();
    }
}
