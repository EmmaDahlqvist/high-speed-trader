using UnityEngine;
using DG.Tweening; // Importera DoTween namespace
using System.Collections;

public class LockCameraZoom : MonoBehaviour
{
    public Camera mainCamera; // Huvudkameran
    public Transform zoomLocation; // Platsen d�r kameran ska zooma in till
    public float zoomDuration = 0.5f; // Hur l�ng tid zoom-effekten tar
    private Vector3 originalPosition; // Kamerans ursprungliga position
    private Quaternion originalRotation; // Kamerans ursprungliga rotation
    private Quaternion targetRotation; // Kamerans m�lrotation n�r den zoomar in
    public float targetXRotation = -5f;

    public ScreenSelector screenSelector;

    public CameraFollower cameraFollower;

    private bool isZoomedIn = false; // F�r att h�lla reda p� om kameran �r zoomad in eller inte

    void Start()
    {
        // Spara kamerans ursprungliga position och rotation
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation;

        // S�tt m�lets rotation (rakt fram)
        targetRotation = Quaternion.LookRotation(zoomLocation.position - mainCamera.transform.position);
        targetRotation = Quaternion.Euler(new Vector3(targetXRotation,0, 0));
    }

    void Update()
    {
        // Om du �r i n�gon annan screen �n meny screenen
        if (screenSelector.GetCurrentLevel() != 0 && !isZoomedIn)
        {
            ZoomIn();
        }
    }

    // Funktion f�r att zooma in
    public void ZoomIn()
    {
        cameraFollower.LockCamera();
        // Flytta kameran till zoom-location och rotera mot den
        mainCamera.transform.DOMove(zoomLocation.position, zoomDuration).SetEase(Ease.InOutQuad);
        mainCamera.transform.DORotateQuaternion(targetRotation, zoomDuration).SetEase(Ease.InOutQuad);

        isZoomedIn = true;
    }

    // Funktion f�r att zooma ut
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

    // Coroutine f�r att gradvis �terst�lla musens k�nslighet
    private IEnumerator RestoreSensitivityWithDelay()
    {
        // V�nta lite innan k�nsligheten �terst�lls
        yield return new WaitForSeconds(0.2f); // 0.2 sekunders f�rdr�jning innan �terst�llning av k�nsligheten

        StartCoroutine(cameraFollower.RestoreSensitivity()); // Anropa Coroutine f�r att gradvis �terst�lla k�nsligheten
    }


}
