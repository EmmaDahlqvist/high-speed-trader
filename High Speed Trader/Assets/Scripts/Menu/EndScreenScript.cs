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
        deathText.text = "You died! You lost: " + cashManager.getLastRemovedCash() + "$";
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
        LevelInitalizer = FindObjectOfType<LevelInitalizer>();
        SceneManager.LoadScene("MenuLobby", LoadSceneMode.Single);
        await Task.Delay(5); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
    }
    
    public async void onBackToMenuButton()
    {
        levelManager.SetLastLevel(0);
        SceneManager.LoadScene("MenuLobby", LoadSceneMode.Single);
        await Task.Delay(5); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuLobby"));
    }
    
    
}
