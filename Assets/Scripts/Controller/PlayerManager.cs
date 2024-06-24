using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public event EventHandler OnPlayerModelStatusChanged;
    public PlayerSO PlayerSO { get => playerSO; set => playerSO = value; }


    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private SelectModelUI selectModelUI;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;
    private List<Transform> shootingPointList;

    private Vector3 initPostion;

    private new void Awake()
    {
        initPostion = transform.position;
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();

        InstatiateModel(playerSO);
        GameController.Instance.OnGameStateUpdated += GameController_OnGameStateUpdated;
        selectModelUI.OnModelSelected += SelectModelUI_OnModelSelected;

    }

    private void GameController_OnGameStateUpdated(object sender, EventArgs e)
    {
        gameObject.SetActive(GameController.Instance.GameState != GameState.SelectModel);
        if (gameObject.activeInHierarchy)
        {
            OnPlayerModelStatusChanged?.Invoke(this, EventArgs.Empty);
        }

        if (GameController.Instance.GameState == GameState.MainMenu)
        {
            transform.position = initPostion;
        }
    }

    private void SelectModelUI_OnModelSelected(object sender, SelectModelUI.OnSelectModelEventArgs e)
    {
        playerSO = e.playerSO;
        InstatiateModel(playerSO);
        gameObject.SetActive(true);
        OnPlayerModelStatusChanged?.Invoke(this, EventArgs.Empty);
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

    private void InstatiateModel(PlayerSO playerSO)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        playerHealth.MaxHealth = playerSO.maxHealth;
        playerMovement.MoveSpeed = playerSO.moveSpeed;
        playerShooter.PooledBulletTag = playerSO.defaultWeapon;

        playerHealth.ResetHealth();

        //Instantiate an player game object model to the child of the player
        GameObject playerModel = Instantiate(playerSO.playerPrefab, transform);
        playerModel.transform.localPosition = new Vector3(0, 0, 0);
        playerModel.transform.localRotation = Quaternion.Euler(-80, 0, 0);

        //Get all the shooting points from the player model
        shootingPointList = GetAllChildTransforms(playerModel.transform);
        playerShooter.SetShootingPointList(shootingPointList);

    }



}
