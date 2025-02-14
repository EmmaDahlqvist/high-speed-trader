using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaptopInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    private ScoreManager scoreManager;
    
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public async void Interact(Interactor interactor)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(5, LoadSceneMode.Single);
        await Task.Delay(1); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(5));
        SceneManager.UnloadSceneAsync(currentSceneIndex);
    }
}
