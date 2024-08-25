using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Movement : MonoBehaviour
{
    public float moveInterval = 2f; // Time between movements
    public float moveDistance = 1.1f; // Distance to move per step
    public float moveSpeed = 2f; // Speed of the movement

    private Vector3 targetPosition;
    private float timer;
    private bool isMoving = false;
    private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

    void Start()
    {
        // Snap the initial position to the nearest grid point
        transform.position = SnapToGrid(transform.position);
        targetPosition = transform.position;
        timer = moveInterval;

        // Ensure the monster starts at a valid position
        if (!IsValidPosition(transform.position))
        {
            Debug.LogError("Monster starts at an invalid position.");
        }
        else
        {
            // Start the movement coroutine
            StartCoroutine(UpdateMovement());
        }
    }

    void Update()
    {
        // Move the monster towards the target position
        if (isMoving)
        {
            MoveMonster();
        }
    }

    private IEnumerator UpdateMovement()
    {
        while (true)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                // Attempt to find a valid direction
                bool foundValidMove = false;
                Vector3[] availableDirections = new Vector3[directions.Length];
                directions.CopyTo(availableDirections, 0);

                //Debug.Log("Available directions: " + string.Join(", ", availableDirections)); // Print available directions

                while (availableDirections.Length > 0 && !foundValidMove)
                {
                    Vector3 newDirection = GetRandomDirection(availableDirections);
                    Vector3 newPosition = SnapToGrid(transform.position + newDirection * moveDistance);

                    //Debug.Log("Trying new position: " + newPosition); // Print the new position

                    if (IsValidPosition(newPosition))
                    {
                        //Debug.Log("Valid position found: " + newPosition); // Print if valid
                        targetPosition = newPosition;
                        isMoving = true;
                        foundValidMove = true;
                    }
                    else
                    {
                        //Debug.Log("Invalid position, removing direction: " + newDirection); // Print if invalid
                        // Remove the invalid direction and try again
                        availableDirections = RemoveDirection(availableDirections, newDirection);
                        //Debug.Log("Remaining directions: " + string.Join(", ", availableDirections)); // Print remaining directions
                    }
                }

                if (!foundValidMove)
                {
                    Debug.LogWarning("Monster couldn't find a valid move.");
                }

                timer = moveInterval; // Reset the timer
            }

            yield return null; // Wait for the next frame
        }
    }

    private Vector3 SnapToGrid(Vector3 position)
    {
        // Snap the position to the nearest grid point based on moveDistance
        float snappedX = Mathf.Round(position.x / moveDistance) * moveDistance;
        float snappedY = Mathf.Round(position.y / moveDistance) * moveDistance;
        return new Vector3(snappedX, snappedY, position.z);
    }

    private Vector3 GetRandomDirection(Vector3[] availableDirections)
    {
        // Choose a random direction from the available directions
        int index = Random.Range(0, availableDirections.Length);
        return availableDirections[index];
    }

    private Vector3[] RemoveDirection(Vector3[] directionArray, Vector3 directionToRemove)
    {
        // Create a new array excluding the direction to remove
        List<Vector3> directionList = new List<Vector3>(directionArray);
        directionList.Remove(directionToRemove);
        return directionList.ToArray();
    }

    private void MoveMonster()
    {
        // Move the monster towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    private bool IsValidPosition(Vector3 position)
    {
        // Implement your logic to check if the position is valid
        return GridData.validPositions.Contains(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle collision with Car
        if (collision.CompareTag("Car"))
        {
            Debug.Log("Game Over Triggered by Monster Collision.");
            GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
            if (gameOverManager != null)
            {
                gameOverManager.GameOver();
            }
        }
    }
}