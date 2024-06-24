using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelUI : UIPage
{
    public class OnSelectLevelEventArgs : EventArgs
    {
        public LevelSO levelSO;
    }

    public event EventHandler<OnSelectLevelEventArgs> OnLevelSelected;
    [SerializeField] private SelectModelUI selectModelUI;
    [SerializeField] private LevelContainerSO levelContainerSO;
    [SerializeField] private Button levelButtonPrefab;
    [SerializeField] private Transform levelsParent;

    private void Awake()
    {
        for (int i = 0; i < levelContainerSO.levels.Count; i++)
        {
            LevelSO levelSO = levelContainerSO.GetLevel(i);
            Button levelButton = Instantiate(levelButtonPrefab, levelsParent);
            levelButton.GetComponent<LevelButton>().SetLevelSO(levelSO);
            levelButton.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
            levelButton.onClick.AddListener(() =>
            {
                OnLevelSelected?.Invoke(this, new OnSelectLevelEventArgs()
                {
                    levelSO = levelSO
                });
                UIController.Instance.PushAndShow(selectModelUI);
            });
        }
    }
}
