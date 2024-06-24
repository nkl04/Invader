using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public GameObject playerPrefab;
    public string defaultWeapon;
    public float moveSpeed;
    public float maxHealth;

}
