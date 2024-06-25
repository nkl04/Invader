using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public bool expandable;
    }
    [SerializeField] private List<Pool> poolList;
    [SerializeField] private Dictionary<string, List<GameObject>> poolDictionary;


    private new void Awake()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in poolList)
        {
            List<GameObject> objectPoolQueue = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                objectPoolQueue.Add(obj);
            }
            poolDictionary.Add(pool.tag, objectPoolQueue);
        }
        GameController.Instance.OnGameStateUpdated += GameController_OnGameStateUpdated;
    }

    private void GameController_OnGameStateUpdated(object sender, EventArgs e)
    {
        if (GameController.Instance.GameState == GameState.MainMenu || GameController.Instance.GameState == GameState.SelectModel)
        {
            DisableAllPooledObject();
        }
    }

    public GameObject GetObjectFromPool(string tag)
    {
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            if (!poolDictionary[tag][i].activeInHierarchy)
            {
                return poolDictionary[tag][i];
            }
        }

        foreach (Pool item in poolList)
        {
            if (item.tag == tag && item.expandable)
            {
                GameObject obj = Instantiate(item.prefab, this.transform);
                obj.SetActive(false);
                item.size++;
                poolDictionary[tag].Add(obj);
                return obj;
            }
        }
        return null;
    }

    public void DisableAllPooledObject(string tag)
    {
        foreach (GameObject obj in poolDictionary[tag])
        {
            obj.SetActive(false);
        }
    }

    public void DisableAllPooledObject()
    {
        foreach (KeyValuePair<string, List<GameObject>> item in poolDictionary)
        {
            foreach (GameObject obj in item.Value)
            {
                obj.SetActive(false);
            }
        }
    }

    public void ClearChildObjectIn(Transform transform)
    {
        foreach (Transform obj in transform)
        {
            Destroy(obj.gameObject);
        }
    }
}
