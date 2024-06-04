using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : Singleton<GameInput>
{
    private PlayerInputAction playerInputAction;

    private new void Awake() {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
    }

    //Get input vector from player ===========================================================================
    public Vector2 GetInputDirectionVector()
    {
        Vector2 directionVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        return directionVector.normalized;
    }
}
