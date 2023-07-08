using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell_Collision : MonoBehaviour, IGridCellRef
{
    public GridCellRefs _gridCellRef { get; private set; }

    public void RefUpdate(GridCellRefs mainRef)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_gridCellRef._gridCellStatus.isOccupied || !collision.CompareTag("GemBall")) return;

        GemBallRefs ballRef = collision.GetComponent<GemBallRefs>();
        // Check if the ball is ready to be attach
        if (ballRef == null || !ballRef._gemBallStatus._canAttach) return;
        
        Debug.Log("Ball Attached!");

        // assign the ball to the grid position 
        collision.transform.position = transform.position;
        ballRef._gemBallStatus.SetCanAttach(false);
        ballRef._rb.bodyType = RigidbodyType2D.Static;

        _gridCellRef._gridCellStatus.SetIsOccupied(true);
    }
    public void SetGridCellRef(GridCellRefs _gridCellRef)
    {
        this._gridCellRef = _gridCellRef;
    }
}
