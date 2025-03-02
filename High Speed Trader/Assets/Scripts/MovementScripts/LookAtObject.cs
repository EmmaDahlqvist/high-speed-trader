using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public GameObject objectToLookAt;
    public float moveTime = 2f;
    public float zoomInFactor = 1f;
    public Camera cam;
    public Transform orientation;
    public Transform camHolder;
    private PlayerCam playerCamScript;
    public float zoomFOV = 10f;
    public float zoomTime = 2f;

    public float timeBeforeStartLooking = 1f;
    public float timeAfterDone = 1f;

    public ScoreManager scoreManager;

    private StartPrompt startPromptScript;

    public CanvasGroup promptCanvasGroup;

    private float originalFov;

    public List<AIControl> aiControls = new List<AIControl>();
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        startPromptScript = FindObjectOfType<StartPrompt>();
        startPromptScript.wait = true;
        zoomTime = moveTime;

        playerCamScript = cam.GetComponent<PlayerCam>();
        originalFov = cam.fieldOfView;

        Invoke("StartLooking", 1f);
    }

    private void StartLooking()
    {
        // Spara den ursprungliga rotationen f�r camHolder
        Quaternion originalRotation = camHolder.rotation;

        // Ber�kna riktningen till objektet (med full 3D-vinkel, inklusive h�jd)
        Vector3 directionToLook = objectToLookAt.transform.position - cam.transform.position;

        // Rotera camHolder f�r att titta p� objektet
        camHolder.DOLookAt(objectToLookAt.transform.position, moveTime)
            .SetEase(Ease.InOutQuad)
            .OnStart(() =>
            {
                // Visa prompten direkt n�r animationen startar
                promptCanvasGroup.alpha = 1f;
            })
            .OnComplete(() =>
            {
                // N�r kameran har tittat p� objektet, �terg� till originalrotationen f�r camHolder
                camHolder.DORotateQuaternion(originalRotation, moveTime)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        // Fade ut prompten n�r den sista animationen �r klar
                        Invoke("NotifyZoomDone", timeAfterDone);
                    });
            }).SetId("LookTween");

        ZoomInOnObject();
    }

    private void StartAIControls()
    {
        foreach (AIControl aiControl in aiControls)
        {
            aiControl.wait = false;
            aiControl.Initiate();
        }
    }

    private void ZoomInOnObject ()
    {
        // Zooma in genom att minska field of view
        cam.DOFieldOfView(zoomFOV, zoomTime)
           .SetEase(Ease.InOutQuad)
           .OnComplete(() =>
           {
               // N�r zoomningen �r klar, zooma tillbaka till original fov
               cam.DOFieldOfView(originalFov, zoomTime)
                      .SetEase(Ease.InOutQuad);
           }).SetId("ZoomTween");
    }


    // notify playercam script that zoom is done
    private void NotifyZoomDone()
    {
        print("notifying done");
        promptCanvasGroup.DOFade(0f, 0.5f);  // Fade out prompten �ver 1 sekund
        playerCamScript.objectZoomWait = false;
        startPromptScript.wait = false;
        startPromptScript.StartPrompts();
        playerCamScript.TurnAroundRoutine();
        scoreManager.StartScore();
        StartAIControls();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SkipIntro()
    {
        DOTween.Kill("ZoomTween");
        DOTween.Kill("LookTween");
        promptCanvasGroup.alpha = 0;
        NotifyZoomDone();
    }
}
