using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRefs : MonoBehaviour
{
    private List<IPlayerRef> _componentList;
    // object's components
    [HideInInspector] public PlayerStatus _playerStatus;
    // Start is called before the first frame update
    void Awake()
    {
        
        GetReferences();

        // add it to the component list
        _componentList = new List<IPlayerRef>();
        if (_playerStatus != null)
        {
            _componentList.Add(_playerStatus);
        }
    }

    private void GetReferences()
    {
        // gets the reference of the components
        _playerStatus = GetComponentInChildren<PlayerStatus>();
        if (_playerStatus == null)
        {
            Debug.LogError("Missing 'PlayerStatus' script!");
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
