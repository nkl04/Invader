using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelContainerSO", menuName = "SO/LevelContainerSO", order = 1)]
public class LevelContainerSO : ScriptableObject
{
    public List<LevelSO> levels;

    public LevelSO GetLevel(int index)
    {
        if (index < levels.Count)
        {
            return levels[index];
        }
        else
        {
            Debug.LogError("It out of level range!");
            return null;
        }
    }
}
