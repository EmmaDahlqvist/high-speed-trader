using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PromptTrigger : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public GameObject promptImage;
    public string message;
    public float fadeDistance = 3f; // Maxavst�nd fr�n zonens mitt d�r prompten fortfarande syns lite
    public float fadeSpeed = 2f; // Hur snabbt den fadar
    private CanvasGroup canvasGroup;
    private Transform player;
    private bool playerInZone = false;
    private Vector3 zoneCenter; // Mitten av zonen
    private float zoneRadius; // Storleken p� trigger-zonen

    void Start()
    {
        promptText.SetText(message);
        canvasGroup = promptImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = promptImage.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f; // make invisible
        promptImage.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Ber�kna zonens mittpunkt och storlek
        Collider col = GetComponent<Collider>();
        zoneCenter = col.bounds.center;
        zoneRadius = col.bounds.extents.magnitude; // Anv�nd storlek p� zonen
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, zoneCenter);

        if (distance <= zoneRadius) // Spelaren �r inne i zonen
        {
            playerInZone = true;
            canvasGroup.alpha = 1f; // Full synlighet
        }
        else // Spelaren �r utanf�r zonen
        {
            playerInZone = false;
            float fadeFactor = Mathf.Clamp01(1 - ((distance - zoneRadius) / fadeDistance));
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, fadeFactor, Time.deltaTime * fadeSpeed);
        }
    }
}
