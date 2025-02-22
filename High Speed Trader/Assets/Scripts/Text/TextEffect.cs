using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    public TextMeshProUGUI loadingText; // referens till din TextMeshProUGUI-komponent
    public float typingSpeed = 0.1f; // justera hastigheten p� skrivningen

    void Start()
    {
        AnimateText();
    }

    void AnimateText()
    {
        // F�r att skapa en skrivningseffekt med DOTween m�ste vi bygga en sekvens.
        string fullText = "Loading..."; // S�tt din text h�r

        // Skapa en sekvens d�r varje bokstav visas en i taget
        loadingText.text = ""; // T�m texten s� att vi b�rjar fr�n b�rjan
        DOTween.To(() => loadingText.text,
                   x => loadingText.text = x,
                   fullText,
                   fullText.Length * typingSpeed).SetEase(Ease.Linear);
    }
}
