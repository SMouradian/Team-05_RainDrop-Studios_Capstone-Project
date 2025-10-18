using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder;

public class headbob : MonoBehaviour
{
    [Range(0.001f, 0.02f)]
    public float amount = 0.002f;
    [Range(1f, 30f)]

    public float frequency = 10.0f;
    [Range(10f, 100f)]
    public float Smooth = 10.0f;
    
    private playerMovement playerMovementScript;
    private float bobTimer = 0f;
    private float previousInputMagnitude = 0f;
    
    void Start()
    {
        // Get reference to player movement script to check if grounded
        playerMovementScript = GetComponentInParent<playerMovement>();
    }

    // LateUpdate is called after all Update calls
    void LateUpdate()
    {
        checkForHeadBob();
    }

    private void checkForHeadBob()
    {
        // Only apply headbob if player is grounded
        bool isGrounded = playerMovementScript != null ? playerMovementScript.isGrounded : true;
        
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        Vector3 targetPosition;
        
        if (inputMagnitude > 0.1f && isGrounded)
        {
            // Increment timer based on movement - continues smoothly
            bobTimer += Time.deltaTime * frequency * inputMagnitude;
            
            float sine = Mathf.Sin(bobTimer);
            float cosine = Mathf.Cos(bobTimer / 2);
            targetPosition = new Vector3(cosine * amount * inputMagnitude, sine * amount * inputMagnitude, 0);
        }
        else
        {
            // Don't reset timer - let it continue so direction changes are smooth
            // Only reset to center position
            targetPosition = Vector3.zero;
        }
        
        // Use faster lerp when stopping, slower when moving for smoother feel
        float lerpSpeed = inputMagnitude > 0.1f ? Smooth : Smooth * 2f;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * lerpSpeed);
        
        previousInputMagnitude = inputMagnitude;
    }


}

