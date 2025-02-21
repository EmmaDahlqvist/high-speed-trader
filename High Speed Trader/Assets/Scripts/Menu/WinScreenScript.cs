using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
{
    
    private TextMeshProUGUI deathText;
    private LevelInitalizer levelInitializer;
    private CashManager cashManager;
    private int lastScore;
    private HighScore highScore;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        highScore = transform.GetComponent<HighScore>();
        levelManager = transform.GetComponent<LevelManager>();

        cashManager = FindObjectOfType<CashManager>();
        levelInitializer = FindObjectOfType<LevelInitalizer>();
        deathText = GameObject.Find("BetMoneyWon").GetComponent<TextMeshProUGUI>();
        lastScore = PlayerPrefs.GetInt("Score", 0);

        
        deathText.text = "Your score: " + lastScore + "$" + "\n" + "Net return: " + (lastScore - cashManager.getLastRemovedCash()) + "$";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        highScore.SetHighScore(lastScore, levelManager.GetLastLevel());

        

        // Reset CashManager state
    }
    
    public async void OnRestartButton()
    {
        cashManager.AddCash(lastScore);
        //TODO reset the level, popup/idk?
        SceneManager.LoadScene("MenuLobby", LoadSceneMode.Single);
        await Task.Delay(5); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));

      
    }
    
    public async void onBackToMenuButton()
    {
        levelManager.SetLastLevel(0);
        cashManager.AddCash(lastScore);
        SceneManager.LoadScene("MenuLobby", LoadSceneMode.Single);
        await Task.Delay(5); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
