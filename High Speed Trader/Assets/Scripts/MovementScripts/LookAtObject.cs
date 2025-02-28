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
        // Spara den ursprungliga rotationen för camHolder
        Quaternion originalRotation = camHolder.rotation;

        // Beräkna riktningen till objektet (med full 3D-vinkel, inklusive höjd)
        Vector3 directionToLook = objectToLookAt.transform.position - cam.transform.position;

        // Rotera camHolder för att titta på objektet
        camHolder.DOLookAt(objectToLookAt.transform.position, moveTime)
            .SetEase(Ease.InOutQuad)
            .OnStart(() =>
            {
                // Visa prompten direkt när animationen startar
                promptCanvasGroup.alpha = 1f;
            })
            .OnComplete(() =>
            {
                // När kameran har tittat på objektet, återgå till originalrotationen för camHolder
                camHolder.DORotateQuaternion(originalRotation, moveTime)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        // Fade ut prompten när den sista animationen är klar
                        promptCanvasGroup.DOFade(0f, 0.5f);  // Fade out prompten över 1 sekund
                        Invoke("NotifyZoomDone", timeAfterDone);
                    });
            });

        ZoomInOnObject();
    }

    private void ZoomInOnObject ()
    {
        // Zooma in genom att minska field of view
        cam.DOFieldOfView(zoomFOV, zoomTime)
           .SetEase(Ease.InOutQuad)
           .OnComplete(() =>
           {
               // När zoomningen är klar, zooma tillbaka till original fov
               cam.DOFieldOfView(originalFov, zoomTime)
                      .SetEase(Ease.InOutQuad);
           });
    }


    // notify playercam script that zoom is done
    private void NotifyZoomDone()
    {
        playerCamScript.objectZoomWait = false;
        startPromptScript.wait = false;
        startPromptScript.StartPrompts();
        playerCamScript.TurnAroundRoutine();
        scoreManager.StartScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
