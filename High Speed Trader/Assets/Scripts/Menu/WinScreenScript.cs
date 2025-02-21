using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour
{
    
    private TextMeshProUGUI deathText;
    private LevelInitalizer LevelInitalizer;
    private CashManager cashManager;
    private HighScore highScore;
    private int lastScore;


    // Start is called before the first frame update
    void Start()
    {
        cashManager = FindObjectOfType<CashManager>();
        LevelInitalizer = FindObjectOfType<LevelInitalizer>();
        highScore = FindObjectOfType<HighScore>();
        deathText = GameObject.Find("BetMoneyWon").GetComponent<TextMeshProUGUI>();
        lastScore = PlayerPrefs.GetInt("Score", 0);

        
        deathText.text = "Your score: " + lastScore + "$" + "\n" + "Net return: " + (lastScore - cashManager.getLastRemovedCash()) + "$";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        

        // Reset CashManager state
    }
    
    public async void OnRestartButton()
    {
        cashManager.AddCash(lastScore);
        highScore.UpdateHighScore(lastScore);
        
        cashManager.AddCash(lastScore);
        highScore.UpdateHighScore(lastScore);
        //TODO reset the level, popup/idk?
        SceneManager.LoadScene("MenuLobby", LoadSceneMode.Single);
        await Task.Delay(5); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));

      
    }
    
    public async void onBackToMenuButton()
    {
        cashManager.AddCash(lastScore);
        highScore.UpdateHighScore(lastScore);
        SceneManager.LoadScene("MenuLobby", LoadSceneMode.Single);
        await Task.Delay(5); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
