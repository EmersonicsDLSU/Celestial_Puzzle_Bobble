using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GemBall_Connections : MonoBehaviour, IGemBallRef
{
    public GemBallRefs _gemBallRef { get; private set; }
    public List<GemBallRefs> AdjacentBalls { get; private set; }

    void Awake()
    {
        // Initialize the list of adjacent balls
        AdjacentBalls = new List<GemBallRefs>();
    }
    public void CheckNeighboringBalls(GameHandler g, int row, int col)
    {
        RemoveAllAdjacentBalls();
    if (row % 2 == 0) // Even row
    {
        if (col > 0 && g.gridRefs[row, col - 1] != null) // Check left ball
            AddAdjacentBall(g.gridRefs[row, col - 1]);

        if (col < g.GetCurLD()._maxColumns - 1 && g.gridRefs[row, col + 1] != null) // Check right ball
            AddAdjacentBall(g.gridRefs[row, col + 1]);

        if (row > 0) // Check upper-left and upper-right balls
        {
            if (col > 0 && g.gridRefs[row - 1, col - 1] != null)
                AddAdjacentBall(g.gridRefs[row - 1, col - 1]);

            if (col < g.GetCurLD()._maxColumns - 1 && g.gridRefs[row - 1, col] != null)
                AddAdjacentBall(g.gridRefs[row - 1, col]);
        }

        if (row < g.GetCurLD()._maxRows - 1) // Check lower-left and lower-right balls
        {
            if (col > 0 && g.gridRefs[row + 1, col - 1] != null)
                AddAdjacentBall(g.gridRefs[row + 1, col - 1]);

            if (col < g.GetCurLD()._maxColumns - 1 && g.gridRefs[row + 1, col] != null)
                AddAdjacentBall(g.gridRefs[row + 1, col]);
        }
    }
    else // Odd row
    {
        if (col > 0 && g.gridRefs[row, col - 1] != null) // Check left ball
            AddAdjacentBall(g.gridRefs[row, col - 1]);

        if (col < g.GetCurLD()._maxColumns - 1 && g.gridRefs[row, col + 1] != null) // Check right ball
            AddAdjacentBall(g.gridRefs[row, col + 1]);

        if (row > 0) // Check upper-left and upper-right balls
        {
            if (g.gridRefs[row - 1, col] != null)
                AddAdjacentBall(g.gridRefs[row - 1, col]);

            if (g.gridRefs[row - 1, col + 1] != null)
                AddAdjacentBall(g.gridRefs[row - 1, col + 1]);
        }

        if (row < g.GetCurLD()._maxRows - 1) // Check lower-left and lower-right balls
        {
            if (g.gridRefs[row + 1, col] != null)
                AddAdjacentBall(g.gridRefs[row + 1, col]);

            if (g.gridRefs[row + 1, col + 1] != null)
                AddAdjacentBall(g.gridRefs[row + 1, col + 1]);
        }
    }
    }

    public void PerformBFS(GemBallRefs firstBall)
    {
        // Create a queue for BFS traversal
        Queue<GemBallRefs> nodeQueue = new Queue<GemBallRefs>();
        HashSet<GemBallRefs> visited = new HashSet<GemBallRefs>();
        List<GemBallRefs> prospectDeletes = new List<GemBallRefs>();
        int currComboCount = 1;
        EGemBall colorToMatch = firstBall._gemBallStatus.GetGemID();

        // Enqueue the starting ball
        nodeQueue.Enqueue(firstBall);
        // iterate to check if there's a combo
        while (nodeQueue.Count > 0)
        {
            // Dequeue the front ball from the queue
            GemBallRefs currentBall = nodeQueue.Dequeue();

            // If the ball is not visited, process it
            if (!visited.Contains(currentBall))
            {
                // Mark the ball as visited
                visited.Add(currentBall);

                // Enqueue the unvisited adjacent balls
                foreach (GemBallRefs adjacentBall in currentBall._gemBallConnections.AdjacentBalls)
                {
                    if (!visited.Contains(adjacentBall) && adjacentBall._gemBallStatus.GetGemID() == colorToMatch)
                    {
                        //Debug.Log($"Found a Matching Color: {colorToMatch}");
                        currComboCount++;
                        nodeQueue.Enqueue(adjacentBall);
                        // bound to be deleted
                        adjacentBall._gemBallStatus._boundToBeDeleted = true;
                    }
                }
            }
        }

        //Debug.Log($"ComboCount: {currComboCount}");
        // check if there's a valid combination
        if (currComboCount >= FindObjectOfType<GameHandler>()._comboLength)
        {
            nodeQueue.Clear();
            visited.Clear();
            // Enqueue the starting ball
            nodeQueue.Enqueue(firstBall);
            prospectDeletes.Add(firstBall);
            // iterate to add all the prospect for deletion
            while (nodeQueue.Count > 0)
            {
                // Dequeue the front ball from the queue
                GemBallRefs currentBall = nodeQueue.Dequeue();

                // If the ball is not visited, process it
                if (!visited.Contains(currentBall))
                {
                    // Mark the ball as visited
                    visited.Add(currentBall);

                    // Enqueue the unvisited adjacent balls
                    foreach (GemBallRefs adjacentBall in currentBall._gemBallConnections.AdjacentBalls)
                    {
                        // no more support and not connected to the ceiling
                        bool ifNoMoreSupportANDIsNotAtTheCeiling = adjacentBall._gemBallConnections.AdjacentBalls.All(
                                ball => ball._gemBallStatus._boundToBeDeleted == true) && adjacentBall.transform.position.y <
                            FindObjectOfType<GameHandler>().GetCurLD()._maxVerticalPos;
                        if (!visited.Contains(adjacentBall) && 
                            (adjacentBall._gemBallStatus.GetGemID() == colorToMatch || ifNoMoreSupportANDIsNotAtTheCeiling)
                            )
                        {
                            // add it to the queue and prospect list
                            nodeQueue.Enqueue(adjacentBall);
                            prospectDeletes.Add(adjacentBall);
                            // bound to be deleted
                            adjacentBall._gemBallStatus._boundToBeDeleted = true;
                            //Debug.Log($"Add to Prospect!");
                        }
                    }
                }
            }
        }

        // remove all the prospects
        foreach (var ball in prospectDeletes)
        {
            // return the pool object
            ball._gemPoolSc.ReturnPool();
        }
        // play collapse vfx
        if (prospectDeletes.Count > 0)
            FindObjectOfType<AudioManager>().PlayCollapseSFX();

        // verify all balls neighbors
        FindObjectOfType<GameHandler>().VerifyAllBallsNeighbors();
    }
    
    // Function to check if the ball at given position is connected to the wall
    public bool CheckConnectivity(GemBall_Connections currentBall) {
        // Base cases: ball is connected to the wall or already visited
        GemBall_Status status = currentBall._gemBallRef._gemBallStatus;
        GemBall_Status.BallPosition pos = status.position;
        if (pos.Row == 0)
            return true;
        if (currentBall.AdjacentBalls.Count <= 0) return false;

        // Mark the current ball as visited
        status._isVisitedForConnectivity = true;

        // Recursive calls for neighboring balls
        foreach (GemBallRefs neighborBall in currentBall.AdjacentBalls) {
            if (!neighborBall._gemBallConnections._gemBallRef._gemBallStatus._isVisitedForConnectivity &&
                CheckConnectivity(neighborBall._gemBallConnections))
            {
                return true; // Connected to the wall
            }
        }
        
        Debug.Log($"Not Connected To WALL!");
        return false; // Not connected to the wall
    }


    // Add a ball to the list of adjacent balls
    public void AddAdjacentBall(GemBallRefs ball)
    {
        // Check if the ball is already in the AdjacentBalls list
        if (AdjacentBalls.Contains(ball))
            return;

        // add it to the adjacency list
        AdjacentBalls.Add(ball);
        // add this ball to the connected ball to since they're now neighbors
        ball._gemBallConnections.AddAdjacentBall(this._gemBallRef);
    }

    // Remove a ball from the list of adjacent balls
    public void RemoveAdjacentBall(GemBallRefs ball)
    {
        AdjacentBalls.Remove(ball);
    }

    // Remove a ball from the list of adjacent balls
    public void RemoveAllAdjacentBalls()
    {
        AdjacentBalls.Clear();
    }

    public void ShowAdjacentBalls()
    {
        foreach (GemBallRefs ball in AdjacentBalls)
        {
            Debug.Log(ball._gemBallStatus.GetGemID());
        }
    }
    public void SetGemBallRef(GemBallRefs _gemBallRef)
    {
        this._gemBallRef = _gemBallRef;
    }

    public void RefUpdate(GemBallRefs mainRef)
    {

    }
}
