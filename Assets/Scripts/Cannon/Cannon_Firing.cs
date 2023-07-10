using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Firing : MonoBehaviour, ICannonRef
{
    private GemSpawner _gemSpawner;
    [SerializeField]
    private Transform _loadedBallPos;
    [SerializeField]
    private Transform _nextBallPos;
    private GemBallRefs _loadedBall, _nextBall;
    private GameHandler _gameHandler;

    public CannonRefs _cannonRef { get; private set; }
    
    [SerializeField] private float force = 20f;
    // functionality for spawning gem objects

    void Start()
    {
        _gemSpawner = FindObjectOfType<GemSpawner>();
        if (_gemSpawner == null)
        {
            Debug.LogError("Missing 'GemSpawner' script!");
        }
        _gameHandler = FindObjectOfType<GameHandler>();
        if (_gemSpawner == null)
        {
            Debug.LogError("Missing 'GameHandler' script!");
        }
        else
        {
            // load the balls
            _gameHandler.CheckExistingBallTypes();
            // random pick based on the existing balls in the grid; for the loadedBall
            int random = Random.Range(0, _gameHandler._existingBallTypes.Count);
            _loadedBall = _gemSpawner.GetObjectPool((EGemBall) random).GetObject()._gemBallRef;
            _loadedBall.transform.position = _loadedBallPos.position;
            _loadedBall.transform.parent = _loadedBallPos;
            // random pick based on the existing balls in the grid; for the nextBall
            random = Random.Range(0, _gameHandler._existingBallTypes.Count);
            _nextBall = _gemSpawner.GetObjectPool((EGemBall) random).GetObject()._gemBallRef;
            _nextBall.transform.position = _nextBallPos.position;
            _nextBall.transform.parent = _nextBallPos;

        }
    }

    private void LoadNewBall()
    {
        // load the balls
        _gameHandler.CheckExistingBallTypes();
        // swap nextBall to loadedBall
        _loadedBall = _nextBall;
        _loadedBall.transform.position = _loadedBallPos.position;
        _loadedBall.transform.parent = _loadedBallPos;
        // random pick based on the existing balls in the grid; for the nextBall
        int random = Random.Range(0, _gameHandler._existingBallTypes.Count);
        _nextBall = _gemSpawner.GetObjectPool((EGemBall) random).GetObject()._gemBallRef;
        _nextBall.transform.position = _nextBallPos.position;
        _nextBall.transform.parent = _nextBallPos;
    }

    float _timer = 0.0f, _threshold = 1.0f;
    public void RefUpdate(CannonRefs mainRef)
    {
        // stop the process
        if (_gemSpawner == null) return;

        _timer += Time.deltaTime;

        // Fire a ball
        if (Input.GetKeyDown(KeyCode.Space) && _timer > _threshold)
        {
            _timer = 0.0f;
            //
            if (_gemSpawner.GetObjectPool(_loadedBall._gemBallStatus.GetGemID()) != null)
            {
                StartCoroutine(DelayShot());
            }
            else
            {
                Debug.LogError($"No Gem Spawner Assigned! {_loadedBall._gemBallStatus.GetGemID()}");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            int random = Random.Range(0, (int)EGemBall.ENUM_SIZE);
            if (_gemSpawner.GetObjectPool((EGemBall) random) != null)
            {
                _gemSpawner.GetObjectPool((EGemBall) random).ReturnObject();
            }
            else
            {
                Debug.LogError($"No Gem Spawner Assigned! {random}");
            }
        }
    }
    
    private float delayTime = 0.5f; // Delay time in seconds
    IEnumerator DelayShot()
    {
        // shoot the loadedBall
        GemPool gem = _loadedBall._gemPoolSc;
        gem._gemBallRef.transform.position = _loadedBallPos.position;
        gem._gemBallRef.transform.parent = _gemSpawner._sourceLocation;
        gem.Launch(transform.parent.transform.up, force);

        // a delay to avoid collision
        yield return new WaitForSeconds(delayTime);
        LoadNewBall();
    }
    public void SetCannonRef(CannonRefs _cannonRef)
    {
        this._cannonRef = _cannonRef;
    }

}
