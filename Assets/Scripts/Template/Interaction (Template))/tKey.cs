using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tKey : tAInteraction
{
    public override void OAwake()
    {
        throw new System.NotImplementedException();
    }

    public override void OStart()
    {
        throw new System.NotImplementedException();
    }

    public override void OCreateDelegates()
    {
        // add events
        D_PreEvents.AddListener(TempFunc);
        D_PostEvents.AddListener(TempFunc);
    }

    public override void ODeleteDelegates()
    {
        // remove events
        D_PreEvents.RemoveListener(TempFunc);
        D_PostEvents.RemoveListener(TempFunc);
    }

    void TempFunc()
    {

    }
}
