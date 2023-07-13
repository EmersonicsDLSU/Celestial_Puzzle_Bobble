using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IPlayerRef
{
    [HideInInspector]public int _score { get; private set; }
    public int _scoreIncrement = 50;
    [HideInInspector]public float _currentLevelTime = 0.0f;
    [HideInInspector]public bool _isLose = false;
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
        // increment total time
        if (!_isLose)
            _currentLevelTime += Time.deltaTime;
    }
}
