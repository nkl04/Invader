using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIPage
{
    [SerializeField] private SettingUI settingUI;
    [SerializeField] private SelectLevelUI selectLevelUI;

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            UIController.Instance.PushAndShow(selectLevelUI);
        });
        settingButton.onClick.AddListener(() =>
        {
            UIController.Instance.PushAndShowWithoutHidingPrevious(settingUI);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Show();
    }
}
