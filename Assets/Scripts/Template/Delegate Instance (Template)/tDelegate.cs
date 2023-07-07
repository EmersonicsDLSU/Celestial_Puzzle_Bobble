using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tDelegate
{
    private static tDelegate _shared_instance = null;

    public static tDelegate GetInstance()
    {
        if (_shared_instance == null)
            _shared_instance = new tDelegate();

        return _shared_instance;
    }

    public Action D_OnDoorOpen = null;

    /* How to add to this delegate:
    private void Awake()
    {
        tDelegate.GetInstance().D_OnDoorOpen += MoveDoor; // MoveDoor can be a private/public func

    }

    public void OnDestroy()
    {
        tDelegate.GetInstance().D_OnDoorOpen -= MoveDoor;
    }
     */
}
