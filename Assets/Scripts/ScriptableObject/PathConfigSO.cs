using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "PathConfigSO", menuName = "SO/PathConfigSO")]
public class PathConfigSO : ScriptableObject
{   
    [SerializeField] private Transform pathPrefab;

    public Transform StartingPoint {get{return pathPrefab.GetChild(0);}}

    public List<Transform> GetPointsWay {get{
        List<Transform> pointWayList = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            pointWayList.Add(child);
        }
        return pointWayList;
    }}

}
