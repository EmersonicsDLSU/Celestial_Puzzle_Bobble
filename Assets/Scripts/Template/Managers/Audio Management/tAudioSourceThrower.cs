using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class tAudioSourceThrower : MonoBehaviour
{
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private Transform _sourceLocation;
    [SerializeField] private GameObject _audioSourcePrefab;

    [HideInInspector] public List<tAudioSource> activeSource;

    private tObjectPool<tAudioSource> audioSourcePool;
    // Start is called before the first frame update
    void Start()
    {

        if (_spawnLocation == null || _sourceLocation == null)
            Debug.LogError("Missing one or more Transform requirement!");
        if (_audioSourcePrefab == null || _audioSourcePrefab.GetComponent<tAudioSource>() == null)
            Debug.LogError("Missing prefab or component!");
        else
        {
            audioSourcePool = new tObjectPool<tAudioSource>(AudioFactoryMethod, TurnOnAudioSource, TurnOffAudioSource, 10, true);
        }
    }
    private tAudioSource AudioFactoryMethod()
    {
        GameObject obj = Instantiate(_audioSourcePrefab) as GameObject;
        tAudioSource objScript = obj.GetComponent<tAudioSource>();
        // attach a reference of the objectPool to the pool object
        objScript.AssignObjectPool(audioSourcePool);

        obj.transform.parent = _sourceLocation;
        obj.gameObject.SetActive(false);

        return obj.GetComponent<tAudioSource>();
    }

    private void TurnOnAudioSource(tAudioSource audioSource)
    {
        // parent and reposition(displayed) the recently borrowed pool object
        audioSource.transform.parent = _spawnLocation;

        activeSource.Add(audioSource);
        audioSource.gameObject.SetActive(true);
    }
    private void TurnOffAudioSource(tAudioSource audioSource)
    {
        // parent and reposition(hidden) the recently borrowed pool object
        audioSource.transform.parent = _sourceLocation;

        activeSource.Remove(audioSource);
        audioSource.gameObject.SetActive(false);
    }

    public void ThrowAudio(Transform location, AudioInfo info)
    {
        tAudioSource audio = audioSourcePool.GetObject();
        TurnOnAudioSource(audio);

        audio._tag = info.tag;
        audio._type = info.type;
        audio._audioSource.clip = info.clip;
        audio._audioSource.mute = info.isMuted;
        audio._audioSource.loop = info.isLoop;
        audio._audioSource.priority = info.priority;
        audio._audioSource.volume = info.volume;
        audio._audioSource.pitch = info.pitch;
        audio._audioSource.spatialBlend = info.spatialBlend;

        audio.transform.position = location.position;
        audio._isPlaying = true;

        activeSource.Add(audio);

        audio._audioSource.Play();
    }

    public void ThrowAudio(AudioInfo info)
    {
        tAudioSource audio = audioSourcePool.GetObject();
        TurnOnAudioSource(audio);

        audio._tag = info.tag;
        audio._type = info.type;
        audio._audioSource.clip = info.clip;
        audio._audioSource.mute = info.isMuted;
        audio._audioSource.loop = info.isLoop;
        audio._audioSource.priority = info.priority;
        audio._audioSource.volume = info.volume;
        audio._audioSource.pitch = info.pitch;
        audio._audioSource.spatialBlend = info.spatialBlend;

        audio.transform.position = _sourceLocation.position;
        audio._isPlaying = true;


        audio._audioSource.Play();
    }

    public void StopAudioByTag(string tag)
    {
        foreach (tAudioSource audio in activeSource)
        {
            if (audio._tag == tag)
            {
                audio._tag = "None";
                audio._type = AudioInfoType.None;
                audio._audioSource.Stop();
                audio._audioSource.clip = null;
                audio._audioSource.mute = false;
                audio._audioSource.loop = false;
                audio._audioSource.priority = 0;
                audio._audioSource.volume = 0;
                audio._audioSource.pitch = 0;
                audio._audioSource.spatialBlend = 0;
                audio._isPlaying = false;

                audioSourcePool.ReturnObject(audio);
                TurnOffAudioSource(audio);
            }
        }

        //Debug.Log("Audio Source with tag " + tag + " not found!");
    }

    public void StopAudioByType(AudioInfoType type)
    {
        foreach (tAudioSource audio in activeSource)
        {
            if (audio._type == type)
            {
                audio._tag = "None";
                audio._type = AudioInfoType.None;
                audio._audioSource.Stop();
                audio._audioSource.clip = null;
                audio._audioSource.mute = false;
                audio._audioSource.loop = false;
                audio._audioSource.priority = 0;
                audio._audioSource.volume = 0;
                audio._audioSource.pitch = 0;
                audio._audioSource.spatialBlend = 0;
                audio._isPlaying = false;

                audioSourcePool.ReturnObject(audio);
                TurnOffAudioSource(audio);
            }
        }

        //Debug.Log("Audio Source with tag " + tag + " not found!");
    }

    void Update()
    {
        foreach (tAudioSource audio in activeSource)
        {
            if (audio._isPlaying && !audio._audioSource.isPlaying)
            {
                audio._tag = "None";
                audio._type = AudioInfoType.None;
                audio._audioSource.Stop();
                audio._audioSource.clip = null;
                audio._audioSource.mute = false;
                audio._audioSource.loop = false;
                audio._audioSource.priority = 0;
                audio._audioSource.volume = 0;
                audio._audioSource.pitch = 0;
                audio._audioSource.spatialBlend = 0;

                audioSourcePool.ReturnObject(audio);
                TurnOffAudioSource(audio);
                audio._isPlaying = false;
            }
        }
    }
}
