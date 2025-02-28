using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLogic : MonoBehaviour
{
    public float loadTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("LastLevel", 0);
        PlayerPrefs.Save();

        Invoke("LoadMainScene", loadTime);
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("MenuLobby");
    }
}
