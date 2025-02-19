using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class EndScreenScript : MonoBehaviour
{
    private TextMeshProUGUI deathText;
    private LevelInitalizer LevelInitalizer;
    private CashManager cashManager;
    
    // Start is called before the first frame update
    void Start()
    {
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Single);
        await Task.Delay(1); // Delay for 1 millisecond
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(1));
        LevelInitalizer.StartLevel();
    }
    
    public async void onBackToMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(7, UnityEngine.SceneManagement.LoadSceneMode.Single);
        await Task.Delay(1); // Delay for 1 millisecond
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(7));
    }
    
    
}
