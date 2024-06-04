using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigSO", menuName = "SO/ConfigSO", order = 1)]
public abstract class ConfigSO : ScriptableObject
{
    Transform pathPrefab;
}
