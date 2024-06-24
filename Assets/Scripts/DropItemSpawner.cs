using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class DropItemSpawner : Singleton<DropItemSpawner>
{
    [SerializeField] private GameObject itemPrefab;
    public void Drop(List<DropRate> itemList, Vector3 position)
    {
        IItem itemSO = itemList[0].item;

        Item dropItem = ObjectPooler.Instance.GetObjectFromPool(itemSO.itemName).GetComponent<Item>();
        dropItem.transform.position = position;

        dropItem.IItem = itemSO;
        dropItem.gameObject.SetActive(true);
    }
}
