using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayLevel : MonoBehaviour
{
    
    SliderBehaviour sliderBehaviour;
    CashManager cashManager;
    private Button playButton;
    
    // Start is called before the first frame update
    public void Start()
    {
        sliderBehaviour = FindObjectOfType<SliderBehaviour>();
        cashManager = FindObjectOfType<CashManager>();
        playButton = GetComponent<Button>();
        if (!sliderBehaviour.active)
            playButton.interactable = false;
    }
    
    public void Play()
    {
       cashManager.RemoveCash(sliderBehaviour.currentBet);
       UnityEngine.SceneManagement.SceneManager.LoadScene(6, UnityEngine.SceneManagement.LoadSceneMode.Single);
       //TODO load correct scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
