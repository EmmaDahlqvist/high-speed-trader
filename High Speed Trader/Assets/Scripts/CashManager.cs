using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    private int cash;
    private int lastRemovedCash;
    private int lastAddedCash;

    // Start is called before the first frame update
    void Start()
    {
        LoadCash();
    }

    public void AddCash(int amount)
    {
        Debug.Log("AddCash called with amount: " + amount);
        Debug.Log("Call Stack: " + System.Environment.StackTrace);

        
        cash += amount;
        lastAddedCash = amount;
        PlayerPrefs.SetInt("LastAddedCash", lastAddedCash);
        SaveCash();
    }
    
    public int getLastAddedCash()
    {
        LoadCash();
        return lastAddedCash;
    }

    public void RemoveCash(int amount)
    {
        if (cash - amount < 0)
        {
            Debug.Log("Not enough cash");
            return;
        }
        cash -= amount;
        lastRemovedCash = amount;
        PlayerPrefs.SetInt("LastRemovedCash", lastRemovedCash);
        SaveCash();
    }
    
    public int getLastRemovedCash()
    {
        LoadCash();
        return lastRemovedCash;
    }

    public int GetCash()
    {
        LoadCash();
        return cash;
    }
    
    public void SetCash(int amount)
    {
        cash = amount;
        SaveCash();
    }

    private void SaveCash()
    {
        PlayerPrefs.SetInt("PlayerCash", cash);
        PlayerPrefs.Save();
    }

    private void LoadCash()
    {
        cash = PlayerPrefs.GetInt("PlayerCash", 150);
        lastRemovedCash = PlayerPrefs.GetInt("LastRemovedCash", 0);
        lastAddedCash = PlayerPrefs.GetInt("LastAddedCash", 0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}