using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IPlayerRef
{
    [HideInInspector]public int _score { get; private set; }
    public int _scoreIncrement = 50;
    void Start()
    {
        
    }

    public void SetScore(int score)
    {
        _score = score;
        FindObjectOfType<UI_Player>().UpdateScore(_score);
    }

    public void RefUpdate(PlayerRefs mainRef)
    {

    }
}
