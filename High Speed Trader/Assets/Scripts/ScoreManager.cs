using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;

    int score;
    int initialScore = 100;
    int multiplier = 10;
    int scoreLoweringRate = 1;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        instance = this;
        score = initialScore * multiplier;
        scoreText.text = score.ToString() + "$";
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString() + "$";
        StartCoroutine(LowerScoreRoutine());
    }

    private IEnumerator LowerScoreRoutine()
    {
        while (score > 0)
        {
            LowerScore();
            yield return new WaitForSeconds(1/2f);
        }
    }

    public void LowerScore()
    {
        score -= scoreLoweringRate;
        scoreText.text = score.ToString() + "$";
    }
}
