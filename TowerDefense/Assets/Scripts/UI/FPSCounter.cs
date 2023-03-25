using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private int fpsCount;
    private Text fpsText;

    private void Awake()
    {
        fpsText = GetComponent<Text>();
        StartCoroutine(textUpdate());
    }

    private IEnumerator textUpdate()
    {
        fpsCount = Mathf.FloorToInt(1 / Time.unscaledDeltaTime);
        fpsText.text = $"FPS: {fpsCount.ToString()}";
        yield return new WaitForSeconds(1f);
        StartCoroutine(textUpdate());
    }
}
