using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell_Status : MonoBehaviour, IGridCellRef
{
    public GridCellRefs _gridCellRef { get; private set; }
    
    public bool isOccupied { get; private set; } = false;

    public void RefUpdate(GridCellRefs mainRef)
    {

    }
    public void SetGridCellRef(GridCellRefs _gridCellRef)
    {
        this._gridCellRef = _gridCellRef;
    }
    public void SetIsOccupied(bool value)
    {
        isOccupied = value;
    }
}

