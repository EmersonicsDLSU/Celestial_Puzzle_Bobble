using UnityEngine;
using UnityEngine.Audio;

public enum SoundCode
{
    NONE = -1,
    CRY_BULBASAUR = 0,
    ENUM_SIZE
}

[System.Serializable]
public class Sound
{
    public SoundCode code = SoundCode.NONE;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1.0f;
    [Range (.1f, 3f)]
    public float pitch = 1.0f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
     
}