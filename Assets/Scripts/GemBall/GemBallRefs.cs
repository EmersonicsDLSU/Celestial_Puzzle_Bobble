using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBallRefs : MonoBehaviour
{
    private List<IGemBallRef> _componentList;
    // object's components
    [HideInInspector] public Rigidbody2D _rb;
    [HideInInspector] public CircleCollider2D _collider;
    [HideInInspector] public GemBall_Status _gemBallStatus;
    [HideInInspector] public GemPool _gemPoolSc;
    [HideInInspector] public GemBall_Collision _gemBallCollision;
    [HideInInspector] public GemBall_Connections _gemBallConnections;

    void Awake()
    {
        GetReferences();
        // add it to the component list
        _componentList = new List<IGemBallRef>();
        if (_gemBallStatus != null)
        {
            _componentList.Add(_gemBallStatus);
        }
        if (_gemPoolSc != null)
        {
            _componentList.Add(_gemPoolSc);
        }
        if (_gemBallCollision != null)
        {
            _componentList.Add(_gemBallCollision);
        }
        if (_gemBallConnections != null)
        {
            _componentList.Add(_gemBallConnections);
        }
    }

    private void GetReferences()
    {
        // gets the reference of the components
        _rb = GetComponentInChildren<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("Missing 'Rigidbody' script!");
        }

        _collider = GetComponentInChildren<CircleCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("Missing 'CircleCollider2D' script!");
        }

        _gemBallStatus = GetComponentInChildren<GemBall_Status>();
        if (_gemBallStatus == null)
        {
            Debug.LogError("Missing 'GemBall' script!");
        }
        else
        {
            _gemBallStatus.SetGemBallRef(this);
        }

        _gemPoolSc = GetComponentInChildren<GemPool>();
        if (_gemPoolSc == null)
        {
            Debug.LogError("Missing 'GemPool' script!");
        }
        else
        {
            _gemPoolSc.SetGemBallRef(this);
        }
        
        _gemBallCollision = GetComponentInChildren<GemBall_Collision>();
        if (_gemBallCollision == null)
        {
            Debug.LogError("Missing 'GemBall_Collision' script!");
        }
        else
        {
            _gemBallCollision.SetGemBallRef(this);
        }
        
        _gemBallConnections = GetComponentInChildren<GemBall_Connections>();
        if (_gemBallConnections == null)
        {
            Debug.LogError("Missing 'GemBall_Connections' script!");
        }
        else
        {
            _gemBallConnections.SetGemBallRef(this);
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
