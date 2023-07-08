using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBall_Collision : MonoBehaviour, IGemBallRef
{
    public GemBallRefs _gemBallRef { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the trigger stayed ball was hit to a wall or to a ball
        if (!_gemBallRef._gemBallStatus._canAttach && (collision.gameObject.CompareTag("GemBall") || collision.gameObject.CompareTag("Wall")))
        {
            _gemBallRef._gemBallStatus.SetCanAttach(true);
            
            Debug.Log("Ball Hit!");
            GemBallRefs tempp = _gemBallRef;
        }
    }

    public void RefUpdate(GemBallRefs mainRef)
    {
        
    }
    public void SetGemBallRef(GemBallRefs _gemBallRef)
    {
        this._gemBallRef = _gemBallRef;
    }
}
