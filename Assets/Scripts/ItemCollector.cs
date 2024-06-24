using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private const string ITEM_TAG = "Item";
    private int coin;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ITEM_TAG))
        {
            Item item = other.GetComponentInParent<Item>();
            if (item == null) return;

            if (item.IItem.itemType == ItemType.Coin)
            {
                IncreaseCoin();
            }
            Destroy(other.gameObject);
        }
    }

    private void IncreaseCoin()
    {
        coin++;
        Debug.Log("Coin: " + coin);
    }
}
