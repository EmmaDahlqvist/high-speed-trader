using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RightClickTutorial : MonoBehaviour
{
    public Image mouseIcon; // UI Image for mouse icon
    public Image rightClickIndicator; // Right click highlight
    public TextMeshProUGUI tutorialText;
    public float pulseSpeed = 1.5f;
    private ScreenSelector screenSelector;

    private bool hasInteracted = false;
    private Vector3 originalScale;

    void Start()
    {
        screenSelector = gameObject.GetComponent<ScreenSelector>();

        // Ensure UI elements are visible at start
        mouseIcon.gameObject.SetActive(true);
        rightClickIndicator.gameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);
        originalScale = rightClickIndicator.transform.localScale; // Sparar startskalan
    }

    void Update()
    {
        // Create a pulsing effect for right-click indicator
        float scaleFactor = 1.0f + Mathf.Sin(Time.time * pulseSpeed) * 0.1f;
        rightClickIndicator.transform.localScale = originalScale * scaleFactor;

        // Check if player holds right-click
        if (Input.GetMouseButton(1) || screenSelector.GetCurrentLevel() != 0)
        {
            HideTutorial();
        } else
        {
            ShowTutorial();
        }
    }

    void HideTutorial()
    {
        hasInteracted = true;
        mouseIcon.gameObject.SetActive(false);
        rightClickIndicator.gameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);
    }

    void ShowTutorial()
    {
        mouseIcon.gameObject.SetActive(true);
        rightClickIndicator.gameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);
    }
}