using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Headbob Settings")]
    public float bobSpeed = 10f;
    public float bobAmount = 0.05f;
    public float transitionSpeed = 5f;

    private Vector3 camOriginalPos;
    private float timer;

    void Awake()
    {
        camOriginalPos = transform.localPosition;
    }

    void Update()
    {
        // Check for movement input
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            // Increment timer based on bob speed
            timer += bobSpeed * Time.deltaTime;

            // Calculate new position using sine and cosine for a natural bobbing motion
            Vector3 newPosition = new Vector3(
                Mathf.Cos(timer) * bobAmount,
                camOriginalPos.y + Mathf.Abs(Mathf.Sin(timer) * bobAmount), // Abs for parabolic path
                camOriginalPos.z
            );

            // Apply the new position
            transform.localPosition = newPosition;
        }
        else
        {
            // Player is not moving, smoothly return to original position
            timer = Mathf.PI / 2; // Reinitialize timer for smooth transition
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                camOriginalPos,
                transitionSpeed * Time.deltaTime
            );
        }
        //if the player is sprinting , increase bob speed and amount
        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     bobSpeed = 15f;
        //     bobAmount = 0.1f;
        // }
        // else
        // {
        //     bobSpeed = 10f;
        //     bobAmount = 0.05f;
        // }
        
        // Reset timer to avoid bloated values
        if (timer > Mathf.PI * 2)
        {
            timer = 0;
        }
    }
}