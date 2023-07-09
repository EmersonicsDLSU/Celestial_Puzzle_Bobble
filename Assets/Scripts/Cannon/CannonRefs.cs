using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRefs : MonoBehaviour
{ private List<ICannonRef> _componentList;
    // object's components
    [HideInInspector] public BoxCollider2D _collider;
    [HideInInspector] public Cannon_Status _cannonStatus;
    [HideInInspector] public Cannon_Collision _cannonCollision;
    [HideInInspector] public Cannon_Movement _cannonMovement;
    [HideInInspector] public Cannon_Firing _cannonFiring;

    void Awake()
    {
        GetReferences();

        // add it to the component list
        _componentList = new List<ICannonRef>();
        if (_cannonStatus != null)
        {
            _componentList.Add(_cannonStatus);
        }
        if (_cannonCollision != null)
        {
            _componentList.Add(_cannonCollision);
        }
        if (_cannonMovement != null)
        {
            _componentList.Add(_cannonMovement);
        }
        if (_cannonFiring != null)
        {
            _componentList.Add(_cannonFiring);
        }
    }

    private void GetReferences()
    {
        // gets the reference of the components
        _collider = GetComponentInChildren<BoxCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("Missing 'Collider2D' script!");
        }

        _cannonStatus = GetComponentInChildren<Cannon_Status>();
        if (_cannonStatus == null)
        {
            Debug.LogError("Missing 'Cannon_Status' script!");
        }
        else
        {
            _cannonStatus.SetCannonRef(this);
        }

        _cannonCollision = GetComponentInChildren<Cannon_Collision>();
        if (_cannonCollision == null)
        {
            Debug.LogError("Missing 'Cannon_Collision' script!");
        }
        else
        {
            _cannonCollision.SetCannonRef(this);
        }
        
        _cannonMovement = GetComponentInChildren<Cannon_Movement>();
        if (_cannonMovement == null)
        {
            Debug.LogError("Missing 'Cannon_Movement' script!");
        }
        else
        {
            _cannonMovement.SetCannonRef(this);
        }

        _cannonFiring = GetComponentInChildren<Cannon_Firing>();
        if (_cannonFiring == null)
        {
            Debug.LogError("Missing 'Cannon_Firing' script!");
        }
        else
        {
            _cannonFiring.SetCannonRef(this);
        }
    }


    void Update()
    {
        // controls the update of all components
        foreach (var comp in _componentList)
        {
            comp.RefUpdate(this);   
        }
    }
}
