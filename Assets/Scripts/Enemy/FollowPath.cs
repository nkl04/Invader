using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private List<Transform> pointsWay;
    private Transform targetPoint;

    private void Update()
    {
        if (pointsWay != null && targetPoint != null)
        {
            MoveFollowPath();
        }
    }
    
    private void MoveFollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
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

    public void SetPathConfig(PathConfigSO pathConfigSO)
    {
        pointsWay = pathConfigSO.GetPointsWay;
        targetPoint = pointsWay[0];
    }
}
