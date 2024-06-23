using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMovement : MonoBehaviour
{
    public bool CanMove { get => canMove; set => canMove = value; }

    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canMove;

    private Vector2 directionVector;
    private Vector3 deltaPosition;

    private Vector3 rootRotation;

    private void Awake()
    {
        rootRotation = transform.rotation.eulerAngles;
        GameController.Instance.OnGameStateUpdated += Instance_OnGameStateUpdated;
    }

    private void Instance_OnGameStateUpdated(object sender, EventArgs e)
    {
        canMove = GameController.Instance.GameState == GameState.InGame;

    }

    private void Update()
    {
        if (canMove)
        {
            Movement();
        }
    }

    //Player movement ===================================================================================   
    private void Movement()
    {
        directionVector = GameInput.Instance.GetInputDirectionVector();


        if (transform.position.x == MainCamera.Instance.MinMoveableBounds.x && directionVector.x < 0
        || transform.position.x == MainCamera.Instance.MaxMoveableBounds.x && directionVector.x > 0)
        {
            // Can not move only on the X
            directionVector = new Vector3(0f, directionVector.y).normalized;
        }

        if (transform.position.y == MainCamera.Instance.MinMoveableBounds.y && directionVector.y < 0
        || transform.position.y == MainCamera.Instance.MaxMoveableBounds.y && directionVector.y > 0)
        {
            // Can not move only on the Y
            directionVector = new Vector3(directionVector.x, 0f).normalized;
        }

        deltaPosition = directionVector * moveSpeed * Time.deltaTime;

        Vector2 newPos = new Vector2();


        //Limit the movement of player in the area of boundary
        newPos.x = Mathf.Clamp(transform.position.x + deltaPosition.x, MainCamera.Instance.MinMoveableBounds.x, MainCamera.Instance.MaxMoveableBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + deltaPosition.y, MainCamera.Instance.MinMoveableBounds.y, MainCamera.Instance.MaxMoveableBounds.y);

        transform.position = newPos;

        if (directionVector.x != 0)
        {
            float tiltAmount = directionVector.x > 0 ? -60f : 60f;
            transform.DORotate(rootRotation + new Vector3(0f, 0f, tiltAmount), 0.2f);
        }
        else
        {
            transform.DORotate(rootRotation, 0.5f);
        }

        if (directionVector.y > 0)
        {
            transform.DORotate(rootRotation + new Vector3(40, 0f, 0f), 0.3f);
        }
        else
        {
            transform.DORotate(rootRotation, 0.4f);
        }
    }
}
