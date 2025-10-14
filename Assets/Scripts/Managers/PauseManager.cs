using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

public class PauseManager : AbstractSingleton<PauseManager>
{
    private List<IPausable> pausableElements = new List<IPausable>();

    public InputAction pauseKey;

    private bool _isGamePaused = false;

    private void Start()
    {
        pauseKey.performed += HandlePausePressed;
    }

    private void HandlePausePressed(InputAction.CallbackContext ctx)
    {
        if (_isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (_isGamePaused) return;

        _isGamePaused = true;

        foreach (IPausable element in pausableElements)
        {
            element?.OnPause();
        }
    }

    private void ResumeGame()
    {
        if (!_isGamePaused) return;

        _isGamePaused = false;

        foreach (IPausable element in pausableElements)
        {
            element?.OnResume();
        }
    }
}
