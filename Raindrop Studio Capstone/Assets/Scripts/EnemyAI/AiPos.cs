using UnityEngine;

public class AiPos : MonoBehaviour
{
    public Transform aiPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (aiPos != null)
        {
            transform.position = aiPos.position;
        }
    }
}
