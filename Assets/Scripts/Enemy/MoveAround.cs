using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    [SerializeField] private float speed = .5f;

    private Vector3 tlPos;
    private Vector3 dlPos;
    private Vector3 trPos;
    private Vector3 drPos;

    private List<Vector3> targetPositions;
    private Vector3 currentTarget;
    private Vector3 startPoint;

    private void OnEnable()
    {
        startPoint = gameObject.GetComponent<FindTargetPosition>().TargetPosition;
        SetTargetPosition(startPoint);
        targetPositions = new List<Vector3>() { tlPos, dlPos, drPos, trPos };
        currentTarget = targetPositions[0];
    }

    private void Update()
    {
        if (targetPositions != null && currentTarget != null)
        {
            MoveAroundPositionList();
        }
    }

    private void MoveAroundPositionList()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            int nextIndex = targetPositions.IndexOf(currentTarget) + 1;
            if (nextIndex < targetPositions.Count)
            {
                currentTarget = targetPositions[nextIndex];
            }
            else
            {
                nextIndex = 0;
                targetPositions[0] = startPoint + new Vector3(-2, 0, 0);
                targetPositions[1] = targetPositions[0] + new Vector3(0, -1, 0);
                currentTarget = targetPositions[nextIndex];
            }
        }
    }

    private void SetTargetPosition(Vector3 startPoint)
    {
        tlPos = startPoint;
        dlPos = tlPos + new Vector3(0, -1, 0);
        drPos = tlPos + new Vector3(2, -1, 0);
        trPos = tlPos + new Vector3(2, 0, 0);
    }


}
