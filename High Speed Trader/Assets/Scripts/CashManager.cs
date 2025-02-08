using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    private int cash;

    // Start is called before the first frame update
    void Start()
    {
        LoadCash();
    }

    public void AddCash(int amount)
    {
        cash += amount;
        SaveCash();
    }

    public void RemoveCash(int amount)
    {
        if (cash - amount < 0)
        {
            Debug.Log("Not enough cash");
            return;
        }
        cash -= amount;
        SaveCash();
    }

    public int GetCash()
    {
        return cash;
    }

    private void SaveCash()
    {
        PlayerPrefs.SetInt("PlayerCash", cash);
        PlayerPrefs.Save();
    }

    private void LoadCash()
    {
        cash = PlayerPrefs.GetInt("PlayerCash", 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}