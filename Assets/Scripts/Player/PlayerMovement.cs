using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canMove;

    private Vector2 directionVector;
    private Vector3 deltaPosition;


    private void Update() {
        if (canMove)
        {
            Movement();
        }
    }

    //Player movement ===================================================================================   
    private void Movement()
    {   
        directionVector = GameInput.Instance.GetInputDirectionVector(); 

        
        if(transform.position.x == MainCamera.Instance.MinMoveableBounds.x && directionVector.x < 0
        ||  transform.position.x == MainCamera.Instance.MaxMoveableBounds.x && directionVector.x > 0)
        {
            // Can not move only on the X
            directionVector = new Vector3(0f,directionVector.y).normalized;
        }

        if(transform.position.y == MainCamera.Instance.MinMoveableBounds.y && directionVector.y < 0
        ||  transform.position.y == MainCamera.Instance.MaxMoveableBounds.y && directionVector.y > 0)
        {
            // Can not move only on the Y
            directionVector = new Vector3(directionVector.x,0f).normalized;
        }

        deltaPosition = directionVector * moveSpeed * Time.deltaTime;

        Vector2 newPos = new Vector2();


        //Limit the movement of player in the area of boundary
        newPos.x = Mathf.Clamp(transform.position.x + deltaPosition.x, MainCamera.Instance.MinMoveableBounds.x, MainCamera.Instance.MaxMoveableBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + deltaPosition.y, MainCamera.Instance.MinMoveableBounds.y, MainCamera.Instance.MaxMoveableBounds.y);
        
        transform.position = newPos; 
    }
}
