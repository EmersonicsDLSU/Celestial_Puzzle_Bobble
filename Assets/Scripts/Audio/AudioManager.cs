using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Mono.Cecil.Cil;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

    private List<Sound> _shootCollection = new List<Sound>();
    private List<Sound> _collapseCollection = new List<Sound>();

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

        _shootCollection.Add(Array.Find(sounds, sound => sound.code == SoundCode.SHOOT));
        _shootCollection.Add(Array.Find(sounds, sound => sound.code == SoundCode.SHOOT2));
        _shootCollection.Add(Array.Find(sounds, sound => sound.code == SoundCode.SHOOT3));

        _collapseCollection.Add(Array.Find(sounds, sound => sound.code == SoundCode.COLLAPSE));
        _collapseCollection.Add(Array.Find(sounds, sound => sound.code == SoundCode.COLLAPSE2));
        _collapseCollection.Add(Array.Find(sounds, sound => sound.code == SoundCode.COLLAPSE3));
    }

    void Start()
    {
        Play(SoundCode.MAIN_MENU_BG);
        Play(SoundCode.READY);
    }

    public void Play(SoundCode code)
    {
        Sound s = Array.Find(sounds, sound => sound.code == code);
        if (s == null) return;
        s.source.Play();
        //Debug.Log($"Play: {code}");
    }

    public void PlayShootSFX()
    {
        int random = Random.Range(0, _shootCollection.Count);
        Sound s = _shootCollection[random];
        if (s == null) return;
        s.source.Play();
    }

    public void PlayCollapseSFX()
    {
        int random = Random.Range(0, _collapseCollection.Count);
        Sound s = _collapseCollection[random];
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