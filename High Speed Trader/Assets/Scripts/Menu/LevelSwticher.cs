using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwticher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void onLeftArrowButton()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Single);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(1));
        }
        else if (currentSceneIndex == 3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2, UnityEngine.SceneManagement.LoadSceneMode.Single);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(2));
        }
    }
    public void onRightArrowButton()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2, UnityEngine.SceneManagement.LoadSceneMode.Single);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(2));
        }
        else if (currentSceneIndex == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(3, UnityEngine.SceneManagement.LoadSceneMode.Single);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(3));
        }
    }
}
