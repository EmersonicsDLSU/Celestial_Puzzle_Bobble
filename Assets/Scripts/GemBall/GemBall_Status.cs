using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBall_Status : MonoBehaviour, IGemBallRef
{
    [SerializeField]
    private EGemBall _gemID = EGemBall.NONE;

    private EGemBallMobility _gemMobility = EGemBallMobility.NONE;
    public GemBallRefs _gemBallRef { get; private set; }

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

    public void SetMobility(EGemBallMobility mobility)
    {
        _gemMobility = mobility;
    }

    public EGemBallMobility GetMobility()
    {
        return _gemMobility;
    }
}
