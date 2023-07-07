using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBall_Status : MonoBehaviour, IGemBallRef
{
    [SerializeField]
    private EGemBall _gemID = EGemBall.NONE;
    public GemBallRefs _gemBallRef { get; private set; }
    public bool _canAttach { get; private set; } = false;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    EGemBall GetGemID()
    {
        return _gemID;
    }

    public void RefUpdate(GemBallRefs mainRef)
    {

    }
    public void SetGemBallRef(GemBallRefs _gemBallRef)
    {
        this._gemBallRef = _gemBallRef;
    }

    public void SetCanAttach(bool value)
    {
        _canAttach = value;
    }
}
