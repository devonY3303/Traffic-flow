using System.Collections.Generic;
using UnityEngine;

public static class GridData
{
    // Initial valid positions for the grid
    private static readonly List<Vector3> initialValidPositions = new List<Vector3>
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
        new Vector3(2.2f, -3.3f, 0f)
    };

    // Current valid positions
    public static List<Vector3> validPositions = new List<Vector3>(initialValidPositions);

    // Current obstacle positions
    public static List<Vector3> obstaclePositions = new List<Vector3>();

    // Add a valid position to the grid
    public static void AddValidPosition(Vector3 position)
    {
        if (!validPositions.Contains(position))
        {
            validPositions.Add(position);
        }
    }

    // Remove a valid position from the grid
    public static void RemoveValidPosition(Vector3 position)
    {
        validPositions.Remove(position);
    }

    // Add an obstacle position to the grid
    public static void AddObstaclePosition(Vector3 position)
    {
        if (!obstaclePositions.Contains(position))
        {
            obstaclePositions.Add(position);
        }
    }

    // Remove an obstacle position from the grid
    public static void RemoveObstaclePosition(Vector3 position)
    {
        obstaclePositions.Remove(position);
    }

    // Reset the grid data to the initial state
    public static void ResetGridData()
    {
        // Clear the current valid positions and restore the initial positions
        validPositions.Clear();
        validPositions.AddRange(initialValidPositions);

        // Clear the current obstacle positions
        obstaclePositions.Clear();
    }
}
