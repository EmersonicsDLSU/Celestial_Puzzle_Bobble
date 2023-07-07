using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Movement : MonoBehaviour, ICannonRef
{
    [SerializeField]
    private GameObject _cannon;

    public CannonRefs _cannonRef { get; private set; }
    
    public float rotationSpeed = 10f;
    // functionality for spawning gem objects

    public void RefUpdate(CannonRefs mainRef)
    {
        // Check if the A key is held down
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate the cannon to the left
            _cannon.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }

        // Check if the D key is held down
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate the cannon to the right
            _cannon.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
    }
    public void SetCannonRef(CannonRefs _cannonRef)
    {
        this._cannonRef = _cannonRef;
    }

}
