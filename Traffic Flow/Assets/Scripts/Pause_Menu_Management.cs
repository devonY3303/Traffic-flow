using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu_Management : MonoBehaviour
{
    public GameObject pausePanel; // Reference to the Pause Panel
    public MonoBehaviour[] componentsToDisable; // Components to disable when the game is paused

    private bool isPaused = false;

    void Start()
    {
        // Ensure the pause panel is hidden at the start
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            if (pausePanel != null)
            {
                pausePanel.SetActive(true); // Show the pause menu
            }
            DisableComponents(true);
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            if (pausePanel != null)
            {
                pausePanel.SetActive(false); // Hide the pause menu
            }
            DisableComponents(false);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        DisableComponents(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene
    }

    private void DisableComponents(bool disable)
    {
        foreach (var component in componentsToDisable)
        {
            if (component != null)
            {
                component.enabled = !disable; // Enable or disable the component
            }
        }
    }
}
