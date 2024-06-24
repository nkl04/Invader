using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerContainerSO", menuName = "SO/Container/PlayerContainerSO", order = 1)]
public class PlayerContainerSO : ScriptableObject
{
    public List<PlayerSO> playerList;
}
