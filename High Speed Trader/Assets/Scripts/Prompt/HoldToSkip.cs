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

    public List<GameObject> crowd = new List<GameObject>();
    public GameObject crowdTeleportLocation;

    private void Start()
    {
        fillImage.gameObject.SetActive(false);
        print("not active");
    }

    bool active;

    void Update()
    {
        if (isSkipped)
        {
            return;
        }

        if(IsKeyboardKeyPressed())
        {
            fillImage.gameObject.SetActive(true);
            active = true;
        }

        if (!active) return;

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

    public void SetSkipped()
    {
        print("skipped");
        isSkipped = true;
        fillImage.gameObject.SetActive(false);
    }

    void OnHoldComplete()
    {
        SetSkipped();

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

        foreach(GameObject crowdObject in crowd)
        {
            crowdObject.transform.position = crowdTeleportLocation.transform.position;
        }
    }

    private bool IsKeyboardKeyPressed()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            // Hoppa över musknappar och joystick-knappar
            if (key >= KeyCode.Mouse0 && key <= KeyCode.Mouse6) continue;
            if (key >= KeyCode.JoystickButton0 && key <= KeyCode.Joystick8Button19) continue;

            if (Input.GetKey(key))
            {
                return true; // Returnera true om en tangentbordsinput registreras
            }
        }
        return false; // Returnera false om inget tangentbordsinput sker
    }

}
