using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLogic : MonoBehaviour
{
    public float loadTime = 2f;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("LastLevel", 0);
        PlayerPrefs.Save();

        levelManager = GetComponent<LevelManager>();

        Invoke("LoadMainScene", loadTime);
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("MenuLobby");
    }
}
