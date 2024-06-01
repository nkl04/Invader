using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "ShapeConfigSO", menuName = "SO/ShapeConfigSO")]
public class ShapeConfigSO : ScriptableObject
{
    [SerializeField] private Transform shapePrefab;

    //get all point list
    public List<Transform> GetPointList()
    {
        List<Transform> pointList = new List<Transform>();
        foreach (Transform child in shapePrefab)
        {
            pointList.Add(child);
        }
        return pointList;
    }

    //get shape prefab
    public Transform GetShapePrefab()
    {
        return shapePrefab;
    }
}