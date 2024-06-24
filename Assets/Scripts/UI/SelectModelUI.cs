using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class SelectModelUI : UIPage
{
    public event EventHandler<OnSelectModelEventArgs> OnModelSelected;
    public class OnSelectModelEventArgs : EventArgs
    {
        public PlayerSO playerSO;
    }

    private Vector3 MODEL_ROTATION = new Vector3(27f, 120f, -20f);
    private Vector3 MODEL_SCALE = new Vector3(2f, 2f, 2f);

    [SerializeField] private PlayerContainerSO playerContainer;
    [SerializeField] private PlayingUI playingUI;
    [SerializeField] private Button prevBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button playBtn;
    [SerializeField] private Transform models;

    private int selectedModelIndex;
    private PlayerSO selectedModel;
    private void Awake()
    {
        selectedModel = PlayerManager.Instance?.PlayerSO;
        selectedModelIndex = playerContainer.playerList.IndexOf(selectedModel);

        foreach (Transform item in models)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < playerContainer.playerList.Count; i++)
        {
            GameObject model = Instantiate(playerContainer.playerList[i].playerPrefab, models);
            model.transform.localPosition = Vector3.zero;
            model.transform.localEulerAngles = MODEL_ROTATION;
            model.transform.localScale = MODEL_SCALE;
            model.SetActive(false);
        }
    }

    private void Start()
    {
        UpDateSelectedModel(selectedModelIndex);

        prevBtn.onClick.AddListener(PrevOption);
        nextBtn.onClick.AddListener(NextOption);
        playBtn.onClick.AddListener(() =>
        {
            OnModelSelected?.Invoke(this, new OnSelectModelEventArgs
            {
                playerSO = selectedModel
            });
            PlayerManager.Instance.PlayerSO = selectedModel;
            UIController.Instance.ClearStackAndSetInitialPage(playingUI);
        });
    }

    private void UpDateSelectedModel(int selectedIndex)
    {
        for (int i = 0; i < models.childCount; i++)
        {
            models.GetChild(i).gameObject.SetActive(false);
        }
        selectedModel = playerContainer.playerList[selectedIndex];
        models.GetChild(selectedIndex).gameObject.SetActive(true);
    }

    private void NextOption()
    {
        selectedModelIndex++;
        if (selectedModelIndex >= playerContainer.playerList.Count)
        {
            selectedModelIndex = 0;
        }
        UpDateSelectedModel(selectedModelIndex);
    }

    private void PrevOption()
    {
        selectedModelIndex--;
        if (selectedModelIndex < 0)
        {
            selectedModelIndex = playerContainer.playerList.Count - 1;
        }
        UpDateSelectedModel(selectedModelIndex);
    }

}
