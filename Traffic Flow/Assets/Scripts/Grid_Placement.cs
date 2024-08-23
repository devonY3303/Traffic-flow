using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridPlacer : MonoBehaviour
{
    public GameObject upGridPrefab;
    public GameObject downGridPrefab;
    public GameObject leftGridPrefab;
    public GameObject rightGridPrefab;

    private GameObject gridToPlace;
    private List<GameObject> placedTiles = new List<GameObject>();
    public float snapThreshold = 0.5f;
    private CarMovement carMovement;

    void Start()
    {
        // Find the CarMovement script
        carMovement = FindObjectOfType<CarMovement>();
        if (carMovement == null)
        {
            Debug.LogError("CarMovement script not found in the scene. Please add a Car with the CarMovement script.");
        }

        // Set up button listeners
        Button upButton = GameObject.Find("UpButton").GetComponent<Button>();
        Button downButton = GameObject.Find("DownButton").GetComponent<Button>();
        Button leftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        Button rightButton = GameObject.Find("RightButton").GetComponent<Button>();

        upButton.onClick.AddListener(() => OnDirectionButtonPressed(GridDirection.Direction.Up, upGridPrefab));
        downButton.onClick.AddListener(() => OnDirectionButtonPressed(GridDirection.Direction.Down, downGridPrefab));
        leftButton.onClick.AddListener(() => OnDirectionButtonPressed(GridDirection.Direction.Left, leftGridPrefab));
        rightButton.onClick.AddListener(() => OnDirectionButtonPressed(GridDirection.Direction.Right, rightGridPrefab));
    }

    void OnDirectionButtonPressed(GridDirection.Direction direction, GameObject gridPrefab)
    {
        if (carMovement != null)
        {
            Vector3 carPosition = carMovement.transform.position;

            // Place the grid underneath the car
            TryPlaceGrid(gridPrefab, carPosition);

            // Move the car in the specified direction
            carMovement.MoveInDirection(direction);
        }
    }

    void TryPlaceGrid(GameObject gridPrefab, Vector3 position)
    {
        Vector3 nearestPosition = GetNearestValidPosition(position);

        if (Vector3.Distance(position, nearestPosition) <= snapThreshold)
        {
            GameObject placedTile = Instantiate(gridPrefab, nearestPosition, Quaternion.identity);
            placedTiles.Add(placedTile);
        }
        else
        {
            Debug.Log("Car is not close enough to a valid position!");
        }
    }

    Vector3 GetNearestValidPosition(Vector3 currentPosition)
    {
        Vector3 nearestPosition = GridData.validPositions[0];
        float smallestDistance = Vector3.Distance(currentPosition, nearestPosition);

        foreach (Vector3 validPosition in GridData.validPositions)
        {
            float distance = Vector3.Distance(currentPosition, validPosition);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                nearestPosition = validPosition;
            }
        }

        return nearestPosition;
    }

    public void ClearPlacedTiles()
    {
        foreach (GameObject tile in placedTiles)
        {
            Destroy(tile);
        }
        placedTiles.Clear();
    }
}
