using System;
using System.Collections.Generic;

public class tObjectPool<T>
{
    // List of poolable objects
    private readonly List<T> _activeStock;
    private readonly List<T> _inactiveStock;
    // A parameter-less function that instantiates a certain class object
    private readonly Func<T> _factoryMethod;

    // List of functions to call when borrowing a pool object
    private readonly Action<T> _turnOnCallback;
    // List of functions to call when returning a pool object
    private readonly Action<T> _turnOffCallback;

    // endless pool if the size is Dynamic
    private readonly bool bIsDynamic;

    public tObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialStock = 0, bool isDynamic = true)
    {
        _factoryMethod = factoryMethod;
        bIsDynamic = isDynamic;

        _turnOffCallback = turnOffCallback;
        _turnOnCallback = turnOnCallback;

        _activeStock = new List<T>();
        _inactiveStock = new List<T>();

        for (var i = 0; i < initialStock; i++)
        {
            var o = _factoryMethod();
            _turnOffCallback(o);
            _inactiveStock.Add(o);
        }
    }

    public T GetObject()
    {
        var result = default(T);
        if (_inactiveStock.Count > 0)
        {
            result = _inactiveStock[0];
            _inactiveStock.RemoveAt(0);
            _activeStock.Add(result);
        }
        else if (bIsDynamic)
            result = _factoryMethod();
        _turnOnCallback(result);
        return result;
    }

    public void ReturnObject(T o)
    {
        _turnOffCallback(o);
        _inactiveStock.Add(o);
        _activeStock.Remove(o);
    }
}