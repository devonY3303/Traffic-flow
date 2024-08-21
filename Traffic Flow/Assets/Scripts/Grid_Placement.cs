using UnityEngine;
using UnityEngine.UI;

public class GridPlacer : MonoBehaviour
{
    public GameObject upGridPrefab;
    public GameObject downGridPrefab;
    public GameObject leftGridPrefab;
    public GameObject rightGridPrefab;

    private GameObject gridToPlace;

    // Define the valid positions for the grid map
    private Vector3[] validPositions = new Vector3[]
    {
        new Vector3(-2.2f, 0f, 0f),
        new Vector3(-1.1f, 0f, 0f),
        new Vector3(0f, 0f, 0f),
        new Vector3(1.1f, 0f, 0f),
        new Vector3(2.2f, 0f, 0f),

        new Vector3(-2.2f, -1.1f, 0f),
        new Vector3(-1.1f, -1.1f, 0f),
        new Vector3(0f, -1.1f, 0f),
        new Vector3(1.1f, -1.1f, 0f),
        new Vector3(2.2f, -1.1f, 0f),

        //Added 3 extra rows
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

        new Vector3(-2.2f, -4.4f, 0f),
        new Vector3(-1.1f, -4.4f, 0f),
        new Vector3(0f, -4.4f, 0f),
        new Vector3(1.1f, -4.4f, 0f),
        new Vector3(2.2f, -4.4f, 0f),
    };

    public float snapThreshold = 0.5f; // How close the click needs to be to a valid position

    void Start()
    {
        // Get the buttons from the UI
        Button upButton = GameObject.Find("UpButton").GetComponent<Button>();
        Button downButton = GameObject.Find("DownButton").GetComponent<Button>();
        Button leftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        Button rightButton = GameObject.Find("RightButton").GetComponent<Button>();

        // Assign button click listeners
        upButton.onClick.AddListener(() => SelectGridToPlace(upGridPrefab));
        downButton.onClick.AddListener(() => SelectGridToPlace(downGridPrefab));
        leftButton.onClick.AddListener(() => SelectGridToPlace(leftGridPrefab));
        rightButton.onClick.AddListener(() => SelectGridToPlace(rightGridPrefab));
    }

    void Update()
    {
        // Check if the player clicked the mouse button and is placing a grid
        if (Input.GetMouseButtonDown(0) && gridToPlace != null)
        {
            TryPlaceGrid();
        }
    }

    void SelectGridToPlace(GameObject gridPrefab)
    {
        gridToPlace = gridPrefab;
    }

    void TryPlaceGrid()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the grid is placed at the correct Z-axis level in a 2D game

        // Find the nearest valid position
        Vector3 nearestPosition = GetNearestValidPosition(mousePosition);

        // Check if the mouse is close enough to the nearest valid position
        if (Vector3.Distance(mousePosition, nearestPosition) <= snapThreshold)
        {
            Debug.Log($"Attempting to place grid at: {nearestPosition}");
            Instantiate(gridToPlace, nearestPosition, Quaternion.identity);
            gridToPlace = null; // Reset gridToPlace to null to prevent additional placement
        }
        else
        {
            Debug.Log("Not close enough to a valid position!");
        }
    }

    Vector3 GetNearestValidPosition(Vector3 currentPosition)
    {
        Vector3 nearestPosition = validPositions[0];
        float smallestDistance = Vector3.Distance(currentPosition, nearestPosition);

        foreach (Vector3 validPosition in validPositions)
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
}
