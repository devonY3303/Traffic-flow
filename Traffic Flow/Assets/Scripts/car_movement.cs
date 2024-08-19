using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveDistance = 1.1f; // Distance to move in grid units
    public float moveSpeed = 5f;      // Speed of the car movement

    private Vector3 targetPosition;
    private bool isMoving = false;

    public Score_Management scoreManager; // Reference to the ScoreManager

    void Start()
    {
        targetPosition = transform.position;

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
