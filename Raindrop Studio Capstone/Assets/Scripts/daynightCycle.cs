using UnityEngine;

public class daynightCycle : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float changeColor;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
