using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreScreen : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI text;
    public HighScore highScore;
    private CashManager cashManager;


    public void UpdateHighScore(int level)
    {
        if(level == 0)
        {
            MenuText();
            return;
        }
        if(level == -1)
        {
            GameOverText();
            return;
        }
        text.text = "HIGHSCORE:";
        scoreText.text = highScore.GetHighscore(level).ToString() + "$";
    }

    public void MenuText()
    {
        text.text = "LOGIN";
        scoreText.text = "...";
    }

    public void GameOverText()
    {
        cashManager = FindObjectOfType<CashManager>();
        text.text = "New Balance:";
        scoreText.text = "100$";
        
    }
    
}
