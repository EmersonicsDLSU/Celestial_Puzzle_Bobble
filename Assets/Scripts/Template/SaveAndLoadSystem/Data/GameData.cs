using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public float _longestTimeSurvived;
    public int _highScore;
    public int _currency;

    public SerializableDictionary<int, int> powerupLevels;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        _longestTimeSurvived = 0.0f;
        _highScore = 0;
        _currency = 0;
        powerupLevels = new SerializableDictionary<int, int>();
    }

}