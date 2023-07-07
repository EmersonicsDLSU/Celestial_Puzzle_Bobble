using UnityEngine;
using UnityEngine.Pool;

public class tBullet : MonoBehaviour
{
    private tObjectPool<tBullet> _objectPool;

    public void AssignObjectPool(tObjectPool<tBullet> objectPool)
    {
        _objectPool = objectPool;
    }

    public GameObject _bloodSplashPrefab;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = collision.transform.rotation;
        Vector3 position = contact.point;
        Instantiate(_bloodSplashPrefab, position, rotation);

        _objectPool.ReturnObject(this);
    }
}
