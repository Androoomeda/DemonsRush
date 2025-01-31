using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConstantSize : MonoBehaviour
{
    [SerializeField] Vector2 DefaultResolution = new Vector2(1920, 1080);
    [Range(0f, 1f)] public float WidthOrHeight = 0;

    private Camera camera;

    private float targetAspect;

    private float initialFov;
    private float horizontalFov = 120f;

    private void Start()
    {
        camera = Camera.main;

        targetAspect = DefaultResolution.x / DefaultResolution.y;

        initialFov = camera.fieldOfView;
        horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
    }

    private void Update()
    {
        float constantWidthFov = CalcVerticalFov(horizontalFov, camera.aspect);
        camera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, WidthOrHeight);
    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

        return vFovInRads * Mathf.Rad2Deg;
    }
}
