using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveDistance = 1.1f; // Distance to move in grid units
    public float moveSpeed = 5f;      // Speed of the car movement

    private Vector3 targetPosition;
    private bool isMoving = false;

    public Score_Management scoreManager; // Reference to the ScoreManager
    public GameObject destinationPrefab;  // Prefab of the Destination tile
    public ObstacleManager obstacleManager; // Reference to the ObstacleManager

    private GameObject currentDestination; // Reference to the current destination tile
    private GridPlacer gridPlacer;

    // Define the edge positions for the destination tile
    private Vector3[] edgePositions = new Vector3[]
    {
        //Left side
        new Vector3(-3.3f, 1.1f, 0f),
        new Vector3(-3.3f, 0f, 0f),
        new Vector3(-3.3f, -1.1f, 0f),
        new Vector3(-3.3f, -2.2f, 0f),
        new Vector3(-3.3f, -3.3f, 0f),

        //Bottom
        new Vector3(-2.2f, -4.4f, 0f),
        new Vector3(-1.1f, -4.4f, 0f),
        new Vector3(0f, -4.4f, 0f),
        new Vector3(1.1f, -4.4f, 0f),
        new Vector3(2.2f, -4.4f, 0f),

        //Right
        new Vector3(3.3f, 1.1f, 0f),
        new Vector3(3.3f, 0f, 0f),
        new Vector3(3.3f, -1.1f, 0f),
        new Vector3(3.3f, -2.2f, 0f),
        new Vector3(3.3f, -3.3f, 0f),

        //Top
        new Vector3(-2.2f, 2.2f, 0f),
        new Vector3(-1.1f, 2.2f, 0f),
        new Vector3(0f, 2.2f, 0f),
        new Vector3(1.1f, 2.2f, 0f),
        new Vector3(2.2f, 2.2f, 0f),
    };

    private GameOverManager gameOverManager;

    void Start()
    {
        // Randomly select a starting position from the grid
        int randomIndex = Random.Range(0, GridData.validPositions.Count);
        transform.position = GridData.validPositions[randomIndex];

        targetPosition = transform.position;

        // Randomly spawn a destination tile on the edge of the grid
        SpawnRandomDestination();

        // Ensure ScoreManager and ObstacleManager are assigned if not set in the Inspector
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<Score_Management>();
            if (scoreManager == null)
            {
                Debug.LogError("ScoreManager not found in the scene. Please add a ScoreManager to the scene.");
            }
        }

        if (obstacleManager == null)
        {
            obstacleManager = FindObjectOfType<ObstacleManager>();
            if (obstacleManager == null)
            {
                Debug.LogError("ObstacleManager not found in the scene. Please add an ObstacleManager to the scene.");
            }
        }

        gridPlacer = FindObjectOfType<GridPlacer>();

        // Get reference to GameOverManager
        gameOverManager = FindObjectOfType<GameOverManager>();

        // Spawn initial obstacles
        if (obstacleManager != null)
        {
            obstacleManager.SpawnRandomObstacles(3);
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                isMoving = false;

                // Check if the car is out of bounds
                if (!IsPositionWithinGrid(targetPosition))
                {
                    // Trigger Game Over
                    if (gameOverManager != null)
                    {
                        gameOverManager.GameOver();
                    }
                }
            }
        }
    }

    void MoveToRandomPosition()
    {
        int randomIndex = Random.Range(0, GridData.validPositions.Count);
        transform.position = GridData.validPositions[randomIndex];
        targetPosition = transform.position;
    }

    private void SpawnRandomDestination()
    {
        // Destroy the current destination if it exists
        if (currentDestination != null)
        {
            Destroy(currentDestination);
        }

        int randomIndex = Random.Range(0, edgePositions.Length);
        Vector3 spawnPosition = edgePositions[randomIndex];

        currentDestination = Instantiate(destinationPrefab, spawnPosition, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GridDirection grid = collision.GetComponent<GridDirection>();

        if (grid != null && !isMoving)
        {
            MoveInDirection(grid.gridDirection);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Trigger Game Over
            if (gameOverManager != null)
            {
                gameOverManager.GameOver();
            }
        }
        else if (ShouldIncreaseScore(grid))
        {
            UpdateScore();

            // Move the car to a random position from validPositions
            int randomIndex = Random.Range(0, GridData.validPositions.Count);
            transform.position = GridData.validPositions[randomIndex];
            targetPosition = GridData.validPositions[randomIndex];

            if (obstacleManager == null)
            {
                obstacleManager = FindObjectOfType<ObstacleManager>();
                if (obstacleManager == null)
                {
                    Debug.LogError("ObstacleManager not found in the scene. Please add an ObstacleManager to the scene.");
                }
            }

            if (obstacleManager != null)
            {
                obstacleManager.ClearObstacles();
                obstacleManager.SpawnRandomObstacles(5);
            }

            // Destroy the old destination
            Destroy(currentDestination);

            // Spawn a new destination
            SpawnRandomDestination();

            // Clear all placed tiles
            if (gridPlacer != null)
            {
                gridPlacer.ClearPlacedTiles();
            }
        }
    }

    public void MoveInDirection(GridDirection.Direction direction)
    {
        switch (direction)
        {
            case GridDirection.Direction.Up:
                targetPosition += Vector3.up * moveDistance;
                break;
            case GridDirection.Direction.Down:
                targetPosition += Vector3.down * moveDistance;
                break;
            case GridDirection.Direction.Left:
                targetPosition += Vector3.left * moveDistance;
                break;
            case GridDirection.Direction.Right:
                targetPosition += Vector3.right * moveDistance;
                break;
        }
        isMoving = true;
    }

    private void UpdateScore()
    {
        if (scoreManager != null)
        {
            scoreManager.AddPoint(); // Update the score in the ScoreManager
        }
    }

    private bool ShouldIncreaseScore(GridDirection grid)
    {
        return grid != null && grid.gameObject.CompareTag("Destination");
    }

    private bool IsPositionWithinGrid(Vector3 position)
    {
        return GridData.validPositions.Contains(position);
    }
}
