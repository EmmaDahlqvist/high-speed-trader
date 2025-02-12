using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add me!!

public class MenuScript : MonoBehaviour
{

    
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }
    
    public void OnQuitButton()
    {
        Application.Quit();
    }
    
}
