using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private FlyMode flyMode;
    [SerializeField] private GameObject spawner1;
    [SerializeField] private GameObject spawner2;
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private bool canSpawn;

    private GameObject currentSpawner;

    private int spawnAmount = 0;

    public Enemy SpawnEnemy()
    {
        if (!canSpawn)
        {
            return null;
        }

        if(currentSpawner == null)
        {
            currentSpawner = Random.Range(0, 2) == 0 ? spawner1 : spawner2;
        }
        else
        {
            currentSpawner = currentSpawner == spawner1 ? spawner2 : spawner1;
        }

        Transform enemyTransform = Instantiate(enemyPrefab,currentSpawner.transform.position,Quaternion.identity);
        enemyTransform.gameObject.SetActive(false);
        enemyTransform.position = currentSpawner.transform.position;
        spawnAmount++;

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        enemy.FlyMode = flyMode;
        
        return enemy;
    }

    public void ResetSpawnAmount()
    {
        spawnAmount = 0;
    }

    //get spawn amount
    public int GetSpawnAmount()
    {
        return spawnAmount;
    }

    //set & get can spawn
    public bool CanSpawn
    {
        get => canSpawn;
        set => canSpawn = value;
    }

    //get time between spawns
    public float TimeBetweenSpawns
    {
        get => timeBetweenSpawns;
    }
}
