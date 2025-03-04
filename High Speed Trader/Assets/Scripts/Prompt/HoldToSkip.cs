using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldToSkip : MonoBehaviour
{
    public PlayerCam playerCam;
    public LookAtObject lookAtObject;

    [Tooltip("UI Image som ska fyllas (m�ste vara inst�lld p� Filled)")]
    public Image fillImage;

    [Tooltip("Hur l�ng tid (i sekunder) kr�vs f�r att h�lla F ned")]
    public float holdDuration = 2f;

    private bool isHolding = false;

    private bool isSkipped;

    public List<GameObject> crowd = new List<GameObject>();
    public GameObject crowdTeleportLocation;

    void Update()
    {
        if (isSkipped) return;
        // Kolla om F h�lls ned
        if (Input.GetKey(KeyCode.F))
        {
            if (!isHolding)
            {
                isHolding = true;
                // �terst�ll fillning och starta tween
                fillImage.fillAmount = 0;
                fillImage.DOFillAmount(1f, holdDuration).SetEase(Ease.Linear)
                         .OnComplete(OnHoldComplete);
            }
        }
        else
        {
            // Om F sl�pps innan tweenen �r klar, avbryt tweenen och �terst�ll fill
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

        if (lookAtObject != null)
        {
            lookAtObject.SkipIntro();
        }
        else
        {
            Debug.Log("Look at object script not assigned");

        }
        if (playerCam != null)
        {
            playerCam.SkipIntro();
        } else
        {
            Debug.Log("Player cam not assigned");
        }
        isSkipped = true;

        foreach(GameObject crowdObject in crowd)
        {
            crowdObject.transform.position = crowdTeleportLocation.transform.position;
        }
    }
}
