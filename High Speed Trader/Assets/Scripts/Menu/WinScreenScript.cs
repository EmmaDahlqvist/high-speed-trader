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

    // Start is called before the first frame update
    void Start()
    {
        cashManager = FindObjectOfType<CashManager>();
        // TODO here I want to see what my bet was going into the level that I failed on, and change the death Text accordingly - for later
        deathText = GameObject.Find("BetMoneyWon").GetComponent<TextMeshProUGUI>();
        cashManager.AddCash(cashManager.GetCash());
        deathText.text = "You sold all your stocks! You earned: " + cashManager.getLastAddedCash() + "$";
        LevelInitalizer = FindObjectOfType<LevelInitalizer>();
    }
    private int deathLevel; //for later TODO
    
    public async void OnRestartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Single);
        await Task.Delay(1); // Delay for 1 millisecond

        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(1);
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);

        LevelInitalizer = FindObjectOfType<LevelInitalizer>();
        if (LevelInitalizer != null)
        {
            LevelInitalizer.StartLevel();
        }
        else
        {
            Debug.LogWarning("LevelInitalizer not found in the scene.");
        }
    }
    
    public async void onBackToMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single);
        await Task.Delay(1); // Delay for 1 millisecond
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
