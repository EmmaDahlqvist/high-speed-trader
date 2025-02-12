using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class PlayerMovement : MonoBehaviour
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

    public Transform orientation;
    public Transform playerObj;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;


    public bool IsGrounded()
    {
        return grounded;
    }

    public enum MovementState
    {
        walking,
        sprinting,
        wallRunning,
        crouching,
        air
    }

    public bool wallRunning;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = playerObj.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.Raycast(playerObj.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch
        if(Input.GetKey(crouchKey))
        {
            StartCrouching();
        }

        // stop crouching
        if(Input.GetKeyUp(crouchKey))
        {
            StopCrouching();
        }
    }

    public bool crouching;

    // initiate crouch
    public void StartCrouching()
    {
        crouching = true;
        playerObj.localScale = new Vector3(playerObj.localScale.x, crouchYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Force);
    }

    // crouch movement
    private void Crouch()
    {
        state = MovementState.crouching;
        moveSpeed = crouchSpeed;
    }

    // stop the crouch
    public void StopCrouching()
    {
        crouching = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }

    private void StateHandler()
    {
        // Mode - WallRunning
        if(wallRunning)
        {
            state = MovementState.wallRunning;
            moveSpeed = wallRunSpeed; // if adding sliding, use desiredMoveSpeed here instead
        }

        // Mode - Crouching
        if(crouching)
        {
            Crouch();
        }

        // Sprinting
        else if (grounded && Input.GetKey(sprintKey) && state != MovementState.crouching)
        {
            state = MovementState.sprinting;
            if (speedCoroutine != null)
            {
                StopCoroutine(speedCoroutine);
            }
            speedCoroutine = StartCoroutine(GraduallyIncreaseSpeed(sprintSpeed));
        }
        // Walking
        else if (grounded)
        {
            state = MovementState.walking;
            if (speedCoroutine != null)
            {
                StopCoroutine(speedCoroutine);
            }
            moveSpeed = walkSpeed;
        }

        // Mode - air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        //in air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(playerObj.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private IEnumerator GraduallyIncreaseSpeed(float targetSpeed)
    {
        while (moveSpeed < targetSpeed)
        {
            moveSpeed += Time.deltaTime * 7f; // Adjust the increment value as needed
            yield return null;
        }
        moveSpeed = targetSpeed;
    }

    private bool IsTouchingWall()
    {
        RaycastHit hit;
        float checkDistance = 1f; // justera detta värde beroende på hur nära väggen spelaren ska vara för att fastna
        return Physics.Raycast(playerObj.position, transform.forward, out hit, checkDistance, whatIsGround);
    }

}
