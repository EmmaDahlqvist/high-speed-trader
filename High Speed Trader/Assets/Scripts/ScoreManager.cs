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

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }
}
