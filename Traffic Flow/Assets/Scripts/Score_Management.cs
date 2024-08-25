using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score_Management : MonoBehaviour
{
    public TMP_Text scoreText;  // Reference to the UI Text component
    private int score = 0;  // Initial score

    void Start()
    {
        UpdateScoreText();
    }

    // Method to increment the score
    public void AddPoint()
    {
        score+=5;
        UpdateScoreText();
    }

    // Method to update the UI text with the current score
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Method to get the current score
    public int getScore()
    {
        return score;
    }
}
