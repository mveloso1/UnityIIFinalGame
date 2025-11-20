using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

//Adds character controller to gameobject
[RequireComponent (typeof(CharacterController))]
public class FPMovement : MonoBehaviour
{
    public float speed = 10;
    float h, v;
    public float gravity = -9.8f;
    public float jumpStrength = 10.0f;
    float velocity;
    float gravityMultiplier = 3.0f;

    public AudioClip jumpSound;
    private AudioSource audioSource;

    CharacterController controller;

    [Header("Dash")]
    //public bool dash;
    public float dashTime;
    public float dashLength = 0.1f;
    public float dashSpeed = 50f;
    public float dashCooldownLength = 1f;
    public float dashCooldown = 0f;

    public Vector3 dashDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController> ();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = h * speed;
        float moveZ = v * speed;
        Vector3 movement = new Vector3(moveX, 0, moveZ);
        

        movement = Vector3.ClampMagnitude(movement, speed);

        if(IsGrounded() && velocity <0)
        {
            velocity = -1;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        movement.y = velocity; 

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        // OVERRIDE: Dash
        // If dashing, override directional movement
        if (dashTime > 0)
        {
            movement = dashDirection * dashSpeed;
            movement *= Time.deltaTime;
            dashTime -= Time.deltaTime;
        }
        // If not dashing, decrement cooldown
        else if (dashCooldown > 0)
            dashCooldown -= Time.deltaTime;
        {
            
        }

        controller.Move(movement);
    }

    public void MoveInput(InputAction.CallbackContext ctx)
    {
        h = ctx.ReadValue<Vector2>().x;
        v = ctx.ReadValue<Vector2>().y;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!IsGrounded())
        {
            return;
        }

        if (ctx.performed)
        {
            velocity *= jumpStrength;
            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
       
    }
    public void Dash(InputAction.CallbackContext ctx)
    {
        // flip dash
        if (ctx.performed)
        {
            if (dashCooldown <= 0)
            {
                dashTime = dashLength;
                dashCooldown = dashCooldownLength;
                // Set dash direction to where player was moving
                if(v != 0 || h != 0)
                    dashDirection = transform.TransformDirection(new Vector3(h, 0, v).normalized);
                // If no direction being held, default to forwards
                else
                    dashDirection = transform.TransformDirection(new Vector3(0, 0, 1));
                
            }
        }
    }
    bool IsGrounded()
    {
        return controller.isGrounded;
    }
}
