using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tBulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private Transform _sourceLocation;
    [SerializeField] private GameObject _bulletPrefab;

    private tObjectPool<tBullet> _objectPool;

    void Start()
    {
        
        if (_spawnLocation == null || _sourceLocation == null)
            Debug.LogError("Missing one or more Transform requirement!");
        if (_bulletPrefab == null || _bulletPrefab.GetComponent<tBullet>() == null)
            Debug.LogError("Missing prefab or component!");
        else
        {
            _objectPool = new tObjectPool<tBullet>(BulletFactoryMethod, TurnOnBullet, TurnOffBullet, 5, true);
        }
    }
    private tBullet BulletFactoryMethod()
    {
        GameObject obj = Instantiate(_bulletPrefab) as GameObject;
        tBullet objScript = obj.GetComponent<tBullet>();
        // attach a reference of the objectPool to the pool object
        objScript.AssignObjectPool(_objectPool);

        obj.transform.parent = _sourceLocation;
        obj.gameObject.SetActive(false);

        return obj.GetComponent<tBullet>();
    }

    private void TurnOnBullet(tBullet bullet)
    {
        // parent and reposition(displayed) the recently borrowed pool object
        bullet.transform.parent = _spawnLocation;
        bullet.transform.position = _spawnLocation.position;

        bullet.gameObject.SetActive(true);
    }
    private void TurnOffBullet(tBullet bullet)
    {
        // parent and reposition(hidden) the recently borrowed pool object
        bullet.transform.parent = _sourceLocation;
        bullet.transform.position = _sourceLocation.position;

        bullet.gameObject.SetActive(false);
    }

    // functionality for spawning bullet objects
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _objectPool.GetObject();
        }
    }
}
