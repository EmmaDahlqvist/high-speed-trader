using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollower : MonoBehaviour, UIHitListener
{
    public float sensX = 400f;
    public float sensY = 400f;

    private float xRotation;
    private float yRotation;

    private bool lookingAtUI;
    private bool justStarted;

    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;  // Lås musen initialt
        Cursor.visible = true;  // Dölj musen initialt
        transform.rotation = Quaternion.Euler(0, 0, 0);
        justStarted = true;
    }

    public Rect mouseBounds = new Rect(100, 100, 800, 500); // (x, y, width, height)

    // Update is called once per frame
    void Update()
    {
        if(lookingAtUI)
        {
            Cursor.visible = true;
        } else
        {
            Cursor.visible = false;
        }


        // mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -20, 20); // left and right

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15, 5); // up and down

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
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
