using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndHighScore : MonoBehaviour
{
    public Text totalHighScoreText;
    private HighScore highScoreManager;

    private void Start()
    {
        highScoreManager = FindObjectOfType<HighScore>();
        DisplayTotalHighScore();
    }

    private void DisplayTotalHighScore()
    {
        int totalHighScore = highScoreManager.GetHighScore();
        totalHighScoreText.text = "Total High Score: " + totalHighScore.ToString();
    }
}
