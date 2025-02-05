using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private bool sliding;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYScale = playerObj.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // start sliding if key down, has speed and is not crouching
        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && !pm.crouching)
            StartSlide();

        // if has no vertical/horizontal motion, start crouch instead

        else if (Input.GetKeyDown(slideKey) && !pm.crouching)
            pm.StartCrouching();

        if (Input.GetKeyUp(slideKey) && (sliding || pm.crouching))
        {
            StopSlideAndCrouch();
        }
    }

    private void FixedUpdate()
    {
        if(sliding)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        sliding = false;
        pm.StartCrouching();
    }

    private void StopSlideAndCrouch()
    {
        sliding = false;
        
        // start crouch instead
        pm.StopCrouching();
    }
}
