using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentBalance : MonoBehaviour
{
    private TextMeshProUGUI CurrentBalanceValue;
    private CashManager cashManager;

    // Start is called before the first frame update
    void Start()
    {
        CurrentBalanceValue = GetComponent<TextMeshProUGUI>();

        cashManager = FindObjectOfType<CashManager>();

        CurrentBalanceValue.text = cashManager.GetCash().ToString() + " $";
    }

    // Update is called once per frame
    void Update()
    {

    }
}