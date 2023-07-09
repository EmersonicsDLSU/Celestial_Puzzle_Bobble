using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private LevelDesign _levelDesign;
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private GameObject _levelParent;
    private GemSpawner _gemSpawner;

    // Start is called before the first frame update
    void Awake()
    {
        if (_levelDesign == null) Debug.LogError("Missing 'LevelDesign' script!");
        if (_playerParent == null) Debug.LogError("Missing 'Player Parent' gameObject!");
        if (_levelParent == null) Debug.LogError("Missing 'Player Parent' gameObject!");
    }

    void Start()
    {
        _gemSpawner = FindObjectOfType<GemSpawner>();
        if (_gemSpawner == null)
        {
            Debug.LogError("Missing 'GemSpawner' script!");
        }
        if (_levelDesign != null && _playerParent != null && _levelParent != null)
        {
            // position the player in the world
            Instantiate(_levelDesign._playerShooterPrefab, _playerParent.transform.position, Quaternion.identity);
            // position the level in the world
            Instantiate(_levelDesign._tileMapPrefab, _levelParent.transform.position, Quaternion.identity);
            // assign the starting balls of the level design
            InitializeStartingBalls();
        }
    }
    
    // Dictionary to map initials to EGemBall enum values
    private Dictionary<char, EGemBall> colorInitialsMap = new Dictionary<char, EGemBall>()
    {
        {'B', EGemBall.BLUE},
        {'N', EGemBall.BROWN},
        {'G', EGemBall.GREEN},
        {'R', EGemBall.RED},
        {'W', EGemBall.WHITE},
        {'Y', EGemBall.YELLOW},
        {'Z', EGemBall.NONE}
};

    private void InitializeStartingBalls()
    {
        // grid for the balls starting positions
        EGemBall[,] grid;

        string fileContents = _levelDesign._txtStartingBalls.text;
        // retrieve each of the lines from the txt file
        string[] lines = fileContents.Split('\n');

        grid = new EGemBall[_levelDesign._maxRows, _levelDesign._maxColumns];

        // iterate each of the lines
        for (int row = 0; row < _levelDesign._maxRows; row++)
        {
            // iterate each of the elements in the current line
            string[] elements = lines[row].Trim().Split(' ');
            for (int col = 0; col < _levelDesign._maxColumns; col++)
            {
                char charValue = elements[col][0];
                // Convert the initial char to the corresponding EGemBall enum value
                EGemBall gemBallEnum = colorInitialsMap[charValue];
                // assign the enum value to the grid
                grid[row, col] = gemBallEnum;
            }
        }

        // position the balls
        AssignBallsPositions(ref grid);
    }

    private void AssignBallsPositions(ref EGemBall[,] grid)
    {
        int verticalMax = 12; // maximum height
        float horizontalOffsetEven = 0.25f; // even rows
        float horizontalOffsetOdd = 0.75f; // odd rows
        GemBallRefs [,] gridRefs = new GemBallRefs[_levelDesign._maxRows, _levelDesign._maxColumns];

        for (int row = 0; row < _levelDesign._maxRows; row++)
        {
            // iterate each of the balls based on the template
            for (int col = 0; col < _levelDesign._maxColumns; col++)
            {
                // get the color of the ball to borrow
                EGemBall gemBallEnum = grid[row, col];
                // checks first if the pool of the current color is empty
                if (gemBallEnum != EGemBall.NONE && _gemSpawner.GetObjectPool(gemBallEnum) != null)
                {
                    // borrow the object from the pool
                    GemPool gem = _gemSpawner.GetObjectPool(gemBallEnum).GetObject();

                    // assign the ball reference to the gridRefs
                    gridRefs[row, col] = gem._gemBallRef;

                    // immediately change the mobility to 'static'
                    gem._gemBallRef._rb.bodyType = RigidbodyType2D.Static;
                    gem._gemBallRef._gemBallStatus.SetMobility(EGemBallMobility.STATIC);

                    // assign the new position of the ball
                    Vector3 newPos = Vector3.zero;
                    if (row % 2 == 0) // if we're in the even row
                        newPos = new Vector3(col + horizontalOffsetEven, verticalMax - row,0);
                    else // if odd row
                        newPos = new Vector3(col + horizontalOffsetOdd, verticalMax - row,0);

                    gem._gemBallRef.transform.position = newPos;
                }
                else
                {
                    // assign the ball reference to the gridRefs
                    gridRefs[row, col] = null;
                }
            }
        }

        // assign the adjacent balls to each of the starting balls in the level
        AssignAdjacentBalls(ref gridRefs);
    }

    private void AssignAdjacentBalls(ref GemBallRefs[,] grid)
    {
        // Assign the adjacent balls for each ball
        for (int row = 0; row < _levelDesign._maxRows; row++)
        {
            for (int col = 0; col < _levelDesign._maxColumns; col++)
            {
                if (grid[row, col] == null || grid[row, col]._gemBallConnections == null) continue;
                GemBall_Connections currentBall = grid[row, col]._gemBallConnections;

                Debug.Log($"Index: [{row},{col}]");

                // Check neighboring balls and add them to the adjacent balls list
                // Even row
                if (row % 2 == 0)
                {
                    if (col > 0 && grid[row, col - 1] != null) // Check left ball
                        currentBall.AddAdjacentBall(grid[row, col - 1]);

                    if (col < _levelDesign._maxColumns - 1 && grid[row, col + 1] != null) // Check right ball
                        currentBall.AddAdjacentBall(grid[row, col + 1]);

                    if (row > 0) // Check upper-left and upper-right balls
                    {
                        if (col > 0 && grid[row - 1, col - 1] != null)
                            currentBall.AddAdjacentBall(grid[row - 1, col - 1]);

                        if (col < _levelDesign._maxColumns - 1 && grid[row - 1, col] != null)
                            currentBall.AddAdjacentBall(grid[row - 1, col]);
                    }

                    if (row < _levelDesign._maxRows - 1) // Check lower-left and lower-right balls
                    {
                        if (col > 0 && grid[row + 1, col - 1] != null)
                            currentBall.AddAdjacentBall(grid[row + 1, col - 1]);

                        if (col < _levelDesign._maxColumns - 1 && grid[row + 1, col] != null)
                            currentBall.AddAdjacentBall(grid[row + 1, col]);
                    }
                }
                // Odd row
                else
                {
                    if (col > 0 && grid[row, col - 1] != null) // Check left ball
                        currentBall.AddAdjacentBall(grid[row, col - 1]);

                    if (col < _levelDesign._maxColumns - 1 && grid[row, col + 1] != null) // Check right ball
                        currentBall.AddAdjacentBall(grid[row, col + 1]);

                    if (row > 0) // Check upper-left and upper-right balls
                    {
                        if (grid[row - 1, col] != null)
                            currentBall.AddAdjacentBall(grid[row - 1, col]);

                        if (grid[row - 1, col + 1] != null)
                            currentBall.AddAdjacentBall(grid[row - 1, col + 1]);
                    }

                    if (row < _levelDesign._maxRows - 1) // Check lower-left and lower-right balls
                    {
                        if (grid[row + 1, col] != null)
                            currentBall.AddAdjacentBall(grid[row + 1, col]);

                        if (grid[row + 1, col + 1] != null)
                            currentBall.AddAdjacentBall(grid[row + 1, col + 1]);
                    }
                }

                currentBall.ShowAdjacentBalls();
            }
        }

    }

}
