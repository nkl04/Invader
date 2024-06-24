using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private const string ENEMY_TAG = "enemy";

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private LevelSO levelSO;
    [SerializeField] private SelectLevelUI selectLevelUI;

    [SerializeField] private float timeBetweenStages;
    [SerializeField] private float timeBetweenSpawns;

    private float normalTime = 0.5f;
    private float fastTime = 0.1f;
    private float timeCounter;

    private bool isEndOfStage = false;
    private FlyMode currentFlyMode;
    private Stage currentStage;
    private List<Enemy> enemieList;

    private bool allowSpawn;

    private new void Awake()
    {
        selectLevelUI.OnLevelSelected += SelectLevelUI_OnLevelSelected;

    }

    private void SelectLevelUI_OnLevelSelected(object sender, SelectLevelUI.OnSelectLevelEventArgs e)
    {
        SetLevelSO(e.levelSO);
        currentStage = levelSO.GetStage(0);
        currentFlyMode = currentStage.flyMode;
        ResetSpawnAmount();
        enemieList = new List<Enemy>();
        GameController.Instance.OnGameStateUpdated += GameController_OnGameStateUpdated;
    }
    private void GameController_OnGameStateUpdated(object sender, System.EventArgs e)
    {
        allowSpawn = GameController.Instance.GameState == GameState.InGame;
    }

    private void Update()
    {
        if (allowSpawn)
        {
            //if the enemy spawner can spawn enemies
            if (enemySpawner.CanSpawn)
            {
                switch (currentFlyMode)
                {
                    case FlyMode.FollowALine:
                        OnFollowPathStage();
                        break;
                    case FlyMode.ToShapeConfig:
                        OnToShapeConfigStage();
                        break;
                }
                #region When the stage is in the end
                //if the enemy spawner can't spawn enemies and the stage is end
                if (isEndOfStage)
                {

                    if (IsAllInOnTargetPosition())
                    {
                        SetMoveAroundAction(true);
                    }

                    if (IsClearEnemyList())
                    {
                        timeCounter = timeBetweenStages;
                        enemySpawner.CanSpawn = false;
                    }
                }
                #endregion
            }

            #region Time counter
            if (timeCounter <= 0)
            {
                enemySpawner.CanSpawn = true;

                #region When the stage is in the end
                if (IsClearEnemyList() && isEndOfStage)
                {
                    isEndOfStage = false;
                    //change to the next stage
                    int index = levelSO.GetStageIndex(currentStage);
                    if (index < levelSO.stageList.Count - 1)
                    {
                        currentStage = levelSO.GetStage(index + 1);
                        currentFlyMode = currentStage.flyMode;
                        ResetSpawnAmount();
                    }
                    else
                    {
                        Debug.Log("All stages are completed");
                    }
                }
                #endregion
            }
            else
            {
                timeCounter -= Time.deltaTime;
            }
            #endregion
        }
    }

    #region StageType
    private void OnFollowPathStage()
    {

        timeBetweenSpawns = normalTime;
        if (enemySpawner.SpawnAmount < currentStage.enemyAmount)
        {
            //spawn 2 enemies from 2 spawner points and set their path
            Enemy enemy1 = SpawnEnemyAndSetPath(enemySpawner.Spawner1, currentStage.pathConfigSOList[0]);
            Enemy enemy2 = SpawnEnemyAndSetPath(enemySpawner.Spawner2, currentStage.pathConfigSOList[1]);

            if (enemy1 != null)
            {
                enemySpawner.SpawnAmount += 1;
            }
            if (enemy2 != null)
            {
                enemySpawner.SpawnAmount += 1;
            }
            if (enemy1 == null || enemy2 == null)
            {
                Debug.LogError("Enemy not spawned");
            }

            timeCounter = timeBetweenSpawns;
            enemySpawner.CanSpawn = false;
        }
        else
        {
            //the spawned enemy amount is equal to the enemy amount in the stage
            isEndOfStage = true;
        }
    }

    private void OnToShapeConfigStage()
    {
        timeBetweenSpawns = fastTime;
        if (enemySpawner.SpawnAmount < currentStage.shapeConfigSO.GetPointList().Count)
        {
            int index = enemySpawner.SpawnAmount;
            Enemy enemy = enemySpawner.SpawnEnemyFromSpawnerSequential();
            enemy.FlyMode = currentFlyMode;
            enemySpawner.SpawnAmount += 1;
            if (enemy != null)
            {
                enemy.GetComponent<FindTargetPosition>().TargetPosition = currentStage.shapeConfigSO.GetPointList()[index].position;
                enemy.gameObject.SetActive(true);
                enemieList.Add(enemy);
            }
            else
            {
                Debug.LogError("Enemy not spawned");
                Debug.Log(enemySpawner.SpawnAmount);
            }

            timeCounter = timeBetweenSpawns;
        }
        else
        {
            isEndOfStage = true;
        }
    }

    #endregion

    //check the enemy list and remove the dead enemies
    public bool IsClearEnemyList()
    {
        for (int i = 0; i < enemieList.Count; i++)
        {
            if (enemieList[i] == null || !enemieList[i].gameObject.activeSelf)
            {
                enemieList.RemoveAt(i);
            }
        }
        return enemieList.Count == 0;
    }

    public bool IsAllInOnTargetPosition()
    {
        for (int i = 0; i < enemieList.Count; i++)
        {
            if (!enemieList[i].GetComponent<FindTargetPosition>().IsReachTargetPosition)
            {
                return false;
            }
        }
        return true;
    }

    public void SetMoveAroundAction(bool isMoveAround)
    {
        for (int i = 0; i < enemieList.Count; i++)
        {
            enemieList[i].GetComponent<MoveAround>().enabled = isMoveAround;
        }
    }
    //spawn an enemy from a spawner point and set its path   
    private Enemy SpawnEnemyAndSetPath(Transform spawner, PathConfigSO pathConfigSO)
    {
        Enemy enemy = enemySpawner.SpawnEnemyFromSpawner(spawner);
        enemy.FlyMode = currentFlyMode;
        enemy.GetComponent<FollowPath>().SetPathConfig(pathConfigSO);
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    public void ResetSpawnAmount()
    {
        enemySpawner.SpawnAmount = 0;
    }

    public void SetLevelSO(LevelSO levelSO)
    {
        this.levelSO = levelSO;
    }

}
