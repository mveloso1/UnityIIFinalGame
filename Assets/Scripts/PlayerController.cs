//using System.Collections.Specialized;
//using System.Security.Cryptography;
//using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    Vector2 moveInput;
    Vector3 velocity;


    [SerializeField] Transform cameraTransform;

    [Header("Jump Variables")]
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] private AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0, 0, 1,1);
    [SerializeField] private int totalJumps = 1;
    private int jumpsRemaining;
    private float jumpTime;


    private bool isJumping = false;
    private float groundCheckDistance = 0.1f;
    private bool isGrounded = false;

    [Header("Dash Variables")]
    [SerializeField] float dashCooldownTime, dashLength, dashSpeed;
    public bool dash;
    float dashCooldown, dashTime;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpsRemaining = totalJumps;
        dashCooldown = 0f;
        dashTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Perfrom Raycast for groundcheck
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out _, groundCheckDistance + 0.01f);

        //Resets Jump
        if (isGrounded && !isJumping)
        {
            jumpsRemaining = totalJumps;
            velocity.y = 0f;
        }

        //Movement Code
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();


        Vector3 desiredMovementDirection = camForward * moveInput.y + camRight * moveInput.x;
        // DASH OVERRIDE
        if (dash)
            desiredMovementDirection = camForward + camRight;
        
        desiredMovementDirection.Normalize();

        if (desiredMovementDirection.sqrMagnitude > 0.01f)
        {
            transform.Translate(desiredMovementDirection * Time.deltaTime * moveSpeed, Space.World);

            //Rotate the player in the new direction
            Quaternion targetRotation = Quaternion.LookRotation(desiredMovementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
        }

        //Jumping
        if (isJumping)
        {
            jumpTime += Time.deltaTime;
            float normalizedTime = jumpTime / jumpDuration;
            if(normalizedTime > 1f)
            {
                isJumping = false;
            }
            else
            {
                //Evaluate our Animation curve for this point in the jump
                float curveValue = jumpCurve.Evaluate(normalizedTime);
                //Set our velocity.y for this frame using the curve
                velocity.y = (jumpHeight/jumpDuration) * curveValue;
            }
        }
        else if (!isGrounded)
        {
            //Apply Gravity
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        //Vertical Movement
        transform.position += new Vector3(0, velocity.y * Time.deltaTime, 0);
    }

    //public void OnMove(InputValue value)
    //{
    //    moveInput = value.Get<Vector2>();
    //    Debug.Log($"Message event : {moveInput}");
    //}

    public void MovePlayer(InputAction.CallbackContext ctx) 
    {
        moveInput = ctx.ReadValue<Vector2>();
        Debug.Log($"Input event : {moveInput}");
    }

    public void PlayerJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && jumpsRemaining > 0)
        {
            isJumping = true;
            //velocity.y = 5;
            jumpTime = 0;
            jumpsRemaining--;
            Debug.Log("Jumped");
        }
    }
}
