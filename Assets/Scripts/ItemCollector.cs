using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private const string ITEM_TAG = "Item";

    [SerializeField] private TextMeshProUGUI coinCounter;

    private int coin;

    private void Start()
    {
        coin = 0;
        coinCounter.text = coin.ToString();
    }


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

            item.gameObject.SetActive(false);
            ObjectPooler.Instance.ClearChildObjectIn(item.transform);
        }
    }

    private void IncreaseCoin()
    {
        coin++;
        coinCounter.text = coin.ToString();
    }


}
