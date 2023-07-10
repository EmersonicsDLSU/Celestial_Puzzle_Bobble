using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Firing : MonoBehaviour, ICannonRef
{
    private GemSpawner _gemSpawner;
    [SerializeField]
    private Transform _cannonPos;

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
    }

    public void RefUpdate(CannonRefs mainRef)
    {
        // stop the process
        if (_gemSpawner == null) return;

        // Fire a ball
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int random = Random.Range(0, (int)EGemBall.ENUM_SIZE);
            if (_gemSpawner.GetObjectPool((EGemBall) random) != null)
            {
               // GemPool gem = _gemSpawner.GetObjectPool((EGemBall) random).GetObject();
                GemPool gem = _gemSpawner.GetObjectPool(EGemBall.YELLOW).GetObject();
                gem._gemBallRef.transform.position = _cannonPos.position;
                gem.Launch(transform.parent.transform.up, force);
            }
            else
            {
                Debug.LogError($"No Gem Spawner Assigned! {random}");
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
    public void SetCannonRef(CannonRefs _cannonRef)
    {
        this._cannonRef = _cannonRef;
    }

}
