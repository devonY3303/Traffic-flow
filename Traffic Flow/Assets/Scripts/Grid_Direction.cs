/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Grid : MonoBehaviour
{
    public Transform car;  // Reference to the object you want to move
    public Vector3 movementDirection = new Vector3(0, 1.1f, 0);  // Direction to move the object
    public float movementSpeed = 5f;  // Speed of the movement

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object has collided with the specific object you want
        if (collision.gameObject.CompareTag("Car"))
        {
            // Start moving the object
            StartCoroutine(MoveObject());
        }
    }

    IEnumerator MoveObject()
    {
        float elapsedTime = 0;
        while (elapsedTime < 1f)  // Move the object for 2 seconds
        {
            car.Translate(movementDirection * movementSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}*/

/*using UnityEngine;

public class GridDirection : MonoBehaviour
{
    public Vector3 direction;  // Direction the car should move when it touches this grid

    void Start()
    {
        // You can manually set this in the inspector or use the following as examples:
        Up: direction = new Vector3(0, 0, 1);
        // Down: direction = new Vector3(0, 0, -1);
        // Left: direction = new Vector3(-1, 0, 0);
        // Right: direction = new Vector3(1, 0, 0);
    }
}*/

using UnityEngine;

public class GridDirection : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }
    public Direction gridDirection;
}

