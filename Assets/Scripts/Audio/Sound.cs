using UnityEngine;
using UnityEngine.Audio;

public enum SoundCode
{
    NONE = -1,
    MAIN_MENU_BG,
    CONTINUE,
    GAME_OVER,
    SHOOT,
    SHOOT2,
    SHOOT3,
    COLLAPSE,
    COLLAPSE2,
    COLLAPSE3,
    READY,
    HURRY_UP,
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