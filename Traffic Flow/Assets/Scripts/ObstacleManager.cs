using UnityEngine;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;  // Prefab for obstacles
    private List<GameObject> activeObstacles = new List<GameObject>(); // List of active obstacles

    public void SpawnRandomObstacles(int count, Vector3 carPosition, Vector3 destinationPosition)
    {
        ClearObstacles();
        Debug.Log("Spawning obstacles");

        List<Vector3> centerTiles = new List<Vector3>
        {
            new Vector3(-1.1f, 0f, 0f),
            new Vector3(0f, 0f, 0f),
            new Vector3(1.1f, 0f, 0f),
            new Vector3(-1.1f, -1.1f, 0f),
            new Vector3(0f, -1.1f, 0f),
            new Vector3(1.1f, -1.1f, 0f),
            new Vector3(-1.1f, -2.2f, 0f),
            new Vector3(0f, -2.2f, 0f),
            new Vector3(1.1f, -2.2f, 0f)
        };

        List<Vector3> forbiddenPositions = new List<Vector3>
        {
            carPosition,
            destinationPosition,
            new Vector3(carPosition.x + 1.1f, carPosition.y, carPosition.z),
            new Vector3(carPosition.x - 1.1f, carPosition.y, carPosition.z),
            new Vector3(carPosition.x, carPosition.y + 1.1f, carPosition.z),
            new Vector3(carPosition.x, carPosition.y - 1.1f, carPosition.z),
            new Vector3(destinationPosition.x + 1.1f, destinationPosition.y, destinationPosition.z),
            new Vector3(destinationPosition.x - 1.1f, destinationPosition.y, destinationPosition.z),
            new Vector3(destinationPosition.x, destinationPosition.y + 1.1f, destinationPosition.z),
            new Vector3(destinationPosition.x, destinationPosition.y - 1.1f, destinationPosition.z)
        };

        for (int i = 0; i < count; i++)
        {
            if (centerTiles.Count > 0)
            {
                Vector3 obstaclePosition = Vector3.zero;
                bool validPositionFound = false;

                for (int attempts = 0; attempts < 200; attempts++)
                {
                    int randomIndex = Random.Range(0, centerTiles.Count);
                    obstaclePosition = centerTiles[randomIndex];

                    if (!forbiddenPositions.Contains(obstaclePosition) &&
                        !IsAdjacent(obstaclePosition, carPosition) &&
                        !IsAdjacent(obstaclePosition, destinationPosition))
                    {
                        validPositionFound = true;
                        break;
                    }
                }

                if (validPositionFound)
                {
                    GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
                    activeObstacles.Add(obstacle);
                    GridData.RemoveValidPosition(obstaclePosition);
                    GridData.AddObstaclePosition(obstaclePosition);

                    Debug.Log("Obstacle spawned at: " + obstaclePosition);
                }
                else
                {
                    Debug.LogWarning("No valid position found for obstacle after 200 attempts.");
                    break;
                }
            }
        }
    }

    private bool IsAdjacent(Vector3 position1, Vector3 position2)
    {
        float distance = Vector3.Distance(position1, position2);
        return distance <= 1.1f; // Assuming that adjacent positions are within 1.1 units
    }

    public void ClearObstacles()
    {
        // Destroy all active obstacles in the scene
        foreach (var obstacle in activeObstacles)
        {
            Destroy(obstacle);
        }

        // Clear the list of active obstacles
        activeObstacles.Clear();

        // Reset the grid data (valid positions and obstacle positions)
        GridData.ResetGridData();

        Debug.Log("Obstacles cleared and grid data reset.");
    }
}
