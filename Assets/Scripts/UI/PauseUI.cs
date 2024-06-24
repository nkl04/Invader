using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : UIPage
{
    [SerializeField] private SettingUI settingUI;
    [SerializeField] private MainMenuUI MainMenuUI;

    //resume button
    [SerializeField] private Button resumeButton;

    //setting button
    [SerializeField] private Button settingButton;

    //quit button
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(() =>
        {
            UIController.Instance.PopAndReturn();
        });
        settingButton.onClick.AddListener(() =>
        {
            UIController.Instance.PushAndShow(settingUI);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            UIController.Instance.ClearStackAndSetInitialPage(MainMenuUI);
        });
    }
}
