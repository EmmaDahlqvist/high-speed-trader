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
        deathText = GameObject.Find("BetMoneyWon").GetComponent<TextMeshProUGUI>();
        cashManager.AddCash(PlayerPrefs.GetInt("Score"));
        deathText.text = "You sold all your stocks! You earned: " + PlayerPrefs.GetInt("Score") + "$";
        LevelInitalizer = FindObjectOfType<LevelInitalizer>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
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
