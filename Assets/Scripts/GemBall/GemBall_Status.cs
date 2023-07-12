using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBall_Status : MonoBehaviour, IGemBallRef
{
    [SerializeField]
    private EGemBall _gemID = EGemBall.NONE;

    private EGemBallMobility _gemMobility = EGemBallMobility.NONE;
    public GemBallRefs _gemBallRef { get; private set; }
    [HideInInspector] public bool _boundToBeDeleted = false;
    [HideInInspector] public bool _isVisitedForConnectivity = false;
    public class BallPosition {
        public int Row { get; set; }
        public int Col { get; set; }
    }

    public BallPosition position = new BallPosition();

    void Start()
    {

    }


    void Update()
    {
        
    }

    public EGemBall GetGemID()
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
