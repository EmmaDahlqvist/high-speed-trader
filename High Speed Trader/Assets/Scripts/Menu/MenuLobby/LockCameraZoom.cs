using UnityEngine;
using DG.Tweening; // Importera DoTween namespace
using System.Collections;

public class LockCameraZoom : MonoBehaviour
{
    public Camera mainCamera; // Huvudkameran
    public Transform zoomLocation; // Platsen där kameran ska zooma in till
    public float zoomDuration = 0.5f; // Hur lång tid zoom-effekten tar
    private Vector3 originalPosition; // Kamerans ursprungliga position
    private Quaternion originalRotation; // Kamerans ursprungliga rotation
    private Quaternion targetRotation; // Kamerans målrotation när den zoomar in
    public float targetXRotation = -5f;

    public ScreenSelector screenSelector;

    public CameraFollower cameraFollower;

    private bool isZoomedIn = false; // För att hålla reda på om kameran är zoomad in eller inte

    void Start()
    {
        // Spara kamerans ursprungliga position och rotation
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation;

        // Sätt målets rotation (rakt fram)
        targetRotation = Quaternion.LookRotation(zoomLocation.position - mainCamera.transform.position);
        targetRotation = Quaternion.Euler(new Vector3(targetXRotation,0, 0));
    }

    void Update()
    {
        // Om du är i någon annan screen än meny screenen
        if (screenSelector.GetCurrentLevel() != 0 && !isZoomedIn)
        {
            ZoomIn();
        }
    }

    // Funktion för att zooma in
    public void ZoomIn()
    {
        cameraFollower.LockCamera();
        // Flytta kameran till zoom-location och rotera mot den
        mainCamera.transform.DOMove(zoomLocation.position, zoomDuration).SetEase(Ease.InOutQuad);
        mainCamera.transform.DORotateQuaternion(targetRotation, zoomDuration).SetEase(Ease.InOutQuad);

        isZoomedIn = true;
    }

    // Funktion för att zooma ut
    public void ZoomOut()
    {
        mainCamera.transform.DOMove(originalPosition, zoomDuration).SetEase(Ease.InOutQuad);
        mainCamera.transform.DORotateQuaternion(originalRotation, zoomDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => {
                cameraFollower.SyncRotationToCamera();
                StartCoroutine(cameraFollower.UnlockCamera());
            });

        StartCoroutine(RestoreSensitivityWithDelay());
        isZoomedIn = false;
    }

    // Coroutine för att gradvis återställa musens känslighet
    private IEnumerator RestoreSensitivityWithDelay()
    {
        // Vänta lite innan känsligheten återställs
        yield return new WaitForSeconds(0.2f); // 0.2 sekunders fördröjning innan återställning av känsligheten

        StartCoroutine(cameraFollower.RestoreSensitivity()); // Anropa Coroutine för att gradvis återställa känsligheten
    }


}
