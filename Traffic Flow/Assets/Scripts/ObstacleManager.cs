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
        for (int i = 0; i < count; i++)
        {
            if (GridData.validPositions.Count > 0)
            {
                Vector3 obstaclePosition = Vector3.zero; // Initialize obstaclePosition
                int attempts = 0;
                bool validPositionFound = false;

                while (attempts < 200)
                {
                    int randomIndex = Random.Range(0, GridData.validPositions.Count);
                    obstaclePosition = GridData.validPositions[randomIndex];
                    attempts++;

                    if (obstaclePosition != carPosition && // Ensure obstacle is not at the car's position
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
                    Debug.LogWarning("No valid position found for obstacle after 100 attempts.");
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
        foreach (var obstacle in activeObstacles)
        {
            Destroy(obstacle);
        }
        activeObstacles.Clear();
    }
}
