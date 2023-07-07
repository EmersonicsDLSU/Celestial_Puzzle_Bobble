using UnityEngine;
using UnityEngine.Pool;

public class GemPool : MonoBehaviour, IGemBallRef
{
    private ObjectPooling<GemPool> _objectPool;
    public GemBallRefs _gemBallRef { get; private set; }

    public void AssignObjectPool(ObjectPooling<GemPool> objectPool)
    {
        _objectPool = objectPool;
    }
    
    public void Launch(Vector2 direction, float force)
    {
        _gemBallRef._rb.isKinematic = false;
        _gemBallRef._rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = collision.transform.rotation;
        Vector3 position = contact.point;

        _objectPool.ReturnObject(this);
    }

    public void RefUpdate(GemBallRefs mainRef)
    {

    }

    public void SetGemBallRef(GemBallRefs _gemBallRef)
    {
        this._gemBallRef = _gemBallRef;
    }
}