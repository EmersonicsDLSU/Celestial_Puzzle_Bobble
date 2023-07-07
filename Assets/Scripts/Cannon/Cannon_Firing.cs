using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Firing : MonoBehaviour, ICannonRef
{
    [SerializeField]
    private GemSpawner _gemSpawner;
    [SerializeField]
    private Transform _cannonPos;

    public CannonRefs _cannonRef { get; private set; }
    
    [SerializeField] private float force = 20f;
    // functionality for spawning gem objects

    public void RefUpdate(CannonRefs mainRef)
    {
        // Fire a ball
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int random = Random.Range(0, (int)EGemBall.ENUM_SIZE);
            if (_gemSpawner.GetObjectPool((EGemBall) random) != null)
            {
                GemPool gem = _gemSpawner.GetObjectPool((EGemBall) random).GetObject();
                gem._gemBallRef.transform.position = _cannonPos.position;
                gem.Launch(transform.parent.transform.up, force);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"No Gem Spawner Assigned! {random}");
#endif
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
#if UNITY_EDITOR
                Debug.LogError($"No Gem Spawner Assigned! {random}");
#endif
            }
        }
    }
    public void SetCannonRef(CannonRefs _cannonRef)
    {
        this._cannonRef = _cannonRef;
    }

}
