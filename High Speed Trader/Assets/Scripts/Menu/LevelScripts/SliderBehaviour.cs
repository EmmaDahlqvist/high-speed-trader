using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("MoneyRelatedStuff")]
    private int minBet;
    private int maxBet;
    public int currentBet;
    private int currentCash;
    private CashManager cashManager;
    private Array minBets = new int[] { 100, 500, 2000 };
    private Array maxBets = new int[] { 250, 1000, 10000 };

    [Header("Slider")]
    private int currentLevel = 1;
    public Slider betSlider; // Reference to the Slider component
    public TextMeshProUGUI currentBetText;
    public bool active = true;


    public void SetLevel(int level)
    {
        currentLevel = level;
    }

    public void SetSlider(Slider slider)
    {
        betSlider = slider;
    }
    
    public void Start()
    {
        cashManager = FindObjectOfType<CashManager>();
        currentCash = cashManager.GetCash();
        currentBetText = GameObject.Find("CurrentBet").GetComponent<TextMeshProUGUI>();
        //currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        betSlider = GetComponent<Slider>();
        minBet = (int)minBets.GetValue(currentLevel-1);
        maxBet = (int)maxBets.GetValue(currentLevel-1);
        
        // Set the slider's min and max values
        betSlider.minValue = minBet;
        betSlider.maxValue = maxBet;
        initializeSlider();

        
        // Add a listener to handle value changes
        betSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void InitializeSlider(int level, Slider slider)
    {
        minBet = (int)minBets.GetValue(level);
        maxBet = (int)maxBets.GetValue(level);
        betSlider = slider;

        slider.minValue = minBet;
        slider.maxValue = maxBet;

        initializeSlider();
    } 

    public void initializeSlider()
    {
       
        if (currentCash < minBet)
        {
            betSlider.value = currentCash;
            currentBetText.text = currentCash.ToString() + " $";
            betSlider.interactable = false; // Disable the slider
            active = false;
            currentBet = minBet;
        }
        else if (currentCash >= minBet)
        {
            betSlider.value = minBet;
            currentBetText.text = minBet + " $";
            currentBet = minBet;
        }
    }
    
    void OnSliderValueChanged(float value)
    {
        if (value > currentCash)
        {
            betSlider.value = currentCash;
            currentBet = currentCash;
            currentBetText.text = currentBet.ToString() + " $";
            return;
        }
        currentBet = (int)value; 
        currentBetText.text = currentBet.ToString() + " $";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
