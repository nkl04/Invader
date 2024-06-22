using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetPosition : MonoBehaviour
{
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public float Speed { get => speed; set => speed = value; }
    private Vector3 targetPosition;
    [SerializeField] private float speed;
    private bool isReachTargetPosition = false;


    private void Update()
    {
        if (isReachTargetPosition)
        {
            return;
        }

        MoveToTargetPosition(speed);
        if (transform.position == targetPosition)
        {
            isReachTargetPosition = true;
        }
    }
    //move to the target position
    public void MoveToTargetPosition(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        targetPosition = new Vector3();
        isReachTargetPosition = false;
    }
}
