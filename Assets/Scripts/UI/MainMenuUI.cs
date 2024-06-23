using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIPage
{
    [SerializeField] private PlayingUI playingUI;
    [SerializeField] private SettingUI settingUI;

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            UIController.Instance.ClearStackAndSetInitialPage(playingUI);
        });
        settingButton.onClick.AddListener(() =>
        {
            UIController.Instance.PushAndShowWithoutHidingPrevious(settingUI);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
