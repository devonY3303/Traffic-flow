/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            transform.Translate(0, 1.1f, 0);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            transform.Translate(0, -1.1f, 0);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            transform.Translate(-1.1f, 0, 0);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.Translate(1.1f, 0, 0);
    }
}*/

/*using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float movementSpeed = 5f;  // Speed of the car's movement

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object is a grid and move in the corresponding direction
        if (collision.gameObject.CompareTag("Grid"))
        {
            // Get the GridDirection component from the collided grid object
            GridDirection gridDirection = collision.gameObject.GetComponent<GridDirection>();

            // Check which direction the grid is set to
            if (gridDirection != null)
            {
                MoveInDirection(gridDirection.direction);
            }
        }
    }

    void MoveInDirection(Vector3 direction)
    {
        // Move the car in the specified direction
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}*/

using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveDistance = 1.1f; // Distance to move in grid units
    public float moveSpeed = 5f;    // Speed of the car movement

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position;
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
}

