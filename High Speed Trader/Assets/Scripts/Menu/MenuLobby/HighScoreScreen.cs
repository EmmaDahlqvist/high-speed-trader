using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreScreen : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI text;
    public void UpdateHighScore()
    {
        text.text = "HIGHEST TURNAROUND:";
        scoreText.text = PlayerPrefs.GetInt("HighScore") + "$";
    }

    public void RemoveAllText()
    {
        text.text = "";
        scoreText.text = "";
    }
}
