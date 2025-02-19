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
        Cursor.lockState = CursorLockMode.Locked;  // Lås musen initialt
        Cursor.visible = false;  // Dölj musen initialt
        transform.rotation = Quaternion.Euler(0, 0, 0);
        justStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(justStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(!lookingAtUI)
                {
                    Cursor.lockState = CursorLockMode.Locked; // lock
                    Cursor.visible = false;
                    transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                }
                justStarted = false;
            } else
            {
                return;
            }
        }
        */

        if(lookingAtUI)
        {
            Cursor.lockState = CursorLockMode.None;  // unlock
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked; // lock
            Cursor.visible = false;
        }


        // mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -80, 80); // left and right

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -10, 10); // up and down

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
