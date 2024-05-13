using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamResolution : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        setupCam();
    }

    private void setupCam()
    {
        float targetWidthAspect = 20.0f * 0.01f;
        float targetHeightAspect = 9.0f * 0.01f;

        cam.aspect = targetWidthAspect / targetHeightAspect;

        float widthRatio = Screen.width / targetWidthAspect;
        float heightRatio = Screen.height / targetHeightAspect;

        float heightAdd = ((100.0f * widthRatio / heightRatio) - 100.0f) * 0.005f;
        float widthAdd = ((100.0f * heightRatio / widthRatio) - 100.0f) * 0.005f;

        if (heightRatio > widthRatio)
        {
            widthAdd = 0.0f;
        }
        else
        {
            heightAdd = 0.0f;
        }

        cam.rect = new Rect(cam.rect.x + Mathf.Abs(widthAdd), cam.rect.y + Mathf.Abs(heightAdd), cam.rect.width + (widthAdd * 2), cam.rect.height + (heightAdd * 2));
    }

    private void OnPreCull()
    {
        Rect rect = cam.rect;
        Rect newRect = new Rect(0, 0, 1, 1);
        cam.rect = newRect;
        GL.Clear(true, true, Color.black);
        cam.rect = rect;
    }
}
