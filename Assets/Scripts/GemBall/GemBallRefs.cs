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
    }

    private void GetReferences()
    {
        // gets the reference of the components
        _rb = GetComponentInChildren<Rigidbody2D>();
        if (_rb == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Missing 'Rigidbody' script!");
#endif
        }

        _collider = GetComponentInChildren<CircleCollider2D>();
        if (_collider == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Missing 'CircleCollider2D' script!");
#endif
        }

        _gemBallStatus = GetComponentInChildren<GemBall_Status>();
        if (_gemBallStatus == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Missing 'GemBall' script!");
#endif
        }
        else
        {
            _gemBallStatus.SetGemBallRef(this);
        }

        _gemPoolSc = GetComponentInChildren<GemPool>();
        if (_gemPoolSc == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Missing 'GemPool' script!");
#endif
        }
        else
        {
            _gemPoolSc.SetGemBallRef(this);
        }
        
        _gemBallCollision = GetComponentInChildren<GemBall_Collision>();
        if (_gemBallCollision == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Missing 'GemBall_Collision' script!");
#endif
        }
        else
        {
            _gemBallCollision.SetGemBallRef(this);
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
