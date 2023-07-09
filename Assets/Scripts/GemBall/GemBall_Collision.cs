using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBall_Collision : MonoBehaviour, IGemBallRef
{
    public GemBallRefs _gemBallRef { get; private set; }
    private const float _maxVerticalPos = 12.0f;
    private const float _maxHorizontalPosOdd = 9.75f;
    private const float _maxHorizontalPosEven = 10.25f;
    private const float _horizontalOffsetOdd = 0.75f;
    private const float _horizontalOffsetEven = 0.25f;

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
            // snap the ball to its new position
            SnapPosition();

            // switch the rigidBody to static
            _gemBallRef._rb.bodyType = RigidbodyType2D.Static;
            _gemBallRef._gemBallStatus.SetMobility(EGemBallMobility.STATIC);
            
        }
    }

    private void SnapPosition()
    {
        Vector3 ogPos = transform.position;
        Vector3 newPosition = Vector3.zero;
            
        // snap to the Y position
        newPosition.y = ogPos.y - Mathf.Floor(ogPos.y) < 0.5f ? Mathf.Floor(ogPos.y) : Mathf.Ceil(ogPos.y);
        if (newPosition.y > _maxVerticalPos) newPosition.y = _maxVerticalPos;
            
        float leftDiff = 0.0f, rightDiff = 0.0f, leftBound = 0.0f, rightBound = 0.0f;
        // snap to the X position
        if (newPosition.y % 2 == 0) // if even row
        {
            // verify the boundary of the current position x
            if (ogPos.x - Mathf.Floor(ogPos.x) < _horizontalOffsetEven) // lower-boundary
            {
                // compare difference from orig to left cell
                leftBound = (Mathf.Floor(ogPos.x - 1) + _horizontalOffsetEven);
                leftDiff = Mathf.Abs(ogPos.x - leftBound);
                // compare difference from orig to right cell
                rightBound = (Mathf.Floor(ogPos.x) + _horizontalOffsetEven);
                rightDiff = Mathf.Abs(ogPos.x - rightBound);
            }
            else // higher boundary
            {
                // compare difference from orig to left cell
                leftBound = (Mathf.Floor(ogPos.x) + _horizontalOffsetEven);
                leftDiff = Mathf.Abs(ogPos.x - leftBound);
                // compare difference from orig to right cell
                rightBound = (Mathf.Ceil(ogPos.x) + _horizontalOffsetEven);
                rightDiff = Mathf.Abs(ogPos.x - rightBound);
            }
                
            // position the ball to the nearest cell
            newPosition.x = leftDiff < rightDiff ? leftBound : rightBound;

            if (newPosition.x > _maxHorizontalPosEven) newPosition.x = _maxHorizontalPosEven;
            else if (newPosition.x < _horizontalOffsetEven) newPosition.x = _horizontalOffsetEven;
            //Debug.Log($"Compare Even: {leftDiff} -- {rightDiff}");
        }
        else // if odd row
        {
            // verify the boundary of the current position x
            if (ogPos.x - Mathf.Floor(ogPos.x) < _horizontalOffsetOdd) // lower-boundary
            {
                // compare difference from orig to left cell
                leftBound = (Mathf.Floor(ogPos.x - 1) + _horizontalOffsetOdd);
                leftDiff = Mathf.Abs(ogPos.x - leftBound); // 7.57 - 6.75
                // compare difference from orig to right cell
                rightBound = (Mathf.Floor(ogPos.x) + _horizontalOffsetOdd);
                rightDiff = Mathf.Abs(ogPos.x - rightBound); // 7.57 - 7.75
            }
            else // higher-boundary
            {
                // compare difference from orig to left cell
                leftBound = (Mathf.Floor(ogPos.x) + _horizontalOffsetOdd);
                leftDiff = Mathf.Abs(ogPos.x - leftBound);
                // compare difference from orig to right cell
                rightBound = (Mathf.Ceil(ogPos.x) + _horizontalOffsetOdd);
                rightDiff = Mathf.Abs(ogPos.x - rightBound);
            }
                
            // position the ball to the nearest cell
            newPosition.x = leftDiff < rightDiff ? leftBound : rightBound;
            
            if (newPosition.x > _maxHorizontalPosOdd) newPosition.x = _maxHorizontalPosOdd;
            else if (newPosition.x < _horizontalOffsetOdd) newPosition.x = _horizontalOffsetOdd;
            //Debug.Log($"Compare Even: {leftDiff} -- {rightDiff}");
        }
            
        // assign the new position for the ball; snap reposition
        transform.position = newPosition;

        //Debug.Log($"Ball Hit At: {ogPos}");
        //Debug.Log($"Ball Place At: {newPosition}");
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
