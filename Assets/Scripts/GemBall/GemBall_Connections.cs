using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBall_Connections : MonoBehaviour, IGemBallRef
{
    public GemBallRefs _gemBallRef { get; private set; }
    public List<GemBallRefs> AdjacentBalls { get; set; }

    void Awake()
    {
        // Initialize the list of adjacent balls
        AdjacentBalls = new List<GemBallRefs>();
    }

    // Add a ball to the list of adjacent balls
    public void AddAdjacentBall(GemBallRefs ball)
    {
        AdjacentBalls.Add(ball);
    }

    // Remove a ball from the list of adjacent balls
    public void RemoveAdjacentBall(GemBallRefs ball)
    {
        AdjacentBalls.Remove(ball);
    }

    public void ShowAdjacentBalls()
    {
        foreach (GemBallRefs ball in AdjacentBalls)
        {
            Debug.Log(ball._gemBallStatus.GetGemID());
        }
    }

    public void RefUpdate(GemBallRefs mainRef)
    {

    }
    public void SetGemBallRef(GemBallRefs _gemBallRef)
    {
        this._gemBallRef = _gemBallRef;
    }
}
