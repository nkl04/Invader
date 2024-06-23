using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "StageListSO", menuName = "SO/LevelSO")]
public class LevelSO : ScriptableObject
{
    public List<Stage> stageList;

    //get the index + 1 stage
    public Stage GetStage(int index)
    {
        if (index < stageList.Count)
        {
            return stageList[index];
        }
        else
        {
            Debug.LogError("It out of stage range!");
            return null;
        }
    }

    //get the index of the stage
    public int GetStageIndex(Stage stage)
    {
        return stageList.IndexOf(stage);
    }
}
