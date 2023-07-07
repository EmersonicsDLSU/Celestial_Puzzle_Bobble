using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public enum AudioInfoType
{
    None = 0,
    Ambience,
    SFX,
    VoiceOver,
    BGM,
    UI
}

[System.Serializable]
public struct AudioInfo
{
    public string name;
    public string tag;
    public AudioClip clip;
    public AudioInfoType type;
    public bool isMuted;
    public bool playOnAwake;
    public bool isLoop;
    [Range(0, 256)]
    public int priority;
    [Range(0f, 1.0f)]
    public float volume;
    [Range(0f, 2.0f)]
    public float pitch;
    [Range(0f, 1.0f)]
    public float spatialBlend;
}

public class tAudioInfo : MonoBehaviour
{
    public List<AudioInfo> tAudioInfoList;
}