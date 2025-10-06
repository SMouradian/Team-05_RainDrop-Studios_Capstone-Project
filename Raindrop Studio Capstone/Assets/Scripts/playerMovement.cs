using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    private float speed = 12f;
    public float walkspeed;
    public float sprintspeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public KeyCode sprintKey = KeyCode.LeftShift;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    public bool isGrounded;
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (move != Vector3.zero)
        {
            move.Normalize();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(speed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        stateHandler();
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
            speed = sprintspeed;
        }
        else if (isGrounded)
        {
            state = MovementState.walking;
            speed = walkspeed;
        }
        else
        {
            state = MovementState.air;
        }
    }
}
