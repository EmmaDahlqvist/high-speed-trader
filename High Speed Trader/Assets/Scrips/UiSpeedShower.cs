using UnityEngine;
using TMPro;

public class UiSpeedShower : MonoBehaviour
{
    public TextMeshProUGUI SpeedText; // Reference to the TextMeshProUGUI component
    public Rigidbody playerRigidbody; // Reference to the player's Rigidbody

    // Update is called once per frame
    void Update()
    {
        // Calculate the player's speed
        float speed = playerRigidbody.velocity.magnitude;

        // Update the TextMeshProUGUI component with the current speed
        SpeedText.text = "Speed: " + speed.ToString("F2");
    }
}