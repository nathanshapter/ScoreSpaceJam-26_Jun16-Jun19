using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource musicSource, effectsSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else { Destroy(this.gameObject); }
    }

    public void PlaySound(AudioClip clip, bool loop = false)
    {
        if (loop)
        {
            effectsSource.clip = clip;
            effectsSource.loop = true;
            effectsSource.Play();
        }
        else
        {
            effectsSource.PlayOneShot(clip);
        }
    }

    public void StopSound(AudioClip clip)
    {
        effectsSource.Stop();
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}