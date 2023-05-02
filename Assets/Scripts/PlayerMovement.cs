using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float groundDrag;
    public float addSprintSpeed;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCD;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeedDeduct;
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



    [Header("Sound Effect")]
    //  public AudioSource walkSound;

    /// 
    private AttributesManager atm;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air,
        idle
    }

    public enum PowerUps
    {
        superspeed,
        addHP
    }


    private void Start()
    {
        readyToJump = true;
     //   isRunning = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        atm = GetComponent<AttributesManager>();
        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        GroundCheck();
        MyInput();
        SpeedControl();
        StateHandler();
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
       /* if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCD); // continously jump when holding jump key
        }*/

        //when to make walking sound
      //  bool isMovingOnGround = (horizontalInput != 0f || verticalInput != 0f) && grounded;
     //   if (isMovingOnGround)
           // walkSound.enabled = true;
       // else
           // walkSound.enabled = false;

        //start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

       
        // limit velocity
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        
    }

    private void Jump()
    {
        // reset y vel
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    //private void FootStepSoundControl()
   // {
     //   if (isRunning && readyToJump)
       //     footstepsSound.enabled = true;
      //  else
  //          footstepsSound.enabled = false;
 //   }

    private void StateHandler()
    {

        // Mode - Crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            //moveSpeed = crouchSpeed;
            moveSpeed = atm.GetSpeed() - crouchSpeedDeduct;
        }
        // Mode - Sprinting
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = atm.GetSpeed() + addSprintSpeed;
        }
        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = atm.GetSpeed();
        }
        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }
}
