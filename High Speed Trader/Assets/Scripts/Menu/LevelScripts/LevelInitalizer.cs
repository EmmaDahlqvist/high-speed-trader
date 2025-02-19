using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitalizer : MonoBehaviour
{
    [Header("Classes That Need Initializing At Each Switch")]
    SliderBehaviour sliderBehaviour;
    CurrentBalance currentBalance;
    PlayLevel playLevel;
    private int currentLevel;
    
    
    // Start is called before the first frame update
    public void StartLevel()
    {
        
       
        currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        CashManager cashManager = FindObjectOfType<CashManager>();
        sliderBehaviour = FindObjectOfType<SliderBehaviour>();
        currentBalance = FindObjectOfType<CurrentBalance>();
        playLevel = FindObjectOfType<PlayLevel>();




        sliderBehaviour.Start();
        currentBalance.Start();
        playLevel.Start();


    }
    void Start()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (sceneName != "WinScreen" && sceneName != "EndScreen")
        {
            StartLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
