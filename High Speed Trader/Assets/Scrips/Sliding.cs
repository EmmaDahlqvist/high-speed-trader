using System;
using UnityEngine;


public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    private Rigidbody rb;
    public Transform playerObj;
    private PlayerMovement pm;
    
    [Header("Sliding")]
    public float slideForce;
    public float maxSlideTimer;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;
    
    [Header("Keybinds")]
    public KeyCode slideKey = KeyCode.C;
    private float horizontalInput;
    private float verticalInput;

    private bool sliding;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = playerObj.GetComponent<PlayerMovement>();
        startYScale = playerObj.localScale.y;
    }
    
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if(Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
        {
            startSlide();
        }
        
        if(Input.GetKeyUp(slideKey) && sliding)
        {
            stopSlide();
        }
        SlidingMovement();
    }

    private void FixedUpdate()
    {
        if(sliding)
            SlidingMovement();
    }

    private void startSlide()
    {
        sliding = true;
        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        slideTimer = maxSlideTimer;
    }
    
    private void stopSlide()
    {
        sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
    
    private void SlidingMovement()
    {
       Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
       
       rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Impulse);
       rb.velocity = new Vector3(rb.velocity.x * 0.98f, rb.velocity.y, rb.velocity.z * 0.98f);
       slideTimer -= Time.deltaTime;
       
       if (slideTimer <= 0)
       {
           stopSlide();
       
       }
       
    }
}