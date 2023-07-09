using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBall_Collision : MonoBehaviour, IGemBallRef
{
    public GemBallRefs _gemBallRef { get; private set; }
    private const float _maxHorizontalPos = 8.25f;
    private const float _horizontalOffset = 0.75f;

    private Vector3 _lastVelocity;
        
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_gemBallRef._gemBallStatus.GetMobility() != EGemBallMobility.MOVING) return;

        // for bounce reflect
        float speed = _lastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);

        _gemBallRef._rb.velocity = direction * Mathf.Max(speed, 0f);

        // Check if the trigger stayed ball was hit to a wall or to a ball
        if (collision.gameObject.CompareTag("GemBall") || collision.gameObject.CompareTag("Wall"))
        {
            Vector3 ogPos = transform.position;
            Vector3 newPosition = ogPos;
            // snap the position
            Debug.Log($"{ogPos.x % 1f}");
            newPosition.x = Mathf.FloorToInt(ogPos.x);
            newPosition.y = Mathf.Floor(ogPos.y);
            
            // if odd row
            if (newPosition.y % 2 >= 1) {
                if (newPosition.x + _horizontalOffset >= _maxHorizontalPos)
                    newPosition.x = _maxHorizontalPos;
                else
                    newPosition.x += _horizontalOffset;
            } // if even row
            else {
                if (newPosition.x >= _maxHorizontalPos)
                    newPosition.x = _maxHorizontalPos;
            }

            // assign the new position for the ball; snap reposition
            transform.position = newPosition;

            // switch the rigidbody to static
            _gemBallRef._rb.bodyType = RigidbodyType2D.Static;
            _gemBallRef._gemBallStatus.SetMobility(EGemBallMobility.STATIC);

            Debug.Log($"Ball Hit At: {ogPos}");
            Debug.Log($"Ball Place At: {newPosition}");

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
