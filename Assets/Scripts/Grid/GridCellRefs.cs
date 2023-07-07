using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellRefs : MonoBehaviour
{
    private List<IGridCellRef> _componentList;
    // object's components
    [HideInInspector] public BoxCollider2D _collider;
    [HideInInspector] public GridCell_Status _gridCellStatus;
    [HideInInspector] public GridCell_Collision _gridCellCellCollision;

    void Awake()
    {
        GetReferences();

        // add it to the component list
        _componentList = new List<IGridCellRef>();
        if (_gridCellStatus != null)
        {
            _componentList.Add(_gridCellStatus);
        }
        if (_gridCellCellCollision != null)
        {
            _componentList.Add(_gridCellCellCollision);
        }
    }

    private void GetReferences()
    {
        // gets the reference of the components
        _collider = GetComponentInChildren<BoxCollider2D>();
        if (_collider == null)
        {
            Debug.LogError("Missing 'Collider2D' script!");
        }

        _gridCellStatus = GetComponentInChildren<GridCell_Status>();
        if (_gridCellStatus == null)
        {
            Debug.LogError("Missing 'GridCell' script!");
        }
        else
        {
            _gridCellStatus.SetGridCellRef(this);
        }

        _gridCellCellCollision = GetComponentInChildren<GridCell_Collision>();
        if (_gridCellCellCollision == null)
        {
            Debug.LogError("Missing 'GridCell_Attachment' script!");
        }
        else
        {
            _gridCellCellCollision.SetGridCellRef(this);
        }
    }

    void Update()
    {
        // controls the update of all components
        foreach (var comp in _componentList)
        {
            comp.RefUpdate(this);   
        }
    }
}
