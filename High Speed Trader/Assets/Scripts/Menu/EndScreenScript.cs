using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    private TextMeshProUGUI deathText;
    private LevelInitalizer LevelInitalizer;
    private CashManager cashManager;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = transform.GetComponent<LevelManager>();
        cashManager = FindObjectOfType<CashManager>();
        // TODO here I want to see what my bet was going into the level that I failed on, and change the death Text accordingly - for later
        deathText = GameObject.Find("BetMoneyLost").GetComponent<TextMeshProUGUI>();
        deathText.text = GameState.KillReason + " You lost: " + cashManager.getLastRemovedCash() + "$";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    public async void OnRestartButton()
    {
        if (CheckGameOver())
        {
            // cashManager.SetCash(100);
            // PlayerPrefs.SetInt("Toggle", 0);
            // Debug.Log("ToggleVal: " + PlayerPrefs.GetInt("Toggle"));
            if (CheckToggle())
            {
                levelManager.SetLastLevel(levelManager.GetLastLevel());
                var loadOperation = SceneManager.LoadSceneAsync("MenuLobby", LoadSceneMode.Single);
                while (!loadOperation.isDone)
                {
                    await Task.Yield();
                }
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
            }
            else
            {
                await LoadGameOverScreen();
            }
        }
        else
        {
            levelManager.SetLastLevel(levelManager.GetLastLevel());
            var loadOperation = SceneManager.LoadSceneAsync("MenuLobby", LoadSceneMode.Single);
            while (!loadOperation.isDone)
            {
                await Task.Yield();
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
        }
    }

    private async Task LoadGameOverScreen()
    {
        levelManager.SetLastLevel(-1);
        var loadOperation = SceneManager.LoadSceneAsync("MenuLobby", LoadSceneMode.Single);
        while (!loadOperation.isDone)
        {
            await Task.Yield();
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
    }

    public async void onBackToMenuButton()
    {
        if (CheckGameOver())
        {
            cashManager.SetCash(100);
            if (CheckToggle())
            {
                levelManager.SetLastLevel(0);
                var loadOperation = SceneManager.LoadSceneAsync("MenuLobby", LoadSceneMode.Single);
                while (!loadOperation.isDone)
                {
                    await Task.Yield();
                }
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
            }
            else
            {
                await LoadGameOverScreen();
            }
        }
        else
        {
            levelManager.SetLastLevel(0);
            var loadOperation = SceneManager.LoadSceneAsync("MenuLobby", LoadSceneMode.Single);
            while (!loadOperation.isDone)
            {
                await Task.Yield();
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
        }
       
    }

    private bool CheckGameOver() => cashManager.GetCash() < 100;
    
    private bool CheckToggle() => PlayerPrefs.GetInt("Toggle") == 1;
    
    
}
