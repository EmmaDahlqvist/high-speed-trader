using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{

    private Camera _mainCam;
    public Transform playerCamera;
    public float distanceFromCamera = 2;

    [SerializeField] private GameObject UIPanel;

    [SerializeField] private TextMeshProUGUI _promptText;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
        UIPanel.SetActive(false);
    }

    private void Update()
    {
        if (playerCamera != null)
        {
            transform.position = playerCamera.position + playerCamera.forward * distanceFromCamera;
            transform.rotation = playerCamera.rotation; 
        }
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
