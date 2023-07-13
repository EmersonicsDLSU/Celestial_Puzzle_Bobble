using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Movement : MonoBehaviour, ICannonRef
{
    [SerializeField]
    private GameObject _cannon;

    public CannonRefs _cannonRef { get; private set; }
    public float _angleLimit = 80.0f;
    
    public float _rotationSpeed = 10f;
    private char _lastKey = '\0';

    private UI_Settings _uiSettings;

    void Start()
    {
        _uiSettings = FindObjectOfType<UI_Settings>();
        if (_uiSettings == null)
        {
            Debug.LogError("Missing 'UI_Settings' script!");
        }
    }

    // functionality for spawning gem objects
    public void RefUpdate(CannonRefs mainRef)
    {
        if (_uiSettings.IsPaused()) return;


        float z = _cannon.transform.rotation.eulerAngles.z;
        // check if the cannon rotation is within the valid direction
        bool isWithinRange = (z >= 0 && z <= _angleLimit) || (z <= 360 && z >= 360 - _angleLimit);
        if (!isWithinRange)
        {
            // a somewhat clamp function for the rotation
            if (_lastKey == 'A') 
                _cannon.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.FloorToInt(_cannon.transform.rotation.eulerAngles.z - 1));
            else if (_lastKey == 'D')
                _cannon.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.CeilToInt(_cannon.transform.rotation.eulerAngles.z + 1));
        }
        // Check if the A key or the left arrow key is pressed, and if it's within the desired range
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && isWithinRange)
        {
            _lastKey = 'A';
            // Rotate the cannon to the left
            _cannon.transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && isWithinRange)
        {
            _lastKey = 'D';
            // Rotate the cannon to the right
            _cannon.transform.Rotate(0f, 0f, -_rotationSpeed * Time.deltaTime);
        }
    }




    public void SetCannonRef(CannonRefs _cannonRef)
    {
        this._cannonRef = _cannonRef;
    }

}
