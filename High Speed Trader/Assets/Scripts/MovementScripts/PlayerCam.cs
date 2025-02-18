using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;

    float xRotation;
    float yRotation;

    [Header("Start")]
    public float lookingBackTime = 2f;
    public float turnAroundTime = 1;
    bool lockedStart = true;


    private List<TurnAroundCompleteListener> turnAroundCompleteListeners = new List<TurnAroundCompleteListener>();

    // Start is called before the first frame update
    void Start()
    {
        // set up rotation from start:
        camHolder.rotation = Quaternion.Euler(0, 180, 0);
        orientation.rotation = Quaternion.Euler(0, 180, 0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // find player and their playermovement script to lock it
        GameObject player = GameObject.FindWithTag("PlayerComponent");

        if (player.GetComponent<PlayerMovement>() != null)
        {
            turnAroundCompleteListeners.Add(player.GetComponent<PlayerMovement>()); // add playermovement script as turn around listener
            turnAroundCompleteListeners.Add(player.GetComponent<StartPrompt>()); // add startprompt script as turn around listener
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(lockedStart)
        {
            if(lookingBackTime <= 0)
            {
                orientation.DORotate(Vector3.zero, turnAroundTime).OnComplete(() => TurnAroundComplete()); ; // turn the orientation back around in time
                camHolder.DORotate(Vector3.zero, turnAroundTime); // turn the camera back around in time
            }

            lookingBackTime -= Time.deltaTime;
        } 
        else
        {
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // rotate cam and orientation
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void TurnAroundComplete()
    {

        lockedStart = false;
        foreach(TurnAroundCompleteListener turnAroundCompleteListener in turnAroundCompleteListeners)
        {
            turnAroundCompleteListener.ActAfterTurn();
        }
    }

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}
