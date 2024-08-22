using System.Collections.Generic;
using UnityEngine;

public static class GridData
{
    public static List<Vector3> validPositions = new List<Vector3>
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

    public static List<Vector3> obstaclePositions = new List<Vector3>();

    public static void AddValidPosition(Vector3 position)
    {
        if (!validPositions.Contains(position))
        {
            validPositions.Add(position);
        }
    }

    public static void RemoveValidPosition(Vector3 position)
    {
        validPositions.Remove(position);
    }

    public static void AddObstaclePosition(Vector3 position)
    {
        if (!obstaclePositions.Contains(position))
        {
            obstaclePositions.Add(position);
        }
    }

    public static void RemoveObstaclePosition(Vector3 position)
    {
        obstaclePositions.Remove(position);
    }
}
