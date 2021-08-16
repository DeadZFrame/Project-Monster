using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    private static bool soundMuted = false;
    public static AudioManager instance;
    public Sound[] sounds;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
        }
    }

    public void Play(string name)
    {
        if (!soundMuted)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Your sound named " + name + " does not exits");
                return;
            }
            s.audioSource.Play();
        }

    }
    public void Stop(string name)
    {
        if (!soundMuted)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Your sound named " + name + " does not exits");
                return;
            }
            s.audioSource.Stop();
        }

    }
}
