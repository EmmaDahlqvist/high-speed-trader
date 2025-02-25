using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{

    private TextMeshProUGUI HighScoreText;

    private int highScore;
    private int level;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public int GetHighscore(int level)
    {
        string key = $"Highscore_Level_{level}";
        return PlayerPrefs.GetInt(key, 0);
    }

    
    public void SetHighScore(int score, int level)
    {
        string key = $"Highscore_Level_{level}";
        int currentHighscore = PlayerPrefs.GetInt(key, 0);

        if (score > currentHighscore)
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
        }
    }
   
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
