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
        SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
        await Task.Delay(5); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("WinScreen"));
    }
}
