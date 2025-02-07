using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptStationary : MonoBehaviour
{

    public float offsetVertical = 2;
    public float offsetHorizontal = 2;

    [SerializeField] private GameObject UIPanel;

    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private Transform objectToAttachTo;


    // Start is called before the first frame update
    void Start()
    {
        UIPanel.SetActive(false);
        // Skapa en offset baserat på objektets riktning
        Vector3 offset = objectToAttachTo.up * offsetVertical - objectToAttachTo.forward * offsetHorizontal;

        // Uppdatera positionen med offset
        transform.position = objectToAttachTo.position + offset;

        transform.rotation = objectToAttachTo.rotation * Quaternion.Euler(0, 180, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isDisplayed = false;
    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        UIPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        UIPanel.SetActive(false);
        isDisplayed = false;
    }
}
