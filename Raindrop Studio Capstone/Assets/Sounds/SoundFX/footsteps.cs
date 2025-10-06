using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class footsteps : MonoBehaviour
{
    public AudioSource footstepsSounds;
    public float walkPitch = 1f;
    public float runPitch = 1.5f;
    public playerMovement player;
    

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                footstepsSounds.enabled = true;
                footstepsSounds.pitch = runPitch;
                
            }
            else
            {
                footstepsSounds.enabled = true;
                footstepsSounds.pitch = walkPitch;
            }
        }
        else
        {
            footstepsSounds.enabled = false;
        }
    }
}