using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PromptTrigger : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public GameObject promptImage;
    public string message;

    // to make sure they dont get the prompt twice
    private bool hasBeenPrompted = false;

    // Start is called before the first frame update
    void Start()
    {
        //dont show from start
        promptImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenPrompted)
        {
            promptText.text = message;
            promptImage.SetActive(true);
            hasBeenPrompted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            promptImage.SetActive(false);
        }
    }
}
