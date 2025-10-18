using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    // Ultrakill-style movement settings
    public float walkSpeed = 20f;
    public float sprintSpeed = 35f;
    public float airAcceleration = 100f; // Air strafing acceleration
    public float groundAcceleration = 70f; // Ground acceleration for smooth transitions
    public float maxAirSpeed = 60f; // Max speed in air
    public float gravity = -40f; // Stronger gravity for snappier feel
    public float jumpForce = 25f;

    public KeyCode sprintKey = KeyCode.LeftShift;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    public bool isGrounded;
    private Vector3 m_PlayerVelocity = Vector3.zero;
    private float currentSpeed = 0f;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Get input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = (transform.right * inputX + transform.forward * inputZ).normalized;

        // Handle jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            m_PlayerVelocity.y = jumpForce;
        }

        // Ground slam - stop vertical momentum when hitting ground
        if (isGrounded && m_PlayerVelocity.y < 0)
        {
            m_PlayerVelocity.y = -2f;
        }

        // Update movement based on ground/air state
        if (isGrounded)
        {
            HandleGroundMovement(inputDirection);
        }
        else
        {
            HandleAirMovement(inputDirection);
        }

        // Apply gravity
        m_PlayerVelocity.y += gravity * Time.deltaTime;

        // Apply movement
        controller.Move(m_PlayerVelocity * Time.deltaTime);

        stateHandler();
    }

    private void HandleGroundMovement(Vector3 inputDirection)
    {
        // Determine target speed based on sprint state
        float targetSpeed = Input.GetKey(sprintKey) ? sprintSpeed : walkSpeed;

        // Apply acceleration on ground
        if (inputDirection.magnitude > 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, groundAcceleration * Time.deltaTime);
        }
        else
        {
            // Decelerate when no input
            currentSpeed = Mathf.Lerp(currentSpeed, 0, groundAcceleration * Time.deltaTime);
        }

        // Move on ground
        Vector3 horizontalMovement = inputDirection * currentSpeed;
        m_PlayerVelocity.x = horizontalMovement.x;
        m_PlayerVelocity.z = horizontalMovement.z;
    }

    private void HandleAirMovement(Vector3 inputDirection)
    {
        // Air strafing: accelerate in the direction of input (Ultrakill-style)
        if (inputDirection.magnitude > 0)
        {
            Vector3 horizontalVelocity = new Vector3(m_PlayerVelocity.x, 0, m_PlayerVelocity.z);
            float currentHorizontalSpeed = horizontalVelocity.magnitude;

            // Apply air acceleration
            Vector3 accelerationDirection = inputDirection;
            Vector3 velocityAddition = accelerationDirection * airAcceleration * Time.deltaTime;
            horizontalVelocity += velocityAddition;

            // Clamp max air speed
            if (horizontalVelocity.magnitude > maxAirSpeed)
            {
                horizontalVelocity = horizontalVelocity.normalized * maxAirSpeed;
            }

            m_PlayerVelocity.x = horizontalVelocity.x;
            m_PlayerVelocity.z = horizontalVelocity.z;
        }
    }


    public MovementState state; public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void stateHandler()
    {
        if (isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
        }
        else if (isGrounded)
        {
            state = MovementState.walking;
        }
        else
        {
            state = MovementState.air;
        }
    }
}