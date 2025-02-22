using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    public TextMeshProUGUI loadingText; // referens till din TextMeshProUGUI-komponent
    public float typingSpeed = 0.1f; // justera hastigheten på skrivningen

    void Start()
    {
        AnimateText();
    }

    void AnimateText()
    {
        // För att skapa en skrivningseffekt med DOTween måste vi bygga en sekvens.
        string fullText = "Loading..."; // Sätt din text här

        // Skapa en sekvens där varje bokstav visas en i taget
        loadingText.text = ""; // Töm texten så att vi börjar från början
        DOTween.To(() => loadingText.text,
                   x => loadingText.text = x,
                   fullText,
                   fullText.Length * typingSpeed).SetEase(Ease.Linear);
    }
}
