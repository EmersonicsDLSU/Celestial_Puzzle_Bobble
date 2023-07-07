using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Status : MonoBehaviour, ICannonRef
{
    public CannonRefs _cannonRef { get; private set; }

    public void RefUpdate(CannonRefs mainRef)
    {

    }
    public void SetCannonRef(CannonRefs _cannonRef)
    {
        this._cannonRef = _cannonRef;
    }
}
