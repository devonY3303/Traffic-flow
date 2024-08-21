using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveDistance = 1.1f; // Distance to move in grid units
    public float moveSpeed = 5f;      // Speed of the car movement

    private Vector3 targetPosition;
    private bool isMoving = false;

    public Score_Management scoreManager; // Reference to the ScoreManager
    public GameObject destinationPrefab;  // Prefab of the Destination tile

    // Define the valid positions for the grid map
    private Vector3[] validPositions = new Vector3[]
    {
        new Vector3(-2.2f, 1.1f, 0f),
        new Vector3(-1.1f, 1.1f, 0f),
        new Vector3(0f, 1.1f, 0f),
        new Vector3(1.1f, 1.1f, 0f),
        new Vector3(2.2f, 1.1f, 0f),

        new Vector3(-2.2f, 0f, 0f),
        new Vector3(-1.1f, 0f, 0f),
        new Vector3(0f, 0f, 0f),
        new Vector3(1.1f, 0f, 0f),
        new Vector3(2.2f, 0f, 0f),

        // Added 3 extra rows
        new Vector3(-2.2f, -1.1f, 0f),
        new Vector3(-1.1f, -1.1f, 0f),
        new Vector3(0f, -1.1f, 0f),
        new Vector3(1.1f, -1.1f, 0f),
        new Vector3(2.2f, -1.1f, 0f),

        new Vector3(-2.2f, -2.2f, 0f),
        new Vector3(-1.1f, -2.2f, 0f),
        new Vector3(0f, -2.2f, 0f),
        new Vector3(1.1f, -2.2f, 0f),
        new Vector3(2.2f, -2.2f, 0f),

        new Vector3(-2.2f, -3.3f, 0f),
        new Vector3(-1.1f, -3.3f, 0f),
        new Vector3(0f, -3.3f, 0f),
        new Vector3(1.1f, -3.3f, 0f),
        new Vector3(2.2f, -3.3f, 0f),
    };

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

    void Start()
    {
        // Randomly select a starting position from the grid
        int randomIndex = Random.Range(0, validPositions.Length);
        transform.position = validPositions[randomIndex];

        targetPosition = transform.position;

        // Randomly spawn a destination tile on the edge of the grid
        SpawnRandomDestination();

        // Ensure ScoreManager is assigned if not set in the Inspector
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<Score_Management>();
            if (scoreManager == null)
            {
                Debug.LogError("ScoreManager not found in the scene. Please add a ScoreManager to the scene.");
            }
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    private void SpawnRandomDestination()
    {
        int randomIndex = Random.Range(0, edgePositions.Length);
        Vector3 spawnPosition = edgePositions[randomIndex];
        Instantiate(destinationPrefab, spawnPosition, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GridDirection grid = collision.GetComponent<GridDirection>();

        if (grid != null && !isMoving)
        {
            MoveInDirection(grid.gridDirection);
        }

        if (ShouldIncreaseScore(grid))
        {
            UpdateScore();
        }
    }

    private void MoveInDirection(GridDirection.Direction direction)
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
        return grid.gameObject.CompareTag("Destination");
    }
}
