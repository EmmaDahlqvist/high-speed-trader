using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class WinScreenScript : MonoBehaviour
{
    
    private TextMeshProUGUI deathText;
    private LevelInitalizer LevelInitalizer;
    private CashManager cashManager;
    private int lastScore;


    // Start is called before the first frame update
    void Start()
    {
        cashManager = FindObjectOfType<CashManager>();
        LevelInitalizer = FindObjectOfType<LevelInitalizer>();
        deathText = GameObject.Find("BetMoneyWon").GetComponent<TextMeshProUGUI>();
        lastScore = PlayerPrefs.GetInt("Score", 0);
  

        
        deathText.text = "You sold all your stocks! You earned: " + lastScore + "$";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        

        // Reset CashManager state
    }
    
    public async void OnRestartButton()
    {
        cashManager.AddCash(lastScore);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(8, UnityEngine.SceneManagement.LoadSceneMode.Single);
        await Task.Delay(1); // Delay for 1 millisecond
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(8));
        LevelInitalizer.StartLevel();

      
    }
    
    public async void onBackToMenuButton()
    {
        cashManager.AddCash(lastScore);
        UnityEngine.SceneManagement.SceneManager.LoadScene(8, UnityEngine.SceneManagement.LoadSceneMode.Single);
        await Task.Delay(1); // Delay for 1 millisecond
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(8));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
