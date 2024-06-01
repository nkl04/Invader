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
        if (flyMode == FlyMode.FollowALine)
        {
            FollowPath followPath = GetComponent<FollowPath>();
            followPath.enabled = true;
        }
        // else if (flyMode == FlyMode.ToShapeConfig)
        // {
        //     FollowShape followShape = GetComponent<FollowShape>();
        //     followShape.enabled = true;
        // }
    }

}
