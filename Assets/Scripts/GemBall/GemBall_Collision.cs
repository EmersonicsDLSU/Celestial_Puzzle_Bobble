using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBall_Collision : MonoBehaviour, IGemBallRef
{
    public GemBallRefs _gemBallRef { get; private set; }
    private const float _maxHorizontalPos = 8.0f;
    private const float _horizontalOffset = 0.5f;

    private Vector3 _lastVelocity;
        
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // for bounce reflect
        float speed = _lastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);

        _gemBallRef._rb.velocity = direction * Mathf.Max(speed, 0f);

        // Check if the trigger stayed ball was hit to a wall or to a ball
        if (!_gemBallRef._gemBallStatus._canAttach && (collision.gameObject.CompareTag("GemBall") || collision.gameObject.CompareTag("Wall")))
        {
            _gemBallRef._gemBallStatus.SetCanAttach(true);

            Vector3 newPosition = transform.localPosition;
            newPosition.x = Mathf.Round(transform.localPosition.x);
            newPosition.y = Mathf.Floor(transform.localPosition.y);
            
            // if odd row
            if (newPosition.y % 2 >= 1) {
                if (newPosition.x + _horizontalOffset >= _maxHorizontalPos)
                    newPosition.x = _maxHorizontalPos;
                else
                    newPosition.x += _horizontalOffset;
            } 
            else {
                if (newPosition.x >= _maxHorizontalPos)
                    newPosition.x = _maxHorizontalPos;
            }

            transform.localPosition = newPosition;

            _gemBallRef._rb.bodyType = RigidbodyType2D.Static;

            Debug.Log("Ball Hit!");

        }
    }

    public void RefUpdate(GemBallRefs mainRef)
    {
        if (_gemBallRef != null)
            _lastVelocity = _gemBallRef._rb.velocity;
    }
    public void SetGemBallRef(GemBallRefs _gemBallRef)
    {
        this._gemBallRef = _gemBallRef;
    }
}
