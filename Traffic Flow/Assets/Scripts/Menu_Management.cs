using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Management : MonoBehaviour
{
    public GameObject helpPanel; // Reference to the Help Panel

    void Start()
    {
        // Ensure the help panel is hidden at the start
        helpPanel.SetActive(false);
    }

    public void OpenHelpPanel()
    {
        helpPanel.SetActive(true); // Show the help panel
    }

    public void CloseHelpPanel()
    {
        helpPanel.SetActive(false); // Hide the help panel
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
