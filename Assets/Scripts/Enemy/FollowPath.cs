using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public PathConfigSO PathConfigSO { get => pathConfigSO; set => pathConfigSO = value; }
    [SerializeField] private float speed = 5f;
    private PathConfigSO pathConfigSO;

    private List<Transform> pointsWay;
    private Transform targetPoint;

    private void OnEnable()
    {
        if (pathConfigSO != null)
        {
            pointsWay = pathConfigSO.GetPointsWay;
            if (pointsWay.Count > 0)
            {
                targetPoint = pointsWay[0];
            }
        }
    }

    private void Update()
    {
        if (pointsWay != null && targetPoint != null)
        {
            MoveFollowPath();
        }
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
                gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable() {
        pointsWay = null;
    }

}
