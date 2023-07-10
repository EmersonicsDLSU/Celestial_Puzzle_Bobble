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