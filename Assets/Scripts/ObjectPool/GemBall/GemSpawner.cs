using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] public Transform _sourceLocation;
    [SerializeField] private GameObject _bluePrefab;
    [SerializeField] private GameObject _brownPrefab;
    [SerializeField] private GameObject _greenPrefab;
    [SerializeField] private GameObject _redPrefab;
    [SerializeField] private GameObject _whitePrefab;
    [SerializeField] private GameObject _yellowPrefab;
    [SerializeField] private int _maxStockPerGem = 50;
    private List<GameObject> _gemPrefabList;

    private ObjectPooling<GemPool> _blueGemPool, _brownGemPool, _greenGemPool,
        _redGemPool, _whiteGemPool, _yellowGemPool;
    private List<ObjectPooling<GemPool>> _gemPoolList;

    void Awake()
    {
        InitializeObjects();
    }

    // Public Functions
    public ObjectPooling<GemPool> GetObjectPool(EGemBall gemType)
    {
        return _gemPoolList[(int)gemType];
    }

    // Private Functions
    private void InitializeObjects()
    {
        _gemPrefabList = new List<GameObject>();
        _gemPoolList = new List<ObjectPooling<GemPool>>();
        // assign data objects in the list
        _gemPrefabList.Add(_bluePrefab);_gemPrefabList.Add(_brownPrefab);_gemPrefabList.Add(_greenPrefab);
        _gemPrefabList.Add(_redPrefab);_gemPrefabList.Add(_whitePrefab);_gemPrefabList.Add(_yellowPrefab);

        if (_spawnLocation == null || _sourceLocation == null)
            Debug.LogError("Missing one or more Transform requirement!");
        bool willInitialize = true;
        foreach (var gem in _gemPrefabList)
        {
            if (gem == null || gem.GetComponentInChildren<GemPool>() == null)
            {
                Debug.LogError("Missing prefab or component!");
                willInitialize = false;
                break;
            }
        }
        if (willInitialize)
        {
            _blueGemPool = new ObjectPooling<GemPool>(BlueGemFactoryMethod, TurnOnGem, TurnOffGem, _maxStockPerGem, false);
            _brownGemPool = new ObjectPooling<GemPool>(BrownGemFactoryMethod, TurnOnGem, TurnOffGem, _maxStockPerGem, false);
            _greenGemPool = new ObjectPooling<GemPool>(GreenGemFactoryMethod, TurnOnGem, TurnOffGem, _maxStockPerGem, false);
            _redGemPool = new ObjectPooling<GemPool>(RedGemFactoryMethod, TurnOnGem, TurnOffGem, _maxStockPerGem, false);
            _whiteGemPool = new ObjectPooling<GemPool>(WhiteGemFactoryMethod, TurnOnGem, TurnOffGem, _maxStockPerGem, false);
            _yellowGemPool = new ObjectPooling<GemPool>(YellowGemFactoryMethod, TurnOnGem, TurnOffGem, _maxStockPerGem, false);
            _gemPoolList.Add(_blueGemPool);_gemPoolList.Add(_brownGemPool);_gemPoolList.Add(_greenGemPool);
            _gemPoolList.Add(_redGemPool);_gemPoolList.Add(_whiteGemPool);_gemPoolList.Add(_yellowGemPool);
        }
    }
    private GemPool BlueGemFactoryMethod()
    {
        GameObject obj = Instantiate(_bluePrefab) as GameObject;
        GemPool objScript = obj.GetComponentInChildren<GemPool>();

        TranslateToSource(obj);

        return obj.GetComponentInChildren<GemPool>();
    }
    private GemPool BrownGemFactoryMethod()
    {
        GameObject obj = Instantiate(_brownPrefab) as GameObject;
        GemPool objScript = obj.GetComponentInChildren<GemPool>();

        TranslateToSource(obj);

        return obj.GetComponentInChildren<GemPool>();
    }
    private GemPool GreenGemFactoryMethod()
    {
        GameObject obj = Instantiate(_greenPrefab) as GameObject;
        GemPool objScript = obj.GetComponentInChildren<GemPool>();

        TranslateToSource(obj);

        return obj.GetComponentInChildren<GemPool>();
    }
    private GemPool RedGemFactoryMethod()
    {
        GameObject obj = Instantiate(_redPrefab) as GameObject;
        GemPool objScript = obj.GetComponentInChildren<GemPool>();

        TranslateToSource(obj);

        return obj.GetComponentInChildren<GemPool>();
    }
    private GemPool WhiteGemFactoryMethod()
    {
        GameObject obj = Instantiate(_whitePrefab) as GameObject;
        GemPool objScript = obj.GetComponentInChildren<GemPool>();

        TranslateToSource(obj);

        return obj.GetComponentInChildren<GemPool>();
    }
    private GemPool YellowGemFactoryMethod()
    {
        GameObject obj = Instantiate(_yellowPrefab) as GameObject;
        GemPool objScript = obj.GetComponentInChildren<GemPool>();

        TranslateToSource(obj);

        return obj.GetComponentInChildren<GemPool>();
    }

    private void TranslateToSource(GameObject obj)
    {
        obj.transform.parent = _sourceLocation;
        obj.gameObject.SetActive(false);
    }
    
    private void TurnOnGem(GemPool gem)
    {
        Transform currentTransform = gem.transform;

        while (currentTransform != null)
        {
            GemBallRefs gemParent = currentTransform.GetComponent<GemBallRefs>();
            if (gemParent != null)
            {
                // parent and reposition(displayed) the recently borrowed pool object
                gemParent.transform.SetParent(_sourceLocation, false);
                gemParent.gameObject.SetActive(true);
                return;
            }

            currentTransform = currentTransform.parent;
        }
        
    }
    private void TurnOffGem(GemPool gem)
    {
        Transform currentTransform = gem.transform;

        while (currentTransform != null)
        {
            GemBallRefs gemParent = currentTransform.GetComponent<GemBallRefs>();
            if (gemParent != null)
            {
                // clear the adjacent balls
                gemParent._gemBallConnections.AdjacentBalls.Clear();
                // reset deletion status
                gemParent._gemBallStatus._boundToBeDeleted = false;
                // parent and reposition(hidden) the recently borrowed pool object
                gemParent.transform.SetParent(_sourceLocation, false);
                gemParent.gameObject.SetActive(false);
                return;
            }

            currentTransform = currentTransform.parent;
        }
        
    }

}
