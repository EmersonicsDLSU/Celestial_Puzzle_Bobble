using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tAudioSource : MonoBehaviour
{
    //ComponentReference
    [HideInInspector] public AudioSource _audioSource;
    [HideInInspector] public string _tag;
    [HideInInspector] public AudioInfoType _type;
    [HideInInspector] public bool _isPlaying;

    private tObjectPool<tAudioSource> audioSourcePool;

    private void Awake()
    {
        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.rolloffMode = AudioRolloffMode.Linear;
        _audioSource.maxDistance = 5;
    }

    public void AssignObjectPool(tObjectPool<tAudioSource> objectPool)
    {
        audioSourcePool = objectPool;
    }
    
}
