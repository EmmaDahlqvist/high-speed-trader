using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollower : MonoBehaviour, UIHitListener
{
    public float sensX = 400f;
    public float sensY = 400f;
    private float originalSensX;
    private float originalSensY;

    private float xRotation;
    private float yRotation;
    private bool lookingAtUI = false;

    public Camera playerCamera;

    public Rect mouseBounds = new Rect(100, 100, 800, 500); // (x, y, width, height)
    private bool cameraRotation = true;

    private bool lockCamera = false;

    public ScreenSelector screenSelector;

    // Start is called before the first frame update
    void Start()
    {
        originalSensX = sensX; // Spara original känslighet
        originalSensY = sensY; // Spara original känslighet

        //lockCamera = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // Unlock if in menu
        if(screenSelector.GetCurrentLevel() == 0)
        {
            StartCoroutine(DelayedUnlock(0.5f));
        }
    }

    private IEnumerator DelayedUnlock(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(UnlockCamera());
    }

    public void StopCameraRotation()
    {
        cameraRotation = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public void LockCamera()
    {
        lockCamera = true;
    }

    public IEnumerator UnlockCamera()
    {
        yield return new WaitForSeconds(0.2f);

        lockCamera = false;
        Cursor.lockState = CursorLockMode.Confined;  // Lås musen initialt
        Cursor.visible = true;  // Dölj musen initialt
        print("camera unlocked");
    }

    public void SyncRotationToCamera()
    {
        Vector3 currentEuler = Camera.main.transform.eulerAngles; // Ta rotationen från kameran
        xRotation = NormalizeAngle(currentEuler.x); // Fixar 360°-problem
        yRotation = NormalizeAngle(currentEuler.y);
    }

    // Ser till att värdena är inom ett korrekt intervall
    private float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }



    public IEnumerator RestoreSensitivity()
    {

        float targetSensX = originalSensX;
        float targetSensY = originalSensY;
        float elapsedTime = 0f;
        float transitionDuration = 3f;

        float startSensX = sensX;
        float startSensY = sensY;

        while (elapsedTime < transitionDuration)
        {
            sensX = Mathf.Lerp(startSensX, targetSensX, elapsedTime / transitionDuration);
            sensY = Mathf.Lerp(startSensY, targetSensY, elapsedTime / transitionDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sensX = originalSensX;
        sensY = originalSensY;
    }


    // Update is called once per frame
    void Update()
    {
        if (lockCamera) return;

        if(!cameraRotation)
        {
            return;
        }



        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            // mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -25, 35); // left and right

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -15, 15); // up and down

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        } else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }


    // camera hits UI element
    public void ActOnHit()
    {
        lookingAtUI = true;
    }

    public void NoLongerHit()
    {
        lookingAtUI = false;
    }
}
