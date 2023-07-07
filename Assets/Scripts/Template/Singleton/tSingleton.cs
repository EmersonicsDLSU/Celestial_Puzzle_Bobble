using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }

    protected virtual void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this.gameObject.GetComponent<T>();
        }
    }
}
