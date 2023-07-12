using UnityEngine;
using UnityEngine.Audio;
using System;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

    void Awake()
    {
        //SINGLETON DECLARATION
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);


        //INITIALIZATION FOR EACH SOUND
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void Start()
    {
        
    }

    public void Play(SoundCode code)
    {
        Sound s = Array.Find(sounds, sound => sound.code == code);
        if (s == null) return;
        s.source.Play();
    }

    public void Stop(SoundCode code)
    {
        Sound s = Array.Find(sounds, sound => sound.code == code);
        if (s == null) return;
        s.source.Stop();
    }

    

}