using UnityEngine;
using UnityEngine.Pool;

public class GemPool : MonoBehaviour, IGemBallRef
{
    public GemBallRefs _gemBallRef { get; private set; }
    
    
    public void Launch(Vector2 direction, float force)
    {
        _gemBallRef._rb.isKinematic = false;
        _gemBallRef._rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        _gemBallRef._gemBallStatus.SetMobility(EGemBallMobility.MOVING);
    }

    public void ReturnPool()
    {
        var g = FindObjectOfType<GameHandler>();
        GemBall_Status.BallPosition pos = _gemBallRef._gemBallStatus.position;
        if (pos.Row < 0 || pos.Col < 0 || g.gridRefs[pos.Row, pos.Col] == null) return; 
        // remove it from the gridsRef
        Debug.Log($"Delete: [{pos.Row}:{pos.Col}]");
        g.gridRefs[pos.Row, pos.Col] = null;
        FindObjectOfType<GemSpawner>().GetObjectPool(_gemBallRef._gemBallStatus.GetGemID()).ReturnObject(this);
    }

    public void RefUpdate(GemBallRefs mainRef)
    {

    }

    public void SetGemBallRef(GemBallRefs _gemBallRef)
    {
        this._gemBallRef = _gemBallRef;
    }
}