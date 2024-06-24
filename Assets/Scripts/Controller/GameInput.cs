using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : Singleton<GameInput>
{
    public event EventHandler OnPauseAction;
    private PlayerInputAction playerInputAction;

    private new void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.UI.Enable();
        playerInputAction.UI.Pause.performed += Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    //Get input vector from player ===========================================================================
    public Vector2 GetInputDirectionVector()
    {
        Vector2 directionVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        return directionVector.normalized;
    }
}
