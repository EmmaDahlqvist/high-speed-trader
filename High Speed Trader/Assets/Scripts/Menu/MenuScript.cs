using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement; // Add me!!

public class MenuScript : MonoBehaviour
{

    
    public async void OnPlayButton()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        await Task.Delay(1); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }
    
    public void OnQuitButton()
    {
        Application.Quit();
    }
    
}
