using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlyMode
{
    FollowALine,
    ToShapeConfig
}

public class Enemy : MonoBehaviour
{
    public FlyMode FlyMode { get => flyMode; set => flyMode = value; }
    [SerializeField] private FlyMode flyMode;

    // Start is called before the first frame update
    void Start()
    {
        FollowPath followPath = GetComponent<FollowPath>();
        FindTargetPosition findTargetPosition = GetComponent<FindTargetPosition>();
        
        if (flyMode == FlyMode.FollowALine)
        {
            followPath.enabled = true;
            findTargetPosition.enabled = false;
        }
        else if (flyMode == FlyMode.ToShapeConfig)
        {
            findTargetPosition.enabled = true;
            followPath.enabled = false;
        }
    }

}
