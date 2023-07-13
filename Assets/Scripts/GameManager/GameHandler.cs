using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private LevelDesign _currentLevelDesign;
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private GameObject _levelParent;
    private GemSpawner _gemSpawner;
    public GemBallRefs[,] gridRefs { get; private set; }
    public int _comboLength = 3;
    [HideInInspector] public List<EGemBall> _existingBallTypes = new List<EGemBall>();

    [HideInInspector] public GameObject _player;
    [HideInInspector] public GameObject _tileMapLevel;

    // Start is called before the first frame update
    void Awake()
    {
        if (_currentLevelDesign == null) Debug.LogError("Missing 'LevelDesign' script!");
        if (_playerParent == null) Debug.LogError("Missing 'Player Parent' gameObject!");
        if (_levelParent == null) Debug.LogError("Missing 'Player Parent' gameObject!");
        
        _gemSpawner = FindObjectOfType<GemSpawner>();
        if (_gemSpawner == null)
        {
            Debug.LogError("Missing 'GemSpawner' script!");
        }
        if (_currentLevelDesign != null && _playerParent != null && _levelParent != null)
        {
            // position the player in the world
            _player = Instantiate(_currentLevelDesign._playerShooterPrefab, _playerParent.transform.position, Quaternion.identity);
            _player.transform.parent = _playerParent.transform;
            // position the level in the world
            _tileMapLevel = Instantiate(_currentLevelDesign._tileMapPrefab, _levelParent.transform.position, Quaternion.identity);
            _tileMapLevel.transform.parent = _levelParent.transform;
            // assign the starting balls of the level design
            InitializeStartingBalls();
        }
    }

    void Start()
    {

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

        string fileContents = _currentLevelDesign._txtStartingBalls.text;
        // retrieve each of the lines from the txt file
        string[] lines = fileContents.Split('\n');

        grid = new EGemBall[_currentLevelDesign._maxRows, _currentLevelDesign._maxColumns];

        // iterate each of the lines
        for (int row = 0; row < _currentLevelDesign._maxRows; row++)
        {
            // iterate each of the elements in the current line
            string[] elements = lines[row].Trim().Split(' ');
            for (int col = 0; col < _currentLevelDesign._maxColumns; col++)
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
        gridRefs = new GemBallRefs[_currentLevelDesign._maxRows, _currentLevelDesign._maxColumns];

        for (int row = 0; row < _currentLevelDesign._maxRows; row++)
        {
            // iterate each of the balls based on the template
            for (int col = 0; col < _currentLevelDesign._maxColumns; col++)
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
        
        // assign the adjacent balls of this ball
        VerifyAllBallsNeighbors();
        
    }

    public void CheckExistingBallTypes()
    {
        _existingBallTypes.Clear();

        // Iterate through each element in gridRefs
        for (int i = 0; i < gridRefs.GetLength(0); i++)
        {
            for (int j = 0; j < gridRefs.GetLength(1); j++)
            {
                GemBallRefs ballRef = gridRefs[i, j];
                if (ballRef == null) continue;
                EGemBall ballType = ballRef._gemBallStatus.GetGemID();

                // Exclude NONE and ENUM_SIZE from the check
                if (ballType != EGemBall.NONE && ballType != EGemBall.ENUM_SIZE)
                {
                    // Check if the ballType is already in the _existingBallTypes list
                    if (!_existingBallTypes.Contains(ballType))
                    {
                        _existingBallTypes.Add(ballType);
                    }
                }
            }
        }
    }

    public void VerifyAllBallsNeighbors()
    {
        // Assign the adjacent balls for each ball
        for (int row = 0; row < GetCurLD()._maxRows; row++)
        {
            for (int col = 0; col < GetCurLD()._maxColumns; col++)
            {
                if (gridRefs[row, col] == null || gridRefs[row, col]._gemBallConnections == null) continue;
                GemBall_Connections currentBall = gridRefs[row, col]._gemBallConnections;
                // assign the position in the grid
                currentBall._gemBallRef._gemBallStatus.position.Row = row;
                currentBall._gemBallRef._gemBallStatus.position.Col = col;

                //Debug.Log($"Index: [{row},{col}]");

                // check the adjacent ball around the ball
                currentBall.CheckNeighboringBalls(this, row, col);
                // verify to know if the ball is not supported; if not then collapse
                if (!currentBall.CheckConnectivity(currentBall))
                    currentBall._gemBallRef._gemPoolSc.ReturnPool();
                // reset visit status of all existing balls in the grid
                ResetVisitedStateForConnectivity();
                //currentBall.ShowAdjacentBalls();
            }
        }
    }
    public void MoveDownAllBalls()
    {
        List <GemBallRefs> ballsToMove = new List <GemBallRefs>();
        List <GemBallRefs> ballsAtEdge = new List <GemBallRefs>();

        // reset all the gridRefs element
        for (int row = 0; row < GetCurLD()._maxRows; row++)
        {
            for (int col = 0; col < GetCurLD()._maxColumns; col++)
            {
                if (gridRefs[row, col] == null || gridRefs[row, col]._gemBallConnections == null) continue;
                // add to balls to move list
                if (col < GetCurLD()._maxColumns - 1)
                    ballsToMove.Add(gridRefs[row, col]);
                else
                    ballsAtEdge.Add(gridRefs[row, col]);
                // make the cell vacant
                gridRefs[row, col] = null;
            }
        }

        // move the balls 11 units to the right and downward
        foreach (var currentBall in ballsToMove)
        {
            currentBall._gemBallStatus.position.Row += 1;
                //Debug.Log($"Index: [{row},{col}]");

                // if new row is even
                if (currentBall._gemBallStatus.position.Row % 2 == 0)
                {
                    // translate the new position of the ball
                    Vector3 newPosition = currentBall.transform.position;
                    newPosition.x = currentBall._gemBallStatus.position.Col + GetCurLD()._horizontalOffsetEven;
                    newPosition.y = GetCurLD()._maxVerticalPos - currentBall._gemBallStatus.position.Row;
                    currentBall.transform.position = newPosition;
                }
                // if new row is odd
                else
                {
                    // translate the new position of the ball
                    Vector3 newPosition = currentBall.transform.position;
                    newPosition.x = currentBall._gemBallStatus.position.Col + GetCurLD()._horizontalOffsetOdd;
                    newPosition.y = GetCurLD()._maxVerticalPos - currentBall._gemBallStatus.position.Row;
                    currentBall.transform.position = newPosition;
                }
                
                // check if its out of bounds; exceed the threshold line
                if (currentBall._gemBallStatus.position.Row < 0 || currentBall._gemBallStatus.position.Row >= GetCurLD()._maxRows)
                {
                    return;
                }
                // assign the ball to the grid
                gridRefs[currentBall._gemBallStatus.position.Row, currentBall._gemBallStatus.position.Col] = currentBall;
                // recheck its new neighboring balls
                currentBall._gemBallConnections.CheckNeighboringBalls(this, currentBall._gemBallStatus.position.Row,
                    currentBall._gemBallStatus.position.Col);
        }
        
        // increase the base row
        FindAnyObjectByType<PlayerStatus>()._currentBaseRow += 1;

        for (int i = ballsAtEdge.Count - 1; i >= 0; i--)
        {
            var ball = ballsAtEdge[i];
            // reset all the gridRefs element
            bool isBreak = false;
            for (int row = 0; row < GetCurLD()._maxRows && !isBreak; row++)
            {
                for (int col = 0; col < GetCurLD()._maxColumns; col++)
                {
                    bool ifValidCol = (row % 2 == 0) ? true : (col == GetCurLD()._maxColumns - 1 ? false : true);
                    if (gridRefs[row, col] == null && ifValidCol && row >= FindAnyObjectByType<PlayerStatus>()._currentBaseRow)
                    {
                        Debug.Log($"Name:{ball.name} New Row: {row} Col:{col}");
                        ball._gemBallStatus.position.Row = row;
                        ball._gemBallStatus.position.Col = col;
                        // if new row is even
                        if (ball._gemBallStatus.position.Row % 2 == 0)
                        {
                            // translate the new position of the ball
                            Vector3 newPosition = ball.transform.position;
                            newPosition.x = ball._gemBallStatus.position.Col + GetCurLD()._horizontalOffsetEven;
                            newPosition.y = GetCurLD()._maxVerticalPos - ball._gemBallStatus.position.Row;
                            ball.transform.position = newPosition;
                        }
                        // if new row is odd
                        else
                        {
                            // translate the new position of the ball
                            Vector3 newPosition = ball.transform.position;
                            newPosition.x = ball._gemBallStatus.position.Col + GetCurLD()._horizontalOffsetOdd;
                            newPosition.y = GetCurLD()._maxVerticalPos - ball._gemBallStatus.position.Row;
                            ball.transform.position = newPosition;
                        }
                        
                        // check if its out of bounds; exceed the threshold line
                        if (ball._gemBallStatus.position.Row < 0 || ball._gemBallStatus.position.Row >= GetCurLD()._maxRows)
                        {
                            return;
                        }
                        // assign the ball to the grid
                        gridRefs[row, col] = ball;
                        // recheck its new neighboring balls
                        ball._gemBallConnections.CheckNeighboringBalls(this, ball._gemBallStatus.position.Row,
                            ball._gemBallStatus.position.Col);
                        
                        // Remove the ball from the list
                        ballsAtEdge.RemoveAt(i);
                        isBreak = true;
                        break;
                    }
                }
            }

        }
        
    }

    private void ResetVisitedStateForConnectivity()
    {
        // reset visit status for connectivity check
        for (int row = 0; row < GetCurLD()._maxRows; row++)
        {
            for (int col = 0; col < GetCurLD()._maxColumns; col++)
            {
                if (gridRefs[row, col] == null || gridRefs[row, col]._gemBallConnections == null) continue;
                GemBall_Status status = gridRefs[row, col]._gemBallStatus;
                status._isVisitedForConnectivity = false;
            }
        }
    }

    public void ShowGridAndAdjacencies()
    {
        for (int i = 0; i < GetCurLD()._maxRows; i++)
        {
            for (int j = 0; j < GetCurLD()._maxColumns; j++)
            {
                if (gridRefs[i, j] == null) continue;
                Debug.Log($"Position: [{i}:{j}] COLOR: {gridRefs[i,j]._gemBallStatus.GetGemID()}");
                string temp = $"Neighbors: ";
                for (int k = 0; k < gridRefs[i,j]._gemBallConnections.AdjacentBalls.Count; k++)
                {
                    temp += $" {gridRefs[i, j]._gemBallConnections.AdjacentBalls[k]._gemBallStatus.GetGemID()},";
                }
                Debug.Log(temp);
            }
        }
    }

    public LevelDesign GetCurLD()
    {
        return _currentLevelDesign;
    }
    public GameObject GetPP()
    {
        return _playerParent;
    }
    public GameObject GetLP()
    {
        return _levelParent;
    }

}
