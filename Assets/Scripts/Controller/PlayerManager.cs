using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerSO playerSO;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;
    private List<Transform> shootingPointList;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();

        playerHealth.MaxHealth = playerSO.maxHealth;
        playerMovement.MoveSpeed = playerSO.moveSpeed;
        playerShooter.PooledBulletTag = playerSO.defaultWeapon;

        //Instantiate an player game object model to the child of the player
        GameObject playerModel = Instantiate(playerSO.playerPrefab, transform);
        playerModel.transform.localPosition = new Vector3(0, 0, 0);
        playerModel.transform.localRotation = Quaternion.Euler(-80, 0, 0);

        //Get all the shooting points from the player model
        shootingPointList = GetAllChildTransforms(playerModel.transform);
        playerShooter.SetShootingPointList(shootingPointList);
    }


    public List<Transform> GetAllChildTransforms(Transform parent)
    {
        List<Transform> childTransforms = new List<Transform>();

        foreach (Transform child in parent)
        {
            childTransforms.Add(child);
        }

        return childTransforms;
    }


}
