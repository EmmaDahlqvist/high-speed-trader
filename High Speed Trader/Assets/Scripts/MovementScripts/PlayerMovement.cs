using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class PlayerMovement : MonoBehaviour, TurnAroundCompleteListener
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float wallRunSpeed;

    public float groundDrag;

    private Coroutine speedCoroutine;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope check")]
    public float maxStableAngle = 15f;

    public Transform orientation;
    public Transform playerObj;

    float horizontalInput;
    float verticalInput;

    private PlayerMovementListener landingListener;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    private bool lockedStart;

    // Flagga för att hantera hoppet i FixedUpdate
    private bool jumpRequested;

    public void addLandingListener(PlayerMovementListener playerMovementListener)
    {
        landingListener = playerMovementListener;
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    public enum MovementState
    {
        standingStill,
        walking,
        sprinting,
        wallRunning,
        sliding,
        vaulting,
        crouching,
        air
    }

    public bool wallRunning;
    public bool sliding;
    private bool wasInAir = false;
    private bool landed;
    public bool vaulting;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        startYScale = playerObj.localScale.y;
        lockedStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        grounded = Physics.Raycast(playerObj.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        // alternativt: grounded = Physics.CheckSphere(groundCheck.position, 0.2f, whatIsGround);

        if(grounded)
        {
            coyoteTimeCounter = coyoteTime;
        } else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (!grounded && measuringJump)
        {
            // Uppdatera maxJumpHeight med den högsta y-positionen som nås
            maxJumpHeight = Mathf.Max(maxJumpHeight, playerObj.position.y);
        }

        if (lockedStart) return;

        MyInput();
        SpeedControl();
        StateHandler();

        // Hantera drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        if (lockedStart) return;

        // Utför hoppet i FixedUpdate om flaggan är satt
        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
            coyoteTimeCounter = 0;
        }

        MovePlayer();
    }

    private float coyoteTime = 0.08f;
    private float coyoteTimeCounter;

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Sätt hoppsflaggan vid knapptryck (använder GetKeyDown för att få en engångshändelse)
        if (Input.GetKey(jumpKey) && readyToJump && coyoteTimeCounter > 0f)
        {
            jumpRequested = true;
        }

        // Starta crouch
        if (Input.GetKey(crouchKey))
        {
            StartCrouching();
        }

        // Stoppa crouch
        if (Input.GetKeyUp(crouchKey))
        {
            StopCrouching();
        }
    }

    public bool crouching;

    // Initiera crouch
    public void StartCrouching()
    {
        crouching = true;
        playerObj.localScale = new Vector3(playerObj.localScale.x, crouchYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Force);
    }

    // Crouch-rörelse
    private void Crouch()
    {
        state = MovementState.crouching;
        moveSpeed = crouchSpeed;
    }

    // Avsluta crouch
    public void StopCrouching()
    {
        crouching = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }

    private void StateHandler()
    {
        // Mode - vaulting
        if (vaulting)
        {
            state = MovementState.vaulting;
            return;
        }

        // Spelaren var i luften
        if (!wasInAir && !grounded)
        {
            wasInAir = true; // Spelaren är i luften
        }
        else if (wasInAir && grounded) // Var i luften men har landat
        {
            wasInAir = false;

            if (measuringJump)
            {
                float jumpHeight = maxJumpHeight - jumpStartHeight;
                //Debug.Log("Jump height: " + jumpHeight);
                measuringJump = false;
            }
            if (landingListener != null)
            {
                landingListener.PlaySound();
            }
        }

        // Mode - WallRunning
        if (wallRunning)
        {
            state = MovementState.wallRunning;
            moveSpeed = wallRunSpeed;
        }
        // Mode - Står stilla
        if (grounded && horizontalInput == 0 && verticalInput == 0)
        {
            state = MovementState.standingStill;
        }
        // Mode - Sliding
        else if (sliding && grounded)
        {
            state = MovementState.sliding;
        }
        // Mode - Crouching
        else if (crouching)
        {
            Crouch();
        }
        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey) && state != MovementState.crouching)
        {
            state = MovementState.sprinting;
            if (speedCoroutine != null)
            {
                StopCoroutine(speedCoroutine);
            }
            speedCoroutine = StartCoroutine(GraduallyIncreaseSpeed(sprintSpeed));
        }
        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            if (speedCoroutine != null)
            {
                StopCoroutine(speedCoroutine);
            }
            moveSpeed = walkSpeed;
        }
        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // Beräkna rörelseriktningen
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // På marken
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        // I luften
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Begränsa hastigheten om det behövs
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private float jumpStartHeight;
    private float maxJumpHeight;
    private bool measuringJump;

    private void Jump()
    {
        // Återställ y-komponenten av hastigheten
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(playerObj.up * jumpForce, ForceMode.Impulse);

        // Spara start-höjden och initiera mätningen
        jumpStartHeight = playerObj.position.y;
        maxJumpHeight = jumpStartHeight;
        measuringJump = true;

        // Sätt hopp-köld och inaktivera hopp tills cooldown har passerat
        readyToJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private IEnumerator GraduallyIncreaseSpeed(float targetSpeed)
    {
        while (moveSpeed < targetSpeed)
        {
            moveSpeed += Time.deltaTime * 7f; // Justera ökningen efter behov
            yield return null;
        }
        moveSpeed = targetSpeed;
    }

    private bool IsTouchingWall()
    {
        RaycastHit hit;
        float checkDistance = 1f; // Justera detta värde beroende på hur nära väggen spelaren ska vara
        return Physics.Raycast(playerObj.position, transform.forward, out hit, checkDistance, whatIsGround);
    }

    public void ActAfterTurn()
    {
        // Lås upp spelarens rörelse efter att de har vänt sig
        lockedStart = false;
    }
}
