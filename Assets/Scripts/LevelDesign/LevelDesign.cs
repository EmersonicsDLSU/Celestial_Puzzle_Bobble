using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Design", menuName = "New Level Design")]
public class LevelDesign : ScriptableObject
{
    public TextAsset _txtStartingBalls;
    public GameObject _tileMapPrefab;
    public GameObject _playerShooterPrefab;
    public int _maxColumns, _maxRows;
}
