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

    void Start()
    {
        // Set up button listeners
        Button upButton = GameObject.Find("UpButton").GetComponent<Button>();
        Button downButton = GameObject.Find("DownButton").GetComponent<Button>();
        Button leftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        Button rightButton = GameObject.Find("RightButton").GetComponent<Button>();

        upButton.onClick.AddListener(() => SelectGridToPlace(upGridPrefab));
        downButton.onClick.AddListener(() => SelectGridToPlace(downGridPrefab));
        leftButton.onClick.AddListener(() => SelectGridToPlace(leftGridPrefab));
        rightButton.onClick.AddListener(() => SelectGridToPlace(rightGridPrefab));
    }

    void Update()
    {
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
        mousePosition.z = 0;

        Vector3 nearestPosition = GetNearestValidPosition(mousePosition);

        if (Vector3.Distance(mousePosition, nearestPosition) <= snapThreshold)
        {
            GameObject placedTile = Instantiate(gridToPlace, nearestPosition, Quaternion.identity);
            placedTiles.Add(placedTile);
            gridToPlace = null;
        }
        else
        {
            Debug.Log("Not close enough to a valid position!");
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
