using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private List<GameObject> activeObstacles = new List<GameObject>();

    public void SpawnRandomObstacles(int count)
    {
        Debug.Log("Spawning obstacles");
        for (int i = 0; i < count; i++)
        {
            if (GridData.validPositions.Count > 0)
            {
                int randomIndex = Random.Range(0, GridData.validPositions.Count);
                Vector3 obstaclePosition = GridData.validPositions[randomIndex];

                GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
                activeObstacles.Add(obstacle);
                GridData.RemoveValidPosition(obstaclePosition);
                GridData.AddObstaclePosition(obstaclePosition);

                Debug.Log("Obstacle spawned at: " + obstaclePosition);
            }
        }
    }

    public void ClearObstacles()
    {
        Debug.Log("Clearing obstacles");
        foreach (GameObject obstacle in activeObstacles)
        {
            if (obstacle != null)
            {
                Destroy(obstacle);
            }
        }
        activeObstacles.Clear();

        foreach (Vector3 position in GridData.obstaclePositions)
        {
            GridData.AddValidPosition(position);
        }
        GridData.obstaclePositions.Clear();
    }
}
