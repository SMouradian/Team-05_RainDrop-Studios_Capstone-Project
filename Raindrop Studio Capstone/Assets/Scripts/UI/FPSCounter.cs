using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI Fpstext;

    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    void Start()
    {
        if (Fpstext != null)
        {
            Fpstext.text = "0 FPS";
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime)
        {
            int framerate = Mathf.RoundToInt(frameCount / time);
            Fpstext.text = framerate.ToString() + " FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
