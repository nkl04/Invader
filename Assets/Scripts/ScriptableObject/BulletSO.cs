using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "SO/BulletSO", order = 1)]
public class BulletSO : ScriptableObject
{
    [SerializeField] private BulletTag tag;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float existTime = 3f;

    public BulletTag Tag => tag;
    public float Damage => damage;
    public float ExistTime => existTime;
}

public enum BulletTag
{
    Player,
    Enemy
}
