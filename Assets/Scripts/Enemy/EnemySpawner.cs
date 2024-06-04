using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float TimeBetweenSpawns { get => timeBetweenSpawns; set => timeBetweenSpawns = value; }
    public Transform Spawner1 { get => spawner1; set => spawner1 = value; }
    public Transform Spawner2 { get => spawner2; set => spawner2 = value; }
    public int SpawnAmount { get => spawnAmount; set => spawnAmount = value; }

    [SerializeField] private Transform spawner1;
    [SerializeField] private Transform spawner2;
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private bool canSpawn;

    private Transform currentSpawner;
    private float timeBetweenSpawns;
    private int spawnAmount = 0;

    #region Spawn Enemy
    public Enemy SpawnEnemyFromSpawnerSequential()
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

        GameObject enemyGameObject = ObjectPooler.Instance.GetObjectFromPool("enemy");
        enemyGameObject.transform.SetPositionAndRotation(currentSpawner.position, currentSpawner.rotation);
        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        return enemy;
    }

    public Enemy SpawnEnemyFromSpawner(Transform spawner)
    {
        if (!canSpawn)
        {
            return null;
        }

        GameObject enemyGameObject = ObjectPooler.Instance.GetObjectFromPool("enemy");
        enemyGameObject.transform.SetPositionAndRotation(spawner.position, spawner.rotation);
        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
        return enemy;
    }

    #endregion
    

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
}
