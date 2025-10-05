using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder;

public class headbob : MonoBehaviour
{
    [Range(0.001f, 0.01f)]
    public float amount = 0.002f;
    [Range(1f, 30f)]

    public float frequency = 10.0f;
    [Range(10f, 100f)]
    public float Smooth = 10.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkForHeadBob();
    }

    private void checkForHeadBob()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        Vector3 targetPosition;
        if (inputMagnitude > 0)
        {
            float sine = Mathf.Sin(Time.time * frequency);
            float cosine = Mathf.Cos(Time.time * frequency / 2);
            targetPosition = new Vector3(cosine * amount * inputMagnitude, sine * amount * inputMagnitude, 0);
        }
        else
        {
            targetPosition = Vector3.zero;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * Smooth);
    }


}

