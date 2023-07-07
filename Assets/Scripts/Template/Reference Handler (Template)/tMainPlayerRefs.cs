using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creates a sharing system among the components attached to this object
public class tMainPlayerRefs : MonoBehaviour
{
    private List<tIMPRefs> _componentList;
    // object's components
    [HideInInspector] public tPlayerMovement _playerMovementSc;
    //[HideInInspector] public Inventory _playerInventory;
    
    
    void Awake()
    {
        // gets the reference of the components
        if (GetComponentInChildren<tPlayerMovement>() != null) _playerMovementSc = GetComponentInChildren<tPlayerMovement>();
        else Debug.LogError("Missing 'PlayerMovement' script!");
        /*
        if (GetComponentInChildren<Inventory>() != null) _playerInventory = GetComponentInChildren<PlayerMovement>();
        else Debug.LogError("Missing 'Inventory' script!");
        */

        // add it to the component list
        _componentList = new List<tIMPRefs>();
        _componentList.Add(_playerMovementSc);
        //_componentList.Add(_playerInventory);
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
