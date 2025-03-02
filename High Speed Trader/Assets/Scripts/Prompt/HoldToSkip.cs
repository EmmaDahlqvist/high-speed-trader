using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldToSkip : MonoBehaviour
{
    public PlayerCam playerCam;
    public LookAtObject lookAtObject;

    [Tooltip("UI Image som ska fyllas (måste vara inställd på Filled)")]
    public Image fillImage;

    [Tooltip("Hur lång tid (i sekunder) krävs för att hålla F ned")]
    public float holdDuration = 2f;

    private bool isHolding = false;

    private bool isSkipped;

    void Update()
    {
        if (isSkipped) return;
        // Kolla om F hålls ned
        if (Input.GetKey(KeyCode.F))
        {
            if (!isHolding)
            {
                isHolding = true;
                // Återställ fillning och starta tween
                fillImage.fillAmount = 0;
                fillImage.DOFillAmount(1f, holdDuration).SetEase(Ease.Linear)
                         .OnComplete(OnHoldComplete);
            }
        }
        else
        {
            // Om F släpps innan tweenen är klar, avbryt tweenen och återställ fill
            if (isHolding)
            {
                isHolding = false;
                DOTween.Kill(fillImage);
                fillImage.fillAmount = 0;
            }
        }
    }

    void OnHoldComplete()
    {
        fillImage.gameObject.SetActive(false);
        if(playerCam != null)
        {
            playerCam.SkipIntro();
        }
        if(lookAtObject != null)
        {
            lookAtObject.SkipIntro();
        }
        isSkipped = true;
    }
}
