using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    private CashManager cashManager;

    int score;
    public int multiplier;
    [FormerlySerializedAs("scoreLoweringPercent")] public float pps = 0.01f; // Percentage to lower the score per second
    private int maxScore; // Maximum score for percentage calculation

    public float minBet = 100;
    public float maxBet = 250;

    [Header("Wait for looking at balloon")]
    public bool wait = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cashManager = FindObjectOfType<CashManager>();
        score = cashManager.getLastRemovedCash() * multiplier;
        maxScore = score;

        UpdateScoreLoweringInterval();
        scoreText.text = score.ToString() + "$";
        if (wait) return;
        StartScore();
    }

    public void StartScore()
    {
        StartCoroutine(LowerScoreRoutine());
    }

    private IEnumerator LowerScoreRoutine()
    {
        while (score > 0)
        {
            yield return StartCoroutine(LowerScoreGradually());
            yield return new WaitForSeconds(1f); // Wait for 1 second before the next reduction
        }
    }

    private IEnumerator LowerScoreGradually()
    {
        int totalPointsToLower = Mathf.CeilToInt(maxScore * pps); // Calculate based on max score
        float totalTime = 1f; // Total time for the countdown
        float interval = totalTime / totalPointsToLower; // Interval between each point removal

        for (int i = 0; i < totalPointsToLower; i++)
        {
            if (score <= 0) break;
            score--;
            scoreText.text = score.ToString() + "$";
            yield return new WaitForSeconds(interval); // Adjust interval to control the speed of point removal
        }
    }

    private void UpdateScoreLoweringInterval()
    {
        float betAmount = Mathf.Clamp(cashManager.getLastRemovedCash(), minBet, maxBet);

        float t = Mathf.InverseLerp(minBet, maxBet, betAmount);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }
}