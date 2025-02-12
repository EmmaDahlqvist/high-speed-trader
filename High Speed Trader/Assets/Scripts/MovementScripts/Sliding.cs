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
    public float maxAirSlideBoost = 20;
    public float minAirSlideBoost = 5;
    public float airSlideBoostHeight = 2;
    private float slideTimer;
    private float initialSlideSpeed;

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

        AirSliding();
    }

    private bool wasGrounded; // track if player was grounded
    private float fallStartHeight;
    private float extraSlideSpeed = 0;

    // make player get more slide speed if they jump from higher distance
    private void AirSliding()
    {
        bool currentlyGrounded = pm.IsGrounded(); // ground status

        // when player leaves ground, track height
        if (wasGrounded && !currentlyGrounded)
        {
            fallStartHeight = transform.position.y;
        }

        //when player lands
        if (!wasGrounded && currentlyGrounded && airSliding)
        {
            float fallDistance = fallStartHeight - transform.position.y;

            if (fallDistance > airSlideBoostHeight)
            {
                extraSlideSpeed = Mathf.Clamp(fallDistance, minAirSlideBoost, maxAirSlideBoost);
            } else
            {
                extraSlideSpeed = 0; // no extra speed if not high enough fall
            }

            airSliding = false;
        }

        wasGrounded = currentlyGrounded;
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
        initialSlideSpeed = rb.velocity.magnitude; // Record the initial slide speed

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;

        if(!pm.IsGrounded())
        {
            airSliding = true;
        }
    }

    private bool airSliding = false;
    private float slideSpeed;

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

        slideTimer -= Time.deltaTime;

        slideSpeed = Mathf.Lerp(initialSlideSpeed + extraSlideSpeed, 0, 1 - (slideTimer / maxSlideTime));

        // only modify speed when player is on ground
        if (pm.IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.normalized.x * slideSpeed, rb.velocity.y, rb.velocity.normalized.z * slideSpeed);
        }

        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        sliding = false;
        pm.StartCrouching();
        extraSlideSpeed = 0;
    }

    private void StopSlideAndCrouch()
    {
        sliding = false;
        
        // start crouch instead
        pm.StopCrouching();
        extraSlideSpeed = 0;
    }
}
