using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tAudioManager : tSingleton<tAudioManager>
{
    [HideInInspector] public List<AudioInfo> Ambience;
    [HideInInspector] public List<AudioInfo> SFX;
    [HideInInspector] public List<AudioInfo> VoiceOver;
    [HideInInspector] public List<AudioInfo> BGM;
    [HideInInspector] public List<AudioInfo> UI;

    private tAudioSourceThrower throwerRef;

    protected override void Awake()
    {
        base.Awake();

        tAudioInfo TAudioInfo;

        if (this.gameObject.GetComponentInChildren<tAudioInfo>())
        {
            TAudioInfo = this.gameObject.GetComponentInChildren<tAudioInfo>();
            SortAudio(TAudioInfo.tAudioInfoList);
        }
        else
        {
            Debug.LogWarning("Audio List not found!");
        }

        if (this.gameObject.GetComponentInChildren<tAudioSourceThrower>())
        {
            throwerRef = this.gameObject.GetComponentInChildren<tAudioSourceThrower>();
        }
        else
        {
            Debug.LogWarning("Audio Thrower not found!");
        }

    }

    void SortAudio(List<AudioInfo> list)
    {
        foreach (AudioInfo audio in list)
        {
            if (audio.type == AudioInfoType.Ambience)
                Ambience.Add(audio);
            else if (audio.type == AudioInfoType.SFX)
                SFX.Add(audio);
            else if (audio.type == AudioInfoType.VoiceOver)
                VoiceOver.Add(audio);
            else if (audio.type == AudioInfoType.BGM)
                BGM.Add(audio);
            else if (audio.type == AudioInfoType.UI)
                UI.Add(audio);
        }
    }

    public void StopAudioByTag(string tag)
    {
        throwerRef.StopAudioByTag(tag);
    }

    public void StopAudioByType(AudioInfoType type)
    {
        throwerRef.StopAudioByType(type);
    }

    #region Ambience

    public void playAmbienceByName(string name)
    {
        foreach (AudioInfo Ambience in Ambience)
        {
            if (Ambience.name == name)
            {
                //throw sound
                throwerRef.ThrowAudio(Ambience);
                return;
            }
        }
    }

    public void playAmbienceByName(string name, Transform Ambience_Loc)
    {
        foreach (AudioInfo Ambience in Ambience)
        {
            if (Ambience.name == name)
            {
                //throw sound
                throwerRef.ThrowAudio(Ambience_Loc, Ambience);
                return;
            }
        }
    }

    public void playAmbienceByTag(string tag)
    {
        foreach (AudioInfo Ambience in Ambience)
        {
            if (Ambience.tag == tag)
            {
                //throw sound
                throwerRef.ThrowAudio(Ambience);
                return;
            }
        }
    }

    public void playAmbienceByTag(string tag, Transform Ambience_Loc)
    {
        foreach (AudioInfo Ambience in Ambience)
        {
            if (Ambience.tag == tag)
            {
                //throw sound
                throwerRef.ThrowAudio(Ambience_Loc, Ambience);
                return;
            }
        }
    }

    #endregion

    #region SFX

    public void playSFXByName(string name, Transform SFX_Loc)
    {
        foreach (AudioInfo SFX in SFX)
        {
            if (SFX.name == name)
            {
                //throw sound
                throwerRef.ThrowAudio(SFX_Loc, SFX);
                Debug.Log("Thrown");
                return;
            }
        }
    }

    public void playSFXByTag(string tag, Transform SFX_Loc)
    {
        foreach (AudioInfo SFX in SFX)
        {
            if (SFX.tag == tag)
            {
                //throw sound
                throwerRef.ThrowAudio(SFX_Loc, SFX);
                return;
            }
        }
    }

    #endregion

    #region VoiceOver

    public void playVOByName(string name)
    {
        foreach (AudioInfo VO in VoiceOver)
        {
            if (VO.name == name)
            {
                //throw sound
                throwerRef.ThrowAudio(VO);
                return;
            }
        }
    }

    public void playVOByTag(string tag)
    {
        foreach (AudioInfo VO in VoiceOver)
        {
            if (VO.tag == tag)
            {
                //throw sound
                throwerRef.ThrowAudio(VO);
                return;
            }
        }
    }

    #endregion

    #region BGM

    public void playBGMByName(string name)
    {
        foreach (AudioInfo BGM in BGM)
        {
            if (BGM.name == name)
            {
                //throw sound
                Debug.Log("Thrown");
                throwerRef.ThrowAudio(BGM);
                return;
            }
        }
    }

    public void playBGMByTag(string tag)
    {
        foreach (AudioInfo BGM in BGM)
        {
            if (BGM.tag == tag)
            {
                //throw sound
                throwerRef.ThrowAudio(BGM);
                return;
            }
        }
    }

#endregion

    #region UI

    public void playUIByName(string name)
    {
        foreach (AudioInfo UI in UI)
        {
            if (UI.name == name)
            {
                //throw sound
                throwerRef.ThrowAudio(UI);
                return;
            }
        }
    }

    public void playUIByTag(string tag)
    {
        foreach (AudioInfo UI in UI)
        {
            if (UI.tag == tag)
            {
                //throw sound
                throwerRef.ThrowAudio(UI);
                return;
            }
        }
    }

    #endregion
    
}
