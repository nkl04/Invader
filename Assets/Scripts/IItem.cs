using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Coin,
    Gem,
    Key
}

[SerializeField]
public abstract class IItem : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public GameObject itemPrefab;
    public float existTime;

}
