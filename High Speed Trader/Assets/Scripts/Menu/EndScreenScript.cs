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

    private int deathLevel; //for later TODO
    
    public async void OnRestartButton()
    {
        var loadOperation = SceneManager.LoadSceneAsync("MenuLobby", LoadSceneMode.Single);
        while (!loadOperation.isDone)
        {
            await Task.Yield();
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
    }
    
    public async void onBackToMenuButton()
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
