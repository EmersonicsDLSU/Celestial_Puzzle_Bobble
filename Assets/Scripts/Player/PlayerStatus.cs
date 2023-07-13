using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IPlayerRef
{
    [HideInInspector]public int _score { get; private set; }
    public int _scoreIncrement = 50;
    [HideInInspector] public float _currentLevelTime;
    [HideInInspector] public bool _isLose = false;

    private float _ceilingTimer;
    [SerializeField]private float _ceilingThreshold = 20.0f;

    private GameObject _ceilingGameObject;
    private Cannon_Firing _cannonFiring;
    [HideInInspector]public int _currentBaseRow = 0;
    void Start()
    {
        _cannonFiring = FindObjectOfType<Cannon_Firing>();
        if (_cannonFiring == null)
        {
            Debug.LogError("Missing 'UI_Settings' script!");
        }
        // get the reference of the ceiling
        foreach(Transform transform in FindObjectOfType<GameHandler>()._tileMapLevel.transform) {
            if(transform.CompareTag("Wall")) {
                _ceilingGameObject = transform.gameObject;
                break;
            }
        }
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

        // count down for the ceiling; count down only if the player is on idle or no real-time ball movement
        if (_cannonFiring._canShoot)
            _ceilingTimer += Time.deltaTime;
        if (_ceilingTimer >= _ceilingThreshold && _cannonFiring._canShoot)
        {
            _ceilingTimer = 0.0f;
            // lower the ceiling by '1'
            Vector3 newPosition = _ceilingGameObject.transform.position;
            newPosition.y -= 1.0f;
            _ceilingGameObject.transform.position = newPosition;
            FindAnyObjectByType<GameHandler>().MoveDownAllBalls();
            Debug.Log($"Lower Ceiling");
        }
    }
}
