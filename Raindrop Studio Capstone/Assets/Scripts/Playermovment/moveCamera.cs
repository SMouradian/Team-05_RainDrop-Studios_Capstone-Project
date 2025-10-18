using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // LateUpdate ensures camera moves after all other updates
    void LateUpdate()
    {
        transform.position = cameraPosition.position;
    }
}
