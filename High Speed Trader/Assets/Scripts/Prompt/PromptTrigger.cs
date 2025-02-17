using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PromptTrigger : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public GameObject promptImage;
    public string message;
    public float fadeDistance = 3f; // Maxavstånd från zonens mitt där prompten fortfarande syns lite
    public float fadeSpeed = 2f; // Hur snabbt den fadar
    private CanvasGroup canvasGroup;
    private Transform player;
    private bool playerInZone = false;
    private Vector3 zoneCenter; // Mitten av zonen
    private float zoneRadius; // Storleken på trigger-zonen

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

        // Beräkna zonens mittpunkt och storlek
        Collider col = GetComponent<Collider>();
        zoneCenter = col.bounds.center;
        zoneRadius = col.bounds.extents.magnitude; // Använd storlek på zonen
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, zoneCenter);

        if (distance <= zoneRadius) // Spelaren är inne i zonen
        {
            playerInZone = true;
            canvasGroup.alpha = 1f; // Full synlighet
        }
        else // Spelaren är utanför zonen
        {
            playerInZone = false;
            float fadeFactor = Mathf.Clamp01(1 - ((distance - zoneRadius) / fadeDistance));
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, fadeFactor, Time.deltaTime * fadeSpeed);
        }
    }
}
