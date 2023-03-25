using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private InputController inputController;

    private void OnEnable()
    {
        inputController = ServiceLocator.instance.GetService<InputController>();
    }

    public void Pause()
    {
        inputController.inputBlocked = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        inputController.inputBlocked = false;
        Time.timeScale = 1f;
    }
}
