using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{

    private TextMeshProUGUI HighScoreText;

    private int highScore;
    
    // Start is called before the first frame update
    void Start()
    {
        HighScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        UpdateHighScoreText();
    }
    
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        SaveHighScore();
    }
    
    public void UpdateHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
    }
    
    public void UpdateHighScoreText()
    {
        LoadHighScore();
        HighScoreText.text = "High Score: " + highScore + "$";
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
