using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
         
    public TextMeshProUGUI scoreText;
    private CashManager cashManager;
         
    int score;
    public int multiplier;
    int scoreLoweringRate = 1;
    float scoreLoweringInterval = 1f;
    public float minScoreLoweringInterval = 0.5f;
    public float maxScoreLoweringInterval = 2f;
    public float oneSecondLoweringBet = 100f;
         
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        instance = this;
        //last removed is basically just the last bet you made
        // scoreText.text = score.ToString() + "$";
    }
         
    // Start is called before the first frame update
    void Start()
    {
        cashManager = FindObjectOfType<CashManager>();
        score = cashManager.getLastRemovedCash() * multiplier;

        UpdateScoreLoweringInterval();
        scoreText.text = score.ToString() + "$";
        StartCoroutine(LowerScoreRoutine());
    }
         
    private IEnumerator LowerScoreRoutine()
    {
        while (score > 0)
        {
            LowerScore();
            yield return new WaitForSeconds(scoreLoweringInterval);
        }
    }

    public void LowerScore()
    {
        score -= scoreLoweringRate;
        scoreText.text = score.ToString() + "$";

        float threshold = cashManager.getLastRemovedCash() * 1.2f; // when at 120% of bet, start get red text 

        // Calculate the color based on the score
        float t = Mathf.InverseLerp(0, threshold, score);

        Color neonGreen = new Color(0.22f, 1f, 0.08f); // #39FF14

        Color color = Color.Lerp(Color.red, Color.green, t);
        scoreText.color = color;
    }


    private void UpdateScoreLoweringInterval()
    {
        float betAmount = cashManager.getLastRemovedCash();
        scoreLoweringInterval = Mathf.Clamp(oneSecondLoweringBet / Mathf.Max(betAmount, 1), minScoreLoweringInterval, maxScoreLoweringInterval);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }
}
