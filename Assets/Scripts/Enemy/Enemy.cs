using System;
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
    [Serializable]
    public class DropRate
    {
        public IItem item;
        public int rate;
        public int minDrop;
        public int maxDrop;
    }
    public FlyMode FlyMode { get => flyMode; set => flyMode = value; }
    public List<DropRate> DropRateList { get => dropRateList; set => dropRateList = value; }
    [SerializeField] private FlyMode flyMode;
    [SerializeField] private List<DropRate> dropRateList;

    private Rigidbody rb;
    // Start is called before the first frame update
    void OnEnable()
    {
        FollowPath followPath = GetComponent<FollowPath>();
        FindTargetPosition findTargetPosition = GetComponent<FindTargetPosition>();
        MoveAround moveAround = GetComponent<MoveAround>();

        if (followPath == null)
        {
            followPath = gameObject.AddComponent<FollowPath>();
        }
        if (findTargetPosition == null)
        {
            findTargetPosition = gameObject.AddComponent<FindTargetPosition>();
        }
        if (moveAround == null)
        {
            moveAround = gameObject.AddComponent<MoveAround>();
        }

        findTargetPosition.enabled = false;
        moveAround.enabled = false;
        followPath.enabled = false;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

}
