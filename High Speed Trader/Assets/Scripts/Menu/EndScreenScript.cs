using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenScript : MonoBehaviour
{
    private TextMeshProUGUI deathText;
    private LevelInitalizer LevelInitalizer;
    
    // Start is called before the first frame update
    void Start()
    {
        // TODO here I want to see what my bet was going into the level that I failed on, and change the death Text accordingly - for later
        deathText = GameObject.Find("BetMoneyLost").GetComponent<TextMeshProUGUI>();
        deathText.text = "You died on level: " + 99; // TODO change this to the level you died on
        LevelInitalizer = FindObjectOfType<LevelInitalizer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int deathLevel; //for later TODO
    
    public void OnRestartButton()
    {
        // TODO logic to get which level you died on, so the correct level is loaded. (Dont have the logic yet).
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Single);
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(1));
        LevelInitalizer.StartLevel();
    }
    
    public void onBackToMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single);
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0));
    }
    
    
}
