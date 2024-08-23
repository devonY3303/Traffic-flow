using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Slider timerSlider;
    public TMP_Text timerText;
    public float gameTime;

    private float remainingTime;
    private bool stopTimer;

    public GameOverManager gameOverManager; // Reference to the GameOverManager

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopTimer)
        {
            return;
        }

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            stopTimer = true;
            TimerEnded();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        string textTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = textTime;
        timerSlider.value = remainingTime;
    }

    private void TimerEnded()
    {
        if (gameOverManager != null)
        {
            gameOverManager.GameOver(); // Trigger game over
        }
        else
        {
            Debug.LogError("GameOverManager is not assigned. Please assign it in the Inspector.");
        }
    }

    // Method to reset the timer
    public void ResetTimer()
    {
        remainingTime = gameTime;
        timerSlider.maxValue = gameTime;
        timerSlider.value = remainingTime;
        stopTimer = false;
    }
}
