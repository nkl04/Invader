using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private StageListSO stageListSO;

    [SerializeField] private float timeBetweenStages;
    [SerializeField] private float timeBetweenSpawns;

    private FlyMode currentFlyMode;
    private Stage currentStage;
    private float timer;
    private new void Awake() {
        currentStage = stageListSO.GetStage(0);
        currentFlyMode = currentStage.flyMode;
        ResetSpawnAmount();
    }

    private void Update() {
        //if the enemy spawner can spawn enemies
        if (enemySpawner.CanSpawn)
        {   
            // Follow a line mode
            if (currentFlyMode == FlyMode.FollowALine)
            {
                // if there are still enemies to spawn
                if (enemySpawner.SpawnAmount < currentStage.enemyAmount)
                {
                    //spawn 2 enemies from 2 spawner points and set their path
                    Enemy enemy1 = SpawnEnemyAndSetPath(enemySpawner.Spawner1, currentStage.pathConfigSOList[0]);
                    Enemy enemy2 = SpawnEnemyAndSetPath(enemySpawner.Spawner2, currentStage.pathConfigSOList[1]);

                    if (enemy1 != null )
                    {
                        enemySpawner.SpawnAmount += 1;
                    }
                    if (enemy2 != null )
                    {
                        enemySpawner.SpawnAmount += 1;
                    }
                    if (enemy1 == null || enemy2 == null)
                    {
                        Debug.LogError("Enemy not spawned");
                    }
                    
                    timer = timeBetweenSpawns;
                    enemySpawner.CanSpawn = false;
                }
                else
                {
                    //the spawned enemy amount is equal to the enemy amount in the stage

                    //TODO: change the stage to the next stage 
                    ResetSpawnAmount();
                    timer = timeBetweenStages;
                    //change the stage to the next stage
                    if (stageListSO.GetStage(stageListSO.GetStageIndex(currentStage) + 1) != null)
                    {
                        currentStage = stageListSO.GetStage(stageListSO.GetStageIndex(currentStage) + 1);
                        currentFlyMode = currentStage.flyMode;
                    }
                    else
                    {
                        Debug.Log("No more stages");
                    }
                    enemySpawner.CanSpawn = false;
                }
            }
            // To shape config mode
            else if (currentFlyMode == FlyMode.ToShapeConfig)
            {
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
                    }
                    else
                    {
                        Debug.LogError("Enemy not spawned");
                    }
                }
                else
                {
                    enemySpawner.CanSpawn = false; 
                }
            }
        
        }
        
        #region Time counter
        if (timer <= 0)
        {
            enemySpawner.CanSpawn = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        #endregion

    }

    //spawn an enemy from a spawner point and set its path   
    private Enemy SpawnEnemyAndSetPath(Transform spawner, PathConfigSO pathConfigSO)
    {
        Enemy enemy = enemySpawner.SpawnEnemyFromSpawner(spawner);
        enemy.FlyMode = currentFlyMode;
        enemy.GetComponent<FollowPath>().PathConfigSO = pathConfigSO;
        enemy.gameObject.SetActive(true);
        return enemy;
    }
    
    public void ResetSpawnAmount()
    {
        enemySpawner.SpawnAmount = 0;
    }
}
