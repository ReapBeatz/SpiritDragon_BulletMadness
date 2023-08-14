using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private int currentScore = 0;
    private int highScore = 0;

    private void Start()
    {
        LoadHighScore(); 
        UpdateHighScoreUI(); 
    }

    public void AddScore(int score)
    {
        currentScore += score;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            UpdateHighScoreUI(); 
        }
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public int GetHighScore()
    {
        return highScore;
    }

    private void UpdateHighScoreUI()
    {
        
    }
}
