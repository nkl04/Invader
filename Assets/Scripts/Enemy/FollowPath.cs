using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private PathConfigSO pathConfigSO;

    private List<Transform> pointsWay;
    private Transform targetPoint;

    private void Start()
    {
        pointsWay = pathConfigSO.GetPointsWay;
        targetPoint = pointsWay[0];
    }

    private void Update()
    {
        MoveFollowPath();
    }
    
    private void MoveFollowPath()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            int nextIndex = pointsWay.IndexOf(targetPoint) + 1;
            if (nextIndex < pointsWay.Count)
            {
                targetPoint = pointsWay[nextIndex];
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
