using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshPro

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public ObstacleManager obstacleManager; // Reference to the ObstacleManager
    public GameTimer gameTimer;

    public Score_Management scoreManager; // Reference to the ScoreManager
    public TMP_Text scoreText; // Reference to the TextMeshPro element for displaying the score

    public GameObject monsterPrefab;
    public int numberOfMonsters = 1;

    void Start()
    {
        gameOverPanel.SetActive(false); // Ensure the panel is hidden at the start
        if (obstacleManager == null)
        {
            obstacleManager = FindObjectOfType<ObstacleManager>();
            if (obstacleManager == null)
            {
                Debug.LogError("ObstacleManager not found in the scene. Please add an ObstacleManager to the scene.");
            }
        }

        if (gameTimer == null)
        {
            gameTimer = FindObjectOfType<GameTimer>();
            if (gameTimer == null)
            {
                Debug.LogError("GameTimer not found in the scene. Please add a GameTimer to the scene.");
            }
        }

        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<Score_Management>();
            if (scoreManager == null)
            {
                Debug.LogError("ScoreManager not found in the scene. Please add a ScoreManager to the scene.");
            }
        }

        if (scoreText == null)
        {
            Debug.LogError("ScoreText not assigned. Please assign a TextMeshPro object to display the score.");
        }

        SpawnMonsters();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true); // Show the game over panel
        Time.timeScale = 0f; // Pause the game

        if (scoreManager != null && scoreText != null)
        {
            scoreText.text = "Score: " + scoreManager.getScore(); // Assuming GetScore() returns the current score
        }
    }

    public void RestartGame()
    {

        Time.timeScale = 1f; // Resume time

        if (obstacleManager != null)
        {
            obstacleManager.ClearObstacles();
        }

        GridData.ResetGridData(); // Reset the grid data

        
        if (gameTimer != null)
        {
            gameTimer.ResetTimer();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene (replace with your scene name)
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            if (GridData.validPositions.Count > 0)
            {
                int randomIndex = Random.Range(0, GridData.validPositions.Count);
                Vector3 spawnPosition = GridData.validPositions[randomIndex];
                Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}